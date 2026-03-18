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
            collapsibleGroupBox1 = new CollapsibleGroupBox();
            ck_flash = new ModernCheckBox();
            lbl_gpu = new Label();
            num_gpu = new ModernNumericUpDown();
            lbl_context = new Label();
            num_maxcontext = new ModernNumericUpDown();
            btBrowse = new Button();
            ck_lmstudio = new ModernCheckBox();
            collapsibleGroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // edKey
            // 
            edKey.BorderStyle = BorderStyle.FixedSingle;
            edKey.Font = new Font("Segoe UI", 9F);
            edKey.Location = new Point(15, 155);
            edKey.Name = "edKey";
            edKey.PlaceholderText = "When in doubt, leave empty";
            edKey.Size = new Size(496, 23);
            edKey.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.Location = new Point(15, 137);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 4;
            label3.Text = "API Key";
            // 
            // cbAPI
            // 
            cbAPI.BackColor = Color.FromArgb(64, 64, 64);
            cbAPI.DropDownHeight = 180;
            cbAPI.Font = new Font("Segoe UI", 9F);
            cbAPI.Items.AddRange(new object[] { "KoboldAPI", "OpenAI Compatible", "Lethe AI" });
            cbAPI.Location = new Point(15, 55);
            cbAPI.MaxDropDownItems = 10;
            cbAPI.Name = "cbAPI";
            cbAPI.Padding = new Padding(1);
            cbAPI.Size = new Size(182, 23);
            cbAPI.TabIndex = 2;
            cbAPI.SelectedIndexChanged += cbAPI_SelectedIndexChanged;
            // 
            // edUrl
            // 
            edUrl.BorderStyle = BorderStyle.FixedSingle;
            edUrl.Font = new Font("Segoe UI", 9F);
            edUrl.Location = new Point(15, 110);
            edUrl.Name = "edUrl";
            edUrl.PlaceholderText = "This is likely something like http://localhost:port";
            edUrl.Size = new Size(412, 23);
            edUrl.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(15, 37);
            label2.Name = "label2";
            label2.Size = new Size(26, 15);
            label2.TabIndex = 1;
            label2.Text = "API";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F);
            label1.Location = new Point(15, 92);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 0;
            label1.Text = "Backend's Address";
            // 
            // btCheck
            // 
            btCheck.FlatStyle = FlatStyle.Popup;
            btCheck.Location = new Point(15, 192);
            btCheck.Name = "btCheck";
            btCheck.Size = new Size(111, 23);
            btCheck.TabIndex = 6;
            btCheck.Tag = "no-theme";
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
            btCancel.Location = new Point(400, 192);
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
            btConnect.Location = new Point(283, 192);
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
            collapsibleGroupBox1.Controls.Add(ck_lmstudio);
            collapsibleGroupBox1.Controls.Add(ck_flash);
            collapsibleGroupBox1.Controls.Add(lbl_gpu);
            collapsibleGroupBox1.Controls.Add(num_gpu);
            collapsibleGroupBox1.Controls.Add(lbl_context);
            collapsibleGroupBox1.Controls.Add(num_maxcontext);
            collapsibleGroupBox1.Controls.Add(btBrowse);
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
            collapsibleGroupBox1.Size = new Size(523, 227);
            collapsibleGroupBox1.TabIndex = 7;
            collapsibleGroupBox1.Text = "Connection";
            // 
            // ck_flash
            // 
            ck_flash.Font = new Font("Segoe UI", 9F);
            ck_flash.Location = new Point(393, 55);
            ck_flash.Name = "ck_flash";
            ck_flash.Size = new Size(118, 23);
            ck_flash.TabIndex = 21;
            ck_flash.Text = "Flash Attention";
            ck_flash.UseVisualStyleBackColor = true;
            // 
            // lbl_gpu
            // 
            lbl_gpu.AutoSize = true;
            lbl_gpu.Font = new Font("Segoe UI", 9F);
            lbl_gpu.Location = new Point(298, 37);
            lbl_gpu.Name = "lbl_gpu";
            lbl_gpu.Size = new Size(66, 15);
            lbl_gpu.TabIndex = 19;
            lbl_gpu.Text = "GPU Layers";
            // 
            // num_gpu
            // 
            num_gpu.BackColor = Color.FromArgb(64, 64, 64);
            num_gpu.Font = new Font("Segoe UI", 9F);
            num_gpu.Location = new Point(298, 55);
            num_gpu.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            num_gpu.Name = "num_gpu";
            num_gpu.Padding = new Padding(1);
            num_gpu.Size = new Size(89, 23);
            num_gpu.TabIndex = 20;
            num_gpu.Value = new decimal(new int[] { 255, 0, 0, 0 });
            // 
            // lbl_context
            // 
            lbl_context.AutoSize = true;
            lbl_context.Font = new Font("Segoe UI", 9F);
            lbl_context.Location = new Point(203, 37);
            lbl_context.Name = "lbl_context";
            lbl_context.Size = new Size(73, 15);
            lbl_context.TabIndex = 17;
            lbl_context.Text = "Max Context";
            // 
            // num_maxcontext
            // 
            num_maxcontext.BackColor = Color.FromArgb(64, 64, 64);
            num_maxcontext.Font = new Font("Segoe UI", 9F);
            num_maxcontext.Increment = new decimal(new int[] { 512, 0, 0, 0 });
            num_maxcontext.Location = new Point(203, 55);
            num_maxcontext.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            num_maxcontext.Minimum = new decimal(new int[] { 1024, 0, 0, 0 });
            num_maxcontext.Name = "num_maxcontext";
            num_maxcontext.Padding = new Padding(1);
            num_maxcontext.Size = new Size(89, 23);
            num_maxcontext.TabIndex = 18;
            num_maxcontext.Value = new decimal(new int[] { 16384, 0, 0, 0 });
            // 
            // btBrowse
            // 
            btBrowse.FlatStyle = FlatStyle.Popup;
            btBrowse.Location = new Point(433, 110);
            btBrowse.Name = "btBrowse";
            btBrowse.Size = new Size(78, 23);
            btBrowse.TabIndex = 7;
            btBrowse.Tag = "no-theme";
            btBrowse.Text = "Browse";
            btBrowse.UseVisualStyleBackColor = true;
            btBrowse.Click += btBrowse_Click;
            // 
            // ck_lmstudio
            // 
            ck_lmstudio.Font = new Font("Segoe UI", 9F);
            ck_lmstudio.Location = new Point(393, 81);
            ck_lmstudio.Name = "ck_lmstudio";
            ck_lmstudio.Size = new Size(118, 23);
            ck_lmstudio.TabIndex = 22;
            ck_lmstudio.Text = "Is LM Studio";
            ck_lmstudio.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(523, 227);
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
        private Button btBrowse;
        private Label lbl_gpu;
        private ModernNumericUpDown num_gpu;
        private Label lbl_context;
        private ModernNumericUpDown num_maxcontext;
        private ModernCheckBox ck_flash;
        private ModernCheckBox ck_lmstudio;
    }
}