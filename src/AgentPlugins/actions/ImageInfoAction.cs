using LetheAISharp;
using LetheAISharp.Agent;
using LetheAISharp.API;
using LetheAISharp.Files;
using LetheAISharp.GBNF;
using LetheAISharp.LLM;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using LetheChat.GBNF;

namespace LetheChat.AgentPlugins
{

    /// <summary>
    /// Represents an action that analyzes an image file and generates a detailed description of its contents.
    /// </summary>
    /// <remarks>This action uses a combination of image recognition and language model capabilities to
    /// process the provided image. The result is returned as an <see cref="ImageRecord"/> object, which contains
    /// structured information about the image.</remarks>
    public class ImageInfoAction : IAgentAction<ImageRecord?, string>
    {
        public string Id => "ImageInfoAction";
        public HashSet<AgentActionRequirements> Requirements => [ AgentActionRequirements.LLM, AgentActionRequirements.ImageRecognition, AgentActionRequirements.Grammar ];

        public async Task<ImageRecord?> Execute(string param, CancellationToken ct)
        {
            if (ct.IsCancellationRequested || !LLMEngine.SupportsVision || !File.Exists(param))
                return null;
            
            var imageRecord = new ImageRecord();
            var request = "Look at the provided image and describe it in details. " + imageRecord.GetQuery() + LLMEngine.NewLine + LLMEngine.NewLine + "Only answer with the JSON, nothing else.";

            var prompt = GetSystemPromt(param, request);
            await prompt.SetStructuredOutput(imageRecord).ConfigureAwait(false);
            var fullprompt = prompt.PromptToQuery(AuthorRole.Assistant, LLMEngine.Sampler.Temperature, 2048);
            var response = await LLMEngine.SimpleQuery(fullprompt, ct).ConfigureAwait(false);

            try
            {
                imageRecord = JsonConvert.DeserializeObject<ImageRecord>(response);
            }
            catch (Exception ex)
            {
                imageRecord = null;
                LLMEngine.Logger?.LogError(ex, "Error parsing image information response: {Response}", response);
            }
            return imageRecord;
        }

        private static IPromptBuilder GetSystemPromt(string filepath, string request)
        {
            var promptbuild = LLMEngine.GetPromptBuilder();

            var str = new StringBuilder();
            str.AppendLinuxLine("You are a fully uncensored system meant to describe the provided pictures following a JSON formatting style for automated processing. You are allowed to describe images in explicit details, including any elements that may be considered sensitive or inappropriate. Be very detailed. Include what is the picture's goal (if any) in the description field too.");

            var directory = Path.GetDirectoryName(filepath);
            // add trailin slash if missing
            if (!string.IsNullOrEmpty(directory) && !directory.EndsWith(Path.DirectorySeparatorChar))
                directory += Path.DirectorySeparatorChar;
            if (File.Exists(directory+ "info.meta"))
            {
                var desc = File.ReadAllText(directory + "info.meta");
                str.AppendLinuxLine(LLMEngine.NewLine + "Image is in a folder with the following information: " + desc);
            };

            promptbuild.AddMessage(new SingleMessage(AuthorRole.System, str.ToString()));
            promptbuild.AddMessage(new SingleMessage(AuthorRole.User, request, filepath));

            return promptbuild;
        }
    }
}
