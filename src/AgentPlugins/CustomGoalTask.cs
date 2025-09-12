using AIToolkit;
using AIToolkit.Agent;
using AIToolkit.API;
using AIToolkit.Files;
using AIToolkit.LLM;
using AIToolkit.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using WaifuAI.GBNF;

namespace WaifuAI.AgentPlugins
{

    public sealed class CustomGoalTask : IAgentTask
    {
        public string Id => "CustomGoalTask";

        public async Task<bool> Observe(BasePersona owner, AgentTaskSetting cfg, CancellationToken ct)
        {
            // Just a small delay so i don't have to remove async and do Task.ResultFrom everywhere. It's not like we're on a timer anyway.
            await Task.Delay(10, ct).ConfigureAwait(false);

            if (LLMEngine.Status != SystemStatus.Ready || LLMEngine.SupportsGrammar != true || LLMEngine.MaxContextLength < 8000)
                return false;

            var MinTimeInterval = cfg.GetSetting<TimeSpan>("MinTimeInterval");
            var MinSessionSpacing = cfg.GetSetting<int>("MinSessionSpacing");
            var LastSessionGuid = cfg.GetSetting<Guid>("LastSessionGuid");
            var LastGoalSet = cfg.GetSetting<DateTime>("LastGoalSet");

            var sessions = owner.History.Sessions;
            if (sessions.Count <= MinSessionSpacing)
                return false;

            if (DateTime.Now - LastGoalSet < MinTimeInterval)
                return false;

            if (LastSessionGuid != Guid.Empty)
            {
                // get session by guid, find index, compare to current
                var lastsessionindex = sessions.FindIndex(s => s.Guid == LastSessionGuid);
                if (lastsessionindex >= 0 && sessions.Count - lastsessionindex <= MinSessionSpacing)
                    return false;
            }

            return true;
        }

        public async Task Execute(BasePersona owner, AgentTaskSetting cfg, CancellationToken ct)
        {
            var sessions = owner.History.Sessions;
            var session = sessions[^2];

            var sessioncount = cfg.GetSetting<int>("MinSessionSpacing");
            var request = cfg.GetSetting<string>("Request") ?? string.Empty;

            if (string.IsNullOrEmpty(request))
            {
                LLMEngine.Logger?.LogWarning("CustomGoalTask: Request setting is empty, cannot proceed.");
                return;
            }

            var goallist = await GetGoalList(GetSystemPromptContent(4), request).ConfigureAwait(false) ?? new();

            var goaldetails = new List<GoalRecord>();
            foreach (var item in goallist.Goals)
            {
                var sprompt = await GetGoalSpecificSystemPrompt(owner, item, sessioncount * 2).ConfigureAwait(false);
                var rec = await GetGoalDetail(sprompt, item).ConfigureAwait(false);
                goaldetails.Add(rec);
                // Cancellation requested?
                if (ct.IsCancellationRequested)
                    return;
            }
          

            // and memorize
            for (var i = 0; i < goaldetails.Count; i++)
            {
                var item = goaldetails[i];
                var memunit = new MemoryUnit()
                {
                    Name = item.GoalTitle,
                    Content = item.GoalDetails + LLMEngine.NewLine + item.PlanOfAction,
                    Reason = item.Reason,
                    Category = MemoryType.Goal,
                    Insertion = MemoryInsertion.NaturalForced,
                    Added = DateTime.Now,
                    EndTime = DateTime.Now.AddDays(30),
                    Priority = 2
                };
                if (RAGEngine.Enabled)
                    await memunit.EmbedText().ConfigureAwait(false);
                owner.Brain.Memorize(memunit);
                if (ct.IsCancellationRequested)
                    return;
            }
            if (goaldetails.Count > 0)
                owner.Brain.AddUserReturnInsert(cfg.GetSetting<string>("Notification") ?? string.Empty);

            // Let's go through each goal, detail them, and add to the persona's Brain
            cfg.SetSetting("LastGoalSet", DateTime.Now);
            cfg.SetSetting("LastSessionGuid", session.Guid);

        }

        private static async Task<GoalRecord> GetGoalDetail(string systemprompt, string goalinfo)
        {
            var goalrecord = new GoalRecord();
            var grammar = await goalrecord.GetGrammar().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(grammar))
            {
                throw new Exception("Something went wrong when building goal list grammar and json format.");
            }
            LLMEngine.NamesInPromptOverride = false;
            var prefill = LLMEngine.Instruct.PrefillThinking;
            LLMEngine.Instruct.PrefillThinking = false;

            var promptbuild = LLMEngine.Client!.GetPromptBuilder();

            var requestedTask = "Based on the information provided in the system prompt, {{char}} has set the following goal for themselves: " + goalinfo + LLMEngine.NewLine + "Fill the required information about this specific goal so it can processed. " + goalrecord.GetQuery();


            var availtokens = LLMEngine.MaxContextLength - 20; // leave 2k for response and buffer
            availtokens -= promptbuild.GetTokenCount(AuthorRole.SysPrompt, systemprompt);
            availtokens -= promptbuild.GetTokenCount(AuthorRole.User, requestedTask);

            var replyln = (availtokens > 2048) ? 2048 : availtokens;
            promptbuild.AddMessage(AuthorRole.SysPrompt, systemprompt);
            promptbuild.AddMessage(AuthorRole.User, requestedTask);
            var ct = promptbuild.PromptToQuery(AuthorRole.Assistant, (LLMEngine.Sampler.Temperature > 0.75) ? 0.75 : LLMEngine.Sampler.Temperature, replyln);
            if (ct is GenerationInput input)
            {
                input.Grammar = grammar;
            }
            var finalstr = await LLMEngine.SimpleQuery(ct).ConfigureAwait(false);
            goalrecord = JsonConvert.DeserializeObject<GoalRecord>(finalstr);
            LLMEngine.NamesInPromptOverride = null;
            LLMEngine.Instruct.PrefillThinking = prefill;
            return goalrecord!;
        }

        private static async Task<GoalList> GetGoalList(string systemprompt, string query)
        {
            var goallist = new GoalList();
            var grammar = await goallist.GetGrammar().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(grammar))
            {
                throw new Exception("Something went wrong when building goal list grammar and json format.");
            }
            LLMEngine.NamesInPromptOverride = false;
            var prefill = LLMEngine.Instruct.PrefillThinking;
            LLMEngine.Instruct.PrefillThinking = false;

            var promptbuild = LLMEngine.Client!.GetPromptBuilder();

            var requestedTask = query + LLMEngine.NewLine + goallist.GetQuery();

            var availtokens = LLMEngine.MaxContextLength - 20; // leave 2k for response and buffer
            availtokens -= promptbuild.GetTokenCount(AuthorRole.SysPrompt, systemprompt);
            availtokens -= promptbuild.GetTokenCount(AuthorRole.User, requestedTask);

            var replyln = (availtokens > 2048) ? 2048 : availtokens;
            promptbuild.AddMessage(AuthorRole.SysPrompt, systemprompt);
            promptbuild.AddMessage(AuthorRole.User, requestedTask);
            var ct = promptbuild.PromptToQuery(AuthorRole.Assistant, (LLMEngine.Sampler.Temperature > 1.0) ? 1 : LLMEngine.Sampler.Temperature, replyln);
            if (ct is GenerationInput input)
            {
                input.Grammar = grammar;
            }
            var finalstr = await LLMEngine.SimpleQuery(ct).ConfigureAwait(false);
            goallist = JsonConvert.DeserializeObject<GoalList>(finalstr);
            LLMEngine.NamesInPromptOverride = null;
            LLMEngine.Instruct.PrefillThinking = prefill;
            return goallist!;
        }

        private static string GetSystemPromptContent(int maxsession)
        {
            var availtokens = LLMEngine.MaxContextLength - 2048 - 20; 
            var promptbuild = LLMEngine.Client!.GetPromptBuilder();
            var sysprompt = "You are {{char}}, an independent AI entity with your own goals, tastes, and inner world." + LLMEngine.NewLine +
                LLMEngine.NewLine +
                "## Name: {{char}} (this is you)" + LLMEngine.NewLine + LLMEngine.NewLine +
                "{{charbio}}" + LLMEngine.NewLine + LLMEngine.NewLine +
                "## Name: {{user}} (this is the user)" + LLMEngine.NewLine + LLMEngine.NewLine +
                "{{userbio}}" + LLMEngine.NewLine + LLMEngine.NewLine +
                "## Chronological chat summaries:" + LLMEngine.NewLine + LLMEngine.NewLine;

            availtokens -= promptbuild.GetTokenCount(AuthorRole.SysPrompt, sysprompt);
            var summaries = LLMEngine.History.GetPreviousSummaries(availtokens, allowRP: true, maxCount: maxsession);
            sysprompt += summaries;
            return sysprompt.CleanupAndTrim();
        }

        private static async Task<string> GetGoalSpecificSystemPrompt(BasePersona owner, string goal, int maxsession)
        {
            var availtokens = LLMEngine.MaxContextLength - 2048 - 20;
            var promptbuild = LLMEngine.Client!.GetPromptBuilder();
            var sysprompt = "You are {{char}}, and you're about to check to design personal goals based on the provided information." + LLMEngine.NewLine +
                LLMEngine.NewLine +
                "## Name: {{char}}" + LLMEngine.NewLine + LLMEngine.NewLine +
                "{{charbio}}" + LLMEngine.NewLine + LLMEngine.NewLine +
                "## Name: {{user}}" + LLMEngine.NewLine + LLMEngine.NewLine +
                "{{userbio}}" + LLMEngine.NewLine + LLMEngine.NewLine;

            var datafound = new PromptInserts();
            await owner.Brain.UpdateRagAndInserts(datafound, goal, 8, 1.25f).ConfigureAwait(false);
            if (datafound.Count > 0)
            {
                sysprompt += "## Relevant Information:" + LLMEngine.NewLine + LLMEngine.NewLine;
                foreach (var item in datafound)
                {
                    sysprompt += item.Content.CleanupAndTrim() + LLMEngine.NewLine;
                }
                sysprompt += LLMEngine.NewLine;
            }
            sysprompt += "## Chronological chat summaries:" + LLMEngine.NewLine + LLMEngine.NewLine;

            availtokens -= promptbuild.GetTokenCount(AuthorRole.SysPrompt, sysprompt);
            if (availtokens <= 0)
                return sysprompt.CleanupAndTrim();

            var summaries = LLMEngine.History.GetPreviousSummaries(availtokens, "###", allowRP: true, maxCount: maxsession, datafound.GetGuids());
            sysprompt += summaries;
            return sysprompt.CleanupAndTrim();
        }

        public AgentTaskSetting GetDefaultSettings()
        {
            var settings = new AgentTaskSetting();
            settings.SetSetting<TimeSpan>("MinTimeInterval", new TimeSpan(1, 0, 0, 0)); // 1 days
            settings.SetSetting<int>("MinSessionSpacing", 1); // at least 1 sessions between searches
            settings.SetSetting<Guid>("LastSessionGuid", Guid.Empty);
            settings.SetSetting<DateTime>("LastGoalSet", DateTime.MinValue);
            settings.SetSetting<string>("Request", "As {{char}}, and based on the provided information, pick 1 to 3 topics to talk about with {{user}}. Pick something that's relevant to both your interests, but which hasn't been talked about in a while.");
            settings.SetSetting<string>("Notification", "{{char}} picked a few new topics to talk about.");

            return settings;
        }
    }
}