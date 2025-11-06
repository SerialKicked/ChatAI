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
            return new SlashReturn(msg, false, true, true);
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

    internal class CmdMainLock(ISlashCommand owner) : SlashCommandInfo(owner)
    {
        public override string ID => "/lock";
        public override string Description => "Core - Lock the togglable device";
        public override string Slash => "/lock [duration in days](optional)";
        public override SlashReturn Execute(string userinput)
        {
            MainForm.Bot!.LockManager.Settings.Enabled = true;

            var remainder = userinput.Length > ID.Length ? userinput[ID.Length..] : string.Empty;
            var durationstring = remainder.Trim();
            if (string.IsNullOrWhiteSpace(durationstring))
            {
                MainForm.Bot.LockManager.LockItem();
            }
            if (!int.TryParse(durationstring, out int durationdays) || durationdays <= 0)
                return new SlashReturn(GetHelpMessage(), false, false, true);
            MainForm.Bot.LockManager.LockItem(new TimeSpan(durationdays,0,0,0));
            var msg = new SingleMessage(AuthorRole.System, MainForm.Bot.LockManager.GetStatusMessage());
            return new SlashReturn(msg, true, true, false);
        }
    }

    internal class CmdMainUnlock(ISlashCommand owner) : SlashCommandInfo(owner)
    {
        public override string ID => "/unlock";
        public override string Description => "Core - Unlock the togglable device";
        public override string Slash => "/unlock";
        public override SlashReturn Execute(string userinput)
        {
            MainForm.Bot!.LockManager.Settings.Enabled = true;
            MainForm.Bot.LockManager.UnlockItem();
            var msg = new SingleMessage(AuthorRole.System, MainForm.Bot.LockManager.GetStatusMessage());
            return new SlashReturn(msg, true, true, false);
        }
    }
    internal class CmdMainLockInfo(ISlashCommand owner) : SlashCommandInfo(owner)
    {
        public override string ID => "/lockinfo";
        public override string Description => "Core - Check remaining timer on lock";
        public override string Slash => "/lockinfo";
        public override SlashReturn Execute(string userinput)
        {
            var msg = new SingleMessage(AuthorRole.System, MainForm.Bot!.LockManager.GetStatusMessage());
            return new SlashReturn(msg, true, true, false);
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

            var res = LLMEngine.Bot.Brain.GetMemoriesByTitle(dialog, true);
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
            Commands = 
            [ 
                new CmdMainSystem(this), new CmdMainRecall(this), new CmdMainHelp(this), 
                new CmdMainLock(this), new CmdMainUnlock(this), new CmdMainLockInfo(this) 
            ];
        }

        public SlashReturn RunCommand(string userinput)
        {
            if (userinput.Length > 0)
            {
                foreach (var item in Commands)
                {
                    // get first word only
                    var firstword = userinput.Split(' ')[0];
                    if (firstword.Equals(item.ID, StringComparison.OrdinalIgnoreCase))
                        return item.Execute(userinput);
                }
            }
            return new SlashReturn(null, false, false);
        }
    }
}
