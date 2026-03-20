using LetheAISharp.Files;
using LetheAISharp.LLM;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using LetheChat.Game;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace LetheChat.Slash
{
    internal class CmdRenpyGame(ISlashCommand owner) : SlashCommandInfo(owner)
    {
        public RenpyGameCmds renpyGameCmds => (RenpyGameCmds)Owner;

        public override string ID => "/rgame";
        public override string Description => "RenPy - Initialize game state";
        public override string Slash => "/rgame [game_folder]";
        public override SlashReturn Execute(string userinput)
        {
            // remove the /sys prefix
            var remainder = userinput.Length > ID.Length ? userinput[ID.Length..] : string.Empty;
            var msgpath = remainder.Trim();
            if (string.IsNullOrWhiteSpace(msgpath))
                return new SlashReturn(GetHelpMessage(), false, false, true);
            renpyGameCmds.RenpyHandler = new RenPyDialogHandler(msgpath, "Slay The Princess");
            var message = new SingleMessage(AuthorRole.System, "*Game Loaded: Slay The Princess*");
            return new SlashReturn(message, true, false);
        }
    }

    internal class CmdRenpyContinue(ISlashCommand owner) : SlashCommandInfo(owner)
    {
        public RenpyGameCmds renpyGameCmds => (RenpyGameCmds)Owner;

        public override string ID => "/rcontinue";
        public override string Description => "RenPy - Show current screen with optional user comment";
        public override string Slash => "/rcontinue [user_comment?]";
        public override SlashReturn Execute(string userinput)
        {
            if (renpyGameCmds.RenpyHandler is null)
                return new SlashReturn(null, false, false);
            var gameinfo = renpyGameCmds.RenpyHandler.Continue();
            // check if there's something after "/continue" in ed_input.Text and if there is, store in variable
            var extra = userinput.Length > 9 ? userinput[10..].Trim() : string.Empty;
            var message = string.Empty;
            // remove the /sys prefix
            if (!string.IsNullOrEmpty(extra))
            {
                message = $"**{LLMEngine.User.Name}'s Comment**" + LLMEngine.NewLine + extra + LLMEngine.NewLine + LLMEngine.NewLine;
            }
            message += gameinfo.ShowFullScreen();
            var msg = new SingleMessage(AuthorRole.User, message);
            return new SlashReturn(msg, true, true);
        }
    }

    internal class CmdRenpyPick(ISlashCommand owner) : SlashCommandInfo(owner)
    {
        public RenpyGameCmds renpyGameCmds => (RenpyGameCmds)Owner;

        public override string ID => "/rpick";
        public override string Description => "RenPy - Pick an option";
        public override string Slash => "/rpick [option]";
        public override SlashReturn Execute(string userinput)
        {
            if (renpyGameCmds.RenpyHandler is null)
                return new SlashReturn(GetHelpMessage(), false, false, true);
            var remainder = userinput.Length > ID.Length ? userinput[ID.Length..] : string.Empty;
            var dialog = remainder.Trim();
            var id = int.TryParse(dialog, out var test) ? test : 0;
            var gameinfo = renpyGameCmds.RenpyHandler.MakeChoice(id);

            var msg = new SingleMessage(AuthorRole.System, gameinfo);
            return new SlashReturn(msg, true, false);
        }
    }

    internal class CmdRenpyDialogs(ISlashCommand owner) : SlashCommandInfo(owner)
    {
        public RenpyGameCmds renpyGameCmds => (RenpyGameCmds)Owner;

        public override string ID => "/rdialogs";
        public override string Description => "RenPy - Show current screen's dialogs";
        public override string Slash => "/rdialogs";
        public override SlashReturn Execute(string userinput)
        {
            if (renpyGameCmds.RenpyHandler is null)
                return new SlashReturn(null, false, false);

            var gameinfo = renpyGameCmds.RenpyHandler.Continue();
            var msg = new SingleMessage(AuthorRole.System, gameinfo.ShowDialogs());
            return new SlashReturn(msg, true, false);
        }
    }


    public class RenpyGameCmds : ISlashCommand
    {
        public List<SlashCommandInfo> Commands { get; }
        public bool FirstLineOnly => false;

        public RenPyDialogHandler? RenpyHandler { get; set; }

        public RenpyGameCmds()
        {
            Commands = [ new CmdRenpyGame(this), new CmdRenpyContinue(this), new CmdRenpyPick(this), new CmdRenpyDialogs(this) ];
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
