using LetheChat.Files;

namespace LetheChat.src.forms
{
    partial class ScenarioEditForm
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
            ed_message = new TextBox();
            bt_save = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // ed_message
            // 
            ed_message.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ed_message.BorderStyle = BorderStyle.FixedSingle;
            ed_message.Font = new Font("Segoe UI", 11F);
            ed_message.Location = new Point(12, 12);
            ed_message.Multiline = true;
            ed_message.Name = "ed_message";
            ed_message.ScrollBars = ScrollBars.Vertical;
            ed_message.Size = new Size(472, 241);
            ed_message.TabIndex = 0;
            ed_message.KeyPress += ScenarioEditForm_KeyPress;
            // 
            // bt_save
            // 
            bt_save.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bt_save.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            bt_save.Location = new Point(12, 259);
            bt_save.Name = "bt_save";
            bt_save.Size = new Size(231, 23);
            bt_save.TabIndex = 1;
            bt_save.Text = "Save Scenario";
            bt_save.UseVisualStyleBackColor = true;
            bt_save.Click += bt_save_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Font = new Font("Segoe UI", 9F);
            button1.Location = new Point(252, 259);
            button1.Name = "button1";
            button1.Size = new Size(232, 23);
            button1.TabIndex = 2;
            button1.Text = "Clear";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ScenarioEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(496, 292);
            Controls.Add(button1);
            Controls.Add(bt_save);
            Controls.Add(ed_message);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ScenarioEditForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Scenario Override";
            KeyPress += ScenarioEditForm_KeyPress;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ed_message;
        private Button bt_save;
        private Button button1;
    }
}