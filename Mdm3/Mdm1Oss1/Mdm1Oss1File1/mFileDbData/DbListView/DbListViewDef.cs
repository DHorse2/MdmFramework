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
// using System.Runtime.ExceptionServices;
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
// using System.Threading.Tasks;
#endregion
#region System Windows Forms
using System.Drawing;
//using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
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
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
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
using ToolBar = System.Windows.Forms.ToolBar;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion
#endregion

namespace Mdm.Oss.File.Db.Data
{
    public class DbListViewDef : StdDef, IDisposable
    // : StdDef, IDisposable
    // : StdConsoleManagerDef, IDisposable
    {
        #region Fields
        public new StdConsoleManagerDef st;
        public mFileSqlConnectionDef DbFile;
        public DbListViewStateGridsDef DbListViewStateGrids;
        public DbListViewDef DbListViewThis;
        public DataGridView GridViewEx
        {
            get { return GridViewGetDefault(); }
            set { GridViewSetDefaultFrom(ref value); }
        }
        //public StateIs GridViewStatus;
        // When this is set the GridView
        // will not be instantiated. It
        // has to be passed in. It will
        // get updated. No control will
        // be created.

        public DbListViewControlDef DbListViewControl;
        public bool DbListViewHidden;
        public StateIs DbListViewControlStatus;
        // Current instance possibly.
        public DbDataDef DbData;
        #endregion
        #region Constructors
        public DbListViewDef(
            ref object SenderPassed,
            ref StdConsoleManagerDef stPassed,
            bool GridViewIsExternalPassed, 
            bool ControlIsUsedPassed,
            bool UseConsoleButtonsPassed)
            : base(ref SenderPassed, ConsoleSourceIs.Interface, stPassed.ClassRole, stPassed.ClassFeatures) 
        {
            #region Initialize Console
            //FormParent = SenderPassed as zStdBaseForm; // AppStd
            if (stPassed != null)
            {
                st = stPassed; ConsoleSender = st;
                // ConsoleSource = st.ConsoleSource;
                ConsoleSource = ConsoleSourceIs.Interface;
                ClassRole = st.ClassRole;
                ClassFeatures = st.ClassFeatures;
            } else 
            {
                ConsoleComponentCreate();
            }
            //st.ConsoleSet(st, st.ClassRole, st.ClassFeatures);
            StdRunControlUi = st.StdRunControlUi;
            #endregion
            InitializeDbListView();
            GridViewIsExternal = GridViewIsExternalPassed;
            DbListViewControlIsUsed = ControlIsUsedPassed;
            UseConsoleButtons = UseConsoleButtonsPassed;
            InitializeDbListView();
        }
        public DbListViewDef(
            ref object SenderPassed,
            ref StdConsoleManagerDef stPassed,
            ref DbListViewStateGridsDef DbListViewStateGridsPassed,
            ref DataGridView GridViewPassed,
            bool GridViewIsExternalPassed, 
            bool ControlIsUsedPassed,
            bool UseConsoleButtonsPassed)
        {
            #region Initialize Console
            Sender = SenderPassed;
            FormParent = SenderPassed as iStdBaseForm; // AppStd
            if (stPassed != null)
            {
                st = stPassed;
                ConsoleSource = st.ConsoleSource;
                ClassRole = st.ClassRole;
                ClassFeatures = st.ClassFeatures;
            }
            if (st == null)
            {
                ConsoleComponentCreate();
            }
            // st.StdConsoleSet(st, st.ClassRole, st.ClassFeatures);
            StdRunControlUi = st.StdRunControlUi;
            #endregion
            DbListViewStateGrids = DbListViewStateGridsPassed;
            GridViewSetDefaultFrom(ref GridViewPassed);
            GridViewIsExternal = GridViewIsExternalPassed;
            DbListViewControlIsUsed = ControlIsUsedPassed;
            UseConsoleButtons = UseConsoleButtonsPassed;
            //
            Status = StateIs.DoesExist;
            InitializeDbListView();
        }
        public DbListViewDef()
        {
            UseConsoleButtons = false;
            ConsoleComponentCreate();
            InitializeDbListView();
        }
        private StateIs ConsoleComponentCreate()
        {
            InitializeDbListView();
            StateIs TempStatus = InitializeConsoleComponent();
            st = new StdConsoleManagerDef(ConsoleSource, ClassRole, ClassFeatures)
            { ConsoleVerbosity = 7 };
            // st.ClassFeaturesFlagsSet(ClassFeaturesIsThis);
            //StdRunControlUi = st.StdRunControlUi;
            if (UseConsoleButtons)
            {
                StdRunControlUi = st.StdRunControlGet(ref StdRunControlUi);
            }
            else
            {
                StdRunControlUi = new StdBaseRunControlUiDef(ref SenderIsThis, ref ConsoleSender, st.StdKey);
            }
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
        #region Initialization, Dispose
        // Initialize
        public virtual StateIs InitializeDbListView()
        {
            Status = StateIs.Started;
            base.InitializeStd();
            DbListViewThis = this;
            if (!GridViewIsExternal)
            {
                Status = InitializeGridView();
            }
            if (DbListViewControlIsUsed)
            {
                Status = ControlInitialize();
            }
            return (Status = StateIs.Initialized);
        }
        public virtual StateIs InitializeGridView()
        {
            GridViewStatus = StateIs.Initialized;
            // Which object already exists depends on 
            // the call to the constructor()
            if (GridView == null)
            {
                if (!GridViewIsExternal)
                {
                    GridView = new DataGridView();
                    // GridViewSetFrom(ref GridViewPassed);
                    GridViewStatus = StateIs.Valid;
                }
                else
                {
                    // Exception?
                    // external but still needs to be set.
                    GridViewStatus = StateIs.DoesNotExist;
                }
            }
            else 
            {
                // Is it therefore External?
                GridViewStatus = StateIs.DoesExist; 
            }
            return GridViewStatus;
        }
        // Dispose
        public new void Dispose()
        {
            Status = Dispose(Status);
            base.Dispose();
        }
        public new StateIs Dispose(StateIs StatusPassed)
        {
            if (DbListViewControl != null)
            {
                DbListViewControl.Dispose();
                DbListViewControlStatus = StateIs.DoesNotExist;
            }
            if (GridView != null)
            {
                GridView.Dispose();
                GridViewStatus = StateIs.DoesNotExist;
            }
            return (Status = StateIs.DoesNotExist);
        }
        public void Clear() { }
        #endregion
        // DbListView Management
        #region Main Menu and Panel (Disabled)
        //private MainMenu MenuMain;
        //private MenuItem MenuFileItem, MenuOpenItem;
        //private MenuItem MenuFolderItem, MenuCloseItem;
        //private TableLayoutPanel MainTableLayoutPanel;
        #endregion
        #region DbListView Control Management - Form Control
        public virtual StateIs ControlInitialize()
        {
            if (UseConsoleButtons)
            {
                StdRunControlUi = st.StdRunControlGet(ref StdRunControlUi);
            }
            else
            {
                StdRunControlUi = new StdBaseRunControlUiDef(ref SenderIsThis, ref ConsoleSender, st.StdKey);
            }
            //bool GridViewIsExternal = true;
            DbListViewControlStatus = StateIs.Started;
            if (DbListViewControl == null)
            {
                DbListViewControl = new DbListViewControlDef(ref Sender, ref st, ref DbListViewThis, ref GridView, ref DbFile, GridViewIsExternal, UseConsoleButtons);
                DbListViewControlStatus = StateIs.Valid;
            }
            else { DbListViewControlStatus = StateIs.DoesExist; }
            DbListViewControlStatus = StateIs.Initialized;
            return DbListViewControlStatus;
        }
        public virtual StateIs ControlCreate()
        {
            // Db List View Control.
            // Here, the control is integrated with
            // the DbListView.
            // It can be completely independent of the
            // Grid View or either can be extendend and
            // customized.
            // I would expect that this is always integrated.
            // When its false the control is externally
            // managed, if it exists at all.
            // This will not be touched. For example 
            // like the externalGridView it will 
            // not be instantiated.
            if (GridView == null) { }
            if (DbListViewControl == null)
            {
                InitializeDbListView();
            }
            else
            {
                DbListViewControlStatus = StateIs.DoesExist;
                if (GridView == null)
                {
                    InitializeGridView();
                }
            }
            return DbListViewControlStatus;
        }
        public virtual StateIs ControlDispose()
        {
            DbListViewControl.Dispose(Status);
            DbListViewControl = null;
            return StateIs.Finished;
        }
        #endregion
    }
}