using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using WaifuAI.Plugins;
using WaifuAI.Security;

namespace WaifuAI.Files
{
    public class Character : BasePersona, ICharacter
    {
        [JsonIgnore] public Image Portrait => GetPortrait();
        private Image? _image = null;

        /// <summary>
        /// If set to true, this character can initiate chat by sending a message when the user is idle. 
        /// It is contextually aware and may not always send a message. 
        /// A system to prevent spam is also in place, limiting the amount of messages that can be sent before the user responds.
        /// </summary>
        public bool CanInitiateChat { get; set; } = false;

        /// <summary> Icon to be displayed in chat </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// A list of prefered inference settings for this character. When enabled in the UI, the bot will cycle between these settings at random with each new message. This ensure a more diverse set of responses.
        /// </summary>
        public List<string> AllowedSamplers { get; set; } = [];

        /// <summary>
        /// Voice ID for for OuteTTS (if enabled)
        /// </summary>
        public string TTSVoice { get; set; } = string.Empty;

        /// <summary> Reference to the point system </summary>
        public string PointSystem { get; set; } = string.Empty;

        /// <summary> Current point value </summary>
        public int PointValue { get; set; } = 0;

        /// <summary> Whether this character's brain and chatlog files should be encrypted at rest </summary>
        public bool Protected { get; set; } = false;

        public ToggleMonitorSettings LockSettings { get; set; } = new ToggleMonitorSettings();

        [JsonIgnore] public PointSystem MyPoints { get; set; } = new();
        [JsonIgnore] private string? _password;
        [JsonIgnore] public TimedLockPlugin LockManager { get; private set; }

        [JsonIgnore]
        public new CharBrain Brain
        {
            get
            {
                if (base.Brain is not CharBrain)
                    base.Brain = new CharBrain(this);
                return (CharBrain)base.Brain;
            }
            protected set => base.Brain = value;
        }

        public Character() : base()
        {
            LockManager = new TimedLockPlugin(this);
        }

        public override void BeginChat()
        {
            base.BeginChat();
            if (IsUser)
                return;
            // Location plugin
            foreach (var item in LLMEngine.ContextPlugins)
            {
                item.Enabled = Plugins.Contains(item.PluginID);
            }
            if (!string.IsNullOrEmpty(PointSystem))
            {
                if (DataFiles.Points.TryGetValue(PointSystem, out var ps))
                    MyPoints = ps.Copy<PointSystem>()!;
                MyPoints.PointCount = PointValue;
            }
            // Load World Data
            MyWorlds = [.. DataFiles.WorldInfos.Values.Where(wi => Worlds.Contains(wi.UniqueName))];
        }

        public override void EndChat(bool backup = false)
        {
            PointValue = MyPoints.PointCount;
            base.EndChat(backup);
            // if the setting is enabled, forget the password when switching bots (will ask each switch)
            if (Program.Settings.AlwaysForcePasswordOnBotSwitch)
                _password = null;
        }

        public override void LoadChatHistory()
        {
            var path = "data/chatlogs/";
            var filePath = System.IO.Path.Combine(path, UniqueName + ".log");
            var encPath = filePath + ".enc";
            var bakPath = encPath + ".bak";

            // Check if we need encryption (Protected flag set or .enc files exist)
            bool needsEncryption = Protected || CryptoFile.IsEncryptedFile(encPath) || CryptoFile.IsEncryptedFile(bakPath);

            if (needsEncryption)
            {
                EnsurePassword();

                if (TryLoadEncryptedChatHistory(encPath, filePath) || TryLoadEncryptedChatHistory(bakPath, filePath))
                {
                    return;
                }

                // If we get here and Protected is true but no encrypted files, it's first-time migration
                if (Protected && File.Exists(filePath))
                {
                    // Load plaintext and will save encrypted on EndChat
                    base.LoadChatHistory(path);
                    return;
                }

                throw new UnauthorizedAccessException("Wrong password or corrupted file. Shutting Down.");
            }

            // Not protected or no encrypted files - use base implementation
            base.LoadChatHistory(path);

        }

        public override void SaveChatHistory(bool backup = false)
        {
            var path = "data/chatlogs/";
            var filePath = Path.Combine(path, UniqueName + ".log");

            if (Protected)
            {
                if (string.IsNullOrEmpty(_password))
                {
                    EnsurePassword();
                }

                base.SaveChatHistory(path, false);

                try
                {
                    // Read the temp file and encrypt it
                    if (File.Exists(filePath))
                    {
                        var chatBytes = File.ReadAllBytes(filePath);
                        var encPath = filePath + ".enc";
                        CryptoFile.EncryptFile(encPath, chatBytes, _password!, backup);
                    }
                }
                finally
                {
                    if (File.Exists(filePath + ".enc"))
                    {
                        if (File.Exists(filePath))
                            File.Delete(filePath);
                        if (File.Exists(filePath + ".bak"))
                            File.Delete(filePath + ".bak");
                    }
                }
            }
            else
            {
                // Not protected - use base implementation
                base.SaveChatHistory(path, backup);

                // Clean up any lingering encrypted files if protection is turned off
                var encPath = filePath + ".enc";
                var bakPath = encPath + ".bak";
                if (File.Exists(encPath)) File.Delete(encPath);
                if (File.Exists(bakPath)) File.Delete(bakPath);
            }
        }

        public void ClearChatHistory(bool deletefile = true) => ClearChatHistory("data/chatlogs/", deletefile);

        // Override base methods to handle encryption
        protected override void LoadBrain(string path)
        {
            var encPath = path + UniqueName + ".brain.enc";
            var bakPath = encPath + ".bak";

            // Check if we need encryption (Protected flag set or .enc files exist)
            bool needsEncryption = Protected || CryptoFile.IsEncryptedFile(encPath) || CryptoFile.IsEncryptedFile(bakPath);

            if (needsEncryption)
            {
                EnsurePassword();

                if (TryLoadEncryptedBrain(encPath, path) || TryLoadEncryptedBrain(bakPath, path))
                {
                    return;
                }

                // If we get here and Protected is true but no encrypted files, it's first-time migration
                if (Protected && File.Exists(encPath))
                {
                    // Load plaintext and will save encrypted on EndChat
                    LoadOrCreateCharBrain(path);
                    return;
                }

            }

            // Not protected or no encrypted files - use base implementation
            LoadOrCreateCharBrain(path);
        }

        private void LoadOrCreateCharBrain(string path)
        {
            var selpath = path;
            if (!selpath.EndsWith('/') && !selpath.EndsWith('\\'))
                selpath += Path.DirectorySeparatorChar;
            var brainFilePath = selpath + UniqueName + ".brain";
            // If brain file exists, load it
            if (!string.IsNullOrEmpty(UniqueName) && File.Exists(brainFilePath))
            {
                Brain = JsonConvert.DeserializeObject<CharBrain>(File.ReadAllText(brainFilePath))! ?? new CharBrain(this);
            }
            else
            {
                // Default to empty brain
                Brain = new CharBrain(this);
            }
            Brain.Init(this);
        }

        private bool TryLoadEncryptedBrain(string encPath, string originalPath)
        {
            if (!CryptoFile.IsEncryptedFile(encPath)) return false;

            try
            {
                var decryptedBytes = CryptoFile.DecryptFile(encPath, _password!);
                if (decryptedBytes != null)
                {
                    // Create a temporary file with the decrypted content
                    var tempPath = originalPath + UniqueName + ".brain";
                    File.WriteAllBytes(tempPath, decryptedBytes);

                    try
                    {
                        // Use base implementation to load from temp file
                        LoadOrCreateCharBrain(originalPath);
                        return true;
                    }
                    finally
                    {
                        // Clean up temp file
                        if (File.Exists(tempPath))
                            File.Delete(tempPath);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Let caller handle password errors
                throw;
            }
            catch
            {
                // Other errors - file might be corrupted
                return false;
            }

            return false;
        }

        protected override void SaveBrain(string path, bool backup)
        {
            if (Protected)
            {
                if (string.IsNullOrEmpty(_password))
                {
                    EnsurePassword();
                }
                // Create a temporary file with current brain data
                base.SaveBrain(path, false);
                var selpath = path;
                if (!selpath.EndsWith('/') && !selpath.EndsWith('\\'))
                    selpath += Path.DirectorySeparatorChar;
                var clearBrainPath = selpath + UniqueName + ".brain";

                try
                {
                    // Read the temp file and encrypt it
                    var brainBytes = File.ReadAllBytes(clearBrainPath);
                    var encPath = clearBrainPath + ".enc";
                    CryptoFile.EncryptFile(encPath, brainBytes, _password!, backup);

                }
                finally
                {
                    if (File.Exists(clearBrainPath + ".enc"))
                    {
                        if (File.Exists(clearBrainPath))
                            File.Delete(clearBrainPath);
                        if (File.Exists(clearBrainPath + ".bak"))
                            File.Delete(clearBrainPath + ".bak");
                    }
                }
            }
            else
            {
                // Not protected - use base implementation
                base.SaveBrain(path, backup);

                // Clean up any lingering encrypted files if protection is turned off
                var encPath = path + ".enc";
                var bakPath = encPath + ".bak";
                if (File.Exists(encPath)) File.Delete(encPath);
                if (File.Exists(bakPath)) File.Delete(bakPath);
            }
        }

        private bool TryLoadEncryptedChatHistory(string encPath, string originalPath)
        {
            if (!CryptoFile.IsEncryptedFile(encPath)) return false;

            try
            {
                var decryptedBytes = CryptoFile.DecryptFile(encPath, _password!);
                if (decryptedBytes != null)
                {
                    // Create a temporary file with the decrypted content
                    var tempPath = originalPath + ".temp";
                    File.WriteAllBytes(tempPath, decryptedBytes);

                    try
                    {
                        // Use base implementation to load from temp file
                        // We need to extract the directory and filename for the base method
                        var dir = Path.GetDirectoryName(tempPath);
                        var fileName = Path.GetFileNameWithoutExtension(tempPath);
                        var basePath = Path.Combine(dir!, fileName.Replace(".temp", ""));

                        // Temporarily move temp file to expected location
                        File.Move(tempPath, basePath);
                        try
                        {
                            base.LoadChatHistory(dir!);
                            return true;
                        }
                        finally
                        {
                            // Move back to temp location for cleanup
                            if (File.Exists(basePath))
                                File.Move(basePath, tempPath);
                        }
                    }
                    finally
                    {
                        // Clean up temp file
                        if (File.Exists(tempPath))
                            File.Delete(tempPath);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Let caller handle password errors
                throw;
            }
            catch
            {
                // Other errors - file might be corrupted
                return false;
            }

            return false;
        }

        private void EnsurePassword()
        {
            if (string.IsNullOrEmpty(_password))
            {
                var password = string.Empty;
                var retries = 3;
                while (string.IsNullOrEmpty(password))
                {
                    password = Interaction.InputBox($"Enter password for protected character '{Name}':", "Character Password Required", "");
                    if (retries <= 0)
                    {
                        throw new UnauthorizedAccessException("No password provided. Shutting Down.");
                    }
                    retries--;
                }
                _password = password;
            }
        }

        private Image GetPortrait()
        {
            if (_image != null)
                return _image;
            var defaultfile = IsUser ? "user.png" : "Assistant.png";
            var selectedfile = File.Exists(@"data\img\" + Icon) ? Icon : defaultfile;
            _image = Image.FromFile("data/img/" + selectedfile);
            return _image;
        }


    }
}
