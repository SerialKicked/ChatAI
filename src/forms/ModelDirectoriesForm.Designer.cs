namespace LetheChat.src.forms
{
    partial class ModelDirectoriesForm
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
            lblTitle = new System.Windows.Forms.Label();
            lstDirectories = new System.Windows.Forms.ListBox();
            panButtons = new System.Windows.Forms.Panel();
            btAdd = new System.Windows.Forms.Button();
            btRemove = new System.Windows.Forms.Button();
            btOK = new System.Windows.Forms.Button();
            btCancel = new System.Windows.Forms.Button();
            panButtons.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblTitle.Location = new System.Drawing.Point(8, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(180, 15);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Model search directories:";
            // 
            // lstDirectories
            // 
            lstDirectories.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lstDirectories.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lstDirectories.Font = new System.Drawing.Font("Segoe UI", 9F);
            lstDirectories.FormattingEnabled = true;
            lstDirectories.IntegralHeight = false;
            lstDirectories.ItemHeight = 20;
            lstDirectories.Location = new System.Drawing.Point(8, 30);
            lstDirectories.Name = "lstDirectories";
            lstDirectories.Size = new System.Drawing.Size(544, 270);
            lstDirectories.TabIndex = 1;
            // 
            // panButtons
            // 
            panButtons.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panButtons.Controls.Add(btAdd);
            panButtons.Controls.Add(btRemove);
            panButtons.Controls.Add(btOK);
            panButtons.Controls.Add(btCancel);
            panButtons.Location = new System.Drawing.Point(8, 312);
            panButtons.Name = "panButtons";
            panButtons.Size = new System.Drawing.Size(544, 34);
            panButtons.TabIndex = 2;
            // 
            // btAdd
            // 
            btAdd.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btAdd.ForeColor = System.Drawing.Color.White;
            btAdd.Location = new System.Drawing.Point(0, 0);
            btAdd.Name = "btAdd";
            btAdd.Size = new System.Drawing.Size(130, 30);
            btAdd.TabIndex = 0;
            btAdd.Tag = "no-theme";
            btAdd.Text = "Add Folder…";
            btAdd.UseVisualStyleBackColor = false;
            btAdd.Click += btAdd_Click;
            // 
            // btRemove
            // 
            btRemove.BackColor = System.Drawing.Color.FromArgb(183, 28, 28);
            btRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btRemove.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btRemove.ForeColor = System.Drawing.Color.White;
            btRemove.Location = new System.Drawing.Point(138, 0);
            btRemove.Name = "btRemove";
            btRemove.Size = new System.Drawing.Size(130, 30);
            btRemove.TabIndex = 1;
            btRemove.Tag = "no-theme";
            btRemove.Text = "Remove Selected";
            btRemove.UseVisualStyleBackColor = false;
            btRemove.Click += btRemove_Click;
            // 
            // btOK
            // 
            btOK.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btOK.BackColor = System.Drawing.Color.DarkKhaki;
            btOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btOK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btOK.ForeColor = System.Drawing.Color.Black;
            btOK.Location = new System.Drawing.Point(345, 0);
            btOK.Name = "btOK";
            btOK.Size = new System.Drawing.Size(90, 30);
            btOK.TabIndex = 2;
            btOK.Tag = "no-theme";
            btOK.Text = "OK";
            btOK.UseVisualStyleBackColor = false;
            btOK.Click += btOK_Click;
            // 
            // btCancel
            // 
            btCancel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btCancel.Location = new System.Drawing.Point(443, 0);
            btCancel.Name = "btCancel";
            btCancel.Size = new System.Drawing.Size(90, 30);
            btCancel.TabIndex = 3;
            btCancel.Text = "Cancel";
            btCancel.UseVisualStyleBackColor = true;
            btCancel.Click += btCancel_Click;
            // 
            // ModelDirectoriesForm
            // 
            AcceptButton = btOK;
            CancelButton = btCancel;
            ClientSize = new System.Drawing.Size(560, 358);
            Controls.Add(lblTitle);
            Controls.Add(lstDirectories);
            Controls.Add(panButtons);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ModelDirectoriesForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Manage Model Folders";
            Load += ModelDirectoriesForm_Load;
            panButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListBox lstDirectories;
        private System.Windows.Forms.Panel panButtons;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btRemove;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
    }
}
