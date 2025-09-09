using AIToolkit.Files;
using AIToolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaifuAI.src.forms
{
    public partial class SysPromptForm : Form
    {
        public SystemPrompt SelectedPromptEditor = new();
        private bool disableevents = false;

        public SysPromptForm()
        {
            InitializeComponent();
        }

        public void SetupPromptEditor(string Forceid = "")
        {
            disableevents = true;
            listPrompt.Items.Clear();
            foreach (var item in DataFiles.SysPrompts)
            {
                listPrompt.Items.Add(item.Value.UniqueName);
            }
            var idwant = 0;
            if (Forceid != "")
                idwant = listPrompt.Items.IndexOf(Forceid);
            if (listPrompt.Items.Count > 0 && idwant != -1)
            {
                listPrompt.SelectedIndex = idwant;
                SelectedPromptEditor = DataFiles.SysPrompts[listPrompt.SelectedItem!.ToString()!].Copy<SystemPrompt>()!;
            }
            disableevents = false;
            if (SelectedPromptEditor != null)
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

        private void listPrompt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPrompt.SelectedItem == null || disableevents)
                return;
            SelectedPromptEditor = DataFiles.SysPrompts[listPrompt.SelectedItem.ToString()!].Copy<SystemPrompt>()!;
            edFileName.Text = listPrompt.SelectedItem.ToString();
            LoadSysPromptSettings(SelectedPromptEditor);
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

        private void btSave_Click(object sender, EventArgs e)
        {
            var NewName = edFileName.Text;
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
            SetupPromptEditor(NewName);
        }
    }
}
