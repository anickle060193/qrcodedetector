using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeDetector
{
    public static class Log
    {
        private static readonly string LOG_FILENAME_FORMAT = "log-{0}.txt";
        private static string _logFilename;
        private static bool _initialized;

        public static void Init()
        {
            int i = 0;
            while( File.Exists( String.Format( LOG_FILENAME_FORMAT, i ) ) )
            {
                i++;
            }
            _logFilename = String.Format( LOG_FILENAME_FORMAT, i );
            _initialized = true;
            File.Create( _logFilename ).Close();
        }

        private static void VerifyInitialized()
        {
            if( !_initialized )
            {
                throw new InvalidOperationException( "Log has not been initialized. See Log.Init()" );
            }
        }

        private static StreamWriter CreateWriter()
        {
            VerifyInitialized();
            return new StreamWriter( _logFilename, true );
        }

        private static void WriteDateTimeMemberNameLine( string memberName, int sourceLineNumber, StreamWriter writer )
        {
            writer.Write( DateTime.Now.ToString( "[ MM/dd/yyyy hh:mm.ss ] : " ) + memberName + " line: " + sourceLineNumber.ToString() + " - " );
        }

        public static void Write( string output, [CallerMemberName]string memberName = "MemberName", [CallerLineNumber] int sourceLineNumber = 0 )
        {
            using( StreamWriter writer = CreateWriter() )
            {
                WriteDateTimeMemberNameLine( memberName, sourceLineNumber, writer );
                writer.WriteLine( output );
            }
        }

        public static void Write( string output, Exception ex, [CallerMemberName]string memberName = "MemberName", [CallerLineNumber] int sourceLineNumber = 0 )
        {
            using( StreamWriter writer = CreateWriter() )
            {
                WriteDateTimeMemberNameLine( memberName, sourceLineNumber, writer );
                writer.WriteLine( output );
                writer.WriteLine( ex.ToString() );
            }
        }
    }
}
