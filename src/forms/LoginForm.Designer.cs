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
            groupBox1 = new GroupBox();
            edKey = new TextBox();
            label3 = new Label();
            cbAPI = new ComboBox();
            edUrl = new TextBox();
            label2 = new Label();
            label1 = new Label();
            btCheck = new Button();
            btCancel = new Button();
            btConnect = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(edKey);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(cbAPI);
            groupBox1.Controls.Add(edUrl);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(434, 119);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Connection";
            // 
            // edKey
            // 
            edKey.Font = new Font("Segoe UI", 9F);
            edKey.Location = new Point(170, 81);
            edKey.Name = "edKey";
            edKey.PlaceholderText = "When in doubt, leave empty";
            edKey.Size = new Size(248, 23);
            edKey.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.Location = new Point(170, 63);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 4;
            label3.Text = "API Key";
            // 
            // cbAPI
            // 
            cbAPI.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAPI.Font = new Font("Segoe UI", 9F);
            cbAPI.FormattingEnabled = true;
            cbAPI.Items.AddRange(new object[] { "KoboldAPI", "OpenAI Compatible" });
            cbAPI.Location = new Point(6, 81);
            cbAPI.Name = "cbAPI";
            cbAPI.Size = new Size(158, 23);
            cbAPI.TabIndex = 2;
            // 
            // edUrl
            // 
            edUrl.Font = new Font("Segoe UI", 9F);
            edUrl.Location = new Point(6, 37);
            edUrl.Name = "edUrl";
            edUrl.PlaceholderText = "This is likely something like http://localhost:port";
            edUrl.Size = new Size(412, 23);
            edUrl.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F);
            label2.Location = new Point(6, 63);
            label2.Name = "label2";
            label2.Size = new Size(25, 15);
            label2.TabIndex = 1;
            label2.Text = "API";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F);
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 0;
            label1.Text = "Backend's Address";
            // 
            // btCheck
            // 
            btCheck.Location = new Point(12, 137);
            btCheck.Name = "btCheck";
            btCheck.Size = new Size(111, 23);
            btCheck.TabIndex = 6;
            btCheck.Text = "Check";
            btCheck.UseVisualStyleBackColor = true;
            btCheck.Click += btCheck_Click;
            // 
            // btCancel
            // 
            btCancel.DialogResult = DialogResult.Cancel;
            btCancel.Location = new Point(335, 137);
            btCancel.Name = "btCancel";
            btCancel.Size = new Size(111, 23);
            btCancel.TabIndex = 5;
            btCancel.Text = "Cancel";
            btCancel.UseVisualStyleBackColor = true;
            btCancel.Click += btCancel_Click;
            // 
            // btConnect
            // 
            btConnect.DialogResult = DialogResult.OK;
            btConnect.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btConnect.ForeColor = Color.Green;
            btConnect.Location = new Point(218, 137);
            btConnect.Name = "btConnect";
            btConnect.Size = new Size(111, 23);
            btConnect.TabIndex = 4;
            btConnect.Text = "Connect";
            btConnect.UseVisualStyleBackColor = true;
            btConnect.Click += btConnect_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(457, 173);
            Controls.Add(btConnect);
            Controls.Add(btCancel);
            Controls.Add(btCheck);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Connect...";
            Shown += LoginForm_Shown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private ComboBox cbAPI;
        private TextBox edUrl;
        private Label label2;
        private Label label1;
        private TextBox edKey;
        private Label label3;
        private Button btCheck;
        private Button btCancel;
        private Button btConnect;
    }
}