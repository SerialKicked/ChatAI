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
            btSave = new Button();
            edInstruct = new TextBox();
            listInstruct = new ListBox();
            panContent = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btSave);
            panel1.Controls.Add(edInstruct);
            panel1.Controls.Add(listInstruct);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(234, 575);
            panel1.TabIndex = 1;
            // 
            // btSave
            // 
            btSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btSave.Location = new Point(3, 549);
            btSave.Name = "btSave";
            btSave.Size = new Size(228, 23);
            btSave.TabIndex = 3;
            btSave.Text = "Save Instruction Format";
            btSave.UseVisualStyleBackColor = true;
            btSave.Click += btSave_Click;
            // 
            // edInstruct
            // 
            edInstruct.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            edInstruct.Location = new Point(3, 520);
            edInstruct.Name = "edInstruct";
            edInstruct.Size = new Size(228, 23);
            edInstruct.TabIndex = 2;
            // 
            // listInstruct
            // 
            listInstruct.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listInstruct.BorderStyle = BorderStyle.FixedSingle;
            listInstruct.FormattingEnabled = true;
            listInstruct.Location = new Point(3, 3);
            listInstruct.Name = "listInstruct";
            listInstruct.ScrollAlwaysVisible = true;
            listInstruct.Size = new Size(228, 497);
            listInstruct.TabIndex = 1;
            listInstruct.SelectedIndexChanged += listInstruct_SelectedIndexChanged;
            // 
            // panContent
            // 
            panContent.BorderStyle = BorderStyle.FixedSingle;
            panContent.Dock = DockStyle.Fill;
            panContent.Location = new Point(234, 0);
            panContent.Name = "panContent";
            panContent.Size = new Size(678, 575);
            panContent.TabIndex = 2;
            // 
            // InstructForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(912, 575);
            Controls.Add(panContent);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InstructForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Instruction Format Editor";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private ListBox listInstruct;
        private Button btSave;
        private TextBox edInstruct;
        private Panel panContent;
    }
}