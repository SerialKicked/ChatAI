namespace LetheChat.src.forms
{
    partial class RawLogForm
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
            txtSearchRes = new TextBox();
            listSystemLog = new ListView();
            colTime = new ColumnHeader();
            colLevel = new ColumnHeader();
            colMessage = new ColumnHeader();
            btClose = new Button();
            modernTabControl1 = new LetheChat.Controls.ModernTabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            modernTabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // txtSearchRes
            // 
            txtSearchRes.BorderStyle = BorderStyle.FixedSingle;
            txtSearchRes.Dock = DockStyle.Fill;
            txtSearchRes.Location = new Point(3, 3);
            txtSearchRes.Multiline = true;
            txtSearchRes.Name = "txtSearchRes";
            txtSearchRes.ScrollBars = ScrollBars.Vertical;
            txtSearchRes.Size = new Size(850, 529);
            txtSearchRes.TabIndex = 3;
            // 
            // listSystemLog
            // 
            listSystemLog.Columns.AddRange(new ColumnHeader[] { colTime, colLevel, colMessage });
            listSystemLog.Dock = DockStyle.Fill;
            listSystemLog.FullRowSelect = true;
            listSystemLog.Location = new Point(3, 3);
            listSystemLog.MultiSelect = false;
            listSystemLog.Name = "listSystemLog";
            listSystemLog.Size = new Size(850, 529);
            listSystemLog.TabIndex = 0;
            listSystemLog.UseCompatibleStateImageBehavior = false;
            listSystemLog.View = View.Details;
            // 
            // colTime
            // 
            colTime.Text = "Time";
            colTime.Width = 150;
            // 
            // colLevel
            // 
            colLevel.Text = "Level";
            colLevel.Width = 100;
            // 
            // colMessage
            // 
            colMessage.Text = "Message";
            colMessage.Width = 560;
            // 
            // btClose
            // 
            btClose.BackColor = Color.PaleGreen;
            btClose.Dock = DockStyle.Bottom;
            btClose.FlatStyle = FlatStyle.Flat;
            btClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btClose.Location = new Point(0, 579);
            btClose.Name = "btClose";
            btClose.Size = new Size(864, 23);
            btClose.TabIndex = 5;
            btClose.Tag = "no-theme";
            btClose.Text = "Close";
            btClose.UseVisualStyleBackColor = false;
            btClose.Click += button1_Click;
            // 
            // modernTabControl1
            // 
            modernTabControl1.Appearance = TabAppearance.FlatButtons;
            modernTabControl1.Controls.Add(tabPage1);
            modernTabControl1.Controls.Add(tabPage2);
            modernTabControl1.Dock = DockStyle.Fill;
            modernTabControl1.Font = new Font("Segoe UI", 9F);
            modernTabControl1.ItemSize = new Size(0, 36);
            modernTabControl1.Location = new Point(0, 0);
            modernTabControl1.Name = "modernTabControl1";
            modernTabControl1.SelectedIndex = 0;
            modernTabControl1.Size = new Size(864, 579);
            modernTabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(37, 37, 37);
            tabPage1.Controls.Add(txtSearchRes);
            tabPage1.Font = new Font("Segoe UI", 9F);
            tabPage1.ForeColor = Color.FromArgb(230, 230, 230);
            tabPage1.Location = new Point(4, 40);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(856, 535);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Full Prompt";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(37, 37, 37);
            tabPage2.Controls.Add(listSystemLog);
            tabPage2.Font = new Font("Segoe UI", 9F);
            tabPage2.ForeColor = Color.FromArgb(230, 230, 230);
            tabPage2.Location = new Point(4, 40);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(856, 535);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "System Logs";
            // 
            // RawLogForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(864, 602);
            Controls.Add(modernTabControl1);
            Controls.Add(btClose);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RawLogForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Raw Message Log";
            modernTabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtSearchRes;
        private Button btClose;
        private Controls.ModernTabControl modernTabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ListView listSystemLog;
        private ColumnHeader colTime;
        private ColumnHeader colLevel;
        private ColumnHeader colMessage;
    }
}