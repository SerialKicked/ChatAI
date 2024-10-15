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
    }
}
