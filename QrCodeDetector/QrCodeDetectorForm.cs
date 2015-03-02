using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private ImageHolder _currentImage;
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

        private void uxDataGrid_CellStateChanged( object sender, DataGridViewCellStateChangedEventArgs e )
        {
            if( e.StateChanged == DataGridViewElementStates.Selected )
            {
                if( 0 <= e.Cell.RowIndex && e.Cell.RowIndex < uxImageHolderBindingSource.Count )
                {
                    _tracking = false;

                    _currentImage = (ImageHolder)uxImageHolderBindingSource.List[ e.Cell.RowIndex ];
                    uxImageDisplay.Image = _currentImage.Image;
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
            foreach( FileInfo filename in dir.GetFiles( "*.png" ) )
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
                LuminanceSource source = new BitmapLuminanceSource( _currentImage.Image );
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
                Bitmap image = _currentImage.Image;
                int x = Math.Min( _start.X, _current.X );
                int y = Math.Min( _start.Y, _current.Y );
                int width = Math.Min( Math.Abs( _current.X - _start.X ), image.Width - x );
                int height = Math.Min( Math.Abs( _current.Y - _start.Y ), image.Height - y );

                if( width >= MIN_SUB_SIZE && height >= MIN_SUB_SIZE )
                {
                    Rectangle rect = new Rectangle( x, y, width, height );
                    Bitmap bitmap = image.Clone( rect, image.PixelFormat );
                    try
                    {
                        string fullFilename = _currentImage.FullFilename;
                        string directory = Path.GetDirectoryName( fullFilename );
                        string filename = Path.GetFileNameWithoutExtension( fullFilename );
                        string extension = Path.GetExtension( fullFilename );
                        string now = DateTime.Now.ToString( "hh-mm-ss_MM-dd-yyy" );
                        string newFilename = directory + Path.DirectorySeparatorChar + "sub" + Path.DirectorySeparatorChar + filename + "_" + now + extension;
                        bitmap.Save( newFilename );
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

        private void uxEnhance_Click( object sender, EventArgs e )
        {
            if( _currentImage != null )
            {
                CustomImage image = new CustomImage( _currentImage.Image );
                CustomImage blurred = image.BlurImage( 9 );
                CustomImage result = image + ( image - blurred );
                new ImageForm( "Enhanced!...sorta", result.ConvertToBitmap() ).Show();
            }
        }
    }
}
