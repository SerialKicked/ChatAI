using LetheAISharp;
using LetheAISharp.LLM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace WaifuAI.GBNF
{

    public class UserRecord : LLMExtractableBase<UserRecord>
    {
        [JsonIgnore] private static string Schema = string.Empty;

        [MinLength(0)]
        [MaxLength(50)]
        [Description("A list of {{user}}'s relations and friends, add relevant details when available")]
        public List<string> Relationships { get; set; } = [];

        [MinLength(0)]
        [MaxLength(50)]
        [Description("A detailed list of {{user}}'s tastes and hobbies")]
        public List<string> TastesAndHobbies { get; set; } = [];

        [MinLength(0)]
        [MaxLength(50)]
        [Description("A detailed list of {{user}}'s kinks and sexual tastes")]
        public List<string> Sexual { get; set; } = [];

        [MinLength(0)][MaxLength(50)][Description("A list of other facts about {{user}} not fitting in the other categories")]
        public List<string> UserInfo { get; set; } = [];


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
