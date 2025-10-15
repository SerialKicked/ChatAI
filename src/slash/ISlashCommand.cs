using LetheAISharp.Files;
using LetheAISharp.LLM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaifuAI.Slash
{
    public class SlashReturn(SingleMessage? message, bool logToHistory = true, bool replaceUser = false, bool noBotResponse = false)
    {
        public SingleMessage? Message { get; set; } = message;
        public bool LogToHistory { get; set; } = logToHistory;
        public bool ReplaceUser { get; set; } = replaceUser;
        public bool NoBotResponse { get; set; } = noBotResponse;
    }


    public abstract class SlashCommandInfo(ISlashCommand owner)
    {
        public virtual string ID { get; } = string.Empty;
        public virtual string Description { get; } = string.Empty;
        public virtual string Slash { get; } = string.Empty;

        protected ISlashCommand Owner = owner;

        public abstract SlashReturn Execute(string userinput);

        protected SingleMessage GetHelpMessage()
        {
            return new SingleMessage(AuthorRole.System, Description + LLMEngine.NewLine + Slash);
        }
    }

    public interface ISlashCommand
    {
        List<SlashCommandInfo> Commands { get; }
        bool FirstLineOnly { get; }
        SlashReturn RunCommand(string userinput);
    }
}
