using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeDetector
{
    public static class Log
    {
        private static string _logFilename;

        public static void Init( string filename )
        {
            _logFilename = filename;
        }

        private static void VerifyInitialized()
        {
            if( !File.Exists( _logFilename ) )
            {
                throw new InvalidOperationException( "Log has not been initialized. See Log.Init()" );
            }
        }

        private static StreamWriter CreateWriter()
        {
            VerifyInitialized();
            return new StreamWriter( _logFilename, true );
        }

        private static void WriteDateTimeTag( string tag, StreamWriter writer )
        {
            DateTime now = DateTime.Now;
            writer.Write( now.ToLongDateString() );
            writer.Write( " " );
            writer.Write( now.ToLongTimeString() );
            writer.Write( ": " );
            writer.Write( tag );
            writer.Write( " - " );
        }

        public static void Write( string tag, string output )
        {
            using( StreamWriter writer = CreateWriter() )
            {
                WriteDateTimeTag( tag, writer );
                writer.Write( output );
            }
        }

        public static void Write( string tag, Exception ex )
        {
            using( StreamWriter writer = CreateWriter() )
            {
                WriteDateTimeTag( tag, writer );
                writer.Write( ex.ToString() );
            }
        }
    }
}
