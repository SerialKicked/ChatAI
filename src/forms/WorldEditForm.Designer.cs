namespace WaifuAI.src.forms
{
    partial class WorldEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorldEditForm));
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
            lb_worldentries = new ListBox();
            label17 = new Label();
            label16 = new Label();
            num_scandepth = new NumericUpDown();
            ed_worlddesc = new TextBox();
            groupBox3 = new GroupBox();
            bt_close = new Button();
            bt_worldsave = new Button();
            cb_worlds = new ComboBox();
            groupBox1 = new GroupBox();
            groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numWItriggerchance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_wentrypriority).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_wentryduration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_wentryposition).BeginInit();
            groupBox7.SuspendLayout();
            groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_scandepth).BeginInit();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
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
            groupBox8.Location = new Point(263, 522);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(733, 189);
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
            groupBox7.Location = new Point(263, 270);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(733, 246);
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
            ed_wentrymem.Size = new Size(719, 159);
            ed_wentrymem.TabIndex = 2;
            ed_wentrymem.TextChanged += UpdateWorldEntryEvent;
            // 
            // ed_wentryname
            // 
            ed_wentryname.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ed_wentryname.Location = new Point(6, 37);
            ed_wentryname.Name = "ed_wentryname";
            ed_wentryname.Size = new Size(719, 23);
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
            groupBox6.Controls.Add(bt_delwentry);
            groupBox6.Controls.Add(bt_addwentry);
            groupBox6.Controls.Add(lb_worldentries);
            groupBox6.Location = new Point(12, 270);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(245, 441);
            groupBox6.TabIndex = 3;
            groupBox6.TabStop = false;
            groupBox6.Text = "World Entries";
            // 
            // ck_wiembed
            // 
            ck_wiembed.AutoSize = true;
            ck_wiembed.Location = new Point(679, 70);
            ck_wiembed.Name = "ck_wiembed";
            ck_wiembed.Size = new Size(150, 19);
            ck_wiembed.TabIndex = 8;
            ck_wiembed.Text = "Use Vector Embeddings";
            ck_wiembed.UseVisualStyleBackColor = true;
            ck_wiembed.CheckedChanged += ck_wiembed_CheckedChanged;
            // 
            // bt_delwentry
            // 
            bt_delwentry.Location = new Point(143, 409);
            bt_delwentry.Name = "bt_delwentry";
            bt_delwentry.Size = new Size(90, 23);
            bt_delwentry.TabIndex = 7;
            bt_delwentry.Text = "Delete";
            bt_delwentry.UseVisualStyleBackColor = true;
            bt_delwentry.Click += bt_delwentry_Click;
            // 
            // bt_addwentry
            // 
            bt_addwentry.Location = new Point(4, 409);
            bt_addwentry.Name = "bt_addwentry";
            bt_addwentry.Size = new Size(90, 23);
            bt_addwentry.TabIndex = 6;
            bt_addwentry.Text = "Add New";
            bt_addwentry.UseVisualStyleBackColor = true;
            bt_addwentry.Click += bt_addwentry_Click;
            // 
            // lb_worldentries
            // 
            lb_worldentries.FormattingEnabled = true;
            lb_worldentries.Location = new Point(6, 22);
            lb_worldentries.Name = "lb_worldentries";
            lb_worldentries.Size = new Size(227, 379);
            lb_worldentries.TabIndex = 4;
            lb_worldentries.SelectedIndexChanged += lb_worldentries_SelectedIndexChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label17.Location = new Point(679, 19);
            label17.Name = "label17";
            label17.Size = new Size(71, 15);
            label17.TabIndex = 3;
            label17.Text = "Scan Depth";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label16.Location = new Point(6, 23);
            label16.Name = "label16";
            label16.Size = new Size(71, 15);
            label16.TabIndex = 2;
            label16.Text = "Description";
            // 
            // num_scandepth
            // 
            num_scandepth.Location = new Point(679, 41);
            num_scandepth.Name = "num_scandepth";
            num_scandepth.Size = new Size(249, 23);
            num_scandepth.TabIndex = 1;
            num_scandepth.ValueChanged += num_scandepth_ValueChanged;
            // 
            // ed_worlddesc
            // 
            ed_worlddesc.Location = new Point(6, 41);
            ed_worlddesc.Multiline = true;
            ed_worlddesc.Name = "ed_worlddesc";
            ed_worlddesc.ScrollBars = ScrollBars.Vertical;
            ed_worlddesc.Size = new Size(667, 142);
            ed_worlddesc.TabIndex = 0;
            ed_worlddesc.KeyPress += ed_worlddesc_KeyPress;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(bt_close);
            groupBox3.Controls.Add(bt_worldsave);
            groupBox3.Controls.Add(cb_worlds);
            groupBox3.Location = new Point(12, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(984, 57);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "File Selection";
            // 
            // bt_close
            // 
            bt_close.Location = new Point(903, 22);
            bt_close.Name = "bt_close";
            bt_close.Size = new Size(75, 23);
            bt_close.TabIndex = 2;
            bt_close.Text = "Close";
            bt_close.UseVisualStyleBackColor = true;
            bt_close.Click += bt_close_Click;
            // 
            // bt_worldsave
            // 
            bt_worldsave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_worldsave.Location = new Point(789, 22);
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
            cb_worlds.Size = new Size(775, 23);
            cb_worlds.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ck_wiembed);
            groupBox1.Controls.Add(ed_worlddesc);
            groupBox1.Controls.Add(label16);
            groupBox1.Controls.Add(num_scandepth);
            groupBox1.Controls.Add(label17);
            groupBox1.Location = new Point(12, 75);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(984, 189);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "World Settings";
            // 
            // WorldEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 716);
            Controls.Add(groupBox1);
            Controls.Add(groupBox8);
            Controls.Add(groupBox7);
            Controls.Add(groupBox6);
            Controls.Add(groupBox3);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WorldEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "World Info Editor";
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numWItriggerchance).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_wentrypriority).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_wentryduration).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_wentryposition).EndInit();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)num_scandepth).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
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
        private ListBox lb_worldentries;
        private Label label17;
        private Label label16;
        private NumericUpDown num_scandepth;
        private TextBox ed_worlddesc;
        private GroupBox groupBox3;
        private Button bt_close;
        private Button bt_worldsave;
        private ComboBox cb_worlds;
        private GroupBox groupBox1;
    }
}