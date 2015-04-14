using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeDetector
{
    public static class Utitlies
    {
        public static bool HasWritePermissionOnDir( string path )
        {
            try
            {
                bool writeAllow = false;
                bool writeDeny = false;
                DirectorySecurity accessControlList = Directory.GetAccessControl( path );
                if( accessControlList == null )
                {
                    return false;
                }
                AuthorizationRuleCollection accessRules = accessControlList.GetAccessRules( true, true, typeof( System.Security.Principal.SecurityIdentifier ) );
                if( accessRules == null )
                {
                    return false;
                }

                foreach( FileSystemAccessRule rule in accessRules )
                {
                    if( ( FileSystemRights.Write & rule.FileSystemRights ) != FileSystemRights.Write )
                    {
                        continue;
                    }

                    if( rule.AccessControlType == AccessControlType.Allow )
                    {
                        writeAllow = true;
                    }
                    else if( rule.AccessControlType == AccessControlType.Deny )
                    {
                        writeDeny = true;
                    }
                }
                return writeAllow && !writeDeny;
            }
            catch
            {
                return false;
            }
        }

        public static int BoundTo( int value, int lowerBound, int higherBound )
        {
            if( value < lowerBound )
            {
                return lowerBound;
            }
            else if( value > higherBound )
            {
                return higherBound;
            }
            else
            {
                return value;
            }
        }

        public static float BoundTo( float value, float lowerBound, float higherBound )
        {
            if( value < lowerBound )
            {
                return lowerBound;
            }
            else if( value > higherBound )
            {
                return higherBound;
            }
            else
            {
                return value;
            }
        }

        public static Rectangle GetBounds( Point p1, Point p2, int offset )
        {
            int x = Math.Min( p1.X, p2.X ) - offset;
            int y = Math.Min( p1.Y, p2.Y ) - offset;
            int width = Math.Abs( p1.X - p2.X ) + offset;
            int height = Math.Abs( p1.Y - p2.Y ) + offset;
            return new Rectangle( x, y, width, height );
        }

        public static Rectangle GetBounds( Point p1, Point p2 )
        {
            return GetBounds( p1, p2, 0 );
        }
    }
}
