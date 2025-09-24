using AngleSharp.Dom;
using AngleSharp.Io;
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
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp;
using WaifuAI.Web;
using LetheAISharp.API;

namespace WaifuAI.Plugins
{
    public class WebNavigationResult(bool isSuccess, string? response, string? subPageLink)
    {
        public bool IsSuccess { get; set; } = isSuccess;
        public string? Response { get; set; } = response;
        public string? SubPageLink { get; set; } = subPageLink;
    }

    public class BrowsePlugin : IContextPlugin
    {
        public string PluginID { get; } = "Browser";
        public bool Enabled { get; set; } = false;

        private readonly string[] kwEnter = [ "website", "browse", " web", "browsing", "find a ", "search for ", "look for ", "find me" ];
        public bool KeywordDetection { get; set; } = true;
        public bool ModelDetection { get; set; } = true;
        public bool EnforceCorrectGrammar { get; set; } = false;

        public Dictionary<string,string> WebsiteSpecificInfo { get; set; } = [];

        private List<WebsiteDefinition> websites = [];
        private WebsiteDefinition? Website;
        private string _basegoal = string.Empty;
        private PageType _location;
        private readonly WebScraper crawler = new();
        private string _currenthistory = string.Empty;

        private bool responseAppendNeeded = false;
        private WEntry? lastresponse = null;


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
                formatedresponsed.Append($"**Link:** [{lastresponse.Title}]({lastresponse.Link})");
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
            List<string> test = [];
            responseAppendNeeded = false;
            string foundCommand = string.Empty;
            foreach (var item in DataFiles.Websites)
            {
                test.Add(item.Value.CommandID);
                if (userinput.Contains(item.Value.CommandID, StringComparison.OrdinalIgnoreCase))
                    foundCommand = item.Key;
            }

            if (!string.IsNullOrEmpty(foundCommand) || (KeywordDetection && kwEnter.Any(kw => userinput.Contains(kw, StringComparison.OrdinalIgnoreCase))))
            {
                LLMEngine.NamesInPromptOverride = false;
                var x = await QueryLLM(userinput, foundCommand);
                if (!string.IsNullOrEmpty(x))
                {
                    if (DataFiles.Websites.TryGetValue(x, out var site))
                    {
                        Website = site;
                        Program.BigForm!.ForceUpdateLastMessage($"**{LLMEngine.Bot.Name}:** *I am browsing {Website.WebsiteName}...*");
                    }
                    else
                    {
                        LLMEngine.NamesInPromptOverride = null;
                        return new PluginResponse { IsHandled = false, Response = null };
                    }
                    // call the website plugin here
                    _currenthistory = string.Empty;
                    var webresult = await StartWebNavigation(userinput);
                    if (webresult.IsSuccess)
                    {
                        Program.BigForm!.ForceUpdateLastMessage($"**{LLMEngine.Bot.Name}:** *I am writing a message...*");
                    }
                    else
                    {
                        Program.BigForm.ForceUpdateLastMessage($"**{LLMEngine.Bot.Name}:** *I am writing a message (web navigation failed)...*");
                    }

                    var output = new PluginResponse 
                    { 
                        IsHandled = webresult.IsSuccess, 
                        Response = webresult.Response,
                        AuthorRole = AuthorRole.System,
                        Replace = false
                    };
                    LLMEngine.NamesInPromptOverride = null;
                    return output;
                }
            }
            LLMEngine.NamesInPromptOverride = null;
            return new PluginResponse { IsHandled = false, Response = null }; // call the website plugin
        }

        #endregion

        private static string BuildInitialPrompt()
        {
            var promptbuilder = new StringBuilder();
            promptbuilder.AppendLinuxLine("You are a web browsing agent. Your goal is to find the requested information while taking the information presented in the chatlog below into consideration.");
            promptbuilder.AppendLinuxLine();
            promptbuilder.AppendLinuxLine("# Characters:");
            promptbuilder.AppendLinuxLine($"- **{LLMEngine.User.Name}:** {LLMEngine.User.GetBio(LLMEngine.Bot.Name).RemoveNewLines()}");
            promptbuilder.AppendLinuxLine($"- **{LLMEngine.Bot.Name}:** {LLMEngine.Bot.GetBio(LLMEngine.User.Name).RemoveNewLines()}");
            promptbuilder.AppendLinuxLine();
            promptbuilder.AppendLinuxLine("# Recent Chatlog:");
            promptbuilder.AppendLinuxLine(LLMEngine.History.CurrentSession.GetRawDialogs(600, true, true, false, false));
            return promptbuilder.ToString();
        }

        private async Task<string> SendQuery(object context)
        {
            if (context is GenerationInput llmparams)
            {
                if (EnforceCorrectGrammar)
                    llmparams.Grammar = "root ::= ([0-9][0-9]?[0-9]?)";
            }
            var response = await LLMEngine.SimpleQuery(context);
            if (!EnforceCorrectGrammar)
                response = response.RemoveThinkingBlocks();
            // strip anything that is not a number from response
            response = new string([.. response.Where(c => char.IsDigit(c))]);
            return response;
        }

        private async Task<WebNavigationResult> StartWebNavigation(string basegoal)
        {
            if (Website == null)
                return new WebNavigationResult(false, "Website not found.", string.Empty);
            Website.AllowComplexNavigation = false;
            _basegoal = basegoal;
            _location = PageType.FrontPage;
            LLMEngine.NamesInPromptOverride = false;
            var promptbuilder = LLMEngine.GetPromptBuilder();
            var strbuilder = new StringBuilder();
            {
                strbuilder.Append(BuildInitialPrompt());
                strbuilder.AppendLinuxLine();
            }
            strbuilder.AppendLinuxLine(Website.RenderFrontPage(string.Empty));
            promptbuilder.AddMessage(AuthorRole.SysPrompt, strbuilder.ToString());
            promptbuilder.AddMessage(AuthorRole.User, $"Complete this request from {LLMEngine.User.Name}: {_basegoal}");
            var addthinking = LLMEngine.Instruct.PrefillThinking;
            if (EnforceCorrectGrammar)
                addthinking = false;
            var context = promptbuilder.PromptToQuery(AuthorRole.Assistant, 0.5f, 1024, overridePrefill: addthinking);
            var response = await SendQuery(context);
            LLMEngine.NamesInPromptOverride = null;
            if (string.IsNullOrEmpty(response))
                return new WebNavigationResult(false, "Failed to navigate the website properly.", null);
            if (int.TryParse(response, out var index) && index <= Website.MainLinks.Count && index > 0)
            {
                _currenthistory += response + LLMEngine.Instruct.BotEnd;
                return await DoPage(Website.MainLinks[index-1]);
            }
            else
            {
                crawler.ClearCache();
                return new WebNavigationResult(false, "Failed to navigate the website properly.", null);
            }
        }

        public async Task<WebNavigationResult> DoPage(WLink page)
        {
            _location = page.Category;
            Program.BigForm!.ForceUpdateLastMessage($"**{LLMEngine.Bot.Name}:** *I am browsing {page.Title}...*");
            var websiterender = await Website!.RenderPage(page.ID, string.Empty, crawler);
            if (string.IsNullOrEmpty(websiterender))
            {
                crawler.ClearCache();
                return new WebNavigationResult(false, "Failed to load the page content.", null);

            }

            LLMEngine.NamesInPromptOverride = false;
            var promptbuilder = LLMEngine.GetPromptBuilder();

            var strbuild = new StringBuilder();
            strbuild.AppendLinuxLine(BuildInitialPrompt());
            strbuild.AppendLinuxLine();
            strbuild.AppendLinuxLine(websiterender);

            promptbuilder.AddMessage(AuthorRole.SysPrompt, strbuild.ToString());
            promptbuilder.AddMessage(AuthorRole.User, $"Complete this request from {LLMEngine.User.Name}: {_basegoal}");

            var addthinking = LLMEngine.Instruct.PrefillThinking;
            if (EnforceCorrectGrammar)
                addthinking = false;
            var ctx = promptbuilder.PromptToQuery(AuthorRole.Assistant, 0.5f, 1024, overridePrefill: addthinking);

            var response = await SendQuery(ctx);
            LLMEngine.NamesInPromptOverride = null;

            if (string.IsNullOrEmpty(response))
                return new WebNavigationResult(false, "Null Answer", null);
            switch (_location)
            {
                case PageType.MetaPage:
                    {
                        var metalinks = Website.SubLinks[page.ID];
                        if (int.TryParse(response, out var metaindex))
                        {
                            if (metalinks?.Count > 0 &&  metaindex <= metalinks.Count && metaindex > 0)
                                return await DoPage(metalinks[metaindex - 1]);
                            //else if (NavigationHistory && metaindex == 0)
                            //    return await StartWebNavigation(_basegoal);
                        }
                        crawler.ClearCache();
                        return new WebNavigationResult(false, "Failed to navigate meta page", null);
                    }
                case PageType.ListingPage:
                    {
                        if (int.TryParse(response, out var index))
                        {
                            if (index <= Website.CurrentListing.Entries.Count && index > 0)
                                return new WebNavigationResult(true, TurnInResult(Website.CurrentListing.Entries[index - 1]), null);
                            //else if (NavigationHistory && index == 0)
                            //    return await StartWebNavigation(_basegoal);
                        }
                        crawler.ClearCache();
                        return new WebNavigationResult(false, "Failed to navigate the listing properly.", null);
                    }
                default:
                    crawler.ClearCache();
                    return new WebNavigationResult(false, "The request page type is not handled yet.", null);
            }
        }

        private string TurnInResult(WEntry wEntry)
        {
            var text = new StringBuilder();
            text.AppendLinuxLine("After searching the net, {{char}} found the following link:");
            text.AppendLinuxLine(wEntry.ToString()).AppendLinuxLine();
            text.Append("Inform {{user}} about the page you've just found. Integrate this information seamlessly into the current conversation. You don't need to post the link, it'll be added automatically.");
            responseAppendNeeded = true;
            lastresponse = wEntry;
            return text.ToString();
        }

        private object TaskSelectionPrompt(string userinput, string cmd)
        {
            websites = [];
            LLMEngine.NamesInPromptOverride = false;
            var builder = LLMEngine.GetPromptBuilder();
            var prompt = new StringBuilder();
            prompt.AppendLinuxLine("Your goal is to determine if the user asked you to complete one of the following tasks:");
            prompt.AppendLinuxLine();
            prompt.AppendLinuxLine("# Available Tasks:");
            var x = 1;
            foreach (var item in DataFiles.Websites)
            {
                if (!string.IsNullOrEmpty(cmd) && item.Key != cmd)
                    continue;
                prompt.AppendLinuxLine($"{x}. {item.Value.TaskQuery}");
                x++;
                websites.Add(item.Value);
            }
            prompt.AppendLinuxLine($"{x}. Retrieve weather info about a particular location.");
            prompt.AppendLinuxLine();
            prompt.AppendLinuxLine("# Rules:");
            prompt.AppendLinuxLine("- If one of the tasks above corresponds to the user input, answer with the corresponding number only, nothing else.");
            prompt.AppendLinuxLine("- If no task in the list above corresponds to what the user requested, answer: 0");
            prompt.AppendLinuxLine("- Pick one single option.");
            prompt.AppendLinuxLine("- Do not add any commentary or names.").AppendLinuxLine();
            prompt.AppendLinuxLine("# Examples:");
            prompt.AppendLinuxLine("User: Do you know what's the meteo in Paris?");
            prompt.AppendLinuxLine("Response: " + x.ToString());
            prompt.AppendLinuxLine("User: See you soon.");
            prompt.AppendLinuxLine("Response: 0").AppendLinuxLine();
            prompt.AppendLinuxLine("# Message to be evaluated:");
            prompt.Append(userinput);
            
            builder.AddMessage(AuthorRole.SysPrompt, prompt.ToString());
            builder.AddMessage(AuthorRole.User, $"Determine if the user asked you to complete one of the above tasks and respond with the corresponding number.");
            var addthinking = LLMEngine.Instruct.PrefillThinking;
            if (EnforceCorrectGrammar)
                addthinking = false;
            var res = builder.PromptToQuery(AuthorRole.Assistant, 0.5f, 1024, overridePrefill: addthinking);
            LLMEngine.NamesInPromptOverride = null;
            return res;
        }

        /// <summary>
        /// Inference and Input handler (async) TO REDO
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="lb"></param>
        /// <returns></returns>
        private async Task<string> QueryLLM(string inputText, string cmd)
        {
            var finalstr = await LLMEngine.SimpleQuery(TaskSelectionPrompt(inputText, cmd));
            finalstr = finalstr.RemoveThinkingBlocks();
            if (string.IsNullOrEmpty(finalstr))
                return string.Empty;
            if (finalstr.Equals("no", StringComparison.InvariantCultureIgnoreCase) || !int.TryParse(finalstr, out var found) || found > websites.Count)
                return string.Empty; 
            return found == 0 ? string.Empty : websites[found-1].UniqueName;
        }
    }
}