using LetheAISharp;
using LetheAISharp.Agent;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaifuAI.Files;

namespace WaifuAI.src.forms
{
    public partial class CharEditForm : Form
    {
        private Character SelectedCharacter = new();

        public CharEditForm()
        {
            InitializeComponent();
        }

        public void SetupCharacterEditor(string Forceid = "", bool addEvents = true)
        {
            cb_charlist.Items.Clear();
            foreach (var item in DataFiles.Characters)
            {
                cb_charlist.Items.Add(item.Value.UniqueName);
            }
            cb_pointsystems.Items.Clear();
            foreach (var item in DataFiles.Points)
            {
                cb_pointsystems.Items.Add(item.Key);
            }

            var idwant = 0;
            if (Forceid != "")
                idwant = cb_charlist.Items.IndexOf(Forceid);
            if (cb_charlist.Items.Count > 0)
            {
                cb_charlist.SelectedIndex = idwant;
                SelectedCharacter = DataFiles.Characters[cb_charlist.SelectedItem!.ToString()!].Copy<Character>()!;
            }
            if (addEvents)
            {
                cb_charlist.SelectedIndexChanged += (sender, e) =>
                {
                    SelectedCharacter = DataFiles.Characters[cb_charlist.SelectedItem!.ToString()!].Copy<Character>()!;
                    LoadCharacterSettings(SelectedCharacter);
                };

                cb_icon.Items.Clear();
                // load all the png and jpg files in the data/img folder
                var files = Directory.GetFiles("data/img", "*.png");
                files = [.. files, .. Directory.GetFiles("data/img", "*.jpg")];
                foreach (var item in files)
                {
                    cb_icon.Items.Add(Path.GetFileName(item));
                }
            }
            LoadCharacterSettings(SelectedCharacter);
        }

        private Character SaveUIToCharacter()
        {
            var mychar = SelectedCharacter.Copy<Character>()!;
            mychar.Name = ed_name.Text;
            mychar.Bio = ed_bio.Text.ToLinuxFormat();
            mychar.Scenario = ed_scenario.Text.ToLinuxFormat();
            mychar.FirstMessage = [.. ed_firstmessage.Lines];
            mychar.IsUser = ck_isuser.Checked;
            mychar.SystemPrompt = ed_sysprompt.Text.ToLinuxFormat();
            mychar.TTSVoice = ed_outetts.Text;
            mychar.ExampleDialogs = [.. ed_writingstyle.Lines];
            mychar.CanInitiateChat = ck_caninitchat.Checked;
            mychar.SenseOfTime = ck_senseoftime.Checked;
            mychar.SelfEditTokens = (int)num_selfedittokens.Value;
            mychar.SelfEditField = ed_selfedit.Text.ToLinuxFormat();
            mychar.Icon = cb_icon.SelectedText ?? string.Empty;
            mychar.Plugins = [.. ckl_plugins.CheckedItems.Cast<string>()];
            mychar.Worlds = [.. ckl_worldinfo.CheckedItems.Cast<string>()];
            mychar.AllowedSamplers = [.. ckl_samplers.CheckedItems.Cast<string>()];
            mychar.PointSystem = (cb_pointsystems.SelectedIndex != -1) ? cb_pointsystems.Text : string.Empty;
            mychar.PointValue = (int)num_ptvalue.Value;
            mychar.DatesInSessionSummaries = ck_irldates.Checked;
            mychar.Protected = ckPassword.Checked;
            mychar.UniqueName = edFilename.Text;
            mychar.Brain.MoodHandling = ckMoodSystem.Checked;
            mychar.Brain.DisableEurekas = !ckAllowEurekas.Checked;
            mychar.Brain.HoursBeforeAFK = (float)num_minAFK.Value;
            mychar.Brain.MinMessageDelay = (int)numEurekaMinMess.Value;
            mychar.Brain.MinInsertDelay = TimeSpan.FromHours((double)numEurekaMinTime.Value);
            mychar.Brain.EurekaCutOff = TimeSpan.FromDays((int)numKeepEurekas.Value);
            mychar.Brain.Mood.Cheer = (double)moodCheer.Value;
            mychar.Brain.Mood.Curiosity = (double)moodCuriosity.Value;
            mychar.Brain.Mood.Energy = (double)moodEnergy.Value;
            mychar.Brain.Mood.Horniness = (double)moodHorny.Value;
            mychar.Brain.Mood.Sanity = (double)moodSanity.Value;
            mychar.Brain.Mood.Submission = (double)moodSub.Value;
            mychar.AgentMode = ckAgent.Checked;
            mychar.AgentTasks = [.. listAgentTasks.CheckedItems.Cast<string>()];
            mychar.DisableBotGuidance = ckNoGuidance.Checked;
            mychar.MiniBio = edMiniBio.Text.ToLinuxFormat();
            if (mychar.AgentSystem is not null)
                mychar.AgentSystem.Config.MinInactivityTime = TimeSpan.FromHours((double)numAgentDelay.Value);
            mychar.Brain.DisableRAG = [.. listNoRAGMemTypes.CheckedItems.Cast<string>().Select(s => Enum.Parse<MemoryType>(s))];
            mychar.Brain.DecayableMemories = [.. listCanDecay.CheckedItems.Cast<string>().Select(s => Enum.Parse<MemoryType>(s))];
            mychar.LockSettings.Enabled = ckTimerEnable.Checked;
            mychar.LockSettings.StatusUnlockedMessage = edTimerOff.Text.ToLinuxFormat();
            mychar.LockSettings.StatusLockedMessage = edTimerOn.Text.ToLinuxFormat();
            mychar.LockSettings.StatusLockOverMessage = edTimerExpired.Text.ToLinuxFormat();
            mychar.LockSettings.IsLocked = ckTimerActive.Checked;
            mychar.LockSettings.AutomaticUnlockOnDurationEnd = ckTimerAutoExpire.Checked;

            return mychar;
        }

        private void LoadCharacterSettings(Character selectedCharacter)
        {
            ed_name.Text = selectedCharacter.Name;
            ed_bio.Text = selectedCharacter.Bio.ToWinFormat();
            ed_scenario.Text = selectedCharacter.Scenario.ToWinFormat();
            ed_firstmessage.Clear();
            ed_firstmessage.Lines = [.. selectedCharacter.FirstMessage];
            ck_isuser.Checked = selectedCharacter.IsUser;
            ed_sysprompt.Text = selectedCharacter.SystemPrompt.ToWinFormat();
            ed_outetts.Text = selectedCharacter.TTSVoice;
            ed_writingstyle.Clear();
            ed_writingstyle.Lines = [.. selectedCharacter.ExampleDialogs];
            ck_caninitchat.Checked = selectedCharacter.CanInitiateChat;
            ck_senseoftime.Checked = selectedCharacter.SenseOfTime;
            num_selfedittokens.Value = selectedCharacter.SelfEditTokens;
            ed_selfedit.Text = selectedCharacter.SelfEditField.ToWinFormat();
            num_ptvalue.Value = selectedCharacter.PointValue;
            ck_irldates.Checked = selectedCharacter.DatesInSessionSummaries;
            ckPassword.Checked = selectedCharacter.Protected;
            ckMoodSystem.Checked = selectedCharacter.Brain.MoodHandling;
            ckAllowEurekas.Checked = !selectedCharacter.Brain.DisableEurekas;
            num_minAFK.Value = (decimal)selectedCharacter.Brain.HoursBeforeAFK;
            numEurekaMinMess.Value = selectedCharacter.Brain.MinMessageDelay;
            numEurekaMinTime.Value = (decimal)selectedCharacter.Brain.MinInsertDelay.TotalHours;
            numKeepEurekas.Value = (decimal)selectedCharacter.Brain.EurekaCutOff.TotalDays;
            ckAgent.Checked = selectedCharacter.AgentMode;
            numAgentDelay.Value = (decimal)(selectedCharacter.AgentSystem?.Config.MinInactivityTime.TotalHours ?? 0.5);
            ckNoGuidance.Checked = selectedCharacter.DisableBotGuidance;
            moodCheer.Value = (decimal)selectedCharacter.Brain.Mood.Cheer;
            moodCuriosity.Value = (decimal)selectedCharacter.Brain.Mood.Curiosity;
            moodEnergy.Value = (decimal)selectedCharacter.Brain.Mood.Energy;
            moodHorny.Value = (decimal)selectedCharacter.Brain.Mood.Horniness;
            moodSanity.Value = (decimal)selectedCharacter.Brain.Mood.Sanity;
            moodSub.Value = (decimal)selectedCharacter.Brain.Mood.Submission;
            edMiniBio.Text = selectedCharacter.MiniBio.ToWinFormat();
            edTimerExpired.Text = selectedCharacter.LockSettings.StatusLockOverMessage.ToWinFormat();
            edTimerOff.Text = selectedCharacter.LockSettings.StatusUnlockedMessage.ToWinFormat();
            edTimerOn.Text = selectedCharacter.LockSettings.StatusLockedMessage.ToWinFormat();
            ckTimerActive.Checked = selectedCharacter.LockSettings.IsLocked;
            ckTimerAutoExpire.Checked = selectedCharacter.LockSettings.AutomaticUnlockOnDurationEnd;
            ckTimerEnable.Checked = selectedCharacter.LockSettings.Enabled;


            edFilename.Text = selectedCharacter.UniqueName;
            ckl_plugins.Items.Clear();
            foreach (var item in LLMEngine.ContextPlugins)
            {
                ckl_plugins.Items.Add(item.PluginID, selectedCharacter.Plugins.Contains(item.PluginID));
            }
            ckl_worldinfo.Items.Clear();
            foreach (var item in DataFiles.WorldInfos)
            {
                ckl_worldinfo.Items.Add(item.Value.UniqueName, selectedCharacter.Worlds.Contains(item.Value.UniqueName));
            }
            ckl_samplers.Items.Clear();
            foreach (var item in DataFiles.Inference)
            {
                ckl_samplers.Items.Add(item.Value.UniqueName, selectedCharacter.AllowedSamplers.Contains(item.Value.UniqueName));
            }

            listAgentTasks.Items.Clear();
            var tasks = AgentRuntime.GetRegisteredPluginIds();
            foreach (var item in tasks)
            {
                listAgentTasks.Items.Add(item, selectedCharacter.AgentTasks.Contains(item));
            }

            listNoRAGMemTypes.Items.Clear();
            listCanDecay.Items.Clear();
            foreach (var item in Enum.GetValues<MemoryType>())
            {
                listNoRAGMemTypes.Items.Add(item.ToString(), selectedCharacter.Brain.DisableRAG.Contains(item));
                listCanDecay.Items.Add(item.ToString(), selectedCharacter.Brain.DecayableMemories.Contains(item));
            }

            // set cb_icon item index to the one that matches the selected character icon
            cb_icon.SelectedIndex = cb_icon.Items.IndexOf(selectedCharacter.Icon);

            cb_pointsystems.SelectedIndex = !string.IsNullOrWhiteSpace(selectedCharacter.PointSystem) && DataFiles.Points.ContainsKey(selectedCharacter.PointSystem) ? cb_pointsystems.Items.IndexOf(selectedCharacter.PointSystem) : -1;
        }

        private void bt_worldsave_Click(object sender, EventArgs e)
        {
            var NewName = edFilename.Text;
            if (string.IsNullOrWhiteSpace(NewName))
            {
                MessageBox.Show("Please select a valide name for the character");
                return;
            }
            // If name already exists ask for confirmation
            if (DataFiles.Characters.ContainsKey(NewName) && (MessageBox.Show("This character already exists, do you want to overwrite it?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No))
                return;
            SelectedCharacter = SaveUIToCharacter();
            SelectedCharacter.UniqueName = NewName;
            DataFiles.Characters[NewName] = SelectedCharacter;
            (SelectedCharacter as IFile).SaveToFile("data/chars/" + NewName + ".json");

            SetupCharacterEditor(NewName, false);

            LLMEngine.LoadPersonas([.. DataFiles.Characters.Values]);
            // Update the sampler list in the chat menu
            var currselection = cb_charlist.SelectedItem?.ToString() ?? string.Empty;
            cb_charlist.Items.Clear();
            foreach (var item in DataFiles.Characters)
            {
                cb_charlist.Items.Add(item.Value.UniqueName);
            }
            var newidx = cb_charlist.Items.IndexOf(currselection);
            cb_charlist.SelectedIndex = newidx == -1 ? 0 : newidx;

        }

        private void cb_icon_SelectedIndexChanged(object sender, EventArgs e)
        {
            // load the image from the data/img folder into the picture
            if (!File.Exists("data/img/" + cb_icon.SelectedText))
                return;
            var img = Image.FromFile("data/img/" + cb_icon.SelectedText);
            if (img != null)
            {
                pic.Image = img;
            }
        }

        private async void btRegenBio_Click(object sender, EventArgs e)
        {
            if (SelectedCharacter == null)
                return;
            SelectedCharacter.LoadChatHistory();
            if (SelectedCharacter.History.Sessions.Count < 2)
                return;
            if (num_selfedittokens.Value == 0)
            {
                ed_selfedit.Text = string.Empty;
                SelectedCharacter.SelfEditField = string.Empty;
                SelectedCharacter.SelfEditTokens = 0;
            }
            else
            {
                SelectedCharacter.SelfEditTokens = (int)num_selfedittokens.Value;
                await SelectedCharacter.UpdateSelfEditSection();
                ed_selfedit.Text = SelectedCharacter.SelfEditField.ToWinFormat();
            }
        }

        private void ckPassword_CheckedChanged(object sender, EventArgs e)
        {
            SelectedCharacter.Protected = ckPassword.Checked;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
