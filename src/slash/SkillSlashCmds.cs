using LetheAISharp;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Skills;
using System.Text;

namespace LetheChat.Slash
{
    internal class CmdSkill(ISlashCommand owner) : SlashCommandInfo(owner)
    {
        public override string ID => "/skill";
        public override string Description => "Skills - Manage the skill library (list, show, load, create, edit)";
        public override string Slash => "/skill [list | show <name> | load <name> | create [focus] | edit [name]]";

        private static SlashReturn Info(string text, bool log = false) 
            => new(new SingleMessage(AuthorRole.System, text), log, true, true);

        public override SlashReturn Execute(string userinput)
        {
            var remainder = userinput.Length > ID.Length ? userinput[ID.Length..].Trim() : string.Empty;
            var parts = remainder.Split([' '], 2, StringSplitOptions.RemoveEmptyEntries);
            var sub = parts.Length > 0 ? parts[0].ToLowerInvariant() : "list";
            var arg = parts.Length > 1 ? parts[1].Trim() : string.Empty;
            var manager = LLMEngine.SkillManager;

            switch (sub)
            {
                case "list":
                    return Info(BuildList(manager));

                case "show":
                case "load":
                    {
                        if (arg.Length == 0)
                            return new SlashReturn(GetHelpMessage(), false, true, true);
                        var skill = manager.Get(arg);
                        if (skill == null)
                            return Info($"Skill '{arg}' not found. Use /skill list to see the available skills.");
                        if (sub == "show")
                            return Info($"**{skill.Name}**" + (skill.Enabled ? "" : " (disabled)") + LLMEngine.NewLine + skill.Description + LLMEngine.NewLine + LLMEngine.NewLine + skill.Body);
                        // load: insert the skill in the chat as a system message so the model follows it
                        var msg = $"# Skill: {skill.Name}{LLMEngine.NewLine}{LLMEngine.NewLine}{skill.Body}";
                        return new SlashReturn(new SingleMessage(AuthorRole.System, msg), true, true, true);
                    }

                case "create":
                case "new":
                    {
                        if (LLMEngine.Status == SystemStatus.Busy)
                            return Info("The model is busy, try again once it has finished.");
                        var form = Program.BigForm!;
                        form.BeginInvoke(async () => await form.BeginSkillDraft(arg));
                        return Info("Writing a skill from the current chat. The skill editor will open once the draft is ready.");
                    }

                case "edit":
                    {
                        var form = Program.BigForm!;
                        var name = arg.Length > 0 ? manager.Get(arg)?.Name : null;
                        form.BeginInvoke(() => form.OpenSkillEditor(name));
                        return Info(arg.Length > 0 && name == null ? $"Skill '{arg}' not found, opening the skill editor." : "Opening the skill editor.");
                    }

                default:
                    return new SlashReturn(GetHelpMessage(), false, true, true);
            }
        }

        private static string BuildList(SkillManager manager)
        {
            var all = manager.All;
            if (all.Count == 0)
                return "**Skills**" + LLMEngine.NewLine + "The skill library is empty. Use /skill create to write one from the current chat.";
            var sb = new StringBuilder("**Skills**");
            sb.AppendLinuxLine();
            foreach (var s in all)
            {
                sb.AppendLinuxLine($"- **{s.Name}**{(s.Enabled ? string.Empty : " (disabled)")}: {s.Description}");
            }
            return sb.ToString();
        }
    }

    public class SkillSlashCmds : ISlashCommand
    {
        public List<SlashCommandInfo> Commands { get; }
        public bool FirstLineOnly => false;

        public SkillSlashCmds()
        {
            Commands = [new CmdSkill(this)];
        }

        public SlashReturn RunCommand(string userinput)
        {
            if (userinput.Length > 0)
            {
                var firstword = userinput.Split(' ', '\n')[0];
                foreach (var item in Commands)
                {
                    if (firstword.Equals(item.ID, StringComparison.OrdinalIgnoreCase))
                        return item.Execute(userinput);
                }
            }
            return new SlashReturn(null, false, false);
        }
    }
}
