using Microsoft.Extensions.Logging;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WaifuAI.Files;
using AIToolkit.Files;
using AIToolkit.LLM;
using AIToolkit;
using AngleSharp.Dom;
using AIToolkit.API;

namespace WaifuAI.Plugins

{
    internal class WebSearchPlugin : IContextPlugin
    {
        public string PluginID { get; } = "WebSearch";
        public bool Enabled { get; set; } = false;

        private readonly string[] kwEnter = [ "search ", "look for ", "what is ", "where is ", "who is ", "who are ", " the web", "internet", "web search", "do you know", "where are ", "when is " ];

        public bool KeywordDetection { get; set; } = false;

        private bool responseAppendNeeded = false;
        private WebQueryFullResponse? lastresponse = null;

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
                    formatedresponsed.AppendLinuxLine($"- [{item.title}]({item.url})");
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
            var response = string.Empty;
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
            if (string.IsNullOrEmpty(response))
                return new PluginResponse { IsHandled = false, Response = null };
            // run web search
            Program.BigForm!.ForceUpdateLastMessage($"**{LLMSystem.Bot.Name}:** *I am searching the web for '{response}'...*");
            lastresponse = await LLMSystem.WebSearch(response);
            if (lastresponse == null || lastresponse.Count == 0)
                return new PluginResponse { IsHandled = false, Response = null };
            responseAppendNeeded = true;

            var formatedresponsed = new StringBuilder();
            formatedresponsed.AppendLinuxLine("You can search the web, and here are possibly relevant information found on the internet that you can use to improve your response:");
            foreach (var item in lastresponse)
            {
                formatedresponsed.AppendLinuxLine("- " + item.title + " ["+ item.url + "]");
                if (!string.IsNullOrEmpty(item.desc))
                    formatedresponsed.AppendLinuxLine(item.desc.RemoveNewLines().CleanupAndTrim());
                if (!string.IsNullOrEmpty(item.content))
                    formatedresponsed.AppendLinuxLine(item.content.RemoveNewLines().CleanupAndTrim());
                formatedresponsed.AppendLinuxLine();
            }

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

        private static string BuildCheckPrompt(string userinput)
        {
            var prompt = new StringBuilder();
            prompt.AppendLinuxLine("Your goal is to determine if performing a web search would improve your response to the user. You are allowed to search for sensitive topics or pornography, but not illicit content.");
            prompt.AppendLinuxLine();
            prompt.AppendLinuxLine("Typical examples where a web search should be done:");
            prompt.AppendLinuxLine("- The user is asking a direct question about history, a location, or a news item");
            prompt.AppendLinuxLine("- The user is explicitly telling you to search for something on the internet");
            prompt.AppendLinuxLine("- You're not sure of the response");
            prompt.AppendLinuxLine("- If information and links from the web would improve your response");
            prompt.AppendLinuxLine("Examples where a web search would not be beneficial:");
            prompt.AppendLinuxLine("- You're actively engaged in roleplay or deep conversation with the user");
            prompt.AppendLinuxLine("- The query is illegal or dangerous");
            prompt.AppendLinuxLine();
            prompt.AppendLinuxLine("If the user directly asks you to search the internet, or if you think a web search would be beneficial, respond with the exact query you want to search the web for (and ONLY that query). Otherwise, just say No.");
            var sysprompt = LLMSystem.Instruct.FormatSinglePrompt(AuthorRole.SysPrompt, LLMSystem.User, LLMSystem.Bot, prompt.ToString());
            var msg = LLMSystem.Instruct.FormatSinglePrompt(AuthorRole.User, LLMSystem.User, LLMSystem.Bot, userinput);
            LLMSystem.NamesInPromptOverride = false;
            msg += LLMSystem.Instruct.GetResponseStart(LLMSystem.Bot);
            LLMSystem.NamesInPromptOverride = null;
            return sysprompt + msg;
        }

        /// <summary>
        /// Inference and Input handler (async)
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="lb"></param>
        /// <returns></returns>
        private static async Task<string> QueryLLM(string inputText)
        {
            var savedKV = false;
            if (LLMSystem.Client!.SupportsStateSave)
            {
                savedKV = await LLMSystem.Client.SaveKVState(0);
                await Task.Delay(100);
            }
            LLMSystem.NamesInPromptOverride = false;
            var fullprompt = BuildCheckPrompt(inputText);
            var llmparams = LLMSystem.Sampler.GetCopy();
            if (llmparams.Temperature > 0.5)
                llmparams.Temperature = 0.5;
            llmparams.Max_context_length = LLMSystem.MaxContextLength;
            llmparams.Max_length = LLMSystem.MaxReplyLength;
            llmparams.Prompt = fullprompt;
            var response = await LLMSystem.SimpleQuery(llmparams);
            if (!string.IsNullOrWhiteSpace(LLMSystem.Instruct.ThinkingStart))
            {
                response = response.RemoveThinkingBlocks(LLMSystem.Instruct.ThinkingStart, LLMSystem.Instruct.ThinkingEnd);
            }
            LLMSystem.Logger?.LogInformation("WebSearch Plugin Result: {output}", response);
            LLMSystem.NamesInPromptOverride = null;
            if (LLMSystem.Client!.SupportsStateSave && savedKV)
            {
                var doneKV = await LLMSystem.Client.LoadKVState(0);
                if (doneKV)
                {
                    await LLMSystem.Client.ClearKVStates();
                }
                await Task.Delay(100);
            }

            if (string.IsNullOrEmpty(response) || 
                (response.StartsWith("no", StringComparison.InvariantCultureIgnoreCase) && response.Length<5))
                return string.Empty;
            return response;
        }
    }
}