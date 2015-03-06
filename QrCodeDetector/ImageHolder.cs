using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AForge;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;
using AForge.Imaging;
using System.Drawing.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;

namespace QrCodeDetector
{
    public class ImageHolder
    {
        private static readonly int EDGE_DETECTION_THRESHOLD = 40;
        private static readonly Dictionary<DecodeHintType, Object> HINTS = new Dictionary<DecodeHintType, object>();
        static ImageHolder()
        {
            HINTS.Add( DecodeHintType.TRY_HARDER, true );
            HINTS.Add( DecodeHintType.POSSIBLE_FORMATS, BarcodeFormat.QR_CODE );
        }

        public String FullFilename { get; private set; }

        public string Filename
        {
            get
            {
                return Path.GetFileName( FullFilename );
            }
        }

        public bool HasRunEdgeDetection { get; private set; }

        public List<List<IntPoint>> Corners { get; private set; }

        public bool HasRunQrCodeDetection { get; private set; }

        public ResultPoint[] QrCodePoints { get; private set; }

        public String QrCodeData { get; private set; }

        public ImageHolder( string filename )
        {
            FullFilename = filename;
            Corners = new List<List<IntPoint>>();
            try
            {
                using( Bitmap bitmap = LoadImage() )
                {

                }
            }
            catch( Exception ex )
            {
                throw new BadImageFormatException( "The specified file is not an image: " + filename, ex );
            }
        }

        public Bitmap LoadImage()
        {
            if( File.Exists( FullFilename ) )
            {
                using( Stream input = File.OpenRead(FullFilename ) )
                {
                    return new Bitmap( input );
                }
            }
            else
            {
                throw new ArgumentException( "Image file does not exist: " + FullFilename );
            }
        }

        public void DetectQrCode()
        {
            if( HasRunQrCodeDetection )
            {
                return;
            }
            using( Bitmap bitmap = this.LoadImage() )
            {
                LuminanceSource source = new BitmapLuminanceSource( bitmap );
                Binarizer binarizer = new HybridBinarizer( source );
                BinaryBitmap binBitmap = new BinaryBitmap( binarizer );
                QRCodeReader reader = new QRCodeReader();
                Result result = reader.decode( binBitmap, HINTS );

                if( result != null )
                {
                    QrCodePoints = result.ResultPoints;
                    QrCodeData = result.Text;
                }
                HasRunQrCodeDetection = true;
            }
        }

        public void RunEdgeDetection( int value )
        {
            if( HasRunEdgeDetection )
            {
                return;
            }
            using( Bitmap newBitmap = LoadImage() )
            {
                Rectangle rect = new Rectangle( 0, 0, newBitmap.Width, newBitmap.Height );
                using( UnmanagedImage image = new UnmanagedImage( newBitmap.LockBits( rect, ImageLockMode.ReadWrite, newBitmap.PixelFormat ) ) )
                {
                    using( UnmanagedImage grayImage = UnmanagedImage.Create( image.Width, image.Height, PixelFormat.Format8bppIndexed ) )
                    {
                        Grayscale.CommonAlgorithms.BT709.Apply( image, grayImage );

                        DifferenceEdgeDetector edgeDetector = new DifferenceEdgeDetector();
                        using( UnmanagedImage edgesImage = edgeDetector.Apply( grayImage ) )
                        {
                            Threshold thresholdFilter = new Threshold( value );
                            thresholdFilter.ApplyInPlace( edgesImage );

                            BlobCounter blobCounter = new BlobCounter();
                            blobCounter.MinHeight = 10;
                            blobCounter.MinHeight = 10;
                            blobCounter.FilterBlobs = true;
                            blobCounter.ObjectsOrder = ObjectsOrder.Size;

                            blobCounter.ProcessImage( edgesImage );
                            Blob[] blobs = blobCounter.GetObjectsInformation();

                            Corners.Clear();
                            foreach( Blob blob in blobs )
                            {
                                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints( blob );
                                List<IntPoint> corners = null;

                                SimpleShapeChecker shapeChecker = new SimpleShapeChecker();

                                if( shapeChecker.IsQuadrilateral( edgePoints, out corners ) )
                                {
                                    List<IntPoint> leftEdgePoints, rightEdgePoints;
                                    blobCounter.GetBlobsLeftAndRightEdges( blob, out leftEdgePoints, out rightEdgePoints );

                                    float diff = ImageManipulation.CalculateAvgEdgeBrightnessDiff( leftEdgePoints, rightEdgePoints, grayImage );

                                    if( diff > 20 )
                                    {
                                        Corners.Add( corners );
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
