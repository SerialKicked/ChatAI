using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Media.Render;
using Windows.Media.Transcoding;
using Windows.Storage;
using Whisper.net;
using Whisper.net.Ggml;
using System.ComponentModel;

namespace LetheChat
{

    /// <summary>
    /// Settings controlling audio capture and Whisper transcription.
    /// </summary>
    public class SpeechRecognizerSettings
    {
        public static Dictionary<string, string> AvailableLanguages { get; set; } = new Dictionary<string, string>()
        {
            { "auto",  "Auto-detect" },
            { "af",    "Afrikaans" },
            { "sq",    "Albanian" },
            { "am",    "Amharic" },
            { "ar",    "Arabic" },
            { "hy",    "Armenian" },
            { "as",    "Assamese" },
            { "az",    "Azerbaijani" },
            { "ba",    "Bashkir" },
            { "eu",    "Basque" },
            { "be",    "Belarusian" },
            { "bn",    "Bengali" },
            { "bs",    "Bosnian" },
            { "br",    "Breton" },
            { "bg",    "Bulgarian" },
            { "yue",   "Cantonese" },
            { "ca",    "Catalan" },
            { "zh",    "Chinese" },
            { "hr",    "Croatian" },
            { "cs",    "Czech" },
            { "da",    "Danish" },
            { "nl",    "Dutch" },
            { "en",    "English" },
            { "et",    "Estonian" },
            { "fo",    "Faroese" },
            { "fi",    "Finnish" },
            { "fr",    "French" },
            { "gl",    "Galician" },
            { "ka",    "Georgian" },
            { "de",    "German" },
            { "el",    "Greek" },
            { "gu",    "Gujarati" },
            { "ht",    "Haitian Creole" },
            { "ha",    "Hausa" },
            { "haw",   "Hawaiian" },
            { "he",    "Hebrew" },
            { "hi",    "Hindi" },
            { "hu",    "Hungarian" },
            { "is",    "Icelandic" },
            { "id",    "Indonesian" },
            { "it",    "Italian" },
            { "ja",    "Japanese" },
            { "jw",    "Javanese" },
            { "kn",    "Kannada" },
            { "kk",    "Kazakh" },
            { "km",    "Khmer" },
            { "ko",    "Korean" },
            { "lo",    "Lao" },
            { "la",    "Latin" },
            { "lv",    "Latvian" },
            { "ln",    "Lingala" },
            { "lt",    "Lithuanian" },
            { "lb",    "Luxembourgish" },
            { "mk",    "Macedonian" },
            { "mg",    "Malagasy" },
            { "ms",    "Malay" },
            { "ml",    "Malayalam" },
            { "mt",    "Maltese" },
            { "mi",    "Maori" },
            { "mr",    "Marathi" },
            { "mn",    "Mongolian" },
            { "my",    "Myanmar" },
            { "ne",    "Nepali" },
            { "no",    "Norwegian" },
            { "nn",    "Nynorsk" },
            { "oc",    "Occitan" },
            { "ps",    "Pashto" },
            { "fa",    "Persian" },
            { "pl",    "Polish" },
            { "pt",    "Portuguese" },
            { "pa",    "Punjabi" },
            { "ro",    "Romanian" },
            { "ru",    "Russian" },
            { "sa",    "Sanskrit" },
            { "sr",    "Serbian" },
            { "sn",    "Shona" },
            { "sd",    "Sindhi" },
            { "si",    "Sinhala" },
            { "sk",    "Slovak" },
            { "sl",    "Slovenian" },
            { "so",    "Somali" },
            { "es",    "Spanish" },
            { "su",    "Sundanese" },
            { "sw",    "Swahili" },
            { "sv",    "Swedish" },
            { "tl",    "Tagalog" },
            { "tg",    "Tajik" },
            { "ta",    "Tamil" },
            { "tt",    "Tatar" },
            { "te",    "Telugu" },
            { "th",    "Thai" },
            { "bo",    "Tibetan" },
            { "tr",    "Turkish" },
            { "tk",    "Turkmen" },
            { "uk",    "Ukrainian" },
            { "ur",    "Urdu" },
            { "uz",    "Uzbek" },
            { "vi",    "Vietnamese" },
            { "cy",    "Welsh" },
            { "yi",    "Yiddish" },
            { "yo",    "Yoruba" },
        };

        /// <summary>Full path to the Whisper ggml model file (e.g. ggml-base.bin).</summary>
        public string WhisperFile { get; set; } = "ggml-base.bin";

        /// <summary>BCP-47 language code passed to Whisper ("auto" lets Whisper detect it).</summary>
        [Description("Language code passed to Whisper. While list is extensive, do not expect much outside of the usual western languages. Set to 'auto' to let Whisper detect the language automatically.")]
        public string Language { get; set; } = "auto";

        /// <summary>Audio sample rate used for recording (Hz). Whisper expects 16000 Hz.</summary>
        public uint SampleRate { get; set; } = 16000;

        /// <summary>Number of audio channels to capture (1 = mono).</summary>
        public uint Channels { get; set; } = 1;

        /// <summary>
        /// Amplitude threshold (0.0–1.0) below which audio is considered silence.
        /// Used to detect automatic stop when <see cref="SilenceTimeoutSeconds"/> &gt; 0.
        /// </summary>
        [Description("Amplitude threshold (0.0–1.0) below which audio is considered silence. Used to detect automatic stop when SilenceTimeoutSeconds > 0.")]
        public float SilenceThreshold { get; set; } = 0.015f;

        /// <summary>
        /// If &gt; 0, recording stops automatically after this many consecutive seconds of silence.
        /// Set to 0 to disable auto-stop (manual StopRecordingAsync only).
        /// </summary>
        [Description("If > 0, recording stops automatically after this many consecutive seconds of silence. Set to 0 to disable auto-stop (manual StopRecordingAsync only).")]
        public double SilenceTimeoutSeconds { get; set; } = 4.0;

        /// <summary>
        /// If set to true, the Whisper model will be unloaded from memory before LLM generation and reloaded afterwards. 
        /// This can help reduce peak memory usage at the cost of increased latency.
        /// </summary>
        [Description("If set to true, the Whisper model will be unloaded from memory before LLM generation and reloaded afterwards. \n"+
            "This can help reduce peak memory usage at the cost of increased latency.")]
        public bool DynamicLoadModel { get; set; } = false;

        /// <summary>
        /// Global switch to enable or disable speech recognition features.
        /// </summary>
        [Description("Global switch to enable or disable speech recognition features.")]
        public bool AllowAudioRecording { get; set; } = true;

        /// <summary>Temporary WAV file written after each recording session.</summary>
        public string TempWavPath { get; set; } = Path.Combine(Path.GetTempPath(), "lethe_speech_input.wav");
    }

    /// <summary>
    /// Handles microphone recording and Whisper-based transcription as a single self-contained unit.
    /// </summary>
    public class SpeechRecognizer : IDisposable
    {
        // COM interface required to read raw bytes from a WinRT AudioFrame buffer
        [ComImport]
        [Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private unsafe interface IMemoryBufferByteAccess
        {
            void GetBuffer(out byte* buffer, out uint capacity);
        }

        // ──────────────────────────────────────────────────────────────
        // State
        // ──────────────────────────────────────────────────────────────
        public SpeechRecognizerSettings Settings => Program.Settings.AudioSettings;

        private WhisperFactory? _factory;
        private WhisperProcessor? _processor;
        private bool _disposed;

        // Recording state
        private AudioGraph? _graph;
        private AudioDeviceInputNode? _inputNode;
        private AudioFileOutputNode? _fileOutputNode;
        private AudioFrameOutputNode? _frameOutputNode;
        private bool _isRecording;
        private TaskCompletionSource<bool>? _recordingStopTcs;
        private DateTime _lastSoundTime;

        // ──────────────────────────────────────────────────────────────
        // Model management
        // ──────────────────────────────────────────────────────────────

        /// <summary>
        /// Loads the Whisper model from <see cref="SpeechRecognizerSettings.WhisperFile"/>.
        /// Downloads the model automatically if the file does not exist yet.
        /// </summary>
        public async Task LoadModelAsync(GgmlType modelType = GgmlType.Base, CancellationToken ct = default)
        {
            UnloadModel();

            if (!File.Exists(Settings.WhisperFile))
                await DownloadModelAsync(modelType, ct);

            _factory = WhisperFactory.FromPath(Settings.WhisperFile);
            _processor = _factory.CreateBuilder()
                .WithLanguage(Settings.Language)
                .Build();
        }

        /// <summary>
        /// Downloads a Whisper ggml model from Hugging Face to <see cref="SpeechRecognizerSettings.WhisperFile"/>.
        /// </summary>
        public async Task DownloadModelAsync(GgmlType modelType = GgmlType.Base, CancellationToken ct = default)
        {
            using var modelStream = await WhisperGgmlDownloader.Default.GetGgmlModelAsync(modelType);
            using var fileWriter = File.OpenWrite(Settings.WhisperFile);
            await modelStream.CopyToAsync(fileWriter, ct);
        }

        /// <summary>Disposes the Whisper processor and factory, freeing model memory.</summary>
        public void UnloadModel()
        {
            _processor?.Dispose();
            _processor = null;
            _factory?.Dispose();
            _factory = null;
        }

        // ──────────────────────────────────────────────────────────────
        // Recording
        // ──────────────────────────────────────────────────────────────

        /// <summary>
        /// Opens the default microphone and begins capturing audio.
        /// Returns a Task that completes when recording has stopped (either via
        /// <see cref="StopRecordingAsync"/> or automatic silence detection).
        /// </summary>
        public async Task<Task> StartRecordingAsync()
        {
            if (_isRecording)
                throw new InvalidOperationException("Recording is already in progress.");

            _recordingStopTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            _lastSoundTime = DateTime.UtcNow;

            // Create the audio graph targeting speech capture
            var graphSettings = new AudioGraphSettings(AudioRenderCategory.Speech);
            var graphResult = await AudioGraph.CreateAsync(graphSettings);
            if (graphResult.Status != AudioGraphCreationStatus.Success)
                throw new InvalidOperationException($"AudioGraph creation failed: {graphResult.Status}");

            _graph = graphResult.Graph;

            var encoding = AudioEncodingProperties.CreatePcm(Settings.SampleRate, Settings.Channels, 16);
            var profile = MediaEncodingProfile.CreateWav(AudioEncodingQuality.Low);
            profile.Audio = encoding;

            // Microphone input (default device, speech category applies noise suppression)
            var inputResult = await _graph.CreateDeviceInputNodeAsync(MediaCategory.Speech);
            if (inputResult.Status != AudioDeviceNodeCreationStatus.Success)
                throw new InvalidOperationException($"Microphone input failed: {inputResult.Status}. Make sure a microphone is connected.");

            _inputNode = inputResult.DeviceInputNode;

            // File output — AudioGraph writes the WAV directly, no manual header needed
            var outputDir = Path.GetDirectoryName(Settings.TempWavPath)!;
            var outputFileName = Path.GetFileName(Settings.TempWavPath);
            var folder = await StorageFolder.GetFolderFromPathAsync(outputDir);
            var file = await folder.CreateFileAsync(outputFileName, CreationCollisionOption.ReplaceExisting);
            var fileResult = await _graph.CreateFileOutputNodeAsync(file, profile);
            if (fileResult.Status != AudioFileNodeCreationStatus.Success)
                throw new InvalidOperationException($"Audio file output creation failed: {fileResult.Status}");

            _fileOutputNode = fileResult.FileOutputNode;

            // Frame output used for silence detection
            _frameOutputNode = _graph.CreateFrameOutputNode(encoding);

            _inputNode.AddOutgoingConnection(_fileOutputNode);
            _inputNode.AddOutgoingConnection(_frameOutputNode);

            if (Settings.SilenceTimeoutSeconds > 0)
                _graph.QuantumStarted += OnQuantumStarted;

            _isRecording = true;
            _graph.Start();

            return _recordingStopTcs.Task;
        }

        /// <summary>
        /// Stops microphone capture. The WAV file at <see cref="SpeechRecognizerSettings.TempWavPath"/>
        /// is finalised and ready to use after this returns.
        /// </summary>
        public async Task StopRecordingAsync()
        {
            if (!_isRecording)
                return;

            _isRecording = false;

            if (_graph != null)
                _graph.QuantumStarted -= OnQuantumStarted;

            _graph?.Stop();

            if (_fileOutputNode != null)
                await _fileOutputNode.FinalizeAsync();

            _inputNode?.Dispose();
            _fileOutputNode?.Dispose();
            _frameOutputNode?.Dispose();
            _graph?.Dispose();

            _inputNode = null;
            _fileOutputNode = null;
            _frameOutputNode = null;
            _graph = null;

            _recordingStopTcs?.TrySetResult(true);
        }

        // ──────────────────────────────────────────────────────────────
        // Transcription
        // ──────────────────────────────────────────────────────────────

        /// <summary>
        /// Runs Whisper on <see cref="SpeechRecognizerSettings.TempWavPath"/> and returns
        /// the full transcribed text. The model must be loaded first via <see cref="LoadModelAsync"/>.
        /// </summary>
        public async Task<string> TranscribeAsync(CancellationToken ct = default)
        {
            if (_processor == null)
                throw new InvalidOperationException("Whisper model is not loaded. Call LoadModelAsync first.");

            if (!File.Exists(Settings.TempWavPath))
                throw new FileNotFoundException("No recorded audio file found.", Settings.TempWavPath);

            using var fileStream = File.OpenRead(Settings.TempWavPath);
            var result = new StringBuilder();

            await foreach (var segment in _processor.ProcessAsync(fileStream, ct))
                result.Append(segment.Text);

            return result.ToString().Trim();
        }

        /// <summary>
        /// Convenience method: stops recording, then immediately transcribes and returns the text.
        /// </summary>
        public async Task<string> StopAndTranscribeAsync(CancellationToken ct = default)
        {
            await StopRecordingAsync();
            return await TranscribeAsync(ct);
        }

        // ──────────────────────────────────────────────────────────────
        // Private helpers
        // ──────────────────────────────────────────────────────────────

        private void OnQuantumStarted(AudioGraph sender, object args)
        {
            if (_frameOutputNode == null) return;

            using var frame = _frameOutputNode.GetFrame();
            using var buffer = frame.LockBuffer(Windows.Media.AudioBufferAccessMode.Read);
            using var reference = buffer.CreateReference();

            unsafe
            {
                ((IMemoryBufferByteAccess)reference).GetBuffer(out byte* dataPtr, out uint capacity);
                float* samples = (float*)dataPtr;
                int sampleCount = (int)(capacity / sizeof(float));

                for (int i = 0; i < sampleCount; i++)
                {
                    if (MathF.Abs(samples[i]) > Settings.SilenceThreshold)
                    {
                        _lastSoundTime = DateTime.UtcNow;
                        return;
                    }
                }
            }

            if ((DateTime.UtcNow - _lastSoundTime).TotalSeconds >= Settings.SilenceTimeoutSeconds)
                _ = StopRecordingAsync();
        }

        // ──────────────────────────────────────────────────────────────
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            if (_isRecording)
                StopRecordingAsync().GetAwaiter().GetResult();

            UnloadModel();
        }
    }
}
