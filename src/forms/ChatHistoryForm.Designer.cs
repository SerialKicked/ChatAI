using LetheChat.Controls;

namespace LetheChat.Forms
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
            collapsibleGroupBox3 = new CollapsibleGroupBox();
            web_sessioncontent = new Microsoft.Web.WebView2.WinForms.WebView2();
            bt_historyupdate = new Button();
            button1 = new Button();
            label64 = new Label();
            ck_hist_isrp = new ModernCheckBox();
            bt_sessionrefresh = new Button();
            ed_sessiontitle = new TextBox();
            ed_sessioninfo = new TextBox();
            lbl_sessiondata = new Label();
            ck_hist_casesensitive = new ModernCheckBox();
            ck_hist_kw = new ModernCheckBox();
            ed_hist_kw2 = new TextBox();
            ck_hist_sticky = new ModernCheckBox();
            label58 = new Label();
            label56 = new Label();
            cb_hist_kwlink = new ModernComboBox();
            ed_hist_kw1 = new TextBox();
            label57 = new Label();
            panel4 = new Panel();
            listSession = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            panel5 = new Panel();
            btEmbedAll = new Button();
            bt_deleteAllHistory = new Button();
            verticalStackPanel1 = new Panel();
            modernTabControl1 = new ModernTabControl();
            tabPage1 = new TabPage();
            button2 = new Button();
            tabPage2 = new TabPage();
            columnHeader3 = new ColumnHeader();
            collapsibleGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)web_sessioncontent).BeginInit();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            verticalStackPanel1.SuspendLayout();
            modernTabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // collapsibleGroupBox3
            // 
            collapsibleGroupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            collapsibleGroupBox3.BackColor = Color.FromArgb(37, 37, 37);
            collapsibleGroupBox3.Controls.Add(web_sessioncontent);
            collapsibleGroupBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox3.Location = new Point(8, 283);
            collapsibleGroupBox3.Name = "collapsibleGroupBox3";
            collapsibleGroupBox3.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox3.Size = new Size(823, 387);
            collapsibleGroupBox3.TabIndex = 5;
            collapsibleGroupBox3.Text = "Content";
            // 
            // web_sessioncontent
            // 
            web_sessioncontent.AllowExternalDrop = true;
            web_sessioncontent.CreationProperties = null;
            web_sessioncontent.DefaultBackgroundColor = Color.White;
            web_sessioncontent.Dock = DockStyle.Fill;
            web_sessioncontent.Location = new Point(12, 32);
            web_sessioncontent.Name = "web_sessioncontent";
            web_sessioncontent.Size = new Size(799, 345);
            web_sessioncontent.TabIndex = 4;
            web_sessioncontent.ZoomFactor = 1D;
            // 
            // bt_historyupdate
            // 
            bt_historyupdate.FlatStyle = FlatStyle.Flat;
            bt_historyupdate.Location = new Point(6, 186);
            bt_historyupdate.Name = "bt_historyupdate";
            bt_historyupdate.Size = new Size(108, 28);
            bt_historyupdate.TabIndex = 4;
            bt_historyupdate.Tag = "no-theme";
            bt_historyupdate.Text = "Update Embeds";
            bt_historyupdate.UseVisualStyleBackColor = true;
            bt_historyupdate.Click += bt_historyupdate_Click;
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(120, 186);
            button1.Name = "button1";
            button1.Size = new Size(90, 28);
            button1.TabIndex = 21;
            button1.Tag = "no-theme";
            button1.Text = "Mood Check";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label64
            // 
            label64.AutoSize = true;
            label64.Font = new Font("Segoe UI", 9F);
            label64.Location = new Point(6, 14);
            label64.Name = "label64";
            label64.Size = new Size(30, 15);
            label64.TabIndex = 1;
            label64.Text = "Title";
            // 
            // ck_hist_isrp
            // 
            ck_hist_isrp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ck_hist_isrp.Font = new Font("Segoe UI", 9F);
            ck_hist_isrp.Location = new Point(640, 7);
            ck_hist_isrp.Name = "ck_hist_isrp";
            ck_hist_isrp.Size = new Size(159, 19);
            ck_hist_isrp.TabIndex = 20;
            ck_hist_isrp.Text = "Roleplay Only";
            ck_hist_isrp.UseVisualStyleBackColor = true;
            ck_hist_isrp.CheckedChanged += UpdateHistoryEntryEvent;
            // 
            // bt_sessionrefresh
            // 
            bt_sessionrefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bt_sessionrefresh.BackColor = Color.DarkSeaGreen;
            bt_sessionrefresh.FlatStyle = FlatStyle.Flat;
            bt_sessionrefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_sessionrefresh.ForeColor = Color.Black;
            bt_sessionrefresh.Location = new Point(648, 186);
            bt_sessionrefresh.Name = "bt_sessionrefresh";
            bt_sessionrefresh.Size = new Size(151, 28);
            bt_sessionrefresh.TabIndex = 2;
            bt_sessionrefresh.Tag = "no-theme";
            bt_sessionrefresh.Text = "Regenerate Entry";
            bt_sessionrefresh.UseVisualStyleBackColor = false;
            bt_sessionrefresh.Click += bt_sessionrefresh_Click;
            // 
            // ed_sessiontitle
            // 
            ed_sessiontitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ed_sessiontitle.Font = new Font("Segoe UI", 9F);
            ed_sessiontitle.Location = new Point(6, 32);
            ed_sessiontitle.Name = "ed_sessiontitle";
            ed_sessiontitle.PlaceholderText = "No Session Selected";
            ed_sessiontitle.Size = new Size(793, 23);
            ed_sessiontitle.TabIndex = 0;
            ed_sessiontitle.TextChanged += ed_sessiontitle_TextChanged;
            // 
            // ed_sessioninfo
            // 
            ed_sessioninfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ed_sessioninfo.Font = new Font("Segoe UI", 9F);
            ed_sessioninfo.Location = new Point(6, 76);
            ed_sessioninfo.Multiline = true;
            ed_sessioninfo.Name = "ed_sessioninfo";
            ed_sessioninfo.PlaceholderText = "Select a session from the left panel to show information about it.";
            ed_sessioninfo.ScrollBars = ScrollBars.Vertical;
            ed_sessioninfo.Size = new Size(793, 104);
            ed_sessioninfo.TabIndex = 2;
            ed_sessioninfo.TextChanged += ed_sessioninfo_TextChanged;
            // 
            // lbl_sessiondata
            // 
            lbl_sessiondata.AutoSize = true;
            lbl_sessiondata.Font = new Font("Segoe UI", 9F);
            lbl_sessiondata.Location = new Point(6, 58);
            lbl_sessiondata.Name = "lbl_sessiondata";
            lbl_sessiondata.Size = new Size(58, 15);
            lbl_sessiondata.TabIndex = 3;
            lbl_sessiondata.Text = "Summary";
            lbl_sessiondata.Click += lbl_sessiondata_Click;
            // 
            // ck_hist_casesensitive
            // 
            ck_hist_casesensitive.Font = new Font("Segoe UI", 9F);
            ck_hist_casesensitive.Location = new Point(611, 55);
            ck_hist_casesensitive.Name = "ck_hist_casesensitive";
            ck_hist_casesensitive.Size = new Size(141, 19);
            ck_hist_casesensitive.TabIndex = 19;
            ck_hist_casesensitive.Text = "Case Sensitive";
            ck_hist_casesensitive.UseVisualStyleBackColor = true;
            ck_hist_casesensitive.CheckedChanged += UpdateHistoryEntryEvent;
            // 
            // ck_hist_kw
            // 
            ck_hist_kw.Font = new Font("Segoe UI", 9F);
            ck_hist_kw.Location = new Point(6, 14);
            ck_hist_kw.Name = "ck_hist_kw";
            ck_hist_kw.Size = new Size(209, 19);
            ck_hist_kw.TabIndex = 12;
            ck_hist_kw.Text = "Enable Keyword Trigger";
            ck_hist_kw.UseVisualStyleBackColor = true;
            ck_hist_kw.CheckedChanged += UpdateHistoryEntryEvent;
            // 
            // ed_hist_kw2
            // 
            ed_hist_kw2.BorderStyle = BorderStyle.FixedSingle;
            ed_hist_kw2.Font = new Font("Segoe UI", 9F);
            ed_hist_kw2.Location = new Point(385, 54);
            ed_hist_kw2.Name = "ed_hist_kw2";
            ed_hist_kw2.Size = new Size(220, 23);
            ed_hist_kw2.TabIndex = 16;
            ed_hist_kw2.TextChanged += UpdateHistoryEntryEvent;
            // 
            // ck_hist_sticky
            // 
            ck_hist_sticky.Font = new Font("Segoe UI", 9F);
            ck_hist_sticky.Location = new Point(8, 99);
            ck_hist_sticky.Name = "ck_hist_sticky";
            ck_hist_sticky.Size = new Size(183, 19);
            ck_hist_sticky.TabIndex = 21;
            ck_hist_sticky.Text = "Always Activated";
            ck_hist_sticky.UseVisualStyleBackColor = true;
            ck_hist_sticky.CheckedChanged += UpdateHistoryEntryEvent;
            // 
            // label58
            // 
            label58.AutoSize = true;
            label58.Font = new Font("Segoe UI", 9F);
            label58.Location = new Point(8, 36);
            label58.Name = "label58";
            label58.Size = new Size(67, 15);
            label58.TabIndex = 13;
            label58.Text = "Keywords 1";
            // 
            // label56
            // 
            label56.AutoSize = true;
            label56.Font = new Font("Segoe UI", 9F);
            label56.Location = new Point(234, 36);
            label56.Name = "label56";
            label56.Size = new Size(78, 15);
            label56.TabIndex = 18;
            label56.Text = "Keyword Link";
            // 
            // cb_hist_kwlink
            // 
            cb_hist_kwlink.BackColor = Color.FromArgb(64, 64, 64);
            cb_hist_kwlink.DropDownHeight = 180;
            cb_hist_kwlink.Font = new Font("Segoe UI", 9F);
            cb_hist_kwlink.Items.AddRange(new object[] { "And", "Or", "Not" });
            cb_hist_kwlink.Location = new Point(234, 54);
            cb_hist_kwlink.MaxDropDownItems = 10;
            cb_hist_kwlink.Name = "cb_hist_kwlink";
            cb_hist_kwlink.Padding = new Padding(1);
            cb_hist_kwlink.Size = new Size(145, 23);
            cb_hist_kwlink.TabIndex = 17;
            cb_hist_kwlink.SelectedIndexChanged += UpdateHistoryEntryEvent;
            // 
            // ed_hist_kw1
            // 
            ed_hist_kw1.BorderStyle = BorderStyle.FixedSingle;
            ed_hist_kw1.Font = new Font("Segoe UI", 9F);
            ed_hist_kw1.Location = new Point(8, 54);
            ed_hist_kw1.Name = "ed_hist_kw1";
            ed_hist_kw1.Size = new Size(220, 23);
            ed_hist_kw1.TabIndex = 14;
            ed_hist_kw1.TextChanged += UpdateHistoryEntryEvent;
            // 
            // label57
            // 
            label57.AutoSize = true;
            label57.Font = new Font("Segoe UI", 9F);
            label57.Location = new Point(385, 36);
            label57.Name = "label57";
            label57.Size = new Size(67, 15);
            label57.TabIndex = 15;
            label57.Text = "Keywords 2";
            // 
            // panel4
            // 
            panel4.Controls.Add(listSession);
            panel4.Controls.Add(panel5);
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(376, 681);
            panel4.TabIndex = 0;
            // 
            // listSession
            // 
            listSession.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            listSession.Dock = DockStyle.Fill;
            listSession.FullRowSelect = true;
            listSession.Location = new Point(0, 0);
            listSession.Name = "listSession";
            listSession.Size = new Size(376, 601);
            listSession.TabIndex = 2;
            listSession.UseCompatibleStateImageBehavior = false;
            listSession.View = View.Details;
            listSession.SelectedIndexChanged += listSession_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Title";
            columnHeader1.Width = 240;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Date";
            columnHeader2.Width = 90;
            // 
            // panel5
            // 
            panel5.Controls.Add(btEmbedAll);
            panel5.Controls.Add(bt_deleteAllHistory);
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(0, 601);
            panel5.Name = "panel5";
            panel5.Size = new Size(376, 80);
            panel5.TabIndex = 1;
            // 
            // btEmbedAll
            // 
            btEmbedAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btEmbedAll.BackColor = Color.Black;
            btEmbedAll.FlatStyle = FlatStyle.Flat;
            btEmbedAll.Font = new Font("Segoe UI", 9F);
            btEmbedAll.Location = new Point(6, 6);
            btEmbedAll.Name = "btEmbedAll";
            btEmbedAll.Size = new Size(364, 28);
            btEmbedAll.TabIndex = 5;
            btEmbedAll.Tag = "no-theme";
            btEmbedAll.Text = "Embed Everything";
            btEmbedAll.UseVisualStyleBackColor = false;
            btEmbedAll.Click += btEmbedAll_Click;
            // 
            // bt_deleteAllHistory
            // 
            bt_deleteAllHistory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bt_deleteAllHistory.BackColor = Color.Red;
            bt_deleteAllHistory.FlatStyle = FlatStyle.Flat;
            bt_deleteAllHistory.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_deleteAllHistory.ForeColor = Color.Black;
            bt_deleteAllHistory.Location = new Point(6, 40);
            bt_deleteAllHistory.Name = "bt_deleteAllHistory";
            bt_deleteAllHistory.Size = new Size(364, 28);
            bt_deleteAllHistory.TabIndex = 4;
            bt_deleteAllHistory.Tag = "no-theme";
            bt_deleteAllHistory.Text = "Delete All History";
            bt_deleteAllHistory.UseVisualStyleBackColor = false;
            bt_deleteAllHistory.Click += bt_deleteAllHistory_Click;
            // 
            // verticalStackPanel1
            // 
            verticalStackPanel1.Controls.Add(modernTabControl1);
            verticalStackPanel1.Controls.Add(collapsibleGroupBox3);
            verticalStackPanel1.Dock = DockStyle.Fill;
            verticalStackPanel1.Location = new Point(376, 0);
            verticalStackPanel1.Margin = new Padding(8);
            verticalStackPanel1.Name = "verticalStackPanel1";
            verticalStackPanel1.Padding = new Padding(8);
            verticalStackPanel1.Size = new Size(839, 681);
            verticalStackPanel1.TabIndex = 3;
            // 
            // modernTabControl1
            // 
            modernTabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            modernTabControl1.Appearance = TabAppearance.Buttons;
            modernTabControl1.Controls.Add(tabPage1);
            modernTabControl1.Controls.Add(tabPage2);
            modernTabControl1.Font = new Font("Segoe UI", 9F);
            modernTabControl1.ItemSize = new Size(0, 36);
            modernTabControl1.Location = new Point(8, 12);
            modernTabControl1.Name = "modernTabControl1";
            modernTabControl1.SelectedIndex = 0;
            modernTabControl1.Size = new Size(823, 265);
            modernTabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(37, 37, 37);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(bt_historyupdate);
            tabPage1.Controls.Add(label64);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(lbl_sessiondata);
            tabPage1.Controls.Add(ed_sessioninfo);
            tabPage1.Controls.Add(ck_hist_isrp);
            tabPage1.Controls.Add(ed_sessiontitle);
            tabPage1.Controls.Add(bt_sessionrefresh);
            tabPage1.Font = new Font("Segoe UI", 9F);
            tabPage1.ForeColor = Color.FromArgb(230, 230, 230);
            tabPage1.Location = new Point(4, 40);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(815, 221);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Session Info";
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(216, 186);
            button2.Name = "button2";
            button2.Size = new Size(119, 28);
            button2.TabIndex = 22;
            button2.Tag = "no-theme";
            button2.Text = "Move to top";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(37, 37, 37);
            tabPage2.Controls.Add(ck_hist_casesensitive);
            tabPage2.Controls.Add(ck_hist_kw);
            tabPage2.Controls.Add(label57);
            tabPage2.Controls.Add(ed_hist_kw2);
            tabPage2.Controls.Add(ed_hist_kw1);
            tabPage2.Controls.Add(ck_hist_sticky);
            tabPage2.Controls.Add(cb_hist_kwlink);
            tabPage2.Controls.Add(label58);
            tabPage2.Controls.Add(label56);
            tabPage2.Font = new Font("Segoe UI", 9F);
            tabPage2.ForeColor = Color.FromArgb(230, 230, 230);
            tabPage2.Location = new Point(4, 40);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(815, 221);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Activation";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "ID";
            // 
            // ChatHistoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1215, 681);
            Controls.Add(verticalStackPanel1);
            Controls.Add(panel4);
            ForeColor = Color.WhiteSmoke;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            MinimumSize = new Size(800, 600);
            Name = "ChatHistoryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chat History";
            Load += ChatHistoryForm_Load;
            KeyDown += ChatHistoryForm_KeyDown;
            collapsibleGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)web_sessioncontent).EndInit();
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            verticalStackPanel1.ResumeLayout(false);
            modernTabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Microsoft.Web.WebView2.WinForms.WebView2 web_sessioncontent;
        private ModernCheckBox ck_hist_casesensitive;
        private ModernCheckBox ck_hist_kw;
        private ModernCheckBox ck_hist_sticky;
        private Label label56;
        private TextBox ed_hist_kw1;
        private Label label57;
        private ModernComboBox cb_hist_kwlink;
        private Label label58;
        private TextBox ed_hist_kw2;
        private ModernCheckBox ck_hist_isrp;
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
        private Button button1;
        private Controls.CollapsibleGroupBox collapsibleGroupBox3;
        private Panel verticalStackPanel1;
        private ModernTabControl modernTabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button button2;
        private ColumnHeader columnHeader3;
    }
}