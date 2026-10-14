using LetheAISharp;
using LetheAISharp.Agent.Actions;
using LetheAISharp.LLM;
using LetheAISharp.Skills;
using LetheChat.Controls;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.IO;

namespace LetheChat.Forms
{
    /// <summary>
    /// Editor for the skill library (data/skills). Built in code, no designer file.
    /// </summary>
    public class SkillForm : Form
    {
        private readonly ModernListBox listSkills = new();
        private readonly TextBox edName = new();
        private readonly TextBox edDescription = new();
        private readonly TextBox edKeywords = new();
        private readonly TextBox edBody = new();
        private readonly ModernCheckBox ckEnabled = new();
        private readonly Label lblSource = new();
        private readonly Label lblStatus = new();
        private readonly Button btSave = new();
        private readonly Button btDraft = new();
        private readonly Button btImprove = new();
        private readonly Button btNew = new();
        private readonly Button btDelete = new();
        private readonly Button btImport = new();
        private readonly Button btFolder = new();

        private readonly List<string> _listNames = [];
        private bool _loading = false;
        private bool _dirty = false;
        private bool _generating = false;
        private string? _currentName = null; // name of the stored skill being edited (null = unsaved new skill)
        private SkillSource _currentSource = SkillSource.User;
        private List<string> _currentExtra = [];
        private CancellationTokenSource? _cts;

        public SkillForm()
        {
            Text = "Skill Editor";
            Size = new Size(1100, 700);
            MinimumSize = new Size(900, 560);
            StartPosition = FormStartPosition.CenterParent;
            KeyPreview = true;
            BuildLayout();
            KeyDown += SkillForm_KeyDown;
            FormClosing += SkillForm_FormClosing;
        }

        #region *** Layout ***

        private void BuildLayout()
        {
            // Left side: list + buttons
            var panLeft = new Panel { Dock = DockStyle.Left, Width = 250, Padding = new Padding(6) };

            listSkills.Dock = DockStyle.Fill;
            listSkills.BorderStyle = BorderStyle.FixedSingle;
            listSkills.Font = new Font("Segoe UI", 9.25F);
            listSkills.SelectedIndexChanged += listSkills_SelectedIndexChanged;

            var panLeftButtons = new Panel { Dock = DockStyle.Bottom, Height = 4 * 30 + 6 };
            ConfigureButton(btFolder, "Open skills folder", Color.LightSlateGray, btFolder_Click);
            ConfigureButton(btImport, "Import skill file...", Color.LightSlateGray, btImport_Click);
            ConfigureButton(btDelete, "Delete skill", Color.IndianRed, btDelete_Click);
            ConfigureButton(btNew, "New skill", Color.DarkKhaki, btNew_Click);
            // Dock bottom: last added is laid out first, so add in reverse visual order
            foreach (var b in new[] { btFolder, btImport, btDelete, btNew })
            {
                b.Dock = DockStyle.Top;
                b.Height = 26;
                panLeftButtons.Controls.Add(b);
            }
            // spacing panels between buttons are handled via Margin-less layout: keep them tight but readable
            panLeft.Controls.Add(listSkills);
            panLeft.Controls.Add(panLeftButtons);

            // Right side: form
            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 10,
                Padding = new Padding(10, 6, 10, 6)
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int i = 0; i < 9; i++)
                table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            // body row is index 8, make it fill
            table.RowStyles[8] = new RowStyle(SizeType.Percent, 100);

            edName.BorderStyle = BorderStyle.FixedSingle;
            edName.Dock = DockStyle.Top;
            edName.MaxLength = Skill.MaxNameLength;
            edName.PlaceholderText = "my-skill-name";

            edDescription.BorderStyle = BorderStyle.FixedSingle;
            edDescription.Dock = DockStyle.Top;
            edDescription.Multiline = true;
            edDescription.Height = 56;
            edDescription.ScrollBars = ScrollBars.Vertical;
            edDescription.MaxLength = Skill.MaxDescriptionLength;

            edKeywords.BorderStyle = BorderStyle.FixedSingle;
            edKeywords.Dock = DockStyle.Top;
            edKeywords.PlaceholderText = "keyword one, keyword two";

            edBody.BorderStyle = BorderStyle.FixedSingle;
            edBody.Dock = DockStyle.Fill;
            edBody.Multiline = true;
            edBody.AcceptsReturn = true;
            edBody.AcceptsTab = true;
            edBody.ScrollBars = ScrollBars.Vertical;
            edBody.Font = new Font("Consolas", 10F);
            edBody.MaxLength = 0;

            ckEnabled.Text = "Enabled (visible to the model)";
            ckEnabled.AutoSize = false;
            ckEnabled.Width = 260;
            ckEnabled.Height = 24;
            lblSource.AutoSize = true;
            lblSource.Padding = new Padding(20, 4, 0, 0);

            var panFlags = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
            panFlags.Controls.Add(ckEnabled);
            panFlags.Controls.Add(lblSource);

            table.Controls.Add(MakeLabel("Name (lowercase letters, digits and hyphens)"), 0, 0);
            table.Controls.Add(edName, 0, 1);
            table.Controls.Add(MakeLabel("Description (what the skill does and when to use it, this is what the model sees in the skill list)"), 0, 2);
            table.Controls.Add(edDescription, 0, 3);
            table.Controls.Add(MakeLabel("Keywords (optional, help automatic suggestion and search)"), 0, 4);
            table.Controls.Add(edKeywords, 0, 5);
            table.Controls.Add(panFlags, 0, 6);
            table.Controls.Add(MakeLabel("Instructions (markdown)"), 0, 7);
            table.Controls.Add(edBody, 0, 8);

            var panBottom = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, FlowDirection = FlowDirection.LeftToRight, WrapContents = true, Padding = new Padding(0, 6, 0, 0) };
            ConfigureButton(btSave, "Save skill", Color.DarkSeaGreen, btSave_Click);
            ConfigureButton(btDraft, "Write from current chat...", Color.DarkKhaki, btDraft_Click);
            ConfigureButton(btImprove, "Improve with current chat...", Color.DarkKhaki, btImprove_Click);
            btSave.Width = 130;
            btDraft.Width = 190;
            btImprove.Width = 190;
            lblStatus.AutoSize = true;
            lblStatus.Padding = new Padding(10, 6, 0, 0);
            panBottom.Controls.AddRange([btSave, btDraft, btImprove, lblStatus]);
            table.Controls.Add(panBottom, 0, 9);

            Controls.Add(table);
            Controls.Add(panLeft);

            foreach (var tb in new[] { edName, edDescription, edKeywords, edBody })
                tb.TextChanged += (_, _) => MarkDirty();
            ckEnabled.CheckedChanged += (_, _) => MarkDirty();
        }

        private static Label MakeLabel(string text) => new()
        {
            Text = text,
            AutoSize = true,
            Dock = DockStyle.Top,
            Margin = new Padding(0, 8, 0, 2)
        };

        private static void ConfigureButton(Button bt, string text, Color back, EventHandler click)
        {
            bt.Text = text;
            bt.BackColor = back;
            bt.ForeColor = Color.Black;
            bt.FlatStyle = FlatStyle.Flat;
            bt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt.Tag = "no-theme";
            bt.Height = 26;
            bt.UseVisualStyleBackColor = false;
            bt.Click += click;
        }

        #endregion

        #region *** Public API ***

        /// <summary>
        /// Fill the list and select the requested skill (or the first one).
        /// </summary>
        public void SetupSkillEditor(string forceId = "")
        {
            RefreshList(forceId);
        }

        /// <summary>
        /// Show an unsaved draft in the editor (for example one generated from the chat).
        /// </summary>
        public void LoadDraft(Skill draft)
        {
            var suggested = draft.Name;
            if (LLMEngine.SkillManager.Get(suggested) != null && draft.FolderPath.Length == 0)
                suggested = LLMEngine.SkillManager.MakeUniqueName(suggested);
            _currentName = draft.FolderPath.Length > 0 ? draft.Name : null;
            DisplaySkill(draft, suggested);
            MarkDirty();
            SetStatus("Review the draft, edit what you need, then press Save.");
        }

        #endregion

        #region *** Helpers ***

        private void SetStatus(string text) => lblStatus.Text = text;

        private void MarkDirty()
        {
            if (!_loading)
                _dirty = true;
        }

        private void RefreshList(string selectName = "")
        {
            _loading = true;
            listSkills.Items.Clear();
            _listNames.Clear();
            foreach (var s in LLMEngine.SkillManager.All)
            {
                _listNames.Add(s.Name);
                listSkills.Items.Add(s.Enabled ? s.Name : s.Name + "  (disabled)");
            }
            var idx = string.IsNullOrEmpty(selectName) ? -1 : _listNames.FindIndex(n => n.Equals(selectName, StringComparison.OrdinalIgnoreCase));
            if (idx < 0 && _listNames.Count > 0 && string.IsNullOrEmpty(selectName))
                idx = 0;
            _loading = false;
            if (idx >= 0)
            {
                listSkills.SelectedIndex = idx;
                var skill = LLMEngine.SkillManager.Get(_listNames[idx]);
                if (skill != null)
                {
                    _currentName = skill.Name;
                    DisplaySkill(skill, skill.Name);
                }
            }
            else if (_listNames.Count == 0)
            {
                _currentName = null;
                DisplaySkill(new Skill(), string.Empty);
            }
        }

        private void DisplaySkill(Skill skill, string name)
        {
            _loading = true;
            edName.Text = name;
            edDescription.Text = skill.Description;
            edKeywords.Text = string.Join(", ", skill.Keywords);
            edBody.Text = skill.Body.ToWinFormat();
            ckEnabled.Checked = skill.Enabled;
            _currentSource = skill.Source;
            _currentExtra = [.. skill.ExtraFrontmatter];
            lblSource.Text = "Source: " + skill.Source;
            _loading = false;
            _dirty = false;
        }

        private bool ConfirmDiscard()
        {
            if (!_dirty)
                return true;
            return MessageBox.Show(this, "You have unsaved changes. Discard them?", "Unsaved changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private Skill BuildSkillFromUI()
        {
            return new Skill
            {
                Name = Skill.Slugify(edName.Text),
                Description = edDescription.Text.Replace("\r\n", " ").Replace('\n', ' ').Trim(),
                Keywords = [.. edKeywords.Text.Split(',').Select(k => k.Trim()).Where(k => k.Length > 0).Distinct(StringComparer.OrdinalIgnoreCase)],
                Body = edBody.Text.ToLinuxFormat().Trim(),
                Enabled = ckEnabled.Checked,
                Source = _currentSource,
                ExtraFrontmatter = [.. _currentExtra]
            };
        }

        #endregion

        #region *** Events ***

        private void listSkills_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_loading)
                return;
            var idx = listSkills.SelectedIndex;
            if (idx < 0 || idx >= _listNames.Count)
                return;
            if (_listNames[idx].Equals(_currentName, StringComparison.OrdinalIgnoreCase))
                return;
            if (!ConfirmDiscard())
            {
                // restore previous selection
                _loading = true;
                listSkills.SelectedIndex = _currentName == null ? -1 : _listNames.FindIndex(n => n.Equals(_currentName, StringComparison.OrdinalIgnoreCase));
                _loading = false;
                return;
            }
            var skill = LLMEngine.SkillManager.Get(_listNames[idx]);
            if (skill == null)
                return;
            _currentName = skill.Name;
            DisplaySkill(skill, skill.Name);
            SetStatus(string.Empty);
        }

        private void btNew_Click(object? sender, EventArgs e)
        {
            if (!ConfirmDiscard())
                return;
            _loading = true;
            listSkills.SelectedIndex = -1;
            _loading = false;
            _currentName = null;
            DisplaySkill(new Skill(), string.Empty);
            edName.Focus();
            SetStatus("New skill.");
        }

        private void btSave_Click(object? sender, EventArgs e)
        {
            var skill = BuildSkillFromUI();
            if (_currentName == null && LLMEngine.SkillManager.Get(skill.Name) != null)
            {
                if (MessageBox.Show(this, $"A skill named '{skill.Name}' already exists. Overwrite it?", "Overwrite?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            if (_currentName != null)
            {
                var existing = LLMEngine.SkillManager.Get(_currentName);
                if (existing != null)
                    skill.FolderPath = existing.FolderPath;
            }
            var err = LLMEngine.SkillManager.Save(skill, _currentName);
            if (err.Length > 0)
            {
                MessageBox.Show(this, err, "Cannot save skill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _currentName = skill.Name;
            _dirty = false;
            RefreshList(skill.Name);
            SetStatus($"Saved '{skill.Name}'.");
        }

        private void btDelete_Click(object? sender, EventArgs e)
        {
            if (_currentName == null)
                return;
            if (MessageBox.Show(this, $"Delete the skill '{_currentName}' and its folder? This cannot be undone.", "Delete skill", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            var err = LLMEngine.SkillManager.Delete(_currentName);
            if (err.Length > 0)
            {
                MessageBox.Show(this, err, "Cannot delete skill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _currentName = null;
            _dirty = false;
            RefreshList();
            SetStatus("Skill deleted.");
        }

        private void btImport_Click(object? sender, EventArgs e)
        {
            if (!ConfirmDiscard())
                return;
            using var ofd = new OpenFileDialog
            {
                Title = "Import a skill",
                Filter = "Skill files (*.md)|*.md|All files (*.*)|*.*",
                CheckFileExists = true
            };
            if (ofd.ShowDialog(this) != DialogResult.OK)
                return;
            var skill = LLMEngine.SkillManager.Import(ofd.FileName, out var err);
            if (skill == null)
            {
                MessageBox.Show(this, err, "Import failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _dirty = false;
            RefreshList(skill.Name);
            SetStatus($"Imported '{skill.Name}'. It is disabled until you have reviewed it.");
        }

        private void btFolder_Click(object? sender, EventArgs e)
        {
            var root = LLMEngine.SkillManager.RootFolder;
            Directory.CreateDirectory(root);
            Process.Start(new ProcessStartInfo("explorer.exe", $"\"{root}\"") { UseShellExecute = true });
        }

        private async void btDraft_Click(object? sender, EventArgs e)
        {
            await GenerateAsync(improve: false);
        }

        private async void btImprove_Click(object? sender, EventArgs e)
        {
            await GenerateAsync(improve: true);
        }

        private async Task GenerateAsync(bool improve)
        {
            if (_generating)
                return;
            if (improve && _currentName == null)
            {
                MessageBox.Show(this, "Select an existing skill to improve first.", "Improve skill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ConfirmDiscard())
                return;
            var focus = Interaction.InputBox(
                "Optional: describe what the skill should capture from the current chat (leave empty to let the model decide).",
                improve ? "Improve skill" : "Write skill from chat", string.Empty);
            // InputBox returns empty string on cancel as well, which we treat as "no focus"

            var existing = improve ? LLMEngine.SkillManager.Get(_currentName) : null;
            _generating = true;
            SetGeneratingState(true);
            SetStatus("The model is writing the skill, this can take a while...");
            _cts = new CancellationTokenSource();
            try
            {
                var action = new SkillDraftAction();
                var result = await action.Execute(new SkillDraftParams { Request = focus, ExistingSkill = existing }, _cts.Token);
                if (IsDisposed)
                    return;
                if (!result.Success)
                {
                    SetStatus("Failed: " + result.Error);
                    MessageBox.Show(this, result.Error, "Could not write the skill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var draft = result.Skill!;
                if (existing != null)
                {
                    _currentName = existing.Name;
                    DisplaySkill(draft, existing.Name);
                    MarkDirty();
                    SetStatus("Improved version ready. Review it, then press Save.");
                }
                else
                {
                    LoadDraft(draft);
                }
            }
            finally
            {
                _generating = false;
                if (!IsDisposed)
                    SetGeneratingState(false);
                _cts?.Dispose();
                _cts = null;
            }
        }

        private void SetGeneratingState(bool generating)
        {
            btDraft.Enabled = !generating;
            btImprove.Enabled = !generating;
            btSave.Enabled = !generating;
            btNew.Enabled = !generating;
            btDelete.Enabled = !generating;
            btImport.Enabled = !generating;
            listSkills.Enabled = !generating;
        }

        private void SkillForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && !_generating)
            {
                Close();
            }
        }

        private void SkillForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_generating)
            {
                e.Cancel = true;
                MessageBox.Show(this, "Please wait for the model to finish writing the skill.", "Skill Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ConfirmDiscard())
                e.Cancel = true;
        }

        #endregion
    }
}
