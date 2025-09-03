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
            openFileDialog1 = new OpenFileDialog();
            fontDialog1 = new FontDialog();
            HelptoolTip = new ToolTip(components);
            statusbar = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            bindingSource1 = new BindingSource(components);
            AutoTalkTimer = new System.Windows.Forms.Timer(components);
            tabConsole = new TabPage();
            ed_log = new TextBox();
            tabHistory = new TabPage();
            panel6 = new Panel();
            web_sessioncontent = new Microsoft.Web.WebView2.WinForms.WebView2();
            panel7 = new Panel();
            groupBox25 = new GroupBox();
            ck_hist_casesensitive = new CheckBox();
            ck_hist_kw = new CheckBox();
            ck_hist_sticky = new CheckBox();
            label56 = new Label();
            ed_hist_kw1 = new TextBox();
            label57 = new Label();
            cb_hist_kwlink = new ComboBox();
            label58 = new Label();
            ed_hist_kw2 = new TextBox();
            groupBox12 = new GroupBox();
            ck_hist_isrp = new CheckBox();
            bt_historyupdate = new Button();
            lbl_sessiondata = new Label();
            ed_sessioninfo = new TextBox();
            label64 = new Label();
            ed_sessiontitle = new TextBox();
            bt_sessionrefresh = new Button();
            panel4 = new Panel();
            listSession = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            panel5 = new Panel();
            btEmbedAll = new Button();
            bt_deleteAllHistory = new Button();
            tabChat = new TabPage();
            bt_impersonate = new Button();
            web_chat = new Microsoft.Web.WebView2.WinForms.WebView2();
            bt_delete = new Button();
            bt_reroll = new Button();
            bt_send = new Button();
            ed_input = new TextBox();
            panel1 = new Panel();
            groupBox23 = new GroupBox();
            btWorldEditor = new Button();
            btMainSettings = new Button();
            ck_agentmode = new CheckBox();
            bt_clearimg = new Button();
            pictEmbed = new PictureBox();
            ck_onlinerag = new CheckBox();
            ck_worldinfo = new CheckBox();
            ck_ragenabled = new CheckBox();
            ck_sessionmemory = new CheckBox();
            groupBox5 = new GroupBox();
            btSysPrompt = new Button();
            bt_editchar = new Button();
            bt_scenario = new Button();
            label3 = new Label();
            cb_bot = new ComboBox();
            label4 = new Label();
            ck_ttstoggle = new CheckBox();
            cb_user = new ComboBox();
            bt_newsession = new Button();
            label11 = new Label();
            ck_caninitchat = new CheckBox();
            cb_sysprompt = new ComboBox();
            ck_senseoftime = new CheckBox();
            groupBox4 = new GroupBox();
            btSampleEditor = new Button();
            btInstructEdit = new Button();
            ck_ragtothink = new CheckBox();
            ck_charsampler = new CheckBox();
            ck_disablethink = new CheckBox();
            ck_forceNames = new CheckBox();
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
            tabMain = new TabControl();
            tabSearch = new TabPage();
            txtSearchRes = new TextBox();
            textBox1 = new TextBox();
            statusbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            tabConsole.SuspendLayout();
            tabHistory.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)web_sessioncontent).BeginInit();
            panel7.SuspendLayout();
            groupBox25.SuspendLayout();
            groupBox12.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            tabChat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)web_chat).BeginInit();
            panel1.SuspendLayout();
            groupBox23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictEmbed).BeginInit();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_temperature).BeginInit();
            grp_model.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_maxresponse).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_maxcontext).BeginInit();
            tabMain.SuspendLayout();
            tabSearch.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
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
            statusbar.Location = new Point(0, 931);
            statusbar.Name = "statusbar";
            statusbar.Size = new Size(1063, 22);
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
            // tabConsole
            // 
            tabConsole.Controls.Add(ed_log);
            tabConsole.Location = new Point(4, 27);
            tabConsole.Name = "tabConsole";
            tabConsole.Padding = new Padding(3);
            tabConsole.Size = new Size(996, 897);
            tabConsole.TabIndex = 9;
            tabConsole.Text = "Raw Prompt";
            tabConsole.UseVisualStyleBackColor = true;
            // 
            // ed_log
            // 
            ed_log.BackColor = SystemColors.Control;
            ed_log.BorderStyle = BorderStyle.None;
            ed_log.Dock = DockStyle.Fill;
            ed_log.Font = new Font("Segoe UI", 9F);
            ed_log.Location = new Point(3, 3);
            ed_log.Multiline = true;
            ed_log.Name = "ed_log";
            ed_log.ScrollBars = ScrollBars.Vertical;
            ed_log.Size = new Size(990, 891);
            ed_log.TabIndex = 3;
            // 
            // tabHistory
            // 
            tabHistory.Controls.Add(panel6);
            tabHistory.Controls.Add(panel4);
            tabHistory.Location = new Point(4, 27);
            tabHistory.Name = "tabHistory";
            tabHistory.Size = new Size(1055, 897);
            tabHistory.TabIndex = 6;
            tabHistory.Text = "Chat History";
            tabHistory.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            panel6.Controls.Add(web_sessioncontent);
            panel6.Controls.Add(panel7);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(376, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(679, 897);
            panel6.TabIndex = 2;
            // 
            // web_sessioncontent
            // 
            web_sessioncontent.AllowExternalDrop = true;
            web_sessioncontent.CreationProperties = null;
            web_sessioncontent.DefaultBackgroundColor = Color.White;
            web_sessioncontent.Dock = DockStyle.Fill;
            web_sessioncontent.Location = new Point(0, 308);
            web_sessioncontent.Name = "web_sessioncontent";
            web_sessioncontent.Size = new Size(679, 589);
            web_sessioncontent.TabIndex = 4;
            web_sessioncontent.ZoomFactor = 1D;
            // 
            // panel7
            // 
            panel7.Controls.Add(groupBox25);
            panel7.Controls.Add(groupBox12);
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point(0, 0);
            panel7.Name = "panel7";
            panel7.Size = new Size(679, 308);
            panel7.TabIndex = 3;
            // 
            // groupBox25
            // 
            groupBox25.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox25.Controls.Add(ck_hist_casesensitive);
            groupBox25.Controls.Add(ck_hist_kw);
            groupBox25.Controls.Add(ck_hist_sticky);
            groupBox25.Controls.Add(label56);
            groupBox25.Controls.Add(ed_hist_kw1);
            groupBox25.Controls.Add(label57);
            groupBox25.Controls.Add(cb_hist_kwlink);
            groupBox25.Controls.Add(label58);
            groupBox25.Controls.Add(ed_hist_kw2);
            groupBox25.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox25.Location = new Point(6, 200);
            groupBox25.Name = "groupBox25";
            groupBox25.Size = new Size(665, 101);
            groupBox25.TabIndex = 1;
            groupBox25.TabStop = false;
            groupBox25.Text = "Keyword Activation";
            // 
            // ck_hist_casesensitive
            // 
            ck_hist_casesensitive.AutoSize = true;
            ck_hist_casesensitive.Font = new Font("Segoe UI", 9F);
            ck_hist_casesensitive.Location = new Point(284, 22);
            ck_hist_casesensitive.Name = "ck_hist_casesensitive";
            ck_hist_casesensitive.Size = new Size(100, 19);
            ck_hist_casesensitive.TabIndex = 19;
            ck_hist_casesensitive.Text = "Case Sensitive";
            ck_hist_casesensitive.UseVisualStyleBackColor = true;
            ck_hist_casesensitive.CheckedChanged += UpdateHistoryEntryEvent;
            // 
            // ck_hist_kw
            // 
            ck_hist_kw.AutoSize = true;
            ck_hist_kw.Font = new Font("Segoe UI", 9F);
            ck_hist_kw.Location = new Point(6, 22);
            ck_hist_kw.Name = "ck_hist_kw";
            ck_hist_kw.Size = new Size(150, 19);
            ck_hist_kw.TabIndex = 12;
            ck_hist_kw.Text = "Enable Keyword Trigger";
            ck_hist_kw.UseVisualStyleBackColor = true;
            ck_hist_kw.CheckedChanged += UpdateHistoryEntryEvent;
            // 
            // ck_hist_sticky
            // 
            ck_hist_sticky.AutoSize = true;
            ck_hist_sticky.Font = new Font("Segoe UI", 9F);
            ck_hist_sticky.Location = new Point(162, 22);
            ck_hist_sticky.Name = "ck_hist_sticky";
            ck_hist_sticky.Size = new Size(116, 19);
            ck_hist_sticky.TabIndex = 21;
            ck_hist_sticky.Text = "Always Activated";
            ck_hist_sticky.UseVisualStyleBackColor = true;
            ck_hist_sticky.CheckedChanged += UpdateHistoryEntryEvent;
            // 
            // label56
            // 
            label56.AutoSize = true;
            label56.Font = new Font("Segoe UI", 9F);
            label56.Location = new Point(232, 52);
            label56.Name = "label56";
            label56.Size = new Size(78, 15);
            label56.TabIndex = 18;
            label56.Text = "Keyword Link";
            // 
            // ed_hist_kw1
            // 
            ed_hist_kw1.Font = new Font("Segoe UI", 9F);
            ed_hist_kw1.Location = new Point(6, 70);
            ed_hist_kw1.Name = "ed_hist_kw1";
            ed_hist_kw1.Size = new Size(220, 23);
            ed_hist_kw1.TabIndex = 14;
            ed_hist_kw1.TextChanged += UpdateHistoryEntryEvent;
            // 
            // label57
            // 
            label57.AutoSize = true;
            label57.Font = new Font("Segoe UI", 9F);
            label57.Location = new Point(368, 52);
            label57.Name = "label57";
            label57.Size = new Size(67, 15);
            label57.TabIndex = 15;
            label57.Text = "Keywords 2";
            // 
            // cb_hist_kwlink
            // 
            cb_hist_kwlink.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_hist_kwlink.Font = new Font("Segoe UI", 9F);
            cb_hist_kwlink.FormattingEnabled = true;
            cb_hist_kwlink.Items.AddRange(new object[] { "And", "Or", "Not" });
            cb_hist_kwlink.Location = new Point(232, 70);
            cb_hist_kwlink.Name = "cb_hist_kwlink";
            cb_hist_kwlink.Size = new Size(130, 23);
            cb_hist_kwlink.TabIndex = 17;
            cb_hist_kwlink.SelectedIndexChanged += UpdateHistoryEntryEvent;
            // 
            // label58
            // 
            label58.AutoSize = true;
            label58.Font = new Font("Segoe UI", 9F);
            label58.Location = new Point(6, 52);
            label58.Name = "label58";
            label58.Size = new Size(67, 15);
            label58.TabIndex = 13;
            label58.Text = "Keywords 1";
            // 
            // ed_hist_kw2
            // 
            ed_hist_kw2.Font = new Font("Segoe UI", 9F);
            ed_hist_kw2.Location = new Point(368, 70);
            ed_hist_kw2.Name = "ed_hist_kw2";
            ed_hist_kw2.Size = new Size(220, 23);
            ed_hist_kw2.TabIndex = 16;
            ed_hist_kw2.TextChanged += UpdateHistoryEntryEvent;
            // 
            // groupBox12
            // 
            groupBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox12.Controls.Add(ck_hist_isrp);
            groupBox12.Controls.Add(bt_historyupdate);
            groupBox12.Controls.Add(lbl_sessiondata);
            groupBox12.Controls.Add(ed_sessioninfo);
            groupBox12.Controls.Add(label64);
            groupBox12.Controls.Add(ed_sessiontitle);
            groupBox12.Controls.Add(bt_sessionrefresh);
            groupBox12.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox12.Location = new Point(6, 3);
            groupBox12.Name = "groupBox12";
            groupBox12.Size = new Size(665, 191);
            groupBox12.TabIndex = 0;
            groupBox12.TabStop = false;
            groupBox12.Text = "Session Information";
            // 
            // ck_hist_isrp
            // 
            ck_hist_isrp.AutoSize = true;
            ck_hist_isrp.Font = new Font("Segoe UI", 9F);
            ck_hist_isrp.Location = new Point(229, 14);
            ck_hist_isrp.Name = "ck_hist_isrp";
            ck_hist_isrp.Size = new Size(99, 19);
            ck_hist_isrp.TabIndex = 20;
            ck_hist_isrp.Text = "Roleplay Only";
            ck_hist_isrp.UseVisualStyleBackColor = true;
            ck_hist_isrp.CheckedChanged += UpdateHistoryEntryEvent;
            // 
            // bt_historyupdate
            // 
            bt_historyupdate.Location = new Point(335, 11);
            bt_historyupdate.Name = "bt_historyupdate";
            bt_historyupdate.Size = new Size(108, 23);
            bt_historyupdate.TabIndex = 4;
            bt_historyupdate.Text = "Update Entry";
            bt_historyupdate.UseVisualStyleBackColor = true;
            bt_historyupdate.Click += bt_historyupdate_Click;
            // 
            // lbl_sessiondata
            // 
            lbl_sessiondata.AutoSize = true;
            lbl_sessiondata.Font = new Font("Segoe UI", 9F);
            lbl_sessiondata.Location = new Point(6, 63);
            lbl_sessiondata.Name = "lbl_sessiondata";
            lbl_sessiondata.Size = new Size(58, 15);
            lbl_sessiondata.TabIndex = 3;
            lbl_sessiondata.Text = "Summary";
            // 
            // ed_sessioninfo
            // 
            ed_sessioninfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ed_sessioninfo.Font = new Font("Segoe UI", 9F);
            ed_sessioninfo.Location = new Point(6, 81);
            ed_sessioninfo.Multiline = true;
            ed_sessioninfo.Name = "ed_sessioninfo";
            ed_sessioninfo.PlaceholderText = "Select a session from the left panel to show information about it.";
            ed_sessioninfo.ScrollBars = ScrollBars.Vertical;
            ed_sessioninfo.Size = new Size(653, 104);
            ed_sessioninfo.TabIndex = 2;
            ed_sessioninfo.TextChanged += ed_sessioninfo_TextChanged;
            // 
            // label64
            // 
            label64.AutoSize = true;
            label64.Font = new Font("Segoe UI", 9F);
            label64.Location = new Point(6, 19);
            label64.Name = "label64";
            label64.Size = new Size(30, 15);
            label64.TabIndex = 1;
            label64.Text = "Title";
            // 
            // ed_sessiontitle
            // 
            ed_sessiontitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ed_sessiontitle.Font = new Font("Segoe UI", 9F);
            ed_sessiontitle.Location = new Point(6, 37);
            ed_sessiontitle.Name = "ed_sessiontitle";
            ed_sessiontitle.PlaceholderText = "No Session Selected";
            ed_sessiontitle.Size = new Size(653, 23);
            ed_sessiontitle.TabIndex = 0;
            ed_sessiontitle.TextChanged += ed_sessiontitle_TextChanged;
            // 
            // bt_sessionrefresh
            // 
            bt_sessionrefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bt_sessionrefresh.Location = new Point(508, 11);
            bt_sessionrefresh.Name = "bt_sessionrefresh";
            bt_sessionrefresh.Size = new Size(151, 23);
            bt_sessionrefresh.TabIndex = 2;
            bt_sessionrefresh.Text = "Generate Summary";
            bt_sessionrefresh.UseVisualStyleBackColor = true;
            bt_sessionrefresh.Click += bt_sessionrefresh_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(listSession);
            panel4.Controls.Add(panel5);
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(376, 897);
            panel4.TabIndex = 0;
            // 
            // listSession
            // 
            listSession.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listSession.Dock = DockStyle.Fill;
            listSession.FullRowSelect = true;
            listSession.Location = new Point(0, 0);
            listSession.Name = "listSession";
            listSession.Size = new Size(376, 831);
            listSession.TabIndex = 2;
            listSession.UseCompatibleStateImageBehavior = false;
            listSession.View = View.Details;
            listSession.SelectedIndexChanged += listSession_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Title";
            columnHeader1.Width = 280;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Date";
            columnHeader2.Width = 80;
            // 
            // panel5
            // 
            panel5.Controls.Add(btEmbedAll);
            panel5.Controls.Add(bt_deleteAllHistory);
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(0, 831);
            panel5.Name = "panel5";
            panel5.Size = new Size(376, 66);
            panel5.TabIndex = 1;
            // 
            // btEmbedAll
            // 
            btEmbedAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btEmbedAll.BackColor = SystemColors.Control;
            btEmbedAll.Font = new Font("Segoe UI", 9F);
            btEmbedAll.Location = new Point(3, 6);
            btEmbedAll.Name = "btEmbedAll";
            btEmbedAll.Size = new Size(367, 23);
            btEmbedAll.TabIndex = 5;
            btEmbedAll.Text = "Embed Everything";
            btEmbedAll.UseVisualStyleBackColor = false;
            btEmbedAll.Click += btEmbedAll_Click;
            // 
            // bt_deleteAllHistory
            // 
            bt_deleteAllHistory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bt_deleteAllHistory.BackColor = SystemColors.Control;
            bt_deleteAllHistory.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_deleteAllHistory.ForeColor = Color.DarkRed;
            bt_deleteAllHistory.Location = new Point(3, 35);
            bt_deleteAllHistory.Name = "bt_deleteAllHistory";
            bt_deleteAllHistory.Size = new Size(367, 23);
            bt_deleteAllHistory.TabIndex = 4;
            bt_deleteAllHistory.Text = "Delete All History";
            bt_deleteAllHistory.UseVisualStyleBackColor = false;
            bt_deleteAllHistory.Click += bt_deleteAllHistory_Click;
            // 
            // tabChat
            // 
            tabChat.Controls.Add(bt_impersonate);
            tabChat.Controls.Add(web_chat);
            tabChat.Controls.Add(bt_delete);
            tabChat.Controls.Add(bt_reroll);
            tabChat.Controls.Add(bt_send);
            tabChat.Controls.Add(ed_input);
            tabChat.Controls.Add(panel1);
            tabChat.Location = new Point(4, 27);
            tabChat.Name = "tabChat";
            tabChat.Padding = new Padding(3);
            tabChat.Size = new Size(996, 897);
            tabChat.TabIndex = 1;
            tabChat.Text = "Chat";
            tabChat.UseVisualStyleBackColor = true;
            // 
            // bt_impersonate
            // 
            bt_impersonate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_impersonate.BackColor = Color.Turquoise;
            bt_impersonate.FlatStyle = FlatStyle.Flat;
            bt_impersonate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_impersonate.Location = new Point(928, 808);
            bt_impersonate.Name = "bt_impersonate";
            bt_impersonate.Size = new Size(60, 25);
            bt_impersonate.TabIndex = 7;
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
            web_chat.Location = new Point(212, 3);
            web_chat.Name = "web_chat";
            web_chat.Size = new Size(778, 798);
            web_chat.TabIndex = 6;
            web_chat.ZoomFactor = 1D;
            // 
            // bt_delete
            // 
            bt_delete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_delete.BackColor = Color.LightCoral;
            bt_delete.FlatStyle = FlatStyle.Flat;
            bt_delete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_delete.Location = new Point(928, 869);
            bt_delete.Name = "bt_delete";
            bt_delete.Size = new Size(60, 25);
            bt_delete.TabIndex = 5;
            bt_delete.Text = "Delete";
            bt_delete.UseVisualStyleBackColor = false;
            bt_delete.Click += DeleteLastMessage;
            // 
            // bt_reroll
            // 
            bt_reroll.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_reroll.BackColor = Color.PaleGoldenrod;
            bt_reroll.FlatStyle = FlatStyle.Flat;
            bt_reroll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_reroll.Location = new Point(928, 839);
            bt_reroll.Name = "bt_reroll";
            bt_reroll.Size = new Size(60, 25);
            bt_reroll.TabIndex = 4;
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
            bt_send.Location = new Point(864, 808);
            bt_send.Name = "bt_send";
            bt_send.Size = new Size(60, 86);
            bt_send.TabIndex = 3;
            bt_send.Text = "Send";
            bt_send.UseVisualStyleBackColor = false;
            bt_send.Click += SendMessage;
            // 
            // ed_input
            // 
            ed_input.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ed_input.BackColor = Color.WhiteSmoke;
            ed_input.BorderStyle = BorderStyle.FixedSingle;
            ed_input.Font = new Font("Segoe UI", 11F);
            ed_input.Location = new Point(212, 806);
            ed_input.Multiline = true;
            ed_input.Name = "ed_input";
            ed_input.ScrollBars = ScrollBars.Vertical;
            ed_input.Size = new Size(646, 87);
            ed_input.TabIndex = 2;
            ed_input.KeyPress += ed_input_KeyPress;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Controls.Add(groupBox23);
            panel1.Controls.Add(groupBox5);
            panel1.Controls.Add(groupBox4);
            panel1.Controls.Add(grp_model);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(203, 891);
            panel1.TabIndex = 0;
            // 
            // groupBox23
            // 
            groupBox23.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox23.Controls.Add(btWorldEditor);
            groupBox23.Controls.Add(btMainSettings);
            groupBox23.Controls.Add(ck_agentmode);
            groupBox23.Controls.Add(bt_clearimg);
            groupBox23.Controls.Add(pictEmbed);
            groupBox23.Controls.Add(ck_onlinerag);
            groupBox23.Controls.Add(ck_worldinfo);
            groupBox23.Controls.Add(ck_ragenabled);
            groupBox23.Controls.Add(ck_sessionmemory);
            groupBox23.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox23.Location = new Point(6, 663);
            groupBox23.Name = "groupBox23";
            groupBox23.Size = new Size(187, 227);
            groupBox23.TabIndex = 26;
            groupBox23.TabStop = false;
            groupBox23.Text = "Quick Settings";
            // 
            // btWorldEditor
            // 
            btWorldEditor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btWorldEditor.Font = new Font("Segoe UI", 9F);
            btWorldEditor.Location = new Point(6, 169);
            btWorldEditor.Name = "btWorldEditor";
            btWorldEditor.Size = new Size(175, 23);
            btWorldEditor.TabIndex = 39;
            btWorldEditor.Text = "WorldInfo Editor";
            btWorldEditor.UseVisualStyleBackColor = true;
            btWorldEditor.Click += btWorldEditor_Click;
            // 
            // btMainSettings
            // 
            btMainSettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btMainSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btMainSettings.Location = new Point(6, 198);
            btMainSettings.Name = "btMainSettings";
            btMainSettings.Size = new Size(175, 23);
            btMainSettings.TabIndex = 38;
            btMainSettings.Text = "General Settings";
            btMainSettings.UseVisualStyleBackColor = true;
            btMainSettings.Click += btMainSettings_Click;
            // 
            // ck_agentmode
            // 
            ck_agentmode.AutoSize = true;
            ck_agentmode.Font = new Font("Segoe UI", 9F);
            ck_agentmode.Location = new Point(6, 120);
            ck_agentmode.Name = "ck_agentmode";
            ck_agentmode.Size = new Size(159, 19);
            ck_agentmode.TabIndex = 37;
            ck_agentmode.Text = "Background Agent Mode";
            ck_agentmode.UseVisualStyleBackColor = true;
            ck_agentmode.CheckedChanged += ck_agentmode_CheckedChanged;
            // 
            // bt_clearimg
            // 
            bt_clearimg.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            bt_clearimg.Font = new Font("Segoe UI", 9F);
            bt_clearimg.Location = new Point(127, 140);
            bt_clearimg.Name = "bt_clearimg";
            bt_clearimg.Size = new Size(54, 23);
            bt_clearimg.TabIndex = 34;
            bt_clearimg.Text = "Clear";
            bt_clearimg.UseVisualStyleBackColor = true;
            bt_clearimg.Click += bt_clearimg_Click;
            // 
            // pictEmbed
            // 
            pictEmbed.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pictEmbed.BorderStyle = BorderStyle.FixedSingle;
            pictEmbed.Location = new Point(6, 123);
            pictEmbed.Name = "pictEmbed";
            pictEmbed.Size = new Size(54, 40);
            pictEmbed.SizeMode = PictureBoxSizeMode.StretchImage;
            pictEmbed.TabIndex = 33;
            pictEmbed.TabStop = false;
            // 
            // ck_onlinerag
            // 
            ck_onlinerag.AutoSize = true;
            ck_onlinerag.Font = new Font("Segoe UI", 9F);
            ck_onlinerag.Location = new Point(6, 47);
            ck_onlinerag.Name = "ck_onlinerag";
            ck_onlinerag.Size = new Size(88, 19);
            ck_onlinerag.TabIndex = 29;
            ck_onlinerag.Text = "Web Search";
            ck_onlinerag.UseVisualStyleBackColor = true;
            ck_onlinerag.CheckedChanged += ck_onlinerag_CheckedChanged;
            // 
            // ck_worldinfo
            // 
            ck_worldinfo.AutoSize = true;
            ck_worldinfo.Font = new Font("Segoe UI", 9F);
            ck_worldinfo.Location = new Point(6, 72);
            ck_worldinfo.Name = "ck_worldinfo";
            ck_worldinfo.Size = new Size(127, 19);
            ck_worldinfo.TabIndex = 27;
            ck_worldinfo.Text = "World Info Enabled";
            ck_worldinfo.UseVisualStyleBackColor = true;
            ck_worldinfo.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // ck_ragenabled
            // 
            ck_ragenabled.AutoSize = true;
            ck_ragenabled.Checked = true;
            ck_ragenabled.CheckState = CheckState.Checked;
            ck_ragenabled.Font = new Font("Segoe UI", 9F);
            ck_ragenabled.Location = new Point(6, 22);
            ck_ragenabled.Name = "ck_ragenabled";
            ck_ragenabled.Size = new Size(83, 19);
            ck_ragenabled.TabIndex = 20;
            ck_ragenabled.Text = "Local RAG ";
            ck_ragenabled.UseVisualStyleBackColor = true;
            ck_ragenabled.CheckedChanged += ck_ragenabled_CheckedChanged;
            // 
            // ck_sessionmemory
            // 
            ck_sessionmemory.AutoSize = true;
            ck_sessionmemory.Font = new Font("Segoe UI", 9F);
            ck_sessionmemory.Location = new Point(6, 95);
            ck_sessionmemory.Name = "ck_sessionmemory";
            ck_sessionmemory.Size = new Size(113, 19);
            ck_sessionmemory.TabIndex = 24;
            ck_sessionmemory.Text = "Session Memory";
            ck_sessionmemory.UseVisualStyleBackColor = true;
            ck_sessionmemory.CheckedChanged += ck_sessionmemory_CheckedChanged;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Controls.Add(btSysPrompt);
            groupBox5.Controls.Add(bt_editchar);
            groupBox5.Controls.Add(bt_scenario);
            groupBox5.Controls.Add(label3);
            groupBox5.Controls.Add(cb_bot);
            groupBox5.Controls.Add(label4);
            groupBox5.Controls.Add(ck_ttstoggle);
            groupBox5.Controls.Add(cb_user);
            groupBox5.Controls.Add(bt_newsession);
            groupBox5.Controls.Add(label11);
            groupBox5.Controls.Add(ck_caninitchat);
            groupBox5.Controls.Add(cb_sysprompt);
            groupBox5.Controls.Add(ck_senseoftime);
            groupBox5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox5.Location = new Point(6, 370);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(187, 287);
            groupBox5.TabIndex = 25;
            groupBox5.TabStop = false;
            groupBox5.Text = "Chat Settings";
            // 
            // btSysPrompt
            // 
            btSysPrompt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btSysPrompt.Location = new Point(146, 104);
            btSysPrompt.Name = "btSysPrompt";
            btSysPrompt.Size = new Size(35, 20);
            btSysPrompt.TabIndex = 29;
            btSysPrompt.Text = "...";
            btSysPrompt.UseVisualStyleBackColor = true;
            btSysPrompt.Click += btSysPrompt_Click;
            // 
            // bt_editchar
            // 
            bt_editchar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_editchar.Location = new Point(146, 16);
            bt_editchar.Name = "bt_editchar";
            bt_editchar.Size = new Size(35, 20);
            bt_editchar.TabIndex = 27;
            bt_editchar.Text = "...";
            bt_editchar.UseVisualStyleBackColor = true;
            bt_editchar.Click += bt_editchar_Click;
            // 
            // bt_scenario
            // 
            bt_scenario.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bt_scenario.Font = new Font("Segoe UI", 9F);
            bt_scenario.Location = new Point(6, 228);
            bt_scenario.Name = "bt_scenario";
            bt_scenario.Size = new Size(175, 23);
            bt_scenario.TabIndex = 26;
            bt_scenario.Text = "Change Scenario";
            bt_scenario.UseVisualStyleBackColor = true;
            bt_scenario.Click += bt_scenario_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.Location = new Point(6, 19);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 0;
            label3.Text = "Bot Persona";
            // 
            // cb_bot
            // 
            cb_bot.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_bot.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_bot.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cb_bot.Location = new Point(6, 37);
            cb_bot.Name = "cb_bot";
            cb_bot.Size = new Size(175, 23);
            cb_bot.TabIndex = 1;
            cb_bot.SelectedIndexChanged += cb_bot_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F);
            label4.Location = new Point(6, 63);
            label4.Name = "label4";
            label4.Size = new Size(75, 15);
            label4.TabIndex = 2;
            label4.Text = "User Persona";
            // 
            // ck_ttstoggle
            // 
            ck_ttstoggle.AutoSize = true;
            ck_ttstoggle.Font = new Font("Segoe UI", 9F);
            ck_ttstoggle.Location = new Point(6, 204);
            ck_ttstoggle.Name = "ck_ttstoggle";
            ck_ttstoggle.Size = new Size(84, 19);
            ck_ttstoggle.TabIndex = 32;
            ck_ttstoggle.Text = "Enable TTS";
            ck_ttstoggle.UseVisualStyleBackColor = true;
            ck_ttstoggle.CheckedChanged += ck_ttstoggle_CheckedChanged;
            // 
            // cb_user
            // 
            cb_user.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_user.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_user.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cb_user.Location = new Point(6, 81);
            cb_user.Name = "cb_user";
            cb_user.Size = new Size(175, 23);
            cb_user.TabIndex = 3;
            cb_user.SelectedIndexChanged += cb_user_SelectedIndexChanged;
            // 
            // bt_newsession
            // 
            bt_newsession.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bt_newsession.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_newsession.Location = new Point(6, 258);
            bt_newsession.Name = "bt_newsession";
            bt_newsession.Size = new Size(175, 23);
            bt_newsession.TabIndex = 21;
            bt_newsession.Text = "Start New Session";
            bt_newsession.UseVisualStyleBackColor = true;
            bt_newsession.Click += StartNewSession;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F);
            label11.Location = new Point(6, 107);
            label11.Name = "label11";
            label11.Size = new Size(88, 15);
            label11.TabIndex = 18;
            label11.Text = "System Prompt";
            // 
            // ck_caninitchat
            // 
            ck_caninitchat.AutoSize = true;
            ck_caninitchat.Font = new Font("Segoe UI", 9F);
            ck_caninitchat.Location = new Point(6, 179);
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
            cb_sysprompt.Font = new Font("Segoe UI", 9F);
            cb_sysprompt.Location = new Point(6, 125);
            cb_sysprompt.Name = "cb_sysprompt";
            cb_sysprompt.Size = new Size(175, 23);
            cb_sysprompt.TabIndex = 19;
            cb_sysprompt.SelectedIndexChanged += cb_sysprompt_SelectionIndexChanged;
            // 
            // ck_senseoftime
            // 
            ck_senseoftime.AutoSize = true;
            ck_senseoftime.Font = new Font("Segoe UI", 9F);
            ck_senseoftime.Location = new Point(6, 154);
            ck_senseoftime.Name = "ck_senseoftime";
            ck_senseoftime.Size = new Size(100, 19);
            ck_senseoftime.TabIndex = 23;
            ck_senseoftime.Text = "Sense of Time";
            ck_senseoftime.UseVisualStyleBackColor = true;
            ck_senseoftime.CheckedChanged += ck_senseoftime_CheckedChanged;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.Controls.Add(btSampleEditor);
            groupBox4.Controls.Add(btInstructEdit);
            groupBox4.Controls.Add(ck_ragtothink);
            groupBox4.Controls.Add(ck_charsampler);
            groupBox4.Controls.Add(ck_disablethink);
            groupBox4.Controls.Add(ck_forceNames);
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(cb_instruct);
            groupBox4.Controls.Add(label6);
            groupBox4.Controls.Add(cb_infer);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(num_temperature);
            groupBox4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox4.Location = new Point(6, 105);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(187, 259);
            groupBox4.TabIndex = 24;
            groupBox4.TabStop = false;
            groupBox4.Text = "Inference Settings";
            // 
            // btSampleEditor
            // 
            btSampleEditor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btSampleEditor.Location = new Point(146, 60);
            btSampleEditor.Name = "btSampleEditor";
            btSampleEditor.Size = new Size(35, 20);
            btSampleEditor.TabIndex = 30;
            btSampleEditor.Text = "...";
            btSampleEditor.UseVisualStyleBackColor = true;
            btSampleEditor.Click += btSampleEditor_Click;
            // 
            // btInstructEdit
            // 
            btInstructEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btInstructEdit.Location = new Point(146, 16);
            btInstructEdit.Name = "btInstructEdit";
            btInstructEdit.Size = new Size(35, 20);
            btInstructEdit.TabIndex = 28;
            btInstructEdit.Text = "...";
            btInstructEdit.UseVisualStyleBackColor = true;
            btInstructEdit.Click += btInstructEdit_Click;
            // 
            // ck_ragtothink
            // 
            ck_ragtothink.AutoSize = true;
            ck_ragtothink.Font = new Font("Segoe UI", 9F);
            ck_ragtothink.Location = new Point(6, 231);
            ck_ragtothink.Name = "ck_ragtothink";
            ck_ragtothink.Size = new Size(161, 19);
            ck_ragtothink.TabIndex = 36;
            ck_ragtothink.Text = "Put context in think block";
            ck_ragtothink.UseVisualStyleBackColor = true;
            ck_ragtothink.CheckedChanged += ck_ragtothink_CheckedChanged;
            // 
            // ck_charsampler
            // 
            ck_charsampler.AutoSize = true;
            ck_charsampler.Font = new Font("Segoe UI", 9F);
            ck_charsampler.Location = new Point(6, 156);
            ck_charsampler.Name = "ck_charsampler";
            ck_charsampler.Size = new Size(155, 19);
            ck_charsampler.TabIndex = 26;
            ck_charsampler.Text = "Use character's samplers";
            ck_charsampler.UseVisualStyleBackColor = true;
            // 
            // ck_disablethink
            // 
            ck_disablethink.AutoSize = true;
            ck_disablethink.Font = new Font("Segoe UI", 9F);
            ck_disablethink.Location = new Point(6, 206);
            ck_disablethink.Name = "ck_disablethink";
            ck_disablethink.Size = new Size(92, 19);
            ck_disablethink.TabIndex = 35;
            ck_disablethink.Text = "No Thinking";
            ck_disablethink.UseVisualStyleBackColor = true;
            ck_disablethink.CheckedChanged += ck_disablethink_CheckedChanged;
            // 
            // ck_forceNames
            // 
            ck_forceNames.AutoSize = true;
            ck_forceNames.Font = new Font("Segoe UI", 9F);
            ck_forceNames.Location = new Point(6, 181);
            ck_forceNames.Name = "ck_forceNames";
            ck_forceNames.Size = new Size(143, 19);
            ck_forceNames.TabIndex = 25;
            ck_forceNames.Text = "Add names to prompt";
            ck_forceNames.UseVisualStyleBackColor = true;
            ck_forceNames.CheckedChanged += ck_forceNames_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F);
            label5.Location = new Point(6, 19);
            label5.Name = "label5";
            label5.Size = new Size(105, 15);
            label5.TabIndex = 4;
            label5.Text = "Instruction Format";
            // 
            // cb_instruct
            // 
            cb_instruct.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_instruct.Font = new Font("Segoe UI", 9F);
            cb_instruct.Location = new Point(6, 37);
            cb_instruct.Name = "cb_instruct";
            cb_instruct.Size = new Size(175, 23);
            cb_instruct.TabIndex = 5;
            cb_instruct.SelectedIndexChanged += cb_instruct_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F);
            label6.Location = new Point(4, 65);
            label6.Name = "label6";
            label6.Size = new Size(102, 15);
            label6.TabIndex = 6;
            label6.Text = "Sampling Settings";
            // 
            // cb_infer
            // 
            cb_infer.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_infer.Font = new Font("Segoe UI", 9F);
            cb_infer.Location = new Point(6, 83);
            cb_infer.Name = "cb_infer";
            cb_infer.Size = new Size(175, 23);
            cb_infer.TabIndex = 7;
            cb_infer.SelectedIndexChanged += cb_infer_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F);
            label9.Location = new Point(6, 109);
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
            num_temperature.Location = new Point(6, 127);
            num_temperature.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            num_temperature.Name = "num_temperature";
            num_temperature.Size = new Size(175, 23);
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
            grp_model.Size = new Size(187, 96);
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
            num_maxresponse.Size = new Size(81, 23);
            num_maxresponse.TabIndex = 12;
            num_maxresponse.ThousandsSeparator = true;
            num_maxresponse.Value = new decimal(new int[] { 512, 0, 0, 0 });
            num_maxresponse.ValueChanged += num_maxresponse_ValueChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F);
            label7.Location = new Point(6, 19);
            label7.Name = "label7";
            label7.Size = new Size(73, 15);
            label7.TabIndex = 8;
            label7.Text = "Max Context";
            // 
            // num_maxcontext
            // 
            num_maxcontext.Font = new Font("Segoe UI", 9F);
            num_maxcontext.Increment = new decimal(new int[] { 512, 0, 0, 0 });
            num_maxcontext.Location = new Point(6, 37);
            num_maxcontext.Maximum = new decimal(new int[] { 16384, 0, 0, 0 });
            num_maxcontext.Minimum = new decimal(new int[] { 1024, 0, 0, 0 });
            num_maxcontext.Name = "num_maxcontext";
            num_maxcontext.Size = new Size(88, 23);
            num_maxcontext.TabIndex = 10;
            num_maxcontext.Value = new decimal(new int[] { 16384, 0, 0, 0 });
            num_maxcontext.ValueChanged += num_maxcontext_ValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F);
            label8.Location = new Point(100, 19);
            label8.Name = "label8";
            label8.Size = new Size(76, 15);
            label8.TabIndex = 11;
            label8.Text = "Reply Length";
            // 
            // bt_connect
            // 
            bt_connect.Location = new Point(6, 66);
            bt_connect.Name = "bt_connect";
            bt_connect.Size = new Size(175, 23);
            bt_connect.TabIndex = 14;
            bt_connect.Text = "Connect";
            bt_connect.UseVisualStyleBackColor = true;
            bt_connect.Click += Connect;
            // 
            // tabMain
            // 
            tabMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabMain.Appearance = TabAppearance.FlatButtons;
            tabMain.Controls.Add(tabChat);
            tabMain.Controls.Add(tabHistory);
            tabMain.Controls.Add(tabConsole);
            tabMain.Controls.Add(tabSearch);
            tabMain.Location = new Point(0, 0);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(1063, 928);
            tabMain.TabIndex = 1;
            // 
            // tabSearch
            // 
            tabSearch.Controls.Add(txtSearchRes);
            tabSearch.Controls.Add(textBox1);
            tabSearch.Location = new Point(4, 27);
            tabSearch.Name = "tabSearch";
            tabSearch.Padding = new Padding(3);
            tabSearch.Size = new Size(996, 897);
            tabSearch.TabIndex = 10;
            tabSearch.Text = "RAG Search";
            tabSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearchRes
            // 
            txtSearchRes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtSearchRes.Location = new Point(8, 35);
            txtSearchRes.Multiline = true;
            txtSearchRes.Name = "txtSearchRes";
            txtSearchRes.Size = new Size(980, 846);
            txtSearchRes.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(8, 6);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(980, 23);
            textBox1.TabIndex = 0;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1063, 953);
            Controls.Add(statusbar);
            Controls.Add(tabMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "w(AI)fu";
            FormClosing += MainForm_FormClosing;
            statusbar.ResumeLayout(false);
            statusbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            tabConsole.ResumeLayout(false);
            tabConsole.PerformLayout();
            tabHistory.ResumeLayout(false);
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)web_sessioncontent).EndInit();
            panel7.ResumeLayout(false);
            groupBox25.ResumeLayout(false);
            groupBox25.PerformLayout();
            groupBox12.ResumeLayout(false);
            groupBox12.PerformLayout();
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            tabChat.ResumeLayout(false);
            tabChat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)web_chat).EndInit();
            panel1.ResumeLayout(false);
            groupBox23.ResumeLayout(false);
            groupBox23.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictEmbed).EndInit();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_temperature).EndInit();
            grp_model.ResumeLayout(false);
            grp_model.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_maxresponse).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_maxcontext).EndInit();
            tabMain.ResumeLayout(false);
            tabSearch.ResumeLayout(false);
            tabSearch.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OpenFileDialog openFileDialog1;
        private FontDialog fontDialog1;
        private ToolTip HelptoolTip;
        private StatusStrip statusbar;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private BindingSource bindingSource1;
        internal System.Windows.Forms.Timer AutoTalkTimer;
        private TabPage tabConsole;
        private TextBox ed_log;
        private TabPage tabHistory;
        private Panel panel6;
        private Microsoft.Web.WebView2.WinForms.WebView2 web_sessioncontent;
        private Panel panel7;
        private GroupBox groupBox25;
        private CheckBox ck_hist_casesensitive;
        private CheckBox ck_hist_kw;
        private CheckBox ck_hist_sticky;
        private Label label56;
        private TextBox ed_hist_kw1;
        private Label label57;
        private ComboBox cb_hist_kwlink;
        private Label label58;
        private TextBox ed_hist_kw2;
        private GroupBox groupBox12;
        private Label lbl_sessiondata;
        private TextBox ed_sessioninfo;
        private Label label64;
        private TextBox ed_sessiontitle;
        private Button bt_sessionrefresh;
        private Panel panel4;
        private Panel panel5;
        private Button bt_deleteAllHistory;
        private TabPage tabChat;
        private Button bt_impersonate;
        private Microsoft.Web.WebView2.WinForms.WebView2 web_chat;
        private Button bt_delete;
        private Button bt_reroll;
        private Button bt_send;
        private TextBox ed_input;
        private Panel panel1;
        private GroupBox groupBox23;
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
        private GroupBox groupBox4;
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
        private TabControl tabMain;
        private Button bt_historyupdate;
        private ListView listSession;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Button btEmbedAll;
        private CheckBox ck_disablethink;
        private CheckBox ck_ragtothink;
        private CheckBox ck_hist_isrp;
        private CheckBox ck_agentmode;
        private TabPage tabSearch;
        private TextBox txtSearchRes;
        private TextBox textBox1;
        private Button btInstructEdit;
        private Button btSysPrompt;
        private Button btSampleEditor;
        private Button btMainSettings;
        private Button btWorldEditor;
    }
}
