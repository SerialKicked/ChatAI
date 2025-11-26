using LetheAISharp;
using LetheAISharp.Agent;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WaifuAI.AgentPlugins;
using WaifuAI.GBNF;

namespace WaifuAI.Files
{
    public class CharBrain(BasePersona basePersona) : Brain(basePersona)
    {
        [JsonIgnore] protected new Character Owner => (Character)base.Owner;

        [JsonIgnore]
        public new AdvancedMoodState Mood
        {
            get
            {
                if (base.Mood is not AdvancedMoodState)
                    base.Mood = new AdvancedMoodState();
                return (AdvancedMoodState)base.Mood;
            }
            set => base.Mood = value;
        }

        [JsonIgnore] private MemoryVault fileSystem { get; set; } = new();

        private void ResetFileSystem()
        {
            fileSystem.Clear();
            var files = Memories.Where(m => m.Category == MemoryType.Image || m.Category == MemoryType.File).ToList();
            fileSystem.AddMemories(files);
        }

        public override void Init(BasePersona owner)
        {
            DisableRAG.Add(MemoryType.File);
            DisableRAG.Add(MemoryType.Image);
            base.Init(owner);
            ResetFileSystem();
        }

        public override void Memorize(MemoryUnit mem, bool skipDuplicateCheck = false)
        {
            base.Memorize(mem, skipDuplicateCheck);
            if (mem.Category == MemoryType.Image || mem.Category == MemoryType.File)
                ResetFileSystem();
        }

        public override void Forget(MemoryUnit mem)
        {
            base.Forget(mem);
            if (mem.Category == MemoryType.Image || mem.Category == MemoryType.File)
                ResetFileSystem();
        }

        public async Task<List<MemoryUnit>> GetFiles(string userInput, int maxResults = 3, float? maxDist = null)
        {
            if (fileSystem.Count == 0)
                return [];
            var res = await fileSystem.Search(userInput, maxResults, maxDist).ConfigureAwait(false);
            if (res.Count == 0)
                return [];
            return [.. res.Select(r => r.Memory)];
        }

        public override async Task ProcessPreviousSession()
        {
            await base.ProcessPreviousSession().ConfigureAwait(false);
            // Ensure there is a previous session to analyze and that we're not editing an old one
            if (Owner.History.Sessions.Count < 2 ||
                (Owner.History.CurrentSessionID != -1 && Owner.History.CurrentSessionID != Owner.History.Sessions.Count - 1))
                return;

            var prevsession = Owner.History.Sessions[^2];
            
            if (prevsession.MetaData.IsRoleplaySession && prevsession.Messages.Count >= 10)
                Mood.Horniness -= (0.015 * prevsession.Messages.Count);

            // Analyze previous session for triggers
            var action = AgentRuntime.GetAction<MoodAnalysis?, SessionMoodCheckParams>("SessionMoodCheckAction");
            if (action is null)
                return;

            var result = await action.Execute(new SessionMoodCheckParams(prevsession), CancellationToken.None);
            if (result is null)
                return;

            static double Delta(Modifier m) => m switch
            {
                Modifier.HighReduction  => -0.2,
                Modifier.SmallReduction => -0.1,
                Modifier.SmallIncrease  =>  0.1,
                Modifier.HighIncrease   =>  0.2,
                _ => 0.0
            };

            Mood.Horniness  += Delta(result.Horniness);
            Mood.Submission += Delta(result.Submission);
            Mood.Energy     += Delta(result.Energy);
            Mood.Cheer      += Delta(result.Happy);
            Mood.Curiosity  += Delta(result.Curiosity);
            Mood.Sanity     += Delta(result.Sanity);
        }

        public override async Task<List<VaultResult>> Search(string message, int maxRes, float maxDist)
        {
            if (!LLMEngine.Settings.RAGEnabled)
                return [];

            var toretrieve = maxRes * 2 + 5;
            if (toretrieve < 30)
                toretrieve = 30;
            var found = await base.Search(message, toretrieve, maxDist).ConfigureAwait(false);
            if (found.Count == 0)
                return [];

            // Check if message contains the words RP or roleplay
            var requestIsAboutRoleplay = message.Contains(" RP", StringComparison.OrdinalIgnoreCase) || message.Contains(" roleplay", StringComparison.OrdinalIgnoreCase) || message.Contains(" role play", StringComparison.OrdinalIgnoreCase);

            foreach (var item in found)
            {
                if (item.Memory.Category == MemoryType.ChatSession && item.Memory is ChatSession session)
                {
                    if (session.MetaData.IsRoleplaySession)
                    {
                        if (requestIsAboutRoleplay)
                            item.Distance -= 0.015f; // Boost RP sessions
                        else
                            item.Distance += 0.015f; // Decay RP sessions
                    }
                    // Mark sticky as not wanted because they are handled with different insertion method
                    if (session.Sticky && LLMEngine.Settings.SessionMemorySystem)
                        item.Distance += 2f;
                }
                var embedhelpers = MemoryUnit.EmbedHelpers[item.Memory.Category];
                if (embedhelpers?.Count > 0)
                {
                    foreach (var kw in embedhelpers)
                    {
                        if (message.ContainsWholeWord(kw, StringComparison.OrdinalIgnoreCase))
                        {
                            item.Distance -= 0.015f; // Boost if the message contains one of the embed helpers for this category
                            break;
                        }
                    }
                }
            }
            // Remove entries with distance above limit
            found.RemoveAll(e => e.Distance > maxDist);
            found.Sort((a, b) => a.Distance.CompareTo(b.Distance));
            // If we have too many results, trim the list to maxRes
            if (found.Count > maxRes)
                found = found.GetRange(0, maxRes);
            return found;
        }

        public override SingleMessage? BuildAwayMessage(bool forced = false)
        {
            var res = base.BuildAwayMessage(forced);
            if (Owner.LockSettings.Enabled && res is not null)
            {
                var infostr = Owner.LockManager.GetStatusMessage();
                if (!string.IsNullOrEmpty(infostr))
                {
                    res.Message += LLMEngine.NewLine + LLMEngine.NewLine + infostr;
                }
            }
            return res;
        }
    }
}
