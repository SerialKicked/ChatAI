using LetheAISharp;
using LetheAISharp.Agent;
using LetheAISharp.API;
using LetheAISharp.Files;
using LetheAISharp.GBNF;
using LetheAISharp.LLM;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using WaifuAI.Files;
using WaifuAI.GBNF;

namespace WaifuAI.AgentPlugins
{
    public class FindGroupNextAgentParams
    {
        public GroupChar Group { get; set; } = new GroupChar();
        public List<SingleMessage> Messages { get; set; } = [];
        public bool AllowFullAnalysis { get; set; } = true;
    }

    /// <summary>
    /// Represents an agent action that analyzes a group chat session to determine which participant should speak next.
    /// </summary>
    /// <remarks>This action uses recent chat history and participant information to select the next speaker,
    /// prioritizing those who have not spoken recently or at all. It requires both language model and grammar
    /// capabilities. The action is typically used in automated group conversation scenarios to facilitate fair and
    /// context-aware turn-taking.</remarks>
    public class FindGroupNextAgent : IAgentAction<Character?, FindGroupNextAgentParams>
    {
        public string Id => "FindGroupNextAgent";
        public HashSet<AgentActionRequirements> Requirements => [AgentActionRequirements.LLM, AgentActionRequirements.Grammar];
        public async Task<Character?> Execute(FindGroupNextAgentParams param, CancellationToken ct)
        {
            if (ct.IsCancellationRequested)
                return null;

            var savedKV = false;
            if (LLMEngine.Client!.SupportsStateSave)
            {
                savedKV = await LLMEngine.Client.SaveKVState(0);
                await Task.Delay(100, ct);
            }

            LLMEngine.NamesInPromptOverride = false;
            var prefill = LLMEngine.Instruct.PrefillThinking;
            LLMEngine.Instruct.PrefillThinking = false;
            var replyln = 2048;

            var promptbuild = LLMEngine.GetPromptBuilder();

            var sysprompt = "You are a system designed to analyze chatlogs and determine who should get a turn to talk next. " +
                "Use the dialogs and context clues provided in the chatlog to make your choice. " + LLMEngine.NewLine + LLMEngine.NewLine +
                "You will be provided with a chatlog and a list of participants. Respond with a short reasoning followed by the number corresponding to your selection.";
            var group = param.Group;
            // retrieve the last 6 messages from the session
            var messages = param.Messages;
            var request = new StringBuilder();
            request.AppendLinuxLine("# Chatlog").AppendLinuxLine();
            foreach (var msg in messages)
            {
                request.AppendLinuxLine($"{msg.Sender?.Name ?? "Unknown"}: {msg.Message.RemoveNewLines()}").AppendLinuxLine();
            }
            var lstpeople = new List<Character>() { (MainForm.User as Character)! };
            lstpeople.AddRange(group.AllPersonas);
            lstpeople.Remove((Character)(messages.Last().Sender)!);
            request.AppendLinuxLine().AppendLinuxLine("# Pick who is talking next:");
            var x = 1;
            foreach (var member in lstpeople)
            {
                request.AppendLinuxLine($"{member?.Name ?? "Unknown"} is {x}");
                x++;
            }
            request.AppendLinuxLine().Append("Based on the chatlog, determine who is meant to talk next, and type the number corresponding to that person from the list above.");

            promptbuild.AddMessage(AuthorRole.SysPrompt, sysprompt);
            promptbuild.AddMessage(AuthorRole.User, request.ToString());
            var query = promptbuild.PromptToQuery(AuthorRole.Assistant, (LLMEngine.Sampler.Temperature > 0.75) ? 0.75 : LLMEngine.Sampler.Temperature, replyln, forceAltRoles: false);
            if (query is GenerationInput llmparams && !param.AllowFullAnalysis)
            {
                llmparams.Grammar = "root ::= [^0-9]* [0-9]";
            }

            var finalstr = await LLMEngine.SimpleQuery(query, ct).ConfigureAwait(false);
            finalstr = finalstr.Trim();
            if (string.IsNullOrWhiteSpace(finalstr))
                return null;
            if (param.AllowFullAnalysis)
            {
                // retrieve the last number from the response
                var numbers = finalstr.Where(c => char.IsDigit(c)).ToArray();
                finalstr = numbers.Length > 0 ? numbers[^1].ToString() : string.Empty;
            }
            else
            {
                // retrieve the last character from the response as it should be the number
                finalstr = finalstr[^1].ToString();
            }

            LLMEngine.NamesInPromptOverride = null;
            LLMEngine.Instruct.PrefillThinking = prefill;

            if (LLMEngine.Client!.SupportsStateSave && savedKV)
            {
                await Task.Delay(50, ct);
                var doneKV = await LLMEngine.Client.LoadKVState(0);
                if (doneKV)
                {
                    await LLMEngine.Client.ClearKVStates();
                }
                await Task.Delay(50, ct);
            }

            if (int.TryParse(finalstr.Trim(), out int selectedindex) && selectedindex <= lstpeople.Count && selectedindex > 0)
            {
                selectedindex--;
                return lstpeople[selectedindex];
            }
            return null;
        }
    }
}
