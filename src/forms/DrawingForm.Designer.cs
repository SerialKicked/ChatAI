namespace LetheChat.Forms
{
    partial class DrawingForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _canvas?.Dispose();
                _gfx?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panBottom = new Panel();
            btSave = new Button();
            lblInfo = new Label();
            picCanvas = new PictureBox();
            panScroll = new Panel();
            panBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picCanvas).BeginInit();
            panScroll.SuspendLayout();
            SuspendLayout();
            // 
            // panBottom
            // 
            panBottom.Controls.Add(btSave);
            panBottom.Controls.Add(lblInfo);
            panBottom.Dock = DockStyle.Bottom;
            panBottom.Location = new Point(0, 574);
            panBottom.Name = "panBottom";
            panBottom.Padding = new Padding(4);
            panBottom.Size = new Size(800, 36);
            panBottom.TabIndex = 0;
            // 
            // btSave
            // 
            btSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btSave.BackColor = Color.DarkSeaGreen;
            btSave.FlatStyle = FlatStyle.Flat;
            btSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btSave.ForeColor = Color.Black;
            btSave.Location = new Point(697, 5);
            btSave.Name = "btSave";
            btSave.Size = new Size(95, 26);
            btSave.TabIndex = 0;
            btSave.Tag = "no-theme";
            btSave.Text = "Save as PNG";
            btSave.UseVisualStyleBackColor = false;
            btSave.Click += btSave_Click;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Font = new Font("Segoe UI", 8.5F);
            lblInfo.ForeColor = Color.LightSkyBlue;
            lblInfo.Location = new Point(8, 10);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(60, 15);
            lblInfo.TabIndex = 1;
            lblInfo.Text = "No canvas.";
            // 
            // picCanvas
            // 
            picCanvas.BackColor = Color.White;
            picCanvas.Location = new Point(0, 0);
            picCanvas.Name = "picCanvas";
            picCanvas.Size = new Size(800, 600);
            picCanvas.SizeMode = PictureBoxSizeMode.AutoSize;
            picCanvas.TabIndex = 0;
            picCanvas.TabStop = false;
            // 
            // panScroll
            // 
            panScroll.AutoScroll = true;
            panScroll.Controls.Add(picCanvas);
            panScroll.Dock = DockStyle.Fill;
            panScroll.Location = new Point(0, 0);
            panScroll.Name = "panScroll";
            panScroll.Size = new Size(800, 574);
            panScroll.TabIndex = 1;
            // 
            // DrawingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(37, 38, 42);
            ClientSize = new Size(800, 610);
            Controls.Add(panScroll);
            Controls.Add(panBottom);
            ForeColor = Color.WhiteSmoke;
            FormBorderStyle = FormBorderStyle.Sizable;
            KeyPreview = true;
            MinimumSize = new Size(320, 240);
            Name = "DrawingForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Drawing Canvas";
            panBottom.ResumeLayout(false);
            panBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picCanvas).EndInit();
            panScroll.ResumeLayout(false);
            panScroll.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panBottom;
        private Button btSave;
        private Label lblInfo;
        private Panel panScroll;
        private PictureBox picCanvas;
    }
}
