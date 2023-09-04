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
using Mdm.Oss.Components;
using Mdm.Oss.Decl;
// using Mdm.Oss.Mobj;
using Mdm.Oss.Std;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
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
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
// using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion
namespace Mdm.Oss.File.Type.Srt.Script
{
    #region Script Task and Event argument definitions
    public class ScriptTaskDef : DbTaskDef
    {
        public new delegate void TaskDbDataEventHandler(
            object sender, ScriptDataEventArgs e);

        public new event TaskDbDataEventHandler TaskDbDataEvent;
        public ScriptTaskDef(ref object FormParentPassed, ref object ConsoleSenderPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
                : base(ref FormParentPassed, ref ConsoleSenderPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        {
            // st = stPassed;
        }
        public override void FireTaskDbDataEvent(object ScriptDataPassed)
        {
            bool Error = false;
            bool Skip = false;
            ScriptDataDef ScriptData = ScriptDataPassed as ScriptDataDef;
            if (ScriptData == null) { Error = true; }
            else
            {
                if (TaskDbDataEvent == null) { Error = true; }
                else
                {
                    ScriptDataEventArgs ScriptDataArgs = new ScriptDataEventArgs(ScriptData);
                    if (TaskDbDataEvent.Target is
                            System.Windows.Forms.Control)
                    {
                        System.Windows.Forms.Control targetForm = TaskDbDataEvent.Target
                                as System.Windows.Forms.Control;
                        targetForm.Invoke(TaskDbDataEvent,
                                new object[] { this, ScriptDataArgs });
                    }
                    else
                    {
                        TaskDbDataEvent(this, ScriptDataArgs);
                    }
                }
            }
            if (Error)
            {
                // ???? ToDo DbTask Event Error!
                if (ScriptData == null)
                {

                }
                if (TaskDbDataEvent == null)
                {
                    // ???? ToDo Programming error - No DbTask Event Error!
                }
            }
        }
    }
    #endregion
}
