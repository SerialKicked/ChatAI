using LetheAISharp.Files;
using LetheAISharp.LLM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LetheChat.AgentPlugins;
using LetheChat.Plugins;

namespace LetheChat.Files
{
    public enum GroupChatMode { Manual, RoundRobin, NameDetection, LLMSelection }

    public class GroupChar : GroupPersona<Character>, ICharacter
    {
        private Queue<Character> _responseQueue = new();
        private int _autoResponsesThisRound = 0;
        private readonly FindGroupNextAgent findGroupNextAgent = new();

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

        [JsonIgnore] private readonly Dictionary<string, Image> portraitCache = [];

        [JsonIgnore] CharBrain ICharacter.Brain => PrimaryBot?.Brain ?? throw new InvalidOperationException("No primary bot set.");

        [JsonIgnore]
        public PointSystem MyPoints
        {
            get => PrimaryBot?.MyPoints ?? throw new InvalidOperationException("No primary bot set.");
            set => PrimaryBot!.MyPoints = value;
        }

        // === New helper properties for MainForm ===
        [JsonIgnore] public bool HasQueuedResponses => _responseQueue.Count > 0;
        [JsonIgnore] public int QueuedCount => _responseQueue.Count;
        [JsonIgnore] public IEnumerable<string> QueuedNames => _responseQueue.Select(c => c.UniqueName);

        public void ClearChatHistory(bool deletefile = true) => PrimaryBot?.ClearChatHistory(deletefile);

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

            foreach (var item in LLMEngine.ContextPlugins)
                item.Enabled = PrimaryBot?.Plugins.Contains(item.PluginID) ?? false;

            MyWorlds = PrimaryBot?.MyWorlds ?? [.. DataFiles.WorldInfos.Values.Where(wi => Worlds.Contains(wi.UniqueName))];

            foreach (var agent in SecondaryBots)
                agent.MyWorlds = [.. DataFiles.WorldInfos.Values.Where(wi => agent.Worlds.Contains(wi.UniqueName))];
        }

        public override void EndChat(bool backup = false)
        {
            PrimaryBot?.PointValue = PrimaryBot.MyPoints.PointCount;
            base.EndChat(backup);
        }


        public async Task<Character?> GetNextFromQueue()
        {
            // Remove entries that are no longer part of the group
            while (_responseQueue.Count > 0 && !AllPersonas.Contains(_responseQueue.Peek()))
                _responseQueue.Dequeue();

            if (Program.Settings.GroupChatMode == GroupChatMode.LLMSelection && _responseQueue.Count == 0)
            {
                _autoResponsesThisRound++;
                if (_autoResponsesThisRound > Program.Settings.GroupChatAutoResponseLimit)
                    return null;
                var next = await LLMPickNext();
                return (next is not null && next != LLMEngine.User) ? next : null;
            }

            if (_responseQueue.Count > 0 && (_autoResponsesThisRound < Program.Settings.GroupChatAutoResponseLimit || Program.Settings.GroupChatMode == GroupChatMode.RoundRobin))
            {
                _autoResponsesThisRound++;
                return _responseQueue.Dequeue();
            }
            return null;
        }

        public void ResetAutoResponseCounter() => _autoResponsesThisRound = 0;

        private void BuildQueue_NameDetection(string userMessage)
        {
            var allBots = AllPersonas.Cast<Character>().ToList();
            var mentioned = new List<(Character Bot, int Position)>();

            foreach (var bot in allBots)
            {
                var index = userMessage.IndexOf(bot.Name, StringComparison.OrdinalIgnoreCase);
                if (index >= 0)
                    mentioned.Add((bot, index));
            }

            foreach (var (bot, _) in mentioned.OrderBy(x => x.Position))
                _responseQueue.Enqueue(bot);
        }

        private void BuildQueue_RoundRobin()
        {
            foreach (var bot in AllPersonas.Cast<Character>())
                _responseQueue.Enqueue(bot);
        }

        public async Task<Character?> LLMPickNext(string? userMessage = null)
        {
            var gparams = new FindGroupNextAgentParams
            {
                Group = this,
                Messages = [.. History.CurrentSession.Messages.TakeLast(6)],
                AllowFullAnalysis = true
            };
            if (userMessage is not null)
                gparams.Messages.Add(new SingleMessage(AuthorRole.User, userMessage));
            return await findGroupNextAgent.Execute(gparams, CancellationToken.None);
        }

        public async Task BuildResponseQueue(string userMessage)
        {
            _responseQueue.Clear();
            ResetAutoResponseCounter();

            switch (Program.Settings.GroupChatMode)
            {
                case GroupChatMode.NameDetection:
                    BuildQueue_NameDetection(userMessage);
                    break;
                case GroupChatMode.RoundRobin:
                    BuildQueue_RoundRobin();
                    break;
                case GroupChatMode.LLMSelection:
                    var selectedBot = await LLMPickNext(userMessage);
                    if (selectedBot != null && selectedBot != MainForm.Bot)
                        _responseQueue.Enqueue(selectedBot);
                    else
                        BuildQueue_NameDetection(userMessage); // fallback
                    break;
                case GroupChatMode.Manual:
                default:
                    break; // leave empty (manual)
            }
        }

        // Optional helper: prime first responder (called by MainForm)
        public async Task<Character?> PrimeFirstResponder()
        {
            var next = await GetNextFromQueue();
            if (next != null)
                SetCurrentBot(next.UniqueName);
            return next;
        }

        public void ClearResponseQueue()
        {
            _responseQueue.Clear();
            _autoResponsesThisRound = 0;
        }
    }
}