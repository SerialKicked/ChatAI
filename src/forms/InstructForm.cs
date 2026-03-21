using LetheAISharp.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LetheChat.src.forms
{
    public partial class InstructForm : Form
    {
        private InstructFormat SelectedInstructEditor = new();
        private bool disableevents = false;

        public InstructForm()
        {
            InitializeComponent();
        }

        public void SetupInstructEditor(string Forceid = "")
        {
            listInstruct.Items.Clear();
            disableevents = true;
            foreach (var item in DataFiles.Instruct)
            {
                listInstruct.Items.Add(item.Value.UniqueName);
            }
            var idwant = 0;
            if (Forceid != "")
                idwant = listInstruct.Items.IndexOf(Forceid);
            if (listInstruct.Items.Count > 0 && idwant != -1)
            {
                listInstruct.SelectedIndex = idwant;
                SelectedInstructEditor = DataFiles.Instruct[listInstruct.SelectedItem!.ToString()!].Copy<InstructFormat>()!;
            }
            edInstruct.Text = SelectedInstructEditor.UniqueName;
            CreateInstructControls(panContent, SelectedInstructEditor);
            disableevents = false;
        }

        /// <summary>
        /// Create the editor for the instruction format Program.Settings
        /// </summary>
        /// <param name="target"></param>
        /// <param name="instructsetting"></param>
        private static void CreateInstructControls(Control target, InstructFormat instructsetting)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8605 // Converting null literal or possible null value to non-nullable type.
            target.Controls.Clear();
            int yPos = 10;
            Type type = typeof(InstructFormat);
            PropertyInfo[] properties = type.GetProperties();
            string[] ignore = ["UniqueName"];

            var lst = new List<PropertyInfo>(properties);
            // sort lst to match the order in InstructFormat.Properties

            foreach (var propertyName in InstructFormat.Properties)
            {
                if (ignore.Contains(propertyName))
                    continue;
                var property = lst.Find(p => p.Name == propertyName);
                if (property == null)
                    continue;
                Label label = new() { Text = property.Name + ":", Location = new Point(10, yPos), Width = 240 };
                Control? control = null;
                if (property.PropertyType == typeof(int))
                {
                    control = new NumericUpDown { Minimum = -1, Maximum = int.MaxValue, Value = (int)property.GetValue(instructsetting), Location = new System.Drawing.Point(150, yPos), Width = 100 };
                    ((NumericUpDown)control).ValueChanged += (sender, e) => property.SetValue(instructsetting, (int)((NumericUpDown)control).Value);
                }
                else if (property.PropertyType == typeof(double))
                {
                    control = new NumericUpDown { Value = (decimal)(double)property.GetValue(instructsetting), Location = new Point(150, yPos), Width = 100, DecimalPlaces = 2, Increment = 0.01M };
                    ((NumericUpDown)control).ValueChanged += (sender, e) => property.SetValue(instructsetting, (double)((NumericUpDown)control).Value);
                }
                else if (property.PropertyType == typeof(string))
                {
                    control = new TextBox { Text = ((string)property.GetValue(instructsetting)!).Replace("\n", "\\n"), Location = new Point(150, yPos), Width = 400 };
                    ((TextBox)control).TextChanged += (sender, e) => property.SetValue(instructsetting, ((TextBox)control).Text.Replace("\\n", "\n"));
                }
                else if (property.PropertyType == typeof(bool))
                {
                    control = new CheckBox { Checked = (bool)property.GetValue(instructsetting), Location = new Point(150, yPos) };
                    ((CheckBox)control).CheckedChanged += (sender, e) => property.SetValue(instructsetting, ((CheckBox)control).Checked);
                }
                else if (property.PropertyType == typeof(ICollection<int>))
                {
                    control = new TextBox { Text = string.Join(",", (ICollection<int>)property.GetValue(instructsetting)!), Location = new Point(150, yPos), Width = 400 };
                    ((TextBox)control).TextChanged += (sender, e) => property.SetValue(instructsetting, ((TextBox)control).Text.Split(',').Select(int.Parse).ToList());
                }
                else if (property.PropertyType == typeof(List<string>))
                {
                    control = new TextBox { Text = string.Join(",", (List<string>)property.GetValue(instructsetting) ?? []), Location = new Point(150, yPos), Width = 400 };
                    ((TextBox)control).TextChanged += (sender, e) => property.SetValue(instructsetting, ((TextBox)control).Text.Split(',').ToList());
                }

                if (control != null)
                {
                    label.Location = new Point(10, yPos);
                    control.Location = new Point(250, yPos);
                    target.Controls.Add(label);
                    target.Controls.Add(control);
                    yPos += 30;
                }
            }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8605 // Converting null literal or possible null value to non-nullable type.
        }

        private void listInstruct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (disableevents || listInstruct.SelectedItem == null)
                return;
            SelectedInstructEditor = DataFiles.Instruct[listInstruct.SelectedItem!.ToString()!].Copy<InstructFormat>()!;
            edInstruct.Text = listInstruct.SelectedItem!.ToString();
            CreateInstructControls(panContent, SelectedInstructEditor);
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            var NewName = edInstruct.Text;
            if (string.IsNullOrWhiteSpace(NewName))
            {
                MessageBox.Show("Please select a valide name for the new instruction format.");
                return;
            }
            // If name already exists ask for confirmation
            if (DataFiles.Instruct.ContainsKey(NewName) && (MessageBox.Show("This instruction format already exists, do you want to overwrite it?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No))
                return;
            SelectedInstructEditor.UniqueName = NewName;
            DataFiles.Instruct[NewName] = SelectedInstructEditor;
            (SelectedInstructEditor as IFile).SaveToFile("data/instruct/" + NewName + ".json");
            SetupInstructEditor(NewName);

        }

        private void InstructForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
