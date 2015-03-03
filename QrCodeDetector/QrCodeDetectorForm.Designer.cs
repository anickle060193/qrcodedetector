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
            this.uxDataGrid = new System.Windows.Forms.DataGridView();
            this.uxImageDisplay = new System.Windows.Forms.PictureBox();
            this.uxMenu = new System.Windows.Forms.MenuStrip();
            this.uxFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.uxSetImageDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.uxImageDirectoryBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.uxFileSystemWatcher = new System.IO.FileSystemWatcher();
            this.uxQrCodeOutput = new System.Windows.Forms.TextBox();
            this.uxEnhance = new System.Windows.Forms.Button();
            this.uxValue = new System.Windows.Forms.NumericUpDown();
            this.uxPicturePanel = new System.Windows.Forms.Panel();
            this.filenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uxImageHolderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uxDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxImageDisplay)).BeginInit();
            this.uxMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxFileSystemWatcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxValue)).BeginInit();
            this.uxPicturePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxImageHolderBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // uxDataGrid
            // 
            this.uxDataGrid.AllowUserToAddRows = false;
            this.uxDataGrid.AllowUserToDeleteRows = false;
            this.uxDataGrid.AllowUserToOrderColumns = true;
            this.uxDataGrid.AllowUserToResizeRows = false;
            this.uxDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.uxDataGrid.AutoGenerateColumns = false;
            this.uxDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.uxDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uxDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.filenameDataGridViewTextBoxColumn});
            this.uxDataGrid.DataSource = this.uxImageHolderBindingSource;
            this.uxDataGrid.Location = new System.Drawing.Point(12, 83);
            this.uxDataGrid.MultiSelect = false;
            this.uxDataGrid.Name = "uxDataGrid";
            this.uxDataGrid.ReadOnly = true;
            this.uxDataGrid.RowHeadersVisible = false;
            this.uxDataGrid.Size = new System.Drawing.Size(240, 266);
            this.uxDataGrid.TabIndex = 0;
            this.uxDataGrid.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.uxDataGrid_CellStateChanged);
            this.uxDataGrid.CurrentCellChanged += new System.EventHandler(this.uxDataGrid_CurrentCellChanged);
            this.uxDataGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.uxDataGrid_KeyUp);
            // 
            // uxImageDisplay
            // 
            this.uxImageDisplay.Location = new System.Drawing.Point(3, 3);
            this.uxImageDisplay.Name = "uxImageDisplay";
            this.uxImageDisplay.Size = new System.Drawing.Size(105, 123);
            this.uxImageDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.uxImageDisplay.TabIndex = 1;
            this.uxImageDisplay.TabStop = false;
            this.uxImageDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.uxImageDisplay_Paint);
            this.uxImageDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.uxImageDisplay_MouseDown);
            this.uxImageDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.uxImageDisplay_MouseMove);
            this.uxImageDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.uxImageDisplay_MouseUp);
            this.uxImageDisplay.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.uxImageDisplay_PreviewKeyDown);
            // 
            // uxMenu
            // 
            this.uxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxFileMenu});
            this.uxMenu.Location = new System.Drawing.Point(0, 0);
            this.uxMenu.Name = "uxMenu";
            this.uxMenu.Size = new System.Drawing.Size(584, 24);
            this.uxMenu.TabIndex = 2;
            this.uxMenu.Text = "menuStrip1";
            // 
            // uxFileMenu
            // 
            this.uxFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxSetImageDirectory});
            this.uxFileMenu.Name = "uxFileMenu";
            this.uxFileMenu.Size = new System.Drawing.Size(37, 20);
            this.uxFileMenu.Text = "File";
            // 
            // uxSetImageDirectory
            // 
            this.uxSetImageDirectory.Name = "uxSetImageDirectory";
            this.uxSetImageDirectory.Size = new System.Drawing.Size(177, 22);
            this.uxSetImageDirectory.Text = "Set Image Directory";
            this.uxSetImageDirectory.Click += new System.EventHandler(this.uxSetImageDirectory_Click);
            // 
            // uxFileSystemWatcher
            // 
            this.uxFileSystemWatcher.EnableRaisingEvents = true;
            this.uxFileSystemWatcher.Filter = "*.png";
            this.uxFileSystemWatcher.IncludeSubdirectories = true;
            this.uxFileSystemWatcher.SynchronizingObject = this;
            // 
            // uxQrCodeOutput
            // 
            this.uxQrCodeOutput.Location = new System.Drawing.Point(12, 27);
            this.uxQrCodeOutput.Name = "uxQrCodeOutput";
            this.uxQrCodeOutput.ReadOnly = true;
            this.uxQrCodeOutput.Size = new System.Drawing.Size(240, 20);
            this.uxQrCodeOutput.TabIndex = 3;
            // 
            // uxEnhance
            // 
            this.uxEnhance.Location = new System.Drawing.Point(12, 53);
            this.uxEnhance.Name = "uxEnhance";
            this.uxEnhance.Size = new System.Drawing.Size(162, 23);
            this.uxEnhance.TabIndex = 4;
            this.uxEnhance.Text = "Enhance!";
            this.uxEnhance.UseVisualStyleBackColor = true;
            this.uxEnhance.Click += new System.EventHandler(this.uxEnhance_Click);
            // 
            // uxConstrast
            // 
            this.uxValue.Location = new System.Drawing.Point(180, 56);
            this.uxValue.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.uxValue.Name = "uxConstrast";
            this.uxValue.Size = new System.Drawing.Size(72, 20);
            this.uxValue.TabIndex = 5;
            // 
            // uxPicturePanel
            // 
            this.uxPicturePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxPicturePanel.AutoScroll = true;
            this.uxPicturePanel.Controls.Add(this.uxImageDisplay);
            this.uxPicturePanel.Location = new System.Drawing.Point(258, 27);
            this.uxPicturePanel.Name = "uxPicturePanel";
            this.uxPicturePanel.Size = new System.Drawing.Size(314, 322);
            this.uxPicturePanel.TabIndex = 6;
            // 
            // filenameDataGridViewTextBoxColumn
            // 
            this.filenameDataGridViewTextBoxColumn.DataPropertyName = "Filename";
            this.filenameDataGridViewTextBoxColumn.HeaderText = "Filename";
            this.filenameDataGridViewTextBoxColumn.Name = "filenameDataGridViewTextBoxColumn";
            this.filenameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // uxImageHolderBindingSource
            // 
            this.uxImageHolderBindingSource.DataSource = typeof(QrCodeDetector.ImageHolder);
            // 
            // QrCodeDetectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.uxPicturePanel);
            this.Controls.Add(this.uxValue);
            this.Controls.Add(this.uxEnhance);
            this.Controls.Add(this.uxQrCodeOutput);
            this.Controls.Add(this.uxDataGrid);
            this.Controls.Add(this.uxMenu);
            this.MainMenuStrip = this.uxMenu;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "QrCodeDetectorForm";
            this.Text = "QR Code Detector";
            ((System.ComponentModel.ISupportInitialize)(this.uxDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxImageDisplay)).EndInit();
            this.uxMenu.ResumeLayout(false);
            this.uxMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxFileSystemWatcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxValue)).EndInit();
            this.uxPicturePanel.ResumeLayout(false);
            this.uxPicturePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxImageHolderBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView uxDataGrid;
        private System.Windows.Forms.PictureBox uxImageDisplay;
        private System.Windows.Forms.MenuStrip uxMenu;
        private System.Windows.Forms.ToolStripMenuItem uxFileMenu;
        private System.Windows.Forms.ToolStripMenuItem uxSetImageDirectory;
        private System.Windows.Forms.FolderBrowserDialog uxImageDirectoryBrowser;
        private System.Windows.Forms.BindingSource uxImageHolderBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn filenameDataGridViewTextBoxColumn;
        private System.IO.FileSystemWatcher uxFileSystemWatcher;
        private System.Windows.Forms.TextBox uxQrCodeOutput;
        private System.Windows.Forms.NumericUpDown uxValue;
        private System.Windows.Forms.Button uxEnhance;
        private System.Windows.Forms.Panel uxPicturePanel;
    }
}

