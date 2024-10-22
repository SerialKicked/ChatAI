using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaifuAI.Memory;
using WaifuAI.Files;
using Newtonsoft.Json;
using Microsoft.VisualBasic.ApplicationServices;
using Parlot.Fluent;
using WaifuAI.Web;
using System.Text.RegularExpressions;

namespace WaifuAI
{

    internal record STWorldEntry
    {
        public string[] key = [];
        public string[] keysecondary = [];
        public string comment = string.Empty;
        public string content = string.Empty;
        public bool disable = false;
        public int sticky = 0;
        public int position = 1;
        public int selectiveLogic = 0;
        public int depth = 0;
        public int order = 100;
    }

    internal class ImportSTWorld : BaseFile
    {
        public Dictionary<int, STWorldEntry> entries { get; set; } = [];
    }

    public class BooleanOrStringConverter : JsonConverter<bool>
    {
        public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Boolean)
            {
                return (bool)reader.Value!;
            }
            else if (reader.TokenType == JsonToken.String)
            {
                var stringValue = (string)reader.Value!;
                return string.IsNullOrEmpty(stringValue);
            }
            else if (reader.TokenType == JsonToken.Null)
            {
                return false;
            }
            throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing boolean.");
        }

        public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }



    // Suppress CS0649: It's JSON loaded
#pragma warning disable CS0649
    internal record STMessage
    {
        public string name = string.Empty;
        public bool is_user = false;
        [JsonConverter(typeof(BooleanOrStringConverter))]
        public bool is_system { get; set; } = false;
        public string mes = string.Empty;
        public string send_date = string.Empty;
    }
    #pragma warning restore CS0649

    internal class ImportSTChat : BaseFile
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

    public static class JsonlToJsonConverter
    {
        public static void ConvertJsonlToJson(string inputPath, string outputPath)
        {
            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"The file {inputPath} does not exist.");

            var lines = File.ReadAllLines(inputPath);
            var messages = new List<STMessage>();

            foreach (var line in lines)
            {
                var message = JsonConvert.DeserializeObject<STMessage>(line);
                if (message != null)
                {
                    messages.Add(message);
                }
            }

            var importST = new ImportSTChat
            {
                Inventory = messages
            };

            var json = JsonConvert.SerializeObject(importST, Formatting.Indented);
            File.WriteAllText(outputPath, json);
        }
    }

    public static class StringExtensions
    {
        public static StringBuilder AppendLinuxLine(this StringBuilder sb, string? text = null)
        {
            return text == null ? sb.Append(LLMSystem.NewLine) : sb.Append(text).Append(LLMSystem.NewLine);
        }

        public static string ToWinFormat(this string text) => text.Replace("\n", "\r\n");

        public static string ToLinuxFormat(this string text) => text.Replace("\r\n", "\n");

        public static string SanitizeForJS(this string text)
        {
            return text.Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r");
        }

        public static string FixAsterisks(this string text)
        {
            // Automatically close asterisks if they are not closed before the end of each paragraph delimited by a newline
            var lines = text.Split(LLMSystem.NewLine);
            for (int i = 0; i < lines.Length; i++)
            {
                // skip small lines
                if (lines[i].Length <= 2)
                    continue;
                if (lines[i].StartsWith('*'))
                {
                    // remove the first character from lines[i]
                    lines[i] = lines[i][1..];
                    lines[i] = "*" + lines[i].Trim();
                }
                if (lines[i].Count(c => c == '*') % 2 == 1)
                {
                    // If a line ends with but doesn't start with an asterisk, add one at the beginning
                    if (lines[i].EndsWith('*') && !lines[i].StartsWith('*'))
                    {
                        lines[i] = "*" + lines[i].Trim();
                    }
                    else
                        lines[i] = lines[i].Trim() + "*";
                }
            }
            return string.Join(LLMSystem.NewLine, lines);
        }
    }

    /// <summary>
    /// A bunch of functions to make WPF's life easier
    /// </summary>
    public static class Tools
    {
        public static int CPUCoreCount() => Environment.ProcessorCount;

        internal static bool ImportWorld(string inputpath, string outputpath)
        {
            if (!File.Exists(inputpath))
                return false;
            try
            {
                var filecontent = File.ReadAllText(inputpath);
                var importST = JsonConvert.DeserializeObject<ImportSTWorld>(filecontent);
                if (importST == null)
                    return false;

                var outputWorld = new WorldInfo
                {
                    Name = Path.GetFileNameWithoutExtension(inputpath),
                    Description = "Imported from Silly Tavern",
                };
                foreach (var (key, item) in importST.entries)
                {
                    var entry = new WorldEntry
                    {
                        KeyWordsMain = [.. item.key],
                        KeyWordsSecondary = [.. item.keysecondary],
                        CaseSensitive = false,
                        Duration = item.sticky,
                        Enabled = !item.disable,
                        Message = item.content,
                        Name = item.comment,
                        Priority = item.order,
                        PositionIndex = item.depth,
                        Position = item.position == 1 ? WEPosition.SystemPrompt : WEPosition.Chat,
                        WordLink = item.selectiveLogic switch
                        {
                            0 => KeyWordLink.And,
                            1 => KeyWordLink.Or,
                            2 => KeyWordLink.Not,
                            _ => KeyWordLink.And
                        }
                    };
                    outputWorld.Entries.Add(entry);
                }
                (outputWorld as IFile).SaveToFile(outputpath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Import a SillyTavern chatlog file (preconverted from JSONL to JSON ImportST) into a wAIfu Chatlog
        /// </summary>
        /// <param name="inputpath"></param>
        /// <param name="outputpath"></param>
        /// <param name="bot"></param>
        /// <param name="user"></param>
        internal static bool ImportChatlog(string inputpath, string outputpath, string bot, string user)
        {
            if (!File.Exists(inputpath))
                return false;
            try
            {
                var lines = File.ReadAllLines(inputpath);
                var messages = new List<STMessage>();
                foreach (var line in lines)
                {
                    var message = JsonConvert.DeserializeObject<STMessage>(line);
                    if (message != null)
                        messages.Add(message);
                }
                var importST = new ImportSTChat
                {
                    Inventory = messages
                };
                var chat = new Chatlog();
                foreach (var msg in importST.Inventory)
                {
                    var role = msg.is_user ? AuthorRole.User : AuthorRole.Assistant;
                    if (!msg.is_user && msg.is_system)
                        role = AuthorRole.System;
                    chat.Messages.Add(new SingleMessage(role, DateTime.TryParse(msg.send_date, out var d) ? d : default, msg.mes ?? string.Empty, bot, user));
                }
                (chat as IFile).SaveToFile(outputpath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
            var item = JsonConvert.DeserializeObject<ImportSTChat>(str)!;

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
