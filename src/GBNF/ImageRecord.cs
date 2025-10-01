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

    public class ImageRecord : LLMExtractableBase<ImageRecord>
    {
        [JsonIgnore] private static string Schema = string.Empty;

        [Required]
        [Description("A title for this image")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Description("A very detailed description of the image, including characters, pose, age, background, and other visual elements")]
        public string Description { get; set; } = string.Empty;

        [Description("If the image features text, write it here")]
        public string ImageText { get; set; } = string.Empty;

        [Description("If the image features any notable messaging or themes, describe them here")]
        public string Interpretation { get; set; } = string.Empty;

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
