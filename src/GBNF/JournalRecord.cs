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

namespace WaifuAI.GBNF
{

    public class JournalRecord : LLMExtractableBase<JournalRecord>
    {
        [JsonIgnore] private static string Schema = string.Empty;

        [Required]
        [Description("Decide if you want to write in your journal today (true or false)")]
        public bool WriteInJournal { get; set; } = true;

        [Description("This is the topic you wish to write about today (if you pick true). Just enter a short description.")]
        public string Topic { get; set; } = string.Empty;

        public override async Task<string> GetGrammar()
        {
            if (Schema == string.Empty)
            {
                Schema = await base.GetGrammar().ConfigureAwait(false);
            }
            return Schema;
        }
    }
}
