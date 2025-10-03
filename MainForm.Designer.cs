using LLama.Common;

namespace WaifuAI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            HelptoolTip = new ToolTip(components);
            statusbar = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            AutoTalkTimer = new System.Windows.Forms.Timer(components);
            panRight = new Panel();
            collapsibleGroupBox1 = new WaifuAI.Controls.CollapsibleGroupBox();
            modernCheckBox1 = new WaifuAI.Controls.ModernCheckBox();
            button1 = new Button();
            groupBox5 = new GroupBox();
            btChatHistory = new Button();
            btSysPrompt = new Button();
            cb_user = new ComboBox();
            label4 = new Label();
            bt_editchar = new Button();
            bt_scenario = new Button();
            label3 = new Label();
            cb_bot = new ComboBox();
            ck_ttstoggle = new CheckBox();
            bt_newsession = new Button();
            label11 = new Label();
            ck_caninitchat = new CheckBox();
            cb_sysprompt = new ComboBox();
            ck_senseoftime = new CheckBox();
            btVectorSearch = new Button();
            bt_impersonate = new Button();
            web_chat = new Microsoft.Web.WebView2.WinForms.WebView2();
            bt_delete = new Button();
            bt_reroll = new Button();
            bt_send = new Button();
            ed_input = new WaifuAI.Controls.SpellCheckedTextBox();
            panLeft = new Panel();
            boxVLM = new GroupBox();
            label1 = new Label();
            pictEmbed = new PictureBox();
            bt_clearimg = new Button();
            grp_settings = new GroupBox();
            ck_agentmode = new CheckBox();
            ck_onlinerag = new CheckBox();
            panel1 = new Panel();
            ckNatMem = new CheckBox();
            ck_ragenabled = new CheckBox();
            ck_worldinfo = new CheckBox();
            ck_sessionmemory = new CheckBox();
            button2 = new Button();
            btRawLog = new Button();
            btWorldEditor = new Button();
            btMainSettings = new Button();
            grp_inference = new GroupBox();
            btSampleEditor = new Button();
            btInstructEdit = new Button();
            ck_charsampler = new CheckBox();
            ck_forceNames = new CheckBox();
            ck_ragtothink = new CheckBox();
            ck_disablethink = new CheckBox();
            label5 = new Label();
            cb_instruct = new ComboBox();
            label6 = new Label();
            cb_infer = new ComboBox();
            label9 = new Label();
            num_temperature = new NumericUpDown();
            grp_model = new GroupBox();
            num_maxresponse = new NumericUpDown();
            label7 = new Label();
            num_maxcontext = new NumericUpDown();
            label8 = new Label();
            bt_connect = new Button();
            statusbar.SuspendLayout();
            panRight.SuspendLayout();
            collapsibleGroupBox1.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)web_chat).BeginInit();
            panLeft.SuspendLayout();
            boxVLM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictEmbed).BeginInit();
            grp_settings.SuspendLayout();
            grp_inference.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_temperature).BeginInit();
            grp_model.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_maxresponse).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_maxcontext).BeginInit();
            SuspendLayout();
            // 
            // HelptoolTip
            // 
            HelptoolTip.AutoPopDelay = 5000;
            HelptoolTip.InitialDelay = 300;
            HelptoolTip.IsBalloon = true;
            HelptoolTip.ReshowDelay = 100;
            HelptoolTip.ToolTipIcon = ToolTipIcon.Info;
            HelptoolTip.ToolTipTitle = "Help and Tips";
            // 
            // statusbar
            // 
            statusbar.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            statusbar.Location = new Point(0, 877);
            statusbar.Name = "statusbar";
            statusbar.Size = new Size(1261, 22);
            statusbar.TabIndex = 2;
            statusbar.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.AutoSize = false;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(300, 17);
            toolStripStatusLabel1.Text = "Session Info";
            toolStripStatusLabel1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(95, 17);
            toolStripStatusLabel2.Text = "Generation Time";
            // 
            // AutoTalkTimer
            // 
            AutoTalkTimer.Enabled = true;
            AutoTalkTimer.Interval = 1000;
            AutoTalkTimer.Tick += AutoTalkTimer_Tick;
            // 
            // panRight
            // 
            panRight.Controls.Add(collapsibleGroupBox1);
            panRight.Controls.Add(button1);
            panRight.Controls.Add(groupBox5);
            panRight.Dock = DockStyle.Right;
            panRight.Location = new Point(1058, 0);
            panRight.Name = "panRight";
            panRight.Size = new Size(203, 877);
            panRight.TabIndex = 8;
            // 
            // collapsibleGroupBox1
            // 
            collapsibleGroupBox1.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox1.Controls.Add(modernCheckBox1);
            collapsibleGroupBox1.Font = new Font("Segoe UI", 9F);
            collapsibleGroupBox1.Location = new Point(6, 388);
            collapsibleGroupBox1.Name = "collapsibleGroupBox1";
            collapsibleGroupBox1.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox1.Size = new Size(190, 160);
            collapsibleGroupBox1.TabIndex = 27;
            collapsibleGroupBox1.Text = "collapsibleGroupBox1";
            // 
            // modernCheckBox1
            // 
            modernCheckBox1.Font = new Font("Segoe UI", 9F);
            modernCheckBox1.Location = new Point(15, 37);
            modernCheckBox1.Name = "modernCheckBox1";
            modernCheckBox1.Size = new Size(150, 26);
            modernCheckBox1.TabIndex = 0;
            modernCheckBox1.Text = "modernCheckBox1";
            modernCheckBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(6, 359);
            button1.Name = "button1";
            button1.Size = new Size(190, 23);
            button1.TabIndex = 26;
            button1.Text = "Test Shit Button";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Controls.Add(btChatHistory);
            groupBox5.Controls.Add(btSysPrompt);
            groupBox5.Controls.Add(cb_user);
            groupBox5.Controls.Add(label4);
            groupBox5.Controls.Add(bt_editchar);
            groupBox5.Controls.Add(bt_scenario);
            groupBox5.Controls.Add(label3);
            groupBox5.Controls.Add(cb_bot);
            groupBox5.Controls.Add(ck_ttstoggle);
            groupBox5.Controls.Add(bt_newsession);
            groupBox5.Controls.Add(label11);
            groupBox5.Controls.Add(ck_caninitchat);
            groupBox5.Controls.Add(cb_sysprompt);
            groupBox5.Controls.Add(ck_senseoftime);
            groupBox5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox5.Location = new Point(6, 3);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(190, 350);
            groupBox5.TabIndex = 25;
            groupBox5.TabStop = false;
            groupBox5.Text = "Chat Settings";
            // 
            // btChatHistory
            // 
            btChatHistory.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btChatHistory.BackColor = Color.Khaki;
            btChatHistory.FlatStyle = FlatStyle.Flat;
            btChatHistory.Font = new Font("Segoe UI", 9F);
            btChatHistory.Location = new Point(6, 263);
            btChatHistory.Name = "btChatHistory";
            btChatHistory.Size = new Size(178, 23);
            btChatHistory.TabIndex = 33;
            btChatHistory.Tag = "no-theme";
            btChatHistory.Text = "Chat History Manager";
            btChatHistory.UseVisualStyleBackColor = false;
            btChatHistory.Click += btChatHistory_Click;
            // 
            // btSysPrompt
            // 
            btSysPrompt.BackColor = Color.LightSteelBlue;
            btSysPrompt.FlatStyle = FlatStyle.Flat;
            btSysPrompt.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Pixel);
            btSysPrompt.Location = new Point(127, 14);
            btSysPrompt.Name = "btSysPrompt";
            btSysPrompt.Size = new Size(58, 20);
            btSysPrompt.TabIndex = 29;
            btSysPrompt.Tag = "no-theme";
            btSysPrompt.Text = "Editor";
            btSysPrompt.UseVisualStyleBackColor = false;
            btSysPrompt.Click += btSysPrompt_Click;
            // 
            // cb_user
            // 
            cb_user.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_user.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_user.FlatStyle = FlatStyle.Flat;
            cb_user.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cb_user.Location = new Point(7, 150);
            cb_user.Name = "cb_user";
            cb_user.Size = new Size(178, 23);
            cb_user.TabIndex = 3;
            cb_user.SelectedIndexChanged += cb_user_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label4.Location = new Point(7, 128);
            label4.Name = "label4";
            label4.Size = new Size(75, 15);
            label4.TabIndex = 2;
            label4.Text = "User Persona";
            // 
            // bt_editchar
            // 
            bt_editchar.BackColor = Color.LightSteelBlue;
            bt_editchar.FlatStyle = FlatStyle.Flat;
            bt_editchar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Pixel);
            bt_editchar.Location = new Point(126, 69);
            bt_editchar.Name = "bt_editchar";
            bt_editchar.Size = new Size(58, 20);
            bt_editchar.TabIndex = 27;
            bt_editchar.Tag = "no-theme";
            bt_editchar.Text = "Editor";
            bt_editchar.UseVisualStyleBackColor = false;
            bt_editchar.Click += bt_editchar_Click;
            // 
            // bt_scenario
            // 
            bt_scenario.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bt_scenario.BackColor = Color.Khaki;
            bt_scenario.FlatStyle = FlatStyle.Flat;
            bt_scenario.Font = new Font("Segoe UI", 9F);
            bt_scenario.Location = new Point(6, 292);
            bt_scenario.Name = "bt_scenario";
            bt_scenario.Size = new Size(178, 23);
            bt_scenario.TabIndex = 26;
            bt_scenario.Tag = "no-theme";
            bt_scenario.Text = "Change Scenario";
            bt_scenario.UseVisualStyleBackColor = false;
            bt_scenario.Click += bt_scenario_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label3.Location = new Point(7, 74);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 0;
            label3.Text = "Bot Persona";
            // 
            // cb_bot
            // 
            cb_bot.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_bot.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_bot.FlatStyle = FlatStyle.Flat;
            cb_bot.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cb_bot.Location = new Point(7, 95);
            cb_bot.Name = "cb_bot";
            cb_bot.Size = new Size(178, 23);
            cb_bot.TabIndex = 1;
            cb_bot.SelectedIndexChanged += cb_bot_SelectedIndexChanged;
            // 
            // ck_ttstoggle
            // 
            ck_ttstoggle.AutoSize = true;
            ck_ttstoggle.Font = new Font("Segoe UI", 9F);
            ck_ttstoggle.Location = new Point(7, 230);
            ck_ttstoggle.Name = "ck_ttstoggle";
            ck_ttstoggle.Size = new Size(84, 19);
            ck_ttstoggle.TabIndex = 32;
            ck_ttstoggle.Text = "Enable TTS";
            ck_ttstoggle.UseVisualStyleBackColor = true;
            ck_ttstoggle.CheckedChanged += ck_ttstoggle_CheckedChanged;
            // 
            // bt_newsession
            // 
            bt_newsession.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bt_newsession.BackColor = Color.PaleGreen;
            bt_newsession.FlatStyle = FlatStyle.Flat;
            bt_newsession.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_newsession.Location = new Point(6, 321);
            bt_newsession.Name = "bt_newsession";
            bt_newsession.Size = new Size(178, 23);
            bt_newsession.TabIndex = 21;
            bt_newsession.Tag = "no-theme";
            bt_newsession.Text = "Start New Session";
            bt_newsession.UseVisualStyleBackColor = false;
            bt_newsession.Click += StartNewSession;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label11.Location = new Point(6, 19);
            label11.Name = "label11";
            label11.Size = new Size(85, 15);
            label11.TabIndex = 18;
            label11.Text = "System Prompt";
            // 
            // ck_caninitchat
            // 
            ck_caninitchat.AutoSize = true;
            ck_caninitchat.Font = new Font("Segoe UI", 9F);
            ck_caninitchat.Location = new Point(7, 181);
            ck_caninitchat.Name = "ck_caninitchat";
            ck_caninitchat.Size = new Size(131, 19);
            ck_caninitchat.TabIndex = 28;
            ck_caninitchat.Text = "Bot can initiate chat";
            ck_caninitchat.UseVisualStyleBackColor = true;
            ck_caninitchat.CheckedChanged += ck_caninit_CheckedChanged;
            // 
            // cb_sysprompt
            // 
            cb_sysprompt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_sysprompt.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_sysprompt.FlatStyle = FlatStyle.Flat;
            cb_sysprompt.Font = new Font("Segoe UI", 9F);
            cb_sysprompt.Location = new Point(7, 40);
            cb_sysprompt.Name = "cb_sysprompt";
            cb_sysprompt.Size = new Size(178, 23);
            cb_sysprompt.TabIndex = 19;
            cb_sysprompt.SelectedIndexChanged += cb_sysprompt_SelectionIndexChanged;
            // 
            // ck_senseoftime
            // 
            ck_senseoftime.AutoSize = true;
            ck_senseoftime.Font = new Font("Segoe UI", 9F);
            ck_senseoftime.Location = new Point(7, 205);
            ck_senseoftime.Name = "ck_senseoftime";
            ck_senseoftime.Size = new Size(100, 19);
            ck_senseoftime.TabIndex = 23;
            ck_senseoftime.Text = "Sense of Time";
            ck_senseoftime.UseVisualStyleBackColor = true;
            ck_senseoftime.CheckedChanged += ck_senseoftime_CheckedChanged;
            // 
            // btVectorSearch
            // 
            btVectorSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btVectorSearch.BackColor = Color.Khaki;
            btVectorSearch.FlatStyle = FlatStyle.Flat;
            btVectorSearch.Font = new Font("Segoe UI", 9F);
            btVectorSearch.Location = new Point(8, 206);
            btVectorSearch.Name = "btVectorSearch";
            btVectorSearch.Size = new Size(174, 23);
            btVectorSearch.TabIndex = 34;
            btVectorSearch.Tag = "no-theme";
            btVectorSearch.Text = "Vector Search";
            btVectorSearch.UseVisualStyleBackColor = false;
            btVectorSearch.Click += btVectorSearch_Click;
            // 
            // bt_impersonate
            // 
            bt_impersonate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_impersonate.BackColor = Color.LightSteelBlue;
            bt_impersonate.FlatStyle = FlatStyle.Flat;
            bt_impersonate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_impersonate.Location = new Point(992, 788);
            bt_impersonate.Name = "bt_impersonate";
            bt_impersonate.Size = new Size(60, 25);
            bt_impersonate.TabIndex = 7;
            bt_impersonate.Tag = "no-theme";
            bt_impersonate.Text = "For Me";
            bt_impersonate.UseVisualStyleBackColor = false;
            bt_impersonate.Click += Impersonate;
            // 
            // web_chat
            // 
            web_chat.AllowExternalDrop = false;
            web_chat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            web_chat.CreationProperties = null;
            web_chat.DefaultBackgroundColor = Color.White;
            web_chat.Location = new Point(209, 3);
            web_chat.Name = "web_chat";
            web_chat.Size = new Size(843, 779);
            web_chat.TabIndex = 6;
            web_chat.ZoomFactor = 1D;
            // 
            // bt_delete
            // 
            bt_delete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_delete.BackColor = Color.LightCoral;
            bt_delete.FlatStyle = FlatStyle.Flat;
            bt_delete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_delete.Location = new Point(992, 849);
            bt_delete.Name = "bt_delete";
            bt_delete.Size = new Size(60, 25);
            bt_delete.TabIndex = 5;
            bt_delete.Tag = "no-theme";
            bt_delete.Text = "Delete";
            bt_delete.UseVisualStyleBackColor = false;
            bt_delete.Click += DeleteLastMessage;
            // 
            // bt_reroll
            // 
            bt_reroll.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_reroll.BackColor = Color.Khaki;
            bt_reroll.FlatStyle = FlatStyle.Flat;
            bt_reroll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_reroll.Location = new Point(992, 819);
            bt_reroll.Name = "bt_reroll";
            bt_reroll.Size = new Size(60, 25);
            bt_reroll.TabIndex = 4;
            bt_reroll.Tag = "no-theme";
            bt_reroll.Text = "ReRoll";
            bt_reroll.UseVisualStyleBackColor = false;
            bt_reroll.Click += RerollMessage;
            // 
            // bt_send
            // 
            bt_send.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_send.BackColor = Color.PaleGreen;
            bt_send.FlatStyle = FlatStyle.Flat;
            bt_send.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_send.Location = new Point(926, 788);
            bt_send.Name = "bt_send";
            bt_send.Size = new Size(60, 86);
            bt_send.TabIndex = 3;
            bt_send.Tag = "no-theme";
            bt_send.Text = "Send";
            bt_send.UseVisualStyleBackColor = false;
            bt_send.Click += SendMessage;
            // 
            // ed_input
            // 
            ed_input.AllowDrop = true;
            ed_input.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ed_input.BackColor = Color.FromArgb(32, 70, 130, 180);
            ed_input.Font = new Font("Segoe UI", 16F);
            ed_input.Location = new Point(209, 788);
            ed_input.Multiline = true;
            ed_input.Name = "ed_input";
            ed_input.ScrollBars = ScrollBars.Vertical;
            ed_input.Size = new Size(711, 86);
            ed_input.TabIndex = 2;
            // 
            // panLeft
            // 
            panLeft.Controls.Add(boxVLM);
            panLeft.Controls.Add(grp_settings);
            panLeft.Controls.Add(grp_inference);
            panLeft.Controls.Add(grp_model);
            panLeft.Dock = DockStyle.Left;
            panLeft.Location = new Point(0, 0);
            panLeft.Name = "panLeft";
            panLeft.Size = new Size(203, 877);
            panLeft.TabIndex = 0;
            // 
            // boxVLM
            // 
            boxVLM.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            boxVLM.Controls.Add(label1);
            boxVLM.Controls.Add(pictEmbed);
            boxVLM.Controls.Add(bt_clearimg);
            boxVLM.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            boxVLM.Location = new Point(6, 735);
            boxVLM.Name = "boxVLM";
            boxVLM.Size = new Size(190, 139);
            boxVLM.TabIndex = 27;
            boxVLM.TabStop = false;
            boxVLM.Text = "Visual Language Model";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F);
            label1.Location = new Point(27, 22);
            label1.Name = "label1";
            label1.Size = new Size(140, 15);
            label1.TabIndex = 35;
            label1.Text = "Drag && drop images here";
            // 
            // pictEmbed
            // 
            pictEmbed.BorderStyle = BorderStyle.FixedSingle;
            pictEmbed.Location = new Point(64, 40);
            pictEmbed.Name = "pictEmbed";
            pictEmbed.Size = new Size(64, 64);
            pictEmbed.SizeMode = PictureBoxSizeMode.StretchImage;
            pictEmbed.TabIndex = 33;
            pictEmbed.TabStop = false;
            // 
            // bt_clearimg
            // 
            bt_clearimg.BackColor = Color.LightSteelBlue;
            bt_clearimg.FlatStyle = FlatStyle.Flat;
            bt_clearimg.Font = new Font("Segoe UI", 9F);
            bt_clearimg.Location = new Point(6, 110);
            bt_clearimg.Name = "bt_clearimg";
            bt_clearimg.Size = new Size(175, 23);
            bt_clearimg.TabIndex = 34;
            bt_clearimg.Tag = "no-theme";
            bt_clearimg.Text = "Clear";
            bt_clearimg.UseVisualStyleBackColor = false;
            bt_clearimg.Click += bt_clearimg_Click;
            // 
            // grp_settings
            // 
            grp_settings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grp_settings.Controls.Add(ck_agentmode);
            grp_settings.Controls.Add(ck_onlinerag);
            grp_settings.Controls.Add(panel1);
            grp_settings.Controls.Add(ckNatMem);
            grp_settings.Controls.Add(ck_ragenabled);
            grp_settings.Controls.Add(ck_worldinfo);
            grp_settings.Controls.Add(ck_sessionmemory);
            grp_settings.Controls.Add(button2);
            grp_settings.Controls.Add(btVectorSearch);
            grp_settings.Controls.Add(btRawLog);
            grp_settings.Controls.Add(btWorldEditor);
            grp_settings.Controls.Add(btMainSettings);
            grp_settings.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grp_settings.Location = new Point(6, 376);
            grp_settings.Name = "grp_settings";
            grp_settings.Padding = new Padding(5);
            grp_settings.Size = new Size(190, 353);
            grp_settings.TabIndex = 26;
            grp_settings.TabStop = false;
            grp_settings.Text = "Quick Settings";
            // 
            // ck_agentmode
            // 
            ck_agentmode.AutoSize = true;
            ck_agentmode.Dock = DockStyle.Top;
            ck_agentmode.Font = new Font("Segoe UI", 9F);
            ck_agentmode.Location = new Point(5, 126);
            ck_agentmode.Name = "ck_agentmode";
            ck_agentmode.Size = new Size(180, 19);
            ck_agentmode.TabIndex = 37;
            ck_agentmode.Text = "Agent Mode";
            ck_agentmode.UseVisualStyleBackColor = true;
            ck_agentmode.CheckedChanged += ck_agentmode_CheckedChanged;
            // 
            // ck_onlinerag
            // 
            ck_onlinerag.AutoSize = true;
            ck_onlinerag.Dock = DockStyle.Top;
            ck_onlinerag.Font = new Font("Segoe UI", 9F);
            ck_onlinerag.Location = new Point(5, 107);
            ck_onlinerag.Name = "ck_onlinerag";
            ck_onlinerag.Size = new Size(180, 19);
            ck_onlinerag.TabIndex = 29;
            ck_onlinerag.Text = "Web Search";
            ck_onlinerag.UseVisualStyleBackColor = true;
            ck_onlinerag.CheckedChanged += ck_onlinerag_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(5, 97);
            panel1.Margin = new Padding(8);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(8);
            panel1.Size = new Size(180, 10);
            panel1.TabIndex = 27;
            // 
            // ckNatMem
            // 
            ckNatMem.AutoSize = true;
            ckNatMem.Dock = DockStyle.Top;
            ckNatMem.Font = new Font("Segoe UI", 9F);
            ckNatMem.Location = new Point(5, 78);
            ckNatMem.Name = "ckNatMem";
            ckNatMem.Size = new Size(180, 19);
            ckNatMem.TabIndex = 42;
            ckNatMem.Text = "Natural Memory Inserts";
            ckNatMem.UseVisualStyleBackColor = true;
            ckNatMem.CheckedChanged += ckNatMem_CheckedChanged;
            // 
            // ck_ragenabled
            // 
            ck_ragenabled.AutoSize = true;
            ck_ragenabled.Checked = true;
            ck_ragenabled.CheckState = CheckState.Checked;
            ck_ragenabled.Dock = DockStyle.Top;
            ck_ragenabled.Font = new Font("Segoe UI", 9F);
            ck_ragenabled.Location = new Point(5, 59);
            ck_ragenabled.Name = "ck_ragenabled";
            ck_ragenabled.Size = new Size(180, 19);
            ck_ragenabled.TabIndex = 27;
            ck_ragenabled.Text = "Local RAG ";
            ck_ragenabled.UseVisualStyleBackColor = true;
            ck_ragenabled.CheckedChanged += ck_ragenabled_CheckedChanged;
            // 
            // ck_worldinfo
            // 
            ck_worldinfo.AutoSize = true;
            ck_worldinfo.Dock = DockStyle.Top;
            ck_worldinfo.Font = new Font("Segoe UI", 9F);
            ck_worldinfo.Location = new Point(5, 40);
            ck_worldinfo.Name = "ck_worldinfo";
            ck_worldinfo.Size = new Size(180, 19);
            ck_worldinfo.TabIndex = 25;
            ck_worldinfo.Text = "World Info Enabled";
            ck_worldinfo.UseVisualStyleBackColor = true;
            ck_worldinfo.CheckedChanged += ck_worldinfo_CheckedChanged;
            // 
            // ck_sessionmemory
            // 
            ck_sessionmemory.AutoSize = true;
            ck_sessionmemory.Dock = DockStyle.Top;
            ck_sessionmemory.Font = new Font("Segoe UI", 9F);
            ck_sessionmemory.Location = new Point(5, 21);
            ck_sessionmemory.Name = "ck_sessionmemory";
            ck_sessionmemory.Size = new Size(180, 19);
            ck_sessionmemory.TabIndex = 24;
            ck_sessionmemory.Text = "Session Memory";
            ck_sessionmemory.UseVisualStyleBackColor = true;
            ck_sessionmemory.CheckedChanged += ck_sessionmemory_CheckedChanged;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button2.BackColor = Color.Khaki;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 9F);
            button2.Location = new Point(8, 235);
            button2.Name = "button2";
            button2.Size = new Size(174, 23);
            button2.TabIndex = 41;
            button2.Tag = "no-theme";
            button2.Text = "Brain Memory Map";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // btRawLog
            // 
            btRawLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btRawLog.BackColor = Color.Khaki;
            btRawLog.FlatStyle = FlatStyle.Flat;
            btRawLog.Font = new Font("Segoe UI", 9F);
            btRawLog.Location = new Point(8, 264);
            btRawLog.Name = "btRawLog";
            btRawLog.Size = new Size(174, 23);
            btRawLog.TabIndex = 40;
            btRawLog.Tag = "no-theme";
            btRawLog.Text = "View Raw Log";
            btRawLog.UseVisualStyleBackColor = false;
            btRawLog.Click += btRawLog_Click;
            // 
            // btWorldEditor
            // 
            btWorldEditor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btWorldEditor.BackColor = Color.LightGreen;
            btWorldEditor.FlatStyle = FlatStyle.Flat;
            btWorldEditor.Font = new Font("Segoe UI", 9F);
            btWorldEditor.Location = new Point(8, 293);
            btWorldEditor.Name = "btWorldEditor";
            btWorldEditor.Size = new Size(174, 23);
            btWorldEditor.TabIndex = 39;
            btWorldEditor.Tag = "no-theme";
            btWorldEditor.Text = "WorldInfo Editor";
            btWorldEditor.UseVisualStyleBackColor = false;
            btWorldEditor.Click += btWorldEditor_Click;
            // 
            // btMainSettings
            // 
            btMainSettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btMainSettings.BackColor = Color.PaleGreen;
            btMainSettings.FlatStyle = FlatStyle.Flat;
            btMainSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btMainSettings.Location = new Point(8, 322);
            btMainSettings.Name = "btMainSettings";
            btMainSettings.Size = new Size(174, 23);
            btMainSettings.TabIndex = 38;
            btMainSettings.Tag = "no-theme";
            btMainSettings.Text = "General Settings";
            btMainSettings.UseVisualStyleBackColor = false;
            btMainSettings.Click += btMainSettings_Click;
            // 
            // grp_inference
            // 
            grp_inference.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grp_inference.Controls.Add(btSampleEditor);
            grp_inference.Controls.Add(btInstructEdit);
            grp_inference.Controls.Add(ck_charsampler);
            grp_inference.Controls.Add(ck_forceNames);
            grp_inference.Controls.Add(ck_ragtothink);
            grp_inference.Controls.Add(ck_disablethink);
            grp_inference.Controls.Add(label5);
            grp_inference.Controls.Add(cb_instruct);
            grp_inference.Controls.Add(label6);
            grp_inference.Controls.Add(cb_infer);
            grp_inference.Controls.Add(label9);
            grp_inference.Controls.Add(num_temperature);
            grp_inference.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grp_inference.Location = new Point(6, 105);
            grp_inference.Name = "grp_inference";
            grp_inference.Padding = new Padding(5);
            grp_inference.Size = new Size(190, 265);
            grp_inference.TabIndex = 24;
            grp_inference.TabStop = false;
            grp_inference.Text = "Inference Settings";
            // 
            // btSampleEditor
            // 
            btSampleEditor.BackColor = Color.LightSteelBlue;
            btSampleEditor.FlatStyle = FlatStyle.Flat;
            btSampleEditor.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Pixel);
            btSampleEditor.Location = new Point(123, 77);
            btSampleEditor.Name = "btSampleEditor";
            btSampleEditor.Size = new Size(62, 20);
            btSampleEditor.TabIndex = 30;
            btSampleEditor.Tag = "no-theme";
            btSampleEditor.Text = "Editor";
            btSampleEditor.UseVisualStyleBackColor = false;
            btSampleEditor.Click += btSampleEditor_Click;
            // 
            // btInstructEdit
            // 
            btInstructEdit.BackColor = Color.LightSteelBlue;
            btInstructEdit.FlatStyle = FlatStyle.Flat;
            btInstructEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Pixel);
            btInstructEdit.Location = new Point(123, 22);
            btInstructEdit.Name = "btInstructEdit";
            btInstructEdit.Size = new Size(62, 20);
            btInstructEdit.TabIndex = 28;
            btInstructEdit.Tag = "no-theme";
            btInstructEdit.Text = "Editor";
            btInstructEdit.UseVisualStyleBackColor = false;
            btInstructEdit.Click += btInstructEdit_Click;
            // 
            // ck_charsampler
            // 
            ck_charsampler.AutoSize = true;
            ck_charsampler.Dock = DockStyle.Bottom;
            ck_charsampler.Font = new Font("Segoe UI", 9F);
            ck_charsampler.Location = new Point(5, 184);
            ck_charsampler.Name = "ck_charsampler";
            ck_charsampler.Size = new Size(180, 19);
            ck_charsampler.TabIndex = 26;
            ck_charsampler.Text = "Use character's samplers";
            ck_charsampler.UseVisualStyleBackColor = true;
            // 
            // ck_forceNames
            // 
            ck_forceNames.AutoSize = true;
            ck_forceNames.Dock = DockStyle.Bottom;
            ck_forceNames.Font = new Font("Segoe UI", 9F);
            ck_forceNames.Location = new Point(5, 203);
            ck_forceNames.Name = "ck_forceNames";
            ck_forceNames.Size = new Size(180, 19);
            ck_forceNames.TabIndex = 25;
            ck_forceNames.Text = "Add names to prompt";
            ck_forceNames.UseVisualStyleBackColor = true;
            ck_forceNames.CheckedChanged += ck_forceNames_CheckedChanged;
            // 
            // ck_ragtothink
            // 
            ck_ragtothink.AutoSize = true;
            ck_ragtothink.Dock = DockStyle.Bottom;
            ck_ragtothink.Font = new Font("Segoe UI", 9F);
            ck_ragtothink.Location = new Point(5, 222);
            ck_ragtothink.Name = "ck_ragtothink";
            ck_ragtothink.Size = new Size(180, 19);
            ck_ragtothink.TabIndex = 36;
            ck_ragtothink.Text = "Put context in think block";
            ck_ragtothink.UseVisualStyleBackColor = true;
            ck_ragtothink.CheckedChanged += ck_ragtothink_CheckedChanged;
            // 
            // ck_disablethink
            // 
            ck_disablethink.AutoSize = true;
            ck_disablethink.Dock = DockStyle.Bottom;
            ck_disablethink.Font = new Font("Segoe UI", 9F);
            ck_disablethink.Location = new Point(5, 241);
            ck_disablethink.Name = "ck_disablethink";
            ck_disablethink.Size = new Size(180, 19);
            ck_disablethink.TabIndex = 35;
            ck_disablethink.Text = "Disable Thinking";
            ck_disablethink.UseVisualStyleBackColor = true;
            ck_disablethink.CheckedChanged += ck_disablethink_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label5.Location = new Point(5, 30);
            label5.Name = "label5";
            label5.Size = new Size(102, 15);
            label5.TabIndex = 4;
            label5.Text = "Instruction Format";
            // 
            // cb_instruct
            // 
            cb_instruct.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_instruct.FlatStyle = FlatStyle.Flat;
            cb_instruct.Font = new Font("Segoe UI", 9F);
            cb_instruct.Location = new Point(5, 48);
            cb_instruct.Name = "cb_instruct";
            cb_instruct.Size = new Size(180, 23);
            cb_instruct.TabIndex = 5;
            cb_instruct.SelectedIndexChanged += cb_instruct_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label6.Location = new Point(5, 85);
            label6.Name = "label6";
            label6.Size = new Size(102, 15);
            label6.TabIndex = 6;
            label6.Text = "Sampling Settings";
            // 
            // cb_infer
            // 
            cb_infer.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_infer.FlatStyle = FlatStyle.Flat;
            cb_infer.Font = new Font("Segoe UI", 9F);
            cb_infer.Location = new Point(5, 103);
            cb_infer.Name = "cb_infer";
            cb_infer.Size = new Size(180, 23);
            cb_infer.TabIndex = 7;
            cb_infer.SelectedIndexChanged += cb_infer_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label9.Location = new Point(5, 132);
            label9.Name = "label9";
            label9.Size = new Size(122, 15);
            label9.TabIndex = 16;
            label9.Text = "Temperature Override";
            // 
            // num_temperature
            // 
            num_temperature.DecimalPlaces = 2;
            num_temperature.Font = new Font("Segoe UI", 9F);
            num_temperature.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_temperature.Location = new Point(5, 150);
            num_temperature.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            num_temperature.Name = "num_temperature";
            num_temperature.Size = new Size(180, 23);
            num_temperature.TabIndex = 17;
            num_temperature.ThousandsSeparator = true;
            num_temperature.Value = new decimal(new int[] { 7, 0, 0, 65536 });
            num_temperature.ValueChanged += num_temperature_ValueChanged;
            // 
            // grp_model
            // 
            grp_model.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grp_model.Controls.Add(num_maxresponse);
            grp_model.Controls.Add(label7);
            grp_model.Controls.Add(num_maxcontext);
            grp_model.Controls.Add(label8);
            grp_model.Controls.Add(bt_connect);
            grp_model.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grp_model.Location = new Point(6, 3);
            grp_model.Name = "grp_model";
            grp_model.Padding = new Padding(5);
            grp_model.Size = new Size(190, 96);
            grp_model.TabIndex = 23;
            grp_model.TabStop = false;
            grp_model.Text = "Model Settings";
            // 
            // num_maxresponse
            // 
            num_maxresponse.Font = new Font("Segoe UI", 9F);
            num_maxresponse.Increment = new decimal(new int[] { 32, 0, 0, 0 });
            num_maxresponse.Location = new Point(100, 37);
            num_maxresponse.Maximum = new decimal(new int[] { 16384, 0, 0, 0 });
            num_maxresponse.Name = "num_maxresponse";
            num_maxresponse.Size = new Size(85, 23);
            num_maxresponse.TabIndex = 12;
            num_maxresponse.ThousandsSeparator = true;
            num_maxresponse.Value = new decimal(new int[] { 512, 0, 0, 0 });
            num_maxresponse.ValueChanged += num_maxresponse_ValueChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F);
            label7.Location = new Point(8, 21);
            label7.Name = "label7";
            label7.Size = new Size(73, 15);
            label7.TabIndex = 8;
            label7.Text = "Max Context";
            // 
            // num_maxcontext
            // 
            num_maxcontext.Font = new Font("Segoe UI", 9F);
            num_maxcontext.Increment = new decimal(new int[] { 512, 0, 0, 0 });
            num_maxcontext.Location = new Point(5, 37);
            num_maxcontext.Maximum = new decimal(new int[] { 16384, 0, 0, 0 });
            num_maxcontext.Minimum = new decimal(new int[] { 1024, 0, 0, 0 });
            num_maxcontext.Name = "num_maxcontext";
            num_maxcontext.Size = new Size(89, 23);
            num_maxcontext.TabIndex = 10;
            num_maxcontext.Value = new decimal(new int[] { 16384, 0, 0, 0 });
            num_maxcontext.ValueChanged += num_maxcontext_ValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F);
            label8.Location = new Point(102, 21);
            label8.Name = "label8";
            label8.Size = new Size(76, 15);
            label8.TabIndex = 11;
            label8.Text = "Reply Length";
            // 
            // bt_connect
            // 
            bt_connect.BackColor = Color.PaleGreen;
            bt_connect.Dock = DockStyle.Bottom;
            bt_connect.FlatStyle = FlatStyle.Flat;
            bt_connect.Location = new Point(5, 68);
            bt_connect.Name = "bt_connect";
            bt_connect.Size = new Size(180, 23);
            bt_connect.TabIndex = 14;
            bt_connect.Tag = "no-theme";
            bt_connect.Text = "Connect";
            bt_connect.UseVisualStyleBackColor = false;
            bt_connect.Click += bt_connectClick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1261, 899);
            Controls.Add(panRight);
            Controls.Add(bt_impersonate);
            Controls.Add(web_chat);
            Controls.Add(bt_delete);
            Controls.Add(bt_reroll);
            Controls.Add(bt_send);
            Controls.Add(ed_input);
            Controls.Add(panLeft);
            Controls.Add(statusbar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "w(AI)fu";
            FormClosing += MainForm_FormClosing;
            statusbar.ResumeLayout(false);
            statusbar.PerformLayout();
            panRight.ResumeLayout(false);
            collapsibleGroupBox1.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)web_chat).EndInit();
            panLeft.ResumeLayout(false);
            boxVLM.ResumeLayout(false);
            boxVLM.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictEmbed).EndInit();
            grp_settings.ResumeLayout(false);
            grp_settings.PerformLayout();
            grp_inference.ResumeLayout(false);
            grp_inference.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_temperature).EndInit();
            grp_model.ResumeLayout(false);
            grp_model.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_maxresponse).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_maxcontext).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ToolTip HelptoolTip;
        private StatusStrip statusbar;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        internal System.Windows.Forms.Timer AutoTalkTimer;
        private Button bt_impersonate;
        private Microsoft.Web.WebView2.WinForms.WebView2 web_chat;
        private Button bt_delete;
        private Button bt_reroll;
        private Button bt_send;
        private WaifuAI.Controls.SpellCheckedTextBox ed_input;
        private Panel panLeft;
        private GroupBox grp_settings;
        private Button bt_clearimg;
        private PictureBox pictEmbed;
        private CheckBox ck_ttstoggle;
        private CheckBox ck_onlinerag;
        private CheckBox ck_caninitchat;
        private CheckBox ck_worldinfo;
        private CheckBox ck_ragenabled;
        private CheckBox ck_sessionmemory;
        private CheckBox ck_senseoftime;
        private GroupBox groupBox5;
        private Button bt_editchar;
        private Button bt_scenario;
        private Label label3;
        private ComboBox cb_bot;
        private Label label4;
        private ComboBox cb_user;
        private Button bt_newsession;
        private Label label11;
        private ComboBox cb_sysprompt;
        private GroupBox grp_inference;
        private CheckBox ck_charsampler;
        private CheckBox ck_forceNames;
        private Label label5;
        private ComboBox cb_instruct;
        private Label label6;
        private ComboBox cb_infer;
        private Label label9;
        private NumericUpDown num_temperature;
        private GroupBox grp_model;
        private NumericUpDown num_maxresponse;
        private Label label7;
        private NumericUpDown num_maxcontext;
        private Label label8;
        private Button bt_connect;
        private CheckBox ck_disablethink;
        private CheckBox ck_ragtothink;
        private CheckBox ck_agentmode;
        private Button btInstructEdit;
        private Button btSysPrompt;
        private Button btSampleEditor;
        private Button btMainSettings;
        private Button btWorldEditor;
        private Panel panRight;
        private GroupBox boxVLM;
        private Label label1;
        private Button btChatHistory;
        private Button btVectorSearch;
        private Button btRawLog;
        private Button button1;
        private Button button2;
        private CheckBox ckNatMem;
        private Panel panel1;
        private Controls.CollapsibleGroupBox collapsibleGroupBox1;
        private Controls.ModernCheckBox modernCheckBox1;
    }
}
