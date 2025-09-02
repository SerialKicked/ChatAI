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
            tabSettings = new TabPage();
            groupBox24 = new GroupBox();
            num_removeitalicmaxword = new NumericUpDown();
            label63 = new Label();
            ck_oneparagraph = new CheckBox();
            ck_remlastsentence = new CheckBox();
            label62 = new Label();
            num_italicratio = new NumericUpDown();
            ck_reduceitalic = new CheckBox();
            ck_noemphasisword = new CheckBox();
            ck_fixquotes = new CheckBox();
            ck_noquotes = new CheckBox();
            ck_unbold = new CheckBox();
            label61 = new Label();
            num_antislopchance = new NumericUpDown();
            ed_sloplist = new TextBox();
            ck_antislop = new CheckBox();
            ck_fixasterix = new CheckBox();
            groupBox11 = new GroupBox();
            label65 = new Label();
            cb_pastsession = new ComboBox();
            num_memtokens = new NumericUpDown();
            label32 = new Label();
            groupBox10 = new GroupBox();
            num_msgcount = new NumericUpDown();
            label30 = new Label();
            num_fontsize = new NumericUpDown();
            label29 = new Label();
            cb_background = new ComboBox();
            label28 = new Label();
            groupBox9 = new GroupBox();
            ck_webgrammar = new CheckBox();
            ck_webkeyword = new CheckBox();
            groupBox2 = new GroupBox();
            bt_chattosessions = new Button();
            bt_importworld = new Button();
            bt_ImportSTChat = new Button();
            groupBox1 = new GroupBox();
            ck_sysrag = new CheckBox();
            ck_alwayswebsearch = new CheckBox();
            ck_ragdocs = new CheckBox();
            label15 = new Label();
            num_ragindex = new NumericUpDown();
            label14 = new Label();
            num_ragmaxretrieve = new NumericUpDown();
            label13 = new Label();
            num_ragcutoff = new NumericUpDown();
            label12 = new Label();
            cb_ragheuristic = new ComboBox();
            button1 = new Button();
            tabSamplers = new TabPage();
            bt_savesampler = new Button();
            label1 = new Label();
            cb_samplerlist = new ComboBox();
            pan_samplers = new Panel();
            groupBox20 = new GroupBox();
            ck_trimstop = new CheckBox();
            ck_renderspecial = new CheckBox();
            ck_ignoreeos = new CheckBox();
            groupBox19 = new GroupBox();
            num_xtcthres = new NumericUpDown();
            label50 = new Label();
            num_xtcprob = new NumericUpDown();
            label51 = new Label();
            groupBox18 = new GroupBox();
            num_drymul = new NumericUpDown();
            label46 = new Label();
            num_drybase = new NumericUpDown();
            label48 = new Label();
            num_dryrange = new NumericUpDown();
            label49 = new Label();
            groupBox17 = new GroupBox();
            num_smoothfac = new NumericUpDown();
            label59 = new Label();
            num_dynexpo = new NumericUpDown();
            label45 = new Label();
            num_dynrange = new NumericUpDown();
            label47 = new Label();
            groupBox16 = new GroupBox();
            num_meta = new NumericUpDown();
            label42 = new Label();
            num_mtau = new NumericUpDown();
            label44 = new Label();
            cb_miro = new ComboBox();
            label43 = new Label();
            groupBox15 = new GroupBox();
            num_reppenrange = new NumericUpDown();
            label41 = new Label();
            num_reppen = new NumericUpDown();
            label40 = new Label();
            groupBox14 = new GroupBox();
            num_seed = new NumericUpDown();
            label39 = new Label();
            num_temp = new NumericUpDown();
            label38 = new Label();
            groupBox13 = new GroupBox();
            num_tfs = new NumericUpDown();
            label37 = new Label();
            num_typical = new NumericUpDown();
            label36 = new Label();
            num_minp = new NumericUpDown();
            label35 = new Label();
            num_topp = new NumericUpDown();
            label34 = new Label();
            num_topa = new NumericUpDown();
            label33 = new Label();
            num_topk = new NumericUpDown();
            label31 = new Label();
            tabSysPrompt = new TabPage();
            bt_promptsave = new Button();
            label10 = new Label();
            cb_promptlist = new ComboBox();
            pan_prompt = new Panel();
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
            tabWorldInfo = new TabPage();
            panel3 = new Panel();
            groupBox8 = new GroupBox();
            label60 = new Label();
            numWItriggerchance = new NumericUpDown();
            label27 = new Label();
            num_wentrypriority = new NumericUpDown();
            label26 = new Label();
            num_wentryduration = new NumericUpDown();
            ck_wentrycasesensitive = new CheckBox();
            label25 = new Label();
            num_wentryposition = new NumericUpDown();
            label24 = new Label();
            cb_wentrylocation = new ComboBox();
            label23 = new Label();
            cb_wentrykwlink = new ComboBox();
            ed_wentrykw2 = new TextBox();
            label22 = new Label();
            ed_wentrykw1 = new TextBox();
            label21 = new Label();
            ck_wentryenabled = new CheckBox();
            groupBox7 = new GroupBox();
            label20 = new Label();
            ed_wentrymem = new TextBox();
            ed_wentryname = new TextBox();
            label19 = new Label();
            groupBox6 = new GroupBox();
            ck_wiembed = new CheckBox();
            bt_delwentry = new Button();
            bt_addwentry = new Button();
            label18 = new Label();
            lb_worldentries = new ListBox();
            label17 = new Label();
            label16 = new Label();
            num_scandepth = new NumericUpDown();
            ed_worlddesc = new TextBox();
            groupBox3 = new GroupBox();
            bt_worldsave = new Button();
            cb_worlds = new ComboBox();
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
            ck_agentmode = new CheckBox();
            ck_ragtothink = new CheckBox();
            ck_disablethink = new CheckBox();
            bt_clearimg = new Button();
            pictEmbed = new PictureBox();
            ck_ttstoggle = new CheckBox();
            bt_test = new Button();
            ck_onlinerag = new CheckBox();
            ck_caninitchat = new CheckBox();
            ck_worldinfo = new CheckBox();
            ck_ragenabled = new CheckBox();
            ck_sessionmemory = new CheckBox();
            ck_senseoftime = new CheckBox();
            groupBox5 = new GroupBox();
            bt_editchar = new Button();
            bt_scenario = new Button();
            label3 = new Label();
            cb_bot = new ComboBox();
            label4 = new Label();
            cb_user = new ComboBox();
            bt_newsession = new Button();
            label11 = new Label();
            cb_sysprompt = new ComboBox();
            groupBox4 = new GroupBox();
            btInstructEdit = new Button();
            ck_charsampler = new CheckBox();
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
            tabSettings.SuspendLayout();
            groupBox24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_removeitalicmaxword).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_italicratio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_antislopchance).BeginInit();
            groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_memtokens).BeginInit();
            groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_msgcount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_fontsize).BeginInit();
            groupBox9.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_ragindex).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_ragmaxretrieve).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_ragcutoff).BeginInit();
            tabSamplers.SuspendLayout();
            pan_samplers.SuspendLayout();
            groupBox20.SuspendLayout();
            groupBox19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_xtcthres).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_xtcprob).BeginInit();
            groupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_drymul).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_drybase).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_dryrange).BeginInit();
            groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_smoothfac).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_dynexpo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_dynrange).BeginInit();
            groupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_meta).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_mtau).BeginInit();
            groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_reppenrange).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_reppen).BeginInit();
            groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_seed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_temp).BeginInit();
            groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_tfs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_typical).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_minp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_topp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_topa).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_topk).BeginInit();
            tabSysPrompt.SuspendLayout();
            pan_prompt.SuspendLayout();
            groupBox22.SuspendLayout();
            groupBox21.SuspendLayout();
            tabWorldInfo.SuspendLayout();
            panel3.SuspendLayout();
            groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numWItriggerchance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_wentrypriority).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_wentryduration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_wentryposition).BeginInit();
            groupBox7.SuspendLayout();
            groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_scandepth).BeginInit();
            groupBox3.SuspendLayout();
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
            statusbar.Location = new Point(0, 921);
            statusbar.Name = "statusbar";
            statusbar.Size = new Size(1004, 22);
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
            tabConsole.Size = new Size(996, 887);
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
            ed_log.Size = new Size(990, 881);
            ed_log.TabIndex = 3;
            // 
            // tabSettings
            // 
            tabSettings.Controls.Add(groupBox24);
            tabSettings.Controls.Add(groupBox11);
            tabSettings.Controls.Add(groupBox10);
            tabSettings.Controls.Add(groupBox9);
            tabSettings.Controls.Add(groupBox2);
            tabSettings.Controls.Add(groupBox1);
            tabSettings.Location = new Point(4, 27);
            tabSettings.Name = "tabSettings";
            tabSettings.Padding = new Padding(3);
            tabSettings.Size = new Size(996, 887);
            tabSettings.TabIndex = 4;
            tabSettings.Text = "Settings";
            tabSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox24
            // 
            groupBox24.Controls.Add(num_removeitalicmaxword);
            groupBox24.Controls.Add(label63);
            groupBox24.Controls.Add(ck_oneparagraph);
            groupBox24.Controls.Add(ck_remlastsentence);
            groupBox24.Controls.Add(label62);
            groupBox24.Controls.Add(num_italicratio);
            groupBox24.Controls.Add(ck_reduceitalic);
            groupBox24.Controls.Add(ck_noemphasisword);
            groupBox24.Controls.Add(ck_fixquotes);
            groupBox24.Controls.Add(ck_noquotes);
            groupBox24.Controls.Add(ck_unbold);
            groupBox24.Controls.Add(label61);
            groupBox24.Controls.Add(num_antislopchance);
            groupBox24.Controls.Add(ed_sloplist);
            groupBox24.Controls.Add(ck_antislop);
            groupBox24.Controls.Add(ck_fixasterix);
            groupBox24.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox24.Location = new Point(416, 124);
            groupBox24.Name = "groupBox24";
            groupBox24.Size = new Size(402, 352);
            groupBox24.TabIndex = 29;
            groupBox24.TabStop = false;
            groupBox24.Text = "Output Formatting";
            // 
            // num_removeitalicmaxword
            // 
            num_removeitalicmaxword.CausesValidation = false;
            num_removeitalicmaxword.Font = new Font("Segoe UI", 9F);
            num_removeitalicmaxword.Location = new Point(273, 259);
            num_removeitalicmaxword.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            num_removeitalicmaxword.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_removeitalicmaxword.Name = "num_removeitalicmaxword";
            num_removeitalicmaxword.Size = new Size(67, 23);
            num_removeitalicmaxword.TabIndex = 40;
            num_removeitalicmaxword.Value = new decimal(new int[] { 1, 0, 0, 0 });
            num_removeitalicmaxword.ValueChanged += num_removeitalicmaxword_ValueChanged;
            // 
            // label63
            // 
            label63.AutoSize = true;
            label63.Font = new Font("Segoe UI", 9F);
            label63.Location = new Point(202, 262);
            label63.Name = "label63";
            label63.Size = new Size(66, 15);
            label63.TabIndex = 39;
            label63.Text = "Max Words";
            // 
            // ck_oneparagraph
            // 
            ck_oneparagraph.AutoSize = true;
            ck_oneparagraph.Font = new Font("Segoe UI", 9F);
            ck_oneparagraph.Location = new Point(6, 313);
            ck_oneparagraph.Name = "ck_oneparagraph";
            ck_oneparagraph.Size = new Size(203, 19);
            ck_oneparagraph.TabIndex = 38;
            ck_oneparagraph.Text = "Stop generation at first paragraph";
            ck_oneparagraph.UseVisualStyleBackColor = true;
            ck_oneparagraph.CheckedChanged += ck_oneparagraph_CheckedChanged;
            // 
            // ck_remlastsentence
            // 
            ck_remlastsentence.AutoSize = true;
            ck_remlastsentence.Font = new Font("Segoe UI", 9F);
            ck_remlastsentence.Location = new Point(6, 288);
            ck_remlastsentence.Name = "ck_remlastsentence";
            ck_remlastsentence.Size = new Size(275, 19);
            ck_remlastsentence.TabIndex = 37;
            ck_remlastsentence.Text = "If output > length, remove unfinished sentence";
            ck_remlastsentence.UseVisualStyleBackColor = true;
            ck_remlastsentence.CheckedChanged += ck_remlastsentence_CheckedChanged;
            // 
            // label62
            // 
            label62.AutoSize = true;
            label62.Font = new Font("Segoe UI", 9F);
            label62.Location = new Point(27, 262);
            label62.Name = "label62";
            label62.Size = new Size(96, 15);
            label62.TabIndex = 36;
            label62.Text = "Removal Chance";
            // 
            // num_italicratio
            // 
            num_italicratio.CausesValidation = false;
            num_italicratio.DecimalPlaces = 2;
            num_italicratio.Font = new Font("Segoe UI", 9F);
            num_italicratio.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_italicratio.Location = new Point(129, 259);
            num_italicratio.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_italicratio.Name = "num_italicratio";
            num_italicratio.Size = new Size(67, 23);
            num_italicratio.TabIndex = 35;
            num_italicratio.Value = new decimal(new int[] { 1, 0, 0, 0 });
            num_italicratio.ValueChanged += num_italicratio_ValueChanged;
            // 
            // ck_reduceitalic
            // 
            ck_reduceitalic.AutoSize = true;
            ck_reduceitalic.Font = new Font("Segoe UI", 9F);
            ck_reduceitalic.Location = new Point(6, 234);
            ck_reduceitalic.Name = "ck_reduceitalic";
            ck_reduceitalic.Size = new Size(270, 19);
            ck_reduceitalic.TabIndex = 34;
            ck_reduceitalic.Text = "Remove a ratio of italic sentences from output";
            ck_reduceitalic.UseVisualStyleBackColor = true;
            ck_reduceitalic.CheckedChanged += ck_reduceitalic_CheckedChanged;
            // 
            // ck_noemphasisword
            // 
            ck_noemphasisword.AutoSize = true;
            ck_noemphasisword.Font = new Font("Segoe UI", 9F);
            ck_noemphasisword.Location = new Point(6, 209);
            ck_noemphasisword.Name = "ck_noemphasisword";
            ck_noemphasisword.Size = new Size(334, 19);
            ck_noemphasisword.TabIndex = 33;
            ck_noemphasisword.Text = "Don't emphasis single words (useful for QwQ / R1 models)";
            ck_noemphasisword.UseVisualStyleBackColor = true;
            ck_noemphasisword.CheckedChanged += ck_noemphasisword_CheckedChanged;
            // 
            // ck_fixquotes
            // 
            ck_fixquotes.AutoSize = true;
            ck_fixquotes.Font = new Font("Segoe UI", 9F);
            ck_fixquotes.Location = new Point(6, 184);
            ck_fixquotes.Name = "ck_fixquotes";
            ck_fixquotes.Size = new Size(260, 19);
            ck_fixquotes.TabIndex = 32;
            ck_fixquotes.Text = "Fix quoted text (useful for QwQ / R1 models)";
            ck_fixquotes.UseVisualStyleBackColor = true;
            ck_fixquotes.CheckedChanged += ck_fixquotes_CheckedChanged;
            // 
            // ck_noquotes
            // 
            ck_noquotes.AutoSize = true;
            ck_noquotes.Font = new Font("Segoe UI", 9F);
            ck_noquotes.Location = new Point(6, 159);
            ck_noquotes.Name = "ck_noquotes";
            ck_noquotes.Size = new Size(300, 19);
            ck_noquotes.TabIndex = 31;
            ck_noquotes.Text = "Don't use quotes (quotation marks will be removed)";
            ck_noquotes.UseVisualStyleBackColor = true;
            ck_noquotes.CheckedChanged += ck_noquotes_CheckedChanged;
            // 
            // ck_unbold
            // 
            ck_unbold.AutoSize = true;
            ck_unbold.Font = new Font("Segoe UI", 9F);
            ck_unbold.Location = new Point(6, 134);
            ck_unbold.Name = "ck_unbold";
            ck_unbold.Size = new Size(316, 19);
            ck_unbold.TabIndex = 30;
            ck_unbold.Text = "Don't bold text (any text in bold turned back to regular)";
            ck_unbold.UseVisualStyleBackColor = true;
            ck_unbold.CheckedChanged += ck_unbold_CheckedChanged;
            // 
            // label61
            // 
            label61.AutoSize = true;
            label61.Font = new Font("Segoe UI", 9F);
            label61.Location = new Point(27, 104);
            label61.Name = "label61";
            label61.Size = new Size(96, 15);
            label61.TabIndex = 29;
            label61.Text = "Removal Chance";
            // 
            // num_antislopchance
            // 
            num_antislopchance.CausesValidation = false;
            num_antislopchance.DecimalPlaces = 2;
            num_antislopchance.Font = new Font("Segoe UI", 9F);
            num_antislopchance.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_antislopchance.Location = new Point(129, 101);
            num_antislopchance.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_antislopchance.Name = "num_antislopchance";
            num_antislopchance.Size = new Size(67, 23);
            num_antislopchance.TabIndex = 28;
            num_antislopchance.Value = new decimal(new int[] { 1, 0, 0, 0 });
            num_antislopchance.ValueChanged += num_antislopchance_ValueChanged;
            // 
            // ed_sloplist
            // 
            ed_sloplist.Location = new Point(27, 72);
            ed_sloplist.Name = "ed_sloplist";
            ed_sloplist.PlaceholderText = "comma separated list of words to filter out";
            ed_sloplist.Size = new Size(352, 23);
            ed_sloplist.TabIndex = 2;
            ed_sloplist.TextChanged += ed_sloplist_TextChanged;
            // 
            // ck_antislop
            // 
            ck_antislop.AutoSize = true;
            ck_antislop.Font = new Font("Segoe UI", 9F);
            ck_antislop.Location = new Point(6, 47);
            ck_antislop.Name = "ck_antislop";
            ck_antislop.Size = new Size(248, 19);
            ck_antislop.TabIndex = 1;
            ck_antislop.Text = "Remove words from list (ad-hoc anti slop)";
            ck_antislop.UseVisualStyleBackColor = true;
            ck_antislop.CheckedChanged += ck_antislop_CheckedChanged;
            // 
            // ck_fixasterix
            // 
            ck_fixasterix.AutoSize = true;
            ck_fixasterix.Font = new Font("Segoe UI", 9F);
            ck_fixasterix.Location = new Point(6, 22);
            ck_fixasterix.Name = "ck_fixasterix";
            ck_fixasterix.Size = new Size(190, 19);
            ck_fixasterix.TabIndex = 0;
            ck_fixasterix.Text = "Attempt to fix missing asterisks";
            ck_fixasterix.UseVisualStyleBackColor = true;
            ck_fixasterix.CheckedChanged += ck_fixasterix_CheckedChanged;
            // 
            // groupBox11
            // 
            groupBox11.Controls.Add(label65);
            groupBox11.Controls.Add(cb_pastsession);
            groupBox11.Controls.Add(num_memtokens);
            groupBox11.Controls.Add(label32);
            groupBox11.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox11.Location = new Point(416, 6);
            groupBox11.Name = "groupBox11";
            groupBox11.Size = new Size(402, 116);
            groupBox11.TabIndex = 27;
            groupBox11.TabStop = false;
            groupBox11.Text = "Session Memory System";
            // 
            // label65
            // 
            label65.AutoSize = true;
            label65.Font = new Font("Segoe UI", 9F);
            label65.Location = new Point(6, 66);
            label65.Name = "label65";
            label65.Size = new Size(135, 15);
            label65.TabIndex = 34;
            label65.Text = "Handling of chat history";
            // 
            // cb_pastsession
            // 
            cb_pastsession.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_pastsession.Font = new Font("Segoe UI", 9F);
            cb_pastsession.Items.AddRange(new object[] { "Current session only", "Fit as much as possible, including previous sessions" });
            cb_pastsession.Location = new Point(6, 84);
            cb_pastsession.Name = "cb_pastsession";
            cb_pastsession.Size = new Size(373, 23);
            cb_pastsession.TabIndex = 33;
            cb_pastsession.SelectedIndexChanged += cb_pastsession_SelectedIndexChanged;
            // 
            // num_memtokens
            // 
            num_memtokens.Font = new Font("Segoe UI", 9F);
            num_memtokens.Increment = new decimal(new int[] { 512, 0, 0, 0 });
            num_memtokens.Location = new Point(6, 40);
            num_memtokens.Maximum = new decimal(new int[] { 128000, 0, 0, 0 });
            num_memtokens.Minimum = new decimal(new int[] { 512, 0, 0, 0 });
            num_memtokens.Name = "num_memtokens";
            num_memtokens.Size = new Size(125, 23);
            num_memtokens.TabIndex = 27;
            num_memtokens.Value = new decimal(new int[] { 2048, 0, 0, 0 });
            num_memtokens.ValueChanged += num_memtokens_ValueChanged;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Font = new Font("Segoe UI", 9F);
            label32.Location = new Point(6, 22);
            label32.Name = "label32";
            label32.Size = new Size(258, 15);
            label32.TabIndex = 28;
            label32.Text = "Reserved Tokens for summaries of past sessions";
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(num_msgcount);
            groupBox10.Controls.Add(label30);
            groupBox10.Controls.Add(num_fontsize);
            groupBox10.Controls.Add(label29);
            groupBox10.Controls.Add(cb_background);
            groupBox10.Controls.Add(label28);
            groupBox10.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox10.Location = new Point(8, 409);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(402, 67);
            groupBox10.TabIndex = 26;
            groupBox10.TabStop = false;
            groupBox10.Text = "User Interface";
            // 
            // num_msgcount
            // 
            num_msgcount.Font = new Font("Segoe UI", 9F);
            num_msgcount.Increment = new decimal(new int[] { 20, 0, 0, 0 });
            num_msgcount.Location = new Point(292, 37);
            num_msgcount.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            num_msgcount.Minimum = new decimal(new int[] { 20, 0, 0, 0 });
            num_msgcount.Name = "num_msgcount";
            num_msgcount.Size = new Size(79, 23);
            num_msgcount.TabIndex = 29;
            num_msgcount.Value = new decimal(new int[] { 20, 0, 0, 0 });
            num_msgcount.ValueChanged += num_msgcount_ValueChanged;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new Font("Segoe UI", 9F);
            label30.Location = new Point(292, 18);
            label30.Name = "label30";
            label30.Size = new Size(97, 15);
            label30.TabIndex = 30;
            label30.Text = "Shown Messages";
            // 
            // num_fontsize
            // 
            num_fontsize.Font = new Font("Segoe UI", 9F);
            num_fontsize.Location = new Point(207, 37);
            num_fontsize.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            num_fontsize.Minimum = new decimal(new int[] { 6, 0, 0, 0 });
            num_fontsize.Name = "num_fontsize";
            num_fontsize.Size = new Size(79, 23);
            num_fontsize.TabIndex = 27;
            num_fontsize.Value = new decimal(new int[] { 7, 0, 0, 0 });
            num_fontsize.ValueChanged += num_fontsize_ValueChanged;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("Segoe UI", 9F);
            label29.Location = new Point(207, 19);
            label29.Name = "label29";
            label29.Size = new Size(54, 15);
            label29.TabIndex = 28;
            label29.Text = "Font Size";
            // 
            // cb_background
            // 
            cb_background.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_background.Font = new Font("Segoe UI", 9F);
            cb_background.Location = new Point(6, 37);
            cb_background.Name = "cb_background";
            cb_background.Size = new Size(195, 23);
            cb_background.TabIndex = 27;
            cb_background.SelectedIndexChanged += cb_background_SelectedIndexChanged;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("Segoe UI", 9F);
            label28.Location = new Point(6, 19);
            label28.Name = "label28";
            label28.Size = new Size(99, 15);
            label28.TabIndex = 26;
            label28.Text = "Chat Background";
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(ck_webgrammar);
            groupBox9.Controls.Add(ck_webkeyword);
            groupBox9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox9.Location = new Point(8, 213);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(402, 95);
            groupBox9.TabIndex = 25;
            groupBox9.TabStop = false;
            groupBox9.Text = "Website Browsing Plugin";
            // 
            // ck_webgrammar
            // 
            ck_webgrammar.AutoSize = true;
            ck_webgrammar.Font = new Font("Segoe UI", 9F);
            ck_webgrammar.Location = new Point(6, 47);
            ck_webgrammar.Name = "ck_webgrammar";
            ck_webgrammar.Size = new Size(119, 19);
            ck_webgrammar.TabIndex = 33;
            ck_webgrammar.Text = "Enforce Grammar";
            ck_webgrammar.UseVisualStyleBackColor = true;
            ck_webgrammar.CheckedChanged += ck_webkeyword_CheckedChanged;
            // 
            // ck_webkeyword
            // 
            ck_webkeyword.AutoSize = true;
            ck_webkeyword.Font = new Font("Segoe UI", 9F);
            ck_webkeyword.Location = new Point(6, 22);
            ck_webkeyword.Name = "ck_webkeyword";
            ck_webkeyword.Size = new Size(123, 19);
            ck_webkeyword.TabIndex = 32;
            ck_webkeyword.Text = "Keyword activated";
            ck_webkeyword.UseVisualStyleBackColor = true;
            ck_webkeyword.CheckedChanged += ck_webkeyword_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(bt_chattosessions);
            groupBox2.Controls.Add(bt_importworld);
            groupBox2.Controls.Add(bt_ImportSTChat);
            groupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox2.Location = new Point(8, 314);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(402, 89);
            groupBox2.TabIndex = 24;
            groupBox2.TabStop = false;
            groupBox2.Text = "Import";
            // 
            // bt_chattosessions
            // 
            bt_chattosessions.Font = new Font("Segoe UI", 9F);
            bt_chattosessions.ForeColor = Color.Red;
            bt_chattosessions.Location = new Point(6, 51);
            bt_chattosessions.Name = "bt_chattosessions";
            bt_chattosessions.Size = new Size(195, 23);
            bt_chattosessions.TabIndex = 22;
            bt_chattosessions.Text = "Raw chat to session list";
            bt_chattosessions.UseVisualStyleBackColor = true;
            // 
            // bt_importworld
            // 
            bt_importworld.Font = new Font("Segoe UI", 9F);
            bt_importworld.Location = new Point(207, 22);
            bt_importworld.Name = "bt_importworld";
            bt_importworld.Size = new Size(189, 23);
            bt_importworld.TabIndex = 2;
            bt_importworld.Text = "Import ST WorldInfo";
            bt_importworld.UseVisualStyleBackColor = true;
            bt_importworld.Click += bt_importworld_Click;
            // 
            // bt_ImportSTChat
            // 
            bt_ImportSTChat.Font = new Font("Segoe UI", 9F);
            bt_ImportSTChat.Location = new Point(6, 22);
            bt_ImportSTChat.Name = "bt_ImportSTChat";
            bt_ImportSTChat.Size = new Size(195, 23);
            bt_ImportSTChat.TabIndex = 1;
            bt_ImportSTChat.Text = "Import ST Chat";
            bt_ImportSTChat.UseVisualStyleBackColor = true;
            bt_ImportSTChat.Click += bt_ImportSTChat_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ck_sysrag);
            groupBox1.Controls.Add(ck_alwayswebsearch);
            groupBox1.Controls.Add(ck_ragdocs);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(num_ragindex);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(num_ragmaxretrieve);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(num_ragcutoff);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(cb_ragheuristic);
            groupBox1.Controls.Add(button1);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.Location = new Point(8, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(402, 201);
            groupBox1.TabIndex = 23;
            groupBox1.TabStop = false;
            groupBox1.Text = "RAG System";
            // 
            // ck_sysrag
            // 
            ck_sysrag.AutoSize = true;
            ck_sysrag.Font = new Font("Segoe UI", 9F);
            ck_sysrag.Location = new Point(195, 47);
            ck_sysrag.Name = "ck_sysrag";
            ck_sysrag.Size = new Size(196, 19);
            ck_sysrag.TabIndex = 36;
            ck_sysrag.Text = "Force All RAG to System Prompt";
            ck_sysrag.UseVisualStyleBackColor = true;
            ck_sysrag.CheckedChanged += ck_sysrag_CheckedChanged;
            // 
            // ck_alwayswebsearch
            // 
            ck_alwayswebsearch.AutoSize = true;
            ck_alwayswebsearch.Font = new Font("Segoe UI", 9F);
            ck_alwayswebsearch.Location = new Point(195, 22);
            ck_alwayswebsearch.Name = "ck_alwayswebsearch";
            ck_alwayswebsearch.Size = new Size(170, 19);
            ck_alwayswebsearch.TabIndex = 35;
            ck_alwayswebsearch.Text = "No keyword for online RAG";
            ck_alwayswebsearch.UseVisualStyleBackColor = true;
            ck_alwayswebsearch.CheckedChanged += ck_alwayswebsearch_CheckedChanged;
            // 
            // ck_ragdocs
            // 
            ck_ragdocs.AutoSize = true;
            ck_ragdocs.Enabled = false;
            ck_ragdocs.Font = new Font("Segoe UI", 9F);
            ck_ragdocs.Location = new Point(195, 72);
            ck_ragdocs.Name = "ck_ragdocs";
            ck_ragdocs.Size = new Size(125, 19);
            ck_ragdocs.TabIndex = 29;
            ck_ragdocs.Text = "Search Documents";
            ck_ragdocs.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F);
            label15.Location = new Point(6, 63);
            label15.Name = "label15";
            label15.Size = new Size(98, 15);
            label15.TabIndex = 28;
            label15.Text = "Placement Depth";
            // 
            // num_ragindex
            // 
            num_ragindex.Font = new Font("Segoe UI", 9F);
            num_ragindex.Location = new Point(6, 81);
            num_ragindex.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_ragindex.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            num_ragindex.Name = "num_ragindex";
            num_ragindex.Size = new Size(144, 23);
            num_ragindex.TabIndex = 27;
            num_ragindex.Value = new decimal(new int[] { 1, 0, 0, 0 });
            num_ragindex.ValueChanged += num_ragindex_ValueChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F);
            label14.Location = new Point(6, 107);
            label14.Name = "label14";
            label14.Size = new Size(63, 15);
            label14.TabIndex = 26;
            label14.Text = "Max count";
            // 
            // num_ragmaxretrieve
            // 
            num_ragmaxretrieve.Font = new Font("Segoe UI", 9F);
            num_ragmaxretrieve.Location = new Point(6, 125);
            num_ragmaxretrieve.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_ragmaxretrieve.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_ragmaxretrieve.Name = "num_ragmaxretrieve";
            num_ragmaxretrieve.Size = new Size(144, 23);
            num_ragmaxretrieve.TabIndex = 25;
            num_ragmaxretrieve.Value = new decimal(new int[] { 1, 0, 0, 0 });
            num_ragmaxretrieve.ValueChanged += num_ragmaxretrieve_ValueChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F);
            label13.Location = new Point(6, 151);
            label13.Name = "label13";
            label13.Size = new Size(123, 15);
            label13.TabIndex = 24;
            label13.Text = "Distance cut-off point";
            // 
            // num_ragcutoff
            // 
            num_ragcutoff.DecimalPlaces = 3;
            num_ragcutoff.Font = new Font("Segoe UI", 9F);
            num_ragcutoff.Increment = new decimal(new int[] { 5, 0, 0, 196608 });
            num_ragcutoff.Location = new Point(6, 169);
            num_ragcutoff.Maximum = new decimal(new int[] { 5, 0, 0, 65536 });
            num_ragcutoff.Minimum = new decimal(new int[] { 5, 0, 0, 196608 });
            num_ragcutoff.Name = "num_ragcutoff";
            num_ragcutoff.Size = new Size(144, 23);
            num_ragcutoff.TabIndex = 23;
            num_ragcutoff.Value = new decimal(new int[] { 2, 0, 0, 65536 });
            num_ragcutoff.ValueChanged += num_ragcutoff_ValueChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F);
            label12.Location = new Point(6, 19);
            label12.Name = "label12";
            label12.Size = new Size(125, 15);
            label12.TabIndex = 4;
            label12.Text = "RAG Heuristic Method";
            // 
            // cb_ragheuristic
            // 
            cb_ragheuristic.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_ragheuristic.Font = new Font("Segoe UI", 9F);
            cb_ragheuristic.Items.AddRange(new object[] { "Heuristic", "Simple" });
            cb_ragheuristic.Location = new Point(6, 37);
            cb_ragheuristic.Name = "cb_ragheuristic";
            cb_ragheuristic.Size = new Size(144, 23);
            cb_ragheuristic.TabIndex = 3;
            cb_ragheuristic.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Location = new Point(177, 167);
            button1.Name = "button1";
            button1.Size = new Size(214, 23);
            button1.TabIndex = 2;
            button1.Text = "Apply RAG Settings";
            button1.UseVisualStyleBackColor = true;
            button1.Click += ApplyRAGSettings;
            // 
            // tabSamplers
            // 
            tabSamplers.Controls.Add(bt_savesampler);
            tabSamplers.Controls.Add(label1);
            tabSamplers.Controls.Add(cb_samplerlist);
            tabSamplers.Controls.Add(pan_samplers);
            tabSamplers.Location = new Point(4, 27);
            tabSamplers.Name = "tabSamplers";
            tabSamplers.Padding = new Padding(3);
            tabSamplers.Size = new Size(996, 887);
            tabSamplers.TabIndex = 3;
            tabSamplers.Text = "Samplers";
            tabSamplers.UseVisualStyleBackColor = true;
            // 
            // bt_savesampler
            // 
            bt_savesampler.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bt_savesampler.Location = new Point(606, 8);
            bt_savesampler.Name = "bt_savesampler";
            bt_savesampler.Size = new Size(75, 23);
            bt_savesampler.TabIndex = 5;
            bt_savesampler.Text = "Save";
            bt_savesampler.UseVisualStyleBackColor = true;
            bt_savesampler.Click += bt_savesampler_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(8, 8);
            label1.Name = "label1";
            label1.Size = new Size(68, 23);
            label1.TabIndex = 2;
            label1.Text = "Sampler";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cb_samplerlist
            // 
            cb_samplerlist.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_samplerlist.FormattingEnabled = true;
            cb_samplerlist.Location = new Point(118, 8);
            cb_samplerlist.Name = "cb_samplerlist";
            cb_samplerlist.Size = new Size(482, 23);
            cb_samplerlist.TabIndex = 1;
            // 
            // pan_samplers
            // 
            pan_samplers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pan_samplers.AutoScroll = true;
            pan_samplers.Controls.Add(groupBox20);
            pan_samplers.Controls.Add(groupBox19);
            pan_samplers.Controls.Add(groupBox18);
            pan_samplers.Controls.Add(groupBox17);
            pan_samplers.Controls.Add(groupBox16);
            pan_samplers.Controls.Add(groupBox15);
            pan_samplers.Controls.Add(groupBox14);
            pan_samplers.Controls.Add(groupBox13);
            pan_samplers.Location = new Point(8, 38);
            pan_samplers.Name = "pan_samplers";
            pan_samplers.Size = new Size(982, 686);
            pan_samplers.TabIndex = 0;
            // 
            // groupBox20
            // 
            groupBox20.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox20.Controls.Add(ck_trimstop);
            groupBox20.Controls.Add(ck_renderspecial);
            groupBox20.Controls.Add(ck_ignoreeos);
            groupBox20.Location = new Point(736, 71);
            groupBox20.Name = "groupBox20";
            groupBox20.Size = new Size(229, 300);
            groupBox20.TabIndex = 7;
            groupBox20.TabStop = false;
            groupBox20.Text = "Misc Settings";
            // 
            // ck_trimstop
            // 
            ck_trimstop.AutoSize = true;
            ck_trimstop.Location = new Point(6, 76);
            ck_trimstop.Name = "ck_trimstop";
            ck_trimstop.Size = new Size(136, 19);
            ck_trimstop.TabIndex = 18;
            ck_trimstop.Text = "Trim Stop Sequences";
            ck_trimstop.UseVisualStyleBackColor = true;
            // 
            // ck_renderspecial
            // 
            ck_renderspecial.AutoSize = true;
            ck_renderspecial.Location = new Point(6, 51);
            ck_renderspecial.Name = "ck_renderspecial";
            ck_renderspecial.Size = new Size(143, 19);
            ck_renderspecial.TabIndex = 17;
            ck_renderspecial.Text = "Render Special Tokens";
            ck_renderspecial.UseVisualStyleBackColor = true;
            // 
            // ck_ignoreeos
            // 
            ck_ignoreeos.AutoSize = true;
            ck_ignoreeos.Location = new Point(6, 26);
            ck_ignoreeos.Name = "ck_ignoreeos";
            ck_ignoreeos.Size = new Size(119, 19);
            ck_ignoreeos.TabIndex = 16;
            ck_ignoreeos.Text = "Ignore EOS Token";
            ck_ignoreeos.UseVisualStyleBackColor = true;
            // 
            // groupBox19
            // 
            groupBox19.Controls.Add(num_xtcthres);
            groupBox19.Controls.Add(label50);
            groupBox19.Controls.Add(num_xtcprob);
            groupBox19.Controls.Add(label51);
            groupBox19.Location = new Point(487, 189);
            groupBox19.Name = "groupBox19";
            groupBox19.Size = new Size(243, 182);
            groupBox19.TabIndex = 6;
            groupBox19.TabStop = false;
            groupBox19.Text = "Exclude Top Tokens (XTC)";
            // 
            // num_xtcthres
            // 
            num_xtcthres.DecimalPlaces = 2;
            num_xtcthres.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_xtcthres.Location = new Point(120, 51);
            num_xtcthres.Maximum = new decimal(new int[] { 5, 0, 0, 65536 });
            num_xtcthres.Name = "num_xtcthres";
            num_xtcthres.Size = new Size(110, 23);
            num_xtcthres.TabIndex = 15;
            num_xtcthres.Value = new decimal(new int[] { 15, 0, 0, 131072 });
            // 
            // label50
            // 
            label50.AutoSize = true;
            label50.Location = new Point(9, 53);
            label50.Name = "label50";
            label50.Size = new Size(60, 15);
            label50.TabIndex = 14;
            label50.Text = "Threshold";
            // 
            // num_xtcprob
            // 
            num_xtcprob.DecimalPlaces = 2;
            num_xtcprob.Location = new Point(120, 22);
            num_xtcprob.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_xtcprob.Name = "num_xtcprob";
            num_xtcprob.Size = new Size(110, 23);
            num_xtcprob.TabIndex = 13;
            // 
            // label51
            // 
            label51.AutoSize = true;
            label51.Location = new Point(9, 24);
            label51.Name = "label51";
            label51.Size = new Size(64, 15);
            label51.TabIndex = 12;
            label51.Text = "Probability";
            // 
            // groupBox18
            // 
            groupBox18.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox18.Controls.Add(num_drymul);
            groupBox18.Controls.Add(label46);
            groupBox18.Controls.Add(num_drybase);
            groupBox18.Controls.Add(label48);
            groupBox18.Controls.Add(num_dryrange);
            groupBox18.Controls.Add(label49);
            groupBox18.Location = new Point(487, 71);
            groupBox18.Name = "groupBox18";
            groupBox18.Size = new Size(269, 112);
            groupBox18.TabIndex = 5;
            groupBox18.TabStop = false;
            groupBox18.Text = "DRY Anti Repetition";
            // 
            // num_drymul
            // 
            num_drymul.DecimalPlaces = 2;
            num_drymul.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_drymul.Location = new Point(120, 22);
            num_drymul.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_drymul.Name = "num_drymul";
            num_drymul.Size = new Size(110, 23);
            num_drymul.TabIndex = 11;
            // 
            // label46
            // 
            label46.AutoSize = true;
            label46.Location = new Point(9, 24);
            label46.Name = "label46";
            label46.Size = new Size(58, 15);
            label46.TabIndex = 10;
            label46.Text = "Multiplier";
            // 
            // num_drybase
            // 
            num_drybase.DecimalPlaces = 2;
            num_drybase.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_drybase.Location = new Point(120, 51);
            num_drybase.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_drybase.Name = "num_drybase";
            num_drybase.Size = new Size(110, 23);
            num_drybase.TabIndex = 9;
            num_drybase.Value = new decimal(new int[] { 175, 0, 0, 131072 });
            // 
            // label48
            // 
            label48.AutoSize = true;
            label48.Location = new Point(9, 53);
            label48.Name = "label48";
            label48.Size = new Size(31, 15);
            label48.TabIndex = 8;
            label48.Text = "Base";
            // 
            // num_dryrange
            // 
            num_dryrange.Location = new Point(120, 80);
            num_dryrange.Maximum = new decimal(new int[] { 128000, 0, 0, 0 });
            num_dryrange.Name = "num_dryrange";
            num_dryrange.Size = new Size(110, 23);
            num_dryrange.TabIndex = 7;
            // 
            // label49
            // 
            label49.AutoSize = true;
            label49.Location = new Point(9, 80);
            label49.Name = "label49";
            label49.Size = new Size(40, 15);
            label49.TabIndex = 6;
            label49.Text = "Range";
            // 
            // groupBox17
            // 
            groupBox17.Controls.Add(num_smoothfac);
            groupBox17.Controls.Add(label59);
            groupBox17.Controls.Add(num_dynexpo);
            groupBox17.Controls.Add(label45);
            groupBox17.Controls.Add(num_dynrange);
            groupBox17.Controls.Add(label47);
            groupBox17.Location = new Point(245, 189);
            groupBox17.Name = "groupBox17";
            groupBox17.Size = new Size(236, 182);
            groupBox17.TabIndex = 4;
            groupBox17.TabStop = false;
            groupBox17.Text = "Dynamic Temperature";
            // 
            // num_smoothfac
            // 
            num_smoothfac.DecimalPlaces = 2;
            num_smoothfac.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_smoothfac.Location = new Point(120, 80);
            num_smoothfac.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_smoothfac.Name = "num_smoothfac";
            num_smoothfac.Size = new Size(110, 23);
            num_smoothfac.TabIndex = 13;
            // 
            // label59
            // 
            label59.AutoSize = true;
            label59.Location = new Point(9, 82);
            label59.Name = "label59";
            label59.Size = new Size(85, 15);
            label59.TabIndex = 12;
            label59.Text = "Smooth Factor";
            label59.Click += label59_Click;
            // 
            // num_dynexpo
            // 
            num_dynexpo.DecimalPlaces = 2;
            num_dynexpo.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_dynexpo.Location = new Point(120, 51);
            num_dynexpo.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_dynexpo.Name = "num_dynexpo";
            num_dynexpo.Size = new Size(110, 23);
            num_dynexpo.TabIndex = 11;
            num_dynexpo.Value = new decimal(new int[] { 9, 0, 0, 65536 });
            // 
            // label45
            // 
            label45.AutoSize = true;
            label45.Location = new Point(9, 53);
            label45.Name = "label45";
            label45.Size = new Size(56, 15);
            label45.TabIndex = 10;
            label45.Text = "Exponent";
            // 
            // num_dynrange
            // 
            num_dynrange.DecimalPlaces = 2;
            num_dynrange.Location = new Point(120, 22);
            num_dynrange.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            num_dynrange.Name = "num_dynrange";
            num_dynrange.Size = new Size(110, 23);
            num_dynrange.TabIndex = 7;
            // 
            // label47
            // 
            label47.AutoSize = true;
            label47.Location = new Point(9, 24);
            label47.Name = "label47";
            label47.Size = new Size(40, 15);
            label47.TabIndex = 6;
            label47.Text = "Range";
            // 
            // groupBox16
            // 
            groupBox16.Controls.Add(num_meta);
            groupBox16.Controls.Add(label42);
            groupBox16.Controls.Add(num_mtau);
            groupBox16.Controls.Add(label44);
            groupBox16.Controls.Add(cb_miro);
            groupBox16.Controls.Add(label43);
            groupBox16.Location = new Point(245, 71);
            groupBox16.Name = "groupBox16";
            groupBox16.Size = new Size(236, 112);
            groupBox16.TabIndex = 3;
            groupBox16.TabStop = false;
            groupBox16.Text = "Mirostat Sampler";
            // 
            // num_meta
            // 
            num_meta.DecimalPlaces = 2;
            num_meta.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_meta.Location = new Point(117, 80);
            num_meta.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_meta.Name = "num_meta";
            num_meta.Size = new Size(110, 23);
            num_meta.TabIndex = 12;
            num_meta.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // label42
            // 
            label42.AutoSize = true;
            label42.Location = new Point(6, 82);
            label42.Name = "label42";
            label42.Size = new Size(23, 15);
            label42.TabIndex = 11;
            label42.Text = "Eta";
            // 
            // num_mtau
            // 
            num_mtau.DecimalPlaces = 2;
            num_mtau.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_mtau.Location = new Point(117, 51);
            num_mtau.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_mtau.Name = "num_mtau";
            num_mtau.Size = new Size(110, 23);
            num_mtau.TabIndex = 10;
            num_mtau.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label44
            // 
            label44.AutoSize = true;
            label44.Location = new Point(6, 53);
            label44.Name = "label44";
            label44.Size = new Size(26, 15);
            label44.TabIndex = 9;
            label44.Text = "Tau";
            // 
            // cb_miro
            // 
            cb_miro.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_miro.FormattingEnabled = true;
            cb_miro.Items.AddRange(new object[] { "Disabled", "v1", "v2" });
            cb_miro.Location = new Point(117, 22);
            cb_miro.Name = "cb_miro";
            cb_miro.Size = new Size(110, 23);
            cb_miro.TabIndex = 8;
            // 
            // label43
            // 
            label43.AutoSize = true;
            label43.Location = new Point(6, 24);
            label43.Name = "label43";
            label43.Size = new Size(45, 15);
            label43.TabIndex = 4;
            label43.Text = "Version";
            // 
            // groupBox15
            // 
            groupBox15.Controls.Add(num_reppenrange);
            groupBox15.Controls.Add(label41);
            groupBox15.Controls.Add(num_reppen);
            groupBox15.Controls.Add(label40);
            groupBox15.Location = new Point(3, 283);
            groupBox15.Name = "groupBox15";
            groupBox15.Size = new Size(236, 88);
            groupBox15.TabIndex = 2;
            groupBox15.TabStop = false;
            groupBox15.Text = "Repetition Penalty";
            // 
            // num_reppenrange
            // 
            num_reppenrange.Location = new Point(117, 51);
            num_reppenrange.Maximum = new decimal(new int[] { 128000, 0, 0, 0 });
            num_reppenrange.Name = "num_reppenrange";
            num_reppenrange.Size = new Size(110, 23);
            num_reppenrange.TabIndex = 7;
            // 
            // label41
            // 
            label41.AutoSize = true;
            label41.Location = new Point(6, 53);
            label41.Name = "label41";
            label41.Size = new Size(40, 15);
            label41.TabIndex = 6;
            label41.Text = "Range";
            // 
            // num_reppen
            // 
            num_reppen.DecimalPlaces = 2;
            num_reppen.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_reppen.Location = new Point(117, 22);
            num_reppen.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            num_reppen.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_reppen.Name = "num_reppen";
            num_reppen.Size = new Size(110, 23);
            num_reppen.TabIndex = 5;
            num_reppen.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Location = new Point(6, 24);
            label40.Name = "label40";
            label40.Size = new Size(46, 15);
            label40.TabIndex = 4;
            label40.Text = "Penalty";
            // 
            // groupBox14
            // 
            groupBox14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox14.Controls.Add(num_seed);
            groupBox14.Controls.Add(label39);
            groupBox14.Controls.Add(num_temp);
            groupBox14.Controls.Add(label38);
            groupBox14.Location = new Point(3, 3);
            groupBox14.Name = "groupBox14";
            groupBox14.Size = new Size(962, 62);
            groupBox14.TabIndex = 1;
            groupBox14.TabStop = false;
            groupBox14.Text = "Core Settings";
            // 
            // num_seed
            // 
            num_seed.Location = new Point(296, 22);
            num_seed.Maximum = new decimal(new int[] { 1661992959, 1808227885, 5, 0 });
            num_seed.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            num_seed.Name = "num_seed";
            num_seed.Size = new Size(110, 23);
            num_seed.TabIndex = 15;
            num_seed.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.Location = new Point(258, 24);
            label39.Name = "label39";
            label39.Size = new Size(32, 15);
            label39.TabIndex = 14;
            label39.Text = "Seed";
            // 
            // num_temp
            // 
            num_temp.DecimalPlaces = 2;
            num_temp.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_temp.Location = new Point(117, 22);
            num_temp.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            num_temp.Name = "num_temp";
            num_temp.Size = new Size(110, 23);
            num_temp.TabIndex = 13;
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Location = new Point(6, 24);
            label38.Name = "label38";
            label38.Size = new Size(74, 15);
            label38.TabIndex = 12;
            label38.Text = "Temperature";
            // 
            // groupBox13
            // 
            groupBox13.Controls.Add(num_tfs);
            groupBox13.Controls.Add(label37);
            groupBox13.Controls.Add(num_typical);
            groupBox13.Controls.Add(label36);
            groupBox13.Controls.Add(num_minp);
            groupBox13.Controls.Add(label35);
            groupBox13.Controls.Add(num_topp);
            groupBox13.Controls.Add(label34);
            groupBox13.Controls.Add(num_topa);
            groupBox13.Controls.Add(label33);
            groupBox13.Controls.Add(num_topk);
            groupBox13.Controls.Add(label31);
            groupBox13.Location = new Point(3, 71);
            groupBox13.Name = "groupBox13";
            groupBox13.Size = new Size(236, 206);
            groupBox13.TabIndex = 0;
            groupBox13.TabStop = false;
            groupBox13.Text = "Main Samplers";
            // 
            // num_tfs
            // 
            num_tfs.DecimalPlaces = 2;
            num_tfs.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_tfs.Location = new Point(117, 138);
            num_tfs.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_tfs.Name = "num_tfs";
            num_tfs.Size = new Size(110, 23);
            num_tfs.TabIndex = 11;
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Location = new Point(6, 140);
            label37.Name = "label37";
            label37.Size = new Size(103, 15);
            label37.TabIndex = 10;
            label37.Text = "Tail Free Sampling";
            // 
            // num_typical
            // 
            num_typical.DecimalPlaces = 2;
            num_typical.ForeColor = Color.Gray;
            num_typical.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_typical.Location = new Point(117, 167);
            num_typical.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_typical.Name = "num_typical";
            num_typical.Size = new Size(110, 23);
            num_typical.TabIndex = 9;
            num_typical.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Location = new Point(6, 169);
            label36.Name = "label36";
            label36.Size = new Size(44, 15);
            label36.TabIndex = 8;
            label36.Text = "Typical";
            // 
            // num_minp
            // 
            num_minp.DecimalPlaces = 2;
            num_minp.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_minp.Location = new Point(117, 109);
            num_minp.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_minp.Name = "num_minp";
            num_minp.Size = new Size(110, 23);
            num_minp.TabIndex = 7;
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.Location = new Point(6, 111);
            label35.Name = "label35";
            label35.Size = new Size(38, 15);
            label35.TabIndex = 6;
            label35.Text = "Min P";
            // 
            // num_topp
            // 
            num_topp.DecimalPlaces = 2;
            num_topp.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_topp.Location = new Point(117, 80);
            num_topp.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_topp.Name = "num_topp";
            num_topp.Size = new Size(110, 23);
            num_topp.TabIndex = 5;
            num_topp.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Location = new Point(6, 82);
            label34.Name = "label34";
            label34.Size = new Size(37, 15);
            label34.TabIndex = 4;
            label34.Text = "Top P";
            // 
            // num_topa
            // 
            num_topa.DecimalPlaces = 2;
            num_topa.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_topa.Location = new Point(117, 51);
            num_topa.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_topa.Name = "num_topa";
            num_topa.Size = new Size(110, 23);
            num_topa.TabIndex = 3;
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Location = new Point(6, 53);
            label33.Name = "label33";
            label33.Size = new Size(38, 15);
            label33.TabIndex = 2;
            label33.Text = "Top A";
            // 
            // num_topk
            // 
            num_topk.Location = new Point(117, 22);
            num_topk.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            num_topk.Name = "num_topk";
            num_topk.Size = new Size(110, 23);
            num_topk.TabIndex = 1;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(6, 24);
            label31.Name = "label31";
            label31.Size = new Size(37, 15);
            label31.TabIndex = 0;
            label31.Text = "Top K";
            // 
            // tabSysPrompt
            // 
            tabSysPrompt.Controls.Add(bt_promptsave);
            tabSysPrompt.Controls.Add(label10);
            tabSysPrompt.Controls.Add(cb_promptlist);
            tabSysPrompt.Controls.Add(pan_prompt);
            tabSysPrompt.Location = new Point(4, 27);
            tabSysPrompt.Name = "tabSysPrompt";
            tabSysPrompt.Padding = new Padding(3);
            tabSysPrompt.Size = new Size(996, 887);
            tabSysPrompt.TabIndex = 5;
            tabSysPrompt.Text = "System Prompt";
            tabSysPrompt.UseVisualStyleBackColor = true;
            // 
            // bt_promptsave
            // 
            bt_promptsave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bt_promptsave.Location = new Point(605, 8);
            bt_promptsave.Name = "bt_promptsave";
            bt_promptsave.Size = new Size(75, 23);
            bt_promptsave.TabIndex = 13;
            bt_promptsave.Text = "Save";
            bt_promptsave.UseVisualStyleBackColor = true;
            bt_promptsave.Click += bt_promptsave_Click;
            // 
            // label10
            // 
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label10.Location = new Point(8, 8);
            label10.Name = "label10";
            label10.Size = new Size(104, 23);
            label10.TabIndex = 12;
            label10.Text = "System Prompt";
            label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cb_promptlist
            // 
            cb_promptlist.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_promptlist.FormattingEnabled = true;
            cb_promptlist.Location = new Point(118, 8);
            cb_promptlist.Name = "cb_promptlist";
            cb_promptlist.Size = new Size(481, 23);
            cb_promptlist.TabIndex = 11;
            // 
            // pan_prompt
            // 
            pan_prompt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pan_prompt.AutoScroll = true;
            pan_prompt.Controls.Add(groupBox22);
            pan_prompt.Controls.Add(groupBox21);
            pan_prompt.Location = new Point(8, 38);
            pan_prompt.Name = "pan_prompt";
            pan_prompt.Size = new Size(982, 672);
            pan_prompt.TabIndex = 10;
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
            groupBox22.Location = new Point(610, 3);
            groupBox22.Name = "groupBox22";
            groupBox22.Size = new Size(369, 206);
            groupBox22.TabIndex = 1;
            groupBox22.TabStop = false;
            groupBox22.Text = "Section Titles";
            // 
            // ed_editsys_prefix
            // 
            ed_editsys_prefix.Location = new Point(6, 171);
            ed_editsys_prefix.Name = "ed_editsys_prefix";
            ed_editsys_prefix.Size = new Size(316, 23);
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
            ed_editsys_worldinfo.Size = new Size(316, 23);
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
            ed_editsys_dialogs.Size = new Size(316, 23);
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
            ed_editsys_scenario.Size = new Size(316, 23);
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
            groupBox21.Location = new Point(3, 3);
            groupBox21.Name = "groupBox21";
            groupBox21.Size = new Size(601, 666);
            groupBox21.TabIndex = 0;
            groupBox21.TabStop = false;
            groupBox21.Text = "Main Prompt (intro, char and user bio)";
            // 
            // ed_editsys_prompt
            // 
            ed_editsys_prompt.BorderStyle = BorderStyle.FixedSingle;
            ed_editsys_prompt.Dock = DockStyle.Fill;
            ed_editsys_prompt.Location = new Point(3, 19);
            ed_editsys_prompt.Multiline = true;
            ed_editsys_prompt.Name = "ed_editsys_prompt";
            ed_editsys_prompt.Size = new Size(595, 644);
            ed_editsys_prompt.TabIndex = 0;
            // 
            // tabWorldInfo
            // 
            tabWorldInfo.Controls.Add(panel3);
            tabWorldInfo.Controls.Add(groupBox6);
            tabWorldInfo.Controls.Add(groupBox3);
            tabWorldInfo.Location = new Point(4, 27);
            tabWorldInfo.Name = "tabWorldInfo";
            tabWorldInfo.Size = new Size(996, 887);
            tabWorldInfo.TabIndex = 8;
            tabWorldInfo.Text = "World Info";
            tabWorldInfo.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Controls.Add(groupBox8);
            panel3.Controls.Add(groupBox7);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(241, 57);
            panel3.Name = "panel3";
            panel3.Size = new Size(755, 830);
            panel3.TabIndex = 4;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(label60);
            groupBox8.Controls.Add(numWItriggerchance);
            groupBox8.Controls.Add(label27);
            groupBox8.Controls.Add(num_wentrypriority);
            groupBox8.Controls.Add(label26);
            groupBox8.Controls.Add(num_wentryduration);
            groupBox8.Controls.Add(ck_wentrycasesensitive);
            groupBox8.Controls.Add(label25);
            groupBox8.Controls.Add(num_wentryposition);
            groupBox8.Controls.Add(label24);
            groupBox8.Controls.Add(cb_wentrylocation);
            groupBox8.Controls.Add(label23);
            groupBox8.Controls.Add(cb_wentrykwlink);
            groupBox8.Controls.Add(ed_wentrykw2);
            groupBox8.Controls.Add(label22);
            groupBox8.Controls.Add(ed_wentrykw1);
            groupBox8.Controls.Add(label21);
            groupBox8.Controls.Add(ck_wentryenabled);
            groupBox8.Dock = DockStyle.Fill;
            groupBox8.Location = new Point(0, 246);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(755, 584);
            groupBox8.TabIndex = 1;
            groupBox8.TabStop = false;
            groupBox8.Text = "Entry Settings";
            // 
            // label60
            // 
            label60.AutoSize = true;
            label60.Location = new Point(542, 129);
            label60.Name = "label60";
            label60.Size = new Size(87, 15);
            label60.TabIndex = 17;
            label60.Text = "Trigger Chance";
            // 
            // numWItriggerchance
            // 
            numWItriggerchance.DecimalPlaces = 2;
            numWItriggerchance.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numWItriggerchance.Location = new Point(542, 147);
            numWItriggerchance.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numWItriggerchance.Name = "numWItriggerchance";
            numWItriggerchance.Size = new Size(110, 23);
            numWItriggerchance.TabIndex = 16;
            numWItriggerchance.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numWItriggerchance.ValueChanged += UpdateWorldEntryEvent;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(426, 129);
            label27.Name = "label27";
            label27.Size = new Size(45, 15);
            label27.TabIndex = 15;
            label27.Text = "Priority";
            // 
            // num_wentrypriority
            // 
            num_wentrypriority.Location = new Point(426, 147);
            num_wentrypriority.Name = "num_wentrypriority";
            num_wentrypriority.Size = new Size(110, 23);
            num_wentrypriority.TabIndex = 14;
            num_wentrypriority.ValueChanged += UpdateWorldEntryEvent;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(310, 129);
            label26.Name = "label26";
            label26.Size = new Size(53, 15);
            label26.TabIndex = 13;
            label26.Text = "Duration";
            // 
            // num_wentryduration
            // 
            num_wentryduration.Location = new Point(310, 147);
            num_wentryduration.Name = "num_wentryduration";
            num_wentryduration.Size = new Size(110, 23);
            num_wentryduration.TabIndex = 12;
            num_wentryduration.ValueChanged += UpdateWorldEntryEvent;
            // 
            // ck_wentrycasesensitive
            // 
            ck_wentrycasesensitive.AutoSize = true;
            ck_wentrycasesensitive.Location = new Point(6, 103);
            ck_wentrycasesensitive.Name = "ck_wentrycasesensitive";
            ck_wentrycasesensitive.Size = new Size(100, 19);
            ck_wentrycasesensitive.TabIndex = 11;
            ck_wentrycasesensitive.Text = "Case Sensitive";
            ck_wentrycasesensitive.UseVisualStyleBackColor = true;
            ck_wentrycasesensitive.CheckedChanged += UpdateWorldEntryEvent;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(194, 129);
            label25.Name = "label25";
            label25.Size = new Size(35, 15);
            label25.TabIndex = 10;
            label25.Text = "Index";
            // 
            // num_wentryposition
            // 
            num_wentryposition.Location = new Point(194, 147);
            num_wentryposition.Name = "num_wentryposition";
            num_wentryposition.Size = new Size(110, 23);
            num_wentryposition.TabIndex = 9;
            num_wentryposition.ValueChanged += UpdateWorldEntryEvent;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(6, 129);
            label24.Name = "label24";
            label24.Size = new Size(67, 15);
            label24.TabIndex = 8;
            label24.Text = "Positioning";
            // 
            // cb_wentrylocation
            // 
            cb_wentrylocation.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_wentrylocation.FormattingEnabled = true;
            cb_wentrylocation.Items.AddRange(new object[] { "System Prompt", "Chat" });
            cb_wentrylocation.Location = new Point(6, 147);
            cb_wentrylocation.Name = "cb_wentrylocation";
            cb_wentrylocation.Size = new Size(182, 23);
            cb_wentrylocation.TabIndex = 7;
            cb_wentrylocation.SelectedIndexChanged += UpdateWorldEntryEvent;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(311, 56);
            label23.Name = "label23";
            label23.Size = new Size(78, 15);
            label23.TabIndex = 6;
            label23.Text = "Keyword Link";
            // 
            // cb_wentrykwlink
            // 
            cb_wentrykwlink.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_wentrykwlink.FormattingEnabled = true;
            cb_wentrykwlink.Items.AddRange(new object[] { "And", "Or", "Not" });
            cb_wentrykwlink.Location = new Point(311, 74);
            cb_wentrykwlink.Name = "cb_wentrykwlink";
            cb_wentrykwlink.Size = new Size(130, 23);
            cb_wentrykwlink.TabIndex = 5;
            cb_wentrykwlink.SelectedIndexChanged += UpdateWorldEntryEvent;
            // 
            // ed_wentrykw2
            // 
            ed_wentrykw2.Location = new Point(447, 74);
            ed_wentrykw2.Name = "ed_wentrykw2";
            ed_wentrykw2.Size = new Size(253, 23);
            ed_wentrykw2.TabIndex = 4;
            ed_wentrykw2.TextChanged += UpdateWorldEntryEvent;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(447, 56);
            label22.Name = "label22";
            label22.Size = new Size(67, 15);
            label22.TabIndex = 3;
            label22.Text = "Keywords 2";
            // 
            // ed_wentrykw1
            // 
            ed_wentrykw1.Location = new Point(6, 74);
            ed_wentrykw1.Name = "ed_wentrykw1";
            ed_wentrykw1.Size = new Size(299, 23);
            ed_wentrykw1.TabIndex = 2;
            ed_wentrykw1.TextChanged += UpdateWorldEntryEvent;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(6, 56);
            label21.Name = "label21";
            label21.Size = new Size(67, 15);
            label21.TabIndex = 1;
            label21.Text = "Keywords 1";
            // 
            // ck_wentryenabled
            // 
            ck_wentryenabled.AutoSize = true;
            ck_wentryenabled.Location = new Point(6, 22);
            ck_wentryenabled.Name = "ck_wentryenabled";
            ck_wentryenabled.Size = new Size(68, 19);
            ck_wentryenabled.TabIndex = 0;
            ck_wentryenabled.Text = "Enabled";
            ck_wentryenabled.UseVisualStyleBackColor = true;
            ck_wentryenabled.CheckedChanged += UpdateWorldEntryEvent;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(label20);
            groupBox7.Controls.Add(ed_wentrymem);
            groupBox7.Controls.Add(ed_wentryname);
            groupBox7.Controls.Add(label19);
            groupBox7.Dock = DockStyle.Top;
            groupBox7.Location = new Point(0, 0);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(755, 246);
            groupBox7.TabIndex = 0;
            groupBox7.TabStop = false;
            groupBox7.Text = "Entry Info";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(6, 63);
            label20.Name = "label20";
            label20.Size = new Size(52, 15);
            label20.TabIndex = 3;
            label20.Text = "Memory";
            // 
            // ed_wentrymem
            // 
            ed_wentrymem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ed_wentrymem.Location = new Point(6, 81);
            ed_wentrymem.Multiline = true;
            ed_wentrymem.Name = "ed_wentrymem";
            ed_wentrymem.ScrollBars = ScrollBars.Vertical;
            ed_wentrymem.Size = new Size(741, 159);
            ed_wentrymem.TabIndex = 2;
            ed_wentrymem.TextChanged += UpdateWorldEntryEvent;
            // 
            // ed_wentryname
            // 
            ed_wentryname.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ed_wentryname.Location = new Point(6, 37);
            ed_wentryname.Name = "ed_wentryname";
            ed_wentryname.Size = new Size(741, 23);
            ed_wentryname.TabIndex = 1;
            ed_wentryname.TextChanged += UpdateWorldEntryEvent;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(6, 19);
            label19.Name = "label19";
            label19.Size = new Size(39, 15);
            label19.TabIndex = 0;
            label19.Text = "Name";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(ck_wiembed);
            groupBox6.Controls.Add(bt_delwentry);
            groupBox6.Controls.Add(bt_addwentry);
            groupBox6.Controls.Add(label18);
            groupBox6.Controls.Add(lb_worldentries);
            groupBox6.Controls.Add(label17);
            groupBox6.Controls.Add(label16);
            groupBox6.Controls.Add(num_scandepth);
            groupBox6.Controls.Add(ed_worlddesc);
            groupBox6.Dock = DockStyle.Left;
            groupBox6.Location = new Point(0, 57);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(241, 830);
            groupBox6.TabIndex = 3;
            groupBox6.TabStop = false;
            groupBox6.Text = "World Settings";
            // 
            // ck_wiembed
            // 
            ck_wiembed.AutoSize = true;
            ck_wiembed.Location = new Point(8, 163);
            ck_wiembed.Name = "ck_wiembed";
            ck_wiembed.Size = new Size(150, 19);
            ck_wiembed.TabIndex = 8;
            ck_wiembed.Text = "Use Vector Embeddings";
            ck_wiembed.UseVisualStyleBackColor = true;
            ck_wiembed.CheckedChanged += ck_wiembed_CheckedChanged;
            // 
            // bt_delwentry
            // 
            bt_delwentry.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_delwentry.Location = new Point(145, 802);
            bt_delwentry.Name = "bt_delwentry";
            bt_delwentry.Size = new Size(90, 23);
            bt_delwentry.TabIndex = 7;
            bt_delwentry.Text = "Delete";
            bt_delwentry.UseVisualStyleBackColor = true;
            bt_delwentry.Click += bt_delwentry_Click;
            // 
            // bt_addwentry
            // 
            bt_addwentry.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            bt_addwentry.Location = new Point(3, 802);
            bt_addwentry.Name = "bt_addwentry";
            bt_addwentry.Size = new Size(90, 23);
            bt_addwentry.TabIndex = 6;
            bt_addwentry.Text = "Add New";
            bt_addwentry.UseVisualStyleBackColor = true;
            bt_addwentry.Click += bt_addwentry_Click;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label18.Location = new Point(3, 197);
            label18.Name = "label18";
            label18.Size = new Size(71, 15);
            label18.TabIndex = 5;
            label18.Text = "Scan Depth";
            // 
            // lb_worldentries
            // 
            lb_worldentries.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lb_worldentries.FormattingEnabled = true;
            lb_worldentries.Location = new Point(3, 215);
            lb_worldentries.Name = "lb_worldentries";
            lb_worldentries.Size = new Size(232, 574);
            lb_worldentries.TabIndex = 4;
            lb_worldentries.SelectedIndexChanged += lb_worldentries_SelectedIndexChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label17.Location = new Point(3, 116);
            label17.Name = "label17";
            label17.Size = new Size(71, 15);
            label17.TabIndex = 3;
            label17.Text = "Scan Depth";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label16.Location = new Point(3, 19);
            label16.Name = "label16";
            label16.Size = new Size(71, 15);
            label16.TabIndex = 2;
            label16.Text = "Description";
            // 
            // num_scandepth
            // 
            num_scandepth.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            num_scandepth.Location = new Point(3, 134);
            num_scandepth.Name = "num_scandepth";
            num_scandepth.Size = new Size(232, 23);
            num_scandepth.TabIndex = 1;
            num_scandepth.ValueChanged += num_scandepth_ValueChanged;
            // 
            // ed_worlddesc
            // 
            ed_worlddesc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ed_worlddesc.Location = new Point(3, 37);
            ed_worlddesc.Multiline = true;
            ed_worlddesc.Name = "ed_worlddesc";
            ed_worlddesc.ScrollBars = ScrollBars.Vertical;
            ed_worlddesc.Size = new Size(232, 76);
            ed_worlddesc.TabIndex = 0;
            ed_worlddesc.KeyPress += ed_worlddesc_KeyPress;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(bt_worldsave);
            groupBox3.Controls.Add(cb_worlds);
            groupBox3.Dock = DockStyle.Top;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(996, 57);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "File Selection";
            // 
            // bt_worldsave
            // 
            bt_worldsave.Location = new Point(551, 22);
            bt_worldsave.Name = "bt_worldsave";
            bt_worldsave.Size = new Size(110, 23);
            bt_worldsave.TabIndex = 1;
            bt_worldsave.Text = "Save";
            bt_worldsave.UseVisualStyleBackColor = true;
            bt_worldsave.Click += bt_worldsave_Click;
            // 
            // cb_worlds
            // 
            cb_worlds.FormattingEnabled = true;
            cb_worlds.Location = new Point(8, 22);
            cb_worlds.Name = "cb_worlds";
            cb_worlds.Size = new Size(537, 23);
            cb_worlds.TabIndex = 0;
            // 
            // tabHistory
            // 
            tabHistory.Controls.Add(panel6);
            tabHistory.Controls.Add(panel4);
            tabHistory.Location = new Point(4, 27);
            tabHistory.Name = "tabHistory";
            tabHistory.Size = new Size(996, 887);
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
            panel6.Size = new Size(620, 887);
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
            web_sessioncontent.Size = new Size(620, 579);
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
            panel7.Size = new Size(620, 308);
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
            groupBox25.Size = new Size(606, 101);
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
            groupBox12.Size = new Size(606, 191);
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
            ed_sessioninfo.Size = new Size(594, 104);
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
            ed_sessiontitle.Size = new Size(594, 23);
            ed_sessiontitle.TabIndex = 0;
            ed_sessiontitle.TextChanged += ed_sessiontitle_TextChanged;
            // 
            // bt_sessionrefresh
            // 
            bt_sessionrefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bt_sessionrefresh.Location = new Point(449, 11);
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
            panel4.Size = new Size(376, 887);
            panel4.TabIndex = 0;
            // 
            // listSession
            // 
            listSession.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listSession.Dock = DockStyle.Fill;
            listSession.FullRowSelect = true;
            listSession.Location = new Point(0, 0);
            listSession.Name = "listSession";
            listSession.Size = new Size(376, 821);
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
            panel5.Location = new Point(0, 821);
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
            tabChat.Size = new Size(996, 887);
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
            bt_impersonate.Location = new Point(928, 798);
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
            web_chat.Size = new Size(778, 788);
            web_chat.TabIndex = 6;
            web_chat.ZoomFactor = 1D;
            // 
            // bt_delete
            // 
            bt_delete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_delete.BackColor = Color.LightCoral;
            bt_delete.FlatStyle = FlatStyle.Flat;
            bt_delete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_delete.Location = new Point(928, 859);
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
            bt_reroll.Location = new Point(928, 829);
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
            bt_send.Location = new Point(864, 798);
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
            ed_input.Location = new Point(212, 796);
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
            panel1.Size = new Size(203, 881);
            panel1.TabIndex = 0;
            // 
            // groupBox23
            // 
            groupBox23.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox23.Controls.Add(ck_agentmode);
            groupBox23.Controls.Add(ck_ragtothink);
            groupBox23.Controls.Add(ck_disablethink);
            groupBox23.Controls.Add(bt_clearimg);
            groupBox23.Controls.Add(pictEmbed);
            groupBox23.Controls.Add(ck_ttstoggle);
            groupBox23.Controls.Add(bt_test);
            groupBox23.Controls.Add(ck_onlinerag);
            groupBox23.Controls.Add(ck_caninitchat);
            groupBox23.Controls.Add(ck_worldinfo);
            groupBox23.Controls.Add(ck_ragenabled);
            groupBox23.Controls.Add(ck_sessionmemory);
            groupBox23.Controls.Add(ck_senseoftime);
            groupBox23.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox23.Location = new Point(6, 541);
            groupBox23.Name = "groupBox23";
            groupBox23.Size = new Size(187, 337);
            groupBox23.TabIndex = 26;
            groupBox23.TabStop = false;
            groupBox23.Text = "Quick Settings";
            // 
            // ck_agentmode
            // 
            ck_agentmode.AutoSize = true;
            ck_agentmode.Font = new Font("Segoe UI", 9F);
            ck_agentmode.Location = new Point(6, 245);
            ck_agentmode.Name = "ck_agentmode";
            ck_agentmode.Size = new Size(159, 19);
            ck_agentmode.TabIndex = 37;
            ck_agentmode.Text = "Background Agent Mode";
            ck_agentmode.UseVisualStyleBackColor = true;
            ck_agentmode.CheckedChanged += ck_agentmode_CheckedChanged;
            // 
            // ck_ragtothink
            // 
            ck_ragtothink.AutoSize = true;
            ck_ragtothink.Font = new Font("Segoe UI", 9F);
            ck_ragtothink.Location = new Point(6, 220);
            ck_ragtothink.Name = "ck_ragtothink";
            ck_ragtothink.Size = new Size(161, 19);
            ck_ragtothink.TabIndex = 36;
            ck_ragtothink.Text = "Put context in think block";
            ck_ragtothink.UseVisualStyleBackColor = true;
            ck_ragtothink.CheckedChanged += ck_ragtothink_CheckedChanged;
            // 
            // ck_disablethink
            // 
            ck_disablethink.AutoSize = true;
            ck_disablethink.Font = new Font("Segoe UI", 9F);
            ck_disablethink.Location = new Point(6, 195);
            ck_disablethink.Name = "ck_disablethink";
            ck_disablethink.Size = new Size(92, 19);
            ck_disablethink.TabIndex = 35;
            ck_disablethink.Text = "No Thinking";
            ck_disablethink.UseVisualStyleBackColor = true;
            ck_disablethink.CheckedChanged += ck_disablethink_CheckedChanged;
            // 
            // bt_clearimg
            // 
            bt_clearimg.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            bt_clearimg.Location = new Point(112, 308);
            bt_clearimg.Name = "bt_clearimg";
            bt_clearimg.Size = new Size(75, 23);
            bt_clearimg.TabIndex = 34;
            bt_clearimg.Text = "Clear";
            bt_clearimg.UseVisualStyleBackColor = true;
            bt_clearimg.Click += bt_clearimg_Click;
            // 
            // pictEmbed
            // 
            pictEmbed.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pictEmbed.BorderStyle = BorderStyle.FixedSingle;
            pictEmbed.Location = new Point(0, 281);
            pictEmbed.Name = "pictEmbed";
            pictEmbed.Size = new Size(65, 50);
            pictEmbed.SizeMode = PictureBoxSizeMode.StretchImage;
            pictEmbed.TabIndex = 33;
            pictEmbed.TabStop = false;
            // 
            // ck_ttstoggle
            // 
            ck_ttstoggle.AutoSize = true;
            ck_ttstoggle.Font = new Font("Segoe UI", 9F);
            ck_ttstoggle.Location = new Point(6, 170);
            ck_ttstoggle.Name = "ck_ttstoggle";
            ck_ttstoggle.Size = new Size(84, 19);
            ck_ttstoggle.TabIndex = 32;
            ck_ttstoggle.Text = "Enable TTS";
            ck_ttstoggle.UseVisualStyleBackColor = true;
            ck_ttstoggle.CheckedChanged += ck_ttstoggle_CheckedChanged;
            // 
            // bt_test
            // 
            bt_test.Font = new Font("Segoe UI", 9F);
            bt_test.ForeColor = SystemColors.MenuHighlight;
            bt_test.Location = new Point(106, 18);
            bt_test.Name = "bt_test";
            bt_test.Size = new Size(75, 23);
            bt_test.TabIndex = 30;
            bt_test.Text = "Test Stuff";
            bt_test.UseVisualStyleBackColor = true;
            bt_test.Click += button2_Click;
            // 
            // ck_onlinerag
            // 
            ck_onlinerag.AutoSize = true;
            ck_onlinerag.Font = new Font("Segoe UI", 9F);
            ck_onlinerag.Location = new Point(6, 47);
            ck_onlinerag.Name = "ck_onlinerag";
            ck_onlinerag.Size = new Size(87, 19);
            ck_onlinerag.TabIndex = 29;
            ck_onlinerag.Text = "Online RAG";
            ck_onlinerag.UseVisualStyleBackColor = true;
            ck_onlinerag.CheckedChanged += ck_onlinerag_CheckedChanged;
            // 
            // ck_caninitchat
            // 
            ck_caninitchat.AutoSize = true;
            ck_caninitchat.Font = new Font("Segoe UI", 9F);
            ck_caninitchat.Location = new Point(6, 145);
            ck_caninitchat.Name = "ck_caninitchat";
            ck_caninitchat.Size = new Size(131, 19);
            ck_caninitchat.TabIndex = 28;
            ck_caninitchat.Text = "Bot can initiate chat";
            ck_caninitchat.UseVisualStyleBackColor = true;
            ck_caninitchat.CheckedChanged += ck_caninit_CheckedChanged;
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
            // ck_senseoftime
            // 
            ck_senseoftime.AutoSize = true;
            ck_senseoftime.Font = new Font("Segoe UI", 9F);
            ck_senseoftime.Location = new Point(6, 120);
            ck_senseoftime.Name = "ck_senseoftime";
            ck_senseoftime.Size = new Size(100, 19);
            ck_senseoftime.TabIndex = 23;
            ck_senseoftime.Text = "Sense of Time";
            ck_senseoftime.UseVisualStyleBackColor = true;
            ck_senseoftime.CheckedChanged += ck_senseoftime_CheckedChanged;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Controls.Add(bt_editchar);
            groupBox5.Controls.Add(bt_scenario);
            groupBox5.Controls.Add(label3);
            groupBox5.Controls.Add(cb_bot);
            groupBox5.Controls.Add(label4);
            groupBox5.Controls.Add(cb_user);
            groupBox5.Controls.Add(bt_newsession);
            groupBox5.Controls.Add(label11);
            groupBox5.Controls.Add(cb_sysprompt);
            groupBox5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox5.Location = new Point(6, 322);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(187, 213);
            groupBox5.TabIndex = 25;
            groupBox5.TabStop = false;
            groupBox5.Text = "Chat Settings";
            // 
            // bt_editchar
            // 
            bt_editchar.Font = new Font("Segoe UI", 9F);
            bt_editchar.Location = new Point(146, 11);
            bt_editchar.Name = "bt_editchar";
            bt_editchar.Size = new Size(35, 20);
            bt_editchar.TabIndex = 27;
            bt_editchar.Text = "...";
            bt_editchar.UseVisualStyleBackColor = true;
            bt_editchar.Click += bt_editchar_Click;
            // 
            // bt_scenario
            // 
            bt_scenario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            bt_scenario.Font = new Font("Segoe UI", 9F);
            bt_scenario.Location = new Point(6, 154);
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
            bt_newsession.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            bt_newsession.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_newsession.Location = new Point(6, 184);
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
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.Controls.Add(btInstructEdit);
            groupBox4.Controls.Add(ck_charsampler);
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
            groupBox4.Size = new Size(187, 211);
            groupBox4.TabIndex = 24;
            groupBox4.TabStop = false;
            groupBox4.Text = "Inference Settings";
            // 
            // btInstructEdit
            // 
            btInstructEdit.Font = new Font("Segoe UI", 9F);
            btInstructEdit.Location = new Point(146, 11);
            btInstructEdit.Name = "btInstructEdit";
            btInstructEdit.Size = new Size(35, 20);
            btInstructEdit.TabIndex = 28;
            btInstructEdit.Text = "...";
            btInstructEdit.UseVisualStyleBackColor = true;
            btInstructEdit.Click += btInstructEdit_Click;
            // 
            // ck_charsampler
            // 
            ck_charsampler.AutoSize = true;
            ck_charsampler.Font = new Font("Segoe UI", 9F);
            ck_charsampler.Location = new Point(4, 135);
            ck_charsampler.Name = "ck_charsampler";
            ck_charsampler.Size = new Size(155, 19);
            ck_charsampler.TabIndex = 26;
            ck_charsampler.Text = "Use character's samplers";
            ck_charsampler.UseVisualStyleBackColor = true;
            // 
            // ck_forceNames
            // 
            ck_forceNames.AutoSize = true;
            ck_forceNames.Font = new Font("Segoe UI", 9F);
            ck_forceNames.Location = new Point(6, 66);
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
            label6.Location = new Point(4, 88);
            label6.Name = "label6";
            label6.Size = new Size(102, 15);
            label6.TabIndex = 6;
            label6.Text = "Sampling Settings";
            // 
            // cb_infer
            // 
            cb_infer.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_infer.Font = new Font("Segoe UI", 9F);
            cb_infer.Location = new Point(4, 106);
            cb_infer.Name = "cb_infer";
            cb_infer.Size = new Size(177, 23);
            cb_infer.TabIndex = 7;
            cb_infer.SelectedIndexChanged += cb_infer_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F);
            label9.Location = new Point(4, 157);
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
            num_temperature.Location = new Point(4, 175);
            num_temperature.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            num_temperature.Name = "num_temperature";
            num_temperature.Size = new Size(177, 23);
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
            tabMain.Controls.Add(tabWorldInfo);
            tabMain.Controls.Add(tabSysPrompt);
            tabMain.Controls.Add(tabSamplers);
            tabMain.Controls.Add(tabSettings);
            tabMain.Controls.Add(tabConsole);
            tabMain.Controls.Add(tabSearch);
            tabMain.Location = new Point(0, 0);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(1004, 918);
            tabMain.TabIndex = 1;
            // 
            // tabSearch
            // 
            tabSearch.Controls.Add(txtSearchRes);
            tabSearch.Controls.Add(textBox1);
            tabSearch.Location = new Point(4, 27);
            tabSearch.Name = "tabSearch";
            tabSearch.Padding = new Padding(3);
            tabSearch.Size = new Size(996, 887);
            tabSearch.TabIndex = 10;
            tabSearch.Text = "tabPage1";
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
            ClientSize = new Size(1004, 943);
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
            tabSettings.ResumeLayout(false);
            groupBox24.ResumeLayout(false);
            groupBox24.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_removeitalicmaxword).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_italicratio).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_antislopchance).EndInit();
            groupBox11.ResumeLayout(false);
            groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_memtokens).EndInit();
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_msgcount).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_fontsize).EndInit();
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_ragindex).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_ragmaxretrieve).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_ragcutoff).EndInit();
            tabSamplers.ResumeLayout(false);
            pan_samplers.ResumeLayout(false);
            groupBox20.ResumeLayout(false);
            groupBox20.PerformLayout();
            groupBox19.ResumeLayout(false);
            groupBox19.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_xtcthres).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_xtcprob).EndInit();
            groupBox18.ResumeLayout(false);
            groupBox18.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_drymul).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_drybase).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_dryrange).EndInit();
            groupBox17.ResumeLayout(false);
            groupBox17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_smoothfac).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_dynexpo).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_dynrange).EndInit();
            groupBox16.ResumeLayout(false);
            groupBox16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_meta).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_mtau).EndInit();
            groupBox15.ResumeLayout(false);
            groupBox15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_reppenrange).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_reppen).EndInit();
            groupBox14.ResumeLayout(false);
            groupBox14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_seed).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_temp).EndInit();
            groupBox13.ResumeLayout(false);
            groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_tfs).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_typical).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_minp).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_topp).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_topa).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_topk).EndInit();
            tabSysPrompt.ResumeLayout(false);
            pan_prompt.ResumeLayout(false);
            groupBox22.ResumeLayout(false);
            groupBox22.PerformLayout();
            groupBox21.ResumeLayout(false);
            groupBox21.PerformLayout();
            tabWorldInfo.ResumeLayout(false);
            panel3.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numWItriggerchance).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_wentrypriority).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_wentryduration).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_wentryposition).EndInit();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_scandepth).EndInit();
            groupBox3.ResumeLayout(false);
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
        private TabPage tabSettings;
        private GroupBox groupBox24;
        private NumericUpDown num_removeitalicmaxword;
        private Label label63;
        private CheckBox ck_oneparagraph;
        private CheckBox ck_remlastsentence;
        private Label label62;
        private NumericUpDown num_italicratio;
        private CheckBox ck_reduceitalic;
        private CheckBox ck_noemphasisword;
        private CheckBox ck_fixquotes;
        private CheckBox ck_noquotes;
        private CheckBox ck_unbold;
        private Label label61;
        private NumericUpDown num_antislopchance;
        private TextBox ed_sloplist;
        private CheckBox ck_antislop;
        private CheckBox ck_fixasterix;
        private GroupBox groupBox11;
        private NumericUpDown num_memtokens;
        private Label label32;
        private GroupBox groupBox10;
        private NumericUpDown num_msgcount;
        private Label label30;
        private NumericUpDown num_fontsize;
        private Label label29;
        private ComboBox cb_background;
        private Label label28;
        private GroupBox groupBox9;
        private CheckBox ck_webgrammar;
        private CheckBox ck_webkeyword;
        private GroupBox groupBox2;
        private Button bt_chattosessions;
        private Button bt_importworld;
        private Button bt_ImportSTChat;
        private GroupBox groupBox1;
        private CheckBox ck_alwayswebsearch;
        private CheckBox ck_ragdocs;
        private Label label15;
        private NumericUpDown num_ragindex;
        private Label label14;
        private NumericUpDown num_ragmaxretrieve;
        private Label label13;
        private NumericUpDown num_ragcutoff;
        private Label label12;
        private ComboBox cb_ragheuristic;
        private Button button1;
        private TabPage tabSamplers;
        private Button bt_savesampler;
        private Label label1;
        private ComboBox cb_samplerlist;
        private Panel pan_samplers;
        private GroupBox groupBox20;
        private CheckBox ck_trimstop;
        private CheckBox ck_renderspecial;
        private CheckBox ck_ignoreeos;
        private GroupBox groupBox19;
        private NumericUpDown num_xtcthres;
        private Label label50;
        private NumericUpDown num_xtcprob;
        private Label label51;
        private GroupBox groupBox18;
        private NumericUpDown num_drymul;
        private Label label46;
        private NumericUpDown num_drybase;
        private Label label48;
        private NumericUpDown num_dryrange;
        private Label label49;
        private GroupBox groupBox17;
        private NumericUpDown num_smoothfac;
        private Label label59;
        private NumericUpDown num_dynexpo;
        private Label label45;
        private NumericUpDown num_dynrange;
        private Label label47;
        private GroupBox groupBox16;
        private NumericUpDown num_meta;
        private Label label42;
        private NumericUpDown num_mtau;
        private Label label44;
        private ComboBox cb_miro;
        private Label label43;
        private GroupBox groupBox15;
        private NumericUpDown num_reppenrange;
        private Label label41;
        private NumericUpDown num_reppen;
        private Label label40;
        private GroupBox groupBox14;
        private NumericUpDown num_seed;
        private Label label39;
        private NumericUpDown num_temp;
        private Label label38;
        private GroupBox groupBox13;
        private NumericUpDown num_tfs;
        private Label label37;
        private NumericUpDown num_typical;
        private Label label36;
        private NumericUpDown num_minp;
        private Label label35;
        private NumericUpDown num_topp;
        private Label label34;
        private NumericUpDown num_topa;
        private Label label33;
        private NumericUpDown num_topk;
        private Label label31;
        private TabPage tabSysPrompt;
        private Button bt_promptsave;
        private Label label10;
        private ComboBox cb_promptlist;
        private Panel pan_prompt;
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
        private TabPage tabWorldInfo;
        private Panel panel3;
        private GroupBox groupBox8;
        private Label label60;
        private NumericUpDown numWItriggerchance;
        private Label label27;
        private NumericUpDown num_wentrypriority;
        private Label label26;
        private NumericUpDown num_wentryduration;
        private CheckBox ck_wentrycasesensitive;
        private Label label25;
        private NumericUpDown num_wentryposition;
        private Label label24;
        private ComboBox cb_wentrylocation;
        private Label label23;
        private ComboBox cb_wentrykwlink;
        private TextBox ed_wentrykw2;
        private Label label22;
        private TextBox ed_wentrykw1;
        private Label label21;
        private CheckBox ck_wentryenabled;
        private GroupBox groupBox7;
        private Label label20;
        private TextBox ed_wentrymem;
        private TextBox ed_wentryname;
        private Label label19;
        private GroupBox groupBox6;
        private CheckBox ck_wiembed;
        private Button bt_delwentry;
        private Button bt_addwentry;
        private Label label18;
        private ListBox lb_worldentries;
        private Label label17;
        private Label label16;
        private NumericUpDown num_scandepth;
        private TextBox ed_worlddesc;
        private GroupBox groupBox3;
        private Button bt_worldsave;
        private ComboBox cb_worlds;
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
        private Button bt_test;
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
        private Label label65;
        private ComboBox cb_pastsession;
        private ListView listSession;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Button btEmbedAll;
        private CheckBox ck_disablethink;
        private CheckBox ck_ragtothink;
        private CheckBox ck_hist_isrp;
        private CheckBox ck_sysrag;
        private CheckBox ck_agentmode;
        private TabPage tabSearch;
        private TextBox txtSearchRes;
        private TextBox textBox1;
        private Button btInstructEdit;
    }
}
