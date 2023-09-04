//Top//
using System; // Page // App
using System.Collections;
using System.Collections.Generic; // Page // App
using System.Configuration;  // App
using System.ComponentModel;
using System.Data; // App
using System.Diagnostics;
// using System.Drawing;
using System.IO;
using System.Linq; // Page // App
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text; // Page
using System.Text.RegularExpressions;
using System.Windows; // Page // App
// using System.Windows.Controls; // Page
using System.Windows.Data; // Page
// using System.Windows.Documents; // Page
using System.Windows.Input; // Page
using System.Windows.Media; // Page
// using System.Windows.Media.Imaging; // Page
// using System.Windows.Navigation; // Page // App
// using System.Windows.Shapes; // Page
using System.Runtime.Remoting.Messaging;


using    Mdm.Oss.ClipboardUtil;
using    Mdm.Oss.CodeUtil;
using    Mdm.Oss.Support;
// using    Mdm.Oss.Mobj;
// using    Mdm.Oss.FileUtil;
// using    Mdm1Oss1FileCreation1;
using Mdm.Oss.Decl;

namespace Mdm.Oss.Mapp {
    public class Mapplication : DefStdBaseRunFile {
        // Mdm.Oss.Decl.DefStdBaseRunFile.
        #region Class Standard Properties List
        // oo   = Object - Object Type not specified
        // omo  = Object - Mdm  - Mapplication
        // omc  - Object - Mdm  - Mdm Application
        // omc  - Object - Mdm  - TODO
        // oe   - Object - Exception
        // of   - Object - File
        // ofb  - Object - File - Buffer
        // ofs  - Object - File - File Stream
        // ofsr - Object - File - File Stream Reader
        // ofdc - Object - File - Databse Command
        // ofd  - Object - File - Database Connection
        // ofe  - Object - File - Exception
        // oh   - Object - HashTable
        // os   - Object - Structure
        #endregion
        #region Clipboard Ui Helper
        // [GuidAttribute("DF29D855-D0EC-4DA1-BCC3-42FA3A09B1CB")]
        // [ComVisibleAttribute(false)]
        //public interface SVsUIHierWinClipboardHelper
        #endregion
        #region StandardObjects
        #region PackageObjectDeclarations ABSTRACT
        // <Area Id = "Mapplication">
        // <Area Id = "omAplication">
        public Application omAp;
        // <Area Id = "omHControl">
        public object omHa;
        // <Area Id = "omO">
        public object omOb;
        // <Area Id = "omV">
        // <Area Id = "omAplicationThread">
        public object omVe;
        // <Area Id = "omAplication">
        public object omMa;
        // <Area Id = "omW">
        public object omWo;
        #region Page Declartions
        // <Area Id = "opPageObjects">
        // <Area Id = "omP">
        public Page omPa;
        public string sPage1ReturnValue;
        // <Area Id = "omP2">
        public Page omPa2;
        public string sPage2ReturnValue;
        #endregion
        // Introspection
        public string sMethodObjectType;
        public System.Type odstMethodObjectType;
        public int iMethodObjectHashCode;
        public string sMethodObjectToString;
        public bool bMethodObjectEquality;
        public bool bMethodObjectTypeValid;
        public bool bMethodObjectExternalExistance;
        public bool bMethodObjectInternalExistance;
        #endregion
        #region MdmStandardIoObject
        // <Area Id = "Console_Object">
        public TextWriter ocotConsole_Writer;
        // public TextWriter ocotStandardOutput;
        public TextReader ocitConsole_Reader;
        public StreamWriter ocosConsole_Writer;
        // public StreamWriter ocoStandardOutput;
        public StreamReader ocisConsole_Reader;
        // public StreamWriter ocetErrorWriter;
        public IOException eIoe;
        public TextWriter ocetErrorWriter;
        //
        #endregion
        #endregion
        // #region StandardFields
        // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        #region MdmFileDatabaseControlConstants
        // <Section Summary>
        // <Section Role="Declarations">
        // <Section Id = "MdmFileDatabaseControlConstants">
        // <Section Vs="MdmStdRunVs0_9_0">
        // <Section Id = "FileIOConstants">
        // <Area Id = "FileSchemaLevel"
        const int FILE_DICT_DATA = 1;
        const int FILE_DATA = 2;
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // <Area Id = "FileOpenConstants">
        // protected int FileIsOpenStatus = 0;
        const int ciFile_Io_OPEN_OK = 0;
        const int ciFile_Io_OPEN_TRY_FIRST = -3;
        const int ciFile_Io_OPEN_TRY_AGAIN = -2;
        const int ciFile_Io_OPEN_TRY_ENTERED = 1;
        const int ciFile_Io_OPEN_TRY_DEFAULT = 2;
        const int ciFile_Io_OPEN_TRY_ORIGINAL = 3;
        const int ciFile_Io_OPEN_TRY_ALL = 3;
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // TODO OBJECT
        const int ciOf_Obejct_OK = 0;
        const int ciOf_Obejct_FILEDATA = 1000;
        const int ciOf_Obejct_FILEDICT = 2000;
        const int ciOf_Obejct_DATABASE = 3000;
        const int ciOf_Obejct_SERVICE = 4000;
        const int ciOf_Obejct_SERVER = 5000;
        const int ciOf_Obejct_SYSTEM = 6000;
        const int ciOf_Obejct_NETWORK = 7000;
        const int ciOf_Obejct_SECURITY = 8000;
        const int ciOf_Obejct_USER = 9000;
        const int ciOf_Obejct_Undefined = 90000;
        // TODO ACTION
        const int ciOf_Action_OK = 0;
        const int ciOf_Action_OPEN = 100;
        const int ciOf_Action_CLOSE = 200;
        const int ciOf_Action_CREATE = 300;
        const int ciOf_Action_DELETE = 400;
        const int ciOf_Action_READ = 500;
        const int ciOf_Action_WRITE = 600;
        const int ciOf_Action_CONNECT = 700;
        const int ciOf_Action_Undefined = 90000;
        // TODO Result
        const int ciOf_Result_OK = 0;
        const int ciOf_Result_Started = 1;
        const int ciOf_Result_Failed = 2;
        const int ciOf_Result_At_End = 3;
        const int ciOf_Result_Cancelled = 4;
        const int ciOf_Result_TimedOut = 5;
        const int ciOf_Result_Unknow_Failure = 6;
        const int ciOf_Result_Os_Error = 7;
        const int ciOf_Result_Database_Error = 8;
        const int ciOf_Result_Operation_In_Progress = 41;
        const int ciOf_Result_Missing_Name = 21;
        const int ciOf_Result_Undefined = 90000;
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // <Area Id = "FileExistanceConstants">
        const int ciOf_Result_ShouldNotExist = 53;
        const int ciOf_Result_ShouldExist = 54;
        const int ciOf_Result_DoesExist = 52;
        const int ciOf_Result_DoesNotExist = 51;
        const int DBciOf_Result_DoesNotExist = 55;
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // <Area Id = "FileReadModeConstants">
        // protected int ipFileReadMode = 0;
        const int ciRead_Mode_NotSet = 0;
        const int ciRead_Mode_BLOCK = 1;
        const int ciRead_Mode_LINE = 2;
        const int ciRead_Mode_ALL = 3;
        const int ciRead_Mode_SQL = 4;
        const int ciRead_Mode_ERROR = 5;
        const int ciRead_Mode_Undefined = -99999;
        // additional access modes
        const int ciRead_Mode_BINARY = 25;
        const int ciRead_Mode_SEEK = 28;
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // DIST AND NETWORK
        const int ciFile_Io_DISK_FULL = 8001;
        const int ciFile_Io_DISK_ERROR = 8002;
        const int ciFile_Io_NETWORK_ERROR = 8003;
        const int ciFile_Io_INTERNET_ERROR = 8004;
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // <Area Id = "AsciiOpenOptions">
        // protected int AsciiOpenOptions = 0;
        // <Area Id = "FileAccess">
        const int ciFile_Io_Access_NotSet = 0;
        const int ciFile_Io_Access_READ_ONLY = 21;
        const int ciFile_Io_Access_APPEND_ONLY = 22;
        const int ciFile_Io_Access_ERROR = 5;
        // <Area Id = "FileCreation">
        const int ciFile_Io_CREATE_IF_MISSING = 23;
        const int ciFile_Io_CREATE_ONLY = 24;
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
        const int ciFile_Io_NO_ROW_ID = 99;
        const int ciFile_Io_ROW_ID_ShouldNotExist = 33;
        const int ciFile_Io_ROW_ID_DoesExist = 32;
        const int ciFile_Io_ROW_ID_DoesNotExist = 31;
        // <Area Id = "FileTableColumnErrorsConstants">
        #endregion
        // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        #region Types and Devies
        // <Section Summary>
        // <Section Role="Declarations">
        // <Section Id = "MdmLocalTemporaryDeclarations">
        // <Section Vs="MdmLocalTempVarVs0_8_5">
        // MdmLocalTemporaryDeclarations MdmLocalTempVarVs0_8_5
        // <Area Id = "Data">

        // <Area Id = "Counters">

        // <Area Id = "Integers">

        // <Area Id = "Strings">

        // <Area Id = "Printer">

        // <Area Id = "Progess Display Control">

        // <Area Id = "Header, Footer, Status Line, TextBox">

        // <Area Id = "Display Output">

        // <Area Id = "Printer Output">

        // <Area Id = "Console_ Output">

        // <Area Id = "Input">

        // <Area Id = "System Standard Functions ">

        // <Area Id = "Character Constants">

        // <Area Id = "Ascii Delimiters">

        // <Area Id = "Pick Delimeters">

        // <Area Id = "Special Ascii Characters">

        // <Area Id = "White Space Characters">

        // <Area Id = "Null">

        // <Area Id = "Status Verbose Constants">

        // <Area Id = "System Call Function Constants">

        // </Section Summary>
        #endregion
        // #endregion
        // #region MdmClassEntryPointProperties
        #region ClassCommandsConsole_Action
        private string spClassCommandLineRequest;
        // <Area Id = "PrimaryAction">
        private string spClassFileActionRequest;
        // <Area Id = "CommandOptions (External)">
        private string spClassFileActionOptions;
        protected string sClasspFileOptions = "";
        // <Area Id = "OptionItFlags">
        protected bool bClassOptionOne = false;
        protected bool bClassOptionTwo = false;
        protected bool bClassOptionThree = false;
        protected bool bClassOptionFour = false;
        protected bool bClassOptionFive = false;
        protected bool bClassOptionSix = false;
        #endregion
        // #endregion
        // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // #region ClassConstants

        // #endregion
        // #region ClassControl
        #region ClaseInternalResults
        public int iApp_Object_Check;
        public int iApp_Core_ObjectGet;
        public int iApp_Core_ObjectReet;
        public int iApp_Core_ObjectSet;
        public int iApplicationAppObjectGet;
        public int iApplicationAppObjectSet;
        public int iApplicationMappObjectGet;
        public int iApplicationMappObjectSet;
        public int iApplicationHandlerObjectGet;
        public int iApplicationHandlerObjectSet;
        public int iApplicationMapplicationObjectGet;
        public int iApplicationMapplicationObjectSet;
        public int iApplicationMobjectObjectGet;
        public int iApplicationMobjectObjectSet;
        public int iApp_Page_ObjectGet;
        public int iApp_Page_ObjectSet;
        public int iApp_Page_2ObjectGet;
        public int iApp_Page_2ObjectSet;
        public int iApplicationVerbObjectGet;
        public int iApplicationVerbObjectSet;
        public int iApplicationMbgWorkerObjectGet;
        public int iApplicationMbgWorkerObjectSet;
        public int iMapplication;
        public int iMapplicationPassedApp;
        public int iMapplicationStartApp;

        public int iApplicationIoObjectGet;
        // Local Messages
        public string sLocalMessage = "";
        public string sLocalMessage_1 = "";
        public string sLocalMessage_2 = "";
        public string sLocalMessage_3 = "";
        #endregion
        #region ClassInternalStatusMessageDeclarations
        private string spMdm_Message_Process_Text1 = "unknown";
        public string sMdm_Message_Process_Text1 { get { return spMdm_Message_Process_Text1; } set { spMdm_Message_Process_Text1 = value; } }
        private string spMdm_Message_Process_Text2 = "unknown";
        public string sMdm_Message_Process_Text2 { get { return spMdm_Message_Process_Text2; } set { spMdm_Message_Process_Text2 = value; } }
        private string spMdm_Message_Process_Text3 = "unknown";
        public string sMdm_Message_Process_Text3 { get { return spMdm_Message_Process_Text3; } set { spMdm_Message_Process_Text3 = value; } }
        private string spMdm_Message_Process_Text4 = "unknown";
        public string sMdm_Message_Process_Text4 { get { return spMdm_Message_Process_Text4; } set { spMdm_Message_Process_Text4 = value; } }
        #endregion
        // #region MdmClassInternalProperties
        // #endregion
        // #region PrimaryDataObject
        // #endregion
        // #endregion
        // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        #region Class Mdm1 Oss1 Control1 CVS properties
        //
        #endregion
        // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        #region ApplicationManagement
        #region Class Mdm1 Author properties
        #region Class Mdm1 Author Company properties
        private int ipMdmAuthorCompanyId = 99999;
        public int iMdmAuthorCompanyId { get { return ipMdmAuthorCompanyId; } set { ipMdmAuthorCompanyId = value; } }
        private string spMdmAuthorCompanyName = "MacroDm";
        public string sMdmAuthorCompanyName { get { return spMdmAuthorCompanyName; } set { spMdmAuthorCompanyName = value; } }
        private string spMdmAuthorCompanyTitle = "unknown";
        public string sMdmAuthorCompanyTitle { get { return spMdmAuthorCompanyTitle; } set { spMdmAuthorCompanyTitle = value; } }
        private int ipMdmAuthorCompanyNumber = 99999;
        public int iMdmAuthorCompanyNumber { get { return ipMdmAuthorCompanyNumber; } set { ipMdmAuthorCompanyNumber = value; } }
        private int ipMdmAuthorCompanyIntStatus = 99999;
        public int iMdmAuthorCompanyIntStatus { get { return ipMdmAuthorCompanyIntStatus; } set { ipMdmAuthorCompanyIntStatus = value; } }
        private string spMdmAuthorCompanyStatusText = "unknown";
        public string sMdmAuthorCompanyStatusText { get { return spMdmAuthorCompanyStatusText; } set { spMdmAuthorCompanyStatusText = value; } }
        private int ipMdmAuthorCompanyIntResult = 99999;
        public int iMdmAuthorCompanyIntResult { get { return ipMdmAuthorCompanyIntResult; } set { ipMdmAuthorCompanyIntResult = value; } }
        private bool bpMdmAuthorCompanyBoolResult = false;
        public bool bMdmAuthorCompanyBoolResult { get { return bpMdmAuthorCompanyBoolResult; } set { bpMdmAuthorCompanyBoolResult = value; } }
        #endregion
        #region Class Mdm1 Author Person properties
        private int ipMdmAuthorId = 99999;
        public int iMdmAuthorId { get { return ipMdmAuthorId; } set { ipMdmAuthorId = value; } }
        private string spMdmAuthorName = "David G. Horsman";
        public string sMdmAuthorName { get { return spMdmAuthorName; } set { spMdmAuthorName = value; } }
        private string spMdmAuthorTitle = "unknown";
        public string sMdmAuthorTitle { get { return spMdmAuthorTitle; } set { spMdmAuthorTitle = value; } }
        private int ipMdmAuthorNumber = 99999;
        public int iMdmAuthorNumber { get { return ipMdmAuthorNumber; } set { ipMdmAuthorNumber = value; } }
        private int ipMdmAuthorStatus = 99999;
        public int iMdmAuthorIntStatus { get { return ipMdmAuthorStatus; } set { ipMdmAuthorStatus = value; } }
        private string spMdmAuthorStatusText = "unknown";
        public string sMdmAuthorStatusText { get { return spMdmAuthorStatusText; } set { spMdmAuthorStatusText = value; } }
        private int ipMdmAuthorIntResult = 99999;
        public int iMdmAuthorIntResult { get { return ipMdmAuthorIntResult; } set { ipMdmAuthorIntResult = value; } }
        private bool bpMdmAuthorBoolResult = false;
        public bool bMdmAuthorBoolResult { get { return bpMdmAuthorBoolResult; } set { bpMdmAuthorBoolResult = value; } }
        #endregion
        #endregion
        #region Project
        #region Project properties
        private int ipMdmProjectId = 99999;
        public int iMdmProjectId { get { return ipMdmProjectId; } set { ipMdmProjectId = value; } }
        private string spMdmProjectName = "MdmSrtVs5_0";
        public string sMdmProjectName { get { return spMdmProjectName; } set { spMdmProjectName = value; } }
        private string spMdmProjectTitle = "MdmSrtVs5_0";
        public string sMdmProjectTitle { get { return spMdmProjectTitle; } set { spMdmProjectTitle = value; } }
        private int ipMdmProjectNumber = 99999;
        public int iMdmProjectNumber { get { return ipMdmProjectNumber; } set { ipMdmProjectNumber = value; } }
        private int ipMdmProjectIntStatus = 99999;
        public int iMdmProjectIntStatus { get { return ipMdmProjectIntStatus; } set { ipMdmProjectIntStatus = value; } }
        private string spMdmProjectStatusText = "unknown";
        public string sMdmProjectStatusText { get { return spMdmProjectStatusText; } set { spMdmProjectStatusText = value; } }
        private int ipMdmProjectIntResult = 99999;
        public int iMdmProjectIntResult { get { return ipMdmProjectIntResult; } set { ipMdmProjectIntResult = value; } }
        private bool bpMdmProjectBoolResult = false;
        public bool bMdmProjectBoolResult { get { return bpMdmProjectBoolResult; } set { bpMdmProjectBoolResult = value; } }
        #endregion
        #region Task properties
        private int ipMdmTaskId = 99999;
        public int iMdmTaskId { get { return ipMdmTaskId; } set { ipMdmTaskId = value; } }
        private string spMdmTaskName = "Task0";
        public string sMdmTaskName { get { return spMdmTaskName; } set { spMdmTaskName = value; } }
        private string spMdmTaskTitle = "MdmSrtVs5_0";
        public string sMdmTaskTitle { get { return spMdmTaskTitle; } set { spMdmTaskTitle = value; } }
        private int ipMdmTaskNumber = 99999;
        public int iMdmTaskNumber { get { return ipMdmTaskNumber; } set { ipMdmTaskNumber = value; } }
        private int ipMdmTaskIntStatus = 99999;
        public int iMdmTaskIntStatus { get { return ipMdmTaskIntStatus; } set { ipMdmTaskIntStatus = value; } }
        private string spMdmTaskStatusText = "unknown";
        public string sMdmTaskStatusText { get { return spMdmTaskStatusText; } set { spMdmTaskStatusText = value; } }
        private int ipMdmTaskIntResult = 99999;
        public int iMdmTaskIntResult { get { return ipMdmTaskIntResult; } set { ipMdmTaskIntResult = value; } }
        private bool bpMdmTaskBoolResult = false;
        public bool bMdmTaskBoolResult { get { return bpMdmTaskBoolResult; } set { bpMdmTaskBoolResult = value; } }
        #endregion
        #region Task Step properties
        private int ipMdmTaskStepId = 99999;
        public int iMdmTaskStepId { get { return ipMdmTaskStepId; } set { ipMdmTaskStepId = value; } }
        private string spMdmTaskStepName = "Step0";
        public string sMdmTaskStepName { get { return spMdmTaskStepName; } set { spMdmTaskStepName = value; } }
        private string spMdmTaskStepTitle = "MdmSrtVs5_0";
        public string sMdmTaskStepTitle { get { return spMdmTaskStepTitle; } set { spMdmTaskStepTitle = value; } }
        private int ipMdmTaskStepNumber = 99999;
        public int iMdmTaskStepNumber { get { return ipMdmTaskStepNumber; } set { ipMdmTaskStepNumber = value; } }
        private int ipMdmTaskStepIntStatus = 99999;
        public int iMdmTaskStepIntStatus { get { return ipMdmTaskStepIntStatus; } set { ipMdmTaskStepIntStatus = value; } }
        private string spMdmTaskStepStatusText = "unknown";
        public string sMdmTaskStepStatusText { get { return spMdmTaskStepStatusText; } set { spMdmTaskStepStatusText = value; } }
        private int ipMdmTaskStepIntResult = 99999;
        public int iMdmTaskStepIntResult { get { return ipMdmTaskStepIntResult; } set { ipMdmTaskStepIntResult = value; } }
        private bool bpMdmTaskStepBoolResult = false;
        public bool bMdmTaskStepBoolResult { get { return bpMdmTaskStepBoolResult; } set { bpMdmTaskStepBoolResult = value; } }
        #endregion
        #endregion
        #region Solution properties
        private int ipMdmSolutionId = 99999;
        public int iMdmSolutionId { get { return ipMdmSolutionId; } set { ipMdmSolutionId = value; } }
        private string spMdmSolutionName = "MdmSrtVs5_0";
        public string sMdmSolutionName { get { return spMdmSolutionName; } set { spMdmSolutionName = value; } }
        private string spMdmSolutionTitle = "MdmSrtVs5_0";
        public string sMdmSolutionTitle { get { return spMdmSolutionTitle; } set { spMdmSolutionTitle = value; } }
        private int ipMdmSolutionNumber = 99999;
        public int iMdmSolutionNumber { get { return ipMdmSolutionNumber; } set { ipMdmSolutionNumber = value; } }
        private int ipMdmSolutionIntStatus = 99999;
        public int iMdmSolutionIntStatus { get { return ipMdmSolutionIntStatus; } set { ipMdmSolutionIntStatus = value; } }
        private string spMdmSolutionStatusText = "unknown";
        public string sMdmSolutionStatusText { get { return spMdmSolutionStatusText; } set { spMdmSolutionStatusText = value; } }
        private int ipMdmSolutionIntResult = 99999;
        public int iMdmSolutionIntResult { get { return ipMdmSolutionIntResult; } set { ipMdmSolutionIntResult = value; } }
        private bool bpMdmSolutionBoolResult = false;
        public bool bMdmSolutionBoolResult { get { return bpMdmSolutionBoolResult; } set { bpMdmSolutionBoolResult = value; } }
        #endregion
        #region Namespace properties
        private int ipMdmNamespaceId = 99999;
        public int iMdmNamespaceId { get { return ipMdmNamespaceId; } set { ipMdmNamespaceId = value; } }
        private string spMdmNamespaceName = "unknown";
        public string sMdmNamespaceName { get { return spMdmNamespaceName; } set { spMdmNamespaceName = value; } }
        private string spMdmNamespaceTitle = "unknown";
        public string sMdmNamespaceTitle { get { return spMdmNamespaceTitle; } set { spMdmNamespaceTitle = value; } }
        private int ipMdmNamespaceNumber = 99999;
        public int iMdmNamespaceNumber { get { return ipMdmNamespaceNumber; } set { ipMdmNamespaceNumber = value; } }
        private int ipMdmNamespaceIntStatus = 99999;
        public int iMdmNamespaceIntStatus { get { return ipMdmNamespaceIntStatus; } set { ipMdmNamespaceIntStatus = value; } }
        private string spMdmNamespaceStatusText = "unknown";
        public string sMdmNamespaceStatusText { get { return spMdmNamespaceStatusText; } set { spMdmNamespaceStatusText = value; } }
        private int ipMdmNamespaceIntResult = 99999;
        public int iMdmNamespaceIntResult { get { return ipMdmNamespaceIntResult; } set { ipMdmNamespaceIntResult = value; } }
        private bool bpMdmNamespaceBoolResult = false;
        public bool bMdmNamespaceBoolResult { get { return bpMdmNamespaceBoolResult; } set { bpMdmNamespaceBoolResult = value; } }
        #endregion
        #region Assembly properties
        private int ipMdmAssemblyId = 99999;
        public int iMdmAssemblyId { get { return ipMdmAssemblyId; } set { ipMdmAssemblyId = value; } }
        private string spMdmAssemblyName = "unknown";
        public string sMdmAssemblyName { get { return spMdmAssemblyName; } set { spMdmAssemblyName = value; } }
        private string spMdmAssemblyTitle = "unknown";
        public string sMdmAssemblyTitle { get { return spMdmAssemblyTitle; } set { spMdmAssemblyTitle = value; } }
        private int ipMdmAssemblyNumber = 99999;
        public int iMdmAssemblyNumber { get { return ipMdmAssemblyNumber; } set { ipMdmAssemblyNumber = value; } }
        private int ipMdmAssemblyIntStatus = 99999;
        public int iMdmAssemblyIntStatus { get { return ipMdmAssemblyIntStatus; } set { ipMdmAssemblyIntStatus = value; } }
        private string spMdmAssemblyStatusText = "unknown";
        public string sMdmAssemblyStatusText { get { return spMdmAssemblyStatusText; } set { spMdmAssemblyStatusText = value; } }
        private int ipMdmAssemblyIntResult = 99999;
        public int iMdmAssemblyIntResult { get { return ipMdmAssemblyIntResult; } set { ipMdmAssemblyIntResult = value; } }
        private bool bpMdmAssemblyBoolResult = false;
        public bool bMdmAssemblyBoolResult { get { return bpMdmAssemblyBoolResult; } set { bpMdmAssemblyBoolResult = value; } }
        #endregion
        #region Class Mdm1 Oss1 Mobject
        #region System properties
        private int ipMdmSystemId = 99999;
        public int iMdmSystemId { get { return ipMdmSystemId; } set { ipMdmSystemId = value; } }
        private string spMdmSystemName = "unknown";
        public string sMdmSystemName { get { return spMdmSystemName; } set { spMdmSystemName = value; } }
        private string spMdmSystemTitle = "unknown";
        public string sMdmSystemTitle { get { return spMdmSystemTitle; } set { spMdmSystemTitle = value; } }
        private int ipMdmSystemNumber = 99999;
        public int iMdmSystemNumber { get { return ipMdmSystemNumber; } set { ipMdmSystemNumber = value; } }
        private int ipMdmSystemIntStatus = 99999;
        public int iMdmSystemIntStatus { get { return ipMdmSystemIntStatus; } set { ipMdmSystemIntStatus = value; } }
        private string spMdmSystemStatusText = "unknown";
        public string sMdmSystemStatusText { get { return spMdmSystemStatusText; } set { spMdmSystemStatusText = value; } }
        private int ipMdmSystemIntResult = 99999;
        public int iMdmSystemIntResult { get { return ipMdmSystemIntResult; } set { ipMdmSystemIntResult = value; } }
        private bool bpMdmSystemResult = false;
        public bool bMdmSystemResult { get { return bpMdmSystemResult; } set { bpMdmSystemResult = value; } }
        #endregion
        #region Process properties
        private int ipMdm_Process_Id = 99999;
        public int iMdm_Process_Id { get { return ipMdm_Process_Id; } set { ipMdm_Process_Id = value; } }
        private string spMdm_Process_Name = "unknown";
        public string sMdm_Process_Name { get { return spMdm_Process_Name; } set { spMdm_Process_Name = value; } }
        private string spMdm_Process_Title = "unknown";
        public string sMdm_Process_Title { get { return spMdm_Process_Title; } set { spMdm_Process_Title = value; } }
        private int ipMdm_Process_Number = 99999;
        public int iMdm_Process_Number { get { return ipMdm_Process_Number; } set { ipMdm_Process_Number = value; } }
        private int ipsMdm_Message_Process_ = 99999;
        public int isMdm_Message_Process_ { get { return ipsMdm_Message_Process_; } set { ipsMdm_Message_Process_ = value; } }
        private string spMdm_Message_Process_Text = "unknown";
        public string sMdm_Message_Process_Text { get { return spMdm_Message_Process_Text; } set { spMdm_Message_Process_Text = value; } }
        private int ipMdm_Process_IntResult = 99999;
        public int iMdm_Process_IntResult { get { return ipMdm_Process_IntResult; } set { ipMdm_Process_IntResult = value; } }
        private bool bpMdm_Process_BoolResult = false;
        public bool bMdm_Process_BoolResult { get { return bpMdm_Process_BoolResult; } set { bpMdm_Process_BoolResult = value; } }
        #endregion
        /* #region Status Message properties
private string spMdm_Message_Process_Text1 = "unknown";
public string sMdm_Message_Process_Text1 { get { return spMdm_Message_Process_Text1; } set { spMdm_Message_Process_Text1 = value; }}
private string spMdm_Message_Process_Text2 = "unknown";
public string sMdm_Message_Process_Text2 { get { return spMdm_Message_Process_Text2; } set { spMdm_Message_Process_Text2 = value; }}
private string spMdm_Message_Process_Text3 = "unknown";
public string sMdm_Message_Process_Text3 { get { return spMdm_Message_Process_Text3; } set { spMdm_Message_Process_Text3 = value; }}
private string spMdm_Message_Process_Text4 = "unknown";
public string sMdm_Message_Process_Text4 { get { return spMdm_Message_Process_Text4; } set { spMdm_Message_Process_Text4 = value; }}
        #endregion
         */
        #region Class properties
        private int ipMdmClassId = 99999;
        public int iMdmClassId { get { return ipMdmClassId; } set { ipMdmClassId = value; } }
        private string spMdmClassName = "unknown";
        public string sMdmClassName { get { return spMdmClassName; } set { spMdmClassName = value; } }
        private string spMdmClassTitle = "unknown";
        public string sMdmClassTitle { get { return spMdmClassTitle; } set { spMdmClassTitle = value; } }
        private int ipMdmClassNumber = 99999;
        public int iMdmClassNumber { get { return ipMdmClassNumber; } set { ipMdmClassNumber = value; } }
        private int ipMdmClassIntStatus = 99999;
        public int iMdmClassIntStatus { get { return ipMdmClassIntStatus; } set { ipMdmClassIntStatus = value; } }
        private string spMdmClassStatusText = "unknown";
        public string sMdmClassStatusText { get { return spMdmClassStatusText; } set { spMdmClassStatusText = value; } }
        private int ipMdm_Class_Result = 99999;
        public int iMdm_Class_Result { get { return ipMdm_Class_Result; } set { ipMdm_Class_Result = value; } }
        private bool bpMdmClassBoolResult = false;
        public bool bMdmClassBoolResult { get { return bpMdmClassBoolResult; } set { bpMdmClassBoolResult = value; } }
        #endregion
        #region Method properties
        private int ipMdmMethodId = 99999;
        public int iMdmMethodId { get { return ipMdmMethodId; } set { ipMdmMethodId = value; } }
        private string spMdmMethodName = "unknown";
        public string sMdmMethodName { get { return spMdmMethodName; } set { spMdmMethodName = value; } }
        private string spMdmMethodTitle = "unknown";
        public string sMdmMethodTitle { get { return spMdmMethodTitle; } set { spMdmMethodTitle = value; } }
        private int ipMdmMethodNumber = 99999;
        public int iMdmMethodNumber { get { return ipMdmMethodNumber; } set { ipMdmMethodNumber = value; } }
        private int ipMdmMethodStatus = 99999;
        public int iMdmMethodStatus { get { return ipMdmMethodStatus; } set { ipMdmMethodStatus = value; } }
        private string spMdmMethodStatusText = "unknown";
        public string sMdmMethodStatusText { get { return spMdmMethodStatusText; } set { spMdmMethodStatusText = value; } }
        private int ipMdm_Method_Result = 99999;
        public int iMdm_Method_Result { get { return ipMdm_Method_Result; } set { ipMdm_Method_Result = value; } }
        private bool bpMdmMethodBoolResult = false;
        public bool bMdmMethodBoolResult { get { return bpMdmMethodBoolResult; } set { bpMdmMethodBoolResult = value; } }
        #endregion
        #region Attribute properties
        private int ipMdmAttributeId = 99999;
        public int iMdmAttributeId { get { return ipMdmAttributeId; } set { ipMdmAttributeId = value; } }
        private string spMdmAttributeName = "unknown";
        public string sMdmAttributeName { get { return spMdmAttributeName; } set { spMdmAttributeName = value; } }
        private string spMdmAttributeTitle = "unknown";
        public string sMdmAttributeTitle { get { return spMdmAttributeTitle; } set { spMdmAttributeTitle = value; } }
        private int ipMdmAttributeNumber = 99999;
        public int iMdmAttributeNumber { get { return ipMdmAttributeNumber; } set { ipMdmAttributeNumber = value; } }
        private int ipMdmAttributeStatus = 99999;
        public int iMdmAttributeStatus { get { return ipMdmAttributeStatus; } set { ipMdmAttributeStatus = value; } }
        private string spMdmAttributeStatusText = "unknown";
        public string sMdmAttributeStatusText { get { return spMdmAttributeStatusText; } set { spMdmAttributeStatusText = value; } }
        private int ipMdmAttributeIntResult = 99999;
        public int iMdmAttributeIntResult { get { return ipMdmAttributeIntResult; } set { ipMdmAttributeIntResult = value; } }
        private bool bpMdmAttributeBoolResult = false;
        public bool bMdmAttributeBoolResult { get { return bpMdmAttributeBoolResult; } set { bpMdmAttributeBoolResult = value; } }
        #endregion
        #region Parameter properties
        private int ipMdmParameterId = 99999;
        public int iMdmAuthorParameterId { get { return ipMdmParameterId; } set { ipMdmParameterId = value; } }
        private string spMdmParameterName = "unknown";
        public string sMdmParameterName { get { return spMdmParameterName; } set { spMdmParameterName = value; } }
        private int ipMdmParameterNumber = 99999;
        public int iMdmParameterNumber { get { return ipMdmParameterNumber; } set { ipMdmParameterNumber = value; } }
        private string spMdmParameterTitle = "unknown";
        public string sMdmParameterTitle { get { return spMdmParameterTitle; } set { spMdmParameterTitle = value; } }
        private int ipMdmParameterStatus = 99999;
        public int iMdmParameterStatus { get { return ipMdmParameterStatus; } set { ipMdmParameterStatus = value; } }
        private string spMdmParameterStatusText = "unknown";
        public string sMdmParameterStatusText { get { return spMdmParameterStatusText; } set { spMdmParameterStatusText = value; } }
        private int ipMdmParameterIntResult = 99999;
        public int iMdmParameterIntResult { get { return ipMdmParameterIntResult; } set { ipMdmParameterIntResult = value; } }
        private bool bpMdmParameterBoolResult = false;
        public bool bMdmParameterBoolResult { get { return bpMdmParameterBoolResult; } set { bpMdmParameterBoolResult = value; } }
        #endregion
        #region Property properties
        private int ipMdmPropertyId = 99999;
        public int iMdmAuthorPropertyId { get { return ipMdmPropertyId; } set { ipMdmPropertyId = value; } }
        private string spMdmPropertyName = "unknown";
        public string sMdmPropertyName { get { return spMdmPropertyName; } set { spMdmPropertyName = value; } }
        private int ipMdmPropertyNumber = 99999;
        public int iMdmPropertyNumber { get { return ipMdmPropertyNumber; } set { ipMdmPropertyNumber = value; } }
        private string spMdmPropertyTitle = "unknown";
        public string sMdmPropertyTitle { get { return spMdmPropertyTitle; } set { spMdmPropertyTitle = value; } }
        private int ipMdmPropertyStatus = 99999;
        public int iMdmPropertyStatus { get { return ipMdmPropertyStatus; } set { ipMdmPropertyStatus = value; } }
        private string spMdmPropertyStatusText = "unknown";
        public string sMdmPropertyStatusText { get { return spMdmPropertyStatusText; } set { spMdmPropertyStatusText = value; } }
        private int ipMdmPropertyIntResult = 99999;
        public int iMdmPropertyIntResult { get { return ipMdmPropertyIntResult; } set { ipMdmPropertyIntResult = value; } }
        private bool bpMdmPropertyBoolResult = false;
        public bool bMdmPropertyBoolResult { get { return bpMdmPropertyBoolResult; } set { bpMdmPropertyBoolResult = value; } }
        #endregion
        #endregion
        #region RunControlManagement properties
        #region Command properties
        private int ipMdmCommandId = 99999;
        public int iMdmCommandId { get { return ipMdmCommandId; } set { ipMdmCommandId = value; } }
        private string spMdmCommandName = "unknown";
        public string sMdmCommandName { get { return spMdmCommandName; } set { spMdmCommandName = value; } }
        private string spMdmCommandTitle = "unknown";
        public string sMdmCommandTitle { get { return spMdmCommandTitle; } set { spMdmCommandTitle = value; } }
        private int ipMdmCommandNumber = 99999;
        public int iMdmCommandNumber { get { return ipMdmCommandNumber; } set { ipMdmCommandNumber = value; } }
        private int ipMdmCommandStatus = 99999;
        public int iMdmCommandStatus { get { return ipMdmCommandStatus; } set { ipMdmCommandStatus = value; } }
        private string spMdmCommandStatusText = "unknown";
        public string sMdmCommandStatusText { get { return spMdmCommandStatusText; } set { spMdmCommandStatusText = value; } }
        private int ipMdmCommandIntResult = 99999;
        public int iMdmCommandIntResult { get { return ipMdmCommandIntResult; } set { ipMdmCommandIntResult = value; } }
        private bool bpMdmCommandBoolResult = false;
        public bool bMdmCommandBoolResult { get { return bpMdmCommandBoolResult; } set { bpMdmCommandBoolResult = value; } }
        #endregion
        #region RunControl properties
        private int ipMdmRunId = 99999;
        public int iMdmRunId { get { return ipMdmRunId; } set { ipMdmRunId = value; } }
        private string spMdmRunName = "unknown";
        public string sMdmRunName { get { return spMdmRunName; } set { spMdmRunName = value; } }
        private string spMdmRunTitle = "unknown";
        public string sMdmRunTitle { get { return spMdmRunTitle; } set { spMdmRunTitle = value; } }
        private int ipMdmRunNumber = 99999;
        public int iMdmRunNumber { get { return ipMdmRunNumber; } set { ipMdmRunNumber = value; } }
        private int ipMdmRunStatus = 99999;
        public int iMdmRunStatus { get { return ipMdmRunStatus; } set { ipMdmRunStatus = value; } }
        private string spMdmRunStatusText = "unknown";
        public string sMdmRunStatusText { get { return spMdmRunStatusText; } set { spMdmRunStatusText = value; } }
        private int ipMdmRunIntResult = 99999;
        public int iMdmRunIntResult { get { return ipMdmRunIntResult; } set { ipMdmRunIntResult = value; } }
        private bool bpMdmRunBoolResult = false;
        public bool bMdmRunBoolResult { get { return bpMdmRunBoolResult; } set { bpMdmRunBoolResult = value; } }
        #endregion
        #region AutoRun properties
        private int ipMdmAutoRunId = 99999;
        public int iMdmAutoRunId { get { return ipMdmAutoRunId; } set { ipMdmAutoRunId = value; } }
        private string spMdmAutoRunName = "unknown";
        public string sMdmAutoRunName { get { return spMdmAutoRunName; } set { spMdmAutoRunName = value; } }
        private string spMdmAutoRunTitle = "unknown";
        public string sMdmAutoRunTitle { get { return spMdmAutoRunTitle; } set { spMdmAutoRunTitle = value; } }
        private int ipMdmAutoRunNumber = 99999;
        public int iMdmAutoRunNumber { get { return ipMdmAutoRunNumber; } set { ipMdmAutoRunNumber = value; } }
        private int ipMdmAutoRunStatus = 99999;
        public int iMdmAutoRunStatus { get { return ipMdmAutoRunStatus; } set { ipMdmAutoRunStatus = value; } }
        private string spMdmAutoRunStatusText = "unknown";
        public string sMdmAutoRunStatusText { get { return spMdmAutoRunStatusText; } set { spMdmAutoRunStatusText = value; } }
        private int ipMdmAutoRunIntResult = 99999;
        public int iMdmAutoRunIntResult { get { return ipMdmAutoRunIntResult; } set { ipMdmAutoRunIntResult = value; } }
        private bool bpMdmAutoRunBoolResult = false;
        public bool bMdmAutoRunBoolResult { get { return bpMdmAutoRunBoolResult; } set { bpMdmAutoRunBoolResult = value; } }
        #endregion
        #region Input properties
        private int ipMdmInputId = 99999;
        public int iMdmInputId { get { return ipMdmInputId; } set { ipMdmInputId = value; } }
        private string spMdmInputName = "unknown";
        public string sMdmInputName { get { return spMdmInputName; } set { spMdmInputName = value; } }
        private string spMdmInputTitle = "unknown";
        public string sMdmInputTitle { get { return spMdmInputTitle; } set { spMdmInputTitle = value; } }
        private int ipMdmInputNumber = 99999;
        public int iMdmInputNumber { get { return ipMdmInputNumber; } set { ipMdmInputNumber = value; } }
        private int ipMdmInputStatus = 99999;
        public int iMdmInputStatus { get { return ipMdmInputStatus; } set { ipMdmInputStatus = value; } }
        private string spMdmInputStatusText = "unknown";
        public string sMdmInputStatusText { get { return spMdmInputStatusText; } set { spMdmInputStatusText = value; } }
        private int ipMdmInputIntResult = 99999;
        public int iMdmInputIntResult { get { return ipMdmInputIntResult; } set { ipMdmInputIntResult = value; } }
        private bool bpMdmInputBoolResult = false;
        public bool bMdmInputBoolResult { get { return bpMdmInputBoolResult; } set { bpMdmInputBoolResult = value; } }
        #endregion
        #region Output properties
        private int ipMdmOutputId = 99999;
        public int iMdmOutputId { get { return ipMdmOutputId; } set { ipMdmOutputId = value; } }
        private string spMdmOutputName = "unknown";
        public string sMdmOutputName { get { return spMdmOutputName; } set { spMdmOutputName = value; } }
        private string spMdmOutputTitle = "unknown";
        public string sMdmOutputTitle { get { return spMdmOutputTitle; } set { spMdmOutputTitle = value; } }
        private int ipMdmOutputNumber = 99999;
        public int iMdmOutputNumber { get { return ipMdmOutputNumber; } set { ipMdmOutputNumber = value; } }
        private int ipMdmOutputStatus = 99999;
        public int iMdmOutputStatus { get { return ipMdmOutputStatus; } set { ipMdmOutputStatus = value; } }
        private string spMdmOutputStatusText = "unknown";
        public string sMdmOutputStatusText { get { return spMdmOutputStatusText; } set { spMdmOutputStatusText = value; } }
        private int ipMdmOutputIntResult = 99999;
        public int iMdmOutputIntResult { get { return ipMdmOutputIntResult; } set { ipMdmOutputIntResult = value; } }
        private bool bpMdmOutputBoolResult = false;
        public bool bMdmOutputBoolResult { get { return bpMdmOutputBoolResult; } set { bpMdmOutputBoolResult = value; } }
        #endregion
        #endregion
        //
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region ClassExternal properties
        private int ipExternalId = 99999;
        public int iExternalId { get { return ipExternalId; } set { ipExternalId = value; } }
        private string spExternalName = "unknown";
        public string sExternalName { get { return spExternalName; } set { spExternalName = value; } }
        private string spExternalTitle = "unknown";
        public string sExternalTitle { get { return spExternalTitle; } set { spExternalTitle = value; } }
        private int ipExternalNumber = 99999;
        public int iExternalNumber { get { return ipExternalNumber; } set { ipExternalNumber = value; } }
        private int ipExternalStatus = 99999;
        public int iExternalStatus { get { return ipExternalStatus; } set { ipExternalStatus = value; } }
        private string spExternalStatusText = "unknown";
        public string sExternalStatusText { get { return spExternalStatusText; } set { spExternalStatusText = value; } }
        private int ipExternalResult = 99999;
        public int iExternalResult { get { return ipExternalResult; } set { ipExternalResult = value; } }
        private bool bpExternalResult = false;
        public bool bExternalResult { get { return bpExternalResult; } set { bpExternalResult = value; } }
        private string spExternalResult;
        public string sExternalResult { get { return spExternalResult; } set { spExternalResult = value; } }
        public float fpExternalResult;
        public float fExternalResult { get { return fpExternalResult; } set { fpExternalResult = value; } }
        private object oopExternalResult;
        public object ooExternalResult { get { return oopExternalResult; } set { oopExternalResult = (object)value; } }
        public bool bpExternalObjectDoesExist;
        public bool bExternalObjectDoesExist { get { return bpExternalObjectDoesExist; } set { bpExternalObjectDoesExist = value; } }
        #endregion
        #region ClassInternal properties
        private int ipInternalId = 99999;
        public int iInternalId { get { return ipInternalId; } set { ipInternalId = value; } }
        private string spInternalName = "unknown";
        public string sInternalName { get { return spInternalName; } set { spInternalName = value; } }
        private string spInternalTitle = "unknown";
        public string sInternalTitle { get { return spInternalTitle; } set { spInternalTitle = value; } }
        private int ipInternalNumber = 99999;
        public int iInternalNumber { get { return ipInternalNumber; } set { ipInternalNumber = value; } }
        private int ipInternalStatus = 99999;
        public int iInternalStatus { get { return ipInternalStatus; } set { ipInternalStatus = value; } }
        private string spInternalStatusText = "unknown";
        public string sInternalStatusText { get { return spInternalStatusText; } set { spInternalStatusText = value; } }
        private int ipInternalIntResult = 99999;
        public int iInternalIntResult { get { return ipInternalIntResult; } set { ipInternalIntResult = value; } }
        private bool bpInternalBoolResult = false;
        public bool bInternalBoolResult { get { return bpInternalBoolResult; } set { bpInternalBoolResult = value; } }
        #endregion
        #region LocalResult properties
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
        private string isLocalResult;
        public string LocalStringResult { get { return isLocalResult; } set { isLocalResult = value; } }
        private bool bpLocalResult;
        public bool bLocalResult { get { return bpLocalResult; } set { bpLocalResult = value; } }
        private object oopLocalResult;
        public object LocalResult { get { return oopLocalResult; } set { oopLocalResult = (object)value; } }
        private bool bpLocalObjectDoesExist;
        public bool LocalObjectDoesExist { get { return bpLocalObjectDoesExist; } set { bpLocalObjectDoesExist = value; } }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        //
        #region ClassApplication properties
        private int ipMdmApplicationId = 99999;
        public int iMdmApplicationId { get { return ipMdmApplicationId; } set { ipMdmApplicationId = value; } }
        private string spMdmApplicationName = "unknown";
        public string sMdmApplicationName { get { return spMdmApplicationName; } set { spMdmApplicationName = value; } }
        private string spMdmApplicationTitle = "unknown";
        public string sMdmApplicationTitle { get { return spMdmApplicationTitle; } set { spMdmApplicationTitle = value; } }
        private int ipMdmApplicationNumber = 99999;
        public int iMdmApplicationNumber { get { return ipMdmApplicationNumber; } set { ipMdmApplicationNumber = value; } }
        private int ipMdmApplicationStatus = 99999;
        public int iMdmApplicationStatus { get { return ipMdmApplicationStatus; } set { ipMdmApplicationStatus = value; } }
        private string spMdmApplicationStatusText = "unknown";
        public string sMdmApplicationStatusText { get { return spMdmApplicationStatusText; } set { spMdmApplicationStatusText = value; } }
        private int ipMdmApplicationIntResult = 99999;
        public int iMdmApplicationIntResult { get { return ipMdmApplicationIntResult; } set { ipMdmApplicationIntResult = value; } }
        private bool bpMdmApplicationBoolResult = false;
        public bool bMdmApplicationBoolResult { get { return bpMdmApplicationBoolResult; } set { bpMdmApplicationBoolResult = value; } }
        #endregion
        #region ClassOther properties
        private int ipMdmOtherId = 99999;
        public int iMdmOtherId { get { return ipMdmOtherId; } set { ipMdmOtherId = value; } }
        private string spMdmOtherName = "unknown";
        public string sMdmOtherName { get { return spMdmOtherName; } set { spMdmOtherName = value; } }
        private string spMdmOtherTitle = "unknown";
        public string sMdmOtherTitle { get { return spMdmOtherTitle; } set { spMdmOtherTitle = value; } }
        private int ipMdmOtherNumber = 99999;
        public int iMdmOtherNumber { get { return ipMdmOtherNumber; } set { ipMdmOtherNumber = value; } }
        private int ipMdmOtherStatus = 99999;
        public int iMdmOtherStatus { get { return ipMdmOtherStatus; } set { ipMdmOtherStatus = value; } }
        private string spMdmOtherStatusText = "unknown";
        public string sMdmOtherStatusText { get { return spMdmOtherStatusText; } set { spMdmOtherStatusText = value; } }
        private int ipMdmOtherIntResult = 99999;
        public int iMdmOtherIntResult { get { return ipMdmOtherIntResult; } set { ipMdmOtherIntResult = value; } }
        private bool bpMdmOtherBoolResult = false;
        public bool bMdmOtherBoolResult { get { return bpMdmOtherBoolResult; } set { bpMdmOtherBoolResult = value; } }
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        // Class Internals - Properties Fields and Attributes
        #region MapplicationClassInstanceManagement

        #region MapplicationConstructor
        public Mapplication() : base() {
            iMapplication = (int) MethodStateIs.Start;
            //
            MapplicationStartApp();
        }

        public Mapplication(object omPassedO)
            : base() {
            iMapplicationPassedApp = (int) MethodStateIs.Start;
            if (omPassedO != null) {
                if (omOb == null) {
                    omOb = omPassedO;
                }
            }
            MapplicationStartApp();
        }

        #endregion
        #region MapplicationEngine
        public int MapplicationStartApp()
        {
            iMapplicationStartApp = (int) MethodStateIs.Start;
            //
            return iMapplicationStartApp;
        }
        #endregion
        #region MapplicationCoreObjectReset
        public void MapplicationCoreObjectReset() {
            // Run properties
            ipMdmRunId = 99999;
            spMdmRunName = "unknown";
            spMdmRunTitle = "unknown";
            ipMdmRunNumber = 99999;
            ipMdmRunStatus = 99999;
            spMdmRunStatusText = "unknown";
            ipMdmRunIntResult = 99999;
            bpMdmRunBoolResult = false;
            // <Segment Summary>
            // AutoRun properties
            // </Segment Summary>
            ipMdmAutoRunId = 99999;
            spMdmAutoRunName = "unknown";
            spMdmAutoRunTitle = "unknown";
            ipMdmAutoRunNumber = 99999;
            ipMdmAutoRunStatus = 99999;
            spMdmAutoRunStatusText = "unknown";
            ipMdmAutoRunIntResult = 99999;
            bpMdmAutoRunBoolResult = false;
            // <Segment Summary>
            // Input properties
            // </Segment Summary>
            ipMdmInputId = 99999;
            spMdmInputName = "unknown";
            spMdmInputTitle = "unknown";
            ipMdmInputNumber = 99999;
            ipMdmInputStatus = 99999;
            spMdmInputStatusText = "unknown";
            ipMdmInputIntResult = 99999;
            bpMdmInputBoolResult = false;
            // <Segment Summary>
            // Output properties
            // </Segment Summary>
            ipMdmOutputId = 99999;
            spMdmOutputName = "unknown";
            spMdmOutputTitle = "unknown";
            ipMdmOutputNumber = 99999;
            ipMdmOutputStatus = 99999;
            spMdmOutputStatusText = "unknown";
            ipMdmOutputIntResult = 99999;
            bpMdmOutputBoolResult = false;
            // 
            // <Segment Summary>
            // Class External properties
            // </Segment Summary>
            // 
            ipExternalId = 99999;
            spExternalName = "unknown";
            spExternalTitle = "unknown";
            ipExternalNumber = 99999;
            ipExternalStatus = 99999;
            spExternalStatusText = "unknown";
            ipExternalResult = 99999;
            bpExternalResult = false;
            // 
            // <Segment Summary>
            // Class Internal properties
            // </Segment Summary>
            //
            ipInternalId = 99999;
            spInternalName = "unknown";
            spInternalTitle = "unknown";
            ipInternalNumber = 99999;
            ipInternalStatus = 99999;
            spInternalStatusText = "unknown";
            ipInternalIntResult = 99999;
            bpInternalBoolResult = false;
            // 
            // <Segment Summary>
            // Class Application properties
            // </Segment Summary>
            ipMdmApplicationId = 99999;
            spMdmApplicationName = "unknown";
            spMdmApplicationTitle = "unknown";
            ipMdmApplicationNumber = 99999;
            ipMdmApplicationStatus = 99999;
            spMdmApplicationStatusText = "unknown";
            ipMdmApplicationIntResult = 99999;
            bpMdmApplicationBoolResult = false;
            // 
            // 
            // <Segment Summary>
            // Class Other properties
            // </Segment Summary>
            ipMdmOtherId = 99999;
            spMdmOtherName = "unknown";
            spMdmOtherTitle = "unknown";
            ipMdmOtherNumber = 99999;
            ipMdmOtherStatus = 99999;
            spMdmOtherStatusText = "unknown";
            ipMdmOtherIntResult = 99999;
            bpMdmOtherBoolResult = false;
        }
        #endregion
        #region MapplicationAccessors
        #region MdmApplicationOjbectGet
        public int App_Core_ObjectGet(object omPassedO) {
            iApp_Core_ObjectGet = (int) MethodStateIs.Start;
            // App omOb;
            iApp_Core_ObjectGet = App_Core_ObjectCheck((object)omPassedO, (object) omAp, "Mdm.Oss.Mapp.Application");
            if (omPassedO != null && omOb == null) {
                omOb = omPassedO;
                if (omOb == null) {
                    // omOb = omPassedO;
                    // omOb = omOb.GetMapplicationObject();
                    // omOb = (Mapplication)ApplicationMapplicationObjectGet();
                    omOb = this;
                    // omOb.SetMapject(this);
                }
                if (omPassedO != this.omOb) {
                    iApp_Core_ObjectGet = (int) MethodStateIs.NameShouldNotExist;
                }
                // if (omPassedO.omAp != this) {
                   //  iApp_Core_ObjectGet = (int) MethodStateIs.NameShouldNotExist;
                // }
            }
            // App omAp
            if (omAp == null) {
                // omAp = ApplicationAppObjectGet();
                // omAp = (Application)ApplicationAppObjectGet();
                // omAp = this;
                omAp = Application.Current;
                // ApplicationAppObjectSet(this);
            }
            // App omHa;
            if (omHa == null) {
                omHa = ApplicationHandlerObjectGet();
                // omHa = ApplicationHandlerObjectGet();
                // omHa = this;
                // ApplicationHandlerObjectSet(this);
            }
            // App Page;
            if (omPa == null) {
                omPa = App_Page_ObjectGet();
                omPa = omPa;
                omPa = omPa;
                // omPa = (Page)App_Page_ObjectGet();
                // omPa = this
                // App_Page_ObjectSet(this);
            }
            // App Page2;
            if (omPa2 == null) {
                omPa2 = App_Page_2ObjectGet();
                omPa2 = omPa2;
                // omPa2 = (Page2)App_Page_2ObjectGet();
                // omPa2 = this
                // App_Page_2ObjectSet(this);
            }
            // App Verb
            if (omVe == null) {
                omVe = ApplicationVerbObjectGet();
                // omVe = (MimportTldThread)ApplicationVerbObjectGet();
                // omVe = this;
                // ApplicationVerbObjectSet(this);
            }
            // App omAp
            if (omMa == null) {
                omMa = ApplicationMappObjectGet();
                // omMa = (Mapplication)ApplicationMappObjectGet();
                // omMa = this;
                // ApplicationMappObjectSet(this);
            }
            // App BgWorker
            if (omWo == null) {
                omWo = ApplicationMbgWorkerObjectGet();
                // omWo = (MimportTldThread)ApplicationMbgWorkerObjectGet();
                // omWo = this;
                // ApplicationMbgWorkerObjectSet(this);
            }
            return iApp_Core_ObjectGet;
        }
        public int App_Core_ObjectCheck(object ooPassedObject, object ooPassedInternalObject, string sPassedObjectType) {
            iApp_Object_Check = (int) MethodStateIs.Start;
            //
            try {
                odstMethodObjectType = ooPassedObject.GetType();
                iMethodObjectHashCode = ooPassedObject.GetHashCode();
                sMethodObjectToString = ooPassedObject.ToString();
                bMethodObjectEquality = true;
                // 
                // 
                if (ooPassedObject != null) { 
                    bMethodObjectExternalExistance = true;
                    if (ooPassedInternalObject == null) {
                        ooPassedInternalObject = ooPassedObject;
                    } else {
                        if (ooPassedInternalObject == ooPassedObject) {
                            bMethodObjectEquality = true;
                            bMethodObjectTypeValid = true;
                        } else {
                            bMethodObjectEquality = false;
                            bMethodObjectTypeValid = false;
                        }
                    }
                } else { 
                    bMethodObjectExternalExistance = false;
                    bMethodObjectEquality = false;
                    if (ooPassedInternalObject == null) { 
                        bMethodObjectInternalExistance = false;
                    } else { 
                        bMethodObjectInternalExistance = true;
                        bMethodObjectTypeValid = true;
                    }
                }
            } catch (SystemException omveValidationException) {
                //
                bMethodObjectTypeValid = false;
                //
            } finally {
                //
                if (ooPassedInternalObject != null) { 
                    bMethodObjectInternalExistance = true;
                    sMethodObjectToString = ooPassedInternalObject.ToString();
                    odstMethodObjectType = ooPassedInternalObject.GetType();
                    sMethodObjectType = odstMethodObjectType.ToString();
                    if (sPassedObjectType != null) {
                        if (sPassedObjectType.Length > 0) {
                            if (sMethodObjectType != sPassedObjectType) {
                                bMethodObjectTypeValid = false;
                            } else { bMethodObjectTypeValid = true; };
                        } else { bMethodObjectTypeValid = true; };
                    } else {
                        bMethodObjectInternalExistance = false; 
                    }
                } else { 
                    bMethodObjectInternalExistance = false; 
                }
            }
            return iApp_Object_Check;
        }

        #endregion
        #region MdmApp_Object_InstanceAccessors
        public Application ApplicationAppObjectGet() {
            iApplicationAppObjectGet = (int) MethodStateIs.ObjectOK;
            if (omAp == null) {
                omAp = Application.Current;
            }
            return omAp;
            // iApplicationAppObjectGet
        }
        public int ApplicationAppObjectSet(Application omaPassedObject) {
            iApplicationAppObjectSet = (int) MethodStateIs.ObjectOK;
            // App omOb;
            iApplicationAppObjectSet = App_Core_ObjectCheck((object) omaPassedObject, (object)omAp, "Mdm.Oss.Mapp.Application");
            if (omaPassedObject == null) {
                omAp = Application.Current;
                iApplicationHandlerObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
            } else if (omAp == null && omaPassedObject != null) {
                omAp = omaPassedObject;
            } else if (omAp != omaPassedObject) {
                iApplicationAppObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
                if (omAp != omaPassedObject) {
                    iApplicationMobjectObjectSet = (int) MethodStateIs.ObjectShouldNotExist;
                }
            }
            return iApplicationAppObjectSet;
        }
        public object ApplicationMappObjectGet() {
            iApplicationMappObjectGet = (int) MethodStateIs.ObjectOK;
            if (omMa == null) {
                omMa = this;
            }
            return omMa;
        }
        public int ApplicationMappObjectSet(Application ommPassedObject) {
            iApplicationAppObjectSet = (int) MethodStateIs.ObjectOK;
            // App omOb;
            iApplicationAppObjectSet = App_Core_ObjectCheck((object)ommPassedObject, (object)omMa, "Mdm.Oss.Mapp.Application");
            if (ommPassedObject == null) {
                omMa = Application.Current;
                iApplicationHandlerObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
            } else if (omMa == null && ommPassedObject != null) {
                omMa = ommPassedObject;
            } else if (omMa != ommPassedObject) {
                iApplicationAppObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
                if (omMa != ommPassedObject) {
                    iApplicationMobjectObjectSet = (int) MethodStateIs.ObjectShouldNotExist;
                }
            }
            return iApplicationAppObjectSet;
        }
        //
        // iMapplicationMappObjectGet
        public int ApplicationMappObjectSet(object ommPassedObject) {
            iApplicationMappObjectSet = (int) MethodStateIs.ObjectOK;
            // Mapp omOb;
            iApplicationMappObjectSet = App_Core_ObjectCheck((object)ommPassedObject, (object)omMa, "Mdm1Oss1MMapplication1.Mapplication");
            if (ommPassedObject == null) {
                iApplicationHandlerObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
            } else if (omMa == null && ommPassedObject != null) {
                omMa = ommPassedObject;
            } else if (omMa != ommPassedObject) {
                iApplicationMappObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
                if (omMa != ommPassedObject) {
                    iApplicationMobjectObjectSet = (int) MethodStateIs.ObjectShouldNotExist;
                }
            }
            return iApplicationMappObjectSet;
        }
        public object ApplicationHandlerObjectGet() {
            iApplicationHandlerObjectGet = (int) MethodStateIs.ObjectOK;
            if (omHa == null) {
                string stemp99 = omPa.Parent.ToString();
                iApplicationHandlerObjectGet = (int) MethodStateIs.ObjectDoesNotExist;
                return null;
            }
            return omHa;
            // iApplicationHandlerObjectGet
        }
        public int ApplicationHandlerObjectSet(object omhPassedObject) {
            iApplicationHandlerObjectSet = (int) MethodStateIs.ObjectOK;
            iApplicationHandlerObjectSet = App_Core_ObjectCheck((object)omhPassedObject, (object)omHa, "Mdm.Oss.Mapp.Mhandler");
            if (omhPassedObject == null) {
                iApplicationHandlerObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
            } else if (omHa == null && omhPassedObject != null) {
                omHa = omhPassedObject;
            } else if (omHa != omhPassedObject) {
                iApplicationHandlerObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
                if (omHa != omhPassedObject) {
                    iApplicationMobjectObjectSet = (int) MethodStateIs.ObjectShouldNotExist;
                }
            }
            return iApplicationHandlerObjectSet;
        }
        public Page App_Page_ObjectGet() {
            iApp_Page_ObjectGet = (int) MethodStateIs.ObjectOK;
            if (omPa == null) {
                iApp_Page_ObjectGet = (int) MethodStateIs.ObjectDoesNotExist;
                return null;
            }
            return omPa;
            // iApp_Page_ObjectGet
        }
        public int App_Page_ObjectSet(Page ompPassedObject) {
            iApp_Page_ObjectSet = (int) MethodStateIs.ObjectOK;
            iApp_Page_ObjectSet = App_Core_ObjectCheck((object)ompPassedObject, (object)omPa, "Mdm.Oss.Mapp.MimportTld");
            if (ompPassedObject == null) {
                iApp_Page_ObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
            } else if (omPa == null && ompPassedObject != null) {
                omPa = ompPassedObject;
            } else if (omPa != ompPassedObject) {
                iApp_Page_ObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
                if (omPa != ompPassedObject) {
                    iApplicationMobjectObjectSet = (int) MethodStateIs.ObjectShouldNotExist;
                }
            }
            return iApp_Page_ObjectSet;
        }
        public Page App_Page_2ObjectGet() {
            iApp_Page_2ObjectGet = (int) MethodStateIs.ObjectOK;
            if (omPa2 == null) {
                iApp_Page_2ObjectGet = (int) MethodStateIs.ObjectDoesNotExist;
                return null;
            }
            return omPa2;
        }
            // iApp_Page_2ObjectGet
        public int App_Page_2ObjectSet(Page ompPassedObject) {
            iApp_Page_2ObjectSet = (int) MethodStateIs.ObjectOK;
            iApp_Page_2ObjectSet = App_Core_ObjectCheck((object)ompPassedObject, (object)omPa, "Mdm.Oss.Mapp.MimportTld");
            if (ompPassedObject == null) {
                iApp_Page_2ObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
            } else if (omPa2 == null && ompPassedObject != null) {
                omPa2 = ompPassedObject;
            } else if (omPa2 != ompPassedObject) {
                iApp_Page_2ObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
                if (omPa2 != ompPassedObject) {
                    iApplicationMobjectObjectSet = (int) MethodStateIs.ObjectShouldNotExist;
                }
            }
            return iApp_Page_2ObjectSet;
        }
        public object ApplicationVerbObjectGet() {
            iApplicationVerbObjectGet = (int) MethodStateIs.ObjectOK;
            if (omVe == null) {
                iApplicationVerbObjectGet = (int) MethodStateIs.ObjectDoesNotExist;
                return null;
            }
            return omVe;
            // iApplicationVerbObjectGet
        }
        public int ApplicationVerbObjectSet(object omvPassedObject) {
            iApplicationVerbObjectSet = (int) MethodStateIs.ObjectOK;
            iApplicationVerbObjectSet = App_Core_ObjectCheck((object)omvPassedObject, (object)omVe, "Mdm.Oss.Mapp.MimportTld");
            if (omvPassedObject == null) {
                iApplicationVerbObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
            } else if (omVe == null && omvPassedObject != null) {
                omVe = omvPassedObject;
            } else if (omVe != omvPassedObject) {
                iApplicationVerbObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
                if (omVe != omvPassedObject) {
                    iApplicationMobjectObjectSet = (int) MethodStateIs.ObjectShouldNotExist;
                }
            }
            return iApplicationVerbObjectSet;
        }
        //
        // ApplicationMbgWorkerObjectGet
        public object ApplicationMbgWorkerObjectGet() {
            iApplicationMbgWorkerObjectGet = (int) MethodStateIs.ObjectOK;
            if (omWo == null) {
                // omWo = this;
                iApp_Page_ObjectGet = (int) MethodStateIs.ObjectDoesNotExist;
                return null;
            }
            return omWo;
        }
        // ApplicationMbgWorkerObjectGet
        public int ApplicationMbgWorkerObjectSet(object omwPassedObject) {
            iApplicationMbgWorkerObjectSet = (int) MethodStateIs.ObjectOK;
            // MbgWorker omOb;
            iApplicationMbgWorkerObjectSet = App_Core_ObjectCheck((object)omwPassedObject, (object)omWo, "Mdm1Oss1MMbgWorkerlication1.MbgWorkerlication");
            if (omwPassedObject == null) {
                iApplicationHandlerObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
            } else if (omWo == null && omwPassedObject != null) {
                omWo = omwPassedObject;
            } else if (omWo != omwPassedObject) {
                iApplicationMbgWorkerObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
                if (omWo != omwPassedObject) {
                    iApplicationMobjectObjectSet = (int) MethodStateIs.ObjectShouldNotExist;
                }
            }
            return iApplicationMbgWorkerObjectSet;
        }
        #endregion
        #endregion
        #region Mapplication Utitilities
        #endregion
        #endregion
        #region MobjectAccessors
        #region ApplicationIoObjectGet
        public int ApplicationIoObjectGet(Application omPassedA) {
            iApplicationIoObjectGet = (int) MethodStateIs.Start;

            return iApplicationIoObjectGet;
        }
        #endregion
        #region MOBJECTApp_Core_ObjectGet
        public object ApplicationMobjectObjectGet() {
            iApplicationMobjectObjectGet = (int) MethodStateIs.ObjectOK;
            if (omOb == null) {
                iApplicationMobjectObjectGet = (int) MethodStateIs.ObjectDoesNotExist;
            }
            return omOb;
            // iApplicationMobjectObjectGet
        }
        public int ApplicationMobjectObjectSet(object omoPassedObject) {
            iApplicationMobjectObjectSet = (int) MethodStateIs.ObjectOK;
            if (omoPassedObject == null) {
                iApplicationMbgWorkerObjectSet = (int) MethodStateIs.ObjectDoesNotExist;
            } else if (omoPassedObject != null && omoPassedObject != null) {
                if (omOb != omoPassedObject) {
                    iApplicationMobjectObjectSet = (int) MethodStateIs.ObjectShouldNotExist;
                }
                omOb = omoPassedObject;
            }
            if (omOb == null) {
                omOb = this;
            }
            return iApplicationMobjectObjectSet;
        }
        #endregion
        #endregion
        #region MapplicationCommunication
        #region Thread 2 Declarations
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxx THREAD TWO Declarations xxxxxxxxxxxxxxxxxxxxxx
        #region  Thread 2 Synchronous Declare Execution Delegate
        delegate int ImportTldThreadMethodHandler(string sPassed_Message);
        string sAnyExceptionMessage = "";

        #endregion
        #region Thread 2 Asynchronous Execution Declarations and delegates
        // not in use
        delegate int AsyncMethodMessageHandler(string sPassed_Message);
        delegate int AsyncMethodProcessChangeHandler(ProgressChangedEventArgs ePcea);
        delegate void UpdateMessageUiHandler(string sPassed_Message);
        delegate void UpdateProgressUiHandler(ProgressChangedEventArgs sPassed_Message);
        delegate void UpdateMessageDispatcher(System.Windows.Threading.DispatcherPriority oDisp, object oTemp, object results);

        // not in use
        delegate void oTextBoxChangeDelegate(object sender, string s);
        delegate void oTextBoxAddDelegate(object sender, string s);
        delegate void oProgressCompletionDelegate(object sender, string sField, int iAmount, int iMax);
        //
        public delegate void MessageSendEventHandler(object sender, string sPassedMessage);
        public delegate void ProgressSendEventHandler(object sender, ProgressChangedEventArgs ePcea);
        //
        public event MessageSendEventHandler MessageSendToPage;
        public event ProgressSendEventHandler ProgressSendToPage;
        //
        #endregion
        #endregion
         #region THREAD 2 COMMUNICATION
        // xxxxxxxxxxx THREAD 2 COMMUNICATION xxxxxxxxxxxxxxxxxxxxxxxxx
        /// <summary> 
        /// Called when BeginInvoke is finished running. 
        /// </summary> 
        /// <param name="earAscynMethodHandlerResult"></param> 
        /// <remarks></remarks> 
        protected void CallbackMethod(IAsyncResult earAscynMethodHandlerResult) {
            try {
                // Retrieve the delegate. 
                AsyncResult result = (AsyncResult)earAscynMethodHandlerResult;
                AsyncMethodMessageHandler caller = (AsyncMethodMessageHandler)result.AsyncDelegate;

                // Because this method is running from secondary thread it 
                // can never access ui objects because they are created 
                // on the primary thread. Uncomment the next line and 
                // run this demo to see for yourself. 
                // This.asynchronousCount.Text = caller.EndInvoke(earAscynMethodHandlerResult) 

                // Call EndInvoke to retrieve the results. 
                int iReturnValue = caller.EndInvoke(earAscynMethodHandlerResult);

                sAnyExceptionMessage = "Nomal end of processing: code(" + iReturnValue.ToString() + ").";

                // Still on secondary THREAD TWO, must update ui on primary thread 

                CallerUpdateSendMessageToUi(iReturnValue.ToString());

            } catch (Exception eAnyException) {

                sAnyExceptionMessage = "Error in processing: " + eAnyException.Message;
                CallerUpdateSendMessageToUi(sAnyExceptionMessage);
            }
        }
        // CALL BACK TO UPDATE UI WITH MESSAGE ON MAIN THREAD
        /// THREAD TWO Setup delegate to update ui on THREAD ONE with results 
        public void CallerUpdateSendMessageToUi(string sPassed_MessageUpdate) {
            // Get back to primary thread to update ui 
            if (sPassed_MessageUpdate == null) { return; }
            if (Application.Current == null) { return; }

            UpdateMessageUiHandler uiHandler = new UpdateMessageUiHandler(CallerSendMessageToUi);

            string results = sPassed_MessageUpdate;

             // omPa.Mdm_Message_Process_Changed
            // Run new thread off Dispatched (primary thread) 
            // this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, uiHandler, results);
            // this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, uiHandler, results);
            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, uiHandler, results);
        }
        // CALL BACK TO UPDATE UI WITH PROGRESS ON MAIN THREAD
        /// THREAD TWO Setup delegate to update ui on THREAD ONE with results 
        public void CallerUpdateSendProgressToUi(ProgressChangedEventArgs sPassed_ProgressUpdate) {
            // Get back to primary thread to update ui 
            if (sPassed_ProgressUpdate == null) { return; }
            if (Application.Current == null) { return; }
            
            UpdateProgressUiHandler uiProgressHandler = new UpdateProgressUiHandler(CallerSendProgressToUi);
            ProgressChangedEventArgs results = sPassed_ProgressUpdate;

            // Run new thread off Dispatched (primary thread) 
            // this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, uiHandler, results);
            // this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, uiHandler, results);
            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, uiProgressHandler, results);
        }
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxx end of THREAD TWO xxxxxxxxxxxxxxxxxxxx
        // xxxx THREAD ONE xxxx Communications
        #region Thread one receive messages from thread two (thread one portion)
        // xxxxxx THREAD ONE invoked messeages from THREAD TWO xxxxxxxxx
        /// Update UI from Dispatcher Thread 
        public void CallerSendMessageToUi(string sPassed_Message) {
            // update user interface controls from primary UI thread 
            // this.visualIndicator.Text = "Processing Completed.";
            // this.asynchronousCount.Text = rowsupdated + " rows processed.";
           
            MessageSendToPage(this, sPassed_Message);

            // omHa.omPa.Mdm_Message_Process_Changed(this, (string)sPassed_Message);
            // App.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, uiHandler, results);
        }
        // xxxxxx THREAD ONE invoked messeages from THREAD TWO xxxxxxxxx
        /// Update UI from Dispatcher Thread 
        public void CallerSendProgressToUi(ProgressChangedEventArgs sPassed_Progress) {
            // update user interface controls from primary UI thread 
            // this.visualIndicator.Text = "Processing Completed.";
            // this.asynchronousCount.Text = rowsupdated + " rows processed.";

            ProgressSendToPage(this, sPassed_Progress);

            // omHa.omPa.Mdm_Message_Process_Changed(this, (ProgressChangedEventArgs)sPassed_Progress);
            // App.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, uiHandler, results);
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
    }
} // end of namespace Mdm