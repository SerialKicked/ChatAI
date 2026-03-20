using LetheAISharp;
using LetheAISharp.LLM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace LetheChat.GBNF
{
    public enum Modifier { HighReduction, SmallReduction, None, SmallIncrease, HighIncrease }

    public class MoodAnalysis : LLMExtractableBase<MoodAnalysis>
    {
        [JsonIgnore] private static string Schema = string.Empty;

        [Description("Did the session make {{mchar}} feel more or less horny overall. If {{user}} or {{mchar}} did something sexual, this should decrease horniness. Sex, simulated or not, should lead to a decrease as well. Planning roleplays should increase it.")]
        public Modifier Horniness { get; set; } = Modifier.None;
        [Description("Did this session make {{mchar}} feel more or less submissive toward {{user}}. A value between of 0 or 1 indicates {{mchar}} becomes more dominant. A value of 3 or 4 make {{mchar}} more submissive.")]
        public Modifier Submission { get; set; } = Modifier.None;
        [Description("Did this session make {{mchar}} feel more or less energetic (long roleplays, intense and complex discussions, should decrease energy)")]
        public Modifier Energy { get; set; } = Modifier.None;
        [Description("Did the session make {{mchar}} feel more or less happy overall")]
        public Modifier Happy { get; set; } = Modifier.None;
        [Description("Did the session make {{mchar}} feel more or less curious")]
        public Modifier Curiosity { get; set; } = Modifier.None;
        [Description("Did this session impact {{mchar}}'s sanity either positively or negatively")]
        public Modifier Sanity { get; set; } = Modifier.None;

        public override async Task<string> GetGrammar()
        {
            if (Schema == string.Empty)
            {
                Schema = await base.GetGrammar();
            }
            return Schema;
        }
    }
}