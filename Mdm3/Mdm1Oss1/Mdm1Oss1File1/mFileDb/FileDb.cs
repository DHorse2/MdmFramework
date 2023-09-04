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
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Mobj;
using Mdm.Oss.Components;

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
using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion

namespace Mdm.Oss.File.Db
{
    #region DbIoDef - mFile Readers, Writers, Database and File Objects and Buffers
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
    public class DbIoDef : IDisposable
    {
        #region $include Mdm.Oss.File mFile FileDbConnObjectConnection
        // <Area Id = "FileDbConnObject">
        // Database Connection
        // ofd  - 	Object - File - Database Connection
        /// <summary>
        /// </summary>
        public SqlConnection SqlDbConn = null;
        // ofde - 	Object - File - Database Connection - Error

        // ofdcd - 	Object - File - Database Connection - Delegate

        // ofdcv - 	Object - File - Database Connection - Event

        #endregion
        // SqlClient - SqlDataReader - xxxxxxxxxxxxxxxxxxxx
        // <Area Id = "ConnString">
        // property String ConnString
        /// <summary>
        /// </summary>
        public String spConnString;
        /// <summary>
        /// </summary>
        public String ConnString
        {
            get { return spConnString; }
            set
            {
                spConnString = value;
            }
        }

        // Database Command - xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.File mFile FileDbConnObjectCommand
        // ofdc - 		Object - File - Database Command
        /// <summary>
        /// </summary>
        public SqlCommand SqlDbCommand = null;
        /// <summary>
        /// </summary>
        public int SqlDbRowsAffected = 0;
        /// <summary>
        /// </summary>
        public SqlDataReader SqlDbReader = null;
        /// <summary>
        /// </summary>
        public int SqlDbRowsRead = 0;
        /// <summary>
        /// </summary>
        public int SqlDbRowsWritten = 0;

        // Database Command - Timeout
        /// <summary>
        /// </summary>
        public int SqlDbCommandTimeout = 30;

        // Database Command - Error

        // Database Command - Delegate

        // File - Database Command - Event

        // File - Database Command - Adapter
        /// <summary>
        /// </summary>
        public SqlDataAdapter SqlDbAdapterObject = null;

        /// <summary>
        /// </summary>
        public String CommandCurrent = null;
        /// <summary>
        /// </summary>
        public String CommandPassed = null;
        #endregion
        // FILE SUBCLASS - FILE - READER
        // Database Sql ItemData Reader - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.File mFile FileObject SqlDataReader
        // ofddr - 		Object - File - Database Connection - DataReader
        /// <summary>
        /// </summary>
        public SqlDataReader SqlDbDataReader = null;
        /// <summary>
        /// </summary>
        public SqlDataAdapter SqlDbDataWriter = null;

        // ofddre - 	Object - File - Database Connection - DataReader- Error

        // ofddrcd - 	Object - File - Database Connection - DataReader- Delegate

        // ofddrcv - 	Object - File - Database Connection - DataReader- Event

        #endregion

        #region Destructors
        // Track whether Dispose has been called.
        /// <summary>
        /// </summary>
        private bool disposed = false;
        /// <summary>
        /// </summary>
        private bool instantiated = false;

        /// <summary>
        /// </summary>
        ~DbIoDef()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// </summary>
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (components != null) {
                //    components.Dispose();
                //}
            }
            DataClear();
            // base.Dispose(disposing);
            disposed = true;
        }
        #endregion

        /// <summary>
        /// </summary>
        public void DataClear()
        {
            CommandCurrent = "";
            CommandPassed = "";
            SqlDbCommandTimeout = 15;
            SqlDbRowsRead = 0;
            SqlDbRowsWritten = 0;
            SqlDbRowsAffected = 0;
            spConnString = "";
            //
            // ToDo Error on the following statment:
            try
            {
                //if (SqlDbConn != null) { SqlDbConn.Dispose(); }
                if (SqlDbCommand != null) { SqlDbCommand.Dispose(); }
                if (SqlDbAdapterObject != null) { SqlDbAdapterObject.Dispose(); }
                if (SqlDbDataReader != null)
                {
                    if (!SqlDbDataReader.IsClosed) { SqlDbDataReader.Close(); }
                    // SqlDbDataReader.Dispose();
                }
                if (SqlDbDataWriter != null) { SqlDbDataWriter.Dispose(); }
            }
            catch (Exception) {; }
        }
        /// <summary>
        /// </summary>
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
    #endregion
    #region Database DbIdDef, DbMasterDef
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
    public class DbIdDef
    {
        // File Database Information - xxxxxxxxxx
        // #region $include Mdm.Oss.File mFile FileBaseDatabase
        #region $include Mdm.Oss.File mFile FileDatabaseName

        // <Area Id = "FileDatabaseInformation">
        #endregion
        #region $include Mdm.Oss.File mFile FileDatabaseSecurity
        #endregion
        #region $include Mdm.Oss.File mFile FileDatabaseUser
        #endregion
        // #endregion

        // <Area Id = "SourceDatabaseFileGroupInformation">

        // <Area Id = "SourceDatabaseFileNameInformation">

        // <Area Id = "DatabaseMessageConstants">

        /// <summary>
        /// </summary>
        public const String SqlConnectionString =
            "Server=localhost;" +
            "DataBase=;" +
            "Integrated Security=SSPI";

        /// <summary>
        /// </summary>
        public const String ConnectionErrorMsg =
            "To run this sample, you must have SQL " +
            "or MSDE with the Northwind database installed.  For " +
            "instructions on installing MSDE, view the ReadMe file.";

        /*
        /// <summary>
        /// </summary>
        public const String MSDE_CONNECTION_STRING =
            @"Server=(local)\NetSDK;" +
            "DataBase=;" +
            "Integrated Security=SSPI";
        */
        // <Area Id = "FileDatabaseInformation">

        // See DATABASE REGION FOR THESE FIELDS

        /// <summary>
        /// </summary>
        public void DataClear()
        {
        }
    }
    /// <summary>
    /// <para>Master File Class for Database</para>
    /// <para> A separate database object for performing
    /// actions on the master files independent of the
    /// current open database file stream object.</para>
    /// </summary>
    public class DbMasterDef
    {
        // OBJECT SUBCLASS - MASTER FILE
        public const string sUnknown = "unknown";
        public const string sEmpty = "";
        public const int iUnknown = 99999;
        // Database Master File - xxxxxxxxxxxxxxx
        #region $include Mdm.Oss.File mFile FileMasterServer
        // <Area Id = "FileCommand">
        #region $include Mdm.Oss.File mFile FileMaster_Security_Fields

        // <Area Id = "SecurityControl">
        // property bool UseSSIS
        /// <summary>
        /// </summary>
        public bool bpUseSSIS = true;
        /// <summary>
        /// </summary>
        public bool UseSSPI
        {
            get { return bpUseSSIS; }
            set { bpUseSSIS = value; }
        } //   

        /// <summary>
        /// </summary>
        public String MstrSecurityId = sUnknown;
        /// <summary>
        /// </summary>
        public String MstrSecurity;

        #endregion
        #region $include Mdm.Oss.File mFile FileMaster_User_Control

        // <Area Id = "UserControl">
        /// <summary>
        /// </summary>
        public String MstrUserServerId = sUnknown;
        /// <summary>
        /// </summary>
        public String MstrUserDbId = sUnknown;
        /// <summary>
        /// </summary>
        public String MstrUserId = sUnknown;
        /// <summary>
        /// </summary>
        public String MstrUser = "MdmUser99";
        /// <summary>
        /// </summary>
        public String MstrUserPw = "password99";


        // <Area Id = "UserStatus">
        /// <summary>
        /// </summary>
        public bool UserDoesExist = false;
        /// <summary>
        /// </summary>
        public bool UserIsInvalid = false;
        /// <summary>
        /// </summary>
        public bool UserIsCreating = false;
        /// <summary>
        /// </summary>
        public bool UserIsCreated = false;
        #endregion
        #region $include Mdm.Oss.File mFile FileMaster_Server_Database
        // <Area Id = "MasterServerInformation">

        // <Area Id = "MasterServerDatabase">
        //
        /// <summary>
        /// </summary>
        public String MstrDbSystem = sEmpty;
        /// <summary>
        /// </summary>
        public String MstrDbSystemMdm = @"MDMPC13";
        /// <summary>
        /// </summary>
        // public String MstrDbSystemDefault = "localhost";
        /// <summary>
        /// </summary>
        public String MstrDbSystemDefault = sEmpty; // SYSTEM99
        /// <summary>
        /// </summary>
        public String MstrDbSystemDefaultMdm = @"MDMPC13";
        //
        /// <summary>
        /// </summary>
        // public String MstrDbService = "SQLSERVER";
        /// <summary>
        /// </summary>
        public String MstrDbService = sEmpty; // SQLSERVER
        /// <summary>
        /// </summary>
        public String MstrDbServiceMdm = @"SQLEXPRESS";
        /// <summary>
        /// </summary>
        // public String MstrDbServiceDefault = "SQLSERVER";
        /// <summary>
        /// </summary>
        public String MstrDbServiceDefault = sEmpty; // SERVICE99
        /// <summary>
        /// </summary>
        public String MstrDbServiceDefaultMdm = @"SQLEXPRESS";
        //
        //
        // public String MstrDbServer = "localhost";
        /// <summary>
        /// </summary>
        public String MstrDbServer = sEmpty;
        /// <summary>
        /// </summary>
        // public String MstrDbServerMdm = "MdmServer99";
        /// <summary>
        /// </summary>
        public String MstrDbServerMdm = @"MDMPC13\SQLEXPRESS";
        //
        //
        // public String MstrDbServerDefault = "localhost";
        /// <summary>
        /// </summary>
        public String MstrDbServerDefault = @"SERVER99";
        //
        //
        // public String MstrDbServerDefaultMdm = "MdmServer99";
        /// <summary>
        /// </summary>
        public String MstrDbServerDefaultMdm = @"MDMPC13\SQLEXPRESS";
        //
        //
        //
        public String MstrDbServerId = sUnknown;
        //
        //
        // public String MstrDbServerMasterDefault = "master..sysdatabases";
        //
        //
        // public String MstrDbServerMasterDefaultMdm = "MdmServer99..sysdatabases";
        /// <summary>
        /// </summary>
        public String MstrDbServerMasterDefault = @"SERVER99";
        /// <summary>
        /// </summary>
        public String MstrDbServerMasterDefaultMdm = @"MDMPC13\SQLEXPRESS..sysdatabases";
        //
        // <Area Id = "MasterServerFilesLocation">
        //
        //
        //public String MstrDbMasterFile = "localhost.dbo.sysobjects";
        //public String MstrDbMasterFileMdm = "MdmServer99.dbo.sysobjects";
        //public String MstrDbMasterFileDefault = "localhost.dbo.sysobjects";
        //public String MstrDbMasterFileDefaultMdm = "MdmServer99.dbo.sysobjects";
        //
        //
        //
        //
        //public String MstrDbMasterFile = "localhost..sysobjects";
        //public String MstrDbMasterFileMdm = "MdmServer99..sysobjects";
        //public String MstrDbMasterFileDefault = "localhost..sysobjects";
        //public String MstrDbMasterFileDefaultMdm = "MdmServer99..sysobjects";
        //
        //
        /// <summary>
        /// </summary>
        public String MstrDbMasterFile = @"MDMPC13\SQLEXPRESS..sysobjects";
        /// <summary>
        /// </summary>
        public String MstrDbMasterFileMdm = @"MDMPC13\SQLEXPRESS....sysobjects";
        /// <summary>
        /// </summary>
        public String MstrDbMasterFileDefault = @"MDMPC13\SQLEXPRESS....sysobjects";
        /// <summary>
        /// </summary>
        public String MstrDbMasterFileDefaultMdm = @"MDMPC13\SQLEXPRESS....sysobjects";

        // <Area Id = "MasterServer - ServerControl">

        // <Area Id = "MasterServer - Connection">
        /// <summary>
        /// </summary>
        public String MstrCmd = "not used";
        // <Area Id = "MasterServer - Creation">
        /// <summary>
        /// </summary>
        public String MstrDbServerCreateCmd;

        // <Area Id = "MasterServer - DatabaseControl">
        /// <summary>
        /// </summary>
        public String MstrDbDatabaseId = sUnknown;
        /// <summary>
        /// </summary>
        // public String MstrDbDatabase = "dbo";
        /// <summary>
        /// </summary>
        public String MstrDbDatabase = sEmpty;
        /// <summary>
        /// </summary>
        public String MstrDbDatabaseDefault = "Database99";
        /// <summary>
        /// </summary>
        public String MstrDbDatabaseDefaultMdm = "MdmDatabase99";
        // <Area Id = "MasterServer - FileGroupControl">
        /// <summary>
        /// </summary>
        public String MstrDbFileGroupId = sEmpty;
        /// <summary>
        /// </summary>
        public String MstrDbFileGroup = sEmpty;
        /// <summary>
        /// </summary>
        public String MstrDbFileGroupDefault = sEmpty;
        /// <summary>
        /// </summary>
        public String MstrDbFileGroupDefaultMdm = sEmpty;

        // <Area Id = "MasterServer - OwnerControl">
        /// <summary>
        /// </summary>
        public String MstrDbOwnerId = sUnknown;
        // public String MstrDbOwner = "dbo";
        // public String MstrDbOwnerDefault = "sa";
        // public String MstrDbOwnerDefaultMdm = "MdmOwner99";
        /// <summary>
        /// </summary>
        public String MstrDbOwner = "dbo";
        // public String MstrDbOwnerDefault = "sa";
        /// <summary>
        /// </summary>
        public String MstrDbOwnerDefault = "dbo";
        /// <summary>
        /// </summary>
        public String MstrDbOwnerDefaultMdm = "MdmOwner99";

        /// <summary>
        /// </summary>
        public String MstrDbTable = @"MDMPC13\SQLEXPRESS..sysobjects";
        /// <summary>
        /// </summary>
        public String MstrDbTableMdm = @"MDMPC13\SQLEXPRESS....sysobjects";
        /// <summary>
        /// </summary>
        public String MstrDbTableDefault = @"MDMPC13\SQLEXPRESS....sysobjects";
        /// <summary>
        /// </summary>
        public String MstrDbTableDefaultMdm = @"MDMPC13\SQLEXPRESS....sysobjects";

        /// <summary>
        /// </summary>
        public String MstrDbFile = @"MDMPC13\SQLEXPRESS..sysobjects";
        /// <summary>
        /// </summary>
        public String MstrDbFileMdm = @"MDMPC13\SQLEXPRESS....sysobjects";
        /// <summary>
        /// </summary>
        public String MstrDbFileDefault = @"MDMPC13\SQLEXPRESS....sysobjects";
        /// <summary>
        /// </summary>
        public String MstrDbFileDefaultMdm = @"MDMPC13\SQLEXPRESS....sysobjects";

        // <Area Id = "MasterConnectionCommand">

        // <Area Id = "MasterDatabase - Connection">
        // public String MstrConnString;
        /// <summary>
        /// </summary>
        public String MstrConnString = Mdm.Oss.File.Db.DbIdDef.SqlConnectionString;

        // <Area Id = "MasterDatabaseStatus">
        /// <summary>
        /// </summary>
        public bool MstrDbDatabaseIsInitialized = false;
        /// <summary>
        /// </summary>
        public bool MstrDbDatabaseDoesExist = false;
        /// <summary>
        /// </summary>
        public bool MstrDbDatabaseIsInvalid = false;
        /// <summary>
        /// </summary>
        public bool MstrDbDatabaseIsCreating = false;
        /// <summary>
        /// </summary>
        public bool MstrDbDatabaseIsCreated = false;

        // <Area Id = "EndOfMasterServerAndDatabase">

        #endregion
        #region $include Mdm.Oss.File mFile FileMaster_FileGroup
        // <Area Id = "FileGroup">

        /// <summary>
        /// </summary>
        public String spMstrDbFileGroupServerId = sUnknown;
        /// <summary>
        /// </summary>
        public String spMstrDbFileGroupDbId = sUnknown;
        /// <summary>
        /// </summary>
        public String spMstrDbFileGroupId = sUnknown;
        /// <summary>
        /// </summary>
        public String spMstrDbFileGroup = "MdmFileGroup99";
        /// <summary>
        /// </summary>
        public String spMstrDbFileGroupDefault = "MdmFileGroup99";
        // <Area Id = "FileGroupCommand">
        /// <summary>
        /// </summary>
        public String spMDbFileGroupCreateCmd = "not used";
        #endregion
        #region $include Mdm.Oss.File mFile FileMaster_DbFile

        // <Area Id = "MasterFile">
        /// <summary>
        /// </summary>
        public String spMstrDbFileDbId = sUnknown;
        /// <summary>
        /// </summary>
        public String spMstrDbFileDb = "MdmDatabase99";
        /// <summary>
        /// </summary>
        public String spMstrDbFileDbDefault = "MdmDatabase99";

        /// <summary>
        /// </summary>
        public String spMstrDbFileId = sUnknown;
        /// <summary>
        /// </summary>
        // public String spMstrDbFile = "MdmFile99";
        /// <summary>
        /// </summary>
        public String spMstrDbFile = "INFORMATION_SCHEMA.TABLES";
        //
        //
        // public String spMstrDbFile = "sys.objects";

        // FmainPassed.DbIo.CommandCurrent = "USE[" + Fs.DatabaseName + "]; SELECT * FROM 
        // INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FileId.spFileNameFull + "';";
        // FmainPassed.DbIo.CommandCurrent = "USE[" + Fs.DatabaseName + "]; SELECT * FROM 
        // sys.objects WHERE name = " + "'" + FileId.spFileNameFull + "';";
        // SQL = "SELECT * FROM 
        // INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=MyTable";

        // <Area Id = "MstrDbFileStatus">
        /// <summary>
        /// </summary>
        public bool bpMstrDbFileDoesExist = false;
        /// <summary>
        /// </summary>
        public bool bpMstrDbFileIsInvalid = false;
        /// <summary>
        /// </summary>
        public bool bpMstrDbFileIsCreating = false;
        /// <summary>
        /// </summary>
        public bool bpMstrDbFileIsCreated = false;

        #endregion
        // OBJECT SUBCLASS - MASTER FILE CONNECTION
        // Database Master File Connection - xxxx
        #region $include Mdm.Oss.File mFile FileMaster_DbFileConnection
        // <Area Id = "MstrDbFileConnectionCreationCommand">
        /// <summary>
        /// </summary>
        public String MstrDbConnCreateCmd;

        // <Area Id = "MstrDbFileConnStatus">
        /// <summary>
        /// </summary>
        public int ipMstrDbFileConnStatus = -iUnknown;
        /// <summary>
        /// </summary>
        public int iMstrDbFileConnStatus
        {
            get { return ipMstrDbFileConnStatus; }
            set { ipMstrDbFileConnStatus = value; }
        }
        #endregion
        #endregion

        /// <summary>
        /// </summary>
        public DbMasterDef()
        {
        }
    }
    #endregion
}
