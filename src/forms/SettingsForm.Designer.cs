namespace WaifuAI.src.forms
{
    partial class SettingsForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            openFileDialog1 = new OpenFileDialog();
            groupBox24 = new GroupBox();
            ck_lastparaphfilter = new CheckBox();
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
            ck_sessionmemory = new CheckBox();
            groupBox10 = new GroupBox();
            ck_forcePW = new CheckBox();
            ckShowHidden = new CheckBox();
            num_msgcount = new NumericUpDown();
            label30 = new Label();
            num_fontsize = new NumericUpDown();
            label29 = new Label();
            cb_background = new ComboBox();
            label28 = new Label();
            groupBox9 = new GroupBox();
            label4 = new Label();
            ck_searchextract = new CheckBox();
            label3 = new Label();
            ed_searchkey = new TextBox();
            label2 = new Label();
            cb_searchapi = new ComboBox();
            ck_webgrammar = new CheckBox();
            ck_alwayswebsearch = new CheckBox();
            ck_webkeyword = new CheckBox();
            groupBox2 = new GroupBox();
            bt_chattosessions = new Button();
            bt_importworld = new Button();
            bt_ImportSTChat = new Button();
            groupBox1 = new GroupBox();
            ckThirdPerson = new CheckBox();
            num_ragM = new NumericUpDown();
            label1 = new Label();
            label15 = new Label();
            num_ragindex = new NumericUpDown();
            label14 = new Label();
            num_ragmaxretrieve = new NumericUpDown();
            label13 = new Label();
            num_ragcutoff = new NumericUpDown();
            label12 = new Label();
            cb_ragheuristic = new ComboBox();
            panel1 = new Panel();
            bt_Close = new Button();
            HelptoolTip = new ToolTip(components);
            groupBox3 = new GroupBox();
            ck_hallusafe = new CheckBox();
            ck_sysrag = new CheckBox();
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
            ((System.ComponentModel.ISupportInitialize)num_ragM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_ragindex).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_ragmaxretrieve).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_ragcutoff).BeginInit();
            panel1.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox24
            // 
            groupBox24.Controls.Add(ck_lastparaphfilter);
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
            groupBox24.Location = new Point(676, 12);
            groupBox24.Name = "groupBox24";
            groupBox24.Size = new Size(355, 405);
            groupBox24.TabIndex = 29;
            groupBox24.TabStop = false;
            groupBox24.Text = "Output Formatting";
            // 
            // ck_lastparaphfilter
            // 
            ck_lastparaphfilter.AutoSize = true;
            ck_lastparaphfilter.Font = new Font("Segoe UI", 9F);
            ck_lastparaphfilter.Location = new Point(6, 338);
            ck_lastparaphfilter.Name = "ck_lastparaphfilter";
            ck_lastparaphfilter.Size = new Size(206, 19);
            ck_lastparaphfilter.TabIndex = 41;
            ck_lastparaphfilter.Text = "Delete meaningless last paragraph";
            ck_lastparaphfilter.UseVisualStyleBackColor = true;
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
            // 
            // ed_sloplist
            // 
            ed_sloplist.Location = new Point(27, 72);
            ed_sloplist.Name = "ed_sloplist";
            ed_sloplist.PlaceholderText = "comma separated list of words to filter out";
            ed_sloplist.Size = new Size(313, 23);
            ed_sloplist.TabIndex = 2;
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
            // 
            // groupBox11
            // 
            groupBox11.Controls.Add(label65);
            groupBox11.Controls.Add(cb_pastsession);
            groupBox11.Controls.Add(num_memtokens);
            groupBox11.Controls.Add(label32);
            groupBox11.Controls.Add(ck_sessionmemory);
            groupBox11.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox11.Location = new Point(12, 118);
            groupBox11.Name = "groupBox11";
            groupBox11.Size = new Size(326, 122);
            groupBox11.TabIndex = 27;
            groupBox11.TabStop = false;
            groupBox11.Text = "Session Memory System";
            // 
            // label65
            // 
            label65.AutoSize = true;
            label65.Font = new Font("Segoe UI", 9F);
            label65.Location = new Point(6, 73);
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
            cb_pastsession.Location = new Point(6, 91);
            cb_pastsession.Name = "cb_pastsession";
            cb_pastsession.Size = new Size(298, 23);
            cb_pastsession.TabIndex = 33;
            // 
            // num_memtokens
            // 
            num_memtokens.Font = new Font("Segoe UI", 9F);
            num_memtokens.Increment = new decimal(new int[] { 512, 0, 0, 0 });
            num_memtokens.Location = new Point(6, 47);
            num_memtokens.Maximum = new decimal(new int[] { 128000, 0, 0, 0 });
            num_memtokens.Minimum = new decimal(new int[] { 512, 0, 0, 0 });
            num_memtokens.Name = "num_memtokens";
            num_memtokens.Size = new Size(125, 23);
            num_memtokens.TabIndex = 27;
            num_memtokens.Value = new decimal(new int[] { 2048, 0, 0, 0 });
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Font = new Font("Segoe UI", 9F);
            label32.Location = new Point(137, 49);
            label32.Name = "label32";
            label32.Size = new Size(94, 15);
            label32.TabIndex = 28;
            label32.Text = "Reserved Tokens";
            // 
            // ck_sessionmemory
            // 
            ck_sessionmemory.AutoSize = true;
            ck_sessionmemory.Font = new Font("Segoe UI", 9F);
            ck_sessionmemory.Location = new Point(6, 22);
            ck_sessionmemory.Name = "ck_sessionmemory";
            ck_sessionmemory.Size = new Size(194, 19);
            ck_sessionmemory.TabIndex = 24;
            ck_sessionmemory.Text = "Add summaries of past sessions";
            ck_sessionmemory.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(ck_forcePW);
            groupBox10.Controls.Add(ckShowHidden);
            groupBox10.Controls.Add(num_msgcount);
            groupBox10.Controls.Add(label30);
            groupBox10.Controls.Add(num_fontsize);
            groupBox10.Controls.Add(label29);
            groupBox10.Controls.Add(cb_background);
            groupBox10.Controls.Add(label28);
            groupBox10.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox10.Location = new Point(344, 244);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(326, 173);
            groupBox10.TabIndex = 26;
            groupBox10.TabStop = false;
            groupBox10.Text = "User Interface";
            // 
            // ck_forcePW
            // 
            ck_forcePW.AutoSize = true;
            ck_forcePW.Font = new Font("Segoe UI", 9F);
            ck_forcePW.Location = new Point(6, 141);
            ck_forcePW.Name = "ck_forcePW";
            ck_forcePW.Size = new Size(215, 19);
            ck_forcePW.TabIndex = 38;
            ck_forcePW.Text = "Force password when switching bot";
            ck_forcePW.UseVisualStyleBackColor = true;
            // 
            // ckShowHidden
            // 
            ckShowHidden.AutoSize = true;
            ckShowHidden.Font = new Font("Segoe UI", 9F);
            ckShowHidden.Location = new Point(5, 116);
            ckShowHidden.Name = "ckShowHidden";
            ckShowHidden.Size = new Size(189, 19);
            ckShowHidden.TabIndex = 31;
            ckShowHidden.Text = "Show hidden system messages";
            ckShowHidden.UseVisualStyleBackColor = true;
            // 
            // num_msgcount
            // 
            num_msgcount.Font = new Font("Segoe UI", 9F);
            num_msgcount.Increment = new decimal(new int[] { 20, 0, 0, 0 });
            num_msgcount.Location = new Point(122, 81);
            num_msgcount.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            num_msgcount.Minimum = new decimal(new int[] { 20, 0, 0, 0 });
            num_msgcount.Name = "num_msgcount";
            num_msgcount.Size = new Size(79, 23);
            num_msgcount.TabIndex = 29;
            num_msgcount.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new Font("Segoe UI", 9F);
            label30.Location = new Point(104, 63);
            label30.Name = "label30";
            label30.Size = new Size(97, 15);
            label30.TabIndex = 30;
            label30.Text = "Shown Messages";
            // 
            // num_fontsize
            // 
            num_fontsize.Font = new Font("Segoe UI", 9F);
            num_fontsize.Location = new Point(6, 81);
            num_fontsize.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            num_fontsize.Minimum = new decimal(new int[] { 6, 0, 0, 0 });
            num_fontsize.Name = "num_fontsize";
            num_fontsize.Size = new Size(79, 23);
            num_fontsize.TabIndex = 27;
            num_fontsize.Value = new decimal(new int[] { 7, 0, 0, 0 });
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("Segoe UI", 9F);
            label29.Location = new Point(6, 63);
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
            groupBox9.Controls.Add(label4);
            groupBox9.Controls.Add(ck_searchextract);
            groupBox9.Controls.Add(label3);
            groupBox9.Controls.Add(ed_searchkey);
            groupBox9.Controls.Add(label2);
            groupBox9.Controls.Add(cb_searchapi);
            groupBox9.Controls.Add(ck_webgrammar);
            groupBox9.Controls.Add(ck_alwayswebsearch);
            groupBox9.Controls.Add(ck_webkeyword);
            groupBox9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox9.Location = new Point(344, 12);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(326, 226);
            groupBox9.TabIndex = 25;
            groupBox9.TabStop = false;
            groupBox9.Text = "Internet Search";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(6, 131);
            label4.Name = "label4";
            label4.Size = new Size(83, 15);
            label4.TabIndex = 41;
            label4.Text = "wAIfu Plugins";
            // 
            // ck_searchextract
            // 
            ck_searchextract.AutoSize = true;
            ck_searchextract.Font = new Font("Segoe UI", 9F);
            ck_searchextract.Location = new Point(6, 110);
            ck_searchextract.Name = "ck_searchextract";
            ck_searchextract.Size = new Size(209, 19);
            ck_searchextract.TabIndex = 40;
            ck_searchextract.Text = "Try to extract page content (jina.ai)";
            ck_searchextract.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.Location = new Point(6, 63);
            label3.Name = "label3";
            label3.Size = new Size(249, 15);
            label3.TabIndex = 39;
            label3.Text = "Brave Search API Key (Required if using Brave)";
            // 
            // ed_searchkey
            // 
            ed_searchkey.Location = new Point(6, 81);
            ed_searchkey.Name = "ed_searchkey";
            ed_searchkey.PlaceholderText = "comma separated list of words to filter out";
            ed_searchkey.Size = new Size(283, 23);
            ed_searchkey.TabIndex = 38;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F);
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 37;
            label2.Text = "Search API";
            // 
            // cb_searchapi
            // 
            cb_searchapi.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_searchapi.Font = new Font("Segoe UI", 9F);
            cb_searchapi.Items.AddRange(new object[] { "DuckDuckGo", "Brave Search" });
            cb_searchapi.Location = new Point(6, 37);
            cb_searchapi.Name = "cb_searchapi";
            cb_searchapi.Size = new Size(283, 23);
            cb_searchapi.TabIndex = 36;
            // 
            // ck_webgrammar
            // 
            ck_webgrammar.AutoSize = true;
            ck_webgrammar.Font = new Font("Segoe UI", 9F);
            ck_webgrammar.Location = new Point(6, 174);
            ck_webgrammar.Name = "ck_webgrammar";
            ck_webgrammar.Size = new Size(274, 19);
            ck_webgrammar.TabIndex = 33;
            ck_webgrammar.Text = "Browser - Enforce structured output if available";
            ck_webgrammar.UseVisualStyleBackColor = true;
            // 
            // ck_alwayswebsearch
            // 
            ck_alwayswebsearch.AutoSize = true;
            ck_alwayswebsearch.Font = new Font("Segoe UI", 9F);
            ck_alwayswebsearch.Location = new Point(6, 149);
            ck_alwayswebsearch.Name = "ck_alwayswebsearch";
            ck_alwayswebsearch.Size = new Size(250, 19);
            ck_alwayswebsearch.TabIndex = 35;
            ck_alwayswebsearch.Text = "Live Search - Always attempt search (slow)";
            ck_alwayswebsearch.UseVisualStyleBackColor = true;
            // 
            // ck_webkeyword
            // 
            ck_webkeyword.AutoSize = true;
            ck_webkeyword.Font = new Font("Segoe UI", 9F);
            ck_webkeyword.Location = new Point(6, 199);
            ck_webkeyword.Name = "ck_webkeyword";
            ck_webkeyword.Size = new Size(222, 19);
            ck_webkeyword.TabIndex = 32;
            ck_webkeyword.Text = "Browser - Require keyword activation";
            ck_webkeyword.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(bt_chattosessions);
            groupBox2.Controls.Add(bt_importworld);
            groupBox2.Controls.Add(bt_ImportSTChat);
            groupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox2.Location = new Point(344, 423);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(687, 57);
            groupBox2.TabIndex = 24;
            groupBox2.TabStop = false;
            groupBox2.Text = "Import";
            // 
            // bt_chattosessions
            // 
            bt_chattosessions.Font = new Font("Segoe UI", 9F);
            bt_chattosessions.ForeColor = Color.Red;
            bt_chattosessions.Location = new Point(402, 22);
            bt_chattosessions.Name = "bt_chattosessions";
            bt_chattosessions.Size = new Size(195, 23);
            bt_chattosessions.TabIndex = 22;
            bt_chattosessions.Text = "Raw chat to session list";
            bt_chattosessions.UseVisualStyleBackColor = true;
            bt_chattosessions.Click += ConvertChatToSessionList;
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
            groupBox1.Controls.Add(ckThirdPerson);
            groupBox1.Controls.Add(num_ragM);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(num_ragindex);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(num_ragmaxretrieve);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(num_ragcutoff);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(cb_ragheuristic);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.Location = new Point(12, 246);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(326, 234);
            groupBox1.TabIndex = 23;
            groupBox1.TabStop = false;
            groupBox1.Text = "RAG Database Settings";
            // 
            // ckThirdPerson
            // 
            ckThirdPerson.AutoSize = true;
            ckThirdPerson.Font = new Font("Segoe UI", 9F);
            ckThirdPerson.Location = new Point(6, 154);
            ckThirdPerson.Name = "ckThirdPerson";
            ckThirdPerson.Size = new Size(182, 19);
            ckThirdPerson.TabIndex = 38;
            ckThirdPerson.Text = "Convert queries to 3rd person";
            ckThirdPerson.UseVisualStyleBackColor = true;
            // 
            // num_ragM
            // 
            num_ragM.Font = new Font("Segoe UI", 9F);
            num_ragM.Location = new Point(6, 125);
            num_ragM.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            num_ragM.Name = "num_ragM";
            num_ragM.Size = new Size(144, 23);
            num_ragM.TabIndex = 30;
            num_ragM.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F);
            label1.Location = new Point(6, 107);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 29;
            label1.Text = "M Value";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F);
            label15.Location = new Point(160, 19);
            label15.Name = "label15";
            label15.Size = new Size(152, 15);
            label15.TabIndex = 28;
            label15.Text = "Placement Depth (sessions)";
            // 
            // num_ragindex
            // 
            num_ragindex.Font = new Font("Segoe UI", 9F);
            num_ragindex.Location = new Point(160, 37);
            num_ragindex.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_ragindex.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            num_ragindex.Name = "num_ragindex";
            num_ragindex.Size = new Size(144, 23);
            num_ragindex.TabIndex = 27;
            num_ragindex.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F);
            label14.Location = new Point(160, 63);
            label14.Name = "label14";
            label14.Size = new Size(83, 15);
            label14.TabIndex = 26;
            label14.Text = "Max insertions";
            // 
            // num_ragmaxretrieve
            // 
            num_ragmaxretrieve.Font = new Font("Segoe UI", 9F);
            num_ragmaxretrieve.Location = new Point(160, 81);
            num_ragmaxretrieve.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_ragmaxretrieve.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_ragmaxretrieve.Name = "num_ragmaxretrieve";
            num_ragmaxretrieve.Size = new Size(144, 23);
            num_ragmaxretrieve.TabIndex = 25;
            num_ragmaxretrieve.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F);
            label13.Location = new Point(6, 63);
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
            num_ragcutoff.Location = new Point(6, 81);
            num_ragcutoff.Maximum = new decimal(new int[] { 5, 0, 0, 65536 });
            num_ragcutoff.Minimum = new decimal(new int[] { 5, 0, 0, 196608 });
            num_ragcutoff.Name = "num_ragcutoff";
            num_ragcutoff.Size = new Size(144, 23);
            num_ragcutoff.TabIndex = 23;
            num_ragcutoff.Value = new decimal(new int[] { 2, 0, 0, 65536 });
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
            cb_ragheuristic.Items.AddRange(new object[] { "Simple", "Heuristic", "Exact" });
            cb_ragheuristic.Location = new Point(6, 37);
            cb_ragheuristic.Name = "cb_ragheuristic";
            cb_ragheuristic.Size = new Size(144, 23);
            cb_ragheuristic.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(bt_Close);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 483);
            panel1.Name = "panel1";
            panel1.Size = new Size(1043, 50);
            panel1.TabIndex = 31;
            // 
            // bt_Close
            // 
            bt_Close.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bt_Close.BackColor = Color.PaleGreen;
            bt_Close.FlatStyle = FlatStyle.Flat;
            bt_Close.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_Close.Location = new Point(10, 15);
            bt_Close.Name = "bt_Close";
            bt_Close.Size = new Size(1021, 23);
            bt_Close.TabIndex = 1;
            bt_Close.Text = "Apply and Close";
            bt_Close.UseVisualStyleBackColor = false;
            bt_Close.Click += bt_Close_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(ck_hallusafe);
            groupBox3.Controls.Add(ck_sysrag);
            groupBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox3.Location = new Point(12, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(326, 100);
            groupBox3.TabIndex = 32;
            groupBox3.TabStop = false;
            groupBox3.Text = "Core Settings";
            // 
            // ck_hallusafe
            // 
            ck_hallusafe.AutoSize = true;
            ck_hallusafe.Font = new Font("Segoe UI", 9F);
            ck_hallusafe.Location = new Point(6, 47);
            ck_hallusafe.Name = "ck_hallusafe";
            ck_hallusafe.Size = new Size(194, 19);
            ck_hallusafe.TabIndex = 37;
            ck_hallusafe.Text = "Hallucination reduction prompt";
            ck_hallusafe.UseVisualStyleBackColor = true;
            // 
            // ck_sysrag
            // 
            ck_sysrag.AutoSize = true;
            ck_sysrag.Font = new Font("Segoe UI", 9F);
            ck_sysrag.Location = new Point(6, 22);
            ck_sysrag.Name = "ck_sysrag";
            ck_sysrag.Size = new Size(253, 19);
            ck_sysrag.TabIndex = 36;
            ck_sysrag.Text = "Move all memory inserts to system prompt";
            ck_sysrag.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1043, 533);
            Controls.Add(groupBox3);
            Controls.Add(panel1);
            Controls.Add(groupBox24);
            Controls.Add(groupBox11);
            Controls.Add(groupBox10);
            Controls.Add(groupBox2);
            Controls.Add(groupBox9);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
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
            ((System.ComponentModel.ISupportInitialize)num_ragM).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_ragindex).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_ragmaxretrieve).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_ragcutoff).EndInit();
            panel1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private OpenFileDialog openFileDialog1;
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
        private Label label65;
        private ComboBox cb_pastsession;
        private NumericUpDown num_memtokens;
        private Label label32;
        private CheckBox ck_sessionmemory;
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
        private Label label15;
        private NumericUpDown num_ragindex;
        private Label label14;
        private NumericUpDown num_ragmaxretrieve;
        private Label label13;
        private NumericUpDown num_ragcutoff;
        private Label label12;
        private ComboBox cb_ragheuristic;
        private Panel panel1;
        private Button bt_Close;
        private ToolTip HelptoolTip;
        private CheckBox ckShowHidden;
        private CheckBox ck_lastparaphfilter;
        private GroupBox groupBox3;
        private CheckBox ck_sysrag;
        private CheckBox ck_hallusafe;
        private CheckBox ck_forcePW;
        private Label label1;
        private NumericUpDown num_ragM;
        private Label label3;
        private TextBox ed_searchkey;
        private Label label2;
        private ComboBox cb_searchapi;
        private CheckBox ck_searchextract;
        private Label label4;
        private CheckBox ckThirdPerson;
    }
}