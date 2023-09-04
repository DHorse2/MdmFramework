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
using Mdm.Oss.Std;
#endregion

namespace Mdm.Oss.File.Type.Sql
{
    public class FileSqlTaskDef : ConsoleTaskDef
    {
        public FileSqlTaskDef(ref object SenderPassed, ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
            : base(ref SenderPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed) { }

        public delegate void FileTaskDbDataEventHandler(
            object sender, FileSqlDataEventArgs e);

        public event FileTaskDbDataEventHandler FileSqlTaskDataEvent;
        public void FireTaskDbDataEvent(FileSqlDataDef FileSqlData)
        {
            if (FileSqlTaskDataEvent != null)
            {
                FileSqlDataEventArgs args = new FileSqlDataEventArgs(FileSqlData);
                if (FileSqlTaskDataEvent.Target is
                        System.Windows.Forms.Control)
                {
                    System.Windows.Forms.Control targetForm = FileSqlTaskDataEvent.Target
                            as System.Windows.Forms.Control;
                    targetForm.Invoke(FileSqlTaskDataEvent,
                            new object[] { this, args });
                }
                else
                {
                    MethodInfo targetMethod = FileSqlTaskDataEvent.Method;
                    targetMethod.Invoke(FileSqlTaskDataEvent,
                            new object[] { args });
                    //FileSqlTaskMessageEvent(this, args);
                }
            }
        }

    }
}
