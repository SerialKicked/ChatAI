using LetheAISharp;
using LetheAISharp.Agent;
using LetheAISharp.Agent.Actions;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Markdig;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using WaifuAI.AgentPlugins;
using WaifuAI.Controls;
using WaifuAI.Files;
using WaifuAI.Game;
using WaifuAI.Plugins;
using WaifuAI.Slash;
using WaifuAI.src.forms;

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
        private readonly Random RNG = new();
        private string ed_log = string.Empty;
        private SingleMessage? _lastUserMessageForGroupLoop;
        private bool _suppressGroupSwitchEvent = false;

        public RenPyDialogHandler? _renpyDialogHandler;
        public readonly List<ISlashCommand> slashCommands = [new MainSlashCmds(), new RenpyGameCmds()];

        public static ICharacter? Bot => LLMEngine.Bot as ICharacter;
        public static ICharacter? User => LLMEngine.User as ICharacter;

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
            this.Shown += async (_, __) =>
            {
                await InitializeWebViewAsync();
                cb_bot_SelectedIndexChanged(cb_bot, new EventArgs());
                await LoadHistoryToUI();
            };

            ed_input.SpellCheckLanguage = "en-US";

            HelptoolTip.SetToolTip(mck_ragenabled, "Use RAG functionalities to insert summaries of relevant previous sessions based on the user's input." + Environment.NewLine + "Configurable in the Program.Settings tab.");
            HelptoolTip.SetToolTip(mck_guidance, "Insert day and time information to prompt when relevant to give the bot a better understanding of time.");
            HelptoolTip.SetToolTip(mck_sessionmemory, "Use a set amount of tokens (set in Program.Settings) to insert summaries of previous chat sessions with this bot." + Environment.NewLine + "This drastically increases the bot's long-term memory.");
            HelptoolTip.SetToolTip(mck_worldinfo, "Use the WorldInfo file(s) associated with this bot. WorldInfo is a list of keyword-triggered textual information that is inserted into the prompt when the conditions are met." + Environment.NewLine + "See the World Info tab for additional information.");
            HelptoolTip.SetToolTip(mck_charsampler, "If checked, and when using a bot persona containing a list of compatible inference Program.Settings, the inference Program.Settings will be picked at random from that list each time the bot write a new message." + Environment.NewLine + Environment.NewLine + "Will lead to a more creative and less repetitive interaction, but also less consistent.");
            HelptoolTip.SetToolTip(mck_onlinerag, "If checked, the bot may perform a web search (using DuckDuckGo) to improve its responses when asked to.");

            // Load our agentic actions
            AgentRuntime.RegisterAction(new SessionMoodCheckAction());
            AgentRuntime.RegisterAction(new ImageInfoAction());
            AgentRuntime.RegisterAction(new PersonInfoAction());
            AgentRuntime.RegisterAction(new FindGroupNextAgent());
            // Load our agentic plugins
            AgentRuntime.RegisterPlugin("GoalDesignerTask", new GoalDesignerTask());
            AgentRuntime.RegisterPlugin("CustomGoalTask", new CustomGoalTask());
            AgentRuntime.RegisterPlugin("JournalTask", new JournalTask());
            AgentRuntime.RegisterPlugin("SessionGoalTask", new SessionGoalTask());
            AgentRuntime.RegisterPlugin("SarahDomTask", new SarahDomTask());
            // Manage theme
            if (Program.Settings.Skin == "Light")
                ThemeManager.ApplyLight();
            else
                ThemeManager.ApplyDark();
            // Load login form
            var loginForm = new LoginForm();
            ThemeManager.ApplyToForm(loginForm);
            loginForm.ShowDialog(this);
            if (loginForm.DialogResult != DialogResult.OK)
            {
                MessageBox.Show("No connection with backend server. You can use the application, but you cannot chat with the AI.");
            }
            // Chat related events
            SetupChatMenu();
            _activityTimer.OnTrigger += OnBotInitiateConversation;
            // Autodetection of the user's activity (mostly for the background agent functionalities)
            Application.AddMessageFilter(new ActivityMessageFilter());
            ed_input.KeyPress += Ed_input_KeyPress!;
            ThemeManager.ApplyToForm(this);
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

            _isinitloading = true;

            // set cb_user to the Program.Settings.UserFile value if it's in the list, otherwise set index to 0.
            cb_user.SelectedIndex = cb_user.Items.Contains(Program.Settings.UserFile) ? cb_user.Items.IndexOf(Program.Settings.UserFile) : 0;
            // set cb_infer to the Program.Settings.InferenceFile value if it's in the list, otherwise set index to 0.
            cb_infer.SelectedIndex = cb_infer.Items.Contains(Program.Settings.SamplerFile) ? cb_infer.Items.IndexOf(Program.Settings.SamplerFile) : 0;
            // set cb_instruct to the Program.Settings.InstructFile value if it's in the list, otherwise set index to 0.
            cb_instruct.SelectedIndex = cb_instruct.Items.Contains(Program.Settings.Instruct) ? cb_instruct.Items.IndexOf(Program.Settings.Instruct) : 0;
            // set cb_sysprompt to the Program.Settings.PromptFile value if it's in the list, otherwise set index to 0.
            cb_sysprompt.SelectedIndex = cb_sysprompt.Items.Contains(Program.Settings.PromptFile) ? cb_sysprompt.Items.IndexOf(Program.Settings.PromptFile) : 0;

            // set cb_bot to the Program.Settings.BotFile value if it's in the list, otherwise set index to 0.
            // If the saved bot is password-protected, default to "Assistant" to avoid a zombie state on startup.
            var savedBotFile = Program.Settings.BotFile;
            if (cb_bot.Items.Contains(savedBotFile) && DataFiles.Characters.TryGetValue(savedBotFile, out var savedBotChar) && savedBotChar.Protected)
                savedBotFile = "Assistant";
            cb_bot.SelectedIndex = cb_bot.Items.Contains(savedBotFile) ? cb_bot.Items.IndexOf(savedBotFile) : 0;


            num_maxcontext.Maximum = Program.Settings.MaxTotalTokens;
            num_maxcontext.Value = Program.Settings.MaxTotalTokens;
            num_maxresponse.Value = Program.Settings.MaxReplyLength;
            num_temperature.Value = (decimal)Program.Settings.Temperature;
            mck_sessionmemory.Checked = LLMEngine.Settings.SessionMemorySystem;
            mck_ttstoggle.Checked = Program.Settings.UseTTS;
            mck_disablethink.Checked = Program.Settings.DisableThinking;
            mck_ragtothink.Checked = Program.Settings.RAGMoveToThinkBlock;
            mck_agentmode.Checked = LLMEngine.Bot.AgentMode;
            mckNatMem.Checked = !LLMEngine.Bot.Brain.DisableEurekas;
            btVectorSearch.Enabled = LLMEngine.Settings.RAGEnabled;

            Program.ApplyContextPluginSettings();

            LLMEngine.ContextPlugins = [];
            LLMEngine.ContextPlugins.Add(new BrowsePlugin());
            LLMEngine.ContextPlugins.Add(new LocationPlugin("Locations"));
            LLMEngine.ContextPlugins.Add(new WebSearchPlugin());
            mck_ragenabled.Checked = LLMEngine.Settings.RAGEnabled;
            mck_worldinfo.Checked = LLMEngine.Settings.AllowWorldInfo;
            SubscribeLLMEvents();

            ed_input.EnableImageDragDrop(basestr =>
            {
                LLMEngine.VLM_ClearImages();
                LLMEngine.VLM_AddImage(DragNDropExtension.DroppedFilePath);
                DisplayImage(basestr);
            }, 1024);
            pictEmbed.EnableImageDragDrop(basestr =>
            {
                LLMEngine.VLM_ClearImages();
                LLMEngine.VLM_AddImage(DragNDropExtension.DroppedFilePath);
                DisplayImage(basestr);
            }, 1024);
            _isinitloading = false;

        }

        private void SubscribeLLMEvents()
        {
            LLMEngine.OnInferenceStreamed += OnStreamMessageReceived;
            LLMEngine.OnInferenceEnded += OnStreamInferenceEnded;
            LLMEngine.OnFullPromptReady += OnFullPromptReady;
            LLMEngine.OnStatusChanged += OnStatusChanged;
        }

        private void UnsubscribeLLMEvents()
        {
            LLMEngine.OnInferenceStreamed -= OnStreamMessageReceived;
            LLMEngine.OnInferenceEnded -= OnStreamInferenceEnded;
            LLMEngine.OnFullPromptReady -= OnFullPromptReady;
            LLMEngine.OnStatusChanged -= OnStatusChanged;
        }

        private async void bt_backend_Click(object sender, EventArgs e)
        {
            try
            {
                using var loginForm = new LoginForm();
                ThemeManager.ApplyToForm(loginForm);
                loginForm.ShowDialog(this);
                if (loginForm.DialogResult == DialogResult.OK)
                {
                    UnsubscribeLLMEvents();
                    SubscribeLLMEvents();
                    num_maxcontext.Maximum = LLMEngine.MaxContextLength;
                    num_maxcontext.Value = LLMEngine.MaxContextLength;
                    this.Text = "w(AI)fu.NET: " + LLMEngine.CurrentModel;
                    mck_ttstoggle.Enabled = LLMEngine.SupportsTTS;
                    mck_onlinerag.Enabled = LLMEngine.SupportsWebSearch;
                    cboxVLM.Enabled = LLMEngine.SupportsVision;
                    cboxVLM.Expanded = LLMEngine.SupportsVision;
                    UpdateUIState();
                    await LoadHistoryToUI();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error switching backend: {ex.Message}", "Backend Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                bt_send.BackColor = Color.DarkSeaGreen;
                bt_reroll.Enabled = true;
                bt_newsession.Enabled = true;
                bt_impersonate.Enabled = true;
                cb_bot.Enabled = true;
                cboxGroup.Enabled = true;
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
                cboxGroup.Enabled = false;
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
                cboxGroup.Enabled = false;
                cb_bot.Enabled = true;
                cb_user.Enabled = true;
            }
            if (Bot?.AllowedSamplers.Count > 0)
            {
                mck_charsampler.Enabled = true;
            }
            else
            {
                mck_charsampler.Enabled = false;
                mck_charsampler.Checked = false;
            }
            mck_sessionmemory.Checked = LLMEngine.Settings.SessionMemorySystem;
            mck_ttstoggle.Checked = Program.Settings.UseTTS;
            mck_disablethink.Checked = Program.Settings.DisableThinking;
            mck_ragtothink.Checked = Program.Settings.RAGMoveToThinkBlock;
            mck_agentmode.Checked = LLMEngine.Bot.AgentMode;
            mckNatMem.Checked = !LLMEngine.Bot.Brain.DisableEurekas;
            btVectorSearch.Enabled = LLMEngine.Settings.RAGEnabled;

            cbGroupSwitch.Enabled = LLMEngine.Status == SystemStatus.Ready;
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
            LLMEngine.Bot.SaveToFile("data/chars/");
        }

        /// <summary>
        /// Handles bot/character switch 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void cb_bot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isinitloading || !IsHandleCreated || !web_chat.IsHandleCreated)
                return;

            if (cb_bot.SelectedItem is string key && !string.IsNullOrEmpty(key))
            {
                var previousBot = LLMEngine.Bot;
                var previousSelection = LLMEngine.Bot.UniqueName;
                try
                {
                    LLMEngine.Bot = DataFiles.Characters[key];
                    await LoadHistoryToUI();
                    mck_guidance.Checked = !LLMEngine.Bot.DisableBotGuidance;
                    mck_caninitchat.Checked = Bot?.CanInitiateChat ?? false;
                    var searchplug = LLMEngine.ContextPlugins.Find(x => x.PluginID == "WebSearch");
                    if (searchplug != null)
                    {
                        mck_onlinerag.Checked = searchplug.Enabled;
                        mck_onlinerag.Enabled = true;
                    }
                    else
                    {
                        mck_onlinerag.Enabled = false;
                        mck_onlinerag.Checked = false;
                    }
                    _activityTimer?.Reset();
                    UpdateUIState();
                    if (LLMEngine.Bot is not GroupChar)
                    {
                        _isinitloading = true;
                        cbGroupSwitch.Enabled = false;
                        lstGroupMembers.Items.Clear();
                        lstGroupMembers.Enabled = false;
                        ckGroupToggle.Checked = false;
                        _isinitloading = false;
                    }
                    FillGroupMemberList();
                }
                catch (OperationCanceledException)
                {
                    // User cancelled password entry — revert to previous bot
                    _isinitloading = true;
                    if (previousSelection != null && cb_bot.Items.Contains(previousSelection))
                        cb_bot.SelectedItem = previousSelection;
                    else if (cb_bot.Items.Count > 0)
                        cb_bot.SelectedIndex = 0;
                    _isinitloading = false;
                    if (previousBot != null)
                        LLMEngine.Bot = previousBot;
                    MessageBox.Show("Character switch cancelled: password was not provided.", "Access Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
            if (string.IsNullOrEmpty(e))
                return;
            if (!_impersonatemode && !string.IsNullOrEmpty(LLMEngine.Instruct.ThinkingStart) && _currentgencalls == 1)
            {
                var thoughts = ChatRender.GetMessagePrefix(AuthorRole.Assistant) + $"*{LLMEngine.Bot.GetIdentifier()} is thinking...*";
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
                await InvokeAsync(async () => { await WebEditLastMessage(MsgPrefix + stringfix, msg.Guid); });
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
            Bot?.SaveChatHistory();

            // GROUP CHAT CHAIN: continue with next queued bot if any
            if (await AdvanceGroupQueue())
                return;
        }

        [Obsolete("This method will be removed in future versions and replaced by an action. In the meantime, bot cannot initiate conversations.")]
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
            var message = "The last message from {{user}} was posted " + StringExtensions.TimeSpanToHumanString(DateTime.Now - lastusermessage.Date) + " ago. We're {{day}}, the {{date}} at {{time}} now. Would you like to send a message to {{user}} now? Use your best judgement based on the conversation above. In case you don't want to send a message, just respond with No. If you want to send a message, write the message to {{user}} directly while making sure it's contextually relevant. \n\nThis query will repeat every few minutes.";
            if (_afkmessagecount > 1)
                message += " You've already sent " + _afkmessagecount + " unanswered messages in a row.";
            else if (_afkmessagecount == 1)
                message += " You've already sent a message.";
            message = LLMEngine.Bot.ReplaceMacros(message);
            statusbar.Items[1].Text = "Analyzing...";
            var response = "no"; //  await LLMEngine.QuickInferenceForSystemPrompt(message, false);
            response = response.RemoveThinkingBlocks().Trim();

            if (!string.IsNullOrEmpty(response) && !response.StartsWith("no", StringComparison.InvariantCultureIgnoreCase))
            {
                var msg = new SingleMessage(AuthorRole.Assistant, response);
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

        private async void SendMessage(object sender, EventArgs e)
        {
            LLMEngine.Bot.AgentSystem?.NotifyUserActivity();
            _activityTimer?.Reset();
            _afkmessagecount = 0;
            if (LLMEngine.Status == SystemStatus.Busy)
            {
                LLMEngine.CancelGeneration();
                if (LLMEngine.Bot is GroupChar mygroup)
                    mygroup.ClearResponseQueue();
                UpdateUIState();
                return;
            }
            _impersonatemode = false;
            _postdate = DateTime.Now;
            statusbar.Items[1].Text = "Analyzing...";
            UseCharacterDefinedSampler();

            if (string.IsNullOrEmpty(ed_input.Text))
            {
                // ready a new message for the bot's response
                PrepareResponse();
                await SendMessageToUI(new SingleMessage(AuthorRole.Assistant, "*" + LLMEngine.Bot.GetIdentifier() + " is thinking...*"));
                ed_input.Text = string.Empty;
                await LLMEngine.AddBotMessage();
                return;
            }
            ;

            var msgtxt = ed_input.Text.ToLinuxFormat();
            msgtxt = LLMEngine.Bot.ReplaceMacros(msgtxt);
            SlashReturn? foundslash = null;
            foreach (var slash in slashCommands)
            {
                var res = slash.RunCommand(msgtxt);
                if (res.Message != null)
                {
                    foundslash = res;
                    break;
                }
            }
            var userMsg = new SingleMessage(AuthorRole.User, msgtxt, DragNDropExtension.DroppedFilePath);
            if (foundslash is not null && foundslash.ReplaceUser && foundslash.Message is not null)
            {
                userMsg = foundslash.Message;
            }
            await SendMessageToUI(userMsg);
            if (foundslash is not null && !foundslash.ReplaceUser && foundslash.Message is not null)
            {
                await SendMessageToUI(foundslash.Message);
                if (foundslash.LogToHistory)
                    LLMEngine.History.LogMessage(foundslash.Message);
            }

            // GROUP CHAT START: build queue & prime first responder
            _lastUserMessageForGroupLoop = userMsg;
            if (LLMEngine.Bot is GroupChar ggroup && (foundslash is null || !foundslash.NoBotResponse))
            {
                await ggroup.BuildResponseQueue(msgtxt);
                var first = await ggroup.PrimeFirstResponder();
                if (first != null)
                {
                    UpdateGroupSelection(); // keep UI in sync
                    LLMEngine.InvalidatePromptCache();
                }
            }
            // GROUP CHAT END

            if (foundslash is null || !foundslash.NoBotResponse)
            {
                // ready a new message for the bot's response
                PrepareResponse();
                await SendMessageToUI(new SingleMessage(AuthorRole.Assistant, "*" + LLMEngine.Bot.GetIdentifier() + " is reading your message...*"));
                ed_input.Text = string.Empty;
                await LLMEngine.SendMessageToBot(userMsg);
            }
        }

        private async void RerollMessage(object sender, EventArgs e)
        {
            if (LLMEngine.Bot is GroupChar g)
                g.ClearResponseQueue();
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
            await SendMessageToUI(new SingleMessage(AuthorRole.Assistant, "*" + LLMEngine.Bot.GetIdentifier() + " is reading your message...*"));
            PrepareResponse();
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
            await LLMEngine.RerollLastMessage();
        }

        private async void bt_connectClick(object sender, EventArgs e)
        {
            await LLMEngine.Connect();
            num_maxcontext.Maximum = LLMEngine.MaxContextLength;
            num_maxcontext.Value = LLMEngine.MaxContextLength;
            this.Text = "w(AI)fu.NET: " + LLMEngine.CurrentModel;
            mck_ttstoggle.Enabled = LLMEngine.SupportsTTS;
            mck_onlinerag.Enabled = LLMEngine.SupportsWebSearch;
            cboxVLM.Enabled = LLMEngine.SupportsVision;
            cboxVLM.Expanded = LLMEngine.SupportsVision;
            LLMEngine.Bot.AgentSystem?.NotifyUserActivity();
            UpdateUIState();
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
                    loadingForm.AddProgress(25);
                };
                await LLMEngine.History.StartNewChatSession(true, false);
                //await LLMEngine.Bot.Brain.ProcessPreviousSession();
                if (LLMEngine.Bot.SelfEditTokens > 0)
                {
                    loadingForm.SetMessage("Updating dynamic character (this might take a few minutes).");
                }
                else
                {
                    loadingForm.SetMessage("Saving history.");
                    loadingForm.SetProgress(95);
                }
                Bot.SaveChatHistory();
                await Bot.UpdateSelfEditSection();
                Bot.SaveToFile("data/chars");
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
            if (LLMEngine.Bot is GroupChar g)
                g.ClearResponseQueue();

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
            if (InvokeRequired)
            {
                await InvokeAsync(WebChatLoad);
            }
            else
            {
                await WebChatLoad();
            }

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
            if (!mck_charsampler.Checked || !mck_charsampler.Enabled || Bot == null)
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
                Program.Settings.UseTTS = mck_ttstoggle.Checked;
                LLMEngine.Bot.AgentMode = mck_agentmode.Checked;
                LLMEngine.Bot.Brain.DisableEurekas = !mckNatMem.Checked;
                Program.Settings.SessionMemorySystem = mck_sessionmemory.Checked;
                Program.ApplyContextPluginSettings();
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
            LLMEngine.Settings.RAGEnabled = mck_ragenabled.Checked;
            btVectorSearch.Enabled = LLMEngine.Settings.RAGEnabled;
        }

        #endregion


        #region *** WebView2 Handling ***

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

        private static string InjectDialogHtml(string imgPath, string dialog, Guid messageGuid)
        {
            // dialog should already be sanitized for HTML; the pipeline calling this produces the HTML
            // Ensure both .thinking-content and .message-raw exist so JS paths always have a target.
            return $@"
        <div class='chat-message' data-message-guid='{messageGuid}'>
            <div class='portrait'>
                <img src='https://appassets.test/img/{imgPath}' alt='Portrait' width='60'>
            </div>
            <div class='message-content'>
                <div class='thinking-content'></div>
                <div class='message-raw'>
                    {dialog}
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
                    img = (singleMessage.User as ICharacter)!.Icon;
                    break;
                case AuthorRole.Assistant:
                    img = (singleMessage.Bot as ICharacter)!.Icon;
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
                newMessage.Contains(LLMEngine.Instruct.ThinkingStart))
            {
                // Strip assistant prefix
                var worktext = newMessage[ChatRender.GetMessagePrefix(AuthorRole.Assistant).Length..];

                if (!worktext.Contains(LLMEngine.Instruct.ThinkingEnd))
                {
                    // Thinking-only update
                    worktext = worktext.Replace(LLMEngine.Instruct.ThinkingStart, string.Empty);
                    var text = Markdown.ToHtml(worktext, CustomMarkDownPipeline).SanitizeForJS();
                    await web_chat.CoreWebView2.ExecuteScriptAsync(BuildAtomicUpdateScript(text, isThinking: true, messageGuid));
                }
                else
                {
                    // Thinking + final
                    var parts = worktext.Split([LLMEngine.Instruct.ThinkingEnd], 2, StringSplitOptions.None);

                    var thinkingText = parts[0].Replace(LLMEngine.Instruct.ThinkingStart, string.Empty);
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

        public async void ForceUpdateLastMessage(string update)
        {
            await WebEditLastMessage(update);
        }

        private async Task WebChatLoad()
        {
            if (web_chat.CoreWebView2 == null)
                await InitializeWebViewAsync();

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

        private async Task InitializeWebViewAsync()
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

        #endregion


        #region *** Audio and TTS ***

        private async Task OutputTTS(string text)
        {
            // remove all text between asterisks, including the asterisks, as those are for markdown formatting and would mess with TTS
            var cleanText = System.Text.RegularExpressions.Regex.Replace(text, @"\*.*?\*", "\n");

            var paragraphs = cleanText.Split(["\n\n"], StringSplitOptions.RemoveEmptyEntries);

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
            Program.Settings.UseTTS = mck_ttstoggle.Checked;
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
                mck_forceNames.Checked = LLMEngine.Instruct.AddNamesToPrompt;
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

        private void mck_guidance_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Bot.DisableBotGuidance = !mck_guidance.Checked;
        }

        private void ck_sessionmemory_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.SessionMemorySystem = mck_sessionmemory.Checked;
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
            LLMEngine.Instruct.AddNamesToPrompt = mck_forceNames.Checked;
            LLMEngine.InvalidatePromptCache();
        }

        private void ck_worldinfo_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.AllowWorldInfo = mck_worldinfo.Checked;
        }

        private void cb_sysprompt_SelectionIndexChanged(object sender, EventArgs e)
        {
            if (cb_sysprompt.SelectedItem is string key && !string.IsNullOrEmpty(key))
                LLMEngine.SystemPrompt = DataFiles.SysPrompts[key];
        }

        private void ck_caninit_CheckedChanged(object sender, EventArgs e)
        {
            Bot?.CanInitiateChat = mck_caninitchat.Checked;
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
                searchplug.Enabled = mck_onlinerag.Checked;
                mck_onlinerag.Enabled = true;
            }
            else
            {
                mck_onlinerag.Enabled = false;
                mck_onlinerag.Checked = false;
            }
        }

        private void bt_editchar_Click(object sender, EventArgs e)
        {
            using var editForm = new CharEditForm();
            ThemeManager.ApplyToForm(editForm);
            editForm.SetupCharacterEditor(Bot?.GetIdentifier() ?? string.Empty);
            editForm.ShowDialog();
            LLMEngine.InvalidatePromptCache();
            var currbselection = cb_bot.SelectedText;
            var curruselection = cb_user.SelectedText;
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
            DragNDropExtension.DroppedFilePath = string.Empty;
            pictEmbed.Image = null;
        }

        private void ck_disablethink_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.DisableThinking = mck_disablethink.Checked;
        }

        private void ck_ragtothink_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.RAGMoveToThinkBlock = mck_ragtothink.Checked;
        }

        private void ck_agentmode_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Bot.AgentMode = mck_agentmode.Checked;
        }

        private void btInstructEdit_Click(object sender, EventArgs e)
        {
            using var editForm = new InstructForm();
            ThemeManager.ApplyToForm(editForm);
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
            ThemeManager.ApplyToForm(editForm);
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
            ThemeManager.ApplyToForm(editForm);
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
            ThemeManager.ApplyToForm(settingsForm);
            settingsForm.StartPosition = FormStartPosition.CenterParent;
            settingsForm.ShowDialog();
            LLMEngine.InvalidatePromptCache();
            if (LLMEngine.Status == SystemStatus.Ready)
                await WebChatLoad();
        }

        private void btWorldEditor_Click(object sender, EventArgs e)
        {
            using var worldForm = new WorldEditForm();
            ThemeManager.ApplyToForm(worldForm);
            worldForm.StartPosition = FormStartPosition.CenterParent;
            worldForm.ShowDialog();
            LLMEngine.InvalidatePromptCache();
        }

        private async void btChatHistory_Click(object sender, EventArgs e)
        {
            using var worldForm = new ChatHistoryForm();
            ThemeManager.ApplyToForm(worldForm);
            worldForm.StartPosition = FormStartPosition.CenterParent;
            worldForm.ShowDialog();
            LLMEngine.InvalidatePromptCache();
            await WebChatLoad();
        }

        private void btVectorSearch_Click(object sender, EventArgs e)
        {
            using var ragForm = new RAGSearchForm();
            ThemeManager.ApplyToForm(ragForm);
            ragForm.StartPosition = FormStartPosition.CenterParent;
            ragForm.ShowDialog();
        }

        private void btRawLog_Click(object sender, EventArgs e)
        {
            // Show a basic window with the ed_log content in a textbox and a close button
            using var logForm = new RawLogForm();
            ThemeManager.ApplyToForm(logForm);
            logForm.SetText(ed_log);
            logForm.StartPosition = FormStartPosition.CenterParent;
            logForm.ShowDialog();
        }

        private void dbgMood_Click(object sender, EventArgs e)
        {
            var mood = LLMEngine.Bot.Brain.Mood.Describe();
            // show a simple message box
            MessageBox.Show(this, $"{LLMEngine.Bot.ReplaceMacros(mood)}", "Mood State", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MemoryBrowserForm.ShowForActiveBot(this);
        }

        private void ckNatMem_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Bot.Brain.DisableEurekas = !mckNatMem.Checked;
        }

        private void dbgRunAgent_Click(object sender, EventArgs e)
        {
            LLMEngine.Bot.AgentSystem?.ForceRunLoop();
        }

        private void dbgPromptInsert_Click(object sender, EventArgs e)
        {
            var build = new StringBuilder();
            foreach (var item in LLMEngine.dataInserts)
            {
                build.AppendLine(item.Memory.Name + " [ " + item.Duration + " ]");
            }
            MessageBox.Show(build.ToString(), "Current Prompt Inserts", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void cbGroupSwitch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressGroupSwitchEvent || LLMEngine.Status == SystemStatus.Busy)
                return;
            if (Bot is GroupChar group)
            {
                var selectedName = cbGroupSwitch.SelectedItem as string;
                var selectedChar = group.AllPersonas.Find(p => p.UniqueName == selectedName);
                if (selectedChar != null)
                {
                    group.SetCurrentBot(selectedName!);
                    LLMEngine.InvalidatePromptCache();
                }
            }
        }

        private void FillGroupMemberList()
        {
            lstGroupMembers.Items.Clear();
            if (Bot is GroupChar group)
            {
                var lst = DataFiles.Characters.Values.Where(c => !c.IsUser).OrderBy(c => c.Name).ToList();
                if (group.PrimaryBot is not null)
                    lst.Remove(group.PrimaryBot);
                foreach (var persona in lst)
                {
                    lstGroupMembers.Items.Add(persona.UniqueName, group.SecondaryPersonaNames.Contains(persona.UniqueName));
                }
            }
        }

        private async void ckGroupToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_isinitloading || Bot is null)
                return;
            cbGroupSwitch.Enabled = ckGroupToggle.Checked;
            lstGroupMembers.Enabled = ckGroupToggle.Checked;
            if (ckGroupToggle.Checked)
            {
                if (Bot is GroupChar)
                    return; // Already in group mode
                var group = new GroupChar();
                group.SetPrimaryPersona((Character)Bot!);
                LLMEngine.Bot = group;
                cbGroupSwitch.Items.Clear();
                foreach (var name in group.AllPersonas)
                {
                    cbGroupSwitch.Items.Add(name.UniqueName);
                }
                await LoadHistoryToUI();
                mck_guidance.Checked = !LLMEngine.Bot.DisableBotGuidance;
                mck_caninitchat.Checked = Bot?.CanInitiateChat ?? false;
                var searchplug = LLMEngine.ContextPlugins.Find(x => x.PluginID == "WebSearch");
                if (searchplug != null)
                {
                    mck_onlinerag.Checked = searchplug.Enabled;
                    mck_onlinerag.Enabled = true;
                }
                else
                {
                    mck_onlinerag.Enabled = false;
                    mck_onlinerag.Checked = false;
                }
                _activityTimer?.Reset();
                UpdateUIState();
            }
            else
            {
                if (Bot is not GroupChar curGroup)
                    return; // Already in single mode
                curGroup.ClearResponseQueue();
                var gobackbot = curGroup.PrimaryBot;
                // set the cb_bot checkbox to the primary bot
                if (gobackbot is not null)
                {
                    cb_bot.SelectedItem = gobackbot?.UniqueName;
                    cb_bot_SelectedIndexChanged(cb_bot, new EventArgs());
                }
                lstGroupMembers.Items.Clear();
            }
            FillGroupMemberList();
            UpdateGroupSelection();
        }

        private void lstGroupMembers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (Bot is not GroupChar group)
                return;
            var personaID = lstGroupMembers.Items[e.Index].ToString() ?? string.Empty;
            if (e.NewValue == CheckState.Checked)
            {
                var persona = DataFiles.Characters[personaID];
                group.AddSecondaryPersona(persona);
            }
            else
            {
                group.RemoveSecondaryPersona(personaID);
            }
            UpdateGroupSelection();
            LLMEngine.InvalidatePromptCache();
        }

        private void UpdateGroupSelection()
        {
            if (Bot is not GroupChar group) return;
            _suppressGroupSwitchEvent = true;
            try
            {
                cbGroupSwitch.Items.Clear();
                foreach (var name in group.AllPersonas)
                    cbGroupSwitch.Items.Add(name.UniqueName);

                cbGroupSwitch.SelectedItem = group.CurrentBotId;
            }
            finally
            {
                _suppressGroupSwitchEvent = false;
            }
        }

        // Add this helper inside MainForm (class scope)
        private async Task<bool> AdvanceGroupQueue()
        {
            if (LLMEngine.Bot is not GroupChar ggroup)
                return false;

            var lastUser = _lastUserMessageForGroupLoop;
            if (lastUser is null)
                return false;

            var next = await ggroup.GetNextFromQueue();
            if (next is null)
                return false;

            ggroup.SetCurrentBot(next.UniqueName);

            // UI work must happen on the UI thread.
            BeginInvoke(new Action(async () =>
            {
                try
                {
                    DragNDropExtension.DroppedFilePath = string.Empty;
                    LLMEngine.VLM_ClearImages();
                    UpdateGroupSelection();
                    LLMEngine.InvalidatePromptCache();
                    PrepareResponse();
                    await SendMessageToUI(new SingleMessage(
                        AuthorRole.Assistant,
                        "*" + LLMEngine.Bot.GetIdentifier() + " is reading your message...*"));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("AdvanceGroupQueue UI block error: " + ex);
                }
            }));

            // Fire-and-forget the actual generation. We do NOT await here, so the next
            // OnStreamInferenceEnded after this bot finishes will run with no guard blockage.
            _ = Task.Run(async () =>
            {
                try
                {
                    await LLMEngine.AddBotMessage().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("AdvanceGroupQueue generation error: " + ex);
                }
            });

            return true;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            var msg = Bot.Brain.BuildAwayMessage(true);
            if (msg is null)
                return;
            LLMEngine.History.LogMessage(msg);
            await LoadHistoryToUI();


        }

        private async void button5_Click(object sender, EventArgs e)
        {
            var calaction = new CalendarUpdateAction();
            var res = await calaction.Execute(Bot!.Brain.DailySchedule, CancellationToken.None);
            if (res is null)
                return;
            MessageBox.Show(this, res.ScheduleToString(), "New Calendar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FactBrowserForm.ShowForActiveBot(this);
        }
    }
}
