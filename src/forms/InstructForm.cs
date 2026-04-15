using LetheAISharp;
using LetheAISharp.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LetheChat.Forms
{
    public partial class InstructForm : Form
    {
        private InstructFormat SelectedInstructEditor = new();
        private bool disableevents = false;

        public InstructForm()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        public void SetupInstructEditor(string Forceid = "")
        {
            listInstruct.Items.Clear();
            disableevents = true;
            foreach (var item in DataFiles.Instruct)
            {
                listInstruct.Items.Add(item.Value.UniqueName);
            }
            var idwant = 0;
            if (Forceid != "")
                idwant = listInstruct.Items.IndexOf(Forceid);
            if (listInstruct.Items.Count > 0 && idwant != -1)
            {
                listInstruct.SelectedIndex = idwant;
                SelectedInstructEditor = DataFiles.Instruct[listInstruct.SelectedItem!.ToString()!];
            }
            edInstruct.Text = SelectedInstructEditor.UniqueName;
            InstructToUI(SelectedInstructEditor);
            disableevents = false;
        }

        /// <summary>
        /// Create the editor for the instruction format Program.Settings
        /// </summary>
        /// <param name="target"></param>
        /// <param name="instructsetting"></param>
        private void InstructToUI(InstructFormat instructsetting)
        {
            // Message Settings
            ed_bos.Text = instructsetting.BoSToken.Replace("\n", "\\n");
            ed_botprefix.Text = instructsetting.BotStart.Replace("\n", "\\n");
            ed_botsuffix.Text = instructsetting.BotEnd.Replace("\n", "\\n");
            ed_userprefix.Text = instructsetting.UserStart.Replace("\n", "\\n");
            ed_usersuffix.Text = instructsetting.UserEnd.Replace("\n", "\\n");
            ed_sysprefix.Text = instructsetting.SystemStart.Replace("\n", "\\n");
            ed_syssuffix.Text = instructsetting.SystemEnd.Replace("\n", "\\n");
            ck_newlines.Checked = instructsetting.NewLinesBetweenMessages;

            // Thinking settings
            ed_thinkstart.Text = instructsetting.ThinkingStart.Replace("\n", "\\n");
            ed_thinkend.Text = instructsetting.ThinkingEnd.Replace("\n", "\\n");
            ed_thinkgroup.Text = instructsetting.GroupThinkingPrefix.Replace("\n", "\\n");
            ed_thinksysprefix.Text = instructsetting.ThinkingSystemPromptPrefix.Replace("\n", "\\n");
            ed_thinksyssuffix.Text = instructsetting.ThinkingSystemPromptSuffix.Replace("\n", "\\n");
            ed_thinkprefill.Text = instructsetting.ThinkingForcedThought.Replace("\n", "\\n");
            ck_thinkprefill.Checked = instructsetting.PrefillThinking;
            ck_emptythink.Checked = instructsetting.RequireEmptyThinkBlockWhenThinkingDisabled;

            // Flow Control settings
            ed_stopsequence.Text = instructsetting.StopSequence.Replace("\n", "\\n");
            ed_stopstrings.Text = string.Join(",", instructsetting.StopStrings);
            ed_botprefixoverride.Text = instructsetting.BotStartOverride.Replace("\n", "\\n");
            ed_botsuffixoverride.Text = instructsetting.BotEndOverride.Replace("\n", "\\n");
            ck_disablinstructstopstrings.Checked = instructsetting.NoInstructInStopString;
        }

        private InstructFormat UIToInstruct()
        {
            var instructsetting = new InstructFormat
            {
                // Message Settings
                BoSToken = ed_bos.Text.Replace("\\n", "\n"),
                BotStart = ed_botprefix.Text.Replace("\\n", "\n"),
                BotEnd = ed_botsuffix.Text.Replace("\\n", "\n"),
                UserStart = ed_userprefix.Text.Replace("\\n", "\n"),
                UserEnd = ed_usersuffix.Text.Replace("\\n", "\n"),
                SystemStart = ed_sysprefix.Text.Replace("\\n", "\n"),
                SystemEnd = ed_syssuffix.Text.Replace("\\n", "\n"),
                NewLinesBetweenMessages = ck_newlines.Checked,
                // Thinking settings
                ThinkingStart = ed_thinkstart.Text.Replace("\\n", "\n"),
                ThinkingEnd = ed_thinkend.Text.Replace("\\n", "\n"),
                GroupThinkingPrefix = ed_thinkgroup.Text.Replace("\\n", "\n"),
                ThinkingSystemPromptPrefix = ed_thinksysprefix.Text.Replace("\\n", "\n"),
                ThinkingSystemPromptSuffix = ed_thinksyssuffix.Text.Replace("\\n", "\n"),
                ThinkingForcedThought = ed_thinkprefill.Text.Replace("\\n", "\n"),
                PrefillThinking = ck_thinkprefill.Checked,
                RequireEmptyThinkBlockWhenThinkingDisabled = ck_emptythink.Checked,
                // Flow Control settings
                StopSequence = ed_stopsequence.Text.Replace("\\n", "\n"),
                StopStrings = [.. ed_stopstrings.Text.Split(',').Select(x => x.Trim())],
                BotStartOverride = ed_botprefixoverride.Text.Replace("\\n", "\n"),
                BotEndOverride = ed_botsuffixoverride.Text.Replace("\\n", "\n"),
                NoInstructInStopString = ck_disablinstructstopstrings.Checked
            };
            return instructsetting;
        }

        private void listInstruct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (disableevents || listInstruct.SelectedItem == null)
                return;
            SelectedInstructEditor = DataFiles.Instruct[listInstruct.SelectedItem!.ToString()!].Copy<InstructFormat>()!;
            edInstruct.Text = listInstruct.SelectedItem!.ToString();
            InstructToUI(SelectedInstructEditor);
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            var NewName = edInstruct.Text;
            if (string.IsNullOrWhiteSpace(NewName) || NewName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                MessageBox.Show("Please select a valid file name for the new instruction format.");
                return;
            }
            // If name already exists ask for confirmation
            if (DataFiles.Instruct.ContainsKey(NewName) && (MessageBox.Show("This instruction format already exists, do you want to overwrite it?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No))
                return;
            var newsetting = UIToInstruct();
            newsetting.UniqueName = NewName;
            DataFiles.Instruct[NewName] = newsetting;
            (newsetting as IFile).SaveToFile("data/instruct/" + NewName + ".json");
            SetupInstructEditor(NewName);
        }

        private void InstructForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(listInstruct.SelectedItem?.ToString()) || !DataFiles.Instruct.ContainsKey(listInstruct.SelectedItem!.ToString()!))
                return;
            if (MessageBox.Show($"Are you sure you want to delete this instruction format: '{listInstruct.SelectedItem}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            var name = listInstruct.SelectedItem!.ToString()!;
            DataFiles.Instruct.Remove(name);
            var path = "data/instruct/" + name + ".json";
            if (File.Exists(path))
                File.Delete(path);
            SetupInstructEditor();
        }
    }
}
