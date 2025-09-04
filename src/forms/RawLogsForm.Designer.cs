namespace WaifuAI.src.forms
{
    partial class RawLogForm
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
            btClose = new Button();
            SuspendLayout();
            // 
            // txtSearchRes
            // 
            txtSearchRes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtSearchRes.BorderStyle = BorderStyle.FixedSingle;
            txtSearchRes.Location = new Point(12, 12);
            txtSearchRes.Multiline = true;
            txtSearchRes.Name = "txtSearchRes";
            txtSearchRes.ScrollBars = ScrollBars.Vertical;
            txtSearchRes.Size = new Size(840, 546);
            txtSearchRes.TabIndex = 3;
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
            // RawLogForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(864, 602);
            Controls.Add(btClose);
            Controls.Add(txtSearchRes);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RawLogForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Raw Message Log";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSearchRes;
        private Button btClose;
    }
}