using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeDetector
{
    public static class PointUtilities
    {
        public static System.Drawing.Point Floor( this System.Drawing.PointF point )
        {
            return new System.Drawing.Point( (int)point.X, (int)point.Y );
        }

        public static System.Drawing.Point[] ConvertToImagePoints( System.Windows.Forms.PictureBox pic, System.Drawing.Point[] controlPoints )
        {
            return controlPoints.ToList().ConvertAll( p => ConvertToImagePoint( pic, p ) ).ToArray();
        }

        public static System.Drawing.Point ConvertToImagePoint( System.Windows.Forms.PictureBox pic, System.Drawing.Point controlPoint )
        {
            int boxWidth = pic.ClientSize.Width;
            int boxHeight = pic.ClientSize.Height;
            int imageWidth = pic.Image.Width;
            int imageHeight = pic.Image.Height;

            int scaledX = controlPoint.X;
            int scaledY = controlPoint.Y;
            switch( pic.SizeMode )
            {
                case System.Windows.Forms.PictureBoxSizeMode.AutoSize:
                case System.Windows.Forms.PictureBoxSizeMode.Normal:
                    // These are okay. Leave them alone.
                    break;

                case System.Windows.Forms.PictureBoxSizeMode.CenterImage:
                    scaledX = controlPoint.X - ( boxWidth - imageWidth ) / 2;
                    scaledY = controlPoint.Y - ( boxHeight - imageHeight ) / 2;
                    break;

                case System.Windows.Forms.PictureBoxSizeMode.StretchImage:
                    scaledX = (int)( imageWidth * controlPoint.X / (float)boxWidth );
                    scaledY = (int)( imageHeight * controlPoint.Y / (float)boxHeight );
                    break;

                case System.Windows.Forms.PictureBoxSizeMode.Zoom:
                    float pic_aspect = boxWidth / (float)boxHeight;
                    float img_aspect = imageWidth / (float)imageHeight;
                    if( pic_aspect > img_aspect )
                    {
                        // The System.Windows.Forms.PictureBox is wider/shorter than the image.
                        scaledY = (int)( imageHeight * controlPoint.Y / (float)boxHeight );

                        // The image fills the height of the System.Windows.Forms.PictureBox.
                        // Get its width.
                        float scaled_width = imageWidth * boxHeight / imageHeight;
                        float dx = ( boxWidth - scaled_width ) / 2;
                        scaledX = (int)( ( controlPoint.X - dx ) * imageHeight / (float)boxHeight );
                    }
                    else
                    {
                        // The System.Windows.Forms.PictureBox is taller/thinner than the image.
                        scaledX = (int)( imageWidth * controlPoint.X / (float)boxWidth );

                        // The image fills the height of the System.Windows.Forms.PictureBox.
                        // Get its height.
                        float scaled_height = imageHeight * boxWidth / imageWidth;
                        float dy = ( boxHeight - scaled_height ) / 2;
                        scaledY = (int)( ( controlPoint.Y - dy ) * imageWidth / boxWidth );
                    }
                    break;
            }
            return new System.Drawing.Point( scaledX, scaledY );
        }

        public static System.Drawing.Point[] ConvertToControlPoints( System.Windows.Forms.PictureBox pic, System.Drawing.Point[] imagePoints )
        {
            return imagePoints.ToList().ConvertAll( p => ConvertToControlPoint( pic, p ) ).ToArray();
        }

        public static System.Drawing.Point ConvertToControlPoint( System.Windows.Forms.PictureBox box, System.Drawing.Point imagePoint )
        {
            int boxWidth = box.ClientSize.Width;
            int boxHeight = box.ClientSize.Height;
            int imageWidth = box.Image.Width;
            int imageHeight = box.Image.Height;

            int scaledX = imagePoint.X;
            int scaledY = imagePoint.Y;
            switch( box.SizeMode )
            {
                case System.Windows.Forms.PictureBoxSizeMode.AutoSize:
                case System.Windows.Forms.PictureBoxSizeMode.Normal:
                    // These are okay. Leave them alone.
                    break;

                case System.Windows.Forms.PictureBoxSizeMode.CenterImage:
                    scaledX = imagePoint.X + ( boxWidth - imageWidth ) / 2;
                    scaledY = imagePoint.Y + ( boxHeight - imageHeight ) / 2;
                    break;

                case System.Windows.Forms.PictureBoxSizeMode.StretchImage:
                    scaledX = (int)( imagePoint.X * boxWidth / (float)imageWidth );
                    scaledY = (int)( imagePoint.Y * boxHeight / (float)imageHeight );
                    break;

                case System.Windows.Forms.PictureBoxSizeMode.Zoom:
                    float pic_aspect = boxWidth / (float)boxHeight;
                    float img_aspect = imageWidth / (float)imageHeight;
                    if( pic_aspect > img_aspect )
                    {
                        // The System.Windows.Forms.PictureBox is wider/shorter than the image.
                        scaledY = (int)( imagePoint.Y * boxHeight / (float)imageHeight );

                        // The image fills the height of the System.Windows.Forms.PictureBox.
                        // Get its width.
                        float scaled_width = imageWidth * boxHeight / (float)imageHeight;
                        float dx = ( boxWidth - scaled_width ) / 2;
                        scaledX = (int)( imagePoint.X * boxHeight / (float)imageHeight + dx );
                    }
                    else
                    {
                        // The System.Windows.Forms.PictureBox is taller/thinner than the image.
                        scaledX = (int)( imagePoint.X * boxWidth / (float)imageWidth );

                        // The image fills the height of the System.Windows.Forms.PictureBox.
                        // Get its height.
                        float scaled_height = imageHeight * boxWidth / (float)imageWidth;
                        float dy = ( boxHeight - scaled_height ) / 2;
                        scaledY = (int)( imagePoint.Y * boxWidth / (float)imageWidth + dy );
                    }
                    break;
            }
            return new System.Drawing.Point( scaledX, scaledY );
        }

        public static Point[] ConvertToPoints( this AForge.IntPoint[] points )
        {
            return points.ToList().ConvertAll( p => new Point( p.X, p.Y ) ).ToArray();
        }

        public static AForge.IntPoint[] ConvertToIntPoints( this Point[] points )
        {
            return points.ToList().ConvertAll( p => new AForge.IntPoint( p.X, p.Y ) ).ToArray();
        }
    }
}
