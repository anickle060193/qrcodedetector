namespace QrCodeDetector
{
    partial class QrCodeDetectorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.uxImageDirectoryBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.uxFileSystemWatcher = new System.IO.FileSystemWatcher();
            this.uxEnhance = new System.Windows.Forms.Button();
            this.uxValue = new System.Windows.Forms.NumericUpDown();
            this.uxTimer = new System.Windows.Forms.Timer(this.components);
            this.uxDataGrid = new System.Windows.Forms.DataGridView();
            this.QrCodeData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uxImageDisplay = new System.Windows.Forms.PictureBox();
            this.uxHorizontalSplitContainer = new System.Windows.Forms.SplitContainer();
            this.uxControlsGroup = new System.Windows.Forms.GroupBox();
            this.uxShowQuadImages = new System.Windows.Forms.CheckBox();
            this.uxShowEnchancedImage = new System.Windows.Forms.CheckBox();
            this.uxAutoDetectControls = new System.Windows.Forms.GroupBox();
            this.uxAutoDetectOnAdd = new System.Windows.Forms.RadioButton();
            this.uxAutoDetectOnView = new System.Windows.Forms.RadioButton();
            this.uxAutoDetectDisabled = new System.Windows.Forms.RadioButton();
            this.uxAutoAddImages = new System.Windows.Forms.CheckBox();
            this.uxAddImages = new System.Windows.Forms.Button();
            this.uxSetImageWatchDirectory = new System.Windows.Forms.Button();
            this.uxValueLabel = new System.Windows.Forms.Label();
            this.uxQrDataLabel = new System.Windows.Forms.Label();
            this.uxStatusStrip = new System.Windows.Forms.StatusStrip();
            this.uxStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.uxBytesUsed = new System.Windows.Forms.ToolStripStatusLabel();
            this.uxOutput = new System.Windows.Forms.GroupBox();
            this.uxQrCodeData = new System.Windows.Forms.RichTextBox();
            this.uxOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.Filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uxImageHolderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.uxFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.uxSetImageWatchDirectoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxMenu = new System.Windows.Forms.MenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.uxFileSystemWatcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxImageDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxHorizontalSplitContainer)).BeginInit();
            this.uxHorizontalSplitContainer.Panel1.SuspendLayout();
            this.uxHorizontalSplitContainer.Panel2.SuspendLayout();
            this.uxHorizontalSplitContainer.SuspendLayout();
            this.uxControlsGroup.SuspendLayout();
            this.uxAutoDetectControls.SuspendLayout();
            this.uxStatusStrip.SuspendLayout();
            this.uxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxImageHolderBindingSource)).BeginInit();
            this.uxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxFileSystemWatcher
            // 
            this.uxFileSystemWatcher.EnableRaisingEvents = true;
            this.uxFileSystemWatcher.IncludeSubdirectories = true;
            this.uxFileSystemWatcher.SynchronizingObject = this;
            // 
            // uxEnhance
            // 
            this.uxEnhance.Location = new System.Drawing.Point(6, 180);
            this.uxEnhance.Name = "uxEnhance";
            this.uxEnhance.Size = new System.Drawing.Size(218, 23);
            this.uxEnhance.TabIndex = 4;
            this.uxEnhance.Text = "Enhance!";
            this.uxEnhance.UseVisualStyleBackColor = true;
            this.uxEnhance.Click += new System.EventHandler(this.uxEnhance_Click);
            // 
            // uxValue
            // 
            this.uxValue.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.uxValue.Location = new System.Drawing.Point(49, 154);
            this.uxValue.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.uxValue.Name = "uxValue";
            this.uxValue.Size = new System.Drawing.Size(175, 20);
            this.uxValue.TabIndex = 5;
            this.uxValue.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // uxTimer
            // 
            this.uxTimer.Enabled = true;
            this.uxTimer.Tick += new System.EventHandler(this.uxTimer_Tick);
            // 
            // uxDataGrid
            // 
            this.uxDataGrid.AllowUserToAddRows = false;
            this.uxDataGrid.AllowUserToDeleteRows = false;
            this.uxDataGrid.AllowUserToOrderColumns = true;
            this.uxDataGrid.AllowUserToResizeRows = false;
            this.uxDataGrid.AutoGenerateColumns = false;
            this.uxDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.uxDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uxDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Filename,
            this.QrCodeData});
            this.uxDataGrid.DataSource = this.uxImageHolderBindingSource;
            this.uxDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxDataGrid.Location = new System.Drawing.Point(0, 0);
            this.uxDataGrid.MultiSelect = false;
            this.uxDataGrid.Name = "uxDataGrid";
            this.uxDataGrid.ReadOnly = true;
            this.uxDataGrid.RowHeadersVisible = false;
            this.uxDataGrid.Size = new System.Drawing.Size(574, 142);
            this.uxDataGrid.TabIndex = 0;
            this.uxDataGrid.CurrentCellChanged += new System.EventHandler(this.uxDataGrid_CurrentCellChanged);
            this.uxDataGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.uxDataGrid_KeyUp);
            // 
            // QrCodeData
            // 
            this.QrCodeData.DataPropertyName = "QrCodeData";
            this.QrCodeData.HeaderText = "Qr Code Data";
            this.QrCodeData.Name = "QrCodeData";
            this.QrCodeData.ReadOnly = true;
            // 
            // uxImageDisplay
            // 
            this.uxImageDisplay.Location = new System.Drawing.Point(3, 3);
            this.uxImageDisplay.Name = "uxImageDisplay";
            this.uxImageDisplay.Size = new System.Drawing.Size(96, 94);
            this.uxImageDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.uxImageDisplay.TabIndex = 1;
            this.uxImageDisplay.TabStop = false;
            this.uxImageDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.uxImageDisplay_Paint);
            this.uxImageDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.uxImageDisplay_MouseDown);
            this.uxImageDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.uxImageDisplay_MouseMove);
            this.uxImageDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.uxImageDisplay_MouseUp);
            // 
            // uxHorizontalSplitContainer
            // 
            this.uxHorizontalSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxHorizontalSplitContainer.Location = new System.Drawing.Point(248, 27);
            this.uxHorizontalSplitContainer.Name = "uxHorizontalSplitContainer";
            this.uxHorizontalSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // uxHorizontalSplitContainer.Panel1
            // 
            this.uxHorizontalSplitContainer.Panel1.AutoScroll = true;
            this.uxHorizontalSplitContainer.Panel1.Controls.Add(this.uxImageDisplay);
            // 
            // uxHorizontalSplitContainer.Panel2
            // 
            this.uxHorizontalSplitContainer.Panel2.AutoScroll = true;
            this.uxHorizontalSplitContainer.Panel2.Controls.Add(this.uxDataGrid);
            this.uxHorizontalSplitContainer.Size = new System.Drawing.Size(574, 509);
            this.uxHorizontalSplitContainer.SplitterDistance = 363;
            this.uxHorizontalSplitContainer.TabIndex = 8;
            // 
            // uxControlsGroup
            // 
            this.uxControlsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.uxControlsGroup.Controls.Add(this.uxShowQuadImages);
            this.uxControlsGroup.Controls.Add(this.uxShowEnchancedImage);
            this.uxControlsGroup.Controls.Add(this.uxAutoDetectControls);
            this.uxControlsGroup.Controls.Add(this.uxAutoAddImages);
            this.uxControlsGroup.Controls.Add(this.uxAddImages);
            this.uxControlsGroup.Controls.Add(this.uxSetImageWatchDirectory);
            this.uxControlsGroup.Controls.Add(this.uxValueLabel);
            this.uxControlsGroup.Controls.Add(this.uxValue);
            this.uxControlsGroup.Controls.Add(this.uxEnhance);
            this.uxControlsGroup.Location = new System.Drawing.Point(12, 96);
            this.uxControlsGroup.Name = "uxControlsGroup";
            this.uxControlsGroup.Size = new System.Drawing.Size(230, 440);
            this.uxControlsGroup.TabIndex = 9;
            this.uxControlsGroup.TabStop = false;
            this.uxControlsGroup.Text = "Controls";
            // 
            // uxShowQuadImages
            // 
            this.uxShowQuadImages.AutoSize = true;
            this.uxShowQuadImages.Location = new System.Drawing.Point(6, 232);
            this.uxShowQuadImages.Name = "uxShowQuadImages";
            this.uxShowQuadImages.Size = new System.Drawing.Size(152, 17);
            this.uxShowQuadImages.TabIndex = 13;
            this.uxShowQuadImages.Text = "Show Quadrilateral Images";
            this.uxShowQuadImages.UseVisualStyleBackColor = true;
            // 
            // uxShowEnchancedImage
            // 
            this.uxShowEnchancedImage.AutoSize = true;
            this.uxShowEnchancedImage.Location = new System.Drawing.Point(6, 209);
            this.uxShowEnchancedImage.Name = "uxShowEnchancedImage";
            this.uxShowEnchancedImage.Size = new System.Drawing.Size(170, 17);
            this.uxShowEnchancedImage.TabIndex = 12;
            this.uxShowEnchancedImage.Text = "Show Enhanced Edges Image";
            this.uxShowEnchancedImage.UseVisualStyleBackColor = true;
            // 
            // uxAutoDetectControls
            // 
            this.uxAutoDetectControls.Controls.Add(this.uxAutoDetectOnAdd);
            this.uxAutoDetectControls.Controls.Add(this.uxAutoDetectOnView);
            this.uxAutoDetectControls.Controls.Add(this.uxAutoDetectDisabled);
            this.uxAutoDetectControls.Location = new System.Drawing.Point(6, 100);
            this.uxAutoDetectControls.Name = "uxAutoDetectControls";
            this.uxAutoDetectControls.Size = new System.Drawing.Size(215, 48);
            this.uxAutoDetectControls.TabIndex = 11;
            this.uxAutoDetectControls.TabStop = false;
            this.uxAutoDetectControls.Text = "Auto-Detect Options";
            // 
            // uxAutoDetectOnAdd
            // 
            this.uxAutoDetectOnAdd.AutoSize = true;
            this.uxAutoDetectOnAdd.Checked = true;
            this.uxAutoDetectOnAdd.Location = new System.Drawing.Point(149, 19);
            this.uxAutoDetectOnAdd.Name = "uxAutoDetectOnAdd";
            this.uxAutoDetectOnAdd.Size = new System.Drawing.Size(61, 17);
            this.uxAutoDetectOnAdd.TabIndex = 2;
            this.uxAutoDetectOnAdd.TabStop = true;
            this.uxAutoDetectOnAdd.Text = "On Add";
            this.uxAutoDetectOnAdd.UseVisualStyleBackColor = true;
            this.uxAutoDetectOnAdd.CheckedChanged += new System.EventHandler(this.AutoDetectOption_CheckedChanged);
            // 
            // uxAutoDetectOnView
            // 
            this.uxAutoDetectOnView.AutoSize = true;
            this.uxAutoDetectOnView.Location = new System.Drawing.Point(78, 19);
            this.uxAutoDetectOnView.Name = "uxAutoDetectOnView";
            this.uxAutoDetectOnView.Size = new System.Drawing.Size(65, 17);
            this.uxAutoDetectOnView.TabIndex = 1;
            this.uxAutoDetectOnView.Text = "On View";
            this.uxAutoDetectOnView.UseVisualStyleBackColor = true;
            this.uxAutoDetectOnView.CheckedChanged += new System.EventHandler(this.AutoDetectOption_CheckedChanged);
            // 
            // uxAutoDetectDisabled
            // 
            this.uxAutoDetectDisabled.AutoSize = true;
            this.uxAutoDetectDisabled.Location = new System.Drawing.Point(6, 19);
            this.uxAutoDetectDisabled.Name = "uxAutoDetectDisabled";
            this.uxAutoDetectDisabled.Size = new System.Drawing.Size(66, 17);
            this.uxAutoDetectDisabled.TabIndex = 0;
            this.uxAutoDetectDisabled.Text = "Disabled";
            this.uxAutoDetectDisabled.UseVisualStyleBackColor = true;
            this.uxAutoDetectDisabled.CheckedChanged += new System.EventHandler(this.AutoDetectOption_CheckedChanged);
            // 
            // uxAutoAddImages
            // 
            this.uxAutoAddImages.AutoSize = true;
            this.uxAutoAddImages.Location = new System.Drawing.Point(6, 48);
            this.uxAutoAddImages.Name = "uxAutoAddImages";
            this.uxAutoAddImages.Size = new System.Drawing.Size(210, 17);
            this.uxAutoAddImages.TabIndex = 9;
            this.uxAutoAddImages.Text = "Auto-Add Images from Watch Directory";
            this.uxAutoAddImages.UseVisualStyleBackColor = true;
            this.uxAutoAddImages.CheckedChanged += new System.EventHandler(this.uxAutoAddImages_CheckedChanged);
            // 
            // uxAddImages
            // 
            this.uxAddImages.Location = new System.Drawing.Point(6, 19);
            this.uxAddImages.Name = "uxAddImages";
            this.uxAddImages.Size = new System.Drawing.Size(218, 23);
            this.uxAddImages.TabIndex = 8;
            this.uxAddImages.Text = "Add Image(s)";
            this.uxAddImages.UseVisualStyleBackColor = true;
            this.uxAddImages.Click += new System.EventHandler(this.uxAddImages_Click);
            // 
            // uxSetImageWatchDirectory
            // 
            this.uxSetImageWatchDirectory.Location = new System.Drawing.Point(3, 71);
            this.uxSetImageWatchDirectory.Name = "uxSetImageWatchDirectory";
            this.uxSetImageWatchDirectory.Size = new System.Drawing.Size(218, 23);
            this.uxSetImageWatchDirectory.TabIndex = 7;
            this.uxSetImageWatchDirectory.Text = "Set Image Watch Directory";
            this.uxSetImageWatchDirectory.UseVisualStyleBackColor = true;
            this.uxSetImageWatchDirectory.Click += new System.EventHandler(this.uxSetImageDirectory_Click);
            // 
            // uxValueLabel
            // 
            this.uxValueLabel.AutoSize = true;
            this.uxValueLabel.Location = new System.Drawing.Point(6, 156);
            this.uxValueLabel.Name = "uxValueLabel";
            this.uxValueLabel.Size = new System.Drawing.Size(37, 13);
            this.uxValueLabel.TabIndex = 6;
            this.uxValueLabel.Text = "Value:";
            // 
            // uxQrDataLabel
            // 
            this.uxQrDataLabel.AutoSize = true;
            this.uxQrDataLabel.Location = new System.Drawing.Point(6, 16);
            this.uxQrDataLabel.Name = "uxQrDataLabel";
            this.uxQrDataLabel.Size = new System.Drawing.Size(80, 13);
            this.uxQrDataLabel.TabIndex = 7;
            this.uxQrDataLabel.Text = "QR Code Data:";
            // 
            // uxStatusStrip
            // 
            this.uxStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxStatusLabel,
            this.uxBytesUsed});
            this.uxStatusStrip.Location = new System.Drawing.Point(0, 539);
            this.uxStatusStrip.Name = "uxStatusStrip";
            this.uxStatusStrip.ShowItemToolTips = true;
            this.uxStatusStrip.Size = new System.Drawing.Size(834, 22);
            this.uxStatusStrip.TabIndex = 10;
            // 
            // uxStatusLabel
            // 
            this.uxStatusLabel.Name = "uxStatusLabel";
            this.uxStatusLabel.Size = new System.Drawing.Size(746, 17);
            this.uxStatusLabel.Spring = true;
            this.uxStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uxStatusLabel.Click += new System.EventHandler(this.uxStatusLabel_Click);
            // 
            // uxBytesUsed
            // 
            this.uxBytesUsed.Name = "uxBytesUsed";
            this.uxBytesUsed.Size = new System.Drawing.Size(73, 17);
            this.uxBytesUsed.Text = "0 Bytes Used";
            this.uxBytesUsed.ToolTipText = "Total Bytes Currently Used by Program";
            // 
            // uxOutput
            // 
            this.uxOutput.Controls.Add(this.uxQrCodeData);
            this.uxOutput.Controls.Add(this.uxQrDataLabel);
            this.uxOutput.Location = new System.Drawing.Point(12, 27);
            this.uxOutput.Name = "uxOutput";
            this.uxOutput.Size = new System.Drawing.Size(230, 63);
            this.uxOutput.TabIndex = 11;
            this.uxOutput.TabStop = false;
            this.uxOutput.Text = "Output";
            // 
            // uxQrCodeData
            // 
            this.uxQrCodeData.AutoWordSelection = true;
            this.uxQrCodeData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uxQrCodeData.Location = new System.Drawing.Point(6, 32);
            this.uxQrCodeData.Multiline = false;
            this.uxQrCodeData.Name = "uxQrCodeData";
            this.uxQrCodeData.ReadOnly = true;
            this.uxQrCodeData.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.uxQrCodeData.Size = new System.Drawing.Size(218, 25);
            this.uxQrCodeData.TabIndex = 8;
            this.uxQrCodeData.Text = "";
            this.uxQrCodeData.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.uxQrCodeData_LinkClicked);
            // 
            // uxOpenFileDialog
            // 
            this.uxOpenFileDialog.Multiselect = true;
            // 
            // Filename
            // 
            this.Filename.DataPropertyName = "Filename";
            this.Filename.HeaderText = "Filename";
            this.Filename.Name = "Filename";
            this.Filename.ReadOnly = true;
            // 
            // uxImageHolderBindingSource
            // 
            this.uxImageHolderBindingSource.DataSource = typeof(QrCodeDetector.ImageHolder);
            // 
            // uxFileMenu
            // 
            this.uxFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxSetImageWatchDirectoryMenuItem});
            this.uxFileMenu.Name = "uxFileMenu";
            this.uxFileMenu.Size = new System.Drawing.Size(37, 20);
            this.uxFileMenu.Text = "File";
            // 
            // uxSetImageWatchDirectoryMenuItem
            // 
            this.uxSetImageWatchDirectoryMenuItem.Name = "uxSetImageWatchDirectoryMenuItem";
            this.uxSetImageWatchDirectoryMenuItem.Size = new System.Drawing.Size(214, 22);
            this.uxSetImageWatchDirectoryMenuItem.Text = "Set Image Watch Directory";
            this.uxSetImageWatchDirectoryMenuItem.Click += new System.EventHandler(this.uxSetImageDirectory_Click);
            // 
            // uxMenu
            // 
            this.uxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxFileMenu});
            this.uxMenu.Location = new System.Drawing.Point(0, 0);
            this.uxMenu.Name = "uxMenu";
            this.uxMenu.Size = new System.Drawing.Size(834, 24);
            this.uxMenu.TabIndex = 2;
            this.uxMenu.Text = "menuStrip1";
            // 
            // QrCodeDetectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 561);
            this.Controls.Add(this.uxOutput);
            this.Controls.Add(this.uxStatusStrip);
            this.Controls.Add(this.uxControlsGroup);
            this.Controls.Add(this.uxHorizontalSplitContainer);
            this.Controls.Add(this.uxMenu);
            this.MainMenuStrip = this.uxMenu;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "QrCodeDetectorForm";
            this.Text = "QR Code Detector";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.QrCodeDetectorForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.uxFileSystemWatcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxImageDisplay)).EndInit();
            this.uxHorizontalSplitContainer.Panel1.ResumeLayout(false);
            this.uxHorizontalSplitContainer.Panel1.PerformLayout();
            this.uxHorizontalSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uxHorizontalSplitContainer)).EndInit();
            this.uxHorizontalSplitContainer.ResumeLayout(false);
            this.uxControlsGroup.ResumeLayout(false);
            this.uxControlsGroup.PerformLayout();
            this.uxAutoDetectControls.ResumeLayout(false);
            this.uxAutoDetectControls.PerformLayout();
            this.uxStatusStrip.ResumeLayout(false);
            this.uxStatusStrip.PerformLayout();
            this.uxOutput.ResumeLayout(false);
            this.uxOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxImageHolderBindingSource)).EndInit();
            this.uxMenu.ResumeLayout(false);
            this.uxMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog uxImageDirectoryBrowser;
        private System.Windows.Forms.BindingSource uxImageHolderBindingSource;
        private System.IO.FileSystemWatcher uxFileSystemWatcher;
        private System.Windows.Forms.NumericUpDown uxValue;
        private System.Windows.Forms.Button uxEnhance;
        private System.Windows.Forms.Timer uxTimer;
        private System.Windows.Forms.StatusStrip uxStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel uxBytesUsed;
        private System.Windows.Forms.GroupBox uxControlsGroup;
        private System.Windows.Forms.SplitContainer uxHorizontalSplitContainer;
        private System.Windows.Forms.PictureBox uxImageDisplay;
        private System.Windows.Forms.DataGridView uxDataGrid;
        private System.Windows.Forms.Label uxValueLabel;
        private System.Windows.Forms.Label uxQrDataLabel;
        private System.Windows.Forms.GroupBox uxOutput;
        private System.Windows.Forms.CheckBox uxAutoAddImages;
        private System.Windows.Forms.Button uxAddImages;
        private System.Windows.Forms.Button uxSetImageWatchDirectory;
        private System.Windows.Forms.GroupBox uxAutoDetectControls;
        private System.Windows.Forms.RadioButton uxAutoDetectOnAdd;
        private System.Windows.Forms.RadioButton uxAutoDetectOnView;
        private System.Windows.Forms.RadioButton uxAutoDetectDisabled;
        private System.Windows.Forms.ToolStripStatusLabel uxStatusLabel;
        private System.Windows.Forms.OpenFileDialog uxOpenFileDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn Filename;
        private System.Windows.Forms.DataGridViewTextBoxColumn QrCodeData;
        private System.Windows.Forms.RichTextBox uxQrCodeData;
        private System.Windows.Forms.CheckBox uxShowEnchancedImage;
        private System.Windows.Forms.CheckBox uxShowQuadImages;
        private System.Windows.Forms.MenuStrip uxMenu;
        private System.Windows.Forms.ToolStripMenuItem uxFileMenu;
        private System.Windows.Forms.ToolStripMenuItem uxSetImageWatchDirectoryMenuItem;
    }
}

