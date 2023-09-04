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
using Mdm.Oss.Std;
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

namespace Mdm.Oss.File.Db.Table
{
    #region Phrase construction - DbMasterSynDef, DbSynDef
    /// <summary>
    /// <para> Master File Syntax Object</para>
    /// <para> Used for building console commands
    /// for the master database.</para>
    /// <para>Commands include Create, Delete.</para>
    /// </summary>
    public class DbMasterSynDef
    {
        #region Fields
        // OBJECT SUBCLASS - COMMAND PHRASES
        // <Area Id = "MasterDatabase - Creation">
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFileCreateCmd;
        /// <summary>
        /// </summary>
        protected internal String MstrDbDatabaseCreateCmd;

        #region $include Mdm.Oss.File mFile FileDatabasePhrase_Constrution
        // <Area Id = "Phrases">
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseDoLine = "; ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseExecute = "GO"; // wrong
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseDoExecute = "GO"; // wrong

        #region $include Mdm.Oss.File mFile FileMasterServerAndDatabasePhrases
        // <Area Id = "MasterServerAndDatabasePhrases">

        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseServer;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseDatabase;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseSecurity;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseUser;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseUserPw;


        // <Area Id = "CreationPhrases">

        // <Area Id = "MasterDatabase - CreationPhrases">

        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhrase;

        /// <summary>
        /// </summary>
        public bool bpDbPhraseIfIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseIf = "IF EXISTS (";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseIfEnd = ") ";

        /// <summary>
        /// </summary>
        public bool bpDbPhraseSelectIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseSelect = "SELECT * ";

        /// <summary>
        /// </summary>
        public bool bpDbPhraseFromIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseFrom = "FROM ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseFromItems = "MdmServer99..sysdatabases";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseFromEnd = " ";

        /// <summary>
        /// </summary>
        public bool bpDbPhraseWhereIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseWhere = "WHERE ";
        // <Area Id = "sb paired list of dict + value
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseWhereItems = "Name = 'HowToDemo'";

        /// <summary>
        /// </summary>
        public bool bpDbPhraseDropIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseDrop = "DROP ";
        // <Area Id = "sb paired list of dict + value
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseDropItems = "DATABASE HowToDemo";

        /// <summary>
        /// </summary>
        public bool bpDbPhraseCreateIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseCreate = "CREATE ";
        // <Area Id = "sb paired list of dict + value
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseCreateItems = "CREATE DATABASE HowToDemo";

        /// <summary>
        /// </summary>
        protected internal bool spMstrDbPhraseFileGroupIsUsed = false;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseFileGroup = "HowToDemoFileGroup";
        #endregion
        #region $include Mdm.Oss.File mFile FileCreationPhrases
        // <Area Id = "FileCreationPhrases">

        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhrase;

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseUseIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseUse = "USE ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseUseEnd;

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseIfIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseIf = "IF EXISTS (";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseIfEnd = ")";

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseSelectIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseSelect = "SELECT * ";

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseFromIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseFrom = "FROM ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseFromItems = "master..sysdatabases";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseFromEnd = " ";

        // property bool DbFilePhraseWhereIsUsed
        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseWhereIsUsed = false;
        /// <summary>
        /// </summary>
        public bool DbFilePhraseWhereIsUsed
        {
            get { return bpDbFilePhraseWhereIsUsed; }
            set { bpDbFilePhraseWhereIsUsed = value; }
        } //  this is a sample code snippet

        // Master Phrases

        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseWhere = "WHERE ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseWhereAnd = " AND ";
        // <Area Id = "sb paired list of dict + value">
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseWhereItems1 = "Name = 'HowToDemo'";
        // <Area Id = "sb paired list of dict + value">
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseWhereItems2 = "TYPE = 'u'";
        // spMstrDbFilePhraseWhereItemsId[X];
        // spMstrDbFilePhraseWhereItemsExpression[X];
        // spMstrDbFilePhraseWhereItemsValue[X];

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseBeginIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseBegin = "BEGIN";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseBeginEnd = "END";

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseDropIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseDrop = "DROP ";
        // <Area Id = "sb paired list of dict + value">
        // protected internal String spMstrDbFilePhraseDropItems = "TABLE " + "HowToDemo.dbo.MdmFile99";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseDropItems = "TABLE " + "HowToDemo..MdmFile99";


        // <Section Id = "x">

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseCreateIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseCreate = "CREATE ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseCreateObject = "TABLE ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseCreateTable = "MdmFile99";
        // + "HowToDemo.dbo.This";Table

        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseItemsBegin = "(";
        /// <summary>
        /// </summary>
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
        // "NOT NULL sEmpty>


        // <Section Id = "x">

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseConstraintIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraint = "CONSTRAINT [";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintCol = "PK_Numeric99";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintNameDefault = "PK_Numeric99";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintEnd = "]";

        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintType1 = " PRIMARY KEY ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintType2 = " CLUSTERED ";

        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintColBegin = " (";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintColName = "Column0";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintColNameDefalut = "Column0";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintColEnd = ")";


        /// <summary>
        /// </summary>
        public String spMstrDbFilePhraseCreateItems = "CREATE DATABASE HowToDemo";
        #endregion
        #endregion
        #endregion
        /// <summary>
        /// </summary>
        public DbMasterSynDef()
        {
        }
        /// <summary>
        /// </summary>
        public void DataClear()
        {
            bpDbFilePhraseSelectIsUsed = false;
        }
    }
    /// <summary>
    /// <para> Database File Syntax Object</para>
    /// <para> Used for building console commands
    /// for the database.</para>
    /// <para>Command include Create, Add, Alter, Insert, Update, Delete.</para>
    /// </summary>
    public class DbSynDef
    {
        // SYNTAX - xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.File mFile SqlSyntax
        // property String DatabaseFileCreateCmd
        /// <summary>
        /// </summary>
        protected internal DbCommandIs ipDatabaseCmdType;
        /// <summary>
        /// </summary>
        public DbCommandIs DatabaseCmdType
        {
            get { return ipDatabaseCmdType; }
            set { ipDatabaseCmdType = value; }
        } //   

        // property String DatabaseFileCreateCmd
        /// <summary>
        /// </summary>
        protected internal String spDatabaseFileCreateCmd;
        /// <summary>
        /// </summary>
        public String DatabaseFileCreateCmd
        {
            get { return spDatabaseFileCreateCmd; }
            set { spDatabaseFileCreateCmd = value; }
        } //   

        // property String ConnCreateCmd
        /// <summary>
        /// </summary>
        protected internal String spConnCreateCmd;
        /// <summary>
        /// </summary>
        public String sConnCreateCmd
        {
            get { return spConnCreateCmd; }
            set
            {
                spConnCreateCmd = value;
            }
        } //   

        // property String OutputCommand
        /// <summary>
        /// </summary>
        public String spOutputCommand;
        /// <summary>
        /// </summary>
        public String OutputCommand
        {
            get { return spOutputCommand; }
            set { spOutputCommand = value; }
        } //   
        #region $include Mdm.Oss.File mFile Sql File Create Commands
        // Sql File Command
        /// <summary>
        /// </summary>
        public String spSqlCreateCmdScript;
        /// <summary>
        /// </summary>
        public String spSqlCreateCmd;
        /// <summary>
        /// </summary>
        public String spSqlFileDeleteCmd;
        /// <summary>
        /// </summary>
        public String spSqlFileAlterCmd;
        /// <summary>
        /// </summary>
        public String spSqlFileViewCmd;
        #endregion
        #region $include Mdm.Oss.File mFile Sql File Column Commands
        // Sql File Column Commands
        /// <summary>
        /// </summary>
        public String spSqlColumnAddCmdScript;
        /// <summary>
        /// </summary>
        public String spSqlColumnAddCmd;

        /// <summary>
        /// </summary>
        public String spSqlColumnDeleteCmd;

        /// <summary>
        /// </summary>
        public String spSqlColumnAlterCmd;
        // spOutputViewCommand
        /// <summary>
        /// </summary>
        protected internal String spSqlColumnViewScript = "SCRIPT to create a view of a TABLE ";
        /// <summary>
        /// </summary>
        protected internal bool bpSqlColumnViewCmdFirst = true;
        /// <summary>
        /// </summary>
        public String spSqlColumnViewCmdPrefix;
        /// <summary>
        /// </summary>
        public String spSqlColumnViewCmd;
        /// <summary>
        /// </summary>
        public String spSqlColumnViewCmdSuffix;

        // property String OutputReadCommand
        /// <summary>
        /// </summary>
        public String spOutputReadCommand;
        /// <summary>
        /// </summary>
        public String spOutputWriteCommand;
        /// <summary>
        /// </summary>
        public String OutputReadCommand
        {
            get { return spOutputReadCommand; }
            set { spOutputReadCommand = value; }
        }
        //   
        // property String OutputInsertCommand
        /// <summary>
        /// </summary>
        public String spOutputInsertCommand;
        /// <summary>
        /// </summary>
        public String OutputInsertCommand
        {
            get { return spOutputInsertCommand; }
            set { spOutputInsertCommand = value; }
        } //   
        #endregion
        #region $include Mdm.Oss.File mFile Sql Command Output
        // Sql Command Output
        /// <summary>
        /// </summary>
        protected internal String spOutputInsertPrefix = "INSERT INTO ";
        /// <summary>
        /// </summary>
        protected internal String spOutputInsertPrefix1 = " (";

        // property String OutputInsert
        /// <summary>
        /// </summary>
        public String spOutputInsert;
        /// <summary>
        /// </summary>
        public String OutputInsert
        {
            get { return spOutputInsert; }
            set { spOutputInsert = value; }
        } //   

        /// <summary>
        /// </summary>
        protected internal String spOutputInsertScript = "SCRIPT TO Insert into TABLE ";
        /// <summary>
        /// </summary>
        protected internal bool bpOutputInsertScriptFirst = true;
        /// <summary>
        /// </summary>
        protected internal String spOutputInsertSuffix = ")";
        //
        /// <summary>
        /// </summary>
        protected internal String spOutputUpdateScript = "UPDATE";
        /// <summary>
        /// </summary>
        protected internal bool bpOutputUpdateScriptFirst = true;
        /// <summary>
        /// </summary>
        protected internal String spOutputUpdatePrefix = "UPDATE";
        //
        /// <summary>
        /// </summary>
        protected internal String spOutputDeleteScript = "DELETE";
        /// <summary>
        /// </summary>
        protected internal bool bpOutputDeleteScriptFirst = true;
        /// <summary>
        /// </summary>
        protected internal String spOutputDeletePrefix = "DELETE";
        //
        /// <summary>
        /// </summary>
        protected internal String spOutputValuesScript = "VALUES (";
        /// <summary>
        /// </summary>
        protected internal bool bpOutputValuesScriptFirst = true;
        /// <summary>
        /// </summary>
        protected internal String spOutputValuesPrefix = "VALUES (";

        // property String OutputValues
        /// <summary>
        /// </summary>
        public String spOutputValues;
        /// <summary>
        /// </summary>
        public String OutputValues
        {
            get { return spOutputValues; }
            set
            {
                spOutputValues = value;
                OutputValuesSet = true;
            }
        } //   

        /// <summary>
        /// </summary>
        public bool OutputValuesSet = false;

        /// <summary>
        /// </summary>
        protected internal String spOutputValuesSuffix = ")";
        //
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterScript = "SCRIPT TO ALTER TABLE ";
        /// <summary>
        /// </summary>
        protected internal bool bpOutputAlterScriptFirst = true;
        //
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterCommand = "ALTER TABLE ";
        //
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterPrefix = "ALTER TABLE ";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterVerb = "ADD";
        /// <summary>
        /// </summary>
        /// <summary>
        /// </summary>
        protected internal bool bpOutputAlterColumnNameQuoted = false;
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterQuoteChar = "'";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterQuoteCharLeft = "[";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterQuoteCharRight = "]";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnNameSource = "1";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnNameAlias = "1";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnName = "1";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnType = "VARCHAR";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnTypePrefix = "(";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnLength = "50";
        /// <summary>
        /// </summary>
        protected internal int ipOutputAlterColumnLength = 50;
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnTypeSuffix = ")";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnNull = " NULL";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterListSeparatorChar = ",";
        #endregion
        #region $include Mdm.Oss.File mFile Create Database and Database Objects
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
        /// <summary>
        /// </summary>
        public DbSynDef()
        {
        }
    }
    #endregion
}
