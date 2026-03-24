namespace LetheChat.Forms
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
            lbl_info = new Label();
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
            // lbl_info
            // 
            lbl_info.Dock = DockStyle.Fill;
            lbl_info.FlatStyle = FlatStyle.Flat;
            lbl_info.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbl_info.Location = new Point(0, 0);
            lbl_info.Margin = new Padding(8);
            lbl_info.Name = "lbl_info";
            lbl_info.Padding = new Padding(8);
            lbl_info.Size = new Size(604, 121);
            lbl_info.TabIndex = 1;
            lbl_info.Text = "An operation is in progress, please wait.";
            // 
            // LoadingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(604, 144);
            ControlBox = false;
            Controls.Add(lbl_info);
            Controls.Add(progress_bar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "LoadingForm";
            ShowIcon = false;
            Text = "Operation in Progress...";
            ResumeLayout(false);
        }

        #endregion

        private ProgressBar progress_bar;
        private Label lbl_info;
    }
}