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
            btRegenBio = new Button();
            label13 = new Label();
            num_selfedittokens = new NumericUpDown();
            ed_selfedit = new TextBox();
            label12 = new Label();
            tabBrain = new TabPage();
            groupBox5 = new GroupBox();
            label21 = new Label();
            listCanDecay = new CheckedListBox();
            label20 = new Label();
            listNoRAGMemTypes = new CheckedListBox();
            label19 = new Label();
            numAgentDelay = new NumericUpDown();
            ckAgent = new CheckBox();
            groupBox3 = new GroupBox();
            listAgentTasks = new CheckedListBox();
            label18 = new Label();
            tabSettings = new TabPage();
            groupBox10 = new GroupBox();
            num_ptvalue = new NumericUpDown();
            cb_pointsystems = new ComboBox();
            label14 = new Label();
            groupBox8 = new GroupBox();
            ed_outetts = new TextBox();
            label11 = new Label();
            groupBox7 = new GroupBox();
            label17 = new Label();
            numKeepEurekas = new NumericUpDown();
            numEurekaMinTime = new NumericUpDown();
            label16 = new Label();
            label15 = new Label();
            numEurekaMinMess = new NumericUpDown();
            label8 = new Label();
            num_minAFK = new NumericUpDown();
            ckAllowEurekas = new CheckBox();
            ckMoodSystem = new CheckBox();
            ckPassword = new CheckBox();
            ck_irldates = new CheckBox();
            label10 = new Label();
            ck_senseoftime = new CheckBox();
            ck_caninitchat = new CheckBox();
            groupBox6 = new GroupBox();
            ckl_plugins = new CheckedListBox();
            label9 = new Label();
            panel1 = new Panel();
            btSave = new Button();
            edFilename = new TextBox();
            cb_charlist = new ListBox();
            panel2 = new Panel();
            tabControl1.SuspendLayout();
            tabBio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            tabAdvanc.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPrompt.SuspendLayout();
            tabDynamic.SuspendLayout();
            groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_selfedittokens).BeginInit();
            tabBrain.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numAgentDelay).BeginInit();
            groupBox3.SuspendLayout();
            tabSettings.SuspendLayout();
            groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_ptvalue).BeginInit();
            groupBox8.SuspendLayout();
            groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numKeepEurekas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numEurekaMinTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numEurekaMinMess).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_minAFK).BeginInit();
            groupBox6.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.Controls.Add(tabBio);
            tabControl1.Controls.Add(tabAdvanc);
            tabControl1.Controls.Add(tabPrompt);
            tabControl1.Controls.Add(tabDynamic);
            tabControl1.Controls.Add(tabBrain);
            tabControl1.Controls.Add(tabSettings);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(953, 517);
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
            tabBio.Location = new Point(4, 27);
            tabBio.Name = "tabBio";
            tabBio.Padding = new Padding(3);
            tabBio.Size = new Size(945, 486);
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
            tabAdvanc.Controls.Add(groupBox4);
            tabAdvanc.Controls.Add(groupBox2);
            tabAdvanc.Controls.Add(groupBox1);
            tabAdvanc.Location = new Point(4, 27);
            tabAdvanc.Name = "tabAdvanc";
            tabAdvanc.Size = new Size(945, 486);
            tabAdvanc.TabIndex = 2;
            tabAdvanc.Text = "Context";
            tabAdvanc.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(ed_writingstyle);
            groupBox4.Controls.Add(label7);
            groupBox4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox4.Location = new Point(8, 247);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(929, 233);
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
            ed_writingstyle.Size = new Size(917, 183);
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
            tabPrompt.Location = new Point(4, 27);
            tabPrompt.Name = "tabPrompt";
            tabPrompt.Padding = new Padding(3);
            tabPrompt.Size = new Size(945, 486);
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
            tabDynamic.Location = new Point(4, 27);
            tabDynamic.Name = "tabDynamic";
            tabDynamic.Padding = new Padding(3);
            tabDynamic.Size = new Size(945, 486);
            tabDynamic.TabIndex = 4;
            tabDynamic.Text = "Dynamic Bio";
            tabDynamic.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(btRegenBio);
            groupBox9.Controls.Add(label13);
            groupBox9.Controls.Add(num_selfedittokens);
            groupBox9.Controls.Add(ed_selfedit);
            groupBox9.Controls.Add(label12);
            groupBox9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox9.Location = new Point(8, 8);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(929, 470);
            groupBox9.TabIndex = 11;
            groupBox9.TabStop = false;
            groupBox9.Text = "Character's Journal";
            // 
            // btRegenBio
            // 
            btRegenBio.Font = new Font("Segoe UI", 9F);
            btRegenBio.Location = new Point(733, 84);
            btRegenBio.Name = "btRegenBio";
            btRegenBio.Size = new Size(190, 23);
            btRegenBio.TabIndex = 16;
            btRegenBio.Text = "Regenerate";
            btRegenBio.UseVisualStyleBackColor = true;
            btRegenBio.Click += btRegenBio_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F);
            label13.Location = new Point(733, 37);
            label13.Name = "label13";
            label13.Size = new Size(170, 15);
            label13.TabIndex = 15;
            label13.Text = "Maximum tokens (0 to disable)";
            // 
            // num_selfedittokens
            // 
            num_selfedittokens.Font = new Font("Segoe UI", 9F);
            num_selfedittokens.Increment = new decimal(new int[] { 128, 0, 0, 0 });
            num_selfedittokens.Location = new Point(733, 55);
            num_selfedittokens.Maximum = new decimal(new int[] { 2048, 0, 0, 0 });
            num_selfedittokens.Name = "num_selfedittokens";
            num_selfedittokens.Size = new Size(190, 23);
            num_selfedittokens.TabIndex = 14;
            // 
            // ed_selfedit
            // 
            ed_selfedit.BorderStyle = BorderStyle.FixedSingle;
            ed_selfedit.Font = new Font("Segoe UI", 9F);
            ed_selfedit.Location = new Point(6, 37);
            ed_selfedit.Multiline = true;
            ed_selfedit.Name = "ed_selfedit";
            ed_selfedit.ReadOnly = true;
            ed_selfedit.ScrollBars = ScrollBars.Vertical;
            ed_selfedit.Size = new Size(721, 427);
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
            // tabBrain
            // 
            tabBrain.Controls.Add(groupBox5);
            tabBrain.Controls.Add(groupBox3);
            tabBrain.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tabBrain.Location = new Point(4, 27);
            tabBrain.Name = "tabBrain";
            tabBrain.Padding = new Padding(3);
            tabBrain.Size = new Size(945, 486);
            tabBrain.TabIndex = 5;
            tabBrain.Text = "Agentic System";
            tabBrain.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label21);
            groupBox5.Controls.Add(listCanDecay);
            groupBox5.Controls.Add(label20);
            groupBox5.Controls.Add(listNoRAGMemTypes);
            groupBox5.Controls.Add(label19);
            groupBox5.Controls.Add(numAgentDelay);
            groupBox5.Controls.Add(ckAgent);
            groupBox5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox5.Location = new Point(476, 6);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(461, 472);
            groupBox5.TabIndex = 12;
            groupBox5.TabStop = false;
            groupBox5.Text = "Agentic Settings";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label21.Location = new Point(6, 289);
            label21.Name = "label21";
            label21.Size = new Size(183, 15);
            label21.TabIndex = 16;
            label21.Text = "Memory types allowed to decay";
            // 
            // listCanDecay
            // 
            listCanDecay.Font = new Font("Segoe UI", 9F);
            listCanDecay.FormattingEnabled = true;
            listCanDecay.Items.AddRange(new object[] { "Assistant", "General" });
            listCanDecay.Location = new Point(6, 307);
            listCanDecay.Name = "listCanDecay";
            listCanDecay.ScrollAlwaysVisible = true;
            listCanDecay.Size = new Size(446, 148);
            listCanDecay.TabIndex = 15;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label20.Location = new Point(6, 120);
            label20.Name = "label20";
            label20.Size = new Size(243, 15);
            label20.TabIndex = 14;
            label20.Text = "Forbid Memory types from going into RAG";
            // 
            // listNoRAGMemTypes
            // 
            listNoRAGMemTypes.Font = new Font("Segoe UI", 9F);
            listNoRAGMemTypes.FormattingEnabled = true;
            listNoRAGMemTypes.Items.AddRange(new object[] { "Assistant", "General" });
            listNoRAGMemTypes.Location = new Point(6, 138);
            listNoRAGMemTypes.Name = "listNoRAGMemTypes";
            listNoRAGMemTypes.ScrollAlwaysVisible = true;
            listNoRAGMemTypes.Size = new Size(446, 148);
            listNoRAGMemTypes.TabIndex = 13;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 9F);
            label19.Location = new Point(124, 49);
            label19.Name = "label19";
            label19.Size = new Size(174, 15);
            label19.TabIndex = 12;
            label19.Text = "Hours AFK before running tasks";
            // 
            // numAgentDelay
            // 
            numAgentDelay.DecimalPlaces = 2;
            numAgentDelay.Font = new Font("Segoe UI", 9F);
            numAgentDelay.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            numAgentDelay.Location = new Point(24, 47);
            numAgentDelay.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numAgentDelay.Minimum = new decimal(new int[] { 25, 0, 0, 131072 });
            numAgentDelay.Name = "numAgentDelay";
            numAgentDelay.Size = new Size(94, 23);
            numAgentDelay.TabIndex = 11;
            numAgentDelay.Value = new decimal(new int[] { 125, 0, 0, 131072 });
            // 
            // ckAgent
            // 
            ckAgent.AutoSize = true;
            ckAgent.Font = new Font("Segoe UI", 9F);
            ckAgent.Location = new Point(6, 22);
            ckAgent.Name = "ckAgent";
            ckAgent.Size = new Size(226, 19);
            ckAgent.TabIndex = 3;
            ckAgent.Text = "Allow the character to run as an agent";
            ckAgent.UseVisualStyleBackColor = true;
            ckAgent.CheckedChanged += ckAgent_CheckedChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(listAgentTasks);
            groupBox3.Controls.Add(label18);
            groupBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox3.Location = new Point(6, 6);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(455, 472);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "Allowed Tasks";
            // 
            // listAgentTasks
            // 
            listAgentTasks.Font = new Font("Segoe UI", 9F);
            listAgentTasks.FormattingEnabled = true;
            listAgentTasks.Items.AddRange(new object[] { "Assistant", "General" });
            listAgentTasks.Location = new Point(6, 37);
            listAgentTasks.Name = "listAgentTasks";
            listAgentTasks.ScrollAlwaysVisible = true;
            listAgentTasks.Size = new Size(443, 418);
            listAgentTasks.TabIndex = 1;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 9F);
            label18.Location = new Point(6, 19);
            label18.Name = "label18";
            label18.Size = new Size(351, 15);
            label18.TabIndex = 0;
            label18.Text = "Select the tasks this character is allowed to run in the background";
            // 
            // tabSettings
            // 
            tabSettings.Controls.Add(groupBox10);
            tabSettings.Controls.Add(groupBox8);
            tabSettings.Controls.Add(groupBox7);
            tabSettings.Controls.Add(groupBox6);
            tabSettings.Location = new Point(4, 27);
            tabSettings.Name = "tabSettings";
            tabSettings.Size = new Size(945, 486);
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
            groupBox10.Location = new Point(382, 295);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(549, 78);
            groupBox10.TabIndex = 12;
            groupBox10.TabStop = false;
            groupBox10.Text = "Gold / Point System (optional)";
            // 
            // num_ptvalue
            // 
            num_ptvalue.Font = new Font("Segoe UI", 9F);
            num_ptvalue.Location = new Point(423, 38);
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
            cb_pointsystems.Size = new Size(411, 23);
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
            groupBox8.Location = new Point(382, 217);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(555, 72);
            groupBox8.TabIndex = 11;
            groupBox8.TabStop = false;
            groupBox8.Text = "Text To Speech (KoboldCpp API)";
            // 
            // ed_outetts
            // 
            ed_outetts.Font = new Font("Segoe UI", 9F);
            ed_outetts.Location = new Point(6, 37);
            ed_outetts.Name = "ed_outetts";
            ed_outetts.Size = new Size(411, 23);
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
            groupBox7.Controls.Add(label17);
            groupBox7.Controls.Add(numKeepEurekas);
            groupBox7.Controls.Add(numEurekaMinTime);
            groupBox7.Controls.Add(label16);
            groupBox7.Controls.Add(label15);
            groupBox7.Controls.Add(numEurekaMinMess);
            groupBox7.Controls.Add(label8);
            groupBox7.Controls.Add(num_minAFK);
            groupBox7.Controls.Add(ckAllowEurekas);
            groupBox7.Controls.Add(ckMoodSystem);
            groupBox7.Controls.Add(ckPassword);
            groupBox7.Controls.Add(ck_irldates);
            groupBox7.Controls.Add(label10);
            groupBox7.Controls.Add(ck_senseoftime);
            groupBox7.Controls.Add(ck_caninitchat);
            groupBox7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox7.Location = new Point(3, 3);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(373, 475);
            groupBox7.TabIndex = 10;
            groupBox7.TabStop = false;
            groupBox7.Text = "General Settings";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 9F);
            label17.Location = new Point(126, 200);
            label17.Name = "label17";
            label17.Size = new Size(186, 15);
            label17.TabIndex = 17;
            label17.Text = "Keep Natural memories for X days";
            // 
            // numKeepEurekas
            // 
            numKeepEurekas.Font = new Font("Segoe UI", 9F);
            numKeepEurekas.Location = new Point(26, 198);
            numKeepEurekas.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numKeepEurekas.Name = "numKeepEurekas";
            numKeepEurekas.Size = new Size(94, 23);
            numKeepEurekas.TabIndex = 16;
            numKeepEurekas.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numEurekaMinTime
            // 
            numEurekaMinTime.DecimalPlaces = 2;
            numEurekaMinTime.Font = new Font("Segoe UI", 9F);
            numEurekaMinTime.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            numEurekaMinTime.Location = new Point(26, 169);
            numEurekaMinTime.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numEurekaMinTime.Name = "numEurekaMinTime";
            numEurekaMinTime.Size = new Size(94, 23);
            numEurekaMinTime.TabIndex = 15;
            numEurekaMinTime.Value = new decimal(new int[] { 125, 0, 0, 131072 });
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9F);
            label16.Location = new Point(126, 171);
            label16.Name = "label16";
            label16.Size = new Size(234, 15);
            label16.TabIndex = 14;
            label16.Text = "Min time spent between memories (Hours)";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F);
            label15.Location = new Point(126, 142);
            label15.Name = "label15";
            label15.Size = new Size(186, 15);
            label15.TabIndex = 12;
            label15.Text = "Min messages between memories";
            // 
            // numEurekaMinMess
            // 
            numEurekaMinMess.Font = new Font("Segoe UI", 9F);
            numEurekaMinMess.Location = new Point(26, 140);
            numEurekaMinMess.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numEurekaMinMess.Name = "numEurekaMinMess";
            numEurekaMinMess.Size = new Size(94, 23);
            numEurekaMinMess.TabIndex = 11;
            numEurekaMinMess.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F);
            label8.Location = new Point(126, 254);
            label8.Name = "label8";
            label8.Size = new Size(159, 15);
            label8.TabIndex = 10;
            label8.Text = "Hours AFK before time insert";
            // 
            // num_minAFK
            // 
            num_minAFK.DecimalPlaces = 2;
            num_minAFK.Font = new Font("Segoe UI", 9F);
            num_minAFK.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            num_minAFK.Location = new Point(26, 252);
            num_minAFK.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            num_minAFK.Minimum = new decimal(new int[] { 25, 0, 0, 131072 });
            num_minAFK.Name = "num_minAFK";
            num_minAFK.Size = new Size(94, 23);
            num_minAFK.TabIndex = 9;
            num_minAFK.Value = new decimal(new int[] { 125, 0, 0, 131072 });
            // 
            // ckAllowEurekas
            // 
            ckAllowEurekas.AutoSize = true;
            ckAllowEurekas.Font = new Font("Segoe UI", 9F);
            ckAllowEurekas.Location = new Point(6, 115);
            ckAllowEurekas.Name = "ckAllowEurekas";
            ckAllowEurekas.Size = new Size(150, 19);
            ckAllowEurekas.TabIndex = 8;
            ckAllowEurekas.Text = "Natural memory inserts";
            ckAllowEurekas.UseVisualStyleBackColor = true;
            // 
            // ckMoodSystem
            // 
            ckMoodSystem.AutoSize = true;
            ckMoodSystem.Font = new Font("Segoe UI", 9F);
            ckMoodSystem.Location = new Point(6, 65);
            ckMoodSystem.Name = "ckMoodSystem";
            ckMoodSystem.Size = new Size(98, 19);
            ckMoodSystem.TabIndex = 7;
            ckMoodSystem.Text = "Mood system";
            ckMoodSystem.UseVisualStyleBackColor = true;
            ckMoodSystem.CheckedChanged += ckMoodSystem_CheckedChanged;
            // 
            // ckPassword
            // 
            ckPassword.AutoSize = true;
            ckPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            ckPassword.ForeColor = Color.DarkRed;
            ckPassword.Location = new Point(6, 281);
            ckPassword.Name = "ckPassword";
            ckPassword.Size = new Size(140, 19);
            ckPassword.TabIndex = 6;
            ckPassword.Text = "Password protection";
            ckPassword.UseVisualStyleBackColor = true;
            ckPassword.CheckedChanged += ckPassword_CheckedChanged;
            // 
            // ck_irldates
            // 
            ck_irldates.AutoSize = true;
            ck_irldates.Font = new Font("Segoe UI", 9F);
            ck_irldates.Location = new Point(6, 90);
            ck_irldates.Name = "ck_irldates";
            ck_irldates.Size = new Size(230, 19);
            ck_irldates.TabIndex = 5;
            ck_irldates.Text = "Include IRL dates in session summaries";
            ck_irldates.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F);
            label10.Location = new Point(6, 19);
            label10.Name = "label10";
            label10.Size = new Size(291, 15);
            label10.TabIndex = 4;
            label10.Text = "Some options can be toggled on/off in the main chat.";
            // 
            // ck_senseoftime
            // 
            ck_senseoftime.AutoSize = true;
            ck_senseoftime.Font = new Font("Segoe UI", 9F);
            ck_senseoftime.Location = new Point(6, 227);
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
            ck_caninitchat.Location = new Point(6, 40);
            ck_caninitchat.Name = "ck_caninitchat";
            ck_caninitchat.Size = new Size(279, 19);
            ck_caninitchat.TabIndex = 2;
            ck_caninitchat.Text = "Character can initiate chat (when the user is afk)";
            ck_caninitchat.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(ckl_plugins);
            groupBox6.Controls.Add(label9);
            groupBox6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox6.Location = new Point(382, 3);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(555, 208);
            groupBox6.TabIndex = 9;
            groupBox6.TabStop = false;
            groupBox6.Text = "Context Plugins";
            // 
            // ckl_plugins
            // 
            ckl_plugins.Font = new Font("Segoe UI", 9F);
            ckl_plugins.FormattingEnabled = true;
            ckl_plugins.Items.AddRange(new object[] { "Assistant", "General" });
            ckl_plugins.Location = new Point(6, 37);
            ckl_plugins.Name = "ckl_plugins";
            ckl_plugins.ScrollAlwaysVisible = true;
            ckl_plugins.Size = new Size(543, 166);
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
            // panel1
            // 
            panel1.Controls.Add(btSave);
            panel1.Controls.Add(edFilename);
            panel1.Controls.Add(cb_charlist);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(234, 517);
            panel1.TabIndex = 4;
            // 
            // btSave
            // 
            btSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btSave.Location = new Point(3, 484);
            btSave.Name = "btSave";
            btSave.Size = new Size(228, 23);
            btSave.TabIndex = 3;
            btSave.Text = "Save Character";
            btSave.UseVisualStyleBackColor = true;
            btSave.Click += bt_worldsave_Click;
            // 
            // edFilename
            // 
            edFilename.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            edFilename.Location = new Point(3, 455);
            edFilename.Name = "edFilename";
            edFilename.Size = new Size(228, 23);
            edFilename.TabIndex = 2;
            // 
            // cb_charlist
            // 
            cb_charlist.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cb_charlist.BorderStyle = BorderStyle.FixedSingle;
            cb_charlist.FormattingEnabled = true;
            cb_charlist.Location = new Point(3, 3);
            cb_charlist.Name = "cb_charlist";
            cb_charlist.ScrollAlwaysVisible = true;
            cb_charlist.Size = new Size(228, 437);
            cb_charlist.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Controls.Add(tabControl1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(234, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(953, 517);
            panel2.TabIndex = 5;
            // 
            // CharEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1187, 517);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CharEditForm";
            Text = "Character Editor";
            tabControl1.ResumeLayout(false);
            tabBio.ResumeLayout(false);
            tabBio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            tabAdvanc.ResumeLayout(false);
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
            tabBrain.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numAgentDelay).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            tabSettings.ResumeLayout(false);
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_ptvalue).EndInit();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numKeepEurekas).EndInit();
            ((System.ComponentModel.ISupportInitialize)numEurekaMinTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)numEurekaMinMess).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_minAFK).EndInit();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
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
        private GroupBox groupBox4;
        private TextBox ed_writingstyle;
        private Label label7;
        private GroupBox groupBox6;
        private CheckedListBox ckl_plugins;
        private Label label9;
        private GroupBox groupBox7;
        private CheckBox ck_senseoftime;
        private CheckBox ck_caninitchat;
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
        private Button btRegenBio;
        private CheckBox ck_irldates;
        private CheckBox ckPassword;
        private CheckBox ckMoodSystem;
        private Panel panel1;
        private Button btSave;
        private TextBox edFilename;
        private ListBox cb_charlist;
        private TabPage tabBrain;
        private Panel panel2;
        private CheckBox ckAllowEurekas;
        private Label label8;
        private NumericUpDown num_minAFK;
        private NumericUpDown numEurekaMinTime;
        private Label label16;
        private Label label15;
        private NumericUpDown numEurekaMinMess;
        private Label label17;
        private NumericUpDown numKeepEurekas;
        private GroupBox groupBox5;
        private GroupBox groupBox3;
        private CheckedListBox listAgentTasks;
        private Label label18;
        private CheckBox ckAgent;
        private Label label19;
        private NumericUpDown numAgentDelay;
        private Label label20;
        private CheckedListBox listNoRAGMemTypes;
        private Label label21;
        private CheckedListBox listCanDecay;
    }
}