using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using WaifuAI.Memory;
using WaifuAI.Web;

namespace WaifuAI.Files
{
    public class SingleMessage(AuthorRole role, DateTime date, string mess, string chara, string user)
    {
        [JsonIgnore] public Guid Guid = Guid.NewGuid();
        public AuthorRole Role = role;
        public string Message = mess;
        public DateTime Date = date;
        public string CharID = chara;
        public string UserID = user;

        [JsonIgnore] public Character User => !string.IsNullOrEmpty(UserID) && DataFiles.Characters.TryGetValue(UserID, out var u) ? u : LLMSystem.User;
        [JsonIgnore] public Character Bot => !string.IsNullOrEmpty(CharID) && DataFiles.Characters.TryGetValue(CharID, out var c) ? c : LLMSystem.Bot;
        [JsonIgnore] public Character? Sender => Role == AuthorRole.User? User : Role == AuthorRole.Assistant ? Bot : null;
    }

    public class ChatSession
    {
        [JsonIgnore] public Guid Guid = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;

        public float[] EmbedTitle { get; set; } = [];
        public float[] EmbedSummary { get; set; } = [];
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<SingleMessage> Messages { get; set; } = [];
        /// <summary>
        /// If set to true, this memory will always be included in the prompt
        /// </summary>
        public bool Sticky { get; set; } = false;
        public TimeSpan Duration => EndTime - StartTime;

        public async Task<string> GenerateNewSummary()
        {

            var msgtxt = "You are an automated system designed to summarize chat sessions and stories." + LLMSystem.NewLine +
                LLMSystem.NewLine +
                "# Character Information:" + LLMSystem.NewLine +
                "## Name: {{char}}" + LLMSystem.NewLine +
                "{{charbio}}" + LLMSystem.NewLine +
                "## Name: {{user}}" + LLMSystem.NewLine +
                "{{userbio}}" + LLMSystem.NewLine + LLMSystem.NewLine +
                "# Chat Duration: " + LLMSystem.TimeSpanToHumanString(Duration) + LLMSystem.NewLine +
                "# Chat Session:" + LLMSystem.NewLine +
                "" + LLMSystem.NewLine +
                LLMSystem.NewLine +
                "# Instruction:" + LLMSystem.NewLine +
                "Write a full summary of the exchange between {{user}} and {{char}} shown above. The summary must be written from {{char}}'s perspective. Do not introduce the characters. Do not add a title, just write the summary directly. Focus on the important details.";
            if (Messages.Count > 50)
            {
                msgtxt += " The summary should be between 2 and 4 paragraphs.";
            };
            var msg = LLMSystem.Instruct.FormatSinglePrompt(AuthorRole.System, LLMSystem.User, LLMSystem.Bot, msgtxt);
            var tokencount = LLMSystem.GetTokenCount(msg);

            var availtokens = LLMSystem.MaxContextLength - tokencount - 1024;
            var docs = GetRawDialogs(availtokens, true);
            msgtxt = "You are an automated system designed to summarize chat sessions and stories." + LLMSystem.NewLine +
                LLMSystem.NewLine +
                "# Character Information:" + LLMSystem.NewLine +
                "## Name: {{char}}" + LLMSystem.NewLine +
                "{{charbio}}" + LLMSystem.NewLine +
                "## Name: {{user}}" + LLMSystem.NewLine +
                "{{userbio}}" + LLMSystem.NewLine + LLMSystem.NewLine +
                "# Chat Duration: " + LLMSystem.TimeSpanToHumanString(Duration) + LLMSystem.NewLine +
                "# Chat Session:" + LLMSystem.NewLine +
                docs + LLMSystem.NewLine +
                LLMSystem.NewLine +
                "# Instruction:" + LLMSystem.NewLine +
                "Write a full summary of the exchange between {{user}} and {{char}} shown above. The summary must be written from {{char}}'s perspective. Do not introduce the characters. Do not add a title, just write the summary directly. Focus on the important details.";

            var prompt = LLMSystem.Instruct.FormatSinglePrompt(AuthorRole.System, LLMSystem.User, LLMSystem.Bot, msgtxt);

            var llmparams = LLMSystem.Sampler.GetCopy();
            llmparams.Prompt = prompt;
            llmparams.Max_length = 1024;
            llmparams.Max_context_length = LLMSystem.MaxContextLength;
            llmparams.Grammar = string.Empty;
            llmparams.Temperature = 0.5f;
            var result = await LLMSystem.Client.GenerateAsync(llmparams);
            string finalstr = string.Empty;
            foreach (var item in result.Results)
            {
                finalstr += item.Text;
            }
            return finalstr.Trim();
        }

        public async Task<string> GenerateNewTitle(string sum)
        {
            var saveAddNames = LLMSystem.Instruct.AddNamesToPrompt;
            LLMSystem.Instruct.AddNamesToPrompt = false;
            var msgtxt = "You are an automated system designed to give titles to summaries."+ LLMSystem.NewLine + 
                LLMSystem.NewLine + 
                "# Summary:" + LLMSystem.NewLine +
                sum + LLMSystem.NewLine + 
                LLMSystem.NewLine + 
                "# Instruction:" + LLMSystem.NewLine + 
                "Give a title to the summary above. This title should be a single sentence. Write only the title, nothinh else.";
            var msg = LLMSystem.Instruct.FormatSinglePrompt(AuthorRole.System, LLMSystem.User, LLMSystem.Bot, msgtxt);
            var res = msg + LLMSystem.Instruct.GetResponseStart(LLMSystem.Bot);

            var llmparams = LLMSystem.Sampler.GetCopy();
            llmparams.Prompt = res;
            llmparams.Max_length = 350;
            llmparams.Temperature = 0.4f;
            llmparams.Max_context_length = LLMSystem.MaxContextLength;
            var result = await LLMSystem.Client.GenerateAsync(llmparams);
            string finalstr = string.Empty;
            foreach (var item in result.Results)
            {
                finalstr += item.Text;
            }
            // remove any " character from the finalstr
            finalstr = finalstr.Replace("\"", "").Trim();
            LLMSystem.Instruct.AddNamesToPrompt = saveAddNames;
            return finalstr;
        }

        public async Task GenerateEmbeds()
        {
            if (!RAGSystem.Enabled)
                return;
            EmbedTitle = await RAGSystem.EmbeddingText(Title);
            EmbedSummary = await RAGSystem.EmbeddingText(Summary);
        }

        public string GetRawDialogs(int maxTokens, bool ignoresystem)
        {
            var sb = new StringBuilder();
            var totaltks = maxTokens;

            for (int i = Messages.Count - 1; i >= 0; i--)
            {
                var msg = Messages[i];
                var text = string.Empty;
                switch (msg.Role)
                {
                    case AuthorRole.System:
                    case AuthorRole.SysPrompt:
                        if (ignoresystem)
                            continue;
                        text = msg.Message.StartsWith("*") ? LLMSystem.NewLine + msg.Message.Trim() + LLMSystem.NewLine : LLMSystem.NewLine + "*" + msg.Message.Trim() + "*" + LLMSystem.NewLine;
                        break;
                    case AuthorRole.User:
                    case AuthorRole.Assistant:
                        text = "**" + msg.Sender?.Name + ":** " + msg.Message.Trim().Replace(LLMSystem.NewLine, " ") + LLMSystem.NewLine;
                        break;
                }
                if (text == string.Empty)
                    continue;
                var tks = LLMSystem.GetTokenCount(text);
                totaltks -= tks;
                if (totaltks <= 0)
                    return sb.ToString();
                sb.Insert(0, text);
            }
            return sb.ToString();
        }

        public string GetFormatedDialogs(int maxTokens, ref int currentDepth, Dictionary<int, string>? memories)
        {
            var sb = new StringBuilder();
            var totaltks = maxTokens;
            var mems = memories ?? [];
            var entrydepth = currentDepth;

            void CheckAndAddMemories()
            {
                if (!mems.TryGetValue(entrydepth, out string? value) || string.IsNullOrEmpty(value))
                    return;
                var formattedmemory = LLMSystem.Instruct.FormatSinglePrompt(AuthorRole.System, LLMSystem.User, LLMSystem.Bot, value);
                var tksmem = LLMSystem.GetTokenCount(formattedmemory);
                if (tksmem <= totaltks)
                {
                    totaltks -= tksmem;
                    sb.Insert(0, formattedmemory);
                }
            }

            for (int i = Messages.Count - 1; i >= 0; i--)
            {
                var msg = Messages[i];
                var res = LLMSystem.Instruct.FormatSingleMessage(msg);
                var tks = LLMSystem.GetTokenCount(res);
                totaltks -= tks;
                if (totaltks <= 0)
                    return sb.ToString();
                sb.Insert(0, res);
                CheckAndAddMemories();
                entrydepth++;
                currentDepth = entrydepth;
            }
            return sb.ToString();
        }

        public string GetRawSummary(string title = "Chat Session")
        {
            var sb = new StringBuilder();
            sb.AppendLinuxLine("# "+ LLMSystem.ReplaceMacros(title));
            if (StartTime.Date == EndTime.Date)
                sb.AppendLinuxLine("## Date: " + StartTime.DayOfWeek.ToString() + " " + LLMSystem.DateToHumanString(StartTime));
            else
                sb.AppendLinuxLine("## From " + StartTime.DayOfWeek.ToString() + " " + LLMSystem.DateToHumanString(StartTime) + " to " + EndTime.DayOfWeek.ToString() + " " + LLMSystem.DateToHumanString(EndTime));
            sb.AppendLinuxLine("## Title: " + Title.Trim());
            sb.AppendLinuxLine("## Summary: " + LLMSystem.NewLine + Summary.Replace("\n\n"," ").Trim() + LLMSystem.NewLine);
            return sb.ToString();
        }

        public string GetRawMemory()
        {
            var sb = new StringBuilder();
            sb.AppendLinuxLine("# " + Title.Trim());
            if (StartTime.Date == EndTime.Date)
                sb.AppendLinuxLine("## Date: " + StartTime.DayOfWeek.ToString() + " " + LLMSystem.DateToHumanString(StartTime));
            else
                sb.AppendLinuxLine("## From " + StartTime.DayOfWeek.ToString() + " " + LLMSystem.DateToHumanString(StartTime) + " to " + EndTime.DayOfWeek.ToString() + " " + LLMSystem.DateToHumanString(EndTime));
            sb.AppendLinuxLine("## Memory: " + Summary.Replace("\n\n", " ").Trim());
            return sb.ToString();
        }

        public string GetFormatedSummary(string title = "Chat Session")
        {
            return LLMSystem.Instruct.FormatSingleMessage(new SingleMessage(AuthorRole.System, DateTime.Now, GetRawSummary(title), LLMSystem.Bot.UniqueName, LLMSystem.User.UniqueName));
        }

        public int GetFormatedSummaryTokenCount()
        {
            return LLMSystem.GetTokenCount(GetFormatedSummary());
        }
    }

    public class Chatlog : BaseFile, IFile
    {
        public string Name { get; set; } = string.Empty;
        public readonly List<ChatSession> Sessions = [];
        public readonly List<SingleMessage> Messages = [];

        public EventHandler<SingleMessage>? OnMessageAdded;

        private void RaiseOnMessageAdded(SingleMessage message) => OnMessageAdded?.Invoke(this, message);

        public string GetFormatedDialogs(int maxTokens, bool useSessionSystem, Dictionary<int, string>? memories)
        {
            var sb = new StringBuilder();
            var tokensleft = useSessionSystem ? maxTokens - LLMSystem.ReservedSessionTokens : maxTokens;
            var availSessionMemTokens = useSessionSystem ? LLMSystem.ReservedSessionTokens : 0;
            var messagelist = new List<SingleMessage>(Messages);
            var mems = memories ?? [];
            var entrydepth = 0;

            /// <summary> Insert WorldInfo memories into the chatlog </summary>
            void CheckAndAddMemories()
            {
                if (!mems.TryGetValue(entrydepth, out string? value) || string.IsNullOrEmpty(value))
                    return;
                var formattedmemory = LLMSystem.Instruct.FormatSinglePrompt(AuthorRole.System, LLMSystem.User, LLMSystem.Bot, value);
                var tksmem = LLMSystem.GetTokenCount(formattedmemory);
                if (tksmem <= tokensleft)
                {
                    tokensleft -= tksmem;
                    sb.Insert(0, formattedmemory);
                }
            }

            if (!useSessionSystem)
            {
                // add all messages together in the same list
                var newlist = new List<SingleMessage>();
                foreach (var item in Sessions)
                    newlist.AddRange(item.Messages);
                newlist.AddRange(Messages);
                messagelist = newlist;
            }

            for (int i = messagelist.Count - 1; i >= 0; i--)
            {
                var msg = messagelist[i];
                var res = LLMSystem.Instruct.FormatSingleMessage(msg);
                var tks = LLMSystem.GetTokenCount(res);
                tokensleft -= tks;
                if (tokensleft <= 0)
                    break;
                sb.Insert(0, res);
                // check if we need to add a memory
                CheckAndAddMemories();
                entrydepth++;
            }

            if (useSessionSystem)
            {
                for (int i = Sessions.Count - 1; i >= 0; i--)
                {
                    var session = Sessions[i];
                    var summarytokencount = session.GetFormatedSummaryTokenCount();
                    var sessiontokencount = GetTotalTokens(session.Messages);
                    // If all session can fit, or if that session's end is less than 7 days ago, or we have too few messages
                    if (sessiontokencount <= tokensleft || ((DateTime.Now - session.EndTime) < new TimeSpan(7, 0, 0, 0)))
                    {
                        var text = session.GetFormatedDialogs(tokensleft, ref entrydepth, mems);
                        if (string.IsNullOrEmpty(text))
                        {
                            availSessionMemTokens += tokensleft;
                            tokensleft = 0;
                        }
                        else
                            sb.Insert(0, text);
                    }
                    else
                    {
                        // If summary can fit, add it.
                        if (summarytokencount <= availSessionMemTokens)
                        {
                            availSessionMemTokens -= summarytokencount;
                            sb.Insert(0, session.GetFormatedSummary());
                            CheckAndAddMemories();
                            entrydepth++;
                        }
                        else
                        {
                            availSessionMemTokens = 0;
                        }
                    }
                    // check status
                    if (tokensleft > 0)
                    {
                        var currenttokens = LLMSystem.GetTokenCount(sb.ToString());
                        tokensleft = maxTokens - LLMSystem.ReservedSessionTokens - currenttokens;
                    }
                    if (tokensleft <= 0 && availSessionMemTokens <= 0)
                        return sb.ToString();
                }
            }
            return sb.ToString();
        }

        public static int GetTotalTokens(List<SingleMessage> messages)
        {
            var total = new StringBuilder();
            foreach (var item in messages)
            {
                total.Append(LLMSystem.Instruct.FormatSingleMessage(item));
            }
            return LLMSystem.GetTokenCount(total.ToString());
        }

        public ChatSession? GetSessionByID(Guid id) => Sessions.FirstOrDefault(s => s.Guid == id);

        public SingleMessage? GetMessageByID(Guid id) => Messages.FirstOrDefault(m => m.Guid == id);

        public SingleMessage LogMessage(AuthorRole role, string msg, Character user, Character bot)
        {
            var single = new SingleMessage(role, DateTime.Now, msg, bot.UniqueName, user.UniqueName);
            Messages.Add(single);
            RaiseOnMessageAdded(single);
            return single;
        }

        public SingleMessage LogMessage(SingleMessage single)
        {
            Messages.Add(single);
            RaiseOnMessageAdded(single);
            return single;
        }

        public void RemoveAt(int id) => Messages.RemoveAt(id);

        public bool RemoveLast()
        {
            if (Messages.Count > 0)
            {
                Messages.RemoveAt(Messages.Count - 1);
                return true;
            }
            return false;

        }

        public void ClearHistory() => Messages.Clear();

        public SingleMessage? LastMessage() => Messages.Count >= 1 ? Messages.Last() : null;

        public void RemoveEmbeds()
        {
            foreach (var item in Sessions)
            {
                item.EmbedSummary = [];
                item.EmbedTitle = [];
            }
        }

        /// <summary>
        /// Move the current active chat to the session list and generate title, summary, and embeds for it.
        /// </summary>
        /// <returns></returns>
        public async Task<ChatSession> CurrentChatToSession()
        {
            var session = new ChatSession();
            session.Messages.AddRange(Messages);
            session.StartTime = Messages.First().Date;
            // if the first message has a default date, try to find a message with a valid date
            if (session.StartTime == default)
            {
                foreach (var item in session.Messages)
                {
                    if (item.Date != default)
                    {
                        session.StartTime = item.Date;
                        break;
                    }
                }
            }
            session.EndTime = Messages.Last().Date;
            session.Summary = await session.GenerateNewSummary();
            session.Title = await session.GenerateNewTitle(session.Summary);
            await session.GenerateEmbeds();
            return session;
        }

        /// <summary>
        /// Generate title, summary and embeddings for the selected session. Also fixes date issues if any.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public async Task<ChatSession> UpdateSession(ChatSession session)
        {
            if (session.StartTime == default)
            {
                foreach (var item in session.Messages)
                {
                    if (item.Date != default)
                    {
                        session.StartTime = item.Date;
                        break;
                    }
                }
            }
            var previousmess = session.Messages.First();
            foreach (var item in session.Messages)
            {
                if (item.Date == default || item.Date.Year == 1)
                {
                    item.Date = previousmess.Date + new TimeSpan(0,1,0);
                    break;
                }
                previousmess = item;
            }

            session.EndTime = session.Messages.Last().Date;
            var sum = await session.GenerateNewSummary();
            session.Summary = sum;
            session.Title = await session.GenerateNewTitle(sum);
            await session.GenerateEmbeds();
            return session;
        }

        /// <summary>
        /// Generate title, summary and embeddings for all the sessions in the chatlog
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAllSessions()
        {
            foreach (var item in Sessions)
            {
                await UpdateSession(item);
            }
        }

        public async Task StartNewChatSession(bool archivePreviousSession = true)
        {
            if (archivePreviousSession && Messages.Count > 2)
            {
                var session = await CurrentChatToSession();
                Sessions.Add(session);
            }
            Messages.Clear();
            // Generate new system message about the new session
            var msgtxt = "*We're {{day}} the {{date}} at {{time}}.";
            if (Sessions.Count > 0)
            {
                var lastsession = Sessions.Last();
                var timespan = DateTime.Now - lastsession.EndTime;
                if (timespan.Days > 1)
                    msgtxt += " Your last chat was " + timespan.Days.ToString() + " days ago.";
                else if (timespan.Days == 1)
                    msgtxt += " The last chat was yesterday.";
                else if (timespan.Hours > 1)
                    msgtxt += " The last chat was " + timespan.Hours + " hours ago.";
                else
                    msgtxt += " The last chat was " + ((int)timespan.TotalMinutes).ToString() + " minutes ago.";
            }
            msgtxt += "*";
            LogMessage(AuthorRole.System, LLMSystem.ReplaceMacros(msgtxt, LLMSystem.User, LLMSystem.Bot), LLMSystem.User, LLMSystem.Bot); 
        }

        /// <summary>
        /// Divides a raw chatlog (likely imported from ST) into sessions using timestamps and specific messages to determine the start of a new session
        /// </summary>
        public void DivideChatIntoSessions()
        {
            if (Messages.Count == 0)
                return;
            Sessions.Clear();

            // Fix potential date problems
            var firstdate = default(DateTime);
            foreach (var item in Messages)
            {
                if (item.Date != default)
                {
                    firstdate = item.Date;
                    break;
                }
            }
            var previousmess = Messages.First();
            previousmess.Date = firstdate;
            foreach (var item in Messages)
            {
                if (item.Date == default || item.Date.Year == 1)
                {
                    item.Date = previousmess.Date + new TimeSpan(0, 0, 15);
                }
                previousmess = item;
            }
            // I want to check if msg.Message starts with "*We're " followed by a number to consider it as a new session
            string pattern = @"^\*We're \d+";
            // iterate through Messages, and divide them into sessions by checking the time between messages or the presence of a sentence starting by "*We're [number]" or a "Hello" message from user
            var currentsession = new ChatSession();
            var lastmsg = Messages.First();
            currentsession.StartTime = lastmsg.Date;
            currentsession.Messages.Add(lastmsg);
            var sessionmsgcount = 1;
            for (int i = 1; i < Messages.Count; i++)
            {
                var msg = Messages[i];
                var timespan = msg.Date - lastmsg.Date;
                var totaltimespan = msg.Date - currentsession.StartTime;
                var validinitmessage = (msg.Role == AuthorRole.User || msg.Role == AuthorRole.System) && (
                    Regex.IsMatch(msg.Message, pattern) ||
                    msg.Message.StartsWith("Hello ") || msg.Message.StartsWith("Hi!") || msg.Message.StartsWith("Hi ") ||
                    msg.Message.StartsWith("*" + LLMSystem.User.Name + " comes back ") || msg.Message.StartsWith("*" + LLMSystem.User.Name + " logged in.") || 
                    msg.Message.StartsWith("*A few days later") ||
                    msg.Message.StartsWith("*We're a day later") || msg.Message.StartsWith("*We're a week"));
                // Minimum session length should be about 30 messages
                if (sessionmsgcount > 35 && (timespan.TotalDays >= 1 || (totaltimespan.TotalDays > 3 && sessionmsgcount > 120) || validinitmessage))
                {
                    currentsession.EndTime = lastmsg.Date;
                    if (currentsession.Messages.Count > 0)
                        Sessions.Add(currentsession);
                    currentsession = new ChatSession();
                    sessionmsgcount = 0;
                    currentsession.StartTime = msg.Date;
                }
                currentsession.Messages.Add(msg);
                sessionmsgcount++;
                lastmsg = msg;
            }
            currentsession.EndTime = lastmsg.Date;
            if (currentsession.Messages.Count > 0)
                Sessions.Add(currentsession);
            Messages.Clear();
        }

        public (int tokens, TimeSpan duration) GetCurrentChatSessionInfo()
        {
            if (Messages.Count <= 1)
            {
                return (0, TimeSpan.Zero);
            }
            var sb = new StringBuilder();
            foreach (var message in Messages)
            {
                sb.Append(LLMSystem.Instruct.FormatSingleMessage(message));
            }
            var tokencount = LLMSystem.GetTokenCount(sb.ToString());
            var duration = Messages.Last().Date - Messages.First().Date;
            return (tokencount, duration);
        }


        public void SaveToFile(string pPath) 
        {
            var content = JsonConvert.SerializeObject(this);
            File.WriteAllText(pPath, content);
        }
    }
}
