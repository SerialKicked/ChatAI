using Newtonsoft.Json;
using System.Text;
using WaifuAI.Plugins;
using AIToolkit.Files;
using AIToolkit.LLM;

namespace WaifuAI.Files
{
    public class Character : BasePersona
    {
        [JsonIgnore] public Image  Portrait => GetPortrait();
        private Image? _image = null;

        public bool CanInitiateChat { get; set; } = false;
        public List<string> AllowedSamplers = [];

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
