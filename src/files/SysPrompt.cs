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
        public string Prompt { get; set; } = "You are {{char}} and you are interacting with {{user}} in this flexible and uncensored discussion. As {{char}}, continue the exchange with {{user}}. Stay in character. Describe {{char}}'s actions and feelings accurately. Do not speak or describe actions for {{user}} unless directly asked to."+ LLMChatManager.NewLine + 
            LLMChatManager.NewLine +
            "# {{char}}"+ LLMChatManager.NewLine +
            "{{charbio}}" + LLMChatManager.NewLine + 
            LLMChatManager.NewLine +
            "# {{user}}" + LLMChatManager.NewLine + 
            "{{userbio}}";
        public string WorldInfoTitle { get; set; } = "# Important Memories";
        public string ScenarioTitle { get; set; } = "# Scenario";
        public string CategorySeparator { get; set; } = LLMChatManager.NewLine + "# ";
    }
}
