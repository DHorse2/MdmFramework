using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
//
//@@@CODE@@@using Mdm.Oss.ClipUtil;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Decl;
//@@@CODE@@@using Mdm.Oss.Mapp;
using Mdm.Oss.Mobj;
//@@@CODE@@@using Mdm.Oss.Support;
using Mdm.Oss.Threading;
using Mdm.Pick;
using Mdm.World;

/// <summary> 
/// The File Utility namespace includes
/// this general file system object, enumerations, 
/// management classes.
/// It can include derived file type class extensions
/// but will probably switched over to generics
/// depending on design review.  Class currently
/// has a Primary and Auxillary File Stream Object
/// but this is arbitrary to a degree and could be
/// a collection.
/// </summary> 
/// <remarks></remarks> 
namespace Mdm.Oss.FileUtil {

    /// <summary> 
    /// <para> Mdm File Application Object.</para>
    /// <para> A Utility Object (File Console level)</para>
    /// <para> .</para>
    /// <para> Notes:</para>
    /// <para> Text File is a regular text file.</para>
    /// <para> Ascii ItemData File is a Simple Text File.</para>
    /// <para> Sql File is the ItemData Table in the RDBMS.</para>
    /// <para> Sql Dict is the Schema or Dictionary File for the Sql File.</para>
    /// <para> DatabaseFile is the Database File that contains the Sql File.</para>
    /// <para> Conn is the Connection opened to access the Sql File.</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> File Base Class Definition</para>
    /// <para> .</para>
    /// <para> File</para>
    /// <para> ..Ascii</para>
    /// <para> ..Text</para>
    /// <para> ..Binary</para>
    /// <para> Database File</para>
    /// <para> ..Sql</para>
    /// <para> ....MS Sql</para>
    /// <para> ....MY Sql</para>
    /// <para> ..Db2</para>
    /// <para> ..Pick</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> File Type Area:</para>
    ///	<para> Sql, Ascii, Text, Binary, Pick</para>
    /// <para> .</para>
    /// <para> File class usage:</para>
    /// <para> base, abstract, sealed, interface... ???</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> File Type Organization:</para>
    /// <para> Binary File</para>
    /// <para> Ascii File</para>
    /// <para> Ascii Delimited RowPerLine - DEL</para>
    /// <para> Ascii Delimited CellPerLine - DEL</para>
    /// <para> Text File</para>
    /// <para> Text Delimited File</para>
    /// <para> Text Delimited RowPerLine</para>
    /// <para> Text Delimited RowPerLine - CSV</para>
    /// <para> Text Delimited RowPerLine - FIX</para>
    /// <para> Text Delimited CellPerLine</para>
    /// <para> Text Delimited CellPerLine - Tilde</para>
    /// <para> Sql File</para>
    /// <para> Pick File</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> File XXXX is virtual and will be overriden in the subclasses when implemented</para>
    /// <para> Therefore SqlOpen, AsciiFileOpen and TextFileOpen become FileOpen in the SqlFile, AsciiFile, TextFile classes</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> File Management Objects:</para>
    /// <para> DbMaster - Database Master Files</para>
    /// <para> Db - Database Tables being accessed</para>
    /// <para> File - Primary File Stream</para>
    /// <para> Item - A record item or complete data block</para>
    /// <para> Buf - A file stream, ring buffer, pipe, etc.</para>
    /// <para> .</para>
    /// <para> Schema Management Objects:</para>
    /// <para> DbDict - Schema for file</para>
    /// <para> PickDict - Pick style dictionary schema.</para>
    /// <para> Dict - Flat file schema (Tld, CSV)</para>
    /// <para> XmlDict - Schema for XML</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> A File System Object contains two File Stream Objects:</para>
    /// <para> 1) Fmain, the Main or Primary File Stream Object. </para>
    /// <para> ...and...</para>
    /// <para> 1) Faux, the Auxillary File Stream Object. </para>
    /// <para> .</para>
    /// <para> Each File Stream Object is composed of:</para>
    /// <para> 2) A File Summary object. </para>
    /// <para> For each of the File and DB sub-objects:</para>
    /// <para> 3) An ID identification object. </para>
    /// <para> 4) An IO object to move data to and from the file. </para>
    /// <para> 5) One or more File Status Objects. </para>
    /// <para> 6) One or more Row and Column Management objects. </para>
    /// <para> At the Stream or System level:</para>
    /// <para> 7) Additional objects created by extended classes. </para>
    /// <para> 8) Meta data is also present for the File. 
    /// Internal Data is present in the form of run control,
    /// exceptions handling, threading, messaging, etc. </para>
    /// <para> .</para>
    /// <para> In general, a File Stream Object should either use
    /// the File sub-object for various file actions or it should
    /// use the DB sub-object for database IO.</para>
    /// <para> However, File and DB sub-objects can be reused or 
    /// used concurrently keeping in mind that they share
    /// a common set of fields and belong to one File Stream Object.</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> The Core (or primary) set of objects and lists is
    /// ordered by:</para>
    /// <para> 1) System, </para>
    /// <para> 2) Service, </para>
    /// <para> 3) Server, </para>
    /// <para> 4) Database, </para>
    /// <para> 5) FileOwner, </para>
    /// <para> 6) FileGroup, </para>
    /// <para> 7) Table and DiskFile.</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> Types of Run Control Object:</para>
    /// <para> File Action - Contains the file action verb and a few details</para>
    /// <para> File Transformation - Contains an input, output and direction</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> Types of File Object classes:</para>
    /// <para> General design pattern.  Each type of file object has:</para>
    /// <para> Summary - Contains database, user, security, system and other database and additional info</para>
    /// <para> Id - Contains the name of the file, path, Id and basic information</para>
    /// <para> Io - Contains I/O Objects such as streams, readers, buffers</para>
    /// <para> Status - Status setting related to a file</para>
    /// <para> Options - User option flags for a faile</para>
    /// <para> </para>
    /// <para> Row / Item / Record Level Objects:</para>
    /// <para> ItemDef - Contains an Item block, Id, version and a few flags</para>
    /// <para> RowInfo - Counters, indexing and status for a row</para>
    /// </summary> 
    /// <remarks>
    /// <para> PROCEED TO SqlDictProcessDb(String PassedFileName </para> 
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> Note on passing by reference:</para>
    /// <para> All Methods [Object]Check[State] take Passed Names and Options, States and Actions.</para>
    /// <para> </para>
    /// <para> They do NOT set THIS fields, Options, States or Actions to the passed values.  These methods are independant of THIS and be able to act on any Passed Name.</para>
    /// <para> .</para>
    /// <para> The rationelle for this is that each Method might need to Check various States of different Files, perhaps even on different Servers, without Altering the State of This Object or in particular,</para>
    /// <para> The Options and Actions such as:</para>
    /// <para> Open, Delete, Connect, Count,  CheckExists, SchemaChange, RowAdd, RowColumnUpdate, etc.</para>
    /// <para> </para>
    /// <para> The States or Options and Actions on This Object include flags such as DoKeepOpen which indicates that the Using Class wants This Object to ultimately be returned in an Open State.</para>
    /// <para> .</para>
    /// </remarks> 
    public enum _a_File_Application_Object_Mfile_ReadMe : long { ThisIsNotUsed = 0xF }

    /// <summary>
    /// <para> A Utility Object (File Console level)</para>
    /// <para> The console implements:</para>
    /// <para> ....Trace Mdm,</para>
    /// <para> ....print output,</para>
    /// <para> ....Console Output,</para>
    /// <para> ....Run Action and Control</para>
    /// <para> ....Mdm Message Handling</para>
    /// <para> . </para>
    /// <para> DefStdBaseRunFile is the lowest tier of the file system.</para>
    /// <para> DefStdBaseRunFileConsole would similarly represent the first
    /// tier performing User Interface and OS functionality.</para>
    /// <para> . </para>
    /// <para> The console also enables communication between classes, control
    /// over execution, and interoperation betweens threads and modules.</para>
    /// </summary>
    public class Mfile : DefStdBaseRunFileConsole {
        // CONSTANTS SECTION
        // Constants - xxxxxxxxxxxxxxxxxxxxxxxxxx
        // CORE FIELDS SECTION
        // BASE CLASS - FILE
        //
        #region $include Mdm.Oss.FileUtil Mfile Class Meta Data
        //
        public Mobject XUomMovvXv;
        //@@@CODE@@@public Mapplication XUomMavvXv;
        public Mfile FileObject;
        // Class Method Result Returns - xxxxxx
        #region $include Mdm.Oss.FileUtil Mfile Method_ReturnResults
        public long AsciiFileClearResult;
        public long AsciiFileCreateResult;
        public long AsciiFileCreatePassedNameResult;
        public long AsciiFileDeleteResult;
        public long AsciiFileDeletePassedNameResult;
        public long AsciiFileCloseResult;
        public long AsciiFileResetResult;
        public long AsciiFileFileStreamReaderCheckResult;
        public long AsciiFileReadAllResult;
        public long AsciiFileReadBlockResult;
        public long AsciiFileReadLineResult;
        public long AsciiFileReadBlockSeekResult;
        public long AsciiFileWriteResult;
        public long AsciiFileWritePassedNameResult;
        //
        public long MainFileProcessingResult;
        //
        public long ConnCheckDoesExistResult;
        public long ConnCloseResult;
        public long ConnCmdBuildResult;
        public long ConnCreatePassedConnResult;
        public long ConnCreateResult;
        public long ConnCreateBuildResult;
        public long ConnCreateCmdBuildResult;
        public long ConnOpenPassedNameResult;
        public long ConnOpenResult;
        public long ConnResetResult;
        //
        public long DatabaseFileCloseErrorResult;
        public long DatabaseCreateCmdBuildResult;
        public long ConnectionCreateResult;
        public long DatabaseFileCreationErrorResult;
        public long ExceptionDatabaseFileGeneralResult;
        public long DatabaseFileNameLongCreateBuildResult;
        public long DatabaseFieldsGetDefaultsResult;
        public long DatabaseFileOpenErrorResult;
        public long DatabaseResetResult;
        //
        public long MfileResult;
        public long IoOpDoCallbackResult;
        //
        public long SqlColActionResult;
        //
        public long SqlCommandDoResult;
        public long SqlCommandSetDefaultResult;
        //
        public long TableOpenResult;
        public long TableCreateDoResult;
        public long TableCheckDoesExistResult;
        public long TableTestAccessResult;
        public long TableCloseResult;
        public long TableCreateResult;
        //
        public long SqlDataAddResult;
        public long SqlDataDeleteResult;
        public long SqlDataInsertResult;
        public long SqlDataUpdateResult;
        public long SqlDataWriteResult;
        public long SqlDataGetResult;
        //
        public long SqlColAddCmdBuildAllFromArrayResult;
        public long SqlColAddCmdBuildAddFromArrayResult;
        public long SqlColAddCmdBuildViewFromArrayResult;
        public long SqlColAddCmdBuildResult;
        public long SqlColAddCmdBuildAllResult;
        public long SqlColConvertCharactersResult;

        public long SqlDictInsertResult;
        public long SqlDictInsertBuildResult;
        public long SqlDictInsertDoResult;
        public long SqlColClearResult;
        //
        public long SqlDictProcessDbResult;
        public long SqlResetResult;
        //
        public long DatabaseFileLongResult;
        public long TableNameLineBuildResult;
        //
        public long TextFileCloseResult;
        public long TextFileCreateResult;
        public long TextFileDeleteResult;
        public long TextFileDoesExistResult;
        public long TextFileOpenResult;
        public long TextFileProcessMainResult;
        public long TextFileResetResult;
        public long TextFileWriteResult;
        //
        public long SetAppResult;
        //
        //public long FileDoResult;
        public long FileDoOpenResult;
        public long FileDoCreateResult;
        public long FileDoDeleteResult;
        public long FileDoClearResult;
        public long FileDoCloseResult;
        public long FileDoCheckResult;
        public long FileDoGetResult;
        // Delegates;
        public int CharMaxIndexGet() { return Fmain.Buf.CharMaxIndex; }
        public int AttrIndexGet() { return Fmain.Buf.AttrIndex; }
        // Buf.CharMaxIndex, Buf.AttrIndex
        #region Method Results
        // Buf.
        public long FileCloseResult;
        public long FileCloseHandleResult;
        public long FileCloseMfile1Result;
        public long FileOpenResult;
        public long FileOpenHandleResult;
        public long FileReadAllResult;
        public long FileReadLineResult;
        public long FileSeekResult;
        public long FileWriteResult;
        #endregion
        #endregion
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile Operation Meta Data
        /// <summary> 
        /// File System Object Meta Data, System Object,
        /// and working fields.
        /// </summary> 
        public MetaDef Meta;
        public SysDef Sys;
        public DbFileTempDef DbFileTemp;
        // File Transformation
        // pointer NOT INCLUDED with the Mfile class temporarily...
        // public Object FileTransformControl;
        #region $include Mdm.Oss.FileUtil Mfile FileParent & Consolodation
        // <Area Id = "SourceParentName">
        // public String spParentName;
        public String sParentName { get; protected internal set; }
        // public String spParentNameAlias;
        public String sParentNameAlias { get; protected internal set; }
        // public long ipParentId;
        public long iParentId { get; protected internal set; }
        // public String spParentShortName;
        public String sParentShortName { get; protected internal set; }

        // <Area Id = "SourceConsolodationParentName">
        // public String spConsolodationParentName;
        public String sConsolodationParentName { get; protected internal set; }
        // public String spConsolodationParentNameAlias;
        public String sConsolodationParentNameAlias { get; protected internal set; }
        // public long iConsolodationParentId;
        public long iConsolodationParentId { get; protected internal set; }
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
        #region $include Mdm.Oss.FileUtil Mfile FileBase_FileObjects
        // File Transform Control Class
        //#region FileTransformControlPickDef
        //public FileTransformControlPickDef FileTransformControl;
        //public FileSummaryDef Fs = new FileSummaryDef();
        //public long AppIoObjectSet(FileTransformControlPickDef PassedFileTransformControl) {
        //    iAppIoObjectSet = (long)StateIs.Started;
        //    FileTransformControl = PassedFileTransformControl;
        //    iAppIoObjectSet = (long)StateIs.Successful;
        //    return iAppIoObjectSet;
        //}
        //#endregion

        // Core Objects - Mapplication
        #region $include Mdm.Oss.FileUtil Mfile FileBasicInformation
        #region $include Mdm.Oss.FileUtil Mfile FileBase_Classes Primary & Auxillary
        // Core
        /// <summary> 
        /// File Main Primary object and File Auxillary object.
        /// </summary> 
        public FileMainDef Fmain;
        public FileMainDef Faux;
        #endregion
        // Command and Phrase Construction
        public DbSynDef DbSyn;
        // Database Master and Master Phrases
        public DbMasterSynDef DbMasterSyn;
        #endregion
        //
        // Database Error Handling - General
        #region $include Mdm.Oss.FileUtil Mfile EXCEPTIONS SECTION
        #region $include Mdm.Oss.FileUtil Mfile Exception - Database - Command - Os
        // Exception
        // System Exception
        public Exception DbFileCmdOsException; // General Os
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile Exception - Database
        // <Area Id = "FileDatabaseStatus">

        #endregion
        #region $include Mdm.Oss.FileUtil Mfile PathExceptions
        /// <remarks>
        /// PATH ARGUMENT EXCEPTIONS
        /// <Area Id = "ArgumentException:
        ///  path is a zero-length string, contains only white space, or 
        ///  contains one or more invalid characters as defined by InvalidPathChars.  ">
        /// 
        /// <Area Id = "ArgumentNullException:
        ///  path is null reference
        ///  path is null or Nothing or null ptra null 
        ///  (Nothing in Visual Basic). ">
        /// 
        /// PATH LENGTH EXCEPTIONS
        /// <Area Id = "PathTooLongException:
        ///  The specified path, file name, or both 
        ///  exceed the system-defined maximum length. 
        ///  For example, on Windows-based platforms, 
        ///  paths must be less than 248 characters, 
        ///  and file names must be less than 260 characters. ">
        /// 
        /// PATH DIRECTORY TOO LONG
        /// <Area Id = "DirectoryNotFoundException:
        ///  The specified path is invalid, 
        ///  (for example, it is on an unmapped drive). ">
        /// 
        /// ACCESS PERMISSION EXCEPTIONS
        /// <Area Id = "UnauthorizedAccessException:
        ///  path specified a directory. 
        ///  -or- 
        ///  The caller does not have the required permission. ">
        /// 
        /// FILE MISSING EXCEPTIONS
        /// <Area Id = "FileNotFoundException:
        ///  The file specified in path was not found. ">
        /// 
        /// PATH FORMAT UNSUPPORTED
        /// <Area Id = "NotSupportedException:
        ///  path is in an invalid format. ">
        /// </remarks> 
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile GeneralException
        public Exception ExceptionGeneral; // General Exception
        public IOException ExceptionIO; // Input Output Exception
        public NotSupportedException ExceptionNotSupported; // Not Supported Exception
        #endregion
        #endregion
        /// <remarks>
        //
        // File Type Area - Sql, Ascii, Text, Binary, Pick
        // base, abstract, sealed, interface... ???
        // Binary File - xxxxxxxxxxxxxxxxxxxxxxxx
        // Ascii File - xxxxxxxxxxxxxxxxxxxxxxxxx
        // Ascii Delimited RowPerLine - DEL xxxxx
        // Ascii Delimited CellPerLine - DEL xxxx
        // Text File - xxxxxxxxxxxxxxxxxxxxxxxxxx        
        // Text Delimited File - xxxxxxxxxxxxxxxx
        // Text Delimited RowPerLine - xxxxxxxxxx
        // Text Delimited RowPerLine - CSV xxxxxx
        // Text Delimited RowPerLine - FIX xxxxxx
        // Text Delimited CellPerLine - xxxxxxxxx
        // Text Delimited CellPerLine - Tilde xxx
        // Sql File - xxxxxxxxxxxxxxxxxxxxxxxxxxx
        // Pick File - xxxxxxxxxxxxxxxxxxxxxxxxxx
        /// </remarks>
        //
        // Sql - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile Subclass Sql
        // SqlClient - Exceptions 
        #region $include Mdm.Oss.FileUtil Mfile SqlExceptions
        #region $include Mdm.Oss.FileUtil Mfile Exception - Database - Sql
        // System ItemData SqlClient
        public SqlException ExceptSql; // General Database
        public SqlException ExceptDbFileSql; // Sql Database
        #endregion
        // Sql Error Types - DataException - xxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile DataException
        // System ItemData
        public DBConcurrencyException x11 = null;
        // System ItemData
        public DataException x11b = null;
        // System ItemData
        public ConstraintException x13 = null;
        #endregion
        // SqlClient - SqlException - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile DataException Additional Copy
        // System ItemData SqlClient - SQL EXCEPTION
        public SqlException x7 = null;
        #endregion
        #region $include Mdm.Oss.FileUtil Mfile NotSupportedException
        public NotSupportedException ExceptNotSupported; // Delegate
        // public NotSupportedExceptionController oeMexceptNotSupportedExceptionController; // Delegate
        // public NotSupportedExceptionArgs oeMexceptNotSupportedExceptionArgs; // Delegate
        #endregion
        // SqlClient - SqlErrorCollection - xxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile SqlError
        // System ItemData SqlClient - SqlError
        public SqlErrorCollection x8C = null;
        public SqlError x8 = null;
        #endregion
        #endregion
        // SqlClient - Other Objects - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile SqlObjectTypes
        #region $include Mdm.Oss.FileUtil Mfile SqlRowUpdatingEventHandler
        public SqlRowUpdatingEventHandler x1 = null;
        public SqlRowUpdatingEventArgs x1A = null;
        public SqlRowUpdatedEventHandler x2 = null;
        public SqlRowUpdatedEventArgs x2A = null;
        public SqlRowsCopiedEventHandler x3 = null;
        public SqlRowsCopiedEventArgs x3A = null;
        #endregion
        // TODO z$RelVs4 Devrive SQL (My, Ms,...), ASCII, PICK, DB2, OS2 classes from base file class (Pick Text done)
        // SqlClient - SqlParameter - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile SqlParameter
        // System ItemData SqlClient - PARAMETER
        public SqlParameterCollection x4C = null;
        public SqlParameter x4 = null;
        #endregion
        // SqlClient - SqlNotificationType - xxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile SqlNotification
        // System ItemData SqlClient - NOTIFICATION
        public SqlNotificationType x5t;
        public SqlNotificationSource x5s;
        public SqlNotificationInfo x5i;
        public SqlNotificationEventArgs x5e;
        #endregion
        // SqlClient - SqlParameter - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile SqlInfoMessage
        // System ItemData SqlClient - INFORMATION MESSAGE
        public SqlInfoMessageEventHandler x6 = null; // Delegate
        public SqlInfoMessageEventArgs x6A = null; // Delegate Arguments
        #endregion
        // SqlClient - SqlDbType - xxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile SqlDbType
        // System ItemData - SqlDbType
        public SqlDbType x9; // Sql ItemData Type
        #endregion
        // SqlClient - StateChangeEvent - xxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile SqlDataState ChangeEvents
        // System ItemData SqlClient - StateChangeEvent
        public new StateChangeEventHandler SqlDbConnectStateChangedEvent = null;  // Delegate
        public StateChangeEventArgs SqlDbConnectStateChangedEventArgs = null;  // Delegate Arguments
        #endregion
        // SqlClient - StatementCompleted - xxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile StatementCompleted
        // System ItemData SqlClient - StatementCompleted
        public StatementCompletedEventHandler SqlDbCommandCompletedEvent = null;  // Delegate
        public StatementCompletedEventArgs SqlDbCommandCompletedEventArgs = null;  // Delegate Arguments
        #endregion
        #endregion
        #endregion
        //
        // FileSystem Object FileTypes
        // Ascii - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile Subclass Ascii (Empty)

        #endregion
        // Text - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile Subclass Text (Empty)

        #endregion
        // Binary - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile Subclass Binary (Empty)

        #endregion
        //
        // PostRelational Object
        // Pick - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile Subclass Pick
        #region Pick Dict Items
        public PickDictIndexDef PickDictIndex;
        public PickDictItemDef PickDictItem;
        #endregion
        #region Pick Items
        public PickRowDef PickRow;
        public ColPickDef ColPick;
        #endregion
        #endregion
        //
        #endregion
        // $Section oAssembly oClass Main_FileProcessing // xxxxxxxxxx
        // $Section oAssembly oClass Main_FileProcessing // xxxxxxxxxx
        // $Section oAssembly oClass Main_FileProcessing // xxxxxxxxxx
        #region $include Mdm.Oss.FileUtil Mfile Main_FileProcessing
        // <Section Id = "Constructor">
        /// <summary>
        /// Creates a file object associated with the passed Mobject.
        /// This effectively gives ownership of the file to PassedOb.
        /// </summary> 
        /// <param name="PassedOb">Parent or owner Mobject</param> 
        /// <param name="ClassUses.RoleAsUtility">File (Objects) are utility objects</param> 
        public Mfile(ref Mobject PassedOb)
            : base((long)ClassUses.RoleAsUtility) {
            MfileResult = (long)StateIs.Started;
            // MfileResult
            XUomMovvXv = PassedOb;
            MfileInitialize((int)FileAction_DirectionIs.None);
        }

        /// <summary>
        /// Creates a file for "Direction" (i.e. Input, Output, Both)
        /// whose object is associated with the passed Mobject.
        /// This effectively gives ownership of the file to PassedOb.
        /// </summary> 
        /// <param name="PassedOb">Parent or owner Mobject</param> 
        /// <param name="DirectionPassed">Direction of File IO for this file.</param> 
        /// <param name="ClassUses.RoleAsUtility">File (Objects) are utility objects</param> 
        public Mfile(ref Mobject PassedOb, int DirectionPassed)
            : base((long)ClassUses.RoleAsUtility) {
            MfileResult = (long)StateIs.Started;
            // MfileResult
            XUomMovvXv = PassedOb;
            MfileInitialize(DirectionPassed);
        }
        /*
        public Mfile(Object PassedFileTransformControl) : this() {
            // MfileResult = (long)StateIs.Started;
            FileTransformControl = PassedFileTransformControl;
        }
        */
        /// <summary> 
        /// Default constructor would essentially own itself and
        /// operate as a controller.
        /// </summary> 
        /// <param name="ClassUses.RoleAsUtility">File (Objects) are utility objects</param> 
        public Mfile()
            : base((long)ClassUses.RoleAsUtility) {
            MfileResult = (long)StateIs.Started;
            // MfileResult
            Sender = this;
            FileObject = this;
            MfileInitialize((int)FileAction_DirectionIs.None);
        }

        /// <summary> 
        /// Standard initialize after constructors.
        /// Create Meta and System data, initializes fields.
        /// Create Primary and Auxillary File Stream objects.
        /// Create Utility, management and state objects.
        /// Create Script Syntax objects.
        /// </summary> 
        /// <param name="DirectionPassed">Expected direction of IO data flow (Input, Output, Both).</param> 
        public void MfileInitialize(int DirectionPassed) {
            #region Initialize
            Meta = new MetaDef();
            Sys = new SysDef();
            LocalMessage = new LocalMessageDef();
            //
            DbFileTemp = new DbFileTempDef();
            //
            FileObject = this;
            if (XUomMovvXv == null) {
                XUomMovvXv = new Mobject((long)ClassUses.RoleAsUtility); // Mapplication();
            }
            // Location Counter / Values
            XUomMovvXv.TraceMdmCounterLevel1Get = CharMaxIndexGet;
            XUomMovvXv.TraceMdmCounterLevel2Get = AttrIndexGet;
            // Delegates;
            //StatusUi.WasCreatedBy = "Mfile";
            //StatusUi.BoxDelegatesCopyFrom(ref XUomMovvXv.StatusLine);
            //StatusUi.BoxObjectsCopyFrom(ref XUomMovvXv.StatusLine);
            //StatusUi.WasCreatedBy += " and copy from XUomMovvXv";
            //StatusUi.TextConsoleBox.Text = "Hey Dave";
            // Infinitie Loop control
            DoingDefaults = false;
            ObjectListLoading = false;
            // Primary and Auxillary File Objects
            Fmain = new FileMainDef(FileObject, DirectionPassed, "Primary");
            Faux = new FileMainDef(FileObject, DirectionPassed, "Auxillary");
            //
            #region Phrase Construction
            DbSyn = new DbSynDef();
            DbMasterSyn = new DbMasterSynDef();
            #endregion
            #region Pick Specific Object
            PickDictIndex = new PickDictIndexDef();
            PickRow = new PickRowDef();
            PickDictItem = new PickDictItemDef();
            ColPick = new ColPickDef();
            #endregion
            //
            // Initialize Fields
            //
            IterationFirst = bYES;
            MethodIterationFirst = bYES;
            Fmain.ColTrans.sGetResultNotSupported = "";
            Faux.ColTrans.sGetResultNotSupported = "";

            Fmain.FileStatus.bpIsInitialized = false;
            Fmain.FileStatus.bpIsInitialized = false;
            PickRow.PdIndexAliasLow = PickRowDef.PdIndexMaxNew;
            PickRow.PdIndex = 0;
            TableNameClear(ref Fmain);
            FileNameClear(ref Fmain);
            ItemIdClear(ref Fmain);
            MfileResult = SqlReset(ref Fmain);
            //
            Fmain.FileStatus.bpIsInitialized = false;
            MfileResult = DatabaseReset(ref Fmain);
            //
            Fmain.DbStatus.bpIsInitialized = false;
            MfileResult = ConnReset(ref Fmain);
            //
            Fmain.DbMaster.MstrDbDatabaseIsInitialized = false;
            MfileResult = DatabaseReset(ref Fmain);

            Fmain.ColIndex.CharsPassedIn = new char[40];
            Fmain.ColIndex.CharsPassedIn = ("/,:*#?\"<>|.,\\';|][{}=+-()*&^%#@!`~ };_").ToCharArray();
            // Fmain.ColIndex.CharsPassedIn = "/,:*#?\"<>|.,\\';|][{}=+-()*&^%#@!`~ };_";

            Fmain.ColIndex.CharsPassedOut = new char[40];
            Fmain.ColIndex.CharsPassedOut = ("________________________________________").ToCharArray();
            // Fmain.ColIndex.CharsPassedIn = "________________________________________";
            #endregion
            #region FileInputBuffers Set
            Fmain.Buf.LineBuffer = "";
            Fmain.Buf.NewItem = "";
            Fmain.Item.ItemData = "";
            Fmain.Buf.CharIndex = 1;
            Fmain.Buf.CharMaxIndex = 0;
            Fmain.Buf.CharCounter = 0;
            // String[] sNewItem = "";     //  sNewItem=@""
            // String[] InFile.Fmain.Item.ItemData = "";      
            //  InFile.Fmain.Item.ItemData=@""
            // File Bulk Character Conversion (Function)
            Fmain.Buf.ConvertableItem = "";
            Fmain.Buf.ItemConvertFlag = 0; // TODO z$RelVs3 Refine these Import options MfileInitialize
            #endregion
            ConsoleMdmInitialize();
        }
        //public override void ConsoleMdmInitialize() {
        //    base.ConsoleMdmInitialize();
        //    XUomMovvXv.ConsoleMdmInitializeToController(ref Sender);
        //    XUomMovvXv.ConsoleMdmInitializeToController(ref Sender);
        //}

        #region AppObjects
        /// <summary> 
        /// Not Used - Get the file object (this)
        /// </summary> 
        public long AppMfileObjectGet(ref FileMainDef FmainPassed) {
            iAppMfileObjectGet = (long)StateIs.Started;
            FmainPassed.FileObject = this;
            FmainPassed.Fs.FileObject = this;
            iAppMfileObjectGet = (long)StateIs.Successful;
            return iAppMfileObjectGet;
        }
        /// <summary> 
        /// Not Used - Set the Primary File Stream object Fmain (this)
        /// </summary> 
        public long AppMfileObjectSet(ref FileMainDef FmainPassed) {
            iAppMfileObjectSet = (long)StateIs.Started;
            // This is not a valid action on
            FileObject = this;
            Fmain = FmainPassed;
            iAppMfileObjectSet = (long)StateIs.Successful;
            return iAppMfileObjectSet;
        }
        #endregion
        // <Section Id = "MainFileProcessing">
        /// <summary> 
        /// Main Processing Loop method.
        /// Virtual method overriden in derived class
        /// but could be delegate based.
        /// It would use a standard Action delegate.
        /// </summary> 
        public virtual long MainFileProcessing() {
            MainFileProcessingResult = (long)StateIs.Started;
            //
            MainFileProcessingResult = (long)StateIs.Successful;
            return MainFileProcessingResult;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx //
        // Table xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Table
        #region Table Create
        /// <summary> 
        /// 
        /// </summary> 
        public long TableCreateDo(ref FileMainDef FmainPassed) {
            TableCreateDoResult = (long)StateIs.Started;
            FmainPassed.FileStatus.bpIsCreating = true;
            // this code is for database create:
            //if (DbMasterSyn.MstrDbDatabaseCreateCmd == null) {
            //    TableCreateDoResult = DatabaseCreateCmdBuild(); // XXXXXXXXXXXXXXXXXXXXX
            //}
            // Create Table
            TableCreateDoResult = (long)StateIs.InProgress;
            // Connect to database
            try {
                // <Area Id = "General System Errors
                Sys.sMformStatusMessage = "";
                // Not IsConnected
                if (!FmainPassed.DbStatus.bpIsConnected) { ConnOpen(ref FmainPassed); }
            } catch (SqlException ExceptionSql) {
                LocalMessage.ErrorMsg = "";
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, TableCreateDoResult);
                //
                TableCreateDoResult = (long)StateIs.Failed;
                ExceptTableCreationImpl(ref FmainPassed, ref ExceptionSql);
                // <Area Id = "Exit Here
            } catch (Exception ExceptionGeneral) {
                LocalMessage.ErrorMsg = "";
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, ConnCloseResult);
                TableCreateDoResult = (long)StateIs.Failed;
                ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                // <Area Id = "*** App.Job.Exit(); *** TODO
            } finally { }
            //
            // Create table
            TableCreateDoResult = (long)StateIs.InProgress;
            try {
                DbMasterSyn.MstrDbDatabaseCreateCmd = "CREATE-FILE " + FmainPassed.Fs.spTableName;
                FmainPassed.DbIo.SqlDbCommand = new SqlCommand(DbMasterSyn.MstrDbDatabaseCreateCmd, FmainPassed.DbIo.SqlDbConn);
                // FmainPassed.DbIo.SqlDbCommand = new SqlCommand(MstrDbDatabaseCreateCmd, FmainPassed.DbIo.SqlDbConn);
                // ??? reader type / mode ???? (ie scalar)
                //=================
                TableCreateDoResult = SqlCommandDo(ref FmainPassed, DbMasterSyn.MstrDbDatabaseCreateCmd);
                //=================
                if (StateIsSuccessfulAll(TableCreateDoResult)) {
                    FmainPassed.FileStatus.bpIsCreated = true;
                }
                // sMformStatusMessage.Close();
                Sys.sMformStatusMessage = "";
                //=================
                LocalMessage.ErrorMsg = MdmProcessTitle + @" Database Table Creation Status" + "\n" + "Database " + DbMasterSyn.spMstrDbPhraseDatabase + @" successfully created!";
                XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + LocalMessage.ErrorMsg);
                //== <Area Id = "Catch Try
                FmainPassed.FileStatus.bpIsCreating = false;
            } catch (SqlException ExceptionSql) {
                LocalMessage.ErrorMsg = "";
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, TableCreateDoResult);
                TableCreateDoResult = (long)StateIs.Failed;
                ConnCloseResult = (long)StateIs.DatabaseError;
                ExceptTableCreationImpl(ref FmainPassed, ref ExceptionSql);
                LocalMessage.ErrorMsg = MdmProcessTitle + " SQL Exception Error!";
                LocalMessage.ErrorMsg = ExceptionSql.Message;
                ExceptTableCreationImpl(ref FmainPassed, ref ExceptionSql);
                FmainPassed.FileStatus.bpIsCreated = false;
            } catch (Exception ExceptionGeneral) {
                LocalMessage.ErrorMsg = "";
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, ConnCloseResult);
                TableCreateDoResult = (long)StateIs.Failed;
                //
                ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                FmainPassed.FileStatus.bpIsCreated = false;
            }
            // TODO z$RelVs? Database File Creation
            if (FmainPassed.FileStatus.bpIsCreated) {
                TableCreateDoResult = (long)StateIs.Successful;
            }
            if (FmainPassed.FileStatus.bpIsCreating) {
                TableCreateDoResult = (long)StateIs.AbnormalEnd;
                FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.NotCompleted;
            }
            if (!FmainPassed.FileStatus.bpIsValid) {
                TableCreateDoResult = (long)StateIs.AbnormalEnd;
                FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.NotCompleted;
            }
            return TableCreateDoResult;
        }
        #endregion
        #region Table Open
        // <Section Id = "TableOpen">
        /// <summary> 
        /// Open the current table.
        /// </summary> 
        public long TableOpen() {
            TableOpenResult = (long)StateIs.Started;
            TableOpenResult = TableOpen(ref Fmain);
            return TableOpenResult;
        }
        // <Section Id = "x
        /// <summary> 
        /// Open the passed table.
        /// </summary> 
        public long TableOpen(ref FileMainDef FmainPassed) {
            TableOpenResult = (long)StateIs.Started;
            #region Initialize File Information
            int iOptionsResult = 0;
            //
            TableOpenResult = (long)StateIs.InProgress;
            // TableOpenResult = (long)StateIs.Undefined;
            //
            FmainPassed.Buf.ByteCountClear();
            // TODO Why is this here??? Move it??? Analysis required...
            PickRow.DataClear();
            PickRow.RowDataClear(PickRow.PdIndex);
            //
            FmainPassed.DbStatus.bpIsInitialized = true;
            #endregion
            #region Check DoesExist
            switch (FmainPassed.Fs.FileIo.FileReadMode) {
                case ((long)FileIo_ModeIs.Sql):
                    // FmainPassed.DbIo.SqlDbConn.Open();
                    // FmainPassed.FileStatus.bpDoesExistResult = (int)TableCheckDoesExist(ref FmainPassed);
                    //  check Connection Name
                    if (FmainPassed.Fs.TableNameLine.Length == 0 || !FmainPassed.DbStatus.bpNameIsValid) {
                        FmainPassed.Fs.TableNameLine = TableNameLineBuild(ref FmainPassed);
                    }
                    //  check Connection
                    TableOpenResult = ConnCheck(ref FmainPassed);
                    if (StateIsSuccessfulAll(TableOpenResult)) {
                        // TableOpenResult = (long)StateIs.InProgress;
                        //
                        // TODO OPEN TABLE HERE
                        //
                        FmainPassed.FileStatus.ipDoesExistResult = TableOpenResult;
                        if (StateIsSuccessfulAll(TableOpenResult)) {
                            TableOpenResult = (long)StateIs.DoesExist;
                            FmainPassed.FileStatus.bpDoesExist = true;
                        } else {
                            TableOpenResult = (long)StateIs.DoesNotExist;
                            FmainPassed.FileStatus.bpDoesExist = false;
                        }
                    } else { return TableOpenResult; }
                    break;
                case ((long)FileIo_ModeIs.Block):
                case ((long)FileIo_ModeIs.Line):
                case ((long)FileIo_ModeIs.All):
                    FmainPassed.Fs.TableNameLine = FmainPassed.Fs.spTableName;
                    FmainPassed.FileStatus.bpDoesExist = System.IO.File.Exists(FmainPassed.Fs.TableNameLine);
                    if (FmainPassed.FileStatus.bpDoesExist) {
                        TableOpenResult = (long)StateIs.DoesExist;
                    } else {
                        TableOpenResult = (long)StateIs.DoesNotExist;
                    }
                    break;
                default:
                    TableOpenResult = (long)FileAction_Do.NotSet;
                    FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.NotSupported;
                    LocalMessage.ErrorMsg = "File Read Method (" + FmainPassed.Fs.FileIo.FileReadMode.ToString() + ") is not set";
                    throw new NotSupportedException(LocalMessage.ErrorMsg);
                // return TableOpenResult;
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
                if ((FmainPassed.FileStatus.bpDoesExist)) {
                    TableOpenResult = (long)StateIs.DoesExist;
                    #region Option: N: File must not alread exist
                    // <Area Id = "Option: File must not alread exist options here.
                    iOptionsResult = FmainPassed.Fs.FileOptionString.IndexOf("N");
                    if (iOptionsResult > 0) {
                        // <Area Id = "Option: error file already exists.
                        TableOpenResult = (long)StateIs.ShouldNotExist;
                        return TableOpenResult;
                    }
                    #endregion
                    #region Option: D: Delete the file if it exists
                    // <Area Id = "Option: Delete the file if it exists.
                    iOptionsResult = FmainPassed.Fs.FileOptionString.IndexOf("D");
                    if (iOptionsResult > 0) {
                        #region Option: Delete the file
                        switch (FmainPassed.Fs.FileIo.FileReadMode) {
                            case ((long)FileIo_ModeIs.Sql):
                                // TODO Option: Delete the file if it exists
                                // TableOpenResult = SqlFileDelete(FmainPassed.spTableName);
                                if (FmainPassed.FileStatus.bpDoesExist) {
                                    TableOpenResult = (long)StateIs.Successful;
                                } else {
                                    // TODO $ERROR Error Option: Delete the file if it exists
                                    TableOpenResult = (long)StateIs.ShouldNotExist;
                                }
                                return TableOpenResult;
                            case ((long)FileIo_ModeIs.Block):
                            case ((long)FileIo_ModeIs.Line):
                            case ((long)FileIo_ModeIs.All):
                                // TODO Option: Delete the file if it exists
                                File.Delete(FmainPassed.Fs.spTableName);
                                // TODO Option: Create the file if it exists
                                // <Area Id = "Option: Create the file depending on options here.
                                FmainPassed.Fs.FileIo.DbFileStreamObject = File.Create(FmainPassed.Fs.spTableName);
                                if (FmainPassed.Fs.FileIo.DbFileStreamObject != null) {
                                    TableOpenResult = (long)StateIs.Successful;
                                } else {
                                    TableOpenResult = (long)StateIs.ShouldNotExist;
                                }
                                break;
                            default:
                                TableOpenResult = (long)FileAction_Do.NotSet;
                                FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.NotSupported;
                                LocalMessage.ErrorMsg = "File Read Method (" + FmainPassed.Fs.FileIo.FileReadMode.ToString() + ") is not set";
                                throw new NotSupportedException(LocalMessage.ErrorMsg);
                            // return TableOpenResult;
                        }
                        #endregion
                        #region VERIFY delete
                        // <Area Id = "Open the stream and read it back.
                        switch (FmainPassed.Fs.FileIo.FileReadMode) {
                            case ((long)FileIo_ModeIs.Sql):
                                // TODO OPEN Open the stream and read it back
                                if (FmainPassed.FileStatus.bpDoesExist) {
                                    TableOpenResult = (long)StateIs.Successful;
                                } else {
                                    TableOpenResult = (long)StateIs.ShouldNotExist;
                                }
                                return TableOpenResult;
                            case ((long)FileIo_ModeIs.Block):
                            case ((long)FileIo_ModeIs.Line):
                            case ((long)FileIo_ModeIs.All):
                                // TODO OPEN Open the stream and read it back
                                FmainPassed.Fs.FileIo.DbFileStreamObject = File.OpenRead(FmainPassed.Fs.spTableName);
                                // DbFileStreamObject = System.IO.File.TextFileOpen(Fs.spTableName);
                                if (FmainPassed.Fs.FileIo.DbFileStreamObject != null) {
                                    TableOpenResult = (long)StateIs.Successful;
                                } else {
                                    TableOpenResult = (long)StateIs.ShouldNotExist;
                                }
                                break;
                            default:
                                TableOpenResult = (long)FileAction_Do.NotSet;
                                // TODO $ERROR Error Open the stream and read it back
                                LocalMessage.Msg = "File Read Method (" + FmainPassed.Fs.FileIo.FileReadMode.ToString() + ") is not set";
                                throw new NotSupportedException(LocalMessage.Msg);
                            // return TableOpenResult;
                        }
                        #endregion
                    }
                    #endregion
                    #region Option: F: The file must already exist.
                    // <Area Id = "Option: The file must already exist options here.
                    iOptionsResult = FmainPassed.Fs.FileOptionString.IndexOf("F");
                    if (iOptionsResult > 0) {
                        if (TableOpenResult == (long)StateIs.DoesExist) {
                            return TableOpenResult;
                        }
                        // TableOpenResult = (int) StateIs.DoesNotExist;
                    }
                    #endregion
                } else {
                    TableOpenResult = (long)StateIs.DoesNotExist;
                    // File does not exist
                    #region Option: ?: File Does Not Exist
                    // <Area Id = "Option: File must not exist options here.
                    iOptionsResult = FmainPassed.Fs.FileOptionString.IndexOf("?"); // File Does Not Exist
                    if (iOptionsResult > 0) {
                        // <Area Id = "Option: error file already exists.
                        TableOpenResult = (long)StateIs.Successful;
                        return TableOpenResult;
                    }
                    #endregion
                    #region Option: M: Create the missing file. N: Create New Must Not Exist File
                    // <Area Id = "Option: M: Create the missing file depending on options here.
                    iOptionsResult = FmainPassed.Fs.FileOptionString.IndexOf("M"); // Create Missing
                    if (iOptionsResult < 0) { iOptionsResult = FmainPassed.Fs.FileOptionString.IndexOf("N"); } // Create New Must Not Exist
                    if (iOptionsResult > 0) {
                        // <Area Id = "Option: Create the file depending on options here.
                        switch (FmainPassed.Fs.FileIo.FileReadMode) {
                            case ((long)FileIo_ModeIs.Sql):
                                // TODO Option: unknown for Read Mode Sql;
                                if (!FmainPassed.FileStatus.bpDoesExist) {
                                    TableOpenResult = TableCreateToDo(ref FmainPassed);
                                    if (FmainPassed.FileStatus.bpDoesExist) {
                                        TableOpenResult = (long)StateIs.DoesExist;
                                        FmainPassed.FileStatus.bpDoesExist = bYES;
                                    } else {
                                        // TODO Option: Error Create the file depending on options
                                        TableOpenResult = (long)StateIs.ShouldExist;
                                        FmainPassed.FileStatus.bpDoesExist = bNO;
                                    }
                                }
                                return TableOpenResult;
                            case ((long)FileIo_ModeIs.Block):
                            case ((long)FileIo_ModeIs.Line):
                            case ((long)FileIo_ModeIs.All):
                                TableOpenResult = TextFileCreate(ref FmainPassed);
                                if (FmainPassed.FileStatus.bpDoesExist) {
                                    TableOpenResult = (long)StateIs.DoesExist;
                                } else {
                                    // TODO Option: Error Create the file depending on options 
                                    TableOpenResult = (long)StateIs.ShouldExist;
                                }
                                break;
                            default:
                                TableOpenResult = (long)FileAction_Do.NotSet;
                                FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.NotSupported;
                                // TODO Option: Error Create the file depending on options 
                                LocalMessage.ErrorMsg = "File Read Method (" + FmainPassed.Fs.FileIo.FileReadMode.ToString() + ") is not set";
                                throw new NotSupportedException(LocalMessage.ErrorMsg);
                            // return TableOpenResult;
                        }
                    }
                    #endregion

                    // TODO $ERROR Error Option: file missing and no option to create error
                    // <Area Id = "Option: file missing and no option to create error
                    // TableOpenResult = (int) StateIs.DoesNotExist;
                }
                #region Catch Errors
            } catch (SqlException ExceptionSql) {
                LocalMessage.ErrorMsg = "";
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, TableOpenResult);
                TableOpenResult = (long)StateIs.DatabaseError;
                //
                ExceptTableOpenError(ref FmainPassed, ref ExceptionSql);
                // Exit Here
            } catch (Exception ExceptionGeneral) {
                LocalMessage.ErrorMsg = "";
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, ConnCloseResult);
                TableOpenResult = (long)StateIs.UnknownFailure;
                //
                ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                FmainPassed.DbStatus.bpIsConnecting = false;
                FmainPassed.DbStatus.bpIsConnected = false;
                FmainPassed.ConnStatus.NameIsValid = false;
                #endregion
            } finally {
                //
            }
            //
            //  TODO CREATE READER FOR OUTPUT FILE...
            //
            return TableOpenResult;
        }
        // <Section Id = "x
        /// <summary> 
        /// An SQL Exception while Opening Tables.
        /// </summary> 
        /// <param name="ExceptionSql">The SQL exception that occurred.</param> 
        /// <remarks></remarks> 
        public void ExceptTableOpenError(ref SqlException ExceptionSql) {
            ExceptTableOpenError(ref Fmain, ref ExceptionSql);
        }
        /// <summary> 
        /// An SQL Exception while Opening on the passed Table.
        /// </summary> 
        /// <param name="ExceptionSql">The SQL exception that occurred.</param> 
        /// <remarks></remarks> 
        public void ExceptTableOpenError(ref FileMainDef FmainPassed, ref SqlException ExceptionSql) {
            DatabaseFileOpenErrorResult = (long)StateIs.Started;
            FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.SqlError;
            // sMessageBoxMessage = MdmProcessTitle + "\n";
            // sMessageBoxMessage += "\n" + @"SQL Exception Error!";
            // sMessageBoxMessage += "\n" + @"File Open Error!";
            // try {
            // sMessageBoxMessage += "\n" + ExceptSql.ToString();
            // } catch (Exception ExceptionNotSupported) { ; }
            // XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(),PickSystemCallStringResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sMessageBoxMessage + "\n");

            // <Area Id = "display message
        }
        // <Section Id = "x
        /// <summary> 
        /// Obsolete Open...
        /// </summary> 
        public long TableOpenOLD(ref FileMainDef FmainPassed) {
            TableOpenResult = (long)StateIs.Started;
            // TableNameLine
            if (FmainPassed.Fs.TableNameLine.Length == 0 || !FmainPassed.DbStatus.bpNameIsValid) {
                FmainPassed.Fs.TableNameLine = TableNameLineBuild(ref FmainPassed);
            }
            // Counts
            FmainPassed.Buf.BytesRead = 0;
            FmainPassed.Buf.BytesReadTotal = 0;
            FmainPassed.Buf.BytesConverted = 0;
            FmainPassed.Buf.BytesConvertedTotal = 0;
            // Open a connection to the database
            if (!FmainPassed.DbStatus.bpIsConnected) {
                TableOpenResult = ConnOpen(ref FmainPassed);
            }
            //  check if the sql exists
            if (!FmainPassed.FileStatus.bpDoesExist || !FmainPassed.FileStatus.bpIsOpen) {
                FmainPassed.FileStatus.bpDoKeepOpen = true;
                FmainPassed.DbStatus.bpDoKeepOpen = true;
                // TODO $ERROR TableOpen Correct to leave open ???
                TableOpenResult = TableListCheck(ref FmainPassed, false, false);
                // TableOpenResult = TableCheckDoesExist(ref FmainPassed);
                FmainPassed.FileStatus.ipDoesExistResult = TableOpenResult;
                if (FmainPassed.FileStatus.ipDoesExistResult == (long)StateIs.DoesExist) {
                    FmainPassed.FileStatus.bpDoesExist = true;
                }
            } else {
                TableOpenResult = (long)StateIs.DoesExist;
            }
            // FmainPassed.FileStatus.ipStatusCurrent = TableOpenResult;
            // FmainPassed.FileStatus.bpIsOpen = true;
            // TODO $$$CHECK work on Sql File Exits (review for support files)
            if (FmainPassed.FileStatus.bpDoesExist
                && TableOpenResult == (long)StateIs.DoesExist) {
                // the file does not exist
                TableOpenResult = (long)StateIs.DoesExist;
                // SqlFileDoesExist = true;
            } else {
                // the file exists and can be changed
                FmainPassed.DbIo.SqlDbConn.Dispose();
                TableOpenResult = (long)StateIs.DoesNotExist;
                FmainPassed.FileStatus.bpDoesExist = false;
            }

            return TableOpenResult;
        }
        #endregion
        #region Table Existance Control (CheckDoesExist)
        // <<Section Id = "x
        /// <summary> 
        /// Returns a bool indicating if the Table exists.
        /// </summary> 
        public bool TableDoesExist(ref FileMainDef FmainPassed) {
            TableCheckDoesExistResult = TableCheckDoesExist(ref FmainPassed);
            if (StateIsSuccessfulAll(TableCheckDoesExistResult)) {
                return true;
            } else if (TableCheckDoesExistResult == (long)StateIs.DoesNotExist) {
                return false;
            }
            return false;
        }
        // <<Section Id = "x
        /// <summary> 
        /// Check if the Table exists.
        /// </summary> 
        public long TableCheckDoesExist() {
            TableCheckDoesExistResult = (long)StateIs.Started;
            Fmain.FileStatus.ipDoesExistResult = (long)StateIs.Successful;
            Fmain.FileStatus.bpDoesExist = false;
            //
            TableCheckDoesExistResult = TableCheckDoesExist(ref Fmain);
            return TableCheckDoesExistResult;
        }
        // <Section Id = "x">
        // TODO z$RelVs2 TableCheckDoesExist Add Sql File Check Does Exist Code
        /// <summary> 
        /// Check if the passed table exists.
        /// </summary> 
        public long TableCheckDoesExist(ref FileMainDef FmainPassed) {
            TableCheckDoesExistResult = (long)StateIs.Started;
            Fmain.FileStatus.ipDoesExistResult = (long)StateIs.Successful;
            Fmain.FileStatus.bpDoesExist = false;
            // Add code for table lookup
            TableCheckDoesExistResult = TableCheckDoesExist(ref Fmain);
            return TableCheckDoesExistResult;
        }
        // <Section Id = "x">
        // Sql FileExistance Control (CheckDoesExist) (Obsolete)
        /// <summary> 
        /// Test access to the Table.
        /// </summary> 
        public long TableTestAccess(ref FileMainDef FmainPassed) {
            TableTestAccessResult = (long)StateIs.Started;
            // TODO Phase this out of use...
            #region Initialize Flags, Status, etc.
            FmainPassed.RowInfo.UseMethod = (long)FileIo_CommandModeIs.SingleResult;
            FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.Sql;
            FmainPassed.RowInfo.CloseIsNeeded = false;
            // Row
            FmainPassed.RowInfo.HasRows = false;
            FmainPassed.RowInfo.RowContinue = false;
            FmainPassed.RowInfo.RowMax = PickRowDef.PdIndexMax;
            // Colomn
            FmainPassed.RowInfo.HasColumns = false;
            FmainPassed.RowInfo.ColumnContinue = false;
            FmainPassed.RowInfo.ColumnMax = PickRowDef.PdIndexMax;
            // Sql
            FmainPassed.RowInfo.RowIndex = 0;
            // !FmainPassed.DbStatus.bpDoKeepOpen = PassedConnDoClose;
            // FmainPassed.DbStatus.bpDoDispose = FmainPassed.DbStatus.bpDoDispose;
            // SqlFileDoClose = PassedSqlFileDoClose;
            Faux.FileStatus.ipDoesExistResult = (long)StateIs.InProgress;

            FmainPassed.DbIo.SqlDbCommandTimeout = 15;

            FmainPassed.RowInfo.UseMethod = (long)FileIo_CommandModeIs.SingleResult;
            FmainPassed.RowInfo.CloseIsNeeded = false;
            // Row
            FmainPassed.RowInfo.HasRows = false;
            FmainPassed.RowInfo.RowContinue = false;
            FmainPassed.RowInfo.RowIndex = 0;
            FmainPassed.RowInfoDb.RowCount = 0;
            // Clr Native
            FmainPassed.RowInfoDb.RowMax = PickRowDef.PdIndexMax;
            System.Object[] ThisGetValuesArray = new System.Object[(int)ArrayMax.ColumnMax];
            System.Object ThisGetValueObject;
            // Sql
            // ipRowIndex = 0;
            // System.Object[] RowArray;
            // System.Object osoThisGetSqlValue;
            // Colomn
            FmainPassed.RowInfo.HasColumns = false;
            FmainPassed.RowInfoDb.ColumnContinue = false;
            sTemp0 = "";
            TableTestAccessResult = SqlColAction(
                ref FmainPassed,
                // ref DbIo.SqlDbDataReader, ref DbIo.SqlDbDataWriter, ref RowInfoDbAux, ref ColTransAux, 
                false, ColTransformDef.SFC_RESET, sTemp0, 0, 0);
            //
            System.Type tThisTempType;

            System.Data.SqlTypes.SqlString tsdssThisTempSqlString;
            #endregion
            #region Comments
            // This is the test code for performance evaluation on each command type
            // Will require the addition of sql timer functions.
            // Normal usage should be execute schalar.

            // SqlConnection myConnection = new SqlConnection(myConnectionString);
            // SqlCommand myCommand = new SqlCommand(mySelectQuery, myConnection);
            // FmainPassed.DbIo.SqlDbCommand.ExecuteReader();
            // FmainPassed.DbIo.SqlDbCommand.ExecuteReader(new CommandBehavior()); == CommandBehavior.Default
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.KeyInfo);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.SchemaOnly);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.SequentialAccess);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.SingleResult);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.SingleRow);

            // When CommandBehavior.CloseConnection
            // Implicitly closes the connection because 
            // CommandBehavior.CloseConnection was specified.
            // otherwise:

            // Int32 count = (int32)cmd.ExecuteScalar();
            #endregion
            TableTestAccessResult = (long)StateIs.InProgress;
            FmainPassed.FileStatus.bpDoesExist = false;
            FmainPassed.RowInfoDb.RowCount = 0;
            //  check Connection Name
            if (FmainPassed.Fs.TableNameLine.Length == 0 || !FmainPassed.DbStatus.bpNameIsValid) {
                FmainPassed.Fs.TableNameLine = TableNameLineBuild(ref FmainPassed);
            }
            //  check Connection
            TableTestAccessResult = ConnCheck(ref FmainPassed);
            if (StateIsSuccessfulAll(TableTestAccessResult)) {
                TableTestAccessResult = (long)StateIs.InProgress;
                //
                if (TableTestAccessResult == (long)StateIs.Successful || TableTestAccessResult == (long)StateIs.InProgress) {
                    // UseErSingleResult
                    // UseErSchemaOnly
                    // UseErKeyInfo
                    FmainPassed.RowInfo.UseMethod = (long)FileIo_CommandModeIs.SingleRow;
                    // UseErSequentialAccess));
                    FmainPassed.RowInfo.CloseIsNeeded = false;
                    if (!FmainPassed.FileStatus.bpIsOpen) {
                        TableTestAccessResult = (long)StateIs.InProgress;
                        #region Sql Command Behavior File probing
                        try {
                            #region Sql Command Behavior Cases
                            //
                            //
                            TableTestAccessResult = DatabaseListCheck(ref FmainPassed, false, false);
                            //
                            // FmainPassed.DbIo.SqlDbCommand.ResetCommandTimeout();
                            //
                            if ((FmainPassed.RowInfo.UseMethod
                                & (long)(FileIo_CommandModeIs.SingleResult
                                    | FileIo_CommandModeIs.SchemaOnly
                                    | FileIo_CommandModeIs.KeyInfo
                                    | FileIo_CommandModeIs.SingleRow
                                    | FileIo_CommandModeIs.SequentialAccess)) != 0
                                ) {
                                if (FmainPassed.DbIo.SqlDbDataReader != null) {
                                    if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed) {
                                        FmainPassed.RowInfo.CloseIsNeeded = true;  // Only used for research Benchmark Loop
                                    }
                                }
                            }
                            #endregion

                            #region catch errors on Read Mode
                        } catch (SqlException ExceptionSql) {
                            LocalMessage.ErrorMsg = "";
                            ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, TableTestAccessResult);
                            TableTestAccessResult = (long)StateIs.DatabaseError;
                            FmainPassed.FileStatus.bpDoesExist = false;
                            FmainPassed.RowInfoDb.RowCount = 0;
                            FmainPassed.FileStatus.bpIsOpen = false;
                        } catch (Exception ExceptionGeneral) {
                            LocalMessage.ErrorMsg = "";
                            ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, TableTestAccessResult);
                            TableTestAccessResult = (long)StateIs.OsError;
                            FmainPassed.FileStatus.bpDoesExist = false;
                            FmainPassed.RowInfoDb.RowCount = 0;
                            FmainPassed.FileStatus.bpIsOpen = false;
                        } finally {
                            // FmainPassed.DbIo.SqlDbCommand = null;
                            // TableTestAccessResult = Faux.RowInfoDb.RowCount;
                        }
                        // If File is NOT Open Try to Select the File Name in the Master File
                            #endregion
                        #endregion
                        #region Handle results of probing
                        if (FmainPassed.RowInfo.HasRows) {
                            FmainPassed.FileStatus.ipDoesExistResult = (long)StateIs.DoesExist;
                            FmainPassed.FileStatus.bpDoesExist = true;
                        } else {
                            FmainPassed.FileStatus.ipDoesExistResult = (long)StateIs.DoesNotExist;
                            FmainPassed.FileStatus.bpDoesExist = false;
                            FmainPassed.FileStatus.bpIsOpen = false;
                        }
                        // 
                        #region Set Status and Dispose
                        if (FmainPassed.DbStatus.bpIsConnected && FmainPassed.DbIo.SqlDbConn != null) {
                            // <Area Id = "Close connected">
                            if (!FmainPassed.DbStatus.bpDoKeepOpen) {
                                TableTestAccessResult = ConnClose(ref FmainPassed);
                                if (FmainPassed.RowInfoDb.RowCount > 0) {
                                    TableTestAccessResult = (long)StateIs.DoesExist;
                                }
                            }
                        }
                        if (FmainPassed.DbIo.SqlDbDataReader != null) {
                            FmainPassed.DbIo.SqlDbDataReader.Dispose();
                            FmainPassed.DbIo.SqlDbDataReader = null;
                        }
                        if (FmainPassed.DbIo.SqlDbDataWriter != null) {
                            FmainPassed.DbIo.SqlDbDataWriter.Dispose();
                            FmainPassed.DbIo.SqlDbDataWriter = null;
                        }
                        if (FmainPassed.DbStatus.bpDoDispose && FmainPassed.DbIo.SqlDbConn != null) {
                            // <Area Id = "Dispose connected">
                            FmainPassed.DbIo.SqlDbConn.Dispose();
                            FmainPassed.DbIo.SqlDbConn = null;
                        }
                        #endregion
                    } else {
                        FmainPassed.FileStatus.ipDoesExistResult = (long)StateIs.Failed;
                        FmainPassed.FileStatus.bpDoesExist = false;
                        FmainPassed.FileStatus.bpIsOpen = false;
                    }
                }
                        #endregion
            }
            return TableTestAccessResult;
        }
        #endregion
        #region TableClose
        // <Section Id = "x
        public long TableClose(ref FileMainDef FmainPassed) {
            TableCloseResult = (long)StateIs.Started;
            // close reader / writer
            //
            try {
                if (FmainPassed.DbIo.SqlDbDataReader != null) {
                    if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed) { FmainPassed.DbIo.SqlDbDataReader.Close(); }
                }
            } catch { ; }
            // Close writer
            try {
                if (FmainPassed.DbIo.SqlDbDataWriter != null) { FmainPassed.DbIo.SqlDbDataWriter.Dispose(); }
            } catch { ; }
            // Close / dispose Command Adapter
            try {
                if (FmainPassed.DbIo.SqlDbAdapterObject != null) {
                    FmainPassed.DbIo.SqlDbAdapterObject.Dispose();
                    FmainPassed.DbIo.SqlDbAdapterObject = null;
                }
            } catch { ; }
            // close connection
            try {
                if (FmainPassed.DbIo.SqlDbConn != null) {
                    if (FmainPassed.DbIo.SqlDbConn.State.ToString() == "Open") {
                        SqlColAddCmdBuildResult = ConnClose(ref FmainPassed);
                    } else if (FmainPassed.DbIo.SqlDbConn.State.ToString() != "Open") {
                        SqlColAddCmdBuildResult = ConnReset(ref FmainPassed);
                        // SqlColAddCmdBuildResult = ConnOpen(ref SqlConnection FmainPassed.DbIo.SqlDbConn, spTableName, FileId.spTableNameFull);
                        // FmainPassed.DbIo.SqlDbConn.Close();
                    }
                }
            } catch { ; }
            //
            // reset all file control fields
            FmainPassed.FileStatus.DataClear();
            FmainPassed.DbStatus.DataClear();
            FmainPassed.ConnStatus.DataClear();
            FmainPassed.Fs.DbId.DataClear();
            FmainPassed.DbIo.DataClear();

            DbMasterSyn.DataClear();
            //
            PickRow.PdIndexDoSearch = false;

            FmainPassed.Item.ItemIdExists = false;

            DbSyn.bpSqlColumnViewCmdFirst = false;

            FmainPassed.ColTrans.HasRows = false;

            TableCloseResult = (long)StateIs.Successful;
            return TableCloseResult;
        }
        #endregion
        #region TableCreateToDo
        // <Section Id = "x
        public long TableCreateToDo(ref FileMainDef FmainPassed) {
            TableCreateResult = (long)StateIs.Started;
            if (FmainPassed.DbIo.CommandCurrent.Length > 0) {
                TableCreateResult = (long)StateIs.InProgress;
                // FmainPassed.DbIo.CommandCurrent = "CREATE TABLE " + "'" + FmainPassed.Fs.TableNameLine + "'";
                // command
                FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
            } else {
                FmainPassed.Fs.CopyTo(ref Faux.Fs);
                Faux.DbStatus.bpDoKeepOpen = true;
                //
                TableCreateResult = AsciiFileReadRecord(ref Faux);
                //
                TableCreateResult = (long)StateIs.InProgress;
                DbSyn.spSqlCreateCmd = "";
                try {
                    TableCreateResult = Faux.DbIo.SqlDbCommand.ExecuteNonQuery();
                    if (TableCreateResult > 0) {
                        Faux.FileStatus.bpDoesExist = true;
                        // Add Column 0
                        Faux.DbIo.CommandCurrent = DbSyn.spOutputAlterCommand + " " + "'" + Faux.Fs.spTableName + "'";
                        Faux.DbIo.CommandCurrent += " ADD 0 String ";
                        Faux.DbIo.CommandCurrent += " VARCHAR(512)";
                        Faux.DbIo.CommandCurrent += " { PRIMARY KEY }";
                        DbSyn.spSqlCreateCmd = Faux.DbIo.CommandCurrent;
                        Faux.DbIo.SqlDbCommand = new SqlCommand(DbSyn.spSqlCreateCmd, Faux.DbIo.SqlDbConn);
                        TableCreateResult = Faux.DbIo.SqlDbCommand.ExecuteNonQuery();
                        // Add Primary Key
                        // Faux.DbIo.CommandCurrent = " { PRIMARY KEY }";
                        // Add Unique
                        // Faux.DbIo.CommandCurrent = " { UNIQUE }";
                    } else {
                        Faux.FileStatus.bpDoesExist = false;
                    }
                } catch (SqlException ExceptionSql) {
                    LocalMessage.ErrorMsg = "";
                    ExceptSqlImpl(ref Faux, LocalMessage.ErrorMsg, ref ExceptionSql, TableCreateResult);
                    Faux.FileStatus.bpDoesExist = false;
                } catch (Exception ExceptionGeneral) {
                    LocalMessage.ErrorMsg = "";
                    ExceptGeneralFileImpl(ref Faux, LocalMessage.ErrorMsg, ref ExceptionGeneral, TableCreateResult);
                    Faux.FileStatus.bpDoesExist = false;
                } finally {
                    Faux.DbIo.SqlDbCommand = null;
                }
            }
            return TableCreateResult;
        }
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx //
        // Sql Reset
        #region Sql Database File Handling
        #region Sql Reset
        // <Section Id = "SqlReset">
        public long SqlReset(ref FileMainDef FmainPassed) {
            SqlResetResult = (long)StateIs.Started;
            // Fmain.FileStatus.bpIsInitialized
            // if (Fmain.FileStatus.bpIsInitialized) {
            // TableOpenResult = SqlReset(spTableName, spTableNameLine);
            // THIS IS A DISPOSE FUNCTION
            Fmain.FileStatus.bpIsInitialized = false;

            // }
            return SqlResetResult;
        }
        #endregion
        // List and Defaults xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx //
        #region SqlLoadFindLists
        #region Sql LoadFindLists Declarations
        /// <summary>
        /// <para>Selection Lists can be lists of any sort but generally 
        /// apply to lists that will be used for validation or used
        /// to populate combo boxes or other user interface controls.</para>
        /// <para> There is a predefined hierarchy of lists used to validate
        /// database and disk file selection. </para>
        /// <para> This is:</para> 
        /// <para> ..System</para>
        /// <para> ..Service</para>
        /// <para> ..Server</para>
        /// <para> ..Database</para>
        /// <para> ..FileGroup</para>
        /// <para> ..Table</para>
        /// <para> ..DiskFile</para>
        /// <para> ..FileOwner</para>
        /// <para> . </para>
        /// <para> Lists consist of the four components:</para>
        /// <para> ..Main</para>
        /// <para> ..Curr(ent)</para>
        /// <para> ..Prev(ious)</para>
        /// <para> ..DropDown</para>
        /// <para> The usage is self-evident except in the case of 
        /// DropDown.  It is loaded with the hard coded defaults and
        /// defaults loaded from file.  Dropdown is the list returned
        /// for user interface purposes.</para>
        /// <para> . </para>
        /// <para> The lists services are primarily supported
        /// by the following methods:</para>
        /// <para> ..ObjectListLoad</para>
        /// <para> ..ObjectListClearData</para>
        /// <para> ..GenericListLoad</para>
        /// <para> ..ObjectListLoadAscii</para>
        /// <para> ..ObjectParamLoad</para>
        /// <para> ..ObjectParamLoadAscii</para>
        /// <para> . </para>
        /// <para> Each predefined list type has a structured
        /// set of method names in order to operate.</para>
        /// <para> These are:</para>
        /// <para> ..[ListName]ListLoad</para>
        /// <para> ..[ListName]NameChanged</para>
        /// <para> ..[ListName]ListCheck</para>
        /// <para> ..[ListName]NameGetDefault</para>
        /// <para> ..[ListName]ListGet</para>
        /// <para> Please note there is a slight naming convention
        /// error here.  ListName s/b = FieldName but is not.  One
        /// example, System (the ListName) # SystemName (the field).</para>
        /// <para> The issues is the use of Name in the structured
        /// names.  This should be switch to Field so that it will
        /// be compatable with any field (beyond just Name(s)). In
        /// the second item:</para>
        /// <para> SystemNameChanged becomes SystemFieldChanged</para>
        /// </summary>
        public class SelectionList {
            public List<String> Main;
            public List<String> Curr;
            public List<String> Prev;
            public List<String> DropDown;
            public SelectionList() {
                Main = new List<string>();
                DropDown = new List<string>();
                Curr = new List<string>();
                Prev = new List<string>();
                DropDown = new List<string>();
            }
            public void PrepareNext() {
                if (Curr.Count > 0) { Prev = Curr.ToList(); }
                Curr.Clear();
                Main.Clear();
            }
        }
        public SelectionList ObjectList = new SelectionList();
        public SelectionList SystemList = new SelectionList();
        public SelectionList ServiceList = new SelectionList();
        public SelectionList ServerList = new SelectionList();
        public SelectionList DatabaseList = new SelectionList();
        public SelectionList TableList = new SelectionList();
        public SelectionList DiskFileList = new SelectionList();
        public SelectionList FileOwnerList = new SelectionList();
        public SelectionList FileGroupList = new SelectionList();

        // Controls to avoid infinite loops
        public bool ObjectListLoading;
        public bool DoingDefaults;
        #endregion

        #region Object List Load
        public long ObjectListLoadResult;
        public long ObjectListLoad(ref FileMainDef FmainPassed) {
            ObjectListLoadResult = (long)StateIs.Started;
            ObjectList = new SelectionList();
            ObjectListLoadResult = ObjectListLoad(ref FmainPassed, ref ObjectList.Curr);
            return ObjectListLoadResult;
        }

        public long ObjectListLoad(ref FileMainDef FmainPassed, ref List<String> ItemListPassed) {
            ObjectListLoadResult = (long)StateIs.Started;
            ItemListPassed = new List<String>();
            // Object[] Columns = new Object[100];
            try {
                ObjectListLoadResult = (long)StateIs.Successful;
                while (FmainPassed.DbIo.SqlDbDataReader.Read()) {
                    // FmainPassed.DbIo.SqlDbDataReader.GetValues(Columns);
                    ItemListPassed.Add(FmainPassed.DbIo.SqlDbDataReader.GetValue(0).ToString());
                    ObjectListLoadResult = (long)StateIs.EmptyResult;
                }
            } catch (Exception e) {
                ObjectListLoadResult = (long)StateIs.EmptyResult;
                // Empty Set result from Reader Command
                // throw;
            }
            return ObjectListLoadResult;
        }

        public void ObjectListClearData() {
            ObjectList.Main = null;
            SystemList.Main = null;
            ServiceList.Main = null;
            ServerList.Main = null;
            DatabaseList.Main = null;
            FileOwnerList.Main = null;
            FileGroupList.Main = null;
            TableList.Main = null;
        }
        #endregion

        #region Generic Object List Load
        public long GenericListLoadResult;
        public long GenericListLoad(
            ref FileMainDef FmainPassed,
            bool DoClearTargetPassed,
            bool DoGetUiVs,
            String ListNamePassed,
            String FieldNamePassed,
            SelectionList ObjectListPassed
            ) {
            GenericListLoadResult = (long)StateIs.Started;
            ObjectListLoading = true;
            if (ObjectListPassed == null) {
                ObjectListPassed = new SelectionList();
            } else { ObjectListPassed.PrepareNext(); }
            // FmainPassed.Fs.CopyTo(ref Faux.Fs);
            //
            // Check Database List
            //
            String CommandCurrent = "";
            // CommandCurrent += "USE [" + Faux.Fs.DatabaseName + "]; ";
            //CommandCurrent += "USE master";
            //CommandCurrent += ";";
            CommandCurrent += "SELECT " + FieldNamePassed + " FROM " + ListNamePassed;
            CommandCurrent += ";";
            FmainPassed.DbIo.CommandCurrent = CommandCurrent;

            FmainPassed.Fs.ipMetaLevelId = (long)FileType_LevelIs.Data;
            FmainPassed.Fs.ipFileTypeId = (long)FileType_Is.SQL;
            FmainPassed.Fs.ipFileSubTypeId = (long)FileType_SubTypeIs.MS;
            FmainPassed.RowInfo.UseMethod = (long)FileIo_CommandModeIs.Default;
            FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.Sql;
            GenericListLoadResult = SqlCommandDoReader(ref FmainPassed, ref CommandCurrent);
            //if ((GenericListLoadResult & (long)StateIs.MaskSuccessfulAll) > 0) {
            GenericListLoadResult = ObjectListLoad(ref FmainPassed, ref ObjectListPassed.Curr);
            //}
            if (ObjectListPassed.Curr.Count == 0) {
                //
                // Check System List
                //
                FmainPassed.Fs.FileId.FileNameLine = "C:\\Data\\" + ListNamePassed + ".ItemList";
                FmainPassed.Fs.FileId.FileNameSetFromLine(null);
                FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist = false;
                FmainPassed.DbStatus.bpDoKeepOpen = false;
                //
                FmainPassed.Fs.ipMetaLevelId = (long)FileType_LevelIs.Data;
                FmainPassed.Fs.ipFileTypeId = (long)FileType_Is.TEXT;
                FmainPassed.Fs.ipFileSubTypeId = (long)FileType_SubTypeIs.ItemList;
                FmainPassed.Fs.Direction = (int)FileAction_DirectionIs.Input;
                FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.All;
                //
                GenericListLoadResult = AsciiFileReadRecord(ref FmainPassed);
                //if ((GenericListLoadResult & (long)StateIs.MaskSuccessfulAll) > 0) {
                SystemListLoadResult = ObjectListLoadAscii(ref FmainPassed, ref ObjectListPassed.Curr);
                //}
            }
            ObjectListPassed.Main = ObjectListPassed.Curr.ToList();
            ObjectList.DropDown = ObjectListPassed.Curr.ToList();
            //
            String GenericDefault = ObjectParamLoad(ref FmainPassed, "C:\\Data\\" + ListNamePassed + "StdDefault" + ".Param");
            if (GenericDefault.Length != null) {
                if (GenericDefault.Length > 0) {
                    if (!ObjectListPassed.DropDown.Contains(GenericDefault)) {
                        ObjectListPassed.DropDown.Add(GenericDefault);
                    }
                }
            }
            GenericDefault = ObjectParamLoad(ref FmainPassed, "C:\\Data\\" + ListNamePassed + "MdmDefault" + ".Param");
            if (GenericDefault.Length != null) {
                if (GenericDefault.Length > 0) {
                    if (!ObjectListPassed.DropDown.Contains(GenericDefault)) {
                        ObjectListPassed.DropDown.Add(GenericDefault);
                    }
                }
            }
            if (!ObjectListPassed.DropDown.Contains(ListNamePassed + @"99")) { ObjectListPassed.DropDown.Add(ListNamePassed + @"99"); }
            if (ObjectListPassed.Prev.Count > 0) { ObjectListPassed.DropDown.AddRange(ObjectListPassed.Prev); }
            //
            ObjectListLoading = false;
            return GenericListLoadResult;
        }

        #endregion

        #region Ascii List Load
        public long ObjectListLoadAsciiResult;
        /// <summary> 
        /// Using the passed standard list, load
        // the record data of the passed file into the list.
        /// </summary> 
        /// <param name="ItemListPassed">List to load.</param> 
        /// <remarks></remarks> 
        public long ObjectListLoadAscii(ref FileMainDef FmainPassed, ref List<String> ItemListPassed) {
            ObjectListLoadAsciiResult = (long)StateIs.Started;
            ItemListPassed = new List<String>();
            // Object[] Columns = new Object[100];
            Type ObjectType;
            try {
                for (int ItemIndex = 0; ItemIndex < Mrecord.Items.Length; ItemIndex++) {
                    ObjectType = Mrecord.Items[ItemIndex].GetType();
                    // ItemListPassed.Add((String)((String[])(Mrecord.Items[ItemIndex]))[0]);
                    if (ObjectType == typeof(Object[]) || ObjectType == typeof(String[])) {
                        ItemListPassed.Add((String)((String[])(Mrecord.Items[ItemIndex])).First());
                    } else if (ObjectType == typeof(Object) || ObjectType == typeof(String)) {
                        ItemListPassed.Add((String)Mrecord.Items[ItemIndex]);
                    }
                }
                if (ItemListPassed.Count > 0) {
                    ObjectListLoadAsciiResult = (long)StateIs.Successful;
                } else { ObjectListLoadAsciiResult = (long)StateIs.EmptyResult; }
            } catch { ObjectListLoadAsciiResult = (long)StateIs.EmptyResult; }
            return ObjectListLoadAsciiResult;
        }
        #endregion

        #region Ascii Parameter Load
        public long ObjectParamLoadResult;
        /// <summary> 
        /// Load the requested parameter from file.
        /// </summary> 
        /// <param name="ParamFullNamePassed">Fully qualified path name to paramater</param> 
        /// <remarks></remarks> 
        public String ObjectParamLoad(ref FileMainDef FmainPassed, String ParamFullNamePassed) {
            ObjectParamLoadResult = (long)StateIs.Started;
            String ParamResult = null;
            //
            // Check System List
            //
            FmainPassed.Fs.FileId.FileNameLine = ParamFullNamePassed;
            // FmainPassed.Fs.FileId.FileNameLine = "C:\\Data\\" + ParamFullNamePassed + ".Param";
            FmainPassed.Fs.FileId.FileNameSetFromLine(null);
            FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist = false;
            FmainPassed.DbStatus.bpDoKeepOpen = false;
            //
            FmainPassed.Fs.ipMetaLevelId = (long)FileType_LevelIs.Data;
            FmainPassed.Fs.ipFileTypeId = (long)FileType_Is.TEXT;
            FmainPassed.Fs.ipFileSubTypeId = (long)FileType_SubTypeIs.ItemList;
            FmainPassed.Fs.Direction = (int)FileAction_DirectionIs.Input;
            FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.All;
            //
            GenericListLoadResult = AsciiFileReadRecord(ref FmainPassed);
            //if ((GenericListLoadResult & (long)StateIs.MaskSuccessfulAll) > 0) {
            ServerListLoadResult = ObjectParamLoadAscii(ref FmainPassed, ref ParamResult);
            // }
            return ParamResult;
        }

        public long ObjectParamLoadAsciiResult;
        /// <summary> 
        /// Load the passed Parameter Ascii Result
        /// </summary> 
        /// <param name="ItemParamPassed"></param> 
        /// <remarks>Not Finished!!!</remarks> 
        public long ObjectParamLoadAscii(ref FileMainDef FmainPassed, ref String ItemParamPassed) {
            ObjectParamLoadAsciiResult = (long)StateIs.Started;
            ItemParamPassed = null;
            // Object[] Columns = new Object[100];
            Type ObjectType;
            try {
                for (int ItemIndex = 0; ItemIndex < Mrecord.Items.Length; ItemIndex++) {
                    ObjectType = Mrecord.Items[ItemIndex].GetType();
                    // ItemListPassed.Add((String)((String[])(Mrecord.Items[ItemIndex]))[0]);
                    if (ObjectType == typeof(Object[]) || ObjectType == typeof(String[])) {
                        ItemParamPassed = (String)((String[])(Mrecord.Items[ItemIndex])).First();
                        return (ObjectParamLoadAsciiResult = (long)StateIs.Successful);
                    } else if (ObjectType == typeof(Object) || ObjectType == typeof(String)) {
                        ItemParamPassed = (String)Mrecord.Items[ItemIndex];
                        return (ObjectParamLoadAsciiResult = (long)StateIs.Successful);
                    }
                }
                ObjectParamLoadAsciiResult = (long)StateIs.Invalid;
            } catch { ObjectParamLoadAsciiResult = (long)StateIs.EmptyResult; }
            return ObjectParamLoadAsciiResult;
        }
        #endregion

        #region System Lists Management
        public long SystemListLoadResult;
        /// <summary> 
        /// Load the list of Systems
        /// </summary> 
        /// <param name="DoClearTargetPassed"></param> 
        /// <param name="DoGetUiVs"></param> 
        public long SystemListLoad(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            SystemListLoadResult = (long)StateIs.Started;
            //LocalMessage.LogEntry = 
            //XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage,
            //    XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(),
            //    FileDoResult, false,
            //    iNoErrorLevel, iNoErrorSource,
            //    bDoNotDisplay, MessageNoUserEntry,
            //    "A2" + LocalMessage.LogEntry + "\n");

            ObjectListLoading = true;
            if (SystemList.Main == null) {
                SystemList.Main = new List<String>();
            } else { SystemList.PrepareNext(); }
            // FmainPassed.Fs.CopyTo(ref Faux.Fs);
            //
            // Check System List
            //
            FmainPassed.Fs.FileId.FileNameLine = "C:\\System\\System.ItemList";
            FmainPassed.Fs.FileId.FileNameSetFromLine(null);
            FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist = false;
            FmainPassed.DbStatus.bpDoKeepOpen = false;
            FmainPassed.Fs.ipMetaLevelId = (long)FileType_LevelIs.Data;
            FmainPassed.Fs.ipFileTypeId = (long)FileType_Is.TEXT;
            FmainPassed.Fs.ipFileSubTypeId = (long)FileType_SubTypeIs.ItemList;
            FmainPassed.Fs.Direction = (int)FileAction_DirectionIs.Input;
            FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.All;
            //
            SystemListLoadResult = AsciiFileReadRecord(ref FmainPassed);
            if ((SystemListLoadResult & (long)StateIs.MaskSuccessfulAll) > 0) {
                SystemListLoadResult = ObjectListLoadAscii(ref FmainPassed, ref SystemList.Curr);
            }
            //
            // Empty List Exception
            //
            SystemList.DropDown = SystemList.Curr.ToList();
            SystemList.Main = SystemList.Curr.ToList();
            if (FmainPassed.DbMaster.MstrDbSystemDefault.Length > 0) {
                if (!SystemList.DropDown.Contains(FmainPassed.DbMaster.MstrDbSystemDefault)) {
                    SystemList.DropDown.Add(FmainPassed.DbMaster.MstrDbSystemDefault);
                }
            }
            if (FmainPassed.DbMaster.MstrDbSystemDefaultMdm.Length > 0) {
                if (!SystemList.DropDown.Contains(FmainPassed.DbMaster.MstrDbSystemDefaultMdm)) {
                    SystemList.DropDown.Add(FmainPassed.DbMaster.MstrDbSystemDefaultMdm);
                }
            }
            if (!SystemList.DropDown.Contains(@"System99")) { SystemList.DropDown.Add(@"System99"); }
            //
            if (SystemList.Prev.Count > 0 && !SystemList.Prev.SequenceEqual(SystemList.Curr)) {
                SystemList.DropDown.AddRange(SystemList.Prev);
            }
            //
            ObjectListLoading = false;
            return SystemListLoadResult;
        }

        /// <summary> 
        /// OnChange in Systems
        /// Reset dependent data.
        /// </summary> 
        public void SystemNameChanged() {
            ServiceList.Main = null;
            ServerList.Main = null;
            DatabaseList.Main = null;
            TableList.Main = null;
            DiskFileList.Main = null;
            FileOwnerList.Main = null;
            FileGroupList.Main = null;
        }

        /// <summary> 
        /// All lists are to be reset.
        /// </summary> 
        public void AllCoreNameChanged() {
            SystemList.Main = null;
            ServiceList.Main = null;
            ServerList.Main = null;
            DatabaseList.Main = null;
            TableList.Main = null;
            DiskFileList.Main = null;
            FileOwnerList.Main = null;
            FileGroupList.Main = null;
        }

        public long SystemListCheckResult;
        /// <summary> 
        /// Check if the System is in the System List.
        /// </summary> 
        /// <param name="DoClearTargetPassed"></param> 
        /// <param name="DoGetUiVs"></param> 
        public long SystemListCheck(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            SystemListCheckResult = (long)StateIs.Started;
            if (SystemList.Main == null || DoClearTargetPassed) { SystemListCheckResult = SystemListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs); }
            if (SystemList.Main.Contains(FmainPassed.Fs.SystemName)) {
                SystemListCheckResult = (long)StateIs.DoesExist;
            } else {
                SystemListCheckResult = (long)StateIs.DoesNotExist;
            }
            return SystemListCheckResult;
        }

        public long SystemNameResult;
        /// <summary> 
        /// Get the default System Name.
        /// </summary> 
        public String SystemNameGetDefault(ref FileMainDef FmainPassed) {
            SystemNameResult = (long)StateIs.Started;
            String SystemName = null;
            if (!ObjectListLoading) {
                if (SystemList.Main == null) { SystemNameResult = SystemListLoad(ref FmainPassed, false, false); }
                SystemName = SystemList.Main.FirstOrDefault();
            }
            if (SystemName == null) { SystemName = FmainPassed.DbMaster.MstrDbSystemDefault; }
            if (SystemName.Length == 0) { SystemName = FmainPassed.DbMaster.MstrDbSystemDefaultMdm; }
            if (SystemName.Length == 0) { SystemName = "System99"; }
            // SystemName = @"localhost";
            return SystemName;
        }

        public long SystemListGetResult;
        /// <summary> 
        /// Get the System List.
        /// </summary> 
        /// <param name="DoClearTargetPassed"></param> 
        /// <param name="DoGetUiVs"></param> 
        public List<String> SystemListGet(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            SystemListGetResult = (long)StateIs.Started;
            if (SystemList.Main == null || DoClearTargetPassed) {
                SystemListGetResult = SystemListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs);
            } else {
                SystemListGetResult = (long)StateIs.Successful;
            }
            if (DoGetUiVs) {
                return SystemList.DropDown;
            } else { return SystemList.Main; }
        }
        #endregion

        #region Service Lists & Default
        public long ServiceListLoadResult;
        /// <summary> 
        /// Load the Service List.
        /// </summary> 
        public long ServiceListLoad(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            ServiceListLoadResult = (long)StateIs.Started;
            ObjectListLoading = true;
            if (ServiceList.Main == null) {
                ServiceList.Main = new List<String>();
            } else { ServiceList.PrepareNext(); }
            // FmainPassed.Fs.CopyTo(ref Faux.Fs);
            //
            // Load Service List
            //
            FmainPassed.Fs.FileId.FileNameLine = "C:\\Data\\DatabaseService.ItemList";
            FmainPassed.Fs.FileId.FileNameSetFromLine(null);
            FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist = false;
            FmainPassed.DbStatus.bpDoKeepOpen = false;
            FmainPassed.Fs.ipMetaLevelId = (long)FileType_LevelIs.Data;
            FmainPassed.Fs.ipFileTypeId = (long)FileType_Is.TEXT;
            FmainPassed.Fs.ipFileSubTypeId = (long)FileType_SubTypeIs.ItemList;
            FmainPassed.Fs.Direction = (int)FileAction_DirectionIs.Input;
            FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.All;
            //
            ServiceListLoadResult = AsciiFileReadRecord(ref FmainPassed);
            if ((ServiceListLoadResult & (long)StateIs.MaskSuccessfulAll) > 0) {
                ServiceListLoadResult = ObjectListLoadAscii(ref FmainPassed, ref ServiceList.Curr);
            }
            //
            // Empty List Exception
            //
            ServiceList.DropDown = ServiceList.Curr.ToList();
            ServiceList.Main = ServiceList.Curr.ToList();
            if (FmainPassed.DbMaster.MstrDbServiceDefault.Length > 0) {
                if (!ServiceList.DropDown.Contains(FmainPassed.DbMaster.MstrDbServiceDefault)) {
                    ServiceList.DropDown.Add(FmainPassed.DbMaster.MstrDbServiceDefault);
                }
            }
            if (FmainPassed.DbMaster.MstrDbServiceDefaultMdm.Length > 0) {
                if (!ServiceList.DropDown.Contains(FmainPassed.DbMaster.MstrDbServiceDefaultMdm)) {
                    ServiceList.DropDown.Add(FmainPassed.DbMaster.MstrDbServiceDefaultMdm);
                }
            }
            if (!ServiceList.DropDown.Contains(@"Service99")) { ServiceList.DropDown.Add(@"Service99"); }
            if (ServiceList.Prev.Count > 0) { ServiceList.DropDown.AddRange(ServiceList.Prev); }
            //
            ObjectListLoading = false;
            return ServiceListLoadResult;
        }

        public long ServiceNameResult;
        /// <summary> 
        /// Get the default Service Name.
        /// </summary> 
        public String ServiceNameGetDefault(ref FileMainDef FmainPassed) {
            ServiceNameResult = (long)StateIs.Started;
            String ServiceName = null;
            if (!ObjectListLoading) {
                if (ServiceList.Main == null) { ServiceNameResult = ServiceListLoad(ref FmainPassed, false, false); }
                ServiceName = ServiceList.Main.FirstOrDefault();
            }
            if (ServiceName == null) { ServiceName = FmainPassed.DbMaster.MstrDbServiceDefault; }
            if (ServiceName.Length == 0) { ServiceName = FmainPassed.DbMaster.MstrDbServiceDefaultMdm; }
            if (ServiceName.Length == 0) { ServiceName = "Service99"; }
            // ServiceName = @"localhost";
            return ServiceName;
        }

        /// <summary> 
        /// On Service Name changed.
        /// Clear dependent lists.
        /// </summary> 
        public void ServiceNameChanged() {
            ServerList.Main = null;
            TableList.Main = null;
            DiskFileList.Main = null;
            FileOwnerList.Main = null;
            FileGroupList.Main = null;
        }

        public long ServiceListCheckResult;
        /// <summary> 
        /// Check if the Service is in the Service List.
        /// </summary> 
        public long ServiceListCheck(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            ServiceListCheckResult = (long)StateIs.Started;
            if (ServiceList.Main == null || DoClearTargetPassed) { ServiceListCheckResult = ServiceListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs); }
            if (ServiceList.Main.Contains(FmainPassed.Fs.ServiceName)) {
                ServiceListCheckResult = (long)StateIs.DoesExist;
            } else {
                ServiceListCheckResult = (long)StateIs.DoesNotExist;
            }
            return ServiceListCheckResult;
        }

        public long ServiceListGetResult;
        /// <summary> 
        /// Get this Service List.
        /// </summary> 
        public List<String> ServiceListGet(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            ServiceListGetResult = (long)StateIs.Started;
            if (ServiceList.Main == null || DoClearTargetPassed) {
                ServiceListGetResult = ServiceListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs);
            } else {
                ServiceListGetResult = (long)StateIs.Successful;
            }
            if (DoGetUiVs) {
                return ServiceList.DropDown;
            } else { return ServiceList.Main; }
        }
        #endregion

        #region Server Lists & Default
        public long ServerListLoadResult;
        /// <summary> 
        /// Load the Server List.
        /// </summary> 
        public long ServerListLoad(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            ServerListLoadResult = (long)StateIs.Started;
            ObjectListLoading = true;
            if (ServerList.Main == null) {
                ServerList.Main = new List<String>();
            } else { ServerList.PrepareNext(); }
            // FmainPassed.Fs.CopyTo(ref Faux.Fs);
            //
            // Check Server List
            //
            String CommandCurrent = "";
            CommandCurrent = "";
            //CommandCurrent += "USE master";
            //CommandCurrent += ";";
            CommandCurrent += "SELECT name FROM sys.servers ";
            CommandCurrent += ";";
            FmainPassed.DbIo.CommandCurrent = CommandCurrent;
            //
            FmainPassed.Fs.ipMetaLevelId = (long)FileType_LevelIs.Data;
            FmainPassed.Fs.ipFileTypeId = (long)FileType_Is.SQL;
            FmainPassed.Fs.ipFileSubTypeId = (long)FileType_SubTypeIs.MS;
            if (FmainPassed.Fs.SystemName.Length == 0) { FmainPassed.Fs.SystemName = FmainPassed.DbMaster.MstrDbSystemDefaultMdm; }
            if (FmainPassed.Fs.ServiceName.Length == 0) { FmainPassed.Fs.ServiceName = FmainPassed.DbMaster.MstrDbServiceDefaultMdm; }
            if (FmainPassed.Fs.ServerName.Length == 0) { FmainPassed.Fs.ServerName = FmainPassed.DbMaster.MstrDbServerDefault; }
            // if (FmainPassed.Fs.DatabaseName.Length == 0) { FmainPassed.Fs.DatabaseName = FmainPassed.DbMaster.MstrDbDatabaseDefaultMdm; }
            FmainPassed.RowInfo.UseMethod = (long)FileIo_CommandModeIs.Default;
            FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.Sql;
            //
            ServerListLoadResult = SqlCommandDoReader(ref FmainPassed, ref CommandCurrent);
            ServerListLoadResult = ObjectListLoad(ref FmainPassed, ref ServerList.Curr);
            //
            if (ServerList.Curr.Count == 0) {
                //
                // Check Text maintained Database Table List
                //
                FmainPassed.Fs.FileId.FileNameLine = "C:\\Data\\DatabaseServer.ItemList";
                FmainPassed.Fs.FileId.FileNameSetFromLine(null);
                FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist = false;
                FmainPassed.DbStatus.bpDoKeepOpen = false;
                FmainPassed.Fs.ipMetaLevelId = (long)FileType_LevelIs.Data;
                FmainPassed.Fs.ipFileTypeId = (long)FileType_Is.TEXT;
                FmainPassed.Fs.ipFileSubTypeId = (long)FileType_SubTypeIs.ItemList;
                FmainPassed.Fs.Direction = (int)FileAction_DirectionIs.Input;
                FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.All;
                //
                ServerListLoadResult = AsciiFileReadRecord(ref FmainPassed);
                if ((ServerListLoadResult & (long)StateIs.MaskSuccessfulAll) > 0) {
                    ServerListLoadResult = ObjectListLoadAscii(ref FmainPassed, ref ServerList.Curr);
                }
            }
            //
            // Empty List Exception
            //
            ServerList.DropDown = ServerList.Curr.ToList();
            ServerList.Main = ServerList.Curr.ToList();
            if (FmainPassed.DbMaster.MstrDbServerDefault.Length > 0) {
                if (!ServerList.DropDown.Contains(FmainPassed.DbMaster.MstrDbServerDefault)) {
                    ServerList.DropDown.Add(FmainPassed.DbMaster.MstrDbServerDefault);
                }
            }
            if (FmainPassed.DbMaster.MstrDbServerDefaultMdm.Length > 0) {
                if (!ServerList.DropDown.Contains(FmainPassed.DbMaster.MstrDbServerDefaultMdm)) {
                    ServerList.DropDown.Add(FmainPassed.DbMaster.MstrDbServerDefaultMdm);
                }
            }
            if (!ServerList.DropDown.Contains(@"Server99")) { ServerList.DropDown.Add(@"Server99"); }
            //
            if (ServerList.Prev.Count > 0 && !ServerList.Prev.SequenceEqual(ServerList.Curr)) { ServerList.DropDown.AddRange(ServerList.Prev); }
            //
            ObjectListLoading = false;
            return ServerListLoadResult;
        }

        public long ServerListCheckResult;
        /// <summary> 
        /// Check if the Server is in the Server List.
        /// </summary> 
        public long ServerListCheck(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            ServerListCheckResult = (long)StateIs.Started;
            if (ServerList.Main == null || DoClearTargetPassed) { ServerListCheckResult = ServerListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs); }
            if (ServerList.Main.Contains(FmainPassed.Fs.ServerName)) {
                ServerListCheckResult = (long)StateIs.DoesExist;
            } else {
                ServerListCheckResult = (long)StateIs.DoesNotExist;
            }
            return ServerListCheckResult;
        }

        public long ServerNameResult;
        /// <summary> 
        /// Get the default Server Name.
        /// </summary> 
        public String ServerNameGetDefault(ref FileMainDef FmainPassed) {
            ServerNameResult = (long)StateIs.Started;
            String ServerName = null;
            if (!ObjectListLoading) {
                if (ServerList.Main == null) { ServerNameResult = ServerListLoad(ref FmainPassed, false, false); }
                ServerName = ServerList.Main.FirstOrDefault();
            }
            if (ServerName == null) {
                if (FmainPassed.Fs.SystemName.Length > 0 && FmainPassed.Fs.ServiceName.Length > 0) {
                    ServerName = FmainPassed.Fs.SystemName + @"\" + FmainPassed.Fs.ServiceName;
                }
            }
            if (ServerName == null) { ServerName = FmainPassed.DbMaster.MstrDbServerDefault; }
            if (ServerName.Length == 0) { ServerName = FmainPassed.DbMaster.MstrDbServerDefaultMdm; }
            if (ServerName.Length == 0) { ServerName = "Server99"; }
            // ServerName = @"localhost";
            return ServerName;
        }

        /// <summary> 
        /// On Server Name Changed.
        /// Clear dependent lists.
        /// </summary> 
        public void ServerNameChanged() {
            DatabaseList.Main = null;
            TableList.Main = null;
            DiskFileList.Main = null;
            FileOwnerList.Main = null;
            FileGroupList.Main = null;
        }

        public long ServerListGetResult;
        /// <summary> 
        /// Get the Server List.
        /// </summary> 
        public List<String> ServerListGet(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            ServerListGetResult = (long)StateIs.Started;
            if (ServerList.Main == null || DoClearTargetPassed) {
                ServerListGetResult = ServerListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs);
            } else {
                ServerListGetResult = (long)StateIs.Successful;
            }
            if (DoGetUiVs) {
                return ServerList.DropDown;
            } else { return ServerList.Main; }
        }
        #endregion

        #region Database Lists & Default
        public long DatabaseListLoadResult;
        /// <summary> 
        /// Load the Database List.
        /// </summary> 
        public long DatabaseListLoad(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            DatabaseListLoadResult = (long)StateIs.Started;
            ObjectListLoading = true;
            if (DatabaseList.Main == null) {
                DatabaseList.Main = new List<String>();
            } else { DatabaseList.PrepareNext(); }
            // FmainPassed.Fs.CopyTo(ref Faux.Fs);
            //
            // Check Database List
            //
            String CommandCurrent = "";
            // CommandCurrent += "USE [" + Faux.Fs.DatabaseName + "]; ";
            //CommandCurrent += "USE master";
            //CommandCurrent += ";";
            CommandCurrent += "SELECT name FROM sys.databases ";
            CommandCurrent += "WHERE [name] NOT IN ";
            CommandCurrent += "( ";
            CommandCurrent += "'master' , 'msdb', 'model', 'tempdb', ";
            CommandCurrent += "'resource', 'distribution' ";
            CommandCurrent += ")";
            CommandCurrent += ";";
            FmainPassed.DbIo.CommandCurrent = CommandCurrent;

            FmainPassed.Fs.ipMetaLevelId = (long)FileType_LevelIs.Data;
            FmainPassed.Fs.ipFileTypeId = (long)FileType_Is.SQL;
            FmainPassed.Fs.ipFileSubTypeId = (long)FileType_SubTypeIs.MS;
            FmainPassed.RowInfo.UseMethod = (long)FileIo_CommandModeIs.Default;
            FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.Sql;
            DatabaseListLoadResult = SqlCommandDoReader(ref FmainPassed, ref CommandCurrent);
            if ((DatabaseListLoadResult & (long)StateIs.MaskSuccessfulAll) > 0) {
                DatabaseListLoadResult = ObjectListLoad(ref FmainPassed, ref DatabaseList.Curr);
            }
            if (DatabaseList.Curr.Count == 0) {
                //
                // Check System List
                //
                FmainPassed.Fs.FileId.FileNameLine = "C:\\Data\\Database.ItemList";
                FmainPassed.Fs.FileId.FileNameSetFromLine(null);
                FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist = false;
                FmainPassed.DbStatus.bpDoKeepOpen = false;
                //
                FmainPassed.Fs.ipMetaLevelId = (long)FileType_LevelIs.Data;
                FmainPassed.Fs.ipFileTypeId = (long)FileType_Is.TEXT;
                FmainPassed.Fs.ipFileSubTypeId = (long)FileType_SubTypeIs.ItemList;
                FmainPassed.Fs.Direction = (int)FileAction_DirectionIs.Input;
                FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.All;
                //
                DatabaseListLoadResult = AsciiFileReadRecord(ref FmainPassed);
                if ((DatabaseListLoadResult & (long)StateIs.MaskSuccessfulAll) > 0) {
                    DatabaseListLoadResult = ObjectListLoadAscii(ref FmainPassed, ref DatabaseList.Curr);
                }
            }
            DatabaseList.Main = DatabaseList.Curr.ToList();
            DatabaseList.DropDown = DatabaseList.Curr.ToList();
            //
            if (FmainPassed.DbMaster.MstrDbDatabaseDefault.Length > 0) {
                if (!DatabaseList.DropDown.Contains(FmainPassed.DbMaster.MstrDbDatabaseDefault)) {
                    DatabaseList.DropDown.Add(FmainPassed.DbMaster.MstrDbDatabaseDefault);
                }
            }
            if (FmainPassed.DbMaster.MstrDbDatabaseDefaultMdm.Length > 0) {
                if (!DatabaseList.DropDown.Contains(FmainPassed.DbMaster.MstrDbDatabaseDefaultMdm)) {
                    DatabaseList.DropDown.Add(FmainPassed.DbMaster.MstrDbDatabaseDefaultMdm);
                }
            }
            if (!DatabaseList.DropDown.Contains(@"Database99")) { DatabaseList.DropDown.Add(@"Database99"); }
            //
            if (DatabaseList.Prev.Count > 0 && !DatabaseList.Prev.SequenceEqual(DatabaseList.Curr)) { DatabaseList.DropDown.AddRange(DatabaseList.Prev); }
            //
            ObjectListLoading = false;
            return DatabaseListLoadResult;
        }

        public long DatabaseListCheckResult;
        /// <summary> 
        /// Check if the Database is in the Database List.
        /// </summary> 
        public long DatabaseListCheck(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            DatabaseListCheckResult = (long)StateIs.Started;
            if (DatabaseList.Main == null || DoClearTargetPassed) { DatabaseListCheckResult = DatabaseListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs); }
            if (DatabaseList.Main.Contains(FmainPassed.Fs.DatabaseName)) {
                DatabaseListCheckResult = (long)StateIs.DoesExist;
            } else {
                DatabaseListCheckResult = (long)StateIs.DoesNotExist;
            }
            return DatabaseListCheckResult;
        }

        public long DatabaseNameResult;
        /// <summary> 
        /// Get the default Database Name.
        /// </summary> 
        public String DatabaseNameGetDefault(ref FileMainDef FmainPassed) {
            DatabaseNameResult = (long)StateIs.Started;
            String DatabaseName = null;
            if (!ObjectListLoading) {
                if (DatabaseList.Main == null) { DatabaseNameResult = DatabaseListLoad(ref FmainPassed, false, false); }
                DatabaseName = DatabaseList.Main.FirstOrDefault();
            }
            if (DatabaseName == null) { DatabaseName = FmainPassed.DbMaster.MstrDbDatabaseDefault; }
            if (DatabaseName.Length == 0) { DatabaseName = FmainPassed.DbMaster.MstrDbDatabaseDefaultMdm; }
            if (DatabaseName.Length == 0) { DatabaseName = @"Database99"; }
            return DatabaseName;
        }

        /// <summary> 
        /// On Database Name changed
        /// Clear dependent lists.
        /// </summary> 
        public void DatabaseNameChanged() {
            TableList.Main = null;
            DiskFileList.Main = null;
            FileOwnerList.Main = null;
            FileGroupList.Main = null;
        }

        public long DatabaseListGetResult;
        /// <summary> 
        /// Get the Database List.
        /// </summary> 
        public List<String> DatabaseListGet(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            DatabaseListGetResult = (long)StateIs.Started;
            if (DatabaseList.Main == null || DoClearTargetPassed) {
                DatabaseListGetResult = DatabaseListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs);
            } else {
                DatabaseListGetResult = (long)StateIs.Successful;
            }
            if (DoGetUiVs) {
                return DatabaseList.DropDown;
            } else { return DatabaseList.Main; }
        }
        #endregion

        #region File Owner Lists & Default
        public long FileOwnerListLoadResult;
        /// <summary> 
        /// Load the File Owner List.
        /// </summary> 
        public long FileOwnerListLoad(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            FileOwnerListLoadResult = (long)StateIs.Started;
            ObjectListLoading = true;
            if (FileOwnerList.Main == null) {
                FileOwnerList.Main = new List<String>();
            } else { FileOwnerList.PrepareNext(); }
            //            FmainPassed.Fs.CopyTo(ref Faux.Fs);
            //
            // Check Database Table List
            //
            String CommandCurrent = "";
            //CommandCurrent += "USE [" + "master" + "]";
            //CommandCurrent += ";";
            CommandCurrent += "SELECT * FROM sys.sysusers WHERE ";
            CommandCurrent += "NAME = 'dbo' OR ";
            CommandCurrent += "NAME NOT IN ( ";
            CommandCurrent += "SELECT NAME FROM sys.sysusers WHERE ";
            CommandCurrent += "NAME LIKE 'db%' ";
            CommandCurrent += "OR NAME LIKE '##%' ";
            CommandCurrent += "OR NAME = 'INFORMATION_SCHEMA' ";
            CommandCurrent += "OR NAME = 'public' ";
            CommandCurrent += ")";
            CommandCurrent += ";";
            FmainPassed.DbIo.CommandCurrent = CommandCurrent;
            FmainPassed.Fs.ipMetaLevelId = (long)FileType_LevelIs.Data;
            FmainPassed.Fs.ipFileTypeId = (long)FileType_Is.SQL;
            FmainPassed.Fs.ipFileSubTypeId = (long)FileType_SubTypeIs.MS;
            FmainPassed.RowInfo.UseMethod = (long)FileIo_CommandModeIs.Default;
            FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.Sql;
            //
            FileOwnerListLoadResult = SqlCommandDoReader(ref FmainPassed, ref CommandCurrent);
            if ((FileOwnerListLoadResult & (long)StateIs.MaskSuccessfulAll) > 0) {
                FileOwnerListLoadResult = ObjectListLoad(ref FmainPassed, ref FileOwnerList.Curr);
            }
            //
            if (FileOwnerList.Curr.Count == 0) {
                //
                // Check Text maintained Database Table List
                //
                FmainPassed.Fs.FileId.FileNameLine = "C:\\Data\\DatabaseFileOwner.ItemList";
                FmainPassed.Fs.FileId.FileNameSetFromLine(null);
                FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist = false;
                FmainPassed.DbStatus.bpDoKeepOpen = false;
                FmainPassed.Fs.ipMetaLevelId = (long)FileType_LevelIs.Data;
                FmainPassed.Fs.ipFileTypeId = (long)FileType_Is.TEXT;
                FmainPassed.Fs.ipFileSubTypeId = (long)FileType_SubTypeIs.ItemList;
                FmainPassed.Fs.Direction = (int)FileAction_DirectionIs.Input;
                FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.All;
                //
                FileOwnerListLoadResult = AsciiFileReadRecord(ref FmainPassed);
                if ((FileOwnerListLoadResult & (long)StateIs.MaskSuccessfulAll) > 0) {
                    FileOwnerListLoadResult = ObjectListLoadAscii(ref FmainPassed, ref FileOwnerList.Curr);
                }
            }
            //
            FileOwnerList.DropDown = FileOwnerList.Curr.ToList();
            FileOwnerList.Main = FileOwnerList.Curr.ToList();
            if (FmainPassed.DbMaster.MstrDbOwnerDefault.Length > 0) {
                if (!FileOwnerList.DropDown.Contains(FmainPassed.DbMaster.MstrDbOwnerDefault)) {
                    FileOwnerList.DropDown.Add(FmainPassed.DbMaster.MstrDbOwnerDefault);
                }
            }
            if (FmainPassed.DbMaster.MstrDbOwnerDefaultMdm.Length > 0) {
                if (!FileOwnerList.DropDown.Contains(FmainPassed.DbMaster.MstrDbOwnerDefaultMdm)) {
                    FileOwnerList.DropDown.Add(FmainPassed.DbMaster.MstrDbOwnerDefaultMdm);
                }
            }
            if (!FileOwnerList.DropDown.Contains(@"dbo")) { FileOwnerList.DropDown.Add(@"dbo"); }
            //
            if (FileOwnerList.Prev.Count > 0 && !FileOwnerList.Prev.SequenceEqual(FileOwnerList.Curr)) { FileOwnerList.DropDown.AddRange(FileOwnerList.Prev); }
            //
            ObjectListLoading = false;
            return FileOwnerListLoadResult;
        }

        public long FileOwnerListCheckResult;
        /// <summary> 
        /// Check if the File Owner is in the File Owner List.
        /// </summary> 
        public long FileOwnerListCheck(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            FileOwnerListCheckResult = (long)StateIs.Started;
            if (FileOwnerList.Main == null || DoClearTargetPassed) { FileOwnerListCheckResult = FileOwnerListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs); }
            if (FileOwnerList.Main.Contains(FmainPassed.Fs.FileOwnerName)) {
                FileOwnerListCheckResult = (long)StateIs.DoesExist;
            } else {
                FileOwnerListCheckResult = (long)StateIs.DoesNotExist;
            }
            return FileOwnerListCheckResult;
        }

        public long FileOwnerResult;
        /// <summary> 
        /// Get the default File Owner.
        /// </summary> 
        public String FileOwnerGetDefault(ref FileMainDef FmainPassed) {
            FileOwnerResult = (long)StateIs.Started;
            String FileOwner = null;
            if (!ObjectListLoading) {
                if (FileOwnerList.Main == null) { FileOwnerResult = FileOwnerListLoad(ref FmainPassed, false, false); }
                FileOwner = FileOwnerList.Main.FirstOrDefault();
            }
            if (FileOwner == null) { FileOwner = FmainPassed.DbMaster.MstrDbOwnerDefault; }
            if (FileOwner.Length == 0) { FileOwner = FmainPassed.DbMaster.MstrDbOwnerDefaultMdm; }
            if (FileOwner.Length == 0) { FileOwner = "dbo"; }
            return FileOwner;
        }

        public long FileOwnerListGetResult;
        /// <summary> 
        /// Get the File Owner List.
        /// </summary> 
        public List<String> FileOwnerListGet(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            FileOwnerListGetResult = (long)StateIs.Started;
            if (FileOwnerList.Main == null || DoClearTargetPassed) {
                FileOwnerListGetResult = FileOwnerListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs);
            } else {
                FileOwnerListGetResult = (long)StateIs.Successful;
            }
            if (DoGetUiVs) {
                return FileOwnerList.DropDown;
            } else { return FileOwnerList.Main; }
        }
        #endregion

        #region File Table Lists & Default
        // File Table (Database)
        public long FileNameResult;
        /// <summary> 
        /// Get the default File Name.
        /// </summary> 
        public String FileNameGetDefault(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            FileNameResult = (long)StateIs.Started;
            return "File99.Txt";
        }
        #endregion

        #region Table Lists & Default
        public long TableListLoadResult;
        /// <summary> 
        /// Load the Table List.
        /// </summary> 
        public long TableListLoad(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            TableListLoadResult = (long)StateIs.Started;
            ObjectListLoading = true;
            if (TableList.Main == null) {
                TableList.Main = new List<String>();
            } else { TableList.PrepareNext(); }
            // FmainPassed.Fs.CopyTo(ref Faux.Fs);
            //
            if (FmainPassed.Fs.DatabaseName.Length == 0) {
                return (TableListLoadResult = (long)StateIs.EmptyValue);
            }
            //
            if (FmainPassed.Fs.TableNameLine.Length == 0) { FmainPassed.Fs.TableNameLine = TableNameLineBuild(ref FmainPassed); }
            //
            String CommandCurrent = "";
            //CommandCurrent += "USE " + FmainPassed.Fs.DatabaseName + "";
            //CommandCurrent += ";";
            CommandCurrent += "SELECT name FROM sys.tables";
            CommandCurrent += ";";
            FmainPassed.DbIo.CommandCurrent = CommandCurrent;
            // FmainPassed.DbIo.CommandCurrent = "USE [" + FmainPassed.Fs.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FmainPassed.Fs.FileId.FileName + "'";
            // FmainPassed.DbIo.CommandCurrent = "USE[" + FmainPassed.Fs.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FileId.FileNameFull + "'";
            // FmainPassed.DbIo.CommandCurrent = "USE[" + FmainPassed.Fs.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FileId.FileNameFull + "';";
            // FmainPassed.DbIo.CommandCurrent = "USE[" + FmainPassed.Fs.DatabaseName + "]; SELECT * FROM sys.objects WHERE name = " + "'" + FileId.FileNameFull + "';";
            // SQL = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=MyTable";
            // int result = this.ExecuteQuery("if exists(select * from sys.databases where name = {0}", DatabaseName) return 1 else 0");
            // \r\n"; FROM INFORMATION_SCHEMA.TABLES

            FmainPassed.Fs.ipMetaLevelId = (long)FileType_LevelIs.Data;
            FmainPassed.Fs.ipFileTypeId = (long)FileType_Is.SQL;
            FmainPassed.Fs.ipFileSubTypeId = (long)FileType_SubTypeIs.MS;
            FmainPassed.RowInfo.UseMethod = (long)FileIo_CommandModeIs.Default;
            FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.Sql;
            TableListLoadResult = SqlCommandDoReader(ref FmainPassed, ref CommandCurrent);
            if ((TableListLoadResult & (long)StateIs.MaskSuccessfulAll) > 0) {
                TableListLoadResult = ObjectListLoad(ref FmainPassed, ref TableList.Curr);
            }
            TableList.DropDown = TableList.Curr.ToList();
            TableList.Main = TableList.Curr.ToList();
            if (FmainPassed.DbMaster.MstrDbTableDefault.Length > 0) {
                if (!TableList.DropDown.Contains(FmainPassed.DbMaster.MstrDbTableDefault)) {
                    TableList.DropDown.Add(FmainPassed.DbMaster.MstrDbTableDefault);
                }
            }
            if (FmainPassed.DbMaster.MstrDbTableDefaultMdm.Length > 0) {
                if (!TableList.DropDown.Contains(FmainPassed.DbMaster.MstrDbTableDefaultMdm)) {
                    TableList.DropDown.Add(FmainPassed.DbMaster.MstrDbTableDefaultMdm);
                }
            }
            if (!TableList.DropDown.Contains(@"Table99")) { TableList.DropDown.Add(@"Table99"); }
            //
            if (TableList.Prev.Count > 0 && !TableList.Prev.SequenceEqual(TableList.Curr)) { TableList.DropDown.AddRange(TableList.Prev); }
            //
            ObjectListLoading = false;
            return TableListLoadResult;
        }

        public long TableListCheckResult;
        /// <summary> 
        /// Check if the Table is in the Table List.
        /// </summary> 
        public long TableListCheck(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            TableListCheckResult = (long)StateIs.Started;
            if (FmainPassed.Fs.DatabaseName.Length == 0) {
                TableListCheckResult = (long)StateIs.EmptyValue;
            } else {
                if (TableList.Main == null || DoClearTargetPassed) { TableListCheckResult = TableListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs); }
                if (TableList.Main.Contains(FmainPassed.Fs.TableName)) {
                    TableListCheckResult = (long)StateIs.DoesExist;
                } else {
                    TableListCheckResult = (long)StateIs.DoesNotExist;
                }
            }
            return TableListCheckResult;
        }

        public long TableResult;
        /// <summary> 
        /// Get the default Table Name.
        /// </summary> 
        public String TableGetDefault(ref FileMainDef FmainPassed) {
            TableResult = (long)StateIs.Started;
            String Table = null;
            if (!ObjectListLoading) {
                if (TableList.Main == null) { TableResult = TableListLoad(ref FmainPassed, false, false); }
                Table = TableList.Main.FirstOrDefault();
            }
            if (Table == null) { Table = "Table99"; }
            return Table;
        }

        public long TableListGetResult;
        /// <summary> 
        /// Get the Table List.
        /// </summary> 
        public List<String> TableListGet(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            TableListGetResult = (long)StateIs.Started;
            if (TableList.Main == null || DoClearTargetPassed) {
                TableListGetResult = TableListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs);
            } else {
                TableListGetResult = (long)StateIs.Successful;
            }
            if (DoGetUiVs) {
                return TableList.DropDown;
            } else { return TableList.Main; }
        }
        #endregion

        #region FileGroup Lists & Default
        public long FileGroupListLoadResult;
        /// <summary> 
        /// Load the File Group List.
        /// </summary> 
        public long FileGroupListLoad(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            FileGroupListLoadResult = (long)StateIs.Started;
            ObjectListLoading = true;
            if (FileGroupList.Main == null) {
                FileGroupList.Main = new List<String>();
            } else { FileGroupList.PrepareNext(); }
            // FmainPassed.Fs.CopyTo(ref Faux.Fs);
            if (FmainPassed.Fs.DatabaseName.Length == 0) {
                return (FileGroupListLoadResult = (long)StateIs.EmptyValue);
            }
            if (FmainPassed.Fs.TableNameLine.Length == 0) {
                FmainPassed.Fs.TableNameLine = TableNameLineBuild(ref FmainPassed);
            }
            String CommandCurrent = "";
            CommandCurrent = "";
            CommandCurrent += "USE [" + FmainPassed.Fs.DatabaseName + "]";
            CommandCurrent += ";";
            CommandCurrent += "SELECT name FROM sys.filegroups ";
            CommandCurrent += ";";
            FmainPassed.DbIo.CommandCurrent = CommandCurrent;

            FmainPassed.Fs.ipMetaLevelId = (long)FileType_LevelIs.Data;
            FmainPassed.Fs.ipFileTypeId = (long)FileType_Is.SQL;
            FmainPassed.Fs.ipFileSubTypeId = (long)FileType_SubTypeIs.MS;
            FmainPassed.RowInfo.UseMethod = (long)FileIo_CommandModeIs.Default;
            FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.Sql;
            //
            FileGroupListLoadResult = SqlCommandDoReader(ref FmainPassed, ref CommandCurrent);
            if ((FileGroupListLoadResult & (long)StateIs.MaskSuccessfulAll) > 0) {
                FileGroupListLoadResult = ObjectListLoad(ref FmainPassed, ref FileGroupList.Curr);
            }
            FileGroupList.DropDown = FileGroupList.Curr.ToList();
            FileGroupList.Main = FileGroupList.Curr.ToList();
            if (FmainPassed.DbMaster.MstrDbFileGroupDefault.Length > 0) {
                if (!FileGroupList.DropDown.Contains(FmainPassed.DbMaster.MstrDbFileGroupDefault)) {
                    FileGroupList.DropDown.Add(FmainPassed.DbMaster.MstrDbFileGroupDefault);
                }
            }
            if (FmainPassed.DbMaster.MstrDbFileGroupDefaultMdm.Length > 0) {
                if (!FileGroupList.DropDown.Contains(FmainPassed.DbMaster.MstrDbFileGroupDefaultMdm)) {
                    FileGroupList.DropDown.Add(FmainPassed.DbMaster.MstrDbFileGroupDefaultMdm);
                }
            }
            if (!FileGroupList.DropDown.Contains(@"FileGroup99")) { FileGroupList.DropDown.Add(@"FileGroup99"); }
            //
            if (FileGroupList.Prev.Count > 0 && !FileGroupList.Prev.SequenceEqual(FileGroupList.Curr)) { FileGroupList.DropDown.AddRange(FileGroupList.Prev); }
            //
            ObjectListLoading = false;
            return FileGroupListLoadResult;
        }

        public long FileGroupListCheckResult;
        /// <summary> 
        /// Check if the File Group is in the File Group List.
        /// </summary> 
        public long FileGroupListCheck(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            FileGroupListCheckResult = (long)StateIs.Started;
            if (FmainPassed.Fs.DatabaseName.Length == 0) {
                FileGroupListCheckResult = (long)StateIs.EmptyValue;
            } else {
                if (FileGroupList.Main == null || DoClearTargetPassed) { FileGroupListCheckResult = FileGroupListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs); }
                if (FileGroupList.Main.Contains(FmainPassed.Fs.FileGroupName)) {
                    FileGroupListCheckResult = (long)StateIs.DoesExist;
                } else {
                    FileGroupListCheckResult = (long)StateIs.DoesNotExist;
                }
            }
            return FileGroupListCheckResult;
        }

        public long FileGroupResult;
        /// <summary> 
        /// Get the default File Group.
        /// </summary> 
        public String FileGroupGetDefault(ref FileMainDef FmainPassed) {
            FileGroupResult = (long)StateIs.Started;
            String FileGroupName = null;
            if (!ObjectListLoading) {
                if (FileGroupList.Main == null) { FileGroupResult = FileGroupListLoad(ref FmainPassed, false, false); }
                FileGroupName = FileGroupList.Main.FirstOrDefault();
            }
            if (FileGroupName == null) { FileGroupName = FmainPassed.DbMaster.MstrDbFileGroupDefault; }
            if (FileGroupName.Length == 0) { FileGroupName = FmainPassed.DbMaster.MstrDbFileGroupDefaultMdm; }
            // if (FileGroupName.Length == 0) { FileGroupName = "FileGroup99"; }
            // FileGroupName = @"localhost";
            return FileGroupName;
        }

        public long FileGroupListGetResult;
        /// <summary> 
        /// Get the File Group List.
        /// </summary> 
        public List<String> FileGroupListGet(ref FileMainDef FmainPassed, bool DoClearTargetPassed, bool DoGetUiVs) {
            FileGroupListGetResult = (long)StateIs.Started;
            if (FileGroupList.Main == null || DoClearTargetPassed) {
                FileGroupListGetResult = FileGroupListLoad(ref FmainPassed, DoClearTargetPassed, DoGetUiVs);
            } else {
                FileGroupListGetResult = (long)StateIs.Successful;
            }
            if (DoGetUiVs) {
                return FileGroupList.DropDown;
            } else { return FileGroupList.Main; }
        }
        #endregion
        #endregion
        // Sql Command Execution - xxxxxxxxxxxxxxxxxxxxxxx
        #region Sql DoCommand
        public long SqlCommandDoReaderResult;
        /// <summary> 
        /// Do the SQL Reader Command.
        /// </summary> 
        /// <param name="CommandPassed">The SQL Command to be executed.</param> 
        public long SqlCommandDoReader(ref FileMainDef FmainPassed, ref String CommandPassed) {
            SqlCommandDoReaderResult = (long)StateIs.Started;
            #region Documentation
            //
            //Default The query may return multiple result sets. Execution of the query may affect the database state. Default sets no CommandBehavior flags, so calling ExecuteReader(CommandBehavior.Default) is functionally equivalent to calling ExecuteReader(). 
            //SingleResult The query returns a single result set. 
            //SchemaOnly The query returns column information only. When using SchemaOnly, the .NET Framework Data Provider for SQL Server precedes the statement being executed with SET FMTONLY ON. 
            //KeyInfo The query returns column and primary key information.  
            //SingleRow The query is expected to return a single row. Execution of the query may affect the database state. Some .NET Framework data providers may, but are not required to, use this information to optimize the performance of the command. When you specify SingleRow with the ExecuteReader method of the OleDbCommand object, the .NET Framework Data Provider for OLE DB performs binding using the OLE DB IRow interface if it is available. Otherwise, it uses the IRowset interface. If your SQL statement is expected to return only a single row, specifying SingleRow can also improve application performance. It is possible to specify SingleRow when executing queries that return multiple result sets. In that case, multiple result sets are still returned, but each result set has a single row. 
            //SequentialAccess Provides a way for the DataReader to handle rows that contain columns with large binary values. Rather than loading the entire row, SequentialAccess enables the DataReader to load data as a stream. You can then use the GetBytes or GetChars method to specify a byte location to start the read operation, and a limited buffer size for the data being returned. 
            //CloseConnection When the command is executed, the associated Connection object is closed when the associated DataReader object is closed. 

            #endregion
            Faux.RowInfoDb.RowCount = 0;
            LocalMessage.LogEntry = "Do Command " + CommandPassed;
            // FileMainHeaderGet(ref FmainPassed);
            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage,
                XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(),
                FileDoResult, false,
                iNoErrorLevel, iNoErrorSource,
                bDoNotDisplay, MessageNoUserEntry,
                "A2" + LocalMessage.LogEntry + "\n");

            //  check Connection
            SqlCommandDoReaderResult = ConnCheck(ref FmainPassed);
            if ((SqlCommandDoReaderResult & (long)StateIs.MaskSuccessfulAll) > 0) {
                SqlCommandDoReaderResult = (long)StateIs.InProgress;
                //
                if (FmainPassed.DbIo.SqlDbCommand != null) {
                    FmainPassed.DbIo.SqlDbCommand.Dispose();
                    FmainPassed.DbIo.SqlDbCommand = null;
                }
                FmainPassed.DbIo.SqlDbCommand =
                    new SqlCommand(CommandPassed, FmainPassed.DbIo.SqlDbConn);
                FmainPassed.DbIo.SqlDbCommand.CommandTimeout = FmainPassed.DbIo.SqlDbCommandTimeout;
                FmainPassed.DbIo.SqlDbCommand.CommandType = CommandType.Text;
                if (FmainPassed.DbIo.SqlDbDataReader != null) {
                    //    FmainPassed.DbIo.SqlDbDataReader.Dispose();
                    //    FmainPassed.DbIo.SqlDbDataReader = null;
                    if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed) {
                        FmainPassed.DbIo.SqlDbDataReader.Close();
                    }
                }
                if (FmainPassed.DbIo.SqlDbDataWriter != null) {
                    //    FmainPassed.DbIo.SqlDbDataWriter.Dispose();
                    //    FmainPassed.DbIo.SqlDbDataWriter = null;
                }
                switch (FmainPassed.RowInfo.UseMethod) {
                    case ((long)FileIo_CommandModeIs.UseExecuteNoQuery):
                        // Not appropriate for Check Does Exist
                        // no row or columns
                        // used for create, settings, etc
                        try {
                            FmainPassed.RowInfoDb.RowCount = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                            // XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + FmainPassed.DbIo.SqlDbDataReader.sGetString(0));
                        } finally {
                        }
                        break;
                    case ((long)FileIo_CommandModeIs.Default):
                    case ((long)FileIo_CommandModeIs.SingleRow):
                    case ((long)FileIo_CommandModeIs.SingleResult):
                    case ((long)FileIo_CommandModeIs.KeyInfo):
                    case ((long)FileIo_CommandModeIs.SchemaOnly):
                    case ((long)FileIo_CommandModeIs.UseExecuteScalar):
                        #region Execute Scalar Command
                        // FmainPassed.DbIo.SqlDbDataReader.
                        try {
                            switch (FmainPassed.RowInfo.UseMethod) {
                                case ((long)FileIo_CommandModeIs.UseExecuteScalar):
                                    // Scalar = 1 Row 1 Column, no reader
                                    try {
                                        DbFileTemp.ooThisTempObject = FmainPassed.DbIo.SqlDbCommand.ExecuteScalar();
                                        // XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + FmainPassed.DbIo.SqlDbDataReader.sGetString(0));
                                    } finally {
                                    }
                                    break;
                                case ((long)FileIo_CommandModeIs.Default):
                                    // All Rows All Columns
                                    FmainPassed.DbIo.SqlDbDataReader =
                                        FmainPassed.DbIo.SqlDbCommand.ExecuteReader((CommandBehavior)FmainPassed.RowInfo.UseMethod);
                                    break;
                                case ((long)FileIo_CommandModeIs.SingleRow):
                                // Single Row
                                case ((long)FileIo_CommandModeIs.KeyInfo):
                                // Column and Primary Key info
                                case ((long)FileIo_CommandModeIs.SchemaOnly):
                                // Column info
                                case ((long)FileIo_CommandModeIs.SingleResult):
                                    // Single Result Set
                                    FmainPassed.DbIo.SqlDbDataReader =
                                        FmainPassed.DbIo.SqlDbCommand.ExecuteReader((CommandBehavior)FmainPassed.RowInfo.UseMethod);
                                    // FmainPassed.RowInfo.CloseIsNeeded = true;
                                    break;
                                default:
                                    SqlCommandDoReaderResult = (long)StateIs.Undefined;
                                    FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.NotSupported;
                                    LocalMessage.ErrorMsg = "The Sql Command Behavior (Mehtod of reading the file) has not been set)";
                                    throw new NotSupportedException(LocalMessage.ErrorMsg);
                            }
                            #region Catch Error on probative read
                        } catch (SqlException ExceptionSql) {
                            LocalMessage.ErrorMsg = "";
                            ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, SqlCommandDoReaderResult);
                            SqlCommandDoReaderResult = (long)StateIs.DatabaseError;
                            FmainPassed.FileStatus.bpDoesExist = false;
                            FmainPassed.RowInfoDb.RowCount = 0;
                            FmainPassed.FileStatus.bpIsOpen = false;
                        } catch (Exception ExceptionGeneral) {
                            LocalMessage.ErrorMsg = "";
                            ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlCommandDoReaderResult);
                            SqlCommandDoReaderResult = (long)StateIs.OsError;
                            FmainPassed.FileStatus.bpDoesExist = false;
                            FmainPassed.RowInfoDb.RowCount = 0;
                            FmainPassed.FileStatus.bpIsOpen = false;
                        } finally {
                            // FmainPassed.DbIo.SqlDbCommand = null;
                            // SqlCommandDoReaderResult = Faux.RowInfoDb.RowCount;
                        }
                        // If File is NOT Open Try to Select the File Name in the Master File
                            #endregion
                        #endregion
                        #region Reader Object Get Type
                        // FmainPassed.DbIo.SqlDbCommand.Container.Components.Count;
                        try {
                            FmainPassed.RowInfo.RowIndex = 0;
                            FmainPassed.RowInfoDb.RowCount = -1;
                            FmainPassed.RowInfo.bpHasRows = FmainPassed.DbIo.SqlDbDataReader.HasRows;
                            FmainPassed.ColTrans.ColCount = FmainPassed.DbIo.SqlDbDataReader.FieldCount;
                            // only applies to db changes:
                            FmainPassed.RowInfoDb.RowCount = FmainPassed.DbIo.SqlDbDataReader.RecordsAffected;
                            //
                            if (FmainPassed.RowInfo.bpHasRows) {
                                SqlCommandDoReaderResult = (long)StateIs.DoesExist;
                                FmainPassed.FileStatus.bpDoesExist = true;
                                // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                                // Data is present
                            } else {
                                SqlCommandDoReaderResult = (long)StateIs.EmptyResult;
                            }
                            #region Catch errors on Reader Object Get Type
                        } catch (SqlException ExceptionSql) {
                            LocalMessage.ErrorMsg = "";
                            ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, SqlCommandDoReaderResult);
                            SqlCommandDoReaderResult = (long)StateIs.DatabaseError;
                        } catch (Exception ExceptionGeneral) {
                            LocalMessage.ErrorMsg = "";
                            ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlCommandDoReaderResult);
                            SqlCommandDoReaderResult = (long)StateIs.OsError;
                        } finally {
                            if (SqlCommandDoReaderResult == (long)StateIs.InProgress) {
                                SqlCommandDoReaderResult = (long)StateIs.DatabaseError;
                            }
                            FmainPassed.DbIo.SqlDbCommand.ResetCommandTimeout();
                            if ((FmainPassed.RowInfo.UseMethod
                                & (long)(FileIo_CommandModeIs.SingleResult
                                | FileIo_CommandModeIs.SchemaOnly
                                | FileIo_CommandModeIs.KeyInfo
                                | FileIo_CommandModeIs.SingleRow
                                | FileIo_CommandModeIs.SequentialAccess)) != 0) {
                                if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed) {
                                    if (!FmainPassed.DbStatus.bpDoKeepOpen) {
                                        FmainPassed.DbIo.SqlDbDataReader.Close();
                                        if (FmainPassed.DbStatus.bpDoDispose) {
                                            FmainPassed.DbIo.SqlDbDataReader.Dispose();
                                        }
                                    }
                                }
                            }
                        } // Execute Command OK Try to access Reader ItemData
                            #endregion
                        break;
                        #endregion
                    case (99):
                    default:
                        // Command Error
                        SqlCommandDoReaderResult = (long)StateIs.Undefined;
                        FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.NotSupported;
                        FmainPassed.RowInfoDb.RowCount = 0;
                        FmainPassed.FileStatus.bpDoesExist = false;
                        FmainPassed.FileStatus.bpIsOpen = false;
                        LocalMessage.ErrorMsg = "The Sql Command Behavior (Mehtod of reading the file) has not been set)";
                        throw new NotSupportedException(LocalMessage.ErrorMsg);
                } // Execute Correct Command for Reading Method FileUseMethod
                // TODO SqlCommandDoReader $ Log Read to Console
            } // Connection DoesNotExist
            return SqlCommandDoReaderResult;
        }
        // <Section Id = "SqlCommandDo">
        /// <summary> 
        /// Do the SQL Command.
        /// </summary> 
        /// <param name="CommandPassed">The SQL Command to be executed.</param> 
        public long SqlCommandDo(ref FileMainDef FmainPassed, String CommandPassed) {
            SqlCommandDoResult = (long)StateIs.Started;
            FmainPassed.DbIo.CommandCurrent = CommandPassed;
            // command
            // command
            FmainPassed.DbIo.SqlDbCommand = new SqlCommand(CommandPassed, FmainPassed.DbIo.SqlDbConn);
            SqlCommandDoResult = (long)StateIs.InProgress;
            try {
                SqlCommandDoResult = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
            } catch (SqlException ExceptionSql) {
                LocalMessage.ErrorMsg = "";
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, SqlCommandDoResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            } catch (Exception ExceptionGeneral) {
                LocalMessage.ErrorMsg = "";
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlCommandDoResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            } finally {
                FmainPassed.DbIo.SqlDbCommand = null;
            }
            return SqlCommandDoResult;
        }

        /// <summary> 
        /// Set the default SQL Command for this Table.
        /// </summary> 
        public long SqlCommandSetDefault(ref FileMainDef FmainPassed) {
            SqlCommandSetDefaultResult = (long)StateIs.Started;
            FmainPassed.DbIo.CommandCurrent = "SELECT * FROM " + "'" + FmainPassed.Fs.TableName + "'";
            if (FmainPassed.Item.ItemId.Length > 0) {
                FmainPassed.DbIo.CommandCurrent += "WHERE [name] = " + "'" + FmainPassed.Item.ItemId + "'";
            }
            // command

            return SqlCommandSetDefaultResult;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx //
        public long SqlReaderCloseResult;
        /// <summary> 
        /// Close the SQL Reader.
        /// </summary> 
        public long SqlReaderClose(ref FileMainDef FmainPassed) {
            SqlReaderCloseResult = (long)StateIs.Started;
            if (FmainPassed.DbIo.SqlDbDataReader != null) {
                if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed) {
                    if (!FmainPassed.DbStatus.bpDoKeepOpen) {
                        FmainPassed.DbIo.SqlDbDataReader.Close();
                        if (FmainPassed.DbStatus.bpDoDispose) {
                            FmainPassed.DbIo.SqlDbDataReader.Dispose();
                        }
                        FmainPassed.DbStatus.bpIsOpen = false;
                    }
                }
            }
            return (SqlReaderCloseResult = (long)StateIs.Successful);
        }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx //
        public long SqlWriterCloseResult;
        /// <summary> 
        /// CLose the SQL Writer.
        /// </summary> 
        public long SqlWriterClose(ref FileMainDef FmainPassed) {
            SqlWriterCloseResult = (long)StateIs.Started;
            if (FmainPassed.DbIo.SqlDbDataWriter != null) {
                if (!FmainPassed.DbStatus.bpDoKeepOpen || FmainPassed.DbStatus.bpDoDispose) {
                    FmainPassed.DbIo.SqlDbDataWriter.Dispose();
                    FmainPassed.DbStatus.bpIsOpen = false;
                }
            }
            return (SqlWriterCloseResult = (long)StateIs.Successful);
        }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx //
        #region SqlFileUpdate
        // <Section Id = "InsertValue">
        /// <summary> 
        /// Put an Insert command
        /// into the SQL Data command.
        /// </summary> 
        public long SqlDataInsert(ref FileMainDef FmainPassed) {
            SqlDataInsertResult = (long)StateIs.Started;
            FmainPassed.DbIo.CommandCurrent = "INSERT " + "'" + FmainPassed.Fs.TableNameLine + "'";
            // command
            // DbSyn.spOutputItem += ColText;
            if (DbSyn.spOutputInsert.Length > 0) {
                DbSyn.spOutputInsertCommand = DbSyn.spOutputInsertPrefix + FmainPassed.Fs.TableName + DbSyn.spOutputInsertPrefix1;
                DbSyn.spOutputInsertCommand += DbSyn.spOutputInsert + DbSyn.spOutputInsertSuffix;
            }
            //
            if (DbSyn.spOutputValues.Length > 0) {
                DbSyn.spOutputInsertCommand = DbSyn.spOutputInsertPrefix + FmainPassed.Fs.TableName;
                DbSyn.spOutputInsertCommand += DbSyn.spOutputInsert + DbSyn.spOutputInsertSuffix;
            }
            //
            DbSyn.spOutputInsertCommand = DbSyn.spOutputInsert + "\n" + DbSyn.spOutputValues;
            FmainPassed.DbIo.CommandCurrent = DbSyn.spOutputInsertCommand;
            LocalId.LongResult = SqlDataWrite(ref FmainPassed);

            return SqlDataInsertResult;
        }
        // <Section Id = "UpdateValue">
        /// <summary> 
        /// Put an Update command
        /// into the SQL Data command.
        /// </summary> 
        public long SqlDataUpdate(ref FileMainDef FmainPassed) {
            SqlDataUpdateResult = (long)StateIs.Started;
            Fmain.DbIo.CommandCurrent = "UPDATE TABLE " + "'" + FmainPassed.Fs.TableNameLine + "'";
            // command

            return SqlDataUpdateResult;
        }
        // <Section Id = "FileDataDelete">
        /// <summary> 
        /// Put a Delete command
        /// into the SQL Data command.
        /// </summary> 
        public long SqlDataDelete(ref FileMainDef FmainPassed) {
            SqlDataDeleteResult = (long)StateIs.Started;
            FmainPassed.DbIo.CommandCurrent = "DELETE " + "'" + FmainPassed.Fs.TableNameLine + "'";
            // command

            return SqlDataDeleteResult;
        }
        // <Section Id = "FileDataAdd">
        /// <summary> 
        /// Put and Add command
        /// into the SQL Data command.
        /// </summary> 
        public long SqlDataAdd(ref FileMainDef FmainPassed) {
            SqlDataAddResult = (long)StateIs.Started;
            FmainPassed.DbIo.CommandCurrent = "ALTER " + "'" + FmainPassed.Fs.TableNameLine + "'";
            // command

            return SqlDataAddResult;
        }
        // <Section Id = "FileDataGet">
        #endregion
        #region SqlFileGetSet
        // <Section Id = "FileDataGet">
        /// <summary> 
        /// Put a Get command
        /// into the SQL Data command.
        /// </summary> 
        public long SqlDataGet(ref FileMainDef FmainPassed) {
            SqlDataGetResult = (long)StateIs.Started;

            return SqlDataGetResult;
        }
        // <Section Id = "FileDataWrite">
        /// <summary> 
        /// Put a Create Table command (NO)
        /// into the SQL Data command.
        /// Create the SQL Command instance.
        /// </summary> 
        public long SqlDataWrite(ref FileMainDef FmainPassed) {
            SqlDataWriteResult = (long)StateIs.Started;
            FmainPassed.DbIo.CommandCurrent = "CREATE TABLE " + "'" + FmainPassed.Fs.TableNameLine + "'";
            // command
            FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
            SqlDataWriteResult = (long)StateIs.InProgress;
            try {
                SqlDataWriteResult = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                if (SqlDataWriteResult > 0) {
                    FmainPassed.FileStatus.bpDoesExist = true;
                    // Add Column 0
                    FmainPassed.DbIo.CommandCurrent = "ALTER TABLE " + "'" + FmainPassed.Fs.TableName + "'";
                    FmainPassed.DbIo.CommandCurrent += " ADD 0 String ";
                    FmainPassed.DbIo.CommandCurrent += " VARCHAR(512)";
                    FmainPassed.DbIo.CommandCurrent += " { PRIMARY KEY }";
                    FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
                    SqlDataWriteResult = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                    // Add Primary Key
                    // FmainPassed.DbIo.CommandCurrent = " { PRIMARY KEY }";
                    // Add Unique
                    // FmainPassed.DbIo.CommandCurrent = " { UNIQUE }";
                } else {
                    FmainPassed.FileStatus.bpDoesExist = false;
                }
            } catch (SqlException ExceptionSql) {
                LocalMessage.ErrorMsg = "";
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, SqlDataWriteResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            } catch (Exception ExceptionGeneral) {
                LocalMessage.ErrorMsg = "";
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlDataWriteResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            } finally {
                FmainPassed.DbIo.SqlDbCommand = null;
            }

            return SqlDataWriteResult;
        }
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx //
        #region SqlDict // Dictionary Handling // xxxxxxxxxx
        #region SqlDictProcess
        /// <summary> 
        /// Process the dictionary definition data.
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks></remarks> 
        public long SqlDictProcessDb(ref FileMainDef FmainPassed, bool PassedConnDoClose, bool PassedConnDoDispose, bool PassedSqlFileDoClose, RowInfoDef RowInfoPassed, ColTransformDef ColTransformPassed) {
            // TODO SqlDictProcessDb Needs work
            SqlDictProcessDbResult = (long)StateIs.Started;
            // DbStatus.bpDoClose = PassedConnDoClose;
            // DbStatus.bpDoDispose = DbStatus.bpDoDispose;
            // SqlFileDoClose = PassedSqlFileDoClose;

            SqlDictProcessDbResult = (long)StateIs.InProgress;

            RowInfoPassed.UseMethod = (long)FileIo_CommandModeIs.SingleResult;
            RowInfoPassed.CloseIsNeeded = false;

            // Row
            RowInfoPassed.HasRows = false;
            RowInfoPassed.RowContinue = false;
            RowInfoPassed.RowIndex = 0;
            RowInfoPassed.RowCount = 0;
            // Clr Native
            RowInfoPassed.RowMax = PickRowDef.PdIndexMax;
            System.Object[] osoaThisGetValues = new System.Object[(int)ArrayMax.ColumnMax];
            System.Object osoThisGetValue;
            // Sql
            // ipRowIndex = 0;
            // System.Object[] RowArray;
            // System.Object osoThisGetSqlValue;

            // File Row Colomns
            RowInfoPassed.HasColumns = false;
            RowInfoPassed.ColumnContinue = false;
            RowInfoPassed.ColumnMax = PickRowDef.PdIndexMax;
            // File Column Fields
            // Action
            // FmainPassed.ColTrans.ColAction;
            // File Level
            // FmainPassed.ColTrans.FileUseIndexName;
            // FmainPassed.ColTrans.FileIndex;
            // FmainPassed.ColTrans.FileIndexName;
            // FmainPassed.ColTrans.FileCount;
            // Row
            // FmainPassed.ColTrans.iRowIndex;
            // FmainPassed.ColTrans.iRowCount;
            // FmainPassed.ColTrans.sRowIndexName;
            // FmainPassed.ColTrans.RowLastTouched;
            //
            // FmainPassed.ColTrans.HasRows;
            // FmainPassed.ColTrans.RowContinue;
            // FmainPassed.ColTrans.RowMax;
            // Column
            // FmainPassed.ColTrans.ColIndex;
            // FmainPassed.ColTrans.ColCount;
            // FmainPassed.ColTrans.ColIndexName;
            // FmainPassed.ColTrans.ColIndexLastTouched;
            // FmainPassed.ColTrans.ColCountVisible;
            // FmainPassed.ColTrans.ColCountHidden;
            // FmainPassed.ColTrans.ColumnMax;

            // Reset Column Processing
            //FmainPassed.ColTrans.ColAction = ColTransformDef.SFC_RESET;
            //FmainPassed.ColTrans.FileIndexName = "";
            //FmainPassed.ColTrans.ColRowIndex = 0;
            //FmainPassed.ColTrans.ColRowCount = 0;
            sTemp0 = "";
            SqlDictProcessDbResult = SqlColAction(
                ref FmainPassed,
                //ref FmainPassed.DbIo.SqlDbDataReader, ref FmainPassed.DbIo.SqlDbDataWriter, 
                //ref RowInfoDb, ref ColTrans, 
                false, ColTransformDef.SFC_RESET, sTemp0, 0, 0);
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
            // FmainPassed.DbIo.SqlDbCommand.ExecuteReader();
            // FmainPassed.DbIo.SqlDbCommand.ExecuteReader(new CommandBehavior()); == CommandBehavior.Default
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.KeyInfo);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.SchemaOnly);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.SequentialAccess);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.SingleResult);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.SingleRow);

            // When CommandBehavior.CloseConnection
            // Implicitly closes the connection because 
            // CommandBehavior.CloseConnection was specified.
            // otherwise:

            // Int32 count = (int32)cmd.ExecuteScalar();

            SqlDictProcessDbResult = (long)StateIs.InProgress;
            FmainPassed.FileStatus.bpDoesExist = false;
            FmainPassed.RowInfoDb.RowCount = 0;
            if (FmainPassed.Fs.TableNameLine.Length == 0 || !FmainPassed.DbStatus.bpNameIsValid) {
                FmainPassed.Fs.TableNameLine = TableNameLineBuild(ref FmainPassed);
            }
            //  check Connection
            SqlDictProcessDbResult = ConnCheck(ref FmainPassed);
            if (StateIsSuccessfulAll(SqlDictProcessDbResult)) {
                SqlDictProcessDbResult = (long)StateIs.InProgress;
                //
                for (RowInfoPassed.UseMethod = 1; RowInfoPassed.UseMethod < 256; RowInfoPassed.UseMethod <<= 1) {
                    lTemp = (RowInfoPassed.UseMethod &
                        (long)(FileIo_CommandModeIs.SingleResult
                        | FileIo_CommandModeIs.SchemaOnly
                        | FileIo_CommandModeIs.KeyInfo
                        | FileIo_CommandModeIs.SingleRow
                        | FileIo_CommandModeIs.SequentialAccess));
                    lTemp = RowInfoPassed.UseMethod << 4;
                    lTemp = RowInfoPassed.UseMethod
                        & (long)(FileIo_CommandModeIs.SingleResult
                        | FileIo_CommandModeIs.SchemaOnly
                        | FileIo_CommandModeIs.KeyInfo
                        | FileIo_CommandModeIs.SingleRow
                        | FileIo_CommandModeIs.SequentialAccess);
                    if ((RowInfoPassed.UseMethod
                        & (long)(FileIo_CommandModeIs.SingleResult
                        | FileIo_CommandModeIs.SchemaOnly
                        | FileIo_CommandModeIs.KeyInfo
                        | FileIo_CommandModeIs.SingleRow
                        | FileIo_CommandModeIs.SequentialAccess)) != 0) {
                        if (FmainPassed.DbIo.SqlDbDataReader != null) {
                            if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed) {
                                if (FmainPassed.RowInfo.CloseIsNeeded) { FmainPassed.DbIo.SqlDbDataReader.Close(); }
                            }
                        }
                        FmainPassed.RowInfo.CloseIsNeeded = false;
                    }
                    if (!FmainPassed.FileStatus.bpIsOpen) {
                        SqlDictProcessDbResult = (long)StateIs.InProgress;
                        try {
                            FmainPassed.DbIo.CommandCurrent = "";
                            // FmainPassed.DbIo.CommandCurrent += "USE [" + Fs.DatabaseName + "]; ";
                            FmainPassed.DbIo.CommandCurrent += "SELECT * FROM sys.databases ";
                            FmainPassed.DbIo.CommandCurrent += "WHERE [name] NOT IN ";
                            FmainPassed.DbIo.CommandCurrent += "( ";
                            FmainPassed.DbIo.CommandCurrent += "'master' , 'msdb', 'model', 'tempdb', ";
                            FmainPassed.DbIo.CommandCurrent += "'resource', 'distribution' ";
                            FmainPassed.DbIo.CommandCurrent += ")";
                            // FmainPassed.DbIo.CommandCurrent += ";";
                            // FmainPassed.DbIo.CommandCurrent = "USE [" + Fs.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + Fs.TableName + "'";
                            // FmainPassed.DbIo.CommandCurrent = "USE[" + Fs.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + TableNameFull + "'";
                            // FmainPassed.DbIo.CommandCurrent = "USE[" + Fs.DatabaseName + "]; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + TableNameFull + "';";
                            // FmainPassed.DbIo.CommandCurrent = "USE[" + Fs.DatabaseName + "]; SELECT * FROM sys.objects WHERE name = " + "'" + TableNameFull + "';";
                            // SQL = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=MyTable";
                            // int result = this.ExecuteQuery("if exists(select * from sys.databases where name = {0}", DatabaseName) return 1 else 0");
                            // \r\n"; FROM INFORMATION_SCHEMA.TABLES


                            FmainPassed.RowInfoDb.RowCount = 0;
                            FmainPassed.DbIo.SqlDbCommand = null;
                            FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
                            FmainPassed.DbIo.SqlDbCommand.CommandTimeout = FmainPassed.DbIo.SqlDbCommandTimeout;
                            FmainPassed.DbIo.SqlDbCommand.CommandType = CommandType.Text;
                            switch (RowInfoPassed.UseMethod) {
                                case ((long)FileIo_CommandModeIs.UseExecuteNoQuery):
                                    // Not appropriate for Check Does Exist
                                    // no row or columns
                                    // used for create, settings, etc
                                    try {
                                        FmainPassed.RowInfoDb.RowCount = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                                        // Console.WriteLine(FmainPassed.DbIo.SqlDbDataReader.sGetString(0));
                                    } finally {
                                    }
                                    break;
                                case ((long)FileIo_CommandModeIs.Default):
                                case ((long)FileIo_CommandModeIs.SingleRow):
                                case ((long)FileIo_CommandModeIs.SingleResult):
                                case ((long)FileIo_CommandModeIs.KeyInfo):
                                case ((long)FileIo_CommandModeIs.SchemaOnly):
                                case ((long)FileIo_CommandModeIs.UseExecuteScalar):
                                    // FmainPassed.DbIo.SqlDbDataReader.
                                    FmainPassed.DbIo.SqlDbDataReader = null;
                                    FmainPassed.DbIo.SqlDbDataWriter = null;
                                    switch (RowInfoPassed.UseMethod) {
                                        case ((long)FileIo_CommandModeIs.UseExecuteScalar):
                                            // Scalar = 1 Row 1 Column, no reader
                                            try {
                                                DbFileTemp.ooThisTempObject = FmainPassed.DbIo.SqlDbCommand.ExecuteScalar();
                                                // Console.WriteLine(FmainPassed.DbIo.SqlDbDataReader.sGetString(0));
                                            } finally {
                                            }
                                            break;
                                        case ((long)FileIo_CommandModeIs.Default):
                                        // All Rows All Columns
                                        case ((long)FileIo_CommandModeIs.SingleRow):
                                        // Single Row
                                        case ((long)FileIo_CommandModeIs.KeyInfo):
                                        // Column and Primary Key info
                                        case ((long)FileIo_CommandModeIs.SchemaOnly):
                                        // Column info
                                        case ((long)FileIo_CommandModeIs.SingleResult):
                                            // Single Result Set
                                            FmainPassed.DbIo.SqlDbDataReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader((CommandBehavior)RowInfoPassed.UseMethod);
                                            // FmainPassed.RowInfo.CloseIsNeeded = true;
                                            break;
                                        default:
                                            SqlDictProcessDbResult = (long)StateIs.Undefined;
                                            LocalMessage.ErrorMsg = "The Sql Command Behavior (Mehtod of reading the file) has not been set)";
                                            throw new NotSupportedException(LocalMessage.ErrorMsg);
                                    }
                                    // FmainPassed.DbIo.SqlDbCommand.Container.Components.Count;
                                    try {
                                        RowInfoPassed.RowIndex = 0;
                                        FmainPassed.RowInfoDb.RowCount = -1;
                                        RowInfoPassed.HasRows = FmainPassed.DbIo.SqlDbDataReader.HasRows;
                                        FmainPassed.ColTrans.ColCount = FmainPassed.DbIo.SqlDbDataReader.FieldCount;

                                        // only applies to db changes:
                                        // FmainPassed.RowInfoDb.RowCount = FmainPassed.DbIo.SqlDbDataReader.RecordsAffected;
                                        tThisTempType = FmainPassed.DbIo.SqlDbDataReader.GetType();
                                        //
                                        if (RowInfoPassed.HasRows) {
                                            try {
                                                // PROCESS ROWS
                                                // bRead Loop
                                                // while (FmainPassed.DbIo.SqlDbDataReader.bNextResult()) (
                                                // For Each Loop 
                                                // PROCESS ROW
                                                // }
                                                RowInfoPassed.RowCount = 0;
                                                // FmainPassed.RowInfo.RowIndex = 0;
                                                RowInfoPassed.RowContinue = true;
                                                for (RowInfoPassed.RowIndex = 0; RowInfoPassed.RowContinue && RowInfoPassed.RowIndex < RowInfoPassed.RowMax; RowInfoPassed.RowIndex++) {
                                                    //
                                                    // Create Array to accept DataReader Row.
                                                    // GetSql    Array of Sql Values
                                                    // if (FmainPassed.RowInfo.RowIndex >= RowArray.Count()) {
                                                    // RowArray = new System.Object[FmainPassed.RowInfo.RowIndex + 5];
                                                    // }
                                                    // FmainPassed.DbIo.SqlDbDataReader.bNextResult();
                                                    // FmainPassed.DbIo.SqlDbDataReader.iGetSqlValues(RowArray);
                                                    //
                                                    // GetNative Array of Native Values
                                                    // FmainPassed.DbIo.SqlDbDataReader.bNextResult();
                                                    RowInfoPassed.ColumnContinue = FmainPassed.DbIo.SqlDbDataReader.Read();
                                                    if (RowInfoPassed.ColumnContinue) {
                                                        FmainPassed.ColTrans.ColCount = FmainPassed.DbIo.SqlDbDataReader.GetValues(osoaThisGetValues);
                                                        FmainPassed.RowInfoDb.RowCount++;
                                                        //
                                                        // ageGroup = age < 2 ? "Infant" 
                                                        //     : age < 19 ? "Teen" 
                                                        //     : age < 30 ? "Middle aged" 
                                                        //     : "old";
                                                        // xxxx ooThis = (ooTmp = FmainPassed.DbIo.SqlDbDataReader.ooGetSqlValue(FmainPassed.RowInfo.RowIndex)) != null ? ooTmp : null;
                                                        // if (ThisGetValueObject != null) {
                                                        //
                                                        // PROCESS DATA COLUMNS
                                                        // Result Set Row Column Metadata
                                                        FmainPassed.ColTrans.ColCount = FmainPassed.DbIo.SqlDbDataReader.FieldCount;
                                                        FmainPassed.ColTrans.ColCountVisible = FmainPassed.DbIo.SqlDbDataReader.VisibleFieldCount;
                                                        FmainPassed.RowInfo.ColumnContinue = true;
                                                        // foreach (System.Object osoCurrGetSqlValues in RowArray) {
                                                        // PROCESS COLUMN
                                                        // }
                                                        for (FmainPassed.ColTrans.ColIndex = 0; FmainPassed.RowInfo.ColumnContinue && FmainPassed.ColTrans.ColIndex < FmainPassed.ColTrans.ColCount; FmainPassed.ColTrans.ColIndex++) {
                                                            // FmainPassed.DbIo.SqlDbDataReader. PROCESS DETAILS
                                                            // FmainPassed.DbIo.SqlDbDataReader.

                                                            // FmainPassed.ColTrans.iGetOrdinal = FmainPassed.DbIo.SqlDbDataReader.iGetOrdinal("xxx");
                                                            // FmainPassed.ColTrans.ColIndex = FmainPassed.DbIo.SqlDbDataReader.iGetOrdinal(FmainPassed.ColTrans.iGetName);
                                                            FmainPassed.ColTrans.iGetName = FmainPassed.DbIo.SqlDbDataReader.GetName(FmainPassed.ColTrans.ColIndex);
                                                            FmainPassed.ColTrans.iGetOrdinal = FmainPassed.DbIo.SqlDbDataReader.GetOrdinal(FmainPassed.ColTrans.iGetName);
                                                            FmainPassed.ColTrans.sGetDataTypeName = FmainPassed.DbIo.SqlDbDataReader.GetDataTypeName(FmainPassed.ColTrans.ColIndex);
                                                            FmainPassed.ColTrans.ttGetFieldType = FmainPassed.DbIo.SqlDbDataReader.GetFieldType(FmainPassed.ColTrans.ColIndex);
                                                            // FmainPassed.ColTrans.tfdtGetSchemaTable = FmainPassed.DbIo.SqlDbDataReader.GetSchemaTable();
                                                            FmainPassed.ColTrans.bIsDBNull = FmainPassed.DbIo.SqlDbDataReader.IsDBNull(FmainPassed.ColTrans.ColIndex);

                                                            // GetSql
                                                            // ooThisTempObject = FmainPassed.DbIo.SqlDbDataReader.ooGetSqlValue(FmainPassed.ColTrans.ColIndex);
                                                            // tsdssThisTempSqlString = FmainPassed.DbIo.SqlDbDataReader.sGetSqlString(FmainPassed.ColTrans.ColIndex);

                                                            // GetNative
                                                            FmainPassed.ColTrans.ooGetValue = FmainPassed.DbIo.SqlDbDataReader.GetValue(FmainPassed.ColTrans.ColIndex);
                                                            sTemp0 = FmainPassed.DbIo.SqlDbDataReader.GetName(FmainPassed.ColTrans.ColIndex);
                                                            SqlDictProcessDbResult = SqlColAction(
                                                                ref FmainPassed,                                                                // ref FmainPassed.DbIo.SqlDbDataReader, ref FmainPassed.DbIo.SqlDbDataWriter, ref RowInfoDb, ref ColTrans, 
                                                                false, ColTransformDef.SFC_GET_NATIVE_VALUE, sTemp0, FmainPassed.ColTrans.ColIndex, FmainPassed.ColTrans.ColCount);
                                                            // ColTrans.ColCount++;
                                                            if (FmainPassed.ColTrans.ColIndex >= FmainPassed.ColTrans.ColCount) {
                                                                FmainPassed.RowInfo.ColumnContinue = false;
                                                            }
                                                            sTemp0 = "ColomnUpdate";
                                                            SqlDictProcessDbResult = SqlColAction(
                                                                ref FmainPassed,
                                                                //ref FmainPassed.DbIo.SqlDbDataReader, ref FmainPassed.DbIo.SqlDbDataWriter, ref RowInfoDb, ref ColTrans, 
                                                                false, ColTransformDef.SFC_SET_COLUMN, sTemp0, FmainPassed.ColTrans.ColIndex, FmainPassed.ColTrans.ColCount);
                                                            // Console.WriteLine(FmainPassed.DbIo.SqlDbDataReader.sGetString(0));
                                                        } // Column Loop
                                                    } // Row Continue
                                                }// Row Loop
                                            } catch (SqlException ExceptionSql) {
                                                LocalMessage.ErrorMsg = "";
                                                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, SqlDictProcessDbResult);
                                                // if (I am a serious error) {
                                                // SqlDictProcessDbResult = (int) StateIs.DatabaseError;
                                                // FmainPassed.FileStatus.bpDoesExistResult = (int) StateIs.DoesNotExist;
                                                // } else {
                                                //
                                                // }
                                            } catch (Exception ExceptionGeneral) {
                                                LocalMessage.ErrorMsg = "";
                                                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlDictProcessDbResult);
                                                // if (I am a serious error) {
                                                // SqlDictProcessDbResult = (int) StateIs.OsError;
                                                // FmainPassed.FileStatus.ipDoesExistResult = (int) StateIs.DoesNotExist;
                                                // } else {
                                                //
                                                // }
                                            } finally {
                                                sTemp0 = "RowUpdate";
                                                SqlDictProcessDbResult = SqlColAction(ref FmainPassed, false, ColTransformDef.SFC_SET_ROW, sTemp0, FmainPassed.ColTrans.ColIndex, FmainPassed.ColTrans.ColCount);
                                                if (FmainPassed.RowInfoDb.RowCount > 0) {
                                                    SqlDictProcessDbResult = (long)StateIs.DoesExist;
                                                    LocalMessage.ErrorMsg = "File Name Does not exist in Sql File Dict Process Db";
                                                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictProcessDbResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                                                }
                                            } // HasRow Try Row Loop Reading
                                        } // HasRows
                                    } catch (SqlException ExceptionSql) {
                                        LocalMessage.ErrorMsg = "";
                                        ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, SqlDictProcessDbResult);
                                        SqlDictProcessDbResult = (long)StateIs.DatabaseError;
                                        FmainPassed.FileStatus.ipDoesExistResult = (long)StateIs.DoesNotExist;
                                    } catch (Exception ExceptionGeneral) {
                                        LocalMessage.ErrorMsg = "";
                                        ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlDictProcessDbResult);
                                        SqlDictProcessDbResult = (long)StateIs.OsError;
                                        FmainPassed.FileStatus.ipDoesExistResult = (long)StateIs.DoesNotExist;
                                    } finally {
                                        if ((RowInfoPassed.UseMethod
                                            & (long)(FileIo_CommandModeIs.SingleResult
                                            | FileIo_CommandModeIs.SchemaOnly
                                            | FileIo_CommandModeIs.KeyInfo
                                            | FileIo_CommandModeIs.SingleRow
                                            | FileIo_CommandModeIs.SequentialAccess)) != 0) {
                                            if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed) {
                                                if (PassedSqlFileDoClose) { FmainPassed.DbIo.SqlDbDataReader.Close(); }
                                            }
                                        }
                                    } // Execute Command OK Try to access Reader ItemData
                                    break;
                                case (99):
                                default:
                                    // Command Error
                                    SqlDictProcessDbResult = (long)StateIs.Undefined;
                                    FmainPassed.FileStatus.bpDoesExist = false;
                                    FmainPassed.RowInfoDb.RowCount = 0;
                                    FmainPassed.FileStatus.bpIsOpen = false;
                                    LocalMessage.ErrorMsg = "The Sql Command Behavior (Mehtod of reading the file) has not been set)";
                                    throw new NotSupportedException(LocalMessage.ErrorMsg);
                            } // Execute Correct Command for Reading Method FileUseMethod
                            FmainPassed.DbIo.SqlDbCommand.ResetCommandTimeout();
                            if ((RowInfoPassed.UseMethod & (long)(FileIo_CommandModeIs.SingleResult | FileIo_CommandModeIs.SchemaOnly | FileIo_CommandModeIs.KeyInfo | FileIo_CommandModeIs.SingleRow | FileIo_CommandModeIs.SequentialAccess)) != 0) {
                                if (FmainPassed.DbIo.SqlDbDataReader != null) {
                                    if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed) {
                                        FmainPassed.RowInfo.CloseIsNeeded = true;  // Only used for research Benchmark Loop
                                    }
                                }
                            }
                        } catch (SqlException ExceptionSql) {
                            LocalMessage.ErrorMsg = "";
                            ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, SqlDictProcessDbResult);
                            SqlDictProcessDbResult = (long)StateIs.DatabaseError;
                            FmainPassed.FileStatus.bpDoesExist = false;
                            FmainPassed.RowInfoDb.RowCount = 0;
                            FmainPassed.FileStatus.bpIsOpen = false;
                        } catch (Exception ExceptionGeneral) {
                            LocalMessage.ErrorMsg = "";
                            ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlDictProcessDbResult);
                            SqlDictProcessDbResult = (long)StateIs.OsError;
                            FmainPassed.FileStatus.bpDoesExist = false;
                            FmainPassed.RowInfoDb.RowCount = 0;
                            FmainPassed.FileStatus.bpIsOpen = false;
                        } finally {
                            // FmainPassed.DbIo.SqlDbCommand = null;
                            // SqlDictProcessDbResult = FmainPassed.RowInfoDb.RowCount;
                        } // If File is NOT Open Try to Select the File Name in the Master File
                        // 
                        if (FmainPassed.RowInfoDb.RowCount == (long)StateIs.DoesNotExist) {
                            FmainPassed.FileStatus.ipDoesExistResult = (long)StateIs.DoesNotExist;
                            FmainPassed.FileStatus.bpDoesExist = false;
                            // FmainPassed.RowInfoDb.RowCount = 0;
                            FmainPassed.FileStatus.bpIsOpen = false;
                        } else if (FmainPassed.RowInfoDb.RowCount >= 0) {
                            FmainPassed.FileStatus.ipDoesExistResult = (long)StateIs.DoesExist;
                            FmainPassed.FileStatus.bpDoesExist = true;
                        } else {
                            FmainPassed.FileStatus.ipDoesExistResult = (long)StateIs.DoesNotExist;
                            FmainPassed.FileStatus.bpDoesExist = false;
                            FmainPassed.FileStatus.bpIsOpen = false;
                        }
                        if (!FmainPassed.FileStatus.bpDoesExist) {
                            LocalMessage.ErrorMsg = "File Name Does not exist in Sql File Dict Process Db";
                            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictProcessDbResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                        }
                    }
                }
                // 
                if (FmainPassed.DbStatus.bpIsConnected && FmainPassed.DbIo.SqlDbConn != null) {
                    // <Area Id = "Close connected">
                    if (PassedConnDoClose) {
                        SqlDictProcessDbResult = ConnClose(ref FmainPassed);
                        if (FmainPassed.RowInfoDb.RowCount > 0) {
                            SqlDictProcessDbResult = (long)StateIs.DoesExist;
                        }
                    }
                }
                if (FmainPassed.DbStatus.bpDoDispose && FmainPassed.DbIo.SqlDbConn != null) {
                    // <Area Id = "Dispose connected">
                    FmainPassed.DbIo.SqlDbConn = null;
                }
            } else {
                FmainPassed.FileStatus.ipDoesExistResult = (long)StateIs.Failed;
                FmainPassed.FileStatus.bpDoesExist = false;
                FmainPassed.FileStatus.bpIsOpen = false;
            }
            FmainPassed.FileStatus.ipStatusCurrent = FmainPassed.FileStatus.ipDoesExistResult;
            return SqlDictProcessDbResult;
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
        /// <summary> 
        /// Add an SQL Column Add command
        /// </summary> 
        /// <param name="PassedUsePrimary"></param> 
        /// <param name="PassedUseIndexName"></param> 
        /// <param name="iPassedColAction"></param> 
        /// <param name="SqlColumnBuildOptionPassed"></param> 
        /// <param name="iPassedIndex"></param> 
        /// <param name="iPassedCount"></param> 
        /// <remarks></remarks> 
        protected internal long SqlColAddCmdBuild(
            ref FileMainDef FmainPassed,
            bool PassedUsePrimary,
            bool PassedUseIndexName,
            int iPassedColAction,
            String SqlColumnBuildOptionPassed,
            int iPassedIndex,
            int iPassedCount
            ) {
            Meta.UsePrimary = PassedUsePrimary;
            //
            SqlColAddCmdBuildResult = (long)StateIs.Started;
            // command
            SqlColAddCmdBuildResult = (long)StateIs.InProgress;
            DbSyn.spSqlColumnAddCmd = "";
            String sSqlColumnBuildOption = SqlColumnBuildOptionPassed;
            PickRow.PickDictArray[iPassedIndex].ColAdd = "";
            PickRow.PickDictArray[iPassedIndex].ColView = "";
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
                        DbSyn.spOutputAlterColumnType = PickRow.PickDictArray[iPassedIndex].ColTypeWord;
                        if (DbSyn.spOutputAlterColumnType.Length == 0) { DbSyn.spOutputAlterColumnType = "VARCHAR"; }
                        DbSyn.spSqlColumnAddCmd += " " + DbSyn.spOutputAlterColumnType;
                        // Length
                        DbSyn.ipOutputAlterColumnLength = PickRow.PickDictArray[iPassedIndex].ColWidth;
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
                        PickRow.PickDictArray[iPassedIndex].ColAdd = DbSyn.spSqlColumnAddCmd;
                    } // is PK Primary Dictionary Column Definition (ie the PK for this column, not an alias)
                    // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                    // Create View
                    // View Item Column Id
                    DbSyn.spSqlColumnViewCmd = ""; // DbSyn.spOutputAlterVerb + " ";
                    //
                    if (PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType != ColTypeDef.ITEM_ISFUNCTION) {
                        DbSyn.spOutputAlterColumnNameSource = "c" + (PickRow.PickDictArray[iPassedIndex].ItemId + 0).ToString();
                        if (PickRow.PickDictArray[iPassedIndex].ItemIdFoundNumericPk) {
                            sTemp3 = SqlColConvertCharacters(PickRow.PickDictArray[PickRow.PdIndex].sHeading, Fmain.ColIndex.CharsPassedIn, Fmain.ColIndex.CharsPassedOut);
                            DbSyn.spOutputAlterColumnNameAlias = sTemp3;
                        } else {
                            DbSyn.spOutputAlterColumnNameAlias = PickRow.PickDictArray[iPassedIndex].ItemIdConverted;
                        }
                        try {
                            lTemp = Convert.ToInt64(DbSyn.spOutputAlterColumnNameAlias);
                            DbSyn.spOutputAlterColumnNameAlias = "c" + (lTemp + 0).ToString();
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
                            DbSyn.spOutputAlterColumnType = PickRow.PickDictArray[iPassedIndex].ColTypeWord;
                            if (DbSyn.spOutputAlterColumnType.Length == 0) { DbSyn.spOutputAlterColumnType = "VARCHAR"; }
                            DbSyn.spSqlColumnViewCmd += " " + DbSyn.spOutputAlterColumnType;
                            // Length
                            DbSyn.ipOutputAlterColumnLength = PickRow.PickDictArray[iPassedIndex].ColWidth;
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
                        PickRow.PickDictArray[iPassedIndex].ColView = DbSyn.spSqlColumnViewCmd;
                    } // (NOT) ISFUNCTION
                } // ISDICT
                // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                SqlColAddCmdBuildResult = (long)StateIs.Successful;
            } catch (SqlException ExceptionSql) {
                LocalMessage.ErrorMsg = "";
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, SqlColAddCmdBuildResult);
                SqlColAddCmdBuildResult = (long)StateIs.Failed;
                DbSyn.spSqlColumnAddCmd = "";
            } catch (Exception ExceptionGeneral) {
                LocalMessage.ErrorMsg = "";
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlColAddCmdBuildResult);
                SqlColAddCmdBuildResult = (long)StateIs.Failed;
                DbSyn.spSqlColumnAddCmd = "";
            } finally {
                if (true == false) {
                    FmainPassed.DbIo.SqlDbCommand = null;
                }
            }
            return SqlColAddCmdBuildResult;
        }
        // <Section Id = "x
        /// <summary> 
        /// Build an SQL Add Command for all Columns
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks></remarks> 
        protected internal long SqlColAddCmdBuildAll(
            ref FileMainDef FmainPassed,
            bool PassedUsePrimary,
            bool PassedUseIndexName,
            int iPassedColAction,
            String PassedIndexName,
            int iPassedIndex,
            int iPassedCount
            ) {
            SqlColAddCmdBuildAllResult = (long)StateIs.Started;
            FmainPassed.FileStatus.ipStatusCurrent = Faux.FileStatus.ipDoesExistResult;
            // Add Column Command Build by processing schema

            FmainPassed.DbIo.CommandCurrent = DbSyn.spOutputAlterCommand + " " + "'" + FmainPassed.Fs.TableNameLine + "'" + " ";

            return SqlColAddCmdBuildAllResult;
        }
        // <Section Id = "x
        /// <summary> 
        /// Do an SQL Column Action.
        /// </summary> 
        /// <param name="UseIndexNamePassed"></param> 
        /// <param name="ColActionPassed">The Action to perform. (i.e. Set_Row, Set_Column, Get_Native_Value...)</param> 
        /// <param name="ColRowIndexNamePassed"></param> 
        /// <param name="ColRowIndexPassed"></param> 
        /// <param name="ColRowIndexCountPassed"></param> 
        /// <remarks></remarks> 
        protected internal long SqlColAction(
            ref FileMainDef FmainPassed,
            // ref SqlDataReader FmainPassed.DbIo.SqlDbDataReader, ref SqlDataAdapter FmainPassed.DbIo.SqlDbDataWriter, ref RowInfoDef RowInfoPassed, ref ColTransformDef RowInfoPassed, 
             bool UseIndexNamePassed,
             int ColActionPassed,
             String ColRowIndexNamePassed,
             int ColRowIndexPassed,
             int ColRowIndexCountPassed
            ) {
            SqlColActionResult = (long)StateIs.Started;
            SqlDataReader x;
            // FmainPassed.ColTrans.ColAction = ColActionPassed;
            //
            if (UseIndexNamePassed) {
                switch (ColActionPassed) {
                    case (ColTransformDef.SFC_SET_ROW):
                        // Row
                        // REQUIRES A SELECT TO LOCATE THE ROW!
                        // ItemIdNext = FileIndexName;
                        // TODO z$NOTE This should involve an Auxiliary Get or
                        // make use of a Flag to indicate that either
                        // the Primary ItemData Item is being abandoned in
                        // favour of the new Get or that the Primary ItemData
                        // Item will be retained but an Auxiliary Item is
                        // needed for some protected internal reason.
                        FmainPassed.Item.ItemId = ColRowIndexNamePassed;
                        SqlColActionResult = SqlCommandSetDefault(ref FmainPassed);
                        // 
                        break;
                    case (ColTransformDef.SFC_SET_COLUMN):
                        // Column
                        FmainPassed.ColTrans.FileIndex = FmainPassed.DbIo.SqlDbDataReader.GetOrdinal(ColRowIndexNamePassed);
                        break;
                    default:
                        LocalMessage.ErrorMsg = "The Set Column ItemData Action has not been set";
                        FmainPassed.ColTrans.ColAction = (long)StateIs.Undefined;
                        throw new NotSupportedException(LocalMessage.ErrorMsg);
                }
            }
            // bool FileHasRows = PassedHasRows;
            int iHiddenCount = FmainPassed.ColTrans.ColCountHidden;
            //
            switch (ColActionPassed) {
                //
                case (ColTransformDef.SFC_SET_ROW):
                    // Row
                    FmainPassed.ColTrans.ColRowIndex = ColRowIndexPassed;
                    FmainPassed.ColTrans.ColRowIndexName = ColRowIndexNamePassed;
                    FmainPassed.ColTrans.ColRowCount = ColRowIndexCountPassed;
                    if (FmainPassed.ColTrans.ColRowCount > 0) {
                        FmainPassed.ColTrans.ColumnHasRows = true;
                    } else {
                        FmainPassed.ColTrans.ColumnHasRows = false;
                    }
                    break;
                case (ColTransformDef.SFC_SET_COLUMN):
                    // Column
                    FmainPassed.ColTrans.ColIndex = ColRowIndexPassed;
                    FmainPassed.ColTrans.ColIndexName = ColRowIndexNamePassed;
                    FmainPassed.ColTrans.ColCount = ColRowIndexCountPassed;
                    FmainPassed.ColTrans.ColCountVisible = ColRowIndexCountPassed;
                    FmainPassed.ColTrans.ColCountVisible = iHiddenCount;
                    break;
                case (ColTransformDef.SFC_GET_NATIVE_VALUE):
                    // Column
                    FmainPassed.ColTrans.ColIndex = ColRowIndexPassed;
                    FmainPassed.ColTrans.ColIndexName = ColRowIndexNamePassed;
                    FmainPassed.ColTrans.ColCount = ColRowIndexCountPassed;
                    FmainPassed.ColTrans.ColCountVisible = iHiddenCount;
                    // Get
                    FmainPassed.ColTrans.iGetIndex = ColRowIndexPassed;
                    FmainPassed.ColTrans.sGetResultToString = "No Result Available. ";
                    // FmainPassed.ColTrans.sGetResultToString = "";
                    //
                    if (FmainPassed.DbIo.SqlDbDataReader != null) {
                        x = FmainPassed.DbIo.SqlDbDataReader;
                        FmainPassed.ColTrans.sGetDataTypeName = x.GetDataTypeName(FmainPassed.ColTrans.iGetIndex);
                        try {
                            switch (FmainPassed.ColTrans.sGetDataTypeName) {
                                //
                                case ("bool"):
                                case ("bit"):
                                    FmainPassed.ColTrans.bGetBoolean = x.GetBoolean(FmainPassed.ColTrans.iGetIndex);
                                    FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.bGetBoolean.ToString();
                                    break;
                                case ("byte"):
                                case ("tinyint"):
                                    FmainPassed.ColTrans.bbGetByte = x.GetByte(FmainPassed.ColTrans.iGetIndex);
                                    FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.bbGetByte.ToString();
                                    break;
                                case ("char"):
                                    // FmainPassed.ColTrans.bcGetChar = x.GetChar(FmainPassed.ColTrans.iGetIndex);
                                    // FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.bcGetChar.ToString();
                                    break;
                                case ("DateTime"):
                                    FmainPassed.ColTrans.tdtGetDateTime = x.GetDateTime(FmainPassed.ColTrans.iGetIndex);
                                    FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.tdtGetDateTime.ToString();
                                    break;
                                case ("DateTimeOffset"):
                                    FmainPassed.ColTrans.tdtoGetDateTimeOffset = x.GetDateTimeOffset(FmainPassed.ColTrans.iGetIndex);
                                    FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.tdtoGetDateTimeOffset.ToString();
                                    break;
                                case ("decimal"):
                                    FmainPassed.ColTrans.deGetDecimal = x.GetDecimal(FmainPassed.ColTrans.iGetIndex);
                                    FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.deGetDecimal.ToString();
                                    break;
                                case ("double"):
                                    FmainPassed.ColTrans.doGetDouble = x.GetDouble(FmainPassed.ColTrans.iGetIndex);
                                    FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.doGetDouble.ToString();
                                    break;
                                case ("IEnumerator"):
                                    // IEnumerator<FmainPassed.ColTrans.lnGetEnumeratorT> = null;
                                    // sTemp1 = FmainPassed.ColTrans.lnGetEnumerator.ToString();
                                    break;
                                case ("float"):
                                    FmainPassed.ColTrans.fGetFloat = x.GetFloat(FmainPassed.ColTrans.iGetIndex);
                                    FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.fGetFloat.ToString();
                                    break;
                                case ("Guid"):
                                    FmainPassed.ColTrans.tgGetGuid = x.GetGuid(FmainPassed.ColTrans.iGetIndex);
                                    FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.tgGetGuid.ToString();
                                    // bbaTmp1 = FmainPassed.ColTrans.tgGetGuid.ToByteArray()'
                                    break;
                                case ("short"):
                                case ("int16"):
                                case ("smallint"):
                                    FmainPassed.ColTrans.isGetInt16 = x.GetInt16(FmainPassed.ColTrans.iGetIndex);
                                    FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.isGetInt16.ToString();
                                    break;
                                case ("int"):
                                case ("int32"):
                                    // "int" // "int32"
                                    FmainPassed.ColTrans.iGetInt32 = x.GetInt32(FmainPassed.ColTrans.iGetIndex);
                                    FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.iGetInt32.ToString();
                                    break;
                                case ("long"):
                                case ("int64"):
                                case ("bigint"):
                                    FmainPassed.ColTrans.ilGetInt64 = x.GetInt64(FmainPassed.ColTrans.iGetIndex);
                                    FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.ilGetInt64.ToString();
                                    break;
                                // case ("ushort"):
                                // FmainPassed.ColTrans.isuGetInt16 = (short) x.GetInt16(FmainPassed.ColTrans.iGetIndex);
                                // FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.isuGetInt16.ToString();
                                // break;
                                // case ("uint"):
                                // "int" // "int32"
                                // FmainPassed.ColTrans.iuGetInt32 = (int) (x.GetInt32(FmainPassed.ColTrans.iGetIndex);
                                // FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.iuGetInt32.ToString();
                                // break;
                                // case ("ulong"):
                                // FmainPassed.ColTrans.iluGetInt64 = (long) x.GetInt64(FmainPassed.ColTrans.iGetIndex);
                                // FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.iluGetInt64.ToString();
                                // break;
                                case ("string"):
                                    FmainPassed.ColTrans.sGetString = x.GetString(FmainPassed.ColTrans.iGetIndex);
                                    FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.sGetString;
                                    break;
                                case ("TimeSpan"):
                                    FmainPassed.ColTrans.tdtsGetTimeSpan = x.GetTimeSpan(FmainPassed.ColTrans.iGetIndex);
                                    FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.tdtsGetTimeSpan.ToString();
                                    break;
                                case ("varchar"):
                                    FmainPassed.ColTrans.sGetString = x.GetString(FmainPassed.ColTrans.iGetIndex);
                                    FmainPassed.ColTrans.sGetResultToString = FmainPassed.ColTrans.sGetString;
                                    break;
                                case ("ushort"):
                                case ("uint"):
                                case ("ulong"):
                                default:
                                    FmainPassed.ColTrans.sGetResultToString += "Type String Not Found! ";
                                    FmainPassed.ColTrans.sGetResultNotSupported += FmainPassed.ColTrans.sGetDataTypeName + ", ";
                                    LocalMessage.ErrorMsg = FmainPassed.ColTrans.sGetResultToString;
                                    throw new NotSupportedException(FmainPassed.ColTrans.sGetResultToString);
                            }
                            FmainPassed.ColTrans.sGetResultToString = "";
                        } catch (SqlException ExceptionSql) {
                            LocalMessage.ErrorMsg = "";
                            FmainPassed.ColTrans.sGetResultToString += "Sql Error in Get Type String! ";
                            ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, SqlColActionResult);
                            //
                            LocalMessage.ErrorMsg = FmainPassed.ColTrans.sGetResultToString;
                            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlColActionResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                            //
                        } catch (Exception ExceptionGeneral) {
                            LocalMessage.ErrorMsg = "";
                            FmainPassed.ColTrans.sGetResultToString += "General Error in Get Type String! ";
                            ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlColActionResult);
                            //
                            LocalMessage.ErrorMsg = FmainPassed.ColTrans.sGetResultToString;
                            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlColActionResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                        } finally {
                            //
                            FmainPassed.ColTrans.sGetResultToString += "Result Type String (" + FmainPassed.ColTrans.sGetDataTypeName + "). ";
                            FmainPassed.ColTrans.sGetResultToString += "Row: (" + FmainPassed.ColTrans.ColIndex.ToString() + "). ";
                            FmainPassed.ColTrans.sGetResultToString += "Column: ";
                            FmainPassed.ColTrans.sGetResultToString += FmainPassed.ColTrans.ColIndexName;
                            FmainPassed.ColTrans.sGetResultToString += " (" + FmainPassed.ColTrans.ColIndex.ToString() + ").";
                        }
                    } else {
                        FmainPassed.ColTrans.sGetResultToString += "No Result Available, Sql ItemData Reader is null!";
                        LocalMessage.ErrorMsg = FmainPassed.ColTrans.sGetResultToString;
                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlColActionResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                    }
                    break;
                case (ColTransformDef.SFC_GET_SQL_VALUE):
                    //
                    FmainPassed.ColTrans.iGetIndex = ColRowIndexPassed;
                    FmainPassed.ColTrans.sGetResultToString = "No Result Available";
                    //
                    if (FmainPassed.DbIo.SqlDbDataReader != null) {
                        x = FmainPassed.DbIo.SqlDbDataReader;
                        Faux.ColTrans.sGetDataTypeName = FmainPassed.DbIo.SqlDbDataReader.GetDataTypeName(Faux.ColTrans.ColIndex);

                        FmainPassed.ColTrans.sqlbiGetSqlBinary = (System.Data.SqlTypes.SqlBinary)null;
                        FmainPassed.ColTrans.sqlbGetSqlBoolean = false;
                        FmainPassed.ColTrans.sqlbbGetSqlByte = 0;
                        FmainPassed.ColTrans.sqliGetSqlBytes = (System.Data.SqlTypes.SqlBytes)null;
                        FmainPassed.ColTrans.sqliGetSqlChars = (System.Data.SqlTypes.SqlChars)null;
                        FmainPassed.ColTrans.sqltdtGetSqlDateTime = (System.Data.SqlTypes.SqlDateTime.MaxValue);
                        FmainPassed.ColTrans.sqlfdGetSqlDecimal = 0;
                        FmainPassed.ColTrans.fdGetSqlDouble = 0;
                        FmainPassed.ColTrans.tgGetSqlGuid = (System.Data.SqlTypes.SqlGuid.Null);
                        FmainPassed.ColTrans.isGetSqlInt16 = 0;
                        FmainPassed.ColTrans.iGetSqlInt32 = 0;
                        FmainPassed.ColTrans.ilGetSqlInt64 = 0;
                        FmainPassed.ColTrans.fdGetSqlMoney = 0;
                        FmainPassed.ColTrans.fGetSqlSingle = 0;
                        FmainPassed.ColTrans.sGetSqlString = "";
                        FmainPassed.ColTrans.ooGetSqlValue = 0;
                        FmainPassed.ColTrans.iGetSqlValues = 0;
                        FmainPassed.ColTrans.ooGetSqlXml = (System.Data.SqlTypes.SqlXml.Null);
                    }

                    break;
                case (ColTransformDef.SFC_RESET):
                    // Row
                    FmainPassed.ColTrans.ColRowIndex = 0;
                    FmainPassed.ColTrans.ColRowIndexName = "";
                    FmainPassed.ColTrans.ColRowCount = 0;
                    FmainPassed.ColTrans.ColumnHasRows = false;
                    // Column
                    FmainPassed.ColTrans.ColIndex = 0;
                    FmainPassed.ColTrans.ColIndexName = "";
                    FmainPassed.ColTrans.ColCount = 0;
                    FmainPassed.ColTrans.ColCountVisible = 0;
                    // Sql ItemData Client
                    FmainPassed.ColTrans.bGetBoolean = false;
                    FmainPassed.ColTrans.bbGetByte = 0;
                    FmainPassed.ColTrans.loGetBytes = 0;
                    FmainPassed.ColTrans.bcGetChar = (char)0;
                    FmainPassed.ColTrans.loGetChars = 0;
                    FmainPassed.ColTrans.sGetDataTypeName = "";
                    FmainPassed.ColTrans.tdtGetDateTime = (DateTime)System.DateTime.Now;
                    FmainPassed.ColTrans.tdtoGetDateTimeOffset = DateTimeOffset.MinValue;
                    FmainPassed.ColTrans.deGetDecimal = 0;
                    FmainPassed.ColTrans.doGetDouble = 0;
                    // IEnumerator<FmainPassed.ColTrans.lnGetEnumeratorT> = null;
                    FmainPassed.ColTrans.ttGetFieldType = (System.Type)null;
                    FmainPassed.ColTrans.fGetFloat = 0;
                    FmainPassed.ColTrans.tgGetGuid = (System.Guid.Empty);
                    FmainPassed.ColTrans.isGetInt16 = 0;
                    FmainPassed.ColTrans.iGetInt32 = 0;
                    FmainPassed.ColTrans.ilGetInt64 = 0;
                    FmainPassed.ColTrans.iGetName = "";
                    FmainPassed.ColTrans.iGetOrdinal = 0;
                    FmainPassed.ColTrans.ttGetProviderSpecificFieldType = (System.Type)null;
                    FmainPassed.ColTrans.ooGetProviderSpecificValue = 0;
                    FmainPassed.ColTrans.iGetProviderSpecificValues = 0;
                    FmainPassed.ColTrans.tfdtGetSchemaTable = (System.Data.DataTable)null;
                    FmainPassed.ColTrans.sqlbiGetSqlBinary = (System.Data.SqlTypes.SqlBinary)null;
                    FmainPassed.ColTrans.sqlbGetSqlBoolean = false;
                    FmainPassed.ColTrans.sqlbbGetSqlByte = 0;
                    FmainPassed.ColTrans.sqliGetSqlBytes = (System.Data.SqlTypes.SqlBytes)null;
                    FmainPassed.ColTrans.sqliGetSqlChars = (System.Data.SqlTypes.SqlChars)null;
                    FmainPassed.ColTrans.sqltdtGetSqlDateTime = (System.Data.SqlTypes.SqlDateTime.MaxValue);
                    FmainPassed.ColTrans.sqlfdGetSqlDecimal = 0;
                    FmainPassed.ColTrans.fdGetSqlDouble = 0;
                    FmainPassed.ColTrans.tgGetSqlGuid = (System.Data.SqlTypes.SqlGuid.Null);
                    FmainPassed.ColTrans.isGetSqlInt16 = 0;
                    FmainPassed.ColTrans.iGetSqlInt32 = 0;
                    FmainPassed.ColTrans.ilGetSqlInt64 = 0;
                    FmainPassed.ColTrans.fdGetSqlMoney = 0;
                    FmainPassed.ColTrans.fGetSqlSingle = 0;
                    FmainPassed.ColTrans.sGetSqlString = "";
                    FmainPassed.ColTrans.ooGetSqlValue = 0;
                    FmainPassed.ColTrans.iGetSqlValues = 0;
                    FmainPassed.ColTrans.ooGetSqlXml = (System.Data.SqlTypes.SqlXml.Null);
                    FmainPassed.ColTrans.sGetString = "";
                    FmainPassed.ColTrans.tdtsGetTimeSpan = (TimeSpan.Zero);
                    FmainPassed.ColTrans.ooGetValue = 0;
                    FmainPassed.ColTrans.iGetValues = 0;
                    FmainPassed.ColTrans.bICommandBehavior = false;
                    FmainPassed.ColTrans.bIsDBNull = false;
                    FmainPassed.ColTrans.bNextResult = false;
                    FmainPassed.ColTrans.bRead = false;
                    break;
                default:
                    // no action
                    LocalMessage.ErrorMsg = "The Set Column ItemData Action has not been set";
                    FmainPassed.ColTrans.ColAction = (long)StateIs.Undefined;
                    throw new NotSupportedException(LocalMessage.Msg);
            }
            //
            return SqlColActionResult;
        }
        #endregion
        #region SqlDictionaryColumnConversion
        ///*
        /// <summary> 
        /// Character converter, s/b obsolete.
        /// </summary> 
        /// <param name="PassedField"></param> 
        /// <param name="PassedCharsIn"></param> 
        /// <param name="PassedCharsOut"></param> 
        /// <remarks>obsolete</remarks> 
        public String SqlColConvertCharacters(String PassedField, char[] PassedCharsIn, char[] PassedCharsOut) {
            SqlColConvertCharactersResult = (long)StateIs.Started;
            String sFieldOut = PassedField;
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
            // return SqlColAddCmdBuildAllFromArrayResult;
            return sFieldOut;
        }
        #endregion
        #region SqlDictUpdate
        #region SqlDictArrayInsert
        public String SqlDictArrayDesc;
        protected internal bool bIsAnError = true;
        protected internal bool bIsNotAnError = false;
        protected internal bool bDoDecrementIndex = true;
        protected internal bool bDoNotDecrementIndex = false;
        protected internal IndexOutOfRangeException ExceptIndexOutOfRange = null;
        protected internal FormatException ExceptFormat = null;
        protected internal Exception ExceptGeneral = null;
        /// <summary> 
        /// Exception handling for Dict Arrays.
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks></remarks> 
        public void SqlDictArrayIndexError(int PassedMinVerbosity, ref FormatException oeFormatException, ref IndexOutOfRangeException oeIndexOutOfRangeException, String PassedSqlDictArrayDesc, bool PassedIsError, bool PassedReduceIndex) {
            // non numeric item id / column 0
            // normal, not an error here
            if (PassedReduceIndex) { PickRow.PdIndexTemp = -1; }
            if (PassedIsError) {
                PickRow.PdErrorCount += 1;
            } else {
                PickRow.PdErrorWarningCount += 1;
            }
            if (ConsoleVerbosity >= PassedMinVerbosity) {
                LocalMessage.ErrorMsg = PassedSqlDictArrayDesc;
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
            }
        }
        // <Section Id = "SqlDictInsert">
        /// <summary> 
        /// Insert into the Sql Dict Array
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks></remarks> 
        public long SqlDictArrayInsert(ref FileMainDef FmainPassed, String OutputCommandPassed) {
            SqlDictInsertResult = (long)StateIs.Started;
            DbSyn.spOutputCommand = OutputCommandPassed;
            FmainPassed.DbIo.CommandCurrent = OutputCommandPassed;
            SqlDictInsertResult = (long)StateIs.InProgress;
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
            SqlDictInsertResult = (long)StateIs.InProgress;
            //
            // Preliminary Validation
            //
            // Length of item
            if (FmainPassed.Item.ItemData.Length < 12) { PickRow.ColumnDataPoints += 300; }
            if (FmainPassed.Item.ItemData.Length < 15) { PickRow.ColumnDataPoints += 100; }
            if (FmainPassed.Item.ItemData.Length < 20) { PickRow.ColumnDataPoints += 50; }
            if (FmainPassed.Item.ItemData.Length < 25) { PickRow.ColumnDataPoints += 25; }
            if (FmainPassed.Item.ItemData.Length > 100) { PickRow.ColumnDataPoints += 10; }
            if (FmainPassed.Item.ItemData.Length > 30) { PickRow.ColumnDataPoints -= 30; }
            if (PickRow.ColumnDataPoints > 50) {
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
            }
            //
            // Column reference
            try {
                PickRow.AttrTwoInt = -2;
                PickRow.AttrTwoIsNumeric = false;
                PickRow.AttrTwoString = (String)FmainPassed.ColIndex.ColArray[2];
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
                LocalMessage.ErrorMsg = "Non-numeric Attr Two. Item is ItemData.";
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                //
            } catch (IndexOutOfRangeException e) {
                PickRow.AttrTwoInt = -1;
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                PickRow.AttrTwoIsNumeric = false;
                PickRow.PdErrorCount += 1;
                LocalMessage.ErrorMsg = "Abnormal Index Error referencing numeric Attr Two. Index out of range!!! Item will be treated as ItemData.";
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                //
            } catch (Exception ExceptionGeneral) {
                // column 2 is not numeric
                PickRow.AttrTwoInt = -1;
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                PickRow.AttrTwoIsNumeric = false;
                PickRow.PdErrorCount += 1;
                LocalMessage.ErrorMsg = "";
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlDictInsertResult);
                //
                LocalMessage.ErrorMsg = "Abnormal OS Error referencing numeric Attr Two, Os error occured!!! Item will be treated as ItemData.";
                LocalMessage.ErrorMsg = ExceptionGeneral.Message;
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");

            } catch { // warning, not reachable
                // column 2 is not numeric
                PickRow.AttrTwoInt = -1;
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                PickRow.AttrTwoIsNumeric = false;
                PickRow.PdErrorCount += 1;
                LocalMessage.ErrorMsg = "Abnormal and Unknown Error referencing numeric Attr Two, Undefined error occured!!! Item will be treated as ItemData.";
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
            }
            //
            // Column Width
            try {
                PickRow.ColumnWidth = -2;
                PickRow.ColumnWidthIsNumeric = false;
                PickRow.ColumnWidthString = (String)FmainPassed.ColIndex.ColArray[10];
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
                LocalMessage.ErrorMsg = "Non-numeric Column Width. Item is ItemData.";
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
            } catch (IndexOutOfRangeException e) {
                PickRow.PdIndexTemp = -1;
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                PickRow.ColumnWidthIsNumeric = false;
                PickRow.PdErrorCount += 1;
                LocalMessage.ErrorMsg = "Abnormal Index Error referencing numeric Column Width. Index out of range!!! Item will be treated as ItemData.";
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                //
            } catch (Exception ExceptionGeneral) {
                // column 2 is not numeric
                PickRow.PdIndexTemp = -1;
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                PickRow.ColumnWidthIsNumeric = false;
                PickRow.PdErrorCount += 1;
                LocalMessage.ErrorMsg = "Abnormal OS Error referencing numeric Column Width, Os error occured!!! Item will be treated as ItemData.";
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlDictInsertResult);
                //
            } catch {
                // column 2 is not numeric
                PickRow.PdIndexTemp = -1;
                PickRow.iAttrType = ColTypeDef.ITEM_ISDATA;
                PickRow.ColumnWidthIsNumeric = false;
                PickRow.PdErrorCount += 1;
                LocalMessage.ErrorMsg = "Abnormal and Unknown Error referencing numeric Column Width, Undefined error occured!!! Item will be treated as ItemData.";
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
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
                    PickRow.ItemId = (String)FmainPassed.ColIndex.ColArray[0];
                    if (ConsoleVerbosity >= 5) {
                        // XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + ItemIntId + ": " + ColArray[3] + " " + ColArray[2]);
                        XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + FmainPassed.Item.ItemId + ": " + FmainPassed.ColIndex.ColArray[3] + " " + FmainPassed.ColIndex.ColArray[2]);
                    }
                    PickRow.PdIndexTemp = Convert.ToInt32(FmainPassed.ColIndex.ColArray[0]);
                    // Item Id is numeric
                    if (PickRow.PdIndexTemp < 0 || PickRow.PdIndexTemp > PickRowDef.PdIndexMax) {
                        // numeric item id is out of range
                        ExceptIndexOutOfRange = null;
                        ExceptFormat = null;
                        SqlDictArrayDesc = "Id's numeric Item Id reference reference is out or allowed range." + " Index is not numeric and is a name!!! Will place this amoung aliases!";
                        ;
                        SqlDictArrayIndexError(5, ref ExceptFormat, ref ExceptIndexOutOfRange, SqlDictArrayDesc, bIsNotAnError, bDoDecrementIndex);
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
                    ExceptIndexOutOfRange = null;
                    SqlDictArrayDesc = "Index Error referencing numeric Item Id reference identity." + " Index is not numeric and is a name!!! Will place this amoung aliases!";
                    ;
                    SqlDictArrayIndexError(5, ref oeFormatException, ref ExceptIndexOutOfRange, SqlDictArrayDesc, bIsAnError, bDoDecrementIndex);
                } catch (IndexOutOfRangeException oeIndexOutOfRangeException) {
                    ExceptFormat = null;
                    SqlDictArrayDesc = "Index Error referencing numeric Item Id reference identity." + " Index is not numeric and is a name!!! Will place this amoung aliases!";
                    ;
                    SqlDictArrayIndexError(5, ref ExceptFormat, ref oeIndexOutOfRangeException, SqlDictArrayDesc, bIsAnError, bDoDecrementIndex);
                } catch (Exception ExceptionGeneral) {
                    // column 2 is not numeric
                    ExceptIndexOutOfRange = null;
                    ExceptFormat = null;
                    SqlDictArrayDesc = ExceptionGeneral.Message;
                    LocalMessage.ErrorMsg = ExceptionGeneral.Message;
                    ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlDictInsertResult);
                    SqlDictArrayIndexError(5, ref ExceptFormat, ref ExceptIndexOutOfRange, SqlDictArrayDesc, bIsAnError, bDoDecrementIndex);
                    //
                    SqlDictArrayDesc = "OS Error referencing numeric Item Id reference identity, Os error occured!!!" + " Index is not numeric and is a name!!! Will place this amoung aliases!";
                    ;
                    SqlDictArrayIndexError(5, ref ExceptFormat, ref ExceptIndexOutOfRange, SqlDictArrayDesc, bIsNotAnError, bDoNotDecrementIndex);
                } catch {
                    // column 2 is not numeric
                    ExceptIndexOutOfRange = null;
                    ExceptFormat = null;
                    SqlDictArrayDesc = "Unknown Error referencing numeric Item Id reference identity, Undefined error occured!!!" + " Index is not numeric and is a name!!! Will place this amoung aliases!";
                    SqlDictArrayIndexError(5, ref ExceptFormat, ref ExceptIndexOutOfRange, SqlDictArrayDesc, bIsAnError, bDoDecrementIndex);
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
                    PickRow.PdIndexTemp = Convert.ToInt32(FmainPassed.ColIndex.ColArray[2]);
                    // x
                    if (PickRow.PdIndexTemp < 0 || PickRow.PdIndexTemp > PickRowDef.PdIndexMax) {
                        // numeric column is out of range
                        ExceptIndexOutOfRange = null;
                        ExceptFormat = null;
                        SqlDictArrayDesc = "Dictionary numeric Column Number reference is out or allowed range.";
                        SqlDictArrayIndexError(1, ref ExceptFormat, ref ExceptIndexOutOfRange, SqlDictArrayDesc, bIsNotAnError, bDoDecrementIndex);
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
                                    // this RsIndex is initialized
                                    // if the ItemId is not NULL
                                }
                            } catch (IndexOutOfRangeException oeIndexOutOfRangeException) {
                                ExceptFormat = null;
                                SqlDictArrayDesc = "Index Error validating if dictionary core definition need initialization.";
                                SqlDictArrayIndexError(1, ref ExceptFormat, ref oeIndexOutOfRangeException, SqlDictArrayDesc, bIsAnError, bDoDecrementIndex);
                            } catch {
                                // initialized this RsIndex location
                                ExceptIndexOutOfRange = null;
                                ExceptFormat = null;
                                SqlDictArrayDesc = "Warning, Dictionary core definition reference need to be initialized.";
                                SqlDictArrayIndexError(1, ref ExceptFormat, ref ExceptIndexOutOfRange, SqlDictArrayDesc, bIsNotAnError, bDoNotDecrementIndex);
                                //
                                SqlDictInsertResult = SqlColClear(PickDictItem.PdIndexAttrTwo);
                            } finally {
                                if (PickDictItem.PdIndexAttrTwo >= PickRow.PdIndexAliasLow) {
                                    // initialized this RsIndex location
                                    // need to erase current value and replace it.
                                    //
                                    if (PickRow.ItemIdFoundNumericPk) {
                                        ExceptIndexOutOfRange = null;
                                        ExceptFormat = null;
                                        SqlDictArrayDesc = "Maximum # of Aliases and Columns exceeded. Index overflow / crossover!!! Will overwrite alias at this location with Dictionary core definition reference!!!";
                                        SqlDictArrayIndexError(1, ref ExceptFormat, ref ExceptIndexOutOfRange, SqlDictArrayDesc, bIsAnError, bDoNotDecrementIndex);
                                        //
                                        if (!PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo].ItemIdFoundNumericPk) {
                                            SqlDictInsertResult = SqlColClear(PickDictItem.PdIndexAttrTwo);
                                        }
                                    } else {
                                        PickRow.PdIndexTemp = PickRow.PdIndexHigh + 1;
                                        ExceptIndexOutOfRange = null;
                                        ExceptFormat = null;
                                        SqlDictArrayDesc = "Maximum # of Aliases and Columns exceeded. Index overflow / crossover!!! Will overwrite an alias location already processed!!!";
                                        SqlDictArrayIndexError(1, ref ExceptFormat, ref ExceptIndexOutOfRange, SqlDictArrayDesc, bIsAnError, bDoNotDecrementIndex);
                                    }
                                }
                                PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo].ColTouched += 1;
                                PickRow.DictColumnTouched = PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo].ColTouched;
                            }
                        }
                        // x
                        if (PickRow.ItemIdFoundNumericPk && PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo].ItemIdFoundNumericPk) {
                            // PK already exists and was found previously
                            // search for an empty slot for the new item.
                            // Can not replace the existing PK with a new one.
                            // duplicate PK dict definitions should not be possible
                            // reject the newest one for a PK candidate
                            PickRow.PdErrorCount += 1;
                            LocalMessage.ErrorMsg = "Duplicate dictionary core definition!!!";
                            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
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
                            LocalMessage.ErrorMsg = "Dictionary core definition exists.";
                            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                        }
                    }
                    // eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
                } catch (IndexOutOfRangeException e) {
                    PickRow.PdIndexTemp = -1;
                    PickRow.PdErrorWarningCount += 1;
                    LocalMessage.ErrorMsg = "Error referencing dictionary core definition, Index error, out of range!!!";
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                    //
                } catch (FormatException oeMexceptCmdFormatException) {
                    PickRow.PdIndexTemp = -1;
                    PickRow.PdErrorWarningCount += 1;
                    LocalMessage.ErrorMsg = "Error referencing dictionary core definition, Core reference is not a valid number!!!";
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                    //
                } catch (Exception ExceptionGeneral) {
                    // column 2 is not numeric
                    PickRow.PdIndexTemp = -1;
                    PickRow.PdErrorCount += 1;
                    LocalMessage.ErrorMsg = ExceptionGeneral.Message;
                    ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlDictInsertResult);
                    //
                    LocalMessage.ErrorMsg = "Error referencing dictionary core definition, Os error occured!!!";
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                } catch {
                    // column 2 is not numeric
                    PickRow.PdIndexTemp = -1;
                    PickRow.PdErrorCount += 1;
                    LocalMessage.ErrorMsg = "Error referencing dictionary core definition, Undefined error occured!!!";
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                }
                //
                // invalid column 2 column number
                if (PickRow.PdIndexTemp < 0) {
                    PickRow.ItemIdFoundNumericPk = false;
                    //
                    PickRow.PdIndexDoSearch = true;
                    PickRow.ColumnDataPoints += 20;
                    //
                    PickRow.AttrTwoStringAccounName = (String)FmainPassed.ColIndex.ColArray[2];
                    PickRow.AttrThreeFileName = (String)FmainPassed.ColIndex.ColArray[3];
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
                                    // this RsIndex is initialized
                                    // if the ItemId is not NULL
                                }
                            } catch {
                                // initialized this RsIndex location
                                SqlDictInsertResult = SqlColClear(PickRow.PdIndex);
                            }
                            if (PickRow.PickDictArray[PickRow.PdIndexTemp].ColTouched == 0) {
                                PickRow.PdIndexAliasLow = PickRow.PdIndexTemp;
                                break;
                            }
                        }
                        if (PickRow.PdIndexTemp < 0) {
                            // Looped downward below the start of the RsIndex
                            PickRow.PdErrorCount += 1;
                            LocalMessage.ErrorMsg = @"Maximum # of Aliases and Columns exceeded. Dictionary Work File full, Maximum # of entries exceeded. Index below low range!!!";
                            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                            //
                            PickRow.PdIndexTemp = PickRow.PdIndexAliasLow - 1;
                            if (PickRow.PdIndexTemp < 0 || PickRow.PdIndexTemp > PickRowDef.PdIndexMax) {
                                // numeric column is out of range
                                PickRow.PdIndexTemp = -1;
                                PickRow.PdErrorCount += 1;
                                LocalMessage.ErrorMsg = @"Maximum # of Aliases and Columns exceeded. Index overflow / crossover !!! Maximum # of entries exceeded. ";
                                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                            }
                        }
                    } catch (IndexOutOfRangeException e) {
                        PickRow.PdIndexTemp = -1;
                        PickRow.PdErrorCount += 1;
                        LocalMessage.ErrorMsg = "Dictionary Work File full, Maximum # of entries exceeded. Index out of range!!!";
                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                    } catch {
                        PickRow.PdIndexTemp = -1;
                        PickRow.PdErrorCount += 1;
                        LocalMessage.ErrorMsg = "Unknown Error during Dictionary search!!!";
                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PickSystemCallStringResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                    }
                    //
                    if (PickRow.PdIndexTemp != -1 && PickRow.PdIndexTemp <= PickRow.PdIndexHigh) {
                        // Index Error
                        PickRow.PdErrorCount += 1;
                        LocalMessage.ErrorMsg = "Index out of range!!! Alias not allowed to overwrite Dictionary core definition. Attempting to use default location.";
                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                        //
                        if (PickRow.PdIndexHigh > PickRowDef.PdIndexMax) {
                            PickRow.PdIndexTemp = PickRowDef.PdIndexMax;
                        } else {
                            PickRow.PdIndexTemp = PickRow.PdIndexHigh + 1;
                        }
                        PickRow.PdIndex = PickRow.PdIndexTemp;
                        PickRow.PickDictArray[PickRow.PdIndex].ColInvalid += 1;
                    } else if (PickRow.PdIndexTemp != -1) {
                        // RsIndex within low range and file not full
                        // alias will be placed in this position
                        PickRow.PdIndex = PickRow.PdIndexTemp;
                        PickRow.PdIndexAliasLow = PickRow.PdIndex;

                        PickRow.PickDictArray[PickRow.PdIndex].ItemIdFoundNumericPk = false;

                        PickRow.PickDictArray[PickRow.PdIndex].ColTouched += PickRow.DictColumnTouched;
                        PickRow.PickDictArray[PickRow.PdIndex].ColIdDone = false;
                        PickRow.PickDictArray[PickRow.PdIndex].ColLength = 0;
                        PickRow.PickDictArray[PickRow.PdIndex].ColLengthChange = false;
                        PickRow.ColumnDataPoints -= 50;
                    }
                    // end of do search
                    // Attr Two was valid, set it here
                } else if (PickRow.ItemIdFoundNumericPk) {
                    //
                    if (PickRow.PdIndexTemp > PickRowDef.PdIndexMax) {
                        PickRow.PdErrorCount += 1;
                        LocalMessage.ErrorMsg = "Column number too high, Maximum # of Columns exceeded. Index out of range!!!";
                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                        PickRow.PdIndexTemp = PickRowDef.PdIndexMax;
                        PickRow.PickDictArray[PickRow.PdIndexTemp].ColInvalid += 1;
                    }
                    //
                    if (PickRow.PdIndexTemp > PickRow.PdIndexAliasLow) {
                        PickRow.PdErrorCount += 1;
                        LocalMessage.ErrorMsg = @"Maximum # of Aliases and Columns exceeded. Dictionary core definition overwriting alias!!!";
                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                        PickRow.PdIndexHigh = PickRow.PdIndexTemp;
                        PickRow.PickDictArray[PickRow.PdIndexTemp].ColInvalid += 1;
                    }
                    //
                    if (PickRow.PdIndexAliasLow <= PickRow.PdIndexHigh) {
                        PickRow.PdErrorCount += 1;
                        LocalMessage.ErrorMsg = @"Maximum # of Aliases and Columns exceeded. Index overflow / crossover !!!";
                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");

                        PickRow.PickDictArray[PickRow.PdIndexTemp].ColInvalid += 1;
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
                PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints = PickRow.ColumnDataPoints;
                // ColArray[0] = PickRow.PickDictArray[PickRow.PdIndex].ItemId;
                PickRow.PickDictArray[PickRow.PdIndex].ItemId = (String)FmainPassed.ColIndex.ColArray[0];
                PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber = (String)FmainPassed.ColIndex.ColArray[2];
                PickRow.PickDictArray[PickRow.PdIndex].ItemIdConverted = PickRow.PickDictArray[PickRow.PdIndex].ItemId;
                sTemp3 = SqlColConvertCharacters(PickRow.PickDictArray[PickRow.PdIndex].ItemId, FmainPassed.ColIndex.CharsPassedIn, FmainPassed.ColIndex.CharsPassedOut);
                PickRow.PickDictArray[PickRow.PdIndex].ItemIdConverted = sTemp3;
                //
                // Convert numericStr to UInt32 without a format provider.
                //
                //`TODO create a SqlColClear
                //
                try {
                    PickRow.PickDictArray[PickRow.PdIndex].ColIndex = Convert.ToInt32(PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber);
                } catch { ; }
                //
                if (PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber.Length > 2) {
                    PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints -= 10;
                } else if (PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber.Length > 3) {
                    PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints -= 15;
                } else if (PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber.Length > 10) {
                    PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints -= 30;
                } else if (PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber.Length > 30) {
                    PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints -= 50;
                } else {
                    PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 10;
                }
                //
                // Analyze and Convert Dictionary Item
                //
                PickRow.PickDictArray[PickRow.PdIndex].ColIndex = PickDictItem.PdIndexAttrTwo;
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
                    SqlDictInsertResult = (long)StateIs.Invalid;
                } else {
                    // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                    //
                    // ColArray[Am] = ColText;
                    //
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 0 - ID 
                    SqlDictInsertResult = (long)StateIs.InProgress;
                    //
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 1 - Attr Type (A=Attr, Dx=File, Q=FileName Alias, S=??simlar to A??)
                    PickRow.PickDictArray[PickRow.PdIndex].sAttrType = (String)FmainPassed.ColIndex.ColArray[1];
                    if (PickRow.PickDictArray[PickRow.PdIndex].sAttrType.Length > 0) {
                        PickRow.PickDictArray[PickRow.PdIndex].sType = PickRow.PickDictArray[PickRow.PdIndex].sAttrType.Substring(0, 1);
                        if (PickRow.PickDictArray[PickRow.PdIndex].sAttrType.Length > 1) {
                            PickRow.PickDictArray[PickRow.PdIndex].sSubType = PickRow.PickDictArray[PickRow.PdIndex].sAttrType.Substring(1);
                            PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 10; // 10
                            if (PickRow.PickDictArray[PickRow.PdIndex].sAttrType.Length > 2) {
                                PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 10; // 20
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
                    PickRow.PickDictArray[PickRow.PdIndex].sHeading = (String)FmainPassed.ColIndex.ColArray[3];
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 4 - Dependant Upon Controlling Attrubute 999
                    PickRow.PickDictArray[PickRow.PdIndex].sDependancy = (String)FmainPassed.ColIndex.ColArray[4];
                    //     D30
                    //   - Controlling Attr list of Dependant Attrs
                    //     C;29;31;32;33;34;35;36;37;38;39;50;41;42;43;44;45
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 5 -
                    PickRow.PickDictArray[PickRow.PdIndex].sFive = (String)FmainPassed.ColIndex.ColArray[5];
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 6 - Input Conversion
                    PickRow.PickDictArray[PickRow.PdIndex].InputConversion = (String)FmainPassed.ColIndex.ColArray[6];
                    // Examples
                    // MR0 
                    // MR2
                    // MR2ZM (M=Mask R=RightJustify 2=2DecimalPlaces Z=FillZerosWithSpaces, M=??)
                    // D2- (D=Date 2=2DigitYear "-"=FormatSeparationCharacter)
                    if (PickRow.PickDictArray[PickRow.PdIndex].InputConversion.Length > 0) {
                        PickRow.PickDictArray[PickRow.PdIndex].InputConvType = PickRow.PickDictArray[PickRow.PdIndex].InputConversion.Substring(0, 1);
                        if (PickRow.PickDictArray[PickRow.PdIndex].InputConversion.Length > 1) {
                            PickRow.PickDictArray[PickRow.PdIndex].InputConvSubType = PickRow.PickDictArray[PickRow.PdIndex].InputConversion.Substring(1);
                            // PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 10;
                            if (PickRow.PickDictArray[PickRow.PdIndex].InputConversion.Length > 5) {
                                PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 10; // 50
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
                    PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion = (String)FmainPassed.ColIndex.ColArray[7];
                    // Examples
                    // MR0 
                    // MR2
                    // MR2ZM (M=Mask R=RightJustify 2=2DecimalPlaces Z=FillZerosWithSpaces, M=??)
                    // D2- (D=Date 2=2DigitYear "-"=FormatSeparationCharacter)
                    if (PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion.Length > 0) {
                        PickRow.PickDictArray[PickRow.PdIndex].spOutputConvType = PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion.Substring(0, 1);
                        if (PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion.Length > 1) {
                            PickRow.PickDictArray[PickRow.PdIndex].spOutputConvSubType = PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion.Substring(1);
                            // PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 10;
                            if (PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion.Length > 10) {
                                PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 10; // 60
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
                    PickRow.PickDictArray[PickRow.PdIndex].sCorrelative = (String)FmainPassed.ColIndex.ColArray[8];
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
                            // PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 10;
                            if (PickRow.PickDictArray[PickRow.PdIndex].sCorrelative.Length > 10) {
                                PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 10; // 70
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
                    PickRow.PickDictArray[PickRow.PdIndex].sJustify = (String)FmainPassed.ColIndex.ColArray[9];
                    if (PickRow.PickDictArray[PickRow.PdIndex].sJustify.Length > 0) {
                        PickRow.PickDictArray[PickRow.PdIndex].sJustification = PickRow.PickDictArray[PickRow.PdIndex].sJustify.Substring(0, 1);
                        if (PickRow.PickDictArray[PickRow.PdIndex].sJustification != "L" && PickRow.PickDictArray[PickRow.PdIndex].sJustification != "R") {
                            PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 30; // 100
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
                    PickRow.PickDictArray[PickRow.PdIndex].ColWidthString = (String)FmainPassed.ColIndex.ColArray[10];
                    PickRow.PickDictArray[PickRow.PdIndex].ColWidth = 0;
                    PickRow.PickDictArray[PickRow.PdIndex].ColWidthIsNumeric = false;
                    // Convert numericStr to UInt32 without a format provider.
                    SqlDictInsertResult = (long)StateIs.InProgress;
                    try {
                        PickRow.PickDictArray[PickRow.PdIndex].ColWidth = Convert.ToInt32(PickRow.PickDictArray[PickRow.PdIndex].ColWidthString);
                        PickRow.PickDictArray[PickRow.PdIndex].ColWidthIsNumeric = true;
                    } catch (Exception ExceptionGeneral) {
                        PickRow.PickDictArray[PickRow.PdIndex].ColWidth = 0;
                        PickRow.PickDictArray[PickRow.PdIndex].ColWidthIsNumeric = false;
                        PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 50; // 150
                        LocalMessage.ErrorMsg = ExceptionGeneral.Message;
                        ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlDictInsertResult);
                    }
                    if (PickRow.PickDictArray[PickRow.PdIndex].ColWidth > 40) {
                        // stop here
                        PickRow.PickDictArray[PickRow.PdIndex].ColWidthString += "BIG ONE";
                        iIterationCount = 99999;
                        PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 10;  // 160
                    }
                    if (iIterationCount >= 10) {
                        iIterationCount = 0;
                    }
                    //
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 11 - HeadingLong
                    PickRow.PickDictArray[PickRow.PdIndex].sHeadingLong = (String)FmainPassed.ColIndex.ColArray[11];
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 12 - 
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // 13 - HelpShort
                    PickRow.PickDictArray[PickRow.PdIndex].sHelpShort = (String)FmainPassed.ColIndex.ColArray[13];
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
                    PickRow.PickDictArray[PickRow.PdIndex].sRevColumnName = (String)FmainPassed.ColIndex.ColArray[19];
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
                        PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 10;  // 170
                        if (PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter > 30) {
                            PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 100;  // 170
                        }
                    }
                    // xxxxxxxxxxxxxxxxxxxxxxx
                    // Column Type
                    if (PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter > 30 || PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints > 100) {
                        PickRow.PickDictArray[PickRow.PdIndex].iAttrType = ColTypeDef.ITEM_ISDATA;
                        PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType = ColTypeDef.ITEM_ISNOTSET;
                        PickRow.PickDictArray[PickRow.PdIndex].bAttrIsDict = false;
                        PickRow.PickDictArray[PickRow.PdIndex].bAttrIsData = true;
                        PickRow.ColumnInvalid = true;
                    } else {
                        PickRow.PickDictArray[PickRow.PdIndex].iAttrType = ColTypeDef.ITEM_ISNOTSET;
                        PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType = ColTypeDef.ITEM_ISNOTSET;
                        PickRow.PickDictArray[PickRow.PdIndex].ColType = ColTypeDef.ColumnISNOTSET;
                        switch (PickRow.PickDictArray[PickRow.PdIndex].sType) {
                            case ("Q"):
                                PickRow.PickDictArray[PickRow.PdIndex].iAttrType = ColTypeDef.ITEM_ISFILE;
                                PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType = ColTypeDef.ITEM_ISFILEALIAS;
                                PickRow.PickDictArray[PickRow.PdIndex].bAttrIsDict = false;
                                PickRow.PickDictArray[PickRow.PdIndex].bAttrIsData = false;
                                if (PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter > 20) {
                                    PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 100;  // 270
                                }
                                break;
                            case ("D"):
                                PickRow.PickDictArray[PickRow.PdIndex].iAttrType = ColTypeDef.ITEM_ISFILE;
                                PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType = ColTypeDef.ITEM_ISFILE;
                                PickRow.PickDictArray[PickRow.PdIndex].bAttrIsDict = false;
                                PickRow.PickDictArray[PickRow.PdIndex].bAttrIsData = false;
                                if (PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter > 20) {
                                    PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 100;  // 270
                                }
                                PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 10;
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
                                        PickRow.PickDictArray[PickRow.PdIndex].ColType = ColTypeDef.ColumnISCHAR;
                                        PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints += 10;
                                        break;
                                    default:
                                        //
                                        PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType = ColTypeDef.ITEM_ISDICT;
                                        if (PickRow.PickDictArray[PickRow.PdIndex].sCorrType.Length == 0 || PickRow.PickDictArray[PickRow.PdIndex].sType == "S") {
                                            PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType = ColTypeDef.ITEM_ISTYPESAttr;
                                        } else {
                                            LocalMessage.ErrorMsg = "Unrecognized File Dictionary Correlative Field";
                                            throw new NotSupportedException(LocalMessage.ErrorMsg);
                                        }
                                        break;
                                }
                                // Dictionary Attribue Column is Numeric
                                if (PickRow.PickDictArray[PickRow.PdIndex].iAttrType != ColTypeDef.ITEM_ISFUNCTION && PickRow.PickDictArray[PickRow.PdIndex].AttrTwoIsNumeric) {
                                    // Determine ItemData Type for this Column
                                    // Justification
                                    PickRow.PickDictArray[PickRow.PdIndex].ColType = ColTypeDef.ColumnISVARCHAR;
                                    if (PickRow.PickDictArray[PickRow.PdIndex].sJustification == "R") {
                                        PickRow.PickDictArray[PickRow.PdIndex].ColNumericPoints += 5;
                                    } else if (PickRow.PickDictArray[PickRow.PdIndex].sJustification == "L") {
                                        PickRow.PickDictArray[PickRow.PdIndex].ColNumericPoints -= 5;
                                    }
                                    if (PickRow.PickDictArray[PickRow.PdIndex].sJustifyType == "N") {
                                        PickRow.PickDictArray[PickRow.PdIndex].ColNumericPoints += 5;
                                        if (PickRow.PickDictArray[PickRow.PdIndex].sJustification == "R") {
                                            PickRow.PickDictArray[PickRow.PdIndex].ColNumericPoints += 15;
                                        }
                                        PickRow.PickDictArray[PickRow.PdIndex].ColType = ColTypeDef.ColumnISNUMERIC;
                                    }
                                    // Input Conversion
                                    if (PickRow.PickDictArray[PickRow.PdIndex].InputConvType == "D") {
                                        PickRow.PickDictArray[PickRow.PdIndex].ColType = ColTypeDef.ColumnISDATE;
                                    } else if (PickRow.PickDictArray[PickRow.PdIndex].InputConvType == "M") {
                                        if (PickRow.PickDictArray[PickRow.PdIndex].InputConvSubType == "D") {
                                            PickRow.PickDictArray[PickRow.PdIndex].ColNumericPoints += 15;
                                            PickRow.PickDictArray[PickRow.PdIndex].ColType = ColTypeDef.ColumnISNUMERIC;
                                        }
                                    } else {
                                        //
                                    }
                                    // Output Conversion
                                    if (PickRow.PickDictArray[PickRow.PdIndex].spOutputConvType == "D") {
                                        PickRow.PickDictArray[PickRow.PdIndex].ColType = ColTypeDef.ColumnISDATE;
                                    } else if (PickRow.PickDictArray[PickRow.PdIndex].spOutputConvType == "M") {
                                        if (PickRow.PickDictArray[PickRow.PdIndex].spOutputConvSubType == "D") {
                                            PickRow.PickDictArray[PickRow.PdIndex].ColNumericPoints += 15;
                                            PickRow.PickDictArray[PickRow.PdIndex].ColType = ColTypeDef.ColumnISNUMERIC;
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
                                // if (!PickRow.PickDictArray[PickRow.PdIndex].ColIdDone) {
                                // Id must be numeric and column referenced will also be numeric
                                if (PickRow.PickDictArray[PickRow.PdIndex].AttrTwoIsNumeric) {
                                    // PickRow.PickDictArray[PickRow.PdIndex].ColTouched += 1;

                                    // public int[] iItemAttrCounter;
                                    //public int[] iItemLength;
                                    //
                                    PickRow.PickDictArray[PickRow.PdIndex].iItemLength = FmainPassed.Item.ItemData.Length;
                                    PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter = PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter;
                                    if (PickRow.PickDictArray[PickRow.PdIndex].ColIndex == 0 && PickRow.PickDictArray[PickRow.PdIndex].ItemId.ToUpper() == "ID") {
                                        // Dictionary +50FmainPassed.
                                    }
                                    if (PickRow.PickDictArray[PickRow.PdIndex].ColIndex == 0) {
                                        if ((FmainPassed.Fs.FileTypeId & (long)FileType_Is.MaskDatabase) > 0
                                            && PickRow.PickDictArray[PickRow.PdIndex].sHeading.ToUpper()
                                            == FmainPassed.Fs.TableName.ToUpper()) {
                                            // Dictionary +50
                                        } else if (PickRow.PickDictArray[PickRow.PdIndex].sHeading.ToUpper()
                                            == FmainPassed.Fs.FileId.FileName.ToUpper()
                                        ) {
                                            // Dictionary +50
                                        }
                                    }
                                    // length will always be > 0 or error
                                    if (PickRow.PickDictArray[PickRow.PdIndex].ColWidth > 0) {
                                        //
                                        // input & output conversion and correlative must all be empty
                                        if ((PickRow.PickDictArray[PickRow.PdIndex].InputConversion.Length + PickRow.PickDictArray[PickRow.PdIndex].spOutputConversion.Length + PickRow.PickDictArray[PickRow.PdIndex].sCorrelative.Length) == 0) {
                                            // if (PickRow.PickDictArray[PickRow.PdIndex].ColLength == 0) {
                                            //     PickRow.PickDictArray[PickRow.PdIndex].ColLength = PickRow.PickDictArray[PickRow.PdIndex].ColWidth;
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
                                            if (PickRow.PickDictArray[PickRow.PdIndex].ItemId == PickRow.PickDictArray[PickRow.PdIndex].ColIndex.ToString() || PickRow.PickDictArray[PickRow.PdIndex].sHeading == PickRow.PickDictArray[PickRow.PdIndex].ColIndex.ToString()) {
                                                PickRow.PickDictArray[PickRow.PdIndex].ColDefinitionFound = true;
                                                PickRow.PickDictArray[PickRow.PdIndex].ColIdDone = true;
                                                // if (PickRow.PickDictArray[PickRow.PdIndex].ColLength < PickRow.PickDictArray[PickRow.PdIndex].ColWidth) {
                                                //    PickRow.PickDictArray[PickRow.PdIndex].ColLengthChange = true;
                                                PickRow.PickDictArray[PickRow.PdIndex].iItemLength = FmainPassed.Item.ItemData.Length;
                                                PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter = PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter;

                                                // was PickRow.PickDictArray[PickRow.PdIndex].ColLength = PickRow.PickDictArray[PickRow.PdIndex].ColWidth;
                                                // if (PickRow.PickDictArray[PickRow.PdIndex].ItemIdFoundNumericPk) {
                                                // PickRow.PickDictArray[PickRow.PdIndex].ColDefinitionFound = true;
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
                                                if (PickRow.PickDictArray[PickRow.PdIndex].ColLength < PickRow.PickDictArray[PickRow.PdIndex].ColWidth) {
                                                    // TODO THIS CODE SEEMS WRONG HERE vvv
                                                    if (PickRow.PickDictArray[PickRow.PdIndex].ColDefinitionFound) {
                                                        PickRow.PickDictArray[PickRow.PdIndex].ColLengthChange = true;
                                                    } else {
                                                        PickRow.PickDictArray[PickRow.PdIndex].ColLength = PickRow.PickDictArray[PickRow.PdIndex].ColWidth;
                                                        PickRow.PickDictArray[PickRow.PdIndex].ColIdDone = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                // }
                                // Column Attribue ItemData Type
                                PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "vchar";
                                PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = false;
                                switch (PickRow.PickDictArray[PickRow.PdIndex].ColType) {
                                    case (ColTypeDef.ColumnISNUMERIC):
                                        PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "tinyint";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = true;
                                        // bigint   = 8 bytes = 2^63-1      (9,223,372,036,854,775,807)
                                        // int      = 4 bytes = 2^31 - 1    (2,147,483,647)
                                        // smallint = 2 bytes = 2^15 - 1    (32,767)
                                        // tinyint  = 1 bytes = 2^7 - 1     (255)
                                        if (PickRow.PickDictArray[PickRow.PdIndex].ColLength > 9) {
                                            PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "bigint";
                                        } else if (PickRow.PickDictArray[PickRow.PdIndex].ColLength > 4) {
                                            PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "int";
                                        } else if (PickRow.PickDictArray[PickRow.PdIndex].ColLength > 2) {
                                            PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "smallint";
                                        }
                                        break;
                                    case (ColTypeDef.ColumnISCURRENCY):
                                        // decimal
                                        // money
                                        // smallmoney
                                        // smallmoney = 214,748.3647
                                        if (PickRow.PickDictArray[PickRow.PdIndex].ColLength > 5) {
                                            PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "money";
                                        } else {
                                            PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "smallmoney";
                                        }
                                        PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = true;
                                        break;
                                    case (ColTypeDef.ColumnISDATE):
                                        PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "smalldatetime";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = false;
                                        break;
                                    case (ColTypeDef.ColumnISDATETIME):
                                        PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "smalldatetime";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = false;
                                        break;
                                    case (ColTypeDef.ColumnISCHAR):
                                        PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "nchar";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = true;
                                        break;
                                    case (ColTypeDef.ColumnISVARCHAR):
                                        PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "nvarchar";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = true;
                                        break;
                                    case (ColTypeDef.ColumnISCHARU):
                                        PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "nchar";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = true;
                                        break;
                                    case (ColTypeDef.ColumnISVARCHARU):
                                        PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "nvarchar";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = true;
                                        break;
                                    case (ColTypeDef.ColumnISINTEGER):
                                        PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "int";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = false;
                                        break;
                                    case (ColTypeDef.ColumnISFLOAT):
                                        PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "int";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = false;
                                        break;
                                    case (ColTypeDef.ColumnISNOTSET):
                                    case (ColTypeDef.ColumnISUNKNOWN):
                                        PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "nvarchar";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = false;
                                        break;
                                    default:
                                        PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "nvarchar";
                                        PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = false;
                                        LocalMessage.ErrorMsg = "Native Type not set for File Dictionary Correlative Field";
                                        throw new NotSupportedException(LocalMessage.ErrorMsg);
                                }
                                break;
                            default:
                                PickRow.PickDictArray[PickRow.PdIndex].iAttrType = ColTypeDef.ITEM_ISDATA;
                                PickRow.ColumnInvalid = true;
                                PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "nvarchar";
                                PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = false;
                                LocalMessage.ErrorMsg = "Unrecognized File Dictionary Type Field";
                                throw new NotSupportedException(LocalMessage.ErrorMsg);
                        }
                        if (PickRow.PickDictArray[PickRow.PdIndex].iAttrType == ColTypeDef.ITEM_ISNOTSET) {
                            PickRow.ColumnInvalid = true;
                        }
                        if (PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter > 30 || PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints > 100) {
                            PickRow.PickDictArray[PickRow.PdIndex].iAttrType = ColTypeDef.ITEM_ISDATA;
                            PickRow.ColumnInvalid = true;
                        }
                    } // on not ColTypeDef.ITEM_ISDATA
                    if (PickRow.PickDictArray[PickRow.PdIndex].iAttrType != ColTypeDef.ITEM_ISDICT) {
                        PickRow.PickDictArray[PickRow.PdIndex].bAttrIsDict = false;
                        PickRow.PickDictArray[PickRow.PdIndex].bAttrIsData = true;
                    }
                    //
                    if (PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints > 20 || PickRow.ColumnDataPoints > 20 || PickRow.ColumnInvalid || PickRow.PickDictArray[PickRow.PdIndex].iAttrType == ColTypeDef.ITEM_ISDATA) {
                        if (PickRow.ColumnInvalid || PickRow.PickDictArray[PickRow.PdIndex].iAttrType == ColTypeDef.ITEM_ISDATA) {
                            PickRow.PdErrorCount += 1;
                            LocalMessage.ErrorMsg = "This file item is DATA, not a valid dictionary column item!!!";
                            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                        } else {
                            PickRow.PdErrorWarningCount += 1;
                            LocalMessage.ErrorMsg = "This file item might be DATA and not a valid dictionary column item!!!";
                            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                        }
                    }
                    //
                    SqlDictInsertResult = PickRow.PickDictArray[PickRow.PdIndex].iAttrType;
                    sTemp = "";
                    sTemp += FmainPassed.Item.ItemId + ": " + FmainPassed.ColIndex.ColArray[3] + " " + FmainPassed.ColIndex.ColArray[2];
                    sTemp += ", Primary Key ";
                    sTemp += ", Touched (" + PickRow.PickDictArray[PickDictItem.PdIndexAttrTwo].ColTouched.ToString() + ")";
                    sTemp += ", Stored at: " + PickRow.PdIndex.ToString();
                    sTemp += ", Type: " + PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord;
                    sTemp += ", [Type: " + PickRow.PickDictArray[PickRow.PdIndex].ColType;
                    sTemp += ", SubType: " + PickRow.PickDictArray[PickRow.PdIndex].iAttrSubType + "]";
                    sTemp += ", W" + PickRow.PickDictArray[PickRow.PdIndex].ColWidth;
                    sTemp += ", J" + PickRow.PickDictArray[PickRow.PdIndex].sJustification;
                    sTemp += ", Dp" + PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints;
                    sTemp += ", Np" + PickRow.PickDictArray[PickRow.PdIndex].ColNumericPoints;
                    sTemp += ", W" + PickRow.PickDictArray[PickRow.PdIndex].ColType;
                    XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + sTemp);
                } // end of invalid column
            } else {
                // PdErrorCount += 1;
                LocalMessage.ErrorMsg = "This file item is DATA, it is not a valid dictionary column item and was not added!";
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), SqlDictInsertResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
            } // end of ColTypeDef.ITEM_ISDATA not set
            return SqlDictInsertResult;
        }
        //
        /// <summary> 
        /// Add Command Build for all columns from Array.
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks></remarks> 
        public long SqlColAddCmdBuildAllFromArray(ref FileMainDef FmainPassed, String OutputCommandPassed) {
            SqlColAddCmdBuildAllFromArrayResult = (long)StateIs.Started;
            DbSyn.OutputCommand = OutputCommandPassed;
            FmainPassed.DbIo.CommandCurrent = OutputCommandPassed;
            bool SqlColumnReloop = false;
            String sSqlColumnBuildOption = "ADD";
            SqlColAddCmdBuildAllFromArrayResult = (long)StateIs.InProgress;
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
                    SqlColAddCmdBuildAllFromArrayResult = (long)StateIs.InProgress;
                    if (PickRow.PickDictArray[PickRow.PdIndex].ColInvalid <= 0) {
                        if (PickRow.PickDictArray[PickRow.PdIndex].bAttrIsDict) {
                            // Valid Dict Item in this spot
                            if (!PickRow.PickDictArray[PickRow.PdIndex].ColIdDone) {
                                // Warning or error here
                            } // Column Id ReadyToDo
                            //
                            if (PickRow.PickDictArray[PickRow.PdIndex].ColDefinitionFound) {
                                // Warning or error here
                            }
                            //
                            // xxxxxxxxxxxxxxxxx BUILD COMMAND HERE xxxxxxxxxxxxxxxxx
                            //
                            if (iPassIndex == 1) {
                                SqlColAddCmdBuildAllFromArrayResult = SqlColAddCmdBuild(
                                    ref Fmain,
                                    //ref FmainPassed.DbIo.SqlDbDataReader,
                                    //ref FmainPassed.DbIo.SqlDbDataWriter,
                                    //(RowInfoDef)RowInfoDb,
                                    //(ColTransformDef)ColTrans,
                                    true, true, (int)ColTransformDef.SFC_SET_ColumnADD_CMD,
                                    sSqlColumnBuildOption, (int)PickRow.PdIndex, (int)PickRowDef.PdIndexMax);
                                if (SqlColAddCmdBuildAllFromArrayResult == (long)StateIs.Successful) {
                                    if (PickRow.PickDictArray[PickRow.PdIndex].ColAdd.Length > 0) {
                                        if (true == false) {
                                            if (DbSyn.bpOutputAlterScriptFirst) {
                                                DbSyn.bpOutputAlterScriptFirst = false;
                                                // DbSyn.spOutputAlterScript += sSqlColumnBuildOption;
                                            } else { DbSyn.spOutputAlterScript += DbSyn.spOutputAlterListSeparatorChar + " "; }
                                        } else {
                                            DbSyn.bpOutputAlterScriptFirst = false;
                                            DbSyn.spOutputAlterScript = "";
                                        }
                                        DbSyn.spOutputAlterScript += PickRow.PickDictArray[PickRow.PdIndex].ColAdd;
                                        PickRow.PickDictArray[PickRow.PdIndex].ColAddFlag = false;
                                        LocalMessage.Msg1 = "Column Add: " + PickRow.PickDictArray[PickRow.PdIndex].ColAdd;
                                        XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + LocalMessage.Msg1);
                                        if (SqlColAddCmdBuildAllFromArrayResult != (long)StateIs.Failed) {
                                            SqlColAddCmdBuildAllFromArrayResult = SqlColAddCmdBuildAddFromArray(ref FmainPassed, OutputCommandPassed);
                                            LocalMessage.Msg = "Table " + sSqlColumnBuildOption + " " + DbSyn.spSqlColumnAddCmd;
                                            XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + LocalMessage.Msg);
                                        }
                                        SqlColAddCmdBuildAllFromArrayResult = (long)StateIs.InProgress;
                                    } // of add length
                                } // of no error
                            } else {
                                if (PickRow.PickDictArray[PickRow.PdIndex].ColView.Length > 0) {
                                    if (true == true) {
                                        if (DbSyn.bpSqlColumnViewCmdFirst) {
                                            DbSyn.bpSqlColumnViewCmdFirst = false;
                                            // spSqlColumnViewScript += sSqlColumnBuildOption;
                                        } else { DbSyn.spSqlColumnViewScript += DbSyn.spOutputAlterListSeparatorChar + " "; }
                                        DbSyn.spSqlColumnViewScript += PickRow.PickDictArray[PickRow.PdIndex].ColView;
                                        PickRow.PickDictArray[PickRow.PdIndex].ColViewFlag = false;
                                    } else {
                                        DbSyn.spSqlColumnViewScript = PickRow.PickDictArray[PickRow.PdIndex].ColView;
                                        PickRow.PickDictArray[PickRow.PdIndex].ColViewFlag = false;
                                    }
                                    LocalMessage.Msg1 = "View Column Add: " + PickRow.PickDictArray[PickRow.PdIndex].ColView;
                                }
                            }
                            //
                            XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + sSqlColumnBuildOption + " " + LocalMessage.Msg1);
                            LocalMessage.Msg = "Dict Column Build from Array: Pass (" + iPassIndex.ToString() + "), Item (" + (PickRow.PdIndex + 0).ToString() + ").";
                            XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + LocalMessage.Msg);
                            SqlColAddCmdBuildAllFromArrayResult = (long)StateIs.Successful;
                            //
                        } // is dict
                    } // is not invalid
                } // reloop on for PickRow.PdIndex
                if (iPassIndex == 2 && SqlColAddCmdBuildAllFromArrayResult != (long)StateIs.Failed) {
                    SqlColAddCmdBuildAllFromArrayResult = SqlColAddCmdBuildViewFromArray(ref FmainPassed, OutputCommandPassed);
                }
            } // reloop for second pass
            return SqlColAddCmdBuildAllFromArrayResult;
        }
        //
        /// <summary> 
        /// Add Command Build from Array.
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks></remarks> 
        public long SqlColAddCmdBuildAddFromArray(ref FileMainDef FmainPassed, String OutputCommandPassed) {
            SqlColAddCmdBuildAddFromArrayResult = (long)StateIs.Started;
            bool SqlColumnReloop = false;
            String sSqlColumnBuildOption = "";
            // Execute command
            SqlColAddCmdBuildAddFromArrayResult = (long)StateIs.InProgress;
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
                        if ((FmainPassed.Fs.FileTypeId & (long)FileType_Is.MaskDatabase) > 0) {
                            DbSyn.spSqlColumnAddCmd += FmainPassed.Fs.TableName;
                        } else {
                            DbSyn.spSqlColumnAddCmd += FmainPassed.Fs.FileId.FileName;
                        }
                        if (DbSyn.bpOutputAlterColumnNameQuoted) {
                            DbSyn.spSqlColumnAddCmd += DbSyn.spOutputAlterQuoteCharRight;
                        }
                        DbSyn.spSqlColumnAddCmd += " " + sSqlColumnBuildOption + " " + DbSyn.spOutputAlterScript + ";";
                        // spSqlColumnAddCmd += " " + DbSyn.spOutputAlterScript + ";";
                        //
                        SqlColAddCmdBuildAllFromArrayResult = (long)StateIs.Successful;
                        XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + DbSyn.spSqlColumnAddCmd);
                        // sTraceTemp + sProcessHeading + ", " + sProcessSubHeading + ": " + 
                        //
                        if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed || FmainPassed.Fs.ConnDoReset) {
                            FmainPassed.DbIo.SqlDbDataReader.Close();
                        }
                        //
                        if (FmainPassed.DbIo.SqlDbCommand != null) {
                            // FmainPassed.DbIo.SqlDbCommand.Dispose();
                        }
                        if (FmainPassed.DbIo.SqlDbConn.State.ToString() != "Open") {
                            SqlColAddCmdBuildResult = ConnOpen(ref FmainPassed);
                        }
                        // Execute Add Column Command
                        if (FmainPassed.DbIo.SqlDbConn.State.ToString() != "Open") {
                            SqlColAddCmdBuildResult = ConnOpen(ref FmainPassed);
                        }
                        FmainPassed.DbIo.SqlDbCommand = new SqlCommand(DbSyn.spSqlColumnAddCmd, FmainPassed.DbIo.SqlDbConn);
                        SqlColAddCmdBuildResult = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                        //
                        PickRow.PickDictArray[PickRow.PdIndex].ColViewFlag = true;
                        LocalMessage.Msg = "Dict Column Build from Array: Add successful";
                        XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + LocalMessage.Msg);
                        SqlColumnReloop = false;
                        PickRow.PickDictArray[PickRow.PdIndex].ColAddFlag = true;
                    }
                    //
                } catch (SqlException ExceptionSql) {
                    LocalMessage.ErrorMsg = "";
                    ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, SqlColAddCmdBuildAllFromArrayResult);
                    if (!SqlColumnReloop) {
                        SqlColumnReloop = true;
                    } else {
                        SqlColumnReloop = false;
                    }
                    SqlColAddCmdBuildAllFromArrayResult = (long)StateIs.Failed;
                } catch (Exception ExceptionGeneral) {
                    LocalMessage.ErrorMsg = "";
                    ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlColAddCmdBuildAllFromArrayResult);
                    if (!SqlColumnReloop) {
                        SqlColumnReloop = true;
                    } else {
                        SqlColumnReloop = false;
                    }
                    SqlColAddCmdBuildAllFromArrayResult = (long)StateIs.Failed;
                } finally {
                    FmainPassed.DbIo.SqlDbCommand = null;
                }
            } while (SqlColumnReloop);
            // xxxxxxxxxxxxxxxxxxxxxxxxxx
            // View Biuld
            // View Base command
            return SqlColAddCmdBuildAddFromArrayResult;
        }
        //
        /// <summary> 
        /// Build views dictionary data from Array.
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks></remarks> 
        public long SqlColAddCmdBuildViewFromArray(ref FileMainDef FmainPassed, String OutputCommandPassed) {
            SqlColAddCmdBuildViewFromArrayResult = (long)StateIs.Started;
            bool SqlColumnReloop = false;
            String sSqlColumnBuildOption = "ADD";
            SqlColAddCmdBuildAllFromArrayResult = (long)StateIs.InProgress;
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
                        if ((FmainPassed.Fs.FileTypeId & (long)FileType_Is.MaskDatabase) > 0) {
                            DbSyn.spSqlColumnViewCmdPrefix += FmainPassed.Fs.TableName;
                        } else {
                            DbSyn.spSqlColumnViewCmdPrefix += FmainPassed.Fs.FileId.FileName;
                        }
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
                        if ((FmainPassed.Fs.FileTypeId & (long)FileType_Is.MaskDatabase) > 0) {
                            DbSyn.spSqlColumnViewCmdSuffix += FmainPassed.Fs.TableName;
                        } else {
                            DbSyn.spSqlColumnViewCmdSuffix += FmainPassed.Fs.FileId.FileName;
                        }
                        if (DbSyn.bpOutputAlterColumnNameQuoted) {
                            DbSyn.spSqlColumnViewCmdSuffix += DbSyn.spOutputAlterQuoteCharRight;
                        }
                        DbSyn.spSqlColumnViewCmd = " " + DbSyn.spSqlColumnViewCmdPrefix + " " + DbSyn.spSqlColumnViewScript + DbSyn.spSqlColumnViewCmdSuffix + ";";
                        //
                        XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + DbSyn.spSqlColumnViewCmd);
                        //
                        try {
                            if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed) {
                                FmainPassed.DbIo.SqlDbDataReader.Close();
                            }
                        } catch { ; }
                        //
                        if (FmainPassed.DbIo.SqlDbCommand != null) {
                            // FmainPassed.DbIo.SqlDbCommand.Dispose();
                        }
                        // XXXXXXXXXXXXXX BUILD COMMAND FIRST
                        // Execute Add Column Command
                        if (FmainPassed.DbIo.SqlDbConn.State.ToString() != "Open") {
                            if (FmainPassed.DbIo.spConnString.Length == 0
                                || !FmainPassed.ConnStatus.bpNameIsValid
                                || !FmainPassed.Fs.ConnDoReset) {
                                ConnOpenResult = ConnCmdBuild(ref FmainPassed);
                            }
                            // 
                            SqlColAddCmdBuildResult = ConnOpen(ref FmainPassed);
                        }
                        FmainPassed.DbIo.SqlDbCommand = new SqlCommand(DbSyn.spSqlColumnViewCmd, FmainPassed.DbIo.SqlDbConn);
                        SqlColAddCmdBuildResult = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                        SqlColumnReloop = false;
                        //
                        LocalMessage.Msg = "Dict Column Build from Array: Add View successful";
                        XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + LocalMessage.Msg);
                        SqlColAddCmdBuildAllFromArrayResult = (long)StateIs.Successful;
                    } // of not first try
                } catch (SqlException ExceptionSql) {
                    LocalMessage.ErrorMsg = "";
                    ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, SqlColAddCmdBuildAllFromArrayResult);
                    if (!SqlColumnReloop) {
                        SqlColumnReloop = true;
                    } else {
                        SqlColumnReloop = false;
                    }
                    SqlColAddCmdBuildAllFromArrayResult = (long)StateIs.Failed;
                } catch (Exception ExceptionGeneral) {
                    LocalMessage.ErrorMsg = "";
                    ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlColAddCmdBuildAllFromArrayResult);
                    if (!SqlColumnReloop) {
                        SqlColumnReloop = true;
                    } else {
                        SqlColumnReloop = false;
                    }
                    SqlColAddCmdBuildAllFromArrayResult = (long)StateIs.Failed;
                } finally {
                    FmainPassed.DbIo.SqlDbCommand = null;
                }
            } while (SqlColumnReloop);
            return SqlColAddCmdBuildViewFromArrayResult;
        }
        //
        /// <summary> 
        /// Clear the SQL Column Data
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks></remarks> 
        public long SqlColClear(int iPassedPdIndex) {
            SqlColClearResult = (long)StateIs.Started;

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
            // PickRow.PickDictArray[PickRow.PdIndex].ColType = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColInvalid = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColNumericPoints = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColDecimals = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColCurrencyPoints = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColDateFormat = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColFunctionPoints = 0;
            // PickRow.PickDictArray[PickRow.PdIndex].ColSuFile = false;
            // PickRow.PickDictArray[PickRow.PdIndex].ColTouched = new int[100];
            // PickRow.PickDictArray[PickRow.PdIndex].ColIdDone = new bool[100];
            // PickRow.PickDictArray[PickRow.PdIndex].ColLength = new int[100];
            // PickRow.PickDictArray[PickRow.PdIndex].ColLengthChange = new bool[100];
            // PickRow.PickDictArray[PickRow.PdIndex].ColDefinitionFound = new bool[100];
            //
            // 0
            PickRow.PickDictArray[PickRow.PdIndex].ItemId = "";
            PickRow.PickDictArray[PickRow.PdIndex].ItemIntId = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ItemIdConverted = "";

            PickRow.PickDictArray[PickRow.PdIndex].iItemAttrIndex = 0;  // Field being processed in the Dict Column
            PickRow.PickDictArray[PickRow.PdIndex].ItemIdIsNumeric = false;
            // PK
            PickRow.PickDictArray[PickRow.PdIndex].ItemIdFoundNumericPk = false;
            // PickRow.PickDictArray[PickRow.PdIndex].ColTouched = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColIdDone = false;
            // 1 Type for valid dict items
            // Type
            // PickRow.PickDictArray[PickRow.PdIndex].ColInvalid = 0;
            PickRow.PickDictArray[PickRow.PdIndex].sAttrType = "";
            PickRow.PickDictArray[PickRow.PdIndex].iAttrType = 0;
            // Type
            PickRow.PickDictArray[PickRow.PdIndex].sType = "";
            PickRow.PickDictArray[PickRow.PdIndex].ColTypeWord = "";
            // Sub Type
            PickRow.PickDictArray[PickRow.PdIndex].sSubType = "";
            // Column pointers and indexing
            PickRow.PickDictArray[PickRow.PdIndex].ColIndex = 0;
            PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter = 0; // TableOpen  How many Fields (size of)
            PickRow.PickDictArray[PickRow.PdIndex].iItemAttrCounter = 0;
            PickRow.PickDictArray[PickRow.PdIndex].iItemLength = 0;
            // 2
            PickRow.PickDictArray[PickRow.PdIndex].AttrTwoStringValue = "";
            PickRow.PickDictArray[PickRow.PdIndex].PdIndexAttrTwo = 0;
            PickRow.PickDictArray[PickRow.PdIndex].AttrTwoIsNumeric = false;
            // PK
            PickRow.PickDictArray[PickRow.PdIndex].ColTouched = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColIdDone = false;
            PickRow.PickDictArray[PickRow.PdIndex].ColLength = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColLengthChange = false;
            PickRow.PickDictArray[PickRow.PdIndex].ColDefinitionFound = false;
            // Alias account, dict, file, and conversion pointers
            PickRow.PickDictArray[PickRow.PdIndex].ColSuFile = false;
            PickRow.PickDictArray[PickRow.PdIndex].AttrTwoStringAccounName = "";
            PickRow.PickDictArray[PickRow.PdIndex].AttrThreeFileName = "";
            // global - Type for ANY item
            PickRow.PickDictArray[PickRow.PdIndex].sAttrNumber = "";
            PickRow.PickDictArray[PickRow.PdIndex].bAttrIsData = false;
            PickRow.PickDictArray[PickRow.PdIndex].bAttrIsDict = false;
            // ranking for Type
            // ranking analysis and accumulation fields
            PickRow.PickDictArray[PickRow.PdIndex].ColDataPoints = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColNumericPoints = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColDecimals = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColCurrencyPoints = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColDateFormat = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColFunctionPoints = 0;
            // length fields ???
            PickRow.PickDictArray[PickRow.PdIndex].ColLength = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColLengthChange = false;

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
            PickRow.PickDictArray[PickRow.PdIndex].ColWidthString = "";
            PickRow.PickDictArray[PickRow.PdIndex].ColWidth = 0;
            PickRow.PickDictArray[PickRow.PdIndex].ColWidthIsNumeric = false;

            // Heading Long
            PickRow.PickDictArray[PickRow.PdIndex].sHeadingLong = "";
            // Help Short
            PickRow.PickDictArray[PickRow.PdIndex].sHelpShort = "";
            // Revelation RegG ARev Heading
            PickRow.PickDictArray[PickRow.PdIndex].sRevColumnName = "";
            //
            PickRow.PickDictArray[PickRow.PdIndex].ColUseParenthesis = false;
            //
            return SqlColClearResult;
        }
        //
        /// <summary> 
        /// Insert Command Build for Dict Array Data.
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks></remarks> 
        public long SqlDictArrayInsertBuild(ref FileMainDef FmainPassed, String OutputCommandPassed) {
            SqlDictInsertBuildResult = (long)StateIs.Started;
            DbSyn.OutputCommand = OutputCommandPassed;
            FmainPassed.DbIo.CommandCurrent = OutputCommandPassed;
            //
            // TODO z$RelVs? SqlDictArrayInsertBuild build command
            //
            return SqlDictInsertBuildResult;
        }
        //
        /// <summary> 
        /// Do the Execution of SQL Commands
        /// for the Insert.
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks></remarks> 
        public long SqlDictArrayInsertDo(ref FileMainDef FmainPassed, String OutputCommandPassed) {
            SqlDictInsertDoResult = (long)StateIs.Started;
            // CommandPassed = CommandPassed;
            FmainPassed.DbIo.CommandCurrent = OutputCommandPassed;
            //
            //
            // Perform Command
            //
            FmainPassed.DbIo.SqlDbCommand = new SqlCommand(OutputCommandPassed, FmainPassed.DbIo.SqlDbConn);
            //
            SqlDictInsertDoResult = (long)StateIs.InProgress;
            try {
                SqlDictInsertDoResult = Fmain.DbIo.SqlDbCommand.ExecuteNonQuery();
            } catch (SqlException ExceptionSql) {
                LocalMessage.ErrorMsg = "";
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, SqlDictInsertDoResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            } catch (Exception ExceptionGeneral) {
                LocalMessage.ErrorMsg = "";
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlDictInsertDoResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            } finally {
                FmainPassed.DbIo.SqlDbCommand = null;
            }
            return SqlDictInsertDoResult;
        }
        //
        #endregion
        #region SqlDictArrayInsert empty
        #endregion
        #region SqlDictDelete empty
        #endregion
        #endregion
        #endregion
        // Text ItemData File - xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        // TODO z$RelVs$ PICK INCLUDES CSharp - CONTINUE INCLUDES
        #region TextFile
        // <Section Id = "TextFileCheckDoesExist">
        /// <summary> 
        /// Returns a bool indicating if the Text File Exists.
        /// </summary> 
        public bool TextFileDoesExist(ref FileMainDef FmainPassed) {
            TextFileDoesExistResult = TextFileCheckDoesExist(ref FmainPassed);
            if (TextFileDoesExistResult == (long)StateIs.DoesExist) {
                return true;
            } else if (TextFileDoesExistResult == (long)StateIs.DoesNotExist) {
                return false;
            }
            return false;
        }
        // <Section Id = "TextFileCheckDoesExist">
        /// <summary> 
        /// Check if the Text File Exists.
        /// </summary> 
        public long TextFileCheckDoesExist(ref FileMainDef FmainPassed) {
            TextFileDoesExistResult = (long)StateIs.Started;
            FmainPassed.FileStatus.bpDoesExist = false;
            switch (FmainPassed.Fs.FileIo.FileReadMode) {
                case ((long)FileIo_ModeIs.Sql):
                    return (long)StateIs.UnknownFailure;
                    break;
                case ((long)FileIo_ModeIs.Block):
                case ((long)FileIo_ModeIs.Line):
                case ((long)FileIo_ModeIs.All):
                    // xxx full file name parsing
                    if (FmainPassed.Fs.FileId.FileNameLine.Length == 0) {
                        FmainPassed.Fs.FileId.FileNameLine = FileNameLineBuild(ref FmainPassed);
                    }
                    FmainPassed.FileStatus.bpDoesExist = System.IO.File.Exists(FmainPassed.Fs.FileId.FileNameLine);
                    break;
                default:
                    FmainPassed.FileStatus.ipDoesExistResult = (long)FileAction_Do.NotSet;
                    LocalMessage.ErrorMsg = "File Read Method (" + FmainPassed.Fs.FileIo.FileReadMode.ToString() + ") is not set";
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PickSystemCallStringResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                    throw new NotSupportedException(LocalMessage.ErrorMsg);
            }
            if (FmainPassed.FileStatus.bpDoesExist) {
                TextFileDoesExistResult = (long)StateIs.DoesExist;
            } else {
                TextFileDoesExistResult = (long)StateIs.DoesNotExist;
            }
            FmainPassed.FileStatus.ipDoesExistResult = (long)TextFileDoesExistResult;
            return TextFileDoesExistResult;
        }
        // <Section Id = "x
        /// <summary> 
        /// Open the Text File.
        /// </summary> 
        public long TextFileOpen(ref FileMainDef FmainPassed) {
            TextFileOpenResult = (long)StateIs.Started;
            Fmain.Buf.BytesRead = 0;
            Fmain.Buf.BytesReadTotal = 0;
            Fmain.Buf.BytesConverted = 0;
            Fmain.Buf.BytesConvertedTotal = 0;
            TextFileOpenResult = TableOpen(ref FmainPassed);
            return TextFileOpenResult;
        }
        // <Section Id = "TextFileClose">
        /// <summary> 
        /// Close the Text File.
        /// </summary> 
        public long TextFileClose(ref FileMainDef FmainPassed) {
            TextFileCloseResult = (long)StateIs.Started;
            // FileClose(Fs.FileId.FileName);
            // <Area Id = "close the file streams
            try {
                if (Fmain.Fs.FileIo.DbFileStreamReaderObject != null) {
                    Fmain.Fs.FileIo.DbFileStreamReaderObject.Close();
                }
                if (Fmain.Fs.FileIo.DbFileStreamObject != null) {
                    Fmain.Fs.FileIo.DbFileStreamObject.Close();
                }
                //close the file
                if (Fmain.Fs.FileIo.DbFileObject != null) {
                    TextFileCloseResult = AsciiFileClear(ref FmainPassed);
                    // <Area Id = "do destructor;
                }
                TextFileCloseResult = (long)StateIs.Successful;
            } catch (SqlException ExceptionSql) {
                LocalMessage.ErrorMsg = "";
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, TextFileCloseResult);
                ExceptTableOpenError(ref FmainPassed, ref ExceptionSql);
                TextFileCloseResult = (long)StateIs.Failed;
                // Exit Here
            } catch (Exception ExceptionGeneral) {
                LocalMessage.ErrorMsg = "";
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, TextFileCloseResult);
                ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                TextFileCloseResult = (long)StateIs.Failed;
            } finally { ; }
            //
            TextFileCloseResult = (long)StateIs.Successful;
            return TextFileCloseResult;
        }
        // <Section Id = "TextFileWrite">
        /// <summary> 
        /// Write data to the Text File.
        /// </summary> 
        public long TextFileWrite(ref FileMainDef FmainPassed) {
            TextFileWriteResult = (long)StateIs.Started;
            // FileWrite(FileName);
            TextFileWriteResult = (long)StateIs.UnknownFailure;
            return TextFileWriteResult;
        }
        // <Section Id = "TextFileReset">
        /// <summary> 
        /// Reset the Text File Object.
        /// </summary> 
        public long TextFileReset(ref FileMainDef FmainPassed) {
            TextFileResetResult = (long)StateIs.Started;
            // if (Fmain.FileStatus.bpIsInitialized) {
            // THIS IS A DISPOSE FUNCTION
            Fmain.FileStatus.bpIsInitialized = false;
            // }
            return TextFileResetResult;
        }
        // <Section Id = "TextFileCreate">
        /// <summary> 
        /// Create the Text File.
        /// </summary> 
        public long TextFileCreate(ref FileMainDef FmainPassed) {
            TextFileCreateResult = (long)StateIs.Started;
            TextFileCreateResult = (long)StateIs.InProgress;
            try {
                FmainPassed.Fs.FileIo.DbFileStreamObject = File.Create(FmainPassed.Fs.FileId.FileName);
                FmainPassed.FileStatus.bpDoesExist = true;
                TextFileCreateResult = (long)StateIs.Successful;
            } catch (SqlException ExceptionSql) {
                LocalMessage.ErrorMsg = "";
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, TextFileCreateResult);
                TextFileCreateResult = (long)StateIs.ShouldNotExist;
                ExceptTableOpenError(ref FmainPassed, ref ExceptionSql);
                Fmain.FileStatus.bpDoesExist = false;
                // Exit Here
            } catch (Exception ExceptionGeneral) {
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, TextFileCreateResult);
                ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                FmainPassed.FileStatus.bpDoesExist = false;
                TextFileCreateResult = (long)StateIs.OsError;
            } finally {
                FmainPassed.FileStatus.ipStatusCurrent = TextFileCreateResult;
                if (TextFileCreateResult != (long)StateIs.Successful) {
                    Fmain.Fs.FileIo.DbFileStreamObject = null;
                }
            }
            return TextFileCreateResult;
        }
        // <Section Id = "TextFileDelete">
        /// <summary> 
        /// Delete the Text File.
        /// </summary> 
        public long TextFileDelete(ref FileMainDef FmainPassed) {
            TextFileDeleteResult = (long)StateIs.Started;
            // FileDelete(FileName);
            TextFileDeleteResult = (long)StateIs.UnknownFailure;
            return TextFileDeleteResult;
        }
        // <Section Id = "TextFileProcessMain">
        /// <summary> 
        /// Main Method (virtual) for Processing Text File.
        /// </summary> 
        public virtual long TextFileProcessMain(String PassedFileNameRequest) {
            LocalId.LongResult = (long)StateIs.Started;
            // <Area Id = "The files used here were created in the code example
            // in How to: Write to a Text File. You can of course substitute
            // other files of your own.

            // Example #1
            // bRead the file as one string.
            String IoLineAll = System.IO.File.ReadAllText(@"C:\Users\Public\TestFolder\WriteText.txt");

            // <Area Id = "Display the file contents to the Console.
            LocalMessage.Msg = "Contents of writeText.txt : " + IoLineAll;
            MessageMdmSendToPage(Sender, "A2" + LocalMessage.Msg);
            // Example #2
            // <Area Id = "bRead the file aslines into a String array.
            String[] LinesArray = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteLines2.txt");

            MessageMdmSendToPage(Sender, "C" + "Contents of writeLines2.txt: ");
            foreach (String sLine in LinesArray) {
                MessageMdmSendToPage(Sender, "\t" + sLine);
            }
            MessageMdmSendToPage(Sender, "C");

            // <Area Id = "Keep the Console window open in debug mode.
            MessageMdmSendToPage(Sender, "Press any key to exit.");
            System.Console.ReadKey();
            MessageMdmSendToPage(Sender, "C");

            return LocalId.LongResult;
        }
        #endregion
        #region FileDataReset
        // FileDataReset
        /// <summary> 
        /// FileDataReset should reset all of Fmain...
        /// </summary> 
        public long FileDataReset(ref FileMainDef FmainPassed) {
            PickDataResetResult = (long)StateIs.Started;
            //
            FmainPassed.Item.ItemId = ""; // Reset
            if (IterationFirst) {
                FmainPassed.Fs.ItemIdCurrent = ""; // Reset
                FmainPassed.Fs.ItemIdNext = ""; // Reset
            }
            // TODO FileDataReset Reset Other Item ItemData
            // Item ItemData
            FmainPassed.Item.ItemData = "";
            // FileDataReset
            return PickDataResetResult;
        }
        #endregion
        #region DictResetDataReset
        // TODO ColIndexReset needs work
        /// <summary> 
        /// Reset the Column Index Data.
        /// </summary> 
        /// <param name="ColIndexPassed"></param> 
        public long ColIndexReset(ref ColIndexDef ColIndexPassed) {
            PickDictResetResult = (long)StateIs.Started;
            // Index to field within a dict item
            ColIndexPassed.ColIndex = 0; // Pick Dict Reset
            ColIndexPassed.ColIndexTotal = 0; // Pick Dict Reset
            // Info about this dict item;
            ColIndexPassed.ColCount = 0; // Pick Dict Reset
            ColIndexPassed.ColCountTotal = 0;

            ColIndexPassed.ColInvalid = 0;
            // ColIndexPassed.ColInvalid = 0;
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
        /// <summary> 
        /// Clear Dictionary Data.
        /// </summary> 
        public long FileDictClearCurrent(ref FileMainDef FmainPassed) {
            PickDictClearCurrentResult = (long)StateIs.Started;
            // Clear dictionary array that contains this fields properties.
            Array.Clear(FmainPassed.ColIndex.ColArray, 0, 100);
            #region Input Dictionay Index Pointers
            // FILE DICTIONARY FILE CONTROL
            // Reset Input File protected internal Attr pointer for next dict item
            FmainPassed.ColIndex.ColIndex = 0; // dynamic protected internal pointer
            FmainPassed.ColIndex.ColCount = 0; // Total
            FmainPassed.ColIndex.ColCounter = 0; // Current for building
            // FILE DICTIONARY COLUMNS
            FmainPassed.ColIndex.ColAttrIndex = 0;
            FmainPassed.ColIndex.ColAttrCount = 0;
            FmainPassed.ColIndex.ColAttrCounter = 1;
            FmainPassed.ColIndex.ColAttrSet = false;
            FmainPassed.ColIndex.ColAttrInvalid = 0;
            #endregion
            // Item ItemData
            FmainPassed.Item.ItemData = "";
            // ItemData Item Type
            // OutFile.Fmain.Fs.FileTypeId = Fs.FileTypeId;
            // OutFile.Fmain.Fs.FileSubTypeId = Fs.FileSubTypeId;
            //
            // FileDictClearCurrent
            return PickDictClearCurrentResult;
        }
        // FileDataClearCurrent
        /// <summary> 
        /// Clear File Data.
        /// </summary> 
        public long FileDataClearCurrent(ref FileMainDef FmainPassed) {
            PickDataClearCurrentResult = (long)StateIs.Started;
            //
            FmainPassed.Item.ItemId = "";
            if (IterationFirst) { // CHECK & TODO FileDataClearCurrent
                FmainPassed.Fs.ItemIdCurrent = "";
                // OutFile.Fmain.Fs.ItemIdNext = "";
            }
            #region Input ItemData Index Pointers
            // FILE DATA COLUMNS
            FmainPassed.DelSep.ItemAttrIndex = 0; // dynamic protected internal pointer
            FmainPassed.DelSep.ItemAttrCount = 0;
            FmainPassed.DelSep.ItemAttrCountTotal = 0;// Accumulator for shrinking work buffer
            FmainPassed.DelSep.ItemAttrCounter = 1;
            FmainPassed.DelSep.ItemAttrSet = false;
            FmainPassed.DelSep.ItemAttrInvalid = 0;
            // FILE DATA COLUMNS
            FmainPassed.ColIndex.ColAttrIndex = 0; // dynamic protected internal pointer
            FmainPassed.ColIndex.ColAttrCount = 0;
            FmainPassed.ColIndex.ColAttrCountTotal = 0;// Accumulator for shrinking work buffer
            FmainPassed.ColIndex.ColAttrCounter = 1;
            FmainPassed.ColIndex.ColAttrSet = false;
            FmainPassed.ColIndex.ColAttrInvalid = 0;
            // Reset Input File protected internal column processing pointer for next dict item
            FmainPassed.ColIndex.ColIndex = 0;
            FmainPassed.ColIndex.ColCount = 0;
            FmainPassed.ColIndex.ColCountTotal = 0;// Accumulator for shrinking work buffer
            FmainPassed.ColIndex.ColCounter = 0;
            FmainPassed.ColIndex.ColSet = false;
            FmainPassed.ColIndex.ColInvalid = 0;
            #endregion
            #region Item ItemData
            FmainPassed.Item.ItemData = "";
            // ItemData Item Type
            FmainPassed.Fs.FileTypeId =
                (long)FileType_Is.Unknown;
            FmainPassed.Fs.FileSubTypeId =
                (long)FileType_SubTypeIs.Unknown;
            #endregion
            #region InputItem
            FmainPassed.Item.ItemData = ""; // File Item ItemData
            FmainPassed.Fs.ItemIdCurrent = ""; // Current Id
            FmainPassed.Fs.ItemIdNext = ""; // Next Id
            FmainPassed.Item.ItemId = ""; // This Id
            FmainPassed.Item.ItemIdIsChanged = bNO;
            FmainPassed.FileStatus.bpNameIsChanged = bNO;
            // Import Input File Item Name
            // ** ColIndexPassed.Fs.FileId.FileNameFullDefault = "tld.import";
            #endregion
            #region Import Input File Options, Control and Modes
            // Import Output File Read and Access Modes
            FmainPassed.Fs.FileIo.FileReadMode = (long)FileIo_ModeIs.Line; // TODO RunMain FileTransformControl initialize
            FmainPassed.Fs.FileIo.FileAccessMode = (long)FileIo_ModeIs.Line; // TODO RunMain FileTransformControl initialize
            // Import Input File Options
            FmainPassed.Fs.FileOptionString = "F"; // TODO $$$CHECK 7 FileDataClearCurrent options hard corded here...
            #endregion
            #region Other Working Fields
            ItemDataAtrributeClear(ref FmainPassed);
            // other
            FmainPassed.DelSep.ItemAttrMaxIndex = 0; // Total Attrs in Item
            FmainPassed.DelSep.ItemAttrMaxIndexTemp = 0; // Total Attrs in Item
            // Character Pointers
            ItemDataCharClear(ref FmainPassed);
            // Buf.
            FmainPassed.Buf.BytesRead = 0;
            FmainPassed.Buf.BytesReadTotal = 0;
            FmainPassed.Buf.CharIndex = 1;
            FmainPassed.Buf.CharItemEofIndex = 0;
            // Working buffer value
            FmainPassed.Buf.FileWorkBuffer = "";
            // Conversion results
            FmainPassed.Buf.BytesConverted = 0;
            FmainPassed.Buf.BytesConvertedTotal = 0;
            // TODO FileDataClearCurrent Special Characters
            #endregion
            // FileDataClearCurrent
            return PickDataClearCurrentResult;
        }
        #endregion
        // Database Connection - xxxxxxxxxxxxxxxxxxxxxxxxx
        #region Conn
        #region Close Connection
        // <Section Id = "x
        /// <summary> 
        /// Close the Connection.
        /// </summary> 
        public long ConnClose(ref FileMainDef FmainPassed) {
            ConnCloseResult = (long)StateIs.Started;
            // Do not arrive here without checking KeepOpen....
            switch (FmainPassed.Fs.FileIo.FileReadMode) {
                case ((long)FileIo_ModeIs.Sql):
                    if (FmainPassed.ConnStatus.bpIsConnected == false) {
                        // <Area Id = "WARNING - Already disconnected">
                        ConnCloseResult = (long)StateIs.Successful;
                    } else {
                        // <Area Id = "Connected">
                        // ConnStatus.bpIsConnected = true;
                        while (FmainPassed.ConnStatus.bpIsConnected) {
                            // <Area Id = "Connect">
                            ConnCloseResult = (long)StateIs.InProgress;
                            try {
                                // <Area Id = "close connection
                                if (FmainPassed.ConnStatus.bpIsOpen) {
                                    FmainPassed.DbIo.SqlDbConn.Close();
                                }
                                // <Area Id = "dispose of connection
                                if (FmainPassed.ConnStatus.bpIsCreated || FmainPassed.DbIo.SqlDbConn != null) {
                                    FmainPassed.DbIo.SqlDbConn.Dispose();
                                }
                                ConnCloseResult = (long)StateIs.Successful;
                            } catch (SqlException ExceptionSql) {
                                LocalMessage.ErrorMsg = "";
                                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, ConnCloseResult);
                                ExceptTableCloseImpl(ref FmainPassed, ref ExceptionSql);
                                ConnCloseResult = (long)StateIs.DatabaseError;
                            } catch (Exception ExceptionGeneral) {
                                LocalMessage.ErrorMsg = "";
                                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, ConnCloseResult);
                                ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                                ConnCloseResult = (long)StateIs.OsError;
                            } finally {
                                FmainPassed.ConnStatus.bpIsOpen = false;
                                FmainPassed.ConnStatus.bpIsCreated = false;
                                FmainPassed.ConnStatus.bpIsConnected = false;
                                FmainPassed.ConnStatus.bpIsConnecting = false;
                                FmainPassed.ConnStatus.bpNameIsValid = false;
                                FmainPassed.DbIo.DataClear();
                                FmainPassed.Fs.ConnDoReset = true;
                                //
                                ObjectListClearData();
                            } // of try disconnect
                        } // of is connected
                    } // is already connected
                    ConnCloseResult = (long)StateIs.Successful;
                    break;
                case ((long)FileIo_ModeIs.Block):
                case ((long)FileIo_ModeIs.Line):
                case ((long)FileIo_ModeIs.All):
                    FmainPassed.ConnStatus.bpIsOpen = false;
                    FmainPassed.ConnStatus.bpIsCreated = false;
                    FmainPassed.ConnStatus.bpIsConnected = false;
                    FmainPassed.ConnStatus.bpIsConnecting = false;
                    FmainPassed.ConnStatus.NameIsValid = false;
                    FmainPassed.Fs.ConnDoReset = true;
                    ConnCloseResult = (long)StateIs.Successful;
                    // Fmain.FileStatus.bpDoesExist = System.IO.File.Exists(Fs.FileId.FileName);
                    // ConnCloseResult = (long)StateIs.Invalid;
                    break;
                default:
                    ConnCloseResult = (long)FileAction_Do.NotSet;
                    LocalMessage.ErrorMsg = "File Read Method (" + FmainPassed.Fs.FileIo.FileReadMode.ToString() + ") is not set";
                    throw new NotSupportedException(LocalMessage.Msg);
            }

            return ConnCloseResult;
        }
        #endregion
        #region Open Connection
        // <Section Id = "x
        /// <summary> 
        /// Open the Connection.
        /// </summary> 
        public long ConnOpen() {
            ConnOpenResult = (long)StateIs.Started;

            ConnOpenResult = ConnOpen(ref Fmain);

            return ConnOpenResult;
        }

        // <Section Id = "x
        /// <summary> 
        /// Open the Passed Connection.
        /// </summary> 
        public long ConnOpen(ref FileMainDef FmainPassed) {
            ConnOpenResult = (long)StateIs.Started;
            // Fmain.FileStatus.bpIsInitialized
            // if (ConnStatus.bpIsInitialized) {
            // ConnOpenResult = ConnReset();
            // }
            if (!FmainPassed.ConnStatus.bpIsCreated
                || !FmainPassed.ConnStatus.bpNameIsValid
                || FmainPassed.Fs.ConnDoReset
                || FmainPassed.DbIo.SqlDbConn == null
                || FmainPassed.DbIo.spConnString.Length == 0
                ) {
                // close connection
                if (FmainPassed.DbIo.SqlDbConn != null) {
                    try {
                        if (FmainPassed.DbIo.SqlDbConn.State == ConnectionState.Open) {
                            SqlColAddCmdBuildResult = ConnClose(ref FmainPassed);
                        } else if (FmainPassed.DbIo.SqlDbConn.State != ConnectionState.Open) {
                            SqlColAddCmdBuildResult = ConnReset(ref FmainPassed);
                        }
                    } catch { ; }
                }
                // create connection
                ConnOpenResult = ConnCreate(ref FmainPassed);
                //
            } else if (FmainPassed.DbIo.SqlDbConn != null) {
                if (FmainPassed.ConnStatus.bpIsConnected
                    && FmainPassed.DbIo.SqlDbConn.State == ConnectionState.Open
                    // && FmainPassed.ConnStatus.IsOpen
                    ) {
                    // <Area Id = "WARNING - Already connected">
                    return (ConnOpenResult = (long)StateIs.Successful);
                }
            }
            FmainPassed.ConnStatus.bpIsInitialized = true;
            ConnOpenResult = (long)StateIs.InProgress;
            // <Area Id = "CheckDatabaseDoesExist">
            switch (FmainPassed.Fs.FileIo.FileReadMode) {
                case ((long)FileIo_ModeIs.Sql):
                    if (!FmainPassed.ConnStatus.bpIsCreated
                        || !FmainPassed.ConnStatus.bpDoesExist
                        || FmainPassed.DbIo.SqlDbConn == null) {
                        //
                        ConnOpenResult = ConnCheckDoesExist(ref FmainPassed);
                    }
                    break;
                case ((long)FileIo_ModeIs.Block):
                case ((long)FileIo_ModeIs.Line):
                case ((long)FileIo_ModeIs.All):
                    FmainPassed.ConnStatus.ipDoesExistResult = (long)StateIs.DoesExist;
                    FmainPassed.ConnStatus.bpDoesExist = true;
                    Fmain.FileStatus.bpDoesExist = System.IO.File.Exists(FmainPassed.Fs.TableNameLine);
                    if (Fmain.FileStatus.bpDoesExist) {
                        ConnOpenResult = (long)StateIs.DoesExist;
                        FmainPassed.ConnStatus.bpIsOpen = true;
                        FmainPassed.ConnStatus.bpIsConnected = true;
                        FmainPassed.ConnStatus.bpIsConnecting = false;
                        break;
                    } else {
                        ConnOpenResult = (long)StateIs.DoesNotExist;
                        FmainPassed.ConnStatus.bpIsOpen = false;
                        FmainPassed.ConnStatus.bpIsConnected = false;
                        FmainPassed.ConnStatus.bpIsConnecting = false;
                        return ConnOpenResult;
                    }
                default:
                    ConnOpenResult = (long)StateIs.Undefined;
                    LocalMessage.ErrorMsg = "File Read Method (" + FmainPassed.Fs.FileIo.FileReadMode.ToString() + ") is not set";
                    throw new NotSupportedException(LocalMessage.ErrorMsg);
            }
            if (!FmainPassed.ConnStatus.bpIsConnected && FmainPassed.DbIo.SqlDbConn != null) {
                // <Area Id = "not Connected">
                FmainPassed.ConnStatus.bpIsConnecting = true;
                while (!FmainPassed.ConnStatus.bpIsConnected
                    && FmainPassed.ConnStatus.bpIsConnecting
                    && FmainPassed.ConnStatus.bpDoesExist) {
                    // <Area Id = "Connect">
                    ConnOpenResult = FmainPassed.ConnStatus.ipIsConnectingResult = (long)StateIs.InProgress;
                    try {
                        if (!FmainPassed.ConnStatus.bpDoesExist) { ConnOpenResult = ConnCreate(); }
                        if (ConnOpenResult == (long)StateIs.InProgress || ConnOpenResult == (long)StateIs.Successful) {
                            switch (FmainPassed.Fs.FileIo.FileReadMode) {
                                case ((long)FileIo_ModeIs.Sql):
                                    // Open Database Connection
                                    // FmainPassed.ConnStatus.ipIsConnectingResult = (int) StateIs.InProgress;
                                    ConnOpenResult = (long)StateIs.InProgress;
                                    Sys.sMessageBoxMessage = MdmProcessTitle + "\n" + "SQL Database Connection:" + FmainPassed.Fs.DatabaseName;
                                    FmainPassed.DbIo.SqlDbConn.Open();
                                    ConnOpenResult = (long)StateIs.Successful;
                                    FmainPassed.ConnStatus.bpIsOpen = true;
                                    FmainPassed.ConnStatus.bpIsConnecting = false;
                                    FmainPassed.ConnStatus.bpIsConnected = true;
                                    FmainPassed.ConnStatus.NameIsValid = true;
                                    break;
                                case ((long)FileIo_ModeIs.Block):
                                case ((long)FileIo_ModeIs.Line):
                                case ((long)FileIo_ModeIs.All):

                                    // Check Disk access
                                    // Check Folder exists
                                    // Check Other Disk criteria
                                    // Check other File criteria
                                    ConnOpenResult = (long)StateIs.Successful;
                                    FmainPassed.ConnStatus.bpIsOpen = true;
                                    FmainPassed.ConnStatus.bpIsConnected = true;
                                    FmainPassed.ConnStatus.bpIsConnecting = false;
                                    FmainPassed.ConnStatus.NameIsValid = true;
                                    break;
                                default:
                                    LocalMessage.ErrorMsg = "File Read Method (" + FmainPassed.Fs.FileIo.FileReadMode.ToString() + ") is not set";
                                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PickSystemCallStringResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                                    ConnOpenResult = (long)FileAction_Do.NotSet;
                                    throw new NotSupportedException(LocalMessage.ErrorMsg);
                            }
                            // ConnOpenResult = (int) StateIs.Successful;
                            // FmainPassed.ConnStatus.bpIsConnected = true;
                            // FmainPassed.ConnStatus.bpIsConnecting = false;
                            // DbIo.SqlDbConn.Close();
                        } else {
                            LocalMessage.ErrorMsg = "SQL Database connection error on database: " + FmainPassed.Fs.DatabaseName;
                            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PickSystemCallStringResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                            ConnOpenResult = (long)StateIs.Failed;
                            FmainPassed.ConnStatus.bpIsConnecting = false;
                            FmainPassed.ConnStatus.bpIsConnected = false;
                            FmainPassed.ConnStatus.NameIsValid = false;
                        }
                        // exceptions:
                    } catch (NotSupportedException ExceptionGeneral) {
                        LocalMessage.ErrorMsg = "";
                        ExceptTableGeneralImpl(ref ExceptionGeneral);
                        ConnOpenResult = (long)StateIs.Failed;
                        FmainPassed.ConnStatus.bpIsConnecting = false;
                        FmainPassed.ConnStatus.bpIsConnected = false;
                        FmainPassed.ConnStatus.NameIsValid = false;
                    } catch (SqlException ExceptionSql) {
                        LocalMessage.ErrorMsg = "";
                        ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, ConnCloseResult);
                        //
                        ExceptTableOpenError(ref FmainPassed, ref ExceptionSql);
                        ConnOpenResult = (long)StateIs.Failed;
                        FmainPassed.ConnStatus.bpIsConnecting = false;
                        FmainPassed.ConnStatus.bpIsConnected = false;
                        FmainPassed.ConnStatus.NameIsValid = false;
                    } catch (Exception ExceptionGeneral) {
                        LocalMessage.ErrorMsg = "";
                        ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, ConnCloseResult);
                        ConnOpenResult = (long)StateIs.Failed;
                        ConnCloseResult = (long)StateIs.OsError;
                        ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                        FmainPassed.ConnStatus.bpIsConnecting = false;
                        FmainPassed.ConnStatus.bpIsConnected = false;
                        FmainPassed.ConnStatus.NameIsValid = false;
                    } finally {
                        if (ConnOpenResult == (long)StateIs.InProgress) {
                            ConnOpenResult = (long)StateIs.Failed;
                        }
                        FmainPassed.ConnStatus.ipIsConnectingResult = ConnOpenResult;
                        FmainPassed.ConnStatus.bpIsOpen = FmainPassed.ConnStatus.bpIsConnected;
                    } // of try connect
                } // of is connecting
            } else {
                ConnOpenResult = (long)StateIs.Successful;
                FmainPassed.ConnStatus.bpIsOpen = false;
                FmainPassed.ConnStatus.bpIsConnecting = false;
                FmainPassed.ConnStatus.bpIsConnected = true;
                FmainPassed.ConnStatus.NameIsValid = false;
                FmainPassed.ConnStatus.bpIsCreating = false;
                FmainPassed.ConnStatus.bpIsCreated = true;
                FmainPassed.ConnStatus.ipIsConnectingResult = ConnOpenResult;
            }
            FmainPassed.Fs.ConnDoReset = false;
            FmainPassed.ConnStatus.IsOpenResult = ConnOpenResult;
            return ConnOpenResult;
        }
        #endregion
        #region Connection Reset, Check
        // <Section Id = "x
        /// <summary> 
        /// Reset the Connection.
        /// </summary> 
        public long ConnReset(ref FileMainDef FmainPassed) {
            ConnResetResult = (long)StateIs.Started;
            // Fmain.FileStatus.bpIsInitialized
            // if (FmainPassed.ConnStatus.bpIsInitialized) {
            DbSyn.spConnCreateCmd = "";
            DbMasterSyn.spMstrDbFileCreateCmd = "";
            // <Area Id = "DbConnObjects">
            FmainPassed.DbIo.DataClear();
            // Status
            FmainPassed.ConnStatus.ipDoesExistResult = 0;
            FmainPassed.ConnStatus.bpDoesExist = false;
            FmainPassed.ConnStatus.bpIsValid = false;
            FmainPassed.ConnStatus.ipIsConnectingResult = 0;
            FmainPassed.ConnStatus.bpIsConnecting = false;
            FmainPassed.ConnStatus.bpIsConnected = false;
            FmainPassed.ConnStatus.bpIsOpen = false;
            FmainPassed.ConnStatus.bpIsCreating = false;
            FmainPassed.ConnStatus.bpIsCreated = false;
            FmainPassed.ConnStatus.bpIsInitialized = false;
            // Execeptions
            DbFileCmdOsException = null;
            //
            FmainPassed.Fs.ConnDoReset = false;
            //
            ObjectListClearData();
            //
            Sys.sMformStatusMessage = "";
            Sys.sMessageBoxMessage = "";
            //
            return ConnResetResult;
        }
        // <<Section Id = "x
        // This function checks the state of the connection (not flags)
        /// <summary> 
        /// Check the Connection.
        /// </summary> 
        public long ConnCheck(ref FileMainDef FmainPassed) {
            ConnCheckDoesExistResult = (long)StateIs.Started;

            if (FmainPassed.DbIo.SqlDbConn != null
                && !(FmainPassed.DbIo.SqlDbConn.Database == FmainPassed.Fs.DatabaseName
                && FmainPassed.DbIo.SqlDbConn.DataSource == FmainPassed.Fs.ServerName)
                ) {
                try {
                    if (FmainPassed.DbIo.SqlDbConn != null) {
                        if (FmainPassed.DbIo.SqlDbConn.State.ToString() == "Open" && FmainPassed.Fs.ConnDoReset) {
                            FmainPassed.Fs.ConnDoReset = true;
                            FmainPassed.DbIo.ConnString = "";
                            //} else if (FmainPassed.DbIo.SqlDbConn.State.ToString() != "Open" && FmainPassed.Fs.ConnDoReset) {
                            //    SqlColAddCmdBuildResult = ConnReset(ref FmainPassed);
                        }
                    }
                } catch { ; }
            }
            //
            if (FmainPassed.Fs.ServerName.Length == 0
                || FmainPassed.Fs.DatabaseName.Length == 0
                || FmainPassed.Fs.TableNameLine.Length == 0
                || !FmainPassed.DbStatus.bpNameIsValid
                ) {
                FmainPassed.Fs.TableNameLine = TableNameLineBuild(ref FmainPassed);
            }
            //
            if (FmainPassed.DbIo.SqlDbConn == null
                || (!(FmainPassed.DbIo.SqlDbConn.Database == FmainPassed.Fs.DatabaseName
                && FmainPassed.DbIo.SqlDbConn.DataSource == FmainPassed.Fs.ServerName)
                || FmainPassed.DbIo.SqlDbConn.State != ConnectionState.Open)
                ) {
                if (!(FmainPassed.Fs.DatabaseName.Length == 0
                || FmainPassed.Fs.ServerName.Length == 0)) {
                    ConnCheckDoesExistResult = ConnOpen(ref FmainPassed);
                } else { ConnCheckDoesExistResult = (long)StateIs.Failed; }
            } else { ConnCheckDoesExistResult = (long)StateIs.Successful; }
            return ConnCheckDoesExistResult;
        }

        // This function performs a lookup / search for the database (connection)
        /// <summary> 
        /// Check if the Connection Exists.
        /// </summary> 
        public long ConnCheckDoesExist(ref FileMainDef FmainPassed) {
            ConnCheckDoesExistResult = (long)StateIs.Started;
            if (FmainPassed.DbIo.SqlDbConn.State.ToString() == "Open") {
                FmainPassed.ConnStatus.bpDoesExist = true;
                ConnCheckDoesExistResult = (long)StateIs.DoesExist;
            } else {
                FmainPassed.ConnStatus.bpDoesExist = false;
                ConnCheckDoesExistResult = (long)StateIs.DoesNotExist;
            }
            FmainPassed.ConnStatus.ipDoesExistResult = ConnCheckDoesExistResult;
            return ConnCheckDoesExistResult;
        }
        #endregion
        #region ConnCreate
        // <Section Id = "SQL File Handling
        /// <summary> 
        /// Build the Connection Command.
        /// </summary> 
        public long ConnCmdBuild(ref FileMainDef FmainPassed) {
            ConnCmdBuildResult = (long)StateIs.Started;

            FmainPassed.ConnStatus.bpNameIsValid = true;
            String sProvider = "";
            const int PROVIDERDOTNET = 1;
            const int PROVIDEROLEDB = 2;
            const int PROVIDERODBC = 3;
            int iProvider = PROVIDERDOTNET;
            const String PROVIDERSQLNCLI = "SQLNCLI";
            const String PROVIDERNATIVE_CLIENT = "{SQL Native Client}";
            bool UseServer = true;
            //
            const int PROVIDERLOCAL = 1;
            const int PROVIDERREMOTE = 2;
            int iProviderLocation = PROVIDERLOCAL;
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

            FmainPassed.DbIo.spConnString = "";
            iIterationLoopCounter += 1;

            if (iUserSource == UserPROMPT) {
                // oConn.Properties("Prompt") = adPromptAlways
                // ConnCmdBuildResult = ConnCmdUserPrompt();
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

            if (iProvider == PROVIDERDOTNET) {
                //
            } else if (iProvider == PROVIDEROLEDB) {
                // Provider=SQLNCLI;
                sProvider += "Provider=";
                sProvider += PROVIDERSQLNCLI;
            } else if (iProvider == PROVIDERODBC) {
                // Driver={SQL Native Client};
                sProvider += "Driver=";
                sProvider += PROVIDERNATIVE_CLIENT;
            }
            if (sProvider.Length > 0) {
                FmainPassed.DbIo.spConnString += sProvider;
                FmainPassed.DbIo.spConnString += ";";
            }

            if (iProviderLocation == PROVIDERLOCAL) {
                UseServer = true;
            } else if (iProviderLocation == PROVIDERREMOTE) {
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
                FmainPassed.DbIo.spConnString += sProvider;
                FmainPassed.DbIo.spConnString += ";";
            }

            if (UseServer) {
                if (FmainPassed.Fs.ServerName.Length == 0) { FmainPassed.ConnStatus.bpNameIsValid = false; }
                FmainPassed.DbIo.spConnString += "Server=";
                // DbIo.spConnString += FmainPassed.SystemName + @"\" + FmainPassed.ServiceName;
                FmainPassed.DbIo.spConnString += FmainPassed.Fs.ServerName;
                // DbIo.spConnString += "localhost";
                FmainPassed.DbIo.spConnString += ";";
            }

            if (UseDatabasePath) {
                // AttachDbFilename=|DataDirectory|mydbfile.mdf;
                FmainPassed.DbIo.spConnString += "AttachDbFilename=";
                FmainPassed.DbIo.spConnString += sDbPathDirectory;
                FmainPassed.DbIo.spConnString += sDbPathFileName;
                FmainPassed.DbIo.spConnString += ";";
            }

            if (UseDatabaseName) {
                sDatabase += "Database=";
                if (FmainPassed.Fs.DatabaseName.Length == 0) {
                    FmainPassed.ConnStatus.bpNameIsValid = false;
                    FmainPassed.DbStatus.bpNameIsValid = false;
                }
                sDatabase += FmainPassed.Fs.DatabaseName;
                FmainPassed.DbIo.spConnString += sDatabase;
                FmainPassed.DbIo.spConnString += ";";
            } else if (UseCatalog) {
                sDatabase += "Initial Catalog=";
                if (FmainPassed.Fs.DatabaseName.Length == 0) {
                    FmainPassed.ConnStatus.bpNameIsValid = false;
                    FmainPassed.DbStatus.bpNameIsValid = false;
                }
                sDatabase += FmainPassed.Fs.DatabaseName;
                FmainPassed.DbIo.spConnString += sDatabase;
                FmainPassed.DbIo.spConnString += ";";
            }

            //
            if (!Fmain.DbMaster.UseSSPI) {
                Security += "Trusted_Connection=";
                // Security += "True";
                Security += "Yes";
            } else {
                Security += "Integrated Security=";
                Security += "SSPI";
            }
            FmainPassed.DbIo.spConnString += Security;
            FmainPassed.DbIo.spConnString += ";";

            if (UseUser) {
                FmainPassed.DbIo.spConnString += User;
                FmainPassed.DbIo.spConnString += ";";
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
            ConnCmdBuildResult = (long)StateIs.Successful;
            return ConnCmdBuildResult;
        }
        // <Section Id = "ConnCreate">
        /// <summary> 
        /// Create a Connection.
        /// </summary> 
        public long ConnCreate() {
            ConnCreateResult = (long)StateIs.Started;
            ConnCreateResult = ConnCreate(ref Fmain);
            return ConnCreateResult;
        }
        // <Section Id = "ConnCreatePassedConn">
        /// <summary> 
        /// Create the Passed Connection.
        /// </summary> 
        public long ConnCreate(ref FileMainDef FmainPassed) {
            ConnCreatePassedConnResult = (long)StateIs.Started;
            //
            // Current Database Conn Create 
            // and the Database Conn Open
            // are synonymns
            //
            if (FmainPassed.Fs.ServerName.Length == 0
                || FmainPassed.Fs.DatabaseName.Length == 0
                || FmainPassed.Fs.TableNameLine.Length == 0
                || !FmainPassed.DbStatus.bpNameIsValid) {
                FmainPassed.Fs.TableNameLine = TableNameLineBuild(ref FmainPassed);
                FmainPassed.DbIo.ConnString = "";
            }
            if (!FmainPassed.ConnStatus.bpIsCreated
                || !FmainPassed.ConnStatus.bpNameIsValid
                || FmainPassed.Fs.ConnDoReset
                || FmainPassed.DbIo.SqlDbConn == null
                || FmainPassed.DbIo.spConnString.Length == 0
                ) {
                if (!FmainPassed.DbStatus.bpNameIsValid) {
                    if (DoingDefaults) { ConnCreatePassedConnResult = DatabaseFieldsGetDefault(ref FmainPassed); }
                }
                if (FmainPassed.DbIo.spConnString.Length == 0
                    || !FmainPassed.ConnStatus.bpNameIsValid
                    || !FmainPassed.Fs.ConnDoReset) {
                    ConnCreatePassedConnResult = ConnCmdBuild(ref FmainPassed);
                }
            }
            if (DbSyn.spDatabaseFileCreateCmd.Length == 0
                && FmainPassed.DbStatus.bpIsCreating) {
                ConnCreatePassedConnResult = DatabaseCreateCmdBuild();
            }
            if (DbSyn.spConnCreateCmd.Length == 0
                && FmainPassed.DbStatus.bpIsCreating) {
                ConnCreatePassedConnResult = ConnCreateCmdBuild();
            }
            if (FmainPassed.DbIo.SqlDbConn != null && FmainPassed.ConnStatus.bpIsCreated == true) {
                // <Area Id = "WARNING - Already Created">
                ConnCreatePassedConnResult = (long)StateIs.Successful;
                FmainPassed.ConnStatus.ipIsConnectingResult = (long)StateIs.Successful;
                return ConnCreatePassedConnResult;
            } else {
                // <Area Id = "not Created">
                FmainPassed.ConnStatus.ipIsConnectingResult = (long)StateIs.InProgress;
                ConnCreatePassedConnResult = FmainPassed.ConnStatus.ipIsConnectingResult;
                FmainPassed.ConnStatus.bpIsCreating = true;
                while (FmainPassed.ConnStatus.bpIsCreating) {
                    // <Area Id = "Connect">
                    try {
                        if (FmainPassed.DbIo.SqlDbConn == null) {
                            FmainPassed.DbIo.SqlDbConn = new SqlConnection(FmainPassed.DbIo.spConnString);
                        }
                        FmainPassed.DbIo.SqlDbConn.ConnectionString = FmainPassed.DbIo.ConnString;
                        // FmainPassed.DbIo.SqlDbConn.Database = FmainPassed.Fs.DatabaseName;
                        // FmainPassed.DbIo.SqlDbConn.ConnectionTimeout = FmainPassed.DbIo.SqlDbCommandTimeout;
                        // FmainPassed.DbIo.SqlDbConn = PassedSqlDbConnection;
                        // FmainPassed.DbIo.SqlDbConn.Open();
                        // FmainPassed.DbIo.SqlDbConn.Close();
                        FmainPassed.ConnStatus.ipIsConnectingResult = (long)StateIs.Successful;
                        ConnCreatePassedConnResult = (long)StateIs.Successful;
                        FmainPassed.ConnStatus.bpDoesExist = true;
                        FmainPassed.ConnStatus.bpIsCreating = false;
                        FmainPassed.ConnStatus.bpIsCreated = true;
                        FmainPassed.ConnStatus.bpIsInitialized = true;
                        FmainPassed.Fs.ConnDoReset = false;
                    } catch (SqlException ExceptionSql) {
                        LocalMessage.ErrorMsg = "";
                        ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, ConnCloseResult);
                        FmainPassed.ConnStatus.ipIsConnectingResult = (long)StateIs.Failed;
                        ConnCreatePassedConnResult = (long)StateIs.Failed;
                        //
                        ExeceptConnCreateImpl(ref FmainPassed, ref ExceptionSql);
                        FmainPassed.ConnStatus.bpIsCreating = false;
                        FmainPassed.ConnStatus.bpIsCreated = false;
                    } catch (Exception ExceptionGeneral) {
                        LocalMessage.ErrorMsg = "";
                        ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, ConnCloseResult);
                        FmainPassed.ConnStatus.ipIsConnectingResult = (long)StateIs.Failed;
                        ConnCreatePassedConnResult = (long)StateIs.Failed;
                        ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                        FmainPassed.ConnStatus.bpIsCreating = false;
                        FmainPassed.ConnStatus.bpIsCreated = false;
                    } finally {
                        if (FmainPassed.ConnStatus.ipIsConnectingResult == (long)StateIs.InProgress) {
                            ConnCreatePassedConnResult = (long)StateIs.Failed;
                        }
                        FmainPassed.ConnStatus.ipIsConnectingResult = ConnCreatePassedConnResult;
                        Fmain.FileStatus.ipStatusCurrent = ConnCreatePassedConnResult;
                    } // of try connect
                } // of is Creating
            } // is already Created
            FmainPassed.ConnStatus.IsCreatingResult = ConnCreatePassedConnResult;
            return ConnCreatePassedConnResult;
        }
        // <Section Id = "ConnCreateCmdBuild">
        /// <summary> 
        /// Build a Connection Create Command.
        /// </summary> 
        public long ConnCreateCmdBuild() {
            ConnCreateCmdBuildResult = (long)StateIs.Started;
            DbSyn.spConnCreateCmd = "Connection are dynamic";

            ConnCreateCmdBuildResult = (long)StateIs.Successful;
            return ConnCreateCmdBuildResult;

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
            ConnCreateCmdBuildResult = (long)StateIs.Successful;
            return ConnCreateCmdBuildResult;
        }
        #endregion
        #region ConnError
        // <Section Id = "x
        /// <summary> 
        /// Exception handling for Connetion Creation.
        /// </summary> 
        public void ExeceptConnCreateImpl(ref SqlException ExceptionSql) {
            ExeceptConnCreateImpl(ref Fmain, ref ExceptionSql);
        }
        /// <summary> 
        /// Exception handling for Passed Connection Creation.
        /// </summary> 
        public void ExeceptConnCreateImpl(ref FileMainDef FmainPassed, ref SqlException ExceptionSql) {
            ConnectionCreateResult = (long)StateIs.Started;
            Sys.sMessageBoxMessage = MdmProcessTitle + "\n" + @"File Creation Status";
            Sys.sMessageBoxMessage += "\n" + @"Create Connection error!";
            Sys.sMessageBoxMessage += "\n" + @"SQL Exception Error";
            Sys.sMessageBoxMessage += "\n" + ExceptionSql.ToString();
            // <Area Id = "display message
        }
        #endregion
        #endregion
        // Database File - xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region DatabaseFile
        #region Database Reset
        // <Section Id = "x
        /// <summary> 
        /// Reset the Database.
        /// </summary> 
        public long DatabaseReset(ref FileMainDef FmainPassed) {
            DatabaseResetResult = (long)StateIs.Started;
            DatabaseClear(ref FmainPassed);
            DatabaseResetResult = ConnReset(ref FmainPassed);
            return DatabaseResetResult;
        }
        #endregion
        #region Database Table Name Validate
        // <Section Id = "SQL File Handling
        /// <summary> 
        /// Set the default field values for the Database.
        /// </summary> 
        public long DatabaseFieldsGetDefault(ref FileMainDef FmainPassed) {
            DatabaseFieldsGetDefaultsResult = (long)StateIs.Started;
            bool CopyIsDone = false;
            DoingDefaults = true;
            // System
            if (FmainPassed.Fs.SystemName.Length == 0) {
                if (!CopyIsDone) { CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.SystemName = SystemNameGetDefault(ref Faux);
            }
            // Service
            if (FmainPassed.Fs.ServiceName.Length == 0) {
                if (!CopyIsDone) { CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.ServiceName = ServiceNameGetDefault(ref Faux);
            }
            // Server
            if (FmainPassed.Fs.ServerName.Length == 0) {
                if (!CopyIsDone) { CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.ServerName = ServerNameGetDefault(ref Faux);
            }
            // Database
            if (FmainPassed.Fs.DatabaseName.Length == 0) {
                if (!CopyIsDone) { CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.DatabaseName = DatabaseNameGetDefault(ref Faux);
            }
            // FileOwnerName
            if (FmainPassed.Fs.FileOwnerName.Length == 0) {
                if (!CopyIsDone) { CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.FileOwnerName = FileOwnerGetDefault(ref Faux);
            }
            //
            DoingDefaults = false;
            return (DatabaseFieldsGetDefaultsResult = (long)StateIs.Successful);
        }
        #endregion
        #region Database Table Name Build
        // <Section Id = "TableNameLineBuild">
        /// <summary> 
        /// Build the Table Name Line for the Passed Table.
        /// </summary> 
        public String TableNameLineBuild(ref FileMainDef FmainPassed) {
            TableNameLineBuildResult = (long)StateIs.Started;
            if (!DoingDefaults) { TableNameLineBuildResult = DatabaseFieldsGetDefault(ref FmainPassed); }
            FmainPassed.Fs.TableNameLine = FmainPassed.Fs.TableNameLineBuild(ref FmainPassed);
            //
            FmainPassed.DbStatus.bpNameIsValid = true; // TODO ?? bpNameIsValid where is this used ??
            DatabaseFileLongResult = (long)StateIs.Successful;
            return FmainPassed.Fs.TableNameLine;
        }
        // <Section Id = "TableNameLineBuild">
        /// <summary> 
        /// Build the Table Name Line for this Table.
        /// </summary> 
        public long TableNameLineBuild() {
            DatabaseFileLongResult = (long)StateIs.Started;
            Fmain.Fs.TableNameLine = TableNameLineBuild(ref Fmain);
            return (DatabaseFileLongResult = (long)StateIs.Successful);
        }
        #region Database Create Command Build
        // <Section Id = "DatabaseCreateCmdBuild">
        /// <summary> 
        /// Build the Create Command for the Database.
        /// </summary> 
        public long DatabaseCreateCmdBuild() {
            DatabaseCreateCmdBuildResult = (long)StateIs.Started;
            //
            // DbMasterSyn.bpDbFilePhraseUseIsUsed
            // DbMasterSyn.bpDbFilePhraseIfIsUsed
            // DbMasterSyn.bpDbFilePhraseSelectIsUsed
            // DbMasterSyn.bpDbFilePhraseFromIsUsed
            // DbMasterSyn.bpDbFilePhraseWhereIsUsed
            // DbMasterSyn.bpDbFilePhraseBeginIsUsed
            // DbMasterSyn.bpDbFilePhraseDropIsUsed
            // DbMasterSyn.bpDbFilePhraseCreateIsUsed
            // 


            DbMasterSyn.MstrDbDatabaseCreateCmd = "";
            if (DbMasterSyn.bpDbFilePhraseUseIsUsed) {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseUse;
                DbMasterSyn.MstrDbDatabaseCreateCmd += Fmain.DbMaster.spMstrDbFileDb;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseUseEnd;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
            }
            if (DbMasterSyn.bpDbFilePhraseIfIsUsed) {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseIf;
            }
            if (DbMasterSyn.bpDbFilePhraseSelectIsUsed) {
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
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseCreateTable + " ";

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
            return (long)StateIs.NormalEnd;
        }
        // <Section Id = "x
        #endregion
        #endregion
        #region Database Creation
        /// <summary> 
        /// Exceptions handling for Create Command handling.
        /// </summary> 
        public void ExceptTableCreationImpl(ref SqlException ExceptionSql) {
            ExceptTableCreationImpl(ref Fmain, ref ExceptionSql);
        }
        /// <summary> 
        /// Exception handling for Passed File on Create Command handling.
        /// </summary> 
        /// <param name="ExceptionSql"></param> 
        public void ExceptTableCreationImpl(ref FileMainDef FmainPassed, ref SqlException ExceptionSql) {
            DatabaseFileCreationErrorResult = (long)StateIs.Started;
            FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.DatabaseError;
            Sys.sMessageBoxMessage = MdmProcessTitle + "\n" + @"File Creation Status";
            Sys.sMessageBoxMessage += "\n" + @"Creation error!";
            Sys.sMessageBoxMessage += "\n" + @"SQL Exception Error";
            Sys.sMessageBoxMessage += "\n" + ExceptionSql.ToString();
            // <Area Id = "display message
        }
        #endregion
        #region Database Deletion
        #endregion
        #region Database Close
        /// <summary> 
        /// Exceptions handling for Table Close Command handling.
        /// </summary> 
        public void ExceptTableCloseImpl(ref SqlException ExceptionSql) {
            ExceptTableCloseImpl(ref Fmain, ref ExceptionSql);
        }
        /// <summary> 
        /// Exceptions handling for Table Close Command handling for Passed File.
        /// </summary> 
        public void ExceptTableCloseImpl(ref FileMainDef FmainPassed, ref SqlException ExceptionSql) {
            DatabaseFileCloseErrorResult = (long)StateIs.Started;
            Sys.sMessageBoxMessage = MdmProcessTitle + "\n" + @"File Creation Status";
            Sys.sMessageBoxMessage += "\n" + @"Close Connection error!";
            Sys.sMessageBoxMessage += "\n" + @"SQL Exception Error";
            Sys.sMessageBoxMessage += "\n" + ExceptionSql.ToString();
            // <Area Id = "display message
        }
        #endregion
        #region Database File General Exceptions
        //                         } catch (NotSupportedException ExceptionNotSupported) {
        /// <summary> 
        /// Exceptions - General - Not Supported.
        /// </summary> 
        public void ExceptTableGeneralImpl(ref NotSupportedException ExceptionNotSupported) {
            ExceptionDatabaseFileGeneralResult = (long)StateIs.Started;
            ExceptTableGeneralImpl(ref Fmain, ref ExceptionGeneral);
        }
        /// <summary> 
        /// Exceptions - General - Not Support for Passed File.
        /// </summary> 
        public void ExceptTableGeneralImpl(ref FileMainDef FmainPassed, ref NotSupportedException ExceptionNotSupported) {
            ExceptionDatabaseFileGeneralResult = (long)StateIs.Started;
            ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
        }
        /// <summary> 
        /// Exceptions - General
        /// Main Method for all exceptions.
        /// Exceptions being move to base classes
        /// including utility classes such as Mfile.
        /// </summary> 
        public void ExceptTableGeneralImpl(ref Exception ExceptionGeneral) {
            ExceptTableGeneralImpl(ref Fmain, ref ExceptionGeneral);
        }
        /// <summary> 
        /// Exceptions - General for the Passed File.
        /// Main Method for all exceptions.
        /// Exceptions being move to base classes
        /// including utility classes such as Mfile.
        /// </summary> 
        public void ExceptTableGeneralImpl(ref FileMainDef FmainPassed, ref Exception ExceptionGeneral) {
            ExceptionDatabaseFileGeneralResult = (long)StateIs.Started;
            FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.DatabaseError;
            // sMessageBoxMessage = MdmProcessTitle + "\n" + @"File Creation Status";
            // sMessageBoxMessage += "\n" + @"General Exception Error!";
            // sMessageBoxMessage += "\n";
            // try {
            // sMessageBoxMessage += "\n" + ExceptionNotSupported.ToString();
            //             } catch { ; }
            // XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(),PickSystemCallStringResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sMessageBoxMessage + "\n");
            // <Area Id = "display message
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        #region $include Text based Data Record Parsing
        // <Section Id = "Ascii File Handling
        // <Section Id = "Read and return a data record array
        public MdmRecord Mrecord;
        public long AsciiFileReadRecordResult;
        /// <summary> 
        /// Get the Ascii File Directory List.
        /// </summary> 
        public Dictionary<String, Object> AsciiFileReadDirList(ref FileMainDef FmainPassed) {
            AsciiFileReadRecordResult = (long)StateIs.Started;
            AsciiFileReadAll(ref FmainPassed);
            Dictionary<String, Object> MrecDict = new Dictionary<string, object>();

            return MrecDict;
        }
        /// <summary> 
        /// Read an Ascii File Data Record.
        /// </summary> 
        public long AsciiFileReadRecord(ref FileMainDef FmainPassed) {
            AsciiFileReadRecordResult = (long)StateIs.Started;
            // FmainPassed.Fs.FileId.FileNameLine = FmainPassed.Fs.FileId.PropSystemPath;
            AsciiFileReadAll(ref FmainPassed);
            Mrecord = new MdmRecord(ref FmainPassed);

            return (AsciiFileReadRecordResult = (long)StateIs.Finished);
        }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        #region Ascii Data File
        #region Ascii Read
        // <Section Id = "Check and ReadAll and ReadLine AsciiFile
        /// <summary> 
        /// Ascii Read All Content for Item.
        /// </summary> 
        public long AsciiFileReadAll(ref FileMainDef FmainPassed) {
            // <Area Id = "bRead the IoAll of text
            AsciiFileReadAllResult = (long)StateIs.Started;
            FmainPassed.Fs.FileId.FileName = FmainPassed.Fs.FileId.FileName;
            if (FmainPassed.Fs.FileId.FileNameLine.Length == 0) {
                FmainPassed.Fs.FileId.FileNameLine =
                    FileNameLineBuild(ref FmainPassed);
            }
            // <Area Id = "Check File Stream; 
            AsciiFileReadAllResult = AsciiFileFileStreamReaderCheck(ref FmainPassed);
            // <Area Id = "bRead All Lines if Stream OK;
            if (AsciiFileReadAllResult == (long)StateIs.Successful) {
                AsciiFileReadAllResult = (long)StateIs.InProgress;
                try {
                    //
                    FmainPassed.FileStatus.bpItemIsAtEnd = true;
                    FmainPassed.Fs.FileIo.spIoReadBuffer =
                    System.IO.File.ReadAllText(FmainPassed.Fs.FileId.FileNameLine);
                    //
                    FmainPassed.FileStatus.bpDoesExist = true;
                    FmainPassed.Buf.BytesRead = FmainPassed.Fs.FileIo.IoReadBuffer.Length;
                    // FmainPassed.Buf.BytesReadTotal = Fmain.Buf.BytesRead;
                    FmainPassed.Fs.FileIo.spIoAll += FmainPassed.Fs.FileIo.spIoReadBuffer;
                    FmainPassed.Buf.BytesReadTotal = FmainPassed.Fs.FileIo.spIoAll.Length;
                    // TODO X Use of spIoAll is in question vs IoReadBuffer
                    // TODO X Should IoReadBuffer be cleared or IoAll not loaded?
                    // TODO X Most likely is that IoAll should not be loaded or flag s/b used...
                    // iItemDataCharEobIndex = Item.ItemData.Length; // End of Character Buffer
                    // if (Fmain.Buf.ItemIsAtEnd == true) {
                    // iItemDataCharEofIndex = iItemDataCharEobIndex;
                    // }
                    AsciiFileReadAllResult = (long)StateIs.Successful;
                    FmainPassed.FileStatus.ipStatusCurrent = (long)StateIs.Successful;
                } catch (Exception ExceptionGeneral) {
                    AsciiFileReadAllResult = (long)StateIs.Failed;
                    FmainPassed.FileStatus.bpDoesExist = false;
                    FmainPassed.Fs.FileIo.IoReadBuffer = "";
                    FmainPassed.Buf.BytesRead = 0;
                    // FmainPassed.Buf.BytesConverted = 0;
                    // FmainPassed.Buf.BytesConvertedTotal = 0;
                    // iItemDataCharEobIndex = Item.ItemData.Length; // End of Character Buffer
                    // if (FmainPassed.Buf.ItemIsAtEnd == true) {
                    // iItemDataCharEofIndex = iItemDataCharEobIndex;
                    // }
                    if (FmainPassed.Fs.FileIo.DbFileStreamReaderObject == null) {
                        FmainPassed.FileStatus.ipStatusCurrent = (long)StateIs.Failed;
                    } else {
                        FmainPassed.FileStatus.ipStatusCurrent = (long)StateIs.Failed;
                    }
                    //
                    ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, AsciiFileReadAllResult);
                    ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                } finally {
                    // XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(),PickSystemCallStringResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + "Executing finally block." + "\n");
                }
            } else {
                FmainPassed.Fs.FileIo.IoReadBuffer = "";
                FmainPassed.Buf.BytesRead = 0;
                // FmainPassed.Buf.BytesConverted = 0;
                // FmainPassed.Buf.BytesConvertedTotal = 0;
            }
            return AsciiFileReadAllResult;
        }
        // <Section Id = "x
        /// <summary> 
        /// Ascii Read Line for Item.
        /// </summary> 
        public long AsciiFileReadLine(ref FileMainDef FmainPassed) {
            // <Area Id = "bRead All Lines if Stream OK;
            AsciiFileReadLineResult = (long)StateIs.Started;
            FmainPassed.Fs.FileId.FileName = FmainPassed.Fs.FileId.FileName;
            if (FmainPassed.Fs.FileId.spFileNameFull.Length == 0) {
                if (FmainPassed.Fs.FileId.FileNameLine.Length > 0) {
                    FmainPassed.Fs.FileId.spFileNameFull = FmainPassed.Fs.FileId.FileNameLine;
                }
            }
            // <Area Id = "Check File Stream; 
            AsciiFileReadLineResult = AsciiFileFileStreamReaderCheck(ref Fmain);
            // <Area Id = "bRead All Lines if Stream OK;
            if (AsciiFileReadLineResult == (long)StateIs.Successful) {
                AsciiFileReadLineResult = (long)StateIs.InProgress;
                try {
                    Fmain.Fs.FileIo.IoReadBuffer = Fmain.Fs.FileIo.DbFileStreamReaderObject.ReadLine();
                    Fmain.Fs.FileIo.IoLine += Fmain.Fs.FileIo.IoReadBuffer;
                    // <Area Id = "Continue to read until you reach end of file
                    Fmain.FileStatus.ipStatusCurrent = (long)StateIs.Successful;
                    AsciiFileReadLineResult = (long)StateIs.Successful;
                } catch (Exception ExceptionGeneral) {
                    AsciiFileReadLineResult = (long)StateIs.Failed;
                    LocalMessage.ErrorMsg = "";
                    ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, AsciiFileReadLineResult);
                    ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                    if (Fmain.Fs.FileIo.DbFileStreamReaderObject == null) {
                        Fmain.FileStatus.ipStatusCurrent = (long)StateIs.OsError;
                    } else {
                        Fmain.FileStatus.ipStatusCurrent = (long)StateIs.Failed;
                        if (Fmain.Fs.FileIo.IoReadBuffer.Length == 0) {
                            Fmain.FileStatus.ipStatusCurrent = (long)StateIs.Finished;
                        }
                    }
                    Fmain.Fs.FileIo.IoReadBuffer = "";
                    // spIoLine = "";
                } finally {
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PickSystemCallStringResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + "Executing finally block." + "\n");
                }
            }
            return AsciiFileReadLineResult;
        }
        // <Section Id = "public String Check and bRead Database
        /// <summary> 
        /// Check the Ascii File Stream Reader.
        /// </summary> 
        public long AsciiFileFileStreamReaderCheck(ref FileMainDef FmainPassed) {
            AsciiFileFileStreamReaderCheckResult = (long)StateIs.Started;
            FmainPassed.DbStatus.bpIsCreating = true;
            FmainPassed.Fs.FileId.FileName = FmainPassed.Fs.FileId.FileName;
            if (FmainPassed.Fs.FileId.spFileNameLine.Length == 0) {
                if (FmainPassed.Fs.FileId.spFileNameFull.Length > 0) {
                    FmainPassed.Fs.FileId.spFileNameLine = FmainPassed.Fs.FileId.spFileNameFull;
                }
            }
            // spIoBlock = "";
            try {
                // 
                if (FmainPassed.Fs.FileIo.DbFileStreamReaderObject == null) {
                    if (FmainPassed.Fs.FileId.spFileNameLine != null) {
                        //Pass the file path and file name to the StreamReader constructor
                        FmainPassed.Fs.FileIo.DbFileStreamReaderObject = new StreamReader(FmainPassed.Fs.FileId.spFileNameLine);
                    }
                }
            } catch (Exception e) {
                // Error handling code
            }
            if (FmainPassed.Fs.FileIo.DbFileStreamReaderObject == null) {
                AsciiFileFileStreamReaderCheckResult = (long)StateIs.Failed;
                FmainPassed.DbStatus.bpHadError = true;
            } else {
                AsciiFileFileStreamReaderCheckResult = (long)StateIs.Successful;
                FmainPassed.DbStatus.bpIsValid = true;
                FmainPassed.DbStatus.bpIsCreated = true;
                // FmainPassed.DbStatus.bpIsInitialized = true;
                // FmainPassed.DbStatus.bpIsOpen = true;
                // FmainPassed.DbStatus.bpIsOpen= true;
                // FmainPassed.DbStatus.bpDoesExist = true;

            }
            FmainPassed.FileStatus.ipStatusCurrent = AsciiFileFileStreamReaderCheckResult;
            FmainPassed.DbStatus.bpIsCreating = false;
            return AsciiFileFileStreamReaderCheckResult;
        }
        // <Section Id = "public long Check and bRead Binary (AsciiFileReadBlockSeek)
        /// <summary> 
        /// Read a Block of Data from the Ascii File.
        /// </summary> 
        public String AsciiFileReadBlock(ref FileMainDef FmainPassed) {
            // <Area Id = "bRead the Win32 Seek Block from Handle
            AsciiFileReadBlockResult = (long)StateIs.Started;

            // <Area Id = "Check File Stream; 
            AsciiFileReadBlockResult = AsciiFileFileStreamReaderCheck(ref FmainPassed);
            // <Area Id = "bRead All Lines if Stream OK;
            if (AsciiFileReadBlockResult == (long)StateIs.Successful) {
                AsciiFileReadBlockResult = (long)StateIs.InProgress;
                try {
                    // <Area Id = " do a standard read all for now (not Win32 read)
                    // spIoBlock = 
                    AsciiFileReadBlockResult = AsciiFileReadAll(ref FmainPassed);
                    AsciiFileReadBlockResult = (long)StateIs.Successful;
                } catch (Exception ExceptionGeneral) {
                    LocalMessage.ErrorMsg = "";
                    ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, AsciiFileReadBlockResult);
                    AsciiFileReadBlockResult = (long)StateIs.Failed;
                    //
                    ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                    if (Fmain.Fs.FileIo.DbFileStreamReaderObject == null) {
                        Fmain.FileStatus.ipStatusCurrent = (long)StateIs.Failed;
                    } else {
                        Fmain.FileStatus.ipStatusCurrent = (long)StateIs.Failed;
                    }
                } finally {
                    XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + "Executing finally block.");
                }
            }
            return Fmain.Fs.FileIo.spIoBlock;
        }
        // <Section Id = "public long Check and bRead Binary (AsciiFileReadBlockSeek)
        /// <summary> 
        /// Read a Block of Data by Seeking in the Ascii File.
        /// </summary> 
        public String AsciiFileReadBlockSeek(ref FileMainDef FmainPassed) {
            // <Area Id = "bRead the Win32 Seek Block from Handle
            AsciiFileReadBlockSeekResult = (long)StateIs.Started;

            // <Area Id = "Check File Stream; 
            AsciiFileReadBlockSeekResult = AsciiFileFileStreamReaderCheck(ref FmainPassed);
            // <Area Id = "bRead All Lines if Stream OK;
            if (AsciiFileReadBlockSeekResult == (long)StateIs.Successful) {
                AsciiFileReadBlockSeekResult = (long)StateIs.InProgress;
                try {
                    // <Area Id = " do a standard read all for now (not Win32 read)
                    // spIoBlock = 
                    AsciiFileReadBlockSeekResult = AsciiFileReadAll(ref FmainPassed);
                    AsciiFileReadBlockSeekResult = (long)StateIs.Successful;
                } catch (Exception ExceptionGeneral) {
                    LocalMessage.ErrorMsg = "";
                    ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, AsciiFileReadBlockSeekResult);
                    //
                    ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                    AsciiFileReadBlockSeekResult = (long)StateIs.Failed;
                    if (Fmain.Fs.FileIo.DbFileStreamReaderObject == null) {
                        Fmain.FileStatus.ipStatusCurrent = (long)StateIs.Failed;
                    } else {
                        Fmain.FileStatus.ipStatusCurrent = (long)StateIs.Failed;
                    }
                } finally {
                    XUomMovvXv.MessageMdmSendToPageNewLine(Sender, "A2" + "Executing finally block.");
                }
            }
            return Fmain.Fs.FileIo.spIoBlock;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Ascii Write
        // <Section Id = "AsciiFileWritePassedName">
        /// <summary> 
        /// Write the Ascii File Data.
        /// </summary> 
        public long AsciiFileWrite() {
            AsciiFileWriteResult = (long)StateIs.Started;
            AsciiFileWriteResult = AsciiFileWrite(ref Fmain);
            return AsciiFileWriteResult;
        }
        // <Section Id = "AsciiFileWritePassedName">
        /// <summary> 
        /// Write the Ascii File Data for the Passed File.
        /// </summary> 
        public long AsciiFileWrite(ref FileMainDef FmainPassed) {
            AsciiFileWritePassedNameResult = (long)StateIs.Started;
            if (FmainPassed.Fs.FileId.spFileNameLine.Length == 0) {
                FmainPassed.Fs.FileId.spFileNameLine = FileNameLineBuild(ref FmainPassed);
            }
            // TODO WRITE ASCII DATA HERE!!!
            AsciiFileWritePassedNameResult = (long)StateIs.UnknownFailure;
            return AsciiFileWritePassedNameResult;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Ascii Close
        // <Section Id = "x
        /// <summary> 
        /// Close the Passed Ascii File
        /// </summary> 
        public long AsciiFileClose(ref FileMainDef FmainPassed) {
            AsciiFileCloseResult = (long)StateIs.Started;
            // Console.ReadLine();
            // <Area Id = "close the file streams
            if (FmainPassed.Fs.FileIo.DbFileStreamReaderObject != null) {
                FmainPassed.Fs.FileIo.DbFileStreamReaderObject.Close();
            }
            if (FmainPassed.Fs.FileIo.DbFileStreamObject != null) {
                FmainPassed.Fs.FileIo.DbFileStreamObject.Close();
            }
            //close the file
            if (FmainPassed.Fs.FileIo.DbFileObject != null) {
                AsciiFileCloseResult = AsciiFileClear(ref FmainPassed);
                // <Area Id = "do destructor;
            }
            return AsciiFileCloseResult;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Ascii Delete
        // <Section Id = "AsciiFileDeletePassedName">
        /// <summary> 
        /// Delete the Ascii File Data or Item.
        /// </summary> 
        public long AsciiFileDelete() {
            AsciiFileDeleteResult = (long)StateIs.Started;
            AsciiFileDeleteResult = AsciiFileDelete(ref Fmain);
            return AsciiFileDeleteResult;
        }
        // <Section Id = "AsciiFileDeletePassedName">
        /// <summary> 
        /// Delete the Ascii File Data or Item for the Passed File.
        /// </summary> 
        public long AsciiFileDelete(ref FileMainDef FmainPassed) {
            AsciiFileDeletePassedNameResult = (long)StateIs.Started;
            if (FmainPassed.Fs.FileId.spFileNameLine.Length == 0) {
                FmainPassed.Fs.FileId.spFileNameLine = FileNameLineBuild(ref FmainPassed);
            }
            // Delete the data here
            AsciiFileDeletePassedNameResult = (long)StateIs.UnknownFailure;
            return AsciiFileDeletePassedNameResult;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Ascii Clear
        // <Section Id = "AsciiFileClear">
        /// <summary> 
        /// Clear the Ascii File Data.
        /// </summary> 
        public long AsciiFileClear(ref FileMainDef FmainPassed) {
            AsciiFileClearResult = (long)StateIs.Started;
            //
            /*/ 
            TableNameLine = TableNameLineBuild(ref Fs.DatabaseName, ref Fs.spFileOwnerName, ref Fs.spFileName);
            if (spIoAll == null) {
                FmainPassed.Fs.FileIo.IoReadBuffer = "";
                spIoBlock = "";
                spIoLine = "";
                spIoAll = "";
            }
            /*/
            FmainPassed.Fs.FileIo.DbFileObject = null;
            FmainPassed.Fs.FileIo.DbFileStreamObject = null;
            FmainPassed.Fs.FileIo.DbFileStreamReaderObject = null;
            FmainPassed.DbIo.SqlDbConn = null;
            FmainPassed.DbIo.SqlDbCommand = null;
            FmainPassed.DbIo.CommandCurrent = null;
            /*/
            bDbDatabaseDoesExist = false;
            bDbDatabaseIsInvalid = false;
            bDbDatabaseIsCreating = false;
            bDbDatabaseIsCreated = false;

            // <Area Id = "DatabaseErrorObject">
            DbFileCmdOsException = null;
            ExceptSql = null;
            /*/
            FmainPassed.Fs.DataClear();
            //
            // FileData
            FmainPassed.Fs.FileIo.IoReadBuffer = "";
            FmainPassed.Fs.FileIo.spIoBlock = "";
            FmainPassed.Fs.FileIo.spIoLine = "";
            FmainPassed.Fs.FileIo.spIoAll = "";

            return AsciiFileClearResult;
        }
        // <Section Id = "AsciiFileCreatePassedName">
        #endregion
        #region Ascii Create
        /// <summary> 
        /// Create Ascii File.
        /// </summary> 
        public long AsciiFileCreate() {
            AsciiFileCreateResult = (long)StateIs.Started;
            AsciiFileCreateResult = AsciiFileCreate(ref Fmain);
            return AsciiFileCreateResult;
        }
        // <Section Id = "AsciiFileCreatePassedName">
        /// <summary> 
        /// Create the Passed Ascii File.
        /// </summary> 
        public long AsciiFileCreate(ref FileMainDef FmainPassed) {
            AsciiFileCreatePassedNameResult = (long)StateIs.Started;
            if (FmainPassed.Fs.FileId.spFileNameLine.Length == 0) {
                FmainPassed.Fs.FileId.spFileNameLine = FileNameLineBuild(ref FmainPassed);
            }
            // Create the data here
            AsciiFileCreatePassedNameResult = (long)StateIs.Failed;
            return AsciiFileCreatePassedNameResult;
        }
        // <Section Id = "AsciiFileReset">
        #endregion
        #region Ascii Reset
        /// <summary> 
        /// Reset the Ascii File Data and Fields.
        // File
        //  Ascii
        //  Text
        //  Binary
        // Database File
        //  Sql
        //      MS Sql
        //      MY Sql
        //  Db2
        //  Pick
        // xxxxxxxxxxxxxxxxxxxxxxx
        // File XXXX is virtual and will be overriden in the
        // subclasses when implemented
        // Therefore SqlOpen, AsciiFileOpen and TextFileOpen become FileOpen
        // in the SqlFile, AsciiFile, TextFile classes
        //
        /// </summary> 
        public long AsciiFileReset(ref FileMainDef FmainPassed) {
            AsciiFileResetResult = (long)StateIs.Started;
            // if (Faux.FileStatus.bpIsInitialized) {
            // THIS IS A DISPOSE FUNCTION
            Faux.FileStatus.bpIsInitialized = false;
            // }
            return AsciiFileResetResult;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        #region Base File Open Read Write Close
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region FileNameBuild
        // <Section Id = "FileNameLineBuild">
        public long FileNameLineBuildResult;
        /// <summary> 
        /// Build the File Name Line from the File Info for the Passed File.
        /// </summary> 
        public String FileNameLineBuild(ref FileMainDef FmainPassed) {
            FileNameBuildFullResult = (long)StateIs.Started;
            Fmain.Fs.FileId.FileNameLine = FmainPassed.Fs.FileId.FileNameLineBuild(ref FmainPassed);
            return Fmain.Fs.FileId.FileNameLine;
        }
        // <Section Id = "FileNameLineBuild">
        public long FileNameBuildFullResult;
        /// <summary> 
        /// Build the File Name Line from the File Info.
        /// </summary> 
        public long FileNameLineBuild() {
            FileNameBuildFullResult = (long)StateIs.Started;
            Fmain.Fs.FileId.FileNameLine = FileNameLineBuild(ref Fmain);
            return (FileNameBuildFullResult = (long)StateIs.Successful);
        }
        #endregion
        #region READ
        /// <summary> 
        /// Perform a low level file seek on file.
        /// </summary> 
        public virtual long FileSeek(ref FileMainDef FmainPassed, int iPassedOffsetModulo, int iPassedOffsetRemainder, long iPassedFileSeekMode) {
            // 
            //  TODO z$RelVs? (when needed) FileSeek SEEK Read a Buffer / Text block from Win32 File Handle
            // 
            long FileSeek = (long)StateIs.Started;
            FmainPassed.Buf.BytesRead = 0;
            // FmainPassed.Buf.BytesConverted = 0;
            // FmainPassed.Buf.BytesConvertedTotal = 0;
            FileSeek = (long)StateIs.Failed;
            //
            return FileSeek;
        }

        /// <summary> 
        /// Perform a read line.
        /// </summary> 
        public virtual long FileReadLine(ref FileMainDef FmainPassed, String PassedDosRecordBuffer, int iPassedRecordSize) {
            // 
            // TODO $$$CHECK Buf.FileReadLine Read Line from Ascii File
            // 
            FileReadLineResult = (long)StateIs.Started;
            if (true == false) {
                FmainPassed.Buf.BytesRead = 0;
                // FmainPassed.Buf.BytesConverted = 0;
                // FmainPassed.Buf.BytesConvertedTotal = 0;
            }
            FmainPassed.Fs.FileIo.IoReadBuffer = "";
            // o+fPassedFileObject.FileIo.IoReadBuffer

            FileReadLineResult = AsciiFileReadAll(ref FmainPassed);
            // FmainPassed.IoAll += PassedDosRecordBuffer;
            // FmainPassed.IoLine += PassedDosRecordBuffer;
            if (true == false) {
                FmainPassed.Buf.BytesRead = FmainPassed.Fs.FileIo.IoReadBuffer.Length;
                // FmainPassed.Buf.BytesConverted = 0;
                // FmainPassed.Buf.BytesConvertedTotal = 0;
            }
            if (FmainPassed.Buf.BytesRead < 1) {
                FileReadLineResult = (long)StateIs.Failed;
            } else {
                FileReadLineResult = (long)StateIs.Successful;
                FmainPassed.Buf.FileWorkBuffer += FmainPassed.Fs.FileIo.IoReadBuffer;
                if (true == false) {
                    FmainPassed.Buf.BytesReadTotal += FmainPassed.Buf.BytesRead;
                    // FmainPassed.Buf.BytesConverted = 0;
                    // FmainPassed.Buf.BytesConvertedTotal = 0;
                }
                FmainPassed.Buf.FileWorkBuffer += FmainPassed.Fs.FileIo.IoReadBuffer;
            }
            FmainPassed.Buf.CharMaxIndex = FmainPassed.Buf.FileWorkBuffer.Length;
            //
            return FileReadLineResult;
        }

        /// <summary> 
        /// Perform a read all content for item.
        /// </summary> 
        public virtual long FileReadAll(ref FileMainDef FmainPassed, ref String PassedDosRecordBuffer, int iPassedRecordSize) {
            // 
            // Read All Lines from Ascii File
            // 
            FileReadAllResult = (long)StateIs.Started;
            if (true == false) {
                FmainPassed.Buf.BytesRead = 0;
                // FmainPassed.Buf.BytesConverted = 0;
                // FmainPassed.Buf.BytesConvertedTotal = 0;
            }
            FmainPassed.Fs.FileIo.IoReadBuffer = "";
            //
            FileReadAllResult = AsciiFileReadAll(ref FmainPassed);
            //
            if (true == false) {
                FmainPassed.Buf.BytesRead = FmainPassed.Fs.FileIo.IoReadBuffer.Length;
                // FmainPassed.Buf.BytesConverted = 0;
                // FmainPassed.Buf.BytesConvertedTotal = 0;
            }
            // FmainPassed.FileWorkBuffer = FmainPassed.Fs.FileIo.IoReadBuffer;
            if (FmainPassed.Buf.BytesRead < 1) {
                FileReadAllResult = (long)StateIs.Failed;
            } else {
                FileReadAllResult = (long)StateIs.Successful;
                FmainPassed.Buf.FileWorkBuffer += FmainPassed.Fs.FileIo.IoReadBuffer;
                if (true == false) {
                    FmainPassed.Buf.BytesReadTotal += FmainPassed.Buf.BytesRead;
                    // FmainPassed.Buf.BytesConverted = 0;
                    // FmainPassed.Buf.BytesConvertedTotal = 0;
                }
            }
            FmainPassed.Buf.CharMaxIndex = FmainPassed.Buf.FileWorkBuffer.Length;
            //
            return FileReadAllResult;
        }
        #endregion
        #region CLOSE
        /// <summary> 
        /// Close low level file handles, unmanaged objects, etc.
        /// </summary> 
        public virtual long FileCloseHandle(ref FileMainDef FmainPassed) {
            // 
            // Close Win32 Dos File Handle
            // 
            FileCloseHandleResult = (long)StateIs.Started;
            FmainPassed = null;
            //
            return FileCloseHandleResult;
        }
        // TODO $$$CHECK FileReadLineClose Requires correction for handle????
        /// <summary> 
        /// Close the File Line Reader.
        /// </summary> 
        public virtual long FileReadLineClose(ref FileMainDef FmainPassed) {
            // 
            // Close Read Line Ascii File
            //
            FileCloseResult = (long)StateIs.Started;
            FmainPassed = null;
            //
            return FileCloseResult;
        }
        /// <summary> 
        /// Close the File.
        /// </summary> 
        public virtual long FileClose(ref FileMainDef FmainPassed) {
            FileCloseResult = (long)StateIs.Started;
            // ofPassedFileName = "";
            return FileCloseResult;
        }
        #endregion
        #region OPEN
        /// <summary> 
        /// Open a low level file handle.
        /// </summary> 
        public virtual long FileOpenHandle(ref FileMainDef FmainPassed, String PassedName, long iPassedFileOpenMode, long iPassedFileOpenOptions) {
            // 
            // Open Win32 Dos File Handle
            // 
            FileOpenHandleResult = (long)StateIs.Started;
            //
            return FileOpenHandleResult;
        }

        /// <summary> 
        /// Open the file.
        /// </summary> 
        public virtual long FileOpen(ref FileMainDef FmainPassed, String PassedName, long iPassedFileOpenMode, long iPassedFileOpenOptions) {
            // 
            // Open Text Ascii File
            // 
            FileOpenResult = (long)StateIs.Started;
            FmainPassed.Buf.BytesRead = 0;
            FmainPassed.Buf.BytesReadTotal = 0;
            FmainPassed.Buf.BytesConverted = 0;
            FmainPassed.Buf.BytesConvertedTotal = 0;

            FileOpenResult = TextFileOpen(ref FmainPassed);
            if (true == false) {
                FmainPassed.Buf.BytesRead = FmainPassed.Fs.FileIo.IoReadBuffer.Length; // TODO z$NOTE FileOpen this is an open, not a read.
            }
            return FileOpenResult;
        }

        #endregion
        #region WRITE
        /// <summary> 
        /// Write the file data or item.
        /// </summary> 
        public virtual long FileWrite(ref FileMainDef FmainPassed) {
            FileWriteResult = (long)StateIs.Started;
            //
            try {
                switch (FmainPassed.Fs.FileTypeMajorId) {
                    case ((long)FileType_LevelIs.Data):
                        // Handle File Data
                        // TODO z$NOTE FileWrite Empty Records (?null?) are allowed in some types!
                        if (DbSyn.OutputInsert.Length > 0) {
                            switch (FmainPassed.Fs.FileSubTypeMajorId) {
                                case ((long)FileType_SubTypeIs.SQL):
                                case ((long)FileType_SubTypeIs.MS):
                                case ((long)FileType_SubTypeIs.MY):
                                    FileWriteResult = SqlDataInsert(ref FmainPassed);
                                    FmainPassed.Buf.WriteFileCounter += 1;
                                    break;
                                case ((long)FileType_SubTypeIs.TEXT
                                | (long)FileType_SubTypeIs.Tilde):
                                    // Text
                                    break;
                                default:
                                    FileWriteResult = (long)StateIs.Undefined;
                                    LocalMessage.Msg6 = "Main Application - File Subtype (" + FmainPassed.Fs.FileSubTypeId.ToString() + ") not properly set";
                                    throw new NotSupportedException(LocalMessage.Msg6);
                            }
                        }
                        //
                        break;
                    //
                    case ((long)FileType_LevelIs.DictData):
                        // FileDictData
                        // FileDictData
                        // Handle File Schema and Data
                        FmainPassed.ColIndex.ColId = FmainPassed.Item.ItemId;
                        // 
                        // not buffer Attr, s/b output dict item max Attr!
                        // TODO z$NOTE Empty Records (?null?) are allowed in some types!
                        // FileTransformControl.iInputBufferAttrIndex?
                        if (FmainPassed.Item.ItemData.Length > 0) {
                            switch (FmainPassed.Fs.FileSubTypeMajorId) {
                                case ((long)FileType_SubTypeIs.SQL):
                                case ((long)FileType_SubTypeIs.MS):
                                case ((long)FileType_SubTypeIs.MY):
                                    FileWriteResult = SqlDictArrayInsert(ref FmainPassed, "error?");
                                    FmainPassed.Buf.WriteFileCounter += 1;
                                    break;
                                case ((long)FileType_SubTypeIs.TEXT
                                | (long)FileType_SubTypeIs.Tilde):
                                    // Text
                                    break;
                                default:
                                    FileWriteResult = (long)StateIs.Undefined;
                                    LocalMessage.Msg6 = "Main Application - File Subtype (" + FmainPassed.Fs.FileSubTypeId.ToString() + ") not properly set";
                                    throw new NotSupportedException(LocalMessage.Msg6);
                            }
                            DbSyn.OutputValues = "";  // Values for Insert, Update, Delete
                        }
                        break;
                    default:
                        // FileTypeUNKNOWN
                        ColPointerIncrementResult = (long)StateIs.Undefined;
                        LocalMessage.Msg5 = "File Type Error (" + FmainPassed.Fs.FileTypeId.ToString() + ") not properly set";
                        throw new NotSupportedException(LocalMessage.Msg);
                } // end or is DATA Attr not DICT
            } catch (NotSupportedException nse) {
                LocalMessage.ErrorMsg = "Not Supported Exception occured in Pick File Action";
                ExceptNotSupportedImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionNotSupported, FileWriteResult);
                FileWriteResult = (long)StateIs.Failed;
            } catch (SqlException ExceptionSql) {
                LocalMessage.ErrorMsg = "SQL Exception occured in Pick File Write";
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileWriteResult);
                FileWriteResult = (long)StateIs.Failed;
            } catch (Exception ExceptionGeneral) {
                LocalMessage.ErrorMsg = "Unhandled Exception occured in Pick File Action";
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileWriteResult);
                FileWriteResult = (long)StateIs.Failed;
            } finally {
                // FileWriteResult = (long)StateIs.Failed;
            }

            // FileWrite
            return FileWriteResult;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        #region File Actions, State Description and Results
        #region File Action Section
        //
        /// <summary> 
        /// Do File Action.
        /// </summary> 
        public long FileDo(bool DoDefault) {
            // MFile1 FbasePassed, String PassedFileName
            if (DoDefault) {
                FileDoResult = (long)StateIs.Started;
                //
                Fmain.FileAction.ToDo = Fmain.Fs.FileIo.FileReadMode; // ?OK?
                Fmain.FileAction.FileObject = FileObject;
                Fmain.FileAction.Direction = Fmain.Fs.Direction;
                Fmain.FileAction.FileReadMode = Fmain.Fs.FileIo.FileReadMode; // ?OK?
                Fmain.FileAction.Mode = Fmain.Fs.FileIo.FileAccessMode; // ?OK?
                Fmain.FileAction.DoRetry = bYES;
                Fmain.FileAction.DoClearTarget = bNo;

                FileDoResult = FileDo(ref Fmain);
                if (FileDoResult == (long)StateIs.ShouldNotExist) {
                    // TODO $$$CHECK ??? FileTransformControl.OutFile = null;
                }
            }
            return FileDoResult;
        }
        //
        /// <summary> 
        /// Do File Action for Passed File.
        /// Main Method for all file actions.
        /// Centralised in order for muilti-threading 
        /// and messaging to operate.
        /// </summary> 
        public long FileDo(ref FileMainDef FmainPassed) {
            FileDoResult = (long)StateIs.Started;
            FileDoOpenResult = FileDoCloseResult
                = FileDoCheckResult = FileDoGetResult = (long)StateIs.None;
            #region FileDo Top
            String FileNameCurr = "";
            String FileNameNext = FmainPassed.Fs.FileNameGet(ref FmainPassed.Fs);
            String TryNextMessage = "";
            bool DoRetry = bYES;
            bool DoSkipNull = bNO;
            bool DoNextTryFirst = bYES;
            long MethodIterationLoopCounter;
            // File Action
            FileDoResult = FmainPassed.FileAction.Result
                = FmainPassed.FileAction.ModeResult = (long)StateIs.Started;
            FmainPassed.FileAction.ResultObject = null;
            FmainPassed.FileAction.ActionInfo.omvOfObject = (long)FileAction_Do.ObjectFILEDATA;
            FmainPassed.FileAction.ActionInfo.omvOfTarget = FmainPassed.FileAction.ToDo;
            FmainPassed.FileAction.ActionInfo.omvOfResult = (long)StateIs.Successful;
            FmainPassed.FileAction.ActionInfo.omvOfExistStatus = 0;
            FmainPassed.FileAction.ActionInfo.omvOfVerb =
                FmainPassed.FileAction.ActionInfo.omvOfObject
                + FmainPassed.FileAction.ActionInfo.omvOfTarget
                + FmainPassed.FileAction.ActionInfo.omvOfResult;
            #endregion
            try {
                #region Target Action
                FmainPassed.FileAction.Name = "UNDEFINED";
                try {
                    FmainPassed.FileAction.Name = Enum.GetName(typeof(FileAction_Do), FmainPassed.FileAction.ToDo);
                } catch {
                    FileDoOpenResult = (long)StateIs.Undefined;
                    LocalMessage.Msg6 = "Unknown Action for File (" + FmainPassed.FileAction.Name + ")!";
                    LocalMessage.Msg6 += "File Action Verb (" + FmainPassed.FileAction.ToDo + ") not properly set";
                    throw new NotSupportedException(LocalMessage.Msg6);
                }
                #endregion
                #region File Action Direction
                if (!Enum.IsDefined(typeof(FileAction_DirectionIs), FmainPassed.FileAction.Direction)) {
                    FileDoOpenResult = (long)StateIs.Undefined;
                    LocalMessage.Msg6 = "Unknown Direction for file.";
                    LocalMessage.Msg6 += " Mode (" + FmainPassed.FileAction.ModeName
                        + ") with File Direction ("
                        + FmainPassed.FileAction.Direction.ToString()
                        + ") " + FmainPassed.FileAction.DirectionName
                        + ", for file \"" + FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs)
                        + "\".";
                    LocalMessage.ErrorMsg = LocalMessage.Msg6;
                    // PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.ErrorMsg, bYES);
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), FileDoResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                    throw new NotSupportedException(LocalMessage.ErrorMsg);
                }
                #endregion
                #region File Action Mode
                FmainPassed.FileAction.ModeResult = (long)StateIs.InProgress;
                bool FileModeValid = true;
                if (FmainPassed.FileAction.Mode == (long)FileAction_Do.NotSet
                    || !Enum.IsDefined(typeof(FileAction_Do), FmainPassed.FileAction.Mode)) {
                    if (FmainPassed.FileAction.KeyName.Length > 0) {
                        try {
                            FmainPassed.FileAction.Mode
                                = (long)Enum.Parse(typeof(FileAction_Do), FmainPassed.FileAction.KeyName);
                            FileModeValid = true;
                        } catch { FileModeValid = false; }
                    } else { FileModeValid = false; }
                }
                if (!FileModeValid) {
                    FileDoOpenResult = (long)StateIs.Undefined;
                    LocalMessage.Msg6 = "File Action (" + FmainPassed.FileAction.ToDo + ") "
                        + FmainPassed.FileAction.Name + "! " + "\n";
                    LocalMessage.Msg6 += "Unknown File Mode (" + FmainPassed.FileAction.Mode + ") "
                    + "and the lookup key \"" + FmainPassed.FileAction.KeyName + "\" "
                    + "are not valid!!! " + "\n";
                    LocalMessage.Msg6 += "Occured on file \"" + FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs) + "\"";
                    FmainPassed.FileAction.ModeName = "Mode UNKNOWN!";
                    LocalMessage.ErrorMsg = LocalMessage.Msg6;
                    // PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.ErrorMsg, bYES);
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), FileDoResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                    throw new NotSupportedException(LocalMessage.ErrorMsg);
                }
                #endregion
                #region File Read Mode
                bool FileReadModeValid = true;
                if (FmainPassed.FileAction.FileReadMode == (long)FileAction_ReadModeIs.None
                    || !Enum.IsDefined(typeof(FileAction_ReadModeIs), FmainPassed.FileAction.FileReadMode)) {
                    if (FmainPassed.FileAction.KeyName.Length > 0) {
                        try {
                            FmainPassed.FileAction.FileReadMode
                                = (long)Enum.Parse(typeof(FileAction_ReadModeIs), FmainPassed.FileAction.KeyName);
                            FileReadModeValid = true;
                        } catch { FileReadModeValid = false; }
                    } else { FileReadModeValid = false; }
                }
                if (!FileReadModeValid) {
                    FileDoOpenResult = (long)StateIs.Undefined;
                    LocalMessage.Msg6 = "File Action (" + FmainPassed.FileAction.ToDo + ") " + FmainPassed.FileAction.Name + "! " + "\n";
                    LocalMessage.Msg6 += "Unknown File Read Mode (" + FmainPassed.FileAction.FileReadMode + ") "
                        + "and the lookup key \"" + FmainPassed.FileAction.KeyName + "\" "
                        + "are not valid!!! " + "\n";
                    LocalMessage.Msg6 += "Occured on file \"" + FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs) + "\"";
                    LocalMessage.ErrorMsg = LocalMessage.Msg6;
                    // PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.ErrorMsg, bYES);
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage,
                        XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(),
                        FileDoResult, XUomMovvXv.RunErrorDidOccur = true,
                        iNoErrorLevel, iNoErrorSource,
                        bDoNotDisplay, MessageNoUserEntry,
                        "A2" + LocalMessage.ErrorMsg + "\n");
                    throw new NotSupportedException(LocalMessage.ErrorMsg);
                }
                #endregion
                // --------------------------------- //
                #region Perform Action
                LocalMessage.LogEntry = FileMainHeaderGet(ref FmainPassed);
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage,
                    XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(),
                    FileDoResult, bNoError,
                    iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry,
                    "C" + LocalMessage.LogEntry + "\n");
                switch (FmainPassed.FileAction.Name) {
                    #region Check
                    case ("Check"):
                        // Faux.DataClear();
                        // Faux = FmainPassed;
                        FmainPassed.Fs.CopyTo(ref Faux.Fs);
                        FmainPassed.FileAction.CopyTo(ref Faux.FileAction);
                        Faux.FileAction = FmainPassed.FileAction;
                        Faux.IoState = FmainPassed.IoState;
                        // Faux.DbIo = FmainPassed.DbIo;
                        // Faux.Fs.FileIo = FmainPassed.Fs.FileIo;
                        // FileIo
                        Faux.Fs.Direction = FmainPassed.FileAction.Direction;
                        Faux.Fs.FileIo.FileReadMode = Fmain.FileAction.FileReadMode;
                        Faux.Fs.FileIo.FileAccessMode = Fmain.FileAction.Mode;
                        //
                        switch (FmainPassed.FileAction.Mode) {
                            // System
                            case ((long)FileAction_Do.System):
                                FileDoCheckResult = SystemListCheck(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            // Database
                            case ((long)FileAction_Do.Server):
                                FileDoCheckResult = ServerListCheck(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            case ((long)FileAction_Do.Service):
                                FileDoCheckResult = ServiceListCheck(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            case ((long)FileAction_Do.Database):
                                FileDoCheckResult = DatabaseListCheck(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            case ((long)FileAction_Do.Table):
                                // Faux.Fs.TableNameSetFromLine(Faux.Fs.TableNameLine);
                                FileDoCheckResult = TableListCheck(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            case ((long)FileAction_Do.FileGroup):
                                break;
                            // FSO Files
                            case ((long)FileAction_Do.DiskFile):
                                // Faux.Fs.FileId.FileNameSetFromLine(Faux.Fs.FileId.FileNameLine);
                                if (TextFileDoesExist(ref Faux)) {
                                    FileDoCheckResult = (long)StateIs.DoesExist;
                                } else { FileDoCheckResult = (long)StateIs.DoesNotExist; }
                                break;
                            case ((long)FileAction_Do.AsciiDef):
                                break;
                            // Database Security
                            case ((long)FileAction_Do.DbUser):
                                break;
                            case ((long)FileAction_Do.DbPassword):
                                break;
                            case ((long)FileAction_Do.DbSecurityType):
                                break;
                            default:
                                FileDoCheckResult = (long)StateIs.Undefined;
                                //
                                LocalMessage.Msg6 = "File Action (" + FmainPassed.FileAction.Direction + ") " + FmainPassed.FileAction.ModeName;
                                LocalMessage.Msg6 += "! Unknown File Action";
                                LocalMessage.Msg6 += " on file \"" + FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs) + "\"";
                                LocalMessage.ErrorMsg = LocalMessage.Msg6;
                                // PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.ErrorMsg, bYES);
                                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), FileDoResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                                throw new NotSupportedException(LocalMessage.ErrorMsg);
                        }
                        #region Display and modify results of Action based on file options
                        #region Display results of Action
                        StateDescriptionGet(ref FmainPassed);
                        ResultDescriptionLong(ref FmainPassed);
                        #endregion
                        // Handle results of close failure here
                        #endregion
                        break;
                    #endregion
                    #region Get
                    case ("ListGet"):
                        List<String> ObjectList = null;
                        // Faux.DataClear();
                        // Faux = FmainPassed;
                        FmainPassed.Fs.CopyTo(ref Faux.Fs);
                        FmainPassed.FileAction.CopyTo(ref Faux.FileAction);
                        Faux.FileAction = FmainPassed.FileAction;
                        Faux.IoState = FmainPassed.IoState;
                        // Faux.DbIo = FmainPassed.DbIo;
                        // Faux.Fs.FileIo = FmainPassed.Fs.FileIo;
                        // FileIo
                        Faux.Fs.Direction = FmainPassed.FileAction.Direction;
                        Faux.Fs.FileIo.FileReadMode = Fmain.FileAction.FileReadMode;
                        Faux.Fs.FileIo.FileAccessMode = Fmain.FileAction.Mode;
                        //
                        switch (FmainPassed.FileAction.Mode) {
                            // System
                            case ((long)FileAction_Do.System):
                                ObjectList = SystemListGet(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            // Server
                            case ((long)FileAction_Do.Server):
                                ObjectList = ServerListGet(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            // Service
                            case ((long)FileAction_Do.Service):
                                ObjectList = ServiceListGet(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            // Database
                            case ((long)FileAction_Do.Database):
                                ObjectList = DatabaseListGet(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            case ((long)FileAction_Do.Table):
                                // Faux.Fs.TableNameSetFromLine(Faux.Fs.TableNameLine);
                                ObjectList = TableListGet(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            case ((long)FileAction_Do.FileGroup):
                                break;
                            // FSO Files
                            case ((long)FileAction_Do.DiskFile):
                                // Faux.Fs.FileId.FileNameSetFromLine(Faux.Fs.FileId.FileNameLine);
                                if (TextFileDoesExist(ref Faux)) {
                                    FileDoGetResult = (long)StateIs.DoesExist;
                                } else { FileDoGetResult = (long)StateIs.DoesNotExist; }
                                break;
                            case ((long)FileAction_Do.AsciiDef):
                                break;
                            // Database Security
                            case ((long)FileAction_Do.DbUser):
                                break;
                            case ((long)FileAction_Do.DbPassword):
                                break;
                            case ((long)FileAction_Do.DbSecurityType):
                                break;
                            default:
                                FileDoGetResult = (long)StateIs.Undefined;
                                //
                                LocalMessage.Msg6 = "File Action (" + FmainPassed.FileAction.Direction + ") " + FmainPassed.FileAction.ModeName;
                                LocalMessage.Msg6 += "! Unknown File Action";
                                LocalMessage.Msg6 += " on file \"" + FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs) + "\"";
                                LocalMessage.ErrorMsg = LocalMessage.Msg6;
                                // PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.ErrorMsg, bYES);
                                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), FileDoResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                                throw new NotSupportedException(LocalMessage.ErrorMsg);
                        }
                        if (ObjectList != null) {
                            FileDoGetResult = (int)StateIs.Successful;
                            FmainPassed.FileAction.ResultObject = ObjectList;
                        } else {
                            FileDoGetResult = (int)StateIs.EmptyResult;
                            FmainPassed.FileAction.ResultObject = null;
                        }
                        #region Display and modify results of Action based on file options
                        #region Display results of Action
                        StateDescriptionGet(ref FmainPassed);
                        ResultDescriptionLong(ref FmainPassed);
                        #endregion
                        // Handle results of close failure here
                        #endregion
                        break;
                    #endregion
                    #region Open
                    case ("Open"):
                        FileDoOpenResult = (long)FileAction_OpenControl.TryFirst;
                        FileDoResult = FileDoOpenResult;
                        FileNameCurr = "";
                        FileNameNext = FmainPassed.Fs.FileNameGet(ref FmainPassed.Fs);
                        TryNextMessage = "";
                        // DoRetry = FmainPassed.FileAction.DoRetry;
                        DoRetry = bYES;
                        DoSkipNull = bNO;
                        DoNextTryFirst = bYES;
                        // FileIo
                        FmainPassed.Fs.Direction = FmainPassed.FileAction.Direction;
                        FmainPassed.Fs.FileIo.FileReadMode = Fmain.FileAction.FileReadMode;
                        FmainPassed.Fs.FileIo.FileAccessMode = Fmain.FileAction.Mode;
                        //
                        #region Loop for Opening File
                        for (MethodIterationLoopCounter = 1; (MethodIterationLoopCounter <= (long)FileAction_OpenControl.TryAll && FileDoOpenResult < (long)FileAction_OpenControl.TryAll && DoRetry); MethodIterationLoopCounter++) {
                            #region Choose File to try
                            // default output file handling...
                            TryNextMessage = "";
                            if (!DoNextTryFirst) {
                                TryNextMessage += "Bad file name";
                            } else {
                                TryNextMessage += "Opening file name";
                            }
                            TryNextMessage += " \"" + FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs) + "\".";
                            // TODO $$$CHECK PrintOutputMdm_PickPrint(Sender, 3, (bool)bYES);
                            switch (MethodIterationLoopCounter) {
                                case ((long)FileAction_OpenControl.TryDefault):
                                    TryNextMessage += ". Trying the Default file.";
                                    FileNameNext = FmainPassed.Fs.FileNameFullDefault;
                                    //////switch (ToDo.Direction) {
                                    //////    case ((int)Name.FileAction_DirectionIs.Input):
                                    //////        FileNameFullNext = FileTransformControl.InFile.Fmain.Fs.FileNameFullDefault;
                                    //////        break;
                                    //////    case ((int)Name.FileAction_DirectionIs.Output):
                                    //////        FileNameFullNext = FileTransformControl.OutFile.Fmain.Fs.FileNameFullDefault;
                                    //////        break;
                                    //////    default:
                                    //////        break;
                                    //////}
                                    break;
                                // Reloop and try opening the Original File Name
                                case ((long)FileAction_OpenControl.TryOriginal):
                                    // original output file handling...
                                    TryNextMessage += ". Trying the Original file.";
                                    FileNameNext = FmainPassed.Fs.FileNameFullOriginal;
                                    //////switch (ToDo.Direction) {
                                    //////    case ((int)Name.FileAction_DirectionIs.Input):
                                    //////        FileNameFullNext = FileTransformControl.InFile.Fmain.Fs.FileNameFullOriginal;
                                    //////        break;
                                    //////    case ((int)Name.FileAction_DirectionIs.Output):
                                    //////        FileNameFullNext = FileTransformControl.OutFile.Fmain.Fs.FileNameFullOriginal;
                                    //////        break;
                                    //////    default:
                                    //////        break;
                                    //////}
                                    //
                                    break;
                                // Reloop and try opening the Entered File Name
                                case ((long)FileAction_OpenControl.TryEntered):
                                    TryNextMessage += ". Trying the CHOSEN file.";
                                    FileNameNext = FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs);
                                    //////switch (ToDo.Direction) {
                                    //////    case ((int)Name.FileAction_DirectionIs.Input):
                                    //////        FileNameFullNext = PassedFileName;
                                    //////        break;
                                    //////    case ((int)Name.FileAction_DirectionIs.Output):
                                    //////        FileNameFullNext = PassedFileName;
                                    //////        break;
                                    //////    default:
                                    //////        break;
                                    //////}
                                    break;
                                default:
                                    // Loop Error on reloop for other file names
                                    // original output file handling...
                                    TryNextMessage += "!!! Error: Invalid File!!! Trying Original file name";
                                    FileNameNext = FmainPassed.Fs.FileNameFullOriginal;
                                    //////switch (ToDo.Direction) {
                                    //////    case ((int)Name.FileAction_DirectionIs.Input):
                                    //////        //
                                    //////        FileNameFullNext = FileTransformControl.InFile.Fmain.Fs.FileNameFullOriginal;
                                    //////        break;
                                    //////    case ((int)Name.FileAction_DirectionIs.Output):
                                    //////        //
                                    //////        FileNameFullNext = FileTransformControl.OutFile.Fmain.Fs.FileNameFullOriginal;
                                    //////        break;
                                    //////    default:
                                    //////        break;
                                    //////}
                                    //
                                    if (MethodIterationLoopCounter + 1 <= (long)FileAction_OpenControl.TryAll && FileDoOpenResult < (long)FileAction_OpenControl.TryAll && DoRetry) {
                                        TryNextMessage += FmainPassed.FileAction.ModeName + ". Import File Direction ("
                                            + FmainPassed.FileAction.Direction.ToString() + ") "
                                            + FmainPassed.FileAction.Direction + ", for file \""
                                            + FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs) + "\".";
                                        LocalMessage.Msg6 = TryNextMessage;
                                        LocalMessage.ErrorMsg = LocalMessage.Msg6;
                                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), FileDoOpenResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                                        //
                                        TryNextMessage = "Will now try \"" + FileNameNext + "\".";
                                        LocalMessage.Msg6 = TryNextMessage;
                                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), FileDoOpenResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg6 + "\n");
                                        // blank line...
                                        LocalMessage.Msg6 = "";
                                        PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg6, bYES);
                                    } else {
                                        // TODO this throw needs work
                                        LocalMessage.ErrorMsg = "The file "
                                            + FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs)
                                            + ", and no suitable default file was found.";
                                        throw new NotSupportedException(LocalMessage.ErrorMsg);
                                    }
                                    break;
                            } // end of switch MethodIterationLoopCounter
                            #endregion
                            #region OPEN FILE
                            //
                            LocalMessage.Msg6 = TryNextMessage + " Open File being used...";
                            PrintOutputMdm_PickPrint(Sender, 2, "A2" + LocalMessage.Msg6, bYES);
                            //
                            LocalMessage.Msg6 = FmainPassed.FileAction.Name + " file for direction ("
                                + FmainPassed.FileAction.Direction.ToString() + ") "
                                + FmainPassed.FileAction.DirectionName + " for file \""
                                + FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs)
                                + "\"." + "\n";
                            if (FmainPassed.FileAction.KeyName.Length > 0) {
                                LocalMessage.Msg6 += " Activity Key is " + "\""
                                    + FmainPassed.FileAction.KeyName + "\"" + ".\n";
                            }
                            LocalMessage.Msg6 += " The Action Mode ("
                            + FmainPassed.FileAction.Mode.ToString() + ") is \""
                            + FmainPassed.FileAction.ModeName + "\"." + "\n";
                            PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg6, bYES);
                            #region Open the file
                            FileNameCurr = FileNameNext;
                            LocalMessage.Msg6 = FmainPassed.FileAction.ModeName;
                            if (FileNameCurr.Length > 0) {
                                try {
                                    // switch (FmainPassed.FileAction.Mode) {
                                    switch (FmainPassed.Fs.FileIo.FileReadMode) {
                                        case ((long)FileIo_ModeIs.Sql):
                                            FileDoOpenResult = TableOpen(ref FmainPassed);
                                            break;
                                        case ((long)FileIo_ModeIs.Block):
                                            FileDoOpenResult = FileOpenHandle(ref FmainPassed, FileNameCurr, FmainPassed.FileAction.Mode, 0);
                                            break;
                                        case ((long)FileIo_ModeIs.Line):
                                            FileDoOpenResult = FileOpen(ref FmainPassed, FileNameCurr, FmainPassed.FileAction.Mode, 0);
                                            break;
                                        case ((long)FileIo_ModeIs.All):
                                            FileDoOpenResult = TextFileOpen(ref FmainPassed);
                                            // FileDoOpenResult = FileOpen(FbasePassed, FileNameCurr, ToDo.Mode, 0);
                                            break;
                                        default:
                                            break;
                                    }
                                    #region Catch Open Errors
                                } catch (SqlException ExceptionSql) {
                                    LocalMessage.ErrorMsg = "SQL Exception occured in Pick File Action";
                                    ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileDoOpenResult);
                                    FileDoResult = (long)StateIs.Failed;
                                } catch (NotSupportedException ExceptionNotSupported) {
                                    LocalMessage.ErrorMsg = "Not Supported Exception occured in Pick File Action";
                                    ExceptNotSupportedImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionNotSupported, FileDoOpenResult);
                                    FileDoResult = (long)StateIs.Failed;
                                } catch (IOException ExceptionIO) {
                                    LocalMessage.ErrorMsg = "SQL Exception occured in Pick File Action";
                                    ExceptIoImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionIO, FileDoOpenResult);
                                    FileDoResult = (long)StateIs.Failed;
                                } catch (Exception ExceptionGeneral) {
                                    LocalMessage.ErrorMsg = "Unhandled Exception occured in Pick File Action";
                                    ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileDoOpenResult);
                                    FileDoResult = (long)StateIs.Failed;
                                } finally {
                                    if (FileDoResult == (long)StateIs.InProgress) {
                                        FileDoResult = (long)StateIs.Failed;
                                        LocalMessage.Msg6 = "Operation did not complete! Exception Error! ";
                                        // PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg6, bYES);
                                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), FileDoOpenResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg6 + "\n");
                                        // TODO throw error might be prefered instead.  Good for now...
                                    }
                                } // of try connect
                                    #endregion
                            } else {
                                FileDoOpenResult = (long)StateIs.NotSet;
                            }
                            #endregion
                            #endregion
                            #region Display and modify results of Action based on file options
                            #region Display results of Action
                            StateDescriptionGet(ref FmainPassed);
                            ResultDescriptionLong(ref FmainPassed);
                            #endregion
                            if (FileDoOpenResult == (long)StateIs.DoesExist) {
                                #region File Does Exist
                                // TODO FileDo put option check for SHOULD EXIST / SHOULD_NOT EXIST here.
                                if (FmainPassed.FileAction.Direction == (int)FileAction_DirectionIs.Output) {
                                    // Delete File if it Does Exist
                                    if (FmainPassed.Fs.FileOpt.DoCreateFileMustNotExist) {
                                        ; // ToDo Error - Create File if Does Not Exist
                                        FileDoOpenResult = (long)StateIs.ShouldNotExist;
                                        DoRetry = bNO;
                                    } else {
                                        if (FmainPassed.Fs.FileOpt.DoDeleteFile || FmainPassed.Fs.FileOpt.DoCreateFileNew) {
                                            ; // TODO FileDo Delete File if it Does Exist
                                        }
                                        // Create New File regardless if it Does Exist
                                        if (FmainPassed.Fs.FileOpt.DoCreateFileNew) {
                                            ; // NOOP - Create New File regardless if it Does Exist
                                        }
                                        // Create File if Does Not Exist
                                        if (FmainPassed.Fs.FileOpt.DoCreateFileNew) {
                                            ; // TODO FileDo Create File if Does Not Exist
                                        }
                                        // TODO FileDo set up proper default for Good eArmhResult - Check that File Does Exist
                                        FileDoOpenResult = (long)StateIs.Successful;
                                        DoRetry = bNO;
                                        if (FmainPassed.Fs.FileOpt.DoCheckFileDoesExist) {
                                            ; // Good eArmhResult - Check that File Does Exist
                                        }
                                    }
                                    //
                                } else {
                                    // Input File handling where File Does Exist
                                    if (!FmainPassed.FileAction.DoRetry) {
                                        DoRetry = bNO;
                                        DoSkipNull = bYES;
                                    } else {
                                        DoRetry = bYES;
                                        DoSkipNull = bYES;
                                    }
                                } // end out File handling where Does Exist
                                #endregion
                            } else if (FileDoOpenResult == (long)StateIs.DoesNotExist) {
                                #region File Does Not Exist
                                if (FmainPassed.FileAction.Direction == (int)FileAction_DirectionIs.Output) {
                                    // Output File Does Not Exist
                                    if (FmainPassed.Fs.FileOpt.DoDeleteFile) {
                                        ; // NOOP - Delete File if it Does Exist
                                    }
                                    if (FmainPassed.Fs.FileOpt.DoCreateFileMustNotExist || FmainPassed.Fs.FileOpt.DoCreateFileNew) {
                                        ; // TODO FileDo Create File if Does Not Exist
                                        FileDoOpenResult = (long)StateIs.Successful;
                                        DoRetry = bNO;
                                    }
                                    // Check that File Does Exist
                                    if (FmainPassed.Fs.FileOpt.DoCheckFileDoesExist) {
                                        ; // ToDo Error - Check that File Does Exist
                                    }
                                    // Create File if Does Not Exist
                                    if (FmainPassed.Fs.FileOpt.DoCreateFileDoesNotExist) {
                                        FileDoCreateResult = (long)StateIs.InProgress;
                                        FmainPassed.DbIo.CommandCurrent = "";
                                        try {
                                            switch (FmainPassed.Fs.FileIo.FileReadMode) {
                                                case ((long)FileIo_ModeIs.Sql):
                                                    FileDoCreateResult = TableCreateToDo(ref FmainPassed);
                                                    break;
                                                case ((long)FileIo_ModeIs.Block):
                                                    // FileDoCreateResult = FileCreateHandle(ref FmainPassed, FileNameCurr, FileAction.Mode, 0);
                                                    break;
                                                case ((long)FileIo_ModeIs.Line):
                                                    // FileDoCreateResult = FileCreateMfile(ref FmainPassed, FileNameCurr, FileAction.Mode, 0);
                                                    break;
                                                case ((long)FileIo_ModeIs.All):
                                                    // FileDoCreateResult = TextFileCreate(ref FmainPassed);
                                                    // FileDoCreateResult = FileOpen(FbasePassed, FileNameCurr, ToDo.Mode, 0);
                                                    break;
                                                default:
                                                    break;
                                            }
                                            #region Catch Open Errors
                                        } catch (SqlException ExceptionSql) {
                                            LocalMessage.ErrorMsg = "SQL Exception occured in Pick File Action";
                                            ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileDoCreateResult);
                                            FileDoResult = (long)StateIs.Failed;
                                        } catch (NotSupportedException ExceptionNotSupported) {
                                            LocalMessage.ErrorMsg = "Not Supported Exception occured in File Creation Action";
                                            ExceptNotSupportedImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionNotSupported, FileDoCreateResult);
                                            FileDoResult = (long)StateIs.Failed;
                                        } catch (IOException ExceptionIO) {
                                            LocalMessage.ErrorMsg = "SQL Exception occured in File Creation Action";
                                            ExceptIoImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionIO, FileDoCreateResult);
                                            FileDoResult = (long)StateIs.Failed;
                                        } catch (Exception ExceptionGeneral) {
                                            LocalMessage.ErrorMsg = "Unhandled Exception occured in File Creation Action";
                                            ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileDoCreateResult);
                                            FileDoResult = (long)StateIs.Failed;
                                        } finally {
                                            if (FileDoResult == (long)StateIs.InProgress) {
                                                FileDoResult = (long)StateIs.Failed;
                                                LocalMessage.Msg6 = "File Creation Operation did not complete! Exception Error! ";
                                                // PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg6, bYES);
                                                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), FileDoCreateResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg6 + "\n");
                                                // TODO throw error might be prefered instead.  Good for now...
                                            }
                                        } // of try connect
                                            #endregion
                                    }
                                    // end of Ouptput File wher Does Not Exist
                                } else {
                                    // Input File Does Not Exist
                                    // if (FileDoOpenResult == (long)StateIs.ShouldNotExist || FileDoOpenResult == (long)StateIs.MissingName) {
                                    if (MethodIterationLoopCounter < (long)FileAction_OpenControl.TryAll) {
                                        // retry different name;
                                        FileDoOpenResult = (long)FileAction_OpenControl.TryAgain;
                                        DoRetry = bYES;
                                    } else {
                                        FileDoOpenResult = (long)StateIs.ShouldNotExist;
                                        XUomMovvXv.RunErrorDidOccur = ErrorDidOccur;
                                        FmainPassed.FileAction.ResultName = "SHOULD_NOT_EXIST";
                                        // failure, can not open file
                                        // PrintOutputMdm_PickPrint(Sender, 3, (bool)bYES);
                                        LocalMessage.Msg6 = "ABORT: Unable to open \"" + FileNameCurr + "\".";
                                        LocalMessage.Msg6 += FmainPassed.FileAction.Name + " Failed with (" + FileDoResult + "), Verb Result (" + FmainPassed.FileAction.ActionInfo.omvOfResult + ")  properly set";
                                        LocalMessage.ErrorMsg = LocalMessage.Msg6;
                                        // PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg6, bYES);
                                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), FileDoOpenResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                                        //
                                        XUomMovvXv.RunAbortIsOn = bYES;
                                        DoRetry = bNO;
                                        FmainPassed = null;
                                        // 
                                        // TODO could possibly throw new Exception(LocalMessage.ErrorMsg);
                                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), FileDoOpenResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                                        //
                                        LocalMessage.Msg6 = "Error, unable to open the " + FmainPassed.FileAction.Direction + " File!!!";
                                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), FileDoOpenResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg6 + "\n");
                                    }
                                    //
                                    if (FmainPassed.Fs.FileOpt.DoCreateFileNew) {
                                        // RESET FILE DATA
                                        FileDoOpenResult = (long)StateIs.ShouldNotExist;
                                        DoRetry = bNO;
                                    }
                                    //
                                } // end of Input / Ouptput File where Does Not Exist
                                #endregion
                            } // end of Action failed File Does / Does Not Exist handling
                            DoNextTryFirst = bNO;
                            #endregion
                        } // end of for LOOP fault tolerant default file name processing
                        break;
                        #endregion
                    #endregion
                    #region Close
                    case ("Close"):
                        #region CLOSE the file
                        // FileIo
                        FmainPassed.Fs.Direction = FmainPassed.FileAction.Direction;
                        FmainPassed.Fs.FileIo.FileReadMode = Fmain.FileAction.FileReadMode;
                        FmainPassed.Fs.FileIo.FileAccessMode = Fmain.FileAction.Mode;
                        //
                        switch (FmainPassed.Fs.FileIo.FileReadMode) {
                            case ((long)FileIo_ModeIs.Sql):
                                // (ref FileMainDef FmainPassed, 
                                // String PassedFileName, 
                                // String PassedFileNameFull) {
                                FileDoCloseResult = TableClose(ref FmainPassed);
                                break;
                            case ((long)FileIo_ModeIs.Block):
                                FileDoCloseResult = AsciiFileClose(ref FmainPassed);
                                break;
                            case ((long)FileIo_ModeIs.Line):
                                FileDoCloseResult = AsciiFileClose(ref FmainPassed);
                                break;
                            case ((long)FileIo_ModeIs.All):
                                FileDoCloseResult = TextFileClose(ref FmainPassed);
                                // FileDoOpenResult = Buf.FileOpen(FbasePassed, FileNameCurr, ToDo.Mode, 0);
                                break;
                            default:
                                FileDoCloseResult = (long)StateIs.Undefined;
                                //
                                LocalMessage.Msg6 = "File Action (" + FmainPassed.FileAction.Direction + ") " + FmainPassed.FileAction.ModeName;
                                LocalMessage.Msg6 += "! Unknown File Action";
                                LocalMessage.Msg6 += " on file \"" + FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs) + "\"";
                                LocalMessage.ErrorMsg = LocalMessage.Msg6;
                                throw new NotSupportedException(LocalMessage.ErrorMsg);
                        }
                        #endregion
                        #region Display and modify results of Action based on file options
                        // Note that results are handled once (here) after an
                        // Open or Close and then secondary actions are taken that
                        // depend on file options (ie create if missing.)
                        // (not implemented here for close yet)
                        // The only additional (normal) handling after close might be
                        // to delete a file, move it, or copy it.
                        // Result will be handled again to product a final result.
                        #region Display results of Action
                        StateDescriptionGet(ref FmainPassed);
                        ResultDescriptionLong(ref FmainPassed);
                        #endregion
                        // Handle results of close failure here
                        // Implementation of CLOSE HANDLING HERE
                        //#region Display results of Action
                        //StateDescriptionGet(ref FmainPassed);
                        //ResultDescriptionLong(ref FmainPassed);
                        //#endregion
                        #endregion
                        break;
                    #endregion
                    #region Error not Check, Open or Close
                    default:
                        FileDoResult = (long)StateIs.Undefined;
                        XUomMovvXv.RunErrorDidOccur = true;
                        FmainPassed.FileAction.ModeName = "ModeUNKNOWN";
                        //
                        LocalMessage.Msg6 = "File Action (" + FmainPassed.FileAction.Direction + ") "
                            + FmainPassed.FileAction.ModeName;
                        LocalMessage.Msg6 += "! Unknown File Action";
                        LocalMessage.Msg6 += " on file \"" + FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs) + "\"";
                        LocalMessage.ErrorMsg = LocalMessage.Msg6;
                        throw new NotSupportedException(LocalMessage.ErrorMsg);
                    #endregion
                }
                #endregion
                // --------------------------------- //
                #region Process Result of Action
                if (StateIsSuccessfulAll(FileDoResult)) {
                    // Result has NO Error
                } else {
                    #region Result Error
                    LocalMessage.Msg6 = "";
                    LocalMessage.Msg7 = "Calling String: " + PickSystemCallStringResult;
                    LocalMessage.Msg8 = "Activity Key: " + FmainPassed.FileAction.KeyName;
                    LocalMessage.Msg6 += "File Action Error (" + FileDoResult.ToString() + ") ";
                    if (Enum.IsDefined(typeof(StateIs), FileDoResult)) {
                        LocalMessage.Msg6 += Enum.GetName(typeof(StateIs), FileDoResult);
                    }
                    LocalMessage.Msg6 += "." + "\n";
                    LocalMessage.Msg6 += "Failed to " + FmainPassed.FileAction.Name
                        + ", in mode: " + FmainPassed.FileAction.ModeName
                        + ", for direction: " + FmainPassed.FileAction.DirectionName + ".";
                    LocalMessage.ErrorMsg = LocalMessage.Msg6;
                    // PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg6, bYES);
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage,
                        XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(),
                        FileDoResult, XUomMovvXv.RunErrorDidOccur = true,
                        iNoErrorLevel, iNoErrorSource,
                        bDoNotDisplay, MessageNoUserEntry,
                        "A2" + LocalMessage.Msg6 + "\n");
                    #endregion
                } // end of skip null
                #endregion
                #region Catch General Errors
            } catch (SqlException ExceptionSql) {
                LocalMessage.ErrorMsg = "SQL Exception occured in Pick File Action";
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileDoResult);
                FileDoResult = (long)StateIs.Failed;
                FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.SqlError;
            } catch (NotSupportedException ExceptionNotSupported) {
                LocalMessage.ErrorMsg = "Not Supported Exception occured in Pick File Action";
                ExceptNotSupportedImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionNotSupported, FileDoResult);
                FileDoResult = (long)StateIs.Failed;
                FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.NotSupported;
            } catch (IOException ExceptionIO) {
                LocalMessage.ErrorMsg = "SQL Exception occured in Pick File Action";
                ExceptIoImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionIO, FileDoResult);
                FileDoResult = (long)StateIs.Failed;
                FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.IoError;
            } catch (Exception ExceptionGeneral) {
                LocalMessage.ErrorMsg = "Unhandled Exception occured in Pick File Action";
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileDoResult);
                FileDoResult = (long)StateIs.Failed;
                FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.General;
            } finally {
                if (FileDoResult == (long)StateIs.InProgress) {
                    FileDoResult = (long)StateIs.Failed;
                    FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.NotCompleted;
                    LocalMessage.Msg6 = "Operation did not complete! Exception Error! ";
                    // PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg6, bYES);
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), FileDoResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg6 + "\n");
                    // TODO poosible Throw here
                }
                #endregion
            } // of try connect
            //
            if (FmainPassed.IoState.DoCallback) {
                FmainPassed.IoState.ClearKey();
                FmainPassed.IoState.CategoryKey = FmainPassed.FileAction.KeyName;
                FmainPassed.IoState.ActionKey = FmainPassed.FileAction.Name;
                FmainPassed.IoState.IoDoCallback(FileDoResult, FmainPassed.FileAction.ResultObject);
            }
            return FileDoResult;
        }
        #endregion
        //
        #region File IO Error Description
        /// <summary> 
        /// Get a File IO Error Description.
        /// </summary> 
        public String FileIoErrorDescriptionGet(long ResultPassed) {
            String FileIoErrorDescription = "";
            switch (ResultPassed) {
                case ((long)FileIo_ErrorIs.AccessError):
                    FileIoErrorDescription = "information accesss error";
                    break;
                case ((long)FileIo_ErrorIs.DatabaseError):
                    FileIoErrorDescription = "database access error";
                    break;
                case ((long)FileIo_ErrorIs.DiskError):
                    FileIoErrorDescription = "disk access error";
                    break;
                case ((long)FileIo_ErrorIs.DiskFull):
                    FileIoErrorDescription = "disk is full";
                    break;
                case ((long)FileIo_ErrorIs.General):
                    FileIoErrorDescription = "general exception error";
                    break;
                case ((long)FileIo_ErrorIs.InternetError):
                    FileIoErrorDescription = "Internet access error";
                    break;
                case ((long)FileIo_ErrorIs.IoError):
                    FileIoErrorDescription = "general IO error";
                    break;
                case ((long)FileIo_ErrorIs.NetworkError):
                    FileIoErrorDescription = "network access errror";
                    break;
                case ((long)FileIo_ErrorIs.None):
                    FileIoErrorDescription = "no error occured";
                    break;
                case ((long)FileIo_ErrorIs.NotCompleted):
                    FileIoErrorDescription = "action not completed error";
                    break;
                case ((long)FileIo_ErrorIs.NotSupported):
                    FileIoErrorDescription = "software function not supported";
                    break;
                case ((long)FileIo_ErrorIs.OsError):
                    FileIoErrorDescription = "operating system error";
                    break;
                case ((long)FileIo_ErrorIs.SqlError):
                    FileIoErrorDescription = "SQL database error";
                    break;
                default:
                    FileIoErrorDescription = "Unknown error" + " (" + ResultPassed + ")";
                    break;
            }
            if (TraceBugOnNow && iTraceDisplayCountTotal > iTraceBugThreshold) {
                LocalMessage.Msg9 = "\n" + FileIoErrorDescription;
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.DelSep.ItemAttrMaxIndex, ColPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg9 + "\n");
            }
            return FileIoErrorDescription;
        }
        #endregion
        //

        /// <summary> 
        /// Get a File Object Header string
        /// </summary> 
        public String FileMainHeaderGet(ref FileMainDef FmainPassed) {
            LocalMessage.Header = "";
            try {
                LocalMessage.Header += "File (" + FmainPassed.Fs.FileId.FileId + ") " + FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs);
                LocalMessage.Header += ", Action (" + FmainPassed.FileAction.ToDo + ") " + FmainPassed.FileAction.Name;
                LocalMessage.Header += ", Mode (" + FmainPassed.FileAction.ModeName + ") " + FmainPassed.FileAction.ModeName;
                LocalMessage.Header += ", Direction (" + FmainPassed.FileAction.Direction + ") " + FmainPassed.FileAction.Direction;
                LocalMessage.Header += ".";
            } catch {
                LocalMessage.Header += ", there is invalid header data!!!";
            }
            return LocalMessage.Header;
        }

        #region File State Description
        /// <summary> 
        /// Get the State Description for the current File Action.
        /// </summary> 
        public void StateDescriptionGet(ref FileMainDef FmainPassed) {
            #region File Object Result
            FileDoResult = FileDoOpenResult | FileDoCloseResult | FileDoCheckResult | FileDoGetResult;
            FmainPassed.FileAction.Result = FileDoResult;
            FmainPassed.FileAction.ActionInfo.Result = FmainPassed.FileAction.Result;
            // Name.StateDescription = "";
            LocalMessage.Msg6 = FmainPassed.FileAction.ModeName;
            try {
                // FmainPassed.FileAction.ActionInfo.StateDescription = FmainPassed.FileAction.Result.ToString();
                FmainPassed.FileAction.ActionInfo.ResultName = FmainPassed.FileAction.ResultName;
                FmainPassed.FileAction.ActionInfo.ResultDescription = "File Action result was "
                + FmainPassed.FileAction.ActionInfo.ResultName + ".";
            } catch (IOException ExceptionIO) {
                FmainPassed.FileAction.ResultName = "Action Result Error";
                FmainPassed.FileAction.ActionInfo.ResultName = FmainPassed.FileAction.ResultName;
                FmainPassed.FileAction.ActionInfo.ResultDescription = "Error converting File Action result";
                LocalMessage.ErrorMsg = FmainPassed.FileAction.ActionInfo.ResultDescription;
                ExceptIoImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionIO, FmainPassed.FileAction.Result);
                // throw new IOException (LocalMessage.ErrorMsg);
                XUomMovvXv.RunErrorDidOccur = true;
                LocalMessage.Msg6 += FmainPassed.FileAction.Name + " Failed with (" + FileDoResult + "), Verb Result (" + FmainPassed.FileAction.ActionInfo.omvOfResult + ") not properly set";
                LocalMessage.ErrorMsg = LocalMessage.Msg6;
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PickSystemCallStringResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                throw new NotSupportedException(LocalMessage.ErrorMsg);
            } // end of switch FmainPassed.omvOfResult
            #endregion
        }
        #endregion

        /// <summary> 
        /// Build the File Action Long Description string.
        /// </summary> 
        public void ResultDescriptionLong(ref FileMainDef FmainPassed) {
            sTemp2 = FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs);
            if (sTemp2.Length > 60) { sTemp2 = sTemp2.Substring(0, 57) + "..."; }
            sTemp3 = "The result "
                + " on file " + sTemp2 + ": ";
            sTemp4 = "The " + FmainPassed.FileAction.Name
                + " for direction " + FmainPassed.FileAction.DirectionName
                + " (using mode " + FmainPassed.FileAction.ModeName + ")"
                + " had the result: "
                + FmainPassed.FileAction.ResultName + " "
                + "(" + FileDoResult + ")." + "\n"
                + " Return result (" + FileDoResult + ")."
                ;
            LocalMessage.Msg6 += sTemp3 + "\n" + sTemp4;
            PrintOutputMdm_PickPrint(Sender, 3, "A2" + sTemp3, bYES);
            PrintOutputMdm_PickPrint(Sender, 3, "A2" + sTemp4, bYES);
        }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        #region Check Buffer Shift
        public long ShiftCheckResult;
        #region Increment Column Pointer
        public long FileIndexPointerIncrementResult;
        /// <summary> 
        /// Increment the File Index Pointer.
        /// </summary> 
        public long FileIndexPointerIncrement(ref FileMainDef FmainPassed) {
            FileIndexPointerIncrementResult = (int)StateIs.Started;
            //
            // Increment Column Pointer
            //
            iIterationCount += 1;
            //
            if (FmainPassed.DelSep.ItemAttrCounter > (iTraceShiftIndexByCount / 10) && iTraceByteCount > (5 * iTraceShiftIndexByCount)) {
                FileIndexPointerIncrementResult = ShiftCheck(ref FmainPassed);
            }
            if (ConsoleVerbosity >= 5) {
                LocalMessage.Msg0 = "Increment: Input (" + FmainPassed.DelSep.ItemAttrCounter.ToString() + ")";
                if (TraceOn) { XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.DelSep.ItemAttrMaxIndex, FileIndexPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg0 + "\n"); }
            }
            // (Mapplication)XUomMovvXv.XUomMovvXv.XUomPmvvXv.ProgressBarMdm1.Value += 1;
            // TODO Post Back Progress Bar ItemData
            switch (FmainPassed.Fs.FileTypeMajorId) {
                case ((long)FileType_LevelIs.Data):
                    // Processing ItemData
                    switch (FmainPassed.Fs.FileSubTypeMajorId) {
                        //////////////////////// TILDE ROW NOT BEING HANDLED
                        case ((long)FileType_SubTypeIs.SQL):
                        case ((long)FileType_SubTypeIs.MS):
                        case ((long)FileType_SubTypeIs.MY):
                        case ((long)FileType_SubTypeIs.CSV):
                        case ((long)FileType_SubTypeIs.TEXT
                        | (long)FileType_SubTypeIs.Tilde):
                            FmainPassed.DelSep.ItemAttrCount += 1; // total number columns.
                            FmainPassed.DelSep.ItemAttrCountTotal += 1; // total number columns.
                            FmainPassed.DelSep.ItemAttrCounter += 1; // move pointer
                            // Total # of Fields in Dict Item
                            // Increment Output Dictionary Pointer
                            if (FmainPassed.DelSep.ItemAttrIndex + 1 < 100) {
                                FmainPassed.DelSep.ItemAttrIndex += 1;// Next Field within Dict
                            }
                            if (ConsoleVerbosity >= 5) {
                                LocalMessage.Msg5 = "Output ItemData: ";
                                LocalMessage.Msg5 += " Count (" + FmainPassed.DelSep.ItemAttrCounter.ToString() + ")";
                                LocalMessage.Msg5 += ", Column (" + FmainPassed.DelSep.ItemAttrCounter.ToString() + ")";
                                if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.DelSep.ItemAttrMaxIndex, FileIndexPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg5 + "\n"); }
                            }
                            break;
                        case ((long)FileType_SubTypeIs.ASC):
                        case ((long)FileType_SubTypeIs.DAT):
                        case ((long)FileType_SubTypeIs.FIX):
                        default:
                            // Subtype error
                            FileIndexPointerIncrementResult = 0;
                            LocalMessage.Msg5 = "File SubType Error (" + FmainPassed.Fs.FileSubTypeId.ToString() + ") not properly set";
                            throw new NotSupportedException(LocalMessage.Msg5);
                    }
                    break;
                //
                case ((long)FileType_LevelIs.DictData):
                    // FileDictData
                    switch (FmainPassed.Fs.FileSubTypeMajorId) {
                        case ((long)FileType_SubTypeIs.SQL):
                        case ((long)FileType_SubTypeIs.MS):
                        case ((long)FileType_SubTypeIs.MY):
                        case ((long)FileType_SubTypeIs.CSV):
                            Fmain.ColIndex.ColCount += 1;
                            Fmain.ColIndex.ColCountTotal += 1;
                            Fmain.ColIndex.ColCounter += 1;
                            // Total # of Fields in Dict Item
                            // Increment Output Dictionary Pointer
                            if (Fmain.ColIndex.ColIndex + 1 < 100) {
                                Fmain.ColIndex.ColIndex += 1;// Next Field within Dict
                            }
                            //
                            if (ConsoleVerbosity >= 5) {
                                LocalMessage.Msg5 = "Output Dictionary: ";
                                LocalMessage.Msg5 += " Count (" + Fmain.ColIndex.ColCount.ToString() + ")";
                                LocalMessage.Msg5 += ", Attr (" + Fmain.ColIndex.ToString() + ")";
                                if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.DelSep.ItemAttrMaxIndex, FileIndexPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg5 + "\n"); }
                            }
                            break;
                        case ((long)FileType_SubTypeIs.TEXT
                        | (long)FileType_SubTypeIs.Tilde):
                        case ((long)FileType_SubTypeIs.ASC):
                        case ((long)FileType_SubTypeIs.DAT):
                        case ((long)FileType_SubTypeIs.FIX):
                        default:
                            // SubType Error 
                            FileIndexPointerIncrementResult = 0;
                            LocalMessage.Msg5 = "File SubType Error (" + FmainPassed.Fs.FileSubTypeId.ToString() + ") not properly set";
                            throw new NotSupportedException(LocalMessage.Msg5);
                    }
                    break;
                default:
                    // FileTypeUNKNOWN
                    FileIndexPointerIncrementResult = 0;
                    LocalMessage.Msg5 = "File Type Error (" + FmainPassed.Fs.FileTypeId.ToString() + ") not properly set";
                    throw new NotSupportedException(LocalMessage.Msg5);
            } // end or is DATA Attr not DICT
            return FileIndexPointerIncrementResult;
        }
        #endregion
        #region Increment Column Pointer
        public long ColPointerIncrementResult;
        /// <summary> 
        /// Increment the Column Index Pointer.
        /// </summary> 
        public long ColPointerIncrement(ref FileMainDef FmainPassed) {
            ColPointerIncrementResult = (int)StateIs.Started;
            if (FmainPassed.DelSep.ItemAttrCounter > (iTraceShiftIndexByCount / 10) && iTraceByteCount > (5 * iTraceShiftIndexByCount)) {
                ColPointerIncrementResult = ShiftCheck(ref FmainPassed);
            }
            if (ConsoleVerbosity >= 5) {
                LocalMessage.Msg0 = "Increment: Input (" + FmainPassed.DelSep.ItemAttrCounter.ToString() + ")";
                if (TraceOn) { XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, FmainPassed.Buf.CharMaxIndex, FmainPassed.DelSep.ItemAttrMaxIndex, ColPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg0 + "\n"); }
            }
            // (Mapplication)XUomMovvXv.XUomMovvXv.XUomPmvvXv.ProgressBarMdm1.Value += 1;
            // TODO Post Back Progress Bar ItemData
            switch (FmainPassed.Fs.FileTypeMajorId) {
                case ((long)FileType_LevelIs.Data):
                    // Processing ItemData
                    switch (FmainPassed.Fs.FileSubTypeMajorId) {
                        //////////////////////// TILDE ROW NOT BEING HANDLED
                        case ((long)FileType_SubTypeIs.SQL):
                        case ((long)FileType_SubTypeIs.MS):
                        case ((long)FileType_SubTypeIs.MY):
                        case ((long)FileType_SubTypeIs.CSV):
                        case ((long)FileType_SubTypeIs.TEXT
                        | (long)FileType_SubTypeIs.Tilde):
                            break;
                        case ((long)FileType_SubTypeIs.ASC):
                        case ((long)FileType_SubTypeIs.DAT):
                        case ((long)FileType_SubTypeIs.FIX):
                            // SubType Error 
                            ColPointerIncrementResult = (long)StateIs.Undefined;
                            LocalMessage.Msg5 = "File SubType Error (" + FmainPassed.Fs.FileSubTypeId.ToString() + ") not properly set";
                            throw new NotSupportedException(LocalMessage.Msg5);
                    }
                    if (Fmain.ColIndex.ColSet) {
                        Fmain.ColIndex.ColCounter += 1;
                        // if (ColCounter > ColCount) { ColCounter = ColCount; }
                        // Next Field within Dict
                        if (Fmain.ColIndex.ColIndex + 1 < 100) { Fmain.ColIndex.ColIndex += 1; }
                        if (ConsoleVerbosity >= 5) {
                            LocalMessage.Msg5 = "File ItemData Column Increment: ";
                            LocalMessage.Msg5 += " Count (" + Fmain.ColIndex.ColCount.ToString() + ")";
                            LocalMessage.Msg5 += ", Column (" + Fmain.ColIndex.ColCounter.ToString() + ")";
                            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.ColIndex.ColMaxIndex, ColPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg5 + "\n"); }
                        }
                    }
                    // Total # of Fields in Dict Item
                    // Increment Output Dictionary Pointer
                    if (FmainPassed.DelSep.ItemAttrSet) {
                        FmainPassed.DelSep.ItemAttrCounter += 1;
                        if (FmainPassed.DelSep.ItemAttrCounter > FmainPassed.DelSep.ItemAttrCount) { FmainPassed.DelSep.ItemAttrCount = FmainPassed.DelSep.ItemAttrCounter; }
                        if (FmainPassed.DelSep.ItemAttrIndex + 1 < 100) { FmainPassed.DelSep.ItemAttrIndex += 1; }
                        if (ConsoleVerbosity >= 5) {
                            LocalMessage.Msg5 = "File ItemData Column Attribue Increment: ";
                            LocalMessage.Msg5 += " Count (" + FmainPassed.DelSep.ItemAttrCount.ToString() + ")";
                            LocalMessage.Msg5 += ", Column (" + FmainPassed.DelSep.ItemAttrCounter.ToString() + ")";
                            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.ColIndex.ColMaxIndex, ColPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg5 + "\n"); }
                        }
                    }
                    break;
                //
                case ((long)FileType_LevelIs.DictData):
                    // FileDictData
                    switch (FmainPassed.Fs.FileSubTypeMajorId) {
                        case ((long)FileType_SubTypeIs.SQL):
                        case ((long)FileType_SubTypeIs.MS):
                        case ((long)FileType_SubTypeIs.MY):
                        case ((long)FileType_SubTypeIs.CSV):
                            break;
                        case ((long)FileType_SubTypeIs.TEXT
                        | (long)FileType_SubTypeIs.Tilde):
                        case ((long)FileType_SubTypeIs.ASC):
                        case ((long)FileType_SubTypeIs.DAT):
                        case ((long)FileType_SubTypeIs.FIX):
                        default:
                            // SubType Error 
                            ColPointerIncrementResult = (long)StateIs.Undefined;
                            LocalMessage.Msg5 = "File SubType Error (" + FmainPassed.Fs.FileSubTypeId.ToString() + ") not properly set";
                            throw new NotSupportedException(LocalMessage.Msg5);
                    }
                    if (Fmain.ColIndex.ColSet) {
                        Fmain.ColIndex.ColCounter += 1;
                        // if (ColCounter > ColCount) { ColCounter = ColCount; }
                        // Next Field within Dict
                        if (Fmain.ColIndex.ColIndex + 1 < 100) { Fmain.ColIndex.ColIndex += 1; }
                        if (ConsoleVerbosity >= 5) {
                            LocalMessage.Msg5 = "File Dictionary Column Increment: ";
                            LocalMessage.Msg5 += " Count (" + Fmain.ColIndex.ColCount.ToString() + ")";
                            LocalMessage.Msg5 += ", Column (" + Fmain.ColIndex.ColCounter.ToString() + ")";
                            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.ColIndex.ColMaxIndex, ColPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg5 + "\n"); }
                        }
                    }
                    if (Fmain.ColIndex.ColAttrSet) {
                        Fmain.ColIndex.ColAttrCounter += 1;
                        // if (ColAttrCounter > ColAttrCount) { ColAttrCount = ColAttrCounter; }
                        if (Fmain.ColIndex.ColAttrIndex + 1 < 100) { Fmain.ColIndex.ColAttrIndex += 1; }
                        //
                        if (ConsoleVerbosity >= 5) {
                            LocalMessage.Msg5 = "File Dictionary Column Attribue Increment: ";
                            LocalMessage.Msg5 += " Count (" + Fmain.ColIndex.ColAttrCount.ToString() + ")";
                            LocalMessage.Msg5 += ", Column (" + Fmain.ColIndex.ColAttrCounter.ToString() + ")";
                            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.ColIndex.ColMaxIndex, ColPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg5 + "\n"); }
                        }
                    }
                    break;
                default:
                    // FileTypeUNKNOWN
                    ColPointerIncrementResult = (long)StateIs.Undefined;
                    LocalMessage.Msg5 = "File Column Increment File Type Error (" + FmainPassed.Fs.FileTypeId.ToString() + ") not properly set";
                    throw new NotSupportedException(LocalMessage.Msg5);
            } // end or is DATA Attr not DICT
            return ColPointerIncrementResult;
        }
        #endregion
        /// <summary> 
        /// Perform a File Buffer Shift Check.
        /// </summary> 
        public long ShiftCheck(ref FileMainDef FmainPassed) {
            ShiftCheckResult = (int)StateIs.Started;
            // Shift Input Buffer after iTraceShiftIndexByCount lines or increment Input Buffer Index
            if (FmainPassed.DelSep.ItemAttrCounter > (iTraceShiftIndexByCount / 10) && iTraceByteCount > (5 * iTraceShiftIndexByCount)) {
                if (FmainPassed.DelSep.ItemAttrCounter > iTraceShiftIndexByCount || iTraceByteCount > (10 * iTraceShiftIndexByCount)) {
                    if (TraceBugOnNow && iTraceDisplayCountTotal > iTraceBugThreshold) {
                        LocalMessage.Msg9 = "Debug Point Reached, ready to Shift buffer " + iTraceShiftIndexByCount.ToString() + " lines, Index(" + FmainPassed.DelSep.ItemAttrCounter.ToString() + "), ShiftByteCount(" + iTraceByteCount.ToString() + "), ByteCount(" + iTraceByteCountTotal.ToString() + ")";
                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, FmainPassed.Buf.CharMaxIndex, FmainPassed.DelSep.ItemAttrMaxIndex, ShiftCheckResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg9 + "\n");
                        LocalMessage.Msg = LocalMessage.Msg9;
                    }
                    //
                    while (FmainPassed.DelSep.ItemAttrCounter > iTraceShiftIndexByCount) {
                        ShiftCheckResult = ItemDataShift(ref FmainPassed, iTraceShiftIndexByCount);
                        // ItemAttrCounter -= iTraceShiftIndexByCount;
                        iTraceByteCountTotal += (int)ShiftCheckResult;
                        iTraceByteCount -= (int)ShiftCheckResult;
                    }
                    //
                    if (TraceBugOnNow && iTraceDisplayCountTotal > iTraceBugThreshold) {
                        LocalMessage.Msg9 = "Debug Point Reached, Shift finished, now Index(" + FmainPassed.DelSep.ItemAttrCounter.ToString() + "), ShiftByteCount(" + iTraceByteCount.ToString() + "), ByteCount(" + iTraceByteCountTotal.ToString() + ")";
                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, FmainPassed.Buf.CharMaxIndex, FmainPassed.DelSep.ItemAttrMaxIndex, ColPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg9 + "\n");
                        //System.Diagnostics.Debug.WriteLine("MessageBlock [" + sTraceMessageBlock + "]");
                        //sTraceMessageBlock = "";
                        LocalMessage.Msg = LocalMessage.Msg9;
                    }
                }
            }
            //
            return ShiftCheckResult;
        }
        // Move Shift Item.ItemData by x Attrs
        public long ItemDataShiftResult;
        /// <summary> 
        /// File Buffer - Item Data Shift Data and Pointer.
        /// </summary> 
        public long ItemDataShift(ref FileMainDef FmainPassed, int iPassedAttrsToShift) {
            ItemDataShiftResult = (long)StateIs.Started;
            int iItemDataShifted = 0;
            // 
            // TODO $$$CHECK Use Primitive extension values here (McString)
            iItemDataShifted = PickIndex(FmainPassed.Item.ItemData, ColumnSeparator, iPassedAttrsToShift);
            if (iItemDataShifted >= 0) {
                // 
                if (FmainPassed.Item.ItemData.Length > iItemDataShifted) {
                    FmainPassed.Item.ItemData = FmainPassed.Item.ItemData.Substring(iItemDataShifted + 1);
                    //
                    FmainPassed.ColIndex.ColCounter -= iPassedAttrsToShift; // Current Attr
                    FmainPassed.ColIndex.ColMaxIndexTemp -= iPassedAttrsToShift;
                    if (FmainPassed.ColIndex.ColCounter < 0) {
                        FmainPassed.ColIndex.ColCounter = 0;
                        // error condition - ColIndex.ColCounter 
                    }
                } else { iItemDataShifted = 0; }
                // else { ColIndexPassed.Item.ItemData = ""; }
            } else { iItemDataShifted = 0; }
            // else { ColIndexPassed.Item.ItemData = ""; }
            //
            // TODO $$$CHECK Use Primitive extension values here (McString)
            // ItemDataShiftResult = ((FilePickDb)FilePickDbObject).PickItemDataCounterGet(ColIndexPassed);
            // ColIndexPassed.ColMaxIndex = 0; // Total Attrs in Item
            // ColIndexPassed.ColMaxIndexTemp = 0; // Total Attrs in Item
            // ItemDataAtrributeClear(ColIndexPassed);
            // Working value
            // ColIndexPassed.ColCounter = 0; // ItemData Items in Item / Row / Item
            // Character Pointers
            // ItemDataCharClear(ColIndexPassed);
            // 
            // TODO ItemDataShift NOTE More work needed on delimiters,
            // TODO ItemDataShift add delimiters as required.
            // TODO ItemDataShift NOTE Count of columns not rows 
            // TODO ItemDataShift check for csv handling before changing
            // TODO ItemDataShift NOTE This does not handle quoteded characters!!!
            //
            return iItemDataShifted;
        }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        #region File Property Clear
        // <Section Id = "x
        /// <summary> 
        /// Clear the Database Connection.
        /// </summary> 
        public void ConnectionClear(ref FileMainDef FmainPassed) {
            // ? Readers / Writers etc ?
            // ? Close ? Dispose ?
            FmainPassed.ConnStatus.DataClear();
        }
        // <Section Id = "x
        /// <summary> 
        /// Clear the Database Name.
        /// </summary> 
        public void DatabaseClear(ref FileMainDef FmainPassed) {
            // <Area Id = "SourceDatabaseInformation">
            FmainPassed.Fs.DatabaseName = "";
            FmainPassed.Fs.spTableNameLine = "";

            DbSyn.spDatabaseFileCreateCmd = "";

            FmainPassed.DbStatus.DataClear();
        }
        /// <summary> 
        /// Clear the Tabe Name.
        /// </summary> 
        public void TableNameClear(ref FileMainDef FmainPassed) {
            FmainPassed.Fs.TableName = "";
            FmainPassed.Fs.FileNameFullNext = "";
            FmainPassed.Fs.FileNameFullCurrent = "";
            FmainPassed.Fs.FileNameFullOriginal = "";
            FmainPassed.Fs.TableNameFull = "";
            FmainPassed.Fs.TableNameLine = "";
            //
            // FmainPassed.DbStatus.DataClear();
        }
        /// <summary> 
        /// Clear the Disk File Name.
        /// </summary> 
        public void FileNameClear(ref FileMainDef FmainPassed) {
            FmainPassed.Fs.FileId.FileName = "";
            FmainPassed.Fs.FileNameFullNext = "";
            FmainPassed.Fs.FileNameFullCurrent = "";
            FmainPassed.Fs.FileNameFullOriginal = "";
            FmainPassed.Fs.FileId.FileNameFull = "";
            FmainPassed.Fs.FileId.FileNameLine = "";
            //
            // FmainPassed.FileStatus.DataClear();
        }
        /// <summary> 
        /// Clear the File Item Id.
        /// </summary> 
        public void ItemIdClear(ref FileMainDef FmainPassed) {
            FmainPassed.Item.ItemId = "";
            FmainPassed.Fs.ItemIdNext = "";
            FmainPassed.Fs.ItemIdCurrent = "";
        }
        /// <summary> 
        /// Clear the Item Attribute Pointers.
        /// </summary> 
        public void ItemAtrributeClear(ref FileMainDef FmainPassed) {
            // Attr Handling for Ascii and Text
            // Ascii Attr Pointers
            // Working value
            FmainPassed.DelSep.ItemAttrCounter = 1; // Current Attr
            FmainPassed.DelSep.ItemAttrCount = 0; // ItemData Items in Item / Row / Item
            FmainPassed.DelSep.ItemAttrMaxIndex = 0; // Total Attrs in Item
            FmainPassed.DelSep.ItemAttrMaxIndexTemp = 0;
            //
        }
        /// <summary> 
        /// Clear the Item Attributes and Values Pointers.
        /// </summary> 
        public void ItemDataAtrributeClear(ref FileMainDef FmainPassed) {
            FmainPassed.DelSep.iItemDataAttrEos2Index = 0; // Current Column Separator 2
            FmainPassed.DelSep.iItemDataAttrEos1Index = 0; // Current Column Separator 1
            FmainPassed.DelSep.iItemDataAttrEosIndex = 0; // Current Column Sub-Value
            FmainPassed.DelSep.iItemDataAttrEovIndex = 0; // Current Column Value
            FmainPassed.DelSep.iItemDataAttrEoaIndex = 0; // Current Column
            FmainPassed.DelSep.iItemDataAttrEorIndex = 0; // Current Row
            FmainPassed.DelSep.iItemDataAttrEofIndex = 0; // Current File
        }
        /// <summary> 
        /// Clear the Item Data Characters Pointers.
        /// </summary> 
        public void ItemDataCharClear(ref FileMainDef FmainPassed) {
            // Character Pointers
            FmainPassed.DelSep.iItemDataCharEobIndex = 0; // End of Character Buffer
            FmainPassed.DelSep.iItemDataCharIndex = 0; // Character Pointer
            FmainPassed.DelSep.iItemDataCharEofIndex = 0; // Character End of File
        }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        #region Exception Code
        #region Common Exception Code
        public long ExceptCommonResult;
        //The array below will be implemented as an error object in future:
        public String[] ExceptCommonMessage;
        //
        /// <summary> 
        /// Exception Main Method - All Exceptions
        /// </summary> 
        public void ExceptCommonFileImpl(
            Object ExceptionPassed,
            ref FileMainDef FmainPassed,
            int PassedErrorLevel,
            int PassedErrorSource,
            String ErrorMsgPassed,
            long PassedMethodResult
            ) {
            String FileNameCurr = FmainPassed.Fs.FileNameLineGet(ref FmainPassed.Fs);
            ExceptCommonResult = PassedMethodResult;
            FmainPassed.FileAction.FileException.Add(ExceptionPassed);
            ConsoleVerbosity = 7;
            if (FileNameCurr != null) {
                if (FileNameCurr.Length > 0) {
                    //
                    LocalMessage.ErrorMsg = "File Name: " + FileNameCurr + ", ";
                    LocalMessage.ErrorMsg += "Direction: " + FmainPassed.Fs.DirectionName;
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMovvXv.RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                    //
                    LocalMessage.ErrorMsg = "System: " + FmainPassed.Fs.SystemName + ", ";
                    LocalMessage.ErrorMsg += "Database: " + FmainPassed.Fs.DatabaseName;
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMovvXv.RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                    //
                    LocalMessage.ErrorMsg = "File Owner: " + FmainPassed.Fs.FileOwnerName + ", ";
                    LocalMessage.ErrorMsg += "File Group: " + FmainPassed.Fs.FileGroupName;
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMovvXv.RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                }
            }
            if (FmainPassed.DbIo.CommandCurrent != null) {
                if (FmainPassed.DbIo.CommandCurrent.Length > 0) {
                    LocalMessage.ErrorMsg = "Database command: " + FmainPassed.DbIo.CommandCurrent;
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMovvXv.RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                }
            }
            if (FmainPassed.DbIo.spConnString != null) {
                if (FmainPassed.DbIo.spConnString.Length > 0) {
                    LocalMessage.ErrorMsg = "Database connection command: " + FmainPassed.DbIo.spConnString;
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMovvXv.RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
                }
            }
            if (ErrorMsgPassed.Length > 0) {
                if (ExceptCommonMessage != null) {
                    String sTemp0 = "";
                    for (int i = 0; i < ExceptCommonMessage.Length; i++) {
                        if (ExceptCommonMessage[i].Length > 0) {
                            sTemp0 = "Error Details (" + (i + 1) + "): " + ExceptCommonMessage[i];
                            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + sTemp0 + "\n");
                        }
                    }
                }
                LocalMessage.ErrorMsg = "Error Summary: " + ErrorMsgPassed;
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMovvXv.RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageEnterResume, "A2" + LocalMessage.ErrorMsg + "\n");
            } else {
                LocalMessage.ErrorMsg = "No Error Details available.";
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMovvXv.RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageEnterResume, "A2" + LocalMessage.ErrorMsg + "\n");
            }
            //
            ExceptCommonMessage = null; // clear after all handling...
        }
        #endregion
        #region SQL Exception
        public long ExceptSqlResult;
        /// <summary> 
        /// Exceptions Main Method - SQL Exceptions
        /// SECTION MessageDetailsMdm not implemented.
        /// Two classes Trace and Message
        /// Exceptions are part of Trace Class
        /// Database classes can be 
        /// </summary> 
        public void ExceptSqlImpl(
            ref FileMainDef FmainPassed,
            String ErrorMsgPassed,
            ref SqlException ExceptionSql,
            long PassedMethodResult
            ) {
            ExceptSqlResult = PassedMethodResult;
            FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.SqlError;
            LocalMessage.ErrorMsg = "SQL Database Exception: (" + PassedMethodResult.ToString() + "): ";
            // + ExceptSql.Message;
            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
            //
            ExceptCommonMessage = new String[ExceptionSql.Errors.Count];
            for (int i = 0; i < ExceptionSql.Errors.Count; i++) {
                ExceptCommonMessage[i] = ExceptionSql.Errors[i].ToString();
            }
            ExceptCommonFileImpl(ExceptionSql, ref FmainPassed, iNoErrorLevel, iNoErrorSource, LocalMessage.ErrorMsg, PassedMethodResult);
        }
        #endregion
        #region General File Exception
        public long ExceptGeneralFileResult;
        /// <summary> 
        /// Exceptions Main Method - File Exceptions
        /// Routes from SQL Exception.
        /// Routes to General Exception.
        /// </summary> 
        public void ExceptGeneralFileImpl(
            ref FileMainDef FmainPassed,
            String ErrorMsgPassed,
            ref Exception ExceptionGeneral,
            long PassedMethodResult
            ) {
            ExceptGeneralFileResult = PassedMethodResult;
            FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.General;
            LocalMessage.ErrorMsg = "General Exception: (" + PassedMethodResult.ToString() + ") " + ExceptionGeneral.Message;
            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
            ExceptCommonFileImpl(ExceptionGeneral, ref FmainPassed, iNoErrorLevel, iNoErrorSource, LocalMessage.ErrorMsg, PassedMethodResult);
            XUomMovvXv.RunErrorDidOccur = bNO;
        }
        #endregion
        #region NotSupported Exception
        public long ExceptNotSupportedResult;
        /// <summary> 
        /// Exceptions - Not Supported Exception
        /// Routes to General Exception.
        /// </summary> 
        public void ExceptNotSupportedImpl(
            ref FileMainDef FmainPassed,
            String ErrorMsgPassed, ref 
            NotSupportedException ExceptionNotSupported,
            long PassedMethodResult
            ) {
            ExceptNotSupportedResult = PassedMethodResult;
            FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.NotSupported;
            LocalMessage.ErrorMsg = "NotSupported Exception: (" + PassedMethodResult.ToString() + ") " + ExceptionNotSupported.Message;
            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
            ExceptCommonFileImpl(ExceptionNotSupported, ref FmainPassed, iNoErrorLevel, iNoErrorSource, LocalMessage.ErrorMsg, PassedMethodResult);
            XUomMovvXv.RunErrorDidOccur = bNO;
        }
        #endregion
        #region IO Exception
        public long ExceptIoResult;
        /// <summary> 
        /// Exceptions - IO Exception
        /// Routes to File Exception.
        /// </summary> 
        public void ExceptIoImpl(
            ref FileMainDef FmainPassed,
            String ErrorMsgPassed,
            ref IOException ExceptionIo,
            long PassedMethodResult
            ) {
            ExceptIoResult = PassedMethodResult;
            FmainPassed.FileStatus.ipStatusCurrent = (long)FileIo_ErrorIs.IoError;
            LocalMessage.ErrorMsg = "IO Exception: (" + PassedMethodResult.ToString() + ") " + ExceptionIo.Message;
            XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PassedMethodResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.ErrorMsg + "\n");
            ExceptCommonFileImpl(ExceptionIo, ref FmainPassed, iNoErrorLevel, iNoErrorSource, LocalMessage.ErrorMsg, PassedMethodResult);
            XUomMovvXv.RunErrorDidOccur = bNO;
        }
        #endregion
        //
        // END OF COMMON EXCEPTIONS
        #endregion
        /// <summary> 
        /// String Description of File System Object
        /// </summary> 
        public override String ToString() {
            String FileNameCurr = Fmain.Fs.FileNameLineGet(ref Fmain.Fs);
            if (FileNameCurr != null && Fmain.Fs.DirectionName != null) {
                // String sTemp = "File Summary: " + FileNameCurr + " for " + Fmain.Fs.DirectionName;
                String sTemp = Fmain.Fs.ToString();
                return sTemp;
            } else { return base.ToString(); }
            return "";
        }
    }
    // End of Mfile
}
