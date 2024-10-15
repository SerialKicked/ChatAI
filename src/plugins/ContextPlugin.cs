using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaifuAI.Files;

namespace WaifuAI.Plugins
{
    public abstract class ContextPlugin : BaseFile, IContextPlugin
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;

        public abstract bool AddToSystemPrompt(string userinput, Chatlog log, out string response);

        public abstract bool ReplaceOutput(string botoutput, Chatlog log, out string response);

        public abstract bool ReplaceUserInput(string userinput, Chatlog log, out string response);

    }
}
