namespace WaifuAI.src.forms
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
            btSave = new Button();
            edFileName = new TextBox();
            listPrompt = new ListBox();
            groupBox22 = new GroupBox();
            ed_editsys_prefix = new TextBox();
            label55 = new Label();
            ed_editsys_worldinfo = new TextBox();
            label54 = new Label();
            ed_editsys_dialogs = new TextBox();
            label53 = new Label();
            ed_editsys_scenario = new TextBox();
            label52 = new Label();
            groupBox21 = new GroupBox();
            ed_editsys_prompt = new TextBox();
            PanMenu.SuspendLayout();
            groupBox22.SuspendLayout();
            groupBox21.SuspendLayout();
            SuspendLayout();
            // 
            // PanMenu
            // 
            PanMenu.Controls.Add(btSave);
            PanMenu.Controls.Add(edFileName);
            PanMenu.Controls.Add(listPrompt);
            PanMenu.Dock = DockStyle.Left;
            PanMenu.Location = new Point(0, 0);
            PanMenu.Name = "PanMenu";
            PanMenu.Size = new Size(234, 518);
            PanMenu.TabIndex = 0;
            // 
            // btSave
            // 
            btSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btSave.Location = new Point(3, 492);
            btSave.Name = "btSave";
            btSave.Size = new Size(228, 23);
            btSave.TabIndex = 6;
            btSave.Text = "Save System Prompt";
            btSave.UseVisualStyleBackColor = true;
            btSave.Click += btSave_Click;
            // 
            // edFileName
            // 
            edFileName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            edFileName.Location = new Point(3, 463);
            edFileName.Name = "edFileName";
            edFileName.PlaceholderText = "Filename for prompt";
            edFileName.Size = new Size(228, 23);
            edFileName.TabIndex = 5;
            // 
            // listPrompt
            // 
            listPrompt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listPrompt.BorderStyle = BorderStyle.FixedSingle;
            listPrompt.FormattingEnabled = true;
            listPrompt.Location = new Point(3, 3);
            listPrompt.Name = "listPrompt";
            listPrompt.ScrollAlwaysVisible = true;
            listPrompt.Size = new Size(228, 452);
            listPrompt.TabIndex = 4;
            listPrompt.SelectedIndexChanged += listPrompt_SelectedIndexChanged;
            // 
            // groupBox22
            // 
            groupBox22.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox22.Controls.Add(ed_editsys_prefix);
            groupBox22.Controls.Add(label55);
            groupBox22.Controls.Add(ed_editsys_worldinfo);
            groupBox22.Controls.Add(label54);
            groupBox22.Controls.Add(ed_editsys_dialogs);
            groupBox22.Controls.Add(label53);
            groupBox22.Controls.Add(ed_editsys_scenario);
            groupBox22.Controls.Add(label52);
            groupBox22.Location = new Point(862, 3);
            groupBox22.Name = "groupBox22";
            groupBox22.Size = new Size(369, 512);
            groupBox22.TabIndex = 3;
            groupBox22.TabStop = false;
            groupBox22.Text = "Section Titles";
            // 
            // ed_editsys_prefix
            // 
            ed_editsys_prefix.Location = new Point(6, 171);
            ed_editsys_prefix.Name = "ed_editsys_prefix";
            ed_editsys_prefix.Size = new Size(357, 23);
            ed_editsys_prefix.TabIndex = 7;
            // 
            // label55
            // 
            label55.AutoSize = true;
            label55.Location = new Point(6, 153);
            label55.Name = "label55";
            label55.Size = new Size(129, 15);
            label55.TabIndex = 6;
            label55.Text = "Category Section Prefix";
            // 
            // ed_editsys_worldinfo
            // 
            ed_editsys_worldinfo.Location = new Point(6, 127);
            ed_editsys_worldinfo.Name = "ed_editsys_worldinfo";
            ed_editsys_worldinfo.Size = new Size(357, 23);
            ed_editsys_worldinfo.TabIndex = 5;
            // 
            // label54
            // 
            label54.AutoSize = true;
            label54.Location = new Point(6, 109);
            label54.Name = "label54";
            label54.Size = new Size(131, 15);
            label54.TabIndex = 4;
            label54.Text = "World Info Section Title";
            // 
            // ed_editsys_dialogs
            // 
            ed_editsys_dialogs.Location = new Point(6, 83);
            ed_editsys_dialogs.Name = "ed_editsys_dialogs";
            ed_editsys_dialogs.Size = new Size(357, 23);
            ed_editsys_dialogs.TabIndex = 3;
            // 
            // label53
            // 
            label53.AutoSize = true;
            label53.Location = new Point(6, 65);
            label53.Name = "label53";
            label53.Size = new Size(174, 15);
            label53.TabIndex = 2;
            label53.Text = "Example Dialogs Title (optional)";
            // 
            // ed_editsys_scenario
            // 
            ed_editsys_scenario.Location = new Point(6, 39);
            ed_editsys_scenario.Name = "ed_editsys_scenario";
            ed_editsys_scenario.Size = new Size(357, 23);
            ed_editsys_scenario.TabIndex = 1;
            // 
            // label52
            // 
            label52.AutoSize = true;
            label52.Location = new Point(6, 21);
            label52.Name = "label52";
            label52.Size = new Size(120, 15);
            label52.TabIndex = 0;
            label52.Text = "Scenario Section Title";
            // 
            // groupBox21
            // 
            groupBox21.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox21.Controls.Add(ed_editsys_prompt);
            groupBox21.Location = new Point(240, 3);
            groupBox21.Name = "groupBox21";
            groupBox21.Size = new Size(616, 512);
            groupBox21.TabIndex = 2;
            groupBox21.TabStop = false;
            groupBox21.Text = "Main Prompt (intro, char and user bio)";
            // 
            // ed_editsys_prompt
            // 
            ed_editsys_prompt.BorderStyle = BorderStyle.FixedSingle;
            ed_editsys_prompt.Location = new Point(6, 22);
            ed_editsys_prompt.Multiline = true;
            ed_editsys_prompt.Name = "ed_editsys_prompt";
            ed_editsys_prompt.ScrollBars = ScrollBars.Vertical;
            ed_editsys_prompt.Size = new Size(604, 481);
            ed_editsys_prompt.TabIndex = 0;
            // 
            // SysPromptForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1243, 518);
            Controls.Add(groupBox22);
            Controls.Add(groupBox21);
            Controls.Add(PanMenu);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SysPromptForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "System Prompt Editor";
            PanMenu.ResumeLayout(false);
            PanMenu.PerformLayout();
            groupBox22.ResumeLayout(false);
            groupBox22.PerformLayout();
            groupBox21.ResumeLayout(false);
            groupBox21.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanMenu;
        private Button btSave;
        private TextBox edFileName;
        private ListBox listPrompt;
        private GroupBox groupBox22;
        private TextBox ed_editsys_prefix;
        private Label label55;
        private TextBox ed_editsys_worldinfo;
        private Label label54;
        private TextBox ed_editsys_dialogs;
        private Label label53;
        private TextBox ed_editsys_scenario;
        private Label label52;
        private GroupBox groupBox21;
        private TextBox ed_editsys_prompt;
    }
}