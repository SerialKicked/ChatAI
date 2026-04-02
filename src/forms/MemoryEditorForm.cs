using LetheAISharp;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LetheChat.Forms
{
    public partial class MemoryEditorForm : Form
    {
        private MemoryUnit? _editingMemory;
        private readonly bool _isNewMemory;

        public MemoryUnit? EditedMemory { get; private set; }

        public MemoryEditorForm(MemoryUnit? memory = null)
        {
            InitializeComponent();
            _isNewMemory = memory == null;
            _editingMemory = memory ?? CreateNewMemory();

            Text = _isNewMemory ? "Add New Memory" : "Edit Memory";
            KeyPreview = true;
            KeyDown += MemoryEditorForm_KeyDown;

            PopulateCategories();
            PopulateInsertionTypes();
            LoadMemoryData();
        }

        private static MemoryUnit CreateNewMemory()
        {
            return new MemoryUnit
            {
                Category = MemoryType.General,
                Insertion = MemoryInsertion.Trigger,
                Priority = 1,
                Duration = 1,
                TriggerChance = 1.0f,
                Enabled = true,
                Sticky = false,
                CaseSensitive = false,
                WordLink = KeyWordLink.And,
                Added = DateTime.Now,
                EndTime = DateTime.Now.AddMonths(1)
            };
        }

        private void MemoryEditorForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void PopulateCategories()
        {
            cbCategory.Items.Clear();
            foreach (var value in Enum.GetValues<MemoryType>())
                cbCategory.Items.Add(value);
        }

        private void PopulateInsertionTypes()
        {
            cbInsertion.Items.Clear();
            foreach (var value in Enum.GetValues<MemoryInsertion>())
                cbInsertion.Items.Add(value);

            cbWordLink.Items.Clear();
            foreach (var value in Enum.GetValues<KeyWordLink>())
                cbWordLink.Items.Add(value);
        }

        private void LoadMemoryData()
        {
            if (_editingMemory == null)
                return;

            edTitle.Text = _editingMemory.Name ?? string.Empty;
            edContent.Text = _editingMemory.Content?.ToWinFormat() ?? string.Empty;
            edReason.Text = _editingMemory.Reason?.ToWinFormat() ?? string.Empty;
            edPath.Text = _editingMemory.Path ?? string.Empty;

            cbCategory.SelectedItem = _editingMemory.Category;
            cbInsertion.SelectedItem = _editingMemory.Insertion;
            cbWordLink.SelectedItem = _editingMemory.WordLink;

            numPriority.Value = Math.Clamp(_editingMemory.Priority, (int)numPriority.Minimum, (int)numPriority.Maximum);
            numDuration.Value = Math.Clamp(_editingMemory.Duration, (int)numDuration.Minimum, (int)numDuration.Maximum);
            numPosition.Value = Math.Clamp(_editingMemory.PositionIndex, (int)numPosition.Minimum, (int)numPosition.Maximum);
            numTriggerChance.Value = (decimal)Math.Clamp(_editingMemory.TriggerChance, 0f, 1f);

            ckEnabled.Checked = _editingMemory.Enabled;
            ckSticky.Checked = _editingMemory.Sticky;
            ckCaseSensitive.Checked = _editingMemory.CaseSensitive;
            ckProtecc.Checked = _editingMemory.Protected;

            dtpAdded.Value = _editingMemory.Added;
            dtpEndTime.Value = _editingMemory.EndTime;

            edKeywordsMain.Text = string.Join(", ", _editingMemory.KeyWordsMain);
            edKeywordsSecondary.Text = string.Join(", ", _editingMemory.KeyWordsSecondary);

            UpdateUIForCategory();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUIForCategory();
        }

        private void UpdateUIForCategory()
        {
            if (cbCategory.SelectedItem is not MemoryType category)
                return;

            // Enable/disable fields based on category
            bool showPath = category == MemoryType.File || category == MemoryType.Image;
            lblPath.Visible = showPath;
            edPath.Visible = showPath;
            btBrowsePath.Visible = showPath;
        }

        private void btBrowsePath_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Select File",
                Filter = "All Files (*.*)|*.*",
                CheckFileExists = true
            };

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                edPath.Text = ofd.FileName;
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            SaveToMemory();
            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(edTitle.Text))
            {
                MessageBox.Show(this, "Please enter a title for the memory.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                edTitle.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(edContent.Text))
            {
                MessageBox.Show(this, "Please enter content for the memory.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                edContent.Focus();
                return false;
            }

            return true;
        }

        private void SaveToMemory()
        {
            if (_editingMemory == null)
                return;

            _editingMemory.Name = edTitle.Text.Trim();
            _editingMemory.Content = edContent.Text.Trim().ToLinuxFormat();
            _editingMemory.Reason = edReason.Text.Trim().ToLinuxFormat();
            _editingMemory.Path = edPath.Text.Trim();

            if (cbCategory.SelectedItem is MemoryType cat)
                _editingMemory.Category = cat;
            if (cbInsertion.SelectedItem is MemoryInsertion ins)
                _editingMemory.Insertion = ins;
            if (cbWordLink.SelectedItem is KeyWordLink wl)
                _editingMemory.WordLink = wl;

            _editingMemory.Priority = (int)numPriority.Value;
            _editingMemory.Duration = (int)numDuration.Value;
            _editingMemory.PositionIndex = (int)numPosition.Value;
            _editingMemory.TriggerChance = (float)numTriggerChance.Value;

            _editingMemory.Enabled = ckEnabled.Checked;
            _editingMemory.Sticky = ckSticky.Checked;
            _editingMemory.CaseSensitive = ckCaseSensitive.Checked;
            _editingMemory.Protected = ckProtecc.Checked;

            _editingMemory.Added = dtpAdded.Value;
            _editingMemory.EndTime = dtpEndTime.Value;

            // Parse keywords
            _editingMemory.KeyWordsMain = ParseKeywords(edKeywordsMain.Text);
            _editingMemory.KeyWordsSecondary = ParseKeywords(edKeywordsSecondary.Text);

            EditedMemory = _editingMemory;
        }

        private static List<string> ParseKeywords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return [];

            return text.Split(',')
                      .Select(k => k.Trim())
                      .Where(k => !string.IsNullOrWhiteSpace(k))
                      .ToList();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void btGenerateEmbedding_Click(object sender, EventArgs e)
        {
            if (_editingMemory == null)
                return;

            if (!LLMEngine.Settings.RAGEnabled)
            {
                MessageBox.Show(this, "RAG is not enabled in settings.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Save current content first
            _editingMemory.Content = edContent.Text.Trim().ToLinuxFormat();
            _editingMemory.Name = edTitle.Text.Trim();

            if (string.IsNullOrWhiteSpace(_editingMemory.Content))
            {
                MessageBox.Show(this, "Cannot generate embedding for empty content.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btGenerateEmbedding.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                await _editingMemory.BuildEmbedding();
                MessageBox.Show(this, "Embedding generated successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to generate embedding: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btGenerateEmbedding.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void ckEnabled_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
