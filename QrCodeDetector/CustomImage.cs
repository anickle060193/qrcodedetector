using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeDetector
{
    public class CustomImage
    {
        private static readonly int MIN_COLOR_INT = 0;
        private static readonly int MAX_COLOR_INT = 255;

        private readonly float[ , ] _image;
        private float _min = float.MaxValue;
        private float _max = float.MinValue;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public float this[ int x, int y ]
        {
            get
            {
                return _image[ x, y ];
            }

            set
            {
                if( value < _min )
                {
                    _min = value;
                }
                if( value > _max )
                {
                    _max = value;
                }
                _image[ x, y ] = value;
            }
        }

        public CustomImage( int width, int height )
        {
            Width = width;
            Height = height;
            _image = new float[ Width, Height ];
        }

        public CustomImage( CustomImage image ) : this( image.Width, image.Height )
        {

        }

        public CustomImage( Bitmap bitmap ) : this( bitmap.Width, bitmap.Height )
        {
            for( int x = 0; x < Width; x++ )
            {
                for( int y = 0; y < Height; y++ )
                {
                    this[ x, y ] = ColorToFloat( bitmap.GetPixel( x, y ) );
                }
            }
        }

        private static float ColorToFloat( Color c )
        {
            return ( (float)c.R / MAX_COLOR_INT + (float)c.R / MAX_COLOR_INT + (float)c.R / MAX_COLOR_INT ) / 3.0f;
        }

        private static Color FloatToColor( float f )
        {
            return Color.FromArgb( (int)( MAX_COLOR_INT * f ), (int)( MAX_COLOR_INT * f ), (int)( MAX_COLOR_INT * f ) );
        }

        private static void CheckImages( CustomImage i1, CustomImage i2 )
        {
            if( i1 == null || i2 == null )
            {
                throw new ArgumentException( "Custom image cannot be null." );
            }
            if( i1.Width != i2.Width && i1.Height != i2.Height )
            {
                throw new ArgumentException( "CustomImages must be of the same size." );
            }
        }

        public static CustomImage operator +( CustomImage i1, CustomImage i2 )
        {
            CheckImages( i1, i2 );
            CustomImage i1s = i1.ScaleImage();
            CustomImage i2s = i2.ScaleImage();

            CustomImage image = new CustomImage( i1 );
            for( int x = 0; x < image.Width; x++ )
            {
                for( int y = 0; y < image.Height; y++ )
                {
                    image[ x, y ] = i1s[ x, y ] + i2s[ x, y ];
                }
            }
            return image;
        }

        public static CustomImage operator -( CustomImage i1, CustomImage i2 )
        {
            CheckImages( i1, i2 );
            CustomImage i1s = i1.ScaleImage();
            CustomImage i2s = i2.ScaleImage();

            CustomImage image = new CustomImage( i1 );
            for( int x = 0; x < image.Width; x++ )
            {
                for( int y = 0; y < image.Height; y++ )
                {
                    image[ x, y ] = i1s[ x, y ] - i2s[ x, y ];
                }
            }
            return image;
        }

        public CustomImage BlurImage( int blurSize )
        {
            CustomImage blurred = new CustomImage( this );

            for( int xx = 0; xx < Width; xx++ )
            {
                for( int yy = 0; yy < Height; yy++ )
                {
                    float avg = 0;
                    int blurPixelCount = 0;
                    for( int x = xx; ( x < xx + blurSize && x < Width ); x++ )
                    {
                        for( int y = yy; ( y < yy + blurSize && y < Height ); y++ )
                        {
                            avg += this[ x, y ];
                            blurPixelCount++;
                        }
                    }
                    avg /= blurPixelCount;
                    for( int x = xx; x < xx + blurSize && x < Width; x++ )
                    {
                        for( int y = yy; y < yy + blurSize && y < Height; y++ )
                        {
                            blurred[ x, y ] = avg;
                        }
                    }
                }
            }

            return blurred;
        }

        public CustomImage ApplyThreshold( float threshold, float lower, float higher )
        {
            CustomImage image = new CustomImage( this );
            for( int x = 0; x < image.Width; x++ )
            {
                for( int y = 0; y < image.Height; y++ )
                {
                    if( this[ x, y ] < threshold )
                    {
                        image[ x, y ] = lower;
                    }
                    else
                    {
                        image[ x, y ] = higher;
                    }
                }
            }
            return image;
        }

        public Bitmap ToBitmap()
        {
            Bitmap bitmap = new Bitmap( Width, Height );
            for( int x = 0; x < Width; x++ )
            {
                for( int y = 0; y < Height; y++ )
                {
                    bitmap.SetPixel( x, y, FloatToColor( ApplyScale( this[ x, y ] ) ) );
                }
            }
            return bitmap;
        }

        private float ApplyScale( float f )
        {
            float scaled = ( f - _min ) / ( _max - _min );
            if( scaled > 1.0f )
            {
                return 1.0f;
            }
            else if( scaled < 0.0f )
            {
                return 0.0f;
            }
            else
            {
                return scaled;
            }
        }

        public CustomImage ScaleImage()
        {
            CustomImage image = new CustomImage( this );
            for( int x = 0; x < Width; x++ )
            {
                for( int y = 0; y < Height; y++ )
                {
                    image[ x, y ] = ApplyScale( this[ x, y ] );
                }
            }
            return image;
        }
    }
}
