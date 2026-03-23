using LLama.Common;
using LetheChat.Controls;

namespace LetheChat
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
            btChatHistory = new Button();
            btSysPrompt = new Button();
            cb_user = new ModernComboBox();
            label4 = new Label();
            bt_editchar = new Button();
            bt_scenario = new Button();
            label3 = new Label();
            cb_bot = new ModernComboBox();
            bt_newsession = new Button();
            label11 = new Label();
            cb_sysprompt = new ModernComboBox();
            label1 = new Label();
            pictEmbed = new PictureBox();
            bt_clearimg = new Button();
            button2 = new Button();
            btRawLog = new Button();
            btWorldEditor = new Button();
            btMainSettings = new Button();
            btSampleEditor = new Button();
            btInstructEdit = new Button();
            label5 = new Label();
            cb_instruct = new ModernComboBox();
            label6 = new Label();
            cb_infer = new ModernComboBox();
            label9 = new Label();
            num_temperature = new ModernNumericUpDown();
            collapseModel = new CollapsibleGroupBox();
            num_maxresponse = new ModernNumericUpDown();
            label7 = new Label();
            num_maxcontext = new ModernNumericUpDown();
            label8 = new Label();
            bt_refresh = new Button();
            panel4 = new Panel();
            bt_llama = new Button();
            bt_backend = new Button();
            bt_impersonate = new Button();
            web_chat = new Microsoft.Web.WebView2.WinForms.WebView2();
            bt_delete = new Button();
            bt_reroll = new Button();
            bt_send = new Button();
            ed_input = new SpellCheckedTextBox();
            collapsibleGroupBox1 = new CollapsibleGroupBox();
            mck_charsampler = new ModernCheckBox();
            mck_forceNames = new ModernCheckBox();
            mck_ragtothink = new ModernCheckBox();
            mck_disablethink = new ModernCheckBox();
            panLeft = new VerticalStackPanel();
            cboxVLM = new CollapsibleGroupBox();
            collapsibleGroupBox2 = new CollapsibleGroupBox();
            panel5 = new Panel();
            mck_agentmode = new ModernCheckBox();
            ckToolCalls = new ModernCheckBox();
            mck_onlinerag = new ModernCheckBox();
            panel2 = new Panel();
            mckNatMem = new ModernCheckBox();
            mck_guidance = new ModernCheckBox();
            mck_ragenabled = new ModernCheckBox();
            mck_worldinfo = new ModernCheckBox();
            mck_sessionmemory = new ModernCheckBox();
            panRight = new VerticalStackPanel();
            collapsibleGroupBox4 = new CollapsibleGroupBox();
            button4 = new Button();
            button3 = new Button();
            btRunAgent = new Button();
            panel9 = new Panel();
            collapsibleMenus = new CollapsibleGroupBox();
            panel3 = new Panel();
            cboxGroup = new CollapsibleGroupBox();
            cbGroupSwitch = new ModernComboBox();
            label2 = new Label();
            panel6 = new Panel();
            lstGroupMembers = new ModernCheckedListBox();
            panel7 = new Panel();
            ckGroupToggle = new ModernCheckBox();
            collapsibleGroupBox3 = new CollapsibleGroupBox();
            mck_ttstoggle = new ModernCheckBox();
            mck_caninitchat = new ModernCheckBox();
            panel1 = new Panel();
            statusbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictEmbed).BeginInit();
            collapseModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)web_chat).BeginInit();
            collapsibleGroupBox1.SuspendLayout();
            panLeft.SuspendLayout();
            cboxVLM.SuspendLayout();
            collapsibleGroupBox2.SuspendLayout();
            panRight.SuspendLayout();
            collapsibleGroupBox4.SuspendLayout();
            collapsibleMenus.SuspendLayout();
            cboxGroup.SuspendLayout();
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
            statusbar.Location = new Point(0, 981);
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
            toolStripStatusLabel3.BorderStyle = Border3DStyle.SunkenOuter;
            toolStripStatusLabel3.DisplayStyle = ToolStripItemDisplayStyle.Text;
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
            // btChatHistory
            // 
            btChatHistory.AutoSize = true;
            btChatHistory.BackColor = Color.DarkKhaki;
            btChatHistory.Dock = DockStyle.Bottom;
            btChatHistory.FlatStyle = FlatStyle.Flat;
            btChatHistory.Font = new Font("Segoe UI", 9F);
            btChatHistory.ForeColor = Color.Black;
            btChatHistory.Location = new Point(8, 245);
            btChatHistory.Name = "btChatHistory";
            btChatHistory.Size = new Size(184, 27);
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
            btSysPrompt.Location = new Point(134, 33);
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
            cb_user.BackColor = Color.FromArgb(64, 64, 64);
            cb_user.DropDownHeight = 620;
            cb_user.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cb_user.Location = new Point(8, 155);
            cb_user.MaxDropDownItems = 25;
            cb_user.Name = "cb_user";
            cb_user.Padding = new Padding(1);
            cb_user.Size = new Size(184, 23);
            cb_user.TabIndex = 3;
            cb_user.SelectedIndexChanged += cb_user_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            label4.Location = new Point(8, 137);
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
            bt_editchar.Location = new Point(134, 85);
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
            bt_scenario.AutoSize = true;
            bt_scenario.BackColor = Color.DarkKhaki;
            bt_scenario.Dock = DockStyle.Bottom;
            bt_scenario.FlatStyle = FlatStyle.Flat;
            bt_scenario.Font = new Font("Segoe UI", 9F);
            bt_scenario.ForeColor = Color.Black;
            bt_scenario.Location = new Point(8, 272);
            bt_scenario.Name = "bt_scenario";
            bt_scenario.Size = new Size(184, 27);
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
            label3.Location = new Point(8, 92);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 0;
            label3.Text = "Bot Persona";
            // 
            // cb_bot
            // 
            cb_bot.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_bot.BackColor = Color.FromArgb(64, 64, 64);
            cb_bot.DropDownHeight = 620;
            cb_bot.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cb_bot.Location = new Point(8, 111);
            cb_bot.MaxDropDownItems = 25;
            cb_bot.Name = "cb_bot";
            cb_bot.Padding = new Padding(1);
            cb_bot.Size = new Size(184, 23);
            cb_bot.TabIndex = 1;
            cb_bot.SelectedIndexChanged += cb_bot_SelectedIndexChanged;
            // 
            // bt_newsession
            // 
            bt_newsession.AutoSize = true;
            bt_newsession.BackColor = Color.DarkSeaGreen;
            bt_newsession.Dock = DockStyle.Bottom;
            bt_newsession.FlatStyle = FlatStyle.Flat;
            bt_newsession.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_newsession.ForeColor = Color.Black;
            bt_newsession.Location = new Point(8, 299);
            bt_newsession.Name = "bt_newsession";
            bt_newsession.Size = new Size(184, 27);
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
            label11.Location = new Point(8, 38);
            label11.Name = "label11";
            label11.Size = new Size(85, 15);
            label11.TabIndex = 18;
            label11.Text = "System Prompt";
            // 
            // cb_sysprompt
            // 
            cb_sysprompt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_sysprompt.BackColor = Color.FromArgb(64, 64, 64);
            cb_sysprompt.DropDownHeight = 620;
            cb_sysprompt.Font = new Font("Segoe UI", 9F);
            cb_sysprompt.Location = new Point(8, 56);
            cb_sysprompt.MaxDropDownItems = 25;
            cb_sysprompt.Name = "cb_sysprompt";
            cb_sysprompt.Padding = new Padding(1);
            cb_sysprompt.Size = new Size(184, 23);
            cb_sysprompt.TabIndex = 19;
            cb_sysprompt.SelectedIndexChanged += cb_sysprompt_SelectionIndexChanged;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F);
            label1.Location = new Point(106, 35);
            label1.Name = "label1";
            label1.Size = new Size(82, 69);
            label1.TabIndex = 35;
            label1.Text = "Drag && drop images here <<--";
            // 
            // pictEmbed
            // 
            pictEmbed.BackColor = Color.Silver;
            pictEmbed.BorderStyle = BorderStyle.FixedSingle;
            pictEmbed.Location = new Point(11, 35);
            pictEmbed.Name = "pictEmbed";
            pictEmbed.Size = new Size(86, 84);
            pictEmbed.SizeMode = PictureBoxSizeMode.StretchImage;
            pictEmbed.TabIndex = 33;
            pictEmbed.TabStop = false;
            // 
            // bt_clearimg
            // 
            bt_clearimg.AutoSize = true;
            bt_clearimg.Dock = DockStyle.Bottom;
            bt_clearimg.FlatStyle = FlatStyle.Flat;
            bt_clearimg.Location = new Point(8, 126);
            bt_clearimg.Margin = new Padding(0);
            bt_clearimg.Name = "bt_clearimg";
            bt_clearimg.Size = new Size(184, 27);
            bt_clearimg.TabIndex = 34;
            bt_clearimg.Tag = "";
            bt_clearimg.Text = "Clear";
            bt_clearimg.UseVisualStyleBackColor = false;
            bt_clearimg.Click += bt_clearimg_Click;
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.BackColor = Color.DarkKhaki;
            button2.Dock = DockStyle.Top;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 9F);
            button2.ForeColor = Color.Black;
            button2.Location = new Point(8, 260);
            button2.Name = "button2";
            button2.Size = new Size(184, 27);
            button2.TabIndex = 41;
            button2.Tag = "no-theme";
            button2.Text = "Persona's Brain";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // btRawLog
            // 
            btRawLog.AutoSize = true;
            btRawLog.BackColor = Color.DarkKhaki;
            btRawLog.Dock = DockStyle.Top;
            btRawLog.FlatStyle = FlatStyle.Flat;
            btRawLog.Font = new Font("Segoe UI", 9F);
            btRawLog.ForeColor = Color.Black;
            btRawLog.Location = new Point(8, 42);
            btRawLog.Name = "btRawLog";
            btRawLog.Size = new Size(184, 27);
            btRawLog.TabIndex = 40;
            btRawLog.Tag = "no-theme";
            btRawLog.Text = "Logs and Prompts";
            btRawLog.UseVisualStyleBackColor = false;
            btRawLog.Click += btRawLog_Click;
            // 
            // btWorldEditor
            // 
            btWorldEditor.AutoSize = true;
            btWorldEditor.BackColor = Color.DarkKhaki;
            btWorldEditor.Dock = DockStyle.Top;
            btWorldEditor.FlatStyle = FlatStyle.Flat;
            btWorldEditor.Font = new Font("Segoe UI", 9F);
            btWorldEditor.ForeColor = Color.Black;
            btWorldEditor.Location = new Point(8, 69);
            btWorldEditor.Name = "btWorldEditor";
            btWorldEditor.Size = new Size(184, 27);
            btWorldEditor.TabIndex = 39;
            btWorldEditor.Tag = "no-theme";
            btWorldEditor.Text = "WorldInfo Editor";
            btWorldEditor.UseVisualStyleBackColor = false;
            btWorldEditor.Click += btWorldEditor_Click;
            // 
            // btMainSettings
            // 
            btMainSettings.AutoSize = true;
            btMainSettings.BackColor = Color.DarkSeaGreen;
            btMainSettings.Dock = DockStyle.Top;
            btMainSettings.FlatStyle = FlatStyle.Flat;
            btMainSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btMainSettings.ForeColor = Color.Black;
            btMainSettings.Location = new Point(8, 96);
            btMainSettings.Name = "btMainSettings";
            btMainSettings.Size = new Size(184, 27);
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
            btSampleEditor.Location = new Point(127, 92);
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
            cb_instruct.BackColor = Color.FromArgb(64, 64, 64);
            cb_instruct.DropDownHeight = 620;
            cb_instruct.Font = new Font("Segoe UI", 9F);
            cb_instruct.Location = new Point(11, 63);
            cb_instruct.MaxDropDownItems = 25;
            cb_instruct.Name = "cb_instruct";
            cb_instruct.Padding = new Padding(1);
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
            cb_infer.BackColor = Color.FromArgb(64, 64, 64);
            cb_infer.DropDownHeight = 620;
            cb_infer.Font = new Font("Segoe UI", 9F);
            cb_infer.Location = new Point(11, 118);
            cb_infer.MaxDropDownItems = 25;
            cb_infer.Name = "cb_infer";
            cb_infer.Padding = new Padding(1);
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
            num_temperature.BackColor = Color.FromArgb(64, 64, 64);
            num_temperature.DecimalPlaces = 2;
            num_temperature.Font = new Font("Segoe UI", 9F);
            num_temperature.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_temperature.Location = new Point(11, 165);
            num_temperature.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            num_temperature.Name = "num_temperature";
            num_temperature.Padding = new Padding(1);
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
            collapseModel.Controls.Add(bt_refresh);
            collapseModel.Controls.Add(panel4);
            collapseModel.Controls.Add(bt_llama);
            collapseModel.Controls.Add(bt_backend);
            collapseModel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapseModel.Location = new Point(0, 6);
            collapseModel.Name = "collapseModel";
            collapseModel.Padding = new Padding(8, 32, 8, 8);
            collapseModel.Size = new Size(200, 185);
            collapseModel.TabIndex = 27;
            collapseModel.Text = "Model";
            // 
            // num_maxresponse
            // 
            num_maxresponse.BackColor = Color.FromArgb(64, 64, 64);
            num_maxresponse.Font = new Font("Segoe UI", 9F);
            num_maxresponse.Increment = new decimal(new int[] { 32, 0, 0, 0 });
            num_maxresponse.Location = new Point(106, 56);
            num_maxresponse.Maximum = new decimal(new int[] { 16384, 0, 0, 0 });
            num_maxresponse.Name = "num_maxresponse";
            num_maxresponse.Padding = new Padding(1);
            num_maxresponse.Size = new Size(85, 23);
            num_maxresponse.TabIndex = 18;
            num_maxresponse.ThousandsSeparator = true;
            num_maxresponse.Value = new decimal(new int[] { 512, 0, 0, 0 });
            num_maxresponse.ValueChanged += num_maxresponse_ValueChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F);
            label7.Location = new Point(11, 38);
            label7.Name = "label7";
            label7.Size = new Size(73, 15);
            label7.TabIndex = 15;
            label7.Text = "Max Context";
            // 
            // num_maxcontext
            // 
            num_maxcontext.BackColor = Color.FromArgb(64, 64, 64);
            num_maxcontext.Font = new Font("Segoe UI", 9F);
            num_maxcontext.Increment = new decimal(new int[] { 512, 0, 0, 0 });
            num_maxcontext.Location = new Point(8, 56);
            num_maxcontext.Maximum = new decimal(new int[] { 16384, 0, 0, 0 });
            num_maxcontext.Minimum = new decimal(new int[] { 1024, 0, 0, 0 });
            num_maxcontext.Name = "num_maxcontext";
            num_maxcontext.Padding = new Padding(1);
            num_maxcontext.Size = new Size(89, 23);
            num_maxcontext.TabIndex = 16;
            num_maxcontext.Value = new decimal(new int[] { 16384, 0, 0, 0 });
            num_maxcontext.ValueChanged += num_maxcontext_ValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F);
            label8.Location = new Point(106, 38);
            label8.Name = "label8";
            label8.Size = new Size(76, 15);
            label8.TabIndex = 17;
            label8.Text = "Reply Length";
            // 
            // bt_refresh
            // 
            bt_refresh.AutoSize = true;
            bt_refresh.BackColor = Color.DarkSeaGreen;
            bt_refresh.Dock = DockStyle.Bottom;
            bt_refresh.FlatStyle = FlatStyle.Flat;
            bt_refresh.ForeColor = Color.Black;
            bt_refresh.Location = new Point(8, 86);
            bt_refresh.Name = "bt_refresh";
            bt_refresh.Size = new Size(184, 27);
            bt_refresh.TabIndex = 19;
            bt_refresh.Tag = "no-theme";
            bt_refresh.Text = "Refresh";
            bt_refresh.UseVisualStyleBackColor = false;
            bt_refresh.Click += bt_connectClick;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(8, 113);
            panel4.Margin = new Padding(8);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(8);
            panel4.Size = new Size(184, 10);
            panel4.TabIndex = 56;
            // 
            // bt_llama
            // 
            bt_llama.AutoSize = true;
            bt_llama.BackColor = Color.SteelBlue;
            bt_llama.Dock = DockStyle.Bottom;
            bt_llama.FlatAppearance.BorderColor = Color.Black;
            bt_llama.FlatStyle = FlatStyle.Flat;
            bt_llama.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_llama.ForeColor = Color.White;
            bt_llama.Location = new Point(8, 123);
            bt_llama.Name = "bt_llama";
            bt_llama.Size = new Size(184, 27);
            bt_llama.TabIndex = 55;
            bt_llama.Tag = "no-theme";
            bt_llama.Text = "Managed llama.cpp";
            bt_llama.UseVisualStyleBackColor = false;
            bt_llama.Click += bt_llama_Click;
            // 
            // bt_backend
            // 
            bt_backend.AutoSize = true;
            bt_backend.BackColor = Color.SteelBlue;
            bt_backend.Dock = DockStyle.Bottom;
            bt_backend.FlatAppearance.BorderColor = Color.Black;
            bt_backend.FlatStyle = FlatStyle.Flat;
            bt_backend.ForeColor = Color.White;
            bt_backend.Location = new Point(8, 150);
            bt_backend.Name = "bt_backend";
            bt_backend.Size = new Size(184, 27);
            bt_backend.TabIndex = 20;
            bt_backend.Tag = "no-theme";
            bt_backend.Text = "Connect to Backend";
            bt_backend.UseVisualStyleBackColor = false;
            bt_backend.Click += bt_backend_Click;
            // 
            // bt_impersonate
            // 
            bt_impersonate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_impersonate.BackColor = Color.LightSlateGray;
            bt_impersonate.FlatStyle = FlatStyle.Flat;
            bt_impersonate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_impersonate.ForeColor = Color.Black;
            bt_impersonate.Location = new Point(992, 892);
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
            web_chat.Size = new Size(846, 883);
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
            bt_delete.Location = new Point(992, 953);
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
            bt_reroll.Location = new Point(992, 923);
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
            bt_send.Location = new Point(926, 892);
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
            ed_input.Location = new Point(206, 892);
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
            collapsibleGroupBox1.Location = new Point(0, 199);
            collapsibleGroupBox1.Margin = new Padding(8);
            collapsibleGroupBox1.Name = "collapsibleGroupBox1";
            collapsibleGroupBox1.Padding = new Padding(8, 32, 8, 8);
            collapsibleGroupBox1.Size = new Size(200, 301);
            collapsibleGroupBox1.TabIndex = 27;
            collapsibleGroupBox1.Text = "Inference Settings";
            // 
            // mck_charsampler
            // 
            mck_charsampler.Dock = DockStyle.Bottom;
            mck_charsampler.Font = new Font("Segoe UI", 9F);
            mck_charsampler.Location = new Point(8, 189);
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
            mck_forceNames.Location = new Point(8, 215);
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
            mck_ragtothink.Location = new Point(8, 241);
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
            mck_disablethink.Location = new Point(8, 267);
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
            panLeft.Size = new Size(200, 981);
            panLeft.TabIndex = 28;
            // 
            // cboxVLM
            // 
            cboxVLM.BackColor = Color.FromArgb(37, 38, 42);
            cboxVLM.Controls.Add(label1);
            cboxVLM.Controls.Add(pictEmbed);
            cboxVLM.Controls.Add(bt_clearimg);
            cboxVLM.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cboxVLM.Location = new Point(0, 806);
            cboxVLM.Name = "cboxVLM";
            cboxVLM.Padding = new Padding(8, 32, 8, 8);
            cboxVLM.Size = new Size(200, 161);
            cboxVLM.TabIndex = 29;
            cboxVLM.Text = "Visual Language Models";
            // 
            // collapsibleGroupBox2
            // 
            collapsibleGroupBox2.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox2.Controls.Add(button2);
            collapsibleGroupBox2.Controls.Add(panel5);
            collapsibleGroupBox2.Controls.Add(mck_agentmode);
            collapsibleGroupBox2.Controls.Add(ckToolCalls);
            collapsibleGroupBox2.Controls.Add(mck_onlinerag);
            collapsibleGroupBox2.Controls.Add(panel2);
            collapsibleGroupBox2.Controls.Add(mckNatMem);
            collapsibleGroupBox2.Controls.Add(mck_guidance);
            collapsibleGroupBox2.Controls.Add(mck_ragenabled);
            collapsibleGroupBox2.Controls.Add(mck_worldinfo);
            collapsibleGroupBox2.Controls.Add(mck_sessionmemory);
            collapsibleGroupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox2.Location = new Point(0, 508);
            collapsibleGroupBox2.Name = "collapsibleGroupBox2";
            collapsibleGroupBox2.Padding = new Padding(8, 32, 8, 8);
            collapsibleGroupBox2.Size = new Size(200, 290);
            collapsibleGroupBox2.TabIndex = 28;
            collapsibleGroupBox2.Text = "Memory and Tools";
            // 
            // panel5
            // 
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(8, 250);
            panel5.Margin = new Padding(8);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(8);
            panel5.Size = new Size(184, 10);
            panel5.TabIndex = 49;
            // 
            // mck_agentmode
            // 
            mck_agentmode.Dock = DockStyle.Top;
            mck_agentmode.Font = new Font("Segoe UI", 9F);
            mck_agentmode.Location = new Point(8, 224);
            mck_agentmode.Name = "mck_agentmode";
            mck_agentmode.Size = new Size(184, 26);
            mck_agentmode.TabIndex = 48;
            mck_agentmode.Text = "Background Agent";
            mck_agentmode.UseVisualStyleBackColor = true;
            mck_agentmode.CheckedChanged += ck_agentmode_CheckedChanged;
            // 
            // ckToolCalls
            // 
            ckToolCalls.Dock = DockStyle.Top;
            ckToolCalls.Font = new Font("Segoe UI", 9F);
            ckToolCalls.Location = new Point(8, 198);
            ckToolCalls.Name = "ckToolCalls";
            ckToolCalls.Size = new Size(184, 26);
            ckToolCalls.TabIndex = 51;
            ckToolCalls.Text = "Allow Tool-calling";
            ckToolCalls.UseVisualStyleBackColor = true;
            ckToolCalls.CheckedChanged += ckToolCalls_CheckedChanged;
            // 
            // mck_onlinerag
            // 
            mck_onlinerag.Dock = DockStyle.Top;
            mck_onlinerag.Font = new Font("Segoe UI", 9F);
            mck_onlinerag.Location = new Point(8, 172);
            mck_onlinerag.Name = "mck_onlinerag";
            mck_onlinerag.Size = new Size(184, 26);
            mck_onlinerag.TabIndex = 47;
            mck_onlinerag.Text = "Allow Web Search";
            mck_onlinerag.UseVisualStyleBackColor = true;
            mck_onlinerag.CheckedChanged += ck_onlinerag_CheckedChanged;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(8, 162);
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
            mckNatMem.Location = new Point(8, 136);
            mckNatMem.Name = "mckNatMem";
            mckNatMem.Size = new Size(184, 26);
            mckNatMem.TabIndex = 45;
            mckNatMem.Text = "Contextual Inserts";
            mckNatMem.UseVisualStyleBackColor = true;
            mckNatMem.CheckedChanged += ckNatMem_CheckedChanged;
            // 
            // mck_guidance
            // 
            mck_guidance.Dock = DockStyle.Top;
            mck_guidance.Font = new Font("Segoe UI", 9F);
            mck_guidance.Location = new Point(8, 110);
            mck_guidance.Name = "mck_guidance";
            mck_guidance.Size = new Size(184, 26);
            mck_guidance.TabIndex = 35;
            mck_guidance.Text = "Mood & Date Guidance";
            mck_guidance.UseVisualStyleBackColor = true;
            mck_guidance.CheckedChanged += mck_guidance_CheckedChanged;
            // 
            // mck_ragenabled
            // 
            mck_ragenabled.Dock = DockStyle.Top;
            mck_ragenabled.Font = new Font("Segoe UI", 9F);
            mck_ragenabled.Location = new Point(8, 84);
            mck_ragenabled.Name = "mck_ragenabled";
            mck_ragenabled.Size = new Size(184, 26);
            mck_ragenabled.TabIndex = 44;
            mck_ragenabled.Text = "Contextual Memory";
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
            mck_worldinfo.Text = "Enable WorldInfo";
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
            mck_sessionmemory.Text = "Remember Past Sessions";
            mck_sessionmemory.UseVisualStyleBackColor = true;
            mck_sessionmemory.CheckedChanged += ck_sessionmemory_CheckedChanged;
            // 
            // panRight
            // 
            panRight.Controls.Add(collapsibleGroupBox4);
            panRight.Controls.Add(collapsibleMenus);
            panRight.Controls.Add(cboxGroup);
            panRight.Controls.Add(collapsibleGroupBox3);
            panRight.Dock = DockStyle.Right;
            panRight.Location = new Point(1061, 0);
            panRight.Name = "panRight";
            panRight.Padding = new Padding(0, 6, 0, 6);
            panRight.Size = new Size(200, 981);
            panRight.TabIndex = 29;
            // 
            // collapsibleGroupBox4
            // 
            collapsibleGroupBox4.BackColor = Color.FromArgb(37, 37, 37);
            collapsibleGroupBox4.Controls.Add(button4);
            collapsibleGroupBox4.Controls.Add(button3);
            collapsibleGroupBox4.Controls.Add(btRunAgent);
            collapsibleGroupBox4.Controls.Add(panel9);
            collapsibleGroupBox4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox4.Location = new Point(0, 813);
            collapsibleGroupBox4.Name = "collapsibleGroupBox4";
            collapsibleGroupBox4.Padding = new Padding(8, 32, 8, 8);
            collapsibleGroupBox4.Size = new Size(200, 135);
            collapsibleGroupBox4.TabIndex = 30;
            collapsibleGroupBox4.Text = "Debug";
            // 
            // button4
            // 
            button4.AutoSize = true;
            button4.Dock = DockStyle.Top;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Segoe UI", 9F);
            button4.Location = new Point(8, 96);
            button4.Name = "button4";
            button4.Size = new Size(184, 27);
            button4.TabIndex = 53;
            button4.Text = "Force AFK insert";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.AutoSize = true;
            button3.Dock = DockStyle.Top;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI", 9F);
            button3.Location = new Point(8, 69);
            button3.Name = "button3";
            button3.Size = new Size(184, 27);
            button3.TabIndex = 52;
            button3.Text = "Insert List";
            button3.UseVisualStyleBackColor = false;
            button3.Click += dbgPromptInsert_Click;
            // 
            // btRunAgent
            // 
            btRunAgent.AutoSize = true;
            btRunAgent.Dock = DockStyle.Top;
            btRunAgent.FlatStyle = FlatStyle.Flat;
            btRunAgent.Font = new Font("Segoe UI", 9F);
            btRunAgent.Location = new Point(8, 42);
            btRunAgent.Name = "btRunAgent";
            btRunAgent.Size = new Size(184, 27);
            btRunAgent.TabIndex = 51;
            btRunAgent.Text = "Force Agent Loop";
            btRunAgent.UseVisualStyleBackColor = false;
            btRunAgent.Click += dbgRunAgent_Click;
            // 
            // panel9
            // 
            panel9.Dock = DockStyle.Top;
            panel9.Location = new Point(8, 32);
            panel9.Margin = new Padding(8);
            panel9.Name = "panel9";
            panel9.Padding = new Padding(8);
            panel9.Size = new Size(184, 10);
            panel9.TabIndex = 49;
            // 
            // collapsibleMenus
            // 
            collapsibleMenus.BackColor = Color.FromArgb(37, 37, 37);
            collapsibleMenus.Controls.Add(btMainSettings);
            collapsibleMenus.Controls.Add(btWorldEditor);
            collapsibleMenus.Controls.Add(btRawLog);
            collapsibleMenus.Controls.Add(panel3);
            collapsibleMenus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleMenus.Location = new Point(0, 678);
            collapsibleMenus.Name = "collapsibleMenus";
            collapsibleMenus.Padding = new Padding(8, 32, 8, 8);
            collapsibleMenus.Size = new Size(200, 127);
            collapsibleMenus.TabIndex = 28;
            collapsibleMenus.Text = "Options and Settings";
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(8, 32);
            panel3.Margin = new Padding(8);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(8);
            panel3.Size = new Size(184, 10);
            panel3.TabIndex = 48;
            // 
            // cboxGroup
            // 
            cboxGroup.BackColor = Color.FromArgb(37, 37, 37);
            cboxGroup.Controls.Add(cbGroupSwitch);
            cboxGroup.Controls.Add(label2);
            cboxGroup.Controls.Add(panel6);
            cboxGroup.Controls.Add(lstGroupMembers);
            cboxGroup.Controls.Add(panel7);
            cboxGroup.Controls.Add(ckGroupToggle);
            cboxGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cboxGroup.Location = new Point(0, 348);
            cboxGroup.Name = "cboxGroup";
            cboxGroup.Padding = new Padding(8, 32, 8, 8);
            cboxGroup.Size = new Size(200, 322);
            cboxGroup.TabIndex = 29;
            cboxGroup.Text = "Group Chat";
            // 
            // cbGroupSwitch
            // 
            cbGroupSwitch.BackColor = Color.FromArgb(64, 64, 64);
            cbGroupSwitch.Dock = DockStyle.Top;
            cbGroupSwitch.DropDownHeight = 620;
            cbGroupSwitch.Enabled = false;
            cbGroupSwitch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cbGroupSwitch.Location = new Point(8, 287);
            cbGroupSwitch.MaxDropDownItems = 25;
            cbGroupSwitch.Name = "cbGroupSwitch";
            cbGroupSwitch.Padding = new Padding(1);
            cbGroupSwitch.Size = new Size(184, 23);
            cbGroupSwitch.TabIndex = 3;
            cbGroupSwitch.SelectedIndexChanged += cbGroupSwitch_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(8, 272);
            label2.Name = "label2";
            label2.Size = new Size(131, 15);
            label2.TabIndex = 2;
            label2.Text = "Switch Active Persona";
            // 
            // panel6
            // 
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(8, 262);
            panel6.Margin = new Padding(8);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(8);
            panel6.Size = new Size(184, 10);
            panel6.TabIndex = 49;
            // 
            // lstGroupMembers
            // 
            lstGroupMembers.AlwaysScrollbar = true;
            lstGroupMembers.BackColor = Color.FromArgb(64, 64, 64);
            lstGroupMembers.Dock = DockStyle.Top;
            lstGroupMembers.Enabled = false;
            lstGroupMembers.Font = new Font("Segoe UI", 9F);
            lstGroupMembers.ItemHeight = 24;
            lstGroupMembers.Location = new Point(8, 68);
            lstGroupMembers.Name = "lstGroupMembers";
            lstGroupMembers.Padding = new Padding(1);
            lstGroupMembers.Size = new Size(184, 194);
            lstGroupMembers.TabIndex = 46;
            lstGroupMembers.ItemCheck += lstGroupMembers_ItemCheck;
            // 
            // panel7
            // 
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point(8, 58);
            panel7.Margin = new Padding(8);
            panel7.Name = "panel7";
            panel7.Padding = new Padding(8);
            panel7.Size = new Size(184, 10);
            panel7.TabIndex = 50;
            // 
            // ckGroupToggle
            // 
            ckGroupToggle.Dock = DockStyle.Top;
            ckGroupToggle.Font = new Font("Segoe UI", 9F);
            ckGroupToggle.Location = new Point(8, 32);
            ckGroupToggle.Name = "ckGroupToggle";
            ckGroupToggle.Size = new Size(184, 26);
            ckGroupToggle.TabIndex = 45;
            ckGroupToggle.Text = "Enable Group Chat";
            ckGroupToggle.UseVisualStyleBackColor = true;
            ckGroupToggle.CheckedChanged += ckGroupToggle_CheckedChanged;
            // 
            // collapsibleGroupBox3
            // 
            collapsibleGroupBox3.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox3.Controls.Add(mck_ttstoggle);
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
            collapsibleGroupBox3.Size = new Size(200, 334);
            collapsibleGroupBox3.TabIndex = 27;
            collapsibleGroupBox3.Text = "Chat Settings";
            // 
            // mck_ttstoggle
            // 
            mck_ttstoggle.Dock = DockStyle.Bottom;
            mck_ttstoggle.Font = new Font("Segoe UI", 9F);
            mck_ttstoggle.Location = new Point(8, 183);
            mck_ttstoggle.Name = "mck_ttstoggle";
            mck_ttstoggle.Size = new Size(184, 26);
            mck_ttstoggle.TabIndex = 36;
            mck_ttstoggle.Text = "Text To Speech";
            mck_ttstoggle.UseVisualStyleBackColor = true;
            mck_ttstoggle.CheckedChanged += ck_ttstoggle_CheckedChanged;
            // 
            // mck_caninitchat
            // 
            mck_caninitchat.Dock = DockStyle.Bottom;
            mck_caninitchat.Enabled = false;
            mck_caninitchat.Font = new Font("Segoe UI", 9F);
            mck_caninitchat.Location = new Point(8, 209);
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
            panel1.Location = new Point(8, 235);
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
            ClientSize = new Size(1261, 1003);
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
            Text = "Lethe Chat";
            FormClosing += MainForm_FormClosing;
            statusbar.ResumeLayout(false);
            statusbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictEmbed).EndInit();
            collapseModel.ResumeLayout(false);
            collapseModel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)web_chat).EndInit();
            collapsibleGroupBox1.ResumeLayout(false);
            collapsibleGroupBox1.PerformLayout();
            panLeft.ResumeLayout(false);
            cboxVLM.ResumeLayout(false);
            cboxVLM.PerformLayout();
            collapsibleGroupBox2.ResumeLayout(false);
            collapsibleGroupBox2.PerformLayout();
            panRight.ResumeLayout(false);
            collapsibleGroupBox4.ResumeLayout(false);
            collapsibleGroupBox4.PerformLayout();
            collapsibleMenus.ResumeLayout(false);
            collapsibleMenus.PerformLayout();
            cboxGroup.ResumeLayout(false);
            cboxGroup.PerformLayout();
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
        private LetheChat.Controls.SpellCheckedTextBox ed_input;
        private Button bt_editchar;
        private Button bt_scenario;
        private Label label3;
        private ModernComboBox cb_bot;
        private Label label4;
        private ModernComboBox cb_user;
        private Button bt_newsession;
        private Label label11;
        private ModernComboBox cb_sysprompt;
        private Label label5;
        private ModernComboBox cb_instruct;
        private Label label6;
        private ModernComboBox cb_infer;
        private Label label9;
        private ModernNumericUpDown num_temperature;
        private Button btInstructEdit;
        private Button btSysPrompt;
        private Button btSampleEditor;
        private Button btMainSettings;
        private Button btWorldEditor;
        private Button btChatHistory;
        private Button btRawLog;
        private Button button2;
        private Controls.CollapsibleGroupBox collapseModel;
        private ModernNumericUpDown num_maxresponse;
        private Label label7;
        private ModernNumericUpDown num_maxcontext;
        private Label label8;
        private Button bt_refresh;
        private Button bt_backend;
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
        private Controls.ModernCheckBox mck_guidance;
        private Controls.ModernCheckBox mck_caninitchat;
        private Panel panel1;
        private CollapsibleGroupBox collapsibleMenus;
        private Panel panel3;
        private Panel panel5;
        private CollapsibleGroupBox cboxGroup;
        private Label label2;
        private ModernComboBox cbGroupSwitch;
        private ModernCheckBox ckGroupToggle;
        private Panel panel6;
        private ModernCheckedListBox lstGroupMembers;
        private Panel panel7;
        private CollapsibleGroupBox collapsibleGroupBox4;
        private Panel panel9;
        private Button button3;
        private Button btRunAgent;
        private Button button4;
        private ModernCheckBox ckToolCalls;
        private Button bt_llama;
        private Panel panel4;
    }
}
