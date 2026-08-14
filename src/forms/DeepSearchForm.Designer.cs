namespace LetheChat.Forms
{
    partial class DeepSearchForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panTop = new Panel();
            btCancel = new Button();
            btSearch = new Button();
            edQuery = new TextBox();
            lblQuery = new Label();
            lblStatus = new Label();
            splitMain = new SplitContainer();
            listProgress = new ListView();
            colPhase = new ColumnHeader();
            colRound = new ColumnHeader();
            colMessage = new ColumnHeader();
            tabResults = new LetheChat.Controls.ModernTabControl();
            tabFinal = new TabPage();
            webFinalReport = new Microsoft.Web.WebView2.WinForms.WebView2();
            tabEvolving = new TabPage();
            webEvolvingReport = new Microsoft.Web.WebView2.WinForms.WebView2();
            tabSources = new TabPage();
            listSources = new ListView();
            colSourceTitle = new ColumnHeader();
            colSourceUrl = new ColumnHeader();
            colSourceSummary = new ColumnHeader();
            tabPlan = new TabPage();
            txtPlan = new RichTextBox();
            panBottom = new Panel();
            btClose = new Button();
            panTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            tabResults.SuspendLayout();
            tabFinal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webFinalReport).BeginInit();
            tabEvolving.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webEvolvingReport).BeginInit();
            tabSources.SuspendLayout();
            tabPlan.SuspendLayout();
            panBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panTop
            // 
            panTop.Controls.Add(btCancel);
            panTop.Controls.Add(btSearch);
            panTop.Controls.Add(edQuery);
            panTop.Controls.Add(lblQuery);
            panTop.Controls.Add(lblStatus);
            panTop.Dock = DockStyle.Top;
            panTop.Location = new Point(0, 0);
            panTop.Name = "panTop";
            panTop.Padding = new Padding(6, 4, 6, 4);
            panTop.Size = new Size(1089, 68);
            panTop.TabIndex = 0;
            // 
            // btCancel
            // 
            btCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btCancel.BackColor = Color.DarkRed;
            btCancel.Enabled = false;
            btCancel.FlatAppearance.BorderColor = Color.Black;
            btCancel.FlatStyle = FlatStyle.Flat;
            btCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btCancel.ForeColor = Color.White;
            btCancel.Location = new Point(1002, 7);
            btCancel.Name = "btCancel";
            btCancel.Size = new Size(75, 26);
            btCancel.TabIndex = 2;
            btCancel.Tag = "no-theme";
            btCancel.Text = "Cancel";
            btCancel.UseVisualStyleBackColor = false;
            btCancel.Click += btCancel_Click;
            // 
            // btSearch
            // 
            btSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btSearch.BackColor = Color.DarkSeaGreen;
            btSearch.FlatStyle = FlatStyle.Flat;
            btSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btSearch.ForeColor = Color.Black;
            btSearch.Location = new Point(921, 7);
            btSearch.Name = "btSearch";
            btSearch.Size = new Size(75, 26);
            btSearch.TabIndex = 1;
            btSearch.Tag = "no-theme";
            btSearch.Text = "Search";
            btSearch.UseVisualStyleBackColor = false;
            btSearch.Click += btSearch_Click;
            // 
            // edQuery
            // 
            edQuery.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edQuery.BackColor = Color.FromArgb(50, 52, 56);
            edQuery.BorderStyle = BorderStyle.FixedSingle;
            edQuery.Font = new Font("Segoe UI", 9F);
            edQuery.ForeColor = Color.WhiteSmoke;
            edQuery.Location = new Point(62, 8);
            edQuery.Name = "edQuery";
            edQuery.Size = new Size(853, 23);
            edQuery.TabIndex = 0;
            edQuery.KeyDown += edQuery_KeyDown;
            // 
            // lblQuery
            // 
            lblQuery.AutoSize = true;
            lblQuery.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblQuery.ForeColor = Color.WhiteSmoke;
            lblQuery.Location = new Point(12, 10);
            lblQuery.Name = "lblQuery";
            lblQuery.Size = new Size(44, 15);
            lblQuery.TabIndex = 3;
            lblQuery.Text = "Query:";
            // 
            // lblStatus
            // 
            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Font = new Font("Segoe UI", 8.5F);
            lblStatus.ForeColor = Color.LightSkyBlue;
            lblStatus.Location = new Point(6, 42);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(4, 0, 0, 0);
            lblStatus.Size = new Size(1077, 22);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Ready.";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.FixedPanel = FixedPanel.Panel1;
            splitMain.Location = new Point(0, 68);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(listProgress);
            splitMain.Panel1MinSize = 120;
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(tabResults);
            splitMain.Panel2MinSize = 300;
            splitMain.Size = new Size(1089, 576);
            splitMain.SplitterDistance = 280;
            splitMain.TabIndex = 1;
            // 
            // listProgress
            // 
            listProgress.BackColor = Color.FromArgb(30, 30, 30);
            listProgress.BorderStyle = BorderStyle.None;
            listProgress.Columns.AddRange(new ColumnHeader[] { colPhase, colRound, colMessage });
            listProgress.Dock = DockStyle.Fill;
            listProgress.Font = new Font("Segoe UI", 8.5F);
            listProgress.ForeColor = Color.FromArgb(200, 200, 200);
            listProgress.FullRowSelect = true;
            listProgress.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listProgress.Location = new Point(0, 0);
            listProgress.Name = "listProgress";
            listProgress.Size = new Size(280, 576);
            listProgress.TabIndex = 0;
            listProgress.UseCompatibleStateImageBehavior = false;
            listProgress.View = View.Details;
            // 
            // colPhase
            // 
            colPhase.Text = "Phase";
            colPhase.Width = 80;
            // 
            // colRound
            // 
            colRound.Text = "Round";
            colRound.Width = 46;
            // 
            // colMessage
            // 
            colMessage.Text = "Message";
            colMessage.Width = 500;
            // 
            // tabResults
            // 
            tabResults.Appearance = TabAppearance.FlatButtons;
            tabResults.Controls.Add(tabFinal);
            tabResults.Controls.Add(tabEvolving);
            tabResults.Controls.Add(tabSources);
            tabResults.Controls.Add(tabPlan);
            tabResults.Dock = DockStyle.Fill;
            tabResults.Font = new Font("Segoe UI", 9F);
            tabResults.ItemSize = new Size(0, 36);
            tabResults.Location = new Point(0, 0);
            tabResults.Name = "tabResults";
            tabResults.SelectedIndex = 0;
            tabResults.Size = new Size(805, 576);
            tabResults.TabIndex = 0;
            // 
            // tabFinal
            // 
            tabFinal.BackColor = Color.FromArgb(37, 37, 37);
            tabFinal.Controls.Add(webFinalReport);
            tabFinal.Font = new Font("Segoe UI", 9F);
            tabFinal.ForeColor = Color.FromArgb(230, 230, 230);
            tabFinal.Location = new Point(4, 40);
            tabFinal.Name = "tabFinal";
            tabFinal.Padding = new Padding(3);
            tabFinal.Size = new Size(797, 532);
            tabFinal.TabIndex = 0;
            tabFinal.Text = "Final Report";
            // 
            // webFinalReport
            // 
            webFinalReport.AllowExternalDrop = false;
            webFinalReport.CreationProperties = null;
            webFinalReport.DefaultBackgroundColor = Color.FromArgb(15, 17, 23);
            webFinalReport.Dock = DockStyle.Fill;
            webFinalReport.Location = new Point(3, 3);
            webFinalReport.Name = "webFinalReport";
            webFinalReport.Size = new Size(791, 526);
            webFinalReport.TabIndex = 0;
            webFinalReport.ZoomFactor = 1D;
            // 
            // tabEvolving
            // 
            tabEvolving.BackColor = Color.FromArgb(37, 37, 37);
            tabEvolving.Controls.Add(webEvolvingReport);
            tabEvolving.Font = new Font("Segoe UI", 9F);
            tabEvolving.ForeColor = Color.FromArgb(230, 230, 230);
            tabEvolving.Location = new Point(4, 40);
            tabEvolving.Name = "tabEvolving";
            tabEvolving.Padding = new Padding(3);
            tabEvolving.Size = new Size(797, 526);
            tabEvolving.TabIndex = 1;
            tabEvolving.Text = "Evolving Report";
            // 
            // webEvolvingReport
            // 
            webEvolvingReport.AllowExternalDrop = false;
            webEvolvingReport.CreationProperties = null;
            webEvolvingReport.DefaultBackgroundColor = Color.FromArgb(15, 17, 23);
            webEvolvingReport.Dock = DockStyle.Fill;
            webEvolvingReport.Location = new Point(3, 3);
            webEvolvingReport.Name = "webEvolvingReport";
            webEvolvingReport.Size = new Size(791, 520);
            webEvolvingReport.TabIndex = 0;
            webEvolvingReport.ZoomFactor = 1D;
            // 
            // tabSources
            // 
            tabSources.BackColor = Color.FromArgb(37, 37, 37);
            tabSources.Controls.Add(listSources);
            tabSources.Font = new Font("Segoe UI", 9F);
            tabSources.ForeColor = Color.FromArgb(230, 230, 230);
            tabSources.Location = new Point(4, 40);
            tabSources.Name = "tabSources";
            tabSources.Padding = new Padding(3);
            tabSources.Size = new Size(797, 526);
            tabSources.TabIndex = 2;
            tabSources.Text = "Sources";
            // 
            // listSources
            // 
            listSources.BackColor = Color.FromArgb(30, 30, 30);
            listSources.BorderStyle = BorderStyle.None;
            listSources.Columns.AddRange(new ColumnHeader[] { colSourceTitle, colSourceUrl, colSourceSummary });
            listSources.Dock = DockStyle.Fill;
            listSources.Font = new Font("Segoe UI", 8.5F);
            listSources.ForeColor = Color.FromArgb(200, 200, 200);
            listSources.FullRowSelect = true;
            listSources.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listSources.Location = new Point(3, 3);
            listSources.Name = "listSources";
            listSources.Size = new Size(791, 520);
            listSources.TabIndex = 0;
            listSources.UseCompatibleStateImageBehavior = false;
            listSources.View = View.Details;
            // 
            // colSourceTitle
            // 
            colSourceTitle.Text = "Title";
            colSourceTitle.Width = 180;
            // 
            // colSourceUrl
            // 
            colSourceUrl.Text = "URL";
            colSourceUrl.Width = 240;
            // 
            // colSourceSummary
            // 
            colSourceSummary.Text = "Summary";
            colSourceSummary.Width = 400;
            // 
            // tabPlan
            // 
            tabPlan.BackColor = Color.FromArgb(37, 37, 37);
            tabPlan.Controls.Add(txtPlan);
            tabPlan.Font = new Font("Segoe UI", 9F);
            tabPlan.ForeColor = Color.FromArgb(230, 230, 230);
            tabPlan.Location = new Point(4, 40);
            tabPlan.Name = "tabPlan";
            tabPlan.Padding = new Padding(3);
            tabPlan.Size = new Size(797, 526);
            tabPlan.TabIndex = 3;
            tabPlan.Text = "Research Plan";
            // 
            // txtPlan
            // 
            txtPlan.BackColor = Color.FromArgb(37, 37, 37);
            txtPlan.BorderStyle = BorderStyle.None;
            txtPlan.Dock = DockStyle.Fill;
            txtPlan.Font = new Font("Segoe UI", 10F);
            txtPlan.ForeColor = Color.FromArgb(200, 200, 200);
            txtPlan.Location = new Point(3, 3);
            txtPlan.Name = "txtPlan";
            txtPlan.ReadOnly = true;
            txtPlan.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtPlan.Size = new Size(791, 520);
            txtPlan.TabIndex = 0;
            txtPlan.Tag = "no-theme";
            txtPlan.Text = "";
            // 
            // panBottom
            // 
            panBottom.Controls.Add(btClose);
            panBottom.Dock = DockStyle.Bottom;
            panBottom.Location = new Point(0, 644);
            panBottom.Name = "panBottom";
            panBottom.Padding = new Padding(4);
            panBottom.Size = new Size(1089, 36);
            panBottom.TabIndex = 2;
            // 
            // btClose
            // 
            btClose.BackColor = Color.DarkRed;
            btClose.Dock = DockStyle.Fill;
            btClose.FlatStyle = FlatStyle.Popup;
            btClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btClose.ForeColor = Color.Black;
            btClose.Location = new Point(4, 4);
            btClose.Name = "btClose";
            btClose.Padding = new Padding(4);
            btClose.Size = new Size(1081, 28);
            btClose.TabIndex = 0;
            btClose.Tag = "no-theme";
            btClose.Text = "Close";
            btClose.UseVisualStyleBackColor = false;
            btClose.Click += btClose_Click;
            // 
            // DeepSearchForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(37, 38, 42);
            ClientSize = new Size(1089, 680);
            Controls.Add(splitMain);
            Controls.Add(panBottom);
            Controls.Add(panTop);
            ForeColor = Color.WhiteSmoke;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            KeyPreview = true;
            MinimumSize = new Size(800, 500);
            Name = "DeepSearchForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Deep Search";
            KeyDown += DeepSearchForm_KeyDown;
            panTop.ResumeLayout(false);
            panTop.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            tabResults.ResumeLayout(false);
            tabFinal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webFinalReport).EndInit();
            tabEvolving.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webEvolvingReport).EndInit();
            tabSources.ResumeLayout(false);
            tabPlan.ResumeLayout(false);
            panBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panTop;
        private Label lblQuery;
        private TextBox edQuery;
        private Button btSearch;
        private Button btCancel;
        private SplitContainer splitMain;
        private ListView listProgress;
        private ColumnHeader colPhase;
        private ColumnHeader colRound;
        private ColumnHeader colMessage;
        private Controls.ModernTabControl tabResults;
        private TabPage tabFinal;
        private Microsoft.Web.WebView2.WinForms.WebView2 webFinalReport;
        private TabPage tabEvolving;
        private Microsoft.Web.WebView2.WinForms.WebView2 webEvolvingReport;
        private TabPage tabSources;
        private ListView listSources;
        private ColumnHeader colSourceTitle;
        private ColumnHeader colSourceUrl;
        private ColumnHeader colSourceSummary;
        private TabPage tabPlan;
        private RichTextBox txtPlan;
        private Panel panBottom;
        private Button btClose;
        private Label lblStatus;
    }
}
