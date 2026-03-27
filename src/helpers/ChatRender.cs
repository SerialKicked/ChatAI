using LetheAISharp.Files;
using LetheAISharp.LLM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetheChat
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
                AuthorRole.Tool => "",
                AuthorRole.User => "**" + LLMEngine.User.Name + ":** ",
                AuthorRole.Assistant => "**" + LLMEngine.Bot.Name  + ":** ",
                _ => "",
            };
        }

        public static string GetMessagePrefix(SingleMessage message)
        {
            return message.Role switch
            {
                AuthorRole.System => "**SYSTEM:** ",
                AuthorRole.User => "**" + message.User.Name + ":** ",
                AuthorRole.Assistant => "**" + message.Bot.Name + ":** ",
                AuthorRole.Tool => "**TOOL RESULT:** ",
                _ => "**Error:** ",
            };
        }
    }
}
