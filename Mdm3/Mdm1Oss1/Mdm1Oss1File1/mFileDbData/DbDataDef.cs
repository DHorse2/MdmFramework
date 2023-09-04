#region Dependencies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Reflection;
#region System Data and SQL
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
#endregion

using System.Threading;
//using System.Threading.Tasks;

using System.Windows.Forms;
using System.Windows.Controls;

#region  Mdm Core
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Mobj;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Std;
using Mdm.Oss.Components;
#endregion
#region  Mdm Db and File
using Mdm.Oss.File;
using Mdm.Oss.File.Control;
using Mdm.Oss.File.Db;
using Mdm.Oss.File.Db.Data;
using Mdm.Oss.File.Db.Table;
using Mdm.Oss.File.Db.Thread;
// using Mdm.Oss.File.Properties;
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
// using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
// using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion

namespace Mdm.Oss.File.Db.Data
{
    public class DbDataDef : iDbData, iDbRunData
    {
        public StateIs SetSqlDataResult;
        public StateIs OnTaskDataEventResult;

        public Object ObjectData;
        public DbTaskDef DbTask;
        public DbThreadDef DbThread;
        #region Fields
        public int Id { get; set; }
        public string FileName;
        public string DataType;
        public string DataTypeMinor;
        public DataStatusIs DataStatus;
        public StateIs Status;
        public string FieldUsed;
        // Rules for state change are 
        // new() is dirty
        // Read is clean
        // Load from SQL is clean
        // Write is clean
        // Enternal UI datagridview edits set dirty. (Cell value change)
        // Anything that alter the fields set dirty.
        // Could be properties with the sets controlling it too.  Not good
        public bool Found;
        public bool Valid;
        public string Value;
        public string DateUpdated;
        public bool DataIsDirty;
        #endregion
        protected const string sUnknown = "unknown";
        protected const string sEmpty = "";
        protected const int iUnknown = 99999;
        #region Threads
        [ThreadStatic]
        public static string setCommand;
        [ThreadStatic]
        public static string setCommandPrefix;
        [ThreadStatic]
        public static string setCommandSuffix;
        [ThreadStatic]
        public static string setFieldList;
        [ThreadStatic]
        public static string setFieldListPrefix;
        [ThreadStatic]
        public static string setFieldListSuffix;
        #endregion
        public virtual DataStatusIs GetStatus()
        {
            return DataStatus;
        }
        public virtual StateIs SetObject(object ObjectDataPassed)
        {
            ObjectData = ObjectDataPassed;
            return StateIs.Finished;
        }
        public virtual StateIs SetDbTask(DbTaskDef DbTaskPassed)
        {
            DbTask = DbTaskPassed;
            return StateIs.Finished;
        }
        public virtual StateIs SetId(int PassedId)
        {
            throw new NotImplementedException();
            return 0;
        }
        public virtual void Clear()
        {
            Id = -1;
            FileName = sEmpty;
            FieldUsed = sEmpty; // "Target Path"
            Found = false;
            Valid = false;
            Value = null;
            DateUpdated = sEmpty;
            DataIsDirty = true;
            Reset();
        }
        public virtual void Reset()
        {
        }
        public DbDataDef() { Clear(); }
        public DbDataDef(object PassedObjectData, DbThreadDef DbThreadPassed)
        {
            ObjectData = PassedObjectData;
            DbThread = DbThreadPassed;
        }
        #region DataGridView for Rows
        public DataGridView GridView;
        public virtual ref DataGridView GridViewGet()
        {
            return ref GridView;
        }
        public virtual StateIs GridViewSetFrom(ref DataGridView GridViewPassed)
        {
            GridView = GridViewPassed;
            if (GridView == null)
            {
                return StateIs.DoesNotExist;
            } else
            {
                return StateIs.DoesExist;
            }
        }
        public virtual ref DataGridViewRow SetDataGridViewRow(ref DataGridViewRow rowGrid)
        {
            rowGrid = new DataGridViewRow();
            // rowGrid.Cells.Add(new DataGridViewCell(); // [0]
            rowGrid.Cells[0].Value = Id.ToString();
            return ref rowGrid;
        }
        public virtual string[] SetRowData()
        {
            string[] rowString = new string[1];
            rowString[0] = "ERROR row data not implemented."; // ToDo throw warning message.
            return rowString;
        }
        #endregion
        #region SQL
        public virtual object GetSqlData(object PassedData, mFileSqlConnectionDef ScriptFile)
        {
            DbDataDef LocalData;
            LocalData = PassedData as DbDataDef;
            if (LocalData == null) { LocalData = new DbDataDef(); }

            LocalData.Id = (int)ScriptFile.Fmain.DbIo.SqlDbReader["Id"];
            // if (LinkData.Id > RowNumberCurrent) { RowNumberCurrent = LinkData.Id; }

            try
            {
                LocalData.FileName = (string)ScriptFile.Fmain.DbIo.SqlDbReader["FileName"];
            }
            catch (Exception e)
            {
                LocalData.FileName = null;
            }

            try
            {
                LocalData.Value = (string)ScriptFile.Fmain.DbIo.SqlDbReader["Value"]; ;
            }
            catch (Exception e)
            {
                LocalData.Value = null;
            }
            try
            {
                object tmp = (object)ScriptFile.Fmain.DbIo.SqlDbReader["DateUpdated"];
                LocalData.DateUpdated = tmp.ToString();
            }
            catch (Exception e)
            {
                LocalData.DateUpdated = sEmpty;
            }
            //LocalData.DateUpdated = DateTime.Now.ToString("O");

            LocalData.Found = false;
            LocalData.Valid = false;
            LocalData.DataIsDirty = false;
            return LocalData;
        }
        public virtual StateIs SetSqlData(object PassedData, mFileSqlConnectionDef ScriptFile)
        {
            DbDataDef LocalData;
            LocalData = PassedData as DbDataDef;
            if (LocalData == null) { return StateIs.Undefined; }

            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@IdValue", SqlDbType.Int);
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters["@IdValue"].Value = LocalData.Id;

            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@FileNameValue", SqlDbType.VarChar);
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters["@FileNameValue"].Value = LocalData.FileName;

            //ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@ValueValue", SqlDbType.VarChar);
            //ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters["@ValueValue"].Value = LocalData.Value;
      
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@DateUpdatedValue", SqlDbType.VarChar);
            LocalData.DateUpdated = DateTime.Now.ToString("O");
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters["@DateUpdatedValue"].Value = LocalData.DateUpdated;
            return 0;
        }
        public void SetSqlDataLinePrefix(object FileSqlThreadPassed)
        {
            DbThreadDef FileSqlThread = FileSqlThreadPassed as DbThreadDef;
            setCommandPrefix = sEmpty; setFieldListPrefix = sEmpty;
            if (FileSqlThread.SqlCommandType == DbCommandIs.Update)
            {
                setCommand += " SET ";
            }
            else
            {
                setCommand += " VALUES (";
                setFieldList += "(";
            }
            return;
        }
        public virtual string SetSqlDataLine(DbThreadDef FileSqlThread)
        {
            string setCommand = sEmpty;
            string setFieldList = sEmpty;
            SetSqlDataLineSuffix(FileSqlThread);
            setCommand += setCommandPrefix;
            setFieldList += setFieldListPrefix;
            //if (FileSqlThread.SqlCommandType != DbCommandIs.Update 
            //    // && FileSqlThread.SqlCommandType != (int)SqlCommandIs.Insert
            //    )
            //{
            //    if (FileSqlThread.SqlCommandType == DbCommandIs.Update)
            //    {
            //        setCommand += @"Id";
            //        setCommand += @" = ";
            //    }
            //    setCommand += @"@IdValue";
            //    setCommand += @", ";
            //    setFieldList += @"Id";
            //    setFieldList += @", ";
            //}
            if (FileSqlThread.SqlCommandType == DbCommandIs.Update)
            {
                setCommand += @"FileName";
                setCommand += @" = ";
            }
            setCommand += @"@FileNameValue";
            setCommand += @", ";
            setFieldList += @"FileName";
            setFieldList += @", ";

            if (FileSqlThread.SqlCommandType == DbCommandIs.Update)
            {
                setCommand += @"Value";
                setCommand += @" = ";
            }
            setCommand += @"@ValueValue";
            setCommand += @", ";
            setFieldList += @"Value";
            setFieldList += @", ";

            if (FileSqlThread.SqlCommandType == DbCommandIs.Update)
            {
                setCommand += @"DateUpdated";
                setCommand += @" = ";
            }
            setCommand += @"@DateUpdatedValue";
            setFieldList += @"DateUpdated";
            SetSqlDataLineSuffix(FileSqlThread);
            setCommand += setCommandSuffix;
            setFieldList += setFieldListSuffix;

            return setCommand;
        }
        public void SetSqlDataLineSuffix(DbThreadDef FileSqlThread)
        {
            setCommandSuffix = sEmpty; setFieldListSuffix = sEmpty;
            if (FileSqlThread.SqlCommandType == DbCommandIs.Update)
            {
                // setCommand += sEmpty;
            }
            else
            {
                setCommandPrefix += @")";
                setFieldList += @")";
                setCommandPrefix = setFieldList + setCommandPrefix;
            }
        }
        #endregion
        #region Get Fields
        public virtual string GetFieldValue(String FieldName, object ScriptObject)
        {
            // If (null) ?
            DbDataDef PassedScript = (DbDataDef)ScriptObject;
            switch (FieldName)
            {
                case "DataType":
                    return PassedScript.DataType;
                    break;
                case "DataTypeMinor":
                    return PassedScript.DataTypeMinor;
                    break;
                case "Id":
                    return PassedScript.Id.ToString();
                    break;
                case "FileName":
                    return PassedScript.FileName;
                    break;
                case "Value":
                    return PassedScript.Value;
                    break;
                case "DateUpdated":
                    return PassedScript.DateUpdated;
                    break;
                case "DataIsDirty":
                    return PassedScript.DataIsDirty.ToString();
                    break;
                default:
                    return "ERROR";
                    break;
            }
            return "ERROR";
        }
        public virtual object GetFieldData(String FieldName, object ScriptObject)
        {
            DbDataDef PassedScript = (DbDataDef)ScriptObject;
            switch (FieldName)
            {
                case "DataType":
                    return PassedScript.DataType;
                    break;
                case "DataTypeMinor":
                    return PassedScript.DataTypeMinor;
                    break;
                case "Id":
                    return PassedScript.Id.ToString();
                    break;
                case "FileName":
                    return PassedScript.FileName;
                    break;
                case "Value":
                    return PassedScript.Value;
                    break;
                case "DateUpdated":
                    return PassedScript.DateUpdated;
                    break;
                case "DataIsDirty":
                    return PassedScript.DataIsDirty;
                    break;
                default:
                    return "ERROR";
                    break;
            }
            return "ERROR";
        }
        public virtual string GetFieldName(object FieldObject)
        {
            return nameof(FieldObject);
        }
        public static void GetAppendedFieldNameList(ref List<string> FieldNameList, ref string FieldNameListText)
        {
            GetAppendedFieldNameListStatic(ref FieldNameList, ref FieldNameListText);
        }
        public static void GetAppendedFieldNameListStatic(ref List<string> FieldNameList, ref string FieldNameListText)
        {
            // List<string> FieldList = new List<string>;
            // FieldNameList.Add( ;
            FieldNameList.Add("DataType");
            FieldNameList.Add("DataTypeMinor");
            FieldNameList.Add("Id");
            FieldNameList.Add("FileName");
            FieldNameList.Add("Value");
            FieldNameList.Add("DateUpdated");
            FieldNameList.Add("DataIsDirty");
            FieldNameList.Add("ERROR");

            FieldNameListText += "DataType, ";
            FieldNameListText += "DataTypeMinor, ";
            FieldNameListText += "Id, ";
            FieldNameListText += "FileName, ";
            FieldNameListText += "Value, ";
            FieldNameListText += "DateUpdated, ";
            FieldNameListText += "DataIsDirty, ";
            FieldNameListText += "ERROR, ";
            return;
        }
        #endregion
        // Used by delegate TaskDbDataEventHandler
        public virtual void OnTaskDataEventYYY(Object sender, Object DbDataEventObject)
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
            DbDataEventArgs DbDataArgs; // = ScriptDataEventObject as ScriptDataEventArgs;
            DbDataDef DbData;
            DataGridView DbListUsed;
            string[] rowString;

            string LocalMessage = "UI Data event from (" + sender.ToString() + ") on Script Thread.";
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
                            // Display Script Line
                            rowString = ((iDbData)DbData.ObjectData).SetRowData();
                            // Note: that the dataGridViews returned
                            // originated in this program.
                            // However the list below might not be visible.
                            DbListUsed = new DataGridView();
                            // Error = DbListViews.SetDataGridView(ref CrListUsed, ref ((iDbTaskDataSql)DbData.ObjectData).GetStatus());
                            Error = DbTaskLocal.DbListViews.SetDataGridView(ref DbTaskLocal.DbListViews, DbTaskLocal.DataStatus);
                            if (!Error)
                            {
                                if(DbListUsed.Name.StartsWith("Action"))
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

        //public virtual void OnTaskDbDataEvent(Object sender, Object DbDataEventObject)
        //{
        //    bool Error = false;
        //    bool Skip = false;
        //    // Validate sending object
        //    DbTaskDef DbTaskLocal = DbTaskDef.DbTaskValidate(sender);
        //    if (DbTaskLocal == null)
        //    {
        //        Error = true; // NO???? 
        //                      // ToDo Throw invalid data warning.
        //                      // return; // NO???? 
        //    }
        //    DbDataEventArgs DbDataArgs; // = ScriptDataEventObject as ScriptDataEventArgs;
        //    DbDataDef DbData;
        //    // ref DataGridView DbListUsed = new DataGridView();
        //    // Valid DbTask
        //    string LocalMessage = "UI Data event on Database Thread.";
        //    DbTaskDef.DbTaskDataEventMessage(sender, DbTaskLocal, LocalMessage, Error);
        //    // Lock Mutex
        //    if (DbTaskLocal.WaitLockMutex == null) { DbTaskLocal.WaitLockMutex = new Mutex(); }
        //    // Update CrList (always).
        //    DbDataArgs = DbDataEventObject as DbDataEventArgs;
        //    if (((DbDataEventArgs)DbDataEventObject).DbData == null) { Error = true; }
        //    else
        //    {
        //        // Skip s/b false;
        //        // insert code here <<
        //        if (!Skip)
        //        {
        //            DbData = DbDataArgs.DbData as DbDataDef;
        //            if (DbData == null) { Error = true; }
        //            else
        //            {
        //                // Valid Event Data
        //                // This should execute on the UI thread...
        //                Error = DbTaskLocal.DbListViews.SetDataGridView(ref DbTaskLocal.DbListViews, ref Status);
        //                // DbListUsed = DbTaskLocal.DbListViews.DbList;
        //                Skip = false;
        //                // Insert Case statements here <<
        //                if (!Skip && !Error)
        //                {
        //                    // Display Script Line
        //                    string[] rowString = ((iDbTaskDataSql)DbData.ObjectData).SetRowData();
        //                    // Note that error is not set
        //                    // There should be a result returned
        //                    // and the row passed by reference.
        //                    if (!Error)
        //                    {
        //                        DbTaskLocal.DbListViews.DbList.Rows.Add(rowString);
        //                        DbTaskLocal.DbListViews.DbList.FirstDisplayedScrollingRowIndex =
        //                            DbTaskLocal.DbListViews.DbList.RowCount - 1;
        //                    }
        //                    else
        //                    {
        //                        // Data or List error
        //                        if (DbTaskLocal.DbListViews.DbList != null)
        //                        {
        //                            DbTaskLocal.DbListViews.DbList.Rows.Add(rowString);
        //                            DbTaskLocal.DbListViews.DbList.FirstDisplayedScrollingRowIndex =
        //                                DbTaskLocal.DbListViews.DbList.RowCount - 1;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    if (Error)
        //    {
        //        // ToDo Throw invalid data warning.
        //    }
        //}
        //public virtual void FireTaskDbDataEvent(DbDataDef DbData)
        //{
        //    if (DbTaskDataEvent != null)
        //    {
        //        DbDataEventArgs args = new DbDataEventArgs(DbData);
        //        if (DbTaskDataEvent.Target is
        //                System.Windows.Forms.Control)
        //        {
        //            System.Windows.Forms.Control targetForm = DbTaskDataEvent.Target
        //                    as System.Windows.Forms.Control;
        //            targetForm.Invoke(DbTaskDataEvent,
        //                    new object[] { this, args });
        //        }
        //        else
        //        {
        //            DbTaskDataEvent(this, args);
        //        }
        //    }
        //}
    }
}
