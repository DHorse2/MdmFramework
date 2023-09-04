#region Dependencies
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
#region  Mdm Core
// Project > Add Reference > 
// add shell32.dll reference
// (new) possibly interop.Shell32 & interop.IWshRuntimeLibrary
// > COM > Microsoft Shell Controls and Automation
using Shell32;
// > COM > Windows Script Host Object Model.
using IWshRuntimeLibrary;
using Mdm.Oss;
using Mdm.Oss.Components;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Mobj;
// using Mdm.Oss.ShortcutUtil;
using Mdm.Oss.Std;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
#endregion
#region  Mdm Db and File
using Mdm.Oss.File;
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
using Mdm.Oss.File.Type.Srt.Script;
#endregion
#region Mdm Srt (Search, replace and transform)
using Mdm.Srt;
using Mdm.Srt.Core;
using Mdm.Srt.Transform;
using Mdm.Srt.Script;
#endregion
// namespace Mdm.Oss.File.Type.DataType.DataSubType
// namespace Mdm.Oss.File.Type.Srt.Run
// namespace Mdm.Oss.File.Type.Srt.Script
#endregion

namespace Mdm.Oss.File.Type.Srt.Script
{
    public class ScriptDataDef : DbDataDef
    {
        public string ScriptName;
        public string Sequence;
        public string InputString;
        public string OutputString;
        public string OutputAction;
        //
        public List<string> FieldUsedList;
        public string FieldUsedListText;
        public List<string> OutputActionList;
        public string OutputActionListText;
        //
        public ScriptDataDef LocalScriptData;
        public string[] rowString;
        public mDropDownCellDef LocalDropDownCell;
        public DataGridViewRow LocalGridViewRow;
        //
        public ScriptDataDef()
        {
            Clear();
        }
        public new void Clear()
        {
            base.Clear();
            ScriptName = sEmpty;
            Sequence = sEmpty;
            InputString = sEmpty;
            OutputString = sEmpty;
            FieldUsed = sEmpty; // "Target Path"
            OutputAction = sEmpty; // "Fix"
            Reset();
        }
        public new void Reset()
        {
            base.Reset();
        }
        #region SQL
        // Note: Similar to XML serialize the SQL calls could be an interface NEEDS A TRY?
        public override object GetSqlData(object PassedData, mFileSqlConnectionDef ScriptFile)
        {
            LocalScriptData = PassedData as ScriptDataDef;
            if (LocalScriptData == null) { LocalScriptData = new ScriptDataDef(); }
            LocalScriptData = (ScriptDataDef)base.GetSqlData(LocalScriptData, ScriptFile);
            LocalScriptData.Id = (int)ScriptFile.Fmain.DbIo.SqlDbReader["Id"];
            // if (LinkData.Id > RowNumberCurrent) { RowNumberCurrent = LinkData.Id; }
            LocalScriptData.ScriptName = (string)ScriptFile.Fmain.DbIo.SqlDbReader["ScriptName"];
            LocalScriptData.Sequence = (string)ScriptFile.Fmain.DbIo.SqlDbReader["Sequence"];


            try
            {
                LocalScriptData.OutputAction = (string)ScriptFile.Fmain.DbIo.SqlDbReader["OutputAction"];
            }
            catch (Exception e)
            {
                LocalScriptData.OutputAction = "";
            };

            try
            {
                LocalScriptData.FieldUsed = (string)ScriptFile.Fmain.DbIo.SqlDbReader["FieldUsed"];
            }
            catch (Exception e)
            {
                LocalScriptData.FieldUsed = "";
            };

            try
            {
                LocalScriptData.InputString = (string)ScriptFile.Fmain.DbIo.SqlDbReader["InputString"];
            }
            catch (Exception e)
            {
                LocalScriptData.InputString = "";
            };

            try
            {
                LocalScriptData.OutputString = (string)ScriptFile.Fmain.DbIo.SqlDbReader["OutputString"];
            }
            catch (Exception e)
            {
                LocalScriptData.OutputString = null;
            };

            try
            {
                LocalScriptData.FileName = (string)ScriptFile.Fmain.DbIo.SqlDbReader["FileName"];
            }
            catch (Exception e)
            {
                LocalScriptData.FileName = null;
            }

            try
            {
                LocalScriptData.Value = (string)ScriptFile.Fmain.DbIo.SqlDbReader["Value"]; ;
            }
            catch (Exception e)
            {
                LocalScriptData.Value = null;
            }

            try
            {
                object tmp = (object)ScriptFile.Fmain.DbIo.SqlDbReader["DateUpdated"];
                // if (tmp != null)
                //{
                LocalScriptData.DateUpdated = tmp.ToString();
                //}
            }
            catch (Exception e)
            {
                LocalScriptData.DateUpdated = sEmpty;
            }
            //LocalData.DateUpdated = DateTime.Now.ToString("O");

            LocalScriptData.Found = false;
            LocalScriptData.Valid = false;
            LocalScriptData.DataIsDirty = false;
            return LocalScriptData;
        }
        public override StateIs SetSqlData(object PassedData, mFileSqlConnectionDef ScriptFile)
        {
            ScriptDataDef LocalData;
            LocalData = PassedData as ScriptDataDef;
            if (LocalData == null) { return StateIs.Undefined; }
            base.SetSqlData(LocalData, ScriptFile);
            if (LocalData.ScriptName == null) { LocalData.ScriptName = sEmpty; }
            if (LocalData.Sequence == null) { LocalData.Sequence = sEmpty; }
            if (LocalData.InputString == null) { LocalData.InputString = sEmpty; }
            // if (LocalData.OutputString == null) { LocalData.OutputString = sEmpty; }
            // if (LocalData.FileName == null) { LocalData.FileName = sEmpty; }

            //ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@IdValue", SqlDbType.Int);
            //ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters["@IdValue"].Value = LocalData.Id;
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@ScriptNameValue", SqlDbType.VarChar);
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters["@ScriptNameValue"].Value = LocalData.ScriptName;
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@SequenceValue", SqlDbType.VarChar);
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters["@SequenceValue"].Value = LocalData.Sequence;
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@OutputActionValue", SqlDbType.VarChar);
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters["@OutputActionValue"].Value = LocalData.OutputAction;
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@FieldUsedValue", SqlDbType.VarChar);
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters["@FieldUsedValue"].Value = LocalData.FieldUsed;

            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@InputStringValue", SqlDbType.VarChar);
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters["@InputStringValue"].Value = LocalData.InputString;
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@OutputStringValue", SqlDbType.VarChar);
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters["@OutputStringValue"].Value = LocalData.OutputString;

            //ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@FileNameValue", SqlDbType.VarChar);
            //ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters["@FileNameValue"].Value = LocalData.FileName;
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@ValueValue", SqlDbType.VarChar);
            ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters["@ValueValue"].Value = LocalData.Value;

            //ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters.Add("@DateUpdatedValue", SqlDbType.VarChar);
            //LocalData.DateUpdated = DateTime.Now.ToString("O");
            //ScriptFile.Fmain.DbIo.SqlDbCommand.Parameters["@DateUpdatedValue"].Value = LocalData.DateUpdated;
            //
            return 0;
        }
        public override string SetSqlDataLine(DbThreadDef FileSqlThread)
        {
            setCommand = sEmpty;
            setFieldList = sEmpty;
            if (FileSqlThread.SqlCommandType == DbCommandIs.Update)
            {
                setCommand += " SET ";
            }
            else
            {
                setCommand += " VALUES (";
                setFieldList += "(";

            }
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
                setCommand += @"ScriptName";
                setCommand += @" = ";
            }
            setCommand += @"@ScriptNameValue";
            setCommand += @", ";
            setFieldList += @"ScriptName";
            setFieldList += @", ";

            if (FileSqlThread.SqlCommandType == DbCommandIs.Update)
            {
                setCommand += @"Sequence";
                setCommand += @" = ";
            }
            setCommand += @"@SequenceValue";
            setCommand += @", ";
            setFieldList += @"Sequence";
            setFieldList += @", ";

            if (FileSqlThread.SqlCommandType == DbCommandIs.Update)
            {
                setCommand += @"OutputAction";
                setCommand += @" = ";
            }
            setCommand += @"@OutputActionValue";
            setCommand += @", ";
            setFieldList += @"OutputAction";
            setFieldList += @", ";

            if (FileSqlThread.SqlCommandType == DbCommandIs.Update)
            {
                setCommand += @"FieldUsed";
                setCommand += @" = ";
            }
            setCommand += @"@FieldUsedValue";
            setCommand += @", ";
            setFieldList += @"FieldUsed";
            setFieldList += @", ";

            if (FileSqlThread.SqlCommandType == DbCommandIs.Update)
            {
                setCommand += @"InputString";
                setCommand += @" = ";
            }
            setCommand += @"@InputStringValue";
            setCommand += @", ";
            setFieldList += @"InputString";
            setFieldList += @", ";

            if (FileSqlThread.SqlCommandType == DbCommandIs.Update)
            {
                setCommand += @"OutputString";
                setCommand += @" = ";
            }
            setCommand += @"@OutputStringValue";
            setCommand += @", ";
            setFieldList += @"OutputString";
            setFieldList += @", ";

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

            if (FileSqlThread.SqlCommandType == DbCommandIs.Update)
            {
                // setCommand += sEmpty;
                return setCommand;
            }
            else
            {
                setCommand += @")";
                setFieldList += @")";
                setCommand = setFieldList + setCommand;
                return setCommand;
            }
        }
        #endregion
        #region UI Controls and data sources
        public override ref DataGridViewRow SetDataGridViewRow(ref DataGridViewRow GridViewRowPassed)
        {
            LocalGridViewRow = GridViewRowPassed;
            LocalDropDownCell = new mDropDownCellDef();
            LocalGridViewRow = new DataGridViewRow();
            LocalGridViewRow.Cells[0].Value = ScriptName;
            LocalGridViewRow.Cells[1].Value = Sequence;

            // rowGrid.Cells[3].Value = FieldUsed;
            LocalDropDownCell = new mDropDownCellDef();
            LocalDropDownCell.DataSource = FieldUsedList;
            LocalDropDownCell.Value = FieldUsed;
            LocalGridViewRow.Cells[2].ValueType = typeof(Mdm.Oss.Components.mDropDownColumnDef);
            LocalGridViewRow.Cells[2].Value = LocalDropDownCell;

            LocalGridViewRow.Cells[3].Value = InputString;

            // rowGrid.Cells[2].Value = OutputAction;
            LocalDropDownCell.DataSource = OutputActionList;
            LocalDropDownCell.Value = OutputAction;
            LocalGridViewRow.Cells[4].ValueType = typeof(Mdm.Oss.Components.mDropDownColumnDef);
            LocalGridViewRow.Cells[4].Value = LocalDropDownCell;

            LocalGridViewRow.Cells[5].Value = OutputString;

            LocalGridViewRow.Cells[6].Value = FileName;
            LocalGridViewRow.Cells[7].Value = Value;
            LocalGridViewRow.Cells[8].Value = DateUpdated;
            LocalGridViewRow.Cells[9].Value = Id.ToString();
            return ref LocalGridViewRow;
        }
        public override string[] SetRowData()
        {
            rowString = new string[10];
            rowString[0] = ScriptName;
            rowString[1] = Sequence;
            rowString[2] = FieldUsed;
            rowString[3] = InputString;
            rowString[4] = OutputAction;
            rowString[5] = OutputString;
            rowString[6] = FileName;
            rowString[7] = Value;
            rowString[8] = DateUpdated;
            rowString[9] = Id.ToString();
            return rowString;
        }
        #endregion
        #region Field Gets - data sources
        public override string GetFieldValue(String FieldName, object ScriptDataPassed)
        {
            // If (null) ?
            ScriptDataDef LocalScriptData = (ScriptDataDef)ScriptDataPassed;
            switch (FieldName)
            {
                case "DataType":
                    return LocalScriptData.DataType;
                    break;
                case "DataTypeMinor":
                    return LocalScriptData.DataTypeMinor;
                    break;
                case "ScriptName":
                    return LocalScriptData.ScriptName;
                    break;
                case "Sequence":
                    return LocalScriptData.Sequence;
                    break;
                case "Id":
                    return LocalScriptData.Id.ToString();
                    break;
                case "OutputAction":
                    return LocalScriptData.OutputAction;
                    break;
                case "FieldUsed":
                    return LocalScriptData.FieldUsed;
                    break;
                case "InputString":
                    return LocalScriptData.InputString;
                    break;
                case "OutputString":
                    return LocalScriptData.OutputString;
                    break;
                case "FileName":
                    return LocalScriptData.FileName;
                    break;
                case "Value":
                    return LocalScriptData.Value;
                    break;
                case "DateUpdated":
                    return LocalScriptData.DateUpdated;
                    break;
                case "DataIsDirty":
                    return LocalScriptData.DataIsDirty.ToString();
                    break;
                default:
                    return "ERROR";
                    break;
            }
            return "ERROR";
        }
        public override object GetFieldData(String FieldName, object ScriptDataPassed)
        {
            ScriptDataDef LocalScriptData = (ScriptDataDef)ScriptDataPassed;
            switch (FieldName)
            {
                case "DataType":
                    return LocalScriptData.DataType;
                    break;
                case "DataTypeMinor":
                    return LocalScriptData.DataTypeMinor;
                    break;
                case "ScriptName":
                    return LocalScriptData.ScriptName;
                    break;
                case "Sequence":
                    return LocalScriptData.Sequence;
                    break;
                case "Id":
                    return LocalScriptData.Id.ToString();
                    break;
                case "OutputAction":
                    return LocalScriptData.OutputAction;
                    break;
                case "FieldUsed":
                    return LocalScriptData.FieldUsed;
                    break;
                case "InputString":
                    return LocalScriptData.InputString;
                    break;
                case "OutputString":
                    return LocalScriptData.OutputString;
                    break;
                case "FileName":
                    return LocalScriptData.FileName;
                    break;
                case "Value":
                    return LocalScriptData.Value;
                    break;
                case "DateUpdated":
                    return LocalScriptData.DateUpdated;
                    break;
                case "DataIsDirty":
                    return LocalScriptData.DataIsDirty;
                    break;
                default:
                    return "ERROR";
                    break;
            }
            return "ERROR";
        }
        public override string GetFieldName(object FieldObject)
        {
            return nameof(FieldObject);
        }
        public static new void GetAppendedFieldNameList(ref List<string> FieldNameList, ref string FieldNameListText)
        {
            GetAppendedFieldNameListStatic(ref FieldNameList, ref FieldNameListText);
        }
        public static new void GetAppendedFieldNameListStatic(ref List<string> FieldNameList, ref string FieldNameListText)
        {
            // List<string> FieldList = new List<string>;
            // FieldNameList.Add( ;
            DbDataDef.GetAppendedFieldNameListStatic(ref FieldNameList, ref FieldNameListText);
            FieldNameList.Add("ScriptName");
            FieldNameList.Add("Sequence");
            FieldNameList.Add("OutputAction");
            FieldNameList.Add("FieldUsed");
            FieldNameList.Add("InputString");
            FieldNameList.Add("OutputString");

            FieldNameListText += "ScriptName, ";
            FieldNameListText += "Sequence, ";
            FieldNameListText += "OutputAction, ";
            FieldNameListText += "Field Used, ";
            FieldNameListText += "InputString, ";
            FieldNameListText += "OutputString, ";
            return;
        }
        public void OutputActionListSetFrom(ref List<string> OutputActionListPassed)
        {
            OutputActionList = OutputActionListPassed;
        }
        public void FieldUsedListSetFrom(ref List<string> FieldUsedListPassed)
        {
            FieldUsedList = FieldUsedListPassed;
        }
        #endregion
        public override void OnTaskDataEventYYY(Object sender, Object ScriptDataEventObject)
        {
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
            DbDataEventArgs ScriptDataArgs; // = ScriptDataEventObject as ScriptDataEventArgs;
            ScriptDataDef ScriptData;
            DataGridView DbListUsed;
            // Valid DbTask
            string LocalMessage = "UI Data event on Script Thread.";
            DbTaskDef.TaskDbDataEventMessage(sender, DbTaskLocal, LocalMessage, Error);
            // Lock Mutex
            if (DbTaskLocal.WaitLockMutex == null) { DbTaskLocal.WaitLockMutex = new Mutex(); }
            // Update CrList (always).
            ScriptDataArgs = ScriptDataEventObject as DbDataEventArgs;
            if (((DbDataEventArgs)ScriptDataArgs).DbData == null) { Error = true; }
            else
            {
                ScriptData = ((DbDataEventArgs)ScriptDataArgs).DbData as ScriptDataDef;
                if (ScriptData == null) { Error = true; }
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
                            // Display Script Line
                            string[] rowString = ((iDbData)ScriptData.ObjectData).SetRowData();
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
            }
        }
    }
    // I only use a few of these but
    // it enumerates the possible states.
    // Current Valid, Invalid, NewValid (with impled added?), Deleted.

}
