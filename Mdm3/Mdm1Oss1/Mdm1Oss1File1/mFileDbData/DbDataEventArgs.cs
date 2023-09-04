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
using Mdm.Oss.File;
using Mdm.Oss.Mobj;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Components;

namespace Mdm.Oss.File.Db.Data
{
    public class DbDataEventArgs : ConsoleMessageEventArgs
    {
        public DbDataDef DbData;
        public object DbDataObject;
        public DbDataEventArgs(object DbDataObjectPassed)
        {
            DbDataObject = DbDataObjectPassed;
            DbData = DbDataObjectPassed as DbDataDef;
            Status = CalculationStatus.Calculating;
        }
        public DbDataEventArgs(DbDataDef DbDataPassed)
        {
            DbData = DbDataPassed;
            DbDataObject = DbData;
            Status = CalculationStatus.Calculating;
        }
        public DbDataEventArgs(int PassedDirectoryProgress, int PassedFileProgress)
        {
            DirectoryProgress = PassedDirectoryProgress;
            FileProgress = PassedFileProgress;
            Status = CalculationStatus.Calculating;
        }
        public DbDataEventArgs(int PassedProgress)
        {
            Progress = PassedProgress;
            Status = CalculationStatus.Calculating;
        }
        public DbDataEventArgs(CalculationStatus PassedStatus, bool PassedSetButtons)
        {
            Status = PassedStatus;
            SetButtons = PassedSetButtons;
        }
        public DbDataEventArgs()
        {
            //
        }
    }
}
