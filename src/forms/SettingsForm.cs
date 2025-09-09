using AIToolkit;
using AIToolkit.Files;
using AIToolkit.LLM;
using AIToolkit.Memory;
using AIToolkit.SearchAPI;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WaifuAI.Files;
using WaifuAI.Plugins;
using WaifuAI.Web;

namespace WaifuAI.src.forms
{
    public partial class SettingsForm : Form
    {
        private bool _isinitloading = true;

        public SettingsForm()
        {
            InitializeComponent();
            HelptoolTip.SetToolTip(ck_webgrammar, "If checked, the LLM will be better at navigating the website, but its results will be less accurate." + Environment.NewLine + "Only enable if the LLM is consistently failing at browsing the web.");
            HelptoolTip.SetToolTip(ck_alwayswebsearch, "Normally, Online RAG (using DuckDuckGo API search) will only be attempt if you explicitely ask the bot to search the web. If you check this box, the LLM will always try to determine if a search would be useful." + Environment.NewLine + Environment.NewLine + "May lead to many false positive, and overall slower generation with some models.");
            LoadSettings();
            _isinitloading = false;
        }

        private void LoadSettings()
        {
            if (!File.Exists("settings.json"))
            {
                Program.Settings = new WaifuSettings();
                File.WriteAllText("settings.json", JsonConvert.SerializeObject(Program.Settings, Formatting.Indented));
            }

            var str = File.ReadAllText("settings.json");
            Program.Settings = JsonConvert.DeserializeObject<WaifuSettings>(str)!;
            LLMEngine.Settings = Program.Settings;

            var saveinit = _isinitloading;
            _isinitloading = true;

            // Set all the controls to their current values
            num_fontsize.Value = Program.Settings.FontSize;
            num_msgcount.Value = Program.Settings.MaxMessagesOnScreen;

            // Load background files
            cb_background.Items.Clear();
            foreach (var file in Directory.GetFiles("data/background"))
            {
                cb_background.Items.Add(Path.GetFileName(file));
            }
            cb_background.SelectedIndex = cb_background.Items.IndexOf(Program.Settings.BackgroundFile);

            num_memtokens.Value = Program.Settings.SessionReservedTokens;
            ck_sessionmemory.Checked = LLMEngine.Settings.SessionMemorySystem;

            switch (LLMEngine.Settings.RAGHeuristic)
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
            num_ragcutoff.Value = (decimal)Program.Settings.RAGDistanceCutOff;
            num_ragmaxretrieve.Value = Program.Settings.RAGMaxEntries;
            num_ragindex.Value = Program.Settings.RAGIndex;
            ck_alwayswebsearch.Checked = Program.Settings.AlwaysWebSearchQuery;
            ck_fixasterix.Checked = Program.Settings.AsteriskCheck;
            ck_antislop.Checked = Program.Settings.AntiSlop;
            num_antislopchance.Value = (decimal)Program.Settings.AntiSlopRatio;
            ck_webkeyword.Checked = Program.Settings.WebsitePluginUseKeywords;
            ck_webgrammar.Checked = Program.Settings.WebsitePluginGrammar;
            ck_unbold.Checked = Program.Settings.RoleplayFormatting.RemoveAllBoldedText;
            ck_noemphasisword.Checked = Program.Settings.RoleplayFormatting.RemoveSingleWorldEmphasis;
            ck_noquotes.Checked = Program.Settings.RoleplayFormatting.RemoveAllQuotes;
            ck_fixquotes.Checked = Program.Settings.RoleplayFormatting.FixQuotes;
            ck_reduceitalic.Checked = Program.Settings.RoleplayFormatting.RemoveItalic;
            num_italicratio.Value = (decimal)Program.Settings.RoleplayFormatting.RemoveItalicRatio;
            num_removeitalicmaxword.Value = Program.Settings.RoleplayFormatting.RemoveItalicMaxWords;
            ck_lastparaphfilter.Checked = Program.Settings.RoleplayFormatting.LastParagraphDeleter;
            cb_pastsession.SelectedIndex = (int)Program.Settings.SessionHandling;
            ck_sysrag.Checked = Program.Settings.MoveAllInsertsToSysPrompt;
            ck_remlastsentence.Checked = Program.Settings.RemoveCutSentence;
            ck_oneparagraph.Checked = Program.Settings.StopGenerationOnFirstParagraph;
            ed_sloplist.Text = Program.Settings.AntiSlopList.Length > 0 ? string.Join(",", Program.Settings.AntiSlopList) : string.Empty;
            ckShowHidden.Checked = Program.Settings.ShowHiddenMessages;

            if (LLMEngine.ContextPlugins.Find(x => x.PluginID == "WebSearch") is WebSearchPlugin searchplug)
            {
                searchplug.KeywordDetection = !ck_alwayswebsearch.Checked;
            }

            if (LLMEngine.ContextPlugins.Find(e => e is BrowsePlugin) is BrowsePlugin webplug)
            {
                webplug.EnforceCorrectGrammar = Program.Settings.WebsitePluginGrammar;
                webplug.KeywordDetection = Program.Settings.WebsitePluginUseKeywords;
            }

            _isinitloading = saveinit;
        }

        public void SaveSettings()
        {
            try
            {
                Program.Settings.FontSize = (int)num_fontsize.Value;
                Program.Settings.MaxMessagesOnScreen = (int)num_msgcount.Value;
                Program.Settings.BackgroundFile = cb_background.SelectedItem?.ToString() ?? "bedroom_cozy.jpg";
                Program.Settings.AlwaysWebSearchQuery = ck_alwayswebsearch.Checked;
                Program.Settings.SessionHandling = cb_pastsession.SelectedIndex == -1 ? SessionHandling.FitAll : (SessionHandling)cb_pastsession.SelectedIndex;
                Program.Settings.AsteriskCheck = ck_fixasterix.Checked;
                Program.Settings.AntiSlop = ck_antislop.Checked;
                Program.Settings.AntiSlopRatio = (float)num_antislopchance.Value;
                Program.Settings.AntiSlopList = !string.IsNullOrEmpty(ed_sloplist.Text) ? ed_sloplist.Text.Split(',') : [];
                Program.Settings.RoleplayFormatting.RemoveAllBoldedText = ck_unbold.Checked;
                Program.Settings.RoleplayFormatting.RemoveSingleWorldEmphasis = ck_noemphasisword.Checked;
                Program.Settings.RoleplayFormatting.RemoveAllQuotes = ck_noquotes.Checked;
                Program.Settings.RoleplayFormatting.FixQuotes = ck_fixquotes.Checked;
                Program.Settings.RoleplayFormatting.RemoveItalic = ck_reduceitalic.Checked;
                Program.Settings.RoleplayFormatting.RemoveItalicRatio = (float)num_italicratio.Value;
                Program.Settings.RoleplayFormatting.RemoveItalicMaxWords = (int)num_removeitalicmaxword.Value;
                Program.Settings.RoleplayFormatting.LastParagraphDeleter = ck_lastparaphfilter.Checked;
                Program.Settings.RemoveCutSentence = ck_remlastsentence.Checked;
                Program.Settings.StopGenerationOnFirstParagraph = ck_oneparagraph.Checked;
                Program.Settings.WebsitePluginUseKeywords = ck_webkeyword.Checked;
                Program.Settings.WebsitePluginGrammar = ck_webgrammar.Checked;
                Program.Settings.SessionMemorySystem = ck_sessionmemory.Checked;
                Program.Settings.RAGDistanceCutOff = (float)num_ragcutoff.Value;
                Program.Settings.RAGMaxEntries = (int)num_ragmaxretrieve.Value;
                Program.Settings.RAGIndex = (int)num_ragindex.Value;
                Program.Settings.MoveAllInsertsToSysPrompt = ck_sysrag.Checked;
                Program.Settings.ShowHiddenMessages = ckShowHidden.Checked;

                if (cb_ragheuristic.SelectedIndex == 0)
                    Program.Settings.RAGHeuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectHeuristic;
                else if (cb_ragheuristic.SelectedIndex == 1)
                    Program.Settings.RAGHeuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectSimple;

                var str = JsonConvert.SerializeObject(Program.Settings, Formatting.Indented);
                File.WriteAllText("settings.json", str);

                if (LLMEngine.ContextPlugins.Find(x => x.PluginID == "WebSearch") is WebSearchPlugin searchplug)
                {
                    searchplug.KeywordDetection = !ck_alwayswebsearch.Checked;
                }

                if (LLMEngine.ContextPlugins.Find(e => e is BrowsePlugin) is BrowsePlugin webplug)
                {
                    webplug.EnforceCorrectGrammar = Program.Settings.WebsitePluginGrammar;
                    webplug.KeywordDetection = Program.Settings.WebsitePluginUseKeywords;
                }

                // Apply RAG settings
                RAGEngine.ApplySettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving settings: {ex.Message}");
            }
        }

        private void bt_ImportSTChat_Click(object sender, EventArgs e)
        {
            // Open a file selection dialog and use Tools.Import to import a chatlog from a jsonl file
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                MessageBox.Show(
                    ImportTools.ImportChatlog(openFileDialog1.FileName, "exported_chat.json", LLMEngine.Bot.UniqueName, LLMEngine.User.UniqueName) ?
                        "Chatlog imported successfully to exported_chat.json in this application's main folder." :
                        "Something went wrong while opening or parsing the file."
                );
        }

        private void bt_importworld_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                MessageBox.Show(
                    ImportTools.ImportWorld(openFileDialog1.FileName, "exported_world.json") ?
                        "WorldInfo imported successfully to exported_world.json in this application's main folder." :
                        "Something went wrong while opening or parsing the file."
                );
        }

        private void ApplyRAGSettings(object sender, EventArgs e)
        {
            LLMEngine.Settings.RAGDistanceCutOff = (float)num_ragcutoff.Value;
            LLMEngine.Settings.RAGMaxEntries = (int)num_ragmaxretrieve.Value;
            LLMEngine.Settings.RAGIndex = (int)num_ragindex.Value;
            LLMEngine.Settings.MoveAllInsertsToSysPrompt = ck_sysrag.Checked;
            if (cb_ragheuristic.SelectedIndex == 0)
                LLMEngine.Settings.RAGHeuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectHeuristic;
            else if (cb_ragheuristic.SelectedIndex == 1)
                LLMEngine.Settings.RAGHeuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectSimple;
            RAGEngine.ApplySettings();
            SaveSettings();
        }

        private void cb_ragheurisitic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_ragheuristic.SelectedIndex == 0)
                LLMEngine.Settings.RAGHeuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectHeuristic;
            else if (cb_ragheuristic.SelectedIndex == 1)
                LLMEngine.Settings.RAGHeuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectSimple;
        }

        private void num_ragcutoff_ValueChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.RAGDistanceCutOff = (float)num_ragcutoff.Value;
        }

        private void num_ragmaxretrieve_ValueChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.RAGMaxEntries = (int)num_ragmaxretrieve.Value;
        }

        private void num_ragindex_ValueChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.RAGIndex = (int)num_ragindex.Value;
        }

        private void num_fontsize_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.FontSize = (int)num_fontsize.Value;
            if (!_isinitloading)
                SaveSettings();
        }

        private void cb_background_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Settings.BackgroundFile = cb_background.SelectedItem?.ToString() ?? "bedroom_cozy.jpg";
            if (!_isinitloading)
                SaveSettings();
        }

        private void ck_fixasterix_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.AsteriskCheck = ck_fixasterix.Checked;
        }

        private void ck_antislop_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.AntiSlop = ck_antislop.Checked;
        }

        private void num_antislopchance_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.AntiSlopRatio = (float)num_antislopchance.Value;
        }

        private void ed_sloplist_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.AntiSlopList = !string.IsNullOrEmpty(ed_sloplist.Text) ? ed_sloplist.Text.Split(',') : [];
        }

        private void ck_webkeyword_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isinitloading)
                SaveSettings();
        }

        private void ck_unbold_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.RemoveAllBoldedText = ck_unbold.Checked;
        }

        private void ck_noquotes_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.RemoveAllQuotes = ck_noquotes.Checked;
        }

        private void ck_fixquotes_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.FixQuotes = ck_fixquotes.Checked;
        }

        private void ck_noemphasisword_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.RemoveSingleWorldEmphasis = ck_noemphasisword.Checked;
        }

        private void ck_oneparagraph_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.StopGenerationOnFirstParagraph = ck_oneparagraph.Checked;
        }

        private void ck_remlastsentence_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.RemoveCutSentence = ck_remlastsentence.Checked;
        }

        private void ck_reduceitalic_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.RemoveItalic = ck_reduceitalic.Checked;
        }

        private void num_italicratio_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.RemoveItalicRatio = (float)num_italicratio.Value;
        }

        private void cb_pastsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isinitloading || cb_pastsession.SelectedIndex == -1)
                return;
            LLMEngine.Settings.SessionHandling = (SessionHandling)cb_pastsession.SelectedIndex;
        }

        private void num_removeitalicmaxword_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.RemoveItalicMaxWords = (int)num_removeitalicmaxword.Value;
        }

        private void ck_alwayswebsearch_CheckedChanged(object sender, EventArgs e)
        {
            // done in save button
        }

        private void ck_sysrag_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.MoveAllInsertsToSysPrompt = ck_sysrag.Checked;
        }

        private void ck_sessionmemory_CheckedChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.SessionMemorySystem = ck_sessionmemory.Checked;
        }

        private void num_memtokens_ValueChanged(object sender, EventArgs e)
        {
            LLMEngine.Settings.SessionReservedTokens = (int)num_memtokens.Value;
        }

        private async void ConvertChatToSessionList(object sender, EventArgs e)
        {
            LLMEngine.History.DivideChatIntoSessions();
            await LLMEngine.History.UpdateAllSessions();
            // The MainForm would need to refresh its chat display here
            // We could raise an event or use a callback for this
            MessageBox.Show("Chat converted to session list successfully!");
        }


        private void bt_Close_Click(object sender, EventArgs e)
        {
            SaveSettings();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ck_webgrammar_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.WebsitePluginGrammar = ck_webgrammar.Checked;
            if (!_isinitloading)
                SaveSettings();
        }

        private void ck_lastparaphfilter_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.RoleplayFormatting.LastParagraphDeleter = ck_lastparaphfilter.Checked;
        }
    }
}