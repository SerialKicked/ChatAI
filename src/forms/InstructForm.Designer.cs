namespace WaifuAI.src.forms
{
    partial class InstructForm
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
            panel1 = new Panel();
            listInstruct = new ListBox();
            edInstruct = new TextBox();
            button1 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(button1);
            panel1.Controls.Add(edInstruct);
            panel1.Controls.Add(listInstruct);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(234, 464);
            panel1.TabIndex = 1;
            // 
            // listInstruct
            // 
            listInstruct.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listInstruct.BorderStyle = BorderStyle.FixedSingle;
            listInstruct.FormattingEnabled = true;
            listInstruct.Location = new Point(3, 3);
            listInstruct.Name = "listInstruct";
            listInstruct.ScrollAlwaysVisible = true;
            listInstruct.Size = new Size(228, 392);
            listInstruct.TabIndex = 1;
            // 
            // edInstruct
            // 
            edInstruct.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            edInstruct.Location = new Point(3, 409);
            edInstruct.Name = "edInstruct";
            edInstruct.Size = new Size(228, 23);
            edInstruct.TabIndex = 2;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.Location = new Point(3, 438);
            button1.Name = "button1";
            button1.Size = new Size(228, 23);
            button1.TabIndex = 3;
            button1.Text = "Save Instruction Format";
            button1.UseVisualStyleBackColor = true;
            // 
            // InstructForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(912, 464);
            Controls.Add(panel1);
            Name = "InstructForm";
            Text = "InstructForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private ListBox listInstruct;
        private Button button1;
        private TextBox edInstruct;
    }
}