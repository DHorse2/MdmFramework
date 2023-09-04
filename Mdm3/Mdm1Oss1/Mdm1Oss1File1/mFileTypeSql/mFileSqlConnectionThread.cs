#region System
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
#region System Globalization
//using System.Globalization;
#endregion
#region System Other
// using System.Collections.Specialized;
#endregion
#region System Reflection, Diagnostics, RT and Timers
//using System.Diagnostics;
//using System.Reflection;
//using System.Runtime;
//using System.Runtime.ExceptionServices;
//using System.Runtime.InteropServices;
//using System.Runtime.Remoting.Messaging;
//using System.Timers;
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
#region  System Threading
using System.Threading;
// using System.Threading.Tasks;
#endregion
#region System Windows Forms
//using System.Drawing;
using System.Windows;
using System.Windows.Forms;
//using System.Windows.Controls;
#endregion
#region System ComponentModel
//using System.ComponentModel;
#endregion
#region System Security
//using System.Security.AccessControl;
//using System.Security.Permissions;
//using System.Security.Principal;
#endregion
#region System Serialization (Runtime and Xml)
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Xml.Serialization;
#endregion
#region System XML
//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Schema;
#endregion
#endregion
#region Mdm
#region  Mdm Core
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.File.Control;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Components;
using Mdm.World;
#endregion
#region  Mdm Db and File
using Mdm.Oss.File;
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
#region Mdm Srt (Search, replace and transform)
using Mdm.Srt;
using Mdm.Srt.Core;
//using Mdm.Srt.Transform;
//using Mdm.Srt.Script;
#endregion

#endregion

namespace Mdm.Oss.File.Type.Sql
{
    public partial class mFileSqlConnectionDef
    // class mFileSqlConnectionThread
    {
        #region Thread Local Storage (ThreadStatic)
        [ThreadStatic]
        public static DbDataDef DbData;
        public static DbTaskDef.CalculationLongDelegate CalculationTaskDel;
        //public static ShortcutTaskDef.ShortcutTaskDataEventHandler ShortcutTaskDataEvent;

        public static int IdColumnIndex = 100;
        public static int DateUpdatedColumnIndex = 200;
        public static int StatusColumnIndex = 300;
        public static int ReplaceColumnIndex = 6;
        public static int FieldNameColumnIndex = 3;
        public static int OutputActionColumnIndex = 4;

        #endregion
        #region File Sql Thread Management
        private bool RefreshIsBusy;
        public bool DataInsertSkipRowEnter;
        public EventArgs DataInsertSkipRowEnterArgs;
        public volatile int DbTaskIdBusyValue;
        public int DbTaskIdBusy
        {
            get
            {
                return DbTaskIdBusyValue;
            }

            set
            {
                if (st != null)
                {
                    string TraceMessage = "Calculation Task DbTaskIdBusy (" + DbTaskIdBusyValue.ToString() + ") now set to " + value.ToString();
                    st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
                }
                DbTaskIdBusyValue = value;
            }
        }
        #endregion
        #region Thread Local Storage (ThreadStatic)
        #region Supporting Fields
        [ThreadStatic]
        public static List<string> Context = new List<string>();
        [ThreadStatic]
        public static string DirectoryName;
        [ThreadStatic]
        public static string FileName;
        [ThreadStatic]
        public static int DirectoryCount;
        [ThreadStatic]
        public static int FileCount;
        [ThreadStatic]
        public static int SelectedCount;
        [ThreadStatic]
        public static string ItemOld;
        [ThreadStatic]
        public static bool ItemValid;
        #endregion
        // mlSrtRun is the exteneded mSrtRun 
        // and has the processing function.
        // A run processs Script and list of ScriptItems
        [ThreadStatic]
        public static SrtRunDef SrtRun;
        #region Results
        // Populate
        [ThreadStatic]
        static StateIs DbPopulateResult;
        [ThreadStatic]
        static StateIs DbPopulateThreadResult;
        [ThreadStatic]
        static StateIs DbPopulateDisposeResult;
        // Command
        [ThreadStatic]
        static StateIs DbCommandResult;
        [ThreadStatic]
        static StateIs DbCommandThreadResult;
        [ThreadStatic]
        static StateIs DbCommandDisposeResult;
        // Close
        [ThreadStatic]
        static StateIs DbConnCloseResult;
        [ThreadStatic]
        static StateIs DbConnCloseThreadResult;
        [ThreadStatic]
        static StateIs DbConnCloseThreadDoResult;
        // Open
        [ThreadStatic]
        static StateIs DbConnOpenResult;
        [ThreadStatic]
        static StateIs DbConnOpenThreadResult;
        [ThreadStatic]
        static StateIs DbConnOpenThreadDoResult;
        // Other
        [ThreadStatic]
        static StateIs ClickThreadDoResult;
        [ThreadStatic]
        static StateIs UpdateCountsResult;
        #endregion
        // LinkData
        [ThreadStatic]
        static int DbListIdTemp = 0;
        [ThreadStatic]
        static bool FirstRead;
        [ThreadStatic]
        static bool NewRecord;
        [ThreadStatic]
        static bool cont;
        //
        #region Tasks
        [ThreadStatic]
        public static int CalculationTaskId;
        [ThreadStatic]
        public static DbThreadDef CalculationTaskThread;
        // Thread Task Types:
        [ThreadStatic]
        public int DbThreadId;
        [ThreadStatic]
        public static DbThreadDef DbTaskThread;
        [ThreadStatic]
        public static DbTaskDef DbTask;
        public DbTaskDef.CalculationLongDelegate DbTaskDel;
        public DbTaskDef.TaskDbGetSqlDelegate DbGetSqlDel;
        public DbTaskDef.TaskDbSetSqlDelegate DbSetSqlDel;
        public DbTaskDef.TaskDbSetSqlDataLineDelegate DbSetSqlDataLineDel;

        [ThreadStatic]
        public int DbCmdThreadId;
        [ThreadStatic]
        public DbTaskDef DbCmdTask;
        public DbTaskDef.CalculationObjectDelegate DbCmdTaskDel;
        public DbTaskDef.TaskDbGetSqlDelegate DbCmdGetSqlDel;
        public DbTaskDef.TaskDbSetSqlDelegate DbCmdSetSqlDel;
        public DbTaskDef.TaskDbSetSqlDataLineDelegate DbCmdSetSqlDataLineDel;

        [ThreadStatic]
        public int DbPopThreadId;
        [ThreadStatic]
        public DbTaskDef DbPopTask;
        public DbTaskDef.CalculationObjectDelegate DbPopTaskDel;
        public DbTaskDef.TaskDbGetSqlDelegate DbPopGetSqlDel;
        public DbTaskDef.TaskDbSetSqlDelegate DbPopSetSqlDel;
        public DbTaskDef.TaskDbSetSqlDataLineDelegate DbPopSetSqlDataLineDel;

        [ThreadStatic]
        public int DbConnOpenThreadId;
        [ThreadStatic]
        public DbTaskDef DbConnOpenTask;
        public DbTaskDef.CalculationLongDelegate DbConnOpenTaskDel;
        public DbTaskDef.TaskDbGetSqlDelegate DbConnOpenGetSqlDel;
        public DbTaskDef.TaskDbSetSqlDelegate DbConnOpenSetSqlDel;
        public DbTaskDef.TaskDbSetSqlDataLineDelegate DbConnOpenSetSqlDataLineDel;

        [ThreadStatic]
        public int DbConnCloseThreadId;
        [ThreadStatic]
        public DbTaskDef DbConnCloseTask;
        public DbTaskDef.CalculationLongDelegate DbConnCloseTaskDel;
        public DbTaskDef.TaskDbGetSqlDelegate DbConnCloseGetSqlDel;
        public DbTaskDef.TaskDbSetSqlDelegate DbConnCloseSetSqlDel;
        public DbTaskDef.TaskDbSetSqlDataLineDelegate DbConnCloseSetSqlDataLineDel;
        // are these threadstatic?
        // should they be part of the task or thread classes?
        //public static bool FileSqlBusy;
        //public static Mutex FileSqlLock;

        //public DbThreadDef DbTaskThread;
        //public DbThreadDef.CalculationLongDelegate DbTaskThreadDel;
        //public FileSqlTaskDef.ScriptTaskDataEventHandler ScriptTaskDataEvent;
        #endregion
        #endregion
        #region Results
        StateIs FileSqlGetResultResult;
        // Populate
        //StateIs DbPopulateResult;
        //StateIs DbPopulateThreadResult;
        //StateIs DbPopulateDisposeResult;
        // Command
        //StateIs DbCommandResult;
        //StateIs DbCommandThreadResult;
        //StateIs DbCommandDisposeResult;
        //// Close
        //StateIs DbConnCloseResult;
        //StateIs DbConnCloseThreadResult;
        //StateIs DbConnCloseThreadDoResult;
        //// Open
        //StateIs DbConnOpenResult;
        //StateIs DbConnOpenThreadResult;
        //StateIs DbConnOpenThreadDoResult;
        #endregion
        #region Db Data CRUD
        public string DateUpdated;
        public bool DataIsDirty;
        public StateIs DataWriteMoveUpThread(DbDataDef DbData, int IdPassed)
        {
            // ToDo Not Implemented message.
            return (FileState.DataWriteMoveUpResult = StateIs.ProgramInvalid);
        }
        // DataReadAll
        // DataWriteToEnd
        public StateIs DataWriteMoveUpAllThread(DbDataDef DbData, int IdPassed)
        {
            //// public void ControlDataGridViewRowIdGet(DataGridView x)
            //// RowRead
            //int NextId = 0;
            //int CurrId = 0;

            //RowNumberSelected = IdPassed;

            //RowNumberWorking = RowNumberSelected - 1;
            ////ControlReplaceDataNextId
            //if (RowNumberWorking >= 0)
            //{
            //    // SequenceNumberWorking = LinkData.Id - SequenceNumberWorking
            //    // >= 0 !!!!!
            //    // I need to use the row data to get the sequence one above...

            //    if (RowNumberWorking >= 0)
            //    {
            //        bool cont = true;
            //        while (cont)
            //        {
            //            CurrId =
            //            (int)CrList.Rows[RowNumberWorking].Cells[1].Value;
            //            // ReadThread(Next)
            //            DataReadThread(ref DbData, CurrId);

            //            // RowWrite to PrevData using Row.Id
            //            DbDataNext = new object();
            //            NextId =
            //            (int)CrList.Rows[RowNumberWorking+1].Cells[1].Value;
            //            DataReadThread(ref DbDataNext, NextId);
            //            // Move Next to Current
            //            // LinkData.Id = LinkData.Id;
            //            DbData.Sequence = DbDataNext.Sequence;
            //            DbData.InputString = DbDataNext.InputString;
            //            DbData.OutputString = DbDataNext.OutputString;
            //            DbData.FileName = DbDataNext.FileName;
            //            // Row Update 
            //            // ToDo WRONG
            //            DataWriteThread(ref DbData, CurrId);
            //            // loop
            //            // RowReadNext
            //            RowNumberWorking += 1;
            //            if (RowNumberWorking >= CrList.Rows.Count) { cont = false; }
            //        }
            //        // RowDelete Next.Id
            //        // RowDataDelete
            //        DataDeleteThread(ref DbDataNext, NextId);
            //        // SequenceNumberCurrent -= 1;
            //        // CrListSqlCommand(ref LinkData, sEmpty, " WHERE Id=" + IdPassed.ToString());
            //    }
            //}
            return (FileState.DataWriteMoveUpResult = StateIs.ProgramInvalid);
        }
        public StateIs DataReadThread(DbDataDef DbData, int IdPassed, bool AddRows, bool FirstOnly, bool GetSqlData, bool PassedSetButtons)
        {
            string TraceMessage = "Read " + IdPassed.ToString();
            st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
            // ToDo Read
            Fmain.FileAction.ToDo = FileAction_ToDoIs.Read;
            // , false, true, false, false
            string SqlSelCmdTemp =
            " WHERE Id=" + IdPassed.ToString();
            //+ " and " +
            //"ScriptName LIKE '" + ScriptNameFilter + "'";
            // ToDo FirstOnly & GetSqlData s/b true?
            FileState.DataReadResult = DbPopulateThreadCreate(DbData, sEmpty, SqlSelCmdTemp,
                AddRows, FirstOnly, GetSqlData, PassedSetButtons, true);
            // Wait for database and get standard result
            DbData = (DbDataDef)CalculationTaskUtils.WaitGetResult(st, DbPopThreadId, DbData, true, true);
            FileState.DataReadResult = (StateIs)CalculationTaskUtils.WaitGetResult(st, DbPopThreadId, DbData, false, false);
            DbPopulateThreadDispose();
            return FileState.DataReadResult;

        }
        // DataWrite to existing only
        public StateIs DataWriteThread(DbDataDef DbData)
        {
            Fmain.FileAction.ToDo = FileAction_ToDoIs.Update;
            FileState.DataWriteResult = DataWriteThread(DbData, DbData.Id);
            return FileState.DataReadResult;
        }
        // DataWrite to existing only
        public StateIs DataWriteThread(DbDataDef DbData, int IdPassed)
        {
            DbData.DateUpdated = DateTime.Now.ToString("O");
            string TraceMessage = "Write " + IdPassed.ToString();
            st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
            Fmain.FileAction.ToDo = FileAction_ToDoIs.Update;
            FileState.DataWriteResult = DbCommandThreadCreate(DbCommandIs.Update, DbData
                , sEmpty, " WHERE Id=" + IdPassed.ToString()
                , true, true, st.StdRunControlUi.SetButtons);
            FileState.DataWriteResult = (StateIs)CalculationTaskUtils.WaitGetResult(st, DbCmdThreadId, DbData, false, false);
            DbCommandThreadDispose();
            DbData.DataIsDirty = false;
            return FileState.DataWriteResult;
        }
        // DataInsert
        public StateIs DataInsertThread(DbDataDef DbData)
        {
            Fmain.FileAction.ToDo = FileAction_ToDoIs.Insert;
            FileState.DataInsertResult = DataInsertThread(DbData, DbData.Id);
            return FileState.DataInsertResult;
        }
        public StateIs DataInsertThread(DbDataDef DbData, int IdPassed)
        {
            DateUpdated = DateTime.Now.ToString("O");
            // DbData.DateUpdated = DateTime.Now.ToString("O");
            string TraceMessage = "Insert " + DbData.Id;
            st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
            Fmain.FileAction.ToDo = FileAction_ToDoIs.Insert;
            FileState.DataInsertResult = DbCommandThreadCreate(DbCommandIs.Insert, DbData
                , "INSERT INTO ", sEmpty
                , true, true, st.StdRunControlUi.SetButtons);
            DbData = (DbDataDef)CalculationTaskUtils.WaitGetResult(st, DbCmdThreadId, DbData, true, false); // Can only get the ID (on new) not make other changes.
            FileState.DataInsertResult = (StateIs)CalculationTaskUtils.WaitGetResult(st, DbCmdThreadId, DbData, false, false);
            DbCommandThreadDispose();
            DataIsDirty = false;
            return FileState.DataInsertResult;
        }
        // DataDelete
        public StateIs DataDeleteThread(DbDataDef DbData, bool PassedSetButtons)
        {
            Fmain.FileAction.ToDo = FileAction_ToDoIs.Delete;
            FileState.DataDeleteResult = DataDeleteThread(DbData, DbData.Id, PassedSetButtons);
            return FileState.DataDeleteResult;
        }
        public StateIs DataDeleteThread(DbDataDef DbData, int IdPassed, bool PassedSetButtons)
        {
            DateUpdated = DateTime.Now.ToString("O");
            string TraceMessage = "Delete " + IdPassed.ToString();
            st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
            Fmain.FileAction.ToDo = FileAction_ToDoIs.Delete;
            FileState.DataDeleteResult = DbCommandThreadCreate(DbCommandIs.Delete, DbData
                , "DELETE ", " WHERE Id=" + IdPassed.ToString()
                , false, true, PassedSetButtons);
            FileState.DataDeleteResult = (StateIs)CalculationTaskUtils.WaitGetResult(st, DbCmdThreadId, DbData, false, false);
            //DataDeleteResult = WaitGetResult(st, FileSqlCmdThreadId, ref LinkData, true, false);
            DbCommandThreadDispose();
            return FileState.DataDeleteResult;
        }
        // DataInsert
        #endregion
        #region Populate
        public StateIs DbPopulateThreadCreate(
            object PassedData, string SelectCmdPassed, string SelectArgPassed
            , bool AddRows, bool FirstOnly, bool GetSqlData
            , bool PassedSetButtons, bool UseThreadPassed)
        {
            DbPopulateResult = StateIs.Started;
            DbThreadDef FileSqlThread = new DbThreadDef
            {
                TaskType = "FileSqlPopulate",
                SqlCommandType = DbCommandIs.Select,

                mData = PassedData,
                mFile = this,
                DbCommandPassed = SelectCmdPassed,
                DbCommandArgPassed = SelectArgPassed,
                PostRowData = AddRows,
                FirstOnly = FirstOnly,
                GetSqlData = GetSqlData,
                UseThread = UseThreadPassed
            };

            if (DbTaskIdBusy > 0)
            {
                CalculationTaskUtils.WaitForBusy(st, DbTaskIdBusy, 10000);
                DbTaskIdBusy = -3;
            }
            DbPopulateResult = StateIs.InProgress;
            Sender = FormParent;
            DbPopTask = new DbTaskDef(ref Sender, ref ConsoleSender, ConsoleSource, ClassRole, ClassFeatures);

            DbTaskIdBusy = DbPopTask.Id;
            DbPopThreadId = DbPopTask.Id;
            FileSqlThread.TaskId = DbPopTask.Id;
            FileSqlThread.DbTask = DbPopTask;
            DbPopTask.FileSqlPopThreadId = DbPopThreadId;
            //
            if (MainThreadIdDoSet) { MainThreadId = DbPopThreadId; MainThreadIdDoSet = false; }
            //
            DbPopTask.TaskType = "FileSqlPopulate";
            //DbPopTask.SetButtons = PassedSetButtons;
            DbPopThreadId = DbPopTask.Id;
            FileSqlThread.TaskId = DbPopTask.Id;
            //
            if (DbPopTask.CalculationObjectDel == null)
            {
                // Create a delegate to the calculation method.
                DbPopTask.CalculationObjectDel =
                        new DbTaskDef.CalculationObjectDelegate(DbPopTask.DbPopulateDo);

                // Create a delegate for Data to UI
                DbPopTask.TaskDbDataEvent += new
                    DbTaskDef.TaskDbDataEventHandler(((AppStdStateView)st.FormParent).OnFileSqlTaskDataEvent);

                //// Create a delegate for UI Messages
                //DbPopTask.ConsoleTaskMessageEvent += new
                //    DbTaskDef.ConsoleTaskMessageEventHandler(st.OnTraceMdmDoImpl);

                // Create a delegate to get the SQL data
                DbPopTask.TaskDbGetSqlDel =
                    new DbTaskDef.TaskDbGetSqlDelegate(((iDbData)FileSqlThread.mData).GetSqlData);

                // Create a delegate to set the SQL data
                DbPopTask.TaskDbSetSqlDel =
                    new DbTaskDef.TaskDbSetSqlDelegate(((iDbData)FileSqlThread.mData).SetSqlData);

                // Create a delegate to set the SQL field names in command lines.
                DbPopTask.TaskDbSetSqlDataLineDel =
                    new DbTaskDef.TaskDbSetSqlDataLineDelegate(((iDbData)FileSqlThread.mData).SetSqlDataLine);

                // Subscribe to the calculation status event.
                DbPopTask.CalculationStatusChanged += new
                  DbTaskDef.CalculationStatusEventHandler(((iDbTask)st.FormParent).OnTaskStatusChanged);
                // ToDo Subscribe to the calculation progress event.
                //FileSqlScTask.CalculationProgressChanged += new
                //  FileSqlScTaskDef.CalchulationProgressEventHandler(OnCalculationProgressChanged);
                DbPopulateResult = StateIs.InProgress;
                if (FileSqlThread.UseThread)
                {
                    DbPopTask.StartCalculation((object)this, FileSqlThread, (DbTaskDef.CalculationObjectDelegate)DbPopTask.CalculationObjectDel);
                } else
                {
                    DbPopTask.CalculationObjectDel(this, FileSqlThread);
                    // DbPopulateDo();
                }
                //CrListSqlCommandThread(FileSqlScThread);
            }
            return DbPopulateResult = StateIs.Successful;
        }
        public StateIs DbPopulateThreadDispose()
        {
            DbPopulateDisposeResult = StateIs.Started;
            return DbPopulateThreadDispose(DbPopThreadId);
        }
        public StateIs DbPopulateThreadDispose(int ThreadIdPassed)
        {
            DbPopulateDisposeResult = StateIs.Started;
            string ThreadId = ThreadIdPassed.ToString();
            DbPopulateDisposeResult = StateIs.Started;
            try
            {
                ((CalculationTaskDef)DbTaskDef.CalculationTaskDict[ThreadId]).Dispose();
            }
            catch (Exception) { }
            DbPopTask = null;
            return DbPopulateDisposeResult = StateIs.Successful;
        }
        #endregion
        #region Command
        public StateIs DbCommandThreadCreate(
            DbCommandIs SqlCommandType
            , object PassedData, string SelectCmdPassed, string SelectArgPassed
            , bool GetSqlData
            , bool PassedSetButtons, bool UseThreadPassed)
        {
            DbCommandResult = StateIs.Started;
            DbThreadDef FileSqlThread = new DbThreadDef
            {
                TaskType = "FileSqlCommand",
                SqlCommandType = SqlCommandType,
                UseThread = UseThreadPassed
            };
            string CommandName = Enum.GetName(typeof(DbCommandIs), SqlCommandType);

            FileSqlThread.Id = -1;
            FileSqlThread.ItemId = "";
            FileSqlThread.mData = PassedData;
            FileSqlThread.mFile = this;
            FileSqlThread.DbCommandPassed = SelectCmdPassed;
            FileSqlThread.DbCommandArgPassed = SelectArgPassed;
            FileSqlThread.GetSqlData = GetSqlData;

            if (DbTaskIdBusy > 0)
            {
                CalculationTaskUtils.WaitForBusy(st, DbTaskIdBusy, 10000);
                DbTaskIdBusy = -2;
            }
            Sender = FormParent;
            DbCmdTask = new DbTaskDef(ref Sender, ref ConsoleSender, ConsoleSource, ClassRole, ClassFeatures);
            DbTaskIdBusy = DbCmdTask.Id;
            DbCmdThreadId = DbCmdTask.Id;
            FileSqlThread.TaskId = DbCmdTask.Id;
            DbCmdTask.FileSqlCmdThreadId = DbCmdThreadId;
            if (MainThreadIdDoSet) { MainThreadId = DbCmdThreadId; MainThreadIdDoSet = false; }

            DbCmdTask.TaskType = "FileSqlCommand";
            //DbCmdTask.SetButtons = PassedSetButtons;
            DbCmdThreadId = DbCmdTask.Id;

            if (DbCmdTask.CalculationObjectDel == null)
            {
                // Create a delegate to the calculation method.
                DbCmdTask.CalculationObjectDel =
                    new DbTaskDef.CalculationObjectDelegate(DbCommandDo);

                // Create a delegate for Data to UI
                //FileSqlScTask.FileSqlScTaskDataEvent += new
                //    FileSqlScTaskDef.ScTaskDataEventHandler(OnFileSqlScTaskDataEvent);

                // Create a delegate for UI Messages
                DbCmdTask.ConsoleTaskMessageEvent += new
                    DbTaskDef.ConsoleTaskMessageEventHandler(st.OnTraceMdmDoImpl);

                // Create a delegate to get the SQL data
                DbCmdTask.TaskDbGetSqlDel =
                    new DbTaskDef.TaskDbGetSqlDelegate(((iDbData)FileSqlThread.mData).GetSqlData);

                // Create a delegate to set the SQL data
                DbCmdTask.TaskDbSetSqlDel =
                    new DbTaskDef.TaskDbSetSqlDelegate(((iDbData)FileSqlThread.mData).SetSqlData);

                // Create a delegate to set the SQL command line paramaters for fields
                DbCmdTask.TaskDbSetSqlDataLineDel =
                    new DbTaskDef.TaskDbSetSqlDataLineDelegate(((iDbData)FileSqlThread.mData).SetSqlDataLine);

                // Subscribe to the calculation status event.
                //DbCmdTask.CalculationStatusChanged += new
                //  DbTaskDef.CalculationStatusEventHandler(OnFileSqlTaskStatusChanged);

                // Subscribe to the calculation progress event.
                //FileSqlScTask.CalculationProgressChanged += new
                //  FileSqlScTaskDef.CalculationProgressEventHandler(OnCalculationProgressChanged);

                // Do Calculation
                DbCommandResult = StateIs.InProgress;
                if (FileSqlThread.UseThread)
                {
                    DbCmdTask.StartCalculation(this, FileSqlThread, (DbTaskDef.CalculationObjectDelegate)DbCmdTask.CalculationObjectDel);
                    //CrListSqlCommandThread(FileSqlScThread);
                } else
                {
                    DbCmdTask.CalculationObjectDel(this, FileSqlThread);
                    // DbCommandDo(this, FileSqlThread);
                }
            }
            return DbCommandResult = StateIs.Finished;
        }
        public StateIs DbCommandThreadDispose()
        {
            return DbCommandThreadDispose(DbCmdThreadId);
        }
        public StateIs DbCommandThreadDispose(int ThreadIdPassed)
        {
            DbCommandDisposeResult = StateIs.Started;
            string ThreadId = ThreadIdPassed.ToString();
            DbCommandDisposeResult = StateIs.Started;
            try
            {
                ((CalculationTaskDef)DbTaskDef.CalculationTaskDict[ThreadId]).Dispose();
            } catch (Exception) { }
            DbCmdTask = null;
            return DbCommandDisposeResult = StateIs.Successful;
        }
        public object DbCommandDo(object sender, object FileSqlThreadPassed)
        {
            DbCommandThreadResult = StateIs.Started;
            #region Initialization
            if (FileSqlThreadPassed is DbThreadDef)
            {
                if (FileSqlThreadPassed == null) { return StateIs.Failed; }
            }
            else { return StateIs.Failed; }
            DbThreadDef FileSqlThread = (DbThreadDef)FileSqlThreadPassed;
            // Prepare Result Object and other fields
            ObjectResultDef ObjectResult = new ObjectResultDef();
            //
            DbCommandThreadResult = StateIs.InProgress;
            FileSqlThread.DbTask.StartCalculateWaitLock();
            //
            string setCommand = sEmpty;
            FileSqlThread.mFile.FileSqlConn.DbSyn.DatabaseCmdType = FileSqlThread.SqlCommandType;
            FileSqlThread.mFile.Fmain.DbIo.CommandCurrent = sEmpty;
            FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputInsertCommand = sEmpty;
            // Set or load the passed command verb
            FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputWriteCommand = FileSqlThread.DbCommandPassed; // SelectCmdPassed;
            #endregion
            // Command Type
            switch (FileSqlThread.SqlCommandType)
            {
                case (DbCommandIs.Delete):
                    // Action to perform
                    FileSqlThread.mFile.Fmain.FileAction.ToDo = FileAction_ToDoIs.Delete;
                    // command function
                    setCommand = FileSqlThread.DbTask.TaskDbSetSqlDataLineDel(FileSqlThread);
                    break;
                case (DbCommandIs.Insert):
                    FileSqlThread.mFile.Fmain.FileAction.ToDo = FileAction_ToDoIs.Insert;
                    setCommand += "; SELECT CAST(scope_identity() AS int)";
                    break;
                case (DbCommandIs.Update):
                    FileSqlThread.mFile.Fmain.FileAction.ToDo = FileAction_ToDoIs.Update;
                    setCommand = FileSqlThread.DbTask.TaskDbSetSqlDataLineDel(FileSqlThread);
                    FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputWriteCommand = "UPDATE ";
                    break;
                default:
                    setCommand = FileSqlThread.DbTask.TaskDbSetSqlDataLineDel(FileSqlThread);
                    break;
            }
            #region command
            FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputWriteCommand +=
                    FileSqlThread.mFile.Fmain.Fs.FileId.FileName;
            // "ShortcutData";
            // FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputWriteCommand += "ShortcutData";
            #region Build Command
            switch (FileSqlThread.SqlCommandType)
            {
                case DbCommandIs.Delete:
                    break;
                case DbCommandIs.Update:
                    setCommand = FileSqlThread.DbTask.TaskDbSetSqlDataLineDel(FileSqlThread);
                    break;
                case DbCommandIs.Insert:
                    setCommand = FileSqlThread.DbTask.TaskDbSetSqlDataLineDel(FileSqlThread);
                    setCommand += "; SELECT CAST(scope_identity() AS int)";
                    break;
                default:
                    setCommand = FileSqlThread.DbTask.TaskDbSetSqlDataLineDel(FileSqlThread);
                    break;
            }
            FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputWriteCommand += setCommand;
            #endregion
            //
            if (FileSqlThread.DbCommandArgPassed.Length > 0)
            {
                FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputWriteCommand += " " + FileSqlThread.DbCommandArgPassed;
            }
            #endregion
            if (FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputWriteCommand.Length > 0)
            {
                FileSqlThread.mFile.Fmain.DbIo.CommandCurrent = FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputWriteCommand;
                // Create the command.
                DbCommandThreadResult = FileSqlThread.mFile.SqlDataCommandCreate(ref FileSqlThread.mFile.Fmain);
                if (st.StateIsSuccessfulAll(DbCommandThreadResult))
                {
                    if (FileSqlThread.GetSqlData)
                    {
                        // Set SQL parameters to accomodate Where clauses...
                        // ((IDbTaskDataSql)FileSqlThread.Data).SetSqlData(FileSqlThread.Data, FileSqlThread.mFile);
                        FileSqlThread.DbTask.TaskDbSetSqlDel(FileSqlThread.mData, FileSqlThread.mFile);
                    }
                    //
                    //FileSqlThread.mFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@Id", SqlDbType.Int, 4);
                    //FileSqlThread.mFile.Fmain.DbIo.SqlDbCommand.Parameters["@Id"].Direction = ParameterDirection.Output;

                    // Execute the command
                    DbCommandThreadResult = FileSqlThread.mFile.SqlDataCommandExecute(ref FileSqlThread.mFile.Fmain);

                    if (st.StateIsSuccessfulAll(DbCommandThreadResult))
                    {
                        DbCommandThreadResult = StateIs.Successful;
                        FileSqlThread.Id = FileSqlThread.mFile.Fmain.Item.ItemPrimaryKey;
                        ObjectResult.Id = FileSqlThread.mFile.Fmain.Item.ItemPrimaryKey;
                        ObjectResult.NewRecord = FileSqlThread.mFile.Fmain.Item.NewRecord;
                    }
                    else
                    {
                        // ToDo errror
                        DbCommandThreadResult = StateIs.Failed;
                        ObjectResult.NewRecord = false;
                    }
                }
                else
                {
                    // ToDo errror
                    DbCommandThreadResult = StateIs.Failed;
                }
            }
            ObjectResult.StdResultLong = DbCommandThreadResult;
            return ObjectResult;
        }
        #endregion
        #region DbFile Conn Open / Close
        public StateIs DbConnCloseThread(bool PassedSetButtons, bool UseThreadPassed)
        {
            DbConnCloseResult = StateIs.Started;
            DbThreadDef FileSqlScOpenThread = new DbThreadDef
            {
                TaskType = "FileSqlCommand",
                // FileSqlScOpenThread.SqlCommandType = SqlCommandType;
                // FileSqlScOpenThread.LinkData = LinkData;
                mFile = this,
                UseThread = false
            };
            Sender = FormParent;
            DbConnCloseTask = new DbTaskDef(ref Sender, ref ConsoleSender, ConsoleSource, ClassRole, ClassFeatures)
            {
                TaskType = "FileSqlCommand",
                //st.StdBaseRunControlUi.SetButtons = PassedSetButtons
            };
            DbConnOpenThreadId = DbConnOpenTask.Id;
            return DbConnCloseThreadDo(FileSqlScOpenThread);
        }
        public StateIs DbConnCloseThreadCreate(bool PassedSetButtons)
        {
            DbConnCloseResult = StateIs.Started;
            #region Guards
            DbThreadDef FileSqlScCloseThread = new DbThreadDef
            {
                TaskType = "FileSqlCommand",
                // FileSqlScCloseThread.SqlCommandType = SqlCommandType;
                // FileSqlScCloseThread.LinkData = LinkData;
                // FileSqlScCloseThread.SelectClosePassed = SelectClosePassed;
                // FileSqlScCloseThread.SelectArgPassed = SelectArgPassed;
                // FileSqlScCloseThread.GetSqlData = GetSqlData;
                mFile = this,
                UseThread = true
            };

            Sender = FormParent;
            DbConnOpenTask = new DbTaskDef(ref Sender, ref ConsoleSender, ConsoleSource, ClassRole, ClassFeatures)
            {
                TaskType = "FileSqlCommand",
                //st.StdBaseRunControlUi.SetButtons = PassedSetButtons
            };
            #endregion
            DbConnCloseThreadId = DbConnCloseTask.Id;

            // Subscribe to the calculation status event.
            //DbConnOpenTask.CalculationStatusChanged += new
            //  DbTaskDef.CalculationStatusEventHandler(OnFileSqlTaskStatusChanged);

            // Subscribe to the calculation progress event.
            //FileSqlScTask.CalculationProgressChanged += new
            //  FileSqlScTaskDef.CalculationProgressEventHandler(OnCalculationProgressChanged);

            // Create a delegate to the calculation method.
            DbConnCloseTaskDel =
                    new DbTaskDef.CalculationLongDelegate(FileSqlScCloseThread.mFile.DbConnCloseThreadDo);

            // Create a delegate for Data to UI
            //FileSqlScTask.FileSqlScTaskDataEvent += new
            //    FileSqlScTaskDef.ScTaskDataEventHandler(FileSqlScCloseThread.mFile.OnFileSqlScTaskDataEvent);

            // Create a delegate for UI Messages
            DbConnCloseTask.ConsoleTaskMessageEvent += new
                DbTaskDef.ConsoleTaskMessageEventHandler(st.OnTraceMdmDoImpl);

            // Create a delegate to get the SQL data
            // FileSqlScCloseGetSqlDel =
            // new FileSqlScTaskDef.FileSqlScGetSqlDelegate(LinkDataDef.GetSqlData);

            // Create a delegate to set the SQL data
            // FileSqlScCloseSetSqlDel =
            // new FileSqlScTaskDef.FileSqlScSetSqlDelegate(LinkDataDef.SetSqlData);

            // Create a delegate to set the SQL command line paramaters for fields
            // FileSqlScCloseSetSqlDataLineDel =
            // new FileSqlScTaskDef.FileSqlScSetSqlDataLineDelegate(LinkData.SetSqlDataLine);

            // Do Calculation
            DbConnCloseResult = StateIs.InProgress;
            DbConnCloseTask.StartCalculation(FileSqlScCloseThread, (DbTaskDef.CalculationLongDelegate)DbConnCloseTaskDel);
            //CrListSqlCommandThread(FileSqlScThread);

            return DbConnCloseThreadResult = StateIs.Finished;
            // return DbConnCloseThread(ref PassedmFile);
        }
        public StateIs DbConnCloseThreadCreateB()
        {
            DbConnCloseThreadResult = StateIs.Started;
            //
            // File Action 
            Fmain.FileAction.Direction = Fmain.Fs.Direction;
            Fmain.FileAction.IoMode = Fmain.Fs.IoMode;
            Fmain.FileAction.ToDo = FileAction_ToDoIs.Close;
            Fmain.FileAction.KeyName = "Table";
            //Fmain.FileAction.ToDo = FileAction_DoIs.Table;
            Fmain.FileAction.FileReadMode = FileAction_ReadModeIs.Database;
            Fmain.FileAction.DoRetry = false;
            Fmain.FileAction.DoClearTarget = false;
            Fmain.FileAction.DoGetUiVs = false;
            //
            DbConnCloseThreadResult = StateIs.InProgress;
            DbConnCloseThreadResult = FileDo(ref Fmain);
            //
            if (st.StateIsSuccessfulAll(DbConnCloseThreadResult))
            {
            }
            return DbConnCloseThreadResult;
        }
        public StateIs DbConnCloseThreadDo(object PassedFileSqlThread)
        {
            DbConnCloseThreadDoResult = StateIs.Started;
            if (PassedFileSqlThread is DbThreadDef)
            {
                if (PassedFileSqlThread == null) { return DbConnCloseThreadDoResult = StateIs.Failed; }
            }
            else { return DbConnCloseThreadDoResult = StateIs.Failed; }

            DbThreadDef FileSqlThread = (DbThreadDef)PassedFileSqlThread;
            mFileSqlConnectionDef LocalmFile = FileSqlThread.mFile;
            //
            DbConnCloseThreadDoResult = StateIs.InProgress;
            FileSqlThread.DbTask.StartCalculateWaitLock();
            // File Action 
            LocalmFile.Fmain.FileAction.Direction = Fmain.Fs.Direction;
            LocalmFile.Fmain.FileAction.IoMode = Fmain.Fs.IoMode;
            LocalmFile.Fmain.FileAction.ToDo = FileAction_ToDoIs.Close;
            LocalmFile.Fmain.FileAction.KeyName = "Table";
            LocalmFile.Fmain.FileAction.FileReadMode = FileAction_ReadModeIs.Database;
            LocalmFile.Fmain.FileAction.DoRetry = false;
            LocalmFile.Fmain.FileAction.DoClearTarget = false;
            LocalmFile.Fmain.FileAction.DoGetUiVs = false;
            //
            DbConnCloseThreadDoResult = LocalmFile.FileDo(ref Fmain);
            if (st.StateIsSuccessfulAll(DbConnCloseThreadDoResult))
            {
            }
            return DbConnCloseThreadDoResult;
        }
        public StateIs DbConnOpenThread(bool PassedSetButtons)
        {
            DbConnOpenResult = StateIs.Started;
            DbThreadDef FileSqlScOpenThread = new DbThreadDef
            {
                TaskType = "FileSqlCommand",
                // FileSqlScOpenThread.SqlCommandType = SqlCommandType;
                // FileSqlScOpenThread.LinkData = LinkData;
                mFile = this
            };
            Sender = FormParent;
            DbConnOpenTask = new DbTaskDef(ref Sender, ref ConsoleSender, ConsoleSource, ClassRole, ClassFeatures)
            {
                TaskType = "FileSqlCommand",
                //st.StdBaseRunControlUi.SetButtons = PassedSetButtons
            };
            DbConnOpenThreadId = DbConnOpenTask.Id;
            return DbConnOpenThreadDo(FileSqlScOpenThread);
        }
        public StateIs DbConnOpenThreadCreate(bool PassedSetButtons)
        {
            DbConnOpenResult = StateIs.Started;
            DbThreadDef FileSqlScOpenThread = new DbThreadDef
            {
                TaskType = "FileSqlCommand",
                mFile = this
            };

            Sender = FormParent;
            DbConnOpenTask = new DbTaskDef(ref Sender, ref ConsoleSender, ConsoleSource, ClassRole, ClassFeatures)
            {
                TaskType = "FileSqlCommand",
                //st.StdBaseRunControlUi.SetButtons = PassedSetButtons,
                ThreadObject = FileSqlScOpenThread
            };
            DbConnOpenThreadId = DbConnOpenTask.Id;
            FileSqlScOpenThread.DbTask = DbConnOpenTask;

            if (FormParent is AppStd)
            {
                // Subscribe to the calculation status event.
                DbConnOpenTask.CalculationStatusChanged += new
                  DbTaskDef.CalculationStatusEventHandler(((iDbTask)FormParent).OnTaskStatusChanged);
            }
            else
            {
                // Subscribe to the calculation status event.
                DbConnOpenTask.CalculationStatusChanged += new
                  DbTaskDef.CalculationStatusEventHandler(OnTaskStatusChanged);
            }
            // Subscribe to the calculation progress event.
            //FileSqlScTask.CalculationProgressChanged += new
            //  FileSqlScTaskDef.CalculationProgressEventHandler(OnCalculationProgressChanged);

            // Create a delegate to the calculation method.
            DbConnOpenTaskDel =
               new DbTaskDef.CalculationLongDelegate(DbConnOpenThreadDo);

            // Create a delegate for Data to UI
            //DbConnOpenTask.FileSqlScTaskDataEvent += new
            //    FileSqlScTaskDef.ScTaskDataEventHandler(OnFileSqlScTaskDataEvent);

            // Create a delegate for UI Messages
            DbConnOpenTask.ConsoleTaskMessageEvent += new
                DbTaskDef.ConsoleTaskMessageEventHandler(st.OnTraceMdmDoImpl);

            // Create a delegate to get the SQL data
            // FileSqlScOpenGetSqlDel =
            // new FileSqlScTaskDef.FileSqlScGetSqlDelegate(DbDataDef.GetSqlData);

            // Create a delegate to set the SQL data
            // FileSqlScOpenSetSqlDel =
            // new FileSqlScTaskDef.FileSqlScSetSqlDelegate(DbDataDef.SetSqlData);

            // Create a delegate to set the SQL command line paramaters for fields
            // FileSqlScOpenSetSqlDataLineDel =
            // new FileSqlScTaskDef.FileSqlScSetSqlDataLineDelegate(LinkData.SetSqlDataLine);

            // Do Calculation
            DbConnOpenResult = StateIs.InProgress;
            DbConnOpenTask.StartCalculation(FileSqlScOpenThread, (DbTaskDef.CalculationLongDelegate)DbConnOpenTaskDel);
            //CrListSqlCommandThread(FileSqlScThread);

            return DbConnOpenThreadResult = StateIs.Finished;
            // return DbConnOpenThread(ref PassedmFile);
        }
        public StateIs DbConnOpenThreadDo(object PassedFileSqlThread)
        {
            DbConnOpenThreadDoResult = StateIs.Started;
            if (PassedFileSqlThread is DbThreadDef)
            {
                if (PassedFileSqlThread == null) { return DbConnOpenThreadDoResult = StateIs.Failed; }
            }
            else { return DbConnOpenThreadDoResult = StateIs.Failed; }
            DbThreadDef FileSqlThread = (DbThreadDef)PassedFileSqlThread;
            //
            DbConnOpenThreadDoResult = StateIs.InProgress;
            FileSqlThread.DbTask.StartCalculateWaitLock();
            // Update the calculation status.
            FileSqlThread.DbTask.CalcState = CalculationStatus.Calculating;
            // Fire a status changed event. RunControlUi.SetButtons
            // FileSqlThread.DbTask.FireStatusChangedEvent(FileSqlThread.DbTask.CalcState, FileSqlThread.DbTask.SetButtons);
            FileSqlThread.DbTask.FireStatusChangedEvent(FileSqlThread.DbTask.CalcState, st.StdRunControlUi.SetButtons);
            // File Action
            FileSqlThread.mFile.Fmain.FileAction.ToDo = FileAction_ToDoIs.Open;
            FileSqlThread.mFile.FileSqlConn.DbSyn.DatabaseCmdType = DbCommandIs.None;

            mFileSqlConnectionDef LocalmFile = FileSqlThread.mFile;
            // Create new task object to manage the calculation.
            // Thread Task Creation

            // File Summary
            if (LocalmFile.Fmain.Fs.FileId.FileName.Length == 0)
            {
                // ControlReplaceFile.Fmain.Fs.
                LocalmFile.Fmain.Fs.IoMode = FileIo_ModeIs.Sql;
                LocalmFile.Fmain.Fs.Direction = FileAction_DirectionIs.Both;
                // ControlReplaceFile.Fmain.Fs.ServerName = "MDMPC13\\SQLEXPRESS"; 
                // ControlReplaceFile.Fmain.Fs.ServerName = "MDMPC13\\MSSQLSERVER";
                LocalmFile.Fmain.Fs.ServerName = "(localdb)\\MSSQLLocalDB";
                // LocalmFile.Fmain.Fs.ServerName = ".\\MSSQLLocalDB";
                // LocalmFile.Fmain.Fs.ServerName = ".\\SQLEXPRESS";
                LocalmFile.Fmain.Fs.DatabaseName = "MdmShortcutData";
                // File Id
                LocalmFile.Fmain.Fs.FileId.FileName = "ReplaceData";
                LocalmFile.Fmain.Fs.FileId.PropSystemPath =
                    @"H:\Data\MdmData99\ShortcutData\Vs1\ShortcutData.mdf";
                // StdBaseDef.DriveOs + @"\System\Clipboard\ReplaceData";
                LocalmFile.Fmain.Fs.FileId.FileNameSetFromLine(@"H:\Data\MdmData99\ShortcutData\Vs1\ShortcutData.mdf");
                // ControlReplaceFile.Fmain.Fs.FileId.FileNameSetFromLine(StdBaseDef.DriveOs + @"\System\ShortcutData\ReplaceData");
                // Options
                LocalmFile.Fmain.Fs.FileOpt.DoCreateFileDoesNotExist = true;
                LocalmFile.Fmain.ConnStatus.DoKeepConn = false;
                LocalmFile.Fmain.FileStatus.DoKeepConn = true;
            }
            // File Action 
            LocalmFile.Fmain.FileAction.Direction = LocalmFile.Fmain.Fs.Direction;
            LocalmFile.Fmain.FileAction.IoMode = LocalmFile.Fmain.Fs.IoMode;
            LocalmFile.Fmain.FileAction.ToDo = FileAction_ToDoIs.Open;
            LocalmFile.Fmain.FileAction.KeyName = "Table";
            LocalmFile.Fmain.FileAction.FileReadMode = FileAction_ReadModeIs.Database;
            LocalmFile.Fmain.FileAction.DoRetry = false;
            LocalmFile.Fmain.FileAction.DoClearTarget = false;
            LocalmFile.Fmain.FileAction.DoGetUiVs = false;
            //
            DbConnOpenThreadDoResult = LocalmFile.FileDo(this);
            //
            if (st.StateIsSuccessfulAll(DbConnOpenThreadDoResult)) { }
            return DbConnOpenThreadDoResult;
        }
        #endregion
        #region Status Change / Data Sent
        public void OnTaskStatusChanged(object sender, object CalculationArgsPassed)
        {
            CalculationEventArgs CalculationArgs = CalculationArgsPassed as CalculationEventArgs;
            if (CalculationArgs == null) { return; } // ToDo
            DbTaskDef DbTaskLocal;
            if (sender is DbTaskDef)
            {
                if (sender == null)
                {
                    return;
                }
                DbTaskLocal = (DbTaskDef)sender;
            }
            else
            {
                return;
            }
            string LocalMessage = "UI Status change on ThreadId: ";
            LocalMessage += DbTaskLocal.Id.ToString();
            LocalMessage += " (" + DbTaskLocal.TaskType + ")";

            switch (CalculationArgs.Status)
            {
                case CalculationStatus.PausePending:
                    DbTaskLocal.st.StdRunControlUi.ButtonPause.Text = "Wait";
                    DbTaskLocal.st.StdRunControlUi.ButtonCancel.Enabled = false;
                    LocalMessage += " Pause is Pending";
                    break;
                case CalculationStatus.Paused:
                    DbTaskLocal.st.StdRunControlUi.ButtonPause.Text = "Resume";
                    LocalMessage += " Paused";
                    break;
                case CalculationStatus.ResumePending:
                    DbTaskLocal.st.StdRunControlUi.ButtonPause.Text = "Wait";
                    DbTaskLocal.st.StdRunControlUi.ButtonCancel.Enabled = false;
                    LocalMessage += " Resume is Pending";
                    break;
                case CalculationStatus.Pending:
                    //if (!DbTaskLocal.WaitIsBusy)
                    //{
                    //    if (DbTaskLocal.WaitLockMutex == null) { DbTaskLocal.WaitLockMutex = new Mutex(); }
                    //    DbTaskLocal.WaitLockMutex.WaitOne();
                    //    DbTaskLocal.WaitIsBusy = true;
                    //}
                    if (CalculationArgs.SetButtons)
                    {
                        DbTaskLocal.st.ButtonDisable();
                        DbTaskLocal.st.StdRunControlUi.ButtonPause.Text = "Pause";
                        DbTaskLocal.st.StdRunControlUi.LabelDbBusyMessage.Text = " Busy please wait...";
                        DbTaskLocal.st.StdRunControlUi.ButtonPause.Invalidate();
                    }
                    LocalMessage += " Calculation is Pending";
                    break;

                case CalculationStatus.Calculating:
                    //if (!DbTaskLocal.WaitIsBusy)
                    //{
                    //    if (DbTaskLocal.WaitLockMutex == null) { DbTaskLocal.WaitLockMutex = new Mutex(); }
                    //    DbTaskLocal.WaitLockMutex.WaitOne();
                    //    DbTaskLocal.WaitIsBusy = true;
                    //}
                    if (CalculationArgs.SetButtons)
                    {
                        DbTaskLocal.st.ButtonDisable();
                    }
                    LocalMessage += " Calculating";
                    break;

                case CalculationStatus.NotCalculating:
                    // check LongResult
                    if (st.StateIsSuccessfulAll(DbTaskLocal.Status))
                    {
                    }
                    else { }
                    // if successful:
                    if (CalculationArgs.SetButtons)
                    {

                        DbTaskLocal.st.StdRunControlUi.ButtonPause.Text = "Completed";
                        DbTaskLocal.st.StdRunControlUi.LabelDbBusyMessage.Text = sEmpty;
                        DbTaskLocal.st.StdRunControlUi.ButtonPause.Invalidate();
                        // 202012 dgh end - Cleanup functions
                    }
                    //if (DbTaskLocal.WaitIsBusy)
                    //{
                    //    DbTaskLocal.WaitLockMutex.ReleaseMutex();
                    //    DbTaskLocal.WaitIsBusy = false;
                    //}
                    LocalMessage += " Finished Calculating";
                    break;

                case CalculationStatus.CancelPending:
                    if (CalculationArgs.SetButtons)
                    {
                        DbTaskLocal.st.ButtonDisable();
                    }
                    LocalMessage += " Cancel is Pending";
                    break;
            }
            st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 13);
        }
        public void OnTaskProgressChanged(object sender, CalculationEventArgs e)
        {
            // This should execute on the UI thread...
            if (e.FileProgress == 0 && e.DirectoryProgress == 0) { return; }
            //CrListLabelDirectoryCount.Text = e.DirectoryProgress.ToString();
            //CrListLabelDirectoryCount.Invalidate();
            //CrListLabelFileCount.Text = e.FileProgress.ToString();
            //CrListLabelFileCount.Invalidate();
        }
        public virtual void OnTaskDataEvent(Object sender, object ePassed)
        {
            bool Error = false;
            bool Skip = false;
            DbThreadDef FileSqlThread;
            DbTaskDef DbTaskLocal;
            DbDataDef DbData;
            DataGridView CrListUsed = null;
            DbDataEventArgs e = ePassed as DbDataEventArgs;
            DbData = e.DbData.ObjectData as DbDataDef;
            string[] rowString;
            // ToDo Try....
            if (e.DbData.ObjectData is iDbData)
            {
                DbData = e.DbData.ObjectData as DbDataDef;
                //
                FileSqlThread = e.DbData.DbThread as DbThreadDef;
                // ToDo DbData.DbTask is null
                DbTaskLocal = FileSqlThread.DbTask as DbTaskDef;
                DbTaskLocal.OnTaskDataEvent(sender, e);
                //((iDbTask)e.DbData.DbTask).OnTaskDataEvent(sender, e);
                // OnScriptTaskDataEvent(sender, (DbDataEventArgs)e);
                return;
            }

            if (e.DbData == null) { Error = true; }
            else
            {
                if (e.DbData.ObjectData == null || !(e.DbData.ObjectData is iDbData)) 
                { 
                    Error = true; 
                }
                else
                {
                    DbData = e.DbData.ObjectData as DbDataDef;
                    // Skip s/b false;
                    // insert code here <<
                    if (!Skip)
                    {
                        rowString = ((iDbData)e.DbData.ObjectData).SetRowData();
                        // string[] rowString = DbData.SetRowData();
                        DataGridView DbListView = ((iDbData)e.DbData.ObjectData).GridViewGet();
                        // Error = DbListViewStateGridsDef.SetDataGridView(ref ScListView, DbData.DataStatus);
                        if (!Error)
                        {
                            CrListUsed.Rows.Add(rowString);
                            CrListUsed.FirstDisplayedScrollingRowIndex = CrListUsed.RowCount - 1;
                        }
                        else
                        {
                            // Data or List error
                            if (CrListUsed != null)
                            {
                                CrListUsed.Rows.Add(rowString);
                                CrListUsed.FirstDisplayedScrollingRowIndex = CrListUsed.RowCount - 1;
                            }
                        }
                    }
                }
            }
        }
        public virtual StateIs FileSqlGetResult(int ThreadIdPassed, ref DbDataDef Data, bool GetData)
        {
            FileSqlGetResultResult = StateIs.Undefined;
            string ThreadId = ThreadIdPassed.ToString();
            if (((CalculationTaskDef)DbTaskDef.CalculationTaskDict[ThreadId]).WaitLockMutex == null)
            {
                ((CalculationTaskDef)DbTaskDef.CalculationTaskDict[ThreadId]).WaitLockMutex = new Mutex();
            }
            try
            {
                CalculationTaskUtils.WaitForBusy(st, ThreadIdPassed, 30000);
                // ((CalculationTaskDef)DbTaskDef.CalculationTaskDict[ThreadId]).WaitForBusy(ThreadIdPassed, 30000);

                ((CalculationTaskDef)DbTaskDef.CalculationTaskDict[ThreadId]).WaitLockMutex.WaitOne();

                lock (DbTaskDef.CalculationTaskDict[ThreadId])
                {

                    FileSqlGetResultResult = (StateIs)((CalculationTaskDef)DbTaskDef.CalculationTaskDict[ThreadId]).StdResultLong;
                    if (GetData)
                    {
                        Data = ((CalculationTaskDef)DbTaskDef.CalculationTaskDict[ThreadId]).ObjectResult as DbDataDef;
                        if (Data == null) 
                        {
                            //return (FileSqlGetResultResult = StateIs.NotSet);
                            Data = new DbDataDef();
                        }
                    }

                    if (NewRecord) { Data.Id = Fmain.Item.ItemPrimaryKey; }
                }
                ((CalculationTaskDef)DbTaskDef.CalculationTaskDict[ThreadId]).WaitLockMutex.ReleaseMutex();

            }
            catch (Exception e)
            {

                // throw;
            }
            return (FileSqlGetResultResult = StateIs.Finished);
        }
        #endregion
    }
}
