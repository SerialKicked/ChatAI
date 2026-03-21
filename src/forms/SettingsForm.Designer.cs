using LetheChat.Controls;

namespace LetheChat.src.forms
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
            num_removeitalicmaxword = new ModernNumericUpDown();
            label63 = new Label();
            ck_oneparagraph = new ModernCheckBox();
            ck_remlastsentence = new ModernCheckBox();
            label62 = new Label();
            num_italicratio = new ModernNumericUpDown();
            ck_reduceitalic = new ModernCheckBox();
            ck_noemphasisword = new ModernCheckBox();
            ck_fixquotes = new ModernCheckBox();
            ck_noquotes = new ModernCheckBox();
            ck_unbold = new ModernCheckBox();
            label61 = new Label();
            num_antislopchance = new ModernNumericUpDown();
            ed_sloplist = new TextBox();
            ck_antislop = new ModernCheckBox();
            ck_fixasterix = new ModernCheckBox();
            label65 = new Label();
            cb_pastsession = new ModernComboBox();
            num_memtokens = new ModernNumericUpDown();
            label32 = new Label();
            ck_sessionmemory = new ModernCheckBox();
            ck_forcePW = new ModernCheckBox();
            ckShowHidden = new ModernCheckBox();
            num_msgcount = new ModernNumericUpDown();
            label30 = new Label();
            num_fontsize = new ModernNumericUpDown();
            label29 = new Label();
            cb_background = new ModernComboBox();
            label28 = new Label();
            label4 = new Label();
            ck_searchextract = new ModernCheckBox();
            label3 = new Label();
            ed_searchkey = new TextBox();
            label2 = new Label();
            cb_searchapi = new ModernComboBox();
            ck_webgrammar = new ModernCheckBox();
            ck_alwayswebsearch = new ModernCheckBox();
            ck_webkeyword = new ModernCheckBox();
            bt_chattosessions = new Button();
            bt_importworld = new Button();
            bt_ImportSTChat = new Button();
            ckThirdPerson = new ModernCheckBox();
            num_ragM = new ModernNumericUpDown();
            label1 = new Label();
            label15 = new Label();
            num_ragindex = new ModernNumericUpDown();
            label14 = new Label();
            num_ragmaxretrieve = new ModernNumericUpDown();
            label13 = new Label();
            num_ragcutoff = new ModernNumericUpDown();
            label12 = new Label();
            cb_ragheuristic = new ModernComboBox();
            panel1 = new Panel();
            bt_Close = new Button();
            HelptoolTip = new ToolTip(components);
            collapsibleGroupBox1 = new CollapsibleGroupBox();
            ckForceInternalGram = new ModernCheckBox();
            ckNoPastInserts = new ModernCheckBox();
            ck_hallusafe = new ModernCheckBox();
            ck_sysrag = new ModernCheckBox();
            verticalStackPanel1 = new VerticalStackPanel();
            collapsibleGroupBox5 = new CollapsibleGroupBox();
            panel2 = new Panel();
            panel7 = new Panel();
            collapsibleGroupBox8 = new CollapsibleGroupBox();
            ckGroupCommit = new ModernCheckBox();
            ckGroupAltern = new ModernCheckBox();
            cbGroupSessionStrategy = new ModernComboBox();
            label9 = new Label();
            panel6 = new Panel();
            numGroupQueue = new ModernNumericUpDown();
            label8 = new Label();
            ckGroupRouting = new ModernComboBox();
            label7 = new Label();
            collapsibleGroupBox3 = new CollapsibleGroupBox();
            numWIEntries = new ModernNumericUpDown();
            label6 = new Label();
            collapsibleGroupBox2 = new CollapsibleGroupBox();
            ckDetailedSum = new ModernCheckBox();
            mck_cutmiddle = new ModernCheckBox();
            panel8 = new Panel();
            collapsibleGroupBox7 = new CollapsibleGroupBox();
            verticalStackPanel2 = new VerticalStackPanel();
            collapsibleGroupBox10 = new CollapsibleGroupBox();
            panel14 = new Panel();
            num_ImgCount = new ModernNumericUpDown();
            label18 = new Label();
            panel9 = new Panel();
            num_imgEmbed = new ModernNumericUpDown();
            label19 = new Label();
            collapsibleGroupBox4 = new CollapsibleGroupBox();
            ckParenthesizeToItalic = new ModernCheckBox();
            ckDelStartSlop = new ModernCheckBox();
            panel5 = new Panel();
            panel3 = new Panel();
            collapsibleGroupBox6 = new CollapsibleGroupBox();
            mcbSkin = new ModernComboBox();
            label5 = new Label();
            panel4 = new Panel();
            verticalStackPanel3 = new VerticalStackPanel();
            collapsibleGroupBox9 = new CollapsibleGroupBox();
            panel13 = new Panel();
            numFactSuper = new ModernNumericUpDown();
            label17 = new Label();
            panel12 = new Panel();
            numFactRetrieval = new ModernNumericUpDown();
            label16 = new Label();
            panel11 = new Panel();
            numFactDedup = new ModernNumericUpDown();
            label10 = new Label();
            panel10 = new Panel();
            numFactTokens = new ModernNumericUpDown();
            label11 = new Label();
            ckFactRetrieval = new ModernCheckBox();
            verticalStackPanel4 = new VerticalStackPanel();
            collapsibleGroupBox11 = new CollapsibleGroupBox();
            ckLlamaCppSamplers = new ModernCheckBox();
            panel15 = new Panel();
            cbChatThinkPolicy = new ModernComboBox();
            label20 = new Label();
            panel16 = new Panel();
            cbChatAllowPrefill = new ModernComboBox();
            label21 = new Label();
            panel17 = new Panel();
            cbParallel = new ModernComboBox();
            label22 = new Label();
            panel1.SuspendLayout();
            collapsibleGroupBox1.SuspendLayout();
            verticalStackPanel1.SuspendLayout();
            collapsibleGroupBox5.SuspendLayout();
            collapsibleGroupBox8.SuspendLayout();
            panel6.SuspendLayout();
            collapsibleGroupBox3.SuspendLayout();
            collapsibleGroupBox2.SuspendLayout();
            panel8.SuspendLayout();
            collapsibleGroupBox7.SuspendLayout();
            verticalStackPanel2.SuspendLayout();
            collapsibleGroupBox10.SuspendLayout();
            panel14.SuspendLayout();
            panel9.SuspendLayout();
            collapsibleGroupBox4.SuspendLayout();
            panel5.SuspendLayout();
            panel3.SuspendLayout();
            collapsibleGroupBox6.SuspendLayout();
            verticalStackPanel3.SuspendLayout();
            collapsibleGroupBox9.SuspendLayout();
            panel13.SuspendLayout();
            panel12.SuspendLayout();
            panel11.SuspendLayout();
            panel10.SuspendLayout();
            verticalStackPanel4.SuspendLayout();
            collapsibleGroupBox11.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // ck_lastparaphfilter
            // 
            ck_lastparaphfilter.Dock = DockStyle.Top;
            ck_lastparaphfilter.Font = new Font("Segoe UI", 9F);
            ck_lastparaphfilter.Location = new Point(12, 393);
            ck_lastparaphfilter.Name = "ck_lastparaphfilter";
            ck_lastparaphfilter.Size = new Size(331, 26);
            ck_lastparaphfilter.TabIndex = 41;
            ck_lastparaphfilter.Text = "Delete meaningless last paragraph";
            ck_lastparaphfilter.UseVisualStyleBackColor = true;
            // 
            // num_removeitalicmaxword
            // 
            num_removeitalicmaxword.BackColor = Color.FromArgb(64, 64, 64);
            num_removeitalicmaxword.CausesValidation = false;
            num_removeitalicmaxword.Font = new Font("Segoe UI", 9F);
            num_removeitalicmaxword.Location = new Point(257, 9);
            num_removeitalicmaxword.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            num_removeitalicmaxword.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_removeitalicmaxword.Name = "num_removeitalicmaxword";
            num_removeitalicmaxword.Padding = new Padding(1);
            num_removeitalicmaxword.Size = new Size(67, 23);
            num_removeitalicmaxword.TabIndex = 40;
            num_removeitalicmaxword.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label63
            // 
            label63.AutoSize = true;
            label63.Font = new Font("Segoe UI", 9F);
            label63.Location = new Point(186, 12);
            label63.Name = "label63";
            label63.Size = new Size(66, 15);
            label63.TabIndex = 39;
            label63.Text = "Max Words";
            // 
            // ck_oneparagraph
            // 
            ck_oneparagraph.Dock = DockStyle.Top;
            ck_oneparagraph.Font = new Font("Segoe UI", 9F);
            ck_oneparagraph.Location = new Point(12, 315);
            ck_oneparagraph.Name = "ck_oneparagraph";
            ck_oneparagraph.Size = new Size(331, 26);
            ck_oneparagraph.TabIndex = 38;
            ck_oneparagraph.Text = "Stop generation at first paragraph";
            ck_oneparagraph.UseVisualStyleBackColor = true;
            // 
            // ck_remlastsentence
            // 
            ck_remlastsentence.Dock = DockStyle.Top;
            ck_remlastsentence.Font = new Font("Segoe UI", 9F);
            ck_remlastsentence.Location = new Point(12, 289);
            ck_remlastsentence.Name = "ck_remlastsentence";
            ck_remlastsentence.Size = new Size(331, 26);
            ck_remlastsentence.TabIndex = 37;
            ck_remlastsentence.Text = "If output > length, remove unfinished sentence";
            ck_remlastsentence.UseVisualStyleBackColor = true;
            // 
            // label62
            // 
            label62.AutoSize = true;
            label62.Font = new Font("Segoe UI", 9F);
            label62.Location = new Point(11, 12);
            label62.Name = "label62";
            label62.Size = new Size(96, 15);
            label62.TabIndex = 36;
            label62.Text = "Removal Chance";
            // 
            // num_italicratio
            // 
            num_italicratio.BackColor = Color.FromArgb(64, 64, 64);
            num_italicratio.CausesValidation = false;
            num_italicratio.DecimalPlaces = 2;
            num_italicratio.Font = new Font("Segoe UI", 9F);
            num_italicratio.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_italicratio.Location = new Point(113, 9);
            num_italicratio.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_italicratio.Name = "num_italicratio";
            num_italicratio.Padding = new Padding(1);
            num_italicratio.Size = new Size(67, 23);
            num_italicratio.TabIndex = 35;
            num_italicratio.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // ck_reduceitalic
            // 
            ck_reduceitalic.Dock = DockStyle.Top;
            ck_reduceitalic.Font = new Font("Segoe UI", 9F);
            ck_reduceitalic.Location = new Point(12, 119);
            ck_reduceitalic.Name = "ck_reduceitalic";
            ck_reduceitalic.Size = new Size(331, 26);
            ck_reduceitalic.TabIndex = 34;
            ck_reduceitalic.Text = "Remove a ratio of italic sentences from output";
            ck_reduceitalic.UseVisualStyleBackColor = true;
            // 
            // ck_noemphasisword
            // 
            ck_noemphasisword.Dock = DockStyle.Top;
            ck_noemphasisword.Font = new Font("Segoe UI", 9F);
            ck_noemphasisword.Location = new Point(12, 185);
            ck_noemphasisword.Name = "ck_noemphasisword";
            ck_noemphasisword.Size = new Size(331, 26);
            ck_noemphasisword.TabIndex = 33;
            ck_noemphasisword.Text = "Don't emphasis single words (useful for R1 models)";
            ck_noemphasisword.UseVisualStyleBackColor = true;
            // 
            // ck_fixquotes
            // 
            ck_fixquotes.Dock = DockStyle.Top;
            ck_fixquotes.Font = new Font("Segoe UI", 9F);
            ck_fixquotes.Location = new Point(12, 211);
            ck_fixquotes.Name = "ck_fixquotes";
            ck_fixquotes.Size = new Size(331, 26);
            ck_fixquotes.TabIndex = 32;
            ck_fixquotes.Text = "Fix quoted text (useful for QwQ / R1 models)";
            ck_fixquotes.UseVisualStyleBackColor = true;
            // 
            // ck_noquotes
            // 
            ck_noquotes.Dock = DockStyle.Top;
            ck_noquotes.Font = new Font("Segoe UI", 9F);
            ck_noquotes.Location = new Point(12, 237);
            ck_noquotes.Name = "ck_noquotes";
            ck_noquotes.Size = new Size(331, 26);
            ck_noquotes.TabIndex = 31;
            ck_noquotes.Text = "Don't use quotes (quotation marks will be removed)";
            ck_noquotes.UseVisualStyleBackColor = true;
            // 
            // ck_unbold
            // 
            ck_unbold.Dock = DockStyle.Top;
            ck_unbold.Font = new Font("Segoe UI", 9F);
            ck_unbold.Location = new Point(12, 263);
            ck_unbold.Name = "ck_unbold";
            ck_unbold.Size = new Size(331, 26);
            ck_unbold.TabIndex = 30;
            ck_unbold.Text = "Don't bold text (any text in bold turned back to regular)";
            ck_unbold.UseVisualStyleBackColor = true;
            // 
            // label61
            // 
            label61.AutoSize = true;
            label61.Font = new Font("Segoe UI", 9F);
            label61.Location = new Point(11, 37);
            label61.Name = "label61";
            label61.Size = new Size(96, 15);
            label61.TabIndex = 29;
            label61.Text = "Removal Chance";
            // 
            // num_antislopchance
            // 
            num_antislopchance.BackColor = Color.FromArgb(64, 64, 64);
            num_antislopchance.CausesValidation = false;
            num_antislopchance.DecimalPlaces = 2;
            num_antislopchance.Font = new Font("Segoe UI", 9F);
            num_antislopchance.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_antislopchance.Location = new Point(113, 35);
            num_antislopchance.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_antislopchance.Name = "num_antislopchance";
            num_antislopchance.Padding = new Padding(1);
            num_antislopchance.Size = new Size(67, 23);
            num_antislopchance.TabIndex = 28;
            num_antislopchance.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // ed_sloplist
            // 
            ed_sloplist.Location = new Point(11, 6);
            ed_sloplist.Name = "ed_sloplist";
            ed_sloplist.PlaceholderText = "comma separated list of words to filter out";
            ed_sloplist.Size = new Size(313, 23);
            ed_sloplist.TabIndex = 2;
            // 
            // ck_antislop
            // 
            ck_antislop.Dock = DockStyle.Top;
            ck_antislop.Font = new Font("Segoe UI", 9F);
            ck_antislop.Location = new Point(12, 32);
            ck_antislop.Name = "ck_antislop";
            ck_antislop.Size = new Size(331, 26);
            ck_antislop.TabIndex = 1;
            ck_antislop.Text = "Remove words from list (ad-hoc anti slop)";
            ck_antislop.UseVisualStyleBackColor = true;
            // 
            // ck_fixasterix
            // 
            ck_fixasterix.Dock = DockStyle.Top;
            ck_fixasterix.Font = new Font("Segoe UI", 9F);
            ck_fixasterix.Location = new Point(12, 341);
            ck_fixasterix.Name = "ck_fixasterix";
            ck_fixasterix.Size = new Size(331, 26);
            ck_fixasterix.TabIndex = 0;
            ck_fixasterix.Text = "Attempt to fix missing asterisks";
            ck_fixasterix.UseVisualStyleBackColor = true;
            // 
            // label65
            // 
            label65.AutoSize = true;
            label65.Dock = DockStyle.Top;
            label65.Font = new Font("Segoe UI", 9F);
            label65.Location = new Point(12, 94);
            label65.Name = "label65";
            label65.Size = new Size(135, 15);
            label65.TabIndex = 34;
            label65.Text = "Handling of chat history";
            // 
            // cb_pastsession
            // 
            cb_pastsession.BackColor = Color.FromArgb(64, 64, 64);
            cb_pastsession.Dock = DockStyle.Top;
            cb_pastsession.DropDownHeight = 180;
            cb_pastsession.Font = new Font("Segoe UI", 9F);
            cb_pastsession.Items.AddRange(new object[] { "Current session only", "Fit as much as possible, including previous sessions" });
            cb_pastsession.Location = new Point(12, 109);
            cb_pastsession.MaxDropDownItems = 10;
            cb_pastsession.Name = "cb_pastsession";
            cb_pastsession.Padding = new Padding(1);
            cb_pastsession.Size = new Size(290, 23);
            cb_pastsession.TabIndex = 33;
            // 
            // num_memtokens
            // 
            num_memtokens.BackColor = Color.FromArgb(64, 64, 64);
            num_memtokens.Font = new Font("Segoe UI", 9F);
            num_memtokens.Increment = new decimal(new int[] { 256, 0, 0, 0 });
            num_memtokens.Location = new Point(11, 6);
            num_memtokens.Maximum = new decimal(new int[] { 128000, 0, 0, 0 });
            num_memtokens.Minimum = new decimal(new int[] { 256, 0, 0, 0 });
            num_memtokens.Name = "num_memtokens";
            num_memtokens.Padding = new Padding(1);
            num_memtokens.Size = new Size(117, 23);
            num_memtokens.TabIndex = 27;
            num_memtokens.Value = new decimal(new int[] { 2048, 0, 0, 0 });
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Font = new Font("Segoe UI", 9F);
            label32.Location = new Point(134, 8);
            label32.Name = "label32";
            label32.Size = new Size(94, 15);
            label32.TabIndex = 28;
            label32.Text = "Reserved Tokens";
            // 
            // ck_sessionmemory
            // 
            ck_sessionmemory.Dock = DockStyle.Top;
            ck_sessionmemory.Font = new Font("Segoe UI", 9F);
            ck_sessionmemory.Location = new Point(12, 32);
            ck_sessionmemory.Name = "ck_sessionmemory";
            ck_sessionmemory.Size = new Size(290, 26);
            ck_sessionmemory.TabIndex = 24;
            ck_sessionmemory.Text = "Add summaries of past sessions";
            ck_sessionmemory.UseVisualStyleBackColor = true;
            // 
            // ck_forcePW
            // 
            ck_forcePW.Dock = DockStyle.Bottom;
            ck_forcePW.Font = new Font("Segoe UI", 9F);
            ck_forcePW.Location = new Point(12, 229);
            ck_forcePW.Name = "ck_forcePW";
            ck_forcePW.Size = new Size(290, 26);
            ck_forcePW.TabIndex = 38;
            ck_forcePW.Text = "Force password when switching bot";
            ck_forcePW.UseVisualStyleBackColor = true;
            // 
            // ckShowHidden
            // 
            ckShowHidden.Dock = DockStyle.Bottom;
            ckShowHidden.Font = new Font("Segoe UI", 9F);
            ckShowHidden.Location = new Point(12, 255);
            ckShowHidden.Name = "ckShowHidden";
            ckShowHidden.Size = new Size(290, 26);
            ckShowHidden.TabIndex = 31;
            ckShowHidden.Text = "Show hidden system messages";
            ckShowHidden.UseVisualStyleBackColor = true;
            // 
            // num_msgcount
            // 
            num_msgcount.BackColor = Color.FromArgb(64, 64, 64);
            num_msgcount.Font = new Font("Segoe UI", 9F);
            num_msgcount.Increment = new decimal(new int[] { 20, 0, 0, 0 });
            num_msgcount.Location = new Point(97, 139);
            num_msgcount.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            num_msgcount.Minimum = new decimal(new int[] { 20, 0, 0, 0 });
            num_msgcount.Name = "num_msgcount";
            num_msgcount.Padding = new Padding(1);
            num_msgcount.Size = new Size(79, 23);
            num_msgcount.TabIndex = 29;
            num_msgcount.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new Font("Segoe UI", 9F);
            label30.Location = new Point(97, 121);
            label30.Name = "label30";
            label30.Size = new Size(97, 15);
            label30.TabIndex = 30;
            label30.Text = "Shown Messages";
            // 
            // num_fontsize
            // 
            num_fontsize.BackColor = Color.FromArgb(64, 64, 64);
            num_fontsize.Font = new Font("Segoe UI", 9F);
            num_fontsize.Location = new Point(12, 139);
            num_fontsize.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            num_fontsize.Minimum = new decimal(new int[] { 6, 0, 0, 0 });
            num_fontsize.Name = "num_fontsize";
            num_fontsize.Padding = new Padding(1);
            num_fontsize.Size = new Size(79, 23);
            num_fontsize.TabIndex = 27;
            num_fontsize.Value = new decimal(new int[] { 7, 0, 0, 0 });
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("Segoe UI", 9F);
            label29.Location = new Point(12, 121);
            label29.Name = "label29";
            label29.Size = new Size(54, 15);
            label29.TabIndex = 28;
            label29.Text = "Font Size";
            // 
            // cb_background
            // 
            cb_background.BackColor = Color.FromArgb(64, 64, 64);
            cb_background.Dock = DockStyle.Top;
            cb_background.DropDownHeight = 180;
            cb_background.Font = new Font("Segoe UI", 9F);
            cb_background.Location = new Point(12, 47);
            cb_background.MaxDropDownItems = 10;
            cb_background.Name = "cb_background";
            cb_background.Padding = new Padding(1);
            cb_background.Size = new Size(290, 23);
            cb_background.TabIndex = 27;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Dock = DockStyle.Top;
            label28.Font = new Font("Segoe UI", 9F);
            label28.Location = new Point(12, 32);
            label28.Name = "label28";
            label28.Size = new Size(99, 15);
            label28.TabIndex = 26;
            label28.Text = "Chat Background";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Top;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
            label4.Location = new Point(12, 154);
            label4.Name = "label4";
            label4.Size = new Size(81, 15);
            label4.TabIndex = 41;
            label4.Text = "Lethe Plugins";
            // 
            // ck_searchextract
            // 
            ck_searchextract.Dock = DockStyle.Top;
            ck_searchextract.Font = new Font("Segoe UI", 9F);
            ck_searchextract.Location = new Point(12, 118);
            ck_searchextract.Name = "ck_searchextract";
            ck_searchextract.Size = new Size(290, 26);
            ck_searchextract.TabIndex = 40;
            ck_searchextract.Text = "Try to extract page content (jina.ai)";
            ck_searchextract.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Top;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(12, 80);
            label3.Name = "label3";
            label3.Size = new Size(127, 15);
            label3.TabIndex = 39;
            label3.Text = "Brave Search API Key";
            // 
            // ed_searchkey
            // 
            ed_searchkey.Dock = DockStyle.Top;
            ed_searchkey.Location = new Point(12, 95);
            ed_searchkey.Name = "ed_searchkey";
            ed_searchkey.PlaceholderText = "Brave API Key";
            ed_searchkey.Size = new Size(290, 23);
            ed_searchkey.TabIndex = 38;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(12, 32);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 37;
            label2.Text = "Search API";
            // 
            // cb_searchapi
            // 
            cb_searchapi.BackColor = Color.FromArgb(64, 64, 64);
            cb_searchapi.Dock = DockStyle.Top;
            cb_searchapi.DropDownHeight = 180;
            cb_searchapi.Font = new Font("Segoe UI", 9F);
            cb_searchapi.Items.AddRange(new object[] { "DuckDuckGo", "Brave Search" });
            cb_searchapi.Location = new Point(12, 47);
            cb_searchapi.MaxDropDownItems = 10;
            cb_searchapi.Name = "cb_searchapi";
            cb_searchapi.Padding = new Padding(1);
            cb_searchapi.Size = new Size(290, 23);
            cb_searchapi.TabIndex = 36;
            // 
            // ck_webgrammar
            // 
            ck_webgrammar.Dock = DockStyle.Top;
            ck_webgrammar.Font = new Font("Segoe UI", 9F);
            ck_webgrammar.Location = new Point(12, 169);
            ck_webgrammar.Name = "ck_webgrammar";
            ck_webgrammar.Size = new Size(290, 26);
            ck_webgrammar.TabIndex = 33;
            ck_webgrammar.Text = "Browser - Enforce structured output";
            ck_webgrammar.UseVisualStyleBackColor = true;
            // 
            // ck_alwayswebsearch
            // 
            ck_alwayswebsearch.Dock = DockStyle.Top;
            ck_alwayswebsearch.Font = new Font("Segoe UI", 9F);
            ck_alwayswebsearch.Location = new Point(12, 221);
            ck_alwayswebsearch.Name = "ck_alwayswebsearch";
            ck_alwayswebsearch.Size = new Size(290, 26);
            ck_alwayswebsearch.TabIndex = 35;
            ck_alwayswebsearch.Text = "Live Search - Always attempt search (slow)";
            ck_alwayswebsearch.UseVisualStyleBackColor = true;
            // 
            // ck_webkeyword
            // 
            ck_webkeyword.Dock = DockStyle.Top;
            ck_webkeyword.Font = new Font("Segoe UI", 9F);
            ck_webkeyword.Location = new Point(12, 195);
            ck_webkeyword.Name = "ck_webkeyword";
            ck_webkeyword.Size = new Size(290, 26);
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
            bt_chattosessions.Location = new Point(15, 64);
            bt_chattosessions.Name = "bt_chattosessions";
            bt_chattosessions.Size = new Size(281, 23);
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
            bt_importworld.Location = new Point(163, 35);
            bt_importworld.Name = "bt_importworld";
            bt_importworld.Size = new Size(133, 23);
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
            bt_ImportSTChat.Size = new Size(142, 23);
            bt_ImportSTChat.TabIndex = 1;
            bt_ImportSTChat.Tag = "no-theme";
            bt_ImportSTChat.Text = "Import ST Chat";
            bt_ImportSTChat.UseVisualStyleBackColor = false;
            bt_ImportSTChat.Click += bt_ImportSTChat_Click;
            // 
            // ckThirdPerson
            // 
            ckThirdPerson.Dock = DockStyle.Bottom;
            ckThirdPerson.Font = new Font("Segoe UI", 9F);
            ckThirdPerson.Location = new Point(12, 171);
            ckThirdPerson.Name = "ckThirdPerson";
            ckThirdPerson.Size = new Size(290, 26);
            ckThirdPerson.TabIndex = 38;
            ckThirdPerson.Text = "Convert queries to 3rd person";
            ckThirdPerson.UseVisualStyleBackColor = true;
            // 
            // num_ragM
            // 
            num_ragM.BackColor = Color.FromArgb(64, 64, 64);
            num_ragM.Font = new Font("Segoe UI", 9F);
            num_ragM.Location = new Point(12, 147);
            num_ragM.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            num_ragM.Name = "num_ragM";
            num_ragM.Padding = new Padding(1);
            num_ragM.Size = new Size(147, 23);
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
            num_ragindex.BackColor = Color.FromArgb(64, 64, 64);
            num_ragindex.Font = new Font("Segoe UI", 9F);
            num_ragindex.Location = new Point(169, 59);
            num_ragindex.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_ragindex.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            num_ragindex.Name = "num_ragindex";
            num_ragindex.Padding = new Padding(1);
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
            label14.Size = new Size(109, 15);
            label14.TabIndex = 26;
            label14.Text = "Max Session Entries";
            // 
            // num_ragmaxretrieve
            // 
            num_ragmaxretrieve.BackColor = Color.FromArgb(64, 64, 64);
            num_ragmaxretrieve.Font = new Font("Segoe UI", 9F);
            num_ragmaxretrieve.Location = new Point(169, 103);
            num_ragmaxretrieve.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_ragmaxretrieve.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_ragmaxretrieve.Name = "num_ragmaxretrieve";
            num_ragmaxretrieve.Padding = new Padding(1);
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
            num_ragcutoff.BackColor = Color.FromArgb(64, 64, 64);
            num_ragcutoff.DecimalPlaces = 3;
            num_ragcutoff.Font = new Font("Segoe UI", 9F);
            num_ragcutoff.Increment = new decimal(new int[] { 5, 0, 0, 196608 });
            num_ragcutoff.Location = new Point(12, 103);
            num_ragcutoff.Maximum = new decimal(new int[] { 5, 0, 0, 65536 });
            num_ragcutoff.Minimum = new decimal(new int[] { 5, 0, 0, 196608 });
            num_ragcutoff.Name = "num_ragcutoff";
            num_ragcutoff.Padding = new Padding(1);
            num_ragcutoff.Size = new Size(147, 23);
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
            cb_ragheuristic.BackColor = Color.FromArgb(64, 64, 64);
            cb_ragheuristic.DropDownHeight = 180;
            cb_ragheuristic.Font = new Font("Segoe UI", 9F);
            cb_ragheuristic.Items.AddRange(new object[] { "Simple", "Heuristic", "Exact" });
            cb_ragheuristic.Location = new Point(12, 59);
            cb_ragheuristic.MaxDropDownItems = 10;
            cb_ragheuristic.Name = "cb_ragheuristic";
            cb_ragheuristic.Padding = new Padding(1);
            cb_ragheuristic.Size = new Size(147, 23);
            cb_ragheuristic.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(bt_Close);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 635);
            panel1.Name = "panel1";
            panel1.Size = new Size(1330, 38);
            panel1.TabIndex = 31;
            // 
            // bt_Close
            // 
            bt_Close.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bt_Close.BackColor = Color.DarkSeaGreen;
            bt_Close.FlatStyle = FlatStyle.Flat;
            bt_Close.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_Close.ForeColor = Color.Black;
            bt_Close.Location = new Point(12, 6);
            bt_Close.Name = "bt_Close";
            bt_Close.Size = new Size(1308, 23);
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
            collapsibleGroupBox1.Controls.Add(ckForceInternalGram);
            collapsibleGroupBox1.Controls.Add(ckNoPastInserts);
            collapsibleGroupBox1.Controls.Add(ck_hallusafe);
            collapsibleGroupBox1.Controls.Add(ck_sysrag);
            collapsibleGroupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox1.Location = new Point(6, 6);
            collapsibleGroupBox1.Name = "collapsibleGroupBox1";
            collapsibleGroupBox1.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox1.Size = new Size(314, 145);
            collapsibleGroupBox1.TabIndex = 33;
            collapsibleGroupBox1.Text = "Core Settings";
            // 
            // ckForceInternalGram
            // 
            ckForceInternalGram.Dock = DockStyle.Top;
            ckForceInternalGram.Font = new Font("Segoe UI", 9F);
            ckForceInternalGram.Location = new Point(12, 110);
            ckForceInternalGram.Name = "ckForceInternalGram";
            ckForceInternalGram.Size = new Size(290, 26);
            ckForceInternalGram.TabIndex = 39;
            ckForceInternalGram.Text = "Use internal library's Grammar Builder";
            ckForceInternalGram.UseVisualStyleBackColor = true;
            // 
            // ckNoPastInserts
            // 
            ckNoPastInserts.Dock = DockStyle.Top;
            ckNoPastInserts.Font = new Font("Segoe UI", 9F);
            ckNoPastInserts.Location = new Point(12, 84);
            ckNoPastInserts.Name = "ckNoPastInserts";
            ckNoPastInserts.Size = new Size(290, 26);
            ckNoPastInserts.TabIndex = 38;
            ckNoPastInserts.Text = "Disable hidden inserts in past sessions";
            ckNoPastInserts.UseVisualStyleBackColor = true;
            // 
            // ck_hallusafe
            // 
            ck_hallusafe.Dock = DockStyle.Top;
            ck_hallusafe.Font = new Font("Segoe UI", 9F);
            ck_hallusafe.Location = new Point(12, 58);
            ck_hallusafe.Name = "ck_hallusafe";
            ck_hallusafe.Size = new Size(290, 26);
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
            ck_sysrag.Size = new Size(290, 26);
            ck_sysrag.TabIndex = 36;
            ck_sysrag.Text = "Move all memory inserts to system prompt";
            ck_sysrag.UseVisualStyleBackColor = true;
            // 
            // verticalStackPanel1
            // 
            verticalStackPanel1.Controls.Add(collapsibleGroupBox5);
            verticalStackPanel1.Controls.Add(collapsibleGroupBox8);
            verticalStackPanel1.Controls.Add(collapsibleGroupBox1);
            verticalStackPanel1.Dock = DockStyle.Left;
            verticalStackPanel1.Location = new Point(0, 0);
            verticalStackPanel1.Name = "verticalStackPanel1";
            verticalStackPanel1.Padding = new Padding(6, 6, 0, 6);
            verticalStackPanel1.Size = new Size(320, 635);
            verticalStackPanel1.TabIndex = 34;
            // 
            // collapsibleGroupBox5
            // 
            collapsibleGroupBox5.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox5.CanCollapse = false;
            collapsibleGroupBox5.Controls.Add(ck_alwayswebsearch);
            collapsibleGroupBox5.Controls.Add(ck_webkeyword);
            collapsibleGroupBox5.Controls.Add(ck_webgrammar);
            collapsibleGroupBox5.Controls.Add(label4);
            collapsibleGroupBox5.Controls.Add(panel2);
            collapsibleGroupBox5.Controls.Add(ck_searchextract);
            collapsibleGroupBox5.Controls.Add(ed_searchkey);
            collapsibleGroupBox5.Controls.Add(label3);
            collapsibleGroupBox5.Controls.Add(panel7);
            collapsibleGroupBox5.Controls.Add(cb_searchapi);
            collapsibleGroupBox5.Controls.Add(label2);
            collapsibleGroupBox5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox5.Location = new Point(6, 359);
            collapsibleGroupBox5.Name = "collapsibleGroupBox5";
            collapsibleGroupBox5.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox5.Size = new Size(314, 273);
            collapsibleGroupBox5.TabIndex = 0;
            collapsibleGroupBox5.Text = "Web Search";
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(12, 144);
            panel2.Margin = new Padding(8);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(8);
            panel2.Size = new Size(290, 10);
            panel2.TabIndex = 52;
            // 
            // panel7
            // 
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point(12, 70);
            panel7.Margin = new Padding(8);
            panel7.Name = "panel7";
            panel7.Padding = new Padding(8);
            panel7.Size = new Size(290, 10);
            panel7.TabIndex = 51;
            // 
            // collapsibleGroupBox8
            // 
            collapsibleGroupBox8.BackColor = Color.FromArgb(37, 37, 37);
            collapsibleGroupBox8.CanCollapse = false;
            collapsibleGroupBox8.Controls.Add(ckGroupCommit);
            collapsibleGroupBox8.Controls.Add(ckGroupAltern);
            collapsibleGroupBox8.Controls.Add(cbGroupSessionStrategy);
            collapsibleGroupBox8.Controls.Add(label9);
            collapsibleGroupBox8.Controls.Add(panel6);
            collapsibleGroupBox8.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox8.Location = new Point(6, 159);
            collapsibleGroupBox8.Name = "collapsibleGroupBox8";
            collapsibleGroupBox8.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox8.Size = new Size(314, 192);
            collapsibleGroupBox8.TabIndex = 1;
            collapsibleGroupBox8.Text = "Group Chat";
            // 
            // ckGroupCommit
            // 
            ckGroupCommit.Dock = DockStyle.Top;
            ckGroupCommit.Font = new Font("Segoe UI", 9F);
            ckGroupCommit.Location = new Point(12, 150);
            ckGroupCommit.Name = "ckGroupCommit";
            ckGroupCommit.Size = new Size(290, 26);
            ckGroupCommit.TabIndex = 62;
            ckGroupCommit.Text = "Save Sessions to secondary characters too";
            ckGroupCommit.UseVisualStyleBackColor = true;
            // 
            // ckGroupAltern
            // 
            ckGroupAltern.Dock = DockStyle.Top;
            ckGroupAltern.Font = new Font("Segoe UI", 9F);
            ckGroupAltern.Location = new Point(12, 124);
            ckGroupAltern.Name = "ckGroupAltern";
            ckGroupAltern.Size = new Size(290, 26);
            ckGroupAltern.TabIndex = 61;
            ckGroupAltern.Text = "Forced message role alternance";
            ckGroupAltern.UseVisualStyleBackColor = true;
            // 
            // cbGroupSessionStrategy
            // 
            cbGroupSessionStrategy.BackColor = Color.FromArgb(64, 64, 64);
            cbGroupSessionStrategy.Dock = DockStyle.Top;
            cbGroupSessionStrategy.DropDownHeight = 180;
            cbGroupSessionStrategy.Font = new Font("Segoe UI", 9F);
            cbGroupSessionStrategy.Location = new Point(12, 101);
            cbGroupSessionStrategy.MaxDropDownItems = 10;
            cbGroupSessionStrategy.Name = "cbGroupSessionStrategy";
            cbGroupSessionStrategy.Padding = new Padding(1);
            cbGroupSessionStrategy.Size = new Size(290, 23);
            cbGroupSessionStrategy.TabIndex = 59;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Dock = DockStyle.Top;
            label9.Font = new Font("Segoe UI", 9F);
            label9.Location = new Point(12, 86);
            label9.Name = "label9";
            label9.Size = new Size(117, 15);
            label9.TabIndex = 60;
            label9.Text = "Past Session Strategy";
            // 
            // panel6
            // 
            panel6.Controls.Add(numGroupQueue);
            panel6.Controls.Add(label8);
            panel6.Controls.Add(ckGroupRouting);
            panel6.Controls.Add(label7);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(12, 32);
            panel6.Margin = new Padding(8);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(8);
            panel6.Size = new Size(290, 54);
            panel6.TabIndex = 55;
            // 
            // numGroupQueue
            // 
            numGroupQueue.BackColor = Color.FromArgb(64, 64, 64);
            numGroupQueue.Font = new Font("Segoe UI", 9F);
            numGroupQueue.Location = new Point(225, 24);
            numGroupQueue.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numGroupQueue.Name = "numGroupQueue";
            numGroupQueue.Padding = new Padding(1);
            numGroupQueue.Size = new Size(65, 23);
            numGroupQueue.TabIndex = 58;
            numGroupQueue.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F);
            label8.Location = new Point(199, 5);
            label8.Name = "label8";
            label8.Size = new Size(88, 15);
            label8.TabIndex = 59;
            label8.Text = "Max Bot Queue";
            // 
            // ckGroupRouting
            // 
            ckGroupRouting.BackColor = Color.FromArgb(64, 64, 64);
            ckGroupRouting.DropDownHeight = 180;
            ckGroupRouting.Font = new Font("Segoe UI", 9F);
            ckGroupRouting.Location = new Point(0, 24);
            ckGroupRouting.MaxDropDownItems = 10;
            ckGroupRouting.Name = "ckGroupRouting";
            ckGroupRouting.Padding = new Padding(1);
            ckGroupRouting.Size = new Size(219, 23);
            ckGroupRouting.TabIndex = 43;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F);
            label7.Location = new Point(3, 5);
            label7.Name = "label7";
            label7.Size = new Size(95, 15);
            label7.TabIndex = 44;
            label7.Text = "Routing Strategy";
            // 
            // collapsibleGroupBox3
            // 
            collapsibleGroupBox3.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox3.CanCollapse = false;
            collapsibleGroupBox3.Controls.Add(numWIEntries);
            collapsibleGroupBox3.Controls.Add(label6);
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
            collapsibleGroupBox3.Location = new Point(6, 207);
            collapsibleGroupBox3.Name = "collapsibleGroupBox3";
            collapsibleGroupBox3.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox3.Size = new Size(314, 207);
            collapsibleGroupBox3.TabIndex = 35;
            collapsibleGroupBox3.Text = "Retrieval Augmented Generation";
            // 
            // numWIEntries
            // 
            numWIEntries.BackColor = Color.FromArgb(64, 64, 64);
            numWIEntries.Font = new Font("Segoe UI", 9F);
            numWIEntries.Location = new Point(169, 147);
            numWIEntries.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numWIEntries.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numWIEntries.Name = "numWIEntries";
            numWIEntries.Padding = new Padding(1);
            numWIEntries.Size = new Size(137, 23);
            numWIEntries.TabIndex = 39;
            numWIEntries.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F);
            label6.Location = new Point(169, 129);
            label6.Name = "label6";
            label6.Size = new Size(123, 15);
            label6.TabIndex = 40;
            label6.Text = "Max WorldInfo Entries";
            // 
            // collapsibleGroupBox2
            // 
            collapsibleGroupBox2.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox2.CanCollapse = false;
            collapsibleGroupBox2.Controls.Add(ckDetailedSum);
            collapsibleGroupBox2.Controls.Add(mck_cutmiddle);
            collapsibleGroupBox2.Controls.Add(cb_pastsession);
            collapsibleGroupBox2.Controls.Add(label65);
            collapsibleGroupBox2.Controls.Add(panel8);
            collapsibleGroupBox2.Controls.Add(ck_sessionmemory);
            collapsibleGroupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox2.Location = new Point(6, 6);
            collapsibleGroupBox2.Name = "collapsibleGroupBox2";
            collapsibleGroupBox2.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox2.Size = new Size(314, 193);
            collapsibleGroupBox2.TabIndex = 34;
            collapsibleGroupBox2.Text = "Session Memory System";
            // 
            // ckDetailedSum
            // 
            ckDetailedSum.Dock = DockStyle.Top;
            ckDetailedSum.Font = new Font("Segoe UI", 9F);
            ckDetailedSum.Location = new Point(12, 158);
            ckDetailedSum.Name = "ckDetailedSum";
            ckDetailedSum.Size = new Size(290, 26);
            ckDetailedSum.TabIndex = 57;
            ckDetailedSum.Text = "Use Detailed Summaries";
            ckDetailedSum.UseVisualStyleBackColor = true;
            // 
            // mck_cutmiddle
            // 
            mck_cutmiddle.Dock = DockStyle.Top;
            mck_cutmiddle.Font = new Font("Segoe UI", 9F);
            mck_cutmiddle.Location = new Point(12, 132);
            mck_cutmiddle.Name = "mck_cutmiddle";
            mck_cutmiddle.Size = new Size(290, 26);
            mck_cutmiddle.TabIndex = 35;
            mck_cutmiddle.Text = "Middle-cut text overflow during summarization";
            mck_cutmiddle.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            panel8.Controls.Add(num_memtokens);
            panel8.Controls.Add(label32);
            panel8.Dock = DockStyle.Top;
            panel8.Location = new Point(12, 58);
            panel8.Margin = new Padding(8);
            panel8.Name = "panel8";
            panel8.Padding = new Padding(8);
            panel8.Size = new Size(290, 36);
            panel8.TabIndex = 56;
            // 
            // collapsibleGroupBox7
            // 
            collapsibleGroupBox7.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox7.CanCollapse = false;
            collapsibleGroupBox7.Controls.Add(bt_chattosessions);
            collapsibleGroupBox7.Controls.Add(bt_ImportSTChat);
            collapsibleGroupBox7.Controls.Add(bt_importworld);
            collapsibleGroupBox7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox7.Location = new Point(6, 536);
            collapsibleGroupBox7.Name = "collapsibleGroupBox7";
            collapsibleGroupBox7.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox7.Size = new Size(314, 96);
            collapsibleGroupBox7.TabIndex = 1;
            collapsibleGroupBox7.Text = "Import Files";
            // 
            // verticalStackPanel2
            // 
            verticalStackPanel2.Controls.Add(collapsibleGroupBox10);
            verticalStackPanel2.Controls.Add(collapsibleGroupBox4);
            verticalStackPanel2.Dock = DockStyle.Left;
            verticalStackPanel2.Location = new Point(640, 0);
            verticalStackPanel2.Name = "verticalStackPanel2";
            verticalStackPanel2.Padding = new Padding(6, 6, 0, 6);
            verticalStackPanel2.Size = new Size(361, 635);
            verticalStackPanel2.TabIndex = 35;
            // 
            // collapsibleGroupBox10
            // 
            collapsibleGroupBox10.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox10.CanCollapse = false;
            collapsibleGroupBox10.Controls.Add(panel14);
            collapsibleGroupBox10.Controls.Add(panel9);
            collapsibleGroupBox10.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox10.Location = new Point(6, 485);
            collapsibleGroupBox10.Name = "collapsibleGroupBox10";
            collapsibleGroupBox10.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox10.Size = new Size(355, 147);
            collapsibleGroupBox10.TabIndex = 35;
            collapsibleGroupBox10.Text = "Image Processing";
            // 
            // panel14
            // 
            panel14.Controls.Add(num_ImgCount);
            panel14.Controls.Add(label18);
            panel14.Dock = DockStyle.Top;
            panel14.Location = new Point(12, 68);
            panel14.Margin = new Padding(8);
            panel14.Name = "panel14";
            panel14.Padding = new Padding(8);
            panel14.Size = new Size(331, 36);
            panel14.TabIndex = 57;
            // 
            // num_ImgCount
            // 
            num_ImgCount.BackColor = Color.FromArgb(64, 64, 64);
            num_ImgCount.Font = new Font("Segoe UI", 9F);
            num_ImgCount.Location = new Point(11, 6);
            num_ImgCount.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            num_ImgCount.Name = "num_ImgCount";
            num_ImgCount.Padding = new Padding(1);
            num_ImgCount.Size = new Size(117, 23);
            num_ImgCount.TabIndex = 27;
            num_ImgCount.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 9F);
            label18.Location = new Point(134, 8);
            label18.Name = "label18";
            label18.Size = new Size(179, 15);
            label18.TabIndex = 28;
            label18.Text = "Max Images in prompt (0 = max)";
            // 
            // panel9
            // 
            panel9.Controls.Add(num_imgEmbed);
            panel9.Controls.Add(label19);
            panel9.Dock = DockStyle.Top;
            panel9.Location = new Point(12, 32);
            panel9.Margin = new Padding(8);
            panel9.Name = "panel9";
            panel9.Padding = new Padding(8);
            panel9.Size = new Size(331, 36);
            panel9.TabIndex = 56;
            // 
            // num_imgEmbed
            // 
            num_imgEmbed.BackColor = Color.FromArgb(64, 64, 64);
            num_imgEmbed.Font = new Font("Segoe UI", 9F);
            num_imgEmbed.Increment = new decimal(new int[] { 64, 0, 0, 0 });
            num_imgEmbed.Location = new Point(11, 6);
            num_imgEmbed.Maximum = new decimal(new int[] { 16384, 0, 0, 0 });
            num_imgEmbed.Minimum = new decimal(new int[] { 256, 0, 0, 0 });
            num_imgEmbed.Name = "num_imgEmbed";
            num_imgEmbed.Padding = new Padding(1);
            num_imgEmbed.Size = new Size(117, 23);
            num_imgEmbed.TabIndex = 27;
            num_imgEmbed.Value = new decimal(new int[] { 768, 0, 0, 0 });
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 9F);
            label19.Location = new Point(134, 8);
            label19.Name = "label19";
            label19.Size = new Size(127, 15);
            label19.TabIndex = 28;
            label19.Text = "Image Embedding Size";
            // 
            // collapsibleGroupBox4
            // 
            collapsibleGroupBox4.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox4.CanCollapse = false;
            collapsibleGroupBox4.Controls.Add(ckParenthesizeToItalic);
            collapsibleGroupBox4.Controls.Add(ck_lastparaphfilter);
            collapsibleGroupBox4.Controls.Add(ckDelStartSlop);
            collapsibleGroupBox4.Controls.Add(ck_fixasterix);
            collapsibleGroupBox4.Controls.Add(ck_oneparagraph);
            collapsibleGroupBox4.Controls.Add(ck_remlastsentence);
            collapsibleGroupBox4.Controls.Add(ck_unbold);
            collapsibleGroupBox4.Controls.Add(ck_noquotes);
            collapsibleGroupBox4.Controls.Add(ck_fixquotes);
            collapsibleGroupBox4.Controls.Add(ck_noemphasisword);
            collapsibleGroupBox4.Controls.Add(panel5);
            collapsibleGroupBox4.Controls.Add(ck_reduceitalic);
            collapsibleGroupBox4.Controls.Add(panel3);
            collapsibleGroupBox4.Controls.Add(ck_antislop);
            collapsibleGroupBox4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox4.Location = new Point(6, 6);
            collapsibleGroupBox4.Name = "collapsibleGroupBox4";
            collapsibleGroupBox4.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox4.Size = new Size(355, 471);
            collapsibleGroupBox4.TabIndex = 0;
            collapsibleGroupBox4.Text = "Output Formatting";
            // 
            // ckParenthesizeToItalic
            // 
            ckParenthesizeToItalic.Dock = DockStyle.Top;
            ckParenthesizeToItalic.Font = new Font("Segoe UI", 9F);
            ckParenthesizeToItalic.Location = new Point(12, 419);
            ckParenthesizeToItalic.Name = "ckParenthesizeToItalic";
            ckParenthesizeToItalic.Size = new Size(331, 26);
            ckParenthesizeToItalic.TabIndex = 58;
            ckParenthesizeToItalic.Text = "Turn parenthesizes to italic text";
            ckParenthesizeToItalic.UseVisualStyleBackColor = true;
            // 
            // ckDelStartSlop
            // 
            ckDelStartSlop.Dock = DockStyle.Top;
            ckDelStartSlop.Font = new Font("Segoe UI", 9F);
            ckDelStartSlop.Location = new Point(12, 367);
            ckDelStartSlop.Name = "ckDelStartSlop";
            ckDelStartSlop.Size = new Size(331, 26);
            ckDelStartSlop.TabIndex = 57;
            ckDelStartSlop.Text = "Delete starting slop";
            ckDelStartSlop.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            panel5.Controls.Add(label62);
            panel5.Controls.Add(num_italicratio);
            panel5.Controls.Add(label63);
            panel5.Controls.Add(num_removeitalicmaxword);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(12, 145);
            panel5.Margin = new Padding(8);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(8);
            panel5.Size = new Size(331, 40);
            panel5.TabIndex = 56;
            // 
            // panel3
            // 
            panel3.Controls.Add(ed_sloplist);
            panel3.Controls.Add(label61);
            panel3.Controls.Add(num_antislopchance);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(12, 58);
            panel3.Margin = new Padding(8);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(8);
            panel3.Size = new Size(331, 61);
            panel3.TabIndex = 55;
            // 
            // collapsibleGroupBox6
            // 
            collapsibleGroupBox6.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox6.CanCollapse = false;
            collapsibleGroupBox6.Controls.Add(mcbSkin);
            collapsibleGroupBox6.Controls.Add(ck_forcePW);
            collapsibleGroupBox6.Controls.Add(ckShowHidden);
            collapsibleGroupBox6.Controls.Add(num_msgcount);
            collapsibleGroupBox6.Controls.Add(label29);
            collapsibleGroupBox6.Controls.Add(label30);
            collapsibleGroupBox6.Controls.Add(num_fontsize);
            collapsibleGroupBox6.Controls.Add(label5);
            collapsibleGroupBox6.Controls.Add(panel4);
            collapsibleGroupBox6.Controls.Add(cb_background);
            collapsibleGroupBox6.Controls.Add(label28);
            collapsibleGroupBox6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox6.Location = new Point(6, 237);
            collapsibleGroupBox6.Name = "collapsibleGroupBox6";
            collapsibleGroupBox6.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox6.Size = new Size(314, 291);
            collapsibleGroupBox6.TabIndex = 1;
            collapsibleGroupBox6.Text = "User Interface";
            // 
            // mcbSkin
            // 
            mcbSkin.BackColor = Color.FromArgb(64, 64, 64);
            mcbSkin.Dock = DockStyle.Top;
            mcbSkin.DropDownHeight = 180;
            mcbSkin.Font = new Font("Segoe UI", 9F);
            mcbSkin.Items.AddRange(new object[] { "Light", "Dark" });
            mcbSkin.Location = new Point(12, 95);
            mcbSkin.MaxDropDownItems = 10;
            mcbSkin.Name = "mcbSkin";
            mcbSkin.Padding = new Padding(1);
            mcbSkin.Size = new Size(290, 23);
            mcbSkin.TabIndex = 40;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Top;
            label5.Font = new Font("Segoe UI", 9F);
            label5.Location = new Point(12, 80);
            label5.Name = "label5";
            label5.Size = new Size(43, 15);
            label5.TabIndex = 39;
            label5.Text = "UI Skin";
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(12, 70);
            panel4.Margin = new Padding(8);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(8);
            panel4.Size = new Size(290, 10);
            panel4.TabIndex = 54;
            // 
            // verticalStackPanel3
            // 
            verticalStackPanel3.Controls.Add(collapsibleGroupBox9);
            verticalStackPanel3.Controls.Add(collapsibleGroupBox3);
            verticalStackPanel3.Controls.Add(collapsibleGroupBox2);
            verticalStackPanel3.Dock = DockStyle.Left;
            verticalStackPanel3.Location = new Point(320, 0);
            verticalStackPanel3.Name = "verticalStackPanel3";
            verticalStackPanel3.Padding = new Padding(6, 6, 0, 6);
            verticalStackPanel3.Size = new Size(320, 635);
            verticalStackPanel3.TabIndex = 36;
            // 
            // collapsibleGroupBox9
            // 
            collapsibleGroupBox9.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox9.CanCollapse = false;
            collapsibleGroupBox9.Controls.Add(panel13);
            collapsibleGroupBox9.Controls.Add(panel12);
            collapsibleGroupBox9.Controls.Add(panel11);
            collapsibleGroupBox9.Controls.Add(panel10);
            collapsibleGroupBox9.Controls.Add(ckFactRetrieval);
            collapsibleGroupBox9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox9.Location = new Point(6, 422);
            collapsibleGroupBox9.Name = "collapsibleGroupBox9";
            collapsibleGroupBox9.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox9.Size = new Size(314, 210);
            collapsibleGroupBox9.TabIndex = 35;
            collapsibleGroupBox9.Text = "Fact-Based Retrieval";
            // 
            // panel13
            // 
            panel13.Controls.Add(numFactSuper);
            panel13.Controls.Add(label17);
            panel13.Dock = DockStyle.Top;
            panel13.Location = new Point(12, 164);
            panel13.Margin = new Padding(8);
            panel13.Name = "panel13";
            panel13.Padding = new Padding(8);
            panel13.Size = new Size(290, 35);
            panel13.TabIndex = 61;
            // 
            // numFactSuper
            // 
            numFactSuper.AccessibleDescription = "";
            numFactSuper.BackColor = Color.FromArgb(64, 64, 64);
            numFactSuper.DecimalPlaces = 3;
            numFactSuper.Font = new Font("Segoe UI", 9F);
            numFactSuper.Increment = new decimal(new int[] { 5, 0, 0, 196608 });
            numFactSuper.Location = new Point(11, 6);
            numFactSuper.Maximum = new decimal(new int[] { 5, 0, 0, 65536 });
            numFactSuper.Minimum = new decimal(new int[] { 5, 0, 0, 196608 });
            numFactSuper.Name = "numFactSuper";
            numFactSuper.Padding = new Padding(1);
            numFactSuper.Size = new Size(117, 23);
            numFactSuper.TabIndex = 59;
            numFactSuper.Value = new decimal(new int[] { 2, 0, 0, 65536 });
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 9F);
            label17.Location = new Point(134, 8);
            label17.Name = "label17";
            label17.Size = new Size(131, 15);
            label17.TabIndex = 60;
            label17.Text = "Supersession Threshold";
            // 
            // panel12
            // 
            panel12.Controls.Add(numFactRetrieval);
            panel12.Controls.Add(label16);
            panel12.Dock = DockStyle.Top;
            panel12.Location = new Point(12, 129);
            panel12.Margin = new Padding(8);
            panel12.Name = "panel12";
            panel12.Padding = new Padding(8);
            panel12.Size = new Size(290, 35);
            panel12.TabIndex = 60;
            // 
            // numFactRetrieval
            // 
            numFactRetrieval.AccessibleDescription = "";
            numFactRetrieval.BackColor = Color.FromArgb(64, 64, 64);
            numFactRetrieval.DecimalPlaces = 3;
            numFactRetrieval.Font = new Font("Segoe UI", 9F);
            numFactRetrieval.Increment = new decimal(new int[] { 5, 0, 0, 196608 });
            numFactRetrieval.Location = new Point(11, 6);
            numFactRetrieval.Maximum = new decimal(new int[] { 5, 0, 0, 65536 });
            numFactRetrieval.Minimum = new decimal(new int[] { 5, 0, 0, 196608 });
            numFactRetrieval.Name = "numFactRetrieval";
            numFactRetrieval.Padding = new Padding(1);
            numFactRetrieval.Size = new Size(117, 23);
            numFactRetrieval.TabIndex = 59;
            numFactRetrieval.Value = new decimal(new int[] { 2, 0, 0, 65536 });
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9F);
            label16.Location = new Point(134, 8);
            label16.Name = "label16";
            label16.Size = new Size(108, 15);
            label16.TabIndex = 60;
            label16.Text = "Retrieval Threshold";
            // 
            // panel11
            // 
            panel11.Controls.Add(numFactDedup);
            panel11.Controls.Add(label10);
            panel11.Dock = DockStyle.Top;
            panel11.Location = new Point(12, 94);
            panel11.Margin = new Padding(8);
            panel11.Name = "panel11";
            panel11.Padding = new Padding(8);
            panel11.Size = new Size(290, 35);
            panel11.TabIndex = 59;
            // 
            // numFactDedup
            // 
            numFactDedup.AccessibleDescription = "";
            numFactDedup.BackColor = Color.FromArgb(64, 64, 64);
            numFactDedup.DecimalPlaces = 3;
            numFactDedup.Font = new Font("Segoe UI", 9F);
            numFactDedup.Increment = new decimal(new int[] { 5, 0, 0, 196608 });
            numFactDedup.Location = new Point(11, 6);
            numFactDedup.Maximum = new decimal(new int[] { 5, 0, 0, 65536 });
            numFactDedup.Minimum = new decimal(new int[] { 5, 0, 0, 196608 });
            numFactDedup.Name = "numFactDedup";
            numFactDedup.Padding = new Padding(1);
            numFactDedup.Size = new Size(117, 23);
            numFactDedup.TabIndex = 59;
            numFactDedup.Value = new decimal(new int[] { 2, 0, 0, 65536 });
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F);
            label10.Location = new Point(134, 8);
            label10.Name = "label10";
            label10.Size = new Size(137, 15);
            label10.TabIndex = 60;
            label10.Text = "Deduplication Threshold";
            // 
            // panel10
            // 
            panel10.Controls.Add(numFactTokens);
            panel10.Controls.Add(label11);
            panel10.Dock = DockStyle.Top;
            panel10.Location = new Point(12, 58);
            panel10.Margin = new Padding(8);
            panel10.Name = "panel10";
            panel10.Padding = new Padding(8);
            panel10.Size = new Size(290, 36);
            panel10.TabIndex = 56;
            // 
            // numFactTokens
            // 
            numFactTokens.BackColor = Color.FromArgb(64, 64, 64);
            numFactTokens.Font = new Font("Segoe UI", 9F);
            numFactTokens.Increment = new decimal(new int[] { 256, 0, 0, 0 });
            numFactTokens.Location = new Point(11, 6);
            numFactTokens.Maximum = new decimal(new int[] { 128000, 0, 0, 0 });
            numFactTokens.Minimum = new decimal(new int[] { 256, 0, 0, 0 });
            numFactTokens.Name = "numFactTokens";
            numFactTokens.Padding = new Padding(1);
            numFactTokens.Size = new Size(117, 23);
            numFactTokens.TabIndex = 27;
            numFactTokens.Value = new decimal(new int[] { 2048, 0, 0, 0 });
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F);
            label11.Location = new Point(134, 8);
            label11.Name = "label11";
            label11.Size = new Size(126, 15);
            label11.TabIndex = 28;
            label11.Text = "Fact Tokens (0 disable)";
            // 
            // ckFactRetrieval
            // 
            ckFactRetrieval.Dock = DockStyle.Top;
            ckFactRetrieval.Font = new Font("Segoe UI", 9F);
            ckFactRetrieval.Location = new Point(12, 32);
            ckFactRetrieval.Name = "ckFactRetrieval";
            ckFactRetrieval.Size = new Size(290, 26);
            ckFactRetrieval.TabIndex = 24;
            ckFactRetrieval.Text = "Fact Retrieval Enabled";
            ckFactRetrieval.UseVisualStyleBackColor = true;
            // 
            // verticalStackPanel4
            // 
            verticalStackPanel4.Controls.Add(collapsibleGroupBox7);
            verticalStackPanel4.Controls.Add(collapsibleGroupBox6);
            verticalStackPanel4.Controls.Add(collapsibleGroupBox11);
            verticalStackPanel4.Dock = DockStyle.Left;
            verticalStackPanel4.Location = new Point(1001, 0);
            verticalStackPanel4.Name = "verticalStackPanel4";
            verticalStackPanel4.Padding = new Padding(6, 6, 0, 6);
            verticalStackPanel4.Size = new Size(320, 635);
            verticalStackPanel4.TabIndex = 37;
            // 
            // collapsibleGroupBox11
            // 
            collapsibleGroupBox11.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox11.CanCollapse = false;
            collapsibleGroupBox11.Controls.Add(ckLlamaCppSamplers);
            collapsibleGroupBox11.Controls.Add(panel15);
            collapsibleGroupBox11.Controls.Add(cbChatThinkPolicy);
            collapsibleGroupBox11.Controls.Add(label20);
            collapsibleGroupBox11.Controls.Add(panel16);
            collapsibleGroupBox11.Controls.Add(cbChatAllowPrefill);
            collapsibleGroupBox11.Controls.Add(label21);
            collapsibleGroupBox11.Controls.Add(panel17);
            collapsibleGroupBox11.Controls.Add(cbParallel);
            collapsibleGroupBox11.Controls.Add(label22);
            collapsibleGroupBox11.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox11.Location = new Point(6, 6);
            collapsibleGroupBox11.Name = "collapsibleGroupBox11";
            collapsibleGroupBox11.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox11.Size = new Size(314, 223);
            collapsibleGroupBox11.TabIndex = 41;
            collapsibleGroupBox11.Text = "Backend-Specific Settings";
            // 
            // ckLlamaCppSamplers
            // 
            ckLlamaCppSamplers.Dock = DockStyle.Top;
            ckLlamaCppSamplers.Font = new Font("Segoe UI", 9F);
            ckLlamaCppSamplers.Location = new Point(12, 179);
            ckLlamaCppSamplers.Name = "ckLlamaCppSamplers";
            ckLlamaCppSamplers.Size = new Size(290, 26);
            ckLlamaCppSamplers.TabIndex = 58;
            ckLlamaCppSamplers.Text = "Allow advanced samplers in Llama.cpp";
            ckLlamaCppSamplers.UseVisualStyleBackColor = true;
            // 
            // panel15
            // 
            panel15.Dock = DockStyle.Top;
            panel15.Location = new Point(12, 169);
            panel15.Margin = new Padding(8);
            panel15.Name = "panel15";
            panel15.Padding = new Padding(8);
            panel15.Size = new Size(290, 10);
            panel15.TabIndex = 55;
            // 
            // cbChatThinkPolicy
            // 
            cbChatThinkPolicy.BackColor = Color.FromArgb(64, 64, 64);
            cbChatThinkPolicy.Dock = DockStyle.Top;
            cbChatThinkPolicy.DropDownHeight = 180;
            cbChatThinkPolicy.Font = new Font("Segoe UI", 9F);
            cbChatThinkPolicy.Items.AddRange(new object[] { "Auto", "Emitted", "Silent" });
            cbChatThinkPolicy.Location = new Point(12, 145);
            cbChatThinkPolicy.MaxDropDownItems = 10;
            cbChatThinkPolicy.Name = "cbChatThinkPolicy";
            cbChatThinkPolicy.Padding = new Padding(1);
            cbChatThinkPolicy.Size = new Size(290, 24);
            cbChatThinkPolicy.TabIndex = 0;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Dock = DockStyle.Top;
            label20.Font = new Font("Segoe UI", 9F);
            label20.Location = new Point(12, 130);
            label20.Name = "label20";
            label20.Size = new Size(216, 15);
            label20.TabIndex = 5;
            label20.Text = "Chat Completion: First Think Tag Policy";
            // 
            // panel16
            // 
            panel16.Dock = DockStyle.Top;
            panel16.Location = new Point(12, 120);
            panel16.Margin = new Padding(8);
            panel16.Name = "panel16";
            panel16.Padding = new Padding(8);
            panel16.Size = new Size(290, 10);
            panel16.TabIndex = 56;
            // 
            // cbChatAllowPrefill
            // 
            cbChatAllowPrefill.BackColor = Color.FromArgb(64, 64, 64);
            cbChatAllowPrefill.Dock = DockStyle.Top;
            cbChatAllowPrefill.DropDownHeight = 180;
            cbChatAllowPrefill.Font = new Font("Segoe UI", 9F);
            cbChatAllowPrefill.Items.AddRange(new object[] { "Auto", "Allow", "Disallow" });
            cbChatAllowPrefill.Location = new Point(12, 96);
            cbChatAllowPrefill.MaxDropDownItems = 10;
            cbChatAllowPrefill.Name = "cbChatAllowPrefill";
            cbChatAllowPrefill.Padding = new Padding(1);
            cbChatAllowPrefill.Size = new Size(290, 24);
            cbChatAllowPrefill.TabIndex = 4;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Dock = DockStyle.Top;
            label21.Font = new Font("Segoe UI", 9F);
            label21.Location = new Point(12, 81);
            label21.Name = "label21";
            label21.Size = new Size(230, 15);
            label21.TabIndex = 3;
            label21.Text = "Chat Completion: Assistant Prefill Allowed";
            // 
            // panel17
            // 
            panel17.Dock = DockStyle.Top;
            panel17.Location = new Point(12, 71);
            panel17.Margin = new Padding(8);
            panel17.Name = "panel17";
            panel17.Padding = new Padding(8);
            panel17.Size = new Size(290, 10);
            panel17.TabIndex = 57;
            // 
            // cbParallel
            // 
            cbParallel.BackColor = Color.FromArgb(64, 64, 64);
            cbParallel.Dock = DockStyle.Top;
            cbParallel.DropDownHeight = 180;
            cbParallel.Font = new Font("Segoe UI", 9F);
            cbParallel.Items.AddRange(new object[] { "Auto", "Allow", "Disallow" });
            cbParallel.Location = new Point(12, 47);
            cbParallel.MaxDropDownItems = 10;
            cbParallel.Name = "cbParallel";
            cbParallel.Padding = new Padding(1);
            cbParallel.Size = new Size(290, 24);
            cbParallel.TabIndex = 2;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Dock = DockStyle.Top;
            label22.Font = new Font("Segoe UI", 9F);
            label22.Location = new Point(12, 32);
            label22.Name = "label22";
            label22.Size = new Size(111, 15);
            label22.TabIndex = 1;
            label22.Text = "Parallel Tool Calling";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1330, 673);
            Controls.Add(verticalStackPanel4);
            Controls.Add(verticalStackPanel2);
            Controls.Add(verticalStackPanel3);
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
            KeyDown += SettingsForm_KeyDown;
            panel1.ResumeLayout(false);
            collapsibleGroupBox1.ResumeLayout(false);
            verticalStackPanel1.ResumeLayout(false);
            collapsibleGroupBox5.ResumeLayout(false);
            collapsibleGroupBox5.PerformLayout();
            collapsibleGroupBox8.ResumeLayout(false);
            collapsibleGroupBox8.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            collapsibleGroupBox3.ResumeLayout(false);
            collapsibleGroupBox3.PerformLayout();
            collapsibleGroupBox2.ResumeLayout(false);
            collapsibleGroupBox2.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            collapsibleGroupBox7.ResumeLayout(false);
            verticalStackPanel2.ResumeLayout(false);
            collapsibleGroupBox10.ResumeLayout(false);
            panel14.ResumeLayout(false);
            panel14.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            collapsibleGroupBox4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            collapsibleGroupBox6.ResumeLayout(false);
            collapsibleGroupBox6.PerformLayout();
            verticalStackPanel3.ResumeLayout(false);
            collapsibleGroupBox9.ResumeLayout(false);
            panel13.ResumeLayout(false);
            panel13.PerformLayout();
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            verticalStackPanel4.ResumeLayout(false);
            collapsibleGroupBox11.ResumeLayout(false);
            collapsibleGroupBox11.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private ModernNumericUpDown num_removeitalicmaxword;
        private Label label63;
        private ModernCheckBox ck_oneparagraph;
        private ModernCheckBox ck_remlastsentence;
        private Label label62;
        private ModernNumericUpDown num_italicratio;
        private ModernCheckBox ck_reduceitalic;
        private ModernCheckBox ck_noemphasisword;
        private ModernCheckBox ck_fixquotes;
        private ModernCheckBox ck_noquotes;
        private ModernCheckBox ck_unbold;
        private Label label61;
        private ModernNumericUpDown num_antislopchance;
        private TextBox ed_sloplist;
        private ModernCheckBox ck_antislop;
        private ModernCheckBox ck_fixasterix;
        private Label label65;
        private ModernComboBox cb_pastsession;
        private ModernNumericUpDown num_memtokens;
        private Label label32;
        private ModernCheckBox ck_sessionmemory;
        private ModernNumericUpDown num_msgcount;
        private Label label30;
        private ModernNumericUpDown num_fontsize;
        private Label label29;
        private ModernComboBox cb_background;
        private Label label28;
        private ModernCheckBox ck_webgrammar;
        private ModernCheckBox ck_webkeyword;
        private Button bt_chattosessions;
        private Button bt_importworld;
        private Button bt_ImportSTChat;
        private ModernCheckBox ck_alwayswebsearch;
        private Label label15;
        private ModernNumericUpDown num_ragindex;
        private Label label14;
        private ModernNumericUpDown num_ragmaxretrieve;
        private Label label13;
        private ModernNumericUpDown num_ragcutoff;
        private Label label12;
        private ModernComboBox cb_ragheuristic;
        private Panel panel1;
        private Button bt_Close;
        private ToolTip HelptoolTip;
        private ModernCheckBox ckShowHidden;
        private ModernCheckBox ck_lastparaphfilter;
        private ModernCheckBox ck_forcePW;
        private Label label1;
        private ModernNumericUpDown num_ragM;
        private Label label3;
        private TextBox ed_searchkey;
        private Label label2;
        private ModernComboBox cb_searchapi;
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
        private Label label5;
        private ModernComboBox mcbSkin;
        private ModernCheckBox mck_cutmiddle;
        private ModernNumericUpDown numWIEntries;
        private Label label6;
        private ModernCheckBox ckNoPastInserts;
        private Panel panel2;
        private Panel panel7;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private CollapsibleGroupBox collapsibleGroupBox8;
        private Panel panel6;
        private Panel panel8;
        private ModernCheckBox ckDelStartSlop;
        private ModernCheckBox ckGroupAltern;
        private ModernComboBox cbGroupSessionStrategy;
        private Label label9;
        private ModernCheckBox ckDetailedSum;
        private ModernCheckBox ckGroupCommit;
        private ModernCheckBox ckParenthesizeToItalic;
        private VerticalStackPanel verticalStackPanel4;
        private ModernCheckBox ckForceInternalGram;
        private CollapsibleGroupBox collapsibleGroupBox9;
        private Panel panel10;
        private ModernNumericUpDown numFactTokens;
        private Label label11;
        private ModernCheckBox ckFactRetrieval;
        private Panel panel11;
        private ModernNumericUpDown numFactDedup;
        private Label label10;
        private Panel panel12;
        private ModernNumericUpDown numFactRetrieval;
        private Label label16;
        private Panel panel13;
        private ModernNumericUpDown numFactSuper;
        private Label label17;
        private ModernNumericUpDown numGroupQueue;
        private Label label8;
        private ModernComboBox ckGroupRouting;
        private Label label7;
        private CollapsibleGroupBox collapsibleGroupBox10;
        private Panel panel14;
        private ModernNumericUpDown num_ImgCount;
        private Label label18;
        private Panel panel9;
        private ModernNumericUpDown num_imgEmbed;
        private Label label19;
        private CollapsibleGroupBox collapsibleGroupBox11;
        private ModernCheckBox ckLlamaCppSamplers;
        private Panel panel15;
        private ModernComboBox cbChatThinkPolicy;
        private Label label20;
        private Panel panel16;
        private ModernComboBox cbChatAllowPrefill;
        private Label label21;
        private Panel panel17;
        private ModernComboBox cbParallel;
        private Label label22;
    }
}