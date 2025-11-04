namespace WaifuAI.src.forms
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
        private System.Windows.Forms.Panel panelBottom;
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
            listMemories = new ListView();
            colTitle = new ColumnHeader();
            colCategory = new ColumnHeader();
            colAdded = new ColumnHeader();
            btDeleteSelected = new Button();
            btEditSelected = new Button();
            btAddNew = new Button();
            panelLeftTop = new Panel();
            cbCategory = new Controls.ModernComboBox();
            lblCategory = new Label();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            panelBottom = new Panel();
            btClose = new Button();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            panelLeftTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            panelBottom.SuspendLayout();
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
            splitMain.Panel1.Controls.Add(listMemories);
            splitMain.Panel1.Controls.Add(btDeleteSelected);
            splitMain.Panel1.Controls.Add(btEditSelected);
            splitMain.Panel1.Controls.Add(btAddNew);
            splitMain.Panel1.Controls.Add(panelLeftTop);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(webView);
            splitMain.Size = new Size(1072, 514);
            splitMain.SplitterDistance = 400;
            splitMain.TabIndex = 0;
            // 
            // listMemories
            // 
            listMemories.Columns.AddRange(new ColumnHeader[] { colTitle, colCategory, colAdded });
            listMemories.Dock = DockStyle.Fill;
            listMemories.FullRowSelect = true;
            listMemories.Location = new Point(0, 38);
            listMemories.MultiSelect = false;
            listMemories.Name = "listMemories";
            listMemories.Size = new Size(400, 422);
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
            btDeleteSelected.BackColor = Color.Maroon;
            btDeleteSelected.Dock = DockStyle.Bottom;
            btDeleteSelected.FlatStyle = FlatStyle.Popup;
            btDeleteSelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btDeleteSelected.ForeColor = Color.WhiteSmoke;
            btDeleteSelected.Location = new Point(0, 491);
            btDeleteSelected.Name = "btDeleteSelected";
            btDeleteSelected.Size = new Size(400, 23);
            btDeleteSelected.TabIndex = 2;
            btDeleteSelected.Tag = "no-theme";
            btDeleteSelected.Text = "Delete Entry";
            btDeleteSelected.UseVisualStyleBackColor = false;
            btDeleteSelected.Click += btDeleteSelected_Click;
            // 
            // btEditSelected
            // 
            btEditSelected.BackColor = Color.SteelBlue;
            btEditSelected.Dock = DockStyle.Bottom;
            btEditSelected.FlatStyle = FlatStyle.Popup;
            btEditSelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btEditSelected.ForeColor = Color.WhiteSmoke;
            btEditSelected.Location = new Point(0, 468);
            btEditSelected.Name = "btEditSelected";
            btEditSelected.Size = new Size(400, 23);
            btEditSelected.TabIndex = 3;
            btEditSelected.Tag = "no-theme";
            btEditSelected.Text = "Edit Entry";
            btEditSelected.UseVisualStyleBackColor = false;
            btEditSelected.Click += btEditSelected_Click;
            // 
            // btAddNew
            // 
            btAddNew.BackColor = Color.DarkSeaGreen;
            btAddNew.Dock = DockStyle.Bottom;
            btAddNew.FlatStyle = FlatStyle.Popup;
            btAddNew.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btAddNew.ForeColor = Color.WhiteSmoke;
            btAddNew.Location = new Point(0, 460);
            btAddNew.Name = "btAddNew";
            btAddNew.Size = new Size(400, 23);
            btAddNew.TabIndex = 4;
            btAddNew.Tag = "no-theme";
            btAddNew.Text = "Add New Entry";
            btAddNew.UseVisualStyleBackColor = false;
            btAddNew.Click += btAddNew_Click;
            // 
            // panelLeftTop
            // 
            panelLeftTop.Controls.Add(cbCategory);
            panelLeftTop.Controls.Add(lblCategory);
            panelLeftTop.Dock = DockStyle.Top;
            panelLeftTop.Location = new Point(0, 0);
            panelLeftTop.Name = "panelLeftTop";
            panelLeftTop.Padding = new Padding(8);
            panelLeftTop.Size = new Size(400, 38);
            panelLeftTop.TabIndex = 1;
            // 
            // cbCategory
            // 
            cbCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCategory.Location = new Point(80, 8);
            cbCategory.Name = "cbCategory";
            cbCategory.Size = new Size(309, 23);
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
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(0, 0);
            webView.Name = "webView";
            webView.Size = new Size(668, 514);
            webView.TabIndex = 0;
            webView.ZoomFactor = 1D;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(btClose);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 514);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(8);
            panelBottom.Size = new Size(1072, 44);
            panelBottom.TabIndex = 1;
            // 
            // btClose
            // 
            btClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btClose.BackColor = Color.DarkSeaGreen;
            btClose.FlatStyle = FlatStyle.Flat;
            btClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btClose.Location = new Point(8, 8);
            btClose.Name = "btClose";
            btClose.Size = new Size(1056, 28);
            btClose.TabIndex = 0;
            btClose.Tag = "no-theme";
            btClose.Text = "Close";
            btClose.UseVisualStyleBackColor = false;
            btClose.Click += btClose_Click;
            // 
            // MemoryBrowserForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1072, 558);
            Controls.Add(splitMain);
            Controls.Add(panelBottom);
            MinimizeBox = false;
            Name = "MemoryBrowserForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Memory Browser";
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            panelLeftTop.ResumeLayout(false);
            panelLeftTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}