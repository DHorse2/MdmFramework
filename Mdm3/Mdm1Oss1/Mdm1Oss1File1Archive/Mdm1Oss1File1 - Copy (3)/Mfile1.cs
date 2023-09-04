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
using Mdm.Oss.ClipboardUtil;
using Mdm.Oss.CodeUtil;
using Mdm.Oss.Decl;
using Mdm.Oss.Mapp;
using Mdm.Oss.Mobj;
using Mdm.Oss.Support;
using Mdm.Pick;

namespace Mdm.Oss.FileUtil {

    public class Mfile : DefStdBaseRunFileConsole {
        // PROCEED TO SqlFileDictProcessDb(String PassedFileName 
        //
        // Notes:
        // Text File is a regular text file.
        // Ascii ItemData File is a Simple Text File.
        // Sql File is the ItemData Table in the RDBMS.
        // Sql Dict is the Schema or Dictionary
        //  File for the Sql File.
        // DatabaseFile is the Database File that
        //   contains the Sql File.
        // Conn is the Connection opened to
        //   access the Sql File.

        // All Methods [Object]Check[State] take
        // Passed Names and 
        // Options, 
        // States and 
        // Actions.
        // 
        // They currently set THIS Names but
        // do NOT set THIS Options, States or
        // Actions to the passed values.
        // 
        // In future these method should be 
        // independant of THIS and be able to 
        // act on any
        // Passed Name.
        //
        // The rationelle for this is that each
        // Method might need to Check various
        // States of different
        // Files, perhaps evon on different 
        // Servers, without Altering the
        // State of This Object or in particular,
        // The Options and Actions such as:
        // Open, Delete, Connect, Count, 
        // CheckExists, SchemaChange,
        // RowAdd, RowColumnUpdate, etc.
        // 
        // The States or Options and Actions on
        // This Object such as 
        // KeepOpen indicate that the 
        // Using Class wants This Object to
        // ultimately be returned in an
        // Open State.
        // 
        // CONSTANTS SECTION
        // Constants - xxxxxxxxxxxxxxxxxxxxxxxxxx
        //
        // CORE FIELDS SECTION
        // BASE CLASS - FILE
        // File Base Class Definition
        //
        protected internal MetaDef Meta;
        protected internal SysDef Sys;
        protected internal ActionInfoDef ActionInfo;
        protected internal DbFileTempDef DbFileTemp;
        //
        #region $include Mdm.Oss.FileUtil Mfile MdmFileBase_Class
        // Array Sizes
        public static int ColArrayMax = 256; // []
        public static int ColAliasMax = 1024; // []
        #region $include Mdm.Oss.FileUtil Mfile MdmFileBasicInformation

        #region Mfile MdmFile Main Objects

        //#region FileTransformControlDef
        //public FileTransformControlDef FileTransformControl;
        //public FileSummaryDef FileSummary = new FileSummaryDef();
        //public long AppIoObjectSet(FileTransformControlDef omPassedFileTransformControl) {
        //    iAppIoObjectSet = (int)DatabaseControl.ResultStarted;
        //    FileTransformControl = omPassedFileTransformControl;
        //    iAppIoObjectSet = (int)DatabaseControl.ResultOK;
        //    return iAppIoObjectSet;
        //}
        //#endregion
        // Core Objects - Mapplication
        public Mapplication XUomMavvXv;
        public Mfile FileObject;
        
        // public Object FileTransformControl;
        public FileSummaryDef FileSummary;
        public FileSummaryDef FileSummaryAux;
        public FileIdDef FileId;
        public FileIdDef FileIdAux;
        public FileIoDef FileIo;
        public FileIoDef FileIoAux;
        public FileStatusDef FileStatus;
        public FileStatusDef FileStatusAux;
        //
        #endregion
        //
        // <Area Id = "FileContents">
        // ToDo Item (File Record)
        public ItemDef Item;
        public BufDef Buf;
        //
        //
        // ROWS:
        public RowInfoDef RowInfo;
        public RowInfoDef RowInfoAux;
        //
        // COLUMNS:
        public ColIndexDef ColIndex;
        public ColIndexDef ColIndexAux;
        //
        // CORE OBJECTS SECTION
        // FILE SUBCLASS - DATABASE
        // SqlClient FileDatabaseObject - xxxxxxxxxxxxxxxxx
        public DbIoDef DbIo;
        public DbIoDef DbIoAux;

        public DbIdDef DbId;
        public DbIdDef DbIdAux;

        public DbStatusDef DbStatus;
        public DbStatusDef DbStatusAux;

        public DbMasterDef DbMaster;
        public DbMasterSynDef DbMasterSyn;

        #region $include Mdm.Oss.FileUtil Mfile EXCEPTIONS SECTION
        // Database Error Handling - Os and Sql - xxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmException - Database - Command - Os
        // Exception
        // System Exception
        public Exception oeDbFileCmdOsException; // General Os
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmException - Database - Sql
        // System ItemData SqlClient
        public SqlException ExceptionSql; // General Database
        public SqlException ofeMexceptDbFileSqlException; // Sql Database
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmException - Database
        // <Area Id = "FileDatabaseStatus">

        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmPathExceptions
        // PATH ARGUMENT EXCEPTIONS
        // <Area Id = "ArgumentException:
        //  path is a zero-length string, contains only white space, or 
        //  contains one or more invalid characters as defined by InvalidPathChars.  ">

        // <Area Id = "ArgumentNullException:
        //  path is null reference
        //  path is null or Nothing or null ptra null 
        //  (Nothing in Visual Basic). ">

        // PATH LENGTH EXCEPTIONS
        // <Area Id = "PathTooLongException:
        //  The specified path, file name, or both 
        //  exceed the system-defined maximum length. 
        //  For example, on Windows-based platforms, 
        //  paths must be less than 248 characters, 
        //  and file names must be less than 260 characters. ">

        // PATH DIRECTORY TOO LONG
        // <Area Id = "DirectoryNotFoundException:
        //  The specified path is invalid, 
        //  (for example, it is on an unmapped drive). ">

        // ACCESS PERMISSION EXCEPTIONS
        // <Area Id = "UnauthorizedAccessException:
        //  path specified a directory. 
        //  -or- 
        //  The caller does not have the required permission. ">

        // FILE MISSING EXCEPTIONS
        // <Area Id = "FileNotFoundException:
        //  The file specified in path was not found. ">

        // PATH FORMAT UNSUPPORTED
        // <Area Id = "NotSupportedException:
        //  path is in an invalid format. ">
        #endregion
        // Sql Error Types - DataException - xxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmDataException
        // System ItemData
        public DBConcurrencyException x11 = null;
        // System ItemData
        public DataException x11b = null;
        // System ItemData
        public ConstraintException x13 = null;

        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmGeneralException
        public Exception ExceptionGeneral; // General Execption
        public IOException ExceptionIO; // Input Output Execption
        public NotSupportedException ExceptionNotSupported; // Not Supported Execption
        #endregion
        #endregion
        // FILE SUBCLASS - FILE - ROW RECORD ITEM
        // SqlClient - Row Handling - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmSqlObjectTypes
        #region $include Mdm.Oss.FileUtil Mfile MdmSqlRowUpdatingEventHandler
        public SqlRowUpdatingEventHandler x1 = null;
        public SqlRowUpdatingEventArgs x1A = null;
        public SqlRowUpdatedEventHandler x2 = null;
        public SqlRowUpdatedEventArgs x2A = null;
        public SqlRowsCopiedEventHandler x3 = null;
        public SqlRowsCopiedEventArgs x3A = null;
        #endregion
        // TODO z$RelVs4 Devrive SQL (My, Ms,...), ASCII, PICK, DB2, OS2 classes from base file class (Pick Text done)
        // SqlClient - SqlParameter - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmSqlParameter
        // System ItemData SqlClient - PARAMETER
        public SqlParameterCollection x4C = null;
        public SqlParameter x4 = null;
        #endregion
        // SqlClient - SqlNotificationType - xxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmDataException
        // System ItemData SqlClient - NOTIFICATION
        public SqlNotificationType x5t;
        public SqlNotificationSource x5s;
        public SqlNotificationInfo x5i;
        public SqlNotificationEventArgs x5e;
        #endregion
        // SqlClient - SqlParameter - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmSqlInfoMessage
        // System ItemData SqlClient - INFORMATION MESSAGE
        public SqlInfoMessageEventHandler x6 = null; // Delegate
        public SqlInfoMessageEventArgs x6A = null; // Delegate Arguments
        #endregion
        // SqlClient - SqlException - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmDataException Additional Copy
        // System ItemData SqlClient - SQL EXCEPTION
        public SqlException x7 = null;
        #endregion
        // SqlClient - SqlErrorCollection - xxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmSqlError
        // System ItemData SqlClient - SqlError
        public SqlErrorCollection x8C = null;
        public SqlError x8 = null;
        #endregion
        // SqlClient - SqlDbType - xxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmSqlDbType
        // System ItemData - SqlDbType
        public SqlDbType x9; // Sql ItemData Type
        #endregion
        // SqlClient - SqlDataReader - xxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmSqlDataReader
        // System ItemData SqlClient SqlDataReader
        public SqlDataReader SqlDbDataReader = null; // Sql Forward only ItemData Reader
        #endregion
        #endregion
        // SqlClient - StateChangeEvent - xxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmFileExceptions
        #region $include Mdm.Oss.FileUtil Mfile MdmDataException
        // System ItemData SqlClient - StateChangeEvent
        public new StateChangeEventHandler SqlDbConnectStateChangedEvent = null;  // Delegate
        public StateChangeEventArgs SqlDbConnectStateChangedEventArgs = null;  // Delegate Arguments
        #endregion
        // SqlClient - StatementCompleted - xxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmDataException
        // System ItemData SqlClient - StatementCompleted
        public StatementCompletedEventHandler SqlDbCommandCompletedEvent = null;  // Delegate
        public StatementCompletedEventArgs SqlDbCommandCompletedEventArgs = null;  // Delegate Arguments
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmNotSupportedException
        public NotSupportedException oeMexceptNotSupportedException; // Delegate
        // public NotSupportedExceptionController oeMexceptNotSupportedExceptionController; // Delegate
        // public NotSupportedExceptionArgs oeMexceptNotSupportedExceptionArgs; // Delegate
        #endregion
        #endregion
        // OBJECT CORE FIELDS SECTION
        #region $include Mdm.Oss.FileUtil Mfile MdmFileDescriptionArrays
        public String[] DbFileExt = { "tld", "txt", "csv", "mdf", "xxx" };
        public String[] DbFileTypeCode = { "tld", "txt", "csv", "mdf", "xxx" };
        // <Area Id = "more Text: "asc", "fix",
        // more MsSql:
        // more MySql:
        // more Code Script: "bat", "js", "wsh", "proc"
        // more Code Language: "cobol", "dartbasic", "pickbasic", "c", "cpp", "csharp"
        // more Markup: "ini", "AcsDoc", "MdmDoc", "html", "xhtml", "css"
        // more Mdm Protcol: "AscEftBank1", "MdmAi1", "AscCreditCardCo1"
        // more Std Protcol: "IP", "TCP", "Http", "Pkh", "Kermit", "Scada"
        // more Telco Protocol:">

        // <Area Id = "per:
        // String[] saWeekDays = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };">
        public String[] DbFilePrimaryType = { "Text", "Text", "TextCsv", "MsSql", "MySql" };
        public String[] DbFileSecondaryType = { "Tilde", "Text", "Csv", "Datafile", "Datafile" };
        public String[] DbFileTypeDescription = { 
            "Tilde delimeted datafile", 
            "Text File", 
            "Comma delimited  text file", 
            "MS SQL data file", 
            "My Sql data file" };
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmFileControlState
        // <Section Summary>
        // <Section Role="Declarations">
        // <Section Id = "MdmFileDatabaseControlState">
        // <Section Vs="MdmFileDbVs0_8_9">
        // <Area Id = "GeneralStatusCondition">
        //
        // <Area Id = "IoType">
        protected internal int ipIoType;
        public int IoType {
            get { return ipIoType; }
            set { ipIoType = value; }
        }
        // <Area Id = "FileReadMode">
        protected internal int ipFileReadMode;
        public int FileReadMode {
            get { return ipFileReadMode; }
            set { ipFileReadMode = value; }
        }
        // <Area Id = "FileWriteMode">
        protected internal int ipFileWriteMode;
        public int FileWriteMode {
            get { return ipFileWriteMode; }
            set { ipFileWriteMode = value; }
        }
        // <Area Id = "FileAccessMode">
        protected internal int ipFileAccessMode;
        public int FileAccessMode {
            get { return ipFileAccessMode; }
            set { ipFileAccessMode = value; }
        }
        #endregion
        #region File Field Separator ItemData
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
        public int iDataItemAttrEos2Index; // End of Column Separator 2
        public int iDataItemAttrEos1Index; // End of Column Separator 1
        public int iDataItemAttrEosIndex; // End of Column Sub-Value
        public int iDataItemAttrEovIndex; // End of Column Value
        //
        public int iDataItemAttrEoaIndex; // End of Column
        public int iDataItemAttrEoaIndexEnd; // End of Column
        public int iDataItemAttrEorIndex; // End of Row
        public int iDataItemAttrEofIndex; // End of File
        // Character Pointers
        public int iDataItemCharIndex; // DataItem Character Pointer
        public int iDataItemCharEobIndex; // DataItem Character Pointer to end of block
        public int iDataItemCharEofIndex; // DataItem Character Pointer to end of File
        // <Area Id = "AsciiOpenOptions">
        public int iAsciiOpenOptions;
        #endregion
        // BUFFER
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile SourceFileAction
        // <Area Id = "SourceDetailsProperties">

        // <Area Id = "IoActionBeingPerformed">

        // <Area Id = "SourceFileAction">
        protected internal String spFileAction;
        public String FileAction {
            get { return spFileAction; }
            set { spFileAction = value; }
        }

        protected internal String spFileActionName;
        public String FileActionName {
            get { return spFileActionName; }
            set { spFileActionName = value; }
        }
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

        // <Area Id = "MdmStandardFileInformation">

        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmFileParentDwBFields

        // <Area Id = "SourceParentName">
        // public String spParentName;
        public String sParentName { get; protected internal set; }
        // public String spParentNameAlias;
        public String sParentNameAlias { get; protected internal set; }
        // public int ipParentId;
        public int iParentId { get; protected internal set; }
        // public String spParentShortName;
        public String sParentShortName { get; protected internal set; }

        // <Area Id = "SourceConsolodationParentName">
        // public String spConsolodationParentName;
        public String sConsolodationParentName { get; protected internal set; }
        // public String spConsolodationParentNameAlias;
        public String sConsolodationParentNameAlias { get; protected internal set; }
        // public int iConsolodationParentId;
        public int iConsolodationParentId { get; protected internal set; }
        // public String spConsolodationParentShortName;
        public String sConsolodationParentShortName { get; protected internal set; }

        // <Area Id = "ContextDomainInformation">

        // <Area Id = "RootDomainInformation">

        // <Area Id = "RootOwnerEntityInformation">
        // <Area Id = "RootOwnerPathPaternInformation">
        // <Area Id = "RootOwnerClusteringInformation">
        // <Area Id = "RootOwnerReplicationInformation">


        // <Area Id = "XmlXpathInformation">

        // <Area Id = "IpDomainInformation">

        // <Area Id = "SourceODBC">

        // <Area Id = "SQLSystemInformation (Physical)">

        // <Area Id = "SourceSystemInformation">

        // <Area Id = "ServerInformationDatabase">

        #endregion
        #endregion

        // ItemData Files - xxxxxxxxxxxxxxxxxxxxxxxxx
        // base, abstract, sealed, interface... ???
        #region $include Mdm.Oss.FileUtil Mfile MdmFileType_Classes \\ (extension)
        // Ascii File - xxxxxxxxxxxxxxxxxxxxxxxxx
        // Text File - xxxxxxxxxxxxxxxxxxxxxxxxxx        
        // Text Delimited File - xxxxxxxxxxxxxxxx
        // Text Delimited RowPerLine - xxxxxxxxxx
        // Text Delimited RowPerLine - CSV xxxxxx
        // Text Delimited RowPerLine - FIX xxxxxx
        // Text Delimited CellPerLine - xxxxxxxxx
        // Text Delimited CellPerLine - Tilde xxx
        // Sql File - xxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmSql_File
        // Primary and Auxillary Rows
        RowInfoDef SqlRowInfo = new RowInfoDef();
        #endregion // of SqlFile
        #region $include Mdm.Oss.FileUtil Mfile MdmDbFileField
        // Primary and Auxillary Column Indexing
        protected internal RowInfoDef ColInfoDb;
        protected internal RowInfoDef ColInfoDbAux;
        // Columns
        // Primary and Auxillary Column Structs
        protected internal ColInfoDef ColInfo;
        protected internal ColInfoDef ColInfoAux;
        #endregion
        #endregion // of FileTypeClasses
        // Class Internal Method Results - xxxxxx
        #region $include oAssembly oClass MdmClassInternal_Results
        public long iAsciiDataClear;
        public long iAsciiDataCreate;
        public long iAsciiDataCreatePassedName;
        public long iAsciiDataDelete;
        public long iAsciiDataDeletePassedName;
        public long iAsciiDataFileClose;
        public long iAsciiDataFileReset;
        public long iAsciiDataFileStreamReaderCheck;
        public long iAsciiDataReadAll;
        public long iAsciiDataReadBlock;
        public long iAsciiDataReadLine;
        public long iAsciiDataReadBlockSeek;
        public long iAsciiDataWrite;
        public long iAsciiDataWritePassedName;
        //
        public long iMainFileProcessing;
        //
        public long iConnCheckDoesExist;
        public long iConnClose;
        public long iConnCmdBuild;
        public long iConnCreatePassedConn;
        public long iConnCreate;
        public long iConnCreateBuild;
        public long iConnCreateCmdBuild;
        public long iConnOpenPassedName;
        public long iConnOpen;
        public long iConnReset;
        //
        public long iDatabaseFileCloseError;
        public long iDatabaseFileCreate;
        public long iDatabaseFileCreateBuild;
        public long iDatabaseFileCreateConnectionError;
        public long iDatabaseFileCreationError;
        public long iDatabaseFilExceptionGeneralError;
        public long iDatabaseFileNameLongCreateBuild;
        public long iDatabaseFileNameValidate;
        public long iDatabaseFileOpen;
        public long iDatabaseFileOpenError;
        public long iDatabaseFileReset;
        //
        public long iMfile;
        //
        public long iSqlColAction;
        //
        public long iSqlCommandExecute;
        //
        public long SqlFileCheckAndSetDoesExistResult;
        public long SqlFileCheckAndSetDoesExistPassedFileNameResult;
        public long SqlFileCloseResult;
        public long SqlFileCreateResult;
        //
        public long SqlFileDataAddResult;
        public long SqlFileDataGetResult;
        public long SqlFileDataDeleteResult;
        public long SqlFileDataInsertResult;
        public long SqlFileDataUpdateResult;
        public long SqlFileDataWriteResult;
        //
        public long iSqlColAddCmdBuildAllFromArray;
        public long iSqlColAddCmdBuildAddFromArray;
        public long iSqlColAddCmdBuildViewFromArray;
        public long iSqlColAddCmdBuild;
        public long iSqlColAddCmdBuildAll;
        public long iSqlColConvertCharacters;

        public long iSqlFileDictInsert;
        public long iSqlFileDictInsertBuild;
        public long iSqlFileDictInsertDo;
        public long iSqlColClear;
        //
        public long iSqlDictProcessDbPassedFileName;
        //
        public long iSqlFileNameBuildFull;
        public long iSqlFileNameBuildFullString;
        public long iSqlFileOpenPassedFileName;
        public long iSqlFileOpen;
        public long iSqlFileReset;
        //
        public long iTextFileClose;
        public long iTextFileCreate;
        public long iTextFileDelete;
        public long iTextFileCheckAndSetDoesExist;
        public long iTextFileOpen;
        public long iTextFileProcessMain;
        public long iTextFileReset;
        public long iTextFileWrite;
        //
        public long iSetApp;
        //
        #endregion
        #region $include oAssembly oClass MdmClassControl
        #endregion

        public DbSynDef DbSyn;

        // Pick Items
        public PickDictIndexDef PickDictIndex;
        public PickRowDef PickRow;
        public PickDictItemDef PickDictItem;


        // $Section oAssembly oClass Main_FileProcessing // xxxxxxxxxx
        // $Section oAssembly oClass Main_FileProcessing // xxxxxxxxxx
        // $Section oAssembly oClass Main_FileProcessing // xxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmMain_FileProcessing
        // <Section Id = "Constructor">
        public Mfile(ref Mapplication omPassedOb)
            : base() {
            iMfile = (int)DatabaseControl.ResultStarted;
            // iMfile
            XUomMavvXv = omPassedOb;
            StatusLine1MdmTextAdd = XUomMavvXv.StatusLine1MdmTextAdd;
            MfileInitialize();
        }
        /*
        public Mfile(Object PassedFileTransformControl) : this() {
            // iMfile = (int)DatabaseControl.ResultStarted;
            FileTransformControl = PassedFileTransformControl;
        }
        */
        public Mfile()
            : base() {
            iMfile = (int)DatabaseControl.ResultStarted;
            // iMfile
            Sender = this;
            FileObject = this;
            MfileInitialize();
        }
        public void MfileInitialize() {
            #region Initialize
            Meta = new MetaDef();
            Sys = new SysDef();
            ActionInfo = new ActionInfoDef();
            //
            DbFileTemp = new DbFileTempDef();
            //
            FileObject = this;
            if (XUomMavvXv == null) {
                XUomMavvXv = new Mapplication();
            }
            // Primary File
            FileSummary = new FileSummaryDef();

            DbIo = new DbIoDef();

            FileId = new FileIdDef();
            FileIo = new FileIoDef();
            DbId = new DbIdDef();
            // File Status
            FileStatus = new FileStatusDef();
            DbStatus = new DbStatusDef();
            //
            // Items
            // Item / Row
            Item = new ItemDef();
            Buf = new BufDef();
            //
            // Rows
            RowInfo = new RowInfoDef();
            //
            // Column / Field
            // Columns
            // Column Indexing
            ColInfoDb = new RowInfoDef();
            ColInfo = new ColInfoDef();
            ColIndex = new ColIndexDef();
            //
            // Fields
            

            //
            // Auxillary File
            FileSummaryAux = new FileSummaryDef();
            DbIoAux = new DbIoDef();
            FileIdAux = new FileIdDef();
            FileIoAux = new FileIoDef();
            DbIdAux = new DbIdDef();
            FileStatusAux = new FileStatusDef();
            DbStatusAux = new DbStatusDef();
            //
            // RowInfoDef osAf = new RowInfoDef();
            ColInfoDbAux = new RowInfoDef();
            // ColInfoDef osAfc = new ColInfoDef();
            ColInfoAux = new ColInfoDef();
            ColIndexAux = new ColIndexDef();
            //
            // Temp Fields
            // DbFileTempDef osFtemp = new DbFileTempDef();
            //
            // Database Master File
            DbMaster = new DbMasterDef();
            //
            // Phrase Construction
            DbSyn = new DbSynDef();
            DbMasterSyn = new DbMasterSynDef();
            //
            // Pick Specific Object
            PickDictIndex = new PickDictIndexDef();
            PickRow = new PickRowDef();
            PickDictItem = new PickDictItemDef();
            //
            // Initialize Fields
            //
            IterationFirst = bYES;
            MethodIterationFirst = bYES;
            ColInfo.sGetResultNotSupported = "";
            ColInfoAux.sGetResultNotSupported = "";

            FileStatus.bpFileIsInitialized = false;
            FileStatus.FileIsInitialized = false;
            PickRow.PdIndexAliasLow = PickRowDef.PdIndexMaxNew;
            PickRow.PdIndex = 0;
            FileNameClear(ref FileObject);
            ItemIdClear(ref FileObject);
            FileSummary.FileDirectionName = "BOTH";
            FileId.spFileNameFull = "";
            iMfile = SqlFileReset(FileSummary.FileName, FileId.spFileNameFull);
            //
            DbStatus.bpDatabaseFileIsInitialized = false;
            iMfile = DatabaseFileReset();
            //
            DbStatus.bpConnIsInitialized = false;
            iMfile = ConnReset();
            //
            DbMaster.MstrDbDatabaseIsInitialized = false;
            iMfile = DatabaseFileReset();

            ColIndex.CharsPassedIn = new char[40];
            ColIndex.CharsPassedIn = ("/,:*#?\"<>|.,\\';|][{}=+-()*&^%#@!`~ };_").ToCharArray();
            // ColIndex.CharsPassedIn = "/,:*#?\"<>|.,\\';|][{}=+-()*&^%#@!`~ };_";

            ColIndex.CharsPassedOut = new char[40];
            ColIndex.CharsPassedOut = ("________________________________________").ToCharArray();
            // ColIndex.CharsPassedIn = "________________________________________";
            #endregion
            #region FileInputBuffers Set
            Buf.LineBuffer = "";
            Buf.NewItem = "";
            Item.ItemData = "";
            Buf.CharIndex = 1;
            Buf.CharMaxIndex = 0;
            Buf.CharCounter = 0;
            // String[] sNewItem = "";     //  sNewItem=@""
            // String[] InputFile.Item.ItemData = "";      
            //  InputFile.Item.ItemData=@""
            // File Bulk Character Conversion (Function)
            Buf.ConvertableItem = "";
            Buf.ItemConvertFlag = 0; // TODO z$RelVs3 Refine these Import options MfileInitialize
            #endregion
            ConsoleMdmInitialize();
        }
        public override void ConsoleMdmInitialize() {
            base.ConsoleMdmInitialize();
            XUomMavvXv.ConsoleMdmInitializeToController(ref Sender);
        }
        #region AppObjects
        public long AppMfileObjectSet(ref FileSummaryDef omPassedFileSummary) {
            iAppMfileObjectSet = (int)DatabaseControl.ResultStarted;
            FileSummary = omPassedFileSummary;
            iAppMfileObjectSet = (int)DatabaseControl.ResultOK;
            return iAppMfileObjectSet;
        }
        #endregion
        // <Section Id = "MainFileProcessing">
        public long MainFileProcessing() {
            iMainFileProcessing = (int)DatabaseControl.ResultStarted;
            //
            iMainFileProcessing = (int)DatabaseControl.ResultOK;
            return iMainFileProcessing;
        }
        #endregion
        // Sql ItemData File - xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region SqlFileData
        #region SqlFileExistance_Control
        // <<Section Id = "x
        public long SqlFileCheckAndSetDoesExist(ref SqlConnection PassedDbConnection, bool bPassedConnDoClose, bool bPassedConnDoDispose, bool bPassedSqlFileDoClose, ref RowInfoDef ColInfoDbPassed, ref ColInfoDef ColInfoPassed) {
            SqlFileCheckAndSetDoesExistResult = (int)DatabaseControl.ResultStarted;
            FileStatus.iFileDoesExist = (int)DatabaseControl.ResultOK;
            FileStatus.FileDoesExist = false;
            //
            SqlFileCheckAndSetDoesExistResult = SqlFileCheckAndSetDoesExist(ref PassedDbConnection, FileSummary.FileName, FileId.spFileNameFull, bPassedConnDoClose, bPassedConnDoDispose, bPassedSqlFileDoClose, ref ColInfoDbPassed, ref ColInfoPassed);
            return SqlFileCheckAndSetDoesExistResult;
        }
        // <Section Id = "x">
        // TODO z$RelVs2 SqlFileCheckAndSetDoesExist Add Sql File Check Does Exist Code
        public long SqlFileCheckAndSetDoesExist(ref SqlConnection PassedDbConnection, String sPassedFileName, String sPassedFileNameFull, bool bPassedConnDoClose, bool bPassedConnDoDispose, bool bPassedSqlFileDoClose, ref RowInfoDef ColInfoDbPassed, ref ColInfoDef ColInfoPassed) {
            // TODO SqlFileCheckAndSetDoesExist SHOULD BE USING AUX (?done?)
            // TODO SqlFileCheckAndSetDoesExist , bool bPassedUsePrimary
            // TODO SqlFileCheckAndSetDoesExist Read mode on database
            ColInfoDbPassed.UseMethod = (int)DatabaseControl.UseErSingleResult;
            ColInfoDbPassed.CloseIsNeeded = false;
            // Row
            ColInfoDbPassed.HasRows = false;
            ColInfoDbPassed.RowContinue = false;
            ColInfoDbPassed.RowMax = PickRowDef.PdIndexMax;
            // Colomn
            ColInfoDbPassed.HasColumns = false;
            ColInfoDbPassed.ColumnContinue = false;
            ColInfoDbPassed.ColumnMax = PickRowDef.PdIndexMax;
            // TODO SqlFileCheckAndSetDoesExist VISIBILITY OF SQL SCANNING
            // Sql
            ColInfoDbPassed.RowIndex = 0;
            System.Object[] RowArray = new System.Object[ColArrayMax];
            SqlFileCheckAndSetDoesExistPassedFileNameResult = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            // DbStatus.bpConnDoClose = bPassedConnDoClose;
            // DbStatus.bpConnDoDispose = DbStatus.bpConnDoDispose;
            // SqlFileDoClose = bPassedSqlFileDoClose;
            FileStatusAux.iFileDoesExist = (int)DatabaseControl.ResultOperationInProgress;

            DbIo.SqlDbCommandTimeout = 15;

            ColInfoDbPassed.UseMethod = (int)DatabaseControl.UseErSingleResult;
            ColInfoDbPassed.CloseIsNeeded = false;
            // Row
            ColInfoDbPassed.HasRows = false;
            ColInfoDbPassed.RowContinue = false;
            ColInfoDbPassed.RowIndex = 0;
            ColInfoDbAux.RowCount = 0;
            // Clr Native
            ColInfoDbAux.RowMax = PickRowDef.PdIndexMax;
            System.Object[] ThisGetValuesArray = new System.Object[ColArrayMax];
            System.Object ThisGetValueObject;
            // Sql
            // ipRowIndex = 0;
            // System.Object[] RowArray;
            // System.Object osoThisGetSqlValue;
            // Colomn
            ColInfoDbPassed.HasColumns = false;
            ColInfoDbAux.ColumnContinue = false;
            sTemp0 = "";
            SqlFileCheckAndSetDoesExistPassedFileNameResult = SqlColAction(ref DbIo.SqlDbDataReaderObject, ref DbIo.SqlDbDataWriterObject, ref ColInfoDbAux, ref ColInfoAux, false, false, ColInfoDef.SFC_RESET, ref sTemp0, 0, 0);
            //
            System.Type tThisTempType;

            System.Data.SqlTypes.SqlString tsdssThisTempSqlString;

            // This is the test code for performance evaluation on each command type
            // Will require the addition of sql timer functions.
            // Normal usage should be execute schalar.

            // SqlConnection myConnection = new SqlConnection(myConnectionString);
            // SqlCommand myCommand = new SqlCommand(mySelectQuery, myConnection);
            // DbIo.SqlDbCommandObject.ExecuteReader();
            // DbIo.SqlDbCommandObject.ExecuteReader(new CommandBehavior()); == CommandBehavior.Default
            // SqlDataReader myReader = DbIo.SqlDbCommandObject.ExecuteReader(CommandBehavior.CloseConnection);
            // SqlDataReader myReader = DbIo.SqlDbCommandObject.ExecuteReader(CommandBehavior.KeyInfo);
            // SqlDataReader myReader = DbIo.SqlDbCommandObject.ExecuteReader(CommandBehavior.SchemaOnly);
            // SqlDataReader myReader = DbIo.SqlDbCommandObject.ExecuteReader(CommandBehavior.SequentialAccess);
            // SqlDataReader myReader = DbIo.SqlDbCommandObject.ExecuteReader(CommandBehavior.SingleResult);
            // SqlDataReader myReader = DbIo.SqlDbCommandObject.ExecuteReader(CommandBehavior.SingleRow);

            // When CommandBehavior.CloseConnection
            // Implicitly closes the connection because 
            // CommandBehavior.CloseConnection was specified.
            // otherwise:

            // Int32 count = (int32)cmd.ExecuteScalar();

            SqlFileCheckAndSetDoesExistPassedFileNameResult = (int)DatabaseControl.ResultOperationInProgress;
            FileStatus.FileBoolDoesExist = false;
            ColInfoDbAux.RowCount = (int)DatabaseControl.ResultDoesNotExist;
            if (FileId.FileNameFull.Length == 0) {
                FileId.FileNameFull = SqlFileNameBuildFull(ref FileSummary.spDatabaseName, ref FileSummary.spFileOwner, ref FileSummary.spFileName, ref FileId.spFileNameFull);
            }
            //  check Connection
            if (!DbStatus.bpConnIsConnected || PassedDbConnection == null) {
                // <Area Id = "WARNING - Already connected">
                SqlFileCheckAndSetDoesExistPassedFileNameResult = ConnOpen(ref PassedDbConnection);
            }
            if (SqlFileCheckAndSetDoesExistPassedFileNameResult == (int)DatabaseControl.ResultOK || SqlFileCheckAndSetDoesExistPassedFileNameResult == (int)DatabaseControl.ResultOperationInProgress) {
                // UseErSingleResult
                // UseErSchemaOnly
                // UseErKeyINFO
                ColInfoDbPassed.UseMethod = (int)DatabaseControl.UseErSingleROW;
                // UseErSequentialAccess));
                ColInfoDbPassed.CloseIsNeeded = false;
                if (!FileStatus.FileIsOpen) {
                    SqlFileCheckAndSetDoesExistPassedFileNameResult = (int)DatabaseControl.ResultOperationInProgress;
                    #region Sql Command Behavior File probing
                    try {
                        DbIo.CommandCurrent = "";
                        // DbIo.CommandCurrent += "USE [" + FileSummary.DatabaseName + "]; ";
                        DbIo.CommandCurrent += "SELECT * FROM sys.databases ";
                        DbIo.CommandCurrent += "WHERE [name] NOT IN ";
                        DbIo.CommandCurrent += "( ";
                        DbIo.CommandCurrent += "'master' , 'msdb', 'model', 'tempdb', ";
                        DbIo.CommandCurrent += "'resource', 'distribution' ";
                        DbIo.CommandCurrent += ")";
                        // DbIo.CommandCurrent += ";";
                        // DbIo.CommandCurrent = "USE [" + FileSummary.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FileSummary.FileName + "'";
                        // DbIo.CommandCurrent = "USE[" + FileSummary.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FileId.FileNameFull + "'";
                        // DbIo.CommandCurrent = "USE[" + FileSummary.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FileId.FileNameFull + "';";
                        // DbIo.CommandCurrent = "USE[" + FileSummary.DatabaseName + "]; SELECT * FROM sys.objects WHERE name = " + "'" + FileId.FileNameFull + "';";
                        // SQL = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=MyTable";
                        // int result = this.ExecuteQuery("if exists(select * from sys.databases where name = {0}", DatabaseName) return 1 else 0");
                        // \r\n"; FROM INFORMATION_SCHEMA.TABLES
                        #region Sql Command Behavior Cases
                        ColInfoDbAux.RowCount = (int)DatabaseControl.ResultDoesNotExist;
                        DbIo.SqlDbCommandObject = null;
                        DbIo.SqlDbCommandObject = new SqlCommand(DbIo.CommandCurrent, PassedDbConnection);
                        DbIo.SqlDbCommandObject.CommandTimeout = DbIo.SqlDbCommandTimeout;
                        DbIo.SqlDbCommandObject.CommandType = CommandType.Text;
                        switch (ColInfoDbPassed.UseMethod) {
                            case ((int)DatabaseControl.UseExecuteNoQuery):
                                // Not appropriate for Check Does Exist
                                // no row or columns
                                // used for create, settings, etc
                                try {
                                    ColInfoDbAux.RowCount = DbIo.SqlDbCommandObject.ExecuteNonQuery();
                                    // MessageMdmSendToPageNewLine(ref Sender, "A2" + DbIo.SqlDbDataReaderObject.sGetString(0));
                                } finally {
                                }
                                break;
                            case ((int)DatabaseControl.UseErDefault):
                            case ((int)DatabaseControl.UseErSingleROW):
                            case ((int)DatabaseControl.UseErSingleResult):
                            case ((int)DatabaseControl.UseErKeyINFO):
                            case ((int)DatabaseControl.UseErSchemaOnly):
                            case ((int)DatabaseControl.UseExecuteScalar):
                                #region Execute Scalar Command
                                // DbIo.SqlDbDataReaderObject.
                                DbIo.SqlDbDataReaderObject = null;
                                DbIo.SqlDbDataWriterObject = null;
                                try {
                                    switch (ColInfoDbPassed.UseMethod) {
                                        case ((int)DatabaseControl.UseExecuteScalar):
                                            // Scalar = 1 Row 1 Column, no reader
                                            try {
                                                DbFileTemp.ooThisTempObject = DbIo.SqlDbCommandObject.ExecuteScalar();
                                                // MessageMdmSendToPageNewLine(ref Sender, "A2" + DbIo.SqlDbDataReaderObject.sGetString(0));
                                            } finally {
                                            }
                                            break;
                                        case ((int)DatabaseControl.UseErDefault):
                                        // All Rows All Columns
                                        case ((int)DatabaseControl.UseErSingleROW):
                                        // Single Row
                                        case ((int)DatabaseControl.UseErKeyINFO):
                                        // Column and Primary Key info
                                        case ((int)DatabaseControl.UseErSchemaOnly):
                                        // Column info
                                        case ((int)DatabaseControl.UseErSingleResult):
                                            // Single Result Set
                                            DbIo.SqlDbDataReaderObject = DbIo.SqlDbCommandObject.ExecuteReader((CommandBehavior)ColInfoDbPassed.UseMethod);
                                            // ColInfoDbPassed.CloseIsNeeded = true;
                                            break;
                                        default:
                                            SqlFileCheckAndSetDoesExistPassedFileNameResult = (int)DatabaseControl.ResultUndefined;
                                            sLocalErrorMessage = "The Sql Command Behavior (Mehtod of reading the file) has not been set)";
                                            throw new NotSupportedException(sLocalErrorMessage);
                                    }
                                    #region Catch Error on probative read
                                } catch (SqlException ExceptionSql) {
                                    sLocalErrorMessage = "";
                                    ExceptSql(sLocalErrorMessage, ref ExceptionSql, SqlFileCheckAndSetDoesExistPassedFileNameResult);
                                    SqlFileCheckAndSetDoesExistPassedFileNameResult = (int)DatabaseControl.ResultDatabaseError;
                                    FileStatusAux.FileDoesExist = false;
                                    ColInfoDbAux.RowCount = (int)DatabaseControl.ResultDoesNotExist;
                                    FileStatusAux.FileIsOpen = false;
                                } catch (Exception ExceptionGeneral) {
                                    sLocalErrorMessage = "";
                                    ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, SqlFileCheckAndSetDoesExistPassedFileNameResult);
                                    SqlFileCheckAndSetDoesExistPassedFileNameResult = (int)DatabaseControl.ResultOsError;
                                    FileStatusAux.FileBoolDoesExist = false;
                                    ColInfoDbAux.RowCount = (int)DatabaseControl.ResultDoesNotExist;
                                    FileStatusAux.FileIsOpen = false;
                                } finally {
                                    // DbIo.SqlDbCommandObject = null;
                                    // SqlFileCheckAndSetDoesExistPassedFileNameResult = ColInfoDbAux.RowCount;
                                }
                                // If File is NOT Open Try to Select the File Name in the Master File
                                    #endregion
                                #endregion
                                #region Reader Object Get Type
                                // DbIo.SqlDbCommandObject.Container.Components.Count;
                                try {
                                    ColInfoDbPassed.RowIndex = 0;
                                    ColInfoDbAux.RowCount = -1;
                                    ColInfoDbPassed.HasRows = DbIo.SqlDbDataReaderObject.HasRows;
                                    ColInfo.ColCount = DbIo.SqlDbDataReaderObject.FieldCount;

                                    // only applies to db changes:
                                    // ColInfoDbAux.RowCount = DbIo.SqlDbDataReaderObject.RecordsAffected;
                                    tThisTempType = DbIo.SqlDbDataReaderObject.GetType();
                                    //
                                    if (ColInfoDbPassed.HasRows) {
                                        SqlFileCheckAndSetDoesExistPassedFileNameResult = (int)DatabaseControl.ResultDoesExist;
                                        FileStatusAux.iFileDoesExist = (int)DatabaseControl.ResultDoesExist;
                                        FileStatusAux.FileBoolDoesExist = true;
                                        // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                                        // FILE IS EXISTS
                                    }
                                    #region Catch errors on Reader Object Get Type
                                } catch (SqlException ExceptionSql) {
                                    sLocalErrorMessage = "";
                                    ExceptSql(sLocalErrorMessage, ref ExceptionSql, SqlFileCheckAndSetDoesExistPassedFileNameResult);
                                    SqlFileCheckAndSetDoesExistPassedFileNameResult = (int)DatabaseControl.ResultDatabaseError;
                                    FileStatusAux.iFileDoesExist = (int)DatabaseControl.ResultDoesNotExist;
                                } catch (Exception ExceptionGeneral) {
                                    sLocalErrorMessage = "";
                                    ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, SqlFileCheckAndSetDoesExistPassedFileNameResult);
                                    SqlFileCheckAndSetDoesExistPassedFileNameResult = (int)DatabaseControl.ResultOsError;
                                    FileStatusAux.iFileDoesExist = (int)DatabaseControl.ResultDoesNotExist;
                                } finally {
                                    if (SqlFileCheckAndSetDoesExistPassedFileNameResult == (int)DatabaseControl.ResultOperationInProgress) {
                                        SqlFileCheckAndSetDoesExistPassedFileNameResult = (int)DatabaseControl.ResultDatabaseError;
                                    }
                                    if ((ColInfoDbPassed.UseMethod & (int)(DatabaseControl.UseErSingleResult | DatabaseControl.UseErSchemaOnly | DatabaseControl.UseErKeyINFO | DatabaseControl.UseErSingleROW | DatabaseControl.UseErSequentialAccess)) != 0) {
                                        if (!DbIo.SqlDbDataReaderObject.IsClosed) {
                                            if (bPassedSqlFileDoClose) { DbIo.SqlDbDataReaderObject.Close(); }
                                        }
                                    }
                                } // Execute Command OK Try to access Reader ItemData
                                    #endregion
                                break;
                                #endregion
                            case (99):
                            default:
                                // Error
                                SqlFileCheckAndSetDoesExistPassedFileNameResult = (int)DatabaseControl.ResultUndefined;
                                FileStatusAux.FileBoolDoesExist = false;
                                ColInfoDbAux.RowCount = 0;
                                FileStatusAux.FileIsOpen = false;
                                sLocalErrorMessage = "The Sql Command Behavior (Mehtod of reading the file) has not been set)";
                                throw new NotSupportedException(sLocalErrorMessage);
                        } // Execute Correct Command for Reading Method FileUseMethod
                        DbIo.SqlDbCommandObject.ResetCommandTimeout();
                        if ((ColInfoDbPassed.UseMethod & (int)(DatabaseControl.UseErSingleResult | DatabaseControl.UseErSchemaOnly | DatabaseControl.UseErKeyINFO | DatabaseControl.UseErSingleROW | DatabaseControl.UseErSequentialAccess)) != 0) {
                            if (DbIo.SqlDbDataReaderObject != null) {
                                if (!DbIo.SqlDbDataReaderObject.IsClosed) {
                                    ColInfoDbPassed.CloseIsNeeded = true;  // Only used for research Benchmark Loop
                                }
                            }
                        }
                        #endregion
                        #region catch errors on Read Mode
                    } catch (SqlException ExceptionSql) {
                        sLocalErrorMessage = "";
                        ExceptSql(sLocalErrorMessage, ref ExceptionSql, SqlFileCheckAndSetDoesExistPassedFileNameResult);
                        SqlFileCheckAndSetDoesExistPassedFileNameResult = (int)DatabaseControl.ResultDatabaseError;
                        FileStatusAux.FileDoesExist = false;
                        ColInfoDbAux.RowCount = (int)DatabaseControl.ResultDoesNotExist;
                        FileStatusAux.FileIsOpen = false;
                    } catch (Exception ExceptionGeneral) {
                        sLocalErrorMessage = "";
                        ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, SqlFileCheckAndSetDoesExistPassedFileNameResult);
                        SqlFileCheckAndSetDoesExistPassedFileNameResult = (int)DatabaseControl.ResultOsError;
                        FileStatusAux.FileBoolDoesExist = false;
                        ColInfoDbAux.RowCount = (int)DatabaseControl.ResultDoesNotExist;
                        FileStatusAux.FileIsOpen = false;
                    } finally {
                        // DbIo.SqlDbCommandObject = null;
                        // SqlFileCheckAndSetDoesExistPassedFileNameResult = ColInfoDbAux.RowCount;
                    }
                    // If File is NOT Open Try to Select the File Name in the Master File
                        #endregion
                    #endregion
                    #region Handle results of probing
                    if (ColInfoDbPassed.HasRows) {
                        FileStatusAux.iFileDoesExist = (int)DatabaseControl.ResultDoesExist;
                        FileStatusAux.FileBoolDoesExist = true;
                    } else {
                        FileStatusAux.iFileDoesExist = (int)DatabaseControl.ResultDoesNotExist;
                        FileStatusAux.FileBoolDoesExist = false;
                        FileStatusAux.FileIsOpen = false;
                    }
                }
                // 
                if (DbStatus.bpConnIsConnected && PassedDbConnection != null) {
                    // <Area Id = "Close connected">
                    if (bPassedConnDoClose) {
                        SqlFileCheckAndSetDoesExistPassedFileNameResult = ConnClose(ref PassedDbConnection, ref sPassedFileName, ref sPassedFileNameFull);
                        if (ColInfoDbAux.RowCount > 0) {
                            SqlFileCheckAndSetDoesExistPassedFileNameResult = (int)DatabaseControl.ResultDoesExist;
                        }
                    }
                }
                if (DbStatus.bpConnDoDispose && PassedDbConnection != null) {
                    // <Area Id = "Dispose connected">
                    PassedDbConnection = null;
                }
            } else {
                FileStatusAux.iFileDoesExist = (int)DatabaseControl.ResultFailed;
                FileStatusAux.FileBoolDoesExist = false;
                FileStatusAux.FileIsOpen = false;
            }
                    #endregion
            FileStatusAux.StatusCurrent = FileStatusAux.iFileDoesExist;
            return SqlFileCheckAndSetDoesExistPassedFileNameResult;
        }
        // <Section Id = "SqlFileReset">
        public long SqlFileReset(String sPassedFileName, String sPassedFileNameFull) {
            iSqlFileReset = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            // FileStatus.FileIsInitialized
            // if (FileStatus.FileIsInitialized) {
            // iSqlFileOpenPassedFileName = SqlFileReset(sPassedFileName, sPassedFileNameFull);
            // THIS IS A DISPOSE FUNCTION
            FileStatus.FileIsInitialized = false;

            // }
            return iSqlFileReset;
        }
        // Sql Command Execution - xxxxxxxxxxxxxxxxxxxxxxx
        #region SqlFileCommandExecute
        // <Section Id = "SqlCommandExecute">
        public long SqlCommandExecute(String OutputCommandPassed) {
            iSqlCommandExecute = (int)DatabaseControl.ResultStarted;
            DbIo.CommandCurrent = OutputCommandPassed;
            // command
            // command
            DbIo.SqlDbCommandObject = new SqlCommand(OutputCommandPassed, DbIo.SqlDbConnection);
            iSqlCommandExecute = (int)DatabaseControl.ResultOperationInProgress;
            try {
                iSqlCommandExecute = DbIo.SqlDbCommandObject.ExecuteNonQuery();
            } catch (SqlException ExceptionSql) {
                sLocalErrorMessage = "";
                ExceptSql(sLocalErrorMessage, ref ExceptionSql, iSqlCommandExecute);
                FileStatus.FileDoesExist = false;
            } catch (Exception ExceptionGeneral) {
                sLocalErrorMessage = "";
                ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iSqlCommandExecute);
                FileStatus.FileDoesExist = false;
            } finally {
                DbIo.SqlDbCommandObject = null;
            }
            return iSqlCommandExecute;
        }
        #endregion
        #endregion
        #region SqlFileOpen
        // <Section Id = "x
        public long SqlFileOpen(ref SqlConnection PassedDbConnection, String sPassedFileName, String sPassedFileNameFull) {
            iSqlFileOpenPassedFileName = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            if (FileId.FileNameFull.Length == 0) {
                FileId.FileNameFull = SqlFileNameBuildFull(ref FileSummary.spDatabaseName, ref FileSummary.spFileOwner, ref FileSummary.spFileName, ref FileId.spFileNameFull);
            }
            // Counts
            Buf.BytesRead = 0;
            Buf.BytesReadTotal = 0;
            Buf.BytesConverted = 0;
            Buf.BytesConvertedTotal = 0;
            // Open a connection to the database
            if (!DbStatus.bpConnIsConnected) {
                iSqlFileOpenPassedFileName = ConnOpen(ref PassedDbConnection, ref FileSummary.spFileName, ref FileId.spFileNameFull);
            }
            //  check if the sql exists
            if (!FileStatus.FileDoesExist || !FileStatus.FileIsOpen) {
                // TODO $ERROR SqlFileOpen Correct to leave open ???
                iSqlFileOpenPassedFileName = SqlFileCheckAndSetDoesExist(ref PassedDbConnection, FileSummary.FileName, FileId.FileNameFull, false, false, false, ref ColInfoDb, ref ColInfo);
                FileStatus.FileDoesExist = FileStatusAux.FileBoolDoesExist;
                FileStatus.iFileDoesExist = FileStatusAux.iFileDoesExist;
                FileStatus.FileIsOpen = FileStatusAux.FileIsOpen;
            } else {
                iSqlFileOpenPassedFileName = (int)DatabaseControl.ResultDoesExist;
            }
            // FileStatus.ipStatusCurrent = iSqlFileOpenPassedFileName;
            // FileStatus.FileIsOpen = true;
            // TODO $$$CHECK work on Sql File Exits (review for support files)
            if (FileStatus.FileDoesExist && iSqlFileOpenPassedFileName == (int)DatabaseControl.ResultDoesExist) {
                // the file does not exist
                iSqlFileOpenPassedFileName = (int)DatabaseControl.ResultDoesExist;
                // SqlFileDoesExist = true;
            } else {
                // the file exists and can be changed
                PassedDbConnection.Dispose();
                iSqlFileOpenPassedFileName = (int)DatabaseControl.ResultDoesNotExist;
                FileStatus.FileDoesExist = false;
            }

            return iSqlFileOpenPassedFileName;
        }
        // <Section Id = "x
        public long SqlFileOpen() {
            iSqlFileOpen = (int)DatabaseControl.ResultStarted;

            iSqlFileOpen = SqlFileOpen(ref DbIo.SqlDbConnection, FileSummary.FileName, FileId.FileNameFull);

            return iSqlFileOpen;
        }
        #endregion
        #region SqlFileClose
        // <Section Id = "x
        public long SqlFileClose(ref SqlConnection PassedDbConnection, Mfile ofPassedFileObject, String sPassedFileName, String sPassedFileNameFull) {
            SqlFileCloseResult = (int)DatabaseControl.ResultStarted;
            ofPassedFileObject.FileSummary.FileName = sPassedFileName;
            ofPassedFileObject.FileId.FileNameFull = sPassedFileNameFull;
            // close reader / writer
            //
            try {
                if (DbIo.SqlDbDataReaderObject != null) {
                    if (!DbIo.SqlDbDataReaderObject.IsClosed) { DbIo.SqlDbDataReaderObject.Close(); }
                }
            } catch { ; }
            // Close writer
            try {
                if (DbIo.SqlDbDataWriterObject != null) { DbIo.SqlDbDataWriterObject.Dispose(); }
            } catch { ; }
            // Close / dispose Command Adapter
            try {
                if (DbIo.SqlDbCommandAdapterObject != null) {
                    DbIo.SqlDbCommandAdapterObject.Dispose();
                    DbIo.SqlDbCommandAdapterObject = null;
                }
            } catch { ; }
            // close connection
            try {
                if (PassedDbConnection != null) {
                    if (PassedDbConnection.State.ToString() == "Open") {
                        iSqlColAddCmdBuild = ConnClose(ref PassedDbConnection, ref FileSummary.spFileName, ref FileId.spFileNameFull);
                    } else if (DbIo.SqlDbConnection.State.ToString() != "Open") {
                        iSqlColAddCmdBuild = ConnReset();
                        // iSqlColAddCmdBuild = ConnOpen(ref SqlConnection PassedDbConnection, FileName, FileId.FileNameFull);
                        // DbIo.SqlDbConnection.Close();
                    }
                }
            } catch { ; }
            //
            // reset all file control fields
            DbIo.CommandCurrent = null;
            //
            DbIo.SqlDbCommandObject = null;
            DbIo.SqlDbDataReaderObject = null;
            DbIo.SqlDbCommandAdapterObject = null;
            //
            DbStatus.bpConnDoClose = false;
            DbStatus.bpConnDoDispose = true;
            DbStatus.bpConnDoesExist = false;
            DbStatus.bpConnIsClosed = true;
            DbStatus.bpConnIsConnected = false;
            DbStatus.bpConnIsConnecting = false;
            DbStatus.bpConnIsCreated = false;
            DbStatus.bpConnIsCreating = false;
            DbStatus.bpConnIsInitialized = false;
            DbStatus.bpConnIsOpen = false;
            DbStatus.bpConnIsValid = false;

            DbStatus.bpDatabaseFileDoesExist = false;
            DbStatus.bpDatabaseFileIsCreated = false;
            DbStatus.bpDatabaseFileIsCreating = false;
            DbStatus.bpDatabaseFileIsInitialized = false;
            DbStatus.bpDatabaseFileIsInvalid = false;
            DbStatus.bpDatabaseFileNameIsValid = false;
            DbMasterSyn.bpDbFileFilePhraseSelectIsUsed = false;

            DbStatus.bpDbFileGroupDoesExist = false;
            PickRow.PdIndexDoSearch = false;

            FileStatus.bpFileBoolDoesExist = false;
            Item.ItemIdExists = false;
            FileStatus.bpFileIsInitialized = false;

            DbSyn.bpSqlColumnViewCmdFirst = false;
            FileStatus.FileDoClose = false;
            FileStatus.FileDoesExist = false;
            FileStatus.FileIsInitialized = false;
            FileStatus.FileIsOpen = false;
            FileStatus.FileKeepOpen = false;
            ColInfo.bHasRows = false;
            /*
            FileStatus.bpFileDoesExist = false;
            FileStatus.bpFileIsInitialized = false;
            FileStatus.bpFileIsOpen = false;
            */
            SqlFileCloseResult = (int)DatabaseControl.ResultOK;
            return SqlFileCloseResult;
        }
        #endregion
        #region SqlFileCreate
        // <Section Id = "x
        public long SqlFileCreate(String sPassedFileName, String sPassedFileNameFull) {
            SqlFileCreateResult = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            DbIo.CommandCurrent = "CREATE TABLE " + "'" + FileId.FileNameFull + "'";
            // command
            DbIo.SqlDbCommandObject = new SqlCommand(DbIo.CommandCurrent, DbIo.SqlDbConnection);
            SqlFileCreateResult = (int)DatabaseControl.ResultOperationInProgress;
            DbSyn.spSqlFileCreateCmd = "";
            try {
                SqlFileCreateResult = DbIo.SqlDbCommandObject.ExecuteNonQuery();
                if (SqlFileCreateResult > 0) {
                    FileStatus.FileDoesExist = true;
                    // Add Column 0
                    DbIo.CommandCurrent = DbSyn.spOutputAlterCommand + " " + "'" + FileId.FileNameFull + "'";
                    DbIo.CommandCurrent += " ADD 0 String ";
                    DbIo.CommandCurrent += " VARCHAR(512)";
                    DbIo.CommandCurrent += " { PRIMARY KEY }";
                    DbSyn.spSqlFileCreateCmd = DbIo.CommandCurrent;
                    DbIo.SqlDbCommandObject = new SqlCommand(DbSyn.spSqlFileCreateCmd, DbIo.SqlDbConnection);
                    SqlFileCreateResult = DbIo.SqlDbCommandObject.ExecuteNonQuery();
                    // Add Primary Key
                    // DbIo.CommandCurrent = " { PRIMARY KEY }";
                    // Add Unique
                    // DbIo.CommandCurrent = " { UNIQUE }";
                } else {
                    FileStatus.FileDoesExist = false;
                }
            } catch (SqlException ExceptionSql) {
                sLocalErrorMessage = "";
                ExceptSql(sLocalErrorMessage, ref ExceptionSql, SqlFileCreateResult);
                FileStatus.FileDoesExist = false;
            } catch (Exception ExceptionGeneral) {
                sLocalErrorMessage = "";
                ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, SqlFileCreateResult);
                FileStatus.FileDoesExist = false;
            } finally {
                DbIo.SqlDbCommandObject = null;
            }
            return SqlFileCreateResult;
        }
        #endregion
        #region SqlFileUpdate
        // <Section Id = "InsertValue">
        public long SqlFileDataInsert(String sPassedFileName, String sPassedFileNameFull) {
            SqlFileDataInsertResult = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            DbIo.CommandCurrent = "INSERT " + "'" + FileId.FileNameFull + "'";
            // command
            // DbSyn.spOutputItem += ColText;
            if (DbSyn.spOutputInsert.Length > 0) {
                DbSyn.spOutputInsertCommand = DbSyn.spOutputInsertPrefix + sPassedFileName + DbSyn.spOutputInsertPrefix1;
                DbSyn.spOutputInsertCommand += DbSyn.spOutputInsert + DbSyn.spOutputInsertSuffix;
            }
            //
            if (DbSyn.spOutputValues.Length > 0) {
                DbSyn.spOutputInsertCommand = DbSyn.spOutputInsertPrefix + sPassedFileName;
                DbSyn.spOutputInsertCommand += DbSyn.spOutputInsert + DbSyn.spOutputInsertSuffix;
            }
            //
            DbSyn.spOutputInsertCommand = DbSyn.spOutputInsert + "\n" + DbSyn.spOutputValues;
            DbIo.CommandCurrent = DbSyn.spOutputInsertCommand;
            LocalLongResult = SqlFileDataWrite(sPassedFileName, sPassedFileNameFull);

            return SqlFileDataInsertResult;
        }
        // <Section Id = "UpdateValue">
        public long SqlFileDataUpdate(String sPassedFileName, String sPassedFileNameFull) {
            SqlFileDataUpdateResult = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            DbIo.CommandCurrent = "UPDATE TABLE " + "'" + FileId.FileNameFull + "'";
            // command

            return SqlFileDataUpdateResult;
        }
        // <Section Id = "FileDataDelete">
        public long SqlFileDataDelete(String sPassedFileName, String sPassedFileNameFull) {
            SqlFileDataDeleteResult = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            DbIo.CommandCurrent = "DELETE " + "'" + FileId.FileNameFull + "'";
            // command

            return SqlFileDataDeleteResult;
        }
        // <Section Id = "FileDataAdd">
        public long SqlFileDataAdd(String sPassedFileName, String sPassedFileNameFull) {
            SqlFileDataAddResult = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            DbIo.CommandCurrent = "ALTER " + "'" + FileId.FileNameFull + "'";
            // command

            return SqlFileDataAddResult;
        }
        // <Section Id = "FileDataGet">
        #endregion
        #region SqlFileGetSet
        public long SqlFileDataGet(String sPassedFileName, String sPassedFileNameFull, String sPassedItemId) {
            SqlFileDataGetResult = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            Item.ItemId = sPassedItemId;
            DbIo.CommandCurrent = "SELECT * FROM " + "'" + FileSummary.FileName + "'";
            DbIo.CommandCurrent += "WHERE [name] = " + "'" + Item.ItemId + "'";
            // command

            return SqlFileDataGetResult;
        }
        // <Section Id = "FileDataWrite">
        public long SqlFileDataWrite(String sPassedFileName, String sPassedFileNameFull) {
            SqlFileDataWriteResult = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            DbIo.CommandCurrent = "CREATE TABLE " + "'" + FileId.FileNameFull + "'";
            // command
            DbIo.SqlDbCommandObject = new SqlCommand(DbIo.CommandCurrent, DbIo.SqlDbConnection);
            SqlFileDataWriteResult = (int)DatabaseControl.ResultOperationInProgress;
            try {
                SqlFileDataWriteResult = DbIo.SqlDbCommandObject.ExecuteNonQuery();
                if (SqlFileDataWriteResult > 0) {
                    FileStatus.FileDoesExist = true;
                    // Add Column 0
                    DbIo.CommandCurrent = "ALTER TABLE " + "'" + FileId.FileNameFull + "'";
                    DbIo.CommandCurrent += " ADD 0 String ";
                    DbIo.CommandCurrent += " VARCHAR(512)";
                    DbIo.CommandCurrent += " { PRIMARY KEY }";
                    DbIo.SqlDbCommandObject = new SqlCommand(DbIo.CommandCurrent, DbIo.SqlDbConnection);
                    SqlFileDataWriteResult = DbIo.SqlDbCommandObject.ExecuteNonQuery();
                    // Add Primary Key
                    // DbIo.CommandCurrent = " { PRIMARY KEY }";
                    // Add Unique
                    // DbIo.CommandCurrent = " { UNIQUE }";
                } else {
                    FileStatus.FileDoesExist = false;
                }
            } catch (SqlException ExceptionSql) {
                sLocalErrorMessage = "";
                ExceptSql(sLocalErrorMessage, ref ExceptionSql, SqlFileDataWriteResult);
                FileStatus.FileDoesExist = false;
            } catch (Exception ExceptionGeneral) {
                sLocalErrorMessage = "";
                ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, SqlFileDataWriteResult);
                FileStatus.FileDoesExist = false;
            } finally {
                DbIo.SqlDbCommandObject = null;
            }

            return SqlFileDataWriteResult;
        }
        #endregion
        #endregion
        #region SqlFileDict // Dictionary Handling // xxxxxxxxxx
        #region SqlFileDictProcess
        public long SqlFileDictProcessDb(String sPassedFileName, String sPassedFileNameFull, bool bPassedConnDoClose, bool bPassedConnDoDispose, bool bPassedSqlFileDoClose, RowInfoDef ColInfoDbPassed, ColInfoDef ColInfoPassed) {
            // TODO SqlFileDictProcessDb Needs work
            iSqlDictProcessDbPassedFileName = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            // DbStatus.bpConnDoClose = bPassedConnDoClose;
            // DbStatus.bpConnDoDispose = DbStatus.bpConnDoDispose;
            // SqlFileDoClose = bPassedSqlFileDoClose;

            iSqlDictProcessDbPassedFileName = (int)DatabaseControl.ResultOperationInProgress;

            ColInfoDbPassed.UseMethod = (int)DatabaseControl.UseErSingleResult;
            ColInfoDbPassed.CloseIsNeeded = false;

            // Row
            ColInfoDbPassed.HasRows = false;
            ColInfoDbPassed.RowContinue = false;
            ColInfoDbPassed.RowIndex = 0;
            ColInfoDbPassed.RowCount = 0;
            // Clr Native
            ColInfoDbPassed.RowMax = PickRowDef.PdIndexMax;
            System.Object[] osoaThisGetValues = new System.Object[ColArrayMax];
            System.Object osoThisGetValue;
            // Sql
            // ipRowIndex = 0;
            // System.Object[] RowArray;
            // System.Object osoThisGetSqlValue;

            // File Row Colomns
            ColInfoDbPassed.HasColumns = false;
            ColInfoDbPassed.ColumnContinue = false;
            ColInfoDbPassed.ColumnMax = PickRowDef.PdIndexMax;
            // File Column Fields
            // Action
            // ColInfoPassed.ColAction;
            // File Level
            // ColInfoPassed.FileUseIndexName;
            // ColInfoPassed.FileIndex;
            // ColInfoPassed.FileIndexName;
            // ColInfoPassed.FileCount;
            // Row
            // ColInfoPassed.iRowIndex;
            // ColInfoPassed.iRowCount;
            // ColInfoPassed.sRowIndexName;
            // ColInfoPassed.tdtRowLastTouched;
            //
            // ColInfoPassed.bHasRows;
            // ColInfoPassed.bRowContinue;
            // ColInfoPassed.iRowMax;
            // Column
            // ColInfoPassed.ColIndex;
            // ColInfoPassed.ColCount;
            // ColInfoPassed.sColIndexName;
            // ColInfoPassed.tdtIndexLastTouched;
            // ColInfoPassed.ColCountVisible;
            // ColInfoPassed.ColCountHidden;
            // ColInfoPassed.ColumnMax;

            // Reset Column Processing
            sTemp0 = "";
            iSqlDictProcessDbPassedFileName = SqlColAction(ref DbIo.SqlDbDataReaderObject, ref DbIo.SqlDbDataWriterObject, ref ColInfoDb, ref ColInfo, false, false, ColInfoDef.SFC_RESET, ref sTemp0, 0, 0);
            //
            System.Type tThisTempType;
            DbFileTemp.ooThisTempObject = null;
            DbFileTemp.sThisTempString = "";
            DbFileTemp.iThisTempInt = 0;
            DbFileTemp.bThisTempBool = false;
            DbFileTemp.ooTmp = null;
            DbFileTemp.ooThis = null;

            System.Data.SqlTypes.SqlString tsdssThisTempSqlString;

            // This is the test code for performance evaluation on each command type
            // Will require the addition of sql timer functions.
            // Normal usage should be execute schalar.

            // SqlConnection myConnection = new SqlConnection(myConnectionString);
            // SqlCommand myCommand = new SqlCommand(mySelectQuery, myConnection);
            // DbIo.SqlDbCommandObject.ExecuteReader();
            // DbIo.SqlDbCommandObject.ExecuteReader(new CommandBehavior()); == CommandBehavior.Default
            // SqlDataReader myReader = DbIo.SqlDbCommandObject.ExecuteReader(CommandBehavior.CloseConnection);
            // SqlDataReader myReader = DbIo.SqlDbCommandObject.ExecuteReader(CommandBehavior.KeyInfo);
            // SqlDataReader myReader = DbIo.SqlDbCommandObject.ExecuteReader(CommandBehavior.SchemaOnly);
            // SqlDataReader myReader = DbIo.SqlDbCommandObject.ExecuteReader(CommandBehavior.SequentialAccess);
            // SqlDataReader myReader = DbIo.SqlDbCommandObject.ExecuteReader(CommandBehavior.SingleResult);
            // SqlDataReader myReader = DbIo.SqlDbCommandObject.ExecuteReader(CommandBehavior.SingleRow);

            // When CommandBehavior.CloseConnection
            // Implicitly closes the connection because 
            // CommandBehavior.CloseConnection was specified.
            // otherwise:

            // Int32 count = (int32)cmd.ExecuteScalar();

            iSqlDictProcessDbPassedFileName = (int)DatabaseControl.ResultOperationInProgress;
            FileStatusAux.FileBoolDoesExist = false;
            ColInfoDbAux.RowCount = (int)DatabaseControl.ResultDoesNotExist;
            if (FileId.FileNameFull.Length == 0) {
                FileId.FileNameFull = SqlFileNameBuildFull(ref FileSummary.spDatabaseName, ref FileSummary.spFileOwner, ref FileSummary.spFileName, ref FileId.spFileNameFull);
            }
            //  check Connection
            if (!DbStatus.bpConnIsConnected || DbIo.SqlDbConnection == null) {
                // <Area Id = "WARNING - Already connected">
                iSqlDictProcessDbPassedFileName = ConnOpen(ref DbIo.SqlDbConnection);
            }
            if (iSqlDictProcessDbPassedFileName == (int)DatabaseControl.ResultOK || iSqlDictProcessDbPassedFileName == (int)DatabaseControl.ResultOperationInProgress) {
                for (ColInfoDbPassed.UseMethod = 1; ColInfoDbPassed.UseMethod < 256; ColInfoDbPassed.UseMethod <<= 1) {
                    iTemp = (ColInfoDbPassed.UseMethod & (int)(DatabaseControl.UseErSingleResult | DatabaseControl.UseErSchemaOnly | DatabaseControl.UseErKeyINFO | DatabaseControl.UseErSingleROW | DatabaseControl.UseErSequentialAccess));
                    iTemp = ColInfoDbPassed.UseMethod << 4;
                    iTemp = ColInfoDbPassed.UseMethod & (int)(DatabaseControl.UseErSingleResult | DatabaseControl.UseErSchemaOnly | DatabaseControl.UseErKeyINFO | DatabaseControl.UseErSingleROW | DatabaseControl.UseErSequentialAccess);
                    if ((ColInfoDbPassed.UseMethod & (int)(DatabaseControl.UseErSingleResult | DatabaseControl.UseErSchemaOnly | DatabaseControl.UseErKeyINFO | DatabaseControl.UseErSingleROW | DatabaseControl.UseErSequentialAccess)) != 0) {
                        if (DbIo.SqlDbDataReaderObject != null) {
                            if (!DbIo.SqlDbDataReaderObject.IsClosed) {
                                if (RowInfo.CloseIsNeeded) { DbIo.SqlDbDataReaderObject.Close(); }
                            }
                        }
                        RowInfo.CloseIsNeeded = false;
                    }
                    if (!FileStatus.FileIsOpen) {
                        iSqlDictProcessDbPassedFileName = (int)DatabaseControl.ResultOperationInProgress;
                        try {
                            DbIo.CommandCurrent = "";
                            // DbIo.CommandCurrent += "USE [" + FileSummary.DatabaseName + "]; ";
                            DbIo.CommandCurrent += "SELECT * FROM sys.databases ";
                            DbIo.CommandCurrent += "WHERE [name] NOT IN ";
                            DbIo.CommandCurrent += "( ";
                            DbIo.CommandCurrent += "'master' , 'msdb', 'model', 'tempdb', ";
                            DbIo.CommandCurrent += "'resource', 'distribution' ";
                            DbIo.CommandCurrent += ")";
                            // DbIo.CommandCurrent += ";";

                            // DbIo.CommandCurrent = "USE [" + FileSummary.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FileSummary.FileName + "'";
                            // DbIo.CommandCurrent = "USE[" + FileSummary.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FileId.FileNameFull + "'";
                            // DbIo.CommandCurrent = "USE[" + FileSummary.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FileId.FileNameFull + "';";
                            // DbIo.CommandCurrent = "USE[" + FileSummary.DatabaseName + "]; SELECT * FROM sys.objects WHERE name = " + "'" + FileId.FileNameFull + "';";
                            // SQL = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=MyTable";
                            // int result = this.ExecuteQuery("if exists(select * from sys.databases where name = {0}", DatabaseName) return 1 else 0");
                            // \r\n"; FROM INFORMATION_SCHEMA.TABLES


                            ColInfoDbAux.RowCount = (int)DatabaseControl.ResultDoesNotExist;
                            DbIo.SqlDbCommandObject = null;
                            DbIo.SqlDbCommandObject = new SqlCommand(DbIo.CommandCurrent, DbIo.SqlDbConnection);
                            DbIo.SqlDbCommandObject.CommandTimeout = DbIo.SqlDbCommandTimeout;
                            DbIo.SqlDbCommandObject.CommandType = CommandType.Text;
                            switch (ColInfoDbPassed.UseMethod) {
                                case ((int)DatabaseControl.UseExecuteNoQuery):
                                    // Not appropriate for Check Does Exist
                                    // no row or columns
                                    // used for create, settings, etc
                                    try {
                                        ColInfoDbAux.RowCount = DbIo.SqlDbCommandObject.ExecuteNonQuery();
                                        // Console.WriteLine(DbIo.SqlDbDataReaderObject.sGetString(0));
                                    } finally {
                                    }
                                    break;
                                case ((int)DatabaseControl.UseErDefault):
                                case ((int)DatabaseControl.UseErSingleROW):
                                case ((int)DatabaseControl.UseErSingleResult):
                                case ((int)DatabaseControl.UseErKeyINFO):
                                case ((int)DatabaseControl.UseErSchemaOnly):
                                case ((int)DatabaseControl.UseExecuteScalar):
                                    // DbIo.SqlDbDataReaderObject.
                                    DbIo.SqlDbDataReaderObject = null;
                                    DbIo.SqlDbDataWriterObject = null;
                                    switch (ColInfoDbPassed.UseMethod) {
                                        case ((int)DatabaseControl.UseExecuteScalar):
                                            // Scalar = 1 Row 1 Column, no reader
                                            try {
                                                DbFileTemp.ooThisTempObject = DbIo.SqlDbCommandObject.ExecuteScalar();
                                                // Console.WriteLine(DbIo.SqlDbDataReaderObject.sGetString(0));
                                            } finally {
                                            }
                                            break;
                                        case ((int)DatabaseControl.UseErDefault):
                                        // All Rows All Columns
                                        case ((int)DatabaseControl.UseErSingleROW):
                                        // Single Row
                                        case ((int)DatabaseControl.UseErKeyINFO):
                                        // Column and Primary Key info
                                        case ((int)DatabaseControl.UseErSchemaOnly):
                                        // Column info
                                        case ((int)DatabaseControl.UseErSingleResult):
                                            // Single Result Set
                                            DbIo.SqlDbDataReaderObject = DbIo.SqlDbCommandObject.ExecuteReader((CommandBehavior)ColInfoDbPassed.UseMethod);
                                            // RowInfo.CloseIsNeeded = true;
                                            break;
                                        default:
                                            iSqlDictProcessDbPassedFileName = (int)DatabaseControl.ResultUndefined;
                                            sLocalErrorMessage = "The Sql Command Behavior (Mehtod of reading the file) has not been set)";
                                            throw new NotSupportedException(sLocalErrorMessage);
                                    }
                                    // DbIo.SqlDbCommandObject.Container.Components.Count;
                                    try {
                                        ColInfoDbPassed.RowIndex = 0;
                                        ColInfoDbAux.RowCount = -1;
                                        ColInfoDbPassed.HasRows = DbIo.SqlDbDataReaderObject.HasRows;
                                        ColInfo.ColCount = DbIo.SqlDbDataReaderObject.FieldCount;

                                        // only applies to db changes:
                                        // ColInfoDbAux.RowCount = DbIo.SqlDbDataReaderObject.RecordsAffected;
                                        tThisTempType = DbIo.SqlDbDataReaderObject.GetType();
                                        //
                                        if (ColInfoDbPassed.HasRows) {
                                            try {
                                                // PROCESS ROWS
                                                // bRead Loop
                                                // while (DbIo.SqlDbDataReaderObject.bNextResult()) (
                                                // For Each Loop 
                                                // PROCESS ROW
                                                // }
                                                ColInfoDbPassed.RowCount = 0;
                                                // ColInfoDbPassed.RowIndex = 0;
                                                ColInfoDbPassed.RowContinue = true;
                                                for (ColInfoDbPassed.RowIndex = 0; ColInfoDbPassed.RowContinue && ColInfoDbPassed.RowIndex < ColInfoDbPassed.RowMax; ColInfoDbPassed.RowIndex++) {
                                                    //
                                                    // Create Array to accept DataReader Row.
                                                    // GetSql    Array of Sql Values
                                                    // if (ColInfoDbPassed.RowIndex >= RowArray.Count()) {
                                                    // RowArray = new System.Object[ColInfoDbPassed.RowIndex + 5];
                                                    // }
                                                    // DbIo.SqlDbDataReaderObject.bNextResult();
                                                    // DbIo.SqlDbDataReaderObject.iGetSqlValues(RowArray);
                                                    //
                                                    // GetNative Array of Native Values
                                                    // DbIo.SqlDbDataReaderObject.bNextResult();
                                                    ColInfoDbPassed.ColumnContinue = DbIo.SqlDbDataReaderObject.Read();
                                                    if (ColInfoDbPassed.ColumnContinue) {
                                                        ColInfoAux.ColCount = DbIo.SqlDbDataReaderObject.GetValues(osoaThisGetValues);
                                                        ColInfoDbAux.RowCount++;
                                                        //
                                                        // ageGroup = age < 2 ? "Infant" 
                                                        //     : age < 19 ? "Teen" 
                                                        //     : age < 30 ? "Middle aged" 
                                                        //     : "old";
                                                        // xxxx ooThis = (ooTmp = DbIo.SqlDbDataReaderObject.ooGetSqlValue(ColInfoDbPassed.RowIndex)) != null ? ooTmp : null;
                                                        // if (ThisGetValueObject != null) {
                                                        //
                                                        // PROCESS DATA COLUMNS
                                                        // Result Set Row Column Metadata
                                                        ColInfo.ColCount = DbIo.SqlDbDataReaderObject.FieldCount;
                                                        ColInfo.ColCountVisible = DbIo.SqlDbDataReaderObject.VisibleFieldCount;
                                                        RowInfoAux.ColumnContinue = true;
                                                        // foreach (System.Object osoCurrGetSqlValues in RowArray) {
                                                        // PROCESS COLUMN
                                                        // }
                                                        for (ColInfoAux.ColIndex = 0; RowInfoAux.ColumnContinue && ColInfoAux.ColIndex < ColInfo.ColCount; ColInfoAux.ColIndex++) {
                                                            // DbIo.SqlDbDataReaderObject. PROCESS DETAILS
                                                            // DbIo.SqlDbDataReaderObject.

                                                            // ColInfoPassed.iGetOrdinal = DbIo.SqlDbDataReaderObject.iGetOrdinal("xxx");
                                                            // ColInfoAux.ColIndex = DbIo.SqlDbDataReaderObject.iGetOrdinal(ColInfoPassed.iGetName);
                                                            ColInfoPassed.iGetName = DbIo.SqlDbDataReaderObject.GetName(ColInfoAux.ColIndex);
                                                            ColInfoPassed.iGetOrdinal = DbIo.SqlDbDataReaderObject.GetOrdinal(ColInfoPassed.iGetName);
                                                            ColInfoPassed.sGetDataTypeName = DbIo.SqlDbDataReaderObject.GetDataTypeName(ColInfoAux.ColIndex);
                                                            ColInfoPassed.ttGetFieldType = DbIo.SqlDbDataReaderObject.GetFieldType(ColInfoAux.ColIndex);
                                                            // ColInfoPassed.tfdtGetSchemaTable = DbIo.SqlDbDataReaderObject.GetSchemaTable();
                                                            ColInfoPassed.bIsDBNull = DbIo.SqlDbDataReaderObject.IsDBNull(ColInfoAux.ColIndex);

                                                            // GetSql
                                                            // ooThisTempObject = DbIo.SqlDbDataReaderObject.ooGetSqlValue(ColInfoAux.ColIndex);
                                                            // tsdssThisTempSqlString = DbIo.SqlDbDataReaderObject.sGetSqlString(ColInfoAux.ColIndex);

                                                            // GetNative
                                                            ColInfoPassed.ooGetValue = DbIo.SqlDbDataReaderObject.GetValue(ColInfoAux.ColIndex);
                                                            sTemp0 = DbIo.SqlDbDataReaderObject.GetName(ColInfoAux.ColIndex);
                                                            iSqlDictProcessDbPassedFileName = SqlColAction(ref DbIo.SqlDbDataReaderObject, ref DbIo.SqlDbDataWriterObject, ref ColInfoDb, ref ColInfo, false, false, ColInfoDef.SFC_GET_NATIVE_VALUE, ref sTemp0, ColInfoAux.ColIndex, ColInfo.ColCount);
                                                            // ColInfo.ColCount++;
                                                            if (ColInfoAux.ColIndex >= ColInfo.ColCount) {
                                                                RowInfoAux.ColumnContinue = false;
                                                            }
                                                            sTemp0 = "ColomnUpdate";
                                                            iSqlDictProcessDbPassedFileName = SqlColAction(ref DbIo.SqlDbDataReaderObject, ref DbIo.SqlDbDataWriterObject, ref ColInfoDb, ref ColInfo, false, false, ColInfoDef.SFC_SET_COLUMN, ref sTemp0, ColInfoAux.ColIndex, ColInfo.ColCount);
                                                            // Console.WriteLine(DbIo.SqlDbDataReaderObject.sGetString(0));
                                                        } // Column Loop
                                                    } // Row Continue
                                                }// Row Loop
                                            } catch (SqlException ExceptionSql) {
                                                sLocalErrorMessage = "";
                                                ExceptSql(sLocalErrorMessage, ref ExceptionSql, iSqlDictProcessDbPassedFileName);
                                                // if (I am a serious error) {
                                                // iSqlDictProcessDbPassedFileName = (int) DatabaseControl.ResultDatabaseError;
                                                // FileStatusAux.iFileDoesExist = (int) DatabaseControl.ResultDoesNotExist;
                                                // } else {
                                                //
                                                // }
                                            } catch (Exception ExceptionGeneral) {
                                                sLocalErrorMessage = "";
                                                ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iSqlDictProcessDbPassedFileName);
                                                // if (I am a serious error) {
                                                // iSqlDictProcessDbPassedFileName = (int) DatabaseControl.ResultOsError;
                                                // FileStatusAux.iFileDoesExist = (int) DatabaseControl.ResultDoesNotExist;
                                                // } else {
                                                //
                                                // }
                                            } finally {
                                                sTemp0 = "RowUpdate";
                                                iSqlDictProcessDbPassedFileName = SqlColAction(ref DbIo.SqlDbDataReaderObject, ref DbIo.SqlDbDataWriterObject, ref ColInfoDbPassed, ref ColInfoPassed, false, false, ColInfoDef.SFC_SET_ROW, ref sTemp0, ColInfoAux.ColIndex, ColInfo.ColCount);
                                                if (ColInfoDbAux.RowCount > 0) {
                                                    iSqlDictProcessDbPassedFileName = (int)DatabaseControl.ResultDoesExist;
                                                    sLocalErrorMessage = "File Name Does not exist in Sql File Dict Process Db";
                                                    XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlDictProcessDbPassedFileName, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                                                }
                                            } // HasRow Try Row Loop Reading
                                        } // HasRows
                                    } catch (SqlException ExceptionSql) {
                                        sLocalErrorMessage = "";
                                        ExceptSql(sLocalErrorMessage, ref ExceptionSql, iSqlDictProcessDbPassedFileName);
                                        iSqlDictProcessDbPassedFileName = (int)DatabaseControl.ResultDatabaseError;
                                        FileStatusAux.iFileDoesExist = (int)DatabaseControl.ResultDoesNotExist;
                                    } catch (Exception ExceptionGeneral) {
                                        sLocalErrorMessage = "";
                                        ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iSqlDictProcessDbPassedFileName);
                                        iSqlDictProcessDbPassedFileName = (int)DatabaseControl.ResultOsError;
                                        FileStatusAux.iFileDoesExist = (int)DatabaseControl.ResultDoesNotExist;
                                    } finally {
                                        if ((ColInfoDbPassed.UseMethod & (int)(DatabaseControl.UseErSingleResult | DatabaseControl.UseErSchemaOnly | DatabaseControl.UseErKeyINFO | DatabaseControl.UseErSingleROW | DatabaseControl.UseErSequentialAccess)) != 0) {
                                            if (!DbIo.SqlDbDataReaderObject.IsClosed) {
                                                if (bPassedSqlFileDoClose) { DbIo.SqlDbDataReaderObject.Close(); }
                                            }
                                        }
                                    } // Execute Command OK Try to access Reader ItemData
                                    break;
                                case (99):
                                default:
                                    // Error
                                    iSqlDictProcessDbPassedFileName = (int)DatabaseControl.ResultUndefined;
                                    FileStatusAux.FileBoolDoesExist = false;
                                    ColInfoDbAux.RowCount = 0;
                                    FileStatusAux.FileIsOpen = false;
                                    sLocalErrorMessage = "The Sql Command Behavior (Mehtod of reading the file) has not been set)";
                                    throw new NotSupportedException(sLocalErrorMessage);
                            } // Execute Correct Command for Reading Method FileUseMethod
                            DbIo.SqlDbCommandObject.ResetCommandTimeout();
                            if ((ColInfoDbPassed.UseMethod & (int)(DatabaseControl.UseErSingleResult | DatabaseControl.UseErSchemaOnly | DatabaseControl.UseErKeyINFO | DatabaseControl.UseErSingleROW | DatabaseControl.UseErSequentialAccess)) != 0) {
                                if (DbIo.SqlDbDataReaderObject != null) {
                                    if (!DbIo.SqlDbDataReaderObject.IsClosed) {
                                        RowInfo.CloseIsNeeded = true;  // Only used for research Benchmark Loop
                                    }
                                }
                            }
                        } catch (SqlException ExceptionSql) {
                            sLocalErrorMessage = "";
                            ExceptSql(sLocalErrorMessage, ref ExceptionSql, iSqlDictProcessDbPassedFileName);
                            iSqlDictProcessDbPassedFileName = (int)DatabaseControl.ResultDatabaseError;
                            FileStatus.FileDoesExist = false;
                            ColInfoDbAux.RowCount = (int)DatabaseControl.ResultDoesNotExist;
                            FileStatusAux.FileIsOpen = false;
                        } catch (Exception ExceptionGeneral) {
                            sLocalErrorMessage = "";
                            ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iSqlDictProcessDbPassedFileName);
                            iSqlDictProcessDbPassedFileName = (int)DatabaseControl.ResultOsError;
                            FileStatusAux.FileBoolDoesExist = false;
                            ColInfoDbAux.RowCount = (int)DatabaseControl.ResultDoesNotExist;
                            FileStatusAux.FileIsOpen = false;
                        } finally {
                            // DbIo.SqlDbCommandObject = null;
                            // iSqlDictProcessDbPassedFileName = ColInfoDbAux.RowCount;
                        } // If File is NOT Open Try to Select the File Name in the Master File
                        // 
                        if (ColInfoDbAux.RowCount == (int)DatabaseControl.ResultDoesNotExist) {
                            FileStatusAux.iFileDoesExist = (int)DatabaseControl.ResultDoesNotExist;
                            FileStatusAux.FileBoolDoesExist = false;
                            // ColInfoDbAux.RowCount = 0;
                            FileStatusAux.FileIsOpen = false;
                        } else if (ColInfoDbAux.RowCount >= 0) {
                            FileStatusAux.iFileDoesExist = (int)DatabaseControl.ResultDoesExist;
                            FileStatusAux.FileBoolDoesExist = true;
                        } else {
                            FileStatusAux.iFileDoesExist = (int)DatabaseControl.ResultDoesNotExist;
                            FileStatusAux.FileBoolDoesExist = false;
                            FileStatusAux.FileIsOpen = false;
                        }
                        if (!FileStatusAux.FileBoolDoesExist) {
                            sLocalErrorMessage = "File Name Does not exist in Sql File Dict Process Db";
                            XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlDictProcessDbPassedFileName, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                        }
                    }
                }
                // 
                if (DbStatus.bpConnIsConnected && DbIo.SqlDbConnection != null) {
                    // <Area Id = "Close connected">
                    if (bPassedConnDoClose) {
                        iSqlDictProcessDbPassedFileName = ConnClose(ref DbIo.SqlDbConnection, ref sPassedFileName, ref sPassedFileNameFull);
                        if (ColInfoDbAux.RowCount > 0) {
                            iSqlDictProcessDbPassedFileName = (int)DatabaseControl.ResultDoesExist;
                        }
                    }
                }
                if (DbStatus.bpConnDoDispose && DbIo.SqlDbConnection != null) {
                    // <Area Id = "Dispose connected">
                    DbIo.SqlDbConnection = null;
                }
            } else {
                FileStatusAux.iFileDoesExist = (int)DatabaseControl.ResultFailed;
                FileStatusAux.FileBoolDoesExist = false;
                FileStatusAux.FileIsOpen = false;
            }
            FileStatus.StatusCurrent = FileStatusAux.iFileDoesExist;
            return iSqlDictProcessDbPassedFileName;
        }
        // <Section Id = "x
        // type_name[({precision[.scale]})][NULL|NOT NULL]
        // DbIo.CommandCurrent += type_name + "(" + precision + "." + scale + ")";
        // DbIo.CommandCurrent += type_name;
        //
        // DbIo.CommandCurrent += " NULL";
        // DbIo.CommandCurrent += " NOT NULL";
        //
        // {DROP DEFAULT 
        // | SET DEFAULT constant_expression 
        //
        // DbIo.CommandCurrent += " SET DEFAULT " + constant_expression;
        //
        // | IDENTITY [ ( seed , increment ) ]
        // DbIo.CommandCurrent += " IDENTITY (" + seed + ", " + increment + ")";
        // DbIo.CommandCurrent += " IDENTITY";
        //
        // Constraints
        // DbIo.CommandCurrent += " CONSTRAINT " + constraint_name;
        // Add Primary Key
        // DbIo.CommandCurrent += " PRIMARY KEY";
        // Add Unique
        // DbIo.CommandCurrent += " UNIQUE";
        //
        // | REFERENCES ref_table [ (ref_column) ] 
        // [ ON DELETE { CASCADE | NO ACTION | SET DEFAULT |SET NULL } ] 
        // [ ON UPDATE { CASCADE | NO ACTION | SET DEFAULT |SET NULL } ]
        //
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        /*
        ALTER TABLE table_name
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        ALTER COLUMN column_name 
           {
            type_name[({precision[.scale]})][NULL|NOT NULL]
           {DROP DEFAULT 
           | SET DEFAULT constant_expression 
           | IDENTITY [ ( seed , increment ) ]
           } 
        | ADD 
           { < column_definition > | < table_constraint > } [ ,...n ] 
        | DROP 
           { [ CONSTRAINT ] constraint_name 
           | COLUMN column }
        ] }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        < column_definition > ::= 
           { column_name data_type } 
           [ [ DEFAULT constant_expression ] 
              | IDENTITY [ ( seed , increment ) ] 
           ] 
           [ROWGUIDCOL]
           [ < column_constraint > ] [ ...n ] ]
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        < column_constraint > ::= 
           [ NULL | NOT NULL ] 
           [ CONSTRAINT constraint_name ] 
           { 
              | { PRIMARY KEY | UNIQUE } 
              | REFERENCES ref_table [ (ref_column) ] 
              [ ON DELETE { CASCADE | NO ACTION | SET DEFAULT |SET NULL } ] 
              [ ON UPDATE { CASCADE | NO ACTION | SET DEFAULT |SET NULL } ]
           }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        < table_constraint > ::= 
           [ CONSTRAINT constraint_name ] 
           { [ { PRIMARY KEY | UNIQUE } 
              { ( column [ ,...n ] ) } 
              | FOREIGN KEY 
                ( column [ ,...n ] )
                REFERENCES ref_table [ (ref_column [ ,...n ] ) ] 
              [ ON DELETE { CASCADE | NO ACTION | SET DEFAULT |SET NULL } ] 
              [ ON UPDATE { CASCADE | NO ACTION | SET DEFAULT |SET NULL } ] 
           }
        */
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        protected internal long SqlColAddCmdBuild(
            ref SqlDataReader ofddrPassedSqlDbDataReader,
            ref SqlDataAdapter ofddrPassedSqlDbDataWriter,
            RowInfoDef ColInfoDbPassed,
            ColInfoDef ColInfoPassed,
            bool bPassedUsePrimary, bool bPassedUseIndexName,
            int iPassedColAction,
            String sPassedSqlColumnBuildOption,
            int iPassedIndex, int iPassedCount
            ) {
            Meta.UsePrimary = bPassedUsePrimary;
            //
            iSqlColAddCmdBuild = (int)DatabaseControl.ResultStarted;
            // command
            iSqlColAddCmdBuild = (int)DatabaseControl.ResultOperationInProgress;
            DbSyn.spSqlColumnAddCmd = "";
            String sSqlColumnBuildOption = sPassedSqlColumnBuildOption;
            PickRow.PickDictArray[iPassedIndex].sColumnAdd = "";
            PickRow.PickDictArray[iPassedIndex].sColumnView = "";
            try {
                // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                // Create Table

                if (PickRow.PickDictArray[PickRow.PdIndex].iAttrType == ColTypeDef.ITEM_ISDICT) {
                    if (PickRow.PickDictArray[iPassedIndex].ItemIdFoundNumericPk) {
                        // Add Item Column Id
                        // sSqlColumnBuildOption
                        DbSyn.spOutputAlterColumnName = "c" + (PickRow.PickDictArray[iPassedIndex].ItemId + 0).ToString();
                        if (DbSyn.spOutputAlterColumnName.Length == 0) { DbSyn.spOutputAlterColumnName = PickRow.PickDictArray[iPassedIndex].ItemIdConverted; }
                        // spSqlColumnAddCmd += DbSyn.spOutputAlterVerb + " " + DbSyn.spOutputAlterColumnName;
                        DbSyn.spSqlColumnAddCmd += DbSyn.spOutputAlterColumnName;
                        //
                        // DbIo.CommandCurrent += " String ";
                        // Add Column SubType & Length
                        // Add DbIo.CommandCurrent += " VARCHAR(512)";
                        // Type
                        DbSyn.spOutputAlterColumnType = PickRow.PickDictArray[iPassedIndex].sColumnTypeWord;
                        if (DbSyn.spOutputAlterColumnType.Length == 0) { DbSyn.spOutputAlterColumnType = "VARCHAR"; }
                        DbSyn.spSqlColumnAddCmd += " " + DbSyn.spOutputAlterColumnType;
                        // Length
                        DbSyn.ipOutputAlterColumnLength = PickRow.PickDictArray[iPassedIndex].ColumnWidth;
                        if (DbSyn.ipOutputAlterColumnLength < 1 || DbSyn.ipOutputAlterColumnLength > 32000000) {
                            DbSyn.ipOutputAlterColumnLength = 512;
                        }
                        DbSyn.spOutputAlterColumnLength = DbSyn.ipOutputAlterColumnLength.ToString();
                        DbSyn.spSqlColumnAddCmd += "" + DbSyn.spOutputAlterColumnTypePrefix + DbSyn.spOutputAlterColumnLength + DbSyn.spOutputAlterColumnTypeSuffix;
                        // Add Column Key and Indexing information
                        if (iPassedIndex == 0) {
                            DbSyn.spSqlColumnAddCmd += " CONSTRAINT " + DbSyn.spOutputAlterColumnName + "Pk";
                            DbSyn.spSqlColumnAddCmd += " PRIMARY KEY";
                            DbSyn.spSqlColumnAddCmd += " UNIQUE";
                            // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                        }
                        PickRow.PickDictArray[iPassedIndex].sColumnAdd = DbSyn.spSqlColumnAddCmd;
                    } // is PK Primary Dictionary Column Definition (ie the PK for this column, not an alias)
                    // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                    // Create View
                    // View Item Column Id
                    DbSyn.spSqlColumnViewCmd = ""; // DbSyn.spOutputAlterVerb + " ";
                    //
                    if (PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType != ColTypeDef.ITEM_ISFUNCTION) {
                        DbSyn.spOutputAlterColumnNameSource = "c" + (PickRow.PickDictArray[iPassedIndex].ItemId + 0).ToString();
                        if (PickRow.PickDictArray[iPassedIndex].ItemIdFoundNumericPk) {
                            sTemp3 = SqlColConvertCharacters(PickRow.PickDictArray[PickRow.PdIndex].sHeading, ColIndex.CharsPassedIn, ColIndex.CharsPassedOut);
                            DbSyn.spOutputAlterColumnNameAlias = sTemp3;
                        } else {
                            DbSyn.spOutputAlterColumnNameAlias = PickRow.PickDictArray[iPassedIndex].ItemIdConverted;
                        }
                        try {
                            iTemp = Convert.ToInt32(DbSyn.spOutputAlterColumnNameAlias);
                            DbSyn.spOutputAlterColumnNameAlias = "c" + (iTemp + 0).ToString();
                        } catch { ; }
                        if (DbSyn.spOutputAlterColumnNameAlias.Length == 0) {
                            DbSyn.spSqlColumnViewCmd += DbSyn.spOutputAlterColumnNameSource;
                        } else {
                            DbSyn.spSqlColumnViewCmd += DbSyn.spOutputAlterColumnNameSource + " AS " + DbSyn.spOutputAlterColumnNameAlias;
                        }

                        if (true == false) {
                            // DbIo.CommandCurrent += " String ";
                            // View Column SubType & Length
                            // View DbIo.CommandCurrent += " VARCHAR(512)";
                            // Type
                            DbSyn.spOutputAlterColumnType = PickRow.PickDictArray[iPassedIndex].sColumnTypeWord;
                            if (DbSyn.spOutputAlterColumnType.Length == 0) { DbSyn.spOutputAlterColumnType = "VARCHAR"; }
                            DbSyn.spSqlColumnViewCmd += " " + DbSyn.spOutputAlterColumnType;
                            // Length
                            DbSyn.ipOutputAlterColumnLength = PickRow.PickDictArray[iPassedIndex].ColumnWidth;
                            if (DbSyn.ipOutputAlterColumnLength < 1 || DbSyn.ipOutputAlterColumnLength > 32000000) {
                                DbSyn.ipOutputAlterColumnLength = 512;
                            }
                            DbSyn.spOutputAlterColumnLength = DbSyn.ipOutputAlterColumnLength.ToString();
                            DbSyn.spSqlColumnViewCmd += "" + DbSyn.spOutputAlterColumnTypePrefix + DbSyn.spOutputAlterColumnLength + DbSyn.spOutputAlterColumnTypeSuffix;
                            // View Column Key and Indexing information
                            if (iPassedIndex == 0) {
                                DbSyn.spSqlColumnViewCmd += " { PRIMARY KEY }";
                                // View Primary Key
                                // DbIo.CommandCurrent = " { PRIMARY KEY }";
                                // View Unique
                                // DbIo.CommandCurrent = " { UNIQUE }";
                            }
                        }
                        PickRow.PickDictArray[iPassedIndex].sColumnView = DbSyn.spSqlColumnViewCmd;
                    } // (NOT) ISFUNCTION
                } // ISDICT
                // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                iSqlColAddCmdBuild = (int)DatabaseControl.ResultOK;
            } catch (SqlException ExceptionSql) {
                sLocalErrorMessage = "";
                ExceptSql(sLocalErrorMessage, ref ExceptionSql, iSqlColAddCmdBuild);
                iSqlColAddCmdBuild = (int)DatabaseControl.ResultFailed;
                DbSyn.spSqlColumnAddCmd = "";
            } catch (Exception ExceptionGeneral) {
                sLocalErrorMessage = "";
                ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iSqlColAddCmdBuild);
                iSqlColAddCmdBuild = (int)DatabaseControl.ResultFailed;
                DbSyn.spSqlColumnAddCmd = "";
            } finally {
                if (true == false) {
                    DbIo.SqlDbCommandObject = null;
                }
            }
            return iSqlColAddCmdBuild;
        }
        // <Section Id = "x
        protected internal long SqlColAddCmdBuildAll(ref SqlDataReader ofddrPassedSqlDbDataReader, ref SqlDataAdapter ofddrPassedSqlDbDataWriter, ref RowInfoDef ColInfoDbPassed, ref ColInfoDef ColInfoPassed, bool bPassedUsePrimary, bool bPassedUseIndexName, int iPassedColAction, ref String sPassedIndexName, int iPassedIndex, int iPassedCount) {
            iSqlColAddCmdBuildAll = (int)DatabaseControl.ResultStarted;
            FileStatus.StatusCurrent = FileStatusAux.iFileDoesExist;
            // Add Column Command Build by processing schema

            DbIo.CommandCurrent = DbSyn.spOutputAlterCommand + " " + "'" + FileId.FileNameFull + "'" + " ";

            return iSqlColAddCmdBuildAll;
        }
        // <Section Id = "x
        protected internal long SqlColAction(ref SqlDataReader ofddrPassedSqlDbDataReader, ref SqlDataAdapter ofddrPassedSqlDbDataWriter, ref RowInfoDef ColInfoDbPassed, ref ColInfoDef ColInfoPassed, bool bPassedUsePrimary, bool bPassedUseIndexName, int iPassedColAction, ref String sPassedIndexName, int iPassedIndex, int iPassedCount) {
            iSqlColAction = (int)DatabaseControl.ResultStarted;
            SqlDataReader x;
            //
            ColInfoPassed.ColAction = iPassedColAction;

            if (bPassedUseIndexName) {
                switch (ColInfoPassed.ColAction) {
                    case (ColInfoDef.SFC_SET_ROW):
                        // Row
                        // REQUIRES A SELECT TO LOCATE THE ROW!
                        // ItemIdNext = FileIndexName;
                        // TODO z$NOTE This should involve an Auxiliary Get or
                        // make use of a Flag to indicate that either
                        // the Primary ItemData Item is being abandoned in
                        // favour of the new Get or that the Primary ItemData
                        // Item will be retained but an Auxiliary Item is
                        // needed for some protected internal reason.
                        iSqlColAction = SqlFileDataGet(FileSummary.FileName, FileId.FileNameFull, sPassedIndexName);
                        // 
                        break;
                    case (ColInfoDef.SFC_SET_COLUMN):
                        // Column
                        ColInfoPassed.FileIndex = DbIo.SqlDbDataReaderObject.GetOrdinal(sPassedIndexName);
                        break;
                    default:
                        sLocalErrorMessage = "The Set Column ItemData Action has not been set";
                        ColInfoPassed.ColAction = (int)DatabaseControl.ResultUndefined;
                        throw new NotSupportedException(sLocalErrorMessage);
                }
            }

            // bool FileHasRows = bPassedHasRows;
            int iHiddenCount = ColInfoPassed.ColCountHidden;

            switch (ColInfoPassed.ColAction) {
                //
                case (ColInfoDef.SFC_SET_ROW):
                    // Row
                    ColInfoPassed.ColRowIndex = iPassedIndex;
                    ColInfoPassed.sColRowIndexName = sPassedIndexName;
                    ColInfoPassed.ColRowCount = iPassedCount;
                    if (ColInfoPassed.ColRowCount > 0) {
                        ColInfoPassed.ColumnHasRows = true;
                    } else {
                        ColInfoPassed.ColumnHasRows = false;
                    }
                    break;
                case (ColInfoDef.SFC_SET_COLUMN):
                    // Column
                    ColInfoPassed.ColIndex = iPassedIndex;
                    ColInfoPassed.sColIndexName = sPassedIndexName;
                    ColInfoPassed.ColCount = iPassedCount;
                    ColInfoPassed.ColCountVisible = iPassedCount;
                    ColInfoPassed.ColCountVisible = iHiddenCount;
                    break;
                case (ColInfoDef.SFC_GET_NATIVE_VALUE):
                    // Column
                    ColInfoPassed.ColIndex = iPassedIndex;
                    ColInfoPassed.sColIndexName = sPassedIndexName;
                    ColInfoPassed.ColCount = iPassedCount;
                    ColInfoPassed.ColCountVisible = iHiddenCount;
                    // Get
                    ColInfoPassed.iGetIndex = iPassedIndex;
                    ColInfoPassed.sGetResultToString = "No Result Available. ";
                    // ColInfoPassed.sGetResultToString = "";
                    //
                    if (ofddrPassedSqlDbDataReader != null) {
                        x = ofddrPassedSqlDbDataReader;
                        ColInfoPassed.sGetDataTypeName = x.GetDataTypeName(ColInfoPassed.iGetIndex);
                        try {
                            switch (ColInfoPassed.sGetDataTypeName) {
                                //
                                case ("bool"):
                                case ("bit"):
                                    ColInfoPassed.bGetBoolean = x.GetBoolean(ColInfoPassed.iGetIndex);
                                    ColInfoPassed.sGetResultToString = ColInfoPassed.bGetBoolean.ToString();
                                    break;
                                case ("byte"):
                                case ("tinyint"):
                                    ColInfoPassed.bbGetByte = x.GetByte(ColInfoPassed.iGetIndex);
                                    ColInfoPassed.sGetResultToString = ColInfoPassed.bbGetByte.ToString();
                                    break;
                                case ("char"):
                                    // ColInfoPassed.bcGetChar = x.GetChar(ColInfoPassed.iGetIndex);
                                    // ColInfoPassed.sGetResultToString = ColInfoPassed.bcGetChar.ToString();
                                    break;
                                case ("DateTime"):
                                    ColInfoPassed.tdtGetDateTime = x.GetDateTime(ColInfoPassed.iGetIndex);
                                    ColInfoPassed.sGetResultToString = ColInfoPassed.tdtGetDateTime.ToString();
                                    break;
                                case ("DateTimeOffset"):
                                    ColInfoPassed.tdtoGetDateTimeOffset = x.GetDateTimeOffset(ColInfoPassed.iGetIndex);
                                    ColInfoPassed.sGetResultToString = ColInfoPassed.tdtoGetDateTimeOffset.ToString();
                                    break;
                                case ("decimal"):
                                    ColInfoPassed.deGetDecimal = x.GetDecimal(ColInfoPassed.iGetIndex);
                                    ColInfoPassed.sGetResultToString = ColInfoPassed.deGetDecimal.ToString();
                                    break;
                                case ("double"):
                                    ColInfoPassed.doGetDouble = x.GetDouble(ColInfoPassed.iGetIndex);
                                    ColInfoPassed.sGetResultToString = ColInfoPassed.doGetDouble.ToString();
                                    break;
                                case ("IEnumerator"):
                                    // IEnumerator<ColInfoPassed.lnGetEnumeratorT> = null;
                                    // sTemp1 = ColInfoPassed.lnGetEnumerator.ToString();
                                    break;
                                case ("float"):
                                    ColInfoPassed.fGetFloat = x.GetFloat(ColInfoPassed.iGetIndex);
                                    ColInfoPassed.sGetResultToString = ColInfoPassed.fGetFloat.ToString();
                                    break;
                                case ("Guid"):
                                    ColInfoPassed.tgGetGuid = x.GetGuid(ColInfoPassed.iGetIndex);
                                    ColInfoPassed.sGetResultToString = ColInfoPassed.tgGetGuid.ToString();
                                    // bbaTmp1 = ColInfoPassed.tgGetGuid.ToByteArray()'
                                    break;
                                case ("short"):
                                case ("int16"):
                                case ("smallint"):
                                    ColInfoPassed.isGetInt16 = x.GetInt16(ColInfoPassed.iGetIndex);
                                    ColInfoPassed.sGetResultToString = ColInfoPassed.isGetInt16.ToString();
                                    break;
                                case ("int"):
                                case ("int32"):
                                    // "int" // "int32"
                                    ColInfoPassed.iGetInt32 = x.GetInt32(ColInfoPassed.iGetIndex);
                                    ColInfoPassed.sGetResultToString = ColInfoPassed.iGetInt32.ToString();
                                    break;
                                case ("long"):
                                case ("int64"):
                                case ("bigint"):
                                    ColInfoPassed.ilGetInt64 = x.GetInt64(ColInfoPassed.iGetIndex);
                                    ColInfoPassed.sGetResultToString = ColInfoPassed.ilGetInt64.ToString();
                                    break;
                                // case ("ushort"):
                                // ColInfoPassed.isuGetInt16 = (short) x.GetInt16(ColInfoPassed.iGetIndex);
                                // ColInfoPassed.sGetResultToString = ColInfoPassed.isuGetInt16.ToString();
                                // break;
                                // case ("uint"):
                                // "int" // "int32"
                                // ColInfoPassed.iuGetInt32 = (int) (x.GetInt32(ColInfoPassed.iGetIndex);
                                // ColInfoPassed.sGetResultToString = ColInfoPassed.iuGetInt32.ToString();
                                // break;
                                // case ("ulong"):
                                // ColInfoPassed.iluGetInt64 = (long) x.GetInt64(ColInfoPassed.iGetIndex);
                                // ColInfoPassed.sGetResultToString = ColInfoPassed.iluGetInt64.ToString();
                                // break;
                                case ("string"):
                                    ColInfoPassed.sGetString = x.GetString(ColInfoPassed.iGetIndex);
                                    ColInfoPassed.sGetResultToString = ColInfoPassed.sGetString;
                                    break;
                                case ("TimeSpan"):
                                    ColInfoPassed.tdtsGetTimeSpan = x.GetTimeSpan(ColInfoPassed.iGetIndex);
                                    ColInfoPassed.sGetResultToString = ColInfoPassed.tdtsGetTimeSpan.ToString();
                                    break;
                                case ("varchar"):
                                    ColInfoPassed.sGetString = x.GetString(ColInfoPassed.iGetIndex);
                                    ColInfoPassed.sGetResultToString = ColInfoPassed.sGetString;
                                    break;
                                case ("ushort"):
                                case ("uint"):
                                case ("ulong"):
                                default:
                                    ColInfoPassed.sGetResultToString += "Type String Not Found! ";
                                    ColInfoPassed.sGetResultNotSupported += ColInfoPassed.sGetDataTypeName + ", ";
                                    sLocalErrorMessage = ColInfoPassed.sGetResultToString;
                                    throw new NotSupportedException(ColInfoPassed.sGetResultToString);
                            }
                            ColInfoPassed.sGetResultToString = "";
                        } catch (SqlException ExceptionSql) {
                            sLocalErrorMessage = "";
                            ColInfoPassed.sGetResultToString += "Sql Error in Get Type String! ";
                            ExceptSql(sLocalErrorMessage, ref ExceptionSql, iSqlColAction);
                            //
                            sLocalErrorMessage = ColInfoPassed.sGetResultToString;
                            XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlColAction, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                            //
                        } catch (Exception ExceptionGeneral) {
                            sLocalErrorMessage = "";
                            ColInfoPassed.sGetResultToString += "General Error in Get Type String! ";
                            ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iSqlColAction);
                            //
                            sLocalErrorMessage = ColInfoPassed.sGetResultToString;
                            XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlColAction, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                        } finally {
                            //
                            ColInfoPassed.sGetResultToString += "Result Type String (" + ColInfoPassed.sGetDataTypeName + "). ";
                            ColInfoPassed.sGetResultToString += "Row: (" + ColInfoPassed.ColIndex.ToString() + "). ";
                            ColInfoPassed.sGetResultToString += "Column: ";
                            ColInfoPassed.sGetResultToString += ColInfoPassed.sColIndexName;
                            ColInfoPassed.sGetResultToString += " (" + ColInfoPassed.ColIndex.ToString() + ").";
                        }
                    } else {
                        ColInfoPassed.sGetResultToString += "No Result Available, Sql ItemData Reader is null!";
                        sLocalErrorMessage = ColInfoPassed.sGetResultToString;
                        XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlColAction, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                    }
                    break;
                case (ColInfoDef.SFC_GET_SQL_VALUE):
                    //
                    ColInfoPassed.iGetIndex = iPassedIndex;
                    ColInfoPassed.sGetResultToString = "No Result Available";
                    //
                    if (ofddrPassedSqlDbDataReader != null) {
                        x = ofddrPassedSqlDbDataReader;
                        ColInfoAux.sGetDataTypeName = ofddrPassedSqlDbDataReader.GetDataTypeName(ColInfoAux.ColIndex);

                        ColInfoPassed.sqlbiGetSqlBinary = (System.Data.SqlTypes.SqlBinary)null;
                        ColInfoPassed.sqlbGetSqlBoolean = false;
                        ColInfoPassed.sqlbbGetSqlByte = 0;
                        ColInfoPassed.sqliGetSqlBytes = (System.Data.SqlTypes.SqlBytes)null;
                        ColInfoPassed.sqliGetSqlChars = (System.Data.SqlTypes.SqlChars)null;
                        ColInfoPassed.sqltdtGetSqlDateTime = (System.Data.SqlTypes.SqlDateTime.MaxValue);
                        ColInfoPassed.sqlfdGetSqlDecimal = 0;
                        ColInfoPassed.fdGetSqlDouble = 0;
                        ColInfoPassed.tgGetSqlGuid = (System.Data.SqlTypes.SqlGuid.Null);
                        ColInfoPassed.isGetSqlInt16 = 0;
                        ColInfoPassed.iGetSqlInt32 = 0;
                        ColInfoPassed.ilGetSqlInt64 = 0;
                        ColInfoPassed.fdGetSqlMoney = 0;
                        ColInfoPassed.fGetSqlSingle = 0;
                        ColInfoPassed.sGetSqlString = "";
                        ColInfoPassed.ooGetSqlValue = 0;
                        ColInfoPassed.iGetSqlValues = 0;
                        ColInfoPassed.ooGetSqlXml = (System.Data.SqlTypes.SqlXml.Null);
                    }

                    break;
                case (ColInfoDef.SFC_RESET):
                    // Row
                    ColInfoPassed.ColRowIndex = 0;
                    ColInfoPassed.sColRowIndexName = "";
                    ColInfoPassed.ColRowCount = 0;
                    ColInfoPassed.ColumnHasRows = false;
                    // Column
                    ColInfoPassed.ColIndex = 0;
                    ColInfoPassed.sColIndexName = "";
                    ColInfoPassed.ColCount = 0;
                    ColInfoPassed.ColCountVisible = 0;
                    // Sql ItemData Client
                    ColInfoPassed.bGetBoolean = false;
                    ColInfoPassed.bbGetByte = 0;
                    ColInfoPassed.loGetBytes = 0;
                    ColInfoPassed.bcGetChar = (char)0;
                    ColInfoPassed.loGetChars = 0;
                    ColInfoPassed.sGetDataTypeName = "";
                    ColInfoPassed.tdtGetDateTime = (DateTime)System.DateTime.Now;
                    ColInfoPassed.tdtoGetDateTimeOffset = DateTimeOffset.MinValue;
                    ColInfoPassed.deGetDecimal = 0;
                    ColInfoPassed.doGetDouble = 0;
                    // IEnumerator<ColInfoPassed.lnGetEnumeratorT> = null;
                    ColInfoPassed.ttGetFieldType = (System.Type)null;
                    ColInfoPassed.fGetFloat = 0;
                    ColInfoPassed.tgGetGuid = (System.Guid.Empty);
                    ColInfoPassed.isGetInt16 = 0;
                    ColInfoPassed.iGetInt32 = 0;
                    ColInfoPassed.ilGetInt64 = 0;
                    ColInfoPassed.iGetName = "";
                    ColInfoPassed.iGetOrdinal = 0;
                    ColInfoPassed.ttGetProviderSpecificFieldType = (System.Type)null;
                    ColInfoPassed.ooGetProviderSpecificValue = 0;
                    ColInfoPassed.iGetProviderSpecificValues = 0;
                    ColInfoPassed.tfdtGetSchemaTable = (System.Data.DataTable)null;
                    ColInfoPassed.sqlbiGetSqlBinary = (System.Data.SqlTypes.SqlBinary)null;
                    ColInfoPassed.sqlbGetSqlBoolean = false;
                    ColInfoPassed.sqlbbGetSqlByte = 0;
                    ColInfoPassed.sqliGetSqlBytes = (System.Data.SqlTypes.SqlBytes)null;
                    ColInfoPassed.sqliGetSqlChars = (System.Data.SqlTypes.SqlChars)null;
                    ColInfoPassed.sqltdtGetSqlDateTime = (System.Data.SqlTypes.SqlDateTime.MaxValue);
                    ColInfoPassed.sqlfdGetSqlDecimal = 0;
                    ColInfoPassed.fdGetSqlDouble = 0;
                    ColInfoPassed.tgGetSqlGuid = (System.Data.SqlTypes.SqlGuid.Null);
                    ColInfoPassed.isGetSqlInt16 = 0;
                    ColInfoPassed.iGetSqlInt32 = 0;
                    ColInfoPassed.ilGetSqlInt64 = 0;
                    ColInfoPassed.fdGetSqlMoney = 0;
                    ColInfoPassed.fGetSqlSingle = 0;
                    ColInfoPassed.sGetSqlString = "";
                    ColInfoPassed.ooGetSqlValue = 0;
                    ColInfoPassed.iGetSqlValues = 0;
                    ColInfoPassed.ooGetSqlXml = (System.Data.SqlTypes.SqlXml.Null);
                    ColInfoPassed.sGetString = "";
                    ColInfoPassed.tdtsGetTimeSpan = (TimeSpan.Zero);
                    ColInfoPassed.ooGetValue = 0;
                    ColInfoPassed.iGetValues = 0;
                    ColInfoPassed.bICommandBehavior = false;
                    ColInfoPassed.bIsDBNull = false;
                    ColInfoPassed.bNextResult = false;
                    ColInfoPassed.bRead = false;
                    break;
                default:
                    // no action
                    sLocalErrorMessage = "The Set Column ItemData Action has not been set";
                    ColInfoPassed.ColAction = (int)DatabaseControl.ResultUndefined;
                    throw new NotSupportedException(LocalMessage);
            }
            if (bPassedUsePrimary) {
                ColInfo = ColInfoPassed;
            } else {
                ColInfoAux = ColInfoPassed;
            }
            //
            return iSqlColAction;
        }
        #endregion
        #region SqlFileDictionaryColumnConversion
        ///*
        public String SqlColConvertCharacters(String sPassedField, char[] PassedCharsIn, char[] PassedCharsOut) {
            iSqlColConvertCharacters = (int)DatabaseControl.ResultStarted;
            String sFieldOut = sPassedField;
            // CHAR CONVERT 2
            // public String PickConvertTypeTwo(String sField_Char, String sCharTo, String sField) {
            String sTemp1 = "";
            String sTemp2 = "";
            String sField_Char = "";
            // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            int iLocChar = 0;
            for (iLocChar = 0; iLocChar < PassedCharsIn.Count(); iLocChar++) {
                if (PassedCharsIn[iLocChar] != PassedCharsOut[iLocChar]) {
                    sField_Char = PassedCharsIn[iLocChar].ToString();
                    // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                    int iLoc = 0;
                    int iForCounter = 0;
                    // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                    // C# For Loop
                    do {
                        iLoc = sFieldOut.IndexOf(sField_Char, 0);
                        if (iLoc >= 0) {
                            iForCounter++;
                            sTemp1 = sFieldOut.Substring(0, iLoc);
                            if (iLoc < sFieldOut.Length - 1) {
                                sTemp2 = sFieldOut.Substring(iLoc + 1, sFieldOut.Length - iLoc - 1);
                            } else {
                                sTemp2 = "";
                            }
                            sFieldOut = sTemp1 + PassedCharsOut[iLocChar] + sTemp2;
                        }
                    } while (iLoc > 0);
                }
            } // Process next replacement char
            //  return sField;
            // return iSqlColAddCmdBuildAllFromArray;
            return sFieldOut;
        }
        #endregion
        #region SqlFileDictUpdate
        #region SqlFileDictArrayInsert
        public String SqlFileDictArrayDesc;
        protected internal bool bIsAnError = true;
        protected internal bool bIsNotAnError = false;
        protected internal bool bDoDecrementIndex = true;
        protected internal bool bDoNotDecrementIndex = false;
        protected internal IndexOutOfRangeException oeIndexOutOfRangeException = null;
        protected internal FormatException oeFormatException = null;
        protected internal Exception oExceptionGeneralExecpt = null;
        public void SqlFileDictArrayIndexError(int PassedMinVerbosity, ref FormatException oeFormatException, ref IndexOutOfRangeException oeIndexOutOfRangeException, String PassedSqlFileDictArrayDesc, bool PassedIsError, bool PassedReduceIndex) {
            // non numeric item id / column 0
            // normal, not an error here
            if (PassedReduceIndex) { PickRow.PdIndexTemp = -1; }
            if (PassedIsError) {
                PickRow.PdErrorCount += 1;
            } else {
                PickRow.PdErrorWarningCount += 1;
            }
            if (ConsoleVerbosity >= PassedMinVerbosity) {
                sLocalErrorMessage = PassedSqlFileDictArrayDesc;
                XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
            }
        }
        // <Section Id = "SqlDictInsert">
        public long SqlFileDictArrayInsert(String OutputCommandPassed) {
            iSqlFileDictInsert = (int)DatabaseControl.ResultStarted;
            DbSyn.spOutputCommand = OutputCommandPassed;
            DbIo.CommandCurrent = OutputCommandPassed;
            iSqlFileDictInsert = (int)DatabaseControl.ResultOperationInProgress;
            // PickRow.PickDictArray = null;
            // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            //
            // ColArray[Am] = ColText;
            //
            PickRow.iAttrType = ColTypeDef.ITEM_ISNOTSET;
            //
            PickRow.PdIndexTemp = -2;
            PickRow.PdIndex = -2;
            PickDictItem.PdIndexAttrTwo = -2;
            PickRow.PdIndexItemId = -2;
            //
            PickRow.ItemIdFoundNumericPk = false;
            PickRow.DictColumnIdDone = false;
            //
            PickRow.PdIndexDoSearch = true;
            //
            PickRow.ItemId = "";
            PickRow.ItemIntId = 0;
            PickRow.DictColumnIdDone = false;
            PickRow.ColumnInvalid = false;
            PickRow.ColumnDataPoints = 0;
            PickRow.DictColumnTouched = 0;
            //
            PickRow.PdIndexItemId = PickRowDef.PdIndexMax;
            PickRow.PdIndexTemp = PickRowDef.PdIndexMax;
            //
            PickRow.AttrTwoIsNumeric = false;
            PickRow.AttrTwoString = "";
            PickRow.AttrTwoStringAccounName = "";
            PickRow.AttrThreeFileName = "";
            //
            iSqlFileDictInsert = (int)DatabaseControl.ResultOperationInProgress;
            //
            // Preliminary Validation
            //
            // Length of item
            if (Item.ItemData.Length < 12) { PickRow.ColumnDataPoints += 300; }
            if (Item.ItemData.Length < 15) { PickRow.ColumnDataPoints += 100; }
            if (Item.ItemData.Length < 20) { PickRow.ColumnDataPoints += 50; }
            if (Item.ItemData.Length < 25) { PickRow.ColumnDataPoints += 25; }
            if (Item.ItemData.Length > 100) { PickRow.ColumnDataPoints += 10; }
            if (Item.ItemData.Length > 30) { PickRow.ColumnDataPoints -= 30; }
            if (PickRow.ColumnDataPoints > 50) {
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
            }
            //
            // Column reference
            try {
                PickRow.AttrTwoInt = -2;
                PickRow.AttrTwoIsNumeric = false;
                PickRow.AttrTwoString = (String)ColIndex.ColArray[2];
                PickRow.AttrTwoInt = Convert.ToInt32(PickRow.AttrTwoString);
                PickRow.AttrTwoIsNumeric = true;
                // Attr Two is numeric
                if (PickRow.AttrTwoInt < 0) {
                    PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                    PickRow.ColumnDataPoints += 500;
                } else if (PickRow.AttrTwoInt < 5) {
                    PickRow.ColumnDataPoints -= 10;
                } else if (PickRow.AttrTwoInt < 10) {
                    PickRow.ColumnDataPoints -= 40;
                } else if (PickRow.AttrTwoInt < 20) {
                    PickRow.ColumnDataPoints -= 30;
                } else if (PickRow.AttrTwoInt > 200) {
                    PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                    PickRow.ColumnDataPoints += 500;
                } else if (PickRow.AttrTwoInt > 100) {
                    PickRow.ColumnDataPoints += 30;
                } else if (PickRow.AttrTwoInt > 75) {
                    PickRow.ColumnDataPoints += 20;
                } else if (PickRow.AttrTwoInt > 50) {
                    PickRow.ColumnDataPoints += 10;
                    // 21 - 50 defaults to -10 below
                } else { PickRow.ColumnDataPoints -= 10; }
                if (PickRow.ColumnDataPoints > 50) {
                    PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                }
            } catch (FormatException oeMexceptCmdFormatException) {
                // non numeric Attr Two / column 0
                // normal, not an error here
                PickRow.AttrTwoInt = -1;
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                PickRow.AttrTwoIsNumeric = false;
                PickRow.PdErrorWarningCount += 1;
                sLocalErrorMessage = "Non-numeric Attr Two. Item is ItemData.";
                XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                //
            } catch (IndexOutOfRangeException e) {
                PickRow.AttrTwoInt = -1;
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                PickRow.AttrTwoIsNumeric = false;
                PickRow.PdErrorCount += 1;
                sLocalErrorMessage = "Abnormal Index Error referencing numeric Attr Two. Index out of range!!! Item will be treated as ItemData.";
                XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                //
            } catch (Exception ExceptionGeneral) {
                // column 2 is not numeric
                PickRow.AttrTwoInt = -1;
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                PickRow.AttrTwoIsNumeric = false;
                PickRow.PdErrorCount += 1;
                sLocalErrorMessage = "";
                ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iSqlFileDictInsert);
                //
                sLocalErrorMessage = "Abnormal OS Error referencing numeric Attr Two, Os error occured!!! Item will be treated as ItemData.";
                sLocalErrorMessage = ExceptionGeneral.Message;
                XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");

            } catch { // warning, not reachable
                // column 2 is not numeric
                PickRow.AttrTwoInt = -1;
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                PickRow.AttrTwoIsNumeric = false;
                PickRow.PdErrorCount += 1;
                sLocalErrorMessage = "Abnormal and Unknown Error referencing numeric Attr Two, Undefined error occured!!! Item will be treated as ItemData.";
                XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
            }
            //
            // Column Width
            try {
                PickRow.ColumnWidth = -2;
                PickRow.ColumnWidthIsNumeric = false;
                PickRow.ColumnWidthString = (String)ColIndex.ColArray[10];
                PickRow.ColumnWidth = Convert.ToInt32(PickRow.ColumnWidthString);
                // Column width is numeric
                PickRow.ColumnWidthIsNumeric = true;
                if (PickRow.ColumnWidth > 120) {
                    PickRow.ColumnDataPoints += 500;
                } else if (PickRow.ColumnWidth > 85) {
                    PickRow.ColumnDataPoints += 100;
                } else if (PickRow.ColumnWidth > 60) {
                    PickRow.ColumnDataPoints += 30;
                } else if (PickRow.ColumnWidth > 35) {
                    PickRow.ColumnDataPoints += 20;
                } else if (PickRow.ColumnWidth > 30) {
                    PickRow.ColumnDataPoints += 5;
                } else if (PickRow.ColumnWidth < 2) {
                    PickRow.ColumnDataPoints += 20;
                } else if (PickRow.ColumnWidth < 1) {
                    PickRow.ColumnDataPoints += 500;
                } else {
                    PickRow.ColumnDataPoints -= 40;
                }
                if (PickRow.ColumnDataPoints > 50) {
                    PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                }
            } catch (FormatException oeMexceptCmdFormatException) {
                // non numeric Column Width / column 0
                // normal, not an error here
                PickRow.ColumnWidth = -1;
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                PickRow.ColumnWidthIsNumeric = false;
                PickRow.PdErrorWarningCount += 1;
                sLocalErrorMessage = "Non-numeric Column Width. Item is ItemData.";
                XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
            } catch (IndexOutOfRangeException e) {
                PickRow.PdIndexTemp = -1;
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                PickRow.ColumnWidthIsNumeric = false;
                PickRow.PdErrorCount += 1;
                sLocalErrorMessage = "Abnormal Index Error referencing numeric Column Width. Index out of range!!! Item will be treated as ItemData.";
                XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                //
            } catch (Exception ExceptionGeneral) {
                // column 2 is not numeric
                PickRow.PdIndexTemp = -1;
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                PickRow.ColumnWidthIsNumeric = false;
                PickRow.PdErrorCount += 1;
                sLocalErrorMessage = "Abnormal OS Error referencing numeric Column Width, Os error occured!!! Item will be treated as ItemData.";
                ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iSqlFileDictInsert);
                //
            } catch {
                // column 2 is not numeric
                PickRow.PdIndexTemp = -1;
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                PickRow.ColumnWidthIsNumeric = false;
                PickRow.PdErrorCount += 1;
                sLocalErrorMessage = "Abnormal and Unknown Error referencing numeric Column Width, Undefined error occured!!! Item will be treated as ItemData.";
                XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
            }
            //
            // skip or continue analysis
            //
            if (PickRow.iAttrType != ColTypeDef.ITEM_ISDATA) {
                PickRow.iAttrType = ColTypeDef.ITEM_ISDICT;
                //
                // Check Item Id (0) for Dictionary Attr (Column) defined
                // If this is a number and it matches the Attr Two column
                // then we have a Primary Key PK and it it the master definition
                // for this column.  All others are aliases.
                //
                // 0 - ID 
                // 0 - int iAttrIndex = 0;
                // Load Id
                try {
                    PickRow.PdIndexItemId = -2;
                    PickRow.ItemIntId = -2;
                    PickRow.ItemId = (String)ColIndex.ColArray[0];
                    if (ConsoleVerbosity >= 5) {
                        // MessageMdmSendToPageNewLine(ref Sender, "A2" + ItemIntId + ": " + ColArray[3] + " " + ColArray[2]);
                        MessageMdmSendToPageNewLine(ref Sender, "A2" + Item.ItemId + ": " + ColIndex.ColArray[3] + " " + ColIndex.ColArray[2]);
                    }
                    PickRow.PdIndexTemp = Convert.ToInt32(ColIndex.ColArray[0]);
                    // Item Id is numeric
                    if (PickRow.PdIndexTemp < 0 || PickRow.PdIndexTemp > PickRowDef.PdIndexMax) {
                        // numeric item id is out of range
                        oeIndexOutOfRangeException = null;
                        oeFormatException = null;
                        SqlFileDictArrayDesc = "Id's numeric Item Id reference reference is out or allowed range." + " Index is not numeric and is a name!!! Will place this amoung aliases!";
                        ;
                        SqlFileDictArrayIndexError(5, ref oeFormatException, ref oeIndexOutOfRangeException, SqlFileDictArrayDesc, bIsNotAnError, bDoDecrementIndex);
                    } else {
                        // valid numeric item id
                        PickRow.PdIndexItemId = PickRow.PdIndexTemp;
                        PickRow.ItemIntId = PickRow.PdIndexTemp;
                        PickRow.ItemIdIsNumeric = true;
                        PickRow.ColumnDataPoints -= 20;
                    }
                } catch (FormatException oeFormatException) {
                    // non numeric item id / column 0
                    // normal, not an error here
                    oeIndexOutOfRangeException = null;
                    SqlFileDictArrayDesc = "Index Error referencing numeric Item Id reference identity." + " Index is not numeric and is a name!!! Will place this amoung aliases!";
                    ;
                    SqlFileDictArrayIndexError(5, ref oeFormatException, ref oeIndexOutOfRangeException, SqlFileDictArrayDesc, bIsAnError, bDoDecrementIndex);
                } catch (IndexOutOfRangeException oeIndexOutOfRangeException) {
                    oeFormatException = null;
                    SqlFileDictArrayDesc = "Index Error referencing numeric Item Id reference identity." + " Index is not numeric and is a name!!! Will place this amoung aliases!";
                    ;
                    SqlFileDictArrayIndexError(5, ref oeFormatException, ref oeIndexOutOfRangeException, SqlFileDictArrayDesc, bIsAnError, bDoDecrementIndex);
                } catch (Exception ExceptionGeneral) {
                    // column 2 is not numeric
                    oeIndexOutOfRangeException = null;
                    oeFormatException = null;
                    SqlFileDictArrayDesc = ExceptionGeneral.Message;
                    sLocalErrorMessage = ExceptionGeneral.Message;
                    ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iSqlFileDictInsert);
                    SqlFileDictArrayIndexError(5, ref oeFormatException, ref oeIndexOutOfRangeException, SqlFileDictArrayDesc, bIsAnError, bDoDecrementIndex);
                    //
                    SqlFileDictArrayDesc = "OS Error referencing numeric Item Id reference identity, Os error occured!!!" + " Index is not numeric and is a name!!! Will place this amoung aliases!";
                    ;
                    SqlFileDictArrayIndexError(5, ref oeFormatException, ref oeIndexOutOfRangeException, SqlFileDictArrayDesc, bIsNotAnError, bDoNotDecrementIndex);
                } catch {
                    // column 2 is not numeric
                    oeIndexOutOfRangeException = null;
                    oeFormatException = null;
                    SqlFileDictArrayDesc = "Unknown Error referencing numeric Item Id reference identity, Undefined error occured!!!" + " Index is not numeric and is a name!!! Will place this amoung aliases!";
                    SqlFileDictArrayIndexError(5, ref oeFormatException, ref oeIndexOutOfRangeException, SqlFileDictArrayDesc, bIsAnError, bDoDecrementIndex);
                }
                // invalid column number
                if (PickRow.PdIndexItemId < 0) {
                    PickRow.ItemIdIsNumeric = false;
                    PickRow.PdIndexItemId = -1;
                    //
                    PickRow.PdIndexDoSearch = true;
                    PickRow.ColumnDataPoints += 10;
                }
                //
                // 2 - Check Attr Two (2) for Dictionary Attr (Column) defined
                //
                try {
                    // Convert numeric String to UInt32 without a format provider.
                    // [PickRow.PdIndex]
                    PickDictItem.PdIndexAttrTwo = -2;
                    // AttrTwoString = ColArray[2];
                    PickRow.PdIndexTemp = Convert.ToInt32(ColIndex.ColArray[2]);
                    // x
                    if (PickRow.PdIndexTemp < 0 || PickRow.PdIndexTemp > PickRowDef.PdIndexMax) {
                        // numeric column is out of range
                        oeIndexOutOfRangeException = null;
                        oeFormatException = null;
                        SqlFileDictArrayDesc = "Dictionary numeric Column Number reference is out or allowed range.";
                        SqlFileDictArrayIndexError(1, ref oeFormatException, ref oeIndexOutOfRangeException, SqlFileDictArrayDesc, bIsNotAnError, bDoDecrementIndex);
                    } else {
                        //
                        // Column 2 is numeric and within a valid range
                        PickDictItem.PdIndexAttrTwo = PickRow.PdIndexTemp;
                        PickRow.AttrTwoIsNumeric = true;
                        if (PickRow.ItemIntId < 0) { PickRow.ItemIntId = PickDictItem.PdIndexAttrTwo; }
                        PickRow.ColumnDataPoints -= 20;
                        //
                        // Compare 0 to 2 numeric for PK, if a match the dict definition has been found
                        if ((PickRow.ItemIdIsNumeric && PickRow.AttrTwoIsNumeric) && PickRow.PdIndexItemId >= 0 && PickDictItem.PdIndexAttrTwo >= 0 && PickRow.PdIndexItemId == PickDictItem.PdIndexAttrTwo) {
                            // PRIMARY KEY LOCATED FOR THIS COLUMN
                            // *** Located the PK controlling definition for a column
                            PickRow.ItemIdFoundNumericPk = true;
                            // PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo].ItemIdFoundNumericPk = true;
                            PickRow.PdIndexDoSearch = false;
                            PickRow.DictColumnIdDone = true;
                            PickRow.ColumnDataPoints -= 50;
                        } else { PickRow.PdIndexTemp = -1; }
                        // 
                        // DETECT AND CLEAR NULL DATA AT PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo]
                        //
                        if (PickRow.AttrTwoIsNumeric) {
                            try {
                                if (PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo].ItemIntId == 0) {
                                    // this index is initialized
                                    // if the ItemId is not NULL
                                }
                            } catch (IndexOutOfRangeException oeIndexOutOfRangeException) {
                                oeFormatException = null;
                                SqlFileDictArrayDesc = "Index Error validating if dictionary core definition need initialization.";
                                SqlFileDictArrayIndexError(1, ref oeFormatException, ref oeIndexOutOfRangeException, SqlFileDictArrayDesc, bIsAnError, bDoDecrementIndex);
                            } catch {
                                // initialized this index location
                                oeIndexOutOfRangeException = null;
                                oeFormatException = null;
                                SqlFileDictArrayDesc = "Warning, Dictionary core definition reference need to be initialized.";
                                SqlFileDictArrayIndexError(1, ref oeFormatException, ref oeIndexOutOfRangeException, SqlFileDictArrayDesc, bIsNotAnError, bDoNotDecrementIndex);
                                //
                                iSqlFileDictInsert = SqlColClear(PickDictItem.PdIndexAttrTwo);
                            } finally {
                                if (PickDictItem.PdIndexAttrTwo >= PickRow.PdIndexAliasLow) {
                                    // initialized this index location
                                    // need to erase current value and replace it.
                                    //
                                    if (PickRow.ItemIdFoundNumericPk) {
                                        oeIndexOutOfRangeException = null;
                                        oeFormatException = null;
                                        SqlFileDictArrayDesc = "Maximum # of Aliases and Columns exceeded. Index overflow / crossover!!! Will overwrite alias at this location with Dictionary core definition reference!!!";
                                        SqlFileDictArrayIndexError(1, ref oeFormatException, ref oeIndexOutOfRangeException, SqlFileDictArrayDesc, bIsAnError, bDoNotDecrementIndex);
                                        //
                                        if (!PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo].ItemIdFoundNumericPk) {
                                            iSqlFileDictInsert = SqlColClear(PickDictItem.PdIndexAttrTwo);
                                        }
                                    } else {
                                        PickRow.PdIndexTemp = PickRow.PdIndexHigh + 1;
                                        oeIndexOutOfRangeException = null;
                                        oeFormatException = null;
                                        SqlFileDictArrayDesc = "Maximum # of Aliases and Columns exceeded. Index overflow / crossover!!! Will overwrite an alias location already processed!!!";
                                        SqlFileDictArrayIndexError(1, ref oeFormatException, ref oeIndexOutOfRangeException, SqlFileDictArrayDesc, bIsAnError, bDoNotDecrementIndex);
                                    }
                                }
                                PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo].DictColumnTouched += 1;
                                PickRow.DictColumnTouched = PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo].DictColumnTouched;
                            }
                        }
                        // x
                        if (PickRow.ItemIdFoundNumericPk && PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo].ItemIdFoundNumericPk) {
                            // PK already exists and was found previously
                            // search for an empty slot for the new item.
                            // Can not replace the existing PK with a new one.
                            // duplicate PK dict definitions should not be possible
                            // reject the newest one for a PK candidate
                            // TODO FINISH CONVERTING THE ITEMS BELOW TO ERROR CALL
                            PickRow.PdErrorCount += 1;
                            sLocalErrorMessage = "Duplicate dictionary core definition!!!";
                            XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                            //
                            PickRow.ItemIdFoundNumericPk = false;
                            PickRow.DictColumnIdDone = true;
                            PickRow.PdIndexDoSearch = true;
                            PickRow.PdIndexTemp = -1;
                        }
                        // x
                        if (PickRow.ItemIdIsNumeric && PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo].ItemIdFoundNumericPk) {
                            // One or more keys with the same Attr number
                            // exists and was counted in this position
                            PickRow.PdErrorWarningCount += 1;
                            sLocalErrorMessage = "Dictionary core definition exists.";
                            XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                        }
                    }
                    // eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
                } catch (IndexOutOfRangeException e) {
                    PickRow.PdIndexTemp = -1;
                    PickRow.PdErrorWarningCount += 1;
                    sLocalErrorMessage = "Error referencing dictionary core definition, Index error, out of range!!!";
                    XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                    //
                } catch (FormatException oeMexceptCmdFormatException) {
                    PickRow.PdIndexTemp = -1;
                    PickRow.PdErrorWarningCount += 1;
                    sLocalErrorMessage = "Error referencing dictionary core definition, Core reference is not a valid number!!!";
                    XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                    //
                } catch (Exception ExceptionGeneral) {
                    // column 2 is not numeric
                    PickRow.PdIndexTemp = -1;
                    PickRow.PdErrorCount += 1;
                    sLocalErrorMessage = ExceptionGeneral.Message;
                    ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iSqlFileDictInsert);
                    //
                    sLocalErrorMessage = "Error referencing dictionary core definition, Os error occured!!!";
                    XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                } catch {
                    // column 2 is not numeric
                    PickRow.PdIndexTemp = -1;
                    PickRow.PdErrorCount += 1;
                    sLocalErrorMessage = "Error referencing dictionary core definition, Undefined error occured!!!";
                    XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                }
                //
                // invalid column 2 column number
                if (PickRow.PdIndexTemp < 0) {
                    PickRow.ItemIdFoundNumericPk = false;
                    //
                    PickRow.PdIndexDoSearch = true;
                    PickRow.ColumnDataPoints += 20;
                    //
                    PickRow.AttrTwoStringAccounName = (String)ColIndex.ColArray[2];
                    PickRow.AttrThreeFileName = (String)ColIndex.ColArray[3];
                }
                //
                // Search for empty dictionary place holder for this column alias
                //
                if (PickRow.PdIndexDoSearch) {
                    try {
                        for (PickRow.PdIndexTemp = PickRow.PdIndexAliasLow - 1; PickRow.PdIndexTemp > PickRow.PdIndexHigh; PickRow.PdIndexTemp--) {
                            // 
                            // DETECT AND CLEAR NULL DATA AT PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo]
                            //
                            try {
                                if (PickRow.PickDictArray[PickRow.PdIndexTemp].ItemIntId == 0) {
                                    // this index is initialized
                                    // if the ItemId is not NULL
                                }
                            } catch {
                                // initialized this index location
                                iSqlFileDictInsert = SqlColClear(PickRow.PdIndex);
                            }
                            if (PickRow.PickDictArray[PickRow.PdIndexTemp].DictColumnTouched == 0) {
                                PickRow.PdIndexAliasLow = PickRow.PdIndexTemp;
                                break;
                            }
                        }
                        if (PickRow.PdIndexTemp < 0) {
                            // Looped downward below the start of the index
                            PickRow.PdErrorCount += 1;
                            sLocalErrorMessage = @"Maximum # of Aliases and Columns exceeded. Dictionary Work File full, Maximum # of entries exceeded. Index below low range!!!";
                            XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                            //
                            PickRow.PdIndexTemp = PickRow.PdIndexAliasLow - 1;
                            if (PickRow.PdIndexTemp < 0 || PickRow.PdIndexTemp > PickRowDef.PdIndexMax) {
                                // numeric column is out of range
                                PickRow.PdIndexTemp = -1;
                                PickRow.PdErrorCount += 1;
                                sLocalErrorMessage = @"Maximum # of Aliases and Columns exceeded. Index overflow / crossover !!! Maximum # of entries exceeded. ";
                                XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                            }
                        }
                    } catch (IndexOutOfRangeException e) {
                        PickRow.PdIndexTemp = -1;
                        PickRow.PdErrorCount += 1;
                        sLocalErrorMessage = "Dictionary Work File full, Maximum # of entries exceeded. Index out of range!!!";
                        XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                    } catch {
                        PickRow.PdIndexTemp = -1;
                        PickRow.PdErrorCount += 1;
                        sLocalErrorMessage = "Unknown Error during Dictionary search!!!";
                        XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PickSystemCallStringResult, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                    }
                    //
                    if (PickRow.PdIndexTemp != -1 && PickRow.PdIndexTemp <= PickRow.PdIndexHigh) {
                        // error
                        PickRow.PdErrorCount += 1;
                        sLocalErrorMessage = "Index out of range!!! Alias not allowed to overwrite Dictionary core definition. Attempting to use default location.";
                        XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                        //
                        if (PickRow.PdIndexHigh > PickRowDef.PdIndexMax) {
                            PickRow.PdIndexTemp = PickRowDef.PdIndexMax;
                        } else {
                            PickRow.PdIndexTemp = PickRow.PdIndexHigh + 1;
                        }
                        PickRow.PdIndex = PickRow.PdIndexTemp;
                        PickRow.PickDictArray[PickRow.PdIndex].ColumnInvalid += 1;
                    } else if (PickRow.PdIndexTemp != -1) {
                        // index within low range and file not full
                        // alias will be placed in this position
                        PickRow.PdIndex = PickRow.PdIndexTemp;
                        PickRow.PdIndexAliasLow = PickRow.PdIndex;

                        PickRow.PickDictArray[PickRow.PdIndex].ItemIdFoundNumericPk = false;

                        PickRow.PickDictArray[PickRow.PdIndex].DictColumnTouched += PickRow.DictColumnTouched;
                        PickRow.PickDictArray[PickRow.PdIndex].DictColumnIdDone = false;
                        PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength = 0;
                        PickRow.PickDictArray[PickRow.PdIndex].DictColumnLengthChange = false;
                        PickRow.ColumnDataPoints -= 50;
                    }
                    // end of do search
                    // Attr Two was valid, set it here
                } else if (PickRow.ItemIdFoundNumericPk) {
                    //
                    if (PickRow.PdIndexTemp > PickRowDef.PdIndexMax) {
                        PickRow.PdErrorCount += 1;
                        sLocalErrorMessage = "Column number too high, Maximum # of Columns exceeded. Index out of range!!!";
                        XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                        PickRow.PdIndexTemp = PickRowDef.PdIndexMax;
                        PickRow.PickDictArray[PickRow.PdIndexTemp].ColumnInvalid += 1;
                    }
                    //
                    if (PickRow.PdIndexTemp > PickRow.PdIndexAliasLow) {
                        PickRow.PdErrorCount += 1;
                        sLocalErrorMessage = @"Maximum # of Aliases and Columns exceeded. Dictionary core definition overwriting alias!!!";
                        XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                        PickRow.PdIndexHigh = PickRow.PdIndexTemp;
                        PickRow.PickDictArray[PickRow.PdIndexTemp].ColumnInvalid += 1;
                    }
                    //
                    if (PickRow.PdIndexAliasLow <= PickRow.PdIndexHigh) {
                        PickRow.PdErrorCount += 1;
                        sLocalErrorMessage = @"Maximum # of Aliases and Columns exceeded. Index overflow / crossover !!!";
                        XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");

                        PickRow.PickDictArray[PickRow.PdIndexTemp].ColumnInvalid += 1;
                    }
                    //
                    if (PickRow.PdIndexTemp > PickRow.PdIndexHigh) {
                        PickRow.PdIndexHigh = PickRow.PdIndexTemp;
                    }
                    //
                    PickRow.PdIndex = PickRow.PdIndexTemp; // PickDictItem.PdIndexAttrTwo
                    //
                    PickRow.PickDictArray[PickRow.PdIndex].ItemIdFoundNumericPk = PickRow.ItemIdFoundNumericPk;
                    //
                }

                //
                // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                //
                PickRow.PickDictArray[PickRow.PdIndex].ItemId = PickRow.ItemId;
                PickRow.PickDictArray[PickRow.PdIndex].ItemIntId = PickRow.ItemIntId;
                PickRow.PickDictArray[PickRow.PdIndex].ItemIdConverted = PickRow.PickDictArray[PickRow.PdIndex].ItemId;
                //
                PickRow.PickDictArray[PickRow.PdIndex].ItemIdIsNumeric = PickRow.ItemIdIsNumeric;
                PickRow.PickDictArray[PickRow.PdIndex].AttrTwoIsNumeric = PickRow.AttrTwoIsNumeric;
                PickRow.PickDictArray[PickRow.PdIndex].PdIndexAttrTwo = PickDictItem.PdIndexAttrTwo;
                //
                PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints = PickRow.ColumnDataPoints;
                // ColArray[0] = PickRow.PickDictArray[PickRow.PdIndex].ItemId;
                PickRow.PickDictArray[PickRow.PdIndex].ItemId = (String)ColIndex.ColArray[0];
                PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber = (String)ColIndex.ColArray[2];
                PickRow.PickDictArray[PickRow.PdIndex].ItemIdConverted = PickRow.PickDictArray[PickRow.PdIndex].ItemId;
                sTemp3 = SqlColConvertCharacters(PickRow.PickDictArray[PickRow.PdIndex].ItemId, ColIndex.CharsPassedIn, ColIndex.CharsPassedOut);
                PickRow.PickDictArray[PickRow.PdIndex].ItemIdConverted = sTemp3;
                //
                // Convert numericStr to UInt32 without a format provider.
                //
                //`TODO create a SqlColClear
                //
                try {
                    PickRow.PickDictArray[PickRow.PdIndex].DictColIndex = Convert.ToInt32(PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber);
                } catch { ; }
                //
                if (PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber.Length > 2) {
                    PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints -= 10;
                } else if (PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber.Length > 3) {
                    PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints -= 15;
                } else if (PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber.Length > 10) {
                    PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints -= 30;
                } else if (PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber.Length > 30) {
                    PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints -= 50;
                } else {
                    PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 10;
                }
                //
                // Analyze and Convert Dictionary Item
                //
                PickRow.PickDictArray[PickRow.PdIndex].DictColIndex = PickDictItem.PdIndexAttrTwo;
                //
                // TODO Standard Incrment Trace goes here
                iIterationCount += 1;
                if (iIterationCount >= 50) {
                    iIterationCount = 0;
                }
                //
                if (PickRow.ColumnInvalid) {
                    // DO NOT PROCESS THIS ROW / ITEM AS A DICTIONARY RECORD
                    // IT IS DATA
                    iSqlFileDictInsert = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.InvalidResult;
                } else {
                    // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                    //
                    // ColArray[Am] = ColText;
                    //
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 0 - ID 
                    iSqlFileDictInsert = (int)DatabaseControl.ResultOperationInProgress;
                    //
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 1 - Attr Type (A=Attr, Dx=File, Q=FileName Alias, S=??simlar to A??)
                    PickRow.PickDictArray[PickRow.PdIndex].sAttrType = (String)ColIndex.ColArray[1];
                    if (PickRow.PickDictArray[PickRow.PdIndex].sAttrType.Length > 0) {
                        PickRow.PickDictArray[PickRow.PdIndex].sType = PickRow.PickDictArray[PickRow.PdIndex].sAttrType.Substring(0, 1);
                        if (PickRow.PickDictArray[PickRow.PdIndex].sAttrType.Length > 1) {
                            PickRow.PickDictArray[PickRow.PdIndex].sSubType = PickRow.PickDictArray[PickRow.PdIndex].sAttrType.Substring(1);
                            PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 10; // 10
                            if (PickRow.PickDictArray[PickRow.PdIndex].sAttrType.Length > 2) {
                                PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 10; // 20
                            }
                        } else {
                            PickRow.PickDictArray[PickRow.PdIndex].sSubType = "";
                        }
                    } else {
                        PickRow.PickDictArray[PickRow.PdIndex].sType = "";
                        PickRow.PickDictArray[PickRow.PdIndex].sSubType = "";
                    }
                    //
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 2 - Attr / Column Number
                    // PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber = ColArray[2];
                    //
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 3 - Heading or FileName
                    PickRow.PickDictArray[PickRow.PdIndex].sHeading = (String)ColIndex.ColArray[3];
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 4 - Dependant Upon Controlling Attrubute 999
                    PickRow.PickDictArray[PickRow.PdIndex].sDependancy = (String)ColIndex.ColArray[4];
                    //     D30
                    //   - Controlling Attr list of Dependant Attrs
                    //     C;29;31;32;33;34;35;36;37;38;39;50;41;42;43;44;45
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 5 -
                    PickRow.PickDictArray[PickRow.PdIndex].sFive = (String)ColIndex.ColArray[5];
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 6 - Input Conversion
                    PickRow.PickDictArray[PickRow.PdIndex].InputConversion = (String)ColIndex.ColArray[6];
                    // Examples
                    // MR0 
                    // MR2
                    // MR2ZM (M=Mask R=RightJustify 2=2DecimalPlaces Z=FillZerosWithSpaces, M=??)
                    // D2- (D=Date 2=2DigitYear "-"=FormatSeparationCharacter)
                    if (PickRow.PickDictArray[PickRow.PdIndex].InputConversion.Length > 0) {
                        PickRow.PickDictArray[PickRow.PdIndex].InputConvType = PickRow.PickDictArray[PickRow.PdIndex].InputConversion.Substring(0, 1);
                        if (PickRow.PickDictArray[PickRow.PdIndex].InputConversion.Length > 1) {
                            PickRow.PickDictArray[PickRow.PdIndex].InputConvSubType = PickRow.PickDictArray[PickRow.PdIndex].InputConversion.Substring(1);
                            // PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 10;
                            if (PickRow.PickDictArray[PickRow.PdIndex].InputConversion.Length > 5) {
                                PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 10; // 50
                            }
                        } else {
                            PickRow.PickDictArray[PickRow.PdIndex].InputConvSubType = "";
                        }
                    } else {
                        PickRow.PickDictArray[PickRow.PdIndex].InputConvType = "";
                        PickRow.PickDictArray[PickRow.PdIndex].InputConvSubType = "";
                    }
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 7 - Output Conversion
                    PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion = (String)ColIndex.ColArray[7];
                    // Examples
                    // MR0 
                    // MR2
                    // MR2ZM (M=Mask R=RightJustify 2=2DecimalPlaces Z=FillZerosWithSpaces, M=??)
                    // D2- (D=Date 2=2DigitYear "-"=FormatSeparationCharacter)
                    if (PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion.Length > 0) {
                        PickRow.PickDictArray[PickRow.PdIndex].spOutputConvType = PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion.Substring(0, 1);
                        if (PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion.Length > 1) {
                            PickRow.PickDictArray[PickRow.PdIndex].spOutputConvSubType = PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion.Substring(1);
                            // PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 10;
                            if (PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion.Length > 10) {
                                PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 10; // 60
                            }
                        } else {
                            PickRow.PickDictArray[PickRow.PdIndex].spOutputConvSubType = "";
                        }
                    } else {
                        PickRow.PickDictArray[PickRow.PdIndex].spOutputConvType = "";
                        PickRow.PickDictArray[PickRow.PdIndex].spOutputConvSubType = "";
                    }
                    // 
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 8 - Correlative 
                    PickRow.PickDictArray[PickRow.PdIndex].sCorrelative = (String)ColIndex.ColArray[8];
                    // Examples 
                    // F - Format and File Retrieve
                    // F;999;999
                    // F;C1
                    // G - GoTo Field Separated Value
                    // G1*1
                    // G2*1
                    // G0*1
                    // G2*2ýTSTT;X;2;2
                    // T - Foriegn File Retrieve
                    // TADD;X;2;2
                    // TADD;X;12;12ýG4*1
                    // F;2;"A";=;2;"T";=;+
                    // F;1(TADD;X;6;6);8(G3*1);:
                    // 
                    if (PickRow.PickDictArray[PickRow.PdIndex].sCorrelative.Length > 0) {
                        PickRow.PickDictArray[PickRow.PdIndex].sCorrType = PickRow.PickDictArray[PickRow.PdIndex].sCorrelative.Substring(0, 1);
                        if (PickRow.PickDictArray[PickRow.PdIndex].sCorrelative.Length > 1) {
                            PickRow.PickDictArray[PickRow.PdIndex].sCorrSubType = PickRow.PickDictArray[PickRow.PdIndex].sCorrelative.Substring(1);
                            // PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 10;
                            if (PickRow.PickDictArray[PickRow.PdIndex].sCorrelative.Length > 10) {
                                PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 10; // 70
                            }
                        } else {
                            PickRow.PickDictArray[PickRow.PdIndex].sCorrSubType = "";
                        }
                    } else {
                        PickRow.PickDictArray[PickRow.PdIndex].sCorrType = "";
                        PickRow.PickDictArray[PickRow.PdIndex].sCorrSubType = "";
                    }
                    //
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 9 - Justification (R, RN, L)
                    PickRow.PickDictArray[PickRow.PdIndex].sJustify = (String)ColIndex.ColArray[9];
                    if (PickRow.PickDictArray[PickRow.PdIndex].sJustify.Length > 0) {
                        PickRow.PickDictArray[PickRow.PdIndex].sJustification = PickRow.PickDictArray[PickRow.PdIndex].sJustify.Substring(0, 1);
                        if (PickRow.PickDictArray[PickRow.PdIndex].sJustification != "L" && PickRow.PickDictArray[PickRow.PdIndex].sJustification != "R") {
                            PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 30; // 100
                        }
                        if (PickRow.PickDictArray[PickRow.PdIndex].sJustify.Length > 1) {
                            PickRow.PickDictArray[PickRow.PdIndex].sJustifyType = PickRow.PickDictArray[PickRow.PdIndex].sJustify.Substring(1);
                        } else {
                            PickRow.PickDictArray[PickRow.PdIndex].sJustifyType = "";
                        }
                    } else {
                        PickRow.PickDictArray[PickRow.PdIndex].sJustification = "";
                        PickRow.PickDictArray[PickRow.PdIndex].sJustifyType = "";
                    }
                    //
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 10 - Length (999)
                    PickRow.PickDictArray[PickRow.PdIndex].ColumnWidthString = (String)ColIndex.ColArray[10];
                    PickRow.PickDictArray[PickRow.PdIndex].ColumnWidth = 0;
                    PickRow.PickDictArray[PickRow.PdIndex].ColumnWidthIsNumeric = false;
                    // Convert numericStr to UInt32 without a format provider.
                    iSqlFileDictInsert = (int)DatabaseControl.ResultOperationInProgress;
                    try {
                        PickRow.PickDictArray[PickRow.PdIndex].ColumnWidth = Convert.ToInt32(PickRow.PickDictArray[PickRow.PdIndex].ColumnWidthString);
                        PickRow.PickDictArray[PickRow.PdIndex].ColumnWidthIsNumeric = true;
                    } catch (Exception ExceptionGeneral) {
                        PickRow.PickDictArray[PickRow.PdIndex].ColumnWidth = 0;
                        PickRow.PickDictArray[PickRow.PdIndex].ColumnWidthIsNumeric = false;
                        PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 50; // 150
                        sLocalErrorMessage = ExceptionGeneral.Message;
                        ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iSqlFileDictInsert);
                    }
                    if (PickRow.PickDictArray[PickRow.PdIndex].ColumnWidth > 40) {
                        // stop here
                        PickRow.PickDictArray[PickRow.PdIndex].ColumnWidthString += "BIG ONE";
                        iIterationCount = 99999;
                        PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 10;  // 160
                    }
                    if (iIterationCount >= 10) {
                        iIterationCount = 0;
                    }
                    //
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 11 - HeadingLong
                    PickRow.PickDictArray[PickRow.PdIndex].sHeadingLong = (String)ColIndex.ColArray[11];
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 12 - 
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 13 - HelpShort
                    PickRow.PickDictArray[PickRow.PdIndex].sHelpShort = (String)ColIndex.ColArray[13];
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 14 - 
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 15 - 
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 16 - 
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 17 -
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 18 -
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 19 - REVA REVG ColumnName 
                    PickRow.PickDictArray[PickRow.PdIndex].sRevColumnName = (String)ColIndex.ColArray[19];
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 20 - 
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    //
                    // Check number of Attrs
                    // PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter[1] = item.count;
                    // PickRow.PickDictArray[PickRow.PdIndex].iItemLength[1] = item.length;
                    if (PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter > 20) {
                        PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 10;  // 170
                        if (PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter > 30) {
                            PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 100;  // 170
                        }
                    }
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // Column Type
                    if (PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter > 30 || PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints > 100) {
                        PickRow.PickDictArray[PickRow.PdIndex].iAttrType = ColTypeDef.ITEM_ISDATA;
                        PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType = ColTypeDef.ITEM_ISNOTSET;
                        PickRow.PickDictArray[PickRow.PdIndex].bAttrIsDict = false;
                        PickRow.PickDictArray[PickRow.PdIndex].bAttrIsData = true;
                        PickRow.ColumnInvalid = true;
                    } else {
                        PickRow.PickDictArray[PickRow.PdIndex].iAttrType = ColTypeDef.ITEM_ISNOTSET;
                        PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType = ColTypeDef.ITEM_ISNOTSET;
                        PickRow.PickDictArray[PickRow.PdIndex].ColumnType = ColTypeDef.ColumnISNOTSET;
                        switch (PickRow.PickDictArray[PickRow.PdIndex].sType) {
                            case ("Q"):
                                PickRow.PickDictArray[PickRow.PdIndex].iAttrType = ColTypeDef.ITEM_ISFILE;
                                PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType = ColTypeDef.ITEM_ISFILEALIAS;
                                PickRow.PickDictArray[PickRow.PdIndex].bAttrIsDict = false;
                                PickRow.PickDictArray[PickRow.PdIndex].bAttrIsData = false;
                                if (PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter > 20) {
                                    PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 100;  // 270
                                }
                                break;
                            case ("D"):
                                PickRow.PickDictArray[PickRow.PdIndex].iAttrType = ColTypeDef.ITEM_ISFILE;
                                PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType = ColTypeDef.ITEM_ISFILE;
                                PickRow.PickDictArray[PickRow.PdIndex].bAttrIsDict = false;
                                PickRow.PickDictArray[PickRow.PdIndex].bAttrIsData = false;
                                if (PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter > 20) {
                                    PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 100;  // 270
                                }
                                PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 10;
                                break;
                            case ("S"):
                            case ("A"):
                                // Column is Attr
                                PickRow.PickDictArray[PickRow.PdIndex].iAttrType = ColTypeDef.ITEM_ISDICT;
                                PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType = ColTypeDef.ITEM_ISNOTSET;
                                PickRow.PickDictArray[PickRow.PdIndex].bAttrIsDict = true;
                                PickRow.PickDictArray[PickRow.PdIndex].bAttrIsData = false;
                                switch (PickRow.PickDictArray[PickRow.PdIndex].sCorrType) {
                                    case ("T"):
                                    case ("F"):
                                    case ("G"):
                                        //
                                        PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType = ColTypeDef.ITEM_ISFUNCTION;
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnType = ColTypeDef.ColumnISCHAR;
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints += 10;
                                        break;
                                    default:
                                        //
                                        PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType = ColTypeDef.ITEM_ISDICT;
                                        if (PickRow.PickDictArray[PickRow.PdIndex].sCorrType == "" || PickRow.PickDictArray[PickRow.PdIndex].sType == "S") {
                                            PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType = ColTypeDef.ITEM_ISTYPESAttr;
                                        } else {
                                            sLocalErrorMessage = "Unrecognized File Dictionary Correlative Field";
                                            throw new NotSupportedException(sLocalErrorMessage);
                                        }
                                        break;
                                }
                                // Dictionary Attribue Column is Numeric
                                if (PickRow.PickDictArray[PickRow.PdIndex].iAttrType != ColTypeDef.ITEM_ISFUNCTION && PickRow.PickDictArray[PickRow.PdIndex].AttrTwoIsNumeric) {
                                    // Determine ItemData Type for this Column
                                    // Justification
                                    PickRow.PickDictArray[PickRow.PdIndex].ColumnType = ColTypeDef.ColumnISVARCHAR;
                                    if (PickRow.PickDictArray[PickRow.PdIndex].sJustification == "R") {
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnNumericPoints += 5;
                                    } else if (PickRow.PickDictArray[PickRow.PdIndex].sJustification == "L") {
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnNumericPoints -= 5;
                                    }
                                    if (PickRow.PickDictArray[PickRow.PdIndex].sJustifyType == "N") {
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnNumericPoints += 5;
                                        if (PickRow.PickDictArray[PickRow.PdIndex].sJustification == "R") {
                                            PickRow.PickDictArray[PickRow.PdIndex].ColumnNumericPoints += 15;
                                        }
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnType = ColTypeDef.ColumnISNUMERIC;
                                    }
                                    // Input Conversion
                                    if (PickRow.PickDictArray[PickRow.PdIndex].InputConvType == "D") {
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnType = ColTypeDef.ColumnISDATE;
                                    } else if (PickRow.PickDictArray[PickRow.PdIndex].InputConvType == "M") {
                                        if (PickRow.PickDictArray[PickRow.PdIndex].InputConvSubType == "D") {
                                            PickRow.PickDictArray[PickRow.PdIndex].ColumnNumericPoints += 15;
                                            PickRow.PickDictArray[PickRow.PdIndex].ColumnType = ColTypeDef.ColumnISNUMERIC;
                                        }
                                    } else {
                                        //
                                    }
                                    // Output Conversion
                                    if (PickRow.PickDictArray[PickRow.PdIndex].spOutputConvType == "D") {
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnType = ColTypeDef.ColumnISDATE;
                                    } else if (PickRow.PickDictArray[PickRow.PdIndex].spOutputConvType == "M") {
                                        if (PickRow.PickDictArray[PickRow.PdIndex].spOutputConvSubType == "D") {
                                            PickRow.PickDictArray[PickRow.PdIndex].ColumnNumericPoints += 15;
                                            PickRow.PickDictArray[PickRow.PdIndex].ColumnType = ColTypeDef.ColumnISNUMERIC;
                                        }
                                    } else {
                                        //
                                    }
                                }
                                //
                                // xxxxxxxxxxxxxxxxxxxxxxxxxx
                                //
                                // Sql Column Created
                                // 
                                // if (!PickRow.PickDictArray[PickRow.PdIndex].DictColumnIdDone) {
                                // Id must be numeric and column referenced will also be numeric
                                if (PickRow.PickDictArray[PickRow.PdIndex].AttrTwoIsNumeric) {
                                    // PickRow.PickDictArray[PickRow.PdIndex].DictColumnTouched += 1;

                                    // public int[] iItemAttrCounter;
                                    //public int[] iItemLength;
                                    //
                                    PickRow.PickDictArray[PickRow.PdIndex].iItemLength = Item.ItemData.Length;
                                    PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter = PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter;
                                    if (PickRow.PickDictArray[PickRow.PdIndex].DictColIndex == 0 && PickRow.PickDictArray[PickRow.PdIndex].ItemId.ToUpper() == "ID") {
                                        // Dictionary +50
                                    }
                                    if (PickRow.PickDictArray[PickRow.PdIndex].DictColIndex == 0 && PickRow.PickDictArray[PickRow.PdIndex].sHeading.ToUpper() == FileSummary.FileName.ToUpper()) {
                                        // Dictionary +50
                                    }
                                    // length will always be > 0 or error
                                    if (PickRow.PickDictArray[PickRow.PdIndex].ColumnWidth > 0) {
                                        //
                                        // input & output conversion and correlative must all be empty
                                        if ((PickRow.PickDictArray[PickRow.PdIndex].InputConversion.Length + PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion.Length + PickRow.PickDictArray[PickRow.PdIndex].sCorrelative.Length) == 0) {
                                            // if (PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength == 0) {
                                            //     PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength = PickRow.PickDictArray[PickRow.PdIndex].ColumnWidth;
                                            // }
                                            // Dictionary's Lenght will be used.
                                            // Controlling ID will always be used.
                                            //
                                            // Determination of Defining Dictionary Column
                                            // ID - Numeric
                                            // Attr - Numeric
                                            // Heading - not relevant ( = File Name +10) ( numeric = numerice ID +20)
                                            // Input Conversion - empty
                                            // Output Conversion - empty
                                            // Correlative - empty
                                            // Justification - not relevant (L usually +10)
                                            //
                                            // Length - must be > 0)
                                            // 
                                            if (PickRow.PickDictArray[PickRow.PdIndex].ItemId == PickRow.PickDictArray[PickRow.PdIndex].DictColIndex.ToString() || PickRow.PickDictArray[PickRow.PdIndex].sHeading == PickRow.PickDictArray[PickRow.PdIndex].DictColIndex.ToString()) {
                                                PickRow.PickDictArray[PickRow.PdIndex].DictColumnDefinitionFound = true;
                                                PickRow.PickDictArray[PickRow.PdIndex].DictColumnIdDone = true;
                                                // if (PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength < PickRow.PickDictArray[PickRow.PdIndex].ColumnWidth) {
                                                //    PickRow.PickDictArray[PickRow.PdIndex].DictColumnLengthChange = true;
                                                PickRow.PickDictArray[PickRow.PdIndex].iItemLength = Item.ItemData.Length;
                                                PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter = PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter;

                                                // was PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength = PickRow.PickDictArray[PickRow.PdIndex].ColumnWidth;
                                                // if (PickRow.PickDictArray[PickRow.PdIndex].ItemIdFoundNumericPk) {
                                                // PickRow.PickDictArray[PickRow.PdIndex].DictColumnDefinitionFound = true;
                                                // }
                                                // }
                                                // +50
                                                // need to store item id of this Attr
                                                // also, it does not matter if Heading matches
                                                // the Attr number here.
                                                // A matching heading to Attr always means that
                                                // we have the candidate key of the dictionary.
                                                // For example: Id = "3", Attr = 3 is a clear
                                                // case of having the formal definition for dictionary
                                                // item "3".
                                            } else {
                                                if (PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength < PickRow.PickDictArray[PickRow.PdIndex].ColumnWidth) {
                                                    // TODO THIS CODE SEEMS WRONG HERE vvv
                                                    if (PickRow.PickDictArray[PickRow.PdIndex].DictColumnDefinitionFound) {
                                                        PickRow.PickDictArray[PickRow.PdIndex].DictColumnLengthChange = true;
                                                    } else {
                                                        PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength = PickRow.PickDictArray[PickRow.PdIndex].ColumnWidth;
                                                        PickRow.PickDictArray[PickRow.PdIndex].DictColumnIdDone = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                // }
                                // Column Attribue ItemData Type
                                PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "vchar";
                                PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = false;
                                switch (PickRow.PickDictArray[PickRow.PdIndex].ColumnType) {
                                    case (ColTypeDef.ColumnISNUMERIC):
                                        PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "tinyint";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = true;
                                        // bigint   = 8 bytes = 2^63-1      (9,223,372,036,854,775,807)
                                        // int      = 4 bytes = 2^31 - 1    (2,147,483,647)
                                        // smallint = 2 bytes = 2^15 - 1    (32,767)
                                        // tinyint  = 1 bytes = 2^7 - 1     (255)
                                        if (PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength > 9) {
                                            PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "bigint";
                                        } else if (PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength > 4) {
                                            PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "int";
                                        } else if (PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength > 2) {
                                            PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "smallint";
                                        }
                                        break;
                                    case (ColTypeDef.ColumnISCURRENCY):
                                        // decimal
                                        // money
                                        // smallmoney
                                        // smallmoney = 214,748.3647
                                        if (PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength > 5) {
                                            PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "money";
                                        } else {
                                            PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "smallmoney";
                                        }
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = true;
                                        break;
                                    case (ColTypeDef.ColumnISDATE):
                                        PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "smalldatetime";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = false;
                                        break;
                                    case (ColTypeDef.ColumnISDATETIME):
                                        PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "smalldatetime";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = false;
                                        break;
                                    case (ColTypeDef.ColumnISCHAR):
                                        PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "nchar";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = true;
                                        break;
                                    case (ColTypeDef.ColumnISVARCHAR):
                                        PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "nvarchar";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = true;
                                        break;
                                    case (ColTypeDef.ColumnISCHARU):
                                        PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "nchar";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = true;
                                        break;
                                    case (ColTypeDef.ColumnISVARCHARU):
                                        PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "nvarchar";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = true;
                                        break;
                                    case (ColTypeDef.ColumnISINTEGER):
                                        PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "int";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = false;
                                        break;
                                    case (ColTypeDef.ColumnISFLOAT):
                                        PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "int";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = false;
                                        break;
                                    case (ColTypeDef.ColumnISNOTSET):
                                    case (ColTypeDef.ColumnISUNKNOWN):
                                        PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "nvarchar";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = false;
                                        break;
                                    default:
                                        PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "nvarchar";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = false;
                                        sLocalErrorMessage = "Native Type not set for File Dictionary Correlative Field";
                                        throw new NotSupportedException(sLocalErrorMessage);
                                }
                                break;
                            default:
                                PickRow.PickDictArray[PickRow.PdIndex].iAttrType = ColTypeDef.ITEM_ISDATA;
                                PickRow.ColumnInvalid = true;
                                PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "nvarchar";
                                PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = false;
                                sLocalErrorMessage = "Unrecognized File Dictionary Type Field";
                                throw new NotSupportedException(sLocalErrorMessage);
                        }
                        if (PickRow.PickDictArray[PickRow.PdIndex].iAttrType == ColTypeDef.ITEM_ISNOTSET) {
                            PickRow.ColumnInvalid = true;
                        }
                        if (PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter > 30 || PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints > 100) {
                            PickRow.PickDictArray[PickRow.PdIndex].iAttrType = ColTypeDef.ITEM_ISDATA;
                            PickRow.ColumnInvalid = true;
                        }
                    } // on not ColTypeDef.ITEM_ISDATA
                    if (PickRow.PickDictArray[PickRow.PdIndex].iAttrType != ColTypeDef.ITEM_ISDICT) {
                        PickRow.PickDictArray[PickRow.PdIndex].bAttrIsDict = false;
                        PickRow.PickDictArray[PickRow.PdIndex].bAttrIsData = true;
                    }
                    //
                    if (PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints > 20 || PickRow.ColumnDataPoints > 20 || PickRow.ColumnInvalid || PickRow.PickDictArray[PickRow.PdIndex].iAttrType == ColTypeDef.ITEM_ISDATA) {
                        if (PickRow.ColumnInvalid || PickRow.PickDictArray[PickRow.PdIndex].iAttrType == ColTypeDef.ITEM_ISDATA) {
                            PickRow.PdErrorCount += 1;
                            sLocalErrorMessage = "This file item is DATA, not a valid dictionary column item!!!";
                            XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                        } else {
                            PickRow.PdErrorWarningCount += 1;
                            sLocalErrorMessage = "This file item might be DATA and not a valid dictionary column item!!!";
                            XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                        }
                    }
                    //
                    iSqlFileDictInsert = PickRow.PickDictArray[PickRow.PdIndex].iAttrType;
                    sTemp = "";
                    sTemp += Item.ItemId + ": " + ColIndex.ColArray[3] + " " + ColIndex.ColArray[2];
                    sTemp += ", Primary Key ";
                    sTemp += ", Touched (" + PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo].DictColumnTouched.ToString() + ")";
                    sTemp += ", Stored at: " + PickRow.PdIndex.ToString();
                    sTemp += ", Type: " + PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord;
                    sTemp += ", [Type: " + PickRow.PickDictArray[PickRow.PdIndex].ColumnType;
                    sTemp += ", SubType: " + PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType + "]";
                    sTemp += ", W" + PickRow.PickDictArray[PickRow.PdIndex].ColumnWidth;
                    sTemp += ", J" + PickRow.PickDictArray[PickRow.PdIndex].sJustification;
                    sTemp += ", Dp" + PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints;
                    sTemp += ", Np" + PickRow.PickDictArray[PickRow.PdIndex].ColumnNumericPoints;
                    sTemp += ", W" + PickRow.PickDictArray[PickRow.PdIndex].ColumnType;
                    MessageMdmSendToPageNewLine(ref Sender, "A2" + sTemp);
                } // end of invalid column
            } else {
                // PdErrorCount += 1;
                sLocalErrorMessage = "This file item is DATA, it is not a valid dictionary column item and was not added!";
                XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), iSqlFileDictInsert, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
            } // end of ColTypeDef.ITEM_ISDATA not set
            return iSqlFileDictInsert;
        }
        //
        public long SqlColAddCmdBuildAllFromArray(String OutputCommandPassed) {
            iSqlColAddCmdBuildAllFromArray = (int)DatabaseControl.ResultStarted;
            DbSyn.OutputCommand = OutputCommandPassed;
            DbIo.CommandCurrent = OutputCommandPassed;
            bool SqlColumnReloop = false;
            String sSqlColumnBuildOption = "ADD";
            iSqlColAddCmdBuildAllFromArray = (int)DatabaseControl.ResultOperationInProgress;
            //
            // TODO SqlColAddCmdBuildAllFromArray build shema creation commands 
            // TODO SqlColAddCmdBuildAllFromArray TO WRITE OUT DICTIONARY
            // TODO SqlColAddCmdBuildAllFromArray 95% done
            //
            for (int iPassIndex = 1; iPassIndex <= 2; iPassIndex++) {
                DbSyn.spOutputAlterScript = "";
                DbSyn.bpOutputAlterScriptFirst = true;
                DbSyn.spSqlColumnViewScript = "";
                DbSyn.bpSqlColumnViewCmdFirst = true;
                // PickRow.PdIndex = -2;
                // PickRow.PickDictArray[PickRow.PdIndex]
                for (PickRow.PdIndex = 0; PickRow.PdIndex < PickRowDef.PdIndexMax; PickRow.PdIndex++) {
                    iSqlColAddCmdBuildAllFromArray = (int)DatabaseControl.ResultOperationInProgress;
                    if (PickRow.PickDictArray[PickRow.PdIndex].ColumnInvalid <= 0) {
                        if (PickRow.PickDictArray[PickRow.PdIndex].bAttrIsDict) {
                            // Valid Dict Item in this spot
                            if (!PickRow.PickDictArray[PickRow.PdIndex].DictColumnIdDone) {
                                // Warning or error here
                            } // Column Id Done
                            //
                            if (PickRow.PickDictArray[PickRow.PdIndex].DictColumnDefinitionFound) {
                                // Warning or error here
                            }
                            //
                            // xxxxxxxxxxxxxxxxx BUILD COMMAND HERE xxxxxxxxxxxxxxxxx
                            //
                            if (iPassIndex == 1) {
                                iSqlColAddCmdBuildAllFromArray = SqlColAddCmdBuild(
                                    ref DbIo.SqlDbDataReaderObject,
                                    ref DbIo.SqlDbDataWriterObject,
                                    (RowInfoDef)ColInfoDb,
                                    (ColInfoDef)ColInfo,
                                    true, true, (int)ColInfoDef.SFC_SET_ColumnADD_CMD,
                                    sSqlColumnBuildOption, (int)PickRow.PdIndex, (int)PickRowDef.PdIndexMax);
                                if (iSqlColAddCmdBuildAllFromArray == (int)DatabaseControl.ResultOK) {
                                    if (PickRow.PickDictArray[PickRow.PdIndex].sColumnAdd.Length > 0) {
                                        if (true == false) {
                                            if (DbSyn.bpOutputAlterScriptFirst) {
                                                DbSyn.bpOutputAlterScriptFirst = false;
                                                // DbSyn.spOutputAlterScript += sSqlColumnBuildOption;
                                            } else { DbSyn.spOutputAlterScript += DbSyn.spOutputAlterListSeparatorChar + " "; }
                                        } else {
                                            DbSyn.bpOutputAlterScriptFirst = false;
                                            DbSyn.spOutputAlterScript = "";
                                        }
                                        DbSyn.spOutputAlterScript += PickRow.PickDictArray[PickRow.PdIndex].sColumnAdd;
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnAdd = false;
                                        LocalMessage1 = "Column Add: " + PickRow.PickDictArray[PickRow.PdIndex].sColumnAdd;
                                        MessageMdmSendToPageNewLine(ref Sender, "A2" + LocalMessage1);
                                        if (iSqlColAddCmdBuildAllFromArray != (int)DatabaseControl.ResultFailed) {
                                            iSqlColAddCmdBuildAllFromArray = SqlColAddCmdBuildAddFromArray(OutputCommandPassed);
                                            LocalMessage = "Table " + sSqlColumnBuildOption + " " + DbSyn.spSqlColumnAddCmd;
                                            MessageMdmSendToPageNewLine(ref Sender, "A2" + LocalMessage);
                                        }
                                        iSqlColAddCmdBuildAllFromArray = (int)DatabaseControl.ResultOperationInProgress;
                                    } // of add length
                                } // of no error
                            } else {
                                if (PickRow.PickDictArray[PickRow.PdIndex].sColumnView.Length > 0) {
                                    if (true == true) {
                                        if (DbSyn.bpSqlColumnViewCmdFirst) {
                                            DbSyn.bpSqlColumnViewCmdFirst = false;
                                            // spSqlColumnViewScript += sSqlColumnBuildOption;
                                        } else { DbSyn.spSqlColumnViewScript += DbSyn.spOutputAlterListSeparatorChar + " "; }
                                        DbSyn.spSqlColumnViewScript += PickRow.PickDictArray[PickRow.PdIndex].sColumnView;
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnView = false;
                                    } else {
                                        DbSyn.spSqlColumnViewScript = PickRow.PickDictArray[PickRow.PdIndex].sColumnView;
                                        PickRow.PickDictArray[PickRow.PdIndex].ColumnView = false;
                                    }
                                    LocalMessage1 = "View Column Add: " + PickRow.PickDictArray[PickRow.PdIndex].sColumnView;
                                }
                            }
                            //
                            MessageMdmSendToPageNewLine(ref Sender, "A2" + sSqlColumnBuildOption + " " + LocalMessage1);
                            LocalMessage = "Dict Column Build from Array: Pass (" + iPassIndex.ToString() + "), Item (" + (PickRow.PdIndex + 0).ToString() + ").";
                            MessageMdmSendToPageNewLine(ref Sender, "A2" + LocalMessage);
                            iSqlColAddCmdBuildAllFromArray = (int)DatabaseControl.ResultOK;
                            //
                        } // is dict
                    } // is not invalid
                } // reloop on for PickRow.PdIndex
                if (iPassIndex == 2 && iSqlColAddCmdBuildAllFromArray != (int)DatabaseControl.ResultFailed) {
                    iSqlColAddCmdBuildAllFromArray = SqlColAddCmdBuildViewFromArray(OutputCommandPassed);
                }
            } // reloop for second pass
            return iSqlColAddCmdBuildAllFromArray;
        }
        //
        public long SqlColAddCmdBuildAddFromArray(String OutputCommandPassed) {
            iSqlColAddCmdBuildAddFromArray = (int)DatabaseControl.ResultStarted;
            bool SqlColumnReloop = false;
            String sSqlColumnBuildOption = "";
            // Execute command
            iSqlColAddCmdBuildAddFromArray = (int)DatabaseControl.ResultOperationInProgress;
            sSqlColumnBuildOption = "ADD";
            SqlColumnReloop = false;
            do {
                // xxxxxxxxxxxxxxxxxxxxxxxxxx
                // Table Build Command
                if (SqlColumnReloop) { sSqlColumnBuildOption = "ALTER COLUMN"; }
                //
                try {
                    if (!DbSyn.bpOutputAlterScriptFirst) {
                        // Add Column Command Build 
                        DbSyn.spSqlColumnAddCmd = "";
                        // Table
                        if (SqlColumnReloop) {
                            DbSyn.spSqlColumnAddCmd = "ALTER";
                        } else {
                            // spSqlColumnAddCmd = "ADD";
                            DbSyn.spSqlColumnAddCmd = "ALTER";
                        }
                        DbSyn.spSqlColumnAddCmd += " TABLE ";
                        if (DbSyn.bpOutputAlterColumnNameQuoted) {
                            DbSyn.spSqlColumnAddCmd += DbSyn.spOutputAlterQuoteCharLeft;
                        }
                        DbSyn.spSqlColumnAddCmd += FileId.FileNameFull;
                        if (DbSyn.bpOutputAlterColumnNameQuoted) {
                            DbSyn.spSqlColumnAddCmd += DbSyn.spOutputAlterQuoteCharRight;
                        }
                        DbSyn.spSqlColumnAddCmd += " " + sSqlColumnBuildOption + " " + DbSyn.spOutputAlterScript + ";";
                        // spSqlColumnAddCmd += " " + DbSyn.spOutputAlterScript + ";";
                        //
                        iSqlColAddCmdBuildAllFromArray = (int)DatabaseControl.ResultOK;
                        MessageMdmSendToPageNewLine(ref Sender, "A2" + DbSyn.spSqlColumnAddCmd);
                        // sTraceTemp + sProcessHeading + ", " + sProcessSubHeading + ": " + 
                        //
                        if (!DbIo.SqlDbDataReaderObject.IsClosed) {
                            DbIo.SqlDbDataReaderObject.Close();
                        }
                        //
                        if (DbIo.SqlDbCommandObject != null) {
                            // DbIo.SqlDbCommandObject.Dispose();
                        }
                        if (DbIo.SqlDbConnection.State.ToString() != "Open") {
                            iSqlColAddCmdBuild = ConnOpen(ref DbIo.SqlDbConnection, ref FileSummary.spFileName, ref FileId.spFileNameFull);
                        }
                        // Execute Add Column Command
                        if (DbIo.SqlDbConnection.State.ToString() != "Open") {
                            iSqlColAddCmdBuild = ConnOpen(ref DbIo.SqlDbConnection, ref FileSummary.spFileName, ref FileId.spFileNameFull);
                        }
                        DbIo.SqlDbCommandObject = new SqlCommand(DbSyn.spSqlColumnAddCmd, DbIo.SqlDbConnection);
                        iSqlColAddCmdBuild = DbIo.SqlDbCommandObject.ExecuteNonQuery();
                        //
                        PickRow.PickDictArray[PickRow.PdIndex].ColumnView = true;
                        LocalMessage = "Dict Column Build from Array: Add successful";
                        MessageMdmSendToPageNewLine(ref Sender, "A2" + LocalMessage);
                        SqlColumnReloop = false;
                        PickRow.PickDictArray[PickRow.PdIndex].ColumnAdd = true;
                    }
                    //
                } catch (SqlException ExceptionSql) {
                    sLocalErrorMessage = "";
                    ExceptSql(sLocalErrorMessage, ref ExceptionSql, iSqlColAddCmdBuildAllFromArray);
                    if (!SqlColumnReloop) {
                        SqlColumnReloop = true;
                    } else {
                        SqlColumnReloop = false;
                    }
                    iSqlColAddCmdBuildAllFromArray = (int)DatabaseControl.ResultFailed;
                } catch (Exception ExceptionGeneral) {
                    sLocalErrorMessage = "";
                    ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iSqlColAddCmdBuildAllFromArray);
                    if (!SqlColumnReloop) {
                        SqlColumnReloop = true;
                    } else {
                        SqlColumnReloop = false;
                    }
                    iSqlColAddCmdBuildAllFromArray = (int)DatabaseControl.ResultFailed;
                } finally {
                    DbIo.SqlDbCommandObject = null;
                }
            } while (SqlColumnReloop);
            // xxxxxxxxxxxxxxxxxxxxxxxxxx
            // View Biuld
            // View Base command
            return iSqlColAddCmdBuildAddFromArray;
        }
        //
        public long SqlColAddCmdBuildViewFromArray(String OutputCommandPassed) {
            iSqlColAddCmdBuildViewFromArray = (int)DatabaseControl.ResultStarted;
            bool SqlColumnReloop = false;
            String sSqlColumnBuildOption = "ADD";
            iSqlColAddCmdBuildAllFromArray = (int)DatabaseControl.ResultOperationInProgress;
            do {
                if (SqlColumnReloop) { sSqlColumnBuildOption = "ALTER"; }
                try {
                    if (!DbSyn.bpSqlColumnViewCmdFirst) {
                        // View Base command
                        DbSyn.spSqlColumnViewCmdPrefix = "";
                        //
                        if (SqlColumnReloop) {
                            DbSyn.spSqlColumnViewCmdPrefix += "ALTER";
                        } else {
                            DbSyn.spSqlColumnViewCmdPrefix += "CREATE";
                            // spSqlColumnViewCmdPrefix += "ALTER";
                        }
                        DbSyn.spSqlColumnViewCmdPrefix += " VIEW ";
                        if (DbSyn.bpOutputAlterColumnNameQuoted) {
                            DbSyn.spSqlColumnViewCmdPrefix += DbSyn.spOutputAlterQuoteCharLeft;
                        }
                        DbSyn.spSqlColumnViewCmdPrefix += FileSummary.FileName;
                        DbSyn.spSqlColumnViewCmdPrefix += "View";
                        if (DbSyn.bpOutputAlterColumnNameQuoted) {
                            DbSyn.spSqlColumnViewCmdPrefix += DbSyn.spOutputAlterQuoteCharRight;
                        }
                        DbSyn.spSqlColumnViewCmdPrefix += " AS SELECT";
                        // All
                        // xxxxxxxxxxxxxxxxxx spSqlColumnViewCmd = ""; //  " * "
                        // Suffix
                        // View of Table
                        DbSyn.spSqlColumnViewCmdSuffix = " FROM ";
                        if (DbSyn.bpOutputAlterColumnNameQuoted) {
                            DbSyn.spSqlColumnViewCmdSuffix += DbSyn.spOutputAlterQuoteCharLeft;
                        }
                        DbSyn.spSqlColumnViewCmdSuffix += FileId.FileNameFull;
                        if (DbSyn.bpOutputAlterColumnNameQuoted) {
                            DbSyn.spSqlColumnViewCmdSuffix += DbSyn.spOutputAlterQuoteCharRight;
                        }
                        DbSyn.spSqlColumnViewCmd = " " + DbSyn.spSqlColumnViewCmdPrefix + " " + DbSyn.spSqlColumnViewScript + DbSyn.spSqlColumnViewCmdSuffix + ";";
                        //
                        MessageMdmSendToPageNewLine(ref Sender, "A2" + DbSyn.spSqlColumnViewCmd);
                        //
                        try {
                            if (!DbIo.SqlDbDataReaderObject.IsClosed) {
                                DbIo.SqlDbDataReaderObject.Close();
                            }
                        } catch { ; }
                        //
                        if (DbIo.SqlDbCommandObject != null) {
                            // DbIo.SqlDbCommandObject.Dispose();
                        }
                        // XXXXXXXXXXXXXX BUILD COMMAND FIRST
                        // Execute Add Column Command
                        if (DbIo.SqlDbConnection.State.ToString() != "Open") {
                            if (DbIo.spConnString.Length == 0) {
                                iConnOpen = ConnCmdBuild(ref FileSummary.spSystemName, ref FileSummary.spDatabaseName, ref FileSummary.spServerName, ref FileSummary.spServiceName);
                            }
                            // 
                            iSqlColAddCmdBuild = ConnOpen(ref DbIo.SqlDbConnection, ref FileSummary.spFileName, ref FileId.spFileNameFull);
                        }
                        DbIo.SqlDbCommandObject = new SqlCommand(DbSyn.spSqlColumnViewCmd, DbIo.SqlDbConnection);
                        iSqlColAddCmdBuild = DbIo.SqlDbCommandObject.ExecuteNonQuery();
                        SqlColumnReloop = false;
                        //
                        LocalMessage = "Dict Column Build from Array: Add View successful";
                        MessageMdmSendToPageNewLine(ref Sender, "A2" + LocalMessage);
                        iSqlColAddCmdBuildAllFromArray = (int)DatabaseControl.ResultOK;
                    } // of not first try
                } catch (SqlException ExceptionSql) {
                    sLocalErrorMessage = "";
                    ExceptSql(sLocalErrorMessage, ref ExceptionSql, iSqlColAddCmdBuildAllFromArray);
                    if (!SqlColumnReloop) {
                        SqlColumnReloop = true;
                    } else {
                        SqlColumnReloop = false;
                    }
                    iSqlColAddCmdBuildAllFromArray = (int)DatabaseControl.ResultFailed;
                } catch (Exception ExceptionGeneral) {
                    sLocalErrorMessage = "";
                    ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iSqlColAddCmdBuildAllFromArray);
                    if (!SqlColumnReloop) {
                        SqlColumnReloop = true;
                    } else {
                        SqlColumnReloop = false;
                    }
                    iSqlColAddCmdBuildAllFromArray = (int)DatabaseControl.ResultFailed;
                } finally {
                    DbIo.SqlDbCommandObject = null;
                }
            } while (SqlColumnReloop);
            return iSqlColAddCmdBuildViewFromArray;
        }
        //
        public long SqlColClear(int iPassedPdIndex) {
            iSqlColClear = (int)DatabaseControl.ResultStarted;

            // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            //
            // ColArray[Am] = ColText;
            //
            // xxxxxxxxxxxxxxxxxxxxxxx
            // Id
            // PickRow.PickDictArray[PickRow.PdIndex].ItemId = "";
            // PickRow.PickDictArray[PickRow.PdIndex].ItemIntId = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter = 0;

            // Attr ItemData Type
            // PickRow.PickDictArray[PickRow.PdIndex].sAttrType = "";
            // PickRow.PickDictArray[PickRow.PdIndex].iAttrType = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].sType = "";
            // PickRow.PickDictArray[PickRow.PdIndex].sSubType = "";
            // PickRow.PickDictArray[PickRow.PdIndex].bAttrIsData = false;
            // PickRow.PickDictArray[PickRow.PdIndex].bAttrIsDict = false;
            // Attr / Column Number
            // PickRow.PickDictArray[PickRow.PdIndex].AttrTwoIsNumeric = false;

            // Column Type
            // PickRow.PickDictArray[PickRow.PdIndex].ColumnType = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColumnInvalid = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColumnNumericPoints = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColumnDecimals = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColumnCurrencyPoints = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColumnDateFormat = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColumnFunctionPoints = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColumnSuFile = false;
            // PickRow.PickDictArray[PickRow.PdIndex].DictColumnTouched = new int[100];
            // PickRow.PickDictArray[PickRow.PdIndex].DictColumnIdDone = new bool[100];
            // PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength = new int[100];
            // PickRow.PickDictArray[PickRow.PdIndex].DictColumnLengthChange = new bool[100];
            // PickRow.PickDictArray[PickRow.PdIndex].DictColumnDefinitionFound = new bool[100];
            //
            // 0
            PickRow.PickDictArray[PickRow.PdIndex].ItemId = "";
            PickRow.PickDictArray[PickRow.PdIndex].ItemIntId = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ItemIdConverted = "";

            PickRow.PickDictArray[PickRow.PdIndex].iItemAttrIndex = 0;  // Field being processed in the Dict Column
            PickRow.PickDictArray[PickRow.PdIndex].ItemIdIsNumeric = false;
            // PK
            PickRow.PickDictArray[PickRow.PdIndex].ItemIdFoundNumericPk = false;
            // PickRow.PickDictArray[PickRow.PdIndex].DictColumnTouched = 0;
            PickRow.PickDictArray[PickRow.PdIndex].DictColumnIdDone = false;
            // 1 Type for valid dict items
            // Type
            // PickRow.PickDictArray[PickRow.PdIndex].ColumnInvalid = 0;
            PickRow.PickDictArray[PickRow.PdIndex].sAttrType = "";
            PickRow.PickDictArray[PickRow.PdIndex].iAttrType = 0;
            // Type
            PickRow.PickDictArray[PickRow.PdIndex].sType = "";
            PickRow.PickDictArray[PickRow.PdIndex].sColumnTypeWord = "";
            // Sub Type
            PickRow.PickDictArray[PickRow.PdIndex].sSubType = "";
            // Column pointers and indexing
            PickRow.PickDictArray[PickRow.PdIndex].DictColIndex = 0;
            PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter = 0; // DatabaseFileOpen  How many Fields (size of)
            PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter = 0;
            PickRow.PickDictArray[PickRow.PdIndex].iItemLength = 0;
            // 2
            PickRow.PickDictArray[PickRow.PdIndex].AttrTwoStringValue = "";
            PickRow.PickDictArray[PickRow.PdIndex].PdIndexAttrTwo = 0;
            PickRow.PickDictArray[PickRow.PdIndex].AttrTwoIsNumeric = false;
            // PK
            PickRow.PickDictArray[PickRow.PdIndex].DictColumnTouched = 0;
            PickRow.PickDictArray[PickRow.PdIndex].DictColumnIdDone = false;
            PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength = 0;
            PickRow.PickDictArray[PickRow.PdIndex].DictColumnLengthChange = false;
            PickRow.PickDictArray[PickRow.PdIndex].DictColumnDefinitionFound = false;
            // Alias account, dict, file, and conversion pointers
            PickRow.PickDictArray[PickRow.PdIndex].ColumnSuFile = false;
            PickRow.PickDictArray[PickRow.PdIndex].AttrTwoStringAccounName = "";
            PickRow.PickDictArray[PickRow.PdIndex].AttrThreeFileName = "";
            // global - Type for ANY item
            PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber = "";
            PickRow.PickDictArray[PickRow.PdIndex].bAttrIsData = false;
            PickRow.PickDictArray[PickRow.PdIndex].bAttrIsDict = false;
            // ranking for Type
            // ranking analysis and accumulation fields
            PickRow.PickDictArray[PickRow.PdIndex].ColumnDataPoints = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColumnNumericPoints = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColumnDecimals = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColumnCurrencyPoints = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColumnDateFormat = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColumnFunctionPoints = 0;
            // length fields ???
            PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength = 0;
            PickRow.PickDictArray[PickRow.PdIndex].DictColumnLengthChange = false;

            // 3
            // Heading or name of aliased tiem
            PickRow.PickDictArray[PickRow.PdIndex].sHeading = "";

            // Depedancy
            // Dependancy (4) (5)
            PickRow.PickDictArray[PickRow.PdIndex].sDependancy = "";
            PickRow.PickDictArray[PickRow.PdIndex].bDependancy = false;
            PickRow.PickDictArray[PickRow.PdIndex].iDendancyKeyColumn = 0;
            PickRow.PickDictArray[PickRow.PdIndex].sDendancyKeyList = "";

            // 4
            // Can be used for controlling dependant
            // Controlling column
            PickRow.PickDictArray[PickRow.PdIndex].sFour = "";

            // 5
            // Can be used for controlling dependant
            // List of dependant columns
            PickRow.PickDictArray[PickRow.PdIndex].sFive = "";

            // input and output conversions, correlatives
            // 6
            // Input Conversion
            PickRow.PickDictArray[PickRow.PdIndex].InputConversion = "";
            PickRow.PickDictArray[PickRow.PdIndex].InputConvType = "";
            PickRow.PickDictArray[PickRow.PdIndex].InputConvSubType = "";

            // 7
            // Output Conversion
            PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion = "";
            PickRow.PickDictArray[PickRow.PdIndex].spOutputConvType = "";
            PickRow.PickDictArray[PickRow.PdIndex].spOutputConvSubType = "";

            // 8
            // Correlative
            PickRow.PickDictArray[PickRow.PdIndex].sCorrelative = "";
            PickRow.PickDictArray[PickRow.PdIndex].sCorrType = "";
            PickRow.PickDictArray[PickRow.PdIndex].sCorrSubType = "";

            // 9
            // Justification
            PickRow.PickDictArray[PickRow.PdIndex].sJustify = "";
            PickRow.PickDictArray[PickRow.PdIndex].sJustification = "";
            PickRow.PickDictArray[PickRow.PdIndex].sJustifyType = "";

            // 10
            // Length
            PickRow.PickDictArray[PickRow.PdIndex].ColumnWidthString = "";
            PickRow.PickDictArray[PickRow.PdIndex].ColumnWidth = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColumnWidthIsNumeric = false;

            // Heading Long
            PickRow.PickDictArray[PickRow.PdIndex].sHeadingLong = "";
            // Help Short
            PickRow.PickDictArray[PickRow.PdIndex].sHelpShort = "";
            // Revelation RegG ARev Heading
            PickRow.PickDictArray[PickRow.PdIndex].sRevColumnName = "";
            //
            PickRow.PickDictArray[PickRow.PdIndex].ColumnUseParenthesis = false;
            //
            return iSqlColClear;
        }
        //
        public long SqlFileDictArrayInsertBuild(String OutputCommandPassed) {
            iSqlFileDictInsertBuild = (int)DatabaseControl.ResultStarted;
            DbSyn.OutputCommand = OutputCommandPassed;
            DbIo.CommandCurrent = OutputCommandPassed;
            //
            // TODO z$RelVs? SqlFileDictArrayInsertBuild build command
            //
            return iSqlFileDictInsertBuild;
        }
        //
        public long SqlFileDictArrayInsertDo(String OutputCommandPassed) {
            iSqlFileDictInsertDo = (int)DatabaseControl.ResultStarted;
            // OutputCommandPassed = OutputCommandPassed;
            DbIo.CommandCurrent = OutputCommandPassed;
            //
            //
            // Perform Command
            //
            DbIo.SqlDbCommandObject = new SqlCommand(OutputCommandPassed, DbIo.SqlDbConnection);
            //
            iSqlFileDictInsertDo = (int)DatabaseControl.ResultOperationInProgress;
            try {
                iSqlFileDictInsertDo = DbIo.SqlDbCommandObject.ExecuteNonQuery();
            } catch (SqlException ExceptionSql) {
                sLocalErrorMessage = "";
                ExceptSql(sLocalErrorMessage, ref ExceptionSql, iSqlFileDictInsertDo);
                FileStatus.FileDoesExist = false;
            } catch (Exception ExceptionGeneral) {
                sLocalErrorMessage = "";
                ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iSqlFileDictInsertDo);
                FileStatus.FileDoesExist = false;
            } finally {
                DbIo.SqlDbCommandObject = null;
            }
            return iSqlFileDictInsertDo;
        }
        //
        #endregion
        #region SqlFileDictArrayInsert empty
        #endregion
        #region SqlFileDictDelete empty
        #endregion
        #endregion
        #endregion
        // Text ItemData File - xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        // TODO z$RelVs$ PICK INCLUDES CSharp - CONTINUE INCLUDES
        #region TextFile
        // <Section Id = "TextFileCheckAndSetDoesExist">
        public bool TextFileCheckAndSetDoesExist(ref String sPassedFileName, ref String sPassedFileNameFull, bool bPassedConnDoClose, bool bPassedConnDoDispose, bool bPassedFileDoClose) {
            iTextFileCheckAndSetDoesExist = (int)DatabaseControl.ResultStarted;
            FileStatus.bpFileDoesExist = false;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            switch (FileReadMode) {
                case ((int)DatabaseControl.ReadModeSQL):
                    if (FileId.FileNameFull.Length == 0) {
                        FileId.FileNameFull = SqlFileNameBuildFull(ref FileSummary.spDatabaseName, ref FileSummary.spFileOwner, ref FileSummary.spFileName, ref FileId.spFileNameFull);
                    }
                    FileStatus.iFileDoesExist = SqlFileCheckAndSetDoesExist(ref DbIo.SqlDbConnection, FileSummary.FileName, FileId.FileNameFull, bPassedConnDoClose, bPassedConnDoDispose, bPassedFileDoClose, ref ColInfoDb, ref ColInfo);
                    if (FileStatus.iFileDoesExist != (int)DatabaseControl.ResultOK) {
                        FileStatus.FileDoesExist = false;
                    }
                    iTextFileCheckAndSetDoesExist = FileStatus.iFileDoesExist;
                    break;
                case ((int)DatabaseControl.ReadModeBLOCK):
                case ((int)DatabaseControl.ReadModeLINE):
                case ((int)DatabaseControl.ReadModeAll):
                    // xxx full file name parsing
                    FileId.FileNameFull = FileSummary.FileName;
                    FileStatus.bpFileDoesExist = System.IO.File.Exists(FileSummary.FileName);
                    if (FileStatus.bpFileDoesExist == true) {
                        iTextFileCheckAndSetDoesExist = (int)DatabaseControl.ResultDoesExist;
                    } else {
                        iTextFileCheckAndSetDoesExist = (int)DatabaseControl.ResultDoesNotExist;
                    }
                    FileStatus.ipFileDoesExist = iTextFileCheckAndSetDoesExist;
                    break;
                default:
                    FileStatus.ipFileDoesExist = (int)DatabaseControl.ReadModeError;
                    sLocalErrorMessage = "File Read Method (" + FileReadMode.ToString() + ") is not set";
                    XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PickSystemCallStringResult, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                    throw new NotSupportedException(sLocalErrorMessage);
            }
            return FileStatus.bpFileDoesExist;
        }
        // <Section Id = "x
        public long TextFileOpen(ref String sPassedFileName, ref String sPassedFileNameFull) {
            iTextFileOpen = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            Buf.BytesRead = 0;
            Buf.BytesReadTotal = 0;
            Buf.BytesConverted = 0;
            Buf.BytesConvertedTotal = 0;
            iTextFileOpen = DatabaseFileOpen(ref DbIo.SqlDbConnection, ref FileSummary.spFileName, ref FileId.spFileNameFull);
            return iTextFileOpen;
        }
        // <Section Id = "TextFileClose">
        public long TextFileClose(ref String sPassedFileName, ref String sPassedFileNameFull) {
            iTextFileClose = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            // FileClose(FileSummary.FileName);
            // <Area Id = "close the file streams
            try {
                if (FileIo.DbFileStreamReaderObject != null) {
                    FileIo.DbFileStreamReaderObject.Close();
                }
                if (FileIo.DbFileStreamObject != null) {
                    FileIo.DbFileStreamObject.Close();
                }
                //close the file
                if (FileIo.DbFileObject != null) {
                    iTextFileClose = AsciiDataClear();
                    // <Area Id = "do destructor;
                }
                iTextFileClose = (int)DatabaseControl.ResultOK;
            } catch (SqlException ExceptionSql) {
                sLocalErrorMessage = "";
                ExceptSql(sLocalErrorMessage, ref ExceptionSql, iTextFileClose);
                ExceptDatabaseFileOpenError(ref ExceptionSql);
                iTextFileClose = (int)DatabaseControl.ResultFailed;
                // Exit Here
            } catch (Exception ExceptionGeneral) {
                sLocalErrorMessage = "";
                ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iTextFileClose);
                DatabaseFilExceptionGeneralError(ref ExceptionGeneral);
                iTextFileClose = (int)DatabaseControl.ResultFailed;
            } finally { ; }
            //
            iTextFileClose = (int)DatabaseControl.ActionOK;
            return iTextFileClose;
        }
        // <Section Id = "TextFileWrite">
        public long TextFileWrite(ref String sPassedFileName, ref String sPassedFileNameFull) {
            iTextFileWrite = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            // FileWrite(FileName);
            iTextFileWrite = (int)DatabaseControl.ResultUnknownFailure;
            return iTextFileWrite;
        }
        // <Section Id = "TextFileReset">
        public long TextFileReset(ref String sPassedFileName, ref String sPassedFileNameFull) {
            iTextFileReset = (int)DatabaseControl.ResultStarted;
            // if (FileStatus.bpFileIsInitialized) {
            // THIS IS A DISPOSE FUNCTION
            FileStatus.bpFileIsInitialized = false;
            // }
            return iTextFileReset;
        }
        // <Section Id = "TextFileCreate">
        public long TextFileCreate(ref String sPassedFileName, ref String sPassedFileNameFull) {
            iTextFileCreate = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            iTextFileCreate = (int)DatabaseControl.ResultOperationInProgress;
            try {
                FileIo.DbFileStreamObject = File.Create(FileSummary.FileName);
                FileStatus.bpFileDoesExist = true;
                iTextFileCreate = (int)DatabaseControl.ResultOK;
            } catch (SqlException ExceptionSql) {
                sLocalErrorMessage = "";
                ExceptSql(sLocalErrorMessage, ref ExceptionSql, iTextFileCreate);
                iTextFileCreate = (int)DatabaseControl.ResultShouldNotExist;
                ExceptDatabaseFileOpenError(ref ExceptionSql);
                FileStatus.bpFileDoesExist = false;
                // Exit Here
            } catch (Exception ExceptionGeneral) {
                ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iTextFileCreate);
                DatabaseFilExceptionGeneralError(ref ExceptionGeneral);
                FileStatus.bpFileDoesExist = false;
                iTextFileCreate = (int)DatabaseControl.ResultOsError;
            } finally {
                FileStatus.ipStatusCurrent = iTextFileCreate;
                if (iTextFileCreate != (int)DatabaseControl.ResultOK) {
                    FileIo.DbFileStreamObject = null;
                }
            }
            return iTextFileCreate;
        }
        // <Section Id = "TextFileDelete">
        public long TextFileDelete(ref String sPassedFileName, ref String sPassedFileNameFull) {
            iTextFileDelete = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            // FileDelete(FileName);
            iTextFileDelete = (int)DatabaseControl.ResultUnknownFailure;
            return iTextFileDelete;
        }
        // <Section Id = "TextFileProcessMain">
        public long TextFileProcessMain(String sPassedFileNameRequest) {
            LocalIntResult = (int)DatabaseControl.ResultStarted;
            // <Area Id = "The files used here were created in the code example
            // in How to: Write to a Text File. You can of course substitute
            // other files of your own.

            // Example #1
            // bRead the file as one string.
            String IoLineAll = System.IO.File.ReadAllText(@"C:\Users\Public\TestFolder\WriteText.txt");

            // <Area Id = "Display the file contents to the Console.
            LocalMessage = "Contents of writeText.txt : " + IoLineAll;
            MessageMdmSendToPage(ref Sender, "A2" + LocalMessage);
            // Example #2
            // <Area Id = "bRead the file aslines into a String array.
            String[] LinesArray = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteLines2.txt");

            MessageMdmSendToPage(ref Sender, "Contents of writeLines2.txt: ");
            foreach (String sLine in LinesArray) {
                MessageMdmSendToPage(ref Sender, "\t" + sLine);
            }
            MessageMdmSendToPage(ref Sender, "C");

            // <Area Id = "Keep the Console window open in debug mode.
            MessageMdmSendToPage(ref Sender, "Press any key to exit.");
            System.Console.ReadKey();
            MessageMdmSendToPage(ref Sender, "C");

            return LocalIntResult;
        }
        #endregion
        #region FileDataReset
        // FileDataReset
        public long FileDataReset(ref Mfile ofPassedFileObject) {
            PickDataResetResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            //
            ofPassedFileObject.Item.ItemId = ""; // Reset
            if (IterationFirst) {
                ofPassedFileObject.FileSummary.ItemIdCurrent = ""; // Reset
                ofPassedFileObject.FileSummary.ItemIdNext = ""; // Reset
            }
            // TODO FileDataReset Reset Other Item ItemData
            // Item ItemData
            ofPassedFileObject.Item.ItemData = "";
            // FileDataReset
            return PickDataResetResult;
        }
        #endregion
        #region DictResetDataReset
        // TODO ColIndexReset needs work
        public long ColIndexReset(ref ColIndexDef ColIndexPassed) {
            PickDictResetResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // Index to field within a dict item
            ColIndexPassed.ColIndex = 0; // Pick Dict Reset
            ColIndexPassed.ColIndexTotal = 0; // Pick Dict Reset
            // Info about this dict item;
            ColIndexPassed.ColCount = 0; // Pick Dict Reset
            ColIndexPassed.ColCountTotal = 0;

            ColIndexPassed.ColInvalid = 0;
            // ColIndexPassed.ColumnInvalid = 0;
            // TODO Reset Other Item Dict ItemData
            ColIndexPassed.ColAttrIndex = 0;
            ColIndexPassed.ColAttrCount = 0; // ItemData Items in Item / Row / Item
            ColIndexPassed.ColAttrCountTotal = 0;
            ColIndexPassed.ColAttrCounter = 0; // Current Attr
            ColIndexPassed.ColAttrMaxIndex = 0; // Total Attrs in Item
            ColIndexPassed.ColAttrMaxIndexTemp = 0;

            // ColIndexReset
            return PickDictResetResult;
        }
        #endregion
        #region Output File Clear Dict & Current
        // FileDictClearCurrent
        public long FileDictClearCurrent(ref Mfile ofPassedFileObject) {
            PickDictClearCurrentResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // Clear dictionary array that contains this fields properties.
            Array.Clear(ofPassedFileObject.ColIndex.ColArray, 0, 100);
            #region Input Dictionay Index Pointers
            // FILE DICTIONARY FILE CONTROL
            // Reset Input File protected internal Attr pointer for next dict item
            ofPassedFileObject.ColIndex.ColIndex = 0; // dynamic protected internal pointer
            ofPassedFileObject.ColIndex.ColCount = 0; // Total
            ofPassedFileObject.ColIndex.ColCounter = 0; // Current for building
            // FILE DICTIONARY COLUMNS
            ofPassedFileObject.ColIndex.ColAttrIndex = 0;
            ofPassedFileObject.ColIndex.ColAttrCount = 0;
            ofPassedFileObject.ColIndex.ColAttrCounter = 1;
            ofPassedFileObject.ColIndex.ColAttrSet = false;
            ofPassedFileObject.ColIndex.ColAttrInvalid = 0;
            #endregion
            // Item ItemData
            ofPassedFileObject.Item.ItemData = "";
            // ItemData Item Type
            // OutputFile.FileSummary.FileTypeId = FileSummary.FileTypeId;
            // OutputFile.FileSummary.FileSubTypeId = FileSummary.FileSubTypeId;
            //
            // FileDictClearCurrent
            return PickDictClearCurrentResult;
        }
        // FileDataClearCurrent
        public long FileDataClearCurrent(ref Mfile ofPassedFileObject) {
            PickDataClearCurrentResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            //
            ofPassedFileObject.Item.ItemId = "";
            if (IterationFirst) { // CHECK & TODO FileDataClearCurrent
                ofPassedFileObject.FileSummary.ItemIdCurrent = "";
                // OutputFile.FileSummary.ItemIdNext = "";
            }
            #region Input ItemData Index Pointers
            // FILE DATA COLUMNS
            ofPassedFileObject.ItemAttrIndex = 0; // dynamic protected internal pointer
            ofPassedFileObject.ItemAttrCount = 0;
            ofPassedFileObject.ItemAttrCountTotal = 0;// Accumulator for shrinking work buffer
            ofPassedFileObject.ItemAttrCounter = 1;
            ofPassedFileObject.ItemAttrSet = false;
            ofPassedFileObject.ItemAttrInvalid = 0;
            // FILE DATA COLUMNS
            ofPassedFileObject.ColIndex.ColAttrIndex = 0; // dynamic protected internal pointer
            ofPassedFileObject.ColIndex.ColAttrCount = 0;
            ofPassedFileObject.ColIndex.ColAttrCountTotal = 0;// Accumulator for shrinking work buffer
            ofPassedFileObject.ColIndex.ColAttrCounter = 1;
            ofPassedFileObject.ColIndex.ColAttrSet = false;
            ofPassedFileObject.ColIndex.ColAttrInvalid = 0;
            // Reset Input File protected internal column processing pointer for next dict item
            ofPassedFileObject.ColIndex.ColIndex = 0;
            ofPassedFileObject.ColIndex.ColCount = 0;
            ofPassedFileObject.ColIndex.ColCountTotal = 0;// Accumulator for shrinking work buffer
            ofPassedFileObject.ColIndex.ColCounter = 0;
            ofPassedFileObject.ColIndex.ColSet = false;
            ofPassedFileObject.ColIndex.ColInvalid = 0;
            #endregion
            #region Item ItemData
            ofPassedFileObject.Item.ItemData = "";
            // ItemData Item Type
            ofPassedFileObject.FileSummary.FileTypeId =
                (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeUNKNOWN;
            ofPassedFileObject.FileSummary.FileSubTypeId =
                (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeUNKNOWN;
            #endregion
            #region InputItem
            ofPassedFileObject.Item.ItemData = ""; // File Item ItemData
            ofPassedFileObject.FileSummary.ItemIdCurrent = ""; // Current Id
            ofPassedFileObject.FileSummary.ItemIdNext = ""; // Next Id
            ofPassedFileObject.Item.ItemId = ""; // This Id
            ofPassedFileObject.Item.ItemIdIsChanged = bNO;
            ofPassedFileObject.FileStatus.FileNameIsChanged = bNO;
            // Import Input File Item Name
            // ** ColIndexPassed.FileSummary.FileNameDefault = "tld.import";
            #endregion
            #region Import Input File Options, Control and Modes
            // Import Output File Read and Access Modes
            ofPassedFileObject.FileReadMode = (int)DatabaseControl.ReadModeLINE; // TODO RunMain FileTransformControl initialize
            ofPassedFileObject.FileAccessMode = (int)DatabaseControl.ReadModeLINE; // TODO RunMain FileTransformControl initialize
            // Import Input File Options
            ofPassedFileObject.FileSummary.FileOptions = "F"; // TODO $$$CHECK 7 FileDataClearCurrent options hard corded here...
            #endregion
            #region Other Working Fields
            DataItemAtrributeClear(ref FileObject);
            // other
            ofPassedFileObject.ItemAttrMaxIndex = 0; // Total Attrs in Item
            ofPassedFileObject.ItemAttrMaxIndexTemp = 0; // Total Attrs in Item
            // Character Pointers
            DataItemCharClear(ref FileObject);
            // Buf.
            ofPassedFileObject.Buf.BytesRead = 0;
            ofPassedFileObject.Buf.BytesReadTotal = 0;
            ofPassedFileObject.Buf.CharIndex = 1;
            ofPassedFileObject.Buf.CharItemEofIndex = 0;
            // Working buffer value
            ofPassedFileObject.Buf.FileWorkBuffer = "";
            // Conversion results
            ofPassedFileObject.Buf.BytesConverted = 0;
            ofPassedFileObject.Buf.BytesConvertedTotal = 0;
            // TODO FileDataClearCurrent Special Characters
            #endregion
            // FileDataClearCurrent
            return PickDataClearCurrentResult;
        }
        #endregion
        // Database Connection - xxxxxxxxxxxxxxxxxxxxxxxxx
        #region Conn
        // <Section Id = "x
        public long ConnClose(ref SqlConnection PassedDbConnection, ref String sPassedFileName, ref String sPassedFileNameFull) {
            iConnClose = (int)DatabaseControl.ResultStarted;
            switch (FileReadMode) {
                case ((int)DatabaseControl.ReadModeSQL):
                    iConnClose = (int)DatabaseControl.ResultOK;
                    break;
                case ((int)DatabaseControl.ReadModeBLOCK):
                case ((int)DatabaseControl.ReadModeLINE):
                case ((int)DatabaseControl.ReadModeAll):
                    DbStatus.bpConnIsConnected = false;
                    DbStatus.bpConnIsConnecting = false;
                    iConnClose = (int)DatabaseControl.ResultOK;
                    // FileStatus.bpFileDoesExist = System.IO.File.Exists(FileSummary.FileName);
                    // iConnClose = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.InvalidResult;
                    return iConnClose;
                default:
                    iConnClose = (int)DatabaseControl.ReadModeError;
                    sLocalErrorMessage = "File Read Method (" + FileReadMode.ToString() + ") is not set";
                    throw new NotSupportedException(LocalMessage);
            }
            if (DbStatus.bpConnIsConnected == false) {
                // <Area Id = "WARNING - Already disconnected">
                iConnClose = (int)DatabaseControl.ResultOK;
            } else {
                // <Area Id = "Connected">
                // DbStatus.bpConnIsConnected = true;
                while (DbStatus.bpConnIsConnected) {
                    // <Area Id = "Connect">
                    iConnClose = (int)DatabaseControl.ResultOperationInProgress;
                    try {
                        PassedDbConnection.Close();
                        PassedDbConnection.Dispose();
                        iConnClose = (int)DatabaseControl.ResultOK;
                    } catch (SqlException ExceptionSql) {
                        sLocalErrorMessage = "";
                        ExceptSql(sLocalErrorMessage, ref ExceptionSql, iConnClose);
                        ExceptDatabaseFileCloseError(ref ExceptionSql);
                        iConnClose = (int)DatabaseControl.ResultDatabaseError;
                    } catch (Exception ExceptionGeneral) {
                        sLocalErrorMessage = "";
                        ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iConnClose);
                        DatabaseFilExceptionGeneralError(ref ExceptionGeneral);
                        iConnClose = (int)DatabaseControl.ResultOsError;
                    } finally {
                        DbStatus.bpConnIsConnected = false;
                        DbStatus.bpConnIsConnecting = false;
                    } // of try connect
                } // of is connecting
            } // is already connected
            return iConnClose;
        }
        // <Section Id = "x
        public long ConnOpen(ref SqlConnection PassedDbConnection) {
            iConnOpen = (int)DatabaseControl.ResultStarted;

            iConnOpen = ConnOpen(ref PassedDbConnection, ref FileSummary.spFileName, ref FileId.spFileNameFull);

            return iConnOpen;
        }

        // <Section Id = "x
        public long ConnOpen(ref SqlConnection PassedDbConnection, ref String sPassedFileName, ref String sPassedFileNameFull) {
            iConnOpen = (int)DatabaseControl.ResultStarted;
            // FileStatus.FileIsInitialized
            // if (DbStatus.ConnIsInitialized) {
            // iConnOpen = ConnReset();
            // }
            FileSummary.FileName = sPassedFileName;
            FileId.FileNameFull = sPassedFileNameFull;
            if (FileId.FileNameFull.Length == 0) {
                FileId.FileNameFull = SqlFileNameBuildFull(ref FileSummary.spDatabaseName, ref FileSummary.spFileOwner, ref FileSummary.spFileName, ref FileId.spFileNameFull);
            }
            if (PassedDbConnection != null && DbStatus.bpConnIsConnected == true) {
                // <Area Id = "WARNING - Already connected">
                iConnOpen = DbStatus.ipConnStatus;
                return DbStatus.ipConnStatus;
            } else {
                // <Area Id = "not Connected">
                if (!DbStatus.bpConnIsConnected || PassedDbConnection == null) {
                    if (!DbStatus.bpDatabaseFileNameIsValid) {
                        iConnOpen = DatabaseFileNameValidate(ref FileSummary.spSystemName, ref FileSummary.spDatabaseName, ref FileSummary.spServerName, ref FileSummary.spServiceName);
                    }
                    if (DbIo.spConnString.Length == 0) {
                        iConnOpen = ConnCmdBuild(ref FileSummary.spSystemName, ref FileSummary.spDatabaseName, ref FileSummary.spServerName, ref FileSummary.spServiceName);
                    }
                    if (!DbStatus.bpConnIsCreated) {
                        // ConnCreate also opens the connection
                        iConnOpen = ConnCreate(ref PassedDbConnection);
                    }
                    DbStatus.ConnIsInitialized = true;
                }
                // <Area Id = "CheckDatabaseDoesExist">
                switch (FileReadMode) {
                    case ((int)DatabaseControl.ReadModeSQL):
                        if (FileId.FileNameFull.Length == 0) {
                            FileId.FileNameFull = SqlFileNameBuildFull(ref FileSummary.spDatabaseName, ref FileSummary.spFileOwner, ref FileSummary.spFileName, ref FileId.spFileNameFull);
                        }
                        if (!DbStatus.bpConnIsCreated || !DbStatus.bpConnDoesExist || PassedDbConnection == null) {
                            iConnOpen = ConnCheckDoesExist(ref FileSummary.spFileName, ref FileId.spFileNameFull, false, false);
                        }
                        break;
                    case ((int)DatabaseControl.ReadModeBLOCK):
                    case ((int)DatabaseControl.ReadModeLINE):
                    case ((int)DatabaseControl.ReadModeAll):
                        DbStatus.ipConnDoesExist = (int)DatabaseControl.ResultDoesExist;
                        FileId.FileNameFull = FileSummary.FileName;
                        DbStatus.bpConnDoesExist = true;
                        FileStatus.bpFileDoesExist = System.IO.File.Exists(FileId.FileNameFull);
                        if (FileStatus.bpFileDoesExist == true) {
                            iConnOpen = (int)DatabaseControl.ResultDoesExist;
                            DbStatus.bpConnIsOpen = true;
                            DbStatus.bpConnIsConnected = true;
                            DbStatus.bpConnIsConnecting = false;
                            break;
                        } else {
                            iConnOpen = (int)DatabaseControl.ResultDoesNotExist;
                            DbStatus.bpConnIsOpen = false;
                            DbStatus.bpConnIsConnected = false;
                            DbStatus.bpConnIsConnecting = false;
                            return iConnOpen;
                        }
                    default:
                        iConnOpen = (int)DatabaseControl.ResultUndefined;
                        sLocalErrorMessage = "File Read Method (" + FileReadMode.ToString() + ") is not set";
                        throw new NotSupportedException(sLocalErrorMessage);
                }
                if (!DbStatus.bpConnIsConnected && PassedDbConnection != null) {
                    // <Area Id = "not Connected">
                    DbStatus.bpConnIsConnecting = true;
                    while (!DbStatus.bpConnIsConnected && DbStatus.bpConnIsConnecting && DbStatus.bpConnDoesExist) {
                        // <Area Id = "Connect">
                        DbStatus.ipConnStatus = (int)DatabaseControl.ResultOperationInProgress;
                        iConnOpen = DbStatus.ipConnStatus;
                        try {
                            if (!DbStatus.bpConnDoesExist) { iConnOpen = ConnCreate(); }
                            if (iConnOpen == (int)DatabaseControl.ResultOperationInProgress || iConnOpen == (int)DatabaseControl.ResultOK) {
                                switch (FileReadMode) {
                                    case ((int)DatabaseControl.ReadModeSQL):
                                        // Open Database Connection
                                        // DbStatus.ipConnStatus = (int) DatabaseControl.ResultOperationInProgress;
                                        iConnOpen = (int)DatabaseControl.ResultOperationInProgress;
                                        DbId.sMessageBoxMessage = MdmProcessTitle + "\n" + "SQL Database Connection:" + FileSummary.DatabaseName;
                                        PassedDbConnection.Open();
                                        iConnOpen = (int)DatabaseControl.ResultOK;
                                        DbStatus.bpConnIsOpen = true;
                                        DbStatus.bpConnIsConnecting = false;
                                        DbStatus.bpConnIsConnected = true;
                                        break;
                                    case ((int)DatabaseControl.ReadModeBLOCK):
                                    case ((int)DatabaseControl.ReadModeLINE):
                                    case ((int)DatabaseControl.ReadModeAll):

                                        // Check Disk access
                                        // Check Folder exists
                                        // Check Other Disk criteria
                                        // Check other File criteria
                                        iConnOpen = (int)DatabaseControl.ResultOK;
                                        DbStatus.bpConnIsOpen = true;
                                        DbStatus.bpConnIsConnected = true;
                                        DbStatus.bpConnIsConnecting = false;
                                        break;
                                    default:
                                        sLocalErrorMessage = "File Read Method (" + FileReadMode.ToString() + ") is not set";
                                        XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PickSystemCallStringResult, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                                        iConnOpen = (int)DatabaseControl.ReadModeError;
                                        throw new NotSupportedException(sLocalErrorMessage);
                                }
                                // iConnOpen = (int) DatabaseControl.ResultOK;
                                // DbStatus.bpConnIsConnected = true;
                                // DbStatus.bpConnIsConnecting = false;
                                // DbIo.SqlDbConnection.Close();
                            } else {
                                sLocalErrorMessage = "SQL Database connection error on database: " + FileSummary.DatabaseName;
                                XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PickSystemCallStringResult, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                                iConnOpen = (int)DatabaseControl.ResultFailed;
                                DbStatus.bpConnIsConnecting = false;
                                DbStatus.bpConnIsConnected = false;
                            }
                            // exceptions:
                        } catch (NotSupportedException ExceptionGeneral) {
                            sLocalErrorMessage = "";
                            DatabaseFilExceptionGeneralError(ref ExceptionGeneral);
                            iConnOpen = (int)DatabaseControl.ResultFailed;
                            DbStatus.bpConnIsConnecting = false;
                            DbStatus.bpConnIsConnected = false;
                        } catch (SqlException ExceptionSql) {
                            sLocalErrorMessage = "";
                            ExceptSql(sLocalErrorMessage, ref ExceptionSql, iConnClose);
                            //
                            ExceptDatabaseFileOpenError(ref ExceptionSql);
                            iConnOpen = (int)DatabaseControl.ResultFailed;
                            DbStatus.bpConnIsConnecting = false;
                            DbStatus.bpConnIsConnected = false;
                        } catch (Exception ExceptionGeneral) {
                            sLocalErrorMessage = "";
                            ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iConnClose);
                            iConnOpen = (int)DatabaseControl.ResultFailed;
                            iConnClose = (int)DatabaseControl.ResultOsError;
                            DatabaseFilExceptionGeneralError(ref ExceptionGeneral);
                            DbStatus.bpConnIsConnecting = false;
                            DbStatus.bpConnIsConnected = false;
                        } finally {
                            if (iConnOpen == (int)DatabaseControl.ResultOperationInProgress) {
                                iConnOpen = (int)DatabaseControl.ResultFailed;
                            }
                            DbStatus.ipConnStatus = iConnOpen;
                            DbStatus.bpConnIsOpen = DbStatus.bpConnIsConnected;
                        } // of try connect
                    } // of is connecting
                } else {
                    iConnOpen = (int)DatabaseControl.ResultOK;
                    DbStatus.bpConnIsOpen = false;
                    DbStatus.bpConnIsConnecting = false;
                    DbStatus.bpConnIsConnected = true;
                    DbStatus.bpConnIsCreating = false;
                    DbStatus.bpConnIsCreated = true;
                    DbStatus.ipConnStatus = iConnOpen;
                }
            } // is already connected
            return iConnOpen;
        }
        // <Section Id = "x
        public long ConnReset() {
            iConnReset = (int)DatabaseControl.ResultStarted;
            // FileStatus.FileIsInitialized
            // if (DbStatus.ConnIsInitialized) {
            DbSyn.spConnCreateCmd = "";
            DbStatus.ipConnDoesExist = -99999;
            DbStatus.bpConnDoesExist = false;
            DbStatus.bpConnIsValid = false;
            DbIo.spConnString = "";
            // <Area Id = "ConnectionStatus">
            DbStatus.ipConnStatus = -99999;
            DbStatus.bpConnIsConnecting = false;
            DbStatus.bpConnIsConnected = false;
            DbStatus.bpConnIsOpen = false;
            DbStatus.bpConnIsCreating = false;
            DbStatus.bpConnIsCreated = false;
            DbStatus.bpConnIsClosed = false;
            // <Area Id = "DatabaseControlMessages">
            DbId.sMformStatusMessage = "";
            DbId.sMessageBoxMessage = "";

            // <Area Id = "DatabaseObjects">
            DbMasterSyn.spMstrDbFileCreateCmd = "";

            DbIo.SqlDbCommandObject = null;
            DbIo.SqlDbConnection = null;
            oeDbFileCmdOsException = null;
            // this = null;
            // this.dispose();

            // <Area Id = "SourceDatabaseFileGroupInformation">

            // <Area Id = "SourceDatabaseFileNameInformation">

            // <Area Id = "DatabaseMessageConstants">

            DbStatus.ConnIsInitialized = false;
            // }
            return iConnReset;
        }
        // <<Section Id = "x
        public long ConnCheckDoesExist(ref String PasedFileName, ref String FileNameFullPassed, bool bPassedConnDoClose, bool bPassedConnDoDispose) {
            iConnCheckDoesExist = (int)DatabaseControl.ResultStarted;

            DbStatus.ipConnDoesExist = (int)DatabaseControl.ResultOK;
            DbStatus.bpConnDoesExist = true;
            // xxx CODE NEED TO BE ADDED TO READ THE MASTER FILE

            iConnCheckDoesExist = (int)DatabaseControl.ResultOK;

            return iConnCheckDoesExist;
        }
        #endregion
        // Database File - xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region DatabaseFile
        #region DatabaseFile MasterConnection
        #region ConnCreate
        // <Section Id = "SQL File Handling
        public long ConnCmdBuild(ref String sPassedSystemName, ref String sPassedDatabaseName, ref String sPassedServerName, ref String sPassedServiceName) {
            iConnCmdBuild = (int)DatabaseControl.ResultStarted;

            String sProvider = "";
            const int PROVIDErDOTNET = 1;
            const int PROVIDErOLEDB = 2;
            const int PROVIDErODBC = 3;
            int iProvider = PROVIDErDOTNET;
            const String PROVIDErSQLNCLI = "SQLNCLI";
            const String PROVIDErNATIVE_CLIENT = "{SQL Native Client}";
            bool UseServer = true;
            //
            const int PROVIDErLOCAL = 1;
            const int PROVIDErREMOTE = 2;
            int iProviderLocation = PROVIDErLOCAL;
            String sProviderIp = "";
            String sProviderPort = "";

            String sDatabase = "";
            bool UseDatabasePath = false;
            String sDbPathDirectory = "";
            String sDbPathFileName = "";

            bool UseCatalog = false;
            bool UseDatabaseName = true;

            String User = "";
            bool UseUser = false;
            const int UserEntered = 2;
            const int UserPROMPT = 3;
            int iUserSource = UserEntered;
            String ServerUserName = "";
            String ServerUserPassword = "";

            bool UseDataSource = false;

            String Security = "";
            const int CONN_TRUSTED = 1;
            const int CONN_INTEGRATED = 2;
            int iUseTrustedConnection = CONN_INTEGRATED;

            DbIo.spConnString = "";
            iIterationLoopCounter += 1;

            if (iUserSource == UserPROMPT) {
                // oConn.Properties("Prompt") = adPromptAlways
                // iConnCmdBuild = ConnCmdUserPrompt();
                UseUser = true;
            }
            if (UseUser) {
                // Not integrates with handler or
                // or page entry of user.
                // Mapplication lacks user info.
                User += "User ID=";
                User += ServerUserName;
                User += "Password =";
                User += ServerUserPassword;
            }

            if (iProvider == PROVIDErDOTNET) {
                //
            } else if (iProvider == PROVIDErOLEDB) {
                // Provider=SQLNCLI;
                sProvider += "Provider=";
                sProvider += PROVIDErSQLNCLI;
            } else if (iProvider == PROVIDErODBC) {
                // Driver={SQL Native Client};
                sProvider += "Driver=";
                sProvider += PROVIDErNATIVE_CLIENT;
            }
            if (sProvider.Length > 0) {
                DbIo.spConnString += sProvider;
                DbIo.spConnString += ";";
            }

            if (iProviderLocation == PROVIDErLOCAL) {
                UseServer = true;
            } else if (iProviderLocation == PROVIDErREMOTE) {
                sProvider = "";
                // bool UseDataSourceAddress = false;
                UseServer = false;
                sProvider += "ItemData Source=";
                // DbIo.spConnString += "190.190.200.100,1433"
                sProvider += sProviderIp;
                sProvider += ",";
                sProvider += sProviderPort;
                sProvider += ";";
                //
                // bool UseNetworkLibrary = false;
                sProvider += "Network Library=";
                sProvider += "DBMSSOCN";
                // Initial Catalog=myDataBase;
                UseCatalog = true;
                UseDatabaseName = false;
                DbIo.spConnString += sProvider;
                DbIo.spConnString += ";";
            }

            if (UseServer) {
                DbIo.spConnString += "Server=";
                // DbIo.spConnString += sPassedSystemName + @"\" + sPassedServiceName;
                DbIo.spConnString += sPassedServerName;
                // DbIo.spConnString += "localhost";
                DbIo.spConnString += ";";
            }

            if (UseDatabasePath) {
                // AttachDbFilename=|DataDirectory|mydbfile.mdf;
                DbIo.spConnString += "AttachDbFilename=";
                DbIo.spConnString += sDbPathDirectory;
                DbIo.spConnString += sDbPathFileName;
                DbIo.spConnString += ";";
            }

            if (UseDatabaseName) {
                sDatabase += "Database=";
            } else if (UseCatalog) {
                sDatabase += "Initial Catalog=";
            }
            sDatabase += sPassedDatabaseName;
            DbIo.spConnString += sDatabase;
            DbIo.spConnString += ";";

            //
            if (!DbMaster.UseSSPI) {
                Security += "Trusted_Connection=";
                // Security += "True";
                Security += "Yes";
            } else {
                Security += "Integrated Security=";
                Security += "SSPI";
            }
            DbIo.spConnString += Security;
            DbIo.spConnString += ";";

            if (UseUser) {
                DbIo.spConnString += User;
                DbIo.spConnString += ";";
                // User ID=myUsername;Password=myPassword;
                // Uid=myUsername; Pwd=myPassword;
            }

            //
            // .NET Framework ItemData Provider for SQL Server
            // ItemData Source=myServerAddress;
            // Initial Catalog=myDataBase;
            // Integrated Security=SSPI;
            // User ID=myDomain\myUsername;
            // Password=myPassword;
            // 
            // ItemData Source=190.190.200.100,1433;
            // Network Library=DBMSSOCN;
            // Initial Catalog=myDataBase;
            // User ID=myUsername;Password=myPassword;
            //
            // Server=.\SQLExpress;
            // AttachDbFilename=|DataDirectory|mydbfile.mdf; 
            // Database=dbname;
            // Trusted_Connection=Yes;
            // 
            // ItemData Source=.\SQLExpress;
            // Integrated Security=true; 
            // AttachDbFilename=|DataDirectory|\mydb.mdf;
            // User Instance=true;
            //
            // SQL Native Client OLE DB Provider
            // Provider=SQLNCLI;
            // Server=myServerAddress;Database=myDataBase;
            // Uid=myUsername; Pwd=myPassword;
            //
            // oConn.Properties("Prompt") = adPromptAlways
            // oConn.Open "Provider=SQLNCLI;
            // Server=myServerAddress;DataBase=myDataBase;
            // 
            // SQL Native Client ODBC Driver
            // Driver={SQL Native Client};
            // Server=myServerAddress;Database=myDataBase; 
            // Uid=myUsername;Pwd=myPassword;
            //
            // Driver={SQL Native Client};
            // Server=myServerAddress;Database=myDataBase; 
            // Trusted_Connection=yes;
            //
            // oConn.Properties("Prompt") = adPromptAlways
            // Driver={SQL Native Client};
            // Server=myServerAddress;Database=myDataBase;
            //
            // Driver={SQL Native Client};
            // Server=.\SQLExpress;
            // AttachDbFilename=c:\mydbfile.mdf; 
            // Database=dbname;
            // Trusted_Connection=Yes;
            //
            // Driver={SQL Native Client};
            // Server=.\SQLExpress; 
            // AttachDbFilename=|DataDirectory|mydbfile.mdf;
            // Database=dbname;
            // Trusted_Connection=Yes;
            //
            iConnCmdBuild = (int)DatabaseControl.ResultOK;
            return iConnCmdBuild;
        }
        // <Section Id = "ConnCreate">
        public long ConnCreate() {
            iConnCreate = (int)DatabaseControl.ResultStarted;
            iConnCreate = ConnCreate(ref DbIo.SqlDbConnection);
            return iConnCreate;
        }
        // <Section Id = "ConnCreatePassedConn">
        public long ConnCreate(ref SqlConnection PassedSqlDbConnection) {
            iConnCreatePassedConn = (int)DatabaseControl.ResultStarted;
            //
            // Current Database Conn Create 
            // and the Database Conn Open
            // are synonymns
            //
            if (!DbStatus.bpConnIsCreated || PassedSqlDbConnection == null) {
                if (!DbStatus.bpDatabaseFileNameIsValid) {
                    iConnCreatePassedConn = DatabaseFileNameValidate(ref FileSummary.spSystemName, ref FileSummary.spDatabaseName, ref FileSummary.spServerName, ref FileSummary.spServiceName);
                }
                if (DbSyn.spDatabaseFileCreateCmd.Length == 0) {
                    iConnCreatePassedConn = DatabaseFileCreateCmdBuild();
                }
                // if (spConnCreateCmd.Length == 0) {
                // iConnCreatePassedConn = ConnCreateCmdBuild();
                // }
                if (DbIo.spConnString.Length == 0) {
                    iConnCreatePassedConn = ConnCmdBuild(ref FileSummary.spSystemName, ref FileSummary.spDatabaseName, ref FileSummary.spServerName, ref FileSummary.spServiceName);
                }
            }
            if (PassedSqlDbConnection != null && DbStatus.bpConnIsCreated == true) {
                // <Area Id = "WARNING - Already Created">
                iConnCreatePassedConn = (int)DatabaseControl.ResultOK;
                DbStatus.ipConnStatus = (int)DatabaseControl.ResultOK;
                DbStatus.bpConnDoesExist = true;
                DbStatus.bpConnIsCreating = false;
                DbStatus.bpConnIsCreated = true;
                return iConnCreatePassedConn;
            } else {
                // <Area Id = "not Created">
                DbStatus.ipConnStatus = (int)DatabaseControl.ResultOperationInProgress;
                iConnCreatePassedConn = DbStatus.ipConnStatus;
                DbStatus.bpConnIsCreating = true;
                while (DbStatus.bpConnIsCreating) {
                    // <Area Id = "Connect">
                    try {
                        if (PassedSqlDbConnection == null) {
                            PassedSqlDbConnection = new SqlConnection(DbIo.spConnString);
                        }
                        // DbIo.SqlDbConnection = PassedSqlDbConnection;
                        // DbIo.SqlDbConnection.Open();
                        // DbIo.SqlDbConnection.Close();
                        DbStatus.ipConnStatus = (int)DatabaseControl.ResultOK;
                        iConnCreatePassedConn = (int)DatabaseControl.ResultOK;
                        DbStatus.bpConnDoesExist = true;
                        DbStatus.bpConnIsCreating = false;
                        DbStatus.bpConnIsCreated = true;
                    } catch (SqlException ExceptionSql) {
                        sLocalErrorMessage = "";
                        ExceptSql(sLocalErrorMessage, ref ExceptionSql, iConnClose);
                        DbStatus.ipConnStatus = (int)DatabaseControl.ResultFailed;
                        iConnCreatePassedConn = (int)DatabaseControl.ResultFailed;
                        //
                        ConnCreateError(ref ExceptionSql);
                        DbStatus.bpConnIsCreating = false;
                        DbStatus.bpConnIsCreated = false;
                    } catch (Exception ExceptionGeneral) {
                        sLocalErrorMessage = "";
                        ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iConnClose);
                        DbStatus.ipConnStatus = (int)DatabaseControl.ResultFailed;
                        iConnCreatePassedConn = (int)DatabaseControl.ResultFailed;
                        DatabaseFilExceptionGeneralError(ref ExceptionGeneral);
                        DbStatus.bpConnIsCreating = false;
                        DbStatus.bpConnIsCreated = false;
                    } finally {
                        if (DbStatus.ipConnStatus == (int)DatabaseControl.ResultOperationInProgress) {
                            iConnCreatePassedConn = (int)DatabaseControl.ResultFailed;
                        }
                        DbStatus.ipConnStatus = iConnCreatePassedConn;
                        FileStatus.ipStatusCurrent = iConnCreatePassedConn;
                    } // of try connect
                } // of is Creating
            } // is already Created
            return iConnCreatePassedConn;
        }
        // <Section Id = "ConnCreateCmdBuild">
        public long ConnCreateCmdBuild() {
            iConnCreateCmdBuild = (int)DatabaseControl.ResultStarted;
            DbSyn.spConnCreateCmd = "Connection are dynamic";

            iConnCreateCmdBuild = (int)DatabaseControl.ResultOK;
            return iConnCreateCmdBuild;

            if (DbMasterSyn.bpDbPhraseIfIsUsed) {
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseIf;
            }
            if (DbMasterSyn.bpDbFilePhraseSelectIsUsed) {
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseSelect;
            }
            if (DbMasterSyn.bpDbPhraseFromIsUsed) {
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseFrom;
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseFromItems;
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseFromEnd;
            }
            if (DbMasterSyn.bpDbPhraseWhereIsUsed) {
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseWhere;
                // sb paired list of dict + value
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseWhereItems + " ";
            }
            if (DbMasterSyn.bpDbPhraseIfIsUsed) {
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseIfEnd;
            }
            if (DbMasterSyn.bpDbPhraseDropIsUsed) {
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseDrop;
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseDropItems + " ";
            }
            if (DbMasterSyn.bpDbPhraseCreateIsUsed) {
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseCreate;
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseCreateItems + " ";
            }
            /*
            DbSyn.spConnCreateCmd = "IF EXISTS (" +
                "SELECT * " +
                "FROM master..sysdatabases " +
                "WHERE Name = 'HowToDemo')" + spMstrDbPhraseDoLine +
                "DROP DATABASE HowToDemo" + spMstrDbPhraseDoLine +
                "CREATE DATABASE HowToDemo";
             // FROM INFORMATION_SCHEMA.TABLES
             */
            iConnCreateCmdBuild = (int)DatabaseControl.ResultOK;
            return iConnCreateCmdBuild;
        }
        #endregion
        #region ConnError
        // <Section Id = "x
        public void ConnCreateError(ref SqlException ExceptionSql) {
            iDatabaseFileCreateConnectionError = (int)DatabaseControl.ResultStarted;
            DbId.sMessageBoxMessage = MdmProcessTitle + "\n" + @"File Creation Status";
            DbId.sMessageBoxMessage += "\n" + @"Create Connection error!";
            DbId.sMessageBoxMessage += "\n" + @"SQL Exception Error";
            DbId.sMessageBoxMessage += "\n" + ExceptionSql.ToString();
            // <Area Id = "display message
        }
        #endregion
        #endregion
        #region DatabaseFile Table
        #region DatabaseFileReset
        // <Section Id = "x
        public long DatabaseFileReset() {
            iDatabaseFileReset = (int)DatabaseControl.ResultStarted;

            // if (bpDatabaseFileIsInitialized) {

            // <Area Id = "SourceDatabaseInformation">
            FileSummary.DatabaseName = "";
            DbId.spDatabaseFileNameLong = "";
            DbStatus.bpDatabaseFileNameIsValid = false;
            DbSyn.spDatabaseFileCreateCmd = "";

            // <Area Id = "DbFileStatus">
            DbStatus.bpDatabaseFileDoesExist = false;
            DbStatus.bpDatabaseFileIsInvalid = false;
            DbStatus.bpDatabaseFileIsCreating = false;
            DbStatus.bpDatabaseFileIsCreated = false;

            iDatabaseFileReset = ConnReset();

            DbStatus.bpDatabaseFileIsInitialized = false;

            // }

            return iDatabaseFileReset;
        }
        #endregion
        #region DatabaseFileNameValidate
        // <Section Id = "SQL File Handling
        public long DatabaseFileNameValidate(ref String sPassedSystemName, ref String sPassedDatabaseName, ref String sPassedServerName, ref String sPassedServiceName) {
            iDatabaseFileNameValidate = (int)DatabaseControl.ResultStarted;
            DbIo.sConnString = "";
            iIterationLoopCounter += 1;
            // Database
            if (sPassedDatabaseName.Length == 0) {
                sPassedDatabaseName = DbMaster.MstrDbDatabaseDefault;
            }
            if (sPassedDatabaseName.Length == 0) {
                sPassedDatabaseName = DbMaster.MstrDbDatabaseDefaultMdm;
            }
            if (sPassedDatabaseName.Length == 0) {
                sPassedDatabaseName = @"WorkingDatabase";
            }
            FileSummary.DatabaseName = sPassedDatabaseName;

            // System
            if (sPassedSystemName.Length == 0) {
                sPassedSystemName = DbMaster.MstrDbSystemDefault;
            }

            if (sPassedSystemName.Length == 0) {
                sPassedSystemName = DbMaster.MstrDbSystemDefaultMdm;
            }

            if (sPassedSystemName.Length == 0) {
                sPassedSystemName = @"localhost";
            }
            FileSummary.SystemName = sPassedSystemName;

            // Service
            if (sPassedServiceName.Length == 0) {
                sPassedServiceName = DbMaster.MstrDbServiceDefault;
            }

            if (sPassedServiceName.Length == 0) {
                sPassedServiceName = DbMaster.MstrDbServiceDefaultMdm;
            }

            if (sPassedServiceName.Length == 0) {
                sPassedServiceName = @"localhost";
            }
            FileSummary.ServiceName = sPassedServiceName;

            // Server
            if (sPassedServerName.Length == 0 || FileSummary.ServerName.Length == 0) {
                FileSummary.ServerName = FileSummary.SystemName + @"\" + FileSummary.ServiceName;
            }

            DbStatus.bpDatabaseFileNameIsValid = true;

            iDatabaseFileNameValidate = ConnCmdBuild(ref FileSummary.spSystemName, ref FileSummary.spDatabaseName, ref FileSummary.spServerName, ref FileSummary.spServiceName);

            return iDatabaseFileNameValidate;
        }
        #endregion
        #region DatabaseFileNameBuild
        // <Section Id = "SqlFileNameBuildFull">
        public String SqlFileNameBuildFull(ref String sPassedDatabaseName, ref String sPassedFileOwner, ref String sPassedFileName, ref String sPassedFileNameFull) {
            iSqlFileNameBuildFullString = (int)DatabaseControl.ResultStarted;
            DbId.DatabaseFileNameLong = "";

            // sPassedDatabaseName = "";
            // sPassedDatabaseName = "master";
            // sPassedDatabaseName = "DaveTestDb1";
            // DatabaseFileNameLong += sPassedDatabaseName;
            // DatabaseFileNameLong += ".";

            // DatabaseName
            if (sPassedDatabaseName.Length == 0) {
                sPassedDatabaseName = DbMaster.MstrDbDatabaseDefault;
            }

            if (sPassedDatabaseName.Length == 0) {
                sPassedDatabaseName = DbMaster.MstrDbDatabaseDefaultMdm;
            }

            if (sPassedDatabaseName.Length > 0) {
                DbId.DatabaseFileNameLong += sPassedDatabaseName;
                DbId.DatabaseFileNameLong += ".";
            }

            // FileOwner
            if (sPassedFileOwner.Length == 0) {
                sPassedFileOwner = DbMaster.MstrDbOwnerDefault;
            }

            if (sPassedFileOwner.Length == 0) {
                sPassedFileOwner = DbMaster.MstrDbOwnerDefaultMdm;
            }

            if (sPassedFileOwner.Length == 0) {
                sPassedFileOwner = "dbo";
            }

            DbId.DatabaseFileNameLong += sPassedFileOwner;
            DbId.DatabaseFileNameLong += ".";

            // FileName
            DbId.DatabaseFileNameLong += sPassedFileName;
            // iSqlFileNameBuildFullString
            return DbId.DatabaseFileNameLong;
        }
        // <Section Id = "SqlFileNameBuildFull">
        public long SqlFileNameBuildFull() {
            iSqlFileNameBuildFull = (int)DatabaseControl.ResultStarted;
            DbId.DatabaseFileNameLong = FileId.FileNameFull = SqlFileNameBuildFull(ref FileSummary.spDatabaseName, ref FileSummary.spFileOwner, ref FileSummary.spFileName, ref FileId.spFileNameFull);
            return iSqlFileNameBuildFull;
        }
        #endregion
        #region DatabaseFileBuild
        // <Section Id = "DatabaseFileCreateCmdBuild">
        public long DatabaseFileCreateCmdBuild() {
            iDatabaseFileCreateBuild = (int)DatabaseControl.ResultStarted;
            DbMasterSyn.MstrDbDatabaseCreateCmd = "";
            if (DbMasterSyn.bpDbFilePhraseUseIsUsed) {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseUse;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMaster.spMstrDbFileDb;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseUseEnd;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
            }
            if (DbMasterSyn.bpDbFilePhraseIfIsUsed) {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseIf;
            }
            if (DbMasterSyn.bpDbFileFilePhraseSelectIsUsed) {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseSelect;
            }
            if (DbMasterSyn.bpDbFilePhraseFromIsUsed) {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseFrom;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseFromItems;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseFromEnd;
            }
            if (DbMasterSyn.bpDbFilePhraseWhereIsUsed) {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseWhere;
                // sb paired list of dict + value
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseWhereItems1;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseWhereAnd;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseWhereItems2;

                // Loop X = 2 to n 
                // on Where Items using When AND
                // DbMasterSyn.spMstrDbFilePhraseWhereItemsAnd + 
                // DbMasterSyn.spMstrDbFilePhraseWhereItemsId[X] 
                // DbMasterSyn.spMstrDbFilePhraseWhereItemsExpression[X] 
                // DbMasterSyn.spMstrDbFilePhraseWhereItemsValue[X] 
            }

            if (DbMasterSyn.bpDbFilePhraseIfIsUsed) {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseIfEnd;
            }

            if (DbMasterSyn.bpDbFilePhraseIfIsUsed || DbMasterSyn.bpDbFilePhraseWhereIsUsed) {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
            }

            if (DbMasterSyn.bpDbFilePhraseBeginIsUsed) {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseBegin;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
            }

            if (DbMasterSyn.bpDbFilePhraseDropIsUsed) {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseDrop;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseDropItems + " ";
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
            }

            if (DbMasterSyn.bpDbFilePhraseBeginIsUsed) {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseBeginEnd;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
            }

            if (DbMasterSyn.bpDbFilePhraseCreateIsUsed) {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseCreate;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseCreateObject;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseCreateTableName + " ";

                // DbMasterSyn.spMstrDbFilePhraseDColumnId[X] 
                // DbMasterSyn.spMstrDbFilePhraseWhereItemsType[X]; 
                // DbMasterSyn.spMstrDbFilePhraseWhereItemsTypeHasLength[X];
                if (DbMasterSyn.bpDbFilePhraseCreateIsUsed) {
                    // + "("
                    // DbMasterSyn.spMstrDbFilePhraseWhereItemsTypeLength[X];
                    // + ")"
                }
                // + " "
                // DbMasterSyn.spMstrDbFilePhraseWhereItemsRange[X];
                // "NOT NULL "
                if (DbMasterSyn.bpDbFilePhraseCreateIsUsed) {
                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraint;
                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraintCol;
                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraintEnd;

                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraintType1;
                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraintType2;

                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraintColBegin;
                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraintColName;
                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraintColEnd;
                    DbMasterSyn.MstrDbDatabaseCreateCmd += "";
                }


                // %%% XXX ;    
                // TODO z$RelVs? Database Creation MstrDbDatabaseCreateCmd
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
            }
            /*
            DbMasterSyn.MstrDbDatabaseCreateCmd = "IF EXISTS (" +
                "SELECT * " +
                "FROM master..sysdatabases " +
                "WHERE Name = 'HowToDemo')" + spMstrDbPhraseDoLine +
                "DROP DATABASE HowToDemo" + spMstrDbPhraseDoLine +
                "CREATE DATABASE HowToDemo";
             */
            return (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.NormalEnd;
        }
        // <Section Id = "x
        public long DatabaseFileCreate() {
            iDatabaseFileCreate = (int)DatabaseControl.ResultStarted;
            if (DbMasterSyn.MstrDbDatabaseCreateCmd == null) {
                iDatabaseFileCreate = DatabaseFileCreateCmdBuild();
            }
            // Create
            iDatabaseFileCreate = (int)DatabaseControl.ResultOperationInProgress;
            try {
                // <Area Id = "General System Errors
                DbId.sMformStatusMessage = "";
                // Not IsConnected
                if (!DbStatus.bpConnIsConnected) {
                    // <Area Id = "Create connection
                    DbStatus.bpConnIsCreating = true;
                    while (DbStatus.bpConnIsCreating) {
                        if (DbMaster.MstrConnString == null) {
                            ConnCmdBuild(ref FileSummary.spSystemName, ref FileSummary.spDatabaseName, ref FileSummary.spServerName, ref FileSummary.spServiceName);
                        }
                        ConnCreate(ref DbIo.SqlDbConnection);
                        DbStatus.bpConnIsCreating = false;
                        if (DbStatus.bpConnIsConnected == false) {
                            ConnCreateError(ref ExceptionSql);
                            // <Area Id = "Exit Here?
                        }
                    }
                }
                // IsConnected
                if (DbStatus.bpConnIsConnected) {
                    // <Area Id = "Open connection
                    DbIo.SqlDbConnection.Open();
                    DbStatus.bpConnIsOpen = true;
                    DbStatus.bpConnIsClosed = false;
                    // <Area Id = "SqlDbConnection Creation
                } // end of if
            } catch (SqlException ExceptionSql) {
                sLocalErrorMessage = "";
                ExceptSql(sLocalErrorMessage, ref ExceptionSql, iDatabaseFileCreate);
                //
                iDatabaseFileCreate = (int)DatabaseControl.ResultFailed;
                ExceptDatabaseFileCreationError(ref ExceptionSql);
                // <Area Id = "Exit Here
            } catch (Exception ExceptionGeneral) {
                sLocalErrorMessage = "";
                ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iConnClose);
                iDatabaseFileCreate = (int)DatabaseControl.ResultFailed;
                //
                DatabaseFilExceptionGeneralError(ref ExceptionGeneral);
                // <Area Id = "*** App.Job.Exit(); *** TODO
            } finally {
                // <Area Id = "Close Open connection
                if (DbStatus.bpConnIsOpen && !DbStatus.bpConnIsClosed) {
                    DbIo.SqlDbConnection.Close();
                }
                DbStatus.bpConnIsOpen = false;
                DbStatus.bpConnIsClosed = true;

                if (DbStatus.bpConnIsCreated || DbStatus.bpConnIsConnected) {
                    // <Area Id = "dispose of connection
                    DbStatus.bpConnIsCreated = false;
                }
                DbStatus.bpConnIsCreated = false;
                DbStatus.bpConnIsConnected = false;
                DbStatus.bpConnIsConnecting = false;

            }
            iDatabaseFileCreate = (int)DatabaseControl.ResultOperationInProgress;
            try {
                DbMasterSyn.MstrDbDatabaseCreateCmd = "CREATE-FILE " + FileId.FileNameFull;
                DbIo.SqlDbCommandObject = new SqlCommand(DbMasterSyn.MstrDbDatabaseCreateCmd, DbIo.SqlDbConnection);
                // DbIo.SqlDbCommandObject = new SqlCommand(MstrDbDatabaseCreateCmd, DbIo.SqlDbConnection);
                //=================
                DbIo.SqlDbConnection.Open();
                DbIo.SqlDbCommandObject.ExecuteNonQuery();
                DbIo.SqlDbConnection.Close();
                //=================
                // <Area Id = "ItemData has been successfully submitted, so 
                // break out of the loop
                // and close the status form.
                DbStatus.bpDatabaseFileIsCreated = true;

                // sMformStatusMessage.Close();
                DbId.sMformStatusMessage = "";
                //=================
                sLocalErrorMessage = MdmProcessTitle + @" Database Creation Status" + "\n" + "Database " + DbMasterSyn.spMstrDbPhraseDatabase + @" successfully created!";
                MessageMdmSendToPageNewLine(ref Sender, "A2" + sLocalErrorMessage);
                //== <Area Id = "Catch Try
            } catch (SqlException ExceptionSql) {
                sLocalErrorMessage = "";
                ExceptSql(sLocalErrorMessage, ref ExceptionSql, iDatabaseFileCreate);
                iDatabaseFileCreate = (int)DatabaseControl.ResultFailed;
                iConnClose = (int)DatabaseControl.ResultDatabaseError;
                ExceptDatabaseFileCreationError(ref ExceptionSql);
                sLocalErrorMessage = MdmProcessTitle + " SQL Exception Error!";
                sLocalErrorMessage = ExceptionSql.Message;
                ExceptDatabaseFileCreationError(ref ExceptionSql);
                DbStatus.bpDatabaseFileIsCreated = false;
            } catch (Exception ExceptionGeneral) {
                sLocalErrorMessage = "";
                ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iConnClose);
                iDatabaseFileCreate = (int)DatabaseControl.ResultFailed;
                //
                DatabaseFilExceptionGeneralError(ref ExceptionGeneral);
                DbStatus.bpDatabaseFileIsCreated = false;
            }
            // TODO z$RelVs? Database File Creation
            if (DbStatus.bpDatabaseFileIsCreated) {
                iDatabaseFileCreate = (int)DatabaseControl.ResultStarted;
            }

            if (DbStatus.bpDatabaseFileIsCreating) {
                iDatabaseFileCreate = 811;
            }

            if (DbStatus.bpDatabaseFileIsInvalid) {
                iDatabaseFileCreate = 911;
            }

            return iDatabaseFileCreate;
        }
        #endregion
        #region DatabaseFileOpen
        // <Section Id = "DatabaseFileOpen">
        public long DatabaseFileOpen(ref SqlConnection PassedDbConnection, ref String sPassedFileName, ref String sPassedFileNameFull) {
            iDatabaseFileOpen = (int)DatabaseControl.ResultStarted;
            #region Initialize File Information
            int iOptionsResult = 0;
            //
            FileSummary.FileName = sPassedFileName;
            if (FileId.FileNameFull.Length == 0) {
                if (sPassedFileNameFull.Length > 0) {
                    FileId.FileNameFull = sPassedFileNameFull;
                }
            }
            iDatabaseFileOpen = (int)DatabaseControl.ResultOperationInProgress;
            // iDatabaseFileOpen = (int)DatabaseControl.ResultUndefined;
            // Read Counts
            Buf.BytesRead = 0;
            Buf.BytesReadTotal = 0;
            Buf.BytesConverted = 0;
            Buf.BytesConvertedTotal = 0;
            // Pick Dictionary
            PickRow.PdIndex = 0;
            PickRow.PdItemCount = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ItemId = "";
            PickRow.PickDictArray[PickRow.PdIndex].ItemIntId = 0;
            PickRow.PickDictArray[PickRow.PdIndex].iItemAttrIndex = 0;  // Field being processed in the Dict Column
            //
            PickRow.PickDictArray[PickRow.PdIndex].DictColIndex = 0;
            PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter = 0; // DatabaseFileOpen  How many Fields (size of)
            PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter = 0;
            PickRow.PickDictArray[PickRow.PdIndex].iItemLength = 0;

            PickRow.PickDictArray[PickRow.PdIndex].AttrTwoStringValue = "";
            PickRow.PickDictArray[PickRow.PdIndex].PdIndexAttrTwo = 0;
            PickRow.PickDictArray[PickRow.PdIndex].AttrTwoIsNumeric = false;

            PickRow.PickDictArray[PickRow.PdIndex].DictColumnTouched = 0;
            PickRow.PickDictArray[PickRow.PdIndex].DictColumnIdDone = false;
            PickRow.PickDictArray[PickRow.PdIndex].DictColumnLength = 0;
            PickRow.PickDictArray[PickRow.PdIndex].DictColumnLengthChange = false;
            PickRow.PickDictArray[PickRow.PdIndex].DictColumnDefinitionFound = false;
            //
            DbStatus.ConnIsInitialized = true;
            #endregion
            #region Check DoesExist
            switch (FileReadMode) {
                case ((int)DatabaseControl.ReadModeSQL):
                    // DbIo.SqlDbConnection.Open();
                    FileId.FileNameFull = SqlFileNameBuildFull(ref FileSummary.spDatabaseName, ref FileSummary.spFileOwner, ref FileSummary.spFileName, ref FileId.spFileNameFull);
                    FileStatus.iFileDoesExist = SqlFileCheckAndSetDoesExist(ref PassedDbConnection, FileSummary.FileName, FileId.FileNameFull, false, false, false, ref ColInfoDb, ref ColInfo);
                    if (FileStatus.iFileDoesExist == (int)DatabaseControl.ResultDoesExist) {
                        iDatabaseFileOpen = (int)DatabaseControl.ResultDoesExist;
                        FileStatus.FileDoesExist = true;
                    } else {
                        iDatabaseFileOpen = (int)DatabaseControl.ResultDoesNotExist;
                        FileStatus.FileDoesExist = false;
                    }
                    break;
                case ((int)DatabaseControl.ReadModeBLOCK):
                case ((int)DatabaseControl.ReadModeLINE):
                case ((int)DatabaseControl.ReadModeAll):
                    FileId.FileNameFull = FileSummary.FileName;
                    FileStatus.FileDoesExist = System.IO.File.Exists(FileSummary.FileName);
                    if (FileStatus.FileDoesExist == true) {
                        iDatabaseFileOpen = (int)DatabaseControl.ResultDoesExist;
                    } else {
                        iDatabaseFileOpen = (int)DatabaseControl.ResultDoesNotExist;
                    }
                    break;
                default:
                    iDatabaseFileOpen = (int)DatabaseControl.ReadModeError;
                    sLocalErrorMessage = "File Read Method (" + FileReadMode.ToString() + ") is not set";
                    throw new NotSupportedException(sLocalErrorMessage);
                // return iDatabaseFileOpen;
            }
            #endregion
            // File exists or not
            // Process results based on options:
            // Create Missing File
            // Delete Existing File
            // Append to file
            // Empty file
            // File should not exist
            // File should exist
            try {
                // File does exist
                if ((FileStatus.FileDoesExist)) {
                    iDatabaseFileOpen = (int)DatabaseControl.ResultDoesExist;
                    #region Option: N: File must not alread exist
                    // <Area Id = "Option: File must not alread exist options here.
                    iOptionsResult = FileSummary.FileOptions.IndexOf("N");
                    if (iOptionsResult > 0) {
                        // <Area Id = "Option: error file already exists.
                        iDatabaseFileOpen = (int)DatabaseControl.ResultShouldNotExist;
                        return iDatabaseFileOpen;
                    }
                    #endregion
                    #region Option: D: Delete the file if it exists
                    // <Area Id = "Option: Delete the file if it exists.
                    iOptionsResult = FileSummary.FileOptions.IndexOf("D");
                    if (iOptionsResult > 0) {
                        #region Option: Delete the file
                        switch (FileReadMode) {
                            case ((int)DatabaseControl.ReadModeSQL):
                                // TODO Option: Delete the file if it exists
                                // iDatabaseFileOpen = SqlFileDelete(sPassedFileName);
                                if (FileStatus.FileDoesExist) {
                                    iDatabaseFileOpen = (int)DatabaseControl.ResultOK;
                                } else {
                                    // TODO $ERROR Error Option: Delete the file if it exists
                                    iDatabaseFileOpen = (int)DatabaseControl.ResultShouldNotExist;
                                }
                                return iDatabaseFileOpen;
                            case ((int)DatabaseControl.ReadModeBLOCK):
                            case ((int)DatabaseControl.ReadModeLINE):
                            case ((int)DatabaseControl.ReadModeAll):
                                // TODO Option: Delete the file if it exists
                                File.Delete(FileSummary.FileName);
                                // TODO Option: Create the file if it exists
                                // <Area Id = "Option: Create the file depending on options here.
                                FileIo.DbFileStreamObject = File.Create(FileSummary.FileName);
                                if (FileIo.DbFileStreamObject != null) {
                                    iDatabaseFileOpen = (int)DatabaseControl.ResultOK;
                                } else {
                                    iDatabaseFileOpen = (int)DatabaseControl.ResultShouldNotExist;
                                }
                                break;
                            default:
                                iDatabaseFileOpen = (int)DatabaseControl.ReadModeError;
                                sLocalErrorMessage = "File Read Method (" + FileReadMode.ToString() + ") is not set";
                                throw new NotSupportedException(sLocalErrorMessage);
                            // return iDatabaseFileOpen;
                        }
                        #endregion
                        #region VERIFY delete
                        // <Area Id = "Open the stream and read it back.
                        switch (FileReadMode) {
                            case ((int)DatabaseControl.ReadModeSQL):
                                // TODO OPEN Open the stream and read it back
                                if (FileStatus.FileDoesExist) {
                                    iDatabaseFileOpen = (int)DatabaseControl.ResultOK;
                                } else {
                                    iDatabaseFileOpen = (int)DatabaseControl.ResultShouldNotExist;
                                }
                                return iDatabaseFileOpen;
                            case ((int)DatabaseControl.ReadModeBLOCK):
                            case ((int)DatabaseControl.ReadModeLINE):
                            case ((int)DatabaseControl.ReadModeAll):
                                // TODO OPEN Open the stream and read it back
                                FileIo.DbFileStreamObject = File.OpenRead(FileSummary.FileName);
                                // DbFileStreamObject = System.IO.File.TextFileOpen(FileSummary.FileName);
                                if (FileIo.DbFileStreamObject != null) {
                                    iDatabaseFileOpen = (int)DatabaseControl.ResultOK;
                                } else {
                                    iDatabaseFileOpen = (int)DatabaseControl.ResultShouldNotExist;
                                }
                                break;
                            default:
                                iDatabaseFileOpen = (int)DatabaseControl.ReadModeError;
                                // TODO $ERROR Error Open the stream and read it back
                                LocalMessage = "File Read Method (" + FileReadMode.ToString() + ") is not set";
                                throw new NotSupportedException(LocalMessage);
                            // return iDatabaseFileOpen;
                        }
                        #endregion
                    }
                    #endregion
                    #region Option: F: The file must already exist.
                    // <Area Id = "Option: The file must already exist options here.
                    iOptionsResult = FileSummary.FileOptions.IndexOf("F");
                    if (iOptionsResult > 0) {
                        if (iDatabaseFileOpen == (int)DatabaseControl.ResultDoesExist) {
                            return iDatabaseFileOpen;
                        }
                        // iDatabaseFileOpen = (int) DatabaseControl.ResultDoesNotExist;
                    }
                    #endregion
                } else {
                    iDatabaseFileOpen = (int)DatabaseControl.ResultDoesNotExist;
                    // File does not exist
                    #region Option: ?: File Does Not Exist
                    // <Area Id = "Option: File must not exist options here.
                    iOptionsResult = FileSummary.FileOptions.IndexOf("?"); // File Does Not Exist
                    if (iOptionsResult > 0) {
                        // <Area Id = "Option: error file already exists.
                        iDatabaseFileOpen = (int)DatabaseControl.ResultOK;
                        return iDatabaseFileOpen;
                    }
                    #endregion
                    #region Option: M: Create the missing file. N: Create New Must Not Exist File
                    // <Area Id = "Option: M: Create the missing file depending on options here.
                    iOptionsResult = FileSummary.FileOptions.IndexOf("M"); // Create Missing
                    if (iOptionsResult < 0) { iOptionsResult = FileSummary.FileOptions.IndexOf("N"); } // Create New Must Not Exist
                    if (iOptionsResult > 0) {
                        // <Area Id = "Option: Create the file depending on options here.
                        switch (FileReadMode) {
                            case ((int)DatabaseControl.ReadModeSQL):
                                // TODO Option: unknown for Read Mode Sql;
                                if (!FileStatus.FileDoesExist) {
                                    iDatabaseFileOpen = SqlFileCreate(sPassedFileName, sPassedFileNameFull);
                                    if (FileStatus.FileDoesExist) {
                                        iDatabaseFileOpen = (int)DatabaseControl.ResultDoesExist;
                                        FileStatus.FileDoesExist = bYES;
                                    } else {
                                        // TODO Option: Error Create the file depending on options
                                        iDatabaseFileOpen = (int)DatabaseControl.ResultShouldExist;
                                        FileStatus.FileDoesExist = bNO;
                                    }
                                }
                                return iDatabaseFileOpen;
                            case ((int)DatabaseControl.ReadModeBLOCK):
                            case ((int)DatabaseControl.ReadModeLINE):
                            case ((int)DatabaseControl.ReadModeAll):
                                iDatabaseFileOpen = TextFileCreate(ref sPassedFileName, ref sPassedFileNameFull);
                                if (FileStatus.FileDoesExist) {
                                    iDatabaseFileOpen = (int)DatabaseControl.ResultDoesExist;
                                } else {
                                    // TODO Option: Error Create the file depending on options 
                                    iDatabaseFileOpen = (int)DatabaseControl.ResultShouldExist;
                                }
                                break;
                            default:
                                iDatabaseFileOpen = (int)DatabaseControl.ReadModeError;
                                // TODO Option: Error Create the file depending on options 
                                sLocalErrorMessage = "File Read Method (" + FileReadMode.ToString() + ") is not set";
                                throw new NotSupportedException(sLocalErrorMessage);
                            // return iDatabaseFileOpen;
                        }
                    }
                    #endregion

                    // TODO $ERROR Error Option: file missing and no option to create error
                    // <Area Id = "Option: file missing and no option to create error
                    // iDatabaseFileOpen = (int) DatabaseControl.ResultDoesNotExist;
                }
                #region Catch Errors
            } catch (SqlException ExceptionSql) {
                sLocalErrorMessage = "";
                ExceptSql(sLocalErrorMessage, ref ExceptionSql, iDatabaseFileOpen);
                iDatabaseFileOpen = (int)DatabaseControl.ResultDatabaseError;
                //
                ExceptDatabaseFileOpenError(ref ExceptionSql);
                // Exit Here
            } catch (Exception ExceptionGeneral) {
                sLocalErrorMessage = "";
                ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iConnClose);
                iDatabaseFileOpen = (int)DatabaseControl.ResultUnknownFailure;
                //
                DatabaseFilExceptionGeneralError(ref ExceptionGeneral);
                DbStatus.bpConnIsConnecting = false;
                DbStatus.bpConnIsConnected = false;
                #endregion
            } finally {
                #region Close Open connection
                if (!FileStatus.FileKeepOpen) {
                    // <Area Id = "Close Open connection
                    if (DbStatus.bpConnIsOpen && !DbStatus.bpConnIsClosed) {
                        DbIo.SqlDbConnection.Close();
                    }
                    DbStatus.bpConnIsOpen = false;
                    DbStatus.bpConnIsClosed = true;
                    if (DbStatus.bpConnIsCreated || DbStatus.bpConnIsConnected) {
                        // <Area Id = "dispose of connection
                        DbStatus.bpConnIsCreated = false;
                    }
                    DbStatus.bpConnIsCreated = false;
                    DbStatus.bpConnIsConnected = false;
                    DbStatus.bpConnIsConnecting = false;
                }
                #endregion
            }
            return iDatabaseFileOpen;
        }
        public void ExceptDatabaseFileOpenError(ref SqlException ExceptionSql) {
            iDatabaseFileOpenError = (int)DatabaseControl.ResultStarted;
            // sMessageBoxMessage = MdmProcessTitle + "\n";
            // sMessageBoxMessage += "\n" + @"SQL Exception Error!";
            // sMessageBoxMessage += "\n" + @"File Open Error!";
            // try {
            // sMessageBoxMessage += "\n" + ExceptionSql.ToString();
            // } catch (Exception ExceptionGeneral) { ; }
            // XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(),PickSystemCallStringResult, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sMessageBoxMessage + "\n");

            // <Area Id = "display message
        }
        #endregion
        #region DatabaseFileCreation
        public void ExceptDatabaseFileCreationError(ref SqlException ExceptionSql) {
            iDatabaseFileCreationError = (int)DatabaseControl.ResultStarted;
            DbId.sMessageBoxMessage = MdmProcessTitle + "\n" + @"File Creation Status";
            DbId.sMessageBoxMessage += "\n" + @"Creation error!";
            DbId.sMessageBoxMessage += "\n" + @"SQL Exception Error";
            DbId.sMessageBoxMessage += "\n" + ExceptionSql.ToString();
            // <Area Id = "display message
        }
        #endregion
        #region DatabaseFileDeletion
        #endregion
        #region DatabaseFileClose
        public void ExceptDatabaseFileCloseError(ref SqlException ExceptionSql) {
            iDatabaseFileCloseError = (int)DatabaseControl.ResultStarted;
            DbId.sMessageBoxMessage = MdmProcessTitle + "\n" + @"File Creation Status";
            DbId.sMessageBoxMessage += "\n" + @"Close Connection error!";
            DbId.sMessageBoxMessage += "\n" + @"SQL Exception Error";
            DbId.sMessageBoxMessage += "\n" + ExceptionSql.ToString();
            // <Area Id = "display message
        }
        #endregion
        #region DatabaseFileControl
        //                         } catch (NotSupportedException ExceptionGeneral) {
        public void DatabaseFilExceptionGeneralError(ref NotSupportedException ExceptionGeneral) {
            iDatabaseFilExceptionGeneralError = (int)DatabaseControl.ResultStarted;

        }

        public void DatabaseFilExceptionGeneralError(ref Exception ExceptionGeneral) {
            iDatabaseFilExceptionGeneralError = (int)DatabaseControl.ResultStarted;
            // sMessageBoxMessage = MdmProcessTitle + "\n" + @"File Creation Status";
            // sMessageBoxMessage += "\n" + @"General Exception Error!";
            // sMessageBoxMessage += "\n";
            // try {
            // sMessageBoxMessage += "\n" + ExceptionGeneral.ToString();
            //             } catch { ; }
            // XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(),PickSystemCallStringResult, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sMessageBoxMessage + "\n");
            // <Area Id = "display message
        }
        #endregion
        #endregion
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmFileType_AsciFile
        // <Section Id = "Ascii File Handling
        // <Section Id = "public String Check and ReadAll and ReadLine AsciiData
        public long AsciiDataReadAll(ref String sPassedFileName, ref String sPassedFileNameFull) {
            // <Area Id = "bRead the IoAll of text
            iAsciiDataReadAll = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            if (FileId.spFileNameFull.Length == 0) {
                if (sPassedFileNameFull.Length > 0) {
                    FileId.spFileNameFull = sPassedFileNameFull;
                }
            }
            // <Area Id = "Check File Stream; 
            iAsciiDataReadAll = AsciiDataFileStreamReaderCheck(ref FileSummary.spFileName, ref FileId.spFileNameFull);
            // <Area Id = "bRead All Lines if Stream OK;
            if (iAsciiDataReadAll == (int)DatabaseControl.ResultOK) {
                iAsciiDataReadAll = (int)DatabaseControl.ResultOperationInProgress;
                try {
                    //
                    Buf.ItemIsAtEnd = true;
                    FileIo.IoReadBuffer = System.IO.File.ReadAllText(FileSummary.FileName);
                    //
                    Buf.DoesExist = true;
                    FileIo.spIoAll += FileIo.IoReadBuffer;
                    Buf.BytesRead = FileIo.IoReadBuffer.Length;
                    // Buf.BytesReadTotal = Buf.BytesRead;
                    Buf.BytesReadTotal = FileIo.spIoAll.Length;
                    // iDataItemCharEobIndex = Item.ItemData.Length; // End of Character Buffer
                    // if (Buf.ItemIsAtEnd == true) {
                    // iDataItemCharEofIndex = iDataItemCharEobIndex;
                    // }
                    iAsciiDataReadAll = (int)DatabaseControl.ResultOK;
                    FileStatus.StatusCurrent = (int)DatabaseControl.ResultOK;
                } catch (Exception ExceptionGeneral) {
                    iAsciiDataReadAll = (int)DatabaseControl.ResultFailed;
                    Buf.DoesExist = false;
                    FileIo.IoReadBuffer = "";
                    Buf.BytesRead = 0;
                    // Buf.BytesConverted = 0;
                    // Buf.BytesConvertedTotal = 0;
                    // iDataItemCharEobIndex = Item.ItemData.Length; // End of Character Buffer
                    // if (Buf.ItemIsAtEnd == true) {
                    // iDataItemCharEofIndex = iDataItemCharEobIndex;
                    // }
                    if (FileIo.DbFileStreamReaderObject == null) {
                        FileStatus.StatusCurrent = (int)DatabaseControl.ResultFailed;
                    } else {
                        FileStatus.StatusCurrent = (int)DatabaseControl.ResultFailed;
                    }
                    //
                    ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iAsciiDataReadAll);
                    DatabaseFilExceptionGeneralError(ref ExceptionGeneral);
                } finally {
                    // XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(),PickSystemCallStringResult, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + "Executing finally block." + "\n");
                }
            } else {
                FileIo.IoReadBuffer = "";
                Buf.BytesRead = 0;
                // Buf.BytesConverted = 0;
                // Buf.BytesConvertedTotal = 0;
            }
            return iAsciiDataReadAll;
        }
        // <Section Id = "x
        public long AsciiDataReadLine(ref String sPassedFileName, ref String sPassedFileNameFull) {
            // <Area Id = "bRead All Lines if Stream OK;
            iAsciiDataReadLine = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            if (FileId.spFileNameFull.Length == 0) {
                if (sPassedFileNameFull.Length > 0) {
                    FileId.spFileNameFull = sPassedFileNameFull;
                }
            }
            // <Area Id = "Check File Stream; 
            iAsciiDataReadLine = AsciiDataFileStreamReaderCheck(ref FileSummary.spFileName, ref FileId.spFileNameFull);
            // <Area Id = "bRead All Lines if Stream OK;
            if (iAsciiDataReadLine == (int)DatabaseControl.ResultOK) {
                iAsciiDataReadLine = (int)DatabaseControl.ResultOperationInProgress;
                try {
                    FileIo.IoReadBuffer = FileIo.DbFileStreamReaderObject.ReadLine();
                    FileIo.IoLine += FileIo.IoReadBuffer;
                    // <Area Id = "Continue to read until you reach end of file
                    FileStatus.StatusCurrent = (int)DatabaseControl.ResultOK;
                    iAsciiDataReadLine = (int)DatabaseControl.ResultOK;
                } catch (Exception ExceptionGeneral) {
                    iAsciiDataReadLine = (int)DatabaseControl.ResultFailed;
                    sLocalErrorMessage = "";
                    ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iAsciiDataReadLine);
                    DatabaseFilExceptionGeneralError(ref ExceptionGeneral);
                    if (FileIo.DbFileStreamReaderObject == null) {
                        FileStatus.StatusCurrent = (int)DatabaseControl.ResultOsError;
                    } else {
                        FileStatus.StatusCurrent = (int)DatabaseControl.ResultFailed;
                        if (FileIo.IoReadBuffer.Length == 0) {
                            FileStatus.StatusCurrent = (int)DatabaseControl.ResultAtEnd;
                        }
                    }
                    FileIo.IoReadBuffer = "";
                    // spIoLine = "";
                } finally {
                    XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PickSystemCallStringResult, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + "Executing finally block." + "\n");
                }
            }
            return iAsciiDataReadLine;
        }
        // <Section Id = "public String Check and bRead Database
        public long AsciiDataFileStreamReaderCheck(ref String sPassedFileName, ref String sPassedFileNameFull) {
            iAsciiDataFileStreamReaderCheck = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            if (FileId.spFileNameFull.Length == 0) {
                if (sPassedFileNameFull.Length > 0) {
                    FileId.spFileNameFull = sPassedFileNameFull;
                }
            }
            // spIoBlock = "";
            // 
            if (FileIo.DbFileStreamReaderObject == null) {
                if (sPassedFileName != null) {
                    // FileSummary.spFileName = sPassedFileName;
                    if (FileSummary.FileName != null) {
                        //Pass the file path and file name to the StreamReader constructor
                        FileIo.DbFileStreamReaderObject = new StreamReader(FileSummary.FileName);
                    }
                }
            }
            if (FileIo.DbFileStreamReaderObject == null) {
                iAsciiDataFileStreamReaderCheck = (int)DatabaseControl.ResultFailed;
            } else {
                iAsciiDataFileStreamReaderCheck = (int)DatabaseControl.ResultOK;
            }
            FileStatus.StatusCurrent = iAsciiDataFileStreamReaderCheck;
            return iAsciiDataFileStreamReaderCheck;
        }
        // <Section Id = "public int Check and bRead Binary (AsciiDataReadBlockSeek)
        public String AsciiDataReadBlock(ref String sPassedFileName, ref String sPassedFileNameFull) {
            // <Area Id = "bRead the Win32 Seek Block from Handle
            iAsciiDataReadBlock = (int)DatabaseControl.ResultStarted;

            // <Area Id = "Check File Stream; 
            iAsciiDataReadBlock = AsciiDataFileStreamReaderCheck(ref sPassedFileName, ref sPassedFileNameFull);
            // <Area Id = "bRead All Lines if Stream OK;
            if (iAsciiDataReadBlock == (int)DatabaseControl.ResultOK) {
                iAsciiDataReadBlock = (int)DatabaseControl.ResultOperationInProgress;
                try {
                    // <Area Id = " do a standard read all for now (not Win32 read)
                    // spIoBlock = 
                    iAsciiDataReadBlock = AsciiDataReadAll(ref sPassedFileName, ref sPassedFileNameFull);
                    iAsciiDataReadBlock = (int)DatabaseControl.ResultOK;
                } catch (Exception ExceptionGeneral) {
                    sLocalErrorMessage = "";
                    ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iAsciiDataReadBlock);
                    iAsciiDataReadBlock = (int)DatabaseControl.ResultFailed;
                    //
                    DatabaseFilExceptionGeneralError(ref ExceptionGeneral);
                    if (FileIo.DbFileStreamReaderObject == null) {
                        FileStatus.StatusCurrent = (int)DatabaseControl.ResultFailed;
                    } else {
                        FileStatus.StatusCurrent = (int)DatabaseControl.ResultFailed;
                    }
                } finally {
                    MessageMdmSendToPageNewLine(ref Sender, "A2" + "Executing finally block.");
                }
            }
            return FileIo.spIoBlock;
        }
        // <Section Id = "public int Check and bRead Binary (AsciiDataReadBlockSeek)
        public String AsciiDataReadBlockSeek(ref String sPassedFileName, ref String sPassedFileNameFull) {
            // <Area Id = "bRead the Win32 Seek Block from Handle
            iAsciiDataReadBlockSeek = (int)DatabaseControl.ResultStarted;

            // <Area Id = "Check File Stream; 
            iAsciiDataReadBlockSeek = AsciiDataFileStreamReaderCheck(ref sPassedFileName, ref sPassedFileNameFull);
            // <Area Id = "bRead All Lines if Stream OK;
            if (iAsciiDataReadBlockSeek == (int)DatabaseControl.ResultOK) {
                iAsciiDataReadBlockSeek = (int)DatabaseControl.ResultOperationInProgress;
                try {
                    // <Area Id = " do a standard read all for now (not Win32 read)
                    // spIoBlock = 
                    iAsciiDataReadBlockSeek = AsciiDataReadAll(ref sPassedFileName, ref sPassedFileNameFull);
                    iAsciiDataReadBlockSeek = (int)DatabaseControl.ResultOK;
                } catch (Exception ExceptionGeneral) {
                    sLocalErrorMessage = "";
                    ExceptGeneral(sLocalErrorMessage, ref ExceptionGeneral, iAsciiDataReadBlockSeek);
                    //
                    DatabaseFilExceptionGeneralError(ref ExceptionGeneral);
                    iAsciiDataReadBlockSeek = (int)DatabaseControl.ResultFailed;
                    if (FileIo.DbFileStreamReaderObject == null) {
                        FileStatus.StatusCurrent = (int)DatabaseControl.ResultFailed;
                    } else {
                        FileStatus.StatusCurrent = (int)DatabaseControl.ResultFailed;
                    }
                } finally {
                    MessageMdmSendToPageNewLine(ref Sender, "A2" + "Executing finally block.");
                }
            }
            return FileIo.spIoBlock;
        }
        // <Section Id = "AsciiDataClear">
        public long AsciiDataClear() {
            iAsciiDataClear = (int)DatabaseControl.ResultStarted;
            //
            /*/ 
            FileId.FileNameFull = SqlFileNameBuildFull(ref FileSummary.spDatabaseName, ref FileSummary.spFileOwner, ref FileSummary.spFileName);
            if (spIoAll == null) {
                FileIo.IoReadBuffer = "";
                spIoBlock = "";
                spIoLine = "";
                spIoAll = "";
            }
            /*/
            FileIo.DbFileObject = null;
            FileIo.DbFileStreamObject = null;
            FileIo.DbFileStreamReaderObject = null;
            DbIo.SqlDbConnection = null;
            DbIo.SqlDbCommandObject = null;
            DbIo.CommandCurrent = null;
            /*/
            bDbDatabaseDoesExist = false;
            bDbDatabaseIsInvalid = false;
            bDbDatabaseIsCreating = false;
            bDbDatabaseIsCreated = false;

            // <Area Id = "DatabaseErrorObject">
            oeDbFileCmdOsException = null;
            ExceptionSql = null;
            /*/
            FileSummary.DataClear(ref FileSummary);
            //
            // FileData
            FileIo.IoReadBuffer = "";
            FileIo.spIoBlock = "";
            FileIo.spIoLine = "";
            FileIo.spIoAll = "";

            return iAsciiDataClear;
        }
        // <Section Id = "x
        public long AsciiDataFileClose() {
            iAsciiDataFileClose = (int)DatabaseControl.ResultStarted;
            // Console.ReadLine();
            // <Area Id = "close the file streams
            if (FileIo.DbFileStreamReaderObject != null) {
                FileIo.DbFileStreamReaderObject.Close();
            }
            if (FileIo.DbFileStreamObject != null) {
                FileIo.DbFileStreamObject.Close();
            }
            //close the file
            if (FileIo.DbFileObject != null) {
                iAsciiDataFileClose = AsciiDataClear();
                // <Area Id = "do destructor;
            }
            return iAsciiDataFileClose;
        }
        // <Section Id = "AsciiDataCreatePassedName">
        public long AsciiDataCreate() {
            iAsciiDataCreate = (int)DatabaseControl.ResultStarted;
            iAsciiDataCreate = AsciiDataCreate(ref FileSummary.spFileName, ref FileId.spFileNameFull);
            return iAsciiDataCreate;
        }
        // <Section Id = "AsciiDataCreatePassedName">
        public long AsciiDataCreate(ref String sPassedFileName, ref String sPassedFileNameFull) {
            iAsciiDataCreatePassedName = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.spFileNameFull = SqlFileNameBuildFull(ref FileSummary.spDatabaseName, ref FileSummary.spFileOwner, ref FileSummary.spFileName, ref FileId.spFileNameFull);
            iAsciiDataCreatePassedName = (int)DatabaseControl.ResultFailed;
            return iAsciiDataCreatePassedName;
        }
        // <Section Id = "AsciiDataDeletePassedName">
        public long AsciiDataDelete() {
            iAsciiDataDelete = (int)DatabaseControl.ResultStarted;
            iAsciiDataDelete = AsciiDataDelete(ref FileSummary.spFileName, ref FileId.spFileNameFull);
            return iAsciiDataDelete;
        }
        // <Section Id = "AsciiDataDeletePassedName">
        public long AsciiDataDelete(ref String sPassedFileName, ref String sPassedFileNameFull) {
            iAsciiDataDeletePassedName = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.spFileNameFull = SqlFileNameBuildFull(ref FileSummary.spDatabaseName, ref FileSummary.spFileOwner, ref FileSummary.spFileName, ref FileId.spFileNameFull);
            iAsciiDataDeletePassedName = (int)DatabaseControl.ResultUnknownFailure;
            return iAsciiDataDeletePassedName;
        }
        // <Section Id = "AsciiDataWritePassedName">
        public long AsciiDataWrite() {
            iAsciiDataWrite = (int)DatabaseControl.ResultStarted;
            iAsciiDataWrite = AsciiDataWrite(ref FileSummary.spFileName, ref FileId.spFileNameFull);
            return iAsciiDataWrite;
        }
        // <Section Id = "AsciiDataWritePassedName">
        public long AsciiDataWrite(ref String sPassedFileName, ref String sPassedFileNameFull) {
            iAsciiDataWritePassedName = (int)DatabaseControl.ResultStarted;
            FileSummary.FileName = sPassedFileName;
            FileId.spFileNameFull = SqlFileNameBuildFull(ref FileSummary.spDatabaseName, ref FileSummary.spFileOwner, ref FileSummary.spFileName, ref FileId.spFileNameFull);
            iAsciiDataWritePassedName = (int)DatabaseControl.ResultUnknownFailure;
            return iAsciiDataWritePassedName;
        }
        // <Section Id = "AsciiDataFileReset">
        public long AsciiDataFileReset(ref String sPassedFileName, ref String sPassedFileNameFull) {
            iAsciiDataFileReset = (int)DatabaseControl.ResultStarted;
            // if (FileStatusAux.FileIsInitialized) {
            // THIS IS A DISPOSE FUNCTION
            FileStatusAux.FileIsInitialized = false;
            // }
            return iAsciiDataFileReset;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Check File Types
        public long FileTypeBaseCheck(long PassedFileTypeId) {
            long ThisBaseFileTypeId;
            return ThisBaseFileTypeId = PassedFileTypeId &
                ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeData
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeDictData
                );
        }
        public long FileSubTypeBaseCheck(long PassedFileSubTypeId) {
            long ThisBaseFileSubTypeId;
            return ThisBaseFileSubTypeId = PassedFileSubTypeId &
            ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMS
            | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMY
            | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDB2
            | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeORACLE
            | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeXML
            | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXT
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXTSTD
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeASC
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeFIX
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDAT
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeCSV
            | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_CSV 
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_ROW 
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_NATIVE 
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_NATIVE_ONE
            );
        }
        public long FileSubTypeMinorGet(long PassedFileSubTypeId) {
            // NOTE Very early playing with masks and code text formatting.  
            // NOTE Eventual use of bit arrays?
            // NOTE PRACTICES All of this gets rolled into the property get / set locations eventually in either case.
            // NOTE This is not required as the Sub and Minor types share the save Sub byte value because a long was used.
            // NOTE The long might be required eventually as there are a massive number of variations in file format
            // NOTE including a variety of proprietary, custom and "one off" types that would be retained 
            // NOTE and also added dynamically.
            long ThisBaseFileSubTypeId;
            long ThisMask = 0x0FFFFFFF;
            return ThisBaseFileSubTypeId = PassedFileSubTypeId |
                (ThisMask & (
                (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeSQL
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMY
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDB2
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeORACLE
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeXML
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXTSTD
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeASC
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeFIX
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDAT
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeCSV
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_CSV
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_ROW
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_NATIVE
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_NATIVE_ONE
                ));
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmActionAnd_Locals
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Check Buffer Shift
        public int iShiftCheck;
        #region Increment Column Pointer
        public int iFileIndexPointerIncrement;
        public long FileIndexPointerIncrement() {
            iFileIndexPointerIncrement = (int)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            //
            // Increment Column Pointer
            //
            iIterationCount += 1;
            //
            if (ItemAttrCounter > (iTraceShiftIndexByCount / 10) && iTraceByteCount > (5 * iTraceShiftIndexByCount)) {
                iFileIndexPointerIncrement = ShiftCheck();
            }
            if (ConsoleVerbosity >= 5) {
                LocalMessage0 = "Increment: Input (" + ItemAttrCounter.ToString() + ")";
                if (TraceOn) { TraceMdmDo(ref Sender, bIsMessage, Buf.CharMaxIndex, ItemAttrMaxIndex, iFileIndexPointerIncrement, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage0 + "\n"); }
            }
            // (Mapplication)XUomMavvXv.XUomCovvXv.XUomPmvvXv.ProgressBarMdm1.Value += 1;
            // TODO Post Back Progress Bar ItemData
            switch (FileSummary.FileTypeBaseId) {
                case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeData):
                    // Processing ItemData
                    switch (FileSummary.FileSubTypeBaseId) {
                        //////////////////////// TILDE ROW NOT BEING HANDLED
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeSQL):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMS):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMY):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeCSV):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXT
                        | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE):
                            ItemAttrCount += 1; // total number columns.
                            ItemAttrCountTotal += 1; // total number columns.
                            ItemAttrCounter += 1; // move pointer
                            // Total # of Fields in Dict Item
                            // Increment Output Dictionary Pointer
                            if (ItemAttrIndex + 1 < 100) {
                                ItemAttrIndex += 1;// Next Field within Dict
                            }
                            if (ConsoleVerbosity >= 5) {
                                LocalMessage5 = "Output ItemData: ";
                                LocalMessage5 += " Count (" + ItemAttrCounter.ToString() + ")";
                                LocalMessage5 += ", Column (" + ItemAttrCounter.ToString() + ")";
                                if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { TraceMdmDo(ref Sender, bIsMessage, Buf.CharMaxIndex, ItemAttrMaxIndex, iFileIndexPointerIncrement, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage5 + "\n"); }
                            }
                            break;
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeASC):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDAT):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeFIX):
                            // error SubType not supported
                            break;
                        default:
                            // error FileSubTypeUNKNOWN
                            iFileIndexPointerIncrement = (int)DatabaseControl.ResultUndefined;
                            LocalMessage5 = "File SubType Error (" + FileSummary.FileSubTypeId.ToString() + ") not properly set";
                            throw new NotSupportedException(LocalMessage5);
                    }
                    break;
                //
                case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeDictData):
                    // FileDictData
                    switch (FileSummary.FileSubTypeBaseId) {
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeSQL):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMS):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMY):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeCSV):
                            ColIndex.ColCount += 1;
                            ColIndex.ColCountTotal += 1;
                            ColIndex.ColCounter += 1;
                            // Total # of Fields in Dict Item
                            // Increment Output Dictionary Pointer
                            if (ColIndex.ColIndex + 1 < 100) {
                                ColIndex.ColIndex += 1;// Next Field within Dict
                            }
                            //
                            if (ConsoleVerbosity >= 5) {
                                LocalMessage5 = "Output Dictionary: ";
                                LocalMessage5 += " Count (" + ColIndex.ColCount.ToString() + ")";
                                LocalMessage5 += ", Attr (" + ColIndex.ToString() + ")";
                                if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { TraceMdmDo(ref Sender, bIsMessage, Buf.CharMaxIndex, ItemAttrMaxIndex, iFileIndexPointerIncrement, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage5 + "\n"); }
                            }
                            break;
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXT
                        | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeASC):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDAT):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeFIX):
                            // error SubType not supported
                            break;
                        default:
                            // error FileSubTypeUNKNOWN
                            iFileIndexPointerIncrement = (int)DatabaseControl.ResultUndefined;
                            LocalMessage5 = "File SubType Error (" + FileSummary.FileSubTypeId.ToString() + ") not properly set";
                            throw new NotSupportedException(LocalMessage5);
                    }
                    break;
                default:
                    // FileTypeUNKNOWN
                    iFileIndexPointerIncrement = (int)DatabaseControl.ResultUndefined;
                    LocalMessage5 = "File Type Error (" + FileSummary.FileTypeId.ToString() + ") not properly set";
                    throw new NotSupportedException(LocalMessage5);
            } // end or is DATA Attr not DICT
            return iFileIndexPointerIncrement;
        }
        #endregion
        #region Increment Column Pointer
        public int iColPointerIncrement;
        public long ColPointerIncrement() {
            iColPointerIncrement = (int)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            if (ItemAttrCounter > (iTraceShiftIndexByCount / 10) && iTraceByteCount > (5 * iTraceShiftIndexByCount)) {
                iColPointerIncrement = ShiftCheck();
            }
            if (ConsoleVerbosity >= 5) {
                LocalMessage0 = "Increment: Input (" + ItemAttrCounter.ToString() + ")";
                if (TraceOn) { TraceMdmDo(ref Sender, bIsMessage, Buf.CharMaxIndex, ItemAttrMaxIndex, iColPointerIncrement, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage0 + "\n"); }
            }
            // (Mapplication)XUomMavvXv.XUomCovvXv.XUomPmvvXv.ProgressBarMdm1.Value += 1;
            // TODO Post Back Progress Bar ItemData
            switch (FileSummary.FileTypeBaseId) {
                case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeData):
                    // Processing ItemData
                    switch (FileSummary.FileSubTypeBaseId) {
                        //////////////////////// TILDE ROW NOT BEING HANDLED
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeSQL):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMS):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMY):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeCSV):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXT
                        | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE):
                            break;
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeASC):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDAT):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeFIX):
                            // error SubType not supported
                            break;
                        default:
                            // error FileSubTypeUNKNOWN
                            iColPointerIncrement = (int)DatabaseControl.ResultUndefined;
                            LocalMessage5 = "File SubType Error (" + FileSummary.FileSubTypeId.ToString() + ") not properly set";
                            throw new NotSupportedException(LocalMessage5);
                    }
                    if (ColIndex.ColSet) {
                        ColIndex.ColCounter += 1;
                        // if (ColCounter > ColCount) { ColCounter = ColCount; }
                        // Next Field within Dict
                        if (ColIndex.ColIndex + 1 < 100) { ColIndex.ColIndex += 1; }
                        if (ConsoleVerbosity >= 5) {
                            LocalMessage5 = "File ItemData Column Increment: ";
                            LocalMessage5 += " Count (" + ColIndex.ColCount.ToString() + ")";
                            LocalMessage5 += ", Column (" + ColIndex.ColCounter.ToString() + ")";
                            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { TraceMdmDo(ref Sender, bIsMessage, Buf.CharMaxIndex, ColIndex.ColMaxIndex, iColPointerIncrement, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage5 + "\n"); }
                        }
                    }
                    // Total # of Fields in Dict Item
                    // Increment Output Dictionary Pointer
                    if (ItemAttrSet) {
                        ItemAttrCounter += 1;
                        if (ItemAttrCounter > ItemAttrCount) { ItemAttrCount = ItemAttrCounter; }
                        if (ItemAttrIndex + 1 < 100) { ItemAttrIndex += 1; }
                        if (ConsoleVerbosity >= 5) {
                            LocalMessage5 = "File ItemData Column Attribue Increment: ";
                            LocalMessage5 += " Count (" + ItemAttrCount.ToString() + ")";
                            LocalMessage5 += ", Column (" + ItemAttrCounter.ToString() + ")";
                            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { TraceMdmDo(ref Sender, bIsMessage, Buf.CharMaxIndex, ColIndex.ColMaxIndex, iColPointerIncrement, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage5 + "\n"); }
                        }
                    }
                    break;
                //
                case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeDictData):
                    // FileDictData
                    switch (FileSummary.FileSubTypeBaseId) {
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeSQL):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMS):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMY):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeCSV):
                            break;
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXT
                        | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeASC):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDAT):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeFIX):
                            // error SubType not supported
                            break;
                        default:
                            // error FileSubTypeUNKNOWN
                            iColPointerIncrement = (int)DatabaseControl.ResultUndefined;
                            LocalMessage5 = "File SubType Error (" + FileSummary.FileSubTypeId.ToString() + ") not properly set";
                            throw new NotSupportedException(LocalMessage5);
                    }
                    if (ColIndex.ColSet) {
                        ColIndex.ColCounter += 1;
                        // if (ColCounter > ColCount) { ColCounter = ColCount; }
                        // Next Field within Dict
                        if (ColIndex.ColIndex + 1 < 100) { ColIndex.ColIndex += 1; }
                        if (ConsoleVerbosity >= 5) {
                            LocalMessage5 = "File Dictionary Column Increment: ";
                            LocalMessage5 += " Count (" + ColIndex.ColCount.ToString() + ")";
                            LocalMessage5 += ", Column (" + ColIndex.ColCounter.ToString() + ")";
                            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { TraceMdmDo(ref Sender, bIsMessage, Buf.CharMaxIndex, ColIndex.ColMaxIndex, iColPointerIncrement, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage5 + "\n"); }
                        }
                    }
                    if (ColIndex.ColAttrSet) {
                        ColIndex.ColAttrCounter += 1;
                        // if (ColAttrCounter > ColAttrCount) { ColAttrCount = ColAttrCounter; }
                        if (ColIndex.ColAttrIndex + 1 < 100) { ColIndex.ColAttrIndex += 1; }
                        //
                        if (ConsoleVerbosity >= 5) {
                            LocalMessage5 = "File Dictionary Column Attribue Increment: ";
                            LocalMessage5 += " Count (" + ColIndex.ColAttrCount.ToString() + ")";
                            LocalMessage5 += ", Column (" + ColIndex.ColAttrCounter.ToString() + ")";
                            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { TraceMdmDo(ref Sender, bIsMessage, Buf.CharMaxIndex, ColIndex.ColMaxIndex, iColPointerIncrement, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage5 + "\n"); }
                        }
                    }
                    break;
                default:
                    // FileTypeUNKNOWN
                    iColPointerIncrement = (int)DatabaseControl.ResultUndefined;
                    LocalMessage5 = "File Column Increment File Type Error (" + FileSummary.FileTypeId.ToString() + ") not properly set";
                    throw new NotSupportedException(LocalMessage5);
            } // end or is DATA Attr not DICT
            return iColPointerIncrement;
        }
        #endregion
        public int ShiftCheck() {
            iShiftCheck = (int)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // Shift Input Buffer after iTraceShiftIndexByCount lines or increment Input Buffer Index
            if (ItemAttrCounter > (iTraceShiftIndexByCount / 10) && iTraceByteCount > (5 * iTraceShiftIndexByCount)) {
                if (ItemAttrCounter > iTraceShiftIndexByCount || iTraceByteCount > (10 * iTraceShiftIndexByCount)) {
                    if (TraceBugOnNow && iTraceDisplayCountTotal > iTraceBugThreshold) {
                        LocalMessage9 = "Debug Point Reached, ready to Shift buffer " + iTraceShiftIndexByCount.ToString() + " lines, Index(" + ItemAttrCounter.ToString() + "), ShiftByteCount(" + iTraceByteCount.ToString() + "), ByteCount(" + iTraceByteCountTotal.ToString() + ")";
                        TraceMdmDo(ref Sender, bIsMessage, Buf.CharMaxIndex, ItemAttrMaxIndex, iShiftCheck, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage9 + "\n");
                        LocalMessage = LocalMessage9;
                    }
                    //
                    while (ItemAttrCounter > iTraceShiftIndexByCount) {
                        iShiftCheck = ItemDataShift(ref FileObject, iTraceShiftIndexByCount);
                        // ItemAttrCounter -= iTraceShiftIndexByCount;
                        iTraceByteCountTotal += (int)iShiftCheck;
                        iTraceByteCount -= (int)iShiftCheck;
                    }
                    //
                    if (TraceBugOnNow && iTraceDisplayCountTotal > iTraceBugThreshold) {
                        LocalMessage9 = "Debug Point Reached, Shift finished, now Index(" + ItemAttrCounter.ToString() + "), ShiftByteCount(" + iTraceByteCount.ToString() + "), ByteCount(" + iTraceByteCountTotal.ToString() + ")";
                        TraceMdmDo(ref Sender, bIsMessage, Buf.CharMaxIndex, ItemAttrMaxIndex, iColPointerIncrement, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage9 + "\n");
                        //System.Diagnostics.Debug.WriteLine("MessageBlock [" + sTraceMessageBlock + "]");
                        //sTraceMessageBlock = "";
                        LocalMessage = LocalMessage9;
                    }
                }
            }
            //
            return iShiftCheck;
        }
        // xxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile FileDatabaseControlResultGetText
        public String FileDatabaseControlResultGetText(int PassedIntResult) {
            String sResult = "";
            switch (PassedIntResult) {
                case ((int)DatabaseControl.ResultUndefined):
                    sResult = "Null start";
                    break;
                case ((int)DatabaseControl.ResultMissingName):
                    sResult = "File must have a value";
                    break;
                case ((int)DatabaseControl.ResultDoesNotExist):
                    sResult = "File not found";
                    break;
                case ((int)DatabaseControl.ResultShouldNotExist):
                    sResult = "File already exists";
                    break;
                case ((int)DatabaseControl.IoRowIdDoesNotExist):
                    sResult = "Item Id not found";
                    break;
                case ((int)DatabaseControl.IoRowIdDoesExist):
                    sResult = "Item Id already exists";
                    break;
                default:
                    sResult = "Unknown error" + " (" + PassedIntResult + ")";
                    break;
            }
            if (TraceBugOnNow && iTraceDisplayCountTotal > iTraceBugThreshold) {
                LocalMessage9 = "\n" + sResult;
                TraceMdmDo(ref Sender, bIsMessage, Buf.CharMaxIndex, ItemAttrMaxIndex, iColPointerIncrement, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage9 + "\n");
            }
            return sResult;
        }
        #endregion

        // Move Shift Item.ItemData by x Attrs
        public long iItemDataShift;
        public int ItemDataShift(ref Mfile ofPassedFileObject, int iPassedAttrsToShift) {
            iItemDataShift = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            int iItemDataShifted = 0;
            // 
            // TODO $$$CHECK Use Primitive extension values here (McString)
            iItemDataShifted = PickIndex(ofPassedFileObject.Item.ItemData, ColumnSeparator, iPassedAttrsToShift);
            if (iItemDataShifted >= 0) {
                // 
                if (ofPassedFileObject.Item.ItemData.Length > iItemDataShifted) {
                    ofPassedFileObject.Item.ItemData = ofPassedFileObject.Item.ItemData.Substring(iItemDataShifted + 1);
                    //
                    ofPassedFileObject.ColIndex.ColCounter -= iPassedAttrsToShift; // Current Attr
                    ofPassedFileObject.ColIndex.ColMaxIndexTemp -= iPassedAttrsToShift;
                    if (ofPassedFileObject.ColIndex.ColCounter < 0) {
                        ofPassedFileObject.ColIndex.ColCounter = 0; // error condition
                    }
                } else { iItemDataShifted = 0; }
                // else { ColIndexPassed.Item.ItemData = ""; }
            } else { iItemDataShifted = 0; }
            // else { ColIndexPassed.Item.ItemData = ""; }
            //
            // TODO $$$CHECK Use Primitive extension values here (McString)
            // iItemDataShift = ((FilePickDb)FilePickDbObject).PickItemDataCounterGet(ColIndexPassed);
            // ColIndexPassed.ColMaxIndex = 0; // Total Attrs in Item
            // ColIndexPassed.ColMaxIndexTemp = 0; // Total Attrs in Item
            // DataItemAtrributeClear(ColIndexPassed);
            // Working value
            // ColIndexPassed.ColCounter = 0; // ItemData Items in Item / Row / Item
            // Character Pointers
            // DataItemCharClear(ColIndexPassed);
            // 
            // TODO ItemDataShift NOTE More work needed on delimiters,
            // TODO ItemDataShift add delimiters as required.
            // TODO ItemDataShift NOTE Count of columns not rows 
            // TODO ItemDataShift check for csv handling before changing
            // TODO ItemDataShift NOTE This does not handle quoteded characters!!!
            //
            return iItemDataShifted;
        }
        #endregion
        #region File Property Clear
        public void FileNameClear(ref Mfile PassedMfile) {
            PassedMfile.FileSummary.FileName = "";
            PassedMfile.FileSummary.FileNameNext = "";
            PassedMfile.FileSummary.FileNameCurrent = "";
            PassedMfile.FileSummary.FileNameOriginal = "";
            PassedMfile.FileId.FileNameFull = "";
            PassedMfile.FileStatus.FileIsOpen = false;
        }
        public void ItemIdClear(ref Mfile PassedMfile) {
            PassedMfile.Item.ItemId = "";
            PassedMfile.FileSummary.ItemIdNext = "";
            PassedMfile.FileSummary.ItemIdCurrent = "";
        }
        public void ItemAtrributeClear(ref Mfile PassedMfile) {
            // Attr Handling for Ascii and Text
            // Ascii Attr Pointers
            // Working value
            PassedMfile.ItemAttrCounter = 1; // Current Attr
            PassedMfile.ItemAttrCount = 0; // ItemData Items in Item / Row / Item
            PassedMfile.ItemAttrMaxIndex = 0; // Total Attrs in Item
            PassedMfile.ItemAttrMaxIndexTemp = 0;
            //
        }
        public void DataItemAtrributeClear(ref Mfile PassedMfile) {
            PassedMfile.iDataItemAttrEos2Index = 0; // Current Column Separator 2
            PassedMfile.iDataItemAttrEos1Index = 0; // Current Column Separator 1
            PassedMfile.iDataItemAttrEosIndex = 0; // Current Column Sub-Value
            PassedMfile.iDataItemAttrEovIndex = 0; // Current Column Value
            PassedMfile.iDataItemAttrEoaIndex = 0; // Current Column
            PassedMfile.iDataItemAttrEorIndex = 0; // Current Row
            PassedMfile.iDataItemAttrEofIndex = 0; // Current File
        }
        public void DataItemCharClear(ref Mfile PassedMfile) {
            // Character Pointers
            PassedMfile.iDataItemCharEobIndex = 0; // End of Character Buffer
            PassedMfile.iDataItemCharIndex = 0; // Character Pointer
            PassedMfile.iDataItemCharEofIndex = 0; // Character End of File
        }
        #endregion
        #region Exception Code
        #region Common Exception Code
        public long ExecptCommonResult;
        //The array below will be implemented as an error object in future:
        public String[] ExecptCommonMessage;
        //
        public void ExecptCommonException(int PassedErrorLevel, int PassedErrorSource, String PassedLocalErrorMessage, long PassedMethodResult) {
            ExecptCommonResult = PassedMethodResult;
            ConsoleVerbosity = 7;
            if (FileSummary.FileName != null) {
                if (FileSummary.FileName.Length > 0) {
                    //
                    sLocalErrorMessage = "File Name: " + FileSummary.FileName + ", ";
                    sLocalErrorMessage += "Direction: " + FileSummary.FileDirectionName;
                    XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMavvXv.RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                    //
                    sLocalErrorMessage = "System: " + FileSummary.SystemName + ", ";
                    sLocalErrorMessage += "Database: " + FileSummary.DatabaseName;
                    XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMavvXv.RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                    //
                    sLocalErrorMessage = "File Owner: " + FileSummary.FileOwner + ", ";
                    sLocalErrorMessage += "File Group: " + FileSummary.FileGroup;
                    XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMavvXv.RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                }
            }
            if (DbIo.CommandCurrent != null) {
                if (DbIo.CommandCurrent.Length > 0) {
                    sLocalErrorMessage = "Database command: " + DbIo.CommandCurrent;
                    XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMavvXv.RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                }
            }
            if (DbIo.spConnString != null) {
                if (DbIo.spConnString.Length > 0) {
                    sLocalErrorMessage = "Database connection command: " + DbIo.spConnString;
                    XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMavvXv.RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
                }
            }
            if (PassedLocalErrorMessage.Length > 0) {
                if (ExecptCommonMessage != null) {
                    String sTemp0 = "";
                    for (int i = 0; i < ExecptCommonMessage.Length; i++) {
                        if (ExecptCommonMessage[i].Length > 0) {
                            sTemp0 = "Error Details (" + (i + 1) + "): " + ExecptCommonMessage[i];
                            XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sTemp0 + "\n");
                        }
                    }
                }
                sLocalErrorMessage = "Error Summary: " + PassedLocalErrorMessage;
                XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMavvXv.RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageEnterResume, "A2" + sLocalErrorMessage + "\n");
            } else {
                sLocalErrorMessage = "No Error Details available.";
                XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMavvXv.RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageEnterResume, "A2" + sLocalErrorMessage + "\n");
            }
            //
            ExecptCommonMessage = null; // clear after all handling...
        }
        #endregion
        #region SQL Exception
        public long ExceptSqlResult;
        // SECTION MessageDetailsMdm not implemented.
        // Two classes Trace and Message
        // Exceptions are part of Trace Class
        // Database classes can be 
        public void ExceptSql(String PassedLocalErrorMessage, ref SqlException ExceptionSql, long PassedMethodResult) {
            ExceptSqlResult = PassedMethodResult;
            sLocalErrorMessage = "SQL Database Exception: (" + PassedMethodResult.ToString() + "): ";
            // + ExceptionSql.Message;
            XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
            //
            ExecptCommonMessage = new String[ExceptionSql.Errors.Count];
            for (int i = 0; i < ExceptionSql.Errors.Count; i++) {
                ExecptCommonMessage[i] = ExceptionSql.Errors[i].ToString();
            }
            ExecptCommonException(iNoErrorLevel, iNoErrorSource, sLocalErrorMessage, PassedMethodResult);
        }
        #endregion
        #region General Exception
        public long ExceptGeneralResult;
        public void ExceptGeneral(String PassedLocalErrorMessage, ref Exception ExceptionGeneral, long PassedMethodResult) {
            ExceptGeneralResult = PassedMethodResult;
            sLocalErrorMessage = "General Exception: (" + PassedMethodResult.ToString() + ") " + ExceptionGeneral.Message;
            XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
            ExecptCommonException(iNoErrorLevel, iNoErrorSource, sLocalErrorMessage, PassedMethodResult);
            XUomMavvXv.RunErrorDidOccur = bNO;
        }
        #endregion
        #region NotSupported Exception
        public long ExceptNotSupportedResult;
        public void ExceptNotSupported(String PassedLocalErrorMessage, ref NotSupportedException ExceptionNotSupported, long PassedMethodResult) {
            ExceptNotSupportedResult = PassedMethodResult;
            sLocalErrorMessage = "NotSupported Exception: (" + PassedMethodResult.ToString() + ") " + ExceptionNotSupported.Message;
            XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
            ExecptCommonException(iNoErrorLevel, iNoErrorSource, sLocalErrorMessage, PassedMethodResult);
            XUomMavvXv.RunErrorDidOccur = bNO;
        }
        #endregion
        #region IO Exception
        public long ExceptIOResult;
        public void ExceptIO(String PassedLocalErrorMessage, ref IOException ExceptionIO, long PassedMethodResult) {
            ExceptIOResult = PassedMethodResult;
            sLocalErrorMessage = "IO Exception: (" + PassedMethodResult.ToString() + ") " + ExceptionIO.Message;
            XUomMavvXv.TraceMdmDo(ref Sender, bIsMessage, XUomMavvXv.TraceMdmCounterLevel1GetDefault(), XUomMavvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMavvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sLocalErrorMessage + "\n");
            ExecptCommonException(iNoErrorLevel, iNoErrorSource, sLocalErrorMessage, PassedMethodResult);
            XUomMavvXv.RunErrorDidOccur = bNO;
        }
        #endregion
        //
        // END OF COMMON EXCEPTIONS
        #endregion
        public override String ToString() {
            if (FileSummary.FileName != null && FileSummary.FileDirectionName != null) {
                String sTemp = "File Summary: " + FileSummary.FileName + " for " + FileSummary.FileDirectionName;
                return sTemp;
            } else { return base.ToString(); }
            return "";
        }
    }
    //
    // End of Mfile
    //

    #region Classes and ItemData Objects

    public class SysDef {
        // TODO System Objects - (TO BE MOVED) - xxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmSystem Objects
        #region $include Mdm.Oss.FileUtil Mfile MdmSystem - AppDomain
        public CrossAppDomainDelegate x22 = null;  // Delegate
        public ApplicationException x21;
        public AppDomainUnloadedException x23;
        public AppDomainInitializer x24; // Delegate
        public AppDomain x24o;
        #endregion
        /* #region $include Mdm.Oss.FileUtil Mfile MdmSystem - Action<T1> Signatures
        public Action AT0; // Delegate
        public Action<T1> AT1; //Delegate
        public Action<T1, T2> AT2; // Delegate
        public Action<T1, T2, T3> AT3; // Delegate
        public Action<T1, T2, T3, T4> AT4; // Delegate
        #endregion
        */
        #region $include Mdm.Oss.FileUtil Mfile MdmSystem Exceptions
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
        #region $include Mdm.Oss.FileUtil Mfile MdmSystem Windows
        // System Windows - StartupEvent
        public StartupEventHandler ooStartTemp = null; // Delegate Application Startup
        public StartupEventArgs ooStartTempA = null; // Delegate Application Startup Arguments
        // System Windows - Controls
        // System Windows - Controls - TextChanged
        public TextChangedEventHandler ooControlTecxChangedEvent;
        public TextChangedEventHandler ooControlTextChanged;
        #endregion
        // XXX - (TO BE MOVED) END - xxxxxxxxxxxxxxxxxxxxxx
        #endregion
        public SysDef() {
        }
    }

    public class MetaDef {
        public bool UsePrimary;
        public DateTime tdtPrimaryLastTouched;
        public DateTime tdtAuxiliaryLastTouched;

        public MetaDef() {
        }
    }

    public class ActionInfoDef {
        //
        // CORE BEHAVIOR SECTION
        // OBJECT, TARGET, Result, VERB
        public int omvOfObject;
        public int omvOfTarget;
        public int omvOfResult;
        public int omvOfExistStatus;
        public int omvOfVerb;
        // omvOfVerb  = omvOfObject + omvOfTarget + omvOfResult;
        // Working Variables
        public String sResult;
        public String sResultReturned;
        public int iResultReturned;

        public ActionInfoDef() {
        }
    }

    public enum FileActionDirectionIs : int {
        Output = 1,
        Input = 2
    }

    public enum ColEscapedIs : int {
        ColEscapedFORBINARY = 1,
        ColEscapedNEWLINE = 2,
        ColEscapedVstudioFormat = 3
    }

    public class FileOptionsDef {
        #region OptionItFlags Declaration
        // Option File Flags
        public bool OptionToDoCheckFileDoesExist; // F
        public bool OptionToDoCreateFileDoesNotExist; // ?
        public bool OptionToDoCreateFileNew; // N
        public bool OptionToDoCreateFileMustNotExist; // M
        public bool OptionToDoCreateMissingFile;
        public bool OptionToDoDeleteFile; // D
        // Option Item Flags
        public bool OptionToDoOverwriteExistingItem; // O
        public bool OptionToDoCheckItemIds;
        public bool OptionToDoCheckItemIdDoesExist; // E
        public bool OptionToDoCheckItemIdDoesNotExist; // ?
        public bool OptionToDoEnterEachItemId; // I
        public bool OptionToDoConvertItem; // C
        public String sConvertableItem;
        // Option Run Flags
        public bool OptionToDoLogActivity;// L
        public bool OptionToDoProceedAutomatically; // A
        // File Bulk Character Conversion (Function)
        // OptionItFlags
        public int OptionItemConvertItFlag;
        public int OptionItemCreateItFlag;
        public int OptionItemWriteItFlag;
        #endregion
        public String TldFileOptions; // TODO Move TldFileOptions

    }

    // ToDo Define File Status Values
    public class FileStatusDef {
        #region FileStatusDef Declaration
        // <Area Id = "IoStateStatus">
        protected internal long ipStatusCurrent;
        public long StatusCurrent {
            get { return ipStatusCurrent; }
            set { ipStatusCurrent = value; }
        }
        // ID of the File not the record (i.e. FSO)
        // property bool FileIdExists
        public bool bpFileIdExists;
        public bool FileIdExists {
            get { return bpFileIdExists; }
            set { bpFileIdExists = value; }
        }
        // <Area Id = "FileDoesExistStatus">
        protected internal int ipFileDoesExistStatus;
        public int FileDoesExistStatus {
            get { return ipFileDoesExistStatus; }
            set { ipFileDoesExistStatus = value; }
        }
        // <Area Id = "FileIsOpenStatus"
        protected internal int ipFileIsOpenStatus;
        public int FileIsOpenStatus {
            get { return ipFileIsOpenStatus; }
            set { ipFileIsOpenStatus = value; }
        }

        // property bool FileIsInitialized
        protected internal bool bpFileIsInitialized = false;
        public bool FileIsInitialized {
            get { return bpFileIsInitialized; }
            set { bpFileIsInitialized = value; }
        }
        // property bool FileDoesExist
        protected internal bool bpFileBoolDoesExist = false;
        public bool FileBoolDoesExist {
            get { return bpFileBoolDoesExist; }
            set { bpFileBoolDoesExist = value; }
        }
        // iFileDoesExist
        // property int FileDoesExist
        protected internal long ipFileDoesExist;
        public long iFileDoesExist {
            get { return ipFileDoesExist; }
            set { ipFileDoesExist = value; }
        }
        // property bool FileDoesExist
        protected internal bool bpFileDoesExist;
        public bool FileDoesExist {
            get { return bpFileDoesExist; }
            set { bpFileDoesExist = value; }
        }
        // property bool FileKeepOpen
        protected internal bool bpFileKeepOpen = false;
        public bool FileKeepOpen {
            get { return bpFileKeepOpen; }
            set { bpFileKeepOpen = value; }
        }
        // property bool FileDoClose
        protected internal bool bpFileDoClose = false;
        public bool FileDoClose {
            get { return bpFileDoClose; }
            set { bpFileDoClose = value; }
        }
        //
        public bool FileIsOpen;
        public int FileNameCurrentNotValid;
        public bool FileNameIsChanged;

        #endregion
        public FileStatusDef() {
            bpFileIsInitialized = false;
            bpFileBoolDoesExist = false;
            ipFileDoesExist = 0;
        }
    }

    #region Mfile Readers, Writers, Database and File Objects and Buffers
    // Database Io
    public class DbIoDef {
        #region $include Mdm.Oss.FileUtil Mfile MdmFileDatabaseObjectConnection
        // <Area Id = "FileDatabaseObject">
        // Database Connection
        // ofd  - 	Object - File - Database Connection
        public SqlConnection SqlDbConnection = null;
        // ofde - 	Object - File - Database Connection - Error

        // ofdcd - 	Object - File - Database Connection - Delegate

        // ofdcv - 	Object - File - Database Connection - Event

        #endregion

        public int SqlDbCommandTimeout = 15;

        // <Area Id = "ConnString">
        // property String ConnString
        protected internal String spConnString;
        public String sConnString {
            get { return spConnString; }
            set {
                spConnString = value;
            }
        } //   

        // Database Command - xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmFileDatabaseObjectCommand
        // ofdc - 		Object - File - Database Command
        public SqlCommand SqlDbCommandObject = null;
        // ofdce - 	   Object - File - Database Command - Error

        // ofdccd - 	Object - File - Database Command - Delegate

        // ofdccv - 	Object - File - Database Command - Event

        // ofdcad - 	Object - File - Database Command - Adapter
        public SqlDataAdapter SqlDbCommandAdapterObject = null;

        public String CommandCurrent = null;
        public String CommandPassed = null;
        #endregion
        // FILE SUBCLASS - FILE - READER
        // Database Sql ItemData Reader - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmFileObject SqlDataReader
        // ofddr - 		Object - File - Database Connection - DataReader
        public SqlDataReader SqlDbDataReaderObject = null;
        public SqlDataAdapter SqlDbDataWriterObject = null;

        // ofddre - 	Object - File - Database Connection - DataReader- Error

        // ofddrcd - 	Object - File - Database Connection - DataReader- Delegate

        // ofddrcv - 	Object - File - Database Connection - DataReader- Event

        #endregion

        public DbIoDef() {
        }
    }
    // Ascii, Text, Xml, Uml, Tld, Delimited and FeildSeparated.
    public class FileIoDef {
        // File Object and ItemData - dxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmFileObjects
        // <Area Id = "FileObject">
        public Object DbFileObject = null;
        // System IO
        // System IO Stream
        public FileStream DbFileStreamObject = null;
        public StreamReader DbFileStreamReaderObject = null; // Stream Reader
        public StreamWriter DbFileStreamWriterObject = null; // Stream Writer
        // System IO TextReader
        public TextReader DbFileTextReadObject = null; // Text Reader
        #endregion
        #region File Buffers
        public String IoReadBuffer {
            get { return IoReadBuffer; }
            set { IoReadBuffer = value; }
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

        public FileIoDef() {
        }
    }
    // Binary File
    public class BinIoDef {
        // Buf.Seek
        // public File ofNativeFile;
        public int iRecordSize = 1024;
        public int iOffsetSize = (1024 * 32) - 1;
        public int iCurrentOffset;
        public int iCurrentOffsetModulo;
        public int iCurrentOffsetRemainder;
        public int CurrentAttrCounter;
        public BinIoDef() {
        }
    }
    // File Buffer
    public class BufDef {
        #region $include Mdm.Oss.FileUtil Mfile MdmBuf.
        #region $include Mdm.Oss.FileUtil Mfile MdmBuf. Name
        public String FileName;
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmBuf. Buffer
        // Buf. Fields
        public String FileWorkBuffer;
        public bool DoesExist = false;
        public String LineBuffer;
        public String NewItem;
        //
        // public String[] Buf.NewItem;     
        //  sNewItem=""
        // NOTE public String[] Item.ItemData; see Cols instead
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmBuf. Control Flags
        public bool ItemIsAtEnd = false;
        public bool HasCharacters = false;
        //  Convert Parameters
        public String ConvertableItem;
        public int ItemConvertFlag;
        //  Attr Indexing
        public int AttrIndex;
        public int AttrMaxIndex;
        //  Character Controls
        public bool CharactersIsFound = false;
        public int CharactersFound;
        public int CharCounter; // ItemData Items in AnyString
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile Mdm Read
        //  Read
        //  Read Status
        public bool ReadFileIsAtEnd = false;
        public bool ReadFileError = false;
        // Number of Reads
        public int ReadFileCounter;
        // Number of Bytes Read
        public bool BytesIsRead = false;
        public int BytesRead;
        public int BytesReadTotal;
        public int BytesConverted;
        public int BytesConvertedTotal;
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile Mdm Buffer Character Indexing
        //  Character Indexing for Buffer
        public int CharIndex = 1;
        public int CharMaxIndex;
        public int CharItemEofIndex;
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile Mdm Write
        //  Writes
        //  Number of Writes
        public int WriteFileCounter;
        //  Number of Bytes Writen
        public bool BytesIsWriten = false;
        public int BytesWriten;
        public int BytesWritenTotal;
        #endregion
        #endregion
        public BufDef() {
        }
    }
    #endregion

    #region Mfile ItemData Structures and Subclasses

    #region File
    public class FileSummaryDef {
        public FileSummaryDef() {
            FileSummaryDefObject = this;
            DataClear(ref FileSummaryDefObject);
        }
        FileSummaryDef FileSummaryDefObject;
        #region FileSummary Clear
        public long DataClear(ref FileSummaryDef PassedFileSummary) {
            PassedFileSummary.FileOpt = new FileOptionsDef();
            #region SetInputItem
            PassedFileSummary.FileName = "Unknown99";
            PassedFileSummary.FileDirectionName = "INPUT";
            // State Change
            PassedFileSummary.FileNameDefault = "";
            PassedFileSummary.FileNameOriginal = "";
            PassedFileSummary.FileVersion = "";
            PassedFileSummary.FileVersionDate = "";
            PassedFileSummary.ItemWriteItBoolFlag = false;
            #endregion
            #region ItemType Set
            PassedFileSummary.FileType = "Unknown FileType99";
            PassedFileSummary.FileTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeUNKNOWN; // TODO SHOULD LOAD FROM OPTIONS
            PassedFileSummary.FileSubType = "Unknown FileSubType99";
            PassedFileSummary.FileSubTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeUNKNOWN;
            #endregion
            #region File Other Attributes
            PassedFileSummary.SystemName = "";
            PassedFileSummary.ServerName = "";
            PassedFileSummary.ServiceName = "";
            PassedFileSummary.DatabaseName = "";
            PassedFileSummary.FileOwner = "";
            PassedFileSummary.FileGroup = "";
            PassedFileSummary.FileGroupName = "";
            PassedFileSummary.FileGroupId = 0;
            #endregion
            return 0;
        }
        #endregion
        public FileOptionsDef FileOpt;
        #region ItemDef Declaration
        // public Object File;
        public Object FileObject;
        // File
        public String spFileName;
        public String FileName {
            get { return spFileName; }
            set { spFileName = value; }
        }
        public String spFileDirectionName;
        public String FileDirectionName {
            get { return spFileDirectionName; }
            set { spFileDirectionName = value; }
        }
        // FileActionDirection
        public int ipFileActionDirection;
        public int FileActionDirection {
            get { return ipFileActionDirection; }
            set { ipFileActionDirection = value; }
        }
        // FileType
        public String spFileType;
        public String FileType {
            get { return spFileType; }
            set { spFileType = value; }
        }
        public long ipFileTypeId;
        public long FileTypeId {
            get { return ipFileTypeId; }
            set {
                ipFileTypeId = value;
                FileTypeBaseId = FileTypeBaseGet(ipFileTypeId);
            }
        }
        public long FileTypeBaseId;

        public String spFileSubType;
        public String FileSubType {
            get { return spFileSubType; }
            set { spFileSubType = value; }
        }
        public long ipFileSubTypeId;
        public long FileSubTypeId {
            get { return ipFileSubTypeId; }
            set {
                ipFileSubTypeId = value;
                FileSubTypeBaseId = FileSubTypeBaseGet(ipFileSubTypeId);
                FileSubTypeMinorId = FileSubTypeMinorGet(ipFileSubTypeId);
            }
        }
        public long FileSubTypeBaseId;
        public long FileSubTypeMinorId;
        #region Check File Types
        public long FileTypeBaseGet(long PassedFileTypeId) {
            long ThisBaseFileTypeId;
            return ThisBaseFileTypeId = PassedFileTypeId &
                ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeData
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeDictData
                );
        }
        public long FileSubTypeBaseGet(long PassedFileSubTypeId) {
            long ThisBaseFileSubTypeId;
            return ThisBaseFileSubTypeId = PassedFileSubTypeId &
            ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMS
            | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMY
            | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDB2
            | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeORACLE
            | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeXML
            | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXT
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXTSTD
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeASC
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeFIX
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDAT
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeCSV
            | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_CSV 
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_ROW 
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_NATIVE 
                //| (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_NATIVE_ONE
            );
        }
        public long FileSubTypeMinorGet(long PassedFileSubTypeId) {
            // NOTE Very early playing with masks and code text formatting.  
            // NOTE Eventual use of bit arrays?
            // NOTE PRACTICES All of this gets rolled into the property get / set locations eventually in either case.
            // NOTE This is not required as the Sub and Minor types share the save Sub byte value because a long was used.
            // NOTE The long might be required eventually as there are a massive number of variations in file format
            // NOTE including a variety of proprietary, custom and "one off" types that would be retained 
            // NOTE and also added dynamically.
            long ThisBaseFileSubTypeId;
            long ThisMask = 0x0FFFFFFF;
            return ThisBaseFileSubTypeId = PassedFileSubTypeId |
                (ThisMask & (
                (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeSQL
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMY
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDB2
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeORACLE
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeXML
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXTSTD
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeASC
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeFIX
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDAT
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeCSV
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_CSV
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_ROW
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_NATIVE
                | (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_NATIVE_ONE
                ));
        }
        #endregion
        // FileNameControl
        public String spFileNameCurrent;
        public String FileNameCurrent {
            get { return spFileNameCurrent; }
            set { spFileNameCurrent = value; }
        }
        //
        public String spFileNameNext;
        public String FileNameNext {
            get { return spFileNameNext; }
            set { spFileNameNext = value; }
        }
        public String FileNameDefault;
        public String FileNameOriginal;
        // File Record Version
        public String FileVersion;
        public String FileVersionDate;
        // FileId
        public int ipFileId;
        public int FileId {
            get { return ipFileId; }
            set { ipFileId = value; }
        }
        // Item Set Access
        // ItemIdCurrent
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

        // System
        public String spSystemName;
        public String SystemName {
            get { return spSystemName; }
            set { spSystemName = value; }
        }
        // SystemObject
        public Object SystemObject;
        // Database
        public String spDatabaseName;
        public String DatabaseName {
            get { return spDatabaseName; }
            set { spDatabaseName = value; }
        }
        // Connection
        public SqlConnection DatabaseObject;
        // Owner
        // <Area Id = "FileOwner">
        protected internal String spFileOwner = "";
        public String FileOwner {
            get { return spFileOwner; }
            set { spFileOwner = value; }
        }
        // FileGroup
        // <Area Id = "FileGroup">
        protected internal String spFileGroup = "";
        public String FileGroup {
            get { return spFileGroup; }
            set { spFileGroup = value; }
        }
        //
        public String ipFileActionDirectionName;
        public String FileActionDirectionName {
            get { return ipFileActionDirectionName; }
            set { ipFileActionDirectionName = value; }
        }
        // FileOptionsDef
        public String spFileOptions = "";
        public String FileOptions {
            get { return spFileOptions; }
            set { spFileOptions = value; }
        }
        public String FileOptionsCurrent = "";
        // FileWrite
        public bool ItemWriteItBoolFlag = false;
        // Database Connection Lines
        // File Command Lines
        public String MasterSystemLine;
        public String MasterDatabaseLine;
        public String MasterFileLine;
        // User Command Lines
        public String UserNameLine;
        public String UserPasswordLine;
        public bool UserPasswordRequiredOption;
        // Security Lines
        public String SecurityMasterSystemLine;
        public String SecurityMasterDatabaseLine;
        public String SecurityMasterFileLine;
        // <Area Id = "FileGroupInformation">
        public String spFileGroupName = "";
        public String FileGroupName {
            get { return spFileGroupName; }
            set { spFileGroupName = value; }
        }

        // <Area Id = "FileGroupInformation">
        public int ipFileGroupId = 99999;
        public int FileGroupId {
            get { return ipFileGroupId; }
            set { ipFileGroupId = value; }
        }

        // <Area Id = "ServiceNameInformation">
        public String spServiceName = "";
        public String ServiceName {
            get { return spServiceName; }
            set { spServiceName = value; }
        }

        // <Area Id = "ServerNameInformation">
        public String spServerName = "";
        public String ServerName {
            get { return spServerName; }
            set { spServerName = value; }
        }
        #endregion
        public override String ToString() {
            if (FileName != null && FileDirectionName != null) {
                String sTemp = "File Summary: " + FileName + " for " + FileDirectionName;
                return sTemp;
            } else { return base.ToString(); }
            return "";
        }
    }

    public class FileIdDef {
        #region File Core Properties
        protected internal String spFileNameFull;
        public String FileNameFull {
            get { return spFileNameFull; }
            set { spFileNameFull = value; }
        }
        protected internal String spFileNameLine;
        public String FileNameLine {
            get { return spFileNameLine; }
            set { spFileNameLine = value; }
        }
        protected internal String spFileExt;
        public String FileExt {
            get { return spFileExt; }
            set { spFileExt = value; }
        }
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmFileMeta_Type_Details
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
        public Guid gFileNameGuid {
            get { return gpFileNameGuid; }
            set { gpFileNameGuid = value; }
        }
        // File Status ////////////////////
        #endregion
        #region File Path and Location Info
        // <Area Id = "SourceDriveSystem - PhysicalLocation">
        protected internal int ipFileDriveSystemId = -99999;
        public int FileDriveSystemId {
            get { return ipFileDriveSystemId; }
            set { ipFileDriveSystemId = value; }
        }

        // <Area Id = "SourceDriveName - PhysicalLocation">
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

        protected internal String spFileDriveLetterAlias;
        public String FileDriveLetterAlias {
            get { return spFileDriveLetterAlias; }
            set { spFileDriveLetterAlias = value; }
        }

        protected internal String spFileDriveShortName;
        public String FileDriveShortName {
            get { return spFileDriveShortName; }
            set { spFileDriveShortName = value; }
        }

        // <Area Id = "SourcePathName">
        protected internal String spPathName;
        public String sPathName {
            get { return spPathName; }
            set { spPathName = value; }
        }

        protected internal String spPathNameAlias;
        public String sPathNameAlias {
            get { return spPathNameAlias; }
            set { spPathNameAlias = value; }
        }

        protected internal int ipPathId = -99999;
        public int iPathId {
            get { return ipPathId; }
            set { ipPathId = value; }
        }

        protected internal String spPathShortName;
        public String sPathShortName {
            get { return spPathShortName; }
            set { spPathShortName = value; }
        }
        #endregion
        #endregion

        public void DataClear() {
            FileNameFull = "";
            FileNameLine = "";
            FileExt = "";
            FileNameAlias = "";
            FileShortName = "";
            FileShort83Name = "";
            FileShortUnixName = "";
            gpFileNameGuid = new Guid();
        }

    }
    #endregion    

    #region Row / Item
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

    public class RowInfoDef {
        // FileRowInfo
        public bool LineIsRow = false;
        public bool LineIsColumn = false;
        // Read mode on database
        public int UseMethod;
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

        public int ColMaxIndex = Mdm.Oss.FileUtil.Mfile.ColArrayMax;
        public int ColMaxIndexNew = Mdm.Oss.FileUtil.Mfile.ColArrayMax + 1;
        // property String aColValues
        //
        public Object[] ColArray = new System.Object[Mdm.Oss.FileUtil.Mfile.ColArrayMax];
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

    public class ColInfoDef {
        // Action
        public int ColAction;
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
        public String sColRowIndexName;
        public DateTime tdtRowLastTouched;
        // Column
        public bool bHasRows;
        public bool bRowContinue;
        public int iRowMax;
        // Column
        public int ColIndex;
        public int ColCount;
        public String sColIndexName;
        public DateTime tdtIndexLastTouched;
        public int ColCountVisible;
        public int ColCountHidden;
        public int ColumnMax;
        // Get Results
        public int iGetIndex;
        public String sGetResultToString;
        public String sGetResultNotSupported;
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

    public class ColPickDef {
        #region RowColumnCharacterControl
        // ColInfo
        public String ColText;
        public String ColExtracted;
        public String ColTempId;
        public String ColTempFileName;
        // FileCharacterControl
        public String ColCharacter;
        //
        public bool ColQuoteBool = true;
        public enum ColQuoteIs : int {
            ColQuoteDOUBLE = 1,
            ColQuoteSINGLE = 2,
            ColQuoteBACKSLASH = 3,
            ColQuoteFORWARD = 4,
            ColQuoteBRACKETE = 5
        }
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

    public class ColTypeDef {
        #region $include Mdm.Oss.FileUtil Mfile MdmPickDict DictionaryItem Constants
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
        #region $include Mdm.Oss.FileUtil Mfile MdmDictionaryColumn
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
    public class DbIdDef {
        // File Database Information - xxxxxxxxxx
        // #region $include Mdm.Oss.FileUtil Mfile MdmFileBaseDatabase
        #region $include Mdm.Oss.FileUtil Mfile MdmFileDatabaseName

        // <Area Id = "FileDatabaseInformation">
        // <Area Id = "FileDatabaseFileNameLongInformation">
        protected internal String spDatabaseFileNameLong;
        public String DatabaseFileNameLong {
            get { return spDatabaseFileNameLong; }
            set { spDatabaseFileNameLong = value; }
        }

        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmFileDatabaseSecurity
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmFileDatabaseUser
        #endregion
        // #endregion

        // <Area Id = "DatabaseControlMessages">
        public String sMformStatusMessage;
        public String sMessageBoxMessage;

        // <Area Id = "SourceDatabaseFileGroupInformation">

        // <Area Id = "SourceDatabaseFileNameInformation">

        // <Area Id = "DatabaseMessageConstants">

        protected internal const String SQL_CONNECTION_STRING =
            "Server=localhost;" +
            "DataBase=;" +
            "Integrated Security=SSPI";

        protected internal const String CONNECTIONError_MSG =
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
            DatabaseFileNameLong = "";
            //
            sMessageBoxMessage = "";
            sMformStatusMessage = "";
        }
    }

    public class DbStatusDef {
        // File Database Connection - xxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmFileConnection_ControlFields

        // <Area Id = "DbFileConnectionStatus">
        protected internal long ipDatabaseFileConnStatus = -99999;
        public long iDatabaseFileConnStatus {
            get { return ipDatabaseFileConnStatus; }
            set {
                ipDatabaseFileConnStatus = value;
            }
        }

        // property bool DbStatus.ConnIsInitialized
        protected internal bool bpConnIsInitialized = false;
        public bool ConnIsInitialized {
            get { return bpConnIsInitialized; }
            set {
                bpConnIsInitialized = value;
            }
        } //   

        // <Area Id = "ConnDoesExist">
        protected internal bool bpConnDoesExist = false;
        public bool ConnDoesExist {
            get { return bpConnDoesExist; }
            set {
                bpConnDoesExist = value;
            }
        }

        // <Area Id = "SourceDatabaseInformation">
        public long ipConnDoesExist = -99999;

        // property long ConnStatus 
        protected internal long ipConnStatus;
        public long iConnStatus {
            get { return ipConnStatus; }
            set {
                ipConnStatus = value;
            }
        } //   

        // property bool ConnIsValid
        protected internal bool bpConnIsValid = false;
        public bool ConnIsValid {
            get { return bpConnIsValid; }
            set {
                bpConnIsValid = value;
            }
        } //   

        // property bool ConnIsCreating
        protected internal bool bpConnIsCreating = false;
        public bool ConnIsCreating {
            get { return bpConnIsCreating; }
            set {
                bpConnIsCreating = value;
            }
        }
        protected internal bool bpConnIsCreated = false;
        public bool ConnIsCreated {
            get { return bpConnIsCreated; }
            set {
                bpConnIsCreated = value;
            }
        }
        protected internal bool bpConnIsConnecting = false;
        public bool ConnIsConnecting {
            get { return bpConnIsConnecting; }
            set {
                bpConnIsConnecting = value;
            }
        }
        protected internal bool bpConnIsConnected = false;
        public bool ConnIsConnected {
            get { return bpConnIsConnected; }
            set {
                bpConnIsConnected = value;
            }
        }
        protected internal bool bpConnIsOpen = false;
        public bool ConnIsOpen {
            get { return bpConnIsOpen; }
            set {
                bpConnIsOpen = value;
            }
        }
        protected internal bool bpConnIsClosed = false;
        public bool ConnIsClosed {
            get { return bpConnIsClosed; }
            set {
                bpConnIsClosed = value;
            }
        }

        // DoConnectionClose = false;
        // DoConncetionDispose = false;
        // property bool ConnDoClose
        protected internal bool bpConnDoClose = false;
        public bool ConnDoClose {
            get { return bpConnDoClose; }
            set {
                bpConnDoClose = value;
            }
        } //   

        // property bool ConnDoDispose
        protected internal bool bpConnDoDispose = false;
        public bool ConnDoDispose {
            get { return bpConnDoDispose; }
            set {
                bpConnDoDispose = value;
            }
        } //

        // <Area Id = "DbFileStatus">
        public bool bpDatabaseFileIsInitialized = false;
        public bool bpDatabaseFileDoesExist = false;
        public bool bpDatabaseFileIsInvalid = false;
        public bool bpDatabaseFileIsCreating = false;
        public bool bpDatabaseFileIsCreated = false;

        // property bool DatabaseFileNameIsValid
        protected internal bool bpDatabaseFileNameIsValid = false;
        public bool DatabaseFileNameIsValid {
            get { return bpDatabaseFileNameIsValid; }
            set { bpDatabaseFileNameIsValid = value; }
        } //   

        // <Area Id = "FileGroupStatus">
        public bool bpDbFileGroupDoesExist = false;
        public bool bpDbFileGroupIsInvalid = false;
        public bool bpDbFileGroupIsCreating = false;
        public bool bpDbFileGroupIsCreated = false;

        #endregion

        public DbStatusDef() {
        }
    }

    public class DbMasterDef {
        // OBJECT SUBCLASS - MASTER FILE
        // Database Master File - xxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmFileMasterServer
        // <Area Id = "FileCommand">
        #region $include Mdm.Oss.FileUtil Mfile MdmFileMaster_Security_Fields

        // <Area Id = "SecurityControl">
        // property bool UseSSIS
        protected internal bool bpUseSSIS = true;
        public bool UseSSPI {
            get { return bpUseSSIS; }
            set { bpUseSSIS = value; }
        } //   

        protected internal String MstrDbSecurityId = "99999";
        protected internal String MstrDbSecurity;

        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmFileMaster_User_Control

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
        #region $include Mdm.Oss.FileUtil Mfile MdmFileMaster_Server_Database
        // <Area Id = "MasterServerInformation">

        // <Area Id = "MasterServerDatabase">
        //
        protected internal String MstrDbSystem = @"";
        protected internal String MstrDbSystemMdm = @"MDMPC11";
        // protected internal String MstrDbSystemDefault = "localhost";
        protected internal String MstrDbSystemDefault = @"SYSTEM99";
        protected internal String MstrDbSystemDefaultMdm = @"MDMPC11";
        //
        // protected internal String MstrDbService = "SQLSERVER";
        protected internal String MstrDbService = @"";
        protected internal String MstrDbServiceMdm = @"SQLEXPRESS";
        // protected internal String MstrDbServiceDefault = "SQLSERVER";
        protected internal String MstrDbServiceDefault = @"SERVICE99";
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
        protected internal String MstrDbDatabaseDefault = "MdmDatabase99";
        protected internal String MstrDbDatabaseDefaultMdm = "MdmDatabase99";

        // <Area Id = "MasterServer - OwnerControl">
        protected internal String MstrDbOwnerId = "99999";
        // protected internal String MstrDbOwner = "dbo";
        // protected internal String MstrDbOwnerDefault = "sa";
        // protected internal String MstrDbOwnerDefaultMdm = "MdmOwner99";
        protected internal String MstrDbOwner = "dbo";
        protected internal String MstrDbOwnerDefault = "sa";
        protected internal String MstrDbOwnerDefaultMdm = "MdmOwner99";

        // <Area Id = "MasterConnectionCommand">

        // <Area Id = "MasterDatabase - Connection">
        // protected internal String MstrConnString;
        protected internal String MstrConnString = Mdm.Oss.FileUtil.DbIdDef.SQL_CONNECTION_STRING;

        // <Area Id = "MasterDatabaseStatus">
        protected internal bool MstrDbDatabaseIsInitialized = false;
        protected internal bool MstrDbDatabaseDoesExist = false;
        protected internal bool MstrDbDatabaseIsInvalid = false;
        protected internal bool MstrDbDatabaseIsCreating = false;
        protected internal bool MstrDbDatabaseIsCreated = false;

        // <Area Id = "EndOfMasterServerAndDatabase">

        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmFileMaster_FileGroup
        // <Area Id = "FileGroup">

        protected internal String spMstrDbFileGroupServerId = "99999";
        protected internal String spMstrDbFileGroupDbId = "99999";
        protected internal String spMstrDbFileGroupId = "99999";
        protected internal String spMstrDbFileGroup = "MdmFileGroup99";
        protected internal String spMstrDbFileGroupDefault = "MdmFileGroup99";
        // <Area Id = "FileGroupCommand">
        protected internal String spMDbFileGroupCreateCmd = "not used";
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmFileMaster_DbFile

        // <Area Id = "MasterFile">
        protected internal String spMstrDbFileDbId = "99999";
        protected internal String spMstrDbFileDb = "MdmDatabase99";
        protected internal String spMstrDbFileDbDefault = "MdmDatabase99";

        protected internal String spMstrDbFileId = "99999";
        // protected internal String spMstrDbFile = "MdmFile99";
        protected internal String spMstrDbFile = "INFORMATION_SCHEMA.TABLES";
        // protected internal String spMstrDbFile = "sys.objects";

        // DbIo.CommandCurrent = "USE[" + FileSummary.DatabaseName + "]; SELECT * FROM 
        // INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FileId.spFileNameFull + "';";
        // DbIo.CommandCurrent = "USE[" + FileSummary.DatabaseName + "]; SELECT * FROM 
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
        #region $include Mdm.Oss.FileUtil Mfile MdmFileMaster_DbFileConnection
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
    public class DbMasterSynDef {
        // OBJECT SUBCLASS - COMMAND PHRASES
        // <Area Id = "MasterDatabase - Creation">
        protected internal String spMstrDbFileCreateCmd;
        protected internal String MstrDbDatabaseCreateCmd;
        
        #region $include Mdm.Oss.FileUtil Mfile MdmFileDatabasePhrase_Constrution
        // <Area Id = "Phrases">
        protected internal String spMstrDbPhraseDoLine = "; ";
        protected internal String spMstrDbPhraseExecute = "GO"; // wrong
        protected internal String spMstrDbPhraseDoExecute = "GO"; // wrong

        #region $include Mdm.Oss.FileUtil Mfile MdmFileMasterServerAndDatabasePhrases
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

        public bool bpDbFilePhraseSelectIsUsed = true;
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
        #region $include Mdm.Oss.FileUtil Mfile MdmFileCreationPhrases
        // <Area Id = "FileCreationPhrases">

        protected internal String spMstrDbFilePhrase;

        public bool bpDbFilePhraseUseIsUsed = true;
        protected internal String spMstrDbFilePhraseUse = "USE ";
        protected internal String spMstrDbFilePhraseUseEnd;

        public bool bpDbFilePhraseIfIsUsed = true;
        protected internal String spMstrDbFilePhraseIf = "IF EXISTS (";
        protected internal String spMstrDbFilePhraseIfEnd = ")";

        public bool bpDbFileFilePhraseSelectIsUsed = true;
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
        protected internal String spMstrDbFilePhraseCreateTableName = "MdmFile99";
        // + "HowToDemo.dbo.This";TableName

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
    }

    public class DbSynDef {
        // SYNTAX - xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile MdmSqlSyntax
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
        #region $include Mdm.Oss.FileUtil Mfile MdmSql File Create Commands
        // Sql File Command
        public String spSqlFileCreateCmdScript;
        public String spSqlFileCreateCmd;
        public String spSqlFileDeleteCmd;
        public String spSqlFileAlterCmd;
        public String spSqlFileViewCmd;
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile MdmSql File Column Commands
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
        #region $include Mdm.Oss.FileUtil Mfile MdmSql Command Output
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
        #region $include Mdm.Oss.FileUtil Mfile MdmCreate Database and Database Objects
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
    public class PickRowDef {
        #region $include Mdm.Oss.FileUtil Mfile MdmPickDictControl
        // Index Key for array and relative row number
        public int PdIndex;
        public static int PdIndexMax = Mdm.Oss.FileUtil.Mfile.ColAliasMax;
        public static int PdIndexMaxNew = Mdm.Oss.FileUtil.Mfile.ColAliasMax + 1; // Used in the new
        public int PdIndexHigh;
        public int PdIndexAliasLow;
        //
        public PickDictItemDef[] PickDictArray = new PickDictItemDef[PdIndexMaxNew];
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

        public int PdIndexTemp;
        public int PdItemCount;

        public int PdErrorCount;
        public int PdErrorWarningCount;

        public bool PdIndexDoSearch = true;
        public int ColumnDataPoints;
        #endregion
        public PickRowDef() {
        }
    }

    public class PickDictItemDef {
        public String ItemId;
        public int ItemIntId;
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
        public int DictColIndex; // Dictionary Column Number
        public int ColumnDataPoints;
        public int ColumnType;
        public int ColumnInvalid;
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
        public String ColumnWidthString;
        public int ColumnWidth;
        public bool ColumnWidthIsNumeric;
        // ??
        public String sHeadingLong;
        public String sHelpShort;
        public String sRevColumnName;

        public int ColumnNumericPoints;
        public int ColumnDecimals;
        public int ColumnCurrencyPoints;
        public int ColumnDateFormat;
        public int ColumnFunctionPoints;
        public bool ColumnSuFile;

        public int DictColumnTouched;
        public bool DictColumnIdDone;
        public int DictColumnLength;
        public bool DictColumnLengthChange;
        public bool DictColumnDefinitionFound;

        public String sColumnTypeWord;
        public bool ColumnUseParenthesis;

        public String AttrTwoStringValue;
        public int PdIndexAttrTwo;
        //
        public String AttrTwoStringAccounName;
        public String AttrThreeFileName;
        // Add
        public String sColumnAdd;
        public String sColumnDelete;
        public String sColumnAlter;
        public String sColumnUpdate;
        public String sColumnValidate;
        public String sColumnStatisticsGet;
        public String sColumnView;

        public bool ColumnAdd;
        public bool ColumnDelete;
        public bool ColumnAlter;
        public bool ColumnUpdate;
        public bool ColumnValidate;
        public bool ColumnStatisticsGet;
        public bool ColumnView;

        public String sTrigerAdd;
        public String sTrigerDelete;
        public String sTrigerAlter;
        public String sTrigerUpdate;

        public bool bTrigerAdd;
        public bool bTrigerDelete;
        public bool bTrigerAlter;
        public bool bTrigerUpdate;
    }

    // NEXT move to pick
    // Pick Dict Item Class
    public class PickDictIndexDef {
        // TODO Pick Dict Item Class
        //
        public static int iPicDictIndexMax = Mdm.Oss.FileUtil.Mfile.ColArrayMax;
        public static int iPicDictIndexMaxNew = Mdm.Oss.FileUtil.Mfile.ColArrayMax + 1;
        //
        public String[] PickDictIndexDefArray = new string[Mdm.Oss.FileUtil.Mfile.ColArrayMax];
        //
        public static int ipPickDictIndexDefGet;
        //
        public static int ipPickDictIndexDefIndex;
        public static int PickDictIndexDefIndex {
            get { return ipPickDictIndexDefIndex; }
            set { ipPickDictIndexDefIndex = value; }
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
            get { return PickDictIndexDefArray.Length; }
        }
        // Indexer declaration.
        // Input parameter is validated by client 
        // code before being passed to the indexer.
        public String this[int iPassedPickDictIndexDefIndex] {
            get {
                ipPickDictIndexDefIndex = iPassedPickDictIndexDefIndex;
                return PickDictIndexDefArray[iPassedPickDictIndexDefIndex];
            }

            set {
                ipPickDictIndexDefIndex = iPassedPickDictIndexDefIndex;
                PickDictIndexDefArray[iPassedPickDictIndexDefIndex] = value;
            }
        }
        // This method finds the sPickDictIndexDefInstance or returns -1
        public int PickDictIndexDefIndexGet(String sPassedPickDictIndexDef) {
            ipPickDictIndexDefGet = 0;
            foreach (String sPickDictIndexDefInstance in PickDictIndexDefArray) {
                if (sPickDictIndexDefInstance == sPassedPickDictIndexDef) {
                    // ipPickDictIndexDefIndex = ipPickDictIndexDefGet;
                    return ipPickDictIndexDefGet;
                }
                ipPickDictIndexDefGet++;
            }
            return -1;
        }
        // This method finds the sPickDictIndexDefInstance or returns ""
        public String PickDictIndexDefGet(String sPassedPickDictIndexDef) {
            ipPickDictIndexDefGet = 0;
            foreach (String sPickDictIndexDefInstance in PickDictIndexDefArray) {
                ipPickDictIndexDefGet += 1;
                // PickDictIndexDefArray[ipPickDictIndexDefGet] = "?";
                if (sPickDictIndexDefInstance == sPassedPickDictIndexDef) {
                    ipPickDictIndexDefIndex = ipPickDictIndexDefGet;
                    return sPickDictIndexDefInstance;
                }
                ipPickDictIndexDefGet++;
            }
            return "";
        }
        // The get accessor returns an integer for a given string
        public String this[String sPassedPickDictIndexDef] {
            get { return (PickDictIndexDefGet(sPassedPickDictIndexDef)); }
            set { PickDictIndexDefArray[PickDictIndexDefIndexGet(sPassedPickDictIndexDef)] = value; }
        }
    }
    #endregion

    #endregion

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

    #region Transformation
    public class FileTransformControlBaseDef : DefStdBaseRunFileConsole {
        #region FileTransformRun InputOutput
        public FileTransformControlBaseDef()
            : base() {

            // InputFile.FileTransformControl = this;
            // OutputFile.FileTransformControl = this;
        }
        // Transformation
        public String TransformFileTitle = "";
        // FileActionDirection
        public int ipFileActionDirection;
        public int FileActionDirection {
            get { return ipFileActionDirection; }
            set { ipFileActionDirection = value; }
        }
        // Source (Import) and Destination (Output) Object
        #region InputItem Declaration
        // InputFile
        public Mfile InputFile;
        #endregion
        #region FileOutputItem Declaration
        public Mfile OutputFile;
        #endregion
        #endregion
        public FileOptionsDef FileOpt = new FileOptionsDef();
        #region ItemIdNotes
        // And Id would be found in the 
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
        #region InputItemClassFields Passed
            // SourceDestinationObject
        #region InputItem Passed
            // InputFile
            String sPassedInputFileName,
            // Object ofPassedInputFileObject,
            Mfile ofPassedInputFileObject,
            String sPassedInputFileOptions,
        #endregion
        #region FileOutputItem Passed
            // OutputSystem
            String sPassedOutputSystemName,
           Object ooPassedOutputSystemObject,
            // OutputDatabase
            String sPassedOutputDatabaseName,
            SqlConnection PassedOutputDatabaseObject,
            // OutputFile
            String sPassedOutputFileName,
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
            InputFile.FileSummary.FileName = sPassedInputFileName;
            InputFile.FileSummary.FileDirectionName = "INPUT";
            InputFile.Item.ItemLen = sPassedInputFileName.Length;
            #endregion
            #region SetFileOutputItem
            InputFile.FileSummary.FileName = sPassedOutputFileName;
            InputFile.FileSummary.FileDirectionName = "OUTPUT";
            // MFILE1 OBJECT
            if (ofPassedOutputFileObject != null) {
                if (OutputFile != null) {
                    OutputFile = null;
                }
                OutputFile = ofPassedOutputFileObject;
            }
            #endregion
            #region ItemId
            InputFile.Item.ItemId = sPassedOutputItemId;
            #endregion
            #region SetMinputTldItemClassFields
            // Source and Destination Object
            #region SetInputItem Empty
            #endregion
            #region SetFileOutputItem Empty
            // OutputSystem
            // String FileSummary.spSystemName;
            // Object ooSystemObject;
            // Output Database
            // String spDatabaseName;
            // SqlConnection DatabaseObject;
            // OutputFile
            // String OutputFileName;
            // MfilePickDb OutputFile;
            // String sPassedOutputFileOptions;
            // OutputItemId
            // String ItemId;
            // 
            #endregion
            #region SetInputItemId
            // (Existing in Output vs Options)
            InputFile.Item.ItemId = "";
            InputFile.FileSummary.ItemIdCurrent = "";
            // FileTransformControl.ItemId = @"";
            // FileTransformControl.PickRow.PickDictArray.ItemId = @"";
            InputFile.Item.ItemIdIsChanged = bNO;
            InputFile.FileStatus.FileNameIsChanged = bNO;
            #endregion
            #endregion
            #region ClassInternalProperties
            //#region FileInputItemType Set
            //InputFile.FileSummary.FileType = "Unknown FileType99";
            //InputFile.FileSummary.FileTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeUNKNOWN; // TODO SHOULD LOAD FROM OPTIONS
            //InputFile.FileSummary.FileSubType = "Unknown FileSubType99";
            //InputFile.FileSummary.FileSubTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeUNKNOWN;
            //#endregion
            //#region FileOutputItemType Set
            //OutputFile.FileSummary.FileType = "Unknown FileType99";
            //OutputFile.FileSummary.FileTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeUNKNOWN; // TODO SHOULD LOAD FROM OPTIONS
            //OutputFile.FileSummary.FileSubType = "Unknown FileSubType99";
            //OutputFile.FileSummary.FileSubTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeUNKNOWN;
            //#endregion
            #endregion
        } // End of Constructor - InputItemClassFields Passed

        public override String ToString() {
            if (InputFile != null && OutputFile != null) {
                if (InputFile.FileSummary.FileName != null && OutputFile.FileSummary.FileName != null) {
                    String sTemp = "File Summary: " + InputFile.FileSummary.FileName + " and " + OutputFile.FileSummary.FileName;
                    return sTemp;
                } else { return base.ToString(); }
            } else { return base.ToString(); }
            return "";
        }
    }
    #endregion

    #endregion
}
