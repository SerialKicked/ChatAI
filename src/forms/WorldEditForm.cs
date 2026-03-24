using LetheAISharp.Files;
using LetheAISharp.Memory;
using LetheAISharp;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LetheChat.Forms
{
    public partial class WorldEditForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WorldInfo SelectedWorldEditor { get; set; } = new WorldInfo();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MemoryUnit SelectedWorldEntryEditor { get; set; } = new MemoryUnit();

        private bool _isInitLoading = true;

        // Mapping between sorted UI list index and original WorldInfo.Entries index
        private readonly List<int> _sortedToOriginalIndexMap = [];

        public WorldEditForm()
        {
            InitializeComponent();
            SetupWorldEditor();
            _isInitLoading = false;
            KeyPreview = true;
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

            // Create sorted list with index mapping
            RefreshWorldEntriesList();

            if (lb_worldentries.Items.Count > 0)
            {
                // Find the UI index for the forced entry (original index)
                var uiIndex = _sortedToOriginalIndexMap.IndexOf(forceEntry);
                if (uiIndex == -1 || uiIndex >= lb_worldentries.Items.Count)
                    uiIndex = 0;

                var originalIndex = _sortedToOriginalIndexMap[uiIndex];
                SelectedWorldEntryEditor = selectedWorldEditor.Entries[originalIndex];
                lb_worldentries.SelectedIndex = uiIndex;
            }
        }

        private void RefreshWorldEntriesList()
        {
            lb_worldentries.Items.Clear();
            _sortedToOriginalIndexMap.Clear();

            if (SelectedWorldEditor?.Entries == null || SelectedWorldEditor.Entries.Count == 0)
                return;

            // Create indexed pairs and sort by name
            var indexedEntries = SelectedWorldEditor.Entries
                .Select((entry, index) => new { Entry = entry, OriginalIndex = index })
                .OrderBy(x => x.Entry.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();

            // Populate the listbox and mapping
            foreach (var item in indexedEntries)
            {
                lb_worldentries.Items.Add(item.Entry.Name);
                _sortedToOriginalIndexMap.Add(item.OriginalIndex);
            }
        }

        private void lb_worldentries_SelectedIndexChanged(object sender, EventArgs e)
        {
            var uiIndex = lb_worldentries.SelectedIndex;
            if (uiIndex < 0 || uiIndex >= _sortedToOriginalIndexMap.Count)
                return;

            var originalIndex = _sortedToOriginalIndexMap[uiIndex];
            if (originalIndex < 0 || originalIndex >= SelectedWorldEditor.Entries.Count)
                return;

            SelectedWorldEntryEditor = SelectedWorldEditor.Entries[originalIndex];
            LoadWorldEntry(SelectedWorldEntryEditor);
        }

        private void LoadWorldEntry(MemoryUnit worldEntry)
        {
            var sv = _isInitLoading;
            _isInitLoading = true;
            ed_wentryname.Text = worldEntry.Name;
            ed_wentrymem.Text = worldEntry.Content.ToWinFormat();
            // Convert worldEntry's keywords to a comma separated string to show in ed_wentrykw1.Text
            ed_wentrykw1.Text = string.Join(",", worldEntry.KeyWordsMain);
            ed_wentrykw2.Text = string.Join(",", worldEntry.KeyWordsSecondary);
            num_wentryduration.Value = worldEntry.Duration;
            num_wentryposition.Value = worldEntry.PositionIndex;
            num_wentrypriority.Value = worldEntry.Priority;
            cb_wentrykwlink.SelectedIndex = (int)worldEntry.WordLink;
            ck_wentrycasesensitive.Checked = worldEntry.CaseSensitive;
            ck_wentryenabled.Checked = worldEntry.Enabled;
            numWItriggerchance.Value = (decimal)worldEntry.TriggerChance;
            ckSticky.Checked = worldEntry.Sticky;
            _isInitLoading = sv;
        }

        private void SaveWorldEntry()
        {
            SelectedWorldEntryEditor.Name = ed_wentryname.Text;
            SelectedWorldEntryEditor.Category = MemoryType.WorldInfo;
            SelectedWorldEntryEditor.Insertion = MemoryInsertion.Trigger;
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
            SelectedWorldEntryEditor.CaseSensitive = ck_wentrycasesensitive.Checked;
            SelectedWorldEntryEditor.Enabled = ck_wentryenabled.Checked;
            SelectedWorldEntryEditor.TriggerChance = (float)numWItriggerchance.Value;
            SelectedWorldEntryEditor.Sticky = ckSticky.Checked;

            // Refresh the list to maintain alphabetical order after name changes
            var currentSelectedOriginalIndex = _sortedToOriginalIndexMap[lb_worldentries.SelectedIndex];
            RefreshWorldEntriesList();

            // Find the new UI position of the edited entry and reselect it
            var newUiIndex = _sortedToOriginalIndexMap.IndexOf(currentSelectedOriginalIndex);
            if (newUiIndex >= 0)
                lb_worldentries.SelectedIndex = newUiIndex;
        }

        private async Task<bool> SaveWorldInfo()
        {
            SelectedWorldEditor.Description = ed_worlddesc.Text;
            SelectedWorldEditor.ScanDepth = (int)num_scandepth.Value;
            SelectedWorldEditor.DoEmbeds = ck_wiembed.Checked;
            var NewName = cb_worlds.Text;
            if (string.IsNullOrWhiteSpace(NewName))
            {
                MessageBox.Show("Please select a valid name for the world info");
                return false;
            }
            // If name already exists ask for confirmation
            if (DataFiles.WorldInfos.ContainsKey(NewName) && (MessageBox.Show("This world info already exists, do you want to overwrite it?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No))
                return false;
            await SelectedWorldEditor.EmbedText();
            SelectedWorldEditor.UniqueName = NewName;
            DataFiles.WorldInfos[NewName] = SelectedWorldEditor;

            (SelectedWorldEditor as IFile).SaveToFile("data/worlds/" + NewName + ".json");
            return true;
        }

        private void UpdateWorldEntryEvent(object sender, EventArgs e)
        {
            if (_isInitLoading)
                return;
            SaveWorldEntry();
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
                var uiIndex = lb_worldentries.SelectedIndex;
                var originalIndex = _sortedToOriginalIndexMap[uiIndex];

                SelectedWorldEditor.Entries.RemoveAt(originalIndex);
                RefreshWorldEntriesList();

                // Select the next available entry
                if (lb_worldentries.Items.Count > 0)
                {
                    var newSelection = Math.Min(uiIndex, lb_worldentries.Items.Count - 1);
                    lb_worldentries.SelectedIndex = newSelection;
                }
            }
        }

        private void bt_addwentry_Click(object sender, EventArgs e)
        {
            SelectedWorldEntryEditor = new MemoryUnit() { Name = "New Entry" };
            SelectedWorldEditor.Entries.Add(SelectedWorldEntryEditor);

            RefreshWorldEntriesList();

            // Find and select the newly added entry in the sorted list
            var newEntryOriginalIndex = SelectedWorldEditor.Entries.Count - 1;
            var newEntryUiIndex = _sortedToOriginalIndexMap.IndexOf(newEntryOriginalIndex);
            if (newEntryUiIndex >= 0)
                lb_worldentries.SelectedIndex = newEntryUiIndex;
        }

        private void ck_wiembed_CheckedChanged(object sender, EventArgs e)
        {
            SelectedWorldEditor.DoEmbeds = ck_wiembed.Checked;
        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WorldEditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}