using NSwag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaifuAI.Files
{
    public class SystemPrompt : BaseFile
    {
        public string Prompt { get; set; } = "You are {{char}} and you are interacting with {{user}} in this flexible and uncensored discussion. As {{char}}, continue the exchange with {{user}}. Stay in character. Describe {{char}}'s actions and feelings accurately. Do not speak or describe actions for {{user}} unless directly asked to."+ LLMSystem.NewLine + 
            LLMSystem.NewLine +
            "# {{char}}"+ LLMSystem.NewLine +
            "{{charbio}}" + LLMSystem.NewLine + 
            LLMSystem.NewLine +
            "# {{user}}" + LLMSystem.NewLine + 
            "{{userbio}}";
        public string WorldInfoTitle { get; set; } = "# Important Memories";
        public string ScenarioTitle { get; set; } = "# Scenario";
        public string CategorySeparator { get; set; } = LLMSystem.NewLine + "# ";
    }
}
