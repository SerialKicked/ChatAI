namespace WaifuAI.src.forms
{
    partial class SamplerForm
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
            panMenu = new Panel();
            btSave = new Button();
            edFileName = new TextBox();
            listSampler = new ListBox();
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
            groupBox20 = new GroupBox();
            ck_trimstop = new CheckBox();
            ck_renderspecial = new CheckBox();
            ck_ignoreeos = new CheckBox();
            panMenu.SuspendLayout();
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
            groupBox20.SuspendLayout();
            SuspendLayout();
            // 
            // panMenu
            // 
            panMenu.Controls.Add(btSave);
            panMenu.Controls.Add(edFileName);
            panMenu.Controls.Add(listSampler);
            panMenu.Dock = DockStyle.Left;
            panMenu.Location = new Point(0, 0);
            panMenu.Name = "panMenu";
            panMenu.Size = new Size(234, 524);
            panMenu.TabIndex = 0;
            // 
            // btSave
            // 
            btSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btSave.Location = new Point(3, 498);
            btSave.Name = "btSave";
            btSave.Size = new Size(228, 23);
            btSave.TabIndex = 9;
            btSave.Text = "Save Sampling Settings";
            btSave.UseVisualStyleBackColor = true;
            btSave.Click += btSave_Click;
            // 
            // edFileName
            // 
            edFileName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            edFileName.Location = new Point(3, 469);
            edFileName.Name = "edFileName";
            edFileName.PlaceholderText = "Filename for prompt";
            edFileName.Size = new Size(228, 23);
            edFileName.TabIndex = 8;
            // 
            // listSampler
            // 
            listSampler.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listSampler.BorderStyle = BorderStyle.FixedSingle;
            listSampler.FormattingEnabled = true;
            listSampler.Location = new Point(3, 3);
            listSampler.Name = "listSampler";
            listSampler.ScrollAlwaysVisible = true;
            listSampler.Size = new Size(228, 452);
            listSampler.TabIndex = 7;
            listSampler.SelectedIndexChanged += listSampler_SelectedIndexChanged;
            // 
            // groupBox15
            // 
            groupBox15.Controls.Add(num_reppenrange);
            groupBox15.Controls.Add(label41);
            groupBox15.Controls.Add(num_reppen);
            groupBox15.Controls.Add(label40);
            groupBox15.Location = new Point(240, 283);
            groupBox15.Name = "groupBox15";
            groupBox15.Size = new Size(236, 88);
            groupBox15.TabIndex = 5;
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
            groupBox14.Controls.Add(num_seed);
            groupBox14.Controls.Add(label39);
            groupBox14.Controls.Add(num_temp);
            groupBox14.Controls.Add(label38);
            groupBox14.Location = new Point(240, 3);
            groupBox14.Name = "groupBox14";
            groupBox14.Size = new Size(727, 62);
            groupBox14.TabIndex = 4;
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
            groupBox13.Location = new Point(240, 71);
            groupBox13.Name = "groupBox13";
            groupBox13.Size = new Size(236, 206);
            groupBox13.TabIndex = 3;
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
            // groupBox19
            // 
            groupBox19.Controls.Add(num_xtcthres);
            groupBox19.Controls.Add(label50);
            groupBox19.Controls.Add(num_xtcprob);
            groupBox19.Controls.Add(label51);
            groupBox19.Location = new Point(724, 189);
            groupBox19.Name = "groupBox19";
            groupBox19.Size = new Size(243, 182);
            groupBox19.TabIndex = 10;
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
            groupBox18.Controls.Add(num_drymul);
            groupBox18.Controls.Add(label46);
            groupBox18.Controls.Add(num_drybase);
            groupBox18.Controls.Add(label48);
            groupBox18.Controls.Add(num_dryrange);
            groupBox18.Controls.Add(label49);
            groupBox18.Location = new Point(724, 71);
            groupBox18.Name = "groupBox18";
            groupBox18.Size = new Size(243, 112);
            groupBox18.TabIndex = 9;
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
            groupBox17.Location = new Point(482, 189);
            groupBox17.Name = "groupBox17";
            groupBox17.Size = new Size(236, 182);
            groupBox17.TabIndex = 8;
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
            groupBox16.Location = new Point(482, 71);
            groupBox16.Name = "groupBox16";
            groupBox16.Size = new Size(236, 112);
            groupBox16.TabIndex = 7;
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
            // groupBox20
            // 
            groupBox20.Controls.Add(ck_trimstop);
            groupBox20.Controls.Add(ck_renderspecial);
            groupBox20.Controls.Add(ck_ignoreeos);
            groupBox20.Location = new Point(240, 377);
            groupBox20.Name = "groupBox20";
            groupBox20.Size = new Size(727, 78);
            groupBox20.TabIndex = 11;
            groupBox20.TabStop = false;
            groupBox20.Text = "Misc Settings";
            // 
            // ck_trimstop
            // 
            ck_trimstop.AutoSize = true;
            ck_trimstop.Location = new Point(493, 26);
            ck_trimstop.Name = "ck_trimstop";
            ck_trimstop.Size = new Size(136, 19);
            ck_trimstop.TabIndex = 18;
            ck_trimstop.Text = "Trim Stop Sequences";
            ck_trimstop.UseVisualStyleBackColor = true;
            // 
            // ck_renderspecial
            // 
            ck_renderspecial.AutoSize = true;
            ck_renderspecial.Location = new Point(251, 26);
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
            // SamplerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(985, 524);
            Controls.Add(groupBox20);
            Controls.Add(groupBox19);
            Controls.Add(groupBox18);
            Controls.Add(groupBox17);
            Controls.Add(groupBox16);
            Controls.Add(groupBox15);
            Controls.Add(groupBox14);
            Controls.Add(groupBox13);
            Controls.Add(panMenu);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SamplerForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sampler Editor";
            panMenu.ResumeLayout(false);
            panMenu.PerformLayout();
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
            groupBox20.ResumeLayout(false);
            groupBox20.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panMenu;
        private Button btSave;
        private TextBox edFileName;
        private ListBox listSampler;
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
        private GroupBox groupBox20;
        private CheckBox ck_trimstop;
        private CheckBox ck_renderspecial;
        private CheckBox ck_ignoreeos;
    }
}