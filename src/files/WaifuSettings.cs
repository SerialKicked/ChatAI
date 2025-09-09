using AIToolkit;
using AIToolkit.Files;
using AIToolkit.LLM;
using AIToolkit.SearchAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaifuAI.Files
{
    public class WaifuSettings : LLMSettings
    {
        public string BotFile { get; set; } = "Assistant";
        public string UserFile { get; set; } = "User";
        public string PromptFile { get; set; } = "Standard";
        public string Instruct { get; set; } = "ChatML";
        public string SamplerFile { get; set; } = "Default";
        public double Temperature { get; set; } = 0.70;
        public int MaxTotalTokens { get; set; } = 16384;
        public int MaxMessagesOnScreen { get; set; } = 100;
        public int FontSize { get; set; } = 18;
        public bool AlwaysWebSearchQuery { get; set; } = false;
        public bool ShowHiddenMessages { get; set; } = false;
        public string BackgroundFile { get; set; } = "bedroom_cozy.jpg";
        public bool UseTTS { get; set; } = false;
        public bool AsteriskCheck { get; set; } = false;
        public bool AntiSlop { get; set; } = false;
        public float AntiSlopRatio { get; set; } = 1;
        public string[] AntiSlopList { get; set; } = [];
        public bool WebsitePluginUseKeywords { get; set; } = false;
        public bool WebsitePluginGrammar { get; set; } = false;
        public bool RemoveCutSentence { get; set; } = false;
        public StringFix RoleplayFormatting { get; set; } = new StringFix(false, false, false, false, false, 1, 50, false);
    }
}
