using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetheAISharp.Files;

namespace WaifuAI.Files
{
    internal class ModelMetaData : BaseFile
    {
        public int ContextSize { get; set; } = 8192;
        public int BatchSize { get; set; } = 512;
        public int GPULayers { get; set; } = 255;
        public string DefaultInstructFile { get; set; } = string.Empty;
        public string DefaultInferenceFile { get; set; } = string.Empty;
        public string TaskInstructFile { get; set; } = string.Empty;
        public string TaskInferenceFile { get; set; } = string.Empty;
        public bool FlashAttention { get; set; } = true;
        public int KVCacheCompression { get; set; } = 0;
        public bool UseQuantMatMul  { get; set; } = true;
        public bool LowVRAMMode { get; set; } = false;
        public int HardwareEngine { get; set; } = 0;
        public int GPUID { get; set; } = 1;
        public int Threads { get; set; } = 3;



    }
}
