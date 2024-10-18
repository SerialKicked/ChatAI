using System;
using System.Net;
using WaifuAI.Files;
using System.Reflection;
using Newtonsoft.Json;
using Markdig;
using WaifuAI.Memory;
using Microsoft.Web.WebView2.Core;
using WaifuAI.src.forms;
using System.Numerics;

namespace WaifuAI
{
    public partial class MainForm : Form
    {
        public WaifuSettings Settings { get; set; } = new WaifuSettings();

        public SamplerSettings SelectedSamplerEditor { get; set; } = new SamplerSettings();
        public InstructFormat SelectedInstructEditor { get; set; } = new InstructFormat();
        public SystemPrompt SelectedPromptEditor { get; set; } = new SystemPrompt();

        private string? _currentgeneration = null;
        private int _currentgenerationtokencount = 0;
        private ChatSession? _selectedSession = null;

        public MainForm()
        {
            InitializeComponent();
            // Load async API Test events
            bt_getmodel.Click += APIGetModelName!;
            bt_version.Click += APIGetVersion!;
            bt_maxctxlen.Click += APIGetMaxContextLen!;
            bt_tokencount.Click += APIGetTokenCount!;
            bt_generate.Click += APIGenerate!;
            bt_extraversion.Click += APIGetExtraVersion!;
            bt_perf.Click += APIGetPerformances!;
            bt_stream.Click += APIStreamGenerate!;
            // Chat related events
            bt_connect.Click += Connect!;
            bt_send.Click += SendMessage!;
            bt_reroll.Click += RerollMessage!;
            bt_delete.Click += DeleteLastMessage!;
            bt_chattosessions.Click += ConvertChatToSessionList!;
            bt_sessionrefresh.Click += bt_sessionrefresh_Click!;
            bt_newsession.Click += StartNewSession!;
            // Load editors and chat menu
            bt_embedall.Click += EmbedAllSessions!;
            SetupSamplerEditor();
            SetupInstructEditor();
            SetupPromptEditor();
            SetupChatMenu();
        }

        private void SetupChatMenu()
        {
            cb_bot.Items.Clear();
            cb_user.Items.Clear();
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
            LLMSystem.Init();
            LLMSystem.OnInferenceStreamed += OnStreamMessageReceived;
            LLMSystem.OnInferenceEnded += OnStreamInferenceEnded;
            LLMSystem.OnFullPromptReady += OnFullPromptReady;
        }

        private void OnFullPromptReady(object? sender, string e)
        {
            ed_log.Clear();
            var text = "====== New Generation ======\n\n" + e + "\n\n";
            ed_log.Text = text.Replace("\n", Environment.NewLine);
        }

        private async void OnStreamMessageReceived(object? sender, string e)
        {
            _currentgeneration += e;
            _currentgenerationtokencount++;
            if (_currentgenerationtokencount > 1)
            {
                _currentgenerationtokencount = 0;
                var MsgPrefix = LLMSystem.GetMessagePrefix(AuthorRole.Assistant);
                await WebEditLastMessage(MsgPrefix + _currentgeneration);
            }
        }

        private async void OnStreamInferenceEnded(object? sender, string e)
        {
            var MsgPrefix = LLMSystem.GetMessagePrefix(AuthorRole.Assistant);
            var msg = LLMSystem.Bot.History.LogMessage(AuthorRole.Assistant, e, LLMSystem.User, LLMSystem.Bot);
            await WebEditLastMessage(MsgPrefix + e);

            _currentgeneration = string.Empty;
            _currentgenerationtokencount = 0;
            Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                ShowCurrentSessionInfo();
            });
        }


        private void ShowCurrentSessionInfo()
        {
            var (tokens, duration) = LLMSystem.History.GetCurrentChatSessionInfo();
            lbl_session.Text = "Tokens: " + tokens + Environment.NewLine + "Duration: " + duration.TotalDays.ToString("F2") + " days";
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

        #region *** Editor Related Functions ***

        /// <summary>
        /// Initialize the inference settings editor panel
        /// </summary>
        /// <param name="Forceid"></param>
        private void SetupSamplerEditor(string Forceid = "")
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
            cb_samplerlist.SelectedIndexChanged += (sender, e) =>
            {
                SelectedSamplerEditor = DataFiles.Inference[cb_samplerlist.SelectedItem!.ToString()!].GetCopy();
                CreateSamplerControls(pan_samplers, SelectedSamplerEditor);
            };
            CreateSamplerControls(pan_samplers, SelectedSamplerEditor);
        }

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

        /// <summary>
        /// Initialize the instruction format editor panel
        /// </summary>
        /// <param name="Forceid"></param>
        private void SetupPromptEditor(string Forceid = "")
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
            cb_promptlist.SelectedIndexChanged += (sender, e) =>
            {
                SelectedPromptEditor = DataFiles.SysPrompts[cb_promptlist.SelectedItem!.ToString()!].Copy<SystemPrompt>()!;
                CreatePromptControls(pan_prompt, SelectedPromptEditor);
            };
            CreatePromptControls(pan_prompt, SelectedPromptEditor);
        }

        /// <summary>
        /// Create the editor for the inference sampling settings
        /// </summary>
        /// <param name="target"></param>
        /// <param name="generationInput"></param>
        private static void CreateSamplerControls(Control target, SamplerSettings generationInput)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8605 // Converting null literal or possible null value to non-nullable type.
            target.Controls.Clear();
            int yPos = 10;
            Type type = typeof(GenerationInput);
            PropertyInfo[] properties = type.GetProperties();
            int xMargin = 500;
            bool ApplyMargin = true;
            string[] ignore = ["UniqueName", "Prompt", "Memory", "Max_length", "Images", "Logit_bias", "AdditionalProperties", "Dry_sequence_breakers"];

            var lst = new List<PropertyInfo>(properties);
            // sort lst alphabetically
            lst.Sort((a, b) => a.Name.CompareTo(b.Name));

            foreach (var property in lst)
            {
                if (ignore.Contains(property.Name))
                    continue;
                Label label = new() { Text = property.Name + ":", Location = new Point(10, yPos), Width = 240 };
                Control? control = null;
                if (property.PropertyType == typeof(int))
                {
                    control = new NumericUpDown { Minimum = -1, Maximum = int.MaxValue, Value = (int)property.GetValue(generationInput), Location = new Point(150, yPos), Width = 100 };
                    ((NumericUpDown)control).ValueChanged += (sender, e) => property.SetValue(generationInput, (int)((NumericUpDown)control).Value);
                }
                else if (property.PropertyType == typeof(double))
                {
                    control = new NumericUpDown { Value = (decimal)(double)property.GetValue(generationInput), Location = new Point(150, yPos), Width = 100, DecimalPlaces = 2, Increment = 0.01M };
                    ((NumericUpDown)control).ValueChanged += (sender, e) => property.SetValue(generationInput, (double)((NumericUpDown)control).Value);
                }
                else if (property.PropertyType == typeof(string))
                {
                    control = new TextBox { Text = (string)property.GetValue(generationInput), Location = new Point(150, yPos), Width = 200 };
                    ((TextBox)control).TextChanged += (sender, e) => property.SetValue(generationInput, ((TextBox)control).Text);
                }
                else if (property.PropertyType == typeof(bool))
                {
                    control = new CheckBox { Checked = (bool)property.GetValue(generationInput), Location = new Point(150, yPos) };
                    ((CheckBox)control).CheckedChanged += (sender, e) => property.SetValue(generationInput, ((CheckBox)control).Checked);
                }
                else if (property.PropertyType == typeof(ICollection<int>))
                {
                    control = new TextBox { Text = string.Join(",", (ICollection<int>)property.GetValue(generationInput)!), Location = new Point(150, yPos), Width = 200 };
                    ((TextBox)control).TextChanged += (sender, e) => property.SetValue(generationInput, ((TextBox)control).Text.Split(',').Select(int.Parse).ToList());
                }
                else if (property.PropertyType == typeof(ICollection<string>))
                {
                    control = new TextBox { Text = string.Join(",", (ICollection<string>)property.GetValue(generationInput) ?? []), Location = new Point(150, yPos), Width = 200 };
                    ((TextBox)control).TextChanged += (sender, e) => property.SetValue(generationInput, ((TextBox)control).Text.Split(',').ToList());
                }

                if (control != null)
                {
                    if (ApplyMargin)
                    {
                        label.Location = new Point(10 + xMargin, yPos);
                        control.Location = new Point(250 + xMargin, yPos);
                    }
                    else
                    {
                        label.Location = new Point(10, yPos);
                        control.Location = new Point(250, yPos);
                        yPos += 30;
                    }

                    target.Controls.Add(label);
                    target.Controls.Add(control);
                    ApplyMargin = !ApplyMargin;
                }
            }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8605 // Converting null literal or possible null value to non-nullable type.
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

        private static void CreatePromptControls(Control target, SystemPrompt promptsetting)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8605 // Converting null literal or possible null value to non-nullable type.
            target.Controls.Clear();
            int yPos = 10;
            Type type = typeof(SystemPrompt);
            PropertyInfo[] properties = type.GetProperties();
            string[] ignore = ["UniqueName"];

            var lst = new List<PropertyInfo>(properties);
            // sort lst to match the order in InstructFormat.Properties

            foreach (var property in lst)
            {
                if (ignore.Contains(property.Name))
                    continue;
                Label label = new() { Text = property.Name + ":", Location = new Point(10, yPos), Width = 240 };
                Control? control = null;
                if (property.PropertyType == typeof(int))
                {
                    control = new NumericUpDown { Minimum = -1, Maximum = int.MaxValue, Value = (int)property.GetValue(promptsetting), Location = new System.Drawing.Point(150, yPos), Width = 100 };
                    ((NumericUpDown)control).ValueChanged += (sender, e) => property.SetValue(promptsetting, (int)((NumericUpDown)control).Value);
                }
                else if (property.PropertyType == typeof(double))
                {
                    control = new NumericUpDown { Value = (decimal)(double)property.GetValue(promptsetting), Location = new Point(150, yPos), Width = 100, DecimalPlaces = 2, Increment = 0.01M };
                    ((NumericUpDown)control).ValueChanged += (sender, e) => property.SetValue(promptsetting, (double)((NumericUpDown)control).Value);
                }
                else if (property.PropertyType == typeof(string))
                {
                    control = new TextBox { Text = ((string)property.GetValue(promptsetting)!).Replace("\n", "\\n"), Location = new Point(150, yPos), Width = 400 };
                    ((TextBox)control).TextChanged += (sender, e) => property.SetValue(promptsetting, ((TextBox)control).Text.Replace("\\n", "\n"));
                }
                else if (property.PropertyType == typeof(bool))
                {
                    control = new CheckBox { Checked = (bool)property.GetValue(promptsetting), Location = new Point(150, yPos) };
                    ((CheckBox)control).CheckedChanged += (sender, e) => property.SetValue(promptsetting, ((CheckBox)control).Checked);
                }
                else if (property.PropertyType == typeof(ICollection<int>))
                {
                    control = new TextBox { Text = string.Join(",", (ICollection<int>)property.GetValue(promptsetting)!), Location = new Point(150, yPos), Width = 400 };
                    ((TextBox)control).TextChanged += (sender, e) => property.SetValue(promptsetting, ((TextBox)control).Text.Split(',').Select(int.Parse).ToList());
                }
                else if (property.PropertyType == typeof(ICollection<string>))
                {
                    control = new TextBox { Text = string.Join(",", (ICollection<string>)property.GetValue(promptsetting) ?? []), Location = new Point(150, yPos), Width = 400 };
                    ((TextBox)control).TextChanged += (sender, e) => property.SetValue(promptsetting, ((TextBox)control).Text.Split(',').ToList());
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
            SelectedSamplerEditor.UniqueName = NewName;
            DataFiles.Inference[NewName] = SelectedSamplerEditor;
            (SelectedSamplerEditor as IFile).SaveToFile("data/params/" + NewName + ".json");
            SetupSamplerEditor(NewName);
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
        }

        #endregion

        #region *** Main Chat Functions ***

        private async void SendMessage(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ed_input.Text))
                return;
            var messagetext = LLMSystem.GetAwayString() + LLMSystem.ReplaceMacros(ed_input.Text.Replace(Environment.NewLine, LLMSystem.NewLine), LLMSystem.User, LLMSystem.Bot);
            var msg = new SingleMessage(AuthorRole.User, DateTime.Now, messagetext, LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName);
            await SendMessageToUI(msg);

            // ready a new message for the bot's response
            _currentgeneration = string.Empty;
            _currentgenerationtokencount = 0;
            await SendMessageToUI(
                new SingleMessage(AuthorRole.Assistant, DateTime.Now, "*" + LLMSystem.Bot.UniqueName + " is reading your post...*", LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName));
            ed_input.Text = string.Empty;
            await LLMSystem.SendMessageToBot(msg);
        }

        private async void RerollMessage(object sender, EventArgs e)
        {
            if (LLMSystem.Status == SystemStatus.Busy || LLMSystem.History.Messages.Count == 0)
                return;
            await WebEditLastMessage("*" + LLMSystem.Bot.UniqueName + " is thinking...*");
            _currentgeneration = string.Empty;
            _currentgenerationtokencount = 0;
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
            if (MessageBox.Show("This will archive the current chat and start a new one.", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            await LLMSystem.History.StartNewChatSession(true);
            LLMSystem.Bot.SaveChatHistory();
            await WebChatLoad();
            LoadChatHistoryTab();
        }

        private async void DeleteLastMessage(object sender, EventArgs e)
        {
            if (LLMSystem.Status == SystemStatus.Busy || LLMSystem.History.Messages.Count == 0)
                return;
            LLMSystem.RemoveLastMessage();
            await WebChatLoad();
        }

        private void LoadHistoryToUI()
        {
            WebChatLoad();
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
            var text = Markdown.ToHtml(LLMSystem.GetMessagePrefix(singleMessage.Role) + singleMessage.Message);
            var coremsg = $@"
                    <div class='portrait'>
                        <img src='https://appassets.test/img/{img}' alt='Portrait' width='50' height='67'>
                    </div>
                    <div class='message-content'>
                        {text}
                    </div>";

            coremsg = coremsg.Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r");
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
                num_maxcontext.Value = Settings.MaxTotalTokens;
                num_maxresponse.Value = Settings.MaxResponseTokens;
                num_temperature.Value = (decimal)Settings.Temperature;
                RAGSystem.Heuristic = Settings.RAGHeurisitc;
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
                RAGSystem.UseSummaries = Settings.RAGUseSummaries;
                ck_ragsummaries.Checked = Settings.RAGUseSummaries;
                RAGSystem.UseTitles = Settings.RAGUseTitles;
                ck_ragtitles.Checked = Settings.RAGUseTitles;
                RAGSystem.DistanceCutOff = Settings.RAGDistanceCutOff;
                num_ragcutoff.Value = (decimal)Settings.RAGDistanceCutOff;
                LLMSystem.MaxContextLength = Settings.MaxTotalTokens;
                LLMSystem.MaxReplyLength = Settings.MaxResponseTokens;
                LLMSystem.ReservedSessionTokens = Settings.ReservedSessionTokens;
                LLMSystem.MaxRAGEntries = Settings.MaxRAGEntries;
                num_ragmaxretrieve.Value = Settings.MaxRAGEntries;
                LLMSystem.RAGIndex = Settings.RAGPosition;
                num_ragindex.Value = Settings.RAGPosition;
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
                Settings.RAGHeurisitc = RAGSystem.Heuristic;
                Settings.RAGUseSummaries = RAGSystem.UseSummaries;
                Settings.RAGUseTitles = RAGSystem.UseTitles;
                Settings.RAGDistanceCutOff = RAGSystem.DistanceCutOff;
                Settings.ReservedSessionTokens = LLMSystem.ReservedSessionTokens;
                Settings.MaxRAGEntries = LLMSystem.MaxRAGEntries;
                Settings.RAGPosition = LLMSystem.RAGIndex;
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
                    Tools.ImportChatlog(openFileDialog1.FileName, "exported_chat.json", LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName) ?
                        "Chatlog imported successfully to exported_chat.json in this application's main folder." :
                        "Something went wrong while opening or parsing the file."
                );
        }

        private void bt_importworld_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                MessageBox.Show(
                    Tools.ImportWorld(openFileDialog1.FileName, "exported_world.json") ?
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
            LLMSystem.Bot.SaveChatHistory();
            RAGSystem.VectorizeChatlog(LLMSystem.History);
        }

        private void ApplyRAGSettings(object sender, EventArgs e)
        {
            RAGSystem.UseSummaries = ck_ragsummaries.Checked;
            RAGSystem.UseTitles = ck_ragtitles.Checked;
            RAGSystem.DistanceCutOff = (float)num_ragcutoff.Value;
            LLMSystem.MaxRAGEntries = (int)num_ragmaxretrieve.Value;
            LLMSystem.RAGIndex = (int)num_ragindex.Value;
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
            if (web_sessioncontent.CoreWebView2 == null)
            {
                await web_sessioncontent.EnsureCoreWebView2Async();
            }
            var dialogs = session.GetRawDialogs(int.MaxValue, false).Replace("\n", "\n\n");
            var inf = "# " + session.Title + LLMSystem.NewLine + LLMSystem.NewLine + "## Summary:" + LLMSystem.NewLine + LLMSystem.NewLine + session.Summary + LLMSystem.NewLine + LLMSystem.NewLine + "## Dialogs:" + LLMSystem.NewLine + LLMSystem.NewLine + dialogs;
            web_sessioncontent.NavigateToString(Markdown.ToHtml(inf));
        }

        private async void bt_sessionrefresh_Click(object sender, EventArgs e)
        {
            if (_selectedSession == null)
                return;
            _selectedSession.Summary = await _selectedSession.GenerateNewSummary();
            _selectedSession.Title = await _selectedSession.GenerateNewTitle(_selectedSession.Summary);
            DisplaySessionDetails(_selectedSession);
            LLMSystem.Bot.SaveChatHistory();
        }

        #endregion

        #region *** API Testing Functions ***

        private async void APIGetPerformances(object sender, EventArgs e)
        {
            LLMSystem.Init();
            try
            {
                var result = await LLMSystem.Client.PerfAsync();
                listBox1.Items.Add("Performances: " + result.Uptime + " Last Gen Ms: " + result.Last_process + " Last Gen Tks: " + result.Last_token_count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void APIGetModelName(object sender, EventArgs e)
        {
            LLMSystem.Init();
            try
            {
                KCBasicResult result = await LLMSystem.Client.ModelAsync();
                listBox1.Items.Add("Model: " + result.Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void APIGetVersion(object sender, EventArgs e)
        {
            LLMSystem.Init();
            try
            {
                KCBasicResult result = await LLMSystem.Client.VersionAsync();
                listBox1.Items.Add("Version: " + result.Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void APIGetExtraVersion(object sender, EventArgs e)
        {
            LLMSystem.Init();
            try
            {
                var result = await LLMSystem.Client.ExtraVersionAsync();
                listBox1.Items.Add("Version: " + result.result + result.version);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void APIGetMaxContextLen(object sender, EventArgs e)
        {
            LLMSystem.Init();
            try
            {
                var result = await LLMSystem.Client.TrueMaxContextLengthAsync();
                listBox1.Items.Add("MaxLength: " + result.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void APIGetTokenCount(object sender, EventArgs e)
        {
            LLMSystem.Init();

            var str = File.ReadAllText("data/chatlogs/Sarah.json");
            var item = JsonConvert.DeserializeObject<Chatlog>(str)!;
            var output = item.GetFormatedDialogs(16384, false, []);
            try
            {
                var mparams = new KcppPrompt { Prompt = output };
                var result = await LLMSystem.Client.TokencountAsync(mparams);
                listBox1.Items.Add("Token Count: " + result.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void APIGenerate(object sender, EventArgs e)
        {
            LLMSystem.Init();
            try
            {
                var mparams = new GenerationInput()
                {
                    Prompt = ed_generate.Text,
                    Max_context_length = 16384,
                    Max_length = 512,
                    Temperature = 0.7,
                    Top_k = 0,
                    Top_p = 1,
                    Typical = 1,
                    Min_p = 0,
                    Top_a = 0,
                    Tfs = 1,
                    Rep_pen = 1,
                    Rep_pen_range = 0,
                    Smoothing_factor = 0,
                    Xtc_threshold = 0.1,
                    Xtc_probability = 0.33,
                    Dry_allowed_length = 2,
                    Dry_base = 1.75,
                    Dry_multiplier = 0.8,
                    Dry_sequence_breakers = ["\n", ":", "\"", "*"],
                    Sampler_order = [6, 0, 1, 3, 4, 2, 5],
                    Mirostat = 0
                };
                var result = await LLMSystem.Client.GenerateAsync(mparams);
                foreach (var item in result.Results)
                {
                    listBox1.Items.Add("Generation: " + item.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void APIStreamGenerate(object sender, EventArgs e)
        {
            LLMSystem.Init();
            try
            {
                var mparams = new GenerationInput()
                {
                    Prompt = ed_generate.Text,
                    Max_context_length = LLMSystem.MaxContextLength,
                    Max_length = LLMSystem.MaxReplyLength,
                    Temperature = 0.7,
                    Top_k = 0,
                    Top_p = 1,
                    Typical = 1,
                    Min_p = 0,
                    Top_a = 0,
                    Tfs = 1,
                    Rep_pen = 1,
                    Rep_pen_range = 0,
                    Smoothing_factor = 0,
                    Xtc_threshold = 0.1,
                    Xtc_probability = 0.33,
                    Dry_allowed_length = 2,
                    Dry_base = 1.75,
                    Dry_multiplier = 0.8,
                    Dry_sequence_breakers = ["\n", ":", "\"", "*"],
                    Sampler_order = [6, 0, 1, 3, 4, 2, 5],
                    Mirostat = 0,
                };
                await LLMSystem.Client.GenerateTextStreamAsync(mparams);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void bt_apiEmbed_Click(object sender, EventArgs e)
        {
            ed_generate.Clear();
            var res = await RAGSystem.Search(ed_tokencount.Text, 5);
            foreach (var (session, category, distance) in res)
            {
                ed_generate.AppendText("[" + category.ToString() + " - " + distance.ToString("F3") + "] " + session.Title + Environment.NewLine);
            }
        }

        #endregion

        private void cb_bot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_bot.SelectedItem is string key && !string.IsNullOrEmpty(key))
            {
                LLMSystem.Bot = DataFiles.Characters[key];
                LoadHistoryToUI();
                LoadChatHistoryTab();
                ShowCurrentSessionInfo();
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
                LLMSystem.Instruct = DataFiles.Instruct[key];

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
            if (e.KeyChar == (char)13 && ModifierKeys == Keys.Shift)
            {
                e.Handled = true;
                SendMessage(sender, e);
            }
        }

        private void num_temperature_ValueChanged(object sender, EventArgs e)
        {
            LLMSystem.ForceTemperature = ((double)num_temperature.Value);
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
            SelectedPromptEditor.UniqueName = NewName;
            DataFiles.SysPrompts[NewName] = SelectedPromptEditor;
            (SelectedPromptEditor as IFile).SaveToFile("data/sysprompts/" + NewName + ".json");
            SetupPromptEditor(NewName);
        }

        private void cb_sysprompt_SelectionIndexChanged(object sender, EventArgs e)
        {
            if (cb_sysprompt.SelectedItem is string key && !string.IsNullOrEmpty(key))
                LLMSystem.SystemPrompt = DataFiles.SysPrompts[key];
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            LLMSystem.Bot.EndSession();
        }


        private static string InjectDialogCSS(string htmlContent)
        {
            string css = @"
            <style>
                body { 
                    max-height: 100%;
                    overflow-y: auto;
                    overflow-x: hidden;
                    padding: 16px;
                    width: 100%;
                    box-sizing: border-box;
                    background-image: url('https://appassets.test/ui/background.jpg');
                    background-size: cover; /* Ensures the image covers the entire background */
                    background-attachment: fixed; /* Keeps the background image fixed in place */
                    background-position: center; /* Centers the background image */
                    background-repeat: no-repeat; /* Prevents the background image from repeating */

                }
                em { color: yellow; }
                strong { color: Tomato }

                .chat-message {
                    display: flex;
                    align-items: flex-start;
                    margin-bottom: 10px;
                    border: 1px solid gray;
                    background-color: rgba(0, 0, 0, 0.75);
                    color: rgb(200, 200, 200);
                }
                .chatContainer {
                }

                .portrait {
                    flex: 0 0 70px;
                    padding: 10px;
                    margin-right: 0px;
                }

                .message-content {
                    flex: 1;
                    word-wrap: break-word;
                    padding-right: 10px;
                }


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
                document.addEventListener('DOMContentLoaded', (event) => {
                    const chatContainer = document.getElementById('chatContainer');
                    chatContainer.addEventListener('dblclick', (event) => {
                        if (event.target.classList.contains('chat-message')) {
                            const index = Array.from(chatContainer.children).indexOf(event.target);
                            window.chrome.webview.postMessage({ type: 'EditMessage', index: index + 1 });
                        }
                    });
                });            
            </script>";
            return $"<html><head>{css}</head><body>{scripts}<div id='chatContainer'>{htmlContent}<br/></div></body></html>";
        }

        private void EditMessage(int messageIndex)
        {
            var realid = LLMSystem.History.Messages.Count - Settings.MaxMessagesOnScreen;
            if (realid < 0)
                realid = 0;
            realid += messageIndex - 1;
            var editForm = new EditMessageForm(LLMSystem.History.Messages[realid].Guid);
            try
            {
                if (editForm.ShowDialog() == DialogResult.OK && editForm.Message != null)
                {
                    LoadHistoryToUI();
                    LLMSystem.InvalidatePromptCache();
                }
            }
            finally
            {
                editForm.Dispose();
            }
        }

        private static string InjectDialogHtml(string imgPath, string dialog)
        {
            // Convert relative path to absolute path and format as file URI
            return $@"
                <div class='chat-message'>
                    <div class='portrait'>
                        <img src='https://appassets.test/img/{imgPath}' alt='Portrait' width='50' height='67'>
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
                    img = LLMSystem.User.Icon;
                    break;
                case AuthorRole.Assistant:
                    img = LLMSystem.Bot.Icon;
                    break;
            }
            return InjectDialogHtml(img, Markdown.ToHtml(LLMSystem.GetMessagePrefix(singleMessage.Role) + singleMessage.Message));
        }

        private async Task WebEditLastMessage(string newMessage)
        {
            if (InvokeRequired)
            {
                await InvokeAsync(new Func<Task>(async () => await WebEditLastMessage(newMessage)));
                return;
            }
            var text = Markdown.ToHtml(newMessage);
            text = text.Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r");
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
            var text = Markdown.ToHtml(newMessage);
            text = text.Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r");
            var script = $"updateMessageAtIndex(\"{text}\", {index});";
            await web_chat.CoreWebView2.ExecuteScriptAsync(script);
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
            }
            var html = string.Empty;
            var start = LLMSystem.History.Messages.Count - Settings.MaxMessagesOnScreen;
            if (start < 0)
                start = 0;
            for (int i = start; i < LLMSystem.History.Messages.Count; i++)
            {
                html += AddHtmlMessage(LLMSystem.History.Messages[i]);
            }
            web_chat.NavigateToString(InjectDialogCSS(html));
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

    }
}
