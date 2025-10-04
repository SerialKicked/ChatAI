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
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            AutoTalkTimer = new System.Windows.Forms.Timer(components);
            button1 = new Button();
            btChatHistory = new Button();
            btSysPrompt = new Button();
            cb_user = new ComboBox();
            label4 = new Label();
            bt_editchar = new Button();
            bt_scenario = new Button();
            label3 = new Label();
            cb_bot = new ComboBox();
            bt_newsession = new Button();
            label11 = new Label();
            cb_sysprompt = new ComboBox();
            label1 = new Label();
            pictEmbed = new PictureBox();
            bt_clearimg = new Button();
            button2 = new Button();
            btVectorSearch = new Button();
            btRawLog = new Button();
            btWorldEditor = new Button();
            btMainSettings = new Button();
            btSampleEditor = new Button();
            btInstructEdit = new Button();
            label5 = new Label();
            cb_instruct = new ComboBox();
            label6 = new Label();
            cb_infer = new ComboBox();
            label9 = new Label();
            num_temperature = new NumericUpDown();
            collapseModel = new WaifuAI.Controls.CollapsibleGroupBox();
            num_maxresponse = new NumericUpDown();
            label7 = new Label();
            num_maxcontext = new NumericUpDown();
            label8 = new Label();
            bt_connect = new Button();
            bt_impersonate = new Button();
            web_chat = new Microsoft.Web.WebView2.WinForms.WebView2();
            bt_delete = new Button();
            bt_reroll = new Button();
            bt_send = new Button();
            ed_input = new WaifuAI.Controls.SpellCheckedTextBox();
            collapsibleGroupBox1 = new WaifuAI.Controls.CollapsibleGroupBox();
            mck_charsampler = new WaifuAI.Controls.ModernCheckBox();
            mck_forceNames = new WaifuAI.Controls.ModernCheckBox();
            mck_ragtothink = new WaifuAI.Controls.ModernCheckBox();
            mck_disablethink = new WaifuAI.Controls.ModernCheckBox();
            panLeft = new WaifuAI.Controls.VerticalStackPanel();
            cboxVLM = new WaifuAI.Controls.CollapsibleGroupBox();
            collapsibleGroupBox2 = new WaifuAI.Controls.CollapsibleGroupBox();
            mck_agentmode = new WaifuAI.Controls.ModernCheckBox();
            mck_onlinerag = new WaifuAI.Controls.ModernCheckBox();
            panel2 = new Panel();
            mckNatMem = new WaifuAI.Controls.ModernCheckBox();
            mck_ragenabled = new WaifuAI.Controls.ModernCheckBox();
            mck_worldinfo = new WaifuAI.Controls.ModernCheckBox();
            mck_sessionmemory = new WaifuAI.Controls.ModernCheckBox();
            panRight = new WaifuAI.Controls.VerticalStackPanel();
            button3 = new Button();
            collapsibleGroupBox3 = new WaifuAI.Controls.CollapsibleGroupBox();
            mck_ttstoggle = new WaifuAI.Controls.ModernCheckBox();
            mck_senseoftime = new WaifuAI.Controls.ModernCheckBox();
            mck_caninitchat = new WaifuAI.Controls.ModernCheckBox();
            panel1 = new Panel();
            statusbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictEmbed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_temperature).BeginInit();
            collapseModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_maxresponse).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_maxcontext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)web_chat).BeginInit();
            collapsibleGroupBox1.SuspendLayout();
            panLeft.SuspendLayout();
            cboxVLM.SuspendLayout();
            collapsibleGroupBox2.SuspendLayout();
            panRight.SuspendLayout();
            collapsibleGroupBox3.SuspendLayout();
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
            statusbar.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3 });
            statusbar.Location = new Point(0, 1020);
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
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size(851, 17);
            toolStripStatusLabel3.Spring = true;
            // 
            // AutoTalkTimer
            // 
            AutoTalkTimer.Enabled = true;
            AutoTalkTimer.Interval = 1000;
            AutoTalkTimer.Tick += AutoTalkTimer_Tick;
            // 
            // button1
            // 
            button1.Location = new Point(0, 381);
            button1.Name = "button1";
            button1.Size = new Size(200, 23);
            button1.TabIndex = 26;
            button1.Text = "Test Shit Button";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btChatHistory
            // 
            btChatHistory.BackColor = Color.DarkKhaki;
            btChatHistory.Dock = DockStyle.Bottom;
            btChatHistory.FlatStyle = FlatStyle.Flat;
            btChatHistory.Font = new Font("Segoe UI", 9F);
            btChatHistory.ForeColor = Color.Black;
            btChatHistory.Location = new Point(8, 285);
            btChatHistory.Name = "btChatHistory";
            btChatHistory.Size = new Size(184, 26);
            btChatHistory.TabIndex = 33;
            btChatHistory.Tag = "no-theme";
            btChatHistory.Text = "Chat History Manager";
            btChatHistory.UseVisualStyleBackColor = false;
            btChatHistory.Click += btChatHistory_Click;
            // 
            // btSysPrompt
            // 
            btSysPrompt.BackColor = Color.LightSlateGray;
            btSysPrompt.FlatStyle = FlatStyle.Flat;
            btSysPrompt.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Pixel);
            btSysPrompt.ForeColor = Color.Black;
            btSysPrompt.Location = new Point(131, 30);
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
            cb_user.Location = new Point(11, 166);
            cb_user.Name = "cb_user";
            cb_user.Size = new Size(178, 23);
            cb_user.TabIndex = 3;
            cb_user.SelectedIndexChanged += cb_user_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label4.Location = new Point(11, 148);
            label4.Name = "label4";
            label4.Size = new Size(75, 15);
            label4.TabIndex = 2;
            label4.Text = "User Persona";
            // 
            // bt_editchar
            // 
            bt_editchar.BackColor = Color.LightSlateGray;
            bt_editchar.FlatStyle = FlatStyle.Flat;
            bt_editchar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Pixel);
            bt_editchar.ForeColor = Color.Black;
            bt_editchar.Location = new Point(131, 85);
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
            bt_scenario.BackColor = Color.DarkKhaki;
            bt_scenario.Dock = DockStyle.Bottom;
            bt_scenario.FlatStyle = FlatStyle.Flat;
            bt_scenario.Font = new Font("Segoe UI", 9F);
            bt_scenario.ForeColor = Color.Black;
            bt_scenario.Location = new Point(8, 311);
            bt_scenario.Name = "bt_scenario";
            bt_scenario.Size = new Size(184, 24);
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
            label3.Location = new Point(11, 90);
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
            cb_bot.Location = new Point(11, 111);
            cb_bot.Name = "cb_bot";
            cb_bot.Size = new Size(178, 23);
            cb_bot.TabIndex = 1;
            cb_bot.SelectedIndexChanged += cb_bot_SelectedIndexChanged;
            // 
            // bt_newsession
            // 
            bt_newsession.BackColor = Color.DarkSeaGreen;
            bt_newsession.Dock = DockStyle.Bottom;
            bt_newsession.FlatStyle = FlatStyle.Flat;
            bt_newsession.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_newsession.ForeColor = Color.Black;
            bt_newsession.Location = new Point(8, 335);
            bt_newsession.Name = "bt_newsession";
            bt_newsession.Size = new Size(184, 24);
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
            label11.Location = new Point(11, 35);
            label11.Name = "label11";
            label11.Size = new Size(85, 15);
            label11.TabIndex = 18;
            label11.Text = "System Prompt";
            // 
            // cb_sysprompt
            // 
            cb_sysprompt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_sysprompt.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_sysprompt.FlatStyle = FlatStyle.Flat;
            cb_sysprompt.Font = new Font("Segoe UI", 9F);
            cb_sysprompt.Location = new Point(11, 56);
            cb_sysprompt.Name = "cb_sysprompt";
            cb_sysprompt.Size = new Size(178, 23);
            cb_sysprompt.TabIndex = 19;
            cb_sysprompt.SelectedIndexChanged += cb_sysprompt_SelectionIndexChanged;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F);
            label1.Location = new Point(106, 35);
            label1.Name = "label1";
            label1.Size = new Size(82, 85);
            label1.TabIndex = 35;
            label1.Text = "Drag && drop images here <<--";
            // 
            // pictEmbed
            // 
            pictEmbed.BackColor = Color.Silver;
            pictEmbed.BorderStyle = BorderStyle.FixedSingle;
            pictEmbed.Location = new Point(11, 35);
            pictEmbed.Name = "pictEmbed";
            pictEmbed.Size = new Size(88, 85);
            pictEmbed.SizeMode = PictureBoxSizeMode.StretchImage;
            pictEmbed.TabIndex = 33;
            pictEmbed.TabStop = false;
            // 
            // bt_clearimg
            // 
            bt_clearimg.Dock = DockStyle.Bottom;
            bt_clearimg.FlatStyle = FlatStyle.Flat;
            bt_clearimg.Location = new Point(8, 129);
            bt_clearimg.Margin = new Padding(0);
            bt_clearimg.Name = "bt_clearimg";
            bt_clearimg.Size = new Size(184, 23);
            bt_clearimg.TabIndex = 34;
            bt_clearimg.Tag = "";
            bt_clearimg.Text = "Clear";
            bt_clearimg.UseVisualStyleBackColor = false;
            bt_clearimg.Click += bt_clearimg_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.DarkKhaki;
            button2.Dock = DockStyle.Bottom;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 9F);
            button2.ForeColor = Color.Black;
            button2.Location = new Point(8, 254);
            button2.Name = "button2";
            button2.Size = new Size(184, 24);
            button2.TabIndex = 41;
            button2.Tag = "no-theme";
            button2.Text = "Brain Memory Map";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // btVectorSearch
            // 
            btVectorSearch.BackColor = Color.DarkKhaki;
            btVectorSearch.Dock = DockStyle.Bottom;
            btVectorSearch.FlatStyle = FlatStyle.Flat;
            btVectorSearch.Font = new Font("Segoe UI", 9F);
            btVectorSearch.ForeColor = Color.Black;
            btVectorSearch.Location = new Point(8, 206);
            btVectorSearch.Name = "btVectorSearch";
            btVectorSearch.Size = new Size(184, 24);
            btVectorSearch.TabIndex = 34;
            btVectorSearch.Tag = "no-theme";
            btVectorSearch.Text = "Vector Search";
            btVectorSearch.UseVisualStyleBackColor = false;
            btVectorSearch.Click += btVectorSearch_Click;
            // 
            // btRawLog
            // 
            btRawLog.BackColor = Color.DarkKhaki;
            btRawLog.Dock = DockStyle.Bottom;
            btRawLog.FlatStyle = FlatStyle.Flat;
            btRawLog.Font = new Font("Segoe UI", 9F);
            btRawLog.ForeColor = Color.Black;
            btRawLog.Location = new Point(8, 230);
            btRawLog.Name = "btRawLog";
            btRawLog.Size = new Size(184, 24);
            btRawLog.TabIndex = 40;
            btRawLog.Tag = "no-theme";
            btRawLog.Text = "View Raw Log";
            btRawLog.UseVisualStyleBackColor = false;
            btRawLog.Click += btRawLog_Click;
            // 
            // btWorldEditor
            // 
            btWorldEditor.BackColor = Color.DarkSeaGreen;
            btWorldEditor.Dock = DockStyle.Bottom;
            btWorldEditor.FlatStyle = FlatStyle.Flat;
            btWorldEditor.Font = new Font("Segoe UI", 9F);
            btWorldEditor.ForeColor = Color.Black;
            btWorldEditor.Location = new Point(8, 278);
            btWorldEditor.Name = "btWorldEditor";
            btWorldEditor.Size = new Size(184, 24);
            btWorldEditor.TabIndex = 39;
            btWorldEditor.Tag = "no-theme";
            btWorldEditor.Text = "WorldInfo Editor";
            btWorldEditor.UseVisualStyleBackColor = false;
            btWorldEditor.Click += btWorldEditor_Click;
            // 
            // btMainSettings
            // 
            btMainSettings.BackColor = Color.DarkSeaGreen;
            btMainSettings.Dock = DockStyle.Bottom;
            btMainSettings.FlatStyle = FlatStyle.Flat;
            btMainSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btMainSettings.ForeColor = Color.Black;
            btMainSettings.Location = new Point(8, 302);
            btMainSettings.Name = "btMainSettings";
            btMainSettings.Size = new Size(184, 24);
            btMainSettings.TabIndex = 38;
            btMainSettings.Tag = "no-theme";
            btMainSettings.Text = "General Settings";
            btMainSettings.UseVisualStyleBackColor = false;
            btMainSettings.Click += btMainSettings_Click;
            // 
            // btSampleEditor
            // 
            btSampleEditor.BackColor = Color.LightSlateGray;
            btSampleEditor.FlatStyle = FlatStyle.Flat;
            btSampleEditor.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Pixel);
            btSampleEditor.ForeColor = Color.Black;
            btSampleEditor.Location = new Point(129, 92);
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
            btInstructEdit.BackColor = Color.LightSlateGray;
            btInstructEdit.FlatStyle = FlatStyle.Flat;
            btInstructEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Pixel);
            btInstructEdit.ForeColor = Color.Black;
            btInstructEdit.Location = new Point(129, 37);
            btInstructEdit.Name = "btInstructEdit";
            btInstructEdit.Size = new Size(62, 20);
            btInstructEdit.TabIndex = 28;
            btInstructEdit.Tag = "no-theme";
            btInstructEdit.Text = "Editor";
            btInstructEdit.UseVisualStyleBackColor = false;
            btInstructEdit.Click += btInstructEdit_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label5.Location = new Point(7, 45);
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
            cb_instruct.Location = new Point(11, 63);
            cb_instruct.Name = "cb_instruct";
            cb_instruct.Size = new Size(180, 23);
            cb_instruct.TabIndex = 5;
            cb_instruct.SelectedIndexChanged += cb_instruct_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label6.Location = new Point(7, 100);
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
            cb_infer.Location = new Point(11, 118);
            cb_infer.Name = "cb_infer";
            cb_infer.Size = new Size(180, 23);
            cb_infer.TabIndex = 7;
            cb_infer.SelectedIndexChanged += cb_infer_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label9.Location = new Point(7, 147);
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
            num_temperature.Location = new Point(11, 165);
            num_temperature.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            num_temperature.Name = "num_temperature";
            num_temperature.Size = new Size(180, 23);
            num_temperature.TabIndex = 17;
            num_temperature.ThousandsSeparator = true;
            num_temperature.Value = new decimal(new int[] { 7, 0, 0, 65536 });
            num_temperature.ValueChanged += num_temperature_ValueChanged;
            // 
            // collapseModel
            // 
            collapseModel.BackColor = Color.FromArgb(37, 38, 42);
            collapseModel.Controls.Add(num_maxresponse);
            collapseModel.Controls.Add(label7);
            collapseModel.Controls.Add(num_maxcontext);
            collapseModel.Controls.Add(label8);
            collapseModel.Controls.Add(bt_connect);
            collapseModel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapseModel.Location = new Point(0, 6);
            collapseModel.Name = "collapseModel";
            collapseModel.Padding = new Padding(8, 32, 8, 8);
            collapseModel.Size = new Size(200, 129);
            collapseModel.TabIndex = 27;
            collapseModel.Text = "Model";
            // 
            // num_maxresponse
            // 
            num_maxresponse.Font = new Font("Segoe UI", 9F);
            num_maxresponse.Increment = new decimal(new int[] { 32, 0, 0, 0 });
            num_maxresponse.Location = new Point(106, 61);
            num_maxresponse.Maximum = new decimal(new int[] { 16384, 0, 0, 0 });
            num_maxresponse.Name = "num_maxresponse";
            num_maxresponse.Size = new Size(85, 23);
            num_maxresponse.TabIndex = 18;
            num_maxresponse.ThousandsSeparator = true;
            num_maxresponse.Value = new decimal(new int[] { 512, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F);
            label7.Location = new Point(7, 43);
            label7.Name = "label7";
            label7.Size = new Size(73, 15);
            label7.TabIndex = 15;
            label7.Text = "Max Context";
            // 
            // num_maxcontext
            // 
            num_maxcontext.Font = new Font("Segoe UI", 9F);
            num_maxcontext.Increment = new decimal(new int[] { 512, 0, 0, 0 });
            num_maxcontext.Location = new Point(11, 61);
            num_maxcontext.Maximum = new decimal(new int[] { 16384, 0, 0, 0 });
            num_maxcontext.Minimum = new decimal(new int[] { 1024, 0, 0, 0 });
            num_maxcontext.Name = "num_maxcontext";
            num_maxcontext.Size = new Size(89, 23);
            num_maxcontext.TabIndex = 16;
            num_maxcontext.Value = new decimal(new int[] { 16384, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F);
            label8.Location = new Point(102, 43);
            label8.Name = "label8";
            label8.Size = new Size(76, 15);
            label8.TabIndex = 17;
            label8.Text = "Reply Length";
            // 
            // bt_connect
            // 
            bt_connect.BackColor = Color.DarkSeaGreen;
            bt_connect.Dock = DockStyle.Bottom;
            bt_connect.FlatStyle = FlatStyle.Flat;
            bt_connect.ForeColor = Color.Black;
            bt_connect.Location = new Point(8, 98);
            bt_connect.Name = "bt_connect";
            bt_connect.Size = new Size(184, 23);
            bt_connect.TabIndex = 19;
            bt_connect.Tag = "no-theme";
            bt_connect.Text = "Connect";
            bt_connect.UseVisualStyleBackColor = false;
            bt_connect.Click += bt_connectClick;
            // 
            // bt_impersonate
            // 
            bt_impersonate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_impersonate.BackColor = Color.LightSlateGray;
            bt_impersonate.FlatStyle = FlatStyle.Flat;
            bt_impersonate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_impersonate.ForeColor = Color.Black;
            bt_impersonate.Location = new Point(992, 931);
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
            web_chat.Location = new Point(206, 3);
            web_chat.Name = "web_chat";
            web_chat.Size = new Size(846, 922);
            web_chat.TabIndex = 6;
            web_chat.ZoomFactor = 1D;
            // 
            // bt_delete
            // 
            bt_delete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_delete.BackColor = Color.DarkRed;
            bt_delete.FlatStyle = FlatStyle.Flat;
            bt_delete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_delete.ForeColor = Color.Black;
            bt_delete.Location = new Point(992, 992);
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
            bt_reroll.BackColor = Color.DarkKhaki;
            bt_reroll.FlatStyle = FlatStyle.Flat;
            bt_reroll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_reroll.ForeColor = Color.Black;
            bt_reroll.Location = new Point(992, 962);
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
            bt_send.BackColor = Color.DarkSeaGreen;
            bt_send.FlatStyle = FlatStyle.Flat;
            bt_send.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_send.ForeColor = Color.Black;
            bt_send.Location = new Point(926, 931);
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
            ed_input.Location = new Point(206, 931);
            ed_input.Multiline = true;
            ed_input.Name = "ed_input";
            ed_input.ScrollBars = ScrollBars.Vertical;
            ed_input.Size = new Size(714, 86);
            ed_input.TabIndex = 2;
            ed_input.Tag = "no-theme";
            // 
            // collapsibleGroupBox1
            // 
            collapsibleGroupBox1.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox1.Controls.Add(mck_charsampler);
            collapsibleGroupBox1.Controls.Add(btSampleEditor);
            collapsibleGroupBox1.Controls.Add(mck_forceNames);
            collapsibleGroupBox1.Controls.Add(btInstructEdit);
            collapsibleGroupBox1.Controls.Add(label5);
            collapsibleGroupBox1.Controls.Add(num_temperature);
            collapsibleGroupBox1.Controls.Add(label9);
            collapsibleGroupBox1.Controls.Add(cb_infer);
            collapsibleGroupBox1.Controls.Add(label6);
            collapsibleGroupBox1.Controls.Add(cb_instruct);
            collapsibleGroupBox1.Controls.Add(mck_ragtothink);
            collapsibleGroupBox1.Controls.Add(mck_disablethink);
            collapsibleGroupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox1.Location = new Point(0, 143);
            collapsibleGroupBox1.Margin = new Padding(8);
            collapsibleGroupBox1.Name = "collapsibleGroupBox1";
            collapsibleGroupBox1.Padding = new Padding(8, 32, 8, 8);
            collapsibleGroupBox1.Size = new Size(200, 307);
            collapsibleGroupBox1.TabIndex = 27;
            collapsibleGroupBox1.Text = "Inference Settings";
            // 
            // mck_charsampler
            // 
            mck_charsampler.Dock = DockStyle.Bottom;
            mck_charsampler.Font = new Font("Segoe UI", 9F);
            mck_charsampler.Location = new Point(8, 195);
            mck_charsampler.Name = "mck_charsampler";
            mck_charsampler.Size = new Size(184, 26);
            mck_charsampler.TabIndex = 31;
            mck_charsampler.Text = "Use character's samplers";
            mck_charsampler.UseVisualStyleBackColor = true;
            // 
            // mck_forceNames
            // 
            mck_forceNames.Dock = DockStyle.Bottom;
            mck_forceNames.Font = new Font("Segoe UI", 9F);
            mck_forceNames.Location = new Point(8, 221);
            mck_forceNames.Name = "mck_forceNames";
            mck_forceNames.Size = new Size(184, 26);
            mck_forceNames.TabIndex = 0;
            mck_forceNames.Text = "Add names to prompt";
            mck_forceNames.UseVisualStyleBackColor = true;
            mck_forceNames.CheckedChanged += ck_forceNames_CheckedChanged;
            // 
            // mck_ragtothink
            // 
            mck_ragtothink.Dock = DockStyle.Bottom;
            mck_ragtothink.Font = new Font("Segoe UI", 9F);
            mck_ragtothink.Location = new Point(8, 247);
            mck_ragtothink.Name = "mck_ragtothink";
            mck_ragtothink.Size = new Size(184, 26);
            mck_ragtothink.TabIndex = 32;
            mck_ragtothink.Text = "Put context in think block";
            mck_ragtothink.UseVisualStyleBackColor = true;
            mck_ragtothink.CheckedChanged += ck_ragtothink_CheckedChanged;
            // 
            // mck_disablethink
            // 
            mck_disablethink.Dock = DockStyle.Bottom;
            mck_disablethink.Font = new Font("Segoe UI", 9F);
            mck_disablethink.Location = new Point(8, 273);
            mck_disablethink.Name = "mck_disablethink";
            mck_disablethink.Size = new Size(184, 26);
            mck_disablethink.TabIndex = 33;
            mck_disablethink.Text = "Disable Thinking";
            mck_disablethink.UseVisualStyleBackColor = true;
            mck_disablethink.CheckedChanged += ck_disablethink_CheckedChanged;
            // 
            // panLeft
            // 
            panLeft.Controls.Add(cboxVLM);
            panLeft.Controls.Add(collapsibleGroupBox2);
            panLeft.Controls.Add(collapsibleGroupBox1);
            panLeft.Controls.Add(collapseModel);
            panLeft.Dock = DockStyle.Left;
            panLeft.Location = new Point(0, 0);
            panLeft.Name = "panLeft";
            panLeft.Padding = new Padding(0, 6, 0, 6);
            panLeft.Size = new Size(200, 1020);
            panLeft.TabIndex = 28;
            // 
            // cboxVLM
            // 
            cboxVLM.BackColor = Color.FromArgb(37, 38, 42);
            cboxVLM.Controls.Add(label1);
            cboxVLM.Controls.Add(pictEmbed);
            cboxVLM.Controls.Add(bt_clearimg);
            cboxVLM.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cboxVLM.Location = new Point(0, 800);
            cboxVLM.Name = "cboxVLM";
            cboxVLM.Padding = new Padding(8, 32, 8, 8);
            cboxVLM.Size = new Size(200, 160);
            cboxVLM.TabIndex = 29;
            cboxVLM.Text = "Visual Language Models";
            // 
            // collapsibleGroupBox2
            // 
            collapsibleGroupBox2.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox2.Controls.Add(mck_agentmode);
            collapsibleGroupBox2.Controls.Add(mck_onlinerag);
            collapsibleGroupBox2.Controls.Add(panel2);
            collapsibleGroupBox2.Controls.Add(mckNatMem);
            collapsibleGroupBox2.Controls.Add(mck_ragenabled);
            collapsibleGroupBox2.Controls.Add(mck_worldinfo);
            collapsibleGroupBox2.Controls.Add(mck_sessionmemory);
            collapsibleGroupBox2.Controls.Add(btVectorSearch);
            collapsibleGroupBox2.Controls.Add(btRawLog);
            collapsibleGroupBox2.Controls.Add(button2);
            collapsibleGroupBox2.Controls.Add(btWorldEditor);
            collapsibleGroupBox2.Controls.Add(btMainSettings);
            collapsibleGroupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox2.Location = new Point(0, 458);
            collapsibleGroupBox2.Name = "collapsibleGroupBox2";
            collapsibleGroupBox2.Padding = new Padding(8, 32, 8, 8);
            collapsibleGroupBox2.Size = new Size(200, 334);
            collapsibleGroupBox2.TabIndex = 28;
            collapsibleGroupBox2.Text = "Memory and Settings";
            // 
            // mck_agentmode
            // 
            mck_agentmode.Dock = DockStyle.Top;
            mck_agentmode.Font = new Font("Segoe UI", 9F);
            mck_agentmode.Location = new Point(8, 172);
            mck_agentmode.Name = "mck_agentmode";
            mck_agentmode.Size = new Size(184, 26);
            mck_agentmode.TabIndex = 48;
            mck_agentmode.Text = "Background Agent";
            mck_agentmode.UseVisualStyleBackColor = true;
            mck_agentmode.CheckedChanged += ck_agentmode_CheckedChanged;
            // 
            // mck_onlinerag
            // 
            mck_onlinerag.Dock = DockStyle.Top;
            mck_onlinerag.Font = new Font("Segoe UI", 9F);
            mck_onlinerag.Location = new Point(8, 146);
            mck_onlinerag.Name = "mck_onlinerag";
            mck_onlinerag.Size = new Size(184, 26);
            mck_onlinerag.TabIndex = 47;
            mck_onlinerag.Text = "Web Search";
            mck_onlinerag.UseVisualStyleBackColor = true;
            mck_onlinerag.CheckedChanged += ck_onlinerag_CheckedChanged;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(8, 136);
            panel2.Margin = new Padding(8);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(8);
            panel2.Size = new Size(184, 10);
            panel2.TabIndex = 46;
            // 
            // mckNatMem
            // 
            mckNatMem.Dock = DockStyle.Top;
            mckNatMem.Font = new Font("Segoe UI", 9F);
            mckNatMem.Location = new Point(8, 110);
            mckNatMem.Name = "mckNatMem";
            mckNatMem.Size = new Size(184, 26);
            mckNatMem.TabIndex = 45;
            mckNatMem.Text = "Contextual Memory";
            mckNatMem.UseVisualStyleBackColor = true;
            mckNatMem.CheckedChanged += ckNatMem_CheckedChanged;
            // 
            // mck_ragenabled
            // 
            mck_ragenabled.Dock = DockStyle.Top;
            mck_ragenabled.Font = new Font("Segoe UI", 9F);
            mck_ragenabled.Location = new Point(8, 84);
            mck_ragenabled.Name = "mck_ragenabled";
            mck_ragenabled.Size = new Size(184, 26);
            mck_ragenabled.TabIndex = 44;
            mck_ragenabled.Text = "Vector Database";
            mck_ragenabled.UseVisualStyleBackColor = true;
            mck_ragenabled.CheckedChanged += ck_ragenabled_CheckedChanged;
            // 
            // mck_worldinfo
            // 
            mck_worldinfo.Dock = DockStyle.Top;
            mck_worldinfo.Font = new Font("Segoe UI", 9F);
            mck_worldinfo.Location = new Point(8, 58);
            mck_worldinfo.Name = "mck_worldinfo";
            mck_worldinfo.Size = new Size(184, 26);
            mck_worldinfo.TabIndex = 43;
            mck_worldinfo.Text = "World Info Memory";
            mck_worldinfo.UseVisualStyleBackColor = true;
            mck_worldinfo.CheckedChanged += ck_worldinfo_CheckedChanged;
            // 
            // mck_sessionmemory
            // 
            mck_sessionmemory.Dock = DockStyle.Top;
            mck_sessionmemory.Font = new Font("Segoe UI", 9F);
            mck_sessionmemory.Location = new Point(8, 32);
            mck_sessionmemory.Name = "mck_sessionmemory";
            mck_sessionmemory.Size = new Size(184, 26);
            mck_sessionmemory.TabIndex = 42;
            mck_sessionmemory.Text = "Session Memory";
            mck_sessionmemory.UseVisualStyleBackColor = true;
            mck_sessionmemory.CheckedChanged += ck_sessionmemory_CheckedChanged;
            // 
            // panRight
            // 
            panRight.Controls.Add(button3);
            panRight.Controls.Add(button1);
            panRight.Controls.Add(collapsibleGroupBox3);
            panRight.Dock = DockStyle.Right;
            panRight.Location = new Point(1061, 0);
            panRight.Name = "panRight";
            panRight.Padding = new Padding(0, 6, 0, 6);
            panRight.Size = new Size(200, 1020);
            panRight.TabIndex = 29;
            // 
            // button3
            // 
            button3.Location = new Point(0, 412);
            button3.Name = "button3";
            button3.Size = new Size(200, 23);
            button3.TabIndex = 28;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // collapsibleGroupBox3
            // 
            collapsibleGroupBox3.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox3.Controls.Add(mck_ttstoggle);
            collapsibleGroupBox3.Controls.Add(mck_senseoftime);
            collapsibleGroupBox3.Controls.Add(mck_caninitchat);
            collapsibleGroupBox3.Controls.Add(btSysPrompt);
            collapsibleGroupBox3.Controls.Add(cb_user);
            collapsibleGroupBox3.Controls.Add(cb_sysprompt);
            collapsibleGroupBox3.Controls.Add(label4);
            collapsibleGroupBox3.Controls.Add(bt_editchar);
            collapsibleGroupBox3.Controls.Add(label11);
            collapsibleGroupBox3.Controls.Add(label3);
            collapsibleGroupBox3.Controls.Add(cb_bot);
            collapsibleGroupBox3.Controls.Add(panel1);
            collapsibleGroupBox3.Controls.Add(btChatHistory);
            collapsibleGroupBox3.Controls.Add(bt_scenario);
            collapsibleGroupBox3.Controls.Add(bt_newsession);
            collapsibleGroupBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox3.Location = new Point(0, 6);
            collapsibleGroupBox3.Name = "collapsibleGroupBox3";
            collapsibleGroupBox3.Padding = new Padding(8, 32, 8, 8);
            collapsibleGroupBox3.Size = new Size(200, 367);
            collapsibleGroupBox3.TabIndex = 27;
            collapsibleGroupBox3.Text = "Chat Settings";
            // 
            // mck_ttstoggle
            // 
            mck_ttstoggle.Dock = DockStyle.Bottom;
            mck_ttstoggle.Font = new Font("Segoe UI", 9F);
            mck_ttstoggle.Location = new Point(8, 197);
            mck_ttstoggle.Name = "mck_ttstoggle";
            mck_ttstoggle.Size = new Size(184, 26);
            mck_ttstoggle.TabIndex = 36;
            mck_ttstoggle.Text = "Text To Speech";
            mck_ttstoggle.UseVisualStyleBackColor = true;
            mck_ttstoggle.CheckedChanged += ck_ttstoggle_CheckedChanged;
            // 
            // mck_senseoftime
            // 
            mck_senseoftime.Dock = DockStyle.Bottom;
            mck_senseoftime.Font = new Font("Segoe UI", 9F);
            mck_senseoftime.Location = new Point(8, 223);
            mck_senseoftime.Name = "mck_senseoftime";
            mck_senseoftime.Size = new Size(184, 26);
            mck_senseoftime.TabIndex = 35;
            mck_senseoftime.Text = "Sense of Time";
            mck_senseoftime.UseVisualStyleBackColor = true;
            mck_senseoftime.CheckedChanged += ck_senseoftime_CheckedChanged;
            // 
            // mck_caninitchat
            // 
            mck_caninitchat.Dock = DockStyle.Bottom;
            mck_caninitchat.Font = new Font("Segoe UI", 9F);
            mck_caninitchat.Location = new Point(8, 249);
            mck_caninitchat.Name = "mck_caninitchat";
            mck_caninitchat.Size = new Size(184, 26);
            mck_caninitchat.TabIndex = 34;
            mck_caninitchat.Text = "Bot can initiate chat";
            mck_caninitchat.UseVisualStyleBackColor = true;
            mck_caninitchat.CheckedChanged += ck_caninit_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(8, 275);
            panel1.Margin = new Padding(8);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(8);
            panel1.Size = new Size(184, 10);
            panel1.TabIndex = 47;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(37, 38, 42);
            ClientSize = new Size(1261, 1042);
            Controls.Add(panRight);
            Controls.Add(panLeft);
            Controls.Add(bt_impersonate);
            Controls.Add(web_chat);
            Controls.Add(bt_delete);
            Controls.Add(bt_reroll);
            Controls.Add(bt_send);
            Controls.Add(ed_input);
            Controls.Add(statusbar);
            ForeColor = Color.WhiteSmoke;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "w(AI)fu";
            FormClosing += MainForm_FormClosing;
            statusbar.ResumeLayout(false);
            statusbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictEmbed).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_temperature).EndInit();
            collapseModel.ResumeLayout(false);
            collapseModel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_maxresponse).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_maxcontext).EndInit();
            ((System.ComponentModel.ISupportInitialize)web_chat).EndInit();
            collapsibleGroupBox1.ResumeLayout(false);
            collapsibleGroupBox1.PerformLayout();
            panLeft.ResumeLayout(false);
            cboxVLM.ResumeLayout(false);
            collapsibleGroupBox2.ResumeLayout(false);
            panRight.ResumeLayout(false);
            collapsibleGroupBox3.ResumeLayout(false);
            collapsibleGroupBox3.PerformLayout();
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
        private Button bt_editchar;
        private Button bt_scenario;
        private Label label3;
        private ComboBox cb_bot;
        private Label label4;
        private ComboBox cb_user;
        private Button bt_newsession;
        private Label label11;
        private ComboBox cb_sysprompt;
        private Label label5;
        private ComboBox cb_instruct;
        private Label label6;
        private ComboBox cb_infer;
        private Label label9;
        private NumericUpDown num_temperature;
        private Button btInstructEdit;
        private Button btSysPrompt;
        private Button btSampleEditor;
        private Button btMainSettings;
        private Button btWorldEditor;
        private Button btChatHistory;
        private Button btVectorSearch;
        private Button btRawLog;
        private Button button1;
        private Button button2;
        private Controls.CollapsibleGroupBox collapseModel;
        private NumericUpDown num_maxresponse;
        private Label label7;
        private NumericUpDown num_maxcontext;
        private Label label8;
        private Button bt_connect;
        private Controls.CollapsibleGroupBox collapsibleGroupBox1;
        private Controls.VerticalStackPanel panLeft;
        private Label label1;
        private PictureBox pictEmbed;
        private Button bt_clearimg;
        private Controls.ModernCheckBox mck_forceNames;
        private Controls.ModernCheckBox mck_charsampler;
        private Controls.ModernCheckBox mck_ragtothink;
        private Controls.ModernCheckBox mck_disablethink;
        private Controls.CollapsibleGroupBox collapsibleGroupBox2;
        private Controls.ModernCheckBox mck_sessionmemory;
        private Controls.ModernCheckBox mckNatMem;
        private Controls.ModernCheckBox mck_ragenabled;
        private Controls.ModernCheckBox mck_worldinfo;
        private Controls.ModernCheckBox mck_agentmode;
        private Controls.ModernCheckBox mck_onlinerag;
        private Panel panel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private Controls.CollapsibleGroupBox cboxVLM;
        private Controls.VerticalStackPanel panRight;
        private Controls.CollapsibleGroupBox collapsibleGroupBox3;
        private Controls.ModernCheckBox mck_ttstoggle;
        private Controls.ModernCheckBox mck_senseoftime;
        private Controls.ModernCheckBox mck_caninitchat;
        private Button button3;
        private Panel panel1;
    }
}
