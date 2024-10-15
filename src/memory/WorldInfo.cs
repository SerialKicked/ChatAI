using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaifuAI.Files;

namespace WaifuAI.Memory
{
    public enum WEPosition { SystemPrompt, Chat }
    public enum KeyWordLink
    {
        /// <summary> Triggers when there's at least one keyword from both Main and Secondary </summary>
        And,
        /// <summary> Triggers when there's a keyword from Main or Secondary </summary>
        Or,
        /// <summary> Triggers when there's a keyword from Main but not from Secondary </summary>
        Not
    }

    public class WorldEntry
    {
        public string Name = string.Empty;
        public bool Enabled = true;
        public List<string> KeyWordsMain = [];
        public List<string> KeyWordsSecondary = [];
        public KeyWordLink WordLink = KeyWordLink.And;
        public WEPosition Position = WEPosition.SystemPrompt;
        public int PositionIndex = 0;
        public int Priority = 100;
        public bool CaseSensitive = false;
        public string Message = string.Empty;

        public bool CheckKeywords(string message)
        {
            if (!Enabled)
                return false;
            var main = KeyWordsMain.Any(kw => message.Contains(kw, CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase));
            var secondary = KeyWordsSecondary.Any(kw => message.Contains(kw, CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase));
            return WordLink switch
            {
                KeyWordLink.And => main && secondary,
                KeyWordLink.Or => main || secondary,
                KeyWordLink.Not => main && !secondary,
                _ => false
            };
        }
    }

    public class WorldInfo : BaseFile
    {
        private class ActiveLink
        {
            public int RecordID = 0;
            public int DurationLeft = 0;
        }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ScanDepth { get; set; } = 1;
        public int ActivationDuration { get; set; } = 1;
        public List<WorldEntry> Entries { get; set; } = [];
        private readonly List<ActiveLink> activeEntries = [];

        /// <summary>
        /// Check for entries from a string
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<WorldEntry> FindEntries(string message)
        {
            foreach (var entry in activeEntries)
                entry.DurationLeft--;
            activeEntries.RemoveAll(a => a.DurationLeft <= 0);
            var active = activeEntries.Where(a => a.DurationLeft > 0).ToList();
            for (int i = 0; i < Entries.Count; i++)
            {
                var entry = Entries[i];
                if (!entry.Enabled || active.Any(a => a.RecordID == i))
                    continue;
                if (entry.CheckKeywords(message))
                    activeEntries.Add(new ActiveLink { RecordID = i, DurationLeft = ActivationDuration });
            }
            return activeEntries.Select(a => Entries[a.RecordID]).ToList();
        }

        /// <summary>
        /// Check the ScanDepth last messages for any active entries
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public List<WorldEntry> FindEntries(Chatlog log, string? userinput = null)
        {
            if (log.Messages.Count == 0)
                return [];
            // retrieve the last messages from the chatlog
            var messages = log.Messages.Skip(Math.Max(0, log.Messages.Count - ScanDepth)).Select(m => m.Message).ToList();  
            var stbuilder = new StringBuilder();
            foreach (var item in messages)
                stbuilder.AppendLinuxLine(item);
            if (userinput != null)
                stbuilder.AppendLinuxLine(userinput);
            return FindEntries(stbuilder.ToString());
        }

        public void Reset()
        {
            activeEntries.Clear();
        }
    }
}
