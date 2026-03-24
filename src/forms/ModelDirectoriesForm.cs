using LetheChat.Controls;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace LetheChat.Forms
{
    public partial class ModelDirectoriesForm : Form
    {
        public ModelDirectoriesForm()
        {
            InitializeComponent();
        }

        private void ModelDirectoriesForm_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyToForm(this);
            PopulateList();
        }

        private void PopulateList()
        {
            lstDirectories.Items.Clear();
            foreach (var dir in Program.Settings.ModelDirectories)
                lstDirectories.Items.Add(dir);
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            using var dlg = new FolderBrowserDialog
            {
                Description = "Select a folder to search for GGUF models",
                UseDescriptionForTitle = true,
                ShowNewFolderButton = false
            };

            if (dlg.ShowDialog(this) != DialogResult.OK)
                return;

            var path = dlg.SelectedPath;
            if (!lstDirectories.Items.Contains(path))
                lstDirectories.Items.Add(path);
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            if (lstDirectories.SelectedIndex >= 0)
                lstDirectories.Items.RemoveAt(lstDirectories.SelectedIndex);
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            Program.Settings.ModelDirectories.Clear();
            foreach (string item in lstDirectories.Items)
                Program.Settings.ModelDirectories.Add(item);

            File.WriteAllText("settings.json", JsonConvert.SerializeObject(Program.Settings, Formatting.Indented));

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
