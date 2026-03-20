using LetheChat.Controls;
using LetheChat.Files;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace LetheChat.src.forms
{
    public partial class ModelForm : Form
    {
        private bool _loading;
        private LocalModel? _currentModel;
        private readonly ToolTip _toolTip = new() { AutoPopDelay = 8000, InitialDelay = 400, ReshowDelay = 200 };

        private static string? Tip(string propertyName) =>
            typeof(LlamaCppSettings)
                .GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance)
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description;

        public ModelForm()
        {
            InitializeComponent();
            components ??= new System.ComponentModel.Container();
            components.Add(_toolTip);
            BuildSettingsPanel();
            listModels.SelectedIndexChanged += listModels_SelectedIndexChanged;
            btApply.Click += btApply_Click;
            bt_newsession.Click += bt_newsession_Click;
        }

        private void ModelForm_Load(object sender, EventArgs e)
        {
            PopulateModelList();
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

            const int labelW = 200;
            const int ctrlX = 205;
            const int ctrlW = 370;
            const int ctrlH = 28;
            const int rowGap = 34;
            int y = 0;

            void AddRow(string labelText, Control ctrl, string? tip = null)
            {
                var lbl = new Label
                {
                    Text = labelText,
                    Location = new Point(0, y + 6),
                    Size = new Size(labelW, 20),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                ctrl.Location = new Point(ctrlX, y);
                ctrl.Size = new Size(ctrlW, ctrlH);
                if (tip != null)
                {
                    _toolTip.SetToolTip(lbl, tip);
                    _toolTip.SetToolTip(ctrl, tip);
                }
                panSettingsScroll.Controls.Add(lbl);
                panSettingsScroll.Controls.Add(ctrl);
                y += rowGap;
            }

            num_port = new ModernNumericUpDown { Minimum = 1, Maximum = 65535 };
            AddRow("Port", num_port, Tip(nameof(LlamaCppSettings.Port)));

            num_threads = new ModernNumericUpDown { Minimum = 0, Maximum = 256 };
            AddRow("CPU Threads", num_threads, Tip(nameof(LlamaCppSettings.Threads)));

            num_gpuLayers = new ModernNumericUpDown { Minimum = 0, Maximum = 9999 };
            AddRow("GPU Layers (-ngl)", num_gpuLayers, Tip(nameof(LlamaCppSettings.GpuLayers)));

            num_contextSize = new ModernNumericUpDown { Minimum = 512, Maximum = 1048576, Increment = 512 };
            AddRow("Context Size (-c)", num_contextSize, Tip(nameof(LlamaCppSettings.ContextSize)));

            cb_flashAttention = new ModernComboBox();
            cb_flashAttention.Items.AddRange(new object[] { "Auto", "On", "Off" });
            cb_flashAttention.SelectedIndex = 0;
            AddRow("Flash Attention (-fa)", cb_flashAttention, Tip(nameof(LlamaCppSettings.FlashAttention)));

            cb_reasoning = new ModernComboBox();
            cb_reasoning.Items.AddRange(new object[] { "Auto", "On", "Off" });
            cb_reasoning.SelectedIndex = 0;
            AddRow("Reasoning (-rea)", cb_reasoning, Tip(nameof(LlamaCppSettings.Reasoning)));

            num_reasoningBudget = new ModernNumericUpDown { Minimum = -1, Maximum = 1000000 };
            AddRow("Reasoning Budget", num_reasoningBudget, Tip(nameof(LlamaCppSettings.ReasoningBudget)));

            var lblArgs = new Label
            {
                Text = "Additional Args",
                Location = new Point(0, y + 6),
                Size = new Size(labelW, 20),
                TextAlign = ContentAlignment.MiddleLeft
            };
            ed_additionalArgs = new TextBox
            {
                Location = new Point(ctrlX, y),
                Size = new Size(ctrlW, ctrlH),
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Extra command-line args...",
                BackColor = ThemeManager.curthemePanelColor,
                ForeColor = ThemeManager.curthemeTextColor,
                Font = ThemeManager.curthemeBaseFont
            };
            var additionalArgsTip = Tip(nameof(LlamaCppSettings.AdditionalArgs));
            if (additionalArgsTip != null)
            {
                _toolTip.SetToolTip(lblArgs, additionalArgsTip);
                _toolTip.SetToolTip(ed_additionalArgs, additionalArgsTip);
            }
            panSettingsScroll.Controls.Add(lblArgs);
            panSettingsScroll.Controls.Add(ed_additionalArgs);
            y += rowGap + 8;

            void AddCheck(ModernCheckBox ck, string? tip = null)
            {
                ck.Location = new Point(0, y);
                ck.Size = new Size(labelW + ctrlW + 5, 26);
                if (tip != null)
                    _toolTip.SetToolTip(ck, tip);
                panSettingsScroll.Controls.Add(ck);
                y += 32;
            }

            ck_props = new ModernCheckBox { Text = "Enable Props (--props)" };
            AddCheck(ck_props, Tip(nameof(LlamaCppSettings.Props)));

            ck_kvToGpu = new ModernCheckBox { Text = "Offload KV Cache to GPU (-kvo)" };
            AddCheck(ck_kvToGpu, Tip(nameof(LlamaCppSettings.KVcacheToGPU)));

            ck_mlock = new ModernCheckBox { Text = "Memory Lock (--mlock)" };
            AddCheck(ck_mlock, Tip(nameof(LlamaCppSettings.mlock)));

            ck_mmap = new ModernCheckBox { Text = "Memory Map (--mmap)" };
            AddCheck(ck_mmap, Tip(nameof(LlamaCppSettings.mmap)));

            ck_loadMmproj = new ModernCheckBox { Text = "Auto-load mmproj (vision) if available" };
            AddCheck(ck_loadMmproj, Tip(nameof(LlamaCppSettings.LoadMMprojIfAvailable)));

            ck_loadJinja = new ModernCheckBox { Text = "Auto-load .jinja chat template if available" };
            AddCheck(ck_loadJinja, Tip(nameof(LlamaCppSettings.LoadJinjaIfAvailable)));

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
        }

        private void LoadModelToUI(LocalModel model)
        {
            _currentModel = model;
            var s = model.Settings;
            _loading = true;

            num_port.Value = s.Port;
            num_threads.Value = s.Threads;
            num_gpuLayers.Value = s.GpuLayers;
            num_contextSize.Value = s.ContextSize;
            cb_flashAttention.SelectedIndex = s.FlashAttention switch { true => 1, false => 2, _ => 0 };
            cb_reasoning.SelectedIndex = s.Reasoning switch { true => 1, false => 2, _ => 0 };
            num_reasoningBudget.Value = s.ReasoningBudget;
            ck_props.Checked = s.Props;
            ck_kvToGpu.Checked = s.KVcacheToGPU;
            ck_mlock.Checked = s.mlock;
            ck_mmap.Checked = s.mmap;
            ck_loadMmproj.Checked = s.LoadMMprojIfAvailable;
            ck_loadJinja.Checked = s.LoadJinjaIfAvailable;
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
            s.ReasoningBudget = (int)num_reasoningBudget.Value;
            s.Props = ck_props.Checked;
            s.KVcacheToGPU = ck_kvToGpu.Checked;
            s.mlock = ck_mlock.Checked;
            s.mmap = ck_mmap.Checked;
            s.LoadMMprojIfAvailable = ck_loadMmproj.Checked;
            s.LoadJinjaIfAvailable = ck_loadJinja.Checked;
            s.AdditionalArgs = ed_additionalArgs.Text.Trim();
        }

        private void listModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading || listModels.SelectedIndex < 0 || listModels.SelectedIndex >= DataFiles.LocalModels.AvailModels.Count)
                return;
            LoadModelToUI(DataFiles.LocalModels.AvailModels[listModels.SelectedIndex]);
        }

        private void btApply_Click(object sender, EventArgs e)
        {
            SaveUIToCurrentModel();
            File.WriteAllText("modelDB.json", JsonConvert.SerializeObject(DataFiles.LocalModels, Formatting.Indented));
            MessageBox.Show("Model settings saved.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bt_newsession_Click(object sender, EventArgs e)
        {
            DataFiles.LocalModels.SearchModels(false);
            DataFiles.LocalModels.PruneModels();
            File.WriteAllText("modelDB.json", JsonConvert.SerializeObject(DataFiles.LocalModels, Formatting.Indented));
            PopulateModelList();
        }

        private void bt_newsession_Click_1(object sender, EventArgs e)
        {

        }
    }
}
