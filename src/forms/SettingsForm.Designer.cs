using WaifuAI.Controls;

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
            ck_lastparaphfilter = new ModernCheckBox();
            num_removeitalicmaxword = new NumericUpDown();
            label63 = new Label();
            ck_oneparagraph = new ModernCheckBox();
            ck_remlastsentence = new ModernCheckBox();
            label62 = new Label();
            num_italicratio = new NumericUpDown();
            ck_reduceitalic = new ModernCheckBox();
            ck_noemphasisword = new ModernCheckBox();
            ck_fixquotes = new ModernCheckBox();
            ck_noquotes = new ModernCheckBox();
            ck_unbold = new ModernCheckBox();
            label61 = new Label();
            num_antislopchance = new NumericUpDown();
            ed_sloplist = new TextBox();
            ck_antislop = new ModernCheckBox();
            ck_fixasterix = new ModernCheckBox();
            label65 = new Label();
            cb_pastsession = new ComboBox();
            num_memtokens = new NumericUpDown();
            label32 = new Label();
            ck_sessionmemory = new ModernCheckBox();
            ck_forcePW = new ModernCheckBox();
            ckShowHidden = new ModernCheckBox();
            num_msgcount = new NumericUpDown();
            label30 = new Label();
            num_fontsize = new NumericUpDown();
            label29 = new Label();
            cb_background = new ComboBox();
            label28 = new Label();
            label4 = new Label();
            ck_searchextract = new ModernCheckBox();
            label3 = new Label();
            ed_searchkey = new TextBox();
            label2 = new Label();
            cb_searchapi = new ComboBox();
            ck_webgrammar = new ModernCheckBox();
            ck_alwayswebsearch = new ModernCheckBox();
            ck_webkeyword = new ModernCheckBox();
            bt_chattosessions = new Button();
            bt_importworld = new Button();
            bt_ImportSTChat = new Button();
            ckThirdPerson = new ModernCheckBox();
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
            collapsibleGroupBox1 = new CollapsibleGroupBox();
            ck_hallusafe = new ModernCheckBox();
            ck_sysrag = new ModernCheckBox();
            verticalStackPanel1 = new VerticalStackPanel();
            collapsibleGroupBox3 = new CollapsibleGroupBox();
            collapsibleGroupBox2 = new CollapsibleGroupBox();
            verticalStackPanel2 = new VerticalStackPanel();
            collapsibleGroupBox7 = new CollapsibleGroupBox();
            collapsibleGroupBox4 = new CollapsibleGroupBox();
            verticalStackPanel3 = new VerticalStackPanel();
            collapsibleGroupBox6 = new CollapsibleGroupBox();
            collapsibleGroupBox5 = new CollapsibleGroupBox();
            ((System.ComponentModel.ISupportInitialize)num_removeitalicmaxword).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_italicratio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_antislopchance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_memtokens).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_msgcount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_fontsize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_ragM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_ragindex).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_ragmaxretrieve).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_ragcutoff).BeginInit();
            panel1.SuspendLayout();
            collapsibleGroupBox1.SuspendLayout();
            verticalStackPanel1.SuspendLayout();
            collapsibleGroupBox3.SuspendLayout();
            collapsibleGroupBox2.SuspendLayout();
            verticalStackPanel2.SuspendLayout();
            collapsibleGroupBox7.SuspendLayout();
            collapsibleGroupBox4.SuspendLayout();
            verticalStackPanel3.SuspendLayout();
            collapsibleGroupBox6.SuspendLayout();
            collapsibleGroupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // ck_lastparaphfilter
            // 
            ck_lastparaphfilter.Font = new Font("Segoe UI", 9F);
            ck_lastparaphfilter.Location = new Point(15, 348);
            ck_lastparaphfilter.Name = "ck_lastparaphfilter";
            ck_lastparaphfilter.Size = new Size(330, 26);
            ck_lastparaphfilter.TabIndex = 41;
            ck_lastparaphfilter.Text = "Delete meaningless last paragraph";
            ck_lastparaphfilter.UseVisualStyleBackColor = true;
            // 
            // num_removeitalicmaxword
            // 
            num_removeitalicmaxword.CausesValidation = false;
            num_removeitalicmaxword.Font = new Font("Segoe UI", 9F);
            num_removeitalicmaxword.Location = new Point(282, 269);
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
            label63.Location = new Point(211, 272);
            label63.Name = "label63";
            label63.Size = new Size(66, 15);
            label63.TabIndex = 39;
            label63.Text = "Max Words";
            // 
            // ck_oneparagraph
            // 
            ck_oneparagraph.Font = new Font("Segoe UI", 9F);
            ck_oneparagraph.Location = new Point(15, 323);
            ck_oneparagraph.Name = "ck_oneparagraph";
            ck_oneparagraph.Size = new Size(330, 26);
            ck_oneparagraph.TabIndex = 38;
            ck_oneparagraph.Text = "Stop generation at first paragraph";
            ck_oneparagraph.UseVisualStyleBackColor = true;
            // 
            // ck_remlastsentence
            // 
            ck_remlastsentence.Font = new Font("Segoe UI", 9F);
            ck_remlastsentence.Location = new Point(15, 298);
            ck_remlastsentence.Name = "ck_remlastsentence";
            ck_remlastsentence.Size = new Size(330, 26);
            ck_remlastsentence.TabIndex = 37;
            ck_remlastsentence.Text = "If output > length, remove unfinished sentence";
            ck_remlastsentence.UseVisualStyleBackColor = true;
            // 
            // label62
            // 
            label62.AutoSize = true;
            label62.Font = new Font("Segoe UI", 9F);
            label62.Location = new Point(36, 272);
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
            num_italicratio.Location = new Point(138, 269);
            num_italicratio.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_italicratio.Name = "num_italicratio";
            num_italicratio.Size = new Size(67, 23);
            num_italicratio.TabIndex = 35;
            num_italicratio.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // ck_reduceitalic
            // 
            ck_reduceitalic.Font = new Font("Segoe UI", 9F);
            ck_reduceitalic.Location = new Point(15, 244);
            ck_reduceitalic.Name = "ck_reduceitalic";
            ck_reduceitalic.Size = new Size(330, 26);
            ck_reduceitalic.TabIndex = 34;
            ck_reduceitalic.Text = "Remove a ratio of italic sentences from output";
            ck_reduceitalic.UseVisualStyleBackColor = true;
            // 
            // ck_noemphasisword
            // 
            ck_noemphasisword.Font = new Font("Segoe UI", 9F);
            ck_noemphasisword.Location = new Point(15, 219);
            ck_noemphasisword.Name = "ck_noemphasisword";
            ck_noemphasisword.Size = new Size(330, 26);
            ck_noemphasisword.TabIndex = 33;
            ck_noemphasisword.Text = "Don't emphasis single words (useful for R1 models)";
            ck_noemphasisword.UseVisualStyleBackColor = true;
            // 
            // ck_fixquotes
            // 
            ck_fixquotes.Font = new Font("Segoe UI", 9F);
            ck_fixquotes.Location = new Point(15, 194);
            ck_fixquotes.Name = "ck_fixquotes";
            ck_fixquotes.Size = new Size(330, 26);
            ck_fixquotes.TabIndex = 32;
            ck_fixquotes.Text = "Fix quoted text (useful for QwQ / R1 models)";
            ck_fixquotes.UseVisualStyleBackColor = true;
            // 
            // ck_noquotes
            // 
            ck_noquotes.Font = new Font("Segoe UI", 9F);
            ck_noquotes.Location = new Point(15, 169);
            ck_noquotes.Name = "ck_noquotes";
            ck_noquotes.Size = new Size(330, 26);
            ck_noquotes.TabIndex = 31;
            ck_noquotes.Text = "Don't use quotes (quotation marks will be removed)";
            ck_noquotes.UseVisualStyleBackColor = true;
            // 
            // ck_unbold
            // 
            ck_unbold.Font = new Font("Segoe UI", 9F);
            ck_unbold.Location = new Point(15, 144);
            ck_unbold.Name = "ck_unbold";
            ck_unbold.Size = new Size(330, 26);
            ck_unbold.TabIndex = 30;
            ck_unbold.Text = "Don't bold text (any text in bold turned back to regular)";
            ck_unbold.UseVisualStyleBackColor = true;
            // 
            // label61
            // 
            label61.AutoSize = true;
            label61.Font = new Font("Segoe UI", 9F);
            label61.Location = new Point(36, 114);
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
            num_antislopchance.Location = new Point(138, 111);
            num_antislopchance.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_antislopchance.Name = "num_antislopchance";
            num_antislopchance.Size = new Size(67, 23);
            num_antislopchance.TabIndex = 28;
            num_antislopchance.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // ed_sloplist
            // 
            ed_sloplist.Location = new Point(36, 82);
            ed_sloplist.Name = "ed_sloplist";
            ed_sloplist.PlaceholderText = "comma separated list of words to filter out";
            ed_sloplist.Size = new Size(313, 23);
            ed_sloplist.TabIndex = 2;
            // 
            // ck_antislop
            // 
            ck_antislop.Font = new Font("Segoe UI", 9F);
            ck_antislop.Location = new Point(15, 57);
            ck_antislop.Name = "ck_antislop";
            ck_antislop.Size = new Size(330, 26);
            ck_antislop.TabIndex = 1;
            ck_antislop.Text = "Remove words from list (ad-hoc anti slop)";
            ck_antislop.UseVisualStyleBackColor = true;
            // 
            // ck_fixasterix
            // 
            ck_fixasterix.Font = new Font("Segoe UI", 9F);
            ck_fixasterix.Location = new Point(15, 32);
            ck_fixasterix.Name = "ck_fixasterix";
            ck_fixasterix.Size = new Size(330, 26);
            ck_fixasterix.TabIndex = 0;
            ck_fixasterix.Text = "Attempt to fix missing asterisks";
            ck_fixasterix.UseVisualStyleBackColor = true;
            // 
            // label65
            // 
            label65.AutoSize = true;
            label65.Font = new Font("Segoe UI", 9F);
            label65.Location = new Point(12, 108);
            label65.Name = "label65";
            label65.Size = new Size(135, 15);
            label65.TabIndex = 34;
            label65.Text = "Handling of chat history";
            // 
            // cb_pastsession
            // 
            cb_pastsession.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_pastsession.FlatStyle = FlatStyle.Flat;
            cb_pastsession.Font = new Font("Segoe UI", 9F);
            cb_pastsession.Items.AddRange(new object[] { "Current session only", "Fit as much as possible, including previous sessions" });
            cb_pastsession.Location = new Point(15, 126);
            cb_pastsession.Name = "cb_pastsession";
            cb_pastsession.Size = new Size(291, 23);
            cb_pastsession.TabIndex = 33;
            // 
            // num_memtokens
            // 
            num_memtokens.Font = new Font("Segoe UI", 9F);
            num_memtokens.Increment = new decimal(new int[] { 512, 0, 0, 0 });
            num_memtokens.Location = new Point(15, 67);
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
            label32.Location = new Point(158, 69);
            label32.Name = "label32";
            label32.Size = new Size(94, 15);
            label32.TabIndex = 28;
            label32.Text = "Reserved Tokens";
            // 
            // ck_sessionmemory
            // 
            ck_sessionmemory.Font = new Font("Segoe UI", 9F);
            ck_sessionmemory.Location = new Point(15, 35);
            ck_sessionmemory.Name = "ck_sessionmemory";
            ck_sessionmemory.Size = new Size(291, 26);
            ck_sessionmemory.TabIndex = 24;
            ck_sessionmemory.Text = "Add summaries of past sessions";
            ck_sessionmemory.UseVisualStyleBackColor = true;
            // 
            // ck_forcePW
            // 
            ck_forcePW.Font = new Font("Segoe UI", 9F);
            ck_forcePW.Location = new Point(15, 159);
            ck_forcePW.Name = "ck_forcePW";
            ck_forcePW.Size = new Size(279, 26);
            ck_forcePW.TabIndex = 38;
            ck_forcePW.Text = "Force password when switching bot";
            ck_forcePW.UseVisualStyleBackColor = true;
            // 
            // ckShowHidden
            // 
            ckShowHidden.Font = new Font("Segoe UI", 9F);
            ckShowHidden.Location = new Point(15, 127);
            ckShowHidden.Name = "ckShowHidden";
            ckShowHidden.Size = new Size(200, 26);
            ckShowHidden.TabIndex = 31;
            ckShowHidden.Text = "Show hidden system messages";
            ckShowHidden.UseVisualStyleBackColor = true;
            // 
            // num_msgcount
            // 
            num_msgcount.Font = new Font("Segoe UI", 9F);
            num_msgcount.Increment = new decimal(new int[] { 20, 0, 0, 0 });
            num_msgcount.Location = new Point(136, 98);
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
            label30.Location = new Point(136, 80);
            label30.Name = "label30";
            label30.Size = new Size(97, 15);
            label30.TabIndex = 30;
            label30.Text = "Shown Messages";
            // 
            // num_fontsize
            // 
            num_fontsize.Font = new Font("Segoe UI", 9F);
            num_fontsize.Location = new Point(15, 98);
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
            label29.Location = new Point(15, 80);
            label29.Name = "label29";
            label29.Size = new Size(54, 15);
            label29.TabIndex = 28;
            label29.Text = "Font Size";
            // 
            // cb_background
            // 
            cb_background.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_background.FlatStyle = FlatStyle.Flat;
            cb_background.Font = new Font("Segoe UI", 9F);
            cb_background.Location = new Point(15, 50);
            cb_background.Name = "cb_background";
            cb_background.Size = new Size(278, 23);
            cb_background.TabIndex = 27;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("Segoe UI", 9F);
            label28.Location = new Point(15, 32);
            label28.Name = "label28";
            label28.Size = new Size(99, 15);
            label28.TabIndex = 26;
            label28.Text = "Chat Background";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(31, 172);
            label4.Name = "label4";
            label4.Size = new Size(83, 15);
            label4.TabIndex = 41;
            label4.Text = "wAIfu Plugins";
            // 
            // ck_searchextract
            // 
            ck_searchextract.Font = new Font("Segoe UI", 9F);
            ck_searchextract.Location = new Point(15, 144);
            ck_searchextract.Name = "ck_searchextract";
            ck_searchextract.Size = new Size(278, 26);
            ck_searchextract.TabIndex = 40;
            ck_searchextract.Text = "Try to extract page content (jina.ai)";
            ck_searchextract.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic);
            label3.Location = new Point(15, 88);
            label3.Name = "label3";
            label3.Size = new Size(266, 15);
            label3.TabIndex = 39;
            label3.Text = "Brave Search API Key (Required if using Brave)";
            // 
            // ed_searchkey
            // 
            ed_searchkey.Location = new Point(15, 106);
            ed_searchkey.Name = "ed_searchkey";
            ed_searchkey.PlaceholderText = "comma separated list of words to filter out";
            ed_searchkey.Size = new Size(278, 23);
            ed_searchkey.TabIndex = 38;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic);
            label2.Location = new Point(14, 36);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 37;
            label2.Text = "Search API";
            // 
            // cb_searchapi
            // 
            cb_searchapi.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_searchapi.FlatStyle = FlatStyle.Flat;
            cb_searchapi.Font = new Font("Segoe UI", 9F);
            cb_searchapi.Items.AddRange(new object[] { "DuckDuckGo", "Brave Search" });
            cb_searchapi.Location = new Point(14, 54);
            cb_searchapi.Name = "cb_searchapi";
            cb_searchapi.Size = new Size(280, 23);
            cb_searchapi.TabIndex = 36;
            // 
            // ck_webgrammar
            // 
            ck_webgrammar.Font = new Font("Segoe UI", 9F);
            ck_webgrammar.Location = new Point(15, 226);
            ck_webgrammar.Name = "ck_webgrammar";
            ck_webgrammar.Size = new Size(268, 26);
            ck_webgrammar.TabIndex = 33;
            ck_webgrammar.Text = "Browser - Enforce structured output";
            ck_webgrammar.UseVisualStyleBackColor = true;
            // 
            // ck_alwayswebsearch
            // 
            ck_alwayswebsearch.Font = new Font("Segoe UI", 9F);
            ck_alwayswebsearch.Location = new Point(15, 194);
            ck_alwayswebsearch.Name = "ck_alwayswebsearch";
            ck_alwayswebsearch.Size = new Size(279, 26);
            ck_alwayswebsearch.TabIndex = 35;
            ck_alwayswebsearch.Text = "Live Search - Always attempt search (slow)";
            ck_alwayswebsearch.UseVisualStyleBackColor = true;
            // 
            // ck_webkeyword
            // 
            ck_webkeyword.Font = new Font("Segoe UI", 9F);
            ck_webkeyword.Location = new Point(15, 258);
            ck_webkeyword.Name = "ck_webkeyword";
            ck_webkeyword.Size = new Size(278, 26);
            ck_webkeyword.TabIndex = 32;
            ck_webkeyword.Text = "Browser - Require keyword activation";
            ck_webkeyword.UseVisualStyleBackColor = true;
            // 
            // bt_chattosessions
            // 
            bt_chattosessions.BackColor = Color.Silver;
            bt_chattosessions.FlatStyle = FlatStyle.Flat;
            bt_chattosessions.Font = new Font("Segoe UI", 9F);
            bt_chattosessions.ForeColor = Color.Red;
            bt_chattosessions.Location = new Point(15, 93);
            bt_chattosessions.Name = "bt_chattosessions";
            bt_chattosessions.Size = new Size(334, 23);
            bt_chattosessions.TabIndex = 22;
            bt_chattosessions.Tag = "no-theme";
            bt_chattosessions.Text = "Raw chat to session list";
            bt_chattosessions.UseVisualStyleBackColor = false;
            bt_chattosessions.Click += ConvertChatToSessionList;
            // 
            // bt_importworld
            // 
            bt_importworld.BackColor = Color.Silver;
            bt_importworld.FlatStyle = FlatStyle.Flat;
            bt_importworld.Font = new Font("Segoe UI", 9F);
            bt_importworld.ForeColor = Color.Black;
            bt_importworld.Location = new Point(15, 64);
            bt_importworld.Name = "bt_importworld";
            bt_importworld.Size = new Size(334, 23);
            bt_importworld.TabIndex = 2;
            bt_importworld.Tag = "no-theme";
            bt_importworld.Text = "Import ST WorldInfo";
            bt_importworld.UseVisualStyleBackColor = false;
            bt_importworld.Click += bt_importworld_Click;
            // 
            // bt_ImportSTChat
            // 
            bt_ImportSTChat.BackColor = Color.Silver;
            bt_ImportSTChat.FlatStyle = FlatStyle.Flat;
            bt_ImportSTChat.Font = new Font("Segoe UI", 9F);
            bt_ImportSTChat.ForeColor = Color.Black;
            bt_ImportSTChat.Location = new Point(15, 35);
            bt_ImportSTChat.Name = "bt_ImportSTChat";
            bt_ImportSTChat.Size = new Size(334, 23);
            bt_ImportSTChat.TabIndex = 1;
            bt_ImportSTChat.Tag = "no-theme";
            bt_ImportSTChat.Text = "Import ST Chat";
            bt_ImportSTChat.UseVisualStyleBackColor = false;
            bt_ImportSTChat.Click += bt_ImportSTChat_Click;
            // 
            // ckThirdPerson
            // 
            ckThirdPerson.Font = new Font("Segoe UI", 9F);
            ckThirdPerson.Location = new Point(15, 180);
            ckThirdPerson.Name = "ckThirdPerson";
            ckThirdPerson.Size = new Size(200, 26);
            ckThirdPerson.TabIndex = 38;
            ckThirdPerson.Text = "Convert queries to 3rd person";
            ckThirdPerson.UseVisualStyleBackColor = true;
            // 
            // num_ragM
            // 
            num_ragM.Font = new Font("Segoe UI", 9F);
            num_ragM.Location = new Point(15, 147);
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
            label1.Location = new Point(15, 129);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 29;
            label1.Text = "M Value";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F);
            label15.Location = new Point(169, 41);
            label15.Name = "label15";
            label15.Size = new Size(152, 15);
            label15.TabIndex = 28;
            label15.Text = "Placement Depth (sessions)";
            // 
            // num_ragindex
            // 
            num_ragindex.Font = new Font("Segoe UI", 9F);
            num_ragindex.Location = new Point(169, 59);
            num_ragindex.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_ragindex.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            num_ragindex.Name = "num_ragindex";
            num_ragindex.Size = new Size(137, 23);
            num_ragindex.TabIndex = 27;
            num_ragindex.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F);
            label14.Location = new Point(169, 85);
            label14.Name = "label14";
            label14.Size = new Size(83, 15);
            label14.TabIndex = 26;
            label14.Text = "Max insertions";
            // 
            // num_ragmaxretrieve
            // 
            num_ragmaxretrieve.Font = new Font("Segoe UI", 9F);
            num_ragmaxretrieve.Location = new Point(169, 103);
            num_ragmaxretrieve.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_ragmaxretrieve.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_ragmaxretrieve.Name = "num_ragmaxretrieve";
            num_ragmaxretrieve.Size = new Size(137, 23);
            num_ragmaxretrieve.TabIndex = 25;
            num_ragmaxretrieve.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F);
            label13.Location = new Point(15, 85);
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
            num_ragcutoff.Location = new Point(15, 103);
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
            label12.Location = new Point(15, 41);
            label12.Name = "label12";
            label12.Size = new Size(125, 15);
            label12.TabIndex = 4;
            label12.Text = "RAG Heuristic Method";
            // 
            // cb_ragheuristic
            // 
            cb_ragheuristic.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_ragheuristic.FlatStyle = FlatStyle.Flat;
            cb_ragheuristic.Font = new Font("Segoe UI", 9F);
            cb_ragheuristic.Items.AddRange(new object[] { "Simple", "Heuristic", "Exact" });
            cb_ragheuristic.Location = new Point(15, 59);
            cb_ragheuristic.Name = "cb_ragheuristic";
            cb_ragheuristic.Size = new Size(144, 23);
            cb_ragheuristic.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(bt_Close);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 537);
            panel1.Name = "panel1";
            panel1.Size = new Size(1006, 50);
            panel1.TabIndex = 31;
            // 
            // bt_Close
            // 
            bt_Close.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bt_Close.BackColor = Color.DarkSeaGreen;
            bt_Close.FlatStyle = FlatStyle.Flat;
            bt_Close.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_Close.ForeColor = Color.Black;
            bt_Close.Location = new Point(10, 15);
            bt_Close.Name = "bt_Close";
            bt_Close.Size = new Size(984, 23);
            bt_Close.TabIndex = 1;
            bt_Close.Tag = "no-theme";
            bt_Close.Text = "Apply and Close";
            bt_Close.UseVisualStyleBackColor = false;
            bt_Close.Click += bt_Close_Click;
            // 
            // collapsibleGroupBox1
            // 
            collapsibleGroupBox1.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox1.CanCollapse = false;
            collapsibleGroupBox1.Controls.Add(ck_hallusafe);
            collapsibleGroupBox1.Controls.Add(ck_sysrag);
            collapsibleGroupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox1.Location = new Point(0, 6);
            collapsibleGroupBox1.Name = "collapsibleGroupBox1";
            collapsibleGroupBox1.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox1.Size = new Size(321, 95);
            collapsibleGroupBox1.TabIndex = 33;
            collapsibleGroupBox1.Text = "Core Settings";
            // 
            // ck_hallusafe
            // 
            ck_hallusafe.Dock = DockStyle.Top;
            ck_hallusafe.Font = new Font("Segoe UI", 9F);
            ck_hallusafe.Location = new Point(12, 58);
            ck_hallusafe.Name = "ck_hallusafe";
            ck_hallusafe.Size = new Size(297, 26);
            ck_hallusafe.TabIndex = 37;
            ck_hallusafe.Text = "Hallucination reduction prompt";
            ck_hallusafe.UseVisualStyleBackColor = true;
            // 
            // ck_sysrag
            // 
            ck_sysrag.Dock = DockStyle.Top;
            ck_sysrag.Font = new Font("Segoe UI", 9F);
            ck_sysrag.Location = new Point(12, 32);
            ck_sysrag.Name = "ck_sysrag";
            ck_sysrag.Size = new Size(297, 26);
            ck_sysrag.TabIndex = 36;
            ck_sysrag.Text = "Move all memory inserts to system prompt";
            ck_sysrag.UseVisualStyleBackColor = true;
            // 
            // verticalStackPanel1
            // 
            verticalStackPanel1.Controls.Add(collapsibleGroupBox3);
            verticalStackPanel1.Controls.Add(collapsibleGroupBox2);
            verticalStackPanel1.Controls.Add(collapsibleGroupBox1);
            verticalStackPanel1.Dock = DockStyle.Left;
            verticalStackPanel1.Location = new Point(0, 0);
            verticalStackPanel1.Name = "verticalStackPanel1";
            verticalStackPanel1.Padding = new Padding(0, 6, 0, 6);
            verticalStackPanel1.Size = new Size(321, 537);
            verticalStackPanel1.TabIndex = 34;
            // 
            // collapsibleGroupBox3
            // 
            collapsibleGroupBox3.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox3.CanCollapse = false;
            collapsibleGroupBox3.Controls.Add(ckThirdPerson);
            collapsibleGroupBox3.Controls.Add(label12);
            collapsibleGroupBox3.Controls.Add(num_ragM);
            collapsibleGroupBox3.Controls.Add(cb_ragheuristic);
            collapsibleGroupBox3.Controls.Add(label1);
            collapsibleGroupBox3.Controls.Add(num_ragcutoff);
            collapsibleGroupBox3.Controls.Add(label15);
            collapsibleGroupBox3.Controls.Add(label13);
            collapsibleGroupBox3.Controls.Add(num_ragindex);
            collapsibleGroupBox3.Controls.Add(num_ragmaxretrieve);
            collapsibleGroupBox3.Controls.Add(label14);
            collapsibleGroupBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox3.Location = new Point(0, 284);
            collapsibleGroupBox3.Name = "collapsibleGroupBox3";
            collapsibleGroupBox3.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox3.Size = new Size(321, 248);
            collapsibleGroupBox3.TabIndex = 35;
            collapsibleGroupBox3.Text = "Retrieval Augmented Generation";
            // 
            // collapsibleGroupBox2
            // 
            collapsibleGroupBox2.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox2.CanCollapse = false;
            collapsibleGroupBox2.Controls.Add(label65);
            collapsibleGroupBox2.Controls.Add(ck_sessionmemory);
            collapsibleGroupBox2.Controls.Add(cb_pastsession);
            collapsibleGroupBox2.Controls.Add(label32);
            collapsibleGroupBox2.Controls.Add(num_memtokens);
            collapsibleGroupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox2.Location = new Point(0, 109);
            collapsibleGroupBox2.Name = "collapsibleGroupBox2";
            collapsibleGroupBox2.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox2.Size = new Size(321, 167);
            collapsibleGroupBox2.TabIndex = 34;
            collapsibleGroupBox2.Text = "Session Memory System";
            // 
            // verticalStackPanel2
            // 
            verticalStackPanel2.Controls.Add(collapsibleGroupBox7);
            verticalStackPanel2.Controls.Add(collapsibleGroupBox4);
            verticalStackPanel2.Dock = DockStyle.Right;
            verticalStackPanel2.Location = new Point(641, 0);
            verticalStackPanel2.Name = "verticalStackPanel2";
            verticalStackPanel2.Padding = new Padding(0, 6, 0, 6);
            verticalStackPanel2.Size = new Size(365, 537);
            verticalStackPanel2.TabIndex = 35;
            // 
            // collapsibleGroupBox7
            // 
            collapsibleGroupBox7.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox7.CanCollapse = false;
            collapsibleGroupBox7.Controls.Add(bt_chattosessions);
            collapsibleGroupBox7.Controls.Add(bt_ImportSTChat);
            collapsibleGroupBox7.Controls.Add(bt_importworld);
            collapsibleGroupBox7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox7.Location = new Point(0, 400);
            collapsibleGroupBox7.Name = "collapsibleGroupBox7";
            collapsibleGroupBox7.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox7.Size = new Size(365, 132);
            collapsibleGroupBox7.TabIndex = 1;
            collapsibleGroupBox7.Text = "Import Files";
            // 
            // collapsibleGroupBox4
            // 
            collapsibleGroupBox4.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox4.CanCollapse = false;
            collapsibleGroupBox4.Controls.Add(ck_lastparaphfilter);
            collapsibleGroupBox4.Controls.Add(ck_fixasterix);
            collapsibleGroupBox4.Controls.Add(num_removeitalicmaxword);
            collapsibleGroupBox4.Controls.Add(ck_antislop);
            collapsibleGroupBox4.Controls.Add(label63);
            collapsibleGroupBox4.Controls.Add(ed_sloplist);
            collapsibleGroupBox4.Controls.Add(ck_oneparagraph);
            collapsibleGroupBox4.Controls.Add(num_antislopchance);
            collapsibleGroupBox4.Controls.Add(ck_remlastsentence);
            collapsibleGroupBox4.Controls.Add(label61);
            collapsibleGroupBox4.Controls.Add(label62);
            collapsibleGroupBox4.Controls.Add(ck_unbold);
            collapsibleGroupBox4.Controls.Add(num_italicratio);
            collapsibleGroupBox4.Controls.Add(ck_noquotes);
            collapsibleGroupBox4.Controls.Add(ck_reduceitalic);
            collapsibleGroupBox4.Controls.Add(ck_fixquotes);
            collapsibleGroupBox4.Controls.Add(ck_noemphasisword);
            collapsibleGroupBox4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox4.Location = new Point(0, 6);
            collapsibleGroupBox4.Name = "collapsibleGroupBox4";
            collapsibleGroupBox4.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox4.Size = new Size(365, 386);
            collapsibleGroupBox4.TabIndex = 0;
            collapsibleGroupBox4.Text = "Output Formatting";
            // 
            // verticalStackPanel3
            // 
            verticalStackPanel3.Controls.Add(collapsibleGroupBox6);
            verticalStackPanel3.Controls.Add(collapsibleGroupBox5);
            verticalStackPanel3.Dock = DockStyle.Fill;
            verticalStackPanel3.Location = new Point(321, 0);
            verticalStackPanel3.Name = "verticalStackPanel3";
            verticalStackPanel3.Padding = new Padding(6);
            verticalStackPanel3.Size = new Size(320, 537);
            verticalStackPanel3.TabIndex = 36;
            // 
            // collapsibleGroupBox6
            // 
            collapsibleGroupBox6.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox6.CanCollapse = false;
            collapsibleGroupBox6.Controls.Add(ck_forcePW);
            collapsibleGroupBox6.Controls.Add(label28);
            collapsibleGroupBox6.Controls.Add(ckShowHidden);
            collapsibleGroupBox6.Controls.Add(cb_background);
            collapsibleGroupBox6.Controls.Add(num_msgcount);
            collapsibleGroupBox6.Controls.Add(label29);
            collapsibleGroupBox6.Controls.Add(label30);
            collapsibleGroupBox6.Controls.Add(num_fontsize);
            collapsibleGroupBox6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox6.Location = new Point(6, 313);
            collapsibleGroupBox6.Name = "collapsibleGroupBox6";
            collapsibleGroupBox6.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox6.Size = new Size(308, 219);
            collapsibleGroupBox6.TabIndex = 1;
            collapsibleGroupBox6.Text = "User Interface";
            // 
            // collapsibleGroupBox5
            // 
            collapsibleGroupBox5.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox5.CanCollapse = false;
            collapsibleGroupBox5.Controls.Add(label4);
            collapsibleGroupBox5.Controls.Add(label2);
            collapsibleGroupBox5.Controls.Add(ck_searchextract);
            collapsibleGroupBox5.Controls.Add(ck_webkeyword);
            collapsibleGroupBox5.Controls.Add(label3);
            collapsibleGroupBox5.Controls.Add(ck_alwayswebsearch);
            collapsibleGroupBox5.Controls.Add(ed_searchkey);
            collapsibleGroupBox5.Controls.Add(ck_webgrammar);
            collapsibleGroupBox5.Controls.Add(cb_searchapi);
            collapsibleGroupBox5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox5.Location = new Point(6, 6);
            collapsibleGroupBox5.Name = "collapsibleGroupBox5";
            collapsibleGroupBox5.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox5.Size = new Size(308, 299);
            collapsibleGroupBox5.TabIndex = 0;
            collapsibleGroupBox5.Text = "Web Search";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1006, 587);
            Controls.Add(verticalStackPanel3);
            Controls.Add(verticalStackPanel2);
            Controls.Add(verticalStackPanel1);
            Controls.Add(panel1);
            ForeColor = SystemColors.ButtonFace;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)num_removeitalicmaxword).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_italicratio).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_antislopchance).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_memtokens).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_msgcount).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_fontsize).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_ragM).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_ragindex).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_ragmaxretrieve).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_ragcutoff).EndInit();
            panel1.ResumeLayout(false);
            collapsibleGroupBox1.ResumeLayout(false);
            verticalStackPanel1.ResumeLayout(false);
            collapsibleGroupBox3.ResumeLayout(false);
            collapsibleGroupBox3.PerformLayout();
            collapsibleGroupBox2.ResumeLayout(false);
            collapsibleGroupBox2.PerformLayout();
            verticalStackPanel2.ResumeLayout(false);
            collapsibleGroupBox7.ResumeLayout(false);
            collapsibleGroupBox4.ResumeLayout(false);
            collapsibleGroupBox4.PerformLayout();
            verticalStackPanel3.ResumeLayout(false);
            collapsibleGroupBox6.ResumeLayout(false);
            collapsibleGroupBox6.PerformLayout();
            collapsibleGroupBox5.ResumeLayout(false);
            collapsibleGroupBox5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private NumericUpDown num_removeitalicmaxword;
        private Label label63;
        private ModernCheckBox ck_oneparagraph;
        private ModernCheckBox ck_remlastsentence;
        private Label label62;
        private NumericUpDown num_italicratio;
        private ModernCheckBox ck_reduceitalic;
        private ModernCheckBox ck_noemphasisword;
        private ModernCheckBox ck_fixquotes;
        private ModernCheckBox ck_noquotes;
        private ModernCheckBox ck_unbold;
        private Label label61;
        private NumericUpDown num_antislopchance;
        private TextBox ed_sloplist;
        private ModernCheckBox ck_antislop;
        private ModernCheckBox ck_fixasterix;
        private Label label65;
        private ComboBox cb_pastsession;
        private NumericUpDown num_memtokens;
        private Label label32;
        private ModernCheckBox ck_sessionmemory;
        private NumericUpDown num_msgcount;
        private Label label30;
        private NumericUpDown num_fontsize;
        private Label label29;
        private ComboBox cb_background;
        private Label label28;
        private ModernCheckBox ck_webgrammar;
        private ModernCheckBox ck_webkeyword;
        private Button bt_chattosessions;
        private Button bt_importworld;
        private Button bt_ImportSTChat;
        private ModernCheckBox ck_alwayswebsearch;
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
        private ModernCheckBox ckShowHidden;
        private ModernCheckBox ck_lastparaphfilter;
        private ModernCheckBox ck_forcePW;
        private Label label1;
        private NumericUpDown num_ragM;
        private Label label3;
        private TextBox ed_searchkey;
        private Label label2;
        private ComboBox cb_searchapi;
        private ModernCheckBox ck_searchextract;
        private Label label4;
        private ModernCheckBox ckThirdPerson;
        private Controls.CollapsibleGroupBox collapsibleGroupBox1;
        private ModernCheckBox ck_hallusafe;
        private ModernCheckBox ck_sysrag;
        private Controls.VerticalStackPanel verticalStackPanel1;
        private Controls.CollapsibleGroupBox collapsibleGroupBox3;
        private Controls.CollapsibleGroupBox collapsibleGroupBox2;
        private Controls.VerticalStackPanel verticalStackPanel2;
        private Controls.CollapsibleGroupBox collapsibleGroupBox4;
        private Controls.CollapsibleGroupBox collapsibleGroupBox7;
        private Controls.VerticalStackPanel verticalStackPanel3;
        private Controls.CollapsibleGroupBox collapsibleGroupBox6;
        private Controls.CollapsibleGroupBox collapsibleGroupBox5;
    }
}