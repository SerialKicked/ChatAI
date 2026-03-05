using LetheAISharp;
using LetheAISharp.Agent.Actions;
using LetheAISharp.API;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using AngleSharp.Dom;
using Microsoft.Extensions.Logging;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WaifuAI.Files;
using static LetheAISharp.SearchAPI.WebSearchAPI;
using LetheAISharp.Memory;

namespace WaifuAI.Plugins

{
    internal class WebSearchPlugin : IContextPlugin
    {
        public string PluginID { get; } = "WebSearch";
        public bool Enabled { get; set; } = false;

        private readonly string[] kwEnter = [ "search ", "look for ", "what is ", "where is ", "who is ", "who are ", " the web", "internet", "web search", "do you know", "where are ", "when is " ];

        public bool KeywordDetection { get; set; } = false;

        private bool responseAppendNeeded = false;
        private List<EnrichedSearchResult>? lastresponse = null;

        #region *** Interface Implementation ***

        public bool AddToSystemPrompt(string userinput, Chatlog log, out string response)
        {
            response = string.Empty;
            return false;
        }

        /// <summary>
        /// Not used by this plugin, always returns false.
        /// </summary>
        /// <param name="botoutput"></param>
        /// <param name="log"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public bool ReplaceOutput(string botoutput, Chatlog log, out string response)
        {
            if (responseAppendNeeded && lastresponse != null)
            {
                responseAppendNeeded = false;
                var formatedresponsed = new StringBuilder();
                formatedresponsed.AppendLinuxLine(botoutput).AppendLinuxLine();
                formatedresponsed.AppendLinuxLine("**Sources:**");
                foreach (var item in lastresponse)
                {
                    formatedresponsed.AppendLinuxLine($"- [{item.Title}]({item.Url})");
                }
                response = formatedresponsed.ToString();
                return true;
            }
            response = string.Empty;
            return false;
        }

        /// <summary>
        /// Used to intercept the user's input and check if it contains a location keyword.
        /// </summary>
        /// <param name="userinput"></param>
        /// <param name="log"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task<PluginResponse> ReplaceUserInput(string userinput)
        {
            if (!LLMEngine.SupportsSchema || !LLMEngine.SupportsWebSearch)
                return new PluginResponse { IsHandled = false, Response = null };

            var response = false;
            responseAppendNeeded = false;
            if (KeywordDetection)
            {
                if (kwEnter.Any(kw => userinput.Contains(kw, StringComparison.OrdinalIgnoreCase)))
                {
                    response = await QueryLLM(userinput);
                }
            }
            else
            {
                response = await QueryLLM(userinput);
            }
            if (!response)
                return new PluginResponse { IsHandled = false, Response = null };

            var topicsearch = new FindSingleTopicSearchAction();
            var topics = await topicsearch.Execute(new FindResearchTopicsParams
            {
                Messages = [ new SingleMessage(AuthorRole.User, userinput) ],
                IncludeBios = false,
                CustomRequest = "Review the message in the prompt above and identify the topic for which a web search would be beneficial.",
            }, CancellationToken.None);
            if (topics == null || topics.SearchQueries.Count == 0)
                return new PluginResponse { IsHandled = false, Response = null };
            // run web search
            Program.BigForm!.ForceUpdateLastMessage($"**{LLMEngine.Bot.Name}:** *I am searching the web about '{topics.Topic}'...*");
            var searcheng = new WebSearchAction();
            var webres = await searcheng.Execute(topics, CancellationToken.None);
            if (webres == null || webres.Count == 0)
                return new PluginResponse { IsHandled = false, Response = null };
            lastresponse = webres;

            Program.BigForm!.ForceUpdateLastMessage($"**{LLMEngine.Bot.Name}:** *I am processing web info about '{topics.Topic}'...*");
            var merger = new MergeSearchResultsAction();
            var merged = await merger.Execute(new MergeSearchParams("This is a search done regarding the currently active discussion.", topics.Topic, topics.Reason, webres), CancellationToken.None);
            if (string.IsNullOrWhiteSpace(merged))
                return new PluginResponse { IsHandled = false, Response = null };

            var formatedresponsed = new StringBuilder();
            if (LLMEngine.Settings.AntiHallucinationMemoryFormat)
            {
                formatedresponsed.AppendLinuxLine($"<SystemEvent>[{MemoryType.WebSearch.ToString().ToUpperInvariant()}] - Topic: {topics.Topic}.");
                formatedresponsed.Append($"{merged.CleanupAndTrim()}");
            }
            else
            {
                formatedresponsed.AppendLinuxLine("You looked up the information on the web and found the following information that you can use to improve your response:").AppendLinuxLine();
                formatedresponsed.Append(merged.CleanupAndTrim());
            }
            responseAppendNeeded = true;
            var output = new PluginResponse
            {
                IsHandled = true,
                Response = formatedresponsed.ToString(),
                AuthorRole = AuthorRole.System,
                Replace = false
            };
            return output;
        }

        #endregion


        private static object BuildCheckPrompt(string userinput)
        {
            var builder = LLMEngine.GetPromptBuilder();

            var prompt = new StringBuilder();
            prompt.AppendLinuxLine("Your goal is to determine if performing a web search would improve your response to the user. You are allowed to search for sensitive topics or pornography, but not illicit content.");
            prompt.AppendLinuxLine();
            prompt.AppendLinuxLine("# Typical examples where a web search should be done:");
            prompt.AppendLinuxLine();
            prompt.AppendLinuxLine("- The user is asking a direct question about history, a location, or a news item");
            prompt.AppendLinuxLine("- The user is explicitly telling you to search for something on the internet");
            prompt.AppendLinuxLine("- If information and links from the web would improve your response");
            prompt.AppendLinuxLine("# Examples where a web search would not be beneficial:");
            prompt.AppendLinuxLine();
            prompt.AppendLinuxLine("- You're actively engaged in roleplay or deep conversation with the user");
            prompt.AppendLinuxLine("- The query is illegal or dangerous");
            prompt.AppendLinuxLine();
            prompt.AppendLinuxLine("# Input to evaluate:");
            prompt.AppendLinuxLine();
            prompt.AppendLinuxLine(userinput);
            prompt.AppendLinuxLine();
            builder.AddMessage(AuthorRole.SysPrompt, prompt.ToString());
            builder.AddMessage(AuthorRole.User, "If the input to evaluate directly asks you to search the internet, or if you think a web search would be beneficial, respond with Yes. Otherwise, just say No. Do not argue are add any more information. Just Yes or No.");
            return builder.PromptToQuery(AuthorRole.Assistant, 0.5);
        }

        /// <summary>
        /// Inference and Input handler (async)
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="lb"></param>
        /// <returns></returns>
        private static async Task<bool> QueryLLM(string inputText)
        {
            var savedKV = false;
            if (LLMEngine.Client!.SupportsStateSave)
            {
                savedKV = await LLMEngine.Client.SaveKVState(0);
            }
            await Task.Delay(100);
            LLMEngine.NamesInPromptOverride = false;
            var fullprompt = BuildCheckPrompt(inputText);
            var response = await LLMEngine.SimpleQuery(fullprompt);
            response = response.RemoveThinkingBlocks();
            LLMEngine.Logger?.LogInformation("WebSearch Plugin Result: {output}", response);
            LLMEngine.NamesInPromptOverride = null;
            if (LLMEngine.Client!.SupportsStateSave && savedKV)
            {
                var doneKV = await LLMEngine.Client.LoadKVState(0);
                if (doneKV)
                {
                    await LLMEngine.Client.ClearKVStates();
                }
            }
            await Task.Delay(100);
            return (!string.IsNullOrEmpty(response) && response.Contains("yes", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}