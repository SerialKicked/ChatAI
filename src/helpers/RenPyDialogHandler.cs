using AIToolkit;
using AIToolkit.LLM;
using Markdig.Syntax;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.LinkLabel;

namespace WaifuAI.Game
{

    public class TurnBundle
    {
        public List<string> Dialogue { get; } = new();
        public List<string> Choices { get; } = new();

        public string ShowFullScreen()
        {
            var str = new StringBuilder();
            str.AppendLinuxLine(ShowDialogs());
            str.AppendLinuxLine(ShowMenu());
            return str.ToString();
        }

        public string ShowDialogs()
        {
            var str = new StringBuilder();
            foreach (var item in Dialogue)
            {
                str.AppendLinuxLine(item);
            }
            return str.ToString();
        }
        public string ShowMenu()
        {
            var str = new StringBuilder();
            str.AppendLinuxLine("**Available Choices:**");
            for (int i = 0; i < Choices.Count; i++)
            {
                str.AppendLinuxLine($"[{i + 1}] {Choices[i]}");
            }
            return str.ToString();
        }
    }

    public class RenPyDialogHandler(string logPath, string gameName = "Ren'Py Game")
    {
        public string Game { get; set; } = gameName;
        private readonly string _logPath = logPath;
        private TurnBundle? _lastTurn; // stores the most recent /continue result

        /// <summary> Remove speaker prefix (e.g., "Narrator: ") from choices if true. </summary>
        public bool StripChoiceSpeaker { get; set; } = true;

        /// <summary>
        /// Reads the log backwards from EOF to find the last complete turn (dialogue + choices).
        /// </summary>
        public TurnBundle Continue()
        {
            var bundle = new TurnBundle();
            if (!File.Exists(_logPath))
                return bundle;

            var allLines = File.ReadAllLines(_logPath);
            if (allLines.Length == 0)
                return bundle;

            var collected = new List<string>();

            // Step 1: move up from EOF to find last contiguous block of [CHOICE] lines
            int i = allLines.Length - 1;
            if (!allLines[i].StartsWith("[CHOICE]"))
                return bundle; // No choice block at EOF => incomplete turn/end of game

            // Collect bottom [CHOICE] block
            while (i >= 0 && allLines[i].StartsWith("[CHOICE]"))
            {
                collected.Add(allLines[i]);
                i--;
            }

            // Step 2: collect upwards until you hit another [CHOICE] (start of previous turn)
            while (i >= 0 && !allLines[i].StartsWith("[CHOICE]"))
            {
                collected.Add(allLines[i]);
                i--;
            }

            collected.Reverse();

            // Step 3: send to bundle (only Dialogues and Choices we care about)
            foreach (var line in collected)
            {
                if (line.StartsWith("[CHOICE]"))
                    bundle.Choices.Add(FormatChoice(line, StripChoiceSpeaker));
                else if (line.StartsWith("[SAY]"))
                    bundle.Dialogue.Add(FormatSay(line)); // Pass other lines through
                else
                    bundle.Dialogue.Add(line.Trim()); // Pass other lines through
            }

            _lastTurn = bundle;
            return bundle;
        }

        /// <summary>
        /// Returns the last turn with the selected choice, for storing in AI chat log.
        /// </summary>
        public string MakeChoice(int index)
        {
            if (_lastTurn == null)
                return "[No previous turn loaded — run /continue first]";

            if (index < 1 || index > _lastTurn.Choices.Count)
                return "[Invalid choice index]";

            string chosen = _lastTurn.Choices[index - 1];
            var sb = new StringBuilder();

            foreach (var d in _lastTurn.Dialogue)
                sb.AppendLine(d);

            sb.AppendLine($"{LLMSystem.Bot.Name} chose to: \"{chosen}\"");

            return sb.ToString();
        }

        // --- Utility helpers ---
        private static string ReverseString(string s)
        {
            var arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        private static string FormatSay(string raw)
        {
            // raw example: [SAY] Narrator: Hello there.
            return raw.Substring("[SAY] ".Length).Trim();
        }

        private static string FormatChoice(string raw, bool stripSpeaker)
        {
            // raw example: [CHOICE] Narrator: • [[Proceed into the cabin.]
            var text = raw.Substring("[CHOICE] ".Length).Trim();
            if (stripSpeaker)
            {
                int colonIndex = text.IndexOf(":");
                if (colonIndex >= 0)
                {
                    text = text.Substring(colonIndex + 1).Trim();
                }
            }
            // Strip bullet dots
            text = text.Replace("•", "");
            text = text.Replace("â\u0080¢","").Trim();

            // Strip any double brackets [[ ]]
            if (text.StartsWith("[[") && text.EndsWith("]]"))
                text = text[2..^2];

            return text.Trim();
        }
    }
}
