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

        //[Description("A list of {{user}}'s relations and friends, add relevant details when available")]
        //public List<string> Relationships { get; set; } = [];
        [MinLength(0)]
        [MaxLength(10)]
        [Description("A detailed list of {{user}}'s tastes and hobbies (up to 10 entries)")]
        public List<string> TastesAndHobbies { get; set; } = [];

        [MinLength(0)]
        [MaxLength(10)]
        [Description("A detailed list of {{user}}'s kinks and sexual tastes (up to 10 entries)")]
        public List<string> Sexual { get; set; } = [];

        [MinLength(0)]
        [MaxLength(10)]
        [Description("A list of other facts about {{user}} not fitting in the other categories (up to 10 entries)")]
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
