using AForge;
using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeDetector
{
    public static class ImageUtilities
    {
        // Calculate average brightness difference between pixels outside and
        // inside of the object bounded by specified left and right edge
        public static float CalculateAvgEdgeBrightnessDiff( List<IntPoint> leftEdgePoints, List<IntPoint> rightEdgePoints, UnmanagedImage image )
        {
            const int stepSize = 3;

            // create list of points, which are a bit on the left/right from edges
            List<IntPoint> leftEdgePoints1 = new List<IntPoint>();
            List<IntPoint> leftEdgePoints2 = new List<IntPoint>();
            List<IntPoint> rightEdgePoints1 = new List<IntPoint>();
            List<IntPoint> rightEdgePoints2 = new List<IntPoint>();

            int tx1, tx2, ty;
            int widthM1 = image.Width - 1;

            for( int k = 0; k < leftEdgePoints.Count; k++ )
            {
                tx1 = leftEdgePoints[ k ].X - stepSize;
                tx2 = leftEdgePoints[ k ].X + stepSize;
                ty = leftEdgePoints[ k ].Y;

                leftEdgePoints1.Add( new IntPoint( ( tx1 < 0 ) ? 0 : tx1, ty ) );
                leftEdgePoints2.Add( new IntPoint( ( tx2 > widthM1 ) ? widthM1 : tx2, ty ) );

                tx1 = rightEdgePoints[ k ].X - stepSize;
                tx2 = rightEdgePoints[ k ].X + stepSize;
                ty = rightEdgePoints[ k ].Y;

                rightEdgePoints1.Add( new IntPoint( ( tx1 < 0 ) ? 0 : tx1, ty ) );
                rightEdgePoints2.Add( new IntPoint( ( tx2 > widthM1 ) ? widthM1 : tx2, ty ) );
            }

            // collect pixel values from specified points
            byte[] leftValues1 = image.Collect8bppPixelValues( leftEdgePoints1 );
            byte[] leftValues2 = image.Collect8bppPixelValues( leftEdgePoints2 );
            byte[] rightValues1 = image.Collect8bppPixelValues( rightEdgePoints1 );
            byte[] rightValues2 = image.Collect8bppPixelValues( rightEdgePoints2 );

            // calculate average difference between pixel values from outside of
            // the shape and from inside
            float diff = 0;
            int pixelCount = 0;

            for( int k = 0; k < leftEdgePoints.Count; k++ )
            {
                if( rightEdgePoints[ k ].X - leftEdgePoints[ k ].X > stepSize * 2 )
                {
                    diff += ( leftValues1[ k ] - leftValues2[ k ] );
                    diff += ( rightValues2[ k ] - rightValues1[ k ] );
                    pixelCount += 2;
                }
            }
            return diff / pixelCount;
        }

        public static Bitmap AdjustConstrast( int contrast, Bitmap bitmap )
        {
            Bitmap dup = new Bitmap( bitmap );
            BitmapData bmpData = dup.LockBits( new Rectangle( 0, 0, dup.Width, dup.Height ), System.Drawing.Imaging.ImageLockMode.ReadWrite, dup.PixelFormat );

            int numBytes = bmpData.Stride * dup.Height;
            byte[] rgbValues = new byte[ numBytes ];

            Marshal.Copy( bmpData.Scan0, rgbValues, 0, numBytes );
            
            byte[] contrast_lookup = new byte[ 256 ];
            double newValue = 0;
            double c = ( 100.0 + contrast ) / 100.0;

            c *= c;

            for( int i = 0; i < 256; i++ )
            {
                newValue = (double)i;
                newValue /= 255.0;
                newValue -= 0.5;
                newValue *= c;
                newValue += 0.5;
                newValue *= 255;

                if( newValue < 0 )
                {
                    newValue = 0;
                }
                if( newValue > 255 )
                {
                    newValue = 255;
                }
                contrast_lookup[ i ] = (byte)newValue;
            }

            for( int i = 0; i < rgbValues.Length; i++ )
            {
                rgbValues[ i ] = contrast_lookup[ rgbValues[ i ] ];
            }

            Marshal.Copy( rgbValues, 0, bmpData.Scan0, numBytes );
            dup.UnlockBits( bmpData );
            return dup;
        }

        public static Bitmap SubImage( Bitmap original, Rectangle cropRect )
        {
            Bitmap target = new Bitmap( cropRect.Width, cropRect.Height );
            using( Graphics g = Graphics.FromImage( target ) )
            {
                g.DrawImage( original, new Rectangle( 0, 0, target.Width, target.Height ), cropRect, GraphicsUnit.Pixel );
            }
            return target;
        }
    }
}
