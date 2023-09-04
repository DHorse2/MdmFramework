using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;
using System.Text;

namespace Mdm.Oss.WinUtil {
    #region Windows Event HookTypeIs
    /// <summary>
	/// Windows Event HookTypeIs used to manage the seven types of hooks.
    /// CALLWNDPROC 1, CBT 2, DEBUG 3, GETMESSAGE 4, KEYBOARD 5, MOUSE 6, MSGFILTER 7
    /// </summary>
	public enum HookTypeIs : int { // int
		WM_NULL         = 0x0000,
        IDM_CALLWNDPROC = 0x0001,
        IDM_CBT         = 0x0002,
        IDM_DEBUG       = 0x0003,
        IDM_GETMESSAGE  = 0x0004,
        IDM_KEYBOARD    = 0x0005,
        IDM_MOUSE       = 0x0006,
        IDM_MSGFILTER   = 0x0007
    }
    #endregion

    #region PeekMessage() Options
    /// <summary>
	/// PeekMessage() Options
    /// </summary>
	public enum PeekMessageOptionIs : int { // int
  PM_NOREMOVE         = 0x0000
, PM_REMOVE           = 0x0001
, PM_NOYIELD          = 0x0002
// #if(WINVER >= = 0x0500)
      // , PM_QS_INPUT         // (QS_INPUT << 16)
      // , PM_QS_POSTMESSAGE   // ((QS_POSTMESSAGE | QS_HOTKEY | QS_TIMER) << 16)
      // , PM_QS_PAINT         // (QS_PAINT << 16)
      // , PM_QS_SENDMESSAGE   // (QS_SENDMESSAGE << 16)
// #endif /* WINVER >= = 0x0500 */
    }
    #endregion
    #region Message Filter Proc Codes
    /*
 * WH_MSGFILTER Filter Proc Codes
 */
    public enum MsgFilterCodes : int {
        MSGF_DIALOGBOX = 0,
        MSGF_MESSAGEBOX = 1,
        MSGF_MENU = 2,
        MSGF_SCROLLBAR = 5,
        MSGF_NEXTWINDOW = 6,
        MSGF_MAX = 8, // unused
        MSGF_USER = 4096
    }
    #endregion
    #region HookCodes
/*
 * SetWindowsHook() codes
 */
        public enum SetHookCodes : int {

  WH_MIN              = (-1)
, WH_MSGFILTER        = (-1)
, WH_JOURNALRECORD    = 0
, WH_JOURNALPLAYBACK  = 1
, WH_KEYBOARD         = 2
, WH_GETMESSAGE       = 3
, WH_CALLWNDPROC      = 4
, WH_CBT              = 5
, WH_SYSMSGFILTER     = 6
, WH_MOUSE            = 7
// #if defined(_WIN32_WINDOWS)
, WH_HARDWARE         = 8
// #endif
, WH_DEBUG            = 9
, WH_SHELL           = 10
, WH_FOREGROUNDIDLE  = 11
// #if(WINVER >= 0x0400)
, WH_CALLWNDPROCRET  = 12
// #endif /* WINVER >= 0x0400 */

// #if (_WIN32_WINNT >= 0x0400)
, WH_KEYBOARD_LL     = 13
, WH_MOUSE_LL        = 14
// #endif // (_WIN32_WINNT >= 0x0400)
        }

/*
 * Hook Codes
 */
        public enum HookCodes : int {

  HC_ACTION           = 0
, HC_GETNEXT          = 1
, HC_SKIP             = 2
, HC_NOREMOVE         = 3
, HC_NOREM            = HC_NOREMOVE
, HC_SYSMODALON       = 4
, HC_SYSMODALOFF      = 5
        }
/*
 * CBT Hook Codes
 */
        public enum CbtHookCodes : int {

  HCBT_MOVESIZE       = 0
, HCBT_MINMAX         = 1
, HCBT_QS             = 2
, HCBT_CREATEWND      = 3
, HCBT_DESTROYWND     = 4
, HCBT_ACTIVATE       = 5
, HCBT_CLICKSKIPPED   = 6
, HCBT_KEYSKIPPED     = 7
, HCBT_SYSCOMMAND     = 8
, HCBT_SETFOCUS       = 9
        }
#endregion
    /// <summary>
    /// Windows Hooks Handler
    /// </summary>
    class Win32HookCs {

        public int WH_MINHOOK = h.WH_MIN;
        public int WH_MAXHOOK = h.WH_MAX;
            
            [DllImport("User32.dll", CharSet = CharSet.Auto)]
        //public static extern IntPtr SetWindowsHookEx(
        //    int idHook,
        //    IntPtr lpfn,
        //    IntPtr hMod,
        //    UInt32 dwThreadId);
        public static extern h.HHOOK SetWindowsHookEx(
                [In] int idHook,
                [MarshalAs(UnmanagedType.FunctionPtr)] Delegate lpfn,
                // [In] h.HOOKPROC lpfn,
                [In] h.HINSTANCE hMod,
                [In] h.DWORD dwThreadId);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
            public static extern void UnhookWindowsHookEx([In] h.HHOOK hhk);
            // BOOL UnhookWindowsHookEx(HHOOK hhk);
        //UnhookWindowsHookEx(HookDataArr[index].hhook);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern h.LRESULT DefWindowProc(
            [In] h.HWND hWnd, 
            [In] h.UINT Msg,
            [In] h.WPARAM wParam,
            [In] h.LPARAM lParam);
        // LRESULT DefWindowProc(HWND hWnd, UINT Msg, WPARAM wParam, LPARAM lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern h.LRESULT CallNextHookEx(
            [In] h.HHOOK hhk,
            [In] int nCode,
            [In] h.WPARAM wParam,
            [In] h.LPARAM lParam);
        // LRESULT CallNextHookEx(HHOOK hhk, int nCode, WPARAM wParam, LPARAM lParam);
        
        private const int NUMHOOKS = 7;
 
        // Global variables 
 
    public struct HookDataDef { 
        public int nType;
        public HookTypeDel hkprc;
        // public h.HOOKPROC hkprc;
        public h.HHOOK hhook;
        public bool HookIsInstalled;
        public Int32 MsgInstanceCount;
    }

    public static HookDataDef HookData; 
 
    public static HookDataDef[] HookDataArr = new HookDataDef[NUMHOOKS];
    public static Int32[] MsgInstanceCount = new Int32[NUMHOOKS];
    public static IntPtr MenuHandle; // HMENU

    public h.HWND hwndMain;

    public delegate h.LRESULT HookTypeDel(int nCode, h.WPARAM wParam, h.LPARAM lParam);
 
    public Int32 MainWndProc(
        h.HWND hwndMainPassed,
        h.UINT uMsg,
        h.WPARAM wParam,
        h.LPARAM lParam
        ) {
        //
        hwndMain = hwndMainPassed;
        int index; 
 
    switch ((MsgTypeIs)(uint)uMsg) 
    { 
        case MsgTypeIs.WM_CREATE: 
 
            // Save the menu handle. 
            MenuHandle = Win32Window.GetMenu(hwndMain); 
 
            // Initialize structures with hook data. The menu-item 
            // identifiers are defined as 0 through 6 in the 
            // header file. They can be used to identify array 
            // elements both here and during the WM_COMMAND 
            // message. 


 
            HookDataArr[(int)HookTypeIs.IDM_CALLWNDPROC].nType = (int)SetHookCodes.WH_CALLWNDPROC; 
            HookDataArr[(int)HookTypeIs.IDM_CALLWNDPROC].hkprc = CallWndProc; 
            HookDataArr[(int)HookTypeIs.IDM_CBT].nType = (int)SetHookCodes.WH_CBT; 
            HookDataArr[(int)HookTypeIs.IDM_CBT].hkprc = CBTProc; 
            HookDataArr[(int)HookTypeIs.IDM_DEBUG].nType = (int)SetHookCodes.WH_DEBUG; 
            HookDataArr[(int)HookTypeIs.IDM_DEBUG].hkprc = DebugProc; 
            HookDataArr[(int)HookTypeIs.IDM_GETMESSAGE].nType = (int)SetHookCodes.WH_GETMESSAGE; 
            HookDataArr[(int)HookTypeIs.IDM_GETMESSAGE].hkprc = GetMsgProc; 
            HookDataArr[(int)HookTypeIs.IDM_KEYBOARD].nType = (int)SetHookCodes.WH_KEYBOARD; 
            HookDataArr[(int)HookTypeIs.IDM_KEYBOARD].hkprc = KeyboardProc; 
            HookDataArr[(int)HookTypeIs.IDM_MOUSE].nType = (int)SetHookCodes.WH_MOUSE; 
            HookDataArr[(int)HookTypeIs.IDM_MOUSE].hkprc = MouseProc; 
            HookDataArr[(int)HookTypeIs.IDM_MSGFILTER].nType = (int)SetHookCodes.WH_MSGFILTER; 
            HookDataArr[(int)HookTypeIs.IDM_MSGFILTER].hkprc = MessageProc; 
 
            // Initialize all flags in the array to FALSE. 
            // memset(HookDataArrFlag, false, sizeof(HookDataArrFlag)); 
            // HookDataArr.SetValue(false, 0, 7) .HookIsInstalled;

            return (Int32)0; 
 
        case MsgTypeIs.WM_COMMAND: 
            switch (Win32General.LOWORD((uint)wParam)) 
            { 
                 // The user selected a hook command from the menu. 
 
                case (ushort)HookTypeIs.IDM_CALLWNDPROC: 
                case (ushort)HookTypeIs.IDM_CBT: 
                case (ushort)HookTypeIs.IDM_DEBUG: 
                case (ushort)HookTypeIs.IDM_GETMESSAGE: 
                case (ushort)HookTypeIs.IDM_KEYBOARD: 
                case (ushort)HookTypeIs.IDM_MOUSE: 
                case (ushort)HookTypeIs.IDM_MSGFILTER: 
 
                    // Use the menu-item identifier as an index 
                    // into the array of structures with hook data. 

                    index = Win32General.LOWORD((UInt32)wParam); 
 
                    // If the selected type of hook procedure isn't 
                    // installed yet, install it and check the 
                    // associated menu item. 

                    if (!HookDataArr[index].HookIsInstalled) 
                    { 
                        HookDataArr[index].hhook = SetWindowsHookEx( 
                            HookDataArr[index].nType, 
                            HookDataArr[index].hkprc,
                            (h.HINSTANCE) null, 
                            Win32General.GetCurrentThreadId());
                        //
                        Win32Window.CheckMenuItem(MenuHandle, (uint)index, 
                            (uint)(Win32Window.MenuFlagIs.MF_BYCOMMAND | Win32Window.MenuFlagIs.MF_CHECKED));
                        //
                        HookDataArr[index].HookIsInstalled = true; 
                    } 
 
                    // If the selected type of hook procedure is 
                    // already installed, remove it and remove the 
                    // check mark from the associated menu item. 
 
                    else 
                    { 
                        UnhookWindowsHookEx(HookDataArr[index].hhook);
                        //
                        Win32Window.CheckMenuItem(MenuHandle, 
                            (uint)index, 
                            (uint)(Win32Window.MenuFlagIs.MF_BYCOMMAND | Win32Window.MenuFlagIs.MF_UNCHECKED)); 
                        //
                        HookDataArr[index].HookIsInstalled = false; 
                    } 
 
                default:
                    return ((int)DefWindowProc(hwndMain, uMsg, wParam, 
                        lParam)); 
            } 
            break; 
 
            //
            // Process other messages. 
            //
 
        default:
            return DefWindowProc(hwndMain, uMsg, wParam, lParam); 
    } 
    // return null; 
} 
 
/**************************************************************** 
  WH_CALLWNDPROC hook procedure 
 ****************************************************************/

    h.LRESULT CallWndProc(int nCode, h.WPARAM wParam, h.LPARAM lParam) { 
    // WINAPI 

    h.CHAR[] szCWPBuf = new h.CHAR[256]; 
    h.CHAR[] szMsg = new h.CHAR[16]; 
    h.HDC hdc; 
    h.size_t cch;
    h.HRESULT hResult;

    if (nCode < 0)  // do not process message 
        return CallNextHookEx(HookDataArr[(int)HookTypeIs.IDM_CALLWNDPROC].hhook, nCode, 
                wParam, lParam); 
 
    // Call an application-defined function that converts a message 
    // constant to a string and copies it to a buffer. 
 
    LookUpTheMessage((PMSG) lParam, szMsg); 
 
    hdc = GetDC(hwndMain); 
 
    switch ((HookCodes)nCode) 
    { 
        case HookCodes.HC_ACTION:
            hResult = StringCchPrintf(szCWPBuf, 256/sizeof(char),  
               "CALLWNDPROC - tsk: %ld, msg: %s, %d times   ",
                wParam, szMsg, HookDataArr[(int)HookTypeIs.IDM_CALLWNDPROC].MsgInstanceCount++);
            if (Win32General.FAILED(hResult))
            {
            // TODO: writer error handler
            }
            hResult = StringCchLength(szCWPBuf, 256/sizeof(char), &cch);
            if (Win32General.FAILED(hResult))
            {
            // TODO: write error handler
            } 
            TextOut(hdc, 2, 15, szCWPBuf, cch); 
            break; 
 
        default: 
            break; 
    } 
 
    ReleaseDC(hwndMain, hdc);
    return CallNextHookEx(HookDataArr[(int)HookTypeIs.IDM_CALLWNDPROC].hhook, nCode, 
        wParam, lParam); 
} 
 
/**************************************************************** 
  WH_GETMESSAGE hook procedure 
 ****************************************************************/

    public h.LRESULT GetMsgProc(int nCode, h.WPARAM wParam, h.LPARAM lParam) { 
    // CALLBACK 

    h.CHAR[] szMSGBuf = new h.CHAR[256]; 
    h.CHAR[] szRem = new h.CHAR[16]; 
    h.CHAR[] szMsg = new h.CHAR[16];
    h.HDC hdc;
    int MsgInstanceCount = 0;
    h.size_t cch; 
    h.HRESULT hResult;
 
    if (nCode < 0) // do not process message 
        return CallNextHookEx(HookDataArr[(int)HookTypeIs.IDM_GETMESSAGE].hhook, nCode, 
            wParam, lParam); 
 
    switch ((HookCodes)nCode) 
    { 
        case HookCodes.HC_ACTION: 
            switch ((PeekMessageOptionIs)(int)wParam) 
            { 
                case PeekMessageOptionIs.PM_REMOVE:
                    hResult = Win32General.StringCchCopy(szRem, 16/sizeof(char), "PM_REMOVE");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
                    break; 
 
                case PeekMessageOptionIs.PM_NOREMOVE:
                    hResult = Win32General.StringCchCopy(szRem, 16/sizeof(char), "PM_NOREMOVE");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
                    break; 
 
                default:
                    hResult = Win32General.StringCchCopy(szRem, 16/sizeof(char), "Unknown");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
                    break; 
            } 
 
            // Call an application-defined function that converts a 
            // message constant to a string and copies it to a 
            // buffer. 
 
            LookUpTheMessage((PMSG) lParam, szMsg); 
 
            hdc = GetDC(hwndMain);
            hResult = StringCchPrintf(szMSGBuf, 256/sizeof(char), 
                "GETMESSAGE - wParam: %s, msg: %s, %d times   ",
                szRem, szMsg, HookDataArr[(int)HookTypeIs.IDM_GETMESSAGE].MsgInstanceCount++);
            if (Win32General.FAILED(hResult))
            {
            // TODO: write error handler
            }
            hResult = StringCchLength(szMSGBuf, 256/sizeof(char), &cch);
            if (Win32General.FAILED(hResult))
            {
            // TODO: write error handler
            } 
            TextOut(hdc, 2, 35, szMSGBuf, cch); 
            break; 
 
        default: 
            break; 
    } 
 
    ReleaseDC(hwndMain, hdc);
    return CallNextHookEx(HookDataArr[(int)HookTypeIs.IDM_GETMESSAGE].hhook, nCode, 
        wParam, lParam); 
} 
 
/**************************************************************** 
  WH_DEBUG hook procedure 
 ****************************************************************/

    h.LRESULT DebugProc(int nCode, h.WPARAM wParam, h.LPARAM lParam) { 
    // CALLBACK 
    h.CHAR[] szBuf = new h.CHAR[128]; 
    h.HDC hdc; 
    int MsgInstanceCount = 0; 
    h.size_t cch; 
    h.HRESULT hResult;
 
    if (nCode < 0)  // do not process message 
        return CallNextHookEx(HookDataArr[(int)HookTypeIs.IDM_DEBUG].hhook, nCode, 
            wParam, lParam); 
 
    hdc = GetDC(hwndMain); 
 
    switch ((HookCodes)nCode) 
    { 
        case HookCodes.HC_ACTION:
            hResult = StringCchPrintf(szBuf, 128/sizeof(char),   
                "DEBUG - nCode: %d, tsk: %ld, %d times   ",
                nCode, wParam, HookDataArr[(int)HookTypeIs.IDM_DEBUG].MsgInstanceCount++);
            if (Win32General.FAILED(hResult))
            {
            // TODO: write error handler
            }
            hResult = StringCchLength(szBuf, 128/sizeof(char), &cch);
            if (Win32General.FAILED(hResult))
            {
            // TODO: write error handler
            } 
            TextOut(hdc, 2, 55, szBuf, cch); 
            break; 
 
        default: 
            break; 
    } 
 
    ReleaseDC(hwndMain, hdc);
    return CallNextHookEx(HookDataArr[(int)HookTypeIs.IDM_DEBUG].hhook, nCode, wParam, 
        lParam); 
} 
 
/**************************************************************** 
  WH_CBT hook procedure 
 ****************************************************************/

    h.LRESULT CBTProc(int nCode, h.WPARAM wParam, h.LPARAM lParam) { 
    // CALLBACK 

    h.CHAR[] szBuf = new h.CHAR[128]; 
    h.CHAR[] szCode = new h.CHAR[128]; 
    h.HDC hdc; 
    h.size_t cch; 
    h.HRESULT hResult;
 
    if (nCode < 0)  // do not process message 
        return CallNextHookEx(HookDataArr[(int)HookTypeIs.IDM_CBT].hhook, nCode, wParam, 
            lParam); 
 
    hdc = GetDC(hwndMain); 
 
    switch ((CbtHookCodes)nCode) 
    { 
        case CbtHookCodes.HCBT_ACTIVATE:
            hResult = Win32General.StringCchCopy(szCode, 128/sizeof(char), "HCBT_ACTIVATE");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
            break; 
 
        case CbtHookCodes.HCBT_CLICKSKIPPED:
            hResult = Win32General.StringCchCopy(szCode, 128/sizeof(char), "HCBT_CLICKSKIPPED");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
            break; 
 
        case CbtHookCodes.HCBT_CREATEWND:
            hResult = Win32General.StringCchCopy(szCode, 128/sizeof(char), "HCBT_CREATEWND");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
            break; 
 
        case CbtHookCodes.HCBT_DESTROYWND:
            hResult = Win32General.StringCchCopy(szCode, 128/sizeof(char), "HCBT_DESTROYWND");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
            break; 
 
        case CbtHookCodes.HCBT_KEYSKIPPED:
            hResult = Win32General.StringCchCopy(szCode, 128/sizeof(char), "HCBT_KEYSKIPPED");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
            break; 
 
        case CbtHookCodes.HCBT_MINMAX:
            hResult = Win32General.StringCchCopy(szCode, 128/sizeof(char), "HCBT_MINMAX");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
            break; 
 
        case CbtHookCodes.HCBT_MOVESIZE:
            hResult = Win32General.StringCchCopy(szCode, 128/sizeof(char), "HCBT_MOVESIZE");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
            break; 
 
        case CbtHookCodes.HCBT_QS:
            hResult = Win32General.StringCchCopy(szCode, 128/sizeof(char), "HCBT_QS");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
            break; 
 
        case CbtHookCodes.HCBT_SETFOCUS:
            hResult = Win32General.StringCchCopy(szCode, 128/sizeof(char), "HCBT_SETFOCUS");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
            break; 
 
        case CbtHookCodes.HCBT_SYSCOMMAND:
            hResult = Win32General.StringCchCopy(szCode, 128/sizeof(char), "HCBT_SYSCOMMAND");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
            break; 
 
        default:
            hResult = Win32General.StringCchCopy(szCode, 128/sizeof(char), "Unknown");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
            break; 
    } 
    hResult = StringCchPrintf(szBuf, 128/sizeof(char), "CBT -  nCode: %s, tsk: %ld, %d times   ",
        szCode, wParam, HookDataArr[CBT].MsgInstanceCount++);
    if (Win32General.FAILED(hResult))
    {
    // TODO: write error handler
    }
    hResult = StringCchLength(szBuf, 128/sizeof(char), &cch);
    if (Win32General.FAILED(hResult))
    {
    // TODO: write error handler
    } 
    TextOut(hdc, 2, 75, szBuf, cch); 
    ReleaseDC(hwndMain, hdc);
    return CallNextHookEx(HookDataArr[(int)HookTypeIs.IDM_CBT].hhook, nCode, wParam, 
        lParam); 
} 
 
/**************************************************************** 
  WH_MOUSE hook procedure 
 ****************************************************************/

    h.LRESULT MouseProc(int nCode, h.WPARAM wParam, h.LPARAM lParam) { 
    // CALLBACK 

    h.CHAR[] szBuf = new h.CHAR[128]; 
    h.CHAR[] szMsg = new h.CHAR[16]; 
    h.HDC hdc; 
    h.size_t cch; 
    h.HRESULT hResult;
 
    if (nCode < 0)  // do not process the message 
        return CallNextHookEx(HookDataArr[(int)HookTypeIs.IDM_MOUSE].hhook, nCode, 
            wParam, lParam); 
 
    // Call an application-defined function that converts a message 
    // constant to a string and copies it to a buffer. 
 
    LookUpTheMessage((PMSG) lParam, szMsg); 
 
    hdc = GetDC(hwndMain);
    hResult = StringCchPrintf(szBuf, 128/sizeof(char), 
        "MOUSE - nCode: %d, msg: %s, x: %d, y: %d, %d times   ", 
        nCode, szMsg, LOWORD(lParam), HIWORD(lParam), HookDataArr[(int)HookTypeIs.IDM_MOUSE].MsgInstanceCount++); 
    if (Win32General.FAILED(hResult))
    {
    // TODO: write error handler
    }
    hResult = StringCchLength(szBuf, 128/sizeof(char), &cch);
    if (Win32General.FAILED(hResult))
    {
    // TODO: write error handler
    }
    TextOut(hdc, 2, 95, szBuf, cch); 
    ReleaseDC(hwndMain, hdc); 
    return CallNextHookEx(HookDataArr[MOUSE].hhook, nCode, wParam, 
        lParam); 
} 
 
/**************************************************************** 
  WH_KEYBOARD hook procedure 
 ****************************************************************/

    h.LRESULT KeyboardProc(int nCode, h.WPARAM wParam, h.LPARAM lParam) { 
    // CALLBACK 

    h.CHAR[] szBuf = new h.CHAR[128]; 
    h.HDC hdc; 
    h.size_t cch; 
    h.HRESULT hResult;
 
    if (nCode < 0)  // do not process message 
        return CallNextHookEx(HookDataArr[(int)HookTypeIs.IDM_KEYBOARD].hhook, nCode, 
            wParam, lParam); 
 
    hdc = GetDC(hwndMain);
    hResult = StringCchPrintf(szBuf, 128/sizeof(char), "KEYBOARD - nCode: %d, vk: %d, %d times ", nCode, wParam, HookDataArr[KEYBOARD].MsgInstanceCount++);
    if (Win32General.FAILED(hResult))
    {
    // TODO: write error handler
    } 
    hResult = StringCchLength(szBuf, 128/sizeof(char), &cch);
    if (Win32General.FAILED(hResult))
    {
    // TODO: write error handler
    } 
    TextOut(hdc, 2, 115, szBuf, cch); 
    ReleaseDC(hwndMain, hdc);
    return CallNextHookEx(HookDataArr[(int)HookTypeIs.IDM_KEYBOARD].hhook, nCode, wParam, 
        lParam); 
} 
 
/**************************************************************** 
  WH_MSGFILTER hook procedure 
 ****************************************************************/

    h.LRESULT MessageProc(int nCode, h.WPARAM wParam, h.LPARAM lParam) { 
    // CALLBACK 

    h.CHAR[] szBuf = new h.CHAR[128]; 
    h.CHAR[] szMsg = new h.CHAR[16]; 
    h.CHAR[] szCode = new h.CHAR[32]; 
    h.HDC hdc; 
    h.size_t cch; 
    h.HRESULT hResult;
 
    if (nCode < 0)  // do not process message 
        return CallNextHookEx(HookDataArr[(int)HookTypeIs.IDM_MSGFILTER].hhook, nCode, 
            wParam, lParam);

    switch ((MsgFilterCodes)nCode) 
    {
        case MsgFilterCodes.MSGF_DIALOGBOX:
            hResult = Win32General.StringCchCopy(szCode, 32/sizeof(char), "MSGF_DIALOGBOX");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
            break;

        case MsgFilterCodes.MSGF_MENU:
            hResult = Win32General.StringCchCopy(szCode, 32/sizeof(char), "MSGF_MENU");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
            break;

        case MsgFilterCodes.MSGF_SCROLLBAR:
            hResult = Win32General.StringCchCopy(szCode, 32/sizeof(char), "MSGF_SCROLLBAR");
                    if (Win32General.FAILED(hResult))
                    {
                    // TODO: write error handler
                    } 
            break; 
 
        default:
            hResult = StringCchPrintf(szCode, 128/sizeof(char), "Unknown: %d", nCode);
    if (Win32General.FAILED(hResult))
    {
    // TODO: write error handler
    }
            break; 
    } 
 
    // Call an application-defined function that converts a message 
    // constant to a string and copies it to a buffer. 
 
    LookUpTheMessage((PMSG) lParam, szMsg); 
 
    hdc = GetDC(hwndMain);
    hResult = StringCchPrintf(szBuf, 128/sizeof(char),  
        "MSGFILTER  nCode: %s, msg: %s, %d times    ",
        szCode, szMsg, HookDataArr[(int)HookTypeIs.IDM_MSGFILTER].MsgInstanceCount++);
    if (Win32General.FAILED(hResult))
    {
    // TODO: write error handler
    } 
    hResult = StringCchLength(szBuf, 128/sizeof(char), &cch);
    if (Win32General.FAILED(hResult))
    {
    // TODO: write error handler
    } 
    TextOut(hdc, 2, 135, szBuf, cch); 
    ReleaseDC(hwndMain, hdc);
    return CallNextHookEx(HookDataArr[(int)HookTypeIs.IDM_MSGFILTER].hhook, nCode, 
        wParam, lParam); 
} 

    }
}
