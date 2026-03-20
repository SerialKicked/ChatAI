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
            var dirlist = Program.Settings.LlamaCppServerDirectories;
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
                    AvailModels.Add(new LocalModel { ModelFile = item });
            }
        }
    }
}
