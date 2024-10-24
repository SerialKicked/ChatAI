using System.Text;
using System.Windows;
using Microsoft.Extensions.Logging;
using WaifuAI.Files;
using WaifuAI.Memory;
using Newtonsoft.Json;
using System.IO;
using WaifuAI.Web;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using static LLama.Common.ChatHistory;
using System;

namespace WaifuAI
{
    enum SystemStatus { NotInit, Ready, Busy, Automated }
    enum SystemPromptSection { MainPrompt, BotBio, UserBio, Scenario, Memory, ContextInfo }

    public class InsertPrompt(string prompt, AuthorRole role = AuthorRole.SysPrompt, int depth = 1)
    {
        public AuthorRole Role = role;
        public string Prompt = prompt;
        public int Depth = depth;
    }

    public delegate void BasicDelegateFunction();

    static class LLMSystem
    {
        public static int MaxRAGEntries { get; set; } = 3;
        public static int RAGIndex { get; set; } = 3;
        public static int ReservedSessionTokens { get; set; } = 2048;
        public static int MaxReplyLength { get; set; } = 512;
        public static int MaxContextLength { 
            get => maxContextLength;
            set 
            {
                if (value != maxContextLength) 
                    InvalidatePromptCache();
                maxContextLength = value;
            }
        }
        public static string CurrentModel { get; private set; } = string.Empty;
        public static string Backend { get; private set; } = string.Empty;
        public static double ForceTemperature { get; set; } = 0.7;
        public static string ScenarioOverride { get; set; } = string.Empty;

        public static LLMWebsite? Website = null;

        public static EventHandler<string>? OnFullPromptReady;
        /// <summary> Called during inference each time the LLM outputs a new token </summary>
        public static EventHandler<string>? OnInferenceStreamed;
        /// <summary> Called once the inference has ended, returns the full string </summary>
        public static EventHandler<string>? OnInferenceEnded;
        /// <summary> Called when the system changes states (no init, busy, ready) </summary>
        public static EventHandler<SystemStatus>? OnStatusChanged;
        private static void RaiseOnFullPromptReady(string fullprompt) => OnFullPromptReady?.Invoke(null, fullprompt);
        private static void RaiseOnStatusChange(SystemStatus newStatus) => OnStatusChanged?.Invoke(null, newStatus);
        private static void RaiseOnInferenceStreamed(string addedString) => OnInferenceStreamed?.Invoke(null, addedString);
        private static void RaiseOnInferenceEnded(string fullString) => OnInferenceEnded?.Invoke(null, fullString);


        public static BasicDelegateFunction? UI_RefreshChat = null;


        public static SystemStatus Status
        {
            get => status;
            private set
            {
                status = value;
                RaiseOnStatusChange(value);
            }
        }

        public static int CurrentTokenCost { get; private set; } = 0;

        public static bool LongTermMemory
        {
            get => longTermMemory;
            set
            {
                longTermMemory = value;
            }
        }

        public static Character Bot { get => bot; set => ChangeBot(value); }

        public static ILogger? logger = null;

        // Default/Current Characters, Users, Instruct format, and inference parameters
        private static Character bot = new() { IsUser = false, Name = "Assistant", Bio = "You are an helpful AI assistant whose goal is to answer questions and complete tasks.", UniqueName = string.Empty };
        public static Character User = new() { IsUser = true, Name = "User", UniqueName = string.Empty };
        public static InstructFormat Instruct { 
            get => instruct; 
            set
            {
                instruct = value;
                InvalidatePromptCache();
            } 
        }
        public static SamplerSettings Sampler = new();
        public static SystemPrompt SystemPrompt = new();
        public static Chatlog History => Bot.History;

        private static SystemStatus status = SystemStatus.NotInit;
        private static bool longTermMemory = true;
        private static int systemPromptSize = 0;

        public static readonly string NewLine = "\n";
        private static string StreamingTextProgress = string.Empty;

        private static List<WorldEntry> _currentWorldEntries = [];
        private static string _LastGeneratedPrompt = string.Empty;
        private static readonly HttpClient _httpclient = new();
        public static KClient Client = new(_httpclient);
        private static int maxContextLength = 4096;
        private static InstructFormat instruct = new();
        public static HashSet<Guid> usedGuidInSession = [];

        public static void Init()
        {
            if (Status != SystemStatus.NotInit)
                return;
            Client.BaseUrl = "http://localhost:5001";
            Client.ReadResponseAsString = true;
            Client.StreamingMessageReceived += Client_StreamingMessageReceived;
            Status = SystemStatus.Ready;
        }

        private static void Client_StreamingMessageReceived(object? sender, TextStreamingEvenArg e)
        {
            // "null", "stop", "length"
            if (e.Data.finish_reason != "null")
            {
                if (!string.IsNullOrEmpty(e.Data.token))
                    StreamingTextProgress += e.Data.token;
                var response = StreamingTextProgress.Trim();
                foreach (var ctxplug in Bot.Plugins)
                {
                    if (ctxplug.ReplaceUserInput(ReplaceMacros(response, User, Bot), History, out var editedresponse))
                        response = editedresponse;
                }
                RaiseOnInferenceEnded(response);
                if (Status != SystemStatus.Automated)
                    Status = SystemStatus.Ready;
            }
            else
            {
                StreamingTextProgress += e.Data.token;
                RaiseOnInferenceStreamed(e.Data.token);
            }
        }

        public static string ReplaceMacros(string inputText)
        {
            return ReplaceMacros(inputText, User, Bot);
        }

        /// <summary>
        /// Replaces the macros in a string with the actual values.
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="user"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public static string ReplaceMacros(string inputText, Character user, Character character)
        {
            StringBuilder res = new(inputText);
            res.Replace("{{user}}", user.Name)
               .Replace("{{userbio}}", user.GetBio(character.Name))
               .Replace("{{char}}", character.Name)
               .Replace("{{charbio}}", character.GetBio(user.Name))
               .Replace("{{date}}", DateToHumanString(DateTime.Now))
               .Replace("{{time}}", DateTime.Now.ToShortTimeString())
               .Replace("{{day}}", DateTime.Now.DayOfWeek.ToString())
               .Replace("{{scenario}}", string.IsNullOrWhiteSpace(ScenarioOverride) ? character.GetScenario(user.Name) : ScenarioOverride)
               .Replace("{{instructprompt}}", Instruct.SystemPrompt);
            return res.ToString();
        }

        /// <summary>
        /// Change the current bot persona.
        /// </summary>
        /// <param name="newbot"></param>
        private static void ChangeBot(Character newbot)
        {
            InvalidatePromptCache();
            _currentWorldEntries = [];
            bot.EndSession(true);
            bot = newbot;
            bot.BeginSession();
            RAGSystem.VectorizeChatlog(History);
        }

        /// <summary>
        /// Returns the current token count of a string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int GetTokenCount(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;
            else if (text.Length > MaxContextLength * 10)
                return text.Length / 5;
            try
            {
                var mparams = new KcppPrompt() { Prompt = text };
                var res = Client.TokencountAsync(mparams).GetAwaiter().GetResult();
                return res.Value;
            }
            catch (Exception)
            {
                //MessageBox.Show($"An error occured while counting tokens, estimate used instead. {ex.Message}");
                return text.Length / 5; // or any default value you want to return in case of an error
            }
        }

        public static bool CancelGeneration()
        {
            try
            {
                var mparams = new Body3() { };
                var res = Client.AbortAsync(mparams).GetAwaiter().GetResult();
                if (res.Success)
                    Status = SystemStatus.Ready;
                return res.Success;
            }
            catch (Exception)
            {
                //MessageBox.Show($"An error occured while counting tokens, estimate used instead. {ex.Message}");
                return true; // or any default value you want to return in case of an error
            }
        }

        /// <summary>
        /// Connects to the LLM server and retrieves the needed info.
        /// </summary>
        public static async Task Connect()
        {
            Init();
            try
            {
                var result = await Client.TrueMaxContextLengthAsync();
                MaxContextLength = result.Value;
                var info = await Client.ModelAsync();
                var index = info.Result.IndexOf('/');
                if (index > 0)
                    info.Result = info.Result[(index + 1)..];
                CurrentModel = info.Result;
                var engine = await Client.ExtraVersionAsync();
                Backend = engine.result + " " + engine.version;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error while connecting: {ex.Message}");
            }
        }

        /// <summary>
        /// Generates a full prompt for the LLM to use
        /// </summary>
        /// <param name="newMessage">Added message from the user</param>
        /// <returns></returns>
        private static async Task<string> GenerateFullPrompt(AuthorRole MsgSender, string newMessage)
        {
            var msg = string.IsNullOrEmpty(newMessage) ? string.Empty : Instruct.FormatSinglePrompt(MsgSender, User, Bot, newMessage);
            var tokencount = string.IsNullOrEmpty(msg) ? 0 :GetTokenCount(msg);
            var rawprompt = new StringBuilder(RawSystemPrompt(User, Bot));
            var inserts = new Dictionary<int, string>();
            if (Bot.MyWorlds.Count > 0)
            {
                _currentWorldEntries = [];
                foreach (var world in Bot.MyWorlds)
                {
                    _currentWorldEntries.AddRange(world.FindEntries(History, newMessage));
                }
                var entries = _currentWorldEntries.FindAll(e => e.Position == WEPosition.SystemPrompt);
                if (entries.Count > 0)
                {
                    rawprompt.AppendLinuxLine().AppendLinuxLine(SystemPrompt.WorldInfoTitle);
                    foreach (var item in entries)
                        rawprompt.AppendLinuxLine(item.Message);
                }
                entries = _currentWorldEntries.FindAll(e => e.Position == WEPosition.Chat);
                foreach (var item in entries)
                {
                    if (!inserts.TryAdd(item.PositionIndex, item.Message))
                        inserts[item.PositionIndex] += NewLine + item.Message;
                }
            }
            foreach (var ctxplug in Bot.Plugins)
            {
                if (ctxplug.AddToSystemPrompt(newMessage, History, out var ctxinfo))
                    rawprompt.AppendLinuxLine(ctxinfo);
            }

            var sysprompt = Instruct.BoSToken + Instruct.FormatSinglePrompt(AuthorRole.SysPrompt, User, Bot, rawprompt.ToString());

            systemPromptSize = GetTokenCount(sysprompt);
            tokencount += systemPromptSize;
            var memprompt = string.Empty;
            usedGuidInSession = [];
            if (Bot.UseRAG && RAGSystem.Enabled)
            {
                var searchmessage = string.IsNullOrWhiteSpace(newMessage) ? History.GetLastUserMessageContent() : newMessage;
                var search = await RAGSystem.Search(ReplaceMacros(searchmessage), MaxRAGEntries);
                memprompt = MemoriesToMessage(search);
                if (!string.IsNullOrEmpty(memprompt))
                {
                    if (RAGIndex == -1)
                    {
                        memprompt = Instruct.FormatSinglePrompt(AuthorRole.SysPrompt, User, Bot, memprompt);
                        tokencount += GetTokenCount(memprompt);
                    }
                    else
                    {
                        if (!inserts.TryAdd(RAGIndex, memprompt))
                            inserts[RAGIndex] += NewLine + memprompt;
                    }
                    foreach (var item in search)
                        usedGuidInSession.Add(item.session.Guid);
                }
            }
            if (string.IsNullOrEmpty(newMessage) && MsgSender == AuthorRole.User)
                tokencount += GetTokenCount(Instruct.GetResponseStart(User));
            else
                tokencount += GetTokenCount(Instruct.GetResponseStart(Bot));
            var availtokens = (int)(MaxContextLength) - tokencount - MaxReplyLength;
            var history = History.GetFormatedDialogs(availtokens, Bot.SessionMemorySystem, inserts);


            if (string.IsNullOrEmpty(newMessage) && MsgSender == AuthorRole.User)
            {
                var res = !string.IsNullOrEmpty(memprompt) && RAGIndex == -1 ?
                    sysprompt + NewLine + memprompt + history + msg + Instruct.GetUserStart(User) :
                    sysprompt + NewLine + history + msg + Instruct.GetUserStart(User);
                return res;
            }
            else
            {
                var res = !string.IsNullOrEmpty(memprompt) && RAGIndex == -1 ?
                    sysprompt + NewLine + memprompt + history + msg + Instruct.GetResponseStart(Bot) :
                    sysprompt + NewLine + history + msg + Instruct.GetResponseStart(Bot);
                return res;
            }
        }

        private static string MemoriesToMessage(List<(ChatSession session, EmbedType category, float distance)> memories)
        {
            if (memories.Count == 0)
                return string.Empty;
            var stbuild = new StringBuilder();
            stbuild.AppendLinuxLine("The message from {{user}} at the bottom triggered the following memories:");
            foreach (var (session, _, _) in memories)
            {
                stbuild.AppendLinuxLine(session.GetRawMemory());
            }
            return stbuild.ToString();
        }

        /// <summary>
        /// Generates a raw system prompt with arbitrary initial prompt, user , and character bios and the (optional) scenario.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public static string RawSystemPrompt(Character user, Character character)
        {
            var selprompt = !string.IsNullOrEmpty(character.SystemPrompt) ? character.SystemPrompt : SystemPrompt.Prompt;
            var res = new StringBuilder(selprompt).AppendLinuxLine();
            if (character != null && !string.IsNullOrEmpty(character.Scenario))
            {
                res.AppendLinuxLine().AppendLinuxLine(SystemPrompt.ScenarioTitle).AppendLinuxLine("{{scenario}}");
            }
            return res.ToString();
        }

        public static async Task AddBotMessage()
        {
            if (Status == SystemStatus.Busy)
                return;
            await StartGeneration(AuthorRole.Assistant, "");
        }

        public static async Task ImpersonateUser()
        {
            if (Status == SystemStatus.Busy)
                return;
            await StartGeneration(AuthorRole.User, "");
        }

        /// <summary>
        /// Sends a message to the bot and logs it to the chat history. Response done through the RaiseOnInferenceStreamed and OnInferenceEnded events.
        /// </summary>
        /// <param name="MsgSender"></param>
        /// <param name="userInput"></param>
        /// <param name="logtohistory"></param>
        /// <returns></returns>
        public static async Task SendMessageToBot(SingleMessage message, bool logtohistory = true)
        {
            if (Status == SystemStatus.Busy)
                return;
            await StartGeneration(message.Role, message.Message);
        }

        /// <summary>
        /// Rerolls the last response from the bot.
        /// </summary>
        /// <returns></returns>
        public static async Task RerollLastMessage()
        {
            if (Status != SystemStatus.Ready || History.Messages.Count == 0 || History.LastMessage()?.Role != AuthorRole.Assistant)
                return;
            History.RemoveLast();
            if (string.IsNullOrEmpty(_LastGeneratedPrompt))
            {
                await StartGeneration(AuthorRole.Assistant, string.Empty);
            }
            else
            {
                Status = SystemStatus.Busy;
                StreamingTextProgress = string.Empty;
                GenerationInput genparams = Sampler.GetCopy();
                if (ForceTemperature >= 0)
                    genparams.Temperature = ForceTemperature;
                genparams.Max_context_length = MaxContextLength;
                genparams.Max_length = MaxReplyLength;
                genparams.Stop_sequence = Instruct.GetStoppingStrings(User, Bot);
                genparams.Prompt = _LastGeneratedPrompt;
                RaiseOnFullPromptReady(genparams.Prompt);
                await Client.GenerateTextStreamAsync(genparams);
            }
        }

        public static async Task QueryWebsite(WebsiteDefinition webdef, string userinput)
        {
            if (Website != null)
            {
                Website.KillEvent();
            }
            StartAutomation();
            Website = new LLMWebsite(webdef, userinput);
            await Website.Start();
        }

        public static void StartAutomation() => Status = SystemStatus.Automated;
        public static void StopAutomation() => Status = SystemStatus.Ready;

        /// <summary>
        /// Starts the generation process for the bot.
        /// </summary>
        /// <param name="MsgSender">Role of the sender</param>
        /// <param name="userInput">Message from sender</param>
        /// <returns></returns>
        private static async Task StartGeneration(AuthorRole MsgSender, string userInput)
        {
            if (Status == SystemStatus.Busy)
                return;
            if (Status != SystemStatus.Automated)
                Status = SystemStatus.Busy;

            var inputText = userInput;
            if (!string.IsNullOrEmpty(inputText))
                foreach (var ctxplug in Bot.Plugins)
                    if (ctxplug.ReplaceUserInput(ReplaceMacros(inputText, User, Bot), History, out var ctxinfo))
                        inputText = ctxinfo;
            StreamingTextProgress = string.Empty;
            GenerationInput genparams = Sampler.GetCopy();
            _LastGeneratedPrompt = await GenerateFullPrompt(MsgSender, inputText);
            if (ForceTemperature >= 0)
                genparams.Temperature = ForceTemperature;
            genparams.Max_context_length = MaxContextLength;
            genparams.Max_length = MaxReplyLength;
            genparams.Stop_sequence = Instruct.GetStoppingStrings(User, Bot);
            genparams.Prompt = _LastGeneratedPrompt;
            if (!string.IsNullOrEmpty(userInput))
                Bot.History.LogMessage(MsgSender, userInput, User, Bot);

            RaiseOnFullPromptReady(genparams.Prompt);
            await Client.GenerateTextStreamAsync(genparams);
        }

        /// <summary>
        /// Returns a message prefix depending on the role. (generally the user/bot's name)
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static string GetMessagePrefix(AuthorRole role)
        {
            return role switch
            {
                AuthorRole.System => "**SYSTEM:** ",
                AuthorRole.SysPrompt => "**SYS PROMPT:** ",
                AuthorRole.User => "**" + User.Name + ":** ",
                AuthorRole.Assistant => "**" + Bot.Name + ":** ",
                _ => "**Error:** ",
            };
        }

        public static string GetMessagePrefix(SingleMessage message)
        {
            return message.Role switch
            {
                AuthorRole.System => "**SYSTEM:** ",
                AuthorRole.SysPrompt => "**SYS PROMPT:** ",
                AuthorRole.User => "**" + message.User.Name + ":** ",
                AuthorRole.Assistant => "**" + message.Bot.Name + ":** ",
                _ => "**Error:** ",
            };
        }

        /// <summary>
        /// Returns an away string depending on the last chat's date.
        /// </summary>
        /// <returns></returns>
        public static string GetAwayString()
        {
            if (History.Messages.Count == 0 || !Bot.SenseOfTime)
                return string.Empty;

            var timespan = DateTime.Now - History.Messages.Last().Date;
            if (timespan <= new TimeSpan(2, 0, 0))
                return string.Empty;

            var msgtxt = (DateTime.Now.Date != History.Messages.Last().Date.Date) || (timespan > new TimeSpan(12, 0, 0)) ? "It's {{day}}, {{date}} at {{time}}." : string.Empty;
            if (timespan.Days > 1)
                msgtxt += " Your last chat was " + timespan.Days.ToString() + " days ago.";
            else if (timespan.Days == 1)
                msgtxt += " The last chat was yesterday.";
            else
                msgtxt += " The last chat was about " + timespan.Hours.ToString() + " hours ago.";
            msgtxt = "*" + msgtxt.Trim() + "* ";
            return ReplaceMacros(msgtxt, User, Bot);
        }

        public static string DateToHumanString(DateTime date)
        {
            static string GetDaySuffix(int day)
            {
                if (day >= 11 && day <= 13)
                {
                    return "th";
                }

                return (day % 10) switch
                {
                    1 => "st",
                    2 => "nd",
                    3 => "rd",
                    _ => "th",
                };
            }

            string daySuffix = GetDaySuffix(date.Day);
            string formattedDate = date.ToString("MMMM d", CultureInfo.InvariantCulture) + daySuffix + ", " + date.Year.ToString(CultureInfo.InvariantCulture);
            return formattedDate;
        }

        /// <summary>
        /// Turn a time span into something clearly legible for a human
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string TimeSpanToHumanString(TimeSpan span)
        {
            // Turn a time span into something clearly legible for a human
            if (span.Days > 0)
                return span.Days.ToString() + " days";
            else if (span.Hours > 0)
                return span.Hours.ToString() + " hours";
            else if (span.Minutes > 0)
                return span.Minutes.ToString() + " minutes";
            else
                return span.Seconds.ToString() + " seconds";
        }

        internal static void RemoveLastMessage()
        {
            LLMSystem.History.RemoveLast();
            InvalidatePromptCache();
        }

        internal static void InvalidatePromptCache()
        {
            _LastGeneratedPrompt = string.Empty;
        }
    }
}
