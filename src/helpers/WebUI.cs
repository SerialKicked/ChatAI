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
using System.Diagnostics;
using System.IO;
using System.Text;

namespace LetheChat
{
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

        private static string InjectDialogHtml(string imgPath, string dialog, string? thinking, Guid messageGuid)
        {
            // dialog should already be sanitized for HTML; the pipeline calling this produces the HTML
            // Ensure both .thinking-content and .message-raw exist so JS paths always have a target.
            string think = thinking ?? string.Empty;
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
            return $"<html><head>{css}</head><body>{scripts}<div id='chatContainer'>{htmlContent}<br/></div></body></html>";
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
            return InjectDialogHtml(img, html, think, singleMessage.Guid);
        }

        public async Task RemoveLastMessage()
        {
            if (mainForm.InvokeRequired)
            {
                await InvokeAsync(new Func<Task>(RemoveLastMessage));
                return;
            }
            await web_chat.CoreWebView2.ExecuteScriptAsync(@"
                (function(){
                    const msgs = document.getElementsByClassName('chat-message');
                    if (msgs.length > 0) { msgs[msgs.length - 1].remove(); }
                })();");
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
        }

        public async Task EditMessage(string newMessage, Guid? messageGuid = null)
        {
            if (mainForm.InvokeRequired)
            {
                await InvokeAsync(new Func<Task>(async () => await EditMessage(newMessage, messageGuid)));
                return;
            }

            // Builds one atomic JS block that updates content AND sets GUID on the same wrapper
            string BuildAtomicUpdateScript(string html, bool isThinking, Guid? guid)
            {
                var guidLiteral = guid.HasValue ? guid.Value.ToString() : string.Empty;

                return $@"
(function() {{
  try {{
    const contents = document.getElementsByClassName('message-content');
    if (!contents || contents.length === 0) {{
      console.error('WebEditLastMessage: no .message-content elements found');
      return;
    }}
    const idx = contents.length - 1;

    if (typeof updateMessageAtIndex === 'function') {{
      updateMessageAtIndex(""{html}"", idx, {(isThinking ? "true" : "false")});
    }} else {{
      const messageContent = contents[idx];
      const target = {(isThinking ? "messageContent.querySelector('.thinking-content')" : "messageContent.querySelector('.message-raw')")};
      if (target) {{
        target.innerHTML = ""{html}"";
      }} else {{
        console.error('WebEditLastMessage: target element not found for isThinking=' + {(isThinking ? "true" : "false").ToString().ToLowerInvariant()});
      }}
    }}

    const wrapper = contents[idx].closest('.chat-message');
    {(messageGuid.HasValue ? $"if (wrapper) wrapper.setAttribute('data-message-guid', '{guidLiteral}');" : "")}
  }} catch (e) {{
    console.error('WebEditLastMessage: exception', e);
  }}
}})();";
            }

            if (!string.IsNullOrEmpty(LLMEngine.Instruct.ThinkingStart) &&
                newMessage.StartsWith(ChatRender.GetMessagePrefix(AuthorRole.Assistant)) &&
                newMessage.Contains(LLMEngine.Instruct.ThinkingStart.RemoveNewLines()))
            {
                // Strip assistant prefix
                var worktext = newMessage[ChatRender.GetMessagePrefix(AuthorRole.Assistant).Length..];

                if (!worktext.Contains(LLMEngine.Instruct.ThinkingEnd.RemoveNewLines()))
                {
                    // Thinking-only update
                    worktext = worktext.Replace(LLMEngine.Instruct.ThinkingStart, string.Empty);
                    var text = Markdown.ToHtml(worktext, CustomMarkDownPipeline).SanitizeForJS();
                    await web_chat.CoreWebView2.ExecuteScriptAsync(BuildAtomicUpdateScript(text, isThinking: true, messageGuid));
                }
                else
                {
                    // Thinking + final
                    var parts = worktext.Split([LLMEngine.Instruct.ThinkingEnd.RemoveNewLines()], 2, StringSplitOptions.None);

                    var thinkingText = parts[0].Replace(LLMEngine.Instruct.ThinkingStart.RemoveNewLines(), string.Empty);
                    var thinkingHtml = Markdown.ToHtml(thinkingText, CustomMarkDownPipeline).SanitizeForJS();
                    await web_chat.CoreWebView2.ExecuteScriptAsync(BuildAtomicUpdateScript(thinkingHtml, isThinking: true, messageGuid));

                    var msgoutput = ChatRender.GetMessagePrefix(AuthorRole.Assistant) + parts[1].TrimStart().TrimStart('\n').TrimStart();
                    var messageHtml = Markdown.ToHtml(msgoutput, CustomMarkDownPipeline).SanitizeForJS();
                    await web_chat.CoreWebView2.ExecuteScriptAsync(BuildAtomicUpdateScript(messageHtml, isThinking: false, messageGuid));
                }
            }
            else
            {
                var text = Markdown.ToHtml(newMessage, CustomMarkDownPipeline).SanitizeForJS();
                await web_chat.CoreWebView2.ExecuteScriptAsync(BuildAtomicUpdateScript(text, isThinking: false, messageGuid));
            }

            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
        }

        public async Task ReloadFullChat()
        {
            if (web_chat.CoreWebView2 == null)
                await InitializeWebViewAsync();

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
            web_chat.NavigateToString(html);
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
            web_chat.ZoomFactor = 1D;
        }

        public async Task LoadHistoryToUI()
        {
            if (mainForm.InvokeRequired)
            {
                await InvokeAsync(ReloadFullChat);
            }
            else
            {
                await ReloadFullChat();
            }
        }

        public async Task SendMessageToUI(SingleMessage singleMessage)
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
            var coremsg = $@"
                    <div class='portrait'>
                        <img src='https://appassets.test/img/{img}' alt='Portrait' width='60'>
                    </div>
                    <div class='message-content'>
                        <div class='message-raw'>
                            {text}
                        </div>
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
                    </div>";
            }

            coremsg = coremsg.SanitizeForJS();
            var script = $"addHtmlAfterLastChatMessage(\"{coremsg}\", \"{singleMessage.Guid}\");";
            await web_chat.CoreWebView2.ExecuteScriptAsync(script);
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
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
