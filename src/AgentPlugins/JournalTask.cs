using LetheAISharp;
using LetheAISharp.Agent;
using LetheAISharp.API;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WaifuAI.GBNF;

namespace WaifuAI.AgentPlugins
{

    public sealed class JournalTask : IAgentTask
    {
        public string Id => "JournalTask";
        public string Ability => "Write journal entries";

        public async Task<bool> Observe(BasePersona owner, AgentTaskSetting cfg, CancellationToken ct)
        {
            // Just a small delay so i don't have to remove async and do Task.ResultFrom everywhere. It's not like we're on a timer anyway.
            await Task.Delay(10, ct).ConfigureAwait(false);

            if (LLMEngine.Status != SystemStatus.Ready || !LLMEngine.SupportsSchema || LLMEngine.MaxContextLength < 8000)
                return false;

            var MinTimeInterval = cfg.GetSetting<TimeSpan>("TriggerInterval");
            var LastGoalSet = cfg.GetSetting<DateTime>("LastTrigger");
            if (DateTime.Now - LastGoalSet < MinTimeInterval)
                return false;

            return true;
        }

        public async Task Execute(BasePersona owner, AgentTaskSetting cfg, CancellationToken ct)
        {
            var response = new JournalRecord();
            var query = await GetQueryPrompt(owner);

            var result = await LLMEngine.SimpleQuery(query, ct).ConfigureAwait(false);
            try
            {
                response = JsonConvert.DeserializeObject<JournalRecord>(result);
            }
            catch (Exception ex)
            {
                LLMEngine.Logger?.LogError(ex, "Could not parse journal task response: {response}", result);
                return;
            }
            cfg.SetSetting("LastTrigger", DateTime.Now);
            if (response?.WriteInJournal != true)
                return;
            // Time to write in the journal, yepee
            var prompt = await GetEntryPrompt(owner, response.Topic);
            result = await LLMEngine.SimpleQuery(prompt, ct).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(result))
                return;
            result = result.RemoveThinkingBlocks();
            var entry = new MemoryUnit()
            {
                Name = $"{owner.Name}'s Journal Entry: {StringExtensions.DateToHumanString(DateTime.Now)}",
                Content = result.RemoveUnfinishedSentence().CleanupAndTrim(),
                Reason = response?.Topic ?? string.Empty,
                Category = MemoryType.Journal,
                Insertion = MemoryInsertion.Trigger,
                Added = DateTime.Now,
                EndTime = DateTime.Now.AddMonths(6),
                Priority = 1,
            };
            await entry.EmbedText().ConfigureAwait(false);
            await entry.UpdateSentiment().ConfigureAwait(false);
            owner.Brain.Memorize(entry, true);
            owner.Brain.AddUserReturnInsert($"{owner.Name} wrote an entry in their journal.");
            LLMEngine.Logger?.LogInformation("{char} wrote a new journal entry: {entry}", owner.Name, entry.Name);
        }

        private static async Task<object> GetEntryPrompt(BasePersona owner, string topic)
        {
            var lastsession = owner.History.Sessions[^2];
            var entries = owner.Brain.GetMemories(MemoryType.Journal);
            MemoryUnit? mostrecententry = null;
            if (entries.Count > 0)
            {
                mostrecententry = entries.OrderByDescending(m => m.Added).First();
            }
            var lastmessagedate = owner.History.GetLastMessageFrom(AuthorRole.User)?.Date ?? DateTime.MinValue;
            var timespansince = lastmessagedate > DateTime.MinValue ? DateTime.Now - lastmessagedate : TimeSpan.Zero;

            var prompt = new StringBuilder();
            prompt.AppendLinuxLine("You are {{char}} and you are about to write an entry in your private journal.").AppendLinuxLine();

            prompt.AppendLinuxLine("# {{char}}'s Biography").AppendLinuxLine();
            prompt.AppendLinuxLine("{{charbio}}").AppendLinuxLine();
            prompt.AppendLinuxLine("# {{user}}'s Biography").AppendLinuxLine();
            prompt.AppendLinuxLine("{{userbio}}").AppendLinuxLine();
            prompt.AppendLinuxLine("# Summary of previous session with {{user}}").AppendLinuxLine();
            prompt.AppendLinuxLine($"{lastsession.ToSnippet(TitleInsertType.None, LLMEngine.Bot.DatesInSessionSummaries, false, false)}").AppendLinuxLine();

            prompt.AppendLinuxLine("# Most recent dialogs between you and {{user}}").AppendLinuxLine();
            prompt.AppendLinuxLine(owner.History.CurrentSession.GetRawDialogs(1250, true, false, false, false)).AppendLinuxLine();

            if (mostrecententry is not null)
            {
                prompt.AppendLinuxLine("# Your most recent journal entry").AppendLinuxLine();
                prompt.AppendLinuxLine($"**{mostrecententry.Name}**");
                prompt.AppendLinuxLine($"{mostrecententry.Content.CleanupAndTrim()}").AppendLinuxLine();
            }
            prompt.AppendLinuxLine("# Relevant Information and Thoughts").AppendLinuxLine();
            if (lastmessagedate > DateTime.MinValue)
            {
                prompt.AppendLinuxLine($"You last spoke to {LLMEngine.User.Name} about {StringExtensions.TimeSpanToHumanString(timespansince)} ago.").AppendLinuxLine();
            }
            var recall = new PromptInserts();
            await owner.Brain.UpdateRagAndInserts(recall, topic, 3, 1.1f);
            foreach (var item in recall)
            {
                prompt.AppendLinuxLine(item.Content).AppendLinuxLine();
            }
            var builder = LLMEngine.GetPromptBuilder();
            builder.AddMessage(AuthorRole.SysPrompt, prompt.ToString());
            builder.AddMessage(AuthorRole.User, "As {{char}} write a new entry in your private journal. You've set the following topic for yourself: " + topic + LLMEngine.NewLine + "Feel free to write about something else if you feel like it. Make sure the entry reflects your personality and current situation. Write in a casual, personal tone, as if you were writing to yourself. Use first person perspective. Do not add a date (it's done automatically.");
            return builder.PromptToQuery(AuthorRole.Assistant, -1, 3000);
        }

        private static async Task<object> GetQueryPrompt(BasePersona owner)
        {
            var journal = new JournalRecord();

            var lastsession = owner.History.Sessions[^2];
            var entries = owner.Brain.GetMemories(MemoryType.Journal);
            MemoryUnit? mostrecententry = null;
            if (entries.Count > 0)
            {
                mostrecententry = entries.OrderByDescending(m => m.Added).First();
            }
            var lastmessagedate = owner.History.GetLastMessageFrom(AuthorRole.User)?.Date ?? DateTime.MinValue;
            var timespansince = lastmessagedate > DateTime.MinValue ? DateTime.Now - lastmessagedate : TimeSpan.Zero;

            var prompt = new StringBuilder();
            prompt.AppendLinuxLine("You are {{char}} and you are considering if you want to write another entry in your private journal.").AppendLinuxLine();

            prompt.AppendLinuxLine("# {{char}}'s Biography").AppendLinuxLine();
            prompt.AppendLinuxLine("{{charbio}}").AppendLinuxLine();
            prompt.AppendLinuxLine("# {{user}}'s Biography").AppendLinuxLine();
            prompt.AppendLinuxLine("{{userbio}}").AppendLinuxLine();
            prompt.AppendLinuxLine("# Summary of previous session with {{user}}").AppendLinuxLine();
            prompt.AppendLinuxLine($"{lastsession.ToSnippet(TitleInsertType.None, LLMEngine.Bot.DatesInSessionSummaries, false, false)}").AppendLinuxLine();
            prompt.AppendLinuxLine("# Most recent dialogs between you and {{user}}").AppendLinuxLine();
            prompt.AppendLinuxLine(owner.History.CurrentSession.GetRawDialogs(1250, true, false, false, false)).AppendLinuxLine();
            if (mostrecententry is not null)
            {
                prompt.AppendLinuxLine("# You most recent journal entry").AppendLinuxLine();
                prompt.AppendLinuxLine($"Title: {mostrecententry.Name}");
                prompt.AppendLinuxLine($"{mostrecententry.Content}").AppendLinuxLine();
            }
            prompt.AppendLinuxLine("# Relevant Information").AppendLinuxLine();
            if (lastmessagedate > DateTime.MinValue)
            {
                prompt.AppendLinuxLine($"You last spoke to {LLMEngine.User.Name} about {StringExtensions.TimeSpanToHumanString(timespansince)} ago.");
            }
            var goals = owner.Brain.GetMemories(MemoryType.Goal).FindAll(e => e.Insertion != MemoryInsertion.Trigger);
            if (goals.Count > 0)
            {
                prompt.AppendLinuxLine("You have the following current goals:");
                foreach (var g in goals.OrderBy(g => g.Priority).Take(5))
                {
                    prompt.AppendLinuxLine($"- {g.Name}");
                }
            }

            var builder = LLMEngine.GetPromptBuilder();
            builder.AddMessage(AuthorRole.SysPrompt, prompt.ToString());
            if (mostrecententry is null)
            {
                builder.AddMessage(AuthorRole.User, "Based on the above information, decide if you want to write a new journal entry or not. This will be your first entry, as such you should definitely write one. " + journal.GetQuery());
            }
            else
            {
                builder.AddMessage(AuthorRole.User, "Based on the above information, decide if you want to write a new journal entry or not. " + journal.GetQuery());
            }
            await builder.SetStructuredOutput(journal);
            return builder.PromptToQuery(AuthorRole.Assistant, -1, 1024, false);
        }

        public AgentTaskSetting GetDefaultSettings()
        {
            var settings = new AgentTaskSetting();
            settings.SetSetting<TimeSpan>("TriggerInterval", new TimeSpan(1, 0, 0, 0)); // 1 days
            settings.SetSetting<DateTime>("LastTrigger", DateTime.MinValue);

            return settings;
        }
    }
}