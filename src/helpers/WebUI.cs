using CommunityToolkit.HighPerformance;
using LetheAISharp;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheChat.Files;
using LetheChat.Forms;
using Markdig;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace LetheChat
{
    public static class HelpTool
    {
        public static string? Tip<T>(string propertyName) => typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance)
        ?.GetCustomAttribute<DescriptionAttribute>()
        ?.Description;

        public static void ApplyTooltip(ToolTip tipmanager, Control target, string? tip)
        {
            if (string.IsNullOrEmpty(tip))
                return;
            tipmanager.SetToolTip(target, tip);
            foreach (Control child in target.Controls)
            {
                tipmanager.SetToolTip(child, tip);
                foreach (Control grandChild in child.Controls)
                    tipmanager.SetToolTip(grandChild, tip);
            }
        }

        public static void ApplyTooltip<T>(ToolTip tipmanager, Control target, string propertyName)
        {
            var tip = Tip<T>(propertyName);
            if (string.IsNullOrEmpty(tip))
                return;
            tipmanager.SetToolTip(target, tip);
            foreach (Control child in target.Controls)
            {
                tipmanager.SetToolTip(child, tip);
                foreach (Control grandChild in child.Controls)
                    tipmanager.SetToolTip(grandChild, tip);
            }
        }
    }

    public class WebUI(MainForm form, WebView2 webview)
    {
        /// <summary>
        /// Custom markdown pipeline with extensions
        /// </summary>
        public static MarkdownPipeline CustomMarkDownPipeline { get; } = new MarkdownPipelineBuilder()
            .UseSoftlineBreakAsHardlineBreak().UseAdvancedExtensions()
            .UseEmojiAndSmiley()
            .UseAutoLinks()
            .Use(new QuoteColorExtension())
            .Build();   

        private readonly WebView2 web_chat = webview;
        private readonly MainForm mainForm = form;

        /// <summary>
        /// // Helper method to use Invoke with async methods
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        private Task<bool> InvokeAsync(Func<Task> func)
        {
            var tcs = new TaskCompletionSource<bool>();
            mainForm.BeginInvoke(new Action(async () =>
            {
                try
                {
                    await func();
                    tcs.SetResult(true);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }));
            return tcs.Task;
        }

        #region *** DOM operation serialization ***

        // WebView2 is not forgiving: a script executed while a navigation is in flight runs against a document that is
        // about to be thrown away (or not at all), and a reload that is not awaited makes every later script racy.
        // So every operation touching the page goes through RunDom: they run one at a time, in arrival order, on the UI
        // thread, and a reload only completes once the new document is loaded.
        private readonly SemaphoreSlim _domGate = new(1, 1);

        private async Task RunDom(Func<Task> op)
        {
            await _domGate.WaitAsync().ConfigureAwait(false);
            try
            {
                if (mainForm.IsDisposed)
                    return;
                if (mainForm.InvokeRequired)
                    await InvokeAsync(op).ConfigureAwait(false);
                else
                    await op();
            }
            finally
            {
                _domGate.Release();
            }
        }

        #endregion

        #region *** Live message tracking ***

        // The "live" message is the placeholder of the response being generated. Streaming edits address it by guid
        // (never "the last message"), so an edit that arrives late - after a cancel, a reload, or once the message got its
        // final guid - finds nothing and is dropped instead of overwriting an unrelated message.
        private readonly object _liveLock = new();
        private Guid _liveGuid = Guid.Empty;

        /// <summary>
        /// Guid of the message currently being generated on screen, or <see cref="Guid.Empty"/> when there is none.
        /// </summary>
        public Guid LiveMessageGuid
        {
            get { lock (_liveLock) return _liveGuid; }
        }

        private void SetLive(Guid guid)
        {
            lock (_liveLock) _liveGuid = guid;
        }

        private void ClearLiveIf(Guid guid)
        {
            lock (_liveLock)
            {
                if (_liveGuid == guid)
                    _liveGuid = Guid.Empty;
            }
        }

        #endregion

        private static string InjectDialogHtml(string imgPath, string dialog, string? thinking, Guid messageGuid, string? imagesHtml = null)
        {
            // dialog should already be sanitized for HTML; the pipeline calling this produces the HTML
            // Ensure both .thinking-content and .message-raw exist so JS paths always have a target.
            string think = thinking ?? string.Empty;
            string images = imagesHtml ?? string.Empty;
            return $@"
        <div class='chat-message' data-message-guid='{messageGuid}'>
            <div class='portrait'>
                <img src='https://appassets.test/img/{imgPath}' alt='Portrait' width='60'>
            </div>
            <div class='message-content'>
                <div class='thinking-content'>{think}</div>
                <div class='message-raw'>
                    {dialog}
                </div>
                {images}
            </div>
        </div>";
        }

        private static string InjectDialogCSS(string htmlContent)
        {
            string css = $@"
                <style>
                    body {{ 
                        max-height: 100%;
                        overflow-y: auto;
                        overflow-x: hidden;
                        padding: 16px;
                        font-size: {Program.Settings.FontSize}px;
                        width: 100%;
                        box-sizing: border-box;
                        background-image: url('https://appassets.test/background/{Program.Settings.BackgroundFile}');
                        background-size: cover; /* Ensures the image covers the entire background */
                        background-attachment: fixed; /* Keeps the background image fixed in place */
                        background-position: center; /* Centers the background image */
                        background-repeat: no-repeat; /* Prevents the background image from repeating */
                    }}
                    em {{ color: yellow; }}
                    strong {{ color: Tomato }}
                    a {{ color: gold }}
                    h1 {{ font-size: 1.3em; }}
                    h2 {{ font-size: 1.25em; }}
                    h3 {{ font-size: 1.2em; }}
                    h4 {{ font-size: 1.15em; }}
                    h5 {{ font-size: 1.1em; }}

                    .chat-message {{
                        display: flex;
                        align-items: flex-start;
                        margin-bottom: 10px;
                        border: 1px solid gray;
                        background-color: rgba(0, 0, 0, 0.75);
                        color: rgb(200, 200, 200);
                    }}
                    .chatContainer {{
                    }}

                    .portrait {{
                        flex: 0 0 70px;
                        padding: 10px;
                        margin-right: 0px;
                    }}

                    .thinking-box {{margin: 5px 0;
                        border: 1px solid #444;
                        border-radius: 4px;
                        overflow: hidden;
                    }}

                    .thinking-header {{padding: 5px 10px;
                        background-color: rgba(80, 80, 80, 0.5);
                        cursor: pointer;
                        user-select: none;
                    }}

                    .thinking-content {{display: none;
                        padding: 10px;
                        background-color: rgba(40, 40, 40, 0.5);
                    }}

                    .thinking-box.expanded .thinking-content {{display: block;
                    }}

                    .message-content {{
                        flex: 1;
                        word-wrap: break-word;
                        padding-right: 10px;
                    }}
                    {ChatImageBroker.LightboxCss}
                </style>";
                        string scripts = @"
                <script>
                    function updateMessageAtIndex(text, index, isthink) {
                        const messageContents = document.getElementsByClassName('message-content');
                        if (index >= 0 && index < messageContents.length) {
                            const messageContent = messageContents[index];
                            const target = isthink ? 
                                messageContent.querySelector('.thinking-content') : 
                                messageContent.querySelector('.message-raw');

                            if (target) {
                                target.innerHTML = text;
                            } else {
                                console.error('Target element not found');
                            }
                        } else {
                            console.error('Index out of bounds');
                        }
                    }
                    function updateMessageBothAtIndex(messageHtml, thinkingHtml, index) {
                        const messageContents = document.getElementsByClassName('message-content');
                        if (index >= 0 && index < messageContents.length) {
                            const messageContent = messageContents[index];
                            const msgTarget = messageContent.querySelector('.message-raw');
                            if (msgTarget) {
                                msgTarget.innerHTML = messageHtml;
                            } else {
                                console.error('updateMessageBothAtIndex: .message-raw not found');
                            }
                            const thinkTarget = messageContent.querySelector('.thinking-content');
                            if (thinkTarget) {
                                thinkTarget.innerHTML = thinkingHtml || '';
                            } else {
                                console.error('updateMessageBothAtIndex: .thinking-content not found');
                            }
                        } else {
                            console.error('updateMessageBothAtIndex: index out of bounds');
                        }
                    }
                    function addHtmlAfterLastChatMessage(htmlContent, messageGuid) {
                        const container = document.getElementById('chatContainer') || document.body;
                        const chatMessages = container.querySelectorAll('.chat-message');

                        const newDiv = document.createElement('div');
                        newDiv.className = 'chat-message';
                        newDiv.setAttribute('data-message-guid', messageGuid);
                        newDiv.innerHTML = htmlContent;

                        if (chatMessages.length > 0) {
                            const lastChatMessage = chatMessages[chatMessages.length - 1];
                            lastChatMessage.insertAdjacentElement('afterend', newDiv);
                        } else {
                            // No previous messages: append as first message
                            container.appendChild(newDiv);
                        }
                    }
                    document.addEventListener('DOMContentLoaded', (event) => 
                    {
                        const chatContainer = document.getElementById('chatContainer');
                        chatContainer.addEventListener('dblclick', (event) => 
                        {
                            if (event.target && event.target.classList && event.target.classList.contains('chat-thumb'))
                                return;
                            let targetElement = event.target;
                            while (targetElement && !targetElement.classList.contains('chat-message')) 
                            {
                                targetElement = targetElement.parentElement;
                            }
                            if (targetElement && targetElement.classList.contains('chat-message')) 
                            {
                                const messageGuid = targetElement.getAttribute('data-message-guid');
                                window.chrome.webview.postMessage({ type: 'EditMessage', guid: messageGuid });
                            }
                        });
                    });         
                </script>";
            return $"<html><head>{css}</head><body>{scripts}{ChatImageBroker.LightboxScriptTag}<div id='chatContainer'>{htmlContent}<br/></div></body></html>";
        }

        private static string AddHtmlMessage(SingleMessage singleMessage)
        {
            string img = "gears.png";
            switch (singleMessage.Role)
            {
                case AuthorRole.User:
                    img = (singleMessage.User as ICharacter)!.Icon;
                    break;
                case AuthorRole.Assistant:
                    img = (singleMessage.Bot as ICharacter)!.Icon;
                    break;
                case AuthorRole.Tool:
                    img = "tools.png";
                    break;
            }
            var msg = singleMessage.Message;
            var think = singleMessage.ThinkBlock;
            if (singleMessage.ToolCalls.Count > 0 && singleMessage.Role != AuthorRole.Tool)
            {
                msg += "\n\nTool calls:\n";
                foreach (var call in singleMessage.ToolCalls)
                {
                    var res = call.Success ? "Success" : "Failure";
                    msg += $"- {call.CallId}: {call.FunctionName}() => {res} in {(int)call.Duration.TotalMilliseconds}ms\n";
                }
            }

            var html = Markdown.ToHtml(ChatRender.GetMessagePrefix(singleMessage) + msg, CustomMarkDownPipeline);
            if (!string.IsNullOrEmpty(think))
            {
                think = Markdown.ToHtml(think, CustomMarkDownPipeline);
            }
            return InjectDialogHtml(img, html, think, singleMessage.Guid, ChatImageBroker.BuildThumbsHtml(singleMessage));
        }


        /// <summary>
        /// Removes the last message element of the page (reroll / delete-last). For a message you know the guid of, prefer <see cref="RemoveMessage"/>.
        /// </summary>
        public Task RemoveLastMessage() => RunDom(async () =>
        {
            if (web_chat.CoreWebView2 == null)
                return;
            await web_chat.CoreWebView2.ExecuteScriptAsync(@"
                (function(){
                    const msgs = document.getElementsByClassName('chat-message');
                    if (msgs.length > 0) { msgs[msgs.length - 1].remove(); }
                })();");
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
        });

        /// <summary>
        /// Removes the message with the given guid from the page (no effect on the chat history).
        /// </summary>
        /// <returns>true if the message was on the page</returns>
        public async Task<bool> RemoveMessage(Guid messageGuid)
        {
            var found = false;
            await RunDom(async () =>
            {
                if (web_chat.CoreWebView2 == null)
                    return;
                var res = await web_chat.CoreWebView2.ExecuteScriptAsync(
                    $"(function(){{ const el = document.querySelector('.chat-message[data-message-guid=\"{messageGuid}\"]'); if (!el) return false; el.remove(); return true; }})();");
                found = res == "true";
            });
            ClearLiveIf(messageGuid);
            return found;
        }

        #region *** Message edits (coalesced) ***

        // Streaming edits are produced faster than WebView2 can apply them. Instead of piling them up, edits for the same
        // message that are still waiting are merged (the newest wins). A "final" edit (one that renames the message to its
        // history guid) is never overwritten by a streaming edit, and edits for different messages are never merged.
        private sealed class PendingEdit(string messageHtml, string thinkingHtml, Guid? newGuid, Guid target)
        {
            public string MessageHtml { get; private set; } = messageHtml;
            public string ThinkingHtml { get; private set; } = thinkingHtml;
            public Guid? NewGuid { get; private set; } = newGuid;
            public Guid Target { get; } = target;
            public TaskCompletionSource<bool> Done { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

            public void Update(string messageHtml, string thinkingHtml, Guid? newGuid)
            {
                MessageHtml = messageHtml;
                ThinkingHtml = thinkingHtml;
                NewGuid = newGuid;
            }
        }

        private readonly object _editLock = new();
        private readonly List<PendingEdit> _editQueue = [];
        private bool _editPumpRunning;

        /// <summary>
        /// Replaces the content of a message on the page. The message is addressed by guid: <paramref name="targetGuid"/>, or
        /// the live (being generated) message when omitted. If that message is not on the page (cancelled, reloaded, already
        /// finalized) nothing is touched and false is returned: it never falls back to "the last message".
        /// </summary>
        /// <param name="message">markdown of the message</param>
        /// <param name="thinking">markdown of the thinking block</param>
        /// <param name="newGuid">when set, the message element is renamed to this guid (final edit, it becomes the history message)</param>
        /// <param name="targetGuid">message to edit; defaults to <see cref="LiveMessageGuid"/></param>
        /// <returns>true if the edit was applied</returns>
        public async Task<bool> EditMessage(string message, string? thinking = null, Guid? newGuid = null, Guid? targetGuid = null)
        {
            var target = targetGuid ?? LiveMessageGuid;
            if (target == Guid.Empty)
                return false;

            var messageHtml = Markdown.ToHtml(message, CustomMarkDownPipeline).SanitizeForJS();
            var thinkingHtml = !string.IsNullOrEmpty(thinking)
                ? Markdown.ToHtml(thinking, CustomMarkDownPipeline).SanitizeForJS()
                : string.Empty;

            Task<bool> waiter;
            var startPump = false;
            lock (_editLock)
            {
                var tail = _editQueue.Count > 0 ? _editQueue[^1] : null;
                if (tail != null && tail.Target == target && tail.NewGuid == null)
                {
                    // merge into the waiting streaming edit (a final edit simply makes it final)
                    tail.Update(messageHtml, thinkingHtml, newGuid);
                    waiter = tail.Done.Task;
                }
                else if (tail != null && tail.Target == target && tail.NewGuid != null && newGuid == null)
                {
                    // a streaming edit that trails the final one: obsolete
                    waiter = tail.Done.Task;
                }
                else
                {
                    var edit = new PendingEdit(messageHtml, thinkingHtml, newGuid, target);
                    _editQueue.Add(edit);
                    waiter = edit.Done.Task;
                    if (!_editPumpRunning)
                    {
                        _editPumpRunning = true;
                        startPump = true;
                    }
                }
            }
            if (startPump)
                _ = PumpEditsAsync();
            return await waiter.ConfigureAwait(false);
        }

        private async Task PumpEditsAsync()
        {
            while (true)
            {
                PendingEdit edit;
                lock (_editLock)
                {
                    if (_editQueue.Count == 0)
                    {
                        _editPumpRunning = false;
                        return;
                    }
                    edit = _editQueue[0];
                    _editQueue.RemoveAt(0);
                }
                var ok = false;
                try
                {
                    await RunDom(async () => ok = await ApplyEditAsync(edit)).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error while editing a message: {ex}");
                }
                edit.Done.TrySetResult(ok);
            }
        }

        private async Task<bool> ApplyEditAsync(PendingEdit edit)
        {
            if (web_chat.CoreWebView2 == null)
                return false;

            var rename = edit.NewGuid.HasValue ? $"wrapper.setAttribute('data-message-guid', '{edit.NewGuid.Value}');" : string.Empty;
            var script = $@"
(function() {{
  try {{
    const wrapper = document.querySelector('.chat-message[data-message-guid=""{edit.Target}""]');
    if (!wrapper) return false;
    const content = wrapper.querySelector('.message-content');
    if (!content) return false;
    const msgTarget = content.querySelector('.message-raw');
    if (msgTarget) {{ msgTarget.innerHTML = ""{edit.MessageHtml}""; }}
    const thinkTarget = content.querySelector('.thinking-content');
    if (thinkTarget) {{ thinkTarget.innerHTML = ""{edit.ThinkingHtml}""; }}
    {rename}
    window.scrollTo(0, document.body.scrollHeight);
    return true;
  }} catch (e) {{
    console.error('EditMessage: exception', e);
    return false;
  }}
}})();";

            var res = await web_chat.CoreWebView2.ExecuteScriptAsync(script);
            var ok = res == "true";
            if (ok && edit.NewGuid.HasValue)
                ClearLiveIf(edit.Target);
            return ok;
        }

        #endregion

        /// <summary>
        /// Rebuilds the page from the chat history and returns once the new document is loaded.
        /// Anything that is not in the history (a placeholder being generated) is gone afterwards.
        /// </summary>
        public Task ReloadFullChat() => RunDom(ReloadCoreAsync);

        private async Task ReloadCoreAsync()
        {
            if (web_chat.CoreWebView2 == null)
                await InitializeWebViewAsync();

            // The page is rebuilt from the history: a placeholder is not part of it.
            SetLive(Guid.Empty);

            var html = string.Empty;
            var start = LLMEngine.History.CurrentSession.Messages.Count - Program.Settings.MaxMessagesOnScreen;
            if (start < 0)
                start = 0;
            for (int i = start; i < LLMEngine.History.CurrentSession.Messages.Count; i++)
            {
                if (!LLMEngine.History.CurrentSession.Messages[i].Hidden || Program.Settings.ShowHiddenMessages)
                    html += AddHtmlMessage(LLMEngine.History.CurrentSession.Messages[i]);
            }
            html = InjectDialogCSS(html);

            // NavigateToString returns immediately. Scripts executed before the new document is up would hit the old
            // one (or nothing), so wait for the navigation to complete before releasing the DOM gate.
            var navDone = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            void OnNavCompleted(object? s, CoreWebView2NavigationCompletedEventArgs e) => navDone.TrySetResult(e.IsSuccess);
            web_chat.CoreWebView2!.NavigationCompleted += OnNavCompleted;
            try
            {
                web_chat.NavigateToString(html);
                var finished = await Task.WhenAny(navDone.Task, Task.Delay(10000));
                if (finished != navDone.Task)
                    Debug.WriteLine("ReloadFullChat: navigation did not complete in time.");
            }
            finally
            {
                web_chat.CoreWebView2.NavigationCompleted -= OnNavCompleted;
            }
        }

        private void OpenEditMessageMenu(Guid messageGuid)
        {
            if (LLMEngine.Status == SystemStatus.Busy)
                return;

            mainForm.Enabled = false;
            using var _editMessage = new EditMessageForm(messageGuid)
            {
                TopMost = true,
                StartPosition = FormStartPosition.CenterParent
            };
            _editMessage.Refresh();
            try
            {
                if (_editMessage.ShowDialog() == DialogResult.OK && _editMessage.Message != null)
                {
                    mainForm.BeginInvoke((System.Windows.Forms.MethodInvoker)async delegate
                    {
                        await LoadHistoryToUI();
                        LLMEngine.InvalidatePromptCache();
                    });
                }
            }
            finally
            {
                mainForm.Enabled = true;
            }
        }

        public async Task InitializeWebViewAsync()
        {
            if (web_chat.CoreWebView2 != null) return;
            await web_chat.EnsureCoreWebView2Async();
            web_chat.CoreWebView2!.Settings.AreDevToolsEnabled = false;
            web_chat.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            web_chat.CoreWebView2.SetVirtualHostNameToFolderMapping(
                "appassets.test",
                Path.Combine(AppContext.BaseDirectory, "data"),
                CoreWebView2HostResourceAccessKind.Allow);

            web_chat.CoreWebView2.DOMContentLoaded += OnWebChatContentLoaded!;
            web_chat.CoreWebView2.WebMessageReceived += OnWebChatWebMessageReceived!;
            web_chat.CoreWebView2.NewWindowRequested += OnNewWindowRequested;
            web_chat.CoreWebView2.NavigationStarting += OnNavigationStarting;
            web_chat.CoreWebView2.AddWebResourceRequestedFilter(
                ChatImageBroker.UrlPrefix + "*",
                CoreWebView2WebResourceContext.Image);
            web_chat.CoreWebView2.WebResourceRequested += OnChatImageRequested;
            web_chat.ZoomFactor = 1D;
        }

        public Task LoadHistoryToUI() => ReloadFullChat();

        /// <summary>
        /// Appends a message after the last one on the page (the chat history is not touched).
        /// </summary>
        /// <param name="singleMessage">message to display</param>
        /// <param name="live">true for the placeholder of a response about to be generated: it becomes the live message, the
        /// target of <see cref="EditMessage"/> calls that do not name one</param>
        public async Task SendMessageToUI(SingleMessage singleMessage, bool live = false)
        {
            string img = "gears.png";
            switch (singleMessage.Role)
            {
                case AuthorRole.User:
                    img = MainForm.User?.Icon ?? "gears.png";
                    break;
                case AuthorRole.Assistant:
                    img = MainForm.Bot?.Icon ?? "gears.png";
                    break;
                //case AuthorRole.Tool:
                case AuthorRole.Tool:
                    img = "tools.png";
                    break;
            }
            var text = Markdown.ToHtml(ChatRender.GetMessagePrefix(singleMessage) + singleMessage.Message, WebUI.CustomMarkDownPipeline);
            var thumbs = ChatImageBroker.BuildThumbsHtml(singleMessage);
            var coremsg = $@"
                    <div class='portrait'>
                        <img src='https://appassets.test/img/{img}' alt='Portrait' width='60'>
                    </div>
                    <div class='message-content'>
                        <div class='message-raw'>
                            {text}
                        </div>
                        {thumbs}
                    </div>";

            if (singleMessage.Role == AuthorRole.Assistant && !string.IsNullOrEmpty(LLMEngine.Instruct.ThinkingStart))
            {
                coremsg = $@"
                    <div class='portrait'>
                        <img src='https://appassets.test/img/{img}' alt='Portrait' width='60'>
                    </div>
                    <div class='message-content'>
                        <div class='thinking-box'>
                            <div class='thinking-header' onclick='this.parentElement.classList.toggle(""expanded"")'>
                                {LLMEngine.Bot.Name} is thinking... (click to expand)
                            </div>
                            <div class='thinking-content'> 
                            </div>
                        </div>
                        <div class='message-raw'>
                            {text}
                        </div>
                        {thumbs}
                    </div>";
            }

            coremsg = coremsg.SanitizeForJS();
            var script = $"addHtmlAfterLastChatMessage(\"{coremsg}\", \"{singleMessage.Guid}\");";
            await RunDom(async () =>
            {
                if (web_chat.CoreWebView2 == null)
                    return;
                await web_chat.CoreWebView2.ExecuteScriptAsync(script);
                // The page owns the element now: edits can address it.
                if (live)
                    SetLive(singleMessage.Guid);
                await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
            });
        }


        #region *** Event Handlers for WebView2 ***

        private void OnNavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
        {
            var url = e.Uri;
            if (url.StartsWith("https://") || url.StartsWith("http://"))
            {
                e.Cancel = true; // Prevent the WebView2 control from opening the link
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
        }

        private void OnNewWindowRequested(object? sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true; // Prevent the WebView2 control from opening the link
            var url = e.Uri;
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private async void OnWebChatContentLoaded(object sender, CoreWebView2DOMContentLoadedEventArgs e)
        {
            if (web_chat?.CoreWebView2 != null)
            {
                await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
            }
        }

        /// <summary>
        /// Serves full-size versions of attached chat images on demand (lightbox view).
        /// Thumbnails are embedded inline as base64, so this only fires when a user
        /// actually clicks a thumbnail.
        /// </summary>
        private void OnChatImageRequested(object? sender, CoreWebView2WebResourceRequestedEventArgs e)
        {
            if (web_chat?.CoreWebView2 != null)
                ChatImageBroker.HandleWebResourceRequested(web_chat.CoreWebView2, e);
        }

        private void OnWebChatWebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                var message = e.WebMessageAsJson;
                if (string.IsNullOrEmpty(message))
                    return;
                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(message);
                if (json == null || !json.TryGetValue("type", out object? value) || value.ToString() != "EditMessage")
                    return;
                if (!json.TryGetValue("guid", out object? guidObj))
                    return;

                if (!Guid.TryParse(guidObj.ToString(), out Guid messageGuid))
                    return;

                // Use BeginInvoke instead of Invoke to avoid potential deadlocks
                mainForm.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        if (!mainForm.IsDisposed && mainForm.IsHandleCreated)
                        {
                            OpenEditMessageMenu(messageGuid);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error in EditMessage: {ex}");
                    }
                }));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnWebChatWebMessageReceived: {ex}");
            }
        }

        #endregion
    }
}
