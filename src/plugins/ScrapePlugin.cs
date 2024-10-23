using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.Logging;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WaifuAI.Files;
using WaifuAI.Memory;
using WaifuAI.Web;

namespace WaifuAI.Plugins

{
    internal class BrowsePlugin : ContextPlugin
    {
        private readonly string[] kwEnter = [ "website ", "browse " ];
        public bool KeywordDetection { get; set; } = true;
        public bool ModelDetection { get; set; } = true;

        private List<WebsiteDefinition> websites = [];

        #region *** Interface Implementation ***

        /// <summary>
        /// Add the current location to the system prompt (if any)
        /// </summary>
        /// <param name="userinput">user's prompt</param>
        /// <param name="log">chatlog</param>
        /// <param name="response">contains the bit to be added to sysprompt (out)</param>
        /// <returns></returns>
        public override bool AddToSystemPrompt(string userinput, Chatlog log, out string response)
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
        public override bool ReplaceOutput(string botoutput, Chatlog log, out string response)
        {
            response = string.Empty;
            return ReplaceUserInput(botoutput, log, out response); 
        }

        /// <summary>
        /// Used to intercept the user's input and check if it contains a location keyword.
        /// </summary>
        /// <param name="userinput"></param>
        /// <param name="log"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public override bool ReplaceUserInput(string userinput, Chatlog log, out string response)
        {
            if (kwEnter.Any(kw => userinput.Contains(kw, StringComparison.OrdinalIgnoreCase)))
            {
                var x = QueryLLM(userinput).GetAwaiter().GetResult();
                if (!string.IsNullOrEmpty(x))
                {
                    
                }
            }
            response = string.Empty;
            return false;
        }

        #endregion


        private string BuildCheckPrompt(string userinput)
        {
            websites = [];
            var prompt = new StringBuilder();
            prompt.AppendLinuxLine("Your goal is to determine if the user asked you to retrive information from one of the websites specified below.");
            prompt.AppendLinuxLine();
            prompt.AppendLinuxLine("# Available Websites:");
            var i = 1;
            foreach (var loc in DataFiles.Websites)
            {
                prompt.AppendLinuxLine(i.ToString() + ". " + loc.Value.WebsiteName);
                prompt.AppendLinuxLine(loc.Value.WebsiteInfo);
                websites.Add(loc.Value);
                i++;
            }
            prompt.AppendLinuxLine(i.ToString() + ". Stack Overflow");
            prompt.AppendLinuxLine("Q&A website for programming related tasks.");

            prompt.AppendLinuxLine("# Rules:");
            prompt.AppendLinuxLine("- If no website in the list above correspond to what the user requested, answer: No");
            prompt.AppendLinuxLine("- Otherwise, pick the most likely website from the list by answer with the corresponding number.").AppendLinuxLine();
            prompt.AppendLinuxLine("# Examples:");
            prompt.AppendLinuxLine("User: Can you check the web and look what's wrong with my C# script?");
            prompt.AppendLinuxLine("Response: " + i.ToString());
            prompt.AppendLinuxLine("User: See you soon.");
            prompt.AppendLinuxLine("Response: No").AppendLinuxLine();
            prompt.AppendLinuxLine("# User message to be evaluated:");
            prompt.AppendLinuxLine(userinput);

            var sysprompt = LLMSystem.Instruct.FormatSinglePrompt(AuthorRole.System, LLMSystem.User, LLMSystem.Bot, prompt.ToString());

            if (LLMSystem.Instruct.BotStart != null)
                sysprompt += LLMSystem.Instruct.BotStart;
            return sysprompt;
        }

        /// <summary>
        /// Inference and Input handler (async) TO REDO
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="lb"></param>
        /// <returns></returns>
        private async Task<string> QueryLLM(string inputText)
        {
            var fullprompt = BuildCheckPrompt(inputText);
            var fullresponse = new StringBuilder();
            var llmparams = LLMSystem.Sampler.GetCopy();
            llmparams.Temperature = 0;
            llmparams.Prompt = fullprompt;
            var result = await LLMSystem.Client.GenerateAsync(llmparams);
            string finalstr = string.Empty;
            foreach (var item in result.Results)
            {
                finalstr += item.Text;
            }
            if (string.IsNullOrEmpty(finalstr))
                return string.Empty;
            if (finalstr.Equals("no", StringComparison.InvariantCultureIgnoreCase) || !int.TryParse(finalstr, out var found) || found > websites.Count)
                return string.Empty;
            return websites[found - 1].UniqueName;
        }
    }
}