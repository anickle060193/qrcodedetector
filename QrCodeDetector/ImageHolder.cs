using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeDetector
{
    class ImageHolder
    {
        public String FullFilename { get; private set; }

        public string Filename
        {
            get
            {
                return Path.GetFileName( FullFilename );
            }
        }

        public ImageHolder( string filename )
        {
            FullFilename = filename;
        }

        public Bitmap LoadImage()
        {
            if( File.Exists( FullFilename ) )
            {
                return (Bitmap)Bitmap.FromFile( FullFilename );
            }
            else
            {
                throw new ArgumentException( "Image file does not exist: " + FullFilename );
            }
        }
    }
}
