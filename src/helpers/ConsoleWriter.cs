using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetheChat
{
    public class ControlWriter(TextBox textbox) : TextWriter
    {
        private readonly TextBox _textbox = textbox;

        public override void Write(char value)
        {
            _textbox.Invoke(() =>
            {
                _textbox.AppendText(value.ToString());
            });
        }

        public override void Write(string? value)
        {
            if (value != null)
            {
                _textbox.Invoke(() =>
                {
                    _textbox.AppendText(value);
                });
            }
        }
        public override Encoding Encoding => Encoding.UTF8;
    }

    public class ControlLoggerProvider(ControlWriter writer) : ILoggerProvider
    {
        private readonly ControlWriter _writer = writer;

        public ILogger CreateLogger(string categoryName)
        {
            return new ControlLogger(_writer);
        }

        public void Dispose() 
        { 
            GC.SuppressFinalize(this); 
        }
    }

    public class ControlLogger(ControlWriter writer) : ILogger
    {
        private readonly ControlWriter _writer = writer;

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;
            _writer.Write($"{logLevel}: {formatter(state, exception!)}\n");
        }
    }
}
