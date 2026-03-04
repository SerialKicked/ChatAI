using LetheAISharp.Files;
using LetheAISharp.LLM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaifuAI
{
    internal static class ChatRender
    {
        /// <summary>
        /// Returns a message prefix depending on the role. (generally the user/bot's name)
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static string GetMessagePrefix(AuthorRole role)
        {
            return role switch
            {
                AuthorRole.System => "",
                AuthorRole.SysPrompt => "",
                AuthorRole.ToolResult => "",
                AuthorRole.User => "**" + LLMEngine.User.Name + ":** ",
                AuthorRole.Assistant => "**" + LLMEngine.Bot.Name  + ":** ",
                _ => "**Error:** ",
            };
        }

        public static string GetMessagePrefix(SingleMessage message)
        {
            return message.Role switch
            {
                AuthorRole.System => "**SYSTEM:** ",
                AuthorRole.SysPrompt => "**SYS PROMPT:** ",
                AuthorRole.User => "**" + message.User.Name + ":** ",
                AuthorRole.Assistant => "**" + message.Bot.Name + ":** ",
                AuthorRole.ToolResult => "**TOOL RESULT:** ",
                _ => "**Error:** ",
            };
        }
    }
}
