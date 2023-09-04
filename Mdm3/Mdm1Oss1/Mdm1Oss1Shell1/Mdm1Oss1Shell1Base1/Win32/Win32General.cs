using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;
using System.Text;

namespace Mdm.Oss.WinUtil {
    /// <summary>
    /// Windows General, miscellaneous, utility and cross-cutting functions
    /// </summary>
    public class Win32GeneralDef {

        // Use interop to call the method necessary
        // to clean up the unmanaged resource.
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public extern static Boolean CloseHandle(IntPtr handle);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern UInt16 LOWORD([In] UInt32 dwValue);
        // WORD LOWORD(DWORD dwValue);

        // [DllImport("User32.dll", CharSet = CharSet.Auto)]
        // public static extern         
        // sizeof unary-expression
        // sizeof ( type-name )


        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern Types.h.DWORD GetCurrentThreadId();

        public static bool FAILED(Types.h.HRESULT Status) {
            // #define FAILED(Status)     ((HRESULT)(Status)<0)
            if ((int)Status < 0) {
                return true;
            } else { return false; }
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern Types.h.HRESULT StringCchLength(
            [In] Types.h.LPCTSTR psz,
            [In] Types.h.size_t cchMax,
            [Out, MarshalAs(UnmanagedType.LPStr)] String pcch);
        // size_t* pcch 
        // S_OK 
        // STRSAFE_E_INVALID_PARAMETER
        // STRSAFE_MAX_CCH

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern Types.h.HRESULT StringCbLength(
            [In] Types.h.LPCTSTR psz,
            [In] Types.h.size_t cbMax,
            [Out, MarshalAs(UnmanagedType.LPStr)] String pcb);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern Types.h.HRESULT StringCchCopy(
            [Out] Types.h.LPTSTR pszDest,
            [In] Types.h.size_t cchDest,
            [In] Types.h.LPCTSTR pszSrc
        );



        // [In, MarshalAs( UnmanagedType.X )]
        // [Out, MarshalAs( UnmanagedType.X )]
    }
}
