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
    public enum GroupChatMode { Manual, RoundRobin, NameDetection, Mixed }

    public class GroupChar : GroupPersona<Character>, ICharacter
    {
        private Queue<Character> _responseQueue = new();
        private int _autoResponsesThisRound = 0;

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


        /// <summary>
        /// Gets next bot from queue or null to return control to user
        /// </summary>
        public Character? GetNextFromQueue()
        {
            if (_responseQueue.Count > 0 && _autoResponsesThisRound < Program.Settings.GroupChatAutoResponseLimit)
            {
                _autoResponsesThisRound++;
                return _responseQueue.Dequeue();
            }
            return null; // Queue exhausted or safety limit hit
        }

        /// <summary>
        /// Resets the auto-response counter (call when user sends message)
        /// </summary>
        public void ResetAutoResponseCounter()
        {
            _autoResponsesThisRound = 0;
        }

        private void BuildQueue_NameDetection(string userMessage)
        {
            var allBots = AllPersonas.Cast<Character>().ToList();
            var mentioned = new List<(Character Bot, int Position)>();

            foreach (var bot in allBots)
            {
                var index = userMessage.IndexOf(bot.Name, StringComparison.OrdinalIgnoreCase);
                if (index >= 0)
                {
                    mentioned.Add((bot, index));
                }
            }

            // Enqueue in order of mention
            foreach (var (bot, _) in mentioned.OrderBy(x => x.Position))
            {
                _responseQueue.Enqueue(bot);
            }
        }

        private void BuildQueue_RoundRobin()
        {
            var allBots = AllPersonas.Cast<Character>().ToList();

            // Queue all bots in order
            foreach (var bot in allBots)
            {
                _responseQueue.Enqueue(bot);
            }

            // Optionally add user "turn" at the end by returning null
            // (handled by GetNextFromQueue returning null when empty)
        }

        public void BuildResponseQueue(string userMessage)
        {
            _responseQueue.Clear();

            switch (Program.Settings.GroupChatMode)
            {
                case GroupChatMode.NameDetection:
                    BuildQueue_NameDetection(userMessage);
                    break;

                case GroupChatMode.RoundRobin:
                    BuildQueue_RoundRobin();
                    break;

                case GroupChatMode.Manual:
                default:
                    // Queue stays empty, manual selection only
                    break;
            }
        }

    }
}
