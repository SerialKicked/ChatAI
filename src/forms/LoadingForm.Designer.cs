namespace WaifuAI.src.forms
{
    partial class LoadingForm
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
            progress_bar = new ProgressBar();
            panel1 = new Panel();
            lbl_info = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // progress_bar
            // 
            progress_bar.Dock = DockStyle.Bottom;
            progress_bar.Location = new Point(0, 121);
            progress_bar.Name = "progress_bar";
            progress_bar.Size = new Size(604, 23);
            progress_bar.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(lbl_info);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(580, 103);
            panel1.TabIndex = 1;
            // 
            // lbl_info
            // 
            lbl_info.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbl_info.Location = new Point(3, 9);
            lbl_info.Name = "lbl_info";
            lbl_info.Size = new Size(572, 82);
            lbl_info.TabIndex = 0;
            lbl_info.Text = "An operation is in progress, please wait.";
            // 
            // LoadingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(604, 144);
            ControlBox = false;
            Controls.Add(panel1);
            Controls.Add(progress_bar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "LoadingForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Operation in Progress...";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ProgressBar progress_bar;
        private Panel panel1;
        private Label lbl_info;
    }
}