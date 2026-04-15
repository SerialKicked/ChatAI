using LetheAISharp;
using LetheAISharp.Agent;
using LetheAISharp.API;
using LetheAISharp.Files;
using LetheAISharp.GBNF;
using LetheAISharp.LLM;
using LetheAISharp.Moods;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using LetheChat.GBNF;

namespace LetheChat.AgentPlugins
{
    public class SessionMoodCheckParams(ChatSession session)
    {
        public ChatSession Session = session;
    }

    /// <summary>
    /// Represents an action that analyzes the mood of a session based on its transcript and context.
    /// </summary>
    /// <remarks>This action uses a language model to evaluate the mood and feelings expressed during a
    /// session. The analysis is returned as a dictionary mapping mood keys to <see cref="Modifier"/> values,
    /// built dynamically from the currently loaded moodlets. The action requires specific agent capabilities,
    /// including having a language model loaded and a backend with structured output functionalities.</remarks>
    public class SessionMoodCheckAction : IAgentAction<Dictionary<string, Modifier>?, SessionMoodCheckParams>
    {
        public string Id => "SessionMoodCheckAction";
        public HashSet<AgentActionRequirements> Requirements => [AgentActionRequirements.LLM, AgentActionRequirements.Grammar];

        public async Task<Dictionary<string, Modifier>?> Execute(SessionMoodCheckParams param, CancellationToken ct)
        {
            if (ct.IsCancellationRequested)
                return null;

            if (LLMEngine.Bot.Brain.Mood.MoodData.Count == 0)
                return null;

            var moodlsts = new Dictionary<string, IMoodlet>();
            foreach (var moodinfo in LLMEngine.Bot.Brain.Mood.MoodData)
            {
                if (MoodManager.Moodlets.TryGetValue(moodinfo.Key, out var moodlet))
                {
                    moodlsts[moodinfo.Key] = moodlet;
                }
            }

            var moodformat = new DynamicMoodAnalysis(moodlsts);
            var request = "Based on the transcript of the session, update your mood and feelings." + moodformat.GetQuery() + LLMEngine.NewLine + "The values are as follows:\n 0: High reduction\n 1: Small reduction\n 2: No change\n 3: Small increase\n 4: High increase \n\n Only answer with the JSON, nothing else.";

            var prompt = GetSystemPromt(param.Session, request);
            await prompt.SetStructuredOutput(moodformat).ConfigureAwait(false);
            var fullprompt = prompt.PromptToQuery(AuthorRole.Assistant, LLMEngine.Sampler.Temperature, 1024);
            var response = await LLMEngine.SimpleQuery(fullprompt, ct).ConfigureAwait(false);
            response = response.RemoveThinkingBlocks();

            Dictionary<string, Modifier>? result = null;
            try
            {
                result = DynamicMoodAnalysis.ParseResponse(response);
            }
            catch (Exception ex)
            {
                LLMEngine.Logger?.LogError(ex, "Error parsing mood analysis response: {Response}", response);
            }
            return result;
        }

        private static IPromptBuilder GetSystemPromt(ChatSession param, string request)
        {
            var promptbuild = LLMEngine.GetPromptBuilder();

            var str = new StringBuilder();
            var tokenleft = LLMEngine.MaxContextLength - 1024; // leave some space for response + mix
            str.AppendLinuxLine("You are {{mchar}} and you are meant to reflect on this session with {{user}}.").AppendLinuxLine();

            str.AppendLinuxLine("## {{mchar}} (this is you)").AppendLinuxLine();
            str.AppendLine("{{mcharbio}}").AppendLinuxLine();
            str.AppendLinuxLine("## {{user}} (this is the user)").AppendLinuxLine();
            str.AppendLine("{{userbio}}").AppendLinuxLine();
            if (!string.IsNullOrEmpty(param.Content))
            {
                str.AppendLinuxLine($"## Session Summary: {param.Name}").AppendLinuxLine();
                str.AppendLine($"{param.Content}").AppendLinuxLine();
            }
            str.AppendLinuxLine("## Transcript").AppendLinuxLine();

            tokenleft -= promptbuild.GetTokenCount(AuthorRole.System, str.ToString());
            tokenleft -= promptbuild.GetTokenCount(AuthorRole.User, request);

            var transcript = param.GetRawDialogs(tokenleft, true, false, false, true);
            str.Append(transcript);

            promptbuild.AddMessage(new SingleMessage(AuthorRole.System, str.ToString()));
            promptbuild.AddMessage(new SingleMessage(AuthorRole.User, request));

            return promptbuild;
        }
    }
}
