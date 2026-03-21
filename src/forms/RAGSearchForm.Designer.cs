namespace LetheChat.src.forms
{
    partial class RAGSearchForm
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
            txtSearchRes = new TextBox();
            groupBox1 = new GroupBox();
            textBox1 = new TextBox();
            btClose = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // txtSearchRes
            // 
            txtSearchRes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtSearchRes.BorderStyle = BorderStyle.FixedSingle;
            txtSearchRes.Location = new Point(12, 75);
            txtSearchRes.Multiline = true;
            txtSearchRes.Name = "txtSearchRes";
            txtSearchRes.ScrollBars = ScrollBars.Vertical;
            txtSearchRes.Size = new Size(840, 483);
            txtSearchRes.TabIndex = 3;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(840, 57);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Vector Search";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(6, 22);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "This will perform a proximity search in the currently loaded character's vector database";
            textBox1.Size = new Size(828, 23);
            textBox1.TabIndex = 3;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // btClose
            // 
            btClose.BackColor = Color.PaleGreen;
            btClose.FlatStyle = FlatStyle.Flat;
            btClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btClose.Location = new Point(12, 567);
            btClose.Name = "btClose";
            btClose.Size = new Size(840, 23);
            btClose.TabIndex = 5;
            btClose.Text = "Close";
            btClose.UseVisualStyleBackColor = false;
            btClose.Click += button1_Click;
            // 
            // RAGSearchForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(864, 602);
            Controls.Add(btClose);
            Controls.Add(groupBox1);
            Controls.Add(txtSearchRes);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RAGSearchForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Vector Search";
            KeyDown += RAGSearchForm_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSearchRes;
        private GroupBox groupBox1;
        private TextBox textBox1;
        private Button btClose;
    }
}