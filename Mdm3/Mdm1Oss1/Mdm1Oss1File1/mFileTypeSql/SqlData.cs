using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;

using Mdm.Oss.Mobj;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Components;
using Mdm.Srt;
using Mdm.Srt.Core;
using Mdm.Srt.Transform;
#region  Mdm Db and File
using Mdm.Oss.File;
using Mdm.Oss.File.Db;
using Mdm.Oss.File.Db.Data;
using Mdm.Oss.File.Db.Table;
using Mdm.Oss.File.Db.Thread;
using Mdm.Oss.File.Properties;
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
//using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
using Mdm.Oss.File.Type.Srt.Script;
#endregion

namespace Mdm.Oss.File.Type.Sql
{
    public class FileSqlDataDef
    {
        public DbDataDef DbData;

        public FileSqlDataDef(ScriptDataDef DbDataPassed)
        {
            DbData = DbDataPassed;
        }
    }
    public class FileSqlDataEventArgs : ConsoleMessageEventArgs
    {
        public FileSqlDataDef FileSqlData;

        public FileSqlDataEventArgs(FileSqlDataDef FileSqlDataPassed)
        {
            FileSqlData = FileSqlDataPassed;
            Status = CalculationStatus.Calculating;
        }
        public FileSqlDataEventArgs(int PassedProgress)
        {
            Progress = PassedProgress;
            Status = CalculationStatus.Calculating;
        }

        public FileSqlDataEventArgs(CalculationStatus PassedStatus, bool PassedSetButtons)
        {
            Status = PassedStatus;
            SetButtons = PassedSetButtons;
        }
        public FileSqlDataEventArgs()
        {
            //
        }
    }
}
