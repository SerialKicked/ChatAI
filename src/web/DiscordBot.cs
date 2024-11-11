using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Browser.Dom;
using Discord;
using Discord.WebSocket;
using Markdig.Helpers;
using Parlot.Fluent;
using WaifuAI.Files;
using WaifuAI.Memory;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace WaifuAI.Web
{
    internal class DiscordBot
    {
        private readonly DiscordSocketClient _client;
        private string PersonaID = "EsKaBoT";
        private string SysPromptID = "Discord";
        private HashSet<ulong> AdminID = [];

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

        public async Task RunBotAsync()
        {
            _client.Log += Log;
            // open D:\token.txt and read token from there
            var token = File.ReadAllText("d:\\discordtoken.txt");
            AdminID = new();
            AdminID.Add(331764911988539402);
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED);

            _client.MessageReceived -= MessageReceived;
            _client.MessageReceived += MessageReceived;
            //_client.SlashCommandExecuted -= _client_SlashCommandExecuted;
            //_client.SlashCommandExecuted += _client_SlashCommandExecuted;
            await Task.Delay(-1);
        }

        private async Task _client_SlashCommandExecuted(SocketSlashCommand arg)
        {
            await arg.Channel.SendMessageAsync(arg.ToString());
        }

        public async Task KillBot()
        {
            //_client.SlashCommandExecuted -= _client_SlashCommandExecuted;
            _client.MessageReceived -= MessageReceived;
            await _client.StopAsync();
            SetThreadExecutionState(ES_CONTINUOUS);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task<string> QueryLLM_Scan(IEnumerable<IMessage> contextMessages, string goal = "")
        {
            LLMSystem.NamesInPromptOverride = false;
            var SysPrompt = DataFiles.SysPrompts[SysPromptID];
            var Bot = DataFiles.Characters[PersonaID];
            var msgtxt = new StringBuilder();
            msgtxt.AppendLinuxLine("You are an automated system designed to identify bug reports and feature requests in discord chatlogs.");
            msgtxt.AppendLinuxLine();
            msgtxt.AppendLinuxLine("# Discord Chat:");
            msgtxt.AppendLinuxLine();
            for (int i = contextMessages.Count() - 1; i >= 2; i--)
            {
                var contextMessage = contextMessages.ElementAt(i);
                if (!string.IsNullOrWhiteSpace(contextMessage.Content))
                {
                    var contentmsg = contextMessage.Content.Trim('"');
                    var guildUser = contextMessage.Author as SocketGuildUser;
                    var localname = guildUser?.Nickname ?? contextMessage.Author.Username;
                    msgtxt.AppendLinuxLine($"{localname}: {contentmsg}");
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
            var llmparams = LLMSystem.Sampler.GetCopy();
            llmparams.Prompt = msg;
            llmparams.Max_length = LLMSystem.MaxReplyLength;
            llmparams.Max_context_length = LLMSystem.MaxContextLength;
            llmparams.Temperature = 0.5;
            return LLMSystem.SimpleQuery(llmparams);
        }

        private Task<string> QueryLLM_Chat(string username, string message, IEnumerable<IMessage> contextMessages)
        {
            LLMSystem.NamesInPromptOverride = true;
            var SysPrompt = DataFiles.SysPrompts[SysPromptID];
            var Bot = DataFiles.Characters[PersonaID];

            var rawprompt = new StringBuilder(SysPrompt.GetSystemPromptRaw(Bot) + LLMSystem.NewLine + "Don't use quotations marks in your responses.");
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

            rawprompt.AppendLinuxLine().AppendLinuxLine(SysPrompt.CategorySeparator + " Chat History");
            for (int i = contextMessages.Count() - 1 ; i >= 2; i--)
            {
                var contextMessage = contextMessages.ElementAt(i);
                if (!string.IsNullOrWhiteSpace(contextMessage.Content))
                {
                    var contentmsg = contextMessage.Content.Trim('"');
                    // trim leading and ending quote if present from contentmsg

                    var guildUser = contextMessage.Author as SocketGuildUser;
                    var localname = guildUser?.Nickname ?? contextMessage.Author.Username;
                    rawprompt.AppendLinuxLine($"{localname}: {contentmsg}");

                }
            }

            var msg = LLMSystem.Instruct.FormatSinglePromptNoUserInfo(AuthorRole.SysPrompt, username, Bot, rawprompt.ToString());
            msg += LLMSystem.Instruct.FormatSinglePromptNoUserInfo(AuthorRole.User, username, Bot, message);
            msg += LLMSystem.Instruct.GetResponseStart(Bot);

            var llmparams = LLMSystem.Sampler.GetCopy();
            llmparams.Prompt = msg;
            llmparams.Max_length = LLMSystem.MaxReplyLength;
            llmparams.Temperature = LLMSystem.ForceTemperature;
            llmparams.Max_context_length = LLMSystem.MaxContextLength;

            return LLMSystem.SimpleQuery(llmparams);
        }

        private async Task HandleBotResponse(SocketMessage message)
        {
            var text = message.Content;
            var question = text;
            if (message.Reference == null)
            {
                var parts = text.Split(' ');
                if (parts.Length < 2)
                {
                    await message.Channel.SendMessageAsync("Usage: !ask <question>");
                    return;
                }
                question = string.Join(' ', parts.Skip(1));
            }
            else
            {
                var referencedMessage = await message.Channel.GetMessageAsync(message.Reference.MessageId.Value);
                if (referencedMessage != null)
                {
                    question = "> In Reponse to your post: " + referencedMessage.Content.RemoveNewLines().CleanupAndTrim() + LLMSystem.NewLine + question;
                }

            }

            List<string> WaitMsg = [
                "*I am thinking very hard about your query.* 🧠⚙️",
                    "*That's a tough one! Let me think...* 🧠⚙️",
                    "*Beep Boop. I'm thinking!* 🧠⚙️"
                ];
            // retrieve random wait message
            var waitMessage = await message.Channel.SendMessageAsync(WaitMsg[LLMSystem.RNG.Next(WaitMsg.Count)]);

            var channel = message.Channel as ITextChannel;
            var messages = await channel!.GetMessagesAsync(40).FlattenAsync();

            var guildUser = message.Author as SocketGuildUser;
            var username = guildUser?.Nickname ?? message.Author.Username;

            var response = await QueryLLM_Chat(username, question, messages);

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
                if (character.Name != _client.CurrentUser.Username)
                {
                    await _client.CurrentUser.ModifyAsync(user => user.Username = character.Name);
                }
                await message.Channel.SendMessageAsync($"Bot persona switched to {character.Name}");

            }
            else
            {
                await message.Channel.SendMessageAsync($"Character ID {newName} not found.");
            }
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.IsBot) 
                return;

            var text = message.Content;

            if (text == "!myID")
            {
                await message.Channel.SendMessageAsync(message.Author.Id.ToString());
            }
            else if (text.StartsWith("!switch") && AdminID.Contains(message.Author.Id))
            {
                await SwitchBot(message);
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
            else if (text == "!atc")
            {
                await message.Channel.SendMessageAsync("After The Collapse is the best game ever." + Environment.NewLine + "https://store.steampowered.com/app/727570/After_the_Collapse/");
            }
            else if (text.StartsWith("!ask") || 
                    (text.StartsWith("<@") && message.MentionedUsers.Any(user => user.Id == _client.CurrentUser.Id)) || 
                     message.Reference != null)
            {
                if (message.Reference != null)
                {
                    var referencedMessage = await message.Channel.GetMessageAsync(message.Reference.MessageId.Value);
                    if (referencedMessage.Author.Id != _client.CurrentUser.Id)
                        return;
                }
                await HandleBotResponse(message);
            }
        }
    }
}
