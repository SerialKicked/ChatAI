using LetheAISharp.LLM;
using WaifuAI.Plugins;

namespace WaifuAI.Files
{
    public interface ICharacter : IBasePersona
    {
        List<string> AllowedSamplers { get; set; }
        new CharBrain Brain { get; }
        bool CanInitiateChat { get; set; }
        string Icon { get; set; }
        TimedLockPlugin LockManager { get; }
        ToggleMonitorSettings LockSettings { get; set; }
        string PointSystem { get; set; }
        int PointValue { get; set; }
        Image Portrait { get; }
        bool Protected { get; set; }
        string TTSVoice { get; set; }
        PointSystem MyPoints { get; set; }

        void ClearChatHistory(bool deletefile = true);
    }
}