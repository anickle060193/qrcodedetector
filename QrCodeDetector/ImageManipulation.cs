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
    public class ImageManipulation
    {
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
    }
}
