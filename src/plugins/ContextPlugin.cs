using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaifuAI.Files;

namespace WaifuAI.Plugins
{

    public class PluginResponse
    {
        public bool IsHandled { get; set; }
        public string? Response { get; set; }
        public bool Replace { get; set; } = true;
        public AuthorRole AuthorRole { get; set; } = AuthorRole.User;
    }

    public abstract class ContextPlugin : BaseFile, IContextPlugin
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;

        public virtual async Task<PluginResponse> ReplaceUserInput(string userinput) 
        {
            return new PluginResponse { IsHandled = false, Response = string.Empty };
        }

        public abstract bool AddToSystemPrompt(string userinput, Chatlog log, out string response);

        public abstract bool ReplaceOutput(string botoutput, Chatlog log, out string response);
    }
}
