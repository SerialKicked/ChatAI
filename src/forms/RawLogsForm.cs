using LetheAISharp;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void SetText(string text)
        {
            txtSearchRes.Text = text;
        }
    }
}
