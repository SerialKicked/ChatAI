using LetheAISharp;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.SearchAPI;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetheChat.Plugins;
using System.IO;

namespace LetheChat.Files
{
    public class LetheChatSettings : LLMSettings
    {
        public string Skin { get; set; } = "Dark";
        public string BotFile { get; set; } = "Assistant";
        public string UserFile { get; set; } = "User";
        public string PromptFile { get; set; } = "Standard";
        public string Instruct { get; set; } = "ChatML";
        public string SamplerFile { get; set; } = "Default";
        public double Temperature { get; set; } = 0.70;
        public int MaxMessagesOnScreen { get; set; } = 100;
        public int FontSize { get; set; } = 18;
        public bool AlwaysWebSearchQuery { get; set; } = false;
        public bool ShowHiddenMessages { get; set; } = false;
        public string BackgroundFile { get; set; } = "bedroom_cozy.jpg";
        public bool UseTTS { get; set; } = false;
        public bool AsteriskCheck { get; set; } = false;
        public bool AntiSlop { get; set; } = false;
        public float AntiSlopRatio { get; set; } = 1;
        public string[] AntiSlopList { get; set; } = [];
        public bool RemoveCutSentence { get; set; } = false;
        public bool AlwaysForcePasswordOnBotSwitch { get; set; } = false;
        public StringFix RoleplayFormatting { get; set; } = new StringFix(false, false, false, false, false, 1, 50, false, false, false);
        public GroupChatMode GroupChatMode { get; set; } = GroupChatMode.Manual;
        public int GroupChatAutoResponseLimit { get; set; } = 2;
        public bool ManagedLlama { get; set; } = false;

        /// <summary>
        /// Full path (including filename) to the llama.cpp server executable. This is used to launch the server when using the Llama.cpp plugin.
        /// </summary>
        public string PathToLlamaCppServer { get; set; } = string.Empty;

        /// <summary>
        /// Set to true if the backend is using ik_llama.cpp instead of the original llama.cpp. 
        /// This is required to ensure compatibility with ik_llama.cpp, which has some differences in supported command-line arguments compared to the original llama.cpp.
        /// </summary>
        [Description("Only set to true if you're using ik_llama.cpp instead of the original llama.cpp. This ensures compatibility with ik_llama.cpp's supported command-line arguments. Do not check otherwise.")]
        public bool IsIkLlama { get; set; } = false;


        /// <summary>
        /// List of directories to search for GGUF models.
        /// </summary>
        public List<string> ModelDirectories { get; set; } = [];

        /// <summary>
        /// Default settings for the Llama.cpp server. These settings will be applied to all models launched with the Llama.cpp plugin, unless overridden by model-specific settings.
        /// </summary>
        public LlamaCppSettings DefaultLLamaCppSettings { get; set; } = new LlamaCppSettings();
    }

    public enum KVCacheQuantization
    {
        f16,
        q8_0,
        q5_0,
        q4_0
    }

    public class LlamaCppSettings : BaseFile
    {

        /// <summary>
        /// Port is the port that the Llama.cpp server will listen on. The default is 8080, but it can be changed if needed.
        /// </summary>
        [Description("The port the Llama.cpp server listens on. Default: 8080.")]
        public int Port { get; set; } = 8080;

        /// <summary>
        /// Indicates whether the app can edit server properties (this should stay true).
        /// </summary>
        [Description("Allows the app to edit server properties at runtime (--props). Keep this enabled unless you have a specific reason to disable it.")]
        public bool Props { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether FlashAttention optimization is enabled.
        /// </summary>
        /// <remarks>When set to <see langword="true"/>, FlashAttention optimization may be used to
        /// accelerate attention computations if supported by the underlying hardware and software. If set to <see
        /// langword="false"/>, FlashAttention is explicitly disabled. If <see langword="null"/>, the default behavior
        /// is determined automatically by Llama.cpp.</remarks>
        [Description("FlashAttention optimization (-fa). On = force enable, Off = force disable, Auto = let Llama.cpp decide based on hardware support.")]
        public bool? FlashAttention { get; set; } = null;

        /// <summary>
        /// CPU Threads used by Llama.cpp for inference. 
        /// The default is 6, but it can be changed based on the user's CPU capabilities and performance requirements. 
        /// Setting this to a higher value may improve performance on multi-core CPUs, but it may also increase resource usage.
        /// </summary>
        [Description("Number of CPU threads used for inference. Higher values can improve performance on multi-core CPUs but also increase resource usage. Default: 6.")]
        public int Threads { get; set; } = 6;

        /// <summary>
        /// Offload layers to GPU. The default is 255, which means that all layers will be offloaded to the GPU if available.
        /// </summary>
        [Description("Number of model layers to offload to the GPU (-ngl). Set to 255 to offload all layers. Reduce if you run out of VRAM.")]
        public int GpuLayers { get; set; } = 255;

        /// <summary>
        /// Quantization format for the KV cache. The default is f16, which means that the KV cache will be stored in 16-bit floating point format.
        /// </summary>
        [Description("Quantization format for the KV cache. Options: f16 (default), q8_0, q5_0, q4_0.\n\n" + 
            "Lower precision formats reduce GPU memory usage but makes the model less coherent the longer the context is. As such, this is the worst way to free room for the context size.\n\n" +
            "Keep this to F16, or if you're desperate and quality is of little concern to you, Q8_0.")]
        public KVCacheQuantization KVCacheQuantization { get; set; } = KVCacheQuantization.f16;

        /// <summary>
        /// Context size for the model
        /// </summary>
        [Description("Context window size in tokens (-c). Larger values allow longer conversations but use more GPU/CPU memory. Default: 16384.")]
        public int ContextSize { get; set; } = 16384;

        /// <summary>
        /// Enables or disables LLM reasoning mode in Llama.cpp. 
        /// if set to true, Llama.cpp will use a reasoning mode. Set to false, it'll explicitly disable reasoning mode. 
        /// If set to null, the default behavior will be determined automatically by Llama.cpp based on template.
        /// </summary>
        [Description("Reasoning mode (-rea). On = force enable, Off = force disable, Auto = determined by Llama.cpp from the model's chat template.")]
        public bool? Reasoning { get; set; } = null;

        /// <summary>
        /// Reasoning token budget: -1 for unlimited, 0 to disable (same as setting Reasoning to false), or any positive integer to set a specific token budget for reasoning.
        /// </summary>
        [Description("Token budget for reasoning. -1 = unlimited, 0 = disable reasoning, any positive value = cap reasoning to that many tokens.")]
        public int ReasoningBudget { get; set; } = -1;

        /// <summary>
        /// Offload the KV cache to the GPU. This improves performance, but it also increases GPU memory usage proportionally to context size. 
        /// The default is true, which means that the KV cache will be offloaded to the GPU if available.
        /// </summary>
        [Description("Offloads the KV cache to the GPU (-kvo) for better performance. Increases GPU memory usage proportional to context size.")]
        public bool KVcacheToGPU { get; set; } = true;

        /// <summary>
        /// Memory-lock the model in RAM to prevent it from being swapped out to disk.
        /// </summary>
        [Description("Locks the model in RAM to prevent it from being swapped out to disk (--mlock). Useful for consistent inference latency.")]
        public bool mlock { get; set; } = false;

        /// <summary>
        /// Whether to memory-map the model (if mmap disabled, slower load but may reduce pageouts if not using mlock).
        /// </summary>
        [Description("Memory-maps the model file (--mmap). Disabling is slower to load but may reduce page-outs when mlock is not in use.")]
        public bool mmap { get; set; } = true;

        /// <summary>
        /// Use a full size SWA cache instead of the default smaller one. This massively increases memory usage.
        /// </summary>
        [Description("use full-size SWA cache (--swa-full). You probably should leave this alone unless you know what you're doing.")]
        public bool swafull { get; set; } = false;

        /// <summary>
        /// Max number of context checkpoints to create. It's meant to save computation time, but only really works when the model has a fixed system prompt and when the context is not full.
        /// This makes it practically useless, and often counter-productive with Lethe Chat.
        /// </summary>
        [Description("Max number of context checkpoints to create. Only useful if you have a fixed system prompt and your context is not full. Counter-productive with Lethe Chat, so keep this at 0 unless you know what you're doing. Also keep to zero on Gemma4 models.")]
        public int CheckpointCount { get; set; } = 0;

        /// <summary>
        /// Additional command-line arguments to pass to the Llama.cpp server. 
        /// This allows for further customization of the server's behavior by specifying any additional arguments supported by Llama.cpp that are not explicitly exposed as properties in this class.
        /// </summary>
        [Description("Extra command-line arguments passed directly to the Llama.cpp server. Use for any options not explicitly exposed above.")]
        public string AdditionalArgs { get; set; } = string.Empty;

        /// <summary>
        /// If the mmproj file is available in the model directory, load it automatically. 
        /// The mmproj file contains the vision layer, and if it's available, it enables vision capabilities in the model.
        /// </summary>
        [Description("Automatically loads the mmproj (vision) file from the model directory if one is present, enabling multimodal capabilities.")]
        public bool LoadMMprojIfAvailable { get; set; } = true;

        /// <summary>
        /// If a .jinja file is available in the model directory, load it automatically.
        /// This applies a chat template to the model, overriding the one in the gguf file (if any).
        /// </summary>
        [Description("Automatically loads a .jinja chat template from the model directory if present, overriding the template built into the GGUF file.")]
        public bool LoadJinjaIfAvailable { get; set; } = true;

        /// <summary>
        /// Instruction template UniqueID (filename without extension) to use for selected local models. 
        /// </summary>
        /// <remarks>In chat-completion mode, this will ensure better token estimation count and will handle thinking messages properly. In text-completion mode, it is required for the model to work at all.</remarks>
        [Description("Instruction template to use for selected local models. \n\n" +
            "In chat-completion mode, this will ensure better token estimation count and will handle thinking messages properly. In text-completion mode, it is required for the model to work at all.")]
        public string LocalInstructTemplateID { get; set;  } = string.Empty;

        public string GetArgsForDirectory(string modelPath)
        {
            var model = string.Empty;
            var mmproj = string.Empty;
            var jinja = string.Empty;
            // Check if the directory exists and 
            if (!Directory.Exists(modelPath))
                return string.Empty;
            if (LoadMMprojIfAvailable)
            {
                var mmprojFiles = Directory.GetFiles(modelPath, "mmproj*.gguf");
                if (mmprojFiles.Length > 0)
                    mmproj = mmprojFiles[0];
            }
            if (LoadJinjaIfAvailable)
            {
                var jinjaFiles = Directory.GetFiles(modelPath, "*.jinja");
                if (jinjaFiles.Length > 0)
                    jinja = jinjaFiles[0];
            }
            // if so, find the first .gguf file in it (that doesn't start with mmproj)
            var ggufFiles = Directory.GetFiles(modelPath, "*.gguf").Where(f => !Path.GetFileName(f).StartsWith("mmproj", StringComparison.OrdinalIgnoreCase)).ToArray();
            if (ggufFiles.Length > 0)
                model = ggufFiles[0];
            else
                return string.Empty;
            var args = new StringBuilder();

            if (!string.IsNullOrEmpty(mmproj))
                args.Append($" -mm \"{mmproj}\"");
            if (!string.IsNullOrEmpty(jinja))
                args.Append($" --chat-template-file \"{jinja}\"");
            return args.ToString() + GetArgs();
        }

        public string GetArgs()
        {
            var args = new StringBuilder($" {(Program.Settings.IsIkLlama ? string.Empty : "--no-webui")} --reasoning-format none --port {Port} -np 1");
            if (Props && !Program.Settings.IsIkLlama)
                args.Append(" --props");

            if (KVCacheQuantization != KVCacheQuantization.f16)
                args.Append($" -ctk {KVCacheQuantization.ToString()} -ctv {KVCacheQuantization.ToString()}");

            if (FlashAttention.HasValue)
            {
                if (FlashAttention.Value)
                    args.Append(" -fa on");
                else
                    args.Append(" -fa off");
            }

            args.Append($" --ctx-checkpoints {CheckpointCount}");

            if (Threads > 0)
                args.Append($" --threads {Threads}");

            if (GpuLayers > 0)
                args.Append($" -ngl {GpuLayers}");

            if (ContextSize > 0)
                args.Append($" -c {ContextSize}");
            if (Reasoning.HasValue && !Program.Settings.IsIkLlama)
            {
                if (Reasoning.Value)
                    args.Append(" -rea on");
                else
                    args.Append(" -rea off");
            }
            if (!Reasoning.HasValue || Reasoning.Value)
            {
                if (ReasoningBudget >= 0)
                    args.Append($" --reasoning-budget {ReasoningBudget}");
                if (ReasoningBudget >= 1)
                    args.Append(" --reasoning-budget-message \"...\n\nI think I've explored this enough, time to respond.\n\"");
            }

            if (!KVcacheToGPU)
                args.Append(" -nkvo");
            else
            {
                if (!Program.Settings.IsIkLlama)
                    args.Append(" -kvo");
            }

            if (mlock)
                args.Append(" --mlock");

            if (swafull)
                args.Append(" --swa-full");

            if (mmap)
            {
                if (!Program.Settings.IsIkLlama)
                    args.Append(" --mmap");
            }
            else
                args.Append(" --no-mmap");

            if (!string.IsNullOrWhiteSpace(AdditionalArgs))
                args.Append($" {AdditionalArgs}");
            return args.ToString();
        }

    }
}
