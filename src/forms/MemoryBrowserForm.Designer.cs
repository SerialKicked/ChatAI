namespace LetheChat.src.forms
{
    partial class MemoryBrowserForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel panelLeftTop;
        private Controls.ModernComboBox cbCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ListView listMemories;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colCategory;
        private System.Windows.Forms.ColumnHeader colAdded;
        private System.Windows.Forms.Button btClose;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private Button btDeleteSelected;
        private Button btEditSelected;
        private Button btAddNew;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            splitMain = new SplitContainer();
            btClose = new Button();
            mainTabControl = new LetheChat.Controls.ModernTabControl();
            tabBrowse = new TabPage();
            panelLeftTop = new Panel();
            cbCategory = new LetheChat.Controls.ModernComboBox();
            lblCategory = new Label();
            listMemories = new ListView();
            colTitle = new ColumnHeader();
            colCategory = new ColumnHeader();
            colAdded = new ColumnHeader();
            btDeleteSelected = new Button();
            btAddNew = new Button();
            btEditSelected = new Button();
            tabFacts = new TabPage();
            button1 = new Button();
            listFacts = new ListView();
            colFact = new ColumnHeader();
            colFirstSeen = new ColumnHeader();
            colLastSeen = new ColumnHeader();
            colRefs = new ColumnHeader();
            colSuperseded = new ColumnHeader();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            mainTabControl.SuspendLayout();
            tabBrowse.SuspendLayout();
            panelLeftTop.SuspendLayout();
            tabFacts.SuspendLayout();
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
            splitMain.Panel1.Controls.Add(mainTabControl);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(webView);
            splitMain.Size = new Size(1301, 767);
            splitMain.SplitterDistance = 485;
            splitMain.TabIndex = 0;
            // 
            // btClose
            // 
            btClose.BackColor = Color.LightSlateGray;
            btClose.Dock = DockStyle.Bottom;
            btClose.FlatStyle = FlatStyle.Flat;
            btClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btClose.Location = new Point(0, 738);
            btClose.Name = "btClose";
            btClose.Size = new Size(485, 29);
            btClose.TabIndex = 0;
            btClose.Tag = "no-theme";
            btClose.Text = "Close";
            btClose.UseVisualStyleBackColor = false;
            btClose.Click += btClose_Click;
            // 
            // mainTabControl
            // 
            mainTabControl.Appearance = TabAppearance.Buttons;
            mainTabControl.Controls.Add(tabBrowse);
            mainTabControl.Controls.Add(tabFacts);
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Font = new Font("Segoe UI", 9F);
            mainTabControl.ItemSize = new Size(0, 36);
            mainTabControl.Location = new Point(0, 0);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(485, 767);
            mainTabControl.TabIndex = 5;
            // 
            // tabBrowse
            // 
            tabBrowse.BackColor = Color.FromArgb(37, 37, 37);
            tabBrowse.Controls.Add(panelLeftTop);
            tabBrowse.Controls.Add(listMemories);
            tabBrowse.Controls.Add(btDeleteSelected);
            tabBrowse.Controls.Add(btAddNew);
            tabBrowse.Controls.Add(btEditSelected);
            tabBrowse.Font = new Font("Segoe UI", 9F);
            tabBrowse.ForeColor = Color.FromArgb(230, 230, 230);
            tabBrowse.Location = new Point(4, 40);
            tabBrowse.Name = "tabBrowse";
            tabBrowse.Padding = new Padding(3);
            tabBrowse.Size = new Size(477, 723);
            tabBrowse.TabIndex = 0;
            tabBrowse.Text = "Memory Browser";
            // 
            // panelLeftTop
            // 
            panelLeftTop.Controls.Add(cbCategory);
            panelLeftTop.Controls.Add(lblCategory);
            panelLeftTop.Dock = DockStyle.Top;
            panelLeftTop.Location = new Point(3, 3);
            panelLeftTop.Name = "panelLeftTop";
            panelLeftTop.Padding = new Padding(8);
            panelLeftTop.Size = new Size(471, 38);
            panelLeftTop.TabIndex = 1;
            // 
            // cbCategory
            // 
            cbCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbCategory.BackColor = Color.FromArgb(64, 64, 64);
            cbCategory.DropDownHeight = 180;
            cbCategory.Font = new Font("Segoe UI", 9F);
            cbCategory.Location = new Point(80, 8);
            cbCategory.MaxDropDownItems = 10;
            cbCategory.Name = "cbCategory";
            cbCategory.Padding = new Padding(1);
            cbCategory.Size = new Size(380, 23);
            cbCategory.TabIndex = 0;
            cbCategory.SelectedIndexChanged += cbCategory_SelectedIndexChanged;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(8, 12);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(58, 15);
            lblCategory.TabIndex = 1;
            lblCategory.Text = "Category:";
            // 
            // listMemories
            // 
            listMemories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listMemories.Columns.AddRange(new ColumnHeader[] { colTitle, colCategory, colAdded });
            listMemories.FullRowSelect = true;
            listMemories.Location = new Point(3, 47);
            listMemories.MultiSelect = false;
            listMemories.Name = "listMemories";
            listMemories.Size = new Size(471, 612);
            listMemories.TabIndex = 0;
            listMemories.UseCompatibleStateImageBehavior = false;
            listMemories.View = View.Details;
            listMemories.ColumnClick += listMemories_ColumnClick;
            listMemories.SelectedIndexChanged += listMemories_SelectedIndexChanged;
            listMemories.DoubleClick += listMemories_DoubleClick;
            // 
            // colTitle
            // 
            colTitle.Text = "Title";
            colTitle.Width = 180;
            // 
            // colCategory
            // 
            colCategory.Text = "Category";
            colCategory.Width = 100;
            // 
            // colAdded
            // 
            colAdded.Text = "Added";
            colAdded.Width = 120;
            // 
            // btDeleteSelected
            // 
            btDeleteSelected.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btDeleteSelected.BackColor = Color.DarkRed;
            btDeleteSelected.FlatStyle = FlatStyle.Popup;
            btDeleteSelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btDeleteSelected.ForeColor = Color.Black;
            btDeleteSelected.Location = new Point(370, 665);
            btDeleteSelected.Name = "btDeleteSelected";
            btDeleteSelected.Size = new Size(104, 27);
            btDeleteSelected.TabIndex = 2;
            btDeleteSelected.Tag = "no-theme";
            btDeleteSelected.Text = "Delete Entry";
            btDeleteSelected.UseVisualStyleBackColor = false;
            btDeleteSelected.Click += btDeleteSelected_Click;
            // 
            // btAddNew
            // 
            btAddNew.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btAddNew.BackColor = Color.DarkSeaGreen;
            btAddNew.FlatStyle = FlatStyle.Popup;
            btAddNew.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btAddNew.ForeColor = Color.Black;
            btAddNew.Location = new Point(3, 665);
            btAddNew.Name = "btAddNew";
            btAddNew.Size = new Size(245, 27);
            btAddNew.TabIndex = 4;
            btAddNew.Tag = "no-theme";
            btAddNew.Text = "Add New Entry";
            btAddNew.UseVisualStyleBackColor = false;
            btAddNew.Click += btAddNew_Click;
            // 
            // btEditSelected
            // 
            btEditSelected.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btEditSelected.BackColor = Color.DarkKhaki;
            btEditSelected.FlatStyle = FlatStyle.Popup;
            btEditSelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btEditSelected.ForeColor = Color.Black;
            btEditSelected.Location = new Point(254, 665);
            btEditSelected.Name = "btEditSelected";
            btEditSelected.Size = new Size(110, 27);
            btEditSelected.TabIndex = 3;
            btEditSelected.Tag = "no-theme";
            btEditSelected.Text = "Edit Entry";
            btEditSelected.UseVisualStyleBackColor = false;
            btEditSelected.Click += btEditSelected_Click;
            // 
            // tabFacts
            // 
            tabFacts.BackColor = Color.FromArgb(37, 37, 37);
            tabFacts.Controls.Add(button1);
            tabFacts.Controls.Add(listFacts);
            tabFacts.Font = new Font("Segoe UI", 9F);
            tabFacts.ForeColor = Color.FromArgb(230, 230, 230);
            tabFacts.Location = new Point(4, 40);
            tabFacts.Name = "tabFacts";
            tabFacts.Padding = new Padding(3);
            tabFacts.Size = new Size(477, 723);
            tabFacts.TabIndex = 1;
            tabFacts.Text = "Known Facts";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button1.BackColor = Color.DarkRed;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(3, 665);
            button1.Name = "button1";
            button1.Size = new Size(471, 27);
            button1.TabIndex = 3;
            button1.Tag = "no-theme";
            button1.Text = "Delete Selected Fact";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // listFacts
            // 
            listFacts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listFacts.Columns.AddRange(new ColumnHeader[] { colFact, colFirstSeen, colLastSeen, colRefs, colSuperseded });
            listFacts.FullRowSelect = true;
            listFacts.Location = new Point(3, 6);
            listFacts.MultiSelect = false;
            listFacts.Name = "listFacts";
            listFacts.Size = new Size(471, 653);
            listFacts.TabIndex = 1;
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
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(0, 0);
            webView.Name = "webView";
            webView.Size = new Size(812, 767);
            webView.TabIndex = 0;
            webView.ZoomFactor = 1D;
            // 
            // MemoryBrowserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1301, 767);
            Controls.Add(splitMain);
            MinimizeBox = false;
            Name = "MemoryBrowserForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Memory Browser";
            KeyDown += MemoryBrowserForm_KeyDown_1;
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            mainTabControl.ResumeLayout(false);
            tabBrowse.ResumeLayout(false);
            panelLeftTop.ResumeLayout(false);
            panelLeftTop.PerformLayout();
            tabFacts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);
        }

        private Controls.ModernTabControl mainTabControl;
        private TabPage tabBrowse;
        private TabPage tabFacts;
        private ListView listFacts;
        private ColumnHeader colFact;
        private ColumnHeader colFirstSeen;
        private ColumnHeader colLastSeen;
        private ColumnHeader colRefs;
        private ColumnHeader colSuperseded;
        private Button button1;
    }
}