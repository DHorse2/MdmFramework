#pragma once

#define NUMHOOKS 7 
#define IDM_CALLWNDPROC 1
#define IDM_CBT 2
#define IDM_DEBUG 3
#define IDM_GETMESSAGE 4
#define IDM_KEYBOARD 5
#define IDM_MOUSE 6
#define IDM_MSGFILTER 7

namespace Mdm {
    namespace Oss {
        namespace WinUtil {

            using namespace System;
            using namespace Mdm::Oss::WinUtil;

            delegate LRESULT HookProcDel(int, WPARAM, LPARAM);
            //
            typedef struct _HookDataDef 
            { 
                int nType; 
                HOOKPROC hkprc; 
                // HookProcDel^ hkprc;
                HHOOK hhook; 
                BOOL HookIsSet;
            } HookDataDef; 
            //
            public class Win32Hook {
            public:
                Win32Hook(void);
                //
                LRESULT WINAPI   MainWndProc(HWND hwndMainPassed, UINT uMsg, WPARAM wParam, LPARAM lParam);
                LRESULT CALLBACK CallWndProc(int nCode, WPARAM wParam, LPARAM lParam);
                LRESULT CALLBACK CBTProc(int nCode, WPARAM wParam, LPARAM lParam);
                LRESULT CALLBACK DebugProc(int nCode, WPARAM wParam, LPARAM lParam);
                LRESULT CALLBACK GetMsgProc(int nCode, WPARAM wParam, LPARAM lParam);
                LRESULT CALLBACK KeyboardProc(int nCode, WPARAM wParam, LPARAM lParam);
                LRESULT CALLBACK MouseProc(int nCode, WPARAM wParam, LPARAM lParam);
                LRESULT CALLBACK MessageProc(int nCode, WPARAM wParam, LPARAM lParam);

                VOID LookUpTheMessage(PMSG lParam, STRSAFE_LPWSTR szMsg);

                HWND hwndMain;
                // array<HookDataDef ^> ^ HookDataArr;
                HookDataDef HookDataArr[NUMHOOKS]; 
                int HookIndex; 
            };
        }
    }
}
