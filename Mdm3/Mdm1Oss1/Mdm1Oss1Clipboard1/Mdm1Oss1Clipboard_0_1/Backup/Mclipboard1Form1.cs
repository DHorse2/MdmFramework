using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Mdm.Oss.ShellUtil.BaseUtil;

namespace Mdm.Oss.ClipboardUtil {

    #region Clipboard Notes
    /// <summary>
    /// Clipboard Monitor Example 
    /// Copyright (c) Ross Donald 2003
    /// ross@radsoftware.com.au
    /// http://www.radsoftware.com.au
    /// <br/>
    /// Demonstrates how to create a clipboard monitor in C#. Whenever an item is copied
    /// to the clipboard by any application this form will be notified by a call to 
    /// the WindowProc method with the WM_DRAWCLIPBOARD message allowing this form to
    /// read the contents of the clipboard and perform some processing.
    /// </summary>
    /// <remarks>
    /// This application has some functionality beyond a simple example. When an item is copied
    /// to the clipboard this application loOks for hyperlinks, unc paths or email addresses 
    /// then displays a balloon dialog (Windows XP only) showing the link that was found.
    /// The icon in the system tray area can be clicked to display a menu of the found links.
    /// <br/>
    /// This source code is a work in progress and comes without warranty expressed or implied.
    /// It is an attempt to demonstrate a concept, not to be a finished application.
    /// </remarks>
    /// 
    #endregion

    public partial class Mclipboard1Form1 : Form {

        #region Clipboard Formats
        #region Format Description Indexing
        String[] formatsAll = new String[] 
		{
			DataFormats.Bitmap,
			DataFormats.CommaSeparatedValue,
			DataFormats.Dib,
			DataFormats.Dif,
			DataFormats.EnhancedMetafile,
			DataFormats.FileDrop,
			DataFormats.Html,
			DataFormats.Locale,
			DataFormats.MetafilePict,
			DataFormats.OemText,
			DataFormats.Palette,
			DataFormats.PenData,
			DataFormats.Riff,
			DataFormats.Rtf,
			DataFormats.Serializable,
			DataFormats.StringFormat,
			DataFormats.SymbolicLink,
			DataFormats.Text,
			DataFormats.Tiff,
			DataFormats.UnicodeText,
			DataFormats.WaveAudio
		};
        #endregion
        #region Format Description Words
        String[] formatsAllDesc = new String[] 
		{
			"Bitmap",
			"CommaSeparatedValue",
			"Dib",
			"Dif",
			"EnhancedMetafile",
			"FileDrop",
			"Html",
			"Locale",
			"MetafilePict",
			"OemText",
			"Palette",
			"PenData",
			"Riff",
			"Rtf",
			"Serializable",
			"StringFormat",
			"SymbolicLink",
			"Text",
			"Tiff",
			"UnicodeText",
			"WaveAudio"
		};
        #endregion
        #endregion

        #region Constants
        // internal static WndProcMsgsFillDict odMsgDescs;
        // internal static class WndProcMsgsFillDict {
        internal void WndProcMsgsFillDict(Dictionary<string, Int32> EnumMsgs) {
            // Create a new dictionary of strings, with String keys.
            //
            // Dictionary<string, Int32> EnumMsgs = new Dictionary<string, Int32>();

            // Add some elements to the dictionary. There are no 
            // duplicate keys, but some of the values are duplicates.
            // EnumMsgs.Add("txt", 0);

            #region Copy of Enum Msgs
            EnumMsgs.Add("NULL", 0x0000);
            EnumMsgs.Add("CREATE", 0x0001);
            EnumMsgs.Add("DESTROY", 0x0002);
            EnumMsgs.Add("MOVE", 0x0003);
            EnumMsgs.Add("SIZE", 0x0005);
            EnumMsgs.Add("ACTIVATE", 0x0006);
            EnumMsgs.Add("SETFOCUS", 0x0007);
            EnumMsgs.Add("KILLFOCUS", 0x0008);
            EnumMsgs.Add("ENABLE", 0x000A);
            EnumMsgs.Add("SETREDRAW", 0x000B);
            EnumMsgs.Add("SETTEXT", 0x000C);
            EnumMsgs.Add("GETTEXT", 0x000D);
            EnumMsgs.Add("GETTEXTLENGTH", 0x000E);
            EnumMsgs.Add("PAINT", 0x000F);
            EnumMsgs.Add("CLOSE", 0x0010);
            EnumMsgs.Add("QUERYENDSESSION", 0x0011);
            EnumMsgs.Add("QUIT", 0x0012);
            EnumMsgs.Add("QUERYOPEN", 0x0013);
            EnumMsgs.Add("ERASEBKGND", 0x0014);
            EnumMsgs.Add("SYSCOLORCHANGE", 0x0015);
            EnumMsgs.Add("ENDSESSION", 0x0016);
            EnumMsgs.Add("SHOWWINDOW", 0x0018);
            EnumMsgs.Add("WININICHANGE", 0x001A);
            EnumMsgs.Add("SETTINGCHANGE", 0x001A);
            EnumMsgs.Add("DEVMODECHANGE", 0x001B);
            EnumMsgs.Add("ACTIVATEAPP", 0x001C);
            EnumMsgs.Add("FONTCHANGE", 0x001D);
            EnumMsgs.Add("TIMECHANGE", 0x001E);
            EnumMsgs.Add("CANCELMODE", 0x001F);
            EnumMsgs.Add("SETCURSOR", 0x0020);
            EnumMsgs.Add("MOUSEACTIVATE", 0x0021);
            EnumMsgs.Add("CHILDACTIVATE", 0x0022);
            EnumMsgs.Add("QUEUESYNC", 0x0023);
            EnumMsgs.Add("GETMINMAXINFO", 0x0024);
            EnumMsgs.Add("PAINTICON", 0x0026);
            EnumMsgs.Add("ICONERASEBKGND", 0x0027);
            EnumMsgs.Add("NEXTDLGCTL", 0x0028);
            EnumMsgs.Add("SPOOLERSTATUS", 0x002A);
            EnumMsgs.Add("DRAWITEM", 0x002B);
            EnumMsgs.Add("MEASUREITEM", 0x002C);
            EnumMsgs.Add("DELETEITEM", 0x002D);
            EnumMsgs.Add("VKEYTOITEM", 0x002E);
            EnumMsgs.Add("CHARTOITEM", 0x002F);
            EnumMsgs.Add("SETFONT", 0x0030);
            EnumMsgs.Add("GETFONT", 0x0031);
            EnumMsgs.Add("SETHOTKEY", 0x0032);
            EnumMsgs.Add("GETHOTKEY", 0x0033);
            EnumMsgs.Add("QUERYDRAGICON", 0x0037);
            EnumMsgs.Add("COMPAREITEM", 0x0039);
            EnumMsgs.Add("GETOBJECT", 0x003D);
            EnumMsgs.Add("COMPACTING", 0x0041);
            EnumMsgs.Add("COMMNOTIFY", 0x0044);
            EnumMsgs.Add("WINDOWPOSCHANGING", 0x0046);
            EnumMsgs.Add("WINDOWPOSCHANGED", 0x0047);
            EnumMsgs.Add("POWER", 0x0048);
            EnumMsgs.Add("COPYDATA", 0x004A);
            EnumMsgs.Add("CANCELJOURNAL", 0x004B);
            EnumMsgs.Add("NOTIFY", 0x004E);
            EnumMsgs.Add("INPUTLANGCHANGEREQUEST", 0x0050);
            EnumMsgs.Add("INPUTLANGCHANGE", 0x0051);
            EnumMsgs.Add("TCARD", 0x0052);
            EnumMsgs.Add("HELP", 0x0053);
            EnumMsgs.Add("USERCHANGED", 0x0054);
            EnumMsgs.Add("NOTIFYFORMAT", 0x0055);
            EnumMsgs.Add("CONTEXTMENU", 0x007B);
            EnumMsgs.Add("STYLECHANGING", 0x007C);
            EnumMsgs.Add("STYLECHANGED", 0x007D);
            EnumMsgs.Add("DISPLAYCHANGE", 0x007E);
            EnumMsgs.Add("GETICON", 0x007F);
            EnumMsgs.Add("SETICON", 0x0080);
            EnumMsgs.Add("NCCREATE", 0x0081);
            EnumMsgs.Add("NCDESTROY", 0x0082);
            EnumMsgs.Add("NCCALCSIZE", 0x0083);
            EnumMsgs.Add("NCHITTEST", 0x0084);
            EnumMsgs.Add("NCPAINT", 0x0085);
            EnumMsgs.Add("NCACTIVATE", 0x0086);
            EnumMsgs.Add("GETDLGCODE", 0x0087);
            EnumMsgs.Add("SYNCPAINT", 0x0088);
            EnumMsgs.Add("NCMOUSEMOVE", 0x00A0);
            EnumMsgs.Add("NCLBUTTONDOWN", 0x00A1);
            EnumMsgs.Add("NCLBUTTONUP", 0x00A2);
            EnumMsgs.Add("NCLBUTTONDBLCLK", 0x00A3);
            EnumMsgs.Add("NCRBUTTONDOWN", 0x00A4);
            EnumMsgs.Add("NCRBUTTONUP", 0x00A5);
            EnumMsgs.Add("NCRBUTTONDBLCLK", 0x00A6);
            EnumMsgs.Add("NCMBUTTONDOWN", 0x00A7);
            EnumMsgs.Add("NCMBUTTONUP", 0x00A8);
            EnumMsgs.Add("NCMBUTTONDBLCLK", 0x00A9);
            EnumMsgs.Add("KEYDOWN", 0x0100);
            EnumMsgs.Add("KEYUP", 0x0101);
            EnumMsgs.Add("CHAR", 0x0102);
            EnumMsgs.Add("DEADCHAR", 0x0103);
            EnumMsgs.Add("SYSKEYDOWN", 0x0104);
            EnumMsgs.Add("SYSKEYUP", 0x0105);
            EnumMsgs.Add("SYSCHAR", 0x0106);
            EnumMsgs.Add("SYSDEADCHAR", 0x0107);
            EnumMsgs.Add("KEYLAST", 0x0108);
            EnumMsgs.Add("IME_StartCOMPOSITION", 0x010D);
            EnumMsgs.Add("IME_EndCOMPOSITION", 0x010E);
            EnumMsgs.Add("IME_COMPOSITION", 0x010F);
            EnumMsgs.Add("IME_KEYLAST", 0x010F);
            EnumMsgs.Add("INITDIALOG", 0x0110);
            EnumMsgs.Add("COMMAND", 0x0111);
            EnumMsgs.Add("SYSCOMMAND", 0x0112);
            EnumMsgs.Add("TIMER", 0x0113);
            EnumMsgs.Add("HSCROLL", 0x0114);
            EnumMsgs.Add("VSCROLL", 0x0115);
            EnumMsgs.Add("INITMENU", 0x0116);
            EnumMsgs.Add("INITMENUPOPUP", 0x0117);
            EnumMsgs.Add("MENUSELECT", 0x011F);
            EnumMsgs.Add("MENUCHAR", 0x0120);
            EnumMsgs.Add("ENTERIDLE", 0x0121);
            EnumMsgs.Add("MENURBUTTONUP", 0x0122);
            EnumMsgs.Add("MENUDRAG", 0x0123);
            EnumMsgs.Add("MENUGETOBJECT", 0x0124);
            EnumMsgs.Add("UNINITMENUPOPUP", 0x0125);
            EnumMsgs.Add("MENUCOMMAND", 0x0126);
            EnumMsgs.Add("CTLCOLORMSGBOX", 0x0132);
            EnumMsgs.Add("CTLCOLOREDIT", 0x0133);
            EnumMsgs.Add("CTLCOLORLISTBOX", 0x0134);
            EnumMsgs.Add("CTLCOLORBTN", 0x0135);
            EnumMsgs.Add("CTLCOLORDLG", 0x0136);
            EnumMsgs.Add("CTLCOLORSCROLLBAR", 0x0137);
            EnumMsgs.Add("CTLCOLORSTATIC", 0x0138);
            EnumMsgs.Add("MOUSEMOVE", 0x0200);
            EnumMsgs.Add("LBUTTONDOWN", 0x0201);
            EnumMsgs.Add("LBUTTONUP", 0x0202);
            EnumMsgs.Add("LBUTTONDBLCLK", 0x0203);
            EnumMsgs.Add("RBUTTONDOWN", 0x0204);
            EnumMsgs.Add("RBUTTONUP", 0x0205);
            EnumMsgs.Add("RBUTTONDBLCLK", 0x0206);
            EnumMsgs.Add("MBUTTONDOWN", 0x0207);
            EnumMsgs.Add("MBUTTONUP", 0x0208);
            EnumMsgs.Add("MBUTTONDBLCLK", 0x0209);
            EnumMsgs.Add("MOUSEWHEEL", 0x020A);
            EnumMsgs.Add("PARENTNOTIFY", 0x0210);
            EnumMsgs.Add("ENTERMENULOOP", 0x0211);
            EnumMsgs.Add("EXITMENULOOP", 0x0212);
            EnumMsgs.Add("NEXTMENU", 0x0213);
            EnumMsgs.Add("SIZING", 0x0214);
            EnumMsgs.Add("CAPTURECHANGED", 0x0215);
            EnumMsgs.Add("MOVING", 0x0216);
            EnumMsgs.Add("DEVICECHANGE", 0x0219);
            EnumMsgs.Add("MDICREATE", 0x0220);
            EnumMsgs.Add("MDIDESTROY", 0x0221);
            EnumMsgs.Add("MDIACTIVATE", 0x0222);
            EnumMsgs.Add("MDIRESTORE", 0x0223);
            EnumMsgs.Add("MDINEXT", 0x0224);
            EnumMsgs.Add("MDIMAXIMIZE", 0x0225);
            EnumMsgs.Add("MDITILE", 0x0226);
            EnumMsgs.Add("MDICASCADE", 0x0227);
            EnumMsgs.Add("MDIICONARRANGE", 0x0228);
            EnumMsgs.Add("MDIGETACTIVE", 0x0229);
            EnumMsgs.Add("MDISETMENU", 0x0230);
            EnumMsgs.Add("ENTERSIZEMOVE", 0x0231);
            EnumMsgs.Add("EXITSIZEMOVE", 0x0232);
            EnumMsgs.Add("DROPFILES", 0x0233);
            EnumMsgs.Add("MDIREFRESHMENU", 0x0234);
            EnumMsgs.Add("IME_SETCONTEXT", 0x0281);
            EnumMsgs.Add("IME_NOTIFY", 0x0282);
            EnumMsgs.Add("IME_CONTROL", 0x0283);
            EnumMsgs.Add("IME_COMPOSITIONFULL", 0x0284);
            EnumMsgs.Add("IME_SELECT", 0x0285);
            EnumMsgs.Add("IME_CHAR", 0x0286);
            EnumMsgs.Add("IME_REQUEST", 0x0288);
            EnumMsgs.Add("IME_KEYDOWN", 0x0290);
            EnumMsgs.Add("IME_KEYUP", 0x0291);
            EnumMsgs.Add("MOUSEHOVER", 0x02A1);
            EnumMsgs.Add("MOUSELEAVE", 0x02A3);
            EnumMsgs.Add("CUT", 0x0300);
            EnumMsgs.Add("COPY", 0x0301);
            EnumMsgs.Add("PASTE", 0x0302);
            EnumMsgs.Add("CLEAR", 0x0303);
            EnumMsgs.Add("UNDO", 0x0304);
            EnumMsgs.Add("RENDERFORMAT", 0x0305);
            EnumMsgs.Add("RENDERALLFORMATS", 0x0306);
            EnumMsgs.Add("DESTROYCLIPBOARD", 0x0307);
            EnumMsgs.Add("DRAWCLIPBOARD", 0x0308);
            EnumMsgs.Add("PAINTCLIPBOARD", 0x0309);
            EnumMsgs.Add("VSCROLLCLIPBOARD", 0x030A);
            EnumMsgs.Add("SIZECLIPBOARD", 0x030B);
            EnumMsgs.Add("ASKCBFORMATNAME", 0x030C);
            EnumMsgs.Add("CHANGECBCHAIN", 0x030D);
            EnumMsgs.Add("HSCROLLCLIPBOARD", 0x030E);
            EnumMsgs.Add("QUERYNEWPALETTE", 0x030F);
            EnumMsgs.Add("PALETTEISCHANGING", 0x0310);
            EnumMsgs.Add("PALETTECHANGED", 0x0311);
            EnumMsgs.Add("HOTKEY", 0x0312);
            EnumMsgs.Add("PRINT", 0x0317);
            EnumMsgs.Add("PRINTCLIENT", 0x0318);
            EnumMsgs.Add("HANDHELDFIRST", 0x0358);
            EnumMsgs.Add("HANDHELDLAST", 0x035F);
            EnumMsgs.Add("AFXFIRST", 0x0360);
            EnumMsgs.Add("AFXLAST", 0x037F);
            EnumMsgs.Add("PENWINFIRST", 0x0380);
            EnumMsgs.Add("PENWINLAST", 0x038F);
            EnumMsgs.Add("APP", 0x8000);
            EnumMsgs.Add("USER", 0x0400);
            EnumMsgs.Add("MACRODM", 0x9950);
            EnumMsgs.Add("DAVEHORSMAN", 0x9951);
            #endregion
        }
        // } // WndProcMsgsFillDict
        #endregion

        #region Fields
        // Icons
        // Notification Area
        private System.Windows.Forms.NotifyIcon NotifyAreaIcon;
        private System.Windows.Forms.RichTextBox NotifyAreaIconText;
        protected System.Windows.Forms.ContextMenu NotifyAreaContextMenu;
        // Context Menus
        protected System.Drawing.Point ContextMenuPoint;

        #endregion

        #region Form Fields
        // private System.ComponentModel.IContainer components;
        // Menus
        private System.Windows.Forms.MainMenu Mclipboard1MenuMain;
        private System.Windows.Forms.MenuItem Mclipboard1MenuFormats;
        private System.Windows.Forms.RichTextBox Mclipboard1ControlClipboardText;
        private System.Windows.Forms.MenuItem Mclipboard1MenuSupported;
        //
        private System.Windows.Forms.MenuItem Mclipboard1ItemExit;
        private System.Windows.Forms.MenuItem Mclipboard1ItemHide;
        private System.Windows.Forms.MenuItem Mclipboard1ItemSep2;
        private System.Windows.Forms.MenuItem Mclipboard1ItemSep1;
        private System.Windows.Forms.MenuItem Mclipboard1ItemHyperlink;
        private System.Windows.Forms.MenuItem Mclipboard1ItemSystray;

        //TODO RAD.Windows.NotificationAreaIcon NotifyAreaIcon
        //private RAD.Windows.NotificationAreaIcon NotifyAreaIcon;
        //private System.Windows.Forms.MessageBox NotifyAreaMessageBox;
        //private Mdm.Oss.ClipboardUtil.NotificationAreaIcon NotifyAreaIcon;
        //private System.Windows.NotificationAreaIcon;


        // Boxes
        // private System.Windows.Forms.MessageBox;
        // private System.Windows.Forms.TextBox;
        // private System.Windows.Forms.ToolBar;
        // private System.Windows.Forms.ToolStrip;
        // private System.Windows.Forms.ToolTip;
        // private System.Windows.Forms.Application;
        // private System.Windows.Forms.BaseCollection;
        // private System.Windows.Forms.Clipboard;
        // private System.Windows.Forms.CommonDialog;
        // private System.Windows.Forms.MessageBox;
        #endregion

        #region Clipboard Fields

        IntPtr _ClipboardViewerNext;

        System.Collections.Queue _hyperlink = new System.Collections.Queue();

        #endregion

        #region Constructor
        public Mclipboard1Form1() {
            InitializeComponent();

            Dictionary<string, Int32> EnumMsgs = new Dictionary<string, Int32>();

            WndProcMsgsFillDict(EnumMsgs);


            // Mdm Code
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Mclipboard1Form1));
            this.Mclipboard1MenuMain = new System.Windows.Forms.MainMenu();
            this.Mclipboard1MenuFormats = new System.Windows.Forms.MenuItem();
            this.Mclipboard1MenuSupported = new System.Windows.Forms.MenuItem();

            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));


            this.NotifyAreaContextMenu = new System.Windows.Forms.ContextMenu();
            this.Mclipboard1ItemSystray = new System.Windows.Forms.MenuItem();
            this.Mclipboard1ItemHyperlink = new System.Windows.Forms.MenuItem();
            this.Mclipboard1ItemSep1 = new System.Windows.Forms.MenuItem();
            this.Mclipboard1ItemHide = new System.Windows.Forms.MenuItem();
            this.Mclipboard1ItemSep2 = new System.Windows.Forms.MenuItem();
            this.Mclipboard1ItemExit = new System.Windows.Forms.MenuItem();
            this.Mclipboard1ControlClipboardText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Mclipboard1MenuMain
            // 
            this.Mclipboard1MenuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			 this.Mclipboard1MenuFormats,
			 this.Mclipboard1MenuSupported
                }
            );
            // 
            // Mclipboard1MenuFormats
            // 
            this.Mclipboard1MenuFormats.Index = 0;
            this.Mclipboard1MenuFormats.Text = "Formats";
            // 
            // Mclipboard1MenuSupported
            // 
            this.Mclipboard1MenuSupported.Index = 1;
            this.Mclipboard1MenuSupported.Text = "Supported";
            // 
            // NotifyAreaIcon
            //
            this.NotifyAreaIcon = new NotifyIcon(this.components);
            this.NotifyAreaIconText = new System.Windows.Forms.RichTextBox();
            //
            //this.NotifyAreaIcon.DisplayMenuOnLeftClick = true;
            this.NotifyAreaIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.NotifyAreaIcon.Text = "Clip Monitor Loading...";
            this.NotifyAreaIcon.BalloonTipText = "Clip Monitor";
            this.NotifyAreaIcon.Visible = true;
            // 
            // Notify Events
            //
            this.NotifyAreaIcon.BalloonTipClicked += new System.EventHandler(this.NotifyAreaIcon_BalloonClick);
            this.NotifyAreaIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyAreaIcon_IconClick);
            // 
            // NotifyAreaContextMenu
            // 
            this.NotifyAreaContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
				 this.Mclipboard1ItemSystray,
				 this.Mclipboard1ItemHyperlink,
				 this.Mclipboard1ItemSep1,
				 this.Mclipboard1ItemHide,
				 this.Mclipboard1ItemSep2,
				 this.Mclipboard1ItemExit});
            // this.NotifyAreaContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            // 
            // Mclipboard1ItemSystray
            // 
            this.Mclipboard1ItemSystray.Index = 0;
            this.Mclipboard1ItemSystray.Text = "C:\\Temp\\SysTray";
            this.Mclipboard1ItemSystray.Click += new System.EventHandler(this.Mclipboard1ItemHyperlink_Click);
            // 
            // 
            this.Mclipboard1ItemHyperlink.DefaultItem = true;
            this.Mclipboard1ItemHyperlink.Index = 1;
            this.Mclipboard1ItemHyperlink.Text = "http://localhost/footprint/";
            this.Mclipboard1ItemHyperlink.Click += new System.EventHandler(this.Mclipboard1ItemHyperlink_Click);
            // 
            // Mclipboard1ItemSep1
            // 
            this.Mclipboard1ItemSep1.Index = 2;
            this.Mclipboard1ItemSep1.Text = "-";
            // 
            // Mclipboard1ItemHide
            // 
            this.Mclipboard1ItemHide.Index = 3;
            this.Mclipboard1ItemHide.Text = "Hide";
            this.Mclipboard1ItemHide.Click += new System.EventHandler(this.Mclipboard1ItemHide_Click);
            // 
            // Mclipboard1ItemSep2
            // 
            this.Mclipboard1ItemSep2.Index = 4;
            this.Mclipboard1ItemSep2.Text = "-";
            // 
            // Mclipboard1ItemExit
            // 
            this.Mclipboard1ItemExit.Index = 5;
            this.Mclipboard1ItemExit.MergeOrder = 1000;
            this.Mclipboard1ItemExit.Text = "E&xit";
            //
            //
            this.NotifyAreaIcon.ContextMenu = this.NotifyAreaContextMenu;
            // 
            // Mclipboard1ControlClipboardText
            // 
            this.Mclipboard1ControlClipboardText.DetectUrls = false;
            this.Mclipboard1ControlClipboardText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Mclipboard1ControlClipboardText.Name = "Mclipboard1ControlClipboardText";
            this.Mclipboard1ControlClipboardText.ReadOnly = true;
            this.Mclipboard1ControlClipboardText.Size = new System.Drawing.Size(348, 273);
            this.Mclipboard1ControlClipboardText.TabIndex = 0;
            this.Mclipboard1ControlClipboardText.Text = "";
            this.Mclipboard1ControlClipboardText.WordWrap = false;
            // 
            // Mclipboard1FormMain1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(348, 273);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.Mclipboard1ControlClipboardText}
                );
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            // this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.App")));
            // this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            // this.Icon = ((System.Drawing.Icon)(resources.GetObject("App.Ico")));
            this.Location = new System.Drawing.Point(100, 100);
            this.Menu = this.Mclipboard1MenuMain;
            this.Name = "Mclipboard1FormMain1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Clipboard Manager";
            this.Resize += new System.EventHandler(this.Mclipboard1FormMain_Resize);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Mclipboard1FormMain_Closing);
            this.Load += new System.EventHandler(this.Mclipboard1FormMain_Load);
            this.ResumeLayout(false);

            GetClipboardData();
        }
        #endregion

        #region Main - Idle
        public void Mclipboard1FormMain1() {
            InitializeComponent();
            //TODO NotifyAreaIcon.Visible = true;
        }
        #endregion
        //     public class Mclipboard1FormMain1 : System.Windows.Forms.Form

        #region Properties - Public

        #endregion

        #region Methods - Private
        #region Methods - Activation & restore
        private void MinimizeClipboardViewer() {
            this.WindowState = FormWindowState.Minimized;
        }

        private void MaximizeClipboardViewer() {
            this.WindowState = FormWindowState.Maximized;
        }

        private void NormalizeClipboardViewer() {
            this.WindowState = FormWindowState.Normal;
        }
        #endregion

        #region Clipboard Registration
        /// Register this form as a Clipboard Viewer application
        private void RegisterClipboardViewer() {
            // _ClipboardViewerNext = SetClipboardViewer(this.Handle);
            _ClipboardViewerNext =  User32.SetClipboardViewer(this.Handle);
        }
        /// Remove this form from the Clipboard Viewer list
        private void UnregisterClipboardViewer() {
            //ChangeClipboardChain(this.Handle, _ClipboardViewerNext);
            User32.ChangeClipboardChain(this.Handle, _ClipboardViewerNext);
        }
        #endregion
        #region Format Menu Build
        /// Build a menu listing the formats supported by the contents of the clipboard
        /// <param name="iData">The current clipboard data object</param>
        private void FormatMenuBuild(IDataObject iData) {
            String[] saTrFormatsNative = iData.GetFormats(false);
            String[] saTrFormatsAll = iData.GetFormats(true);

            System.Collections.Hashtable ohFormatList = new System.Collections.Hashtable(10);

            Mclipboard1MenuFormats.MenuItems.Clear();

            for (int i = 0; i <= saTrFormatsAll.GetUpperBound(0); i++) {
                ohFormatList.Add(saTrFormatsAll[i], "Converted");
            }

            for (int i = 0; i <= saTrFormatsNative.GetUpperBound(0); i++) {
                if (ohFormatList.Contains(saTrFormatsNative[i])) {
                    ohFormatList[saTrFormatsNative[i]] = "Native/Converted";
                } else {
                    ohFormatList.Add(saTrFormatsNative[i], "Native");
                }
            }

            foreach (System.Collections.DictionaryEntry item in ohFormatList) {
                MenuItem Mclipboard1ItemNew = new MenuItem(item.Key.ToString() + "\t" + item.Value.ToString());
                Mclipboard1MenuFormats.MenuItems.Add(Mclipboard1ItemNew);
            }
        }
        #endregion
        #region Supported Menu Build
        /// list the formats that are supported from the default clipboard formats.
        /// <param name="iData">The current clipboard contents</param>
        private void SupportedMenuBuild(IDataObject iData) {
            Mclipboard1MenuSupported.MenuItems.Clear();

            for (int i = 0; i <= formatsAll.GetUpperBound(0); i++) {
                MenuItem Mclipboard1ItemFormat = new MenuItem(formatsAllDesc[i]);
                //
                // Get supported formats
                //
                if (iData.GetDataPresent(formatsAll[i])) {
                    Mclipboard1ItemFormat.Checked = true;
                }
                Mclipboard1MenuSupported.MenuItems.Add(Mclipboard1ItemFormat);

            }
        }
        #endregion
        #region Links Extraction
        /// Search the clipboard contents for hyperlinks and unc paths, etc
        /// <param name="iData">The current clipboard contents</param>
        /// <returns>true if new links were found, false otherwise</returns>
        private bool ClipboardSearch(IDataObject iData) {
            bool FoundNewLinks = false;
            //
            // If it is not text then quit
            // cannot search bitmap etc
            //
            if (!iData.GetDataPresent(DataFormats.Text)) {
                return false;
            }

            String strClipboardText;

            try {
                strClipboardText = (String)iData.GetData(DataFormats.Text);
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                return false;
            }

            // Hyperlinks e.g. http://www.server.com/folder/file.aspx
            System.Text.RegularExpressions.Regex rxURL = new System.Text.RegularExpressions.Regex(@"(\b(?:http|https|ftp|file)://[^\s]+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            rxURL.Match(strClipboardText);

            foreach (System.Text.RegularExpressions.Match rm in rxURL.Matches(strClipboardText)) {
                if (!_hyperlink.Contains(rm.ToString())) {
                    _hyperlink.Enqueue(rm.ToString());
                    FoundNewLinks = true;
                }
            }

            // Files and folders - \\servername\foldername\
            // TODO Reg Ex rxFile - Files and folders - needs work
            System.Text.RegularExpressions.Regex rxFile = new System.Text.RegularExpressions.Regex(@"(\b\w:\\[^ ]*)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            rxFile.Match(strClipboardText);

            // foreach (Match rm in rxFile.Matches(strClipboardText))
            foreach (System.Text.RegularExpressions.Match rm in rxFile.Matches(strClipboardText)) {
                if (!_hyperlink.Contains(rm.ToString())) {
                    _hyperlink.Enqueue(rm.ToString());
                    FoundNewLinks = true;
                }
            }

            // UNC Files 
            // TODO Reg Ex rxUnc - UNC Files - needs work
            System.Text.RegularExpressions.Regex rxUNC = new System.Text.RegularExpressions.Regex(@"(\\\\[^\s/:\*\?\" + "\"" + @"\<\>\|]+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            rxUNC.Match(strClipboardText);

            foreach (System.Text.RegularExpressions.Match rm in rxUNC.Matches(strClipboardText)) {
                if (!_hyperlink.Contains(rm.ToString())) {
                    _hyperlink.Enqueue(rm.ToString());
                    FoundNewLinks = true;
                }
            }

            // UNC folders
            // TODO Reg Ex rxUNCFolder - UNC folders - needs work
            System.Text.RegularExpressions.Regex rxUNCFolder = new System.Text.RegularExpressions.Regex(@"(\\\\[^\s/:\*\?\" + "\"" + @"\<\>\|]+\\)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            rxUNCFolder.Match(strClipboardText);

            foreach (System.Text.RegularExpressions.Match rm in rxUNCFolder.Matches(strClipboardText)) {
                if (!_hyperlink.Contains(rm.ToString())) {
                    _hyperlink.Enqueue(rm.ToString());
                    FoundNewLinks = true;
                }
            }

            // Email Addresses
            System.Text.RegularExpressions.Regex rxEmailAddress = new System.Text.RegularExpressions.Regex(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            rxEmailAddress.Match(strClipboardText);

            foreach (System.Text.RegularExpressions.Match rm in rxEmailAddress.Matches(strClipboardText)) {
                if (!_hyperlink.Contains(rm.ToString())) {
                    _hyperlink.Enqueue("mailto:" + rm.ToString());
                    FoundNewLinks = true;
                }
            }

            return FoundNewLinks;
        }
        #endregion
        #region System Tray Icon and Menu
        /// Build the system tray menu from the hyperlink list
        private void ContextMenuBuild() {
            //
            // Only show the last 10 items
            //
            while (_hyperlink.Count > 10) {
                _hyperlink.Dequeue();
            }

            NotifyAreaContextMenu.MenuItems.Clear();

            foreach (String objLink in _hyperlink) {
                NotifyAreaContextMenu.MenuItems.Add(objLink.ToString(), new EventHandler(Mclipboard1ItemHyperlink_Click));
            }
            NotifyAreaContextMenu.MenuItems.Add("-");
            NotifyAreaContextMenu.MenuItems.Add("Cancel Menu", new EventHandler(Mclipboard1ItemCancelMenu_Click));
            NotifyAreaContextMenu.MenuItems.Add("-");
            NotifyAreaContextMenu.MenuItems.Add(Mclipboard1ItemHide.Text, new EventHandler(Mclipboard1ItemHide_Click));
            NotifyAreaContextMenu.MenuItems.Add("-");
            NotifyAreaContextMenu.MenuItems.Add("E&xit", new EventHandler(Mclipboard1ItemExit_Click));
        }
        #endregion
        #region Menu Selection
        /// Called when an item is chosen from the menu
        /// <param name="pstrLink">The link that was clicked</param>
        private void OpenLink(String pstrLink) {
            try {
                //
                // Run the link
                //

                // TODO needs more work to check for missing files etc
                System.Diagnostics.Process.Start(pstrLink);
            } catch (Exception ex) {
                MessageBox.Show(this, ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion
        #region Clipboard Display
        /// Show the clipboard contents in the window 
        /// and show the notification balloon if a link is found
        private void GetClipboardData() {
            //
            // Data on the clipboard uses the 
            // IDataObject interface
            //
            IDataObject iData = new DataObject();
            String strText = "clipmon";

            try {
                iData = Clipboard.GetDataObject();
            } catch (System.Runtime.InteropServices.ExternalException externEx) {
                // Copying a field definition in Access 2002 causes this sometimes?

                System.Diagnostics.Debug.WriteLine("InteropServices.ExternalException: {0}", externEx.Message);
                return;
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                return;
            }

            // 
            // Get RTF if it is present
            //
            if (iData.GetDataPresent(DataFormats.Rtf)) {
                Mclipboard1ControlClipboardText.Rtf = (String)iData.GetData(DataFormats.Rtf);

                if (iData.GetDataPresent(DataFormats.Text)) {
                    strText = "RTF";
                }
            } else {
                // 
                // Get Text if it is present
                //
                if (iData.GetDataPresent(DataFormats.Text)) {
                    Mclipboard1ControlClipboardText.Text = (String)iData.GetData(DataFormats.Text);

                    strText = "Text";

                    System.Diagnostics.Debug.WriteLine((String)iData.GetData(DataFormats.Text));
                } else {
                    //
                    // Only show RTF or TEXT
                    //
                    Mclipboard1ControlClipboardText.Text = "(cannot display this format)";
                }
            }

            NotifyAreaIcon.BalloonTipText = strText;
            NotifyAreaIcon.Text = strText;
            NotifyAreaIcon.Visible = true;
            // NotifyAreaIcon.Show();

            if (ClipboardSearch(iData)) {
                //
                // Found some new links
                //
                System.Text.StringBuilder strBalloon = new System.Text.StringBuilder(100);

                foreach (String objLink in _hyperlink) {
                    strBalloon.Append(objLink.ToString() + "\n");
                }

                ContextMenuBuild();

                if (_hyperlink.Count > 0) {

                    // NotifyAreaIcon.BalloonDisplay(NotificationAreaIcon.NOTIFYICONdwInfoItFlags.NIIfINFO, "Links", strBalloon.ToString());

                    // NotifyAreaIcon.BalloonDisplay(
                    //    NotificationAreaIcon.NOTIFYICONdwInfoItFlags.NIIfINFO,
                    //    "Links", 
                    //    strBalloon.ToString());
                    //
                    // TODO NotifyAreaIcon.Text = strBalloon.ToString();
                    // tmp = System.Windows.Forms.MessageBoxIcon.Exclamation;

                }
            }
            FormatMenuBuild(iData);
            SupportedMenuBuild(iData);
        }
        #endregion
        #endregion

        #region Methods - Public
        #region Main - Run
        /// The main entry point for the application.
        [STAThread]
        static void Main() {
            Application.Run(new Mclipboard1Form1());
        }
        #endregion
        #region Windows Message Processing
        protected override void WndProc(ref Message m) {
            String wmThisMessage = "";
            int iThisMessage = 0;
            Int32 wParam;
            try {
                wmThisMessage = m.Msg.ToString();
                Convert.ToInt32(wmThisMessage, iThisMessage);
            } catch (Exception e) {
                iThisMessage = 0;
            }
            WndProcMessageDisplay(ref m);

            // switch ((Msgs)m.Msg)

            // private const int WM_SYSCOMMAND = 0x0112;
            // private const int SC_MINIMIZED = 0xF020;
            // private const int WM_ACTIVATE = 0x0006;
            // SC_MINIMIZE
            // SC_RESTORE

            // protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case (int)Msgs.WM_SYSCOMMAND:
                    /* A window receives this message when the user chooses a command from the Window menu (formerly known as the system or control menu) or when the user chooses the maximize button, minimize button, restore button, or close button. */
                    base.WndProc(ref m);
                    // if (m.WParam.ToInt32() == (int)Msgs.SC_MINIMIZED)
                    wParam = m.WParam.ToInt32();
                    wParam = wParam << 4;
                    if (wParam > 0) {
                        this.ShowInTaskbar = false;
                        NotifyAreaIcon.Visible = true;
                    }
                    // base.WndProc(ref m);
                    break;

                case (int)Msgs.WM_ACTIVATE:
                    base.WndProc(ref m);
                    if ((int)m.WParam != 0) { ; }


                    wParam = m.WParam.ToInt32();
                    wParam = wParam << 4;
                    if (wParam > 0) {
                        this.ShowInTaskbar = true;
                        // NotifyAreaIcon.Visible = false;
                        this.NotifyAreaIcon.Visible = true;
                    }
                    // base.WndProc(ref m);
                    break;
                /*
                // The WM_ACTIVATEAPP message occurs when the application
                // becomes the active application or becomes inactive.
                // The WParam value identifies what is occurring.
                appActive = (((int)m.WParam != 0));

                // Invalidate to get new text painted.
                this.Invalidate();
                */
                case (int)Msgs.WM_COMMAND:                // = 0x0111,
                case (int)Msgs.WM_MENUCOMMAND:            // = 0x0126,
                    // case (int)Msgs.SC_MINIMIZE: // = 0xF020;
                    // case (int)ShowWindowStyles.SW_SHOWMINIMIZED: // =  2;
                    // case (int)ShowWindowStyles.SW_MINIMIZE: // =  6;
                    // case (int)ShowWindowStyles.SW_FORCEMINIMIZE: // = 11;
                    System.Diagnostics.Debug.WriteLine("Mclipboard1 Form1 WindowProc Msgs: System Command / Window Show: " + m.Msg, "WndProc");
                    // Let the form process the messages that we are
                    // not interested in
                    //
                    base.WndProc(ref m);
                    break;
                //
                // The WM_DRAWCLIPBOARD message is sent to the first window 
                // in the clipboard viewer chain when the content of the 
                // clipboard changes. This enables a clipboard viewer 
                // window to display the new content of the clipboard. 
                //
                case (int)Msgs.WM_DRAWCLIPBOARD:
                    // System.Diagnostics.Debug.WriteLine("Mclipboard1 Form1 WindowProc DRAWCLIPBOARD: " + m.Msg, "WndProc");
                    GetClipboardData();
                    //
                    // Each window that receives the WM_DRAWCLIPBOARD message 
                    // must call the SendMessage function to pass the message 
                    // on to the next window in the clipboard viewer chain.
                    //
                    //SendMessage(_ClipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    User32.SendMessage(_ClipboardViewerNext, m.Msg, m.WParam, m.LParam);

                    // this.WindowState = FormWindowState.Normal;
                    // this.ShowWithoutActivation;
                    // this.ShowInTaskbar;
                    // this.ShowDialog();
                    // this.Show();
                    // this.SendToBack();

                    // this.RestoreBounds;
                    // this.Region;
                    // this.PreferredSize();
                    // this.Paint event;

                    /*
                    this.OnVisibleChanged() raise event;
                    this.OnShown();
                    this.OnResizeEnd();
                    this.OnResize();
                    this.OnResizeBegin();
                    this.OnParentVisibleChanged();
                    this.OnPaint();
                    this.OnMove();
                    this.OnMdiChildActivate();
                    this.OnMaximumSizeChanged();
                    this.OnMaximizedBoundsChanged();
                    this.OnLostFocus();
                    this.OnInvalidated();
                    this.OnGotFocus();
                    this.OnFormClosing();
                    this.OnFormClosed();
                    this.OnDeactivate();
                    this.OnActivated();

                    this.NotifyAreaIcon_BalloonClick() EventArgs;
                    this.NotifyAreaIcon Text;
                    this.
                        */

                    // this.Resize();
                    // this.RaisePaintEvent() raise;
                    // this.Paint() RaisePaintEvent;
                    // this.

                    // this.TopLevelControl.Show();
                    // this.Parent;
                    // this.Owner();
                    // this.OwnedForms() Form array;

                    // this.Refresh();

                    if (!this.Visible) {
                        this.Show();
                    }
                    if (!this.TopLevel) {
                        this.Activate();
                    }
                    // if (!this.Focused) {
                    // this.Focus();
                    // }

                    // if (this.WindowState.ToString() != "Normal") {
                    //  this.Activate();
                    // }


                    // this.Activate();
                    break;
                //
                // The WM_CHANGECBCHAIN message is sent to the first window 
                // in the clipboard viewer chain when a window is being 
                // removed from the chain. 
                //
                case (int)Msgs.WM_CHANGECBCHAIN:
                    // System.Diagnostics.Debug.WriteLine("WM_CHANGECBCHAIN: lParam: " + m.LParam, "WndProc");

                    // When a clipboard viewer window receives the WM_CHANGECBCHAIN message, 
                    // it should call the SendMessage function to pass the message to the 
                    // next window in the chain, unless the next window is the window 
                    // being removed. In this case, the clipboard viewer should save 
                    // the handle specified by the lParam parameter as the next window in the chain. 

                    //
                    // wParam is the Handle to the window being removed from 
                    // the clipboard viewer chain 
                    // lParam is the Handle to the next window in the chain 
                    // following the window being removed. 
                    if (m.WParam == _ClipboardViewerNext) {
                        //
                        // If wParam is the next clipboard viewer then it
                        // is being removed so update pointer to the next
                        // window in the clipboard chain
                        //
                        _ClipboardViewerNext = m.LParam;
                    } else {
                        User32.SendMessage(_ClipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    }
                    break;

                // case (int)Msgs.WM_ACTIVATE: //               = 0x0006,
                case (int)Msgs.WM_ACTIVATEAPP: //            = 0x001C,
                case (int)Msgs.WM_NCACTIVATE: //             = 0x0086,
                    // System.Diagnostics.Debug.WriteLine("Mclipboard1 Form1 WindowProc (NC)Activate(APP): " + m.Msg, "WndProc");
                    // Let the form process the messages that we are
                    // not interested in
                    //
                    base.WndProc(ref m);
                    break;

                case (int)Msgs.WM_SETFOCUS: //               = 0x0007,
                case (int)Msgs.WM_KILLFOCUS: //              = 0x0008,
                case (int)Msgs.WM_CLOSE: //                  = 0x0010,
                case (int)Msgs.WM_SHOWWINDOW: //             = 0x0018,
                case (int)Msgs.WM_MDICREATE: //              = 0x0220,
                case (int)Msgs.WM_MDIDESTROY: //             = 0x0221,
                case (int)Msgs.WM_MDIACTIVATE: //            = 0x0222,
                case (int)Msgs.WM_MDIRESTORE: //             = 0x0223,
                case (int)Msgs.WM_MDINEXT: //                = 0x0224,
                case (int)Msgs.WM_MDIMAXIMIZE: //            = 0x0225,
                case (int)Msgs.WM_MDITILE: //                = 0x0226,
                case (int)Msgs.WM_MDICASCADE: //             = 0x0227,
                case (int)Msgs.WM_MDIICONARRANGE: //         = 0x0228,
                case (int)Msgs.WM_MDIGETACTIVE: //           = 0x0229,
                case (int)Msgs.WM_MDISETMENU: //             = 0x0230,
                case (int)Msgs.WM_MDIREFRESHMENU: //         = 0x0234,
                case (int)Msgs.WM_CUT: //                    = 0x0300,
                case (int)Msgs.WM_COPY: //                   = 0x0301,
                case (int)Msgs.WM_PASTE: //                  = 0x0302,
                case (int)Msgs.WM_CLEAR: //                  = 0x0303,
                case (int)Msgs.WM_UNDO: //                   = 0x0304,
                    // System.Diagnostics.Debug.WriteLine("Mclipboard1 Form1 WindowProc (MDI) Focus, Close, XCVZ: " + m.Msg, "WndProc");
                    // Let the form process the messages that we are
                    // not interested in
                    //
                    base.WndProc(ref m);
                    break;

                default:
                    // System.Diagnostics.Debug.WriteLine("Mclipboard1 Form1 WindowProc to base: " + m.Msg, "WndProc");
                    //
                    // Let the form process the messages that we are
                    // not interested in
                    //
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion
        #endregion

        public void WndProcMessageDisplay(ref Message m) {
            String wmThisMessage = m.Msg.ToString();
            String sTemp1;
            String wmThisMessageDesc;
            String sTempMessage;
            sTempMessage = "WindowProc: ";
            sTempMessage += "Int:" + wmThisMessage + "  Constant:";
            sTemp1 = ClipboardField(m.ToString(), "(", 2);
            wmThisMessageDesc = ClipboardField(sTemp1, ")", 1);
            sTempMessage += wmThisMessageDesc;
            sTempMessage += "  Hex:" + Convert.ToInt32(wmThisMessage).ToString("X");
            System.Diagnostics.Debug.WriteLine(sTempMessage, "Mclipboard1 Form1 WndProc");
        }

        public String ConvertStringToHex(String asciiString) {
            String hex = "";
            foreach (char c in asciiString) {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }
        private String ClipboardField(String sField, String sField_Char, int FieldOccurence) {
            // PickField = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            String sTemp = sField;
            int iLoc = 0;
            int iForCounter = 0;
            //For Loop
            for (iForCounter = 1; (iForCounter <= FieldOccurence && sTemp.Length > 0); iForCounter++) {
                iLoc = sTemp.IndexOf(sField_Char, 0);
                if (iForCounter <= FieldOccurence) {
                    if (iLoc == -1) {
                        if (iForCounter < FieldOccurence) {
                            sTemp = "";
                        }
                        break;
                    } else if (iForCounter == FieldOccurence) {
                        if (iLoc > 0) {
                            sTemp = sTemp.Substring(0, iLoc);
                        } else { sTemp = ""; }
                        break;
                    } else if (iForCounter < FieldOccurence) {
                        sTemp = sTemp.Substring(iLoc + 1);
                    }

                }
            } // end of for
            return sTemp;
        }


        #region Event Handlers - Menu
        private void Mclipboard1ItemExit_Click(Object sender, EventArgs e) {
            this.Close();
        }

        private void Mclipboard1ItemHide_Click(Object sender, System.EventArgs e) {
            this.Visible = (!this.Visible);
            Mclipboard1ItemHide.Text = this.Visible ? "Hide" : "Show";

            if (this.Visible == true) {
                if (this.WindowState == FormWindowState.Minimized) {
                    this.WindowState = FormWindowState.Normal;
                }
            }
        }

        private void Mclipboard1ItemHyperlink_Click(Object sender, System.EventArgs e) {
            MenuItem Mclipboard1ItemHL = (MenuItem)sender;

            OpenLink(Mclipboard1ItemHL.Text);
        }

        private void Mclipboard1ItemCancelMenu_Click(Object sender, System.EventArgs e) {
            // Do nothing - Cancel the menu
            bool MenuClickCancel = true;
        }

        private void Mclipboard1FormMain_Resize(Object sender, System.EventArgs e) {
            if ((this.WindowState == FormWindowState.Minimized) && (this.Visible == true)) {
                // hide when minimised
                this.Visible = false;
                Mclipboard1ItemHide.Text = "Show";
            }
        }
        #endregion

        #region Event Handlers - Internal
        private void Mclipboard1FormMain_Load(Object sender, System.EventArgs e) {
            RegisterClipboardViewer();
        }

        private void Mclipboard1FormMain_Closing(Object sender, System.ComponentModel.CancelEventArgs e) {
            UnregisterClipboardViewer();
        }

        private void NotifyAreaIcon_IconClick(Object sender, System.Windows.Forms.MouseEventArgs e) {
            NotifyAreaIcon_BalloonClick(sender);
        }

        private void NotifyAreaIcon_BalloonClick(Object sender, System.EventArgs e) {
            NotifyAreaIcon_BalloonClick(sender);
        }

        private void NotifyAreaIcon_BalloonClick(Object sender) {
            if (_hyperlink.Count == 1) {
                String strItem = (String)_hyperlink.ToArray()[0];

                // Only one link so open it
                OpenLink(strItem);
            } else {
                ContextMenuPoint.X = 50;
                ContextMenuPoint.Y = 50;
                //NotifyAreaIcon.ContextMenu.Show(
                //    NotifyAreaIcon,
                //    ContextMenuPoint,
                //    LeftRightAlignment.Right
                //    );
                // .ContextMenuDisplay();
                // NotifyAreaIcon.Show();
            }
        }

        private void Mclipboard1FormMain_Minimize(Object sender, System.ComponentModel.CancelEventArgs e) {
            // Minimize();
        }
        private void Mclipboard1FormMain_Restore(Object sender, System.ComponentModel.CancelEventArgs e) {
            // Restore();
        }
        private void Mclipboard1FormMain_Activate(Object sender, System.ComponentModel.CancelEventArgs e) {
            // Activate();
        }

        #endregion

        #region IDisposable Implementation
        /*
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
        */
        #endregion
    }
} // end of namespace Mdm
