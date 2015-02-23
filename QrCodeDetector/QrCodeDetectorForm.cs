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

namespace QrCodeDetector
{
    /// <summary>
    /// A QrCodeDetectorForm class.
    /// </summary>
    public partial class QrCodeDetectorForm : Form
    {
        /// <summary>
        /// The directory that is being watched for new images.
        /// </summary>
        private string _imageDirectory;

        /// <summary>
        /// Constructs a new QrCodeDetectorForm.
        /// </summary>
        public QrCodeDetectorForm()
        {
            InitializeComponent();

            uxFileSystemWatcher.EnableRaisingEvents = false;
            uxFileSystemWatcher.Filter = "*.png";
            uxFileSystemWatcher.Deleted += HandleDirectoryWatcherEvents;
            uxFileSystemWatcher.Created += HandleDirectoryWatcherEvents;

            SetWatchDirectory( Properties.Settings.Default.WatchDirectoryLocation );
        }

        /// <summary>
        /// Handles a Click event on the uxSetImageDirectory button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxSetImageDirectory_Click( object sender, EventArgs e )
        {
            if( uxImageDirectoryBrowser.ShowDialog() == DialogResult.OK )
            {
                SetWatchDirectory( uxImageDirectoryBrowser.SelectedPath );
            }
        }

        /// <summary>
        /// Handles a change in the directory being watched.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Finds the index of the given image in the list of images.
        /// </summary>
        /// <param name="filename">The filename of the image to find</param>
        /// <returns>The index of the image in the list of images</returns>
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

        /// <summary>
        /// Handles a CellStateChange event on uxDataGrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxDataGrid_CellStateChanged( object sender, DataGridViewCellStateChangedEventArgs e )
        {
            if( e.StateChanged == DataGridViewElementStates.Selected )
            {
                if( 0 <= e.Cell.RowIndex && e.Cell.RowIndex < uxImageHolderBindingSource.Count )
                {
                    ImageHolder ih = (ImageHolder)uxImageHolderBindingSource.List[ e.Cell.RowIndex ];
                    uxImageDisplay.Image = ih.Image;
                }
            }
        }

        /// <summary>
        /// Invokes a given delegate on the DataGrid.
        /// </summary>
        /// <param name="action">The delegate to run</param>
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

        /// <summary>
        /// Sets the directory to watch for images.
        /// </summary>
        /// <param name="directoryName">The path of the directory to watch</param>
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
            foreach( string filename in Directory.GetFiles( _imageDirectory, "*.png" ) )
            {
                try
                {
                    ImageHolder im = new ImageHolder( filename );
                    uxImageHolderBindingSource.Add( im );
                }
                catch { }
            }
        }
    }
}
