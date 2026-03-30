using LetheAISharp;
using LetheAISharp.LLM;
using LetheAISharp.Moods;
using Newtonsoft.Json;
using System.Text;

namespace LetheChat.GBNF
{
    /// <summary>
    /// Dynamically builds structured output (query text + GBNF grammar) based on
    /// the currently loaded moodlets, and parses the LLM response back into a
    /// <see cref="Dictionary{String, Modifier}"/>.
    /// </summary>
    public class DynamicMoodAnalysis : ILLMExtractableBase
    {
        private readonly Dictionary<string, IMoodlet> _moodlets;

        public DynamicMoodAnalysis(Dictionary<string, IMoodlet> moodlets)
        {
            _moodlets = moodlets;
        }

        public string GetQuery()
        {
            var sb = new StringBuilder("Respond using a JSON format containing the following information:\n");
            foreach (var (key, moodlet) in _moodlets)
            {
                var desc = LLMEngine.Bot.ReplaceMacros(moodlet.Description);
                sb.AppendLine($"- {key}: {desc}");
            }
            return sb.ToString();
        }

        public Task<string> GetGrammar()
        {
            var keys = _moodlets.Keys.ToList();
            var sb = new StringBuilder();

            // Root rule: JSON object with one key-value pair per loaded moodlet
            sb.Append("root ::= \"{\" space ");
            for (int i = 0; i < keys.Count; i++)
            {
                sb.Append($"{SanitizeRuleName(keys[i])}-kv");
                if (i < keys.Count - 1)
                    sb.Append(" \",\" space ");
            }
            sb.AppendLine(" \"}\" space");

            // Modifier enum rule (matches the Modifier enum names as JSON strings)
            sb.AppendLine("modifier ::= \"\\\"HighReduction\\\"\" | \"\\\"SmallReduction\\\"\" | \"\\\"None\\\"\" | \"\\\"SmallIncrease\\\"\" | \"\\\"HighIncrease\\\"\"");

            // Key-value rules for each moodlet
            foreach (var key in keys)
            {
                var sanitized = SanitizeRuleName(key);
                var escaped = EscapeGbnfString(key);
                sb.AppendLine($"{sanitized}-kv ::= \"\\\"{escaped}\\\"\" space \":\" space modifier space");
            }

            sb.AppendLine("space ::= | \" \" | \"\\n\"{1,2} [ \\t]{0,20}");

            return Task.FromResult(sb.ToString());
        }

        /// <summary>
        /// Parses a JSON response string from the LLM into a mood modifier dictionary.
        /// </summary>
        public static Dictionary<string, Modifier>? ParseResponse(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, Modifier>>(json);
        }

        private static string SanitizeRuleName(string name)
        {
            return new string([.. name.Select(c => char.IsLetterOrDigit(c) ? c : '_')]);
        }

        private static string EscapeGbnfString(string str)
        {
            return str.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }
    }
}
