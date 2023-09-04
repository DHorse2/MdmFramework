using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mdm.Oss.Components;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;

namespace Mdm.Oss.Components
{
    public class StdNotifyIconDef
        //: StdBaseDef, IDisposable
        : StdDictItemDef, IDisposable
    {
        #region Data and properties
        public static int OrderGlobal;
        //public T Item;
        public bool NewRecord;
        public string TaskType;
        public bool SetButtons;
        public int RowCount;
        #endregion
        #region Fields
        // private Mdm.Oss.ClipUtil.NotificationAreaIcon NotifyAreaIcon;
        // private System.Windows.NotificationAreaIcon;
        // private System.Windows.Forms.MessageBox;
        public NotifyIcon NotifyIconItem;
        public string ImageName;
        //
        public iStdBaseForm FormStd;
        public FormWindowState FormWindowStatePrev;
        public ToolStripButton ButtonControl;
        public int ButtonOffset;
        public ContextMenu CntxtMenu; // tray
        public bool CntxtMenuIsOpen;
        public MenuItem menuItemClose;
        public MenuItem menuItemShow;
        public MenuItem menuItemCloseAll;
        public bool IsUsed { get; set; }
        public bool IsNotifyGroup;
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
        public StdNotifyIconDef(
            ref object SenderPassed,
            ref object stPassed,
            StdKeyDef StdKeyPassed,
            string TitlePassed,
            bool IsNotifyGroupPassed)
        : base(ref SenderPassed, ref stPassed)
        {
            Title = TitlePassed;
            IsNotifyGroup = IsNotifyGroupPassed;
            if (StdKeyPassed != null)
            {
                StdKey = StdKeyPassed;
                StdKey.GetTo(ref IconLevel, ref IconOrder, ref IconName);
            }
            else
            {
                IconLevel = "#"; IconOrder = "#"; IconName = "NotSet";
            }
            // Notes. FormParent already holds FormStd.
            if (SenderPassed != null && SenderPassed is iStdBaseForm) { FormStd = SenderPassed as iStdBaseForm; }
            InitializeStdNotifiyIcon();
        }
        public StdNotifyIconDef(
            ref object SenderPassed,
            ref object stPassed,
            string LevelPassed,
            string OrderPassed,
            string NamePassed,
            string TitlePassed,
            bool IsNotifyGroupPassed)
        : base(ref SenderPassed, ref stPassed)
        {
            IsNotifyGroup = IsNotifyGroupPassed;
            IconLevel = LevelPassed;
            IconOrder = OrderPassed;
            IconName = NamePassed;
            Title = TitlePassed;
            StdKey = new StdKeyDef(IconLevel, IconOrder, IconName);
            // Notes. FormParent already holds FormStd.
            if (SenderPassed != null && SenderPassed is iStdBaseForm) { FormStd = SenderPassed as iStdBaseForm; }
            InitializeStdNotifiyIcon();
        }
        #endregion
        #region Initialize / Dispose
        public void InitializeStdNotifiyIcon()
        {
            #region Manage standard objects
            if (StdKey == null)
            {
                StdKey = new StdKeyDef(IconLevel, IconOrder, IconName);
            }
            // Notes. FormParent already holds FormStd.
            if (Sender != null)
            {
                if (Sender is StdNotifyDef)
                {
                    StdNotify = Sender as StdNotifyDef;
                    StdNotifyRoot = StdNotify.Root;
                    StdRunControlUi = StdNotify.StdRunControlUi;
                }
                else if (Sender is StdBaseRunControlUiDef)
                {
                    StdRunControlUi = Sender as StdBaseRunControlUiDef;
                    StdNotify = StdRunControlUi.StdNotify;
                    StdNotifyRoot = StdNotify.Root;
                }
                else if (Sender is iStdBaseForm)
                {
                    FormStd = Sender as iStdBaseForm;
                    if (st == null) { st = FormStd.ConsoleGet(); }
                    StdRunControlUi = FormStd.StdRunControlUiGet();
                    if (StdRunControlUi == null)
                    { StdRunControlUi = ((iClassFeatures)FormStd.ConsoleGet()).StdRunControlUiGet(); }
                    StdNotify = FormStd.StdNotifyGet("this");
                    StdNotifyRoot = FormStd.StdNotifyGet("root");
                }
                else
                {
                    //FormStd = Sender as Form;
                }
            }
            if (StdNotify == null
                && StdRunControlUi != null
                && StdRunControlUi.StdNotify != null)
            {
                StdNotify = StdRunControlUi.StdNotify;
                StdNotifyRoot = StdNotify.Root;
            }
            if (StdNotify == null
                && st != null
                && ((iClassFeatures)st).StdRunControlUiGet().StdNotify != null)
            {
                StdNotify = ((iClassFeatures)st).StdRunControlUiGet().StdNotify;
                StdNotifyRoot = StdNotify.Root;
            }
            //if (FormStd == null && FormParent != null)
            //{
            //    FormStd = FormParent;
            //}
            #endregion
            InitializeStdNotify();
            ButtonTrayBuild();
        }
        public void InitializeStdNotify()
        {
            if (StdKey == null) { StdKey = new StdKeyDef(IconLevel, IconOrder, IconName); }
            Name = IconName;
            if (StdNotify != null)
            {
                if (!StdNotify.Items.ContainsKey(StdKey.Key))
                {
                    StdNotify.Items.Add(StdKey.Key, this);
                }
            }
            //
            //if (FormStd != null)
            //{ FormStd.TopMostInitialize(ref StdProcessDef.Processes[StdKey.Key].StdScreen.ScreenObject); }
            //
            IsUsed = true;
            VisibleAllowed = true;
            InitializeIconControl();
            InitializeButtonControl();
            Visible = true;
            //ItemNotifyIcon.Visible = Visible;
        }
        public void Alert()
        { Alert("", 0); }
        public void Alert(string TextPassed, int Duration)
        {
            if (TextPassed.Length > 0)
            { NotifyIconItem.BalloonTipText = TextPassed; }
            if (Duration <= 0) { Duration = 500; }
            if (NotifyIconItem.BalloonTipText.Length > 0)
            { NotifyIconItem.ShowBalloonTip(Duration); }
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
        public void InitializeIconControl()
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
            {
                NotifyIconItem.Icon = Icon.ExtractAssociatedIcon(@".\Resource\" + IconName + @".ico");
                if (FormStd != null)
                { ((Form)FormStd).Icon = Icon.ExtractAssociatedIcon(@".\Resource\" + IconName + @".ico"); }
            }
            catch (Exception)
            {
                try
                {
                    NotifyIconItem.Icon = Icon.ExtractAssociatedIcon(@".\Resource\" + "IconError" + @".ico");
                    if (FormStd != null)
                    { ((Form)FormStd).Icon = Icon.ExtractAssociatedIcon(@".\Resource\" + "IconError" + @".ico"); }
                }
                catch (Exception) { } // No icons present.
            }
            NotifyIconItem.Visible = Visible;
            // NotifyIcon.Icon = new Icon("MdmControlLeft.ico");
            // NotifyIcon.Icon = Icon;
            //NotifyIcon.Icon = new Icon(Text + ".ico");
            // Context Menus
            NotifyIconItem.MouseClick += new MouseEventHandler(NotifyIcon_Click);
            NotifyIconItem.MouseDown += new MouseEventHandler(NotifyIcon1_MouseDown);
            NotifyIconItem.MouseDoubleClick += new MouseEventHandler(NotifyIcon_DoubleClick);
            // Context Menu
            menuItemClose = new MenuItem("Close" + IconName, new EventHandler(NotifyIcon_CntxtClick));
            menuItemShow = new MenuItem("Show" + IconName, new EventHandler(NotifyIcon_CntxtClick));
            menuItemCloseAll = new MenuItem("Close All", new EventHandler(NotifyIcon_CntxtClick));
            //
            if (CntxtMenu == null) { 
                CntxtMenu = new ContextMenu(); 
            } else
            {
                CntxtMenu.MenuItems.Clear();
            }
            CntxtMenu.MenuItems.Add(menuItemCloseAll);
            CntxtMenu.MenuItems.Add(menuItemClose);
            CntxtMenu.MenuItems.Add(menuItemShow);
            //
            if (NotifyIconItem.ContextMenu == null)
            {
                NotifyIconItem.ContextMenu = new ContextMenu();
            }
            else
            {
                NotifyIconItem.ContextMenu.MenuItems.Clear();
            }
            NotifyIconItem.ContextMenu.MergeMenu(CntxtMenu);
            // Form
            //if (FormStd != null)
            //{
            //    ((Form)FormStd).Icon = NotifyIconItem.Icon;
            //    ((Form)FormStd).ShowIcon = Visible;
            //}
            // new EventHandler(NotifyIcon_BalloonTipClosed);
            // (sender1, e) => { var thisIcon = (NotifyIcon)sender1; thisIcon.Visible = false; thisIcon.Dispose(); };
            //ItemNotifyIcon.Visible = Visible;
            #endregion
            #region Balloon Tip Handling
            NotifyIconItem.BalloonTipClosed += new EventHandler(NotifyIcon_BalloonTipClosed);
            if (NotifyIconItem.BalloonTipText.Length == 0)
            {
                NotifyIconItem.BalloonTipTitle = IconName; //  "My Sample Application";
                if (Title.Length > 0)
                { NotifyIconItem.BalloonTipText = Title; }
                else if (Text.Length > 0)
                { NotifyIconItem.BalloonTipText = Text; }
                else if (IconName.Length > 0)
                { NotifyIconItem.BalloonTipText = IconName; }
                else { NotifyIconItem.BalloonTipText = "IconLoadError"; }
            }
            NotifyIconItem.BalloonTipIcon = ToolTipIcon.None; // NotifyIconItem.Icon; // ToolTipIcon.Info;
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
            StdNotifyRemove();
            NotifyIconItem.Visible = false;
            NotifyIconItem.Dispose();
            base.Dispose();
        }
        #endregion
        public void ButtonTrayBuild()
        {
            if (StdNotify != null
                && StdNotify.IsUsed)
            {
                StdNotify.ButtonTrayBuild();
            }
        }
        public void StdNotifyAdd()
        {
            if (StdNotify != null
                && !StdNotify.Items.ContainsKey(StdKey.Key))
            {
                StdNotify.Items.Add(StdKey.Key, StdNotifyIcon);
            }
        }
        public void StdNotifyRemove()
        {
            if (StdNotify != null
                && StdNotify.Items.ContainsKey(StdKey.Key))
            {
                StdNotify.Items.Remove(StdKey.Key);
            }
        }
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
        private void NotifyIcon_DoubleClick(object sender, MouseEventArgs e)
        {
            NotifyIcon_ClickDo(sender);
        }
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
            bool ThisTopMost = false;
            bool SameScreen = false;
            string ClassNameThis = this.GetType().Name;
            if (IsNotifyGroup)
            {
                // This icon is a Group and holds
                // one ore more TrayIcons and/or Groups.
                // IE. It's recursive.
                if (IsUsed)
                {
                    // Toggle the Group's Tray Icons
                    // and visible forms.
                    if (StdNotify.NotifyGroupOpen)
                    { StdNotify.NotifyGroupOpen = false; }
                    else
                    { StdNotify.NotifyGroupOpen = true; }
                    StdNotifyDef.ButtonTrayGroupBuild(ref StdNotify, false, true);
                }
            }
            else
            // This is a Tray Notify Icon.
            // It normally is linked to a form
            // but could also be a service.
            {
                if (IsUsed && VisibleAllowed)
                {
                    // Form. Not all icons have forms.
                    if (FormStd != null)
                    {
                        //FormStd.InitializeTopMost(ref StdProcessDef.Processes[StdProcess.StdKey.Key].StdScreen.ScreenObject);
                        if (StdProcessDef.WindowTopMost == StdKey.Key) { ThisTopMost = true; }
                        if (!ThisTopMost)
                        {
                            // WindowState 
                            //FormStd.VisibleSet(true);
                            FormStd.Form_ShowFromTray(false);
                            FormStd.TopMostMake(this, ref FormStd.ScreenGet());
                        }
                        else
                        {
                            if (!FormStd.VisibleGet())
                            {
                                //FormStd.VisibleSet(true);
                                FormStd.Form_ShowFromTray(false);
                                FormStd.TopMostMake(this, ref FormStd.ScreenGet());
                            }
                            else
                            {
                                //FormStd.VisibleSet(false);
                                FormStd.Form_HideToTray(false);
                                FormStd.TopMostPrevMake(this, ref FormStd.ScreenGet());
                            }
                        }
                    }
                    // There is currently no other functions
                    // attached to the Notify Icon aside from
                    // its context menu.
                }
            }
            #region Comments
            //NotifyIcon.BalloonTipText = "My application still working...";
            //NotifyIcon.BalloonTipTitle = "My Sample Application";
            //NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            //NotifyIcon.ShowBalloonTip(1000);
            //
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
            #endregion
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
        private void NotifyIcon1_MouseUp(Object Sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //do something
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (Sender is NotifyIcon)
                {
                    if (((NotifyIcon)Sender).ContextMenuStrip != null)
                    {
                        //((NotifyIcon)Sender).ContextMenuStrip.Show();
                        ((NotifyIcon)Sender).ContextMenuStrip.Hide();
                    }
                }
                else if (Sender is Control)
                {
                    CntxtMenu.Show(((Control)Sender), new Point(
                            e.X + ((Control)Sender).Left + 20,
                            e.Y + ((Control)Sender).Top + 20));
                    CntxtMenuIsOpen = true;
                }
            }
        }
        private void NotifyIcon1_MouseDown(Object Sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //do something
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (Sender is NotifyIcon)
                {
                    if (((NotifyIcon)Sender).ContextMenuStrip != null)
                    {
                        ((NotifyIcon)Sender).ContextMenuStrip.Show();
                        //((NotifyIcon)Sender).ContextMenuStrip.Hide();
                    }
                }
                else if (Sender is Control)
                {
                    CntxtMenu.Show(((Control)Sender), new Point(
                            e.X + ((Control)Sender).Left + 20,
                            e.Y + ((Control)Sender).Top + 20));
                    CntxtMenuIsOpen = true;
                }
            }
            #region Comments
            //if (!CntxtMenuIsOpen)
            //{
            //    CntxtMenu.Show();
            //}
            //else
            //{
            //    CntxtMenu.Collapse fire
            //}
            //System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            //if (Sender is Icon)
            //{
            //    Icon IconPassed = Sender as Icon;
            //    messageBoxCS.AppendFormat("{0} = {1}", "Size", IconPassed.Size.ToString());

            //} else if (Sender is System.Windows.Forms.Control)
            //{
            //    Point Loc = ((System.Windows.Forms.Control)Sender).Location;
            //    messageBoxCS.AppendFormat("{0} = {1}", "Loc", Loc.ToString());
            //}

            //messageBoxCS.AppendFormat("{0} = {1}", "Button", e.Button);
            //messageBoxCS.AppendLine();
            //messageBoxCS.AppendFormat("{0} = {1}", "Clicks", e.Clicks);
            //messageBoxCS.AppendLine();
            //messageBoxCS.AppendFormat("{0} = {1}", "X", e.X);
            //messageBoxCS.AppendLine();
            //messageBoxCS.AppendFormat("{0} = {1}", "Y", e.Y);
            //messageBoxCS.AppendLine();
            //messageBoxCS.AppendFormat("{0} = {1}", "Delta", e.Delta);
            //messageBoxCS.AppendLine();
            //messageBoxCS.AppendFormat("{0} = {1}", "Location", e.Location);
            //messageBoxCS.AppendLine();
            //MessageBox.Show(messageBoxCS.ToString(), "MouseClick Event");
            #endregion
        }
        public override string ToString()
        {
            return StdKey.Key + Title;
            //return base.ToString();
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
