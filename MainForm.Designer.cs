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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabControl1 = new TabControl();
            tabChat = new TabPage();
            bt_delete = new Button();
            bt_reroll = new Button();
            bt_send = new Button();
            ed_input = new TextBox();
            flowChat = new FlowLayoutPanel();
            panel1 = new Panel();
            button1 = new Button();
            cb_sysprompt = new ComboBox();
            label11 = new Label();
            num_temperature = new NumericUpDown();
            label9 = new Label();
            lbl_info = new Label();
            bt_connect = new Button();
            num_maxresponse = new NumericUpDown();
            label8 = new Label();
            num_maxcontext = new NumericUpDown();
            label7 = new Label();
            cb_infer = new ComboBox();
            label6 = new Label();
            cb_instruct = new ComboBox();
            label5 = new Label();
            cb_user = new ComboBox();
            label4 = new Label();
            cb_bot = new ComboBox();
            label3 = new Label();
            tabInstruct = new TabPage();
            bt_instructsave = new Button();
            label2 = new Label();
            cb_instructlist = new ComboBox();
            pan_instruct = new Panel();
            tabPage1 = new TabPage();
            bt_promptsave = new Button();
            label10 = new Label();
            cb_promptlist = new ComboBox();
            pan_prompt = new Panel();
            tabSamplers = new TabPage();
            bt_savesampler = new Button();
            label1 = new Label();
            cb_samplerlist = new ComboBox();
            pan_samplers = new Panel();
            tabSettings = new TabPage();
            bt_importworld = new Button();
            bt_ImportSTChat = new Button();
            tabAPI = new TabPage();
            bt_stream = new Button();
            bt_perf = new Button();
            bt_extraversion = new Button();
            ed_generate = new TextBox();
            bt_generate = new Button();
            ed_tokencount = new TextBox();
            bt_tokencount = new Button();
            bt_maxctxlen = new Button();
            bt_version = new Button();
            bt_getmodel = new Button();
            listBox1 = new ListBox();
            openFileDialog1 = new OpenFileDialog();
            ed_log = new TextBox();
            tabControl1.SuspendLayout();
            tabChat.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)num_temperature).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_maxresponse).BeginInit();
            ((System.ComponentModel.ISupportInitialize)num_maxcontext).BeginInit();
            tabInstruct.SuspendLayout();
            tabPage1.SuspendLayout();
            tabSamplers.SuspendLayout();
            tabSettings.SuspendLayout();
            tabAPI.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.Controls.Add(tabChat);
            tabControl1.Controls.Add(tabInstruct);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabSamplers);
            tabControl1.Controls.Add(tabSettings);
            tabControl1.Controls.Add(tabAPI);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(982, 589);
            tabControl1.TabIndex = 1;
            // 
            // tabChat
            // 
            tabChat.Controls.Add(bt_delete);
            tabChat.Controls.Add(bt_reroll);
            tabChat.Controls.Add(bt_send);
            tabChat.Controls.Add(ed_input);
            tabChat.Controls.Add(flowChat);
            tabChat.Controls.Add(panel1);
            tabChat.Location = new Point(4, 27);
            tabChat.Name = "tabChat";
            tabChat.Padding = new Padding(3);
            tabChat.Size = new Size(974, 558);
            tabChat.TabIndex = 1;
            tabChat.Text = "Chat";
            tabChat.UseVisualStyleBackColor = true;
            // 
            // bt_delete
            // 
            bt_delete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_delete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_delete.Location = new Point(891, 516);
            bt_delete.Name = "bt_delete";
            bt_delete.Size = new Size(75, 39);
            bt_delete.TabIndex = 5;
            bt_delete.Text = "REM LAST";
            bt_delete.UseVisualStyleBackColor = true;
            bt_delete.Click += DeleteLastMessage;
            // 
            // bt_reroll
            // 
            bt_reroll.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_reroll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_reroll.Location = new Point(810, 516);
            bt_reroll.Name = "bt_reroll";
            bt_reroll.Size = new Size(75, 39);
            bt_reroll.TabIndex = 4;
            bt_reroll.Text = "REROLL";
            bt_reroll.UseVisualStyleBackColor = true;
            // 
            // bt_send
            // 
            bt_send.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bt_send.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_send.Location = new Point(729, 516);
            bt_send.Name = "bt_send";
            bt_send.Size = new Size(75, 39);
            bt_send.TabIndex = 3;
            bt_send.Text = "SEND";
            bt_send.UseVisualStyleBackColor = true;
            // 
            // ed_input
            // 
            ed_input.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ed_input.Location = new Point(209, 516);
            ed_input.Multiline = true;
            ed_input.Name = "ed_input";
            ed_input.Size = new Size(514, 39);
            ed_input.TabIndex = 2;
            ed_input.KeyPress += ed_input_KeyPress;
            // 
            // flowChat
            // 
            flowChat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowChat.AutoScroll = true;
            flowChat.BorderStyle = BorderStyle.FixedSingle;
            flowChat.FlowDirection = FlowDirection.TopDown;
            flowChat.Location = new Point(209, 6);
            flowChat.Name = "flowChat";
            flowChat.Size = new Size(757, 504);
            flowChat.TabIndex = 1;
            flowChat.WrapContents = false;
            flowChat.Resize += flowChat_Resize;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(cb_sysprompt);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(num_temperature);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(lbl_info);
            panel1.Controls.Add(bt_connect);
            panel1.Controls.Add(num_maxresponse);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(num_maxcontext);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(cb_infer);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(cb_instruct);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(cb_user);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(cb_bot);
            panel1.Controls.Add(label3);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 552);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(6, 429);
            button1.Name = "button1";
            button1.Size = new Size(187, 23);
            button1.TabIndex = 20;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // cb_sysprompt
            // 
            cb_sysprompt.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_sysprompt.Location = new Point(2, 206);
            cb_sysprompt.Name = "cb_sysprompt";
            cb_sysprompt.Size = new Size(191, 23);
            cb_sysprompt.TabIndex = 19;
            cb_sysprompt.SelectedIndexChanged += cb_sysprompt_SelectionIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label11.Location = new Point(4, 188);
            label11.Name = "label11";
            label11.Size = new Size(93, 15);
            label11.TabIndex = 18;
            label11.Text = "System Prompt";
            // 
            // num_temperature
            // 
            num_temperature.DecimalPlaces = 2;
            num_temperature.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            num_temperature.Location = new Point(2, 347);
            num_temperature.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            num_temperature.Name = "num_temperature";
            num_temperature.Size = new Size(191, 23);
            num_temperature.TabIndex = 17;
            num_temperature.ThousandsSeparator = true;
            num_temperature.Value = new decimal(new int[] { 7, 0, 0, 65536 });
            num_temperature.ValueChanged += num_temperature_ValueChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label9.Location = new Point(6, 329);
            label9.Name = "label9";
            label9.Size = new Size(133, 15);
            label9.TabIndex = 16;
            label9.Text = "Temperature Override";
            // 
            // lbl_info
            // 
            lbl_info.AutoSize = true;
            lbl_info.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbl_info.Location = new Point(2, 512);
            lbl_info.Name = "lbl_info";
            lbl_info.Size = new Size(59, 15);
            lbl_info.TabIndex = 15;
            lbl_info.Text = "Waiting...";
            // 
            // bt_connect
            // 
            bt_connect.Location = new Point(2, 458);
            bt_connect.Name = "bt_connect";
            bt_connect.Size = new Size(191, 23);
            bt_connect.TabIndex = 14;
            bt_connect.Text = "Connect";
            bt_connect.UseVisualStyleBackColor = true;
            // 
            // num_maxresponse
            // 
            num_maxresponse.Increment = new decimal(new int[] { 32, 0, 0, 0 });
            num_maxresponse.Location = new Point(2, 303);
            num_maxresponse.Maximum = new decimal(new int[] { 16384, 0, 0, 0 });
            num_maxresponse.Name = "num_maxresponse";
            num_maxresponse.Size = new Size(191, 23);
            num_maxresponse.TabIndex = 12;
            num_maxresponse.ThousandsSeparator = true;
            num_maxresponse.Value = new decimal(new int[] { 512, 0, 0, 0 });
            num_maxresponse.ValueChanged += num_maxresponse_ValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label8.Location = new Point(6, 285);
            label8.Name = "label8";
            label8.Size = new Size(129, 15);
            label8.TabIndex = 11;
            label8.Text = "Max Response Length";
            // 
            // num_maxcontext
            // 
            num_maxcontext.Increment = new decimal(new int[] { 512, 0, 0, 0 });
            num_maxcontext.Location = new Point(2, 259);
            num_maxcontext.Maximum = new decimal(new int[] { 16384, 0, 0, 0 });
            num_maxcontext.Minimum = new decimal(new int[] { 1024, 0, 0, 0 });
            num_maxcontext.Name = "num_maxcontext";
            num_maxcontext.Size = new Size(191, 23);
            num_maxcontext.TabIndex = 10;
            num_maxcontext.Value = new decimal(new int[] { 16384, 0, 0, 0 });
            num_maxcontext.ValueChanged += num_maxcontext_ValueChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label7.Location = new Point(6, 241);
            label7.Name = "label7";
            label7.Size = new Size(121, 15);
            label7.TabIndex = 8;
            label7.Text = "Max Context Length";
            // 
            // cb_infer
            // 
            cb_infer.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_infer.Location = new Point(2, 162);
            cb_infer.Name = "cb_infer";
            cb_infer.Size = new Size(191, 23);
            cb_infer.TabIndex = 7;
            cb_infer.SelectedIndexChanged += cb_infer_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.Location = new Point(4, 144);
            label6.Name = "label6";
            label6.Size = new Size(107, 15);
            label6.TabIndex = 6;
            label6.Text = "Sampling Settings";
            // 
            // cb_instruct
            // 
            cb_instruct.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_instruct.Location = new Point(2, 118);
            cb_instruct.Name = "cb_instruct";
            cb_instruct.Size = new Size(191, 23);
            cb_instruct.TabIndex = 5;
            cb_instruct.SelectedIndexChanged += cb_instruct_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(4, 100);
            label5.Name = "label5";
            label5.Size = new Size(111, 15);
            label5.TabIndex = 4;
            label5.Text = "Instruction Format";
            // 
            // cb_user
            // 
            cb_user.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_user.Location = new Point(2, 74);
            cb_user.Name = "cb_user";
            cb_user.Size = new Size(191, 23);
            cb_user.TabIndex = 3;
            cb_user.SelectedIndexChanged += cb_user_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(4, 56);
            label4.Name = "label4";
            label4.Size = new Size(80, 15);
            label4.TabIndex = 2;
            label4.Text = "User Persona";
            // 
            // cb_bot
            // 
            cb_bot.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_bot.Location = new Point(2, 30);
            cb_bot.Name = "cb_bot";
            cb_bot.Size = new Size(191, 23);
            cb_bot.TabIndex = 1;
            cb_bot.SelectedIndexChanged += cb_bot_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(4, 12);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 0;
            label3.Text = "Bot Persona";
            // 
            // tabInstruct
            // 
            tabInstruct.Controls.Add(bt_instructsave);
            tabInstruct.Controls.Add(label2);
            tabInstruct.Controls.Add(cb_instructlist);
            tabInstruct.Controls.Add(pan_instruct);
            tabInstruct.Location = new Point(4, 27);
            tabInstruct.Name = "tabInstruct";
            tabInstruct.Padding = new Padding(3);
            tabInstruct.Size = new Size(974, 558);
            tabInstruct.TabIndex = 2;
            tabInstruct.Text = "Instruction Format Editor";
            tabInstruct.UseVisualStyleBackColor = true;
            // 
            // bt_instructsave
            // 
            bt_instructsave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bt_instructsave.Location = new Point(849, 8);
            bt_instructsave.Name = "bt_instructsave";
            bt_instructsave.Size = new Size(75, 23);
            bt_instructsave.TabIndex = 9;
            bt_instructsave.Text = "Save";
            bt_instructsave.UseVisualStyleBackColor = true;
            bt_instructsave.Click += bt_instructsave_Click;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(8, 8);
            label2.Name = "label2";
            label2.Size = new Size(104, 23);
            label2.TabIndex = 8;
            label2.Text = "Instruct Format";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cb_instructlist
            // 
            cb_instructlist.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_instructlist.FormattingEnabled = true;
            cb_instructlist.Location = new Point(118, 8);
            cb_instructlist.Name = "cb_instructlist";
            cb_instructlist.Size = new Size(725, 23);
            cb_instructlist.TabIndex = 7;
            // 
            // pan_instruct
            // 
            pan_instruct.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pan_instruct.AutoScroll = true;
            pan_instruct.Location = new Point(8, 38);
            pan_instruct.Name = "pan_instruct";
            pan_instruct.Size = new Size(958, 513);
            pan_instruct.TabIndex = 6;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(bt_promptsave);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(cb_promptlist);
            tabPage1.Controls.Add(pan_prompt);
            tabPage1.Location = new Point(4, 27);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(974, 558);
            tabPage1.TabIndex = 5;
            tabPage1.Text = "System Prompt Editor";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // bt_promptsave
            // 
            bt_promptsave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bt_promptsave.Location = new Point(849, 12);
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
            label10.Location = new Point(8, 12);
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
            cb_promptlist.Location = new Point(118, 12);
            cb_promptlist.Name = "cb_promptlist";
            cb_promptlist.Size = new Size(725, 23);
            cb_promptlist.TabIndex = 11;
            // 
            // pan_prompt
            // 
            pan_prompt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pan_prompt.AutoScroll = true;
            pan_prompt.Location = new Point(8, 42);
            pan_prompt.Name = "pan_prompt";
            pan_prompt.Size = new Size(958, 513);
            pan_prompt.TabIndex = 10;
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
            tabSamplers.Size = new Size(974, 558);
            tabSamplers.TabIndex = 3;
            tabSamplers.Text = "Sampler Editor";
            tabSamplers.UseVisualStyleBackColor = true;
            // 
            // bt_savesampler
            // 
            bt_savesampler.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bt_savesampler.Location = new Point(849, 10);
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
            label1.Location = new Point(8, 10);
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
            cb_samplerlist.Location = new Point(82, 10);
            cb_samplerlist.Name = "cb_samplerlist";
            cb_samplerlist.Size = new Size(761, 23);
            cb_samplerlist.TabIndex = 1;
            // 
            // pan_samplers
            // 
            pan_samplers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pan_samplers.AutoScroll = true;
            pan_samplers.Location = new Point(8, 40);
            pan_samplers.Name = "pan_samplers";
            pan_samplers.Size = new Size(958, 513);
            pan_samplers.TabIndex = 0;
            // 
            // tabSettings
            // 
            tabSettings.Controls.Add(ed_log);
            tabSettings.Controls.Add(bt_importworld);
            tabSettings.Controls.Add(bt_ImportSTChat);
            tabSettings.Location = new Point(4, 27);
            tabSettings.Name = "tabSettings";
            tabSettings.Padding = new Padding(3);
            tabSettings.Size = new Size(974, 558);
            tabSettings.TabIndex = 4;
            tabSettings.Text = "Settings";
            tabSettings.UseVisualStyleBackColor = true;
            // 
            // bt_importworld
            // 
            bt_importworld.Location = new Point(8, 35);
            bt_importworld.Name = "bt_importworld";
            bt_importworld.Size = new Size(154, 23);
            bt_importworld.TabIndex = 1;
            bt_importworld.Text = "Import ST WorldInfo";
            bt_importworld.UseVisualStyleBackColor = true;
            bt_importworld.Click += bt_importworld_Click;
            // 
            // bt_ImportSTChat
            // 
            bt_ImportSTChat.Location = new Point(8, 6);
            bt_ImportSTChat.Name = "bt_ImportSTChat";
            bt_ImportSTChat.Size = new Size(154, 23);
            bt_ImportSTChat.TabIndex = 0;
            bt_ImportSTChat.Text = "Import ST Chat";
            bt_ImportSTChat.UseVisualStyleBackColor = true;
            bt_ImportSTChat.Click += bt_ImportSTChat_Click;
            // 
            // tabAPI
            // 
            tabAPI.Controls.Add(bt_stream);
            tabAPI.Controls.Add(bt_perf);
            tabAPI.Controls.Add(bt_extraversion);
            tabAPI.Controls.Add(ed_generate);
            tabAPI.Controls.Add(bt_generate);
            tabAPI.Controls.Add(ed_tokencount);
            tabAPI.Controls.Add(bt_tokencount);
            tabAPI.Controls.Add(bt_maxctxlen);
            tabAPI.Controls.Add(bt_version);
            tabAPI.Controls.Add(bt_getmodel);
            tabAPI.Controls.Add(listBox1);
            tabAPI.Location = new Point(4, 27);
            tabAPI.Name = "tabAPI";
            tabAPI.Padding = new Padding(3);
            tabAPI.Size = new Size(974, 558);
            tabAPI.TabIndex = 0;
            tabAPI.Text = "API Testing";
            tabAPI.UseVisualStyleBackColor = true;
            // 
            // bt_stream
            // 
            bt_stream.Location = new Point(6, 151);
            bt_stream.Name = "bt_stream";
            bt_stream.Size = new Size(127, 23);
            bt_stream.TabIndex = 10;
            bt_stream.Text = "Stream";
            bt_stream.UseVisualStyleBackColor = true;
            // 
            // bt_perf
            // 
            bt_perf.Location = new Point(139, 64);
            bt_perf.Name = "bt_perf";
            bt_perf.Size = new Size(127, 23);
            bt_perf.TabIndex = 9;
            bt_perf.Text = "Perf Info";
            bt_perf.UseVisualStyleBackColor = true;
            // 
            // bt_extraversion
            // 
            bt_extraversion.Location = new Point(139, 35);
            bt_extraversion.Name = "bt_extraversion";
            bt_extraversion.Size = new Size(127, 23);
            bt_extraversion.TabIndex = 8;
            bt_extraversion.Text = "Extra Version";
            bt_extraversion.UseVisualStyleBackColor = true;
            // 
            // ed_generate
            // 
            ed_generate.Location = new Point(139, 122);
            ed_generate.Multiline = true;
            ed_generate.Name = "ed_generate";
            ed_generate.Size = new Size(279, 87);
            ed_generate.TabIndex = 7;
            ed_generate.Text = "<|system|>You are a helpful assistant<|user|>Hello, how are you doing?<|model|>";
            // 
            // bt_generate
            // 
            bt_generate.Location = new Point(6, 122);
            bt_generate.Name = "bt_generate";
            bt_generate.Size = new Size(127, 23);
            bt_generate.TabIndex = 6;
            bt_generate.Text = "Generate";
            bt_generate.UseVisualStyleBackColor = true;
            // 
            // ed_tokencount
            // 
            ed_tokencount.Location = new Point(139, 93);
            ed_tokencount.Name = "ed_tokencount";
            ed_tokencount.Size = new Size(279, 23);
            ed_tokencount.TabIndex = 5;
            ed_tokencount.Text = "This is a test sentence";
            // 
            // bt_tokencount
            // 
            bt_tokencount.Location = new Point(6, 93);
            bt_tokencount.Name = "bt_tokencount";
            bt_tokencount.Size = new Size(127, 23);
            bt_tokencount.TabIndex = 4;
            bt_tokencount.Text = "Count Tokens";
            bt_tokencount.UseVisualStyleBackColor = true;
            // 
            // bt_maxctxlen
            // 
            bt_maxctxlen.Location = new Point(6, 64);
            bt_maxctxlen.Name = "bt_maxctxlen";
            bt_maxctxlen.Size = new Size(127, 23);
            bt_maxctxlen.TabIndex = 3;
            bt_maxctxlen.Text = "Max Ctx Len";
            bt_maxctxlen.UseVisualStyleBackColor = true;
            // 
            // bt_version
            // 
            bt_version.Location = new Point(6, 35);
            bt_version.Name = "bt_version";
            bt_version.Size = new Size(127, 23);
            bt_version.TabIndex = 2;
            bt_version.Text = "Version";
            bt_version.UseVisualStyleBackColor = true;
            // 
            // bt_getmodel
            // 
            bt_getmodel.Location = new Point(6, 6);
            bt_getmodel.Name = "bt_getmodel";
            bt_getmodel.Size = new Size(127, 23);
            bt_getmodel.TabIndex = 1;
            bt_getmodel.Text = "Get Model Name";
            bt_getmodel.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.Dock = DockStyle.Bottom;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(3, 431);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(968, 124);
            listBox1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // ed_log
            // 
            ed_log.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ed_log.Location = new Point(8, 196);
            ed_log.Multiline = true;
            ed_log.Name = "ed_log";
            ed_log.Size = new Size(958, 354);
            ed_log.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 589);
            Controls.Add(tabControl1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "w(AI)fu";
            FormClosing += MainForm_FormClosing;
            tabControl1.ResumeLayout(false);
            tabChat.ResumeLayout(false);
            tabChat.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)num_temperature).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_maxresponse).EndInit();
            ((System.ComponentModel.ISupportInitialize)num_maxcontext).EndInit();
            tabInstruct.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabSamplers.ResumeLayout(false);
            tabSettings.ResumeLayout(false);
            tabSettings.PerformLayout();
            tabAPI.ResumeLayout(false);
            tabAPI.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControl1;
        private TabPage tabAPI;
        private TabPage tabChat;
        private Button bt_getmodel;
        private ListBox listBox1;
        private Button bt_maxctxlen;
        private Button bt_version;
        private TextBox ed_tokencount;
        private Button bt_tokencount;
        private TextBox ed_generate;
        private Button bt_generate;
        private Button bt_extraversion;
        private Button bt_perf;
        private Button bt_stream;
        private TabPage tabInstruct;
        private TabPage tabSamplers;
        private TabPage tabSettings;
        private Panel pan_samplers;
        private Label label1;
        private ComboBox cb_samplerlist;
        private Button bt_savesampler;
        private Button bt_instructsave;
        private Label label2;
        private ComboBox cb_instructlist;
        private Panel pan_instruct;
        private Panel panel1;
        private Label label3;
        private ComboBox cb_instruct;
        private Label label5;
        private ComboBox cb_user;
        private Label label4;
        private ComboBox cb_bot;
        private ComboBox cb_infer;
        private Label label6;
        private Label label7;
        private NumericUpDown num_maxcontext;
        private NumericUpDown num_maxresponse;
        private Label label8;
        private FlowLayoutPanel flowChat;
        private TextBox ed_input;
        private Button bt_delete;
        private Button bt_reroll;
        private Button bt_send;
        private TabPage tabPage1;
        private Button bt_connect;
        private Label lbl_info;
        private Label label9;
        private NumericUpDown num_temperature;
        private Button bt_promptsave;
        private Label label10;
        private ComboBox cb_promptlist;
        private Panel pan_prompt;
        private ComboBox cb_sysprompt;
        private Label label11;
        private Button button1;
        private Button bt_ImportSTChat;
        private OpenFileDialog openFileDialog1;
        private Button bt_importworld;
        private TextBox ed_log;
    }
}
