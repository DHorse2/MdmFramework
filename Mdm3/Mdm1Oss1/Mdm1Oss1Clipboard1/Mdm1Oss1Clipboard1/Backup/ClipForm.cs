using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
// using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using System.Data;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using Mdm.Oss.Decl;
using Mdm.Oss.FileUtil;
using Mdm.Oss.IeUtil;
using Mdm.Oss.WinUtil;
using Mdm.Oss.WinUtil.Types;


// using Mdm.Oss.ClipUtil;
// using Mdm.Oss.ClipUtil.Windows;

namespace Mdm.Oss.ClipUtil
{

    /// <summary>
    /// Clipboard Meta Data
    /// </summary>
    /// <remarks>
    /// Meta data for each addition to the clipboard
    /// </remarks>
    public class ClipMetaDef {
        public UInt32 IdKey;
        public DateTimeOffset DataCreationTime;
        /*
        	int year,
	        int month,
	        int day,
	        int hour,
	        int minute,
	        int second,
	        int millisecond,
	        Calendar calendar,
	        DateTimeKind kind
        */
        public bool DataProcessed;
        public UInt32 SequenceNumber;
        public IDataObject iData;
        //
		/// <summary>
        /// Creates meta data object with date, time, 
		/// sequence number, processed flag, format, contents.
        /// </summary> 
        public ClipMetaDef() {
            IdKey = 0;
            // Call the native GetSystemTime method
            // with the defined structure.
            Win32TimeDef.SYSTEMTIME DateTimeNow = new Win32TimeDef.SYSTEMTIME();
            Win32TimeDef.GetSystemTime(DateTimeNow);
            // Load Current Time
            DataCreationTime = new DateTimeOffset(
                DateTimeNow.wYear,
                DateTimeNow.wMonth,
                    // DateTimeNow.DayOfWeek,
                DateTimeNow.wDay,
                DateTimeNow.wHour,
                DateTimeNow.wMinute,
                DateTimeNow.wSecond,
                DateTimeNow.wMilliseconds,
                CultureInfo.CurrentCulture.Calendar,
                new TimeSpan(0)
                );
            DataProcessed = false;
            SequenceNumber = 0;
            iData = new DataObject();
        }
    }

    /// <summary>
    /// Clipboard Program Control
    /// </summary>
    /// <remarks>
    /// Counters and fields to control the history
    /// </remarks>
    public class ProgControlDef {
        public UInt32 IdKeyCurrent;
        public UInt32 SequenceNumber;
        public UInt32 ClipCount;
        DateTimeOffset DataCreationTime;
        //
		/// <summary>
        /// Program control with current id, sequence number.
        /// </summary> 
        public ProgControlDef() {
            ClipCount = 0;
            IdKeyCurrent = 0;
            SequenceNumber = 0;
            // DataCreationTime
        }
    }
    
    /// <summary>
	/// Clipboard Monitor
	/// </summary>
	/// <remarks>
    /// Based upon Rad Softwares clipboard example
	/// </remarks>
	public class ClipFormMain : System.Windows.Forms.Form
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

		#region Constants



		#endregion

		#region Fields

        #region Form Fields
		/// <summary>
        /// UI Form elements
        /// </summary> 
        private System.ComponentModel.IContainer components;

		private System.Windows.Forms.MenuStrip MenuMain;

        private ToolStripMenuItem MenuFormats;
		private ToolStripMenuItem MenuSupported;

        private ToolStripMenuItem MenuLinks;
        private ToolStripMenuItem MenuFolderNFile;
        private ToolStripMenuItem MenuHyperlinks;
        private ToolStripMenuItem MenuOther;
        private ToolStripMenuItem ItemSep3;
        private ToolStripMenuItem MenuClearLinks;
        private ToolStripMenuItem MenuBuildLinks;
        private ToolStripMenuItem ItemSep4;

        // protected System.Windows.Forms.ContextMenu CntxtMenuTray;
        protected System.Windows.Forms.ContextMenuStrip CntxtMenuTray;

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

		// private Mdm.Oss.ClipUtil.NotificationAreaIcon notAreaIcon;
        // private System.Windows.NotificationAreaIcon;
        // private System.Windows.Forms.MessageBox;
        private System.Windows.Forms.NotifyIcon notAreaIcon;
        private System.Windows.Forms.ContextMenu notAreaIconCntxtMenuTray;

        // private System.Windows.Forms.TextBox;
        // private System.Windows.Forms.ToolBar;
        // private System.Windows.Forms.ToolStrip;
        // private System.Windows.Forms.ToolTip;
        // private System.Windows.Forms.Application;
        // private System.Windows.Forms.BaseCollection;
        // private System.Windows.Forms.Clipboard;
        // private System.Windows.Forms.CommonDialog;
        // private System.Windows.Forms.MessageBox;
        private System.Windows.Forms.MessageBox notAreaMessageBox;

		private System.Windows.Forms.RichTextBox ControlClipboardText;
        #endregion

        #region Program Fields
        bool bShowWhenMinimized = true;
        bool bShowFromMenu = false;
        IntPtr _ClipboardViewerNext;
        // IDataObject iData;
        UInt32 SequenceNumberCurrent;

        ProgControlDef ProgControl;

        ClipMetaDef ClipMeta;

        List<ClipMetaDef> ClipHist;

        // Queue HyperlinkList = new Queue();
		/// <summary>
        /// Hyperlink meta data including link type,
		/// document type and process verb.
        /// </summary> 
        public class HyperlinkMeta {
            public int HyperlinkType;
            public int DocumentType;
            public string ProcessName;
            public ProcessStartInfo ProcessStartupInfo;
		/// <summary>
        /// Default constructor using text document and New verb.
        /// </summary> 
            public HyperlinkMeta() {
                HyperlinkType = (int) HyperlinkTypeIs.Document;
                DocumentType = (int) DocumentTypeIs.TextDocument;
                ProcessName = "New Document.txt";
                HyperlinkDataInit(ProcessName, HyperlinkType, DocumentType);
                return;
            }
		/// <summary>
        /// Create a text document link and use the passed verb.
        /// </summary> 
            public HyperlinkMeta(string PassedProcessName) {
                ProcessName = PassedProcessName;
                HyperlinkType = (int)HyperlinkTypeIs.Document;
                DocumentType = (int) DocumentTypeIs.TextDocument;
                HyperlinkDataInit(ProcessName, HyperlinkType, DocumentType);
                return;
            }
		/// <summary>
        /// Create the passed Hyperlink type and use the passed verb.
        /// </summary> 
            public HyperlinkMeta(string PassedProcessName, int iPassedHyperlinkType) {
                HyperlinkType = iPassedHyperlinkType;
                ProcessName = PassedProcessName;
                DocumentType = (int) DocumentTypeIs.TextDocument;
                HyperlinkDataInit(ProcessName, HyperlinkType, DocumentType);
                return;
            }
		/// <summary>
        /// Create the passed Hyperlink and document type and used the passed verb.
        /// </summary> 
            public HyperlinkMeta(string PassedProcessName, int iPassedHyperlinkType, int iPassedDocumentType) {
                HyperlinkType = iPassedHyperlinkType;
                ProcessName = PassedProcessName;
                DocumentType = iPassedDocumentType;
                HyperlinkDataInit(ProcessName, HyperlinkType, DocumentType);
            }
		/// <summary>
        /// Standard initialize called after constructors.
        /// </summary> 
            public void HyperlinkDataInit(string PassedProcessName, int iPassedHyperlinkType, int iPassedDocumentType) {
                HyperlinkType = iPassedHyperlinkType;
                DocumentType = iPassedDocumentType;
                ProcessName = PassedProcessName;
                if (PassedProcessName.Length == 0) { PassedProcessName = "New Document.txt"; }
                ProcessStartupInfo = new ProcessStartInfo(PassedProcessName);
                return;
            }
        }

		/// <summary>
        /// Dictionary list containing unique hyperlinks.
        /// </summary> 
        Dictionary<string, HyperlinkMeta> HyperlinkList = new Dictionary<string, HyperlinkMeta>();

		/// <summary>
        /// Hyperlink types.
        /// </summary> 
        private enum HyperlinkTypeIs : int {
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
        private enum DocumentTypeIs : int {
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

        #region Constructors
        
        Mdm.Oss.WinUtil.Types.h.HINSTANCE? Dave;
        
		/// <summary>
        /// Clipboard UI Form main method.
        /// </summary> 
        public ClipFormMain()
		{
			InitializeComponent();
            //if (!notAreaIcon.Visible) {
            //    notAreaIcon.Visible = true;
            //}
		}

		#endregion

		#region Properties - Public

		#endregion

		#region Methods - Private

        #endregion

        #region Methods - Clipboard Component
        /// <summary>
		/// Register this form as a Clipboard Viewer application
		/// </summary>
		private void RegisterClipboardViewer()
		{
			// _ClipboardViewerNext = SetClipboardViewer(this.Handle);
            _ClipboardViewerNext = Win32ClipDef.SetClipboardViewer(this.Handle);
        }

		/// <summary>
		/// Remove this form from the Clipboard Viewer list
		/// </summary>
		private void UnregisterClipboardViewer()
		{
			//ChangeClipboardChain(this.Handle, _ClipboardViewerNext);
            Win32ClipDef.ChangeClipboardChain(this.Handle, _ClipboardViewerNext);
		}

		/// <summary>
		/// Build a menu listing the formats supported by the contents of the clipboard
		/// </summary>
		/// <param name="iData">The current clipboard data object</param>
		private void MenuFormatBuild()
		{
            string[] astrFormatsNative = ClipMeta.iData.GetFormats(false);
            string[] astrFormatsAll = ClipMeta.iData.GetFormats(true);

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
                if (ClipMeta.iData.GetDataPresent(formatsAll[i]))
				{
					ItemFormat.Checked = true;
				}
				MenuSupported.DropDownItems.Add(ItemFormat);
		
			}
		}

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
            if (!ClipMeta.iData.GetDataPresent(DataFormats.Text))
			{
                // ToDo ClipboardSearch DataFormats.
				return false; 
			}

			string ClipboardTextData;

			try 
			{
                ClipboardTextData = (string)ClipMeta.iData.GetData(DataFormats.Text);
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
				if(!HyperlinkList.ContainsKey(UrlMatchRegEx.ToString()))
				{
                    HyperlinkList.Add(UrlMatchRegEx.ToString(), new HyperlinkMeta(UrlMatchRegEx.ToString(),  (int) HyperlinkTypeIs.Hyperlink));
					FoundNewLinks = true;
				}
			}

            // Hyperlinks in text e.g. www.server.com/folder/file.aspx
            UrlRegEx = new Regex(@"(\bwww.[^\p{Pi}\p{Pf}" + "\"" + @"\s>]+)", RegexOptions.IgnoreCase);
            UrlRegEx.Match(ClipboardTextData);
            foreach (Match UrlMatchRegEx in UrlRegEx.Matches(ClipboardTextData)) {
                string sTmp = @"http://" + UrlMatchRegEx.ToString();
                if (!HyperlinkList.ContainsKey(sTmp)) {
                    HyperlinkList.Add(sTmp, new HyperlinkMeta(UrlMatchRegEx.ToString(), (int)HyperlinkTypeIs.Hyperlink));
                    FoundNewLinks = true;
                }
            }

            char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();
            string InvalidFileNameString = "";
            foreach (char cInvalidFChar in InvalidFileNameChars) {
                InvalidFileNameString += cInvalidFChar.ToString();
            }

            char[] InvalidPathChars = Path.GetInvalidPathChars();
            string InvalidPathString = "";
            foreach (char cInvalidFChar in InvalidPathChars) {
                InvalidPathString += cInvalidFChar.ToString();
            }

			// Files and folders - \\servername\foldername\
            // Regex rxBlock = new Regex(@"(\b\w:\\[\w \\]*)", RegexOptions.IgnoreCase);
            Regex FolderFileRegEx = new Regex(@"(\b\w:\\[\w \p{S}\\.]*)", RegexOptions.IgnoreCase);
            FolderFileRegEx.Match(ClipboardTextData);

            foreach (Match UrlMatchRegEx in FolderFileRegEx.Matches(ClipboardTextData))
			{
                // TODO ClipboardSearch File Folder File split match string and check invalid path chars vs invalid filename chars
                Regex LineRegEx = new Regex(@"(\b\w:\\[^" + InvalidPathString + "]*)", RegexOptions.IgnoreCase);
                LineRegEx.Match(UrlMatchRegEx.Value);
                foreach (Match LineMatchRegEx in LineRegEx.Matches(UrlMatchRegEx.Value)) {
                    if (!HyperlinkList.ContainsKey(LineMatchRegEx.ToString())) {
                        HyperlinkList.Add(LineMatchRegEx.ToString(), new HyperlinkMeta(LineMatchRegEx.ToString(), (int)HyperlinkTypeIs.Folder));
                        FoundNewLinks = true;
                    }
                }
			}

			// UNC Files 
            // TODO ClipboardSearch needs work
			Regex UncRegEx = new Regex(@"(\\\\[^\s/:\*\?\" + "\"" + @"\<\>\|]+)", RegexOptions.IgnoreCase);
			UncRegEx.Match(ClipboardTextData);

			foreach (Match UrlMatchRegEx in UncRegEx.Matches(ClipboardTextData)) {
				if(!HyperlinkList.ContainsKey(UrlMatchRegEx.ToString())) {
                    HyperlinkList.Add(UrlMatchRegEx.ToString(), new HyperlinkMeta(UrlMatchRegEx.ToString(), (int)HyperlinkTypeIs.UncFile));
					FoundNewLinks = true;
				}
			}

			// UNC folders
            // TODO ClipboardSearch needs work
			Regex UncFolderRegEx = new Regex(@"(\\\\[^\s/:\*\?\" + "\"" + @"\<\>\|]+\\)", RegexOptions.IgnoreCase);
			UncFolderRegEx.Match(ClipboardTextData);

			foreach (Match UrlMatchRegEx in UncFolderRegEx.Matches(ClipboardTextData)) {
				if(!HyperlinkList.ContainsKey(UrlMatchRegEx.ToString())) {
                    HyperlinkList.Add(UrlMatchRegEx.ToString(), new HyperlinkMeta(UrlMatchRegEx.ToString(), (int)HyperlinkTypeIs.UncFolder));
					FoundNewLinks = true;
				}
			}

			// Email Addresses
			Regex MailToRegEx = new Regex(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)", RegexOptions.IgnoreCase);
			MailToRegEx.Match(ClipboardTextData);

			foreach (Match UrlMatchRegEx in MailToRegEx.Matches(ClipboardTextData)) {
				if(!HyperlinkList.ContainsKey(UrlMatchRegEx.ToString())) {
                    HyperlinkList.Add(UrlMatchRegEx.ToString(), new HyperlinkMeta(UrlMatchRegEx.ToString(), (int)HyperlinkTypeIs.MailTo));
					FoundNewLinks = true;
				}
			}

			return FoundNewLinks;
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

            if (bBuildHyperlinks) {
                // MenuLinks.DropDownItems.Clear();
                MenuFolderNFile.DropDownItems.Clear();
                MenuHyperlinks.DropDownItems.Clear();
                MenuOther.DropDownItems.Clear();
                MenuFolderNFileCntxt.DropDownItems.Clear();
                MenuHyperlinksCntxt.DropDownItems.Clear();
                MenuOtherCntxt.DropDownItems.Clear();
            }

            foreach (KeyValuePair<string, HyperlinkMeta> HyperlinkListItem in HyperlinkList) {
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
                switch (HyperlinkListItem.Value.HyperlinkType) {
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
                        switch (iDocumentType) {
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


		/// <summary>
		/// Called when an item is chosen from the menu
		/// </summary>
		/// <param name="HyperLinkPassed">The link that was clicked</param>
		private void LinkOpen(string HyperLinkPassed)
		{
			try {
                // TODO LinkOpen needs more work to check for missing files etc

                HyperlinkMeta HyperlinkToOpen;
                try {
                    if (HyperlinkList.TryGetValue(HyperLinkPassed, out HyperlinkToOpen)) {
				        //
				        // Run the link
				        //
                        string ProcessName = "";
                        string ProcessArgument = "";
                        string ProcessFileName = "";
                        switch (HyperlinkToOpen.HyperlinkType) {
                            case (int) HyperlinkTypeIs.Hyperlink:
                                ProcessName = "IExplore.exe";
                                ProcessArgument = HyperLinkPassed;
                                break;
                            case (int)HyperlinkTypeIs.Folder:
                                ProcessName = "Explore.exe";
                                ProcessArgument = HyperLinkPassed;
                                break;
                            case (int) HyperlinkTypeIs.UncFolder:
                                ProcessName = "Explore.exe";
                                ProcessArgument = HyperLinkPassed;
                                break;
                            case (int) HyperlinkTypeIs.UncFile:
                                ProcessName = "Explore.exe";
                                ProcessFileName = HyperLinkPassed;
                                // ProcessArgument = HyperLinkPassed;
                                break;
                            case (int) HyperlinkTypeIs.MailTo:
                                ProcessName = "Outlook.exe";
                                ProcessArgument = HyperLinkPassed;
                                break;
                            case (int) HyperlinkTypeIs.Document:
                                ProcessName = "Word.exe";
                                ProcessArgument = HyperLinkPassed;
                                int iDocumentType = 1;
                                switch (iDocumentType) {
                                    case (int) DocumentTypeIs.WordDocument:
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
                } catch (KeyNotFoundException) {
                    Console.WriteLine("Key = \"{1}\" was not found.", HyperLinkPassed);
                }


			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

		}

		/// <summary>
		/// Show the clipboard contents in the window 
		/// and show the notification balloon if a link is found
		/// </summary>
		private void ClipboardDataGet()
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
                ClipMeta.iData = Clipboard.GetDataObject();
			}
			catch (System.Runtime.InteropServices.ExternalException externEx)
			{
				// Copying a field definition in Access 2002 causes this sometimes?
				Debug.WriteLine("InteropServices.ExternalException: {0}", externEx.Message);
				return;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return;
			}
					
			// 
			// Get RTF if it is present
			//
            if (ClipMeta.iData.GetDataPresent(DataFormats.Rtf))
			{
                ControlClipboardText.Rtf = (string)ClipMeta.iData.GetData(DataFormats.Rtf);

                if (ClipMeta.iData.GetDataPresent(DataFormats.Text))
				{
					strText = "RTF";
				}
			} else {
				// 
				// Get Text if it is present
				//
                if (ClipMeta.iData.GetDataPresent(DataFormats.Text))
				{
                    ControlClipboardText.Text = (string)ClipMeta.iData.GetData(DataFormats.Text);
							
					strText = "Text";

                    Debug.WriteLine((string)ClipMeta.iData.GetData(DataFormats.Text));
				} else {
					//
					// Only show RTF or TEXT
					//
					ControlClipboardText.Text = "(cannot display this format)";
				}
			}

			// notAreaIcon.Tooltip = strText;
            notAreaIcon.Text = strText;
            if (!notAreaIcon.Visible) {
                notAreaIcon.Visible = true;
            }
            // notAreaIcon.Show();

            MenuFormatBuild();
            MenuSupportedBuild();

            if (ClipboardSearch()) {
				//
				// Found some new links
				//
				System.Text.StringBuilder strBalloon = new System.Text.StringBuilder(100);

                foreach (KeyValuePair<string, HyperlinkMeta> kvp in HyperlinkList) {
					strBalloon.Append(kvp.Key  + "\n");
				}

				MenuContextBuild(true);

				if (HyperlinkList.Count > 0) {

                    // notAreaIcon.BalloonDisplay(NotificationAreaIcon.NOTIFYICONdwInfoFlags.NIIF_INFO, "Links", strBalloon.ToString());

                    // notAreaIcon.BalloonDisplay(
                    //    NotificationAreaIcon.NOTIFYICONdwInfoFlags.NIIF_INFO,
                    //    "Links", 
                    //    strBalloon.ToString());
                    //
                    string strTemp = strBalloon.ToString();
                    int iTemp0 = strTemp.Length;
                    if (iTemp0 > 63) { iTemp0 = 63; }
                    notAreaIcon.Text = strTemp.Substring(0,iTemp0);
                    notAreaIcon.BalloonTipTitle = @"Links were found, including:";
                    notAreaIcon.BalloonTipText = strTemp;
                    notAreaIcon.ShowBalloonTip(10000);
                    // tmp = System.Windows.Forms.MessageBoxIcon.Exclamation;
 
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

        Win32ClipDef Win32Clip = new Win32ClipDef();


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

					ClipboardDataGet();

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
        private void FormMain_Load(object sender, System.EventArgs e) {
            RegisterClipboardViewer();
        }

		/// <summary>
        /// Closes the form and therefore Unregisters it from the Clipboard Chain.
        /// </summary> 
        private void FormMain_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            UnregisterClipboardViewer();
        }

		/// <summary>
        /// Resizes the form controls the hide / show feature.
        /// </summary> 
        private void FormMain_Resize(object sender, System.EventArgs e) {
            if (!bShowWhenMinimized) {
                if ((this.WindowState == FormWindowState.Minimized) && (this.Visible == true)) {
                    // hide when minimised
                    this.Visible = false;
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
			this.Close();
		}

		/// <summary>
        /// Cancel selected (does not do anything).
        /// </summary> 
        private void ItemCancelMenu_Click(object sender, System.EventArgs e) {
            // Do nothing - Cancel the menu
        }

		/// <summary>
        /// Hide chosen from the menu, place in system tray.
        /// </summary> 
        private void ItemHideMenu_Click(object sender, System.EventArgs e) {

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
            this.Visible = bShowWhenMinimized;
            if (this.Visible == true)
			{
				if (this.WindowState == FormWindowState.Minimized)
				{
					this.WindowState = FormWindowState.Normal;
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
        private void MenuClearLinks_Click(object sender, EventArgs e) {
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
        private void MenuBuildLinks_Click(object sender, EventArgs e) {
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

            try {
                if (HyperlinkList.ContainsKey(ItemHL.Text)) {
                    LinkOpen(ItemHL.Text);
                }
                // HyperlinkList
                // HyperlinkList[ItemHL.Text];
            } catch (KeyNotFoundException) {
                Console.WriteLine("Key = \"{1}\" was not found.", ItemHL.Text);
            }
        }
        #endregion
        #region Event Handlers - Notify Icon (Hide, NotitifyIcon)

		/// <summary>
        /// Notification Icon double-click, hide or show the UI.
        /// </summary> 
        private void notAreaIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
            bShowFromMenu = true;
            ItemHide_Click(sender, e);
        }

		/// <summary>
        /// Notification Icon click, hide or show the UI.
        /// </summary> 
        private void notAreaIcon_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                bShowFromMenu = false;
                ItemHide_Click(sender, e);
            } else { bShowFromMenu = true; }
        }

		/// <summary>
        /// Notification Balloon click, currently hide or show the UI.
		/// Action to take not yet finalized...
        /// </summary> 
        private void notAreaIcon_BalloonClick(object sender, System.EventArgs e) {
            if (HyperlinkList.Count == 1) {
                // TODO notAreaIcon_BalloonClick Balloon Click Action
                // string strItem = (string)HyperlinkList.ToArray()[0];

                // Only one link so open it
                // LinkOpen(strItem);
            } else {
                // notAreaIcon.ContextMenuDisplay();
                // notAreaIcon.Show();
                if (!notAreaIcon.Visible) {
                    notAreaIcon.Visible = true;
                }
            }
        }

        #endregion
        // Controls
        #region Event Handlers - Controls - Clipboard Control
        private void ControlClipboardText_TextChanged(object sender, EventArgs e) {

        }
        #endregion
        #endregion
        
        #region IDisposable Implementation
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
		#endregion


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            #region Form Creation
            #region Form Object Creation
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClipFormMain));
            // this.MenuMain = new System.Windows.Forms.MainMenu(this.components);
            this.MenuMain = new System.Windows.Forms.MenuStrip();

            this.MenuFormats = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSupported = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLinks = new System.Windows.Forms.ToolStripMenuItem();

            this.MenuClearLinks = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuClearLinksCntxt = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBuildLinks = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBuildLinksCntxt = new System.Windows.Forms.ToolStripMenuItem();

            this.MenuFolderNFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHyperlinks = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOther = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFolderNFileCntxt = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHyperlinksCntxt = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOtherCntxt = new System.Windows.Forms.ToolStripMenuItem();

            this.notAreaIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.CntxtMenuTray = new System.Windows.Forms.ContextMenuStrip();

            this.ItemSep1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSep2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSep3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSep4 = new System.Windows.Forms.ToolStripMenuItem();

            this.ItemHide = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemExit = new System.Windows.Forms.ToolStripMenuItem();

            this.notAreaIconCntxtMenuTray = new System.Windows.Forms.ContextMenu();

            this.ControlClipboardText = new System.Windows.Forms.RichTextBox();

            this.ItemSystray = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemHyperlink = new System.Windows.Forms.ToolStripMenuItem();

            #endregion
            #region Form Menu Definition
            this.SuspendLayout();
            // 
            // MenuMain
            // 
            // this.MenuMain.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.MenuMain.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.MenuFormats,
            this.MenuSupported,
            this.MenuLinks});
            // 
            // MenuFormats
            // 
            this.MenuFormats.MergeIndex = 0;
            this.MenuFormats.Text = "Formats";
            // 
            // MenuSupported
            // 
            this.MenuSupported.MergeIndex = 1;
            this.MenuSupported.Text = "Supported";
            // 
            // MenuLinks
            // 
            this.MenuLinks.MergeIndex = 2;
            this.MenuLinks.Text = "Links";
            //
            this.MenuLinks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.MenuFolderNFile,
            this.MenuHyperlinks,
            this.MenuOther,
            this.ItemSep4,
            this.MenuClearLinks,
            this.MenuBuildLinks});
            // 
            // MenuFolderNFile
            // 
            this.MenuFolderNFile.MergeIndex = 0;
            this.MenuFolderNFile.Text = "FoldersAndFiles";
            this.MenuFolderNFileCntxt.MergeIndex = 0;
            this.MenuFolderNFileCntxt.Text = "FoldersAndFiles";
            // 
            // MenuHyperlinks
            // 
            this.MenuHyperlinks.MergeIndex = 1;
            this.MenuHyperlinks.Text = "Hyperlinks";
            this.MenuHyperlinksCntxt.MergeIndex = 1;
            this.MenuHyperlinksCntxt.Text = "Hyperlinks";
            // 
            // MenuOther
            // 
            this.MenuOther.MergeIndex = 2;
            this.MenuOther.Text = "Other Links";
            this.MenuOtherCntxt.MergeIndex = 2;
            this.MenuOtherCntxt.Text = "Other Links";
            // 
            // ItemSep4
            // 
            this.ItemSep4.MergeIndex = 3;
            this.ItemSep4.Text = "-";
            // 
            // MenuClearLinks
            // 
            this.MenuClearLinks.MergeIndex = 4;
            this.MenuClearLinks.Text = "Clear Links menu";
            this.MenuClearLinks.Click += new System.EventHandler(this.MenuClearLinks_Click);
            this.MenuClearLinksCntxt.MergeIndex = 4;
            this.MenuClearLinksCntxt.Text = "Clear Links menu";
            this.MenuClearLinksCntxt.Click += new System.EventHandler(this.MenuClearLinks_Click);
            // 
            // MenuBuildLinks
            // 
            this.MenuBuildLinks.MergeIndex = 5;
            this.MenuBuildLinks.Text = "Build Links menu from current contents";
            this.MenuBuildLinks.Click += new System.EventHandler(this.MenuBuildLinks_Click);
            this.MenuBuildLinksCntxt.MergeIndex = 5;
            this.MenuBuildLinksCntxt.Text = "Build Links menu from current contents";
            this.MenuBuildLinksCntxt.Click += new System.EventHandler(this.MenuBuildLinks_Click);
            // 
            // notAreaIcon
            // 
            this.notAreaIcon.ContextMenuStrip = this.CntxtMenuTray;
            // this.notAreaIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notAreaIcon.Icon")));
            this.notAreaIcon.Icon = new Icon("MdmControlLeft.ico");
            this.notAreaIcon.Text = "Mdm ClipBoard";
            this.notAreaIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notAreaIcon_MouseClick);
            this.notAreaIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notAreaIcon_MouseDoubleClick);
            //
            //System.Windows.Forms.ToolStripMenuItem[] temp = new System.Windows.Forms.ToolStripMenuItem[6];
            //this.MenuLinks.DropDownItems.CopyTo(temp, 0);
            //System.Windows.Forms.ToolStripItemCollection temp1 = new System.Windows.Forms.ToolStripItemCollection(this.CntxtMenuTray, temp);
            ////
            //// this.MenuLinks.DropDownItems.CopyTo(temp, 0);
            //// temp = this.MenuLinks.DropDownItems;
            //this.CntxtMenuTray.Items.AddRange(temp1);
            // 
            // CntxtMenuTray
            // 
            this.CntxtMenuTray.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.MenuFolderNFileCntxt,
            this.MenuHyperlinksCntxt,
            this.MenuOtherCntxt,
            this.ItemSep3,
            this.MenuClearLinksCntxt,
            this.MenuBuildLinksCntxt,
            this.ItemSep1,
            this.ItemHide,
            this.ItemSep2,
            this.ItemExit});
            // 
            // ItemSystray
            // 
            //this.ItemSystray.MergeIndex = 0;
            //this.ItemSystray.Text = "C:\\Temp\\SysTray";
            //this.ItemSystray.Click += new System.EventHandler(this.ItemHyperlink_Click);
            // 
            // ItemHyperlink
            // 
            //this.ItemHyperlink.DefaultItem = true;
            //this.ItemHyperlink.MergeIndex = 1;
            //this.ItemHyperlink.Text = "http://localhost/footprint/";
            //this.ItemHyperlink.Click += new System.EventHandler(this.ItemHyperlink_Click);
            // 
            // ItemSep3
            // 
            this.ItemSep3.MergeIndex = 3;
            this.ItemSep3.Text = "-";
            // 
            // ItemSep1
            // 
            this.ItemSep1.MergeIndex = 6;
            this.ItemSep1.Text = "-";
            // 
            // ItemHide
            // 
            this.ItemHide.MergeIndex = 7;
            this.ItemHide.Text = "Hide";
            this.ItemHide.Click += new System.EventHandler(this.ItemHideMenu_Click);
            // 
            // ItemSep2
            // 
            this.ItemSep2.MergeIndex = 8;
            this.ItemSep2.Text = "-";
            // 
            // ItemExit
            // 
            this.ItemExit.MergeIndex = 9;
            //this.ItemExit.MergeOrder = 1000;
            this.ItemExit.Text = "E&xit";
            this.ItemExit.Click += new System.EventHandler(this.ItemExit_Click);
            #endregion
            #region Form Component Definition
            // 
            // ControlClipboardText
            // 
            this.ControlClipboardText.DetectUrls = false;
            this.ControlClipboardText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlClipboardText.Location = new System.Drawing.Point(0, 0);
            this.ControlClipboardText.Name = "ControlClipboardText";
            this.ControlClipboardText.ReadOnly = true;
            this.ControlClipboardText.Size = new System.Drawing.Size(354, 315);
            this.ControlClipboardText.TabIndex = 0;
            this.ControlClipboardText.Text = "";
            this.ControlClipboardText.WordWrap = false;
            this.ControlClipboardText.TextChanged += new System.EventHandler(this.ControlClipboardText_TextChanged);
            // 
            // ClipFormMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(354, 315);
            this.Controls.Add(this.ControlClipboardText);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Icon = new Icon("MdmControlLeft.ico");
            this.Location = new System.Drawing.Point(100, 100);
            //this.Menu = this.MenuMain;
            this.MainMenuStrip = this.MenuMain;
            // Dock the MenuStrip to the top of the form.
            this.MenuMain.Dock = DockStyle.Top;

            this.Controls.Add(this.MenuMain);


            this.Name = "ClipFormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Clipboard Manager";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormMain_Closing);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.ResumeLayout(false);
            #endregion
            #endregion
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
            //
            ClipboardDataGet();
            //
            HasFile = FileOpen();
		}

        private bool HasFile;
        private Mfile ClipFile;
        private FileActionDef FileAction_;
		/// <summary>
        /// Open the clipboard database file.
        /// </summary> 
        private bool FileOpen() {
            HasFile = false;
            ClipFile = new Mfile();
            // File Summary
            ClipFile.Fmain.Fs.Direction = (int)FileAction_DirectionIs.Both;
            ClipFile.Fmain.Fs.ServerName = "MDMPC11\\SQLEXPRESS";
            ClipFile.Fmain.Fs.DatabaseName = "MdmDatabase99";
            // File Id
            ClipFile.Fmain.Fs.FileId.FileName = "ClipData";
            ClipFile.Fmain.Fs.FileId.FileNameSetFromLine(null);
            ClipFile.Fmain.Fs.FileId.PropSystemPath =
                @"C:\System\Clipboard\ClipData";
            // Options
            ClipFile.Fmain.Fs.FileOpt.DoCreateFileDoesNotExist = true;
            ClipFile.Fmain.ConnStatus.DoKeepOpen = true;
            ClipFile.Fmain.DbStatus.DoKeepOpen = true;
            // File Action
            ClipFile.Fmain.FileAction.Direction = ClipFile.Fmain.Fs.Direction;
            ClipFile.Fmain.FileAction.ToDo = (long)FileAction_Do.Open;
            ClipFile.Fmain.FileAction.KeyName = "Table";
            ClipFile.Fmain.FileAction.Mode = (long)FileAction_Do.Table;
            ClipFile.Fmain.FileAction.FileReadMode = (long)FileIo_ModeIs.Sql;
            ClipFile.Fmain.FileAction.DoRetry = false;
            ClipFile.Fmain.FileAction.DoClearTarget = false;
            ClipFile.Fmain.FileAction.DoGetUiVs = false;
            //
            ClipFile.FileDo(ref ClipFile.Fmain);
            //
            return HasFile;
        }
    }
}
