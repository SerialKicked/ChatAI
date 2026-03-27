using LetheChat.Controls;

namespace LetheChat.Forms
{
    partial class CharEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharEditForm));
            btRegenBio = new Button();
            label13 = new Label();
            num_selfedittokens = new ModernNumericUpDown();
            ed_selfedit = new TextBox();
            label12 = new Label();
            num_ptvalue = new ModernNumericUpDown();
            cb_pointsystems = new ModernComboBox();
            label14 = new Label();
            ed_outetts = new TextBox();
            label11 = new Label();
            label9 = new Label();
            label8 = new Label();
            num_minAFK = new ModernNumericUpDown();
            label17 = new Label();
            numKeepEurekas = new ModernNumericUpDown();
            numEurekaMinTime = new ModernNumericUpDown();
            label16 = new Label();
            label15 = new Label();
            numEurekaMinMess = new ModernNumericUpDown();
            pic = new PictureBox();
            cb_icon = new ModernComboBox();
            label4 = new Label();
            ed_firstmessage = new TextBox();
            ed_scenario = new TextBox();
            label3 = new Label();
            ed_bio = new TextBox();
            label2 = new Label();
            ck_isuser = new CheckBox();
            ed_name = new TextBox();
            label1 = new Label();
            ed_writingstyle = new TextBox();
            label7 = new Label();
            label5 = new Label();
            label6 = new Label();
            ed_sysprompt = new TextBox();
            textBox2 = new TextBox();
            label19 = new Label();
            numAgentDelay = new ModernNumericUpDown();
            label18 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            cb_charlist = new ModernListBox();
            edFilename = new TextBox();
            panel3 = new Panel();
            btSave = new Button();
            panel4 = new Panel();
            TabControl = new ModernTabControl();
            tbGeneral = new TabPage();
            label27 = new Label();
            edMiniBio = new TextBox();
            ckNoGuidance = new CheckBox();
            tbStyle = new TabPage();
            collapsibleGroupBox3 = new CollapsibleGroupBox();
            collapsibleGroupBox2 = new CollapsibleGroupBox();
            ckl_samplers = new ModernCheckedListBox();
            collapsibleGroupBox1 = new CollapsibleGroupBox();
            ckl_worldinfo = new ModernCheckedListBox();
            tbPrompt = new TabPage();
            tbAgent = new TabPage();
            collapsibleGroupBox8 = new CollapsibleGroupBox();
            listNoRAGMemTypes = new ModernCheckedListBox();
            label23 = new Label();
            collapsibleGroupBox7 = new CollapsibleGroupBox();
            listCanDecay = new ModernCheckedListBox();
            label22 = new Label();
            collapsibleGroupBox6 = new CollapsibleGroupBox();
            ckAllowEurekas = new ModernCheckBox();
            ckAgent = new ModernCheckBox();
            collapsibleGroupBox5 = new CollapsibleGroupBox();
            listAgentTasks = new ModernCheckedListBox();
            tbSettings = new TabPage();
            collapsibleGroupBox11 = new CollapsibleGroupBox();
            ckTimerAddDuration = new ModernCheckBox();
            ckTimerActive = new ModernCheckBox();
            ckTimerAutoExpire = new ModernCheckBox();
            label30 = new Label();
            edTimerExpired = new TextBox();
            label29 = new Label();
            edTimerOff = new TextBox();
            label28 = new Label();
            edTimerOn = new TextBox();
            ckTimerEnable = new ModernCheckBox();
            collapsibleGroupBox10 = new CollapsibleGroupBox();
            collapsibleGroupBox9 = new CollapsibleGroupBox();
            ckl_plugins = new ModernCheckedListBox();
            collapsibleGroupBox4 = new CollapsibleGroupBox();
            ckPassword = new ModernCheckBox();
            ck_senseoftime = new ModernCheckBox();
            ck_irldates = new ModernCheckBox();
            ckMoodSystem = new ModernCheckBox();
            ck_caninitchat = new ModernCheckBox();
            tabMood = new TabPage();
            ckStaticMood = new ModernCheckBox();
            label24 = new Label();
            moodSanity = new ModernNumericUpDown();
            label25 = new Label();
            moodSub = new ModernNumericUpDown();
            label26 = new Label();
            moodHorny = new ModernNumericUpDown();
            label21 = new Label();
            moodCuriosity = new ModernNumericUpDown();
            label20 = new Label();
            moodCheer = new ModernNumericUpDown();
            label10 = new Label();
            moodEnergy = new ModernNumericUpDown();
            tabSchedule = new TabPage();
            lblSchedulePrefix = new Label();
            edSchedulePrefix = new TextBox();
            lblSunday = new Label();
            edSunday = new TextBox();
            lblMonday = new Label();
            edMonday = new TextBox();
            lblTuesday = new Label();
            edTuesday = new TextBox();
            lblWednesday = new Label();
            edWednesday = new TextBox();
            lblThursday = new Label();
            edThursday = new TextBox();
            lblFriday = new Label();
            edFriday = new TextBox();
            lblSaturday = new Label();
            edSaturday = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            panel1.SuspendLayout();
            TabControl.SuspendLayout();
            tbGeneral.SuspendLayout();
            tbStyle.SuspendLayout();
            collapsibleGroupBox3.SuspendLayout();
            collapsibleGroupBox2.SuspendLayout();
            collapsibleGroupBox1.SuspendLayout();
            tbPrompt.SuspendLayout();
            tbAgent.SuspendLayout();
            collapsibleGroupBox8.SuspendLayout();
            collapsibleGroupBox7.SuspendLayout();
            collapsibleGroupBox6.SuspendLayout();
            collapsibleGroupBox5.SuspendLayout();
            tbSettings.SuspendLayout();
            collapsibleGroupBox11.SuspendLayout();
            collapsibleGroupBox10.SuspendLayout();
            collapsibleGroupBox9.SuspendLayout();
            collapsibleGroupBox4.SuspendLayout();
            tabMood.SuspendLayout();
            tabSchedule.SuspendLayout();
            SuspendLayout();
            // 
            // btRegenBio
            // 
            btRegenBio.FlatStyle = FlatStyle.Flat;
            btRegenBio.Location = new Point(235, 271);
            btRegenBio.Name = "btRegenBio";
            btRegenBio.Size = new Size(190, 27);
            btRegenBio.TabIndex = 16;
            btRegenBio.Text = "Regenerate";
            btRegenBio.UseVisualStyleBackColor = true;
            btRegenBio.Click += btRegenBio_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9F);
            label13.Location = new Point(15, 257);
            label13.Name = "label13";
            label13.Size = new Size(170, 15);
            label13.TabIndex = 15;
            label13.Text = "Maximum tokens (0 to disable)";
            // 
            // num_selfedittokens
            // 
            num_selfedittokens.BackColor = Color.FromArgb(64, 64, 64);
            num_selfedittokens.BorderStyle = BorderStyle.FixedSingle;
            num_selfedittokens.Font = new Font("Segoe UI", 9F);
            num_selfedittokens.Increment = new decimal(new int[] { 128, 0, 0, 0 });
            num_selfedittokens.Location = new Point(15, 275);
            num_selfedittokens.Maximum = new decimal(new int[] { 2048, 0, 0, 0 });
            num_selfedittokens.Name = "num_selfedittokens";
            num_selfedittokens.Padding = new Padding(1);
            num_selfedittokens.Size = new Size(190, 23);
            num_selfedittokens.TabIndex = 14;
            // 
            // ed_selfedit
            // 
            ed_selfedit.BorderStyle = BorderStyle.FixedSingle;
            ed_selfedit.Font = new Font("Segoe UI", 9F);
            ed_selfedit.Location = new Point(15, 50);
            ed_selfedit.Multiline = true;
            ed_selfedit.Name = "ed_selfedit";
            ed_selfedit.ReadOnly = true;
            ed_selfedit.ScrollBars = ScrollBars.Vertical;
            ed_selfedit.Size = new Size(410, 204);
            ed_selfedit.TabIndex = 13;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F);
            label12.Location = new Point(15, 32);
            label12.Name = "label12";
            label12.Size = new Size(373, 15);
            label12.TabIndex = 0;
            label12.Text = "Optional system prompt field (char allowed to edit after each session)";
            // 
            // num_ptvalue
            // 
            num_ptvalue.BackColor = Color.FromArgb(64, 64, 64);
            num_ptvalue.BorderStyle = BorderStyle.FixedSingle;
            num_ptvalue.Font = new Font("Segoe UI", 9F);
            num_ptvalue.Location = new Point(305, 250);
            num_ptvalue.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            num_ptvalue.Minimum = new decimal(new int[] { 10000000, 0, 0, int.MinValue });
            num_ptvalue.Name = "num_ptvalue";
            num_ptvalue.Padding = new Padding(1);
            num_ptvalue.Size = new Size(120, 23);
            num_ptvalue.TabIndex = 4;
            // 
            // cb_pointsystems
            // 
            cb_pointsystems.BackColor = Color.FromArgb(64, 64, 64);
            cb_pointsystems.DropDownHeight = 180;
            cb_pointsystems.Font = new Font("Segoe UI", 9.25F);
            cb_pointsystems.Location = new Point(15, 250);
            cb_pointsystems.MaxDropDownItems = 10;
            cb_pointsystems.Name = "cb_pointsystems";
            cb_pointsystems.Padding = new Padding(1);
            cb_pointsystems.Size = new Size(284, 23);
            cb_pointsystems.TabIndex = 3;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9F);
            label14.Location = new Point(15, 232);
            label14.Name = "label14";
            label14.Size = new Size(235, 15);
            label14.TabIndex = 1;
            label14.Text = "Optional point system (/pointhelp for help)";
            // 
            // ed_outetts
            // 
            ed_outetts.BorderStyle = BorderStyle.FixedSingle;
            ed_outetts.Font = new Font("Segoe UI", 9F);
            ed_outetts.Location = new Point(12, 589);
            ed_outetts.Name = "ed_outetts";
            ed_outetts.Size = new Size(455, 23);
            ed_outetts.TabIndex = 2;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label11.Location = new Point(12, 571);
            label11.Name = "label11";
            label11.Size = new Size(257, 15);
            label11.TabIndex = 1;
            label11.Text = "Voice ID for KoboldCPP's Text-To-Speech API";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F);
            label9.Location = new Point(15, 32);
            label9.Name = "label9";
            label9.Size = new Size(248, 15);
            label9.TabIndex = 0;
            label9.Text = "Plugins this character has access to by default";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F);
            label8.Location = new Point(144, 165);
            label8.Name = "label8";
            label8.Size = new Size(159, 15);
            label8.TabIndex = 10;
            label8.Text = "Hours AFK before time insert";
            // 
            // num_minAFK
            // 
            num_minAFK.BackColor = Color.FromArgb(64, 64, 64);
            num_minAFK.BorderStyle = BorderStyle.FixedSingle;
            num_minAFK.DecimalPlaces = 2;
            num_minAFK.Font = new Font("Segoe UI", 9F);
            num_minAFK.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            num_minAFK.Location = new Point(44, 163);
            num_minAFK.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            num_minAFK.Minimum = new decimal(new int[] { 25, 0, 0, 131072 });
            num_minAFK.Name = "num_minAFK";
            num_minAFK.Padding = new Padding(1);
            num_minAFK.Size = new Size(94, 23);
            num_minAFK.TabIndex = 9;
            num_minAFK.Value = new decimal(new int[] { 125, 0, 0, 131072 });
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 9F);
            label17.Location = new Point(138, 188);
            label17.Name = "label17";
            label17.Size = new Size(156, 15);
            label17.TabIndex = 17;
            label17.Text = "Prune messages after X days";
            // 
            // numKeepEurekas
            // 
            numKeepEurekas.BackColor = Color.FromArgb(64, 64, 64);
            numKeepEurekas.Font = new Font("Segoe UI", 9F);
            numKeepEurekas.Location = new Point(38, 186);
            numKeepEurekas.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numKeepEurekas.Name = "numKeepEurekas";
            numKeepEurekas.Padding = new Padding(1);
            numKeepEurekas.Size = new Size(94, 23);
            numKeepEurekas.TabIndex = 16;
            numKeepEurekas.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numEurekaMinTime
            // 
            numEurekaMinTime.BackColor = Color.FromArgb(64, 64, 64);
            numEurekaMinTime.DecimalPlaces = 2;
            numEurekaMinTime.Font = new Font("Segoe UI", 9F);
            numEurekaMinTime.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            numEurekaMinTime.Location = new Point(38, 157);
            numEurekaMinTime.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numEurekaMinTime.Name = "numEurekaMinTime";
            numEurekaMinTime.Padding = new Padding(1);
            numEurekaMinTime.Size = new Size(94, 23);
            numEurekaMinTime.TabIndex = 15;
            numEurekaMinTime.Value = new decimal(new int[] { 125, 0, 0, 131072 });
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9F);
            label16.Location = new Point(138, 159);
            label16.Name = "label16";
            label16.Size = new Size(230, 15);
            label16.TabIndex = 14;
            label16.Text = "Min time spent between messages (hours)";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F);
            label15.Location = new Point(138, 130);
            label15.Name = "label15";
            label15.Size = new Size(233, 15);
            label15.TabIndex = 12;
            label15.Text = "Minimum posts between system messages";
            // 
            // numEurekaMinMess
            // 
            numEurekaMinMess.BackColor = Color.FromArgb(64, 64, 64);
            numEurekaMinMess.Font = new Font("Segoe UI", 9F);
            numEurekaMinMess.Location = new Point(38, 128);
            numEurekaMinMess.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numEurekaMinMess.Name = "numEurekaMinMess";
            numEurekaMinMess.Padding = new Padding(1);
            numEurekaMinMess.Size = new Size(94, 23);
            numEurekaMinMess.TabIndex = 11;
            numEurekaMinMess.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // pic
            // 
            pic.Location = new Point(630, 12);
            pic.Name = "pic";
            pic.Size = new Size(54, 50);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 16;
            pic.TabStop = false;
            // 
            // cb_icon
            // 
            cb_icon.BackColor = Color.FromArgb(64, 64, 64);
            cb_icon.DropDownHeight = 180;
            cb_icon.Font = new Font("Segoe UI", 9.25F);
            cb_icon.Location = new Point(690, 30);
            cb_icon.MaxDropDownItems = 10;
            cb_icon.Name = "cb_icon";
            cb_icon.Padding = new Padding(1);
            cb_icon.Size = new Size(205, 23);
            cb_icon.TabIndex = 15;
            cb_icon.SelectedIndexChanged += cb_icon_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(488, 431);
            label4.Name = "label4";
            label4.Size = new Size(283, 15);
            label4.TabIndex = 14;
            label4.Text = "First Message (one per line, use \\n for paragraphs)";
            // 
            // ed_firstmessage
            // 
            ed_firstmessage.BorderStyle = BorderStyle.FixedSingle;
            ed_firstmessage.Location = new Point(488, 449);
            ed_firstmessage.Multiline = true;
            ed_firstmessage.Name = "ed_firstmessage";
            ed_firstmessage.ScrollBars = ScrollBars.Vertical;
            ed_firstmessage.Size = new Size(407, 119);
            ed_firstmessage.TabIndex = 13;
            // 
            // ed_scenario
            // 
            ed_scenario.BorderStyle = BorderStyle.FixedSingle;
            ed_scenario.Location = new Point(12, 449);
            ed_scenario.Multiline = true;
            ed_scenario.Name = "ed_scenario";
            ed_scenario.ScrollBars = ScrollBars.Vertical;
            ed_scenario.Size = new Size(455, 119);
            ed_scenario.TabIndex = 12;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(12, 431);
            label3.Name = "label3";
            label3.Size = new Size(100, 15);
            label3.TabIndex = 11;
            label3.Text = "Default Scenario";
            // 
            // ed_bio
            // 
            ed_bio.BorderStyle = BorderStyle.FixedSingle;
            ed_bio.Location = new Point(12, 74);
            ed_bio.Multiline = true;
            ed_bio.Name = "ed_bio";
            ed_bio.ScrollBars = ScrollBars.Vertical;
            ed_bio.Size = new Size(883, 267);
            ed_bio.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(12, 56);
            label2.Name = "label2";
            label2.Size = new Size(138, 15);
            label2.TabIndex = 9;
            label2.Text = "Biography / Description";
            // 
            // ck_isuser
            // 
            ck_isuser.AutoSize = true;
            ck_isuser.Location = new Point(373, 32);
            ck_isuser.Name = "ck_isuser";
            ck_isuser.Size = new Size(94, 19);
            ck_isuser.TabIndex = 8;
            ck_isuser.Text = "User Persona";
            ck_isuser.UseVisualStyleBackColor = true;
            // 
            // ed_name
            // 
            ed_name.BorderStyle = BorderStyle.FixedSingle;
            ed_name.Location = new Point(12, 30);
            ed_name.Name = "ed_name";
            ed_name.Size = new Size(355, 23);
            ed_name.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 6;
            label1.Text = "Name";
            // 
            // ed_writingstyle
            // 
            ed_writingstyle.BorderStyle = BorderStyle.FixedSingle;
            ed_writingstyle.Font = new Font("Segoe UI", 9F);
            ed_writingstyle.Location = new Point(15, 50);
            ed_writingstyle.Multiline = true;
            ed_writingstyle.Name = "ed_writingstyle";
            ed_writingstyle.ScrollBars = ScrollBars.Vertical;
            ed_writingstyle.Size = new Size(856, 183);
            ed_writingstyle.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F);
            label7.Location = new Point(14, 32);
            label7.Name = "label7";
            label7.Size = new Size(327, 15);
            label7.TabIndex = 0;
            label7.Text = "Optional list of writing style instructions (or dialog examples)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F);
            label5.Location = new Point(15, 32);
            label5.Name = "label5";
            label5.Size = new Size(331, 15);
            label5.TabIndex = 0;
            label5.Text = "Select favorite samplers for random sampler mode. (optional)";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F);
            label6.Location = new Point(15, 32);
            label6.Name = "label6";
            label6.Size = new Size(361, 15);
            label6.TabIndex = 0;
            label6.Text = "Select the world info files this character is allowed to use. (optional)";
            // 
            // ed_sysprompt
            // 
            ed_sysprompt.BackColor = Color.Gray;
            ed_sysprompt.BorderStyle = BorderStyle.FixedSingle;
            ed_sysprompt.Dock = DockStyle.Fill;
            ed_sysprompt.Location = new Point(0, 139);
            ed_sysprompt.Multiline = true;
            ed_sysprompt.Name = "ed_sysprompt";
            ed_sysprompt.PlaceholderText = "Optional System Prompt";
            ed_sysprompt.ScrollBars = ScrollBars.Vertical;
            ed_sysprompt.Size = new Size(917, 479);
            ed_sysprompt.TabIndex = 13;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.SlateGray;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Dock = DockStyle.Top;
            textBox2.ForeColor = Color.Black;
            textBox2.Location = new Point(0, 0);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(917, 139);
            textBox2.TabIndex = 1;
            textBox2.Tag = "no-theme";
            textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 9F);
            label19.Location = new Point(138, 69);
            label19.Name = "label19";
            label19.Size = new Size(174, 15);
            label19.TabIndex = 12;
            label19.Text = "Hours AFK before running tasks";
            // 
            // numAgentDelay
            // 
            numAgentDelay.BackColor = Color.FromArgb(64, 64, 64);
            numAgentDelay.DecimalPlaces = 2;
            numAgentDelay.Font = new Font("Segoe UI", 9F);
            numAgentDelay.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            numAgentDelay.Location = new Point(38, 67);
            numAgentDelay.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numAgentDelay.Minimum = new decimal(new int[] { 25, 0, 0, 131072 });
            numAgentDelay.Name = "numAgentDelay";
            numAgentDelay.Padding = new Padding(1);
            numAgentDelay.Size = new Size(94, 23);
            numAgentDelay.TabIndex = 11;
            numAgentDelay.Value = new decimal(new int[] { 125, 0, 0, 131072 });
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 9F);
            label18.Location = new Point(15, 32);
            label18.Name = "label18";
            label18.Size = new Size(351, 15);
            label18.TabIndex = 0;
            label18.Text = "Select the tasks this character is allowed to run in the background";
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(cb_charlist);
            panel1.Controls.Add(edFilename);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(btSave);
            panel1.Controls.Add(panel4);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(234, 662);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 586);
            panel2.Margin = new Padding(8);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(8);
            panel2.Size = new Size(234, 10);
            panel2.TabIndex = 48;
            // 
            // cb_charlist
            // 
            cb_charlist.AlwaysScrollbar = true;
            cb_charlist.BackColor = Color.FromArgb(64, 64, 64);
            cb_charlist.Dock = DockStyle.Fill;
            cb_charlist.Font = new Font("Segoe UI", 9.25F);
            cb_charlist.Location = new Point(0, 0);
            cb_charlist.Name = "cb_charlist";
            cb_charlist.Padding = new Padding(1);
            cb_charlist.Size = new Size(234, 578);
            cb_charlist.TabIndex = 4;
            // 
            // edFilename
            // 
            edFilename.BorderStyle = BorderStyle.FixedSingle;
            edFilename.Dock = DockStyle.Bottom;
            edFilename.Location = new Point(0, 596);
            edFilename.Name = "edFilename";
            edFilename.Size = new Size(234, 23);
            edFilename.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 619);
            panel3.Margin = new Padding(8);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(8);
            panel3.Size = new Size(234, 10);
            panel3.TabIndex = 49;
            // 
            // btSave
            // 
            btSave.BackColor = Color.DarkSeaGreen;
            btSave.Dock = DockStyle.Bottom;
            btSave.FlatStyle = FlatStyle.Flat;
            btSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btSave.Location = new Point(0, 629);
            btSave.Name = "btSave";
            btSave.Size = new Size(234, 23);
            btSave.TabIndex = 3;
            btSave.Tag = "no-theme";
            btSave.Text = "Save Character";
            btSave.UseVisualStyleBackColor = false;
            btSave.Click += bt_worldsave_Click;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 652);
            panel4.Margin = new Padding(8);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(8);
            panel4.Size = new Size(234, 10);
            panel4.TabIndex = 50;
            // 
            // TabControl
            // 
            TabControl.Appearance = TabAppearance.Buttons;
            TabControl.Controls.Add(tbGeneral);
            TabControl.Controls.Add(tbStyle);
            TabControl.Controls.Add(tbPrompt);
            TabControl.Controls.Add(tbAgent);
            TabControl.Controls.Add(tbSettings);
            TabControl.Controls.Add(tabMood);
            TabControl.Controls.Add(tabSchedule);
            TabControl.Dock = DockStyle.Fill;
            TabControl.Font = new Font("Segoe UI", 9F);
            TabControl.ItemSize = new Size(0, 36);
            TabControl.Location = new Point(234, 0);
            TabControl.Name = "TabControl";
            TabControl.SelectedIndex = 0;
            TabControl.Size = new Size(925, 662);
            TabControl.TabIndex = 5;
            // 
            // tbGeneral
            // 
            tbGeneral.BackColor = Color.FromArgb(37, 37, 37);
            tbGeneral.Controls.Add(label27);
            tbGeneral.Controls.Add(edMiniBio);
            tbGeneral.Controls.Add(ckNoGuidance);
            tbGeneral.Controls.Add(ed_outetts);
            tbGeneral.Controls.Add(label4);
            tbGeneral.Controls.Add(label11);
            tbGeneral.Controls.Add(pic);
            tbGeneral.Controls.Add(ed_firstmessage);
            tbGeneral.Controls.Add(label1);
            tbGeneral.Controls.Add(ed_scenario);
            tbGeneral.Controls.Add(cb_icon);
            tbGeneral.Controls.Add(label3);
            tbGeneral.Controls.Add(ed_name);
            tbGeneral.Controls.Add(ck_isuser);
            tbGeneral.Controls.Add(label2);
            tbGeneral.Controls.Add(ed_bio);
            tbGeneral.Font = new Font("Segoe UI", 9F);
            tbGeneral.ForeColor = Color.FromArgb(230, 230, 230);
            tbGeneral.Location = new Point(4, 40);
            tbGeneral.Name = "tbGeneral";
            tbGeneral.Padding = new Padding(3);
            tbGeneral.Size = new Size(917, 618);
            tbGeneral.TabIndex = 0;
            tbGeneral.Text = "General";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label27.Location = new Point(12, 344);
            label27.Name = "label27";
            label27.Size = new Size(301, 15);
            label27.TabIndex = 19;
            label27.Text = "Mini Bio (used for group chat and /charlist command)";
            // 
            // edMiniBio
            // 
            edMiniBio.BorderStyle = BorderStyle.FixedSingle;
            edMiniBio.Location = new Point(12, 362);
            edMiniBio.Multiline = true;
            edMiniBio.Name = "edMiniBio";
            edMiniBio.ScrollBars = ScrollBars.Vertical;
            edMiniBio.Size = new Size(883, 66);
            edMiniBio.TabIndex = 18;
            // 
            // ckNoGuidance
            // 
            ckNoGuidance.AutoSize = true;
            ckNoGuidance.Location = new Point(488, 31);
            ckNoGuidance.Name = "ckNoGuidance";
            ckNoGuidance.Size = new Size(116, 19);
            ckNoGuidance.TabIndex = 17;
            ckNoGuidance.Text = "No Bot Guidance";
            ckNoGuidance.UseVisualStyleBackColor = true;
            // 
            // tbStyle
            // 
            tbStyle.BackColor = Color.FromArgb(37, 37, 37);
            tbStyle.Controls.Add(collapsibleGroupBox3);
            tbStyle.Controls.Add(collapsibleGroupBox2);
            tbStyle.Controls.Add(collapsibleGroupBox1);
            tbStyle.Font = new Font("Segoe UI", 9F);
            tbStyle.ForeColor = Color.FromArgb(230, 230, 230);
            tbStyle.Location = new Point(4, 40);
            tbStyle.Name = "tbStyle";
            tbStyle.Size = new Size(917, 618);
            tbStyle.TabIndex = 1;
            tbStyle.Text = "Style";
            // 
            // collapsibleGroupBox3
            // 
            collapsibleGroupBox3.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox3.CanCollapse = false;
            collapsibleGroupBox3.Controls.Add(ed_writingstyle);
            collapsibleGroupBox3.Controls.Add(label7);
            collapsibleGroupBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox3.Location = new Point(12, 337);
            collapsibleGroupBox3.Name = "collapsibleGroupBox3";
            collapsibleGroupBox3.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox3.Size = new Size(886, 252);
            collapsibleGroupBox3.TabIndex = 2;
            collapsibleGroupBox3.Text = "Writing Guidance";
            // 
            // collapsibleGroupBox2
            // 
            collapsibleGroupBox2.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox2.CanCollapse = false;
            collapsibleGroupBox2.Controls.Add(ckl_samplers);
            collapsibleGroupBox2.Controls.Add(label5);
            collapsibleGroupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox2.Location = new Point(458, 19);
            collapsibleGroupBox2.Name = "collapsibleGroupBox2";
            collapsibleGroupBox2.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox2.Size = new Size(440, 312);
            collapsibleGroupBox2.TabIndex = 1;
            collapsibleGroupBox2.Text = "Favoite Inference Settings";
            // 
            // ckl_samplers
            // 
            ckl_samplers.BackColor = Color.FromArgb(64, 64, 64);
            ckl_samplers.Font = new Font("Segoe UI", 9.25F);
            ckl_samplers.Location = new Point(15, 50);
            ckl_samplers.Name = "ckl_samplers";
            ckl_samplers.Padding = new Padding(1);
            ckl_samplers.Size = new Size(410, 258);
            ckl_samplers.TabIndex = 1;
            // 
            // collapsibleGroupBox1
            // 
            collapsibleGroupBox1.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox1.CanCollapse = false;
            collapsibleGroupBox1.Controls.Add(ckl_worldinfo);
            collapsibleGroupBox1.Controls.Add(label6);
            collapsibleGroupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox1.Location = new Point(12, 19);
            collapsibleGroupBox1.Name = "collapsibleGroupBox1";
            collapsibleGroupBox1.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox1.Size = new Size(440, 312);
            collapsibleGroupBox1.TabIndex = 0;
            collapsibleGroupBox1.Text = "World Info";
            // 
            // ckl_worldinfo
            // 
            ckl_worldinfo.BackColor = Color.FromArgb(64, 64, 64);
            ckl_worldinfo.Font = new Font("Segoe UI", 9.25F);
            ckl_worldinfo.Location = new Point(15, 50);
            ckl_worldinfo.Name = "ckl_worldinfo";
            ckl_worldinfo.Padding = new Padding(1);
            ckl_worldinfo.Size = new Size(410, 258);
            ckl_worldinfo.TabIndex = 1;
            // 
            // tbPrompt
            // 
            tbPrompt.BackColor = Color.FromArgb(37, 37, 37);
            tbPrompt.Controls.Add(ed_sysprompt);
            tbPrompt.Controls.Add(textBox2);
            tbPrompt.Font = new Font("Segoe UI", 9F);
            tbPrompt.ForeColor = Color.FromArgb(230, 230, 230);
            tbPrompt.Location = new Point(4, 40);
            tbPrompt.Name = "tbPrompt";
            tbPrompt.Size = new Size(917, 618);
            tbPrompt.TabIndex = 2;
            tbPrompt.Text = "System Prompt";
            // 
            // tbAgent
            // 
            tbAgent.BackColor = Color.FromArgb(37, 37, 37);
            tbAgent.Controls.Add(collapsibleGroupBox8);
            tbAgent.Controls.Add(collapsibleGroupBox7);
            tbAgent.Controls.Add(collapsibleGroupBox6);
            tbAgent.Controls.Add(collapsibleGroupBox5);
            tbAgent.Font = new Font("Segoe UI", 9F);
            tbAgent.ForeColor = Color.FromArgb(230, 230, 230);
            tbAgent.Location = new Point(4, 40);
            tbAgent.Name = "tbAgent";
            tbAgent.Size = new Size(917, 618);
            tbAgent.TabIndex = 3;
            tbAgent.Text = "Background Agent";
            // 
            // collapsibleGroupBox8
            // 
            collapsibleGroupBox8.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox8.CanCollapse = false;
            collapsibleGroupBox8.Controls.Add(listNoRAGMemTypes);
            collapsibleGroupBox8.Controls.Add(label23);
            collapsibleGroupBox8.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox8.Location = new Point(12, 324);
            collapsibleGroupBox8.Name = "collapsibleGroupBox8";
            collapsibleGroupBox8.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox8.Size = new Size(440, 288);
            collapsibleGroupBox8.TabIndex = 3;
            collapsibleGroupBox8.Text = "Memories: No RAG";
            // 
            // listNoRAGMemTypes
            // 
            listNoRAGMemTypes.BackColor = Color.FromArgb(64, 64, 64);
            listNoRAGMemTypes.Font = new Font("Segoe UI", 9.25F);
            listNoRAGMemTypes.Location = new Point(15, 50);
            listNoRAGMemTypes.Name = "listNoRAGMemTypes";
            listNoRAGMemTypes.Padding = new Padding(1);
            listNoRAGMemTypes.Size = new Size(410, 226);
            listNoRAGMemTypes.TabIndex = 18;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Segoe UI", 9F);
            label23.Location = new Point(15, 32);
            label23.Name = "label23";
            label23.Size = new Size(289, 15);
            label23.TabIndex = 17;
            label23.Text = "Memory types not allowed in the general RAG system";
            // 
            // collapsibleGroupBox7
            // 
            collapsibleGroupBox7.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox7.CanCollapse = false;
            collapsibleGroupBox7.Controls.Add(listCanDecay);
            collapsibleGroupBox7.Controls.Add(label22);
            collapsibleGroupBox7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox7.Location = new Point(458, 324);
            collapsibleGroupBox7.Name = "collapsibleGroupBox7";
            collapsibleGroupBox7.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox7.Size = new Size(440, 288);
            collapsibleGroupBox7.TabIndex = 2;
            collapsibleGroupBox7.Text = "Memories: Decay Allowed";
            // 
            // listCanDecay
            // 
            listCanDecay.BackColor = Color.FromArgb(64, 64, 64);
            listCanDecay.Font = new Font("Segoe UI", 9.25F);
            listCanDecay.Location = new Point(15, 50);
            listCanDecay.Name = "listCanDecay";
            listCanDecay.Padding = new Padding(1);
            listCanDecay.Size = new Size(410, 226);
            listCanDecay.TabIndex = 19;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Segoe UI", 9F);
            label22.Location = new Point(15, 32);
            label22.Name = "label22";
            label22.Size = new Size(378, 15);
            label22.TabIndex = 17;
            label22.Text = "Memories of those types will decay (ang get pruned) when left unused";
            // 
            // collapsibleGroupBox6
            // 
            collapsibleGroupBox6.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox6.CanCollapse = false;
            collapsibleGroupBox6.Controls.Add(ckAllowEurekas);
            collapsibleGroupBox6.Controls.Add(label17);
            collapsibleGroupBox6.Controls.Add(ckAgent);
            collapsibleGroupBox6.Controls.Add(numKeepEurekas);
            collapsibleGroupBox6.Controls.Add(numAgentDelay);
            collapsibleGroupBox6.Controls.Add(numEurekaMinTime);
            collapsibleGroupBox6.Controls.Add(label19);
            collapsibleGroupBox6.Controls.Add(label16);
            collapsibleGroupBox6.Controls.Add(label15);
            collapsibleGroupBox6.Controls.Add(numEurekaMinMess);
            collapsibleGroupBox6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox6.Location = new Point(458, 19);
            collapsibleGroupBox6.Name = "collapsibleGroupBox6";
            collapsibleGroupBox6.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox6.Size = new Size(440, 299);
            collapsibleGroupBox6.TabIndex = 1;
            collapsibleGroupBox6.Text = "Background Agent Settings";
            // 
            // ckAllowEurekas
            // 
            ckAllowEurekas.Font = new Font("Segoe UI", 9F);
            ckAllowEurekas.Location = new Point(15, 96);
            ckAllowEurekas.Name = "ckAllowEurekas";
            ckAllowEurekas.Size = new Size(410, 26);
            ckAllowEurekas.TabIndex = 18;
            ckAllowEurekas.Text = "Allow tasks to insert system messages as \"memories\"";
            ckAllowEurekas.UseVisualStyleBackColor = true;
            // 
            // ckAgent
            // 
            ckAgent.Font = new Font("Segoe UI", 9F);
            ckAgent.Location = new Point(15, 35);
            ckAgent.Name = "ckAgent";
            ckAgent.Size = new Size(410, 26);
            ckAgent.TabIndex = 13;
            ckAgent.Text = "Allow the character to run as an agent";
            ckAgent.UseVisualStyleBackColor = true;
            // 
            // collapsibleGroupBox5
            // 
            collapsibleGroupBox5.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox5.CanCollapse = false;
            collapsibleGroupBox5.Controls.Add(listAgentTasks);
            collapsibleGroupBox5.Controls.Add(label18);
            collapsibleGroupBox5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox5.Location = new Point(12, 19);
            collapsibleGroupBox5.Name = "collapsibleGroupBox5";
            collapsibleGroupBox5.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox5.Size = new Size(440, 299);
            collapsibleGroupBox5.TabIndex = 0;
            collapsibleGroupBox5.Text = "Background Tasks";
            // 
            // listAgentTasks
            // 
            listAgentTasks.BackColor = Color.FromArgb(64, 64, 64);
            listAgentTasks.Font = new Font("Segoe UI", 9.25F);
            listAgentTasks.Location = new Point(15, 52);
            listAgentTasks.Name = "listAgentTasks";
            listAgentTasks.Padding = new Padding(1);
            listAgentTasks.Size = new Size(410, 226);
            listAgentTasks.TabIndex = 1;
            // 
            // tbSettings
            // 
            tbSettings.BackColor = Color.FromArgb(37, 37, 37);
            tbSettings.Controls.Add(collapsibleGroupBox11);
            tbSettings.Controls.Add(collapsibleGroupBox10);
            tbSettings.Controls.Add(collapsibleGroupBox9);
            tbSettings.Controls.Add(collapsibleGroupBox4);
            tbSettings.Font = new Font("Segoe UI", 9F);
            tbSettings.ForeColor = Color.FromArgb(230, 230, 230);
            tbSettings.Location = new Point(4, 40);
            tbSettings.Name = "tbSettings";
            tbSettings.Size = new Size(917, 618);
            tbSettings.TabIndex = 4;
            tbSettings.Text = "Settings";
            // 
            // collapsibleGroupBox11
            // 
            collapsibleGroupBox11.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox11.CanCollapse = false;
            collapsibleGroupBox11.Controls.Add(ckTimerAddDuration);
            collapsibleGroupBox11.Controls.Add(ckTimerActive);
            collapsibleGroupBox11.Controls.Add(ckTimerAutoExpire);
            collapsibleGroupBox11.Controls.Add(label30);
            collapsibleGroupBox11.Controls.Add(edTimerExpired);
            collapsibleGroupBox11.Controls.Add(label29);
            collapsibleGroupBox11.Controls.Add(edTimerOff);
            collapsibleGroupBox11.Controls.Add(label28);
            collapsibleGroupBox11.Controls.Add(edTimerOn);
            collapsibleGroupBox11.Controls.Add(ckTimerEnable);
            collapsibleGroupBox11.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox11.Location = new Point(458, 312);
            collapsibleGroupBox11.Name = "collapsibleGroupBox11";
            collapsibleGroupBox11.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox11.Size = new Size(440, 298);
            collapsibleGroupBox11.TabIndex = 4;
            collapsibleGroupBox11.Text = "Timer Plugin";
            // 
            // ckTimerAddDuration
            // 
            ckTimerAddDuration.Font = new Font("Segoe UI", 9F);
            ckTimerAddDuration.Location = new Point(15, 266);
            ckTimerAddDuration.Name = "ckTimerAddDuration";
            ckTimerAddDuration.Size = new Size(410, 26);
            ckTimerAddDuration.TabIndex = 12;
            ckTimerAddDuration.Text = "Insert time spent in message";
            ckTimerAddDuration.UseVisualStyleBackColor = true;
            // 
            // ckTimerActive
            // 
            ckTimerActive.Font = new Font("Segoe UI", 9F);
            ckTimerActive.Location = new Point(15, 234);
            ckTimerActive.Name = "ckTimerActive";
            ckTimerActive.Size = new Size(410, 26);
            ckTimerActive.TabIndex = 11;
            ckTimerActive.Text = "Timer Active";
            ckTimerActive.UseVisualStyleBackColor = true;
            // 
            // ckTimerAutoExpire
            // 
            ckTimerAutoExpire.Font = new Font("Segoe UI", 9F);
            ckTimerAutoExpire.Location = new Point(15, 204);
            ckTimerAutoExpire.Name = "ckTimerAutoExpire";
            ckTimerAutoExpire.Size = new Size(410, 26);
            ckTimerAutoExpire.TabIndex = 10;
            ckTimerAutoExpire.Text = "Automatic timer expiration on duration over";
            ckTimerAutoExpire.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new Font("Segoe UI", 9F);
            label30.Location = new Point(15, 157);
            label30.Name = "label30";
            label30.Size = new Size(158, 15);
            label30.TabIndex = 9;
            label30.Text = "Satus Message: Time expired";
            // 
            // edTimerExpired
            // 
            edTimerExpired.BorderStyle = BorderStyle.FixedSingle;
            edTimerExpired.Font = new Font("Segoe UI", 9F);
            edTimerExpired.Location = new Point(15, 175);
            edTimerExpired.Name = "edTimerExpired";
            edTimerExpired.Size = new Size(410, 23);
            edTimerExpired.TabIndex = 8;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("Segoe UI", 9F);
            label29.Location = new Point(15, 113);
            label29.Name = "label29";
            label29.Size = new Size(176, 15);
            label29.TabIndex = 7;
            label29.Text = "Status Message: Timenr inactive";
            // 
            // edTimerOff
            // 
            edTimerOff.BorderStyle = BorderStyle.FixedSingle;
            edTimerOff.Font = new Font("Segoe UI", 9F);
            edTimerOff.Location = new Point(15, 131);
            edTimerOff.Name = "edTimerOff";
            edTimerOff.Size = new Size(410, 23);
            edTimerOff.TabIndex = 6;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("Segoe UI", 9F);
            label28.Location = new Point(15, 69);
            label28.Name = "label28";
            label28.Size = new Size(170, 15);
            label28.TabIndex = 5;
            label28.Text = "Status Message: Timer running";
            // 
            // edTimerOn
            // 
            edTimerOn.BorderStyle = BorderStyle.FixedSingle;
            edTimerOn.Font = new Font("Segoe UI", 9F);
            edTimerOn.Location = new Point(15, 87);
            edTimerOn.Name = "edTimerOn";
            edTimerOn.Size = new Size(410, 23);
            edTimerOn.TabIndex = 4;
            // 
            // ckTimerEnable
            // 
            ckTimerEnable.Font = new Font("Segoe UI", 9F);
            ckTimerEnable.Location = new Point(15, 35);
            ckTimerEnable.Name = "ckTimerEnable";
            ckTimerEnable.Size = new Size(410, 26);
            ckTimerEnable.TabIndex = 1;
            ckTimerEnable.Text = "Enabled";
            ckTimerEnable.UseVisualStyleBackColor = true;
            // 
            // collapsibleGroupBox10
            // 
            collapsibleGroupBox10.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox10.CanCollapse = false;
            collapsibleGroupBox10.Controls.Add(btRegenBio);
            collapsibleGroupBox10.Controls.Add(label12);
            collapsibleGroupBox10.Controls.Add(label13);
            collapsibleGroupBox10.Controls.Add(num_selfedittokens);
            collapsibleGroupBox10.Controls.Add(ed_selfedit);
            collapsibleGroupBox10.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox10.Location = new Point(12, 312);
            collapsibleGroupBox10.Name = "collapsibleGroupBox10";
            collapsibleGroupBox10.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox10.Size = new Size(440, 298);
            collapsibleGroupBox10.TabIndex = 3;
            collapsibleGroupBox10.Text = "Character's Personal Field";
            // 
            // collapsibleGroupBox9
            // 
            collapsibleGroupBox9.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox9.CanCollapse = false;
            collapsibleGroupBox9.Controls.Add(ckl_plugins);
            collapsibleGroupBox9.Controls.Add(label9);
            collapsibleGroupBox9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox9.Location = new Point(458, 19);
            collapsibleGroupBox9.Name = "collapsibleGroupBox9";
            collapsibleGroupBox9.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox9.Size = new Size(440, 287);
            collapsibleGroupBox9.TabIndex = 2;
            collapsibleGroupBox9.Text = "Context Plugins";
            // 
            // ckl_plugins
            // 
            ckl_plugins.BackColor = Color.FromArgb(64, 64, 64);
            ckl_plugins.Font = new Font("Segoe UI", 9.25F);
            ckl_plugins.Location = new Point(15, 50);
            ckl_plugins.Name = "ckl_plugins";
            ckl_plugins.Padding = new Padding(1);
            ckl_plugins.Size = new Size(410, 226);
            ckl_plugins.TabIndex = 1;
            // 
            // collapsibleGroupBox4
            // 
            collapsibleGroupBox4.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox4.CanCollapse = false;
            collapsibleGroupBox4.Controls.Add(num_ptvalue);
            collapsibleGroupBox4.Controls.Add(label8);
            collapsibleGroupBox4.Controls.Add(ckPassword);
            collapsibleGroupBox4.Controls.Add(cb_pointsystems);
            collapsibleGroupBox4.Controls.Add(label14);
            collapsibleGroupBox4.Controls.Add(num_minAFK);
            collapsibleGroupBox4.Controls.Add(ck_senseoftime);
            collapsibleGroupBox4.Controls.Add(ck_irldates);
            collapsibleGroupBox4.Controls.Add(ckMoodSystem);
            collapsibleGroupBox4.Controls.Add(ck_caninitchat);
            collapsibleGroupBox4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox4.Location = new Point(12, 19);
            collapsibleGroupBox4.Name = "collapsibleGroupBox4";
            collapsibleGroupBox4.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox4.Size = new Size(440, 287);
            collapsibleGroupBox4.TabIndex = 1;
            collapsibleGroupBox4.Text = "General Settings";
            // 
            // ckPassword
            // 
            ckPassword.FlatStyle = FlatStyle.Flat;
            ckPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic);
            ckPassword.ForeColor = Color.Red;
            ckPassword.Location = new Point(15, 192);
            ckPassword.Name = "ckPassword";
            ckPassword.Size = new Size(410, 26);
            ckPassword.TabIndex = 4;
            ckPassword.Tag = "no-theme";
            ckPassword.Text = "Password Protection (no recovery available if lost)";
            ckPassword.UseVisualStyleBackColor = true;
            // 
            // ck_senseoftime
            // 
            ck_senseoftime.Font = new Font("Segoe UI", 9F);
            ck_senseoftime.Location = new Point(15, 131);
            ck_senseoftime.Name = "ck_senseoftime";
            ck_senseoftime.Size = new Size(410, 26);
            ck_senseoftime.TabIndex = 3;
            ck_senseoftime.Text = "Sense of Time (characer knows IRL time and how long you're AFK)";
            ck_senseoftime.UseVisualStyleBackColor = true;
            // 
            // ck_irldates
            // 
            ck_irldates.Font = new Font("Segoe UI", 9F);
            ck_irldates.Location = new Point(15, 99);
            ck_irldates.Name = "ck_irldates";
            ck_irldates.Size = new Size(410, 26);
            ck_irldates.TabIndex = 2;
            ck_irldates.Text = "Include IRL dates in summaries (disable if this is a roleplay character)";
            ck_irldates.UseVisualStyleBackColor = true;
            // 
            // ckMoodSystem
            // 
            ckMoodSystem.Font = new Font("Segoe UI", 9F);
            ckMoodSystem.Location = new Point(15, 67);
            ckMoodSystem.Name = "ckMoodSystem";
            ckMoodSystem.Size = new Size(410, 26);
            ckMoodSystem.TabIndex = 1;
            ckMoodSystem.Text = "Mood system (character's mood change over time based on context)";
            ckMoodSystem.UseVisualStyleBackColor = true;
            // 
            // ck_caninitchat
            // 
            ck_caninitchat.Enabled = false;
            ck_caninitchat.Font = new Font("Segoe UI", 9F);
            ck_caninitchat.Location = new Point(15, 35);
            ck_caninitchat.Name = "ck_caninitchat";
            ck_caninitchat.Size = new Size(410, 26);
            ck_caninitchat.TabIndex = 0;
            ck_caninitchat.Text = "TODO: Character can initiate chat (when the user is afk)";
            ck_caninitchat.UseVisualStyleBackColor = true;
            // 
            // tabMood
            // 
            tabMood.BackColor = Color.FromArgb(37, 37, 37);
            tabMood.Controls.Add(ckStaticMood);
            tabMood.Controls.Add(label24);
            tabMood.Controls.Add(moodSanity);
            tabMood.Controls.Add(label25);
            tabMood.Controls.Add(moodSub);
            tabMood.Controls.Add(label26);
            tabMood.Controls.Add(moodHorny);
            tabMood.Controls.Add(label21);
            tabMood.Controls.Add(moodCuriosity);
            tabMood.Controls.Add(label20);
            tabMood.Controls.Add(moodCheer);
            tabMood.Controls.Add(label10);
            tabMood.Controls.Add(moodEnergy);
            tabMood.Font = new Font("Segoe UI", 9F);
            tabMood.ForeColor = Color.FromArgb(230, 230, 230);
            tabMood.Location = new Point(4, 40);
            tabMood.Name = "tabMood";
            tabMood.Padding = new Padding(3);
            tabMood.Size = new Size(917, 618);
            tabMood.TabIndex = 5;
            tabMood.Text = "Mood Values";
            // 
            // ckStaticMood
            // 
            ckStaticMood.Font = new Font("Segoe UI", 9F);
            ckStaticMood.Location = new Point(20, 17);
            ckStaticMood.Name = "ckStaticMood";
            ckStaticMood.Size = new Size(150, 26);
            ckStaticMood.TabIndex = 12;
            ckStaticMood.Text = "Static Mood Values";
            ckStaticMood.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(20, 282);
            label24.Name = "label24";
            label24.Size = new Size(39, 15);
            label24.TabIndex = 11;
            label24.Text = "Sanity";
            // 
            // moodSanity
            // 
            moodSanity.BackColor = Color.FromArgb(64, 64, 64);
            moodSanity.DecimalPlaces = 5;
            moodSanity.Font = new Font("Segoe UI", 9F);
            moodSanity.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            moodSanity.Location = new Point(20, 300);
            moodSanity.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            moodSanity.Name = "moodSanity";
            moodSanity.Padding = new Padding(1);
            moodSanity.Size = new Size(120, 24);
            moodSanity.TabIndex = 10;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(20, 237);
            label25.Name = "label25";
            label25.Size = new Size(68, 15);
            label25.TabIndex = 9;
            label25.Text = "Submission";
            // 
            // moodSub
            // 
            moodSub.BackColor = Color.FromArgb(64, 64, 64);
            moodSub.DecimalPlaces = 5;
            moodSub.Font = new Font("Segoe UI", 9F);
            moodSub.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            moodSub.Location = new Point(20, 255);
            moodSub.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            moodSub.Name = "moodSub";
            moodSub.Padding = new Padding(1);
            moodSub.Size = new Size(120, 24);
            moodSub.TabIndex = 8;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(20, 192);
            label26.Name = "label26";
            label26.Size = new Size(60, 15);
            label26.TabIndex = 7;
            label26.Text = "Horniness";
            // 
            // moodHorny
            // 
            moodHorny.BackColor = Color.FromArgb(64, 64, 64);
            moodHorny.DecimalPlaces = 5;
            moodHorny.Font = new Font("Segoe UI", 9F);
            moodHorny.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            moodHorny.Location = new Point(20, 210);
            moodHorny.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            moodHorny.Name = "moodHorny";
            moodHorny.Padding = new Padding(1);
            moodHorny.Size = new Size(120, 24);
            moodHorny.TabIndex = 6;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(20, 147);
            label21.Name = "label21";
            label21.Size = new Size(54, 15);
            label21.TabIndex = 5;
            label21.Text = "Curiosity";
            // 
            // moodCuriosity
            // 
            moodCuriosity.BackColor = Color.FromArgb(64, 64, 64);
            moodCuriosity.DecimalPlaces = 5;
            moodCuriosity.Font = new Font("Segoe UI", 9F);
            moodCuriosity.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            moodCuriosity.Location = new Point(20, 165);
            moodCuriosity.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            moodCuriosity.Name = "moodCuriosity";
            moodCuriosity.Padding = new Padding(1);
            moodCuriosity.Size = new Size(120, 24);
            moodCuriosity.TabIndex = 4;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(20, 102);
            label20.Name = "label20";
            label20.Size = new Size(38, 15);
            label20.TabIndex = 3;
            label20.Text = "Cheer";
            // 
            // moodCheer
            // 
            moodCheer.BackColor = Color.FromArgb(64, 64, 64);
            moodCheer.DecimalPlaces = 5;
            moodCheer.Font = new Font("Segoe UI", 9F);
            moodCheer.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            moodCheer.Location = new Point(20, 120);
            moodCheer.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            moodCheer.Name = "moodCheer";
            moodCheer.Padding = new Padding(1);
            moodCheer.Size = new Size(120, 24);
            moodCheer.TabIndex = 2;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(20, 57);
            label10.Name = "label10";
            label10.Size = new Size(43, 15);
            label10.TabIndex = 1;
            label10.Text = "Energy";
            // 
            // moodEnergy
            // 
            moodEnergy.BackColor = Color.FromArgb(64, 64, 64);
            moodEnergy.DecimalPlaces = 5;
            moodEnergy.Font = new Font("Segoe UI", 9F);
            moodEnergy.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            moodEnergy.Location = new Point(20, 75);
            moodEnergy.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            moodEnergy.Name = "moodEnergy";
            moodEnergy.Padding = new Padding(1);
            moodEnergy.Size = new Size(120, 24);
            moodEnergy.TabIndex = 0;
            // 
            // tabSchedule
            // 
            tabSchedule.BackColor = Color.FromArgb(37, 37, 37);
            tabSchedule.Controls.Add(lblSchedulePrefix);
            tabSchedule.Controls.Add(edSchedulePrefix);
            tabSchedule.Controls.Add(lblSunday);
            tabSchedule.Controls.Add(edSunday);
            tabSchedule.Controls.Add(lblMonday);
            tabSchedule.Controls.Add(edMonday);
            tabSchedule.Controls.Add(lblTuesday);
            tabSchedule.Controls.Add(edTuesday);
            tabSchedule.Controls.Add(lblWednesday);
            tabSchedule.Controls.Add(edWednesday);
            tabSchedule.Controls.Add(lblThursday);
            tabSchedule.Controls.Add(edThursday);
            tabSchedule.Controls.Add(lblFriday);
            tabSchedule.Controls.Add(edFriday);
            tabSchedule.Controls.Add(lblSaturday);
            tabSchedule.Controls.Add(edSaturday);
            tabSchedule.Font = new Font("Segoe UI", 9F);
            tabSchedule.ForeColor = Color.FromArgb(230, 230, 230);
            tabSchedule.Location = new Point(4, 40);
            tabSchedule.Name = "tabSchedule";
            tabSchedule.Padding = new Padding(3);
            tabSchedule.Size = new Size(917, 618);
            tabSchedule.TabIndex = 6;
            tabSchedule.Text = "Daily Schedule";
            // 
            // lblSchedulePrefix
            // 
            lblSchedulePrefix.AutoSize = true;
            lblSchedulePrefix.Location = new Point(20, 15);
            lblSchedulePrefix.Name = "lblSchedulePrefix";
            lblSchedulePrefix.Size = new Size(87, 15);
            lblSchedulePrefix.TabIndex = 0;
            lblSchedulePrefix.Text = "Schedule Prefix";
            // 
            // edSchedulePrefix
            // 
            edSchedulePrefix.BackColor = Color.FromArgb(64, 64, 64);
            edSchedulePrefix.ForeColor = Color.FromArgb(230, 230, 230);
            edSchedulePrefix.Location = new Point(20, 33);
            edSchedulePrefix.Name = "edSchedulePrefix";
            edSchedulePrefix.Size = new Size(400, 23);
            edSchedulePrefix.TabIndex = 1;
            // 
            // lblSunday
            // 
            lblSunday.AutoSize = true;
            lblSunday.Location = new Point(20, 70);
            lblSunday.Name = "lblSunday";
            lblSunday.Size = new Size(46, 15);
            lblSunday.TabIndex = 2;
            lblSunday.Text = "Sunday";
            // 
            // edSunday
            // 
            edSunday.BackColor = Color.FromArgb(64, 64, 64);
            edSunday.ForeColor = Color.FromArgb(230, 230, 230);
            edSunday.Location = new Point(20, 88);
            edSunday.Name = "edSunday";
            edSunday.Size = new Size(870, 23);
            edSunday.TabIndex = 3;
            // 
            // lblMonday
            // 
            lblMonday.AutoSize = true;
            lblMonday.Location = new Point(20, 120);
            lblMonday.Name = "lblMonday";
            lblMonday.Size = new Size(51, 15);
            lblMonday.TabIndex = 4;
            lblMonday.Text = "Monday";
            // 
            // edMonday
            // 
            edMonday.BackColor = Color.FromArgb(64, 64, 64);
            edMonday.ForeColor = Color.FromArgb(230, 230, 230);
            edMonday.Location = new Point(20, 138);
            edMonday.Name = "edMonday";
            edMonday.Size = new Size(870, 23);
            edMonday.TabIndex = 5;
            // 
            // lblTuesday
            // 
            lblTuesday.AutoSize = true;
            lblTuesday.Location = new Point(20, 170);
            lblTuesday.Name = "lblTuesday";
            lblTuesday.Size = new Size(51, 15);
            lblTuesday.TabIndex = 6;
            lblTuesday.Text = "Tuesday";
            // 
            // edTuesday
            // 
            edTuesday.BackColor = Color.FromArgb(64, 64, 64);
            edTuesday.ForeColor = Color.FromArgb(230, 230, 230);
            edTuesday.Location = new Point(20, 188);
            edTuesday.Name = "edTuesday";
            edTuesday.Size = new Size(870, 23);
            edTuesday.TabIndex = 7;
            // 
            // lblWednesday
            // 
            lblWednesday.AutoSize = true;
            lblWednesday.Location = new Point(20, 220);
            lblWednesday.Name = "lblWednesday";
            lblWednesday.Size = new Size(68, 15);
            lblWednesday.TabIndex = 8;
            lblWednesday.Text = "Wednesday";
            // 
            // edWednesday
            // 
            edWednesday.BackColor = Color.FromArgb(64, 64, 64);
            edWednesday.ForeColor = Color.FromArgb(230, 230, 230);
            edWednesday.Location = new Point(20, 238);
            edWednesday.Name = "edWednesday";
            edWednesday.Size = new Size(870, 23);
            edWednesday.TabIndex = 9;
            // 
            // lblThursday
            // 
            lblThursday.AutoSize = true;
            lblThursday.Location = new Point(20, 270);
            lblThursday.Name = "lblThursday";
            lblThursday.Size = new Size(56, 15);
            lblThursday.TabIndex = 10;
            lblThursday.Text = "Thursday";
            // 
            // edThursday
            // 
            edThursday.BackColor = Color.FromArgb(64, 64, 64);
            edThursday.ForeColor = Color.FromArgb(230, 230, 230);
            edThursday.Location = new Point(20, 288);
            edThursday.Name = "edThursday";
            edThursday.Size = new Size(870, 23);
            edThursday.TabIndex = 11;
            // 
            // lblFriday
            // 
            lblFriday.AutoSize = true;
            lblFriday.Location = new Point(20, 320);
            lblFriday.Name = "lblFriday";
            lblFriday.Size = new Size(39, 15);
            lblFriday.TabIndex = 12;
            lblFriday.Text = "Friday";
            // 
            // edFriday
            // 
            edFriday.BackColor = Color.FromArgb(64, 64, 64);
            edFriday.ForeColor = Color.FromArgb(230, 230, 230);
            edFriday.Location = new Point(20, 338);
            edFriday.Name = "edFriday";
            edFriday.Size = new Size(870, 23);
            edFriday.TabIndex = 13;
            // 
            // lblSaturday
            // 
            lblSaturday.AutoSize = true;
            lblSaturday.Location = new Point(20, 370);
            lblSaturday.Name = "lblSaturday";
            lblSaturday.Size = new Size(53, 15);
            lblSaturday.TabIndex = 14;
            lblSaturday.Text = "Saturday";
            // 
            // edSaturday
            // 
            edSaturday.BackColor = Color.FromArgb(64, 64, 64);
            edSaturday.ForeColor = Color.FromArgb(230, 230, 230);
            edSaturday.Location = new Point(20, 388);
            edSaturday.Name = "edSaturday";
            edSaturday.Size = new Size(870, 23);
            edSaturday.TabIndex = 15;
            // 
            // CharEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1159, 662);
            Controls.Add(TabControl);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CharEditForm";
            Text = "Character Editor";
            KeyDown += CharEditForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            TabControl.ResumeLayout(false);
            tbGeneral.ResumeLayout(false);
            tbGeneral.PerformLayout();
            tbStyle.ResumeLayout(false);
            collapsibleGroupBox3.ResumeLayout(false);
            collapsibleGroupBox3.PerformLayout();
            collapsibleGroupBox2.ResumeLayout(false);
            collapsibleGroupBox2.PerformLayout();
            collapsibleGroupBox1.ResumeLayout(false);
            collapsibleGroupBox1.PerformLayout();
            tbPrompt.ResumeLayout(false);
            tbPrompt.PerformLayout();
            tbAgent.ResumeLayout(false);
            collapsibleGroupBox8.ResumeLayout(false);
            collapsibleGroupBox8.PerformLayout();
            collapsibleGroupBox7.ResumeLayout(false);
            collapsibleGroupBox7.PerformLayout();
            collapsibleGroupBox6.ResumeLayout(false);
            collapsibleGroupBox6.PerformLayout();
            collapsibleGroupBox5.ResumeLayout(false);
            collapsibleGroupBox5.PerformLayout();
            tbSettings.ResumeLayout(false);
            collapsibleGroupBox11.ResumeLayout(false);
            collapsibleGroupBox11.PerformLayout();
            collapsibleGroupBox10.ResumeLayout(false);
            collapsibleGroupBox10.PerformLayout();
            collapsibleGroupBox9.ResumeLayout(false);
            collapsibleGroupBox9.PerformLayout();
            collapsibleGroupBox4.ResumeLayout(false);
            collapsibleGroupBox4.PerformLayout();
            tabMood.ResumeLayout(false);
            tabMood.PerformLayout();
            tabSchedule.ResumeLayout(false);
            tabSchedule.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox ed_bio;
        private Label label2;
        private CheckBox ck_isuser;
        private TextBox ed_name;
        private Label label1;
        private TextBox textBox2;
        private TextBox ed_scenario;
        private Label label3;
        private Label label4;
        private TextBox ed_firstmessage;
        private TextBox ed_sysprompt;
        private Label label6;
        private Label label5;
        private TextBox ed_writingstyle;
        private Label label7;
        private Label label9;
        private TextBox ed_outetts;
        private Label label11;
        private ModernComboBox cb_icon;
        private PictureBox pic;
        private Label label13;
        private ModernNumericUpDown num_selfedittokens;
        private TextBox ed_selfedit;
        private Label label12;
        private ModernComboBox cb_pointsystems;
        private Label label14;
        private ModernNumericUpDown num_ptvalue;
        private Button btRegenBio;
        private Panel panel1;
        private Button btSave;
        private TextBox edFilename;
        private Label label8;
        private ModernNumericUpDown num_minAFK;
        private ModernNumericUpDown numEurekaMinTime;
        private Label label16;
        private Label label15;
        private ModernNumericUpDown numEurekaMinMess;
        private Label label17;
        private ModernNumericUpDown numKeepEurekas;
        private Label label18;
        private Label label19;
        private ModernNumericUpDown numAgentDelay;
        private Controls.ModernTabControl TabControl;
        private TabPage tbGeneral;
        private TabPage tbStyle;
        private TabPage tbPrompt;
        private TabPage tbAgent;
        private TabPage tbSettings;
        private Controls.CollapsibleGroupBox collapsibleGroupBox2;
        private Controls.CollapsibleGroupBox collapsibleGroupBox1;
        private Controls.CollapsibleGroupBox collapsibleGroupBox3;
        private Controls.CollapsibleGroupBox collapsibleGroupBox6;
        private Controls.CollapsibleGroupBox collapsibleGroupBox5;
        private Controls.ModernCheckBox ckAgent;
        private Controls.ModernCheckBox ckAllowEurekas;
        private Controls.CollapsibleGroupBox collapsibleGroupBox7;
        private Label label22;
        private Controls.CollapsibleGroupBox collapsibleGroupBox8;
        private Label label23;
        private Controls.CollapsibleGroupBox collapsibleGroupBox9;
        private Controls.CollapsibleGroupBox collapsibleGroupBox4;
        private Controls.ModernCheckBox ckPassword;
        private Controls.ModernCheckBox ck_senseoftime;
        private Controls.ModernCheckBox ck_irldates;
        private Controls.ModernCheckBox ckMoodSystem;
        private Controls.ModernCheckBox ck_caninitchat;
        private Controls.CollapsibleGroupBox collapsibleGroupBox10;
        private Controls.ModernListBox cb_charlist;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Controls.ModernCheckedListBox listAgentTasks;
        private Controls.ModernCheckedListBox listNoRAGMemTypes;
        private Controls.ModernCheckedListBox listCanDecay;
        private Controls.ModernCheckedListBox ckl_worldinfo;
        private Controls.ModernCheckedListBox ckl_samplers;
        private Controls.ModernCheckedListBox ckl_plugins;
        private CheckBox ckNoGuidance;
        private TabPage tabMood;
        private Label label10;
        private ModernNumericUpDown moodEnergy;
        private Label label21;
        private ModernNumericUpDown moodCuriosity;
        private Label label20;
        private ModernNumericUpDown moodCheer;
        private Label label24;
        private ModernNumericUpDown moodSanity;
        private Label label25;
        private ModernNumericUpDown moodSub;
        private Label label26;
        private ModernNumericUpDown moodHorny;
        private Label label27;
        private TextBox edMiniBio;
        private CollapsibleGroupBox collapsibleGroupBox11;
        private Label label28;
        private TextBox edTimerOn;
        private ModernCheckBox ckTimerEnable;
        private Label label30;
        private TextBox edTimerExpired;
        private Label label29;
        private TextBox edTimerOff;
        private ModernCheckBox ckTimerAutoExpire;
        private ModernCheckBox ckTimerActive;
        private ModernCheckBox ckStaticMood;
        private ModernCheckBox ckTimerAddDuration;
        private TabPage tabSchedule;
        private Label lblSchedulePrefix;
        private TextBox edSchedulePrefix;
        private Label lblSunday;
        private TextBox edSunday;
        private Label lblMonday;
        private TextBox edMonday;
        private Label lblTuesday;
        private TextBox edTuesday;
        private Label lblWednesday;
        private TextBox edWednesday;
        private Label lblThursday;
        private TextBox edThursday;
        private Label lblFriday;
        private TextBox edFriday;
        private Label lblSaturday;
        private TextBox edSaturday;
    }
}