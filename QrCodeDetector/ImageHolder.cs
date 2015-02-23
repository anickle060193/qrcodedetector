using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeDetector
{
    /// <summary>
    /// A class containing a Image definition.
    /// </summary>
    class ImageHolder
    {
        /// <summary>
        /// The full file path to the image file.
        /// </summary>
        public String FullFilename { get; set; }

        /// <summary>
        /// The actual Image.
        /// </summary>
        public Bitmap Image { get; set; }

        /// <summary>
        /// The name of the image file.
        /// </summary>
        public string Filename
        {
            get
            {
                return Path.GetFileName( FullFilename );
            }
        }

        /// <summary>
        /// Constructs a new ImageHolder.
        /// </summary>
        /// <param name="filename">The filename of the image</param>
        public ImageHolder( string filename )
        {
            FullFilename = filename;
            LoadImage();
        }

        /// <summary>
        /// Loads and stores the image into a Bitmap.
        /// </summary>
        private void LoadImage()
        {
            if( File.Exists( FullFilename ) )
            {
                using( FileStream input = File.Open( FullFilename, FileMode.Open, FileAccess.Read ) )
                {
                    if( Image != null )
                    {
                        Image.Dispose();
                    }
                    Image = new Bitmap( input );
                }
            }
        }
    }
}
