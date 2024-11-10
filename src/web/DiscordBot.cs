using System;
using System.Collections.Generic;
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

namespace WaifuAI.Web
{
    internal class DiscordBot
    {
        private readonly DiscordSocketClient _client;
        private const string PersonaID = "EsKaBoT";
        private const string SysPromptID = "Discord";

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
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED);

            _client.MessageReceived -= MessageReceived;
            _client.MessageReceived += MessageReceived;
            await Task.Delay(-1);
        }

        public async Task KillBot()
        {
            _client.MessageReceived -= MessageReceived;
            await _client.StopAsync();
            SetThreadExecutionState(ES_CONTINUOUS);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task<string> QueryLLM(string username, string message, IEnumerable<IMessage> contextMessages)
        {
            LLMSystem.NamesInPromptOverride = true;
            var SysPrompt = DataFiles.SysPrompts[SysPromptID];
            var Bot = DataFiles.Characters[PersonaID];

            var rawprompt = new StringBuilder(SysPrompt.GetSystemPromptRaw(Bot));
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
                    var guildUser = contextMessage.Author as SocketGuildUser;
                    var localname = guildUser?.Nickname ?? contextMessage.Author.Username;
                    rawprompt.AppendLinuxLine($"{localname}: {contextMessage.Content}");

                }
            }

            var msg = LLMSystem.Instruct.FormatSinglePromptNoUserInfo(AuthorRole.SysPrompt, username, Bot, rawprompt.ToString());
            msg += LLMSystem.Instruct.FormatSinglePromptNoUserInfo(AuthorRole.User, username, Bot, message);
            msg += LLMSystem.Instruct.GetResponseStart(Bot);

            var llmparams = LLMSystem.Sampler.GetCopy();
            llmparams.Prompt = msg;
            llmparams.Max_length = 450;
            llmparams.Max_context_length = LLMSystem.MaxContextLength;

            return LLMSystem.SimpleQuery(llmparams);
        }


        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.IsBot) 
                return;

            var text = message.Content;

            if (text == "!ping")
            {
                await message.Channel.SendMessageAsync("Pong!");
            }
            else if (text == "!atc")
            {
                await message.Channel.SendMessageAsync("After The Collapse is the best game ever.");
            }
            else if (text.StartsWith("!ask") || (text.StartsWith("<@") && message.MentionedUsers.Any(user => user.Id == _client.CurrentUser.Id)))
            {
                if (LLMSystem.Status != SystemStatus.Ready)
                {
                    await message.Channel.SendMessageAsync("Sorry, I'm a bit busy right now.");
                    return;
                }
                var parts = text.Split(' ');
                if (parts.Length < 2)
                {
                    await message.Channel.SendMessageAsync("Usage: !ask <question>");
                    return;
                }
                var question = string.Join(' ', parts.Skip(1));

                var waitMessage = await message.Channel.SendMessageAsync("*I am thinking very hard about your query.* 🧠⚙️");

                var channel = message.Channel as ITextChannel;
                var messages = await channel!.GetMessagesAsync(20).FlattenAsync();

                var guildUser = message.Author as SocketGuildUser;
                var username = guildUser?.Nickname ?? message.Author.Username;

                var response = await QueryLLM(username, question, messages);
                await waitMessage.ModifyAsync(msg => msg.Content = response);
                
                //await message.Channel.SendMessageAsync($"You asked: {question}");
            }
        }
    }
}
