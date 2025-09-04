namespace WaifuAI.src.forms
{
    partial class ChatHistoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatHistoryForm));
            panel6 = new Panel();
            web_sessioncontent = new Microsoft.Web.WebView2.WinForms.WebView2();
            panel7 = new Panel();
            groupBox25 = new GroupBox();
            ck_hist_casesensitive = new CheckBox();
            ck_hist_kw = new CheckBox();
            ck_hist_sticky = new CheckBox();
            label56 = new Label();
            ed_hist_kw1 = new TextBox();
            label57 = new Label();
            cb_hist_kwlink = new ComboBox();
            label58 = new Label();
            ed_hist_kw2 = new TextBox();
            groupBox12 = new GroupBox();
            ck_hist_isrp = new CheckBox();
            bt_historyupdate = new Button();
            lbl_sessiondata = new Label();
            ed_sessioninfo = new TextBox();
            label64 = new Label();
            ed_sessiontitle = new TextBox();
            bt_sessionrefresh = new Button();
            panel4 = new Panel();
            listSession = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            panel5 = new Panel();
            btEmbedAll = new Button();
            bt_deleteAllHistory = new Button();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)web_sessioncontent).BeginInit();
            panel7.SuspendLayout();
            groupBox25.SuspendLayout();
            groupBox12.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // panel6
            // 
            panel6.Controls.Add(web_sessioncontent);
            panel6.Controls.Add(panel7);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(376, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(808, 685);
            panel6.TabIndex = 2;
            // 
            // web_sessioncontent
            // 
            web_sessioncontent.AllowExternalDrop = true;
            web_sessioncontent.CreationProperties = null;
            web_sessioncontent.DefaultBackgroundColor = Color.White;
            web_sessioncontent.Dock = DockStyle.Fill;
            web_sessioncontent.Location = new Point(0, 308);
            web_sessioncontent.Name = "web_sessioncontent";
            web_sessioncontent.Size = new Size(808, 377);
            web_sessioncontent.TabIndex = 4;
            web_sessioncontent.ZoomFactor = 1D;
            // 
            // panel7
            // 
            panel7.Controls.Add(groupBox25);
            panel7.Controls.Add(groupBox12);
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point(0, 0);
            panel7.Name = "panel7";
            panel7.Size = new Size(808, 308);
            panel7.TabIndex = 3;
            // 
            // groupBox25
            // 
            groupBox25.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox25.Controls.Add(ck_hist_casesensitive);
            groupBox25.Controls.Add(ck_hist_kw);
            groupBox25.Controls.Add(ck_hist_sticky);
            groupBox25.Controls.Add(label56);
            groupBox25.Controls.Add(ed_hist_kw1);
            groupBox25.Controls.Add(label57);
            groupBox25.Controls.Add(cb_hist_kwlink);
            groupBox25.Controls.Add(label58);
            groupBox25.Controls.Add(ed_hist_kw2);
            groupBox25.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox25.Location = new Point(6, 200);
            groupBox25.Name = "groupBox25";
            groupBox25.Size = new Size(794, 101);
            groupBox25.TabIndex = 1;
            groupBox25.TabStop = false;
            groupBox25.Text = "Keyword Activation";
            // 
            // ck_hist_casesensitive
            // 
            ck_hist_casesensitive.AutoSize = true;
            ck_hist_casesensitive.Font = new Font("Segoe UI", 9F);
            ck_hist_casesensitive.Location = new Point(284, 22);
            ck_hist_casesensitive.Name = "ck_hist_casesensitive";
            ck_hist_casesensitive.Size = new Size(100, 19);
            ck_hist_casesensitive.TabIndex = 19;
            ck_hist_casesensitive.Text = "Case Sensitive";
            ck_hist_casesensitive.UseVisualStyleBackColor = true;
            ck_hist_casesensitive.CheckedChanged += UpdateHistoryEntryEvent;
            // 
            // ck_hist_kw
            // 
            ck_hist_kw.AutoSize = true;
            ck_hist_kw.Font = new Font("Segoe UI", 9F);
            ck_hist_kw.Location = new Point(6, 22);
            ck_hist_kw.Name = "ck_hist_kw";
            ck_hist_kw.Size = new Size(150, 19);
            ck_hist_kw.TabIndex = 12;
            ck_hist_kw.Text = "Enable Keyword Trigger";
            ck_hist_kw.UseVisualStyleBackColor = true;
            ck_hist_kw.CheckedChanged += UpdateHistoryEntryEvent;
            // 
            // ck_hist_sticky
            // 
            ck_hist_sticky.AutoSize = true;
            ck_hist_sticky.Font = new Font("Segoe UI", 9F);
            ck_hist_sticky.Location = new Point(162, 22);
            ck_hist_sticky.Name = "ck_hist_sticky";
            ck_hist_sticky.Size = new Size(116, 19);
            ck_hist_sticky.TabIndex = 21;
            ck_hist_sticky.Text = "Always Activated";
            ck_hist_sticky.UseVisualStyleBackColor = true;
            ck_hist_sticky.CheckedChanged += UpdateHistoryEntryEvent;
            // 
            // label56
            // 
            label56.AutoSize = true;
            label56.Font = new Font("Segoe UI", 9F);
            label56.Location = new Point(232, 52);
            label56.Name = "label56";
            label56.Size = new Size(78, 15);
            label56.TabIndex = 18;
            label56.Text = "Keyword Link";
            // 
            // ed_hist_kw1
            // 
            ed_hist_kw1.Font = new Font("Segoe UI", 9F);
            ed_hist_kw1.Location = new Point(6, 70);
            ed_hist_kw1.Name = "ed_hist_kw1";
            ed_hist_kw1.Size = new Size(220, 23);
            ed_hist_kw1.TabIndex = 14;
            ed_hist_kw1.TextChanged += UpdateHistoryEntryEvent;
            // 
            // label57
            // 
            label57.AutoSize = true;
            label57.Font = new Font("Segoe UI", 9F);
            label57.Location = new Point(368, 52);
            label57.Name = "label57";
            label57.Size = new Size(67, 15);
            label57.TabIndex = 15;
            label57.Text = "Keywords 2";
            // 
            // cb_hist_kwlink
            // 
            cb_hist_kwlink.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_hist_kwlink.Font = new Font("Segoe UI", 9F);
            cb_hist_kwlink.FormattingEnabled = true;
            cb_hist_kwlink.Items.AddRange(new object[] { "And", "Or", "Not" });
            cb_hist_kwlink.Location = new Point(232, 70);
            cb_hist_kwlink.Name = "cb_hist_kwlink";
            cb_hist_kwlink.Size = new Size(130, 23);
            cb_hist_kwlink.TabIndex = 17;
            cb_hist_kwlink.SelectedIndexChanged += UpdateHistoryEntryEvent;
            // 
            // label58
            // 
            label58.AutoSize = true;
            label58.Font = new Font("Segoe UI", 9F);
            label58.Location = new Point(6, 52);
            label58.Name = "label58";
            label58.Size = new Size(67, 15);
            label58.TabIndex = 13;
            label58.Text = "Keywords 1";
            // 
            // ed_hist_kw2
            // 
            ed_hist_kw2.Font = new Font("Segoe UI", 9F);
            ed_hist_kw2.Location = new Point(368, 70);
            ed_hist_kw2.Name = "ed_hist_kw2";
            ed_hist_kw2.Size = new Size(220, 23);
            ed_hist_kw2.TabIndex = 16;
            ed_hist_kw2.TextChanged += UpdateHistoryEntryEvent;
            // 
            // groupBox12
            // 
            groupBox12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox12.Controls.Add(ck_hist_isrp);
            groupBox12.Controls.Add(bt_historyupdate);
            groupBox12.Controls.Add(lbl_sessiondata);
            groupBox12.Controls.Add(ed_sessioninfo);
            groupBox12.Controls.Add(label64);
            groupBox12.Controls.Add(ed_sessiontitle);
            groupBox12.Controls.Add(bt_sessionrefresh);
            groupBox12.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox12.Location = new Point(6, 3);
            groupBox12.Name = "groupBox12";
            groupBox12.Size = new Size(794, 191);
            groupBox12.TabIndex = 0;
            groupBox12.TabStop = false;
            groupBox12.Text = "Session Information";
            // 
            // ck_hist_isrp
            // 
            ck_hist_isrp.AutoSize = true;
            ck_hist_isrp.Font = new Font("Segoe UI", 9F);
            ck_hist_isrp.Location = new Point(229, 14);
            ck_hist_isrp.Name = "ck_hist_isrp";
            ck_hist_isrp.Size = new Size(99, 19);
            ck_hist_isrp.TabIndex = 20;
            ck_hist_isrp.Text = "Roleplay Only";
            ck_hist_isrp.UseVisualStyleBackColor = true;
            ck_hist_isrp.CheckedChanged += UpdateHistoryEntryEvent;
            // 
            // bt_historyupdate
            // 
            bt_historyupdate.Location = new Point(335, 11);
            bt_historyupdate.Name = "bt_historyupdate";
            bt_historyupdate.Size = new Size(108, 23);
            bt_historyupdate.TabIndex = 4;
            bt_historyupdate.Text = "Update Entry";
            bt_historyupdate.UseVisualStyleBackColor = true;
            bt_historyupdate.Click += bt_historyupdate_Click;
            // 
            // lbl_sessiondata
            // 
            lbl_sessiondata.AutoSize = true;
            lbl_sessiondata.Font = new Font("Segoe UI", 9F);
            lbl_sessiondata.Location = new Point(6, 63);
            lbl_sessiondata.Name = "lbl_sessiondata";
            lbl_sessiondata.Size = new Size(58, 15);
            lbl_sessiondata.TabIndex = 3;
            lbl_sessiondata.Text = "Summary";
            // 
            // ed_sessioninfo
            // 
            ed_sessioninfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ed_sessioninfo.Font = new Font("Segoe UI", 9F);
            ed_sessioninfo.Location = new Point(6, 81);
            ed_sessioninfo.Multiline = true;
            ed_sessioninfo.Name = "ed_sessioninfo";
            ed_sessioninfo.PlaceholderText = "Select a session from the left panel to show information about it.";
            ed_sessioninfo.ScrollBars = ScrollBars.Vertical;
            ed_sessioninfo.Size = new Size(782, 104);
            ed_sessioninfo.TabIndex = 2;
            ed_sessioninfo.TextChanged += ed_sessioninfo_TextChanged;
            // 
            // label64
            // 
            label64.AutoSize = true;
            label64.Font = new Font("Segoe UI", 9F);
            label64.Location = new Point(6, 19);
            label64.Name = "label64";
            label64.Size = new Size(30, 15);
            label64.TabIndex = 1;
            label64.Text = "Title";
            // 
            // ed_sessiontitle
            // 
            ed_sessiontitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ed_sessiontitle.Font = new Font("Segoe UI", 9F);
            ed_sessiontitle.Location = new Point(6, 37);
            ed_sessiontitle.Name = "ed_sessiontitle";
            ed_sessiontitle.PlaceholderText = "No Session Selected";
            ed_sessiontitle.Size = new Size(782, 23);
            ed_sessiontitle.TabIndex = 0;
            ed_sessiontitle.TextChanged += ed_sessiontitle_TextChanged;
            // 
            // bt_sessionrefresh
            // 
            bt_sessionrefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bt_sessionrefresh.Location = new Point(637, 11);
            bt_sessionrefresh.Name = "bt_sessionrefresh";
            bt_sessionrefresh.Size = new Size(151, 23);
            bt_sessionrefresh.TabIndex = 2;
            bt_sessionrefresh.Text = "Generate Summary";
            bt_sessionrefresh.UseVisualStyleBackColor = true;
            bt_sessionrefresh.Click += bt_sessionrefresh_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(listSession);
            panel4.Controls.Add(panel5);
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(376, 685);
            panel4.TabIndex = 0;
            // 
            // listSession
            // 
            listSession.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listSession.Dock = DockStyle.Fill;
            listSession.FullRowSelect = true;
            listSession.Location = new Point(0, 0);
            listSession.Name = "listSession";
            listSession.Size = new Size(376, 619);
            listSession.TabIndex = 2;
            listSession.UseCompatibleStateImageBehavior = false;
            listSession.View = View.Details;
            listSession.SelectedIndexChanged += listSession_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Title";
            columnHeader1.Width = 280;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Date";
            columnHeader2.Width = 80;
            // 
            // panel5
            // 
            panel5.Controls.Add(btEmbedAll);
            panel5.Controls.Add(bt_deleteAllHistory);
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(0, 619);
            panel5.Name = "panel5";
            panel5.Size = new Size(376, 66);
            panel5.TabIndex = 1;
            // 
            // btEmbedAll
            // 
            btEmbedAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btEmbedAll.BackColor = SystemColors.Control;
            btEmbedAll.Font = new Font("Segoe UI", 9F);
            btEmbedAll.Location = new Point(3, 6);
            btEmbedAll.Name = "btEmbedAll";
            btEmbedAll.Size = new Size(367, 23);
            btEmbedAll.TabIndex = 5;
            btEmbedAll.Text = "Embed Everything";
            btEmbedAll.UseVisualStyleBackColor = false;
            btEmbedAll.Click += btEmbedAll_Click;
            // 
            // bt_deleteAllHistory
            // 
            bt_deleteAllHistory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bt_deleteAllHistory.BackColor = SystemColors.Control;
            bt_deleteAllHistory.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_deleteAllHistory.ForeColor = Color.DarkRed;
            bt_deleteAllHistory.Location = new Point(3, 35);
            bt_deleteAllHistory.Name = "bt_deleteAllHistory";
            bt_deleteAllHistory.Size = new Size(367, 23);
            bt_deleteAllHistory.TabIndex = 4;
            bt_deleteAllHistory.Text = "Delete All History";
            bt_deleteAllHistory.UseVisualStyleBackColor = false;
            bt_deleteAllHistory.Click += bt_deleteAllHistory_Click;
            // 
            // ChatHistoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 685);
            Controls.Add(panel6);
            Controls.Add(panel4);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            MinimumSize = new Size(800, 600);
            Name = "ChatHistoryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chat History";
            Load += ChatHistoryForm_Load;
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)web_sessioncontent).EndInit();
            panel7.ResumeLayout(false);
            groupBox25.ResumeLayout(false);
            groupBox25.PerformLayout();
            groupBox12.ResumeLayout(false);
            groupBox12.PerformLayout();
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel6;
        private Microsoft.Web.WebView2.WinForms.WebView2 web_sessioncontent;
        private Panel panel7;
        private GroupBox groupBox25;
        private CheckBox ck_hist_casesensitive;
        private CheckBox ck_hist_kw;
        private CheckBox ck_hist_sticky;
        private Label label56;
        private TextBox ed_hist_kw1;
        private Label label57;
        private ComboBox cb_hist_kwlink;
        private Label label58;
        private TextBox ed_hist_kw2;
        private GroupBox groupBox12;
        private CheckBox ck_hist_isrp;
        private Button bt_historyupdate;
        private Label lbl_sessiondata;
        private TextBox ed_sessioninfo;
        private Label label64;
        private TextBox ed_sessiontitle;
        private Button bt_sessionrefresh;
        private Panel panel4;
        private ListView listSession;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Panel panel5;
        private Button btEmbedAll;
        private Button bt_deleteAllHistory;
    }
}