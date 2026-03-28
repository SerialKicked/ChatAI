using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace LetheChat.Security
{
    // Envelope format:
    // [8 bytes magic "CAIENC01"][1 byte version=1]
    // [4 bytes LE kdfIterations][16 bytes salt][12 bytes nonce]
    // [4 bytes LE ciphertextLen][ciphertext][16 bytes tag]
    public static class CryptoFile
    {
        private static readonly byte[] Magic = Encoding.ASCII.GetBytes("CAIENC01");
        private const byte Version = 1;
        private const int SaltLen = 16;
        private const int NonceLen = 12;
        private const int TagLen = 16;
        private const int KeyLen = 32;
        private const int DefaultKdfIterations = 310_000; // NIST-aligned modern default

        public static bool IsEncryptedFile(string path)
        {
            if (!File.Exists(path) || new FileInfo(path).Length < Magic.Length + 1)
                return false;
            using var fs = File.OpenRead(path);
            var header = new byte[Magic.Length];
            var read = fs.Read(header, 0, header.Length);
            if (read != header.Length) return false;
            return header.AsSpan().SequenceEqual(Magic);
        }

        public static T? DecryptJsonFile<T>(string path, string password) where T : class
        {
            var bytes = DecryptFile(path, password);
            if (bytes == null) return null;
            var json = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static void EncryptJsonFile<T>(string path, T obj, string password, bool backup = false) where T : class
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            var bytes = Encoding.UTF8.GetBytes(json);
            EncryptFile(path, bytes, password, backup);
        }

        public static byte[]? DecryptFile(string path, string password)
        {
            if (!File.Exists(path)) return null;

            using var fs = File.OpenRead(path);
            using var reader = new BinaryReader(fs);

            // Read and validate magic
            var magic = reader.ReadBytes(Magic.Length);
            if (!magic.AsSpan().SequenceEqual(Magic))
                throw new InvalidDataException("Invalid encrypted file format");

            // Read version
            var version = reader.ReadByte();
            if (version != Version)
                throw new InvalidDataException($"Unsupported file version: {version}");

            // Read KDF parameters
            var kdfIterations = reader.ReadInt32();
            var salt = reader.ReadBytes(SaltLen);
            var nonce = reader.ReadBytes(NonceLen);

            // Read ciphertext
            var ciphertextLen = reader.ReadInt32();
            var ciphertext = reader.ReadBytes(ciphertextLen);
            var tag = reader.ReadBytes(TagLen);

            // Derive key using static Pbkdf2 method
            var key = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                kdfIterations,
                HashAlgorithmName.SHA256,
                KeyLen
            );

            // Decrypt
            using var aes = new AesGcm(key, TagLen);
            var plaintext = new byte[ciphertextLen];
            try
            {
                aes.Decrypt(nonce, ciphertext, tag, plaintext);
                return plaintext;
            }
            catch (CryptographicException)
            {
                // Show dialog box telling the password is wrong and then exit the app forcefully to prevent any further damage to the file
                MessageBox.Show("Incorrect password. The file could not be decrypted (wrong password or corrupted file). The app will shut down to prevent damage.", "Decryption Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // error code 86 = incorrect password
                Environment.Exit(86);
                return null;
            }
        }

        public static void EncryptFile(string path, byte[] data, string password, bool backup = false)
        {
            // Generate random parameters
            var salt = RandomNumberGenerator.GetBytes(SaltLen);
            var nonce = RandomNumberGenerator.GetBytes(NonceLen);

            // Derive key using static Pbkdf2 method
            var key = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                DefaultKdfIterations,
                HashAlgorithmName.SHA256,
                KeyLen
            );

            // Encrypt
            using var aes = new AesGcm(key, TagLen);
            var ciphertext = new byte[data.Length];
            var tag = new byte[TagLen];
            aes.Encrypt(nonce, data, ciphertext, tag);

            // Write to temp file first (atomic operation)
            var tempPath = path + ".tmp";
            using (var fs = File.Create(tempPath))
            using (var writer = new BinaryWriter(fs))
            {
                // Write header
                writer.Write(Magic);
                writer.Write(Version);
                writer.Write(DefaultKdfIterations);
                writer.Write(salt);
                writer.Write(nonce);
                writer.Write(ciphertext.Length);
                writer.Write(ciphertext);
                writer.Write(tag);
            }

            // Atomic replace
            if (File.Exists(path))
            {
                if (backup)
                {
                    var bakPath = path + ".bak";
                    File.Replace(tempPath, path, bakPath);
                }
                else
                {
                    File.Replace(tempPath, path, null);
                }
            }
            else
            {
                File.Move(tempPath, path);
            }
        }
    }
}