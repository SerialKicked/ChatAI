using LetheAISharp.LLM;
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
using LetheChat.Controls;

namespace LetheChat.src.forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        public void LoadSettings()
        {
            edUrl.Text = Program.Settings.BackendUrl;
            cbAPI.SelectedIndex = (int)Program.Settings.BackendAPI;
            edKey.Text = Program.Settings.OpenAIKey;
            num_gpu.Value = Program.Settings.LlamaSharpGPULayers;
            num_maxcontext.Value = Program.Settings.MaxTotalTokens;
            ck_flash.Checked = Program.Settings.LlamaSharpFlashAttention;
            UpdateFields();
        }

        private void UpdateFields()
        {
            switch ((BackendAPI)cbAPI.SelectedIndex)
            {
                case BackendAPI.OpenAI:
                    edKey.Enabled = true;
                    edKey.PlaceholderText = "Enter your OpenAI API key here";
                    lbl_context.Visible = true;
                    num_maxcontext.Visible = true;
                    lbl_gpu.Visible = false;
                    num_gpu.Visible = false;
                    ck_flash.Visible = false;
                    break;
                case BackendAPI.LlamaCpp:
                    edKey.Enabled = false;
                    edKey.PlaceholderText = "No Key Required";
                    edKey.Text = string.Empty;
                    lbl_context.Visible = false;
                    num_maxcontext.Visible = false;
                    lbl_gpu.Visible = false;
                    num_gpu.Visible = false;
                    ck_flash.Visible = false;
                    break;
                case BackendAPI.KoboldAPI:
                    edKey.Enabled = false;
                    edKey.PlaceholderText = "No Key Required";
                    edKey.Text = string.Empty;
                    lbl_context.Visible = false;
                    num_maxcontext.Visible = false;
                    lbl_gpu.Visible = false;
                    num_gpu.Visible = false;
                    ck_flash.Visible = false;
                    break;
                case BackendAPI.LlamaSharp:
                    edKey.Enabled = false;
                    edKey.PlaceholderText = "No Key Required";
                    edKey.Text = string.Empty;
                    lbl_context.Visible = true;
                    num_maxcontext.Visible = true;
                    lbl_gpu.Visible = true;
                    num_gpu.Visible = true;
                    ck_flash.Visible = true;
                    break;
            }
            btBrowse.Visible = cbAPI.SelectedIndex == (int)BackendAPI.LlamaSharp;
            btCheck.Enabled = cbAPI.SelectedIndex != (int)BackendAPI.LlamaSharp;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btCheck_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            LLMEngine.Setup(edUrl.Text, (BackendAPI)cbAPI.SelectedIndex, !string.IsNullOrEmpty(edKey.Text) ? edKey.Text : "123");
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
            LLMEngine.Settings.LlamaSharpFlashAttention = ck_flash.Checked;
            LLMEngine.Settings.MaxTotalTokens = (int)num_maxcontext.Value;
            LLMEngine.Settings.LlamaSharpGPULayers = (int)num_gpu.Value;

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
                this.DialogResult = DialogResult.OK;
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
            WindowDragHelper.MakeDraggable(collapsibleGroupBox1, this);
        }

        private void cbAPI_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFields();
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            // Open file dialog to select a .GGUF model file
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "GGUF Model Files (*.gguf)|*.gguf|All Files (*.*)|*.*";
                ofd.Title = "Select a GGUF Model File";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    edUrl.Text = $"{ofd.FileName}";
                }
            }
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
