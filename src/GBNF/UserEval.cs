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

    public class UserEval : LLMExtractableBase<UserEval>
    {
        [JsonIgnore] private static string Schema = string.Empty;

        [MinLength(1)]
        [MaxLength(6)]
        [Description("Long term goals that {{mchar}} wants to complete regarding {{user}}. Those are overall aspirations for the months and years to come. You can alter entries but avoid deletion.")]
        public List<string> LongTermGoals { get; set; } = [];

        [Description("Analysis of the submitted chat session and {{user}}'s behavior, in regards to the stated goals.")]
        public string Analysis { get; set; } = string.Empty;

        [MinLength(0)]
        [MaxLength(10)]
        [Description("A list of {{user}}'s blocks and barriers that {{mchar}} wants to overcome. Be detailed, include reasons and context.")]
        public List<string> Blocks { get; set; } = [];

        [MinLength(0)]
        [MaxLength(10)]
        [Description("A list of potential topics that {{user}} is trying to hide or keep away from {{mchar}}. Be detailed, include reasons and context. Only fill this list if the chat session contains hints to such a thing.")]
        public List<string> Secrets { get; set; } = [];

        [MinLength(0)]
        [MaxLength(10)]
        [Description("A list to track {{user}}'s progress in relation with your stated goals. Be detailed, include reasons and context.")]
        public List<string> Progress { get; set; } = [];

        [Description("Based on the information, make a conclusion about {{user}}'s behavior and their alignment with the stated goals. Offer opinions and new directives for further improvement.")]
        public string Conclusions { get; set; } = string.Empty;

        [MinLength(1)]
        [MaxLength(5)]
        [Description("Short term tasks that {{mchar}} want to impose on {{user}}. They are meant to help achieve long-term goals, and overcome potential blocks.")]
        public List<string> ShortTermTasks { get; set; } = [];

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
