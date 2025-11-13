using LetheAISharp;
using LetheAISharp.LLM;
using LetheAISharp.Memory;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaifuAI.src.forms
{
    public partial class RAGSearchForm : Form
    {
        public RAGSearchForm()
        {
            InitializeComponent();
            textBox1.Text = LLMEngine.History.GetLastMessageFrom(AuthorRole.User)?.Message ?? string.Empty;
            DoSearch(textBox1.Text); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async Task DoSearch(string searchstr)
        {
            txtSearchRes.Clear();
            if (!string.IsNullOrWhiteSpace(searchstr))
            {
                var res = new StringBuilder();
                res.AppendLine($"Base string: {searchstr}");
                if (LLMEngine.Settings.RAGConvertTo3rdPerson)
                {
                    searchstr = searchstr.ConvertToThirdPerson();
                    res.AppendLine($"Converted to 3rd person: {searchstr}");
                }
                res.AppendLine();
                res.AppendLine("Search Results:");
                var found = await LLMEngine.Bot.Brain.Search(searchstr, 100, 1.2f);
                foreach (var item in found)
                {
                    var distance = item.Distance.ToString("0.0000");
                    var cat = item.Memory.Category.ToString();
                    var title = item.Memory.Name;
                    var content = LLMEngine.Bot.ReplaceMacros(item.Memory.Content);
                    res.AppendLine(cat + " (dist: " + distance + "): " + title);
                    res.AppendLine(content);
                    res.AppendLine();
                }
                txtSearchRes.Text = res.ToString();
            }
        }

        private async void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                var searchstr = textBox1.Text;
                await DoSearch(searchstr);
            }
        }
    }
}
