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
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using WaifuAI.Files;
using WaifuAI.Game;
using WaifuAI.Plugins;
using WaifuAI.src.forms;
using WaifuAI.Web;

namespace WaifuAI
{
    public partial class MainForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WebScraper WebScraper { get; set; } = new WebScraper();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SamplerSettings SelectedSamplerEditor { get; set; } = new SamplerSettings();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public InstructFormat SelectedInstructEditor { get; set; } = new InstructFormat();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SystemPrompt SelectedPromptEditor { get; set; } = new SystemPrompt();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WorldInfo SelectedWorldEditor { get; set; } = new WorldInfo();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WorldEntry SelectedWorldEntryEditor { get; set; } = new WorldEntry();

        private string? _currentgeneration = null;
        private int _currentgenerationtokencount = 0;
        private int _currentgencalls = 0;
        private ChatSession? _selectedSession = null;
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

        public static Character? Bot => LLMSystem.Bot as Character;
        public static Character? User => LLMSystem.User as Character;

        public static MarkdownPipeline CustomMarkDownPipeline { get; } = new MarkdownPipelineBuilder()
            .UseSoftlineBreakAsHardlineBreak().UseAdvancedExtensions()
            .UseEmojiAndSmiley()
            .UseAutoLinks()
            .Use(new QuoteColorExtension())
            .Build();

        public MainForm()
        {
            InitializeComponent();
            // load all the image files in data/backgrounds to the cb_background combobox
            foreach (var file in Directory.GetFiles("data/background"))
            {
                cb_background.Items.Add(Path.GetFileName(file));
            }

            HelptoolTip.SetToolTip(ck_webgrammar, "If checked, the LLM will be better at navigating the website, but its results will be less accurate." + Environment.NewLine + "Only enable if the LLM is consistently failing at browsing the web.");

            HelptoolTip.SetToolTip(ck_ragenabled, "Use RAG functionalities to insert summaries of relevant previous sessions based on the user's input." + Environment.NewLine + "Configurable in the Program.Settings tab.");
            HelptoolTip.SetToolTip(ck_senseoftime, "Insert day and time information to prompt when relevant to give the bot a better understanding of time.");
            HelptoolTip.SetToolTip(ck_sessionmemory, "Use a set amount of tokens (set in Program.Settings) to insert summaries of previous chat sessions with this bot." + Environment.NewLine + "This drastically increases the bot's long-term memory.");
            HelptoolTip.SetToolTip(ck_worldinfo, "Use the WorldInfo file(s) associated with this bot. WorldInfo is a list of keyword-triggered textual information that is inserted into the prompt when the conditions are met." + Environment.NewLine + "See the World Info tab for additional information.");

            HelptoolTip.SetToolTip(ck_alwayswebsearch, "Normally, Online RAG (using DuckDuckGo API search) will only be attempt if you explicitely ask the bot to search the web. If you check this box, the LLM will always try to determine if a search would be useful." + Environment.NewLine + Environment.NewLine + "May lead to many false positive, and overall slower generation with some models.");
            HelptoolTip.SetToolTip(ck_charsampler, "If checked, and when using a bot persona containing a list of compatible inference Program.Settings, the inference Program.Settings will be picked at random from that list each time the bot write a new message." + Environment.NewLine + Environment.NewLine + "Will lead to a more creative and less repetitive interaction, but also less consistent.");
            HelptoolTip.SetToolTip(ck_onlinerag, "If checked, the bot may perform a web search (using DuckDuckGo) to improve its responses when asked to.");
            HelptoolTip.SetToolTip(btEmbedAll, "If you're using RAG and have manually edited some entries in the history, press this button to update all the embeddings so RAG functionalities are accurate.");

            // Chat related events
            bt_chattosessions.Click += ConvertChatToSessionList!;
            // Load editors and chat menu
            SetupSamplerEditor();
            SetupInstructEditor();
            SetupWorldEditor();
            SetupPromptEditor();
            SetupListSessionContextMenu();
            SetupChatMenu();
            _isinitloading = false;
            _activityTimer.OnTrigger += OnBotInitiateConversation;

        }

        private async void OnBotInitiateConversation(object? sender, EventArgs e)
        {
            if (LLMSystem.Status != SystemStatus.Ready || Bot?.CanInitiateChat != true || _afkmessagecount > 2)
                return;
            _activityTimer?.Reset();
            _impersonatemode = false;
            _postdate = DateTime.Now;
            var lastusermessage = LLMSystem.History.CurrentSession.Messages.LastOrDefault(m => m.Role == AuthorRole.User);
            if (lastusermessage == null)
                return;
            var message = "The last message from {{user}} was posted " + AIToolkit.StringExtensions.TimeSpanToHumanString(DateTime.Now - lastusermessage.Date) + " ago. We're {{day}}, the {{date}} at {{time}} now. Would you like to send a message to {{user}} now? Use your best judgement based on the conversation above. In case you don't want to send a message, just respond with No. If you want to send a message, write the message to {{user}} directly while making sure it's contextually relevant. \n\nThis query will repeat every few minutes.";
            if (_afkmessagecount > 1)
                message += " You've already sent " + _afkmessagecount + " unanswered messages in a row.";
            else if (_afkmessagecount == 1)
                message += " You've already sent a message.";
            message = LLMSystem.ReplaceMacros(message);
            statusbar.Items[1].Text = "Analyzing...";
            var response = await LLMSystem.QuickInferenceForSystemPrompt(message, false);
            response = response.RemoveThinkingBlocks(LLMSystem.Instruct.ThinkingStart, LLMSystem.Instruct.ThinkingEnd).Trim();

            if (!string.IsNullOrEmpty(response) && !response.StartsWith("no", StringComparison.InvariantCultureIgnoreCase))
            {
                var msg = new SingleMessage(AuthorRole.Assistant, DateTime.Now, response, LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName);
                Bot.History.LogMessage(msg);
                _afkmessagecount++;
                await SendMessageToUI(msg, Bot.History.CurrentSession.Messages.Count - 1);
                // play a notification sound
                System.Media.SystemSounds.Question.Play();
            }
        }

        private void SetupChatMenu()
        {
            cb_bot.Items.Clear();
            cb_user.Items.Clear();
            bt_scenario.ForeColor = string.IsNullOrWhiteSpace(LLMSystem.Settings.ScenarioOverride) ? Color.Black : Color.DarkGreen;
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

            LLMSystem.Init();
            LLMSystem.ContextPlugins = [];
            LLMSystem.ContextPlugins.Add(new BrowsePlugin());
            LLMSystem.ContextPlugins.Add(new LocationPlugin("Locations"));
            LLMSystem.ContextPlugins.Add(new WebSearchPlugin());
            RAGSystem.Enabled = true;
            ck_ragenabled.Checked = RAGSystem.Enabled;
            ck_worldinfo.Checked = LLMSystem.Settings.AllowWorldInfo;
            LLMSystem.OnInferenceStreamed += OnStreamMessageReceived;
            LLMSystem.OnInferenceEnded += OnStreamInferenceEnded;
            LLMSystem.OnFullPromptReady += OnFullPromptReady;
            LLMSystem.OnStatusChanged += OnStatusChanged;

            ed_input.EnableImageDragDrop(basestr =>
            {
                LLMSystem.VLM_ClearImages();
                LLMSystem.VLM_AddB64Image(basestr);
                DisplayImage(basestr);
            }, 1024);
            pictEmbed.EnableImageDragDrop(basestr =>
            {
                LLMSystem.VLM_ClearImages();
                LLMSystem.VLM_AddB64Image(basestr);
                DisplayImage(basestr);
            }, 1024);

            // Agent Stuff
            AgentRuntime.Instance.Start();
            EventBus.Subscribe<StagedMessageReadyEvent>(e =>
            {
                BeginInvoke(new Action(() =>
                {
                    statusbar.Items[1].Text = "Background suggestion ready.";
                    // (Later) open a panel listing e.Message.Draft & allow accept/discard.
                }));
            });

        }

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

        private void OnStatusChanged(object? sender, SystemStatus e)
        {
            Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                UpdateUIState();
            });
        }

        private void UpdateUIState()
        {
            _activityTimer?.Reset();
            bt_scenario.ForeColor = string.IsNullOrWhiteSpace(LLMSystem.Settings.ScenarioOverride) ? Color.Black : Color.DarkGreen;
            if (LLMSystem.Status == SystemStatus.Ready)
            {
                bt_delete.Enabled = true;
                bt_connect.Enabled = true;
                bt_send.Enabled = true;
                bt_send.Text = "Send";
                bt_send.BackColor = Color.PaleGreen;
                bt_reroll.Enabled = true;
                bt_chattosessions.Enabled = true;
                bt_newsession.Enabled = true;
                bt_impersonate.Enabled = true;
                cb_bot.Enabled = true;
                cb_user.Enabled = true;
                ShowCurrentSessionInfo();
            }
            else if (LLMSystem.Status == SystemStatus.Busy)
            {
                bt_delete.Enabled = false;
                bt_connect.Enabled = false;
                bt_send.Enabled = true;
                bt_send.Text = "Cancel";
                bt_send.BackColor = Color.OrangeRed;
                bt_reroll.Enabled = false;
                bt_chattosessions.Enabled = false;
                bt_newsession.Enabled = false;
                bt_impersonate.Enabled = false;
                cb_bot.Enabled = false;
                cb_user.Enabled = false;
            }
            else if (LLMSystem.Status == SystemStatus.NotInit)
            {
                bt_delete.Enabled = false;
                bt_connect.Enabled = false;
                bt_send.Enabled = false;
                bt_send.Text = "Offline";
                bt_send.BackColor = Color.OrangeRed;
                bt_reroll.Enabled = false;
                bt_chattosessions.Enabled = false;
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

        private void OnFullPromptReady(object? sender, string e)
        {
            Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                ed_log.Clear();
                var text = "====== New Generation ======\n\n" + e + "\n\n";
                ed_log.Text = text.ToWinFormat();
            });
        }

        private async void OnStreamMessageReceived(object? sender, string e)
        {
            if (!_impersonatemode && !string.IsNullOrEmpty(LLMSystem.Instruct.ThinkingStart) && _currentgencalls == 1)
            {
                var thoughts = ChatRender.GetMessagePrefix(AuthorRole.Assistant) + $"*{LLMSystem.Bot.UniqueName} is thinking...*";
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
                    if (!string.IsNullOrWhiteSpace(LLMSystem.Instruct.ThinkingStart) && !string.IsNullOrEmpty(LLMSystem.Instruct.ThinkingEnd))
                    {
                        // Check if we have more than a single ThinkingEnd block, if so, we need to end the generation
                        var endcount = _currentgeneration.CountSubstring(LLMSystem.Instruct.ThinkingEnd);
                        if (endcount > 1)
                        {
                            LLMSystem.CancelGeneration();
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
                LLMSystem.InvalidatePromptCache();
            }
            else
            {
                var stringfix = Program.Settings.AsteriskCheck ? e.FixAsterisks() : e;
                if (!string.IsNullOrWhiteSpace(LLMSystem.Instruct.ThinkingEnd) && stringfix.CountSubstring(LLMSystem.Instruct.ThinkingEnd) > 1)
                {
                    stringfix = stringfix.RemoveEverythingAfterLast(LLMSystem.Instruct.ThinkingEnd);
                }

                if (Program.Settings.RemoveCutSentence)
                    stringfix = stringfix.RemoveUnfinishedSentence();
                if (Program.Settings.AntiSlop)
                    stringfix = stringfix.RemoveSlop(Program.Settings.AntiSlopList, Program.Settings.AntiSlopRatio);
                // Roleplay filter
                stringfix = stringfix.FixRoleplayString(Program.Settings.RoleplayFormatting, false);

                var MsgPrefix = ChatRender.GetMessagePrefix(AuthorRole.Assistant);
                await WebEditLastMessage(MsgPrefix + stringfix);
                var msg = LLMSystem.Bot.History.LogMessage(AuthorRole.Assistant, stringfix, LLMSystem.User, LLMSystem.Bot);
                PrepareResponse();
                if (_forcereload || Program.Settings.MaxMessagesOnScreen <= LLMSystem.History.CurrentSession.Messages.Count)
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
                if (Program.Settings.UseTTS && !string.IsNullOrEmpty(Bot?.TTSVoice) && LLMSystem.Client?.SupportsTTS == true)
                {
                    await OutputTTS(stringfix);
                }
            }
            (LLMSystem.Bot as Character)?.SaveChatHistory();
        }

        private async Task OutputTTS(string text)
        {
            var paragraphs = text.Split(["\n\n"], StringSplitOptions.RemoveEmptyEntries);

            if (paragraphs.Length == 0)
                return;

            var voiceID = Bot?.TTSVoice ?? "Waifu";
            int index = 0;

            // Start generating TTS for the first paragraph
            var currentWaveTask = LLMSystem.GenerateTTS(paragraphs[index], voiceID);
            index++;

            while (Program.Settings.UseTTS && LLMSystem.Status != SystemStatus.Busy)
            {
                // Wait for the current TTS generation to complete
                var currentWave = await currentWaveTask;

                // Start playing the current audio chunk in a background task
                var playTask = PlayAudioAsync(currentWave);

                // Generate TTS for the next paragraph while the current one is playing
                Task<byte[]>? nextWaveTask = null;
                if (index < paragraphs.Length)
                {
                    nextWaveTask = LLMSystem.GenerateTTS(paragraphs[index], voiceID);
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

        private void ShowCurrentSessionInfo()
        {
            var (tokens, duration) = LLMSystem.History.GetCurrentChatSessionInfo();
            statusbar.Items[0].Text = $"Current Session: {duration.TotalDays:F2} days ({tokens} tokens)";
        }

        // Helper method to use Invoke with async methods
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

        #region *** Sampler Editor ***

        /// <summary>
        /// Initialize the inference Program.Settings editor panel
        /// </summary>
        /// <param name="Forceid"></param>
        private void SetupSamplerEditor(string Forceid = "", bool addEvents = true)
        {
            cb_samplerlist.Items.Clear();
            foreach (var item in DataFiles.Inference)
            {
                cb_samplerlist.Items.Add(item.Value.UniqueName);
            }
            var idwant = 0;
            if (Forceid != "")
                idwant = cb_samplerlist.Items.IndexOf(Forceid);
            if (cb_samplerlist.Items.Count > 0)
            {
                cb_samplerlist.SelectedIndex = idwant;
                SelectedSamplerEditor = DataFiles.Inference[cb_samplerlist.SelectedItem!.ToString()!].GetCopy();
            }
            if (addEvents)
            {
                cb_samplerlist.SelectedIndexChanged += (sender, e) =>
                {
                    SelectedSamplerEditor = DataFiles.Inference[cb_samplerlist.SelectedItem!.ToString()!].GetCopy();
                    LoadSamplerSettings(SelectedSamplerEditor);
                };
            }
            LoadSamplerSettings(SelectedSamplerEditor);
        }

        private void LoadSamplerSettings(SamplerSettings selected)
        {
            num_temp.Value = (decimal)selected.Temperature;
            num_seed.Value = selected.Sampler_seed;
            num_topk.Value = selected.Top_k;
            num_topp.Value = (decimal)selected.Top_p;
            num_typical.Value = (decimal)selected.Typical;
            num_minp.Value = (decimal)selected.Min_p;
            num_topa.Value = (decimal)selected.Top_a;
            num_tfs.Value = (decimal)selected.Tfs;
            num_reppen.Value = (decimal)selected.Rep_pen;
            num_reppenrange.Value = selected.Rep_pen_range;
            cb_miro.SelectedIndex = (int)selected.Mirostat;
            num_meta.Value = (decimal)selected.Mirostat_eta;
            num_mtau.Value = (decimal)selected.Mirostat_tau;
            num_xtcthres.Value = (decimal)selected.Xtc_threshold;
            num_xtcprob.Value = (decimal)selected.Xtc_probability;
            num_drybase.Value = (decimal)selected.Dry_base;
            num_drymul.Value = (decimal)selected.Dry_multiplier;
            num_dryrange.Value = selected.Dry_allowed_length;
            num_dynexpo.Value = (decimal)selected.Dynatemp_exponent;
            num_dynrange.Value = (decimal)selected.Dynatemp_range;
            num_smoothfac.Value = (decimal)selected.Smoothing_factor;
            ck_ignoreeos.Checked = selected.Bypass_eos;
            ck_renderspecial.Checked = selected.Render_special;
            ck_trimstop.Checked = selected.Trim_stop;
        }

        private SamplerSettings SaveSamplerUIToSettiongs()
        {
            return new SamplerSettings()
            {
                Temperature = (double)num_temp.Value,
                Sampler_seed = (int)num_seed.Value,
                Top_k = (int)num_topk.Value,
                Top_p = (double)num_topp.Value,
                Typical = (double)num_typical.Value,
                Min_p = (double)num_minp.Value,
                Top_a = (double)num_topa.Value,
                Tfs = (double)num_tfs.Value,
                Rep_pen = (double)num_reppen.Value,
                Rep_pen_range = (int)num_reppenrange.Value,
                Mirostat = (double)cb_miro.SelectedIndex,
                Mirostat_eta = (double)num_meta.Value,
                Mirostat_tau = (int)num_mtau.Value,
                Xtc_threshold = (double)num_xtcthres.Value,
                Xtc_probability = (double)num_xtcprob.Value,
                Dry_base = (double)num_drybase.Value,
                Dry_multiplier = (double)num_drymul.Value,
                Dry_allowed_length = (int)num_dryrange.Value,
                Dynatemp_exponent = (double)num_dynexpo.Value,
                Dynatemp_range = (double)num_dynrange.Value,
                Smoothing_factor = (double)num_smoothfac.Value,
                Bypass_eos = ck_ignoreeos.Checked,
                Render_special = ck_renderspecial.Checked,
                Trim_stop = ck_trimstop.Checked,
                Sampler_order = [6, 0, 1, 3, 4, 2, 5],
                Dry_sequence_breakers = ["\n", ":", "\"", "*", "<|im_end|>", "<|im_start|>"],
                Max_context_length = 8192,
                Max_length = 512,
                Prompt = "",
                Memory = "",
                Images = []
            };
        }

        private void bt_savesampler_Click(object sender, EventArgs e)
        {
            var NewName = cb_samplerlist.Text;
            if (string.IsNullOrWhiteSpace(NewName))
            {
                MessageBox.Show("Please select a valide name for the new sampler");
                return;
            }
            // If name already exists ask for confirmation
            if (DataFiles.Inference.ContainsKey(NewName) && (MessageBox.Show("This sampler already exists, do you want to overwrite it?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No))
                return;
            SelectedSamplerEditor = SaveSamplerUIToSettiongs();
            SelectedSamplerEditor.UniqueName = NewName;
            DataFiles.Inference[NewName] = SelectedSamplerEditor;
            (SelectedSamplerEditor as IFile).SaveToFile("data/params/" + NewName + ".json");
            SetupSamplerEditor(NewName, false);

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

        #endregion

        #region *** System Prompt Editor ***

        /// <summary>
        /// Initialize the instruction format editor panel
        /// </summary>
        /// <param name="Forceid"></param>
        private void SetupPromptEditor(string Forceid = "", bool addEvents = true)
        {
            cb_promptlist.Items.Clear();
            foreach (var item in DataFiles.SysPrompts)
            {
                cb_promptlist.Items.Add(item.Value.UniqueName);
            }
            var idwant = 0;
            if (Forceid != "")
                idwant = cb_promptlist.Items.IndexOf(Forceid);
            if (cb_promptlist.Items.Count > 0)
            {
                cb_promptlist.SelectedIndex = idwant;
                SelectedPromptEditor = DataFiles.SysPrompts[cb_promptlist.SelectedItem!.ToString()!].Copy<SystemPrompt>()!;
            }
            if (addEvents)
            {
                cb_promptlist.SelectedIndexChanged += (sender, e) =>
                {
                    SelectedPromptEditor = DataFiles.SysPrompts[cb_promptlist.SelectedItem!.ToString()!].Copy<SystemPrompt>()!;
                    LoadSysPromptSettings(SelectedPromptEditor);
                };
            }
            LoadSysPromptSettings(SelectedPromptEditor);
        }

        private void LoadSysPromptSettings(SystemPrompt selected)
        {
            ed_editsys_prompt.Text = selected.Prompt.ToWinFormat();
            ed_editsys_worldinfo.Text = selected.WorldInfoTitle.Replace("\n", "\\n");
            ed_editsys_scenario.Text = selected.ScenarioTitle.Replace("\n", "\\n");
            ed_editsys_dialogs.Text = selected.DialogsTitle.Replace("\n", "\\n");
            ed_editsys_prefix.Text = selected.CategorySeparator.Replace("\n", "\\n");
        }

        private SystemPrompt SaveSysPromptUIToSettings()
        {
            return new SystemPrompt()
            {
                Prompt = ed_editsys_prompt.Text.ToLinuxFormat(),
                WorldInfoTitle = ed_editsys_worldinfo.Text.Replace("\\n", "\n"),
                ScenarioTitle = ed_editsys_scenario.Text.Replace("\\n", "\n"),
                DialogsTitle = ed_editsys_dialogs.Text.Replace("\\n", "\n"),
                CategorySeparator = ed_editsys_prefix.Text.Replace("\\n", "\n")
            };
        }

        private void bt_promptsave_Click(object sender, EventArgs e)
        {
            var NewName = cb_promptlist.Text;
            if (string.IsNullOrWhiteSpace(NewName))
            {
                MessageBox.Show("Please select a valide name for the new system prompt format.");
                return;
            }
            // If name already exists ask for confirmation
            if (DataFiles.SysPrompts.ContainsKey(NewName) && (MessageBox.Show("This prompt format already exists, do you want to overwrite it?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No))
                return;
            SelectedPromptEditor = SaveSysPromptUIToSettings();
            SelectedPromptEditor.UniqueName = NewName;
            DataFiles.SysPrompts[NewName] = SelectedPromptEditor;
            (SelectedPromptEditor as IFile).SaveToFile("data/sysprompts/" + NewName + ".json");
            SetupPromptEditor(NewName, false);
            // Update the prompt list in the chat menu
            var currselection = cb_sysprompt.SelectedItem?.ToString() ?? "";
            cb_sysprompt.Items.Clear();
            foreach (var item in DataFiles.SysPrompts)
            {
                cb_sysprompt.Items.Add(item.Value.UniqueName);
            }
            var newidx = cb_sysprompt.Items.IndexOf(currselection);
            cb_sysprompt.SelectedIndex = newidx == -1 ? 0 : newidx;
        }

        #endregion

        #region *** Other Editor Related Functions ***

        /// <summary>
        /// Initialize the instruction format editor panel
        /// </summary>
        /// <param name="Forceid"></param>
        private void SetupInstructEditor(string Forceid = "")
        {
            cb_instructlist.Items.Clear();
            foreach (var item in DataFiles.Instruct)
            {
                cb_instructlist.Items.Add(item.Value.UniqueName);
            }
            var idwant = 0;
            if (Forceid != "")
                idwant = cb_instructlist.Items.IndexOf(Forceid);
            if (cb_instructlist.Items.Count > 0)
            {
                cb_instructlist.SelectedIndex = idwant;
                SelectedInstructEditor = DataFiles.Instruct[cb_instructlist.SelectedItem!.ToString()!].Copy<InstructFormat>()!;
            }
            cb_instructlist.SelectedIndexChanged += (sender, e) =>
            {
                SelectedInstructEditor = DataFiles.Instruct[cb_instructlist.SelectedItem!.ToString()!].Copy<InstructFormat>()!;
                CreateInstructControls(pan_instruct, SelectedInstructEditor);
            };
            CreateInstructControls(pan_instruct, SelectedInstructEditor);
        }

        private void SetupWorldEditor(string ForceID = "", int forceEntry = 0)
        {
            cb_worlds.Items.Clear();
            foreach (var item in DataFiles.WorldInfos)
            {
                cb_worlds.Items.Add(item.Value.UniqueName);
            }
            var idwant = (ForceID != "") ? cb_worlds.Items.IndexOf(ForceID) : 0;
            if (cb_worlds.Items.Count > 0)
            {
                cb_worlds.SelectedIndex = idwant;
                SelectedWorldEditor = DataFiles.WorldInfos[cb_worlds.SelectedItem!.ToString()!].Copy<WorldInfo>()!;
                LoadWorldSettings(SelectedWorldEditor, forceEntry);
            }
            cb_worlds.SelectedIndexChanged += (sender, e) =>
            {
                var wid = cb_worlds.SelectedItem?.ToString();
                if (DataFiles.WorldInfos.TryGetValue(wid!, out var wi))
                {
                    SelectedWorldEditor = wi.Copy<WorldInfo>()!;
                    LoadWorldSettings(SelectedWorldEditor, forceEntry);
                }
            };
        }

        private void LoadWorldSettings(WorldInfo selectedWorldEditor, int forceEntry = 0)
        {
            ed_worlddesc.Text = selectedWorldEditor.Description;
            num_scandepth.Value = selectedWorldEditor.ScanDepth;
            ck_wiembed.Checked = selectedWorldEditor.DoEmbeds;
            lb_worldentries.Items.Clear();
            foreach (var item in selectedWorldEditor.Entries)
            {
                lb_worldentries.Items.Add(item.Name);
            }
            if (lb_worldentries.Items.Count > 0)
            {
                var selentry = forceEntry < lb_worldentries.Items.Count ? forceEntry : 0;
                SelectedWorldEntryEditor = selectedWorldEditor.Entries[selentry];
                lb_worldentries.SelectedIndex = selentry;
            }
        }

        private void lb_worldentries_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = lb_worldentries.SelectedIndex;
            if (id < 0 || SelectedWorldEditor.Entries.Count <= id)
                return;
            SelectedWorldEntryEditor = SelectedWorldEditor.Entries[id];
            LoadWorldEntry(SelectedWorldEntryEditor);
        }

        private void LoadWorldEntry(WorldEntry worldEntry)
        {
            var sv = _isinitloading;
            _isinitloading = true;
            ed_wentryname.Text = worldEntry.Name;
            ed_wentrymem.Text = worldEntry.Content.ToWinFormat();
            // Convert worldEntry's keywords to a comma separated string to show in ed_wentrykw1.Text
            ed_wentrykw1.Text = string.Join(",", worldEntry.KeyWordsMain);
            ed_wentrykw2.Text = string.Join(",", worldEntry.KeyWordsSecondary);
            num_wentryduration.Value = worldEntry.Duration;
            num_wentryposition.Value = worldEntry.PositionIndex;
            num_wentrypriority.Value = worldEntry.Priority;
            cb_wentrykwlink.SelectedIndex = (int)worldEntry.WordLink;
            cb_wentrylocation.SelectedIndex = (int)worldEntry.Position;
            ck_wentrycasesensitive.Checked = worldEntry.CaseSensitive;
            ck_wentryenabled.Checked = worldEntry.Enabled;
            numWItriggerchance.Value = (decimal)worldEntry.TriggerChance;
            _isinitloading = sv;
        }

        private void SaveWorldEntry()
        {
            SelectedWorldEntryEditor.Name = ed_wentryname.Text;
            SelectedWorldEntryEditor.Category = AIToolkit.Memory.MemoryType.WorldInfo;
            SelectedWorldEntryEditor.Insertion = AIToolkit.Memory.MemoryInsertion.Trigger;
            SelectedWorldEntryEditor.Content = ed_wentrymem.Text.ToLinuxFormat();
            if (!string.IsNullOrWhiteSpace(ed_wentrykw1.Text))
            {
                SelectedWorldEntryEditor.KeyWordsMain = ed_wentrykw1.Text.Split(',')?.ToList() ?? [];
            }
            else
            {
                SelectedWorldEntryEditor.KeyWordsMain = [];
            }
            if (!string.IsNullOrWhiteSpace(ed_wentrykw2.Text))
            {
                SelectedWorldEntryEditor.KeyWordsSecondary = ed_wentrykw2.Text.Split(',')?.ToList() ?? [];
            }
            else
            {
                SelectedWorldEntryEditor.KeyWordsSecondary = [];
            }
            SelectedWorldEntryEditor.Duration = (int)num_wentryduration.Value;
            SelectedWorldEntryEditor.PositionIndex = (int)num_wentryposition.Value;
            SelectedWorldEntryEditor.Priority = (int)num_wentrypriority.Value;
            SelectedWorldEntryEditor.WordLink = (KeyWordLink)cb_wentrykwlink.SelectedIndex;
            SelectedWorldEntryEditor.Position = (WEPosition)cb_wentrylocation.SelectedIndex;
            SelectedWorldEntryEditor.CaseSensitive = ck_wentrycasesensitive.Checked;
            SelectedWorldEntryEditor.Enabled = ck_wentryenabled.Checked;
            SelectedWorldEntryEditor.TriggerChance = (float)numWItriggerchance.Value;

            var idx = SelectedWorldEditor.Entries.IndexOf(SelectedWorldEntryEditor);
            if (idx >= 0 && idx < lb_worldentries.Items.Count)
                lb_worldentries.Items[idx] = SelectedWorldEntryEditor.Name;
        }

        private async Task<bool> SaveWorldInfo()
        {
            SelectedWorldEditor.Description = ed_worlddesc.Text;
            SelectedWorldEditor.ScanDepth = (int)num_scandepth.Value;
            SelectedWorldEditor.DoEmbeds = ck_wiembed.Checked;
            var NewName = cb_worlds.Text;
            if (string.IsNullOrWhiteSpace(NewName))
            {
                MessageBox.Show("Please select a valide name for the new sampler");
                return false;
            }
            // If name already exists ask for confirmation
            if (DataFiles.Inference.ContainsKey(NewName) && (MessageBox.Show("This sampler already exists, do you want to overwrite it?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No))
                return false;
            await SelectedWorldEditor.EmbedText();
            SelectedWorldEditor.UniqueName = NewName;
            DataFiles.WorldInfos[NewName] = SelectedWorldEditor;

            (SelectedWorldEditor as IFile).SaveToFile("data/worlds/" + NewName + ".json");
            return true;
        }

        private void UpdateWorldEntryEvent(object sender, EventArgs e)
        {
            if (_isinitloading)
                return;
            SaveWorldEntry();
        }

        /// <summary>
        /// Create the editor for the instruction format Program.Settings
        /// </summary>
        /// <param name="target"></param>
        /// <param name="instructsetting"></param>
        private static void CreateInstructControls(Control target, InstructFormat instructsetting)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8605 // Converting null literal or possible null value to non-nullable type.
            target.Controls.Clear();
            int yPos = 10;
            Type type = typeof(InstructFormat);
            PropertyInfo[] properties = type.GetProperties();
            string[] ignore = ["UniqueName"];

            var lst = new List<PropertyInfo>(properties);
            // sort lst to match the order in InstructFormat.Properties

            foreach (var propertyName in InstructFormat.Properties)
            {
                if (ignore.Contains(propertyName))
                    continue;
                var property = lst.Find(p => p.Name == propertyName);
                if (property == null)
                    continue;
                Label label = new() { Text = property.Name + ":", Location = new Point(10, yPos), Width = 240 };
                Control? control = null;
                if (property.PropertyType == typeof(int))
                {
                    control = new NumericUpDown { Minimum = -1, Maximum = int.MaxValue, Value = (int)property.GetValue(instructsetting), Location = new System.Drawing.Point(150, yPos), Width = 100 };
                    ((NumericUpDown)control).ValueChanged += (sender, e) => property.SetValue(instructsetting, (int)((NumericUpDown)control).Value);
                }
                else if (property.PropertyType == typeof(double))
                {
                    control = new NumericUpDown { Value = (decimal)(double)property.GetValue(instructsetting), Location = new Point(150, yPos), Width = 100, DecimalPlaces = 2, Increment = 0.01M };
                    ((NumericUpDown)control).ValueChanged += (sender, e) => property.SetValue(instructsetting, (double)((NumericUpDown)control).Value);
                }
                else if (property.PropertyType == typeof(string))
                {
                    control = new TextBox { Text = ((string)property.GetValue(instructsetting)!).Replace("\n", "\\n"), Location = new Point(150, yPos), Width = 400 };
                    ((TextBox)control).TextChanged += (sender, e) => property.SetValue(instructsetting, ((TextBox)control).Text.Replace("\\n", "\n"));
                }
                else if (property.PropertyType == typeof(bool))
                {
                    control = new CheckBox { Checked = (bool)property.GetValue(instructsetting), Location = new Point(150, yPos) };
                    ((CheckBox)control).CheckedChanged += (sender, e) => property.SetValue(instructsetting, ((CheckBox)control).Checked);
                }
                else if (property.PropertyType == typeof(ICollection<int>))
                {
                    control = new TextBox { Text = string.Join(",", (ICollection<int>)property.GetValue(instructsetting)!), Location = new Point(150, yPos), Width = 400 };
                    ((TextBox)control).TextChanged += (sender, e) => property.SetValue(instructsetting, ((TextBox)control).Text.Split(',').Select(int.Parse).ToList());
                }
                else if (property.PropertyType == typeof(List<string>))
                {
                    control = new TextBox { Text = string.Join(",", (List<string>)property.GetValue(instructsetting) ?? []), Location = new Point(150, yPos), Width = 400 };
                    ((TextBox)control).TextChanged += (sender, e) => property.SetValue(instructsetting, ((TextBox)control).Text.Split(',').ToList());
                }

                if (control != null)
                {
                    label.Location = new Point(10, yPos);
                    control.Location = new Point(250, yPos);
                    target.Controls.Add(label);
                    target.Controls.Add(control);
                    yPos += 30;
                }
            }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8605 // Converting null literal or possible null value to non-nullable type.
        }

        private void bt_instructsave_Click(object sender, EventArgs e)
        {
            var NewName = cb_instructlist.Text;
            if (string.IsNullOrWhiteSpace(NewName))
            {
                MessageBox.Show("Please select a valide name for the new instruction format.");
                return;
            }
            // If name already exists ask for confirmation
            if (DataFiles.Instruct.ContainsKey(NewName) && (MessageBox.Show("This instruction format already exists, do you want to overwrite it?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No))
                return;
            SelectedInstructEditor.UniqueName = NewName;
            DataFiles.Instruct[NewName] = SelectedInstructEditor;
            (SelectedInstructEditor as IFile).SaveToFile("data/instruct/" + NewName + ".json");
            SetupInstructEditor(NewName);
            // Update the prompt list in the chat menu
            var currselection = cb_instruct.Text;
            cb_instruct.Items.Clear();
            foreach (var item in DataFiles.Instruct)
            {
                cb_instruct.Items.Add(item.Value.UniqueName);
            }
            var newidx = cb_instruct.Items.IndexOf(currselection);
            cb_instruct.SelectedIndex = newidx == -1 ? 0 : newidx;
        }

        #endregion

        #region *** Main Chat Functions ***

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
            if (LLMSystem.Status == SystemStatus.Busy)
                return;
            statusbar.Items[1].Text = "Analyzing...";
            _postdate = DateTime.Now;
            _impersonatemode = true;
            PrepareResponse();
            ed_input.Text = string.Empty;
            await LLMSystem.ImpersonateUser();
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
                return (new SingleMessage(AuthorRole.System, DateTime.Now, LLMSystem.ReplaceMacros(response), LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName), usercmdonly);
            }
            return (null, usercmdonly);
        }

        private async void SendMessage(object sender, EventArgs e)
        {
            ForceCloseEditMenu();
            _activityTimer?.Reset();
            _afkmessagecount = 0;
            if (LLMSystem.Status == SystemStatus.Busy)
            {
                LLMSystem.CancelGeneration();
                return;
            }
            _impersonatemode = false;
            _postdate = DateTime.Now;
            statusbar.Items[1].Text = "Analyzing...";
            UseCharacterDefinedSampler();
            if (!string.IsNullOrEmpty(ed_input.Text))
            {
                var messagetext = LLMSystem.ReplaceMacros(LLMSystem.GetAwayString() + ed_input.Text.ToLinuxFormat(), LLMSystem.User, LLMSystem.Bot);
                var msg = new SingleMessage(AuthorRole.User, DateTime.Now, messagetext, LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName);

                if (ed_input.Text.StartsWith("/sys "))
                {
                    msg.Role = AuthorRole.System;
                    // remove the /sys prefix
                    msg.Message = msg.Message[5..].Trim();
                    await SendMessageToUI(msg, Bot!.History.CurrentSession.Messages.Count);
                    // ready a new message for the bot's response
                    PrepareResponse();
                    await SendMessageToUI(
                        new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMSystem.Bot.UniqueName + " is reading your message...*", LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName), Bot.History.CurrentSession.Messages.Count + 1);
                    ed_input.Text = string.Empty;
                    await LLMSystem.SendMessageToBot(msg);
                }
                else if (ed_input.Text.StartsWith("/game "))
                {
                    // remove the /sys prefix
                    var msgpath = msg.Message[6..].Trim();
                    _renpyDialogHandler = new RenPyDialogHandler(msgpath, "Slay The Princess");
                    var message = new SingleMessage(AuthorRole.System, DateTime.Now, "*Game Loaded: Slay The Princess*", LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName, false);
                    await SendMessageToUI(message);
                    LLMSystem.Bot.History.LogMessage(message);
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
                        msg.Message = $"**{User?.Name ?? "User"}'s Comment**" + LLMSystem.NewLine + extra + LLMSystem.NewLine + LLMSystem.NewLine + gameinfo.ShowFullScreen();
                    }
                    else
                    {
                        msg.Message = gameinfo.ShowFullScreen();
                    }
                    await SendMessageToUI(msg, Bot!.History.CurrentSession.Messages.Count);
                    // ready a new message for the bot's response
                    PrepareResponse();
                    await SendMessageToUI(
                        new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMSystem.Bot.UniqueName + " is reading your message...*", LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName), Bot.History.CurrentSession.Messages.Count + 1);
                    ed_input.Text = string.Empty;
                    await LLMSystem.SendMessageToBot(msg);
                }
                else if (ed_input.Text.StartsWith("/pick ") && _renpyDialogHandler != null)
                {
                    var select = msg.Message[6..].Trim();
                    var id = int.TryParse(select, out var test) ? test : 0;
                    var gameinfo = _renpyDialogHandler.MakeChoice(id);

                    msg.Role = AuthorRole.System;
                    // remove the /sys prefix
                    msg.Message = gameinfo;
                    await SendMessageToUI(msg, Bot!.History.CurrentSession.Messages.Count);
                    // ready a new message for the bot's response
                    PrepareResponse();
                    await SendMessageToUI(
                        new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMSystem.Bot.UniqueName + " is reading your message...*", LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName), Bot.History.CurrentSession.Messages.Count + 1);
                    ed_input.Text = string.Empty;
                    await LLMSystem.SendMessageToBot(msg);
                }
                else if (ed_input.Text.StartsWith("/dialogs") && _renpyDialogHandler != null)
                {
                    var gameinfo = _renpyDialogHandler.Continue();
                    msg.Role = AuthorRole.System;
                    // remove the /sys prefix
                    msg.Message = gameinfo.ShowDialogs();
                    await SendMessageToUI(msg, Bot!.History.CurrentSession.Messages.Count);
                    // ready a new message for the bot's response
                    PrepareResponse();
                    await SendMessageToUI(
                        new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMSystem.Bot.UniqueName + " is reading your message...*", LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName), Bot.History.CurrentSession.Messages.Count + 1);
                    ed_input.Text = string.Empty;
                    await LLMSystem.SendMessageToBot(msg);
                }
                else
                {
                    var (response, usercmdonly) = ProcessSlashCommands(messagetext);
                    if (response != null)
                    {
                        if (usercmdonly)
                        {
                            LLMSystem.History.LogMessage(response);
                            await SendMessageToUI(response, LLMSystem.History.CurrentSession.Messages.Count - 1);
                            ed_input.Text = string.Empty;
                            statusbar.Items[1].Text = "Ready!";
                            return;
                        }
                        else
                        {
                            LLMSystem.History.LogMessage(msg);
                            await SendMessageToUI(msg, LLMSystem.History.CurrentSession.Messages.Count - 1);
                            await SendMessageToUI(response, LLMSystem.History.CurrentSession.Messages.Count);
                            PrepareResponse();
                            await SendMessageToUI(
                                new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMSystem.Bot.UniqueName + " is reading your message...*", LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName), LLMSystem.History.CurrentSession.Messages.Count + 1);
                            ed_input.Text = string.Empty;
                            await LLMSystem.SendMessageToBot(response);
                        }
                    }
                    else
                    {
                        await SendMessageToUI(msg, LLMSystem.History.CurrentSession.Messages.Count);
                        // ready a new message for the bot's response
                        PrepareResponse();
                        await SendMessageToUI(
                            new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMSystem.Bot.UniqueName + " is reading your message...*", LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName), LLMSystem.History.CurrentSession.Messages.Count + 1);
                        ed_input.Text = string.Empty;
                        await LLMSystem.SendMessageToBot(msg);
                    }
                }
            }
            else
            {
                // ready a new message for the bot's response
                PrepareResponse();
                await SendMessageToUI(
                    new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMSystem.Bot.UniqueName + " is reading your message...*", LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName), LLMSystem.History.CurrentSession.Messages.Count);
                ed_input.Text = string.Empty;
                await LLMSystem.AddBotMessage();
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
            if (LLMSystem.Status == SystemStatus.Busy || LLMSystem.History.CurrentSession.Messages.Count == 0 || LLMSystem.History.LastMessage()?.Role != AuthorRole.Assistant)
                return;
            _activityTimer?.Reset();
            _impersonatemode = false;
            _postdate = DateTime.Now;
            statusbar.Items[1].Text = "Analyzing...";
            UseCharacterDefinedSampler();
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
            await WebRemoveLastMessage();
            await SendMessageToUI(new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMSystem.Bot.UniqueName + " is reading your message...*", LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName), LLMSystem.History.CurrentSession.Messages.Count - 1);
            PrepareResponse();
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
            await LLMSystem.RerollLastMessage();
        }

        private async void Connect(object sender, EventArgs e)
        {
            await LLMSystem.Connect();
            num_maxcontext.Maximum = LLMSystem.MaxContextLength;
            num_maxcontext.Value = LLMSystem.MaxContextLength;
            grp_model.Text = LLMSystem.CurrentModel;
            ck_ttstoggle.Enabled = LLMSystem.SupportsTTS;
            ck_onlinerag.Enabled = LLMSystem.SupportsWebSearch;
        }

        private async void StartNewSession(object sender, EventArgs e)
        {
            // Check if we're in a past sessions, if so, ask if the user wants to update the archive before going back to the current session
            if (LLMSystem.History.CurrentSessionID != -1 && LLMSystem.History.CurrentSessionID != LLMSystem.History.Sessions.Count - 1)
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

                LLMSystem.OnQuickInferenceEnded += (s, e) =>
                {
                    loadingForm.AddProgress(50);
                };
                await LLMSystem.History.StartNewChatSession(true);
                if (LLMSystem.Bot.SelfEditTokens > 0)
                {
                    loadingForm.SetMessage("Updating dynamic character (this might take a few minutes).");
                }
                else
                {
                    loadingForm.SetMessage("Saving history.");
                    loadingForm.SetProgress(95);
                }
                (LLMSystem.Bot as Character)?.SaveChatHistory();
                await LLMSystem.Bot.UpdateSelfEditSection();
                if (!string.IsNullOrEmpty(LLMSystem.Bot.UniqueName))
                    (LLMSystem.Bot as IFile).SaveToFile("data/chars/" + LLMSystem.Bot.UniqueName + ".json");
                loadingForm.SetMessage("Loading new session.");
                loadingForm.SetProgress(100);
                LLMSystem.RemoveQuickInferenceEventHandler();
                await WebChatLoad();
                LoadChatHistoryTab();
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
                    LLMSystem.OnQuickInferenceEnded += (s, e) =>
                    {
                        loadingForm.AddProgress(20);
                    };
                    await LLMSystem.History.CurrentSession.UpdateSession();
                    LLMSystem.RemoveQuickInferenceEventHandler();
                }
                loadingForm.SetMessage("Loading current session.");
                loadingForm.SetProgress(95);
                LLMSystem.History.CurrentSessionID = -1;
                (LLMSystem.Bot as Character)?.SaveChatHistory();
                await WebChatLoad();
                LoadChatHistoryTab();
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
            if (LLMSystem.Status == SystemStatus.Busy || LLMSystem.History.CurrentSession.Messages.Count == 0)
                return;
            LLMSystem.History.RemoveLast();
            LLMSystem.InvalidatePromptCache();
            await WebRemoveLastMessage();
        }

        private async Task LoadHistoryToUI()
        {
            await WebChatLoad();
        }

        private async Task SendMessageToUI(SingleMessage singleMessage, int index = -1)
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

            if (singleMessage.Role == AuthorRole.Assistant && !string.IsNullOrEmpty(LLMSystem.Instruct.ThinkingStart))
            {
                coremsg = $@"
                    <div class='portrait'>
                        <img src='https://appassets.test/img/{img}' alt='Portrait' width='60'>
                    </div>
                    <div class='message-content'>
                        <div class='thinking-box'>
                            <div class='thinking-header' onclick='this.parentElement.classList.toggle(""expanded"")'>
                                {LLMSystem.Bot.Name} is thinking... (click to expand)
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
            var script = $"addHtmlAfterLastChatMessage(\"{coremsg}\", {index});";
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

        #region *** Program.Settings Tab Functions ***

        private void LoadSettings()
        {
            if (!File.Exists("settings.json"))
            {
                Program.Settings = new WaifuSettings();
                File.WriteAllText("settings.json", JsonConvert.SerializeObject(Program.Settings, Formatting.Indented));
            }

            var str = File.ReadAllText("settings.json");
            Program.Settings = JsonConvert.DeserializeObject<WaifuSettings>(str)!;
            LLMSystem.Settings = Program.Settings;
            var saveinit = _isinitloading;
            _isinitloading = true;
            LLMSystem.MaxContextLength = Program.Settings.MaxTotalTokens;

            WebSearchAPI.SearchAPI = Program.Settings.SearchAPI;
            WebSearchAPI.SearchDetailedResults = Program.Settings.SearchDetailedResults;
            WebSearchAPI.BraveAPIKey = Program.Settings.BraveAPIKey;
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
            num_memtokens.Value = Program.Settings.ReservedSessionTokens;
            switch (LLMSystem.Settings.RAGHeuristic)
            {
                case HNSW.Net.NeighbourSelectionHeuristic.SelectSimple:
                    cb_ragheuristic.SelectedIndex = 1;
                    break;
                case HNSW.Net.NeighbourSelectionHeuristic.SelectHeuristic:
                    cb_ragheuristic.SelectedIndex = 0;
                    break;
                default:
                    break;
            }
            num_ragcutoff.Value = (decimal)Program.Settings.RAGDistanceCutOff;
            num_ragmaxretrieve.Value = Program.Settings.RAGMaxEntries;
            num_ragindex.Value = Program.Settings.RAGIndex;
            cb_background.SelectedIndex = cb_background.Items.IndexOf(Program.Settings.BackgroundFile);
            num_fontsize.Value = Program.Settings.FontSize;
            num_msgcount.Value = Program.Settings.MaxMessagesOnScreen;
            ck_alwayswebsearch.Checked = Program.Settings.AlwaysWebSearchQuery;
            ck_ttstoggle.Checked = Program.Settings.UseTTS;
            ck_fixasterix.Checked = Program.Settings.AsteriskCheck;
            ck_antislop.Checked = Program.Settings.AntiSlop;
            num_antislopchance.Value = (decimal)Program.Settings.AntiSlopRatio;
            ck_webkeyword.Checked = Program.Settings.WebsitePluginUseKeywords;
            ck_webgrammar.Checked = Program.Settings.WebsitePluginGrammar;
            ck_unbold.Checked = Program.Settings.RoleplayFormatting.RemoveAllBoldedText;
            ck_noemphasisword.Checked = Program.Settings.RoleplayFormatting.RemoveSingleWorldEmphasis;
            ck_noquotes.Checked = Program.Settings.RoleplayFormatting.RemoveAllQuotes;
            ck_fixquotes.Checked = Program.Settings.RoleplayFormatting.FixQuotes;
            ck_reduceitalic.Checked = Program.Settings.RoleplayFormatting.RemoveItalic;
            num_italicratio.Value = (decimal)Program.Settings.RoleplayFormatting.RemoveItalicRatio;
            num_removeitalicmaxword.Value = Program.Settings.RoleplayFormatting.RemoveItalicMaxWords;
            cb_pastsession.SelectedIndex = (int)Program.Settings.SessionHandling;
            ck_sysrag.Checked = Program.Settings.RAGMoveToSysPrompt;
            ck_remlastsentence.Checked = Program.Settings.RemoveCutSentence;
            ck_oneparagraph.Checked = Program.Settings.StopGenerationOnFirstParagraph;
            ed_sloplist.Text = Program.Settings.AntiSlopList.Length > 0 ? string.Join(",", Program.Settings.AntiSlopList) : string.Empty;
            ck_disablethink.Checked = Program.Settings.DisableThinking;
            ck_ragtothink.Checked = Program.Settings.RAGMoveToThinkBlock;
            ck_agentmode.Checked = Program.Settings.AgentEnabled;


            if (LLMSystem.ContextPlugins.Find(x => x.PluginID == "WebSearch") is WebSearchPlugin searchplug)
            {
                searchplug.KeywordDetection = !ck_alwayswebsearch.Checked;
            }

            if (LLMSystem.ContextPlugins.Find(e => e is BrowsePlugin) is BrowsePlugin webplug)
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
                Program.Settings.FontSize = (int)num_fontsize.Value;
                Program.Settings.MaxMessagesOnScreen = (int)num_msgcount.Value;
                Program.Settings.BackgroundFile = cb_background.SelectedItem?.ToString() ?? "bedroom_cozy.jpg";
                Program.Settings.AlwaysWebSearchQuery = ck_alwayswebsearch.Checked;
                Program.Settings.UseTTS = ck_ttstoggle.Checked;
                Program.Settings.SessionHandling = cb_pastsession.SelectedIndex == -1 ? SessionHandling.FitAll : (SessionHandling)cb_pastsession.SelectedIndex;
                Program.Settings.AsteriskCheck = ck_fixasterix.Checked;
                Program.Settings.AntiSlop = ck_antislop.Checked;
                Program.Settings.AntiSlopRatio = (float)num_antislopchance.Value;
                Program.Settings.AntiSlopList = !string.IsNullOrEmpty(ed_sloplist.Text) ? ed_sloplist.Text.Split(',') : [];
                Program.Settings.RoleplayFormatting.RemoveAllBoldedText = ck_unbold.Checked;
                Program.Settings.RoleplayFormatting.RemoveSingleWorldEmphasis = ck_noemphasisword.Checked;
                Program.Settings.RoleplayFormatting.RemoveAllQuotes = ck_noquotes.Checked;
                Program.Settings.RoleplayFormatting.FixQuotes = ck_fixquotes.Checked;
                Program.Settings.RoleplayFormatting.RemoveItalic = ck_reduceitalic.Checked;
                Program.Settings.RoleplayFormatting.RemoveItalicRatio = (float)num_italicratio.Value;
                Program.Settings.RoleplayFormatting.RemoveItalicMaxWords = (int)num_removeitalicmaxword.Value;
                Program.Settings.RemoveCutSentence = ck_remlastsentence.Checked;
                Program.Settings.StopGenerationOnFirstParagraph = ck_oneparagraph.Checked;
                Program.Settings.WebsitePluginUseKeywords = ck_webkeyword.Checked;
                Program.Settings.WebsitePluginGrammar = ck_webgrammar.Checked;
                Program.Settings.AgentEnabled = ck_agentmode.Checked;

                var str = JsonConvert.SerializeObject(Program.Settings, Formatting.Indented);
                File.WriteAllText("settings.json", str);
                if (LLMSystem.ContextPlugins.Find(x => x.PluginID == "WebSearch") is WebSearchPlugin searchplug)
                {
                    searchplug.KeywordDetection = !ck_alwayswebsearch.Checked;
                }

                if (LLMSystem.ContextPlugins.Find(e => e is BrowsePlugin) is BrowsePlugin webplug)
                {
                    webplug.EnforceCorrectGrammar = Program.Settings.WebsitePluginGrammar;
                    webplug.KeywordDetection = Program.Settings.WebsitePluginUseKeywords;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving Program.Settings: {ex.Message}");
            }
        }

        private async void ConvertChatToSessionList(object sender, EventArgs e)
        {
            LLMSystem.History.DivideChatIntoSessions();
            await LLMSystem.History.UpdateAllSessions();
            await WebChatLoad();
        }

        private void bt_ImportSTChat_Click(object sender, EventArgs e)
        {
            // Open a file selection dialog and use Tools.Import to import a chatlog from a jsonl file
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                MessageBox.Show(
                    ImportTools.ImportChatlog(openFileDialog1.FileName, "exported_chat.json", LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName) ?
                        "Chatlog imported successfully to exported_chat.json in this application's main folder." :
                        "Something went wrong while opening or parsing the file."
                );
        }

        private void bt_importworld_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                MessageBox.Show(
                    ImportTools.ImportWorld(openFileDialog1.FileName, "exported_world.json") ?
                        "WorldInfo imported successfully to exported_world.json in this application's main folder." :
                        "Something went wrong while opening or parsing the file."
                );
        }

        private void ApplyRAGSettings(object sender, EventArgs e)
        {
            LLMSystem.Settings.RAGDistanceCutOff = (float)num_ragcutoff.Value;
            LLMSystem.Settings.RAGMaxEntries = (int)num_ragmaxretrieve.Value;
            LLMSystem.Settings.RAGIndex = (int)num_ragindex.Value;
            LLMSystem.Settings.RAGMoveToSysPrompt = ck_sysrag.Checked;
            if (cb_ragheuristic.SelectedIndex == 0)
                LLMSystem.Settings.RAGHeuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectHeuristic;
            else if (cb_ragheuristic.SelectedIndex == 1)
                LLMSystem.Settings.RAGHeuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectSimple;
            RAGSystem.ApplySettings();
            SaveSettings();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_ragheuristic.SelectedIndex == 0)
                LLMSystem.Settings.RAGHeuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectHeuristic;
            else if (cb_ragheuristic.SelectedIndex == 1)
                LLMSystem.Settings.RAGHeuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectSimple;
        }

        private void num_ragcutoff_ValueChanged(object sender, EventArgs e)
        {
            LLMSystem.Settings.RAGDistanceCutOff = (float)num_ragcutoff.Value;
        }

        private void num_ragmaxretrieve_ValueChanged(object sender, EventArgs e)
        {
            LLMSystem.Settings.RAGMaxEntries = (int)num_ragmaxretrieve.Value;
        }

        private void ck_ragenabled_CheckedChanged(object sender, EventArgs e)
        {
            RAGSystem.Enabled = ck_ragenabled.Checked;
        }

        private void num_ragindex_ValueChanged(object sender, EventArgs e)
        {
            LLMSystem.Settings.RAGIndex = (int)num_ragindex.Value;
        }

        private async void num_fontsize_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.FontSize = (int)num_fontsize.Value;
            if (!_isinitloading)
                await WebChatLoad();
        }

        private async void cb_background_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Settings.BackgroundFile = cb_background.SelectedItem?.ToString() ?? "bedroom_cozy.jpg";
            if (!_isinitloading)
                await WebChatLoad();
        }

        private void ck_fixasterix_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.AsteriskCheck = ck_fixasterix.Checked;
        }

        private void ck_antislop_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.AntiSlop = ck_antislop.Checked;
        }

        private void num_antislopchance_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.AntiSlopRatio = (float)num_antislopchance.Value;
        }

        private void ed_sloplist_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.AntiSlopList = !string.IsNullOrEmpty(ed_sloplist.Text) ? ed_sloplist.Text.Split(',') : [];
        }

        private void ck_webkeyword_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isinitloading)
                SaveSettings();
        }

        private void ck_unbold_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.RemoveAllBoldedText = ck_unbold.Checked;
        }

        private void ck_noquotes_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.RemoveAllQuotes = ck_noquotes.Checked;
        }

        private void ck_fixquotes_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.FixQuotes = ck_fixquotes.Checked;

        }

        private void ck_noemphasisword_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.RemoveSingleWorldEmphasis = ck_noemphasisword.Checked;
        }

        private void ck_oneparagraph_CheckedChanged(object sender, EventArgs e)
        {
            LLMSystem.Settings.StopGenerationOnFirstParagraph = ck_oneparagraph.Checked;
        }

        private void ck_remlastsentence_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.RemoveCutSentence = ck_remlastsentence.Checked;
        }

        private void ck_reduceitalic_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.RemoveItalic = ck_reduceitalic.Checked;
        }

        private void num_italicratio_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.RemoveItalicRatio = (float)num_italicratio.Value;
        }

        private void cb_pastsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isinitloading || cb_pastsession.SelectedIndex == -1)
                return;
            LLMSystem.Settings.SessionHandling = (SessionHandling)cb_pastsession.SelectedIndex;
        }

        #endregion

        #region *** Chat History Tab Functions ***

        private void SetupListSessionContextMenu()
        {
            // Create context menu
            ContextMenuStrip contextMenu = new();

            // Add menu items
            ToolStripMenuItem switchSessionItem = new("Set Session As Active");
            switchSessionItem.Click += async (sender, e) => await SwitchToSelectedSession();

            ToolStripMenuItem insertSessionItem = new("Insert New Session Below");
            insertSessionItem.Click += async (sender, e) => await InsertSessionAfterSelected();

            ToolStripMenuItem deleteSessionItem = new("Delete Selected Session");
            deleteSessionItem.Click += async (sender, e) => await DeleteSelectedSession();

            ToolStripMenuItem CheckRPItem = new("Recheck if RP");
            CheckRPItem.Click += async (sender, e) => await RefreshRPInfo();

            // Add items to menu
            contextMenu.Items.Add(switchSessionItem);
            contextMenu.Items.Add(insertSessionItem);
            contextMenu.Items.Add(deleteSessionItem);
            contextMenu.Items.Add(CheckRPItem);

            // Attach opening event to control items' visibility based on selection
            contextMenu.Opening += (sender, e) =>
            {
                bool hasSelection = listSession.SelectedItems.Count > 0;
                switchSessionItem.Enabled = hasSelection;
                insertSessionItem.Enabled = hasSelection;
                deleteSessionItem.Enabled = hasSelection;
                CheckRPItem.Enabled = hasSelection;

                // Cancel opening if no items selected
                if (!hasSelection)
                    e.Cancel = true;
            };

            // Assign menu to ListView
            listSession.ContextMenuStrip = contextMenu;
        }

        private async Task SwitchToSelectedSession()
        {
            if (_selectedSession == null)
                return;

            LLMSystem.Bot.History.CurrentSessionID = LLMSystem.Bot.History.Sessions.IndexOf(_selectedSession);
            _activityTimer?.Reset();
            LoadChatHistoryTab();
            await WebChatLoad();
        }

        private async Task InsertSessionAfterSelected()
        {
            if (_selectedSession == null)
                return;

            LLMSystem.Bot.History.CurrentSessionID = LLMSystem.Bot.History.Sessions.IndexOf(_selectedSession);
            var id = LLMSystem.Bot.History.CurrentSessionID;
            if (id == LLMSystem.Bot.History.Sessions.Count - 1)
            {
                LLMSystem.Bot.History.Sessions.Add(new ChatSession());
            }
            else
            {
                LLMSystem.Bot.History.Sessions.Insert(id + 1, new ChatSession());
            }
            LLMSystem.Bot.History.CurrentSessionID++;
            _activityTimer?.Reset();
            _selectedSession = LLMSystem.History.CurrentSession;
            await LLMSystem.History.StartNewChatSession(true);
            await WebChatLoad();
            LoadChatHistoryTab();
        }


        private async Task RefreshRPInfo()
        {
            if (_selectedSession == null)
                return;
            _selectedSession.MetaData.IsRoleplaySession = await _selectedSession.IsRoleplay();
            DisplaySessionDetails(_selectedSession);
        }

        private async Task DeleteSelectedSession()
        {
            if (_selectedSession == null)
                return;

            if (LLMSystem.History.Sessions.Count <= 1)
            {
                bt_deleteAllHistory_Click(this, EventArgs.Empty);
                return;
            }

            if (MessageBox.Show("This will delete the selected session permanently. Are you sure?",
                              "Delete Session?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var needchangesession = LLMSystem.History.CurrentSession == _selectedSession;
                LLMSystem.History.Sessions.Remove(_selectedSession);
                if (needchangesession)
                {
                    LLMSystem.History.CurrentSessionID = LLMSystem.History.Sessions.Count - 1;
                }
            }

            _activityTimer?.Reset();
            _selectedSession = LLMSystem.History.CurrentSession;
            DisplaySessionDetails(_selectedSession);
            LoadChatHistoryTab();
            await WebChatLoad();
        }

        public void LoadChatHistoryTab()
        {
            listSession.Items.Clear();
            if (LLMSystem.History.Sessions.Count == 0)
                return;
            foreach (var session in LLMSystem.History.Sessions)
            {
                var item = new ListViewItem([session.Title, session.StartTime.ToString("g")])
                {
                    Tag = session
                };
                if (LLMSystem.History.Sessions.IndexOf(session) == LLMSystem.History.CurrentSessionID)
                {
                    item.Font = new Font(item.Font, FontStyle.Bold);
                }
                if (session.Sticky)
                {
                    item.ForeColor = Color.Red;
                }
                listSession.Items.Add(item);
            }
        }

        private void listSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSession.SelectedItems.Count <= 0)
                return;
            var selectedItem = listSession.SelectedItems[0];
            _selectedSession = (ChatSession)selectedItem.Tag!;
            DisplaySessionDetails(_selectedSession);
        }

        private async void DisplaySessionDetails(ChatSession session)
        {
            var sv = _isinitloading;
            _isinitloading = true;
            ed_sessiontitle.Text = session.Title;
            ed_sessioninfo.Text = session.Summary.ToWinFormat();
            lbl_sessiondata.Text = session.StartTime.ToString("g") + " - " + session.EndTime.ToString("g") + " - " + session.Messages.Count + " messages";

            ed_hist_kw1.Text = string.Join(",", session.KeyWordsMain);
            ed_hist_kw2.Text = string.Join(",", session.KeyWordsSecondary);
            cb_hist_kwlink.SelectedIndex = (int)session.WordLink;
            ck_hist_casesensitive.Checked = session.CaseSensitive;
            ck_hist_kw.Checked = session.Enabled;
            ck_hist_sticky.Checked = session.Sticky;
            ck_hist_isrp.Checked = session.MetaData.IsRoleplaySession;
            _isinitloading = sv;

            if (web_sessioncontent.CoreWebView2 == null)
            {
                await web_sessioncontent.EnsureCoreWebView2Async();
            }
            var dialogs = session.GetRawDialogs(int.MaxValue, false).Replace("\n", "\n\n");

            var res = new StringBuilder();
            res.AppendLinuxLine($"# {session.Title}").AppendLinuxLine();
            res.AppendLinuxLine("## Summary:").AppendLinuxLine().AppendLinuxLine(session.Summary).AppendLinuxLine();
            res.AppendLinuxLine("## Keywords: ");
            foreach (var item in session.MetaData.Keywords)
            {
                res.Append(item + ", ");
            }
            res.AppendLinuxLine();
            res.AppendLinuxLine("## Goals: ");
            foreach (var item in session.MetaData.FutureGoals)
            {
                res.AppendLinuxLine("- " + item);
            }
            res.AppendLinuxLine("## Relevance: " + session.MetaData.Relevance.ToString());
            res.AppendLinuxLine().AppendLinuxLine();
            res.AppendLinuxLine("## Dialogs:").AppendLinuxLine().AppendLinuxLine(dialogs);
            web_sessioncontent.NavigateToString(Markdown.ToHtml(res.ToString(), CustomMarkDownPipeline));
        }

        private void UpdateHistoryEntryEvent(object sender, EventArgs e)
        {
            if (_isinitloading || _selectedSession == null)
                return;
            if (!string.IsNullOrWhiteSpace(ed_hist_kw1.Text))
            {
                _selectedSession.KeyWordsMain = ed_hist_kw1.Text.Split(',')?.ToList() ?? [];
            }
            else
            {
                _selectedSession.KeyWordsMain = [];
            }
            if (!string.IsNullOrWhiteSpace(ed_hist_kw2.Text))
            {
                _selectedSession.KeyWordsSecondary = ed_hist_kw2.Text.Split(',')?.ToList() ?? [];
            }
            else
            {
                _selectedSession.KeyWordsSecondary = [];
            }
            _selectedSession.WordLink = (KeyWordLink)cb_hist_kwlink.SelectedIndex;
            _selectedSession.CaseSensitive = ck_hist_casesensitive.Checked;
            _selectedSession.Enabled = ck_hist_kw.Checked;
            _selectedSession.Sticky = ck_hist_sticky.Checked;
            _selectedSession.MetaData.IsRoleplaySession = ck_hist_isrp.Checked;
        }

        private async void bt_sessionrefresh_Click(object sender, EventArgs e)
        {
            if (_selectedSession == null)
                return;
            this.Enabled = false;
            using var loadingForm = new LoadingForm() { Owner = this, StartPosition = FormStartPosition.Manual };
            loadingForm.CenterToParent();
            loadingForm.Show();
            loadingForm.BringToFront();
            loadingForm.Refresh();
            try
            {
                loadingForm.SetMessage("Updating session summary and meta-data. Depending on your computer, the model, and the context window, it might take a while.");
                loadingForm.SetProgress(5);
                LLMSystem.OnQuickInferenceEnded += (s, e) =>
                {
                    loadingForm.AddProgress(20);
                };
                _selectedSession.StartTime = _selectedSession.Messages.First().Date;
                // if the first message has a default date, try to find a message with a valid date
                if (_selectedSession.StartTime == default)
                {
                    foreach (var item in _selectedSession.Messages)
                    {
                        if (item.Date != default)
                        {
                            _selectedSession.StartTime = item.Date;
                            break;
                        }
                    }
                }
                await _selectedSession.UpdateSession();
                loadingForm.SetMessage("Finalizing.");
                loadingForm.SetProgress(95);
                DisplaySessionDetails(_selectedSession);
                LoadChatHistoryTab();
                (LLMSystem.Bot as Character)?.SaveChatHistory();
            }
            finally
            {
                LLMSystem.RemoveQuickInferenceEventHandler();
                loadingForm.Close();
                this.Enabled = true;
            }
        }

        private void ed_sessiontitle_TextChanged(object sender, EventArgs e)
        {
            if (_isinitloading || _selectedSession == null)
                return;
            _selectedSession.MetaData.Title = ed_sessiontitle.Text;
        }

        private void ed_sessioninfo_TextChanged(object sender, EventArgs e)
        {
            if (_isinitloading || _selectedSession == null)
                return;
            _selectedSession.MetaData.Summary = ed_sessioninfo.Text.ToLinuxFormat();

        }

        private async void bt_historyupdate_Click(object sender, EventArgs e)
        {
            if (_selectedSession == null)
                return;
            await _selectedSession.GenerateEmbeds();
            DisplaySessionDetails(_selectedSession);
            LoadChatHistoryTab();
            (LLMSystem.Bot as Character)?.SaveChatHistory();

        }

        private async void btEmbedAll_Click(object sender, EventArgs e)
        {
            if (!RAGSystem.Enabled)
            {
                MessageBox.Show("The RAG System is not enabled. Operation cancelled.");
                return;
            }
            this.Enabled = false;

            using var loadingForm = new LoadingForm() { Owner = this };

            // Set the position in a way that doesn't affect the internal layout
            loadingForm.StartPosition = FormStartPosition.Manual;
            loadingForm.CenterToParent();
            loadingForm.Show();
            loadingForm.BringToFront();
            loadingForm.Refresh();

            try
            {
                loadingForm.SetMessage("Embedding all chat sessions. This might take a moment.");
                loadingForm.SetProgress(0);
                loadingForm.SetMax(LLMSystem.History.Sessions.Count);
                RAGSystem.OnEmbedSession += (s, e) =>
                {
                    loadingForm.AddProgress(1);
                };
                await RAGSystem.EmbedChatSessions(LLMSystem.History);
                loadingForm.SetMessage("Saving history...");
                (LLMSystem.Bot as Character)?.SaveChatHistory(true);
                loadingForm.SetMessage("Loading Updated Vector Database...");
                RAGSystem.VectorizeChatBot(LLMSystem.Bot);
                loadingForm.SetMessage("Brain Embedding...");
                await LLMSystem.Bot.Brain.RegenEmbeds();
            }
            finally
            {
                RAGSystem.RemoveEmbedEventHandler();
                loadingForm.Close();
                this.Enabled = true;
                MessageBox.Show("All sessions have been embedded successfully.");
            }

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
                function addHtmlAfterLastChatMessage(htmlContent, index) {
                    const chatMessages = document.querySelectorAll('.chat-message');
                    if (chatMessages.length > 0) {
                        const lastChatMessage = chatMessages[chatMessages.length - 1];
                        const newDiv = document.createElement('div');
                        newDiv.className = 'chat-message';
                        newDiv.setAttribute('data-message-index', index);
                        newDiv.innerHTML = htmlContent;
                        lastChatMessage.insertAdjacentElement('afterend', newDiv);
                    } else {
                        console.warn('No chat messages found.');
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
                            const messageIndex = parseInt(targetElement.getAttribute('data-message-index'));
                            window.chrome.webview.postMessage({ type: 'EditMessage', index: messageIndex });
                        }
                    });
                });         
            </script>";
            return $"<html><head>{css}</head><body>{scripts}<div id='chatContainer'>{htmlContent}<br/></div></body></html>";
        }

        private static string InjectDialogHtml(string imgPath, string dialog, int index)
        {
            // Replace thinking tags with collapsible div structure, using the instruction format's tags
            var processedDialog = dialog;
            return $@"
                <div class='chat-message' data-message-index='{index}'>
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

        private static string AddHtmlMessage(SingleMessage singleMessage, int index)
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
            return InjectDialogHtml(img, html, index);
        }

        private async Task WebRemoveLastMessage()
        {
            if (InvokeRequired)
            {
                await InvokeAsync(new Func<Task>(WebRemoveLastMessage));
                return;
            }
            await web_chat.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('chat-message')[document.getElementsByClassName('chat-message').length - 1].remove();");
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
        }

        private async Task WebEditLastMessage(string newMessage)
        {
            if (InvokeRequired)
            {
                await InvokeAsync(new Func<Task>(async () => await WebEditLastMessage(newMessage)));
                return;
            }

            if (!string.IsNullOrEmpty(LLMSystem.Instruct.ThinkingStart) &&
                newMessage.StartsWith(ChatRender.GetMessagePrefix(AuthorRole.Assistant)) &&
                newMessage.Contains(LLMSystem.Instruct.ThinkingStart))
            {
                // remove prefix from message
                var worktext = newMessage[ChatRender.GetMessagePrefix(AuthorRole.Assistant).Length..];
                if (!worktext.Contains(LLMSystem.Instruct.ThinkingEnd))
                {
                    worktext = worktext.Replace(LLMSystem.Instruct.ThinkingStart, string.Empty);
                    var text = Markdown.ToHtml(worktext, CustomMarkDownPipeline);
                    text = text.SanitizeForJS();
                    var script = $"updateMessageAtIndex(\"{text}\", document.getElementsByClassName('message-content').length - 1, true);";
                    var result = await web_chat.CoreWebView2.ExecuteScriptAsync(script);
                }
                else
                {
                    // both tokens are found, so we want two strings now: the first one is the thinking part, the second one is the message part
                    var parts = worktext.Split([LLMSystem.Instruct.ThinkingEnd], 2, StringSplitOptions.None);
                    var thinkingText = parts[0].Replace(LLMSystem.Instruct.ThinkingStart, string.Empty);
                    thinkingText = Markdown.ToHtml(thinkingText, CustomMarkDownPipeline).SanitizeForJS();
                    var script = $"updateMessageAtIndex(\"{thinkingText}\", document.getElementsByClassName('message-content').length - 1, true);";
                    var result = await web_chat.CoreWebView2.ExecuteScriptAsync(script);

                    var msgoutput = ChatRender.GetMessagePrefix(AuthorRole.Assistant) + parts[1].TrimStart().TrimStart('\n').TrimStart();
                    var messageText = Markdown.ToHtml(msgoutput, CustomMarkDownPipeline).SanitizeForJS();
                    script = $"updateMessageAtIndex(\"{messageText}\", document.getElementsByClassName('message-content').length - 1, false);";
                    result = await web_chat.CoreWebView2.ExecuteScriptAsync(script);
                }
            }
            else
            {
                var text = Markdown.ToHtml(newMessage, CustomMarkDownPipeline);
                text = text.SanitizeForJS();
                var script = $"updateMessageAtIndex(\"{text}\", document.getElementsByClassName('message-content').length - 1, false);";
                var result = await web_chat.CoreWebView2.ExecuteScriptAsync(script);
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
            var start = LLMSystem.History.CurrentSession.Messages.Count - Program.Settings.MaxMessagesOnScreen;
            if (start < 0)
                start = 0;
            for (int i = start; i < LLMSystem.History.CurrentSession.Messages.Count; i++)
            {
                if (!LLMSystem.History.CurrentSession.Messages[i].Hidden)
                    html += AddHtmlMessage(LLMSystem.History.CurrentSession.Messages[i], i);
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
                if (!json.TryGetValue("index", out object? indexObj))
                    return;
                int divNumber = Convert.ToInt32(indexObj);

                // Use BeginInvoke instead of Invoke to avoid potential deadlocks
                BeginInvoke(new Action(() =>
                {
                    try
                    {
                        if (!IsDisposed && IsHandleCreated)
                        {
                            EditMessage(divNumber);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error in EditMessageSimple: {ex}");
                    }
                }));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnWebChatWebMessageReceived: {ex}");
            }
        }

        private void EditMessage(int messageIndex)
        {
            if (LLMSystem.Status == SystemStatus.Busy || messageIndex < 0 || messageIndex >= LLMSystem.History.CurrentSession.Messages.Count)
                return;
            var realid = messageIndex;
            //if (realid < 0)
            //    realid = 0;
            //realid += messageIndex - 1;
            //if (realid >= LLMSystem.History.CurrentSession.Messages.Count)
            //    return;
            this.Enabled = false;
            using var _editMessage = new EditMessageForm(LLMSystem.History.CurrentSession.Messages[realid].Guid)
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
                        LLMSystem.InvalidatePromptCache();
                    });
                }
            }
            finally
            {
                this.Enabled = true;
            }
        }

        #endregion

        private async void cb_bot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_bot.SelectedItem is string key && !string.IsNullOrEmpty(key))
            {
                _selectedSession = null;
                LLMSystem.Bot = DataFiles.Characters[key];
                await LoadHistoryToUI();
                LoadChatHistoryTab();
                ck_senseoftime.Checked = LLMSystem.Bot.SenseOfTime;
                ck_sessionmemory.Checked = LLMSystem.Bot.SessionMemorySystem;
                ck_caninitchat.Checked = Bot?.CanInitiateChat ?? false;
                var searchplug = LLMSystem.ContextPlugins.Find(x => x.PluginID == "WebSearch");
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

        private void num_maxcontext_ValueChanged(object sender, EventArgs e)
        {
            LLMSystem.MaxContextLength = (int)num_maxcontext.Value;
        }

        private void num_maxresponse_ValueChanged(object sender, EventArgs e)
        {
            LLMSystem.Settings.MaxReplyLength = (int)num_maxresponse.Value;
        }

        private void cb_user_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_user.SelectedItem is string key && !string.IsNullOrEmpty(key))
                LLMSystem.User = DataFiles.Characters[key];
        }

        private void cb_instruct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_instruct.SelectedItem is string key && !string.IsNullOrEmpty(key))
            {
                LLMSystem.Instruct = DataFiles.Instruct[key];
                ck_forceNames.Checked = LLMSystem.Instruct.AddNamesToPrompt;
            }
        }

        private void cb_infer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_infer.SelectedItem is string key && !string.IsNullOrEmpty(key))
                LLMSystem.Sampler = DataFiles.Inference[key];
            num_temperature.Value = (decimal)LLMSystem.Sampler.Temperature;
        }

        private void ed_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if user pressed Shift + Enter send message
            if (e.KeyChar == (char)13)
            {
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

        private void num_temperature_ValueChanged(object sender, EventArgs e)
        {
            LLMSystem.ForceTemperature = ((double)num_temperature.Value);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AutoTalkTimer.Stop();
            SaveSettings();
            LLMSystem.Bot.EndChat(backup: true);
            if (!string.IsNullOrEmpty(LLMSystem.Bot.UniqueName))
                (LLMSystem.Bot as IFile).SaveToFile("data/chars/" + LLMSystem.Bot.UniqueName + ".json");
            AgentRuntime.Instance.Stop();
        }

        private void ck_senseoftime_CheckedChanged(object sender, EventArgs e)
        {
            LLMSystem.Bot.SenseOfTime = ck_senseoftime.Checked;
        }

        private void ck_sessionmemory_CheckedChanged(object sender, EventArgs e)
        {
            LLMSystem.Bot.SessionMemorySystem = ck_sessionmemory.Checked;
        }

        private void bt_scenario_Click(object sender, EventArgs e)
        {
            var editForm = new ScenarioEditForm();
            editForm.ShowDialog();
            editForm.Dispose();
            bt_scenario.ForeColor = string.IsNullOrWhiteSpace(LLMSystem.Settings.ScenarioOverride) ? Color.Black : Color.DarkGreen;
            LLMSystem.InvalidatePromptCache();
        }

        private void num_scandepth_ValueChanged(object sender, EventArgs e)
        {
            SelectedWorldEditor.ScanDepth = (int)num_scandepth.Value;
        }

        private void ed_worlddesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            SelectedWorldEditor.Description = ed_worlddesc.Text;
        }

        private async void bt_worldsave_Click(object sender, EventArgs e)
        {
            var saved = await SaveWorldInfo();
            if (saved)
                MessageBox.Show("World Info Saved!");
        }

        private void bt_delwentry_Click(object sender, EventArgs e)
        {
            if (SelectedWorldEditor?.Entries.Count > 0 && lb_worldentries.Items.Count > 0 && lb_worldentries.SelectedIndex >= 0)
            {
                var idx = lb_worldentries.SelectedIndex;
                SelectedWorldEditor.Entries.RemoveAt(idx);
                lb_worldentries.Items.RemoveAt(idx);
                LoadWorldSettings(SelectedWorldEditor);
            }
        }

        private void bt_addwentry_Click(object sender, EventArgs e)
        {
            SelectedWorldEntryEditor = new WorldEntry() { Name = "New Entry" };
            SelectedWorldEditor.Entries.Add(SelectedWorldEntryEditor);
            lb_worldentries.Items.Add(SelectedWorldEntryEditor.Name);
            lb_worldentries.SelectedIndex = lb_worldentries.Items.Count - 1;
        }

        private void num_msgcount_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.MaxMessagesOnScreen = (int)num_msgcount.Value;
        }

        private void ck_forceNames_CheckedChanged(object sender, EventArgs e)
        {
            LLMSystem.Instruct.AddNamesToPrompt = ck_forceNames.Checked;
            LLMSystem.InvalidatePromptCache();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            LLMSystem.Settings.AllowWorldInfo = ck_worldinfo.Checked;
        }

        private void num_memtokens_ValueChanged(object sender, EventArgs e)
        {
            LLMSystem.Settings.ReservedSessionTokens = (int)num_memtokens.Value;
        }

        private async void bt_deleteAllHistory_Click(object sender, EventArgs e)
        {
            // Confirm before deleting
            if (MessageBox.Show("This will delete all chat history with this character permanently. Are you sure?", "Delete All History?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LLMSystem.History.DeleteAll(true);
                var message = new SingleMessage(AuthorRole.Assistant, DateTime.Now, LLMSystem.Bot.GetWelcomeLine(LLMSystem.User.Name), LLMSystem.Bot.UniqueName, LLMSystem.Bot.UniqueName);
                LLMSystem.History.LogMessage(message);
                LoadChatHistoryTab();
                await WebChatLoad();
            }
        }

        private void cb_sysprompt_SelectionIndexChanged(object sender, EventArgs e)
        {
            if (cb_sysprompt.SelectedItem is string key && !string.IsNullOrEmpty(key))
                LLMSystem.SystemPrompt = DataFiles.SysPrompts[key];
        }

        private void ck_caninit_CheckedChanged(object sender, EventArgs e)
        {
            if (Bot != null)
                Bot.CanInitiateChat = ck_caninitchat.Checked;
        }

        private void AutoTalkTimer_Tick(object sender, EventArgs e)
        {
            if (LLMSystem.Status != SystemStatus.Ready || !string.IsNullOrEmpty(ed_input.Text) || Bot?.CanInitiateChat != true)
                return;
            _activityTimer?.IsTimeout();
        }

        private void ck_onlinerag_CheckedChanged(object sender, EventArgs e)
        {
            var searchplug = LLMSystem.ContextPlugins.Find(x => x.PluginID == "WebSearch");
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

        private void ck_wiembed_CheckedChanged(object sender, EventArgs e)
        {
            SelectedWorldEditor.DoEmbeds = ck_wiembed.Checked;
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

        private void button2_Click(object sender, EventArgs e)
        {
            // female: "Tina", "super chariot of death", "super chariot in death"
            // matel: "Lor_ Merciless", "kobo", "chatty"
            //    var ttsinput = new AIToolkit.API.TextToSpeechInput()
            //    {
            //        Input = ed_input.Text,
            //        Voice = "super chariot in death",
            //    };

            //    var audioData = await LLMSystem.GenerateTTS(ttsinput.Input, ttsinput.Voice);
            //    PlayAudio(audioData);
            //
        }

        private void ck_ttstoggle_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.UseTTS = ck_ttstoggle.Checked;
        }

        private void label59_Click(object sender, EventArgs e)
        {

        }

        private void bt_editchar_Click(object sender, EventArgs e)
        {
            var editForm = new CharEditForm();
            editForm.SetupCharacterEditor(Bot?.UniqueName ?? string.Empty);
            editForm.ShowDialog();
            editForm.Dispose();
            LLMSystem.InvalidatePromptCache();
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

        private void num_removeitalicmaxword_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.RemoveItalicMaxWords = (int)num_removeitalicmaxword.Value;
        }

        private void bt_clearimg_Click(object sender, EventArgs e)
        {
            LLMSystem.VLM_ClearImages();
            pictEmbed.Image = null;
        }

        private void ck_disablethink_CheckedChanged(object sender, EventArgs e)
        {
            LLMSystem.Settings.DisableThinking = ck_disablethink.Checked;
        }

        private void ck_ragtothink_CheckedChanged(object sender, EventArgs e)
        {
            LLMSystem.Settings.RAGMoveToThinkBlock = ck_ragtothink.Checked;
        }

        private void ck_alwayswebsearch_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ck_sysrag_CheckedChanged(object sender, EventArgs e)
        {
            LLMSystem.Settings.RAGMoveToSysPrompt = ck_sysrag.Checked;
        }

        private void ck_agentmode_CheckedChanged(object sender, EventArgs e)
        {
            LLMSystem.Settings.AgentEnabled = ck_agentmode.Checked;
        }

        private async void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                var searchstr = textBox1.Text;
                txtSearchRes.Clear();
                if (!string.IsNullOrWhiteSpace(searchstr))
                {

                    var res = new StringBuilder();
                    res.AppendLine($"Base string: {searchstr}");
                    searchstr = searchstr.ConvertToThirdPerson();
                    res.AppendLine($"Converted to 3rd person: {searchstr}");
                    res.AppendLine();
                    res.AppendLine("Search Results:");
                    var found = await RAGSystem.Search(searchstr, 100, 1.2f);
                    foreach (var item in found)
                    {
                        var title = "[unknown]";
                        var content = "[unknown]";
                        var distance = item.distance.ToString("0.0000");
                        var cat = item.category.ToString();
                        if (item.session is MemoryUnit unit)
                        {
                            title = unit.Name;
                            content = unit.Content;
                        }
                        else if (item.session is ChatSession sess)
                        {
                            title = sess.Title;
                            content = sess.Summary;
                        }
                        res.AppendLine(cat + " (dist: " + distance + "): " + title);
                        res.AppendLine(content);
                        res.AppendLine();
                    }
                    txtSearchRes.Text = res.ToString();
                }
            }
        }
    }
}
