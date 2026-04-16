using LetheAISharp.API;
using LetheAISharp.LLM;
using LetheChat.Controls;
using LetheChat.Files;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Windows.ApplicationModel.Contacts;

namespace LetheChat.Forms
{
    public partial class ModelForm : Form
    {
        private bool _loading;
        private LocalModel? _currentModel;
        private readonly ToolTip _toolTip = new() { AutoPopDelay = 8000, InitialDelay = 400, ReshowDelay = 200 };



        public ModelForm()
        {
            InitializeComponent();
            components ??= new System.ComponentModel.Container();
            components.Add(_toolTip);
            BuildSettingsPanel();
            listModels.SelectedIndexChanged += listModels_SelectedIndexChanged!;
            KeyPreview = true;

            Program.LlamaCppProcess.OutputReceived += OnServerOutput;
            Program.LlamaCppProcess.ServerReady += OnServerReady;
        }

        private void ModelForm_Load(object sender, EventArgs e)
        {
            PopulateModelList();
            UpdateServerStatus();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Program.LlamaCppProcess.OutputReceived -= OnServerOutput;
            Program.LlamaCppProcess.ServerReady -= OnServerReady;
            base.OnFormClosed(e);
        }

        private void verticalStackPanel1_Paint(object sender, PaintEventArgs e) { }
        private void verticalStackPanel2_Paint(object sender, PaintEventArgs e) { }

        private void BuildSettingsPanel()
        {
            panSettingsScroll = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(4, 0, 4, 0)
            };

            const int rowGap = 32;
            const int colGap = 24;
            var xboxw = (boxSettings.Width - 8) / 2 - colGap;

            int labelW = (int)(xboxw / 2f);
            int ctrlX = labelW + 4;
            int ctrlW = xboxw - labelW;
            int ctrlH = 28;

            int y = 0;
            bool colleft = true;

            void AddRow(string labelText, Control ctrl, string? tip = null)
            {
                var lblloc = new Point(colleft ? 0 : xboxw + colGap, y + 4);

                var lbl = new Label
                {
                    Text = labelText,
                    Location = lblloc,
                    Size = new Size(labelW, 20),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                ctrl.Location = new Point(colleft ? ctrlX : xboxw + colGap + ctrlX, y);
                ctrl.Size = new Size(ctrlW, ctrlH);
                if (ctrl is ModernComboBox cb)
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
                }

                if (!string.IsNullOrEmpty(tip))
                {
                    HelpToolTip.SetToolTip(lbl, tip);
                    HelpToolTip.SetToolTip(ctrl, tip);
                    foreach (Control child in ctrl.Controls)
                    {
                        HelpToolTip.SetToolTip(child, tip);
                        foreach (Control grandChild in child.Controls)
                            HelpToolTip.SetToolTip(grandChild, tip);
                    }
                }
                panSettingsScroll.Controls.Add(lbl);
                panSettingsScroll.Controls.Add(ctrl);
                if (!colleft)
                    y += rowGap;
                colleft = !colleft;
            }


            void AddCheck(ModernCheckBox ck, string? tip = null)
            {
                ck.Location = new Point(colleft ? 0 : xboxw + colGap, y);
                ck.Size = new Size(xboxw, 26);
                if (!string.IsNullOrEmpty(tip))
                {
                    HelpToolTip.SetToolTip(ck, tip);
                }
                panSettingsScroll.Controls.Add(ck);
                if (!colleft)
                    y += 30;
                colleft = !colleft;
            }

            num_port = new ModernNumericUpDown { Minimum = 1, Maximum = 65535 };
            AddRow("Port", num_port, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.Port)));

            num_threads = new ModernNumericUpDown { Minimum = 0, Maximum = 256 };
            AddRow("CPU Threads", num_threads, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.Threads)));

            cb_completion = new ModernComboBox();
            cb_completion.Items.AddRange(["Default (Chat)", "Text", "Chat"]);
            cb_completion.SelectedIndex = 0;
            AddRow("Completion Type", cb_completion, HelpTool.Tip<LlamaCppSettings>(nameof(LLMEngine.Settings.DefaultCompletionType)));

            cb_instructlocal = new ModernComboBox
            {
                MaxDropDownItems = 20
            };
            // add all DataFiles.Instruct id to combo box
            cb_instructlocal.Items.Add("None");
            foreach (var instruct in DataFiles.Instruct)
                cb_instructlocal.Items.Add(instruct.Key);
            AddRow("Instruct Template", cb_instructlocal, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.LocalInstructTemplateID)));


            num_gpuLayers = new ModernNumericUpDown { Minimum = 0, Maximum = 9999 };
            AddRow("GPU Layers", num_gpuLayers, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.GpuLayers)));

            num_contextSize = new ModernNumericUpDown { Minimum = 512, Maximum = 1048576, Increment = 512 };
            AddRow("Context Size", num_contextSize, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.ContextSize)));

            cb_kvQuant = new ModernComboBox();
            cb_kvQuant.Items.AddRange(["Full", "Q8_0", "Q5_0", "Q4_0"]);
            cb_kvQuant.SelectedIndex = 0;
            AddRow("KV Cache Quantization", cb_kvQuant, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.KVCacheQuantization)));

            cb_flashAttention = new ModernComboBox();
            cb_flashAttention.Items.AddRange(["Auto", "On", "Off"]);
            cb_flashAttention.SelectedIndex = 0;
            AddRow("Flash Attention", cb_flashAttention, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.FlashAttention)));

            cb_reasoning = new ModernComboBox();
            cb_reasoning.Items.AddRange(["Auto", "On", "Off"]);
            cb_reasoning.SelectedIndex = 0;
            AddRow("Reasoning (-rea)", cb_reasoning, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.Reasoning)));

            num_reasoningBudget = new ModernNumericUpDown { Minimum = -1, Maximum = 1000000 };
            AddRow("Reasoning Budget", num_reasoningBudget, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.ReasoningBudget)));

            var lblArgs = new Label
            {
                Text = "Additional Args",
                Location = new Point(0, y + 4),
                Size = new Size(labelW, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            ed_additionalArgs = new TextBox
            {
                Location = new Point(ctrlX, y + 4),
                Size = new Size(xboxw * 2 - labelW + colGap, ctrlH),
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Extra command-line args...",
                BackColor = ThemeManager.curthemePanelColor,
                ForeColor = ThemeManager.curthemeTextColor,
                Font = ThemeManager.curthemeBaseFont
            };

            var additionalArgsTip = HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.AdditionalArgs));
            if (!string.IsNullOrEmpty(additionalArgsTip))
            {
                HelpToolTip.SetToolTip(lblArgs, additionalArgsTip);
                HelpToolTip.SetToolTip(ed_additionalArgs, additionalArgsTip);
            }
            panSettingsScroll.Controls.Add(lblArgs);
            panSettingsScroll.Controls.Add(ed_additionalArgs);
            y += rowGap + 16;

            colleft = true;
            ck_props = new ModernCheckBox { Text = "Enable Props (--props)" };
            AddCheck(ck_props, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.Props)));

            ck_kvToGpu = new ModernCheckBox { Text = "Offload KV Cache to GPU (-kvo)" };
            AddCheck(ck_kvToGpu, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.KVcacheToGPU)));

            ck_mlock = new ModernCheckBox { Text = "Memory Lock (--mlock)" };
            AddCheck(ck_mlock, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.mlock)));

            ck_mmap = new ModernCheckBox { Text = "Memory Map (--mmap)" };
            AddCheck(ck_mmap, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.mmap)));

            ck_loadMmproj = new ModernCheckBox { Text = "Auto-load mmproj (vision) if available" };
            AddCheck(ck_loadMmproj, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.LoadMMprojIfAvailable)));

            ck_loadJinja = new ModernCheckBox { Text = "Auto-load .jinja chat template if available" };
            AddCheck(ck_loadJinja, HelpTool.Tip<LlamaCppSettings>(nameof(LlamaCppSettings.LoadJinjaIfAvailable)));

            boxSettings.Controls.Add(panSettingsScroll);
            SetSettingsEnabled(false);
        }

        private void PopulateModelList()
        {
            _loading = true;
            listModels.Items.Clear();
            foreach (var model in DataFiles.LocalModels.AvailModels)
                listModels.Items.Add(model.FileName);
            _loading = false;

            if (listModels.Items.Count > 0)
            {
                listModels.SelectedIndex = 0;
                LoadModelToUI(DataFiles.LocalModels.AvailModels[0]);
            }
            else
            {
                SetSettingsEnabled(false);
            }
        }

        private void SetSettingsEnabled(bool enabled)
        {
            panSettingsScroll.Enabled = enabled;
            btApply.Enabled = enabled;
            UpdateLaunchStopButtons();
        }

        private void UpdateLaunchStopButtons()
        {
            bool managed = Program.LlamaCppProcess.IsManaged;
            bool hasModel = _currentModel != null;
            btLaunch.Enabled = managed && hasModel;
            btStop.Enabled = Program.LlamaCppProcess.IsRunning;
            if (!managed)
                btLaunch.Enabled = false;
        }

        private void UpdateServerStatus()
        {
            if (!Program.LlamaCppProcess.IsManaged)
            {
                lblServerStatus.Text = "Server management disabled (set path in Settings)";
                lblServerStatus.ForeColor = ThemeManager.curthemeMutedText;
            }
            else if (Program.LlamaCppProcess.IsRunning)
            {
                lblServerStatus.Text = "● Server Running";
                lblServerStatus.ForeColor = ThemeManager.curthemeSuccessColor;
            }
            else
            {
                lblServerStatus.Text = "○ Server Stopped";
                lblServerStatus.ForeColor = ThemeManager.curthemeTextColor;
            }
            UpdateLaunchStopButtons();
        }

        private void LoadModelToUI(LocalModel model)
        {
            _currentModel = model;
            var s = model.Settings;
            _loading = true;

            cb_completion.SelectedIndex = LLMEngine.Settings.DefaultCompletionType switch 
            { 
                CompletionType.Text => 1, 
                CompletionType.Chat => 2, 
                _ => 0 
            };
            boxSettings.Text = $"Settings - {model.FileName}";
            num_port.Value = s.Port;
            num_threads.Value = s.Threads;
            num_gpuLayers.Value = s.GpuLayers;
            num_contextSize.Value = s.ContextSize;
            cb_flashAttention.SelectedIndex = s.FlashAttention switch { true => 1, false => 2, _ => 0 };
            cb_reasoning.SelectedIndex = s.Reasoning switch { true => 1, false => 2, _ => 0 };
            cb_instructlocal.SelectedItem =
                !string.IsNullOrEmpty(s.LocalInstructTemplateID) && DataFiles.Instruct.ContainsKey(s.LocalInstructTemplateID)
                    ? s.LocalInstructTemplateID
                    : "None";
            num_reasoningBudget.Value = s.ReasoningBudget;
            ck_props.Checked = s.Props;
            ck_kvToGpu.Checked = s.KVcacheToGPU;
            ck_mlock.Checked = s.mlock;
            ck_mmap.Checked = s.mmap;
            cb_kvQuant.SelectedIndex = (int)s.KVCacheQuantization;
            ck_loadMmproj.Checked = s.LoadMMprojIfAvailable;
            ck_loadMmproj.ForcedColor = model.IsMMProjFilePresent() ? Color.Green : null;
            ck_loadMmproj.Refresh();
            ck_loadJinja.Checked = s.LoadJinjaIfAvailable;
            ck_loadJinja.ForcedColor = model.IsJinjaFilePresent() ? Color.Green : null;
            ck_loadJinja.Refresh();
            ed_additionalArgs.Text = s.AdditionalArgs;

            SetSettingsEnabled(true);
            _loading = false;
        }

        private void SaveUIToCurrentModel()
        {
            if (_currentModel == null) return;
            var s = _currentModel.Settings;

            s.Port = (int)num_port.Value;
            s.Threads = (int)num_threads.Value;
            s.GpuLayers = (int)num_gpuLayers.Value;
            s.ContextSize = (int)num_contextSize.Value;
            s.FlashAttention = cb_flashAttention.SelectedIndex switch { 1 => (bool?)true, 2 => false, _ => null };
            s.Reasoning = cb_reasoning.SelectedIndex switch { 1 => (bool?)true, 2 => false, _ => null };
            s.LocalInstructTemplateID = !string.IsNullOrEmpty(cb_instructlocal.SelectedText) && cb_instructlocal.SelectedText != "None"
                ? cb_instructlocal.SelectedText
                : "";
            s.ReasoningBudget = (int)num_reasoningBudget.Value;
            s.Props = ck_props.Checked;
            s.KVcacheToGPU = ck_kvToGpu.Checked;
            s.KVCacheQuantization = (KVCacheQuantization)cb_kvQuant.SelectedIndex;
            s.mlock = ck_mlock.Checked;
            s.mmap = ck_mmap.Checked;
            s.LoadMMprojIfAvailable = ck_loadMmproj.Checked;
            s.LoadJinjaIfAvailable = ck_loadJinja.Checked;
            s.AdditionalArgs = ed_additionalArgs.Text.Trim();

            LLMEngine.Settings.DefaultCompletionType = cb_completion.SelectedIndex switch
            {
                1 => CompletionType.Text,
                2 => CompletionType.Chat,
                _ => null,
            };
        }

        private void listModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading || listModels.SelectedIndex < 0 || listModels.SelectedIndex >= DataFiles.LocalModels.AvailModels.Count)
                return;
            LoadModelToUI(DataFiles.LocalModels.AvailModels[listModels.SelectedIndex]);
        }

        private void bt_newsession_Click_1(object sender, EventArgs e)
        {
            DataFiles.LocalModels.SearchModels(false);
            DataFiles.LocalModels.PruneModels();
            File.WriteAllText("modelDB.json", JsonConvert.SerializeObject(DataFiles.LocalModels, Formatting.Indented));
            PopulateModelList();
        }


        private const int MaxLogLines = 500;

        private void ClearServerLog()
        {
            if (InvokeRequired)
                BeginInvoke(ClearServerLog);
            else
                lvServerLog.Items.Clear();
        }

        private void AppendLogLine(string level, string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(() => AppendLogLine(level, message));
                return;
            }

            if (lvServerLog.Items.Count >= MaxLogLines)
                lvServerLog.Items.RemoveAt(0);

            var item = new ListViewItem(level);
            item.SubItems.Add(message);
            item.ForeColor = level == "INFO"
                ? ThemeManager.curthemeInfoColor
                : ThemeManager.curthemeSuccessColor;

            lvServerLog.Items.Add(item);
            lvServerLog.Items[^1].EnsureVisible();
        }

        private void OnServerOutput(object? sender, LogLineEventArgs e)
        {
            AppendLogLine(e.Level, e.Message);
        }

        private void OnServerReady(object? sender, EventArgs e)
        {
            if (InvokeRequired)
                BeginInvoke(UpdateServerStatus);
            else
                UpdateServerStatus();
        }

        private async void btLaunch_Click_1(object sender, EventArgs e)
        {
            if (_currentModel == null) return;

            SaveUIToCurrentModel();

            try
            {
                File.WriteAllText("modelDB.json", JsonConvert.SerializeObject(DataFiles.LocalModels, Formatting.Indented));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save model settings before launch:\n{ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btLaunch.Enabled = false;
            btStop.Enabled = false;
            lblServerStatus.Text = "⏳ Starting... Please wait...";
            lblServerStatus.ForeColor = ThemeManager.curthemeAccentColor;
            ClearServerLog();

            var model = _currentModel;
            bool ready = await Program.LlamaCppProcess.LaunchAsync(model);

            if (ready)
            {
                UpdateServerStatus();
                LLMEngine.Disconnect();
                try
                {
                    LLMEngine.Setup($"http://127.0.0.1:{model.Settings.Port}", BackendAPI.LlamaCpp, null, LLMEngine.Settings.DefaultCompletionType);
                    await LLMEngine.Connect();
                    DialogResult = DialogResult.OK;
                    await Program.BigForm!.RefreshConnectionState();
                    Program.BigForm!.SetInstruct(model.Settings.LocalInstructTemplateID);
                    Close();
                }
                catch (Exception ex)
                {
                    UpdateServerStatus();
                    MessageBox.Show($"Server started but connection failed:\n{ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                UpdateServerStatus();
                MessageBox.Show("Failed to start llama-server (timeout or error). Check the server path in Settings.", "Launch Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btApply_Click_1(object sender, EventArgs e)
        {
            SaveUIToCurrentModel();
            File.WriteAllText("modelDB.json", JsonConvert.SerializeObject(DataFiles.LocalModels, Formatting.Indented));
            MessageBox.Show("Model settings saved.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btStop_Click(object sender, EventArgs e)
        {
            btStop.Enabled = false;
            btLaunch.Enabled = false;
            lblServerStatus.Text = "⏳ Stopping...";
            lblServerStatus.ForeColor = ThemeManager.curthemeMutedText;

            await Program.LlamaCppProcess.KillAsync();
            LLMEngine.Disconnect();
            UpdateServerStatus();
        }

        private void btLoadFolders_Click(object sender, EventArgs e)
        {
            using var dlg = new ModelDirectoriesForm();
            if (dlg.ShowDialog(this) != DialogResult.OK)
                return;

            DataFiles.LocalModels.SearchModels(false);
            DataFiles.LocalModels.PruneModels();
            File.WriteAllText("modelDB.json", JsonConvert.SerializeObject(DataFiles.LocalModels, Formatting.Indented));
            PopulateModelList();
        }

        private void ModelForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}

