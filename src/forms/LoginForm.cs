using AIToolkit.LLM;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaifuAI.src.forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public void LoadSettings()
        {
            edUrl.Text = Program.Settings.BackendUrl;
            cbAPI.SelectedIndex = (int)Program.Settings.BackendAPI;
            edKey.Text = Program.Settings.OpenAIKey;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btCheck_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            LLMEngine.Setup(edUrl.Text, (BackendAPI)cbAPI.SelectedIndex, !string.IsNullOrEmpty(edKey.Text) ? edKey.Text : null);
            var res = await LLMEngine.CheckBackend();
            if (res)
            {
                MessageBox.Show("Connection Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Connection Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Enabled = true;
        }

        private async void btConnect_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            LLMEngine.Setup(edUrl.Text, (BackendAPI)cbAPI.SelectedIndex, !string.IsNullOrEmpty(edKey.Text) ? edKey.Text : null);
            var res = await LLMEngine.CheckBackend();
            if (res)
            {
                Program.Settings.BackendUrl = edUrl.Text;
                Program.Settings.BackendAPI = (BackendAPI)cbAPI.SelectedIndex;
                if (!string.IsNullOrEmpty(edKey.Text))
                    Program.Settings.OpenAIKey = edKey.Text;
                await LLMEngine.Connect();
                this.Enabled = true;
                Close();
            }
            else
            {
                MessageBox.Show("Connection Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Enabled = true;
            }
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            LoadSettings();
        }
    }
}
