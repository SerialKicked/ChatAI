using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LetheAISharp.Files;
using LetheAISharp.LLM;
using LetheAISharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using WaifuAI.Controls;

namespace WaifuAI.src.forms
{
    public partial class EditMessageForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Guid MessageID { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SingleMessage? Message { get; private set; }

        public EditMessageForm()
        {
            InitializeComponent();
            ed_message.SpellCheckLanguage = "en-US";
            ed_message.KeyUp += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
            };
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            if (Message == null)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            else
            {
                Message.Message = LLMEngine.Bot.ReplaceMacros(ed_message.Text.ToLinuxFormat());
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        public EditMessageForm(Guid messageID)
        {
            InitializeComponent();
            ed_message.SpellCheckLanguage = "en-US";
            ed_message.KeyUp += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
            };
            Message = LLMEngine.History.GetMessageByID(messageID, true);
            if (Message == null)
            {
                Close();
                return;
            }
            Setup();
        }

        private void SafeInvoke(Action action)
        {
            if (IsHandleCreated && !Disposing && !IsDisposed)
            {
                Invoke(action);
            }
        }

        private void Setup()
        {
            if (InvokeRequired)
            {
                SafeInvoke(() =>
                {
                    ed_message.Text = Message!.Message.ToWinFormat();
                });

            }
            else
            {
                ed_message.Text = Message!.Message.ToWinFormat();
            }
        }

        private void EditMessageForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Message == null)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            else
            {
                LLMEngine.History.CurrentSession.Messages.Remove(Message);
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
