#pragma once

// #include "resource.h"


#define NUMHOOKS 7 
#define IDM_CALLWNDPROC 1
#define IDM_CBT 2
#define IDM_DEBUG 3
#define IDM_GETMESSAGE 4
#define IDM_KEYBOARD 5
#define IDM_MOUSE 6
#define IDM_MSGFILTER 7

            typedef struct _HookDataDef 
            { 
                int nType; 
                HOOKPROC hkprc; 
                HHOOK hhook; 
                BOOL HookIsSet;
            } HookData; 


namespace Mdm {
    namespace Oss {
        namespace WinUtil {

            // public class Win32Hook {
            // public:
                LRESULT WINAPI MainWndProc(HWND hwndMainPassed, UINT uMsg, WPARAM wParam, LPARAM lParam);
                LRESULT CALLBACK CallWndProc(int nCode, WPARAM wParam, LPARAM lParam);
                LRESULT CALLBACK CBTProc(int nCode, WPARAM wParam, LPARAM lParam);
                LRESULT CALLBACK DebugProc(int nCode, WPARAM wParam, LPARAM lParam);
                LRESULT CALLBACK GetMsgProc(int nCode, WPARAM wParam, LPARAM lParam);
                LRESULT CALLBACK KeyboardProc(int nCode, WPARAM wParam, LPARAM lParam);
                LRESULT CALLBACK MouseProc(int nCode, WPARAM wParam, LPARAM lParam);
                LRESULT CALLBACK MessageProc(int nCode, WPARAM wParam, LPARAM lParam);

                VOID WINAPI LookUpTheMessage(PMSG lParam, STRSAFE_LPWSTR szMsg);

                //HWND hwndMain;
                //_HookDataDef HookDataArr[NUMHOOKS]; 
                //int HookIndex; 
            // };
        }
    }
}
