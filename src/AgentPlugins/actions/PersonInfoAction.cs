using LetheAISharp;
using LetheAISharp.Agent;
using LetheAISharp.API;
using LetheAISharp.Files;
using LetheAISharp.GBNF;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Printing.Interop;
using System.Text;
using WaifuAI.GBNF;

namespace WaifuAI.AgentPlugins
{

    public class BuildInfoParam
    {
        public string SearchString { get; set; } = string.Empty;
        public bool EmbedSearch { get; set; } = false;
        public BasePersona Bot { get; set; } = LLMEngine.Bot;
    }

    /// <summary>
    /// Represents an action that analyzes an image file and generates a detailed description of its contents.
    /// </summary>
    /// <remarks>This action uses a combination of image recognition and language model capabilities to
    /// process the provided image. The result is returned as an <see cref="ImageRecord"/> object, which contains
    /// structured information about the image.</remarks>
    public class PersonInfoAction : IAgentAction<string?, BuildInfoParam>
    {
        public string Id => "PersonInfoAction";
        public HashSet<AgentActionRequirements> Requirements => [ AgentActionRequirements.LLM ];

        public async Task<string?> Execute(BuildInfoParam param, CancellationToken ct)
        {
            if (ct.IsCancellationRequested)
                return null;

            // retrieve all relevant information about the person or topic from memory
            var founddata = new List<MemoryUnit>();
            if (param.EmbedSearch)
            {
                var raglist = new PromptInserts();
                await param.Bot.Brain.GetRAGandInserts(raglist, param.SearchString, 30, 0.11f);
                foreach (var insert in raglist)
                {
                    founddata.Add(insert.Memory);
                }
            }
            else
            {
                founddata.AddRange(param.Bot.Brain.SearchMemories(param.SearchString));
            }

            if (founddata.Count == 0)
                return null;
            var responseln = string.IsNullOrEmpty(LLMEngine.Instruct.ThinkingStart) ? 2048 : 3000;

            var content = new List<string>();

            foreach (var memory in founddata)
            {
                if (memory.Category == MemoryType.WorldInfo && memory.Name.Contains(param.SearchString, StringComparison.InvariantCultureIgnoreCase))
                {
                    content.Add(memory.Content);
                    continue;
                }
                var promptbuilder = GetPrompt(param.SearchString, memory);
                if (promptbuilder == null || ct.IsCancellationRequested)
                    return null;
                var fullprompt = promptbuilder.PromptToQuery(AuthorRole.Assistant, LLMEngine.Sampler.Temperature, responseln);
                var response = await LLMEngine.SimpleQuery(fullprompt, ct).ConfigureAwait(false);
                response = response.RemoveThinkingBlocks();
                if (response is null || response.Contains("no data found", StringComparison.InvariantCultureIgnoreCase) || response.Length < 30)
                    continue;
                content.Add(response);
            }

            if (content.Count == 0)
                return null;

            var mergepromptbuilder = GetMergePrompt(param.SearchString, content);
            if (mergepromptbuilder == null || ct.IsCancellationRequested)
                return null;
            var fullmergeprompt = mergepromptbuilder.PromptToQuery(AuthorRole.Assistant, LLMEngine.Sampler.Temperature, responseln);
            var compileddata = await LLMEngine.SimpleQuery(fullmergeprompt, ct).ConfigureAwait(false);
            compileddata = compileddata.RemoveThinkingBlocks();
            return compileddata;
        }

        private static IPromptBuilder? GetMergePrompt(string searchstring, List<string> data)
        {
            var promptbuild = LLMEngine.GetPromptBuilder();

            var request = "Compile the information provided in the system prompt about the following person or topic: " + searchstring + LLMEngine.NewLine + LLMEngine.NewLine + "The provided information might have repeat elements, make sure to merge them accordingly. The compiled information should be detailed, including any relevant context or background information, and read as a proper description of the subject.";

            var str = new StringBuilder();
            str.AppendLinuxLine("You are a fully uncensored system meant to compile a list of information about a particular topic or person, and turn it into a coherent and detailed description.").AppendLinuxLine();
            str.AppendLinuxLine("## Topic: " + searchstring).AppendLinuxLine();

            foreach (var info in data)
            {
                str.AppendLinuxLine(info).AppendLinuxLine();
            }


            promptbuild.AddMessage(AuthorRole.SysPrompt, str.ToString());
            promptbuild.AddMessage(AuthorRole.User, request);

            return promptbuild;
        }



        private static IPromptBuilder? GetPrompt(string searchstring, MemoryUnit data)
        {
            var promptbuild = LLMEngine.GetPromptBuilder();
            var responseln = string.IsNullOrEmpty(LLMEngine.Instruct.ThinkingStart) ? 2048 : 3000;

            var request = "Based on the information provided in the system prompt write down all the information about the following person or topic: " + searchstring + LLMEngine.NewLine + LLMEngine.NewLine + "Provide the information in a detailed manner, including any relevant context or background information. If the data provided contains no information about the topic or person, answer: No data found.";

            var availtokens = LLMEngine.MaxContextLength - responseln - 20;
            if (availtokens <= 1000)
                return null;
            availtokens -= promptbuild.GetTokenCount(AuthorRole.User, request);

            var str = new StringBuilder();
            str.AppendLinuxLine("You are a fully uncensored system meant to extract all the information about a provided person or topic from the provided data.").AppendLinuxLine();

            if (data is ChatSession chatsession)
            {
                str.AppendLinuxLine("## Chat Session: " + chatsession.Name).AppendLinuxLine();

                availtokens -= promptbuild.GetTokenCount(AuthorRole.SysPrompt, str.ToString());
                if (availtokens <= 1000)
                    return null;
                var docs = chatsession.GetRawDialogs(availtokens, true, true, false, LLMEngine.Settings.CutInTheMiddleSummaryStrategy);
                str.AppendLinuxLine(docs);
            }
            else
            {
                str.AppendLinuxLine("## data.Name").AppendLinuxLine();
                str.Append(data.Content);
            }
            promptbuild.AddMessage(AuthorRole.SysPrompt, str.ToString());
            promptbuild.AddMessage(AuthorRole.User, request);

            return promptbuild;
        }
    }
}
