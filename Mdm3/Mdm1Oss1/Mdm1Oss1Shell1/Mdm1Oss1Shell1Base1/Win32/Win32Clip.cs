using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;
using System.Text;

namespace Mdm.Oss.WinUtil {
    /// <summary>
    /// COM functions that deal with the Windows Clipboard
    /// </summary>
    public class Win32ClipDef {
        /// <summary>
        /// Windows Event MessageTypeIs sent to the WindowProc
        /// </summary>

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer([In] IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(
            [In] IntPtr hWndRemove,  // handle to window to remove
            [In] IntPtr hWndNewNext  // handle to next window
            );
        
        // DWORD GetClipboardSequenceNumber(VOID);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern UInt32 GetClipboardSequenceNumber();
    }
}

