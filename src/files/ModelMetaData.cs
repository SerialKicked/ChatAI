using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnarkisTools.Files;

namespace WaifuAI.Files
{
    internal class ModelMetaData : BaseFile
    {
        public int ContextSize { get; set; } = 4096;
        public int BatchSize { get; set; } = 512;
        public int GPULayers { get; set; } = 256;
        public string DefaultInstructFile { get; set; } = string.Empty;
        public string DefaultInferenceFile { get; set; } = string.Empty;
        public bool RolePlay { get; set; } = false;
        public bool Aligned { get; set; } = false;
        public string Notes { get; set; } = string.Empty;



    }
}
