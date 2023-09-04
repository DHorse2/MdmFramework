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
using System.Windows.Controls; // Page
using System.Windows.Data; // Page
using System.Windows.Documents; // Page
using System.Windows.Input; // Page
using System.Windows.Media; // Page
using System.Windows.Media.Imaging; // Page
using System.Windows.Navigation; // Page // App
using System.Windows.Shapes; // Page
using System.Runtime.Remoting.Messaging;


//using    Mdm.Oss.ClipUtil;
//using Mdm.Oss.CodeUtil;
using Mdm.Oss.Decl;
using Mdm.Oss.Console;
using Mdm.Oss.File;
//using Mdm.Oss.Support;
using Mdm.Oss.Mobj;
// using Mdm.Oss.FileUtil;
// using    Mdm1Oss1FileCreation1;
//using Mdm.Oss.UrlUtil.Hist;


namespace Mdm.Oss.Mapp {

    /// <summary>
    /// <para> Mdm Application Object</para>
    /// <para> This is the application level abstraction
    /// of the MVVC framework.  Beyound inherited framework
    /// functionality it contains features for:</para>
    /// <para> ....Retrieving and validating objects.</para>
    /// <para> ....Start, pausing and cancelling processes.</para>
    /// <para> ....Communications with the verb, controller and pages.</para>
    /// <para> ....Console and Message communications.</para>
    /// <para> ....Exceptions handling.</para>
    /// <para> ....Thread handling.</para>
    /// <para> . </para>
    /// <para> This is the base class for Mcontroller, the MVVC
    /// controller.  It currently has weaker types then the 
    /// controller, and requires a Page base class to be implemented
    /// to correct this.</para>
    /// </summary>
    public class Mapplication : Mobject {
        // <Area Id = "Mapplication">
        new public Mapplication XUomMavvXv;
        // <Area Id = "MdmClassLevelSenders">
        new public Object Sender;
        new public Object SenderIsThis;
        // <Area Id = "omU">
        // @@@dgh4 new public MurlHist1Form1 XUomUrvvXv;
        // <Area Id = "omU">
        //public MurlHist1Form1 XUomUrvvXv;
        // public System.Windows.Forms.Form XUomUrvvXv;
        // <Area Id = "omcLocalClipboard">
        //public ClipFormMain XUomClvvXv;

        string sTemp;
        string sTemp3;
        int iTemp;
        int iTemp0;

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

        // <Area Id = "Console Output">

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
        #region ClassCommandConsoleAction
        private String ClassCommandLineRequest;
        // <Area Id = "PrimaryAction">
        private String ClassFileRunRequest;
        // <Area Id = "CommandOptions (External)">
        protected String ClassFileOptions = "";
        // <Area Id = "OptionItFlags">
        protected bool ClassOptionOne = false;
        protected bool ClassOptionTwo = false;
        protected bool ClassOptionThree = false;
        protected bool ClassOptionFour = false;
        protected bool ClassOptionFive = false;
        protected bool ClassOptionSix = false;
        #endregion
        #region Class Mdm1 Oss1 Control1 CVS properties
        //
        #endregion
        // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        #region AppManagement
        #region Class Mdm1 Author properties
        #region Class Mdm1 Author Company properties
        private int ipMdmAuthorCompanyId = 99999;
        public int MdmAuthorCompanyId { get { return ipMdmAuthorCompanyId; } set { ipMdmAuthorCompanyId = value; } }
        private String spMdmAuthorCompanyName = "MacroDm";
        public String MdmAuthorCompanyName { get { return spMdmAuthorCompanyName; } set { spMdmAuthorCompanyName = value; } }
        private String spMdmAuthorCompanyTitle = "unknown";
        public String MdmAuthorCompanyTitle { get { return spMdmAuthorCompanyTitle; } set { spMdmAuthorCompanyTitle = value; } }
        private int ipMdmAuthorCompanyNumber = 99999;
        public int MdmAuthorCompanyNumber { get { return ipMdmAuthorCompanyNumber; } set { ipMdmAuthorCompanyNumber = value; } }
        private int ipMdmAuthorCompanyIntStatus = 99999;
        public int MdmAuthorCompanyIntStatus { get { return ipMdmAuthorCompanyIntStatus; } set { ipMdmAuthorCompanyIntStatus = value; } }
        private String spMdmAuthorCompanyStatusText = "unknown";
        public String MdmAuthorCompanyStatusText { get { return spMdmAuthorCompanyStatusText; } set { spMdmAuthorCompanyStatusText = value; } }
        private int ipMdmAuthorCompanyIntResult = 99999;
        public int MdmAuthorCompanyIntResult { get { return ipMdmAuthorCompanyIntResult; } set { ipMdmAuthorCompanyIntResult = value; } }
        private bool bpMdmAuthorCompanyBoolResult = false;
        public bool MdmAuthorCompanyBoolResult { get { return bpMdmAuthorCompanyBoolResult; } set { bpMdmAuthorCompanyBoolResult = value; } }
        #endregion
        #region Class Mdm1 Author Person properties
        private int ipMdmAuthorId = 99999;
        public int MdmAuthorId { get { return ipMdmAuthorId; } set { ipMdmAuthorId = value; } }
        private String spMdmAuthorName = "David G. Horsman";
        public String MdmAuthorName { get { return spMdmAuthorName; } set { spMdmAuthorName = value; } }
        private String spMdmAuthorTitle = "unknown";
        public String MdmAuthorTitle { get { return spMdmAuthorTitle; } set { spMdmAuthorTitle = value; } }
        private int ipMdmAuthorNumber = 99999;
        public int MdmAuthorNumber { get { return ipMdmAuthorNumber; } set { ipMdmAuthorNumber = value; } }
        private int ipMdmAuthorStatus = 99999;
        public int MdmAuthorIntStatus { get { return ipMdmAuthorStatus; } set { ipMdmAuthorStatus = value; } }
        private String spMdmAuthorStatusText = "unknown";
        public String MdmAuthorStatusText { get { return spMdmAuthorStatusText; } set { spMdmAuthorStatusText = value; } }
        private int ipMdmAuthorIntResult = 99999;
        public int MdmAuthorIntResult { get { return ipMdmAuthorIntResult; } set { ipMdmAuthorIntResult = value; } }
        private bool bpMdmAuthorBoolResult = false;
        public bool MdmAuthorBoolResult { get { return bpMdmAuthorBoolResult; } set { bpMdmAuthorBoolResult = value; } }
        #endregion
        #endregion
        #region Project
        #region Project properties
        private int ipMdmProjectId = 99999;
        public int MdmProjectId { get { return ipMdmProjectId; } set { ipMdmProjectId = value; } }
        private String spMdmProjectName = "MdmSrtVs5_0";
        public String MdmProjectName { get { return spMdmProjectName; } set { spMdmProjectName = value; } }
        private String spMdmProjectTitle = "MdmSrtVs5_0";
        public String MdmProjectTitle { get { return spMdmProjectTitle; } set { spMdmProjectTitle = value; } }
        private int ipMdmProjectNumber = 99999;
        public int MdmProjectNumber { get { return ipMdmProjectNumber; } set { ipMdmProjectNumber = value; } }
        private int ipMdmProjectIntStatus = 99999;
        public int MdmProjectIntStatus { get { return ipMdmProjectIntStatus; } set { ipMdmProjectIntStatus = value; } }
        private String spMdmProjectStatusText = "unknown";
        public String MdmProjectStatusText { get { return spMdmProjectStatusText; } set { spMdmProjectStatusText = value; } }
        private int ipMdmProjectIntResult = 99999;
        public int MdmProjectIntResult { get { return ipMdmProjectIntResult; } set { ipMdmProjectIntResult = value; } }
        private bool bpMdmProjectBoolResult = false;
        public bool MdmProjectBoolResult { get { return bpMdmProjectBoolResult; } set { bpMdmProjectBoolResult = value; } }
        #endregion
        #region Task properties
        private int ipMdmTaskId = 99999;
        public int MdmTaskId { get { return ipMdmTaskId; } set { ipMdmTaskId = value; } }
        private String spMdmTaskName = "Task0";
        public String MdmTaskName { get { return spMdmTaskName; } set { spMdmTaskName = value; } }
        private String spMdmTaskTitle = "MdmSrtVs5_0";
        public String MdmTaskTitle { get { return spMdmTaskTitle; } set { spMdmTaskTitle = value; } }
        private int ipMdmTaskNumber = 99999;
        public int MdmTaskNumber { get { return ipMdmTaskNumber; } set { ipMdmTaskNumber = value; } }
        private int ipMdmTaskIntStatus = 99999;
        public int MdmTaskIntStatus { get { return ipMdmTaskIntStatus; } set { ipMdmTaskIntStatus = value; } }
        private String spMdmTaskStatusText = "unknown";
        public String MdmTaskStatusText { get { return spMdmTaskStatusText; } set { spMdmTaskStatusText = value; } }
        private int ipMdmTaskIntResult = 99999;
        public int MdmTaskIntResult { get { return ipMdmTaskIntResult; } set { ipMdmTaskIntResult = value; } }
        private bool bpMdmTaskBoolResult = false;
        public bool MdmTaskBoolResult { get { return bpMdmTaskBoolResult; } set { bpMdmTaskBoolResult = value; } }
        #endregion
        #region Task Step properties
        private int ipMdmTaskStepId = 99999;
        public int MdmTaskStepId { get { return ipMdmTaskStepId; } set { ipMdmTaskStepId = value; } }
        private String spMdmTaskStepName = "Step0";
        public String MdmTaskStepName { get { return spMdmTaskStepName; } set { spMdmTaskStepName = value; } }
        private String spMdmTaskStepTitle = "MdmSrtVs5_0";
        public String MdmTaskStepTitle { get { return spMdmTaskStepTitle; } set { spMdmTaskStepTitle = value; } }
        private int ipMdmTaskStepNumber = 99999;
        public int MdmTaskStepNumber { get { return ipMdmTaskStepNumber; } set { ipMdmTaskStepNumber = value; } }
        private int ipMdmTaskStepIntStatus = 99999;
        public int MdmTaskStepIntStatus { get { return ipMdmTaskStepIntStatus; } set { ipMdmTaskStepIntStatus = value; } }
        private String spMdmTaskStepStatusText = "unknown";
        public String MdmTaskStepStatusText { get { return spMdmTaskStepStatusText; } set { spMdmTaskStepStatusText = value; } }
        private int ipMdmTaskStepIntResult = 99999;
        public int MdmTaskStepIntResult { get { return ipMdmTaskStepIntResult; } set { ipMdmTaskStepIntResult = value; } }
        private bool bpMdmTaskStepBoolResult = false;
        public bool MdmTaskStepBoolResult { get { return bpMdmTaskStepBoolResult; } set { bpMdmTaskStepBoolResult = value; } }
        #endregion
        #endregion
        #region Solution properties
        private int ipMdmSolutionId = 99999;
        public int MdmSolutionId { get { return ipMdmSolutionId; } set { ipMdmSolutionId = value; } }
        private String spMdmSolutionName = "MdmSrtVs5_0";
        public String MdmSolutionName { get { return spMdmSolutionName; } set { spMdmSolutionName = value; } }
        private String spMdmSolutionTitle = "MdmSrtVs5_0";
        public String MdmSolutionTitle { get { return spMdmSolutionTitle; } set { spMdmSolutionTitle = value; } }
        private int ipMdmSolutionNumber = 99999;
        public int MdmSolutionNumber { get { return ipMdmSolutionNumber; } set { ipMdmSolutionNumber = value; } }
        private int ipMdmSolutionIntStatus = 99999;
        public int MdmSolutionIntStatus { get { return ipMdmSolutionIntStatus; } set { ipMdmSolutionIntStatus = value; } }
        private String spMdmSolutionStatusText = "unknown";
        public String MdmSolutionStatusText { get { return spMdmSolutionStatusText; } set { spMdmSolutionStatusText = value; } }
        private int ipMdmSolutionIntResult = 99999;
        public int MdmSolutionIntResult { get { return ipMdmSolutionIntResult; } set { ipMdmSolutionIntResult = value; } }
        private bool bpMdmSolutionBoolResult = false;
        public bool MdmSolutionBoolResult { get { return bpMdmSolutionBoolResult; } set { bpMdmSolutionBoolResult = value; } }
        #endregion
        #region Namespace properties
        private int ipMdmNamespaceId = 99999;
        public int MdmNamespaceId { get { return ipMdmNamespaceId; } set { ipMdmNamespaceId = value; } }
        private String spMdmNamespaceName = "unknown";
        public String MdmNamespaceName { get { return spMdmNamespaceName; } set { spMdmNamespaceName = value; } }
        private String spMdmNamespaceTitle = "unknown";
        public String MdmNamespaceTitle { get { return spMdmNamespaceTitle; } set { spMdmNamespaceTitle = value; } }
        private int ipMdmNamespaceNumber = 99999;
        public int MdmNamespaceNumber { get { return ipMdmNamespaceNumber; } set { ipMdmNamespaceNumber = value; } }
        private int ipMdmNamespaceIntStatus = 99999;
        public int MdmNamespaceIntStatus { get { return ipMdmNamespaceIntStatus; } set { ipMdmNamespaceIntStatus = value; } }
        private String spMdmNamespaceStatusText = "unknown";
        public String MdmNamespaceStatusText { get { return spMdmNamespaceStatusText; } set { spMdmNamespaceStatusText = value; } }
        private int ipMdmNamespaceIntResult = 99999;
        public int MdmNamespaceIntResult { get { return ipMdmNamespaceIntResult; } set { ipMdmNamespaceIntResult = value; } }
        private bool bpMdmNamespaceBoolResult = false;
        public bool MdmNamespaceBoolResult { get { return bpMdmNamespaceBoolResult; } set { bpMdmNamespaceBoolResult = value; } }
        #endregion
        #region Assembly properties
        private int ipMdmAssemblyId = 99999;
        public int MdmAssemblyId { get { return ipMdmAssemblyId; } set { ipMdmAssemblyId = value; } }
        private String spMdmAssemblyName = "unknown";
        public String MdmAssemblyName { get { return spMdmAssemblyName; } set { spMdmAssemblyName = value; } }
        private String spMdmAssemblyTitle = "unknown";
        public String MdmAssemblyTitle { get { return spMdmAssemblyTitle; } set { spMdmAssemblyTitle = value; } }
        private int ipMdmAssemblyNumber = 99999;
        public int MdmAssemblyNumber { get { return ipMdmAssemblyNumber; } set { ipMdmAssemblyNumber = value; } }
        private int ipMdmAssemblyIntStatus = 99999;
        public int MdmAssemblyIntStatus { get { return ipMdmAssemblyIntStatus; } set { ipMdmAssemblyIntStatus = value; } }
        private String spMdmAssemblyStatusText = "unknown";
        public String MdmAssemblyStatusText { get { return spMdmAssemblyStatusText; } set { spMdmAssemblyStatusText = value; } }
        private int ipMdmAssemblyIntResult = 99999;
        public int MdmAssemblyIntResult { get { return ipMdmAssemblyIntResult; } set { ipMdmAssemblyIntResult = value; } }
        private bool bpMdmAssemblyBoolResult = false;
        public bool MdmAssemblyBoolResult { get { return bpMdmAssemblyBoolResult; } set { bpMdmAssemblyBoolResult = value; } }
        #endregion
        #region ClassApp properties
        private int ipMdmAppId = 99999;
        public int MdmAppId { get { return ipMdmAppId; } set { ipMdmAppId = value; } }
        private String spMdmAppName = "unknown";
        public String MdmAppName { get { return spMdmAppName; } set { spMdmAppName = value; } }
        private String spMdmAppTitle = "unknown";
        public String MdmAppTitle { get { return spMdmAppTitle; } set { spMdmAppTitle = value; } }
        private int ipMdmAppNumber = 99999;
        public int MdmAppNumber { get { return ipMdmAppNumber; } set { ipMdmAppNumber = value; } }
        private int ipMdmAppStatus = 99999;
        public int MdmAppStatus { get { return ipMdmAppStatus; } set { ipMdmAppStatus = value; } }
        private String spMdmAppStatusText = "unknown";
        public String MdmAppStatusText { get { return spMdmAppStatusText; } set { spMdmAppStatusText = value; } }
        private int ipMdmAppIntResult = 99999;
        public int MdmAppIntResult { get { return ipMdmAppIntResult; } set { ipMdmAppIntResult = value; } }
        private bool bpMdmAppBoolResult = false;
        public bool MdmAppBoolResult { get { return bpMdmAppBoolResult; } set { bpMdmAppBoolResult = value; } }
        #endregion
        #region ClassOther properties
        private int ipMdmOtherId = 99999;
        public int MdmOtherId { get { return ipMdmOtherId; } set { ipMdmOtherId = value; } }
        private String spMdmOtherName = "unknown";
        public String MdmOtherName { get { return spMdmOtherName; } set { spMdmOtherName = value; } }
        private String spMdmOtherTitle = "unknown";
        public String MdmOtherTitle { get { return spMdmOtherTitle; } set { spMdmOtherTitle = value; } }
        private int ipMdmOtherNumber = 99999;
        public int MdmOtherNumber { get { return ipMdmOtherNumber; } set { ipMdmOtherNumber = value; } }
        private int ipMdmOtherStatus = 99999;
        public int MdmOtherStatus { get { return ipMdmOtherStatus; } set { ipMdmOtherStatus = value; } }
        private String spMdmOtherStatusText = "unknown";
        public String MdmOtherStatusText { get { return spMdmOtherStatusText; } set { spMdmOtherStatusText = value; } }
        private int ipMdmOtherIntResult = 99999;
        public int MdmOtherIntResult { get { return ipMdmOtherIntResult; } set { ipMdmOtherIntResult = value; } }
        private bool bpMdmOtherBoolResult = false;
        public bool MdmOtherBoolResult { get { return bpMdmOtherBoolResult; } set { bpMdmOtherBoolResult = value; } }
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        // Class Internals - Properties Fields and Attributes
        #region MapplicationClassInstanceManagement
        #region MapplicationConstructor
        public Mapplication()
            : base() {
            iMapplication = (long)StateIs.Started;
            MdmProcessTitle = "Initializing, not set yet";
            //
            Sender = this;
            XUomMovvXv = this;
            XUomMavvXv = this;
            MapplicationInitialize();
        }

        public Mapplication(long ClassFeaturesPassed)
            : base(ClassFeaturesPassed) {
            iMapplication = (long)StateIs.Started;
            MdmProcessTitle = "Initializing, not set yet";
            //
            Sender = this;
            XUomMovvXv = this;
            XUomMavvXv = this;
            MapplicationInitialize();
        }

        public Mapplication(long ClassFeaturesPassed, Object PassedCo)
            : base(ClassFeaturesPassed) {
            iMapplicationPassedApp = (long)StateIs.Started;
            MdmProcessTitle = "Initializing, not set yet";
            //
            Sender = this;
            XUomMovvXv = this;
            XUomMavvXv = this;
            if (PassedCo != null) {
                if (XUomCovvXv == null) {
                    XUomCovvXv = PassedCo;
                }
            }
            MapplicationInitialize();
        }

        public Mapplication(long ClassFeaturesPassed, Application PassedA, Page PassedP)
            : base(ClassFeaturesPassed) {
            iMapplicationPassedApp = (long)StateIs.Started;
            MdmProcessTitle = "Initializing, not set yet";
            //
            Sender = this;
            XUomMovvXv = this;
            XUomMavvXv = this;
            if (PassedA != null) {
                if (XUomApvvXv != null) {
                    AppObject = XUomApvvXv = null;
                }
                AppObject = XUomApvvXv = PassedA;
            }
            if (PassedP != null) {
                if (XUomPmvvXv != null) {
                    PageMainObject = XUomPmvvXv = null;
                }
                PageMainObject = XUomPmvvXv = PassedP;
            }
            MapplicationInitialize();
            // iMapplicationPassedApp
        }


        #endregion
        #region MapplicationEngine
        public long MapplicationInitialize()
        {
            iMapplicationStartApp = (long)StateIs.Started;
            //
            // StatusLine = new StatusLineDef(false, true, true); // Boxes used...
            // StatusUi.BoxCreate();
            ConsoleMdmInitialize();
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
            ExternalId.Id = 99999;
            ExternalId.Name = "unknown";
            ExternalId.Title = "unknown";
            ExternalId.Number = 99999;
            ExternalId.Status = 99999;
            ExternalId.StatusText = "unknown";
            ExternalId.IntResult = 99999;
            ExternalId.BoolResult = false;
            // 
            // <Segment Summary>
            // Class Internal properties
            // </Segment Summary>
            //
            InternalId.Id = 99999;
            InternalId.Name = "unknown";
            InternalId.Title = "unknown";
            InternalId.Number = 99999;
            InternalId.Status = 99999;
            InternalId.StatusText = "unknown";
            InternalId.IntResult = 99999;
            InternalId.BoolResult = false;
            // 
            // <Segment Summary>
            // Class App properties
            // </Segment Summary>
            ipMdmAppId = 99999;
            spMdmAppName = "unknown";
            spMdmAppTitle = "unknown";
            ipMdmAppNumber = 99999;
            ipMdmAppStatus = 99999;
            spMdmAppStatusText = "unknown";
            ipMdmAppIntResult = 99999;
            bpMdmAppBoolResult = false;
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
        #region MdmCoreObjectCreation
        public override long AppCoreObjectCreate(ref Page PassedMinputTldPageMain) {
            PageMainObject = XUomPmvvXv = PassedMinputTldPageMain;
            AppCoreObjectCreate();
            return 0;
        }
        public override long AppCoreObjectCreate(ref System.Windows.Application PassedApplication) {
            AppObject = XUomApvvXv = PassedApplication;
            AppCoreObjectCreate();
            return 0;
        }
        public override long AppCoreObjectCreate() {
            iAppCoreObjectCreate = (long)StateIs.Started;
            // Start Up Ui Capable Controller Unit
            // MinputTldApp XUomApvvXv - App being run
            if (XUomApvvXv == null) {
                // AppObject = XUomApvvXv = XUomApvvXv;
                AppObject = XUomApvvXv = Application.Current;
            }
            // MinputTldApp XUomPmvvXv - Main or Home Page
            if (XUomPmvvXv == null) {
                // PageMainObject = XUomPmvvXv = ref XUomPmvvXv;
                PageMainObject = XUomPmvvXv = new Page();
            }
            // MinputTldApp XUomMavvXv - Standard Object
            if (XUomMavvXv == null) {
                // XUomMavvXv = (Mobject)XUomMavvXv;
                XUomMavvXv = new Mapplication((long)ClassUses.RoleAsController, XUomApvvXv, (Page)XUomPmvvXv);
            }
            iAppCoreObjectCreate = AppSetObjectByType(ref Sender);
            if (XUomMavvXv == null) {
                XUomMavvXv = new Mapplication((long)ClassUses.RoleAsController, XUomMavvXv);
                iAppCoreObjectCreate = AppMappObjectSet(XUomMavvXv);
                SenderIsThis = XUomPmvvXv;
                iAppCoreObjectCreate = PageMainObjectSet(ref SenderIsThis);
                SenderIsThis = XUomApvvXv;
                iAppCoreObjectCreate = AppAppObjectSet(ref SenderIsThis);
            }
            // MinputTldApp XUomCovvXv - Main Process Supervision and Control
            if (XUomCovvXv == null) {
                SenderIsThis = XUomCovvXv;
                AppMcontrollerObjectSet(ref SenderIsThis);
            }
            AppCoreObjectCreatePages();
            AppCoreObjectCreateVerbs();
            return iAppCoreObjectCreate;
        }
        public override long AppCoreObjectCreatePages() {
            // MinputTldApp XUomPmvvXv - Main or Home Page
            if (XUomPmvvXv == null) {
                // DbDetailPageObject = XUomPdvvXv = (MinputTldPageMain)XUomPdvvXv;
                PageMainObject = XUomPmvvXv = new Page();
                SenderIsThis = XUomPmvvXv;
                iAppCoreObjectCreate = PageMainObjectSet(ref SenderIsThis);
            return iAppCoreObjectCreate;
            }
            // MinputTldApp XUomPdvvXv - Details, level 2, options, etc. supplementary Page
            if (XUomPdvvXv == null) {
                // XUomMavvXv or this
                PageMainObject = XUomPmvvXv = new Page();
                // MinputTldApp omvLocalBoard - Main Process performed by XUomApvvXv
                if (XUomUrvvXv == null & XUomUrvvXvCreateNow) {
                    XUomUrvvXv = new MurlHist1Form1(); // XUomUrvvXv = XUomUrvvXv;
                    XUomUrvvXv.Show();
                }
            }
            // Store Pages
            // if (OutputFileNameLast != FileLine.Text && XUomPdvvXv != null) {
            // TODO $$$$NEXT AppCoreObjectCreatePages DbDetailPageSetDefault(ref XUomPmvvXv, ref XUomPdvvXv);
            // }
            return iAppCoreObjectCreate;
        }
        public override long AppCoreObjectCreateVerbs() {
            // MinputTldApp XUomVevvXv - Main Process performed by XUomApvvXv
            if (XUomVevvXv == null) {
                // XUomVevvXv = (MinputTldThread)XUomVevvXv;
                XUomVevvXv = new Object();
                SenderIsThis = XUomVevvXv;
                AppVerbObjectSet(ref SenderIsThis);
                // AppVerbObjectSet((Object)XUomVevvXv);
            }
            // MinputTldApp XUomVtvvXv - BgWorkerlication being run
            if (XUomVtvvXv == null) {
                XUomVtvvXv = new Object();
                SenderIsThis = XUomVtvvXv;
                iAppCoreObjectCreate = AppVerbBgWorkerObjectSet(ref SenderIsThis);
            }
            return iAppCoreObjectCreate;
        }
        #endregion
        #region MdmObjectInstanceAccessors
        #region MdmAppObjectInstanceAccessors
        public override System.Windows.Application AppAppObjectGet() {
            iAppAppObjectGet = (long)StateIs.Valid;
            if (XUomApvvXv == null) {
                AppObject = XUomApvvXv = System.Windows.Application.Current;
            }
            return XUomApvvXv;
            // iAppAppObjectGet
        }
        public override long AppAppObjectSet(ref Object omaPassedObject) {
            iAppAppObjectSet = (long)StateIs.Valid;
            // App XUomMovvXv;
            iAppAppObjectSet = AppCoreObjectCheck((Object)omaPassedObject, (Object)XUomApvvXv, "Mdm.Oss.Mapp.Application");
            if (omaPassedObject == null) {
                AppObject = XUomApvvXv = System.Windows.Application.Current;
                iAppMcontrollerObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomApvvXv == null && omaPassedObject != null) {
                AppObject = XUomApvvXv = (System.Windows.Application)omaPassedObject;
            } else if (XUomApvvXv != omaPassedObject) {
                iAppAppObjectSet = (long)StateIs.DoesNotExist;
                if (XUomApvvXv != omaPassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iAppAppObjectSet;
        }
        public new Mapplication AppMappObjectGet() {
            iAppMappObjectGet = (long)StateIs.Valid;
            return XUomMavvXv;
        }

        public virtual long AppMappObjectSet(ref Mapplication ommPassedObject) {
            iAppMappObjectSet = (long)StateIs.Valid;
            // Mapp XUomMovvXv;
            iAppMappObjectSet = AppCoreObjectCheck((Object)ommPassedObject, (Object)XUomMavvXv, "Mdm1Oss1Mapplication1.Mapplication");
            if (ommPassedObject == null) {
                iAppMcontrollerObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomMavvXv == null && ommPassedObject != null) {
                XUomMavvXv = ommPassedObject;
            } else if (XUomMavvXv != ommPassedObject) {
                iAppMappObjectSet = (long)StateIs.DoesNotExist;
                if (XUomMavvXv != ommPassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iAppMappObjectSet;
        }

        public override Object AppMcontrollerObjectGet() {
            iAppMcontrollerObjectGet = (long)StateIs.Valid;
            if (XUomCovvXv == null) {
                String stemp99 = XUomPmvvXv.Parent.ToString();
                iAppMcontrollerObjectGet = (long)StateIs.DoesNotExist;
                return null;
            }
            return XUomCovvXv;
            // iAppMcontrollerObjectGet
        }
        public override long AppMcontrollerObjectSet(ref Object omhPassedObject) {
            iAppMcontrollerObjectSet = (long)StateIs.Valid;
            iAppMcontrollerObjectSet = AppCoreObjectCheck((Object)omhPassedObject, (Object)XUomCovvXv, "Mdm.Oss.Mapp.Mcontroller");
            if (omhPassedObject == null) {
                iAppMcontrollerObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomCovvXv == null && omhPassedObject != null) {
                XUomCovvXv = omhPassedObject;
            } else if (XUomCovvXv != omhPassedObject) {
                iAppMcontrollerObjectSet = (long)StateIs.DoesNotExist;
                if (XUomCovvXv != omhPassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iAppMcontrollerObjectSet;
        }
        #endregion
        #region MdmPageObjectInstanceAccessors
        // Page Main (1)
        // iDbDetailPageObjectGet
        public virtual long PageMainInitialized(ref Mobject PassedOb) {
            iPageMainLoaded = (long)StateIs.Valid;
            StatusUi.LineSet(2, "MinputTldPageMain initialised");
            // MinputTldApp MinputTldPageMain;
            if (XUomPdvvXv == null) { DbDetailPageObject = XUomPdvvXv = PassedOb.XUomPdvvXv; }
            //
            iPageMainLoaded = PageMainSetCoreObjects(ref PassedOb);
            //
            iPageMainLoaded = (long)StateIs.Valid;
            return iPageMainLoaded;
        }
        public virtual long PageMainSetCoreObjects(ref Mobject PassedOb) {
            iPageMainSetCoreObjects = (long)StateIs.Started;
            iPageMainSetCoreObjects = AppCoreObjectGet(PassedOb);
            return iPageMainSetCoreObjects;
        }
        public override Page PageMainObjectGet() {
            iPageMainObjectGet = (long)StateIs.Valid;
            if (XUomPmvvXv == null) {
                iPageMainObjectGet = (long)StateIs.DoesNotExist;
                return null;
            }
            return XUomPmvvXv;
            // iPageMainObjectGet
        }
        public override long PageMainObjectSet(ref Object PassedObject) {
            iPageMainObjectSet = (long)StateIs.Valid;
            iPageMainObjectSet = AppCoreObjectCheck((Object)PassedObject, (Object)XUomPmvvXv, "Mdm.Oss.Mapp.MinputTld");
            if (PassedObject == null) {
                iPageMainObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomPmvvXv == null && PassedObject != null) {
                PageMainObject = XUomPmvvXv = (Page)PassedObject;
            } else if (XUomPmvvXv != PassedObject) {
                iPageMainObjectSet = (long)StateIs.DoesNotExist;
                if (XUomPmvvXv != PassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iPageMainObjectSet;
        }
        // Page Detail (2)
        public virtual long DbDetailPageLoaded(ref Mapplication PassedXUomMavvXv) {
            iDbDetailPageLoaded = (long)StateIs.Valid;
            StatusUi.LineSet(2, "MinputTldDbDetailPage initialised");
            // MinputTldApp MinputTldDbDetailPage;
            if (XUomPdvvXv == null) { DbDetailPageObject = XUomPdvvXv = PassedXUomMavvXv.XUomPdvvXv; }
            //
            iDbDetailPageLoaded = DbDetailPageSetCoreObjects(ref 
                PassedXUomMavvXv);
            //
            iDbDetailPageLoaded = (long)StateIs.Valid;
            return iDbDetailPageLoaded;
        }
        public virtual long DbDetailPageSetCoreObjects(ref Mapplication PassedXUomMavvXv) {
            iDbDetailPageSetCoreObjects = (long)StateIs.Started;
            iDbDetailPageSetCoreObjects = AppCoreObjectGet((Mobject) PassedXUomMavvXv);
            //SenderIsThis = PassedXUomMavvXv;
            //iDbDetailPageSetCoreObjects = AppCoreObjectGet(ref SenderIsThis);
            return iDbDetailPageSetCoreObjects;
        }
        public override Page DbDetailPageObjectGet() {
            iDbDetailPageObjectGet = (long)StateIs.Valid;
            if (XUomPdvvXv == null) {
                iDbDetailPageObjectGet = (long)StateIs.DoesNotExist;
                return null;
            }
            return XUomPdvvXv;
        }
        public override long DbDetailPageObjectSet(ref Object PassedObject) {
            iDbDetailPageObjectSet = (long)StateIs.Valid;
            iDbDetailPageObjectSet = AppCoreObjectCheck((Object)PassedObject, (Object)XUomPmvvXv, "Mdm.Oss.Mapp.MinputTld");
            if (PassedObject == null) {
                iDbDetailPageObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomPdvvXv == null && PassedObject != null) {
                DbDetailPageObject = XUomPdvvXv = (Page)PassedObject;
            } else if (XUomPdvvXv != PassedObject) {
                iDbDetailPageObjectSet = (long)StateIs.DoesNotExist;
                if (XUomPdvvXv != PassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iDbDetailPageObjectSet;
        }
        #endregion
        #region MdmVerbObjectInstanceAccessors
        public override Object AppVerbObjectGet() {
            iAppVerbObjectGet = (long)StateIs.Valid;
            if (XUomVtvvXv == null) {
                iAppVerbObjectGet = (long)StateIs.DoesNotExist;
                return null;
            }
            return XUomVtvvXv;
            // iAppVerbObjectGet
        }
        public override long AppVerbObjectSet(ref Object omvPassedObject) {
            iAppVerbObjectSet = (long)StateIs.Valid;
            iAppVerbObjectSet = AppCoreObjectCheck((Object)omvPassedObject, (Object)XUomVevvXv, "Mdm.Oss.Mapp.MinputTld");
            if (omvPassedObject == null) {
                iAppVerbObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomVevvXv == null && omvPassedObject != null) {
                XUomVevvXv = omvPassedObject;
            } else if (XUomVevvXv != omvPassedObject) {
                iAppVerbObjectSet = (long)StateIs.DoesNotExist;
                if (XUomVevvXv != omvPassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iAppVerbObjectSet;
        }
        public override Object AppVerbThreadObjectGet() {
            iAppVerbThreadObjectGet = (long)StateIs.Valid;
            if (XUomVtvvXv == null) {
                iAppVerbThreadObjectGet = (long)StateIs.DoesNotExist;
                return null;
            }
            return XUomVtvvXv;
            // iAppVerbThreadObjectGet
        }
        public override long AppVerbThreadObjectSet(ref Object omvPassedObject) {
            iAppVerbThreadObjectSet = (long)StateIs.Valid;
            iAppVerbThreadObjectSet = AppCoreObjectCheck((Object)omvPassedObject, (Object)XUomVtvvXv, "Mdm.Oss.Mapp.MinputTld");
            if (omvPassedObject == null) {
                iAppVerbThreadObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomVtvvXv == null && omvPassedObject != null) {
                XUomVtvvXv = omvPassedObject;
            } else if (XUomVtvvXv != omvPassedObject) {
                iAppVerbThreadObjectSet = (long)StateIs.DoesNotExist;
                if (XUomVtvvXv != omvPassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iAppVerbThreadObjectSet;
        }
        public override Object AppVerbBgWorkerObjectGet() {
            iAppVerbBgWorkerObjectGet = (long)StateIs.Valid;
            if (XUomVbvvXv == null) {
                // XUomVbvvXv = this;
                iPageMainObjectGet = (long)StateIs.DoesNotExist;
                return null;
            }
            return XUomVbvvXv;
        }
        public override long AppVerbBgWorkerObjectSet(ref Object PassedObject) {
            iAppVerbBgWorkerObjectSet = (long)StateIs.Valid;
            // MbgWorker XUomMovvXv; 1MMbgWor
            // iAppVerbBgWorkerObjectSet = AppCoreObjectCheck((Object)PassedObject, (Object)XUomVtvvXv, "Mdm1Oss1MbgWorkerlication1.MbgWorkerlication");
            if (PassedObject == null) {
                iAppMcontrollerObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomVtvvXv == null && PassedObject != null) {
                XUomVtvvXv = (BackgroundWorker)PassedObject;
            } else if (XUomVtvvXv != PassedObject) {
                iAppVerbBgWorkerObjectSet = (long)StateIs.DoesNotExist;
                if (XUomVtvvXv != PassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iAppVerbBgWorkerObjectSet;
        }
        #endregion
        #endregion
        #endregion
        #region Mapplication Utitilities
        #endregion
        #endregion
        #region MapplicationCommunication
         #region THREAD 2 COMMUNICATION
        // xxxxxxxxxxx THREAD 2 COMMUNICATION xxxxxxxxxxxxxxxxxxxxxxxxx
        /// <summary> 
        /// Called when BeginInvoke is finished running. 
        /// </summary> 
        /// <param name="earAsyncMethodControllerResult"></param> 
        /// <remarks>
        // Application.Current.Dispatcher.Invoke
        // using ThreadUiTextMessageImpl implement in the 
        // Mcontroller currently the contoller uses
        // MessageMdmSendToPage delegate in StdBaseRunFileConsoleDef
        // this in turn is set to 
        // MessageMdmSendToPageImpl due to threading requirements
        // although then once removed because of the base classes.
        //
        // Some of the convolute calls
        // sort of evolved in an Agile way as
        // functions were moved into the MVC controller
        // from Page and Verb code combined with
        // various functions moved to the DefStd
        // and DefBase class structure that was created
        // in the move from Procedural to OO C# code.
        /// </remarks> 
        //
        public void CallbackMethodTest(IAsyncResult ThreadControllerAsyncResult) {
            try {
                // Retrieve the delegate. 
                ThreadUiTextMessageAsyncResult = (AsyncResult)ThreadControllerAsyncResult;
                ThreadControllerAsyncDel ThreadControllerAsync =
                    (ThreadControllerAsyncDel)ThreadUiTextMessageAsyncResult.AsyncDelegate;
                // can never access ui objects because they are created 
                // on the primary thread. 
                // Call EndInvoke to retrieve the results. 
               Object ReturnValue = new object();
                ReturnValue = ThreadControllerAsync.EndInvoke(ThreadControllerAsyncResult);
                sAnyExceptionMessage = "Nomal end of processing: ";
                // Still on secondary THREAD TWO, must update ui on primary thread 
                ThreadUiTextMessageAsync(ref Sender, ReturnValue.ToString());
            } catch (Exception eAnyException) {

                sAnyExceptionMessage = "Error in processing: " + eAnyException.Message;
                ThreadUiTextMessageAsync(ref Sender, sAnyExceptionMessage);
            }
        }
        // CALL BACK TO UPDATE UI WITH MESSAGE ON MAIN THREAD
        /// THREAD TWO Setup delegate to update ui on THREAD ONE with ThreadUiProgressResults 
        public virtual void ThreadUiTextMessageDoSet() {
            ThreadUiTextMessageDoSetResult = (long)StateIs.Valid;
            // How to get back to primary thread to update ui 
            // TODO ThreadUiTextMessageDoSet insert passed method name
            ThreadUiTextMessageInvoke = new ThreadUiTextMessageDel(StatusLineChanged);
            ThreadUiTextMessageAsyncInvoke = new ThreadUiTextMessageAsyncDel(StatusLineChanged);
            //
            ThreadUiTextMessage = new ThreadUiTextMessageDel(ThreadUiTextMessageImpl);
            ThreadUiTextMessageAsync = new ThreadUiTextMessageAsyncDel(ThreadUiTextMessageImpl);
        }
        public virtual void ThreadUiTextMessageDo(ref Object SenderPassed, ProgressChangedEventArgs PassedThreadUiProgressUpdate) {
            ThreadUiTextMessageDoResult = (long)StateIs.Valid;
            if (ThreadUiTextMessage == null) {
                ThreadUiTextMessageDoSet();
                if (ThreadUiTextMessage == null) { return; }
            }
            ThreadUiTextMessageContent = (String)PassedThreadUiProgressUpdate.UserState;
            ThreadUiTextMessageDo(ref SenderPassed, ThreadUiTextMessageContent);
        }
        public virtual void ThreadUiTextMessageDo(ref Object SenderPassed, String PassedThreadUiTextMessageContent) {
            ThreadUiTextMessageDoResult = (long)StateIs.Valid;
            if (ThreadUiTextMessage == null) {
                ThreadUiTextMessageDoSet();
                if (ThreadUiTextMessage == null) { return; }
            }
            // Get back to primary thread to update ui 
            // StatusLineChanged
            // Run new thread off Dispatched (primary thread) 
            ThreadUiTextMessageImpl(ref SenderPassed, PassedThreadUiTextMessageContent);
        }
        // CALL BACK TO UPDATE UI WITH PROGRESS ON MAIN THREAD
        /// THREAD TWO Setup delegate to update ui on THREAD ONE with results 
        public virtual void ThreadUiProgressDoSet() {
             ThreadUiProgressDoSetResult = (long)StateIs.Valid;
            // How to get back to primary thread to update ui 
             // TODO ThreadUiTextMessageDoSet insert passed method name
            ThreadUiProgressInvoke = new ThreadUiProgressDel(StatusLineChanged);
            ThreadUiProgressAsyncInvoke = new ThreadUiProgressAsyncDel(StatusLineChanged);
            //
            ThreadUiProgress = new ThreadUiProgressDel(ThreadUiProgressImpl);
            ThreadUiProgressAsync = new ThreadUiProgressAsyncDel(ThreadUiProgressImpl);
        }
        public virtual void ThreadUiProgressDo(ref Object SenderPassed, ProgressChangedEventArgs PassedThreadUiProgressUpdate) {
            ThreadUiProgressDoResult = (long)StateIs.Valid;
            if (ClassFeature.MdmThreadIsUsed) {
                if (ThreadUiProgress == null) {
                    ThreadUiProgressDoSet();
                    if (ThreadUiProgress == null) { return; }
                }
                // Get back to primary thread to update ui 
                ThreadUiProgressImpl(ref SenderPassed, PassedThreadUiProgressUpdate);
            }
        }
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxx end of THREAD TWO xxxxxxxxxxxxxxxxxxxx
        // xxxx THREAD ONE xxxx Communications
        #region Thread one receive messages from thread two (thread one portion)
        // xxxxxx THREAD ONE invoked messeages from THREAD TWO xxxxxxxxx
        /// Update UI from Dispatcher Thread 
        public virtual void ThreadUiTextMessageImpl(ref Object SenderPassed, String PassedThreadUiTextMessageContent) {
            ThreadUiTextMessageDoResult = (long)StateIs.Valid;
            if (ClassFeature.MdmThreadIsUsed) {
                if (ThreadUiTextMessage == null) {
                    ThreadUiTextMessageDoSet();
                    if (ThreadUiTextMessage == null) { return; }
                }
                // Get back to primary thread to update ui 
                // StatusLineChanged
                // Run new thread off Dispatched (primary thread) 
                ThreadUiTextMessageContent = PassedThreadUiTextMessageContent;
                if (ThreadUiTextMessageContent.Length == 0) { return; }
                // MessageMdmSendToPage(SenderPassed, PassedThreadUiTextMessageContent);
                Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, ThreadUiTextMessageAsyncInvoke, SenderPassed, ThreadUiTextMessageContent);
            }
        }
        // xxxxxx THREAD ONE invoked messeages from THREAD TWO xxxxxxxxx
        /// Update UI from Dispatcher Thread (ABSTRACT)
        public virtual void ThreadUiProgressImpl(ref Object SenderPassed, ProgressChangedEventArgs PassedThreadUiProgressUpdate) {
            ThreadUiProgressDoResult = (long)StateIs.Valid;
            if (ClassFeature.MdmThreadIsUsed) {
                if (ThreadUiProgress == null) {
                    ThreadUiProgressDoSet();
                    if (ThreadUiProgress == null) { return; }
                }
                // Get back to primary thread to update ui 
                // Run new thread off Dispatched (primary thread) 
                // StatusLineChanged(ref SenderPassed, PassedProgress);
                Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, ThreadUiProgressAsyncInvoke, SenderPassed, PassedThreadUiProgressUpdate);
            }
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region App Processing
        #region Start, Cancel and Pause Control
        #region Results
        public long iAppStart;
        public long iAppCancel;
        public long iAppPause;
        public long iAppActionDo;

        public long iAppCancelProcessing;
        public long iAppPauseProcessing;
        public long iAppComplete_Processing;
        public long iAppObject;
        //
        public long iAppDoProcessing;
        public long iAppDoProcessingPassed;
        public long iAppDoProcessing_SetOn;
        //
        public long iAppInitializeController;
        //
        public long iAppOptions_Set;
        //
        public long iAppCoreObjectSetUsingMapplication;
        public long iAppCoreObjectGetFromAppplication;
        #endregion
        #region Start, Cancel and Pause Buttons
        public virtual void  StartButtonPressedIsEnabled(bool PassedBool) { StartButtonPressed.IsEnabled = PassedBool; }
        public virtual void  CancelButtonPressedIsEnabled(bool PassedBool) { CancelButtonPressed.IsEnabled = PassedBool; }
        public virtual void  PauseButtonPressedIsEnabled(bool PassedBool) { PauseButtonPressed.IsEnabled = PassedBool; }

        public virtual void  StartButtonPressedContent(String PassedText) { StartButtonPressed.Content = PassedText; }
        public virtual void  CancelButtonPressedContent(String PassedText) { CancelButtonPressed.Content = PassedText; }
        public virtual void  PauseButtonPressedContent(String PassedText) { PauseButtonPressed.Content = PassedText; }

        public virtual void  ProgressBarMdm1InvalidateVisual(){ProgressBarMdm1.InvalidateVisual(); }
        public virtual void  THISInvalidateVisual(Object SenderPassed) {
            ((System.Windows.FrameworkElement)Sender).InvalidateVisual();
            ((System.Windows.FrameworkElement)Sender).BringIntoView();
            PageMainInvalidateVisual = true; // Page Name?
            PageMainBringIntoView = true;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Progress Bar
        public bool bImportProgressBarColored = false;
        public bool bImportProgressBarColorChanged = true;

        public void ControlsMdmCheckColor() {
            if (bImportProgressBarColorChanged) {
                if (RunPausePending
                    || RunActionState[RunPause, RunState] != RunTense_Off) {
                    ProgressBarMdm1Background(System.Windows.Media.Brushes.LightGreen);
                } else if (RunCancelPending
                    || RunActionState[RunCancel, RunState] == RunTense_Do
                    || RunActionState[RunCancel, RunState] == RunTense_Doing
                    || RunActionState[RunCancel, RunState] == RunTense_Did) {
                    ProgressBarMdm1Foreground(System.Windows.Media.Brushes.Red);
                    ProgressBarMdm1Background(System.Windows.Media.Brushes.Orange);
                } else if (RunStartPending
                    || RunActionState[RunRunDo, RunState] == RunTense_Do
                    || RunActionState[RunRunDo, RunState] == RunTense_Doing) {
                    ProgressBarMdm1Background(System.Windows.Media.Brushes.LightGray);
                    if (RunErrorDidOccur) {
                        ProgressBarMdm1Foreground(System.Windows.Media.Brushes.LightYellow);
                    } else {
                        ProgressBarMdm1Foreground(System.Windows.Media.Brushes.Green);
                    }
                } else if (RunActionState[RunRunDo, RunState] == RunTense_Did) {
                    ProgressBarMdm1Foreground(System.Windows.Media.Brushes.Blue);
                    ProgressBarMdm1Background(System.Windows.Media.Brushes.LightGray);
                } else {
                    ProgressBarMdm1Foreground(System.Windows.Media.Brushes.Green);
                    // ProgressBarMdm1Foreground(System.Windows.Media.Brushes.Blue);
                    ProgressBarMdm1Background(System.Windows.Media.Brushes.LightGray);
                }
                //
                // ProgressBarMdm1.Value = 0;
                // BringIntoView();
                ProgressBarMdm1.InvalidateVisual();
                ProgressBarMdm1.InvalidateVisual();
                bImportProgressBarColorChanged = false;
            }
        }

        #region $include Mdm.Srt.MinputTld MinputTldPageMain ProgressBarMdmUi_Bar_Class Colors
        public int iColorOfBar = icColorLightBlue;
        public const int icColorGreen = 1;
        public const int icColorBlue = 2;
        public const int icColorRed = 3;
        public const int icColorYellow = 4;
        public const int icColorLightBlue = 5;
        public const int icColorWhite = 6;
        #endregion
        #endregion
        #region Progress Bar 1
        public virtual void ProgressBarMdm1Foreground(Object PassedBrush) { 
            ProgressBarMdm1.Foreground = (System.Windows.Media.Brush) PassedBrush; }
        public virtual void  ProgressBarMdm1Background(Object PassedBrush) {
            ProgressBarMdm1.Background = (System.Windows.Media.Brush) PassedBrush;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region App Start Pause Cancel do the command Run Actions
        // Action Buttons
        public virtual void AppStart(Object SenderPassed, RoutedEventArgs RoutedEventItem) {
            iAppStart = (long)StateIs.Started;
            // Start the File Import
            RunAction = RunStart;
            RunMetric = RunState;
            RunTense = RunTense_Do;
            // LocalRunning = bYES;
            AppActionDo();
            // return iAppStart;
        }
        // TODO z$RelVs2 AppCancel AppLogic_ - Cancel Button Click - Do Cancel
        public virtual void AppCancel(Object SenderPassed, RoutedEventArgs RoutedEventItem) {
            iAppCancel = (long)StateIs.Started;
            // Cancel the File Import
            RunAction = RunCancel;
            RunMetric = RunState;
            RunTense = RunTense_Do;
            AppActionDo();
        }
        public virtual void AppPause(Object SenderPassed, RoutedEventArgs RoutedEventItem) {
            iAppPause = (long)StateIs.Started;
            // Pause the File Import
            // RunTense = RunTense_Do;
            // RunTense = RunTense;
            if (RunActionState[RunPause, RunState] == RunTense_Did
                ) {
                RunAction = RunPause;
                RunMetric = RunState;
                RunTense = RunTense_Done;
                AppActionDo();
            } else if (RunActionState[RunPause, RunState] == RunTense_Off
                || RunActionState[RunPause, RunState] == RunTense_Done
                ) {
                RunAction = RunPause;
                RunMetric = RunState;
                RunTense = RunTense_Do;
                AppActionDo();
            } else {
                // Update status, buttons, etc...
                AppActionDo();
            }
        }
        #endregion
        #region App Start Do Main with verify and OK to proceed
        public virtual void AppStartDoMain(ref Object SenderPassed, RoutedEventArgs RoutedEventItem) {
            #region $include Mdm.Srt.MinputTld MinputTldPageMain ProcessFields_Init
            iAppStart = 0;
            LocalId.Running = false;
            LocalId.Started = true;
            //
            StartButtonPressedIsEnabled(false);
            //
            ControlsMdmCheckColor();
            StatusUi.Item1Add(SenderPassed, "Starting File Progessing" + "\n");
            StatusUi.Item2Add(SenderPassed, "Verifying your entry, please wait..." + "\n");
            #endregion
            iAppStart = AppStartDoValidation(ref SenderPassed, RoutedEventItem); // Call to this page's validation
            #region $include Mdm.Srt.MinputTld MinputTldPageMain AppLogic_Proceed
            if (iAppStart != 0) {
                // TODO z$RelVs2 AppStartDoMain Prompt: OK to process?
                // TODO z$RelVs2 AppStartDoMain Are you sure?
                // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                /// TODO z$RelVs2 AppStartDoMain AnswerYes:
                //
                StatusUi.Item1Add(SenderPassed, "Processing starting, please wait..." + "\n");
                StatusUi.Item2Add(SenderPassed, "Initializing..." + "\n");
                //
                // Process Request
                //
                LocalId.LongResult = (int)AppDoProcessing();
                //
                // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                /// TODO z$RelVs2 AppStartDoMain AnswerNo:
                /// StatusLine2 = "Start MinputTldAppProcessRunFile AnswerNo";
                /// 
            }
            #endregion
        }
        #endregion
        #endregion
        // Do Processing Must be overriden
        #region virtual (abastract really)
        public virtual long AppDoProcessing() {
            iAppDoProcessing = (long)StateIs.Started;
            // Must be overriden
            // iAppDoProcessing = AppDoProcessing();
            // iAppDoProcessing = (long)StateIs.Valid;
            return iAppDoProcessing;
        }

        public virtual long AppStartDoValidation(ref Object SenderPassed, RoutedEventArgs RoutedEventItem) {
            iAppStart = (int)StateIs.Started;
            // Must be overriden
            // AppStartDoLoadCurrentFromPages();
            //
            return iAppStart;
        }
        // Run Doing set to On pending flags
        public long AppDoProcessing_SetOn() {
            iAppDoProcessing_SetOn = (long)StateIs.Valid;
            RunAbortIsOn = false;
            RunStartPending = true;
            RunCancelPending = false;
            RunPausePending = false;
            return iAppDoProcessing_SetOn;
        }
        #endregion
        #region App Completed Cancel Pause Functions
        // Completion
        public long AppComplete_Processing() {
            iAppComplete_Processing = (long)StateIs.Started;
            // if (!RunCancelPending && !RunAbortIsOn) {
            if (bYES == bYES) {
                LocalMessage.Msg = "Processing Completed.";
                PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg, true);
                // System.Windows.RoutedEventArgs RoutedEventItemTemp = new System.Windows.RoutedEventArgs();
                // RoutedEventTemp = null;
                // XUomVtvvXv.CallerAsynchronousEventsComplete_Click;
                // Set State
                RunAction = RunRunDo; RunMetric = RunState; RunTense = RunTense_Did;
                RunActionState[RunRunDo, RunState] = RunTense_Did;
                RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                    "R" + RunAction.ToString()  + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                // "Y" + LocalMessage.Msg);
                ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs);
            }
            return iAppComplete_Processing;
        }
        // Pause Processing
        public long AppPauseProcessing() {
            iAppPauseProcessing = (long)StateIs.Started;
            if (bYES == bYES) {
                LocalMessage.Msg = "Processing Pause";
                if (RunActionState[RunPause, RunState] == RunTense_Did) {
                    LocalMessage.Msg += " requesting resume.";
                    RunPausePending = bNO;
                    RunAction = RunPause; RunMetric = RunState; RunTense = RunTense_Done;
                    RunActionState[RunPause, RunState] = RunTense_Done;
                } else if (RunActionState[RunPause, RunState] == RunTense_Done) {
                    LocalMessage.Msg += " completed, normal processing resumed.";
                    RunPausePending = bNO;
                    RunAction = RunPause; RunMetric = RunState; RunTense = RunTense_Off;
                    RunActionState[RunPause, RunState] = RunTense_Off;
                } else {
                    LocalMessage.Msg += " command requested, submitting now.";
                    RunPausePending = bYES;
                    RunAction = RunPause; RunMetric = RunState; RunTense = RunTense_Do;
                    RunActionState[RunPause, RunState] = RunTense_Do;
                    // System.Windows.RoutedEventArgs RoutedEventItemTemp = new System.Windows.RoutedEventArgs();
                    // RoutedEventTemp = null;
                    // TODO maybe: XUomVtvvXv.CallerAsynchronousEventsPauseClick;
                    // Set State
                }
                // PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg, true);
                RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                    "R" + RunAction.ToString()  + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs); // (the ThreadUiProgressAsync)
            }
            return iAppPauseProcessing;
        }
        // Cancellation
        public long AppCancelProcessing() {
            iAppCancelProcessing = (long)StateIs.Started;
            // TODO AppCancelProcessing Add flag to clear display upon new run
            // TODO AppCancelProcessing Seems to have a button bug.
            // TODO AppCancelProcessing Check code.
            if (bYES == bYES) {
                RunCancelPending = true;
                // RunCancelled = true;
                LocalMessage.Msg = "Processing Cancelling.";
                PrintOutputMdm_PickPrint(Sender, 3, LocalMessage.Msg, true);
                // System.Windows.RoutedEventArgs RoutedEventItemTemp = new System.Windows.RoutedEventArgs();
                // RoutedEventTemp = null;
                // TODO AppCancelProcessing maybe: XUomVtvvXv.CallerAsynchronousEventsCancelClick;
                // Set State
                RunAction = RunCancel; RunMetric = RunState; RunTense = RunTense_Do;
                RunActionState[RunCancel, RunState] = RunTense_Do;
                RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                    "R" + RunAction.ToString()  + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs);
            }
            return iAppCancelProcessing;
        }
        // Cancellation Completed
        public long AppCancelCompleted() {
            iAppCancelProcessing = (long)StateIs.Started;
            if (bYES == bYES) {
                RunCancelPending = false;
                RunAbortIsOn = true;
                // TODO AppCancelProcessing maybe: XUomVtvvXv.CallerAsynchronousEventsCancelClick;
                LocalMessage.Msg = "ABORT: Run was CANCELLED sucessfully!!!";
                PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg, true);
                // Set State
                RunAction = RunCancel; RunMetric = RunState; RunTense = RunTense_Did;
                RunActionState[RunCancel, RunState] = RunTense_Did;
                ProgressChangedEventArgs eaOpcCurrentRunProgressMax = new ProgressChangedEventArgs(0,
                    "R" + RunAction.ToString()  + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                // "X" + LocalMessage.Msg);
                ThreadUiProgressAsync(ref Sender, eaOpcCurrentRunProgressMax);
            }
            return iAppCancelProcessing;
        }
        #endregion
        #region App Action Do
        public virtual void AppActionDo() {
            iAppActionDo = (long)StateIs.Started;
            //
            iAppActionWaitCounter = 0;
            // System.Windows.Media.Brush iTempColor = (System.Windows.Media.Brush)new object();
            int iTempColor = icColorLightBlue;
            bImportProgressBarColorChanged = true;
            ControlsMdmCheckColor();
            // Action the File Import
            if (bYES == bYES) {
                // StatusLine2 = "";
                // StatusUi.Item1Add(Sender, sRunActionVerb[RunAction] + " Import Tld Process File" + "\n");
                // StatusLine1.Text = "";
                //
                // TODO z$RelVs2 AppActionDo Are you sure?
                if (bYES == bYES) {
                    // AnswerYes:
                    //
                    #region Execute Request Action
                    #region Invoke Standard Code
                    StatusUi.Item1Add(Sender, "\n" + RunActionDoing[RunAction] + " Import Tld Process!" + "\n");
                    if (ConsoleVerbosity >= 6) {
                        PrintOutputMdm_PickPrint(Sender, 3, "C" + "\n" + RunActionDoing[RunAction] + " Import Process " + RunActionVerb[RunAction] + "..." + "\n", true);
                    }
                    // Start, Pause, Cancel
                    switch (RunAction) {
                        case (RunStart):
                            iTempColor = icColorGreen;
                            RoutedEventArgs RoutedEventItem = new RoutedEventArgs();
                            AppStartDoMain(ref Sender, RoutedEventItem);
                            break;
                        case (RunPause):
                            iTempColor = icColorYellow;
                            LocalId.LongResult = AppPauseProcessing();
                            break;
                        case (RunCancel):
                            iTempColor = icColorRed;
                            LocalId.LongResult = AppCancelProcessing();
                            break;
                        default:
                            break;
                    }
                    #endregion
                    #region Update Progress Bar
                    if (XUomPmvvXv != null) {
                        // XUomPmvvXv.ProgressBarMdm1Foreground((System.Windows.Media.Brush)MdmControlColorGet(iTempColor);
                        bImportProgressBarColorChanged = true;
                        ControlsMdmCheckColor();
                        //
                        ProgressBarMdm1.InvalidateVisual();
                        XUomPmvvXv.InvalidateVisual();
                        XUomPmvvXv.BringIntoView();
                    }
                    #endregion
                    #region Invoke Requested Async Action
                    // Might be set above or passed...?
                    if (bYES == bNO) {
                        sRunActionRequest = "$" + RunActionVerb[RunAction];
                        ThreadUiProgressChangedArgs = new ProgressChangedEventArgs(0, (Object)sRunActionRequest);
                        object[] oTemp = { this, ThreadUiProgressChangedArgs };
                        Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, ThreadUiProgressAsync, oTemp);
                    }
                    #endregion
                    #endregion
                    #region Modify State and Buttons to indicate State Change (ie. In Progress, Pending state)
                    switch (RunAction) {
                        #region Start
                        case (RunStart):
                            StartButtonPressedIsEnabled(bNO);
                            CancelButtonPressedIsEnabled(bYES);
                            PauseButtonPressedIsEnabled(bYES);
                            if (RunActionState[RunRunDo, RunState] == RunTense_Doing) { return; }
                            break;
                        #endregion
                        #region Pause
                        case (RunPause):
                            if (RunActionState[RunPause, RunState] == RunTense_Do
                                ) {
                                RunPausePending = bYES;
                                StartButtonPressedIsEnabled(bNO);
                            } else if (RunActionState[RunPause, RunState] == RunTense_DoNot
                                    || RunActionState[RunPause, RunState] == RunTense_DidNot
                                ) {
                                RunPausePending = bNO;
                                PauseButtonPressedIsEnabled(bNO);
                            } else if (RunActionState[RunPause, RunState] == RunTense_Off
                                ) {
                                RunPausePending = bNO;
                                PauseButtonPressedIsEnabled(bYES);
                            } else if (RunActionState[RunPause, RunState] == RunTense_Did
                                    || RunActionState[RunPause, RunState] == RunTense_Off
                                    || RunActionState[RunPause, RunState] == RunTense_Done
                                ) {
                                RunPausePending = bNO;
                                PauseButtonPressedIsEnabled(bYES);
                            } else if (RunActionState[RunPause, RunState] == RunTense_DoNot
                                ) {
                                RunPausePending = bNO;
                                // RunActionState[RunPause, RunState] = RunTense_Off;
                            }
                            PauseButtonPressedIsEnabled(bYES);
                            break;
                        #endregion
                        #region Cancel
                        case (RunCancel):
                            RunCancelPending = bYES;
                            RunAbortIsOn = bYES;
                            PauseButtonPressedIsEnabled(bNO);
                            StartButtonPressedIsEnabled(bNO);
                            // XUomVtvvXv.CallerAsynchronousEventsCancelClick(ref Sender, new RoutedEventArgs());
                            break;
                        #endregion
                        #region Default
                        default:
                            break;
                        #endregion
                    }
                    #endregion
                    #region Wait Handling
                    // Pause Thread briefly
                    if (bYES == bNO) {
                        System.Threading.Thread.Sleep(1000);
                        iAppActionWaitCounter = 1000;
                    }
                    // Initialize and wait 
                    if (bYES == bNO) {
                        double dFlashAdjust = (ProgressBarMdm1.Maximum / 100 * 30);
                        int iWaitMilliIncrement = 250;
                        int iWaitMilliIncrementMax = 60000;
                        bool bTempContinue = bYES;
                        int iDivRem = 0;
                        bImportProgressBarColorChanged = true;
                        ControlsMdmCheckColor();
                        // Wiat Loop
                        while (bTempContinue && iAppActionWaitCounter < iWaitMilliIncrementMax) {
                            System.Threading.Thread.Sleep(iWaitMilliIncrement);
                            Math.DivRem(iAppActionWaitCounter, 3000, out iDivRem);
                            if (iDivRem == 0) {
                                if (bImportProgressBarColored) {
                                    // ProgressBarMdm1Foreground(System.Windows.Media.Brushes.White;
                                    // ProgressBarMdm1Background(System.Windows.Media.Brushes.Yellow;
                                    bImportProgressBarColored = bNO;
                                    bImportProgressBarColorChanged = true;
                                    ControlsMdmCheckColor();
                                    // ProgressBarMdm1.Value += dFlashAdjust;
                                } else {
                                    // ProgressBarMdm1Foreground((System.Windows.Media.Brush)MdmControlColorGet(iTempColor);
                                    // ProgressBarMdm1Background(System.Windows.Media.Brushes.Yellow;
                                    bImportProgressBarColored = bYES;
                                    bImportProgressBarColorChanged = true;
                                    ControlsMdmCheckColor();
                                    // ProgressBarMdm1.Value -= dFlashAdjust;
                                }
                                //
                                if (iAppActionWaitCounter > 0) { StatusUi.Item1Add(Sender, "."); }
                            }
                            switch (RunAction) {
                                case (RunStart):
                                    if (!RunStartPending) { bTempContinue = bNO; }
                                    break;
                                case (RunPause):
                                    if (!RunPausePending) { bTempContinue = bNO; }
                                    break;
                                case (RunCancel):
                                    if (!RunCancelPending) { bTempContinue = bNO; }
                                    break;
                                default:
                                    break;
                            }
                            iAppActionWaitCounter += iWaitMilliIncrement;
                        }
                    }
                    #endregion
                    #region Check Pending State or Alter Button State
                    switch (RunAction) {
                        #region Start
                        case (RunStart):
                            if (RunStartPending) {
                                StartButtonPressedIsEnabled(bNO);
                                PauseButtonPressedIsEnabled(bNO);
                                CancelButtonPressedIsEnabled(bNO);
                                // TODO $ERROR AppActionDo Error DID NOT START!!!
                            } else {
                                StartButtonPressedIsEnabled(bNO);
                                PauseButtonPressedIsEnabled(bYES);
                                CancelButtonPressedIsEnabled(bYES);
                                //
                                LocalId.Running = bYES;
                                LocalId.Started = bYES;
                            }
                            break;
                        #endregion
                        #region Pause
                        case (RunPause):
                            // PauseButtonPressedContent("?";
                            if (RunPausePending) {
                                StartButtonPressedIsEnabled(bNO);
                                PauseButtonPressedIsEnabled(bYES);
                                CancelButtonPressedIsEnabled(bYES);
                                // TODO $ERROR AppActionDo Error DID NOT START!!!
                            } else {
                                StartButtonPressedIsEnabled(bNO);
                                PauseButtonPressedIsEnabled(bYES);
                                CancelButtonPressedIsEnabled(bYES);
                                //
                                LocalId.Running = bYES;
                                LocalId.Started = bYES;
                            }
                            break;
                        #endregion
                        #region Cancel
                        case (RunCancel):
                            if (RunCancelPending) {
                                // TODO $ERROR AppActionDo Error DID NOT CANCEL!!!
                                StartButtonPressedIsEnabled(bNO);
                                PauseButtonPressedIsEnabled(bNO);
                                CancelButtonPressedIsEnabled(bYES);
                            } else {
                                // LocalId.Running = bNO;
                                // MinputTldApp.Current.Shutdown();
                                StartButtonPressedIsEnabled(bYES);
                                PauseButtonPressedIsEnabled(bNO);
                                CancelButtonPressedIsEnabled(bNO);
                            }
                            break;
                        #endregion
                        #region Default
                        default:
                            break;
                        #endregion
                    }
                    #endregion
                    // TODO AppActionDo StatusLineChanged(ref Sender,new ProgressChangedEventArgs(0, UserState));
                    //
                    // ProgressBarMdm1Foreground((System.Windows.Media.Brush)MdmControlColorGet(iTempColor);
                    bImportProgressBarColorChanged = true;
                    ControlsMdmCheckColor();
                    //ProgressBarMdm1.InvalidateVisual();
                    THISInvalidateVisual(XUomPmvvXv);
                    // StatusUi.LineAdd(1, "\n");
                    //
                } else {
                    // AnswerNo:
                    // StatusLine1 = "Cancel MinputTldAppProcessRunFile AnswerNo" + "\n";
                }
            }
        }
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        public void Dave() { int i = 1; }
        public int bii;
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Srt.MinputTld MinputTldPageMain Mdm Status Line Control
        public virtual void StatusLineMdmTargetGet(String PassedText) {
            //
            // * Trace and Log
            //
            // C Xxx
            //
            // L 999
            // H 999
            // V 999
            // P 999
            // I 999
            //
            // M 9 Xxx
            // A 9 Xxx
            //
            // R Metric Tense Action
            // R 9 9 999
            // R 9 9 Xxx
            // TODO StatusLineMdmTargetGet move BoxPadding to constructor
            // BoxPadding = 0;
            //
            MessageStatusError = false;
            ProcessStatusAction = "";
            ProcessStatusTargetDouble = 0;
            ProcessStatusTarget = 0;
            MessageStatusSubTargetDouble = 0;
            ProcessStatusSubTarget = 0;
            ProcessStatusTargetState = 0;

            //
            ProgressBarMdm1Property = 0;
            MessageProperty2 = 0;
            // TODO StatusLineMdmTargetGet Clean this up, it is slow.
            if (PassedText.Length > 0) {
                ProcessStatusAction = PassedText.Substring(0, 1).ToUpper();
                if (ProcessStatusAction == "$") {
                    try { MessageTextOutConsole = PassedText.Substring(1); } catch { MessageTextOutConsole = "Missing Command"; }
                } else if (ProcessStatusAction == "C") {
                    if (PassedText.Length > 1) {
                        MessageTextOutConsole = PassedText.Substring(1);
                    } else { MessageTextOutConsole = ""; }
                } else if (ProcessStatusAction == "E") {
                    if (PassedText.Length > 1) {
                        MessageTextOutError = PassedText.Substring(1);
                    } else { MessageTextOutError = ""; }
                } else if (ProcessStatusAction == "A" || ProcessStatusAction == "M") {
                    try {
                        if (PassedText.Length > 2) {
                            MessageTextOutStatusLine = PassedText.Substring(2);
                        } else { MessageTextOutStatusLine = ""; }
                        MessageStatusSubTargetDouble = Convert.ToInt32(PassedText.Substring(1, 1));
                        ProcessStatusSubTarget = (int)MessageStatusSubTargetDouble;
                    } catch {
                        MessageStatusError = true; 
                        MessageStatusSubTargetDouble = 0;
                        MessageTextOutStatusLine = "";
                    }
                    // "L" "H" "V" "P"
                } else if (ProcessStatusAction == "L" // Progress Low
                    || ProcessStatusAction == "H" // Progress High
                    || ProcessStatusAction == "V" // Progress Value
                    || ProcessStatusAction == "P" // Progress ?
                    || ProcessStatusAction == "R" // RunAction
                    || ProcessStatusAction == "I" // Progress Image Change
                    ) {
                    try {
                        if (ProcessStatusAction == "R") {
                            MessageTextOutRunAction = "";
                            MessageTextOutRunAction = PassedText.Substring(2);
                        } else {
                            MessageTextOutProgress = "";
                            MessageTextOutProgress = PassedText.Substring(2);
                        }
                        try { MessageStatusSubTargetDouble = Convert.ToInt32(PassedText.Substring(1, 1)); } catch { MessageStatusSubTargetDouble = 0; }
                        ProcessStatusSubTarget = (int)MessageStatusSubTargetDouble;
                        //
                        try { ProcessStatusTargetState = Convert.ToInt32(PassedText.Substring(2, 1)); } catch { ProcessStatusTargetState = 0; }
                        try { ProgressBarMdm1Property = Convert.ToDouble(MessageTextOutConsole); } catch { ProgressBarMdm1Property = 0; }
                        //
                        if (PassedText.Length > 3) {
                            // scan for numeric value used in Progress high, low, value, 
                            // scan for numeric value used RunAction value
                            sTemp = "";
                            try {
                                for (iTemp0 = 3; iTemp0 < PassedText.Length; iTemp0++) {
                                    iTemp = Convert.ToInt32(PassedText.Substring(iTemp0, 1));
                                    sTemp += iTemp.ToString();
                                }
                            } catch { ; }
                            try { ProcessStatusTargetDouble = Convert.ToDouble(sTemp); } catch { ProcessStatusTargetDouble = 0; }
                            ProcessStatusTarget = (int)ProcessStatusTargetDouble;
                            if (ProcessStatusAction == "R") {
                                MessageTextOutRunAction = PassedText.Substring(iTemp0);
                            } else {
                                MessageTextOutProgress = PassedText.Substring(iTemp0);
                            }
                        }
                    } catch { MessageStatusError = true; }
                } else { MessageStatusError = true; }
            } else { MessageStatusError = true; }
            if (MessageStatusError) { ProcessStatusAction = "E"; }
        }
        //
        public virtual void StatusLineSizeDoAdjust(TextBoxManageDef BoxManagePassed, TextBox BoxPassed, String TextOutPassed) {
            BoxManagePassed.DisplayCount += 1;
            BoxManagePassed.DisplayAdjustCount += 1;
            // Initialize High Low
            BoxManagePassed.WidthCurrent = (double)TextOutPassed.Length;
            if (XUomPmvvXv != null) {
                if (BoxManagePassed.WidthHigh == 0) { 
                    // BoxManagePassed.WidthHigh = XUomPmvvXv.ActualWidth;
                    BoxManagePassed.WidthHigh = 50;
                    if (BoxManagePassed.BoxWidthCurrent == 0) {
                        if (Double.IsNaN(BoxPassed.Width)) {
                            // BoxPassed.Width = 0;
                            BoxManagePassed.BoxWidthCurrent = BoxManagePassed.WidthCurrent;
                        } else { BoxManagePassed.BoxWidthCurrent = BoxPassed.Width; }
                    }
                }
                if (BoxManagePassed.WidthLow == 0) { BoxManagePassed.WidthLow = 50; }
                if (BoxPassed != null) {
                    BoxManagePassed.BoxWidthCurrent = (double)XUomPmvvXv.ActualWidth 
                        - (BoxManagePassed.BoxPadding.dLeft + BoxManagePassed.BoxPadding.dRight);
                }
            }
            // Get Current Widths
            //if (BoxPassed != null) {
            //    BoxManagePassed.BoxWidthCurrent = (double)BoxPassed.Width;
            //} else { BoxManagePassed.BoxWidthCurrent = (double)BoxManagePassed.WidthCurrent; }
            //
            // Set High contatins the longest line longer than the TextBox width
            // if (BoxManagePassed.WidthCurrent > BoxManagePassed.BoxWidthCurrent) {
            if (Double.IsNaN(BoxManagePassed.BoxWidthCurrent) || BoxManagePassed.WidthCurrent > BoxManagePassed.BoxWidthCurrent) {
                if (BoxManagePassed.WidthCurrent > BoxManagePassed.WidthHigh) {
                    BoxManagePassed.WidthHigh = BoxManagePassed.WidthCurrent;
                }
            }
            // Set Low contains the longest lines shorter than TextBox width
            if (Double.IsNaN(BoxManagePassed.BoxWidthCurrent) || BoxManagePassed.WidthCurrent < BoxManagePassed.BoxWidthCurrent) {
                // the > GT below is correct
                if (BoxManagePassed.WidthCurrent > BoxManagePassed.WidthLow) {
                    BoxManagePassed.WidthLow = BoxManagePassed.WidthCurrent;
                }
            }
            // Adjust Page
            if (XUomPmvvXv != null) {
                SenderIsThis = XUomPmvvXv;
                // Adjust every DisplayAdjustCountMax lines
                if (BoxManagePassed.DisplayAdjustCount > BoxManagePassed.DisplayAdjustCountMax) {
                    // Call Page Adjust with desired width
                    if (BoxManagePassed.WidthHigh > BoxManagePassed.BoxWidthCurrent) {
                        //
                        PageSizeChangedDoAdjust(SenderIsThis, BoxManagePassed, BoxPassed, (BoxManagePassed.WidthHigh
                            + (BoxManagePassed.BoxPadding.dLeft + BoxManagePassed.BoxPadding.dRight) + 10), 0);
                        //
                    } else if (BoxManagePassed.WidthLow < BoxManagePassed.BoxWidthCurrent) {
                        //
                        PageSizeChangedDoAdjust(SenderIsThis, BoxManagePassed, BoxPassed, (BoxManagePassed.WidthLow
                            + (BoxManagePassed.BoxPadding.dLeft + BoxManagePassed.BoxPadding.dRight) + 10), 0);
                        //
                    }
                    //
                    BoxManagePassed.DisplayAdjustCount = 0;
                    //
                    if (BoxManagePassed.WidthHigh > 0) {
                        BoxManagePassed.WidthHigh = BoxManagePassed.WidthHigh * 0.75;
                    } else { BoxManagePassed.WidthHigh = 50; }
                    //
                    if (BoxManagePassed.WidthHigh > 0) {
                        BoxManagePassed.WidthLow = BoxManagePassed.WidthHigh * 0.50;
                    } else { BoxManagePassed.WidthLow = 50; }
                }
            }
        }
        //
        public virtual void StatusLineChanged(ref Object SenderPassed, String PassedText) {
            UserState = PassedText;
            ThreadUiProgressChangedArgs = new ProgressChangedEventArgs(0, UserState);
            StatusLineChanged(ref SenderPassed, ThreadUiProgressChangedArgs);
        }
        //
        public virtual void StatusLineChanged(ref Object SenderPassed, ProgressChangedEventArgs PassedProgressChangedEventArgs) {
            // StatusLineMdmChangePassedText
            // Output Text should be in MessageTextOutStatusLine for "A" "M" 
            // Output Text should be in MessageTextOutConsole for "C" "$" 
            UserState = (String)PassedProgressChangedEventArgs.UserState;
            StatusLineMdmTargetGet(UserState);
            //
            try {
                // Run State Change Handling
                switch (ProcessStatusAction) {
                    #region "$" Run Action Commands
                    case("$"):
                        // Pre evaluation of Arguments that
                        // may require actions or additonal info
                        // PROGRESS CHANGED
                        try {
                            UserState = (String)PassedProgressChangedEventArgs.UserState;
                        } catch { UserState = ""; return; }
                        if (UserState.Length > 0) { UserCommandPrefix = UserState.Substring(0, 1); }
                        if (UserState.Length > 1) { UserCommand = UserState.Substring(1); }
                        if (UserCommandPrefix == "$") {
                            sTemp = "Executing command";
                            // "$" Start is an action taken in this subclass
                            // Currently this worker thread start by... other means...
                            //
                            // "$" Cancell is an action taken in this subclass
                            // on an "R"eport a (re)Evaluate would be circular,
                            // RunActionExtractCommands recurses here to display any action taken...
                            ThreadUiTextMessageContent = "";
                            RunActionExtractCommands(ref SenderPassed, PassedProgressChangedEventArgs, this);
                        }
                        break;
                    #endregion
                    #region "R" Run Action
                    case ("R"):
                        /*
                        try { RunAction = ProcessStatusTarget; } catch { RunAction = RunTense_Off; }
                        try { RunMetric = ProcessStatusSubTarget; } catch { RunMetric = RunTense_Off; }
                        try { RunTense = ProcessStatusTargetState; } catch { RunTense = RunTense_Off; }
                        */
                        // MessageTextOutRunAction
                        // RunActionState[ProcessStatusTarget, ProcessStatusSubTarget] = RunActionState[ProcessStatusTarget, ProcessStatusSubTarget];
                        // TODO StatusLineMdmTargetGet use this and get rid of lines of code elsewhere:
                        // RunActionState[ProcessStatusTarget, ProcessStatusSubTarget] = ProcessStatusTargetState;
                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "[t" + ProcessStatusTarget.ToString() + "s" + ProcessStatusTarget.ToString() + "RoutedEvent" + ProcessStatusTargetState.ToString() + "]"); }
                        # region Run State State / Tense
                        // Run State Tense
                        switch (ProcessStatusTargetState) {
                            case (RunTense_Off):
                                break;
                            case (RunTense_Do):
                                MessageTextOutRunAction = RunActionVerb[RunAction] + " " + MessageTextOutRunAction;
                                break;
                            case (RunTense_DoNot):
                                break;
                            case (RunTense_Doing):
                                MessageTextOutRunAction = RunActionDoing[ProcessStatusTarget] + " " + MessageTextOutRunAction;
                                break;
                            case (RunTense_Did):
                                MessageTextOutRunAction = RunActionDid[ProcessStatusTarget] + " " + MessageTextOutRunAction;
                                break;
                            case (RunTense_DidNot):
                                break;
                            default:
                                break;
                        }
                        if (ConsoleVerbosity >= 6) {
                            StatusUi.TextConsoleAdd(SenderPassed, 
                                "[t" + ProcessStatusTarget.ToString() 
                                + "s" + ProcessStatusSubTarget.ToString() 
                                + "RoutedEvent" + ProcessStatusTargetState.ToString() 
                                + "] [" + UserState + "] : " + MessageTextOutRunAction);
                        }
                        #endregion
                        # region Run State Metrics
                        // Run State Metrics
                        switch (ProcessStatusSubTarget) {
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
                        switch (ProcessStatusTarget) {
                            case (RunCancel):
                                # region Run State State / Tense
                                // Run State Tense
                                switch (ProcessStatusTargetState) {
                                    case (RunTense_Off):
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "c"); }
                                        break;
                                    case (RunTense_Do):
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "c"); }
                                        break;
                                    case (RunTense_DoNot):
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "c"); }
                                        break;
                                    case (RunTense_Doing):
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "c"); }
                                        break;
                                    case (RunTense_Did):
                                        PauseButtonPressedContent("Pause");
                                        StartButtonPressedIsEnabled(bYES);
                                        PauseButtonPressedIsEnabled(bNO);
                                        CancelButtonPressedIsEnabled(bNO);
                                        StatusUi.Item1Add(SenderPassed, "Processing Cancelled!!!" + "\n");
                                        THISInvalidateVisual(SenderPassed);
                                        break;
                                    case (RunTense_DidNot):
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "c"); }
                                        break;
                                    default:
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "c"); }
                                        break;
                                }
                                bImportProgressBarColorChanged = true;
                                ControlsMdmCheckColor();
                                #endregion
                                break;
                            case (RunPause):
                                # region Run State State / Tense
                                // Run State Tense
                                switch (ProcessStatusTargetState) {
                                    case (RunTense_Off):
                                        PauseButtonPressedContent("Pause");
                                        StartButtonPressedIsEnabled(bNO);
                                        PauseButtonPressedIsEnabled(bYES);
                                        CancelButtonPressedIsEnabled(bYES);
                                        StatusUi.Item1Add(SenderPassed, "Pause available..." + "\n");
                                        THISInvalidateVisual(SenderPassed);
                                        break;
                                    case (RunTense_Do):
                                    case (RunTense_On):
                                        PauseButtonPressedContent("Resume");
                                        StartButtonPressedIsEnabled(bNO);
                                        PauseButtonPressedIsEnabled(bYES);
                                        CancelButtonPressedIsEnabled(bYES);
                                        StatusUi.Item1Add(SenderPassed, "Pause requested, waiting for process..." + "\n");
                                        THISInvalidateVisual(SenderPassed);
                                        break;
                                    case (RunTense_Doing):
                                        PauseButtonPressedContent("Resume");
                                        StartButtonPressedIsEnabled(bNO);
                                        PauseButtonPressedIsEnabled(bYES);
                                        CancelButtonPressedIsEnabled(bYES);
                                        StatusUi.Item1Add(SenderPassed, "Process pausing..." + "\n");
                                        THISInvalidateVisual(SenderPassed);
                                        break;
                                    case (RunTense_Did):
                                        PauseButtonPressedContent("Resume");
                                        StartButtonPressedIsEnabled(bNO);
                                        PauseButtonPressedIsEnabled(bYES);
                                        CancelButtonPressedIsEnabled(bYES);
                                        StatusUi.Item1Add(SenderPassed, "Process paused, resume to continue..." + "\n");
                                        THISInvalidateVisual(SenderPassed);
                                        break;
                                    case (RunTense_Done):
                                        PauseButtonPressedContent("Pause");
                                        StartButtonPressedIsEnabled(bNO);
                                        PauseButtonPressedIsEnabled(bYES);
                                        CancelButtonPressedIsEnabled(bYES);
                                        StatusUi.Item1Add(SenderPassed, "Pause done, waiting for process to resume..." + "\n");
                                        THISInvalidateVisual(SenderPassed);
                                        break;
                                    case (RunTense_DoNot):
                                        PauseButtonPressedContent("No Pause");
                                        // StartButtonPressedIsEnabled(bNO);
                                        PauseButtonPressedIsEnabled(bNO);
                                        // CancelButtonPressedIsEnabled(bYES);
                                        StatusUi.Item1Add(SenderPassed, "Pause not allowed..." + "\n");
                                        THISInvalidateVisual(SenderPassed);
                                        break;
                                    case (RunTense_DidNot):
                                        // An ERROR occured...
                                        PauseButtonPressedContent("Failed Pause");
                                        // StartButtonPressedIsEnabled(bNO);
                                        PauseButtonPressedIsEnabled(bNO);
                                        // CancelButtonPressedIsEnabled(bYES);
                                        StatusUi.Item1Add(SenderPassed, "Pause failed..." + "\n");
                                        THISInvalidateVisual(SenderPassed);
                                        break;
                                    default:
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "p"); }
                                        break;
                                }
                                bImportProgressBarColorChanged = true;
                                ControlsMdmCheckColor();
                                #endregion
                                break;
                            case (RunStart):
                                # region Run State State / Tense
                                // Run State Tense
                                switch (ProcessStatusTargetState) {
                                    case (RunTense_Off):
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "s"); }
                                        break;
                                    case (RunTense_Do):
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "s"); }
                                        break;
                                    case (RunTense_DoNot):
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "s"); }
                                        break;
                                    case (RunTense_Doing):
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "s"); }
                                        break;
                                    case (RunTense_Did):
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "s"); }
                                        break;
                                    case (RunTense_DidNot):
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "s"); }
                                        break;
                                    default:
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "s"); }
                                        break;
                                }
                                bImportProgressBarColorChanged = true;
                                ControlsMdmCheckColor();
                                #endregion
                                break;
                            case (RunNoOp4):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "n4"); }
                                break;
                            case (RunNoOp5):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "n5"); }
                                break;
                            case (RunInitialize):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "iTemp0"); }
                                break;
                            case (RunRunDo):
                                # region Run State State / Tense
                                // Run State Tense
                                switch (ProcessStatusTargetState) {
                                    case (RunTense_Off):
                                        RunStartPending = bNO;
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "d"); }
                                        break;
                                    case (RunTense_Do):
                                        RunStartPending = bYES;
                                        PauseButtonPressedContent("Pause");
                                        StartButtonPressedIsEnabled(bNO);
                                        PauseButtonPressedIsEnabled(bYES);
                                        CancelButtonPressedIsEnabled(bYES);
                                        break;
                                    case (RunTense_DoNot):
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "d"); }
                                        break;
                                    case (RunTense_Doing):
                                        RunStartPending = bNO;
                                        PauseButtonPressedContent("Pause");
                                        StartButtonPressedIsEnabled(bNO);
                                        PauseButtonPressedIsEnabled(bYES);
                                        CancelButtonPressedIsEnabled(bYES);
                                        break;
                                    case (RunTense_Did):
                                        RunStartPending = bNO;
                                        PauseButtonPressedContent("Pause");
                                        StartButtonPressedIsEnabled(bYES);
                                        PauseButtonPressedIsEnabled(bNO);
                                        CancelButtonPressedIsEnabled(bNO);
                                        StatusUi.Item1Add(SenderPassed, "Processing Completed." + "\n");
                                        THISInvalidateVisual(SenderPassed);
                                        break;
                                    case (RunTense_DidNot):
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "d"); }
                                        break;
                                    default:
                                        if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "d"); }
                                        break;
                                }
                                bImportProgressBarColorChanged = true;
                                ControlsMdmCheckColor();
                                #endregion
                                break;
                            case (RunUserInput):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "u<"); }
                                break;
                            case (RunOpen):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "o"); }
                                break;
                            case (RunMain_Do):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "md"); }
                                break;
                            case (RunMain_DoSelect):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "ms"); }
                                break;
                            case (RunMain_DoLock_Add):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "mll"); }
                                break;
                            case (RunMain_DoRead):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "mr"); }
                                break;
                            case (RunMain_DoValidate):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "mv"); }
                                break;
                            case (RunMain_DoAccept):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "ma"); }
                                break;
                            case (RunMain_DoReport):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "m>"); }
                                break;
                            case (RunMain_DoProcess):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "mp"); }
                                break;
                            case (RunMain_DoUpdate):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "mu"); }
                                break;
                            case (RunMain_DoWrite):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "mw"); }
                                break;
                            case (RunMain_DoLock_Remove):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "mlr"); }
                                break;
                            case (RunClose):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "mc"); }
                                break;
                            case (RunFinish):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "f"); }
                                if (ConsoleVerbosity >= 1) {
                                    StatusUi.Item1Add(SenderPassed, "Processing finished." + "\n");
                                    THISInvalidateVisual(SenderPassed);
                                }
                                break;
                            case (RunAbort):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "a"); }
                                break;
                            case (RunReloop):
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, @"^"); }
                                break;
                            case (RunFirst):
                                bImportProgressBarColorChanged = true;
                                ControlsMdmCheckColor();
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "m1"); }
                                break;
                            default:
                                if (RunAction == RunActionOrOptionX) { ; }
                                if (RunAction == RunActionOrOptionY) { ; }
                                if (RunAction == RunActionOrOptionZ) { ; }
                                if (RunAction == RunActionOrOption1) { ; }
                                if (RunAction == RunActionOrOption2) { ; }
                                if (ConsoleVerbosity >= 6) { StatusUi.TextConsoleAdd(SenderPassed, "z" + RunAction.ToString()); }
                                break;
                        }
                        //if (MessageTextOutRunAction.Length != 0) {
                        //    ProcessStatusAction = "C";
                        //    MessageTextOutConsole = "C" + UserState + ": " + MessageTextOutRunAction;
                        //}
                        #endregion
                        break;
                    #endregion
                    #region User Status X
                    case ("X"):
                        // start
                        break;
                    #endregion
                    #region User Status Y
                    case ("Y"):
                        // pause
                        break;
                    #endregion
                    #region User Status Z
                    case ("Z"):
                        // cancel
                        break;
                    #endregion
                    #region "I" Image Change for Progress
                    case("I"):
                        if (ProcessStatusSubTarget > 0) {
                            PageMainHeadingImage1Animate(true);
                        } else {
                            PageMainHeadingImage1Animate(false);
                        }
                        break;
                    #endregion
                    #region "L" "H" "V" "P" Completion Status Update
                    case ("L"):
                        ProgressBarMdm1.Minimum = ProgressBarMdm1Property;
                        break;
                    case ("H"):
                        ProgressBarMdm1.Maximum = ProgressBarMdm1Property;
                        break;
                    case ("V"):
                        ProgressBarMdm1.Value = ProgressBarMdm1Property;
                        bImportProgressBarColorChanged = true;
                        ControlsMdmCheckColor();
                        break;
                    case ("P"):
                        /*
                        if (ProgressBarMdm1Property > 0 && ProgressBarMdm1.Maximum > 0) {
                            ProgressBarMdm1.Value = ProgressBarMdm1.Maximum / ProgressBarMdm1Property * 100;
                        } else if (ProgressBarMdm1Property > 0) {
                            ProgressBarMdm1.Value = ProgressBarMdm1.Maximum;
                        } else {
                            ProgressBarMdm1.Value = 0;
                        }
                        */
                        break;
                    #endregion
                    #region "E" Error Message Handling
                    case ("E"):
                        break;
                    #endregion
                    #region "C" Console Output
                    case ("C"):
                        if (ProcessStatusAction == "C" && !ConsoleOn) { return; }
                        StatusUi.TextConsoleAdd(SenderPassed, MessageTextOutConsole);
                        //
                        StatusLineSizeDoAdjust(StatusUi.TextConsoleManage, StatusUi.TextConsoleBox, MessageTextOutConsole);
                        //
                        break;
                    #endregion
                    #region Status Line / Text Area Output
                    #region "A" Add to Text in Area
                    case ("A"):
                        if (!ConsoleTextOn) { return; }
                        if (MessageTextOutStatusLine.Length == 0) {
                            sTemp3 = "";
                        }
                        ProcessStatusTarget = ProcessStatusSubTarget;
                        switch (ProcessStatusTarget) {
                            case (2):
                                if (!ConsoleText2On) { return; }
                                StatusUi.Item2Add(SenderPassed, MessageTextOutStatusLine);
                                break;
                            case (3):
                                if (!ConsoleText3On) { return; }
                                StatusUi.Item3Add(SenderPassed, MessageTextOutStatusLine);
                                break;
                            case (4):
                                if (!ConsoleText4On) { return; }
                                StatusUi.Item4Add(SenderPassed, MessageTextOutStatusLine);
                                break;
                            case (1):
                            default:
                                if (!ConsoleTextOn && !ConsoleText1On) { return; }
                                StatusUi.Item1Add(SenderPassed, MessageTextOutStatusLine);
                                break;
                        }
                        break;
                    #endregion
                    #region "M" Replace Text Area with message
                    case ("M"):
                        if (!ConsoleTextOn) { return; }
                        ProcessStatusTarget = ProcessStatusSubTarget;
                        switch (ProcessStatusTarget) {
                            case (2):
                                if (!ConsoleText2On) { return; }
                                StatusUi.Item2Changed(SenderPassed, MessageTextOutStatusLine);
                                break;
                            case (3):
                                if (!ConsoleText3On) { return; }
                                StatusUi.Item3Changed(SenderPassed, MessageTextOutStatusLine);
                                break;
                            case (4):
                                if (!ConsoleText4On) { return; }
                                StatusUi.Item4Changed(SenderPassed, MessageTextOutStatusLine);
                                break;
                            case (1):
                            default:
                                if (!ConsoleTextOn) { return; }
                                StatusUi.Item1Changed(SenderPassed, MessageTextOutStatusLine);
                                break;
                        }
                        break;
                    #endregion
                    #endregion
                    default:
                        StatusUi.TextConsoleAdd(SenderPassed, "Error!!!! Invalid message" + "\n");
                        StatusUi.TextConsoleAdd(SenderPassed, "From " + SenderPassed.ToString() + ": " + UserState + "\n");
                        RunErrorDidOccur = true;
                        bImportProgressBarColorChanged = true;
                        ControlsMdmCheckColor();
                        break;
                }
            } catch {
                StatusUi.TextConsoleAdd(SenderPassed, "Error processing message" + "\n");
                StatusUi.TextConsoleAdd(SenderPassed, "From " + SenderPassed.ToString() + ": " + UserState + "\n");
                RunErrorDidOccur = true;
                bImportProgressBarColorChanged = true;
                ControlsMdmCheckColor();
            };
            // TODO NEED TO SEND AND "E" Error MESSAGE FROM IMPORT
            // if (RunErrorDidOccur) {
            // XUomPmvvXv.BringIntoView();
            // }
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Srt.MinputTld MinputTldPageMain Mdm Console Control
        public virtual void ConsoleMdmInitializeToController(ref Object RunFileConsolePassed) {
            // Optional Utility Services
            ((StdBaseRunFileConsoleDef)RunFileConsolePassed).XUomUrvvXvCreateNow = XUomUrvvXvCreateNow;
            ((StdBaseRunFileConsoleDef)RunFileConsolePassed).XUomClvvXvCreateNow = XUomClvvXvCreateNow;
            //
            if (((StdBaseRunFileConsoleDef)RunFileConsolePassed).ClassFeature.MdmButtonIsUsed) {
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ProgressBarMdm1 = ProgressBarMdm1;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).StartButtonPressed = StartButtonPressed;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).CancelButtonPressed = CancelButtonPressed;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).PauseButtonPressed = PauseButtonPressed;
            }
            // Special Delegats to hand MdmSendToPage feautres
            if (((StdBaseRunFileConsoleDef)RunFileConsolePassed).ClassFeature.MdmSendIsUsed) {
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).MessageMdmSendToPageNewLineSet = MessageMdmSendToPageNewLineImpl;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).MessageMdmSendToPageNewLine = MessageMdmSendToPageNewLineSetImpl;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).MessageMdmSendToPage = MessageMdmSendToPageImpl;
            }
            // Delegates in controll, worker threads and application
            // to direct output to the standard progress, StatusLine1-4 and Console
            if (((StdBaseRunFileConsoleDef)RunFileConsolePassed).ClassFeature.MdmThreadIsUsed) {
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ThreadUiProgressAsync = ThreadUiProgressAsync;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ThreadUiTextMessageAsync = ThreadUiTextMessageAsync;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ThreadUiProgressAsyncInvoke = ThreadUiProgressAsyncInvoke;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ThreadUiTextMessageAsyncInvoke = ThreadUiTextMessageAsyncInvoke;
            }
            // Console handling
            // TODO ConsoleMdmInitializeToController Separate these concerns properly...
            if (((StdBaseRunFileConsoleDef)RunFileConsolePassed).ClassFeature.MdmIsUsed) {
                //
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleVerbosity = ConsoleVerbosity;
                //
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleOn = ConsoleOn;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickConsoleOn = ConsolePickConsoleOn;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickConsoleBasicOn = ConsolePickConsoleBasicOn;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleToDisc = ConsoleToDisc;
                //
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceData = TraceData;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceOn = TraceOn;
                //
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).DoLogActivity = DoLogActivity;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceDisplayMessageDetail = TraceDisplayMessageDetail;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceHeadings = TraceHeadings;
                // Tracing Detail
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceDebugOn = TraceDebugOn;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceFirst = TraceFirst;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceHeadings = TraceHeadings;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceData = TraceData;
                //
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceIteration = TraceIteration;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceIterationOnNow = TraceIterationOnNow;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceIterationInitialState = TraceIterationInitialState;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceIterationRepeat = TraceIterationRepeat;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceIterationCheckPoint = TraceIterationCheckPoint;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceIterationOnForWarningGiven = TraceIterationOnForWarningGiven;
                //
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceDisplay = TraceDisplay;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceDisplayOnNow = TraceDisplayOnNow;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceDisplayInitialState = TraceDisplayInitialState;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceDisplayRepeat = TraceDisplayRepeat;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceDisplayCheckPoint = TraceDisplayCheckPoint;
                //
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceBug = TraceBug;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceBugOnNow = TraceBugOnNow;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceBugInitialState = TraceBugInitialState;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceBugRepeat = TraceBugRepeat;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceBugCheckPoint = TraceBugCheckPoint;
            }
            // Basic delegate point to presentation objects on forms
            if (((StdBaseRunFileConsoleDef)RunFileConsolePassed).ClassFeature.StatusUiIsUsed) {
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.BoxDelegatesCopyFrom(ref StatusUi);
            //
            //((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.Box1 = StatusUi.Box1;
            //((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.Box2 = StatusUi.Box2;
            //((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.Box3 = StatusUi.Box2;
            //((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.Box4 = StatusUi.Box2;
            //((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.TextConsole = StatusUi.TextConsole;
            //
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).PageSizeChangedDoAdjust = PageSizeChangedDoAdjust;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.Box1Manage.ScrollDo = StatusUi.Box1Manage.ScrollDo;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.Box2Manage.ScrollDo = StatusUi.Box2Manage.ScrollDo;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.Box3Manage.ScrollDo = StatusUi.Box3Manage.ScrollDo;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.Box4Manage.ScrollDo = StatusUi.Box4Manage.ScrollDo;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.TextConsoleManage.ScrollDo = StatusUi.TextConsoleManage.ScrollDo;
            }
        }
        public virtual bool ConsoleMdmAny() {
            if (!(ClassFeature.MdmTraceIsUsed | ClassFeature.MdmConsoleIsUsed | ClassFeature.MdmPrintIsUsed)) { return false; }
            if (TraceOn | ConsoleOn | ConsolePickConsoleOn | ConsolePickConsoleBasicOn) { return true; } else { return false; }
            return true;
        }

        public override void ConsoleMdmInitialize() {
            base.ConsoleMdmInitialize();
            if (ClassFeature.MdmTraceIsUsed) {
                ThreadUiProgressDoSet();
                ThreadUiTextMessageDoSet();
            }
            ConsoleMdmFlagsInitialize(ref Sender);
            //
            if (ClassFeature.MdmIsUsed) {
                TraceData = bNO;
                TraceOn = bOFF;
                ConsoleOn = bOFF;
                ConsolePickConsoleOn = bOFF;
                ConsolePickConsoleBasicOn = bOFF;
                //
                ConsoleVerbosity = 3;
            }
            //
            // ConsoleMdmInitializeToController(ref SenderPassed);
            //if ("Page is primary Application" != "Current.Application is primary Application") {
            //      XUomPmvvXv.ConsoleMdmInitialize(ref SenderPassed);
            //} else { // XUomApvvXv.ConsoleMdmInitialize(ref SenderPassed); }
        }

        public void ConsoleMdmFlagsInitialize(ref Object RunFileConsolePassed) {
            // Std_I0_Console
            if (((StdBaseRunFileConsoleDef)RunFileConsolePassed).ClassFeature.MdmIsUsed) {
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceData = bNO;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceOn = bON;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleOn = bOFF;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickConsoleOn = bON;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickConsoleBasicOn = bON;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleToDisc = bOFF;
                //
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).DoLogActivity = bYES;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceDisplayMessageDetail = bYES;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TraceHeadings = bNO;
                //
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleVerbosity = 3;
                // Display
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleOutput = "";
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleOutputLog = "";
                // <Area Id = "((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickConsole">
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleOn = bON;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickConsoleBasicOn = bON;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickConsoleToDisc = bOFF;
                // Display
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickConsoleOutput = "";
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickConsoleOutputLog = "";
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickTextConsole = "";
                // public ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickTextBlock;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickConsoleTextBlock = ""; // text block
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickConsoleTextPositionX = 0;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickConsoleTextPositionY = 0;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsolePickConsoleTextPositionZ = 0;
                // 
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleTextOn = bON;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleText0On = bOFF;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleText1On = bON;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleText2On = bON;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleText3On = bOFF;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleText4On = bOFF;
                ((StdBaseRunFileConsoleDef)RunFileConsolePassed).ConsoleText5On = bOFF;
            }
            //
            ((StdBaseRunFileConsoleDef)RunFileConsolePassed).sMessageText = "";
            ((StdBaseRunFileConsoleDef)RunFileConsolePassed).sMessageText0 = "";
            ((StdBaseRunFileConsoleDef)RunFileConsolePassed).MessageTextOutConsole = "";
            ((StdBaseRunFileConsoleDef)RunFileConsolePassed).MessageTextOutStatusLine = "";
            ((StdBaseRunFileConsoleDef)RunFileConsolePassed).MessageTextOutProgress = "";
            ((StdBaseRunFileConsoleDef)RunFileConsolePassed).MessageTextOutError = "";
            ((StdBaseRunFileConsoleDef)RunFileConsolePassed).MessageTextOutRunAction = "";
            // <Area Id = "TextConsole>
            ((StdBaseRunFileConsoleDef)RunFileConsolePassed).CommandLineRequest = "";
            ((StdBaseRunFileConsoleDef)RunFileConsolePassed).CommandLineRequestResult = 0;
            ((StdBaseRunFileConsoleDef)RunFileConsolePassed).TextConsole = "";
            if (((StdBaseRunFileConsoleDef)RunFileConsolePassed).ClassFeature.StatusUiIsUsed) {
                // TODO ConsoleMdmFlagsInitialize ??? IS BOX MANAGMENT USED WHEN BOXES ARE NOT?
                // TODO ConsoleMdmFlagsInitialize YES IS IS...
                if (((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.BoxManageIsUsed) {
                    ((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.Box1Manage.ScrollDo = true;
                    ((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.Box2Manage.ScrollDo = true;
                    ((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.Box3Manage.ScrollDo = true;
                    ((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.Box4Manage.ScrollDo = true;
                    ((StdBaseRunFileConsoleDef)RunFileConsolePassed).StatusUi.TextConsoleManage.ScrollDo = true;
                }
            }
        }

        public virtual void ConsoleChanged(Object SenderPassed, String PassedText) {
            // TextConsole.Text = "External output for display, please wait...";
            // ?????????????????ConsoleAddImpl(SenderPassed, PassedText);
        }
        #endregion
        public virtual void PageMainHeadingImage1Animate(bool PassedOnFlag) {
            // technically an abstract function
        }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
    }   
} // end of namespace Mdm
