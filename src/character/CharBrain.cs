using LetheAISharp.Agent;
using LetheAISharp.Files;
using LetheAISharp.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaifuAI.AgentPlugins;
using WaifuAI.GBNF;

namespace WaifuAI.Files
{
    internal class CharBrain(BasePersona basePersona) : Brain(basePersona)
    {
        public new AdvancedMoodState Mood { get; set; } = new AdvancedMoodState();

        public override async Task ProcessPreviousSession()
        {
            await base.ProcessPreviousSession().ConfigureAwait(false);
            // Ensure there is a previous session to analyze and that we're not editing an old one
            if (Owner.History.Sessions.Count < 2 ||
                (Owner.History.CurrentSessionID != -1 && Owner.History.CurrentSessionID != Owner.History.Sessions.Count - 1))
                return;

            var prevsession = Owner.History.Sessions[^2];
            
            if (prevsession.MetaData.IsRoleplaySession && prevsession.Messages.Count >= 10)
                Mood.Horniness -= (0.015 * prevsession.Messages.Count);

            // Analyze previous session for triggers
            var action = AgentRuntime.GetAction<MoodAnalysis?, SessionMoodCheckParams>("SessionMoodCheckAction");
            if (action is null)
                return;

            var result = await action.Execute(new SessionMoodCheckParams(prevsession), CancellationToken.None);
            if (result is null)
                return;

            static double Delta(Modifier m) => m switch
            {
                Modifier.HighReduction  => -0.2,
                Modifier.SmallReduction => -0.1,
                Modifier.SmallIncrease  =>  0.1,
                Modifier.HighIncrease   =>  0.2,
                _ => 0.0
            };

            Mood.Horniness  += Delta(result.Horniness);
            Mood.Submission += Delta(result.Submission);
            Mood.Energy     += Delta(result.Energy);
            Mood.Cheer      += Delta(result.Cheer);
            Mood.Curiosity  += Delta(result.Curiosity);
            Mood.Sanity     += Delta(result.Sanity);
        }
    }
}
