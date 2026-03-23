using LetheChat.Files;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LetheChat
{
    /// <summary>Event data for a single output line from the llama-server process.</summary>
    public sealed class LogLineEventArgs : EventArgs
    {
        public string Level { get; }
        public string Message { get; }
        public LogLineEventArgs(string level, string message) { Level = level; Message = message; }
    }

    /// <summary>
    /// Manages the lifecycle of the llama-server.exe process.
    /// When <see cref="IsManaged"/> is <see langword="false"/> (i.e. PathToLlamaCppServer is
    /// unset or the file does not exist), the manager is completely inert and the app behaves
    /// exactly as it does without this class.
    /// </summary>
    public class LlamaCppProcessManager : IDisposable
    {
        private Process? _serverProcess;
        private bool _disposed;

        /// <summary>
        /// Returns <see langword="true"/> when <c>PathToLlamaCppServer</c> is set and the file exists.
        /// </summary>
        public bool IsManaged => Program.Settings.ManagedLlama
            && !string.IsNullOrWhiteSpace(Program.Settings.PathToLlamaCppServer)
            && File.Exists(Program.Settings.PathToLlamaCppServer);

        /// <summary>
        /// Returns <see langword="true"/> when the managed process is currently alive.
        /// </summary>
        public bool IsRunning
        {
            get
            {
                try { return _serverProcess is not null && !_serverProcess.HasExited; }
                catch { return false; }
            }
        }

        /// <summary>
        /// Fired for every stdout / stderr line received from the server process.
        /// <see cref="LogLineEventArgs.Level"/> is <c>"OUT"</c> for stdout and <c>"INFO"</c> for stderr.
        /// </summary>
        public event EventHandler<LogLineEventArgs>? OutputReceived;

        /// <summary>
        /// Fired once when the server emits a line containing "listening" (case-insensitive),
        /// indicating it is ready to accept connections.
        /// </summary>
        public event EventHandler? ServerReady;

        /// <summary>
        /// Launches llama-server.exe for the given model, waits until it reports "listening",
        /// and returns <see langword="true"/> on success or <see langword="false"/> on timeout/failure.
        /// Any previously managed process is killed first.
        /// </summary>
        /// <param name="model">The model to launch.</param>
        /// <param name="ct">Optional cancellation token.</param>
        /// <param name="timeoutSeconds">Seconds to wait for the server to become ready (default 120).</param>
        public async Task<bool> LaunchAsync(LocalModel model, CancellationToken ct = default, int timeoutSeconds = 120)
        {
            if (!IsManaged)
                return false;

            await KillAsync();

            var exe = Program.Settings.PathToLlamaCppServer;
            var modelDir = Path.GetDirectoryName(model.ModelFile) ?? string.Empty;
            var args = model.Settings.GetArgsForDirectory(modelDir);

            if (string.IsNullOrEmpty(args))
                return false;

            var psi = new ProcessStartInfo
            {
                FileName = exe,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };

            var process = new Process
            {
                StartInfo = psi,
                EnableRaisingEvents = true,
            };

            var readyTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            void OnLine(string level, string? line)
            {
                if (line is null) return;
                OutputReceived?.Invoke(this, new LogLineEventArgs(level, line));
                if (line.Contains("listening", StringComparison.OrdinalIgnoreCase))
                {
                    readyTcs.TrySetResult(true);
                    ServerReady?.Invoke(this, EventArgs.Empty);
                }
            }

            process.OutputDataReceived += (_, e) => OnLine("OUT", e.Data);
            process.ErrorDataReceived += (_, e) => OnLine("INFO", e.Data);
            process.Exited += (_, _) => readyTcs.TrySetResult(false);

            try
            {
                process.Start();
                _serverProcess = process;
                ChildProcessTracker.Track(process);  // ← add this line
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            catch
            {
                process.Dispose();
                return false;
            }

            _serverProcess = process;
            //process.BeginOutputReadLine();
            //process.BeginErrorReadLine();

            using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct, timeoutCts.Token);
            linkedCts.Token.Register(() => readyTcs.TrySetResult(false));

            try
            {
                return await readyTcs.Task.ConfigureAwait(false);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Kills the managed process (entire process tree) and waits for it to exit.
        /// Safe to call when no process is running.
        /// </summary>
        public async Task KillAsync()
        {
            var proc = _serverProcess;
            _serverProcess = null;

            if (proc is null)
                return;

            try
            {
                if (!proc.HasExited)
                {
                    proc.Kill(entireProcessTree: true);
                    await proc.WaitForExitAsync().ConfigureAwait(false);
                }
            }
            catch
            {
                // Process may have already exited — ignore.
            }
            finally
            {
                proc.Dispose();
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            KillAsync().GetAwaiter().GetResult();
            GC.SuppressFinalize(this);
        }
    }
}
