using NAudio.Wave;
using System;
using System.IO;

namespace AnarkisTools
{
    /// <summary>
    /// Record audio from the microphone and save it to a file.
    /// </summary>
    public class AudioRecord : IDisposable
    {
        private WaveInEvent waveIn;
        private WaveFileWriter? writer;
        private MemoryStream? memoryStream;

        public AudioRecord()
        {
            waveIn = new WaveInEvent();
            waveIn.DataAvailable += OnDataAvailable;
            waveIn.RecordingStopped += OnRecordingStopped;
        }

        public void StartRecording(string filePath)
        {
            memoryStream = new MemoryStream();
            writer = new WaveFileWriter(filePath, waveIn.WWaveFormat);
            waveIn.StartRecording();
        }

        public void StopRecording()
        {
            waveIn.StopRecording();
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (writer != null)
            {
                writer.Write(e.Buffer, 0, e.BytesRecorded);
                writer.Flush();
            }
        }

        private void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            writer?.Dispose();
            writer = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                waveIn?.Dispose();
                writer?.Dispose();
                memoryStream?.Dispose();
            }
        }

    }
}
