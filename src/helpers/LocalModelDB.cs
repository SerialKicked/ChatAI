using LetheAISharp.Files;
using LetheChat.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LetheChat
{

    public class LocalModel
    {
        public string FileName => Path.GetFileName(ModelFile);
        public string ModelFile { get; set; } = string.Empty;
        public LlamaCppSettings Settings { get; set; } = new LlamaCppSettings();

        public string GetLlamaCppArguments()
        {
            var args = new StringBuilder($" -m \"{ModelFile}\" ");
            args.Append(Settings.GetArgsForDirectory(Path.GetDirectoryName(ModelFile)!));
            return args.ToString();
        }

        public bool IsJinjaFilePresent()
        {
            // get directory of the model file
            var dir = Path.GetDirectoryName(ModelFile);
            if (string.IsNullOrEmpty(dir))
                return false;

            // check if any .jinja file exists in the directory
            var jinjaFiles = Directory.GetFiles(dir, "*.jinja", SearchOption.TopDirectoryOnly);
            return jinjaFiles.Length > 0;
        }

        public bool IsMMProjFilePresent()
        {
            // get directory of the model file
            var dir = Path.GetDirectoryName(ModelFile);
            if (string.IsNullOrEmpty(dir))
                return false;
            // check if any .mmproj file exists in the directory
            var mmprojFiles = Directory.GetFiles(dir, "mmproj*.gguf", SearchOption.TopDirectoryOnly);
            return mmprojFiles.Length > 0;
        }
    }


    public class LocalModelDB : BaseFile
    {
        public List<LocalModel> AvailModels { get; set; } = [];

        public void PruneModels()
        {
            AvailModels.RemoveAll(m => !File.Exists(m.ModelFile));
        }

        public void SearchModels(bool clearExisting)
        {
            var dirlist = Program.Settings.ModelDirectories;
            var foundfiles = new List<string>();
            // go through each directory and find all files named "*.gguf" no matter their depth (that doesn't start with "mmproj")
            foreach (var dir in dirlist)
                if (Directory.Exists(dir))
                {
                    var files = Directory.GetFiles(dir, "*.gguf", SearchOption.AllDirectories);
                    foundfiles.AddRange(files.Where(f => !Path.GetFileName(f).StartsWith("mmproj", StringComparison.OrdinalIgnoreCase)));
                }

            if (clearExisting)
                AvailModels.Clear();

            foreach (var item in foundfiles)
            {
                var found = AvailModels.Find(m => m.ModelFile == item);
                if (found is null)
                {
                    var model = new LocalModel
                    {
                        ModelFile = item,
                        Settings = Program.Settings.DefaultLLamaCppSettings.Copy<LlamaCppSettings>() ?? new LlamaCppSettings()
                    };
                    AvailModels.Add(model);
                }
            }

            // sort the list by filename
            AvailModels.Sort((a, b) => string.Compare(a.FileName, b.FileName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
