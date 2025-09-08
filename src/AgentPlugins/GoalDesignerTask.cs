using AIToolkit;
using AIToolkit.Agent;
using AIToolkit.API;
using AIToolkit.Files;
using AIToolkit.GBNF;
using AIToolkit.LLM;
using AIToolkit.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Text;
using WaifuAI.GBNF;
using static AIToolkit.SearchAPI.WebSearchAPI;

namespace WaifuAI.AgentPlugins
{
    public enum RPHandling { Always, Never, Random }

    public sealed class GoalDesignerTask : IAgentTask
    {
        public string Id => "GoalDesignerTask";

        public async Task<bool> Observe(BasePersona owner, AgentTaskSetting cfg, CancellationToken ct)
        {
            // Just a small delay so i don't have to remove async and do Task.ResultFrom everywhere. It's not like we're on a timer anyway.
            await Task.Delay(10, ct);

            if (LLMSystem.Status != SystemStatus.Ready || LLMSystem.Client?.SupportsSchema != true || LLMSystem.MaxContextLength < 8000)
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

            var mainPrompt = GetSystemPromptContent(owner, rpmode);

            // Now, let's get the list of self goals
            var goaldetails = new List<GoalRecord>();

            var req = "Based on the information provided in the system prompt, write a list of personal goals you want to set for yourself as {{char}}. These can be actions or topics you want to search on the internet. Keep the list realistic and order it from most to least important.";
            var goallist = await GetGoalList(owner, mainPrompt, req);
            if (goallist?.Goals.Count > 0)
            {
                foreach (var item in goallist.Goals)
                {
                    var rec = await GetGoalDetail(owner, mainPrompt, item);
                    goaldetails.Add(rec);
                    // Cancellation requested?
                    if (ct.IsCancellationRequested)
                        return;
                }
            }

            for (var i = 0; i < goaldetails.Count; i++)
            {
                var item = goaldetails[i];
                var memunit = new MemoryUnit()
                {
                    Name = item.GoalTitle,
                    Content = item.GoalDetails + LLMSystem.NewLine + item.PlanOfAction,
                    Reason = item.Reason,
                    Category = MemoryType.Goal,
                    Insertion = MemoryInsertion.Natural,
                    Added = DateTime.Now,
                    EndTime = DateTime.Now.AddDays(30),
                    Priority = Math.Clamp(3 - i, 0, 3)
                };
                if (RAGSystem.Enabled)
                    await memunit.EmbedText();
                owner.Brain.Memories.Add(memunit);
            }


            if (ct.IsCancellationRequested)
                return;
            goaldetails.Clear();
            req = "Based on the information provided in the system prompt, write a list of things that you want {{user}} to do or become for you. This can also include topics where you want to question or challenge {{user}}'s perspective. Order the list from most to least important.";
            goallist = await GetGoalList(owner, mainPrompt, req);
            if (goallist?.Goals.Count > 0)
            {
                foreach (var item in goallist.Goals)
                {
                    var rec = await GetGoalDetail(owner, mainPrompt, item);
                    goaldetails.Add(rec);
                    // Cancellation requested?
                    if (ct.IsCancellationRequested)
                        return;
                }
            }

            for (var i = 0; i < goaldetails.Count; i++)
            {
                var item = goaldetails[i];
                var memunit = new MemoryUnit()
                {
                    Name = item.GoalTitle,
                    Content = item.GoalDetails + LLMSystem.NewLine + item.PlanOfAction,
                    Reason = item.Reason,
                    Category = MemoryType.Goal,
                    Insertion = MemoryInsertion.Natural,
                    Added = DateTime.Now,
                    EndTime = DateTime.Now.AddDays(30),
                    Priority = Math.Clamp(3 - i, 0, 3)
                };
                if (RAGSystem.Enabled)
                    await memunit.EmbedText();
                owner.Brain.Memories.Add(memunit);
            }

            // Let's go through each goal, detail them, and add to the persona's Brain
            cfg.SetSetting("LastGoalSet", DateTime.Now);
            cfg.SetSetting("LastSessionGuid", session.Guid);

        }

        private async Task<GoalRecord> GetGoalDetail(BasePersona owner, string systemprompt, string goalinfo)
        {
            var goalrecord = new GoalRecord();
            var grammar = await goalrecord.GetGrammar();
            if (string.IsNullOrWhiteSpace(grammar))
            {
                throw new Exception("Something went wrong when building goal list grammar and json format.");
            }
            LLMSystem.NamesInPromptOverride = false;
            var prefill = LLMSystem.Instruct.PrefillThinking;
            LLMSystem.Instruct.PrefillThinking = false;

            var promptbuild = LLMSystem.Client!.GetPromptBuilder();

            var requestedTask = "Based on the information provided in the system prompt, {{char}} has set the following goal for themselves: " + goalinfo + LLMSystem.NewLine + "Fill the required information about this specific goal so it can processed. " + goalrecord.GetQuery();


            var availtokens = LLMSystem.MaxContextLength - 20; // leave 2k for response and buffer
            availtokens -= promptbuild.GetTokenCount(AuthorRole.SysPrompt, systemprompt);
            availtokens -= promptbuild.GetTokenCount(AuthorRole.User, requestedTask);

            var replyln = (availtokens > 2048) ? 2048 : availtokens;
            promptbuild.AddMessage(AuthorRole.SysPrompt, systemprompt);
            promptbuild.AddMessage(AuthorRole.User, requestedTask);
            var ct = promptbuild.PromptToQuery(AuthorRole.Assistant, (LLMSystem.Sampler.Temperature > 0.75) ? 0.75 : LLMSystem.Sampler.Temperature, replyln);
            if (ct is GenerationInput input)
            {
                input.Grammar = grammar;
            }
            var finalstr = await LLMSystem.SimpleQuery(ct);
            goalrecord = JsonConvert.DeserializeObject<GoalRecord>(finalstr);
            LLMSystem.NamesInPromptOverride = null;
            LLMSystem.Instruct.PrefillThinking = prefill;
            return goalrecord!;
        }

        private async Task<GoalList> GetGoalList(BasePersona owner, string systemprompt, string query)
        {
            var goallist = new GoalList();
            var grammar = await goallist.GetGrammar();
            if (string.IsNullOrWhiteSpace(grammar))
            {
                throw new Exception("Something went wrong when building goal list grammar and json format.");
            }
            LLMSystem.NamesInPromptOverride = false;
            var prefill = LLMSystem.Instruct.PrefillThinking;
            LLMSystem.Instruct.PrefillThinking = false;

            var promptbuild = LLMSystem.Client!.GetPromptBuilder();

            var requestedTask = query + LLMSystem.NewLine + goallist.GetQuery();

            var availtokens = LLMSystem.MaxContextLength - 20; // leave 2k for response and buffer
            availtokens -= promptbuild.GetTokenCount(AuthorRole.SysPrompt, systemprompt);
            availtokens -= promptbuild.GetTokenCount(AuthorRole.User, requestedTask);

            var replyln = (availtokens > 2048) ? 2048 : availtokens;
            promptbuild.AddMessage(AuthorRole.SysPrompt, systemprompt);
            promptbuild.AddMessage(AuthorRole.User, requestedTask);
            var ct = promptbuild.PromptToQuery(AuthorRole.Assistant, (LLMSystem.Sampler.Temperature > 1.0) ? 1 : LLMSystem.Sampler.Temperature, replyln);
            if (ct is GenerationInput input)
            {
                input.Grammar = grammar;
            }
            var finalstr = await LLMSystem.SimpleQuery(ct);
            goallist = JsonConvert.DeserializeObject<GoalList>(finalstr);
            LLMSystem.NamesInPromptOverride = null;
            LLMSystem.Instruct.PrefillThinking = prefill;
            return goallist!;
        }


        private string GetSystemPromptContent(BasePersona owner, RPHandling rpHandling)
        {
            var availtokens = LLMSystem.MaxContextLength - 2048 - 20; 
            var promptbuild = LLMSystem.Client!.GetPromptBuilder();
            var sysprompt = "You are {{char}}, and you're about to check to design personal goals based on the information provided." + LLMSystem.NewLine +
                LLMSystem.NewLine +
                "## Name: {{char}}" + LLMSystem.NewLine + LLMSystem.NewLine +
                "{{charbio}}" + LLMSystem.NewLine + LLMSystem.NewLine +
                "## Name: {{user}}" + LLMSystem.NewLine + LLMSystem.NewLine +
                "{{userbio}}" + LLMSystem.NewLine + LLMSystem.NewLine +
                "## Chronological chat summaries:" + LLMSystem.NewLine + LLMSystem.NewLine;

            availtokens -= promptbuild.GetTokenCount(AuthorRole.SysPrompt, sysprompt);
            var AllowRP = rpHandling == RPHandling.Always || (rpHandling == RPHandling.Random && (new Random()).Next(0, 3) == 1);
            var summaries = LLMSystem.History.GetPreviousSummaries(availtokens, allowRP: AllowRP, maxCount: 25);
            sysprompt += summaries;
            return sysprompt.CleanupAndTrim();
        }

        public AgentTaskSetting GetDefaultSettings()
        {
            var settings = new AgentTaskSetting();
            settings.SetSetting<TimeSpan>("MinTimeInterval", new TimeSpan(7,0,0,0)); // 7 days
            settings.SetSetting<int>("MinSessionSpacing", 4); // at least 2 sessions between searches
            settings.SetSetting<Guid>("LastSessionGuid", Guid.Empty);
            settings.SetSetting<DateTime>("LastGoalSet", default(DateTime));
            settings.SetSetting<int>("IncludeRPSession", (int)RPHandling.Random);

            return settings;
        }
    }
}