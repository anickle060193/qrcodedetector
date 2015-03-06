using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace QrCodeDetector
{
    public partial class QrCodeDetectorForm : Form
    {
        private static readonly int MIN_SUB_SIZE = 10;
        private static readonly String[] IMAGE_EXTENSIONS = new String[] { ".png", ".jpg", ".jpeg" };

        private static readonly Pen PEN = new Pen( Color.Blue, 2 );
        private static readonly SolidBrush SELECTION_BRUSH = new SolidBrush( Color.FromArgb( 175, Color.LimeGreen ) );
        private static readonly Pen SELECTION_PEN = new Pen( Color.LimeGreen );
        private static readonly Dictionary<DecodeHintType, Object> HINTS = new Dictionary<DecodeHintType, object>();
        static QrCodeDetectorForm()
        {
            HINTS.Add( DecodeHintType.TRY_HARDER, true );
            HINTS.Add( DecodeHintType.POSSIBLE_FORMATS, BarcodeFormat.QR_CODE );
        }

        private List<List<IntPoint>> _corners = new List<List<IntPoint>>();
        private string _imageDirectory;
        private ImageHolder _currentImageHolder;
        private Bitmap _currentImage;
        private System.Drawing.Point[] _qrPoints;
        private bool _tracking;
        private System.Drawing.Point _start;
        private System.Drawing.Point _current;

        public QrCodeDetectorForm()
        {
            InitializeComponent();

            uxFileSystemWatcher.EnableRaisingEvents = false;
            uxFileSystemWatcher.Filter = "*.png";
            uxFileSystemWatcher.Deleted += HandleDirectoryWatcherEvents;
            uxFileSystemWatcher.Created += HandleDirectoryWatcherEvents;

            SetWatchDirectory( Properties.Settings.Default.WatchDirectoryLocation );
        }

        private void uxSetImageDirectory_Click( object sender, EventArgs e )
        {
            if( uxImageDirectoryBrowser.ShowDialog() == DialogResult.OK )
            {
                SetWatchDirectory( uxImageDirectoryBrowser.SelectedPath );
            }
        }

        private void HandleDirectoryWatcherEvents( object sender, FileSystemEventArgs e )
        {
            if( IMAGE_EXTENSIONS.Contains( Path.GetExtension( e.FullPath ) ) )
            {
                switch( e.ChangeType )
                {
                    case WatcherChangeTypes.Created:
                        InvokeOnDataGrid( (Action)delegate()
                        {
                            uxImageHolderBindingSource.Add( new ImageHolder( e.FullPath ) );
                        } );
                        break;

                    case WatcherChangeTypes.Deleted:
                        int index = FindImageHolderIndex( e.FullPath );
                        if( index >= 0 )
                        {
                            InvokeOnDataGrid( (Action)delegate()
                            {
                                uxImageHolderBindingSource.RemoveAt( index );
                            } );
                        }
                        break;
                }
            }
        }

        private int FindImageHolderIndex( string filename )
        {
            for( int i = 0; i < uxImageHolderBindingSource.Count; i++ )
            {
                ImageHolder holder = (ImageHolder)uxImageHolderBindingSource.List[ i ];
                if( holder.FullFilename.Equals(filename ) )
                {
                    return i;
                }
            }
            return -1;
        }

        private void uxDataGrid_CurrentCellChanged( object sender, EventArgs e )
        {
            if( uxDataGrid.CurrentCell != null )
            {
                int index = uxDataGrid.CurrentCell.RowIndex;
                if( 0 <= index && index < uxImageHolderBindingSource.Count )
                {
                    _tracking = false;

                    SetCurrentImage( (ImageHolder)uxImageHolderBindingSource.List[ index ] );
                    DetectQrCode();
                }
            }
        }

        private void InvokeOnDataGrid( Action action )
        {
            if( uxDataGrid.InvokeRequired )
            {
                uxDataGrid.Invoke( action );
            }
            else
            {
                action();
            }
        }

        private void SetWatchDirectory( string directoryName )
        {
            if( String.IsNullOrEmpty( directoryName )
             || !Directory.Exists( directoryName ) )
            {
                return;
            }
            if( !String.Equals( _imageDirectory, directoryName ) )
            {
                _imageDirectory = directoryName;
                QrCodeDetector.Properties.Settings.Default.WatchDirectoryLocation = _imageDirectory;
                Properties.Settings.Default.Save();
            }

            uxFileSystemWatcher.Path = _imageDirectory;
            uxFileSystemWatcher.EnableRaisingEvents = true;

            uxImageHolderBindingSource.Clear();
            AddImages( new DirectoryInfo( _imageDirectory ) );
        }

        private void AddImages( DirectoryInfo dir )
        {
            IEnumerable<FileInfo> files = dir.GetFiles().Where( info => IMAGE_EXTENSIONS.Any( ext => info.Name.ToLower().EndsWith( ext ) ) );
            foreach( FileInfo filename in files )
            {
                try
                {
                    ImageHolder im = new ImageHolder( filename.FullName );
                    uxImageHolderBindingSource.Add( im );
                }
                catch { }
            }
            foreach( DirectoryInfo d in dir.GetDirectories() )
            {
                AddImages( d );
            }
        }

        private void SetCurrentImage( ImageHolder holder )
        {
            if( _currentImage != null )
            {
                _currentImage.Dispose();
            }
            _currentImageHolder = holder;
            _currentImage = holder.LoadImage();
            _corners.Clear();
            _qrPoints = null;
            _tracking = false;
            uxImageDisplay.Image = _currentImage;
        }

        void uxImageDisplay_Paint( object sender, PaintEventArgs e )
        {
            Graphics g = e.Graphics;
            if( _qrPoints != null )
            {
                g.DrawPolygon( PEN, _qrPoints );
            }
            if( _tracking
             && _start != System.Drawing.Point.Empty
             && _current != System.Drawing.Point.Empty )
            {
                int x = Math.Min( _start.X, _current.X );
                int y = Math.Min( _start.Y, _current.Y );
                int width = Math.Abs( _current.X - _start.X );
                int height = Math.Abs( _current.Y - _start.Y );
                g.FillRectangle( SELECTION_BRUSH, x, y, width, height );
                g.DrawRectangle( SELECTION_PEN, x, y, width, height );
            }

            foreach( List<IntPoint> cornerList in _corners )
            {
                System.Drawing.Point[] points = cornerList.ConvertAll( intPoint => new System.Drawing.Point( intPoint.X, intPoint.Y ) ).ToArray();
                g.DrawPolygon( Pens.Red, points );
            }
        }

        private void DetectQrCode()
        {
            if( _currentImage == null )
            {
                return;
            }
            try
            {
                LuminanceSource source = new BitmapLuminanceSource( _currentImage );
                Binarizer binarizer = new HybridBinarizer( source );
                BinaryBitmap binBitmap = new BinaryBitmap( binarizer );
                QRCodeReader reader = new QRCodeReader();
                Result result = reader.decode( binBitmap, HINTS );

                if( result != null )
                {
                    _qrPoints = result.ResultPoints.ToList().ConvertAll( point => new System.Drawing.Point( (int)point.X, (int)point.Y ) ).ToArray();
                    uxImageDisplay.Invalidate();

                    uxQrCodeOutput.Text = result.Text;
                }
                else
                {
                    uxQrCodeOutput.Text = "No QR codes detected.";
                    _qrPoints = null;
                }
            }
            catch( Exception ex )
            {
                _qrPoints = null;
                MessageBox.Show( "The following error occured:\n" + ex.ToString() );
            }
        }

        private void uxImageDisplay_MouseDown( object sender, MouseEventArgs e )
        {
            _tracking = _currentImage != null;
            _start = new System.Drawing.Point( e.X, e.Y );
        }

        private void uxImageDisplay_MouseMove( object sender, MouseEventArgs e )
        {
            if( _tracking )
            {
                _current = new System.Drawing.Point( e.X, e.Y );
                uxImageDisplay.Invalidate();
            }
        }

        private void uxImageDisplay_MouseUp( object sender, MouseEventArgs e )
        {
            if( _tracking )
            {
                _tracking = false;

                _current = new System.Drawing.Point( e.X, e.Y );
                Bitmap image = _currentImage;
                int x = Utitlies.BoundTo( _start.X, 0, image.Width );
                int y = Utitlies.BoundTo( _start.Y, 0, image.Height );
                int width = Utitlies.BoundTo( Math.Abs( x - _current.X ), 0, image.Width - x );
                int height = Utitlies.BoundTo( Math.Abs( y - _current.Y ), 0, image.Height - y );

                if( width >= MIN_SUB_SIZE && height >= MIN_SUB_SIZE )
                {
                    try
                    {
                        using( Bitmap bitmap = ImageManipulation.SubImage( image, new Rectangle( x, y, width, height ) ) )
                        {
                            string fullFilename = _currentImageHolder.FullFilename;
                            string directory = Path.GetDirectoryName( fullFilename );
                            string filename = Path.GetFileNameWithoutExtension( fullFilename );
                            string extension = Path.GetExtension( fullFilename );
                            string now = DateTime.Now.ToString( "hh-mm-ss_MM-dd-yyy" );
                            string newFilename = directory + Path.DirectorySeparatorChar + "sub" + Path.DirectorySeparatorChar + now + "_" + filename + extension;
                            string newDirectoryName = Path.GetDirectoryName( newFilename );
                            if( !Directory.Exists( newDirectoryName ) )
                            {
                                Directory.CreateDirectory( newDirectoryName );
                            }
                            if( !Utitlies.HasWritePermissionOnDir( newDirectoryName ) )
                            {
                                MessageBox.Show( "You do not have access to write this file: " + newFilename );
                            }
                            else
                            {
                                bitmap.Save( newFilename );
                            }
                        }
                    }
                    catch( OutOfMemoryException ex )
                    {
                        MessageBox.Show( "We failed...sorry\n" + ex.ToString() );
                    }
                    catch( Exception ex )
                    {
                        MessageBox.Show( "The image could not be saved.\n" + ex.ToString() );
                    }
                }
            }

            uxImageDisplay.Invalidate();
        }

        private void uxImageDisplay_PreviewKeyDown( object sender, PreviewKeyDownEventArgs e )
        {
            switch( e.KeyCode )
            {
                case Keys.Escape:
                    _tracking = false;
                    uxImageDisplay.Invalidate();
                    break;
            }
        }

        private void uxDataGrid_KeyUp( object sender, KeyEventArgs e )
        {
            switch( e.KeyCode )
            {
                case Keys.Delete:
                    int row = uxDataGrid.CurrentCell.RowIndex;
                    if( 0 <= row && row < uxImageHolderBindingSource.Count )
                    {
                        ImageHolder im = (ImageHolder)uxImageHolderBindingSource.List[ row ];
                        uxImageHolderBindingSource.RemoveAt( row );
                        try
                        {
                            File.Delete( im.FullFilename );
                        }
                        catch( Exception ex )
                        {
                            MessageBox.Show( "Could not delete selected image:\n" + ex.ToString() );
                        }
                        row = Math.Min( row - 1, uxImageHolderBindingSource.Count - 1 );
                        if( 0 <= row && row < uxImageHolderBindingSource.Count )
                        {
                            uxDataGrid.Rows[ row ].Selected = true;
                        }
                    }
                    break;
            }
        }

        private void uxEnhance_Click( object sender, EventArgs e )
        {
            if( _currentImage != null )
            {
                UnmanagedImage image, grayImage, edgesImage;

                Bitmap newBitmap = new Bitmap( _currentImage );
                Rectangle rect = new Rectangle( 0, 0, newBitmap.Width, newBitmap.Height );
                image = new UnmanagedImage( newBitmap.LockBits( rect, ImageLockMode.ReadWrite, _currentImage.PixelFormat ) );

                if( _currentImage.PixelFormat == PixelFormat.Format8bppIndexed )
                {
                    grayImage = image;
                }
                else
                {
                    grayImage = UnmanagedImage.Create( image.Width, image.Height, PixelFormat.Format8bppIndexed );
                    Grayscale.CommonAlgorithms.BT709.Apply( image, grayImage );
                }
                
                DifferenceEdgeDetector edgeDetector = new DifferenceEdgeDetector();
                edgesImage = edgeDetector.Apply( grayImage );

                Threshold thresholdFilter = new Threshold( (int)uxValue.Value );
                thresholdFilter.ApplyInPlace( edgesImage );

                BlobCounter blobCounter = new BlobCounter();
                blobCounter.MinHeight = 10;
                blobCounter.MinHeight = 10;
                blobCounter.FilterBlobs = true;
                blobCounter.ObjectsOrder = ObjectsOrder.Size;

                blobCounter.ProcessImage( edgesImage );
                Blob[] blobs = blobCounter.GetObjectsInformation();

                _corners.Clear();
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
                            _corners.Add( corners );
                        }
                    }
                }

                uxImageDisplay.Invalidate();

                foreach( IDisposable disposable in new IDisposable[] { newBitmap, image, grayImage, edgesImage } )
                {
                    if( disposable != null )
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        private void uxTimer_Tick( object sender, EventArgs e )
        {
            uxMemory.Text = String.Format( "{0:n0}", GC.GetTotalMemory( false ) ) + " Bytes";
        }
    }
}
