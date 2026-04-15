using LetheChat.Controls;

namespace LetheChat.Forms
{
    partial class InstructForm
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
            edInstruct = new TextBox();
            verticalStackPanel1 = new VerticalStackPanel();
            listInstruct = new ModernListBox();
            groupBox14 = new CollapsibleGroupBox();
            ed_botsuffix = new TextBox();
            label5 = new Label();
            ed_botprefix = new TextBox();
            label17 = new Label();
            ed_usersuffix = new TextBox();
            label3 = new Label();
            ed_userprefix = new TextBox();
            label4 = new Label();
            ed_syssuffix = new TextBox();
            label1 = new Label();
            ed_sysprefix = new TextBox();
            label2 = new Label();
            ed_bos = new TextBox();
            label12 = new Label();
            ck_newlines = new ModernCheckBox();
            collapsibleGroupBox1 = new CollapsibleGroupBox();
            ck_disablinstructstopstrings = new ModernCheckBox();
            ed_botsuffixoverride = new TextBox();
            label7 = new Label();
            ed_botprefixoverride = new TextBox();
            label8 = new Label();
            ed_stopstrings = new TextBox();
            label6 = new Label();
            ed_stopsequence = new TextBox();
            label11 = new Label();
            collapsibleGroupBox2 = new CollapsibleGroupBox();
            ck_emptythink = new ModernCheckBox();
            ed_thinkgroup = new TextBox();
            label16 = new Label();
            ed_thinkprefill = new TextBox();
            label15 = new Label();
            ed_thinksyssuffix = new TextBox();
            label14 = new Label();
            ed_thinksysprefix = new TextBox();
            label13 = new Label();
            ck_thinkprefill = new ModernCheckBox();
            ed_thinkend = new TextBox();
            label9 = new Label();
            ed_thinkstart = new TextBox();
            label10 = new Label();
            bt_Save = new Button();
            bt_delete = new Button();
            verticalStackPanel1.SuspendLayout();
            groupBox14.SuspendLayout();
            collapsibleGroupBox1.SuspendLayout();
            collapsibleGroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // edInstruct
            // 
            edInstruct.BorderStyle = BorderStyle.FixedSingle;
            edInstruct.Location = new Point(4, 466);
            edInstruct.Name = "edInstruct";
            edInstruct.Size = new Size(277, 23);
            edInstruct.TabIndex = 2;
            // 
            // verticalStackPanel1
            // 
            verticalStackPanel1.Controls.Add(bt_delete);
            verticalStackPanel1.Controls.Add(bt_Save);
            verticalStackPanel1.Controls.Add(edInstruct);
            verticalStackPanel1.Controls.Add(listInstruct);
            verticalStackPanel1.Dock = DockStyle.Left;
            verticalStackPanel1.Location = new Point(0, 0);
            verticalStackPanel1.Name = "verticalStackPanel1";
            verticalStackPanel1.Padding = new Padding(4, 8, 4, 0);
            verticalStackPanel1.Size = new Size(285, 564);
            verticalStackPanel1.TabIndex = 2;
            // 
            // listInstruct
            // 
            listInstruct.BackColor = Color.FromArgb(64, 64, 64);
            listInstruct.BorderStyle = BorderStyle.FixedSingle;
            listInstruct.Dock = DockStyle.Top;
            listInstruct.Font = new Font("Segoe UI", 9.25F);
            listInstruct.Location = new Point(4, 8);
            listInstruct.Name = "listInstruct";
            listInstruct.Padding = new Padding(1);
            listInstruct.Size = new Size(277, 450);
            listInstruct.TabIndex = 2;
            listInstruct.SelectedIndexChanged += listInstruct_SelectedIndexChanged;
            // 
            // groupBox14
            // 
            groupBox14.BackColor = Color.FromArgb(37, 37, 37);
            groupBox14.CanCollapse = false;
            groupBox14.Controls.Add(ed_botsuffix);
            groupBox14.Controls.Add(label5);
            groupBox14.Controls.Add(ed_botprefix);
            groupBox14.Controls.Add(label17);
            groupBox14.Controls.Add(ed_usersuffix);
            groupBox14.Controls.Add(label3);
            groupBox14.Controls.Add(ed_userprefix);
            groupBox14.Controls.Add(label4);
            groupBox14.Controls.Add(ed_syssuffix);
            groupBox14.Controls.Add(label1);
            groupBox14.Controls.Add(ed_sysprefix);
            groupBox14.Controls.Add(label2);
            groupBox14.Controls.Add(ed_bos);
            groupBox14.Controls.Add(label12);
            groupBox14.Controls.Add(ck_newlines);
            groupBox14.Font = new Font("Segoe UI", 9.25F);
            groupBox14.Location = new Point(291, 8);
            groupBox14.Name = "groupBox14";
            groupBox14.Padding = new Padding(12, 32, 12, 10);
            groupBox14.Size = new Size(514, 286);
            groupBox14.TabIndex = 5;
            groupBox14.TabStop = false;
            groupBox14.Text = "Message Formatting (Required)";
            // 
            // ed_botsuffix
            // 
            ed_botsuffix.Location = new Point(257, 203);
            ed_botsuffix.Name = "ed_botsuffix";
            ed_botsuffix.Size = new Size(242, 24);
            ed_botsuffix.TabIndex = 39;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(257, 183);
            label5.Name = "label5";
            label5.Size = new Size(94, 17);
            label5.TabIndex = 38;
            label5.Text = "Assistant Suffix";
            // 
            // ed_botprefix
            // 
            ed_botprefix.Location = new Point(6, 203);
            ed_botprefix.Name = "ed_botprefix";
            ed_botprefix.Size = new Size(242, 24);
            ed_botprefix.TabIndex = 37;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(6, 183);
            label17.Name = "label17";
            label17.Size = new Size(95, 17);
            label17.TabIndex = 36;
            label17.Text = "Assistant Prefix";
            // 
            // ed_usersuffix
            // 
            ed_usersuffix.Location = new Point(257, 156);
            ed_usersuffix.Name = "ed_usersuffix";
            ed_usersuffix.Size = new Size(242, 24);
            ed_usersuffix.TabIndex = 35;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(257, 136);
            label3.Name = "label3";
            label3.Size = new Size(70, 17);
            label3.TabIndex = 34;
            label3.Text = "User Suffix";
            // 
            // ed_userprefix
            // 
            ed_userprefix.Location = new Point(6, 156);
            ed_userprefix.Name = "ed_userprefix";
            ed_userprefix.Size = new Size(242, 24);
            ed_userprefix.TabIndex = 33;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 136);
            label4.Name = "label4";
            label4.Size = new Size(71, 17);
            label4.TabIndex = 32;
            label4.Text = "User Prefix";
            // 
            // ed_syssuffix
            // 
            ed_syssuffix.Location = new Point(257, 109);
            ed_syssuffix.Name = "ed_syssuffix";
            ed_syssuffix.Size = new Size(242, 24);
            ed_syssuffix.TabIndex = 31;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(257, 89);
            label1.Name = "label1";
            label1.Size = new Size(84, 17);
            label1.TabIndex = 30;
            label1.Text = "System Suffix";
            // 
            // ed_sysprefix
            // 
            ed_sysprefix.Location = new Point(6, 109);
            ed_sysprefix.Name = "ed_sysprefix";
            ed_sysprefix.Size = new Size(242, 24);
            ed_sysprefix.TabIndex = 29;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 89);
            label2.Name = "label2";
            label2.Size = new Size(85, 17);
            label2.TabIndex = 28;
            label2.Text = "System Prefix";
            // 
            // ed_bos
            // 
            ed_bos.Location = new Point(100, 46);
            ed_bos.Name = "ed_bos";
            ed_bos.Size = new Size(143, 24);
            ed_bos.TabIndex = 26;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 49);
            label12.Name = "label12";
            label12.Size = new Size(71, 17);
            label12.TabIndex = 25;
            label12.Text = "BoS Token:";
            // 
            // ck_newlines
            // 
            ck_newlines.Font = new Font("Segoe UI", 9F);
            ck_newlines.Location = new Point(6, 248);
            ck_newlines.Name = "ck_newlines";
            ck_newlines.Size = new Size(493, 26);
            ck_newlines.TabIndex = 24;
            ck_newlines.Text = "New lines between messages";
            ck_newlines.UseVisualStyleBackColor = true;
            // 
            // collapsibleGroupBox1
            // 
            collapsibleGroupBox1.BackColor = Color.FromArgb(37, 37, 37);
            collapsibleGroupBox1.CanCollapse = false;
            collapsibleGroupBox1.Controls.Add(ck_disablinstructstopstrings);
            collapsibleGroupBox1.Controls.Add(ed_botsuffixoverride);
            collapsibleGroupBox1.Controls.Add(label7);
            collapsibleGroupBox1.Controls.Add(ed_botprefixoverride);
            collapsibleGroupBox1.Controls.Add(label8);
            collapsibleGroupBox1.Controls.Add(ed_stopstrings);
            collapsibleGroupBox1.Controls.Add(label6);
            collapsibleGroupBox1.Controls.Add(ed_stopsequence);
            collapsibleGroupBox1.Controls.Add(label11);
            collapsibleGroupBox1.Font = new Font("Segoe UI", 9.25F);
            collapsibleGroupBox1.Location = new Point(291, 300);
            collapsibleGroupBox1.Name = "collapsibleGroupBox1";
            collapsibleGroupBox1.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox1.Size = new Size(514, 251);
            collapsibleGroupBox1.TabIndex = 6;
            collapsibleGroupBox1.TabStop = false;
            collapsibleGroupBox1.Text = "Flow Control (Advanced)";
            // 
            // ck_disablinstructstopstrings
            // 
            ck_disablinstructstopstrings.Font = new Font("Segoe UI", 9F);
            ck_disablinstructstopstrings.Location = new Point(6, 189);
            ck_disablinstructstopstrings.Name = "ck_disablinstructstopstrings";
            ck_disablinstructstopstrings.Size = new Size(493, 26);
            ck_disablinstructstopstrings.TabIndex = 29;
            ck_disablinstructstopstrings.Text = "Disable default stop strings";
            ck_disablinstructstopstrings.UseVisualStyleBackColor = true;
            // 
            // ed_botsuffixoverride
            // 
            ed_botsuffixoverride.Location = new Point(257, 159);
            ed_botsuffixoverride.Name = "ed_botsuffixoverride";
            ed_botsuffixoverride.Size = new Size(242, 24);
            ed_botsuffixoverride.TabIndex = 27;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(257, 139);
            label7.Name = "label7";
            label7.Size = new Size(117, 17);
            label7.TabIndex = 26;
            label7.Text = "Bot Suffix Override";
            // 
            // ed_botprefixoverride
            // 
            ed_botprefixoverride.Location = new Point(6, 159);
            ed_botprefixoverride.Name = "ed_botprefixoverride";
            ed_botprefixoverride.Size = new Size(242, 24);
            ed_botprefixoverride.TabIndex = 25;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 139);
            label8.Name = "label8";
            label8.Size = new Size(118, 17);
            label8.TabIndex = 24;
            label8.Text = "Bot Prefix Override";
            // 
            // ed_stopstrings
            // 
            ed_stopstrings.Location = new Point(6, 109);
            ed_stopstrings.Name = "ed_stopstrings";
            ed_stopstrings.Size = new Size(493, 24);
            ed_stopstrings.TabIndex = 15;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 89);
            label6.Name = "label6";
            label6.Size = new Size(245, 17);
            label6.TabIndex = 14;
            label6.Text = "Custom Stop Strings (comma separated)";
            // 
            // ed_stopsequence
            // 
            ed_stopsequence.Location = new Point(6, 60);
            ed_stopsequence.Name = "ed_stopsequence";
            ed_stopsequence.Size = new Size(493, 24);
            ed_stopsequence.TabIndex = 13;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 40);
            label11.Name = "label11";
            label11.Size = new Size(98, 17);
            label11.TabIndex = 12;
            label11.Text = "Stop Sequence:";
            // 
            // collapsibleGroupBox2
            // 
            collapsibleGroupBox2.BackColor = Color.FromArgb(37, 37, 37);
            collapsibleGroupBox2.CanCollapse = false;
            collapsibleGroupBox2.Controls.Add(ck_emptythink);
            collapsibleGroupBox2.Controls.Add(ed_thinkgroup);
            collapsibleGroupBox2.Controls.Add(label16);
            collapsibleGroupBox2.Controls.Add(ed_thinkprefill);
            collapsibleGroupBox2.Controls.Add(label15);
            collapsibleGroupBox2.Controls.Add(ed_thinksyssuffix);
            collapsibleGroupBox2.Controls.Add(label14);
            collapsibleGroupBox2.Controls.Add(ed_thinksysprefix);
            collapsibleGroupBox2.Controls.Add(label13);
            collapsibleGroupBox2.Controls.Add(ck_thinkprefill);
            collapsibleGroupBox2.Controls.Add(ed_thinkend);
            collapsibleGroupBox2.Controls.Add(label9);
            collapsibleGroupBox2.Controls.Add(ed_thinkstart);
            collapsibleGroupBox2.Controls.Add(label10);
            collapsibleGroupBox2.Font = new Font("Segoe UI", 9.25F);
            collapsibleGroupBox2.Location = new Point(811, 8);
            collapsibleGroupBox2.Name = "collapsibleGroupBox2";
            collapsibleGroupBox2.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox2.Size = new Size(514, 544);
            collapsibleGroupBox2.TabIndex = 7;
            collapsibleGroupBox2.TabStop = false;
            collapsibleGroupBox2.Text = "Thinking / CoT Models";
            // 
            // ck_emptythink
            // 
            ck_emptythink.Font = new Font("Segoe UI", 9F);
            ck_emptythink.Location = new Point(6, 320);
            ck_emptythink.Name = "ck_emptythink";
            ck_emptythink.Size = new Size(493, 26);
            ck_emptythink.TabIndex = 37;
            ck_emptythink.Text = "Prefill empty thinking block when thinking is disabled";
            ck_emptythink.UseVisualStyleBackColor = true;
            // 
            // ed_thinkgroup
            // 
            ed_thinkgroup.Location = new Point(6, 250);
            ed_thinkgroup.Name = "ed_thinkgroup";
            ed_thinkgroup.Size = new Size(493, 24);
            ed_thinkgroup.TabIndex = 36;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(6, 230);
            label16.Name = "label16";
            label16.Size = new Size(470, 17);
            label16.TabIndex = 35;
            label16.Text = "Prefill thinking block with this during group chat (should indicate character turn)";
            // 
            // ed_thinkprefill
            // 
            ed_thinkprefill.Location = new Point(6, 203);
            ed_thinkprefill.Name = "ed_thinkprefill";
            ed_thinkprefill.Size = new Size(493, 24);
            ed_thinkprefill.TabIndex = 34;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(6, 183);
            label15.Name = "label15";
            label15.Size = new Size(307, 17);
            label15.TabIndex = 33;
            label15.Text = "Prefill thinking block with this text (bias the thinking)";
            // 
            // ed_thinksyssuffix
            // 
            ed_thinksyssuffix.Location = new Point(6, 156);
            ed_thinksyssuffix.Name = "ed_thinksyssuffix";
            ed_thinksyssuffix.Size = new Size(493, 24);
            ed_thinksyssuffix.TabIndex = 32;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 136);
            label14.Name = "label14";
            label14.Size = new Size(340, 17);
            label14.TabIndex = 31;
            label14.Text = "Add at the end of System Prompt when thinking enabled:";
            // 
            // ed_thinksysprefix
            // 
            ed_thinksysprefix.Location = new Point(6, 109);
            ed_thinksysprefix.Name = "ed_thinksysprefix";
            ed_thinksysprefix.Size = new Size(493, 24);
            ed_thinksysprefix.TabIndex = 30;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 89);
            label13.Name = "label13";
            label13.Size = new Size(344, 17);
            label13.TabIndex = 29;
            label13.Text = "Add at the start of System Prompt when thinking enabled:";
            // 
            // ck_thinkprefill
            // 
            ck_thinkprefill.Font = new Font("Segoe UI", 9F);
            ck_thinkprefill.Location = new Point(6, 289);
            ck_thinkprefill.Name = "ck_thinkprefill";
            ck_thinkprefill.Size = new Size(493, 26);
            ck_thinkprefill.TabIndex = 28;
            ck_thinkprefill.Text = "Prefill starting thinking token when thinking is enabled";
            ck_thinkprefill.UseVisualStyleBackColor = true;
            // 
            // ed_thinkend
            // 
            ed_thinkend.Location = new Point(257, 56);
            ed_thinkend.Name = "ed_thinkend";
            ed_thinkend.Size = new Size(242, 24);
            ed_thinkend.TabIndex = 27;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(257, 36);
            label9.Name = "label9";
            label9.Size = new Size(82, 17);
            label9.TabIndex = 26;
            label9.Text = "Thinking End";
            // 
            // ed_thinkstart
            // 
            ed_thinkstart.Location = new Point(6, 56);
            ed_thinkstart.Name = "ed_thinkstart";
            ed_thinkstart.Size = new Size(242, 24);
            ed_thinkstart.TabIndex = 25;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 36);
            label10.Name = "label10";
            label10.Size = new Size(87, 17);
            label10.TabIndex = 24;
            label10.Text = "Thinking Start";
            // 
            // bt_Save
            // 
            bt_Save.BackColor = Color.DarkSeaGreen;
            bt_Save.FlatStyle = FlatStyle.Flat;
            bt_Save.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_Save.ForeColor = Color.Black;
            bt_Save.Location = new Point(4, 497);
            bt_Save.Name = "bt_Save";
            bt_Save.Size = new Size(277, 23);
            bt_Save.TabIndex = 10;
            bt_Save.Tag = "no-theme";
            bt_Save.Text = "Save Settings";
            bt_Save.UseVisualStyleBackColor = false;
            bt_Save.Click += btSave_Click;
            // 
            // bt_delete
            // 
            bt_delete.BackColor = Color.DarkRed;
            bt_delete.FlatStyle = FlatStyle.Flat;
            bt_delete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_delete.ForeColor = Color.WhiteSmoke;
            bt_delete.Location = new Point(4, 528);
            bt_delete.Name = "bt_delete";
            bt_delete.Size = new Size(277, 23);
            bt_delete.TabIndex = 11;
            bt_delete.Tag = "no-theme";
            bt_delete.Text = "Delete Selected";
            bt_delete.UseVisualStyleBackColor = false;
            bt_delete.Click += bt_delete_Click;
            // 
            // InstructForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1336, 564);
            Controls.Add(collapsibleGroupBox2);
            Controls.Add(collapsibleGroupBox1);
            Controls.Add(groupBox14);
            Controls.Add(verticalStackPanel1);
            ForeColor = Color.WhiteSmoke;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InstructForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Instruction Format Editor";
            KeyDown += InstructForm_KeyDown;
            verticalStackPanel1.ResumeLayout(false);
            verticalStackPanel1.PerformLayout();
            groupBox14.ResumeLayout(false);
            groupBox14.PerformLayout();
            collapsibleGroupBox1.ResumeLayout(false);
            collapsibleGroupBox1.PerformLayout();
            collapsibleGroupBox2.ResumeLayout(false);
            collapsibleGroupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button btSave;
        private TextBox edInstruct;
        private VerticalStackPanel verticalStackPanel1;
        private ModernListBox listInstruct;
        private CollapsibleGroupBox groupBox14;
        private CollapsibleGroupBox collapsibleGroupBox1;
        private TextBox ed_stopsequence;
        private Label label11;
        private TextBox ed_bos;
        private Label label12;
        private ModernCheckBox ck_newlines;
        private TextBox ed_stopstrings;
        private Label label6;
        private TextBox ed_botsuffixoverride;
        private Label label7;
        private TextBox ed_botprefixoverride;
        private Label label8;
        private ModernCheckBox ck_disablinstructstopstrings;
        private CollapsibleGroupBox collapsibleGroupBox2;
        private TextBox ed_thinkend;
        private Label label9;
        private TextBox ed_thinkstart;
        private Label label10;
        private TextBox ed_thinksysprefix;
        private Label label13;
        private ModernCheckBox ck_thinkprefill;
        private TextBox ed_thinksyssuffix;
        private Label label14;
        private TextBox ed_thinkgroup;
        private Label label16;
        private TextBox ed_thinkprefill;
        private Label label15;
        private ModernCheckBox ck_emptythink;
        private TextBox ed_botsuffix;
        private Label label5;
        private TextBox ed_botprefix;
        private Label label17;
        private TextBox ed_usersuffix;
        private Label label3;
        private TextBox ed_userprefix;
        private Label label4;
        private TextBox ed_syssuffix;
        private Label label1;
        private TextBox ed_sysprefix;
        private Label label2;
        private Button bt_Save;
        private Button bt_delete;
    }
}