using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AIToolkit.LLM;
using AIToolkit;

namespace WaifuAI.src.forms
{
    public partial class ScenarioEditForm : Form
    {
        public ScenarioEditForm()
        {
            InitializeComponent();
            ed_message.PlaceholderText = LLMEngine.Bot.Scenario.ToWinFormat();
            ed_message.Text = LLMEngine.Settings.ScenarioOverride.ToWinFormat();

        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            LLMEngine.Settings.ScenarioOverride = !string.IsNullOrWhiteSpace(ed_message.Text) ? LLMEngine.ReplaceMacros(ed_message.Text.ToLinuxFormat()) : string.Empty;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ScenarioEditForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LLMEngine.Settings.ScenarioOverride = string.Empty;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
