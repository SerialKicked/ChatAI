using LetheAISharp;
using LetheAISharp.Agent;
using LetheAISharp.Agent.Actions;
using LetheAISharp.Agent.Tools;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Markdig;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using LetheChat.AgentPlugins;
using LetheChat.Controls;
using LetheChat.Files;
using LetheChat.Game;
using LetheChat.Plugins;
using LetheChat.Slash;
using LetheChat.Forms;
using LetheAISharp.Moods;

namespace LetheChat
{
    public partial class MainForm : Form
    {
        private StringBuilder _currentgeneration = new();
        private StringBuilder _currentgenerationThink = new();
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

        public WebUI webUI = null!;

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

            // Avoid running any runtime logic when the designer instantiates the form;
            // this prevents exceptions and stale WPF state that blank the designer surface.
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            webUI = new WebUI(this, web_chat);

            EnsureLLMLoggerConnected();
            this.Shown += async (_, __) =>
            {
                await webUI.InitializeWebViewAsync();
                cb_bot_SelectedIndexChanged(cb_bot, new EventArgs());
                await webUI.LoadHistoryToUI();
            };

            ed_input.SpellCheckLanguage = "en-US";
            SetupHelp();

            MoodManager.LoadDefaultMoods();
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
            // Register tools
            LLMEngine.ToolManager.RegisterToolList(new WebSearchTools());
            LLMEngine.ToolManager.RegisterToolList(new MemoryTools());
            var pluginsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");
            LLMEngine.RegisterPluginsFromDirectory(pluginsDir);

            // Manage theme
            if (Program.Settings.Skin == "Light")
                ThemeManager.ApplyLight();
            else
                ThemeManager.ApplyDark();

            // Chat related events
            SetupChatMenu();
            _activityTimer.OnTrigger += OnBotInitiateConversation;
            // Autodetection of the user's activity (mostly for the background agent functionalities)
            Application.AddMessageFilter(new ActivityMessageFilter());
            ed_input.KeyPress += Ed_input_KeyPress!;
            ThemeManager.ApplyToForm(this);
        }

        private void SetupHelp() 
        {
            HelptoolTip.ToolTipIcon = ToolTipIcon.Info;
            HelptoolTip.ToolTipTitle = "Help";
            HelpTool.ApplyTooltip(HelptoolTip, mck_ragenabled, "Use RAG functionalities to insert relevant memories into the prompt based on the user's input." + Environment.NewLine + "Configurable in the Settings menu.");
            HelpTool.ApplyTooltip(HelptoolTip, mck_sessionmemory, "Insert summaries of recent chat sessions into the system prompt. This drastically increases the character's long-term memory." + Environment.NewLine + "This function uses a set amount of tokens, configurable in the Settings menu.");
            HelpTool.ApplyTooltip(HelptoolTip, mck_worldinfo, "Use the Lorebook file(s) associated with this bot. A Lorebook is a list of keyword-triggered textual information that is inserted into the prompt when the conditions are met." + Environment.NewLine + "See the Settings menu, and Character and Lorebook editors for additional settings.");
            HelpTool.ApplyTooltip(HelptoolTip, mck_charsampler, "If checked the application will switch between the different sampling settings associated with the current character at random." + Environment.NewLine + "Character-specific settings can be set in the Character editor.");
            HelpTool.ApplyTooltip(HelptoolTip, mck_onlinerag, "If checked, the character is allowed to perform web searches (using DuckDuckGo or Brave) to improve its responses when asked to. Check the Settings menu for details." + Environment.NewLine + Environment.NewLine + "This is a custom system used when the web-search toolset is not available or enabled.");
            HelpTool.ApplyTooltip(HelptoolTip, mck_guidance, "Depending on the characters' settings, they can have moods varying over time, can be aware of the time, or change the topic of discussion based on background activity." + Environment.NewLine + Environment.NewLine + "This is a global check allowing/disallowing all those features at once, so you don't have to change 4 different settings each time you want to toggle the guidance.");
            HelpTool.ApplyTooltip(HelptoolTip, mckNatMem, "When a character can run background tasks (like goal setting or background web queries), the system can insert the results into the prompt when it feels its related to the current conversation." + Environment.NewLine + Environment.NewLine + "This allows you to toggle the behavior entirely.");
            HelpTool.ApplyTooltip(HelptoolTip, ckToolCalls, "Whether to allow the use of tool-calls in the current conversation. If unchecked, the bot will not be able to call any tools, even if they are enabled in the settings." + Environment.NewLine + Environment.NewLine + "This is useful to quickly toggle tool access without having to change the settings.");
            HelpTool.ApplyTooltip(HelptoolTip, mck_agentmode, "Allow the bot to use its background agent features when the user is AFK, such as setting goals, writing in their journal, or performing websearches based on discussion." + Environment.NewLine + Environment.NewLine + "This is only relevant if the character has background tasks setup.");
            HelpTool.ApplyTooltip(HelptoolTip, mck_forceNames, "Whether to add the user and bot's names before their respective input (name: input). This can help some fine-tuned models that have been trained that way. This may conflict with the correct operation of thinking models.");
            HelpTool.ApplyTooltip(HelptoolTip, mck_disablethink, "Check this if you're not using a 'thinking' model or if you wish to disable the feature.");
            HelpTool.ApplyTooltip(HelptoolTip, ckGroupToggle, "Toggle group chat mode. Allowing for secondary characters to interact with you and the current main character in a more dynamic way." + Environment.NewLine + Environment.NewLine + "Group mode works much better with a dedicated group system prompt. Not all models can play ball with group mode, especially thinking models (those need to be setup properly).");

            HelpTool.ApplyTooltip(HelptoolTip, bt_brain, "Open the current Character's Brain" + Environment.NewLine + Environment.NewLine + "This is where you can see and edit all the memories, goals, and background information unique to the character." + Environment.NewLine + "This is mostly relevant to characters that have background tasks or the memory toolset enabled.");
            HelpTool.ApplyTooltip(HelptoolTip, bt_backend, "Connect to an externally-managed backend.");
            HelpTool.ApplyTooltip(HelptoolTip, bt_llama, "Open the model selection menu and load any model through llama.cpp directly from Lethe Chat.");
            HelpTool.ApplyTooltip(HelptoolTip, bt_scenario, "Set a scenario override for the current conversation instead of the one associated with the character." + Environment.NewLine + "This is useful to quickly switch context without having to change the character's settings.");
            HelpTool.ApplyTooltip(HelptoolTip, bt_newsession, "Start a new chat session with the current character." + Environment.NewLine + Environment.NewLine +
                "This is a critically important part of Lethe Chat, it will archive the current session, summarize it, and add metadata for easier retrieval, " + Environment.NewLine +
                "before starting a new session. It is strongly encouraged to do this when reaching a natural pause in the conversation, or when getting close to " + Environment.NewLine +
                "the context limit of the model, to avoid losing important information and to keep the character's memory relevant." + Environment.NewLine + Environment.NewLine +
                "Understanding how to manage sessions is simple but crucial. See the official documentation for more information.");
            HelpTool.ApplyTooltip(HelptoolTip, bt_reroll, "Regenerate the last response from the bot without changing the conversation history." + Environment.NewLine + Environment.NewLine +
                "This is useful when you want to get a different response to the same input, or if you want to slightly change the direction of the conversation without adding new information." + Environment.NewLine +
                "The bot will use the same prompt and settings as before, so this is a good way to test how much randomness the model has, or to get a different variation of a response you liked.");
            HelpTool.ApplyTooltip(HelptoolTip, bt_impersonate, "Tell the bot to impersonate you and write a response for you. Not all models support this feature very well.");
            HelpTool.ApplyTooltip(HelptoolTip, btChatHistory, "View and manage the history of all your chat sessions with the current character." + Environment.NewLine + Environment.NewLine +
                "This is where you can see all your past conversations, export them, or load them back into the chat." + Environment.NewLine + 
                "This is also where you can see the summaries and metadata of each session, which can help you keep track of your interactions with the character.");
            HelpTool.ApplyTooltip(HelptoolTip, btMainSettings, "Open the main settings menu, where you can configure global settings for the application, memory systems, llama.cpp defaults, the appearance, and other settings.");

            HelpTool.ApplyTooltip(HelptoolTip, num_maxcontext, "Set the maximum context length for the model. This is the total number of tokens that the model can consider in a single response, including the prompt and the response itself." + Environment.NewLine + 
                "This value is normally automatically detected by Lethe chat according to the backend's settings.");
            HelpTool.ApplyTooltip(HelptoolTip, num_maxresponse, "Set the maximum length for the model's responses. This is the maximum number of tokens that the model will generate in a single response." + Environment.NewLine + 
                "Keep in mind that the total context length (prompt + response) cannot exceed the model's maximum context length.");
            HelpTool.ApplyTooltip(HelptoolTip, num_temperature, "Set the temperature for the model's responses (overriding the one provided by the selected sampling method." + Environment.NewLine +
                "This value that controls the randomness of the model's output. A higher temperature will result in more varied responses, while a lower temperature will result in more deterministic responses.");

            HelpTool.ApplyTooltip(HelptoolTip, cb_instruct, "Select an instruction template for the loaded model. Instruction templates are used to format the prompt in a way that model naturally understands." + Environment.NewLine +
                "It is imperative to select the correct instruction template for the model you are using, otherwise performance will degrade dramatically.");
            HelpTool.ApplyTooltip(HelptoolTip, cb_infer, "Select a sampling method for the loaded model. Sampling methods are used to control how the model generates responses, and can have a big impact on the quality and style of the responses." + Environment.NewLine +
                "The available sampling methods depend on the model you are using, and can be configured in the sampler editor.");
            HelpTool.ApplyTooltip(HelptoolTip, cb_sysprompt, "Select the format of the system prompt. The system prompt is a special part of the prompt that is used to give the model instructions on how to behave, and to provide it with relevant information that should be considered in its responses." + Environment.NewLine +
                "The system prompt can be used to set the tone of the conversation, to provide the model with information about the world or the characters, or to give it specific instructions on how to respond." + Environment.NewLine +
                "It depends on both your use case and the model being used.");
            HelpTool.ApplyTooltip(HelptoolTip, cb_bot, "Select the character you want to talk to. Each character can have its own unique personality, background, and settings that influence how it responds to your messages." + Environment.NewLine +
                "You can create and customize characters in the character editor, and each character can have its own set of memories, goals, and even different system prompts and sampling settings.");
            HelpTool.ApplyTooltip(HelptoolTip, cb_user, "Select the user profile you want to use. User profiles are used to store information about you that the model can use to personalize its responses, such as your name, preferences, or background information." + Environment.NewLine +
                "This is mostly useful for characters that have a more complex system prompt that takes into account the user's information, but it can be used with any character.");
            HelpTool.ApplyTooltip(HelptoolTip, cbGroupSwitch, "When talking to a character group, you can select which character in the group you want to talk next. This dropdown allows you to do that.");
        }

        private static void EnsureLLMLoggerConnected()
        {
            if (LLMEngine.Logger is LLMEngineUiLogger)
                return;

            LLMEngine.Logger = new LLMEngineUiLogger(nameof(LLMEngine));
            LLMEngine.Logger.LogInformation("LLMEngine logger connected from MainForm.");
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
            mck_agentmode.Checked = LLMEngine.Bot.AgentMode;
            mckNatMem.Checked = !LLMEngine.Bot.Brain.DisableEurekas;
            ckToolCalls.Checked = LLMEngine.Settings.ToolCallsAllowed;
            mck_ragenabled.Checked = LLMEngine.Settings.RAGEnabled;
            mck_worldinfo.Checked = LLMEngine.Settings.AllowWorldInfo;
            mck_forceNames.Checked = LLMEngine.Settings.AddNamesToPrompt;

            // Initialize context plugins
            Program.ApplyContextPluginSettings();
            LLMEngine.ContextPlugins = [];
            LLMEngine.ContextPlugins.Add(new LocationPlugin("Locations"));
            LLMEngine.ContextPlugins.Add(new WebSearchPlugin());


            SubscribeLLMEvents();


            ed_input.EnableImageDragDrop(basestr =>
            {
                DisplayImage(basestr);
            }, LLMEngine.Settings.ImageResolution);
            pictEmbed.EnableImageDragDrop(basestr =>
            {
                DisplayImage(basestr);
            }, LLMEngine.Settings.ImageResolution);
            _isinitloading = false;

        }

        private void SubscribeLLMEvents()
        {
            LLMEngine.OnInferenceSegment += LMEngine_OnInferenceSeqmentReceived;
            LLMEngine.OnInferenceCompleted += LLMEngine_OnInferenceCompleted;
            LLMEngine.OnFullPromptReady += OnFullPromptReady;
            LLMEngine.OnStatusChanged += OnStatusChanged;
            LLMEngine.OnHistoryLogged += LLMEngine_OnHistoryLogged;
        }

        private void UnsubscribeLLMEvents()
        {
            LLMEngine.OnInferenceSegment -= LMEngine_OnInferenceSeqmentReceived;
            LLMEngine.OnInferenceCompleted -= LLMEngine_OnInferenceCompleted;
            LLMEngine.OnFullPromptReady -= OnFullPromptReady;
            LLMEngine.OnStatusChanged -= OnStatusChanged;
            LLMEngine.OnHistoryLogged -= LLMEngine_OnHistoryLogged;
        }

        private void LLMEngine_OnHistoryLogged(object? sender, SingleMessage e)
        {
            if (e.Hidden && Program.Settings.ShowHiddenMessages)
                _forcereload = true;
        }

        private async void bt_backend_Click(object sender, EventArgs e)
        {
            try
            {
                // Kill any managed llama-server before switching to a different backend
                if (Program.LlamaCppProcess.IsRunning)
                    await Program.LlamaCppProcess.KillAsync();

                using var loginForm = new LoginForm();
                ThemeManager.ApplyToForm(loginForm);
                loginForm.ShowDialog(this);
                if (loginForm.DialogResult == DialogResult.OK)
                {
                    UnsubscribeLLMEvents();
                    SubscribeLLMEvents();
                    num_maxcontext.Maximum = LLMEngine.MaxContextLength;
                    num_maxcontext.Value = LLMEngine.MaxContextLength;
                    this.Text = "Lethe Chat: " + LLMEngine.CurrentModel;
                    mck_ttstoggle.Enabled = LLMEngine.SupportsTTS;
                    mck_onlinerag.Enabled = LLMEngine.SupportsWebSearch;
                    cboxVLM.Enabled = LLMEngine.SupportsVision;
                    cboxVLM.Expanded = LLMEngine.SupportsVision;
                    await RefreshConnectionState();
                }
                UpdateUIState();
                await webUI.LoadHistoryToUI();
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
                bt_llama.Enabled = Program.LlamaCppProcess.IsManaged;
                bt_delete.Enabled = true;
                bt_refresh.Enabled = true;
                bt_send.Enabled = true;
                bt_send.Text = "Send";
                bt_send.BackColor = Color.DarkSeaGreen;
                bt_reroll.Enabled = true;
                bt_newsession.Enabled = true;
                bt_impersonate.Enabled = true;
                cb_bot.Enabled = true;
                cboxGroup.Enabled = true;
                cb_user.Enabled = true;
                ckToolCalls.Enabled = true;
                bt_backend.Enabled = true;
                btMainSettings.Enabled = true;
                var (tokens, duration) = LLMEngine.History.GetCurrentChatSessionInfo();
                statusbar.Items[0].Text = $"Current Session: {duration.TotalDays:F2} days ({tokens} tokens)";
                cb_instruct.Enabled = true;
            }
            else if (LLMEngine.Status == SystemStatus.Busy)
            {
                bt_llama.Enabled = false;
                bt_delete.Enabled = false;
                bt_refresh.Enabled = false;
                bt_send.Enabled = true;
                bt_send.Text = "Cancel";
                bt_send.BackColor = Color.OrangeRed;
                bt_reroll.Enabled = false;
                bt_newsession.Enabled = false;
                bt_impersonate.Enabled = false;
                bt_backend.Enabled = false;
                cboxGroup.Enabled = false;
                cb_bot.Enabled = false;
                cb_user.Enabled = false;
                ckToolCalls.Enabled = false;
                btMainSettings.Enabled = false;
                cb_instruct.Enabled = false;
            }
            else if (LLMEngine.Status == SystemStatus.NotInit)
            {
                bt_llama.Enabled = Program.LlamaCppProcess.IsManaged;
                bt_delete.Enabled = false;
                bt_refresh.Enabled = false;
                bt_send.Enabled = false;
                bt_backend.Enabled = true;
                bt_send.Text = "Offline";
                bt_send.BackColor = Color.OrangeRed;
                bt_reroll.Enabled = false;
                bt_newsession.Enabled = false;
                bt_impersonate.Enabled = false;
                cboxGroup.Enabled = false;
                cb_bot.Enabled = true;
                cb_user.Enabled = true;
                btMainSettings.Enabled = true;
                cb_instruct.Enabled = true;
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
            mck_agentmode.Checked = LLMEngine.Bot.AgentMode;
            mckNatMem.Checked = !LLMEngine.Bot.Brain.DisableEurekas;
            mck_disablethink.Enabled = LLMEngine.Instruct.IsThinkFormat;
            cbMemStyle.SelectedIndex = (int)LLMEngine.Settings.RecallMemoryMode;

            if (!LLMEngine.SupportsToolCalls)
            {
                ckToolCalls.Enabled = LLMEngine.SupportsToolCalls;
                ckToolCalls.Checked = false;
            }


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
            Program.LlamaCppProcess.Dispose();
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
                    await webUI.LoadHistoryToUI();
                    mck_guidance.Checked = !LLMEngine.Bot.DisableBotGuidance;
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

        private async void LMEngine_OnInferenceSeqmentReceived(object? sender, InferenceSegment e)
        {
            if (string.IsNullOrEmpty(e.Text))
                return;

            if (!_impersonatemode && !string.IsNullOrEmpty(LLMEngine.Instruct.ThinkingStart) && _currentgencalls <= 1)
            {
                var thoughts = ChatRender.GetMessagePrefix(AuthorRole.Assistant) + $"*{LLMEngine.Bot.GetIdentifier()} is thinking...*";
                await webUI.EditMessage(thoughts);
            }

            switch (e.Channel)
            {
                case InferenceChannel.ToolCall:
                    if (e.ToolCall is not null)
                        _currentgenerationThink.AppendLinuxLine().AppendLinuxLine($"[Tool Call: {e.ToolCall.FunctionName}]");
                    break;
                case InferenceChannel.Text:
                    _currentgeneration.Append(e.Text);
                    break;
                case InferenceChannel.Thinking:
                    _currentgenerationThink.Append(e.Text);
                    break;
                default:
                    break;
            }
            _currentgencalls++;
            _currentgenerationtokencount++;
            _responselength = DateTime.Now - _postdate;
            _activityTimer?.Reset();
            if (_currentgenerationtokencount > 2)
            {
                _currentgenerationtokencount = 0;
                if (!_impersonatemode)
                {
                    Invoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        statusbar.Items[1].Text = $"Generation: {_responselength.TotalSeconds:F2}s";
                    });
                    var stringfix = _currentgeneration.ToString().StripThinkTags();
                    if (string.IsNullOrEmpty(stringfix))
                        stringfix = $"*{LLMEngine.Bot.GetIdentifier()} is thinking...*";
                    else
                        stringfix = stringfix.FixRoleplayString(Program.Settings.RoleplayFormatting, true);

                    var MsgPrefix = ChatRender.GetMessagePrefix(AuthorRole.Assistant);
                    await webUI.EditMessage(MsgPrefix + stringfix, _currentgenerationThink.ToString().StripThinkTags());
                }
                else
                {
                    Invoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        statusbar.Items[1].Text = $"Generation: {_responselength.TotalSeconds:F2}s";
                        ed_input.Text = _currentgenerationThink.ToString() + _currentgeneration.ToString();
                    });
                }
            }
        }

        private async void LLMEngine_OnInferenceCompleted(object? sender, InferenceResult e)
        {
            _responselength = DateTime.Now - _postdate;
            _activityTimer?.Reset();
            // add time to the log
            if (_impersonatemode)
            {
                _impersonatemode = false;
                Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    ed_input.Text = e.ThinkingContent?.ToWinFormat() ?? string.Empty + e.Response.ToWinFormat();
                    statusbar.Items[1].Text = $"Generation: {_responselength.TotalSeconds:F2}s";
                });
                LLMEngine.InvalidatePromptCache();
            }
            else
            {
                var activebot = (LLMEngine.Bot is GroupPersonaBase grp ? grp.GetCurrentPersona()?.Name : LLMEngine.Bot.Name) ?? LLMEngine.Bot.Name;
                var statcheck = activebot + ": ";
                var stringfix = e.Response;
                if (stringfix.StartsWith(statcheck))
                {
                    // remove the statcheck string at the start of e.Response. Like "Bob: Hello!" becomes "Hello!" if statcheck contains "Bob: "
                    var x = stringfix[statcheck.Length..];
                    stringfix = x;
                }
                else
                {
                    statcheck = "**" + activebot + ":** ";
                    if (stringfix.StartsWith(statcheck))
                    {
                        // remove the statcheck string at the start of e.Response. Like "Bob: Hello!" becomes "Hello!" if statcheck contains "Bob: "
                        var x = stringfix[statcheck.Length..];
                        stringfix = x;
                    }
                }
                if (Program.Settings.AsteriskCheck)
                    stringfix = stringfix.FixAsterisks();

                if (Program.Settings.RemoveCutSentence)
                    stringfix = stringfix.RemoveUnfinishedSentence();
                if (Program.Settings.AntiSlop)
                    stringfix = stringfix.RemoveSlop(Program.Settings.AntiSlopList, Program.Settings.AntiSlopRatio);
                // Roleplay filter
                stringfix = stringfix.FixRoleplayString(Program.Settings.RoleplayFormatting, false);

                var MsgPrefix = ChatRender.GetMessagePrefix(AuthorRole.Assistant);

                var msg = LLMEngine.Bot.History.LogMessage(AuthorRole.Assistant, stringfix, LLMEngine.User, LLMEngine.Bot);
                msg.ThinkBlock = e.ThinkingContent ?? string.Empty;
                var thinkingContent = !string.IsNullOrEmpty(e.ThinkingContent) ? e.ThinkingContent : null;
                await InvokeAsync(async () => { await webUI.EditMessage(MsgPrefix + stringfix, thinkingContent, msg.Guid); });
                PrepareResponse();

                if (_forcereload || Program.Settings.MaxMessagesOnScreen <= LLMEngine.History.CurrentSession.Messages.Count)
                {
                    Invoke((System.Windows.Forms.MethodInvoker)async delegate
                    {
                        await webUI.ReloadFullChat();
                    });
                    _forcereload = false;
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
            LLMEngine.Logger?.LogInformation($"[InferenceCompleted] Response: {e.Response} - Complete: {e.FinishReason} - Tool: {e.ToolCalls?.Count > 0}");
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
                await webUI.SendMessageToUI(msg);
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
            if (LLMEngine.Status == SystemStatus.NotInit)
                return;
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
            _currentgeneration.Clear();
            _currentgenerationThink.Clear();
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
            if (LLMEngine.Status == SystemStatus.NotInit)
                return;
            LLMEngine.Bot.AgentSystem?.NotifyUserActivity();
            _activityTimer?.Reset();
            _afkmessagecount = 0;
            if (LLMEngine.Status == SystemStatus.Busy)
            {
                LLMEngine.CancelGeneration();
                LLMEngine.Bot.AgentSystem?.CancelWork();
                if (LLMEngine.Bot is GroupChar mygroup)
                    mygroup.ClearResponseQueue();
                UpdateUIState();
                await webUI.ReloadFullChat();
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
                await webUI.SendMessageToUI(new SingleMessage(AuthorRole.Assistant, "*" + LLMEngine.Bot.GetIdentifier() + " is thinking...*"));
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
            await webUI.SendMessageToUI(userMsg);
            if (foundslash is not null && !foundslash.ReplaceUser && foundslash.Message is not null)
            {
                await webUI.SendMessageToUI(foundslash.Message);
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
                await webUI.SendMessageToUI(new SingleMessage(AuthorRole.Assistant, "*" + LLMEngine.Bot.GetIdentifier() + " is reading your message...*"));
                ed_input.Text = string.Empty;
                await LLMEngine.SendMessageToBot(userMsg);
                DragNDropExtension.DroppedFilePath = string.Empty;
                pictEmbed.Image = null;

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
            await webUI.RemoveLastMessage();
            await webUI.SendMessageToUI(new SingleMessage(AuthorRole.Assistant, "*" + LLMEngine.Bot.GetIdentifier() + " is reading your message...*"));
            PrepareResponse();
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
            await LLMEngine.RerollLastMessage();
        }

        private async void bt_connectClick(object sender, EventArgs e)
        {
            await RefreshConnectionState();
        }

        public async Task RefreshConnectionState()
        {
            await LLMEngine.Connect();
            num_maxcontext.Maximum = LLMEngine.MaxContextLength;
            num_maxcontext.Value = LLMEngine.MaxContextLength;
            this.Text = "Lethe Chat: " + LLMEngine.CurrentModel;
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
                if (Bot != null)
                {
                    Bot.SaveChatHistory();
                    await Bot.UpdateSelfEditSection();
                    Bot.SaveToFile("data/chars");
                }
                loadingForm.SetMessage("Loading new session.");
                loadingForm.SetProgress(100);
                LLMEngine.RemoveQuickInferenceEventHandler();
                await webUI.ReloadFullChat();
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
                await webUI.ReloadFullChat();
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
                await webUI.RemoveLastMessage();
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
                (DataFiles.LocalModels as IFile).SaveToFile("modelDB.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving Program.Settings: {ex.Message}");
            }
        }

        private void ck_ragenabled_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.RAGEnabled = mck_ragenabled.Checked;
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


        #region *** Group Chat Functions ***

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
                await webUI.LoadHistoryToUI();
                mck_guidance.Checked = !LLMEngine.Bot.DisableBotGuidance;
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
                    UpdateGroupSelection();
                    LLMEngine.InvalidatePromptCache();
                    PrepareResponse();
                    await webUI.SendMessageToUI(new SingleMessage(
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
                if (_isinitloading)
                    return;
                LLMEngine.InvalidatePromptCache();
                UpdateUIState();
            }
        }

        public void SetInstruct(string instruct)
        {
            if (!string.IsNullOrEmpty(instruct) && DataFiles.Instruct.TryGetValue(instruct, out var instructData))
            {
                LLMEngine.Instruct = instructData;
                cb_instruct.SelectedText = instruct;
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
            if (_isinitloading)
                return;
            LLMEngine.Bot.DisableBotGuidance = !mck_guidance.Checked;
        }

        private void ck_sessionmemory_CheckedChanged(object sender, EventArgs e)
        {
            if (_isinitloading)
                return;
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
            if (_isinitloading)
                return;
            LLMEngine.Settings.AddNamesToPrompt = mck_forceNames.Checked;
            LLMEngine.InvalidatePromptCache();
        }

        private void ck_worldinfo_CheckedChanged(object sender, EventArgs e)
        {
            if (_isinitloading)
                return;
            LLMEngine.Settings.AllowWorldInfo = mck_worldinfo.Checked;
        }

        private void cb_sysprompt_SelectionIndexChanged(object sender, EventArgs e)
        {
            if (cb_sysprompt.SelectedItem is string key && !string.IsNullOrEmpty(key))
                LLMEngine.SystemPrompt = DataFiles.SysPrompts[key];
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
            DragNDropExtension.DroppedFilePath = string.Empty;
            pictEmbed.Image = null;
        }

        private void ck_disablethink_CheckedChanged(object sender, EventArgs e)
        {
            if (_isinitloading)
                return;
            LLMEngine.Settings.DisableThinking = mck_disablethink.Checked;
        }

        private void ck_agentmode_CheckedChanged(object sender, EventArgs e)
        {
            if (_isinitloading)
                return;
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
            //ThemeManager.ApplyToForm(settingsForm);
            settingsForm.StartPosition = FormStartPosition.CenterParent;
            settingsForm.ShowDialog();
            LLMEngine.InvalidatePromptCache();
            if (LLMEngine.Status == SystemStatus.Ready)
                await webUI.ReloadFullChat();
            UpdateUIState();
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
            await webUI.ReloadFullChat();
        }

        private void btRawLog_Click(object sender, EventArgs e)
        {
            // Show a basic window with the ed_log content in a textbox and a close button
            using var logForm = new RawLogForm();
            ThemeManager.ApplyToForm(logForm);
            logForm.SetText(ed_log);
            logForm.SetSystemLog(LLMEngineLogSink.GetText());
            logForm.StartPosition = FormStartPosition.CenterParent;
            logForm.TopMost = true;
            logForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MemoryBrowserForm.ShowForActiveBot(this);
        }

        private void ckNatMem_CheckedChanged(object sender, EventArgs e)
        {
            if (_isinitloading)
                return;
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

        private async void button4_Click(object sender, EventArgs e)
        {
            if (Bot is null)
                return;
            var msg = Bot.Brain.BuildAwayMessage(true);
            if (msg is null)
                return;
            LLMEngine.History.LogMessage(msg);
            await webUI.LoadHistoryToUI();
        }

        private void ckToolCalls_CheckedChanged(object sender, EventArgs e)
        {
            if (_isinitloading)
                return;
            LLMEngine.Settings.ToolCallsAllowed = ckToolCalls.Checked;
        }

        private void bt_llama_Click(object sender, EventArgs e)
        {
            using var modelForm = new ModelForm();
            ThemeManager.ApplyToForm(modelForm);
            modelForm.ShowDialog(this);
        }

        private void cbMemStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.RecallMemoryMode = (MemoryMode)cbMemStyle.SelectedIndex;
        }
    }
}
