using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mdm.Oss.Components;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;

namespace Mdm.Oss.Components
{
    public class StdNotifyXXXDef
         : StdDictIndexDef<string, StdNotifyXXXIconDef>, IDisposable
    //: StdDictIndexDef<StdKey, StdNotifyIconDef>, IDisposable
    //: StdDictIndexDef<StdDictIndexDef<StdNotifyIconDef>>, IDisposable
    //: StdDictIndexDef<StdNotifyIconDef>, IDisposable
    //: StdBaseDef, IDisposable
    {
        public string aaaNotMarker;
        #region Static Fields - Tray and Dict
        // protected ContextMenu CntxtMenuTray;
        protected ContextMenuStrip CntxtMenuTray;
        #endregion
        #region Fields
        public string Name;
        public object ItemObject;
        //
        public static MenuStrip StdNotifyMenuStrip;
        public static ContextMenu StdNotifyCntxMenu;
        public static ToolStripMenuItem StdNotifyTray;
        public static int LevelShowMin = 1;
        public static int LevelShowApp = 2;
        public static int LevelShowMax = 9;
        //
        public StdNotifyXXXIconDef StdNotifyIcon;
        //public StdNotifyKeyDef StdNotifyKey;
        public bool IsUsed { get; set; }
        public bool VisibleAllowed { get; set; }
        public bool Visible { get; set; }
        public bool Closed;
        public string IconLevel;
        public string IconOrder;
        public string IconName;
        public int Offset;
        #endregion
        #region Standard Objects
        public object Sender;
        public object ConsoleSender;
        public object st;
        public StdProcessDef StdProcess;
        public StdKeyDef StdKey;
        public StdBaseRunControlUiDef StdRunControlUi;
        #endregion
        #region Build Tray
        public void ButtonTrayBuild()
        {
            string tmp = this[StdKey.Key].bVisible.ToString();

            bool tmpVisible;
            if (StdNotifyCntxMenu != null)
            {
                StdNotifyCntxMenu.MenuItems.Clear();
            }
            else
            {
                StdNotifyCntxMenu = new ContextMenu();
            }
            if (StdNotifyMenuStrip != null)
            {
                StdNotifyMenuStrip.Items.Clear();
            } else
            {
                StdNotifyMenuStrip = new MenuStrip();
            }
            //if (IsUsed && VisibleAllowed)
            //{
            //    StdNotifyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            //    StdNotifyIcon.ButtonControl});
            //}
            string KeyOrderPrev = "#";
            string KeyLevelPrev = "#";
            int ButtonOffsetPrev = 0;
            int IconOffsetPrev = 0;
            foreach (KeyValuePair<string, StdNotifyXXXIconDef> LocalNotifyIcon in this)
            {
                tmpVisible = true;
                LocalNotifyIcon.Value.InitializeNotifiyIcon();
                StdNotifyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
                    LocalNotifyIcon.Value.ButtonControl });
                StdNotifyCntxMenu.MenuItems.Add(
                    LocalNotifyIcon.Value.menuItemShow
                    );
                if (!LocalNotifyIcon.Value.IsUsed || !LocalNotifyIcon.Value.VisibleAllowed)
                { tmpVisible = false; }
                if (LocalNotifyIcon.Value.IconLevel[0] > StdNotifyXXXDef.LevelShowMax.ToString()[0]) { tmpVisible = false; }
                if (LocalNotifyIcon.Value.IconLevel[0] < StdNotifyXXXDef.LevelShowMin.ToString()[0]) { tmpVisible = false; }
                if (LocalNotifyIcon.Value.VisibleAlert)
                {
                    //LocalNotifyIcon.Value.VisibleAlert = false;
                    tmpVisible = true;
                }
                if (tmpVisible)
                {
                    if (LocalNotifyIcon.Value.IconLevel[0] > KeyLevelPrev[0])
                    {
                        KeyLevelPrev = LocalNotifyIcon.Value.IconLevel;
                        KeyOrderPrev = "#";
                        ButtonOffsetPrev += LocalNotifyIcon.Value.ButtonControl.Width;
                        LocalNotifyIcon.Value.ButtonOffset = ButtonOffsetPrev;
                        // Button Margin Left?

                        IconOffsetPrev += LocalNotifyIcon.Value.NotifyIconItem.Icon.Width;
                        LocalNotifyIcon.Value.IconOffset = IconOffsetPrev;
                        //LocalNotifyIcon.Value.ButtonControl.Padding.Bottom = IconOffsetPrev;
                        // Icon offset +/- based on tray on bottom/top
                        //tmpVisible = false; 
                    }

                    if (LocalNotifyIcon.Value.IconOrder[0] > KeyOrderPrev[0])
                    {
                        KeyOrderPrev = LocalNotifyIcon.Value.IconOrder;
                        //tmpVisible = false; 
                    }
                    //LocalNotifyIcon.Value.InitializeNotifiyIcon();
                    //StdNotifyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
                    //LocalNotifyIcon.Value.ButtonControl });
                    //LocalNotifyIcon.Value.Visible = true;
                    //LocalNotifyIcon.Value.NotifyIconItem.Visible = LocalNotifyIcon.Value.Visible;
                    //LocalNotifyIcon.Value.ButtonControl.Visible = LocalNotifyIcon.Value.Visible;
                }
                else
                {
                    //LocalNotifyIcon.Value.Visible = false;
                    //LocalNotifyIcon.Value.ItemNotifyIcon.Visible = false;
                    //LocalNotifyIcon.Value.ButtonControl.Visible = false;
                }
                //if (LocalNotifyIcon.Value.Visible != tmpVisible)
                //{ LocalNotifyIcon.Value.Visible = tmpVisible; }
                LocalNotifyIcon.Value.Visible = tmpVisible;
            }
            //StdNotifyTray.AddRange(new System.Windows.Forms.ToolStripButton[] {
            //     ButtonConsoleAll,
            //     ButtonConsoleSystem,
            //     ButtonConsoleUser,
            //     ButtonConsoleDatabase,
            //     ButtonConsoleError
            //});
        }
        #endregion
        #region Constructors, Init, Dispose
        static StdNotifyXXXDef()
        {
            //this["0"]["0"] = StdNotifyIcon;
            //this.
        }
        public StdNotifyXXXDef()
        {
            Sender = this;
            InitializeStdNotifiy();
        }
        public StdNotifyXXXDef(ref object SenderPassed, ref object stPassed, StdKeyDef StdKeyPassed)
        {
            Sender = SenderPassed;
            ConsoleSender = stPassed; st = stPassed;
            StdKey = StdKeyPassed;
            InitializeStdNotifiy();
        }
        public void InitializeStdNotifiy()
        {
            IsUsed = true;
            VisibleAllowed = true;
            Visible = false;
            Closed = false;
            Offset = 0;
            StdNotifyMenuStrip = new MenuStrip();
            StdNotifyTray = new ToolStripMenuItem();
            StdKey = new StdKeyDef("0", "0", "Root");
            StdNotifyIcon = new StdNotifyXXXIconDef(ref Sender, ref st, StdKey);
            this.Add(StdKey.Key, StdNotifyIcon);
            //new StdNotifyIconDef(ref Sender, ref st)
            //new StdNotifyDef(ref Sender, ref st)
            //)
            //StdDict = new Dictionary<string, mNotifyIconDef>();
            // StdDict. .Name = "Status Icons";
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
            //foreach (KeyValuePair<string, StdNotifyIconDef> LocalNotifyIcon in this)
            //{
            //    LocalNotifyIcon.Value.Dispose();
            //}
            //base.Dispose();
        }
        #endregion
    }
}
