using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mdm.Oss.Components;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;

namespace Mdm.Oss.Components
{
    public class StdNotifyXXXIconDef : StdBaseDef, IDisposable
    {
        #region Data and properties
        public static int OrderGlobal;
        public int Id { get; set; }
        public int ItemId;
        //public T Item;
        public bool NewRecord;
        public string TaskType;
        public bool SetButtons;
        public int RowCount;
        #endregion
        #region Fields
        // private Mdm.Oss.ClipUtil.NotificationAreaIcon notAreaIcon;
        // private System.Windows.NotificationAreaIcon;
        // private System.Windows.Forms.MessageBox;
        public NotifyIcon NotifyIconItem;
        public string ImageName;
        public iStdBaseForm FormStd;
        public FormWindowState FormWindowStatePrev;
        public ToolStripButton ButtonControl;
        public int ButtonOffset;
        public ContextMenu CntxtMenu; // tray
        public MenuItem menuItemClose;
        public MenuItem menuItemShow;
        public MenuItem menuItemCloseAll;
        public bool IsUsed { get; set; }
        public bool VisibleAlert { get; set; }
        public bool VisibleAllowed { get; set; }
        public bool Visible
        {
            get
            {
                if (NotifyIconItem != null) { NotifyIconItem.Visible = bVisible; }
                if (ButtonControl != null) { ButtonControl.Visible = bVisible; }
                return bVisible;
            }
            set
            {
                bVisible = value;
                if (NotifyIconItem != null) { NotifyIconItem.Visible = bVisible; }
                if (ButtonControl != null) { ButtonControl.Visible = bVisible; }
            }
        }
        public bool bVisible;
        public ObjectVisibilityIs Visibility;
        public string Text
        {
            get;
            set;
        }
        //
        public string IconLevel;
        public string IconOrder; // Sort order.
        public string IconName;
        public int IconOffset;
        public string BalloonTipText;
        //
        //ConsoleTypeDef ConsoleType;
        public object ConsoleType; // ToDo Rename
                                   //public new StdConsoleManagerDef st;
        #endregion
        #region Standard Objects
        //public object Sender;
        //public object ConsoleSender;
        //public object st;
        //public StdProcessDef StdProcess;
        //public StdKeyDef StdKey;
        //public StdBaseRunControlUiDef StdRunControlUi;
        #endregion
        #region Constructors
        public StdNotifyXXXIconDef(
            ref object SenderPassed,
            ref object stPassed,
            StdKeyDef StdKeyPassed)
        : base(ref SenderPassed, ref stPassed)
        {
            Sender = SenderPassed;
            if (StdKeyPassed != null)
            {
                StdKey = StdKeyPassed;
                StdKey.GetTo(ref IconLevel, ref IconOrder, ref IconName);
            }
            else
            {
                IconLevel = "#"; IconOrder = "#"; IconName = "NotSet";
                StdKey = new StdKeyDef(IconLevel, IconOrder, IconName);
            }
            InitializeNotifiyIcon();
        }
        public StdNotifyXXXIconDef(
            ref object SenderPassed,
            ref object stPassed,
            string LevelPassed,
            string OrderPassed,
            string NamePassed)
        : base(ref SenderPassed, ref stPassed)
        {
            Sender = SenderPassed;
            IconName = NamePassed;
            IconLevel = LevelPassed;
            IconOrder = OrderPassed;
            StdKey = new StdKeyDef(IconLevel, IconOrder, IconName);
            InitializeNotifiyIcon();
        }
        public StdNotifyXXXIconDef(
            ref object SenderPassed,
            ref object stPassed,
            string LevelPassed,
            string OrderPassed,
            string IconNamePassed,
            string IconTextPassed,
            ref NotifyIcon StdNotifyIconPassed,
            ref ContextMenu StdNotifyCntxtMenuPassed)
        : base(ref SenderPassed, ref stPassed)
        {
            Sender = SenderPassed;
            Text = IconTextPassed;
            NotifyIconItem = StdNotifyIconPassed;
            CntxtMenu = StdNotifyCntxtMenuPassed;
            IconName = StdNotifyIconPassed.Text;
            IconLevel = LevelPassed;
            IconOrder = OrderPassed;
            StdKey = new StdKeyDef(IconLevel, IconOrder, IconName);
            BalloonTipText = NotifyIconItem.BalloonTipText;
            InitializeNotifiyIcon();
        }
        #endregion
        #region Initialize / Dispose
        public void InitializeNotifiyIcon()
        {
            if (StdKey == null)
            {
                IconLevel = "#"; IconOrder = "#"; IconName = "NotSet";
                StdKey = new StdKeyDef(IconLevel, IconOrder, IconName);
            }
            // Notes. FormParent already holds FormStd.
            if (Sender != null)
            {
                if (Sender is iStdBaseForm)
                {
                    FormStd = Sender as iStdBaseForm;
                }
                else if (Sender is StdBaseRunControlUiDef)
                {
                    StdRunControlUi = Sender as StdBaseRunControlUiDef;
                }
                else if (Sender is iStdBaseForm)
                {
                    FormStd = Sender as iStdBaseForm;
                }
            }
            // ToDo Get Icon.



            InitializeNotifiyIconIcon();
            ButtonTrayBuild();
        }
        public void InitializeNotifiyIconIcon()
        {
            if (FormStd != null)
            { FormStd.InitializeTopMost(ref StdProcessDef.Processes[StdProcess.StdKey.Key].StdScreen.ScreenObject); }
            IsUsed = true; VisibleAllowed = true;
            StdKey = new StdKeyDef(IconLevel, IconOrder, IconName);
            #region Manage standard objects
            // Notes. FormParent already holds FormStd.
            if (Sender != null && Sender is StdBaseRunControlUiDef)
            {
                StdRunControlUi = (StdBaseRunControlUiDef)Sender;
            }
            else if (st != null && st is iClassFeatures)
            {
                StdRunControlUi = ((iClassFeatures)st).StdRunControlUiGet();

            }
            else if (Sender != null && Sender is iStdBaseForm)
            {
                FormStd = Sender as iStdBaseForm;
                StdRunControlUi = ((iStdBaseForm)FormStd).StdRunControlUiGet();
            }
            #endregion
            InitializeIcon();
            InitializeButtonControl();
            Visible = true;
            //ItemNotifyIcon.Visible = Visible;
        }
        public void ButtonTrayBuild()
        {
            if (StdRunControlUi.StdNotify != null
                && StdRunControlUi.StdNotify.IsUsed
                && StdRunControlUi.StdNotify.VisibleAllowed)
            {
                StdRunControlUi.StdNotify.ButtonTrayBuild();
            }
        }
        public void StdNotifyAdd()
        {
            if (StdRunControlUi != null
                && StdRunControlUi.StdNotify != null
                && !StdRunControlUi.StdNotify.ContainsKey(StdKey.Key))
            {
                StdRunControlUi.StdNotify.Add(StdKey.Key, this);
            }
        }
        public void StdNotifyRemove()
        {
            if (StdRunControlUi != null
                && StdRunControlUi.StdNotify != null
                && StdRunControlUi.StdNotify.ContainsKey(StdKey.Key))
            {
                StdRunControlUi.StdNotify.Remove(StdKey.Key);
            }
        }
        public void InitializeButtonControl()
        {
            ButtonControl = new ToolStripButton();
            ButtonControl.Name = IconName;
            ButtonControl.ToolTipText = "?";
            try
            { ButtonControl.Image = Image.FromFile(@".\Resource\" + IconName + @".ico"); }
            catch (Exception)
            { ButtonControl.Image = Image.FromFile(@".\Resource\" + "IconError" + @".ico"); }
            //ButtonControl.Visible = Visible;
        }
        public void InitializeIcon()
        {
            if (Text == null) { Text = ""; }
            #region Icon
            if (NotifyIconItem != null)
            {
                NotifyIconItem.Visible = false;
                NotifyIconItem.Dispose();
            }
            NotifyIconItem = new NotifyIcon();
            try
            { NotifyIconItem.Icon = Icon.ExtractAssociatedIcon(@".\Resource\" + IconName + @".ico"); }
            catch (Exception)
            { NotifyIconItem.Icon = Icon.ExtractAssociatedIcon(@".\Resource\" + "IconError" + @".ico"); }
            NotifyIconItem.Visible = Visible;
            // NotifyIcon.Icon = new Icon("MdmControlLeft.ico");
            // NotifyIcon.Icon = Icon;
            //NotifyIcon.Icon = new Icon(Text + ".ico");
            // Context Menus
            NotifyIconItem.MouseClick += new MouseEventHandler(NotifyIcon_Click);
            // Context Menu
            menuItemClose = new MenuItem("Close", new EventHandler(NotifyIcon_CntxtClick));
            menuItemShow = new MenuItem("Show", new EventHandler(NotifyIcon_CntxtClick));
            menuItemClose = new MenuItem("Close All", new EventHandler(NotifyIcon_CntxtClick));
            if (CntxtMenu == null) { CntxtMenu = new ContextMenu(); }
            CntxtMenu.MenuItems.Add(menuItemClose);
            CntxtMenu.MenuItems.Add(menuItemShow);
            NotifyIconItem.ContextMenu = CntxtMenu;
            // Form
            if (Sender is Form)
            {
                ((Form)Sender).Icon = NotifyIconItem.Icon;
                ((Form)Sender).ShowIcon = Visible;
            }
            // new EventHandler(NotifyIcon_BalloonTipClosed);
            // (sender1, e) => { var thisIcon = (NotifyIcon)sender1; thisIcon.Visible = false; thisIcon.Dispose(); };
            //ItemNotifyIcon.Visible = Visible;
            #endregion
            #region Balloon Tip Handling
            NotifyIconItem.BalloonTipClosed += new EventHandler(NotifyIcon_BalloonTipClosed);
            if (NotifyIconItem.BalloonTipText.Length == 0)
            {
                NotifyIconItem.BalloonTipTitle = IconName; //  "My Sample Application";
                if (Text.Length > 0)
                { NotifyIconItem.BalloonTipText = Text; }
                else if (IconName.Length > 0)
                { NotifyIconItem.BalloonTipText = IconName; }
                else { NotifyIconItem.BalloonTipText = "IconLoadError"; }
            }
            NotifyIconItem.BalloonTipIcon = ToolTipIcon.Info;
            NotifyIconItem.ShowBalloonTip(500);
            #endregion
            //Visible = true;
        }
        public void Close()
        {

        }
        public new void Dispose()
        {
            Dispose(true);
        }
        public void Dispose(bool dummy)
        {
            if (StdRunControlUi != null
                && StdRunControlUi.StdNotify != null
                && StdRunControlUi.StdNotify.ContainsKey(StdKey.Key))
            {
                StdRunControlUi.StdNotify.Remove(StdKey.Key);
            }
            NotifyIconItem.Visible = false;
            NotifyIconItem.Dispose();
            base.Dispose();
        }
        #endregion
        #region Icon Show Hide
        public void Hide(object sender, EventArgs e)
        {
            HideDo(sender);
        }
        public void HideDo(object sender)
        {
            Visible = false;
        }
        public void Show(object sender, EventArgs e)
        {
            ShowDo(sender);
        }
        public void ShowDo(object sender)
        {
            Visible = true;
        }
        #endregion
        #region Icon Click
        private void NotifyIcon_Click(object sender, MouseEventArgs e)
        {
            NotifyIcon_ClickDo(sender);
        }
        public void NotifyIcon_ClickDo()
        {
            NotifyIcon_ClickDo(this);
        }
        public void NotifyIcon_ClickDo(object sender)
        {
            //NotifyIcon.BalloonTipText = "My application still working...";
            //NotifyIcon.BalloonTipTitle = "My Sample Application";
            //NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            //NotifyIcon.ShowBalloonTip(1000);
            //
            bool ThisTopMost = false;
            bool SameScreen = false;
            string ClassNameThis = this.GetType().Name;
            // System.IntPtr MainHandle = GetTopMostDef.GetWindowHandle("Shortcut Repair Browser");
            // Window window = Window.GetWindow((this);
            // var wih = new WindowInteropHelper(window);
            // IntPtr hWnd = wih.Handle;
            // StdProcess = ((iClassFeatures)st).StdProcessGet();
            // List<IntPtr> temp = GetTopMostDef.GetAllChildrenWindowHandles(MainHandle, 100);
            //if (!StdProcessDef.Screens.ContainsKey(StdProcess.Key))
            //{
            //    StdProcessDef.Screens.Add(StdProcess.Key, new ScreenDef(StdProcess.Key));
            //}
            if (IsUsed && VisibleAllowed)
            {
                //FormStd.InitializeTopMost(ref StdProcessDef.Processes[StdProcess.StdKey.Key].StdScreen.ScreenObject);
                if (StdProcessDef.WindowTopMost == StdProcess.StdKey.Key) { ThisTopMost = true; }
                if (!ThisTopMost)
                {
                    // WindowState 
                    FormStd.VisibleSet(true);
                    FormStd.Form_ShowFromTray();
                    FormStd.TopMostMake(this, ref FormStd.ScreenGet());
                }
                else
                {
                    if (!FormStd.VisibleGet())
                    {
                        FormStd.VisibleSet(true);
                        FormStd.Form_ShowFromTray();
                        FormStd.TopMostMake(this, ref FormStd.ScreenGet());
                    }
                    else
                    {
                        FormStd.VisibleSet(false);
                        FormStd.Form_HideToTray();
                        FormStd.TopMostPrevMake(this, ref FormStd.ScreenGet());
                    }
                }
            }
        }
        private void NotifyIcon_BalloonTipClosed(object sender, EventArgs e)
        {
            //Visible = false;
            //Dispose();
            //((NotifyIcon)sender).Visible = false;
            //((NotifyIcon)sender).Dispose();
        }
        #endregion
        // Other
        public void NotifyIconReset()
        {
            NotifyIconItem = new NotifyIcon();
            ButtonControl = new ToolStripButton();
            CntxtMenu = new ContextMenu();
            ImageName = "anonymous";
            IconName = "anonymous";
        }
        private void NotifyIcon_CntxtClick(object sender, EventArgs e)
        {
            // Menu:
            // Show (Form) MenuItem
            // Close (Form)
            // Close All
            //NotifyIcon_ClickDo(sender);
        }
    }
}
//
//NotifyIcon m_notifyIcon;
//m_notifyIcon = new NotifyIcon();
//m_notifyIcon.Text = text; // tooltip text show over tray icon
//m_notifyIcon.Visible = true;
//m_notifyIcon.Icon = icon; // icon in the tray
//m_notifyIcon.ContextMenu = menu; // context menu
