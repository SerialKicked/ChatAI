using AIToolkit;
using AIToolkit.Agent;
using AIToolkit.LLM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace WaifuAI.GBNF
{

    public class GoalList : LLMExtractableBase<GoalList>
    {
        [JsonIgnore] protected static string Schema = string.Empty;

        [Required]
        [MinLength(0)]
        [MaxLength(3)]
        [Description("A list of goals and pursuits that {{char}} wants to do.")]
        public List<string> Goals { get; set; } = [];

        public override async Task<string> GetGrammar()
        {
            if (Schema == string.Empty)
            {
                Schema = await base.GetGrammar();
            }
            return Schema;
        }

        public override string GetQuery()
        {
            var requestedTask = "Answer the request using the following JSON format:" + LLMEngine.NewLine;

            var schema = DescriptionHelper.GetAllDescriptionsRecursive<GoalList>();

            foreach (var prop in schema)
            {
                requestedTask += $"- {prop.Key}: {prop.Value}\n";
            }
            requestedTask = LLMEngine.Bot.ReplaceMacros(requestedTask);
            return requestedTask;
        }
    }

    public class GoalRecord : LLMExtractableBase<GoalRecord>
    {
        [JsonIgnore] protected static string Schema = string.Empty;

        [Required]
        [Description("A goal {{char}} wants to set for themselves.")]
        public string GoalTitle { get; set; } = string.Empty;

        [Required]
        [Description("A detailed description of what {{char}} wants to do, or get {{user}} to do.")]
        public string GoalDetails { get; set; } = string.Empty;

        [Required]
        [Description("The reason for setting this goal.")]
        public string Reason { get; set; } = string.Empty;

        [Required]
        [Description("The plan of action {{char}} wants to put in place to achieve this goal.")]
        public string PlanOfAction { get; set; } = string.Empty;

        public override async Task<string> GetGrammar()
        {
            if (Schema == string.Empty)
            {
                Schema = await base.GetGrammar();
            }
            return Schema;
        }

        public override string GetQuery()
        {
            var requestedTask = "Answer the request using the following JSON format:" + LLMEngine.NewLine;
            var schema = DescriptionHelper.GetAllDescriptionsRecursive<GoalRecord>();
            foreach (var prop in schema)
            {
                requestedTask += $"- {prop.Key}: {prop.Value}\n";
            }
            requestedTask = LLMEngine.Bot.ReplaceMacros(requestedTask);
            return requestedTask;
        }
    }
}
