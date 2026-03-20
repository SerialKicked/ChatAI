using LetheChat.Controls;

namespace LetheChat.src.forms
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
            panel4 = new Panel();
            edInstruct = new TextBox();
            panel3 = new Panel();
            btSave = new Button();
            listInstruct = new ModernListBox();
            panel2 = new Panel();
            panContent = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(edInstruct);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(btSave);
            panel1.Controls.Add(listInstruct);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(234, 738);
            panel1.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 661);
            panel4.Margin = new Padding(8);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(8);
            panel4.Size = new Size(234, 10);
            panel4.TabIndex = 51;
            // 
            // edInstruct
            // 
            edInstruct.BorderStyle = BorderStyle.FixedSingle;
            edInstruct.Dock = DockStyle.Bottom;
            edInstruct.Location = new Point(0, 671);
            edInstruct.Name = "edInstruct";
            edInstruct.Size = new Size(234, 23);
            edInstruct.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 694);
            panel3.Margin = new Padding(8);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(8);
            panel3.Size = new Size(234, 10);
            panel3.TabIndex = 50;
            // 
            // btSave
            // 
            btSave.BackColor = Color.DarkSeaGreen;
            btSave.Dock = DockStyle.Bottom;
            btSave.FlatStyle = FlatStyle.Flat;
            btSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btSave.Location = new Point(0, 704);
            btSave.Name = "btSave";
            btSave.Size = new Size(234, 24);
            btSave.TabIndex = 3;
            btSave.Text = "Save Instruction Format";
            btSave.UseVisualStyleBackColor = false;
            btSave.Click += btSave_Click;
            // 
            // listInstruct
            // 
            listInstruct.BackColor = Color.FromArgb(64, 64, 64);
            listInstruct.BorderStyle = BorderStyle.FixedSingle;
            listInstruct.Dock = DockStyle.Fill;
            listInstruct.Font = new Font("Segoe UI", 9.25F);
            listInstruct.Location = new Point(0, 0);
            listInstruct.Name = "listInstruct";
            listInstruct.Padding = new Padding(1);
            listInstruct.Size = new Size(234, 706);
            listInstruct.TabIndex = 1;
            listInstruct.SelectedIndexChanged += listInstruct_SelectedIndexChanged;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 728);
            panel2.Margin = new Padding(8);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(8);
            panel2.Size = new Size(234, 10);
            panel2.TabIndex = 49;
            // 
            // panContent
            // 
            panContent.BorderStyle = BorderStyle.FixedSingle;
            panContent.Dock = DockStyle.Fill;
            panContent.Location = new Point(234, 0);
            panContent.Name = "panContent";
            panContent.Size = new Size(678, 738);
            panContent.TabIndex = 2;
            // 
            // InstructForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(912, 738);
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
        private ModernListBox listInstruct;
        private Button btSave;
        private TextBox edInstruct;
        private Panel panContent;
        private Panel panel3;
        private Panel panel2;
        private Panel panel4;
    }
}