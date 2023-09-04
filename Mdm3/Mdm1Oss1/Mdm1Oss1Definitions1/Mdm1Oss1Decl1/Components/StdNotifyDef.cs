using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mdm.Oss.Components;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;

namespace Mdm.Oss.Components
{
    public class StdNotifyDef
         : StdDictIndexDef<string, StdNotifyDef>, IDisposable
    //: StdDictIndexDef<StdKey, StdNotifyIconDef>, IDisposable
    //: StdDictIndexDef<StdDictIndexDef<StdNotifyIconDef>>, IDisposable
    //: StdDictIndexDef<StdNotifyIconDef>, IDisposable
    //: StdBaseDef, IDisposable
    {
        public string aaaNotMarker;
        public string Title;

        public StdDictIndexDef<string, StdNotifyIconDef> Items;
        //public StdDictItemArrayDef<StdNotifyIconExDef> sdArray;
        // alternatively you can create ordered list and ignore Items.
        public List<StdNotifyIconDef> ItemsArray;
        public bool UsesArray;
        #region Static Fields - Tray and Dict
        #region alternative Root struct
        // struct ?
        //public class RootDef
        //{
        //    public StdKeyDef StdKey;
        //    public StdNotifyDef StdNotify;
        //    public MenuStrip NotifyMenuStrip;
        //    public ContextMenu NotifyCntxMenu;
        //    public ToolStripMenuItem NotifyTray;
        //    public void ControlReset()
        //    {
        //        if (NotifyMenuStrip == null) {
        //            NotifyMenuStrip = new MenuStrip();
        //            NotifyCntxMenu = new ContextMenu();
        //            NotifyTray = new ToolStripMenuItem();
        //        } else
        //        {
        //            NotifyMenuStrip.Items.Clear();
        //            NotifyCntxMenu.MenuItems.Clear();
        //            NotifyTray = new ToolStripMenuItem();
        //        }

        //    }
        //    public RootDef(StdKeyDef StdKeyPassed)
        //    {
        //        if (StdKeyPassed == null)
        //        { StdKey = new StdKeyDef("0", "0", "Root"); }
        //        else { StdKey = StdKeyPassed; }
        //        StdNotify = null;
        //        ControlReset();
        //    }
        //    public RootDef(StdNotifyDef StdNotifyPassed, StdKeyDef StdKeyPassed)
        //    {
        //        if (StdKeyPassed == null)
        //        { StdKey = new StdKeyDef("0", "0", "Root"); }
        //        else { StdKey = StdKeyPassed; }
        //        StdNotify = StdNotifyPassed;
        //        ControlReset();
        //    }
        //}
        //public RootDef Root;
        #endregion
        public StdNotifyDef Root;
        #endregion
        #region Local Controls
        public MenuStrip NotifyMenuStrip;
        public ToolStripMenuItem NotifyTray;
        //
        public ContextMenu NotifyCntxMenu;
        public MenuItem menuItemClose;
        public MenuItem menuItemShow;
        public MenuItem menuItemCloseAll;
        //
        public void ControlReset()
        {
            if (NotifyMenuStrip == null)
            {
                NotifyMenuStrip = new MenuStrip();
                NotifyCntxMenu = new ContextMenu();
                NotifyTray = new ToolStripMenuItem();
            }
            else
            {
                NotifyMenuStrip.Items.Clear();
                NotifyCntxMenu.MenuItems.Clear();
                NotifyTray = new ToolStripMenuItem();
            }

        }
        #endregion
        #region Fields
        public string Name;
        public object ItemObject;
        //
        // protected ContextMenu CntxtMenuTray;
        protected ContextMenuStrip CntxtMenuTray;
        public static int LevelShowMin = 0;
        public static int LevelShowApp = 2;
        public static int LevelShowMax = 9;
        //
        public StdNotifyIconDef StdNotifyIcon;
        //public StdNotifyKeyDef StdNotifyKey;
        public bool IsUsed { get; set; }
        public bool VisibleAllowed { get; set; }
        public bool Visible { get; set; }
        public bool IsNotifyGroup;
        public bool NotifyGroupOpen;
        public bool HideItemsOnClick;
        public int ItemsArrayMax;
        public bool Open;
        public bool Closed;
        public string IconLevel;
        public string IconOrder;
        public string IconName;
        public int Offset;
        #endregion
        #region Standard Objects
        public object Sender;
        public object SenderIsThis;
        public object ConsoleSender;
        public object st;
        public StdProcessDef StdProcess;
        public StdKeyDef StdKey;
        public StdBaseRunControlUiDef StdRunControlUi;
        #endregion
        // this
        public void StdNotifyAdd()
        {
            // not used
            if (!ContainsKey(StdKey.Key))
            {
                Add(StdKey.Key, this);
            }
        }
        public void StdNotifyRemove()
        {
            if (ContainsKey(StdKey.Key))
            {
                Remove(StdKey.Key);
            }
        }

        #region Build Tray
        public void ButtonTrayGroupHide(ref StdNotifyDef StdNotifyObjectPassed, bool IsVisible)
        {
            bool First = true;
            bool tmpVisible = false;
            StdNotifyDef LocalNotifyGroup = StdNotifyObjectPassed as StdNotifyDef;
            StdNotifyDef StdNotifyTmp;

            foreach (KeyValuePair<string, StdNotifyDef> LocalNotify in LocalNotifyGroup)
            {
                if (LocalNotify.Value.IsUsed
                    && LocalNotify.Value.VisibleAllowed

                    && (!LocalNotify.Value.HideItemsOnClick
                    || LocalNotify.Value.Open)
                    )
                {
                    if (First)
                    {

                        foreach (StdNotifyIconDef LocalNotifyIcon in LocalNotify.Value.ItemsArray)
                        {
                            tmpVisible = IsVisible;
                            LocalNotifyIcon.InitializeStdNotifiyIcon();
                            NotifyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
                    LocalNotifyIcon.ButtonControl });
                            NotifyCntxMenu.MenuItems.Add(
                                LocalNotifyIcon.menuItemShow
                                );
                            if (!LocalNotifyIcon.IsUsed || !LocalNotifyIcon.VisibleAllowed)
                            { tmpVisible = false; }
                            if (LocalNotifyIcon.IconLevel[0] > StdNotifyDef.LevelShowMax.ToString()[0]) { tmpVisible = false; }
                            if (LocalNotifyIcon.IconLevel[0] < StdNotifyDef.LevelShowMin.ToString()[0]) { tmpVisible = false; }
                            if (LocalNotifyIcon.VisibleAlert)
                            {
                                //LocalNotifyIcon.VisibleAlert = false;
                                tmpVisible = true;
                            }
                            LocalNotifyIcon.Visible = tmpVisible;
                        }
                        First = false;
                    }
                    else
                    {
                        StdNotifyTmp = LocalNotify.Value;
                        if (StdNotifyTmp.IsNotifyGroup)
                        {
                            ButtonTrayGroupHide(ref StdNotifyTmp, IsVisible);
                        }
                    }
                    First = false;
                }
            }

        }
        static public void ButtonTrayGroupBuild(ref StdNotifyDef NotifyGroupPassed, bool BuildControls, bool UseGroupVisible)
        {
            if (NotifyGroupPassed == null) { return; }
            bool tmpVisible;
            string KeyOrderPrev = "#";
            string KeyLevelPrev = "#";
            int ButtonOffsetPrev = 0;
            int IconOffsetPrev = 0;
            bool First = true;
            StdNotifyDef LocalNotifyGroup = NotifyGroupPassed as StdNotifyDef;
            StdNotifyIconDef LocalNotifyIcon;
            StdNotifyDef StdNotifyTmp;
            //
            if (BuildControls) { ButtonTrayReset(ref LocalNotifyGroup); }
            //
            foreach (KeyValuePair<string, StdNotifyIconDef> LocalNotifyItem in LocalNotifyGroup.Items)
            {
                LocalNotifyIcon = LocalNotifyItem.Value;
                if (LocalNotifyIcon.IsUsed)
                {

                    if (UseGroupVisible)
                    {
                        if (LocalNotifyIcon.StdKey.Key == LocalNotifyGroup.StdKey.Key)
                        {
                            tmpVisible = true;
                        }
                        else
                        {
                            tmpVisible = LocalNotifyGroup.NotifyGroupOpen;
                        }
                    }
                    else
                    {
                        tmpVisible = true;
                        if (!LocalNotifyIcon.IsUsed || !LocalNotifyIcon.VisibleAllowed)
                        { tmpVisible = false; }
                        if (LocalNotifyIcon.IconLevel[0] > StdNotifyDef.LevelShowMax.ToString()[0]) { tmpVisible = false; }
                        if (LocalNotifyIcon.IconLevel[0] < StdNotifyDef.LevelShowMin.ToString()[0]) { tmpVisible = false; }
                        if (LocalNotifyIcon.VisibleAlert)
                        {
                            //LocalNotifyIcon.VisibleAlert = false;
                            tmpVisible = true;
                        }
                    }
                    if (!LocalNotifyIcon.VisibleAllowed) { tmpVisible = false; }
                    // Process result
                    if (tmpVisible)
                    {
                        if (LocalNotifyIcon.IconLevel[0] > KeyLevelPrev[0])
                        {
                            KeyLevelPrev = LocalNotifyIcon.IconLevel;
                            KeyOrderPrev = "#";
                            ButtonOffsetPrev += LocalNotifyIcon.ButtonControl.Width;
                            LocalNotifyIcon.ButtonOffset = ButtonOffsetPrev;
                            // Button Margin Left?

                            IconOffsetPrev += LocalNotifyIcon.NotifyIconItem.Icon.Width;
                            LocalNotifyIcon.IconOffset = IconOffsetPrev;
                            //LocalNotifyIcon.ButtonControl.Padding.Bottom = IconOffsetPrev;
                            // Icon offset +/- based on tray on bottom/top
                            //tmpVisible = false; 
                        }

                        if (LocalNotifyIcon.IconOrder[0] > KeyOrderPrev[0])
                        { KeyOrderPrev = LocalNotifyIcon.IconOrder; }
                        //LocalNotifyIcon.InitializeStdNotifiyIcon();
                    }
                    //
                    if (BuildControls)
                    {
                        StdNotifyDef.ButtonTrayAdd(ref LocalNotifyGroup, ref LocalNotifyIcon);
                    }
                    // Check if thes Visible State has actuall changed.
                    if (LocalNotifyIcon.Visible != tmpVisible)
                    {
                        LocalNotifyIcon.Visible = tmpVisible;
                        LocalNotifyIcon.NotifyIconItem.Visible = LocalNotifyIcon.Visible;
                        LocalNotifyIcon.ButtonControl.Visible = LocalNotifyIcon.Visible;
                        // Is a Form attached to the Notify Icon.
                        if (LocalNotifyIcon.FormStd != null
                            && LocalNotifyIcon.Visible != LocalNotifyIcon.FormStd.VisibleGet())
                        {
                            if (UseGroupVisible)
                            { LocalNotifyIcon.FormStd.StdNotifyGroupAction(LocalNotifyIcon.Visible); }
                        }
                    }
                }
            }
            // Process any Notifiy Child Groups for this Group
            // "this" is not a member of its own set (Groups)
            First = true;
            foreach (KeyValuePair<string, StdNotifyDef> LocalNotify in LocalNotifyGroup)
            {
                if (LocalNotify.Value.IsUsed)
                {
                    StdNotifyTmp = LocalNotify.Value;
                    if (StdNotifyTmp.IsNotifyGroup)
                    {
                        if (UseGroupVisible) { StdNotifyTmp.NotifyGroupOpen = LocalNotifyGroup.NotifyGroupOpen; }
                        ButtonTrayGroupBuild(ref StdNotifyTmp, BuildControls, UseGroupVisible);
                    }
                }
                First = false;
            }
        }
        public static void ButtonTrayReset(ref StdNotifyDef LocalNotifyGroup)
        {
            if (LocalNotifyGroup.NotifyCntxMenu != null)
            {
                LocalNotifyGroup.NotifyCntxMenu.MenuItems.Clear();
            }
            else
            {
                LocalNotifyGroup.NotifyCntxMenu = new ContextMenu();
            }
            if (LocalNotifyGroup.NotifyMenuStrip != null)
            {
                LocalNotifyGroup.NotifyMenuStrip.Items.Clear();
            }
            else
            {
                LocalNotifyGroup.NotifyMenuStrip = new MenuStrip();
            }
            //if (IsUsed && VisibleAllowed)
            //{
            //    StdNotifyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            //    StdNotifyIcon.ButtonControl});
            //}
        }
        public static void ButtonTrayAdd(ref StdNotifyDef LocalNotifyGroup, ref StdNotifyIconDef LocalNotifyIcon)
        {
            bool BuildControls = true;
            if (BuildControls)
            {
                LocalNotifyGroup.NotifyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
                                LocalNotifyIcon.ButtonControl });
                //
                LocalNotifyGroup.NotifyCntxMenu.MergeMenu(LocalNotifyIcon.CntxtMenu);
                //foreach (MenuItem IconMenuItem in LocalNotifyGroup.StdNotifyCntxMenu.MenuItems)
                //{
                //    LocalNotifyGroup.StdNotifyCntxMenu.MenuItems.Add(
                //        IconMenuItem);
                //}
                //LocalNotifyGroup.StdNotifyCntxMenu.MenuItems.Add(
                //    LocalNotifyIcon.menuItemShow);
            }
        }
        public void ButtonTrayBuild()
        {
            StdNotifyDef.ButtonTrayGroupBuild(ref Root, true, true);
        }
        #endregion
        public void InitializeStdNotifiy()
        {
            SenderIsThis = this;
            if (StdKey == null)
            {
                if (IconName == null)
                {
                    StdKey = new StdKeyDef("#", "#", "NotSet");
                }
                else
                {
                    StdKey = new StdKeyDef(IconLevel, IconOrder, IconName);
                }
            }
            else
            {
                StdKey.GetTo(ref IconLevel, ref IconOrder, ref IconName);
            }
            Name = StdKey.IconName;
            if (Root == null)
            {
                // Get it from st?
                //Root = new RootDef(this, StdKey); 
                //Root = this;
            }
            if (Items == null)
            {
                Items = new StdDictIndexDef<string, StdNotifyIconDef>();
            }
            else
            {
                //Items.Clear();
            }
            //else { Root.ControlReset(); }
            // Fields
            ItemsArrayMax = 20;
            ItemsArray = new List<StdNotifyIconDef>(); // [ItemsArrayMax];
            IsUsed = true;
            VisibleAllowed = true;
            Visible = false;
            NotifyGroupOpen = true;
            Open = false;
            Closed = false;
            Offset = 0;
            ControlReset();
            if (StdNotifyIcon == null) { StdNotifyIcon = new StdNotifyIconDef(ref SenderIsThis, ref st, StdKey, Title, true); }

            //this.Add(StdKey.Key, (StdNotifyExDef)SenderIsThis);
            if (!this.Items.ContainsKey(StdKey.Key))
            { this.Items.Add(StdKey.Key, StdNotifyIcon); }
            //this.Add(StdKey.Key, StdNotifyIcon);
            //new StdNotifyIconDef(ref Sender, ref st)
            //new StdNotifyDef(ref Sender, ref st)
            //)
            //StdDict = new Dictionary<string, mNotifyIconDef>();
            // StdDict. .Name = "Status Icons";
        }
        #region Constructors, Init, Dispose
        //static StdNotifyDef()
        //{
        //    //StdNotifyRoot = new StdNotifyDef(new StdKeyDef("0", "0", "Root"), true);
        //    //StdNotifyMenuStrip = new MenuStrip();
        //    //StdNotifyCntxMenu = new ContextMenu();
        //    //StdNotifyTray = new ToolStripMenuItem();
        //    //this["0"]["0"] = StdNotifyIcon;
        //    //this.
        //}
        public StdNotifyDef(
            //ref object SenderPassed, ref object stPassed, StdKeyDef StdKeyPassed)
            ref object SenderPassed,
            ref object stPassed,
            StdKeyDef StdKeyPassed,
            string TitlePassed,
            bool IsNotifyGroupPassed)
        {
            Sender = SenderPassed;
            ConsoleSender = stPassed; st = stPassed;
            StdKey = StdKeyPassed;
            Title = TitlePassed;
            IsNotifyGroup = IsNotifyGroupPassed;
            InitializeStdNotifiy();
        }
        public StdNotifyDef(
            StdKeyDef StdKeyPassed,
            string TitlePassed,
            bool IsNotifyGroupPassed)
        {
            Sender = this;
            StdKey = StdKeyPassed;
            Title = TitlePassed;
            IsNotifyGroup = IsNotifyGroupPassed;
            InitializeStdNotifiy();
        }
        public StdNotifyDef()
        {
            Sender = this;
            Title = "";
            IsNotifyGroup = false;
            InitializeStdNotifiy();
        }
        public void Close()
        {

        }
        public void Dispose()
        {
            Dispose(true);
        }
        public void Dispose(bool Ifurget)
        {
            StdNotifyIcon.Dispose();
            //foreach (KeyValuePair<string, StdNotifyIconDef> LocalNotifyIcon in this)
            //{
            //    LocalNotifyIcon.Value.Dispose();
            //}
            //base.Dispose();
        }
        #endregion
        public override string ToString()
        {
            return (
                "Key" + StdKey.Key + " " +
                base.ToString());
        }
    }
}
