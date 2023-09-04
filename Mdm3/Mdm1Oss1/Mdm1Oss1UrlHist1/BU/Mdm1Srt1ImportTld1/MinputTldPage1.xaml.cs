//Top//
using System;
using System.Collections.Generic;
// using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
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

// using Mdm.Oss.ClipboardUtil;
using Mdm.Oss.CodeUtil;
using Mdm.Oss.Support;
using Mdm.Oss.Mapp;
using Mdm.Oss.Mobj;
using Mdm.Oss.UrlUtil.Hist;
using Mdm.Oss.FileUtil;
//  using Mdm1Oss1MinputTld1Thread;
// using    Mdm1Oss1FileCreation1;
using System.ComponentModel;
using System.Timers;
using System.Diagnostics;   
using System.Threading;

namespace Mdm.Srt.InputTld
{

    public partial class MinputTldPageMain : Page
    {
        #region Clipboard Ui Helper
        // [GuidAttribute("DF29D855-D0EC-4DA1-BCC3-42FA3A09B1CB")]
        // [ComVisibleAttribute(false)]
        //public interface SVsUIHierWinClipboardHelper
        #endregion
        #region Common Declarations and Delegates
        // Mdm1 Srt1 InputTld1 OpSys Bootstrap
        // Mdm.Srt.InputTld - MinputTld Class
        //
        // delegates and event callbacks
        /*
        public delegate void TextBoxChangeDelegate(object sender, string s);
        public delegate void TextBoxAddDelegate(object sender, string s);
        public delegate void ProgressCompletionDelegate(object sender, string sField, int iAmount, int iMax);

        public event TextBoxChangeDelegate InputFileLineChange;
        public event TextBoxChangeDelegate OutputFileLineChange;

        public event ProgressCompletionDelegate oStatusLineMdmChanged;

        public event TextBoxChangeDelegate StatusLineMdmChanged;
        public event TextBoxChangeDelegate StatusLineMdmText2TextChanged;
        public event TextBoxChangeDelegate StatusLineMdmText3TextChanged;
        public event TextBoxChangeDelegate StatusLineMdmText4TextChanged;

        public event TextBoxAddDelegate StatusLineMdmText1TextAdd;
        public event TextBoxAddDelegate StatusLineMdmText2TextAdd;
        public event TextBoxAddDelegate StatusLineMdmText3TextAdd;
        public event TextBoxAddDelegate StatusLineMdmText4TextAdd;
        // public delegate void WorkerProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs eaProcessChangeEventArgs);
        */
        delegate void UpdatePageUiHandler(RoutedEventHandler erehPassedRoutedEventHandler);
        delegate void UpdatePageUiProgressHandler(object oPassedSender, ProgressChangedEventArgs ePassedPcea);
        delegate void UpdatePageUiMessageHandler(object oPassedSender, String sPassedMessage);
        delegate void UpdatePageUiEvent(RoutedEventArgs ereaPassedRoutedEventArgs);

        // public event EventHandler<MdmCommandEventArgs> MessageSentEvent;

        public delegate void MessageSendEventHandler(object sender, string sPassedMessage);

        public event MessageSendEventHandler MessageToPage;
        #endregion
        // #region ClassControl
        #region ClaseInternalResults;
        internal long iAppCoreObjectCreate;
        internal long iAppCoreObjectGetFromApp;
        internal long iAppCoreObjectGet;
        internal long iAppCoreObjectSet;
        internal long iAppObjectGet;
        internal long iAppObjectSet;
        internal long iAppPageMainObjectGet;
        internal long iAppPageMainObjectSet;
        internal long iApplicationMobjectObjectGet;
        internal long iApplicationMobjectObjectSet;
        //
        // Page
        internal long iOnResize;
        internal long iPage1;
        internal long iPage1Loaded;
        internal long Page1SizeChangedResult;
        internal long Page2CreationResult;
        internal long Page2NavigateResult;
        //
        // File Action
        public long iAppActionDo;
        public long iAppStart;
        public long iAppPause;
        public long iAppCancel;
        //
        public int iAppActionWaitCounter = 0;
        //
        public bool bImportProgressBarColored = false;
        public bool bImportProgressBarColorChanged = true;
        //
        #endregion
        #region ClasInternalStatusMessageDeclarations
        // TextBox StatusLine1 = new TextBox();
        // TextBox StatusLine2 = new TextBox();
        TextBox StatusLine3 = new TextBox();
        TextBox StatusLine4 = new TextBox();
        private string spStatusLineMdmText1 = "unknown";
        public string StatusLineMdmText1 { get { return spStatusLineMdmText1; } set { spStatusLineMdmText1 = value; } }
        private string spStatusLineMdmText2 = "unknown";
        public string StatusLineMdmText2 { get { return spStatusLineMdmText2; } set { spStatusLineMdmText2 = value; } }
        private string spStatusLineMdmText3 = "unknown";
        public string StatusLineMdmText3 { get { return spStatusLineMdmText3; } set { spStatusLineMdmText3 = value; } }
        private string spStatusLineMdmText4 = "unknown";
        public string StatusLineMdmText4 { get { return spStatusLineMdmText4; } set { spStatusLineMdmText4 = value; } }

        public BackgroundWorker worker;   


        #endregion
        // #endregion
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
        public MinputTldThread omVe;
        // <Area Id = "omAplication">
        public Mapplication omMa;
        // <Area Id = "omW">
        public MinputTld omWt;
        // TODO // <Area Id = "omcLocalClipboard">
        // TODO internal Mclipboard1Form1 omCl;
        // <Area Id = "omU">
        internal MurlHist1Form1 omUr;


        #endregion
        // #endregion
        #region PageEvents
        // public event EventHandler<CloseEventArgs> CancelClose;
        #endregion
        #region Page Declartions
        // <Area Id = "omP">
        internal MinputTldPageMain omPm;
        double dConsoleDisplayCount = 0;
        double dConcoleDisplayAdjustCount = 0;
        double dConcoleDisplayAdjustCountMax = 15;
        double dConsoleWidthHigh = 0;
        double dConsoleWidthLow = 0;
        // <Area Id = "omP2">
        internal MinputTldPageDetail omPd;
        #endregion
        // Class Internals - Properties Fields and Attributes
        #region MinputTldPageMain Internal Field Declarations
        internal string InputFileNameCurrent;
        internal Mfile InputFile;
        internal long iInputFileNameCurrentNotValid = 99999;

        //xxxxxxxxxxxxxxxx
        internal string OutputSystemNameCurrent = "";
        internal object OutputSystemObject = null;

        internal string OutputDatabaseNameCurrent = "";
        internal SqlConnection OutputDatabaseObject = null;

        //xxxxxxxxxxxxxxxx
        internal string OutputFileNameCurrent = "99999";
        internal string OutputFileNameLast = "99999";
        internal Mfile OutputFileObject;

        internal long OutputFileNameCurrentNotValid = 99999;

        internal string OutputFileItemIdCurrent = "99999";
        internal long OutputFileItemIdCurrentNotValid = 99999;

        internal string OutputFileOptionsCurrent = "";

        internal bool OptionToDoOverwriteExistingItemCurrent;
        internal bool OptionToDoCheckItemIdsCurrent;
        internal bool OptionToDoCheckFileDoesExistCurrent;
        internal bool OptionToDoEnterEachItemIdCurrent;
        internal bool OptionToDoLogActivityCurrent;
        internal bool OptionToDoProceedAutomaticallyCurrent;
        internal bool OptionToDoCreateMissingFileCurrent;

        internal string sPage2ReturnValue;
        internal System.Uri uTld1Page2Uri = new System.Uri("/Mdm.Srt.InputTld;component/page2.xaml", System.UriKind.Relative);

        #endregion
        #endregion
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
        protected int iPickPrinterRouting = 14;
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
        protected int iPickPrinterColumnCounter = 0;
        protected int iPickPrinterRowCounter = 0;
        protected int iPickPrinterColumn = 0;
        protected int iPickPrinterRow = 0;
        //
        // Console Output
        protected bool ConsolePickConsole_NewLine = false;
        protected int iConsolePickConsole_ColumnCounter = 0;
        protected int iConsolePickConsole_RowCounter = 0;
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
        protected string CrLf = ((char)13).ToString() + ((char)10).ToString();
        string Cr = ((char)13).ToString();
        string Lf = ((char)10).ToString();
        string Eof = ((char)26).ToString();
        // string Eot =  ((char)26).ToString();
        // PickSyntax Delimeters
        // string Sm =  ((char)255).ToString();
        string Am = ((char)254).ToString();
        string Vm = ((char)253).ToString();
        string Svm = ((char)252).ToString();
        string Lvm = "*";
        string Lsvm = "@";
        // Special Ascii Characters
        string Tld = "~";
        string Asterisk = "*";
        string Stick = "|";
        string BackSlash = @"\";
        string ForwardSlash = @"/";
        string AtSymbol = @"@";
        // White Space Characters
        string Ff = ((char)12).ToString();
        string Bs = ((char)08).ToString();
        string Sp = ((char)20).ToString();
        // Tab ; Horizontal Tab
        // Vtab ; Vertical Tab
        // Null
        string Null = ((char)00).ToString();
        // Status Verbose Constants
        // System Call Function Constants
        protected int iPickSystemCommand = -1;
        const int SYSTEM_COMMAND_LINE = 0;
        const int SYSTEM_SLEEP = 11;
        const int SYSTEM_TYPEAHEAD_CHARACTERS = 14;
        // </Section Summary>
        #endregion
        #region External Command Declarations
        // public event EventHandler<MdmCommandEventArgs> SampleEvent;
        #endregion
        #region Constructors and Initialization
        public MinputTldPageMain()
        {
            iPage1 = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            //
            InitializeComponent();
            //
            // MessageToPage += new MessageSendEventHandler(StatusLineMdmChanged);
            //
            this.PageScrollViewer.Content = this.PageGridMain;
            //
            this.Content = this.PageScrollViewer;
            // MinputTldApp.Current.MainWindow.Content = this.PageScrollViewer;
            //
            // MinputTldPageMain.ToolTip = "This application imports and converts files into a database";
            ApplicationHeading1.ToolTip = "Utility to import single or multiple items from a text file.";
            InputFileLine.ToolTip = "Enter the file that will be processed.  \nNormally a text file with optional TLD format unique record keys (iTemp0.e. ~me~) \nwhen there are multiple records in the file.";
            InputFileNameLabel.ToolTip = InputFileLine.ToolTip;
            OutputFileLine.ToolTip = "Enter the output file that the process will use.  \nNormal a text file with optional TLD format row / records names (iTemp0.e. ~me~) \nwhen there are multiple records in the file.";
            OutputFileNameLabel.ToolTip = OutputFileLine.ToolTip;
            OutputFileItemId.ToolTip = "Enter the Id, row identifier or (unique) key \nto be used in writing to the Output File.  \nNot required for TLD formated text files \nor where multiple items are merged into a single record.";
            OutputIdLabel.ToolTip = OutputFileItemId.ToolTip;
            //
            OptionsLabel.ToolTip = "Check off any options you with to use during importing.";
            OptionToDoOverwriteExistingItem.ToolTip = "Check this if you want to overwrite (replace) items \nin the output file that already exist";
            OptionToDoCheckFileDoesExist.ToolTip = "Check this to indicate the output file must already exist.";
            OptionToDoCheckItemIds.ToolTip = "Check this to indicate the record keys must alreay exist in the output file to be valid.";
            OptionToDoEnterEachItemId.ToolTip = "Check this if you wish to enter the record key \nfor each item to be added to the output file.";
            OptionToDoLogActivity.ToolTip = "Check this box to keep a log file of the import processing";
            OptionToDoProceedAutomatically.ToolTip = "Check this box to proceed without further prompting. \nDefault responses will be used where required.";
            OptionToDoCreateMissingFile.ToolTip = "Check this box if you want the output file to be created if it is missing.";
            //
            DatabasePageButton.ToolTip = "Click the Database button to enter output: \nDatabase, \nServer, \nUser ID and \nSecurity information.";
            StartButtonPressed.ToolTip = "Click Start to begin processing your file.";
            PauseButtonPressed.ToolTip = "Click Pause to temporarily stop processing your file.";
            CancelButtonPressed.ToolTip = "Click Cancel to stop processing or close the application.";
            //
            ConsoleCommandText.ToolTip = "";
            ConsoleCommandlabel.ToolTip = ConsoleCommandText.ToolTip;
            StatusLine1.ToolTip = "";
            StatusLine2.ToolTip = "";
            //
            StartButtonPressed.IsEnabled = true;
            CancelButtonPressed.IsEnabled = false;
            PauseButtonPressed.IsEnabled = false;
            //
            StatusLineMdmText1 = "";
            StatusLineMdmText2 = "";
            StatusLineMdmText3 = "";
            StatusLineMdmText4 = "";
            //
            iPage1 = AppCoreObjectCreate();
            // MinputTldApp.Current.MainWindow.ShowInTaskbar = false;

            // public delegate void TextBoxChangeDelegate(object sender, TextChangedEventArgs eTcea);
            // public delegate void ProgressCompletionDelegate(object sender, string sField, int iAmount, int iMax);
            //

            /*
            omVe.StatusLineMdmChanged += new MinputTldThread.TextBoxChangeDelegate(StatusLineMdmChanged);
            omVe.StatusLineMdmText2TextChanged += new MinputTldThread.TextBoxChangeDelegate(StatusLineMdmText2TextChanged);
            omVe.StatusLineMdmText3TextChanged += new MinputTldThread.TextBoxChangeDelegate(StatusLineMdmText3TextChanged);
            omVe.StatusLineMdmText4TextChanged += new MinputTldThread.TextBoxChangeDelegate(StatusLineMdmText4TextChanged);

            omVe.StatusLineMdmText1TextAdd += new MinputTldThread.TextBoxAddDelegate(StatusLineMdmText1TextAdd);
            omVe.StatusLineMdmText2TextAdd += new MinputTldThread.TextBoxAddDelegate(StatusLineMdmText2TextAdd);
            omVe.StatusLineMdmText3TextAdd += new MinputTldThread.TextBoxAddDelegate(StatusLineMdmText3TextAdd);
            omVe.StatusLineMdmText4TextAdd += new MinputTldThread.TextBoxAddDelegate(StatusLineMdmText4TextAdd);

            omVe.oStatusLineMdmChanged += new MinputTldThread.ProgressCompletionDelegate(ProgressBarMdmCompletionChanged);
            //
            // delegate oStatusLineMdmChanged += new System.Delegate(ProgressBarMdmCompletionChanged(ProgressBarMdmCompletionChanged);
            //
            */
        }

        /* // TODO Mdm Command Event "Main" Test Method DoEvents
        public static void MdmCommandDoLocalEventsMain() {
            MdmCommandEvent MdmCommandEventLocal = new MdmCommandEvent();
            //
            MdmCommandEventLocal.MdmEventHandlerThisThreadEventReceived += 
                new EventHandler<MdmCommandEventArgs>(MdmCommandLocalEventInstanceHandlerMethod);
            //
            MdmCommandEventLocal.MdmCommandEventMethod("Hey wait a minute!" + "\n");
            MdmCommandEventLocal.MdmCommandEventMethod("Is MS's example OK?" + "\n");
            MdmCommandEventLocal.MdmCommandEventMethod("It appears odd." + "\n");
            MdmCommandEventLocal.MdmCommandEventMethod("The public method does not include the calling object!" + "\n");
            MdmCommandEventLocal.MdmCommandEventMethod(@"Yet the local method passes a 'this' calling object!" + "\n");
            MdmCommandEventLocal.MdmCommandEventMethod(@"and calls itself using 'this' and the event message" + "\n");
            MdmCommandEventLocal.MdmCommandEventMethod(@"it seems circular until you realise that the caller" + "\n");
            MdmCommandEventLocal.MdmCommandEventMethod(@"is calling the locally declared handler per the " + "\n");
            MdmCommandEventLocal.MdmCommandEventMethod(@"thread safe event class definition!  This seems wrong" + "\n");
            MdmCommandEventLocal.MdmCommandEventMethod(@"in the the 'object src' with alwasy be 'this' not the" + "\n");
            MdmCommandEventLocal.MdmCommandEventMethod(@"external caller..." + "\n");


        }
        // TODO Mdm Command Event Thread Safe Local Message Handler
        private static void MdmCommandLocalEventInstanceHandlerMethod(object src, MdmCommandEventArgs MdmCommandEventInstance) {
            // Console.WriteLine(MdmCommandEventInstance.Message);
            System.Diagnostics.Debug.WriteLine("MinputTldPageMain Mdm Message: Mdm External Command: " + MdmCommandEventInstance.Message + "  From:" + src.ToString(), "MdmCommand" + "\n");
        }
        */
        /* // TODO Thread Save OnResize
        protected virtual void OnResize(EventArgs eTcea) {
            iOnResize = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            Tld1Page1.Height = 800;
            Tld1Page1.Width = 600;
            Tld1Page1.OnResize(eTcea);
            // UIElement element, Size previousSize, bool widthChanged, bool heightChanged);
            // as in: base.OnRenderSizeChanged(this.Tld1Page1.VisualParent, 0, true, true);
            Tld1Page1.InvalidateVisual();
        }
        */
        #endregion
        // MinputTldPageMain  - xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Page1Load
        private void Page1Loaded(object sender, RoutedEventArgs e) {
            iPage1Loaded = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            StatusLine1.Text += "Loading please wait... ";
            //
            ConsoleMdmControlSet();
            //
            // Initialize Page 1
            if (omPm == null) {
                omPm = this;
                omPm = this;
                omMa.AppPageMainObjectSet(omPm);
                omCo.AppPageMainObject(omPm);
                // omCo.AppPageMainSetDefaults((MinputTldPageMain)this);
                // Application Started
                if (!LocalStarted) {
                    // Set Focus to First Field
                    MdmControlSetFocus(InputFileLine);
                    LocalStarted = true;
                }
            }
            // LocalLongResult = omCo.AppInitialize();
            // Page2Instance
            if (omPd == null) {
                // Initialize MinputTldPageDetail
                Page2Creation(omOb);
            }
            
            // iPage1Loaded
            // TODO Mdm.Srt.InputTld.MinputTldPageMain.MdmCommandDoLocalEventsMain();
        }

        private void Page1SizeChanged(object sender, RoutedEventArgs e) {
            PageSizeChangedDoAdjust((Page)sender, 0, 0);
        }
        public void PageSizeChangedDoAdjust(Page sender, double dPassedDesiredWidth, double dPassedDesiredHeight) {
            Page1SizeChangedResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
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


            double dGridActualWidth = 0;
            double dGridActualHeight = 0;
            try {
                if (omPm != null) {
                    dGridActualWidth = omPm.PageGridMain.ActualWidth;
                    dGridActualHeight = omPm.PageGridMain.ActualHeight;
                } else {
                    dGridActualWidth = MinputTldApp.Current.MainWindow.ActualWidth;
                    dGridActualHeight = MinputTldApp.Current.MainWindow.ActualHeight;
                }
            } catch {
                dGridActualWidth = sender.ActualWidth;
                dGridActualHeight = sender.ActualHeight;
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
                dGridActualWidth = dDesiredWidth;
                dGridActualHeight = dDesiredHeight;
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
        private void Page1GridChanged(object sender, RoutedEventArgs e) {
            Page1GridSizeChangedDoAdjust((Page)sender, 0, 0);
        }
        public void Page1GridSizeChangedDoAdjust(Page sender, double dPassedDesiredWidth, double dPassedDesiredHeight) {
            Page1SizeChangedResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // PageGridMain.
            // omPm.TldOptionRows
            //
            double dColWidth_0 = gcCol0.ActualWidth;
            double dColWidth_1 = gcCol1.ActualWidth;
            double dColWidth_2 = gcCol2.ActualWidth;
            double dColWidth_3 = gcCol3.ActualWidth;
            double dColWidth_4 = gcCol4.ActualWidth;
            double dColWidth_5 = gcCol5.ActualWidth;
        }
        #endregion
        // MinputTldPageDetail - xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Page2Load
        // Initalize MinputTldPageDetail
        void Page2Creation(Mobject omPmssedO) {
            Page2CreationResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // Page2Function_Creation();
            if (omPd == null) {
                // Initialize MinputTldPageDetail
                string tmpstr = "Initial Data Item Value";
                omPd = new MinputTldPageDetail(omOb, tmpstr);
                omMa.AppPageDetailObjectSet(omPd);
                Page2CreationResult = omMa.AppCoreObjectGet(omOb);
                // omCo.AppPageMainObject((MinputTldPageMain) omPm, (MinputTldPageDetail) omPd);
                omCo.AppPageMainObject(omPm, omPd);
            }
            // Set MinputTldPageDetail Fields
            omCo.AppPageDetailSetDefaults(omPm, omPd);
            sTemp = "Do you exist?";
            StatusLineMdmText1TextAdd(this, "\n" + "Check if the file exists: " + omPd.DoesExist(sTemp) + "\n");
        }
        void Page2Navigate() {
            Page2NavigateResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            if (omPd == null) { Page2Creation(omOb); }
            omPd.Height = Height;
            omPd.Width = Width;
            // omCo.LocalLongResult = omCo.AppPageDetailSetDefaults(omPm, omPd);
            NavigationService.Navigate(omPd, System.UriKind.Relative);
            // Page2NavigateResult
        }
        #endregion
        // Focus Control - xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Focus Control
        public void MdmControlCheckColor() {
            if (bImportProgressBarColorChanged) {
                if (omCo.bRunPausePending 
                    || omCo.iaRunActionState[RunPause, RunState] != RunTense_Off) {
                        ProgressBarMdm1.Foreground = System.Windows.Media.Brushes.Yellow;
                        ProgressBarMdm1.Background = System.Windows.Media.Brushes.LightGreen;
                } else if (omCo.bRunCancelPending 
                    || omCo.iaRunActionState[RunCancel, RunState] == RunTense_Do
                    || omCo.iaRunActionState[RunCancel, RunState] == RunTense_Doing
                    || omCo.iaRunActionState[RunCancel, RunState] == RunTense_Did) {
                        ProgressBarMdm1.Foreground = System.Windows.Media.Brushes.Red;
                        ProgressBarMdm1.Background = System.Windows.Media.Brushes.Orange;
                } else if (omCo.bRunStartPending 
                    || omCo.iaRunActionState[RunRunDo, RunState] == RunTense_Do
                    || omCo.iaRunActionState[RunRunDo, RunState] == RunTense_Doing) {
                    ProgressBarMdm1.Background = System.Windows.Media.Brushes.LightGray;
                    if (omCo.RunErrorDidOccur) {
                        ProgressBarMdm1.Foreground = System.Windows.Media.Brushes.LightYellow;
                    } else {
                        ProgressBarMdm1.Foreground = System.Windows.Media.Brushes.Green;
                    }
                } else if (omCo.iaRunActionState[RunRunDo, RunState] == RunTense_Did) {
                    ProgressBarMdm1.Foreground = System.Windows.Media.Brushes.Blue;
                    ProgressBarMdm1.Background = System.Windows.Media.Brushes.LightGray;
                } else {
                    ProgressBarMdm1.Foreground = System.Windows.Media.Brushes.Green;
                    // ProgressBarMdm1.Foreground = System.Windows.Media.Brushes.Blue;
                    ProgressBarMdm1.Background = System.Windows.Media.Brushes.LightGray;
                }
                //
                // ProgressBarMdm1.Value = 0;
                // this.BringIntoView();
                ProgressBarMdm1.InvalidateVisual();
                bImportProgressBarColorChanged = false;
            }
        }
        public void MdmControlSetFocus(System.Windows.Controls.Control control)
        {
            // Set focus to the control, if it can receive focus.
            if (control.Focusable) { control.Focus(); }
            this.BringIntoView();
        }
        private void InputFileLineGotFocus(object sender, RoutedEventArgs e)
        {
            MdmControlCheckColor();
            StatusLine1.ToolTip = "Enter the file that will be processed.  Normal a text file with optional TLD format row / records names (ipPickDictItemGet.eTcea. ~me~) when there are multiple records in the file.";
            StatusLine1.Text += "Enter the file you want to import." + "\n";
            InputFileLine.Text += @"C:\Rec\ACTDICT.TXT";
        }
        private void InputFileLineLostFocus(object sender, RoutedEventArgs e)
        {
            // StatusLine1.Text = "";
        }
        private void OutputFileLineGotFocus(object sender, RoutedEventArgs e)
        {
            MdmControlCheckColor();
            StatusLine1.Text += "Enter the file name to place the data into." + "\n";
            // OutputFileLine.Text = @"C:\Rec\daveACTDICT.TXT";
        }
        private void OutputFileLineLostFocus(object sender, RoutedEventArgs e)
        {
            // StatusLine1.Text = "";
            /*
            if (OutputFileNameLast != OutputFileLine.Text && omPd != null) {
                omCo.AppPageDetailSetDefaults((MinputTldPageMain)omPm, (MinputTldPageDetail)omPd);
            }
            */
            OutputFileNameLast = OutputFileLine.Text;
        }
        private void OutputFileItemIdGotFocus(object sender, RoutedEventArgs e)
        {
            MdmControlCheckColor();
            StatusLine1.Text += "Enter the record key if this is a single row or document." + "\n";
        }
        private void OutputFileItemIdLostFocus(object sender, RoutedEventArgs e)
        {
            // StatusLine1.Text = "";
        }
        private void OptionToDoOverwriteExistingItemGotFocus(object sender, RoutedEventArgs e)
        {
            MdmControlCheckColor();
            StatusLine1.Text += "Check this if you want to overwrite items that exist in the output file." + "\n";
        }
        private void OptionToDoOverwriteExistingItemLostFocus(object sender, RoutedEventArgs e)
        {
            // StatusLine1.Text = "";
        }
        private void OptionToDoCheckFileDoesExistGotFocus(object sender, RoutedEventArgs e)
        {
            MdmControlCheckColor();
            StatusLine1.Text += "Check this if the output file must already exist." + "\n";
            // can't have create and exists checked at the same time
        }
        private void OptionToDoCheckFileDoesExistLostFocus(object sender, RoutedEventArgs e)
        {
            // StatusLine1.Text = "";
            // can't have create and exists checked at the same time
        }
        private void OptionToDoCheckItemIdsGotFocus(object sender, RoutedEventArgs e)
        {
            MdmControlCheckColor();
            StatusLine1.Text += "Check this if you want to individually Confirm each Item Id in the Output File already DoesExist." + "\n";
        }
        private void OptionToDoCheckItemIdsLostFocus(object sender, RoutedEventArgs e)
        {
            // StatusLine1.Text = "";
        }
        private void OptionToDoEnterEachItemIdGotFocus(object sender, RoutedEventArgs e)
        {
            MdmControlCheckColor();
            StatusLine1.Text += "Check this if you want to enter each Item Id or Row Key individually." + "\n";
        }
        private void OptionToDoEnterEachItemIdLostFocus(object sender, RoutedEventArgs e)
        {
            // StatusLine1.Text = "";
        }
        private void OptionToDoLogActivityGotFocus(object sender, RoutedEventArgs e)
        {
            MdmControlCheckColor();
            StatusLine1.Text += "Check this if you want to have this run logged." + "\n";
        }
        private void OptionToDoLogActivityLostFocus(object sender, RoutedEventArgs e)
        {
            // StatusLine1.Text = "";
        }
        private void OptionToDoProceedAutomaticallyGotFocus(object sender, RoutedEventArgs e)
        {
            MdmControlCheckColor();
            StatusLine1.Text += "Option to run on automatic without prompting you for further responses." + "\n";
        }

        private void OptionToDoProceedAutomaticallyLostFocus(object sender, RoutedEventArgs e)
        {
            // StatusLine1.Text = "";
        }

        private void OptionToDoCreateMissingFileGotFocus(object sender, RoutedEventArgs e)
        {
            MdmControlCheckColor();
            StatusLine1.Text += "Option to create the output file if it is missing." + "\n";
            // can't have create and exists checked at the same time
        }

        private void OptionToDoCreateMissingFileLostFocus(object sender, RoutedEventArgs e)
        {
            // StatusLine1.Text = "";
            // can't have create and exists checked at the same time
        }
        private void DatabasePageButtonGotFocus(object sender, RoutedEventArgs e)
        {
            MdmControlCheckColor();
            StatusLine1.Text += "Click to change output database settings." + "\n";
        }

        private void DatabasePageButtonLostFocus(object sender, RoutedEventArgs e)
        {
            // StatusLine1.Text = "";
        }
        private void AppStartButtonPressedGotFocus(object sender, RoutedEventArgs e)
        {
            MdmControlCheckColor();
            StatusLine1.Text += "Click to start processing." + "\n";
        }

        private void AppStartButtonPressedLostFocus(object sender, RoutedEventArgs e)
        {
            // StatusLine1.Text = "";
        }
        private void AppPauseButtonPressedGotFocus(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
            StatusLine1.Text += "Click to pause processing." + "\n";
        }

        private void AppPauseButtonPressedLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void AppCancelButtonPressedGotFocus(object sender, RoutedEventArgs e)
        {
            StatusLine1.Text += "Click to cancel processing and exit." + "\n";
        }
        private void AppCancelButtonPressedLostFocus(object sender, RoutedEventArgs e)
        {
            // StatusLine1.Text = "";
        }
        #endregion
        #region CheckBoxes Checked

        private void OptionToDoOverwriteExistingItemChecked(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
        }

        private void OptionToDoCheckFileDoesExistChecked(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
        }

        private void OptionToDoCheckItemIdsChecked(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
        }

        private void OptionToDoEnterEachItemIdChecked(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
        }

        private void OptionToDoCreateMissingFileChecked(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
        }

        private void OptionToDoProceedAutomaticallyChecked(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
        }

        private void OptionToDoLogActivityChecked(object sender, RoutedEventArgs e) {
            MdmControlCheckColor();
        }
        #endregion
        // OutputDatabasePage2 - xxxxxxxxxxxxxxxxxxxxxxxxxx
        #region OutputDatabasePage2
        private void DatabasePageButtonDo(object sender, RoutedEventArgs e)
        {
            // StatusLineMdmText2 = "";
            StatusLineMdmText1TextAdd(this, "Output Database MinputTldPageDetail..." + "\n");
            StatusLineMdmText1TextAdd(this, "Output Database MinputTldPageDetail..." + "\n");
            Page2Navigate();
            // Dead code due to navigation
            StatusLineMdmText1TextAdd(this, "Return from Output Database MinputTldPageDetail" + "\n");
            // Set focus to this sLine
            MdmControlSetFocus(StartButtonPressed);
        }
        #endregion
        // AppLogic_ - Validation - xxxxxxxxxxxxxxx
        #region $Section Mdm.Srt.InputTld MinputTldPageMain Validate_Fields_Local
        #region $include Mdm.Srt.InputTld MinputTldPageMain Validate_Feild
        private long InputFileLineValidation() {
            StatusLineMdmText1TextAdd(this, "Import File Line Validation" + "\n");
            if (InputFileLine.Text == "") {
                // File Item Id Empty
                // This is usually OK
                // Check Options
                iInputFileNameCurrentNotValid = (int)Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.ResultMissingName;
            } else {
                if (InputFileNameCurrent != InputFileLine.Text) {
                    // StatusLineMdmText1TextAdd(this, 
                    /// changed
                    StatusLineMdmText1TextAdd(this, "Input File Changed to: " + InputFileLine.Text + "\n");
                    StatusLineMdmText2TextAdd(this, "Checking please wait... " + InputFileLine.Text + "\n");
                    /// do check
                    iInputFileNameCurrentNotValid = omCo.InputFileCheck(InputFileLine.Text);
                    if (iInputFileNameCurrentNotValid == 0) {
                        /// set current value
                        InputFileNameCurrent = InputFileLine.Text;
                    }
                }
            }
            return iInputFileNameCurrentNotValid;
        }
        private long OutputFileLineValidation() {
            // Validation
            StatusLineMdmText1TextAdd(this, "Output File Line Validation" + "\n");
            //string tmp;
            //string tmp1;
            //tmp = OutputFileLine.Text;
            //@tmp1 = OutputFileLine.Text;
            if (OutputFileLine.Text == "") {
                // File Item Id Empty
                // This is usually OK
                // Check Options
                OutputFileNameCurrentNotValid = (int)Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.ResultMissingName;
            } else {
                if (OutputFileNameCurrent != OutputFileLine.Text) {
                    /// changed           
                    StatusLineMdmText1TextAdd(this, "Output File Changed to: " + OutputFileLine.Text + "\n");
                    StatusLineMdmText2TextAdd(this, "Checking please wait... " + OutputFileLine.Text + "\n");
                    /// do check
                    OutputFileNameCurrentNotValid = omCo.OutputFileCheck(OutputFileLine.Text);
                    if (OutputFileNameCurrentNotValid == 0) {
                        /// set current value
                        OutputFileNameCurrent = OutputFileLine.@Text;
                    }
                }
            }
            return OutputFileNameCurrentNotValid;

        }
        private long OutputFileItemIdValidation() {
            // Item Id Validation
            OutputFileItemIdCurrentNotValid = 0;
            StatusLineMdmText1TextAdd(this, "Output File Item Id Validation" + "\n");
            if (OutputFileItemId.Text == "") {
                // File Item Id Empty
                // This is usually OK
                // Check Options
                // OutputFileItemIdCurrentNotValid = (int)DatabaseControl.ResultMissingName;
                // OutputFileItemIdCurrentNotValid = 0;
            } else {
                if (OutputFileItemIdCurrent != OutputFileItemId.Text) {
                    /// changed           
                    StatusLineMdmText1TextAdd(this, "Output File Item Id: " + OutputFileItemId.Text + "\n");
                    StatusLineMdmText2TextAdd(this, "Checking please wait... " + InputFileLine.Text + "\n");
                    /// do check
                    OutputFileItemIdCurrentNotValid = omCo.OutputFileItemIdCheck(OutputFileItemId.Text);
                    /// set current value
                    OutputFileItemIdCurrent = OutputFileItemId.Text;
                }
            }
            return OutputFileItemIdCurrentNotValid;
        }
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain Validate_StartDo
        private long AppStartDoValidation(object sender, RoutedEventArgs eRea) {
            iAppStart = (int)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            #region ProcessFields_Validate_Options
            /// 
            ///
            StatusLineMdmText1 = "Processing Options: ";
            if (OptionToDoOverwriteExistingItem.IsChecked == true) {
                OptionToDoOverwriteExistingItemCurrent = true;
                StatusLineMdmText1 += "\n" + "OptionToDoOverwriteExistingItemCurrent";
            } else {
                OptionToDoOverwriteExistingItemCurrent = false;
            };

            if (OptionToDoCheckItemIds.IsChecked == true) {
                OptionToDoCheckItemIdsCurrent = true;
                StatusLineMdmText1 += "\n" + "OptionToDoCheckItemIdsCurrent";
            } else {
                OptionToDoCheckItemIdsCurrent = false;
            };
            if (OptionToDoCheckFileDoesExist.IsChecked == true) {
                OptionToDoCheckFileDoesExistCurrent = true;
                StatusLineMdmText1 += "\n" + "OptionToDoCheckFileDoesExistCurrent";
                OptionToDoCreateMissingFile.IsChecked = false;
            } else {
                OptionToDoCheckFileDoesExistCurrent = false;
            };
            if (OptionToDoEnterEachItemId.IsChecked == true) {
                OptionToDoEnterEachItemIdCurrent = true;
                StatusLineMdmText1 += "\n" + "OptionToDoEnterEachItemIdCurrent";
            } else {
                OptionToDoEnterEachItemIdCurrent = false;
            };

            if (OptionToDoLogActivity.IsChecked == true) {
                OptionToDoLogActivityCurrent = true;
                StatusLineMdmText1 += "\n" + "OptionToDoLogActivityCurrent";
            } else {
                OptionToDoLogActivityCurrent = false;
            };

            if (OptionToDoProceedAutomatically.IsChecked == true) {
                OptionToDoProceedAutomaticallyCurrent = true;
                StatusLineMdmText1 += "\n" + "OptionToDoProceedAutomaticallyCurrent";
            } else {
                OptionToDoProceedAutomaticallyCurrent = false;
            };

            if (OptionToDoCreateMissingFile.IsChecked == true) {
                OptionToDoCreateMissingFileCurrent = true;
                StatusLineMdmText1 += "\n" + "OptionToDoCreateMissingFileCurrent";
            } else {
                OptionToDoCreateMissingFileCurrent = false;
            };
            //
            StatusLineMdmTextSet(2, 0, StatusLineMdmText1, "Scanned options and fouund:" + "\n", "\n" + "The options above were turned on.", true);
            ///
            /// Marshall Options
            /// 
            ///

            omCo.OptionToDoOverwriteExistingItem = OptionToDoOverwriteExistingItemCurrent;
            omCo.OptionToDoCheckItemIds = OptionToDoCheckItemIdsCurrent;
            omCo.OptionToDoEnterEachItemId = OptionToDoEnterEachItemIdCurrent;
            omCo.OptionToDoCheckFileDoesExist = OptionToDoCheckFileDoesExistCurrent;
            omCo.OptionToDoLogActivity = OptionToDoLogActivityCurrent;
            omCo.OptionToDoProceedAutomatically = OptionToDoProceedAutomaticallyCurrent;
            omCo.OptionToDoCreateMissingFile = OptionToDoCreateMissingFileCurrent;
            #endregion
            #region ProcessFields_Validate_InputFile
            iInputFileNameCurrentNotValid = InputFileLineValidation();
            if (iInputFileNameCurrentNotValid != 0) {
                StatusLineMdmText1 = "File " + InputFileLine;
                sTemp = "";
                sTemp1 = "";
                sTemp += MfileStatusResultTextGetString((int)iInputFileNameCurrentNotValid);
                StatusLineMdmTextSet(1, (int)iInputFileNameCurrentNotValid, StatusLineMdmText1, sTemp, sTemp1, true);
            }
            #endregion
            #region ProcessFields_Validate_InputFileItemId
            #endregion
            #region ProcessFields_Validate_OutputFile
            OutputFileNameCurrentNotValid = OutputFileLineValidation();
            StatusLineMdmText1 = "File " + InputFileLine;
            sTemp = "";
            sTemp1 = "";
            sTemp += MfileStatusResultTextGetString((int)iInputFileNameCurrentNotValid);
            if (OutputFileNameCurrentNotValid != 0) {
                MfileStatusResultTextGetString((int)OutputFileNameCurrentNotValid);
                StatusLineMdmTextSet(1, (int)OutputFileNameCurrentNotValid, StatusLineMdmText1, "Output ", "", true);
            }
            // ItemId
            OutputFileItemIdCurrentNotValid = OutputFileItemIdValidation();
            if (OutputFileItemIdCurrentNotValid != 0) {
                MfileStatusResultTextGetString((int)OutputFileItemIdCurrentNotValid);
                StatusLineMdmTextSet(1, (int)OutputFileItemIdCurrentNotValid, StatusLineMdmText1, "Output", "", true);
            }
            #endregion
            #region ProcessFields_Validate_OutputFileItemId
            #endregion
            #region ProcessFields_Validation_Failed_ControlFocus_Set
            if (iInputFileNameCurrentNotValid != 0) {
                // Set focus to this sLine
                MdmControlSetFocus(InputFileLine);
            } else if (OutputFileNameCurrentNotValid != 0) {
                // Set focus to this sLine
                MdmControlSetFocus(OutputFileLine);
            } else if (OutputFileItemIdCurrentNotValid != 0) {
                // Set focus to this sLine
                MdmControlSetFocus(OutputFileItemId);
            } else {
                //Ok
                iAppStart = 1;
            }
            #endregion
            #region ProcessFields_Current_Set
            //
            InputFileNameCurrent = InputFileLine.Text;
            OutputFileNameCurrent = OutputFileLine.Text;
            OutputFileItemIdCurrent = OutputFileItemId.Text;
            //
            // Display Database page
            //
            // OutputSystemNameCurrent,
            // OutputDatabaseNameCurrent,
            #endregion
            return iAppStart;
        }
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmClass Standard Root Word Constants
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
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmLocal Result properties
        // Initialization
        public bool LocalStarted = false;
        public bool LocalRunning = false;
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
        private bool bpLocalBoolResult;
        public bool LocalBoolResult { get { return bpLocalBoolResult; } set { bpLocalBoolResult = value; } }
        private string spLocalStringResult;
        public string LocalStringResult { get { return spLocalStringResult; } set { spLocalStringResult = value; } }
        private long lpLocalLongResult;
        public long LocalLongResult { get { return lpLocalLongResult; } set { lpLocalLongResult = value; } }
        private int ipLocalIntResult;
        public int LocalIntResult { get { return ipLocalIntResult; } set { ipLocalIntResult = value; } }
        private object opLocalObjectResult;
        public object LocalObjectResult { get { return opLocalObjectResult; } set { opLocalObjectResult = (object)value; } }
        private bool bpLocalObjectDoesExist;
        public bool LocalObjectDoesExist { get { return bpLocalObjectDoesExist; } set { bpLocalObjectDoesExist = value; } }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Srt.InputTld MinputTldPageMain AppLogic_Action
        #region $include Mdm.Srt.InputTld MinputTldPageMain AppLogic_RunDeclarations
        #region $include Mdm.Srt.InputTld MinputTldPageMain AppLogic_RunActionConstants
        //
        // if (omCo.iaRunActionState[RunCancel, RunStateCurr] == RunTense_Do || omCo.iaRunActionState[RunPause, RunStateCurr] == RunTense_Did || omCo.iaRunActionState[RunCancel, RunStateCurr] == RunTense_Doing) { bTemp9 = bYES; }
        // if (omCo.iaRunActionState[RunAbort, RunStateCurr] == RunTense_Do || omCo.iaRunActionState[RunPause, RunStateCurr] == RunTense_Did || omCo.iaRunActionState[RunAbort, RunStateCurr] == RunTense_Doing) { bTemp9 = bYES; }
        //
        public int RunTense = 0;
        public const int RunTense_Off = 0;
        public const int RunTense_Do = 1;
        public const int RunTenseOn = 1;
        public const int RunTense_DoNot = 2;
        public const int RunTense_Doing = 3;
        public const int RunTense_Did = 4;
        public const int RunTense_Done = 4;
        public const int RunTense_DidNot = 5;
        //
        public int RunMetric = 0;
        public const int RunState = 1;
        public const int RunState_Last_Update = 2;
        public const int RunDoLast_Count = 3;
        public const int RunDoCount = 4;
        public const int RunDoSkip_Count = 5;
        public const int RunDoError_Count = 6;
        public const int RunDoWarning_Count = 7;
        public const int RunDoRetry_Count = 8;
        //
        public static int iaRunActionState_Max = 25;
        public int[,] iaRunActionState = new int[iaRunActionState_Max + 5, 8];
        public ProgressChangedEventArgs ePceaRunActionState = new ProgressChangedEventArgs((int)0, "");
        //
        public int RunAction = 0;
        public int RunActionRequest = 0;
        //
        public const int RunCancel = 1;
        public const int RunPause = 2;
        public const int RunStart = 3;
        public const int RunNoOp4 = 4;
        public const int RunNoOp5 = 5;
        public const int RunInitialize = 6;
        public const int RunRunDo = 7;
        public const int RunUserInput = 8;
        public const int RunOpen = 9;
        public const int RunMain_Do = 10;
        public const int RunMain_DoSelect = 11;
        public const int RunMain_DoLock_Add = 12;
        public const int RunMain_DoRead = 13;
        public const int RunMain_DoValidate = 14;
        public const int RunMain_DoAccept = 15;
        public const int RunMain_DoReport = 16;
        public const int RunMain_DoProcess = 17;
        public const int RunMain_DoUpdate = 18;
        public const int RunMain_DoWrite = 19;
        public const int RunMain_DoLock_Remove = 20;
        public const int RunClose = 21;
        public const int RunFinish = 22;
        public const int RunAbort = 23;
        public const int RunReloop = 24;
        public const int RunFirst = 25;
        public int RunOptionX = iaRunActionState_Max + 1;
        public int RunOptionY = iaRunActionState_Max + 2;
        public int RunOptionZ = iaRunActionState_Max + 3;
        public int RunOption1 = iaRunActionState_Max + 4;
        public int RunOption2 = iaRunActionState_Max + 5;
        //
        public string[] sRunActionVerb = { "NoOp", 
                                               "Cancel", "Pause", "Start", "NoOp4", "NoOp5", 
                                               "Initialize", "Do", "UserInput", "Open", "DoMain",
                                               "Select", "Lock", "Read", "Validate", "Accept",
                                               "Report", "Process", "Update", "Write", "UnLock",
                                               "Finish", "Abort", 
                                               "OptionX", "OptionY", "OptionZ", "VerbY", "VerbZ"
                                           };
        public string[] sRunActionDoing = { "NoOping", 
                                                "Cancelling", "Pausing", "Starting", "NoOp4", "NoOp5", 
                                               "Initialize", "Doing", "UserInputing", "Opening", "DoingMain",
                                               "Selecting", "Locking", "Reading", "Validating", "Accepting",
                                               "Reporting", "Processing", "Updating", "Writing", "UnLocking",
                                               "Finishing", "Abortint", 
                                               "OptionXing", "OptionYing", "OptionZing", "VerbYing", "VerbZing"
                                            };
        public string[] sRunActionDid = { "NoOped", 
                                              "Cancelled", "Paused", "Started", "NoOp4", "NoOp5", 
                                               "Initialized", "Did", "UserInputed", "Opened", "DoMained",
                                               "Selected", "Locked", "Read", "Validated", "Accepted",
                                               "Reported", "Processed", "Updated", "Writen", "UnLocked",
                                               "Finished", "Aborted", 
                                               "OptionXed", "OptionYed", "OptionZed", "VerbYed", "VerbZed"
                                          };
        //
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain AppLogic_RunActionState_Declarations
        #region $include Mdm.Srt.InputTld MinputTldPageMain AppLogic_RunState
        // <Area Id = "PrimaryActions">
        private string spRunOptions;
        public string RunOptions { get { return spRunOptions; } set { spRunOptions = value; } }
        public int ipRunStatus;
        public int RunStatus { get { return ipRunStatus; } set { ipRunStatus = value; } }
        //
        public string FileActionRequest;
        public string sPickFileActionRequest;
        // <Area Id = "RunStatusControlItFlags">
        public int RunCount = 0;
        public int RunDebugCount = 0;
        public bool bRunReloop = false;
        public bool bRunFirst = true;
        //
        public bool bRunStartPending = bNO;
        public bool bRunPausePending = bNO;
        public bool bRunCancelPending = bNO;
        //
        public string sRunActionRequest;
        public string UserState;
        public string UserCommandPrefix;
        public string UserCommand;
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain AppLogic_RunErrors
        // <Area Id = "Errors">
        public bool bRunAbort = bNO;
        //
        public bool RunErrorDidOccur = false;
        public bool RunErrorDidOccurOnce = false;
        //
        public string sLocalErrorMessage = "";
        //
        public int RunErrorNumber = 0;
        public int RunGlobalErrorNumber = 99999;
        public int RunThrowException = 99999;
        public int RunShellErrorNumber = 99999;
        public int RunErrorCount = 0;
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain AppLogic_RunProcessing_Loop
        // <Area Id = "IterationStatusControlItFlags">
        public int iIterationCount = 0;
        public int iIterationRemaider = 0;
        public int iIterationDebugCount = 0;
        public bool IterationAbort = false;
        public bool IterationReloop = false;
        public bool IterationFirst = true;
        public int iIterationLoopCounter = 0;
        // <Area Id = "MethodIterationStatusControlItFlags">
        public bool MethodIterationAbort = false;
        public bool MethodIterationReloop = false;
        public bool MethodIterationFirst = true;
        public int MethodIterationLoopCounter = 0;
        #endregion
        #endregion
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain AppLogic_Run
        #region $include Mdm.Srt.InputTld MinputTldPageMain AppLogic_RunActionEvaluate Analysis
        public void AppRunActionEvaluate(object sender, ProgressChangedEventArgs ePcea, Mcontroller omHPassed) {
            // This code handles both the Page UI command request and call backs from BgWorker
            try {
                UserState = (string)ePcea.UserState;
            } catch { UserState = ""; }
            UserCommandPrefix = "";
            UserCommand = "";
            if (UserState.Length > 0) { UserCommandPrefix = UserState.Substring(0, 1); }
            if (UserState.Length > 1) { UserCommand = UserState.Substring(1); }

            if (UserCommandPrefix == "$") {
                if (UserCommand == "Start") {
                    if (omHPassed.iaRunActionState[RunRunDo, RunState] != RunTense_Did && omHPassed.iaRunActionState[RunRunDo, RunState] != RunTense_Doing) {
                        bRunStartPending = bYES;
                        omHPassed.iaRunActionState[RunAbort, RunState] = iNO;
                        omHPassed.iaRunActionState[RunFirst, RunState] = iYES;
                        omHPassed.iaRunActionState[RunReloop, RunState] = iNO;
                        RunAction = RunRunDo;
                        RunMetric = RunState;
                        RunTense = RunTense_Do;
                        omHPassed.iaRunActionState[RunRunDo, RunState] = RunTense_Do;
                        omHPassed.ePceaRunActionState = new ProgressChangedEventArgs(0,
                "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString() + "Start");
                        omVe.backgroundWorker_ProgressChanged(this, omHPassed.ePceaRunActionState);
                    }
                } else if (UserCommand == "Started") {
                    bRunStartPending = bNO;
                    omHPassed.iaRunActionState[RunRunDo, RunState] = RunTense_Did;
                } else if (UserCommand == "Cancel") {
                    if (omHPassed.iaRunActionState[RunRunDo, RunState] == RunTense_Doing) {
                        if (omHPassed.iaRunActionState[RunCancel, RunState] != RunTense_Did && omHPassed.iaRunActionState[RunCancel, RunState] != RunTense_Doing) {
                            bRunCancelPending = bYES;
                            RunAction = RunCancel;
                            RunMetric = RunState;
                            RunTense = RunTense_Do;
                            omHPassed.iaRunActionState[RunCancel, RunState] = RunTense_Do;
                            omHPassed.ePceaRunActionState = new ProgressChangedEventArgs(0,
                "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString() + "Cancel");
                            omVe.backgroundWorker_ProgressChanged(this, omHPassed.ePceaRunActionState);
                            omHPassed.RunCancelAsync();
                            // System.Windows.RoutedEventArgs eTcea = new System.Windows.RoutedEventArgs;
                            // eTcea.Source = this;
                            // omVe.CallerAsynchronousEventsCancelClick(this, eTcea);
                        }
                    }
                } else if (UserCommand == "Cancelled") {
                    bRunCancelPending = bNO;
                    omHPassed.iaRunActionState[RunCancel, RunState] = RunTense_Did;
                } else if (UserCommand == "Pause") {
                    if (!bRunCancelPending) {
                        RunAction = RunPause;
                        RunMetric = RunState;
                        if (omHPassed.iaRunActionState[RunPause, RunState] == RunTense_Did) {
                            bRunPausePending = bNO;
                            RunTense = RunTense_Off;
                        } else {
                            // System.Windows.RoutedEventArgs eReaTemp = new System.Windows.RoutedEventArgs();
                            // eReaTemp = null;
                            // omWt.CallerAsynchronousEventsPauseClick;
                            // Set State
                            if (omHPassed.iaRunActionState[RunRunDo, RunState] == RunTense_Doing) {
                                if (omHPassed.iaRunActionState[RunPause, RunState] != RunTense_Did && omHPassed.iaRunActionState[RunPause, RunState] != RunTense_Doing) {
                                    bRunPausePending = bYES;
                                    RunTense = RunTense_Do;
                                    omHPassed.iaRunActionState[RunPause, RunState] = RunTense_Do;
                                }
                            }
                        }
                        omHPassed.iaRunActionState[RunPause, RunState] = RunTense;
                        omHPassed.ePceaRunActionState = new ProgressChangedEventArgs(0,
                "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString() + "Pause");
                        omVe.backgroundWorker_ProgressChanged(this, omHPassed.ePceaRunActionState);
                    }
                } else if (UserCommand == "Paused") {
                    bRunPausePending = bNO;
                    omHPassed.iaRunActionState[RunPause, RunState] = RunTense_Did;
                } else if (UserCommand == "xxxxx") {

                }
            }
        }
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmAppLogic_StartDoProceedOk
        private void AppStartDoMain(object sender, RoutedEventArgs eRea) {
            #region $include Mdm.Srt.InputTld MinputTldPageMain ProcessFields_Init
            iAppStart = 0;
            LocalRunning = false;
            LocalStarted = true;
            //
            StartButtonPressed.IsEnabled = false;
            //
            // StatusLineMdmText1 = "";
            // StatusLineMdmText2 = "";
            // StatusLineMdmText3 = "";
            // StatusLineMdmText4 = "";
            MdmControlCheckColor();
            StatusLineMdmText1TextAdd(this, "Starting File Progessing" + "\n");
            StatusLineMdmText2TextAdd(this, "Verifying your entry, please wait..." + "\n");
            #endregion
            iAppStart = AppStartDoValidation(sender, eRea); // Call to this page's validation
            #region $include Mdm.Srt.InputTld MinputTldPageMain AppLogic_Proceed
            if (iAppStart != 0) {
                // TODO Prompt: OK to process?
                // TODO Are you sure?
                // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                /// TODO AnswerYes:
                //
                StatusLineMdmText1TextAdd(this, "Processing starting, please wait..." + "\n");
                StatusLineMdmText2TextAdd(this, "Initializing..." + "\n");
                //
                // Process Request
                //
                LocalLongResult = (int)omCo.AppDoProcessing();
                //
                // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                /// TODO AnswerNo:
                /// StatusLineMdmText2 = "Start InputTldProcesFile AnswerNo";
                /// 
            }
            #endregion
        }
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmAppLogic_ActionSPC // xxx Start Pause Cancel
        // Action Buttons
        private void AppStart(object sender, RoutedEventArgs eRea) {
            iAppStart = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // Start the File Import
            RunAction = RunStart;
            RunMetric = RunState;
            RunTense = RunTense_Do;
            omCo.RunAction = RunAction;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense;
            // LocalRunning = bYES;
            AppActionDo();
            // return iAppStart;
        }
        // TODO AppLogic_ - Cancel Button Click - Do Cancel
        private void AppCancel(object sender, RoutedEventArgs eRea) {
            iAppCancel = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // Cancel the File Import
            RunAction = RunCancel;
            RunMetric = RunState;
            RunTense = RunTense_Do;
            omCo.RunAction = RunAction;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense;
            AppActionDo();
        }
        private void AppPause(object sender, RoutedEventArgs eRea) {
            iAppPause = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // Pause the File Import
            RunAction = RunPause;
            RunMetric = RunState;
            RunTense = RunTense_Do;
            omCo.RunAction = RunPause;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense;
            if (omCo.iaRunActionState[RunPause, RunState] == RunTense_Did) {
                RunTense = RunTense_Off;
            } else {
                RunTense = RunTense_Do;
            }
            omCo.RunTense = RunTense;
            AppActionDo();
        }
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmAppLogic_ActionDo
        private void AppActionDo() {
            iAppActionDo = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            int MethodRunAction = RunAction;
            int MethodRunMetric = RunMetric;
            int MethodRunTense = RunTense;
            //
            iAppActionWaitCounter = 0;
            // System.Windows.Media.Brush iTempColor = (System.Windows.Media.Brush)new object();
            int iTempColor = icColorLightBlue;
            bImportProgressBarColorChanged = true;
            MdmControlCheckColor();
            // Action the File Import
            if (bYES == bYES) {
                // StatusLineMdmText2 = "";
                // StatusLineMdmText1TextAdd(this, sRunActionVerb[MethodRunAction] + " Import Tld Process File" + "\n");
                // StatusLine1.Text = "";
                //
                // TODO Are you sure?
                if (bYES == bYES) {
                    // AnswerYes:
                    //
                    StatusLineMdmText2TextAdd(this, sRunActionDoing[MethodRunAction] + " Import Tld Process File! Please wait..." + "\n");
                    if (iConsoleVerbosity >= 4) {
                        ConsoleCommandMdmTextAdd(this, sRunActionDoing[MethodRunAction] + " Import Process " + sRunActionVerb[MethodRunAction] + "..." + "\n");
                    }
                    //
                    switch (MethodRunAction) {
                        case(RunStart):
                            iTempColor = icColorGreen;
                            RoutedEventArgs eRea = new RoutedEventArgs();
                            AppStartDoMain(this, eRea);
                            break;
                        case(RunPause):
                            iTempColor = icColorYellow;
                            LocalLongResult = omCo.AppPauseProcessing();
                            break;
                        case(RunCancel):
                            iTempColor = icColorRed;
                            LocalLongResult = omCo.AppCancelProcessing();
                            break;
                        default:
                            break;
                    }
                    // ProgressBarMdm1.Foreground = (System.Windows.Media.Brush)MdmControlColorGet(iTempColor);
                    bImportProgressBarColorChanged = true;
                    MdmControlCheckColor();
                    //
                    ProgressBarMdm1.InvalidateVisual();
                    this.InvalidateVisual();
                    this.BringIntoView();
                    //
                    if (bYES == bNO) {
                        string sRunActionRequest = "$" + sRunActionVerb[MethodRunAction];
                        ProgressChangedEventArgs ePcea = new ProgressChangedEventArgs(0, (object)sRunActionRequest);
                        UpdatePageUiProgressHandler oUpuph = new UpdatePageUiProgressHandler(omVe.backgroundWorker_ProgressChanged);
                        object[] oTemp = { this, ePcea };
                        //
                        this.Dispatcher.Invoke(
                            oUpuph,
                            System.Windows.Threading.DispatcherPriority.Normal,
                            oTemp
                        );
                    }
                    //
                    switch (MethodRunAction) {
                        case (RunStart):
                            StartButtonPressed.IsEnabled = bNO;
                            CancelButtonPressed.IsEnabled = bNO;
                            PauseButtonPressed.IsEnabled = bNO;
                            if (omCo.iaRunActionState[RunRunDo, RunState] == RunTense_Doing) { 
                                return; 
                            }
                            break;
                        case (RunPause):
                            if (omCo.iaRunActionState[RunPause, RunState] != RunTense_Did) {
                                omCo.bRunPausePending = bYES;
                                omCo.iaRunActionState[RunPause, RunState] = RunTense_Do;
                                CancelButtonPressed.IsEnabled = bNO;
                                StartButtonPressed.IsEnabled = bNO;
                            } else {
                                omCo.bRunPausePending = bNO;
                                omCo.iaRunActionState[RunPause, RunState] = RunTense_Off;
                            }
                            PauseButtonPressed.IsEnabled = bYES;
                            break;
                        case (RunCancel):
                            omCo.bRunCancelPending = bYES;
                            omCo.bRunAbort = bYES;
                            PauseButtonPressed.IsEnabled = bNO;
                            StartButtonPressed.IsEnabled = bNO;
                            // omVe.CallerAsynchronousEventsCancelClick(this, new RoutedEventArgs());
                            break;
                        default:
                            break;
                    }
                    //
                    if (bYES == bNO) {
                        System.Threading.Thread.Sleep(1000);
                        iAppActionWaitCounter = 1000;
                    }
                    if (bYES == bNO) {
                        double dFlashAdjust = (ProgressBarMdm1.Maximum / 100 * 30);
                        int iWaitMilliIncrement = 250;
                        int iWaitMilliIncrementMax = 60000;
                        bool bTempContinue = bYES;
                        int iDivRem = 0;
                        bImportProgressBarColorChanged = true;
                        MdmControlCheckColor();
                        //
                        while (bTempContinue && iAppActionWaitCounter < iWaitMilliIncrementMax) {
                            System.Threading.Thread.Sleep(iWaitMilliIncrement);
                            Math.DivRem(iAppActionWaitCounter, 3000, out iDivRem);
                            if (iDivRem == 0) {
                                if (bImportProgressBarColored) {
                                    // ProgressBarMdm1.Foreground = System.Windows.Media.Brushes.White;
                                    // ProgressBarMdm1.Background = System.Windows.Media.Brushes.Yellow;
                                    bImportProgressBarColored = bNO;
                                    bImportProgressBarColorChanged = true;
                                    MdmControlCheckColor();
                                    // ProgressBarMdm1.Value += dFlashAdjust;
                                } else {
                                    // ProgressBarMdm1.Foreground = (System.Windows.Media.Brush)MdmControlColorGet(iTempColor);
                                    // ProgressBarMdm1.Background = System.Windows.Media.Brushes.Yellow;
                                    bImportProgressBarColored = bYES;
                                    bImportProgressBarColorChanged = true;
                                    MdmControlCheckColor();
                                    // ProgressBarMdm1.Value -= dFlashAdjust;
                                }
                                //
                                if (iAppActionWaitCounter > 0) { StatusLineMdmText2TextAdd(this, "."); }
                            }
                            switch (MethodRunAction) {
                                case (RunStart):
                                    if (!omCo.bRunStartPending) { bTempContinue = bNO; }
                                    break;
                                case (RunPause):
                                    if (!omCo.bRunPausePending) { bTempContinue = bNO; }
                                    break;
                                case (RunCancel):
                                    if (!omCo.bRunCancelPending) { bTempContinue = bNO; }
                                    break;
                                default:
                                    break;
                            }
                            iAppActionWaitCounter += iWaitMilliIncrement;
                        }
                    }
                    switch (MethodRunAction) {
                        case (RunStart):
                            if (omCo.bRunStartPending) {
                                StartButtonPressed.IsEnabled = bNO;
                                PauseButtonPressed.IsEnabled = bNO;
                                CancelButtonPressed.IsEnabled = bNO;
                                // TODO Error DID NOT START!!!
                            } else {
                                StartButtonPressed.IsEnabled = bNO;
                                PauseButtonPressed.IsEnabled = bYES;
                                CancelButtonPressed.IsEnabled = bYES;
                                //
                                omPm.LocalRunning = bYES;
                                omPm.LocalStarted = bYES;
                            }
                            break;
                        case (RunPause):
                            if (omCo.bRunPausePending) {
                                StartButtonPressed.IsEnabled = bNO;
                                PauseButtonPressed.IsEnabled = bYES;
                                CancelButtonPressed.IsEnabled = bYES;
                                // TODO Error DID NOT START!!!
                            } else {
                                StartButtonPressed.IsEnabled = bNO;
                                PauseButtonPressed.IsEnabled = bYES;
                                CancelButtonPressed.IsEnabled = bYES;
                                //
                                omPm.LocalRunning = bYES;
                                omPm.LocalStarted = bYES;
                            }
                            break;
                        case (RunCancel):
                            if (omCo.bRunCancelPending) {
                                // TODO Error DID NOT CANCEL!!!
                                StartButtonPressed.IsEnabled = bNO;
                                PauseButtonPressed.IsEnabled = bNO;
                                CancelButtonPressed.IsEnabled = bYES;
                            } else {
                                // LocalRunning = bNO;
                                // MinputTldApp.Current.Shutdown();
                                StartButtonPressed.IsEnabled = bYES;
                                PauseButtonPressed.IsEnabled = bNO;
                                CancelButtonPressed.IsEnabled = bNO;
                            }
                            break;
                        default:
                            break;
                    }

                    //
                    // ProgressBarMdm1.Foreground = (System.Windows.Media.Brush)MdmControlColorGet(iTempColor);
                    bImportProgressBarColorChanged = true;
                    MdmControlCheckColor();
                    //ProgressBarMdm1.InvalidateVisual();
                    this.InvalidateVisual();
                    this.BringIntoView();
                    // StatusLineMdmText2TextAdd(this, "\n");
                    //
                } else {
                    // AnswerNo:
                    // StatusLineMdmText1 = "Cancel InputTldProcesFile AnswerNo" + "\n";
                }
            }
        }
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmAppObject // xxxxxxxxxx
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmAppObjectCreation
        internal long AppCoreObjectCreate() {
            iAppCoreObjectCreate = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // Start Up Ui Capable Handler Unit
            // MinputTldApp omAp - Application being run
            if (omAp == null) {
                // omAp = omOb.omAp;
                omAp = MinputTldApp.Current;
            }
            // MinputTldApp omPm - Main or Home Page
            if (omPm == null) {
                // omPm = (MinputTldPageMain)omOb.omPm;
                omPm = this;
            }
            // MinputTldApp omOb - Standard Object
            if (omOb == null) {
                // omOb = (Mobject)omOb.omOb;
                omOb = new Mobject(omAp, omPm);
                iAppCoreObjectCreate = omMa.ApplicationMobjectObjectSet(omOb);
                iAppCoreObjectCreate = omMa.AppPageMainObjectSet(omPm);
                iAppCoreObjectCreate = omMa.ApplicationAppObjectSet(omAp);
                // omhLocalMob = (Mobject)ApplicationMobjectObjectGetFromMaobject();
            }
            // MinputTldApp omPd - Main or Home Page
            if (omPm == null) {
                // omPd = (MinputTldPageMain)omOb.omPd;
                Page2Creation(omOb);
                omPd = omPm.omPd;
                omMa.AppPageDetailObjectSet(omPd);
            }
            // MinputTldApp omCo - Main Process Supervision and Control
            if (omCo == null) {
                // omCo = (Mcontroller)omOb.omCo;
                omCo = new Mcontroller(omOb);
                omMa.ApplicationHandlerObjectSet((object)omCo);
            }
            // MinputTldApp omVe - Main Process performed by omAp
            if (omVe == null) {
                // omVe = (MinputTldThread)omOb.omVe;
                omVe = new MinputTldThread(omOb);
                omMa.ApplicationVerbObjectSet((object)omVe);
                // ApplicationVerbObjectSet((object)omVe);
            }
            // MinputTldApp omMa - Mapplication being run
            if (omMa == null) {
                omMa = (Mapplication)omOb.omMa;
                iAppCoreObjectCreate = omMa.ApplicationMappObjectSet(omMa);
                // omMa = omOb.omMa;
                // omMa = new Mapplication(omOb);
            }
            // MinputTldApp omWt - BgWorkerlication being run
            if (omWt == null) {
                omWt = (MinputTld)omOb.omWt;
                iAppCoreObjectCreate = omMa.ApplicationMbgWorkerObjectSet(omWt);
                // omWt = omOb.omWt;
                // omWt = new BgWorkerlication(omOb);
            }
            // MinputTldApp omvLocalBoard - Main Process performed by omAp
            // TODO if (omCl == null) {
            //    omCl = new Mclipboard1Form1();
            //    omCl.Show();
            //    // MinputTld(omOb);
            //    // omMa.ApplicationBoardObjectSet(omCl);
            //    // ApplicationVerbObjectSet((object)omVe);
            //}
            // MinputTldApp omvLocalBoard - Main Process performed by omAp
            if (omUr == null) {
                omUr = new MurlHist1Form1();
                omUr.Show();
                // MinputTld(omOb);
                // omMa.ApplicationBoardObjectSet(omU);
                // ApplicationVerbObjectSet((object)omVe);
            }
            // Set Local Values
            iAppCoreObjectCreate = ApplicationMobjectObjectSet(omOb);
            //
            iAppCoreObjectCreate = omMa.ApplicationMobjectObjectSet(omOb);
            iAppCoreObjectCreate = omMa.AppPageMainObjectSet(omPm);
            iAppCoreObjectCreate = omMa.AppPageDetailObjectSet(omPd);
            iAppCoreObjectCreate = omMa.ApplicationAppObjectSet(omAp);
            iAppCoreObjectCreate = omMa.ApplicationHandlerObjectSet(omCo);
            iAppCoreObjectCreate = omMa.ApplicationVerbObjectSet(omVe);
            iAppCoreObjectCreate = omMa.ApplicationMobjectObjectSet(omOb);
            iAppCoreObjectCreate = omMa.ApplicationMappObjectSet(omMa);
            iAppCoreObjectCreate = omMa.ApplicationMbgWorkerObjectSet(omWt);
            //
            omPm = (MinputTldPageMain)omMa.AppPageMainObjectGet();
            omPd = (MinputTldPageDetail)omMa.AppPageDetailObjectGet();
            omAp = (Application)omMa.ApplicationAppObjectGet();
            omCo = (Mcontroller)omMa.ApplicationHandlerObjectGet();
            omOb = (Mobject)omMa.ApplicationMobjectObjectGet();
            omVe = (MinputTldThread)omMa.ApplicationVerbObjectGet();
            omMa = (Mapplication)omMa.ApplicationMappObjectGet();
            omWt = (MinputTld)omMa.ApplicationMbgWorkerObjectGet();
            //
            // Store Pages
            //
            if (OutputFileNameLast != OutputFileLine.Text && omPd != null) {
                omCo.AppPageDetailSetDefaults((MinputTldPageMain)omPm, (MinputTldPageDetail)omPd);
            }

            return iAppCoreObjectCreate;
        }
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmAppPageMainObject
        public Page AppPageMainObjectGet() {
            iAppObjectGet = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            return omPm;
        }
        // AppPageMainObjectSet(omPm);
        public long AppPageMainObjectSet(MinputTldPageMain omPmssedP) {
            iAppObjectSet = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omPm = omPmssedP;
            return iAppObjectSet;
        }
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmAppPageControlObjectCreation
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmStandard_PageControlObjectGet
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmStandard_PageControlObjectSet
        #endregion
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmAppMobject
        public Mobject ApplicationMobjectObjectGet() {
            return omOb;
        }
        public long ApplicationMobjectObjectSet(Mobject omPmssedO) {
            iApplicationMobjectObjectSet = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectOK;
            if (omOb == null) {
                if (omPmssedO != null) {
                    omOb = (Mobject)omPmssedO;
                    omAp = (Application)omOb.omAp;
                    omPm = (MinputTldPageMain)omOb.omPm;
                    omPd = (MinputTldPageDetail)omOb.omPd;
                    omCo = (Mcontroller)omOb.omCo;
                    omVe = (MinputTldThread)omOb.omVe;
                    omMa = (Mapplication)omOb.omMa;
                    omWt = (MinputTld)omMa.ApplicationMbgWorkerObjectGet();
                }
            }
            return iApplicationMobjectObjectSet;
        }
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmAppCoreObjectSet
        public long AppCoreObjectSet(Mobject omPmssedO) {
            iAppCoreObjectSet = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            if (omPmssedO == null) {
                iAppCoreObjectSet = AppCoreObjectCreate();
                if (omOb != null) {
                    omPmssedO = omOb;
                }
            }
            // MinputTldApp omO
            if (omOb == null) {
                if (omPmssedO == null) {
                    omOb = new Mobject();
                    omOb = omPmssedO;
                }
            }
            iApplicationMobjectObjectSet = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            iApplicationMobjectObjectSet = omMa.ApplicationMobjectObjectSet(omOb);
            iApplicationMobjectObjectSet = omMa.ApplicationAppObjectSet(omAp);
            iApplicationMobjectObjectSet = omMa.AppPageMainObjectSet(omPm);
            iApplicationMobjectObjectSet = omMa.ApplicationHandlerObjectSet(omCo);
            iApplicationMobjectObjectSet = omMa.ApplicationVerbObjectSet(omVe);
            iApplicationMobjectObjectSet = omMa.ApplicationMappObjectSet(omMa);
            iAppCoreObjectCreate = omMa.ApplicationMappObjectSet(omWt);
            return iAppObjectSet;
        }
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmAppCoreObjectGet
        internal long AppCoreObjectGetFromApp() {
            iAppCoreObjectGetFromApp = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            //
            omOb = (Mobject)omMa.ApplicationMobjectObjectGet();
            omAp = (Application)omMa.ApplicationAppObjectGet();
            omPm = (MinputTldPageMain)omMa.AppPageMainObjectGet();
            omPd = (MinputTldPageDetail)omMa.AppPageDetailObjectGet();
            omCo = (Mcontroller)omMa.ApplicationHandlerObjectGet();
            omVe = (MinputTldThread)omMa.ApplicationVerbObjectGet();
            omMa = (Mapplication)omMa.ApplicationMappObjectGet();
            omWt = (MinputTld)omMa.ApplicationMbgWorkerObjectGet();
            //
            return iAppCoreObjectGetFromApp;
        }
        public long AppCoreObjectGet(Mobject omPmssedO) {
            iAppCoreObjectGet = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omOb = (Mobject)omMa.ApplicationMobjectObjectGet();
            omAp = (Application)omMa.ApplicationAppObjectGet();
            omPm = (MinputTldPageMain)omMa.AppPageMainObjectGet();
            omPd = (MinputTldPageDetail)omMa.AppPageDetailObjectGet();
            omCo = (Mcontroller)omMa.ApplicationHandlerObjectGet();
            omVe = (MinputTldThread)omMa.ApplicationVerbObjectGet();
            omMa = (Mapplication)omMa.ApplicationMappObjectGet();
            omWt = (MinputTld)omMa.ApplicationMbgWorkerObjectGet();
            return iAppObjectSet;
        }
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmStandard_Io
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmStandard_IoObjectCreation
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmStandard_IoObjectGet
        #endregion
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmStandard_PageControlObjectSet
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Srt.InputTld MinputTldPageMain ConsoleMdm // xxxxxxxxxx
        #region $include Mdm.Oss.Mapp Mapplication ConsoleMdmDeclarations
        #region ConsoleControlFlags
        // Std_I0_Console
        public bool ConsoleOn = bOFF;
        public bool ConsoleToDisc = bOFF;
        // Text Areas 0 - 5
        // TODO Text Area 0 - TODO
        // Text Area 1 - Summary Progress, Messages, Errors and Help, ToolTip
        // Text Area 2 - Detailed Progress
        // TODO Text Area 3 - Help - What is it
        // TODO Text Area 4 - Help - How do I do this
        // TODO Text Area 5 - Help and Status - Procedure and Event Sequence
        public bool ConsoleTextOn = bON;
        public bool ConsoleText0On = bOFF;
        public bool ConsoleText1On = bON;
        public bool ConsoleText2On = bON;
        public bool ConsoleText3On = bOFF;
        public bool ConsoleText4On = bOFF;
        public bool ConsoleText5On = bOFF;
        //
        public int iConsoleVerbosity = 3;
        // <Area Id = "ConsolePickConsole">
        public bool ConsolePickConsoleOn = bOFF;
        public bool ConsolePickConsoleBasicOn = bON;
        public bool ConsolePickConsoleToDisc = bOFF;
        // Display
        #endregion
        #region ConsoleOutput
        // Display
        public string sConsoleOutput;
        public string sConsoleOutputLog = "";
        public string sConsolePickConsoleOutput;
        public string sConsolePickConsoleOutputLog = "";
        // public sConsolePickConsoleTextBlock;
        public string sConsolePickConsoleTextBlock; // text block
        public int iConsolePickConsoleTextPositionX;
        public int iConsolePickConsoleTextPositionY;
        public int iConsolePickConsoleTextPositionZ;
        public System.Windows.Point zConsolePickConsoleTextPositionOrigin;
        //
        #endregion
        #region ConsoleTextMessageOutput
        public string sMessageText = "";
        public string sMessageText0 = "";
        public string sMessageText1 = "";
        public string sMessageText2 = "";
        public string sMessageText3 = "";
        public string sMessageText4 = "";
        public string sMessageText5 = "";
        //
        public string sMessageStatusAction = "";
        public string sProcessStatusAction = "";
        //
        #endregion
        #region ConsoleMessageTarget
        public string sMessageStatusTarget = "";
        public double dProcessStatusTarget = 0;
        public int iProcessStatusTarget = 0;
        //
        public int iMessageStatusSubTarget = 0;
        public double dProcessStatusSubTarget = 0;
        public int iProcessStatusSubTarget = 0;
        //
        public int iProcessStatusTargetState = 0;
        //
        public double dMessageBoxActualWidthCurrent = 0;
        public dMessageBoxPadding dMessageBoxActualPadding;
        //
        public struct dMessageBoxPadding {
            internal double dLeft;
            internal double dTop;
            internal double dRight;
            internal double dBottom;
            //
            public dMessageBoxPadding(
                double dL,
                double dT,
                double dR,
                double dB) {
                dLeft = dL;
                dRight = dR;
                dTop = dT;
                dBottom = dB;
            }
        }
        //
        public double dMessageWidthCurrent = 0;
        public double dProgressBarMdm1Property = 0;
        public double dMessageProperty2 = 0;
        #endregion
        #region ConsoleCommand
        // <Area Id = "ConsoleCommand>
        public string sConsolePickConsoleCommand;
        public string sCommandLineRequest;
        public int iCommandLineRequest = 0;
        public string sConsoleCommand;
        #endregion
        #region Console properties
        private int ipConsoleMdmId = 99999;
        public int ConsoleMdmId { get { return ipConsoleMdmId; } set { ipConsoleMdmId = value; } }
        private string spConsoleMdmName = "unknown";
        public string ConsoleMdmName { get { return spConsoleMdmName; } set { spConsoleMdmName = value; } }
        private string spConsoleMdmTitle = "unknown";
        public string ConsoleMdmTitle { get { return spConsoleMdmTitle; } set { spConsoleMdmTitle = value; } }
        private int ipConsoleMdmNumber = 99999;
        public int ConsoleMdmNumber {
            get { return ipConsoleMdmNumber; }
            set {
                ipConsoleMdmNumber = value;
            }
        }
        private int ipConsoleMdmStatus = 99999;
        public int ConsoleMdmStatus { get { return ipConsoleMdmStatus; } set { ipConsoleMdmStatus = value; } }
        private string spConsoleMdmStatusText = "unknown";
        public string ConsoleMdmStatusText { get { return spConsoleMdmStatusText; } set { spConsoleMdmStatusText = value; } }
        private int ipConsoleMdmIntResult = 99999;
        public int ConsoleMdmIntResult { get { return ipConsoleMdmIntResult; } set { ipConsoleMdmIntResult = value; } }
        private bool bpConsoleMdmBoolResult = false;
        public bool ConsoleMdmBoolResult { get { return bpConsoleMdmBoolResult; } set { bpConsoleMdmBoolResult = value; } }
        #endregion
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain ConsoleMdmInput // xxxxxxxxxx
        public void StatusLineMdmDisplayPrompt(object sender, string sPassedPrompt) {
            // MdmProcessIntStatusDisplayPromptsPassedPrompt
            /*
            // this = MinputTldPageMain call to MinputTldPageDetail
            // ?? do I have to pass "eTcea" or get it here?

            ExternalFunctionResultsTextBlock.Visibility = Visibility.Visible;

            // Display eArmhResult prompt - this.ExternalFunctionResultsTextBlock

            ExternalFunctionResultsTextBlock.Text = (eTcea != null ? "Accepted" : "Canceled" + "\n");

            // If page function returned, display eArmhResult and data
            if (eTcea != null)
            {
                // Add "Cancelled" to eArmhResult
                ExternalFunctionResultsTextBlock.Text += "\n" + eTcea.Result;
            }
            */
        }
        // AppLogic_ - Events - External - xxxxxxxx
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmStandard_External_Command_ObjectCreation
        // MdmCommandEventArgs
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmStandard_External_Command_ObjectGet
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain MdmStandard_External_Command_ObjectSet
        #endregion
        #endregion
        #region External Events (as in COM / Service / Messages)
        private void InputFileLineExternalChanged(object sender, TextChangedEventArgs eTcea) {
            StatusLineMdmText1TextAdd(this, "External input file name change coming in, please wait..." + "\n");
            this.InvalidateVisual();
        }
        private void OutputFileLineExternalChanged(object sender, TextChangedEventArgs eTcea) {
            StatusLineMdmText1TextAdd(this, "External output file name change coming in, please wait..." + "\n");
            this.InvalidateVisual();
            // * Trace and Log
        }
        #endregion
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain ConsoleMdmOutput // xxxxxxxxxx
        #region $include Mdm.Srt.InputTld MinputTldPageMain ConsoleMdmMessages
        #region $include Mdm.Srt.InputTld MinputTldPageMain ConsoleMdmUi_StatusChanges
        private void StatusLineMdmTextTextChanged(object sender, TextChangedEventArgs eTcea) {
            sTemp = eTcea.ToString();
            StatusLineMdmTextTextChanged(sender, eTcea.ToString());
        }
        public void StatusLineMdmTextTextChanged(object sender, string sPassedText) {
            // * Trace and Log
            // StatusLine1.Text = sPassedText;
            // StatusLine1.ScrollToHome();
            StatusLine1.InvalidateVisual();
        }
        private void StatusLineMdmText2TextChanged(object sender, TextChangedEventArgs eTcea) {
            sTemp = eTcea.ToString();
            StatusLineMdmText2TextChanged(sender, eTcea.ToString());
        }
        public void StatusLineMdmText2TextChanged(object sender, string sPassedText) {
            // * Trace and Log
            // StatusLine2.Text = sPassedText;
            // StatusLine2.ScrollToHome();
            StatusLine2.InvalidateVisual();
        }
        public void StatusLineMdmText3TextChanged(object sender, string sPassedText) {
            // * Trace and Log
            TextBox StatusLine3 = new TextBox();
            // StatusLine3.Text = sPassedText;
            StatusLine3.InvalidateVisual();
        }
        public void StatusLineMdmText4TextChanged(object sender, string sPassedText) {
            // * Trace and Log
            TextBox StatusLine4 = new TextBox();
            // StatusLine4.Text = sPassedText;
            StatusLine4.InvalidateVisual();
        }
        private void ConsoleCommandMdmTextChanged(object sender, TextChangedEventArgs eTcea) {
            // ConsoleCommandText.Text = "External output for display, please wait...";
            ConsoleCommandText.InvalidateVisual();
            // ConsoleCommandText
        }
        public void StatusLineMdmChanged(object sender, ProgressChangedEventArgs ePceaPassed) {
            // StatusLineMdmChangedePceaPassed
            // * Trace and Log
            string sText = ePceaPassed.UserState.ToString();
            StatusLineMdmChanged(sender, sText);
        }
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain ConsoleMdmMessagePageToString
        public override string ToString() {
            MessageToStringPage();
            return base.ToString();
        }
        // Mdm Message thru ToString handling
        public void MessageToStringPage() {   
            if (omOb != null) {
                if (omOb.sMessageToPage != null) {
                    if (omOb.sMessageToPage.Length > 0) {
                        if (omOb.sMessageToPage.Substring(0, 1) == "#") {
                            StatusLineMdmChanged(this, (string)omOb.sMessageToPage.Substring(2));
                            omOb.sMessageToPage = "";
                        }
                    }
                }
            }
        }
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain ConsoleMdmStatusLine
        public void StatusLineMdmTargetGet(string sPassedText) {
            //
            // * Trace and Log
            //
            // C Xxx
            //
            // L 999
            // H 999
            // V 999
            // P 999
            //
            // M 9 Xxx
            // A 9 Xxx
            //
            // R Metric Tense Action
            // R 9 9 999
            // R 9 9 Xxx
            // dMessageBoxActualPadding = 0;
            dMessageBoxActualPadding = new dMessageBoxPadding(195, 0, 0, 0);
            // 
            sProcessStatusAction = "";
            dProcessStatusTarget = 0;
            iProcessStatusTarget = 0;
            dProcessStatusSubTarget = 0;
            iProcessStatusSubTarget = 0;
            iProcessStatusTargetState = 0;
            //
            dMessageBoxActualWidthCurrent = 0;
            dMessageWidthCurrent = 0;
            dProgressBarMdm1Property = 0;
            dMessageProperty2 = 0;
            //
            if (sPassedText.Length > 0) {
                sProcessStatusAction = sPassedText.Substring(0, 1).ToUpper();
                //
                if (sPassedText.Length > 1) {
                    sMessageText1 = sPassedText.Substring(1);
                    try { dProcessStatusSubTarget = Convert.ToInt32(sPassedText.Substring(1, 1)); } catch { dProcessStatusSubTarget = 0; }
                    iProcessStatusSubTarget = (int)dProcessStatusSubTarget;
                    //
                    if (sPassedText.Length > 2) {
                        sMessageText2 = sPassedText.Substring(2);
                        try { iProcessStatusTargetState = Convert.ToInt32(sPassedText.Substring(2, 1)); } catch { ; }
                        try { dProgressBarMdm1Property = Convert.ToDouble(sMessageText1); } catch { ; }
                        // try { dMessageProperty2 = Convert.ToDouble(sMessageText2); } catch { ; }
                        //
                        if (sPassedText.Length > 3) {
                            sTemp = "";
                            try {
                                for (iTemp0 = 3; iTemp0 < sPassedText.Length; iTemp0++) {
                                    iTemp = Convert.ToInt32(sPassedText.Substring(iTemp0,1));
                                    sTemp += iTemp.ToString();
                                }
                            } catch { ; }
                            try { dProcessStatusTarget = Convert.ToDouble(sTemp); } catch { ; }
                            iProcessStatusTarget = (int)dProcessStatusTarget;
                            sMessageText3 = sPassedText.Substring(iTemp0);
                        }
                    }
                } else { sProcessStatusAction = "C"; }
            } else { sProcessStatusAction = "C"; }
        }
        public void StatusLineMdmChanged(object sender, string sPassedText) {
            // StatusLineMdmChangedsPassedText
            sMessageText0 = "";
            sMessageText1 = "";
            sMessageText2 = "";
            sMessageText3 = "";
            sMessageText4 = "";
            sMessageText5 = "";
            //
            StatusLineMdmTargetGet(sPassedText);
            //
            try {
                // Run State Change Handling
                switch (sProcessStatusAction) {
                    case("R"):
                        /*
                        try { RunAction = iProcessStatusTarget; } catch { RunAction = RunTense_Off; }
                        try { RunMetric = iProcessStatusSubTarget; } catch { RunMetric = RunTense_Off; }
                        try { RunTense = iProcessStatusTargetState; } catch { RunTense = RunTense_Off; }
                        */
                        iaRunActionState[iProcessStatusTarget, iProcessStatusSubTarget] = omCo.iaRunActionState[iProcessStatusTarget, iProcessStatusSubTarget];
                        // iaRunActionState[iProcessStatusTarget, iProcessStatusSubTarget] = iProcessStatusTargetState;
                        if (iConsoleVerbosity >= 4) {ConsoleCommandMdmTextAdd(this, "[t" + iProcessStatusTarget.ToString() + "s" + iProcessStatusTarget.ToString() + "eRea" + iProcessStatusTargetState.ToString() + "]"); }
                        # region Run State State / Tense
                        // Run State Tense
                        switch (iProcessStatusTargetState) {
                            case (RunTense_Off):
                                break;
                            case (RunTense_Do):
                                break;
                            case (RunTense_DoNot):
                                break;
                            case (RunTense_Doing):
                                break;
                            case (RunTense_Did):
                                break;
                            case (RunTense_DidNot):
                                break;
                            default:
                                break;
                        }
                        #endregion
                        # region Run State Metrics
                        // Run State Metrics
                        switch (iProcessStatusSubTarget) {
                            case (RunState):
                                break;
                            case (RunState_Last_Update):
                                break;
                            case (RunDoLast_Count):
                                break;
                            case (RunDoCount):
                                break;
                            case (RunDoSkip_Count):
                                break;
                            case (RunDoError_Count):
                                break;
                            case (RunDoWarning_Count):
                                break;
                            case (RunDoRetry_Count):
                                break;
                            default:
                                break;
                        }
                        #endregion
                        # region Run State Action Verb
                        // Run State
                        switch (iProcessStatusTarget) {
                            case (RunCancel):
                                # region Run State State / Tense
                                // Run State Tense
                                switch (iProcessStatusTargetState) {
                                    case (RunTense_Off):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "c"); }
                                        break;
                                    case (RunTense_Do):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "c"); }
                                        break;
                                    case (RunTense_DoNot):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "c"); }
                                        break;
                                    case (RunTense_Doing):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "c"); }
                                        break;
                                    case (RunTense_Did):
                                        PauseButtonPressed.Content = "Pause";
                                        StartButtonPressed.IsEnabled = bYES;
                                        PauseButtonPressed.IsEnabled = bNO;
                                        CancelButtonPressed.IsEnabled = bNO;
                                        StatusLineMdmText2TextAdd(sender, "Processing Cancelled!!!" + "\n");
                                        this.InvalidateVisual();
                                        break;
                                        break;
                                    case (RunTense_DidNot):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "c"); }
                                        break;
                                    default:
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "c"); }
                                        break;
                                }
                                bImportProgressBarColorChanged = true;
                                MdmControlCheckColor();
                                #endregion
                                break;
                            case (RunPause):
                                # region Run State State / Tense
                                // Run State Tense
                                switch (iProcessStatusTargetState) {
                                    case (RunTense_Off):
                                        PauseButtonPressed.Content = "Pause";
                                        StartButtonPressed.IsEnabled = bNO;
                                        PauseButtonPressed.IsEnabled = bYES;
                                        CancelButtonPressed.IsEnabled = bYES;
                                        StatusLineMdmText2TextAdd(sender, "Process resumed..." + "\n");
                                        this.InvalidateVisual();
                                        break;
                                    case (RunTense_Do):
                                        PauseButtonPressed.Content = "Resume";
                                        StartButtonPressed.IsEnabled = bNO;
                                        PauseButtonPressed.IsEnabled = bYES;
                                        CancelButtonPressed.IsEnabled = bYES;
                                        StatusLineMdmText2TextAdd(sender, "Waiting for process to pause..." + "\n");
                                        this.InvalidateVisual();
                                        break;
                                    case (RunTense_DoNot):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "p"); }
                                        break;
                                    case (RunTense_Doing):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "p"); }
                                        break;
                                    case (RunTense_Did):
                                        PauseButtonPressed.Content = "Resume";
                                        StartButtonPressed.IsEnabled = bNO;
                                        PauseButtonPressed.IsEnabled = bYES;
                                        CancelButtonPressed.IsEnabled = bYES;
                                        StatusLineMdmText2TextAdd(sender, "Process paused, click \"Resume\" to continue..." + "\n");
                                        this.InvalidateVisual();
                                        break;
                                    case (RunTense_DidNot):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "pError"); }
                                        break;
                                    default:
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "p"); }
                                        break;
                                }
                                bImportProgressBarColorChanged = true;
                                MdmControlCheckColor();
                                #endregion
                                break;
                            case (RunStart):
                                # region Run State State / Tense
                                // Run State Tense
                                switch (iProcessStatusTargetState) {
                                    case (RunTense_Off):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "s"); }
                                        break;
                                    case (RunTense_Do):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "s"); }
                                        break;
                                    case (RunTense_DoNot):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "s"); }
                                        break;
                                    case (RunTense_Doing):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "s"); }
                                        break;
                                    case (RunTense_Did):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "s"); }
                                        break;
                                    case (RunTense_DidNot):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "s"); }
                                        break;
                                    default:
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "s"); }
                                        break;
                                }
                                bImportProgressBarColorChanged = true;
                                MdmControlCheckColor();
                                #endregion
                                break;
                            case (RunNoOp4):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "n4"); }
                                break;
                            case (RunNoOp5):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "n5"); }
                                break;
                            case (RunInitialize):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "iTemp0"); }
                                break;
                            case (RunRunDo):
                                # region Run State State / Tense
                                // Run State Tense
                                switch (iProcessStatusTargetState) {
                                    case (RunTense_Off):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "d"); }
                                        break;
                                    case (RunTense_Do):
                                        PauseButtonPressed.Content = "Pause";
                                        StartButtonPressed.IsEnabled = bNO;
                                        PauseButtonPressed.IsEnabled = bYES;
                                        CancelButtonPressed.IsEnabled = bYES;
                                        break;
                                    case (RunTense_DoNot):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "d"); }
                                        break;
                                    case (RunTense_Doing):
                                        PauseButtonPressed.Content = "Pause";
                                        StartButtonPressed.IsEnabled = bNO;
                                        PauseButtonPressed.IsEnabled = bYES;
                                        CancelButtonPressed.IsEnabled = bYES;
                                        break;
                                    case (RunTense_Did):
                                        PauseButtonPressed.Content = "Pause";
                                        StartButtonPressed.IsEnabled = bYES;
                                        PauseButtonPressed.IsEnabled = bNO;
                                        CancelButtonPressed.IsEnabled = bNO;
                                        break;
                                        StatusLineMdmText1TextAdd(sender, "Processing Completed." + "\n");
                                        this.InvalidateVisual();
                                    case (RunTense_DidNot):
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "d"); }
                                        break;
                                    default:
                                        if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "d"); }
                                        break;
                                }
                                bImportProgressBarColorChanged = true;
                                MdmControlCheckColor();
                                #endregion
                                break;
                            case (RunUserInput):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "u<"); }
                                break;
                            case (RunOpen):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "o"); }
                                break;
                            case (RunMain_Do):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "md"); }
                                break;
                            case (RunMain_DoSelect):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "ms"); }
                                break;
                            case (RunMain_DoLock_Add):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "mll"); }
                                break;
                            case (RunMain_DoRead):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "mr"); }
                                break;
                            case (RunMain_DoValidate):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "mv"); }
                                break;
                            case (RunMain_DoAccept):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "ma"); }
                                break;
                            case (RunMain_DoReport):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "m>"); }
                                break;
                            case (RunMain_DoProcess):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "mp"); }
                                break;
                            case (RunMain_DoUpdate):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "mu"); }
                                break;
                            case (RunMain_DoWrite):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "mw"); }
                                break;
                            case (RunMain_DoLock_Remove):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "mlr"); }
                                break;
                            case (RunClose):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "mc"); }
                                break;
                            case (RunFinish):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "f"); }
                                if (iConsoleVerbosity >= 1) {
                                    StatusLineMdmText2TextAdd(sender, "Processing finished." + "\n");
                                    this.InvalidateVisual();
                                }
                                break;
                            case (RunAbort):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "a"); }
                                break;
                            case (RunReloop):
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, @"^"); }
                                break;
                            case (RunFirst):
                                bImportProgressBarColorChanged = true;
                                MdmControlCheckColor();
                                if (iConsoleVerbosity >= 4) {ConsoleCommandMdmTextAdd(this, "m1");}
                                break;
                            default:
                                if (RunAction == RunOptionX) { ; }
                                if (RunAction == RunOptionY) { ; }
                                if (RunAction == RunOptionZ) { ; }
                                if (RunAction == RunOption1) { ; }
                                if (RunAction == RunOption2) { ; }
                                if (iConsoleVerbosity >= 4) { ConsoleCommandMdmTextAdd(this, "z" + RunAction.ToString()); }
                                break;
                        }
                        #endregion
                        break;
                    case("X"):
                        // start
                        break;
                    case("Y"):
                        // pause
                        break;
                    case("Z"):
                        // cancel
                        break;
                    case ("L"):
                        ProgressBarMdm1.Minimum = dProgressBarMdm1Property;
                        break;
                    case ("H"):
                        ProgressBarMdm1.Maximum = dProgressBarMdm1Property;
                        break;
                    case ("V"):
                        ProgressBarMdm1.Value = dProgressBarMdm1Property;
                        bImportProgressBarColorChanged = true;
                        MdmControlCheckColor();
                        break;
                    case ("P"):
                        /*
                        if (dProgressBarMdm1Property > 0 && ProgressBarMdm1.Maximum > 0) {
                            ProgressBarMdm1.Value = ProgressBarMdm1.Maximum / dProgressBarMdm1Property * 100;
                        } else if (dProgressBarMdm1Property > 0) {
                            ProgressBarMdm1.Value = ProgressBarMdm1.Maximum;
                        } else {
                            ProgressBarMdm1.Value = 0;
                        }
                        */
                        break;
                    case ("E"):
                    case ("C"):
                        if (sProcessStatusAction == "C" && !ConsolePickConsoleOn) { return; }
                        ConsoleCommandMdmTextAdd(sender, sMessageText1);
                        dConsoleDisplayCount += 1;
                        dConcoleDisplayAdjustCount += 1;
                        if (dConsoleWidthHigh == 0) { dConsoleWidthHigh = this.ActualWidth; }
                        if (dConsoleWidthLow == 0) { dConsoleWidthLow = this.ActualWidth; }
                        //
                        dMessageWidthCurrent = sMessageText1.Length;
                        // dMessageBoxActualWidthCurrent = this.ActualWidth - 195;
                        dMessageBoxActualWidthCurrent -= (dMessageBoxActualPadding.dLeft + dMessageBoxActualPadding.dRight);
                        if (dMessageWidthCurrent > dMessageBoxActualWidthCurrent) {
                            if (dMessageWidthCurrent > dConsoleWidthHigh) {
                                dConsoleWidthHigh = dMessageWidthCurrent;
                            }
                        }
                        if (dMessageWidthCurrent < dMessageBoxActualWidthCurrent) {
                            if (dMessageWidthCurrent > dConsoleWidthLow) {
                                dConsoleWidthLow = dMessageWidthCurrent;
                            }
                        }
                        if (dConcoleDisplayAdjustCount > dConcoleDisplayAdjustCountMax) {
                            if (dConsoleWidthHigh > dMessageBoxActualWidthCurrent) {
                                PageSizeChangedDoAdjust((Page)this, (dConsoleWidthHigh + 195), 0);
                            } else if (dConsoleWidthLow < dMessageBoxActualWidthCurrent) {
                                PageSizeChangedDoAdjust((Page)this, (dConsoleWidthLow + 195), 0);
                            }
                            dConcoleDisplayAdjustCount = 0;
                            dConsoleWidthHigh = dMessageWidthCurrent / 2;
                            dConsoleWidthLow = dConsoleWidthHigh;
                        }
                        //
                        break;
                    case ("A"):
                        if (!ConsoleTextOn) { return; }
                        if (sMessageText2.Length == 0) {
                            sTemp3 = "";
                        }
                        iProcessStatusTarget = iProcessStatusSubTarget;
                        switch (iProcessStatusTarget) {
                            case (2):
                                if (!ConsoleText2On) { return; }
                                StatusLineMdmText2TextAdd(sender, sMessageText2);
                                break;
                            case (3):
                                if (!ConsoleText3On) { return; }
                                StatusLineMdmText3TextAdd(sender, sMessageText2);
                                break;
                            case (4):
                                if (!ConsoleText4On) { return; }
                                StatusLineMdmText4TextAdd(sender, sMessageText2);
                                break;
                            case (1):
                            default:
                                if (!ConsoleTextOn) { return; }
                                StatusLineMdmText1TextAdd(sender, sMessageText2);
                                break;
                        }
                        break;
                    case ("M"):
                    default:
                        if (!ConsoleTextOn) { return; }
                        iProcessStatusTarget = iProcessStatusSubTarget;
                        switch (iProcessStatusTarget) {
                            case (2):
                                if (!ConsoleText2On) { return; }
                                StatusLineMdmText2TextChanged(sender, sMessageText2);
                                break;
                            case (3):
                                if (!ConsoleText3On) { return; }
                                StatusLineMdmText3TextChanged(sender, sMessageText2);
                                break;
                            case (4):
                                if (!ConsoleText4On) { return; }
                                StatusLineMdmText4TextChanged(sender, sMessageText2);
                                break;
                            case (1):
                            default:
                                if (!ConsoleTextOn) { return; }
                                StatusLineMdmTextTextChanged(sender, sMessageText2);
                                break;
                        }
                        break;
                }
            } catch {
                ConsoleCommandMdmTextAdd(sender, "Error!!!! Invalid message" + "\n");
                ConsoleCommandMdmTextAdd(sender, "From " + sender.ToString() + " [" + sPassedText + "]" + "\n");
                omCo.RunErrorDidOccur = true;
                bImportProgressBarColorChanged = true;
                MdmControlCheckColor();
            };
            // TODO NEED TO SEND AND "E" Error MESSAGE FROM IMPORT
            // if (omCo.RunErrorDidOccur) {
                // this.BringIntoView();
            // }
        }
        public void StatusLineMdmText1TextAdd(object sender, string sPassedText) {
            // * Trace and Log
            if (true == bNO) {
//                StatusLine1.Text = sPassedText + "\n" + StatusLine1.Text;
                StatusLine1.Text = sPassedText + StatusLine1.Text;
                while (StatusLine1.Text.Length > 3000) {
                    StatusLine1.Text = StatusLine1.Text.Substring(0,2500);
                }
                StatusLine1.ScrollToHome();
            } else {
                // if (StatusLine1.Text.Length > 0) {
                    // StatusLine1.Text += sPassedText + "\n";
                // }
                StatusLine1.Text += sPassedText;
                while (StatusLine1.Text.Length > 3000) {
                    StatusLine1.Text = StatusLine1.Text.Substring(500);
                }
                StatusLine1.ScrollToEnd();
            }
            if (omCo.RunErrorDidOccur) {
                this.BringIntoView();
            }
        }
        public void StatusLineMdmText2TextAdd(object sender, string sPassedText) {
            // * Trace and Log
            if (true == false) {
                // StatusLine2.Text = sPassedText + "\n" + StatusLine2.Text;
                StatusLine2.Text = sPassedText + "\n" + StatusLine2.Text;
                while (StatusLine2.Text.Length > 3000) {
                    StatusLine2.Text = StatusLine2.Text.Substring(0, 2500);
                }
                StatusLine2.ScrollToHome();
            } else {
                // if (StatusLine2.Text.Length > 0) {
                   //  StatusLine2.Text += sPassedText + "\n";
                // }
                StatusLine2.Text += sPassedText;
                while (StatusLine2.Text.Length > 3000) {
                    StatusLine2.Text = StatusLine2.Text.Substring(500);
                }
                StatusLine2.ScrollToEnd();
            }
            if (omCo.RunErrorDidOccur) {
                this.BringIntoView();
            }
        }
        public void StatusLineMdmText3TextAdd(object sender, string sPassedText) {
            // * Trace and Log
            if (true == false) {
                // StatusLine3.Text = sPassedText + "\n" + StatusLine3.Text;
                StatusLine3.Text = sPassedText + "\n" + StatusLine3.Text;
                while (StatusLine3.Text.Length > 3000) {
                    StatusLine3.Text = StatusLine3.Text.Substring(0, 2500);
                }
                StatusLine3.ScrollToHome();
            } else {
                // if (StatusLine3.Text.Length > 0) {
                   //  StatusLine3.Text += sPassedText + "\n";
                // }
                StatusLine3.Text += sPassedText;
                while (StatusLine3.Text.Length > 3000) {
                    StatusLine3.Text = StatusLine3.Text.Substring(500);
                }
                StatusLine3.ScrollToEnd();
            }
            if (omCo.RunErrorDidOccur) {
                this.BringIntoView();
            }
        }
        public void StatusLineMdmText4TextAdd(object sender, string sPassedText) {
            // * Trace and Log
            if (true == false) {
                // StatusLine4.Text = sPassedText + "\n" + StatusLine4.Text;
                StatusLine4.Text = sPassedText + StatusLine4.Text;
                while (StatusLine4.Text.Length > 3000) {
                    StatusLine4.Text = StatusLine4.Text.Substring(0, 2500);
                }
                StatusLine4.ScrollToHome();
            } else {
                // if (StatusLine4.Text.Length > 0) {
                   //  StatusLine4.Text += sPassedText + "\n";
                // }
                StatusLine4.Text += sPassedText;
                while (StatusLine4.Text.Length > 3000) {
                    StatusLine4.Text = StatusLine4.Text.Substring(500);
                }
                StatusLine4.ScrollToEnd();
            }
            if (omCo.RunErrorDidOccur) {
                this.BringIntoView();
            }
        }
        public void ConsoleCommandMdmTextAdd(object sender, string sPassedText) {
            // * Trace and Log
            if (true == false) {
                // ConsoleCommandText.Text = sPassedText + "\n" + ConsoleCommandText.Text;
                ConsoleCommandText.Text = sPassedText + ConsoleCommandText.Text;
                while (ConsoleCommandText.Text.Length > 10000) {
                    ConsoleCommandText.Text = ConsoleCommandText.Text.Substring(0,9500);
                }
                ConsoleCommandText.ScrollToHome();
            } else {
                // if (ConsoleCommandText.Text.Length > 0) {
                   //  ConsoleCommandText.Text += sPassedText + "\n";
                // }
                ConsoleCommandText.Text += sPassedText;
                while (ConsoleCommandText.Text.Length > 10000) {
                    ConsoleCommandText.Text = ConsoleCommandText.Text.Substring(500);
                }
                ConsoleCommandText.ScrollToEnd();
            }
            ConsoleCommandText.InvalidateVisual();
            if (omCo.RunErrorDidOccur) {
                this.BringIntoView();
            }
        }
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain ConsoleMdmBox_TextAdd
        private void StatusLineMdmTextSet(int iPassedTarget, int iPassedIntResult, string sPassedStringText, string sPassedStringPrefix, string sPassedStringSuffex, bool bPassedNewLine) {
            StatusLineMdmText3 = "";
            if (sPassedStringPrefix.Length > 0) {
                StatusLineMdmText3 += sPassedStringPrefix + " ";
            }
            if (sPassedStringSuffex.Length > 0) {
                StatusLineMdmText3 += sPassedStringText + " " + sPassedStringSuffex;
            }
            if (iPassedIntResult != 0) {
                StatusLineMdmText3 += " (" + iPassedIntResult + ")";
            }
            if (bPassedNewLine) {
                StatusLineMdmText3 += "\n";
            }
            switch (iPassedTarget) {
                case 99999:
                    StatusLineMdmText2TextAdd(this, "Null start");
                    break;
                case 1:
                    // if (StatusLineMdmText1.Length > 0) {
                    // StatusLineMdmText1 += "\n";
                    // }
                    StatusLineMdmText1TextAdd(this, StatusLineMdmText3);
                    break;
                case 2:
                    /*
                    if (StatusLineMdmText2.Length > 0) {
                        StatusLineMdmText2 = StatusLineMdmText3 + "\n" + StatusLineMdmText2;
                    } else {
                        StatusLineMdmText2 = StatusLineMdmText3;
                    }
                    */
                    StatusLineMdmText2TextAdd(this, StatusLineMdmText3);
                    break;
                case 3:
                    StatusLineMdmText2TextAdd(this, StatusLineMdmText3);
                    break;
                case 4:
                    StatusLineMdmText2TextAdd(this, StatusLineMdmText3);
                    break;
                default:
                    /*
                    if (StatusLineMdmText2.Length > 0) {
                        StatusLineMdmText2 += "\n";
                    }
                    */
                    StatusLineMdmText2TextAdd(this, StatusLineMdmText3);
                    break;
            }
            // StatusLine2.Text = StatusLineMdmText2;
        }
        #endregion
        #region $include Mdm.Srt.InputTld MinputTld1 ConsoleMdmDeclarations_Set
        public void ConsoleMdmControlSet() {
            // Std_I0_Console
            ConsoleOn = bON;
            ConsoleToDisc = bOFF;
            //
            iConsoleVerbosity = 3;
            // Display
            sConsoleOutput = "";
            sConsoleOutputLog = "";
            // <Area Id = "ConsolePickConsole">
            ConsolePickConsoleOn = bON;
            ConsolePickConsoleBasicOn = bON;
            ConsolePickConsoleToDisc = bOFF;
            // Display
            sConsolePickConsoleOutput = "";
            sConsolePickConsoleOutputLog = "";
            sConsolePickConsoleCommand = "";
            // public sConsolePickConsoleTextBlock;
            sConsolePickConsoleTextBlock = ""; // text block
            iConsolePickConsoleTextPositionX = 0;
            iConsolePickConsoleTextPositionY = 0;
            iConsolePickConsoleTextPositionZ = 0;
            // zConsolePickConsoleTextPositionOrigin;
            // Text Areas 0 - 5
            // TODO Text Area 0 - TODO
            // Text Area 1 - Summary Progress, Messages, Errors and Help, ToolTip
            // Text Area 2 - Detailed Progress
            // TODO Text Area 3 - Help - What is it
            // TODO Text Area 4 - Help - How do I do this
            // TODO Text Area 5 - Help and Status - Procedure and Event Sequence
            ConsoleTextOn = bON;
            ConsoleText0On = bOFF;
            ConsoleText1On = bON;
            ConsoleText2On = bON;
            ConsoleText3On = bOFF;
            ConsoleText4On = bOFF;
            ConsoleText5On = bOFF;
            //
            sMessageText = "";
            sMessageText0 = "";
            sMessageText1 = "";
            sMessageText2 = "";
            sMessageText3 = "";
            sMessageText4 = "";
            sMessageText5 = "";
            //
            sMessageStatusAction = "";
            sProcessStatusAction = "";
            //
            sMessageStatusTarget = "";
            dProcessStatusTarget = 0;
            iProcessStatusTarget = 0;
            //
            iMessageStatusSubTarget = 0;
            dProcessStatusSubTarget = 0;
            iProcessStatusSubTarget = 0;
            //
            iProcessStatusTargetState = 0;
            //
            dMessageBoxActualWidthCurrent = 0;
            dMessageWidthCurrent = 0;
            dProgressBarMdm1Property = 0;
            dMessageProperty2 = 0;
            // <Area Id = "ConsoleCommand>
            sCommandLineRequest = "";
            iCommandLineRequest = 0;
            sConsoleCommand = "";
        }
        #endregion
        #region ApplicationLogicStatusDiaplay
        private void ProgressBarMdm1Initialized(object sender, EventArgs e) {
            // oPbThisProgressBarMdm1 = ProgressBarMdm1;
            ProgressBarMdm1.Maximum = 10;
            // ProgressBarMdm1.Maximum = 200;
            // ProgressBarMdm1.Location = new System.Drawing.Point(8, 312);
            // ProgressBarMdm1.Step = ?;
            ProgressBarMdm1.Minimum = 0;
            ProgressBarMdm1.TabIndex = 0;
            ProgressBarMdm1.Value = 0;
            this.InvalidateVisual();
        }

        private void ProgressBarMdm1Changed(object sender, EventArgs e) {
            this.InvalidateVisual();
        }

        public void ProgressBarMdmCompletionChanged(object sender, string sField, int iAmount, int iMax) {
            //
            // todo here
            ProgressBarMdm1.Value = (int)(iAmount / iMax);
            EventArgs e = new EventArgs();
            //
            ProgressBarMdm1Changed(sender, e);
        }


        #endregion
        #endregion
        // xxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MfileStatusResultTextGetString
        private string MfileStatusResultTextGetString(int PassedIntResult) {
            string sResult = "";
            switch (PassedIntResult) {
                case ((int)Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.ResultUndefined):
                    sResult = "Null start";
                    break;
                case ((int)Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.ResultMissingName):
                    sResult = "File must have a value";
                    break;
                case ((int)Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.ResultDoesNotExist):
                    sResult = "File not found";
                    break;
                case ((int)Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.ResultShouldNotExist):
                    sResult = "File already exists";
                    break;
                case ((int)Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.FileIoRowIdDoesNotExist):
                    sResult = "Item Id not found";
                    break;
                case ((int)Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.FileIoRowIdDoesExist):
                    sResult = "Item Id already exists";
                    break;
                default:
                    sResult = "Unknown error" + " (" + PassedIntResult + ")";
                    break;
            }
            StatusLineMdmText1TextAdd(this, "\n" + sResult);
            return sResult;
        }
        #endregion
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Srt.InputTld MinputTldPageMain ProgressBarMdmUi_Bar // xxxxxxxxxx
        #region $include Mdm.Srt.InputTld MinputTldPageMain ProgressBarMdmUi_Bar_Declarations
        //
        ProgressBar opLocalProgressBar;
        public double opLocalProgressBar_Minimum = 0;
        public double opLocalProgressBar_Maximum = 0;
        public double opLocalProgressBar_Value = 0;
        public int opLocalProgressBar_Display = 0;
        //
        ProgressChangedEventArgs eaOpcLocalRunProgressChanged;
        RoutedPropertyChangedEventArgs<double> eadRpcLocalRunProgressChanged;
        RunWorkerCompletedEventArgs eaRwcLocalRunCompleted;
        double eadRpcLocalOldValue = 0;
        double eadRpcLocalNewValue = 0;
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain ProgressBarMdmUi_Bar_Changes
        public void ProgressBarMdm1ProcessChanged(object sender, RoutedPropertyChangedEventArgs<double> eRpceaPassedDouble) {
            // iProgressBarMdm1ChangedeRpceaPassedDouble
            double o = eRpceaPassedDouble.OldValue;
            double n = eRpceaPassedDouble.NewValue;
            double dIpb1Min = ProgressBarMdm1.Minimum;
            double dIbp1Max = ProgressBarMdm1.Maximum;
            double dIbp1Val = ProgressBarMdm1.Value;
            //
            ProgressBarMdm1.Value = eRpceaPassedDouble.NewValue;
        }
        public void ProgressBarMdm1ProcessChanged(object sender, ProgressChangedEventArgs ePceaPassed) {
            // MdmProcessProgressBarMdm1ChangedePceaPassed
            // * Trace and Log, change display text inside control?
            try {
                string sPassedText = ePceaPassed.UserState.ToString();
                // StatusLine1.Text = sPassedText;
                // StatusLine1.ScrollToHome();
                // StatusLine1.InvalidateVisual();
                ProgressBarMdm1.Value = ePceaPassed.ProgressPercentage;
                // ProgressBarMdm1ProcessChanged(sender, ePceaPassed);
                ProgressBarMdm1.InvalidateVisual();
            } catch { ; };
        }
        public void ProgressBarMdm1ProcessChanged(object sender, double dPassedValue) {
            // MdmProcessProgressBarMdm1ChangeddPassedValue
            // * Trace and Log
            string sPassedText = dPassedValue.ToString();
            ProgressBarMdm1.Value = dPassedValue;
            // ProgressBarMdm1ProcessChanged(sender, ePceaPassed);
            ProgressBarMdm1.InvalidateVisual();
        }
        #endregion
        #region $include Mdm.Srt.InputTld MinputTldPageMain ProgressBarMdmUi_Bar_Class
        public int iColorOfBar = icColorLightBlue;
        public const int icColorGreen = 1;
        public const int icColorBlue = 2;
        public const int icColorRed = 3;
        public const int icColorYellow = 4;
        public const int icColorLightBlue = 5;
        public const int icColorWhite = 6;
        //
        public System.Windows.Media.Brush MdmControlColorGet(int iPassedColorOfBar) {
            iColorOfBar = iPassedColorOfBar;
            switch (iPassedColorOfBar) {
                case (icColorGreen):
                    return System.Windows.Media.Brushes.Green;
                    break;
                case (icColorBlue):
                    return System.Windows.Media.Brushes.Blue;
                    break;
                case (icColorRed):
                    return System.Windows.Media.Brushes.Red;
                    break;
                case (icColorYellow):
                    return System.Windows.Media.Brushes.Yellow;
                    break;
                case (icColorWhite):
                    return System.Windows.Media.Brushes.White;
                    break;
                default:
                case (icColorLightBlue):
                    return System.Windows.Media.Brushes.LightBlue;
                    break;
            }
            return System.Windows.Media.Brushes.LightBlue;
        }

        // ProgressBar oPbThisProgressBarMdm1 = new ProgressBarMdm();

        public class ProgressBarMdm : ProgressBar {

            public ProgressBarMdm() : base() {
                // System.Windows.Forms.PaintEventHandler oPea += new System.Windows.Forms.PaintEventHandler(); 
                // this.Style. (ControlStyles.UserPaint, true); 
                // this.SetStyle(ControlStyles.UserPaint, true); 
                // rnd   = new System.Random();
                // myPen = new Pen(Color.Blue);
            }

            /*
                        delegate void UpdatePageUiPaintEvent(RoutedEventArgs ereaPassedRoutedEventArgs);

                        protected override void Paint(object sender, System.Windows.Forms.PaintEventArgs ePcea) {
                            this.Paint(this, ePcea); 
                        }

                        public void PaintIt(RoutedEventArgs ereaPassedRoutedEventArgs) {
                            System.Windows.Forms.PaintEventArgs ePea;
                            ePea = null;
                            this.Paint(this, ePea);
                        }

                        protected override void Refresh() { this.Refresh(); }

                        protected override void Redraw() { this.Redraw(); }

                        protected override void OnRender() { this.OnRender(); }

                        protected override void Show() { this.Show(); }

                        private Bitmap DrawingArea;  // Area to draw on.
                        private Button btnCircle;
                        private Button btnLine;
                        private Button btnOK;
                        private System.ComponentModel.Container components = null;
                        private System.Random rnd;
                        private Pen myPen;

                        protected override void Dispose(bool disposing) {
                            if (disposing) {
                                if (components != null) { components.Dispose(); }
                            }
                            // base.Dispose( disposing );
                        }
                        protected override void OnPaint(PaintEventArgs ePcea) {

                            System.Drawing.Rectangle rec = ePcea.ClipRectangle;
                            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
                            // if (ProgressBarRenderer.IsSupported) {
                            // ProgressBarRenderer.DrawHorizontalBar(ePcea.Graphics, ePcea.ClipRectangle);
                            rec.Height = rec.Height - 4;
                            ePcea.Graphics.FillRectangle(Brushes.Red, 2, 2, rec.Width, rec.Height);
                            // }
                            this.OnPaint(ePcea);
                        }
                        */
            // System.Windows.Forms.PaintEventArgs ePcea = new System.Windows.Forms.PaintEventArgs();
            // System.Windows.RoutedEventArgs eRpceaPassedDouble = new System.Windows.RoutedEventArgs();
            // ProgressBarMdm1.Paint(this, new System.Windows.Forms.PaintEventArgs ePcea = null);
            // ProgressBarMdm1.Refresh();
            // ProgressBarMdm1.InvalidateMeasure();
            // this.PageScrollViewer.InvalidateVisual();

            // this.InvalidateVisual();
            // RoutedEvent re = new RoutedEvent(InvalidateVisual);

            // RoutedEventArgs t = new RoutedEventArgs(InvalidateVisual, this);
            // this.ProgressBarMdm1.RaiseEvent(t);
            // ProgressBarMdm1.OnRender();
            /*
            ProgressBarMdm1.UpdateLayout();
            ProgressBarMdm1.InvalidateVisual();
            ProgressBarMdm1.BringIntoView();
            // ProgressBarMdm1.
            this.InvalidateVisual();
            //
            ProgressBarMdm1.Focus();
            ProgressBarMdm1.InvalidateMeasure();
            ProgressBarMdm1.InvalidateProperty();
            ProgressBarMdm1.SetValue += 1;
            this.InvalidateVisual();
            this.Parent.Sh
            */
            /*
            this.Dispatcher.Invoke(
                DispatcherPriority.Normal, 
                new System.Windows.Forms.MethodInvoker(
                delegate()
                  {
                       this.ProgressBar.Value = ePcea.ProgressPercentage;
                  }
                )
            );
            */
            /* // TODO RaiseTapEvent()
            void RaiseTapEvent()
            {
                RoutedEventArgs newEventArgs = new RoutedEventArgs(MyButtonSimple.TapEvent);
                RaiseEvent(newEventArgs);
            }
            // TODO RaiseCloseEvent()
            void RaiseCloseEvent()
            {
                // RoutedEventArgs newEventArgs = new RoutedEventArgs(MinputTldApp.Current.Exit);
                ExitEventHandler newEventArgs = new ExitEventHandler(MinputTldApp.Current.Exit);
                RaiseEvent(newEventArgs);
            }
             */
        }
        #endregion
        #endregion
        #endregion
    }   
}
