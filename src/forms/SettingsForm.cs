using LetheAISharp.API;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using LetheAISharp.SearchAPI;
using LetheChat.Controls;
using LetheChat.Files;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LetheChat.src.forms
{
    public partial class SettingsForm : Form
    {

        private void LoadToolTips()
        {
            HelptoolTip.SetToolTip(ck_webgrammar, "If checked, the LLM will be better at navigating the website, but its results may be less accurate." + Environment.NewLine + "Only enable if the LLM is consistently failing at browsing the web.");
            HelptoolTip.SetToolTip(ck_alwayswebsearch, "Normally, the Search API will only be attempted if you explicitely ask the bot to search the web. If you check this box, the LLM will always try to determine if a search would be useful." + Environment.NewLine + Environment.NewLine + "May lead to many false positive, and overall slower generation with some models.");
            HelptoolTip.SetToolTip(ck_sessionmemory, "If checked, the bot will remember previous chat sessions by having their summaries inserted into the system prompt.");
            HelptoolTip.SetToolTip(num_memtokens, "The number of tokens to reserve for session memory summaries. The more tokens you reserve, the more context the bot can remember from previous sessions, but the less tokens are available for the chatlog." + Environment.NewLine + "If you have a very large context window. A good rule of thumb is 2K per 16K of total context windows available.");
            HelptoolTip.SetToolTip(cb_pastsession, "Determines how the bot will handle past sessions when session memory is enabled." + Environment.NewLine + "FitAll: Fit as many sessions as possible into the chatlog. Older sessions are summarized." + Environment.NewLine + "CurrentOnly: Only the current session is kept in full, older sessions are summarized.");
            HelptoolTip.SetToolTip(ck_sysrag, "If checked, any RAG insert (sessions triggers and world info) will be moved to the system prompt instead of being inserted into the chatlog." + Environment.NewLine + "Some models behave better while contextual info into the system prompt, while others prefer having system messages in the chatlog.");
            HelptoolTip.SetToolTip(num_ragcutoff, "The distance cutoff for RAG retrieval. The lower the value, the more similar the retrieved entries will be to the query." + Environment.NewLine + "A good starting point is between 0.07 and 0.1, assuming you're using the default embedding model.");
            HelptoolTip.SetToolTip(num_ragmaxretrieve, "The maximum number of RAG entries to retrieve for each query. The more entries you retrieve, the more context the bot will have." + Environment.NewLine + "A good starting point is between 2 and 5.");
            HelptoolTip.SetToolTip(num_ragindex, "The level in the chatlog where the entries will be inserted, 0 is just above the latest message pair. " + Environment.NewLine + "You can set it to -1 to put it into the system prompt.");
            HelptoolTip.SetToolTip(num_ragM, "The M parameter for the HNSW index. Higher values lead to better quality results, but slower indexing and retrieval." + Environment.NewLine + "A good starting point is around 15-20.");
            HelptoolTip.SetToolTip(cb_ragheuristic, "The heuristic used to select neighbors in the HNSW index:" + Environment.NewLine +
                "- Simple: fast, but slightly less accurate." + Environment.NewLine +
                "- Heuristic: better choice for large datasets and varied types of data." + Environment.NewLine +
                "- Exact: Uses exact distance calculation for all entries, best accuracy but slower with very large datasets.");
            HelptoolTip.SetToolTip(ck_fixasterix, "If checked, the bot will try to fix any asterisks in its responses. This is useful if the bot is using asterisks for emphasis incorrectly." + Environment.NewLine + "Note that this may not work perfectly, and may lead to some weird formatting.");
            HelptoolTip.SetToolTip(ck_antislop, "A very basic filter to remove words or sentences from the bot's output." + Environment.NewLine + "This is done by checking the bot's responses against a list of 'sloppy' words, and removing them." + Environment.NewLine + "You can customize the list of words in the text box below.");
            HelptoolTip.SetToolTip(num_antislopchance, "The chance that the bot will delete the word or sentence.");
            HelptoolTip.SetToolTip(ed_sloplist, "A comma-separated list of words or phrases that the bot will try to avoid using in its responses.");
            HelptoolTip.SetToolTip(ck_unbold, "If checked, the bot will turn all bolded text from its responses into normal text.");
            HelptoolTip.SetToolTip(ck_noemphasisword, "If checked, the bot will turn any single word emphasis (e.g. *word*) into normal text.");
            HelptoolTip.SetToolTip(ck_noquotes, "If checked, the bot will remove all quotations marks from its responses.");
            HelptoolTip.SetToolTip(ck_fixquotes, "If checked, the bot will try to fix any unclosed or mismatched quotation marks in its responses.");
            HelptoolTip.SetToolTip(ck_reduceitalic, "If checked, the bot will try to reduce the amount of italic text in its responses." + Environment.NewLine + "This is done by removing a percentage of italic text from the output. This can be useful to prevent some models from getting into a very repetitive form of output.");
            HelptoolTip.SetToolTip(num_italicratio, "The percentage of italic text to remove from the bot's responses. A value of 1 means all italic text will be removed, a value of 0 means no italic text will be removed.");
            HelptoolTip.SetToolTip(num_removeitalicmaxword, "The maximum number of words in an italicized section for it to be removed. This is useful to prevent the bot from removing large sections of italic text that may be important." + Environment.NewLine + "For example, if set to 5, any italicized section with 5 or fewer words will be removed, while longer sections will be kept.");
            HelptoolTip.SetToolTip(ck_lastparaphfilter, "If checked, the bot will remove the last paragraph of its response if it detects that it looks like filler." + Environment.NewLine + "Some models have a bad habit of constantly writing useless slop or asking leading questions in the last paragraph of their responses. This is meant to prevent it.");
            HelptoolTip.SetToolTip(ck_remlastsentence, "If checked, the bot will remove the last sentence of its response if it's incomplete, generally due to hitting the response token limit.");
            HelptoolTip.SetToolTip(ck_oneparagraph, "If checked, the bot will stop its generation as soon as it finishes the first paragraph." + Environment.NewLine + "This is useful if you want to keep the bot's responses short and to the point, or if you want to avoid it going off on tangents.");
            HelptoolTip.SetToolTip(ckShowHidden, "If checked, any hidden messages (e.g. system messages) will be shown in the chatlog." + Environment.NewLine + "This is useful for debugging or if you want to see the full context of the conversation.");
            HelptoolTip.SetToolTip(ck_hallusafe, "If checked, the bot will try to format its memory inserts in a way that reduces the chance of hallucinations." + Environment.NewLine + "This is done by adding extra context and formatting to the memory inserts." + Environment.NewLine + "This may lead to slightly longer memory inserts, but helps improve the quality of the bot's responses.");
            HelptoolTip.SetToolTip(cb_searchapi, "The search API to use for web searches. DuckDuckGo is free and does not require an API key, but may be less reliable." + Environment.NewLine + "Brave Search requires an API key (you can get one for free by creating a Brave account), but is generally more reliable and provides better results.");
            HelptoolTip.SetToolTip(ed_searchkey, "Your Brave Search API key. Only needed if you select Brave Search as your search API." + Environment.NewLine + "You can get a free API key by creating a Brave account and generating an API key in the Brave Search settings.");
            HelptoolTip.SetToolTip(ck_searchextract, "If checked, the bot will try to extract more detailed information from the search results." + Environment.NewLine + "This leads to much better results, but is also quite slow.");
            HelptoolTip.SetToolTip(bt_importworld, "Import a worldinfo file exported from SillyTavern." + Environment.NewLine + "The worldinfo will be converted to the internal format and saved to 'exported_world.json' in this application's main folder.");
            HelptoolTip.SetToolTip(bt_ImportSTChat, "Import a chatlog file exported from SillyTavern." + Environment.NewLine + "The chatlog will be converted to the internal format and saved to 'exported_chat.json' in this application's main folder.");
            HelptoolTip.SetToolTip(ck_forcePW, "Normally, when switching to a password-protected bot, the app will remember its password for as long as it's running, so you don't need to enter it multiple times. If checked, the application will always ask for the bot's password when switching.");
            HelptoolTip.SetToolTip(ckThirdPerson, "If checked, the bot will convert user messages to 3rd person when performing RAG searches. This tends to be more effective, especially to retrieve past sessions.");
            HelptoolTip.SetToolTip(ckNoPastInserts, "If checked, the system will not insert date, insert memories, or udate mood information if the active chat session is not the latest. " + Environment.NewLine + "This can help reduce confusion when the bot is recalling past sessions that are not the current one.");
            HelptoolTip.IsBalloon = true;
            HelptoolTip.ToolTipIcon = ToolTipIcon.Info;
            HelptoolTip.ToolTipTitle = "Settings";
        }

        public SettingsForm()
        {
            InitializeComponent();
            KeyPreview = true;
            MainTab.ShowTabs = false;
            ThemeManager.ApplyToForm(this);
            LoadToolTips();
            LoadSettings();
        }

        private void LoadSettings()
        {
            if (!File.Exists("settings.json"))
            {
                Program.Settings = new LetheChatSettings();
                File.WriteAllText("settings.json", JsonConvert.SerializeObject(Program.Settings, Formatting.Indented));
                LLMEngine.Settings = Program.Settings;
            }

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
            cb_ragheuristic.SelectedIndex = (int)LLMEngine.Settings.RAGHeuristic;
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
            ckDelStartSlop.Checked = Program.Settings.RoleplayFormatting.RemoveStartingSlop;
            ckParenthesizeToItalic.Checked = Program.Settings.RoleplayFormatting.ParenthesizeToItalic;
            cb_pastsession.SelectedIndex = (int)Program.Settings.SessionHandling;
            ck_sysrag.Checked = Program.Settings.MoveAllInsertsToSysPrompt;
            ck_remlastsentence.Checked = Program.Settings.RemoveCutSentence;
            ck_oneparagraph.Checked = Program.Settings.StopGenerationOnFirstParagraph;
            ed_sloplist.Text = Program.Settings.AntiSlopList.Length > 0 ? string.Join(",", Program.Settings.AntiSlopList) : string.Empty;
            ckShowHidden.Checked = Program.Settings.ShowHiddenMessages;
            ck_hallusafe.Checked = Program.Settings.AntiHallucinationMemoryFormat;
            ckThirdPerson.Checked = Program.Settings.RAGConvertTo3rdPerson;
            mcbSkin.SelectedIndex = mcbSkin.Items.IndexOf(Program.Settings.Skin);
            mck_cutmiddle.Checked = Program.Settings.CutInTheMiddleSummaryStrategy;
            numWIEntries.Value = Program.Settings.WorldInfoMaxEntries;
            ckNoPastInserts.Checked = Program.Settings.DisableDateAndMoodIfNotLastSession;
            ckForceInternalGram.Checked = Program.Settings.ForceInternalGrammar;
            ckFactRetrieval.Checked = Program.Settings.FactRetrievalEnabled;
            numFactDedup.Value = (decimal)Program.Settings.FactDeduplicationThreshold;
            numFactRetrieval.Value = (decimal)Program.Settings.FactRetrievalThreshold;
            numFactSuper.Value = (decimal)Program.Settings.FactSupersessionThreshold;
            numFactTokens.Value = Program.Settings.CoreFactsTokenBudget;
            num_imgEmbed.Value = Program.Settings.ImageEmbeddingSize;
            num_ImgCount.Value = Program.Settings.MaxImageCount;
            ckLlamaCppSamplers.Checked = Program.Settings.BackendLLamaCppAllowAllSamplers;
            numWebSearchDetailedMaxLength.Value = (decimal)Program.Settings.WebSearchDetailedMaxLength;
            
            ckManagedLlama.Checked = Program.Settings.ManagedLlama;
            edLlamaPath.Text = Program.Settings.PathToLlamaCppServer;
            
            numLlamaPort.Value = (decimal)Program.Settings.DefaultLLamaCppSettings.Port;
            numLlamaThreads.Value = (decimal)Program.Settings.DefaultLLamaCppSettings.Threads;
            numLlamaLayers.Value = (decimal)Program.Settings.DefaultLLamaCppSettings.GpuLayers;
            numLlamaContext.Value = (decimal)Program.Settings.DefaultLLamaCppSettings.ContextSize;
            numLlamaReasonBudget.Value = (decimal)Program.Settings.DefaultLLamaCppSettings.ReasoningBudget;
            ckLlamaProps.Checked = Program.Settings.DefaultLLamaCppSettings.Props;
            ckLlamaKV.Checked = Program.Settings.DefaultLLamaCppSettings.KVcacheToGPU;
            ckLlamaMLock.Checked = Program.Settings.DefaultLLamaCppSettings.mlock;
            ckLlamaMMap.Checked = Program.Settings.DefaultLLamaCppSettings.mmap;
            ckLlamaMMProj.Checked = Program.Settings.DefaultLLamaCppSettings.LoadMMprojIfAvailable;
            ckLlamaJinja.Checked = Program.Settings.DefaultLLamaCppSettings.LoadJinjaIfAvailable;

            cbLlamaFlash.SelectedIndex = Program.Settings.DefaultLLamaCppSettings.FlashAttention switch
            {
                null => 0,
                true => 1,
                false => 2,
            };

            cbLlamaReason.SelectedIndex = Program.Settings.DefaultLLamaCppSettings.Reasoning switch
            {
                null => 0,
                true => 1,
                false => 2,
            };

            cbParallel.SelectedIndex = Program.Settings.BackendParallelToolCalls switch
            {
                null => 0,
                true => 1,
                false => 2,
            };

            cbChatAllowPrefill.SelectedIndex = Program.Settings.BackendChatAllowPrefill switch
            {
                null => 0,
                true => 1,
                false => 2,
            };

            cbChatThinkPolicy.SelectedIndex = Program.Settings.BackendStartThinkTagBehavior switch
            {
                null => 0,
                BackendChatCompletionThinkTagBehavior.Emitted => 1,
                BackendChatCompletionThinkTagBehavior.Silent => 2,
                _ => 0,
            };

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

            // enum GroupChatMode and fill group chat mode combobox
            ckGroupRouting.Items.Clear();
            foreach (var mode in Enum.GetValues<GroupChatMode>())
            {
                ckGroupRouting.Items.Add(mode.ToString());
            }
            ckGroupRouting.SelectedIndex = (int)Program.Settings.GroupChatMode;
            numGroupQueue.Value = (int)Program.Settings.GroupChatAutoResponseLimit;

            cbGroupSessionStrategy.Items.Clear();
            foreach (var mode in Enum.GetValues<GroupChatPastSessionMode>())
            {
                cbGroupSessionStrategy.Items.Add(mode.ToString());
            }
            cbGroupSessionStrategy.SelectedIndex = (int)Program.Settings.GroupSecondaryPersonaSeePastSessions;
            ckGroupAltern.Checked = Program.Settings.GroupInstructFormatAdapter;
            ckDetailedSum.Checked = Program.Settings.SessionDetailedSummary;
            ckGroupCommit.Checked = Program.Settings.CommitGroupSessionToSecondaryPersonaHistory;

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
                Program.Settings.RoleplayFormatting.RemoveStartingSlop = ckDelStartSlop.Checked;
                Program.Settings.RemoveCutSentence = ck_remlastsentence.Checked;
                Program.Settings.RoleplayFormatting.ParenthesizeToItalic = ckParenthesizeToItalic.Checked;
                Program.Settings.StopGenerationOnFirstParagraph = ck_oneparagraph.Checked;
                Program.Settings.WebsitePluginUseKeywords = ck_webkeyword.Checked;
                Program.Settings.WebsitePluginGrammar = ck_webgrammar.Checked;
                Program.Settings.SessionMemorySystem = ck_sessionmemory.Checked;
                Program.Settings.SessionDetailedSummary = ckDetailedSum.Checked;
                Program.Settings.RAGDistanceCutOff = (float)num_ragcutoff.Value;
                Program.Settings.RAGMaxEntries = (int)num_ragmaxretrieve.Value;
                Program.Settings.WorldInfoMaxEntries = (int)numWIEntries.Value;
                Program.Settings.RAGIndex = (int)num_ragindex.Value;
                Program.Settings.RAGMValue = (int)num_ragM.Value;
                Program.Settings.MoveAllInsertsToSysPrompt = ck_sysrag.Checked;
                Program.Settings.ShowHiddenMessages = ckShowHidden.Checked;
                Program.Settings.AntiHallucinationMemoryFormat = ck_hallusafe.Checked;
                Program.Settings.SessionReservedTokens = (int)num_memtokens.Value;
                Program.Settings.AlwaysForcePasswordOnBotSwitch = ck_forcePW.Checked;
                Program.Settings.RAGConvertTo3rdPerson = ckThirdPerson.Checked;
                Program.Settings.RAGHeuristic = (RAGSelectionHeuristic)cb_ragheuristic.SelectedIndex;
                Program.Settings.Skin = mcbSkin.SelectedItem?.ToString() ?? "Dark";
                Program.Settings.CutInTheMiddleSummaryStrategy = mck_cutmiddle.Checked;
                Program.Settings.DisableDateAndMoodIfNotLastSession = ckNoPastInserts.Checked;
                Program.Settings.GroupChatMode = (GroupChatMode)ckGroupRouting.SelectedIndex;
                Program.Settings.GroupChatAutoResponseLimit = (int)numGroupQueue.Value;
                Program.Settings.GroupSecondaryPersonaSeePastSessions = (GroupChatPastSessionMode)cbGroupSessionStrategy.SelectedIndex;
                Program.Settings.GroupInstructFormatAdapter = ckGroupAltern.Checked;
                Program.Settings.CommitGroupSessionToSecondaryPersonaHistory = ckGroupCommit.Checked;
                Program.Settings.ForceInternalGrammar = ckForceInternalGram.Checked;
                Program.Settings.FactRetrievalEnabled = ckFactRetrieval.Checked;
                Program.Settings.FactDeduplicationThreshold = (float)numFactDedup.Value;
                Program.Settings.FactRetrievalThreshold = (float)numFactRetrieval.Value;
                Program.Settings.FactSupersessionThreshold = (float)numFactSuper.Value;
                Program.Settings.CoreFactsTokenBudget = (int)numFactTokens.Value;
                Program.Settings.ImageEmbeddingSize = (int)num_imgEmbed.Value;
                Program.Settings.MaxImageCount = (int)num_ImgCount.Value;
                Program.Settings.BackendLLamaCppAllowAllSamplers = ckLlamaCppSamplers.Checked;

                Program.Settings.BackendParallelToolCalls = cbParallel.SelectedIndex switch
                {
                    0 => null,
                    1 => true,
                    2 => false,
                    _ => null,
                };
                Program.Settings.BackendChatAllowPrefill = cbChatAllowPrefill.SelectedIndex switch
                {
                    0 => null,
                    1 => true,
                    2 => false,
                    _ => null,
                };
                Program.Settings.BackendStartThinkTagBehavior = cbChatThinkPolicy.SelectedIndex switch
                {
                    0 => null,
                    1 => BackendChatCompletionThinkTagBehavior.Emitted,
                    2 => BackendChatCompletionThinkTagBehavior.Silent,
                    _ => null,
                };

                // Search API Settings
                if (cb_searchapi.SelectedIndex == 0)
                    Program.Settings.WebSearchAPI = BackendSearchAPI.DuckDuckGo;
                else if (cb_searchapi.SelectedIndex == 1)
                    Program.Settings.WebSearchAPI = BackendSearchAPI.Brave;
                Program.Settings.WebSearchBraveAPIKey = ed_searchkey.Text;
                Program.Settings.WebSearchDetailedResults = ck_searchextract.Checked;
                Program.Settings.WebSearchDetailedMaxLength = (int)numWebSearchDetailedMaxLength.Value;

                // Llama.cpp settings
                Program.Settings.ManagedLlama = ckManagedLlama.Checked;
                Program.Settings.PathToLlamaCppServer = edLlamaPath.Text;
                Program.Settings.DefaultLLamaCppSettings.Port = (int)numLlamaPort.Value;
                Program.Settings.DefaultLLamaCppSettings.mlock = ckLlamaMLock.Checked;
                Program.Settings.DefaultLLamaCppSettings.mmap = ckLlamaMMap.Checked;
                Program.Settings.DefaultLLamaCppSettings.ContextSize = (int)numLlamaContext.Value;
                Program.Settings.DefaultLLamaCppSettings.FlashAttention = cbLlamaFlash.SelectedIndex switch
                {
                    0 => null,
                    1 => true,
                    2 => false,
                    _ => null,
                };
                Program.Settings.DefaultLLamaCppSettings.Reasoning = cbLlamaReason.SelectedIndex switch
                {
                    0 => null,
                    1 => true,
                    2 => false,
                    _ => null,
                };
                Program.Settings.DefaultLLamaCppSettings.Threads = (int)numLlamaThreads.Value;
                Program.Settings.DefaultLLamaCppSettings.GpuLayers = (int)numLlamaLayers.Value;
                Program.Settings.DefaultLLamaCppSettings.ReasoningBudget = (int)numLlamaReasonBudget.Value;
                Program.Settings.DefaultLLamaCppSettings.Props = ckLlamaProps.Checked;
                Program.Settings.DefaultLLamaCppSettings.KVcacheToGPU = ckLlamaKV.Checked;
                Program.Settings.DefaultLLamaCppSettings.LoadMMprojIfAvailable = ckLlamaMMProj.Checked;
                Program.Settings.DefaultLLamaCppSettings.LoadJinjaIfAvailable = ckLlamaJinja.Checked;

                var str = JsonConvert.SerializeObject(Program.Settings, Formatting.Indented);
                File.WriteAllText("settings.json", str);
                // Context plugin settings
                Program.ApplyContextPluginSettings();
                // Apply RAG settings
                LLMEngine.Bot.Brain.ReloadMemories();
                LLMEngine.Client?.UpdateSearchProvider();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving settings: {ex.Message}");
            }
        }

        private void btFindLlamaExe_Click(object sender, EventArgs e)
        {
            // Open a file dialog to select the llama-server.exe file
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Executable Files|*.exe|All Files|*.*";
            ofd.Title = "Select llama-server.exe";
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Set the selected file path to the appropriate setting
                Program.Settings.PathToLlamaCppServer = ofd.FileName;
                edLlamaPath.Text = ofd.FileName;
            }
        }

        private void bt_ImportSTChat_Click(object sender, EventArgs e)
        {
            // Open a file selection dialog and use Tools.Import to import a chatlog from a jsonl file
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                MessageBox.Show(
                    ImportTools.ImportChatlog(openFileDialog1.FileName, "exported_chat.json", LLMEngine.Bot.GetIdentifier(), LLMEngine.User.UniqueName) ?
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
            if (Program.Settings.Skin == "Light")
                ThemeManager.ApplyLight();
            else
                ThemeManager.ApplyDark();
            ThemeManager.ReapplyTheme(Program.BigForm!);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ColorButton(Button? selected)
        {
            if (selected == null)
                return;
            selected.ForeColor = Color.Gold;
            // Find parent and list all buttons in the same parent and reset their color except the selected one
            if (selected.Parent == null)
                return;
            foreach (var ctrl in selected.Parent.Controls)
            {
                if (ctrl is Button btn && btn != selected)
                    btn.ForeColor = Color.WhiteSmoke;
            }

        }

        private void btBackend_Click(object sender, EventArgs e)
        {
            MainTab.SelectedTab = tabBackend;
            ColorButton(sender as Button);
        }

        private void btModelFolders_Click(object sender, EventArgs e)
        {
            SaveSettings();
            using var dlg = new ModelDirectoriesForm();
            if (dlg.ShowDialog(this) != DialogResult.OK)
                return;
            DataFiles.LocalModels.SearchModels(false);
            DataFiles.LocalModels.PruneModels();
            File.WriteAllText("modelDB.json", JsonConvert.SerializeObject(DataFiles.LocalModels, Formatting.Indented));
            LoadSettings();
        }

        private void btCore_Click(object sender, EventArgs e)
        {
            MainTab.SelectedTab = tabCore;
            ColorButton(sender as Button);
        }

        private void btMemory_Click(object sender, EventArgs e)
        {
            MainTab.SelectedTab = tabMemory;
            ColorButton(sender as Button);
        }

        private void brGroup_Click(object sender, EventArgs e)
        {
            MainTab.SelectedTab = TabGroup;
            ColorButton(sender as Button);
        }

        private void btWeb_Click(object sender, EventArgs e)
        {
            MainTab.SelectedTab = tabWeb;
            ColorButton(sender as Button);
        }

        private void btOutput_Click(object sender, EventArgs e)
        {
            MainTab.SelectedTab = tabOutput;
            ColorButton(sender as Button);
        }

        private void btTools_Click(object sender, EventArgs e)
        {
            MainTab.SelectedTab = tabTools;
            ColorButton(sender as Button);
        }

        private void btApp_Click(object sender, EventArgs e)
        {
            MainTab.SelectedTab = tabApp;
            ColorButton(sender as Button);
        }

        private void NewSettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
