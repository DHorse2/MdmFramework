#region Dependencies
#region System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion
#region System Data & SQL
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
#endregion
#region  System Threading
using System.Threading;
//using System.Threading.Tasks;
#endregion
#region System Reflection
using System.Reflection;
using System.Timers;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
#endregion
#region System Drawing & Windows Forms & Controls
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
// using System.Windows.Controls;
#endregion
#region System Security
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
#endregion
#region  Mdm Core
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;

using Mdm.Oss.Components;
using Mdm.World;
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
// using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion
namespace Mdm.Oss.File.Db.Data
{
    public class DbTaskDef : ConsoleTaskDef, iDbTask
    {

        #region Thread
        public int FileSqlCmdThreadId;
        public int FileSqlPopThreadId;
        public StateIs DbPopulateThreadResult;
        #endregion
        #region Fields
        public new StdConsoleManagerDef st;
        // Directory and File
        public string FileOpenName;
        public string DirectoryOpenName;
        public bool FirstRead;
        public bool cont;
        // DataGridViews Used by forms and processing.
        public Form ParentForm;
        public DbListViewStateGridsDef DbListViews;
        public DbDataDef DbDataEmpty;
        #endregion
        #region Delegates
        public delegate object TaskDbGetSqlDelegate(object Data, mFileSqlConnectionDef mFileSqlConn);
        public TaskDbGetSqlDelegate TaskDbGetSqlDel;
        public delegate StateIs TaskDbSetSqlDelegate(object Data, mFileSqlConnectionDef mFileSqlConn);
        public TaskDbSetSqlDelegate TaskDbSetSqlDel;
        public delegate string TaskDbSetSqlDataLineDelegate(DbThreadDef FileSqlThread);
        public TaskDbSetSqlDataLineDelegate TaskDbSetSqlDataLineDel;
        #region Events
        public delegate void TaskDbDataEventHandler(
            object sender, DbDataEventArgs e);
        public virtual event TaskDbDataEventHandler TaskDbDataEvent;
        #endregion
        #endregion
        #region Constructor and Clear
        public DbTaskDef(ref object FormParentPassed, ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed) 
             : base(ref FormParentPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed) 
        {
            st = base.st as StdConsoleManagerDef;
            FormParent = FormParentPassed as iStdBaseForm;
            if (FormParent != null && FormParent is AppStdStateView)
            {
                DbListViews = ((AppStdStateView)FormParent).DbListViewGetTo(ref DbListViews);
            } else
            {
                // error ToDo
            }
            // Subscribe to the calculation status event.
            //CalculationStatusChanged += new
            //  CalculationStatusEventHandler(OnTaskStatusChanged);

            // Subscribe to the calculation progress event.
            //CalculationProgressChanged += new
            //  CalculationProgressEventHandler(OnCalculationProgressChanged);

            // Create a delegate to the calculation method.
            //DbTaskDoDel =
            //        new CalculationLongDelegate(DbConnOpenThreadDo);

            // Create a delegate for Data to UI
            // TaskDbDataEvent = null;
            //TaskDbDataEvent += new
            //    TaskDbDataEventHandler(OnTaskDataEvent);

            // Create a delegate for UI Messages
            ConsoleTaskMessageEvent += new
                ConsoleTaskMessageEventHandler(((StdConsoleManagerDef)st).OnTraceMdmDoImpl);

            //// Create a delegate to get the SQL data
            //TaskDbGetSqlDel =
            // new TaskDbGetSqlDelegate(DbDataEmpty.GetSqlData);

            //// Create a delegate to set the SQL data
            //TaskDbSetSqlDel =
            //new TaskDbSetSqlDelegate(DbDataEmpty.SetSqlData);

            //// Create a delegate to set the SQL command line paramaters for fields
            //TaskDbSetSqlDataLineDel =
            //new TaskDbSetSqlDataLineDelegate(DbDataEmpty.SetSqlDataLine);
        }
        public void ClearDataEvent()
        {
            foreach (Delegate LocalDel in TaskDbDataEvent.GetInvocationList())
            {
                TaskDbDataEvent -= (TaskDbDataEventHandler)LocalDel;
            }
        }
        #endregion
        #region Db Populate from database
        public object DbPopulateDo(object sender, object FileSqlThreadPassed)
        {
            DbPopulateThreadResult = StateIs.Started;
            // ToDo change to standardized form.
            if (FileSqlThreadPassed == null) { return StateIs.Failed; }
            if (FileSqlThreadPassed is DbThreadDef)
            {
            }
            else { return StateIs.Failed; }
            DbThreadDef FileSqlThread = (DbThreadDef)FileSqlThreadPassed;
            // Update the calculation status.
            CalcState = CalculationStatus.Calculating;
            // Lock Mutex
            DbCommandThreadResult = StateIs.InProgress;
            FileSqlThread.DbTask.StartCalculateWaitLock();
            // Fire a status changed event.
            FireStatusChangedEvent(CalcState, st.StdRunControlUi.SetButtons);
            // Prepare Result Object
            ObjectResultDef ObjectResult = new ObjectResultDef();
            //LinkDataThreadPop = new LinkDataDef();
            FirstRead = true;
            FileSqlThread.mFile.Fmain.FileAction.ToDo = FileAction_ToDoIs.Read;
            FileSqlThread.mFile.FileSqlConn.DbSyn.DatabaseCmdType = DbCommandIs.Select;
            #region Build SQL Select Command (spOutputReadCommand)
            FileSqlThread.mFile.Fmain.DbIo.CommandCurrent = sEmpty;
            FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputInsertCommand = sEmpty;
            // command
            // DbSyn.spOutputItem += ColText;
            // INSERT into X (
            if (FileSqlThread.DbCommandPassed.Length == 0)
            {
                FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputReadCommand = "SELECT *";
                FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputReadCommand += " FROM ";
                FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputReadCommand += "[";
                if (FileSqlThread.mFile.Fmain.Fs.FileId.FileName.Length > 0)
                {
                    FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputReadCommand +=
                    FileSqlThread.mFile.Fmain.Fs.FileId.FileName;
                }
                else
                {
                    // default: exception:
                    FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputReadCommand += "ShortcutData";
                }
                FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputReadCommand += "]";
            }
            else
            {
                FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputReadCommand = FileSqlThread.DbCommandPassed; // SelectCmdPassed;
            }
            //
            if (FileSqlThread.DbCommandArgPassed.Length > 0)
            {
                FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputReadCommand += FileSqlThread.DbCommandArgPassed;
            }
            #endregion
            // ScConnOpen(FileSqlThread.mFile);
            FileSqlThread.mFile.Fmain.DbIo.CommandCurrent = FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputReadCommand;
            DbPopulateThreadResult = FileSqlThread.mFile.SqlDataCommandCreate();
            if (st.StateIsSuccessfulAll(DbPopulateThreadResult))
            {
                if (FileSqlThread.GetSqlData)
                {
                    ((iDbData)FileSqlThread.mData).SetSqlData(FileSqlThread.mData, FileSqlThread.mFile);
                    // LinkDataDef.SetSqlData(LinkDataThreadPop, FileSqlThread.mFile);
                }
                //if (FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputReadCommand.Length > 0)
                //{
                //    ScPopulateThreadResult =
                //    FileSqlThread.mFile.SqlDataReadOpenCommand(ref FileSqlThread.mFile.Fmain, FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputReadCommand);
                //}
                //else
                //{
                DbPopulateThreadResult =
                    FileSqlThread.mFile.SqlDataReadOpen(ref FileSqlThread.mFile.Fmain);
                //}
                //
                if (st.StateIsSuccessfulAll(DbPopulateThreadResult))
                {
                    cont = true;
                    while (cont)
                    {
                        try
                        {
                            // Check for cancel.
                            if (((CalculationTaskDef)DbTaskDef.CalculationTaskDict[FileSqlThread.TaskId.ToString()]).CalcState == CalculationStatus.CancelPending) break;
                            //
                            DbPopulateThreadResult = FileSqlThread.mFile.SqlDataReadNext(ref FileSqlThread.mFile.Fmain);
                            if (!st.StateIsSuccessfulAll(DbPopulateThreadResult))
                            {
                                cont = false;
                            }
                            else
                            {
                                // string mvalue = FileSqlThread.mFile.Fmain.DbIo.SqlDbReader["Id"].ToString();
                                // ToDo Console out this Id
                            }
                            // CrListIdTemp = (int)FileSqlThread.mFile.Fmain.DbIo.SqlDbReader["Id"];
                            //
                        }
                        catch (Exception e)
                        {
                            cont = false;
                        }
                        if (cont)
                        {
                            // LinkDataThreadPop.Clear();
                            FileSqlThread.mData = TaskDbGetSqlDel(null, FileSqlThread.mFile);
                            // FileSqlThread.mData = TaskDbGetSqlDel(FileSqlThread.mData, FileSqlThread.mFile);
                            ObjectResult.ObjectResult = FileSqlThread.mData;
                            //
                            if (FileSqlThread.PostRowData)
                            {
                                // Fire data event...
                                DbDataDef FileSqlPopDbData = new DbDataDef(FileSqlThread.mData, FileSqlThread);
                                // ((DbTaskDef)DbTaskDef.CalculationTaskDict[FileSqlThread.TaskId.ToString()]).FireFileSqlScTaskDataEvent(FileSqlScPopData);
                                FireTaskDbDataEvent(FileSqlPopDbData);

                                // CrListRowAdd(ref LinkData, ref CrList);

                                //RowNumberCount = ScList.Rows.Count - 1;
                                //if (CrListIdTemp > CrListIdCurrent) { CrListIdCurrent = CrListIdTemp; }
                            }
                            FirstRead = false;
                        }
                        if (FileSqlThread.FirstOnly) { cont = false; }
                    }
                    FileSqlThread.mFile.SqlDataReadClose(ref FileSqlThread.mFile.Fmain);
                }
                else
                {
                    string TraceMessage = "Populate reader open failed!";
                    st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.ErrorLog, 13);
                }
            }
            else
            {
                string TraceMessage = "Populate create command failed!";
                st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.ErrorLog, 13);
            }
            if (FirstRead)
            {
                // error or new record...
                FileSqlThread.mFile.Fmain.FileAction.Result = StateIs.DoesNotExist;
                NewRecord = true;
            }
            else
            {
                FileSqlThread.mFile.Fmain.FileAction.Result = StateIs.DoesExist;
                NewRecord = false;
            }
            ObjectResult.NewRecord = NewRecord;
            ObjectResult.ItemId = ItemId;
            ObjectResult.StdResultLong = DbPopulateThreadResult;
            return ObjectResult;
        }
        public StateIs DbPopulateDispose()
        {
            StateIs DbPopulateDisposeResult = StateIs.Started;
            ((CalculationTaskDef)DbTaskDef.CalculationTaskDict[FileSqlPopThreadId.ToString()]).Dispose();
            if (DbTaskIdBusy == FileSqlPopThreadId) { DbTaskIdBusy = -1; }
            return DbPopulateDisposeResult = StateIs.Successful;
        }
        #endregion
        #region Db Command
        #region File Sql Thread Management
        private bool RefreshIsBusy = false;
        public bool DataInsertSkipRowEnter = false;
        public EventArgs DataInsertSkipRowEnterArgs = null;
        [ThreadStatic]
        public DbThreadDef ThreadObject;
        [ThreadStatic]
        public int DbCmdThreadId;
        public DbTaskDef DbCmdTask;
        public DbTaskDef.CalculationObjectDelegate DbCmdTaskDel;
        public DbTaskDef.TaskDbGetSqlDelegate DbCmdGetSqlDel;
        public DbTaskDef.TaskDbSetSqlDelegate DbCmdSetSqlDel;
        public DbTaskDef.TaskDbSetSqlDataLineDelegate DbCmdSetSqlDataLineDel;
        StateIs DbCommandThreadResult;
        // public volatile int DbTaskIdBusyValue;
        //public int DbTaskIdBusy
        //{
        //    get
        //    {
        //        return DbTaskIdBusyValue;
        //    }

        //    set
        //    {
        //        if (st != null)
        //        {
        //            string TraceMessage = "Calculation Task DbTaskIdBusy (" + DbTaskIdBusyValue.ToString() + ") now set to " + value.ToString();
        //            st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
        //        }
        //        DbTaskIdBusyValue = value;
        //    }
        //}
        #endregion
        #region Db Command Functions
        StateIs DbCommandResult;
        StateIs DbCommandDisposeResult;
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
            //
            // Prepare Result Object
            //
            ObjectResultDef ObjectResult = new ObjectResultDef();

            string setCommand = sEmpty;
            //
            FileSqlThread.mFile.FileSqlConn.DbSyn.DatabaseCmdType = FileSqlThread.SqlCommandType;
            //
            FileSqlThread.mFile.Fmain.DbIo.CommandCurrent = sEmpty;
            FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputInsertCommand = sEmpty;
            // DbSyn.spOutputItem += ColText;
            // INSERT into X (
            // Set or load the passed command verb
            FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputWriteCommand = FileSqlThread.DbCommandPassed; // SelectCmdPassed;
            #endregion
            //
            switch (FileSqlThread.SqlCommandType)
            {
                case (DbCommandIs.Delete):
                    // Action to perform
                    FileSqlThread.mFile.Fmain.FileAction.ToDo = FileAction_ToDoIs.Delete;
                    // command function
                    setCommand = TaskDbSetSqlDataLineDel(FileSqlThread);
                    break;
                case (DbCommandIs.Insert):
                    FileSqlThread.mFile.Fmain.FileAction.ToDo = FileAction_ToDoIs.Insert;
                    setCommand += "; SELECT CAST(scope_identity() AS int)";
                    break;
                case (DbCommandIs.Update):
                    FileSqlThread.mFile.Fmain.FileAction.ToDo = FileAction_ToDoIs.Update;
                    setCommand = TaskDbSetSqlDataLineDel(FileSqlThread);
                    FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputWriteCommand = "UPDATE ";
                    break;
                default:
                    setCommand = TaskDbSetSqlDataLineDel(FileSqlThread);
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
                    setCommand = TaskDbSetSqlDataLineDel(FileSqlThread);
                    break;
                case DbCommandIs.Insert:
                    setCommand = TaskDbSetSqlDataLineDel(FileSqlThread);
                    setCommand += "; SELECT CAST(scope_identity() AS int)";
                    break;
                default:
                    setCommand = TaskDbSetSqlDataLineDel(FileSqlThread);
                    break;
            }
            FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputWriteCommand += setCommand;
            #endregion
            //
            if (FileSqlThread.DbCommandArgPassed.Length > 0)
            {
                FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputWriteCommand += " " + FileSqlThread.DbCommandArgPassed;
            }
            //
            //CrListConnOpen();
            //
            #endregion
            if (FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputWriteCommand.Length > 0)
            {
                FileSqlThread.mFile.Fmain.DbIo.CommandCurrent = FileSqlThread.mFile.FileSqlConn.DbSyn.spOutputWriteCommand;
                // Create the command.
                DbCommandThreadResult = FileSqlThread.mFile.SqlDataCommandCreate(ref FileSqlThread.mFile.Fmain);
                //

                if (st.StateIsSuccessfulAll(DbCommandThreadResult))
                {
                    if (FileSqlThread.GetSqlData)
                    {
                        // Set SQL parameters to accomodate Where clauses...
                        // ((IDbTaskDataSql)FileSqlThread.Data).SetSqlData(FileSqlThread.Data, FileSqlThread.mFile);
                        TaskDbSetSqlDel(FileSqlThread.mData, FileSqlThread.mFile);
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
        public StateIs DbCommandDispose()
        {
            DbCommandDisposeResult = StateIs.Started;
            // FileSqlCmdThreadId
            DbCommandDisposeResult = DbCommandDispose(FileSqlCmdThreadId);
            if (DbTaskIdBusy == FileSqlCmdThreadId) { DbTaskIdBusy = -1; }
            return DbCommandDisposeResult;
        }
        public StateIs DbCommandDispose(int ThreadIdPassed)
        {
            string ThreadId = ThreadIdPassed.ToString();
            DbCommandDisposeResult = StateIs.Started;
            ((CalculationTaskDef)DbTaskDef.CalculationTaskDict[ThreadId]).Dispose();
            return DbCommandDisposeResult = StateIs.Successful;
        }
        public virtual StateIs DbCommandCreateThread(ref mFileSqlConnectionDef PassedmFile, DbCommandIs SqlCommandType, object PassedData, string SelectCmdPassed, string SelectArgPassed, bool GetSqlData, bool PassedSetButtons)
        {
            DbCommandResult = StateIs.Started;
            DbThreadDef FileSqlThread = new DbThreadDef
            {
                TaskType = "FileSqlCommand",
                SqlCommandType = SqlCommandType
            };
            string temp = Enum.GetName(typeof(DbCommandIs), SqlCommandType);

            FileSqlThread.Id = -1;
            FileSqlThread.ItemId = "";
            FileSqlThread.mData = PassedData;
            FileSqlThread.mFile = PassedmFile;
            FileSqlThread.DbCommandPassed = SelectCmdPassed;
            FileSqlThread.DbCommandArgPassed = SelectArgPassed;
            FileSqlThread.GetSqlData = GetSqlData;

            if (DbTaskIdBusy > 0)
            {
                CalculationTaskUtils.WaitForBusy(st, DbTaskIdBusy, 10000);
                DbTaskIdBusy = -2;
            }
            Sender = FormParent;
            DbCmdTask = new DbTaskDef(ref Sender, ref ConsoleSender, st.ConsoleSource, st.ClassRole, st.ClassFeatures);
            DbTaskIdBusy = DbCmdTask.Id;
            DbCmdThreadId = DbCmdTask.Id;
            FileSqlThread.TaskId = DbCmdTask.Id;
            DbCmdTask.FileSqlCmdThreadId = DbCmdThreadId;
            if (MainThreadIdDoSet) { MainThreadId = DbCmdThreadId; MainThreadIdDoSet = false; }

            DbCmdTask.TaskType = "FileSqlCommand";
            //DbCmdTask.SetButtons = false;
            //DbCmdTask.SetButtons = st.RunControlUi.SetButtons;
            //DbCmdTask.SetButtons = PassedSetButtons;
            DbCmdThreadId = DbCmdTask.Id;

            if (DbCmdTask.CalculationObjectDel == null)
            {
                // Create a delegate to the calculation method.
                DbCmdTask.CalculationObjectDel =
                    new DbTaskDef.CalculationObjectDelegate(DbCmdTask.DbCommandDo);

                // Create a delegate for Data to UI
                //DbCmdTask.FileSqlScTaskDataEvent += new
                //    DbTaskDef.DbTaskDataEventHandler(OnFileSqlDbTaskDataEvent);

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
                DbCmdTask.CalculationStatusChanged += new
                    //DbTaskDef.CalculationStatusEventHandler(OnTaskStatusChanged);
                    DbTaskDef.CalculationStatusEventHandler(((iDbTask)st.FormParent).OnTaskStatusChanged);

                // Subscribe to the calculation progress event.
                //FileSqlScTask.CalculationProgressChanged += new
                //  FileSqlScTaskDef.CalculationProgressEventHandler(OnCalculationProgressChanged);

                // Do Calculation
                DbCommandResult = StateIs.InProgress;
                DbCmdTask.StartCalculation(this, FileSqlThread, (DbTaskDef.CalculationObjectDelegate)DbCmdTask.CalculationObjectDel);
                //CrListSqlCommandThread(FileSqlScThread);
            }
            return DbCommandResult = StateIs.Finished;
        }
        #endregion
        #endregion
        #region Event Targets
        public virtual void OnTaskStatusChanged(object sender, object CalculationArgsPassed)
        {
            // I think this can be called from the UI thread. ToDo
            CalculationEventArgs CalculationArgs = CalculationArgsPassed as CalculationEventArgs;
            if (CalculationArgs == null) { return; } // ToDo. 
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

            //if (DbTaskLocal.WaitLockMutex == null) { DbTaskLocal.WaitLockMutex = new Mutex(); }
            if (!(FormParent is AppStd)) 
            { 
                CalculationArgs.SetButtons = false; 
            }
            //base.OnTaskStatusChanged(sender, CalculationArgsPassed);
            switch (CalculationArgs.Status)
            {
                case CalculationStatus.PausePending:
                    st.StdRunControlUi.ButtonPause.Text = "Wait";
                    st.StdRunControlUi.ButtonCancel.Enabled = false;
                    LocalMessage += " Pause is Pending";
                    break;
                case CalculationStatus.Paused:
                    st.StdRunControlUi.ButtonPause.Text = "Resume";
                    LocalMessage += " Paused";
                    break;
                case CalculationStatus.ResumePending:
                    st.StdRunControlUi.ButtonPause.Text = "Wait";
                    st.StdRunControlUi.ButtonCancel.Enabled = false;
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
                        ((AppStd)FormParent).ButtonDisable();
                        st.StdRunControlUi.ButtonPause.Text = "Pause";
                        st.StdRunControlUi.LabelDbBusyMessage.Text = " Busy please wait...";
                        st.StdRunControlUi.ButtonPause.Invalidate();
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
                        if (FormParent is AppStd)
                        {
                            ((AppStd)FormParent).ButtonDisable();
                        }
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
                        ((AppStd)FormParent).ButtonEnable();
                        st.StdRunControlUi.ButtonPause.Text = "Completed";
                        st.StdRunControlUi.LabelDbBusyMessage.Text = sEmpty;
                        st.StdRunControlUi.ButtonPause.Invalidate();
                        // 202012 dgh end - Cleanup functions
                    }
                    //if (DbTaskLocal.WaitIsBusy)
                    //{
                    //    if (DbTaskLocal.WaitLockMutex == null) 
                    //    {
                    //        // ERROR
                    //        DbTaskLocal.WaitLockMutex = new Mutex(); 
                    //    }
                    //    DbTaskLocal.WaitLockMutex.ReleaseMutex();
                    //    DbTaskLocal.WaitIsBusy = false;
                    //}
                    LocalMessage += " Finished Calculating";
                    break;

                case CalculationStatus.CancelPending:
                    if (CalculationArgs.SetButtons)
                    {
                        ((AppStd)FormParent).ButtonDisable();
                        st.StdRunControlUi.ButtonPause.Enabled = false;
                        st.StdRunControlUi.ButtonPause.Text = "Cancelling";
                    }
                    LocalMessage += " Cancel is Pending";
                    break;
            }
            st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 13);
        }
        StateIs OnTaskDataEventResult;
        public virtual void OnTaskDataEvent(object sender, object DbDataEventObject)
        {
            // The worker thread passes the row this way.
            // IE DB ops are multithreaded in the background.
            // Note: Lazy loading is also used where possible.
            //       The framework enables, disables and 
            //       hides controls (buttons) as needed.
            // It is a overly complicated. Good luck!
            OnTaskDataEventResult = StateIs.Started;
            bool Error = false;
            bool Skip = false;
            // Validate sending object
            DbTaskDef DbTaskLocal = DbTaskDef.TaskDbValidate(sender);
            if (DbTaskLocal == null)
            {
                Error = true; // NO???? 
                              // ToDo Throw invalid data warning.
                              // return; // NO???? 
            }
            DbDataEventArgs DbDataArgs; // = ScriptDataEventObject as ScriptDataEventArgs;
            DbDataDef DbData;
            DataGridView DbListUsed;
            // Valid DbTask
            string LocalMessage = "UI Data event on Script Thread.";
            DbTaskDef.TaskDbDataEventMessage(sender, DbTaskLocal, LocalMessage, Error);
            // Lock Mutex
            //if (DbTaskLocal.WaitLockMutex == null) { DbTaskLocal.WaitLockMutex = new Mutex(); }
            // Update CrList (always).
            DbDataArgs = DbDataEventObject as DbDataEventArgs;
            if (((DbDataEventArgs)DbDataArgs).DbData == null) { Error = true; }
            else
            {
                DbData = ((DbDataEventArgs)DbDataArgs).DbData as DbDataDef;
                if (DbData == null) { Error = true; }
                else
                {
                    // Skip s/b false;
                    // insert code here <<
                    if (!Skip)
                    {
                        // Valid Event Data
                        // This should execute on the UI thread...
                        DbListUsed = null;
                        Error = DbTaskLocal.DbListViews.SetDataGridView(ref DbTaskLocal.DbListViews, DbTaskLocal.DataStatus);
                        DbListUsed = DbTaskLocal.DbListViews.DbList.StdRunControlUi.GridView;
                        Skip = false;
                        // Insert Case statements here <<
                        if (!Skip && !Error)
                        {
                            // Display Db Line
                            string[] rowString = ((iDbData)DbData.ObjectData).SetRowData();
                            Error = DbTaskLocal.DbListViews.SetDataGridView(ref DbTaskLocal.DbListViews, DbTaskLocal.DataStatus);
                            if (!Error)
                            {
                                DbListUsed.Rows.Add(rowString);
                                DbListUsed.FirstDisplayedScrollingRowIndex = DbListUsed.RowCount - 1;
                            }
                            else
                            {
                                // Data or List error
                                if (DbListUsed != null)
                                {
                                    DbListUsed.Rows.Add(rowString);
                                    DbListUsed.FirstDisplayedScrollingRowIndex = DbListUsed.RowCount - 1;
                                }
                            }
                        }
                    }
                }
            }
            if (Error)
            {
                // ToDo Throw invalid data warning.
                OnTaskDataEventResult = StateIs.DatabaseError;
                return;
            }
            if (Skip)
            {
                // Do nothing?
            }
            OnTaskDataEventResult = StateIs.Successful;
            return;
        }
        public virtual void OnTaskProgressChanged(object sender, CalculationEventArgs e)
        {
            // This should execute on the UI thread...
            if (e.FileProgress == 0 && e.DirectoryProgress == 0) { return; }
            //base.OnTaskProgressChanged(sender, e)
            st.StdRunControlUi.LabelDbDirectoryCount.Text = e.DirectoryProgress.ToString();
            st.StdRunControlUi.LabelDbDirectoryCount.Invalidate();
            st.StdRunControlUi.LabelDbFileCount.Text = e.FileProgress.ToString();
            st.StdRunControlUi.LabelDbFileCount.Invalidate();
            //CrListLabelDirectoryCount.Text = e.DirectoryProgress.ToString();
            //CrListLabelDirectoryCount.Invalidate();
            //CrListLabelFileCount.Text = e.FileProgress.ToString();
            //CrListLabelFileCount.Invalidate();
        }

        #region Dead Event Code
        public virtual void OnTaskDataEventXXX(Object sender, Object DbDataEventObject)
        {
            OnTaskDataEventResult = StateIs.Started;
            bool Error = false;
            bool Skip = false;
            // Validate sending object exists and is a task
            DbTaskDef DbTaskLocal = DbTaskDef.TaskDbValidate(sender);
            if (DbTaskLocal == null)
            {
                Error = true; // NO???? 
                // ToDo Throw invalid data warning.
                // return; // NO???? 
            }

            // Load the data.
            DbDataEventArgs DbDataArgs; // = DbDataEventObject as DbDataEventArgs;
            DbDataDef DbData;
            DataGridView DbListUsed;
            string[] rowString;

            string LocalMessage = "UI Data event from (" + sender.ToString() + ") on Db Thread.";
            DbTaskDef.TaskDbDataEventMessage(sender, DbTaskLocal, LocalMessage, Error);
            // Lock Mutex
            if (DbTaskLocal.WaitLockMutex == null) { DbTaskLocal.WaitLockMutex = new Mutex(); }

            // Update data to gridview CrList.
            // The data has been passed in the arguments.
            DbDataArgs = DbDataEventObject as DbDataEventArgs;
            // Validate
            if (DbDataArgs == null) { Error = true; }
            else
            {
                DbData = DbDataArgs.DbData as DbDataDef;
                if (DbData == null || DbData.ObjectData == null) { Error = true; }
                else
                {
                    if (DbData.ObjectData.GetType() != typeof(iDbData)) { Error = true; }
                    else
                    {
                        // Valid. Load the row data.
                        // This (might?) should execute on the UI thread...
                        // Insert code here << Skip if false here.
                        if (!Skip)
                        {
                            // Display Db Line
                            rowString = ((iDbData)DbData.ObjectData).SetRowData();
                            // Note: that the dataGridViews returned
                            // originated in this program.
                            // However the list below might not be visible.
                            DbListUsed = new DataGridView();
                            // Error = DbListViews.SetDataGridView(ref CrListUsed, ref ((iDbTaskDataSql)DbData.ObjectData).GetStatus());
                            Error = DbTaskLocal.DbListViews.SetDataGridView(ref DbTaskLocal.DbListViews, DbTaskLocal.DataStatus);
                            if (!Error)
                            {
                                if (DbListUsed.Name.StartsWith("Action"))
                                {
                                    DbTaskLocal.DbListViews.Actioned.StdRunControlUi.GridView.Rows.Add(rowString);
                                    DbTaskLocal.DbListViews.Actioned.StdRunControlUi.GridView.FirstDisplayedScrollingRowIndex = DbListUsed.RowCount - 1;
                                }
                                DbListUsed.Rows.Add(rowString);
                                DbListUsed.FirstDisplayedScrollingRowIndex = DbListUsed.RowCount - 1;
                            }
                            else
                            {
                                Skip = true;
                                // This should be pointing at a hidden error list.
                                // There is not separate Skipped list (Valid used.)
                                DbListUsed.Rows.Add(rowString);
                                DbListUsed.FirstDisplayedScrollingRowIndex = DbListUsed.RowCount - 1;
                                // Do nothing, the error is handled by the function.
                            }
                        }
                        else
                        {
                            // Do nothing with skipped rows.
                            // You could output a high verbosity message.
                        }
                    }
                }
            }
            if (Error)
            {
                OnTaskDataEventResult = StateIs.Failed;
                // ToDo Throw invalid data warning.
            }
        }
        public void OnTaskDbDataEventZZZ(Object sender, DbDataEventArgs e)
        {
            bool Error = false;
            bool Skip = false;
            DbTaskDef DbTaskLocal;
            DbDataDef DbData;
            DataGridView DbListUsed = null;

            //if (e.DbData.ObjectData is DbDataDef)
            //{
            //    OnDbTaskDataEvent(sender, e);
            //    // OnDbTaskDataEvent(sender, (DbDataEventArgs)e);
            //    return;
            //}
            //else if (e.DbData.ObjectData is LinkDataDef)
            //{
            //    OnShortcutTaskDataEvent(sender, e);
            //    // OnShortcutTaskDataEvent(sender, (ShortcutDataEventArgs)e);
            //    return;
            //}
            if (e.DbData == null) { Error = true; }
            else
            {
                DbData = e.DbData.ObjectData as DbDataDef;
                if (DbData == null) { Error = true; }
                else
                {
                    // Skip s/b false;
                    // insert code here <<
                    if (!Skip)
                    {
                        string[] rowString = DbData.SetRowData();
                        Error = DbListViews.SetDataGridView(ref DbListViews, DbData.DataStatus);
                        if (!Error)
                        {
                            DbListUsed.Rows.Add(rowString);
                            DbListUsed.FirstDisplayedScrollingRowIndex = DbListUsed.RowCount - 1;
                        }
                        else
                        {
                            // Data or List error
                            if (DbListUsed != null)
                            {
                                DbListUsed.Rows.Add(rowString);
                                DbListUsed.FirstDisplayedScrollingRowIndex = DbListUsed.RowCount - 1;
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #endregion
        public virtual void FireTaskDbDataEvent(object FileSqlDbDataPassed)
        {
            bool Error = false;
            bool Skip = false;
            DbDataDef FileSqlDbData = FileSqlDbDataPassed as DbDataDef;
            if (FileSqlDbData == null) { Error = true; } 
            else 
            {
                if (TaskDbDataEvent == null) { Error = true; }
                else
                {
                    {
                        DbDataEventArgs DbDataArgs = new DbDataEventArgs(FileSqlDbData);
                        if (TaskDbDataEvent.Target is
                                System.Windows.Forms.Control)
                        {
                            System.Windows.Forms.Control targetForm = TaskDbDataEvent.Target
                                    as System.Windows.Forms.Control;
                            targetForm.Invoke(TaskDbDataEvent,
                                    new object[] { this, DbDataArgs });
                        }
                        else if (TaskDbDataEvent.Target is
                                    Mdm.Oss.Std.AppStd)
                        {
                            System.Windows.Forms.Control targetForm = ((AppStd)TaskDbDataEvent.Target).FormParentGet()
                                    as System.Windows.Forms.Control;
                                    //as Mdm.Oss.Std.AppStd;
                            targetForm.Invoke(TaskDbDataEvent,
                                    new object[] { this, DbDataArgs });
                        }
                        else
                        {
                            // Hail Mary. Doesn't work.
                            //TaskDbDataEvent(this, DbDataArgs);

                            MethodInfo targetMethod = TaskDbDataEvent.Method;
                            targetMethod.Invoke(TaskDbDataEvent,
                                    new object[] { this, DbDataArgs });
                            //FileSqlTaskMessageEvent(this, args);
                        }
                    }
                }
                if (Error)
                {
                    // ???? ToDo DbTask Event Error!
                    if (FileSqlDbData == null)
                    {

                    }
                    if (TaskDbDataEvent == null) {
                        // ???? ToDo Programming error - No DbTask Event Error!
                    }
                }
                System.Threading.Thread.Sleep(0);
                //System.Threading.Thread.Yield();
            }
        }
        public static void TaskDbDataEventMessage(Object sender, DbTaskDef DbTaskLocal, string LocalMessage, bool Error)
        {
            if (LocalMessage.Length > 0)
            {
                LocalMessage += "\n";
            }
            LocalMessage += " Id: " + DbTaskLocal.Id.ToString();
            LocalMessage += " Type: (" + DbTaskLocal.TaskType + ")";
            LocalMessage += " Valid: (" + Error.ToString() + ")";
        }
        public DbTaskDef FileSqlTask;
        public static DbTaskDef TaskDbValidate(Object sender)
        {
            bool Error = false;
            DbTaskDef DbTaskLocal = null;
            // Validate sending object
            if (sender is DbTaskDef)
            {
                if (sender == null)
                {
                    Error = true;
                }
                else
                {
                    // ref DbTaskDef DbTaskLocal;
                    // ref DbTaskDef DbTaskLocal = ref (DbTaskDef)sender;
                    // DbTaskLocal = (DbTaskDef)sender;
                    DbTaskLocal = sender as DbTaskDef;
                    return DbTaskLocal;
                }
            }
            else
            {
                Error = true;
            }
            if (Error)
            {
                // ToDo Throw invalid data warning.
                // return null;
                // DbTaskLocal = new DbTaskDef();
                DbTaskLocal = null;
            }
            return DbTaskLocal;
        }
    }
}
