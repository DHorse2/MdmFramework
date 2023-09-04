//Top//
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
// using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
// using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//
using Mdm.Oss.ClipboardUtil;
using Mdm.Oss.CodeUtil;
using Mdm.Oss.Decl;
using Mdm.Oss.Support;
using Mdm.Oss.Mapp;
using Mdm.Oss.Mobj;
using Mdm.Oss.FileUtil;
using Mdm.Oss.Mvc;
// using    Mdm1Oss1FileCreation1;
using System.ComponentModel;

//
namespace Mdm.Oss.Mvc {
    public class Mcontroller : DefStdBaseRunFileConsole {
        delegate void CallerAsynchronousEventsStartClickDel(object sender, System.Windows.RoutedEventArgs eReaPassed);
        CallerAsynchronousEventsStartClickDel CallerAsynchronousEventsStartClick;
        /// <summary>
        /// Interaction logic for MinputTldApp.xaml
        /// </summary>
        /// <summary>
        /// Class Standard Properties
        /// </summary>
        #region ClaseInternalResults;
        public long iAppCancelProcessing;
        public long iAppPauseProcessing;
        public long iAppComplete_Processing;
        public long iAppObject;
        //
        public long iAppDoProcessing;
        public long iAppDoProcessingPassed;
        public long iAppDoProcessing_SetOn;
        //
        public long iAppCoreObjectGet;
        //
        public long iMhandlerStartApp;
        //
        public long iAppPage2Loaded;
        public long iAppOptions_Set;

        public long iInputFileCheck;
        public long iInputFileValidation;
        public long OutputSystemCheckResult;
        public long OutputObjectResetResult;
        //
        public long iMhandlerPassed;
        public long iMhandler;
        public long iMhandlerPassedMob;
        public long iMhandlerPassedString;
        //
        public long iPage2SetCoreObjects;
        public long iPage1SetDefaults;
        public long iPage2SetDefaults;
        public long iPageObject;
        public long iPageObjectPage1;
        public long iPage2SetFileLine;
        #endregion
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
        internal Object omWt;
        // <Area Id = "omAplication">
        public Mapplication omMa;
        // <Area Id = "omW>
        public Object omVe;
        #endregion
        #region Page Declartions
        // <Area Id = "omP">
        public MpageMain omPa;
        public string sPage1ReturnValue;
        // <Area Id = "omP2">
        public MpageDetail omPa2;
        public string sPage2ReturnValue;
        //
        public object PageFields;
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
        protected int iTemp1 = 0;
        protected int iTemp2 = 0;
        protected int iTemp3 = 0;
        // Strings
        protected string sTemp = "";
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
        protected int iPickDisplayLineNumber = 0;
        protected int iPickDisplayColumn = 0;
        protected int iPickDisplayRow = 0;
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
        #region InputTldFields
        public Mfile InputFile;
        public Mfile OutputFile;

        public bool OptionToDoOverwriteExistingItem = false;
        public bool OptionToDoCheckItemIds = false;
        public bool OptionToDoCheckFileDoesExist = false;
        public bool OptionToDoCheckFileNew = false;
        public bool OptionToDoEnterEachItemId = false;
        public bool OptionToDoLogActivity = false;
        public bool OptionToDoProceedAutomatically = false;
        public bool OptionToDoCreateMissingFile = false;
        #endregion
        /* //
        #region Class properties
        private int ipMdmClassId = 99999;
        public int MdmClassId { get { return ipMdmClassId; } set { ipMdmClassId = value; } }
        private string spMdmClassName = "unknown";
        public string MdmClassName { get { return spMdmClassName; } set { spMdmClassName = value; } }
        private string spMdmClassTitle = "unknown";
        public string MdmClassTitle { get { return spMdmClassTitle; } set { spMdmClassTitle = value; } }
        private int ipMdmClassNumber = 99999;
        public int MdmClassNumber { get { return ipMdmClassNumber; } set { ipMdmClassNumber = value; } }
        private int ipMdmClassIntStatus = 99999;
        public int MdmClassIntStatus { get { return ipMdmClassIntStatus; } set { ipMdmClassIntStatus = value; } }
        private string spMdmClassStatusText = "unknown";
        public string MdmClassStatusText { get { return spMdmClassStatusText; } set { spMdmClassStatusText = value; } }
        private int ipMdmClassResult = 99999;
        public int MdmClassResult { get { return ipMdmClassResult; } set { ipMdmClassResult = value; } }
        private bool bpMdmClassBoolResult = false;
        public bool MdmClassBoolResult { get { return bpMdmClassBoolResult; } set { bpMdmClassBoolResult = value; } }
        #endregion
        #region Method properties
        private int ipMdmMethodId = 99999;
        public int MdmMethodId { get { return ipMdmMethodId; } set { ipMdmMethodId = value; } }
        private string spMdmMethodName = "unknown";
        public string MdmMethodName { get { return spMdmMethodName; } set { spMdmMethodName = value; } }
        private string spMdmMethodTitle = "unknown";
        public string MdmMethodTitle { get { return spMdmMethodTitle; } set { spMdmMethodTitle = value; } }
        private int ipMdmMethodNumber = 99999;
        public int MdmMethodNumber { get { return ipMdmMethodNumber; } set { ipMdmMethodNumber = value; } }
        private int ipMdmMethodStatus = 99999;
        public int MdmMethodStatus { get { return ipMdmMethodStatus; } set { ipMdmMethodStatus = value; } }
        private string spMdmMethodStatusText = "unknown";
        public string MdmMethodStatusText { get { return spMdmMethodStatusText; } set { spMdmMethodStatusText = value; } }
        private int ipMdmMethodResult = 99999;
        public int MdmMethodResult { get { return ipMdmMethodResult; } set { ipMdmMethodResult = value; } }
        private bool bpMdmMethodBoolResult = false;
        public bool MdmMethodBoolResult { get { return bpMdmMethodBoolResult; } set { bpMdmMethodBoolResult = value; } }
        #endregion
        #region Attribute properties
        private int ipMdmAttributeId = 99999;
        public int MdmAttributeId { get { return ipMdmAttributeId; } set { ipMdmAttributeId = value; } }
        private string spMdmAttributeName = "unknown";
        public string MdmAttributeName { get { return spMdmAttributeName; } set { spMdmAttributeName = value; } }
        private string spMdmAttributeTitle = "unknown";
        public string MdmAttributeTitle { get { return spMdmAttributeTitle; } set { spMdmAttributeTitle = value; } }
        private int ipMdmAttributeNumber = 99999;
        public int MdmAttributeNumber { get { return ipMdmAttributeNumber; } set { ipMdmAttributeNumber = value; } }
        private int ipMdmAttributeStatus = 99999;
        public int MdmAttributeStatus { get { return ipMdmAttributeStatus; } set { ipMdmAttributeStatus = value; } }
        private string spMdmAttributeStatusText = "unknown";
        public string MdmAttributeStatusText { get { return spMdmAttributeStatusText; } set { spMdmAttributeStatusText = value; } }
        private int ipMdmAttributeIntResult = 99999;
        public int MdmAttributeIntResult { get { return ipMdmAttributeIntResult; } set { ipMdmAttributeIntResult = value; } }
        private bool bpMdmAttributeBoolResult = false;
        public bool MdmAttributeBoolResult { get { return bpMdmAttributeBoolResult; } set { bpMdmAttributeBoolResult = value; } }
        #endregion
        #region Parameter properties
        private int ipMdmParameterId = 99999;
        public int MdmAuthorParameterId { get { return ipMdmParameterId; } set { ipMdmParameterId = value; } }
        private string spMdmParameterName = "unknown";
        public string MdmParameterName { get { return spMdmParameterName; } set { spMdmParameterName = value; } }
        private int ipMdmParameterNumber = 99999;
        public int MdmParameterNumber { get { return ipMdmParameterNumber; } set { ipMdmParameterNumber = value; } }
        private string spMdmParameterTitle = "unknown";
        public string MdmParameterTitle { get { return spMdmParameterTitle; } set { spMdmParameterTitle = value; } }
        private int ipMdmParameterStatus = 99999;
        public int MdmParameterStatus { get { return ipMdmParameterStatus; } set { ipMdmParameterStatus = value; } }
        private string spMdmParameterStatusText = "unknown";
        public string MdmParameterStatusText { get { return spMdmParameterStatusText; } set { spMdmParameterStatusText = value; } }
        private int ipMdmParameterIntResult = 99999;
        public int MdmParameterIntResult { get { return ipMdmParameterIntResult; } set { ipMdmParameterIntResult = value; } }
        private bool bpMdmParameterBoolResult = false;
        public bool MdmParameterBoolResult { get { return bpMdmParameterBoolResult; } set { bpMdmParameterBoolResult = value; } }
        #endregion
        #region Property properties
        private int ipMdmPropertyId = 99999;
        public int MdmAuthorPropertyId { get { return ipMdmPropertyId; } set { ipMdmPropertyId = value; } }
        private string spMdmPropertyName = "unknown";
        public string MdmPropertyName { get { return spMdmPropertyName; } set { spMdmPropertyName = value; } }
        private int ipMdmPropertyNumber = 99999;
        public int MdmPropertyNumber { get { return ipMdmPropertyNumber; } set { ipMdmPropertyNumber = value; } }
        private string spMdmPropertyTitle = "unknown";
        public string MdmPropertyTitle { get { return spMdmPropertyTitle; } set { spMdmPropertyTitle = value; } }
        private int ipMdmPropertyStatus = 99999;
        public int MdmPropertyStatus { get { return ipMdmPropertyStatus; } set { ipMdmPropertyStatus = value; } }
        private string spMdmPropertyStatusText = "unknown";
        public string MdmPropertyStatusText { get { return spMdmPropertyStatusText; } set { spMdmPropertyStatusText = value; } }
        private int ipMdmPropertyIntResult = 99999;
        public int MdmPropertyIntResult { get { return ipMdmPropertyIntResult; } set { ipMdmPropertyIntResult = value; } }
        private bool bpMdmPropertyBoolResult = false;
        public bool MdmPropertyBoolResult { get { return bpMdmPropertyBoolResult; } set { bpMdmPropertyBoolResult = value; } }
        #endregion
        */ // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        #region RunControlManagement properties
        #region Command properties
        private int ipMdmCommandId = 99999;
        public int MdmCommandId { get { return ipMdmCommandId; } set { ipMdmCommandId = value; } }
        private string spMdmCommandName = "unknown";
        public string MdmCommandName { get { return spMdmCommandName; } set { spMdmCommandName = value; } }
        private string spMdmCommandTitle = "unknown";
        public string MdmCommandTitle { get { return spMdmCommandTitle; } set { spMdmCommandTitle = value; } }
        private int ipMdmCommandNumber = 99999;
        public int MdmCommandNumber { get { return ipMdmCommandNumber; } set { ipMdmCommandNumber = value; } }
        private int ipMdmCommandStatus = 99999;
        public int MdmCommandStatus { get { return ipMdmCommandStatus; } set { ipMdmCommandStatus = value; } }
        private string spMdmCommandStatusText = "unknown";
        public string MdmCommandStatusText { get { return spMdmCommandStatusText; } set { spMdmCommandStatusText = value; } }
        private int ipMdmCommandIntResult = 99999;
        public int MdmCommandIntResult { get { return ipMdmCommandIntResult; } set { ipMdmCommandIntResult = value; } }
        private bool bpMdmCommandBoolResult = false;
        public bool MdmCommandBoolResult { get { return bpMdmCommandBoolResult; } set { bpMdmCommandBoolResult = value; } }
        #endregion
        #region Console properties
        private int ipMdmConsoleId = 99999;
        public int MdmConsoleId { get { return ipMdmConsoleId; } set { ipMdmConsoleId = value; } }
        private string spMdmConsoleName = "unknown";
        public string MdmConsoleName { get { return spMdmConsoleName; } set { spMdmConsoleName = value; } }
        private string spMdmConsoleTitle = "unknown";
        public string MdmConsoleTitle { get { return spMdmConsoleTitle; } set { spMdmConsoleTitle = value; } }
        private int ipMdmConsoleNumber = 99999;
        public int MdmConsoleNumber {
            get { return ipMdmConsoleNumber; }
            set {
                ipMdmConsoleNumber = value;
            }
        }
        private int ipMdmConsoleStatus = 99999;
        public int MdmConsoleStatus { get { return ipMdmConsoleStatus; } set { ipMdmConsoleStatus = value; } }
        private string spMdmConsoleStatusText = "unknown";
        public string MdmConsoleStatusText { get { return spMdmConsoleStatusText; } set { spMdmConsoleStatusText = value; } }
        private int ipMdmConsoleIntResult = 99999;
        public int MdmConsoleIntResult { get { return ipMdmConsoleIntResult; } set { ipMdmConsoleIntResult = value; } }
        private bool bpMdmConsoleBoolResult = false;
        public bool MdmConsoleBoolResult { get { return bpMdmConsoleBoolResult; } set { bpMdmConsoleBoolResult = value; } }
        #endregion
        #region RunControl properties
        private int ipMdmRunId = 99999;
        public int MdmRunId { get { return ipMdmRunId; } set { ipMdmRunId = value; } }
        private string spMdmRunName = "unknown";
        public string MdmRunName { get { return spMdmRunName; } set { spMdmRunName = value; } }
        private string spMdmRunTitle = "unknown";
        public string MdmRunTitle { get { return spMdmRunTitle; } set { spMdmRunTitle = value; } }
        private int ipMdmRunNumber = 99999;
        public int MdmRunNumber { get { return ipMdmRunNumber; } set { ipMdmRunNumber = value; } }
        private int ipMdmRunStatus = 99999;
        public int MdmRunStatus { get { return ipMdmRunStatus; } set { ipMdmRunStatus = value; } }
        private string spMdmRunStatusText = "unknown";
        public string MdmRunStatusText { get { return spMdmRunStatusText; } set { spMdmRunStatusText = value; } }
        private int ipMdmRunIntResult = 99999;
        public int MdmRunIntResult { get { return ipMdmRunIntResult; } set { ipMdmRunIntResult = value; } }
        private bool bpMdmRunBoolResult = false;
        public bool MdmRunBoolResult { get { return bpMdmRunBoolResult; } set { bpMdmRunBoolResult = value; } }
        #endregion
        #region AutoRun properties
        private int ipMdmAutoRunId = 99999;
        public int MdmAutoRunId { get { return ipMdmAutoRunId; } set { ipMdmAutoRunId = value; } }
        private string spMdmAutoRunName = "unknown";
        public string MdmAutoRunName { get { return spMdmAutoRunName; } set { spMdmAutoRunName = value; } }
        private string spMdmAutoRunTitle = "unknown";
        public string MdmAutoRunTitle { get { return spMdmAutoRunTitle; } set { spMdmAutoRunTitle = value; } }
        private int ipMdmAutoRunNumber = 99999;
        public int MdmAutoRunNumber { get { return ipMdmAutoRunNumber; } set { ipMdmAutoRunNumber = value; } }
        private int ipMdmAutoRunStatus = 99999;
        public int MdmAutoRunStatus { get { return ipMdmAutoRunStatus; } set { ipMdmAutoRunStatus = value; } }
        private string spMdmAutoRunStatusText = "unknown";
        public string MdmAutoRunStatusText { get { return spMdmAutoRunStatusText; } set { spMdmAutoRunStatusText = value; } }
        private int ipMdmAutoRunIntResult = 99999;
        public int MdmAutoRunIntResult { get { return ipMdmAutoRunIntResult; } set { ipMdmAutoRunIntResult = value; } }
        private bool bpMdmAutoRunBoolResult = false;
        public bool MdmAutoRunBoolResult { get { return bpMdmAutoRunBoolResult; } set { bpMdmAutoRunBoolResult = value; } }
        #endregion
        #region Input properties
        private int ipMdmInputId = 99999;
        public int MdmInputId { get { return ipMdmInputId; } set { ipMdmInputId = value; } }
        private string spMdmInputName = "unknown";
        public string MdmInputName { get { return spMdmInputName; } set { spMdmInputName = value; } }
        private string spMdmInputTitle = "unknown";
        public string MdmInputTitle { get { return spMdmInputTitle; } set { spMdmInputTitle = value; } }
        private int ipMdmInputNumber = 99999;
        public int MdmInputNumber { get { return ipMdmInputNumber; } set { ipMdmInputNumber = value; } }
        private int ipMdmInputStatus = 99999;
        public int MdmInputStatus { get { return ipMdmInputStatus; } set { ipMdmInputStatus = value; } }
        private string spMdmInputStatusText = "unknown";
        public string MdmInputStatusText { get { return spMdmInputStatusText; } set { spMdmInputStatusText = value; } }
        private int ipMdmInputIntResult = 99999;
        public int MdmInputIntResult { get { return ipMdmInputIntResult; } set { ipMdmInputIntResult = value; } }
        private bool bpMdmInputBoolResult = false;
        public bool MdmInputBoolResult { get { return bpMdmInputBoolResult; } set { bpMdmInputBoolResult = value; } }
        #endregion
        #region Output properties
        private int ipMdmOutputId = 99999;
        public int MdmOutputId { get { return ipMdmOutputId; } set { ipMdmOutputId = value; } }
        private string spMdmOutputName = "unknown";
        public string MdmOutputName { get { return spMdmOutputName; } set { spMdmOutputName = value; } }
        private string spMdmOutputTitle = "unknown";
        public string MdmOutputTitle { get { return spMdmOutputTitle; } set { spMdmOutputTitle = value; } }
        private int ipMdmOutputNumber = 99999;
        public int MdmOutputNumber { get { return ipMdmOutputNumber; } set { ipMdmOutputNumber = value; } }
        private int ipMdmOutputStatus = 99999;
        public int MdmOutputStatus { get { return ipMdmOutputStatus; } set { ipMdmOutputStatus = value; } }
        private string spMdmOutputStatusText = "unknown";
        public string MdmOutputStatusText { get { return spMdmOutputStatusText; } set { spMdmOutputStatusText = value; } }
        private int ipMdmOutputIntResult = 99999;
        public int MdmOutputIntResult { get { return ipMdmOutputIntResult; } set { ipMdmOutputIntResult = value; } }
        private bool bpMdmOutputBoolResult = false;
        public bool MdmOutputBoolResult { get { return bpMdmOutputBoolResult; } set { bpMdmOutputBoolResult = value; } }
        #endregion
        #endregion
        #region Class Creation
        // Instantiation
        public Mcontroller(string PassedMessageString) : this() {
            iMhandlerPassedString = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectOK;
            omOb.sCommandLineRequest = PassedMessageString;
            iMhandlerPassedString = AppInitialize(null);
            iMhandlerPassedString = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            // iMhandlerPassedString
        }
        public Mcontroller(Mobject omPassedO) : this() {
            iMhandlerPassedMob = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectOK;
            iMhandlerPassedMob = AppInitialize(omPassedO);
            iMhandlerPassedMob = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            // iMhandlerPassedMob
        }
        public Mcontroller() {
            iMhandler = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectOK;
            iMhandler = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            // iMhandler
        }
        // public event CrossAppDomainDelegate
        #endregion
        public long AppInitialize(Mobject omPassedO) {
            iMhandlerStartApp = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectOK;
            if (omOb == null) {
                if (omPassedO != null) {
                    omOb = (Mobject)omPassedO;
                    omAp = (Application)omOb.omAp;
                    omCo = this;
                    // // omCo = (Mcontroller)omOb.omCo;
                    // omWt = (MinputTldThread)omOb.omWt;
                    omWt = omOb.omWt;
                    omMa = (Mapplication)omOb.omMa;
                    // omVe = (MinputTld)omOb.omVe;
                    omVe = omOb.omVe;
                    omPa = (MpageMain)omOb.omPa;
                    omPa2 = (MpageDetail)omOb.omPa2;
                } else {
                    // error
                    // omAp = MinputTldApp.Current;
                    omCo = this;
                    omOb = new Mobject(omAp);
                    omMa.ApplicationAppObjectSet(omAp);
                }
            }
            //
            omMa.ApplicationHandlerObjectSet(omCo);
            iMhandlerStartApp = omMa.ApplicationHandlerObjectSet(this);
            omPa.MdmMessageProcessText2 = "Handler initialised";
            iMhandlerStartApp = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            return iMhandlerStartApp;
        }
        #region Application Processing
        // Do Processing
        public long AppDoProcessing() {
            iAppDoProcessing = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            //
            iAppDoProcessing = AppDoProcessing(
                PageFields
            //omPa.OutputSystemNameCurrent,
            //omPa.OutputDatabaseNameCurrent,
            //omPa.InputFileNameCurrent,
            //omPa.OutputFileNameCurrent,
            //omPa.OutputFileItemIdCurrent,
            //omPa.OptionToDoOverwriteExistingItemCurrent,
            //omPa.OptionToDoCheckFileDoesExistCurrent,
            //omPa.OptionToDoCheckItemIdsCurrent,
            //omPa.OptionToDoEnterEachItemIdCurrent,
            //omPa.OptionToDoLogActivityCurrent,
            //omPa.OptionToDoProceedAutomaticallyCurrent,
            //omPa.OptionToDoCreateMissingFileCurrent
            );
            // iAppDoProcessing = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            return iAppDoProcessing;
        }
        public long AppDoProcessing(
                object PageFields
            //string sPassedSystemLine,
            //string sPassedDatabaseLine,
            //string sPassedInputFileLine,
            //string sPassedOutputFileLine,
            //string sPassedFileItemId,
            //bool bPassedOptionToDoOverwriteExistingItem,
            //bool bPassedOptionToDoCheckFileDoesExist,
            //bool bPassedOptionToDoCheckItemIds,
            //bool bPassedOptionToDoEnterEachItemId,
            //bool bPassedOptionToDoLogActivity,
            //bool bPassedOptionToDoProceedAutomatically,
            //bool bPassedOptionToDoCreateMissingFile
            ) {
            iAppDoProcessingPassed = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            if (bYES == bYES) {
                //
                // LocalMessage = "Processing Starting.";
                // omMa.MdmOutputPrint_PickPrint(3, LocalMessage, true);
                //
                //InputFile.FileSummary.SystemName = sPassedSystemLine;
                //InputFile.FileSummary.DatabaseName = sPassedDatabaseLine;
                //InputFile.FileSummary.FileName = sPassedInputFileLine;
                ////
                //OutputFile.FileSummary.SystemName = sPassedSystemLine;
                //OutputFile.FileSummary.DatabaseName = sPassedDatabaseLine;
                //OutputFile.FileSummary.FileName = sPassedOutputFileLine;
                ////
                //InputFile.FileSummary.FileItemId = sPassedFileItemId;
                ////
                //OptionToDoOverwriteExistingItem = bPassedOptionToDoOverwriteExistingItem;
                //OptionToDoCheckFileDoesExist = bPassedOptionToDoCheckFileDoesExist;
                //OptionToDoCheckItemIds = bPassedOptionToDoCheckItemIds;
                //OptionToDoEnterEachItemId = bPassedOptionToDoEnterEachItemId;
                //OptionToDoLogActivity = bPassedOptionToDoLogActivity;
                //OptionToDoProceedAutomatically = bPassedOptionToDoProceedAutomatically;
                //OptionToDoCreateMissingFile = bPassedOptionToDoCreateMissingFile;
                //
                iAppDoProcessingPassed = AppOptions_Set();
                //
                // Process Request
                //
                iAppDoProcessing = AppDoProcessing_SetOn();
                //
                // // // // iAppDoProcessingPassed = omWt.ProcessOpenFile("Import", InputFileName, InputFile, OutputFile.FileSummary.FileName, OutputFile, FileItemId, OutputFile.FileSummary.FileOptions);
                //
                System.Windows.RoutedEventArgs eReaTemp = new System.Windows.RoutedEventArgs();
                eReaTemp = null;
                // omWt.CallerAsynchronousEventsStartClick(this, eReaTemp);
                CallerAsynchronousEventsStartClick(this, eReaTemp);
            }
            return iAppDoProcessingPassed;
        }
        // Run Doing set to On pending flags
        public long AppDoProcessing_SetOn() {
            iAppDoProcessing_SetOn = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectOK;
            bRunAbort = false;
            bRunStartPending = true;
            bRunCancelPending = false;
            bRunPausePending = false;
            return iAppDoProcessing_SetOn;
        }
        // TODO NOTE: DO NOT MANIPULATE PRIMARY UI CONTROLS BELOW, CROSS THREADED
        // TODO NOTE2: CONTROL OF BUTTON VISIBILITY IS IN PRIMARY UI
        // Completion
        public long AppComplete_Processing() {
            iAppComplete_Processing = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // if (!bRunCancelPending && !bRunAbort) {
            if (bYES == bYES) {
                LocalMessage = "Processing Completed.";
                MdmOutputPrint_PickPrint(3, "A2" + LocalMessage, true);
                // System.Windows.RoutedEventArgs eReaTemp = new System.Windows.RoutedEventArgs();
                // eReaTemp = null;
                // omVe.CallerAsynchronousEventsComplete_Click;
                // Set State
                RunAction = RunRunDo;
                RunMetric = RunState;
                RunTense = RunTense_Did;
                omCo.iaRunActionState[RunRunDo, RunState] = RunTense;
                omCo.ePceaRunActionState = new ProgressChangedEventArgs(0,
                    "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString() + LocalMessage);
                // "Y" + LocalMessage);
                omWt.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            }
            return iAppComplete_Processing;
        }
        // Pause Processing
        public long AppPauseProcessing() {
            iAppPauseProcessing = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            if (bYES == bYES) {
                LocalMessage = "Processing Pausing.";
                omMa.MdmOutputPrint_PickPrint(3, "A2" + LocalMessage, true);
                RunAction = RunPause;
                RunMetric = RunState;
                if (omCo.iaRunActionState[RunPause, RunState] == RunTense_Did) {
                    bRunPausePending = bNO;
                    RunTense = RunTense_Off;
                } else {
                    bRunPausePending = bYES;
                    RunTense = RunTense_Do;
                    // System.Windows.RoutedEventArgs eReaTemp = new System.Windows.RoutedEventArgs();
                    // eReaTemp = null;
                    // omVe.CallerAsynchronousEventsPauseClick;
                    // Set State
                }
                omCo.iaRunActionState[RunRunDo, RunState] = RunTense;
                omCo.ePceaRunActionState = new ProgressChangedEventArgs(0,
                    "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString() + LocalMessage);
                omWt.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            }
            return iAppPauseProcessing;
        }
        // Cancellation
        public long AppCancelProcessing() {
            iAppCancelProcessing = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            if (bYES == bYES) {
                bRunCancelPending = true;
                // bRunCancelled = true;
                LocalMessage = "Processing Cancelling.";
                omMa.MdmOutputPrint_PickPrint(3, LocalMessage, true);
                // System.Windows.RoutedEventArgs eReaTemp = new System.Windows.RoutedEventArgs();
                // eReaTemp = null;
                // omVe.CallerAsynchronousEventsCancelClick;
                // Set State
                RunAction = RunCancel;
                RunMetric = RunState;
                RunTense = RunTense_Do;
                omCo.iaRunActionState[RunRunDo, RunState] = RunTense;
                omCo.ePceaRunActionState = new ProgressChangedEventArgs(0,
                    "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString() + LocalMessage);
                omWt.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            }
            return iAppCancelProcessing;
        }
        // Cancellation Completed
        public long AppCancelCompleted() {
            iAppCancelProcessing = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            if (bYES == bYES) {
                bRunCancelPending = false;
                bRunAbort = true;
                // omVe.CallerAsynchronousEventsCancelClick;
                LocalMessage = "ABORT: Run was CANCELLED sucessfully!!!";
                omWt.MdmOutputPrint_PickPrint(3, "A2" + LocalMessage, true);
                // Set State
                RunAction = RunCancel;
                RunMetric = RunState;
                RunTense = RunTense_Did;
                omCo.iaRunActionState[RunRunDo, RunState] = RunTense;
                ProgressChangedEventArgs eaOpcCurrentRunProgressMax = new ProgressChangedEventArgs(0,
                    "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString() + LocalMessage);
                // "X" + LocalMessage);
                omWt.backgroundWorker_ProgressChanged(this, eaOpcCurrentRunProgressMax);
            }
            return iAppCancelProcessing;
        }
        #endregion
        #region Application Objects
        public void AppObject(MpageMain opPassedPage1) {
            iAppObject = (int)DatabaseControl.ResultStarted;
            // This is the Set.
            // This should be set and get (properties)
            // MinputTldApp omCo;
            if (omCo == null) {
                // omCo = new Mcontroller();
                omCo = this;
                if (omOb != null) {
                    // omOb.omCo = omCo;
                    omMa.ApplicationHandlerObjectSet(omCo);
                }
                LocalLongResult = omCo.AppInitialize(omOb);
            }
            //
            if (omWt == null) {
                if (omOb.omWt != null) {
                    omWt = (MinputTldThread)omOb.omWt;
                } else if (omOb != null) {
                    if (omOb.omWt != null) {
                        // omWt = (MinputTldThread)omOb.omWt;
                        omWt = (MinputTldThread)omMa.ApplicationVerbObjectGet();
                    } else {
                        omWt = new MinputTldThread();
                        omOb.omWt = omWt;
                    }
                } else {
                    omOb = new Mobject(omAp, omPa);
                    omOb.omOb = omOb;
                    omWt = new MinputTldThread(omOb);
                    omOb.omWt = omWt;
                }
                omMa.ApplicationVerbObjectSet(omWt);
                // LocalLongResult = omWt.AppInitialize();
            }
            //
            if (omMa == null) {
                if (omOb.omMa != null) {
                    omMa = (Mapplication)omOb.omMa;
                } else if (omOb != null) {
                    if (omOb.omOb != null) {
                        // omMa = (MinputTld)omOb.omMa;
                        omMa = (Mapplication)omMa.ApplicationMappObjectGet();
                    } else {
                        omMa = new Mapplication(omOb);
                        omOb.omMa = omMa;
                    }
                } else {
                    omOb = new Mobject(omAp, omPa);
                    omOb.omOb = omOb;
                    omMa = new Mapplication();
                    omOb.omMa = omMa;
                }
                omMa.ApplicationMappObjectSet(omMa);
                // LocalLongResult = omMa.AppInitialize();
            }
            //
            if (omVe == null) {
                if (omOb.omVe != null) {
                    // omVe = (MinputTld)omOb.omVe;
                    omVe = (MinputTld)omMa.ApplicationMappObjectGet();
                } else {
                    omVe = new MinputTld();
                }
                omMa.ApplicationMbgWorkerObjectSet(omVe);
                // LocalLongResult = omVe.AppInitialize();
            }
        }
        public long AppCoreObjectGet(Mobject omPassedO) {
            iAppCoreObjectGet = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // MinputTldApp omCo;
            if (omCo == null) {
                // omCo = ApplicationHandlerObjectGet();
                // omCo = omMa.ApplicationHandlerObjectGet();
                omCo = this;
                // ApplicationHandlerObjectSet(this);
            }
            // MinputTldApp omOb;
            if (omOb == null) {
                omOb = omPassedO;
                // omOb = ApplicationMapplicationObjectGet();
                // omOb = (Mobject)ApplicationMobjectObjectGetFromMaobject();
                // omOb = this;
                // ApplicationMobjectObjectSetMobject(this);
            }
            // MinputTldApp Page;
            if (omPa == null) {
                omPa = (MpageMain)omMa.AppPageObjectGet();
                // omPa = (Page)omOAppPageObjectGet();
                // omPa = this
                // omOAppPageObjectSet(this);
                omPa.omCo = omCo;
                omPa.omOb = omOb;
                omPa.omPa = omPa;
            }
            // MinputTldApp omAp
            if (omAp == null) {
                omAp = omMa.ApplicationAppObjectGet();
                // omAp = ApplicationAppObjectGet();
                // omAp = this;
                // omOApplicationAppObjectSet(this);
                omPa.omAp = omAp;
            }
            // MinputTldApp MpageDetail;
            if (omPa2 == null) {
                omPa2 = omPa.omPa2;
                if (omPa2 == null) {
                    omPa2 = (MpageDetail)omMa.AppPage2ObjectGet();
                    // omPa2 = (MpageDetail)omOAppPage2ObjectGet();
                    // omPa2 = this
                    // omOAppPage2ObjectSet(this);
                    omPa.omPa2 = omPa2;
                }
            }
            // MinputTldApp Verb
            if (omWt == null) {
                // omWt = omMa.ApplicationVerbObjectGet();
                omWt = (MinputTldThread)omMa.ApplicationVerbObjectGet();
                // omWt = this;
                // omOApplicationVerbObjectSet(this);
                omPa.omWt = omWt;
            }
            // MinputTldApp omMa
            if (omMa == null) {
                omMa = (Mapplication)omMa.ApplicationMappObjectGet();
                // omMa = MapplicationMappObjectGet();
                // omMa = this;
                // omOMapplicationMappObjectSet(this);
                omPa.omMa = omMa;
            }
            // MinputTldApp omVe (Thread
            if (omVe == null) {
                omVe = (MinputTld)omMa.ApplicationMbgWorkerObjectGet();
                // omVe = MbgWorkerlicationMbgWorkerObjectGet();
                // omVe = this;
                // omOMbgWorkerlicationMbgWorkerObjectSet(this);
                omPa.omVe = omVe;
            }
            iAppCoreObjectGet = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            return iAppCoreObjectGet;
        }
        public long OutputObjectClear() {
            MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            OutputFile.FileSummary.SystemObject = null;
            OutputFile.FileSummary.DatabaseObject = null;
            OutputFile = null;
            return MdmMethodResult;
        }
        public long OutputObjectReset() {
            OutputObjectResetResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // create System Object
            OutputFile.FileSummary.SystemObject = new Object();
            OutputFile.FileSummary.SystemName = "";
            // clear Database Object;
            // spDatabaseName = "";
            OutputFile.FileSummary.DatabaseObject = null;
            // clear Database File Object;
            // OutputFileName = "";
            OutputFile.FileSummary.FileName = null;
            // clear Database File Id;
            // FileItemId = "";
            // sPassedOutputFileOptions = "";
            OutputObjectResetResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            return OutputObjectResetResult;
        }
        #region Application Pages
        // Pages
        public long AppPageObjectSet(MpageMain opPassedPage1, MpageDetail opPassedPage2) {
            omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omPa = opPassedPage1;
            omPa2 = opPassedPage2;
            omPa2.OutputFileLine.Text = omPa.OutputFileLine.Text;
            omPa2.DbMasterOutputFileLine.Text = omPa2.OutputFileLine.Text;
            omPa2.DbSecurityMasterOutputFileLine.Text = omPa2.OutputFileLine.Text;

            return omOb.MdmMethodResult;
        }
        // Page 1
        public void AppPageObject(MpageMain opPassedPage1) {
            AppObject(opPassedPage1);
            omPa = opPassedPage1;
            omMa.AppPageObjectSet(omPa);
        }
        public void AppPageObject(MpageMain opPassedPage1, MpageDetail opPassedPage2) {
            AppObject(opPassedPage1);
            omPa = opPassedPage1;
            omMa.AppPageObjectSet(omPa);
            omPa2 = opPassedPage2;
            omMa.AppPage2ObjectSet(omPa2);
        }
        public long AppPage1SetDefaults(MpageMain opPassedPage1) {
            omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omPa = opPassedPage1;
            // omPa2 = opPassedPage2;
            omPa.InputFileLine.Text = "";
            omPa.OutputFileLine.Text = "DaveTest1";
            omPa.OutputFileItemId.Text = "";
            // InputFileNameCurrent = omPa.InputFileLine.Text;
            // OutputFileNameCurrent = omPa.OutputFileLine.Text;
            // OutputFileItemIdCurrent = omPa.OutputFileItemId.Text;
            // Options:(DependancyPropertyKey) 
            // omPa.
            // omPa.Checkbox.
            // omPa.Checkbox.Content
            // omPa.Checkbox.IsChecked
            // omPa.Checkbox.IsThreeState
            // omPa.Checkbox.IsThreeState IsCheckedProperty 
            omPa.OptionToDoOverwriteExistingItemCurrent = true;
            omPa.OptionToDoOverwriteExistingItem.IsChecked = true;

            omPa.OptionToDoCheckItemIdsCurrent = false;
            omPa.OptionToDoCheckItemIds.IsChecked = false;

            omPa.OptionToDoCheckFileDoesExistCurrent = false;
            omPa.OptionToDoCheckFileDoesExist.IsChecked = false;

            omPa.OptionToDoEnterEachItemIdCurrent = false;
            omPa.OptionToDoEnterEachItemId.IsChecked = false;

            omPa.OptionToDoLogActivityCurrent = true;
            omPa.OptionToDoLogActivity.IsChecked = true;

            omPa.OptionToDoProceedAutomaticallyCurrent = true;
            omPa.OptionToDoProceedAutomatically.IsChecked = true;

            omPa.OptionToDoCreateMissingFileCurrent = true;
            omPa.OptionToDoCreateMissingFile.IsChecked = true;

            return omOb.MdmMethodResult;
        }
        // Page 2
        public long AppPage2SetCoreObjects(Mobject omPassedO) {
            iPage2SetCoreObjects = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            iPage2SetCoreObjects = AppCoreObjectGet(omPassedO);
            return iPage2SetCoreObjects;
        }
        public long AppPage2Loaded(Mobject omPassedO) {
            iAppPage2Loaded = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectOK;
            omPa2.MdmMessageProcessText2 = "MpageDetail initialised";
            // MinputTldApp MpageDetail;
            if (omPa2 == null) {
                omPa2 = omPa.omPa2;
                if (omPa2 == null) {
                    omPa2 = (MpageDetail)omMa.AppPage2ObjectGet();
                    // omPa2 = (MpageDetail)omOAppPage2ObjectGet();
                    // omPa2 = this
                    // omOAppPage2ObjectSet(this);
                    omPa.omPa2 = omPa2;
                }
            }
            //
            iPage2SetDefaults = AppPage2SetCoreObjects(omPassedO);
            //
            iAppPage2Loaded = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            return iAppPage2Loaded;
        }
        public long AppPage2SetDefaults(MpageMain opPassedPage1, MpageDetail opPassedPage2) {
            iPage2SetDefaults = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omPa = opPassedPage1;
            omPa2 = opPassedPage2;
            //
            // omPa2.OutputSystemLine.Text = @"MDMPC10\SQLEXPRESS";
            omPa2.OutputSystemLine.Text = @"MDMPC11\SQLEXPRESS";
            // omPa2.OutputSystemLine.Text = @"localhost";
            // omPa2.OutputDatabaseLine.Text = @"dbo";
            omPa2.OutputDatabaseLine.Text = @"DaveTestDb1";
            omPa2.OutputFileGroupLine.Text = @"";
            omPa2.OutputFileLine.Text = omPa.OutputFileLine.Text;

            OutputFile.FileSummary.SystemName = omPa2.OutputSystemLine.Text;
            OutputFile.FileSummary.DatabaseName = omPa2.OutputDatabaseLine.Text;
            OutputFile.FileSummary.FileGroupName = omPa2.OutputFileGroupLine.Text;
            OutputFile.FileSummary.FileName = omPa2.OutputFileLine.Text;

            omPa2.DbMasterOutputSystemLine.Text = omPa2.OutputSystemLine.Text;
            omPa2.DbMasterOutputDatabaseLine.Text = omPa2.OutputDatabaseLine.Text;
            omPa2.DbMasterOutputFileLine.Text = omPa2.OutputFileLine.Text;

            OutputFile.FileSummary.MasterSystemLine = omPa2.OutputSystemLine.Text;
            OutputFile.FileSummary.MasterDatabaseLine = omPa2.OutputDatabaseLine.Text;
            OutputFile.FileSummary.MasterFileLine = omPa2.OutputFileLine.Text;

            omPa2.DbUserNameLine.Text = @"Guest";
            omPa2.DbUserPasswordLine.Text = "";
            omPa2.DbUserPasswordRequiredOption.IsChecked = false;

            OutputFile.FileSummary.UserNameLine = omPa2.DbUserNameLine.Text;
            OutputFile.FileSummary.UserPasswordLine = omPa2.DbUserPasswordLine.Text;
            OutputFile.FileSummary.UserPasswordRequiredOption = (bool)omPa2.DbUserPasswordRequiredOption.IsChecked;


            omPa2.DbSecurityMasterOutputSystemLine.Text = omPa2.OutputSystemLine.Text;
            omPa2.DbSecurityMasterOutputDatabaseLine.Text = omPa2.OutputDatabaseLine.Text;
            omPa2.DbSecurityMasterOutputFileLine.Text = omPa2.OutputFileLine.Text;

            OutputFile.FileSummary.SecurityMasterSystemLine = omPa2.DbSecurityMasterOutputSystemLine.Text;
            OutputFile.FileSummary.SecurityMasterDatabaseLine = omPa2.DbSecurityMasterOutputDatabaseLine.Text;
            OutputFile.FileSummary.SecurityMasterFileLine = omPa2.DbSecurityMasterOutputFileLine.Text;

            return iPage2SetDefaults;
        }
        #endregion
        // TODO MpageDetail - Mdm - Application - Object - Get xxxxxxxxxxxxxxxxxxxxx
        #endregion
        #region Application Verb
        // xxxx Import File xxxx
        public long InputFileCheck(string sPassedInputFileName) {
            iInputFileCheck = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            InputFile.FileSummary.FileName = sPassedInputFileName;
            omOb.MdmMessageProcessText2 = "Check Input File";
            iInputFileCheck = InputFileValidation(InputFile.FileSummary.FileName);
            /* if (iInputFileCheck == (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult)
            {
                iInputFileCheck = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            }
            else if (iInputFileCheck == (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.InvalidResult)
            {
                iInputFileCheck = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.InvalidResult;
            }
             */
            iInputFileCheck = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            return iInputFileCheck;
        }
        public long InputFileValidation(string sPassedInputFileName) {
            iInputFileValidation = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            if (InputFile != null) {
                InputFile = null;
            }
            if (InputFile == null) {
                InputFile = new Mfile(omOb);
            }
            iInputFileValidation = omWt.FileNameCheckDoesExist(InputFile, sPassedInputFileName, true, true);
            if (iInputFileValidation == (int)DatabaseControl.ResultDoesExist) {
                InputFile.FileSummary.FileName = sPassedInputFileName;
                iInputFileValidation = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            } else {
                iInputFileValidation = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.InvalidResult;
            }
            InputFile.FileSummary.FileName = sPassedInputFileName;
            // iInputFileValidation = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            return iInputFileValidation;
        }
        // xxxx Output File xxx
        // Output File
        public long OutputFileCheck(string sPassedOutputFileName) {
            OutputFile.FileSummary.FileName = sPassedOutputFileName;
            omOb.MdmMessageProcessText2 = "Check Output File";

            omOb.MdmMethodResult = OutputFileValidation(sPassedOutputFileName);
            // file must exist area

            // file must not exist area

            return omOb.MdmMethodResult;
        }
        public long OutputFileValidation(string sPassedOutputFileName) {
            omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            if (OutputFile != null) {
                OutputFile = null;
            }
            if (OutputFile == null) {
                OutputFile = new Mfile(omOb);
                OutputFile.FileSummary.FileName = sPassedOutputFileName;
            }

            // OutputFile.FileItemId = "";

            // OutputFile.FileSummary.FileOptions = "";

            omOb.MdmMethodResult = omWt.FileNameCheckDoesExist(OutputFile, sPassedOutputFileName, true, true);

            if (omOb.MdmMethodResult == (int)DatabaseControl.ResultDoesExist) {
                if (OptionToDoCheckFileDoesExist) {
                    OutputFile.FileSummary.FileName = sPassedOutputFileName;
                    omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
                } else if (OptionToDoCheckFileNew) {
                    omOb.MdmMessageProcessText2 = "Output File does exist";
                    omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.InvalidResult;
                } else {
                    omOb.MdmMessageProcessText2 = "Warning: Output File does exist";
                    omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
                }
            } else if (omOb.MdmMethodResult == (int)DatabaseControl.ResultDoesNotExist) {
                if (OptionToDoCheckFileDoesExist) {
                    OutputFile.FileSummary.FileName = sPassedOutputFileName;
                    omOb.MdmMessageProcessText2 = "Output File does not exist";
                    omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.InvalidResult;
                } else if (OptionToDoCheckFileNew) {
                    omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
                } else if (OptionToDoCreateMissingFile) {
                    omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
                } else {
                    omOb.MdmMessageProcessText2 = "Warning: Output File does not exist";
                    omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
                }
            }
            return omOb.MdmMethodResult;
        }
        // System
        public long OutputSystemCheck(string sPassedOutputSystemName) {
            OutputSystemCheckResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            OutputFile.FileSummary.SystemName = sPassedOutputSystemName;
            omOb.MdmMessageProcessText2 = "Check Output System";
            OutputSystemCheckResult = OutputSystemValidation(sPassedOutputSystemName);
            if (OutputSystemCheckResult != (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult) {
                OutputSystemCheckResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectDoesNotExist;
            } else {
                OutputSystemCheckResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectDoesExist;
            }
            return OutputSystemCheckResult;
        }
        public long OutputSystemValidation(string sPassedOutputSystemName) {
            omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;

            if (OutputFile.FileSummary.SystemObject != null || OutputFile.FileSummary.DatabaseObject != null || OutputFile.FileSummary.FileName != null) {
                omOb.MdmMethodResult = OutputObjectClear();
            }
            OutputFile.FileSummary.SystemName = sPassedOutputSystemName;

            // 
            if (OutputFile.FileSummary.SystemObject == null) {
                // check system
                OutputFile.FileSummary.SystemObject = new Object();

                omOb.MdmMethodResult = omWt.FileSystemNameCheckDoesExist(OutputFile.FileSummary.SystemObject, OutputFile.FileSummary.SystemName, true, true);
                if (omOb.MdmMethodResult != (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectDoesExist) {
                    OutputFile.FileSummary.SystemName = sPassedOutputSystemName;
                    omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
                } else {
                    omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.InvalidResult;
                    // reset output
                    omOb.MdmMethodResult = OutputObjectReset();
                }
            }
            return omOb.MdmMethodResult;
        }
        // Database
        public long OutputDatabaseCheck(string sPassedOutputDatabaseName) {
            OutputFile.FileSummary.DatabaseName = sPassedOutputDatabaseName;
            omOb.MdmMessageProcessText2 = "Check Output Database";

            omOb.MdmMethodResult = OutputDatabaseValidation(sPassedOutputDatabaseName);
            return omOb.MdmMethodResult;
        }
        public long OutputDatabaseValidation(string sPassedOutputDatabaseName) {
            omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            if (OutputFile.FileSummary.DatabaseObject != null) {
                OutputFile.FileSummary.DatabaseObject = null;
                OutputFile.FileSummary.FileName = null;
            }
            if (OutputFile.FileSummary.DatabaseObject == null) {
                OutputFile.FileSummary.DatabaseName = sPassedOutputDatabaseName;
                // 
                if (OutputFile.FileSummary.SystemObject == null) {
                    omOb.MdmMethodResult = (int)DatabaseControl.ResultShouldNotExist;
                    return omOb.MdmMethodResult;
                }
                //

                OutputFile.FileSummary.DatabaseObject = new SqlConnection();

                OutputFile.FileSummary.FileName = null;

                // check Database exists
                omOb.MdmMethodResult = omWt.FileDatabaseCheckDoesExist(OutputFile.FileSummary.DatabaseObject, sPassedOutputDatabaseName, true, true);
                //
            }
            return omOb.MdmMethodResult;
        }
        // Item Ids
        public long OutputFileItemIdCheck(string OutPassedIdName) {
            omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            OutputFile.FileSummary.FileItemId = OutPassedIdName;
            omOb.MdmMessageProcessText2 = "Check Output File Item Id";
            //
            omOb.MdmMethodResult = omWt.FileNameCheckDoesExist(OutputFile, OutputFile.FileSummary.FileName, true, true);
            if (omOb.MdmMethodResult == 0) {
                omOb.MdmMethodResult = OutputFileItemIdValidation(OutPassedIdName);
            }
            // TODO Check Id Logic
            return omOb.MdmMethodResult;
        }
        public long OutputFileItemIdValidation(string OutPassedIdName) {
            omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // logic to verify an item exist or not
            /*
            OptionToDoOverwriteExistingItem
            OptionToDoCheckItemIdDoesExist
            OptionToDoCheckFileDoesExist
            OptionToDoEnterEachItemId
             */
            return omOb.MdmMethodResult;
        }
        // Options
        public long AppOptions_Set() {
            iAppOptions_Set = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            OutputFile.FileSummary.FileOptions = "";
            if (OptionToDoOverwriteExistingItem) { OutputFile.FileSummary.FileOptions += "O"; };
            if (OptionToDoCheckFileDoesExist) { OutputFile.FileSummary.FileOptions += "F"; };
            if (OptionToDoCheckItemIds) { OutputFile.FileSummary.FileOptions += "E"; };
            if (OptionToDoEnterEachItemId) { OutputFile.FileSummary.FileOptions += "I"; };
            if (OptionToDoLogActivity) { OutputFile.FileSummary.FileOptions += "L"; };
            if (OptionToDoProceedAutomatically) { OutputFile.FileSummary.FileOptions += "A"; };
            if (OptionToDoCreateMissingFile) { OutputFile.FileSummary.FileOptions += "M"; };
            iAppOptions_Set = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            return iAppOptions_Set;
        }
        /*
        public long OutputOptionReset()
        {
            omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // 
            OptionToDoOverwriteExistingItem
            OptionToDoCheckItemIdDoesExist
            OptionToDoCheckFileDoesExist
            OptionToDoEnterEachItemId
            OptionToDoLogActivity
            OptionToDoProceedAutomatically
            OptionToDoCreateFileMustNotExist
            return omOb.MdmMethodResult;
        }
        */
        public long AppOptions_Validation(string PassedFileOptions) {
            omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            string sOptionChar = "";
            int iForCounter = 0;
            //For Loop
            for (iForCounter = 1; iForCounter <= PassedFileOptions.Length; iForCounter++) {
                sOptionChar = PassedFileOptions.Substring(iForCounter, 1);
                switch (sOptionChar) {
                    case "O":
                        OptionToDoOverwriteExistingItem = true;
                        break;
                    case "F":
                        OptionToDoCheckFileDoesExist = true;
                        break;
                    case "E":
                        OptionToDoCheckItemIds = true;
                        break;
                    case "I":
                        OptionToDoEnterEachItemId = true;
                        break;
                    case "L":
                        OptionToDoLogActivity = true;
                        break;
                    case "A":
                        OptionToDoProceedAutomatically = true;
                        break;
                    case "Z":
                        OptionToDoCreateMissingFile = true;
                        break;
                    default:
                        omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.FunctionAttributeOutOfRange;
                        break;
                }
            }
            return omOb.MdmMethodResult;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Action and Locals
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Common Declarations and Delegates
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
        public event oTextBoxChangeDelegate MdmMessageProcessText2TextChanged;
        public event oTextBoxChangeDelegate MdmMessageProcessText3TextChanged;
        public event oTextBoxChangeDelegate MdmMessageProcessText4TextChanged;

        public event oTextBoxAddDelegate MdmMessageProcessText1TextAdd;
        public event oTextBoxAddDelegate MdmMessageProcessText2TextAdd;
        public event oTextBoxAddDelegate MdmMessageProcessText3TextAdd;
        public event oTextBoxAddDelegate MdmMessageProcessText4TextAdd;
        // public delegate void worker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs eaProcessChangeEventArgs);
        */
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        public void RunCancelAsync() {
            omVe.RunCancelAsync();
        }
        #endregion
    }
}
