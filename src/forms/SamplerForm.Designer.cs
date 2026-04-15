using LetheChat.Controls;

namespace LetheChat.Forms
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
            edFileName = new TextBox();
            listSampler = new ModernListBox();
            panel3 = new Panel();
            btSave = new Button();
            panel1 = new Panel();
            groupBox15 = new CollapsibleGroupBox();
            num_reppenrange = new ModernNumericUpDown();
            label41 = new Label();
            num_reppen = new ModernNumericUpDown();
            label40 = new Label();
            groupBox14 = new CollapsibleGroupBox();
            num_seed = new ModernNumericUpDown();
            label39 = new Label();
            num_temp = new ModernNumericUpDown();
            label38 = new Label();
            groupBox13 = new CollapsibleGroupBox();
            num_tfs = new ModernNumericUpDown();
            label37 = new Label();
            num_typical = new ModernNumericUpDown();
            label36 = new Label();
            num_minp = new ModernNumericUpDown();
            label35 = new Label();
            num_topp = new ModernNumericUpDown();
            label34 = new Label();
            num_topa = new ModernNumericUpDown();
            label33 = new Label();
            num_topk = new ModernNumericUpDown();
            label31 = new Label();
            groupBox19 = new CollapsibleGroupBox();
            num_xtcthres = new ModernNumericUpDown();
            label50 = new Label();
            num_xtcprob = new ModernNumericUpDown();
            label51 = new Label();
            groupBox18 = new CollapsibleGroupBox();
            num_drymul = new ModernNumericUpDown();
            label46 = new Label();
            num_drybase = new ModernNumericUpDown();
            label48 = new Label();
            num_dryrange = new ModernNumericUpDown();
            label49 = new Label();
            groupBox17 = new CollapsibleGroupBox();
            num_smoothfac = new ModernNumericUpDown();
            label59 = new Label();
            num_dynexpo = new ModernNumericUpDown();
            label45 = new Label();
            num_dynrange = new ModernNumericUpDown();
            label47 = new Label();
            groupBox16 = new CollapsibleGroupBox();
            num_meta = new ModernNumericUpDown();
            label42 = new Label();
            num_mtau = new ModernNumericUpDown();
            label44 = new Label();
            cb_miro = new ComboBox();
            label43 = new Label();
            groupBox20 = new CollapsibleGroupBox();
            ck_trimstop = new ModernCheckBox();
            ck_renderspecial = new ModernCheckBox();
            ck_ignoreeos = new ModernCheckBox();
            panMenu.SuspendLayout();
            groupBox15.SuspendLayout();
            groupBox14.SuspendLayout();
            groupBox13.SuspendLayout();
            groupBox19.SuspendLayout();
            groupBox18.SuspendLayout();
            groupBox17.SuspendLayout();
            groupBox16.SuspendLayout();
            groupBox20.SuspendLayout();
            SuspendLayout();
            // 
            // panMenu
            // 
            panMenu.Controls.Add(edFileName);
            panMenu.Controls.Add(listSampler);
            panMenu.Controls.Add(panel3);
            panMenu.Controls.Add(btSave);
            panMenu.Controls.Add(panel1);
            panMenu.Dock = DockStyle.Left;
            panMenu.Location = new Point(0, 0);
            panMenu.Name = "panMenu";
            panMenu.Size = new Size(234, 502);
            panMenu.TabIndex = 0;
            // 
            // edFileName
            // 
            edFileName.Dock = DockStyle.Bottom;
            edFileName.Location = new Point(0, 436);
            edFileName.Name = "edFileName";
            edFileName.PlaceholderText = "Filename for prompt";
            edFileName.Size = new Size(234, 23);
            edFileName.TabIndex = 8;
            // 
            // listSampler
            // 
            listSampler.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listSampler.BackColor = Color.FromArgb(64, 64, 64);
            listSampler.BorderStyle = BorderStyle.FixedSingle;
            listSampler.Font = new Font("Segoe UI", 9.25F);
            listSampler.Location = new Point(3, 3);
            listSampler.Name = "listSampler";
            listSampler.Padding = new Padding(1);
            listSampler.Size = new Size(228, 418);
            listSampler.TabIndex = 7;
            listSampler.SelectedIndexChanged += listSampler_SelectedIndexChanged;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 459);
            panel3.Margin = new Padding(8);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(8);
            panel3.Size = new Size(234, 10);
            panel3.TabIndex = 51;
            // 
            // btSave
            // 
            btSave.BackColor = Color.DarkSeaGreen;
            btSave.Dock = DockStyle.Bottom;
            btSave.FlatStyle = FlatStyle.Flat;
            btSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btSave.ForeColor = Color.Black;
            btSave.Location = new Point(0, 469);
            btSave.Name = "btSave";
            btSave.Size = new Size(234, 23);
            btSave.TabIndex = 9;
            btSave.Tag = "no-theme";
            btSave.Text = "Save Sampling Settings";
            btSave.UseVisualStyleBackColor = false;
            btSave.Click += btSave_Click;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 492);
            panel1.Margin = new Padding(8);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(8);
            panel1.Size = new Size(234, 10);
            panel1.TabIndex = 52;
            // 
            // groupBox15
            // 
            groupBox15.BackColor = Color.FromArgb(37, 37, 37);
            groupBox15.CanCollapse = false;
            groupBox15.Controls.Add(num_reppenrange);
            groupBox15.Controls.Add(label41);
            groupBox15.Controls.Add(num_reppen);
            groupBox15.Controls.Add(label40);
            groupBox15.Font = new Font("Segoe UI", 9.25F);
            groupBox15.Location = new Point(240, 302);
            groupBox15.Name = "groupBox15";
            groupBox15.Padding = new Padding(12, 32, 12, 10);
            groupBox15.Size = new Size(236, 106);
            groupBox15.TabIndex = 5;
            groupBox15.TabStop = false;
            groupBox15.Text = "Repetition Penalty";
            // 
            // num_reppenrange
            // 
            num_reppenrange.BackColor = Color.FromArgb(64, 64, 64);
            num_reppenrange.Font = new Font("Segoe UI", 9.25F);
            num_reppenrange.Location = new Point(117, 70);
            num_reppenrange.Maximum = new decimal(new int[] { 128000, 0, 0, 0 });
            num_reppenrange.Name = "num_reppenrange";
            num_reppenrange.Padding = new Padding(1);
            num_reppenrange.Size = new Size(110, 23);
            num_reppenrange.TabIndex = 7;
            // 
            // label41
            // 
            label41.AutoSize = true;
            label41.Location = new Point(6, 72);
            label41.Name = "label41";
            label41.Size = new Size(45, 17);
            label41.TabIndex = 6;
            label41.Text = "Range";
            // 
            // num_reppen
            // 
            num_reppen.BackColor = Color.FromArgb(64, 64, 64);
            num_reppen.DecimalPlaces = 2;
            num_reppen.Font = new Font("Segoe UI", 9.25F);
            num_reppen.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_reppen.Location = new Point(117, 41);
            num_reppen.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            num_reppen.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            num_reppen.Name = "num_reppen";
            num_reppen.Padding = new Padding(1);
            num_reppen.Size = new Size(110, 23);
            num_reppen.TabIndex = 5;
            num_reppen.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Location = new Point(6, 43);
            label40.Name = "label40";
            label40.Size = new Size(49, 17);
            label40.TabIndex = 4;
            label40.Text = "Penalty";
            // 
            // groupBox14
            // 
            groupBox14.BackColor = Color.FromArgb(37, 37, 37);
            groupBox14.CanCollapse = false;
            groupBox14.Controls.Add(num_seed);
            groupBox14.Controls.Add(label39);
            groupBox14.Controls.Add(num_temp);
            groupBox14.Controls.Add(label38);
            groupBox14.Font = new Font("Segoe UI", 9.25F);
            groupBox14.Location = new Point(240, 3);
            groupBox14.Name = "groupBox14";
            groupBox14.Padding = new Padding(12, 32, 12, 10);
            groupBox14.Size = new Size(727, 62);
            groupBox14.TabIndex = 4;
            groupBox14.TabStop = false;
            groupBox14.Text = "Core Settings";
            // 
            // num_seed
            // 
            num_seed.BackColor = Color.FromArgb(64, 64, 64);
            num_seed.Font = new Font("Segoe UI", 9.25F);
            num_seed.Location = new Point(296, 35);
            num_seed.Maximum = new decimal(new int[] { 1661992959, 1808227885, 5, 0 });
            num_seed.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            num_seed.Name = "num_seed";
            num_seed.Padding = new Padding(1);
            num_seed.Size = new Size(110, 23);
            num_seed.TabIndex = 15;
            num_seed.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.Location = new Point(258, 37);
            label39.Name = "label39";
            label39.Size = new Size(37, 17);
            label39.TabIndex = 14;
            label39.Text = "Seed";
            // 
            // num_temp
            // 
            num_temp.BackColor = Color.FromArgb(64, 64, 64);
            num_temp.DecimalPlaces = 2;
            num_temp.Font = new Font("Segoe UI", 9.25F);
            num_temp.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_temp.Location = new Point(117, 35);
            num_temp.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            num_temp.Name = "num_temp";
            num_temp.Padding = new Padding(1);
            num_temp.Size = new Size(110, 23);
            num_temp.TabIndex = 13;
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Location = new Point(6, 37);
            label38.Name = "label38";
            label38.Size = new Size(82, 17);
            label38.TabIndex = 12;
            label38.Text = "Temperature";
            // 
            // groupBox13
            // 
            groupBox13.BackColor = Color.FromArgb(37, 37, 37);
            groupBox13.CanCollapse = false;
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
            groupBox13.Font = new Font("Segoe UI", 9.25F);
            groupBox13.Location = new Point(240, 71);
            groupBox13.Name = "groupBox13";
            groupBox13.Padding = new Padding(12, 32, 12, 10);
            groupBox13.Size = new Size(236, 225);
            groupBox13.TabIndex = 3;
            groupBox13.TabStop = false;
            groupBox13.Text = "Main Samplers";
            // 
            // num_tfs
            // 
            num_tfs.BackColor = Color.FromArgb(64, 64, 64);
            num_tfs.DecimalPlaces = 2;
            num_tfs.Font = new Font("Segoe UI", 9.25F);
            num_tfs.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_tfs.Location = new Point(117, 154);
            num_tfs.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_tfs.Name = "num_tfs";
            num_tfs.Padding = new Padding(1);
            num_tfs.Size = new Size(110, 23);
            num_tfs.TabIndex = 11;
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Location = new Point(6, 156);
            label37.Name = "label37";
            label37.Size = new Size(114, 17);
            label37.TabIndex = 10;
            label37.Text = "Tail Free Sampling";
            // 
            // num_typical
            // 
            num_typical.BackColor = Color.FromArgb(64, 64, 64);
            num_typical.DecimalPlaces = 2;
            num_typical.Font = new Font("Segoe UI", 9.25F);
            num_typical.ForeColor = Color.Gray;
            num_typical.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_typical.Location = new Point(117, 183);
            num_typical.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_typical.Name = "num_typical";
            num_typical.Padding = new Padding(1);
            num_typical.Size = new Size(110, 23);
            num_typical.TabIndex = 9;
            num_typical.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Location = new Point(6, 185);
            label36.Name = "label36";
            label36.Size = new Size(47, 17);
            label36.TabIndex = 8;
            label36.Text = "Typical";
            // 
            // num_minp
            // 
            num_minp.BackColor = Color.FromArgb(64, 64, 64);
            num_minp.DecimalPlaces = 2;
            num_minp.Font = new Font("Segoe UI", 9.25F);
            num_minp.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_minp.Location = new Point(117, 125);
            num_minp.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_minp.Name = "num_minp";
            num_minp.Padding = new Padding(1);
            num_minp.Size = new Size(110, 23);
            num_minp.TabIndex = 7;
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.Location = new Point(6, 127);
            label35.Name = "label35";
            label35.Size = new Size(41, 17);
            label35.TabIndex = 6;
            label35.Text = "Min P";
            // 
            // num_topp
            // 
            num_topp.BackColor = Color.FromArgb(64, 64, 64);
            num_topp.DecimalPlaces = 2;
            num_topp.Font = new Font("Segoe UI", 9.25F);
            num_topp.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_topp.Location = new Point(117, 96);
            num_topp.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_topp.Name = "num_topp";
            num_topp.Padding = new Padding(1);
            num_topp.Size = new Size(110, 23);
            num_topp.TabIndex = 5;
            num_topp.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Location = new Point(6, 98);
            label34.Name = "label34";
            label34.Size = new Size(41, 17);
            label34.TabIndex = 4;
            label34.Text = "Top P";
            // 
            // num_topa
            // 
            num_topa.BackColor = Color.FromArgb(64, 64, 64);
            num_topa.DecimalPlaces = 2;
            num_topa.Font = new Font("Segoe UI", 9.25F);
            num_topa.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_topa.Location = new Point(117, 67);
            num_topa.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_topa.Name = "num_topa";
            num_topa.Padding = new Padding(1);
            num_topa.Size = new Size(110, 23);
            num_topa.TabIndex = 3;
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Location = new Point(6, 69);
            label33.Name = "label33";
            label33.Size = new Size(42, 17);
            label33.TabIndex = 2;
            label33.Text = "Top A";
            // 
            // num_topk
            // 
            num_topk.BackColor = Color.FromArgb(64, 64, 64);
            num_topk.Font = new Font("Segoe UI", 9.25F);
            num_topk.Location = new Point(117, 38);
            num_topk.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            num_topk.Name = "num_topk";
            num_topk.Padding = new Padding(1);
            num_topk.Size = new Size(110, 23);
            num_topk.TabIndex = 1;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(6, 40);
            label31.Name = "label31";
            label31.Size = new Size(42, 17);
            label31.TabIndex = 0;
            label31.Text = "Top K";
            // 
            // groupBox19
            // 
            groupBox19.BackColor = Color.FromArgb(37, 37, 37);
            groupBox19.CanCollapse = false;
            groupBox19.Controls.Add(num_xtcthres);
            groupBox19.Controls.Add(label50);
            groupBox19.Controls.Add(num_xtcprob);
            groupBox19.Controls.Add(label51);
            groupBox19.Font = new Font("Segoe UI", 9.25F);
            groupBox19.Location = new Point(724, 221);
            groupBox19.Name = "groupBox19";
            groupBox19.Padding = new Padding(12, 32, 12, 10);
            groupBox19.Size = new Size(243, 187);
            groupBox19.TabIndex = 10;
            groupBox19.TabStop = false;
            groupBox19.Text = "Exclude Top Tokens (XTC)";
            // 
            // num_xtcthres
            // 
            num_xtcthres.BackColor = Color.FromArgb(64, 64, 64);
            num_xtcthres.DecimalPlaces = 2;
            num_xtcthres.Font = new Font("Segoe UI", 9.25F);
            num_xtcthres.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_xtcthres.Location = new Point(118, 65);
            num_xtcthres.Maximum = new decimal(new int[] { 5, 0, 0, 65536 });
            num_xtcthres.Name = "num_xtcthres";
            num_xtcthres.Padding = new Padding(1);
            num_xtcthres.Size = new Size(110, 23);
            num_xtcthres.TabIndex = 15;
            num_xtcthres.Value = new decimal(new int[] { 15, 0, 0, 131072 });
            // 
            // label50
            // 
            label50.AutoSize = true;
            label50.Location = new Point(7, 67);
            label50.Name = "label50";
            label50.Size = new Size(66, 17);
            label50.TabIndex = 14;
            label50.Text = "Threshold";
            // 
            // num_xtcprob
            // 
            num_xtcprob.BackColor = Color.FromArgb(64, 64, 64);
            num_xtcprob.DecimalPlaces = 2;
            num_xtcprob.Font = new Font("Segoe UI", 9.25F);
            num_xtcprob.Location = new Point(118, 36);
            num_xtcprob.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_xtcprob.Name = "num_xtcprob";
            num_xtcprob.Padding = new Padding(1);
            num_xtcprob.Size = new Size(110, 23);
            num_xtcprob.TabIndex = 13;
            // 
            // label51
            // 
            label51.AutoSize = true;
            label51.Location = new Point(7, 38);
            label51.Name = "label51";
            label51.Size = new Size(70, 17);
            label51.TabIndex = 12;
            label51.Text = "Probability";
            // 
            // groupBox18
            // 
            groupBox18.BackColor = Color.FromArgb(37, 37, 37);
            groupBox18.CanCollapse = false;
            groupBox18.Controls.Add(num_drymul);
            groupBox18.Controls.Add(label46);
            groupBox18.Controls.Add(num_drybase);
            groupBox18.Controls.Add(label48);
            groupBox18.Controls.Add(num_dryrange);
            groupBox18.Controls.Add(label49);
            groupBox18.Font = new Font("Segoe UI", 9.25F);
            groupBox18.Location = new Point(724, 71);
            groupBox18.Name = "groupBox18";
            groupBox18.Padding = new Padding(12, 32, 12, 10);
            groupBox18.Size = new Size(243, 144);
            groupBox18.TabIndex = 9;
            groupBox18.TabStop = false;
            groupBox18.Text = "DRY Anti Repetition";
            // 
            // num_drymul
            // 
            num_drymul.BackColor = Color.FromArgb(64, 64, 64);
            num_drymul.DecimalPlaces = 2;
            num_drymul.Font = new Font("Segoe UI", 9.25F);
            num_drymul.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_drymul.Location = new Point(118, 38);
            num_drymul.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_drymul.Name = "num_drymul";
            num_drymul.Padding = new Padding(1);
            num_drymul.Size = new Size(110, 23);
            num_drymul.TabIndex = 11;
            // 
            // label46
            // 
            label46.AutoSize = true;
            label46.Location = new Point(7, 40);
            label46.Name = "label46";
            label46.Size = new Size(63, 17);
            label46.TabIndex = 10;
            label46.Text = "Multiplier";
            // 
            // num_drybase
            // 
            num_drybase.BackColor = Color.FromArgb(64, 64, 64);
            num_drybase.DecimalPlaces = 2;
            num_drybase.Font = new Font("Segoe UI", 9.25F);
            num_drybase.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_drybase.Location = new Point(118, 67);
            num_drybase.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_drybase.Name = "num_drybase";
            num_drybase.Padding = new Padding(1);
            num_drybase.Size = new Size(110, 23);
            num_drybase.TabIndex = 9;
            num_drybase.Value = new decimal(new int[] { 175, 0, 0, 131072 });
            // 
            // label48
            // 
            label48.AutoSize = true;
            label48.Location = new Point(7, 69);
            label48.Name = "label48";
            label48.Size = new Size(35, 17);
            label48.TabIndex = 8;
            label48.Text = "Base";
            // 
            // num_dryrange
            // 
            num_dryrange.BackColor = Color.FromArgb(64, 64, 64);
            num_dryrange.Font = new Font("Segoe UI", 9.25F);
            num_dryrange.Location = new Point(118, 96);
            num_dryrange.Maximum = new decimal(new int[] { 128000, 0, 0, 0 });
            num_dryrange.Name = "num_dryrange";
            num_dryrange.Padding = new Padding(1);
            num_dryrange.Size = new Size(110, 23);
            num_dryrange.TabIndex = 7;
            // 
            // label49
            // 
            label49.AutoSize = true;
            label49.Location = new Point(7, 96);
            label49.Name = "label49";
            label49.Size = new Size(45, 17);
            label49.TabIndex = 6;
            label49.Text = "Range";
            // 
            // groupBox17
            // 
            groupBox17.BackColor = Color.FromArgb(37, 37, 37);
            groupBox17.CanCollapse = false;
            groupBox17.Controls.Add(num_smoothfac);
            groupBox17.Controls.Add(label59);
            groupBox17.Controls.Add(num_dynexpo);
            groupBox17.Controls.Add(label45);
            groupBox17.Controls.Add(num_dynrange);
            groupBox17.Controls.Add(label47);
            groupBox17.Font = new Font("Segoe UI", 9.25F);
            groupBox17.Location = new Point(482, 221);
            groupBox17.Name = "groupBox17";
            groupBox17.Padding = new Padding(12, 32, 12, 10);
            groupBox17.Size = new Size(236, 187);
            groupBox17.TabIndex = 8;
            groupBox17.TabStop = false;
            groupBox17.Text = "Dynamic Temperature";
            // 
            // num_smoothfac
            // 
            num_smoothfac.BackColor = Color.FromArgb(64, 64, 64);
            num_smoothfac.DecimalPlaces = 2;
            num_smoothfac.Font = new Font("Segoe UI", 9.25F);
            num_smoothfac.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_smoothfac.Location = new Point(117, 94);
            num_smoothfac.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_smoothfac.Name = "num_smoothfac";
            num_smoothfac.Padding = new Padding(1);
            num_smoothfac.Size = new Size(110, 23);
            num_smoothfac.TabIndex = 13;
            // 
            // label59
            // 
            label59.AutoSize = true;
            label59.Location = new Point(9, 100);
            label59.Name = "label59";
            label59.Size = new Size(93, 17);
            label59.TabIndex = 12;
            label59.Text = "Smooth Factor";
            // 
            // num_dynexpo
            // 
            num_dynexpo.BackColor = Color.FromArgb(64, 64, 64);
            num_dynexpo.DecimalPlaces = 2;
            num_dynexpo.Font = new Font("Segoe UI", 9.25F);
            num_dynexpo.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_dynexpo.Location = new Point(117, 65);
            num_dynexpo.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_dynexpo.Name = "num_dynexpo";
            num_dynexpo.Padding = new Padding(1);
            num_dynexpo.Size = new Size(110, 23);
            num_dynexpo.TabIndex = 11;
            num_dynexpo.Value = new decimal(new int[] { 9, 0, 0, 65536 });
            // 
            // label45
            // 
            label45.AutoSize = true;
            label45.Location = new Point(9, 71);
            label45.Name = "label45";
            label45.Size = new Size(62, 17);
            label45.TabIndex = 10;
            label45.Text = "Exponent";
            // 
            // num_dynrange
            // 
            num_dynrange.BackColor = Color.FromArgb(64, 64, 64);
            num_dynrange.DecimalPlaces = 2;
            num_dynrange.Font = new Font("Segoe UI", 9.25F);
            num_dynrange.Location = new Point(117, 36);
            num_dynrange.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            num_dynrange.Name = "num_dynrange";
            num_dynrange.Padding = new Padding(1);
            num_dynrange.Size = new Size(110, 23);
            num_dynrange.TabIndex = 7;
            // 
            // label47
            // 
            label47.AutoSize = true;
            label47.Location = new Point(9, 42);
            label47.Name = "label47";
            label47.Size = new Size(45, 17);
            label47.TabIndex = 6;
            label47.Text = "Range";
            // 
            // groupBox16
            // 
            groupBox16.BackColor = Color.FromArgb(37, 37, 37);
            groupBox16.CanCollapse = false;
            groupBox16.Controls.Add(num_meta);
            groupBox16.Controls.Add(label42);
            groupBox16.Controls.Add(num_mtau);
            groupBox16.Controls.Add(label44);
            groupBox16.Controls.Add(cb_miro);
            groupBox16.Controls.Add(label43);
            groupBox16.Font = new Font("Segoe UI", 9.25F);
            groupBox16.Location = new Point(482, 71);
            groupBox16.Name = "groupBox16";
            groupBox16.Padding = new Padding(12, 32, 12, 10);
            groupBox16.Size = new Size(236, 144);
            groupBox16.TabIndex = 7;
            groupBox16.TabStop = false;
            groupBox16.Text = "Mirostat Sampler";
            // 
            // num_meta
            // 
            num_meta.BackColor = Color.FromArgb(64, 64, 64);
            num_meta.DecimalPlaces = 2;
            num_meta.Font = new Font("Segoe UI", 9.25F);
            num_meta.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_meta.Location = new Point(120, 96);
            num_meta.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            num_meta.Name = "num_meta";
            num_meta.Padding = new Padding(1);
            num_meta.Size = new Size(110, 23);
            num_meta.TabIndex = 12;
            num_meta.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // label42
            // 
            label42.AutoSize = true;
            label42.Location = new Point(9, 98);
            label42.Name = "label42";
            label42.Size = new Size(26, 17);
            label42.TabIndex = 11;
            label42.Text = "Eta";
            // 
            // num_mtau
            // 
            num_mtau.BackColor = Color.FromArgb(64, 64, 64);
            num_mtau.DecimalPlaces = 2;
            num_mtau.Font = new Font("Segoe UI", 9.25F);
            num_mtau.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_mtau.Location = new Point(120, 67);
            num_mtau.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            num_mtau.Name = "num_mtau";
            num_mtau.Padding = new Padding(1);
            num_mtau.Size = new Size(110, 23);
            num_mtau.TabIndex = 10;
            num_mtau.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label44
            // 
            label44.AutoSize = true;
            label44.Location = new Point(9, 69);
            label44.Name = "label44";
            label44.Size = new Size(28, 17);
            label44.TabIndex = 9;
            label44.Text = "Tau";
            // 
            // cb_miro
            // 
            cb_miro.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_miro.FormattingEnabled = true;
            cb_miro.Items.AddRange(new object[] { "Disabled", "v1", "v2" });
            cb_miro.Location = new Point(120, 38);
            cb_miro.Name = "cb_miro";
            cb_miro.Size = new Size(110, 23);
            cb_miro.TabIndex = 8;
            // 
            // label43
            // 
            label43.AutoSize = true;
            label43.Location = new Point(9, 40);
            label43.Name = "label43";
            label43.Size = new Size(51, 17);
            label43.TabIndex = 4;
            label43.Text = "Version";
            // 
            // groupBox20
            // 
            groupBox20.BackColor = Color.FromArgb(37, 37, 37);
            groupBox20.CanCollapse = false;
            groupBox20.Controls.Add(ck_trimstop);
            groupBox20.Controls.Add(ck_renderspecial);
            groupBox20.Controls.Add(ck_ignoreeos);
            groupBox20.Font = new Font("Segoe UI", 9.25F);
            groupBox20.Location = new Point(240, 414);
            groupBox20.Name = "groupBox20";
            groupBox20.Padding = new Padding(12, 32, 12, 10);
            groupBox20.Size = new Size(727, 78);
            groupBox20.TabIndex = 11;
            groupBox20.TabStop = false;
            groupBox20.Text = "Misc Settings";
            // 
            // ck_trimstop
            // 
            ck_trimstop.Font = new Font("Segoe UI", 9.25F);
            ck_trimstop.Location = new Point(502, 44);
            ck_trimstop.Name = "ck_trimstop";
            ck_trimstop.Size = new Size(189, 21);
            ck_trimstop.TabIndex = 18;
            ck_trimstop.Text = "Trim Stop Sequences";
            ck_trimstop.UseVisualStyleBackColor = true;
            // 
            // ck_renderspecial
            // 
            ck_renderspecial.Font = new Font("Segoe UI", 9.25F);
            ck_renderspecial.Location = new Point(260, 44);
            ck_renderspecial.Name = "ck_renderspecial";
            ck_renderspecial.Size = new Size(194, 21);
            ck_renderspecial.TabIndex = 17;
            ck_renderspecial.Text = "Render Special Tokens";
            ck_renderspecial.UseVisualStyleBackColor = true;
            // 
            // ck_ignoreeos
            // 
            ck_ignoreeos.Font = new Font("Segoe UI", 9.25F);
            ck_ignoreeos.Location = new Point(15, 44);
            ck_ignoreeos.Name = "ck_ignoreeos";
            ck_ignoreeos.Size = new Size(212, 21);
            ck_ignoreeos.TabIndex = 16;
            ck_ignoreeos.Text = "Ignore EOS Token";
            ck_ignoreeos.UseVisualStyleBackColor = true;
            // 
            // SamplerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(985, 502);
            Controls.Add(groupBox20);
            Controls.Add(groupBox19);
            Controls.Add(groupBox18);
            Controls.Add(groupBox17);
            Controls.Add(groupBox16);
            Controls.Add(groupBox15);
            Controls.Add(groupBox14);
            Controls.Add(groupBox13);
            Controls.Add(panMenu);
            ForeColor = Color.LightGray;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SamplerForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sampler Editor";
            KeyDown += SamplerForm_KeyDown;
            panMenu.ResumeLayout(false);
            panMenu.PerformLayout();
            groupBox15.ResumeLayout(false);
            groupBox15.PerformLayout();
            groupBox14.ResumeLayout(false);
            groupBox14.PerformLayout();
            groupBox13.ResumeLayout(false);
            groupBox13.PerformLayout();
            groupBox19.ResumeLayout(false);
            groupBox19.PerformLayout();
            groupBox18.ResumeLayout(false);
            groupBox18.PerformLayout();
            groupBox17.ResumeLayout(false);
            groupBox17.PerformLayout();
            groupBox16.ResumeLayout(false);
            groupBox16.PerformLayout();
            groupBox20.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panMenu;
        private Button btSave;
        private TextBox edFileName;
        private ModernListBox listSampler;
        private CollapsibleGroupBox groupBox15;
        private ModernNumericUpDown num_reppenrange;
        private Label label41;
        private ModernNumericUpDown num_reppen;
        private Label label40;
        private CollapsibleGroupBox groupBox14;
        private ModernNumericUpDown num_seed;
        private Label label39;
        private ModernNumericUpDown num_temp;
        private Label label38;
        private CollapsibleGroupBox groupBox13;
        private ModernNumericUpDown num_tfs;
        private Label label37;
        private ModernNumericUpDown num_typical;
        private Label label36;
        private ModernNumericUpDown num_minp;
        private Label label35;
        private ModernNumericUpDown num_topp;
        private Label label34;
        private ModernNumericUpDown num_topa;
        private Label label33;
        private ModernNumericUpDown num_topk;
        private Label label31;
        private CollapsibleGroupBox groupBox19;
        private ModernNumericUpDown num_xtcthres;
        private Label label50;
        private ModernNumericUpDown num_xtcprob;
        private Label label51;
        private CollapsibleGroupBox groupBox18;
        private ModernNumericUpDown num_drymul;
        private Label label46;
        private ModernNumericUpDown num_drybase;
        private Label label48;
        private ModernNumericUpDown num_dryrange;
        private Label label49;
        private CollapsibleGroupBox groupBox17;
        private ModernNumericUpDown num_smoothfac;
        private Label label59;
        private ModernNumericUpDown num_dynexpo;
        private Label label45;
        private ModernNumericUpDown num_dynrange;
        private Label label47;
        private CollapsibleGroupBox groupBox16;
        private ModernNumericUpDown num_meta;
        private Label label42;
        private ModernNumericUpDown num_mtau;
        private Label label44;
        private ComboBox cb_miro;
        private Label label43;
        private CollapsibleGroupBox groupBox20;
        private ModernCheckBox ck_trimstop;
        private ModernCheckBox ck_renderspecial;
        private ModernCheckBox ck_ignoreeos;
        private Panel panel1;
        private Panel panel3;
    }
}