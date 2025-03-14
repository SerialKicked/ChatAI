using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIToolkit;
using AIToolkit.Files;

namespace WaifuAI.Files
{
    public class WaifuSettings : BaseFile
    {
        public string BotFile { get; set; } = "Assistant";
        public string UserFile { get; set; } = "User";
        public string PromptFile { get; set; } = "Standard";
        public string Instruct { get; set; } = "ChatML";
        public string SamplerFile { get; set; } = "Default";
        public string ScenarioOverride { get; set; } = string.Empty;
        public double Temperature { get; set; } = 0.70;
        public int MaxResponseTokens { get; set; } = 512;
        public int MaxTotalTokens { get; set; } = 16384;
        public bool RAGUseTitles { get; set; } = true;
        public bool RAGUseSummaries { get; set; } = true;
        public float RAGDistanceCutOff { get; set; } = 0.165f;
        public HNSW.Net.NeighbourSelectionHeuristic RAGHeurisitc { get; set; } = HNSW.Net.NeighbourSelectionHeuristic.SelectSimple;
        public int MaxRAGEntries { get; set; } = 3;
        public int ReservedSessionTokens { get; set; } = 2048;
        public int RAGPosition { get; set; } = 3;
        public int MaxMessagesOnScreen { get; set; } = 100;
        public int FontSize { get; set; } = 18;
        public bool AlwaysWebSearchQuery { get; set; } = false;
        public string BackgroundFile { get; set; } = "bedroom_cozy.jpg";
        public bool MarkdownMemoryFormating { get; set; } = false;
        public bool UseTTS { get; set; } = false;
        public bool AsteriskCheck { get; set; } = false;
        public bool AntiSlop { get; set; } = false;
        public float AntiSlopRatio { get; set; } = 1;
        public string[] AntiSlopList { get; set; } = [];
        public bool WebsitePluginUseKeywords = false;
        public bool WebsitePluginGrammar = false;
        public StringFix RoleplayFormatting { get; set; } = new StringFix(false,false,false,false);
    }
}
