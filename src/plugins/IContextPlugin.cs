using WaifuAI.Files;

namespace WaifuAI.Plugins
{
    internal interface IContextPlugin
    {
        bool Enabled { get; set; }

        bool AddToSystemPrompt(string userinput, Chatlog log, out string response);
        bool ReplaceOutput(string botoutput, Chatlog log, out string response);
        Task<PluginResponse> ReplaceUserInput(string userinput);
    }
}