using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using LetheChat.Plugins;
using LetheChat.Security;
using System.ComponentModel;

namespace LetheChat.Files
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
        [Description("TODO: If set to true, this character can initiate chat by sending a message when the user is idle. It is contextually aware and may not always send a message. A system to prevent spam is also in place, limiting the amount of messages that can be sent before the user responds.")]
        public bool CanInitiateChat { get; set; } = false;

        [Description("A short bio for this character (should be a paragraph). This is only used for some slash commands and in group chats.")]
        public string MiniBio { get; set; } = string.Empty;

        /// <summary> Icon to be displayed in chat </summary>
        [Description("Icon to be displayed in chat. Place the image file in the data/img folder and enter the filename here. If left blank or file not found, a default icon will be used.")]
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// A list of prefered inference settings for this character. When enabled in the UI, the bot will cycle between these settings at random with each new message. This ensure a more diverse set of responses.
        /// </summary>
        [Description("A list of prefered sampling settings for this character. When enabled in the main UI, the bot will cycle between these settings at random with each new message. This ensure a more diverse set of responses.")]
        public List<string> AllowedSamplers { get; set; } = [];

        /// <summary>
        /// Voice ID for for OuteTTS (if enabled)
        /// </summary>
        [Description("Voice ID that will be used by text-to-speech for this character (if enabled, only available with KoboldCpp).")]
        public string TTSVoice { get; set; } = string.Empty;

        /// <summary> Reference to the point system </summary>
        [Description("Reference to the point system this character uses (optional). See documentation.")]
        public string PointSystem { get; set; } = string.Empty;

        /// <summary> Current point value </summary>
        [Description("Current points availables. See documentation.")]
        public int PointValue { get; set; } = 0;

        /// <summary> Whether this character's brain and chatlog files should be encrypted at rest </summary>
        [Description("If set to true, this character's memory (brain) and chatlog files will be strongly encrypted behind a password. You will be prompted to enter a password when starting a chat with this character. If you forget the password, you will lose access to the character's history and memories permanently: there no recovery options.")]
        public bool Protected { get; set; } = false;

        [Description("Toogle plugin. See documentation.")]
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
            // Set Password thingy
            if (Protected && string.IsNullOrEmpty(_password))
            {
                if (!EnsurePassword())
                    throw new OperationCanceledException("Password entry cancelled.");
            }

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
            if (Protected && string.IsNullOrEmpty(_password))
            {
                return; // User cancelled, skip saving
            }
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
                    return;  // User cancelled, skip saving
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
        public override void LoadBrain(string path)
        {
            var encPath = path + UniqueName + ".brain.enc";
            var bakPath = encPath + ".bak";

            // Check if we need encryption (Protected flag set or .enc files exist)
            bool needsEncryption = Protected || CryptoFile.IsEncryptedFile(encPath) || CryptoFile.IsEncryptedFile(bakPath);

            if (needsEncryption)
            {
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

        public override void SaveBrain(string path, bool backup)
        {
            if (Protected)
            {
                if (string.IsNullOrEmpty(_password))
                {
                    return;  // User cancelled, skip saving
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

        private bool EnsurePassword()
        {
            if (string.IsNullOrEmpty(_password))
            {
                var password = string.Empty;
                while (string.IsNullOrEmpty(password))
                {
                    password = Interaction.InputBox($"Enter password for protected character '{Name}':", "Character Password Required", "");
                    if (string.IsNullOrEmpty(password))
                    {
                        return false;
                    }
                }
                _password = password;
            }
            return true;
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
