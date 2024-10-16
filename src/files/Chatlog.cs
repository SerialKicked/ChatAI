using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.VisualBasic.ApplicationServices;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaifuAI.Memory;

namespace WaifuAI.Files
{
    public class SingleMessage(AuthorRole role, DateTime date, string mess, string chara, string user, bool isNewSession = false)
    {
        [JsonIgnore] public Guid Guid = Guid.NewGuid();
        public AuthorRole Role = role;
        public string Message = mess;
        public DateTime Date = date;
        public string CharID = chara;
        public string UserID = user;
        public bool IsNewSession = isNewSession;
        public float[] Embedding = [];
    }

    public class ChatSession
    {
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<SingleMessage> Messages { get; set; } = [];
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
                "Give a title to the summary above. This title should be a single sentence. Write only the title.";
            var msg = LLMSystem.Instruct.FormatSinglePrompt(AuthorRole.System, LLMSystem.User, LLMSystem.Bot, msgtxt);
            var tokencount = LLMSystem.GetTokenCount(msg);
            tokencount += LLMSystem.GetTokenCount(LLMSystem.Instruct.GetResponseStart(LLMSystem.Bot));
            var res = msg + LLMSystem.Instruct.GetResponseStart(LLMSystem.Bot);

            var llmparams = LLMSystem.Sampler.GetCopy();
            llmparams.Prompt = res;
            llmparams.Max_length = 350;
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
                        text = LLMSystem.NewLine + "*" + msg.Message.Trim() + "*" + LLMSystem.NewLine;
                        break;
                    case AuthorRole.User:
                        {
                            var sel = DataFiles.Characters.TryGetValue(msg.UserID, out var found) ? found : LLMSystem.User;
                            text = "**"+sel.Name+":** " + msg.Message.Trim().Replace(LLMSystem.NewLine, " ") + LLMSystem.NewLine;
                        }
                        break;
                    case AuthorRole.Assistant:
                        {
                            var sel = DataFiles.Characters.TryGetValue(msg.CharID, out var foundbot) ? foundbot : LLMSystem.Bot;
                            text = "**" + sel.Name + ":** " + msg.Message.Trim().Replace(LLMSystem.NewLine, " ") + LLMSystem.NewLine;
                        }
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

        public string GetFormatedDialogs(int maxTokens, ref int currentDepth, List<WorldEntry>? memories)
        {
            var sb = new StringBuilder();
            var totaltks = maxTokens;
            var mems = memories?.FindAll(e => e.Position == WEPosition.Chat) ?? new List<WorldEntry>();
            var entrydepth = currentDepth;

            void CheckAndAddMemories()
            {
                var selmem = mems.FindAll(e => e.PositionIndex == entrydepth);
                if (selmem.Count > 0)
                {
                    var totalmessage = "# Relevant Information from memory: " + LLMSystem.NewLine;
                    foreach (var item in selmem)
                    {
                        totalmessage += item.Message + LLMSystem.NewLine;
                    }
                    var formattedmemory = LLMSystem.Instruct.FormatSinglePrompt(AuthorRole.System, LLMSystem.User, LLMSystem.Bot, totalmessage);
                    var tksmem = LLMSystem.GetTokenCount(formattedmemory);
                    if (tksmem <= totaltks)
                    {
                        totaltks -= tksmem;
                        sb.Insert(0, formattedmemory);
                    }
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

        public string GetRawSummary()
        {
            var sb = new StringBuilder();
            sb.Append("# Previous Chat Summary");
            sb.Append("## Duration: " + StartTime.DayOfWeek.ToString() + ", the "+ StartTime.ToShortDateString() + "at " + StartTime.ToShortTimeString() + 
                " to " + EndTime.DayOfWeek.ToString() + ", the " + EndTime.ToShortDateString() + "at " + EndTime.ToShortTimeString() + LLMSystem.NewLine);
            sb.Append("## Message Count: " + Messages.Count.ToString() + LLMSystem.NewLine);
            sb.Append("## Title: " + Title + LLMSystem.NewLine);
            sb.Append("## Summary: " + LLMSystem.NewLine + Summary + LLMSystem.NewLine);
            return sb.ToString();
        }

        public string GetFormatedSummary()
        {
            return LLMSystem.Instruct.FormatSingleMessage(new SingleMessage(AuthorRole.System, DateTime.Now, GetRawSummary(), LLMSystem.Bot.Name, LLMSystem.User.Name));
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

        public string GetFormatedDialogs(int maxTokens, bool useSessionSystem, List<WorldEntry>? memories)
        {
            var sb = new StringBuilder();
            var tokensleft = maxTokens;
            var messagelist = new List<SingleMessage>(Messages);
            var mems = memories?.FindAll(e => e.Position == WEPosition.Chat) ?? [];
            var entrydepth = 0;

            void CheckAndAddMemories()
            {
                var selmem = mems.FindAll(e => e.PositionIndex == entrydepth);
                if (selmem.Count > 0)
                {
                    var totalmessage = "# Relevant Information from memory:" + LLMSystem.NewLine;
                    foreach (var item in selmem)
                    {
                        totalmessage += item.Message + LLMSystem.NewLine;
                    }
                    var formattedmemory = LLMSystem.Instruct.FormatSinglePrompt(AuthorRole.System, LLMSystem.User, LLMSystem.Bot, totalmessage);
                    var tksmem = LLMSystem.GetTokenCount(formattedmemory);
                    if (tksmem <= tokensleft)
                    {
                        tokensleft -= tksmem;
                        sb.Insert(0, formattedmemory);
                    }
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
                    return sb.ToString();
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
                    // If all session can fit, or if that session's end is less than 2 days ago
                    if (sessiontokencount <= tokensleft || ((DateTime.Now - session.EndTime) < new TimeSpan(2, 0, 0, 0)))
                    {
                        sb.Insert(0, session.GetFormatedDialogs(tokensleft, ref entrydepth, mems));
                    }
                    else
                    {
                        // If summary can fit, add it.
                        if (summarytokencount <= tokensleft)
                        {
                            tokensleft -= summarytokencount;
                            if (tokensleft <= 0)
                                return sb.ToString();
                            sb.Insert(0, session.GetFormatedSummary());
                            CheckAndAddMemories();
                            entrydepth++;
                        }
                        sb.Insert(0, session.GetFormatedDialogs(tokensleft, ref entrydepth, mems));
                    }
                    // check status
                    var currenttokens = LLMSystem.GetTokenCount(sb.ToString());
                    tokensleft = maxTokens - currenttokens;
                    if (tokensleft <= 0)
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

        public SingleMessage? GetMessageByID(Guid id) => Messages.FirstOrDefault(m => m.Guid == id);

        public SingleMessage LogMessage(AuthorRole role, string msg, Character user, Character bot)
        {
            var single = new SingleMessage(role, DateTime.Now, msg, bot.UniqueName, user.UniqueName, false);
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
            foreach (var item in Messages)
            {
                item.Embedding = [];
            }
        }

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
            return session;
        }

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
            session.EndTime = session.Messages.Last().Date;
            session.Summary = await session.GenerateNewSummary();
            session.Title = await session.GenerateNewTitle(session.Summary);
            return session;
        }

        public async Task RewriteAllSessions()
        {
            foreach (var item in Sessions)
            {
                await UpdateSession(item);
            }
        }

        public async Task StartNewChatSession(bool archivePreviousSession = true)
        {
            if (Messages.Count == 0)
                return;
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

        public void DivideChatIntoSessions()
        {
            if (Messages.Count == 0)
                return;
            Sessions.Clear();
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
                var validinitmessage = msg.Role == AuthorRole.User && (
                    Regex.IsMatch(msg.Message, pattern) || 
                    msg.Message.StartsWith("Hello ") || msg.Message.StartsWith("Hi!") || 
                    msg.Message.StartsWith("*"+LLMSystem.User.Name+" comes back ") || msg.Message.StartsWith("*" + LLMSystem.User.Name + " logged in.") ||
                    msg.Message.StartsWith("*We're a day later") || msg.Message.StartsWith("*We're a week"));
                // Minimum session length should be about 30 messages
                if (sessionmsgcount > 30 && (timespan.TotalDays > 1 || validinitmessage))
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
    }
}
