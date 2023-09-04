using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

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
//using Mdm.Oss.File.Properties;
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
//using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
using Mdm.Oss.File.Type.Srt.Script;
#endregion
#region Mdm Srt
using Mdm.Srt;
using Mdm.Srt.Core;
using Mdm.Srt.Transform;
// using Mdm.Srt.Script;
#endregion
namespace Mdm.Oss.File.Type.Srt.Script
{
    public class ScriptDataEventArgs : DbDataEventArgs
    // ConsoleMessageEventArgs
    {
        public ScriptDataDef ScriptData;
        public ScriptDataEventArgs(ScriptDataDef PassedScriptData)
        {
            this.ScriptData = PassedScriptData;
            this.Status = CalculationStatus.Calculating;
        }
        public ScriptDataEventArgs(int progress)
        {
            this.Progress = progress;
            this.Status = CalculationStatus.Calculating;
        }
        public ScriptDataEventArgs(CalculationStatus status)
        {
            this.Status = status;
        }
    }
}
