using AIToolkit;
using AIToolkit.Agent;
using AIToolkit.Files;
using AIToolkit.LLM;
using AIToolkit.Memory;
using AIToolkit.SearchAPI;
using AngleSharp.Text;
using Markdig;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WaifuAI.AgentPlugins;
using WaifuAI.Files;
using WaifuAI.Game;
using WaifuAI.Plugins;
using WaifuAI.src.forms;
using WaifuAI.Web;

namespace WaifuAI
{
    public partial class MainForm : Form
    {
        private string? _currentgeneration = null;
        private int _currentgenerationtokencount = 0;
        private int _currentgencalls = 0;
        private bool _impersonatemode = false;
        private bool _forcereload = false;
        private bool _isinitloading = true;
        private DateTime _postdate = DateTime.Now;
        private TimeSpan _responselength = default;
        private readonly ActivityTimer _activityTimer = new();
        private int _afkmessagecount = 0;
        private EditMessageForm? _editMessageForm;
        private readonly Random RNG = new();
        private RenPyDialogHandler? _renpyDialogHandler;
        private string ed_log = string.Empty;

        public static Character? Bot => LLMEngine.Bot as Character;
        public static Character? User => LLMEngine.User as Character;

        /// <summary>
        /// Custom markdown pipeline with extensions
        /// </summary>
        public static MarkdownPipeline CustomMarkDownPipeline { get; } = new MarkdownPipelineBuilder()
            .UseSoftlineBreakAsHardlineBreak().UseAdvancedExtensions()
            .UseEmojiAndSmiley()
            .UseAutoLinks()
            .Use(new QuoteColorExtension())
            .Build();

        /// <summary>
        /// Intercepts app level mouse/keyboard activity to check if user active or not
        /// </summary>
        public class ActivityMessageFilter : IMessageFilter
        {
            public bool PreFilterMessage(ref Message m)
            {
                // Check for input messages globally
                if (LLMEngine.Bot?.AgentSystem is not null && IsInputMessage(m.Msg))
                {
                    // Signal user activity
                    LLMEngine.Bot?.AgentSystem.NotifyUserActivity();
                }
                return false; // Don't consume the message
            }

            private static bool IsInputMessage(int msg)
            {
                return msg >= 0x0100 && msg <= 0x0108 || // Keyboard messages
                       msg >= 0x0201 && msg <= 0x020E;   // Mouse messages
            }
        }

        /// <summary>
        /// // Helper method to use Invoke with async methods
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        private Task<bool> InvokeAsync(Func<Task> func)
        {
            var tcs = new TaskCompletionSource<bool>();
            BeginInvoke(new Action(async () =>
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

        public MainForm()
        {
            InitializeComponent();

            ed_input.SpellCheckLanguage = "en-US";

            HelptoolTip.SetToolTip(ck_ragenabled, "Use RAG functionalities to insert summaries of relevant previous sessions based on the user's input." + Environment.NewLine + "Configurable in the Program.Settings tab.");
            HelptoolTip.SetToolTip(ck_senseoftime, "Insert day and time information to prompt when relevant to give the bot a better understanding of time.");
            HelptoolTip.SetToolTip(ck_sessionmemory, "Use a set amount of tokens (set in Program.Settings) to insert summaries of previous chat sessions with this bot." + Environment.NewLine + "This drastically increases the bot's long-term memory.");
            HelptoolTip.SetToolTip(ck_worldinfo, "Use the WorldInfo file(s) associated with this bot. WorldInfo is a list of keyword-triggered textual information that is inserted into the prompt when the conditions are met." + Environment.NewLine + "See the World Info tab for additional information.");

            HelptoolTip.SetToolTip(ck_charsampler, "If checked, and when using a bot persona containing a list of compatible inference Program.Settings, the inference Program.Settings will be picked at random from that list each time the bot write a new message." + Environment.NewLine + Environment.NewLine + "Will lead to a more creative and less repetitive interaction, but also less consistent.");
            HelptoolTip.SetToolTip(ck_onlinerag, "If checked, the bot may perform a web search (using DuckDuckGo) to improve its responses when asked to.");

            // Load our app level agent plugins
            AgentRuntime.RegisterPlugin("GoalDesignerTask", new GoalDesignerTask());
            AgentRuntime.RegisterPlugin("CustomGoalTask", new CustomGoalTask());

            // Chat related events
            SetupChatMenu();
            _isinitloading = false;
            _activityTimer.OnTrigger += OnBotInitiateConversation;

            // Autodetection of the user's activity (mostly for the background agent functionalities)
            Application.AddMessageFilter(new ActivityMessageFilter());

            ed_input.KeyPress += Ed_input_KeyPress!;
        }

        /// <summary>
        /// Loads the chat menu with available characters, inference settings, system prompts and instructs.
        /// </summary>
        private void SetupChatMenu()
        {
            cb_bot.Items.Clear();
            cb_user.Items.Clear();
            bt_scenario.ForeColor = string.IsNullOrWhiteSpace(LLMEngine.Settings.ScenarioOverride) ? Color.Black : Color.DarkGreen;
            foreach (var item in DataFiles.Characters)
            {
                if (item.Value.IsUser)
                    cb_user.Items.Add(item.Value.UniqueName);
                else
                    cb_bot.Items.Add(item.Value.UniqueName);
            }
            cb_infer.Items.Clear();
            foreach (var item in DataFiles.Inference)
            {
                cb_infer.Items.Add(item.Value.UniqueName);
            }
            cb_instruct.Items.Clear();
            foreach (var item in DataFiles.Instruct)
            {
                cb_instruct.Items.Add(item.Value.UniqueName);
            }
            cb_sysprompt.Items.Clear();
            foreach (var item in DataFiles.SysPrompts)
            {
                cb_sysprompt.Items.Add(item.Value.UniqueName);
            }
            LoadSettings();

            // Show LoginForm
            var loginForm = new LoginForm();
            loginForm.ShowDialog(this);
            if (loginForm.DialogResult != DialogResult.OK)
            {
                MessageBox.Show("No connection with backend server. You can use the application, but you cannot chat with the AI.");
            }

            LLMEngine.Init();
            LLMEngine.ContextPlugins = [];
            LLMEngine.ContextPlugins.Add(new BrowsePlugin());
            LLMEngine.ContextPlugins.Add(new LocationPlugin("Locations"));
            LLMEngine.ContextPlugins.Add(new WebSearchPlugin());
            RAGEngine.Enabled = true;
            ck_ragenabled.Checked = RAGEngine.Enabled;
            ck_worldinfo.Checked = LLMEngine.Settings.AllowWorldInfo;
            LLMEngine.OnInferenceStreamed += OnStreamMessageReceived;
            LLMEngine.OnInferenceEnded += OnStreamInferenceEnded;
            LLMEngine.OnFullPromptReady += OnFullPromptReady;
            LLMEngine.OnStatusChanged += OnStatusChanged;

            ed_input.EnableImageDragDrop(basestr =>
            {
                LLMEngine.VLM_ClearImages();
                LLMEngine.VLM_AddB64Image(basestr);
                DisplayImage(basestr);
            }, 1024);
            pictEmbed.EnableImageDragDrop(basestr =>
            {
                LLMEngine.VLM_ClearImages();
                LLMEngine.VLM_AddB64Image(basestr);
                DisplayImage(basestr);
            }, 1024);
        }

        /// <summary>
        /// Display an image from a base64 string. Yeah we could use the image directly in prod, 
        /// but the point is to show the base64 convertion works.
        /// </summary>
        /// <param name="base64String"></param>
        private void DisplayImage(string base64String)
        {
            try
            {
                // Convert base64 back to an image to display in a PictureBox
                byte[] imageBytes = Convert.FromBase64String(base64String);
                using var ms = new MemoryStream(imageBytes);
                pictEmbed.Image = Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying image: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Refresh the UI state
        /// </summary>
        private void UpdateUIState()
        {
            _activityTimer?.Reset();
            bt_scenario.ForeColor = string.IsNullOrWhiteSpace(LLMEngine.Settings.ScenarioOverride) ? Color.Black : Color.DarkGreen;
            if (LLMEngine.Status == SystemStatus.Ready)
            {
                bt_delete.Enabled = true;
                bt_connect.Enabled = true;
                bt_send.Enabled = true;
                bt_send.Text = "Send";
                bt_send.BackColor = Color.PaleGreen;
                bt_reroll.Enabled = true;
                bt_newsession.Enabled = true;
                bt_impersonate.Enabled = true;
                cb_bot.Enabled = true;
                cb_user.Enabled = true;
                var (tokens, duration) = LLMEngine.History.GetCurrentChatSessionInfo();
                statusbar.Items[0].Text = $"Current Session: {duration.TotalDays:F2} days ({tokens} tokens)";
            }
            else if (LLMEngine.Status == SystemStatus.Busy)
            {
                bt_delete.Enabled = false;
                bt_connect.Enabled = false;
                bt_send.Enabled = true;
                bt_send.Text = "Cancel";
                bt_send.BackColor = Color.OrangeRed;
                bt_reroll.Enabled = false;
                bt_newsession.Enabled = false;
                bt_impersonate.Enabled = false;
                cb_bot.Enabled = false;
                cb_user.Enabled = false;
            }
            else if (LLMEngine.Status == SystemStatus.NotInit)
            {
                bt_delete.Enabled = false;
                bt_connect.Enabled = false;
                bt_send.Enabled = false;
                bt_send.Text = "Offline";
                bt_send.BackColor = Color.OrangeRed;
                bt_reroll.Enabled = false;
                bt_newsession.Enabled = false;
                bt_impersonate.Enabled = false;
                cb_bot.Enabled = true;
                cb_user.Enabled = true;
            }
            if (Bot?.AllowedSamplers.Count > 0)
            {
                ck_charsampler.Enabled = true;
            }
            else
            {
                ck_charsampler.Enabled = false;
                ck_charsampler.Checked = false;
            }
        }

        /// <summary>
        /// Close the program gracefully
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AutoTalkTimer.Stop();
            SaveSettings();
            LLMEngine.Bot.EndChat(backup: true);
            if (!string.IsNullOrEmpty(LLMEngine.Bot.UniqueName))
                (LLMEngine.Bot as IFile).SaveToFile("data/chars/" + LLMEngine.Bot.UniqueName + ".json");
        }

        /// <summary>
        /// Handles bot/character switch 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void cb_bot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_bot.SelectedItem is string key && !string.IsNullOrEmpty(key))
            {
                LLMEngine.Bot = DataFiles.Characters[key];
                await LoadHistoryToUI();
                ck_senseoftime.Checked = LLMEngine.Bot.SenseOfTime;
                ck_caninitchat.Checked = Bot?.CanInitiateChat ?? false;
                var searchplug = LLMEngine.ContextPlugins.Find(x => x.PluginID == "WebSearch");
                if (searchplug != null)
                {
                    ck_onlinerag.Checked = searchplug.Enabled;
                    ck_onlinerag.Enabled = true;
                }
                else
                {
                    ck_onlinerag.Enabled = false;
                    ck_onlinerag.Checked = false;
                }
                _activityTimer?.Reset();
                UpdateUIState();
            }
        }


        #region *** Event Handlers ***

        private void OnFullPromptReady(object? sender, string e)
        {
            Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                var text = "====== New Generation ======\n\n" + e + "\n\n";
                ed_log = text.ToWinFormat();
            });
        }

        private async void OnStreamMessageReceived(object? sender, string e)
        {
            if (!_impersonatemode && !string.IsNullOrEmpty(LLMEngine.Instruct.ThinkingStart) && _currentgencalls == 1)
            {
                var thoughts = ChatRender.GetMessagePrefix(AuthorRole.Assistant) + $"*{LLMEngine.Bot.UniqueName} is thinking...*";
                await WebEditLastMessage(thoughts);
            }

            _currentgeneration += e;
            _currentgencalls++;
            _currentgenerationtokencount++;
            _responselength = DateTime.Now - _postdate;
            _activityTimer?.Reset();
            if (_currentgenerationtokencount > 1)
            {
                _currentgenerationtokencount = 0;
                if (!_impersonatemode)
                {
                    Invoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        statusbar.Items[1].Text = $"Generation: {_responselength.TotalSeconds:F2}s";
                    });
                    if (!string.IsNullOrWhiteSpace(LLMEngine.Instruct.ThinkingStart) && !string.IsNullOrEmpty(LLMEngine.Instruct.ThinkingEnd))
                    {
                        // Check if we have more than a single ThinkingEnd block, if so, we need to end the generation
                        var endcount = _currentgeneration.CountSubstring(LLMEngine.Instruct.ThinkingEnd);
                        if (endcount > 1)
                        {
                            LLMEngine.CancelGeneration();
                            return;
                        }

                    }
                    var MsgPrefix = ChatRender.GetMessagePrefix(AuthorRole.Assistant);
                    var stringfix = _currentgeneration.FixRoleplayString(Program.Settings.RoleplayFormatting, true);
                    await WebEditLastMessage(MsgPrefix + stringfix);
                }
                else
                {
                    Invoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        statusbar.Items[1].Text = $"Generation: {_responselength.TotalSeconds:F2}s";
                        ed_input.Text = _currentgeneration;
                    });
                }
            }
        }

        private async void OnStreamInferenceEnded(object? sender, string e)
        {
            _responselength = DateTime.Now - _postdate;
            _activityTimer?.Reset();
            // add time to the log
            if (_impersonatemode)
            {
                _impersonatemode = false;
                Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    ed_input.Text = e.ToWinFormat();
                    statusbar.Items[1].Text = $"Generation: {_responselength.TotalSeconds:F2}s";
                });
                LLMEngine.InvalidatePromptCache();
            }
            else
            {
                var stringfix = Program.Settings.AsteriskCheck ? e.FixAsterisks() : e;
                if (!string.IsNullOrWhiteSpace(LLMEngine.Instruct.ThinkingEnd) && stringfix.CountSubstring(LLMEngine.Instruct.ThinkingEnd) > 1)
                {
                    stringfix = stringfix.RemoveEverythingAfterLast(LLMEngine.Instruct.ThinkingEnd);
                }

                if (Program.Settings.RemoveCutSentence)
                    stringfix = stringfix.RemoveUnfinishedSentence();
                if (Program.Settings.AntiSlop)
                    stringfix = stringfix.RemoveSlop(Program.Settings.AntiSlopList, Program.Settings.AntiSlopRatio);
                // Roleplay filter
                stringfix = stringfix.FixRoleplayString(Program.Settings.RoleplayFormatting, false);

                var MsgPrefix = ChatRender.GetMessagePrefix(AuthorRole.Assistant);

                var msg = LLMEngine.Bot.History.LogMessage(AuthorRole.Assistant, stringfix, LLMEngine.User, LLMEngine.Bot);
                await Task.Delay(50);
                await WebEditLastMessage(MsgPrefix + stringfix, msg.Guid);
                PrepareResponse();
                if (_forcereload || Program.Settings.MaxMessagesOnScreen <= LLMEngine.History.CurrentSession.Messages.Count)
                {
                    Invoke((System.Windows.Forms.MethodInvoker)async delegate
                    {
                        await WebChatLoad();
                    });
                }
                Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    statusbar.Items[1].Text = $"Generation: {_responselength.TotalSeconds:F2}s";
                });
                if (Program.Settings.UseTTS && !string.IsNullOrEmpty(Bot?.TTSVoice) && LLMEngine.Client?.SupportsTTS == true)
                {
                    await OutputTTS(stringfix);
                }
            }
            (LLMEngine.Bot as Character)?.SaveChatHistory();
        }

        private async void OnBotInitiateConversation(object? sender, EventArgs e)
        {
            if (LLMEngine.Status != SystemStatus.Ready || Bot?.CanInitiateChat != true || _afkmessagecount > 2)
                return;
            _activityTimer?.Reset();
            _impersonatemode = false;
            _postdate = DateTime.Now;
            var lastusermessage = LLMEngine.History.CurrentSession.Messages.LastOrDefault(m => m.Role == AuthorRole.User);
            if (lastusermessage == null)
                return;
            var message = "The last message from {{user}} was posted " + AIToolkit.StringExtensions.TimeSpanToHumanString(DateTime.Now - lastusermessage.Date) + " ago. We're {{day}}, the {{date}} at {{time}} now. Would you like to send a message to {{user}} now? Use your best judgement based on the conversation above. In case you don't want to send a message, just respond with No. If you want to send a message, write the message to {{user}} directly while making sure it's contextually relevant. \n\nThis query will repeat every few minutes.";
            if (_afkmessagecount > 1)
                message += " You've already sent " + _afkmessagecount + " unanswered messages in a row.";
            else if (_afkmessagecount == 1)
                message += " You've already sent a message.";
            message = LLMEngine.ReplaceMacros(message);
            statusbar.Items[1].Text = "Analyzing...";
            var response = await LLMEngine.QuickInferenceForSystemPrompt(message, false);
            response = response.RemoveThinkingBlocks(LLMEngine.Instruct.ThinkingStart, LLMEngine.Instruct.ThinkingEnd).Trim();

            if (!string.IsNullOrEmpty(response) && !response.StartsWith("no", StringComparison.InvariantCultureIgnoreCase))
            {
                var msg = new SingleMessage(AuthorRole.Assistant, DateTime.Now, response, LLMEngine.Bot.UniqueName, LLMEngine.User.UniqueName);
                Bot.History.LogMessage(msg);
                _afkmessagecount++;
                await SendMessageToUI(msg);
                // play a notification sound
                System.Media.SystemSounds.Question.Play();
            }
        }

        private void OnStatusChanged(object? sender, SystemStatus e)
        {
            Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                UpdateUIState();
            });
        }

        #endregion


        #region *** Main Chat Functions ***

        private void Ed_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            // never triggered on Enter
            if (e.KeyChar == (char)13)
            {
                // never called
                e.Handled = true;
                if (ModifierKeys == Keys.Shift)
                {
                    int caretPosition = ed_input.SelectionStart;
                    ed_input.Text = ed_input.Text.Insert(caretPosition, Environment.NewLine);
                    ed_input.SelectionStart = caretPosition + Environment.NewLine.Length;
                    //ed_input.Text += Environment.NewLine;
                }
                else
                    SendMessage(sender, e);
            }
        }

        private void PrepareResponse()
        {
            _currentgeneration = string.Empty;
            _currentgenerationtokencount = 0;
            _currentgencalls = 0;
        }

        private async void Impersonate(object sender, EventArgs e)
        {
            ForceCloseEditMenu();
            _activityTimer?.Reset();
            if (LLMEngine.Status == SystemStatus.Busy)
                return;
            statusbar.Items[1].Text = "Analyzing...";
            _postdate = DateTime.Now;
            _impersonatemode = true;
            PrepareResponse();
            ed_input.Text = string.Empty;
            await LLMEngine.ImpersonateUser();
        }

        private (SingleMessage? response, bool usercmdonly) ProcessSlashCommands(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return (null, false);

            var workstring = input.Trim();
            // with input a multi-line string, we want to check if any of the lines starts with a command "/" character and if so, remove this particular line from workstring (set it aside for processing)
            var lines = workstring.Split(["\n"], StringSplitOptions.RemoveEmptyEntries);
            var commands = new List<string>();
            foreach (var line in lines)
            {
                if (line.StartsWith('/'))
                {
                    commands.Add(line);
                }
            }
            var foundacommand = false;
            StringBuilder sb = new();
            foreach (var cmd in commands)
            {
                var result = Bot!.MyPoints.ProcessCommand(cmd);
                if (!string.IsNullOrEmpty(result))
                {
                    foundacommand = true;
                    sb.AppendLinuxLine(result);
                }
            }
            if (!foundacommand)
                return (null, false);

            var response = sb.ToString().CleanupAndTrim();

            // check if the user sent only commands or not
            var usercmdonly = commands.Count == lines.Length;

            if (!string.IsNullOrEmpty(response))
            {
                return (new SingleMessage(AuthorRole.System, DateTime.Now, LLMEngine.ReplaceMacros(response), LLMEngine.Bot.UniqueName, LLMEngine.User.UniqueName), usercmdonly);
            }
            return (null, usercmdonly);
        }

        private async void SendMessage(object sender, EventArgs e)
        {
            LLMEngine.Bot.AgentSystem?.NotifyUserActivity();
            ForceCloseEditMenu();
            _activityTimer?.Reset();
            _afkmessagecount = 0;
            if (LLMEngine.Status == SystemStatus.Busy)
            {
                LLMEngine.CancelGeneration();
                return;
            }
            _impersonatemode = false;
            _postdate = DateTime.Now;
            statusbar.Items[1].Text = "Analyzing...";
            UseCharacterDefinedSampler();
            if (!string.IsNullOrEmpty(ed_input.Text))
            {
                var msgtxt = ed_input.Text.ToLinuxFormat();
                msgtxt = LLMEngine.ReplaceMacros(msgtxt, LLMEngine.User, LLMEngine.Bot);
                var msg = new SingleMessage(AuthorRole.User, DateTime.Now, msgtxt, LLMEngine.Bot.UniqueName, LLMEngine.User.UniqueName);

                if (ed_input.Text.StartsWith("/sys "))
                {
                    msg.Role = AuthorRole.System;
                    // remove the /sys prefix
                    msg.Message = msg.Message[5..].Trim();
                    await SendMessageToUI(msg);
                    // ready a new message for the bot's response
                    PrepareResponse();
                    await SendMessageToUI(
                        new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMEngine.Bot.UniqueName + " is reading your message...*", LLMEngine.Bot.UniqueName, LLMEngine.User.UniqueName));
                    ed_input.Text = string.Empty;
                    await LLMEngine.SendMessageToBot(msg);
                }
                else if (ed_input.Text.StartsWith("/game "))
                {
                    // remove the /sys prefix
                    var msgpath = msg.Message[6..].Trim();
                    _renpyDialogHandler = new RenPyDialogHandler(msgpath, "Slay The Princess");
                    var message = new SingleMessage(AuthorRole.System, DateTime.Now, "*Game Loaded: Slay The Princess*", LLMEngine.Bot.UniqueName, LLMEngine.User.UniqueName, false);
                    await SendMessageToUI(message);
                    LLMEngine.Bot.History.LogMessage(message);
                    ed_input.Text = string.Empty;
                }
                else if (ed_input.Text.StartsWith("/continue") && _renpyDialogHandler != null)
                {
                    var gameinfo = _renpyDialogHandler.Continue();
                    // check if there's something after "/continue" in ed_input.Text and if there is, store in variable
                    var extra = string.Empty;
                    if (ed_input.Text.Length > 9)
                    {
                        extra = ed_input.Text[10..].Trim();
                    }

                    msg.Role = AuthorRole.User;
                    // remove the /sys prefix
                    if (!string.IsNullOrEmpty(extra))
                    {
                        msg.Message = $"**{User?.Name ?? "User"}'s Comment**" + LLMEngine.NewLine + extra + LLMEngine.NewLine + LLMEngine.NewLine + gameinfo.ShowFullScreen();
                    }
                    else
                    {
                        msg.Message = gameinfo.ShowFullScreen();
                    }
                    await SendMessageToUI(msg);
                    // ready a new message for the bot's response
                    PrepareResponse();
                    await SendMessageToUI(
                        new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMEngine.Bot.UniqueName + " is reading your message...*", LLMEngine.Bot.UniqueName, LLMEngine.User.UniqueName));
                    ed_input.Text = string.Empty;
                    await LLMEngine.SendMessageToBot(msg);
                }
                else if (ed_input.Text.StartsWith("/pick ") && _renpyDialogHandler != null)
                {
                    var select = msg.Message[6..].Trim();
                    var id = int.TryParse(select, out var test) ? test : 0;
                    var gameinfo = _renpyDialogHandler.MakeChoice(id);

                    msg.Role = AuthorRole.System;
                    // remove the /sys prefix
                    msg.Message = gameinfo;
                    await SendMessageToUI(msg);
                    // ready a new message for the bot's response
                    PrepareResponse();
                    await SendMessageToUI(
                        new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMEngine.Bot.UniqueName + " is reading your message...*", LLMEngine.Bot.UniqueName, LLMEngine.User.UniqueName));
                    ed_input.Text = string.Empty;
                    await LLMEngine.SendMessageToBot(msg);
                }
                else if (ed_input.Text.StartsWith("/dialogs") && _renpyDialogHandler != null)
                {
                    var gameinfo = _renpyDialogHandler.Continue();
                    msg.Role = AuthorRole.System;
                    // remove the /sys prefix
                    msg.Message = gameinfo.ShowDialogs();
                    await SendMessageToUI(msg);
                    // ready a new message for the bot's response
                    PrepareResponse();
                    await SendMessageToUI(
                        new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMEngine.Bot.UniqueName + " is reading your message...*", LLMEngine.Bot.UniqueName, LLMEngine.User.UniqueName));
                    ed_input.Text = string.Empty;
                    await LLMEngine.SendMessageToBot(msg);
                }
                else
                {
                    var (response, usercmdonly) = ProcessSlashCommands(msgtxt);
                    if (response != null)
                    {
                        if (usercmdonly)
                        {
                            LLMEngine.History.LogMessage(response);
                            await SendMessageToUI(response);
                            ed_input.Text = string.Empty;
                            statusbar.Items[1].Text = "Ready!";
                            return;
                        }
                        else
                        {
                            LLMEngine.History.LogMessage(msg);
                            await SendMessageToUI(msg);
                            await SendMessageToUI(response);
                            PrepareResponse();
                            await SendMessageToUI(
                                new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMEngine.Bot.UniqueName + " is reading your message...*", LLMEngine.Bot.UniqueName, LLMEngine.User.UniqueName));
                            ed_input.Text = string.Empty;
                            await LLMEngine.SendMessageToBot(response);
                        }
                    }
                    else
                    {
                        await SendMessageToUI(msg);
                        // ready a new message for the bot's response
                        PrepareResponse();
                        await SendMessageToUI(
                            new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMEngine.Bot.UniqueName + " is reading your message...*", LLMEngine.Bot.UniqueName, LLMEngine.User.UniqueName));
                        ed_input.Text = string.Empty;
                        await LLMEngine.SendMessageToBot(msg);
                    }
                }
            }
            else
            {
                // ready a new message for the bot's response
                PrepareResponse();
                await SendMessageToUI(
                    new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMEngine.Bot.UniqueName + " is reading your message...*", LLMEngine.Bot.UniqueName, LLMEngine.User.UniqueName));
                ed_input.Text = string.Empty;
                await LLMEngine.AddBotMessage();
            }

        }

        private void ForceCloseEditMenu()
        {
            if (_editMessageForm != null && !_editMessageForm.IsDisposed)
            {
                if (_editMessageForm.InvokeRequired)
                {
                    _editMessageForm.Invoke(new Action(() =>
                    {
                        _editMessageForm.Close();
                        _editMessageForm.Dispose();
                    }));
                }
                else
                {
                    _editMessageForm.Close();
                    _editMessageForm.Dispose();
                }
            }
            _editMessageForm = null;
        }

        private async void RerollMessage(object sender, EventArgs e)
        {
            ForceCloseEditMenu();
            _afkmessagecount = 0;
            if (LLMEngine.Status == SystemStatus.Busy || LLMEngine.History.CurrentSession.Messages.Count == 0 || LLMEngine.History.LastMessage()?.Role != AuthorRole.Assistant)
                return;
            _activityTimer?.Reset();
            _impersonatemode = false;
            _postdate = DateTime.Now;
            statusbar.Items[1].Text = "Analyzing...";
            UseCharacterDefinedSampler();
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
            await WebRemoveLastMessage();
            await SendMessageToUI(new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMEngine.Bot.UniqueName + " is reading your message...*", LLMEngine.Bot.UniqueName, LLMEngine.User.UniqueName));
            PrepareResponse();
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
            await LLMEngine.RerollLastMessage();
        }

        private async void Connect(object sender, EventArgs e)
        {
            await LLMEngine.Connect();
            num_maxcontext.Maximum = LLMEngine.MaxContextLength;
            num_maxcontext.Value = LLMEngine.MaxContextLength;
            grp_model.Text = LLMEngine.CurrentModel;
            ck_ttstoggle.Enabled = LLMEngine.SupportsTTS;
            ck_onlinerag.Enabled = LLMEngine.SupportsWebSearch;
            boxVLM.Enabled = LLMEngine.SupportsVision;
            LLMEngine.Bot.AgentSystem?.NotifyUserActivity();
        }

        private async void StartNewSession(object sender, EventArgs e)
        {
            // Check if we're in a past sessions, if so, ask if the user wants to update the archive before going back to the current session
            if (LLMEngine.History.CurrentSessionID != -1 && LLMEngine.History.CurrentSessionID != LLMEngine.History.Sessions.Count - 1)
            {
                await UpdateOldSession();
            }
            else if (MessageBox.Show("This will archive the current chat and start a new one.", "Confirm?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                await UpdateLatestSession();
            }
        }

        private async Task UpdateLatestSession()
        {
            this.Enabled = false;
            using var loadingForm = new LoadingForm() { Owner = this, StartPosition = FormStartPosition.Manual };
            loadingForm.CenterToParent();
            loadingForm.Show();
            loadingForm.BringToFront();
            loadingForm.Refresh();
            try
            {
                loadingForm.SetMessage("Archiving and summarizing current session. Depending on your computer, the model, and the context size, it might take a while.");
                loadingForm.SetProgress(5);

                LLMEngine.OnQuickInferenceEnded += (s, e) =>
                {
                    loadingForm.AddProgress(50);
                };
                await LLMEngine.History.StartNewChatSession(true, false);
                if (LLMEngine.Bot.SelfEditTokens > 0)
                {
                    loadingForm.SetMessage("Updating dynamic character (this might take a few minutes).");
                }
                else
                {
                    loadingForm.SetMessage("Saving history.");
                    loadingForm.SetProgress(95);
                }
                (LLMEngine.Bot as Character)?.SaveChatHistory();
                await LLMEngine.Bot.UpdateSelfEditSection();
                if (!string.IsNullOrEmpty(LLMEngine.Bot.UniqueName))
                    (LLMEngine.Bot as IFile).SaveToFile("data/chars/" + LLMEngine.Bot.UniqueName + ".json");
                loadingForm.SetMessage("Loading new session.");
                loadingForm.SetProgress(100);
                LLMEngine.RemoveQuickInferenceEventHandler();
                await WebChatLoad();
                _afkmessagecount = 0;
                _activityTimer?.Reset();
            }
            finally
            {
                loadingForm.Close();
                this.Enabled = true;
            }

        }

        private async Task UpdateOldSession()
        {
            this.Enabled = false;
            var doupdate = false;

            if (MessageBox.Show("Do you want to update this session's summary before going back to the latest session?", "Refresh?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                doupdate = true;
            }
            using var loadingForm = new LoadingForm() { Owner = this, StartPosition = FormStartPosition.Manual };
            loadingForm.CenterToParent();
            loadingForm.Show();
            loadingForm.BringToFront();
            loadingForm.Refresh();
            try
            {
                if (doupdate)
                {
                    loadingForm.SetMessage("Archiving and summarizing session. Depending on your computer, the model, and the context size, it might take a while.");
                    loadingForm.SetProgress(5);
                    LLMEngine.OnQuickInferenceEnded += (s, e) =>
                    {
                        loadingForm.AddProgress(20);
                    };
                    await LLMEngine.History.CurrentSession.UpdateSession();
                    LLMEngine.RemoveQuickInferenceEventHandler();
                }
                loadingForm.SetMessage("Loading current session.");
                loadingForm.SetProgress(95);
                LLMEngine.History.CurrentSessionID = -1;
                (LLMEngine.Bot as Character)?.SaveChatHistory();
                await WebChatLoad();
                _afkmessagecount = 0;
                _activityTimer?.Reset();
            }
            finally
            {
                loadingForm.Close();
                this.Enabled = true;
            }
        }

        private async void DeleteLastMessage(object sender, EventArgs e)
        {
            _impersonatemode = false;
            if (LLMEngine.Status == SystemStatus.Busy || LLMEngine.History.CurrentSession.Messages.Count == 0)
                return;

            var msgs = LLMEngine.History.CurrentSession.Messages;

            // Remove any trailing hidden messages (not shown in UI)
            if (!Program.Settings.ShowHiddenMessages)
                while (msgs.Count > 0 && msgs[^1].Hidden)
                    LLMEngine.History.RemoveLast();

            // Now remove the last visible message (shown in UI), if any remain
            var removedVisible = false;
            if (msgs.Count > 0)
            {
                LLMEngine.History.RemoveLast();
                removedVisible = true;
            }

            // Now remove any trailing hidden messages again
            if (!Program.Settings.ShowHiddenMessages)
                while (msgs.Count > 0 && msgs[^1].Hidden)
                    LLMEngine.History.RemoveLast();

            LLMEngine.InvalidatePromptCache();

            if (removedVisible)
                await WebRemoveLastMessage();
        }

        private async Task LoadHistoryToUI()
        {
            await WebChatLoad();
        }

        private async Task SendMessageToUI(SingleMessage singleMessage)
        {
            string img = "gears.png";
            switch (singleMessage.Role)
            {
                case AuthorRole.User:
                    img = User?.Icon ?? "gears.png";
                    break;
                case AuthorRole.Assistant:
                    img = Bot?.Icon ?? "gears.png";
                    break;
            }
            var text = Markdown.ToHtml(ChatRender.GetMessagePrefix(singleMessage) + singleMessage.Message, CustomMarkDownPipeline);
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

        private void UseCharacterDefinedSampler()
        {
            if (!ck_charsampler.Checked || !ck_charsampler.Enabled || Bot == null)
                return;
            // make a list of samplers, looking at DataFiles.Inference and what samplers are allowed in the Character's Program.Settings
            var samplers = new List<string>();
            foreach (var item in DataFiles.Inference)
            {
                if (Bot.AllowedSamplers.Contains(item.Key))
                    samplers.Add(item.Key);
            }
            // pick random on from list and change the cb_infer selection to it
            if (samplers.Count > 0)
            {
                var idx = RNG.Next(0, samplers.Count);
                cb_infer.SelectedIndex = cb_infer.Items.IndexOf(samplers[idx]);
            }
        }

        #endregion


        #region *** Settings Tab Functions ***

        private void LoadSettings()
        {
            if (!File.Exists("settings.json"))
            {
                Program.Settings = new WaifuSettings();
                File.WriteAllText("settings.json", JsonConvert.SerializeObject(Program.Settings, Formatting.Indented));
            }

            var str = File.ReadAllText("settings.json");
            Program.Settings = JsonConvert.DeserializeObject<WaifuSettings>(str)!;
            LLMEngine.Settings = Program.Settings;
            var saveinit = _isinitloading;
            _isinitloading = true;
            LLMEngine.MaxContextLength = Program.Settings.MaxTotalTokens;

            // set cb_user to the Program.Settings.UserFile value if it's in the list, otherwise set index to 0.
            cb_user.SelectedIndex = cb_user.Items.Contains(Program.Settings.UserFile) ? cb_user.Items.IndexOf(Program.Settings.UserFile) : 0;
            // set cb_infer to the Program.Settings.InferenceFile value if it's in the list, otherwise set index to 0.
            cb_infer.SelectedIndex = cb_infer.Items.Contains(Program.Settings.SamplerFile) ? cb_infer.Items.IndexOf(Program.Settings.SamplerFile) : 0;
            // set cb_instruct to the Program.Settings.InstructFile value if it's in the list, otherwise set index to 0.
            cb_instruct.SelectedIndex = cb_instruct.Items.Contains(Program.Settings.Instruct) ? cb_instruct.Items.IndexOf(Program.Settings.Instruct) : 0;
            // set cb_bot to the Program.Settings.BotFile value if it's in the list, otherwise set index to 0.
            cb_bot.SelectedIndex = cb_bot.Items.Contains(Program.Settings.BotFile) ? cb_bot.Items.IndexOf(Program.Settings.BotFile) : 0;
            // set cb_sysprompt to the Program.Settings.PromptFile value if it's in the list, otherwise set index to 0.
            cb_sysprompt.SelectedIndex = cb_sysprompt.Items.Contains(Program.Settings.PromptFile) ? cb_sysprompt.Items.IndexOf(Program.Settings.PromptFile) : 0;
            num_maxcontext.Maximum = Program.Settings.MaxTotalTokens;
            num_maxcontext.Value = Program.Settings.MaxTotalTokens;
            num_maxresponse.Value = Program.Settings.MaxReplyLength;
            num_temperature.Value = (decimal)Program.Settings.Temperature;
            ck_sessionmemory.Checked = LLMEngine.Settings.SessionMemorySystem;
            ck_ttstoggle.Checked = Program.Settings.UseTTS;
            ck_disablethink.Checked = Program.Settings.DisableThinking;
            ck_ragtothink.Checked = Program.Settings.RAGMoveToThinkBlock;
            ck_agentmode.Checked = LLMEngine.Bot.AgentMode;

            if (LLMEngine.ContextPlugins.Find(e => e is BrowsePlugin) is BrowsePlugin webplug)
            {
                webplug.EnforceCorrectGrammar = Program.Settings.WebsitePluginGrammar;
                webplug.KeywordDetection = Program.Settings.WebsitePluginUseKeywords;
            }
            _isinitloading = saveinit;
        }

        private void SaveSettings()
        {
            try
            {
                Program.Settings.BotFile = cb_bot.SelectedItem?.ToString() ?? string.Empty;
                Program.Settings.UserFile = cb_user.SelectedItem?.ToString() ?? string.Empty;
                Program.Settings.SamplerFile = cb_infer.SelectedItem?.ToString() ?? string.Empty;
                Program.Settings.Instruct = cb_instruct.SelectedItem?.ToString() ?? string.Empty;
                Program.Settings.PromptFile = cb_sysprompt.SelectedItem?.ToString() ?? string.Empty;
                Program.Settings.Temperature = (double)num_temperature.Value;
                Program.Settings.UseTTS = ck_ttstoggle.Checked;
                LLMEngine.Bot.AgentMode = ck_agentmode.Checked;
                Program.Settings.SessionMemorySystem = ck_sessionmemory.Checked;

                if (LLMEngine.ContextPlugins.Find(e => e is BrowsePlugin) is BrowsePlugin webplug)
                {
                    webplug.EnforceCorrectGrammar = Program.Settings.WebsitePluginGrammar;
                    webplug.KeywordDetection = Program.Settings.WebsitePluginUseKeywords;
                }

                var str = JsonConvert.SerializeObject(Program.Settings, Formatting.Indented);
                File.WriteAllText("settings.json", str);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving Program.Settings: {ex.Message}");
            }
        }

        private void ck_ragenabled_CheckedChanged(object sender, EventArgs e)
        {
            RAGEngine.Enabled = ck_ragenabled.Checked;
        }

        #endregion


        #region *** WebView2 Handling ***

        private string InjectDialogCSS(string htmlContent)
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

        private static string InjectDialogHtml(string imgPath, string dialog, Guid messageGuid)
        {
            // Replace thinking tags with collapsible div structure, using the instruction format's tags
            var processedDialog = dialog;
            return $@"
                <div class='chat-message' data-message-guid='{messageGuid}'>
                    <div class='portrait'>
                        <img src='https://appassets.test/img/{imgPath}' alt='Portrait' width='60'>
                    </div>
                    <div class='message-content'>
                        <div class='message-raw'>
                            {processedDialog}
                        </div>
                    </div>
                </div>";
        }

        private static string AddHtmlMessage(SingleMessage singleMessage)
        {
            string img = "gears.png";
            switch (singleMessage.Role)
            {
                case AuthorRole.User:
                    img = (singleMessage.User as Character)!.Icon;
                    break;
                case AuthorRole.Assistant:
                    img = (singleMessage.Bot as Character)!.Icon;
                    break;
            }
            var html = Markdown.ToHtml(ChatRender.GetMessagePrefix(singleMessage) + singleMessage.Message, CustomMarkDownPipeline);
            return InjectDialogHtml(img, html, singleMessage.Guid);
        }

        private async Task WebRemoveLastMessage()
        {
            if (InvokeRequired)
            {
                await InvokeAsync(new Func<Task>(WebRemoveLastMessage));
                return;
            }
            await web_chat.CoreWebView2.ExecuteScriptAsync(@"
                (function(){
                    const msgs = document.getElementsByClassName('chat-message');
                    if (msgs.length > 0) { msgs[msgs.length - 1].remove(); }
                })();");
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
        }

        private async Task WebEditLastMessage(string newMessage, Guid? messageGuid = null)
        {
            if (InvokeRequired)
            {
                await InvokeAsync(new Func<Task>(async () => await WebEditLastMessage(newMessage, messageGuid)));
                return;
            }

            if (!string.IsNullOrEmpty(LLMEngine.Instruct.ThinkingStart) &&
                newMessage.StartsWith(ChatRender.GetMessagePrefix(AuthorRole.Assistant)) &&
                newMessage.Contains(LLMEngine.Instruct.ThinkingStart))
            {
                // remove prefix from message
                var worktext = newMessage[ChatRender.GetMessagePrefix(AuthorRole.Assistant).Length..];
                if (!worktext.Contains(LLMEngine.Instruct.ThinkingEnd))
                {
                    worktext = worktext.Replace(LLMEngine.Instruct.ThinkingStart, string.Empty);
                    var text = Markdown.ToHtml(worktext, CustomMarkDownPipeline);
                    text = text.SanitizeForJS();
                    var script = $"updateMessageAtIndex(\"{text}\", document.getElementsByClassName('message-content').length - 1, true);";
                    await web_chat.CoreWebView2.ExecuteScriptAsync(script);
                }
                else
                {
                    // both tokens are found, so we want two strings now: the first one is the thinking part, the second one is the message part
                    var parts = worktext.Split([LLMEngine.Instruct.ThinkingEnd], 2, StringSplitOptions.None);
                    var thinkingText = parts[0].Replace(LLMEngine.Instruct.ThinkingStart, string.Empty);
                    thinkingText = Markdown.ToHtml(thinkingText, CustomMarkDownPipeline).SanitizeForJS();
                    var script = $"updateMessageAtIndex(\"{thinkingText}\", document.getElementsByClassName('message-content').length - 1, true);";
                    await web_chat.CoreWebView2.ExecuteScriptAsync(script);

                    var msgoutput = ChatRender.GetMessagePrefix(AuthorRole.Assistant) + parts[1].TrimStart().TrimStart('\n').TrimStart();
                    var messageText = Markdown.ToHtml(msgoutput, CustomMarkDownPipeline).SanitizeForJS();
                    script = $"updateMessageAtIndex(\"{messageText}\", document.getElementsByClassName('message-content').length - 1, false);";
                    await web_chat.CoreWebView2.ExecuteScriptAsync(script);
                }
            }
            else
            {
                var text = Markdown.ToHtml(newMessage, CustomMarkDownPipeline);
                text = text.SanitizeForJS();
                var script = $"updateMessageAtIndex(\"{text}\", document.getElementsByClassName('message-content').length - 1, false);";
                await web_chat.CoreWebView2.ExecuteScriptAsync(script);
            }

            // Update the data-message-guid attribute if a GUID is provided
            if (messageGuid.HasValue)
            {
                var updateGuidScript = $@"
                    const chatMessages = document.querySelectorAll('.chat-message');
                    if (chatMessages.length > 0) {{
                        const lastMessage = chatMessages[chatMessages.length - 1];
                        lastMessage.setAttribute('data-message-guid', '{messageGuid.Value}');
                        console.log('Updated GUID for last message to: {messageGuid.Value}');
                    }} else {{
                        console.error('No chat messages found to update GUID');
                    }}";
                await web_chat.CoreWebView2.ExecuteScriptAsync(updateGuidScript);
            }


            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
        }

        public async void ForceUpdateLastMessage(string update)
        {
            await WebEditLastMessage(update);
        }

        private async Task WebChatLoad()
        {
            if (web_chat.CoreWebView2 == null)
            {
                await web_chat.EnsureCoreWebView2Async();
                web_chat.CoreWebView2!.Settings.AreDevToolsEnabled = false;
                web_chat.CoreWebView2!.Settings.AreDefaultContextMenusEnabled = false;
                web_chat.CoreWebView2.SetVirtualHostNameToFolderMapping("appassets.test", AppContext.BaseDirectory + "data\\", CoreWebView2HostResourceAccessKind.Allow);
                web_chat.CoreWebView2.DOMContentLoaded += OnWebChatContentLoaded!; // Add event handler
                web_chat.CoreWebView2.WebMessageReceived += OnWebChatWebMessageReceived!;
                web_chat.CoreWebView2.NewWindowRequested += OnNewWindowRequested;
                web_chat.CoreWebView2.NavigationStarting += OnNavigationStarting;
            }
            var html = string.Empty;
            _forcereload = false;
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
                BeginInvoke(new Action(() =>
                {
                    try
                    {
                        if (!IsDisposed && IsHandleCreated)
                        {
                            EditMessage(messageGuid);
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

        private void EditMessage(Guid messageGuid)
        {
            if (LLMEngine.Status == SystemStatus.Busy)
                return;

            this.Enabled = false;
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
                    Invoke((System.Windows.Forms.MethodInvoker)async delegate
                    {
                        await LoadHistoryToUI();
                        LLMEngine.InvalidatePromptCache();
                    });
                }
            }
            finally
            {
                this.Enabled = true;
            }
        }

        #endregion


        #region *** Audio and TTS ***

        private async Task OutputTTS(string text)
        {
            var paragraphs = text.Split(["\n\n"], StringSplitOptions.RemoveEmptyEntries);

            if (paragraphs.Length == 0)
                return;

            var voiceID = Bot?.TTSVoice ?? "Waifu";
            int index = 0;

            // Start generating TTS for the first paragraph
            var currentWaveTask = LLMEngine.GenerateTTS(paragraphs[index], voiceID);
            index++;

            while (Program.Settings.UseTTS && LLMEngine.Status != SystemStatus.Busy)
            {
                // Wait for the current TTS generation to complete
                var currentWave = await currentWaveTask;

                // Start playing the current audio chunk in a background task
                var playTask = PlayAudioAsync(currentWave);

                // Generate TTS for the next paragraph while the current one is playing
                Task<byte[]>? nextWaveTask = null;
                if (index < paragraphs.Length)
                {
                    nextWaveTask = LLMEngine.GenerateTTS(paragraphs[index], voiceID);
                    index++;
                }

                // Wait for the current audio playback to finish
                await playTask;

                if (nextWaveTask == null)
                {
                    // No more paragraphs to process
                    break;
                }

                // Move to the next audio chunk
                currentWaveTask = nextWaveTask;
            }
        }

        private static Task PlayAudioAsync(byte[] audioData)
        {
            return Task.Run(() =>
            {
                using var audioStream = new MemoryStream(audioData);
                using var player = new SoundPlayer(audioStream);
                player.PlaySync(); // Plays the sound and waits until it completes
            });
        }

        private void ck_ttstoggle_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.UseTTS = ck_ttstoggle.Checked;
        }

        #endregion


        // === Below lies the button click hell :D Venture at your own risk ===

        private void num_maxcontext_ValueChanged(object sender, EventArgs e)
        {
            LLMEngine.MaxContextLength = (int)num_maxcontext.Value;
        }

        private void num_maxresponse_ValueChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.MaxReplyLength = (int)num_maxresponse.Value;
        }

        private void cb_user_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_user.SelectedItem is string key && !string.IsNullOrEmpty(key))
                LLMEngine.User = DataFiles.Characters[key];
        }

        private void cb_instruct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_instruct.SelectedItem is string key && !string.IsNullOrEmpty(key))
            {
                LLMEngine.Instruct = DataFiles.Instruct[key];
                ck_forceNames.Checked = LLMEngine.Instruct.AddNamesToPrompt;
            }
        }

        private void cb_infer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_infer.SelectedItem is string key && !string.IsNullOrEmpty(key))
                LLMEngine.Sampler = DataFiles.Inference[key];
            num_temperature.Value = (decimal)LLMEngine.Sampler.Temperature;
        }

        private void num_temperature_ValueChanged(object sender, EventArgs e)
        {
            LLMEngine.ForceTemperature = ((double)num_temperature.Value);
        }

        private void ck_senseoftime_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Bot.SenseOfTime = ck_senseoftime.Checked;
        }

        private void ck_sessionmemory_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.SessionMemorySystem = ck_sessionmemory.Checked;
        }

        private void bt_scenario_Click(object sender, EventArgs e)
        {
            using var editForm = new ScenarioEditForm();
            editForm.ShowDialog();
            bt_scenario.ForeColor = string.IsNullOrWhiteSpace(LLMEngine.Settings.ScenarioOverride) ? Color.Black : Color.DarkGreen;
            LLMEngine.InvalidatePromptCache();
        }

        private void ck_forceNames_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Instruct.AddNamesToPrompt = ck_forceNames.Checked;
            LLMEngine.InvalidatePromptCache();
        }

        private void ck_worldinfo_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.AllowWorldInfo = ck_worldinfo.Checked;
        }

        private void cb_sysprompt_SelectionIndexChanged(object sender, EventArgs e)
        {
            if (cb_sysprompt.SelectedItem is string key && !string.IsNullOrEmpty(key))
                LLMEngine.SystemPrompt = DataFiles.SysPrompts[key];
        }

        private void ck_caninit_CheckedChanged(object sender, EventArgs e)
        {
            if (Bot != null)
                Bot.CanInitiateChat = ck_caninitchat.Checked;
        }

        private void AutoTalkTimer_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ed_input.Text))
                LLMEngine.Bot.AgentSystem?.NotifyUserActivity();
            if (LLMEngine.Status != SystemStatus.Ready || !string.IsNullOrEmpty(ed_input.Text) || Bot?.CanInitiateChat != true)
                return;
            _activityTimer?.IsTimeout();
        }

        private void ck_onlinerag_CheckedChanged(object sender, EventArgs e)
        {
            var searchplug = LLMEngine.ContextPlugins.Find(x => x.PluginID == "WebSearch");
            if (searchplug != null)
            {
                searchplug.Enabled = ck_onlinerag.Checked;
                ck_onlinerag.Enabled = true;
            }
            else
            {
                ck_onlinerag.Enabled = false;
                ck_onlinerag.Checked = false;
            }
        }

        private void bt_editchar_Click(object sender, EventArgs e)
        {
            using var editForm = new CharEditForm();
            editForm.SetupCharacterEditor(Bot?.UniqueName ?? string.Empty);
            editForm.ShowDialog();
            LLMEngine.InvalidatePromptCache();
            var currbselection = cb_bot.Text;
            var curruselection = cb_user.Text;
            cb_bot.Items.Clear();
            cb_user.Items.Clear();
            foreach (var item in DataFiles.Characters)
            {
                if (item.Value.IsUser)
                    cb_user.Items.Add(item.Value.UniqueName);
                else
                    cb_bot.Items.Add(item.Value.UniqueName);
            }
            var newidx = cb_bot.Items.IndexOf(currbselection);
            cb_bot.SelectedIndex = newidx == -1 ? 0 : newidx;
            newidx = cb_user.Items.IndexOf(curruselection);
            cb_user.SelectedIndex = newidx == -1 ? 0 : newidx;
        }

        private void bt_clearimg_Click(object sender, EventArgs e)
        {
            LLMEngine.VLM_ClearImages();
            pictEmbed.Image = null;
        }

        private void ck_disablethink_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.DisableThinking = ck_disablethink.Checked;
        }

        private void ck_ragtothink_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.RAGMoveToThinkBlock = ck_ragtothink.Checked;
        }

        private void ck_agentmode_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Bot.AgentMode = ck_agentmode.Checked;
        }

        private void btInstructEdit_Click(object sender, EventArgs e)
        {
            using var editForm = new InstructForm();
            editForm.SetupInstructEditor(LLMEngine.Instruct.UniqueName ?? string.Empty);
            editForm.ShowDialog();
            LLMEngine.InvalidatePromptCache();
            // Update the prompt list in the chat menu
            var currselection = LLMEngine.Instruct.UniqueName;
            cb_instruct.Items.Clear();
            foreach (var item in DataFiles.Instruct)
            {
                cb_instruct.Items.Add(item.Value.UniqueName);
            }
            var newidx = cb_instruct.Items.IndexOf(currselection);
            cb_instruct.SelectedIndex = newidx == -1 ? 0 : newidx;
        }

        private void btSysPrompt_Click(object sender, EventArgs e)
        {
            using var editForm = new SysPromptForm();
            editForm.SetupPromptEditor(LLMEngine.SystemPrompt.UniqueName ?? string.Empty);
            editForm.ShowDialog();
            LLMEngine.InvalidatePromptCache();
            var currselection = cb_sysprompt.SelectedItem?.ToString() ?? "";
            cb_sysprompt.Items.Clear();
            foreach (var item in DataFiles.SysPrompts)
            {
                cb_sysprompt.Items.Add(item.Value.UniqueName);
            }
            var newidx = cb_sysprompt.Items.IndexOf(currselection);
            cb_sysprompt.SelectedIndex = newidx == -1 ? 0 : newidx;
        }

        private void btSampleEditor_Click(object sender, EventArgs e)
        {
            using var editForm = new SamplerForm();
            editForm.SetupSamplerEditor(LLMEngine.Sampler.UniqueName ?? string.Empty);
            editForm.ShowDialog();
            LLMEngine.InvalidatePromptCache();
            // Update the sampler list in the chat menu
            var currselection = cb_infer.SelectedItem?.ToString() ?? "";
            cb_infer.Items.Clear();
            foreach (var item in DataFiles.Inference)
            {
                cb_infer.Items.Add(item.Value.UniqueName);
            }
            var newidx = cb_infer.Items.IndexOf(currselection);
            cb_infer.SelectedIndex = newidx == -1 ? 0 : newidx;
        }

        private async void btMainSettings_Click(object sender, EventArgs e)
        {
            // Open settings form as modal dialog
            using var settingsForm = new SettingsForm();
            settingsForm.StartPosition = FormStartPosition.CenterParent;
            settingsForm.ShowDialog();
            LLMEngine.InvalidatePromptCache();
            await WebChatLoad();
        }

        private void btWorldEditor_Click(object sender, EventArgs e)
        {
            using var worldForm = new WorldEditForm();
            worldForm.StartPosition = FormStartPosition.CenterParent;
            worldForm.ShowDialog();
            LLMEngine.InvalidatePromptCache();
        }

        private async void btChatHistory_Click(object sender, EventArgs e)
        {
            using var worldForm = new ChatHistoryForm();
            worldForm.StartPosition = FormStartPosition.CenterParent;
            worldForm.ShowDialog();
            LLMEngine.InvalidatePromptCache();
            await WebChatLoad();
        }

        private void btVectorSearch_Click(object sender, EventArgs e)
        {
            using var ragForm = new RAGSearchForm();
            ragForm.StartPosition = FormStartPosition.CenterParent;
            ragForm.ShowDialog();
        }

        private void btRawLog_Click(object sender, EventArgs e)
        {
            // Show a basic window with the ed_log content in a textbox and a close button
            using var logForm = new RawLogForm();
            logForm.SetText(ed_log);
            logForm.StartPosition = FormStartPosition.CenterParent;
            logForm.ShowDialog();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SentimentAnalysis.Enabled = true;
            var cool = await SentimentAnalysis.Analyze(ed_input.Text);
            
            //LLMEngine.NamesInPromptOverride = false;
            //var sense = LLMEngine.Bot.DatesInSessionSummaries;
            //LLMEngine.Bot.DatesInSessionSummaries = false;
            ////var res = await SentimentAnalysis.Analyze("I am very happy today!");
            //var availtokens = LLMEngine.MaxContextLength - 1024;
            //var requestedtask = "Based on the information provided in the system prompt, write a list of goals for {{char}}. Things she would want to convince {{user}} to do, or even things she'd like to think or do for herself. Keep the list realistic.";

            //var promptbuild = LLMEngine.Client!.GetPromptBuilder();
            //var sysprompt = "You are {{char}}, and you're looking for things you'd want to convince {{user}} to do or become, but seems reluctant to try." + LLMEngine.NewLine +
            //    LLMEngine.NewLine +
            //    "## Name: {{char}}" + LLMEngine.NewLine + LLMEngine.NewLine +
            //    "{{charbio}}" + LLMEngine.NewLine + LLMEngine.NewLine +
            //    "## Name: {{user}}" + LLMEngine.NewLine + LLMEngine.NewLine +
            //    "{{userbio}}" + LLMEngine.NewLine + LLMEngine.NewLine +
            //    "## Chronological chat session summaries:" + LLMEngine.NewLine + LLMEngine.NewLine;

            //availtokens -= promptbuild.GetTokenCount(AuthorRole.SysPrompt, sysprompt);
            //availtokens -= promptbuild.GetTokenCount(AuthorRole.User, requestedtask);

            //var summaries = LLMEngine.History.GetPreviousSummaries(availtokens, allowRP: true);
            //sysprompt += summaries;
            //promptbuild.AddMessage(AuthorRole.SysPrompt, sysprompt);
            //promptbuild.AddMessage(AuthorRole.User, requestedtask);

            //var ct = promptbuild.PromptToQuery(AuthorRole.Assistant, (LLMEngine.Sampler.Temperature > 0.75) ? 0.75 : LLMEngine.Sampler.Temperature, 1000);
            //var finalstr = await LLMEngine.SimpleQuery(ct);
            //if (!string.IsNullOrWhiteSpace(LLMEngine.Instruct.ThinkingStart))
            //{
            //    finalstr = finalstr.RemoveThinkingBlocks(LLMEngine.Instruct.ThinkingStart, LLMEngine.Instruct.ThinkingEnd);
            //}
            //LLMEngine.Bot.DatesInSessionSummaries = sense;
            //LLMEngine.NamesInPromptOverride = null;
        }
    }
}
