namespace LetheChat.src.forms
{
    partial class ModelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelForm));
            verticalStackPanel1 = new LetheChat.Controls.VerticalStackPanel();
            bt_newsession = new Button();
            listModels = new LetheChat.Controls.ModernListBox();
            verticalStackPanel2 = new LetheChat.Controls.VerticalStackPanel();
            panButtons = new Panel();
            btApply = new Button();
            btLaunch = new Button();
            btStop = new Button();
            lblServerStatus = new Label();
            lvServerLog = new ListView();
            colLogLevel = new ColumnHeader();
            colLogMessage = new ColumnHeader();
            boxSettings = new LetheChat.Controls.CollapsibleGroupBox();
            verticalStackPanel1.SuspendLayout();
            verticalStackPanel2.SuspendLayout();
            panButtons.SuspendLayout();
            SuspendLayout();
            // 
            // verticalStackPanel1
            // 
            verticalStackPanel1.Controls.Add(bt_newsession);
            verticalStackPanel1.Controls.Add(listModels);
            verticalStackPanel1.Dock = DockStyle.Left;
            verticalStackPanel1.Location = new Point(0, 0);
            verticalStackPanel1.Name = "verticalStackPanel1";
            verticalStackPanel1.Padding = new Padding(8);
            verticalStackPanel1.Size = new Size(270, 560);
            verticalStackPanel1.TabIndex = 35;
            verticalStackPanel1.Paint += verticalStackPanel1_Paint;
            // 
            // bt_newsession
            // 
            bt_newsession.AutoSize = true;
            bt_newsession.BackColor = Color.DarkKhaki;
            bt_newsession.FlatStyle = FlatStyle.Flat;
            bt_newsession.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_newsession.ForeColor = Color.Black;
            bt_newsession.Name = "bt_newsession";
            bt_newsession.Size = new Size(254, 27);
            bt_newsession.TabIndex = 22;
            bt_newsession.Tag = "no-theme";
            bt_newsession.Text = "Update Model List";
            bt_newsession.UseVisualStyleBackColor = false;
            bt_newsession.Click += bt_newsession_Click_1;
            // 
            // listModels
            // 
            listModels.AlwaysScrollbar = true;
            listModels.BackColor = Color.FromArgb(64, 64, 64);
            listModels.BorderStyle = BorderStyle.FixedSingle;
            listModels.Font = new Font("Segoe UI", 9F);
            listModels.Name = "listModels";
            listModels.Padding = new Padding(1);
            listModels.Size = new Size(254, 489);
            listModels.TabIndex = 0;
            // 
            // verticalStackPanel2
            // 
            verticalStackPanel2.Controls.Add(panButtons);
            verticalStackPanel2.Controls.Add(lblServerStatus);
            verticalStackPanel2.Controls.Add(lvServerLog);
            verticalStackPanel2.Controls.Add(boxSettings);
            verticalStackPanel2.Dock = DockStyle.Fill;
            verticalStackPanel2.Location = new Point(270, 0);
            verticalStackPanel2.Name = "verticalStackPanel2";
            verticalStackPanel2.Padding = new Padding(8);
            verticalStackPanel2.Size = new Size(645, 560);
            verticalStackPanel2.TabIndex = 36;
            verticalStackPanel2.Paint += verticalStackPanel2_Paint;
            // 
            // panButtons  (holds Apply / Launch / Stop side-by-side)
            // 
            panButtons.Controls.Add(btApply);
            panButtons.Controls.Add(btLaunch);
            panButtons.Controls.Add(btStop);
            panButtons.BackColor = Color.Transparent;
            panButtons.Name = "panButtons";
            panButtons.Size = new Size(629, 27);
            panButtons.TabIndex = 43;
            // 
            // btApply
            // 
            btApply.BackColor = Color.DarkKhaki;
            btApply.FlatStyle = FlatStyle.Flat;
            btApply.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btApply.ForeColor = Color.Black;
            btApply.Location = new Point(0, 0);
            btApply.Name = "btApply";
            btApply.Size = new Size(199, 27);
            btApply.TabIndex = 39;
            btApply.Tag = "no-theme";
            btApply.Text = "Save Model's Settings";
            btApply.UseVisualStyleBackColor = false;
            // 
            // btLaunch
            // 
            btLaunch.BackColor = Color.FromArgb(0x2E, 0x7D, 0x32);
            btLaunch.FlatStyle = FlatStyle.Flat;
            btLaunch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btLaunch.ForeColor = Color.White;
            btLaunch.Location = new Point(207, 0);
            btLaunch.Name = "btLaunch";
            btLaunch.Size = new Size(205, 27);
            btLaunch.TabIndex = 40;
            btLaunch.Tag = "no-theme";
            btLaunch.Text = "Launch ▶";
            btLaunch.UseVisualStyleBackColor = false;
            // 
            // btStop
            // 
            btStop.BackColor = Color.FromArgb(0xB7, 0x1C, 0x1C);
            btStop.FlatStyle = FlatStyle.Flat;
            btStop.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btStop.ForeColor = Color.White;
            btStop.Location = new Point(420, 0);
            btStop.Name = "btStop";
            btStop.Size = new Size(209, 27);
            btStop.TabIndex = 41;
            btStop.Tag = "no-theme";
            btStop.Text = "Stop ■";
            btStop.UseVisualStyleBackColor = false;
            // 
            // lblServerStatus
            // 
            lblServerStatus.Font = new Font("Segoe UI", 9F);
            lblServerStatus.ForeColor = Color.FromArgb(0xE6, 0xE6, 0xE6);
            lblServerStatus.Name = "lblServerStatus";
            lblServerStatus.Size = new Size(629, 18);
            lblServerStatus.TabIndex = 42;
            lblServerStatus.Text = "○ Server Stopped";
            lblServerStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lvServerLog
            // 
            lvServerLog.BackColor = Color.FromArgb(30, 31, 34);
            lvServerLog.BorderStyle = BorderStyle.FixedSingle;
            lvServerLog.Columns.AddRange(new ColumnHeader[] { colLogLevel, colLogMessage });
            lvServerLog.Font = new Font("Consolas", 8.25F);
            lvServerLog.ForeColor = Color.FromArgb(0xE6, 0xE6, 0xE6);
            lvServerLog.FullRowSelect = true;
            lvServerLog.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvServerLog.MultiSelect = false;
            lvServerLog.Name = "lvServerLog";
            lvServerLog.Size = new Size(629, 90);
            lvServerLog.TabIndex = 44;
            lvServerLog.UseCompatibleStateImageBehavior = false;
            lvServerLog.View = View.Details;
            // 
            // colLogLevel
            // 
            colLogLevel.Text = "Level";
            colLogLevel.Width = 50;
            // 
            // colLogMessage
            // 
            colLogMessage.Text = "Message";
            colLogMessage.Width = 560;
            // 
            // boxSettings
            // 
            boxSettings.BackColor = Color.FromArgb(37, 37, 37);
            boxSettings.Font = new Font("Segoe UI", 9F);
            boxSettings.Name = "boxSettings";
            boxSettings.Padding = new Padding(12, 32, 12, 10);
            boxSettings.Size = new Size(629, 370);
            boxSettings.TabIndex = 0;
            boxSettings.Text = "Settings";
            // 
            // ModelForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(915, 560);
            Controls.Add(verticalStackPanel2);
            Controls.Add(verticalStackPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ModelForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Language Model Loader";
            Load += ModelForm_Load;
            verticalStackPanel1.ResumeLayout(false);
            verticalStackPanel1.PerformLayout();
            verticalStackPanel2.ResumeLayout(false);
            verticalStackPanel2.PerformLayout();
            panButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Controls.VerticalStackPanel verticalStackPanel1;
        private Controls.ModernListBox listModels;
        private Controls.VerticalStackPanel verticalStackPanel2;
        private Controls.CollapsibleGroupBox boxSettings;
        private Button bt_newsession;
        private Panel panButtons;
        private Button btApply;
        private Button btLaunch;
        private Button btStop;
        private Label lblServerStatus;
        private ListView lvServerLog;
        private ColumnHeader colLogLevel;
        private ColumnHeader colLogMessage;

        // Settings panel controls (built programmatically)
        private System.Windows.Forms.Panel panSettingsScroll;
        private Controls.ModernNumericUpDown num_port;
        private Controls.ModernNumericUpDown num_threads;
        private Controls.ModernNumericUpDown num_gpuLayers;
        private Controls.ModernNumericUpDown num_contextSize;
        private Controls.ModernNumericUpDown num_reasoningBudget;
        private Controls.ModernComboBox cb_flashAttention;
        private Controls.ModernComboBox cb_reasoning;
        private Controls.ModernCheckBox ck_props;
        private Controls.ModernCheckBox ck_kvToGpu;
        private Controls.ModernCheckBox ck_mlock;
        private Controls.ModernCheckBox ck_mmap;
        private Controls.ModernCheckBox ck_loadMmproj;
        private Controls.ModernCheckBox ck_loadJinja;
        private System.Windows.Forms.TextBox ed_additionalArgs;
    }
}