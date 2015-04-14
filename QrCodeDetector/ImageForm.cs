using AForge.Imaging;
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
        private static void ShowImage( String title, Bitmap image, bool modal )
        {
            ImageForm form = new ImageForm();
            form.uxImage.Image = image;
            form.Text = title;
            if( modal )
            {
                form.ShowDialog();
            }
            else
            {
                form.Show();
            }
        }

        public static void ShowImage( string title, Bitmap image )
        {
            ShowImage( title, image, false );
        }

        public static void ShowImageDialog( String title, Bitmap image )
        {
            ShowImage( title, image, true );
        }

        public static void ShowImage( String title, UnmanagedImage image )
        {
            using( Bitmap bitmap = image.ToManagedImage( true ) )
            {
                ShowImage( title, bitmap );
            }
        }

        public static void ShowImageDialog( String title, UnmanagedImage image )
        {
            using( Bitmap bitmap = image.ToManagedImage( true ) )
            {
                ShowImageDialog( title, bitmap );
            }
        }

        public ImageForm()
        {
            InitializeComponent();
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
