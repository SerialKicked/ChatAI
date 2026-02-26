namespace WaifuAI.src.forms
{
    partial class FactBrowserForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.ListView listFacts;
        private System.Windows.Forms.ColumnHeader colFact;
        private System.Windows.Forms.ColumnHeader colFirstSeen;
        private System.Windows.Forms.ColumnHeader colLastSeen;
        private System.Windows.Forms.ColumnHeader colRefs;
        private System.Windows.Forms.ColumnHeader colSuperseded;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btDeleteSelected;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            splitMain = new SplitContainer();
            btClose = new Button();
            listFacts = new ListView();
            colFact = new ColumnHeader();
            colFirstSeen = new ColumnHeader();
            colLastSeen = new ColumnHeader();
            colRefs = new ColumnHeader();
            colSuperseded = new ColumnHeader();
            btDeleteSelected = new Button();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            SuspendLayout();
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 0);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(btClose);
            splitMain.Panel1.Controls.Add(listFacts);
            splitMain.Panel1.Controls.Add(btDeleteSelected);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(webView);
            splitMain.Size = new Size(1100, 580);
            splitMain.SplitterDistance = 460;
            splitMain.TabIndex = 0;
            // 
            // btClose
            // 
            btClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btClose.BackColor = Color.LightSlateGray;
            btClose.FlatStyle = FlatStyle.Flat;
            btClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btClose.Location = new Point(8, 539);
            btClose.Name = "btClose";
            btClose.Size = new Size(441, 29);
            btClose.TabIndex = 0;
            btClose.Tag = "no-theme";
            btClose.Text = "Close";
            btClose.UseVisualStyleBackColor = false;
            btClose.Click += btClose_Click;
            // 
            // listFacts
            // 
            listFacts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listFacts.Columns.AddRange(new ColumnHeader[] { colFact, colFirstSeen, colLastSeen, colRefs, colSuperseded });
            listFacts.FullRowSelect = true;
            listFacts.Location = new Point(0, 4);
            listFacts.MultiSelect = false;
            listFacts.Name = "listFacts";
            listFacts.Size = new Size(460, 500);
            listFacts.TabIndex = 0;
            listFacts.UseCompatibleStateImageBehavior = false;
            listFacts.View = View.Details;
            listFacts.ColumnClick += listFacts_ColumnClick;
            listFacts.SelectedIndexChanged += listFacts_SelectedIndexChanged;
            // 
            // colFact
            // 
            colFact.Text = "Fact";
            colFact.Width = 200;
            // 
            // colFirstSeen
            // 
            colFirstSeen.Text = "First Seen";
            colFirstSeen.Width = 90;
            // 
            // colLastSeen
            // 
            colLastSeen.Text = "Last Seen";
            colLastSeen.Width = 90;
            // 
            // colRefs
            // 
            colRefs.Text = "Refs";
            colRefs.Width = 40;
            // 
            // colSuperseded
            // 
            colSuperseded.Text = "Status";
            colSuperseded.Width = 40;
            // 
            // btDeleteSelected
            // 
            btDeleteSelected.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btDeleteSelected.BackColor = Color.DarkRed;
            btDeleteSelected.FlatStyle = FlatStyle.Popup;
            btDeleteSelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btDeleteSelected.ForeColor = Color.Black;
            btDeleteSelected.Location = new Point(8, 510);
            btDeleteSelected.Name = "btDeleteSelected";
            btDeleteSelected.Size = new Size(441, 23);
            btDeleteSelected.TabIndex = 2;
            btDeleteSelected.Tag = "no-theme";
            btDeleteSelected.Text = "Delete Selected Fact";
            btDeleteSelected.UseVisualStyleBackColor = false;
            btDeleteSelected.Click += btDeleteSelected_Click;
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(0, 0);
            webView.Name = "webView";
            webView.Size = new Size(636, 580);
            webView.TabIndex = 0;
            webView.ZoomFactor = 1D;
            // 
            // FactBrowserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 580);
            Controls.Add(splitMain);
            MinimizeBox = false;
            Name = "FactBrowserForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Extracted Facts Browser";
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);
        }
    }
}
