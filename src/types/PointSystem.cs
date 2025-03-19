using AIToolkit;
using AIToolkit.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaifuAI.PointSystem
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

        public List<PointUse> Rewards { get; set; } = [];

        public string PointsToString() => $"You currently have {PointCount} {PointName}.";

        public string ListPointUses()
        {
            if (Rewards.Count <= 0)
                return $"There are no uses for your {PointName}.";
            StringBuilder sb = new();
            sb.AppendLinuxLine(PointsToString()).AppendLinuxLine();
            sb.AppendLinuxLine($"Here are the available options:");
            var x = 1;
            var options = new List<PointUse>(Rewards);
            foreach (var use in options)
            {
                if (!string.IsNullOrWhiteSpace(use.Description))
                {
                    sb.AppendLinuxLine($"{x}. {use.Name} - *{use.Description}* - {use.Cost} {PointName}");
                }
                else
                {
                    sb.AppendLinuxLine($"{x}. {use.Name} - {use.Cost} {PointName}");
                }
                x++;
            }
            return sb.ToString();
        }

        public string SpendPointOn(int value)
        {
            var id = value - 1;
            if (id < 0 || id >= Rewards.Count)
                return "Invalid option.";
            var use = Rewards[id];
            if (PointCount < use.Cost)
                return $"You do not have enough {PointName} to get *{use.Name}*.";
            PointCount -= use.Cost;
            return $"You have redeemed *{use.Name}* for {use.Cost} {PointName}.";
        }

        public string SpendPointOn(string name)
        {
            var id = Rewards.FindIndex(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (id < 0 || id >= Rewards.Count)
                return "Invalid option.";
            var use = Rewards[id];
            if (PointCount < use.Cost)
                return $"You do not have enough {PointName} to use this option.";
            PointCount -= use.Cost;
            return $"You have redeemed *{use.Name}* for {use.Cost} {PointName}.";
        }


        public string ProcessCommand(string command)
        {
            var sanitized = command.Trim();
            // Available commands
            // /addpoints X - Add points
            // /rempoints X - Remove points
            // /setpoints X - Set points
            // /listrewards - List rewards
            // /claim X - Claim reward

            if (sanitized.StartsWith("/addpoints"))
            {
                var parts = sanitized.Split(' ');
                if (parts.Length < 2)
                    return "Invalid command.";
                if (int.TryParse(parts[1], out var value))
                {
                    PointCount += value;
                    return $"Added {value} {PointName}. Your new total is: {pointCount}";
                }
                return "Invalid value.";
            }
            else if (sanitized.StartsWith("/rempoints"))
            {
                var parts = sanitized.Split(' ');
                if (parts.Length < 2)
                    return "Invalid command.";
                if (int.TryParse(parts[1], out var value))
                {
                    PointCount -= value;
                    return $"Removed {value} {PointName}. Your new total is: {pointCount}";
                }
                return "Invalid value.";
            }
            else if (sanitized.StartsWith("/setpoints"))
            {
                var parts = sanitized.Split(' ');
                if (parts.Length < 2)
                    return "Invalid command.";
                if (int.TryParse(parts[1], out var value))
                {
                    PointCount = value;
                    return $"Set {PointName} to {value}.";
                }
                return "Invalid value.";
            }
            else if (sanitized.StartsWith("/listrewards"))
            {
                return ListPointUses();
            }
            else if (sanitized.StartsWith("/claim"))
            {
                var parts = sanitized.Split(' ');
                if (parts.Length < 2)
                    return "Invalid command.";
                if (int.TryParse(parts[1], out var value))
                {
                    return SpendPointOn(value);
                }
                else
                {
                    return SpendPointOn(sanitized[6..]);
                }
            }
            return string.Empty;
        }
    }
}
