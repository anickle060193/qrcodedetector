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
            this.uxRunEdgeDetection = new System.Windows.Forms.Button();
            this.uxThreshold = new System.Windows.Forms.NumericUpDown();
            this.uxDataGrid = new System.Windows.Forms.DataGridView();
            this.QrCodeData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uxHorizontalSplitContainer = new System.Windows.Forms.SplitContainer();
            this.uxImageDisplay = new System.Windows.Forms.PictureBox();
            this.uxControlsGroup = new System.Windows.Forms.GroupBox();
            this.uxEnhanceGroup = new System.Windows.Forms.GroupBox();
            this.uxSharpen = new System.Windows.Forms.Button();
            this.uxShowBlobImages = new System.Windows.Forms.CheckBox();
            this.uxMinBlobSizeLabel = new System.Windows.Forms.Label();
            this.uxShowEdgesImage = new System.Windows.Forms.CheckBox();
            this.uxThresholdLabel = new System.Windows.Forms.Label();
            this.uxMinBlobSize = new System.Windows.Forms.NumericUpDown();
            this.uxAutoDetectControls = new System.Windows.Forms.GroupBox();
            this.uxAutoDetectOnAdd = new System.Windows.Forms.RadioButton();
            this.uxAutoDetectOnView = new System.Windows.Forms.RadioButton();
            this.uxAutoDetectDisabled = new System.Windows.Forms.RadioButton();
            this.uxAutoAddImages = new System.Windows.Forms.CheckBox();
            this.uxAddImages = new System.Windows.Forms.Button();
            this.uxSetImageWatchDirectory = new System.Windows.Forms.Button();
            this.uxQrDataLabel = new System.Windows.Forms.Label();
            this.uxStatusStrip = new System.Windows.Forms.StatusStrip();
            this.uxStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.uxOutput = new System.Windows.Forms.GroupBox();
            this.uxQrCodeData = new System.Windows.Forms.RichTextBox();
            this.uxOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.uxFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.uxAddImageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxSetImageWatchDirectoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxMenu = new System.Windows.Forms.MenuStrip();
            this.uxOptionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.uxEnhanceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxApplyThreshold = new System.Windows.Forms.Button();
            this.uxShowGrayScaleImage = new System.Windows.Forms.CheckBox();
            this.Filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uxImageHolderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uxFileSystemWatcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxHorizontalSplitContainer)).BeginInit();
            this.uxHorizontalSplitContainer.Panel1.SuspendLayout();
            this.uxHorizontalSplitContainer.Panel2.SuspendLayout();
            this.uxHorizontalSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxImageDisplay)).BeginInit();
            this.uxControlsGroup.SuspendLayout();
            this.uxEnhanceGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxMinBlobSize)).BeginInit();
            this.uxAutoDetectControls.SuspendLayout();
            this.uxStatusStrip.SuspendLayout();
            this.uxOutput.SuspendLayout();
            this.uxMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxImageHolderBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // uxFileSystemWatcher
            // 
            this.uxFileSystemWatcher.EnableRaisingEvents = true;
            this.uxFileSystemWatcher.IncludeSubdirectories = true;
            this.uxFileSystemWatcher.SynchronizingObject = this;
            // 
            // uxRunEdgeDetection
            // 
            this.uxRunEdgeDetection.Location = new System.Drawing.Point(6, 117);
            this.uxRunEdgeDetection.Name = "uxRunEdgeDetection";
            this.uxRunEdgeDetection.Size = new System.Drawing.Size(206, 23);
            this.uxRunEdgeDetection.TabIndex = 4;
            this.uxRunEdgeDetection.Text = "Run Edge Detection";
            this.uxRunEdgeDetection.UseVisualStyleBackColor = true;
            this.uxRunEdgeDetection.Click += new System.EventHandler(this.uxEnhance_Click);
            // 
            // uxThreshold
            // 
            this.uxThreshold.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.uxThreshold.Location = new System.Drawing.Point(99, 19);
            this.uxThreshold.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.uxThreshold.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.uxThreshold.Name = "uxThreshold";
            this.uxThreshold.Size = new System.Drawing.Size(113, 20);
            this.uxThreshold.TabIndex = 5;
            this.uxThreshold.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
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
            this.uxDataGrid.Size = new System.Drawing.Size(574, 133);
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
            this.uxHorizontalSplitContainer.Size = new System.Drawing.Size(574, 472);
            this.uxHorizontalSplitContainer.SplitterDistance = 335;
            this.uxHorizontalSplitContainer.TabIndex = 8;
            // 
            // uxImageDisplay
            // 
            this.uxImageDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxImageDisplay.Location = new System.Drawing.Point(0, 0);
            this.uxImageDisplay.Name = "uxImageDisplay";
            this.uxImageDisplay.Size = new System.Drawing.Size(574, 335);
            this.uxImageDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.uxImageDisplay.TabIndex = 1;
            this.uxImageDisplay.TabStop = false;
            this.uxImageDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.uxImageDisplay_Paint);
            this.uxImageDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.uxImageDisplay_MouseDown);
            this.uxImageDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.uxImageDisplay_MouseMove);
            this.uxImageDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.uxImageDisplay_MouseUp);
            // 
            // uxControlsGroup
            // 
            this.uxControlsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.uxControlsGroup.Controls.Add(this.uxEnhanceGroup);
            this.uxControlsGroup.Controls.Add(this.uxAutoDetectControls);
            this.uxControlsGroup.Controls.Add(this.uxAutoAddImages);
            this.uxControlsGroup.Controls.Add(this.uxAddImages);
            this.uxControlsGroup.Controls.Add(this.uxSetImageWatchDirectory);
            this.uxControlsGroup.Location = new System.Drawing.Point(12, 96);
            this.uxControlsGroup.Name = "uxControlsGroup";
            this.uxControlsGroup.Size = new System.Drawing.Size(230, 403);
            this.uxControlsGroup.TabIndex = 9;
            this.uxControlsGroup.TabStop = false;
            this.uxControlsGroup.Text = "Controls";
            // 
            // uxEnhanceGroup
            // 
            this.uxEnhanceGroup.Controls.Add(this.uxShowGrayScaleImage);
            this.uxEnhanceGroup.Controls.Add(this.uxApplyThreshold);
            this.uxEnhanceGroup.Controls.Add(this.uxSharpen);
            this.uxEnhanceGroup.Controls.Add(this.uxThreshold);
            this.uxEnhanceGroup.Controls.Add(this.uxShowBlobImages);
            this.uxEnhanceGroup.Controls.Add(this.uxMinBlobSizeLabel);
            this.uxEnhanceGroup.Controls.Add(this.uxShowEdgesImage);
            this.uxEnhanceGroup.Controls.Add(this.uxThresholdLabel);
            this.uxEnhanceGroup.Controls.Add(this.uxRunEdgeDetection);
            this.uxEnhanceGroup.Controls.Add(this.uxMinBlobSize);
            this.uxEnhanceGroup.Location = new System.Drawing.Point(6, 154);
            this.uxEnhanceGroup.Name = "uxEnhanceGroup";
            this.uxEnhanceGroup.Size = new System.Drawing.Size(218, 233);
            this.uxEnhanceGroup.TabIndex = 16;
            this.uxEnhanceGroup.TabStop = false;
            this.uxEnhanceGroup.Text = "Enhance";
            // 
            // uxSharpen
            // 
            this.uxSharpen.Location = new System.Drawing.Point(6, 146);
            this.uxSharpen.Name = "uxSharpen";
            this.uxSharpen.Size = new System.Drawing.Size(206, 23);
            this.uxSharpen.TabIndex = 16;
            this.uxSharpen.Text = "Sharpen";
            this.uxSharpen.UseVisualStyleBackColor = true;
            this.uxSharpen.Click += new System.EventHandler(this.uxSharpen_Click);
            // 
            // uxShowBlobImages
            // 
            this.uxShowBlobImages.AutoSize = true;
            this.uxShowBlobImages.Location = new System.Drawing.Point(6, 94);
            this.uxShowBlobImages.Name = "uxShowBlobImages";
            this.uxShowBlobImages.Size = new System.Drawing.Size(114, 17);
            this.uxShowBlobImages.TabIndex = 13;
            this.uxShowBlobImages.Text = "Show Blob Images";
            this.uxShowBlobImages.UseVisualStyleBackColor = true;
            // 
            // uxMinBlobSizeLabel
            // 
            this.uxMinBlobSizeLabel.AutoSize = true;
            this.uxMinBlobSizeLabel.Location = new System.Drawing.Point(6, 47);
            this.uxMinBlobSizeLabel.Name = "uxMinBlobSizeLabel";
            this.uxMinBlobSizeLabel.Size = new System.Drawing.Size(74, 13);
            this.uxMinBlobSizeLabel.TabIndex = 15;
            this.uxMinBlobSizeLabel.Text = "Min Blob Size:";
            // 
            // uxShowEdgesImage
            // 
            this.uxShowEdgesImage.AutoSize = true;
            this.uxShowEdgesImage.Location = new System.Drawing.Point(6, 71);
            this.uxShowEdgesImage.Name = "uxShowEdgesImage";
            this.uxShowEdgesImage.Size = new System.Drawing.Size(118, 17);
            this.uxShowEdgesImage.TabIndex = 12;
            this.uxShowEdgesImage.Text = "Show Edges Image";
            this.uxShowEdgesImage.UseVisualStyleBackColor = true;
            // 
            // uxThresholdLabel
            // 
            this.uxThresholdLabel.AutoSize = true;
            this.uxThresholdLabel.Location = new System.Drawing.Point(6, 21);
            this.uxThresholdLabel.Name = "uxThresholdLabel";
            this.uxThresholdLabel.Size = new System.Drawing.Size(87, 13);
            this.uxThresholdLabel.TabIndex = 6;
            this.uxThresholdLabel.Text = "Threshold Value:";
            // 
            // uxMinBlobSize
            // 
            this.uxMinBlobSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.uxMinBlobSize.Location = new System.Drawing.Point(99, 45);
            this.uxMinBlobSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.uxMinBlobSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uxMinBlobSize.Name = "uxMinBlobSize";
            this.uxMinBlobSize.Size = new System.Drawing.Size(113, 20);
            this.uxMinBlobSize.TabIndex = 14;
            this.uxMinBlobSize.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
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
            this.uxStatusLabel});
            this.uxStatusStrip.Location = new System.Drawing.Point(0, 502);
            this.uxStatusStrip.Name = "uxStatusStrip";
            this.uxStatusStrip.ShowItemToolTips = true;
            this.uxStatusStrip.Size = new System.Drawing.Size(834, 22);
            this.uxStatusStrip.TabIndex = 10;
            // 
            // uxStatusLabel
            // 
            this.uxStatusLabel.Name = "uxStatusLabel";
            this.uxStatusLabel.Size = new System.Drawing.Size(819, 17);
            this.uxStatusLabel.Spring = true;
            this.uxStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uxStatusLabel.Click += new System.EventHandler(this.uxStatusLabel_Click);
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
            // uxFileMenu
            // 
            this.uxFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxAddImageMenuItem,
            this.uxSetImageWatchDirectoryMenuItem});
            this.uxFileMenu.Name = "uxFileMenu";
            this.uxFileMenu.Size = new System.Drawing.Size(37, 20);
            this.uxFileMenu.Text = "File";
            // 
            // uxAddImageMenuItem
            // 
            this.uxAddImageMenuItem.Name = "uxAddImageMenuItem";
            this.uxAddImageMenuItem.Size = new System.Drawing.Size(214, 22);
            this.uxAddImageMenuItem.Text = "Add Image(s)";
            this.uxAddImageMenuItem.Click += new System.EventHandler(this.uxAddImages_Click);
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
            this.uxFileMenu,
            this.uxOptionsMenu});
            this.uxMenu.Location = new System.Drawing.Point(0, 0);
            this.uxMenu.Name = "uxMenu";
            this.uxMenu.Size = new System.Drawing.Size(834, 24);
            this.uxMenu.TabIndex = 2;
            this.uxMenu.Text = "menuStrip1";
            // 
            // uxOptionsMenu
            // 
            this.uxOptionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxEnhanceMenuItem});
            this.uxOptionsMenu.Name = "uxOptionsMenu";
            this.uxOptionsMenu.Size = new System.Drawing.Size(61, 20);
            this.uxOptionsMenu.Text = "Options";
            // 
            // uxEnhanceMenuItem
            // 
            this.uxEnhanceMenuItem.Name = "uxEnhanceMenuItem";
            this.uxEnhanceMenuItem.Size = new System.Drawing.Size(122, 22);
            this.uxEnhanceMenuItem.Text = "Enhance!";
            this.uxEnhanceMenuItem.Click += new System.EventHandler(this.uxEnhance_Click);
            // 
            // uxApplyThreshold
            // 
            this.uxApplyThreshold.Location = new System.Drawing.Point(6, 198);
            this.uxApplyThreshold.Name = "uxApplyThreshold";
            this.uxApplyThreshold.Size = new System.Drawing.Size(206, 23);
            this.uxApplyThreshold.TabIndex = 17;
            this.uxApplyThreshold.Text = "Apply Threshold";
            this.uxApplyThreshold.UseVisualStyleBackColor = true;
            this.uxApplyThreshold.Click += new System.EventHandler(this.uxApplyThreshold_Click);
            // 
            // uxShowGrayScaleImage
            // 
            this.uxShowGrayScaleImage.AutoSize = true;
            this.uxShowGrayScaleImage.Location = new System.Drawing.Point(6, 175);
            this.uxShowGrayScaleImage.Name = "uxShowGrayScaleImage";
            this.uxShowGrayScaleImage.Size = new System.Drawing.Size(140, 17);
            this.uxShowGrayScaleImage.TabIndex = 18;
            this.uxShowGrayScaleImage.Text = "Show Gray Scale Image";
            this.uxShowGrayScaleImage.UseVisualStyleBackColor = true;
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
            // QrCodeDetectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 524);
            this.Controls.Add(this.uxOutput);
            this.Controls.Add(this.uxStatusStrip);
            this.Controls.Add(this.uxControlsGroup);
            this.Controls.Add(this.uxHorizontalSplitContainer);
            this.Controls.Add(this.uxMenu);
            this.MainMenuStrip = this.uxMenu;
            this.MinimumSize = new System.Drawing.Size(850, 500);
            this.Name = "QrCodeDetectorForm";
            this.Text = "QR Code Detector";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.QrCodeDetectorForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.uxFileSystemWatcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxDataGrid)).EndInit();
            this.uxHorizontalSplitContainer.Panel1.ResumeLayout(false);
            this.uxHorizontalSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uxHorizontalSplitContainer)).EndInit();
            this.uxHorizontalSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uxImageDisplay)).EndInit();
            this.uxControlsGroup.ResumeLayout(false);
            this.uxControlsGroup.PerformLayout();
            this.uxEnhanceGroup.ResumeLayout(false);
            this.uxEnhanceGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxMinBlobSize)).EndInit();
            this.uxAutoDetectControls.ResumeLayout(false);
            this.uxAutoDetectControls.PerformLayout();
            this.uxStatusStrip.ResumeLayout(false);
            this.uxStatusStrip.PerformLayout();
            this.uxOutput.ResumeLayout(false);
            this.uxOutput.PerformLayout();
            this.uxMenu.ResumeLayout(false);
            this.uxMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxImageHolderBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog uxImageDirectoryBrowser;
        private System.Windows.Forms.BindingSource uxImageHolderBindingSource;
        private System.IO.FileSystemWatcher uxFileSystemWatcher;
        private System.Windows.Forms.NumericUpDown uxThreshold;
        private System.Windows.Forms.Button uxRunEdgeDetection;
        private System.Windows.Forms.StatusStrip uxStatusStrip;
        private System.Windows.Forms.GroupBox uxControlsGroup;
        private System.Windows.Forms.SplitContainer uxHorizontalSplitContainer;
        private System.Windows.Forms.DataGridView uxDataGrid;
        private System.Windows.Forms.Label uxThresholdLabel;
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
        private System.Windows.Forms.CheckBox uxShowEdgesImage;
        private System.Windows.Forms.CheckBox uxShowBlobImages;
        private System.Windows.Forms.MenuStrip uxMenu;
        private System.Windows.Forms.ToolStripMenuItem uxFileMenu;
        private System.Windows.Forms.ToolStripMenuItem uxSetImageWatchDirectoryMenuItem;
        private System.Windows.Forms.Label uxMinBlobSizeLabel;
        private System.Windows.Forms.NumericUpDown uxMinBlobSize;
        private System.Windows.Forms.GroupBox uxEnhanceGroup;
        private System.Windows.Forms.ToolStripMenuItem uxAddImageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uxOptionsMenu;
        private System.Windows.Forms.ToolStripMenuItem uxEnhanceMenuItem;
        private System.Windows.Forms.Button uxSharpen;
        private System.Windows.Forms.PictureBox uxImageDisplay;
        private System.Windows.Forms.Button uxApplyThreshold;
        private System.Windows.Forms.CheckBox uxShowGrayScaleImage;
    }
}

