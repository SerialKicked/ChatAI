using Newtonsoft.Json;
using System.Text;
using WaifuAI.Memory;
using WaifuAI.Plugins;

namespace WaifuAI.Files
{
    public enum AdvCharType { Normal, Domina };

    public class Character : BaseFile
    {
        /// <summary> Character's name (used by LLM) </summary>
        public string Name { get; set; } = string.Empty;
        public bool IsUser { get; set; } = false;
        /// <summary> Character's bio (used by LLM) </summary>
        public string Bio { get; set; } = string.Empty;
        /// <summary> Character's default scenario (used by LLM) </summary>
        public string Scenario { get; set; } = string.Empty;
        /// <summary> Character Notes (UI) </summary>
        public string Notes { get; set; } = string.Empty;
        /// <summary> Icon to be displayed in chat </summary>
        public string Icon { get; set; } = string.Empty;
        /// <summary> First message the character will send when starting a new session </summary>
        public List<string> FirstMessage { get; set; } = [];
        /// <summary> Examples of dialogs from the character to get a more consistent tone </summary>
        public List<string> ExampleDialogs { get; set; } = [];
        /// <summary> Custom system prompt for this character </summary>
        public string SystemPrompt { get; set; } = string.Empty;
        /// <summary> WorldInfo applied to this character </summary>
        public List<string> Worlds { get; set; } = [];
        /// <summary> Optional world info being used for the Location plugin </summary>
        public List<string> Plugins { get; set; } = [];
        /// <summary> If set to true, older chat sessions will be summarized, allowing for a advanced form of memory </summary>
        public bool SessionMemorySystem { get; set; } = false;
        /// <summary> If set to true, this bot will stay informed about the spacing between user messages </summary>
        public bool SenseOfTime { get; set; } = false;
        /// <summary> If set to true, this bot will use RAG to recall relevant memories </summary>
        public bool UseRAG { get; set; } = false;

        public AdvCharType CharType { get; set; } = AdvCharType.Normal;

        [JsonIgnore] public List<WorldInfo> MyWorlds { get; private set; } = [];
        [JsonIgnore] public Chatlog History { get; private set; } = new();
        [JsonIgnore] public Image  Portrait => GetPortrait();
        private Image? _image = null;

        public string GetBio(string othername) => IsUser ? 
            Bio.Replace("{{user}}", Name).Replace("{{char}}", othername) :
            Bio.Replace("{{char}}", Name).Replace("{{user}}", othername);

        public string GetScenario(string othername) => IsUser ?
            Scenario.Replace("{{user}}", Name).Replace("{{char}}", othername) :
            Scenario.Replace("{{char}}", Name).Replace("{{user}}", othername);

        public string GetDialogExamples(string othername)
        {
            if (ExampleDialogs.Count == 0)
                return string.Empty;
            var str = new StringBuilder();
            str.AppendLinuxLine($"Here are some examples of {Name}'s writing style:");
            foreach (var item in ExampleDialogs)
                str.AppendLinuxLine("- " + item.Replace("{{user}}", othername).Replace("{{char}}", Name));
            return str.ToString();
        }

        public string GetWelcomeLine(string othername)
        {
            if (FirstMessage.Count == 0)
                return string.Empty;
            // select a random welcome line
            var index = LLMSystem.RNG.Next(FirstMessage.Count);
            return FirstMessage[index].Replace("{{user}}", othername).Replace("{{char}}", Name);
        }

        public Character() { }

        public void BeginSession()
        {
            if (IsUser)
                return;
            // Location plugin
            foreach (var item in LLMSystem.ContextPlugins)
            {
                item.Enabled = Plugins.Contains(item.PluginID);
            }
            LoadChatHistory();
            // load world info
            MyWorlds = DataFiles.WorldInfos.Values.Where(wi => Worlds.Contains(wi.UniqueName)).ToList();
            foreach (var item in MyWorlds)
                item.Reset();
            if (CharType == AdvCharType.Normal || UniqueName == null)
                return;
            var f = "data/sessions/" + UniqueName + ".json";
        }

        public void EndSession(bool backup = false)
        {
            SaveChatHistory(backup);
        }

        public void SaveChatHistory(bool backup = false)
        {
            if (string.IsNullOrEmpty(UniqueName))
                return;
            if (backup && File.Exists("data/chatlogs/" + UniqueName + ".json"))
            {
                if (File.Exists("data/chatlogs/" + UniqueName + ".bak"))
                    File.Delete("data/chatlogs/" + UniqueName + ".bak");
                File.Move("data/chatlogs/" + UniqueName + ".json", "data/chatlogs/" + UniqueName + ".bak");
            }

            History.SaveToFile("data/chatlogs/" + UniqueName + ".json");
        }

        private void LoadChatHistory()
        {
            if (string.IsNullOrEmpty(UniqueName))
            {
                History = new Chatlog();
                return;
            }
            var f = "data/chatlogs/" + UniqueName + ".json";
            History = File.Exists(f) ? JsonConvert.DeserializeObject<Chatlog>(File.ReadAllText(f))! : new Chatlog();
        }

        public void ClearChatHistory(bool deletefile = true)
        {
            History.ClearHistory();
            if (!deletefile)
                return;
            var f = "data/chatlogs/" + UniqueName;
            if (File.Exists(f + ".json")) File.Delete(f + ".json");
            if (File.Exists(f + ".vec")) File.Delete(f + ".vec");
        }

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
