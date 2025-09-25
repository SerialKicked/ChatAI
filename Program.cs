using LetheAISharp.LLM;
using Newtonsoft.Json;
using System.IO;
using WaifuAI.Files;
using WaifuAI.Plugins;

namespace WaifuAI
{
    internal static class Program
    {
        public static MainForm? BigForm { get; private set; }
        public static WaifuSettings Settings { get; set; } = new WaifuSettings();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (!File.Exists("settings.json"))
            {
                Settings = new WaifuSettings();
                File.WriteAllText("settings.json", JsonConvert.SerializeObject(Settings, Formatting.Indented));
            }
            var str = File.ReadAllText("settings.json");
            Settings = JsonConvert.DeserializeObject<WaifuSettings>(str)!;
            LLMEngine.Settings = Settings;

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            DataFiles.LoadDB();
            BigForm = new MainForm();
            Application.Run(BigForm);
        }

        public static void ApplyContextPluginSettings()
        {
            if (LLMEngine.ContextPlugins.Find(x => x.PluginID == "WebSearch") is WebSearchPlugin searchplug)
            {
                searchplug.KeywordDetection = !Settings.AlwaysWebSearchQuery;
            }

            if (LLMEngine.ContextPlugins.Find(e => e is BrowsePlugin) is BrowsePlugin webplug)
            {
                webplug.EnforceCorrectGrammar = Settings.WebsitePluginGrammar;
                webplug.KeywordDetection = Settings.WebsitePluginUseKeywords;
            }
        }
    }
}