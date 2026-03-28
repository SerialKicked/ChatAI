using LetheChat.Controls;

namespace LetheChat.Forms
{
    partial class SysPromptForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PanMenu = new Panel();
            edFileName = new TextBox();
            listPrompt = new ModernListBox();
            panel3 = new Panel();
            btSave = new Button();
            panel1 = new Panel();
            ed_editsys_prefix = new TextBox();
            label55 = new Label();
            ed_editsys_worldinfo = new TextBox();
            label54 = new Label();
            ed_editsys_dialogs = new TextBox();
            label53 = new Label();
            ed_editsys_scenario = new TextBox();
            label52 = new Label();
            ed_editsys_prompt = new TextBox();
            collapsibleGroupBox1 = new CollapsibleGroupBox();
            collapsibleGroupBox2 = new CollapsibleGroupBox();
            ed_corefacts = new TextBox();
            label1 = new Label();
            ed_selfedit = new TextBox();
            label2 = new Label();
            PanMenu.SuspendLayout();
            collapsibleGroupBox1.SuspendLayout();
            collapsibleGroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // PanMenu
            // 
            PanMenu.Controls.Add(edFileName);
            PanMenu.Controls.Add(listPrompt);
            PanMenu.Controls.Add(panel3);
            PanMenu.Controls.Add(btSave);
            PanMenu.Controls.Add(panel1);
            PanMenu.Dock = DockStyle.Left;
            PanMenu.Location = new Point(0, 0);
            PanMenu.Name = "PanMenu";
            PanMenu.Size = new Size(234, 493);
            PanMenu.TabIndex = 0;
            // 
            // edFileName
            // 
            edFileName.Dock = DockStyle.Bottom;
            edFileName.Location = new Point(0, 427);
            edFileName.Name = "edFileName";
            edFileName.PlaceholderText = "Filename for prompt";
            edFileName.Size = new Size(234, 23);
            edFileName.TabIndex = 5;
            // 
            // listPrompt
            // 
            listPrompt.BackColor = Color.FromArgb(64, 64, 64);
            listPrompt.BorderStyle = BorderStyle.FixedSingle;
            listPrompt.Dock = DockStyle.Top;
            listPrompt.Font = new Font("Segoe UI", 9.25F);
            listPrompt.Location = new Point(0, 0);
            listPrompt.Name = "listPrompt";
            listPrompt.Padding = new Padding(1);
            listPrompt.Size = new Size(234, 418);
            listPrompt.TabIndex = 4;
            listPrompt.SelectedIndexChanged += listPrompt_SelectedIndexChanged;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 450);
            panel3.Margin = new Padding(8);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(8);
            panel3.Size = new Size(234, 10);
            panel3.TabIndex = 52;
            // 
            // btSave
            // 
            btSave.BackColor = Color.DarkSeaGreen;
            btSave.Dock = DockStyle.Bottom;
            btSave.FlatStyle = FlatStyle.Flat;
            btSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btSave.ForeColor = Color.Black;
            btSave.Location = new Point(0, 460);
            btSave.Name = "btSave";
            btSave.Size = new Size(234, 23);
            btSave.TabIndex = 6;
            btSave.Tag = "no-theme";
            btSave.Text = "Save System Prompt";
            btSave.UseVisualStyleBackColor = false;
            btSave.Click += btSave_Click;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 483);
            panel1.Margin = new Padding(8);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(8);
            panel1.Size = new Size(234, 10);
            panel1.TabIndex = 53;
            // 
            // ed_editsys_prefix
            // 
            ed_editsys_prefix.BorderStyle = BorderStyle.FixedSingle;
            ed_editsys_prefix.Location = new Point(15, 196);
            ed_editsys_prefix.Name = "ed_editsys_prefix";
            ed_editsys_prefix.Size = new Size(339, 24);
            ed_editsys_prefix.TabIndex = 7;
            // 
            // label55
            // 
            label55.AutoSize = true;
            label55.Location = new Point(15, 176);
            label55.Name = "label55";
            label55.Size = new Size(143, 17);
            label55.TabIndex = 6;
            label55.Text = "Category Section Prefix";
            // 
            // ed_editsys_worldinfo
            // 
            ed_editsys_worldinfo.BorderStyle = BorderStyle.FixedSingle;
            ed_editsys_worldinfo.Location = new Point(15, 149);
            ed_editsys_worldinfo.Name = "ed_editsys_worldinfo";
            ed_editsys_worldinfo.Size = new Size(339, 24);
            ed_editsys_worldinfo.TabIndex = 5;
            // 
            // label54
            // 
            label54.AutoSize = true;
            label54.Location = new Point(15, 129);
            label54.Name = "label54";
            label54.Size = new Size(143, 17);
            label54.TabIndex = 4;
            label54.Text = "Memory Section Title";
            // 
            // ed_editsys_dialogs
            // 
            ed_editsys_dialogs.BorderStyle = BorderStyle.FixedSingle;
            ed_editsys_dialogs.Location = new Point(15, 102);
            ed_editsys_dialogs.Name = "ed_editsys_dialogs";
            ed_editsys_dialogs.Size = new Size(339, 24);
            ed_editsys_dialogs.TabIndex = 3;
            // 
            // label53
            // 
            label53.AutoSize = true;
            label53.Location = new Point(15, 82);
            label53.Name = "label53";
            label53.Size = new Size(193, 17);
            label53.TabIndex = 2;
            label53.Text = "Example Dialogs Title (optional)";
            // 
            // ed_editsys_scenario
            // 
            ed_editsys_scenario.BorderStyle = BorderStyle.FixedSingle;
            ed_editsys_scenario.Location = new Point(15, 55);
            ed_editsys_scenario.Name = "ed_editsys_scenario";
            ed_editsys_scenario.Size = new Size(339, 24);
            ed_editsys_scenario.TabIndex = 1;
            // 
            // label52
            // 
            label52.AutoSize = true;
            label52.Location = new Point(15, 35);
            label52.Name = "label52";
            label52.Size = new Size(192, 17);
            label52.TabIndex = 0;
            label52.Text = "Scenario Section Title (optional)";
            // 
            // ed_editsys_prompt
            // 
            ed_editsys_prompt.BorderStyle = BorderStyle.FixedSingle;
            ed_editsys_prompt.Location = new Point(15, 35);
            ed_editsys_prompt.Multiline = true;
            ed_editsys_prompt.Name = "ed_editsys_prompt";
            ed_editsys_prompt.ScrollBars = ScrollBars.Vertical;
            ed_editsys_prompt.Size = new Size(586, 422);
            ed_editsys_prompt.TabIndex = 0;
            // 
            // collapsibleGroupBox1
            // 
            collapsibleGroupBox1.BackColor = Color.FromArgb(37, 37, 37);
            collapsibleGroupBox1.CanCollapse = false;
            collapsibleGroupBox1.Controls.Add(ed_editsys_prompt);
            collapsibleGroupBox1.Font = new Font("Segoe UI", 9.25F);
            collapsibleGroupBox1.Location = new Point(240, 12);
            collapsibleGroupBox1.Name = "collapsibleGroupBox1";
            collapsibleGroupBox1.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox1.Size = new Size(616, 470);
            collapsibleGroupBox1.TabIndex = 4;
            collapsibleGroupBox1.Text = "Main Prompt (intro, char and user bio)";
            // 
            // collapsibleGroupBox2
            // 
            collapsibleGroupBox2.BackColor = Color.FromArgb(37, 37, 37);
            collapsibleGroupBox2.CanCollapse = false;
            collapsibleGroupBox2.Controls.Add(ed_selfedit);
            collapsibleGroupBox2.Controls.Add(label2);
            collapsibleGroupBox2.Controls.Add(ed_corefacts);
            collapsibleGroupBox2.Controls.Add(label1);
            collapsibleGroupBox2.Controls.Add(ed_editsys_prefix);
            collapsibleGroupBox2.Controls.Add(label52);
            collapsibleGroupBox2.Controls.Add(label55);
            collapsibleGroupBox2.Controls.Add(ed_editsys_scenario);
            collapsibleGroupBox2.Controls.Add(ed_editsys_worldinfo);
            collapsibleGroupBox2.Controls.Add(label53);
            collapsibleGroupBox2.Controls.Add(label54);
            collapsibleGroupBox2.Controls.Add(ed_editsys_dialogs);
            collapsibleGroupBox2.Font = new Font("Segoe UI", 9.25F);
            collapsibleGroupBox2.Location = new Point(862, 12);
            collapsibleGroupBox2.Name = "collapsibleGroupBox2";
            collapsibleGroupBox2.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox2.Size = new Size(369, 470);
            collapsibleGroupBox2.TabIndex = 5;
            collapsibleGroupBox2.Text = "Section Titles";
            // 
            // ed_corefacts
            // 
            ed_corefacts.BorderStyle = BorderStyle.FixedSingle;
            ed_corefacts.Location = new Point(15, 243);
            ed_corefacts.Name = "ed_corefacts";
            ed_corefacts.Size = new Size(339, 24);
            ed_corefacts.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 223);
            label1.Name = "label1";
            label1.Size = new Size(100, 17);
            label1.TabIndex = 8;
            label1.Text = "Core User Facts";
            // 
            // ed_selfedit
            // 
            ed_selfedit.BorderStyle = BorderStyle.FixedSingle;
            ed_selfedit.Location = new Point(15, 292);
            ed_selfedit.Name = "ed_selfedit";
            ed_selfedit.Size = new Size(339, 24);
            ed_selfedit.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 272);
            label2.Name = "label2";
            label2.Size = new Size(155, 17);
            label2.TabIndex = 10;
            label2.Text = "Inner Thoughts (optional)";
            // 
            // SysPromptForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1243, 493);
            Controls.Add(collapsibleGroupBox2);
            Controls.Add(collapsibleGroupBox1);
            Controls.Add(PanMenu);
            ForeColor = Color.WhiteSmoke;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SysPromptForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "System Prompt Editor";
            KeyDown += SysPromptForm_KeyDown;
            PanMenu.ResumeLayout(false);
            PanMenu.PerformLayout();
            collapsibleGroupBox1.ResumeLayout(false);
            collapsibleGroupBox1.PerformLayout();
            collapsibleGroupBox2.ResumeLayout(false);
            collapsibleGroupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanMenu;
        private Button btSave;
        private TextBox edFileName;
        private Controls.ModernListBox listPrompt;
        private TextBox ed_editsys_prefix;
        private Label label55;
        private TextBox ed_editsys_worldinfo;
        private Label label54;
        private TextBox ed_editsys_dialogs;
        private Label label53;
        private TextBox ed_editsys_scenario;
        private Label label52;
        private TextBox ed_editsys_prompt;
        private Controls.CollapsibleGroupBox collapsibleGroupBox1;
        private Controls.CollapsibleGroupBox collapsibleGroupBox2;
        private Panel panel3;
        private Panel panel1;
        private TextBox ed_corefacts;
        private Label label1;
        private TextBox ed_selfedit;
        private Label label2;
    }
}