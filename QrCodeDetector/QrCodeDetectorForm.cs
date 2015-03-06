using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private static readonly int WATCH_DIRECTORY_DEPTH = 2;
        private static readonly int MIN_SUB_SIZE = 10;

        private static readonly Pen QR_CODE_PEN = new Pen( Color.Blue, 2 );
        private static readonly SolidBrush SELECTION_BRUSH = new SolidBrush( Color.FromArgb( 175, Color.LimeGreen ) );
        private static readonly Pen SELECTION_PEN = new Pen( Color.LimeGreen, 2 );
        private static readonly Pen QUADS_PEN = new Pen( Color.Red, 4 );

        private string _imageDirectory;
        private ImageHolder _currentImageHolder;
        private bool _tracking;
        private PointF _start;
        private PointF _currentPoint;

        public QrCodeDetectorForm()
        {
            InitializeComponent();

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
                        AddImage( e.FullPath );
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

                    SetCurrentImage( (ImageHolder)uxImageHolderBindingSource.List[ index ] );
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
            if( uxAutoAddImages.Checked )
            {
                AddImagesFromDirectory( new DirectoryInfo( _imageDirectory ), WATCH_DIRECTORY_DEPTH );
            }
        }

        private void SetCurrentImage( ImageHolder holder )
        {
            _currentImageHolder = holder;
            _tracking = false;
            if( uxImageDisplay.Image != null )
            {
                uxImageDisplay.Image.Dispose();
            }
            uxImageDisplay.Image = holder.LoadImage();
            if( uxAutoDetectOnView.Checked )
            {
                DetectQrCode( _currentImageHolder, true );
            }
        }

        private void AddImagesFromDirectory( DirectoryInfo directory, int directoryDepth )
        {
            if( directoryDepth > 0 )
            {
                foreach( FileInfo file in directory.GetFiles() )
                {
                    AddImage( file.FullName );
                }
                foreach( DirectoryInfo dir in directory.GetDirectories() )
                {
                    AddImagesFromDirectory( dir, directoryDepth - 1 );
                }
            }
        }

        private void DetectQrCode( ImageHolder imageHolder, bool displayResults )
        {
            try
            {
                imageHolder.DetectQrCode();
                uxDataGrid.Invalidate();
            }
            catch( Exception ex )
            {
                Error( "An error occured while detecting QR codes.", ex );
            }
            if( displayResults )
            {
                uxQrCodeData.Text = _currentImageHolder.QrCodeData;
                uxImageDisplay.Invalidate();
            }
        }

        private void uxImageDisplay_Paint( object sender, PaintEventArgs e )
        {
            if( _currentImageHolder != null )
            {
                Graphics g = e.Graphics;
                if( _currentImageHolder.QrCodePoints != null )
                {
                    PointF[] points = _currentImageHolder.QrCodePoints.ToList().ConvertAll( rp => new PointF( rp.X, rp.Y ) ).ToArray();
                    g.DrawPolygon( QR_CODE_PEN, points );
                }
                if( _tracking
                 && _start != PointF.Empty
                 && _currentPoint != PointF.Empty )
                {
                    float x = Math.Min( _start.X, _currentPoint.X );
                    float y = Math.Min( _start.Y, _currentPoint.Y );
                    float width = Math.Abs( _currentPoint.X - _start.X );
                    float height = Math.Abs( _currentPoint.Y - _start.Y );
                    g.FillRectangle( SELECTION_BRUSH, x, y, width, height );
                    g.DrawRectangle( SELECTION_PEN, x, y, width, height );
                }

                foreach( List<IntPoint> cornerList in _currentImageHolder.Corners )
                {
                    PointF[] points = cornerList.ConvertAll( intPoint => new PointF( intPoint.X, intPoint.Y ) ).ToArray();
                    g.DrawPolygon( QUADS_PEN, points );
                }
            }
        }

        private void uxImageDisplay_MouseDown( object sender, MouseEventArgs e )
        {
            _tracking = _currentImageHolder != null;
            _start = new PointF( e.X, e.Y );
        }

        private void uxImageDisplay_MouseMove( object sender, MouseEventArgs e )
        {
            if( _tracking )
            {
                _currentPoint = new PointF( e.X, e.Y );
                uxImageDisplay.Invalidate();
            }
        }

        private void uxImageDisplay_MouseUp( object sender, MouseEventArgs e )
        {
            if( _tracking )
            {
                _tracking = false;

                _currentPoint = new PointF( e.X, e.Y );
                using( Bitmap image = _currentImageHolder.LoadImage() )
                {
                    int x = (int)Utitlies.BoundTo( _start.X, 0, image.Width );
                    int y = (int)Utitlies.BoundTo( _start.Y, 0, image.Height );
                    int width = (int)Utitlies.BoundTo( Math.Abs( x - _currentPoint.X ), 0, image.Width - x );
                    int height = (int)Utitlies.BoundTo( Math.Abs( y - _currentPoint.Y ), 0, image.Height - y );

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
                                    Error( "You do not have access to write this file: " + newFilename, null );
                                }
                                else
                                {
                                    bitmap.Save( newFilename );
                                }
                            }
                        }
                        catch( OutOfMemoryException ex )
                        {
                            Error( "We failed...sorry", ex );
                        }
                        catch( Exception ex )
                        {
                            MessageBox.Show( "The image could not be saved." + ex.ToString() );
                        }
                    }
                }
            }
            uxImageDisplay.Invalidate();
        }

        private void uxImageDisplay_PreviewKeyDown( object sender, PreviewKeyDownEventArgs e )
        {
            //TODO Still not working
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
                            Error( "Could not delete selected image.", ex );
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
            if( _currentImageHolder != null )
            {
                try
                {
                    _currentImageHolder.RunEdgeDetection( (int)uxValue.Value );
                    uxImageDisplay.Invalidate();
                }
                catch( Exception ex )
                {
                    Error( "An error occured while detecting edges.", ex );
                }
            }
        }

        private void uxTimer_Tick( object sender, EventArgs e )
        {
            uxBytesUsed.Text = String.Format( "{0:n0}", GC.GetTotalMemory( false ) ) + " Bytes Used";
        }

        private void uxAutoAddImages_CheckedChanged( object sender, EventArgs e )
        {
            uxFileSystemWatcher.EnableRaisingEvents = uxAutoAddImages.Checked;
        }

        private void Error( String message, Exception ex )
        {
            uxStatusLabel.Text = message;
            if( ex != null )
            {
                uxStatusLabel.Tag = ex;
            }
        }

        private void AddImage( string filename )
        {
            if( FindImageHolderIndex( filename ) < 0 )
            {
                try
                {
                    ImageHolder im = new ImageHolder( filename );
                    uxImageHolderBindingSource.Add( im );
                    if( uxAutoDetectOnAdd.Checked )
                    {
                        DetectQrCode( im, false );
                    }
                }
                catch( Exception ex )
                {
                    Error( "The given image could not be added: " + filename, ex );
                }
            }
        }

        private void uxAddImages_Click( object sender, EventArgs e )
        {
            if( uxOpenFileDialog.ShowDialog() == DialogResult.OK )
            {
                foreach( String filename in uxOpenFileDialog.FileNames )
                {
                    AddImage( filename );
                }
            }
        }

        private void uxQrCodeData_LinkClicked( object sender, LinkClickedEventArgs e )
        {
            Process.Start( e.LinkText );
        }

        private void uxStatusLabel_Click( object sender, EventArgs e )
        {
            Exception ex = uxStatusLabel.Tag as Exception;
            if( ex != null )
            {
                string exception = ex.ToString();
                Clipboard.SetText( exception, TextDataFormat.Text );
                MessageBox.Show( exception );
            }
            uxStatusLabel.Text = "";
            uxStatusLabel.Tag = null;
        }
    }
}
