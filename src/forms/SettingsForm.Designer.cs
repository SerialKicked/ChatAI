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
            panel1 = new Panel();
            bt_Close = new Button();
            HelptoolTip = new ToolTip(components);
            ckShowHidden = new CheckBox();
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
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
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
            groupBox11.Controls.Add(ck_sessionmemory);
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
            // ck_sessionmemory
            // 
            ck_sessionmemory.AutoSize = true;
            ck_sessionmemory.Font = new Font("Segoe UI", 9F);
            ck_sessionmemory.Location = new Point(270, 20);
            ck_sessionmemory.Name = "ck_sessionmemory";
            ck_sessionmemory.Size = new Size(113, 19);
            ck_sessionmemory.TabIndex = 24;
            ck_sessionmemory.Text = "Session Memory";
            ck_sessionmemory.UseVisualStyleBackColor = true;
            ck_sessionmemory.CheckedChanged += ck_sessionmemory_CheckedChanged;
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(ckShowHidden);
            groupBox10.Controls.Add(num_msgcount);
            groupBox10.Controls.Add(label30);
            groupBox10.Controls.Add(num_fontsize);
            groupBox10.Controls.Add(label29);
            groupBox10.Controls.Add(cb_background);
            groupBox10.Controls.Add(label28);
            groupBox10.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox10.Location = new Point(8, 409);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(402, 95);
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
            num_msgcount.ValueChanged += num_fontsize_ValueChanged;
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
            ck_webgrammar.CheckedChanged += ck_webgrammar_CheckedChanged;
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
            cb_ragheuristic.SelectedIndexChanged += cb_ragheurisitic_SelectedIndexChanged;
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
            // panel1
            // 
            panel1.Controls.Add(bt_Close);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 510);
            panel1.Name = "panel1";
            panel1.Size = new Size(824, 50);
            panel1.TabIndex = 31;
            // 
            // bt_Close
            // 
            bt_Close.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_Close.BackColor = Color.PaleGreen;
            bt_Close.FlatStyle = FlatStyle.Flat;
            bt_Close.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_Close.Location = new Point(8, 15);
            bt_Close.Name = "bt_Close";
            bt_Close.Size = new Size(804, 23);
            bt_Close.TabIndex = 1;
            bt_Close.Text = "Apply and Close";
            bt_Close.UseVisualStyleBackColor = false;
            bt_Close.Click += bt_Close_Click;
            // 
            // ckShowHidden
            // 
            ckShowHidden.AutoSize = true;
            ckShowHidden.Font = new Font("Segoe UI", 9F);
            ckShowHidden.Location = new Point(6, 66);
            ckShowHidden.Name = "ckShowHidden";
            ckShowHidden.Size = new Size(189, 19);
            ckShowHidden.TabIndex = 31;
            ckShowHidden.Text = "Show hidden system messages";
            ckShowHidden.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(824, 560);
            Controls.Add(panel1);
            Controls.Add(groupBox24);
            Controls.Add(groupBox11);
            Controls.Add(groupBox10);
            Controls.Add(groupBox9);
            Controls.Add(groupBox2);
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
            ((System.ComponentModel.ISupportInitialize)num_ragindex).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_ragmaxretrieve).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_ragcutoff).EndInit();
            panel1.ResumeLayout(false);
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
        private CheckBox ck_sysrag;
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
        private Panel panel1;
        private Button bt_Close;
        private ToolTip HelptoolTip;
        private CheckBox ckShowHidden;
    }
}