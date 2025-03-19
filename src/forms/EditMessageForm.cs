using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AIToolkit.Files;
using AIToolkit.LLM;
using AIToolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;

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
                Message.Message = LLMSystem.ReplaceMacros(ed_message.Text.ToLinuxFormat());
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        public EditMessageForm(Guid messageID)
        {
            InitializeComponent();
            Message = LLMSystem.History.GetMessageByID(messageID);
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
                SafeInvoke(() => ed_message.Text = Message!.Message.ToWinFormat());
            }
            else
            {
                ed_message.Text = Message!.Message.ToWinFormat();
            }
        }

        private void EditMessageForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => EditMessageForm_KeyPress(sender, e)));
            }
            else
            {
                if (e.KeyChar == (char)Keys.Escape)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
            }
        }
    }
}
