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
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
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
using Mdm.Oss.File.RunControl;
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
//using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Pick;
using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)

namespace Mdm.Oss.File.Type.Sql
{
    public partial class mFileSql : mFileDef, ImFileType, AppStd, IDisposable
    {
        #region Core Fields
        /// <summary>
        /// 1) The connection is set in mFileSqlConnectionDef.
        /// when Fmain and Faux are created.
        /// 2.1) Weirdly, The connection itself is a full
        /// blown File object by inheriting mFileSql.
        /// 2.2) This creates a circle loop and therefore
        /// the code avoids creating already existing object.
        /// 2.3) It would seem a memberwise copy would be the
        /// way to go.
        /// 2.4) This made worse by the fact that an mFileSql
        /// has two of this objects. The primary (actual)
        /// connections Fmain and the secondary Faux.
        /// 3) Faux is intended for loading SQL meta data,
        /// paramaters from disk files and possibly separate
        /// Selects of file data concurrently.
        /// 4) Optimization like lazy creation of Faux would
        /// help.
        /// 5) Recommended usage of the library is to simply
        /// create a mFileSqlConnectionDef.
        /// </summary>
        //public mFileSqlConnectionDef FileSqlConn;
        // SqlClient - Exceptions 
        #region $include Mdm.Oss.FileUtil mFile SqlExceptions
        #region $include Mdm.Oss.FileUtil mFile Exception - Database - Sql
        // System ItemData SqlClient
        public SqlException ExceptSql; // General Database
        public SqlException ExceptDbFileSql; // Sql Database
        #endregion
        // Sql Error Types - DataException - xxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil mFile DataException
        // System ItemData
        public DBConcurrencyException x11;
        // System ItemData
        public DataException x11b;
        // System ItemData
        public ConstraintException x13;
        #endregion
        // SqlClient - SqlException - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil mFile DataException Additional Copy
        // System ItemData SqlClient - SQL EXCEPTION
        public SqlException x7;
        #endregion
        #region $include Mdm.Oss.FileUtil mFile NotSupportedException
        public NotSupportedException ExceptNotSupported; // Delegate
                                                         // public NotSupportedExceptionController oeMexceptNotSupportedExceptionController; // Delegate
                                                         // public NotSupportedExceptionArgs oeMexceptNotSupportedExceptionArgs; // Delegate
        #endregion
        // SqlClient - SqlErrorCollection - xxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil mFile SqlError
        // System ItemData SqlClient - SqlError
        public SqlErrorCollection x8C;
        public SqlError x8;
        #endregion
        #endregion
        // SqlClient - Other Objects - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil mFile SqlObjectTypes
        #region $include Mdm.Oss.FileUtil mFile SqlRowUpdatingEventHandler
        public SqlRowUpdatingEventHandler x1;
        public SqlRowUpdatingEventArgs x1A;
        public SqlRowUpdatedEventHandler x2;
        public SqlRowUpdatedEventArgs x2A;
        public SqlRowsCopiedEventHandler x3;
        public SqlRowsCopiedEventArgs x3A;
        #endregion
        // ToDo z$RelVs4 Devrive SQL (My, Ms,...), ASCII, PICK, DB2, OS2 classes from base file class (Pick Text done)
        // SqlClient - SqlParameter - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil mFile SqlParameter
        // System ItemData SqlClient - PARAMETER
        public SqlParameterCollection x4C;
        public SqlParameter x4;
        #endregion
        // SqlClient - SqlNotificationType - xxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil mFile SqlNotification
        // System ItemData SqlClient - NOTIFICATION
        public SqlNotificationType x5t;
        public SqlNotificationSource x5s;
        public SqlNotificationInfo x5i;
        public SqlNotificationEventArgs x5e;
        #endregion
        // SqlClient - SqlParameter - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil mFile SqlInfoMessage
        // System ItemData SqlClient - INFORMATION MESSAGE
        public SqlInfoMessageEventHandler x6; // Delegate
        public SqlInfoMessageEventArgs x6A; // Delegate Arguments
        #endregion
        // SqlClient - SqlDbType - xxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil mFile SqlDbType
        // System ItemData - SqlDbType
        public SqlDbType x9; // Sql ItemData Type
        #endregion
        // SqlClient - StateChangeEvent - xxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil mFile SqlDataState ChangeEvents
        // System ItemData SqlClient - StateChangeEvent
        public StateChangeEventHandler SqlDbConnectStateChangedEvent;  // Delegate
        public StateChangeEventArgs SqlDbConnectStateChangedEventArgs;  // Delegate Arguments
        #endregion
        // SqlClient - StatementCompleted - xxxxxxxxxxxxxxx
        #region $include Mdm.Oss.FileUtil mFile StatementCompleted
        // System ItemData SqlClient - StatementCompleted
        public StatementCompletedEventHandler SqlDbCommandCompletedEvent;  // Delegate
        public StatementCompletedEventArgs SqlDbCommandCompletedEventArgs;  // Delegate Arguments
        #endregion
        #endregion
        #endregion
        #region Framework Objects empty
        #endregion
        #region Class Factory empty
        #endregion
        #region Class
        #region Class Fields empty - DbSyn
        //#region Phrase Construction
        //DbSyn = new DbSynDef();
        //DbMasterSyn = new DbMasterSynDef();
        //#endregion

        #endregion
        #region Class Initialization
        public mFileSql() 
             : base(ConsoleSourceIs.None, ClassRoleIs.RoleAsUtility, (ClassFeatureIs.MdmUtilTrace & ClassFeatureIs.MdmRunControl)) { 
            // : base() {
            Sender = this;
            if (this is mFileSqlConnectionDef)
            {
                FileSqlConn = (mFileSqlConnectionDef)this;
            }
            if (this is mFileSqlConnectionDef)
            {
                // FileObject is mFile
                Fmain.FileObjectSql = this;
            }
            this.Initialize(); // or this:
            // InitializeMetaData();
            FileState.mFileResult = SqlReset(ref Fmain);
        }
        public mFileSql(ref object SenderPassed, ref StdConsoleManagerDef stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ref SenderPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        {
            //st = base.st as StdConsoleManagerDef;
            FileState.mFileResult = StateIs.Started;
            this.Initialize(); // or this:
        }
        public mFileSql(ref object SenderPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ref SenderPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        {
            FileState.mFileResult = StateIs.Started;
            this.Initialize(); // or this:
        }
        public mFileSql(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        {
            FileState.mFileResult = StateIs.Started;
            this.Initialize(); // or this:
        }
        public StateIs SqlReset(ref mFileMainDef FmainPassed)
        {
            FileState.SqlResetResult = StateIs.Started;
            // Fmain.FileStatus.bpIsInitialized
            // if (Fmain.FileStatus.bpIsInitialized) {
            // TableOpenResult = SqlReset(spTableName, spTableNameLine);
            // THIS IS A DISPOSE FUNCTION
            Fmain.FileStatus.bpIsInitialized = false;

            // }
            return FileState.SqlResetResult;
        }
        #endregion
        #region Class Methods
        // Sql Command Execution - xxxxxxxxxxxxxxxxxxxxxxx
        #region File Sql Command Do -  Database File Handling
        public StateIs SqlCommandDoReaderResult;
        /// <summary> 
        /// Do the SQL Reader Command.
        /// </summary> 
        /// <param name="CommandPassed">The SQL Command to be executed.</param> 
        public StateIs SqlCommandDoReader(ref mFileMainDef FmainPassed, ref String CommandPassed)
        {
            SqlCommandDoReaderResult = StateIs.Started;
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
            // FileMainHeaderGetTo(ref FmainPassed);
           st.TraceMdmDoDetailed(
                ConsoleFormUses.DatabaseLog, 8, ref Sender, bIsMessage,
                TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(),
                FileDoResult, false,
                iNoErrorLevel, iNoErrorSource,
                bDoNotDisplay, MessageNoUserEntry,
                LocalMessage.LogEntry);

            //  check Connection
            SqlCommandDoReaderResult = FileSqlConn.ConnCheck(ref FmainPassed);
            if ((SqlCommandDoReaderResult & StateIs.MaskSuccessfulAll) > 0)
            {
                SqlCommandDoReaderResult = StateIs.InProgress;
                //
                if (FmainPassed.DbIo.SqlDbCommand != null)
                {
                    FmainPassed.DbIo.SqlDbCommand.Dispose();
                    FmainPassed.DbIo.SqlDbCommand = null;
                }
                FmainPassed.DbIo.SqlDbCommand =
                    new SqlCommand(CommandPassed, FmainPassed.DbIo.SqlDbConn);
                FmainPassed.DbIo.SqlDbCommand.CommandTimeout = FmainPassed.DbIo.SqlDbCommandTimeout;
                FmainPassed.DbIo.SqlDbCommand.CommandType = CommandType.Text;
                if (FmainPassed.DbIo.SqlDbDataReader != null)
                {
                    //    FmainPassed.DbIo.SqlDbDataReader.Dispose();
                    //    FmainPassed.DbIo.SqlDbDataReader = null;
                    if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed)
                    {
                        FmainPassed.DbIo.SqlDbDataReader.Close();
                    }
                }
                if (FmainPassed.DbIo.SqlDbDataWriter != null)
                {
                    //    FmainPassed.DbIo.SqlDbDataWriter.Dispose();
                    //    FmainPassed.DbIo.SqlDbDataWriter = null;
                }
                switch (FmainPassed.RowInfo.UseMethod)
                {
                    case (FileIo_SqlCommandModeIs.UseExecuteNoQuery):
                        // Not appropriate for Check Does Exist
                        // no row or columns
                        // used for create, settings, etc
                        try
                        {
                            FmainPassed.RowInfoDb.RowCount = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                            // MessageMdmSendToPageNewLine(Sender, FmainPassed.DbIo.SqlDbDataReader.sGetString(0));
                        }
                        finally
                        {
                        }
                        break;
                    case (FileIo_SqlCommandModeIs.Default):
                    case (FileIo_SqlCommandModeIs.SingleRow):
                    case (FileIo_SqlCommandModeIs.SingleResult):
                    case (FileIo_SqlCommandModeIs.KeyInfo):
                    case (FileIo_SqlCommandModeIs.SchemaOnly):
                    case (FileIo_SqlCommandModeIs.UseExecuteScalar):
                        #region Execute Scalar Command
                        // FmainPassed.DbIo.SqlDbDataReader.
                        bool ExecuteScalarFailed = false;
                        try
                        {
                            switch (FmainPassed.RowInfo.UseMethod)
                            {
                                case (FileIo_SqlCommandModeIs.UseExecuteScalar):
                                    // Scalar = 1 Row 1 Column, no reader
                                    try
                                    {
                                        DbFileTemp.ooThisTempObject = FmainPassed.DbIo.SqlDbCommand.ExecuteScalar();
                                        // MessageMdmSendToPageNewLine(Sender, FmainPassed.DbIo.SqlDbDataReader.sGetString(0));
                                    }
                                    finally
                                    {
                                    }
                                    break;
                                case (FileIo_SqlCommandModeIs.Default):
                                    // All Rows All Columns
                                    FmainPassed.DbIo.SqlDbDataReader =
                                        FmainPassed.DbIo.SqlDbCommand.ExecuteReader((CommandBehavior)FmainPassed.RowInfo.UseMethod);
                                    break;
                                case (FileIo_SqlCommandModeIs.SingleRow):
                                // Single Row
                                case (FileIo_SqlCommandModeIs.KeyInfo):
                                // Column and Primary Key info
                                case (FileIo_SqlCommandModeIs.SchemaOnly):
                                // Column info
                                case (FileIo_SqlCommandModeIs.SingleResult):
                                    // Single Result Set
                                    FmainPassed.DbIo.SqlDbDataReader =
                                        FmainPassed.DbIo.SqlDbCommand.ExecuteReader((CommandBehavior)FmainPassed.RowInfo.UseMethod);
                                    // FmainPassed.RowInfo.CloseIsNeeded = true;
                                    break;
                                default:
                                    SqlCommandDoReaderResult = StateIs.ProgramInvalid;
                                    FmainPassed.FileStatus.Status = StateIs.ProgramInvalid;
                                    // FileIo_CommandModeIs.UseExecuteScalar
                                    LocalMessage.ErrorMsg = "The Sql Command Behavior (Mehtod of reading the file) has not been set)";
                                    LocalMessage.ErrorMsg += "Behavior FmainPassed.RowInfo.UseMethod: " + FmainPassed.RowInfo.UseMethod.ToString();
                                    //ExceptNotSupportedImpl(ref FmainPassed, ref ExceptionNotSupported, LocalMessage.ErrorMsg, FileWriteResult);
                                    throw new NotSupportedException(LocalMessage.ErrorMsg);
                                    break;
                            }
                            #region Catch Error on probative read
                        }
                        catch (SqlException ExceptionSql)
                        {
                            LocalMessage.ErrorMsg = "SQL Read Access Method is invalid (" + FmainPassed.RowInfo.UseMethod.ToString() + ")!";
                            ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, SqlCommandDoReaderResult);
                            SqlCommandDoReaderResult = StateIs.DatabaseError;
                            FmainPassed.FileStatus.bpDoesExist = false;
                            FmainPassed.RowInfoDb.RowCount = 0;
                            FmainPassed.FileStatus.bpIsOpen = false;
                            ExecuteScalarFailed = true;
                        }
                        catch (NotSupportedException ExceptionNotSupported)
                        {
                            LocalMessage.ErrorMsg = "Not Supported Exception occured in Sql Commad Do(#313)";
                            ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileDoResult);
                            FileDoResult = StateIs.Failed;
                            FmainPassed.FileStatus.Status = StateIs.ProgramInvalid;
                            FmainPassed.FileStatus.bpIsOpen = false;
                            ExecuteScalarFailed = true;
                        }
                        catch (Exception ExceptionGeneral)
                        {
                            LocalMessage.ErrorMsg = "General Exception occured in Sql Commad Do(#314)";
                            ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlCommandDoReaderResult);
                            SqlCommandDoReaderResult = StateIs.UnknownFailure;
                            FmainPassed.FileStatus.bpDoesExist = false;
                            FmainPassed.RowInfoDb.RowCount = 0;
                            FmainPassed.FileStatus.bpIsOpen = false;
                            ExecuteScalarFailed = true;
                        }
                        finally
                        {
                            // FmainPassed.DbIo.SqlDbCommand = null;
                            // SqlCommandDoReaderResult = Faux.RowInfoDb.RowCount;
                        }
                        // If File is NOT Open Try to Select the File Name in the Master File
                        #endregion
                        #endregion
                        #region Reader Object Get Type
                        // FmainPassed.DbIo.SqlDbCommand.Container.Components.Count;
                        if (!ExecuteScalarFailed)
                        {
                            try
                            {
                                FmainPassed.RowInfo.RowIndex = 0;
                                FmainPassed.RowInfoDb.RowCount = -1;
                                FmainPassed.RowInfo.bpHasRows = FmainPassed.DbIo.SqlDbDataReader.HasRows;
                                FmainPassed.ColTrans.ColCount = FmainPassed.DbIo.SqlDbDataReader.FieldCount;
                                // only applies to db changes:
                                FmainPassed.RowInfoDb.RowCount = FmainPassed.DbIo.SqlDbDataReader.RecordsAffected;
                                //
                                if (FmainPassed.RowInfo.bpHasRows)
                                {
                                    SqlCommandDoReaderResult = StateIs.DoesExist;
                                    FmainPassed.FileStatus.bpDoesExist = true;
                                    // XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                                    // Data is present
                                }
                                else
                                {
                                    SqlCommandDoReaderResult = StateIs.EmptyResult;
                                }
                                #region Catch errors on Reader Object Get Type
                            }
                            catch (SqlException ExceptionSql)
                            {
                                LocalMessage.ErrorMsg = "SQL Exception(#320) in Sql Commad Do";
                                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, SqlCommandDoReaderResult);
                                SqlCommandDoReaderResult = StateIs.DatabaseError;
                            }
                            catch (NotSupportedException ExceptionNotSupported)
                            {
                                LocalMessage.ErrorMsg = "Not Supported Exception(#315) occured in Sql Commad Do";
                                ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileDoResult);
                                FileDoResult = StateIs.Failed;
                                FmainPassed.FileStatus.Status = StateIs.ProgramInvalid;
                                SqlCommandDoReaderResult = StateIs.ProgramInvalid;
                            }
                            catch (Exception ExceptionGeneral)
                            {
                                LocalMessage.ErrorMsg = "General Exception(#315) occured in Sql Commad Do";
                                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, SqlCommandDoReaderResult);
                                SqlCommandDoReaderResult = StateIs.OsError;
                            }
                            finally
                            {
                                if (SqlCommandDoReaderResult == StateIs.InProgress)
                                {
                                    SqlCommandDoReaderResult = StateIs.DatabaseError;
                                }
                                FmainPassed.DbIo.SqlDbCommand.ResetCommandTimeout();
                                if (((long)FmainPassed.RowInfo.UseMethod
                                    & (long)((long)FileIo_SqlCommandModeIs.SingleResult
                                    | (long)FileIo_SqlCommandModeIs.SchemaOnly
                                    | (long)FileIo_SqlCommandModeIs.KeyInfo
                                    | (long)FileIo_SqlCommandModeIs.SingleRow
                                    | (long)FileIo_SqlCommandModeIs.SequentialAccess)) != 0)
                                {
                                    if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed)
                                    {
                                        if (!FmainPassed.FileStatus.bpDoKeepOpen)
                                        {
                                            FmainPassed.DbIo.SqlDbDataReader.Close();
                                            if (FmainPassed.FileStatus.bpDoDispose)
                                            {
                                                FmainPassed.DbIo.SqlDbDataReader.Dispose();
                                            }
                                        }
                                    }
                                }
                            } // Execute Command OK Try to access Reader ItemData
                        }
                        #endregion
                        break;
                    #endregion
                    // case (99):
                    default:
                        // Command Error
                        SqlCommandDoReaderResult = StateIs.ProgramInvalid;
                        FmainPassed.FileStatus.Status = StateIs.ProgramInvalid;
                        FmainPassed.RowInfoDb.RowCount = 0;
                        FmainPassed.FileStatus.bpDoesExist = false;
                        FmainPassed.FileStatus.bpIsOpen = false;
                        LocalMessage.ErrorMsg = "The Sql Command Behavior (Mehtod of reading the file) has not been set)";
                        // throw new NotSupportedException(LocalMessage.ErrorMsg);
                        ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileDoResult);
                        break;
                } // Execute Correct Command for Reading Method FileUseMethod
                // ToDo SqlCommandDoReader $ Log Read to Console
            }
            else
            {
                // Connection DoesNotExist
            }
            return SqlCommandDoReaderResult;
        }
        // <Section Id = "SqlCommandDo">
        /// <summary> 
        /// Do the SQL Command.
        /// </summary> 
        /// <param name="CommandPassed">The SQL Command to be executed.</param> 
        public StateIs SqlCommandDo(ref mFileMainDef FmainPassed, String CommandPassed)
        {
            FileState.SqlCommandDoResult = StateIs.Started;
            FmainPassed.DbIo.CommandCurrent = CommandPassed;
            // command
            // command
            FmainPassed.DbIo.SqlDbCommand = new SqlCommand(CommandPassed, FmainPassed.DbIo.SqlDbConn);
            FileState.SqlCommandDoResult = StateIs.InProgress;
            try
            {
                FmainPassed.RowInfoDb.RowCount = 0;
                FmainPassed.RowInfoDb.RowCount = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                if (FmainPassed.RowInfoDb.RowCount > 0) {
                    FileState.SqlCommandDoResult = StateIs.Successful;
                } else { FileState.SqlCommandDoResult = StateIs.Failed; }
            }
            catch (SqlException ExceptionSql)
            {
                LocalMessage.ErrorMsg = "Not Supported Exception(#319) occured in Sql Commad Do";
                FileState.SqlCommandDoResult = StateIs.DatabaseError;
                LocalMessage.ErrorMsg = sEmpty;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.SqlCommandDoResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            catch (Exception ExceptionGeneral)
            {
                LocalMessage.ErrorMsg = "General Exception(#319) occured in Sql Commad Do";
                FileState.SqlCommandDoResult = StateIs.Failed;
                LocalMessage.ErrorMsg = sEmpty;
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.SqlCommandDoResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            finally
            {
                FmainPassed.DbIo.SqlDbCommand = null;
            }
            return FileState.SqlCommandDoResult;
        }

        /// <summary> 
        /// Set the default SQL Command for this Table.
        /// </summary> 
        public StateIs SqlCommandSetDefault(ref mFileMainDef FmainPassed)
        {
            FileState.SqlCommandSetDefaultResult = StateIs.Started;
            FmainPassed.DbIo.CommandCurrent = "SELECT * FROM " + "'" + FmainPassed.Fs.TableName + "'";
            if (FmainPassed.Item.ItemId.Length > 0)
            {
                FmainPassed.DbIo.CommandCurrent += "WHERE [name] = " + "'" + FmainPassed.Item.ItemId + "'";
            }
            // command

            return FileState.SqlCommandSetDefaultResult;
        }
        #endregion
        #region File Sql Reader / Writer Close
        public StateIs SqlReaderCloseResult;
        /// <summary> 
        /// Close the SQL Reader.
        /// </summary> 
        public StateIs SqlReaderClose(ref mFileMainDef FmainPassed)
        {
            SqlReaderCloseResult = StateIs.Started;
            if (FmainPassed.DbIo.SqlDbDataReader != null)
            {
                if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed)
                {
                    if (!FmainPassed.FileStatus.bpDoKeepOpen)
                    {
                        FmainPassed.DbIo.SqlDbDataReader.Close();
                        if (FmainPassed.FileStatus.bpDoDispose)
                        {
                            FmainPassed.DbIo.SqlDbDataReader.Dispose();
                        }
                        FmainPassed.FileStatus.bpIsOpen = false;
                    }
                }
            }
            return (SqlReaderCloseResult = StateIs.Successful);
        }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx //
        public StateIs SqlWriterCloseResult;
        /// <summary> 
        /// CLose the SQL Writer.
        /// </summary> 
        public StateIs SqlWriterClose(ref mFileMainDef FmainPassed)
        {
            SqlWriterCloseResult = StateIs.Started;
            if (FmainPassed.DbIo.SqlDbDataWriter != null)
            {
                if (!FmainPassed.FileStatus.bpDoKeepOpen || FmainPassed.FileStatus.bpDoDispose)
                {
                    FmainPassed.DbIo.SqlDbDataWriter.Dispose();
                    FmainPassed.FileStatus.bpIsOpen = false;
                }
            }
            return (SqlWriterCloseResult = StateIs.Successful);
        }
        #endregion
        #region File Sql Data CRUD - Add, Insert, Update, Delete
        // <Section Id = "InsertValue">
        /// <summary> 
        /// Put an Insert command
        /// into the SQL Data command.
        /// </summary> 
        public StateIs SqlDataInsert(ref mFileMainDef FmainPassed)
        {
            FileState.SqlDataInsertResult = StateIs.Started;
            FmainPassed.DbIo.CommandCurrent = "INSERT " + "'" + FmainPassed.Fs.TableNameLine + "'";
            // command
            // FileSqlConn.DbSyn.spOutputItem += ColText;
            if (FmainPassed.FileSqlConn.DbSyn.spOutputInsert.Length > 0)
            {
                FmainPassed.FileSqlConn.DbSyn.spOutputInsertCommand = FmainPassed.FileSqlConn.DbSyn.spOutputInsertPrefix + FmainPassed.Fs.TableName + FmainPassed.FileSqlConn.DbSyn.spOutputInsertPrefix1;
                FmainPassed.FileSqlConn.DbSyn.spOutputInsertCommand += FmainPassed.FileSqlConn.DbSyn.spOutputInsert + FmainPassed.FileSqlConn.DbSyn.spOutputInsertSuffix;
            }
            //
            if (FmainPassed.FileSqlConn.DbSyn.spOutputValues.Length > 0)
            {
                FmainPassed.FileSqlConn.DbSyn.spOutputInsertCommand = FmainPassed.FileSqlConn.DbSyn.spOutputInsertPrefix + FmainPassed.Fs.TableName;
                FmainPassed.FileSqlConn.DbSyn.spOutputInsertCommand += FmainPassed.FileSqlConn.DbSyn.spOutputInsert + FmainPassed.FileSqlConn.DbSyn.spOutputInsertSuffix;
            }
            //
            FmainPassed.FileSqlConn.DbSyn.spOutputInsertCommand = FmainPassed.FileSqlConn.DbSyn.spOutputInsert + "\n" + FmainPassed.FileSqlConn.DbSyn.spOutputValues;
            FmainPassed.DbIo.CommandCurrent = FmainPassed.FileSqlConn.DbSyn.spOutputInsertCommand;
            LocalId.LongResult = SqlDataCommandCreate(ref FmainPassed);
            LocalId.LongResult = SqlDataWrite(ref FmainPassed);

            return FileState.SqlDataInsertResult;
        }
        // <Section Id = "UpdateValue">
        /// <summary> 
        /// Put an Update command
        /// into the SQL Data command.
        /// </summary> 
        public StateIs SqlDataUpdate(ref mFileMainDef FmainPassed)
        {
            FileState.SqlDataUpdateResult = StateIs.Started;
            Fmain.DbIo.CommandCurrent = "UPDATE TABLE " + "'" + FmainPassed.Fs.TableNameLine + "'";
            // command

            return FileState.SqlDataUpdateResult;
        }
        // <Section Id = "FileDataDelete">
        /// <summary> 
        /// Put a Delete command
        /// into the SQL Data command.
        /// </summary> 
        public StateIs SqlDataDelete(ref mFileMainDef FmainPassed)
        {
            FileState.SqlDataDeleteResult = StateIs.Started;
            FmainPassed.DbIo.CommandCurrent = "DELETE " + "'" + FmainPassed.Fs.TableNameLine + "'";
            // command

            return FileState.SqlDataDeleteResult;
        }
        // <Section Id = "FileDataAdd">
        /// <summary> 
        /// Put and Add command
        /// into the SQL Data command.
        /// </summary> 
        public StateIs SqlDataAdd(ref mFileMainDef FmainPassed)
        {
            FileState.SqlDataAddResult = StateIs.Started;
            FmainPassed.DbIo.CommandCurrent = "ALTER " + "'" + FmainPassed.Fs.TableNameLine + "'";
            // command

            return FileState.SqlDataAddResult;
        }
        // <Section Id = "FileDataGet">
        #endregion
        #region File Data - Get, Set, 
        /// <summary> 
        /// Put a Get command
        /// into the SQL Data command.
        /// </summary> 
        public StateIs SqlDataGetTo(ref mFileMainDef FmainPassed)
        {
            FileState.SqlDataGetResult = StateIs.Started;

            return FileState.SqlDataGetResult;
        }
        #endregion
        #region Data Command - SqlDataCommandCreate, SqlDataCommandExecute
        /// <summary> 
        /// Put a Create Table command
        /// (FileCommandCreate SqlDataCommandCreate, SqlDataCommandExecute)
        /// (NO) into the SQL Data command.
        /// Create the SQL Command instance.
        /// </summary> 
        public StateIs SqlDataCommandCreate()
        {
            return SqlDataCommandCreate(ref Fmain);
        }
        public StateIs SqlDataCommandCreate(ref mFileMainDef FmainPassed)
        {
            FileState.SqlDataWriteResult = StateIs.Started;
            bool mCreate = false;
            if (mCreate)
            {
                if (FmainPassed.FileStatus.bpIsCreating) {; }
                FmainPassed.DbIo.CommandCurrent = "CREATE TABLE " + "'" + FmainPassed.Fs.TableNameLine + "'";
                // command
            }
            if (FmainPassed.DbIo.CommandCurrent.Length > 0)
            {
                FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);

                FileState.SqlDataWriteResult = StateIs.Successful;
                LocalMessage.LogEntry = "Create Command: ";

                if (FmainPassed.DbIo.CommandCurrent.Length < 160)
                {
                    LocalMessage.LogEntry += FmainPassed.DbIo.CommandCurrent;
                } else
                {
                    LocalMessage.LogEntry += FmainPassed.DbIo.CommandCurrent.Substring(0,80) + "...";
                }
                // FileMainHeaderGetTo(ref FmainPassed);
               st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 8, ref Sender, bIsMessage,
                    TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(),
                    FileDoResult, false,
                    iNoErrorLevel, iNoErrorSource,
                    bDoNotDisplay, MessageNoUserEntry,
                    LocalMessage.LogEntry);

                // ToDo Message re new command
            }
            return FileState.SqlDataWriteResult;
        }
        // <Section Id = "FileCommandExecute">
        /// <summary> 
        /// Handles SqlDataCommand(s)
        /// Insert
        /// Update
        /// Delete
        /// into the SQL Database.
        /// Create the SQL Command instance.
        /// </summary> 
        public StateIs SqlDataCommandExecute(ref mFileMainDef FmainPassed)
        {
            FileState.SqlDataExecuteResult = StateIs.Started;
            bool mCreate = false;
            if (mCreate)
            {
                if (FmainPassed.FileStatus.bpIsCreating) {; }
                FmainPassed.DbIo.CommandCurrent = "CREATE TABLE " + "'" + FmainPassed.Fs.TableNameLine + "'";
                // command
            }
            FileState.SqlDataExecuteResult = StateIs.InProgress;
            FileSqlConn.ConnOpen(ref FmainPassed);
            if (FmainPassed.DbIo.CommandCurrent.Length > 0)
            {
                LocalMessage.LogEntry = "New Command: " + FmainPassed.DbIo.CommandCurrent;
                // FileMainHeaderGetTo(ref FmainPassed);
               st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 8, ref Sender, bIsMessage,
                    TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(),
                    FileDoResult, false,
                    iNoErrorLevel, iNoErrorSource,
                    bDoNotDisplay, MessageNoUserEntry,
                    LocalMessage.LogEntry);

                // FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
            }
            FileState.SqlDataExecuteResult = StateIs.InProgress;
            try
            {
                try
                {
                    // Open Connection
                    // ToDo SqlDataWrite Test Conn.Open Always ***
                    try
                    {
                        if (!Fmain.ConnStatus.DoKeepOpen)
                        {
                            if (FmainPassed.DbIo.SqlDbConn.State == ConnectionState.Open)
                            {
                                if (FmainPassed.DbIo.SqlDbReader != null && !FmainPassed.DbIo.SqlDbReader.IsClosed)
                                {
                                    FmainPassed.DbIo.SqlDbReader.Close();
                                }
                                // ((mFileDef)FmainPassed.Sender).State.SqlColAddCmdBuildResult = FileSqlConn.ConnClose(ref FmainPassed);
                                FileState.SqlDataExecuteResult = FileSqlConn.ConnOpen(ref FmainPassed);
                            }
                            else if (FmainPassed.DbIo.SqlDbConn.State != ConnectionState.Open)
                            {
                                FileState.SqlDataExecuteResult = FileSqlConn.ConnOpen(ref FmainPassed);
                                //((mFileDef)FmainPassed.Sender).State.SqlColAddCmdBuildResult = ConnReset(ref FmainPassed);
                                //FmainPassed.DbIo.SqlDbConn.Open();
                            }
                        }
                    }
                    catch {; }
                }
                catch (Exception)
                {
                    //FmainPassed.DbIo.SqlDbConn.Close();
                    //FmainPassed.DbIo.SqlDbConn.Open();
                    throw;
                }
                // The following was NOT SqlCommandIs.Insert
                if (FmainPassed.FileSqlConn.DbSyn.DatabaseCmdType != DbCommandIs.Insert)
                {
                    // Note: @ID direction output was added to the command calls
                    FmainPassed.RowInfoDb.RowCount = 0;
                    FmainPassed.RowInfoDb.RowCount = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                } else
                {
                    // Note that all insert commands use ExecuteScalar so that
                    // they can retrieve the new primary key from the insert...
                    // this can be handled by using an OUTPUT clause in the command...
                    FmainPassed.Item.ItemPrimaryKey = (int)FmainPassed.DbIo.SqlDbCommand.ExecuteScalar();

                    if (FmainPassed.Item.ItemPrimaryKey > 0) {
                        FmainPassed.RowInfoDb.RowCount = 1;
                        FmainPassed.Item.NewRecord = true;
                    } else { FmainPassed.RowInfoDb.RowCount = 0; }

                }
                if (FmainPassed.RowInfoDb.RowCount > 0)
                {
                    FileState.SqlDataExecuteResult = StateIs.Successful;
                    // get primary key
                    //try
                    //{
                    //    FmainPassed.Item.ItemPrimaryKey = (int)Convert.ToInt32(FmainPassed.DbIo.SqlDbCommand.Parameters["@ID"].Value);
                    //}
                    //catch (Exception)
                    //{
                    //    FmainPassed.Item.ItemPrimaryKey = 0;
                    //    // throw;
                    //}
                } else {
                    FileState.SqlDataExecuteResult = StateIs.Failed;
                    // ToDo clear primary key?
                }
                // ToDo this is row affected not an error result.
                if (FmainPassed.DbIo.SqlDbReader != null && !FmainPassed.DbIo.SqlDbReader.IsClosed)
                {
                    // FmainPassed.DbIo.SqlDbReader.Close();
                }
                if (FmainPassed.DbIo.SqlDbConn != null && FmainPassed.DbIo.SqlDbConn.State == ConnectionState.Open)
                {
                    FileSqlConn.ConnClose(ref FmainPassed);
                }
                else if (FmainPassed.DbIo.SqlDbConn != null && FmainPassed.DbIo.SqlDbConn.State != ConnectionState.Open)
                {
                    // ((mFileDef)FmainPassed.Sender).State.SqlColAddCmdBuildResult = ConnOpen(ref FmainPassed);
                    //((mFileDef)FmainPassed.Sender).State.SqlColAddCmdBuildResult = ConnReset(ref FmainPassed);
                    //FmainPassed.DbIo.SqlDbConn.Open();
                }
                if (FmainPassed.RowInfoDb.RowCount > 0)
                {
                    FmainPassed.FileStatus.bpDoesExist = true;
                    if (mCreate)
                    {
                        // Add Column 0
                        FmainPassed.DbIo.CommandCurrent = "ALTER TABLE " + "'" + FmainPassed.Fs.TableName + "'";
                        FmainPassed.DbIo.CommandCurrent += " ADD 0 String ";
                        FmainPassed.DbIo.CommandCurrent += " VARCHAR(512)";
                        FmainPassed.DbIo.CommandCurrent += " { PRIMARY KEY }";
                        FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
                        FmainPassed.DbIo.SqlDbRowsAffected = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                        if (FmainPassed.DbIo.SqlDbRowsAffected > 0)
                        {
                            FileState.SqlDataExecuteResult = StateIs.Successful;
                        } else
                        {
                            FileState.SqlDataExecuteResult = StateIs.Failed;

                        }
                        // Add Primary Key
                        // FmainPassed.DbIo.CommandCurrent = " { PRIMARY KEY }";
                        // Add Unique
                        // FmainPassed.DbIo.CommandCurrent = " { UNIQUE }";
                    }
                }
                else {
                    FmainPassed.FileStatus.bpDoesExist = false;
                }
            }
            catch (SqlException ExceptionSql)
            {
                FileState.SqlDataExecuteResult = StateIs.DatabaseError;
                LocalMessage.ErrorMsg = sEmpty;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.SqlDataExecuteResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            catch (Exception ExceptionGeneral)
            {
                FileState.SqlDataExecuteResult = StateIs.Failed;
                LocalMessage.ErrorMsg = sEmpty;
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.SqlDataExecuteResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            finally
            {
                // FmainPassed.DbIo.SqlDbCommand = null;
            }

            return FileState.SqlDataExecuteResult;
        }
        #endregion
        #region File Data Command - Open, Close
        // <Section Id = "FileDataReaderOpen">
        /// <summary> 
        /// Create and Connectioin and get a reader
        /// </summary>         
        public StateIs SqlDataCommandOpenCommand(ref mFileMainDef FmainPassed, string CommandCurrentPassed)
        {
            FileState.SqlDataReadOpenResult = StateIs.Started;
            bool mCreate = false;
            if (mCreate)
            {
                if (FmainPassed.FileStatus.bpIsCreating) {; }
                FmainPassed.DbIo.CommandCurrent = "CREATE TABLE " + "'" + FmainPassed.Fs.TableNameLine + "'";
                // command
            }
            try
            {
                try
                {
                    // ToDo SqlDataWrite Test Conn.Open Always ***
                    try
                    {
                        if (FmainPassed.DbIo.SqlDbConn.State == ConnectionState.Open)
                        {
                            if (!FmainPassed.DbIo.SqlDbReader.IsClosed)
                            {
                                FmainPassed.DbIo.SqlDbReader.Close();
                            }
                            FileState.SqlColAddCmdBuildResult = FileSqlConn.ConnClose(ref FmainPassed);
                        }
                        else if (FmainPassed.DbIo.SqlDbConn.State != ConnectionState.Open)
                        {
                            // ((mFileDef)FmainPassed.Sender).State.SqlColAddCmdBuildResult = ConnOpen(ref FmainPassed);
                            //((mFileDef)FmainPassed.Sender).State.SqlColAddCmdBuildResult = ConnReset(ref FmainPassed);
                            //FmainPassed.DbIo.SqlDbConn.Open();
                        }
                    }
                    catch {; }
                    FileState.SqlDataReadOpenResult = StateIs.InProgress;
                    FileSqlConn.ConnOpen();
                }
                catch (Exception)
                {
                    FileState.SqlDataReadOpenResult = StateIs.Failed;
                    //FmainPassed.DbIo.SqlDbConn.Close();
                    //FmainPassed.DbIo.SqlDbConn.Open();
                    throw;
                }
                if (CommandCurrentPassed.Length > 0)
                {
                    FmainPassed.DbIo.CommandCurrent = CommandCurrentPassed;
                    if (FmainPassed.DbIo.CommandCurrent.Length > 0)
                    {
                        FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
                    }

                    FmainPassed.RowInfoDb.RowCount = 0;
                    FmainPassed.RowInfoDb.RowCount = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                    if (FmainPassed.RowInfoDb.RowCount > 0)
                    { FileState.SqlDataReadOpenResult = StateIs.Successful;
                    } else { FileState.SqlDataReadOpenResult = StateIs.Failed; }

                    if (!FmainPassed.DbIo.SqlDbReader.IsClosed)
                    {
                        // FmainPassed.DbIo.SqlDbReader.Close();
                    }
                }

            }
            catch (SqlException ExceptionSql)
            {
                FileState.SqlDataReadOpenResult = StateIs.DatabaseError;
                LocalMessage.ErrorMsg = sEmpty;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.SqlDataReadOpenResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            catch (Exception ExceptionGeneral)
            {
                FileState.SqlDataReadOpenResult = StateIs.Failed;
                LocalMessage.ErrorMsg = sEmpty;
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.SqlDataReadOpenResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            finally
            {
                // FmainPassed.DbIo.SqlDbCommand = null;
            }

            return FileState.SqlDataReadOpenResult;

        }        // <Section Id = "FileDataReaderOpen">
        /// <summary> 
        /// Create and Connectioin and get a reader
        /// </summary>         
        public StateIs SqlDataCommandOpen(ref mFileMainDef FmainPassed)
        {
            FileState.SqlDataReadOpenResult = StateIs.InProgress;
            // ToDo process command
            SqlDataReadOpenCommand(ref FmainPassed, sEmpty);
            return FileState.SqlDataReadOpenResult;

        }
        // <Section Id = "FileDataReaderOpen">
        /// <summary> 
        /// Close the reader and it's connection
        /// </summary>         
        public StateIs SqlDataCommandClose(ref mFileMainDef FmainPassed)
        {
            bool mCreate = false;
            FileState.SqlDataReadCloseResult = StateIs.InProgress;
            try
            {
                if (FmainPassed.DbIo.SqlDbConn.State == ConnectionState.Open)
                {
                    if (!FmainPassed.DbIo.SqlDbReader.IsClosed)
                    {
                        FmainPassed.DbIo.SqlDbReader.Close();
                    }
                    //
                    if (!Fmain.ConnStatus.DoKeepOpen)
                    {
                        if (!Fmain.ConnStatus.DoKeepOpen)
                        {
                            FileSqlConn.ConnClose(ref FmainPassed);
                        }
                    }
                }
                else if (FmainPassed.DbIo.SqlDbConn.State != ConnectionState.Open)
                {
                    // ((mFileDef)FmainPassed.Sender).State.SqlColAddCmdBuildResult = ConnOpen(ref FmainPassed);
                    //((mFileDef)FmainPassed.Sender).State.SqlColAddCmdBuildResult = ConnReset(ref FmainPassed);
                    //FmainPassed.DbIo.SqlDbConn.Open();
                }
                if (FileState.SqlDataReadCloseResult > 0)
                {
                    FmainPassed.FileStatus.bpDoesExist = true;
                    if (mCreate)
                    {
                        // Add Column 0
                        FmainPassed.DbIo.CommandCurrent = "ALTER TABLE " + "'" + FmainPassed.Fs.TableName + "'";
                        FmainPassed.DbIo.CommandCurrent += " ADD 0 String ";
                        FmainPassed.DbIo.CommandCurrent += " VARCHAR(512)";
                        FmainPassed.DbIo.CommandCurrent += " { PRIMARY KEY }";
                        FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
                        FmainPassed.DbIo.SqlDbRowsAffected = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                        if (FmainPassed.DbIo.SqlDbRowsAffected > 0)
                        {
                            FileState.SqlDataReadCloseResult = StateIs.Successful;
                        }
                        else
                        {
                            FileState.SqlDataReadCloseResult = StateIs.Failed;

                        }
                        // Add Primary Key
                        // FmainPassed.DbIo.CommandCurrent = " { PRIMARY KEY }";
                        // Add Unique
                        // FmainPassed.DbIo.CommandCurrent = " { UNIQUE }";
                    }
                }
                else {
                    FmainPassed.FileStatus.bpDoesExist = false;
                }
            }
            catch (SqlException ExceptionSql)
            {
                // ToDo State not set.  Close code need review
                LocalMessage.ErrorMsg = sEmpty;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.SqlDataReadCloseResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            catch (Exception ExceptionGeneral)
            {
                LocalMessage.ErrorMsg = sEmpty;
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.SqlDataReadCloseResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            finally
            {
                // FmainPassed.DbIo.SqlDbCommand = null;
            }

            return FileState.SqlDataReadCloseResult;

        }
        // <Section Id = "FileDataWrite">
        /// <summary> 
        /// Put a Create Table command (NO)
        /// into the SQL Data command.
        /// Create the SQL Command instance.
        /// </summary> 
        #endregion
        #region File Data Read - Open, Open Command, Close
        // <Section Id = "FileDataReaderOpen">
        /// <summary> 
        /// Create and Connectioin and get a reader
        /// </summary>         
        public StateIs SqlDataReadOpenCommand(ref mFileMainDef FmainPassed, string CommandCurrentPassed)
        {
            FileState.SqlDataReadOpenResult = StateIs.Started;
            bool mCreate = false;
            if (mCreate)
            {
                if (FmainPassed.FileStatus.bpIsCreating) {; }
                FmainPassed.DbIo.CommandCurrent = "CREATE TABLE " + "'" + FmainPassed.Fs.TableNameLine + "'";
                // command
            }
            //
            FileState.SqlDataReadOpenResult = StateIs.InProgress;
            try
            {
                try
                {
                    if (!FmainPassed.FileStatus.DoKeepOpen)
                    {
                        if (FmainPassed.DbIo.SqlDbConn != null)
                        {
                            // Close Reader
                            if (FmainPassed.DbIo.SqlDbConn.State == ConnectionState.Open)
                            {
                                if (FmainPassed.DbIo.SqlDbReader != null)
                                {
                                    if (!FmainPassed.DbIo.SqlDbReader.IsClosed)
                                    {
                                        FmainPassed.DbIo.SqlDbReader.Close();
                                    }
                                }
                                // Close Connection
                                FileState.SqlColAddCmdBuildResult = FileSqlConn.ConnClose(ref FmainPassed);
                            }
                        }
                        // ToDo add exceptions handling
                        //
                        if (!FmainPassed.FileStatus.DoKeepOpen)
                        {
                            // ToDo Detach Connection

                            // ToDo Attach New Connection
                        }
                        //
                        // Open Connection
                        if (!Fmain.ConnStatus.DoKeepOpen) { FileSqlConn.ConnOpen(); }
                    }
                }
                catch (Exception)
                {
                    //FmainPassed.DbIo.SqlDbConn.Close();
                    //FmainPassed.DbIo.SqlDbConn.Open();
                    FileState.SqlDataReadOpenResult = StateIs.Failed;
                    throw;
                }
                if (CommandCurrentPassed.Length > 0)
                {
                    FmainPassed.DbIo.CommandCurrent = CommandCurrentPassed;

                    LocalMessage.LogEntry = "Create Command: " + FmainPassed.DbIo.CommandCurrent;
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 8, ref Sender, bIsMessage,
                        TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(),
                        FileDoResult, false,
                        iNoErrorLevel, iNoErrorSource,
                        bDoNotDisplay, MessageNoUserEntry,
                        LocalMessage.LogEntry);

                    FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
                    // **** HERE
                    //((mFileDef)FmainPassed.Sender).State.SqlDataReadOpenResult = SqlCommandDo(ref FmainPassed, FmainPassed.DbIo.CommandCurrent);

                    LocalMessage.LogEntry = "Do Command: Data Read";
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 10, ref Sender, bIsMessage,
                        TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(),
                        FileDoResult, false,
                        iNoErrorLevel, iNoErrorSource,
                        bDoNotDisplay, MessageNoUserEntry,
                        LocalMessage.LogEntry);

                    FileState.SqlDataReadOpenResult = SqlDataRead(ref FmainPassed);
                }
                else
                {
                    LocalMessage.LogEntry = "Do Command: Data Read";
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 10, ref Sender, bIsMessage,
                        TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(),
                        FileDoResult, false,
                        iNoErrorLevel, iNoErrorSource,
                        bDoNotDisplay, MessageNoUserEntry,
                        LocalMessage.LogEntry);

                    FileState.SqlDataReadOpenResult = SqlDataRead(ref FmainPassed);
                    //((mFileDef)FmainPassed.Sender).State.SqlDataReadOpenResult = StateIs.Failed;
                    //LocalMessage.ErrorMsg = "No Command Present";
                    //ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, ((mFileDef)FmainPassed.Sender).State.SqlDataReadOpenResult);
                }

                ////((mFileDef)FmainPassed.Sender).State.SqlDataReadOpenResult = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                //if (!FmainPassed.FileStatus.DoKeepOpen)
                //{
                //    if (!FmainPassed.DbIo.SqlDbReader.IsClosed)
                //    {
                //        FmainPassed.DbIo.SqlDbReader.Close();
                //    }
                //}
                ////
                //if (!FmainPassed.FileStatus.DoKeepOpen)
                //{
                //    // ToDo Detach Connection
                //}
            }
            catch (SqlException ExceptionSql)
            {
                FileState.SqlDataReadOpenResult = StateIs.DatabaseError;
                LocalMessage.ErrorMsg = sEmpty;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.SqlDataReadOpenResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            catch (Exception ExceptionGeneral)
            {
                FileState.SqlDataReadOpenResult = StateIs.Failed;
                LocalMessage.ErrorMsg = sEmpty;
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.SqlDataReadOpenResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            finally
            {
                // FmainPassed.DbIo.SqlDbCommand = null;
            }

            return FileState.SqlDataReadOpenResult;

        }        // <Section Id = "FileDataReaderOpen">
        /// <summary> 
        /// Create and Connectioin and get a reader
        /// </summary>         
        public StateIs SqlDataReadOpen(ref mFileMainDef FmainPassed)
        {
            FileState.SqlDataReadOpenResult = StateIs.InProgress;
            // ToDo process command
            SqlDataReadOpenCommand(ref FmainPassed, sEmpty);
            return FileState.SqlDataReadOpenResult;

        }
        // <Section Id = "FileDataReaderOpen">
        /// <summary> 
        /// Close the reader and it's connection
        /// </summary>         
        public StateIs SqlDataReadClose(ref mFileMainDef FmainPassed)
        {
            bool mCreate = false;
            FileState.SqlDataReadCloseResult = StateIs.InProgress;
            try
            {
                if (FmainPassed.DbIo != null)
                {
                    if (!Fmain.ConnStatus.DoKeepOpen
                        || FmainPassed.DbIo.SqlDbConn.State == ConnectionState.Open
                        || !FmainPassed.DbIo.SqlDbReader.IsClosed)
                    {
                        if (FmainPassed.DbIo.SqlDbReader != null && !FmainPassed.DbIo.SqlDbReader.IsClosed)
                        {
                            FmainPassed.DbIo.SqlDbReader.Close();
                        }
                        //
                        if (!Fmain.ConnStatus.DoKeepOpen)
                        {
                            FileSqlConn.ConnClose(ref FmainPassed);
                        }
                    }

                    else if (FmainPassed.DbIo.SqlDbConn.State != ConnectionState.Open)
                    {
                        // ((mFileDef)FmainPassed.Sender).State.SqlColAddCmdBuildResult = ConnOpen(ref FmainPassed);
                        //((mFileDef)FmainPassed.Sender).State.SqlColAddCmdBuildResult = ConnReset(ref FmainPassed);
                        //FmainPassed.DbIo.SqlDbConn.Open();
                    }
                    if (StateIsSuccessfulAll(FileState.SqlDataReadCloseResult))
                    {
                        FmainPassed.FileStatus.bpDoesExist = true;
                        if (mCreate)
                        {
                            // Add Column 0
                            FmainPassed.DbIo.CommandCurrent = "ALTER TABLE " + "'" + FmainPassed.Fs.TableName + "'";
                            FmainPassed.DbIo.CommandCurrent += " ADD 0 String ";
                            FmainPassed.DbIo.CommandCurrent += " VARCHAR(512)";
                            FmainPassed.DbIo.CommandCurrent += " { PRIMARY KEY }";
                            FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
                            FileState.SqlDataReadCloseResult = StateIs.InProgress;
                            FmainPassed.DbIo.SqlDbRowsAffected = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                            // Add Primary Key
                            // FmainPassed.DbIo.CommandCurrent = " { PRIMARY KEY }";
                            // Add Unique
                            // FmainPassed.DbIo.CommandCurrent = " { UNIQUE }";
                        }
                    }
                    else {
                        FmainPassed.FileStatus.bpDoesExist = false;
                    }
                }
            }
            catch (SqlException ExceptionSql)
            {
                LocalMessage.ErrorMsg = sEmpty;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.SqlDataReadCloseResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            catch (Exception ExceptionGeneral)
            {
                LocalMessage.ErrorMsg = sEmpty;
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.SqlDataReadCloseResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            finally
            {
                // FmainPassed.DbIo.SqlDbCommand = null;
            }

            return FileState.SqlDataReadCloseResult;

        }
        /// <summary> 
        /// Put a Create Table command (NO)
        /// into the SQL Data command.
        /// Create the SQL Command instance.
        /// </summary> 
        public StateIs SqlDataRead(ref mFileMainDef FmainPassed)
        {
            FileState.SqlDataReadResult = StateIs.InProgress;
            //bool mCreate = false;
            try
            {
                if (FmainPassed.DbIo != null) 
                {
                    // ToDo This is wrong.  An Execute Reader and NonQuery?
                    // ((mFileDef)FmainPassed.Sender).State.SqlDataReadResult = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                    FmainPassed.RowInfoDb.RowCount = 1;
                    FmainPassed.DbIo.SqlDbReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(); //.ExecuteNonQuery
                    if (FmainPassed.RowInfoDb.RowCount > 0)
                    {
                        FileState.SqlDataReadResult = StateIs.Successful;
                    }
                    else { FileState.SqlDataReadResult = StateIs.Failed; }

                    // ToDo SqlDataRead Conn.Open?
                    if (FmainPassed.RowInfoDb.RowCount > 0)
                    {
                        FmainPassed.FileStatus.bpDoesExist = true;
                        //if (mCreate)
                        //{
                        //    // Add Column 0
                        //    FmainPassed.DbIo.CommandCurrent = "ALTER TABLE " + "'" + FmainPassed.Fs.TableName + "'";
                        //    FmainPassed.DbIo.CommandCurrent += " ADD 0 String ";
                        //    FmainPassed.DbIo.CommandCurrent += " VARCHAR(512)";
                        //    FmainPassed.DbIo.CommandCurrent += " { PRIMARY KEY }";
                        //    FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
                        //    ((mFileDef)FmainPassed.Sender).State.SqlDataWriteResult = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                        //    // Add Primary Key
                        //    // FmainPassed.DbIo.CommandCurrent = " { PRIMARY KEY }";
                        //    // Add Unique
                        //    // FmainPassed.DbIo.CommandCurrent = " { UNIQUE }";
                        //}
                    }
                    else {
                        FmainPassed.FileStatus.bpDoesExist = false;
                    }
                } else { FileState.SqlDataReadResult = StateIs.UnknownFailure; }
            }
            catch (SqlException ExceptionSql)
            {
                FileState.SqlDataReadResult = StateIs.DatabaseError;
                LocalMessage.ErrorMsg = sEmpty;
                FileState.SqlDataReadResult = StateIs.Failed;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.SqlDataReadResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            catch (Exception ExceptionGeneral)
            {
                FileState.SqlDataReadResult = StateIs.Failed;
                LocalMessage.ErrorMsg = sEmpty;
                FileState.SqlDataReadResult = StateIs.Failed;
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.SqlDataReadResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            finally
            {
                FmainPassed.DbIo.SqlDbCommand = null;
            }
            return FileState.SqlDataReadResult;
        }
        // <Section Id = "FileDataWrite">
        /// <summary> 
        /// Put a Create Table command (NO)
        /// into the SQL Data command.
        /// Create the SQL Command instance.
        /// </summary> 
        public StateIs SqlDataReadNext(ref mFileMainDef FmainPassed)
        {
            FileState.SqlDataReadResult = StateIs.InProgress;
            //bool mCreate = false;
            bool MoreRows = true;
            try
            {
                LocalMessage.LogEntry = "Do Command: Read Next";
               st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 10, ref Sender, bIsMessage,
                    TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(),
                    FileDoResult, false,
                    iNoErrorLevel, iNoErrorSource,
                    bDoNotDisplay, MessageNoUserEntry,
                    LocalMessage.LogEntry);

                // ToDo SqlDataReadNext Conn.Open.Check?
                MoreRows = FmainPassed.DbIo.SqlDbReader.Read();
                //
                if (!MoreRows)
                {
                    // ((mFileDef)FmainPassed.Sender).State.SqlDataReadResult = StateIs.Finished;
                    FileState.SqlDataReadResult = StateIs.Failed;
                }
                else
                {
                    FileState.SqlDataReadResult = StateIs.Successful;
                }
                if (MoreRows)
                {
                    FmainPassed.FileStatus.bpDoesExist = true;
                    //if (mCreate)
                    //{
                    //    // Add Column 0
                    //    FmainPassed.DbIo.CommandCurrent = "ALTER TABLE " + "'" + FmainPassed.Fs.TableName + "'";
                    //    FmainPassed.DbIo.CommandCurrent += " ADD 0 String ";
                    //    FmainPassed.DbIo.CommandCurrent += " VARCHAR(512)";
                    //    FmainPassed.DbIo.CommandCurrent += " { PRIMARY KEY }";
                    //    FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
                    //    ((mFileDef)FmainPassed.Sender).State.SqlDataWriteResult = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                    //    // Add Primary Key
                    //    // FmainPassed.DbIo.CommandCurrent = " { PRIMARY KEY }";
                    //    // Add Unique
                    //    // FmainPassed.DbIo.CommandCurrent = " { UNIQUE }";
                    //}
                }
                else {
                    FmainPassed.FileStatus.bpDoesExist = false;
                }
                // ((mFileDef)FmainPassed.Sender).State.SqlDataReadResult = StateIs.Finished;
                // ((mFileDef)FmainPassed.Sender).State.SqlDataReadResult = StateIs.Successful;
            }
            catch (SqlException ExceptionSql)
            {
                LocalMessage.ErrorMsg = sEmpty;
                FileState.SqlDataReadResult = StateIs.DatabaseError;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.SqlDataReadResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            catch (Exception ExceptionGeneral)
            {
                LocalMessage.ErrorMsg = sEmpty;
                FileState.SqlDataReadResult = StateIs.Failed;
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.SqlDataReadResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            finally
            {
                // FmainPassed.DbIo.SqlDbCommand = null;
            }
            return FileState.SqlDataReadResult;
        }
        /// <summary> 
        /// Put a Create Table command (NO)
        /// into the SQL Data command.
        /// Create the SQL Command instance.
        /// </summary> 
        public StateIs SqlDataReadNextClose(ref mFileMainDef FmainPassed)
        {
            FileState.SqlDataReadResult = StateIs.InProgress;
            //bool mCreate = false;
            try
            {
                FmainPassed.DbIo.SqlDbReader.Close();
                //
                if (FileState.SqlDataReadResult > 0)
                {
                    FmainPassed.FileStatus.bpDoesExist = true;
                    //if (mCreate)
                    //{
                    //    // Add Column 0
                    //    FmainPassed.DbIo.CommandCurrent = "ALTER TABLE " + "'" + FmainPassed.Fs.TableName + "'";
                    //    FmainPassed.DbIo.CommandCurrent += " ADD 0 String ";
                    //    FmainPassed.DbIo.CommandCurrent += " VARCHAR(512)";
                    //    FmainPassed.DbIo.CommandCurrent += " { PRIMARY KEY }";
                    //    FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
                    //    ((mFileDef)FmainPassed.Sender).State.SqlDataWriteResult = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                    //    // Add Primary Key
                    //    // FmainPassed.DbIo.CommandCurrent = " { PRIMARY KEY }";
                    //    // Add Unique
                    //    // FmainPassed.DbIo.CommandCurrent = " { UNIQUE }";
                    //}
                }
                else {
                    // FmainPassed.FileStatus.bpDoesExist = false;
                }
                FileState.SqlDataReadResult = StateIs.Finished;
            }
            catch (SqlException ExceptionSql)
            {
                LocalMessage.ErrorMsg = sEmpty;
                FileState.SqlDataReadResult = StateIs.DatabaseError;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.SqlDataReadResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            catch (Exception ExceptionGeneral)
            {
                LocalMessage.ErrorMsg = sEmpty;
                FileState.SqlDataReadResult = StateIs.Failed;
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.SqlDataReadResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            finally
            {
                // FmainPassed.DbIo.SqlDbCommand = null;
            }
            return FileState.SqlDataReadResult;
        }
        /// <summary> 
        /// Put a Create Table command (NO)
        /// into the SQL Data command.
        /// Create the SQL Command instance.
        /// </summary> 
        public StateIs SqlDataWrite(ref mFileMainDef FmainPassed)
        {
            FileState.SqlDataWriteResult = StateIs.InProgress;
            bool mCreate = false;
            try
            {
                try
                {
                    // ToDo SqlDataWrite Test Conn.Open Always ***
                    if (!FmainPassed.DbIo.SqlDbReader.IsClosed)
                    {
                        FmainPassed.DbIo.SqlDbReader.Close();
                    }
                    if (FmainPassed.DbIo.SqlDbReader.IsClosed)
                    {
                        FmainPassed.DbIo.SqlDbConn.Open();
                    }
                }
                catch (Exception)
                {
                    //FmainPassed.DbIo.SqlDbConn.Close();
                    //FmainPassed.DbIo.SqlDbConn.Open();
                    throw;
                }
                FmainPassed.RowInfoDb.RowCount = 0;
                FmainPassed.RowInfoDb.RowCount = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                if (FmainPassed.RowInfoDb.RowCount > 0)
                {
                    FileState.SqlDataWriteResult = StateIs.Successful;
                }
                else { FileState.SqlDataWriteResult = StateIs.Failed; }

                if (FmainPassed.RowInfoDb.RowCount > 0)
                {
                    FmainPassed.FileStatus.bpDoesExist = true;
                    if (mCreate)
                    {
                        // Add Column 0
                        FmainPassed.DbIo.CommandCurrent = "ALTER TABLE " + "'" + FmainPassed.Fs.TableName + "'";
                        FmainPassed.DbIo.CommandCurrent += " ADD 0 String ";
                        FmainPassed.DbIo.CommandCurrent += " VARCHAR(512)";
                        FmainPassed.DbIo.CommandCurrent += " { PRIMARY KEY }";
                        FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
                        FileState.SqlDataWriteResult = StateIs.InProgress;
                        FmainPassed.DbIo.SqlDbRowsAffected = FmainPassed.DbIo.SqlDbCommand.ExecuteNonQuery();
                        // Add Primary Key
                        // FmainPassed.DbIo.CommandCurrent = " { PRIMARY KEY }";
                        // Add Unique
                        // FmainPassed.DbIo.CommandCurrent = " { UNIQUE }";
                    }
                }
                else {
                    FmainPassed.FileStatus.bpDoesExist = false;
                }
            }
            catch (SqlException ExceptionSql)
            {
                LocalMessage.ErrorMsg = sEmpty;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.SqlDataWriteResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            catch (Exception ExceptionGeneral)
            {
                LocalMessage.ErrorMsg = sEmpty;
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.SqlDataWriteResult);
                FmainPassed.FileStatus.bpDoesExist = false;
            }
            finally
            {
                // FmainPassed.DbIo.SqlDbCommand = null;
            }

            return FileState.SqlDataWriteResult;
        }
        #endregion
        #endregion
        #region DatabaseFile
        #region Database Table Name Validate
        #endregion
       /// <summary> 
        /// Set the default field values for the Database.
        /// </summary> 
        public new StateIs DatabaseFieldsGetDefault(ref mFileMainDef FmainPassed)
        {
            FileState.DatabaseFieldsGetDefaultsResult = StateIs.Started;
            FileState.CopyIsDone = false;
            FileState.DoingDefaults = true;
            // System
            if (FmainPassed.Fs.SystemName.Length == 0)
            {
                if (!FileState.CopyIsDone) { FileState.CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.SystemName = SystemNameGetDefault(ref Faux);
            }
            // Service
            if (FmainPassed.Fs.ServiceName.Length == 0)
            {
                if (!FileState.CopyIsDone) { FileState.CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.ServiceName = ServiceNameGetDefault(ref Faux);
            }
            // Server
            if (FmainPassed.Fs.ServerName.Length == 0)
            {
                if (!FileState.CopyIsDone) { FileState.CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.ServerName = ServerNameGetDefault(ref Faux);
            }
            // Database
            if (FmainPassed.Fs.DatabaseName.Length == 0)
            {
                if (!FileState.CopyIsDone) { FileState.CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.DatabaseName = DatabaseNameGetDefault(ref Faux);
            }
            // FileOwnerName
            if (FmainPassed.Fs.FileOwnerName.Length == 0)
            {
                if (!FileState.CopyIsDone) { FileState.CopyIsDone = true; Faux.Fs.CopyFrom(ref FmainPassed.Fs); }
                FmainPassed.Fs.FileOwnerName = FileOwnerGetDefault(ref Faux);
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
        public new String TableNameLineBuild(ref mFileMainDef FmainPassed)
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
        public new StateIs TableNameLineBuild()
        {
            FileState.DatabaseFileLongResult = StateIs.Started;
            Fmain.Fs.TableNameLine = TableNameLineBuild(ref Fmain);
            return (FileState.DatabaseFileLongResult = StateIs.Successful);
        }
        #endregion
        #region Database Creation
        #endregion
        #region Database Deletion
        #endregion
        #region Database Close
        #endregion
        #region Database File General Exceptions
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        #region Class Interface
        // implemented in partial classes
        #endregion
        #endregion
        #region Unit Test
        #endregion

    }
    #region Core Fields
    #endregion
    #region Framework Objects
    #endregion
    #region Class Factory
    #endregion
    #region Class
    #region Class Initialization
    // Constructors
    // Initializers
    // Delegates
    #endregion
    #region Class Fields
    #endregion
    #region Class Methods
    #endregion
    #region Class Interface
    // implemented in partial classes
    #endregion
    #endregion
    #region Unit Test
    #endregion
}
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
