using LetheAISharp.Files;
using LetheAISharp.LLM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WaifuAI.Plugins;

namespace WaifuAI.Files
{
    public class GroupChar : GroupPersona<Character>, ICharacter
    {
        [JsonIgnore] public string Icon { get => CurrentBot?.Icon ?? string.Empty; set => CurrentBot!.Icon = value; }
        [JsonIgnore] public List<string> AllowedSamplers { get => CurrentBot?.AllowedSamplers ?? []; set => CurrentBot!.AllowedSamplers = value; }
        [JsonIgnore] public bool CanInitiateChat { get => CurrentBot?.CanInitiateChat ?? false; set => CurrentBot!.CanInitiateChat = value; }

        [JsonIgnore] public TimedLockPlugin LockManager => PrimaryBot?.LockManager ?? throw new InvalidOperationException("No primary bot set.");

        [JsonIgnore] public ToggleMonitorSettings LockSettings { get => PrimaryBot?.LockSettings ?? throw new InvalidOperationException("No primary bot set."); set => PrimaryBot!.LockSettings = value; }
        [JsonIgnore] public string PointSystem { get => PrimaryBot?.PointSystem ?? throw new InvalidOperationException("No primary bot set."); set => PrimaryBot!.PointSystem = value; }
        [JsonIgnore] public int PointValue { get => PrimaryBot?.PointValue ?? throw new InvalidOperationException("No primary bot set."); set => PrimaryBot!.PointValue = value; }

        [JsonIgnore] public Image Portrait => GetPortrait();

        [JsonIgnore] public bool Protected { get => PrimaryBot?.Protected ?? false; set => PrimaryBot!.Protected = value; }
        [JsonIgnore] public string TTSVoice { get => CurrentBot?.TTSVoice ?? string.Empty; set => CurrentBot!.TTSVoice = value; }

        [JsonIgnore]
        private readonly Dictionary<string, Image> portraitCache = [];

        [JsonIgnore]
        // If you MUST expose CharBrain at the GroupChar level for ICharacter:
        CharBrain ICharacter.Brain => PrimaryBot?.Brain ?? throw new InvalidOperationException("No primary bot set.");

        [JsonIgnore]
        public PointSystem MyPoints 
        {
            get => PrimaryBot?.MyPoints ?? throw new InvalidOperationException("No primary bot set.");
            set => PrimaryBot!.MyPoints = value;
        }

        public void ClearChatHistory(bool deletefile = true)
        {
            PrimaryBot?.ClearChatHistory(deletefile);
        }

        private Image GetPortrait()
        {
            var defaultfile = IsUser ? "user.png" : "Assistant.png";
            var selectedfile = File.Exists(@"data\img\" + Icon) ? Icon : defaultfile;

            if (portraitCache.TryGetValue(selectedfile, out var cachedImage))
                return cachedImage;

            var image = Image.FromFile("data/img/" + selectedfile);
            portraitCache[selectedfile] = image;
            return image;
        }

        public override void BeginChat()
        {
            base.BeginChat();
            if (IsUser)
                return;
            // Initialize plugins (refer to main bot)
            foreach (var item in LLMEngine.ContextPlugins)
            {
                item.Enabled = PrimaryBot?.Plugins.Contains(item.PluginID) ?? false;
            }
            MyWorlds = PrimaryBot?.MyWorlds ?? [.. DataFiles.WorldInfos.Values.Where(wi => Worlds.Contains(wi.UniqueName))];
            foreach (var agent in SecondaryBots)
            {
                agent.MyWorlds = [.. DataFiles.WorldInfos.Values.Where(wi => agent.Worlds.Contains(wi.UniqueName))];
            }
        }
        public override void EndChat(bool backup = false)
        {
            // Save point value for primary bot
            if (PrimaryBot != null)
            {
                PrimaryBot.PointValue = PrimaryBot.MyPoints.PointCount;
            }
            base.EndChat(backup); // Saves all personas
        }
    }
}
