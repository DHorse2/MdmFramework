using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;
using System.Text;

namespace Mdm.Oss.WinUtil {
    /// <summary>
    /// COM functions dealing with file informations.
    /// </summary>
    public class Win32FileDef {
        /// <summary>
        /// Contains information about a file object.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        /// <summary>
        /// <para> Provides a 4 part standard structure for UUID's.</para>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct UUID {
            public int Data1;
            public short Data2;
            public short Data3;
            public byte[] Data4;
        }
    }
}
