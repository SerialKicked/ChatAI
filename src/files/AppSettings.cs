using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaifuAI.Files
{
    public class WaifuSettings : BaseFile
    {
        public string BotFile { get; set; } = "Assistant";
        public string UserFile { get; set; } = "User";
        public string PromptFile { get; set; } = "Standard";
        public string Instruct { get; set; } = "ChatML";
        public string SamplerFile { get; set; } = "Default";
        public double Temperature { get; set; } = 0.55;
        public int MaxResponseTokens { get; set; } = 150;
        public int MaxTotalTokens { get; set; } = 8192;
        public bool RAGUseTitles { get; set; } = true;
        public bool RAGUseSummaries { get; set; } = true;
        public float RAGDistanceCutOff { get; set; } = 0.2f;
        public HNSW.Net.NeighbourSelectionHeuristic RAGHeurisitc { get; set; } = HNSW.Net.NeighbourSelectionHeuristic.SelectSimple;
        public int MaxRAGEntries { get; set; } = 4;
        public int ReservedSessionTokens { get; set; } = 2048;
    }
}
