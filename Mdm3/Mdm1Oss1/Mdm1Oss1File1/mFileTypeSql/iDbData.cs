using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Controls;

using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Mobj;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Std;
using Mdm.Oss.Components;
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

namespace Mdm.Oss.File.Db.Data
{
    public interface iDbData : IDbTaskDataId
    {
        // Mutlithreaded Task and Database Task Processing.

        // Note: DataGridView DbList property backed by dbList.
        // This is the current list (gridview) being used.
        // Other private (or public) lists may be used
        // in addition to the default list.
        // Gets Data from SQL object
        DataStatusIs GetStatus();
        object GetSqlData(object PassedData, mFileSqlConnectionDef DbListFile);

        // Sets SQL field parameters using the Data
        StateIs SetSqlData(object PassedLinkData, mFileSqlConnectionDef DbListFile);

        // Composes the SQL fields portion of the command.
        // Per the Command Type
        string SetSqlDataLine(DbThreadDef FileSqlThread);

        // Sets the Data Object
        StateIs SetObject(object PassedData);

        // The ID of This. IE not pulled from the SQL data.
        StateIs SetId(int PassedId);

        // DataGridView containing Rows
        ref DataGridView GridViewGet();
        StateIs GridViewSetFrom(ref DataGridView GridViewPassed);

        // Places the fields in the array per column display order.
        string[] SetRowData();
        ref DataGridViewRow SetDataGridViewRow(ref DataGridViewRow DataGridViewRowPassed);
        //
        //static void GetAppendedFieldNameList(ref List<string> FieldNameList, ref string FieldNameListText);
        // void OnTaskDataEvent(Object sender, Object TaskDataEventObject);
    }
}
