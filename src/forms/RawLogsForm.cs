using LetheAISharp;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaifuAI.src.forms
{
    public partial class RawLogForm : Form
    {
        public RawLogForm()
        {
            InitializeComponent();
            LoadSystemLogEntries();
            LLMEngineLogSink.LogAppended += OnLogAppended;
        }

        private void OnLogAppended(LLMEngineLogEntry entry)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                BeginInvoke(new Action<LLMEngineLogEntry>(OnLogAppended), entry);
                return;
            }

            AddLogEntry(entry);
        }

        private void LoadSystemLogEntries()
        {
            listSystemLog.BeginUpdate();
            listSystemLog.Items.Clear();

            foreach (var entry in LLMEngineLogSink.GetEntries())
            {
                AddLogEntry(entry);
            }

            listSystemLog.EndUpdate();
        }

        private void AddLogEntry(LLMEngineLogEntry entry)
        {
            var item = new ListViewItem(entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
            item.SubItems.Add(entry.Level.ToString());
            item.SubItems.Add(entry.Message);

            switch (entry.Level)
            {
                case LogLevel.Error:
                case LogLevel.Critical:
                    item.ForeColor = System.Drawing.Color.IndianRed;
                    break;
                case LogLevel.Warning:
                    item.ForeColor = System.Drawing.Color.Goldenrod;
                    break;
                case LogLevel.Information:
                    item.ForeColor = System.Drawing.Color.LightSkyBlue;
                    break;
            }

            listSystemLog.Items.Add(item);
            if (listSystemLog.Items.Count > 0)
                listSystemLog.EnsureVisible(listSystemLog.Items.Count - 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void SetText(string text)
        {
            txtSearchRes.Text = text;
        }

        public void SetSystemLog(string text)
        {
            LoadSystemLogEntries();
        }
    }
}
