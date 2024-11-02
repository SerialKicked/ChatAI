using Newtonsoft.Json;
using NSwag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaifuAI.Files
{
    public class InstructFormat : BaseFile
    {
        public static readonly string[] Properties = [
            "SystemPrompt",
            "SysPromptStart", "SysPromptEnd",
            "SystemStart", "SystemEnd",
            "UserStart", "UserEnd",
            "BotStart", "BotEnd",
            "BoSToken", "StopSequence",
            "AddNamesToPrompt",
            "NewLinesBetweenMessages",
            "StopStrings"
            ];
        public string BoSToken { get; set; } = string.Empty;
        public string SystemStart { get; set; } = string.Empty;
        public string SystemEnd { get; set; } = string.Empty;
        public string UserStart { get; set; } = string.Empty;
        public string UserEnd { get; set; } = string.Empty;
        public string BotStart { get; set; } = string.Empty;
        public string BotEnd { get; set; } = string.Empty;
        public string StopSequence { get; set; } = string.Empty;
        public string SysPromptStart { get; set; } = string.Empty;
        public string SysPromptEnd { get; set; } = string.Empty;
        public bool AddNamesToPrompt { get; set; } = true;
        public bool NewLinesBetweenMessages { get; set; } = false;
        public string[] StopStrings { get; set; } = [];

        [JsonIgnore] private bool RealAddNameToPrompt => LLMSystem.NamesInPromptOverride ?? AddNamesToPrompt;

        public string GetResponseStart(Character bot)
        {
            var res = BotStart;
            if (RealAddNameToPrompt)
                res += bot.Name + ":";
            return res;
        }

        public string GetUserStart(Character user)
        {
            var res = UserStart;
            if (RealAddNameToPrompt)
                res += user.Name + ":";
            return res;
        }

        public string FormatSinglePrompt(AuthorRole role, Character user, Character bot, string prompt)
        {
            var realprompt = prompt;
            if (RealAddNameToPrompt)
            {
                if (role == AuthorRole.Assistant)
                    realprompt = string.Format("{0}: {1}", bot.Name, prompt);
                else if (role == AuthorRole.User)
                    realprompt = string.Format("{0}: {1}", user.Name, prompt);
            }
            switch (role)
            {
                case AuthorRole.Unknown:
                    realprompt = "[" + LLMSystem.ReplaceMacros(realprompt, user, bot) + "]";
                    break;
                case AuthorRole.System:
                    realprompt = SystemStart + LLMSystem.ReplaceMacros(realprompt, user, bot) + SystemEnd;
                    break;
                case AuthorRole.User:
                    realprompt = UserStart + LLMSystem.ReplaceMacros(realprompt, user, bot) + UserEnd;
                    break;
                case AuthorRole.Assistant:
                    realprompt = BotStart + LLMSystem.ReplaceMacros(realprompt, user, bot) + BotEnd;
                    break;
                case AuthorRole.SysPrompt:
                    realprompt = SysPromptStart + LLMSystem.ReplaceMacros(realprompt, user, bot) + SysPromptEnd;
                    break;
                default:
                    break;
            }
            if (NewLinesBetweenMessages)
                realprompt += LLMSystem.NewLine;
            return realprompt;
        }

        public string FormatSingleMessage(SingleMessage message)
        {
            return FormatSinglePrompt(message.Role, message.User, message.Bot, message.Message);
        }

        public List<string> GetStoppingStrings(Character user, Character bot)
        {
            var res = new List<string>() { LLMSystem.NewLine + user.Name + ":", LLMSystem.NewLine + bot.Name + ":" };

            if (!string.IsNullOrEmpty(BotStart))
                res.Add(BotStart);
            if (!string.IsNullOrEmpty(BotEnd))
                res.Add(BotEnd);
            if (!string.IsNullOrEmpty(SystemStart))
                res.Add(SystemStart);
            if (!string.IsNullOrEmpty(SystemEnd))
                res.Add(SystemEnd);
            if (!string.IsNullOrEmpty(UserStart))
                res.Add(UserStart);
            if (!string.IsNullOrEmpty(UserEnd))
                res.Add(UserEnd);
            if (!string.IsNullOrEmpty(StopSequence))
                res.Add(StopSequence);

            // Remove duplicates from the list
            res = res.Distinct().ToList();

            return res;
        }
    }
}
