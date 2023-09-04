using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Mdm.Oss.WinUtil;

namespace Mdm.Oss.WinUtil {

    /// <summary>
    /// Com functions dealing with the Shell.
    /// </summary>
    public class Win32ShellDef {
        //Retrieves information about an object in the file system.
        public const int MAX_PATH = 260;

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(
            [In] string pszPath,
            [In] uint dwFileAttributes,
            [In] ref Mdm.Oss.WinUtil.Win32FileDef.SHFILEINFO psfi,
            [In] uint cbSizeFileInfo,
            [In] uint uFlags);

        public const uint FILE_ATTRIBUTRE_NORMAL = 0x4000;

        public const uint ILC_COLORDDB = 0x1;
        public const uint ILC_MASK = 0x0;

        public const uint ILD_TRANSPARENT = 0x1;

        public const uint SHGFI_ATTR_SPECIFIED = 0x20000;
        public const uint SHGFI_ATTRIBUTES = 0x800;
        public const uint SHGFI_PIDL = 0x8;
        public const uint SHGFI_DISPLAYNAME = 0x200;
        public const uint SHGFI_USEFILEATTRIBUTES = 0x10;

        public const uint SHGFI_EXETYPE = 0x2000;
        public const uint SHGFI_SYSICONINDEX = 0x4000;

        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0;
        public const uint SHGFI_SHELLICONSIZE = 0x4;
        public const uint SHGFI_SMALLICON = 0x1;
        public const uint SHGFI_TYPENAME = 0x400;
        public const uint SHGFI_ICONLOCATION = 0x1000;

        [DllImport("Shell32.dll")]
        public static extern IntPtr ShellExecute(
            [In] IntPtr hwnd,
            [In] string lpOperation,
            [In] string lpFile,
            [In] string lpParameters,
            [In] string lpDirectory,
            [In] int nShowCmd
            );

        /// <summary>
        /// <para> Structure that stores file information.</para>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        protected struct structSHFILEINFO {
            /// <summary>
            /// Handle to the icon that represents the file. 
            /// You are responsible for destroying this 
            /// handle with DestroyIcon when you no longer need it. 
            /// </summary>
            public IntPtr hIcon;
            /// <summary>
            /// Index of the icon image within the system image list. 
            /// </summary>
            public Int16 iIcon;
            /// <summary>
            /// Array of values that indicates the attributes 
            /// of the file object. For information about these 
            /// values, see the IShellFolder::GetAttributesOf method. 
            /// </summary>
            public int dwAttributes;
            /// <summary>
            /// String that contains the name of the file 
            /// as it appears in the Microsoft® Windows® Shell, 
            /// or the path and file name of the file that 
            /// contains the icon representing the file.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string szDisplayName;
            /// <summary>
            /// String that describes the type of file.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;

        }
    }
} // end of namespace Mdm 
