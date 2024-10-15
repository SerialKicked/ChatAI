using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaifuAI.Files
{
    public enum PostReplaceMode
    {
        /// <summary> Do not replace the post </summary>
        None,
        /// <summary> Replace the post fully </summary>
        Replace,
        /// <summary> Alter processing but show original string (user input only)</summary>
        ProcessingOnly
    }

    public abstract class Session : BaseFile
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual PostReplaceMode OnBotResponse(string input, out string output)
        {
            output = string.Empty;
            return PostReplaceMode.None;
        }

        public virtual PostReplaceMode OnUserPost(string input, out string output)
        {
            output = string.Empty;
            return PostReplaceMode.None;
        }

        public virtual PostReplaceMode OnSystemPrompt(string input, out string output)
        {
            output = string.Empty;
            return PostReplaceMode.None;
        }

        public virtual void BeginSession() { }
        public virtual void EndSession() { }
        public virtual void FullReset() { }
    }
}
