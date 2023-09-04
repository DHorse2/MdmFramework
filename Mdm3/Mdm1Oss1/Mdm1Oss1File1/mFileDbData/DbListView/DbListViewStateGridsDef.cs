#region Dependencies
#region System
#region System
using System;
#endregion
#region System Collections
using System.Collections;
using System.Collections.Generic;
//using System.Collections.Specialized;
#endregion
#region System Data & IO
using System.IO;
using System.Data;
//using System.Data.Common;
#endregion
#region System Globalization
//using System.Globalization;
#endregion
#region System Other
// using System.Collections.Specialized;
#endregion
#region System Reflection, Diagnostics, RT and Timers
//using System.Diagnostics;
//using System.Reflection;
//using System.Runtime;
//using System.Runtime.ExceptionServices;
//using System.Runtime.InteropServices;
//using System.Runtime.Remoting.Messaging;
using System.Timers;
#endregion
#region System SQL
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
#endregion
#region System Text and Linq
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
#endregion
#region  System Threading
using System.Threading;
//using System.Threading.Tasks;
#endregion
#region System Windows Forms
using System.Drawing;
//using System.Windows;
using System.Windows.Forms;
//using System.Windows.Controls;
#endregion
#region System Serialization (Runtime and Xml)
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Xml.Serialization;
#endregion
#region System XML
//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Schema;
#endregion
#endregion
#region Mdm
// Mdm (Macroscope Design Matrix / Dgh (c))
#region  Mdm Core
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.Thread;
using Mdm.Oss.Components;
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
//using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Pick;
using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion
#endregion

namespace Mdm.Oss.File.Db.Data
{
    public class DbListViewStateGridsDef : StdBaseDef, IDisposable
    // : StdDef, IDisposable
    // : StdConsoleManagerDef, IDisposable
    {
        #region Fields
        public new StdConsoleManagerDef st;
        public mFileSqlConnectionDef DbFile;
        public DbListViewStateGridsDef DbListViewStateGridsThis;
        // These act as defaults
        public bool GridViewIsUsed;
        public bool ControlIsUsed;
        // Currently Selected Grid
        private DbListViewDef dbList;
        public DbListViewDef DbList
        {
            get
            {
                return dbList;
            }
            set
            {
                dbList = value;
            }
        }
        #region DataGridView States
        // Based on Data State/Status 
        // rows can be routed to different gridviews.
        // This is now contained in the class DbListViewsDef
        // bool SetDataGridView(ref DataGridView PassedDbList);
        // This is normally an output of row data.
        // returns the gridview being used.
        // This is determined earlier by Data State,
        // and by program initialization and flow.
        public DbListViewDef Valid;// Valid
        public DbListViewDef Invalid;// Broken
        public DbListViewDef Error;// Error
        // Duplicates would be allowed.
        // IE. Insert & Function updated.
        public DbListViewDef Actioned;// Actioned (CRUD & functions)
        public DbListViewDef ActionUpdated;// Action Update
        public DbListViewDef ActionDeleted;// Action Delete
        public DbListViewDef ActionCreated;// Action Insert
        public DbListViewDef ActionRead;// Action Read
        public DbListViewDef ActionFunction;// Action function / Other
        #endregion
        #endregion
        #region Initialization, Loads, Meta Data and Control
        public DbListViewStateGridsDef(
            ref object SenderPassed,
            ref StdConsoleManagerDef stPassed,
            bool UseConsoleButtonsPassed)
            : base(ref SenderPassed, ref stPassed.SenderIsThis)
        {
            st = base.st as StdConsoleManagerDef;
            // Initialize();
            UseConsoleButtons = UseConsoleButtonsPassed;
            InitializeDbListViewStateGrids();
            InitializeStateGrids();
            // InitializeConsoleComponent();
        }
        public DbListViewStateGridsDef() 
            :base()
        {
            // Initialize();
            ConsoleComponentCreate();
            InitializeDbListViewStateGrids();
            InitializeStateGrids();
            // InitializeConsoleComponent();
        }
        public virtual void InitializeDbListViewStateGrids()
        {
            base.InitializeStd();
            if (UseConsoleButtons)
            {
                StdRunControlUi = st.StdRunControlGet(ref StdRunControlUi);
            }
            else
            {
                StdRunControlUi = new StdBaseRunControlUiDef(ref SenderIsThis, ref ConsoleSender, st.StdKey);
            }
        }
        public void Clear()
        {
            Valid = new DbListViewDef();// Valid
            Invalid = new DbListViewDef();// Broken
            Error = new DbListViewDef();// Error
                                       // Duplicates would be allowed.
                                       // IE. Insert & Function updated.
            Actioned = new DbListViewDef();// Actioned (CRUD & functions)
            ActionUpdated = new DbListViewDef();// Action Update
            ActionDeleted = new DbListViewDef();// Action Delete
            ActionCreated = new DbListViewDef();// Action Insert
            ActionRead = new DbListViewDef();// Action Read
            ActionFunction = new DbListViewDef();// Action function / Other
        }
        public new void Dispose()
        {
            base.Dispose();
        }
        public StateIs InitializeStateGrids()
        {
            GridViewIsUsed = true;
            StdRunControlUi.GridViewIsExternal = false;
            return StateIs.Finished;
        }
        private StateIs ConsoleComponentCreate()
        {
            InitializeDbListViewStateGrids();
            StateIs TempStatus = InitializeConsoleComponent();
            st = new StdConsoleManagerDef(ConsoleSource, ClassRole, ClassFeatures)
            { ConsoleVerbosity = 7 };
            // st.ClassFeaturesFlagsSet(ClassFeaturesIsThis);
            StdRunControlUi = st.StdRunControlUi;
            return StateIs.Finished;
        }
        private StateIs InitializeConsoleComponent()
        {
            #region Framework Features Used
            ClassRole = ClassRoleIs.RoleAsUtility;
            // this isn't perfect but can be tweaked when needed. (defaults?)
            ClassFeatures =
            ClassFeatureIs.MaskUi
            | ClassFeatureIs.MaskButton
            | ClassFeatureIs.MaskStautsUiAsBox
            | ClassFeatureIs.Window
            | ClassFeatureIs.MdmUtilConsole;
            //
            //st = new StdConsoleManagerDef(ConsoleSource, ClassRole, ClassFeatures)
            //{ ConsoleVerbosity = 7 };
            st.ClassFeaturesFlagsSet(ConsoleSource, ClassRole, ClassFeatures);
            #endregion
            // Form Controls are null.
            // They do a lazy load when the user decides to
            // display any given State (Status) DataGridView.
            //
            // InitializeComponentForm();
            return StateIs.Finished;
        }
        #endregion
        #region Status based View selection - Set
        public ref DbListViewDef GetDataGridView()
        {
            return ref dbList;
        }
        public bool SetDataGridView(ref DataStatusIs DataStatus)
        {
            DbListViewStateGridsDef tmpListViews = this;
            bool Error = SetDataGridView(ref tmpListViews, DataStatus);
            return Error;
        }
        public bool SetDataGridView(ref DbListViewStateGridsDef DbListViewsPassed, DataStatusIs DataStatus)
        {
            // This should execute on the UI thread...
            bool Error = false;
            bool Skip = false;
            switch (DataStatus)
            {
                case DataStatusIs.Invalid:
                    DbList = DbListViewsPassed.Invalid;
                    DbListViewsPassed.DbList = DbListViewsPassed.Invalid;
                    break;
                case DataStatusIs.NewValid:
                case DataStatusIs.Added:
                case DataStatusIs.Updated:
                    DbList = DbListViewsPassed.ActionUpdated;
                    DbListViewsPassed.DbList = DbListViewsPassed.ActionUpdated;
                    break;
                case DataStatusIs.Deleted:
                    DbList = DbListViewsPassed.ActionDeleted;
                    DbListViewsPassed.DbList = DbListViewsPassed.ActionDeleted;
                    break;
                case DataStatusIs.Valid:
                    DbList = DbListViewsPassed.Valid;
                    DbListViewsPassed.DbList = DbListViewsPassed.Valid;
                    break;
                default:
                    // DbList should remain unchanged, there is no flag for that.
                    // Trying this out:
                    DbList = DbListViewsPassed.Error;
                    DbListViewsPassed.DbList = DbListViewsPassed.Error;
                    // ToDo Throw warning message.
                    Error = true; // Unless you have Skip logic.
                                  // Skip
                    Skip = true;
                    break;
            }
            //DbListViewsPassed.DbList = DbList;
            return Error;
        }
        #endregion
    }
}
