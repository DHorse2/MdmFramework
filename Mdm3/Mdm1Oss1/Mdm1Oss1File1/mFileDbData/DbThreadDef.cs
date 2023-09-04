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
using Mdm.Oss.Components;

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
// using Mdm.Oss.File.Type.Srt.Script;
#endregion

namespace Mdm.Oss.File.Db.Thread
{
    public class DbThreadDef : ConsoleThreadDef
    {
        public int Id;
        public string ItemId;
        public DbTaskDef DbTask;
        public bool UseThread;
        public mFileSqlConnectionDef mFile;
        public string DbCommandPassed;
        public string DbCommandArgPassed;
        public bool FirstOnly;
        public bool GetSqlData;
    }
}
