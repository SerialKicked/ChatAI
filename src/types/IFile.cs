using Newtonsoft.Json;

namespace WaifuAI.Files
{
    internal interface IFile
    {
        string UniqueName { get; set; }
        static JsonSerializerSettings JsonSettings => new() { Formatting = Formatting.Indented };

        string ExportToString() => JsonConvert.SerializeObject(this, JsonSettings);

        void SaveToFile(string pPath) => File.WriteAllText(pPath, ExportToString());
    }
}