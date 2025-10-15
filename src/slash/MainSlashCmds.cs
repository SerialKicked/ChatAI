using LetheAISharp.Files;
using LetheAISharp.LLM;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using WaifuAI.Game;

namespace WaifuAI.Slash
{
    internal class CmdMainSystem(ISlashCommand owner) : SlashCommandInfo(owner)
    {
        public override string ID => "/sys";
        public override string Description => "Core - Post a system message";
        public override string Slash => "/sys [message]";
        public override SlashReturn Execute(string userinput)
        {
            var remainder = userinput.Length > ID.Length ? userinput[ID.Length..] : string.Empty;
            var dialog = remainder.Trim();
            if (string.IsNullOrWhiteSpace(dialog))
                return new SlashReturn(GetHelpMessage(), false, false, true);
            var msg = new SingleMessage(AuthorRole.System, dialog);
            return new SlashReturn(msg, true, true);
        }
    }

    public class MainSlashCmds : ISlashCommand
    {
        public List<SlashCommandInfo> Commands { get; }
        public bool FirstLineOnly => false;

        public MainSlashCmds()
        {
            Commands = [ new CmdMainSystem(this) ];
        }

        public SlashReturn RunCommand(string userinput)
        {
            foreach (var item in Commands)
            {
                if (userinput.StartsWith(item.ID, StringComparison.OrdinalIgnoreCase))
                    return item.Execute(userinput);
            }
            return new SlashReturn(null, false, false);
        }
    }
}
