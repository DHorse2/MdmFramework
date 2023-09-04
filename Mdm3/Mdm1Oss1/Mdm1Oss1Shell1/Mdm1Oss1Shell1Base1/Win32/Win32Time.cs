using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Linq;
using System.Text;

namespace Mdm.Oss.WinUtil {

    /// <summary>
    /// Com functions that deal with Time and Date.
    /// </summary>
    public class Win32TimeDef {

        public const int MAX_PATH = 260;

        /// <summary>
        /// <para> A 16 byte layout for storing data and time.</para>
        /// </summary>
        //[StructLayout(LayoutKind.Explicit, Size = 16, CharSet = CharSet.Ansi)]
        [StructLayout(LayoutKind.Sequential)]
        public class SYSTEMTIME {
            //[FieldOffset(0)]
            public ushort wYear;
            //[FieldOffset(2)]
            public ushort wMonth;
            //[FieldOffset(4)]
            public ushort wDayOfWeek;
            //[FieldOffset(6)]
            public ushort wDay;
            //[FieldOffset(8)]
            public ushort wHour;
            //[FieldOffset(10)]
            public ushort wMinute;
            //[FieldOffset(12)]
            public ushort wSecond;
            //[FieldOffset(14)]
            public ushort wMilliseconds;
        }

        [DllImport("Kernel32.dll")]
        public extern static void GetSystemTime([Out] SYSTEMTIME lpSystemTime);

        [StructLayout(LayoutKind.Sequential)]
        public struct CoFileTime {
            int dwLowDateTime;
            int dwHighDateTime;
        }

        [DllImport("Kernel32.dll")]
        static extern IntPtr CoFileTimeNow([Out] CoFileTime lpFileTime);

        // BOOL CoDosDateTimeToFileTime(WORD nDosDate, WORD nDosTime, FILETIME* lpFileTime );
        // BOOL CoFileTimeToDosDateTime(FILETIME * lpFileTime, LPWORD lpDosDate, LPWORD lpDosTime );

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        static extern bool FileTimeToSystemTime (
            [In] ref System.Runtime.InteropServices.ComTypes.FILETIME FileTime,
            // [In] ref System.Runtime.InteropServices.ComTypes.FILETIME FileTime,
            [Out] SYSTEMTIME SystemTime);

        /// <summary>
        /// Converts a file time to DateTime format.
        /// </summary>
        /// <param name="filetime">System.Runtime.InteropServices.ComTypes.FILETIME structure</param>
        /// <returns>DateTime structure</returns>
        public static DateTime FileTimeToDateTime(System.Runtime.InteropServices.ComTypes.FILETIME filetime)
        {
            SYSTEMTIME st = new SYSTEMTIME();
            FileTimeToSystemTime(ref filetime, st);
            return new DateTime(st.wYear, st.wMonth, st.wDay, st.wHour, st.wMinute, st.wSecond, st.wMilliseconds);

        }

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        static extern bool SystemTimeToFileTime(
            ref SYSTEMTIME lpSystemTime,
            out System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime);

        /// <summary>
        /// Converts a DateTime to file time format.
        /// </summary>
        /// <param name="datetime">DateTime structure</param>
        /// <returns>System.Runtime.InteropServices.ComTypes.FILETIME structure</returns>
        public static System.Runtime.InteropServices.ComTypes.FILETIME DateTimeToFileTime(DateTime datetime) {
            SYSTEMTIME st = new SYSTEMTIME();
            st.wYear = (ushort)datetime.Year;
            st.wMonth = (ushort)datetime.Month;
            st.wDay = (ushort)datetime.Day;
            st.wHour = (ushort)datetime.Hour;
            st.wMinute = (ushort)datetime.Minute;
            st.wSecond = (ushort)datetime.Second;
            st.wMilliseconds = (ushort)datetime.Millisecond;
            System.Runtime.InteropServices.ComTypes.FILETIME filetime;
            SystemTimeToFileTime(ref st, out filetime);
            return filetime;

        }
        //compares two file times.
        [DllImport("Kernel32.dll")]
        public static extern int CompareFileTime([In] ref System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime1, [In] ref System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime2);
    }
}
