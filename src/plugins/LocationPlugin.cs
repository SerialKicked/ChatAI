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

namespace WaifuAI.Plugins

{
    internal class LocationPlugin(string? world) : IContextPlugin
    {
        public string PluginID { get; } = "Location";
        public bool Enabled { get; set; } = false;

        private readonly string[] kwEnter = [ "go to ", "move to ", "get to " ];
        private readonly WorldInfo locations = !string.IsNullOrEmpty(world) && DataFiles.WorldInfos.TryGetValue(world, out var info) ? info : new();
        private WorldEntry? currentLocation = null;

        public bool IsLocationSet => currentLocation != null;
        public string CurrentLocationName => currentLocation?.Name ?? string.Empty;
        public string CurrentLocationDesc => currentLocation?.Message ?? string.Empty;
        public bool KeywordDetection { get; set; } = true;
        public bool ModelDetection { get; set; } = true;

        #region *** Interface Implementation ***

        /// <summary>
        /// Add the current location to the system prompt (if any)
        /// </summary>
        /// <param name="userinput">user's prompt</param>
        /// <param name="log">chatlog</param>
        /// <param name="response">contains the bit to be added to sysprompt (out)</param>
        /// <returns></returns>
        public bool AddToSystemPrompt(string userinput, Chatlog log, out string response)
        {
            if (currentLocation == null || (!ModelDetection && !KeywordDetection))
            {
                response = string.Empty;
                return false;
            }
            var str = new StringBuilder();
            str.AppendLinuxLine(LLMSystem.NewLine + LLMSystem.SystemPrompt.CategorySeparator+ " Current Location: " + currentLocation.Name);
            str.Append("{{user}} and {{char}} are currently at this location: ").AppendLinuxLine(currentLocation.Message);
            response = str.ToString();
            return true;
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
            if (KeywordDetection && !ModelDetection)
            {
                // Check if userinput contains one of the keywords in kwEnter
                if (kwEnter.Any(kw => userinput.Contains(kw, StringComparison.OrdinalIgnoreCase)))
                {
                    int index = userinput.IndexOfAny(['?', '.']);
                    string punctuation = index != -1 ? userinput.Substring(index, 1) : string.Empty;
                    if (string.IsNullOrEmpty(punctuation) || punctuation == ".")
                    {
                        var loc = locations.FindEntries(LLMSystem.History, userinput).FirstOrDefault();
                        if (loc != null)
                        {
                            LLMSystem.logger?.LogInformation("LocationPlugin KW Only: {output}", loc.Name);
                            currentLocation = loc;
                            //AddMovingInfSystemMessage(log, loc);
                        }
                    }
                }
            }
            else if (ModelDetection && !KeywordDetection)
            {
                // Check if the user input contains any of the locations using the QueryLLM method
                // Task.Run(async () => await QueryLLM(userinput, log)).Wait();
                await QueryLLM(userinput);
            }
            else if (ModelDetection && KeywordDetection)
            {
                // Check if userinput triggers any entry in locations, if so, run QueryLLM
                if (locations.FindEntries(LLMSystem.History, userinput)?.Count > 0)
                {
                    await QueryLLM(userinput);
                    // Task.Run(async () => await QueryLLM(userinput, log)).Wait();
                }
            }
            return new PluginResponse { IsHandled = false, Response = null };
        }

        #endregion

        private static void AddMovingInfSystemMessage(Chatlog log, WorldEntry newLoc)
        {
            var prompt = string.Format("{0} and {1} are moving to a new location: {2}. React accordingly.", LLMSystem.User.Name, LLMSystem.Bot.Name, newLoc.Name);
            //var msg = new SingleMessage(AuthorRole.System, DateTime.Now, prompt, LLMChatManager.Bot.Name, LLMChatManager.User.Name, false);
            log.LogMessage(AuthorRole.System, prompt, LLMSystem.User, LLMSystem.Bot);
        }

        private string BuildCheckPrompt(string userinput)
        {
            var prompt = new StringBuilder();
            prompt.AppendLinuxLine("Your goal is to determine if the user intends to go to a specific location from the list below:");
            prompt.AppendLinuxLine("- Red Cinema");
            foreach (var item in locations.Entries)
            {
                if (item != currentLocation)
                    prompt.AppendLinuxLine($"- {item.Name}");
            }
            prompt.AppendLinuxLine();
            prompt.AppendLinuxLine("If the user is asking a question about the location, say: No");
            prompt.AppendLinuxLine("If the user doesn't want to move to any of those locations now, say: No");
            prompt.AppendLinuxLine("If the user wants to move to one those locations now, only say the name of the place, nothing else.").AppendLinuxLine();
            prompt.AppendLinuxLine("# Examples:").AppendLinuxLine();
            prompt.AppendLinuxLine("User: Let's go to grandma's place!");
            prompt.AppendLinuxLine("Response: No").AppendLinuxLine();
            prompt.AppendLinuxLine("User: Let's go to the Red Cinema.");
            prompt.AppendLinuxLine("Response: Red Cinema").AppendLinuxLine();
            prompt.AppendLinuxLine("User: What do you think about going to the beach?");
            prompt.AppendLinuxLine("Response: No").AppendLinuxLine();
            prompt.AppendLinuxLine("User: Let's go to the beach.");
            prompt.AppendLinuxLine("Response: Beach").AppendLinuxLine();
            prompt.AppendLinuxLine("User: I don't want to go to the Red Cinema.");
            prompt.AppendLinuxLine("Response: No").AppendLinuxLine();

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
        private async Task QueryLLM(string inputText)
        {
            var fullprompt = BuildCheckPrompt(inputText);
            var fullresponse = new StringBuilder();
            var llmparams = LLMSystem.Sampler.GetCopy();
            llmparams.Temperature = 0;
            llmparams.Prompt = fullprompt;
            var finalstr = await LLMSystem.SimpleQuery(llmparams);
            LLMSystem.logger?.LogInformation("LocationPlugin Result: {output}", finalstr);
            if (string.IsNullOrEmpty(finalstr))
                return;
            if (finalstr.Equals("no", StringComparison.InvariantCultureIgnoreCase))
                return;
            var loc = locations.Entries.FirstOrDefault(l => l.Name.Equals(finalstr, StringComparison.InvariantCultureIgnoreCase));
            if (loc != null)
            {
                currentLocation = loc;
                //AddMovingInfSystemMessage(log, loc);
            }
        }
    }
}