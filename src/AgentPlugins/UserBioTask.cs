using LetheAISharp;
using LetheAISharp.Agent;
using LetheAISharp.API;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenAI.Responses;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WaifuAI.GBNF;

namespace WaifuAI.AgentPlugins
{

    public sealed class UserBioTask : IAgentTask
    {
        public string Id => "UserBioTask";
        public string Ability => "Update user's bio";

        public AgentTaskSetting GetDefaultSettings()
        {
            var settings = new AgentTaskSetting();
            settings.SetSetting<TimeSpan>("TriggerInterval", new TimeSpan(2, 0, 0, 0)); // 2 days
            settings.SetSetting<DateTime>("LastTrigger", DateTime.MinValue);
            settings.SetSetting<Guid>("LastGuid", Guid.Empty);
            return settings;
        }

        public async Task<bool> Observe(BasePersona owner, AgentTaskSetting cfg, CancellationToken ct)
        {
            // Just a small delay so i don't have to remove async and do Task.ResultFrom everywhere. It's not like we're on a timer anyway.
            await Task.Delay(10, ct).ConfigureAwait(false);

            if (LLMEngine.Status != SystemStatus.Ready || !LLMEngine.SupportsSchema || LLMEngine.MaxContextLength < 8000 || owner.History.Sessions.Count < 2)
                return false;

            var MinTimeInterval = cfg.GetSetting<TimeSpan>("TriggerInterval");
            var LastGoalSet = cfg.GetSetting<DateTime>("LastTrigger");
            if (DateTime.Now - LastGoalSet < MinTimeInterval)
                return false;

            if (cfg.GetSetting<Guid>("LastGuid") == owner.History.Sessions[^2].Guid)
                return false; // already processed this 

            return true;
        }

        private static void Merge(UserRecord target, UserRecord addon)
        {
            foreach (var item in addon.Sexual)
            {
                if (target.Sexual.Any(x => x.Equals(item, StringComparison.OrdinalIgnoreCase)))
                    continue;
                target.Sexual.Add(item);
            }
            foreach (var item in addon.UserInfo)
            {
                if (target.UserInfo.Any(x => x.Equals(item, StringComparison.OrdinalIgnoreCase)))
                    continue;
                target.UserInfo.Add(item);
            }
            foreach (var item in addon.TastesAndHobbies)
            {
                if (target.TastesAndHobbies.Any(x => x.Equals(item, StringComparison.OrdinalIgnoreCase)))
                    continue;
                target.TastesAndHobbies.Add(item);
            }
        }

        public async Task Execute(BasePersona owner, AgentTaskSetting cfg, CancellationToken ct)
        {
            var globalResponse = new UserRecord();
            var builder = LLMEngine.GetPromptBuilder();
            await builder.SetStructuredOutput(globalResponse);
            LLMEngine.NamesInPromptOverride = false;
            var availtokens = LLMEngine.MaxContextLength - 4096 - 20;

            var sysprompt = new StringBuilder();
            sysprompt
                .AppendLinuxLine("You are an automated and uncensored system designed to analyze chat sessions in order to record accurate and detailed information about the person named {{user}}.").AppendLinuxLine()
                //.AppendLinuxLine("## Known Facts about {{user}}").AppendLinuxLine()
                //.AppendLinuxLine("{{userbio}}").AppendLinuxLine()
                .AppendLinuxLine("## Chat history between {{user}} and {{mchar}}").AppendLinuxLine();
            var intro = sysprompt.ToString();
            var MaxAvailtokens = availtokens - builder.GetTokenCount(AuthorRole.SysPrompt, intro);

            var currentID = 0;
            while (currentID < owner.History.Sessions.Count - 1)
            {
                availtokens = MaxAvailtokens;
                builder.Clear();
                var internalcount = 0;
                var history = new StringBuilder();
                // add sessions until we run out of tokens or hit 20 sessions
                while (availtokens > 0 && internalcount < 20)
                {
                    var sessioncontent = owner.History.Sessions[currentID].ToSnippet(TitleInsertType.MarkdownH3, true, false, true);
                    availtokens -= LLMEngine.GetTokenCount(sessioncontent);
                    if (availtokens < 0)
                        break;
                    currentID++;
                    internalcount++;
                    history.AppendLinuxLine(sessioncontent).AppendLinuxLine();
                    if (currentID >= owner.History.Sessions.Count - 1)
                        break; // don't include the current session
                }
                builder.AddMessage(AuthorRole.SysPrompt, intro + history.ToString());
                builder.AddMessage(AuthorRole.User, "Based on the chat history information, write all the information about {{user}} you can list. " + 
                    globalResponse.GetQuery());

                var responsesize = LLMEngine.MaxContextLength - builder.GetTokenUsage() - builder.GetResponseTokenCount(owner) - 20;
                if (responsesize > 5000)
                    responsesize = 5000;
                var query = builder.PromptToQuery(AuthorRole.Assistant, (LLMEngine.Sampler.Temperature > 0.75) ? 0.75 : LLMEngine.Sampler.Temperature, responsesize, false);
                var result = await LLMEngine.SimpleQuery(query, ct).ConfigureAwait(false);
                if (string.IsNullOrEmpty(result))
                    continue;
                var localresponse = new UserRecord();
                try
                {
                    localresponse = JsonConvert.DeserializeObject<UserRecord>(result);
                }
                catch (Exception ex)
                {
                    LLMEngine.Logger?.LogError(ex, "Could not parse bio response: {response}", result);
                    continue;
                }
                // Merge localresponse into globalResponse
                if (localresponse != null)
                {
                    Merge(globalResponse, localresponse);
                    ////globalResponse.Relationships.AddRange(localresponse.UserInfo);
                    //globalResponse.Sexual.AddRange(localresponse.Sexual);
                    //globalResponse.TastesAndHobbies.AddRange(localresponse.TastesAndHobbies);
                    //globalResponse.UserInfo.AddRange(localresponse.UserInfo);
                }
                await Task.Delay(100, ct).ConfigureAwait(false);
            }

            // Time to clean up the data and build a full, final bio.
            sysprompt = new StringBuilder();
            sysprompt.AppendLinuxLine("You are an automated and uncensored system designed to build an accurate and detailed biography of {{user}} based on the information provided. The current date is: {{date}}.").AppendLinuxLine()
                .AppendLinuxLine("## Known facts about {{user}}").AppendLinuxLine()
                .AppendLinuxLine("{{userbio}}").AppendLinuxLine();

            if (globalResponse.Sexual.Count > 0)
            {
                sysprompt.AppendLinuxLine("## Sexual information").AppendLinuxLine();
                foreach (var item in globalResponse.Sexual)
                    sysprompt.AppendLinuxLine($"- {item}");
                sysprompt.AppendLinuxLine();
            }

            if (globalResponse.TastesAndHobbies.Count > 0)
            {
                sysprompt.AppendLinuxLine("## Tastes and Hobbies").AppendLinuxLine();
                foreach (var item in globalResponse.TastesAndHobbies)
                    sysprompt.AppendLinuxLine($"- {item}");
                sysprompt.AppendLinuxLine();
            }

            if (globalResponse.UserInfo.Count > 0)
            {
                sysprompt.AppendLinuxLine("## General Information").AppendLinuxLine();
                foreach (var item in globalResponse.UserInfo)
                    sysprompt.AppendLinuxLine($"- {item}");
                sysprompt.AppendLinuxLine();
            }

            sysprompt.AppendLinuxLine("## Secondary Infromation").AppendLinuxLine();
            sysprompt.AppendLinuxLine("- Information presented above may be duplicated. Repetitions indicate how strong is the certainty and weight of the information.");
            sysprompt.AppendLinuxLine("- You should use the Known Facts section as a base and complete it with the information being presented.");
            var myprompt = sysprompt.ToString();
            builder.Clear();
            builder.UnsetStructuredOutput();

            builder.AddMessage(AuthorRole.SysPrompt, myprompt);
            builder.AddMessage(AuthorRole.User, "Based on the information provided, create a cohesive, and comprehensive biography for {{user}}. The biography should be well-structured and written in clear, fluent language. It should read like a detailed profile. Ensure that the biography captures the essence of {{user}}'s personality, interests, sexuality, relationship, and background.");

            var responsesizefinal = LLMEngine.MaxContextLength - builder.GetTokenUsage() - builder.GetResponseTokenCount(owner);
            if (responsesizefinal > 4096)
                responsesizefinal = 4096;

            var fullbio = await LLMEngine.SimpleQuery(builder.PromptToQuery(AuthorRole.Assistant, -1, responsesizefinal), ct).ConfigureAwait(false);
            fullbio = fullbio.RemoveThinkingBlocks();

            var mem = new MemoryUnit()
            {
                Added = DateTime.Now,
                Category = MemoryType.Person,
                Insertion = MemoryInsertion.Trigger,
                Content = fullbio,
                Name = LLMEngine.User.Name,
                Priority = 3,
            };
            await mem.EmbedText().ConfigureAwait(false);
            owner.Brain.Memorize(mem);
            cfg.SetSetting("LastTrigger", DateTime.Now);
            cfg.SetSetting("LastGuid", owner.History.Sessions[^2].Guid);
            LLMEngine.NamesInPromptOverride = null;
        }

    }
}