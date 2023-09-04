#region Dependencies
#region System
using System;
#endregion
#region System Collections
using System.Collections;
using System.Collections.Generic;
//using System.Collections.Specialized;
#endregion
#region System Data & IO
using System.IO;
using System.Data;
//using System.Data.Common;
#endregion
#region System SQL
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
#endregion
#region System Text and Linq
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
#endregion
#region System Serialization (Runtime and Xml)
using System.Runtime.Serialization;
// using System.Runtime.Serialization.DataContractSerializer;
using System.Runtime.Serialization.Formatters.Binary;
// using System.Runtime.Serialization.XmlObjectSerializer;
using System.Xml.Serialization;
#endregion

#region  Mdm Core
using Mdm.Oss;
//@@@CODE@@@using Mdm.Oss.ClipUtil;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
// using Mdm.Oss.Mobj;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Components;
using Mdm.World;
#endregion
#region  Mdm MVC Mobject
using Mdm.Oss.Mobj;
#endregion
#region  Mdm Db and File
using Mdm.Oss.File;
using Mdm.Oss.File.Control;
using Mdm.Oss.File.Db;
using Mdm.Oss.File.Db.Data;
using Mdm.Oss.File.Db.Table;
using Mdm.Oss.File.Db.Thread;
//using Mdm.Oss.File.Properties;
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
//using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Pick;
using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion

namespace Mdm.Oss.File
{
    // ToDo not all dispose functions are done correctly.  See warnings...
    ///<summary>
    /// Namespace Convention
    /// Company.
    /// Concern.
    /// Role
    /// Sub-role
    /// Namespace Handling (role)
    /// For sealed classes
    /// 1) Utility Instance and Static Class
    /// 2) Utility Static Class as Extension
    /// Interface for all file types
    /// </summary>
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
    /// <para> The States or Options and Actions on This Object include flags such as ConnDoKeepConn which indicates that the Using Class wants This Object to ultimately be returned in an Open State.</para>
    /// <para> .</para>
    /// </remarks> 
    [DataContract(Namespace = "")]
    public partial class mFileDef : Mobject, AppStd, ImFileType, IDisposable
    {
        #region Class
        #region Class Fields
        #region Core Fields
        #region $include Mdm.Oss.FileUtil mFileDef FileBase_Classes Primary & Auxillary
        /// <summary> 
        /// Core
        /// File Main Primary object and File Auxillary object.
        /// </summary> 
        public mFileMainDef Fmain;
        public mFileMainDef Faux;

        public new Mobject XUomMovvXv;
        //@@@CODE@@@public Mapplication XUomMavvXv;
        public new mFileDef FileObject;
        /// <summary>
        /// </summary>
        public mFileSqlConnectionDef FileSqlConn;
        #endregion
        #endregion
        #region Class Exceptions, Return Results and Meta Data
        // Database Error Handling - General
        #region $include Mdm.Oss.FileUtil mFileDef EXCEPTIONS SECTION
        #region $include Mdm.Oss.FileUtil mFileDef Exception - Database - Command - Os
        // Exception
        // System Exception
        public Exception DbFileCmdOsException; // General Os
        #endregion
        #region $include Mdm.Oss.FileUtil mFileDef Exception - Database
        // <Area Id = "FileDatabaseStatus">

        #endregion
        #region $include Mdm.Oss.FileUtil mFileDef PathExceptions
        /// <remarks>
        /// PATH ARGUMENT EXCEPTIONS
        /// <Area Id = "ArgumentException:
        ///  path is a zero-length string, contains only white space, or 
        ///  contains one or more invalid characters as defined by InvalidPathChars.  ">
        /// </Area> 
        /// 
        /// <Area Id = "ArgumentNullException:
        ///  path is null reference
        ///  path is null or Nothing or null ptra null 
        ///  (Nothing in Visual Basic). ">
        /// </Area> 
        /// 
        /// PATH LENGTH EXCEPTIONS
        /// <Area Id = "PathTooLongException:
        ///  The specified path, file name, or both 
        ///  exceed the system-defined maximum length. 
        ///  For example, on Windows-based platforms, 
        ///  paths must be less than 248 characters, 
        ///  and file names must be less than 260 characters. ">
        /// </Area> 
        /// 
        /// PATH DIRECTORY TOO LONG
        /// <Area Id = "DirectoryNotFoundException:
        ///  The specified path is invalid, 
        ///  (for example, it is on an unmapped drive). ">
        /// </Area> 
        /// 
        /// ACCESS PERMISSION EXCEPTIONS
        /// <Area Id = "UnauthorizedAccessException:
        ///  path specified a directory. 
        ///  -or- 
        ///  The caller does not have the required permission. ">
        /// </Area> 
        /// 
        /// FILE MISSING EXCEPTIONS
        /// <Area Id = "FileNotFoundException:
        ///  The file specified in path was not found. ">
        /// </Area> 
        /// 
        /// PATH FORMAT UNSUPPORTED
        /// <Area Id = "NotSupportedException:
        ///  path is in an invalid format. ">
        /// </Area> 
        /// </remarks> 
        #endregion
        #region $include Mdm.Oss.FileUtil mFileDef GeneralException
        public Exception ExceptionGeneral; // General Exception
        public IOException ExceptionIO; // Input Output Exception
        public NotSupportedException ExceptionNotSupported; // Not Supported Exception
        #endregion
        #endregion
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
        public mFileStateDef FileState;
        // Delegates;
        // Buf.
        public int CharMaxIndexGet() { return Fmain.Buf.CharMaxIndex; }
        public int AttrIndexGet() { return Fmain.Buf.AttrIndex; }
        // Buf.CharMaxIndex, Buf.AttrIndex

        // Pick File - xxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil mFileDef Operation Meta Data
        /// <summary> 
        /// File System Object Meta Data, System Object,
        /// and working fields.
        /// </summary> 
        public MetaDef Meta;
        public SysDef Sys;
        public DbFileTempDef DbFileTemp;
        // File Transformation
        // pointer NOT INCLUDED with the mFileDef class temporarily...
        // public Object FileTransformControl;
        #region $include Mdm.Oss.FileUtil mFileDef FileParent & Consolodation
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
        #endregion
        // Controls to avoid infinite loops
        #region Framework Objects
        #endregion
        #region Class Factory
        #endregion
        #endregion
        #region Pick Specific Objects
        public PickDictIndexDef PickDictIndex; // ToDo
        public PickDictItemDef PickDictItem;
        public PickRowDef PickRow;
        public PickColDef PickCol;
        #endregion
        #region Class Methods
        // Database
        #region DatabaseFile
        #region Database Reset
        // <Section Id = "x
        /// <summary> 
        /// Reset the Database.
        /// </summary> 
        public virtual StateIs DatabaseReset(ref mFileMainDef FmainPassed)
        {
            FileState.DatabaseResetResult = StateIs.Started;
            DatabaseClear(ref FmainPassed);
            FileState.DatabaseResetResult = FmainPassed.FileSqlConn.ConnReset(ref FmainPassed);
            return FileState.DatabaseResetResult;
        }
        #endregion
        #region Database Table Name Validate empty
        #endregion
        /// <summary> 
        /// Set the default field values for the Database.
        /// </summary> 
        public virtual StateIs DatabaseFieldsGetDefault(ref mFileMainDef FmainPassed)
        {
            FileState.DatabaseFieldsGetDefaultsResult = StateIs.Started;
            bool CopyIsDone = false;
            FileState.DoingDefaults = true;
            // System
            if (FmainPassed.Fs.SystemName.Length == 0)
            {
                if (!CopyIsDone) { CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.SystemName = FmainPassed.FileSqlConn.SystemNameGetDefault(ref Faux);
            }
            // Service
            if (FmainPassed.Fs.ServiceName.Length == 0)
            {
                if (!CopyIsDone) { CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.ServiceName = FmainPassed.FileSqlConn.ServiceNameGetDefault(ref Faux);
            }
            // Server
            if (FmainPassed.Fs.ServerName.Length == 0)
            {
                if (!CopyIsDone) { CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.ServerName = FmainPassed.FileSqlConn.ServerNameGetDefault(ref Faux);
            }
            // Database
            if (FmainPassed.Fs.DatabaseName.Length == 0)
            {
                if (!CopyIsDone) { CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.DatabaseName = FmainPassed.FileSqlConn.DatabaseNameGetDefault(ref Faux);
            }
            // FileOwnerName
            if (FmainPassed.Fs.FileOwnerName.Length == 0)
            {
                if (!CopyIsDone) { CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.FileOwnerName = FmainPassed.FileSqlConn.FileOwnerGetDefault(ref Faux);
            }
            //
            FileState.DoingDefaults = false;
            return (FileState.DatabaseFieldsGetDefaultsResult = StateIs.Successful);
        }
        #region Database Table Name Build
        // <Section Id = "TableNameLineBuild">
        /// <summary> 
        /// Build the Table Name Line for the Passed Table.
        /// </summary> 
        public String TableNameLineBuild(ref mFileMainDef FmainPassed)
        {
            FileState.TableNameLineBuildResult = StateIs.Started;
            if (!FileState.DoingDefaults) { FileState.TableNameLineBuildResult = DatabaseFieldsGetDefault(ref FmainPassed); }
            FmainPassed.Fs.TableNameLine = FmainPassed.Fs.TableNameLineBuild(ref FmainPassed);
            //
            FmainPassed.FileStatus.bpNameIsValid = true; // ToDo ?? bpNameIsValid where is this used ??
            FileState.DatabaseFileLongResult = StateIs.Successful;
            return FmainPassed.Fs.TableNameLine;
        }
        // <Section Id = "TableNameLineBuild">
        /// <summary> 
        /// Build the Table Name Line for this Table.
        /// </summary> 
        public StateIs TableNameLineBuild()
        {
            FileState.DatabaseFileLongResult = StateIs.Started;
            Fmain.Fs.TableNameLine = TableNameLineBuild(ref Fmain);
            return (FileState.DatabaseFileLongResult = StateIs.Successful);
        }
        #endregion
        #region Database Creation
        /// <summary> 
        /// Exceptions handling for Create Command handling.
        /// </summary> 
        public void ExceptTableCreationImpl(ref SqlException ExceptionSql)
        {
            ExceptTableCreationImpl(ref Fmain, ref ExceptionSql);
        }
        /// <summary> 
        /// Exception handling for Passed File on Create Command handling.
        /// </summary> 
        /// <param name="FmainPassed"></param> 
        /// <param name="ExceptionSql"></param> 
        public void ExceptTableCreationImpl(ref mFileMainDef FmainPassed, ref SqlException ExceptionSql)
        {
            FileState.DatabaseFileCreationErrorResult = StateIs.Started;
            FmainPassed.FileStatus.FileErrorCurrent = FileIo_ErrorIs.DatabaseError;
            Sys.sMessageBoxMessage = StdProcess.Title + @"File Creation Status";
            Sys.sMessageBoxMessage += "\n" + @"Creation error!";
            Sys.sMessageBoxMessage += "\n" + @"SQL Exception Error";
            Sys.sMessageBoxMessage += "\n" + ExceptionSql.ToString();
            // <Area Id = "display message
        }
        #endregion
        #region Database Deletion
        #endregion
        #region Table Open Exceptions
        /// <summary> 
        /// An SQL Exception while Opening Tables.
        /// </summary> 
        /// <param name="ExceptionSql">The SQL exception that occurred.</param> 
        /// <remarks></remarks> 
        public void ExceptTableOpenError(ref SqlException ExceptionSql)
        {
            ExceptTableOpenError(ref Fmain, ref ExceptionSql);
        }
        /// <summary> 
        /// An SQL Exception while Opening on the passed Table.
        /// </summary> 
        /// <param name="FmainPassed"></param> 
        /// <param name="ExceptionSql">The SQL exception that occurred.</param> 
        /// <remarks></remarks> 
        public void ExceptTableOpenError(ref mFileMainDef FmainPassed, ref SqlException ExceptionSql)
        {
            FileState.DatabaseFileOpenErrorResult = StateIs.Started;
            FmainPassed.FileStatus.FileErrorCurrent = FileIo_ErrorIs.SqlError;
            // sMessageBoxMessage = StdProcess.Title + "\n";
            // sMessageBoxMessage += "\n" + @"SQL Exception Error!";
            // sMessageBoxMessage += "\n" + @"File Open Error!";
            // try {
            // sMessageBoxMessage += "\n" + ExceptSql.ToString();
            // } catch (Exception ExceptionNotSupported) { ; }
            // ((ImTrace)ConsoleSender).TraceMdmDoImpl(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(),PickSystemCallStringResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, sMessageBoxMessage);

            // <Area Id = "display message
        }
        #endregion
        #region Database Close
        /// <summary> 
        /// Exceptions handling for Table Close Command handling.
        /// </summary> 
        public void ExceptTableCloseImpl(ref SqlException ExceptionSql)
        {
            ExceptTableCloseImpl(ref Fmain, ref ExceptionSql);
        }
        /// <summary> 
        /// Exceptions handling for Table Close Command handling for Passed File.
        /// </summary> 
        public void ExceptTableCloseImpl(ref mFileMainDef FmainPassed, ref SqlException ExceptionSql)
        {
            FileState.DatabaseFileCloseErrorResult = StateIs.Started;
            Sys.sMessageBoxMessage = StdProcess.Title + "\n" + @"File Creation Status";
            Sys.sMessageBoxMessage += "\n" + @"Close Connection error!";
            Sys.sMessageBoxMessage += "\n" + @"SQL Exception Error";
            Sys.sMessageBoxMessage += "\n" + ExceptionSql.ToString();
            // <Area Id = "display message
        }
        #endregion
        #endregion
        #endregion
        #region Class Interface
        // implemented in partial classes
        #endregion
        #endregion
        #region Unit Test
        #endregion
    }
    public partial class mFileDef
    {
        #region mFileType
        // Initialize
        public virtual StateIs InitializeType() { return StateIs.Undefined; }
        public virtual StateIs InitializeType(object Sender) { return StateIs.Undefined; }
        // Attach
        public virtual StateIs Attach() { return StateIs.Undefined; }
        // Detach
        public virtual StateIs Detach() { return StateIs.Undefined; }
        // Dispose
        public virtual StateIs DisposeType() { return StateIs.Undefined; }
        // Open
        public virtual StateIs Open() { return StateIs.Undefined; }
        // Close
        public virtual StateIs Close() { return StateIs.Undefined; }
        // Insert
        public virtual StateIs Insert() { return StateIs.Undefined; }
        // Delete
        public virtual StateIs Delete() { return StateIs.Undefined; }
        // Update
        public virtual StateIs Update() { return StateIs.Undefined; }

        #endregion
    }

}
