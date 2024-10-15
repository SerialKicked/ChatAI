using NSwag.CodeGeneration.CSharp;
using NSwag;
using System.Net;
using WaifuAI.Files;
using System.Reflection;
using Newtonsoft.Json;
using System;
using Microsoft.VisualBasic.ApplicationServices;
using System.Security.AccessControl;
using Parlot.Fluent;
using Microsoft.VisualBasic.Devices;
using YamlDotNet.Core.Tokens;
using YamlDotNet.Serialization;

namespace WaifuAI
{
    public partial class MainForm : Form
    {
        public WaifuSettings Settings { get; set; } = new WaifuSettings();

        public SamplerSettings SelectedSamplerEditor { get; set; } = new SamplerSettings();
        public InstructFormat SelectedInstructEditor { get; set; } = new InstructFormat();
        public SystemPrompt SelectedPromptEditor { get; set; } = new SystemPrompt();
        private Image SystemLogo { get; } = Image.FromFile("data/img/gears.png");

        private ChatMessageControl? _lastMessageControl = null;
        private string? _currentgeneration = null;
        private int _currentgenerationtokencount = 0;
        private bool _isfillinghistory = false;

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
            button1.Click += button1_Click!;
            // Load editors and chat menu
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
            LLMChatManager.Init();
            LLMChatManager.OnInferenceStreamed += OnStreamMessageReceived;
            LLMChatManager.OnInferenceEnded += OnStreamInferenceEnded;
        }

        private void OnStreamMessageReceived(object? sender, string e)
        {
            _currentgeneration += e;
            _currentgenerationtokencount++;
            if (_currentgenerationtokencount > 8)
            {
                var MsgPrefix = LLMChatManager.GetMessagePrefix(AuthorRole.Assistant);
                _lastMessageControl?.UpdateMessage(MsgPrefix + _currentgeneration);
                _currentgenerationtokencount = 0;
                // make sure it's invoked in the application UI
                Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    flowChat.VerticalScroll.Value = flowChat.VerticalScroll.Maximum;
                });
            }
        }

        private void OnStreamInferenceEnded(object? sender, string e)
        {
            var MsgPrefix = LLMChatManager.GetMessagePrefix(AuthorRole.Assistant);
            _lastMessageControl?.UpdateMessage(MsgPrefix + e);
            var msg = LLMChatManager.Bot.History.LogMessage(AuthorRole.Assistant, e, LLMChatManager.User, LLMChatManager.Bot);
            if (_lastMessageControl != null)
            {
                _lastMessageControl.AssociatedID = msg.Guid;
                Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    _lastMessageControl.ForceResizeVerticallyToFitContent();
                });
            }

            _currentgeneration = string.Empty;
            _currentgenerationtokencount = 0;
            Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                flowChat.VerticalScroll.Value = flowChat.VerticalScroll.Maximum;
            });
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
                    control = new TextBox { Text = string.Join(",", (ICollection<int>)property.GetValue(generationInput)), Location = new Point(150, yPos), Width = 200 };
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
                    control = new TextBox { Text = ((string)property.GetValue(instructsetting)).Replace("\n", "\\n"), Location = new Point(150, yPos), Width = 400 };
                    ((TextBox)control).TextChanged += (sender, e) => property.SetValue(instructsetting, ((TextBox)control).Text.Replace("\\n", "\n"));
                }
                else if (property.PropertyType == typeof(bool))
                {
                    control = new CheckBox { Checked = (bool)property.GetValue(instructsetting), Location = new Point(150, yPos) };
                    ((CheckBox)control).CheckedChanged += (sender, e) => property.SetValue(instructsetting, ((CheckBox)control).Checked);
                }
                else if (property.PropertyType == typeof(ICollection<int>))
                {
                    control = new TextBox { Text = string.Join(",", (ICollection<int>)property.GetValue(instructsetting)), Location = new Point(150, yPos), Width = 400 };
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
                    control = new TextBox { Text = ((string)property.GetValue(promptsetting)).Replace("\n", "\\n"), Location = new Point(150, yPos), Width = 400 };
                    ((TextBox)control).TextChanged += (sender, e) => property.SetValue(promptsetting, ((TextBox)control).Text.Replace("\\n", "\n"));
                }
                else if (property.PropertyType == typeof(bool))
                {
                    control = new CheckBox { Checked = (bool)property.GetValue(promptsetting), Location = new Point(150, yPos) };
                    ((CheckBox)control).CheckedChanged += (sender, e) => property.SetValue(promptsetting, ((CheckBox)control).Checked);
                }
                else if (property.PropertyType == typeof(ICollection<int>))
                {
                    control = new TextBox { Text = string.Join(",", (ICollection<int>)property.GetValue(promptsetting)), Location = new Point(150, yPos), Width = 400 };
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
            var messagetext = LLMChatManager.GetAwayString() + ed_input.Text;
            var msg = new SingleMessage(AuthorRole.User, DateTime.Now, messagetext, LLMChatManager.Bot.UniqueName, LLMChatManager.User.UniqueName, false);
            SendMessageToUI(msg);

            // ready a new message for the bot's response
            _currentgeneration = string.Empty;
            _currentgenerationtokencount = 0;
            _lastMessageControl = SendMessageToUI(
                new SingleMessage(AuthorRole.Assistant, DateTime.Now, LLMChatManager.Bot.UniqueName + " is reading your post...", LLMChatManager.Bot.UniqueName, LLMChatManager.User.UniqueName, false));
            ed_input.Text = string.Empty;
            await LLMChatManager.SendMessageToBot(msg);
        }

        private async void RerollMessage(object sender, EventArgs e)
        {
            if (LLMChatManager.Status == LLMStatus.Busy || flowChat.Controls.Count == 0)
                return;
            _lastMessageControl = flowChat.Controls[flowChat.Controls.Count - 1] as ChatMessageControl;
            if (_lastMessageControl == null)
                return;
            _lastMessageControl.UpdateMessage(LLMChatManager.Bot.UniqueName + " is thinking...");
            _lastMessageControl.Height = 120;
            _currentgeneration = string.Empty;
            _currentgenerationtokencount = 0;
            await LLMChatManager.RerollLastMessage();
        }

        private async void Connect(object sender, EventArgs e)
        {
            await LLMChatManager.Connect();
            num_maxcontext.Maximum = LLMChatManager.MaxContextLength;
            num_maxcontext.Value = LLMChatManager.MaxContextLength;
            lbl_info.Text = LLMChatManager.CurrentModel + "\n" + LLMChatManager.Backend;
        }

        private void DeleteLastMessage(object sender, EventArgs e)
        {
            if (LLMChatManager.Status == LLMStatus.Busy || flowChat.Controls.Count == 0)
                return;
            var last = flowChat.Controls[flowChat.Controls.Count - 1] as ChatMessageControl;
            last?.Dispose();
            flowChat.Controls.RemoveAt(flowChat.Controls.Count - 1);
            LLMChatManager.History.RemoveLast();
        }

        private void LoadHistoryToUI(int maxMsg = 100)
        {
            ClearChat();
            var start = LLMChatManager.History.Messages.Count - maxMsg;
            if (start < 0)
                start = 0;
            _isfillinghistory = true;
            flowChat.SuspendLayout();
            try
            {
                for (int i = start; i < LLMChatManager.History.Messages.Count; i++)
                {
                    SendMessageToUI(LLMChatManager.History.Messages[i]);
                }
            }
            finally
            {
                flowChat.ResumeLayout();
                _isfillinghistory = false;
                flowChat.VerticalScroll.Value = flowChat.VerticalScroll.Maximum;
            }
        }

        private ChatMessageControl SendMessageToUI(SingleMessage singleMessage)
        {
            Character? sel = null;
            var msg = singleMessage;
            switch (msg.Role)
            {
                case AuthorRole.User:
                    sel = DataFiles.Characters.TryGetValue(msg.UserID, out var found) ? found : LLMChatManager.User;
                    break;
                case AuthorRole.Assistant:
                    sel = DataFiles.Characters.TryGetValue(msg.CharID, out var foundbot) ? foundbot : LLMChatManager.Bot;
                    break;
                default:
                    break;
            }
            var MsgPrefix = LLMChatManager.GetMessagePrefix(msg.Role);
            Image img = SystemLogo;

            switch (msg.Role)
            {
                case AuthorRole.User:
                    img = sel?.Portrait ?? LLMChatManager.User.Portrait;
                    break;
                case AuthorRole.Assistant:
                    img = sel?.Portrait ?? LLMChatManager.Bot.Portrait;
                    break;
            }
            var msgctrl = new ChatMessageControl(img, MsgPrefix + singleMessage.Message);
            msgctrl.AssociatedID = msg.Guid;
            flowChat.Controls.Add(msgctrl);
            flowChat.VerticalScroll.Value = flowChat.VerticalScroll.Maximum;
            msgctrl.Width = flowChat.ClientSize.Width - 20;
            return msgctrl;
        }

        private void LoadSettings()
        {
            if (File.Exists("settings.json"))
            {
                var str = File.ReadAllText("settings.json");
                Settings = JsonConvert.DeserializeObject<WaifuSettings>(str)!;
                // set cb_bot to the settings.BotFile value if it's in the list, otherwise set index to 0.
                cb_bot.SelectedIndex = cb_bot.Items.Contains(Settings.BotFile) ? cb_bot.Items.IndexOf(Settings.BotFile) : 0;
                // set cb_user to the settings.UserFile value if it's in the list, otherwise set index to 0.
                cb_user.SelectedIndex = cb_user.Items.Contains(Settings.UserFile) ? cb_user.Items.IndexOf(Settings.UserFile) : 0;
                // set cb_infer to the settings.InferenceFile value if it's in the list, otherwise set index to 0.
                cb_infer.SelectedIndex = cb_infer.Items.Contains(Settings.SamplerFile) ? cb_infer.Items.IndexOf(Settings.SamplerFile) : 0;
                // set cb_instruct to the settings.InstructFile value if it's in the list, otherwise set index to 0.
                cb_instruct.SelectedIndex = cb_instruct.Items.Contains(Settings.Instruct) ? cb_instruct.Items.IndexOf(Settings.Instruct) : 0;
                // set cb_sysprompt to the settings.PromptFile value if it's in the list, otherwise set index to 0.
                cb_sysprompt.SelectedIndex = cb_sysprompt.Items.Contains(Settings.PromptFile) ? cb_sysprompt.Items.IndexOf(Settings.PromptFile) : 0;
                num_maxcontext.Value = Settings.MaxTotalTokens;
                num_maxresponse.Value = Settings.MaxResponseTokens;
                num_temperature.Value = (decimal)Settings.Temperature;
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
                Settings.MaxTotalTokens = (int)num_maxcontext.Value;
                Settings.MaxResponseTokens = (int)num_maxresponse.Value;
                Settings.Temperature = (double)num_temperature.Value;
                var str = JsonConvert.SerializeObject(Settings, Formatting.Indented);
                File.WriteAllText("settings.json", str);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving settings: {ex.Message}");
            }
        }

        public void ClearChat()
        {
            foreach (var item in flowChat.Controls)
            {
                if (item is ChatMessageControl control)
                    control.Dispose();
            }
            flowChat.Controls.Clear();
        }

        #endregion

        #region *** API Testing Functions ***

        private async void APIGetPerformances(object sender, EventArgs e)
        {
            LLMChatManager.Init();
            try
            {
                var result = await LLMChatManager.Client.PerfAsync();
                listBox1.Items.Add("Performances: " + result.Uptime + " Last Gen Ms: " + result.Last_process + " Last Gen Tks: " + result.Last_token_count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void APIGetModelName(object sender, EventArgs e)
        {
            LLMChatManager.Init();
            try
            {
                KCBasicResult result = await LLMChatManager.Client.ModelAsync();
                listBox1.Items.Add("Model: " + result.Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void APIGetVersion(object sender, EventArgs e)
        {
            LLMChatManager.Init();
            try
            {
                KCBasicResult result = await LLMChatManager.Client.VersionAsync();
                listBox1.Items.Add("Version: " + result.Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void APIGetExtraVersion(object sender, EventArgs e)
        {
            LLMChatManager.Init();
            try
            {
                var result = await LLMChatManager.Client.ExtraVersionAsync();
                listBox1.Items.Add("Version: " + result.result + result.version);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void APIGetMaxContextLen(object sender, EventArgs e)
        {
            LLMChatManager.Init();
            try
            {
                var result = await LLMChatManager.Client.TrueMaxContextLengthAsync();
                listBox1.Items.Add("MaxLength: " + result.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void APIGetTokenCount(object sender, EventArgs e)
        {
            LLMChatManager.Init();

            var str = File.ReadAllText("data/chatlogs/Sarah.json");
            var item = JsonConvert.DeserializeObject<Chatlog>(str)!;
            var output = item.GetFormatedDialogs(16384, false);
            try
            {
                var mparams = new KcppPrompt { Prompt = output };
                var result = await LLMChatManager.Client.TokencountAsync(mparams);
                listBox1.Items.Add("Token Count: " + result.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void APIGenerate(object sender, EventArgs e)
        {
            LLMChatManager.Init();
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
                var result = await LLMChatManager.Client.GenerateAsync(mparams);
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
            LLMChatManager.Init();
            try
            {
                var mparams = new GenerationInput()
                {
                    Prompt = ed_generate.Text,
                    Max_context_length = LLMChatManager.MaxContextLength,
                    Max_length = LLMChatManager.MaxReplyLength,
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
                await LLMChatManager.Client.GenerateTextStreamAsync(mparams);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        #endregion

        private void flowChat_Resize(object sender, EventArgs e)
        {
            if (_isfillinghistory || LLMChatManager.Status == LLMStatus.Busy)
                return;
            flowChat.SuspendLayout();
            try
            {
                foreach (Control control in flowChat.Controls)
                {
                    control.Width = flowChat.ClientSize.Width - 20;
                    if (control is ChatMessageControl ctrl)
                        ctrl.ForceResizeVerticallyToFitContent();
                }
            }
            finally
            {
                flowChat.ResumeLayout();
                flowChat.VerticalScroll.Value = flowChat.VerticalScroll.Maximum;
            }
        }

        private void cb_bot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_bot.SelectedItem is string key && !string.IsNullOrEmpty(key))
            {
                ClearChat();
                LLMChatManager.Bot = DataFiles.Characters[key];
                LoadHistoryToUI(50);
            }
        }

        private void num_maxcontext_ValueChanged(object sender, EventArgs e)
        {
            LLMChatManager.MaxContextLength = (int)num_maxcontext.Value;
        }

        private void num_maxresponse_ValueChanged(object sender, EventArgs e)
        {
            LLMChatManager.MaxReplyLength = (int)num_maxresponse.Value;
        }

        private void cb_user_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_user.SelectedItem is string key && !string.IsNullOrEmpty(key))
                LLMChatManager.User = DataFiles.Characters[key];
        }

        private void cb_instruct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_instruct.SelectedItem is string key && !string.IsNullOrEmpty(key))
                LLMChatManager.Instruct = DataFiles.Instruct[key];

        }

        private void cb_infer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_infer.SelectedItem is string key && !string.IsNullOrEmpty(key))
                LLMChatManager.Sampler = DataFiles.Inference[key];
            num_temperature.Value = (decimal)LLMChatManager.Sampler.Temperature;
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
            LLMChatManager.ForceTemperature = ((double)num_temperature.Value);
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
                LLMChatManager.SystemPrompt = DataFiles.SysPrompts[key];
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            LLMChatManager.Bot.EndSession();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await LLMChatManager.History.StartNewChatSession(true);
            LoadHistoryToUI(50);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tools.Import("g:/import.jsonl", "g:/export.json", LLMChatManager.Bot.Name, LLMChatManager.User.Name);
        }
    }
}
