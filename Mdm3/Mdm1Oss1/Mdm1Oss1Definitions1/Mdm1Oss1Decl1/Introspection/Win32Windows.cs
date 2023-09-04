#region Dependencies
#region Mdm WinUtil, System Shell32, WshRuntime
using Mdm.Oss.WinUtil;
// Project > Add Reference > 
// add shell32.dll reference
// (new) possibly interop.Shell32 & interop.IWshRuntimeLibrary
// > COM > Microsoft Shell Controls and Automation
using Shell32;
// > COM > Windows Script Host Object Model.
using IWshRuntimeLibrary;
using HWND = System.IntPtr;
#endregion
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
//using System.Windows;
using System.Windows.Forms;
//using System.Windows.Controls; // Page

using Mdm;
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Components;
#endregion

namespace Mdm.Oss.WinUtil
{
    /// <summary>Contains functionality to get all the open windows.</summary>
    public class OpenWindowGetter
    {
        /// <summary>Returns a dictionary that contains the handle and title of all the open windows.</summary>
        /// <returns>A dictionary that contains the handle and title of all the open windows.</returns>
        public static IDictionary<HWND, string> GetOpenWindows()
        {
            HWND shellWindow = GetShellWindow();
            Dictionary<HWND, string> windows = new Dictionary<HWND, string>();

            EnumWindows(delegate (HWND hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                System.Text.StringBuilder builder = new System.Text.StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;

            }, 0);

            return windows;
        }

        public delegate bool EnumWindowsProc(HWND hWnd, int lParam);

        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(HWND hWnd, System.Text.StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();
        /// <summary>
        /// Gets the z-order for one or more windows atomically with respect to each other. In Windows, smaller z-order is higher. If the window is not top level, the z order is returned as -1. 
        /// </summary>
        int[] GetZOrder(params IntPtr[] hWnds)
        {
            var z = new int[hWnds.Length];
            for (var i = 0; i < hWnds.Length; i++) z[i] = -1;

            var index = 0;
            var numRemaining = hWnds.Length;
            EnumWindows((wnd, param) =>
            {
                var searchIndex = Array.IndexOf(hWnds, wnd);
                if (searchIndex != -1)
                {
                    z[searchIndex] = index;
                    numRemaining--;
                    if (numRemaining == 0) return false;
                }
                index++;
                return true;
            }, (int)IntPtr.Zero);

            return z;
        }
        // Delegate to filter which windows to include 
        public delegate bool EnumWindowsProc1(IntPtr hWnd, IntPtr lParam);

        /// <summary> Get the text for the window pointed to by hWnd </summary>
        public static string GetWindowText(IntPtr hWnd)
        {
            int size = GetWindowTextLength(hWnd);
            if (size > 0)
            {
                var builder = new System.Text.StringBuilder(size + 1);
                GetWindowText(hWnd, builder, builder.Capacity);
                return builder.ToString();
            }

            return String.Empty;
        }

        /// <summary> Find all windows that match the given filter </summary>
        /// <param name="filter"> A delegate that returns true for windows
        ///    that should be returned and false for windows that should
        ///    not be returned </param>
        public static IEnumerable<IntPtr> FindWindows(EnumWindowsProc filter)
        {
            IntPtr found = IntPtr.Zero;
            List<IntPtr> windows = new List<IntPtr>();

            EnumWindows(delegate (IntPtr wnd, int param)
            {
                if (filter(wnd, param))
                {
                    // only add the windows that pass the filter
                    windows.Add(wnd);
                }

                // but return true here so that we iterate all windows
                return true;
            }, (int)IntPtr.Zero);

            return windows;
        }

        /// <summary> Find all windows that contain the given title text </summary>
        /// <param name="titleText"> The text that the window title must contain. </param>
        public static IEnumerable<IntPtr> FindWindowsWithText(string titleText)
        {
            return FindWindows(delegate (IntPtr wnd, int param)
            {
                return GetWindowText(wnd).Contains(titleText);
            });
        }

        public void FindZOrder()
        {
            // Find z-order for window.
            Process[] procs = Process.GetProcessesByName("notepad");
            Process top = null;
            int topz = int.MaxValue;
            foreach (Process p in procs)
            {
                IntPtr handle = p.MainWindowHandle;
                int z = 0;
                do
                {
                    z++;
                    handle = GetWindow(handle, 3);
                } while (handle != IntPtr.Zero);

                if (z < topz)
                {
                    top = p;
                    topz = z;
                }
            }

            if (top != null)
                Debug.WriteLine(top.MainWindowTitle);
        }

        private HWND GetWindow(HWND handle, int v)
        {
            throw new NotImplementedException();
        }
    }

    public static class HwndHelper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        public static bool GetWindowZOrder(IntPtr hwnd, out int zOrder)
        {
            const uint GW_HWNDPREV = 3;
            const uint GW_HWNDLAST = 1;

            var lowestHwnd = GetWindow(hwnd, GW_HWNDLAST);

            var z = 0;
            var hwndTmp = lowestHwnd;
            while (hwndTmp != IntPtr.Zero)
            {
                if (hwnd == hwndTmp)
                {
                    zOrder = z;
                    return true;
                }

                hwndTmp = GetWindow(hwndTmp, GW_HWNDPREV);
                z++;
            }

            zOrder = int.MinValue;
            return false;
        }
    }

    public class GetTopMostDef
    {
        public const int GW_HWNDNEXT = 2; // The next window is below the specified window
        public const int GW_HWNDPREV = 3; // The previous window is above

        [DllImport("user32.dll")]
        static extern IntPtr GetTopWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindow", SetLastError = true)]
        public static extern IntPtr GetNextWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.U4)] int wFlag);

        public delegate bool EnumWindowsProc(HWND hWnd, int lParam);

        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(HWND hWnd, System.Text.StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(HWND hWnd);

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        //Importing Windows API functions
        [DllImport("user32")]
        public static extern int SetWindowLongA(int hwnd, int nIndex, int dwNew);
        [DllImport("user32")]
        public static extern int GetWindowLongA(int hwnd, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        //[DllImport("user32.dll")]
        //public static extern bool ShowWindowAsync(IntPtr hWnd, ShowWindowCommands cmdShow);


        /// <summary>
        /// Searches for the topmost visible form of your app in all the forms opened in the current Windows session.
        /// </summary>
        /// <param name="hWnd_mainFrm">Handle of the main form</param>
        /// <returns>The Form that is currently TopMost, or null</returns>
        public static FormResultDef GetTopMostWindow(IntPtr hWnd_mainFrm)
        {

            FormResultDef Result = new FormResultDef();
            IntPtr hwnd = GetTopWindow((IntPtr)null);
            if (hwnd != IntPtr.Zero) {
                Screen ScreenMain = Screen.FromHandle(hWnd_mainFrm); //this is the Form class
                Screen ScreenPrimary = Screen.PrimaryScreen;
                Screen ScreenCurrent = null;
                foreach (Screen screen in Screen.AllScreens)
                {
                    if (screen.Primary) { ScreenPrimary = screen; }
                }

                while ((!IsWindowVisible(hwnd) || Result.Form == null) && hwnd != hWnd_mainFrm)
                {
                    try
                    {
                        // Get next window under the current handler
                        hwnd = GetNextWindow(hwnd, GW_HWNDNEXT);
                        ScreenCurrent = Screen.FromHandle(hwnd);
                        if (ScreenCurrent.DeviceName == ScreenPrimary.DeviceName)
                        {
                            Result.Form = (Form)Form.FromHandle(hwnd);
                            Result.ScreenMatch = true;
                        }
                    }
                    catch
                    {
                        // Weird behaviour: In some cases, trying to cast to a Form a handle of an object 
                        // that isn't a form will just return null. In other cases, will throw an exception.
                    }
                }
            }
            return Result;
        }

        public static HWND GetWindowHandle(string Filter)
        {
            IntPtr hWnd = IntPtr.Zero;
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle.Contains(Filter))
                {
                    hWnd = pList.MainWindowHandle;
                }
            }
            return hWnd;
        }

        public static void TestGetOpenWindows()
        {
            foreach (KeyValuePair<IntPtr, string> window in GetOpenWindows())
            {
                IntPtr handle = window.Key;
                string title = window.Value;

                System.Console.WriteLine("{0}: {1}", handle, title);

            }
        }

        public static List<IntPtr> GetAllChildrenWindowHandles(IntPtr hParent, int maxCount)
        {
            System.Collections.Generic.List<IntPtr> result = new List<IntPtr>();
            int ct = 0;
            IntPtr prevChild = IntPtr.Zero;
            IntPtr currChild = IntPtr.Zero;
            while (true && ct < maxCount)
            {
                currChild = FindWindowEx(hParent, prevChild, null, null);
                if (currChild == IntPtr.Zero) break;
                result.Add(currChild);
                prevChild = currChild;
                ++ct;
            }
            return result;
        }

        // Key: device name, Value: dict of window objects
        // Window object key: zorder, Value: window object
        //public static Dictionary<string, Dictionary<int, WindowDef>> Screens
        //    = new Dictionary<string, Dictionary<int, WindowDef>>();
        public List<ScreenDef> ScreensObject = new List<ScreenDef>();
        private const int WS_MINIMIZE = 0x20000;
        private const int WS_MAXIMIZE = 0x10000;
        private const Int32 GWL_STYLE = -16;

        public FormResultDef BuildScreenOrder(object sender, IntPtr WindowHandleMainForm)
        {
            ScreensObject
                = new List<ScreenDef>();

            HWND shellWindow = GetShellWindow();

            Screen ScreenMain = Screen.FromHandle(WindowHandleMainForm); //this is the Form class
            Screen ScreenPrimary = Screen.PrimaryScreen;
            Screen ScreenCurrent = null;
            FormResultDef FormResult = new FormResultDef();
            //string ClassName = MethodBase.GetCurrentMethod().DeclaringType.Name;
            string ClassName = sender.GetType().Name;
            //string NameSpaceName = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
            string NameSpaceName = sender.GetType().Namespace;
            string temp = "ShortcutUtilsFormDef";
            // "Mdm.Oss.ShortcutUtil" + "." + 
            IntPtr toplevelWindow = FindWindow(temp, "Shortcut Repair Browser");
            IntPtr childWindow = FindWindowEx(WindowHandleMainForm, new IntPtr(0), ClassName, ((Form)sender).Text);
            // WindowHandleMainForm


            string ScreenDeviceName = "";
            int ScreenIndex = -1;
            int ScreenIndexThis = -1;
            int WindowIndex = -1;
            int WindowIndexThis = -1;
            bool WindowFound = false;

            foreach (Screen screen in Screen.AllScreens)
            {
                ScreenIndex++;
                if (screen.Primary) { ScreenPrimary = screen; }

                ScreensObject.Add(new ScreenDef());
                ScreensObject[ScreenIndex].Id = ScreenIndex;
                ScreensObject[ScreenIndex].DeviceName = screen.DeviceName;
                ScreensObject[ScreenIndex].WindowsList = new List<WindowDef>();
                ScreensObject[ScreenIndex].ZOrderHi = -1;
            }

            FormResultDef Result = new FormResultDef();
            IntPtr WindowHandle = GetTopWindow((IntPtr)null);

            IntPtr ChildWindow = FindWindowEx(WindowHandleMainForm, new IntPtr(0), ClassName, ((Form)sender).Text);
            // IntPtr ChildWindow = FindWindowEx(WindowHandleMainForm, new IntPtr(0), ClassName, ((Form)sender).Name);

            if (WindowHandle != IntPtr.Zero)
            {
                bool cont = true;
                //while ((!IsWindowVisible(WindowHandle) || Result.Form == null) && WindowHandle != WindowHandleMainForm)
                while ((!IsWindowVisible(WindowHandle) || Result.Form == null) && WindowHandle != WindowHandleMainForm && cont)
                {
                    try
                    {
                        // Get next window under the current handler
                        WindowHandle = GetNextWindow(WindowHandle, GW_HWNDNEXT);

                        if (WindowHandle != IntPtr.Zero)
                        {

                            //int style = GetWindowLong(WindowHandle, GWL_STYLE);
                            //if ((style & WS_MAXIMIZE) == WS_MAXIMIZE)
                            //{
                            //    //It's maximized
                            //}
                            //else if ((style & WS_MINIMIZE) == WS_MINIMIZE)
                            //{
                            //    //It's minimized
                            //}

                            //if (IsIconic(WindowHandle))
                            //{
                            //    ShowWindowAsync(WindowHandle, ShowWindowCommands.SW_RESTORE);
                            //}

                            bool tempa = IsIconic(WindowHandle);
                            bool tempb = IsWindowVisible(WindowHandle);

                            if (IsWindowVisible(WindowHandle) && !IsIconic(WindowHandle))
                            {
                                ScreenCurrent = Screen.FromHandle(WindowHandle);
                                ScreenDeviceName = ScreenCurrent.DeviceName;

                                try
                                {
                                    int length = GetWindowTextLength(WindowHandle);
                                    System.Text.StringBuilder WindowName = new System.Text.StringBuilder(length);
                                    if (length == 0) { WindowName = new System.Text.StringBuilder(""); }
                                    else {
                                        GetWindowText(WindowHandle, WindowName, length + 1);
                                    }

                                    Screen ScreenCurr = Screen.FromHandle(WindowHandle); //this is the Form class

                                    for (ScreenIndex = 0; ScreenIndex < ScreensObject.Count; ScreenIndex++)
                                    {
                                        if (ScreensObject[ScreenIndex].DeviceName == ScreenCurr.DeviceName)
                                        {
                                            break;
                                        }
                                    }

                                    ScreensObject[ScreenIndex].ZOrderHi++;

                                    WindowDef WindowCurr = new WindowDef();
                                    WindowCurr.Handle = WindowHandle;
                                    WindowCurr.ScreenObject = ScreenCurr;
                                    WindowCurr.Name = WindowName.ToString();
                                    if (WindowCurr.Name.Length > 0) {
                                        WindowCurr.Process = 0;
                                        WindowCurr.ZOrder = ScreensObject[ScreenIndex].ZOrderHi++;
                                        WindowFound = false;

                                        //for (WindowIndex = 0; WindowIndex < ScreensObject[ScreenIndex].WindowsList.Count; WindowIndex++)
                                        //{
                                        //}
                                        ScreensObject[ScreenIndex].WindowsList.Add(WindowCurr);
                                        if (WindowHandle == ((Form)sender).Handle)
                                        {
                                            ScreenIndexThis = ScreenIndex;
                                            WindowIndexThis = WindowIndex;
                                            WindowFound = true;
                                            break;
                                        }
                                    }
                                }
                                catch (Exception)
                                {

                                    throw;
                                }

                                if (ScreenCurrent.DeviceName == ScreenPrimary.DeviceName)
                                {
                                    Result.Form = (Form)Form.FromHandle(WindowHandle);
                                    Result.ScreenMatch = true;
                                }
                            }
                        } else { cont = false; }
                    }
                    catch
                    {
                        // Weird behaviour: In some cases, trying to cast to a Form a handle of an object 
                        // that isn't a form will just return null. In other cases, will throw an exception.
                    }
                }
                // return windows;
                // return ScreensObject;
            } else { ScreensObject = null; }
            FormResult.ScreensObject = ScreensObject;
            FormResult.ScreensIndex = ScreenIndexThis;
            FormResult.WindowsIndex = WindowIndexThis;
            return FormResult;
        }
        public static IDictionary<HWND, string> GetOpenWindows()
        {
            HWND shellWindow = GetShellWindow();
            Dictionary<HWND, string> windows = new Dictionary<HWND, string>();

            EnumWindows(delegate (HWND hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                System.Text.StringBuilder builder = new System.Text.StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;

            }, 0);

            return windows;
        }
    }
    public class ScreenDef
    {
        public string DeviceName;
        public int Id;
        public int ZOrderHi;
        public List<WindowDef> WindowsList;

        public ScreenDef()
        {
            DeviceName = "";
            Id = 0;
            ZOrderHi = 0;
            WindowsList = new List<WindowDef>();
        }
    }
    public class WindowDef
    {
        public HWND Handle;
        public string Name;
        public int Process;
        public int ZOrder;
        public Screen ScreenObject;

        public WindowDef()
        {
            Handle = IntPtr.Zero;
            Name = "";
            Process = 0;
            ZOrder = 0;
            ScreenObject = null;
        }
    }

    public class FormResultDef
    {
        public Form Form;
        public bool ScreenMatch;
        public int ScreensIndex;
        public int WindowsIndex;
        public bool IsTopWindows;
        public List<ScreenDef> ScreensObject;
        public FormResultDef()
        {
            Form = null;
            ScreenMatch = false;
            ScreensIndex = 0;
            WindowsIndex = 0;
            IsTopWindows = false;
            ScreensObject = null;
        }
    }

}