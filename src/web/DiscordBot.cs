using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using WaifuAI.Files;
using WaifuAI.Memory;

namespace WaifuAI.Web
{
    internal class DiscordSettings : BaseFile
    {
        public string PersonaID { get; set; } = "EsKaBoT";
        public string SysPromptID { get; set; } = "Discord";
        public double ResponseChance { get; set; } = 0;
        public int ChatSize { get; set; } = 40;
        public HashSet<ulong> AdminID { get; set; } = [];

        public string BotToken { get; set; } = string.Empty;
    }

    internal class DiscordBot
    {
        public EventHandler<string>? OnFullPromptReady;
        private void RaiseOnFullPromptReady(string fullprompt) => OnFullPromptReady?.Invoke(null, fullprompt);

        private readonly DiscordSocketClient _client;
        private string PersonaID = "EsKaBoT";
        private string SysPromptID = "Discord";
        private double ResponseChance = 0;
        private int ChatSize = 40;
        private HashSet<ulong> AdminID = [];
        private string BotSecretToken = string.Empty;
        private bool Muted = false;

        // Import the SetThreadExecutionState function from kernel32.dll
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint SetThreadExecutionState(uint esFlags);

        // Constants for SetThreadExecutionState
        private const uint ES_CONTINUOUS = 0x80000000;
        private const uint ES_SYSTEM_REQUIRED = 0x00000001;
        private const uint ES_AWAYMODE_REQUIRED = 0x00000040;

        public DiscordBot()
        {
            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.MessageContent
            };
            _client = new DiscordSocketClient(config);
        }

        public void SaveSettings()
        {
            var settings = new DiscordSettings()
            {
                AdminID = AdminID,
                ChatSize = ChatSize,
                PersonaID = PersonaID,
                ResponseChance = ResponseChance,
                SysPromptID = SysPromptID,
                BotToken = BotSecretToken,
            };
            (settings as IFile).SaveToFile("discordsettings.json");
        }

        public void LoadSettings()
        {
            if (File.Exists("discordsettings.json"))
            {
                var str = File.ReadAllText("discordsettings.json");
                var settings = JsonConvert.DeserializeObject<DiscordSettings>(str);
                if (settings != null)
                {
                    AdminID = settings.AdminID;
                    ChatSize = settings.ChatSize;
                    PersonaID = settings.PersonaID;
                    ResponseChance = settings.ResponseChance;
                    SysPromptID = settings.SysPromptID;
                    BotSecretToken = settings.BotToken;
                }
            }
        }

        public async Task RunBotAsync()
        {
            LoadSettings();
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, BotSecretToken);
            await _client.StartAsync();

            SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED);

            _client.MessageReceived -= MessageReceived;
            _client.MessageReceived += MessageReceived;
            var Bot = DataFiles.Characters[PersonaID];
            await _client.SetGameAsync($"as {Bot.Name}");
            await Task.Delay(-1);
        }

        private async Task _client_SlashCommandExecuted(SocketSlashCommand arg)
        {
            await arg.Channel.SendMessageAsync(arg.ToString());
        }

        public async Task KillBot()
        {
            //_client.SlashCommandExecuted -= _client_SlashCommandExecuted;
            _client.Log -= Log;
            _client.MessageReceived -= MessageReceived;
            await _client.StopAsync();
            SetThreadExecutionState(ES_CONTINUOUS);
            SaveSettings();
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task<string> ReplaceMentionsWithUsernames(string text, IReadOnlyCollection<ulong> mentionedUserIds)
        {
            foreach (var userId in mentionedUserIds)
            {
                var user = await _client.GetUserAsync(userId);
                var guildUser = user as SocketGuildUser;
                var localname = guildUser?.Nickname ?? user.Username;
                if (user != null)
                {
                    text = text.Replace($"<@{userId}>", $"@{user.Username}");
                }
            }
            return text;
        }

        private string ReplaceMentionsWithUsernames(string text, IReadOnlyCollection<SocketUser> mentionedUsers)
        {
            foreach (var user in mentionedUsers)
            {
                text = text.Replace($"<@{user.Id}>", $"@{user.Username}");
            }
            return text;
        }

        private async Task<string> QueryLLM_Scan(IEnumerable<IMessage> contextMessages, string goal = "")
        {
            LLMSystem.NamesInPromptOverride = false;
            var SysPrompt = DataFiles.SysPrompts[SysPromptID];
            var Bot = DataFiles.Characters[PersonaID].Copy<Character>()!;
            Bot.Name = _client.CurrentUser.Username;
            var msgtxt = new StringBuilder();
            msgtxt.AppendLinuxLine("You are an automated system designed to analyze discord chatlogs and follow instructions.");
            msgtxt.AppendLinuxLine();
            msgtxt.AppendLinuxLine("# Discord Chatlogs:");
            msgtxt.AppendLinuxLine();
            for (int i = contextMessages.Count() - 1; i >= 2; i--)
            {
                var contextMessage = contextMessages.ElementAt(i);
                if (!string.IsNullOrWhiteSpace(contextMessage.Content))
                {
                    var contentmsg = contextMessage.Content.Trim('"');
                    contentmsg = await ReplaceMentionsWithUsernames(contentmsg, contextMessage.MentionedUserIds);
                    var guildUser = contextMessage.Author as SocketGuildUser;
                    var localname = guildUser?.Nickname ?? contextMessage.Author.Username;
                    contentmsg = contentmsg.ReplaceDiscordEmojis();
                    msgtxt.AppendLinuxLine($"{localname}: {contentmsg.RemoveNewLines().CleanupAndTrim()}").AppendLinuxLine();
                }
            }
            msgtxt.AppendLinuxLine();
            msgtxt.AppendLinuxLine("# Instruction:");
            if (string.IsNullOrWhiteSpace(goal))
            {
                msgtxt.Append("Write a list of all identified feature requests in the chatlog above. Then, write a list of all bug reports in the chatlog above. If there's no such items in the chatlog, just say so.");
            }
            else
            {
                msgtxt.Append(goal);
            }
            var msg = LLMSystem.Instruct.FormatSinglePromptNoUserInfo(AuthorRole.System, "EsKa", Bot, msgtxt.ToString());
            msg += LLMSystem.Instruct.GetResponseStart(Bot);
            RaiseOnFullPromptReady(msg);
            var llmparams = LLMSystem.Sampler.GetCopy();
            llmparams.Prompt = msg;
            llmparams.Max_length = LLMSystem.MaxReplyLength;
            llmparams.Max_context_length = LLMSystem.MaxContextLength;
            llmparams.Temperature = 0.5;
            llmparams.Grammar = string.Empty;
            return await LLMSystem.SimpleQuery(llmparams);
        }

        private async Task<string> QueryLLM_Chat(string username, string message, IEnumerable<IMessage> contextMessages)
        {
            LLMSystem.NamesInPromptOverride = false;
            var SysPrompt = DataFiles.SysPrompts[SysPromptID];
            var Bot = DataFiles.Characters[PersonaID].Copy<Character>()!;
            Bot.Name = _client.CurrentUser.Username;

            var rawprompt = new StringBuilder(SysPrompt.GetSystemPromptRaw(Bot) + LLMSystem.NewLine + " - Don't use quotations marks in your responses." + LLMSystem.NewLine + " - Only speak for yourself, don't speak for other people.");
            List<WorldEntry> _currentWorldEntries = [];

            if (Bot.MyWorlds.Count > 0)
            {
                _currentWorldEntries = [];
                foreach (var world in Bot.MyWorlds)
                {
                    _currentWorldEntries.AddRange(world.FindEntries(message));
                }
                if (_currentWorldEntries.Count > 0)
                {
                    rawprompt.AppendLinuxLine().AppendLinuxLine(SysPrompt.WorldInfoTitle);
                    foreach (var item in _currentWorldEntries)
                        rawprompt.AppendLinuxLine(item.Message);
                }
            }

            // Look for the first message containing !switch
            var mylist = new List<IMessage>(contextMessages);
            var id = mylist.FindIndex(e => e.Content.StartsWith("!switch") && AdminID.Contains(e.Author.Id));


            rawprompt.AppendLinuxLine().AppendLinuxLine(SysPrompt.CategorySeparator + " Chat History");
            for (int i = contextMessages.Count() - 1 ; i >= 2; i--)
            {
                if (i > id && id != -1)
                    continue;
                var contextMessage = contextMessages.ElementAt(i);
                if (!string.IsNullOrWhiteSpace(contextMessage.Content))
                {
                    var contentmsg = contextMessage.Content.Trim('"');
                    contentmsg = await ReplaceMentionsWithUsernames(contentmsg, contextMessage.MentionedUserIds);
                    var guildUser = contextMessage.Author as SocketGuildUser;
                    var localname = guildUser?.Nickname ?? contextMessage.Author.Username;
                    contentmsg = contentmsg.ReplaceDiscordEmojis();
                    rawprompt.AppendLinuxLine($"{localname}: {contentmsg.RemoveNewLines().CleanupAndTrim()}").AppendLinuxLine();

                }
            }

            var msg = LLMSystem.Instruct.FormatSinglePromptNoUserInfo(AuthorRole.SysPrompt, username, Bot, rawprompt.ToString());
            msg += LLMSystem.Instruct.FormatSinglePromptNoUserInfo(AuthorRole.User, username, Bot, message.ReplaceDiscordEmojis());
            msg += LLMSystem.Instruct.GetResponseStart(Bot) + Bot.Name+":";
            RaiseOnFullPromptReady(msg);

            var llmparams = LLMSystem.Sampler.GetCopy();
            llmparams.Prompt = msg;
            llmparams.Max_length = LLMSystem.MaxReplyLength;
            llmparams.Temperature = LLMSystem.ForceTemperature;
            llmparams.Max_context_length = LLMSystem.MaxContextLength;

            return await LLMSystem.SimpleQuery(llmparams);
        }

        public static string FilterLLMResponse(string response)
        {
            // Define the regex pattern to match "\nSomeUserName: " and everything that follows
            string pattern = @"\n\n\w+: .*";
            // Use Regex.Replace to remove the matched pattern
            string result = Regex.Replace(response, pattern, string.Empty, RegexOptions.Singleline);
            return result;
        }

        private async Task HandleBotResponse(SocketMessage message)
        {
            var text =message.Content;
            var Bot = DataFiles.Characters[PersonaID].Copy<Character>()!;
            Bot.Name = _client.CurrentUser.Username;
            var guildUser = message.Author as SocketGuildUser;
            var username = guildUser?.Nickname ?? message.Author.Username;
            var question = $"{username}: {text}";
            if (message.Reference == null)
            {
                if (text.StartsWith("!") || text.StartsWith("<@"))
                {
                    var parts = text.Split(' ');
                    if (parts.Length < 2)
                    {
                        await message.Channel.SendMessageAsync("Usage: !ask <question>");
                        return;
                    }
                    question = $"{username}: {string.Join(' ', parts.Skip(1))}";
                }
            }
            else
            {
                var referencedMessage = await message.Channel.GetMessageAsync(message.Reference.MessageId.Value);

                var refgUser = referencedMessage.Author as SocketGuildUser;
                var localname = refgUser?.Nickname ?? referencedMessage.Author.Username;
                if (referencedMessage != null)
                {
                    question = $"<QUOTE>{localname}: {referencedMessage.Content.RemoveNewLines().CleanupAndTrim()}</QUOTE>" + LLMSystem.NewLine + $"{username}: {text}";
                }
            }

            List<string> WaitMsg = [
                "*I am thinking very hard about your query.* 🧠⚙️",
                    "*That's a tough one! Let me think...* 🧠⚙️",
                    "*Beep Boop. I'm thinking!* 🧠⚙️"
                ];
            // retrieve random wait message
            question = ReplaceMentionsWithUsernames(question, message.MentionedUsers);
            var waitMessage = await message.Channel.SendMessageAsync(WaitMsg[LLMSystem.RNG.Next(WaitMsg.Count)]);

            var channel = message.Channel as ITextChannel;
            var messages = await channel!.GetMessagesAsync(ChatSize).FlattenAsync();


            var response = await QueryLLM_Chat(username, question, messages);
            response = FilterLLMResponse(response);
            // check the response for new line followed by "some name: " and cut from there


            List<string> totalmsgs = [];
            for (int i = 0; i < response.Length; i += 1980)
            {
                totalmsgs.Add(response.Substring(i, Math.Min(1980, response.Length - i)));
            }
            for (var i = 0; i < totalmsgs.Count; i++)
            {
                var chunk = totalmsgs[i];
                if (i == 0)
                {
                    await waitMessage.ModifyAsync(msg => msg.Content = chunk);
                }
                else
                {
                    await message.Channel.SendMessageAsync(chunk);
                }
            }
        }

        private async Task ScanCommand(SocketMessage message, string order = "")
        {
            var text = message.Content;
            var parts = text.Split(' ');
            if (parts.Length != 2 || !int.TryParse(parts[1], out var count))
            {
                await message.Channel.SendMessageAsync("Usage: !scan <count>");
                return;
            }
            var waitMessage = await message.Channel.SendMessageAsync("Sure boss! *Scanning...* 🧠⚙️");
            var channel = message.Channel as ITextChannel;
            var messages = await channel!.GetMessagesAsync(count).FlattenAsync();
            var response = await QueryLLM_Scan(messages, order);
            List<string> totalmsgs = [];
            for (int i = 0; i < response.Length; i += 1980)
            {
                totalmsgs.Add(response.Substring(i, Math.Min(1980, response.Length - i)));
            }
            for (var i = 0; i < totalmsgs.Count; i++)
            {
                var chunk = totalmsgs[i];
                if (i == 0)
                {
                    await waitMessage.ModifyAsync(msg => msg.Content = chunk);
                }
                else
                {
                    await message.Channel.SendMessageAsync(chunk);
                }
            }
        }

        private async Task SwitchBot(SocketMessage message)
        {
            var text = message.Content;
            var parts = text.Split(' ');
            if (parts.Length < 2)
            {
                await message.Channel.SendMessageAsync("Usage: !switch <char_ID>");
                return;
            }
            var newName = string.Join(' ', parts.Skip(1));
            if (DataFiles.Characters.TryGetValue(newName, out var character) && !character.IsUser)
            {
                LLMSystem.Bot = character;
                PersonaID = newName;
                await _client.SetGameAsync($"as {character.Name}");
                var welcome = character.GetWelcomeLine("Boss");
                if (!string.IsNullOrWhiteSpace(welcome))
                {
                    await message.Channel.SendMessageAsync($"*Bot persona switched to {character.Name}*" + LLMSystem.NewLine + welcome);
                }
                else
                {
                    await message.Channel.SendMessageAsync($"*Bot persona switched to {character.Name}*");
                }
                SaveSettings();
            }
            else
            {
                await message.Channel.SendMessageAsync($"Character ID {newName} not found.");
            }
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.IsBot || string.IsNullOrWhiteSpace(message.Content))
                return;

            var text = message.Content;
            var Bot = DataFiles.Characters.TryGetValue(PersonaID, out var p) ? p : DataFiles.Characters["EsKaBoT"];

            if (text == "!myID")
            {
                await message.Channel.SendMessageAsync(message.Author.Id.ToString());
            }
            else if (text.StartsWith("!switch") && AdminID.Contains(message.Author.Id))
            {
                await SwitchBot(message);
            }
            else if (text.StartsWith("!chatlog") && AdminID.Contains(message.Author.Id))
            {
                var parts = text.Split(' ');
                if (parts.Length != 2 || !int.TryParse(parts[1], out var count))
                {
                    await message.Channel.SendMessageAsync("Usage: !chatlog <count>");
                    return;
                }
                await message.Channel.SendMessageAsync($"Sure thing, boss! I'll consider the last {count} messages now.");
                ChatSize = count;
                SaveSettings();
            }
            else if (text == "!dynamic" && AdminID.Contains(message.Author.Id))
            {
                ResponseChance = 0.1;
                SaveSettings();
                await message.Channel.SendMessageAsync("Sure thing, boss! I'll try not to be too spammy.");
            }
            else if (text == "!passive" && AdminID.Contains(message.Author.Id))
            {
                ResponseChance = 0;
                await message.Channel.SendMessageAsync("Okay, boss! No talking unless talked to. 🫡");
            }
            else if (text.StartsWith("!rename") && AdminID.Contains(message.Author.Id))
            {
                var parts = text.Split(' ');
                if (parts.Length < 2)
                {
                    await message.Channel.SendMessageAsync("Usage: !rename <new_name>");
                    return;
                }
                var newName = string.Join(' ', parts.Skip(1));
                await _client.CurrentUser.ModifyAsync(user => user.Username = newName);
                await message.Channel.SendMessageAsync($"Bot name changed to {newName}");
            }
            else if (text == "!beep")
            {
                await message.Channel.SendMessageAsync("boop!");
            }
            else if (text == "!mute" && AdminID.Contains(message.Author.Id))
            {
                await _client.SetGameAsync($"dead");
                await message.Channel.SendMessageAsync("🤫");
                Muted = true;
            }
            else if (text == "!talk" && AdminID.Contains(message.Author.Id))
            {
                await message.Channel.SendMessageAsync("Sure, boss!");
                await _client.SetGameAsync($"as {Bot.Name}");
                Muted = false;
            }
            else if (text.StartsWith("!scan") && AdminID.Contains(message.Author.Id))
            {
                await ScanCommand(message);
            }
            else if (text.StartsWith("!nice") && AdminID.Contains(message.Author.Id))
            {
                await ScanCommand(message, "Scan the chatlog, and look for the nicest user. State the reasons why. Don't include EsKa or YesMan in the results.");
            }
            else if (text.StartsWith("!offensive") && AdminID.Contains(message.Author.Id))
            {
                await ScanCommand(message, "Look for offensive, or possibly TOS breaking, language in the log above. Find the biggest offender, and state the reason why.");
            }
            else if (text.StartsWith("!summarize") && AdminID.Contains(message.Author.Id))
            {
                await ScanCommand(message, "Make a detailed summary of the chat in the log above.");
            }
            else if (text.StartsWith("!sum") && AdminID.Contains(message.Author.Id))
            {
                await ScanCommand(message, "Make a short summary of the chat in the log above. Don't use lists, just 1 or 2 paragraphs of text.");
            }
            else if (text == "!atc")
            {
                await message.Channel.SendMessageAsync("After The Collapse is the best game ever." + Environment.NewLine + "https://store.steampowered.com/app/727570/After_the_Collapse/");
            }
            else if (text.StartsWith("!ask") ||
                     (message.Channel.Name == "off-topic" && LLMSystem.RNG.NextDouble() < ResponseChance) ||
                     (text.StartsWith("<@") && message.MentionedUsers.Any(user => user.Id == _client.CurrentUser.Id)) || 
                     message.Reference != null)
            {
                if (message.Reference != null)
                {
                    var referencedMessage = await message.Channel.GetMessageAsync(message.Reference.MessageId.Value);
                    if (referencedMessage.Author.Id != _client.CurrentUser.Id)
                        return;
                }
                if (Muted && !AdminID.Contains(message.Author.Id))
                    return;
                await HandleBotResponse(message);
            }
        }
    }
}
