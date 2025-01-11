using System;
using System.Net;
using WaifuAI.Files;
using AIToolkit;
using System.Reflection;
using Newtonsoft.Json;
using Markdig;
using Microsoft.Web.WebView2.Core;
using AIToolkit.Files;
using AIToolkit.LLM;
using WaifuAI.src.forms;
using WaifuAI.Web;
using WaifuAI.Plugins;
using Discord.Rest;

namespace WaifuAI
{
    public partial class MainForm : Form
    {
        public WaifuSettings Settings { get; set; } = new WaifuSettings();
        public WebScraper WebScraper { get; set; } = new WebScraper();

        public SamplerSettings SelectedSamplerEditor { get; set; } = new SamplerSettings();
        public InstructFormat SelectedInstructEditor { get; set; } = new InstructFormat();
        public SystemPrompt SelectedPromptEditor { get; set; } = new SystemPrompt();
        public WorldInfo SelectedWorldEditor { get; set; } = new WorldInfo();
        public WorldEntry SelectedWorldEntryEditor { get; set; } = new WorldEntry();

        private string? _currentgeneration = null;
        private int _currentgenerationtokencount = 0;
        private ChatSession? _selectedSession = null;
        private bool _impersonatemode = false;
        private bool _forcereload = false;
        private bool _editopened = false;
        private bool _isinitloading = true;
        private DateTime _postdate = DateTime.Now;
        private TimeSpan _responselength = default;
        private ActivityTimer _activityTimer = new();
        private int _afkmessagecount = 0;
        private EditMessageForm? _editMessageForm;


        public static Character? Bot => LLMSystem.Bot as Character;
        public static Character? User => LLMSystem.User as Character;


        public static MarkdownPipeline CustomMarkDownPipeline { get; } = new MarkdownPipelineBuilder()
            .UseSoftlineBreakAsHardlineBreak()
            .UseEmojiAndSmiley()
            .UseAutoLinks()
            .Build();

        public MainForm()
        {
            InitializeComponent();
            // load all the image files in data/backgrounds to the cb_background combobox
            foreach (var file in Directory.GetFiles("data/background"))
            {
                cb_background.Items.Add(Path.GetFileName(file));
            }

            HelptoolTip.SetToolTip(ck_ragweb, "Allows the LLM to browse compatible websites for information.");
            HelptoolTip.SetToolTip(ck_webgrammar, "If checked, the LLM will be better at navigating the website, but its results will be less accurate." + Environment.NewLine + "Only enable if the LLM is consistently failing at browsing the web.");
            HelptoolTip.SetToolTip(ck_webkeyword, "If checked, the web logic will only be run if some internet related keywords are found in the user's request (faster, less accurate)." + Environment.NewLine + "If unckecked, all user inputs will be processed twice, once to check if the web should be visited, and another time for the normal response from the bot (slower, more accurate).");

            HelptoolTip.SetToolTip(ck_ragenabled, "Use RAG and keywords to insert summaries of relevant previous sessions based on the user's input." + Environment.NewLine + "Configurable in the Settings tab.");
            HelptoolTip.SetToolTip(ck_senseoftime, "Insert day and time information to prompt when relevant to give the bot a better understanding of time.");
            HelptoolTip.SetToolTip(ck_sessionmemory, "Use a set amount of tokens (set in settings) to insert summaries of previous chat sessions with this bot." + Environment.NewLine + "This drastically increases the bot's long-term memory.");
            HelptoolTip.SetToolTip(ck_worldinfo, "Use the WorldInfo file(s) associated with this bot. WorldInfo is a list of keyword-triggered textual information that is inserted into the prompt when the conditions are met." + Environment.NewLine + "See the World Info tab for additional information.");

            // Chat related events
            bt_chattosessions.Click += ConvertChatToSessionList!;
            // Load editors and chat menu
            bt_embedall.Click += EmbedAllSessions!;
            SetupSamplerEditor();
            SetupInstructEditor();
            SetupWorldEditor();
            SetupPromptEditor();
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
            var message = "The last message from {{user}} was posted " + LLMSystem.TimeSpanToHumanString(DateTime.Now - lastusermessage.Date) + " ago. We're {{day}}, the {{date}} at {{time}} now. Would you like to send a message to {{user}} now? Use your best judgement based on the conversation above. In case you don't want to send a message, just respond with No. If you want to send a message, enter the message from {{char}} to {{user}} directly while making sure it's contextually relevant. \n\nThis query will repeat every few minutes.";
            if (_afkmessagecount > 1)
                message += " You've already sent " + _afkmessagecount + " unanswered messages in a row.";
            else if (_afkmessagecount == 1)
                message += " You've already sent a message.";
            message = LLMSystem.ReplaceMacros(message);
            statusbar.Items[1].Text = "Analyzing...";
            var response = await LLMSystem.QuickInferenceForSystemPrompt(message, false);


            if (!string.IsNullOrEmpty(response) && !response.StartsWith("no", StringComparison.InvariantCultureIgnoreCase))
            {
                var msg = new SingleMessage(AuthorRole.Assistant, DateTime.Now, response, LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName);
                Bot.History.LogMessage(msg);
                _afkmessagecount++;
                await SendMessageToUI(msg);
                // play a notification sound
                System.Media.SystemSounds.Question.Play();
            }
        }

        private void SetupChatMenu()
        {
            LLMSystem.Init();
            LLMSystem.ContextPlugins = [];
            LLMSystem.ContextPlugins.Add(new BrowsePlugin());
            LLMSystem.ContextPlugins.Add(new LocationPlugin("Locations"));
            cb_bot.Items.Clear();
            cb_user.Items.Clear();
            bt_scenario.ForeColor = string.IsNullOrWhiteSpace(LLMSystem.ScenarioOverride) ? Color.Black : Color.DarkGreen;
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
            RAGSystem.Enabled = true;
            ck_ragenabled.Checked = RAGSystem.Enabled;
            ck_worldinfo.Checked = LLMSystem.WorldInfo;
            LLMSystem.UI_RefreshChat = ForceWebChatReload;
            LLMSystem.UI_ChangeMessage = ForceUpdateLastMessage;
            LLMSystem.OnInferenceStreamed += OnStreamMessageReceived;
            LLMSystem.OnInferenceEnded += OnStreamInferenceEnded;
            LLMSystem.OnFullPromptReady += OnFullPromptReady;
            LLMSystem.OnStatusChanged += OnStatusChanged;
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
            bt_scenario.ForeColor = string.IsNullOrWhiteSpace(LLMSystem.ScenarioOverride) ? Color.Black : Color.DarkGreen;
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
            else
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
            _currentgeneration += e;
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
                    var MsgPrefix = LLMSystem.GetMessagePrefix(AuthorRole.Assistant);
                    await WebEditLastMessage(MsgPrefix + _currentgeneration);
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
                var stringfix = e.FixAsterisks();
                var MsgPrefix = LLMSystem.GetMessagePrefix(AuthorRole.Assistant);
                var msg = LLMSystem.Bot.History.LogMessage(AuthorRole.Assistant, stringfix, LLMSystem.User, LLMSystem.Bot);
                await WebEditLastMessage(MsgPrefix + stringfix);
                _currentgeneration = string.Empty;
                _currentgenerationtokencount = 0;
                if (_forcereload)
                {
                    _forcereload = false;
                    Invoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        WebChatLoad();
                    });
                }
                Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    statusbar.Items[1].Text = $"Generation: {_responselength.TotalSeconds:F2}s";
                });
            }
            (LLMSystem.Bot as Character)?.SaveChatHistory();
        }

        private void ShowCurrentSessionInfo()
        {
            var (tokens, duration) = LLMSystem.History.GetCurrentChatSessionInfo();
            statusbar.Items[0].Text = $"Current Session: {duration.TotalDays.ToString("F2")} days ({tokens} tokens)";
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
        /// Initialize the inference settings editor panel
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
                Dynatemp_range = (int)num_dynrange.Value,
                Bypass_eos = ck_ignoreeos.Checked,
                Render_special = ck_renderspecial.Checked,
                Trim_stop = ck_trimstop.Checked,
                Sampler_order = [6, 0, 1, 3, 4, 2, 5],
                Dry_sequence_breakers = ["\n", ":", "\"", "*"],
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
            ed_wentrymem.Text = worldEntry.Message.ToWinFormat();
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
            _isinitloading = sv;
        }

        private void SaveWorldEntry()
        {
            SelectedWorldEntryEditor.Name = ed_wentryname.Text;
            SelectedWorldEntryEditor.Message = ed_wentrymem.Text.ToLinuxFormat();
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

            var idx = SelectedWorldEditor.Entries.IndexOf(SelectedWorldEntryEditor);
            if (idx >= 0 && idx < lb_worldentries.Items.Count)
                lb_worldentries.Items[idx] = SelectedWorldEntryEditor.Name;
        }

        private void SaveWorldInfo()
        {
            SelectedWorldEditor.Description = ed_worlddesc.Text;
            SelectedWorldEditor.ScanDepth = (int)num_scandepth.Value;
            var NewName = cb_samplerlist.Text;
            if (string.IsNullOrWhiteSpace(NewName))
            {
                MessageBox.Show("Please select a valide name for the new sampler");
                return;
            }
            // If name already exists ask for confirmation
            if (DataFiles.Inference.ContainsKey(NewName) && (MessageBox.Show("This sampler already exists, do you want to overwrite it?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No))
                return;
            SelectedWorldEditor.UniqueName = NewName;
            DataFiles.WorldInfos[NewName] = SelectedWorldEditor;

            (SelectedWorldEditor as IFile).SaveToFile("data/worlds/" + NewName + ".json");
        }

        private void UpdateWorldEntryEvent(object sender, EventArgs e)
        {
            if (_isinitloading)
                return;
            SaveWorldEntry();
        }

        /// <summary>
        /// Create the editor for the instruction format settings
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
                else if (property.PropertyType == typeof(ICollection<string>))
                {
                    control = new TextBox { Text = string.Join(",", (ICollection<string>)property.GetValue(instructsetting) ?? []), Location = new Point(150, yPos), Width = 400 };
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
            var currselection = cb_instruct.SelectedText;
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

        private async void Impersonate(object sender, EventArgs e)
        {
            ForceCloseEditMenu();
            _activityTimer?.Reset();
            if (LLMSystem.Status == SystemStatus.Busy)
                return;
            statusbar.Items[1].Text = "Analyzing...";
            _postdate = DateTime.Now;
            _impersonatemode = true;
            _currentgeneration = string.Empty;
            _currentgenerationtokencount = 0;
            ed_input.Text = string.Empty;
            await LLMSystem.ImpersonateUser();
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
            if (!string.IsNullOrEmpty(ed_input.Text))
            {
                var messagetext = LLMSystem.ReplaceMacros(LLMSystem.GetAwayString() + ed_input.Text.ToLinuxFormat(), LLMSystem.User, LLMSystem.Bot);
                var msg = new SingleMessage(AuthorRole.User, DateTime.Now, messagetext, LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName);
                if (ed_input.Text.StartsWith("/sys "))
                {
                    msg.Role = AuthorRole.System;
                    // remove the /sys prefix
                    msg.Message = msg.Message[5..].Trim();
                }
                else if (ed_input.Text.Contains("/scrape "))
                {
                    // retrieve first word after /scrape, and only the first word
                    var scrape = ed_input.Text[8..].Trim();
                    if (scrape.Contains(' '))
                        scrape = scrape[..scrape.IndexOf(' ')];

                    if (DataFiles.Websites.TryGetValue(scrape, out var web))
                    {
                        var listing = await WebScraper.ParseWebListing(web.Address, web, true);
                        msg.Role = AuthorRole.System;
                        msg.Message = listing.ExportToMarkdown();
                    }
                    else
                    {
                        ed_input.Text = string.Empty;
                        return;
                    }
                }
                await SendMessageToUI(msg);
                // ready a new message for the bot's response
                _currentgeneration = string.Empty;
                _currentgenerationtokencount = 0;
                await SendMessageToUI(
                    new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMSystem.Bot.UniqueName + " is reading your post...*", LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName));
                ed_input.Text = string.Empty;
                await LLMSystem.SendMessageToBot(msg);
            }
            else
            {
                // ready a new message for the bot's response
                _currentgeneration = string.Empty;
                _currentgenerationtokencount = 0;
                await SendMessageToUI(
                    new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMSystem.Bot.UniqueName + " is thinking...*", LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName));
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
                _editMessageForm = null;
                _editopened = false;
            }
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
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
            await WebEditLastMessage($"**{LLMSystem.Bot.Name}:** *I am thinking...*");
            _currentgeneration = string.Empty;
            _currentgenerationtokencount = 0;
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
            await LLMSystem.RerollLastMessage();
        }

        private async void Connect(object sender, EventArgs e)
        {
            await LLMSystem.Connect();
            num_maxcontext.Maximum = LLMSystem.MaxContextLength;
            num_maxcontext.Value = LLMSystem.MaxContextLength;
            grp_model.Text = LLMSystem.CurrentModel;
        }

        private async void StartNewSession(object sender, EventArgs e)
        {
            // Check if we're in a past sessions, if so, ask if the user wants to update the archive before going back to the current session
            if (LLMSystem.History.CurrentSessionID != -1 && LLMSystem.History.CurrentSessionID != LLMSystem.History.Sessions.Count - 1)
            {

                if (MessageBox.Show("Do you want to update this session's summary before going back to the latest session?", "Refresh?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    await LLMSystem.History.CurrentChatToSession();
                }
                LLMSystem.History.CurrentSessionID = -1;
                (LLMSystem.Bot as Character)?.SaveChatHistory();
            }
            else
            {
                if (MessageBox.Show("This will archive the current chat and start a new one.", "Confirm?", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                await LLMSystem.History.StartNewChatSession(true);
                (LLMSystem.Bot as Character)?.SaveChatHistory();
            }
            await WebChatLoad();
            LoadChatHistoryTab();
            _afkmessagecount = 0;
            _activityTimer?.Reset();
        }

        private async void DeleteLastMessage(object sender, EventArgs e)
        {
            _impersonatemode = false;
            if (LLMSystem.Status == SystemStatus.Busy || LLMSystem.History.CurrentSession.Messages.Count == 0)
                return;
            LLMSystem.RemoveLastMessage();
            await WebChatLoad();
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
                    img = LLMSystem.User.Icon;
                    break;
                case AuthorRole.Assistant:
                    img = LLMSystem.Bot.Icon;
                    break;
            }
            var text = Markdown.ToHtml(LLMSystem.GetMessagePrefix(singleMessage) + singleMessage.Message, CustomMarkDownPipeline);
            var coremsg = $@"
                    <div class='portrait'>
                        <img src='https://appassets.test/img/{img}' alt='Portrait' width='60'>
                    </div>
                    <div class='message-content'>
                        {text}
                    </div>";

            coremsg = coremsg.SanitizeForJS();
            var script = $"addHtmlAfterLastChatMessage(\"{coremsg}\");";
            await web_chat.CoreWebView2.ExecuteScriptAsync(script);
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
        }

        private void LoadSettings()
        {
            if (File.Exists("settings.json"))
            {
                var str = File.ReadAllText("settings.json");
                Settings = JsonConvert.DeserializeObject<WaifuSettings>(str)!;
                RAGSystem.Heuristic = Settings.RAGHeurisitc;
                RAGSystem.UseSummaries = Settings.RAGUseSummaries;
                RAGSystem.UseTitles = Settings.RAGUseTitles;
                RAGSystem.DistanceCutOff = Settings.RAGDistanceCutOff;
                LLMSystem.MaxContextLength = Settings.MaxTotalTokens;
                LLMSystem.MaxReplyLength = Settings.MaxResponseTokens;
                LLMSystem.ReservedSessionTokens = Settings.ReservedSessionTokens;
                LLMSystem.MarkdownMemoryFormating = Settings.MarkdownMemoryFormating;
                LLMSystem.MaxRAGEntries = Settings.MaxRAGEntries;
                LLMSystem.RAGIndex = Settings.RAGPosition;
                LLMSystem.ScenarioOverride = Settings.ScenarioOverride;
                LLMSystem.WebBrowsingPlugin = Settings.InternetSearch;
                // set cb_user to the settings.UserFile value if it's in the list, otherwise set index to 0.
                cb_user.SelectedIndex = cb_user.Items.Contains(Settings.UserFile) ? cb_user.Items.IndexOf(Settings.UserFile) : 0;
                // set cb_infer to the settings.InferenceFile value if it's in the list, otherwise set index to 0.
                cb_infer.SelectedIndex = cb_infer.Items.Contains(Settings.SamplerFile) ? cb_infer.Items.IndexOf(Settings.SamplerFile) : 0;
                // set cb_instruct to the settings.InstructFile value if it's in the list, otherwise set index to 0.
                cb_instruct.SelectedIndex = cb_instruct.Items.Contains(Settings.Instruct) ? cb_instruct.Items.IndexOf(Settings.Instruct) : 0;
                // set cb_bot to the settings.BotFile value if it's in the list, otherwise set index to 0.
                cb_bot.SelectedIndex = cb_bot.Items.Contains(Settings.BotFile) ? cb_bot.Items.IndexOf(Settings.BotFile) : 0;
                // set cb_sysprompt to the settings.PromptFile value if it's in the list, otherwise set index to 0.
                cb_sysprompt.SelectedIndex = cb_sysprompt.Items.Contains(Settings.PromptFile) ? cb_sysprompt.Items.IndexOf(Settings.PromptFile) : 0;
                num_maxcontext.Maximum = Settings.MaxTotalTokens;
                num_maxcontext.Value = Settings.MaxTotalTokens;
                num_maxresponse.Value = Settings.MaxResponseTokens;
                num_temperature.Value = (decimal)Settings.Temperature;
                num_memtokens.Value = Settings.ReservedSessionTokens;
                ck_markdown.Checked = Settings.MarkdownMemoryFormating;
                switch (RAGSystem.Heuristic)
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
                ck_ragsummaries.Checked = Settings.RAGUseSummaries;
                ck_ragtitles.Checked = Settings.RAGUseTitles;
                num_ragcutoff.Value = (decimal)Settings.RAGDistanceCutOff;
                num_ragmaxretrieve.Value = Settings.MaxRAGEntries;
                num_ragindex.Value = Settings.RAGPosition;
                ck_ragweb.Checked = Settings.InternetSearch;
                cb_background.SelectedIndex = cb_background.Items.IndexOf(Settings.BackgroundFile);
                num_fontsize.Value = Settings.FontSize;
                num_msgcount.Value = Settings.MaxMessagesOnScreen;
            }
        }

        private void SaveSettings()
        {
            try
            {
                Settings.BotFile = cb_bot.SelectedItem?.ToString() ?? string.Empty;
                Settings.UserFile = cb_user.SelectedItem?.ToString() ?? string.Empty;
                Settings.SamplerFile = cb_infer.SelectedItem?.ToString() ?? string.Empty;
                Settings.Instruct = cb_instruct.SelectedItem?.ToString() ?? string.Empty;
                Settings.PromptFile = cb_sysprompt.SelectedItem?.ToString() ?? string.Empty;
                Settings.MaxTotalTokens = LLMSystem.MaxContextLength;
                Settings.MaxResponseTokens = LLMSystem.MaxReplyLength;
                Settings.Temperature = (double)num_temperature.Value;
                Settings.InternetSearch = LLMSystem.WebBrowsingPlugin;
                Settings.RAGHeurisitc = RAGSystem.Heuristic;
                Settings.RAGUseSummaries = RAGSystem.UseSummaries;
                Settings.RAGUseTitles = RAGSystem.UseTitles;
                Settings.RAGDistanceCutOff = RAGSystem.DistanceCutOff;
                Settings.ReservedSessionTokens = LLMSystem.ReservedSessionTokens;
                Settings.MarkdownMemoryFormating = LLMSystem.MarkdownMemoryFormating;
                Settings.MaxRAGEntries = LLMSystem.MaxRAGEntries;
                Settings.RAGPosition = LLMSystem.RAGIndex;
                Settings.ScenarioOverride = LLMSystem.ScenarioOverride;
                Settings.FontSize = (int)num_fontsize.Value;
                Settings.MaxMessagesOnScreen = (int)num_msgcount.Value;
                Settings.BackgroundFile = cb_background.SelectedItem?.ToString() ?? "bedroom_cozy.jpg";
                var str = JsonConvert.SerializeObject(Settings, Formatting.Indented);
                File.WriteAllText("settings.json", str);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving settings: {ex.Message}");
            }
        }

        #endregion

        #region *** Settings Tab Functions ***

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

        private async void EmbedAllSessions(object sender, EventArgs e)
        {
            if (!RAGSystem.Enabled)
            {
                MessageBox.Show("The RAG System is not enabled. Operation cancelled.");
                return;
            }
            await RAGSystem.EmbedChatSessions(LLMSystem.History);
            MessageBox.Show("All sessions have been embedded successfully.");
            (LLMSystem.Bot as Character)?.SaveChatHistory(true);
            RAGSystem.VectorizeChatlog(LLMSystem.History);
        }

        private void ApplyRAGSettings(object sender, EventArgs e)
        {
            RAGSystem.UseSummaries = ck_ragsummaries.Checked;
            RAGSystem.UseTitles = ck_ragtitles.Checked;
            RAGSystem.DistanceCutOff = (float)num_ragcutoff.Value;
            LLMSystem.MaxRAGEntries = (int)num_ragmaxretrieve.Value;
            LLMSystem.RAGIndex = (int)num_ragindex.Value;
            LLMSystem.WebBrowsingPlugin = ck_ragweb.Checked;
            if (cb_ragheuristic.SelectedIndex == 0)
                RAGSystem.Heuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectHeuristic;
            else if (cb_ragheuristic.SelectedIndex == 1)
                RAGSystem.Heuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectSimple;
            RAGSystem.ApplySettings();
            SaveSettings();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_ragheuristic.SelectedIndex == 0)
                RAGSystem.Heuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectHeuristic;
            else if (cb_ragheuristic.SelectedIndex == 1)
                RAGSystem.Heuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectSimple;
        }

        private void num_ragcutoff_ValueChanged(object sender, EventArgs e)
        {
            RAGSystem.DistanceCutOff = (float)num_ragcutoff.Value;
        }

        private void num_ragmaxretrieve_ValueChanged(object sender, EventArgs e)
        {
            LLMSystem.MaxRAGEntries = (int)num_ragmaxretrieve.Value;
        }

        private void ck_ragenabled_CheckedChanged(object sender, EventArgs e)
        {
            RAGSystem.Enabled = ck_ragenabled.Checked;
        }

        private void num_ragindex_ValueChanged(object sender, EventArgs e)
        {
            LLMSystem.RAGIndex = (int)num_ragindex.Value;
        }

        private async void num_fontsize_ValueChanged(object sender, EventArgs e)
        {
            Settings.FontSize = (int)num_fontsize.Value;
            if (!_isinitloading)
                await WebChatLoad();
        }

        private async void cb_background_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.BackgroundFile = cb_background.SelectedItem?.ToString() ?? "bedroom_cozy.jpg";
            if (!_isinitloading)
                await WebChatLoad();
        }

        #endregion

        #region *** Chat History Tab Functions ***

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
            lbl_sessiontitle.Text = session.Title;
            lbl_sessioninfo.Text = session.StartTime.ToString("g") + " - " + session.EndTime.ToString("g") + " - " + session.Messages.Count + " messages";

            var sv = _isinitloading;
            _isinitloading = true;
            ed_hist_kw1.Text = string.Join(",", session.KeyWordsMain);
            ed_hist_kw2.Text = string.Join(",", session.KeyWordsSecondary);
            cb_hist_kwlink.SelectedIndex = (int)session.WordLink;
            ck_hist_casesensitive.Checked = session.CaseSensitive;
            ck_hist_kw.Checked = session.Enabled;
            ck_hist_sticky.Checked = session.Sticky;
            _isinitloading = sv;

            if (web_sessioncontent.CoreWebView2 == null)
            {
                await web_sessioncontent.EnsureCoreWebView2Async();
            }
            var dialogs = session.GetRawDialogs(int.MaxValue, false).Replace("\n", "\n\n");
            var inf = "# " + session.Title + LLMSystem.NewLine + LLMSystem.NewLine + "## Summary:" + LLMSystem.NewLine + LLMSystem.NewLine + session.Summary + LLMSystem.NewLine + LLMSystem.NewLine + "## Dialogs:" + LLMSystem.NewLine + LLMSystem.NewLine + dialogs;
            web_sessioncontent.NavigateToString(Markdown.ToHtml(inf, CustomMarkDownPipeline));
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
        }

        private async void bt_sessionrefresh_Click(object sender, EventArgs e)
        {
            if (_selectedSession == null)
                return;
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
            _selectedSession.EndTime = _selectedSession.Messages.Last().Date;
            _selectedSession.Summary = await _selectedSession.GenerateNewSummary();
            _selectedSession.Title = await ChatSession.GenerateNewTitle(_selectedSession.Summary);
            DisplaySessionDetails(_selectedSession);
            LoadChatHistoryTab();
            (LLMSystem.Bot as Character)?.SaveChatHistory();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if (_selectedSession == null)
                return;
            LLMSystem.Bot.History.CurrentSessionID = LLMSystem.Bot.History.Sessions.IndexOf(_selectedSession);
            _activityTimer?.Reset();
            LoadChatHistoryTab();
            await WebChatLoad();
        }

        private async void bt_insertsession_Click(object sender, EventArgs e)
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
            await LLMSystem.History.StartNewChatSession(true);
            await WebChatLoad();

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
                    font-size: {Settings.FontSize}px;
                    width: 100%;
                    box-sizing: border-box;
                    background-image: url('https://appassets.test/background/{Settings.BackgroundFile}');
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

                .message-content {{
                    flex: 1;
                    word-wrap: break-word;
                    padding-right: 10px;
                }}
            </style>";
            string scripts = @"
            <script>
                function updateMessageAtIndex(text, index) {
                    const messageContents = document.getElementsByClassName('message-content');
                    if (index >= 0 && index < messageContents.length) {
                        const messageContent = messageContents[index];
                        messageContent.innerHTML = text;
                    } else {
                        console.error('Index out of bounds');
                    }
                }
                function addHtmlAfterLastChatMessage(htmlContent) {
                    const chatMessages = document.querySelectorAll('.chat-message');
                    if (chatMessages.length > 0) {
                        const lastChatMessage = chatMessages[chatMessages.length - 1];
                        const newDiv = document.createElement('div');
                        newDiv.className = 'chat-message';
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
                            const index = Array.from(chatContainer.children).indexOf(targetElement);
                            window.chrome.webview.postMessage({ type: 'EditMessage', index: index + 1 });
                        }
                    });
                });         
            </script>";
            return $"<html><head>{css}</head><body>{scripts}<div id='chatContainer'>{htmlContent}<br/></div></body></html>";
        }

        private void EditMessage(int messageIndex)
        {
            if (_editopened || LLMSystem.Status == SystemStatus.Busy)
                return;
            _editopened = true;
            try
            {
                Task.Run(() =>
                {
                    var realid = LLMSystem.History.CurrentSession.Messages.Count - Settings.MaxMessagesOnScreen;
                    if (realid < 0)
                        realid = 0;
                    realid += messageIndex - 1;
                    if (realid >= LLMSystem.History.CurrentSession.Messages.Count)
                        return;
                    _editMessageForm = new EditMessageForm(LLMSystem.History.CurrentSession.Messages[realid].Guid);
                    if (_editMessageForm.ShowDialog() == DialogResult.OK && _editMessageForm.Message != null)
                    {
                        Invoke((System.Windows.Forms.MethodInvoker)delegate
                        {
                            LoadHistoryToUI();
                            LLMSystem.InvalidatePromptCache();
                        });
                    }
                    _editMessageForm?.Dispose();
                    _editMessageForm = null;
                    _editopened = false;
                });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while editing the message: {ex.Message}");
            }
        }

        private static string InjectDialogHtml(string imgPath, string dialog)
        {
            // Convert relative path to absolute path and format as file URI
            return $@"
                <div class='chat-message'>
                    <div class='portrait'>
                        <img src='https://appassets.test/img/{imgPath}' alt='Portrait' width='60'>
                    </div>
                    <div class='message-content'>
                        {dialog}
                    </div>
                </div>";
        }

        private static string AddHtmlMessage(SingleMessage singleMessage)
        {
            string img = "gears.png";
            switch (singleMessage.Role)
            {
                case AuthorRole.User:
                    img = singleMessage.User.Icon;
                    break;
                case AuthorRole.Assistant:
                    img = singleMessage.Bot.Icon;
                    break;
            }
            var html = Markdown.ToHtml(LLMSystem.GetMessagePrefix(singleMessage) + singleMessage.Message, CustomMarkDownPipeline);
            return InjectDialogHtml(img, html);
        }

        private async Task WebEditLastMessage(string newMessage)
        {
            if (InvokeRequired)
            {
                await InvokeAsync(new Func<Task>(async () => await WebEditLastMessage(newMessage)));
                return;
            }
            var text = Markdown.ToHtml(newMessage, CustomMarkDownPipeline);
            text = text.SanitizeForJS();
            var script = $"updateMessageAtIndex(\"{text}\", document.getElementsByClassName('message-content').length - 1);";
            var result = await web_chat.CoreWebView2.ExecuteScriptAsync(script);
            await web_chat.CoreWebView2.ExecuteScriptAsync("window.scrollTo(0, document.body.scrollHeight);");
        }

        private async Task WebEditMessageByID(string newMessage, int index)
        {
            if (InvokeRequired)
            {
                await InvokeAsync(new Func<Task>(async () => await WebEditMessageByID(newMessage, index)));
                return;
            }
            var text = Markdown.ToHtml(newMessage, CustomMarkDownPipeline);
            text = text.SanitizeForJS();
            var script = $"updateMessageAtIndex(\"{text}\", {index});";
            await web_chat.CoreWebView2.ExecuteScriptAsync(script);
        }

        public void ForceWebChatReload()
        {
            _forcereload = true;
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
            var start = LLMSystem.History.CurrentSession.Messages.Count - Settings.MaxMessagesOnScreen;
            if (start < 0)
                start = 0;
            for (int i = start; i < LLMSystem.History.CurrentSession.Messages.Count; i++)
            {
                html += AddHtmlMessage(LLMSystem.History.CurrentSession.Messages[i]);
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
            string script = "window.scrollTo(0, document.body.scrollHeight);";
            await web_chat.CoreWebView2.ExecuteScriptAsync(script);
        }

        private void OnWebChatWebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var message = e.WebMessageAsJson;
            if (message != null)
            {
                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(message);
                if (json != null && json.TryGetValue("type", out object? value) && value.ToString() == "EditMessage")
                {
                    int divNumber = Convert.ToInt32(json["index"]);
                    Invoke(new Action<int>(EditMessage), divNumber);
                }
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
            LLMSystem.MaxReplyLength = (int)num_maxresponse.Value;
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
            SaveSettings();
            LLMSystem.Bot.EndSession(backup: true);
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
            bt_scenario.ForeColor = string.IsNullOrWhiteSpace(LLMSystem.ScenarioOverride) ? Color.Black : Color.DarkGreen;
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

        private void bt_worldsave_Click(object sender, EventArgs e)
        {
            SaveWorldInfo();
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
            Settings.MaxMessagesOnScreen = (int)num_msgcount.Value;
        }


        private void ck_forceNames_CheckedChanged(object sender, EventArgs e)
        {
            LLMSystem.Instruct.AddNamesToPrompt = ck_forceNames.Checked;
            LLMSystem.InvalidatePromptCache();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            LLMSystem.WorldInfo = ck_worldinfo.Checked;
        }

        private void num_memtokens_ValueChanged(object sender, EventArgs e)
        {
            LLMSystem.ReservedSessionTokens = (int)num_memtokens.Value;
        }

        private void ck_markdown_CheckedChanged(object sender, EventArgs e)
        {
            LLMSystem.MarkdownMemoryFormating = ck_markdown.Checked;
        }

        private void bt_deleteAllHistory_Click(object sender, EventArgs e)
        {
            // Confirm before deleting
            if (MessageBox.Show("This will delete all chat history with this character permanently. Are you sure?", "Delete All History?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LLMSystem.History.DeleteAll(true);
                var message = new SingleMessage(AuthorRole.Assistant, DateTime.Now, LLMSystem.Bot.GetWelcomeLine(LLMSystem.User.Name), LLMSystem.Bot.UniqueName, LLMSystem.Bot.UniqueName);
                LLMSystem.History.LogMessage(message);
                LoadChatHistoryTab();
                WebChatLoad();
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
    }
}
