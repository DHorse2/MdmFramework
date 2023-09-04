#region Dependencies
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Navigation;
//
using Mdm;
//@@@CODE@@@using Mdm.Oss.ClipUtil;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
//@@@CODE@@@using Mdm.Oss.Mapp;
//using Mdm.Oss.Mobj;
//using Mdm.Pick;
//using Mdm.Pick.Console;
//@@@CODE@@@using Mdm.Oss.Support;
//using Mdm.Oss.Threading;

#region  Mdm Db and File
using Mdm.Oss.File;
using Mdm.Oss.File.Control;
//using Mdm.Oss.File.Db;
//using Mdm.Oss.File.Db.Data;
//using Mdm.Oss.File.Db.Table;
//using Mdm.Oss.File.Db.Thread;
//using Mdm.Oss.File.Properties;
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
using Mdm.Oss.File.State;
//using Mdm.Oss.File.Type.Link;
//using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion

namespace Mdm.Oss.File.State
{
    #region FileOptionsDef, FileStatusDef, - File Status, Options
    /// <summary>
    /// A general purpose file options flag set.
    /// Can be used for connections, files, file items or rows
    /// or as needed.
    /// </summary> 
    public class FileOptionsDef : StdBaseDef
    {
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
        public FileOptionsDef()
        {

        }
        /// <summary>
        /// Standard clear all flags.
        /// </summary> 
        public void DataClear()
        {
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
        public StateIs OptionsParseResult;
        /// <summary>
        /// Set file options using passed console string.
        /// </summary> 
        public FileOptionsDef(String FileOptionsStringPassed)
        {
            OptionsParseResult = OptionsParse(FileOptionsStringPassed);
        }
        /// <summary>
        /// Create a console string for the current options.
        /// </summary> 
        public StateIs OptionsParseToString(FileOptionsDef FileOptPassed)
        {
            OptionsParseResult = StateIs.Started;

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

            OptionsParseResult = StateIs.Successful;
            return OptionsParseResult;
        }
        /// <summary>
        /// Parses the passed console compatable string and
        /// sets the currents options per the text.
        /// </summary> 
        public virtual StateIs OptionsParse(String FileOptionsPassed)
        {
            OptionsParseResult = StateIs.Started;
            String sOptionString = "";
            int iForCounter = 0;
            string sCurrentString = "";
            DataClear();
            if (FileOptionsPassed.Contains("(")) { FileOptionsPassed = FileOptionsPassed.FieldLast("("); }
            if (FileOptionsPassed.Contains(")")) { FileOptionsPassed = FileOptionsPassed.Field(")", 1); }
            String[] OptionsList = FileOptionsPassed.Split((" ").ToCharArray());
            //For Loop
            for (iForCounter = 0; iForCounter <= OptionsList.Length; iForCounter++)
            {
                sOptionString = OptionsList[iForCounter].Trim();
                sOptionString = sOptionString.Trim((",").ToCharArray());
                sOptionString = sOptionString.Trim((@"\").ToCharArray());
                if (iForCounter > 0) { FileOptionsString += ", "; }
                FileOptionsString += sOptionString;
                switch (sOptionString)
                {
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
                        OptionsParseResult = StateIs.Undefined;
                        LocalMessage.Msg = "Command Line Option (" + sCurrentString + ") does not exist";
                        // 202101 Dgh ToDo (with return not throw?)
                        // 202101 Dgh ToDo XUomCovvXv.TraceMdmDo(ref Sender, bIsMessage, iNoOp, iNoOp, OptionsParseResult, XUomCovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg);
                        throw new NotSupportedException(LocalMessage.Msg);
                        //LocalMessage.ErrorMsg = LocalMessage.Msg;
                        //ExceptNotSupportedImpl(ref FmainPassed, ref ExceptionNotSupported, LocalMessage.ErrorMsg, FileWriteResult);
                        break;
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
    public class FileStatusDef
    {
        #region FileStatusDef Declaration
        // <Area Id = "IoStateStatus">
        protected internal StateIs mStatusCurrent;
        public StateIs StatusCurrent
        {
            get { return mStatusCurrent; }
            set { mStatusCurrent = value; }
        }
        protected internal FileIo_ErrorIs mFileErrorCurrent;
        public FileIo_ErrorIs FileErrorCurrent
        {
            get { return mFileErrorCurrent; }
            set { mFileErrorCurrent = value; }
        }

        public bool bpNameIsValid;
        public bool NameIsValid
        {
            get { return bpNameIsValid; }
            set { bpNameIsValid = value; }
        }
        // property bool HadError
        public bool bpHadError;
        public bool HadError
        {
            get { return bpHadError; }
            set { bpHadError = value; }
        }
        // ID of the File not the record (i.e. FSO)
        // property bool IdExists
        public bool bpIdExists;
        public bool IdExists
        {
            get { return bpIdExists; }
            set { bpIdExists = value; }
        }
        // property bool DoesExist
        protected internal bool bpDoesExist;
        public bool DoesExist
        {
            get { return bpDoesExist; }
            set { bpDoesExist = value; }
        }
        // property bool IsValid
        protected internal bool bpIsValid;
        public bool IsValid
        {
            get { return bpIsValid; }
            set { bpIsValid = value; }
        }
        // <Area Id = "DoesExist">
        protected internal StateIs ipDoesExistResult;
        public StateIs DoesExistResult
        {
            get { return ipDoesExistResult; }
            set { ipDoesExistResult = value; }
        }
        protected internal bool bpIsOpen;
        public bool IsOpen
        {
            get { return bpIsOpen; }
            set { bpIsOpen = value; }
        }
        // <Area Id = "IsOpen"
        protected internal StateIs ipIsOpenResult;
        public StateIs IsOpenResult
        {
            get { return ipIsOpenResult; }
            set { ipIsOpenResult = value; }
        }
        // <Area Id = "IsCreating"
        protected internal bool bpIsCreating;
        public bool IsCreating
        {
            get { return bpIsCreating; }
            set { bpIsCreating = value; }
        }
        protected internal bool bpIsCreated;
        public bool IsCreated
        {
            get { return bpIsCreated; }
            set { bpIsCreated = value; }
        }
        // <Area Id = "IsCreating"
        protected internal StateIs ipIsCreatingResult;
        public StateIs IsCreatingResult
        {
            get { return ipIsCreatingResult; }
            set { ipIsCreatingResult = value; }
        }

        // property bool IsInitialized
        protected internal bool bpIsInitialized;
        public bool IsInitialized
        {
            get { return bpIsInitialized; }
            set { bpIsInitialized = value; }
        }
        // property bool ConnDoKeepConn
        protected internal bool bpDoKeepConn;
        public bool DoKeepConn
        {
            get { return bpDoKeepConn; }
            set
            {
                bpDoKeepConn = value;
                if (!bpDoKeepConn) { bpDoKeepOpen = false; }
            }
        }
        // property bool ConnDoKeepConn
        protected internal bool bpDoKeepOpen;
        public bool DoKeepOpen
        {
            get { return bpDoKeepOpen; }
            set
            {
                bpDoKeepOpen = value;
                if (!bpDoKeepConn) { bpDoKeepOpen = false; }
            }
        }
        protected internal bool bpIsConnecting;
        public bool IsConnecting
        {
            get { return bpIsConnecting; }
            set
            {
                bpIsConnecting = value;
            }
        }
        protected internal bool bpIsConnected;
        public bool IsConnected
        {
            get { return bpIsConnected; }
            set
            {
                bpIsConnected = value;
            }
        }
        // <Area Id = "IsConnecting"
        protected internal StateIs ipIsConnectingResult;
        public StateIs IsConnectingResult
        {
            get { return ipIsConnectingResult; }
            set { ipIsConnectingResult = value; }
        }
        // property bool DoClose
        protected internal bool bpDoClose;
        public bool DoClose
        {
            get { return bpDoClose; }
            set { bpDoClose = value; }
        }
        // property bool DoDispose
        protected internal bool bpDoDispose;
        public bool DoDispose
        {
            get { return bpDoDispose; }
            set { bpDoDispose = value; }
        }
        // property bool NameCurrentResult
        protected internal StateIs ipNameCurrentResult;
        public StateIs NameCurrentResult
        {
            get { return ipNameCurrentResult; }
            set { ipNameCurrentResult = value; }
        }
        //
        // property bool NameIsChanged
        protected internal bool bpNameIsChanged;
        public bool NameIsChanged
        {
            get { return bpNameIsChanged; }
            set { bpNameIsChanged = value; }
        }
        // property bool ItemIsAtEnd
        protected internal bool bpItemIsAtEnd;
        public bool ItemIsAtEnd
        {
            get { return bpItemIsAtEnd; }
            set { bpItemIsAtEnd = value; }
        }
        // property bool HasCharacters
        protected internal bool bpHasCharacters;
        public bool HasCharacters
        {
            get { return bpHasCharacters; }
            set { bpHasCharacters = value; }
        }
        // property bool DoClose
        protected internal bool bpCharactersWereFound;
        public bool CharactersWereFound
        {
            get { return bpCharactersWereFound; }
            set { bpCharactersWereFound = value; }
        }
        //  Read Status
        // property bool ReadIsAtEnd
        protected internal bool bpReadIsAtEnd;
        public bool ReadIsAtEnd
        {
            get { return bpReadIsAtEnd; }
            set { bpReadIsAtEnd = value; }
        }
        // property bool ReadError
        protected internal bool bpReadError;
        public bool ReadError
        {
            get { return bpReadError; }
            set { bpReadError = value; }
        }
        //  Write Status
        // property bool BytesWereWriten
        protected internal bool bpBytesWereWriten;
        public bool BytesWereWriten
        {
            get { return bpBytesWereWriten; }
            set { bpBytesWereWriten = value; }
        }
        #endregion
        public FileStatusDef() { DataClear(); }

        /// <summary>
        /// Standard clear all flags.
        /// </summary> 
        public void DataClear()
        {
            StatusCurrent = StateIs.Started;

            bpDoesExist = false;
            ipDoesExistResult = StateIs.None;
            //
            bpIsInitialized = false;
            bpIsValid = false;
            bpNameIsValid = false;
            //
            bpIsOpen = false;
            ipIsOpenResult = StateIs.None;
            bpDoKeepConn = true;
            bpDoClose = false;
            bpDoDispose = false;
            //
            bpIsConnected = false;
            bpIsConnecting = false;
            ipIsConnectingResult = StateIs.None;
            //
            bpIsCreated = false;
            bpIsCreating = false;
            ipIsCreatingResult = StateIs.None;
            //
            bpIdExists = false;
            //
            NameCurrentResult = StateIs.None;
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
    #endregion
}
