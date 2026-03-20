namespace WaifuAI.src.forms
{
    partial class NewSettingsForm
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
            panel1 = new Panel();
            button1 = new Button();
            bt_Close = new Button();
            MainTab = new WaifuAI.Controls.ModernTabControl();
            tabPage1 = new TabPage();
            collapsibleGroupBox2 = new WaifuAI.Controls.CollapsibleGroupBox();
            cbChatThinkPolicy = new WaifuAI.Controls.ModernComboBox();
            collapsibleGroupBox1 = new WaifuAI.Controls.CollapsibleGroupBox();
            ckForceInternalGram = new WaifuAI.Controls.ModernCheckBox();
            ckNoPastInserts = new WaifuAI.Controls.ModernCheckBox();
            ck_hallusafe = new WaifuAI.Controls.ModernCheckBox();
            ck_sysrag = new WaifuAI.Controls.ModernCheckBox();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            tabPage5 = new TabPage();
            tabPage6 = new TabPage();
            label1 = new Label();
            label2 = new Label();
            cbParallel = new WaifuAI.Controls.ModernComboBox();
            label3 = new Label();
            cbChatAllowPrefill = new WaifuAI.Controls.ModernComboBox();
            panel4 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            ckLlamaCppSamplers = new WaifuAI.Controls.ModernCheckBox();
            panel1.SuspendLayout();
            MainTab.SuspendLayout();
            tabPage1.SuspendLayout();
            collapsibleGroupBox2.SuspendLayout();
            collapsibleGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(button1);
            panel1.Controls.Add(bt_Close);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 604);
            panel1.Name = "panel1";
            panel1.Size = new Size(867, 38);
            panel1.TabIndex = 32;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button1.BackColor = Color.DarkSeaGreen;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(861, 32);
            button1.TabIndex = 2;
            button1.Tag = "no-theme";
            button1.Text = "Apply and Close";
            button1.UseVisualStyleBackColor = false;
            // 
            // bt_Close
            // 
            bt_Close.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bt_Close.BackColor = Color.DarkSeaGreen;
            bt_Close.FlatStyle = FlatStyle.Flat;
            bt_Close.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_Close.ForeColor = Color.Black;
            bt_Close.Location = new Point(12, -56);
            bt_Close.Name = "bt_Close";
            bt_Close.Size = new Size(2049, 23);
            bt_Close.TabIndex = 1;
            bt_Close.Tag = "no-theme";
            bt_Close.Text = "Apply and Close";
            bt_Close.UseVisualStyleBackColor = false;
            // 
            // MainTab
            // 
            MainTab.Appearance = TabAppearance.Buttons;
            MainTab.Controls.Add(tabPage1);
            MainTab.Controls.Add(tabPage2);
            MainTab.Controls.Add(tabPage3);
            MainTab.Controls.Add(tabPage4);
            MainTab.Controls.Add(tabPage5);
            MainTab.Controls.Add(tabPage6);
            MainTab.Dock = DockStyle.Fill;
            MainTab.Font = new Font("Segoe UI", 9F);
            MainTab.ItemSize = new Size(0, 36);
            MainTab.Location = new Point(0, 0);
            MainTab.Name = "MainTab";
            MainTab.SelectedIndex = 0;
            MainTab.Size = new Size(867, 604);
            MainTab.TabIndex = 33;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(37, 37, 37);
            tabPage1.Controls.Add(collapsibleGroupBox2);
            tabPage1.Controls.Add(collapsibleGroupBox1);
            tabPage1.Font = new Font("Segoe UI", 9F);
            tabPage1.ForeColor = Color.FromArgb(230, 230, 230);
            tabPage1.Location = new Point(4, 40);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(859, 560);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Core && Backend";
            // 
            // collapsibleGroupBox2
            // 
            collapsibleGroupBox2.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox2.CanCollapse = false;
            collapsibleGroupBox2.Controls.Add(ckLlamaCppSamplers);
            collapsibleGroupBox2.Controls.Add(panel4);
            collapsibleGroupBox2.Controls.Add(cbChatThinkPolicy);
            collapsibleGroupBox2.Controls.Add(label3);
            collapsibleGroupBox2.Controls.Add(panel2);
            collapsibleGroupBox2.Controls.Add(cbChatAllowPrefill);
            collapsibleGroupBox2.Controls.Add(label2);
            collapsibleGroupBox2.Controls.Add(panel3);
            collapsibleGroupBox2.Controls.Add(cbParallel);
            collapsibleGroupBox2.Controls.Add(label1);
            collapsibleGroupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox2.Location = new Point(430, 6);
            collapsibleGroupBox2.Name = "collapsibleGroupBox2";
            collapsibleGroupBox2.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox2.Size = new Size(421, 223);
            collapsibleGroupBox2.TabIndex = 40;
            collapsibleGroupBox2.Text = "Backend-Specific Settings";
            // 
            // cbChatThinkPolicy
            // 
            cbChatThinkPolicy.BackColor = Color.FromArgb(64, 64, 64);
            cbChatThinkPolicy.Dock = DockStyle.Top;
            cbChatThinkPolicy.DropDownHeight = 180;
            cbChatThinkPolicy.Font = new Font("Segoe UI", 9F);
            cbChatThinkPolicy.Items.AddRange(new object[] { "Auto", "Allow", "Disallow" });
            cbChatThinkPolicy.Location = new Point(12, 145);
            cbChatThinkPolicy.MaxDropDownItems = 10;
            cbChatThinkPolicy.Name = "cbChatThinkPolicy";
            cbChatThinkPolicy.Padding = new Padding(1);
            cbChatThinkPolicy.Size = new Size(397, 24);
            cbChatThinkPolicy.TabIndex = 0;
            // 
            // collapsibleGroupBox1
            // 
            collapsibleGroupBox1.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox1.CanCollapse = false;
            collapsibleGroupBox1.Controls.Add(ckForceInternalGram);
            collapsibleGroupBox1.Controls.Add(ckNoPastInserts);
            collapsibleGroupBox1.Controls.Add(ck_hallusafe);
            collapsibleGroupBox1.Controls.Add(ck_sysrag);
            collapsibleGroupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox1.Location = new Point(8, 6);
            collapsibleGroupBox1.Name = "collapsibleGroupBox1";
            collapsibleGroupBox1.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox1.Size = new Size(416, 149);
            collapsibleGroupBox1.TabIndex = 34;
            collapsibleGroupBox1.Text = "LLM Core Settings";
            // 
            // ckForceInternalGram
            // 
            ckForceInternalGram.Dock = DockStyle.Top;
            ckForceInternalGram.Font = new Font("Segoe UI", 9F);
            ckForceInternalGram.Location = new Point(12, 110);
            ckForceInternalGram.Name = "ckForceInternalGram";
            ckForceInternalGram.Size = new Size(392, 26);
            ckForceInternalGram.TabIndex = 39;
            ckForceInternalGram.Text = "Use Internal library's structured output generator";
            ckForceInternalGram.UseVisualStyleBackColor = true;
            // 
            // ckNoPastInserts
            // 
            ckNoPastInserts.Dock = DockStyle.Top;
            ckNoPastInserts.Font = new Font("Segoe UI", 9F);
            ckNoPastInserts.Location = new Point(12, 84);
            ckNoPastInserts.Name = "ckNoPastInserts";
            ckNoPastInserts.Size = new Size(392, 26);
            ckNoPastInserts.TabIndex = 38;
            ckNoPastInserts.Text = "Never insert meta-data and memory when chatting in older session";
            ckNoPastInserts.UseVisualStyleBackColor = true;
            // 
            // ck_hallusafe
            // 
            ck_hallusafe.Dock = DockStyle.Top;
            ck_hallusafe.Font = new Font("Segoe UI", 9F);
            ck_hallusafe.Location = new Point(12, 58);
            ck_hallusafe.Name = "ck_hallusafe";
            ck_hallusafe.Size = new Size(392, 26);
            ck_hallusafe.TabIndex = 37;
            ck_hallusafe.Text = "System Prompt patch to reduce memory-related hallucinations";
            ck_hallusafe.UseVisualStyleBackColor = true;
            // 
            // ck_sysrag
            // 
            ck_sysrag.Dock = DockStyle.Top;
            ck_sysrag.Font = new Font("Segoe UI", 9F);
            ck_sysrag.Location = new Point(12, 32);
            ck_sysrag.Name = "ck_sysrag";
            ck_sysrag.Size = new Size(392, 26);
            ck_sysrag.TabIndex = 36;
            ck_sysrag.Text = "Move all memory inserts to the system prompt ";
            ck_sysrag.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(37, 37, 37);
            tabPage2.Font = new Font("Segoe UI", 9F);
            tabPage2.ForeColor = Color.FromArgb(230, 230, 230);
            tabPage2.Location = new Point(4, 40);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(859, 560);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Web Integration";
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.FromArgb(37, 37, 37);
            tabPage3.Font = new Font("Segoe UI", 9F);
            tabPage3.ForeColor = Color.FromArgb(230, 230, 230);
            tabPage3.Location = new Point(4, 40);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(859, 560);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Memory Systems";
            // 
            // tabPage4
            // 
            tabPage4.BackColor = Color.FromArgb(37, 37, 37);
            tabPage4.Font = new Font("Segoe UI", 9F);
            tabPage4.ForeColor = Color.FromArgb(230, 230, 230);
            tabPage4.Location = new Point(4, 40);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(859, 560);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Group Chat";
            // 
            // tabPage5
            // 
            tabPage5.BackColor = Color.FromArgb(37, 37, 37);
            tabPage5.Font = new Font("Segoe UI", 9F);
            tabPage5.ForeColor = Color.FromArgb(230, 230, 230);
            tabPage5.Location = new Point(4, 40);
            tabPage5.Name = "tabPage5";
            tabPage5.Size = new Size(859, 560);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Output Formatting";
            // 
            // tabPage6
            // 
            tabPage6.BackColor = Color.FromArgb(37, 37, 37);
            tabPage6.Font = new Font("Segoe UI", 9F);
            tabPage6.ForeColor = Color.FromArgb(230, 230, 230);
            tabPage6.Location = new Point(4, 40);
            tabPage6.Name = "tabPage6";
            tabPage6.Size = new Size(859, 560);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "Application";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 9F);
            label1.Location = new Point(12, 32);
            label1.Name = "label1";
            label1.Size = new Size(111, 15);
            label1.TabIndex = 1;
            label1.Text = "Parallel Tool Calling";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Segoe UI", 9F);
            label2.Location = new Point(12, 81);
            label2.Name = "label2";
            label2.Size = new Size(230, 15);
            label2.TabIndex = 3;
            label2.Text = "Chat Completion: Assistant Prefill Allowed";
            // 
            // cbParallel
            // 
            cbParallel.BackColor = Color.FromArgb(64, 64, 64);
            cbParallel.Dock = DockStyle.Top;
            cbParallel.DropDownHeight = 180;
            cbParallel.Font = new Font("Segoe UI", 9F);
            cbParallel.Items.AddRange(new object[] { "Auto", "Allow", "Disallow" });
            cbParallel.Location = new Point(12, 47);
            cbParallel.MaxDropDownItems = 10;
            cbParallel.Name = "cbParallel";
            cbParallel.Padding = new Padding(1);
            cbParallel.Size = new Size(397, 24);
            cbParallel.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Top;
            label3.Font = new Font("Segoe UI", 9F);
            label3.Location = new Point(12, 130);
            label3.Name = "label3";
            label3.Size = new Size(216, 15);
            label3.TabIndex = 5;
            label3.Text = "Chat Completion: First Think Tag Policy";
            // 
            // cbChatAllowPrefill
            // 
            cbChatAllowPrefill.BackColor = Color.FromArgb(64, 64, 64);
            cbChatAllowPrefill.Dock = DockStyle.Top;
            cbChatAllowPrefill.DropDownHeight = 180;
            cbChatAllowPrefill.Font = new Font("Segoe UI", 9F);
            cbChatAllowPrefill.Items.AddRange(new object[] { "Auto", "Allow", "Disallow" });
            cbChatAllowPrefill.Location = new Point(12, 96);
            cbChatAllowPrefill.MaxDropDownItems = 10;
            cbChatAllowPrefill.Name = "cbChatAllowPrefill";
            cbChatAllowPrefill.Padding = new Padding(1);
            cbChatAllowPrefill.Size = new Size(397, 24);
            cbChatAllowPrefill.TabIndex = 4;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(12, 169);
            panel4.Margin = new Padding(8);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(8);
            panel4.Size = new Size(397, 10);
            panel4.TabIndex = 55;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(12, 120);
            panel2.Margin = new Padding(8);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(8);
            panel2.Size = new Size(397, 10);
            panel2.TabIndex = 56;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(12, 71);
            panel3.Margin = new Padding(8);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(8);
            panel3.Size = new Size(397, 10);
            panel3.TabIndex = 57;
            // 
            // ckLlamaCppSamplers
            // 
            ckLlamaCppSamplers.Dock = DockStyle.Top;
            ckLlamaCppSamplers.Font = new Font("Segoe UI", 9F);
            ckLlamaCppSamplers.Location = new Point(12, 179);
            ckLlamaCppSamplers.Name = "ckLlamaCppSamplers";
            ckLlamaCppSamplers.Size = new Size(397, 26);
            ckLlamaCppSamplers.TabIndex = 58;
            ckLlamaCppSamplers.Text = "Allow advanced samplers in Llama.cpp (requires --props)";
            ckLlamaCppSamplers.UseVisualStyleBackColor = true;
            // 
            // NewSettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(867, 642);
            Controls.Add(MainTab);
            Controls.Add(panel1);
            Name = "NewSettingsForm";
            Text = "NewSettingsForm";
            panel1.ResumeLayout(false);
            MainTab.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            collapsibleGroupBox2.ResumeLayout(false);
            collapsibleGroupBox2.PerformLayout();
            collapsibleGroupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button bt_Close;
        private Button button1;
        private Controls.ModernTabControl MainTab;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private Controls.CollapsibleGroupBox collapsibleGroupBox1;
        private Controls.ModernCheckBox ckForceInternalGram;
        private Controls.ModernCheckBox ckNoPastInserts;
        private Controls.ModernCheckBox ck_hallusafe;
        private Controls.ModernCheckBox ck_sysrag;
        private Controls.CollapsibleGroupBox collapsibleGroupBox2;
        private Controls.ModernComboBox cbChatThinkPolicy;
        private Label label1;
        private Label label2;
        private Controls.ModernComboBox cbParallel;
        private Label label3;
        private Controls.ModernComboBox cbChatAllowPrefill;
        private Controls.ModernCheckBox ckLlamaCppSamplers;
        private Panel panel4;
        private Panel panel2;
        private Panel panel3;
    }
}