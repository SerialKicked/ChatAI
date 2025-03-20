namespace WaifuAI.src.forms
{
    partial class CharEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharEditForm));
            groupBox3 = new GroupBox();
            bt_worldsave = new Button();
            cb_charlist = new ComboBox();
            tabControl1 = new TabControl();
            tabBio = new TabPage();
            pic = new PictureBox();
            cb_icon = new ComboBox();
            label4 = new Label();
            ed_firstmessage = new TextBox();
            ed_scenario = new TextBox();
            label3 = new Label();
            ed_bio = new TextBox();
            label2 = new Label();
            ck_isuser = new CheckBox();
            ed_name = new TextBox();
            label1 = new Label();
            tabAdvanc = new TabPage();
            groupBox5 = new GroupBox();
            ed_whims = new TextBox();
            label8 = new Label();
            groupBox4 = new GroupBox();
            ed_writingstyle = new TextBox();
            label7 = new Label();
            groupBox2 = new GroupBox();
            ckl_samplers = new CheckedListBox();
            label5 = new Label();
            groupBox1 = new GroupBox();
            ckl_worldinfo = new CheckedListBox();
            label6 = new Label();
            tabPrompt = new TabPage();
            ed_sysprompt = new TextBox();
            textBox2 = new TextBox();
            tabDynamic = new TabPage();
            groupBox9 = new GroupBox();
            label13 = new Label();
            num_selfedittokens = new NumericUpDown();
            ed_selfedit = new TextBox();
            label12 = new Label();
            tabSettings = new TabPage();
            groupBox10 = new GroupBox();
            num_ptvalue = new NumericUpDown();
            cb_pointsystems = new ComboBox();
            label14 = new Label();
            groupBox8 = new GroupBox();
            ed_outetts = new TextBox();
            label11 = new Label();
            groupBox7 = new GroupBox();
            label10 = new Label();
            ck_senseoftime = new CheckBox();
            ck_caninitchat = new CheckBox();
            ck_sessionmemory = new CheckBox();
            groupBox6 = new GroupBox();
            ckl_plugins = new CheckedListBox();
            label9 = new Label();
            groupBox3.SuspendLayout();
            tabControl1.SuspendLayout();
            tabBio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            tabAdvanc.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPrompt.SuspendLayout();
            tabDynamic.SuspendLayout();
            groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_selfedittokens).BeginInit();
            tabSettings.SuspendLayout();
            groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_ptvalue).BeginInit();
            groupBox8.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox6.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(bt_worldsave);
            groupBox3.Controls.Add(cb_charlist);
            groupBox3.Location = new Point(12, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(10);
            groupBox3.Size = new Size(951, 57);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "Character Selection";
            // 
            // bt_worldsave
            // 
            bt_worldsave.Location = new Point(371, 20);
            bt_worldsave.Name = "bt_worldsave";
            bt_worldsave.Size = new Size(110, 23);
            bt_worldsave.TabIndex = 1;
            bt_worldsave.Text = "Save";
            bt_worldsave.UseVisualStyleBackColor = true;
            bt_worldsave.Click += bt_worldsave_Click;
            // 
            // cb_charlist
            // 
            cb_charlist.FormattingEnabled = true;
            cb_charlist.Location = new Point(12, 21);
            cb_charlist.Name = "cb_charlist";
            cb_charlist.Size = new Size(353, 23);
            cb_charlist.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabBio);
            tabControl1.Controls.Add(tabAdvanc);
            tabControl1.Controls.Add(tabPrompt);
            tabControl1.Controls.Add(tabDynamic);
            tabControl1.Controls.Add(tabSettings);
            tabControl1.Location = new Point(12, 75);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(951, 512);
            tabControl1.TabIndex = 3;
            // 
            // tabBio
            // 
            tabBio.Controls.Add(pic);
            tabBio.Controls.Add(cb_icon);
            tabBio.Controls.Add(label4);
            tabBio.Controls.Add(ed_firstmessage);
            tabBio.Controls.Add(ed_scenario);
            tabBio.Controls.Add(label3);
            tabBio.Controls.Add(ed_bio);
            tabBio.Controls.Add(label2);
            tabBio.Controls.Add(ck_isuser);
            tabBio.Controls.Add(ed_name);
            tabBio.Controls.Add(label1);
            tabBio.Location = new Point(4, 24);
            tabBio.Name = "tabBio";
            tabBio.Padding = new Padding(3);
            tabBio.Size = new Size(943, 484);
            tabBio.TabIndex = 0;
            tabBio.Text = "Biography";
            tabBio.UseVisualStyleBackColor = true;
            // 
            // pic
            // 
            pic.Location = new Point(672, 6);
            pic.Name = "pic";
            pic.Size = new Size(54, 50);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 16;
            pic.TabStop = false;
            // 
            // cb_icon
            // 
            cb_icon.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_icon.FormattingEnabled = true;
            cb_icon.Location = new Point(732, 19);
            cb_icon.Name = "cb_icon";
            cb_icon.Size = new Size(205, 23);
            cb_icon.TabIndex = 15;
            cb_icon.SelectedIndexChanged += cb_icon_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(482, 285);
            label4.Name = "label4";
            label4.Size = new Size(313, 15);
            label4.TabIndex = 14;
            label4.Text = "Initial Bot Message (one per line, use \\n for paragraphs)";
            // 
            // ed_firstmessage
            // 
            ed_firstmessage.Location = new Point(482, 303);
            ed_firstmessage.Multiline = true;
            ed_firstmessage.Name = "ed_firstmessage";
            ed_firstmessage.ScrollBars = ScrollBars.Vertical;
            ed_firstmessage.Size = new Size(455, 175);
            ed_firstmessage.TabIndex = 13;
            // 
            // ed_scenario
            // 
            ed_scenario.Location = new Point(6, 303);
            ed_scenario.Multiline = true;
            ed_scenario.Name = "ed_scenario";
            ed_scenario.ScrollBars = ScrollBars.Vertical;
            ed_scenario.Size = new Size(455, 175);
            ed_scenario.TabIndex = 12;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(6, 285);
            label3.Name = "label3";
            label3.Size = new Size(100, 15);
            label3.TabIndex = 11;
            label3.Text = "Default Scenario";
            // 
            // ed_bio
            // 
            ed_bio.Location = new Point(6, 65);
            ed_bio.Multiline = true;
            ed_bio.Name = "ed_bio";
            ed_bio.ScrollBars = ScrollBars.Vertical;
            ed_bio.Size = new Size(931, 217);
            ed_bio.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(6, 47);
            label2.Name = "label2";
            label2.Size = new Size(138, 15);
            label2.TabIndex = 9;
            label2.Text = "Biography / Description";
            // 
            // ck_isuser
            // 
            ck_isuser.AutoSize = true;
            ck_isuser.Location = new Point(367, 23);
            ck_isuser.Name = "ck_isuser";
            ck_isuser.Size = new Size(94, 19);
            ck_isuser.TabIndex = 8;
            ck_isuser.Text = "User Persona";
            ck_isuser.UseVisualStyleBackColor = true;
            // 
            // ed_name
            // 
            ed_name.Location = new Point(6, 21);
            ed_name.Name = "ed_name";
            ed_name.Size = new Size(355, 23);
            ed_name.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(6, 3);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 6;
            label1.Text = "Name";
            // 
            // tabAdvanc
            // 
            tabAdvanc.Controls.Add(groupBox5);
            tabAdvanc.Controls.Add(groupBox4);
            tabAdvanc.Controls.Add(groupBox2);
            tabAdvanc.Controls.Add(groupBox1);
            tabAdvanc.Location = new Point(4, 24);
            tabAdvanc.Name = "tabAdvanc";
            tabAdvanc.Size = new Size(943, 484);
            tabAdvanc.TabIndex = 2;
            tabAdvanc.Text = "Advanced";
            tabAdvanc.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(ed_whims);
            groupBox5.Controls.Add(label8);
            groupBox5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox5.Location = new Point(482, 247);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(455, 233);
            groupBox5.TabIndex = 11;
            groupBox5.TabStop = false;
            groupBox5.Text = "Whim System";
            // 
            // ed_whims
            // 
            ed_whims.Enabled = false;
            ed_whims.Font = new Font("Segoe UI", 9F);
            ed_whims.Location = new Point(6, 37);
            ed_whims.Multiline = true;
            ed_whims.Name = "ed_whims";
            ed_whims.ScrollBars = ScrollBars.Vertical;
            ed_whims.Size = new Size(443, 183);
            ed_whims.TabIndex = 13;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F);
            label8.Location = new Point(6, 19);
            label8.Name = "label8";
            label8.Size = new Size(96, 15);
            label8.TabIndex = 0;
            label8.Text = "Work In Progress";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(ed_writingstyle);
            groupBox4.Controls.Add(label7);
            groupBox4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox4.Location = new Point(8, 247);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(455, 233);
            groupBox4.TabIndex = 10;
            groupBox4.TabStop = false;
            groupBox4.Text = "Writing Style";
            // 
            // ed_writingstyle
            // 
            ed_writingstyle.Font = new Font("Segoe UI", 9F);
            ed_writingstyle.Location = new Point(6, 37);
            ed_writingstyle.Multiline = true;
            ed_writingstyle.Name = "ed_writingstyle";
            ed_writingstyle.ScrollBars = ScrollBars.Vertical;
            ed_writingstyle.Size = new Size(443, 183);
            ed_writingstyle.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F);
            label7.Location = new Point(6, 19);
            label7.Name = "label7";
            label7.Size = new Size(327, 15);
            label7.TabIndex = 0;
            label7.Text = "Optional list of writing style instructions (or dialog examples)";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ckl_samplers);
            groupBox2.Controls.Add(label5);
            groupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox2.Location = new Point(482, 8);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(455, 233);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Favorite Samplers";
            // 
            // ckl_samplers
            // 
            ckl_samplers.Font = new Font("Segoe UI", 9F);
            ckl_samplers.FormattingEnabled = true;
            ckl_samplers.Items.AddRange(new object[] { "Assistant", "General" });
            ckl_samplers.Location = new Point(6, 37);
            ckl_samplers.Name = "ckl_samplers";
            ckl_samplers.ScrollAlwaysVisible = true;
            ckl_samplers.Size = new Size(443, 184);
            ckl_samplers.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F);
            label5.Location = new Point(6, 19);
            label5.Name = "label5";
            label5.Size = new Size(331, 15);
            label5.TabIndex = 0;
            label5.Text = "Select favorite samplers for random sampler mode. (optional)";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ckl_worldinfo);
            groupBox1.Controls.Add(label6);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.Location = new Point(8, 8);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(455, 233);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "World Info";
            // 
            // ckl_worldinfo
            // 
            ckl_worldinfo.Font = new Font("Segoe UI", 9F);
            ckl_worldinfo.FormattingEnabled = true;
            ckl_worldinfo.Items.AddRange(new object[] { "Assistant", "General" });
            ckl_worldinfo.Location = new Point(6, 37);
            ckl_worldinfo.Name = "ckl_worldinfo";
            ckl_worldinfo.ScrollAlwaysVisible = true;
            ckl_worldinfo.Size = new Size(443, 184);
            ckl_worldinfo.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F);
            label6.Location = new Point(6, 19);
            label6.Name = "label6";
            label6.Size = new Size(361, 15);
            label6.TabIndex = 0;
            label6.Text = "Select the world info files this character is allowed to use. (optional)";
            // 
            // tabPrompt
            // 
            tabPrompt.Controls.Add(ed_sysprompt);
            tabPrompt.Controls.Add(textBox2);
            tabPrompt.Location = new Point(4, 24);
            tabPrompt.Name = "tabPrompt";
            tabPrompt.Padding = new Padding(3);
            tabPrompt.Size = new Size(943, 484);
            tabPrompt.TabIndex = 1;
            tabPrompt.Text = "System Prompt";
            tabPrompt.UseVisualStyleBackColor = true;
            // 
            // ed_sysprompt
            // 
            ed_sysprompt.BorderStyle = BorderStyle.FixedSingle;
            ed_sysprompt.Location = new Point(6, 151);
            ed_sysprompt.Multiline = true;
            ed_sysprompt.Name = "ed_sysprompt";
            ed_sysprompt.PlaceholderText = "Optional System Prompt";
            ed_sysprompt.ScrollBars = ScrollBars.Vertical;
            ed_sysprompt.Size = new Size(931, 327);
            ed_sysprompt.TabIndex = 13;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.Info;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Location = new Point(6, 6);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(931, 139);
            textBox2.TabIndex = 1;
            textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // tabDynamic
            // 
            tabDynamic.Controls.Add(groupBox9);
            tabDynamic.Location = new Point(4, 24);
            tabDynamic.Name = "tabDynamic";
            tabDynamic.Padding = new Padding(3);
            tabDynamic.Size = new Size(943, 484);
            tabDynamic.TabIndex = 4;
            tabDynamic.Text = "Dynamic Bio";
            tabDynamic.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(label13);
            groupBox9.Controls.Add(num_selfedittokens);
            groupBox9.Controls.Add(ed_selfedit);
            groupBox9.Controls.Add(label12);
            groupBox9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox9.Location = new Point(8, 8);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(929, 233);
            groupBox9.TabIndex = 11;
            groupBox9.TabStop = false;
            groupBox9.Text = "Self Edit Field";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(455, 40);
            label13.Name = "label13";
            label13.Size = new Size(223, 15);
            label13.TabIndex = 15;
            label13.Text = "Maximum tokens (0 to disable feature)";
            // 
            // num_selfedittokens
            // 
            num_selfedittokens.Font = new Font("Segoe UI", 9F);
            num_selfedittokens.Increment = new decimal(new int[] { 128, 0, 0, 0 });
            num_selfedittokens.Location = new Point(455, 58);
            num_selfedittokens.Maximum = new decimal(new int[] { 2048, 0, 0, 0 });
            num_selfedittokens.Name = "num_selfedittokens";
            num_selfedittokens.Size = new Size(125, 23);
            num_selfedittokens.TabIndex = 14;
            // 
            // ed_selfedit
            // 
            ed_selfedit.Font = new Font("Segoe UI", 9F);
            ed_selfedit.Location = new Point(6, 37);
            ed_selfedit.Multiline = true;
            ed_selfedit.Name = "ed_selfedit";
            ed_selfedit.ReadOnly = true;
            ed_selfedit.ScrollBars = ScrollBars.Vertical;
            ed_selfedit.Size = new Size(443, 183);
            ed_selfedit.TabIndex = 13;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F);
            label12.Location = new Point(6, 19);
            label12.Name = "label12";
            label12.Size = new Size(551, 15);
            label12.TabIndex = 0;
            label12.Text = "This is an optional field the character is allowed to edit on its own as new sessions are added to the chat";
            // 
            // tabSettings
            // 
            tabSettings.Controls.Add(groupBox10);
            tabSettings.Controls.Add(groupBox8);
            tabSettings.Controls.Add(groupBox7);
            tabSettings.Controls.Add(groupBox6);
            tabSettings.Location = new Point(4, 24);
            tabSettings.Name = "tabSettings";
            tabSettings.Size = new Size(943, 484);
            tabSettings.TabIndex = 3;
            tabSettings.Text = "Settings";
            tabSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(num_ptvalue);
            groupBox10.Controls.Add(cb_pointsystems);
            groupBox10.Controls.Add(label14);
            groupBox10.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox10.Location = new Point(8, 222);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(455, 97);
            groupBox10.TabIndex = 12;
            groupBox10.TabStop = false;
            groupBox10.Text = "Gold / Point System (optional)";
            // 
            // num_ptvalue
            // 
            num_ptvalue.Font = new Font("Segoe UI", 9F);
            num_ptvalue.Location = new Point(329, 63);
            num_ptvalue.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            num_ptvalue.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            num_ptvalue.Name = "num_ptvalue";
            num_ptvalue.Size = new Size(120, 23);
            num_ptvalue.TabIndex = 4;
            // 
            // cb_pointsystems
            // 
            cb_pointsystems.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_pointsystems.FormattingEnabled = true;
            cb_pointsystems.Location = new Point(6, 37);
            cb_pointsystems.Name = "cb_pointsystems";
            cb_pointsystems.Size = new Size(443, 23);
            cb_pointsystems.TabIndex = 3;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F);
            label14.Location = new Point(6, 19);
            label14.Name = "label14";
            label14.Size = new Size(356, 15);
            label14.TabIndex = 1;
            label14.Text = "Select the point system this character will use (/pointhelp for help)";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(ed_outetts);
            groupBox8.Controls.Add(label11);
            groupBox8.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox8.Location = new Point(482, 247);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(455, 72);
            groupBox8.TabIndex = 11;
            groupBox8.TabStop = false;
            groupBox8.Text = "Text To Speech (KoboldCpp API)";
            // 
            // ed_outetts
            // 
            ed_outetts.Font = new Font("Segoe UI", 9F);
            ed_outetts.Location = new Point(6, 37);
            ed_outetts.Name = "ed_outetts";
            ed_outetts.Size = new Size(443, 23);
            ed_outetts.TabIndex = 2;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F);
            label11.Location = new Point(6, 19);
            label11.Name = "label11";
            label11.Size = new Size(244, 15);
            label11.TabIndex = 1;
            label11.Text = "Voice ID for KoboldCPP's Text-To-Speech API";
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(label10);
            groupBox7.Controls.Add(ck_senseoftime);
            groupBox7.Controls.Add(ck_caninitchat);
            groupBox7.Controls.Add(ck_sessionmemory);
            groupBox7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox7.Location = new Point(482, 8);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(455, 233);
            groupBox7.TabIndex = 10;
            groupBox7.TabStop = false;
            groupBox7.Text = "Default Character Settings";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F);
            label10.Location = new Point(6, 19);
            label10.Name = "label10";
            label10.Size = new Size(305, 15);
            label10.TabIndex = 4;
            label10.Text = "Those options can be toggled on or off in the main chat.";
            // 
            // ck_senseoftime
            // 
            ck_senseoftime.AutoSize = true;
            ck_senseoftime.Font = new Font("Segoe UI", 9F);
            ck_senseoftime.Location = new Point(6, 97);
            ck_senseoftime.Name = "ck_senseoftime";
            ck_senseoftime.Size = new Size(290, 19);
            ck_senseoftime.TabIndex = 3;
            ck_senseoftime.Text = "Sense of time (insert time relevant info to prompt)";
            ck_senseoftime.UseVisualStyleBackColor = true;
            // 
            // ck_caninitchat
            // 
            ck_caninitchat.AutoSize = true;
            ck_caninitchat.Font = new Font("Segoe UI", 9F);
            ck_caninitchat.Location = new Point(6, 72);
            ck_caninitchat.Name = "ck_caninitchat";
            ck_caninitchat.Size = new Size(279, 19);
            ck_caninitchat.TabIndex = 2;
            ck_caninitchat.Text = "Character can initiate chat (when the user is afk)";
            ck_caninitchat.UseVisualStyleBackColor = true;
            // 
            // ck_sessionmemory
            // 
            ck_sessionmemory.AutoSize = true;
            ck_sessionmemory.Font = new Font("Segoe UI", 9F);
            ck_sessionmemory.Location = new Point(6, 47);
            ck_sessionmemory.Name = "ck_sessionmemory";
            ck_sessionmemory.Size = new Size(355, 19);
            ck_sessionmemory.TabIndex = 1;
            ck_sessionmemory.Text = "Session Memory (insert summaries of past sessions in prompt)";
            ck_sessionmemory.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(ckl_plugins);
            groupBox6.Controls.Add(label9);
            groupBox6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox6.Location = new Point(8, 8);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(455, 208);
            groupBox6.TabIndex = 9;
            groupBox6.TabStop = false;
            groupBox6.Text = "Plugins";
            // 
            // ckl_plugins
            // 
            ckl_plugins.Font = new Font("Segoe UI", 9F);
            ckl_plugins.FormattingEnabled = true;
            ckl_plugins.Items.AddRange(new object[] { "Assistant", "General" });
            ckl_plugins.Location = new Point(6, 37);
            ckl_plugins.Name = "ckl_plugins";
            ckl_plugins.ScrollAlwaysVisible = true;
            ckl_plugins.Size = new Size(443, 130);
            ckl_plugins.TabIndex = 1;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F);
            label9.Location = new Point(6, 19);
            label9.Name = "label9";
            label9.Size = new Size(419, 15);
            label9.TabIndex = 0;
            label9.Text = "Plugins this character has access to. Some can be toggled on/off in main chat.";
            // 
            // CharEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(975, 599);
            Controls.Add(tabControl1);
            Controls.Add(groupBox3);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CharEditForm";
            Text = "Character Editor";
            groupBox3.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabBio.ResumeLayout(false);
            tabBio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            tabAdvanc.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPrompt.ResumeLayout(false);
            tabPrompt.PerformLayout();
            tabDynamic.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_selfedittokens).EndInit();
            tabSettings.ResumeLayout(false);
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_ptvalue).EndInit();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox3;
        private Button bt_worldsave;
        private ComboBox cb_charlist;
        private TabControl tabControl1;
        private TabPage tabBio;
        private TabPage tabPrompt;
        private TextBox ed_bio;
        private Label label2;
        private CheckBox ck_isuser;
        private TextBox ed_name;
        private Label label1;
        private TabPage tabAdvanc;
        private TabPage tabSettings;
        private TextBox textBox2;
        private TextBox ed_scenario;
        private Label label3;
        private Label label4;
        private TextBox ed_firstmessage;
        private TextBox ed_sysprompt;
        private GroupBox groupBox1;
        private CheckedListBox ckl_worldinfo;
        private Label label6;
        private GroupBox groupBox2;
        private CheckedListBox ckl_samplers;
        private Label label5;
        private GroupBox groupBox5;
        private Label label8;
        private GroupBox groupBox4;
        private TextBox ed_writingstyle;
        private Label label7;
        private TextBox ed_whims;
        private GroupBox groupBox6;
        private CheckedListBox ckl_plugins;
        private Label label9;
        private GroupBox groupBox7;
        private CheckBox ck_senseoftime;
        private CheckBox ck_caninitchat;
        private CheckBox ck_sessionmemory;
        private Label label10;
        private GroupBox groupBox8;
        private TextBox ed_outetts;
        private Label label11;
        private ComboBox cb_icon;
        private PictureBox pic;
        private TabPage tabDynamic;
        private GroupBox groupBox9;
        private Label label13;
        private NumericUpDown num_selfedittokens;
        private TextBox ed_selfedit;
        private Label label12;
        private GroupBox groupBox10;
        private ComboBox cb_pointsystems;
        private Label label14;
        private NumericUpDown num_ptvalue;
    }
}