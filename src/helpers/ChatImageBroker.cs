using LetheAISharp;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Security;
using System.Text;

namespace LetheChat
{
    /// <summary>
    /// Bridges between chat message image attachments (arbitrary file paths on disk) and the
    /// WebView2 chat renderers (MainForm chat, ChatHistoryForm). Since NavigateToString documents
    /// cannot reference local files, thumbnails are embedded inline as small base64 JPEG data URIs,
    /// while full-size views are served lazily through a WebResourceRequested interception on the
    /// fake "chatimg.lethe" host. Registered tokens are deduplicated by file path and remain valid
    /// for the lifetime of the app, so they can be shared by any number of WebView2 instances.
    /// </summary>
    internal static class ChatImageBroker
    {
        public const string HostName = "chatimg.lethe";
        public const string UrlPrefix = "https://" + HostName + "/i/";

        /// <summary>
        /// CSS rules for the thumbnail strip and the click-to-show lightbox overlay.
        /// Embed inside a style tag of the chat page.
        /// </summary>
        public const string LightboxCss = @"
                    .chat-thumbs {
                        display: flex;
                        flex-wrap: wrap;
                        gap: 6px;
                        padding: 2px 0 8px 0;
                    }

                    .chat-thumb {
                        max-height: 80px;
                        max-width: 128px;
                        border: 1px solid #777;
                        border-radius: 4px;
                        cursor: pointer;
                        opacity: 0.85;
                    }

                    .chat-thumb:hover {
                        opacity: 1;
                        border-color: gold;
                    }

                    #img-lightbox {
                        display: none;
                        position: fixed;
                        top: 0;
                        left: 0;
                        right: 0;
                        bottom: 0;
                        background-color: rgba(0, 0, 0, 0.9);
                        z-index: 9999;
                        align-items: center;
                        justify-content: center;
                        cursor: zoom-out;
                    }

                    #img-lightbox img {
                        max-width: 94vw;
                        max-height: 94vh;
                        border: 1px solid #888;
                    }";

        /// <summary>
        /// Script tag implementing the click-to-show lightbox. Uses event delegation on
        /// #chatContainer, so it also covers messages added dynamically after page load.
        /// </summary>
        public const string LightboxScriptTag = @"
                <script>
                    function showImageLightbox(url) {
                        let lb = document.getElementById('img-lightbox');
                        if (!lb) {
                            lb = document.createElement('div');
                            lb.id = 'img-lightbox';
                            const lbImg = document.createElement('img');
                            lb.appendChild(lbImg);
                            lb.addEventListener('click', function () {
                                lb.style.display = 'none';
                                lbImg.src = '';
                            });
                            document.body.appendChild(lb);
                        }
                        lb.querySelector('img').src = url;
                        lb.style.display = 'flex';
                    }
                    document.addEventListener('DOMContentLoaded', (event) =>
                    {
                        const chatContainer = document.getElementById('chatContainer');
                        if (!chatContainer) return;
                        chatContainer.addEventListener('click', (event) =>
                        {
                            const clicked = event.target;
                            if (clicked && clicked.classList && clicked.classList.contains('chat-thumb'))
                            {
                                const url = clicked.getAttribute('data-full');
                                if (url) { showImageLightbox(url); }
                            }
                        });
                    });
                </script>";

        private const int ThumbMaxSize = 128;
        private const int ThumbJpegQuality = 70;
        private const int FullViewMaxSize = 1600;
        private const int FullViewJpegQuality = 85;
        private const long MaxOriginalFileBytes = 12 * 1024 * 1024; // serve originals up to 12 MB

        private static readonly ConcurrentDictionary<string, string> _tokenToPath = new(StringComparer.Ordinal);
        private static readonly ConcurrentDictionary<string, string> _pathToToken = new(StringComparer.OrdinalIgnoreCase);
        private static readonly ConcurrentDictionary<string, (DateTime WriteTimeUtc, string Data)> _thumbCache = new(StringComparer.OrdinalIgnoreCase);
        private const int ThumbCacheCap = 128;

        /// <summary>
        /// Serves a chat image for a WebResourceRequested event, when the requested URI is a
        /// broker URL. Register with AddWebResourceRequestedFilter(UrlPrefix + "*", Image) first.
        /// Shared by every WebView2 that renders chat messages.
        /// </summary>
        public static void HandleWebResourceRequested(CoreWebView2 core, CoreWebView2WebResourceRequestedEventArgs e)
        {
            try
            {
                var uri = e.Request.Uri;
                if (!uri.StartsWith(UrlPrefix, StringComparison.OrdinalIgnoreCase))
                    return;
                var token = uri[UrlPrefix.Length..].Trim('/');
                var (content, contentType) = OpenFullImage(token);
                if (content == null)
                {
                    e.Response = core.Environment.CreateWebResourceResponse(
                        null, 404, "Not Found", "Content-Type: text/plain");
                    return;
                }
                e.Response = core.Environment.CreateWebResourceResponse(
                    content, 200, "OK", $"Content-Type: {contentType}\r\nCache-Control: max-age=86400");
            }
            catch (Exception ex)
            {
                LLMEngine.Logger?.LogError(ex, "Error serving chat image resource");
            }
        }

        /// <summary>
        /// Registers an image file and returns the broker URL used for the full-size view.
        /// </summary>
        private static string RegisterImage(string path)
        {
            var token = _pathToToken.GetOrAdd(path, static p =>
            {
                var t = Guid.NewGuid().ToString("N");
                _tokenToPath[t] = p;
                return t;
            });
            return UrlPrefix + token;
        }

        /// <summary>
        /// Builds the thumbnail strip HTML for a message, or an empty string when the message
        /// has no displayable image attachments. Missing or unreadable files are skipped,
        /// mirroring the prompt-building behavior.
        /// </summary>
        public static string BuildThumbsHtml(SingleMessage message)
        {
            if (message.ImagePaths.Count == 0)
                return string.Empty;

            var sb = new StringBuilder();
            foreach (var path in message.ImagePaths)
            {
                if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                    continue;
                var thumb = GetThumbBase64(path);
                if (thumb == null)
                    continue;
                var fullUrl = RegisterImage(path);
                var name = SecurityElement.Escape(Path.GetFileName(path)) ?? "image";
                sb.Append("<img class='chat-thumb' src='data:image/jpeg;base64,")
                    .Append(thumb)
                    .Append("' data-full='").Append(fullUrl)
                    .Append("' title='").Append(name)
                    .Append("' alt='").Append(name).Append("' />");
            }
            if (sb.Length == 0)
                return string.Empty;
            return $"<div class='chat-thumbs'>{sb}</div>";
        }

        /// <summary>
        /// Opens a stream for the full-size view of a registered image. Browser-friendly formats
        /// of reasonable size are served as-is; oversized or non-displayable files (e.g. TIFF)
        /// are re-encoded to a bounded JPEG.
        /// </summary>
        public static (Stream? Content, string ContentType) OpenFullImage(string token)
        {
            if (!_tokenToPath.TryGetValue(token, out var path) || !File.Exists(path))
                return (null, string.Empty);

            var mime = Path.GetExtension(path).ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                ".bmp" => "image/bmp",
                _ => string.Empty,
            };

            try
            {
                if (mime.Length > 0 && new FileInfo(path).Length <= MaxOriginalFileBytes)
                    return (new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read), mime);

                return (EncodeJpeg(path, FullViewMaxSize, FullViewJpegQuality), "image/jpeg");
            }
            catch (Exception ex)
            {
                LLMEngine.Logger?.LogError(ex, "Error serving chat image: {Path}", path);
                return (null, string.Empty);
            }
        }

        private static string? GetThumbBase64(string path)
        {
            try
            {
                var writeTime = File.GetLastWriteTimeUtc(path);
                if (_thumbCache.TryGetValue(path, out var cached) && cached.WriteTimeUtc == writeTime)
                    return cached.Data;

                using var ms = EncodeJpeg(path, ThumbMaxSize, ThumbJpegQuality);
                var data = Convert.ToBase64String(ms.ToArray());
                if (_thumbCache.Count >= ThumbCacheCap)
                    _thumbCache.Clear();
                _thumbCache[path] = (writeTime, data);
                return data;
            }
            catch (Exception ex)
            {
                LLMEngine.Logger?.LogError(ex, "Error generating chat image thumbnail: {Path}", path);
                return null;
            }
        }

        private static MemoryStream EncodeJpeg(string path, int maxSize, int quality)
        {
            using var image = SixLabors.ImageSharp.Image.Load(path);
            var scaled = ImageUtils.ScaleImage(image, maxSize);
            var ownsScaled = !ReferenceEquals(scaled, image);
            try
            {
                var ms = new MemoryStream();
                scaled.Save(ms, new JpegEncoder { Quality = quality });
                ms.Position = 0;
                return ms;
            }
            finally
            {
                if (ownsScaled)
                    scaled.Dispose();
            }
        }
    }
}
