using LetheAISharp;
using LetheAISharp.Agent;
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

    public class GoalList : LLMExtractableBase<GoalList>
    {
        [JsonIgnore] private static string Schema = string.Empty;

        [Required]
        [MinLength(0)]
        [MaxLength(3)]
        [Description("A list of goals and pursuits that {{mchar}} wants to do.")]
        public List<string> Goals { get; set; } = [];

        public override async Task<string> GetGrammar()
        {
            if (Schema == string.Empty)
            {
                Schema = await base.GetGrammar();
            }
            return Schema;
        }

    }

    public class GoalRecord : LLMExtractableBase<GoalRecord>
    {
        [JsonIgnore] private static string Schema = string.Empty;

        [Required]
        [Description("A goal {{mchar}} wants to set for themselves.")]
        public string GoalTitle { get; set; } = string.Empty;

        [Required]
        [Description("A detailed description of what {{mchar}} wants to do, or get {{user}} to do.")]
        public string GoalDetails { get; set; } = string.Empty;

        [Required]
        [Description("The reason for setting this goal.")]
        public string Reason { get; set; } = string.Empty;

        [Required]
        [Description("The plan of action {{mchar}} wants to put in place to achieve this goal.")]
        public string PlanOfAction { get; set; } = string.Empty;

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
