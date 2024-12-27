using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnarkisTools.Files;
using AnarkisTools.LLM;
using AnarkisTools;

namespace WaifuAI.src.forms
{
    public partial class EditMessageForm : Form
    {
        public Guid MessageID { get; private set; }
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
                Message.Message = ed_message.Text.ToLinuxFormat();
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
            ed_message.Text = Message.Message.ToWinFormat();
        }

        private void EditMessageForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
