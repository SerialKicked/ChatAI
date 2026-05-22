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

namespace LetheChat.Forms
{
    public partial class SettingsForm : Form
    {

        private void LoadToolTips()
        {
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ck_alwayswebsearch, nameof(LetheChatSettings.AlwaysWebSearchQuery));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, cb_pastsession, nameof(LetheChatSettings.SessionHandling));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ck_oneparagraph, nameof(LetheChatSettings.StopGenerationOnFirstParagraph));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ck_sessionmemory, nameof(LetheChatSettings.SessionMemorySystem));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckDetailedSum, nameof(LetheChatSettings.SessionDetailedSummary));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, num_ragcutoff, nameof(LetheChatSettings.RAGDistanceCutOff));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, num_ragmaxretrieve, nameof(LetheChatSettings.RAGMaxEntries));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, numWIEntries, nameof(LetheChatSettings.RAGKeywordMaxEntries));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, num_ragindex, nameof(LetheChatSettings.RAGIndex));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, num_ragM, nameof(LetheChatSettings.RAGMValue));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ck_sysrag, nameof(LetheChatSettings.MoveAllInsertsToSysPrompt));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckShowHidden, nameof(LetheChatSettings.ShowHiddenMessages));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ck_hallusafe, nameof(LetheChatSettings.AntiHallucinationMemoryFormat));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, num_memtokens, nameof(LetheChatSettings.SessionReservedTokens));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ck_forcePW, nameof(LetheChatSettings.AlwaysForcePasswordOnBotSwitch));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckThirdPerson, nameof(LetheChatSettings.RAGConvertTo3rdPerson));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, cb_ragheuristic, nameof(LetheChatSettings.RAGHeuristic));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, mck_cutmiddle, nameof(LetheChatSettings.CutInTheMiddleSummaryStrategy));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckNoPastInserts, nameof(LetheChatSettings.DisableDateAndMoodIfNotLastSession));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckGroupRouting, nameof(LetheChatSettings.GroupChatMode));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, numGroupQueue, nameof(LetheChatSettings.GroupChatAutoResponseLimit));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, cbGroupSessionStrategy, nameof(LetheChatSettings.GroupSecondaryPersonaSeePastSessions));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckGroupAltern, nameof(LetheChatSettings.GroupInstructFormatAdapter));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckGroupThinkPrompt, nameof(LetheChatSettings.GroupChatInfoThinkingBlock));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckGroupCommit, nameof(LetheChatSettings.CommitGroupSessionToSecondaryPersonaHistory));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckForceInternalGram, nameof(LetheChatSettings.ForceInternalGrammar));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckFactRetrieval, nameof(LetheChatSettings.FactRetrievalEnabled));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, numFactDedup, nameof(LetheChatSettings.FactDeduplicationThreshold));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, numFactRetrieval, nameof(LetheChatSettings.FactRetrievalThreshold));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, numFactSuper, nameof(LetheChatSettings.FactSupersessionThreshold));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, numFactTokens, nameof(LetheChatSettings.CoreFactsTokenBudget));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckFactRoleplay, nameof(LetheChatSettings.RecordFactsDuringRoleplay));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, num_imgEmbed, nameof(LetheChatSettings.ImageEmbeddingSize));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, num_ImgCount, nameof(LetheChatSettings.MaxImageCount));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckToolAlwaysAsk, nameof(LetheChatSettings.ToolCallsAlwaysManualConfirm));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, numAFKDelay, nameof(LetheChatSettings.BackgroundAgentMinInactivityTime));

            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, cbParallel, nameof(LetheChatSettings.BackendParallelToolCalls));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, cbChatAllowPrefill, nameof(LetheChatSettings.BackendChatAllowPrefill));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ck_backendbostoken, nameof(LetheChatSettings.BackendHandlesBoSToken));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, cb_searchapi, nameof(LetheChatSettings.WebSearchAPI));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ed_searchkey, nameof(LetheChatSettings.WebSearchBraveAPIKey));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ck_searchextract, nameof(LetheChatSettings.WebSearchDetailedResults));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, numWebSearchDetailedMaxLength, nameof(LetheChatSettings.WebSearchDetailedMaxLength));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckManagedLlama, nameof(LetheChatSettings.ManagedLlama));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, edLlamaPath, nameof(LetheChatSettings.PathToLlamaCppServer));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckUseIkLlama, nameof(LetheChatSettings.IsIkLlama));

            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, numAFKDelay, nameof(LetheChatSettings.BackgroundAgentMinInactivityTime));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, numAFKDelay, nameof(LetheChatSettings.BackgroundAgentMinInactivityTime));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, numAFKDelay, nameof(LetheChatSettings.BackgroundAgentMinInactivityTime));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, numAFKDelay, nameof(LetheChatSettings.BackgroundAgentMinInactivityTime));

            HelpTool.ApplyTooltip<LlamaCppSettings>(HelptoolTip, numLlamaPort, nameof(LlamaCppSettings.Port));
            HelpTool.ApplyTooltip<LlamaCppSettings>(HelptoolTip, ckLlamaMLock, nameof(LlamaCppSettings.mlock));
            HelpTool.ApplyTooltip<LlamaCppSettings>(HelptoolTip, ckLlamaMMap, nameof(LlamaCppSettings.mmap));
            HelpTool.ApplyTooltip<LlamaCppSettings>(HelptoolTip, numLlamaContext, nameof(LlamaCppSettings.ContextSize));
            HelpTool.ApplyTooltip<LlamaCppSettings>(HelptoolTip, cbLlamaFlash, nameof(LlamaCppSettings.FlashAttention));
            HelpTool.ApplyTooltip<LlamaCppSettings>(HelptoolTip, cbLlamaReason, nameof(LlamaCppSettings.Reasoning));
            HelpTool.ApplyTooltip<LlamaCppSettings>(HelptoolTip, numLlamaThreads, nameof(LlamaCppSettings.Threads));
            HelpTool.ApplyTooltip<LlamaCppSettings>(HelptoolTip, numLlamaLayers, nameof(LlamaCppSettings.GpuLayers));
            HelpTool.ApplyTooltip<LlamaCppSettings>(HelptoolTip, numLlamaReasonBudget, nameof(LlamaCppSettings.ReasoningBudget));
            HelpTool.ApplyTooltip<LlamaCppSettings>(HelptoolTip, ckLlamaProps, nameof(LlamaCppSettings.Props));
            HelpTool.ApplyTooltip<LlamaCppSettings>(HelptoolTip, ckLlamaKV, nameof(LlamaCppSettings.KVcacheToGPU));
            HelpTool.ApplyTooltip<LlamaCppSettings>(HelptoolTip, ckLlamaMMProj, nameof(LlamaCppSettings.LoadMMprojIfAvailable));
            HelpTool.ApplyTooltip<LlamaCppSettings>(HelptoolTip, ckLlamaJinja, nameof(LlamaCppSettings.LoadJinjaIfAvailable));
            HelpTool.ApplyTooltip<LlamaCppSettings>(HelptoolTip, cbLlamaKV, nameof(LlamaCppSettings.KVCacheQuantization));

            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, cbDefaultCompletion, nameof(LetheChatSettings.DefaultCompletionType));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, cklToolsets, nameof(LetheChatSettings.AllowedToolsets));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, ckAllowtools, nameof(LetheChatSettings.ToolCallsAllowed));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, numToolLimit, nameof(LetheChatSettings.ToolCallLimit));
            HelpTool.ApplyTooltip<LetheChatSettings>(HelptoolTip, numToolMemory, nameof(LetheChatSettings.ToolCallChainLimit));


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

            // Load tools
            cklToolsets.Items.Clear();
            var avail = LLMEngine.ToolManager.GetRegisteredToolListIds();
            foreach (var toolset in avail)
            {
                cklToolsets.Items.Add(toolset, Program.Settings.AllowedToolsets.Contains(toolset));
            }

            // Init Completion
            cbDefaultCompletion.Items.Clear();
            cbDefaultCompletion.Items.AddRange(["Default", "Text", "Chat"]);

            cbDefaultCompletion.SelectedIndex = Program.Settings.DefaultCompletionType switch
            {
                null => 0,
                CompletionType.Text => 1,
                CompletionType.Chat => 2,
                _ => 0,
            };

            ckAllowtools.Checked = Program.Settings.ToolCallsAllowed;
            numToolLimit.Value = Program.Settings.ToolCallLimit;
            numToolMemory.Value = Program.Settings.ToolCallChainLimit;

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
            numWIEntries.Value = Program.Settings.RAGKeywordMaxEntries;
            ckNoPastInserts.Checked = Program.Settings.DisableDateAndMoodIfNotLastSession;
            ckForceInternalGram.Checked = Program.Settings.ForceInternalGrammar;
            ckFactRetrieval.Checked = Program.Settings.FactRetrievalEnabled;
            numFactDedup.Value = (decimal)Program.Settings.FactDeduplicationThreshold;
            numFactRetrieval.Value = (decimal)Program.Settings.FactRetrievalThreshold;
            numFactSuper.Value = (decimal)Program.Settings.FactSupersessionThreshold;
            numFactTokens.Value = Program.Settings.CoreFactsTokenBudget;
            num_imgEmbed.Value = Program.Settings.ImageEmbeddingSize;
            num_ImgCount.Value = Program.Settings.MaxImageCount;
            ckFactRoleplay.Checked = Program.Settings.RecordFactsDuringRoleplay;
            numWebSearchDetailedMaxLength.Value = (decimal)Program.Settings.WebSearchDetailedMaxLength;

            ckManagedLlama.Checked = Program.Settings.ManagedLlama;
            edLlamaPath.Text = Program.Settings.PathToLlamaCppServer;
            ckUseIkLlama.Checked = Program.Settings.IsIkLlama;

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
            ckToolAlwaysAsk.Checked = Program.Settings.ToolCallsAlwaysManualConfirm;
            ck_backendbostoken.Checked = Program.Settings.BackendHandlesBoSToken;

            cbLlamaFlash.SelectedIndex = Program.Settings.DefaultLLamaCppSettings.FlashAttention switch
            {
                null => 0,
                true => 1,
                false => 2,
            };

            cbLlamaKV.SelectedIndex = (int)Program.Settings.DefaultLLamaCppSettings.KVCacheQuantization;

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
            ckGroupThinkPrompt.Checked = Program.Settings.GroupChatInfoThinkingBlock;
            ckDetailedSum.Checked = Program.Settings.SessionDetailedSummary;
            ckGroupCommit.Checked = Program.Settings.CommitGroupSessionToSecondaryPersonaHistory;
            numAFKDelay.Value = (decimal)Program.Settings.BackgroundAgentMinInactivityTime.TotalHours;

            // Audio

            cb_audiolanguage.Items.Clear();
            foreach (var lang in SpeechRecognizerSettings.AvailableLanguages)
            {
                cb_audiolanguage.Items.Add(lang.Value);
            }
            var selectedLang = SpeechRecognizerSettings.AvailableLanguages.ContainsKey(Program.Settings.AudioSettings.Language) ? SpeechRecognizerSettings.AvailableLanguages[Program.Settings.AudioSettings.Language] : "Auto-detect";
            if (selectedLang != null)
            {
                cb_audiolanguage.SelectedIndex = cb_audiolanguage.Items.IndexOf(selectedLang);
            }

            ck_audiodynamic.Checked = Program.Settings.AudioSettings.DynamicLoadModel;
            ck_audioenabled.Checked = Program.Settings.AudioSettings.AllowAudioRecording;
            num_audioSilenceThreshold.Value = (decimal)Program.Settings.AudioSettings.SilenceThreshold;
            num_audiotimeout.Value = (decimal)Program.Settings.AudioSettings.SilenceTimeoutSeconds;

            cb_audiomodel.Items.Clear();
            // if no whisper subfolder, create it
            if (!Directory.Exists("whisper"))
            {
                Directory.CreateDirectory("whisper");
            }
            foreach (var file in Directory.GetFiles("whisper"))
            {
                cb_audiomodel.Items.Add(Path.GetFileName(file));
            }
            cb_audiomodel.SelectedIndex = cb_audiomodel.Items.IndexOf(Program.Settings.AudioSettings.WhisperFile);
            if (cb_audiomodel.SelectedIndex == -1 && cb_audiomodel.Items.Count > 0)
            {
                cb_audiomodel.SelectedIndex = 0;
            }

            // emojis
            ckEmojiRemoval.Checked = Program.Settings.EmojiRemoval;
            numEmojiBaseRemoval.Value = (decimal)Program.Settings.EmojiBaseRemoval;
            numEmojiRemovalEscalation.Value = (decimal)Program.Settings.EmojiRemovalEscalation;
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
                Program.Settings.SessionMemorySystem = ck_sessionmemory.Checked;
                Program.Settings.SessionDetailedSummary = ckDetailedSum.Checked;
                Program.Settings.RAGDistanceCutOff = (float)num_ragcutoff.Value;
                Program.Settings.RAGMaxEntries = (int)num_ragmaxretrieve.Value;
                Program.Settings.RAGKeywordMaxEntries = (int)numWIEntries.Value;
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
                Program.Settings.GroupChatInfoThinkingBlock = ckGroupThinkPrompt.Checked;
                Program.Settings.CommitGroupSessionToSecondaryPersonaHistory = ckGroupCommit.Checked;
                Program.Settings.ForceInternalGrammar = ckForceInternalGram.Checked;
                Program.Settings.FactRetrievalEnabled = ckFactRetrieval.Checked;
                Program.Settings.FactDeduplicationThreshold = (float)numFactDedup.Value;
                Program.Settings.FactRetrievalThreshold = (float)numFactRetrieval.Value;
                Program.Settings.FactSupersessionThreshold = (float)numFactSuper.Value;
                Program.Settings.CoreFactsTokenBudget = (int)numFactTokens.Value;
                Program.Settings.RecordFactsDuringRoleplay = ckFactRoleplay.Checked;
                Program.Settings.ImageEmbeddingSize = (int)num_imgEmbed.Value;
                Program.Settings.MaxImageCount = (int)num_ImgCount.Value;
                Program.Settings.ToolCallsAlwaysManualConfirm = ckToolAlwaysAsk.Checked;
                Program.Settings.BackgroundAgentMinInactivityTime = TimeSpan.FromHours((double)numAFKDelay.Value);

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
                Program.Settings.BackendHandlesBoSToken = ck_backendbostoken.Checked;

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
                Program.Settings.IsIkLlama = ckUseIkLlama.Checked;
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

                Program.Settings.DefaultCompletionType = cbDefaultCompletion.SelectedIndex switch
                {
                    0 => null,
                    1 => CompletionType.Text,
                    2 => CompletionType.Chat,
                    _ => null,
                };

                Program.Settings.DefaultLLamaCppSettings.KVCacheQuantization = (KVCacheQuantization)cbLlamaKV.SelectedIndex;

                Program.Settings.AllowedToolsets = [.. cklToolsets.CheckedItems.Cast<string>()];
                Program.Settings.ToolCallsAllowed = ckAllowtools.Checked;
                Program.Settings.ToolCallLimit = (int)numToolLimit.Value;
                Program.Settings.ToolCallChainLimit = (int)numToolMemory.Value;

                var str = JsonConvert.SerializeObject(Program.Settings, Formatting.Indented);
                File.WriteAllText("settings.json", str);
                // Context plugin settings
                Program.ApplyContextPluginSettings();
                // Apply RAG settings
                LLMEngine.Bot.Brain.ReloadMemories();
                LLMEngine.Client?.UpdateSearchProvider();

                // audio settings
                Program.Settings.AudioSettings.Language = SpeechRecognizerSettings.AvailableLanguages.FirstOrDefault(x => x.Value == cb_audiolanguage.SelectedItem?.ToString()).Key ?? "auto";
                Program.Settings.AudioSettings.DynamicLoadModel = ck_audiodynamic.Checked;
                Program.Settings.AudioSettings.AllowAudioRecording = ck_audioenabled.Checked;
                Program.Settings.AudioSettings.SilenceThreshold = (float)num_audioSilenceThreshold.Value;
                Program.Settings.AudioSettings.SilenceTimeoutSeconds = (float)num_audiotimeout.Value;
                Program.Settings.AudioSettings.WhisperFile = cb_audiomodel.SelectedItem?.ToString() ?? "default";

                // emojis
                Program.Settings.EmojiRemoval = ckEmojiRemoval.Checked;
                Program.Settings.EmojiBaseRemoval = (float)numEmojiBaseRemoval.Value;
                Program.Settings.EmojiRemovalEscalation = (float)numEmojiRemovalEscalation.Value;
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

        private async void bt_importworld_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            var res = await ImportTools.ImportWorld(openFileDialog1.FileName, "exported_world.json");
            if (!res)
            {
                MessageBox.Show("Something went wrong while opening or parsing the file.");
                return;
            }
            MessageBox.Show("Lorebook imported successfully to LetheChat's data/worlds folder.");
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


        private void cklToolsets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cklToolsets.SelectedItem == null)
                return;
            edToolinfo.Clear();
            var toolsetId = cklToolsets.SelectedItem.ToString();
            if (toolsetId == null)
                return;
            var toolList = LLMEngine.ToolManager.GetToolsForIds(toolsetId);
            edToolinfo.Text = $"Tools in {toolsetId}:" + Environment.NewLine + Environment.NewLine;
            foreach (var tool in toolList)
            {
                edToolinfo.Text += $"- {tool.Function?.Name ?? "Unknown"}: {tool.Function?.Description ?? "No description"}" + Environment.NewLine + Environment.NewLine;
            }
        }

        private void btAudio_Click(object sender, EventArgs e)
        {
            MainTab.SelectedTab = tabAudio;
            ColorButton(sender as Button);
        }
    }
}
