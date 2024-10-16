using System.Text;
using System.Windows;
using Microsoft.Extensions.Logging;
using WaifuAI.Files;
using Newtonsoft.Json;
using System.IO;
using WaifuAI.Memory;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace WaifuAI
{
    enum LLMStatus { NotInit, Ready, Busy }
    enum SystemPromptSection { MainPrompt, BotBio, UserBio, Scenario, Memory, ContextInfo }

    static class LLMChatManager
    {
        public const int EmbeddingSize = 384;
        public const int EmbeddingDelay = 80;

        public static int MaxReplyLength { get; set; } = 512;
        public static int MaxContextLength { 
            get => maxContextLength; 
            set => maxContextLength = value; 
        }
        public static bool SkipSpecialTokens { get; set; } = false;
        public static string CurrentModel { get; private set; } = string.Empty;
        public static string Backend { get; private set; } = string.Empty;
        public static double ForceTemperature { get; set; } = 0.7;

        public static EventHandler<string>? OnFullPromptReady;
        /// <summary> Called during inference each time the LLM outputs a new token </summary>
        public static EventHandler<string>? OnInferenceStreamed;
        /// <summary> Called once the inference has ended, returns the full string </summary>
        public static EventHandler<string>? OnInferenceEnded;
        /// <summary> Called when the system changes states (no init, busy, ready) </summary>
        public static EventHandler<LLMStatus>? OnStatusChanged;
        private static void RaiseOnFullPromptReady(string fullprompt) => OnFullPromptReady?.Invoke(null, fullprompt);
        private static void RaiseOnStatusChange(LLMStatus newStatus) => OnStatusChanged?.Invoke(null, newStatus);
        private static void RaiseOnInferenceStreamed(string addedString) => OnInferenceStreamed?.Invoke(null, addedString);
        private static void RaiseOnInferenceEnded(string fullString) => OnInferenceEnded?.Invoke(null, fullString);

        public static LLMStatus Status
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
        public static InstructFormat Instruct = new();
        public static SamplerSettings Sampler = new();
        public static SystemPrompt SystemPrompt = new();
        public static Chatlog History => Bot.History;

        private static LLMStatus status = LLMStatus.NotInit;
        private static bool longTermMemory = true;
        private static int systemPromptSize = 0;

        public static readonly string NewLine = "\n";
        private static string StreamingTextProgress = string.Empty;

        private static List<WorldEntry> _currentWorldEntries = [];
        private static readonly HttpClient _httpclient = new();
        public static KClient Client = new(_httpclient);
        private static int maxContextLength = 4096;

        public static void Init()
        {
            if (Status != LLMStatus.NotInit)
                return;
            SkipSpecialTokens = false;
            Client.BaseUrl = "http://localhost:5001";
            Client.ReadResponseAsString = true;
            Client.StreamingMessageReceived += Client_StreamingMessageReceived;
            Status = LLMStatus.Ready;
        }

        private static void Client_StreamingMessageReceived(object? sender, TextStreamingEvenArg e)
        {
            // "null", "stop", "length"
            if (e.Data.finish_reason != "null")
            {
                var response = StreamingTextProgress;
                foreach (var ctxplug in Bot.Plugins)
                {
                    if (ctxplug.ReplaceUserInput(ReplaceMacros(response, User, Bot), History, out var editedresponse))
                        response = editedresponse;
                }
                RaiseOnInferenceEnded(response);
                Status = LLMStatus.Ready;
            }
            else
            {
                StreamingTextProgress += e.Data.token;
                RaiseOnInferenceStreamed(e.Data.token);
            }
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
               .Replace("{{date}}", DateTime.Now.ToShortDateString())
               .Replace("{{time}}", DateTime.Now.ToShortTimeString())
               .Replace("{{day}}", DateTime.Now.DayOfWeek.ToString())
               .Replace("{{scenario}}", character.GetScenario(user.Name))
               .Replace("{{instructprompt}}", Instruct.SystemPrompt);
            return res.ToString();
        }

        private static void ChangeBot(Character newbot)
        {
            bot.EndSession();
            bot = newbot;
            bot.BeginSession();
        }

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
            catch (Exception ex)
            {
                MessageBox.Show($"An error while counting tokens, estimate used instead: {ex.Message}");
                return text.Length / 5; // or any default value you want to return in case of an error
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
                var index = info.Result.IndexOf("/");
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
        private static string GenerateFullPrompt(AuthorRole MsgSender, string newMessage)
        {
            var msg = String.IsNullOrEmpty(newMessage) ? string.Empty : Instruct.FormatSinglePrompt(MsgSender, User, Bot, newMessage);
            var tokencount = GetTokenCount(msg);
            var rawprompt = new StringBuilder(RawSystemPrompt(User, Bot));
            if (Bot.MyWorlds.Count > 0)
            {
                _currentWorldEntries = [];
                foreach (var item in Bot.MyWorlds)
                {
                    _currentWorldEntries.AddRange(item.FindEntries(History, newMessage));
                }
                var entries = _currentWorldEntries.FindAll(e => e.Position == WEPosition.SystemPrompt);
                if (entries.Count > 0)
                {
                    rawprompt.AppendLinuxLine().AppendLinuxLine(SystemPrompt.WorldInfoTitle);
                    foreach (var item in entries)
                        rawprompt.AppendLinuxLine(item.Message);
                }
            }
            foreach (var ctxplug in Bot.Plugins)
            {
                if (ctxplug.AddToSystemPrompt(newMessage, History, out var ctxinfo))
                    rawprompt.AppendLinuxLine(ctxinfo);
            }

            var sysprompt = Instruct.FormatSinglePrompt(AuthorRole.SysPrompt, User, Bot, rawprompt.ToString());

            systemPromptSize = GetTokenCount(sysprompt);

            tokencount += GetTokenCount(sysprompt);
            tokencount += GetTokenCount(Instruct.GetResponseStart(Bot));
            var availtokens = (int)(MaxContextLength) - tokencount - MaxReplyLength;
            var history = History.GetFormatedDialogs(availtokens, Bot.SessionMemorySystem, _currentWorldEntries);
            var res = sysprompt + LLMChatManager.NewLine + history + msg + Instruct.GetResponseStart(Bot);
            return res;
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

        /// <summary>
        /// Sends a message to the bot and logs it to the chat history. Response done through the RaiseOnInferenceStreamed and OnInferenceEnded events.
        /// </summary>
        /// <param name="MsgSender"></param>
        /// <param name="userInput"></param>
        /// <param name="logtohistory"></param>
        /// <returns></returns>
        public static async Task SendMessageToBot(SingleMessage message, bool logtohistory = true)
        {
            if (Status == LLMStatus.Busy)
                return;
            if (logtohistory)
                Bot.History.LogMessage(message);
            await StartGeneration(message.Role, message.Message);
        }

        /// <summary>
        /// Rerolls the last response from the bot.
        /// </summary>
        /// <returns></returns>
        public static async Task RerollLastMessage()
        {
            if (Status != LLMStatus.Ready || History.Messages.Count == 0 || History.LastMessage()?.Role != AuthorRole.Assistant)
                return;
            History.RemoveLast();
            var lastusermsg = History.LastMessage() ?? new SingleMessage(AuthorRole.User, DateTime.Now, "Hi!", Bot.Name, User.Name);
            await StartGeneration(lastusermsg.Role, string.Empty);
        }

        /// <summary>
        /// Starts the generation process for the bot.
        /// </summary>
        /// <param name="MsgSender">Role of the sender</param>
        /// <param name="userInput">Message from sender</param>
        /// <returns></returns>
        private static async Task StartGeneration(AuthorRole MsgSender, string userInput)
        {
            if (Status == LLMStatus.Busy)
                return;
            Status = LLMStatus.Busy;

            var inputText = userInput;
            foreach (var ctxplug in Bot.Plugins)
            {
                if (ctxplug.ReplaceUserInput(ReplaceMacros(inputText, User, Bot), History, out var ctxinfo))
                    inputText = ctxinfo;
            }

            StreamingTextProgress = string.Empty;
            GenerationInput genparams = Sampler.GetCopy();
            if (ForceTemperature >= 0)
                genparams.Temperature = ForceTemperature;
            genparams.Max_context_length = MaxContextLength;
            genparams.Max_length = MaxReplyLength;
            genparams.Stop_sequence = Instruct.GetStoppingStrings(User, Bot);
            genparams.Prompt = GenerateFullPrompt(MsgSender, inputText);
            RaiseOnFullPromptReady(genparams.Prompt);
            await Client.GenerateTextStreamAsync(genparams);
        }

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

        public static string GetAwayString()
        {
            if (History.Messages.Count == 0 || !Bot.SenseOfTime)
                return string.Empty;

            var timespan = DateTime.Now - History.Messages.Last().Date;
            if (timespan <= new TimeSpan(2, 0, 0))
                return string.Empty;

            var msgtxt = (DateTime.Now.Date != History.Messages.Last().Date.Date) || (timespan > new TimeSpan(12, 0, 0)) ? "It's {{day}}, the {{date}} at {{time}}." : string.Empty;
            if (timespan.Days > 1)
                msgtxt += " Your last chat was " + timespan.Days.ToString() + " days ago.";
            else if (timespan.Days == 1)
                msgtxt += " The last chat was yesterday.";
            else
                msgtxt += " The last chat was about " + timespan.Hours.ToString() + " hours ago.";
            msgtxt = "*" + msgtxt.Trim() + "* ";
            return msgtxt;
        }
    }
}
