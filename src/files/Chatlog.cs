using Microsoft.VisualBasic.ApplicationServices;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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

        public string GenerateSummary()
        {
            var msgtxt = "Disable roleplay. Write a full summary of the exchange between {{user}} and {{char}}. Focus on the important details.";
            if (Messages.Count > 50)
            {
                msgtxt += " The summary should be between 2 and 4 paragraphs.";
            };
            var saveAddNames = LLMChatManager.Instruct.AddNamesToPrompt;
            LLMChatManager.Instruct.AddNamesToPrompt = false;
            var msg = LLMChatManager.Instruct.FormatSinglePrompt(AuthorRole.System, LLMChatManager.User, LLMChatManager.Bot, msgtxt);
            var tokencount = LLMChatManager.GetTokenCount(msg);
            var rawprompt = new StringBuilder(LLMChatManager.RawSystemPrompt(LLMChatManager.User, LLMChatManager.Bot));
            var sysprompt = LLMChatManager.Instruct.FormatSinglePrompt(AuthorRole.System, LLMChatManager.User, LLMChatManager.Bot, rawprompt.ToString());

            tokencount += LLMChatManager.GetTokenCount(sysprompt);
            tokencount += LLMChatManager.GetTokenCount(LLMChatManager.Instruct.GetResponseStart(LLMChatManager.Bot));
            var availtokens = (int)(LLMChatManager.MaxContextLength) - tokencount - 1400;
            var history = GetFormatedDialogs(availtokens);
            var res = sysprompt + LLMChatManager.NewLine + history + msg + LLMChatManager.Instruct.GetResponseStart(LLMChatManager.Bot);

            var llmparams = LLMChatManager.Sampler.GetCopy();
            llmparams.Prompt = res;
            llmparams.Max_length = 1400;
            llmparams.Grammar = string.Empty;
            llmparams.Temperature = 0.5f;
            var result = LLMChatManager.Client.GenerateAsync(llmparams).GetAwaiter().GetResult();
            string finalstr = string.Empty;
            foreach (var item in result.Results)
            {
                finalstr += item.Text;
            }
            LLMChatManager.Instruct.AddNamesToPrompt = saveAddNames;
            return finalstr.Trim();
        }

        public string GenerateTitle(string sum)
        {
            var saveAddNames = LLMChatManager.Instruct.AddNamesToPrompt;
            LLMChatManager.Instruct.AddNamesToPrompt = false;
            var msgtxt = "You are an automated system designed to give titles to summaries."+ LLMChatManager.NewLine + 
                LLMChatManager.NewLine + 
                "# Summary:" + LLMChatManager.NewLine +
                sum + LLMChatManager.NewLine + 
                LLMChatManager.NewLine + 
                "# Instruction:" + LLMChatManager.NewLine + 
                "Give a title to the summary above. This title should be a single sentence. Write only the title.";
            var msg = LLMChatManager.Instruct.FormatSinglePrompt(AuthorRole.System, LLMChatManager.User, LLMChatManager.Bot, msgtxt);
            var tokencount = LLMChatManager.GetTokenCount(msg);
            tokencount += LLMChatManager.GetTokenCount(LLMChatManager.Instruct.GetResponseStart(LLMChatManager.Bot));
            var res = msg + LLMChatManager.Instruct.GetResponseStart(LLMChatManager.Bot);

            var llmparams = LLMChatManager.Sampler.GetCopy();
            llmparams.Prompt = res;
            llmparams.Max_length = 350;
            var result = LLMChatManager.Client.GenerateAsync(llmparams).GetAwaiter().GetResult();
            string finalstr = string.Empty;
            foreach (var item in result.Results)
            {
                finalstr += item.Text;
            }
            // remove any " character from the finalstr
            finalstr = finalstr.Replace("\"", "").Trim();
            LLMChatManager.Instruct.AddNamesToPrompt = saveAddNames;
            return finalstr;
        }

        public string GetFormatedDialogs(int maxTokens = 0)
        {
            var sb = new StringBuilder();
            var totaltks = 0;
            for (int i = Messages.Count - 1; i >= 0; i--)
            {
                var msg = Messages[i];
                var res = LLMChatManager.Instruct.FormatSingleMessage(msg);
                var tks = LLMChatManager.GetTokenCount(res);
                if (maxTokens > 0 && (totaltks + tks > maxTokens))
                    return sb.ToString();
                totaltks += tks;
                sb.Insert(0, res);
            }
            return sb.ToString();
        }
    }

    public class Chatlog : BaseFile, IFile
    {
        public string Name { get; set; } = string.Empty;
        public readonly List<ChatSession> Sessions = [];
        public readonly List<SingleMessage> Messages = [];

        public EventHandler<SingleMessage>? OnMessageAdded;

        private void RaiseOnMessageAdded(SingleMessage message) => OnMessageAdded?.Invoke(this, message);

        public string GetFormatedDialogs(int maxTokens = 0)
        {
            var sb = new StringBuilder();
            var totaltks = 0;
            for (int i = Messages.Count - 1; i >= 0; i--)
            {
                var msg = Messages[i];
                var res = LLMChatManager.Instruct.FormatSingleMessage(msg);
                var tks = LLMChatManager.GetTokenCount(res);
                if (maxTokens > 0 && (totaltks + tks > maxTokens))
                    return sb.ToString();
                totaltks += tks;
                sb.Insert(0, res);
            }
            return sb.ToString();
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

        public ChatSession CurrentChatToSession()
        {
            var session = new ChatSession();
            session.Messages.AddRange(Messages);
            session.StartTime = Messages.First().Date;
            session.EndTime = Messages.Last().Date;
            session.Summary = session.GenerateSummary();
            session.Title = session.GenerateTitle(session.Summary);
            return session;
        }

        public void StartNewChatSession(bool archivePreviousSession = true)
        {
            if (Messages.Count == 0)
                return;
            if (archivePreviousSession && Messages.Count > 2)
            {
                var session = CurrentChatToSession();
                Sessions.Add(session);
            }
            Messages.Clear();
        }

    }
}
