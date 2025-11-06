namespace WaifuAI.src.forms
{
    partial class MemoryEditorForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemoryEditorForm));
            tabControl = new TabControl();
            tabBasic = new TabPage();
            grpBasicInfo = new GroupBox();
            lblTitle = new Label();
            edTitle = new TextBox();
            lblCategory = new Label();
            cbCategory = new WaifuAI.Controls.ModernComboBox();
            lblInsertion = new Label();
            cbInsertion = new WaifuAI.Controls.ModernComboBox();
            lblContent = new Label();
            edContent = new TextBox();
            lblReason = new Label();
            edReason = new TextBox();
            lblPath = new Label();
            edPath = new TextBox();
            btBrowsePath = new Button();
            tabAdvanced = new TabPage();
            grpKeywords = new GroupBox();
            ckCaseSensitive = new CheckBox();
            lblKeywordsMain = new Label();
            edKeywordsMain = new TextBox();
            lblKeywordsSecondary = new Label();
            edKeywordsSecondary = new TextBox();
            lblWordLink = new Label();
            cbWordLink = new WaifuAI.Controls.ModernComboBox();
            lblKeywordHelp = new Label();
            grpSettings = new GroupBox();
            lblPriority = new Label();
            numPriority = new NumericUpDown();
            lblDuration = new Label();
            numDuration = new NumericUpDown();
            lblPosition = new Label();
            numPosition = new NumericUpDown();
            lblTriggerChance = new Label();
            numTriggerChance = new NumericUpDown();
            ckEnabled = new CheckBox();
            ckSticky = new CheckBox();
            grpDates = new GroupBox();
            lblAdded = new Label();
            dtpAdded = new DateTimePicker();
            lblEndTime = new Label();
            dtpEndTime = new DateTimePicker();
            panelButtons = new Panel();
            btSave = new Button();
            btCancel = new Button();
            btGenerateEmbedding = new Button();
            ckProtecc = new CheckBox();
            tabControl.SuspendLayout();
            tabBasic.SuspendLayout();
            grpBasicInfo.SuspendLayout();
            tabAdvanced.SuspendLayout();
            grpKeywords.SuspendLayout();
            grpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPriority).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPosition).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTriggerChance).BeginInit();
            grpDates.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabBasic);
            tabControl.Controls.Add(tabAdvanced);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(684, 561);
            tabControl.TabIndex = 0;
            // 
            // tabBasic
            // 
            tabBasic.Controls.Add(grpBasicInfo);
            tabBasic.Location = new Point(4, 24);
            tabBasic.Name = "tabBasic";
            tabBasic.Padding = new Padding(8);
            tabBasic.Size = new Size(676, 533);
            tabBasic.TabIndex = 0;
            tabBasic.Text = "Basic Info";
            tabBasic.UseVisualStyleBackColor = true;
            // 
            // grpBasicInfo
            // 
            grpBasicInfo.Controls.Add(lblTitle);
            grpBasicInfo.Controls.Add(edTitle);
            grpBasicInfo.Controls.Add(lblCategory);
            grpBasicInfo.Controls.Add(cbCategory);
            grpBasicInfo.Controls.Add(lblInsertion);
            grpBasicInfo.Controls.Add(cbInsertion);
            grpBasicInfo.Controls.Add(lblContent);
            grpBasicInfo.Controls.Add(edContent);
            grpBasicInfo.Controls.Add(lblReason);
            grpBasicInfo.Controls.Add(edReason);
            grpBasicInfo.Controls.Add(lblPath);
            grpBasicInfo.Controls.Add(edPath);
            grpBasicInfo.Controls.Add(btBrowsePath);
            grpBasicInfo.Dock = DockStyle.Fill;
            grpBasicInfo.Location = new Point(8, 8);
            grpBasicInfo.Name = "grpBasicInfo";
            grpBasicInfo.Padding = new Padding(8);
            grpBasicInfo.Size = new Size(660, 517);
            grpBasicInfo.TabIndex = 0;
            grpBasicInfo.TabStop = false;
            grpBasicInfo.Text = "Basic Information";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(11, 27);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(33, 15);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Title:";
            // 
            // edTitle
            // 
            edTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edTitle.Location = new Point(100, 24);
            edTitle.Name = "edTitle";
            edTitle.Size = new Size(549, 23);
            edTitle.TabIndex = 1;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(11, 56);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(58, 15);
            lblCategory.TabIndex = 2;
            lblCategory.Text = "Category:";
            // 
            // cbCategory
            // 
            cbCategory.BackColor = Color.FromArgb(64, 64, 64);
            cbCategory.DropDownHeight = 180;
            cbCategory.Font = new Font("Segoe UI", 9F);
            cbCategory.Location = new Point(100, 53);
            cbCategory.MaxDropDownItems = 10;
            cbCategory.Name = "cbCategory";
            cbCategory.Padding = new Padding(1);
            cbCategory.Size = new Size(200, 23);
            cbCategory.TabIndex = 3;
            cbCategory.SelectedIndexChanged += cbCategory_SelectedIndexChanged;
            // 
            // lblInsertion
            // 
            lblInsertion.AutoSize = true;
            lblInsertion.Location = new Point(320, 56);
            lblInsertion.Name = "lblInsertion";
            lblInsertion.Size = new Size(56, 15);
            lblInsertion.TabIndex = 4;
            lblInsertion.Text = "Insertion:";
            // 
            // cbInsertion
            // 
            cbInsertion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbInsertion.BackColor = Color.FromArgb(64, 64, 64);
            cbInsertion.DropDownHeight = 180;
            cbInsertion.Font = new Font("Segoe UI", 9F);
            cbInsertion.Location = new Point(384, 53);
            cbInsertion.MaxDropDownItems = 10;
            cbInsertion.Name = "cbInsertion";
            cbInsertion.Padding = new Padding(1);
            cbInsertion.Size = new Size(265, 23);
            cbInsertion.TabIndex = 5;
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Location = new Point(11, 85);
            lblContent.Name = "lblContent";
            lblContent.Size = new Size(53, 15);
            lblContent.TabIndex = 6;
            lblContent.Text = "Content:";
            // 
            // edContent
            // 
            edContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            edContent.Location = new Point(11, 103);
            edContent.Multiline = true;
            edContent.Name = "edContent";
            edContent.ScrollBars = ScrollBars.Vertical;
            edContent.Size = new Size(638, 250);
            edContent.TabIndex = 7;
            // 
            // lblReason
            // 
            lblReason.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblReason.AutoSize = true;
            lblReason.Location = new Point(11, 362);
            lblReason.Name = "lblReason";
            lblReason.Size = new Size(48, 15);
            lblReason.TabIndex = 8;
            lblReason.Text = "Reason:";
            // 
            // edReason
            // 
            edReason.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            edReason.Location = new Point(11, 380);
            edReason.Multiline = true;
            edReason.Name = "edReason";
            edReason.ScrollBars = ScrollBars.Vertical;
            edReason.Size = new Size(638, 80);
            edReason.TabIndex = 9;
            // 
            // lblPath
            // 
            lblPath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblPath.AutoSize = true;
            lblPath.Location = new Point(11, 469);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(34, 15);
            lblPath.TabIndex = 10;
            lblPath.Text = "Path:";
            lblPath.Visible = false;
            // 
            // edPath
            // 
            edPath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            edPath.Location = new Point(100, 466);
            edPath.Name = "edPath";
            edPath.Size = new Size(468, 23);
            edPath.TabIndex = 11;
            edPath.Visible = false;
            // 
            // btBrowsePath
            // 
            btBrowsePath.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btBrowsePath.Location = new Point(574, 465);
            btBrowsePath.Name = "btBrowsePath";
            btBrowsePath.Size = new Size(75, 25);
            btBrowsePath.TabIndex = 12;
            btBrowsePath.Text = "Browse...";
            btBrowsePath.UseVisualStyleBackColor = true;
            btBrowsePath.Visible = false;
            btBrowsePath.Click += btBrowsePath_Click;
            // 
            // tabAdvanced
            // 
            tabAdvanced.Controls.Add(grpKeywords);
            tabAdvanced.Controls.Add(grpSettings);
            tabAdvanced.Controls.Add(grpDates);
            tabAdvanced.Location = new Point(4, 24);
            tabAdvanced.Name = "tabAdvanced";
            tabAdvanced.Padding = new Padding(8);
            tabAdvanced.Size = new Size(676, 533);
            tabAdvanced.TabIndex = 1;
            tabAdvanced.Text = "Advanced";
            tabAdvanced.UseVisualStyleBackColor = true;
            // 
            // grpKeywords
            // 
            grpKeywords.Controls.Add(ckCaseSensitive);
            grpKeywords.Controls.Add(lblKeywordsMain);
            grpKeywords.Controls.Add(edKeywordsMain);
            grpKeywords.Controls.Add(lblKeywordsSecondary);
            grpKeywords.Controls.Add(edKeywordsSecondary);
            grpKeywords.Controls.Add(lblWordLink);
            grpKeywords.Controls.Add(cbWordLink);
            grpKeywords.Controls.Add(lblKeywordHelp);
            grpKeywords.Location = new Point(9, 241);
            grpKeywords.Name = "grpKeywords";
            grpKeywords.Padding = new Padding(8);
            grpKeywords.Size = new Size(660, 281);
            grpKeywords.TabIndex = 0;
            grpKeywords.TabStop = false;
            grpKeywords.Text = "Keyword Triggers";
            // 
            // ckCaseSensitive
            // 
            ckCaseSensitive.AutoSize = true;
            ckCaseSensitive.Location = new Point(324, 176);
            ckCaseSensitive.Name = "ckCaseSensitive";
            ckCaseSensitive.Size = new Size(100, 19);
            ckCaseSensitive.TabIndex = 11;
            ckCaseSensitive.Text = "Case Sensitive";
            ckCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // lblKeywordsMain
            // 
            lblKeywordsMain.AutoSize = true;
            lblKeywordsMain.Location = new Point(11, 27);
            lblKeywordsMain.Name = "lblKeywordsMain";
            lblKeywordsMain.Size = new Size(91, 15);
            lblKeywordsMain.TabIndex = 0;
            lblKeywordsMain.Text = "Main Keywords:";
            // 
            // edKeywordsMain
            // 
            edKeywordsMain.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edKeywordsMain.Location = new Point(11, 45);
            edKeywordsMain.Multiline = true;
            edKeywordsMain.Name = "edKeywordsMain";
            edKeywordsMain.ScrollBars = ScrollBars.Vertical;
            edKeywordsMain.Size = new Size(638, 50);
            edKeywordsMain.TabIndex = 1;
            // 
            // lblKeywordsSecondary
            // 
            lblKeywordsSecondary.AutoSize = true;
            lblKeywordsSecondary.Location = new Point(11, 98);
            lblKeywordsSecondary.Name = "lblKeywordsSecondary";
            lblKeywordsSecondary.Size = new Size(119, 15);
            lblKeywordsSecondary.TabIndex = 2;
            lblKeywordsSecondary.Text = "Secondary Keywords:";
            // 
            // edKeywordsSecondary
            // 
            edKeywordsSecondary.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edKeywordsSecondary.Location = new Point(11, 116);
            edKeywordsSecondary.Multiline = true;
            edKeywordsSecondary.Name = "edKeywordsSecondary";
            edKeywordsSecondary.ScrollBars = ScrollBars.Vertical;
            edKeywordsSecondary.Size = new Size(638, 50);
            edKeywordsSecondary.TabIndex = 3;
            // 
            // lblWordLink
            // 
            lblWordLink.AutoSize = true;
            lblWordLink.Location = new Point(11, 177);
            lblWordLink.Name = "lblWordLink";
            lblWordLink.Size = new Size(64, 15);
            lblWordLink.TabIndex = 4;
            lblWordLink.Text = "Word Link:";
            // 
            // cbWordLink
            // 
            cbWordLink.BackColor = Color.FromArgb(64, 64, 64);
            cbWordLink.DropDownHeight = 180;
            cbWordLink.Font = new Font("Segoe UI", 9F);
            cbWordLink.Location = new Point(118, 172);
            cbWordLink.MaxDropDownItems = 10;
            cbWordLink.Name = "cbWordLink";
            cbWordLink.Padding = new Padding(1);
            cbWordLink.Size = new Size(200, 23);
            cbWordLink.TabIndex = 5;
            // 
            // lblKeywordHelp
            // 
            lblKeywordHelp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblKeywordHelp.Location = new Point(11, 205);
            lblKeywordHelp.Name = "lblKeywordHelp";
            lblKeywordHelp.Size = new Size(638, 68);
            lblKeywordHelp.TabIndex = 6;
            lblKeywordHelp.Text = resources.GetString("lblKeywordHelp.Text");
            // 
            // grpSettings
            // 
            grpSettings.Controls.Add(ckProtecc);
            grpSettings.Controls.Add(lblPriority);
            grpSettings.Controls.Add(numPriority);
            grpSettings.Controls.Add(lblDuration);
            grpSettings.Controls.Add(numDuration);
            grpSettings.Controls.Add(lblPosition);
            grpSettings.Controls.Add(numPosition);
            grpSettings.Controls.Add(lblTriggerChance);
            grpSettings.Controls.Add(numTriggerChance);
            grpSettings.Controls.Add(ckEnabled);
            grpSettings.Controls.Add(ckSticky);
            grpSettings.Dock = DockStyle.Top;
            grpSettings.Location = new Point(8, 108);
            grpSettings.Name = "grpSettings";
            grpSettings.Padding = new Padding(8);
            grpSettings.Size = new Size(660, 127);
            grpSettings.TabIndex = 0;
            grpSettings.TabStop = false;
            grpSettings.Text = "Settings";
            // 
            // lblPriority
            // 
            lblPriority.AutoSize = true;
            lblPriority.Location = new Point(11, 27);
            lblPriority.Name = "lblPriority";
            lblPriority.Size = new Size(48, 15);
            lblPriority.TabIndex = 0;
            lblPriority.Text = "Priority:";
            // 
            // numPriority
            // 
            numPriority.Location = new Point(120, 25);
            numPriority.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numPriority.Name = "numPriority";
            numPriority.Size = new Size(120, 23);
            numPriority.TabIndex = 1;
            numPriority.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblDuration
            // 
            lblDuration.AutoSize = true;
            lblDuration.Location = new Point(11, 56);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new Size(56, 15);
            lblDuration.TabIndex = 2;
            lblDuration.Text = "Duration:";
            // 
            // numDuration
            // 
            numDuration.Location = new Point(120, 54);
            numDuration.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numDuration.Name = "numDuration";
            numDuration.Size = new Size(120, 23);
            numDuration.TabIndex = 3;
            numDuration.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblPosition
            // 
            lblPosition.AutoSize = true;
            lblPosition.Location = new Point(280, 27);
            lblPosition.Name = "lblPosition";
            lblPosition.Size = new Size(84, 15);
            lblPosition.TabIndex = 4;
            lblPosition.Text = "Position Index:";
            // 
            // numPosition
            // 
            numPosition.Location = new Point(374, 25);
            numPosition.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numPosition.Name = "numPosition";
            numPosition.Size = new Size(120, 23);
            numPosition.TabIndex = 5;
            // 
            // lblTriggerChance
            // 
            lblTriggerChance.AutoSize = true;
            lblTriggerChance.Location = new Point(280, 56);
            lblTriggerChance.Name = "lblTriggerChance";
            lblTriggerChance.Size = new Size(90, 15);
            lblTriggerChance.TabIndex = 6;
            lblTriggerChance.Text = "Trigger Chance:";
            // 
            // numTriggerChance
            // 
            numTriggerChance.DecimalPlaces = 2;
            numTriggerChance.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numTriggerChance.Location = new Point(374, 54);
            numTriggerChance.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numTriggerChance.Name = "numTriggerChance";
            numTriggerChance.Size = new Size(120, 23);
            numTriggerChance.TabIndex = 7;
            numTriggerChance.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // ckEnabled
            // 
            ckEnabled.AutoSize = true;
            ckEnabled.Checked = true;
            ckEnabled.CheckState = CheckState.Checked;
            ckEnabled.Location = new Point(11, 100);
            ckEnabled.Name = "ckEnabled";
            ckEnabled.Size = new Size(68, 19);
            ckEnabled.TabIndex = 8;
            ckEnabled.Text = "Enabled";
            ckEnabled.UseVisualStyleBackColor = true;
            ckEnabled.CheckedChanged += ckEnabled_CheckedChanged;
            // 
            // ckSticky
            // 
            ckSticky.AutoSize = true;
            ckSticky.Location = new Point(120, 100);
            ckSticky.Name = "ckSticky";
            ckSticky.Size = new Size(57, 19);
            ckSticky.TabIndex = 9;
            ckSticky.Text = "Sticky";
            ckSticky.UseVisualStyleBackColor = true;
            // 
            // grpDates
            // 
            grpDates.Controls.Add(lblAdded);
            grpDates.Controls.Add(dtpAdded);
            grpDates.Controls.Add(lblEndTime);
            grpDates.Controls.Add(dtpEndTime);
            grpDates.Dock = DockStyle.Top;
            grpDates.Location = new Point(8, 8);
            grpDates.Name = "grpDates";
            grpDates.Padding = new Padding(8);
            grpDates.Size = new Size(660, 100);
            grpDates.TabIndex = 1;
            grpDates.TabStop = false;
            grpDates.Text = "Dates";
            // 
            // lblAdded
            // 
            lblAdded.AutoSize = true;
            lblAdded.Location = new Point(11, 27);
            lblAdded.Name = "lblAdded";
            lblAdded.Size = new Size(72, 15);
            lblAdded.TabIndex = 0;
            lblAdded.Text = "Added Date:";
            // 
            // dtpAdded
            // 
            dtpAdded.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpAdded.Format = DateTimePickerFormat.Custom;
            dtpAdded.Location = new Point(120, 24);
            dtpAdded.Name = "dtpAdded";
            dtpAdded.Size = new Size(200, 23);
            dtpAdded.TabIndex = 1;
            // 
            // lblEndTime
            // 
            lblEndTime.AutoSize = true;
            lblEndTime.Location = new Point(11, 56);
            lblEndTime.Name = "lblEndTime";
            lblEndTime.Size = new Size(60, 15);
            lblEndTime.TabIndex = 2;
            lblEndTime.Text = "End Time:";
            // 
            // dtpEndTime
            // 
            dtpEndTime.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.Location = new Point(120, 53);
            dtpEndTime.Name = "dtpEndTime";
            dtpEndTime.Size = new Size(200, 23);
            dtpEndTime.TabIndex = 3;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(btSave);
            panelButtons.Controls.Add(btCancel);
            panelButtons.Controls.Add(btGenerateEmbedding);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Location = new Point(0, 561);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(8);
            panelButtons.Size = new Size(684, 50);
            panelButtons.TabIndex = 1;
            // 
            // btSave
            // 
            btSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btSave.BackColor = Color.DarkSeaGreen;
            btSave.FlatStyle = FlatStyle.Flat;
            btSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btSave.Location = new Point(449, 11);
            btSave.Name = "btSave";
            btSave.Size = new Size(110, 30);
            btSave.TabIndex = 0;
            btSave.Tag = "no-theme";
            btSave.Text = "Save";
            btSave.UseVisualStyleBackColor = false;
            btSave.Click += btSave_Click;
            // 
            // btCancel
            // 
            btCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btCancel.BackColor = Color.IndianRed;
            btCancel.FlatStyle = FlatStyle.Flat;
            btCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btCancel.Location = new Point(565, 11);
            btCancel.Name = "btCancel";
            btCancel.Size = new Size(110, 30);
            btCancel.TabIndex = 1;
            btCancel.Tag = "no-theme";
            btCancel.Text = "Cancel";
            btCancel.UseVisualStyleBackColor = false;
            btCancel.Click += btCancel_Click;
            // 
            // btGenerateEmbedding
            // 
            btGenerateEmbedding.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btGenerateEmbedding.Location = new Point(11, 11);
            btGenerateEmbedding.Name = "btGenerateEmbedding";
            btGenerateEmbedding.Size = new Size(140, 30);
            btGenerateEmbedding.TabIndex = 2;
            btGenerateEmbedding.Text = "Generate Embedding";
            btGenerateEmbedding.UseVisualStyleBackColor = true;
            btGenerateEmbedding.Click += btGenerateEmbedding_Click;
            // 
            // ckProtecc
            // 
            ckProtecc.AutoSize = true;
            ckProtecc.Location = new Point(210, 100);
            ckProtecc.Name = "ckProtecc";
            ckProtecc.Size = new Size(77, 19);
            ckProtecc.TabIndex = 10;
            ckProtecc.Text = "Protected";
            ckProtecc.UseVisualStyleBackColor = true;
            // 
            // MemoryEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 611);
            Controls.Add(tabControl);
            Controls.Add(panelButtons);
            MinimizeBox = false;
            MinimumSize = new Size(700, 650);
            Name = "MemoryEditorForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Memory Editor";
            tabControl.ResumeLayout(false);
            tabBasic.ResumeLayout(false);
            grpBasicInfo.ResumeLayout(false);
            grpBasicInfo.PerformLayout();
            tabAdvanced.ResumeLayout(false);
            grpKeywords.ResumeLayout(false);
            grpKeywords.PerformLayout();
            grpSettings.ResumeLayout(false);
            grpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPriority).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPosition).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTriggerChance).EndInit();
            grpDates.ResumeLayout(false);
            grpDates.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage tabBasic;
        private GroupBox grpBasicInfo;
        private Label lblTitle;
        private TextBox edTitle;
        private Label lblCategory;
        private Controls.ModernComboBox cbCategory;
        private Label lblInsertion;
        private Controls.ModernComboBox cbInsertion;
        private Label lblContent;
        private TextBox edContent;
        private Label lblReason;
        private TextBox edReason;
        private Label lblPath;
        private TextBox edPath;
        private Button btBrowsePath;
        private TabPage tabAdvanced;
        private GroupBox grpSettings;
        private Label lblPriority;
        private NumericUpDown numPriority;
        private Label lblDuration;
        private NumericUpDown numDuration;
        private Label lblPosition;
        private NumericUpDown numPosition;
        private Label lblTriggerChance;
        private NumericUpDown numTriggerChance;
        private CheckBox ckEnabled;
        private CheckBox ckSticky;
        private GroupBox grpDates;
        private Label lblAdded;
        private DateTimePicker dtpAdded;
        private Label lblEndTime;
        private DateTimePicker dtpEndTime;
        private GroupBox grpKeywords;
        private Label lblKeywordsMain;
        private TextBox edKeywordsMain;
        private Label lblKeywordsSecondary;
        private TextBox edKeywordsSecondary;
        private Label lblWordLink;
        private Controls.ModernComboBox cbWordLink;
        private Label lblKeywordHelp;
        private Panel panelButtons;
        private Button btSave;
        private Button btCancel;
        private Button btGenerateEmbedding;
        private CheckBox ckCaseSensitive;
        private CheckBox ckProtecc;
    }
}
