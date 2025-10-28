using LetheAISharp;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using WaifuAI.Game;

namespace WaifuAI.Slash
{
    internal class CmdMainHelp(ISlashCommand owner) : SlashCommandInfo(owner)
    {
        public override string ID => "/help";
        public override string Description => "Core - List all slash commands";
        public override string Slash => "/help";
        public override SlashReturn Execute(string userinput)
        {
            var x = new StringBuilder();
            x.AppendLinuxLine("**Command List:**");
            x.AppendLinuxLine("````");
            foreach (var item in Program.BigForm!.slashCommands)
            {
                foreach (var cmd in item.Commands)
                {
                    x.AppendLinuxLine(cmd.Slash + " -> " + cmd.Description);
                }
            }
            x.AppendLinuxLine("````");
            var msg = new SingleMessage(AuthorRole.System, x.ToString());
            return new SlashReturn(msg, true, true, true);
        }
    }
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

    internal class CmdMainRecall(ISlashCommand owner) : SlashCommandInfo(owner)
    {
        public override string ID => "/recall";
        public override string Description => "Core - Insert memories with specified title as system message";
        public override string Slash => "/recall [memory title]";
        public override SlashReturn Execute(string userinput)
        {
            var remainder = userinput.Length > ID.Length ? userinput[ID.Length..] : string.Empty;
            var dialog = remainder.CleanupAndTrim();
            if (string.IsNullOrWhiteSpace(dialog))
                return new SlashReturn(GetHelpMessage(), false, false, true);

            var res = LLMEngine.Bot.Brain.GetMemoriesByTitle(dialog);
            if (res.Count == 0)
                return new SlashReturn(null, false, false);

            var msgcontent = new StringBuilder();
            foreach (var item in res)
            {
                if (msgcontent.Length > 0)
                    msgcontent.AppendLinuxLine();
                msgcontent.AppendLinuxLine(item.ToSnippet(TitleInsertType.MarkdownH2, item.Category == MemoryType.ChatSession, item.Category == MemoryType.Goal, false));
            }

            var msg = new SingleMessage(AuthorRole.System, msgcontent.ToString());
            return new SlashReturn(msg, true, true, true);
        }
    }


    public class MainSlashCmds : ISlashCommand
    {
        public List<SlashCommandInfo> Commands { get; }
        public bool FirstLineOnly => false;

        public MainSlashCmds()
        {
            Commands = [ new CmdMainSystem(this), new CmdMainRecall(this), new CmdMainHelp(this) ];
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
