using Shell32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
//using System.Windows;
using System.Windows.Forms;
//using System.Windows.Controls; // Page

using Mdm;
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Components;
// using Mdm.Oss.Mobj;
using Mdm.Oss.Thread;
using Mdm.Oss.Std;

#region Mdm WinUtil, System Shell32, WshRuntime
using Mdm.Oss.WinUtil;
// Project > Add Reference > 
// add shell32.dll reference
// (new) possibly interop.Shell32 & interop.IWshRuntimeLibrary
// > COM > Microsoft Shell Controls and Automation
//using Shell32;
// > COM > Windows Script Host Object Model.
//using IWshRuntimeLibrary;
#endregion

using HWND = System.IntPtr;
using System.Windows;
using System.Windows.Interop;

// ToDo $$MAJOR 1) Create TraceMdm Aurgument structure
// ToDo $$MAJOR 2) Create Indexer for RunAction and remove redundant settings
// ToDo $$MAJOR 2) should use indexers passed to set RunAction and RunMetric values
// ToDo $$MAJOR 2) which will reduce lines of code.
// ToDo $$MAJOR 4) Don't pass run action values to TraceMdm but use current value.
// ToDo $$MAJOR 5) Implement TLD for data after testing dict to schema code.
namespace Mdm.Oss.Console
{
    /// <summary> 
    /// <para>(StdBaseRunFileDef) Console implements the
    /// Trace, Logging, Console and messaging 
    /// operations.</para>
    /// <para> . </para>
    /// <para> See the Programming Standards ReadMe</para>
    /// </summary> 
    public class StdConsolesDef : StdBaseDef
    {
        public List<ConsoleTypeDef> Consoles;
        #region Device objects
        // Key: device name, Value: dict of window objects
        // Window object key: zorder, Value: window object
        //Dictionary<string, Dictionary<int, WindowDef>> Screens
        //    = new Dictionary<string, Dictionary<int, WindowDef>>();

        //Dictionary<HWND, string> Windows = new Dictionary<HWND, string>() >;

        //Dictionary<int, WindowDef> WindowsZOrder = new Dictionary<int, WindowDef>();

        public List<WinUtil.ScreenDef> ScreensObject = new List<WinUtil.ScreenDef>();

        public List<string> DeviceName = new List<string>();
        public List<object> DeviceForm = new List<object>();
        public List<object> DeviceTopMostPrev = new List<object>();
        #endregion
        //public StdNotifyDef StdNotify;
        public static bool Close;
        public static new string WindowTopmost;
        string[] images;
        string[] titles;
        string[] names;
        int offset = 0;
        public new StdConsoleManagerDef st;
        string NameTemp;
        static StdConsolesDef()
        {
            Close = false;
            WindowTopmost = "";
        }
        public StdConsolesDef(ref object SenderPassed, ref StdConsoleManagerDef stPassed)
            :   base(ref SenderPassed, ref stPassed.SenderIsThis)
        {
            st = base.st as StdConsoleManagerDef;
            Title = st.Title;
            Name = st.Name;
            //Sender = SenderPassed;
            //st = stPassed; base.st = st; ConsoleSender = st;
            //
            if (st.StdNotify != null
                && st.StdNotifyRoot != null)
            {
                StdKey = st.StdKey;
                StdNotify = st.StdNotify;
                StdNotifyRoot = st.StdNotifyRoot;
            }
            else
            {
                StdKey = new StdKeyDef("2", "0", "Consoles");
                StdNotify = new StdNotifyDef(ref SenderIsThis, ref ConsoleSender, StdKey, Title, true);
                // StdNotifyIcon = new StdNotifyIconDef(ref SenderIsThis, ref ConsoleSender, StdKey, Title, true);
                // StdNotifyIcon.StdNotifyAdd();
                // Get or Create Root Node from the Consoles base class.
                if (st != null)
                {
                    if (st.StdNotify != null
                        && st.StdNotify.Root != null)
                    {
                        StdNotifyRoot = st.StdNotify.Root;
                        StdNotify.Root = StdNotifyRoot;
                    }
                    else if (st.StdNotifyRoot != null)
                    {
                        StdNotifyRoot = st.StdNotifyRoot;
                        StdNotify.Root = StdNotifyRoot;
                    }
                    else
                    {
                        st.StdNotifyRoot = new StdNotifyDef(ref SenderPassed, ref st.ConsoleSender, StdKey, Title, true);
                        StdNotify.Root = StdNotifyRoot;
                    }
                } else
                {
                    StdNotifyRoot = StdNotify;
                }
            }
            st.StdRunControlUi.StdNotify = StdNotify;
            //
            //StdNotifyIcon = new StdNotifyIconDef(ref SenderIsThis, ref ConsoleSender, StdKey, Title, true);
            //StdNotifyIcon.StdNotifyAdd();
            // Console
            // not used but could be.
            images = new string[5];
            images[(int)ConsoleFormUses.All] = "Letter-A.ico"; // All !!!
            images[(int)ConsoleFormUses.DatabaseLog] = "Letter-D.ico"; // Icon.ExtractAssociatedIcon("MdmControlLeft.ico");
            images[(int)ConsoleFormUses.DebugLog] = "Letter-S.ico";
            images[(int)ConsoleFormUses.ErrorLog] = "Letter-E.ico";
            images[(int)ConsoleFormUses.UserLog] = "Letter-U.ico";

            // these are tied to the icon names.
            names = new string[5];
            names[(int)ConsoleFormUses.All] = "Console_All";
            names[(int)ConsoleFormUses.DatabaseLog] = "Console_Database";
            names[(int)ConsoleFormUses.DebugLog] = "Console_Debug";
            names[(int)ConsoleFormUses.ErrorLog] = "Console_Error";
            names[(int)ConsoleFormUses.UserLog] = "Console_User";

            titles = new string[5];
            titles[(int)ConsoleFormUses.All] = "All Messages Console";
            titles[(int)ConsoleFormUses.DatabaseLog] = "Database Message Console";
            titles[(int)ConsoleFormUses.DebugLog] = "Debug Message Console";
            titles[(int)ConsoleFormUses.ErrorLog] = "Error Console";
            titles[(int)ConsoleFormUses.UserLog] = "User Messages";

            Consoles = new List<ConsoleTypeDef>();
            for (int i = 0; i <= 4; i++)
            {
                Consoles.Add(new ConsoleTypeDef(ref SenderIsThis, ref st, StdKey.IconLevel, (i+1).ToString(), names[i], titles[i]));
                //st.StdRunControlUi
                //Type type = GetType();
                //System.Resources.ResourceManager resources =
                //new System.Resources.ResourceManager(type.Namespace + ".Properties.Resources", GetType().Assembly);

                //// here it comes, call GetObject just with the resource name, no namespace and no extension
                //Consoles[i].ConsoleForm.Icon = (System.Drawing.Icon)resources.GetObject(statusText);
                //Consoles[i].ConsoleForm.Icon = Icon.ExtractAssociatedIcon(@".\Resource\" + titles[i] + ".ico"); // ToDo
                Consoles[i].ConsoleForm.Text = titles[i];
                Consoles[i].ConsoleForm.Name = names[i];
                Consoles[i].ConsoleForm.Hide();
                Consoles[i].ConsoleForm.WindowState = FormWindowState.Minimized;
            }
        }
        public void MdmConsolesClose()
        {
            for (int i = 0; i <= 4; i++)
            {
                //Type type = GetType();
                //System.Resources.ResourceManager resources =
                //new System.Resources.ResourceManager(type.Namespace + ".Properties.Resources", GetType().Assembly);

                //// here it comes, call GetObject just with the resource name, no namespace and no extension
                //Consoles[i].ConsoleForm.Icon = (System.Drawing.Icon)resources.GetObject(statusText);
                Close = true;
                Consoles[i].ConsoleForm.Close();
                //Consoles[i].ConsoleForm.NotifyIcon.Visible = false;
            }
        }

    }
}
