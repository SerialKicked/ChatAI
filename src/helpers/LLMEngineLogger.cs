using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuAI
{
    public sealed class LLMEngineLogEntry
    {
        public DateTime Timestamp { get; init; } = DateTime.Now;
        public LogLevel Level { get; init; }
        public string Category { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
    }

    public static class LLMEngineLogSink
    {
        private static readonly object _sync = new();
        private static readonly List<LLMEngineLogEntry> _entries = [];
        private const int MaxEntries = 2000;

        public static event Action<LLMEngineLogEntry>? LogAppended;

        public static void Append(LLMEngineLogEntry entry)
        {
            if (entry == null || string.IsNullOrWhiteSpace(entry.Message))
                return;

            lock (_sync)
            {
                _entries.Add(entry);
                if (_entries.Count > MaxEntries)
                {
                    _entries.RemoveRange(0, _entries.Count - MaxEntries);
                }
            }

            LogAppended?.Invoke(entry);
        }

        public static IReadOnlyList<LLMEngineLogEntry> GetEntries()
        {
            lock (_sync)
            {
                return [.. _entries];
            }
        }

        public static string GetText()
        {
            var sb = new StringBuilder();
            lock (_sync)
            {
                foreach (var entry in _entries)
                {
                    sb.Append('[').Append(entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss")).Append("] ")
                        .Append('[').Append(entry.Level).Append("] ")
                        .Append(entry.Category).Append(": ")
                        .AppendLine(entry.Message);
                }
            }

            return sb.ToString();
        }
    }

    public sealed class LLMEngineUiLogger(string categoryName = "LLMEngine") : ILogger
    {
        private readonly string _categoryName = categoryName;

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

        public void Log<TState>(LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var message = formatter != null ? formatter(state, exception) : state?.ToString() ?? string.Empty;
            if (exception != null)
                message += " | " + exception.Message;

            LLMEngineLogSink.Append(new LLMEngineLogEntry
            {
                Timestamp = DateTime.Now,
                Level = logLevel,
                Category = _categoryName,
                Message = message
            });
        }
    }
}
