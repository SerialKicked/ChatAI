using LetheAISharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WaifuAI.Files;

namespace WaifuAI.Plugins
{
    public class LogToggleEntry
    {
        public bool IsLocked { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [JsonIgnore] public TimeSpan Duration => EndTime - StartTime;
    }

    public class ToggleMonitorSettings
    {
        public bool Enabled { get; set; } = true;
        public string ItemToMonitor { get; set; } = "Keypad";
        public string StatusLockedMessage { get; set; } = "The keypad is locked.";
        public string StatusUnlockedMessage { get; set; } = "The keypad is unlocked.";
        public string StatusLockOverMessage { get; set; } = "The keypad's lock duration has expired and should now be unlocked.";
        public bool IsLocked { get; set; } = false;
        public bool AutomaticUnlockOnDurationEnd { get; set; } = false;
        public TimeSpan LockDuration { get; set; } = TimeSpan.Zero;
        public DateTime StateStartDate { get; set; } = DateTime.MinValue;
        public List<LogToggleEntry> ToggleLog { get; set; } = [];
    }


    public class TimedLockPlugin
    {
        [JsonIgnore] public Character Owner { get; set; }
        public ToggleMonitorSettings Settings => Owner.LockSettings;

        public TimedLockPlugin(Character owner)
        {
            Owner = owner;
        }

        public void LockItem(TimeSpan? duration = null)
        {
            if (!Settings.Enabled)
                return;
            // If lock state change, save previous status and duration to log before updating
            if (Settings.StateStartDate != DateTime.MinValue && !Settings.IsLocked)
            {
                Settings.ToggleLog.Add(new LogToggleEntry
                {
                    IsLocked = Settings.IsLocked,
                    StartTime = Settings.StateStartDate,
                    EndTime = DateTime.Now
                });
            }

            Settings.LockDuration = duration ?? TimeSpan.Zero;
            if (!Settings.IsLocked || Settings.StateStartDate == DateTime.MinValue)
                Settings.StateStartDate = DateTime.Now;
            Settings.IsLocked = true;
        }

        public void UnlockItem()
        {
            if (!Settings.IsLocked || !Settings.Enabled)
                return;
            // If lock state change, save previous status and duration to log before updating
            if (Settings.StateStartDate != DateTime.MinValue && Settings.IsLocked)
            {
                Settings.ToggleLog.Add(new LogToggleEntry
                {
                    IsLocked = Settings.IsLocked,
                    StartTime = Settings.StateStartDate,
                    EndTime = DateTime.Now
                });
            }
            Settings.IsLocked = false;
            Settings.LockDuration = TimeSpan.Zero;
            Settings.StateStartDate = DateTime.Now;
        }

        public string GetStatusMessage()
        {
            if (!Settings.Enabled)
                return string.Empty;
            if (!Settings.IsLocked)
                return Owner.ReplaceMacros(Settings.StatusUnlockedMessage);
            if (Settings.LockDuration == TimeSpan.Zero)
                return Owner.ReplaceMacros(Settings.StatusLockedMessage);
            var unlockTime = Settings.StateStartDate + Settings.LockDuration;
            var timeLeft = unlockTime - DateTime.Now;
            if (timeLeft <= TimeSpan.Zero)
            {
                if (Settings.AutomaticUnlockOnDurationEnd)
                    UnlockItem();
                return Owner.ReplaceMacros($"{Settings.StatusLockOverMessage} Lock expired {StringExtensions.TimeSpanToHumanString(-timeLeft)} ago.");
            }
            else
            {
                return Owner.ReplaceMacros($"{Settings.StatusLockedMessage} Time remaining: {StringExtensions.TimeSpanToHumanString(timeLeft)}.");
            }
        }
    }
}
