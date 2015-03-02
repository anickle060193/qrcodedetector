using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeDetector
{
    class ImageHolder : IDisposable
    {
        private Bitmap _bitmap;

        public String FullFilename { get; private set; }

        public Bitmap Image
        {
            get
            {
                if( _bitmap == null )
                {
                    LoadImage();
                }
                return _bitmap;
            }
        }

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

        private void LoadImage()
        {
            if( _bitmap == null )
            {
                if( File.Exists( FullFilename ) )
                {
                    using( FileStream input = File.Open( FullFilename, FileMode.Open, FileAccess.Read ) )
                    {
                        if( _bitmap != null )
                        {
                            _bitmap.Dispose();
                        }
                        _bitmap = new Bitmap( input );
                    }
                }
            }
        }

        public void Dispose()
        {
            if( _bitmap != null )
            {
                _bitmap.Dispose();
                _bitmap = null;
            }
        }
    }
}
