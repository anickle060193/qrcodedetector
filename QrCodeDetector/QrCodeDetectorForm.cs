using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using QrCodeDetector.Properties;
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
        public enum AutoDetectOptions { Disabled, OnAdd, OnView }

        private static readonly int WATCH_DIRECTORY_DEPTH = 2;
        private static readonly int MIN_SUB_SIZE = 10;
        private static readonly int CLICK_POINT_RADIUS = 4;

        private static readonly Pen QR_CODE_PEN = new Pen( Color.Blue, 2 );
        private static readonly SolidBrush SELECTION_BRUSH = new SolidBrush( Color.FromArgb( 175, Color.LimeGreen ) );
        private static readonly Pen SELECTION_PEN = new Pen( Color.LimeGreen, 2 );
        private static readonly Pen QUADS_PEN = new Pen( Color.Red, 2 );
        private static readonly SolidBrush CLICK_POINTS_BRUSH = new SolidBrush( Color.Orchid );

        private string _imageDirectory;
        private ImageHolder _currentImageHolder;
        private bool _tracking;
        private PointF _start;
        private PointF _currentPoint;
        private List<IntPoint> _clickPoints = new List<IntPoint>();

        public QrCodeDetectorForm()
        {
            InitializeComponent();

            Log.Init();
            Log.Write( "QrCodeDetectorForm started." );

            uxFileSystemWatcher.Deleted += HandleDirectoryWatcherEvents;
            uxFileSystemWatcher.Created += HandleDirectoryWatcherEvents;

            SetWatchDirectory( Settings.Default.WatchDirectoryLocation );
            uxAutoAddImages.Checked = Settings.Default.AutoAddFromWatchDirectory;
            switch( Settings.Default.AutoDetectOption )
            {
                case AutoDetectOptions.Disabled:
                    uxAutoDetectDisabled.Checked = true;
                    break;

                case AutoDetectOptions.OnAdd:
                    uxAutoDetectOnAdd.Checked = true;
                    break;

                case AutoDetectOptions.OnView:
                    uxAutoDetectOnView.Checked = true;
                    break;
            }
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
                    Log.Write( "Image creation detected: " + e.FullPath );
                    AddImage( e.FullPath );
                    break;

                case WatcherChangeTypes.Deleted:
                    int index = FindImageHolderIndex( e.FullPath );
                    if( index >= 0 )
                    {
                        Log.Write( "Image deletion detected: " + e.FullPath );
                        uxImageHolderBindingSource.RemoveAt( index );
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
                Settings.Default.WatchDirectoryLocation = _imageDirectory;
                Settings.Default.Save();

                uxFileSystemWatcher.Path = _imageDirectory;
                Log.Write( "Image watch directory set: " + directoryName );
            }

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
            Log.Write( "Current image set: " + holder.FullFilename );
            if( uxAutoDetectOnView.Checked )
            {
                DetectQrCode( _currentImageHolder );
            }
            uxQrCodeData.Text = _currentImageHolder.QrCodeData;
            uxImageDisplay.Invalidate();
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

        private void DetectQrCode( ImageHolder imageHolder )
        {
            try
            {
                imageHolder.DetectQrCode();
                Log.Write( "QR Code detection ran on: " + imageHolder.FullFilename );
                uxDataGrid.Invalidate();
            }
            catch( Exception ex )
            {
                Log.Write( "An error occured while detecting QR codes in: " + imageHolder.FullFilename, ex );
                Error( "An error occured while detecting QR codes.", ex );
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

                foreach( IntPoint point in _clickPoints )
                {
                    g.FillEllipse( CLICK_POINTS_BRUSH, point.X - CLICK_POINT_RADIUS, point.Y - CLICK_POINT_RADIUS, 2 * CLICK_POINT_RADIUS, 2 * CLICK_POINT_RADIUS );
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
                            using( Bitmap bitmap = ImageUtilities.SubImage( image, new Rectangle( x, y, width, height ) ) )
                            {
                                string newFilename = CreateSubImageFilename( _currentImageHolder.FullFilename );
                                if( newFilename != null )
                                {
                                    bitmap.Save( newFilename );
                                }
                            }
                        }
                        catch( Exception ex )
                        {
                            Log.Write( "The sub-image could not be saved: " + _currentImageHolder.FullFilename, ex );
                            Error( "The image could not be saved.", ex );
                        }
                    }
                    else
                    {
                        _clickPoints.Add( new IntPoint( e.X, e.Y ) );
                        if( _clickPoints.Count == 4 )
                        {
                            try
                            {
                                QuadrilateralTransformation transform = new QuadrilateralTransformation( _clickPoints, 200, 200 );
                                using( Bitmap quadImage = transform.Apply( image ) )
                                {
                                    String newFilename = CreateSubImageFilename( _currentImageHolder.FullFilename );
                                    if( newFilename != null )
                                    {
                                        quadImage.Save( newFilename );
                                    }
                                }
                            }
                            catch( Exception ex )
                            {
                                Log.Write( "The sub-image could not be saved: " + _currentImageHolder.FullFilename, ex );
                                Error( "The image could not be saved.", ex );
                            }
                            _clickPoints.Clear();
                        }
                        uxImageDisplay.Invalidate();
                    }
                }
            }
            uxImageDisplay.Invalidate();
        }

        private String CreateSubImageFilename(String originalFilename )
        {
            string fullFilename = originalFilename;
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
                Log.Write( "The program does not have access to write this file: " + newFilename );
                Error( "You do not have access to write this file: " + newFilename, null );
                return null;
            }
            else
            {
                return newFilename;
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
                            Log.Write( "Image delete: " + im.FullFilename );
                        }
                        catch( Exception ex )
                        {
                            Log.Write( "Could not delete image: " + im.FullFilename, ex );
                            Error( "Could not delete selected image.", ex );
                        }
                        SetSelectedImageCell( row - 1 );
                    }
                    break;
            }
        }

        private void SetSelectedImageCell( int row )
        {
            row = Utitlies.BoundTo( row, 0, uxDataGrid.Rows.Count - 1 );
            if( row > 0 )
            {
                uxDataGrid.ClearSelection();
                DataGridViewCell cell = uxDataGrid[ 0, row ];
                cell.Selected = true;
                uxDataGrid.CurrentCell = cell;
            }
        }

        private void uxEnhance_Click( object sender, EventArgs e )
        {
            if( _currentImageHolder != null )
            {
                try
                {
                    _currentImageHolder.RunEdgeDetection( (int)uxValue.Value, uxShowEnchancedImage.Checked, uxShowQuadImages.Checked );
                    Log.Write( "Edge detection ran on: " + _currentImageHolder.FullFilename );
                    uxImageDisplay.Invalidate();
                }
                catch( Exception ex )
                {
                    Log.Write( "An error occured while detecting edges for: " + _currentImageHolder.FullFilename, ex );
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
            bool isChecked = uxAutoAddImages.Checked;
            uxFileSystemWatcher.EnableRaisingEvents = isChecked;
            Settings.Default.AutoAddFromWatchDirectory = isChecked;
            Settings.Default.Save();
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
            int index = FindImageHolderIndex( filename );
            if( index < 0 )
            {
                try
                {
                    ImageHolder im = new ImageHolder( filename );
                    uxImageHolderBindingSource.Add( im );
                    Log.Write( "Image added: " + filename );
                    if( uxAutoDetectOnAdd.Checked )
                    {
                        DetectQrCode( im );
                    }
                }
                catch( Exception ex )
                {
                    Log.Write( "The given image could not be added: " + filename, ex );
                    Error( "The given image could not be added: " + filename, ex );
                }
            }
            else
            {
                SetSelectedImageCell( index );
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

        private void QrCodeDetectorForm_FormClosed( object sender, FormClosedEventArgs e )
        {
            Log.Write( "QrCodeDetectorForm ended." );
        }

        private void AutoDetectOption_CheckedChanged( object sender, EventArgs e )
        {
            if( sender == uxAutoDetectDisabled )
            {
                Settings.Default.AutoDetectOption = AutoDetectOptions.Disabled;
            }
            else if( sender == uxAutoDetectOnAdd )
            {
                Settings.Default.AutoDetectOption = AutoDetectOptions.OnAdd;
            }
            else if( sender == uxAutoDetectOnView )
            {
                Settings.Default.AutoDetectOption = AutoDetectOptions.OnView;
            }
            Settings.Default.Save();
        }
    }
}
