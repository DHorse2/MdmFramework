#region Dependencies
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Controls;

#region  Mdm Core
using Mdm;
using Mdm.Oss;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.File.Control;
//@@@CODE@@@using Mdm.Oss.Mapp;
// using Mdm.Oss.Mobj;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
//using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
//using Mdm.Oss.Threading;
using Mdm.Oss.Components;
//using Mdm.World;
#endregion
#region  Mdm Db and File
//using Mdm.Oss.File;
//using Mdm.Oss.File.Control;
//using Mdm.Oss.File.Db;
//using Mdm.Oss.File.Db.Data;
//using Mdm.Oss.File.Db.Table;
//using Mdm.Oss.File.Db.Thread;
//using Mdm.Oss.File.Properties;
//using Mdm.Oss.File.RunControl;
#endregion
#region Mdm WinUtil, System Shell32, WshRuntime
using Mdm.Oss.WinUtil;
// Project > Add Reference > 
// add shell32.dll reference
// (new) possibly interop.Shell32 & interop.IWshRuntimeLibrary
// > COM > Microsoft Shell Controls and Automation
using Shell32;
// > COM > Windows Script Host Object Model.
using IWshRuntimeLibrary;
#endregion
#endregion

namespace Mdm.Oss.Std
{
    public partial class StdBaseFormDef
    {
        #region Console Form
        private bool EventsSet;
        public virtual void InitializeStdBaseForm()
        {
            InitializeStd(ref Sender, ref ConsoleSender, ConsoleSource, ClassRole, ClassFeatures);
            ScreenGet();
            // events
            if (!EventsSet)
            {
                Activated += Form_Activated;
                Deactivate += Form_Deactivated;
                GotFocus += Form_GotFocus;
                LostFocus += Form_LostFocus;
                FormClosed += Form_Closed;
                FormClosing += Form_Closing;
                Load += Form_Load;
                LocationChanged += new System.EventHandler(Form_LocationChanged);
                // LocationChanged += new EventHandler(Form_LocationChanged);
                LostFocus += Form_LostFocus;
                //Resize += new EventHandler(Form_Resize);
                Resize += Form_Resize;
                ResizeEnd += Form_ResizeEnd;
                Shown += Form_Shown;
                EventsSet = true;
            }
            TopMostInitialize(ref ScreenObject);
        }
        public virtual void OnForm_Load(EventArgs e)
        { Form_Load(this, e); }
        public virtual void Form_Load(object sender, EventArgs e)
        { Form_Resize(sender, e); }
        public virtual void OnForm_ResizeEnd(EventArgs e)
        { Form_ResizeEnd(this, e); }
        public virtual void Form_ResizeEnd(Object sender, EventArgs e)
        { /*//TopMostMake(this, ref ScreenObject);*/ }
        public virtual void Form_LocationChanged(Object sender, EventArgs e)
        { }
        public virtual void OnForm_Shown(EventArgs e)
        { Form_Shown(this, e); }
        public virtual void Form_Shown(object sender, EventArgs e)
        {
            if (!FormShownInitialized)
            {
                Invalidate();
                FormShownInitialized = true;
                return;
            }
        }
        public virtual void OnForm_Show(EventArgs e)
        { Form_Show(this, e); }
        public virtual void Form_Show(object sender, EventArgs e)
        {
            //TopMostMake(this, ref ScreenObject);
            //Invalidate();
            NotifyIcon_ClickDo(this, true, false);
        }
        public new void Show()
        { base.Show(); }
        public virtual void Form_Hide(object sender, EventArgs e)
        { TopMostRemove(sender, ref ScreenObject); }
        public new void Hide()
        { base.Hide(); }
        public new void BringToFront()
        {
            if (!Visible)
            {
                Show();
                Visible = true;
            }
            base.BringToFront();
            Focus();
        }
        public virtual void StdNotifyGroupAction(bool GroupIsVisible)
        {
            if (GroupIsVisible)
            {
                if (VisibleOnRestore)
                {
                    Form_ShowFromTray(true);
                }
                else
                {
                    Form_HideToTray(true);
                }
            }
            else { Form_HideToTray(true); }
        }
        public virtual void Form_HideToTray(bool IsGroupAction)
        {

            if (StdNotify.Items[StdKey.Key].IsUsed
                && StdNotify.Items[StdKey.Key].VisibleAllowed)
            {
                if (WindowState != FormWindowState.Minimized)
                {
                    StdNotify.Items[StdKey.Key].FormWindowStatePrev = WindowState;
                    Hide();
                }
                Visible = false;
                if (!IsGroupAction) { VisibleOnRestore = Visible; }
            }
        }
        public virtual void Form_ShowFromTray(bool IsGroupAction)
        {
            if (StdNotify.Items[StdKey.Key].IsUsed
                && StdNotify.Items[StdKey.Key].VisibleAllowed)
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    WindowState = StdNotify.Items[StdKey.Key].FormWindowStatePrev;
                    Show();
                }
                Visible = true;
                if (!StdNotifyIcon.Visible) { StdNotifyIcon.Visible = true; }
                BringToFront();
                if (!IsGroupAction) { VisibleOnRestore = Visible; }
            }
        }
        public virtual FormWindowState Form_WindowStateGet()
        { return WindowState; }
        public virtual void Form_WindowStateSet(FormWindowState WindowStatePassed)
        { WindowState = WindowStatePassed; }
        public new void Close()
        {
            base.Close();
            Dispose();
        }

        public virtual void Form_Closing()
        { Form_Closing(this, new FormClosingEventArgs(CloseReason.UserClosing, false)); }
        public virtual void Form_Closing(FormClosingEventArgs e)
        { Form_Closing(this, e); }
        public virtual void Form_Closing(object sender, FormClosingEventArgs e)
        { }
        public virtual void Form_Closed()
        { Form_Closed(this, new FormClosedEventArgs(CloseReason.UserClosing)); }
        public virtual void Form_Closed(FormClosedEventArgs e)
        { Form_Closed(this, e); }
        public virtual void Form_Closed(object sender, FormClosedEventArgs e)
        {
            // ToDo Trap this in console form.
            // ToDo connection close

            //this.Hide();
            if (!StdConsolesDef.Close)
            {
                NotifyIcon_ClickDo();
                //e.Cancel = true;
            }
            else
            {
                //NotifyIcon.Visible = false;
                st.StdRunControlUi.StdNotify.Visible = false;
                st.StdRunControlUi.StdNotify.StdNotifyIcon.Visible = false;
                //st.StdRunControlUi.StdNotify.StdNotifyIcon.Dispose();
                //st.StdRunControlUi.StdNotify.Dispose();
                st.StdConsoles.MdmConsolesClose();
                st.StdRunControlUi.StdNotify.Dispose();
                StdNotifyIcon.Dispose();
                StdRunControlUi.Dispose();
                st.Dispose();
            }
        }
        public virtual void Form_Resize(object sender, EventArgs e)
        {
            //base.OnResize(e);
            //TopMostMake(this, ref ScreenObject);
            //if (WindowState == FormWindowState.Minimized)
            ////if (WindowState == FormWindowState.Minimized && minimizeToTrayToolStripMenuItem.Checked == true)
            //{

            //    Hide();
            //    NotifyIconVisble = false;
            //    //NotifyIcon.BalloonTipText = "My application still working...";
            //    //NotifyIcon.BalloonTipTitle = "My Sample Application";
            //    //NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            //    //NotifyIcon.Visible = true;

            //    // NotifyIcon.ShowBalloonTip(500);
            //    //WindowState = FormWindowState.Minimized;

            //    //   NotificationIcon1.Visible = true;
            //    //   NotificationIcon1.BalloonTipText = "Tool Tip Text";
            //    //NotificationIcon1.ShowBalloonTip(2);  //show balloon tip for 2 seconds
            //    //   NotificationIcon1.Text = "Balloon Text that shows when minimized to tray for 2 seconds";
            //    //   WindowState = FormWindowState.Minimized;
            //    //   ShowInTaskbar = false;
            //}
            //else if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)
            //{
            //    Show();
            //    NotifyIconVisble = true;
            //}
            return;
        }
        public virtual void Form_Activated(object sender, EventArgs e)
        {
            // TopMostPrevCheck(this, ref ScreenObject);
            //TopMostMake(this, ref ScreenObject);
        }
        public virtual void Form_Deactivated(object sender, EventArgs e)
        {
            // TopMostPrevCheck(this, ref ScreenObject);
        }
        public virtual void Form_GotFocus(object sender, EventArgs e)
        {
            TopMostMake(this, ref ScreenObject);
            // 
            //TopMostPrevCheck(this, ref ScreenObject);
            //TopMostMake(this, ref ScreenObject);
            //NotifyIcon_ClickDo();
            //if (FormShownFirst)
            //{
            //    FormShownFirst = false;
            //    return;
            //}
        }
        public virtual void Form_LostFocus(object sender, EventArgs e)
        { }
        public virtual void Form_OnResize(EventArgs e)
        { base.OnResize(e); }
        public virtual void Form_OnShown(object sender, EventArgs e)
        { 
            Form_OnResize(e);
            base.OnShown(e);
        }
        #endregion
        #region ZOrder
        // ToDo All this code need to be rewritten. It is nonsense now.
        public virtual bool TopMostInitialize(ref Screen ThisScreen)
        {
            HandlePtr = this.Handle;
            if (WindowTopmostPrev == null) { WindowTopmostPrev = StdKey.Key; }
            if (WindowTopmost == null) { WindowTopmost = StdKey.Key; }
            if (!StdProcessDef.Processes.ContainsKey(StdKey.Key))
            {
                StdProcessDef.Processes.Add(StdKey.Key, StdProcess);
            }
            // System.IntPtr ThisHandle = GetTopMostDef.GetWindowHandle(this.Name);
            StdProcessDef.Processes[StdKey.Key].StdScreen.ScreenObject = Screen.FromControl(this); //this is the Form class
            return true;
        }
        public virtual bool TopMostPrevMake(object sender, ref Screen ThisScreen)
        {
            if (WindowTopmost != WindowTopmostPrev)
            {
                WindowTopmost = WindowTopmostPrev;
                StdKeyCurr.Key = WindowTopmost;
                ((iStdBaseForm)StdProcessDef.Processes[WindowTopmost].StdScreen.DeviceForm).
                    Form_ShowFromTray(false);
                ((iStdBaseForm)StdProcessDef.Processes[WindowTopmost].StdScreen.DeviceForm).
                    TopMostMake(sender, ref StdProcessDef.Processes[WindowTopmost].StdScreen.ScreenObject);
                //StdProcessDef.WindowTopMostPrev = WindowTopmost;
            }
            // ParentObject.DeviceForm[ScreenIndex] = new object();
            // TopMostMake(sender, ref ScreenObject);
            // StdConsolesDef.WindowTopmost = "";
            return true;
        }
        public virtual bool TopMostMake(object sender, ref Screen ScreenObjectPassed)
        {
            if (ScreenObjectPassed == null)
            { ScreenObjectPassed = ScreenGet(); }
            ScreenObject = ScreenObjectPassed;
            if (!StdProcessDef.Processes.ContainsKey(StdKey.Key))
            {
                StdProcessDef.Processes.Add(StdKey.Key, StdProcess);
                StdProcessDef.Processes[StdKey.Key].StdScreen.DeviceForm = this;
                StdProcessDef.Processes[StdKey.Key].StdScreen.ScreenObject = ScreenObject;
                StdProcessDef.Processes[StdKey.Key].StdScreen.DeviceName = Name;
            }
            // Just a test
            //int NameLoc = StdProcessDef.Processes.DictPosGet(StdKey.Key);
            //if (StdKeyPrev.Key != WindowTopmost)
            if (WindowTopmost == WindowTopmostNotSet)
            {
                WindowTopmost = StdKey.Key;
                StdKeyCurr.Key = WindowTopmost;
                StdProcessDef.WindowTopMost = WindowTopmost;
                StdKeyPrev.Key = WindowTopmost;
                StdProcessDef.WindowTopMostPrev = WindowTopmost;
            } else if (StdKeyPrev.Key != WindowTopmost)
            {
                WindowTopmostPrev = WindowTopmost;
                StdKeyPrev.Key = WindowTopmost;
                StdProcessDef.WindowTopMostPrev = WindowTopmost;
                //StdProcessDef.Screens[StdKey].DeviceForm = StdProcessDef.Screens[StdKeyPrev];
                //WindowTopmost = StdKey.Key;
            }
            WindowTopmost = StdKey.Key;
            StdConsolesDef.WindowTopmost = StdKey.Key;
            StdProcessDef.WindowTopMost = StdKey.Key;
            return true;
        }
        public virtual bool TopMostRemove(object sender, ref Screen ThisScreen)
        {
            if (StdProcessDef.WindowTopMostPrev != StdKey.Key)
            {
                //StdProcessDef.Processes[StdKey.Key].StdScreen.DeviceForm = StdProcessDef.WindowTopMostPrev;
                StdProcessDef.WindowTopMostPrev = StdKey.Key;
            }
            //StdProcessDef.Screens[StdKey].DeviceForm = new object();
            TopMostMake(sender, ref ThisScreen);
            StdConsolesDef.WindowTopmost = "";
            return true;
        }
        public virtual ref Screen ScreenGet()
        {
            ScreenObject = Screen.FromControl(this); //this is the Form class
            return ref ScreenObject;
        }
        #endregion
        #region Shell / Win32
        #region Form Thread (cross) functions

        public new void OnGotFocus(EventArgs EventArgsPassed)
        {
            //if (WindowTopmost == WindowTopmostNotSet)
            //{
            //    WindowTopmost = StdKey.Key;
            //    StdKeyCurr.Key = WindowTopmost;
            //    StdProcessDef.WindowTopMost = WindowTopmost;
            //    StdKeyPrev.Key = WindowTopmost;
            //    StdProcessDef.WindowTopMostPrev = WindowTopmost;
            //}
            //else if (WindowTopmost != StdKey.Key)
            //{ TopMostMake(this, ref ScreenObject); }
            base.OnGotFocus(EventArgsPassed);
        }
        public void ControlTextSet(object SenderPassed, string TextPassed, bool AppendText, bool IsInvokedCall)
        {
            try
            {
                if (!IsInvokedCall)
                {
                    // not Net35 syntax. ToDo
                    BeginInvoke(new Action<object, string, bool, bool>(ControlTextSet), new object[] { SenderPassed, TextPassed, AppendText, true });
                    return;
                }
                Type TType = Sender.GetType();
                string TTypeName = TType.Name;
                //switch (TTypeName)
                //{
                if (SenderPassed is System.Windows.Forms.Form)
                {
                    if (!AppendText) { ((System.Windows.Forms.Form)SenderPassed).Text = ""; }
                    ((System.Windows.Forms.Form)SenderPassed).Text += TextPassed;
                }
                else if (SenderPassed is System.Windows.Forms.ToolStripButton)
                {
                    if (!AppendText) { ((System.Windows.Forms.ToolStripButton)SenderPassed).Text = ""; }
                    ((System.Windows.Forms.ToolStripButton)SenderPassed).Text += TextPassed;
                }
                else if (SenderPassed is System.Windows.Forms.ToolStripLabel)
                {
                    if (!AppendText) { ((System.Windows.Forms.ToolStripLabel)SenderPassed).Text = ""; }
                    ((System.Windows.Forms.ToolStripLabel)SenderPassed).Text += TextPassed;
                }
                else if (SenderPassed is System.Windows.Forms.Button)
                {
                    if (!AppendText) { ((System.Windows.Forms.Button)SenderPassed).Text = ""; }
                    ((System.Windows.Forms.Button)SenderPassed).Text += TextPassed;
                }
                else if (SenderPassed is System.Windows.Forms.Label)
                {
                    if (!AppendText) { ((System.Windows.Forms.Label)SenderPassed).Text = ""; }
                    ((System.Windows.Forms.Label)SenderPassed).Text += TextPassed;
                }
                else if (SenderPassed is System.Windows.Forms.ToolBar)
                {
                    if (!AppendText) { ((System.Windows.Forms.ToolBar)SenderPassed).Text = ""; }
                    ((System.Windows.Forms.ToolBar)SenderPassed).Name += TextPassed;
                }
                else if (SenderPassed is System.Windows.Forms.MenuStrip)
                {
                    if (!AppendText) { ((System.Windows.Forms.MenuStrip)SenderPassed).Text = ""; }
                    ((System.Windows.Forms.MenuStrip)SenderPassed).Name += TextPassed;
                }
                else if (SenderPassed is System.Windows.Forms.Control)
                {
                    if (!AppendText) { ((System.Windows.Forms.Control)SenderPassed).Text = ""; }
                    ((System.Windows.Forms.Control)SenderPassed).Text += TextPassed;
                }
                //    default:
                //        break;
                //}

            }
            catch (Exception e)
            {
                //throw; // ToDo Wrong Sender
            }
        }
        #endregion
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        public IntPtr GetForegroundWindowCall()
        {
            return GetForegroundWindow();
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);
        public int GetWindowThreadProcessIdCall(IntPtr handle, out int processId)
        {
            return GetWindowThreadProcessIdCall(handle, out processId);
        }
        #endregion
        #region Notify
        public virtual void NotifyIcon_BalloonTipClosed(object sender, EventArgs e)
        {

            ((StdNotifyIconDef)sender).Visible = false;
            ((StdNotifyIconDef)sender).Dispose();
            //st.StdRunControlUi.StdNotifyIcon.Visible = false;
            //st.StdRunControlUi.StdNotifyIcon.Dispose();
            //((NotifyIcon)sender).Visible = false;
            //((NotifyIcon)sender).Dispose();
        }
        public virtual void NotifyIcon_Click(object sender, MouseEventArgs e)
        {
            NotifyIcon_ClickDo(sender, false, false);
        }
        public virtual void NotifyIcon_ClickDo()
        {
            NotifyIcon_ClickDo(this, false, false);
        }
        public virtual void NotifyIcon_ClickDo(object sender, bool SetVisible, bool PassedVisible)
        {
            // Waring. Functions are calling this
            // without carrying if Notification Icons
            // are actually enable.
            // Why? At the time to maintain a single
            // logic path for the state. Needs work.
            if (!StdNotifyEnabled) { return; }
            if (StdNotifyIcon.IsUsed
                && StdNotifyIcon.VisibleAllowed)
            {

            }
            if (StdNotifyIcon.IsNotifyGroup)
            {
                if (StdNotify.NotifyGroupOpen)
                { StdNotify.NotifyGroupOpen = false; }
                else
                { StdNotify.NotifyGroupOpen = true; }
                StdNotifyDef.ButtonTrayGroupBuild(ref StdNotify, false, true);
            }
            else
            {
                if (SetVisible)
                {
                    if (PassedVisible) { Visible = false; } else { Visible = true; }
                }
                bool ThisTopMost = false;
                bool SameScreen = false;
                string ClassNameThis = this.GetType().Name;
                //InitializeTopMost(ref ScreenObject);
                if (StdProcessDef.Processes[StdKey.Key].StdScreen.DeviceForm == this) { ThisTopMost = true; }
                //
                if (!ThisTopMost && Visible && st.StdRunControlUi.StdNotify.StdNotifyIcon.Visible)
                {
                    if (WindowState == FormWindowState.Minimized) { WindowState = FormWindowState.Normal; }
                    BringToFront();
                    TopMostMake(this, ref ScreenObject);
                }
                else
                {
                    if (!st.StdRunControlUi.StdNotify.StdNotifyIcon.Visible) { }
                    if (!Visible)
                    {
                        Visible = true;
                        st.StdRunControlUi.StdNotify.StdNotifyIcon.Visible = true;
                        Show();
                        if (WindowState == FormWindowState.Minimized) { WindowState = FormWindowState.Normal; }
                        BringToFront();
                        TopMostMake(this, ref ScreenObject);
                    }
                    else
                    {
                        st.StdRunControlUi.StdNotify.StdNotifyIcon.Visible = false;
                        Hide();
                        TopMostPrevMake(this, ref ScreenObject);
                    }
                }
            }
        }
        #endregion
    }
}
