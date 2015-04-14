namespace QrCodeDetector
{
    partial class ImageForm
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
            this.uxImage = new System.Windows.Forms.PictureBox();
            this.uxSave = new System.Windows.Forms.Button();
            this.uxSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.uxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // uxImage
            // 
            this.uxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxImage.BackColor = System.Drawing.Color.White;
            this.uxImage.Location = new System.Drawing.Point(0, 0);
            this.uxImage.Name = "uxImage";
            this.uxImage.Size = new System.Drawing.Size(284, 205);
            this.uxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.uxImage.TabIndex = 0;
            this.uxImage.TabStop = false;
            // 
            // uxSave
            // 
            this.uxSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uxSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxSave.Location = new System.Drawing.Point(0, 211);
            this.uxSave.Name = "uxSave";
            this.uxSave.Size = new System.Drawing.Size(284, 34);
            this.uxSave.TabIndex = 1;
            this.uxSave.Text = "Save";
            this.uxSave.UseVisualStyleBackColor = true;
            this.uxSave.Click += new System.EventHandler(this.uxSave_Click);
            // 
            // uxSaveFileDialog
            // 
            this.uxSaveFileDialog.DefaultExt = "png";
            this.uxSaveFileDialog.Filter = "PNG Image *.png|*.png";
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 245);
            this.Controls.Add(this.uxSave);
            this.Controls.Add(this.uxImage);
            this.Name = "ImageForm";
            this.Text = "Display";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ImageForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.uxImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox uxImage;
        private System.Windows.Forms.Button uxSave;
        private System.Windows.Forms.SaveFileDialog uxSaveFileDialog;
    }
}