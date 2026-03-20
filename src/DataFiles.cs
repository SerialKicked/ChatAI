using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetheAISharp.Files;
using LetheChat.Files;
using LetheChat.Web;
using LetheAISharp.LLM;
using LetheAISharp.Memory;

namespace LetheChat
{
    static class DataFiles
    {
        public static Dictionary<string, Character> Characters = [];
        public static Dictionary<string, InstructFormat> Instruct = [];
        public static Dictionary<string, SamplerSettings> Inference = [];
        public static Dictionary<string, ModelMetaData> Models = [];
        public static Dictionary<string, WorldInfo> WorldInfos = [];
        public static Dictionary<string, SystemPrompt> SysPrompts = [];
        public static Dictionary<string, WebsiteDefinition> Websites = [];
        public static Dictionary<string, PointSystem> Points = [];

        /// <summary>
        /// Generic Loader for database items
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="pPath">path to look for</param>
        /// <param name="pFilter">filename filter</param>
        /// <returns>string indexed dictionary with T content</returns>
        private static Dictionary<string, T> Load<T>(string pPath, string pFilter, JsonSerializerSettings? serializerSettings = default) where T : IFile
        {
            string[] files;
            if (!Directory.Exists(pPath))
                return [];
            files = Directory.GetFiles(pPath, pFilter);
            var res = new Dictionary<string, T>(files.Length);
            foreach (string f in files)
            {
                var str = File.ReadAllText(f);
                var item = JsonConvert.DeserializeObject<T>(str, serializerSettings);
                if(item != null)
                {
                    var key = Path.GetFileNameWithoutExtension(f);
                    item.UniqueName = key;
                    if (!res.TryAdd(key, item))
                        res[key] = item;
                }
            }
            return res;
        }

        public static void LoadDB()
        {
            Instruct = Load<InstructFormat>("data/instruct/", "*.json");
            Inference = Load<SamplerSettings>("data/params/", "*.json");
            foreach (var item in Inference)
            {
                item.Value.Sampler_order = [6, 0, 1, 3, 4, 2, 5];
            }
            // We should always have a default setting for inference settings
            if (Inference.Count == 0)
            {
                var def = new SamplerSettings() { UniqueName = "Default" };
                Inference[def.UniqueName] = def;
                (def as IFile).SaveToFile("data/params/"+ def.UniqueName + ".json");
            }
            ReloadChars();
            WorldInfos = Load<WorldInfo>("data/worlds/", "*.json");
            SysPrompts = Load<SystemPrompt>("data/sysprompts/", "*.json");
            Websites = Load<WebsiteDefinition>("data/websites/", "*.json");
            Points = Load<PointSystem>("data/pointsystems/", "*.json");
        }

        public static void ReloadChars()
        {
            Characters = Load<Character>("data/chars/", "*.json");
            LLMEngine.LoadPersonas([.. Characters.Values]);
        }
    }
}
