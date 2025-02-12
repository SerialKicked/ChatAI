using Newtonsoft.Json;
using System.Text;
using WaifuAI.Plugins;
using AIToolkit.Files;
using AIToolkit.LLM;

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

        /// <summary>
        /// A list of prefered inference settings for this character. When enabled in the UI, the bot will cycle between these settings at random with each new message. This ensure a more diverse set of responses.
        /// </summary>
        public List<string> AllowedSamplers { get; set; } = [];

        /// <summary>
        /// Voice ID for for OuteTTS (if enabled)
        /// </summary>
        public string TTSVoice { get; set; } = string.Empty;

        public override void BeginChat()
        {
            if (IsUser)
                return;
            // Location plugin
            foreach (var item in LLMSystem.ContextPlugins)
            {
                item.Enabled = Plugins.Contains(item.PluginID);
            }
            LoadChatHistory();
            MyWorlds = DataFiles.WorldInfos.Values.Where(wi => Worlds.Contains(wi.UniqueName)).ToList();
            foreach (var item in MyWorlds)
                item.Reset();
        }

        public override void EndChat(bool backup = false)
        {
            SaveChatHistory(backup);
        }

        protected void LoadChatHistory() => LoadChatHistory("data/chatlogs/");

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
