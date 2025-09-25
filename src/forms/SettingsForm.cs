using LetheAISharp;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using LetheAISharp.SearchAPI;
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
            HelptoolTip.SetToolTip(ck_webgrammar, "If checked, the LLM will be better at navigating the website, but its results may be less accurate." + Environment.NewLine + "Only enable if the LLM is consistently failing at browsing the web.");
            HelptoolTip.SetToolTip(ck_alwayswebsearch, "Normally, the Search API will only be attempted if you explicitely ask the bot to search the web. If you check this box, the LLM will always try to determine if a search would be useful." + Environment.NewLine + Environment.NewLine + "May lead to many false positive, and overall slower generation with some models.");
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
            num_ragM.Value = Program.Settings.RAGMValue;
            ck_forcePW.Checked = Program.Settings.AlwaysForcePasswordOnBotSwitch;
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
            ck_hallusafe.Checked = Program.Settings.AntiHallucinationMemoryFormat;

            Program.ApplyContextPluginSettings();


            // Search API Settings
            switch (Program.Settings.WebSearchAPI)
            {
                case BackendSearchAPI.DuckDuckGo:
                    cb_searchapi.SelectedIndex = 0;
                    break;
                case BackendSearchAPI.Brave:
                    cb_searchapi.SelectedIndex = 1;
                    break;
                default:
                    break;
            }
            ed_searchkey.Text = Program.Settings.WebSearchBraveAPIKey;
            ck_searchextract.Checked = Program.Settings.WebSearchDetailedResults;

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
                Program.Settings.RAGMValue = (int)num_ragM.Value;
                Program.Settings.MoveAllInsertsToSysPrompt = ck_sysrag.Checked;
                Program.Settings.ShowHiddenMessages = ckShowHidden.Checked;
                Program.Settings.AntiHallucinationMemoryFormat = ck_hallusafe.Checked;
                Program.Settings.SessionReservedTokens = (int)num_memtokens.Value;
                Program.Settings.AlwaysForcePasswordOnBotSwitch = ck_forcePW.Checked;

                if (cb_ragheuristic.SelectedIndex == 0)
                    Program.Settings.RAGHeuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectHeuristic;
                else if (cb_ragheuristic.SelectedIndex == 1)
                    Program.Settings.RAGHeuristic = HNSW.Net.NeighbourSelectionHeuristic.SelectSimple;

                // Search API Settings
                if (cb_searchapi.SelectedIndex == 0)
                    Program.Settings.WebSearchAPI = BackendSearchAPI.DuckDuckGo;
                else if (cb_searchapi.SelectedIndex == 1)
                    Program.Settings.WebSearchAPI = BackendSearchAPI.Brave;
                Program.Settings.WebSearchBraveAPIKey = ed_searchkey.Text;
                Program.Settings.WebSearchDetailedResults = ck_searchextract.Checked;

                var str = JsonConvert.SerializeObject(Program.Settings, Formatting.Indented);
                File.WriteAllText("settings.json", str);
                // Context plugin settings
                Program.ApplyContextPluginSettings();
                // Apply RAG settings
                RAGEngine.ApplySettings();
                LLMEngine.Client?.UpdateSearchProvider();
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
    }
}