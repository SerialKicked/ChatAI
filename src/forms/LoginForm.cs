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
            edUrl.Text = Program.Settings.BaseURL;
            cbAPI.SelectedIndex = (int)Program.Settings.API;
            edKey.Text = Program.Settings.APIKey;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btCheck_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            LLMSystem.Setup(edUrl.Text, (BackendAPI)cbAPI.SelectedIndex);
            var res = await LLMSystem.CheckBackend();
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
            LLMSystem.Setup(edUrl.Text, (BackendAPI)cbAPI.SelectedIndex);
            var res = await LLMSystem.CheckBackend();
            if (res)
            {
                Program.Settings.BaseURL = edUrl.Text;
                Program.Settings.API = (BackendAPI)cbAPI.SelectedIndex;
                await LLMSystem.Connect();
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
