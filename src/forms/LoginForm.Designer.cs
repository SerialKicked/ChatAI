using WaifuAI.Controls;

namespace WaifuAI.src.forms
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            edKey = new TextBox();
            label3 = new Label();
            cbAPI = new ModernComboBox();
            edUrl = new TextBox();
            label2 = new Label();
            label1 = new Label();
            btCheck = new Button();
            btCancel = new Button();
            btConnect = new Button();
            collapsibleGroupBox1 = new WaifuAI.Controls.CollapsibleGroupBox();
            collapsibleGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // edKey
            // 
            edKey.BorderStyle = BorderStyle.FixedSingle;
            edKey.Font = new Font("Segoe UI", 9F);
            edKey.Location = new Point(179, 103);
            edKey.Name = "edKey";
            edKey.PlaceholderText = "When in doubt, leave empty";
            edKey.Size = new Size(245, 23);
            edKey.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.Location = new Point(179, 85);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 4;
            label3.Text = "API Key";
            // 
            // cbAPI
            // 
            cbAPI.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAPI.Font = new Font("Segoe UI", 9F);
            cbAPI.Items.AddRange(new object[] { "KoboldAPI", "OpenAI Compatible" });
            cbAPI.Location = new Point(15, 103);
            cbAPI.Name = "cbAPI";
            cbAPI.Size = new Size(158, 23);
            cbAPI.TabIndex = 2;
            // 
            // edUrl
            // 
            edUrl.BorderStyle = BorderStyle.FixedSingle;
            edUrl.Font = new Font("Segoe UI", 9F);
            edUrl.Location = new Point(15, 59);
            edUrl.Name = "edUrl";
            edUrl.PlaceholderText = "This is likely something like http://localhost:port";
            edUrl.Size = new Size(409, 23);
            edUrl.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F);
            label2.Location = new Point(15, 85);
            label2.Name = "label2";
            label2.Size = new Size(25, 15);
            label2.TabIndex = 1;
            label2.Text = "API";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F);
            label1.Location = new Point(15, 41);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 0;
            label1.Text = "Backend's Address";
            // 
            // btCheck
            // 
            btCheck.FlatStyle = FlatStyle.Flat;
            btCheck.Location = new Point(15, 136);
            btCheck.Name = "btCheck";
            btCheck.Size = new Size(111, 23);
            btCheck.TabIndex = 6;
            btCheck.Tag = "";
            btCheck.Text = "Check";
            btCheck.UseVisualStyleBackColor = true;
            btCheck.Click += btCheck_Click;
            // 
            // btCancel
            // 
            btCancel.BackColor = Color.LightSlateGray;
            btCancel.DialogResult = DialogResult.Cancel;
            btCancel.FlatStyle = FlatStyle.Flat;
            btCancel.ForeColor = Color.Black;
            btCancel.Location = new Point(313, 136);
            btCancel.Name = "btCancel";
            btCancel.Size = new Size(111, 23);
            btCancel.TabIndex = 5;
            btCancel.Tag = "no-theme";
            btCancel.Text = "Don't Connect";
            btCancel.UseVisualStyleBackColor = false;
            btCancel.Click += btCancel_Click;
            // 
            // btConnect
            // 
            btConnect.BackColor = Color.DarkSeaGreen;
            btConnect.DialogResult = DialogResult.OK;
            btConnect.FlatStyle = FlatStyle.Flat;
            btConnect.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btConnect.ForeColor = Color.Black;
            btConnect.Location = new Point(196, 136);
            btConnect.Name = "btConnect";
            btConnect.Size = new Size(111, 23);
            btConnect.TabIndex = 4;
            btConnect.Tag = "no-theme";
            btConnect.Text = "Connect";
            btConnect.UseVisualStyleBackColor = false;
            btConnect.Click += btConnect_Click;
            // 
            // collapsibleGroupBox1
            // 
            collapsibleGroupBox1.BackColor = Color.FromArgb(37, 38, 42);
            collapsibleGroupBox1.CanCollapse = false;
            collapsibleGroupBox1.Controls.Add(edKey);
            collapsibleGroupBox1.Controls.Add(btCheck);
            collapsibleGroupBox1.Controls.Add(btCancel);
            collapsibleGroupBox1.Controls.Add(btConnect);
            collapsibleGroupBox1.Controls.Add(label1);
            collapsibleGroupBox1.Controls.Add(label3);
            collapsibleGroupBox1.Controls.Add(label2);
            collapsibleGroupBox1.Controls.Add(cbAPI);
            collapsibleGroupBox1.Controls.Add(edUrl);
            collapsibleGroupBox1.Dock = DockStyle.Fill;
            collapsibleGroupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            collapsibleGroupBox1.Location = new Point(0, 0);
            collapsibleGroupBox1.Name = "collapsibleGroupBox1";
            collapsibleGroupBox1.Padding = new Padding(12, 32, 12, 10);
            collapsibleGroupBox1.Size = new Size(438, 175);
            collapsibleGroupBox1.TabIndex = 7;
            collapsibleGroupBox1.Text = "Connection";
            collapsibleGroupBox1.ExpandedChanged += collapsibleGroupBox1_ExpandedChanged;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(438, 175);
            Controls.Add(collapsibleGroupBox1);
            ForeColor = Color.WhiteSmoke;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "w(AI)fu.NET: Connect...";
            Shown += LoginForm_Shown;
            collapsibleGroupBox1.ResumeLayout(false);
            collapsibleGroupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ModernComboBox cbAPI;
        private TextBox edUrl;
        private Label label2;
        private Label label1;
        private TextBox edKey;
        private Label label3;
        private Button btCheck;
        private Button btCancel;
        private Button btConnect;
        private Controls.CollapsibleGroupBox collapsibleGroupBox1;
    }
}