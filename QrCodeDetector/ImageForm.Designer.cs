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
            ((System.ComponentModel.ISupportInitialize)(this.uxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // uxImage
            // 
            this.uxImage.BackColor = System.Drawing.Color.White;
            this.uxImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxImage.Location = new System.Drawing.Point(0, 0);
            this.uxImage.Name = "uxImage";
            this.uxImage.Size = new System.Drawing.Size(284, 261);
            this.uxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.uxImage.TabIndex = 0;
            this.uxImage.TabStop = false;
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.uxImage);
            this.Name = "ImageForm";
            this.Text = "Display";
            ((System.ComponentModel.ISupportInitialize)(this.uxImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox uxImage;
    }
}