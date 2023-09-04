#include "stdafx.h"
#include "Win32HookMgnt.h"

using namespace System;


namespace Mdm {
    namespace Oss {
        namespace WinUtil {

        // declare a delegate
public delegate LRESULT HookProcDel(int nCode, WPARAM wParam, LPARAM lParam);

            // class Win32Hook {
            // Global variables 
                HWND hwndMain;
                _HookDataDef HookDataArr[NUMHOOKS]; 
                int HookIndex; 
            

            LRESULT WINAPI MainWndProc(
                HWND hwndMainPassed, 
                UINT uMsg, 
                WPARAM wParam, 
                LPARAM lParam) 
            { 
                static BOOL HookIsSet[NUMHOOKS]; 
                static HMENU hmenu;
                hwndMain = hwndMainPassed;
                HookProcDel^ HookProcD;

                switch (uMsg) 
                { 
                case WM_CREATE: 
                    // Save the menu handle. 
                    hmenu = GetMenu(hwndMain); 
                    // Initialize structures with hook data. The menu-item 
                    // identifiers are defined as 0 through 6 in the 
                    // header file. They can be used to identify array 
                    // elements both here and during the WM_COMMAND 
                    // message. 
                    // &Mdm::Oss::WinUtil::
                    
                    // Win32Hook* temp = this;
                    // HookProcD = this->CallWndProc;
                    // HookProcD = this->CallWndProc;
                    // HookProcD = &Mdm::Oss::WinUtil::CallWndProc;
                    // HookProcD = gcnew HookProcDel(temp, &Mdm::Oss::WinUtil::CallWndProc);
                    // HookProcD = &Mdm::Oss::WinUtil::CBTProc;
                    // HookProcD = &Mdm::Oss::WinUtil::CallWndProc;

                    HookDataArr[IDM_CALLWNDPROC].nType = WH_CALLWNDPROC; 
                    HookDataArr[IDM_CALLWNDPROC].hkprc = &Mdm::Oss::WinUtil::CallWndProc;
                    HookDataArr[IDM_CALLWNDPROC].HookIsSet = FALSE;
                    HookDataArr[IDM_CBT].nType = WH_CBT; 
                    HookDataArr[IDM_CBT].hkprc = &Mdm::Oss::WinUtil::CBTProc;
                    HookDataArr[IDM_CBT].HookIsSet = FALSE;
                    HookDataArr[IDM_DEBUG].nType = WH_DEBUG; 
                    HookDataArr[IDM_DEBUG].hkprc = &Mdm::Oss::WinUtil::DebugProc; 
                    HookDataArr[IDM_DEBUG].HookIsSet = FALSE;
                    HookDataArr[IDM_GETMESSAGE].nType = WH_GETMESSAGE; 
                    HookDataArr[IDM_GETMESSAGE].hkprc = &Mdm::Oss::WinUtil::GetMsgProc; 
                    HookDataArr[IDM_GETMESSAGE].HookIsSet = FALSE;
                    HookDataArr[IDM_KEYBOARD].nType = WH_KEYBOARD; 
                    HookDataArr[IDM_KEYBOARD].hkprc = &Mdm::Oss::WinUtil::KeyboardProc; 
                    HookDataArr[IDM_KEYBOARD].HookIsSet = FALSE;
                    HookDataArr[IDM_MOUSE].nType = WH_MOUSE; 
                    HookDataArr[IDM_MOUSE].hkprc = &Mdm::Oss::WinUtil::MouseProc;
                    HookDataArr[IDM_MOUSE].HookIsSet = FALSE;
                    HookDataArr[IDM_MSGFILTER].nType = WH_MSGFILTER; 
                    HookDataArr[IDM_MSGFILTER].hkprc = &Mdm::Oss::WinUtil::MessageProc; 
                    HookDataArr[IDM_MSGFILTER].HookIsSet = FALSE;
                    // Initialize all flags in the array to FALSE. 
                    // memset(HookIsSet, FALSE, sizeof(HookIsSet)); 
                    return 0; 

                case WM_COMMAND: 
                    switch (LOWORD(wParam)) 
                    { 
                        // The user selected a hook command from the menu. 
                    case IDM_CALLWNDPROC: 
                    case IDM_CBT: 
                    case IDM_DEBUG: 
                    case IDM_GETMESSAGE: 
                    case IDM_KEYBOARD: 
                    case IDM_MOUSE: 
                    case IDM_MSGFILTER: 
                        // Use the menu-item identifier as an index 
                        // into the array of structures with hook data. 
                        HookIndex = LOWORD(wParam); 
                        // If the selected type of hook procedure isn't 
                        // installed yet, install it and check the 
                        // associated menu item. 
                        if (!HookDataArr[HookIndex].HookIsSet) 
                        { 
                            //
                            HookDataArr[HookIndex].hhook = SetWindowsHookEx( 
                                HookDataArr[HookIndex].nType, 
                                HookDataArr[HookIndex].hkprc, 
                                (HINSTANCE) NULL, GetCurrentThreadId()); 
                            //
                            //HHOOK SetWindowsHookEx(          int idHook,
                            //    HOOKPROC lpfn,
                            //    HINSTANCE hMod,
                            //    DWORD dwThreadId
                            //);
                            //
                            CheckMenuItem(hmenu, HookIndex, 
                                MF_BYCOMMAND | MF_CHECKED); 
                            HookDataArr[HookIndex].HookIsSet = TRUE; 
                        } 
                        // If the selected type of hook procedure is 
                        // already installed, remove it and remove the 
                        // check mark from the associated menu item. 
                        else 
                        { 
                            UnhookWindowsHookEx(HookDataArr[HookIndex].hhook); 
                            CheckMenuItem(hmenu, HookIndex, 
                                MF_BYCOMMAND | MF_UNCHECKED); 
                            HookDataArr[HookIndex].HookIsSet = FALSE; 
                        } 
                    default: 
                        return (DefWindowProc(hwndMain, uMsg, wParam, 
                            lParam)); 
                    } 
                    break; 
                    //
                    // Process other messages. 
                    //

                default: 
                    return DefWindowProc(hwndMain, uMsg, wParam, lParam); 
                } 
                return NULL; 
            } 

            /**************************************************************** 
            WH_CALLWNDPROC hook procedure 
            ****************************************************************/ 
            LRESULT CALLBACK CallWndProc(int nCode, WPARAM wParam, LPARAM lParam) 
            { 
                // CHAR szCWPBuf[256]; 
                STRSAFE_LPWSTR szCWPBuf;
                // CHAR szMsg[16]; 
                STRSAFE_LPWSTR szMsg;
                HDC hdc; 
                static int c = 0; 
                size_t cch;
                HRESULT hResult; 

                if (nCode < 0)  // do not process message 
                    return CallNextHookEx(HookDataArr[IDM_CALLWNDPROC].hhook, nCode, 
                    wParam, lParam); 

                // Call an application-defined function that converts a message 
                // constant to a string and copies it to a buffer. 

                LookUpTheMessage((PMSG) lParam, szMsg); 

                hdc = GetDC(hwndMain); 

                switch (nCode) 
                { 
                case HC_ACTION:
                    // STRSAFE_LPWSTR
                    hResult = StringCchPrintf(szCWPBuf, 256/sizeof(TCHAR),  
                        L"CALLWNDPROC - tsk: %ld, msg: %s, %d times   ", 
                        wParam, szMsg, c++);
                    if (FAILED(hResult))
                    {
                        // TODO: writer error handler
                    }
                    hResult = StringCchLength(szCWPBuf, 256/sizeof(TCHAR), &cch);
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    TextOut(hdc, 2, 15, szCWPBuf, cch); 
                    break; 

                default: 
                    break; 
                } 

                ReleaseDC(hwndMain, hdc); 
                return CallNextHookEx(HookDataArr[IDM_CALLWNDPROC].hhook, nCode, 
                    wParam, lParam); 
            } 

            /**************************************************************** 
            WH_GETMESSAGE hook procedure 
            ****************************************************************/ 

            LRESULT CALLBACK GetMsgProc(int nCode, WPARAM wParam, LPARAM lParam) 
            { 
                // CHAR szMSGBuf[256]; 
                STRSAFE_LPWSTR szMSGBuf;
                // CHAR szRem[16]; 
                STRSAFE_LPWSTR szRem;
                // CHAR szMsg[16]; 
                STRSAFE_LPWSTR szMsg;
                HDC hdc; 
                static int c = 0; 
                size_t cch; 
                HRESULT hResult;

                if (nCode < 0) // do not process message 
                    return CallNextHookEx(HookDataArr[IDM_GETMESSAGE].hhook, nCode, 
                    wParam, lParam); 

                switch (nCode) 
                { 
                case HC_ACTION: 
                    switch (wParam) 
                    { 
                    case PM_REMOVE:
                        hResult = StringCchCopy(szRem, 16/sizeof(TCHAR), L"PM_REMOVE");
                        if (FAILED(hResult))
                        {
                            // TODO: write error handler
                        } 
                        break; 

                    case PM_NOREMOVE:
                        hResult = StringCchCopy(szRem, 16/sizeof(TCHAR), L"PM_NOREMOVE");
                        if (FAILED(hResult))
                        {
                            // TODO: write error handler
                        } 
                        break; 

                    default:
                        hResult = StringCchCopy(szRem, 16/sizeof(TCHAR), L"Unknown");
                        if (FAILED(hResult))
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
                    hResult = StringCchPrintf(szMSGBuf, 256/sizeof(TCHAR), 
                        L"GETMESSAGE - wParam: %s, msg: %s, %d times   ", 
                        szRem, szMsg, c++);
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    }
                    hResult = StringCchLength(szMSGBuf, 256/sizeof(TCHAR), &cch);
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    TextOut(hdc, 2, 35, szMSGBuf, cch); 
                    break; 

                default: 
                    break; 
                } 

                ReleaseDC(hwndMain, hdc); 
                return CallNextHookEx(HookDataArr[IDM_GETMESSAGE].hhook, nCode, 
                    wParam, lParam); 
            } 

            /**************************************************************** 
            WH_DEBUG hook procedure 
            ****************************************************************/ 

            LRESULT CALLBACK DebugProc(int nCode, WPARAM wParam, LPARAM lParam) 
            { 
                // CHAR szBuf[128]; 
                STRSAFE_LPWSTR szBuf;
                HDC hdc; 
                static int c = 0; 
                size_t cch; 
                HRESULT hResult;

                if (nCode < 0)  // do not process message 
                    return CallNextHookEx(HookDataArr[IDM_DEBUG].hhook, nCode, 
                    wParam, lParam); 

                hdc = GetDC(hwndMain); 

                switch (nCode) 
                { 
                case HC_ACTION:
                    hResult = StringCchPrintf(szBuf, 128/sizeof(TCHAR),   
                        L"DEBUG - nCode: %d, tsk: %ld, %d times   ", 
                        nCode,wParam, c++);
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    }
                    hResult = StringCchLength(szBuf, 128/sizeof(TCHAR), &cch);
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    TextOut(hdc, 2, 55, szBuf, cch); 
                    break; 

                default: 
                    break; 
                } 

                ReleaseDC(hwndMain, hdc); 
                return CallNextHookEx(HookDataArr[IDM_DEBUG].hhook, nCode, wParam, 
                    lParam); 
            } 

            /**************************************************************** 
            WH_CBT hook procedure 
            ****************************************************************/ 

            LRESULT CALLBACK CBTProc(int nCode, WPARAM wParam, LPARAM lParam) 
            { 
                // CHAR szBuf[128]; 
                STRSAFE_LPWSTR szBuf;
                // CHAR szCode[128]; 
                STRSAFE_LPWSTR szCode;
                HDC hdc; 
                static int c = 0; 
                size_t cch; 
                HRESULT hResult;

                if (nCode < 0)  // do not process message 
                    return CallNextHookEx(HookDataArr[IDM_CBT].hhook, nCode, wParam, 
                    lParam); 

                hdc = GetDC(hwndMain); 

                switch (nCode) 
                { 
                case HCBT_ACTIVATE:
                    hResult = StringCchCopy(szCode, 128/sizeof(TCHAR), L"HCBT_ACTIVATE");
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    break; 

                case HCBT_CLICKSKIPPED:
                    hResult = StringCchCopy(szCode, 128/sizeof(TCHAR), L"HCBT_CLICKSKIPPED");
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    break; 

                case HCBT_CREATEWND:
                    hResult = StringCchCopy(szCode, 128/sizeof(TCHAR), L"HCBT_CREATEWND");
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    break; 

                case HCBT_DESTROYWND:
                    hResult = StringCchCopy(szCode, 128/sizeof(TCHAR), L"HCBT_DESTROYWND");
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    break; 

                case HCBT_KEYSKIPPED:
                    hResult = StringCchCopy(szCode, 128/sizeof(TCHAR), L"HCBT_KEYSKIPPED");
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    break; 

                case HCBT_MINMAX:
                    hResult = StringCchCopy(szCode, 128/sizeof(TCHAR), L"HCBT_MINMAX");
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    break; 

                case HCBT_MOVESIZE:
                    hResult = StringCchCopy(szCode, 128/sizeof(TCHAR), L"HCBT_MOVESIZE");
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    break; 

                case HCBT_QS:
                    hResult = StringCchCopy(szCode, 128/sizeof(TCHAR), L"HCBT_QS");
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    break; 

                case HCBT_SETFOCUS:
                    hResult = StringCchCopy(szCode, 128/sizeof(TCHAR), L"HCBT_SETFOCUS");
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    break; 

                case HCBT_SYSCOMMAND:
                    hResult = StringCchCopy(szCode, 128/sizeof(TCHAR), L"HCBT_SYSCOMMAND");
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    break; 

                default:
                    hResult = StringCchCopy(szCode, 128/sizeof(TCHAR), L"Unknown");
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    break; 
                } 
                hResult = StringCchPrintf(szBuf, 128/sizeof(TCHAR), L"CBT -  nCode: %s, tsk: %ld, %d times   ",
                    szCode, wParam, c++);
                if (FAILED(hResult))
                {
                    // TODO: write error handler
                }
                hResult = StringCchLength(szBuf, 128/sizeof(TCHAR), &cch);
                if (FAILED(hResult))
                {
                    // TODO: write error handler
                } 
                TextOut(hdc, 2, 75, szBuf, cch); 
                ReleaseDC(hwndMain, hdc); 
                return CallNextHookEx(HookDataArr[IDM_CBT].hhook, nCode, wParam, 
                    lParam); 
            } 

            /**************************************************************** 
            WH_MOUSE hook procedure 
            ****************************************************************/ 

            LRESULT CALLBACK MouseProc(int nCode, WPARAM wParam, LPARAM lParam) 
            { 
                // CHAR szBuf[128]; 
                STRSAFE_LPWSTR szBuf;
                // CHAR szMsg[16]; 
                STRSAFE_LPWSTR szMsg;
                HDC hdc; 
                static int c = 0; 
                size_t cch; 
                HRESULT hResult;

                if (nCode < 0)  // do not process the message 
                    return CallNextHookEx(HookDataArr[IDM_MOUSE].hhook, nCode, 
                    wParam, lParam); 

                // Call an application-defined function that converts a message 
                // constant to a string and copies it to a buffer. 

                LookUpTheMessage((PMSG) lParam, szMsg); 

                hdc = GetDC(hwndMain);
                hResult = StringCchPrintf(szBuf, 128/sizeof(TCHAR), 
                    L"MOUSE - nCode: %d, msg: %s, x: %d, y: %d, %d times   ", 
                    nCode, szMsg, LOWORD(lParam), HIWORD(lParam), c++); 
                if (FAILED(hResult))
                {
                    // TODO: write error handler
                }
                hResult = StringCchLength(szBuf, 128/sizeof(TCHAR), &cch);
                if (FAILED(hResult))
                {
                    // TODO: write error handler
                }
                TextOut(hdc, 2, 95, szBuf, cch); 
                ReleaseDC(hwndMain, hdc); 
                return CallNextHookEx(HookDataArr[IDM_MOUSE].hhook, nCode, wParam, 
                    lParam); 
            } 

            /**************************************************************** 
            WH_KEYBOARD hook procedure 
            ****************************************************************/ 

            LRESULT CALLBACK KeyboardProc(int nCode, WPARAM wParam, LPARAM lParam) 
            { 
                // CHAR szBuf[128]; 
                STRSAFE_LPWSTR szBuf;
                HDC hdc; 
                static int c = 0; 
                size_t cch; 
                HRESULT hResult;

                if (nCode < 0)  // do not process message 
                    return CallNextHookEx(HookDataArr[IDM_KEYBOARD].hhook, nCode, 
                    wParam, lParam); 

                hdc = GetDC(hwndMain);
                hResult = StringCchPrintf(szBuf, 128/sizeof(TCHAR), L"KEYBOARD - nCode: %d, vk: %d, %d times ", nCode, wParam, c++);
                if (FAILED(hResult))
                {
                    // TODO: write error handler
                } 
                hResult = StringCchLength(szBuf, 128/sizeof(TCHAR), &cch);
                if (FAILED(hResult))
                {
                    // TODO: write error handler
                } 
                TextOut(hdc, 2, 115, szBuf, cch); 
                ReleaseDC(hwndMain, hdc); 
                return CallNextHookEx(HookDataArr[IDM_KEYBOARD].hhook, nCode, wParam, 
                    lParam); 
            } 

            /**************************************************************** 
            WH_MSGFILTER hook procedure 
            ****************************************************************/ 

            LRESULT CALLBACK MessageProc(int nCode, WPARAM wParam, LPARAM lParam) 
            { 
                // CHAR szBuf[128]; 
                STRSAFE_LPWSTR szBuf;
                // CHAR szMsg[16]; 
                STRSAFE_LPWSTR szMsg;
                // CHAR szCode[32]; 
                STRSAFE_LPWSTR szCode;
                HDC hdc; 
                static int c = 0; 
                size_t cch; 
                HRESULT hResult;

                if (nCode < 0)  // do not process message 
                    return CallNextHookEx(HookDataArr[IDM_MSGFILTER].hhook, nCode, 
                    wParam, lParam); 

                switch (nCode) 
                { 
                case MSGF_DIALOGBOX:
                    hResult = StringCchCopy(szCode, 32/sizeof(TCHAR), L"MSGF_DIALOGBOX");
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    break; 

                case MSGF_MENU:
                    hResult = StringCchCopy(szCode, 32/sizeof(TCHAR), L"MSGF_MENU");
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    break; 

                case MSGF_SCROLLBAR:
                    hResult = StringCchCopy(szCode, 32/sizeof(TCHAR), L"MSGF_SCROLLBAR");
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    } 
                    break; 

                default:
                    hResult = StringCchPrintf(szCode, 128/sizeof(TCHAR), L"Unknown: %d", nCode);
                    if (FAILED(hResult))
                    {
                        // TODO: write error handler
                    }
                    break; 
                } 

                // Call an application-defined function that converts a message 
                // constant to a string and copies it to a buffer. 

                LookUpTheMessage((PMSG) lParam, szMsg); 

                hdc = GetDC(hwndMain);
                hResult = StringCchPrintf(szBuf, 128/sizeof(TCHAR),  
                    L"MSGFILTER  nCode: %s, msg: %s, %d times    ", 
                    szCode, szMsg, c++);
                if (FAILED(hResult))
                {
                    // TODO: write error handler
                } 
                hResult = StringCchLength(szBuf, 128/sizeof(TCHAR), &cch);
                if (FAILED(hResult))
                {
                    // TODO: write error handler
                } 
                TextOut(hdc, 2, 135, szBuf, cch); 
                ReleaseDC(hwndMain, hdc); 
                return CallNextHookEx((HookDataArr[IDM_MSGFILTER]).hhook, nCode, 
                    wParam, lParam); 
            }

            VOID WINAPI LookUpTheMessage(PMSG lParam, STRSAFE_LPWSTR szMsg) {

            }
        } 
    }
}
