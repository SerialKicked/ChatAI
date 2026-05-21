using LetheAISharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LetheChat.Files;
using System.ComponentModel;

namespace LetheChat.Plugins
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
        [Description("If set to true, the toggle plugin and interactions are enabled (see documentation).")]
        public bool Enabled { get; set; } = true;

        [Description("The item that the toggle plugin will monitor.")]
        public string ItemToMonitor { get; set; } = "Keypad";
        
        [Description("Message displayed to the character when the monitored item is locked.")]
        public string StatusLockedMessage { get; set; } = "The keypad is locked.";
        
        [Description("Message displayed to the character when the monitored item is unlocked.")]
        public string StatusUnlockedMessage { get; set; } = "The keypad is unlocked.";

        [Description("Message displayed to the character when the monitored item's lock duration has expired.")]
        public string StatusLockOverMessage { get; set; } = "The keypad's lock duration has expired and should now be unlocked.";

        [Description("Indicates whether the monitored item is currently locked.")]
        public bool IsLocked { get; set; } = false;

        [Description("If set to true, the remaining lock duration will be displayed to the character.")]
        public bool ShowLockDuration { get; set; } = false;

        [Description("If set to true, the monitored item will automatically be considered unlocked when the lock duration ends.")]
        public bool AutomaticUnlockOnDurationEnd { get; set; } = false;

        [Description("The duration for which the monitored item should be locked. Set to zero for indefinite lock.")]
        public TimeSpan LockDuration { get; set; } = TimeSpan.Zero;

        [Description("The date and time when the monitored item was last locked or unlocked. Used to calculate lock duration and remaining time.")]
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
            //if (Settings.LockDuration == TimeSpan.Zero)
            //    return Owner.ReplaceMacros(Settings.StatusLockedMessage);
            var unlockTime = Settings.StateStartDate + Settings.LockDuration;
            var timeLeft = unlockTime - DateTime.Now;
            var lockedDuration = DateTime.UtcNow - Settings.StateStartDate;
            if (timeLeft <= TimeSpan.Zero && Settings.LockDuration != TimeSpan.Zero)
            {
                if (Settings.AutomaticUnlockOnDurationEnd)
                    UnlockItem();
                return Owner.ReplaceMacros($"{Settings.StatusLockOverMessage} It's been locked for {StringExtensions.TimeSpanToHumanString(lockedDuration)}. Lock expired {StringExtensions.TimeSpanToHumanString(-timeLeft)} ago.");
            }
            else
            {
                if (Settings.ShowLockDuration)
                {
                    var res = Owner.ReplaceMacros($"{Settings.StatusLockedMessage} It's been locked for {StringExtensions.TimeSpanToHumanString(lockedDuration)}.");
                    if (timeLeft > TimeSpan.Zero)
                        res += $" Time remaining: {StringExtensions.TimeSpanToHumanString(timeLeft)}.";
                    return res;
                }
                else
                {
                    var res = Owner.ReplaceMacros($"{Settings.StatusLockedMessage}.");
                    if (timeLeft > TimeSpan.Zero)
                        res += $" Time remaining: {StringExtensions.TimeSpanToHumanString(timeLeft)}.";
                    return res;
                }
            }
        }
    }
}
