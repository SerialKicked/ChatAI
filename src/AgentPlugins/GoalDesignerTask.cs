using LetheAISharp;
using LetheAISharp.Agent;
using LetheAISharp.API;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using LetheChat.GBNF;

namespace LetheChat.AgentPlugins
{
    public enum RPHandling { Always, Never, Random }

    public sealed class GoalDesignerTask : IAgentTask
    {
        public string Id => "GoalDesignerTask";
        public string Ability => "Set goals";

        public async Task<bool> Observe(BasePersona owner, AgentTaskSetting cfg, CancellationToken ct)
        {
            // Just a small delay so i don't have to remove async and do Task.ResultFrom everywhere. It's not like we're on a timer anyway.
            await Task.Delay(10, ct).ConfigureAwait(false);

            if (LLMEngine.Status != SystemStatus.Ready || LLMEngine.SupportsSchema != true || LLMEngine.MaxContextLength < 8000)
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

            var rpmode = (RPHandling)cfg.GetSetting<int>("IncludeRPSession");
            if (rpmode == RPHandling.Random)
                rpmode = new Random().Next(0, 2) == 1 ? RPHandling.Always : RPHandling.Never;

            var sessioncount = cfg.GetSetting<int>("MinSessionSpacing");

            var mainPrompt = GetSystemPromptContent(rpmode, sessioncount * 2);

            var allgoals = new List<string>();

            // add personal goals
            var req = "Based on the information provided, write a list of personal goals you want to set for yourself as {{mchar}}. Those have to be goals, or topics of research for you directly. They must be of interest to you, independently of {{user}}'s own goals.";
            var goallist = await GetGoalList(mainPrompt, req).ConfigureAwait(false) ?? new();
            if (goallist.Goals.Count > 2)
                goallist.Goals = [.. goallist.Goals.Take(2)];
            allgoals.AddRange(goallist.Goals);

            // add user specific goals
            req = "Based on the information provided, write a list of things that you want {{user}} to do or become for you. This can also include any personal topic where you want to question or challenge {{user}}'s perspective. Order the list from most to least important.";
            goallist = await GetGoalList(mainPrompt, req).ConfigureAwait(false);
            if (goallist.Goals.Count > 2)
                goallist.Goals = [.. goallist.Goals.Take(2)];
            allgoals.AddRange(goallist.Goals);

            // add goals from sessions
            var grabcount = cfg.GetSetting<int>("MinSessionSpacing");

            // grab the list of sessions to analyze
            var prevsessions = sessions.Skip(Math.Max(0, sessions.Count - 1 - grabcount)).Take(grabcount).ToList();
            if (rpmode == RPHandling.Never)
                prevsessions.RemoveAll(s => !s.MetaData.IsRoleplaySession);
            if (prevsessions.Count > 0)
            {
                allgoals.AddRange(prevsessions[^1].MetaData.FutureGoals);
            }

            var goaldetails = new List<GoalRecord>();
            foreach (var item in allgoals)
            {
                var sprompt = await GetGoalSpecificSystemPrompt(owner, rpmode, item, sessioncount * 2).ConfigureAwait(false);
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
                    Insertion = MemoryInsertion.Natural,
                    Added = DateTime.Now,
                    EndTime = DateTime.Now.AddDays(30),
                    Priority = Math.Clamp((6 - i) / 2 , 0, 3)
                };
                if (LLMEngine.Settings.RAGEnabled)
                    await memunit.BuildEmbedding().ConfigureAwait(false);
                owner.Brain.Memorize(memunit);
                if (ct.IsCancellationRequested)
                    return;
            }

            if (goaldetails.Count > 0)
                owner.Brain.AddUserReturnInsert("{{mchar}} has set some new personal goals.", this.Id);


            // Let's go through each goal, detail them, and add to the persona's Brain
            cfg.SetSetting("LastGoalSet", DateTime.Now);
            cfg.SetSetting("LastSessionGuid", session.Guid);

        }

        private static async Task<GoalRecord> GetGoalDetail(string systemprompt, string goalinfo)
        {
            var goalrecord = new GoalRecord();

            var promptbuild = LLMEngine.GetPromptBuilder();

            var requestedTask = "Based on the information provided in the system prompt, {{mchar}} has set the following goal for themselves: " + goalinfo + LLMEngine.NewLine + "Fill the required information about this specific goal so it can be processed. " + goalrecord.GetQuery();

            var availtokens = LLMEngine.MaxContextLength - 20; // leave 2k for response and buffer
            availtokens -= promptbuild.GetTokenCount(AuthorRole.System, systemprompt);
            availtokens -= promptbuild.GetTokenCount(AuthorRole.User, requestedTask);

            var replyln = (availtokens > 2048) ? 2048 : availtokens;
            promptbuild.AddMessage(new SingleMessage(AuthorRole.System, systemprompt));
            promptbuild.AddMessage(new SingleMessage(AuthorRole.User, requestedTask));
            await promptbuild.SetStructuredOutput(goalrecord).ConfigureAwait(false);
            var ct = promptbuild.PromptToQuery(AuthorRole.Assistant, (LLMEngine.Sampler.Temperature > 0.75) ? 0.75 : LLMEngine.Sampler.Temperature, replyln);
            var finalstr = await LLMEngine.SimpleQuery(ct).ConfigureAwait(false);
            finalstr = finalstr.RemoveThinkingBlocks();
            goalrecord = JsonConvert.DeserializeObject<GoalRecord>(finalstr);
            return goalrecord!;
        }

        private static async Task<GoalList> GetGoalList(string systemprompt, string query)
        {
            var goallist = new GoalList();

            var promptbuild = LLMEngine.GetPromptBuilder();

            var requestedTask = query + LLMEngine.NewLine + goallist.GetQuery();

            var availtokens = LLMEngine.MaxContextLength - 20; // leave 2k for response and buffer
            availtokens -= promptbuild.GetTokenCount(AuthorRole.System, systemprompt);
            availtokens -= promptbuild.GetTokenCount(AuthorRole.User, requestedTask);

            var replyln = (availtokens > 2048) ? 2048 : availtokens;
            promptbuild.AddMessage(new SingleMessage(AuthorRole.System, systemprompt));
            promptbuild.AddMessage(new SingleMessage(AuthorRole.User, requestedTask));
            await promptbuild.SetStructuredOutput(goallist).ConfigureAwait(false);
            var ct = promptbuild.PromptToQuery(AuthorRole.Assistant, (LLMEngine.Sampler.Temperature > 1.0) ? 1 : LLMEngine.Sampler.Temperature, replyln);
            var finalstr = await LLMEngine.SimpleQuery(ct).ConfigureAwait(false);
            finalstr = finalstr.RemoveThinkingBlocks();
            goallist = JsonConvert.DeserializeObject<GoalList>(finalstr);
            return goallist!;
        }

        private static string GetSystemPromptContent(RPHandling rpHandling, int maxsession)
        {
            var availtokens = LLMEngine.MaxContextLength - 2048 - 20; 
            var promptbuild = LLMEngine.GetPromptBuilder();
            var sysprompt = "You are {{mchar}}, an independent AI entity with your own goals, tastes, and inner world." + LLMEngine.NewLine +
                LLMEngine.NewLine +
                "## Name: {{mchar}} (this is you)" + LLMEngine.NewLine + LLMEngine.NewLine +
                "{{mcharbio}}" + LLMEngine.NewLine + LLMEngine.NewLine +
                "## Name: {{user}} (this is the user)" + LLMEngine.NewLine + LLMEngine.NewLine +
                "{{userbio}}" + LLMEngine.NewLine + LLMEngine.NewLine +
                "## Chronological chat summaries:" + LLMEngine.NewLine + LLMEngine.NewLine;

            availtokens -= promptbuild.GetTokenCount(AuthorRole.System, sysprompt);
            var AllowRP = rpHandling == RPHandling.Always ? MemoryMode.Normal : MemoryMode.NonRPOnly;
            var summaries = LLMEngine.History.GetPreviousSummaries(availtokens, allowRP: AllowRP, maxCount: maxsession);
            sysprompt += summaries;
            return sysprompt.CleanupAndTrim();
        }

        private static async Task<string> GetGoalSpecificSystemPrompt(BasePersona owner, RPHandling rpHandling, string goal, int maxsession)
        {
            var availtokens = LLMEngine.MaxContextLength - 2048 - 20;
            var promptbuild = LLMEngine.GetPromptBuilder();
            var sysprompt = "You are {{mchar}}, and you're about to check to design personal goals based on the provided information." + LLMEngine.NewLine +
                LLMEngine.NewLine +
                "## Name: {{mchar}}" + LLMEngine.NewLine + LLMEngine.NewLine +
                "{{mcharbio}}" + LLMEngine.NewLine + LLMEngine.NewLine +
                "## Name: {{user}}" + LLMEngine.NewLine + LLMEngine.NewLine +
                "{{userbio}}" + LLMEngine.NewLine + LLMEngine.NewLine;

            var datafound = new PromptInserts();
            await owner.Brain.GetRAGandInserts(datafound, goal, 8, 0.125f).ConfigureAwait(false);
            if (datafound.Count > 0)
            {
                sysprompt += "## Relevant Information:" + LLMEngine.NewLine + LLMEngine.NewLine;
                foreach (var item in datafound)
                {
                    sysprompt += item.ToContent().CleanupAndTrim() + LLMEngine.NewLine;
                }
                sysprompt += LLMEngine.NewLine;
            }
            sysprompt += "## Chronological chat summaries:" + LLMEngine.NewLine + LLMEngine.NewLine;

            availtokens -= promptbuild.GetTokenCount(AuthorRole.System, sysprompt);
            if (availtokens <= 0)
                return sysprompt.CleanupAndTrim();

            var AllowRP = rpHandling == RPHandling.Always ? MemoryMode.Normal : MemoryMode.NonRPOnly;
            var summaries = LLMEngine.History.GetPreviousSummaries(availtokens, "###", allowRP: AllowRP, maxCount: maxsession, datafound.GetGuids());
            sysprompt += summaries;
            return sysprompt.CleanupAndTrim();
        }

        public AgentTaskSetting GetDefaultSettings()
        {
            var settings = new AgentTaskSetting();
            settings.SetSetting<TimeSpan>("MinTimeInterval", new TimeSpan(1,0,0,0)); // 7 days
            settings.SetSetting<int>("MinSessionSpacing", 1); // at least 2 sessions between searches
            settings.SetSetting<Guid>("LastSessionGuid", Guid.Empty);
            settings.SetSetting<DateTime>("LastGoalSet", DateTime.MinValue);
            settings.SetSetting<int>("IncludeRPSession", (int)RPHandling.Random);

            return settings;
        }
    }
}