using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mdm.Oss.Std
{
    public interface iStdBaseForm : iClassFeatures
    {
        #region Console Form
        // Visible
        bool VisibleGet();
        void VisibleSet(bool VisiblePassed);
        void Form_HideToTray(bool IsGroupAction);
        void Form_ShowFromTray(bool IsGroupAction);
        // State
        ref Screen ScreenGet();
        FormWindowState Form_WindowStateGet();
        void Form_WindowStateSet(FormWindowState WindowStatePassed);
        // Form Events
        // Showing
        void BringToFront();
        void Form_Shown(object sender, EventArgs e);
        void Form_Show(object sender, EventArgs e);
        void Show();
        void Form_Hide(object sender, EventArgs e);
        void Hide();
        // 
        void Form_Load(object sender, EventArgs e);
        void Form_Closing();
        void Form_Closing(object sender, System.Windows.Forms.FormClosingEventArgs e);
        void Form_Closed();
        void Form_Closed(object sender, System.Windows.Forms.FormClosedEventArgs e);
        //
        void OnForm_ResizeEnd(EventArgs e);
        void Form_Resize(object sender, EventArgs e);
        //
        void Form_Activated(object sender, EventArgs e);
        void Form_Deactivated(object sender, EventArgs e);
        void Form_GotFocus(object sender, EventArgs e);
        void Form_LostFocus(object sender, EventArgs e);
        #endregion
        #region ZOrder
        void OnGotFocus(System.EventArgs EventArgsPassed);
        bool TopMostInitialize(ref Screen ThisScreen);
        bool TopMostPrevMake(object sender, ref Screen ThisScreen);
        bool TopMostRemove(object sender, ref Screen ThisScreen);
        bool TopMostMake(object sender, ref Screen ThisScreen);
        IntPtr GetForegroundWindowCall();

        int GetWindowThreadProcessIdCall(IntPtr handle, out int processId);

        //[DllImport("user32.dll")]
        //private static extern IntPtr GetForegroundWindow();
        //public bool IsActive()
        ///// <summary>Returns true if the current application has focus, false otherwise</summary>
        //public static bool ApplicationIsActivated()
        #endregion
        #region Notify
        void StdNotifyGroupAction(bool GroupIsVisible);
        void NotifyIcon_BalloonTipClosed(object sender, EventArgs e);
        void NotifyIcon_Click(object sender, MouseEventArgs e);
        void NotifyIcon_ClickDo();
        void NotifyIcon_ClickDo(object sender, bool SetVisible, bool PassedVisible);
        #endregion
    }
}
