using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
//
using Mdm;
//@@@CODE@@@using Mdm.Oss.ClipUtil;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Decl;
using Mdm.Oss.FileUtil;
//@@@CODE@@@using Mdm.Oss.Mapp;
using Mdm.Oss.Mobj;
// using Mdm.Pick.Syntax;
using Mdm.Pick.Console;
//@@@CODE@@@using Mdm.Oss.Support;
using Mdm.Oss.Threading;

/// <summary>
/// File Management System
/// Object entities, Support and Utility items.
/// </summary> 

namespace Mdm.Oss.FileUtil {



    /// <MyDocs>
    /// <MyMembers name="Class Object Types">
    /// <summary>
    /// <para> . </para>
    /// <para> The two main types of objects are File and Db (Database).</para>
    /// <para> Each of these may have:</para>
    /// <para> 1) A File Summary Object.</para>
    /// <para> 2) A basic file object (Fmain). </para>
    /// <para> 3) An ID object for identity. </para>
    /// <para> 4) An IO object used readers and writers.</para>
    /// <para> 5) One or more File Status Objects.</para>
    /// <para> 6) One of serveral column and attribute control classes.</para>
    /// </summary>
    /// </MyMembers>
    /// <MyMembers name="test">
    /// <summary>
    ///     The summary for this type.
    /// </summary>
    /// </MyMembers>
    /// 
    /// <MyMembers name="test2">
    /// <summary>
    ///     The summary for this other type.
    /// </summary>
    /// </MyMembers>
    /// </MyDocs>

    /// <include file='MfileObject.cs' path='MyDocs/MyMembers[@name="Class Object Types"]/*' />
    /// 'C:/Srt Project/Mdm/Mdm1/Mdm1Oss1/Mdm1Oss1File1/MfileDocumentation.xml'
    /// 'C:\Srt Project\Mdm\Mdm1\Mdm1Oss1\Mdm1Oss1File1\MfileDocumentation.xml' 
    /// <include file='xml_include_tag.doc' path='MyDocs/MyMembers[@name="Class Object Types"]/*' />
    
    
    #region Classes and ItemData Objects

    /// <summary>
    /// System Level execeptions, arguments and objects
    /// related to a file object.  Not implemented.
    /// </summary> 
    public class SysDef {
        // TODO System Objects - (TO BE MOVED) - xxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile System Objects
        #region $include Mdm.Oss.FileUtil Mfile System - AppDomain
        public CrossAppDomainDelegate x22 = null;  // Delegate
        public ApplicationException x21;
        public AppDomainUnloadedException x23;
        public AppDomainInitializer x24; // Delegate
        public AppDomain x24o;
        #endregion
        /* #region $include Mdm.Oss.FileUtil Mfile System - Action<T1> Signatures
        public Action AT0; // Delegate
        public Action<T1> AT1; //Delegate
        public Action<T1, T2> AT2; // Delegate
        public Action<T1, T2, T3> AT3; // Delegate
        public Action<T1, T2, T3, T4> AT4; // Delegate
        #endregion
        */
        #region $include Mdm.Oss.FileUtil Mfile System Exceptions
        // System Exceptions - Argument
        public ArgumentException x15 = null;
        public ArgumentOutOfRangeException x15a = null;
        public ArgumentNullException x15b = null;
        // System Exceptions - Arithmetic
        public ArithmeticException x15c = null;
        // System Exceptions - AccessViolation
        public AccessViolationException X41; // Memory Error
        // System Exceptions - DataMisaligned
        public DataMisalignedException x11a = null;
        // System Exceptions - Configuration
        public ConfigurationException x14 = null;
        #endregion
        // .Net - Property change management here.
        // Mdm - Headings and Titles.
        // Mdm - Run Control
        #region $include Mdm.Oss.FileUtil Mfile System Windows
        // System Windows - StartupEvent
        public StartupEventHandler ooStartTemp = null; // Delegate Application Startup
        public StartupEventArgs ooStartTempA = null; // Delegate Application Startup Arguments
        // System Windows - Controls - TextChanged
        public TextChangedEventHandler ooControlTecxChangedEvent;
        public TextChangedEventHandler ooControlTextChanged;
        #endregion
        // <Area Id = "FileAction_DoMessages">
        public String sMformStatusMessage;
        public String sMessageBoxMessage;
        // XXX - (TO BE MOVED) END - xxxxxxxxxxxxxxxxxxxxxx
    #endregion
        public SysDef() {
            // List of used
            sMessageBoxMessage = "";
            sMformStatusMessage = "";
        }
    }

    /// <summary>
    /// A File Managment System Object.
    /// Meta data for general file object defining 
    /// file stream objects (primary/auxillary).
    /// Equivalent to "this" Mfile.
    /// </summary> 
    /// <remarks>
    /// Data context specific to this file
    /// system object not already present in
    /// the base classes (run, file, etc.)
    /// </remarks> 
    public class MetaDef {
        public bool UsePrimary;
        public DateTime tdtPrimaryLastTouched;
        public DateTime tdtAuxiliaryLastTouched;

        public MetaDef() {
        }
    }

    #region $include Mdm.Oss.FileUtil Mfile SourceFileAction
    /// <summary>
    ///  <para> System control object for Actions
    /// this would be aggregated with the 
    /// Run Action, File Do, and File Action objects
    /// in the case of logging, tracing and system event history.
    /// Also used in exceptions data collection. </para> 
    ///  <para> . </para> 
    ///  <para> Note 1: A Core Object (strongly typed) Verb 
    /// executes and acts on the Object and Target 
    /// to generate a Result and/or Result Object. </para> 
    ///  <para> Note 2: Applications fill this information in
    /// typically to match the internal implementation
    /// used in the application. </para> 
    /// </summary> 
    public class ActionInfoDef {
        //
        // CORE BEHAVIOR SECTION
        // OBJECT, TARGET, Result, VERB
        public long omvOfObject;
        public long omvOfTarget;
        public long omvOfResult;
        public long omvOfExistStatus;
        public long omvOfVerb;
        // omvOfVerb  = omvOfObject + omvOfTarget + omvOfResult;
        // Working Variables
        public String ResultName;
        public long Result;
        public String ResultDescription;

        public ActionInfoDef() {
        }
    }

    /// <summary>
    /// <para>A File Action to be taken on either the
    /// primary or auxillary streams.</para>
    /// <para> . </para>
    /// <para> File Actions include:</para>
    /// <para> 1) ListGet</para>
    /// <para> 2) Check</para>
    /// <para> 3) Open</para>
    /// <para> 4) Close</para>
    /// </summary> 
    public class FileActionDef {
        //
        #region Declarations
        // <Area Id = "MdmStandardFileInformation">
        public Mfile FileObject;
        public FileMainDef FmainObject;
        // <Area Id = "SourceDetailsProperties">
        // <Area Id = "IoActionBeingPerformed">
        // <Area Id = "SourceFileAction">
        public ActionInfoDef ActionInfo;

        protected internal long ipToDo;
        /// <summary>
        /// The File Action to perform.
        /// For example GetList, CheckList, Open, Close...
        /// </summary> 
        public long ToDo {
            get { return ipToDo; }
            set {
                if (Enum.IsDefined(typeof(FileAction_Do), value)) {
                    ipToDo = value;
                    spName = Enum.GetName(typeof(FileAction_Do), ipToDo);
                }
            }
        }
        protected internal String spName;
        /// <summary>
        /// The text name of the File Action.
        /// </summary> 
        public String Name {
            get { return spName; }
            set { spName = value; }
        }
        protected internal String spKeyName;
        /// <summary>
        /// The id of File Action Transaction
        /// </summary> 
        public String KeyName {
            get { return spKeyName; }
            set { spKeyName = value; }
        }
        // public String Result;
        protected internal long ipResult;
        public long Result {
            get { return ipResult; }
            set {
                ipResult = value;
                if (Enum.IsDefined(typeof(StateIs), value)) {
                    spResultName = Enum.GetName(typeof(StateIs), ipResult);
                } else { spResultName = "Invalid Result"; }
            }
        }
        protected internal String spResultName;
        public String ResultName {
            get { return spResultName; }
            set { spResultName = value; }
        }
        //
        public object ResultObject;
        //
        protected internal String spDirectionName;
        public String DirectionName {
            get { return spDirectionName; }
            set { spDirectionName = value; }
        }
        // direction
        protected internal int ipDirection;
        public int Direction {
            get { return ipDirection; }
            set {
                if (Enum.IsDefined(typeof(FileAction_DirectionIs), value)) {
                    ipDirection = value;
                    spDirectionName = Enum.GetName(typeof(FileAction_DirectionIs), ipDirection);
                }
            }
        }
        //
        protected internal String spModeName;
        public String ModeName {
            get { return spModeName; }
            set { spModeName = value; }
        }
        protected internal long ipMode;
        public long Mode {
            get { return ipMode; }
            set {
                if (Enum.IsDefined(typeof(FileAction_Do), value)) {
                    ipMode = value;
                    spModeName = Enum.GetName(typeof(FileAction_Do), ipMode);
                }
            }
        }
        //
        public long ModeResult;

        public long FileReadMode;

        public bool DoRetry;

        public bool DoClearTarget;

        public bool DoGetUiVs;

        // <Area Id = "SourceFileActionItFlags">
        /*
         //   0    - Null
         //   1    - DoesExist
         //   2    - Create
         //   4    - Open
         //   8    - Close
         //   16   - Delete
         //   32   - Empty (Delete All)
         //   64   - Shrink
         //   128  - Expand
         //   256  - Lock
         //   512  - Unlock
         //   1024 - Defragment
         //   2048 - bRead Only
         //   4096 - Rebuild
         //   8192 - Rebuild Statistics
         //   16384 - x
         //   32768 - x
         //   65536 - x
         */
        public List<Object> FileException;
        #endregion

        public void FileExceptionClear() { FileException = new List<Object>(); }

        /// <summary>
        /// Main Method - Create a File Action associated with the passed file.
        /// </summary> 
        public FileActionDef(Mfile MfilePassed)
            : this() {
            FileObject = MfilePassed;
        }

        /// <summary>
        /// Default File Action not (yet) associated with a file handler.
        /// </summary> 
        public FileActionDef() {
            FileObject = null;
            ToDo = (long)StateIs.Undefined;
            Name = "";
            Mode = (long)FileAction_Do.NotSet;
            ModeResult = (long)FileAction_Do.NotSet;
            Result = (long)FileAction_Do.NotSet;
            ResultName = "";
            ResultObject = new Object();
            DoRetry = false;
            DoClearTarget = false;
            DoGetUiVs = false;
            ActionInfo = new ActionInfoDef();
            FileException = new List<Object>();
        }

        /// <summary>
        /// Copy the File Action to the Passed Target.
        /// </summary> 
        public void CopyTo(ref FileActionDef FaPassed) {
            FaPassed.FileObject = FileObject;
            FaPassed.ToDo = ToDo;
            FaPassed.Name = Name;
            FaPassed.Mode = Mode;
            FaPassed.ModeResult = ModeResult;
            FaPassed.Result = Result;
            FaPassed.ResultName = ResultName;
            FaPassed.ResultObject = ResultObject;
            FaPassed.DoRetry = DoRetry;
            FaPassed.DoClearTarget = DoClearTarget;
            FaPassed.DoGetUiVs = DoGetUiVs;
            FaPassed.ActionInfo = ActionInfo;
            FaPassed.FileException = FileException;
        }

        /// <summary>
        /// Copy the File Action from the Passed Souce.
        /// </summary> 
        public void CopyFrom(ref FileActionDef FaPassed) {
            FileObject = FaPassed.FileObject;
            ToDo = FaPassed.ToDo;
            Name = FaPassed.Name;
            Mode = FaPassed.Mode;
            ModeResult = FaPassed.ModeResult;
            Result = FaPassed.Result;
            ResultName = FaPassed.ResultName;
            ResultObject = FaPassed.ResultObject;
            DoRetry = FaPassed.DoRetry;
            DoClearTarget = FaPassed.DoClearTarget;
            DoGetUiVs = FaPassed.DoGetUiVs;
            ActionInfo = FaPassed.ActionInfo;
            FileException = FaPassed.FileException;
        }

    }
    #endregion

    #region File Type, Status, Options, etc.
    /// <summary>
    /// A general purpose file options flag set.
    /// Can be used for connections, files, file items or rows
    /// or as needed.
    /// </summary> 
    public class FileOptionsDef : DefStdBase {
        #region OptionItFlags Declaration
        // Option File Flags
        public bool DoCheckFileDoesExist; // E
        public bool DoCheckFileDoesNotExist; // N
        public bool DoCreateFileDoesNotExist; // CN
        public bool DoCreateFileNew; // C
        public bool DoCreateFileMustNotExist; // CM
        public bool DoDeleteFile; // D
        // Option Item Flags
        public bool DoOverwriteExistingItem; // O
        public bool DoCheckItemIds;
        public bool DoCheckItemIdDoesExist; // E
        public bool DoCheckItemIdDoesNotExist; // ?
        public bool DoEnterEachItemId; // I
        public bool DoConvertItem; // C
        public String sConvertableItem;
        // Option Run Flags
        public bool DoLogActivity;// L
        public bool DoProceedAutomatically; // A
        // File Bulk Character Conversion (Function)
        // OptionItFlags
        public int OptionItemConvertItFlag;
        public int OptionItemCreateItFlag;
        public int OptionItemWriteItFlag;
        #endregion
        public String FileOptionsString;

        /// <summary>
        /// A general purpose file options flag set.
        /// </summary> 
        public FileOptionsDef() {

        }

        /// <summary>
        /// Standard clear all flags.
        /// </summary> 
        public void DataClear() {
            FileOptionsString = "";
            // Flow
            DoLogActivity = false;
            DoProceedAutomatically = false;
            // File
            DoCheckFileDoesExist = false;
            DoCreateFileNew = false;
            DoCreateFileMustNotExist = false;
            DoDeleteFile = false;
            // Item
            DoOverwriteExistingItem = false;
            DoCheckItemIdDoesExist = false;
            DoEnterEachItemId = false;
            DoConvertItem = false;
            // Conversion
            OptionItemConvertItFlag = 1;
            OptionItemCreateItFlag = 0;
        }
        // Parse
        public long OptionsParseResult;
        /// <summary>
        /// Set file options using passed console string.
        /// </summary> 
        public FileOptionsDef(String FileOptionsStringPassed) {
            OptionsParseResult = OptionsParse(FileOptionsStringPassed);
        }

        /// <summary>
        /// Create a console string for the current options.
        /// </summary> 
        public long OptionsParseToString(FileOptionsDef FileOptPassed) {
            OptionsParseResult = (long)StateIs.Started;

            String sCurrentString = "";
            if (FileOptPassed.DoLogActivity) { sCurrentString += "L"; }
            if (FileOptPassed.DoProceedAutomatically) { sCurrentString += "A"; }

            if (FileOptPassed.DoCheckFileDoesExist) { sCurrentString += "F"; }
            if (FileOptPassed.DoCreateFileNew) { sCurrentString += "N"; }
            if (FileOptPassed.DoCreateFileMustNotExist) { sCurrentString += "M"; }
            if (FileOptPassed.DoDeleteFile) { sCurrentString += "D"; }

            if (FileOptPassed.DoOverwriteExistingItem) { sCurrentString += "O"; }
            if (FileOptPassed.DoCheckItemIdDoesExist) { sCurrentString += "E"; }
            if (FileOptPassed.DoEnterEachItemId) { sCurrentString += "I"; }
            if (FileOptPassed.DoConvertItem) { sCurrentString += "D"; }

            FileOptPassed.FileOptionsString = sCurrentString;

            OptionsParseResult = (long)StateIs.Successful;
            return OptionsParseResult;
        }

        /// <summary>
        /// Parses the passed console compatable string and
        /// sets the currents options per the text.
        /// </summary> 
        public virtual long OptionsParse(String FileOptionsPassed) {
            OptionsParseResult = (long)StateIs.Started;
            String sOptionString = "";
            int iForCounter = 0;
            DataClear();
            if (FileOptionsPassed.Contains("(")) { FileOptionsPassed = FileOptionsPassed.FieldLast("("); }
            if (FileOptionsPassed.Contains(")")) { FileOptionsPassed = FileOptionsPassed.Field(")", 1); }
            String[] OptionsList = FileOptionsPassed.Split((" ").ToCharArray());
            //For Loop
            for (iForCounter = 0; iForCounter <= OptionsList.Length; iForCounter++) {
                sOptionString = OptionsList[iForCounter].Trim();
                sOptionString = sOptionString.Trim((",").ToCharArray());
                sOptionString = sOptionString.Trim((@"\").ToCharArray());
                if (iForCounter > 0) { FileOptionsString += ", "; }
                FileOptionsString += sOptionString;
                switch (sOptionString) {
                    // Files
                    case "E":
                        DoCheckFileDoesExist = true;
                        break;
                    case "N":
                        DoCheckFileDoesNotExist = true;
                        break;
                    case "CE":
                        DoCreateFileDoesNotExist = true;
                        break;
                    case "CM":
                        DoCreateFileMustNotExist = true;
                        break;
                    case "C":
                        DoCreateFileNew = true;
                        break;
                    case "D":
                        DoDeleteFile = true;
                        break;

                    // Item Ids
                    case "I":
                        DoCheckItemIds = true;
                        break;
                    case "IE":
                        DoCheckItemIdDoesExist = true;
                        break;
                    case "IN":
                        DoCheckItemIdDoesNotExist = true;
                        break;
                    case "CV":
                        DoConvertItem = true;
                        break;
                    case "EI":
                        DoEnterEachItemId = true;
                        break;
                    case "O":
                        DoOverwriteExistingItem = true;
                        break;

                    // Other Options
                    case "L":
                        DoLogActivity = true;
                        break;
                    case "A":
                        DoProceedAutomatically = true;
                        break;
                    case "(":
                    case ")":
                    case " ":
                    case ",":
                    case "/":
                        // Valid composition characters to be removed (a,b,c) /a /b /c
                        break;
                    default:
                        OptionsParseResult = (long)StateIs.Undefined;
                        LocalMessage.Msg = "Command Line Option (" + sCurrentString + ") does not exist";
                        // XUomCovvXv.TraceMdmDo(ref Sender, bIsMessage, iNoOp, iNoOp, OptionsParseResult, XUomCovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg + "\n");
                        throw new NotSupportedException(LocalMessage.Msg);
                }
            }
            return OptionsParseResult;
        }
    }

    /// <summary>
    /// A general purpose file options flag set.
    /// Can be used for connections, files, file items or rows
    /// or as needed.
    /// </summary> 
    public class FileStatusDef {
        #region FileStatusDef Declaration
        // <Area Id = "IoStateStatus">
        protected internal long ipStatusCurrent;
        public long StatusCurrent {
            get { return ipStatusCurrent; }
            set { ipStatusCurrent = value; }
        }
        public bool bpNameIsValid;
        public bool NameIsValid {
            get { return bpNameIsValid; }
            set { bpNameIsValid = value; }
        }
        // property bool HadError
        public bool bpHadError;
        public bool HadError {
            get { return bpHadError; }
            set { bpHadError = value; }
        }
        // ID of the File not the record (i.e. FSO)
        // property bool IdExists
        public bool bpIdExists;
        public bool IdExists {
            get { return bpIdExists; }
            set { bpIdExists = value; }
        }
        // property bool DoesExist
        protected internal bool bpDoesExist;
        public bool DoesExist {
            get { return bpDoesExist; }
            set { bpDoesExist = value; }
        }
        // property bool IsValid
        protected internal bool bpIsValid;
        public bool IsValid {
            get { return bpIsValid; }
            set { bpIsValid = value; }
        }
        // <Area Id = "DoesExist">
        protected internal long ipDoesExistResult;
        public long DoesExistResult {
            get { return ipDoesExistResult; }
            set { ipDoesExistResult = value; }
        }
        protected internal bool bpIsOpen;
        public bool IsOpen {
            get { return bpIsOpen; }
            set { bpIsOpen = value; }
        }
        // <Area Id = "IsOpen"
        protected internal long ipIsOpenResult;
        public long IsOpenResult {
            get { return ipIsOpenResult; }
            set { ipIsOpenResult = value; }
        }
        // <Area Id = "IsCreating"
        protected internal bool bpIsCreating;
        public bool IsCreating {
            get { return bpIsCreating; }
            set { bpIsCreating = value; }
        }
        protected internal bool bpIsCreated;
        public bool IsCreated {
            get { return bpIsCreated; }
            set { bpIsCreated = value; }
        }
        // <Area Id = "IsCreating"
        protected internal long ipIsCreatingResult;
        public long IsCreatingResult {
            get { return ipIsCreatingResult; }
            set { ipIsCreatingResult = value; }
        }

        // property bool IsInitialized
        protected internal bool bpIsInitialized;
        public bool IsInitialized {
            get { return bpIsInitialized; }
            set { bpIsInitialized = value; }
        }
        // property bool DoKeepOpen
        protected internal bool bpDoKeepOpen;
        public bool DoKeepOpen {
            get { return bpDoKeepOpen; }
            set { bpDoKeepOpen = value; }
        }
        protected internal bool bpIsConnecting;
        public bool IsConnecting {
            get { return bpIsConnecting; }
            set {
                bpIsConnecting = value;
            }
        }
        protected internal bool bpIsConnected;
        public bool IsConnected {
            get { return bpIsConnected; }
            set {
                bpIsConnected = value;
            }
        }
        // <Area Id = "IsConnecting"
        protected internal long ipIsConnectingResult;
        public long IsConnectingResult {
            get { return ipIsConnectingResult; }
            set { ipIsConnectingResult = value; }
        }
        // property bool DoClose
        protected internal bool bpDoClose;
        public bool DoClose {
            get { return bpDoClose; }
            set { bpDoClose = value; }
        }
        // property bool DoDispose
        protected internal bool bpDoDispose;
        public bool DoDispose {
            get { return bpDoDispose; }
            set { bpDoDispose = value; }
        }
        // property bool NameCurrentResult
        protected internal long ipNameCurrentResult;
        public long NameCurrentResult {
            get { return ipNameCurrentResult; }
            set { ipNameCurrentResult = value; }
        }
        //
        // property bool NameIsChanged
        protected internal bool bpNameIsChanged;
        public bool NameIsChanged {
            get { return bpNameIsChanged; }
            set { bpNameIsChanged = value; }
        }
        // property bool ItemIsAtEnd
        protected internal bool bpItemIsAtEnd;
        public bool ItemIsAtEnd {
            get { return bpItemIsAtEnd; }
            set { bpItemIsAtEnd = value; }
        }
        // property bool HasCharacters
        protected internal bool bpHasCharacters;
        public bool HasCharacters {
            get { return bpHasCharacters; }
            set { bpHasCharacters = value; }
        }
        // property bool DoClose
        protected internal bool bpCharactersWereFound;
        public bool CharactersWereFound {
            get { return bpCharactersWereFound; }
            set { bpCharactersWereFound = value; }
        }
        //  Read Status
        // property bool ReadIsAtEnd
        protected internal bool bpReadIsAtEnd;
        public bool ReadIsAtEnd {
            get { return bpReadIsAtEnd; }
            set { bpReadIsAtEnd = value; }
        }
        // property bool ReadError
        protected internal bool bpReadError;
        public bool ReadError {
            get { return bpReadError; }
            set { bpReadError = value; }
        }
        //  Write Status
        // property bool BytesWereWriten
        protected internal bool bpBytesWereWriten;
        public bool BytesWereWriten {
            get { return bpBytesWereWriten; }
            set { bpBytesWereWriten = value; }
        }
        #endregion
        public FileStatusDef() { DataClear(); }

        /// <summary>
        /// Standard clear all flags.
        /// </summary> 
        public void DataClear() {
            ipStatusCurrent = (long)StateIs.None;

            bpDoesExist = false;
            ipDoesExistResult = (long)StateIs.None;
            //
            bpIsInitialized = false;
            bpIsValid = false;
            bpNameIsValid = false;
            //
            bpIsOpen = false;
            ipIsOpenResult = (long)StateIs.None;
            bpDoKeepOpen = true;
            bpDoClose = false;
            bpDoDispose = false;
            //
            bpIsConnected = false;
            bpIsConnecting = false;
            ipIsConnectingResult = (long)StateIs.None;
            //
            bpIsCreated = false;
            bpIsCreating = false;
            ipIsCreatingResult = (long)StateIs.None;
            //
            bpIdExists = false;
            //
            NameCurrentResult = (long)StateIs.None;
            NameIsChanged = false;
            ItemIsAtEnd = false;
            HasCharacters = false;
            //  Character Controls
            CharactersWereFound = false;
            //  Read Status
            ReadIsAtEnd = false;
            ReadError = false;
            //  Write Status
            BytesWereWriten = false;
        }
    }

    /// <summary>
    /// Delimited Separators Common defines the hierarchy
    /// of characters used as delimiters
    /// and support functions for delimited data processing.
    /// </summary> 
    public class DelSepDef {
        // Delimited Separators Common
        #region $include Mdm.Oss.FileUtil Mfile FileField Separator ItemData
        // <Area Id = "Col(Attr)Fields">
        public bool ItemAttrSet = false;
        public int ItemAttrInvalid;
        // Ascii Attr Pointers
        public int ItemAttrIndex; // Current array pointer.  Can change any time and is free of build proces
        public int ItemAttrCount; // ItemData Items in Item / Row / Item
        public int ItemAttrCountTotal; // Accumulator for shrinking work buffer
        public int ItemAttrCounter; // Current Attr being loaded
        public int ItemAttrMaxIndex; // Total Attrs in Item
        public int ItemAttrMaxIndexTemp;

        // Working values Fields
        public int iItemDataAttrEos2Index; // End of Column Separator 2
        public int iItemDataAttrEos1Index; // End of Column Separator 1
        public int iItemDataAttrEosIndex; // End of Column Sub-Value
        public int iItemDataAttrEovIndex; // End of Column Value
        //
        public int iItemDataAttrEoaIndex; // End of Column
        public int iItemDataAttrEoaIndexEnd; // End of Column
        public int iItemDataAttrEorIndex; // End of Row
        public int iItemDataAttrEofIndex; // End of File
        // Character Pointers
        public int iItemDataCharIndex; // DataItem Character Pointer
        public int iItemDataCharEobIndex; // DataItem Character Pointer to end of block
        public int iItemDataCharEofIndex; // DataItem Character Pointer to end of File
        // <Area Id = "AsciiOpenOptions">
        public int iAsciiOpenOptions;
        #endregion

        public DelSepDef() {

        }
    }

    #region $include Mdm.Oss.FileUtil Mfile FileTypeName
    /// <summary>
    /// A Management Class for File Type (Items).
    /// Used extensively by the File Management System
    /// </summary> 
    public class FileTypeDef {
        #region Declarations and Load
        //
        // note: could use LINQ here instead...
        //public static Dictionary<String, long> FileExtDict;
        //public static Dictionary<String, long> FileTypeDict;
        //public static Dictionary<long, long> FileTypeIdDict;
        //public static Dictionary<long, long> FileSubTypeIdDict;
        // note: LINQ design pattern
        public static IEnumerable<FileTypeItemDef> FileTypeItemsQuery;
        //
        public static Dictionary<long, FileTypeItemDef> FileTypeItems;
        //
        /// <summary> 
        /// Add a completely defined type using the passed fields.
        /// </summary> 
        /// <param name="ItemIdPassed">The id of the file type definition.</param>  
        /// <param name="FileExtPassed">Extesions served by this type.</param>  
        /// <param name="FileLevelPassed">Abstraction level of file (i.e. Data, Dict, Domain)</param> 
        /// <param name="FileTypeNamePassed">The name of the major file type.</param> 
        /// <param name="FileTypeIdPassed">The general or major file type.</param> 
        /// <param name="FileSubTypeNamePassed">Text name of the file sub-type.</param> 
        /// <param name="FileSubTypeIdPassed">The specific fil sub-type.</param> 
        /// <param name="DescriptionPassed">Description for this file type definition.</param> 
        /// <param name="FileReadModePassed">The file IO read mode to use with this type.</param> 
        /// <param name="IsDefaultPassed">Are defaults taken from the passed information.</param> 
        /// <remarks>
        /// </remarks> 
        public static void FileTypeItemsAdd(
            long ItemIdPassed, String FileExtPassed, long FileLevelPassed,
            String FileTypeNamePassed, long FileTypeIdPassed,
            String FileSubTypeNamePassed, long FileSubTypeIdPassed,
            String DescriptionPassed, long FileReadModePassed,
            bool IsDefaultPassed
            ) {
            FileTypeItems.Add(
                ItemIdPassed,
                new FileTypeItemDef(
            ItemIdPassed, FileExtPassed, FileLevelPassed,
            FileTypeNamePassed, FileTypeIdPassed,
            FileSubTypeNamePassed, FileSubTypeIdPassed,
            DescriptionPassed, FileReadModePassed,
            IsDefaultPassed
                    ));
            // don't want exceptions and try's here...
            ////FileExtDict.Add(FileExtPassed, ItemIdPassed);
            ////FileTypeDict.Add(FileTypeNamePassed, ItemIdPassed);
            ////FileTypeIdDict.Add(FileTypeIdPassed, ItemIdPassed);
            ////FileSubTypeIdDict.Add(FileSubTypeIdPassed, ItemIdPassed);
            ////
            //FileExtDict[FileExtPassed] = ItemIdPassed;
            //FileTypeDict[FileTypeNamePassed] = ItemIdPassed;
            //FileTypeIdDict[FileTypeIdPassed] = ItemIdPassed;
            //FileSubTypeIdDict[FileSubTypeIdPassed] = ItemIdPassed;
        }

        /// <summary> 
        /// Build the file types table using the system defined
        /// core group of supported file types.
        /// Clears the list as well so provides a basic list that
        /// specific applicaitons might add to or remove from.
        /// </summary> 
        public static void FileTypeItemsBuild() {
            FileTypeItems = new Dictionary<long, FileTypeItemDef>();
            //
            //FileExtDict = new Dictionary<string, long>();
            //FileTypeDict = new Dictionary<string, long>();
            //FileTypeIdDict = new Dictionary<long, long>();
            //FileSubTypeIdDict = new Dictionary<long, long>();
            //
            FileTypeItemsAdd(
                701, "tld", (long)FileType_LevelIs.Data,
                "Tilde Text", (long)FileType_Is.Tilde,
                "Tilde Std", (long)FileType_SubTypeIs.Tilde,
                "Tilde delimited data file", (long)FileIo_ModeIs.All, false);
            FileTypeItemsAdd(
                751, "tlddict", (long)FileType_LevelIs.DictData,
                "Tilde Text Dict", (long)FileType_Is.Tilde,
                "Tilde Dict Std", (long)FileType_SubTypeIs.Tilde,
                "Tilde delimited dictionary file", (long)FileIo_ModeIs.All, false);
            FileTypeItemsAdd(
                2, "txt", (long)FileType_LevelIs.Data,
                "Text", (long)FileType_Is.TEXT,
                "Text", (long)FileType_SubTypeIs.TEXT,
                "Text File", (long)FileIo_ModeIs.All, false);
            FileTypeItemsAdd(
                3, "csv", (long)FileType_LevelIs.Data,
                "Text Csv", (long)FileType_Is.TEXT,
                "Csv", (long)FileType_SubTypeIs.CSV,
                "Comma delimited text file", (long)FileIo_ModeIs.All, false);
            FileTypeItemsAdd(
                4, "mdf", (long)FileType_LevelIs.Data,
                "MsSql", (long)FileType_Is.SQL,
                "Microsoft Sql Server File", (long)FileType_SubTypeIs.MS,
                "MS SQL data file", (long)FileIo_ModeIs.Sql, true);
            FileTypeItemsAdd(
                5, "xxx", (long)FileType_LevelIs.Data,
                "MySql", (long)FileType_Is.SQL,
                "Oracle MY SQL File", (long)FileType_SubTypeIs.MY,
                "My Sql data file", (long)FileIo_ModeIs.Sql, false);
            FileTypeItemsAdd(
                1001, "ItemList", (long)FileType_LevelIs.Data,
                "System", (long)FileType_Is.SystemList,
                "ItemList", (long)FileType_SubTypeIs.MY,
                "System standard Text Cr Lf delimited list", (long)FileIo_ModeIs.All, false);
            FileTypeItemsAdd(
                801, "png", (long)FileType_LevelIs.Data,
                "Binary", (long)FileType_Is.Binary,
                "Image", (long)FileType_SubTypeIs.Binary,
                "PNG Image File", (long)FileIo_ModeIs.Binary, false);
        }

        public FileTypeDef() { }
        #endregion

        #region Lookup based on Ext, Type Name, Type, SubType
        /// <summary> 
        /// Property lookup and default (uses LINQ).
        /// </summary> 
        public static FileTypeItemDef FileTypeGetDefault() {
            if (FileTypeItems == null) { FileTypeItemsBuild(); }
            FileTypeItemsQuery = FileTypeItems.Values.Where(
                ItemFound => ItemFound.IsDefault == true);
            return FileTypeItemsQuery.FirstOrDefault();
        }

        /// <summary> 
        /// Property lookup and default (uses LINQ).
        /// </summary> 
        public static FileTypeItemDef FileTypeGetExt(String FileExtPassed) {
            if (FileTypeItems == null) { FileTypeItemsBuild(); }
            FileTypeItemsQuery = FileTypeItems.Values.Where(
                ItemFound => ItemFound.FileExt == FileExtPassed.ToLower());
            return FileTypeItemsQuery.FirstOrDefault();
        }

        /// <summary> 
        /// Property lookup and default (uses LINQ).
        /// </summary> 
        public static FileTypeItemDef FileTypeGetType(String FileTypePassed) {
            if (FileTypeItems == null) { FileTypeItemsBuild(); }
            FileTypeItemsQuery = FileTypeItems.Values.Where(
                ItemFound => ItemFound.FileTypeName == FileTypePassed);
            return FileTypeItemsQuery.FirstOrDefault();
        }

        /// <summary> 
        /// Property lookup and default (uses LINQ).
        /// </summary> 
        public static FileTypeItemDef FileTypeGetTypeId(long FileTypeIdPassed) {
            if (FileTypeItems == null) { FileTypeItemsBuild(); }
            FileTypeItemsQuery = FileTypeItems.Values.Where(
                ItemFound => ItemFound.FileTypeId == FileTypeIdPassed);
            return FileTypeItemsQuery.FirstOrDefault();
        }

        /// <summary> 
        /// Property lookup and default (uses LINQ).
        /// </summary> 
        public static FileTypeItemDef FileTypeGetSubTypeId(long FileSubTypeIdPassed) {
            if (FileTypeItems == null) { FileTypeItemsBuild(); }
            FileTypeItemsQuery = FileTypeItems.Values.Where(
                ItemFound => ItemFound.FileSubTypeId == FileSubTypeIdPassed);
            return FileTypeItemsQuery.FirstOrDefault();
        }
        #endregion

        #region Return File Types
        public static long FileTypeMetaLevelGet(long PassedFileTypeId) { return (0x0000000F & PassedFileTypeId); }
        public static long FileTypeMajorGet(long PassedFileTypeId) { return (0xFFFF0000 & PassedFileTypeId); }
        public static long FileTypeMinorGet(long PassedFileTypeId) { return (0x0000FFFF & PassedFileTypeId); }
        public static long FileSubTypeMajorGet(long PassedFileSubTypeId) { return (0xFFFF0000 & PassedFileSubTypeId); }
        public static long FileSubTypeMinorGet(long PassedFileSubTypeId) { return (0x0000FFFF & PassedFileSubTypeId); }
        //
        public static long FileTypeTildeMinorGet(long PassedFileTypeId) { return (0x00000FFF & PassedFileTypeId); }
        //
        public static bool FileType_IsTilde(long PassedFileTypeId) {
            return ((
                (long)FileType_Is.MaskTilde
                & PassedFileTypeId) > 0);
        }
        public static bool FileType_IsMarkup(long PassedFileTypeId) {
            return ((
                (long)FileType_Is.MaskMarkup
                & PassedFileTypeId) > 0);
        }
        public static bool FileType_IsDiskFile(long PassedFileTypeId) {
            return ((
                ((long)FileType_Is.TEXT |
                (long)FileType_Is.MaskText)
                & PassedFileTypeId) > 0);
        }
        public static bool FileType_IsDatabase(long PassedFileTypeId) {
            return ((
                (long)FileType_Is.MaskDatabase
                & PassedFileTypeId) > 0);
        }
        public static bool FileType_IsSystem(long PassedFileTypeId) {
            return ((
                (long)FileType_Is.MaskSystem
                & PassedFileTypeId) > 0);
        }
        #endregion
    }

    /// <summary>
    /// <para> File Type Definition Item</para>
    /// <para> Defines a specific file type
    /// in the file system.  These are collected
    /// by the File Type class and used to validate
    /// file types and provide default information.</para>
    /// </summary>
    public class FileTypeItemDef {
        public long ItemId;
        public String FileExt;
        public long MetaLevelId; // dict / data
        public String FileTypeName;
        public long FileTypeId;
        public String FileSubTypeName;
        public long FileSubTypeId;
        public String Description;
        public long FileReadMode;
        public bool IsDefault;

        public FileTypeItemDef(
            long ItemIdPassed, String FileExtPassed, long MetaLevelIdPassed,
            String FileTypeNamePassed, long FileTypeIdPassed,
            String FileSubTypeNamePassed, long FileSubTypeIdPassed,
            String DescriptionPassed, long FileReadModePassed,
            bool IsDefaultPassed
            ) {
            MetaLevelId = MetaLevelIdPassed;
            ItemId = ItemIdPassed;
            FileExt = FileExtPassed;
            FileTypeName = FileTypeNamePassed;
            FileTypeId = FileTypeIdPassed;
            FileSubTypeName = FileSubTypeNamePassed;
            FileSubTypeId = FileSubTypeIdPassed;
            Description = DescriptionPassed;
            FileReadMode = FileReadModePassed;
            IsDefault = IsDefaultPassed;
        }
        public FileTypeItemDef() { }
    }
    #endregion
    #endregion

    #region File Core
    /// <summary>
    /// <para> File Summary Class</para>
    /// <para> This class contains all of the 
    /// text fields used to identify a file,
    /// its system, server, database, location
    /// on disk and related fields such as
    /// ownership and security.</para>
    /// <para> This object is intended to be
    /// passed between applications and has a
    /// relatively small footprint compared to
    /// the Mfile File Application Object or 
    /// either of the FileMain Stream Objects 
    /// Fmain and Faux.</para>
    /// <para> The class also includes instances
    /// of FileIo, FileId and FileOpt(ion) classes.
    /// These exist here to provide user interface
    /// access to completely identify a file object.</para>
    /// <para> . </para>
    /// <para> . </para>
    /// </summary>
    public class FileSummaryDef : PickSyntax, IDisposable {
        #region Constructors
        public FileSummaryDef() {
            FileSummaryDefInitialize((int)FileAction_DirectionIs.None);
        }
        public void FileSummaryDefInitialize(int DirectionPassed) {
            FsObject = this;
            FileId = new FileIdDef();
            FileIo = new FileIoDef();
            FileOpt = new FileOptionsDef();
            DbId = new DbIdDef();
            FileTypeItem = new FileTypeItemDef();
            DataClear();
            Direction = DirectionPassed;
        }
        public FileSummaryDef(Mfile MfilePassed, int DirectionPassed) {
            FileObject = MfilePassed;
            FileSummaryDefInitialize(DirectionPassed);
        }
        #endregion

        #region Fs Copy
        public void CopyTo(ref FileSummaryDef FsPassed) {
            DoingCopy = true;
            FileId.CopyTo(ref FsPassed.FileId);
            #region SetInputItem
            FsPassed.Direction = Direction;
            // State Change
            FsPassed.spFileNameFullCurrent = spFileNameFullCurrent;
            FsPassed.spFileNameFullNext = spFileNameFullNext;
            FsPassed.FileNameFullDefault = FileNameFullDefault;
            FsPassed.FileNameFullOriginal = FileNameFullOriginal;
            //
            FsPassed.FileVersion = FileVersion;
            FsPassed.FileVersionDate = FileVersionDate;
            FsPassed.ItemWriteItBoolFlag = ItemWriteItBoolFlag;
            #endregion
            #region ItemType Set
            FsPassed.ipMetaLevelId = ipMetaLevelId;
            FsPassed.ipFileTypeId = ipFileTypeId;
            FsPassed.ipFileSubTypeId = ipFileSubTypeId;
            #endregion
            #region File Other Attributes
            FsPassed.spSystemName = spSystemName;
            FsPassed.spServerName = spServerName;
            FsPassed.spServiceName = spServiceName;
            FsPassed.spDatabaseName = spDatabaseName;
            // FsPassed.ConnDoReset = ConnDoReset; // Applies to DbIo not Fs
            FsPassed.spTableName = spTableName;
            FsPassed.spTableNameFull = spTableNameFull;
            FsPassed.spTableNameLine = spTableNameLine;
            //
            FsPassed.spFileOwnerName = spFileOwnerName;
            FsPassed.spFileGroupName = spFileGroupName;
            FsPassed.ipFileGroupId = ipFileGroupId;
            #endregion
            #region Options, Id, Master, Security and User
            FsPassed.spFileOptionString = spFileOptionString;
            //
            FsPassed.spItemIdCurrent = spItemIdCurrent;
            FsPassed.spItemIdNext = spItemIdNext;
            FsPassed.spItemIdPrev = spItemIdPrev;
            //
            FsPassed.MasterDatabaseLine = MasterDatabaseLine;
            FsPassed.MasterFileLine = MasterFileLine;
            FsPassed.MasterServerLine = MasterServerLine;
            FsPassed.MasterSystemLine = MasterSystemLine;
            //
            FsPassed.SecurityMasterDatabaseLine = SecurityMasterDatabaseLine;
            FsPassed.SecurityMasterFileLine = SecurityMasterFileLine;
            FsPassed.SecurityMasterServerLine = SecurityMasterServerLine;
            FsPassed.SecurityMasterSystemLine = SecurityMasterSystemLine;
            FsPassed.SecuritySqlAuth = SecuritySqlAuth;
            FsPassed.SecurityWindowsAuth = SecurityWindowsAuth;
            //
            FsPassed.UserNameLine = UserNameLine;
            FsPassed.UserPasswordLine = UserPasswordLine;
            FsPassed.UserPasswordRequiredOption = UserPasswordRequiredOption;
            #endregion
            DoingCopy = false;
        }
        public void CopyFrom(ref FileSummaryDef FsPassed) {
            DoingCopy = true;
            FileId.CopyTo(ref FileId);
            #region SetInputItem
            Direction = FsPassed.Direction;
            // State Change
            spFileNameFullCurrent = FsPassed.spFileNameFullCurrent;
            spFileNameFullNext = FsPassed.spFileNameFullNext;
            FileNameFullDefault = FsPassed.FileNameFullDefault;
            FileNameFullOriginal = FsPassed.FileNameFullOriginal;
            //
            FileVersion = FsPassed.FileVersion;
            FileVersionDate = FsPassed.FileVersionDate;
            ItemWriteItBoolFlag = FsPassed.ItemWriteItBoolFlag;
            #endregion
            #region ItemType Set
            ipMetaLevelId = FsPassed.ipMetaLevelId;
            ipFileTypeId = FsPassed.ipFileTypeId;
            ipFileSubTypeId = FsPassed.ipFileSubTypeId;
            #endregion
            #region File Other Attributes
            spSystemName = FsPassed.spSystemName;
            spServerName = FsPassed.spServerName;
            spServiceName = FsPassed.spServiceName;
            spDatabaseName = FsPassed.spDatabaseName;
            // ConnDoReset = FsPassed.ConnDoReset; // Applies to DbIo not Fs
            spTableName = FsPassed.spTableName;
            spTableNameFull = FsPassed.spTableNameFull;
            spTableNameLine = FsPassed.spTableNameLine;
            //
            spFileOwnerName = FsPassed.spFileOwnerName;
            spFileGroupName = FsPassed.spFileGroupName;
            ipFileGroupId = FsPassed.ipFileGroupId;
            #endregion
            #region Options, Id, Master, Security and User
            spFileOptionString = FsPassed.spFileOptionString;
            //
            spItemIdCurrent = FsPassed.spItemIdCurrent;
            spItemIdNext = FsPassed.spItemIdNext;
            spItemIdPrev = FsPassed.spItemIdPrev;
            //
            MasterDatabaseLine = FsPassed.MasterDatabaseLine;
            MasterFileLine = FsPassed.MasterFileLine;
            MasterServerLine = FsPassed.MasterServerLine;
            MasterSystemLine = FsPassed.MasterSystemLine;
            //
            SecurityMasterDatabaseLine = FsPassed.SecurityMasterDatabaseLine;
            SecurityMasterFileLine = FsPassed.SecurityMasterFileLine;
            SecurityMasterServerLine = FsPassed.SecurityMasterServerLine;
            SecurityMasterSystemLine = FsPassed.SecurityMasterSystemLine;
            SecuritySqlAuth = FsPassed.SecuritySqlAuth;
            SecurityWindowsAuth = FsPassed.SecurityWindowsAuth;
            //
            UserNameLine = FsPassed.UserNameLine;
            UserPasswordLine = FsPassed.UserPasswordLine;
            UserPasswordRequiredOption = FsPassed.UserPasswordRequiredOption;
            #endregion
            DoingCopy = false;
        }
        #endregion

        #region Fs Clear
        public void DataClear(Mfile MfilePassed, int DirectionPassed) {
            DataClear();
            FileObject = MfilePassed;
            Direction = DirectionPassed;
        }

        public void DataClear() {
            FileOpt.DataClear();
            FileId.DataClear();
            FileIo.DataClear();
            DbId.DataClear();
            #region FileName current values, Version, Flags
            FileId.FileName = "Unknown99";
            // Direction = (int)FileAction_DirectionIs.None;
            // State Change
            spFileNameFullCurrent = "";
            spFileNameFullNext = "";
            FileNameFullDefault = "";
            FileNameFullOriginal = "";
            //
            FileVersion = "";
            FileVersionDate = "";
            ItemWriteItBoolFlag = false;
            #endregion
            #region File Type Set
            ipMetaLevelId = (long)FileType_LevelIs.None;
            ipFileTypeId = (long)FileType_Is.None;
            ipFileSubTypeId = (long)FileType_SubTypeIs.None;
            #endregion
            #region File System Service, Server, Database, Table, Owner, Group
            spSystemName = "";
            if (SystemObject != null) { SystemObject = null; } // Dispose
            SystemObject = new object(); // not a proper object yet.
            spServerName = "";
            if (ServerObject != null) { ServerObject = null; } // Dispose
            ServerObject = new object(); // not a proper object yet.
            spServiceName = "";
            //
            spDatabaseName = "";
            ConnDoReset = true;
            DoingCopy = false;
            spTableName = "";
            spTableNameLine = "";
            spTableNameFull = "";
            //
            spFileOwnerName = "";
            spFileGroupName = "";
            ipFileGroupId = 0;
            #endregion
            #region Options, Id, Master, Security and User
            spFileOptionString = "";
            //
            spItemIdCurrent = "";
            spItemIdNext = "";
            spItemIdPrev = "";
            //
            MasterDatabaseLine = "";
            MasterFileLine = "";
            MasterServerLine = "";
            MasterSystemLine = "";
            //
            SecurityMasterDatabaseLine = "";
            SecurityMasterFileLine = "";
            SecurityMasterServerLine = "";
            SecurityMasterSystemLine = "";
            SecuritySqlAuth = false;
            SecurityWindowsAuth = false;
            //
            UserNameLine = "";
            UserPasswordLine = "";
            UserPasswordRequiredOption = false;
            #endregion
        }
        #endregion

        #region Destructors
        // Track whether Dispose has been called.
        private bool disposed = false;
        private bool instantiated = false;

        ~FileSummaryDef() {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
        }

        public void Dispose(bool disposing) {
            // If disposing equals true, dispose all managed
            // and unmanaged resources
            if (disposing) {
                //if (components != null) {
                //    components.Dispose();
                //}
                ConnDoReset = true;
                //FileId.Dispose(disposing);
                FileIo.Dispose(disposing);
                //FileOpt.Dispose(disposing);
                //DbId.Dispose(disposing);
            }
            // Call the appropriate methods to clean up
            // unmanaged resources here.
            // base.Dispose(disposing);
            disposed = true;
        }
        #endregion

        #region Name Get
        public String FileNameGet(ref FileSummaryDef FsPassed) {
            if ((FsPassed.FileTypeId & (long)FileType_Is.MaskDatabase) > 0) {
                return FsPassed.TableNameFull;
            } else {
                return FsPassed.FileId.FileNameFull;
            }
        }

        public String FileNameLineGet(ref FileSummaryDef FsPassed) {
            if ((FsPassed.FileTypeId & (long)FileType_Is.MaskDatabase) > 0) {
                return FsPassed.TableNameLine;
            } else {
                return FsPassed.FileId.FileNameLine;
            }
        }
        #endregion

        #region Parsing
        public String TableNameLineBuild(ref FileMainDef FmainPassed) {
            FmainPassed.Fs.TableNameLine = "";
            // ?? System ?? Server ??
            // DatabaseName
            FmainPassed.Fs.TableNameLine += TableNameFullBuild(ref FmainPassed);
            //
            return FmainPassed.Fs.TableNameLine;
        }

        public String TableNameFullBuild(ref FileMainDef FmainPassed) {
            FmainPassed.Fs.TableNameFull = "";
            // DatabaseName
            FmainPassed.Fs.TableNameFull += FmainPassed.Fs.spDatabaseName;
            FmainPassed.Fs.TableNameFull += ".";
            // FileOwnerName
            FmainPassed.Fs.TableNameFull += FmainPassed.Fs.spFileOwnerName;
            FmainPassed.Fs.TableNameFull += ".";
            // FileName
            FmainPassed.Fs.TableNameFull += FmainPassed.Fs.spTableName;
            //
            return FmainPassed.Fs.TableNameFull;
        }

        public void TableNameSetFromLine(String TableNameLinePassed) {
            String[] TempStringArr = new String[2];
            String TableNameLineTemp;
            String TableNameLineTemp1;
            if (TableNameLinePassed == null) {
                TableNameLineTemp = TableNameLine;
            } else {
                TableNameLineTemp = TableNameLinePassed;
            }
            if (TableNameLineTemp.Length == 0) { return; }
            //
            TableNameFullClear(false);
            TableNameLineTemp.Trim();
            TableNameLine = TableNameLineTemp;
            int DotCount = TableNameLine.Count(Dot);
            if (DotCount > 0) {
                TableNameLineTemp1 = TableNameLineTemp.Field(Dot, 3);
                if (DotCount >= 2) {
                    spTableName = TableNameLineTemp1;
                    spFileOwnerName = TableNameLineTemp.Field(Dot, 2);
                    spDatabaseName = TableNameLineTemp.Field(Dot, 1);
                    ConnDoReset = true;
                } else if (DotCount == 1) {
                    TableNameLineTemp1 = TableNameLineTemp.Field(Dot, 2);
                    if (TableNameLineTemp1.Length > 0) {
                        spTableName = TableNameLineTemp1;
                        spFileOwnerName = TableNameLineTemp.Field(Dot, 1);
                    } else {
                        spTableName = TableNameLineTemp1;
                    }
                }
                if (spDatabaseName.Contains("[")) {
                    spDatabaseName = spDatabaseName.Field("[", 2);
                    spDatabaseName = spDatabaseName.Field("]", 2);
                }
                if (spFileOwnerName.Contains("[")) {
                    spFileOwnerName = spFileOwnerName.Field("[", 2);
                    spFileOwnerName = spFileOwnerName.Field("]", 2);
                }
            } else if (TableNameLineTemp.Contains(BackSlash)) {
                spTableName = TableNameLineTemp;
            } else {
                spTableName = TableNameLineTemp;
            }
            if (spTableName.Contains("[")) {
                spTableName = spTableName.Field("[", 2);
                spTableName = spTableName.Field("]", 2);
            }
        }

        public void TableNameFullClear(bool DoClearLinePassed) {
            if (DoClearLinePassed) { TableNameLine = ""; }
            //spDatabaseName = "";
            //spFileOwnerName = "";
            TableName = "";
            TableNameFull = "";
        }

        #endregion

        #region Declarations
        FileSummaryDef FsObject;
        // public Object File;
        public Mfile FileObject;
        //
        public String FmainStreamType;
        //
        public FileOptionsDef FileOpt;
        // File
        #region File Type Information
        public FileTypeItemDef FileTypeItem;
        #region Type Fields
        #region MetaLevel
        public String spMetaLevel;
        public String MetaLevel {
            get { return spMetaLevel; }
            set { spMetaLevel = value; }
        }
        public long ipMetaLevelId;
        public long MetaLevelId {
            get { return ipMetaLevelId; }
            set {
                if (!Enum.IsDefined(typeof(FileType_LevelIs), value)) {
                    value = FileTypeDef.FileTypeMetaLevelGet(value);
                }
                if (Enum.IsDefined(typeof(FileType_LevelIs), value)) {
                    ipMetaLevelId = value;
                    spMetaLevel = Enum.GetName(typeof(FileType_LevelIs), ipMetaLevelId);
                } else { spMetaLevel = "Meta Level is not valid."; }
            }
        }
        #endregion
        #region FileTypeName
        private bool DoingFileTypeId = false;
        private bool DoingFileType = false;
        //
        public String spFileType;
        public String FileType {
            get { return spFileType; }
            set {
                DoingFileType = true;
                try {
                    if (!DoingFileTypeId) {
                        FileTypeId = (long)Enum.Parse(typeof(FileType_Is), value);
                    }
                    spFileType = value;
                } catch { }
                DoingFileType = false;
            }
        }
        public long ipFileTypeId;
        public long FileTypeId {
            get { return ipFileTypeId; }
            set {
                DoingFileTypeId = true;
                try {
                    if (!DoingFileType) {
                        FileType = Enum.GetName(typeof(FileType_Is), value);
                    }
                    ipFileTypeId = value;
                    // MetaLevelId = FileTypeDef.FileTypeMetaLevelGet(ipFileTypeId);
                    FileTypeMajorId = FileTypeDef.FileTypeMajorGet(ipFileTypeId);
                    FileTypeMinorId = FileTypeDef.FileTypeMinorGet(ipFileTypeId);
                } catch { }
                DoingFileTypeId = false;
            }
        }
        public long FileTypeMajorId;
        public long FileTypeMinorId;
        #endregion
        #region FileSubTypeName
        private bool DoingFileSubTypeId = false;
        private bool DoingFileSubType = false;
        //
        public String spFileSubType;
        public String FileSubType {
            get { return spFileSubType; }
            set {
                DoingFileSubType = true;
                try {
                    if (!DoingFileSubTypeId) {
                        FileSubTypeId = (long)Enum.Parse(typeof(FileType_SubTypeIs), value);
                    }
                    spFileSubType = value;
                } catch { }
                DoingFileSubType = false;
            }
        }
        public long ipFileSubTypeId;
        public long FileSubTypeId {
            get { return ipFileSubTypeId; }
            set {
                DoingFileSubTypeId = true;
                try {
                    if (!DoingFileSubType) {
                        FileSubType = Enum.GetName(typeof(FileType_SubTypeIs), value);
                    }
                    ipFileSubTypeId = value;
                    // MetaLevelId = FileSubTypeDef.FileSubTypeMetaLevelGet(ipFileSubTypeId);
                    FileSubTypeMajorId = FileTypeDef.FileSubTypeMajorGet(ipFileSubTypeId);
                    FileSubTypeMinorId = FileTypeDef.FileSubTypeMinorGet(ipFileSubTypeId);
                } catch { }
                DoingFileSubTypeId = false;
            }
        }
        public long FileSubTypeMajorId;
        public long FileSubTypeMinorId;
        #endregion
        #endregion
        #region Type Set From
        public long FileTypeSetFromLine(String FileNameLinePassed) {
            long FileTypeSetResult = (long)StateIs.Started;
            // Extension analysis
            String FileExtPassed;
            if (FileNameLinePassed == null) { FileNameLinePassed = FileId.FileNameLine; }
            if (FileNameLinePassed.Length == 0) { FileNameLinePassed = FileId.FileNameLine; }
            if (FileNameLinePassed.Contains(Dot)) {
                FileExtPassed = FileNameLinePassed.FieldLast(Dot);
            } else { FileExtPassed = ""; }
            FileTypeSetResult = FileTypeSetFromExt(FileExtPassed);
            return FileTypeSetResult;
        }
        public long FileTypeSetFromExt(String FileExtPassed) {
            long FileTypeSetResult = (long)StateIs.Started;
            if (FileExtPassed == null) { FileExtPassed = FileId.FileExt; }
            if (FileExtPassed.Length == 0) { FileExtPassed = FileId.FileExt; }
            if (FileExtPassed.Contains(Dot)) { FileExtPassed = FileExtPassed.FieldLast(Dot); }
            //
            FileTypeItem = null;
            FileTypeSetResult = (long)StateIs.DoesExist;
            if (FileExtPassed.Length == 0) {
                // No extension is SQL (MS) format
                FileTypeItem = FileTypeDef.FileTypeGetTypeId((long)FileType_Is.SQL);
            } else {
                FileTypeItem = FileTypeDef.FileTypeGetExt(FileExtPassed);
                if (FileTypeItem == null) {
                    FileTypeItem = FileTypeDef.FileTypeGetDefault();
                    FileTypeSetResult = (long)StateIs.DoesNotExist;
                }
            }
            //
            if (FileTypeItem != null) {
                MetaLevelId = FileTypeItem.MetaLevelId;
                FileTypeId = FileTypeItem.FileTypeId;
                // FileTypeName = FileTypeItem.FileTypeName;
                FileSubTypeId = FileTypeItem.FileSubTypeId;
                // FileSubTypeName = FileTypeItem.FileSubTypeName;
                FileIo.FileReadMode = FileTypeItem.FileReadMode;
            } else { FileTypeSetResult = (long)StateIs.DoesNotExist; }
            return FileTypeSetResult;
        }
        #endregion
        #endregion

        #region File Name Control
        //
        public String spFileNameFullCurrent;
        public String FileNameFullCurrent {
            get { return spFileNameFullCurrent; }
            set { spFileNameFullCurrent = value; }
        }
        //
        public String spFileNameFullNext;
        public String FileNameFullNext {
            get { return spFileNameFullNext; }
            set { spFileNameFullNext = value; }
        }
        public String FileNameFullDefault;
        public String FileNameFullOriginal;
        #endregion
        #region FileId and Version
        // File Record Version
        public String FileVersion;
        public String FileVersionDate;
        //public int ipFileId;
        //public int FileId {
        //    get { return ipFileId; }
        //    set { ipFileId = value; }
        //}
        public FileIdDef FileId;
        public FileIoDef FileIo;
        #endregion
        #region FileId and Version
        public DbIdDef DbId;
        #endregion
        #region ItemIdCurrent
        public String spItemIdCurrent;
        public String ItemIdCurrent {
            get { return spItemIdCurrent; }
            set { spItemIdCurrent = value; }
        }
        // ItemId
        public String spItemIdNext;
        public String ItemIdNext {
            get { return spItemIdNext; }
            set { spItemIdNext = value; }
        }

        // ItemId
        public String spItemIdPrev;
        public String ItemIdPrev {
            get { return spItemIdPrev; }
            set { spItemIdPrev = value; }
        }
        #endregion
        #region Dir, System, Server... database names
        protected internal String spDirectionName;
        public String DirectionName {
            get { return spDirectionName; }
            set { spDirectionName = value; }
        }
        // direction
        protected internal int ipDirection;
        public int Direction {
            get { return ipDirection; }
            set {
                if (Enum.IsDefined(typeof(FileAction_DirectionIs), value)) {
                    ipDirection = value;
                    spDirectionName = Enum.GetName(typeof(FileAction_DirectionIs), ipDirection);
                }
            }
        }
        // System
        public String spSystemName;
        public String SystemName {
            get { return spSystemName; }
            set { spSystemName = value; }
        }
        public Object SystemObject;
        //
        // Server
        public String spServerName;
        public String ServerName {
            get { return spServerName; }
            set {
                if (spServerName != value && spServerName.Length > 0) { ConnDoReset = true; }
                spServerName = value;
            }
        }
        public Object ServerObject;
        //
        // Service
        public String spServiceName;
        public String ServiceName {
            get { return spServiceName; }
            set { spServiceName = value; }
        }
        public Object ServiceObject;
        //
        // Database
        public String spDatabaseName;
        public String DatabaseName {
            get { return spDatabaseName; }
            set {
                if (spDatabaseName != value && spDatabaseName.Length > 0) { ConnDoReset = true; }
                spDatabaseName = value;
            }
        }
        public bool ConnDoReset;
        public bool DoingCopy;
        //
        // Table
        public String spTableName;
        public String TableName {
            get { return spTableName; }
            set { spTableName = value; }
        }
        // Table ** not used **
        public String spTableNameFull;
        public String TableNameFull {
            get { return spTableNameFull; }
            set { spTableNameFull = value; }
        }
        // Table Name Line
        protected internal String spTableNameLine;
        public String TableNameLine {
            get { return spTableNameLine; }
            set { spTableNameLine = value; }
        }
        //
        // FileGroup
        public String spFileGroupName;
        public String FileGroupName {
            get { return spFileGroupName; }
            set { spFileGroupName = value; }
        }
        // FileGroupId
        public int ipFileGroupId;
        public int FileGroupId {
            get { return ipFileGroupId; }
            set { ipFileGroupId = value; }
        }
        //
        // Owner
        protected internal String spFileOwnerName;
        public String FileOwnerName {
            get { return spFileOwnerName; }
            set { spFileOwnerName = value; }
        }
        #endregion
        //
        #region FileOptionsDef
        public String spFileOptionString;
        public String FileOptionString {
            get { return spFileOptionString; }
            set { spFileOptionString = value; }
        }
        // FileWrite
        public bool ItemWriteItBoolFlag;
        #endregion
        #region Database Master
        // File Command Lines
        public String MasterSystemLine;
        public String MasterServerLine;
        public String MasterDatabaseLine;
        public String MasterFileLine;
        #endregion
        #region User Command Lines
        public String UserNameLine;
        public String UserPasswordLine;
        public bool UserPasswordRequiredOption;
        #endregion
        #region Security Authorization
        public bool SecurityWindowsAuth;
        public bool SecuritySqlAuth;
        // Security Lines
        public String SecurityMasterSystemLine;
        public String SecurityMasterServerLine;
        public String SecurityMasterDatabaseLine;
        public String SecurityMasterFileLine;
        #endregion
        //
        #endregion

        public override String ToString() {
            if (FileId.FileName != null && DirectionName != null) {
                String sTemp = "File Summary: ";
                try {
                    sTemp += "Level: " + Enum.GetName(typeof(FileType_LevelIs), MetaLevelId);
                    sTemp += ", Type: " + Enum.GetName(typeof(FileType_Is), FileTypeId);
                    sTemp += ", SubType: " + Enum.GetName(typeof(FileType_SubTypeIs), FileSubTypeId);
                    sTemp += ". ";
                } catch { sTemp += "Type information not available. "; }
                if (SystemName.Length > 0) { sTemp += "System: " + SystemName + ". "; }
                if (ServiceName.Length > 0) { sTemp += "Service: " + ServiceName + ". "; }
                if (ServerName.Length > 0) { sTemp += "Server: " + ServerName + ". "; }
                if (DatabaseName.Length > 0) { sTemp += "Database: " + DatabaseName + ". "; }
                sTemp += "File: ";
                if ((FileTypeId & (long)FileType_Is.MaskDatabase) > 0) {
                    sTemp += spTableName;
                } else {
                    if (FileId.FileNameLine.Length > 0) {
                        sTemp += FileId.FileNameLine;
                    } else { sTemp += FileId.FileName; }
                }
                sTemp += " for " + DirectionName + ".";
                if (ItemIdCurrent.Length > 0) { sTemp += "Item Id: " + ItemIdCurrent + "."; }
                return sTemp;
            } else { return base.ToString(); }
            return "";
        }
    }

    /// <summary>
    /// <para> File Identification Class</para>
    /// <para> File Name, Path, Drive, etc. </para>
    /// <para> This is used to identify the file that
    /// will be access and to store the disk file name
    /// for database files.</para>
    /// </summary>
    /// <remarks>
    /// <para> The two main types of objects are File and Db (Database).</para>
    /// <para> Each of these may have:</para>
    /// <para> 1) A basic file object (Fmain).</para>
    /// <para> 2) A File Summary Object. </para>
    /// <para> 3) An ID object for identity. </para>
    /// <para> 4) An IO object used readers and writers.</para>
    /// <para> 5) One or more File Status Objects.</para>
    /// <para> 6) One of serveral column and attribute control classes.</para>
    /// </remarks>
    public class FileIdDef : PickSyntax {
        #region File Core Properties
        protected internal String spFileNameLine;
        public String FileNameLine {
            get { return spFileNameLine; }
            set { spFileNameLine = value; }
        }

        protected internal String spFileNameLineCurrent;
        public String FileNameLineCurrent {
            get { return spFileNameLineCurrent; }
            set { spFileNameLineCurrent = value; }
        }

        protected internal String spFileNameLineOriginal;
        public String FileNameLineOriginal {
            get { return spFileNameLineOriginal; }
            set { spFileNameLineOriginal = value; }
        }

        public String spFileNameFull;
        public String FileNameFull {
            get { return spFileNameFull; }
            set { spFileNameFull = value; }
        }
        // FileId
        public int ipFileId;
        public int FileId {
            get { return ipFileId; }
            set { ipFileId = value; }
        }
        // <Area Id = "FileName">
        protected internal String spFileName;
        public String FileName {
            get { return spFileName; }
            set { spFileName = value; }
        }
        protected internal String spFileExt;
        public String FileExt {
            get { return spFileExt; }
            set { spFileExt = value; }
        }
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile FileMeta_Type_Details
        #region FileTypeDetails
        // <Area Id = "FileNameAlias">
        protected internal String spFileNameAlias;
        public String FileNameAlias {
            get { return spFileNameAlias; }
            set { spFileNameAlias = value; }
        }
        //
        protected internal String spFileShortName;
        public String FileShortName {
            get { return spFileShortName; }
            set { spFileShortName = value; }
        }
        //
        protected internal String spFileShort83Name;
        public String FileShort83Name {
            get { return spFileShort83Name; }
            set { spFileShort83Name = value; }
        }
        //
        protected internal String spFileShortUnixName;
        public String FileShortUnixName {
            get { return spFileShortUnixName; }
            set { spFileShortUnixName = value; }
        }
        // <Area Id = "FileGuid">
        protected internal Guid gpFileNameGuid;
        public Guid FileNameGuid {
            get { return gpFileNameGuid; }
            set { gpFileNameGuid = value; }
        }
        // <Area Id = "FileGuid">
        protected internal String spPropSystemPath;
        public String PropSystemPath {
            get { return spPropSystemPath; }
            set { spPropSystemPath = value; }
        }
        // File Status ////////////////////
        #endregion
        #region File Path and Location Info
        // <Area Id = "SourceDriveSystem - PhysicalLocation">
        // System Id (System Name) is equivalent to the NTFS Computer
        // System Name is stored in Fs along with owner.
        protected internal int ipFileDriveSystemId = -99999;
        public int FileDriveSystemId {
            get { return ipFileDriveSystemId; }
            set { ipFileDriveSystemId = value; }
        }

        // <Area Id = "SourceDriveName - PhysicalLocation">
        // This is the mapped network drive name
        protected internal String spFileDriveName;
        public String FileDriveName {
            get { return spFileDriveName; }
            set { spFileDriveName = value; }
        }

        protected internal String spFileDriveLetter;
        public String FileDriveLetter {
            get { return spFileDriveLetter; }
            set { spFileDriveLetter = value; }
        }

        // Letter Alias is the letter to be assigned when
        // mapping network drives from other systems.
        protected internal String spFileDriveLetterMapAlias;
        public String FileDriveLetterMapAlias {
            get { return spFileDriveLetterMapAlias; }
            set { spFileDriveLetterMapAlias = value; }
        }

        // The short name is used by the property management system.
        protected internal String spFileDriveShortName;
        public String FileDriveShortName {
            get { return spFileDriveShortName; }
            set { spFileDriveShortName = value; }
        }

        protected internal String spFileDriveDriveLabel;
        public String FileDriveDriveLabel {
            get { return spFileDriveDriveLabel; }
            set { spFileDriveDriveLabel = value; }
        }

        // <Area Id = "SourcePathName">
        protected internal String spPathName;
        public String PathName {
            get { return spPathName; }
            set { spPathName = value; }
        }

        // Path Alias is the label to be assigned when
        // mapping network drives from other systems.
        // Example: Share\Music\Mp3 might be Mp3Music
        protected internal String spPathNameMapAlias;
        public String PathNameMapAlias {
            get { return spPathNameMapAlias; }
            set { spPathNameMapAlias = value; }
        }

        protected internal int ipPathId = -99999;
        public int PathId {
            get { return ipPathId; }
            set { ipPathId = value; }
        }

        // The short name is used by the property management system.
        protected internal String spPathShortName;
        public String PathShortName {
            get { return spPathShortName; }
            set { spPathShortName = value; }
        }
        #endregion
        #endregion

        #region Clear Fields
        public void DataClear() {
            FileId = 0;
            //
            FileNameFullClear(true);
            //
            FileDriveLetterMapAlias = "";
            FileDriveShortName = "";
            FileDriveSystemId = 0;
            //
            PathShortName = "";
            //
            FileNameAlias = "";
            FileShortName = "";
            FileShort83Name = "";
            FileShortUnixName = "";
            //
            gpFileNameGuid = new Guid();
        }

        public void FileNameFullClear(bool DoClearLinePassed) {
            if (DoClearLinePassed) { FileNameLine = ""; }
            FileDriveDriveLabel = "";
            FileDriveLetter = "";
            PathName = "";
            FileName = "";
            FileNameFull = "";
            FileExt = "";
        }
        #endregion

        #region Copy Fields
        public void CopyTo(ref FileIdDef FileIdPassed) {
            FileIdPassed.FileId = FileId;
            //
            FileNameFullCopyTo(ref FileIdPassed);
            //
            FileIdPassed.FileDriveLetterMapAlias = FileDriveLetterMapAlias;
            FileIdPassed.FileDriveShortName = FileDriveShortName;
            FileIdPassed.FileDriveSystemId = FileDriveSystemId;
            //
            FileIdPassed.PathShortName = PathShortName;
            //
            FileIdPassed.FileNameAlias = FileNameAlias;
            FileIdPassed.FileShortName = FileShortName;
            FileIdPassed.FileShort83Name = FileShort83Name;
            FileIdPassed.FileShortUnixName = FileShortUnixName;
            //
            FileIdPassed.gpFileNameGuid = gpFileNameGuid;
        }

        public void FileNameFullCopyTo(ref FileIdDef FileIdPassed) {
            FileIdPassed.FileNameLine = FileNameLine;
            FileIdPassed.FileDriveDriveLabel = FileDriveDriveLabel;
            FileIdPassed.FileDriveLetter = FileDriveLetter;
            FileIdPassed.PathName = PathName;
            FileIdPassed.FileName = FileName;
            FileIdPassed.FileNameFull = FileNameFull;
            FileIdPassed.FileExt = FileExt;
        }
        #endregion

        public void FileNameSetFromLine(String FileNameLinePassed) {
            String[] TempStringArr = new String[2];
            ////String TempString = "";
            ////TempString = FileNameLinePassed.Field(BackSlash, 0);
            ////TempString = FileNameLinePassed.Field(BackSlash, 1);
            ////TempString = FileNameLinePassed.Field(BackSlash, 2);
            ////TempString = FileNameLinePassed.Field(BackSlash, 3);
            ////TempString = FileNameLinePassed.Field(BackSlash, 4);
            ////TempString = FileNameLinePassed.FieldLast(BackSlash);
            ////TempStringArr = FileNameLinePassed.SplitInTwo(BackSlash, 0);
            ////TempStringArr = FileNameLinePassed.SplitInTwo(BackSlash, 1);
            ////TempStringArr = FileNameLinePassed.SplitInTwo(BackSlash, 2);
            ////TempStringArr = FileNameLinePassed.SplitInTwo(BackSlash, 3);
            ////TempStringArr = FileNameLinePassed.SplitLast(BackSlash);
            String FileNameLineTemp;
            if (FileNameLinePassed == null) {
                FileNameLineTemp = FileNameLine;
            } else {
                FileNameLineTemp = FileNameLinePassed;
            }
            if (FileNameLineTemp.Length == 0) { return; }
            //
            FileNameFullClear(false);
            FileNameLineTemp.Trim();
            FileNameLine = FileNameLineTemp;
            if (FileNameLineTemp.Contains(Collon) && FileNameLineTemp.Substring(1, 1) == Collon) {
                TempStringArr = FileNameLineTemp.SplitInTwo(Collon, 1);
                FileDriveLetter = TempStringArr[0];
                FileNameLineTemp = TempStringArr[1];
            } else if (FileNameLineTemp.Contains(BackSlash)) {
                TempStringArr = FileNameLineTemp.SplitInTwo(BackSlash, 1);
                FileDriveLetterMapAlias = TempStringArr[0];
                FileNameLineTemp = TempStringArr[1];
            }
            if (FileNameLineTemp.Contains(BackSlash)) {
                TempStringArr = FileNameLineTemp.SplitLast(BackSlash);
                PathName = TempStringArr[0];
                FileNameFull = TempStringArr[1];
            } else { FileNameFull = FileNameLineTemp; }
            if (FileNameFull.Contains(Dot)) {
                TempStringArr = FileNameFull.SplitLast(Dot);
                FileName = TempStringArr[0];
                FileExt = TempStringArr[1];
            } else {
                FileName = FileNameFull;
                FileExt = "";
            }
        }

        public string FileNameLineBuild(ref FileMainDef FmainPassed) {
            FmainPassed.Fs.FileId.FileNameLine = "";
            if (FmainPassed.Fs.FileId.FileDriveLetter.Length > 0) {
                FmainPassed.Fs.FileId.FileNameLine +=
                    FmainPassed.Fs.FileId.FileDriveLetter
                    += ":" + BackSlash;
                // or Network Drive
            } else if (FmainPassed.Fs.FileId.FileDriveLetterMapAlias.Length > 0) {
                FmainPassed.Fs.FileId.FileNameLine += BackSlash
                    + FmainPassed.Fs.FileId.FileDriveLetterMapAlias;
                // System
            } else if (FmainPassed.Fs.SystemName.Length > 0) {
                FmainPassed.Fs.FileId.FileNameLine += BackSlash
                    + FmainPassed.Fs.SystemName;
            }
            // Path
            if (FmainPassed.Fs.FileId.PathName.Length > 0) {
                FmainPassed.Fs.FileId.FileNameLine += FmainPassed.Fs.FileId.PathName;
                if (FmainPassed.Fs.FileId.PathName.LastIndexOf(BackSlash) != FmainPassed.Fs.FileId.PathName.Length) {
                    FmainPassed.Fs.FileId.FileNameLine += BackSlash;
                }
            }
            // FileName & Extension
            if (FmainPassed.Fs.FileId.FileNameFull.Length > 0) {
                FmainPassed.Fs.FileId.FileNameLine += FmainPassed.Fs.FileId.FileNameFull;
            } else {
                if (FmainPassed.Fs.FileId.FileName.Length > 0) {
                    FmainPassed.Fs.FileId.FileNameLine += FmainPassed.Fs.FileId.FileName
                        + "." + FmainPassed.Fs.FileId.FileExt;
                }
            }
            return FmainPassed.Fs.FileId.FileNameLine;
        }
    }
    #endregion

    #region Mfile Readers, Writers, Database and File Objects and Buffers
    // Database Io
    /// <summary>
    /// <para> Database IO fields and objects including connection.</para>
    /// </summary>
    /// <remarks>
    /// <para> The two main types of objects are File and Db (Database).</para>
    /// <para> Each of these may have:</para>
    /// <para> 1) A basic file object (Fmain).</para>
    /// <para> 2) A File Summary Object. </para>
    /// <para> 3) An ID object for identity. </para>
    /// <para> 4) An IO object used readers and writers.</para>
    /// <para> 5) One or more File Status Objects.</para>
    /// <para> 6) One of serveral column and attribute control classes.</para>
    /// </remarks>
    public class DbIoDef : IDisposable {
        #region $include Mdm.Oss.FileUtil Mfile FileDbConnObjectConnection
        // <Area Id = "FileDbConnObject">
        // Database Connection
        // ofd  - 	Object - File - Database Connection
        public SqlConnection SqlDbConn = null;
        // ofde - 	Object - File - Database Connection - Error

        // ofdcd - 	Object - File - Database Connection - Delegate

        // ofdcv - 	Object - File - Database Connection - Event

        #endregion
        // SqlClient - SqlDataReader - xxxxxxxxxxxxxxxxxxxx
        // <Area Id = "ConnString">
        // property String ConnString
        protected internal String spConnString;
        public String ConnString {
            get { return spConnString; }
            set {
                spConnString = value;
            }
        } //   

        // Database Command - xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile FileDbConnObjectCommand
        // ofdc - 		Object - File - Database Command
        public SqlCommand SqlDbCommand = null;

        // Database Command - Timeout
        public int SqlDbCommandTimeout = 30;

        // Database Command - Error

        // Database Command - Delegate

        // File - Database Command - Event

        // File - Database Command - Adapter
        public SqlDataAdapter SqlDbAdapterObject = null;

        public String CommandCurrent = null;
        public String CommandPassed = null;
        #endregion
        // FILE SUBCLASS - FILE - READER
        // Database Sql ItemData Reader - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile FileObject SqlDataReader
        // ofddr - 		Object - File - Database Connection - DataReader
        public SqlDataReader SqlDbDataReader = null;
        public SqlDataAdapter SqlDbDataWriter = null;

        // ofddre - 	Object - File - Database Connection - DataReader- Error

        // ofddrcd - 	Object - File - Database Connection - DataReader- Delegate

        // ofddrcv - 	Object - File - Database Connection - DataReader- Event

        #endregion

        #region Destructors
        // Track whether Dispose has been called.
        private bool disposed = false;
        private bool instantiated = false;

        ~DbIoDef() {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
        }

        public void Dispose(bool disposing) {
            if (disposing) {
                //if (components != null) {
                //    components.Dispose();
                //}
            }
            DataClear();
            // base.Dispose(disposing);
            disposed = true;
        }
        #endregion

        public void DataClear() {
            CommandCurrent = "";
            CommandPassed = "";
            SqlDbCommandTimeout = 15;
            spConnString = "";
            //
            if (SqlDbConn != null) { SqlDbConn.Dispose(); }
            if (SqlDbCommand != null) { SqlDbCommand.Dispose(); }
            if (SqlDbAdapterObject != null) { SqlDbAdapterObject.Dispose(); }
            if (SqlDbDataReader != null) {
                if (!SqlDbDataReader.IsClosed) { SqlDbDataReader.Close(); }
                // SqlDbDataReader.Dispose();
            }
            if (SqlDbDataWriter != null) {
                // SqlDbDataWriter.Dispose(); 
            }
        }

        public DbIoDef() { DataClear(); }
    }
    /// <summary>
    /// <para> File IO fields and objects.</para>
    /// <para> . </para>
    /// <para> File Io is used for the types:</para>
    /// <para> Ascii, Text, Xml, Uml, Tld, Delimited and Feild Separated.</para>
    /// <para> . </para>
    /// <para> The IO object includes buffers, streams and IO control flags.</para>
    /// </summary>
    /// <remarks>
    /// <para> The two main types of objects are File and Db (Database).</para>
    /// <para> Each of these may have:</para>
    /// <para> 1) A basic file object (Fmain).</para>
    /// <para> 2) A File Summary Object. </para>
    /// <para> 3) An ID object for identity. </para>
    /// <para> 4) An IO object used readers and writers.</para>
    /// <para> 5) One or more File Status Objects.</para>
    /// <para> 6) One of serveral column and attribute control classes.</para>
    /// </remarks>
    public class FileIoDef : IDisposable {
        // File Object and ItemData - dxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile FileObjects
        // <Area Id = "FileObject">
        public Object DbFileObject;
        // System IO
        // System IO Stream
        public FileStream DbFileStreamObject;
        public StreamReader DbFileStreamReaderObject; // Stream Reader
        public StreamWriter DbFileStreamWriterObject; // Stream Writer
        // System IO TextReader
        public TextReader DbFileTextReadObject; // Text Reader
        #endregion
        // FileSystem Object Common 
        // FileSystem Object Common  - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile FsoFileControlState (make a struct)
        // <Section Summary>
        // <Section Role="Declarations">
        // <Section Id = "MdmFileFileIo">
        // <Section Vs="MdmFileDbVs0_8_9">
        // <Area Id = "GeneralStatusCondition">
        //
        // <Area Id = "IoType">
        protected internal long ipIoType;
        public long IoType {
            get { return ipIoType; }
            set { ipIoType = value; }
        }
        // <Area Id = "FileReadMode">
        protected internal long ipFileReadMode;
        public long FileReadMode {
            get { return ipFileReadMode; }
            set { ipFileReadMode = value; }
        }
        // <Area Id = "FileWriteMode">
        protected internal long ipFileWriteMode;
        public long FileWriteMode {
            get { return ipFileWriteMode; }
            set { ipFileWriteMode = value; }
        }
        // <Area Id = "FileAccessMode">
        protected internal long ipFileAccessMode;
        public long FileAccessMode {
            get { return ipFileAccessMode; }
            set { ipFileAccessMode = value; }
        }
        #endregion
        // 
        #region File Buffers
        protected internal String spIoReadBuffer;
        public String IoReadBuffer {
            get { return spIoReadBuffer; }
            set { spIoReadBuffer = value; }
        }

        protected internal String spIoBlock;
        public String IoBlock {
            get { return spIoBlock; }
            set { spIoBlock = value; }
        }

        protected internal String spIoLine;
        public String IoLine {
            get { return spIoLine; }
            set { spIoLine = value; }
        }

        protected internal String spIoAll;
        public String IoAll {
            get { return spIoAll; }
            set { spIoAll = value; }
        }
        #endregion

        #region Destructors
        // Track whether Dispose has been called.
        private bool disposed = false;
        private bool instantiated = false;

        ~FileIoDef() {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
        }

        public void Dispose(bool disposing) {
            if (disposing) {
                //if (components != null) {
                //    components.Dispose();
                //}
                DataClear();
            }
            // base.Dispose(disposing);
            disposed = true;
        }
        #endregion

        public void DataClear() {
            // File Buffers
            spIoReadBuffer = "";
            spIoBlock = "";
            spIoLine = "";
            spIoAll = "";
            // FsoFileControlState
            ipIoType = 0;
            ipFileReadMode = 0;
            ipFileWriteMode = 0;
            ipFileAccessMode = 0;
            // FileObjects
            if (DbFileObject != null) { DbFileObject = null; }
            if (DbFileStreamObject != null) { DbFileStreamObject.Dispose(); }
            if (DbFileStreamReaderObject != null) { DbFileStreamReaderObject.Dispose(); }
            if (DbFileStreamWriterObject != null) { DbFileStreamWriterObject.Dispose(); }
        }

        public void CopyTo(ref FileIoDef FileIoPassed) {
            // File Buffers
            FileIoPassed.spIoReadBuffer = spIoReadBuffer;
            FileIoPassed.spIoBlock = spIoBlock;
            FileIoPassed.spIoLine = spIoLine;
            FileIoPassed.spIoAll = spIoAll;
            // FsoFileControlState
            FileIoPassed.ipIoType = ipIoType;
            FileIoPassed.ipFileReadMode = ipFileReadMode;
            FileIoPassed.ipFileWriteMode = ipFileWriteMode;
            FileIoPassed.ipFileAccessMode = ipFileAccessMode;
            // FileObjects
            if (DbFileObject != null) { DbFileObject = null; }
            if (DbFileStreamObject != null) { DbFileStreamObject.Dispose(); }
            if (DbFileStreamReaderObject != null) { DbFileStreamReaderObject.Dispose(); }
            if (DbFileStreamWriterObject != null) { DbFileStreamWriterObject.Dispose(); }
            DbFileStreamObject = null;
            DbFileStreamReaderObject = null;
            DbFileStreamWriterObject = null;
        }

        public FileIoDef() {
        }
    }
    // Binary and buffer based File IO
    /// <summary>
    /// <para> Buf IO instances are used for
    /// File IO for the buffer.</para>
    /// <para> . </para>
    /// <para> Generally, a File IO class like Buf
    /// will have an matching BufIO class which
    /// contains file buffers, connection pointers,
    /// file streams and other IO objects.</para>
    /// <para> It will additionally have an ID sub-class
    /// which contains ID's and fields that uniquely
    /// identify the item being accessed.</para>
    /// <para> Typically, a File Status object is
    /// also part of the group for status control</para>
    /// </summary>
    public class BufIoDef {
        // Buf.Seek
        // public File ofNativeFile;
        public int iRecordSize = 1024;
        public int iOffsetSize = (1024 * 32) - 1;
        public int iCurrentOffset;
        public int iCurrentOffsetModulo;
        public int iCurrentOffsetRemainder;
        public int CurrentAttrCounter;
        //
        public void DataClear() {
            iRecordSize = 1024;
            iOffsetSize = (1024 * 32) - 1;
            iCurrentOffset = 0;
            iCurrentOffsetModulo = 0;
            iCurrentOffsetRemainder = 0;
            CurrentAttrCounter = 0;
        }
        public BufIoDef() {
        }
    }

    // File Buffer
    /// <summary>
    /// <para>Buffer Definition includes the bufferes,
    /// read, write and converted counts, control flags
    /// and fields.  </para>
    /// <para> . </para>
    /// <para>The Char fields are indexers used to
    /// point at characters in the buffer.</para>
    /// <para> . </para>
    /// <para> Buf instances are used by the file system
    /// for the ASCII and Binary file types.</para>
    /// </summary>
    public class BufDef {
        #region $include Mdm.Oss.FileUtil Mfile Buf.
        #region $include Mdm.Oss.FileUtil Mfile Buf. Name
        public String FileName;
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile Buf. Buffer
        // Buf. Fields
        public String FileWorkBuffer;
        public String LineBuffer;
        public String NewItem;
        //
        // public String[] Buf.NewItem;     
        //  sNewItem=""
        // NOTE public String[] Item.ItemData; see Cols instead
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile Buf. Control Flags
        //  Convert Parameters
        public String ConvertableItem;
        public int ItemConvertFlag;
        //  Attr Indexing
        public int AttrIndex;
        public int AttrMaxIndex;
        //  Character Controls
        public int CharactersFound;
        public int CharCounter; // ItemData Items in AnyString
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile  Read
        //  Read
        // Number of Reads
        public int ReadFileCounter;
        // Number of Bytes Read
        public bool BytesIsRead = false;
        public int BytesRead;
        public int BytesReadTotal;
        public int BytesConverted;
        public int BytesConvertedTotal;
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile  Buffer Character Indexing
        //  Character Indexing for Buffer
        public int CharIndex = 1;
        public int CharMaxIndex;
        public int CharItemEofIndex;
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile  Write
        //  Writes
        //  Number of Writes
        public int WriteFileCounter;
        //  Number of Bytes Writen
        public int BytesWriten;
        public int BytesWritenTotal;
        #endregion
        #endregion
        public BufDef() { DataClear(); }
        /// <summary>
        /// Clear the read, write and converted fields.
        /// </summary>
        public void ByteCountClear() {
            // Read Counts
            BytesRead = 0;
            BytesReadTotal = 0;
            BytesConverted = 0;
            BytesConvertedTotal = 0;
        }
        /// <summary>
        /// Clear all buffer data.
        /// </summary>
        public void DataClear() {
            ByteCountClear();
        }
    }
    #endregion

    #region Mfile ItemData Structures and Subclasses

    #region Row / Item
    /// <summary>
    /// <para>Item Definition contains the
    /// item data and item Id.</para>
    /// <para> An item is a data record.  Its
    /// specific content can be a:</para>
    /// <para> Document. </para>
    /// <para> File. </para>
    /// <para> Row of data.</para>
    /// <para> Result of one read. </para>
    /// <para> . </para>
    /// <para> A block is not considered an item.  One or
    /// more blocks are read to create an item.</para>
    /// </summary>
    public class ItemDef {
        #region ItemDef Declaration
        #region ItemId Declaration
        // ItemId
        public String spItemId;
        public String ItemId {
            get { return spItemId; }
            set { spItemId = value; }
        }
        public long DataClear(ref ItemDef PassedItem) {
            ItemData = "";
            PassedItem.ItemLen = 0;
            PassedItem.ItemVersion = "";
            PassedItem.ItemVersionDate = "";
            #region ItemId
            PassedItem.ItemId = "Unknown ItemId99";
            #endregion
            return 0;
        }
        // IdControl
        // ItemIdExists
        public bool ipItemIdExists;
        public bool ItemIdExists {
            get { return ipItemIdExists; }
            set { ipItemIdExists = value; }
        }
        public String ItemIdChanged;
        public bool ItemIdIsChanged;
        public int ItemIdCurrentNotValid;
        #endregion
        #region Item Declaration
        // Item ItemData (a whole row or record)
        protected internal String spItemData;
        public String ItemData {
            get { return spItemData; }
            set {
                spItemData = value;
                DataIsSet = true;
            }
        }
        //
        public bool DataIsSet = false;
        // Item File Record Version
        public String ItemVersion;
        public String ItemVersionDate;
        // ItemLen
        public int ipItemLen;
        public int ItemLen {
            get { return ipItemLen; }
            set { ipItemLen = value; }
        }
        #endregion
        #endregion
        public ItemDef() {
        }
    }

    /// <summary>
    /// <para> Row Information </para>
    /// <para> Control flags, counts, indexers for
    /// row control.  Indicates if a line represents
    /// a column or row.</para>
    /// </summary>
    public class RowInfoDef {
        // FileRowInfo
        public bool LineIsRow = false;
        public bool LineIsColumn = false;
        // Read mode on database
        public long UseMethod;
        /// <remarks>
        /// Copied documentation on SQL Connection mode:
        //Default The query may return multiple result sets. 
        // Execution of the query may affect the database state. 
        // Default sets no CommandBehavior flags, 
        // so calling ExecuteReader(CommandBehavior.Default) 
        // is functionally equivalent to calling ExecuteReader(). 
        //SingleResult The query returns a single result set. 
        //SchemaOnly The query returns column information only. 
        // When using SchemaOnly, the .NET Framework Data Provider 
        // for SQL Server precedes the statement being executed with SET FMTONLY ON. 
        //KeyInfo The query returns column and primary key information.  
        //SingleRow The query is expected to return a single row. 
        // Execution of the query may affect the database state. 
        // Some .NET Framework data providers may, but are not required to, 
        // use this information to optimize the performance of the command. 
        // When you specify SingleRow with the ExecuteReader method 
        // of the OleDbCommand object, the .NET Framework Data Provider 
        // for OLE DB performs binding using the OLE DB IRow interface 
        // if it is available. Otherwise, it uses the IRowset interface. 
        // If your SQL statement is expected to return only a single row, 
        // specifying SingleRow can also improve application performance. 
        // It is possible to specify SingleRow when executing queries 
        // that return multiple result sets. In that case, 
        // multiple result sets are still returned, but each result set has a single row. 
        //SequentialAccess Provides a way for the DataReader to handle rows 
        // that contain columns with large binary values. 
        // Rather than loading the entire row, SequentialAccess 
        // enables the DataReader to load data as a stream. 
        // You can then use the GetBytes or GetChars method to specify 
        // a byte location to start the read operation, 
        // and a limited buffer size for the data being returned. 
        //CloseConnection When the command is executed, the associated Connection object 
        // is closed when the associated DataReader object is closed. 
        /// </remarks>
        public bool CloseIsNeeded;
        // Row
        protected internal bool bpHasRows = false;
        public bool HasRows {
            get { return bpHasRows; }
            set { bpHasRows = value; }
        }
        public bool RowContinue;
        // property int RowIndex (Pointer to this row)
        protected internal int ipRowIndex;
        public int RowIndex {
            get { return ipRowIndex; }
            set {
                if (value <= ipRowCount) {
                    ipRowIndex = value;
                }
            }
        }
        protected internal int ipRowCount = -99999;
        public int RowCount {
            get { return ipRowCount; }
            set { ipRowCount = value; }
        }
        public int RowMax;
        // Colomn
        public bool HasColumns;
        public bool ColumnContinue;
        public int ColumnMax;
        // Sql
        public System.Object[] RowArray;
        public RowInfoDef() {
        }
        public RowInfoDef(int PdIndexMaxPassed) {
            RowMax = PdIndexMaxPassed;
        }
    }
    #endregion

    #region Column / Field
    /// <summary>
    /// <para>Column / Field Index Control Class</para>
    /// <para>An indexed class accessing the Col(umn)Array.</para>
    /// <para> . </para>
    /// <para>The Col(umn)Index access the Array which may
    /// hold up to ColumnMax items. The current count is 
    /// found in Col(umn)IndexTotal.</para>
    /// <para> . </para>
    /// <para>This is a general purpose class used for
    /// storing rows of data.  It can also be used for
    /// building row data or as a general utility class.</para>
    /// <para> . </para>
    /// <para>There is one group of fields, ColXxxxxx intended
    /// to manage this Array.  The second group ColAttrXxxxxx
    /// is independent and used locally as a seperate set of
    /// pointers</para>
    /// </summary>
    public class ColIndexDef {
        public ColIndexDef() {

        }
        // property int ColIndex
        // 
        protected internal int ipaColIndex;
        public int ColIndex {
            get { return ipaColIndex; }
            set { ipaColIndex = value; }
        } //   
        public int ColIndexTotal;

        public int ColMaxIndex = (int)ArrayMax.ColumnMax;
        public int ColMaxIndexNew = (int)ArrayMax.ColumnMax + 1;
        // property String aColValues
        //
        public Object[] ColArray = new System.Object[(int)ArrayMax.ColumnMax];
        //
        public String ColValues {
            get { return (String)ColArray[ipaColIndex]; }
            set {
                ColArray[ipaColIndex] = (String)value;
                ColSet = true;
            }
        }
        // ColMaxIndexTemp used with counter and buffers
        public int ColMaxIndexTemp;
        //
        public int ColCount;
        public int ColCountTotal;
        public int ColCounter;

        public bool ColSet = false;
        public int ColInvalid;
        // <Section Summary>
        // <Section Role="Declarations">
        // <Section Id = "MdmFileDictionaryDeclarations">
        // <Section Vs="MdmFileDictVs0_8_9">
        // MdmFileDictionaryDeclarations MdmFileDictVs0_8_9
        //
        public String ColText;

        public bool ColAttrSet = false;
        public int ColAttrInvalid;

        public int ColAttrIndex;
        public int ColAttrCount; // ItemData Items in Item / Row / Item
        public int ColAttrCountTotal;
        public int ColAttrCounter; // Current Attr
        public int ColAttrMaxIndex; // Total Attrs in Item
        public int ColAttrMaxIndexTemp;

        public int ColLength;

        public String ColId;
        public String ColTempId;

        public char[] CharsPassedIn;
        public char[] CharsPassedOut;

    }

    /// <summary>
    /// <para> Column Tranformation Definition</para>
    /// <para> Used to transform a column from on type
    /// to another including transformation and setting
    /// of strong type information for the coloumn.</para>
    /// </summary>
    public class ColTransformDef {
        // Action
        public long ColAction;
        //  ToDo File Column Action
        public const int SFC_RESET = 1;
        public const int SFC_SET_ROW = 2;
        public const int SFC_SET_COLUMN = 3;
        public const int SFC_GET_NATIVE_VALUE = 4;
        public const int SFC_GET_SQL_VALUE = 5;
        public const int SFC_GET_IBM_U2 = 11;
        public const int SFC_GET_IBM_DB2 = 12;
        public const int SFC_GET_MdmTLD_DICT = 21;
        public const int SFC_GET_MdmTLD_DATA = 22;

        public const int SFC_SET_ColumnADD_CMD = 101;
        // File Level
        public bool FileUseIndexName;
        public int FileIndex;
        public String FileIndexName;
        public int FileCount;
        // Row
        public int ColRowIndex;
        public int ColRowCount;
        public bool ColumnHasRows;
        public String ColRowIndexName;
        public DateTime RowLastTouched;
        // Column
        public bool HasRows;
        public bool RowContinue;
        public int RowMax;
        // Column
        public int ColIndex;
        public int ColCount;
        public String ColIndexName;
        public DateTime ColIndexLastTouched;
        public int ColCountVisible;
        public int ColCountHidden;
        public int ColumnMax;
        // Get Results
        public int iGetIndex;
        public String sGetResultToString;
        public String sGetResultNotSupported;
        //
        // Meta and Control
        public Type ttGetFieldType;
        public String iGetName;
        public int iGetOrdinal;
        // XXX public IEnumerator<T> lnGetEnumerator;
        public Type ttGetProviderSpecificFieldType;
        public Object ooGetProviderSpecificValue;
        public int iGetProviderSpecificValues;
        public DataTable tfdtGetSchemaTable;
        public bool bICommandBehavior;
        public bool bIsDBNull;
        //
        public bool bNextResult;
        public bool bRead;
        // Native ItemData Client
        // Native Get and GetArray
        public Object ooGetValue;
        public int iGetValues;
        // Native Field Types
        public bool bGetBoolean;
        public byte bbGetByte;
        public long loGetBytes;
        public char bcGetChar;
        public long loGetChars;
        public String sGetDataTypeName;
        public DateTime tdtGetDateTime;
        public DateTimeOffset tdtoGetDateTimeOffset;
        public decimal deGetDecimal;
        public double doGetDouble;
        public float fGetFloat;
        public Guid tgGetGuid;
        public short isGetInt16;
        public ushort isuGetInt16;
        public int iGetInt32;
        public uint iuGetInt32;
        public long ilGetInt64;
        public ulong iluGetInt64;
        public String sGetString;
        public TimeSpan tdtsGetTimeSpan;
        // Sql ItemData Client
        // Sql Get and GetArray
        public int iGetSqlValues;
        public Object ooGetSqlValue;
        // Sql Field Types
        public System.Data.SqlTypes.SqlBinary sqlbiGetSqlBinary;
        public System.Data.SqlTypes.SqlBoolean sqlbGetSqlBoolean;
        public System.Data.SqlTypes.SqlByte sqlbbGetSqlByte;
        public System.Data.SqlTypes.SqlBytes sqliGetSqlBytes;
        public System.Data.SqlTypes.SqlChars sqliGetSqlChars;
        public System.Data.SqlTypes.SqlDateTime sqltdtGetSqlDateTime;
        public System.Data.SqlTypes.SqlDecimal sqlfdGetSqlDecimal;
        public System.Data.SqlTypes.SqlDouble fdGetSqlDouble;
        public System.Data.SqlTypes.SqlGuid tgGetSqlGuid;
        public System.Data.SqlTypes.SqlInt16 isGetSqlInt16;
        public System.Data.SqlTypes.SqlInt32 iGetSqlInt32;
        public System.Data.SqlTypes.SqlInt64 ilGetSqlInt64;
        public System.Data.SqlTypes.SqlMoney fdGetSqlMoney;
        public System.Data.SqlTypes.SqlSingle fGetSqlSingle;
        public System.Data.SqlTypes.SqlString sGetSqlString;
        // XML ItemData Client
        public System.Data.SqlTypes.SqlXml ooGetSqlXml;
        //
    }

    /// <summary>
    /// <para> Pick Column Indexing Management Class</para>
    /// <para> Used by the derived Pick DB Classes to 
    /// augment the basic column management in Column Index. </para>
    /// </summary>
    public class ColPickDef {
        #region RowColumnCharacterControl
        // ColTrans
        public String ColText;
        public String ColExtracted;
        public String ColTempId;
        public String ColTempFileName;
        // FileCharacterControl
        public String ColCharacter;
        //
        public bool ColQuoteBool = true;
        public int ColQuoteDefault = (int)ColQuoteIs.ColQuoteDOUBLE;
        public int ColQuoteType = (int)ColQuoteIs.ColQuoteDOUBLE;
        //
        public bool ColEscapedBool = false;
        public int ColEscapedInt = 1;
        public int ColEscapedDefault = (int)ColEscapedIs.ColEscapedFORBINARY;
        public int ColEscapedType = (int)ColEscapedIs.ColEscapedFORBINARY;
        #endregion
        public ColPickDef() {
        }
    }

    /// <summary>
    /// <para> Column Type Management Class</para>
    /// <para> Used to set and manage a Column's Type information</para>
    /// </summary>
    public class ColTypeDef {
        #region $include Mdm.Oss.FileUtil Mfile PickDict DictionaryItem Constants
        //This Dict Attr
        public const int ITEM_ISNOTSET = 1000;

        public const int ITEM_ISDICT = 256000;

        public const int ITEM_ISFILE = 2000;
        public const int ITEM_ISFILEALIAS = 4000;

        public const int ITEM_ISTYPESAttr = 8000;

        public const int ITEM_ISFUNCTION = 16000;

        public const int ITEM_ISUNKNOWN = 32000;

        public const int ITEM_ISDATA = 64000;

        public const int ITEM_TYPEError = 128000;

        // This Assication
        // Primary PK
        public const int INDEX_PK = 100;
        // Allias AK
        public const int INDEX_AK = 200;
        // Candiate CK
        public const int INDEX_CK = 800;
        // Defining, Unique defining CK
        public const int INDEX_DK = 1600;

        // Foriegn FK 400
        // Foriegn keys loosely define Rererential Integrity RI
        public const int INDEX_FK = 400; //F,Q Pointer
        public const int INDEX_FK_ACCOUNT = 401; // ACCOUNT
        public const int INDEX_FK_FILE = 402; // Q File Pointer = Account,CustDict,CustData
        public const int INDEX_FK_FileData = 404; // Q File ItemData Pointer = Account,CustDict,CustData1
        public const int INDEX_FK_FileDICT = 408; // Q Dict Pointer = Account,CustDict,Empty
        public const int INDEX_FK_FileData_Attr = 416; // F:Cust:Name
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile DictionaryColumn
        public const int ColumnISNOTSET = 1;
        public const int ColumnISNUMERIC = 2;
        public const int ColumnISINTEGER = 4;
        public const int ColumnISDATE = 8;
        public const int ColumnISDATETIME = 16;
        public const int ColumnISCHAR = 32;
        public const int ColumnISVARCHAR = 64;
        public const int ColumnISCHARU = 128;
        public const int ColumnISVARCHARU = 256;
        public const int ColumnISFLOAT = 512;
        public const int ColumnISCURRENCY = 1024;
        public const int ColumnISFUNCTION = 2048;
        public const int ColumnISUNKNOWN = 4096;
        public const int ColumnTYPEError = 8192;
        #endregion
        public ColTypeDef() {
        }
    }
    #endregion

    #region Database
    /// <summary>
    /// <para>Database ID fields and Objects.</para>
    /// </summary>
    /// <remarks>
    /// <para> The two main types of objects are File and Db (Database).</para>
    /// <para> Each of these may have:</para>
    /// <para> 1) A basic file object (Fmain).</para>
    /// <para> 2) A File Summary Object. </para>
    /// <para> 3) An ID object for identity. </para>
    /// <para> 4) An IO object used readers and writers.</para>
    /// <para> 5) One or more File Status Objects.</para>
    /// <para> 6) One of serveral column and attribute control classes.</para>
    /// </remarks>
    public class DbIdDef {
        // File Database Information - xxxxxxxxxx
        // #region $include Mdm.Oss.FileUtil Mfile FileBaseDatabase
        #region $include Mdm.Oss.FileUtil Mfile FileDatabaseName

        // <Area Id = "FileDatabaseInformation">
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile FileDatabaseSecurity
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile FileDatabaseUser
        #endregion
        // #endregion

        // <Area Id = "SourceDatabaseFileGroupInformation">

        // <Area Id = "SourceDatabaseFileNameInformation">

        // <Area Id = "DatabaseMessageConstants">

        protected internal const String SqlConnectionString =
            "Server=localhost;" +
            "DataBase=;" +
            "Integrated Security=SSPI";

        protected internal const String ConnectionErrorMsg =
            "To run this sample, you must have SQL " +
            "or MSDE with the Northwind database installed.  For " +
            "instructions on installing MSDE, view the ReadMe file.";

        /*
        protected internal const String MSDE_CONNECTION_STRING =
            @"Server=(local)\NetSDK;" +
            "DataBase=;" +
            "Integrated Security=SSPI";
        */
        // <Area Id = "FileDatabaseInformation">

        // See DATABASE REGION FOR THESE FIELDS

        public void DataClear() {
        }
    }

    /// <summary>
    /// <para> Database Status flags</para>
    /// <para> Contains database and connection
    /// specific flags to indicate status.</para>
    /// <para> Note: File Status flag instances are
    /// used for other item and file status needs.</para>
    /// </summary>
    public class DbStatusDef {
        public DbStatusDef() { DataClear(); }
        // File Database Connection - xxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile FileConnection_ControlFields
        // property bool ConnIsInitialized
        protected internal bool bpConnIsInitialized;
        public bool ConnIsInitialized {
            get { return bpConnIsInitialized; }
            set {
                bpConnIsInitialized = value;
            }
        } //   
        // <Area Id = "ConnDoesExist">
        protected internal bool bpConnDoesExist;
        public bool ConnDoesExist {
            get { return bpConnDoesExist; }
            set {
                bpConnDoesExist = value;
            }
        }
        // <Area Id = "SourceDatabaseInformation">
        public long ipDoesExistResult;
        // property long ConnResult 
        protected internal long ipIsConnectingResult;
        public long ConnResult {
            get { return ipIsConnectingResult; }
            set {
                ipIsConnectingResult = value;
            }
        } //   
        // property bool ConnIsValid
        protected internal bool bpConnIsValid;
        public bool ConnIsValid {
            get { return bpConnIsValid; }
            set {
                bpConnIsValid = value;
            }
        } //   
        // property bool ConnIsValid
        // property bool ConnHadError
        protected internal bool bpConnHadError;
        public bool ConnHadError {
            get { return bpConnHadError; }
            set {
                bpConnHadError = value;
            }
        } //   
        // property bool ConnIsCreating
        protected internal bool bpConnIsCreating;
        public bool ConnIsCreating {
            get { return bpConnIsCreating; }
            set {
                bpConnIsCreating = value;
            }
        }
        protected internal bool bpConnIsCreated;
        public bool ConnIsCreated {
            get { return bpConnIsCreated; }
            set {
                bpConnIsCreated = value;
            }
        }
        protected internal bool bpConnIsConnecting;
        public bool ConnIsConnecting {
            get { return bpConnIsConnecting; }
            set {
                bpConnIsConnecting = value;
            }
        }
        protected internal bool bpConnIsConnected;
        public bool ConnIsConnected {
            get { return bpConnIsConnected; }
            set {
                bpConnIsConnected = value;
            }
        }
        protected internal bool bpConnIsOpen;
        public bool ConnIsOpen {
            get { return bpConnIsOpen; }
            set {
                bpConnIsOpen = value;
            }
        }
        protected internal bool bpConnIsClosed;
        public bool ConnIsClosed {
            get { return bpConnIsClosed; }
            set {
                bpConnIsClosed = value;
            }
        }
        // property bool ConnDoDispose
        protected internal bool bpConnDoDispose;
        public bool ConnDoDispose {
            get { return bpConnDoDispose; }
            set {
                bpConnDoDispose = value;
            }
        } //
        // property bool ConnDoKeepOpen
        protected internal bool bpConnDoKeepOpen;
        public bool ConnDoKeepOpen {
            get { return bpConnDoKeepOpen; }
            set {
                bpConnDoKeepOpen = value;
            }
        } //
        // Database
        protected internal bool bpNameIsValid;
        public bool DatabaseNameIsValid {
            get { return bpNameIsValid; }
            set {
                bpNameIsValid = value;
            }
        } //   
        // <Area Id = "FileGroupStatus">
        public bool bpDbFileGroupDoesExist;
        public bool bpDbFileGroupIsValid;
        public bool bpDbFileGroupIsCreating;
        public bool bpDbFileGroupIsCreated;
        #endregion
        public void DataClear() {
            ipIsConnectingResult = (long)StateIs.None;
            bpConnDoKeepOpen = true;
            bpConnDoDispose = true;

            bpConnDoesExist = false;
            ipDoesExistResult = (long)StateIs.None;
            bpConnHadError = false;

            bpConnIsClosed = true;
            bpConnIsConnected = false;
            bpConnIsConnecting = false;
            bpConnIsCreated = false;
            bpConnIsCreating = false;
            bpConnIsInitialized = false;
            bpConnIsOpen = false;
            bpConnIsValid = false;

            bpDbFileGroupDoesExist = false;
            bpDbFileGroupIsCreated = false;
            bpDbFileGroupIsCreating = false;
            bpDbFileGroupIsValid = false;
        }
    }

    /// <summary>
    /// <para>Master File Class for Database</para>
    /// <para> A separate database object for performing
    /// actions on the master files independent of the
    /// current open database file stream object.</para>
    /// </summary>
    public class DbMasterDef {
        // OBJECT SUBCLASS - MASTER FILE
        // Database Master File - xxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile FileMasterServer
        // <Area Id = "FileCommand">
        #region $include Mdm.Oss.FileUtil Mfile FileMaster_Security_Fields

        // <Area Id = "SecurityControl">
        // property bool UseSSIS
        protected internal bool bpUseSSIS = true;
        public bool UseSSPI {
            get { return bpUseSSIS; }
            set { bpUseSSIS = value; }
        } //   

        protected internal String MstrSecurityId = "99999";
        protected internal String MstrSecurity;

        #endregion
        #region $include Mdm.Oss.FileUtil Mfile FileMaster_User_Control

        // <Area Id = "UserControl">
        protected internal String MstrUserServerId = "99999";
        protected internal String MstrUserDbId = "99999";
        protected internal String MstrUserId = "99999";
        protected internal String MstrUser = "MdmUser99";
        protected internal String MstrUserPw = "password99";


        // <Area Id = "UserStatus">
        protected internal bool UserDoesExist = false;
        protected internal bool UserIsInvalid = false;
        protected internal bool UserIsCreating = false;
        protected internal bool UserIsCreated = false;
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile FileMaster_Server_Database
        // <Area Id = "MasterServerInformation">

        // <Area Id = "MasterServerDatabase">
        //
        protected internal String MstrDbSystem = @"";
        protected internal String MstrDbSystemMdm = @"MDMPC11";
        // protected internal String MstrDbSystemDefault = "localhost";
        protected internal String MstrDbSystemDefault = @""; // SYSTEM99
        protected internal String MstrDbSystemDefaultMdm = @"MDMPC11";
        //
        // protected internal String MstrDbService = "SQLSERVER";
        protected internal String MstrDbService = @""; // SQLSERVER
        protected internal String MstrDbServiceMdm = @"SQLEXPRESS";
        // protected internal String MstrDbServiceDefault = "SQLSERVER";
        protected internal String MstrDbServiceDefault = @""; // SERVICE99
        protected internal String MstrDbServiceDefaultMdm = @"SQLEXPRESS";
        //
        // protected internal String MstrDbServer = "localhost";
        protected internal String MstrDbServer = @"";
        // protected internal String MstrDbServerMdm = "MdmServer99";
        protected internal String MstrDbServerMdm = @"MDMPC11\SQLEXPRESS";
        // protected internal String MstrDbServerDefault = "localhost";
        protected internal String MstrDbServerDefault = @"SERVER99";
        // protected internal String MstrDbServerDefaultMdm = "MdmServer99";
        protected internal String MstrDbServerDefaultMdm = @"MDMPC11\SQLEXPRESS";
        //
        protected internal String MstrDbServerId = "99999";
        // protected internal String MstrDbServerMasterDefault = "master..sysdatabases";
        // protected internal String MstrDbServerMasterDefaultMdm = "MdmServer99..sysdatabases";
        protected internal String MstrDbServerMasterDefault = @"SERVER99";
        protected internal String MstrDbServerMasterDefaultMdm = @"MDMPC11\SQLEXPRESS..sysdatabases";
        //
        // <Area Id = "MasterServerFilesLocation">
        /*
        protected internal String MstrDbMasterFile = "localhost.dbo.sysobjects";
        protected internal String MstrDbMasterFileMdm = "MdmServer99.dbo.sysobjects";
        protected internal String MstrDbMasterFileDefault = "localhost.dbo.sysobjects";
        protected internal String MstrDbMasterFileDefaultMdm = "MdmServer99.dbo.sysobjects";
        */
        /*
        protected internal String MstrDbMasterFile = "localhost..sysobjects";
        protected internal String MstrDbMasterFileMdm = "MdmServer99..sysobjects";
        protected internal String MstrDbMasterFileDefault = "localhost..sysobjects";
        protected internal String MstrDbMasterFileDefaultMdm = "MdmServer99..sysobjects";
        */
        protected internal String MstrDbMasterFile = @"MDMPC11\SQLEXPRESS..sysobjects";
        protected internal String MstrDbMasterFileMdm = @"MDMPC11\SQLEXPRESS....sysobjects";
        protected internal String MstrDbMasterFileDefault = @"MDMPC11\SQLEXPRESS....sysobjects";
        protected internal String MstrDbMasterFileDefaultMdm = @"MDMPC11\SQLEXPRESS....sysobjects";

        // <Area Id = "MasterServer - ServerControl">

        // <Area Id = "MasterServer - Connection">
        protected internal String MstrCmd = "not used";
        // <Area Id = "MasterServer - Creation">
        protected internal String MstrDbServerCreateCmd;

        // <Area Id = "MasterServer - DatabaseControl">
        protected internal String MstrDbDatabaseId = "99999";
        // protected internal String MstrDbDatabase = "dbo";
        protected internal String MstrDbDatabase = "";
        protected internal String MstrDbDatabaseDefault = "Database99";
        protected internal String MstrDbDatabaseDefaultMdm = "MdmDatabase99";

        // <Area Id = "MasterServer - FileGroupControl">
        protected internal String MstrDbFileGroupId = "";
        protected internal String MstrDbFileGroup = "";
        protected internal String MstrDbFileGroupDefault = "";
        protected internal String MstrDbFileGroupDefaultMdm = "";

        // <Area Id = "MasterServer - OwnerControl">
        protected internal String MstrDbOwnerId = "99999";
        // protected internal String MstrDbOwner = "dbo";
        // protected internal String MstrDbOwnerDefault = "sa";
        // protected internal String MstrDbOwnerDefaultMdm = "MdmOwner99";
        protected internal String MstrDbOwner = "dbo";
        // protected internal String MstrDbOwnerDefault = "sa";
        protected internal String MstrDbOwnerDefault = "dbo";
        protected internal String MstrDbOwnerDefaultMdm = "MdmOwner99";

        protected internal String MstrDbTable = @"MDMPC11\SQLEXPRESS..sysobjects";
        protected internal String MstrDbTableMdm = @"MDMPC11\SQLEXPRESS....sysobjects";
        protected internal String MstrDbTableDefault = @"MDMPC11\SQLEXPRESS....sysobjects";
        protected internal String MstrDbTableDefaultMdm = @"MDMPC11\SQLEXPRESS....sysobjects";

        protected internal String MstrDbFile = @"MDMPC11\SQLEXPRESS..sysobjects";
        protected internal String MstrDbFileMdm = @"MDMPC11\SQLEXPRESS....sysobjects";
        protected internal String MstrDbFileDefault = @"MDMPC11\SQLEXPRESS....sysobjects";
        protected internal String MstrDbFileDefaultMdm = @"MDMPC11\SQLEXPRESS....sysobjects";

        // <Area Id = "MasterConnectionCommand">

        // <Area Id = "MasterDatabase - Connection">
        // protected internal String MstrConnString;
        protected internal String MstrConnString = Mdm.Oss.FileUtil.DbIdDef.SqlConnectionString;

        // <Area Id = "MasterDatabaseStatus">
        protected internal bool MstrDbDatabaseIsInitialized = false;
        protected internal bool MstrDbDatabaseDoesExist = false;
        protected internal bool MstrDbDatabaseIsInvalid = false;
        protected internal bool MstrDbDatabaseIsCreating = false;
        protected internal bool MstrDbDatabaseIsCreated = false;

        // <Area Id = "EndOfMasterServerAndDatabase">

        #endregion
        #region $include Mdm.Oss.FileUtil Mfile FileMaster_FileGroup
        // <Area Id = "FileGroup">

        protected internal String spMstrDbFileGroupServerId = "99999";
        protected internal String spMstrDbFileGroupDbId = "99999";
        protected internal String spMstrDbFileGroupId = "99999";
        protected internal String spMstrDbFileGroup = "MdmFileGroup99";
        protected internal String spMstrDbFileGroupDefault = "MdmFileGroup99";
        // <Area Id = "FileGroupCommand">
        protected internal String spMDbFileGroupCreateCmd = "not used";
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile FileMaster_DbFile

        // <Area Id = "MasterFile">
        protected internal String spMstrDbFileDbId = "99999";
        protected internal String spMstrDbFileDb = "MdmDatabase99";
        protected internal String spMstrDbFileDbDefault = "MdmDatabase99";

        protected internal String spMstrDbFileId = "99999";
        // protected internal String spMstrDbFile = "MdmFile99";
        protected internal String spMstrDbFile = "INFORMATION_SCHEMA.TABLES";
        // protected internal String spMstrDbFile = "sys.objects";

        // FmainPassed.DbIo.CommandCurrent = "USE[" + Fs.DatabaseName + "]; SELECT * FROM 
        // INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FileId.spFileNameFull + "';";
        // FmainPassed.DbIo.CommandCurrent = "USE[" + Fs.DatabaseName + "]; SELECT * FROM 
        // sys.objects WHERE name = " + "'" + FileId.spFileNameFull + "';";
        // SQL = "SELECT * FROM 
        // INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=MyTable";

        // <Area Id = "MstrDbFileStatus">
        public bool bpMstrDbFileDoesExist = false;
        public bool bpMstrDbFileIsInvalid = false;
        public bool bpMstrDbFileIsCreating = false;
        public bool bpMstrDbFileIsCreated = false;

        #endregion
        // OBJECT SUBCLASS - MASTER FILE CONNECTION
        // Database Master File Connection - xxxx
        #region $include Mdm.Oss.FileUtil Mfile FileMaster_DbFileConnection
        // <Area Id = "MstrDbFileConnectionCreationCommand">
        protected internal String MstrDbConnCreateCmd;

        // <Area Id = "MstrDbFileConnStatus">
        protected internal int ipMstrDbFileConnStatus = -99999;
        public int iMstrDbFileConnStatus {
            get { return ipMstrDbFileConnStatus; }
            set { ipMstrDbFileConnStatus = value; }
        }
        #endregion
        #endregion

        public DbMasterDef() {
        }
    }
    #endregion

    #region Phrase construction
    /// <summary>
    /// <para> Master File Syntax Object</para>
    /// <para> Used for building console commands
    /// for the master database.</para>
    /// <para>Commands include Create, Delete.</para>
    /// </summary>
    public class DbMasterSynDef {
        // OBJECT SUBCLASS - COMMAND PHRASES
        // <Area Id = "MasterDatabase - Creation">
        protected internal String spMstrDbFileCreateCmd;
        protected internal String MstrDbDatabaseCreateCmd;

        #region $include Mdm.Oss.FileUtil Mfile FileDatabasePhrase_Constrution
        // <Area Id = "Phrases">
        protected internal String spMstrDbPhraseDoLine = "; ";
        protected internal String spMstrDbPhraseExecute = "GO"; // wrong
        protected internal String spMstrDbPhraseDoExecute = "GO"; // wrong

        #region $include Mdm.Oss.FileUtil Mfile FileMasterServerAndDatabasePhrases
        // <Area Id = "MasterServerAndDatabasePhrases">

        protected internal String spMstrDbPhraseServer;
        protected internal String spMstrDbPhraseDatabase;
        protected internal String spMstrDbPhraseSecurity;
        protected internal String spMstrDbPhraseUser;
        protected internal String spMstrDbPhraseUserPw;


        // <Area Id = "CreationPhrases">

        // <Area Id = "MasterDatabase - CreationPhrases">

        protected internal String spMstrDbPhrase;

        public bool bpDbPhraseIfIsUsed = true;
        protected internal String spMstrDbPhraseIf = "IF EXISTS (";
        protected internal String spMstrDbPhraseIfEnd = ") ";

        public bool bpDbPhraseSelectIsUsed = true;
        protected internal String spMstrDbPhraseSelect = "SELECT * ";

        public bool bpDbPhraseFromIsUsed = true;
        protected internal String spMstrDbPhraseFrom = "FROM ";
        protected internal String spMstrDbPhraseFromItems = "MdmServer99..sysdatabases";
        protected internal String spMstrDbPhraseFromEnd = " ";

        public bool bpDbPhraseWhereIsUsed = true;
        protected internal String spMstrDbPhraseWhere = "WHERE ";
        // <Area Id = "sb paired list of dict + value
        protected internal String spMstrDbPhraseWhereItems = "Name = 'HowToDemo'";

        public bool bpDbPhraseDropIsUsed = true;
        protected internal String spMstrDbPhraseDrop = "DROP ";
        // <Area Id = "sb paired list of dict + value
        protected internal String spMstrDbPhraseDropItems = "DATABASE HowToDemo";

        public bool bpDbPhraseCreateIsUsed = true;
        protected internal String spMstrDbPhraseCreate = "CREATE ";
        // <Area Id = "sb paired list of dict + value
        protected internal String spMstrDbPhraseCreateItems = "CREATE DATABASE HowToDemo";

        protected internal bool spMstrDbPhraseFileGroupIsUsed = false;
        protected internal String spMstrDbPhraseFileGroup = "HowToDemoFileGroup";
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile FileCreationPhrases
        // <Area Id = "FileCreationPhrases">

        protected internal String spMstrDbFilePhrase;

        public bool bpDbFilePhraseUseIsUsed = true;
        protected internal String spMstrDbFilePhraseUse = "USE ";
        protected internal String spMstrDbFilePhraseUseEnd;

        public bool bpDbFilePhraseIfIsUsed = true;
        protected internal String spMstrDbFilePhraseIf = "IF EXISTS (";
        protected internal String spMstrDbFilePhraseIfEnd = ")";

        public bool bpDbFilePhraseSelectIsUsed = true;
        protected internal String spMstrDbFilePhraseSelect = "SELECT * ";

        public bool bpDbFilePhraseFromIsUsed = true;
        protected internal String spMstrDbFilePhraseFrom = "FROM ";
        protected internal String spMstrDbFilePhraseFromItems = "master..sysdatabases";
        protected internal String spMstrDbFilePhraseFromEnd = " ";

        // property bool DbFilePhraseWhereIsUsed
        public bool bpDbFilePhraseWhereIsUsed = false;
        public bool DbFilePhraseWhereIsUsed {
            get { return bpDbFilePhraseWhereIsUsed; }
            set { bpDbFilePhraseWhereIsUsed = value; }
        } //  this is a sample code snippet

        // Master Phrases

        protected internal String spMstrDbFilePhraseWhere = "WHERE ";
        protected internal String spMstrDbFilePhraseWhereAnd = " AND ";
        // <Area Id = "sb paired list of dict + value">
        protected internal String spMstrDbFilePhraseWhereItems1 = "Name = 'HowToDemo'";
        // <Area Id = "sb paired list of dict + value">
        protected internal String spMstrDbFilePhraseWhereItems2 = "TYPE = 'u'";
        // spMstrDbFilePhraseWhereItemsId[X];
        // spMstrDbFilePhraseWhereItemsExpression[X];
        // spMstrDbFilePhraseWhereItemsValue[X];

        public bool bpDbFilePhraseBeginIsUsed = true;
        protected internal String spMstrDbFilePhraseBegin = "BEGIN";
        protected internal String spMstrDbFilePhraseBeginEnd = "END";

        public bool bpDbFilePhraseDropIsUsed = true;
        protected internal String spMstrDbFilePhraseDrop = "DROP ";
        // <Area Id = "sb paired list of dict + value">
        // protected internal String spMstrDbFilePhraseDropItems = "TABLE " + "HowToDemo.dbo.MdmFile99";
        protected internal String spMstrDbFilePhraseDropItems = "TABLE " + "HowToDemo..MdmFile99";


        // <Section Id = "x">

        public bool bpDbFilePhraseCreateIsUsed = true;
        protected internal String spMstrDbFilePhraseCreate = "CREATE ";
        protected internal String spMstrDbFilePhraseCreateObject = "TABLE ";
        protected internal String spMstrDbFilePhraseCreateTable = "MdmFile99";
        // + "HowToDemo.dbo.This";Table

        protected internal String spMstrDbFilePhraseItemsBegin = "(";
        protected internal String spMstrDbFilePhraseItemsEnd = ")";

        // <Area Id = "spMstrDbFilePhraseDColumnId[X] 
        // spMstrDbFilePhraseWhereItemsType[X]; 
        // spMstrDbFilePhraseWhereItemsTypeHasLength[X];
        // if (bpDbFilePhraseCreateIsUsed)
        // {
        // + "("
        // spMstrDbFilePhraseWhereItemsTypeLength[X];
        // + ")"
        // }
        // + " "
        // spMstrDbFilePhraseWhereItemsRange[X];
        // "NOT NULL "">


        // <Section Id = "x">

        public bool bpDbFilePhraseConstraintIsUsed = true;
        protected internal String spMstrDbFilePhraseConstraint = "CONSTRAINT [";
        protected internal String spMstrDbFilePhraseConstraintCol = "PK_Numeric99";
        protected internal String spMstrDbFilePhraseConstraintNameDefault = "PK_Numeric99";
        protected internal String spMstrDbFilePhraseConstraintEnd = "]";

        protected internal String spMstrDbFilePhraseConstraintType1 = " PRIMARY KEY ";
        protected internal String spMstrDbFilePhraseConstraintType2 = " CLUSTERED ";

        protected internal String spMstrDbFilePhraseConstraintColBegin = " (";
        protected internal String spMstrDbFilePhraseConstraintColName = "Column0";
        protected internal String spMstrDbFilePhraseConstraintColNameDefalut = "Column0";
        protected internal String spMstrDbFilePhraseConstraintColEnd = ")";


        public String spMstrDbFilePhraseCreateItems = "CREATE DATABASE HowToDemo";
        #endregion
        #endregion
        public DbMasterSynDef() {
        }

        public void DataClear() {
            bpDbFilePhraseSelectIsUsed = false;
        }
    }

    /// <summary>
    /// <para> Database File Syntax Object</para>
    /// <para> Used for building console commands
    /// for the database.</para>
    /// <para>Command include Create, Add, Alter, Insert, Update, Delete.</para>
    /// </summary>
    public class DbSynDef {
        // SYNTAX - xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile SqlSyntax
        // property String DatabaseFileCreateCmd
        protected internal String spDatabaseFileCreateCmd;
        public String DatabaseFileCreateCmd {
            get { return spDatabaseFileCreateCmd; }
            set { spDatabaseFileCreateCmd = value; }
        } //   

        // property String ConnCreateCmd
        protected internal String spConnCreateCmd;
        public String sConnCreateCmd {
            get { return spConnCreateCmd; }
            set {
                spConnCreateCmd = value;
            }
        } //   

        // property String OutputCommand
        public String spOutputCommand;
        public String OutputCommand {
            get { return spOutputCommand; }
            set { spOutputCommand = value; }
        } //   
        #region $include Mdm.Oss.FileUtil Mfile Sql File Create Commands
        // Sql File Command
        public String spSqlCreateCmdScript;
        public String spSqlCreateCmd;
        public String spSqlFileDeleteCmd;
        public String spSqlFileAlterCmd;
        public String spSqlFileViewCmd;
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile Sql File Column Commands
        // Sql File Column Commands
        public String spSqlColumnAddCmdScript;
        public String spSqlColumnAddCmd;

        public String spSqlColumnDeleteCmd;

        public String spSqlColumnAlterCmd;
        // spOutputViewCommand
        protected internal String spSqlColumnViewScript = "SCRIPT to create a view of a TABLE ";
        protected internal bool bpSqlColumnViewCmdFirst = true;
        public String spSqlColumnViewCmdPrefix;
        public String spSqlColumnViewCmd;
        public String spSqlColumnViewCmdSuffix;

        // property String OutputInsertCommand
        public String spOutputInsertCommand;
        public String OutputInsertCommand {
            get { return spOutputInsertCommand; }
            set { spOutputInsertCommand = value; }
        } //   
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile Sql Command Output
        // Sql Command Output
        protected internal String spOutputInsertPrefix = "INSERT INTO ";
        protected internal String spOutputInsertPrefix1 = " (";

        // property String OutputInsert
        public String spOutputInsert;
        public String OutputInsert {
            get { return spOutputInsert; }
            set { spOutputInsert = value; }
        } //   

        protected internal String spOutputInsertScript = "SCRIPT TO Insert into TABLE ";
        protected internal bool bpOutputInsertScriptFirst = true;
        protected internal String spOutputInsertSuffix = ")";
        //
        protected internal String spOutputUpdateScript = "UPDATE";
        protected internal bool bpOutputUpdateScriptFirst = true;
        protected internal String spOutputUpdatePrefix = "UPDATE";
        //
        protected internal String spOutputDeleteScript = "DELETE";
        protected internal bool bpOutputDeleteScriptFirst = true;
        protected internal String spOutputDeletePrefix = "DELETE";
        //
        protected internal String spOutputValuesScript = "VALUES (";
        protected internal bool bpOutputValuesScriptFirst = true;
        protected internal String spOutputValuesPrefix = "VALUES (";

        // property String OutputValues
        public String spOutputValues;
        public String OutputValues {
            get { return spOutputValues; }
            set {
                spOutputValues = value;
                OutputValuesSet = true;
            }
        } //   

        public bool OutputValuesSet = false;

        protected internal String spOutputValuesSuffix = ")";
        //
        protected internal String spOutputAlterScript = "SCRIPT TO ALTER TABLE ";
        protected internal bool bpOutputAlterScriptFirst = true;
        //
        protected internal String spOutputAlterCommand = "ALTER TABLE ";
        //
        protected internal String spOutputAlterPrefix = "ALTER TABLE ";
        protected internal String spOutputAlterVerb = "ADD";
        protected internal bool bpOutputAlterColumnNameQuoted = false;
        protected internal String spOutputAlterQuoteChar = "'";
        protected internal String spOutputAlterQuoteCharLeft = "[";
        protected internal String spOutputAlterQuoteCharRight = "]";
        protected internal String spOutputAlterColumnNameSource = "1";
        protected internal String spOutputAlterColumnNameAlias = "1";
        protected internal String spOutputAlterColumnName = "1";
        protected internal String spOutputAlterColumnType = "VARCHAR";
        protected internal String spOutputAlterColumnTypePrefix = "(";
        protected internal String spOutputAlterColumnLength = "50";
        protected internal int ipOutputAlterColumnLength = 50;
        protected internal String spOutputAlterColumnTypeSuffix = ")";
        protected internal String spOutputAlterColumnNull = " NULL";
        protected internal String spOutputAlterListSeparatorChar = ",";
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile Create Database and Database Objects
        // "CREATE DATABASE"
        // "CREATE DEFAULT"
        // "CREATE PROC"
        // "CREATE RULE"
        // "CREATE TRIGGER"
        // "CREATE VIEW"
        // "CREATE SCHEMA"
        // "CREATE PARTITION FUNCTION"
        // "CREATE PARTITION SCHEME"
        // /* Comment text */
        // -- Comment text
        // /* Comment block */

        // @@GLOBALS
        // @@CURSOR
        // @@SYSTEM
        // @@SYSTEM STATISTICAL
        // SYSTEM FUNCTIONS()
        // AGGREGATION()
        // CHECKSUM()
        // CAST() CONVERT()
        // CURSOR_STATUS()
        // DATE()
        // IMAGE TEXTVALID()
        // MATH()
        // METADATA()
        // RANK()
        // SECURITY SUserSID()
        // STRING MANIPULATION()

        // ACTIVE DIRECTORY
        // CATALOG
        // CURSOR MANAGEMENT
        // DATABASE ENGINE
        // DATABASE MAINTENANCE
        // DATABASE QUERIES
        // EXTERNAL SYSTEMS
        // EXTENDED PROCEDURES
        // FULE-TEXT INDEX / SEARCH
        // LOG SHIPPING
        // MAIL
        // NOTIFICATION SERVICES
        // OLE AUTOMATION
        // PROFILER

        // REPLICATION
        // SECURITY
        // SQL AGENT


        // "ENABLE"
        // "DISABLE"

        // "DECLARE"
        // "DECLARE" "@"
        // "SET"
        // "LIKE"

        // "WITH" "AS"
        // "SELECT"
        // "TOP"
        // "SELECT" "INTO"
        // "SELECT" "FROM"
        // "SELECT" "FROM" "WHERE"

        // "GROUP BY"
        // "HAVING"
        // "UNION"

        // "EXCEPT"
        // "INTERCEPT"

        // "ORDER BY"
        // "COMPUTER BY"

        // "FOR"
        // "OPTION"
        // "CASE"
        #endregion
        #endregion
        public DbSynDef() {
        }
    }
    #endregion

    #region Pick Objects
    /// <summary>
    /// <para> Pick Dictionary Item Array Index</para>
    /// <para> An indexed array of ColumnAliasMax elements
    /// that contains the dictionary entry items (records.)
    /// It is used to build or manipulate a Pick dictionary.</para>
    /// </summary>
    /// <remarks>
    /// Usage needs clarification
    /// </remarks>
    public class PickDictItemArrayDef {
        public static int PdIndexMax = (int)ArrayMax.ColumnAliasMax;
        public static int PdIndexMaxNew = (int)ArrayMax.ColumnAliasMax + 1; // Used in the new

        public PickDictItemDef[] aPickDictArray = new PickDictItemDef[PdIndexMaxNew];
        public int PdaIndex;
        public PickDictItemDef this[int PdaIndexPassed] {
            get {
                PdArrayCheck(ref PdaIndexPassed);
                return aPickDictArray[PdaIndex];
            }
            set {
                PdArrayCheck(ref PdaIndexPassed);
                aPickDictArray[PdaIndex] = value;
            }
        }

        public PickDictItemArrayDef() {
            aPickDictArray = new PickDictItemDef[PdIndexMaxNew];
            PdaIndex = 0;
        }

        public void PdArrayCheck(ref int PdaIndexPassed) {
            PdaIndex = PdaIndexPassed;
            if (PdaIndex < 0) {
                PdaIndex = 0;
                // TODO Exception Index Error, out of range (below zero)
            }
            if (PdaIndex > (int)ArrayMax.ColumnAliasMax) {
                PdaIndex = (int)ArrayMax.ColumnAliasMax;
                // TODO Exception Index Error, out of range (greater than maximum allowed)
            }
            if (aPickDictArray[PdaIndex] == null) {
                aPickDictArray[PdaIndex] = new PickDictItemDef();
            }
        }
    }

    /// <summary>
    /// <para> Pick Dictionary Row</para>
    /// <para> An indexed array of ColumnAliasMax elements</para>
    /// </summary>
    /// <remarks>
    /// Usage needs clarification
    /// </remarks>
    public class PickRowDef {
        #region $include Mdm.Oss.FileUtil Mfile PickDictControl
        // Index Key for array and relative row number
        private int ipPdIndex;
        public int PdIndex {
            get { return ipPdIndex; }
            set { ipPdIndex = value; }
        }

        public static int PdIndexMax = (int)ArrayMax.ColumnAliasMax;
        public static int PdIndexMaxNew = (int)ArrayMax.ColumnAliasMax + 1; // Used in the new
        public int PdIndexHigh;
        public int PdIndexAliasLow;
        //
        // public PickDictItemDef[] PickDictArray = new PickDictItemDef[PdIndexMaxNew];
        public PickDictItemArrayDef PickDictArray = new PickDictItemArrayDef();
        //
        public int iAttrType;
        // Extracted from ItemId (0) Dictionary PK / Alias key
        public int PdIndexItemId;
        public bool ItemIdIsNumeric = true;
        public String ItemId;
        public int ItemIntId;
        // Extracted from AttrTwo (2) Dictionary Attr
        public String AttrTwoString;
        public int AttrTwoInt;
        public bool AttrTwoIsNumeric = false;
        //
        public int PdIndexAttrTwo;
        //
        public bool ColumnInvalid = false;
        public int DictColumnTouched;
        // Account name can be equivalent to
        // Account indicating another Company
        // Account indicating another System
        public String AttrTwoStringAccounName;
        // (10)
        // Column Width
        public String ColumnWidthString;
        public int ColumnWidth;
        public bool ColumnWidthIsNumeric;
        //
        // Attr three can be a
        // File Name
        // File Name Dict, File ItemData File
        // File Dict without data
        public String AttrThreeFileName;
        //
        public bool ItemIdFoundNumericPk = false;
        public bool DictColumnIdDone = false;

        private int ipPdIndexTemp;
        public int PdIndexTemp {
            get {
                return ipPdIndexTemp;
            }
            set {
                ipPdIndexTemp = value;
            }
        }

        public int PdItemCount;

        public int PdErrorCount;
        public int PdErrorWarningCount;

        public bool PdIndexDoSearch = true;
        public int ColumnDataPoints;
        #endregion

        public PickRowDef() { }

        public void DataClear() {
            // Pick Dictionary
            PdIndex = 0;
            PdItemCount = 0;
        }

        public void RowDataClear(int PdIndexPassed) {
            // if (PickDictArray[PdIndex] == null) { PickDictArray[PdIndex] = new PickDictItemDef(); } 
            PickDictArray[PdIndexPassed].ItemId = "";
            PickDictArray[PdIndexPassed].ItemIntId = 0;
            PickDictArray[PdIndexPassed].iItemAttrIndex = 0;  // Field being processed in the Dict Column
            //
            PickDictArray[PdIndexPassed].ColIndex = 0;
            PickDictArray[PdIndexPassed].iItemAttrCounter = 0; // TableOpen  How many Fields (size of)
            PickDictArray[PdIndexPassed].iItemAttrCounter = 0;
            PickDictArray[PdIndexPassed].iItemLength = 0;

            PickDictArray[PdIndexPassed].AttrTwoStringValue = "";
            PickDictArray[PdIndexPassed].PdIndexAttrTwo = 0;
            PickDictArray[PdIndexPassed].AttrTwoIsNumeric = false;

            PickDictArray[PdIndexPassed].ColTouched = 0;
            PickDictArray[PdIndexPassed].ColIdDone = false;
            PickDictArray[PdIndexPassed].ColLength = 0;
            PickDictArray[PdIndexPassed].ColLengthChange = false;
            PickDictArray[PdIndexPassed].ColDefinitionFound = false;
        }
    }

    /// <summary>
    /// <para> Pick Dictionary Item</para>
    /// <para> This defines a single dictionary entry
    /// for the pick file system.  Naming conventions
    /// follow the Pick equivalents fairly closely.</para>
    /// </summary>
    /// <remarks>
    /// Usage needs clarification
    /// </remarks>
    public class PickDictItemDef {
        public String ItemId;
        public int ItemIntId;

        public bool InstanceCtor = false;

        public bool ItemIdIsNumeric;
        public String ItemIdConverted;
        //
        public bool ItemIdFoundNumericPk;
        //
        public int iItemAttrIndex;  //  Field being examined in this Dictionary Item
        public int iItemAttrCounter;  // Number of fields making up this Dictionary Item
        public int iItemLength;
        //
        public String sAttrNumber;
        public bool bAttrIsData;
        public bool bAttrIsDict;
        // Type
        public int iAttrType;
        public String sAttrType;
        public String sType;
        // SubType
        public int iAttrSubType;
        public String sAttrSubType;
        public String sSubType;
        //
        public int AttrTwoIntId; // Column Number
        public bool AttrTwoIsNumeric;
        // Array Index
        public int ColIndex; // Dictionary Column Number
        public int ColDataPoints;
        public int ColType;
        public int ColInvalid;
        // Heading (3)
        public String sHeading;
        // Dependancy (4) (5)
        public String sFour;
        public String sDependancy;
        public bool bDependancy;
        public int iDendancyKeyColumn;
        public String sDendancyKeyList;

        public String sFive;
        // (6)
        public String InputConversion;
        public String InputConvType;
        public String InputConvSubType;
        // (7)
        public String spOutputConversion;
        public String spOutputConvType;
        public String spOutputConvSubType;
        // (8)
        public String sCorrelative;
        public String sCorrType;
        public String sCorrSubType;
        // (9)
        public String sJustify;
        public String sJustification;
        public String sJustifyType;
        // (10)
        public String ColWidthString;
        public int ColWidth;
        public bool ColWidthIsNumeric;
        // ??
        public String sHeadingLong;
        public String sHelpShort;
        public String sRevColumnName;

        public int ColNumericPoints;
        public int ColDecimals;
        public int ColCurrencyPoints;
        public int ColDateFormat;
        public int ColFunctionPoints;
        public bool ColSuFile;

        public int ColTouched;
        public bool ColIdDone;
        public int ColLength;
        public bool ColLengthChange;
        public bool ColDefinitionFound;

        public String ColTypeWord;
        public bool ColUseParenthesis;

        public String AttrTwoStringValue;

        private int ipPdIndexAttrTwo;
        public int PdIndexAttrTwo {
            get { return ipPdIndexAttrTwo; }
            set { ipPdIndexAttrTwo = value; }
        }
        //
        public String AttrTwoStringAccounName;
        public String AttrThreeFileName;
        // Add
        public String ColAdd;
        public String ColDelete;
        public String ColAlter;
        public String ColUpdate;
        public String ColValidate;
        public String ColStatisticsGet;
        public String ColView;

        public bool ColAddFlag;
        public bool ColDeleteFlag;
        public bool ColAlterFlag;
        public bool ColUpdateFlag;
        public bool ColValidateFlag;
        public bool ColStatisticsGetFlag;
        public bool ColViewFlag;

        public String sTrigerAdd;
        public String sTrigerDelete;
        public String sTrigerAlter;
        public String sTrigerUpdate;

        public bool bTrigerAdd;
        public bool bTrigerDelete;
        public bool bTrigerAlter;
        public bool bTrigerUpdate;

        public PickDictItemDef() {
            InstanceCtor = true;
            PickDictItemReset(this);
        }

        public void PickDictItemReset(PickDictItemDef PickDictItemPassed) {
            PickDictItemPassed.ItemId = "";
            PickDictItemPassed.ItemIntId = 0;
            PickDictItemPassed.iItemAttrIndex = 0;  // Field being processed in the Dict Column
            //
            PickDictItemPassed.ColIndex = 0;
            PickDictItemPassed.iItemAttrCounter = 0; // TableOpen  How many Fields (size of)
            PickDictItemPassed.iItemAttrCounter = 0;
            PickDictItemPassed.iItemLength = 0;

            PickDictItemPassed.AttrTwoStringValue = "";
            PickDictItemPassed.PdIndexAttrTwo = 0;
            PickDictItemPassed.AttrTwoIsNumeric = false;

            PickDictItemPassed.ColTouched = 0;
            PickDictItemPassed.ColIdDone = false;
            PickDictItemPassed.ColLength = 0;
            PickDictItemPassed.ColLengthChange = false;
            PickDictItemPassed.ColDefinitionFound = false;
        }
    }

    // Pick Dict Item Class
    /// <summary>
    /// <para> Pick Dictionary Index</para>
    /// <para> An indexed array of ColumnMax elements
    /// used to build or manipulate a Pick dictionary.</para>
    /// </summary>
    /// <remarks>
    /// Usage needs clarification
    /// </remarks>
    public class PickDictIndexDef {
        //
        public static int MaxArr = (int)ArrayMax.ColumnMax;
        public static int MaxArrNew = (int)ArrayMax.ColumnMax + 1;
        //
        public String[] IndArray = new string[(int)ArrayMax.ColumnMax];
        //
        public static int ipIndGet;
        //
        public static int ipInd;
        public static int Ind {
            get { return ipInd; }
            set { ipInd = value; }
        }
        // Last Access ItemData
        System.DateTime dtLastAccessDateTime { get; set; }
        //
        // Last Accessed Index
        System.String sLastAccessFieldName { get; set; }
        System.Int32 iLastAccessColumnIndex { get; set; }
        // To enable client code to validate input 
        // when accessing your indexer.
        public int Length {
            get { return IndArray.Length; }
        }
        // Indexer declaration.
        // Input parameter is validated by client 
        // code before being passed to the indexer.
        public String this[int IndPassed] {
            get {
                ipInd = IndPassed;
                return IndArray[IndPassed];
            }

            set {
                ipInd = IndPassed;
                IndArray[IndPassed] = value;
            }
        }
        // This method finds the IndInstance or returns -1
        public int IndGet(String IndValuePassed) {
            ipIndGet = 0;
            foreach (String IndInstance in IndArray) {
                if (IndInstance == IndValuePassed) {
                    // ipInd = ipIndGet;
                    return ipIndGet;
                }
                ipIndGet++;
            }
            return -1;
        }
        // This method finds the IndInstance or returns ""
        public String IndGetValue(String IndValuePassed) {
            ipIndGet = 0;
            foreach (String IndInstance in IndArray) {
                ipIndGet += 1;
                // IndArray[ipIndGet] = "?";
                if (IndInstance == IndValuePassed) {
                    ipInd = ipIndGet;
                    return IndInstance;
                }
                ipIndGet++;
            }
            return "";
        }
        // The get accessor returns an integer for a given string
        public String this[String IndValuePassed] {
            get { return (IndGetValue(IndValuePassed)); }
            set { IndArray[IndGet(IndValuePassed)] = value; }
        }
    }
    #endregion

    #endregion

    /// <summary>
    /// <para> A group of temporary object, pointers and fields.</para>
    /// </summary>
    public class DbFileTempDef {
        // Temp Objects
        public System.Type tThisTempType;
        public Object ooThisTempObject;
        public String sThisTempString;
        public int iThisTempInt;
        public bool bThisTempBool;
        public Object ooTmp;
        public Object ooThis;
        // Temp Fields
    }

    #endregion

    #region Transformation
    /// <summary>
    /// <para> File Transform Control</para>
    /// <para> This is a control structure for
    /// applications that have an input and output file.
    /// When input data is processed (or tranformed) to
    /// create a new output file.</para>
    /// <para> It consists of an input and output
    /// File Summary for persistence and during the
    /// user interface stage.  The summaries are used
    /// to open the file object for processing.</para>
    /// <para> It is permitted and recommended to have
    /// the InFile and OutFile files open during the
    /// user entry process.  However, proper attention
    /// must be paid to changes in file name versus
    /// what file has be opened.</para>
    /// </summary>
    public class FileTransformControlDef : DefStd {
        #region FileTransformRun InputOutput
        public FileTransformControlDef(Mfile InFilePassed, Mfile OutFilePassed)
            : base() {
            InFile = InFilePassed;
            OutFile = OutFilePassed;
            FileTransformControlInitialize();
        }
        public FileTransformControlDef()
            : base() {
            FileTransformControlInitialize();
        }
        public void FileTransformControlInitialize() {
            // InFile.FileTransformControl = this;
            InputFsCurr = new FileSummaryDef(InFile, (int)FileAction_DirectionIs.Input);
            // OutFile.FileTransformControl = this;
            OutputFsCurr = new FileSummaryDef(OutFile, (int)FileAction_DirectionIs.Output);
        }
        // Transformation
        public String TransformFileTitle = "";
        // Source (Import) and Destination (Output) Object
        #region FileInputItem Declaration
        // InFile
        public Mfile InFile;
        public FileSummaryDef InputFsCurr;
        #endregion
        #region FileOutputItem Declaration
        public Mfile OutFile;
        public FileSummaryDef OutputFsCurr;
        #endregion
        #endregion
        #region ItemIdNotes
        // An Id would be found in the 
        // input (import) file
        // this id may be compared to the
        // (entered) ItemId that was
        // supplied by the user.
        // (Currently) the user can only
        // enter one id.  The idea is for them
        // to enter a matched lUrlHistList that would
        // be presented as a paired lUrlHistList for
        // comparison (verification) by the user.
        #endregion
        // 
        // Pass Settings Function
        // 
        public void InputItemClassFields(
            // Part of the struct!!
        #region File Fields Passed
        #region InputFile Passed
            // InFile
            String sPassedInputFileName,
            Mfile ofPassedInputFileObject,
            String sPassedInputFileOptions,
        #endregion
        #region OutputFile Passed
            // OutputSystem
            String OutputSystemNamePassed,
            Object OutputSystemObjectPassed,
            // OutputDatabase
            String OutputDatabaseNamePassed,
            SqlConnection OutputDbConnObjectPassed,
            // OutFile
            String OutputFileNamePassed,
            // Object ofPassedOutputFileObject,
            Mfile ofPassedOutputFileObject,
            String sPassedOutputFileOptions,
            // OutputItemId
            String sPassedOutputItemId,
            // ExistingItemId
            String sPassedInputItemId
        #endregion
        #endregion
            // END OF InputItemClassFields Passed
    ) {
            #region SetInputItem
            InFile.Fmain.Fs.FileId.FileName = sPassedInputFileName;
            InFile.Fmain.Fs.Direction = (int)FileAction_DirectionIs.Input;
            // huh? InFile.Fmain.Item.ItemLen = sPassedInputFileName.Length;
            #endregion
            #region SetFileOutputItem
            OutFile.Fmain.Fs.FileId.FileName = OutputFileNamePassed;
            OutFile.Fmain.Fs.Direction = (int)FileAction_DirectionIs.Output;
            // MFILE1 OBJECT
            if (ofPassedOutputFileObject != null) {
                if (OutFile != null) {
                    OutFile = null;
                }
                OutFile = ofPassedOutputFileObject;
            }
            #endregion
            #region ItemId
            InFile.Fmain.Item.ItemId = sPassedOutputItemId;
            #endregion
            #region SetMinputTldItemClassFields
            // Source and Destination Object
            #region SetInputItem Empty
            #endregion
            #region SetFileOutputItem Empty
            // OutputSystem
            // String Fs.spSystemName;
            // Object ooSystemObject;
            // Output Database
            // String spDatabaseName;
            // SqlConnection DbConnObject;
            // OutFile
            // String OutputFileName;
            // MfilePickDb OutFile;
            // String FileOptionsStringPassed;
            // OutputItemId
            // String ItemId;
            // 
            #endregion
            #region SetInputItemId
            // (Existing in Output vs Options)
            InFile.Fmain.Item.ItemId = "";
            InFile.Fmain.Fs.ItemIdCurrent = "";
            // FileTransformControl.ItemId = @"";
            // FileTransformControl.PickRow.PickDictArray.ItemId = @"";
            InFile.Fmain.Item.ItemIdIsChanged = bNO;
            InFile.Fmain.FileStatus.NameIsChanged = bNO;
            #endregion
            #endregion
            #region ClassInternalProperties
            //#region FileInputItemType Set
            //InFile.Fmain.Fs.FileTypeName = "Unknown FileType99";
            //InFile.Fmain.Fs.FileTypeId = (long)FileType_Is.Unknown; // TODO SHOULD LOAD FROM OPTIONS
            //InFile.Fmain.Fs.FileSubTypeName = "Unknown FileSubType99";
            //InFile.Fmain.Fs.FileSubTypeId = (long)FileType_SubTypeIs.Unknown;
            //#endregion
            //#region FileOutputItemType Set
            //OutFile.Fmain.Fs.FileTypeName = "Unknown FileType99";
            //OutFile.Fmain.Fs.FileTypeId = (long)FileType_Is.Unknown; // TODO SHOULD LOAD FROM OPTIONS
            //OutFile.Fmain.Fs.FileSubTypeName = "Unknown FileSubType99";
            //OutFile.Fmain.Fs.FileSubTypeId = (long)FileType_SubTypeIs.Unknown;
            //#endregion
            #endregion
        } // End of Constructor - InputItemClassFields Passed

        public override String ToString() {
            if (InFile != null && OutFile != null) {
                if (InFile.Fmain.Fs.FileId.FileName != null && OutFile.Fmain.Fs.FileId.FileName != null) {
                    String sTemp = "File Transform Control: " + InFile.ToString() + " and " + OutFile.ToString();
                    return sTemp;
                } else { return "File Transform Control not initialized."; }
            } else { return "File Transform Control not initialized."; }
        }
    }
    #endregion

    #region File Main Object Definition
    /// <summary>
    /// <para> (Main) File Stread Object </para>
    /// <para> See Mfile for an expanded discussion
    /// of these objects.  This is the object that
    /// performs file IO for a Mfile File System
    /// Object.  There is a primary and auxillary object.</para>
    /// </summary>
    /// <remarks>
    /// <para> List of classes used:</para>
    /// <para> General objects:</para>
    /// <para> ....Io State</para>
    /// <para> ....File Action</para>
    /// <para> ....File Summary</para>
    /// <para> ....File Status</para>
    /// <para> Ascii File Objects:</para>
    /// <para> ....File Id (contained in File Summary)</para>
    /// <para> ....File Io (contained in File Summary)</para>
    /// <para> ....File Opt(ions) (contained in File Summary)</para>
    /// <para> ....Item</para>
    /// <para> ....Buf</para>
    /// <para> ....Buf Io</para>
    /// <para> Database File Objects:</para>
    /// <para> ....Db Io</para>
    /// <para> ....Db Status</para>
    /// <para> ....System Status</para>
    /// <para> ....Server Status</para>
    /// <para> ....Conn(ection) Status</para>
    /// <para> Column and Row Management:</para>
    /// <para> ....Row Info(rmation)</para>
    /// <para> ....Col(umn) Index</para>
    /// <para> Primary and Auxillary Column Indexing:</para>
    /// <para> ....Row Info(rmation)</para>
    /// <para> Column Transformation:</para>
    /// <para> ....Col(umn)Transform</para>
    /// <para> Other:</para>
    /// <para> ....Del(imiter)Sep(arator)</para>
    /// </remarks>
    public class FileMainDef : IDisposable {
        public FileMainDef FmainObject;
        // public Object File;
        public Mfile FileObject;
        //
        public String FmainStreamType;
        // System Synchronization
        public IoStateDef IoState;
        // Action
        public FileActionDef FileAction;
        // Primary File
        public FileSummaryDef Fs;
        // public FileIdDef FileId;
        // public FileIoDef FileIo;
        public FileStatusDef FileStatus;
        // Non database files
        #region $include Mdm.Oss.FileUtil Mfile FileBase_FileSystem (Ascii, Binary, Text)
        // Ascii, Text, Tilde...
        //public ItemId uses FileId
        public ItemDef Item;
        //public ItemIo uses FileIo
        // LINE???
        // SENTENCE???
        // PARAGRAPH???
        // SECTION???
        //
        // Binary, Block Mode reads...
        //public BufId uses FileId
        public BufDef Buf;
        public BufIoDef BufIo;
        #endregion
        // Rows & Columns
        #region $include Mdm.Oss.FileUtil Mfile FileBase_RowsColumns
        // ROWS:
        public RowInfoDef RowInfo;
        // COLUMNS:
        public ColIndexDef ColIndex;
        #endregion
        // Delimited Text File Files (Ascii, Text, Tilde)
        public DelSepDef DelSep;
        // Database
        #region $include Mdm.Oss.FileUtil Mfile FileBase_Database
        // SqlClient FileDbConnObject - Primary
        public DbIoDef DbIo;
        public FileStatusDef SystemStatus;
        public FileStatusDef ServerStatus;
        public FileStatusDef ConnStatus;
        public FileStatusDef DbStatus;
        public DbMasterDef DbMaster;
        #endregion
        // Primary and Auxillary Column Indexing
        protected internal RowInfoDef RowInfoDb;
        // Column Transformation
        protected internal ColTransformDef ColTrans;
        //
        private bool ParamsUsed = false;

        #region Constructors
        public FileMainDef(Mfile MfilePassed, int DirectionPassed, String StreamTypePassed) {
            FileObject = MfilePassed;
            FmainStreamType = StreamTypePassed;
            FileMainInitialize(DirectionPassed);
        }
        public FileMainDef() {
            FileMainInitialize((int)FileAction_DirectionIs.None);
            FmainStreamType = "Default";
        }
        public void FileMainInitialize(int DirectionPassed) {
            #region Primary File
            FmainObject = this;
            //
            if (instantiated) { this.Dispose(true); }
            //
            IoState = new IoStateDef();
            //
            FileAction = new FileActionDef(FileObject);
            // File Identification
            // FileId = new FileIdDef();
            Fs = new FileSummaryDef(FileObject, DirectionPassed);
            Fs.FmainStreamType = FmainStreamType;
            // File Status
            FileStatus = new FileStatusDef();
            // Rows
            RowInfo = new RowInfoDef();
            #endregion
            #region Database
            DbIo = new DbIoDef();
            SystemStatus = new FileStatusDef();
            ServerStatus = new FileStatusDef();
            ConnStatus = new FileStatusDef();
            DbStatus = new FileStatusDef();
            // Rows
            RowInfoDb = new RowInfoDef();
            // Column / Field
            // Column Indexing
            ColTrans = new ColTransformDef();
            ColIndex = new ColIndexDef();
            #endregion
            #region Buffer Based I/O
            BufIo = new BufIoDef();
            // Items
            // Item / Row
            Item = new ItemDef();
            Buf = new BufDef();
            #endregion
            // Delimited Text File Files (Ascii, Text, Tilde) - xxxxxxxxxxxxxxxxxxxxx
            DelSep = new DelSepDef();
            #region Database Master File
            DbMaster = new DbMasterDef();
            #endregion
            instantiated = true;
        }
        #endregion
        #region Destructors
        // Track whether Dispose has been called.
        private bool disposed = false;
        private bool instantiated = false;

        ~FileMainDef() {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
        }

        protected void Dispose(bool disposing) {
            // If disposing equals true, dispose all managed
            // and unmanaged resources
            if (disposing) {
                //if (components != null) {
                //    components.Dispose();
                //}
                DbIo.Dispose(disposing);
            }
            // Call the appropriate methods to clean up
            // unmanaged resources here.
            // base.Dispose(disposing);
            disposed = true;
        }
        #endregion

        public void ResetObjects() {
            DbIo.DataClear();
            Fs.DataClear();
        }

        public void DataClear() {
            ResetObjects();
            Fs.DataClear();
            FileStatus = new FileStatusDef();
            SystemStatus = new FileStatusDef();
            ServerStatus = new FileStatusDef();
            ConnStatus = new FileStatusDef();
            DbStatus = new FileStatusDef();
            //
            RowInfo = new RowInfoDef();
            ColTrans = new ColTransformDef();
            //
            DbMaster = new DbMasterDef();
            ////
            //FmainObject;
            //// Object File;
            //FileObject;
            //// System Synchronization
            //IoState;
            //// Action
            //FileAction;
            //// Delimited Text File Files (Ascii, Text, Tilde)
            //DelSep;
        }

    }
    #endregion

    #region Delimited Records
    /// <summary>
    /// <para>Mdm Record class is used
    /// for ASCII delimited IO.  This is
    /// typically disk file data read where
    /// the columns or fields are text.</para>
    /// <para> Typically each line contains
    /// a field, the other most common format
    /// being CSV where a line defines a row
    /// where columns are separated by commas.</para>
    /// <para> MdmRecord accepts Std Delimiter
    /// structures to define field separators.</para>
    /// <para> It is closely tied to the ASCII file
    /// type IO and applies to all sub-types that
    /// are delimited.</para>
    /// </summary>
    public class MdmRecord : DefStd {
        private Object Sender;
        public Object[] Items;
        // private String[] Item;
        public String RecordString;
        public String[] RecordArray;
        public StdDelimDef Del;
        public bool IsQuoted = false;
        public String QuoteChar = "\"";
        public bool IsEscaped = false;
        public int EscapedMode = (int)EscapedFormat.SlashedThreeDigit;

        public long RecordSetResult;
        public long RecordSet(ref String RecordStringPassed) {
            RecordSetResult = (long)StateIs.Started;
            RecordArray = RecordStringPassed.Split(Del.Rs.ToCharArray());
            Array.Resize(ref Items, RecordArray.Length);
            for (int ItemIndex = 0; ItemIndex < RecordArray.Length; ItemIndex++) {
                RecordArray[ItemIndex] = RecordArray[ItemIndex].Trim(Del.Trm.ToCharArray());
                if (RecordArray[ItemIndex].IndexOfAny(Del.Us.ToCharArray()) > 0) {
                    Items[ItemIndex] = RecordArray[ItemIndex].Split(Del.Us.ToCharArray());
                } else {
                    Items[ItemIndex] = RecordArray[ItemIndex];
                }
            }
            RecordArray = null;
            return RecordSetResult;
        }

        #region Indexers
        // Access a record
        public Object this[int RsIndex] {
            get {
                return Items[RsIndex];
            }
            set {
                Items[RsIndex] = value;
            }
        }

        // Access a record column
        public String this[int RsIndex, int UsIndex] {
            get {
                return ((String[])Items[RsIndex])[UsIndex];
            }
            set {
                ((String[])Items[RsIndex])[UsIndex] = value;
            }
        }
        #endregion
        #region Constructors
        public long MdmRecordResult;
        public MdmRecord() {
            Sender = this;
            ipFileTypeId = (long)FileType_Is.TEXT;
            ipFileSubTypeId = (long)FileType_SubTypeIs.ASC;
        }

        public MdmRecord(ref FileMainDef FmainPassed)
            : this() {
            ipFileTypeId = FmainPassed.Fs.ipFileTypeId;
            // spFileType = FmainPassed.Fs.FileTypeName;
            ipFileSubTypeId = FmainPassed.Fs.ipFileSubTypeId;
            // spFileSubType = FmainPassed.Fs.FileSubTypeName;
            //
            MdmRecordResult = MdmRecordInitialize();
            //
            try {
                RecordString = FmainPassed.Fs.FileIo.IoReadBuffer;
                if (RecordString.Length > 0) {
                    MdmRecordResult = RecordSet(ref RecordString);
                    RecordString = "";
                }
            } catch {
                // TODO MdmRecord error handling
            }
        }

        public long MdmRecordInitializeResult;
        public long MdmRecordInitialize() {
            MdmRecordInitializeResult = (long)StateIs.Started;
            MdmRecordInitializeResult = DelimLoad();
            return MdmRecordInitializeResult;
        }
        #endregion

        #region File Type
        // FileTypeName
        //public String spFileType;
        //public String FileTypeName {
        //    get { return spFileType; }
        //    set { spFileType = value; }
        //}
        public long ipFileTypeId;
        public long FileTypeId {
            get { return ipFileTypeId; }
            set {
                ipFileTypeId = value;
                FileTypeMajorId = FileTypeDef.FileTypeMetaLevelGet(ipFileTypeId);
            }
        }
        public long FileTypeMajorId;

        //public String spFileSubType;
        //public String FileSubTypeName {
        //    get { return spFileSubType; }
        //    set { spFileSubType = value; }
        //}
        public long ipFileSubTypeId;
        public long FileSubTypeId {
            get { return ipFileSubTypeId; }
            set {
                ipFileSubTypeId = value;
                FileSubTypeMajorId = FileTypeDef.FileSubTypeMajorGet(ipFileSubTypeId);
                FileSubTypeMinorId = FileTypeDef.FileSubTypeMinorGet(ipFileSubTypeId);
            }
        }
        public long FileSubTypeMajorId;
        public long FileSubTypeMinorId;
        #endregion

        #region Delimiter Load
        public long DelimLoadResult;
        public long DelimLoad() {
            DelimLoadResult = (long)StateIs.Started;
            //None ,
            //FileTypeDictData 1,
            //FileTypeData 2,
            //// Data Types
            //FileTypeTEXT 1,
            //FileTypeSQL 4,
            //FileTypeDB2 1,
            //FileTypeORACLE 2,
            //FileTypeXML 3,
            //FileTypePICK 1,
            //FileTypeOther 2,
            //// Unknonw
            //FileTypeUNKNOWN E,
            //FileTypeUndefined E2,
            //FileTypeUndefined1 E3


            //(long)FileType_SubTypeIs.SQL
            //| (long)FileType_SubTypeIs.MY
            //| (long)FileType_SubTypeIs.DB2
            //| (long)FileType_SubTypeIs.ORACLE
            //| (long)FileType_SubTypeIs.XML
            //| (long)FileType_SubTypeIs.TEXTSTD
            switch (FileSubTypeMajorId) {
                case ((long)FileType_SubTypeIs.FIX):
                    // Columns externally defined
                    // One record per line
                    // Fixed column widths (standard FIX)
                    break;
                case ((long)FileType_SubTypeIs.DAT):
                    // Columns externally defined
                    // ID in first position (Index 0) or externally defined
                    // One record per line
                    // ASCII native formating on line using FS, GS, RS, US, USS, USSS
                    Del = CharTable.DelStdGet();
                    break;
                case ((long)FileType_SubTypeIs.CSV):
                case ((long)FileType_SubTypeIs.Tilde_CSV):
                    // ID bracketed in Tildes in first position (Index 0)
                    // One record per line
                    // CSV formating on line
                    Del = new StdDelimDef();
                    Del.Us = ",";
                    break;
                case ((long)FileType_SubTypeIs.Tilde_ROW):
                    // ID bracketed in Tildes in first position (Index 0)
                    // One column per line
                    // Multivalues embedded in column in Native format
                    Del = CharTable.DelPickGet();
                    break;
                case ((long)FileType_SubTypeIs.Tilde_Native):
                    // ID bracketed in Tildes in first position (Index 0)
                    // One record per line
                    // PICK native formating on line using AM, VM, SVM etc.
                    Del = CharTable.DelPickGet();
                    break;
                case ((long)FileType_SubTypeIs.Tilde_Native_ONE):
                    // Not defined...
                    // ID bracketed in Tildes in first position (Index 0)
                    // One record per line
                    // PICK native formating on line using AM, VM, SVM etc.
                    Del = CharTable.DelPickGet();
                    break;
                case ((long)FileType_SubTypeIs.ASC):
                default:
                    // Columns externally defined
                    // ID in first position (Index 0) or externally defined
                    // One record per line
                    // ASCII native formating on line using FS, GS, 
                    // For RS, US, USS, USSS:
                    //      Rs: each line is a record
                    //      Us: each field is separated by a comma,
                    //          fields are not quoted values.
                    //      Uss: where applicable, multivaules are Stick Chars "|"
                    Del = CharTable.DelAsciiGet();
                    break;
            }
            return DelimLoadResult;
        }
        #endregion
    }
    #endregion
}
