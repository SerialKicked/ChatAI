using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetheChat.Files
{

    internal static class AdvTriggers
    {
        private static readonly List<string> SubTriggers =
        [
            "kitten",
            "name your holes",
            "kneel",
            "on your knees",
            "suck me",
            "submit to me",
            " mask",
            "sensory isolation",
            "your collar",
            "your leash",
            "pet collar",
            "slave collar"
        ];

        private static readonly List<string> DomTriggers =
        [
            "chastity device",
            "chastity cage",
            "buttplug",
            "mistress",
            "mommy",
            "caged",
            "my ass",
            "femboy",
            "please",
            "confession",
            "may i",
            "you're the boss",
            "you are the boss",
            "you should decide"
        ];



        public static bool IsSubmissionTrigger(string input)
        {
            var lowered = input.ToLowerInvariant();
            return SubTriggers.Any(trigger => lowered.Contains(trigger));
        }
        public static bool IsDomTrigger(string input)
        {
            var lowered = input.ToLowerInvariant();
            if (DomTriggers == null)
                return false;
            return DomTriggers?.Any(trigger => lowered.Contains(trigger)) == true;
        }
    }

    public class AdvancedMoodState : MoodState
    {
        private double horniness = 0.25;
        private double submission = 0.5;
        private double sanity = 1;

        public double Horniness
        {
            get => horniness;
            set
            {
                horniness = value;
                horniness = Math.Clamp(horniness, 0, 1);
            }
        }

        public double Submission
        {
            get => submission;
            set
            {
                submission = value;
                submission = Math.Clamp(submission, 0, 1);
            }
        }

        public double Sanity
        {
            get => sanity;
            set
            {
                sanity = value;
                sanity = Math.Clamp(sanity, 0, 1);
            }
        }

        public override void Update()
        {
            base.Update();
            // Horniness naturally increases over time
            Horniness += (1 - Horniness) * 0.0005;
            
            // Sanity naturally recovers over time
            Sanity += (1 - Sanity) * 0.00025;

            // Submission gets to a neutral state over time
            Submission += ((0.5 - Submission) * 0.0005);

            // Special cases based on time since last message exchanged
            var msg = LLMEngine.History.GetLastMessageFrom(AuthorRole.User);
            if (msg != null)
            {
                var timeSinceLast = (DateTime.Now - msg.Date);

                if (timeSinceLast > TimeSpan.FromDays(7))
                {
                    // Long gap means that horniness spikes, submission drops
                    Horniness += 0.05 * timeSinceLast.TotalDays;
                    Submission -= 0.05 * timeSinceLast.TotalDays;
                }
                else if (timeSinceLast >= TimeSpan.FromDays(1))
                {
                    // Recent interaction increases cheer
                    Horniness += 0.025 * timeSinceLast.TotalDays;
                    Submission -= 0.025 * timeSinceLast.TotalDays;
                }
                Sanity += 0.025 * timeSinceLast.TotalDays;
            }
        }

        public override void Interpret(string userMessage)
        {
            base.Interpret(userMessage);
            var lowered = userMessage.ToLowerInvariant();
            if (AdvTriggers.IsSubmissionTrigger(lowered))
            {
                Submission += 0.01;
                Horniness += 0.01;
            }
            else if (AdvTriggers.IsDomTrigger(lowered) == true)
            {
                Submission -= 0.01;
            }
            // get last bot message
            if (LLMEngine.History.GetLastFromInSession(AuthorRole.Assistant) is SingleMessage msg && msg.Message.Contains('?'))
            {
                // If the last message from the assistant was a question, decrease curiosity
                Curiosity -= 0.0025;
            }
        }

        protected override List<string> GetAdjectives()
        {
            var adjectives = base.GetAdjectives();
            // horniness 0.0 - 0.2 : nothing. 0.2 - 0.4 : "flirty". 0.4 - 0.6 : "horny", 0.6 - 0.8 : "lustful", 0.8 - 1.0 : "desperate"
            if (Horniness < 0.35)
            {
                // no adjective
            }
            else if (Horniness < 0.45)
            {
                adjectives.Add("kinda flirty");
            }
            else if (Horniness < 0.55)
            {
                adjectives.Add("flirty");
            }
            else if (Horniness < 0.65)
            {
                adjectives.Add("horny");
            }
            else if (Horniness < 0.85)
            {
                adjectives.Add("lustful");
            }
            else
            {
                if (Sanity < 0.5 && Submission > 0.85)
                {
                    adjectives.Add("like a desperate sextoy");
                }
                else
                {
                    adjectives.Add("sex-starved");
                }
            }

            // submission 0.0 - 0.15 : "dominant", 0.15 - 0.35 : "assertive", 0.35 - 0.65 : nothing, 0.65 - 0.85 : "submissive", 0.85 - 1.0 : "obedient"
            if (Submission < 0.15)
            {
                if (Horniness > 0.75)
                {
                    adjectives.Add("in a mood to use {{user}} like a toy");
                }
                else
                {
                    adjectives.Add("in a dominant mood");
                }
            }
            else if (Submission < 0.35)
            {
                adjectives.Add("in an assertive mood");
            }
            else if (Submission < 0.65)
            {
                // no adjective
            }
            else if (Submission < 0.85)
            {
                adjectives.Add("submissive");
            }
            else
            {
                if (Sanity < 0.2)
                {
                    adjectives.Add("like a mind-broken slave");
                }
                else
                {
                    adjectives.Add("very submissive");
                }
            }

            return adjectives;
        }

    }
}
