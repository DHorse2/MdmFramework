//Top//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
// using System.Drawing;
using System.IO;
using System.Linq;
// using System.Runtime.InteropServices;
using System.Text;
// using System.Web;
// using System.Web.UI;
// using System.Web.UI.HtmlTextWriter;
// using System.Web.Util;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
// using System.Windows.Forms;
using System.Windows.Input;
// using System.Windows.Media;
// using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Mdm.Oss.Decl;

using Mdm.Oss.ClipboardUtil;
using Mdm.Oss.CodeUtil;
using Mdm.Oss.FileUtil;
using Mdm.Oss.Mapp;
using Mdm.Oss.Mobj;
using Mdm.Oss.Mvc;
using Mdm.Oss.UrlUtil.Hist;
using Mdm.Oss.ShellUtil;
using Mdm.Oss.ShellUtil.BaseUtil;
using Mdm.Oss.ShellUtil.FileUtil;
using Mdm.Oss.Support;
using Mdm.Oss.UrlUtil;
//  using Mdm1Oss1MinputTld1Thread;
// using    Mdm1Oss1FileCreation1;

namespace Mdm.Oss.Mvc {


    public class MpageControl : DefStdBaseRunFileConsole {
        int i = 1;
    }

    public class MpageDetail : PageFunction<String> {
        int i = 1;
    }

    public class MpageMain : Page {
        int i = 1;
        MpageControl pC = new MpageControl();
        #region Clipboard Ui Helper
        // [GuidAttribute("DF29D855-D0EC-4DA1-BCC3-42FA3A09B1CB")]
        // [ComVisibleAttribute(false)]
        //public interface SVsUIHierWinClipboardHelper
        #endregion
        #region Common Declarations and Delegates
        // Mdm1 Srt1 ImportTld1 OpSys Bootstrap
        // Mdm1Srt1ImportTld1 - MinputTld Class
        //
        // delegates and event callbacks
        /*
        public delegate void oTextBoxChangeDelegate(object sender, string s);
        public delegate void oTextBoxAddDelegate(object sender, string s);
        public delegate void oProgressCompletionDelegate(object sender, string sField, int iAmount, int iMax);

        public event oTextBoxChangeDelegate InputFileLineChange;
        public event oTextBoxChangeDelegate OutputFileLineChange;

        public event oProgressCompletionDelegate oMdmMessageProcessChanged;

        public event oTextBoxChangeDelegate MdmMessageProcessChanged;
        public event oTextBoxChangeDelegate MdmMessageProcessText2_TextChanged;
        public event oTextBoxChangeDelegate MdmMessageProcessText3_TextChanged;
        public event oTextBoxChangeDelegate MdmMessageProcessText4_TextChanged;

        public event oTextBoxAddDelegate MdmMessageProcessText1TextAdd;
        public event oTextBoxAddDelegate MdmMessageProcessText2TextAdd;
        public event oTextBoxAddDelegate MdmMessageProcessText3TextAdd;
        public event oTextBoxAddDelegate MdmMessageProcessText4TextAdd;
        // public delegate void worker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs eaProcessChangeEventArgs);
        */
        delegate void UpdatePageUiHandler(RoutedEventHandler erehPassedRoutedEventHandler);
        delegate void UpdatePageUiProgressHandler(object oPassedSender, ProgressChangedEventArgs ePassedPcea);
        delegate void UpdatePageUiMessageHandler(object oPassedSender, String sPassedMessage);
        delegate void UpdatePageUiEvent(RoutedEventArgs ereaPassedRoutedEventArgs);

        // public event EventHandler<MdmCommandEventArgs> MessageSentEvent;

        public delegate void MessageSendEventHandler(object sender, string sPassedMessage);

        public event MessageSendEventHandler MessageToPage;
        #endregion
        #region StandardObjects
        #region PackageObjectDeclarations (strongly)TYPED
        // <Area Id = "Mapplication">
        // <Area Id = "omAplication">
        public Application omAp;
        // <Area Id = "omHControl">
        public Mcontroller omCo;
        // <Area Id = "omO">
        internal Mobject omOb;
        // <Area Id = "MdmLocalVerb">
        // <Area Id = "MdmLocalVerb">
        // internal object MinputTldThread;
        public object omWt;
        // <Area Id = "omAplication">
        public Mapplication omMa;
        // <Area Id = "omW">
        // internal object MinputTld;
        public object omVe;
        // <Area Id = "omcLocalClipboard">
        internal Mclipboard1Form1 omCl;
        // <Area Id = "omU">
        internal MurlHist1Form1 omUr;


        #endregion
        // #endregion
        #region PageEvents
        // public event EventHandler<CloseEventArgs> CancelClose;
        #endregion
        #region Page Declartions
        // <Area Id = "omP">
        internal MpageMain omPa;
        double dConsoleDisplayCount = 0;
        double dConcoleDisplayAdjustCount = 0;
        double dConcoleDisplayAdjustCountMax = 15;
        double dConsoleWidthHigh = 0;
        double dConsoleWidthLow = 0;
        // <Area Id = "omP2">
        internal MpageDetail omPa2;
        #endregion
        // Class Internals - Properties Fields and Attributes
        #region PageMain Internal Field Declarations
        internal string ImportFileNameCurrent;
        internal Mfile ofIfo;
        internal int iImportFileNameCurrentNotValid = 99999;

        //xxxxxxxxxxxxxxxx
        internal string OutputSystemNameCurrent = "";
        internal object OutputSystemObject = null;

        internal string OutputDatabaseNameCurrent = "";
        internal SqlConnection OutputDatabaseObject = null;

        //xxxxxxxxxxxxxxxx
        internal string OutputFileNameCurrent = "99999";
        internal string OutputFileNameLast = "99999";
        internal Mfile OutputFileObject;

        internal int iOutputFileNameCurrentNotValid = 99999;

        internal string OutputFileItemIdCurrent = "99999";
        internal int iOutputFileItemIdCurrentNotValid = 99999;

        internal string OutputFileOptionsCurrent = "";

        internal bool OptionToDoOverwriteExistingItemCurrent;
        internal bool OptionToDoCheckItemIdsCurrent;
        internal bool OptionToDoCheckFileDoesExistCurrent;
        internal bool OptionToDoEnterEachItemIdCurrent;
        internal bool OptionToDoLogActivityCurrent;
        internal bool OptionToDoProceedAutomaticallyCurrent;
        internal bool OptionToDoCreateMissingFileCurrent;

        internal string sPage2ReturnValue;
        internal System.Uri uTld1Page2Uri = new System.Uri("/Mdm1Srt1ImportTld1;component/page2.xaml", System.UriKind.Relative);

        #endregion
        #endregion
        #region PageControl
        // #region ClassConstants
        #region MdmFileDatabaseControlConstants
        // <Section Summary>
        // <Section Role="Declarations">
        // <Section Id = "MdmFileDatabaseControlConstants">
        // <Section Vs="MdmStdRunVs0_9_0">
        // <Section Id = "FileIOConstants">
        // <Area Id = "FileSchemaLevel"
        const int FileDictData = 1;
        const int FileData = 2;
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // 
        // <Area Id = "FileOpenConstants">
        // protected int FileIsOpenStatus = 0;
        const int FileIoOpenOK = 0;
        const int FileIoOpenTryFIRST = -3;
        const int FileIoOpenTryAGAIN = -2;
        const int FileIoOpenTryENTERED = 1;
        const int FileIoOpenTryDEFAULT = 2;
        const int FileIoOpenTryORIGINAL = 3;
        const int FileIoOpenTryALL = 3;
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // TODO OBJECT
        const int OfObjectOK = 0;
        const int OfObjectFILEDATA = 1000;
        const int OfObjectFILEDICT = 2000;
        const int OfObjectDATABASE = 3000;
        const int OfObjectSERVICE = 4000;
        const int OfObjectSERVER = 5000;
        const int OfObjectSYSTEM = 6000;
        const int OfObjectNETWORK = 7000;
        const int OfObjectSECURITY = 8000;
        const int OfObjectUSER = 9000;
        const int OfObjectUndefined = 90000;
        // TODO ACTION 
        const int OfActionOK = 0;
        const int OfActionOPEN = 100;
        const int OfActionCLOSE = 200;
        const int OfActionCREATE = 300;
        const int OfActionDELETE = 400;
        const int OfActionREAD = 500;
        const int OfActionWRITE = 600;
        const int OfActionCONNECT = 700;
        const int OfActionUndefined = 90000;
        // TODO RESULT
        const int OfResultOK = 0;
        const int OfResultStarted = 1;
        const int OfResultFailed = 2;
        const int OfResultAtEnd = 3;
        const int OfResultCancelled = 4;
        const int OfResultTimedOut = 5;
        const int OfResultUnknownFailure = 6;
        const int OfResultOsError = 7;
        const int OfResultDatabaseError = 8;
        const int OfResultOperation_IinProgress = 41;
        const int OfResultMissing_Name = 21;
        const int OfResultUndefined = 90000;
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // <Area Id = "FileExistanceConstants">
        const int OfResultShouldNotExist = 53;
        const int OfResultShouldExist = 54;
        const int OfResultDoesExist = 52;
        const int OfResultDoesNotExist = 51;
        const int DBOfResultDoesNotExist = 55;
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // <Area Id = "FileReadModeConstants">
        // protected int ipFileReadMode = 0;
        const int ReadModeNotSet = 0;
        const int ReadModeBLOCK = 1;
        const int ReadModeLINE = 2;
        const int ReadModeALL = 3;
        const int ReadModeSQL = 4;
        const int ReadModeERROR = 5;
        const int ReadModeUndefined = -99999;
        // additional access modes
        const int ReadModeBINARY = 25;
        const int ReadModeSEEK = 28;
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // DIST AND NETWORK
        const int FileIoDiskFULL = 8001;
        const int FileIoDiskERROR = 8002;
        const int FileIoNetworkERROR = 8003;
        const int FileIoInternetERROR = 8004;
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // <Area Id = "AsciiOpenOptions">
        // protected int AsciiOpenOptions = 0;
        // <Area Id = "FileAccess">
        const int FileIoAccessNotSet = 0;
        const int FileIoAccessREAD_ONLY = 21;
        const int FileIoAccessAPPEND_ONLY = 22;
        const int FileIoAccessERROR = 5;
        // <Area Id = "FileCreation">
        const int FileIoCreateIfMISSING = 23;
        const int FileIoCreateONLY = 24;
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // FileNull
        // FileField
        // FileTable
        // FileTableRow
        // FileTableColumn
        // <Area Id = "FileNullErrorsConstants">
        // <Area Id = "FileFileIdErrorsConstants">
        // <Area Id = "FileTableErrorsConstants">
        // <Area Id = "FileTableRowErrorsConstants">
        const int FileIoNoRowID = 99;
        const int FileIoRowIdShouldNotExist = 33;
        const int FileIoRowIdDoesExist = 32;
        const int FileIoRowIdDoesNotExist = 31;
        // <Area Id = "FileTableColumnErrorsConstants">
        #endregion
        // #endregion
        // #region ClassControl
        #region ClaseInternalResults
        internal int AppCoreObjectCreateResult;
        internal int AppCoreObjectGetFromApppResult;
        internal int AppCoreObjectGetResult;
        internal int AppCoreObjectSetResult;
        internal int AppObjectGetResult;
        internal int AppObjectSetResult;
        internal int AppPageObjectGetResult;
        internal int AppPageObjectSetResult;
        internal int ApplicationMobjectObjectGetResult;
        internal int ApplicationMobjectObjectSetResult;
        //
        // Page
        internal int iOnResizeResult;
        internal int Page1Result;
        internal int Page1ResultLoadedResult;
        internal int iMdmPage1_Size_ChangedResult;
        internal int iMdmPage2CreationResult;
        internal int iMdmPage2NavigateResult;
        //
        // File Action
        public int AppActionDoResult;
        public int AppStartResult;
        public int AppPauseResult;
        public int AppCancelResult;
        //
        public int AppActionWaitCounterResult = 0;
        //
        public bool bImportProgressBarColoredResult = false;
        public bool bImportProgressBarColorChangedResult = true;
        //
        #endregion
        #region ClassInternalStatusMessageDeclarations
        // TextBox StatusLine1 = new TextBox();
        // TextBox StatusLine2 = new TextBox();
        private string spMdmMessageProcessText1 = "unknown";
        public string MdmMessageProcessText1 { get { return spMdmMessageProcessText1; } set { spMdmMessageProcessText1 = value; } }
        private string spMdmMessageProcessText2 = "unknown";
        public string MdmMessageProcessText2 { get { return spMdmMessageProcessText2; } set { spMdmMessageProcessText2 = value; } }
        private string spMdmMessageProcessText3 = "unknown";
        public string MdmMessageProcessText3 { get { return spMdmMessageProcessText3; } set { spMdmMessageProcessText3 = value; } }
        private string spMdmMessageProcessText4 = "unknown";
        public string MdmMessageProcessText4 { get { return spMdmMessageProcessText4; } set { spMdmMessageProcessText4 = value; } }

        public BackgroundWorker worker;


        #endregion
        // #endregion
        #region MdmClassTemporaryDeclarations
        // <Section Summary>
        // <Section Role="Declarations">
        // <Section Id = "MdmClassTemporary">
        // <Section Vs="MdmTempVarVs0_8_4">
        // Data
        protected string sTempDataImport = "";
        protected string sTempData = "";
        // Counters
        protected int iTempCounter = 0;
        private int iCharCounter = 0;
        // Integers
        protected int iTemp = 0;
        protected int iTemp0 = 0;
        protected int iTemp1 = 0;
        protected int iTemp2 = 0;
        protected int iTemp3 = 0;
        // Strings
        protected string sTemp = "";
        protected string sTemp0 = "";
        protected string sTemp1 = "";
        protected string sTemp2 = "";
        protected string sTemp3 = "";
        private string sCurrentString = "";
        // Printer / Progess Display Control
        protected int PickPrinterRouting = 14;
        const int PICK_PRINTErON = 11;
        const int PICK_PRINT_TO_FILE = 12;
        const int PICK_PRINT_TO_DISPLAY = 13;
        const int PICK_PRINT_TO_Console = 14;
        // Head, Footer, Status Line, TextBox
        // Display Output
        protected bool bPickDisplayNewLine = false;
        protected int PickDisplayLineNumber = 0;
        protected int PickDisplayColumn = 0;
        protected int PickDisplayRow = 0;
        // Printer Output
        protected bool bPickPrinterNewLine = false;
        protected int PickPrinterColumnCounter = 0;
        protected int PickPrinterRowCounter = 0;
        protected int PickPrinterColumn = 0;
        protected int PickPrinterRow = 0;
        //
        // Console Output
        protected bool bConsolePickConsoleNewLine = false;
        protected int ConsolePickConsoleColumnCounter = 0;
        protected int ConsolePickConsoleRowCounter = 0;
        // Input
        protected bool bPromptNewLine = false;
        protected string sPromptChar = @"?";
        protected string sPromptText = @"";
        protected int iPromptRow = 23;
        protected int iPromptColumn = 1;
        protected string sPromptResponse = @"";
        protected string sPromptDefaultResponse = @"";
        //
        // System Standard Functions Character Constants.
        // Ascii Delimiters
        protected string cbCrLf = ((char)13).ToString() + ((char)10).ToString();
        string cbCr = ((char)13).ToString();
        string cbLf = ((char)10).ToString();
        string cbEof = ((char)26).ToString();
        // string cbEot =  ((char)26).ToString();
        // Pick Delimeters
        // string cbSm =  ((char)255).ToString();
        string cbAm = ((char)254).ToString();
        string cbVm = ((char)253).ToString();
        string cbSvm = ((char)252).ToString();
        string cbLvm = "*";
        string cbLsvm = "@";
        // Special Ascii Characters
        string cbsTld = "~";
        string cbAsterisk = "*";
        string cbStick = "|";
        string cbBackSlash = @"\";
        string cbForwardSlash = @"/";
        string cbAtSymbol = @"@";
        // White Space Characters
        string cbFf = ((char)12).ToString();
        string cbBs = ((char)08).ToString();
        string cbSp = ((char)20).ToString();
        // cbTab ; Horizontal Tab
        // cbVtab ; Vertical Tab
        // Null
        string cbNull = ((char)00).ToString();
        // Status Verbose Constants
        // System Call Function Constants
        protected int PickSystemCommand = -1;
        const int SYSTEM_COMMAND_LINE = 0;
        const int SYSTEM_SLEEP = 11;
        const int SYSTEM_TYPEAHEAD_CHARACTERS = 14;
        // </Section Summary>
        #endregion
        #region External Command Declarations
        // public event EventHandler<MdmCommandEventArgs> SampleEvent;
        #endregion
        #endregion
        #region Standard Page Functions Load, Size etc
        // PageMain  - xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Page1Load
        private void Page1_Loaded(object sender, RoutedEventArgs e) {
            Page1ResultLoadedResult = MethodStart;
            StatusLine1.Text += "Loading please wait... ";
            //
            MdmConsoleControl_Set();
            //
            // Initialize Page 1
            if (omPa == null) {
                omPa = this;
                omPa = this;
                omMa.AppPageObjectSet(omPa);
                omCo.AppPageObject(omPa);
                // omCo.AppPage1_SetDefaults((PageMain)this);
                // Application Started
                if (!bLocalStarted) {
                    // Set Focus to First Field
                    MdmControl_SetFocus(ImportFileLine);
                    bLocalStarted = true;
                }
            }
            // LocalLongResult = omCo.AppInitialize();
            // Page2Instance
            if (omPa2 == null) {
                // Initialize PageDetail
                MdmPage2Creation(omOb);
            }

            // Page1ResultLoaded
            // TODO Mdm.Srt.ImportTld.PageMain.MdmCommandDoLocalEventsMain();
        }
        private void MdmPage1_Size_Changed(object sender, RoutedEventArgs e) {
            MdmPageSize_ChangedDoAdjust((Page)sender, 0, 0);
        }
        public void MdmPageSize_ChangedDoAdjust(Page sender, double dPassedDesiredWidth, double dPassedDesiredHeight) {
            iMdmPage1_Size_Changed = MethodStart;
            // StatusLine1.Text += "Adjusting screen, please wait... ";
            // TODO Window Height Width and Focus
            int iTemp0 = 0;
            bool bInvalidateVisual = false;
            double dBase_Width = base.Width;
            double dWindowWidth = 0;
            double dGridMainWidth = 0;
            string sTemp0 = "";
            //
            string sParent_ActualWidth;
            double dParent_ActualWidth;
            string sParent_ActualHeight;
            double dParent_ActualHeight;
            try {
                sParent_ActualWidth = (sender.Parent.GetValue((Window.ActualWidthProperty)).ToString());
                dParent_ActualWidth = Convert.ToDouble(sParent_ActualWidth);
                sParent_ActualHeight = (sender.Parent.GetValue((Window.ActualHeightProperty)).ToString());
                dParent_ActualHeight = Convert.ToDouble(sParent_ActualHeight);
            } catch {
                dParent_ActualWidth = sender.ActualWidth;
                dParent_ActualHeight = sender.ActualHeight;
            }

            string sWindow_ActualWidth;
            double dWindow_ActualWidth;
            string sWindow_ActualHeight;
            double dWindow_ActualHeight;
            try {
                sWindow_ActualWidth = (sender.GetValue((Window.ActualWidthProperty)).ToString());
                dWindow_ActualWidth = Convert.ToDouble(sWindow_ActualWidth);
                sWindow_ActualHeight = (sender.GetValue((Window.ActualHeightProperty)).ToString());
                dWindow_ActualHeight = Convert.ToDouble(sWindow_ActualHeight);
            } catch {
                dWindow_ActualHeight = sender.ActualHeight;
                dWindow_ActualWidth = sender.ActualWidth;
            }

            string sFrame_ActualWidth;
            double dFrame_ActualWidth;
            string sFrame_ActualHeight;
            double dFrame_ActualHeight;
            try {
                sFrame_ActualWidth = (sender.GetValue((Frame.ActualWidthProperty)).ToString());
                dFrame_ActualWidth = Convert.ToDouble(sFrame_ActualWidth);
                sFrame_ActualHeight = (sender.GetValue((Frame.ActualHeightProperty)).ToString());
                dFrame_ActualHeight = Convert.ToDouble(sFrame_ActualHeight);
            } catch {
                dFrame_ActualHeight = dWindow_ActualHeight;
                dFrame_ActualWidth = dWindow_ActualWidth;
            }


            double dGrid_ActualWidth = 0;
            double dGrid_ActualHeight = 0;
            try {
                if (omPa != null) {
                    dGrid_ActualWidth = omPa.PageGridMain.ActualWidth;
                    dGrid_ActualHeight = omPa.PageGridMain.ActualHeight;
                } else {
                    dGrid_ActualWidth = MinputTldApp.Current.MainWindow.ActualWidth;
                    dGrid_ActualHeight = MinputTldApp.Current.MainWindow.ActualHeight;
                }
            } catch {
                dGrid_ActualWidth = sender.ActualWidth;
                dGrid_ActualHeight = sender.ActualHeight;
            }

            // load width and height
            double dDesiredWidth = dPassedDesiredWidth;
            double dDesiredHeight = dPassedDesiredHeight;

            if (dDesiredWidth == 0) { dDesiredWidth = dParent_ActualWidth; }
            if (dDesiredHeight == 0) { dDesiredHeight = dParent_ActualHeight; }

            if (dDesiredHeight > 0 && dDesiredHeight > sender.MaxHeight) { dDesiredHeight = sender.MaxHeight; }
            if (dDesiredWidth > 0 && dDesiredWidth > sender.MaxWidth) { dDesiredWidth = sender.MaxWidth; }
            if (dDesiredHeight > 0 && dDesiredHeight < sender.MinHeight) { dDesiredHeight = sender.MinHeight; }
            if (dDesiredWidth > 0 && dDesiredWidth < sender.MinWidth) { dDesiredWidth = sender.MinWidth; }

            // adjust grid
            if (true == true) {
                dGrid_ActualWidth = dDesiredWidth;
                dGrid_ActualHeight = dDesiredHeight;
                return;
            }
            //
            if (sender.ActualHeight > 0 && sender.ActualHeight > sender.MaxHeight) {
                sender.Height = sender.MaxHeight;
                bInvalidateVisual = true;
            }
            if (sender.ActualWidth > 0 && sender.ActualWidth > sender.MaxWidth) {
                sender.Width = sender.MaxWidth;
                bInvalidateVisual = true;
            }
            if (sender.ActualHeight > 0 && sender.ActualHeight < sender.MinHeight) {
                sender.Height = sender.MinHeight;
                bInvalidateVisual = true;
            }
            if (sender.ActualWidth > 0 && sender.ActualWidth < sender.MinWidth) {
                sender.Width = sender.MinWidth;
                bInvalidateVisual = true;
            }
            //
            // TODO FrameworkElement.SizeChangedEvent();
            if (bInvalidateVisual) {
                // UpdatePageUiHandler
                // UpdatePageUiEvent
                RoutedEventArgs re = new RoutedEventArgs();
                re.Source = this;
                re.RoutedEvent = Page.SizeChangedEvent;
                sender.RaiseEvent(new RoutedEventArgs());
                re.Source = this;
                re.Handled = false;
                re.RoutedEvent = Page.RequestBringIntoViewEvent;
                sender.RaiseEvent(new RoutedEventArgs());
                sender.InvalidateVisual();
            }
        }
        //
        private void MdmPage1_Grid_Changed(object sender, RoutedEventArgs e) {
            MdmPage1_Grid_Size_ChangedDoAdjust((Page)sender, 0, 0);
        }
        public void MdmPage1_Grid_Size_ChangedDoAdjust(Page sender, double dPassedDesiredWidth, double dPassedDesiredHeight) {
            iMdmPage1_Size_Changed = MethodStart;
            // PageGridMain.
            // omPa.TldOptionRows
            //
            double dColWidth_0 = gcCol0.ActualWidth;
            double dColWidth_1 = gcCol1.ActualWidth;
            double dColWidth_2 = gcCol2.ActualWidth;
            double dColWidth_3 = gcCol3.ActualWidth;
            double dColWidth_4 = gcCol4.ActualWidth;
            double dColWidth_5 = gcCol5.ActualWidth;
        }
        #endregion
        // PageDetail - xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Page2Load
        // Initalize PageDetail
        void MdmPage2Creation(Mobject omPassedO) {
            iMdmPage2Creation = MethodStart;
            // Page2Function_Creation();
            if (omPa2 == null) {
                // Initialize PageDetail
                string tmpstr = "Initial Data Item Value";
                omPa2 = new PageDetail(omOb, tmpstr);
                omMa.AppPage2ObjectSet(omPa2);
                iMdmPage2Creation = omMa.AppCoreObjectGet(omOb);
                // omCo.AppPageObject((PageMain) omPa, (PageDetail) omPa2);
                omCo.AppPageObject(omPa, omPa2);
            }
            // Set PageDetail Fields
            omCo.AppPage2SetDefaults(omPa, omPa2);
            sTemp = "Do you exist?";
            MdmMessageProcessText1TextAdd(this, "\n" + "Check if the file exists: " + omPa2.DoesExist(sTemp) + "\n");
        }
        void MdmPage2Navigate() {
            iMdmPage2Navigate = MethodStart;
            if (omPa2 == null) { MdmPage2Creation(omOb); }
            omPa2.Height = Height;
            omPa2.Width = Width;
            // omCo.LocalLongResult = omCo.AppPage2SetDefaults(omPa, omPa2);
            NavigationService.Navigate(omPa2, System.UriKind.Relative);
            // iMdmPage2Navigate
        }
        #endregion
        // Focus Control - xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Focus Control
        public void MdmControlCheckColor() {
            if (bImportProgressBarColorChanged) {
                if (omCo.bRunPausePending
                    || omCo.iaRunActionState[cRunPause, cRunState] != cRunTense_Off) {
                    ProgressBar1.Foreground = System.Windows.Media.Brushes.Yellow;
                    ProgressBar1.Background = System.Windows.Media.Brushes.LightGreen;
                } else if (omCo.bRunCancelPending
                    || omCo.iaRunActionState[cRunCancel, cRunState] == cRunTense_Do
                    || omCo.iaRunActionState[cRunCancel, cRunState] == cRunTense_Doing
                    || omCo.iaRunActionState[cRunCancel, cRunState] == cRunTense_Did) {
                    ProgressBar1.Foreground = System.Windows.Media.Brushes.Red;
                    ProgressBar1.Background = System.Windows.Media.Brushes.Orange;
                } else if (omCo.bRunStartPending
                    || omCo.iaRunActionState[cRunRunDo, cRunState] == cRunTense_Do
                    || omCo.iaRunActionState[cRunRunDo, cRunState] == cRunTense_Doing) {
                    ProgressBar1.Background = System.Windows.Media.Brushes.LightGray;
                    if (omCo.bRunErrorDidOccur) {
                        ProgressBar1.Foreground = System.Windows.Media.Brushes.LightYellow;
                    } else {
                        ProgressBar1.Foreground = System.Windows.Media.Brushes.Green;
                    }
                } else if (omCo.iaRunActionState[cRunRunDo, cRunState] == cRunTense_Did) {
                    ProgressBar1.Foreground = System.Windows.Media.Brushes.Blue;
                    ProgressBar1.Background = System.Windows.Media.Brushes.LightGray;
                } else {
                    ProgressBar1.Foreground = System.Windows.Media.Brushes.Green;
                    // ProgressBar1.Foreground = System.Windows.Media.Brushes.Blue;
                    ProgressBar1.Background = System.Windows.Media.Brushes.LightGray;
                }
                //
                // ProgressBar1.Value = 0;
                // this.BringIntoView();
                ProgressBar1.InvalidateVisual();
                bImportProgressBarColorChangedResult = false;
            }
        }
        public void MdmControl_SetFocus(System.Windows.Controls.Control control) {
            // Set focus to the control, if it can receive focus.
            if (control.Focusable) { control.Focus(); }
            this.BringIntoView();
        }
        private void ImportFileLine_GotFocus(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
            StatusLine1.ToolTip = "Enter the file that will be processed.  Normal a text file with optional TLD format row / records names (ipPickDictItemGet.eTcea. ~me~) when there are multiple records in the file.";
            StatusLine1.Text += "Enter the file you want to import." + "\n";
            ImportFileLine.Text += @"C:\Rec\ACTDICT.TXT";
        }
        private void ImportFileLine_LostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void OutputFileLine_GotFocus(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
            StatusLine1.Text += "Enter the file name to place the data into." + "\n";
            // OutputFileLine.Text = @"C:\Rec\daveACTDICT.TXT";
        }
        private void OutputFileLine_LostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
            /*
            if (OutputFileNameLast != OutputFileLine.Text && omPa2 != null) {
                omCo.AppPage2SetDefaults((PageMain)omPa, (PageDetail)omPa2);
            }
            */
            OutputFileNameLast = OutputFileLine.Text;
        }
        private void OutputFileItemId_GotFocus(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
            StatusLine1.Text += "Enter the record key if this is a single row or document." + "\n";
        }
        private void OutputFileItemId_LostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void OptionToDoOverwriteExistingItemGotFocus(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
            StatusLine1.Text += "Check this if you want to overwrite items that exist in the output file." + "\n";
        }
        private void OptionToDoOverwriteExistingItemLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void OptionToDoCheckFileDoesExist_GotFocus(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
            StatusLine1.Text += "Check this if the output file must already exist." + "\n";
            // can't have create and exists checked at the same time
        }
        private void OptionToDoCheckFileDoesExist_LostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
            // can't have create and exists checked at the same time
        }
        private void OptionToDoCheckItemIds_GotFocus(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
            StatusLine1.Text += "Check this if you want to individually Confirm each Item Id in the Output File already DoesExist." + "\n";
        }
        private void OptionToDoCheckItemIds_LostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void OptionToDoEnterEachItemId_GotFocus(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
            StatusLine1.Text += "Check this if you want to enter each Item Id or Row Key individually." + "\n";
        }
        private void OptionToDoEnterEachItemId_LostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void OptionToDoLogActivity_GotFocus(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
            StatusLine1.Text += "Check this if you want to have this run logged." + "\n";
        }
        private void OptionToDoLogActivity_LostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void OptionToDoProceedAutomatically_GotFocus(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
            StatusLine1.Text += "Option to run on automatic without prompting you for further responses." + "\n";
        }

        private void OptionToDoProceedAutomatically_LostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }

        private void OptionToDoCreateMissingFileGotFocus(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
            StatusLine1.Text += "Option to create the output file if it is missing." + "\n";
            // can't have create and exists checked at the same time
        }

        private void OptionToDoCreateMissingFileLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
            // can't have create and exists checked at the same time
        }
        private void DatabasePageButton_GotFocus(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
            StatusLine1.Text += "Click to change output database settings." + "\n";
        }

        private void DatabasePageButton_LostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void AppStartButtonPressed_GotFocus(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
            StatusLine1.Text += "Click to start processing." + "\n";
        }

        private void AppStartButtonPressed_LostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void AppPauseButtonPressed_GotFocus(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
            StatusLine1.Text += "Click to pause processing." + "\n";
        }

        private void AppPauseButtonPressed_LostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void AppCancel_ButtonPressed_GotFocus(object sender, RoutedEventArgs e) {
            StatusLine1.Text += "Click to cancel processing and exit." + "\n";
        }
        private void AppCancel_ButtonPressed_LostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        #endregion
        #endregion
        #region $include Mdm1Srt1ImportTld1 PageMain MdmClass Standard Root Word Constants
        // ON = YES = OK = true
        // OFF = NO = BAD = false
        const bool bON = true;
        const bool bOFF = false;
        //
        const bool bYES = true;
        const bool bNO = false;
        //
        const bool bOK = true;
        const bool bBAD = false;
        //
        const int iON = 1;
        const int iOFF = 0;
        //
        const int iYES = 1;
        const int iNO = 0;
        //
        const int iOK = 1;
        const int iBAD = 0;
        #endregion
        #region $include Mdm1Srt1ImportTld1 PageMain MdmLocal Result properties
        // Initialization
        public bool bLocalStarted = false;
        public bool bLocalRunning = false;
        // <Area Id = "LocalCallResults">
        private string spLocalProcessName;
        public string LocalProcessName { get { return spLocalProcessName; } set { spLocalProcessName = value; } }
        private string spLocalClassName;
        public string LocalClassName { get { return spLocalClassName; } set { spLocalClassName = value; } }
        private string spLocalMethodName;
        private string spLocalPatternName;
        public string LocalPatternName { get { return spLocalPatternName; } set { spLocalPatternName = value; } }
        // Area is refers to area within coding patern
        private string spLocalAreaName; // such as init, main, loop, dispose, open, close, display
        public string LocalAreaName { get { return spLocalAreaName; } set { spLocalAreaName = value; } }
        public string LocalMethodName { get { return spLocalMethodName; } set { spLocalMethodName = value; } }
        private int ipLocalResult;
        public int iLocalResult { get { return ipLocalResult; } set { ipLocalResult = value; } }
        private string spLocalResult;
        public string LocalStringResult { get { return spLocalResult; } set { spLocalResult = value; } }
        private bool bpLocalResult;
        public bool bLocalResult { get { return bpLocalResult; } set { bpLocalResult = value; } }
        private object oopLocalResult;
        public object LocalResult { get { return oopLocalResult; } set { oopLocalResult = (object)value; } }
        private bool bpLocalObjectDoesExist;
        public bool LocalObjectDoesExist { get { return bpLocalObjectDoesExist; } set { bpLocalObjectDoesExist = value; } }
        #endregion

    }
}
