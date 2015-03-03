using AForge.Imaging;
using AForge.Imaging.Filters;
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

        private string _imageDirectory;
        private ImageHolder _currentImageHolder;
        private Bitmap _currentImage;
        private PointF[] _qrPoints;
        private bool _tracking;
        private Point _start;
        private Point _current;

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

                    _currentImageHolder = (ImageHolder)uxImageHolderBindingSource.List[ index ];
                    if( _currentImage != null )
                    {
                        _currentImage.Dispose();
                    }
                    _currentImage =_currentImageHolder.LoadImage();
                    uxImageDisplay.Image = _currentImage;
                    DetectQrCode();
                }
            }
        }

        private void uxDataGrid_CellStateChanged( object sender, DataGridViewCellStateChangedEventArgs e )
        {

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

        void uxImageDisplay_Paint( object sender, PaintEventArgs e )
        {
            Graphics g = e.Graphics;
            if( _qrPoints != null )
            {
                g.DrawPolygon( PEN, _qrPoints );
            }
            if( _tracking
             && _start != Point.Empty
             && _current != Point.Empty )
            {
                int x = Math.Min( _start.X, _current.X );
                int y = Math.Min( _start.Y, _current.Y );
                int width = Math.Abs( _current.X - _start.X );
                int height = Math.Abs( _current.Y - _start.Y );
                g.FillRectangle( SELECTION_BRUSH, x, y, width, height );
                g.DrawRectangle( SELECTION_PEN, x, y, width, height );
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
                    ResultPoint[] points = result.ResultPoints;
                    _qrPoints = new PointF[ points.Length ];
                    for( int i = 0; i < points.Length; i++ )
                    {
                        if( points[ i ] != null )
                        {
                            _qrPoints[ i ] = new PointF( points[ i ].X, points[ i ].Y );
                        }
                    }
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
            _start = new Point( e.X, e.Y );
        }

        private void uxImageDisplay_MouseMove( object sender, MouseEventArgs e )
        {
            if( _tracking )
            {
                _current = new Point( e.X, e.Y );
                uxImageDisplay.Invalidate();
            }
        }

        private void uxImageDisplay_MouseUp( object sender, MouseEventArgs e )
        {
            if( _tracking )
            {
                _tracking = false;

                _current = new Point( e.X, e.Y );
                using( Bitmap image = _currentImage )
                {
                    int x = Math.Max( 0, Math.Min( _start.X, _current.X ) );
                    int y = Math.Max( 0, Math.Min( _start.Y, _current.Y ) );
                    int width = Math.Min( Math.Abs( _current.X - _start.X ), image.Width - x );
                    int height = Math.Min( Math.Abs( _current.Y - _start.Y ), image.Height - y );

                    if( width >= MIN_SUB_SIZE && height >= MIN_SUB_SIZE )
                    {
                        Rectangle rect = new Rectangle( x, y, width, height );
                        using( Bitmap bitmap = image.Clone( rect, image.PixelFormat ) )
                        {
                            try
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
                            catch( Exception ex )
                            {
                                MessageBox.Show( "The image could not be saved.\n" + ex.ToString() );
                            }
                        }
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
                Bitmap newBitmap = new Bitmap( _currentImage );
                Rectangle rect = new Rectangle( 0, 0, newBitmap.Width, newBitmap.Height );
                UnmanagedImage image = new UnmanagedImage( newBitmap.LockBits( rect, ImageLockMode.ReadWrite, _currentImage.PixelFormat ) );

                UnmanagedImage grayImage = null;
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
                UnmanagedImage edgesImage = edgeDetector.Apply( grayImage );

                Threshold thresholdFilter = new Threshold( (int)uxValue.Value );
                thresholdFilter.ApplyInPlace( edgesImage );

                new ImageForm( "Edges", edgesImage.ToManagedImage() ).Show();
            }
        }
    }
}
