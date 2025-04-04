using Newtonsoft.Json;
using WaifuAI.Files;

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

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            DataFiles.LoadDB();
            BigForm = new MainForm();
            Application.Run(BigForm);
        }
    }
}