using Newtonsoft.Json;
using System.Text;
using WaifuAI.Plugins;
using AIToolkit.Files;
using AIToolkit.LLM;
using Microsoft.VisualBasic;
using System.IO;

namespace WaifuAI.Files
{
    public class Character : BasePersona
    {
        [JsonIgnore] public Image Portrait => GetPortrait();
        private Image? _image = null;

        /// <summary>
        /// If set to true, this character can initiate chat by sending a message when the user is idle. 
        /// It is contextually aware and may not always send a message. 
        /// A system to prevent spam is also in place, limiting the amount of messages that can be sent before the user responds.
        /// </summary>
        public bool CanInitiateChat { get; set; } = false;

        /// <summary> Icon to be displayed in chat </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// A list of prefered inference settings for this character. When enabled in the UI, the bot will cycle between these settings at random with each new message. This ensure a more diverse set of responses.
        /// </summary>
        public List<string> AllowedSamplers { get; set; } = [];

        /// <summary>
        /// Voice ID for for OuteTTS (if enabled)
        /// </summary>
        public string TTSVoice { get; set; } = string.Empty;

        /// <summary> Reference to the point system </summary>
        public string PointSystem { get; set; } = string.Empty;

        /// <summary> Current point value </summary>
        public int PointValue { get; set; } = 0;

        [JsonIgnore] public PointSystem MyPoints = new();

        public override void BeginChat()
        {
            base.BeginChat();
            if (IsUser)
                return;
            // Location plugin
            foreach (var item in LLMSystem.ContextPlugins)
            {
                item.Enabled = Plugins.Contains(item.PluginID);
            }
            if (!string.IsNullOrEmpty(PointSystem))
            {
                if (DataFiles.Points.TryGetValue(PointSystem, out var ps))
                    MyPoints = ps.Copy<PointSystem>()!;
                MyPoints.PointCount = PointValue;
            }

            LoadChatHistory();
            MyWorlds = [.. DataFiles.WorldInfos.Values.Where(wi => Worlds.Contains(wi.UniqueName))];
            foreach (var item in MyWorlds)
                item.Reset();
        }

        public override async Task EndChat(bool backup = false)
        {
            await base.EndChat(backup);
            PointValue = MyPoints.PointCount;
            SaveChatHistory(backup);
        }

        public void LoadChatHistory() => LoadChatHistory("data/chatlogs/");

        public void SaveChatHistory(bool backup = false) => SaveChatHistory("data/chatlogs/", backup);

        public void ClearChatHistory(bool deletefile = true) => ClearChatHistory("data/chatlogs/", deletefile);

        private Image GetPortrait()
        {
            if (_image != null)
                return _image;
            var defaultfile = IsUser ? "user.png" : "Assistant.png";
            var selectedfile = File.Exists(@"data\img\" + Icon) ? Icon : defaultfile;
            _image = Image.FromFile("data/img/" + selectedfile);
            return _image;
        }
    }
}
