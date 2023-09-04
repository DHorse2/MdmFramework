using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Mdm.Oss.Components;
using Mdm.Oss.Decl;
using Mdm.Oss.File.Type;
using Mdm.Oss.Std;

namespace Mdm.Oss.File.Control
{
    #region File Options
    /// <summary>
    /// A general purpose file options flag set.
    /// Can be used for connections, files, file items or rows
    /// or as needed.
    /// </summary> 
    public class FileOptionsDef : StdDef
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
        public long OptionsParseResult;
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
        public long OptionsParseToString(FileOptionsDef FileOptPassed)
        {
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
        public virtual long OptionsParse(String FileOptionsPassed)
        {
            OptionsParseResult = (long)StateIs.Started;
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
                        OptionsParseResult = (long)StateIs.Undefined;
                        LocalMessage.Msg = "Command Line Option (" + sCurrentString + ") does not exist";
                        // XUomCovvXv.TraceMdmDo(ref Sender, bIsMessage, iNoOp, iNoOp, OptionsParseResult, XUomCovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg);
                        throw new NotSupportedException(LocalMessage.Msg);
                }
            }
            return OptionsParseResult;
        }
    }
    #endregion
    #region File Type, Status, Options, etc.
    /// <summary>
    /// A general purpose file options flag set.
    /// Can be used for connections, files, file items or rows
    /// or as needed.
    /// </summary> 
    public class FileStatusDef : StdBaseDef
    {
        public FileIo_ErrorIs FileErrorCurrent;
        #region FileStatusDef Declaration
        // <Area Id = "IoStateStatus">
        public long ipStatusCurrent;
        public long StatusCurrent
        {
            get { return ipStatusCurrent; }
            set { ipStatusCurrent = value; }
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
        public bool bpDoesExist;
        public bool DoesExist
        {
            get { return bpDoesExist; }
            set { bpDoesExist = value; }
        }
        // property bool IsValid
        public bool bpIsValid;
        public bool IsValid
        {
            get { return bpIsValid; }
            set { bpIsValid = value; }
        }
        // <Area Id = "DoesExist">
        public StateIs ipDoesExistResult;
        public StateIs DoesExistResult
        {
            get { return ipDoesExistResult; }
            set { ipDoesExistResult = value; }
        }
        public bool bpIsOpen;
        public bool IsOpen
        {
            get { return bpIsOpen; }
            set { bpIsOpen = value; }
        }
        // <Area Id = "IsOpen"
        public StateIs ipIsOpenResult;
        public StateIs IsOpenResult
        {
            get { return ipIsOpenResult; }
            set { ipIsOpenResult = value; }
        }
        // <Area Id = "IsCreating"
        public bool bpIsCreating;
        public bool IsCreating
        {
            get { return bpIsCreating; }
            set { bpIsCreating = value; }
        }
        public bool bpIsCreated;
        public bool IsCreated
        {
            get { return bpIsCreated; }
            set { bpIsCreated = value; }
        }
        // <Area Id = "IsCreating"
        public StateIs ipIsCreatingResult;
        public StateIs IsCreatingResult
        {
            get { return ipIsCreatingResult; }
            set { ipIsCreatingResult = value; }
        }

        // property bool IsInitialized
        public bool bpIsInitialized;
        public bool IsInitialized
        {
            get { return bpIsInitialized; }
            set { bpIsInitialized = value; }
        }
        // property bool ConnDoKeepConn
        public bool bpDoKeepConn;
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
        public bool bpDoKeepOpen;
        public bool DoKeepOpen
        {
            get { return bpDoKeepOpen; }
            set
            {
                bpDoKeepOpen = value;
                if (!bpDoKeepConn) { bpDoKeepOpen = false; }
            }
        }
        public bool bpIsConnecting;
        public bool IsConnecting
        {
            get { return bpIsConnecting; }
            set
            {
                bpIsConnecting = value;
            }
        }
        public bool bpIsConnected;
        public bool IsConnected
        {
            get { return bpIsConnected; }
            set
            {
                bpIsConnected = value;
            }
        }
        // <Area Id = "IsConnecting"
        public StateIs ipIsConnectingResult;
        public StateIs IsConnectingResult
        {
            get { return ipIsConnectingResult; }
            set { ipIsConnectingResult = value; }
        }
        // property bool DoClose
        public bool bpDoClose;
        public bool DoClose
        {
            get { return bpDoClose; }
            set { bpDoClose = value; }
        }
        // property bool DoDispose
        public bool bpDoDispose;
        public bool DoDispose
        {
            get { return bpDoDispose; }
            set { bpDoDispose = value; }
        }
        // property bool NameCurrentResult
        public StateIs ipNameCurrentResult;
        public StateIs NameCurrentResult
        {
            get { return ipNameCurrentResult; }
            set { ipNameCurrentResult = value; }
        }
        //
        // property bool NameIsChanged
        public bool bpNameIsChanged;
        public bool NameIsChanged
        {
            get { return bpNameIsChanged; }
            set { bpNameIsChanged = value; }
        }
        // property bool ItemIsAtEnd
        public bool bpItemIsAtEnd;
        public bool ItemIsAtEnd
        {
            get { return bpItemIsAtEnd; }
            set { bpItemIsAtEnd = value; }
        }
        // property bool HasCharacters
        public bool bpHasCharacters;
        public bool HasCharacters
        {
            get { return bpHasCharacters; }
            set { bpHasCharacters = value; }
        }
        // property bool DoClose
        public bool bpCharactersWereFound;
        public bool CharactersWereFound
        {
            get { return bpCharactersWereFound; }
            set { bpCharactersWereFound = value; }
        }
        //  Read Status
        // property bool ReadIsAtEnd
        public bool bpReadIsAtEnd;
        public bool ReadIsAtEnd
        {
            get { return bpReadIsAtEnd; }
            set { bpReadIsAtEnd = value; }
        }
        // property bool ReadError
        public bool bpReadError;
        public bool ReadError
        {
            get { return bpReadError; }
            set { bpReadError = value; }
        }
        //  Write Status
        // property bool BytesWereWriten
        public bool bpBytesWereWriten;
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
            bpDoKeepConn = true;
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
    #endregion
    #region FileAction Constants
    /// <summary> 
    /// Indicates the direction of file IO and a primary type field of files.
    /// </summary> 
    public enum FileAction_DirectionIs : int
    {
        Output = 1,
        Input = 2,
        Both = 3,
        None = 4
    }
    // FileAction.
    /// <summary> 
    /// The read / write IO mode for the indicated lists and objects.
    ///  This indicates wether the object information is stored in a
    /// database, text file, etc. and how it should be loaded (ie. All).
    /// </summary> 
    [Flags]
    public enum FileAction_ReadModeIs : long
    {
        System = FileIo_ModeIs.All,
        Server = FileIo_ModeIs.Sql,
        Service = FileIo_ModeIs.All,
        Database = FileIo_ModeIs.Sql,

        FileGroup = FileIo_ModeIs.Sql,
        Table = FileIo_ModeIs.Sql,
        DbUser = FileIo_ModeIs.Sql,
        DbSecurityType = FileIo_ModeIs.Sql,
        DbPassword = FileIo_ModeIs.Sql,
        DiskFile = FileIo_ModeIs.All,
        AsciiDef = FileIo_ModeIs.All,
        //
        None = 0x0
    }

    /// <summary> 
    /// This enumeration provides a list of basic relooping strategies
    /// that can be employed when allowing the file classes to perform
    /// path and default name searching for requested files.
    /// </summary> 
    /// <remarks>
    /// Part of the principals of operation employed in the OSS classes
    /// is the concept that all properties and objects have
    /// default values and a hierachy of determining defaults that may 
    /// include context analysis.
    /// </remarks> 
    public enum FileAction_OpenControl : long
    {
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // <Area Id = "FileOpenConstants">
        // File Search Loop Control
        TryFirst = -3,
        TryAgain = -2,
        TryEntered = 1,
        TryDefault = 2,
        TryOriginal = 3,
        TryAll = 3
    }
    #endregion
    #region File UI Client Constants
    /// <summary>
    /// <para> File User Interface Level</para>
    /// <para> Indicates which user interface elements
    /// are to be updated or have changed.</para>
    /// <para> . </para>
    /// <para> The database system has four distinct
    /// areas that would likely be displayed in the
    /// user interface.  They are:</para>
    /// <para> ..Core Services </para>
    /// <para> ..User Information </para>
    /// <para> ..Security Information </para>
    /// <para> ..Master Database Information </para>
    /// <para> . </para>
    /// <para> . </para>
    /// <para> The core services are basically the file
    /// being accessed.  It includes:</para> 
    /// <para> ..System</para>
    /// <para> ..Service</para>
    /// <para> ..Server</para>
    /// <para> ..Database</para>
    /// <para> ..FileGroup</para>
    /// <para> ..Table</para>
    /// <para> ..DiskFile</para>
    /// <para> ..FileOwner</para>
    /// </summary>
    [Flags]
    public enum FileUi_LevelIs : long
    {
        MaskCore = 0x00001FFF,
        MaskUser = 0x01FF0000,
        MaskSecurity = 0x02FF0000,
        MaskMaster = 0x04FF0000,
        //
        MaskIsCore = 0x00001000,
        MaskIsUser = 0x01000000,
        MaskIsSecurity = 0x02000000,
        MaskIsMaster = 0x04000000,
        MaskIsFile = 0x08000000,
        MaskOther = 0xFF000000,
        MaskIsOther = 0xFF000000,
        //
        CoreDoAll = FileUi_LevelIs.MaskCore,
        System = 0x1200,
        Service = 0x1100,
        Server = 0x1080,
        Database = 0x1040,
        FileOwner = 0x1020,
        // open 0x1010,
        FileGroup = 0x1008,
        Table = 0x1004,
        DiskFile = 0x1002,
        // open 0x1001,
        //
        UserDoAll = FileUi_LevelIs.MaskUser,
        User = 0x1010000,
        UserPassword = 0x1020000,
        //
        SecurityDoAll = FileUi_LevelIs.MaskSecurity,
        Security = 0x2010000,
        //
        MasterDoAll = FileUi_LevelIs.MaskMaster,
        MasterSystem = 0x4010000,
        MasterServer = 0x4020000,
        MasterDatabase = 0x4040000,
        MasterFile = 0x4080000,
        //
        FileDoAll = FileUi_LevelIs.MaskIsFile,
        File = 0x8010000,
    }
    #endregion
    #region File Io Constants
    /// <summary> 
    /// This enumerations contains file system errors.
    /// </summary> 
    [Flags]
    public enum FileIo_ErrorIs : long
    {
        None = 0x0,
        DiskFull = 0x20,
        DiskError = 0x10,
        NetworkError = 0x100,
        InternetError = 0x1000,
        DatabaseError = 0x10000,
        SqlError = 0x20000,
        NotCompleted = 0x100000,
        IoError = 0x1,
        OsError = 0x2,
        NotSupported = 0x4,
        General = 0x8,
        AccessError = 0x10000000
    }
    /// <summary> 
    /// The read modes for accessing database and disk files.
    /// Sql is the only database mode in use.  For text files the
    /// options include Line Mode, All (content) Mode, or Block Mode.
    /// Block mode is intended for processing very large files.
    /// Where processing is line based and the file is very large
    /// then line mode might be employed.  Binary files would be
    /// read in All Mode or Block Mode.  Binary and Seek Modes
    /// are currently not implemented and should be considered
    /// legacy features.
    /// </summary> 
    [Flags]
    public enum FileIo_ModeIs : long
    {
        None = 0x0,
        // <Area Id = "FileReadModeConstants">
        Block = 0x1,
        Line = 0x10,
        All = 0x100,
        Sql = 0x1000,
        // additional access modes
        Binary = 0x10000,
        Seek = 0x100000
    }
    /// <summary> 
    /// This enumerates the type of SQL file access available
    /// for use by command execution and controls result sets.
    /// </summary> 
    [Flags]
    public enum FileIo_SqlCommandModeIs : long
    {
        None = 0x0,

        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // Schema / Entity Relationship / Dictionary
        //UseErSingleResult = 0x601,
        //UseErSchemaOnly = 0x602,
        //UseErKeyInfo = 0x604,
        //UseErSingleRow = 0x608,
        //UseErSequentialAccess = 0x610,
        //UseErCloseConnection = 0x611,
        //UseExecuteNoQuery = 0x612,
        //UseExecuteScalar = 0x614,
        //
        //UseErDefault = 0x90618,
        //
        Default = CommandBehavior.Default,
        SingleResult = CommandBehavior.SingleResult,
        SchemaOnly = CommandBehavior.SchemaOnly,
        KeyInfo = CommandBehavior.KeyInfo,
        SingleRow = CommandBehavior.SingleRow,
        SequentialAccess = CommandBehavior.SequentialAccess,
        CloseConnection = CommandBehavior.CloseConnection,
        //
        UseExecuteNoQuery = 0xFF612,
        UseExecuteScalar = 0xFF614,
    }
    #endregion
}
