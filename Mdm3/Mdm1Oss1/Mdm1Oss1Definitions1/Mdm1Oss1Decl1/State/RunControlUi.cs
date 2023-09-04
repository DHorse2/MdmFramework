#region Dependencies
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mdm.Oss.Components;
using Mdm.Oss.Decl;
using Mdm.Oss.Std;
#region Mdm WinUtil, System Shell32, WshRuntime
using Mdm.Oss.WinUtil;
//// Project > Add Reference > 
//// add shell32.dll reference
//// (new) possibly interop.Shell32 & interop.IWshRuntimeLibrary
//// > COM > Microsoft Shell Controls and Automation
//using Shell32;
//// > COM > Windows Script Host Object Model.
//using IWshRuntimeLibrary;
#endregion
//namespace Mdm.Oss.Decl.State
#endregion

namespace Mdm.Oss.Run.Control
{
    public class StdBaseRunControlUiDef : IDisposable
    {
        #region Fields
        public StdNotifyDef StdNotify;
        public StdNotifyIconDef StdNotifyIcon;
        public bool StdNotifyVisble;
        public bool StdNotifyUsed;
        //
        public ToolStripButton ButtonPause;
        public ToolStripButton ButtonCancel;
        public ToolStripButton ButtonStart;
        public ToolStripButton ButtonFile;
        public ButtonActionIs ButtonAction;
        public bool SetButtons;
        // Status / State messages.
        public ToolStripLabel LabelDbDirectoryCount;
        public ToolStripLabel LabelDbFileCount;
        public ToolStripLabel LabelDbBusyMessage;
        // This is the App's primary dblistview.
        public object DbListView;
        public DataGridView GridView;
        public StateIs GridViewStatus;
        public bool GridViewIsExternal;
        public bool DbListViewControlIsUsed;
        #endregion
        #region Standard Objects
        public object Sender;
        public object ConsoleSender;
        public object st;
        public StdProcessDef StdProcess;
        public StdKeyDef StdKey;
        public StdBaseRunControlUiDef StdRunControlUi;
        #endregion
        #region Constructors
        public StdBaseRunControlUiDef(ref object SenderPassed, ref object stPassed, StdKeyDef StdKeyPassed)
        {
            Sender = SenderPassed;
            ConsoleSender = stPassed; st = stPassed;
            StdKey = StdKeyPassed;
            Sender = this;
            ControlCreate();
        }
        public StdBaseRunControlUiDef()
        {
            Sender = this;
            ControlCreate();
        }
        #endregion
        #region Init, Create
        public void Initialze()
        {
            ControlCreate();
        }
        public virtual StateIs ControlCreate(bool SetBuattonsPassed)
        {
            ControlCreate();
            SetButtons = SetBuattonsPassed;
            return StateIs.Finished;
        }
        public virtual StateIs ControlCreate()
        {
            // StdNotify = new StdNotifyDef(ref Sender, ref st, StdKey, true);
            // StdNotifyIcon = new StdNotifyIconDef(ref Sender, ref st, StdKey);
            // StdNotify.Add(StdKey, StdNotifyIcon);

            ButtonPause = new ToolStripButton();
            ButtonCancel = new ToolStripButton();
            ButtonStart = new ToolStripButton();
            ButtonFile = new ToolStripButton();
            ButtonAction = ButtonActionIs.None;
            LabelDbBusyMessage = new ToolStripLabel();
            SetButtons = false;

            DbListView = null;
            GridView = null;
            GridViewStatus = StateIs.NotSet;
            GridViewIsExternal = true;
            DbListViewControlIsUsed = false;
            return StateIs.Finished;
        }
        #endregion
        #region Dispose
        StateIs Status;
        public void Dispose()
        {
            Dispose(Status);
            // this is the base.Dispose();
            Status = StateIs.DoesNotExist;
        }
        public void Dispose(StateIs StatusPassed)
        {
            if (StdNotify != null) { StdNotify.Dispose(); }
            //base.Dispose();
            Status = StateIs.DoesNotExist;
        }
        #endregion
        #region Buttons
        public bool ButtonCheck()
        {
            if (ButtonPause != null
            || ButtonCancel != null
            || ButtonStart != null
            || ButtonFile != null
            || ButtonAction != ButtonActionIs.None)
            {
                SetButtons = true;
            }
            return SetButtons;
        }
        public virtual StateIs ButtonRunControlGet(
            ref ToolStripButton ButtonPausePassed,
            ref ToolStripButton ButtonCancelPassed,
            ref ToolStripButton ButtonStartPassed,
            ref ToolStripButton ButtonFilePassed,
            ref ButtonActionIs ButtonActionPassed,
            ref ToolStripLabel LabelDbBusyMessagePassed)
        {
            ButtonPausePassed = ButtonPause;
            ButtonCancelPassed = ButtonCancel;
            ButtonStartPassed = ButtonStart;
            ButtonFilePassed = ButtonFile;
            ButtonActionPassed = ButtonAction;
            SetButtons = ButtonCheck();
            LabelDbBusyMessagePassed = LabelDbBusyMessage;
            return StateIs.Finished;
        }
        public virtual StateIs ButtonRunControlSet(
        ref ToolStripButton ButtonPausePassed,
        ref ToolStripButton ButtonCancelPassed,
        ref ToolStripButton ButtonStartPassed,
        ref ToolStripButton ButtonFilePassed,
        ref ButtonActionIs ButtonActionPassed,
        ref ToolStripLabel LabelDbBusyMessagePassed)
        {
            ButtonPause = ButtonPausePassed;
            ButtonCancel = ButtonCancelPassed;
            ButtonStart = ButtonStartPassed;
            ButtonFile = ButtonFilePassed;
            ButtonAction = ButtonActionPassed;
            SetButtons = ButtonCheck();
            LabelDbBusyMessage = LabelDbBusyMessagePassed;
            return StateIs.Finished;
        }
    }
    #endregion
    // These buttons have a default control they
    // are attached to. They can be attached to
    // a differrent control or form on the fly.
    //Status = ButtonRunControlSet(
    //    ref RunControlUi.ButtonPause,
    //    ref RunControlUi.ButtonCancel,
    //    ref RunControlUi.ButtonStart,
    //    ref RunControlUi.ButtonFile,
    //    ref RunControlUi.ButtonAction,
    //    ref LabelScriptListDirectoryCount,
    //    ref LabelScriptListFileCount,
    //    ref LabelScriptListBusyMessage);
}
