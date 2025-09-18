using LetheAISharp;
using LetheAISharp.Files;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaifuAI.Files
{
    public enum PointEvents { DailyLogin, MessageSent }

    public class PointUse
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Cost { get; set; } = 0;
    }

    public class PointSystem : BaseFile
    {
        private const int IGNORE_POINT_LIMIT = -1;
        private int pointCount = 0;

        public int PointCount { 
            get => pointCount;
            set
            {
                if (PointUpperLimit != IGNORE_POINT_LIMIT && value > PointUpperLimit)
                    pointCount = PointUpperLimit;
                else if (PointLowerLimit != IGNORE_POINT_LIMIT && value < PointLowerLimit)
                    pointCount = PointLowerLimit;
                else
                    pointCount = value;
            } 
        }
        public int PointUpperLimit { get; set; } = IGNORE_POINT_LIMIT;
        public int PointLowerLimit { get; set; } = IGNORE_POINT_LIMIT;
        public string PointName { get; set; } = "Gold";
        public string PointDescription { get; set; } = "Used as a form currency.";
        public bool PersistentMessages { get; set; } = false;

        public List<PointUse> Rewards { get; set; } = [];
        public List<PointUse> Spendings { get; set; } = [];

        public string PointsToString() => $"You currently have {PointCount} {PointName}.";

        public string ListPointUses()
        {
            if (Rewards.Count <= 0)
                return $"There are no preset way to gain {PointName} at the moment.";
            StringBuilder sb = new();
            sb.AppendLinuxLine(PointsToString()).AppendLinuxLine();
            sb.AppendLinuxLine($"Here are the available options to gain {PointName}:");
            var x = 1;
            var options = new List<PointUse>(Rewards);
            foreach (var use in options)
            {
                if (!string.IsNullOrWhiteSpace(use.Description))
                {
                    sb.AppendLinuxLine($"{x}. {use.Name} - *{use.Description}* - Gain {use.Cost} {PointName}");
                }
                else
                {
                    sb.AppendLinuxLine($"{x}. {use.Name} - Gain {use.Cost} {PointName}");
                }
                x++;
            }
            return sb.ToString();
        }

        public string ListRules()
        {
            StringBuilder sb = new();
            sb.AppendLinuxLine(PointDescription).AppendLinuxLine(PointsToString());
            if (Rewards.Count > 0)
            {
                sb.AppendLinuxLine();
                sb.AppendLinuxLine($"Here are the available options to gain {PointName}: (use /claim $ID to select option)");
                var x = 1;
                var options = new List<PointUse>(Rewards);
                foreach (var use in options)
                {
                    if (!string.IsNullOrWhiteSpace(use.Description))
                    {
                        sb.AppendLinuxLine($"{x}. {use.Name} - *{use.Description}* - Gain {use.Cost} {PointName}");
                    }
                    else
                    {
                        sb.AppendLinuxLine($"{x}. {use.Name} - Gain {use.Cost} {PointName}");
                    }
                    x++;
                }
            }
            if (Spendings.Count > 0)
            {
                sb.AppendLinuxLine();
                sb.AppendLinuxLine($"Here are the available ways to spend {PointName}: (use /spend $ID to select option)");
                var x = 1;
                var options = new List<PointUse>(Spendings);
                foreach (var use in options)
                {
                    if (!string.IsNullOrWhiteSpace(use.Description))
                    {
                        sb.AppendLinuxLine($"{x}. {use.Name} - *{use.Description}* - Cost {use.Cost} {PointName}");
                    }
                    else
                    {
                        sb.AppendLinuxLine($"{x}. {use.Name} - Cost {use.Cost} {PointName}");
                    }
                    x++;
                }
            }
            return sb.ToString().CleanupAndTrim();
        }


        public string ListPointSpendings()
        {
            if (Spendings.Count <= 0)
                return $"There are no preset way to spend your {PointName} at the moment.";
            StringBuilder sb = new();
            sb.AppendLinuxLine(PointsToString()).AppendLinuxLine();
            sb.AppendLinuxLine($"Here are the available ways to spend your {PointName}:");
            var x = 1;
            var options = new List<PointUse>(Spendings);
            foreach (var use in options)
            {
                if (!string.IsNullOrWhiteSpace(use.Description))
                {
                    sb.AppendLinuxLine($"{x}. {use.Name} - *{use.Description}* - Cost {use.Cost} {PointName}");
                }
                else
                {
                    sb.AppendLinuxLine($"{x}. {use.Name} - Cost {use.Cost} {PointName}");
                }
                x++;
            }
            return sb.ToString();
        }

        public string GainPointsFor(int value)
        {
            var id = value - 1;
            if (id < 0 || id >= Rewards.Count)
                return "Invalid option.";
            var use = Rewards[id];
            PointCount += use.Cost;
            return $"You selected *{use.Name}* ({use.Description}), giving you {use.Cost} {PointName}. You now have {PointCount} {PointName} left.";
        }

        public string GainPointsFor(string name)
        {
            var clean = name.Trim();
            var id = Rewards.FindIndex(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (id < 0 || id >= Rewards.Count)
                return "Invalid option.";
            var use = Rewards[id];
            PointCount += use.Cost;
            return $"You selected *{use.Name}* ({use.Description}), giving you {use.Cost} {PointName}. You now have {PointCount} {PointName} left.";
        }

        public string SpendPoints(int value)
        {
            var id = value - 1;
            if (id < 0 || id >= Spendings.Count)
                return "Invalid option.";
            var use = Spendings[id];
            PointCount -= use.Cost;
            return $"You selected *{use.Name}* ({use.Description}). Removing {use.Cost} {PointName}. You now have {PointCount} {PointName} left.";
        }

        public string SpendPoints(string name)
        {
            var clean = name.Trim();
            var id = Spendings.FindIndex(x => x.Name.Equals(clean, StringComparison.OrdinalIgnoreCase));
            if (id < 0 || id >= Spendings.Count)
                return "Invalid option.";
            var use = Spendings[id];
            PointCount -= use.Cost;
            return $"You selected *{use.Name}* ({use.Description}). Removing {use.Cost} {PointName}. You now have {PointCount} {PointName} left.";
        }


        public string ProcessCommand(string command)
        {
            var sanitized = command.Trim();
            if (sanitized.StartsWith("/addpoints"))
            {
                var parts = sanitized.Split(' ');
                if (parts.Length < 2)
                    return "{{user}} tried to call /addpoints but no value was provided.";
                if (int.TryParse(parts[1], out var value))
                {
                    PointCount += value;
                    return $"Added {value} {PointName}. Your new total is: {PointCount}";
                }
                return "{{user}} tried to call /addpoints but incorrect value was provided.";
            }
            else if (sanitized.StartsWith("/getpoints"))
            {
                return $"You have {PointCount} {PointName}.";
            }
            else if (sanitized.StartsWith("/rempoints"))
            {
                var parts = sanitized.Split(' ');
                if (parts.Length < 2)
                    return "{{user}} tried to call /rempoints but no value was provided.";
                if (int.TryParse(parts[1], out var value))
                {
                    PointCount -= value;
                    return $"Removed {value} {PointName}. Your new total is: {PointCount}";
                }
                return "{{user}} tried to call /rempoints but incorrect value was provided.";
            }
            else if (sanitized.StartsWith("/setpoints"))
            {
                var parts = sanitized.Split(' ');
                if (parts.Length < 2)
                    return "{{user}} tried to call /setpoints but no value was provided.";
                if (int.TryParse(parts[1], out var value))
                {
                    PointCount = value;
                    return $"You now have {PointCount} {PointName}.";
                }
                return "{{user}} tried to call /setpoints but incorrect value was provided.";
            }
            else if (sanitized.StartsWith("/listrewards"))
            {
                return ListPointUses();
            }
            else if (sanitized.StartsWith("/listexpenses"))
            {
                return ListPointSpendings();
            }
            else if (sanitized.StartsWith("/listrules"))
            {
                return ListRules();
            }
            else if (sanitized.StartsWith("/claim"))
            {
                var parts = sanitized.Split(' ');
                if (parts.Length < 2)
                    return "Invalid command.";
                if (int.TryParse(parts[1], out var value))
                {
                    return GainPointsFor(value);
                }
                else
                {
                    return GainPointsFor(sanitized[6..]);
                }
            }
            else if (sanitized.StartsWith("/spend"))
            {
                var parts = sanitized.Split(' ');
                if (parts.Length < 2)
                    return "Invalid command.";
                if (int.TryParse(parts[1], out var value))
                {
                    return SpendPoints(value);
                }
                else
                {
                    return SpendPoints(sanitized[6..]);
                }
            }
            else if (sanitized.StartsWith("/pointhelp"))
            {
                return "Available commands:\n" +
                    "/getpoints - View current points\n" +
                    "/addpoints X - Add points\n" +
                    "/rempoints X - Remove points\n" +
                    "/setpoints X - Set points\n" +
                    "/listrewards - List actions giving points\n" +
                    "/listexpenses - List actions removing points\n" +
                    "/listrules - List all rules (rewards + spendings) \n" +
                    "/claim X - Claim points from reward list entry\n" +
                    "/spend X - Lose points from expense list entry\n";
            }
            return string.Empty;
        }
    }
}
