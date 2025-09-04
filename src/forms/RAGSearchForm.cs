using AIToolkit;
using AIToolkit.LLM;
using AIToolkit.Memory;
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                var searchstr = textBox1.Text;
                txtSearchRes.Clear();
                if (!string.IsNullOrWhiteSpace(searchstr))
                {

                    var res = new StringBuilder();
                    res.AppendLine($"Base string: {searchstr}");
                    searchstr = searchstr.ConvertToThirdPerson();
                    res.AppendLine($"Converted to 3rd person: {searchstr}");
                    res.AppendLine();
                    res.AppendLine("Search Results:");
                    var found = await RAGSystem.Search(searchstr, 100, 1.2f);
                    foreach (var item in found)
                    {
                        var title = "[unknown]";
                        var content = "[unknown]";
                        var distance = item.distance.ToString("0.0000");
                        var cat = item.category.ToString();
                        if (item.session is MemoryUnit unit)
                        {
                            title = unit.Name;
                            content = LLMSystem.ReplaceMacros(unit.Content);
                        }
                        res.AppendLine(cat + " (dist: " + distance + "): " + title);
                        res.AppendLine(content);
                        res.AppendLine();
                    }
                    txtSearchRes.Text = res.ToString();
                }
            }
        }
    }
}
