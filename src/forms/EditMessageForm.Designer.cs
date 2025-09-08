using WaifuAI.Files;

namespace WaifuAI.src.forms
{
    partial class EditMessageForm
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

        public new void Dispose()
        {
            Dispose(true);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ed_message = new WaifuAI.Controls.SpellCheckedTextBox();
            bt_save = new Button();
            SuspendLayout();
            // 
            // ed_message
            // 
            ed_message.AllowDrop = true;
            ed_message.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ed_message.BackColor = Color.FromArgb(32, 70, 130, 180);
            ed_message.Font = new Font("Segoe UI", 16F);
            ed_message.Location = new Point(12, 12);
            ed_message.Multiline = true;
            ed_message.Name = "ed_message";
            ed_message.ScrollBars = ScrollBars.Vertical;
            ed_message.Size = new Size(498, 263);
            ed_message.SpellCheckLanguage = "en-US";
            ed_message.TabIndex = 0;
            // 
            // bt_save
            // 
            bt_save.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bt_save.BackColor = Color.PaleGreen;
            bt_save.FlatStyle = FlatStyle.Flat;
            bt_save.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_save.Location = new Point(12, 281);
            bt_save.Name = "bt_save";
            bt_save.Size = new Size(498, 23);
            bt_save.TabIndex = 1;
            bt_save.Text = "Save Message";
            bt_save.UseVisualStyleBackColor = false;
            bt_save.Click += bt_save_Click;
            // 
            // EditMessageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(522, 314);
            Controls.Add(bt_save);
            Controls.Add(ed_message);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditMessageForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Message";
            TopMost = true;
            KeyPress += EditMessageForm_KeyPress;
            ResumeLayout(false);
        }

        #endregion

        private WaifuAI.Controls.SpellCheckedTextBox ed_message;
        private Button bt_save;
    }
}