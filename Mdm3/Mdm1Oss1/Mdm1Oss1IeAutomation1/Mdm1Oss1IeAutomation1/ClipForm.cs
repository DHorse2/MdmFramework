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
        IDataObject iData;
        //
        public ClipMetaDef() {
            IdKey = 0;
            // Call the native GetSystemTime method
            // with the defined structure.
            Shell32.SYSTEMTIME DateTimeNow = new Shell32.SYSTEMTIME();
            Shell32.GetSystemTime(ref DateTimeNow);
            // Load Current Time
            DataCreationTime = new DateTimeOffset(
                DateTimeNow.wYear,
                DateTimeNow.wMonth,
                    // DateTimeNow.wDayOfWeek,
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
        private System.ComponentModel.IContainer components;

		private System.Windows.Forms.MainMenu Mclipboard1MenuMain;

        private MenuItem Mclipboard1MenuFormats;
		private MenuItem Mclipboard1MenuSupported;

        private MenuItem Mclipboard1MenuLinks;
        private MenuItem Mclipboard1MenuFolderNFile;
        private MenuItem Mclipboard1MenuHyperlinks;
        private MenuItem Mclipboard1MenuOther;

        protected System.Windows.Forms.ContextMenu Mclipboard1CntxtMenuTray;

        private MenuItem Mclipboard1ItemExit;
		private MenuItem Mclipboard1ItemHide;
		private MenuItem Mclipboard1ItemSep2;
		private MenuItem Mclipboard1ItemSep1;
		private MenuItem Mclipboard1ItemHyperlink;
		private MenuItem Mclipboard1ItemSystray;

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

		private System.Windows.Forms.RichTextBox Mclipboard1ControlClipboardText;
        #endregion

        #region Program Fields
        bool bShowWhenMinimized = true;
        IntPtr _ClipboardViewerNext;

        UInt32 SequenceNumberCurrent;

        ProgControlDef ProgControl;

        ClipMetaDef ClipMeta;

        List<ClipMetaDef> ClipHist;

        // Queue dctHyperlink = new Queue();
        public class mccHyperlink {
            public int iHyperlinkType;
            public int iDocumentType;
            public string sProcessName;
            public ProcessStartInfo mcStartInfo;
            public mccHyperlink() {
                iHyperlinkType = (int) eHyperlinkType.is_Document;
                iDocumentType = (int) eDocumentType.is_TextDocument;
                sProcessName = "New Document.txt";
                mcHyperlinkInit(sProcessName, iHyperlinkType, iDocumentType);
                return;
            }
            public mccHyperlink(string sPassedProcessName) {
                sProcessName = sPassedProcessName;
                iHyperlinkType = (int)eHyperlinkType.is_Document;
                iDocumentType = (int) eDocumentType.is_TextDocument;
                mcHyperlinkInit(sProcessName, iHyperlinkType, iDocumentType);
                return;
            }
            public mccHyperlink(string sPassedProcessName, int iPassedHyperlinkType) {
                iHyperlinkType = iPassedHyperlinkType;
                sProcessName = sPassedProcessName;
                iDocumentType = (int) eDocumentType.is_TextDocument;
                mcHyperlinkInit(sProcessName, iHyperlinkType, iDocumentType);
                return;
            }
            public mccHyperlink(string sPassedProcessName, int iPassedHyperlinkType, int iPassedDocumentType) {
                iHyperlinkType = iPassedHyperlinkType;
                sProcessName = sPassedProcessName;
                iDocumentType = iPassedDocumentType;
                mcHyperlinkInit(sProcessName, iHyperlinkType, iDocumentType);
            }
            public void mcHyperlinkInit(string sPassedProcessName, int iPassedHyperlinkType, int iPassedDocumentType) {
                iHyperlinkType = iPassedHyperlinkType;
                iDocumentType = iPassedDocumentType;
                sProcessName = sPassedProcessName;
                if (sPassedProcessName.Length == 0) { sPassedProcessName = "New Document.txt"; }
                mcStartInfo = new ProcessStartInfo(sPassedProcessName);
                return;
            }
        }

        Dictionary<string, mccHyperlink> dctHyperlink = new Dictionary<string, mccHyperlink>();

        private enum eHyperlinkType : int {
            is_Hyperlink,
            is_Folder,
            is_UncFolder,
            is_UncFile,
            is_MailTo,
            is_Document
        };

        private enum eDocumentType : int {
            is_WordDocument,
            is_ExcelDocument,
            is_OpenOfficeDocument,
            is_TextDocument,
            is_CodeDocument,
            is_BatDocument,
            is_ScriptDocument
        };
        #endregion

        #endregion

        #region Constructors

        public ClipFormMain()
		{
			InitializeComponent();
            if (!notAreaIcon.Visible) {
                notAreaIcon.Visible = true;
            }
            
		}

		#endregion

		#region Properties - Public

		#endregion

		#region Methods - Private

		/// <summary>
		/// Register this form as a Clipboard Viewer application
		/// </summary>
		private void RegisterClipboardViewer()
		{
			// _ClipboardViewerNext = SetClipboardViewer(this.Handle);
            _ClipboardViewerNext = User32.SetClipboardViewer(this.Handle);
        }

		/// <summary>
		/// Remove this form from the Clipboard Viewer list
		/// </summary>
		private void UnregisterClipboardViewer()
		{
			//ChangeClipboardChain(this.Handle, _ClipboardViewerNext);
            User32.ChangeClipboardChain(this.Handle, _ClipboardViewerNext);
		}

		/// <summary>
		/// Build a menu listing the formats supported by the contents of the clipboard
		/// </summary>
		/// <param name="iData">The current clipboard data object</param>
		private void FormatMenuBuild(IDataObject iData)
		{
			string[] astrFormatsNative = iData.GetFormats(false);
			string[] astrFormatsAll = iData.GetFormats(true);

			Hashtable formatList = new Hashtable(10);

			Mclipboard1MenuFormats.MenuItems.Clear();

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
				MenuItem Mclipboard1ItemNew = new MenuItem(item.Key.ToString() + "\t" + item.Value.ToString());
				Mclipboard1MenuFormats.MenuItems.Add(Mclipboard1ItemNew);
			}
		}

		/// <summary>
		/// list the formats that are supported from the default clipboard formats.
		/// </summary>
		/// <param name="iData">The current clipboard contents</param>
		private void SupportedMenuBuild(IDataObject iData)
		{
			Mclipboard1MenuSupported.MenuItems.Clear();
		
			for (int i = 0; i <= formatsAll.GetUpperBound(0); i++)
			{
				MenuItem Mclipboard1ItemFormat = new MenuItem(formatsAllDesc[i]);
				//
				// Get supported formats
				//
				if (iData.GetDataPresent(formatsAll[i]))
				{
					Mclipboard1ItemFormat.Checked = true;
				}
				Mclipboard1MenuSupported.MenuItems.Add(Mclipboard1ItemFormat);
		
			}
		}

		/// <summary>
		/// Search the clipboard contents for hyperlinks and unc paths, etc
		/// </summary>
		/// <param name="iData">The current clipboard contents</param>
		/// <returns>true if new links were found, false otherwise</returns>
		private bool ClipboardSearch(IDataObject iData)
		{
			bool FoundNewLinks = false;
			//
			// If it is not text then quit
			// cannot search bitmap etc
			//
			if (!iData.GetDataPresent(DataFormats.Text))
			{
                // ToDo DataFormats.
				return false; 
			}

			string strClipboardText;

			try 
			{
				 strClipboardText = (string)iData.GetData(DataFormats.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return false;
			}
			
			// Hyperlinks e.g. http://www.server.com/folder/file.aspx

            // ProcessStartInfo startInfo = new ProcessStartInfo(sProcessName);
            dctHyperlink.Clear();

            Regex rxURL = new Regex(@"(\b(?:http|https|ftp|file)://[^\p{Pi}\p{Pf}" + "\"" + @"\s>]+)", RegexOptions.IgnoreCase);
            rxURL.Match(strClipboardText);

			foreach (Match rxMatch in rxURL.Matches(strClipboardText))
			{
				if(!dctHyperlink.ContainsKey(rxMatch.ToString()))
				{
                    dctHyperlink.Add(rxMatch.ToString(), new mccHyperlink(rxMatch.ToString(),  (int) eHyperlinkType.is_Hyperlink));
					FoundNewLinks = true;
				}
			}

            // Hyperlinks in text e.g. www.server.com/folder/file.aspx
            rxURL = new Regex(@"(\bwww.[^\p{Pi}\p{Pf}" + "\"" + @"\s>]+)", RegexOptions.IgnoreCase);
            rxURL.Match(strClipboardText);
            foreach (Match rxMatch in rxURL.Matches(strClipboardText)) {
                string sTmp = @"http://" + rxMatch.ToString();
                if (!dctHyperlink.ContainsKey(sTmp)) {
                    dctHyperlink.Add(sTmp, new mccHyperlink(rxMatch.ToString(), (int)eHyperlinkType.is_Hyperlink));
                    FoundNewLinks = true;
                }
            }

            char[] cInvalidFileNameChars = Path.GetInvalidFileNameChars();
            string sInvalidFileNameChars = "";
            foreach (char cInvalidFChar in cInvalidFileNameChars) {
                sInvalidFileNameChars += cInvalidFChar.ToString();
            }

            char[] cInvalidPathChars = Path.GetInvalidPathChars();
            string sInvalidPathChars = "";
            foreach (char cInvalidFChar in cInvalidPathChars) {
                sInvalidPathChars += cInvalidFChar.ToString();
            }

			// Files and folders - \\servername\foldername\
            // Regex rxBlock = new Regex(@"(\b\w:\\[\w \\]*)", RegexOptions.IgnoreCase);
            Regex rxFolderFile = new Regex(@"(\b\w:\\[\w \p{S}\\.]*)", RegexOptions.IgnoreCase);
            rxFolderFile.Match(strClipboardText);

            foreach (Match rxMatch in rxFolderFile.Matches(strClipboardText))
			{
                // TODO File Folder File split match string and check invalid path chars vs invalid filename chars
                Regex rxLine = new Regex(@"(\b\w:\\[^" + sInvalidPathChars + "]*)", RegexOptions.IgnoreCase);
                rxLine.Match(rxMatch.Value);
                foreach (Match rxLineMatch in rxLine.Matches(rxMatch.Value)) {
                    if (!dctHyperlink.ContainsKey(rxLineMatch.ToString())) {
                        dctHyperlink.Add(rxLineMatch.ToString(), new mccHyperlink(rxLineMatch.ToString(), (int)eHyperlinkType.is_Folder));
                        FoundNewLinks = true;
                    }
                }
			}

			// UNC Files 
			// TODO needs work
			Regex rxUNC = new Regex(@"(\\\\[^\s/:\*\?\" + "\"" + @"\<\>\|]+)", RegexOptions.IgnoreCase);
			rxUNC.Match(strClipboardText);

			foreach (Match rxMatch in rxUNC.Matches(strClipboardText)) {
				if(!dctHyperlink.ContainsKey(rxMatch.ToString())) {
                    dctHyperlink.Add(rxMatch.ToString(), new mccHyperlink(rxMatch.ToString(), (int)eHyperlinkType.is_UncFile));
					FoundNewLinks = true;
				}
			}

			// UNC folders
			// TODO needs work
			Regex rxUNCFolder = new Regex(@"(\\\\[^\s/:\*\?\" + "\"" + @"\<\>\|]+\\)", RegexOptions.IgnoreCase);
			rxUNCFolder.Match(strClipboardText);

			foreach (Match rxMatch in rxUNCFolder.Matches(strClipboardText)) {
				if(!dctHyperlink.ContainsKey(rxMatch.ToString())) {
                    dctHyperlink.Add(rxMatch.ToString(), new mccHyperlink(rxMatch.ToString(), (int)eHyperlinkType.is_UncFolder));
					FoundNewLinks = true;
				}
			}

			// Email Addresses
			Regex rxEmailAddress = new Regex(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)", RegexOptions.IgnoreCase);
			rxEmailAddress.Match(strClipboardText);

			foreach (Match rxMatch in rxEmailAddress.Matches(strClipboardText)) {
				if(!dctHyperlink.ContainsKey(rxMatch.ToString())) {
                    dctHyperlink.Add(rxMatch.ToString(), new mccHyperlink(rxMatch.ToString(), (int)eHyperlinkType.is_MailTo));
					FoundNewLinks = true;
				}
			}

			return FoundNewLinks;
		}

		/// <summary>
		/// Build the system tray menu from the hyperlink list
		/// </summary>
		private void ContextMenuBuild(bool bBuildHyperlinks)
		{
            Dictionary<string, mccHyperlink>.KeyCollection _linkKeyColl = dctHyperlink.Keys;

			//
			// Only show the last 10 items
			//
            int iHyperlinkCounter = 0;
            int iHyperlinkCntxMax = 10;
            Mclipboard1CntxtMenuTray.MenuItems.Clear();

            if (bBuildHyperlinks) {
                // Mclipboard1MenuLinks.MenuItems.Clear();
                Mclipboard1MenuFolderNFile.MenuItems.Clear();
                Mclipboard1MenuHyperlinks.MenuItems.Clear();
                Mclipboard1MenuOther.MenuItems.Clear();
            }

            foreach (KeyValuePair<string, mccHyperlink> kvp in dctHyperlink) {
                //
                //
                if (iHyperlinkCounter++ < iHyperlinkCntxMax) {
                    Mclipboard1CntxtMenuTray.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                } else {
                    if (!bBuildHyperlinks) { break; };
                }
                if (iHyperlinkCounter > 100) { break; };
                if (!bBuildHyperlinks) { continue; };
                //
                switch (kvp.Value.iHyperlinkType) {
                    case (int)eHyperlinkType.is_Hyperlink:
                        Mclipboard1MenuHyperlinks.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                        break;
                    case (int)eHyperlinkType.is_Folder:
                        Mclipboard1MenuFolderNFile.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                        break;
                    case (int)eHyperlinkType.is_UncFolder:
                        Mclipboard1MenuOther.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                        break;
                    case (int)eHyperlinkType.is_UncFile:
                        Mclipboard1MenuOther.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                        break;
                    case (int)eHyperlinkType.is_MailTo:
                        Mclipboard1MenuOther.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                        break;
                    case (int)eHyperlinkType.is_Document:
                        int iDocumentType = 1;
                        switch (iDocumentType) {
                            case (int)eDocumentType.is_WordDocument:
                                Mclipboard1MenuFolderNFile.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                                break;
                            case (int)eDocumentType.is_ExcelDocument:
                                Mclipboard1MenuFolderNFile.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                                break;
                            case (int)eDocumentType.is_OpenOfficeDocument:
                                Mclipboard1MenuFolderNFile.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                                break;
                            case (int)eDocumentType.is_TextDocument:
                                Mclipboard1MenuFolderNFile.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                                break;
                            case (int)eDocumentType.is_CodeDocument:
                                Mclipboard1MenuFolderNFile.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                                break;
                            case (int)eDocumentType.is_BatDocument:
                                Mclipboard1MenuFolderNFile.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                                break;
                            case (int)eDocumentType.is_ScriptDocument:
                                Mclipboard1MenuFolderNFile.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                                break;
                            default:
                                Mclipboard1MenuFolderNFile.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                                break;
                        }
                        // sProcessArgument = pstrLink;
                        break;
                    default:
                        Mclipboard1MenuFolderNFile.MenuItems.Add(kvp.Key, new EventHandler(Mclipboard1ItemHyperlink_Click));
                        break;
                }
            }

			Mclipboard1CntxtMenuTray.MenuItems.Add("-");
			Mclipboard1CntxtMenuTray.MenuItems.Add("Cancel Menu", new EventHandler(Mclipboard1ItemCancelMenu_Click));
			Mclipboard1CntxtMenuTray.MenuItems.Add("-");
			Mclipboard1CntxtMenuTray.MenuItems.Add(Mclipboard1ItemHide.Text, new EventHandler(Mclipboard1ItemHide_Click));
			Mclipboard1CntxtMenuTray.MenuItems.Add("-");
			Mclipboard1CntxtMenuTray.MenuItems.Add("E&xit", new EventHandler(Mclipboard1ItemExit_Click));
		}


		/// <summary>
		/// Called when an item is chosen from the menu
		/// </summary>
		/// <param name="pstrLink">The link that was clicked</param>
		private void OpenLink(string pstrLink)
		{
			try {
				// TODO needs more work to check for missing files etc

                mccHyperlink mcHyperlink;
                try {
                    if (dctHyperlink.TryGetValue(pstrLink, out mcHyperlink)) {
				        //
				        // Run the link
				        //
                        string sProcessName = "";
                        string sProcessArgument = "";
                        string sProcessFileName = "";
                        switch (mcHyperlink.iHyperlinkType) {
                            case (int) eHyperlinkType.is_Hyperlink:
                                sProcessName = "IExplore.exe";
                                sProcessArgument = pstrLink;
                                break;
                            case (int)eHyperlinkType.is_Folder:
                                sProcessName = "Explore.exe";
                                sProcessArgument = pstrLink;
                                break;
                            case (int) eHyperlinkType.is_UncFolder:
                                sProcessName = "Explore.exe";
                                sProcessArgument = pstrLink;
                                break;
                            case (int) eHyperlinkType.is_UncFile:
                                sProcessName = "Explore.exe";
                                sProcessFileName = pstrLink;
                                // sProcessArgument = pstrLink;
                                break;
                            case (int) eHyperlinkType.is_MailTo:
                                sProcessName = "Outlook.exe";
                                sProcessArgument = pstrLink;
                                break;
                            case (int) eHyperlinkType.is_Document:
                                sProcessName = "Word.exe";
                                sProcessArgument = pstrLink;
                                int iDocumentType = 1;
                                switch (iDocumentType) {
                                    case (int) eDocumentType.is_WordDocument:
                                        sProcessName = "Word.exe";
                                        break;
                                    case (int)eDocumentType.is_ExcelDocument:
                                        sProcessName = "Excel.exe";
                                        break;
                                    case (int)eDocumentType.is_OpenOfficeDocument:
                                        sProcessName = "soffice.exe";
                                        sProcessArgument = "-o -nologo -nodefault" + pstrLink;
                                        break;
                                    case (int)eDocumentType.is_TextDocument:
                                    // SciTE.exe
                                        sProcessName = "SciTE.exe";
                                        break;
                                    case (int)eDocumentType.is_CodeDocument:
                                        sProcessName = "SciTE.exe";
                                        break;
                                    case (int)eDocumentType.is_BatDocument:
                                        sProcessName = "SciTE.exe";
                                        break;
                                    case (int)eDocumentType.is_ScriptDocument:
                                        sProcessName = "SciTE.exe";
                                        break;
                                    default:
                                        sProcessName = "SciTE.exe";
                                        break;
                                }
                                // sProcessArgument = pstrLink;
                                break;
                            default:
                                sProcessName = "Explore.exe";
                                sProcessArgument = pstrLink;
                                break;
                        }

                        // ProcessStartInfo startInfo = new ProcessStartInfo(sProcessName);
                        mcHyperlink.mcStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                        // mcStartInfo.
                        if (sProcessArgument.Length > 0) { mcHyperlink.mcStartInfo.Arguments = sProcessArgument; }
                        if (sProcessFileName.Length > 0) { mcHyperlink.mcStartInfo.FileName = sProcessFileName; }
                        System.Diagnostics.Process.Start(mcHyperlink.mcStartInfo);
                        /*

      // Get the path that stores user documents.
      String^ myDocumentsPath = Environment::GetFolderPath( Environment::SpecialFolder::Personal );
      myProcess->StartInfo->FileName = String::Concat( myDocumentsPath, "\\MyFile.doc" );
      myProcess->StartInfo->Verb = "Print";
      myProcess->StartInfo->CreateNoWindow = true;
      myProcess->Start();

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
                    // dctHyperlink
                    // dctHyperlink[Mclipboard1ItemHL.Text];
                } catch (KeyNotFoundException) {
                    Console.WriteLine("Key = \"{1}\" was not found.", pstrLink);
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
		private void GetClipboardData()
		{
			//
			// Data on the clipboard uses the 
			// IDataObject interface
			//
			IDataObject iData = new DataObject();  
			string strText = "clipmon";

            UInt32 Temp = User32.GetClipboardSequenceNumber();

			try
			{
				iData = Clipboard.GetDataObject();
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
			if (iData.GetDataPresent(DataFormats.Rtf))
			{
				Mclipboard1ControlClipboardText.Rtf = (string)iData.GetData(DataFormats.Rtf);
						
				if(iData.GetDataPresent(DataFormats.Text))
				{
					strText = "RTF";
				}
			} else {
				// 
				// Get Text if it is present
				//
				if(iData.GetDataPresent(DataFormats.Text))
				{
					Mclipboard1ControlClipboardText.Text = (string)iData.GetData(DataFormats.Text);
							
					strText = "Text"; 

					Debug.WriteLine((string)iData.GetData(DataFormats.Text));
				} else {
					//
					// Only show RTF or TEXT
					//
					Mclipboard1ControlClipboardText.Text = "(cannot display this format)";
				}
			}

			// notAreaIcon.Tooltip = strText;
            notAreaIcon.Text = strText;
            if (!notAreaIcon.Visible) {
                notAreaIcon.Visible = true;
            }
            // notAreaIcon.Show();

			if( ClipboardSearch(iData) ) {
				//
				// Found some new links
				//
				System.Text.StringBuilder strBalloon = new System.Text.StringBuilder(100);

                foreach (KeyValuePair<string, mccHyperlink> kvp in dctHyperlink) {
					strBalloon.Append(kvp.Key  + "\n");
				}

				FormatMenuBuild(iData);
				SupportedMenuBuild(iData);					
				ContextMenuBuild(true);

				if (dctHyperlink.Count > 0) {

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


		protected override void WndProc(ref Message m)
		{
			switch ((Mclipboard1Messages)m.Msg)
			{
					//
					// The WM_DRAWCLIPBOARD message is sent to the first window 
					// in the clipboard viewer chain when the content of the 
					// clipboard changes. This enables a clipboard viewer 
					// window to display the new content of the clipboard. 
					//
				case Mclipboard1Messages.WM_DRAWCLIPBOARD:
					
					Debug.WriteLine("WindowProc DRAWCLIPBOARD: " + m.Msg, "WndProc");

					GetClipboardData();

					//
					// Each window that receives the WM_DRAWCLIPBOARD message 
					// must call the SendMessage function to pass the message 
					// on to the next window in the clipboard viewer chain.
					//

					//SendMessage(_ClipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    User32.SendMessage(_ClipboardViewerNext, m.Msg, m.WParam, m.LParam);
					break;


					//
					// The WM_CHANGECBCHAIN message is sent to the first window 
					// in the clipboard viewer chain when a window is being 
					// removed from the chain. 
					//
				case Mclipboard1Messages.WM_CHANGECBCHAIN:
					Debug.WriteLine("WM_CHANGECBCHAIN: lParam: " + m.LParam, "WndProc");

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
					if (m.WParam == _ClipboardViewerNext)
					{
						//
						// If wParam is the next clipboard viewer then it
						// is being removed so update pointer to the next
						// window in the clipboard chain
						//
						_ClipboardViewerNext = m.LParam;
					}
					else
					{
                        User32.SendMessage(_ClipboardViewerNext, m.Msg, m.WParam, m.LParam);
					}
					break;

				default:
					//
					// Let the form process the messages that we are
					// not interested in
					//
					base.WndProc(ref m);
					break;

			}

		}

		#endregion


		#region Event Handlers - Menu

		private void Mclipboard1ItemExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Mclipboard1ItemHide_Click(object sender, System.EventArgs e)
		{
            bShowWhenMinimized = (!bShowWhenMinimized);
            Mclipboard1ItemHide.Text = bShowWhenMinimized ? "Hide" : "Show";
            ContextMenuBuild(false);

            this.Visible = bShowWhenMinimized;
            if (this.Visible == true)
			{
				if (this.WindowState == FormWindowState.Minimized)
				{
					this.WindowState = FormWindowState.Normal;
				}
			}
		}

		private void Mclipboard1ItemHyperlink_Click(object sender, System.EventArgs e)
		{
			MenuItem Mclipboard1ItemHL = (MenuItem)sender;

            try {
                if (dctHyperlink.ContainsKey(Mclipboard1ItemHL.Text)) {
                    OpenLink(Mclipboard1ItemHL.Text);
                }
                // dctHyperlink
                // dctHyperlink[Mclipboard1ItemHL.Text];
            } catch (KeyNotFoundException) {
                Console.WriteLine("Key = \"{1}\" was not found.", Mclipboard1ItemHL.Text);
            }
		}

		private void Mclipboard1ItemCancelMenu_Click(object sender, System.EventArgs e)
		{
			// Do nothing - Cancel the menu
		}

		private void Mclipboard1FormMain_Resize(object sender, System.EventArgs e)
		{
            if (!bShowWhenMinimized) {
                if ((this.WindowState == FormWindowState.Minimized) && (this.Visible == true))
			    {
                    // hide when minimised
                    this.Visible = false;
                    Mclipboard1ItemHide.Text = "Show";
                    ContextMenuBuild(false);
                }
			}
		}

		#endregion


		#region Event Handlers - Internal

		private void Mclipboard1FormMain_Load(object sender, System.EventArgs e)
		{
			RegisterClipboardViewer();
		}

		private void Mclipboard1FormMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			UnregisterClipboardViewer();
		}

		private void notAreaIcon_BalloonClick(object sender, System.EventArgs e)
		{
			if(dctHyperlink.Count == 1)
			{
                // TODO Balloon Click Action
				// string strItem = (string)dctHyperlink.ToArray()[0];

				// Only one link so open it
				// OpenLink(strItem);
			}
			else
			{
				// notAreaIcon.ContextMenuDisplay();
                // notAreaIcon.Show();
                if (!notAreaIcon.Visible) {
                    notAreaIcon.Visible = true;
                }
            }
        }

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
            this.Mclipboard1MenuMain = new System.Windows.Forms.MainMenu(this.components);
            this.Mclipboard1MenuFormats = new System.Windows.Forms.MenuItem();
            this.Mclipboard1MenuSupported = new System.Windows.Forms.MenuItem();
            this.Mclipboard1MenuLinks = new System.Windows.Forms.MenuItem();
            this.Mclipboard1MenuFolderNFile = new System.Windows.Forms.MenuItem();
            this.Mclipboard1MenuHyperlinks = new System.Windows.Forms.MenuItem();
            this.Mclipboard1MenuOther = new System.Windows.Forms.MenuItem();
            this.notAreaIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.Mclipboard1CntxtMenuTray = new System.Windows.Forms.ContextMenu();
            this.Mclipboard1ItemSystray = new System.Windows.Forms.MenuItem();
            this.Mclipboard1ItemHyperlink = new System.Windows.Forms.MenuItem();
            this.Mclipboard1ItemSep1 = new System.Windows.Forms.MenuItem();
            this.Mclipboard1ItemHide = new System.Windows.Forms.MenuItem();
            this.Mclipboard1ItemSep2 = new System.Windows.Forms.MenuItem();
            this.Mclipboard1ItemExit = new System.Windows.Forms.MenuItem();
            this.notAreaIconCntxtMenuTray = new System.Windows.Forms.ContextMenu();
            this.Mclipboard1ControlClipboardText = new System.Windows.Forms.RichTextBox();
            #endregion
            #region Form Menu Definition
            this.SuspendLayout();
            // 
            // Mclipboard1MenuMain
            // 
            this.Mclipboard1MenuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Mclipboard1MenuFormats,
            this.Mclipboard1MenuSupported,
            this.Mclipboard1MenuLinks});
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
            // Mclipboard1MenuLinks
            // 
            this.Mclipboard1MenuLinks.Index = 2;
            this.Mclipboard1MenuLinks.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Mclipboard1MenuFolderNFile,
            this.Mclipboard1MenuHyperlinks,
            this.Mclipboard1MenuOther});
            this.Mclipboard1MenuLinks.Text = "Links";
            // 
            // Mclipboard1MenuFolderNFile
            // 
            this.Mclipboard1MenuFolderNFile.Index = 0;
            this.Mclipboard1MenuFolderNFile.Text = "FoldersAndFiles";
            // 
            // Mclipboard1MenuHyperlinks
            // 
            this.Mclipboard1MenuHyperlinks.Index = 1;
            this.Mclipboard1MenuHyperlinks.Text = "Hyperlinks";
            // 
            // Mclipboard1MenuOther
            // 
            this.Mclipboard1MenuOther.Index = 2;
            this.Mclipboard1MenuOther.Text = "Other Links";
            // 
            // notAreaIcon
            // 
            this.notAreaIcon.ContextMenu = this.Mclipboard1CntxtMenuTray;
            this.notAreaIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notAreaIcon.Icon")));
            this.notAreaIcon.Text = "Mdm ClipBoard";
            this.notAreaIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notAreaIcon_MouseClick);
            this.notAreaIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notAreaIcon_MouseDoubleClick);
            // 
            // Mclipboard1CntxtMenuTray
            // 
            this.Mclipboard1CntxtMenuTray.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.Mclipboard1ItemSystray,
            this.Mclipboard1ItemHyperlink,
            this.Mclipboard1ItemSep1,
            this.Mclipboard1ItemHide,
            this.Mclipboard1ItemSep2,
            this.Mclipboard1ItemExit});
            // 
            // Mclipboard1ItemSystray
            // 
            this.Mclipboard1ItemSystray.Index = 0;
            this.Mclipboard1ItemSystray.Text = "C:\\Temp\\SysTray";
            this.Mclipboard1ItemSystray.Click += new System.EventHandler(this.Mclipboard1ItemHyperlink_Click);
            // 
            // Mclipboard1ItemHyperlink
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
            #endregion
            #region Form Component Definition
            // 
            // Mclipboard1ControlClipboardText
            // 
            this.Mclipboard1ControlClipboardText.DetectUrls = false;
            this.Mclipboard1ControlClipboardText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Mclipboard1ControlClipboardText.Location = new System.Drawing.Point(0, 0);
            this.Mclipboard1ControlClipboardText.Name = "Mclipboard1ControlClipboardText";
            this.Mclipboard1ControlClipboardText.ReadOnly = true;
            this.Mclipboard1ControlClipboardText.Size = new System.Drawing.Size(354, 315);
            this.Mclipboard1ControlClipboardText.TabIndex = 0;
            this.Mclipboard1ControlClipboardText.Text = "";
            this.Mclipboard1ControlClipboardText.WordWrap = false;
            this.Mclipboard1ControlClipboardText.TextChanged += new System.EventHandler(this.Mclipboard1ControlClipboardText_TextChanged);
            // 
            // ClipFormMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(354, 315);
            this.Controls.Add(this.Mclipboard1ControlClipboardText);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(100, 100);
            this.Menu = this.Mclipboard1MenuMain;
            this.Name = "ClipFormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Clipboard Manager";
            this.Load += new System.EventHandler(this.Mclipboard1FormMain_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Mclipboard1FormMain_Closing);
            this.Resize += new System.EventHandler(this.Mclipboard1FormMain_Resize);
            this.ResumeLayout(false);
            #endregion
            #endregion

            // This array will be used to keep track of changes to the clipboard data
            ProgControl = new ProgControlDef();
            ClipMeta = new ClipMetaDef();
            ClipHist = new List<ClipMetaDef>();

		}
		#endregion

        private void Mclipboard1ControlClipboardText_TextChanged(object sender, EventArgs e) {

        }

        private void notAreaIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
            Mclipboard1ItemHide_Click(sender, e);
        }

        private void notAreaIcon_MouseClick(object sender, MouseEventArgs e) {
            Mclipboard1ItemHide_Click(sender, e);
        }


	}
}
