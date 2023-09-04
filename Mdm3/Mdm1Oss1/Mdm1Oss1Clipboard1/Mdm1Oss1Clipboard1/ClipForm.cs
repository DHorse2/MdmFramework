#region Dependencies
#region System
#region System
using System;
using System.Linq;
#endregion
#region System Collections
using System.Collections;
using System.Collections.Generic;
//using System.Collections.Specialized;
#endregion
#region System Data & SQL
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
#endregion
#region System Text
using System.Text;
using System.Text.RegularExpressions;
#endregion
#region System Windows Forms
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
//using System.Windows.Controls;
#endregion
#region System Other
//using System.Collections.Specialized;
//using System.ComponentModel;
#endregion
#region System Globalization
using System.Globalization;
#endregion
#region System Serialization (Runtime and Xml)
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
#endregion
#region System Reflection, Runtime, Timers
using System.Diagnostics;
using System.Reflection;
using System.Runtime;
//using System.Runtime.InteropServices;
//using System.Runtime.Remoting.Messaging;
using System.Timers;
#endregion
#region System XML
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Serialization.Advanced;
using System.Xml.Serialization.Configuration;
#endregion
#endregion

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
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Printer;
using Mdm.World;
#endregion
#region Mdm WinUtil, System Shell32, WshRuntime
using Mdm.Oss.WinUtil;
using Mdm.Oss.WinUtil.Types;
//          add shell32.dll reference
//          or COM Microsoft Shell Controls and Automation
//using Shell32;
////          At first, Project > Add Reference > COM > Windows ScriptItemPassed Host Object Model.
//using IWshRuntimeLibrary;
#endregion
#region  Mdm Db and File
using Mdm.Oss.File;
using Mdm.Oss.File.Db;
using Mdm.Oss.File.Db.Data;
using Mdm.Oss.File.Db.Table;
using Mdm.Oss.File.Db.Thread;
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
// using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
using Mdm.Oss.File.Type.Srt.Script;
#endregion
#region Mdm Srt (Search, replace and transform)
using Mdm.Srt;
using Mdm.Srt.Core;
using Mdm.Srt.Transform;
using Mdm.Srt.Script;
#endregion
#region  Mdm Clipboard
using Mdm.Oss.ClipUtil;
using Mdm.Oss.Components;
#endregion
#endregion

namespace Mdm.Oss.ClipUtil
{
    /// <summary>
    /// Clipboard Monitor
    /// </summary>
    /// <remarks>
    /// Based upon Rad Softwares clipboard example
    /// </remarks>
    public class ClipFormMain : StdBaseFormDef
    {
        #region Clipboard Formats
        /// <summary>
        /// Clipboard Formats enumeration
        /// </summary> 
        string[] formatsAll = new string[]
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
        /// <summary>
        /// Format descriptions
        /// </summary> 
        string[] formatsAllDesc = new String[]
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
        #region Fields
        public IDataObject iData;
        // I dunno about 
        // The Srt but it was not hooked up.
        //public ScriptEngine sciptEngine = new ScriptEngine();
        // This appears to be the self test object.
        //public PropManager propManager = new PropManager();
        private Func<Int32> localFunc;
        #region Form Fields
        /// <summary>
        /// UI Form elements 
        /// </summary> 
        private System.ComponentModel.IContainer components;
        private MenuStrip MenuMain;
        // ToDo Add documentation and comments to the 
        // ToDo empty lines below.
        private ToolStripMenuItem MenuFormats;
        private ToolStripMenuItem MenuSupported;
        // ToDo IE. This line.
        private ToolStripMenuItem MenuLinks;
        private ToolStripMenuItem MenuFolderNFile;
        private ToolStripMenuItem MenuHyperlinks;
        private ToolStripMenuItem MenuOther;
        private ToolStripMenuItem ItemSep3;
        private ToolStripMenuItem MenuClearLinks;
        private ToolStripMenuItem MenuBuildLinks;
        private ToolStripMenuItem ItemSep4;
        //
        private ToolBar MenuButtonBar;
        private MenuStrip MenuButtonStrip;
        private ToolStripButton MenuButtonPrev;
        private ToolStripButton MenuButtonNext;
        private ToolStripButton MenuButtonSet;
        //
        //private ToolStripButton RunControlUi.ButtonStart;
        //
        // protected ContextMenu CntxtMenuTray;
        protected ContextMenuStrip CntxtMenuTray;
        //
        private ToolStripMenuItem MenuFolderNFileCntxt;
        private ToolStripMenuItem MenuHyperlinksCntxt;
        private ToolStripMenuItem MenuOtherCntxt;
        private ToolStripMenuItem MenuClearLinksCntxt;
        private ToolStripMenuItem MenuBuildLinksCntxt;
        private ToolStripMenuItem ItemExit;
        private ToolStripMenuItem ItemHide;
        private ToolStripMenuItem ItemSep2;
        private ToolStripMenuItem ItemSep1;
        private ToolStripMenuItem ItemHyperlink;
        private ToolStripMenuItem ItemSystray;
        //
        // private TextBox;
        // private ToolBar;
        // private ToolStrip;
        // private ToolTip;
        // private Application;
        // private BaseCollection;
        // private Clipboard;
        // private CommonDialog;
        // private MessageBox;
        private MessageBox notAreaMessageBox;
        //
        private RichTextBox ControlClipboardText;
        //
        private RichTextBox ControlClipboardText2;
        //
        private Panel ControlClipboardListButtonPanel = new Panel();
        private DataGridView ControlClipboardList = new DataGridView();
        private Button ControlClipboardListAddNewRowButton = new Button();
        private Button ControlClipboardListDeleteRowButton = new Button();
        //
        // private Mdm.Oss.ClipUtil.NotificationAreaIcon NotifyAreaIcon;
        // private System.Windows.NotificationAreaIcon;
        // private MessageBox;
        private NotifyIcon CntxtMenuIcon;
        private ContextMenu CntxtMenu;
        #endregion
        #region Program Fields
        bool bShowWhenMinimized = true;
        bool bShowFromMenu = false;
        IntPtr _ClipboardViewerNext;
        // IDataObject iData;
        Int32 SequenceNumberCurrent = 0;
        Int32 SequenceNumberSelected = 0;
        string cDataLast = sEmpty;
        public new StdConsoleManagerDef st;

        ProgControlDef ProgControl;

        ClipMetaDef ClipMeta;
        MemoryStream ClipMetaMemStream = new MemoryStream();
        string ClipMetaOut;

        mFileSql ClipFile;

        List<ClipMetaDef> ClipHist;
        Win32ClipDef Win32Clip = new Win32ClipDef();

        /// <summary>
        /// Dictionary list containing unique hyperlinks.
        /// </summary> 
        Dictionary<string, HyperlinkMeta> HyperlinkList = new Dictionary<string, HyperlinkMeta>();

        #region HyperlinkList Fields
        // Queue HyperlinkList = new Queue();
        /// <summary>
        /// Hyperlink meta data including link type,
        /// document type and process verb.
        /// </summary> 
        public class HyperlinkMeta
        {
            public int HyperlinkType;
            public int DocumentType;
            public string ProcessName;
            public ProcessStartInfo ProcessStartupInfo;
            /// <summary>
            /// Default constructor using text document and New verb.
            /// </summary> 
            public HyperlinkMeta()
            {
                HyperlinkType = (int)HyperlinkTypeIs.Document;
                DocumentType = (int)DocumentTypeIs.TextDocument;
                ProcessName = "New Document.txt";
                HyperlinkDataInit(ProcessName, HyperlinkType, DocumentType);
                return;
            }
            /// <summary>
            /// Create a text document link and use the passed verb.
            /// </summary> 
            public HyperlinkMeta(string PassedProcessName)
            {
                ProcessName = PassedProcessName;
                HyperlinkType = (int)HyperlinkTypeIs.Document;
                DocumentType = (int)DocumentTypeIs.TextDocument;
                HyperlinkDataInit(ProcessName, HyperlinkType, DocumentType);
                return;
            }
            /// <summary>
            /// Create the passed Hyperlink type and use the passed verb.
            /// </summary> 
            public HyperlinkMeta(string PassedProcessName, int iPassedHyperlinkType)
            {
                HyperlinkType = iPassedHyperlinkType;
                ProcessName = PassedProcessName;
                DocumentType = (int)DocumentTypeIs.TextDocument;
                HyperlinkDataInit(ProcessName, HyperlinkType, DocumentType);
                return;
            }
            /// <summary>
            /// Create the passed Hyperlink and document type and used the passed verb.
            /// </summary> 
            public HyperlinkMeta(string PassedProcessName, int iPassedHyperlinkType, int iPassedDocumentType)
            {
                HyperlinkType = iPassedHyperlinkType;
                ProcessName = PassedProcessName;
                DocumentType = iPassedDocumentType;
                HyperlinkDataInit(ProcessName, HyperlinkType, DocumentType);
            }
            /// <summary>
            /// Standard initialize called after constructors.
            /// </summary> 
            public void HyperlinkDataInit(string PassedProcessName, int iPassedHyperlinkType, int iPassedDocumentType)
            {
                HyperlinkType = iPassedHyperlinkType;
                DocumentType = iPassedDocumentType;
                ProcessName = PassedProcessName;
                if (PassedProcessName.Length == 0) { PassedProcessName = "New Document.txt"; }
                ProcessStartupInfo = new ProcessStartInfo(PassedProcessName);
                return;
            }
        }

        ///// <summary>
        ///// Dictionary list containing unique hyperlinks.
        ///// </summary> 
        //Dictionary<string, HyperlinkMeta> HyperlinkList = new Dictionary<string, HyperlinkMeta>();

        /// <summary>
        /// Hyperlink types.
        /// </summary> 
        private enum HyperlinkTypeIs : int
        {
            Hyperlink,
            Folder,
            UncFolder,
            UncFile,
            MailTo,
            Document
        };

        /// <summary>
        /// Document type hyperlink points to.
        /// </summary> 
        private enum DocumentTypeIs : int
        {
            WordDocument,
            ExcelDocument,
            OpenOfficeDocument,
            TextDocument,
            CodeDocument,
            BatDocument,
            ScriptDocument
        };
        #endregion
        #endregion
        #endregion
        #region Constructors
        Shell32.ShellLinkObject tmpPath;
        /// <summary>
        /// Clipboard UI Form main method.
        /// </summary> 
        public ClipFormMain()
        {
            InitializeComponent();
            #region Notes
            //if (!NotifyAreaIcon.Visible) {
            //    NotifyAreaIcon.Visible = true;
            //}
            //propManager = new PropManager();
            //propManager.CreateShortcutTest();
            //propManager.ShortcutRead(@"C:\SrtVs501\Code\Input\GSrtVs50CodeInputOriginalDictionary.lnk");
            //propManager.shortcutDescription = "hello";
            //propManager.shortcutTargetPath = @"C:\SrtVs501\Code\Input";
            //propManager.ShortcutWrite();
            #endregion
        }
        #endregion
        #region Properties - Public
        #endregion
        #region Methods - Private
        #endregion
        #region Clipboard Component Register, Build Formats, Context Menu
        /// <summary>
        /// Register this form as a Clipboard Viewer application
        /// </summary>
        private void RegisterClipboardViewer()
        {
            // _ClipboardViewerNext = SetClipboardViewer(Handle);
            _ClipboardViewerNext = Win32ClipDef.SetClipboardViewer(Handle);
        }
        /// <summary>
        /// Remove this form from the Clipboard Viewer list
        /// </summary>
        private void UnregisterClipboardViewer()
        {
            //ChangeClipboardChain(Handle, _ClipboardViewerNext);
            Win32ClipDef.ChangeClipboardChain(Handle, _ClipboardViewerNext);
        }
        /// <summary>
        /// Build a menu listing the formats supported by the contents of the clipboard
        /// </summary>
        /// <param name="iData">The current clipboard data object</param>
        private void MenuFormatBuild()
        {
            string[] astrFormatsNative = iData.GetFormats(false);
            string[] astrFormatsAll = iData.GetFormats(true);

            Hashtable formatList = new Hashtable(10);

            MenuFormats.DropDownItems.Clear();

            for (int i = 0; i <= astrFormatsAll.GetUpperBound(0); i++)
            {
                formatList.Add(astrFormatsAll[i], "Converted");
            }

            for (int i = 0; i <= astrFormatsNative.GetUpperBound(0); i++)
            {
                if (formatList.Contains(astrFormatsNative[i]))
                {
                    formatList[astrFormatsNative[i]] = "Native/Converted";
                }
                else
                {
                    formatList.Add(astrFormatsNative[i], "Native");
                }
            }

            foreach (DictionaryEntry item in formatList)
            {
                ToolStripMenuItem ItemNew = new ToolStripMenuItem(item.Key.ToString() + "\t" + item.Value.ToString());
                MenuFormats.DropDownItems.Add(ItemNew);
            }
        }
        /// <summary>
        /// list the formats that are supported from the default clipboard formats.
        /// </summary>
        /// <param name="iData">The current clipboard contents</param>
        private void MenuSupportedBuild()
        {
            MenuSupported.DropDownItems.Clear();

            for (int i = 0; i <= formatsAll.GetUpperBound(0); i++)
            {
                ToolStripMenuItem ItemFormat = new ToolStripMenuItem(formatsAllDesc[i]);
                //
                // Get supported formats
                //
                if (iData.GetDataPresent(formatsAll[i]))
                {
                    ItemFormat.Checked = true;
                }
                MenuSupported.DropDownItems.Add(ItemFormat);

            }
        }
        /// <summary>
        /// Build the system tray menu from the hyperlink list
        /// </summary>
        private void MenuContextBuild(bool bBuildHyperlinks)
        {
            Dictionary<string, HyperlinkMeta>.KeyCollection _linkKeyColl = HyperlinkList.Keys;

            //
            // Only show the last 10 items
            //
            int iHyperlinkCounter = 0;
            //int iHyperlinkCntxMax = 10;
            //CntxtMenuTray.DropDownItems.Clear();

            if (bBuildHyperlinks)
            {
                // MenuLinks.DropDownItems.Clear();
                MenuFolderNFile.DropDownItems.Clear();
                MenuHyperlinks.DropDownItems.Clear();
                MenuOther.DropDownItems.Clear();
                MenuFolderNFileCntxt.DropDownItems.Clear();
                MenuHyperlinksCntxt.DropDownItems.Clear();
                MenuOtherCntxt.DropDownItems.Clear();
            }

            foreach (KeyValuePair<string, HyperlinkMeta> HyperlinkListItem in HyperlinkList)
            {
                //
                //
                iHyperlinkCounter++;
                //if (iHyperlinkCounter++ < iHyperlinkCntxMax) {
                //    CntxtMenuTray.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                //} else {
                //    if (!bBuildHyperlinks) { break; };
                //}
                if (iHyperlinkCounter > 100) { break; };
                if (!bBuildHyperlinks) { continue; };
                //
                switch (HyperlinkListItem.Value.HyperlinkType)
                {
                    case (int)HyperlinkTypeIs.Hyperlink:
                        MenuHyperlinks.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                        MenuHyperlinksCntxt.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                        break;
                    case (int)HyperlinkTypeIs.Folder:
                        MenuFolderNFile.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                        MenuFolderNFileCntxt.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                        break;
                    case (int)HyperlinkTypeIs.UncFolder:
                        MenuOther.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                        MenuOtherCntxt.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                        break;
                    case (int)HyperlinkTypeIs.UncFile:
                        MenuOther.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                        MenuOtherCntxt.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                        break;
                    case (int)HyperlinkTypeIs.MailTo:
                        MenuOther.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                        MenuOtherCntxt.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                        break;
                    case (int)HyperlinkTypeIs.Document:
                        int iDocumentType = 1;
                        switch (iDocumentType)
                        {
                            case (int)DocumentTypeIs.WordDocument:
                                MenuFolderNFile.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                MenuFolderNFileCntxt.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                break;
                            case (int)DocumentTypeIs.ExcelDocument:
                                MenuFolderNFile.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                MenuFolderNFileCntxt.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                break;
                            case (int)DocumentTypeIs.OpenOfficeDocument:
                                MenuFolderNFile.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                MenuFolderNFileCntxt.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                break;
                            case (int)DocumentTypeIs.TextDocument:
                                MenuFolderNFile.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                MenuFolderNFileCntxt.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                break;
                            case (int)DocumentTypeIs.CodeDocument:
                                MenuFolderNFile.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                MenuFolderNFileCntxt.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                break;
                            case (int)DocumentTypeIs.BatDocument:
                                MenuFolderNFile.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                MenuFolderNFileCntxt.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                break;
                            case (int)DocumentTypeIs.ScriptDocument:
                                MenuFolderNFile.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                MenuFolderNFileCntxt.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                break;
                            default:
                                MenuFolderNFile.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                MenuFolderNFileCntxt.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                                break;
                        }
                        // ProcessArgument = HyperLinkPassed;
                        break;
                    default:
                        MenuFolderNFile.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                        MenuFolderNFileCntxt.DropDownItems.Add(HyperlinkListItem.Key, null, new EventHandler(ItemHyperlink_Click));
                        break;
                }
            }

            //CntxtMenuTray.DropDownItems.Add("-");
            //CntxtMenuTray.DropDownItems.Add("Cancel Menu", new EventHandler(ItemCancelMenu_Click));
            //CntxtMenuTray.DropDownItems.Add("-");
            //CntxtMenuTray.DropDownItems.Add(ItemHide.Text, new EventHandler(ItemHideMenu_Click));
            //CntxtMenuTray.DropDownItems.Add("-");
            //CntxtMenuTray.DropDownItems.Add("E&xit", new EventHandler(ItemExit_Click));
        }
        #endregion
        #region Button Click Navigation
        // MenuButtonPrev_Click
        private void MenuButtonPrev_Click(object sender, System.EventArgs e)
        {
            // Add event handler code here.
            // Int32 tempSequenceNumber = 0;
            if (SequenceNumberSelected == 0)
            {
                SequenceNumberSelected = SequenceNumberCurrent;
            }
            if (SequenceNumberSelected > 0)
            {
                SequenceNumberSelected -= 1;
                // read SQL
                if (SequenceNumberSelected == 0) { SequenceNumberSelected = SequenceNumberCurrent; }
                // read SQL
                string cmd = "select ";
                cmd = "where Id=" + SequenceNumberSelected.ToString() + sEmpty;
                ClipboardDataRead(cmd);
                ClipboardDataSetForm(false);
            }
        }
        // MenuButtonNext_Click
        private void MenuButtonNext_Click(object sender, System.EventArgs e)
        {
            // Add event handler code here.
            // Int32 tempSequenceNumber = 0;
            if (SequenceNumberSelected == 0)
            {
                SequenceNumberSelected = SequenceNumberCurrent;
            }
            if (SequenceNumberSelected <= SequenceNumberCurrent)
            {
                if (SequenceNumberSelected == SequenceNumberCurrent) { SequenceNumberSelected = 0; }
                SequenceNumberSelected += 1;
                // read SQL
                string cmd = "select ";
                cmd = "where Id=" + SequenceNumberSelected.ToString() + sEmpty;
                ClipboardDataRead(cmd);
                ClipboardDataSetForm(false);

            }
        }
        // MenuButtonSet_Click
        private void MenuButtonSet_Click(object sender, System.EventArgs e)
        {
            // Add event handler code here.
            // Int32 tempSequenceNumber = 0;
            if (SequenceNumberSelected <= SequenceNumberCurrent)
            {
                ClipboardDataSet(ClipMeta.cData);

            }
        }
        // MenuButtonStart_Click
        private void MenuButtonStart_Click(object sender, System.EventArgs e)
        {
            // Add event handler code here.
            // Int32 tempSequenceNumber = 0;
            if (SequenceNumberSelected <= SequenceNumberCurrent)
            {
                // Execute

            }
        }
        #endregion
        #region Read Write to Database
        /// <summary>
        /// Save the clipboard contents to the database
        /// </summary>
        /// <param name="iData">The current clipboard contents</param>
        /// <returns>true if the data was added to the database</returns>
        private bool ClipboardDataRead(string SelectCmdPassed)
        {
            bool DataRead = false;
            int CrListIdTemp = 0;
            // 
            ClipFile.Fmain.DbIo.CommandCurrent = sEmpty;
            ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand = sEmpty;
            // command
            // DbSyn.spOutputItem += ColText;
            // INSERT into X (
            if (SelectCmdPassed.Length == 0)
            {
                ClipFile.FileSqlConn.DbSyn.spOutputReadCommand = "Select *";
            }
            else
            {
                ClipFile.FileSqlConn.DbSyn.spOutputReadCommand = "Select *"; // SelectCmdPassed;
            }
            ClipFile.FileSqlConn.DbSyn.spOutputReadCommand += " from ";
            // ClipFile.FileSqlConn.DbSyn.spOutputReadCommand += "'"
            ClipFile.FileSqlConn.DbSyn.spOutputReadCommand += "ClipData";
            //
            if (SelectCmdPassed.Length > 0)
            {
                ClipFile.FileSqlConn.DbSyn.spOutputReadCommand += " " + SelectCmdPassed;
            }
            // ClipFile.FileSqlConn.DbSyn.spOutputReadCommand += "'";
            // ClipFile.FileSqlConn.DbSyn.spOutputReadCommand += "(";
            // ClipFile.FileSqlConn.DbSyn.spOutputReadCommand += "Id, Sequence, ClipDataWhen, ClipData1, ClipObject";
            // ClipFile.FileSqlConn.DbSyn.spOutputReadCommand += "Id, Sequence, ClipDataWhen, ClipData1, ClipObject";
            // ClipFile.FileSqlConn.DbSyn.spOutputReadCommand += ")";
            //
            // ClipFile.FileSqlConn.DbSyn.spOutputValues = " VALUES(@Id, @Sequence, @ClipDataWhen, @ClipData1, @ClipObject)";
            // ClipFile.FileSqlConn.DbSyn.spOutputValues = " VALUES(@Sequence, @ClipDataWhen, @ClipData1, @ClipObject)";
            // ClipFile.FileSqlConn.DbSyn.spOutputReadCommand += " " + ClipFile.FileSqlConn.DbSyn.spOutputValues;
            //
            if (ClipFile.FileSqlConn.DbSyn.spOutputReadCommand.Length > 0)
            {
                ClipFile.Fmain.DbIo.CommandCurrent = ClipFile.FileSqlConn.DbSyn.spOutputReadCommand;
                ClipFile.FileState.SqlDataWriteResult = ClipFile.SqlDataCommandCreate(ref ClipFile.Fmain);
            }
            //
            ClipFile.FileState.SqlDataReadResult = ClipFile.SqlDataRead(ref ClipFile.Fmain);
            // SqlCommand cmd = new SqlCommand("Select * from tablename", con);
            // using (SqlConnection myConnection = new SqlConnection(con))
            {
                // string oString = "Select * from ClipData";
                // SqlCommand oCmd = new SqlCommand(oString, myConnection);
                // ClipFile.FileSqlConn.DbSyn.spSqlFileViewCmd = "Select * from ClipData
                //oCmd.Parameters.AddWithValue("@Fname", fName);
                // myConnection.Open();
                // using (SqlDataReader oReader = ClipFile.Fmain.DbIo.SqlDbConn..ExecuteReader())
                // {
                bool cont = true;
                while (st.StateIsSuccessfulAll(ClipFile.SqlDataReadNext(ref ClipFile.Fmain)) && cont)
                {
                    // while (oReader.Read())
                    try
                    {
                        string mvalue = ClipFile.Fmain.DbIo.SqlDbReader["Sequence"].ToString();
                    }
                    catch
                    {
                        cont = false;
                        ClipFile.Fmain.DbIo.SqlDbReader.Close();
                    }
                    if (cont)
                    {
                        CrListIdTemp = (int)ClipFile.Fmain.DbIo.SqlDbReader["Sequence"];
                        if (CrListIdTemp > SequenceNumberCurrent) { SequenceNumberCurrent = CrListIdTemp; }
                        //
                        string ClipObject = (string)ClipFile.Fmain.DbIo.SqlDbReader["ClipObject"];
                        ClipMeta = Deserialize<ClipMetaDef>(ClipObject);
                        ClipMeta.SequenceNumber = (int)ClipFile.Fmain.DbIo.SqlDbReader["Sequence"];
                        ClipMeta.DataCreationTime = (DateTime)ClipFile.Fmain.DbIo.SqlDbReader["ClipDataWhen"];
                        string Data1 = (string)ClipFile.Fmain.DbIo.SqlDbReader["ClipData1"];
                        //
                        ClipboardRowWrite();
                        //
                        //ClipMetaOut = Serialize(ClipMeta);
                        //ClipFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@ClipObject", SqlDbType.VarBinary).Value = ClipMetaOut; // ClipMetaMemStream.GetBuffer(); // (Object) ClipMeta;
                        //
                        // Set Clipboard
                        // Set Form Text
                    }

                    //matchingPerson.firstName = oReader["FirstName"].ToString();
                    //matchingPerson.lastName = oReader["LastName"].ToString();
                }

                // myConnection.Close();
                // }
            }
            return DataRead;
        }
        /// <summary>
        /// Save the clipboard contents to the database
        /// </summary>
        /// <param name="iData">The current clipboard contents</param>
        /// <returns>true if the data was added to the database</returns>
        private bool ClipboardRowWrite()
        {
            bool DataAdded = false;
            // Id Date Data1 Content Seqence()
            string[] rowString = new string[5];
            rowString[0] = ClipMeta.SequenceNumber.ToString();
            rowString[1] = ClipMeta.DataCreationTime.ToString();
            rowString[2] = ClipMeta.Data1;
            rowString[3] = ClipMeta.cData;
            rowString[4] = ClipMeta.SequenceNumber.ToString();

            //if (ControlClipboardList.Rows.Count < ClipMeta.SequenceNumber)
            //{
            ControlClipboardList.Rows.Add(rowString);
            // ControlClipboardList.Rows[ClipMeta.SequenceNumber - 1].SetValues(rowString);
            //}
            return true;

        }
        /// <summary>
        /// Save the clipboard contents to the database
        /// </summary>
        /// <param name="iData">The current clipboard contents</param>
        /// <returns>true if the data was added to the database</returns>
        private bool ClipboardDataWrite()
        {
            bool DataAdded = false;
            string Data1 = sEmpty;
            // Detect Duplicates
            if (ClipMeta.cData == cDataLast) { return false; }
            cDataLast = ClipMeta.cData;
            //
            ClipFile.FileState.SqlDataInsertResult = StateIs.Started;
            SequenceNumberCurrent += 1;
            SequenceSet(SequenceNumberCurrent);
            ClipMeta.SequenceNumber = SequenceNumberCurrent;
            //
            ClipboardRowWrite();
            //
            ClipFile.Fmain.DbIo.CommandCurrent = sEmpty;
            ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand = sEmpty;
            // command
            // DbSyn.spOutputItem += ColText;
            // INSERT into X (
            ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand = "INSERT";
            ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand += " into ";
            // ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand += "'"
            ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand += "ClipData";
            // ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand += "'";
            ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand += "(";
            // ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand += "Id, Sequence, ClipDataWhen, ClipData1, ClipObject";
            ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand += "Id, Sequence, ClipDataWhen, ClipData1, ClipObject";
            ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand += ")";
            //
            ClipFile.FileSqlConn.DbSyn.spOutputValues = " VALUES(@Id, @Sequence, @ClipDataWhen, @ClipData1, @ClipObject)";
            // ClipFile.FileSqlConn.DbSyn.spOutputValues = " VALUES(@Sequence, @ClipDataWhen, @ClipData1, @ClipObject)";
            ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand += " " + ClipFile.FileSqlConn.DbSyn.spOutputValues;
            //
            if (ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand.Length > 0)
            {
                ClipFile.Fmain.DbIo.CommandCurrent = ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand;
                ClipFile.SqlDataCommandCreate(ref ClipFile.Fmain);
            }
            //
            string dt;
            string dt2;
            DateTime date = DateTime.Now;
            DateTime date2 = DateTime.Now;
            dt = date.ToLongTimeString();        // display format:  11:45:44 AM
            dt2 = date2.ToShortDateString();     // display format:  5/22/2010

            // cmd.Parameters.Add("@time_out", SqlDbType.NVarChar, 50).Value = dt;
            // cmd.Parameters.Add("@date_out", SqlDbType.NVarChar, 50).Value = dt2;
            // cmd.Parameters.Add("@date_time", SqlDbType.NVarChar, 50).Value = string.Concat(dt2, " ", dt); // display format:  11/11/2010 4:58:
            //
            CultureInfo MyCultureInfo = new CultureInfo("en-US");
            // 
            date = DateTime.Parse(string.Concat(dt2, " ", dt), MyCultureInfo, DateTimeStyles.NoCurrentDateDefault);
            //
            // ClipFile.FileSqlConn.DbSyn.spOutputValues = "VALUES (@Id, @Sequence, @ClipDataWhen, @ClipData1)";
            // ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand += " " + ClipFile.FileSqlConn.DbSyn.spOutputValues;
            // ClipFile.FileSqlConn.DbSyn.spOutputValues = "VALUES ('" + SequenceNumberCurrent + "', '" + SequenceNumberCurrent + "', '" + Data1 + "', '" + ClipMeta + "');";
            // ClipFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@Id", SqlDbType.Int).Value = SequenceNumberCurrent;
            // ClipFile.Fmain.DbIo.SqlDbCommand.Parameters.AddWithValue("@ClipDataWhen", DateTime.Now); // SqlDbType.DateTime).Value = new Win32TimeDef.SYSTEMTIME();
            // ClipFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@ClipDataWhen", SqlDbType.NVarChar, 50).Value = string.Concat(dt2, " ", dt); 
            // new Win32TimeDef.SYSTEMTIME();
            // ClipFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@ClipData1", SqlDbType.Text).Value = Data1;
            // ClipFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@ClipObject", SqlDbType.VarChar).Value = ClipMeta;
            //
            // ClipFile.FileSqlConn.DbSyn.spOutputValues = SequenceNumberCurrent + SequenceNumberCurrent + Data1 + ClipMeta;
            ClipFile.Fmain.DbIo.SqlDbCommand.Parameters.AddWithValue("@Id", SequenceNumberCurrent);
            ClipFile.Fmain.DbIo.SqlDbCommand.Parameters.AddWithValue("@Sequence", SequenceNumberCurrent);
            ClipFile.Fmain.DbIo.SqlDbCommand.Parameters.AddWithValue("@ClipDataWhen", date2); // SqlDbType.DateTime).Value = new Win32TimeDef.SYSTEMTIME();
            ClipFile.Fmain.DbIo.SqlDbCommand.Parameters.AddWithValue("@ClipData1", ClipMeta.Data1);
            // ClipFile.Fmain.DbIo.SqlDbCommand.Parameters.AddWithValue("@ClipObject", (Object)ClipMeta);

            ClipMetaOut = Serialize(ClipMeta);
            ClipFile.Fmain.DbIo.SqlDbCommand.Parameters.AddWithValue("@ClipObject", ClipMetaOut); // ClipMetaMemStream.GetBuffer(); // (Object) ClipMeta;

            //
            if (ClipFile.FileSqlConn.DbSyn.spOutputValues.Length > 0)            // ClipFile.Fmain.DbIo.CommandCurrent = ClipFile.FileSqlConn.DbSyn.spOutputInsertCommand;

            {
            }
            //
            // ClipFile.LocalId.LongResult = SqlDataWrite(ref FmainPassed);

            // return SqlDataInsertResult;
            ClipFile.SqlDataWrite(ref ClipFile.Fmain);

            return DataAdded;
        }
        #endregion
        #region (de)serialize
        public string SerializeNow(ClipMetaDef value)
        {
            // ClassToSerialize c = new ClassToSerialize();
            // File f = new File("temp.dat");
            // Stream s = f.Open(FileMode.Create);
            // ClipMetaOut = new Object();
            // ClipMetaOut = new MemoryStream();
            // BinaryFormatter b = new BinaryFormatter();
            // s.Close(
            //TextWriter writer = new TextWriter();
            StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
            //StringWriter writer = new StringWriter();
            //DataContractSerializer x = new DataContractSerializer(typeof(T));
            XmlSerializer serializer = new XmlSerializer(value.GetType());
            //XmlSerializer serializer = new XmlSerializer(typeof(Object));
            serializer.Serialize(writer, value);
            //serializer.Serialize(writer, value);
            return writer.ToString();
        }
        public byte[] SerializeNow2(ClipMetaDef ClipMetaPassed)
        {
            ClipMetaMemStream = new MemoryStream();
            //ClipMetaOut = new StreamWriter(ClipMetaMemStream);
            //ClipMetaOut.Write(ClipMetaPassed);
            //ClipMetaOut.Close();
            // ClipMetaMemStream.Write(ClipMetaPassed);
            BinaryFormatter b = new BinaryFormatter();
            ClipMetaMemStream = new MemoryStream();
            b.Serialize(ClipMetaMemStream, ClipMetaPassed);
            ClipMetaMemStream.Position = 0;

            byte[] serializedList = new byte[ClipMetaMemStream.Length];

            ClipMetaMemStream.Read(serializedList, 0, (int)ClipMetaMemStream.Length);
            // ClipMetaMemStream.Read(serializedList, 0, ClipMetaMemStream.Length);

            ClipMetaMemStream.Close();
            // ClipMetaOut = serializedList;
            return serializedList;
            // System.Runtime.Serialization.
        }
        public ClipMetaDef DeSerializeNow(byte[] s)
        {
            // ClassToSerialize c = new ClassToSerialize();
            // File f = new File("temp.dat");
            // Stream s = f.Open(FileMode.Open);
            ClipMetaDef ClipMetaNew;
            MemoryStream mMemStr = new MemoryStream();
            // TextReader rMemStr = new StreamReader();
            // 
            if (s.Length == 0) { return new ClipMetaDef(); }
            mMemStr.Write(s, 0, 0);
            BinaryFormatter b = new BinaryFormatter();
            ClipMetaNew = (ClipMetaDef)b.Deserialize(mMemStr);
            // Console.WriteLine(c.name);
            // s.Close();

            return ClipMetaNew;
        }
        //static MemoryStream memoryStream;
        //static DataContractSerializer serializer;
        //static StreamReader reader;
        public static string Serialize(object obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            // using (StreamWriter memoryStream = new StreamWriter(memoryStream))
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(memoryStream, obj);
                // StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
                // return writer.ToString();
                // DataContractSerializer serializer =  new DataContractSerializer(obj.GetType());
                // serializer.WriteObject(memoryStream, obj);
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }

        }
        public object DeSerialize(object obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            // using (StreamReader memoryStream = new StreamReader(memoryStream))
            using (StreamReader memoryStreamReader = new StreamReader(memoryStream))
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                // DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
                // serializer.WriteObject(memoryStream, obj);
                // This is all wrong.
                obj = serializer.Deserialize(memoryStreamReader);
                // memoryStream.Position = 0;
                // return reader.ReadToEnd();
                return obj;
            }
        }

        public static T Deserialize<T>(string xml)
        {
            using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(memoryStream, Encoding.UTF8, new XmlDictionaryReaderQuotas(), null);
                DataContractSerializer deserializer = new DataContractSerializer(typeof(T));
                return (T)deserializer.ReadObject(reader);
            }
        }
        public static T Deserialize2<T>(string xml)
        {
                //using (Stream stream = new MemoryStream())
                using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                //byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                //stream.Write(data, 0, data.Length);
                //stream.Position = 0;
                DataContractSerializer deserializer = new DataContractSerializer(typeof(T));
                return (T)deserializer.ReadObject(reader);
            }
        }
        #endregion
        #region Search
        /// <summary>
        /// Search the clipboard contents for hyperlinks and unc paths, etc
        /// </summary>
        /// <param name="iData">The current clipboard contents</param>
        /// <returns>true if new links were found, false otherwise</returns>
        private bool ClipboardSearch()
        {
            bool FoundNewLinks = false;
            //
            // If it is not text then quit
            // cannot search bitmap etc
            //
            if (!iData.GetDataPresent(DataFormats.Text))
            {
                // ToDo ClipboardSearch DataFormats.
                return false;
            }

            string ClipboardTextData;

            try
            {
                ClipboardTextData = (string)iData.GetData(DataFormats.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

            // Hyperlinks e.g. http://www.server.com/folder/file.aspx

            // ProcessStartInfo startInfo = new ProcessStartInfo(ProcessName);
            HyperlinkList.Clear();

            Regex UrlRegEx = new Regex(@"(\b(?:http|https|ftp|file|ms-help)://[^\p{Pi}\p{Pf}" + "\"" + @"\s>]+)", RegexOptions.IgnoreCase);
            UrlRegEx.Match(ClipboardTextData);

            foreach (Match UrlMatchRegEx in UrlRegEx.Matches(ClipboardTextData))
            {
                if (!HyperlinkList.ContainsKey(UrlMatchRegEx.ToString()))
                {
                    HyperlinkList.Add(UrlMatchRegEx.ToString(), new HyperlinkMeta(UrlMatchRegEx.ToString(), (int)HyperlinkTypeIs.Hyperlink));
                    FoundNewLinks = true;
                }
            }

            // Hyperlinks in text e.g. www.server.com/folder/file.aspx
            UrlRegEx = new Regex(@"(\bwww.[^\p{Pi}\p{Pf}" + "\"" + @"\s>]+)", RegexOptions.IgnoreCase);
            UrlRegEx.Match(ClipboardTextData);
            foreach (Match UrlMatchRegEx in UrlRegEx.Matches(ClipboardTextData))
            {
                string sTmp = @"http://" + UrlMatchRegEx.ToString();
                if (!HyperlinkList.ContainsKey(sTmp))
                {
                    HyperlinkList.Add(sTmp, new HyperlinkMeta(UrlMatchRegEx.ToString(), (int)HyperlinkTypeIs.Hyperlink));
                    FoundNewLinks = true;
                }
            }

            char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();
            string InvalidFileNameString = sEmpty;
            foreach (char cInvalidFChar in InvalidFileNameChars)
            {
                InvalidFileNameString += cInvalidFChar.ToString();
            }

            char[] InvalidPathChars = Path.GetInvalidPathChars();
            string InvalidPathString = sEmpty;
            foreach (char cInvalidFChar in InvalidPathChars)
            {
                InvalidPathString += cInvalidFChar.ToString();
            }

            // Files and folders - \\servername\foldername\
            // Regex rxBlock = new Regex(@"(\b\w:\\[\w \\]*)", RegexOptions.IgnoreCase);
            Regex FolderFileRegEx = new Regex(@"(\b\w:\\[\w \p{S}\\.]*)", RegexOptions.IgnoreCase);
            FolderFileRegEx.Match(ClipboardTextData);

            foreach (Match UrlMatchRegEx in FolderFileRegEx.Matches(ClipboardTextData))
            {
                // ToDo ClipboardSearch File Folder File split match string
                // ToDo and check invalid path chars vs invalid filename chars
                Regex LineRegEx = new Regex(@"(\b\w:\\[^" + InvalidPathString + "]*)", RegexOptions.IgnoreCase);
                LineRegEx.Match(UrlMatchRegEx.Value);
                foreach (Match LineMatchRegEx in LineRegEx.Matches(UrlMatchRegEx.Value))
                {
                    if (!HyperlinkList.ContainsKey(LineMatchRegEx.ToString()))
                    {
                        HyperlinkList.Add(LineMatchRegEx.ToString(), new HyperlinkMeta(LineMatchRegEx.ToString(), (int)HyperlinkTypeIs.Folder));
                        FoundNewLinks = true;
                    }
                }
            }

            // UNC Files 
            // ToDo ClipboardSearch needs work
            Regex UncRegEx = new Regex(@"(\\\\[^\s/:\*\?\" + "\"" + @"\<\>\|]+)", RegexOptions.IgnoreCase);
            UncRegEx.Match(ClipboardTextData);

            foreach (Match UrlMatchRegEx in UncRegEx.Matches(ClipboardTextData))
            {
                if (!HyperlinkList.ContainsKey(UrlMatchRegEx.ToString()))
                {
                    HyperlinkList.Add(UrlMatchRegEx.ToString(), new HyperlinkMeta(UrlMatchRegEx.ToString(), (int)HyperlinkTypeIs.UncFile));
                    FoundNewLinks = true;
                }
            }

            // UNC folders
            // ToDo ClipboardSearch needs work
            Regex UncFolderRegEx = new Regex(@"(\\\\[^\s/:\*\?\" + "\"" + @"\<\>\|]+\\)", RegexOptions.IgnoreCase);
            UncFolderRegEx.Match(ClipboardTextData);

            foreach (Match UrlMatchRegEx in UncFolderRegEx.Matches(ClipboardTextData))
            {
                if (!HyperlinkList.ContainsKey(UrlMatchRegEx.ToString()))
                {
                    HyperlinkList.Add(UrlMatchRegEx.ToString(), new HyperlinkMeta(UrlMatchRegEx.ToString(), (int)HyperlinkTypeIs.UncFolder));
                    FoundNewLinks = true;
                }
            }

            // Email Addresses
            Regex MailToRegEx = new Regex(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)", RegexOptions.IgnoreCase);
            MailToRegEx.Match(ClipboardTextData);

            foreach (Match UrlMatchRegEx in MailToRegEx.Matches(ClipboardTextData))
            {
                if (!HyperlinkList.ContainsKey(UrlMatchRegEx.ToString()))
                {
                    HyperlinkList.Add(UrlMatchRegEx.ToString(), new HyperlinkMeta(UrlMatchRegEx.ToString(), (int)HyperlinkTypeIs.MailTo));
                    FoundNewLinks = true;
                }
            }

            return FoundNewLinks;
        }
        #endregion
        /// <summary>
        /// Called when an item is chosen from the menu
        /// </summary>
        /// <param name="HyperLinkPassed">The link that was clicked</param>
        private void LinkOpen(string HyperLinkPassed)
        {
            try
            {
                // ToDo LinkOpen needs more work to check for missing files etc

                HyperlinkMeta HyperlinkToOpen;
                try
                {
                    if (HyperlinkList.TryGetValue(HyperLinkPassed, out HyperlinkToOpen))
                    {
                        //
                        // Run the link
                        //
                        string ProcessName = sEmpty;
                        string ProcessArgument = sEmpty;
                        string ProcessFileName = sEmpty;
                        switch (HyperlinkToOpen.HyperlinkType)
                        {
                            case (int)HyperlinkTypeIs.Hyperlink:
                                ProcessName = "IExplore.exe";
                                ProcessArgument = HyperLinkPassed;
                                break;
                            case (int)HyperlinkTypeIs.Folder:
                                ProcessName = "Explore.exe";
                                ProcessArgument = HyperLinkPassed;
                                break;
                            case (int)HyperlinkTypeIs.UncFolder:
                                ProcessName = "Explore.exe";
                                ProcessArgument = HyperLinkPassed;
                                break;
                            case (int)HyperlinkTypeIs.UncFile:
                                ProcessName = "Explore.exe";
                                ProcessFileName = HyperLinkPassed;
                                // ProcessArgument = HyperLinkPassed;
                                break;
                            case (int)HyperlinkTypeIs.MailTo:
                                ProcessName = "Outlook.exe";
                                ProcessArgument = HyperLinkPassed;
                                break;
                            case (int)HyperlinkTypeIs.Document:
                                ProcessName = "Word.exe";
                                ProcessArgument = HyperLinkPassed;
                                int iDocumentType = 1;
                                switch (iDocumentType)
                                {
                                    case (int)DocumentTypeIs.WordDocument:
                                        ProcessName = "Word.exe";
                                        break;
                                    case (int)DocumentTypeIs.ExcelDocument:
                                        ProcessName = "Excel.exe";
                                        break;
                                    case (int)DocumentTypeIs.OpenOfficeDocument:
                                        ProcessName = "soffice.exe";
                                        ProcessArgument = "-o -nologo -nodefault" + HyperLinkPassed;
                                        break;
                                    case (int)DocumentTypeIs.TextDocument:
                                        // SciTE.exe
                                        ProcessName = "SciTE.exe";
                                        break;
                                    case (int)DocumentTypeIs.CodeDocument:
                                        ProcessName = "SciTE.exe";
                                        break;
                                    case (int)DocumentTypeIs.BatDocument:
                                        ProcessName = "SciTE.exe";
                                        break;
                                    case (int)DocumentTypeIs.ScriptDocument:
                                        ProcessName = "SciTE.exe";
                                        break;
                                    default:
                                        ProcessName = "SciTE.exe";
                                        break;
                                }
                                // ProcessArgument = HyperLinkPassed;
                                break;
                            default:
                                ProcessName = "Explore.exe";
                                ProcessArgument = HyperLinkPassed;
                                break;
                        }

                        // ProcessStartInfo startInfo = new ProcessStartInfo(ProcessName);
                        HyperlinkToOpen.ProcessStartupInfo.WindowStyle = ProcessWindowStyle.Maximized;
                        // ProcessStartupInfo.
                        if (ProcessArgument.Length > 0) { HyperlinkToOpen.ProcessStartupInfo.Arguments = ProcessArgument; }
                        if (ProcessFileName.Length > 0) { HyperlinkToOpen.ProcessStartupInfo.FileName = ProcessFileName; }
                        System.Diagnostics.Process.Start(HyperlinkToOpen.ProcessStartupInfo);
                        /*

      // Get the path that stores user documents.
      String^ myDocumentsPath = Environment::GetFolderPath( Environment::SpecialFolder::Personal );
      myProcess->StartInfo->FileName = String::Concat( myDocumentsPath, "\\MyFile.doc" );
      myProcess->StartInfo->Verb = "Print";
      myProcess->StartInfo->CreateNoWindow = true;
      myProcess->Started();

                        HINSTANCE ShellExecute(          
                        HWND hwnd,
                        LPCTSTR lpOperation,
                        LPCTSTR lpFile,
                        LPCTSTR lpParameters,
                        LPCTSTR lpDirectory,
                        INT nShowCmd
                        );
                        */
                    }
                    // HyperlinkList
                    // HyperlinkList[ItemHL.Text];
                }
                catch (KeyNotFoundException)
                {
                    System.Console.WriteLine("Key = \"{1}\" was not found.", HyperLinkPassed);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #region Clipboard Data Set
        /// <summary>
        /// Show the clipboard contents in the window 
        /// and show the notification balloon if a link is found
        /// </summary>
        private void ClipboardDataSet(object dataPassed)
        {
            //
            // Data on the clipboard uses the 
            // IDataObject interface
            iData = Clipboard.GetDataObject();
            if (ClipMeta.Data1 == "Text")
            {
                Clipboard.SetData(DataFormats.Text, ClipMeta.cData);
                // iData.SetData(ClipMeta.cData);
            }
            else if (ClipMeta.Data1 == "RTF")
            {
                Clipboard.SetData(DataFormats.Rtf, ClipMeta.cData);
            }
            // iData = ClipMeta.ciData;
            // ClipMeta.cData = (Object)iData;
            //
            // iData = Clipboard.GetDataObject();
            // Clipboard.SetDataObject(dataPassed);
            //   Clipboard.SetText(dataPassed.ToString());
            // Clipboard.SetData("RTF", dataPassed);
            // Clipboard.SetDataObject(dataPassed);
            //ClipboardDataGet(true);
            // ClipMeta.cData = (Object)iData;
            // ClipMeta.DataCreationTime = null;
            //ClipMeta.DataProcessed = true;
            //ClipMeta.IdKey = 1; // Id
            //ClipMeta.SequenceNumber = 1; // Sequence

        }
        /// <summary>
        /// Show the clipboard contents in the window 
        /// and show the notification balloon if a link is found
        /// </summary>
        private void ClipboardDataGet(bool UseClipboardPassed)
        {
            //
            // Data on the clipboard uses the 
            // IDataObject interface
            //
            // IDataObject iData = new DataObject();
            string strText = "clipmon";

            UInt32 Temp = Win32ClipDef.GetClipboardSequenceNumber();

            try
            {
                if (UseClipboardPassed)
                {
                    iData = Clipboard.GetDataObject();
                    ClipMeta.ciData = iData;
                    // ClipMeta.cData = (Object)iData;
                }
                else
                {
                    iData = Clipboard.GetDataObject();
                    if (ClipMeta.Data1 == "Text")
                    {
                        iData.SetData(ClipMeta.cData);
                    }
                    else if (ClipMeta.Data1 == "RTF")
                    {
                        iData.SetData(DataFormats.Rtf, ClipMeta.cData);
                    }
                    // iData = ClipMeta.ciData;
                    // ClipMeta.cData = (Object)iData; 
                }
            }
            catch (System.Runtime.InteropServices.ExternalException externEx)
            {
                // Copying a field definition in Access 2002 causes this sometimes?
                Debug.WriteLine("InteropServices.ExternalException: {0}", externEx.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            ClipboardDataSetForm(true);

        }
        /// <summary>
        /// Show the clipboard contents in the window 
        /// and show the notification balloon if a link is found
        /// </summary>
        private void ClipboardDataSetForm(bool UseClipboardPassed)
        {
            //
            // Data on the clipboard uses the 
            // IDataObject interface
            //
            // IDataObject iData = new DataObject();
            string strText = "clipmon";

            // 
            // Get RTF if it is present
            //
            if (UseClipboardPassed)
            {
                if (iData.GetDataPresent(DataFormats.Rtf))
                {
                    ClipMeta.cData = (string)iData.GetData(DataFormats.Rtf);
                    ClipMeta.ciData = iData;
                    ControlClipboardText.Rtf = (string)ClipMeta.cData;
                    strText = "RTF";
                    ClipMeta.Data1 = "RTF";
                    // 
                    // Get Text if it is present
                    //
                }
                else if (iData.GetDataPresent(DataFormats.Text))
                {
                    ClipMeta.cData = (string)iData.GetData(DataFormats.Text);
                    ClipMeta.ciData = iData;
                    strText = "Text";
                    ClipMeta.Data1 = "Text";
                    ControlClipboardText.Text = (string)ClipMeta.cData;

                    Debug.WriteLine((string)iData.GetData(DataFormats.Text));
                }
                else
                {
                    //
                    // Only show RTF or TEXT
                    //
                    ControlClipboardText.Text = "(cannot display this format)";
                }
            }
            else
            {
                //
                strText = "Text";
                if (ClipMeta.Data1 == "RTF")
                {
                    ControlClipboardText.Rtf = (string)ClipMeta.cData;
                    ControlClipboardText2.Text = ControlClipboardText.Text;
                }
                else
                {
                    ControlClipboardText.Text = (string)ClipMeta.cData;
                }
            }
            MenuButtonSet.Text = ClipMeta.SequenceNumber.ToString() + ' ' + ClipMeta.Data1;

            // NotifyAreaIcon.Tooltip = strText;
            CntxtMenuIcon.Text = strText;
            if (!CntxtMenuIcon.Visible)
            {
                CntxtMenuIcon.Visible = true;
            }
            // NotifyAreaIcon.Show();

            MenuFormatBuild();
            MenuSupportedBuild();

            if (ClipboardSearch())
            {
                //
                // Found some new links
                //
                System.Text.StringBuilder strBalloon = new System.Text.StringBuilder(100);

                foreach (KeyValuePair<string, HyperlinkMeta> kvp in HyperlinkList)
                {
                    strBalloon.Append(kvp.Key);
                }

                MenuContextBuild(true);

                if (HyperlinkList.Count > 0)
                {

                    // NotifyAreaIcon.BalloonDisplay(NotificationAreaIcon.NOTIFYICONdwInfoFlags.NIIF_INFO, "Links", strBalloon.ToString());

                    // NotifyAreaIcon.BalloonDisplay(
                    //    NotificationAreaIcon.NOTIFYICONdwInfoFlags.NIIF_INFO,
                    //    "Links", 
                    //    strBalloon.ToString());
                    //
                    string strTemp = strBalloon.ToString();
                    int iTemp0 = strTemp.Length;
                    if (iTemp0 > 63) { iTemp0 = 63; }
                    CntxtMenuIcon.Text = strTemp.Substring(0, iTemp0);
                    CntxtMenuIcon.BalloonTipTitle = @"Links were found, including:";
                    CntxtMenuIcon.BalloonTipText = strTemp;
                    CntxtMenuIcon.ShowBalloonTip(10000);
                    // tmp = MessageBoxIcon.Exclamation;

                }
            }
        }
        #endregion
        #region Methods - Public
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new ClipFormMain());
        }
        // Win32ClipDef Win32Clip = new Win32ClipDef();
        // ToDo WndProc [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]        
        protected override void WndProc(ref Message mMsg)
        {
            switch ((MsgTypeIs)mMsg.Msg)
            {
                //
                // The WM_DRAWCLIPBOARD message is sent to the first window 
                // in the clipboard viewer chain when the content of the 
                // clipboard changes. This enables a clipboard viewer 
                // window to display the new content of the clipboard. 
                //
                case MsgTypeIs.WM_DRAWCLIPBOARD:

                    Debug.WriteLine("WindowProc DRAWCLIPBOARD: " + mMsg.Msg, "WndProc");

                    ClipboardDataGet(true);
                    // Add to database...
                    ClipboardDataWrite();

                    //
                    // Each window that receives the WM_DRAWCLIPBOARD message 
                    // must call the SendMessage function to pass the message 
                    // on to the next window in the clipboard viewer chain.
                    //

                    //SendMessage(_ClipboardViewerNext, mMsg.Msg, mMsg.WParam, mMsg.LParam);
                    Win32MsgDef.SendMessage(_ClipboardViewerNext, mMsg.Msg, mMsg.WParam, mMsg.LParam);
                    break;


                //
                // The WM_CHANGECBCHAIN message is sent to the first window 
                // in the clipboard viewer chain when a window is being 
                // removed from the chain. 
                //
                case MsgTypeIs.WM_CHANGECBCHAIN:
                    Debug.WriteLine("WM_CHANGECBCHAIN: lParam: " + mMsg.LParam, "WndProc");

                    // When a clipboard viewer window receives the WM_CHANGECBCHAIN message, 
                    // it should call the SendMessage function to pass the message to the 
                    // to the next window in the chain, unless the next window is the window 
                    // being removed. In this case, the clipboard viewer should save 
                    // the handle specified by the lParam parameter as the next window in the chain. 

                    //
                    // wParam is the Handle to the window being removed from 
                    // the clipboard viewer chain 
                    // lParam is the Handle to the next window in the chain 
                    // following the window being removed. 
                    if (mMsg.WParam == _ClipboardViewerNext)
                    {
                        //
                        // If wParam is the next clipboard viewer then it
                        // is being removed so update pointer to the next
                        // window in the clipboard chain
                        //
                        _ClipboardViewerNext = mMsg.LParam;
                    }
                    else
                    {
                        Win32MsgDef.SendMessage(_ClipboardViewerNext, mMsg.Msg, mMsg.WParam, mMsg.LParam);
                    }
                    break;

                default:
                    //
                    // Let the form process the messages that we are
                    // not interested in
                    //
                    base.WndProc(ref mMsg);
                    break;

            }

        }
        #endregion
        #region Event Handlers - Form, Menu & UI Controls
        // Form & Menus
        #region Event Handlers - Form
        /// <summary>
        /// Loads the form and registers with the Register Clipboard Chain.
        /// </summary> 
        private void FormMain_Load(object sender, System.EventArgs e)
        {
            RegisterClipboardViewer();
        }
        /// <summary>
        /// Closes the form and therefore Unregisters it from the Clipboard Chain.
        /// </summary> 
        private void FormMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UnregisterClipboardViewer();
        }
        /// <summary>
        /// Resizes the form controls the hide / show feature.
        /// </summary> 
        private void FormMain_Resize(object sender, System.EventArgs e)
        {
            if (!bShowWhenMinimized)
            {
                if ((WindowState == FormWindowState.Minimized) && (Visible == true))
                {
                    // hide when minimised
                    Visible = false;
                    ItemHide.Text = "Show";
                    MenuContextBuild(false);
                }
            }
        }
        #endregion
        #region Event Handlers - Menu Core (Exit, Cancel, Hide)
        /// <summary>
        /// Exit selected from menu, close application.
        /// </summary> 
        private void ItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Cancel selected (does not do anything).
        /// </summary> 
        private void ItemCancelMenu_Click(object sender, System.EventArgs e)
        {
            // Do nothing - Cancel the menu
        }
        /// <summary>
        /// Hide chosen from the menu, place in system tray.
        /// </summary> 
        private void ItemHideMenu_Click(object sender, System.EventArgs e)
        {

            ItemHide_Click(sender, e);
        }
        /// <summary>
        /// Hide chosen from the menu, place in system tray.
        /// </summary> 
        private void ItemHide_Click(object sender, System.EventArgs e)
        {
            bShowWhenMinimized = (!bShowWhenMinimized);
            ItemHide.Text = bShowWhenMinimized ? "Hide" : "Show";
            // MenuContextBuild(false);
            //
            Visible = bShowWhenMinimized;
            if (Visible == true)
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Normal;
                }
            }
        }
        #endregion
        #region Event Handlers - Hyperlink Menus
        /// <summary>
        /// Clear links selected, processed links and
        /// context menus are cleared.  Usually done
        /// when too many link items build up in the
        /// context menus.
        /// </summary> 
        private void MenuClearLinks_Click(object sender, EventArgs e)
        {
            // ProcessStartInfo startInfo = new ProcessStartInfo(ProcessName);
            HyperlinkList.Clear();

            // CntxtMenuTray.DropDownItems.Clear();

            // MenuLinks.DropDownItems.Clear();
            MenuFolderNFile.DropDownItems.Clear();
            MenuHyperlinks.DropDownItems.Clear();
            MenuOther.DropDownItems.Clear();
        }
        // MenuBuildLinks_Click
        /// <summary>
        /// Build links select in menu.  Rebuild the link context
        /// menus from the clipboard contents.
        /// </summary> 
        private void MenuBuildLinks_Click(object sender, EventArgs e)
        {
            // ProcessStartInfo startInfo = new ProcessStartInfo(ProcessName);
            // MenuClearLinks_Click(sender, e);
            ClipboardSearch();
            MenuContextBuild(true);
        }
        /// <summary>
        /// A hyperlink was selected from the context menus.
        /// Perform a LinkOpen and execute the verb for this link.
        /// </summary> 
        private void ItemHyperlink_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem ItemHL = (ToolStripMenuItem)sender;

            try
            {
                if (HyperlinkList.ContainsKey(ItemHL.Text))
                {
                    LinkOpen(ItemHL.Text);
                }
                // HyperlinkList
                // HyperlinkList[ItemHL.Text];
            }
            catch (KeyNotFoundException)
            {
                System.Console.WriteLine("Key = \"{1}\" was not found.", ItemHL.Text);
            }
        }
        #endregion
        #region Event Handlers - Notify Icon (Hide, NotitifyIcon)

        /// <summary>
        /// Notification Icon double-click, hide or show the UI.
        /// </summary> 
        private void NotifyAreaIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bShowFromMenu = true;
            ItemHide_Click(sender, e);
        }

        /// <summary>
        /// Notification Icon click, hide or show the UI.
        /// </summary> 
        private void NotifyAreaIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                bShowFromMenu = false;
                ItemHide_Click(sender, e);
            }
            else { bShowFromMenu = true; }
        }

        /// <summary>
        /// Notification Balloon click, currently hide or show the UI.
        /// Action to take not yet finalized...
        /// </summary> 
        private void NotifyAreaIcon_BalloonClick(object sender, System.EventArgs e)
        {
            if (HyperlinkList.Count == 1)
            {
                // ToDo NotifyAreaIcon_BalloonClick Balloon Click Action
                // string strItem = (string)HyperlinkList.ToArray()[0];

                // Only one link so open it
                // LinkOpen(strItem);
            }
            else
            {
                // NotifyAreaIcon.ContextMenuDisplay();
                // NotifyAreaIcon.Show();
                if (!CntxtMenuIcon.Visible)
                {
                    CntxtMenuIcon.Visible = true;
                }
            }
        }

        #endregion
        // Controls
        #region Event Handlers - Controls - Clipboard Control
        private void ControlClipboardText_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion
        #endregion
        #region IDisposable Implementation
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Title = "Clipboard Utility";
            Sender = this; SenderIsThis = this;
            StdProcess = new StdProcessDef("1", "0", "Clipboard")
            {
                // 1 of 2 Processes
                Title = "Windows Clipboard Utility",
                Verbosity = 7
            };
            StdKey = StdProcess.StdKey;
            StdProcess.StdScreen.DeviceForm = this;
            StdProcess.StdScreen.DeviceName = Name;
            StdProcess.StdScreen.ScreenObject = Screen.FromControl(this);
            //
            if (StdRunControlUi == null)
            {
                StdRunControlUi = new StdBaseRunControlUiDef(ref Sender, ref ConsoleSender, StdKey);
            }
            //
            StdRunControlUi = new StdBaseRunControlUiDef();
            StdRunControlUi.ControlCreate();
            //st = new StdConsoleManagerDef();
            #region Console creation
            st = new StdConsoleManagerDef(ref SenderIsThis, ConsoleSource, ClassRole, ClassFeatures)
            {
                StdProcess = this.StdProcess,
                // Core
                // Turns on all the other features.
                ConsoleOn = true,
                // Uses a system console (Output window)
                ConsoleApplication = true,
                // Accepts user responses on the console
                // in effect, pausing on error.
                ConsoleInputOn = true,
                // Write a log to disk.
                ConsoleToDisc = true,
                // Range now from 3 to 20. ToDo Review code.
                ConsoleVerbosity = this.StdProcess.Verbosity,
                // Trace 
                // Trap errors
                TraceDebugOn = true,
                // Enter Debugger on error
                TraceDebugDoErrorPrompt = true,
                // triggers full details tracing after error
                // Verbosity goes to 20 for (X) messages (data points).
                TraceBug = true,
                // Stop on every message.
                TraceBreakOnAll = false,
                // Use detailed messages
                TraceDisplayMessageDetail = true,
                // Trace Bug fields provide a fine grained control of
                // (upon error) the verbosity is set to 20
                // after X messages it is set back to its prior setting.
                // NOTE: This is very resource intensive.
                // It will dramatically slow down the app.
                // 
                // Internals
                TraceIterationInitialState = false,
                TraceDisplayInitialState = false,
                TraceBugInitialState = true,
                // You may want to run a trace cycle when the
                // app starts using the above Initial States.
                TraceFirst = false,
                // Checkpoints stops the run after X data points.
                TraceIterationCheckPointOn = true,
                TraceIterationCheckPoint = 13,
                // and/or you want to step thru a run.
                // Checkpoints stops the run after X data points.
                TraceBugCheckPointOn = true,
                // Prompt every X messages.
                TraceBugCheckPoint = 10,
                // Currently this barfs out framework info.
                // The FW SRT partly extends the DB objects
                // to the dictionary and field data.
                // This example (report) displays fields
                // and state of the C# language extension.
                // 
                // Delay tracing for X messages.
                TraceBugThreshold = 2000,
                // You may want to limit how many times you trace
                // errors (triggers) which might repeat many times if they occur.
                TraceBugOnForCount = 1,
                // However, you may want to resume trace after
                // a number of cycles. This in effective provides
                // periodic samples at high detail.
                TraceBugOnAgainCount = 200,
                // Trace Data creates a left margin area for counts data
                // This was legacy code. It should contain State info.
                // Format: counts: [999999]
                // message, attributes, characters: [999999.999999]
                // using the two state fields passes in detailed messages.
                // ToDo. Not implemented in code or defined well.
                TraceData = bOFF
            };
            ConsoleSender = st;
            // st.ClassFeaturesFlagsSet(ClassFeatures);
            //StdRunControlUi = st.StdRunControlUi;
            st.StdRunControlUiSetFrom(ref StdRunControlUi);
            // Icon
            //Icon = Icon.ExtractAssociatedIcon(@".\Resource\" + "MdmControlLeft.ico");
            StdNotify = new StdNotifyDef(ref SenderIsThis, ref ConsoleSender, StdKey, Title, true);
            StdNotifyIcon = new StdNotifyIconDef(ref StdNotify.SenderIsThis, ref ConsoleSender, StdKey, Title, false);
            StdNotifyIcon.StdNotifyAdd();
            #endregion
            #region Form Creation
            SuspendLayout();
            #region Form Object Creation
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClipFormMain));
            // MenuMain = new MainMenu(components);
            MenuMain = new MenuStrip();

            MenuFormats = new ToolStripMenuItem();
            MenuSupported = new ToolStripMenuItem();
            MenuLinks = new ToolStripMenuItem();

            MenuClearLinks = new ToolStripMenuItem();
            MenuClearLinksCntxt = new ToolStripMenuItem();
            MenuBuildLinks = new ToolStripMenuItem();
            MenuBuildLinksCntxt = new ToolStripMenuItem();

            MenuButtonBar = new ToolBar();
            MenuButtonStrip = new MenuStrip();
            MenuButtonPrev = new ToolStripButton();
            MenuButtonNext = new ToolStripButton();
            MenuButtonSet = new ToolStripButton();
            StdRunControlUi.ButtonStart = new ToolStripButton();


            MenuFolderNFile = new ToolStripMenuItem();
            MenuHyperlinks = new ToolStripMenuItem();
            MenuOther = new ToolStripMenuItem();
            MenuFolderNFileCntxt = new ToolStripMenuItem();
            MenuHyperlinksCntxt = new ToolStripMenuItem();
            MenuOtherCntxt = new ToolStripMenuItem();

            StdProcess = new StdProcessDef("1", "1", "Clipboard")
            {
                IconFileName = "ClipForm_Default.ico",
                Verbosity = 7
            };
            CntxtMenuTray = new ContextMenuStrip();
            CntxtMenuIcon = new NotifyIcon(components);
            CntxtMenu = new ContextMenu();

            StdKey = StdProcess.StdKey;
            StdNotify = new StdNotifyDef(StdKey, Title, true);
            StdNotifyRoot.Add(StdKey.Key, StdNotify);
            StdNotifyIcon = new StdNotifyIconDef(ref StdNotify.SenderIsThis, ref ConsoleSender, StdKey, Title, true);
            StdNotifyIcon.StdNotifyAdd();

            //ref CntxtMenuTray,
            //ref CntxtMenu,
            //ref CntxtMenuIcon 

            ItemSep1 = new ToolStripMenuItem();
            ItemSep2 = new ToolStripMenuItem();
            ItemSep3 = new ToolStripMenuItem();
            ItemSep4 = new ToolStripMenuItem();

            ItemHide = new ToolStripMenuItem();
            ItemExit = new ToolStripMenuItem();


            ControlClipboardText = new RichTextBox();
            ControlClipboardText2 = new RichTextBox();

            ItemSystray = new ToolStripMenuItem();
            ItemHyperlink = new ToolStripMenuItem();

            ControlClipboardListSetupLayout();
            ControlClipboardListInitialize();
            ControlClipboardListPopulateDataGridView();
            Application.EnableVisualStyles();

            #endregion
            #region Form Menu Definition
            // 
            // MenuMain
            // 
            // MenuMain.Items.AddRange(new ToolStripMenuItem[] {
            MenuMain.Items.AddRange(new ToolStripMenuItem[] {
            MenuFormats,
            MenuSupported,
            MenuLinks
            });
            // 
            // MenuFormats
            // 
            MenuFormats.MergeIndex = 0;
            MenuFormats.Text = "Formats";
            // 
            // MenuSupported
            // 
            MenuSupported.MergeIndex = 1;
            MenuSupported.Text = "Supported";
            // 
            // MenuLinks
            // 
            MenuLinks.MergeIndex = 2;
            MenuLinks.Text = "Links";
            //
            MenuLinks.DropDownItems.AddRange(new ToolStripMenuItem[] {
            MenuFolderNFile,
            MenuHyperlinks,
            MenuOther,
            ItemSep4,
            MenuClearLinks,
            MenuBuildLinks
            });
            // 
            // MenuFolderNFile
            // 
            MenuFolderNFile.MergeIndex = 0;
            MenuFolderNFile.Text = "FoldersAndFiles";
            MenuFolderNFileCntxt.MergeIndex = 0;
            MenuFolderNFileCntxt.Text = "FoldersAndFiles";
            // 
            // MenuHyperlinks
            // 
            MenuHyperlinks.MergeIndex = 1;
            MenuHyperlinks.Text = "Hyperlinks";
            MenuHyperlinksCntxt.MergeIndex = 1;
            MenuHyperlinksCntxt.Text = "Hyperlinks";
            // 
            // MenuOther
            // 
            MenuOther.MergeIndex = 2;
            MenuOther.Text = "Other Links";
            MenuOtherCntxt.MergeIndex = 2;
            MenuOtherCntxt.Text = "Other Links";
            // 
            // ItemSep4
            // 
            ItemSep4.MergeIndex = 3;
            ItemSep4.Text = "-";
            //
            // Buttons Prev Next
            //
            // ,
            // MenuButtonPrev,
            //MenuMain.Items.(MenuButtonPrev);
            //MenuButtonBar.Buttons.AddRange(new ToolBarButton[] {
            //    MenuButtonPrev,
            //    MenuButtonNext
            //});

            MenuMain.Items.AddRange(new ToolStripButton[] {
                MenuButtonPrev,
                MenuButtonNext,
                MenuButtonSet,
                StdRunControlUi.ButtonStart
           });
            // MenuButtonPrev
            MenuButtonPrev.Text = "<";
            MenuButtonPrev.Click += new EventHandler(MenuButtonPrev_Click);
            //MenuButtonPrev.DisplayStyle = ToolBarButtonStyle.PushButton;

            // MenuButtonNext
            MenuButtonNext.Text = ">";
            MenuButtonNext.Click += new EventHandler(MenuButtonNext_Click);

            // MenuButtonSet
            MenuButtonSet.Text = "C";
            MenuButtonSet.Click += new EventHandler(MenuButtonSet_Click);
            // 
            // MenuButtonStart
            StdRunControlUi.ButtonStart.Text = "E";
            StdRunControlUi.ButtonStart.Click += new EventHandler(MenuButtonStart_Click);
            // 
            // MenuClearLinks
            // 
            MenuClearLinks.MergeIndex = 4;
            MenuClearLinks.Text = "Clear Links menu";
            MenuClearLinks.Click += new System.EventHandler(MenuClearLinks_Click);
            MenuClearLinksCntxt.MergeIndex = 4;
            MenuClearLinksCntxt.Text = "Clear Links menu";
            MenuClearLinksCntxt.Click += new System.EventHandler(MenuClearLinks_Click);
            // 
            // MenuBuildLinks
            // 
            MenuBuildLinks.MergeIndex = 5;
            MenuBuildLinks.Text = "Build Links menu from current contents";
            MenuBuildLinks.Click += new System.EventHandler(MenuBuildLinks_Click);
            MenuBuildLinksCntxt.MergeIndex = 5;
            MenuBuildLinksCntxt.Text = "Build Links menu from current contents";
            MenuBuildLinksCntxt.Click += new System.EventHandler(MenuBuildLinks_Click);
            // 
            // NotifyAreaIcon
            // 
            CntxtMenuIcon.ContextMenuStrip = CntxtMenuTray;
            // NotifyAreaIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyAreaIcon.Icon")));
            CntxtMenuIcon.Icon = new Icon("MdmControlLeft.ico");
            CntxtMenuIcon.Text = "Mdm ClipBoard";
            CntxtMenuIcon.MouseClick += new MouseEventHandler(NotifyAreaIcon_MouseClick);
            CntxtMenuIcon.MouseDoubleClick += new MouseEventHandler(NotifyAreaIcon_MouseDoubleClick);
            //
            //ToolStripMenuItem[] temp = new ToolStripMenuItem[6];
            //MenuLinks.DropDownItems.CopyTo(temp, 0);
            //ToolStripItemCollection temp1 = new ToolStripItemCollection(CntxtMenuTray, temp);
            ////
            //// MenuLinks.DropDownItems.CopyTo(temp, 0);
            //// temp = MenuLinks.DropDownItems;
            //CntxtMenuTray.Items.AddRange(temp1);
            // 
            // CntxtMenuTray
            // 
            CntxtMenuTray.Items.AddRange(new ToolStripMenuItem[] {
            MenuFolderNFileCntxt,
            MenuHyperlinksCntxt,
            MenuOtherCntxt,
            ItemSep3,
            MenuClearLinksCntxt,
            MenuBuildLinksCntxt,
            ItemSep1,
            ItemHide,
            ItemSep2,
            ItemExit});
            // 
            // ItemSystray
            // 
            //ItemSystray.MergeIndex = 0;
            //ItemSystray.Text = "C:\\Temp\\SysTray";
            //ItemSystray.Click += new System.EventHandler(ItemHyperlink_Click);
            // 
            // ItemHyperlink
            // 
            //ItemHyperlink.DefaultItem = true;
            //ItemHyperlink.MergeIndex = 1;
            //ItemHyperlink.Text = "http://localhost/footprint/";
            //ItemHyperlink.Click += new System.EventHandler(ItemHyperlink_Click);
            // 
            // ItemSep3
            // 
            ItemSep3.MergeIndex = 3;
            ItemSep3.Text = "-";
            // 
            // ItemSep1
            // 
            ItemSep1.MergeIndex = 6;
            ItemSep1.Text = "-";
            // 
            // ItemHide
            // 
            ItemHide.MergeIndex = 7;
            ItemHide.Text = "Hide";
            ItemHide.Click += new System.EventHandler(ItemHideMenu_Click);
            // 
            // ItemSep2
            // 
            ItemSep2.MergeIndex = 8;
            ItemSep2.Text = "-";
            // 
            // ItemExit
            // 
            ItemExit.MergeIndex = 9;
            //ItemExit.MergeOrder = 1000;
            ItemExit.Text = "E&xit";
            ItemExit.Click += new System.EventHandler(ItemExit_Click);
            #endregion
            #region Form Component Definition
            // 
            // ControlClipboardText
            // 
            ControlClipboardText.DetectUrls = false;
            ControlClipboardText.Dock = DockStyle.Top;
            ControlClipboardText.Location = new System.Drawing.Point(0, 0);
            ControlClipboardText.Name = "ControlClipboardText";
            ControlClipboardText.ReadOnly = true;
            ControlClipboardText.Size = new System.Drawing.Size(1054, 200);
            ControlClipboardText.TabIndex = 0;
            ControlClipboardText.Text = "Test1";
            ControlClipboardText.WordWrap = false;
            ControlClipboardText.TextChanged += new System.EventHandler(ControlClipboardText_TextChanged);
            // 
            // ControlClipboardText2
            // 
            ControlClipboardText2.DetectUrls = false;
            ControlClipboardText2.Dock = DockStyle.Top;
            ControlClipboardText2.Location = new System.Drawing.Point(0, 201);
            ControlClipboardText2.Name = "ControlClipboardText2";
            ControlClipboardText2.ReadOnly = true;
            ControlClipboardText2.Size = new System.Drawing.Size(1054, 200);
            ControlClipboardText2.TabIndex = 1;
            ControlClipboardText2.Text = "Test2";
            ControlClipboardText2.WordWrap = false;
            ControlClipboardText2.TextChanged += new System.EventHandler(ControlClipboardText_TextChanged);
            // Controls.Add(ControlClipboardText);
            // Controls.Add(ControlClipboardText2);
            Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            // Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Icon = new Icon(@".\Resource\" + "MdmControlLeft.ico"); // ToDo
            Location = new System.Drawing.Point(100, 100);
            //Menu = MenuMain;
            MainMenuStrip = MenuMain;
            // Dock the MenuStrip to the top of the form.
            MenuMain.Dock = DockStyle.Top;

            // 
            // ClipFormMain
            // 
            AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            ClientSize = new System.Drawing.Size(1054, 680);
            Controls.AddRange(new Control[] {
                ControlClipboardList,
                ControlClipboardText2,
                ControlClipboardText
            });
            Controls.Add(ControlClipboardListButtonPanel);
            Controls.Add(MenuMain);


            StartPosition = FormStartPosition.Manual;
            Name = StdProcess.IconName;
            Text = StdProcess.Title;
            Load += new System.EventHandler(FormMain_Load);
            Closing += new System.ComponentModel.CancelEventHandler(FormMain_Closing);
            Resize += new System.EventHandler(FormMain_Resize);
            #endregion
            ResumeLayout(false);
            #endregion
            st.StdRunControlSetFrom(ref StdRunControlUi);
            InitializeClipComponent();
        }
        #endregion
        /// <summary>
        /// Initialized the program control and history
        /// and opens the database file for access.
        /// </summary> 
        private void InitializeClipComponent()
        {
            // This array will be used to keep track of changes to the clipboard data
            ProgControl = new ProgControlDef();
            ClipMeta = new ClipMetaDef();
            ClipHist = new List<ClipMetaDef>();
            bool HasFile = FileOpen();
            if (HasFile)
            {
                SequenceGet();
                ClipboardDataRead(sEmpty);
                ClipboardDataGet(true);
            } else
            {
                // ToDo throw something... lol.
                // ToDo debugging file creation
                // ToDo Check nested directory creation.
            }
        }
        #region Control Clipboard List
        private void ControlClipboardList_CellFormatting(object sender,
    DataGridViewCellFormattingEventArgs e)
        {
            if (e != null)
            {
                if (ControlClipboardList.Columns[e.ColumnIndex].Name == "Release Date")
                {
                    if (e.Value != null)
                    {
                        try
                        {
                            e.Value = DateTime.Parse(e.Value.ToString())
                                .ToLongDateString();
                            e.FormattingApplied = true;
                        }
                        catch (FormatException)
                        {
                            System.Console.WriteLine("{0} is not a valid date.", e.Value.ToString());
                        }
                    }
                }
            }
        }
        private void ControlClipboardListAddNewRowButton_Click(object sender, EventArgs e)
        {
            ControlClipboardList.Rows.Add();
        }
        private void ControlClipboardListDeleteRowButton_Click(object sender, EventArgs e)
        {
            if (ControlClipboardList.SelectedRows.Count > 0 &&
                ControlClipboardList.SelectedRows[0].Index !=
                ControlClipboardList.Rows.Count - 1)
            {
                ControlClipboardList.Rows.RemoveAt(
                    ControlClipboardList.SelectedRows[0].Index);
            }
        }
        private void ControlClipboardList_SelectionChanged(object sender, EventArgs e)
        {
            if (ControlClipboardList.SelectedRows.Count > 0 &&
                ControlClipboardList.SelectedRows[0].Index !=
                ControlClipboardList.Rows.Count - 1)
            {
                // SequenceNumberSelected = ControlClipboardList.SelectedRows[0].Index + 1;
                DataGridViewRow row = ControlClipboardList.SelectedRows[0];
                SequenceNumberSelected = (int)Convert.ToInt32(ControlClipboardList.SelectedRows[0].Cells["Sequence"].Value);
                ClipboardDataRead("where Id=" + SequenceNumberSelected.ToString());
                ClipboardDataSetForm(false);

                //ControlClipboardList.Rows.RemoveAt(
                //ControlClipboardList.SelectedRows[0].Index);
            }
        }
        private void ControlClipboardListSetupLayout()
        {
            Size = new System.Drawing.Size(650, 554);

            ControlClipboardListAddNewRowButton.Text = "Add Row";
            ControlClipboardListAddNewRowButton.Location = new System.Drawing.Point(10, 10);
            ControlClipboardListAddNewRowButton.Click += new EventHandler(ControlClipboardListAddNewRowButton_Click);

            ControlClipboardListDeleteRowButton.Text = "Delete Row";
            ControlClipboardListDeleteRowButton.Location = new System.Drawing.Point(100, 10);
            ControlClipboardListDeleteRowButton.Click += new EventHandler(ControlClipboardListDeleteRowButton_Click);

            ControlClipboardListButtonPanel.Controls.Add(ControlClipboardListAddNewRowButton);
            ControlClipboardListButtonPanel.Controls.Add(ControlClipboardListDeleteRowButton);
            ControlClipboardListButtonPanel.Height = 50;
            ControlClipboardListButtonPanel.Dock = DockStyle.Bottom;

            //Controls.Add(songsDataGridView);
            //Controls.Add(ControlClipboardListButtonPanel);

            ControlClipboardList.SelectionChanged += new EventHandler(
    ControlClipboardList_SelectionChanged);
        }
        private void ControlClipboardListInitialize()
        {
            //Controls.Add(songsDataGridView);

            ControlClipboardList.ColumnCount = 5;

            ControlClipboardList.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            ControlClipboardList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            ControlClipboardList.ColumnHeadersDefaultCellStyle.Font =
                new Font(ControlClipboardList.Font, FontStyle.Bold);

            ControlClipboardList.Name = "songsDataGridView";
            ControlClipboardList.Location = new System.Drawing.Point(0, 401);
            ControlClipboardList.Size = new System.Drawing.Size(554, 200);
            ControlClipboardList.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            ControlClipboardList.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            ControlClipboardList.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            ControlClipboardList.GridColor = Color.Black;
            ControlClipboardList.RowHeadersVisible = false;

            ControlClipboardList.Columns[0].Name = "Id";
            ControlClipboardList.Columns[1].Name = "Date";
            ControlClipboardList.Columns[2].Name = "Data1";
            ControlClipboardList.Columns[3].Name = "Content";
            ControlClipboardList.Columns[4].Name = "Sequence";
            ControlClipboardList.Columns[4].DefaultCellStyle.Font =
                new Font(ControlClipboardList.DefaultCellStyle.Font, FontStyle.Italic);
            ControlClipboardList.Columns["Content"].Width = 1000;

            ControlClipboardList.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            ControlClipboardList.MultiSelect = false;
            ControlClipboardList.Dock = DockStyle.Fill;

            ControlClipboardList.CellFormatting += new
                DataGridViewCellFormattingEventHandler(
                ControlClipboardList_CellFormatting);
        }
        private void ControlClipboardListPopulateDataGridView()
        {

            string[] row0 = { "11/22/1968", "29", "Revolution 9",
            "Beatles", "The Beatles [White Album]" };
            string[] row1 = { "1960", "6", "Fools Rush In",
            "Frank Sinatra", "Nice 'N' Easy" };
            string[] row2 = { "11/11/1971", "1", "One of These Days",
            "Pink Floyd", "Meddle" };
            string[] row3 = { "1988", "7", "Where Is My Mind?",
            "Pixies", "Surfer Rosa" };
            string[] row4 = { "5/1981", "9", "Can't Find My Mind",
            "Cramps", "Psychedelic Jungle" };
            string[] row5 = { "6/10/2003", "13",
            "Scatterbrain. (As Dead As Leaves.)",
            "Radiohead", "Hail to the Thief" };
            string[] row6 = { "6/30/1992", "3", "Dress", "P J Harvey", "Dry" };

            //ControlClipboardList.Rows.Add(row0);
            //ControlClipboardList.Rows.Add(row1);
            //ControlClipboardList.Rows.Add(row2);
            //ControlClipboardList.Rows.Add(row3);
            //ControlClipboardList.Rows.Add(row4);
            //ControlClipboardList.Rows.Add(row5);
            //C/ontrolClipboardList.Rows.Add(row6);

            ControlClipboardList.Columns[0].DisplayIndex = 1;
            ControlClipboardList.Columns[1].DisplayIndex = 2;
            ControlClipboardList.Columns[2].DisplayIndex = 0;
            ControlClipboardList.Columns[3].DisplayIndex = 3;
            ControlClipboardList.Columns[4].DisplayIndex = 4;
        }
        #endregion
        #region Sequence and Database File
        /// <summary>
        /// Open the clipboard database file.
        /// </summary> 
        private int SequenceGet()
        {
            int Sequence = 0;
            return Sequence;
        }
        /// <summary>
        /// Open the clipboard database file.
        /// </summary> 
        private bool SequenceSet(int SequenceNumberPassed)
        {
            SequenceNumberCurrent = SequenceNumberPassed;
            return true;
        }
        /// <summary>
        /// Open the clipboard database file.
        /// </summary> 
        private bool FileOpen(string fname)
        {
            bool HasFile;
            return true;
        }
        /// <summary>
        /// Open the clipboard database file.
        /// </summary> 
        private bool FileOpen()
        {
            bool HasFile = false;

            ClipFile = new mFileSql();
            StateIs FileDoResult = StateIs.InProgress;
            // File Summary
            // ClipFile.Fmain.Fs.
            ClipFile.Fmain.Fs.Direction = FileAction_DirectionIs.Both;
            // ClipFile.Fmain.Fs.ServerName = "MDMPC13\\SQLEXPRESS"; 
            // ClipFile.Fmain.Fs.ServerName = "MDMPC13\\MSSQLSERVER";
            ClipFile.Fmain.Fs.ServerName = "(localdb)\\MSSQLLocalDB";
            ClipFile.Fmain.Fs.DatabaseName = "MdmClipboardData";
            // File Id
            ClipFile.Fmain.Fs.FileId.FileName = "ClipData";
            ClipFile.Fmain.Fs.FileId.PropSystemPath =
                @"F:\Dev\Data\MdmData99\ClipData\MdmData99ClipDataVs2";
            // @"D:\Dev\Data\MdmData99\ClipData\MdmData99ClipDataVs1";
            // @"C:\System\Clipboard\ClipData";
            ClipFile.Fmain.Fs.FileId.FileNameSetFromLine(@"F:\Dev\Data\MdmData99\ClipData\MdmData99ClipDataVs2\ClipData");
            // ClipFile.Fmain.Fs.FileId.FileNameSetFromLine(@"D:\Dev\Data\MdmData99\ClipData\MdmData99ClipDataVs1\ClipData");
            // ClipFile.Fmain.Fs.FileId.FileNameSetFromLine(@"C:\System\Clipboard\ClipData");
            // Options
            ClipFile.Fmain.Fs.FileOpt.DoCreateFileDoesNotExist = true;
            ClipFile.Fmain.ConnStatus.DoKeepConn = false;
            ClipFile.Fmain.FileStatus.DoKeepConn = false;
            // File Action 
            ClipFile.Fmain.FileAction.Direction = ClipFile.Fmain.Fs.Direction;
            ClipFile.Fmain.FileAction.ToDo = FileAction_ToDoIs.Open;
            ClipFile.Fmain.FileAction.KeyName = "Table";
            ClipFile.Fmain.FileAction.IoMode = FileIo_ModeIs.Sql;
            ClipFile.Fmain.FileAction.FileReadMode = FileAction_ReadModeIs.Database;
            ClipFile.Fmain.FileAction.DoRetry = false;
            ClipFile.Fmain.FileAction.DoClearTarget = false;
            ClipFile.Fmain.FileAction.DoGetUiVs = false;
            //
            FileDoResult = ClipFile.FileDo(ref ClipFile.Fmain);
            //
            if (st.StateIsSuccessfulAll(FileDoResult))
            {
                HasFile = true;
            }
            //
            return HasFile;
        }
        #endregion
    }
}
