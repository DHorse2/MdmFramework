using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
// using System.Threading.Tasks; // not in Net35
#region  Mdm Core
using Mdm.Oss;
//@@@CODE@@@using Mdm.Oss.ClipUtil;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
//@@@CODE@@@using Mdm.Oss.Mapp;
// using Mdm.Oss.Mobj;
//using Mdm.Oss.Sys;
//using Mdm.Oss.Thread;
//using Mdm.Oss.Threading;
using Mdm.Oss.Components;
//using Mdm.World;
#endregion
using Mdm.Oss.Std;

namespace Mdm.Oss.Console
{
    public partial class ConsoleFormDef
    {
        #region Thread Local Storage (ThreadStatic)
        // Process management
        [ThreadStatic]
        public static List<string> Context = new List<string>();
        [ThreadStatic]
        public static string DirectoryName;
        [ThreadStatic]
        public static string FileName;
        // Item stats
        [ThreadStatic]
        public static int DirectoryCount;
        [ThreadStatic]
        public static int FileCount;
        [ThreadStatic]
        public static int LinkCount;
        [ThreadStatic]
        public static string ItemOld;
        [ThreadStatic]
        public static bool ItemValid;
        [ThreadStatic]
        public static int RowIndex;
        // State
        [ThreadStatic]
        static bool cont;
        // Messaging
        [ThreadStatic]
        public StateIs MessageFilterThreadDoResult;
        [ThreadStatic]
        public StateIs MessageFilterThreadResult;
        [ThreadStatic]
        public StateIs FilterRowResult;

        [ThreadStatic]
        public int MessageFilterTaskId;
        [ThreadStatic]
        public static ConsoleThreadDef MessageFilterTaskThread;

        public ConsoleTaskDef MessageFilterTask;
        public ConsoleTaskDef.CalculationLongDelegate MessageFilterTaskDel;
        //public ConsoleTaskDef.FileSqlScGetSqlDelegate MessageFilterGetSqlDel;
        //public ConsoleTaskDef.FileSqlScSetSqlDelegate MessageFilterSetSqlDel;
        //public ConsoleTaskDef.FileSqlScSetSqlDataLineDelegate MessageFilterSetSqlDataLineDel;
        #endregion
    }
}
