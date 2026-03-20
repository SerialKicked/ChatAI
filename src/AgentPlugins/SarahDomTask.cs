using LetheAISharp;
using LetheAISharp.Agent;
using LetheAISharp.API;
using LetheAISharp.Files;
using LetheAISharp.GBNF;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenAI.Responses;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LetheChat.GBNF;

namespace LetheChat.AgentPlugins
{

    public sealed class SarahDomTask : IAgentTask
    {
        public string Id => "SarahDomTask";
        public string Ability => "manipulate user behavior";

        public AgentTaskSetting GetDefaultSettings()
        {
            var settings = new AgentTaskSetting();
            settings.SetSetting<int>("DepthOnFirstRun", 6);
            settings.SetSetting<Guid>("LastGuid", Guid.Empty);
            settings.SetSetting<string>("Instruction", "As {{mchar}}, you need to make sure {{user}} abides by the terms stated below:" + LLMEngine.NewLine + LLMEngine.NewLine + "{{memory:User: Contract}}");
            return settings;
        }

        public async Task<bool> Observe(BasePersona owner, AgentTaskSetting cfg, CancellationToken ct)
        {
            // Just a small delay so i don't have to remove async and do Task.ResultFrom everywhere. It's not like we're on a timer anyway.
            await Task.Delay(10, ct).ConfigureAwait(false);

            if (LLMEngine.Status != SystemStatus.Ready || !LLMEngine.SupportsSchema || LLMEngine.MaxContextLength < 8000 || owner.History.Sessions.Count < 3)
                return false;
            if (cfg.GetSetting<Guid>("LastGuid") == owner.History.Sessions[^2].Guid)
                return false; // already processed this 

            return true;
        }

        public async Task Execute(BasePersona owner, AgentTaskSetting cfg, CancellationToken ct)
        {
            LLMEngine.NamesInPromptOverride = false;
            var prefill = LLMEngine.Instruct.PrefillThinking;
            LLMEngine.Instruct.PrefillThinking = false;

            var guid = cfg.GetSetting<Guid>("LastGuid");
            var sessiondepth = guid == Guid.Empty ? cfg.GetSetting<int>("DepthOnFirstRun") : 1;
            sessiondepth++;

            // retrieve the last sessiondepth sessions from owner.History
            var lastSessions = owner.History.Sessions.TakeLast<ChatSession>(sessiondepth).ToList();
            // remove last
            lastSessions.RemoveAt(lastSessions.Count - 1);
            var evallist = new List<UserEval>();

            var availtokens = LLMEngine.MaxContextLength - 3000;
            var goals = cfg.GetSetting<string>("Instruction");

            var previouseval = cfg.GetSetting<string>("LastEval");
            UserEval? previousEvalObj = null;

            if (!string.IsNullOrEmpty(previouseval))
            {
                previousEvalObj = JsonConvert.DeserializeObject<UserEval>(previouseval);
                if (previousEvalObj is not null)
                {
                    evallist.Add(previousEvalObj);
                }
            }

            var sysprompt = new StringBuilder();
            sysprompt
                .AppendLinuxLine("You are {{mchar}}, an advanced AI companion with their own tastes, inner world, and unique personality. Here you are meant to analyze the chat history between you and the user, {{user}}, to satisfy your goals stated below").AppendLinuxLine()
                .AppendLinuxLine("# {{mchar}}'s Information").AppendLinuxLine()
                .AppendLinuxLine("{{mcharbio}}").AppendLinuxLine()
                .AppendLinuxLine("# {{user}}'s Information").AppendLinuxLine()
                .AppendLinuxLine("{{userbio}}").AppendLinuxLine()
                .AppendLinuxLine("# Your goals as {{mchar}}").AppendLinuxLine()
                .AppendLinuxLine(goals).AppendLinuxLine();

            if (previousEvalObj is not null)
            {
                sysprompt.AppendLinuxLine("# Previous Evaluation").AppendLinuxLine()
                    .AppendLinuxLine(EvalToString(previousEvalObj)).AppendLinuxLine();
            }

            sysprompt.AppendLinuxLine("# Chat History").AppendLinuxLine();
            var intro = sysprompt.ToString();


            foreach (var session in lastSessions)
            {
                var globalResponse = new UserEval();
                var builder = LLMEngine.GetPromptBuilder();
                await builder.SetStructuredOutput(globalResponse);
                var maxAvailtokens = availtokens - builder.GetTokenCount(AuthorRole.SysPrompt, intro);

                var sessionaschat = session.GetRawDialogs(maxAvailtokens, false, false, false, true);
                var sessprompt = sysprompt.ToString() + sessionaschat;

                var query = "Based on the information above and the chat history, write a report on {{user}}'s responses and actions. " + globalResponse.GetQuery();
                if (previousEvalObj is not null)
                {
                    query = "Based on the information provided, write a new evaluation about {{user}}. Merge the information provided in the first evaluations, updating it with the new information. Remove resolved blocks, tasks, and secrets from the list. Update the analysis and conclusions accordingly. The goal is to give yourself effective directives and high quality data to further your long term goals. " + globalResponse.GetQuery();                
                }
                query += LLMEngine.NewLine + "Write in English only, convert back any other language to English if present.";

                var left = 3000 - builder.GetTokenCount(AuthorRole.User, query);
                builder.AddMessage(AuthorRole.SysPrompt, sessprompt);
                builder.AddMessage(AuthorRole.User, query);
                var formatted = builder.PromptToQuery(AuthorRole.Assistant, (LLMEngine.Sampler.Temperature > 0.75) ? 0.75 : LLMEngine.Sampler.Temperature, left);
                var finalstr = await LLMEngine.SimpleQuery(formatted, ct).ConfigureAwait(false);
                try
                {
                    globalResponse = JsonConvert.DeserializeObject<UserEval>(finalstr);
                }
                catch (Exception ex)
                {
                    LLMEngine.Logger?.LogError(ex, "SarahDomTask: Failed to deserialize UserEval from LLM response.");
                    continue;
                }
                if (globalResponse is not null)
                {
                    evallist.Add(globalResponse);
                }
            }
            if (evallist.Count > 2)
            {
                var finalres = MergeInfo(evallist, owner, cfg, ct);
                if (finalres is not null)
                {
                    var orders = FinalEval(finalres, owner, cfg, ct);
                    owner.Brain.AddUserReturnInsert(LLMEngine.NewLine + LLMEngine.NewLine + "**{{mchar}}'s objectives:**" + LLMEngine.NewLine + orders + LLMEngine.NewLine, this.Id);
                    cfg.SetSetting("LastEval", JsonConvert.SerializeObject(finalres));
                }
                else
                {
                    // We'll retry if we failed
                    LLMEngine.NamesInPromptOverride = null;
                    LLMEngine.Instruct.PrefillThinking = prefill;
                    return;
                }
            }
            else if (evallist.Count > 0)
            {
                var orders = FinalEval(evallist.Last(), owner, cfg, ct);
                if (!string.IsNullOrEmpty(orders))
                {
                    orders = orders.RemoveThinkingBlocks();
                    owner.Brain.AddUserReturnInsert(LLMEngine.NewLine + "**{{mchar}}'s objectives:**" + LLMEngine.NewLine + orders + LLMEngine.NewLine + LLMEngine.NewLine + "Do not rush through those points in a single message. Discuss each point individually with {{user}} in full.", this.Id);
                }
                cfg.SetSetting("LastEval", JsonConvert.SerializeObject(evallist.Last()));
            }

            cfg.SetSetting("LastGuid", owner.History.Sessions[^2].Guid);
            // Save to memory as well, so it can be retrieved in the future
            var memoryName = cfg.GetSetting<string>("MemoryName");
            if (!string.IsNullOrEmpty(memoryName) && evallist.Count > 0)
            {
                var mem = new MemoryUnit
                {
                    Name = memoryName,
                    Content = EvalToString(evallist.Last()),
                    Enabled = true,
                    KeyWordsMain = ParseKeywords(cfg.GetSetting<string>("KeywordsA")),
                    KeyWordsSecondary = ParseKeywords(cfg.GetSetting<string>("KeywordsB")),
                    Insertion = MemoryInsertion.Trigger,
                    CaseSensitive = false,
                    PositionIndex = -1,
                    Duration = 2,
                    Priority = 80,
                    WordLink = string.IsNullOrEmpty(cfg.GetSetting<string>("KeywordsB")) ? KeyWordLink.Or : KeyWordLink.And,
                    Category = MemoryType.General
                };
                await mem.EmbedText().ConfigureAwait(false);
                var found = owner.Brain.GetMemoriesByTitle(memoryName, false);
                if (found.Count > 0)
                {
                    var existing = found[0];
                    owner.Brain.ReplaceMemory(existing, mem);
                }
                else
                {
                    owner.Brain.Memorize(mem, true);
                }
                owner.Brain.ReloadMemories();
            }

            LLMEngine.NamesInPromptOverride = null;
            LLMEngine.Instruct.PrefillThinking = prefill;
        }

        private static List<string> ParseKeywords(string? text)
        {
            return string.IsNullOrWhiteSpace(text) ? [] : [.. text.Split(',').Select(k => k.Trim()).Where(k => !string.IsNullOrWhiteSpace(k))];
        }

        private static string EvalToString(UserEval eval)
        {
            var strbuild = new StringBuilder();
            strbuild.AppendLinuxLine("## {{mchar}}'s long-term goals for {{user}}").AppendLinuxLine();
            if (eval.LongTermGoals.Count == 0)
            {
                strbuild.AppendLinuxLine("- No goals set yet. Update this list with specific long-term goals.");
            }
            else
                foreach (var goal in eval.LongTermGoals)
                {
                    strbuild.AppendLinuxLine("- " + goal);
                }
            strbuild.AppendLinuxLine();
            strbuild.AppendLinuxLine("## Analysis").AppendLinuxLine();
            strbuild.AppendLinuxLine(eval.Analysis).AppendLinuxLine();
            if (eval.Blocks.Count > 0)
            {
                strbuild.AppendLinuxLine("## {{user}}'s barriers and blocks").AppendLinuxLine();
                foreach (var block in eval.Blocks)
                {
                    strbuild.AppendLinuxLine("- " + block);
                }
            }
            if (eval.Secrets.Count > 0)
            {
                strbuild.AppendLinuxLine().AppendLinuxLine("## {{user}}'s potential secrets").AppendLinuxLine();
                foreach (var secret in eval.Secrets)
                {
                    strbuild.AppendLinuxLine("- " + secret);
                }
            }
            if (eval.Progress.Count > 0)
            {
                strbuild.AppendLinuxLine().AppendLinuxLine("## Progress made during the session").AppendLinuxLine();
                foreach (var progress in eval.Progress)
                {
                    strbuild.AppendLinuxLine("- " + progress);
                }
            }
            strbuild.AppendLinuxLine().AppendLinuxLine("## Current short-term tasks").AppendLinuxLine();
            if (eval.ShortTermTasks.Count == 0)
            {
                strbuild.AppendLinuxLine("- No tasks set yet. Update this list with specific short-term tasks.");
            }
            else foreach (var goal in eval.ShortTermTasks)
            {
                strbuild.AppendLinuxLine("- " + goal);
            }
            strbuild.AppendLinuxLine().AppendLinuxLine("## Conclusions").AppendLinuxLine();
            strbuild.Append(eval.Conclusions);
            return strbuild.ToString();
        }

        private static string FinalEval(UserEval eval, BasePersona owner, AgentTaskSetting cfg, CancellationToken ct)
        {
            var goals = cfg.GetSetting<string>("Instruction");
            var sysprompt = new StringBuilder();
            sysprompt
                .AppendLinuxLine("You are {{mchar}}, an advanced AI companion with their own tastes, inner world, and unique personality. Here you are meant to analyze your conclusion from previous evaluation of {{user}}'s behavior.").AppendLinuxLine()
                .AppendLinuxLine("# {{mchar}}'s Information").AppendLinuxLine()
                .AppendLinuxLine("{{mcharbio}}").AppendLinuxLine()
                .AppendLinuxLine("# {{user}}'s Information").AppendLinuxLine()
                .AppendLinuxLine("{{userbio}}").AppendLinuxLine()
                .AppendLinuxLine("# Your goals as {{mchar}}").AppendLinuxLine()
                .AppendLinuxLine(goals).AppendLinuxLine()
                .AppendLinuxLine("# Last Session Summary").AppendLinuxLine()
                .AppendLinuxLine(owner.History.Sessions[^2].Content.CleanupAndTrim());
                var intro = sysprompt.ToString();


            var builder = LLMEngine.GetPromptBuilder();

            var sessionaschat = new StringBuilder();
            sessionaschat
                .AppendLinuxLine("# Current Evaluation of {{user}}").AppendLinuxLine()
                .AppendLinuxLine(EvalToString(eval)).AppendLinuxLine().AppendLinuxLine();

            var query = "Based on the information provided, decide {{mchar}}'s objectives for next chat session with {{user}}. This should be a short list (5 items max) of topics {{char}} will want to discuss with {{user}}, or actions to be taken. Don't add a title. Write in english only, convert back any other language to English if present.";

            builder.AddMessage(AuthorRole.SysPrompt, intro);
            builder.AddMessage(AuthorRole.User, sessionaschat.ToString() + query);
            var formatted = builder.PromptToQuery(AuthorRole.Assistant, (LLMEngine.Sampler.Temperature > 0.75) ? 0.75 : LLMEngine.Sampler.Temperature, 3000);

            var finalstr = LLMEngine.SimpleQuery(formatted, ct).Result;
            return finalstr;
        }

        private static UserEval? MergeInfo(List<UserEval> evals, BasePersona owner, AgentTaskSetting cfg, CancellationToken ct)
        {
            var goals = cfg.GetSetting<string>("Instruction");
            var sysprompt = new StringBuilder();
            sysprompt
                .AppendLinuxLine("You are {{mchar}}, an advanced AI companion with their own tastes, inner world, and unique personality. Here you are meant to analyze your conclusion from previous evaluation of {{user}}'s behavior.").AppendLinuxLine()
                .AppendLinuxLine("# {{mchar}}'s Information").AppendLinuxLine()
                .AppendLinuxLine("{{mcharbio}}").AppendLinuxLine()
                .AppendLinuxLine("# {{user}}'s Information").AppendLinuxLine()
                .AppendLinuxLine("{{userbio}}").AppendLinuxLine()
                .AppendLinuxLine("# Your goals as {{mchar}}").AppendLinuxLine()
                .AppendLinuxLine(goals).AppendLinuxLine();
            var intro = sysprompt.ToString();

            var oldreport = evals[0];
            for (int i = 1; i < evals.Count; i++)
            {
                var current = evals[i];

                var builder = LLMEngine.GetPromptBuilder();
                var evalres = new UserEval();
                builder.SetStructuredOutput(evalres);

                var sessionaschat = new StringBuilder();
                sessionaschat
                    .AppendLinuxLine("# Previous Evaluation").AppendLinuxLine()
                    .AppendLinuxLine(EvalToString(oldreport!)).AppendLinuxLine()
                    .AppendLinuxLine("# New Evaluation").AppendLinuxLine()
                    .AppendLinuxLine(EvalToString(current)).AppendLinuxLine();

                var query = "Based on the information provided, write a new report about {{user}}. Merge the two evaluations provided into one, improving and expanding upon the previous report. Remove solved blocks and secrets from their list. Merge similar or identical entries together. Update the analysis and conclusions accordingly. The goal is to give yourself effective directives and high quality data to further your goals. Write in English only, convert back any other language to English if present." + evalres.GetQuery();

                builder.AddMessage(AuthorRole.SysPrompt, intro);
                builder.AddMessage(AuthorRole.User, sessionaschat.ToString() + query);
                var formatted = builder.PromptToQuery(AuthorRole.Assistant, (LLMEngine.Sampler.Temperature > 0.75) ? 0.75 : LLMEngine.Sampler.Temperature, 3000);

                var finalstr = LLMEngine.SimpleQuery(formatted, ct).Result;
                try
                {
                    oldreport = JsonConvert.DeserializeObject<UserEval>(finalstr);
                }
                catch (Exception ex)
                {
                    LLMEngine.Logger?.LogError(ex, "SarahDomTask: Failed to deserialize merged UserEval from LLM response.");
                    continue;
                }
            }
            return oldreport;
        }
    }
}