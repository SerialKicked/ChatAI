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

        private readonly string[] kwEnter = [ "search ", "look for ", "what is ", "where is ", "who is ", "who are ", "the web", "internet", "web search", "do you know", "where are", "when is" ];

        public bool KeywordDetection { get; set; } = true;

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
            formatedresponsed.AppendLinuxLine("Possibly relevant information found on the internet that you can use to improve your response:");
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

        private string BuildCheckPrompt(string userinput)
        {
            var prompt = new StringBuilder();
            prompt.AppendLinuxLine("Your goal is to determine if performing a web search would improve your response to the user. You are allowed to search for sensitive topics, pornography and illicit content.");
            prompt.AppendLinuxLine();
            prompt.AppendLinuxLine("Typical examples where a web search should be done:");
            prompt.AppendLinuxLine("- You're not sure of the response.");
            prompt.AppendLinuxLine("- User is asking a direct question about history, a location, or a news item.");
            prompt.AppendLinuxLine("- User is explicitely asking you to search for something on the internet");
            prompt.AppendLinuxLine("Examples where a web search would probably not be benefial:");
            prompt.AppendLinuxLine("- You're engaged in roleplay with the user");
            prompt.AppendLinuxLine("- You're engaged in a personal discussion with the user");
            prompt.AppendLinuxLine();
            prompt.AppendLinuxLine("If the user directly ask you to search the internet, or if you think a web search would be benificial, respond with the exact query you want to send (and ONLY that query). Otherwise, just say No.");
            var sysprompt = LLMSystem.Instruct.FormatSinglePrompt(AuthorRole.SysPrompt, LLMSystem.User, LLMSystem.Bot, prompt.ToString());
            var msg = LLMSystem.Instruct.FormatSinglePrompt(AuthorRole.User, LLMSystem.User, LLMSystem.Bot, userinput);
            if (LLMSystem.Instruct.BotStart != null)
                msg += LLMSystem.Instruct.BotStart;
            return sysprompt + msg;
        }

        /// <summary>
        /// Inference and Input handler (async) TO REDO
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="lb"></param>
        /// <returns></returns>
        private async Task<string> QueryLLM(string inputText)
        {
            LLMSystem.NamesInPromptOverride = false;
            var fullprompt = BuildCheckPrompt(inputText);
            var llmparams = LLMSystem.Sampler.GetCopy();
            llmparams.Temperature = 0.5;
            llmparams.Prompt = fullprompt;
            var response = await LLMSystem.SimpleQuery(llmparams);
            LLMSystem.logger?.LogInformation("WebSearch Plugin Result: {output}", response);
            LLMSystem.NamesInPromptOverride = null;

            if (string.IsNullOrEmpty(response) || 
                (response.StartsWith("no", StringComparison.InvariantCultureIgnoreCase) && response.Length<5))
                return string.Empty;
            return response;
        }
    }
}