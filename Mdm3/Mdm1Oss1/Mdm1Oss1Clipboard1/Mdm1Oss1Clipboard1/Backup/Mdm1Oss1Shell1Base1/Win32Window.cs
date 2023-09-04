using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;
using System.Text;

namespace Mdm.Oss.WinUtil {
    /// <summary>
    /// COM functions that deal with Windows and Controls
    /// </summary>
    class Win32Window {

        #region Menu Section
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetMenu([In] Types.h.HWND hWnd);
        // HMENU GetMenu(HWND hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 CheckMenuItem(
            [In] IntPtr MenuHandle,
            [In] UInt32 index,
            [In] UInt32 MF_Value); 
        // DWORD CheckMenuItem(HMENU hmenu, UINT uIDCheckItem, UINT uCheck);
        // CheckMenuItem(MenuHandle, index, MF_BYCOMMAND | MF_CHECKED); 


        #region Menu Flags
        /// <summary>
        /// <para> Menu flags for Add / Check / EnableMenuItem().</para>
        /// </summary>
        /// <remarks>
        /// ;win40  -- A lot of MF_* flags have been renamed as MFT_* and MFS_* flags
        /// </remarks>
        public enum MenuFlagIs : uint {

            MF_INSERT = 0x00000000,
            MF_CHANGE = 0x00000080,
            MF_APPEND = 0x00000100,
            MF_DELETE = 0x00000200,
            MF_REMOVE = 0x00001000,

            MF_BYCOMMAND = 0x00000000,
            MF_BYPOSITION = 0x00000400,

            MF_SEPARATOR = 0x00000800,

            MF_ENABLED = 0x00000000,
            MF_GRAYED = 0x00000001,
            MF_DISABLED = 0x00000002,

            MF_UNCHECKED = 0x00000000,
            MF_CHECKED = 0x00000008,
            MF_USECHECKBITMAPS = 0x00000200,

            MF_STRING = 0x00000000,
            MF_BITMAP = 0x00000004,
            MF_OWNERDRAW = 0x00000100,

            MF_POPUP = 0x00000010,
            MF_MENUBARBREAK = 0x00000020,
            MF_MENUBREAK = 0x00000040,

            MF_UNHILITE = 0x00000000,
            MF_HILITE = 0x00000080,

            // #if(WINVER >= = 0x0400)
            MF_DEFAULT = 0x00001000,
            // #endif /* WINVER >= = 0x0400 */
            MF_SYSMENU = 0x00002000,
            MF_HELP = 0x00004000,
            // #if(WINVER >= = 0x0400)
            MF_RIGHTJUSTIFY = 0x00004000,
            // #endif /* WINVER >= = 0x0400 */

            MF_MOUSESELECT = 0x00008000,
            // #if(WINVER >= = 0x0400)
            MF_END = 0x00000080
            /* Note: Obsolete -- only used by old RES files */
            // #endif /* WINVER >= = 0x0400 */
        }
#endregion

#endregion
    }
}
