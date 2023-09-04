using System;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Mdm.Oss.ClipUtil
{
	/// <summary>
	/// API Declarations for the Windows Shell32 library
	/// </summary>
	public class Shell32
	{
		public const int MAX_PATH = 260;

        [DllImport("coredll.dll")]
        public extern static void GetSystemTime(ref SYSTEMTIME lpSystemTime);

        public struct SYSTEMTIME {  
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        [StructLayout(LayoutKind.Sequential)] 
		protected struct structSHFILEINFO 
		{
			/// <summary>
			/// Handle to the icon that represents the file. 
			/// You are responsible for destroying this 
			/// handle with DestroyIcon when you no longer need it. 
			/// </summary>
			public IntPtr hIcon;
			/// <summary>
			/// Index of the icon image within the system image list. 
			/// </summary>
			public Int16  iIcon;
			/// <summary>
			/// Array of values that indicates the attributes 
			/// of the file object. For information about these 
			/// values, see the IShellFolder::GetAttributesOf method. 
			/// </summary>
			public int  dwAttributes;
			/// <summary>
			/// String that contains the name of the file 
			/// as it appears in the Microsoft® Windows® Shell, 
			/// or the path and file name of the file that 
			/// contains the icon representing the file.
			/// </summary>
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=MAX_PATH)] 
			public string  szDisplayName;
			/// <summary>
			/// String that describes the type of file.
			/// </summary>
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)] 
			public string  szTypeName;

		}

        [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ShellExecute(
            IntPtr hwnd, // HWND
            string lpOperation, // LPCTSTR 
            string lpFile, // LPCTSTR 
            string lpParameters, // LPCTSTR 
            string lpDirectory, // LPCTSTR 
            int nShowCmd // INT
        );
	}
}
