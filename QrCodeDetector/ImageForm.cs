using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QrCodeDetector
{
    public partial class ImageForm : Form
    {
        public ImageForm( string title, Bitmap image )
        {
            InitializeComponent();
            this.Text = title;
            uxImage.Image = image;
        }

        private void ImageForm_FormClosed( object sender, FormClosedEventArgs e )
        {
            if( uxImage.Image != null )
            {
                uxImage.Image.Dispose();
            }
        }

        private void uxSave_Click( object sender, EventArgs e )
        {
            if( uxSaveFileDialog.ShowDialog() == DialogResult.OK )
            {
                try
                {
                    uxImage.Image.Save( uxSaveFileDialog.FileName, ImageFormat.Png );
                }
                catch( Exception ex )
                {
                    MessageBox.Show( "The following error occured while saving the image:\n" + ex.ToString() );
                    Log.Write( "An error occured while saving the quadrilateral image.", ex );
                }
            }
        }
    }
}
