using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaifuAI.Files;
using Newtonsoft.Json;

namespace WaifuAI
{

    // Suppress CS0649: It's JSON loaded
    #pragma warning disable CS0649
    internal record STMessage
    {
        public string name = string.Empty;
        public bool is_user = false;
        public string mes = string.Empty;
        public string send_date = string.Empty;
    }
    #pragma warning restore CS0649

    internal class ImportST : BaseFile
    {
        public string Name { get; set; } = string.Empty;
        public List<STMessage> Inventory { get; set; } = [];
    }

    internal class DataSet : List<DataEntry> { }

    internal class DataEntry
    {
        public string Human { get; set; } = string.Empty;
        public string GPT { get; set; } = string.Empty;
    }

    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendLinuxLine(this StringBuilder sb, string? text = null)
        {
            return text == null ? sb.Append(LLMChatManager.NewLine) : sb.Append(text).Append(LLMChatManager.NewLine);
        }
    }

    /// <summary>
    /// A bunch of functions to make WPF's life easier
    /// </summary>
    public static class Tools
    {
        public static int CPUCoreCount() => Environment.ProcessorCount;

        /// <summary>
        /// Import a SillyTavern chatlog file (preconverted from JSONL to JSON ImportST) into a wAIfu Chatlog
        /// </summary>
        /// <param name="inputpath"></param>
        /// <param name="outputpath"></param>
        /// <param name="bot"></param>
        /// <param name="user"></param>
        internal static void Import(string inputpath, string outputpath, string bot, string user)
        {
            if (!File.Exists(inputpath))
                return;
            var str = File.ReadAllText(inputpath);
            var item = JsonConvert.DeserializeObject<ImportST>(str)!;

            var chat = new Chatlog();
            foreach (var msg in item.Inventory)
            {
                var role = msg.is_user ? AuthorRole.User : AuthorRole.Assistant;
                chat.Messages.Add(new SingleMessage(role, DateTime.TryParse(msg.send_date, out var d) ? d : default, msg.mes ?? string.Empty, bot, user, false));
            }
            (chat as IFile).SaveToFile(outputpath);
        }

        /// <summary>
        /// Export a SillyTavern chatlog file (preconverted from JSONL to JSON ImportST) to a dataset
        /// </summary>
        /// <param name="inputpath"></param>
        /// <param name="outputpath"></param>
        /// <param name="bot"></param>
        /// <param name="user"></param>
        internal static void ExportToDataSet(string inputpath, string outputpath)
        {
            if (!File.Exists(inputpath))
                return;
            var str = File.ReadAllText(inputpath);
            var item = JsonConvert.DeserializeObject<ImportST>(str)!;

            var chat = new DataSet();
            var startID = 0;
            // locate first user message list ID
            startID = item.Inventory.FindIndex(x => x.is_user);
            if (startID == -1)
            {
                throw new Exception("No user message found in data set");
            }
            // read messages 2 by 2 and add them to the dataset
            for (int i = startID; i < item.Inventory.Count - 1; i += 2)
            {
                // check it's an user/gpt pair
                if (!item.Inventory[i].is_user || item.Inventory[i + 1].is_user)
                {
                    // find next user message and go there
                    var nextuser = item.Inventory.FindIndex(i + 1, x => x.is_user);
                    if (nextuser == -1)
                    {
                        break;
                    }
                    i = nextuser;
                    continue;
                }
                // add entry
                var entry = new DataEntry
                {
                    Human = item.Inventory[i].mes,
                    GPT = item.Inventory[i + 1].mes
                };
                chat.Add(entry);
            }
            // save dataset
            File.WriteAllText(outputpath, JsonConvert.SerializeObject(chat, Formatting.Indented));
        }
    }
}
