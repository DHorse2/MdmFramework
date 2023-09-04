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
//using System.Threading.Tasks;
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
    public class DbListViewControlDef : StdBaseDef, IDisposable
    // : StdDef, IDisposable
    // : StdConsoleManagerDef, IDisposable
    {
        #region Standard Data Fields
        // st with either be the following this pointer
        // or it will be the instantiated object in an app.
        public new StdConsoleManagerDef st; // always this
        public mFileSqlConnectionDef DbFile; // from parent or null
        //public new DbListViewDef RunControlUi.DbListView; ToDo (?Cast?)
        public DbDataDef DbData; // Passed?
        public bool DbListViewHidden; // There is a hide button.
        // External Form
        Form ParentForm; // UI messages? Button changes.
        #endregion
        #region Main Menu and Button Bars
        public TableLayoutPanel DbListViewLayoutPanel;
        public static int ControlMargin = 8;
        public Padding ControlPadding = new Padding(ControlMargin, 0, ControlMargin, 0);
        #region DbList ViewHeader Button Bar
        public ToolBar ViewHeaderButtonBar;
        #region DbList ViewHeader Description Strip
        public MenuStrip ViewHeaderButtonStrip;
        /// <summary>
        /// /
        /// </summary>
        public ToolStripLabel LabelViewName;
        // Heading.
        // Database (currently readonly)
        public ToolStripLabel LabelViewDatabase;
        public ToolStripTextBox TextBoxViewDatabase;
        // DataGridView DbList
        public ToolStripTextBox TextBoxDbListSelected;
        // Row Count
        public ToolStripTextBox TextBoxDbListCount;
        #endregion
        #region DbList Run Selection Button Strip (Script Specific)
        public MenuStrip ViewRunSelectionButtonStrip;
        // Selection
        // ScriptName Filter (prefix).
        public ToolStripLabel LabelScriptFilter;
        public ToolStripTextBox TextBoxScriptFilter;
        public string ScriptFilterPrev;
        // ComboBox NOT in use. 202101 Dgh ToDo. Create classes for these controls per stack exchange.
        // Field used are defaults. In use.
        public ToolStripLabel LabelFieldUsed;
        public ToolStripComboBox ComboBoxFieldUsed; // Defalut?
        public ToolStripTextBox TextBoxFieldUsed; // Defalut for entry.
                                                  // Action
        public ToolStripLabel LabelOutputAction;
        public ToolStripComboBox ComboBoxOutputAction; // Defalut?
        public ToolStripTextBox TextBoxOutputAction; // Defalut for entry.
                                                     // Database Refresh and Clear.
        public ToolStripButton ButtonDbRefresh;
        public ToolStripButton ButtonDbClear; // WARNING bulk delete.
        #endregion
        #region DbList Run Progess and control.
        public MenuStrip ViewRunProgressButtonStrip;

        // Run Progess and control.
        public ToolStripTextBox TextBoxDirectoryCount;
        public ToolStripTextBox TextBoxFileCount;
        #endregion
        #region Right Edge Message, Hide and Close
        public MenuStrip ViewRightEdgeButtonStrip;
        // Hide 
        public ToolStripButton ButtonHide;
        public ToolStripButton ButtonClose;
        #endregion
        #endregion
        #region ViewFooter Button Bar
        public ToolBar ViewFooterButtonBar;
        #endregion
        #region Edit Buttons
        public MenuStrip ViewEditButtonStrip;

        // Datagrid (and File) controls.
        private ToolStripButton ButtonUp;
        private ToolStripButton ButtonDn;
        private ToolStripButton ButtonAdd;
        private ToolStripButton ButtonIns;
        private ToolStripButton ButtonDel;
        #endregion
        #region Run Options
        public MenuStrip ViewRunOptionsButtonStrip;

        public ToolStripButton CheckBoxChangeValid;
        public ToolStripButton CheckBoxDeleteBad;
        public ToolStripButton CheckBoxDeleteFound;
        #endregion
        #region Actions
        public MenuStrip ViewActionButtonStrip;
        public ToolStripButton ButtonChildForm;
        #endregion
        #endregion
        #region File Sql Thread Management
        public bool RefreshIsBusy = false;
        public bool DataInsertSkipRowEnter = false;
        public EventArgs DataInsertSkipRowEnterArgs = null;
        public volatile int DbTaskIdBusyValue;
        public int DbTaskIdBusy
        {
            get { return DbTaskIdBusyValue; }
            set
            {
                if (st != null)
                {
                    string TraceMessage = "Calculation Task DbTaskIdBusy (" + DbTaskIdBusyValue.ToString() + ") now set to " + value.ToString();
                    st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
                }
                DbTaskIdBusyValue = value;
            }
        }
        #endregion
        #region Initialization, Loads, Meta Data and Control
        #region Constructors
        public DbListViewControlDef(
            ref object SenderPassed,
            ref StdConsoleManagerDef stPassed,
            ref DbListViewDef DbListViewPassed,
            ref DataGridView GridViewPassed,
            ref mFileSqlConnectionDef DbFilePassed,
            bool GridViewIsExternalPassed,
            bool UseConsoleButtonsPassed)
        {
            // Note. This is now the only constructor.
            // This did temporarily inherit StdConsoleManagerDef
            // but that was experimental and for debugging classes.
            // Initialize Fields
            DbFile = DbFilePassed;
            #region Initialize Console
            Sender = SenderPassed;
            FormParent = SenderPassed as iStdBaseForm; // AppStd
            if (stPassed != null)
            {
                st = stPassed; base.st = st; ConsoleSender = st;
            }
            else if (((StdConsoleManagerDef)DbListView).st != null)
            {
                st = ((StdConsoleManagerDef)DbListView).st; base.st = st; ConsoleSender = st;
            }
            if (stPassed == null)
            {
                st = new StdConsoleManagerDef(ConsoleSourceIs.Self, ClassRoleIs.None, ClassFeatureIs.None);
                base.st = st; ConsoleSender = st;
            }
            //
            ConsoleSource = st.ConsoleSource;
            ClassRole = st.ClassRole;
            ClassFeatures = st.ClassFeatures;
            // st.StdConsoleSet(st, st.ClassRole, st.ClassFeatures);
            #endregion
            StdRunControlUi = st.StdRunControlUi;
            DbListView = DbListViewPassed;
            GridView = GridViewPassed;
            GridViewColCurr = GridView.Columns.Count;
            GridViewIsExternal = GridViewIsExternalPassed;
            UseConsoleButtons = UseConsoleButtonsPassed;

            GridViewStatus = StateIs.DoesExist;
            Status = Initialize(ref GridView);
            // Status = StateIs.Started;
        }
        public DbListViewControlDef()
        {
            st = new StdConsoleManagerDef(ConsoleSourceIs.Self, ClassRoleIs.None, ClassFeatureIs.None);
            // Note. No Sql Connection, This may not work.
            StdRunControlUi = st.StdRunControlUi;
            //
            GridView = new DataGridView();
            GridViewIsExternal = false;
            GridViewStatus = StateIs.DoesExist;
            // GridViewStatus = Initialize(ref GridView);
            DbListView = null; // ?
            GridViewColCurr = 2;
            Status = Initialize(ref GridView);
            // Status = StateIs.Started;
        }
        //public DbListViewControlDef(StdConsoleManagerDef stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed,
        //    ref DataGridView GridViewPassed)
        //   : base(stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        //{
        //    // StdConsoleSet(stPassed, ClassRolePassed, ClassFeaturesPassed);
        //    GridView = GridViewPassed;
        //    GridViewIsExternal = true;
        //    GridViewStatus = StateIs.DoesExist;
        //    DbListView = null; // ?
        //    Status = this.Initialize(ref GridView);
        //    // Status = StateIs.Started;
        //}
        //public DbListViewControlDef(StdConsoleManagerDef stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        //   : base(stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        //{
        //    // StdConsoleSet(stPassed, ClassRolePassed, ClassFeaturesPassed);
        //    // Note, no sql connection
        //    GridView = new DataGridView();
        //    GridViewIsExternal = false;
        //    GridViewStatus = StateIs.DoesExist;
        //    DbListView = null; // ?
        //    GridViewIsExternal = false;
        //    Status = this.Initialize(ref GridView);
        //    // Status = StateIs.Started;
        //}
        #endregion
        public StateIs Initialize(ref DataGridView GridViewPassed)
        {
            base.InitializeStd();
            return Status = InitializeComponentForm(ref GridViewPassed);
        }
        public new void Dispose()
        {
            Dispose(Status);
            base.Dispose();
            Status = StateIs.DoesNotExist;
        }
        public new void Dispose(StateIs StatusPassed)
        {
            Status = StateIs.DoesNotExist;
        }
        public void Clear() { }
        public virtual StateIs OnLoad(EventArgs e)
        {
            // base.OnLoad(e);
            return Status = StateIs.Started;
            //Notes:
            // PopulateDataGridView();
            //UpdateLabelText();
            //UpdateBalance();

            //CrList.CellValidating += new
            //    DataGridViewCellValidatingEventHandler(
            //    CellValidating);
            //CrList.CellValidated += new DataGridViewCellEventHandler(
            //    CellValidated);
            //CrList.CellValueChanged += new DataGridViewCellEventHandler(
            //    CellValueChanged);
            //CrList.RowsRemoved += new DataGridViewRowsRemovedEventHandler(
            //    RowsRemoved);
            //CrList.SelectionChanged += new EventHandler(
            //    SelectionChanged);
            //CrList.UserAddedRow += new DataGridViewRowEventHandler(
            //    UserAddedRow);
            //CrList.UserDeletingRow += new
            //    DataGridViewRowCancelEventHandler(UserDeletingRow);

        }
        public virtual StateIs OnLoadDatabase()
        {
            StdRunControlUi.ButtonStart.Enabled = true;
            StdRunControlUi.ButtonFile.Enabled = true;
            ButtonChildForm.Enabled = true;
            // 202012 dgh - Cleanup functions
            CheckBoxChangeValid.Enabled = true;
            CheckBoxDeleteBad.Enabled = true;
            CheckBoxDeleteFound.Enabled = true;
            // 202012 dgh end - Cleanup functions
            StdRunControlUi.ButtonCancel.Enabled = false;
            StdRunControlUi.ButtonPause.Enabled = false;
            ButtonDbRefresh.Enabled = true;
            ButtonDbClear.Enabled = true;
            return StateIs.Finished;
        }
        #endregion
        public StateIs InitializeComponentFormResult;
        public StateIs InitializeComponentForm(ref DataGridView DbListViewPassed)
        {
            InitializeComponentFormResult = StateIs.Started;
            #region DbList ViewHeader Button Bar (Toolbar 1)
            ViewHeaderButtonBar = new ToolBar();
            #region DbList ViewHeader Description Strip
            ViewHeaderButtonStrip = new MenuStrip();
            // Note: Cr stands for Convert / Replace btw.
            LabelViewName = new ToolStripLabel();
            LabelViewName.Text = "Use %'s in ScriptNameFilter. Sequence of Search Replace Strings to apply during search:";
            LabelViewName.AutoSize = true;

            LabelViewDatabase = new ToolStripLabel();
            TextBoxViewDatabase = new ToolStripTextBox(); // readonly

            TextBoxDbListSelected = new ToolStripTextBox();
            TextBoxDbListCount = new ToolStripTextBox();
            #endregion
            #region DbList Run Selection Button Strip (Script Specific)
            ViewRunSelectionButtonStrip = new MenuStrip();
            LabelScriptFilter = new ToolStripLabel();
            TextBoxScriptFilter = new ToolStripTextBox(); // and entry default.
            LabelFieldUsed = new ToolStripLabel();
            TextBoxFieldUsed = new ToolStripTextBox(); // entry default.
            LabelOutputAction = new ToolStripLabel();
            TextBoxOutputAction = new ToolStripTextBox(); // entry default.
            #endregion
            #region DbList Run Progess and control.
            ViewRunProgressButtonStrip = new MenuStrip();
            // Run progress and control
            TextBoxDirectoryCount = new ToolStripTextBox();
            TextBoxFileCount = new ToolStripTextBox();
            StdRunControlUi.ButtonPause = new ToolStripButton();
            StdRunControlUi.ButtonCancel = new ToolStripButton();
            #endregion
            #region Right Edge Message, Hide and Close Strip
            ViewRightEdgeButtonStrip = new MenuStrip();

            ButtonDbClear = new ToolStripButton(); // bulk script delete WARNING!
            ButtonDbRefresh = new ToolStripButton();
            StdRunControlUi.LabelDbBusyMessage = new ToolStripLabel();
            ButtonHide = new ToolStripButton();
            ButtonClose = new ToolStripButton();
            #endregion
            #endregion
            #region ViewFooter Bar (Toolbar 2)
            ViewFooterButtonBar = new ToolBar();
            #region Edit Buttons Strip
            ViewEditButtonStrip = new MenuStrip();
            ButtonUp = new ToolStripButton();
            ButtonDn = new ToolStripButton();
            ButtonAdd = new ToolStripButton();
            ButtonIns = new ToolStripButton();
            ButtonDel = new ToolStripButton();
            #endregion
            #region Run Options Strip
            ViewRunOptionsButtonStrip = new MenuStrip();
            // ViewFooter Run Options
            CheckBoxChangeValid = new ToolStripButton();
            CheckBoxDeleteBad = new ToolStripButton();
            CheckBoxDeleteFound = new ToolStripButton();
            #endregion
            #region Actions Strip (Verbs, etc.)
            ViewActionButtonStrip = new MenuStrip();
            StdRunControlUi.ButtonStart = new ToolStripButton();
            StdRunControlUi.ButtonFile = new ToolStripButton();
            ButtonChildForm = new ToolStripButton();
            #endregion
            #endregion
            // (Toolbar 3).
            #region DataGridView Window Control
            ViewHeaderButtonStrip = new MenuStrip();
            LabelViewName = new ToolStripLabel
            {
                Text = DbListViewPassed.Name + ":",
                //Text = "State View",
                AutoSize = true
            };
            ButtonHide = new ToolStripButton
            {
                Text = "-",
                Alignment = ToolStripItemAlignment.Right,
                BackColor = Color.DarkGray
            };
            ButtonHide.Click += new EventHandler(ButtonHide_Click);
            ButtonClose = new ToolStripButton
            {
                Text = "x",
                Alignment = ToolStripItemAlignment.Right,
                BackColor = Color.DarkGray
            };
            ButtonClose.Click += new EventHandler(ButtonClose_Click);

            #region Action Dropdown - FieldUsed, OutputAction
            FieldUsedList = new List<string>();
            FieldUsedListText = "";
            DbDataDef.GetAppendedFieldNameListStatic(ref FieldUsedList, ref FieldUsedListText);
            // I would say the following are the general abstractions:
            FieldUsedList.Add(", Item"); // the default
            FieldUsedListText += ", Item";
            FieldUsedList.Add(", Line");
            FieldUsedListText += ", Line";
            FieldUsedList.Add(", Field");
            FieldUsedListText += ", Field";

            // no Add, Insert, Create. The Update probably would create.
            OutputActionList = new List<string>();
            OutputActionList.Add("Convert");
            OutputActionList.Add("Delete");
            OutputActionList.Add("Find");
            OutputActionList.Add("Fix");
            OutputActionList.Add("Function");
            OutputActionList.Add("Rename");
            OutputActionList.Add("Replace");
            OutputActionList.Add("Search");
            OutputActionList.Add("Transform");
            OutputActionList.Add("Update");

            OutputActionListText += "Convert, ";
            OutputActionListText += "Delete, ";
            OutputActionListText += "Fix, ";
            OutputActionListText += "Find, ";
            OutputActionListText += "Function, ";
            OutputActionListText += "Rename, ";
            OutputActionListText += "Replace, ";
            OutputActionListText += "Search, ";
            OutputActionListText += "Transform, ";
            OutputActionListText += "Update";
            #endregion

            #region Build Form Box
            #region ViewHeader
            #region View Description Header
            ViewHeaderButtonStrip.Items.AddRange(new ToolStripLabel[] {
                LabelViewDatabase
            });
            ViewHeaderButtonStrip.Items.AddRange(new ToolStripTextBox[] {
                TextBoxViewDatabase,
                TextBoxDbListSelected,
                TextBoxDbListCount
            });
            #endregion
            #region Selection
            ViewRunSelectionButtonStrip.Items.AddRange(new ToolStripLabel[] {
                LabelScriptFilter
            });
            ViewRunSelectionButtonStrip.Items.AddRange(new ToolStripTextBox[] {
                TextBoxScriptFilter
            });
            // Editing defaults
            ViewRunSelectionButtonStrip.Items.AddRange(new ToolStripLabel[] {
                LabelFieldUsed
            });
            ViewRunSelectionButtonStrip.Items.AddRange(new ToolStripTextBox[] {
                TextBoxFieldUsed
            });
            ViewRunSelectionButtonStrip.Items.AddRange(new ToolStripLabel[] {
                LabelOutputAction
            });
            ViewRunSelectionButtonStrip.Items.AddRange(new ToolStripTextBox[] {
                TextBoxOutputAction
            });
            // Database level actions.
            ViewRunSelectionButtonStrip.Items.AddRange(new ToolStripButton[] {
                ButtonDbRefresh,
                ButtonDbClear
           });
            #endregion
            #region DbList Run Progess and control.
            ViewRunProgressButtonStrip.Items.AddRange(new ToolStripTextBox[] {
                TextBoxDirectoryCount,
                TextBoxFileCount
           });
            ViewRunProgressButtonStrip.Items.AddRange(new ToolStripButton[] {
                StdRunControlUi.ButtonPause,
                StdRunControlUi.ButtonCancel
           });
            #endregion
            #region Right aligned stuff
            ViewRightEdgeButtonStrip.Items.AddRange(new ToolStripLabel[] {
                StdRunControlUi.LabelDbBusyMessage
           });
            ViewRightEdgeButtonStrip.Items.AddRange(new ToolStripButton[] {
                ButtonHide,
                ButtonClose
           });
            #endregion
            #endregion
            #region ViewFooters
            #region Edit Grid Buttons
            ViewEditButtonStrip.Items.AddRange(new ToolStripButton[] {
                ButtonUp,
                ButtonDn,
                ButtonAdd,
                ButtonIns,
                ButtonDel,
           });
            #endregion
            #region Actions
            ViewActionButtonStrip.Items.AddRange(new ToolStripButton[] {
                StdRunControlUi.ButtonStart,
                StdRunControlUi.ButtonFile,
                ButtonChildForm
           });
            #endregion
            #region Run Options
            ViewRunOptionsButtonStrip.Items.AddRange(new ToolStripButton[] {
                CheckBoxChangeValid,
                CheckBoxDeleteBad,
                CheckBoxDeleteFound
           });
            #endregion
            #endregion
            // DbListView Creation
            #region DbListView Form Control
            DbListViewPassed = new DataGridView();
            DbListViewLayoutPanel = new TableLayoutPanel();
            InitializeComponentFormResult = InitializeDbList(ref DbListViewPassed);
            // Events
            InitializeComponentFormResult = InitializeDbListEvents(ref DbListViewPassed);
            // Button Bar
            InitializeComponentFormResult = InitializeComponentButtons(ref DbListViewPassed);
            #endregion
            #endregion
            #region Main Form Table Layout Panel
            DbListViewLayoutPanel.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                                | AnchorStyles.Left)
                                | AnchorStyles.Right)));
            DbListViewLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            // Panels.
            int PanelRowCountPrev = DbListViewLayoutPanel.RowCount;
            DbListViewLayoutPanel.RowCount += 3;
            #region Styles
            // NOTE. Must equal 100 when you make changes or it glitches.
            DbListViewLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 4F)); // ViewHeader Line
            DbListViewLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 27F));  // DbList Grid View
            DbListViewLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));  // ViewFooter Line
            #endregion

            // DbListViewLayoutPanel.
            // DbListViewLayoutPanel.Controls.Add(LabelCrListName, 0, 0);
            DbListViewLayoutPanel.Controls.Add(ViewHeaderButtonStrip, 0, PanelRowCountPrev + 0);
            DbListViewLayoutPanel.Controls.Add(ViewRunSelectionButtonStrip, 0, PanelRowCountPrev + 0);
            DbListViewLayoutPanel.Controls.Add(ViewRunProgressButtonStrip, 0, PanelRowCountPrev + 0);
            DbListViewLayoutPanel.Controls.Add(ViewRightEdgeButtonStrip, 0, PanelRowCountPrev + 0);


            DbListViewLayoutPanel.Controls.Add(DbListViewPassed, 0, PanelRowCountPrev + 1);

            DbListViewLayoutPanel.Controls.Add(ViewEditButtonStrip, 0, PanelRowCountPrev + 2);
            DbListViewLayoutPanel.Controls.Add(ViewRunOptionsButtonStrip, 0, PanelRowCountPrev + 2);
            DbListViewLayoutPanel.Controls.Add(ViewActionButtonStrip, 0, PanelRowCountPrev + 2);

            // DbListViewLayoutPanel.Controls.Add(DbListViewName, 0, 3);

            // Standard Components
            ViewHeaderButtonStrip.Items.AddRange(new ToolStripLabel[] {
                    LabelViewName,
                    LabelViewDatabase
               });
            ViewHeaderButtonStrip.Items.AddRange(new ToolStripTextBox[] {
                    TextBoxViewDatabase,
                    TextBoxDbListSelected,
                    TextBoxDbListCount
               });
            ViewRightEdgeButtonStrip.Items.AddRange(new ToolStripLabel[] {
                    StdRunControlUi.LabelDbBusyMessage
               });
            ViewRightEdgeButtonStrip.Items.AddRange(new ToolStripButton[] {
                    ButtonHide,
                    ButtonClose
               });


            DbListViewLayoutPanel.Controls.Add(ViewHeaderButtonStrip, 0, 3);
            DbListViewLayoutPanel.Controls.Add(DbListViewPassed, 0, 4);

            DbListViewLayoutPanel.Location = new System.Drawing.Point(ControlMargin, ControlMargin);
            DbListViewLayoutPanel.Name = "DbListViewLayoutPanel";

            //DbListViewLayoutPanel.RowCount = 1;
            //DbListViewLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            // DbListViewLayoutPanel.Size = new System.Drawing.Size(250, 200);
            DbListViewLayoutPanel.Dock = DockStyle.Fill;

            DbListViewLayoutPanel.TabIndex = 1;

            //DbListViewLayoutPanel.Container.Add(CrList);
            //DbListViewLayoutPanel.Container.Add(DbItems);
            //DbListViewLayoutPanel.Container.Add(ControlClipboardList);
            //DbListViewLayoutPanel.Container.Add(LogOutRichTextBox1);
            //FormParent.Controls.Add(DbListViewLayoutPanel);
            #endregion

            DbListViewLayoutPanel.ClientSize = new System.Drawing.Size(1280, 700);
            // ToDo
            //Controls.AddRange(new Control[]{
            //LogOutRichTextBox1,
            //ControlClipboardList,
            //DbItems,
            //Controls.AddRange(new Control[]{
            //    CrList,
            //    DbItems,
            //    ControlClipboardList,
            //    LogOutRichTextBox1
            //        });

            //Menu = MenuMain;
            //Text = "Shortcut Repair Browser";
            return StateIs.Finished;
        }
        public virtual StateIs InitializeComponentButtons(ref DataGridView ListPassed)
        {
            //
            //MenuMain.Items.AddRange(new ToolStripButton[] {
            // ViewFooterButtonStrip.Items.AddRange(new ToolStripButton[] {
            //     MenuButtonPrev,
            //     MenuButtonNext,
            //     MenuButtonSet,
            //     MenuButtonStart
            //});
            // MenuButtonPrev

            // SRT Run Toolbar
            // Note: Cr stands for Convert / Replace btw.
            #region Srt Run Heading (Toolbar 1)
            LabelViewName = new ToolStripLabel();
            LabelViewName.Text = "Use %'s in ScriptNameFilter. Sequence of Search Replace Strings to apply during search:";
            LabelViewName.AutoSize = true;

            LabelViewDatabase = new ToolStripLabel();
            TextBoxViewDatabase = new ToolStripTextBox(); // readonly
            LabelScriptFilter = new ToolStripLabel();
            TextBoxScriptFilter = new ToolStripTextBox(); // and entry default.
            LabelFieldUsed = new ToolStripLabel();
            TextBoxFieldUsed = new ToolStripTextBox(); // entry default.
            LabelOutputAction = new ToolStripLabel();
            TextBoxOutputAction = new ToolStripTextBox(); // entry default.

            ButtonDbClear = new ToolStripButton(); // bulk script delete WARNING!
            ButtonDbRefresh = new ToolStripButton();
            StdRunControlUi.LabelDbBusyMessage = new ToolStripLabel();
            ButtonHide = new ToolStripButton();
            #endregion
            // Grid view data
            ButtonUp.Text = "Up";
            ButtonUp.Click += new EventHandler(ButtonUp_Click);
            //MenuButtonPrev.DisplayStyle = ToolBarButtonStyle.PushButton;
            // MenuButtonNext
            ButtonDn.Text = "Dn";
            ButtonDn.Click += new EventHandler(ButtonDn_Click);
            // MenuButtonSet
            ButtonIns.Text = "Ins";
            ButtonIns.Click += new EventHandler(ButtonIns_Click);
            // MenuButtonSet
            ButtonAdd.Text = "Add";
            ButtonAdd.Click += new EventHandler(ButtonAdd_Click);
            // MenuButtonStart
            ButtonDel.Text = "Del";
            ButtonDel.Click += new EventHandler(ButtonDel_Click);
            // MenuButtonStart
            StdRunControlUi.ButtonStart.Text = "Directory";
            StdRunControlUi.ButtonStart.Click += new EventHandler(ButtonStart_Click);
            StdRunControlUi.ButtonStart.Enabled = false;
            StdRunControlUi.ButtonStart.Margin = ControlPadding;
            // MenuButtonFile
            StdRunControlUi.ButtonFile.Text = "File";
            StdRunControlUi.ButtonFile.Click += new EventHandler(ButtonFile_Click);
            StdRunControlUi.ButtonFile.Enabled = false;
            StdRunControlUi.ButtonFile.Margin = ControlPadding;
            // MenuButtonChildForm
            ButtonChildForm.Text = "Other Form (ie Analysis)";
            ButtonChildForm.Click += new EventHandler(ButtonChildForm_Click);
            ButtonChildForm.Enabled = false;
            ButtonChildForm.BackColor = Color.LightGreen;
            ButtonChildForm.Margin = ControlPadding;
            // CheckBoxChangeValid
            CheckBoxChangeValid.Text = "Fix";
            CheckBoxChangeValid.Click += new EventHandler(CheckBoxChangeValid_Click);
            CheckBoxChangeValid.Enabled = false;
            CheckBoxChangeValid.CheckOnClick = true;
            CheckBoxChangeValid.BackColor = Color.LightGray;
            CheckBoxChangeValid.Margin = ControlPadding;
            // CheckBoxDeleteBad
            CheckBoxDeleteBad.Text = "Delete Bad";
            CheckBoxDeleteBad.Click += new EventHandler(CheckBoxDeleteBad_Click);
            CheckBoxDeleteBad.Enabled = false;
            CheckBoxDeleteBad.CheckOnClick = true;
            CheckBoxDeleteBad.BackColor = Color.LightGray;
            CheckBoxDeleteBad.Margin = ControlPadding;
            // CheckBoxDeleteFound
            CheckBoxDeleteFound.Text = "Delete Found";
            CheckBoxDeleteFound.Click += new EventHandler(CheckBoxDeleteFound_Click);
            CheckBoxDeleteFound.Enabled = false;
            CheckBoxDeleteFound.CheckOnClick = true;
            CheckBoxDeleteFound.BackColor = Color.LightGray;
            CheckBoxDeleteFound.Margin = ControlPadding;
            // Directory Count
            TextBoxDirectoryCount.Text = "0";
            // Shortcut file count
            TextBoxFileCount.Text = "0";
            // MenuButtonPause
            StdRunControlUi.ButtonPause.Text = "Pause";
            StdRunControlUi.ButtonPause.Click += new EventHandler(ButtonPause_Click);
            StdRunControlUi.ButtonCancel.BackColor = Color.LightGreen;
            StdRunControlUi.ButtonPause.Margin = ControlPadding;
            StdRunControlUi.ButtonPause.Enabled = false;
            // MenuButtonCancel
            StdRunControlUi.ButtonCancel.Text = "Cancel";
            StdRunControlUi.ButtonCancel.Click += new EventHandler(ButtonCancel_Click);
            StdRunControlUi.ButtonCancel.BackColor = Color.Gray;
            StdRunControlUi.ButtonCancel.Margin = ControlPadding;
            StdRunControlUi.ButtonCancel.Enabled = false;
            // BusyMessage
            StdRunControlUi.LabelDbBusyMessage.Font =
                new Font(StdRunControlUi.LabelDbBusyMessage.Font, FontStyle.Bold);
            StdRunControlUi.LabelDbBusyMessage.Text = " Busy please wait..."; // Message is blank when no BG tasks
            StdRunControlUi.LabelDbBusyMessage.BackColor = Color.White;
            StdRunControlUi.LabelDbBusyMessage.ForeColor = Color.Red;
            StdRunControlUi.LabelDbBusyMessage.Alignment = ToolStripItemAlignment.Right;
            return StateIs.Finished;
        }
        #region DataGridView Initialize and Load
        public virtual StateIs InitializeDbList(ref DataGridView ListPassed)
        {
            ListPassed.Name = "DbStateView";
            ListPassed.ColumnCount = ListPassed.ColumnCount; // 18;
            ListPassed.RowHeadersVisible = false;
            ListPassed.AllowUserToAddRows = false;
            ListPassed.SortCompare += new DataGridViewSortCompareEventHandler(CellSortCompare);
            ListPassed.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            //ListPassed.SelectionMode =
            //    DataGridViewSelectionMode.RowHeaderSelect;
            ListPassed.MultiSelect = false;
            ListPassed.AllowUserToAddRows = true;
            #region Docking and Formatting
            ListPassed.Dock = DockStyle.Fill;
            ListPassed.Anchor =
            AnchorStyles.Bottom |
            AnchorStyles.Right |
            AnchorStyles.Top |
            AnchorStyles.Left;
            ListPassed.Margin = new Padding(ControlMargin);
            #endregion
            #region Styles, Colours and Fonts
            ListPassed.GridColor = Color.Black;
            ListPassed.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            ListPassed.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            ListPassed.ColumnHeadersDefaultCellStyle.Font =
                new Font(ListPassed.Font, FontStyle.Bold);
            ListPassed.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Raised;
            ListPassed.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            #endregion
            #region Size and Positioning
            ListPassed.Location = new Point(ControlMargin, 221);
            ListPassed.MinimumSize = new Size(300, 40);
            // ListPassed.MaximumSize = new Size(850, 200);
            ListPassed.Size = new Size(620, 100);
            ListPassed.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            #endregion
            #region Columns (Id, Valid)
            #region Code Snipets for Column Loop
            //// datagrid has calculated it's widths so we can store them
            //foreach (DataGridViewColumn column in ListPassed.Columns)
            //{
            //    // store autosized widths
            //    int colw = column.Width;
            //    // remove autosizing
            //    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //    // set width to calculated by autosize
            //    column.Width = colw;
            //}
            //For i = 0 To ListPassed.Columns.Count - 1
            //Next
            #endregion
            #region Field Column Index set Name
            GridViewColCurr = ListPassed.ColumnCount;
            ListPassed.ColumnCount = GridViewColCurr + 2;
            ListPassed.Columns[GridViewColCurr + 0].Name = "Id";
            ListPassed.Columns[GridViewColCurr + 1].Name = "Valid";
            GridViewColCurr = ListPassed.ColumnCount;
            #endregion
            #region Column Field Details
            ListPassed.Columns["Id"].ReadOnly = true;
            ListPassed.Columns["Id"].Visible = true;
            ListPassed.Columns["Id"].Width = 30;
            ListPassed.Columns["Id"].SortMode = DataGridViewColumnSortMode.Automatic;
            //ListPassed.Columns["PathOnly"].Width = 30;
            //ListPassed.Columns["PathOnly"].SortMode = DataGridViewColumnSortMode.Automatic;
            //ListPassed.Columns["FileNameOnly"].Width = 50;
            //ListPassed.Columns["FileNameOnly"].SortMode = DataGridViewColumnSortMode.Automatic;
            //ListPassed.Columns["TargetPath"].Width = 50;
            //ListPassed.Columns["TargetPath"].SortMode = DataGridViewColumnSortMode.Automatic;
            //ListPassed.Columns["WorkingDirectory"].Width = 50;
            //ListPassed.Columns["WorkingDirectory"].SortMode = DataGridViewColumnSortMode.Automatic;
            //// ListPassed.Columns["WorkingDirectory"].Visible = false;
            //ListPassed.Columns["Description"].Width = 50;
            //ListPassed.Columns["Description"].SortMode = DataGridViewColumnSortMode.Automatic;
            //ListPassed.Columns["HotKey"].Width = 50;
            //ListPassed.Columns["HotKey"].SortMode = DataGridViewColumnSortMode.Automatic;
            //ListPassed.Columns["FullName"].Width = 50;
            //ListPassed.Columns["FullName"].SortMode = DataGridViewColumnSortMode.Automatic;
            //ListPassed.Columns["IconLocation"].Width = 50;
            //ListPassed.Columns["IconLocation"].SortMode = DataGridViewColumnSortMode.Automatic;

            //ListPassed.Columns["File"].Width = 30;
            //ListPassed.Columns["File"].SortMode = DataGridViewColumnSortMode.Automatic;
            //ListPassed.Columns["Target"].Width = 30;
            //ListPassed.Columns["Target"].SortMode = DataGridViewColumnSortMode.Automatic;
            //ListPassed.Columns["Work"].Width = 30;
            //ListPassed.Columns["Work"].SortMode = DataGridViewColumnSortMode.Automatic;
            //ListPassed.Columns["Icon"].Width = 30;
            //ListPassed.Columns["Icon"].SortMode = DataGridViewColumnSortMode.Automatic;

            //ListPassed.Columns["Files"].Width = 30;
            //ListPassed.Columns["Files"].SortMode = DataGridViewColumnSortMode.Automatic;
            //ListPassed.Columns["Targets"].Width = 30;
            //ListPassed.Columns["Targets"].SortMode = DataGridViewColumnSortMode.Automatic;
            //ListPassed.Columns["Working"].Width = 30;
            //ListPassed.Columns["Working"].SortMode = DataGridViewColumnSortMode.Automatic;
            //ListPassed.Columns["Icons"].Width = 30;
            //ListPassed.Columns["Icons"].SortMode = DataGridViewColumnSortMode.Automatic;

            ListPassed.Columns["Valid"].Width = 30;
            ListPassed.Columns["Valid"].SortMode = DataGridViewColumnSortMode.Automatic;
            #endregion
            #region Column Autosize mode
            ListPassed.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //ListPassed.Columns["PathOnly"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //ListPassed.Columns["FileNameOnly"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //ListPassed.Columns["TargetPath"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //ListPassed.Columns["WorkingDirectory"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //ListPassed.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //ListPassed.Columns["HotKey"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //ListPassed.Columns["FullName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //ListPassed.Columns["IconLocation"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //ListPassed.Columns["File"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //ListPassed.Columns["Target"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //ListPassed.Columns["Work"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //ListPassed.Columns["Icon"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //ListPassed.Columns["Files"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //ListPassed.Columns["Targets"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //ListPassed.Columns["Working"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //ListPassed.Columns["Icons"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            ListPassed.Columns["Valid"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            #endregion
            #region Column Display Index
            ListPassed.Columns["Id"].DisplayIndex = ListPassed.ColumnCount - 1; // 10;
                                                                                //ListPassed.Columns["PathOnly"].DisplayIndex = 0;
                                                                                //ListPassed.Columns["FileNameOnly"].DisplayIndex = 1;

            //ListPassed.Columns["TargetPath"].DisplayIndex = 4;
            //ListPassed.Columns["WorkingDirectory"].DisplayIndex = 5;
            //ListPassed.Columns["Description"].DisplayIndex = 6;
            //ListPassed.Columns["HotKey"].DisplayIndex = 7;
            //ListPassed.Columns["FullName"].DisplayIndex = 8;
            //ListPassed.Columns["IconLocation"].DisplayIndex = 9;

            //ListPassed.Columns["File"].DisplayIndex = 2;
            //ListPassed.Columns["Target"].DisplayIndex = 11;
            //ListPassed.Columns["Work"].DisplayIndex = 12;
            //ListPassed.Columns["Icon"].DisplayIndex = 13;

            //ListPassed.Columns["Files"].DisplayIndex = 3;
            //ListPassed.Columns["Targets"].DisplayIndex = 14;
            //ListPassed.Columns["Working"].DisplayIndex = 15;
            //ListPassed.Columns["Icons"].DisplayIndex = 16;

            ListPassed.Columns["Valid"].DisplayIndex = ListPassed.ColumnCount - 2; // Validity
            #endregion
            #endregion
            return StateIs.Finished;
        }
        public virtual StateIs InitializeDbListEvents(ref DataGridView ListPassed)
        {
            ListPassed.CellFormatting += new
                DataGridViewCellFormattingEventHandler(
                CellFormatting);
            ListPassed.CellEndEdit += new DataGridViewCellEventHandler(CellChanged);
            ListPassed.RowEnter += new DataGridViewCellEventHandler(CellRowEnter);
            ListPassed.RowLeave += new DataGridViewCellEventHandler(CellRowLeave);
            ListPassed.SelectionChanged += new EventHandler(CellSelectionChanged);
            ListPassed.RowsRemoved += new DataGridViewRowsRemovedEventHandler(CellRowRemoved);
            return StateIs.Finished;
        }
        #endregion
        #endregion
        #region DbList Buttons
        #region DbList Button Results
        StateIs DbListSelectionProcessResult;
        StateIs ButtonUpResult;
        StateIs ButtonDnResult;
        StateIs ButtonInsResult;
        StateIs ButtonDelResult;
        StateIs ButtonAddResult;
        #endregion
        public void DbListSelectionProcess(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            string ErrorMsg = "CrList Selection Process not implemented!"
                + sender.ToString()
                + " Arguments: " + e.ToString();
            st.ExceptNotSupportedImpl(sender, null, ErrorMsg, 0);
        }
        #region DbList Edit Buttons
        private void ButtonUp_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            string ErrorMsg = "Move Row Up not implemented!";
            ErrorMsg += sender.ToString();
            st.ExceptNotSupportedImpl(sender, null, ErrorMsg, 0);
        }
        private void ButtonDn_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            string ErrorMsg = "Move Row Down not implemented!"
                + sender.ToString()
                + " Arguments: " + e.ToString();
            st.ExceptNotSupportedImpl(sender, null, ErrorMsg, 0);
            // st.TraceMdmDoBasic(ErrorMsg, ConsoleFormUses.DebugLog, 7);
        }
        private void ButtonDel_Click(object sender, EventArgs e)
        {
            StdRunControlUi.ButtonAction = ButtonActionIs.Delete;
            ButtonDelResult = DbFile.DataDeleteThread(((DbListViewDef)DbListView).DbData, st.StdRunControlUi.SetButtons);
            RowDelete(ref DbData, ref GridView);
            RowNumberSelectedLast = -1;
        }
        private void ButtonIns_Click(object sender, EventArgs e) { ButtonIns_Click(); }
        private void ButtonIns_Click()
        {
            StdRunControlUi.ButtonAction = ButtonActionIs.Insert;
            DbData = new DbDataDef();
            ButtonInsResult = DbFile.DataInsertThread(DbData, DbData.Id); // ToDo is this an ID???
            RowInsert(ref DbData, ref GridView);
            GridView.Invalidate();
        }
        private void ButtonAdd_Click(object sender, EventArgs e) { ButtonAdd_Click(); }
        private void ButtonAdd_Click()
        {
            StdRunControlUi.ButtonAction = ButtonActionIs.Insert;
            DbData = new DbDataDef();
            ButtonAddResult = DbFile.DataInsertThread(DbData, DbData.Id);
            RowAdd(ref DbData, ref GridView);
            GridView.Refresh();
        }
        #endregion
        #region Action Buttons
        public StateIs ParentFormOpen(object sender, EventArgs e)
        {
            DbTaskDef DbTaskLocal;
            if (sender != null && sender is DbTaskDef)
            {
                DbTaskLocal = (DbTaskDef)sender;
            }
            else
            {
                string LocalMessage = "Unexpected sender (DbTaskDef) error opening parent (Utils) window.";
                LocalMessage += ". Item: " + DbData.Id.ToString();
                st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 14);
                return StateIs.AbnormalEnd;
            }
            // 202101 dgh end - Add field select (field used) and Actions
            if (ParentForm != null)
            {
                // ToDo missing a parent form interface???
                // ParentForm.ButtonFile_Click(sender, e);
            }
            else
            {
                // throw new NotImplementedException();
                string ErrorMsg = "NotImplementedException. Opening parent (Utils) window not implemented.";
                ErrorMsg += DbTaskLocal.Id.ToString();
                ErrorMsg += "\n" + "Item: " + DbData.Id.ToString() + ".";
                ErrorMsg += " From: " + sender.ToString();
                st.ExceptNotSupportedImpl(sender, null, ErrorMsg, 0);
            }
            return StateIs.Finished;
        }
        public void ButtonFile_Click(object sender, EventArgs e)
        {
            //if (ParentForm != null)
            //{
            //    ParentForm.ButtonFile_Click(sender, e);
            //}
            //else
            //{
            //ParentFormOpen(sender, e);
            //}
        }
        public void ButtonStart_Click(object sender, EventArgs e)
        {
            StdRunControlUi.ButtonAction = ButtonActionIs.Execute;
            //if (ParentForm != null)
            //{
            //    ParentForm.ButtonStart_ClickThread();
            //}
            //else
            //{
            //    ParentFormOpen(sender, e);
            //}
        }
        public void ButtonChildForm_Click(object sender, EventArgs e)
        {
            StdRunControlUi.ButtonAction = ButtonActionIs.Execute;
            MainThreadIdDoSet = true;
            // ParentForm.OtherForm_Open(); or similar
            // ScAnalysis_ClickThread();
        }
        #endregion
        #region Run Options
        // CheckBoxChangeValid
        public void CheckBoxChangeValid_ClickDo(object sender, EventArgs e)
        {
            if (CheckBoxChangeValid.Checked)
            {
                CheckBoxChangeValid.Text = "Update";
                CheckBoxChangeValid.Font =
                    new Font(CheckBoxChangeValid.Font, FontStyle.Bold);
                CheckBoxChangeValid.ForeColor = Color.Red;
                // CheckBoxChangeValid.BackColor = Color.Black;
            }
            else
            {
                CheckBoxChangeValid.Text = "Fix";
                CheckBoxChangeValid.Font =
                    new Font(CheckBoxChangeValid.Font, FontStyle.Regular);
                CheckBoxChangeValid.ForeColor = Color.Black;
                // CheckBoxChangeValid.BackColor = Color.LightGray;
            }
        }
        // CheckBoxChangeValid
        public void CheckBoxChangeValid_Click(object sender, EventArgs e)
        {
            CheckBoxChangeValid_ClickDo(sender, e);
            //if (ParentForm != null)
            //{
            //    ParentForm.CheckBoxChangeValid.Checked = CheckBoxChangeValid.Checked;
            //    ParentForm.CheckBoxChangeValid_ClickDo(sender, e);
            //    ParentForm.CheckBoxChangeValid.Invalidate();
            //}
        }
        // CheckBoxDeleteBad
        public void CheckBoxDeleteBad_ClickDo(object sender, EventArgs e)
        {
            if (CheckBoxDeleteBad.Checked)
            {
                CheckBoxDeleteBad.Text = "DELETE Bad!!!";
                CheckBoxDeleteBad.Font =
                    new Font(CheckBoxDeleteBad.Font, FontStyle.Bold);
                CheckBoxDeleteBad.ForeColor = Color.Red;
            }
            else
            {
                CheckBoxDeleteBad.Text = "Delete Bad?";
                CheckBoxDeleteBad.Font =
                    new Font(CheckBoxDeleteBad.Font, FontStyle.Regular);
                CheckBoxDeleteBad.ForeColor = Color.Black;
            }
        }
        // CheckBoxDeleteBad
        public void CheckBoxDeleteBad_Click(object sender, EventArgs e)
        {
            CheckBoxDeleteBad_ClickDo(sender, e);
            //if (ParentForm != null)
            //{
            //    ParentForm.CheckBoxDeleteBad.Checked = CheckBoxDeleteBad.Checked;
            //    ParentForm.CheckBoxDeleteBad_ClickDo(sender, e);
            //    ParentForm.CheckBoxDeleteBad.Invalidate();
            //}
        }
        // CheckBoxDeleteFound
        public void CheckBoxDeleteFound_ClickDo(object sender, EventArgs e)
        {
            if (CheckBoxDeleteFound.Checked)
            {
                CheckBoxDeleteFound.Text = "DELETE Found!!!";
                CheckBoxDeleteFound.Font =
                    new Font(CheckBoxDeleteFound.Font, FontStyle.Bold);
                CheckBoxDeleteFound.ForeColor = Color.Red;
            }
            else
            {
                CheckBoxDeleteFound.Text = "Delete Found?";
                CheckBoxDeleteFound.Font =
                    new Font(CheckBoxDeleteFound.Font, FontStyle.Regular);
                CheckBoxDeleteFound.ForeColor = Color.Black;
            }
        }
        // CheckBoxDeleteFound
        public void CheckBoxDeleteFound_Click(object sender, EventArgs e)
        {
            CheckBoxDeleteFound_ClickDo(sender, e);
            //if (ParentForm != null)
            //{
            //    ParentForm.CheckBoxDeleteFound.Checked = CheckBoxDeleteFound.Checked;
            //    ParentForm.CheckBoxDeleteFound_ClickDo(sender, e);
            //    ParentForm.CheckBoxDeleteFound.Invalidate();
            //}
        }
        #endregion
        #region Run State and Filter Selection (Disabled)
        //public void TextBoxScriptFilter_LeaveDo(object sender, EventArgs e)
        //{
        //    if (ScriptNameFilter != TextBoxScriptFilter.Text)
        //    {
        //        ScriptNameFilter = TextBoxScriptFilter.Text;
        //        // CODE NORMALLY WOULD BE IN MAIN FORM.
        //    }
        //}
        //public void TextBoxScriptFilter_Leave(object sender, EventArgs e)
        //{
        //    TextBoxScriptFilter_LeaveDo(sender, e);
        //    if (ParentForm != null)
        //    {
        //        ParentForm.TextBoxScriptFilter.Text = TextBoxScriptFilter.Text;
        //        ParentForm.TextBoxScriptFilter_Leave(sender, e);
        //        ParentForm.TextBoxScriptFilter.Invalidate();
        //    }
        //}
        //public void TextBoxScriptFilter_GotFocusDo(object sender, EventArgs e)
        //{
        //    // Set Prev?
        //}
        //public void TextBoxScriptFilter_GotFocus(object sender, EventArgs e)
        //{
        //    TextBoxScriptFilter_GotFocusDo(sender, e);
        //    if (ParentForm != null)
        //    {
        //        ParentForm.TextBoxScriptFilter.Text = TextBoxScriptFilter.Text;
        //        ParentForm.TextBoxScriptFilter_GotFocus(sender, e);
        //        ParentForm.TextBoxScriptFilter.Invalidate();
        //    }
        //}
        #endregion
        #region Button Run Control (Disabled) - Pause, Cancel
        //public void ButtonPause_Click(object sender, EventArgs e)
        //{
        //    // throw new NotImplementedException();
        //    string ErrorMsg = "Pause not implemented!"
        //        + sender.ToString()
        //        + " Arguments: " + e.ToString();
        //    st.ExceptNotSupportedImpl(sender, null, ErrorMsg, 0);
        //}
        //public void ButtonCancel_Click(object sender, EventArgs e)
        //{
        //    RunControlUi.ButtonAction = ButtonActionIs.Execute;
        //    if (ScAnalysisTask != null && (ScAnalysisTask.CalcState == Thread.CalculationStatus.Calculating || ScAnalysisTask.CalcState == Thread.CalculationStatus.Pending))
        //    {
        //        string LocalMessage = "Cancelling Shortcut Analysis Task.";
        //        LocalMessage += sender.ToString();
        //        st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
        //        ScAnalysisTask.StopCalculation();
        //    }
        //    if (DbCmdTask != null && (DbCmdTask.CalcState == Thread.CalculationStatus.Calculating || DbCmdTask.CalcState == Thread.CalculationStatus.Pending))
        //    {
        //        string LocalMessage = "Cancelling SQL Command Task.";
        //        LocalMessage += sender.ToString();
        //        st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
        //        DbCmdTask.StopCalculation();
        //    }
        //    if (DbPopTask != null && (DbPopTask.CalcState == Thread.CalculationStatus.Calculating || DbPopTask.CalcState == Thread.CalculationStatus.Pending))
        //    {
        //        string LocalMessage = "Cancelling SQL Populate Form Task.";
        //        LocalMessage += sender.ToString();
        //        st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
        //        DbPopTask.StopCalculation();
        //    }
        //    if (ScAnalysisTask != null && (ScAnalysisTask.CalcState == Thread.CalculationStatus.Calculating || ScAnalysisTask.CalcState == Thread.CalculationStatus.Pending))
        //    {
        //        //if (MainThreadId >= 0)
        //        //{
        //        //    CalculationTaskDef.FileSqlWaitForBusy(MainThreadId, 10000);
        //        //}
        //        //else {
        //        string LocalMessage = "Waiting for Shortcut Analysis Task.";
        //        LocalMessage += sender.ToString();
        //        st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
        //        CalculationTaskUtilsDef.WaitForBusy(st, ScAnalysisTask.Id, 10000);
        //        LocalMessage = "Shortcut Analysis Task cancelled.";
        //        LocalMessage += sender.ToString();
        //        st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
        //        //}
        //    }
        //    //WaitForFileSqlBusy(FileSqlScTask.Id, 10000);
        //    //WaitForFileSqlBusy(FileSqlScCmdTask.Id, 10000);
        //    //WaitForFileSqlBusy(FileSqlScPopTask.Id, 10000);
        //}
        //public void ButtonDbRefresh_Click(object sender, EventArgs e)
        //{
        //    RefreshIsBusy = true;
        //    DbListView.Rows.Clear();
        //    DbListView.Refresh();
        //    // Load CrList database
        //    ScriptFile.Fmain.FileAction.ToDo = FileAction_Do.FileData;
        //    MainThreadIdDoSet = true;
        //    DbPopulate(ref ScriptFile, DbData, sEmpty, ReportOrderMain(), true, false, false, true);
        //    RefreshIsBusy = false;
        //}
        //public void ButtonDbClear_Click(object sender, EventArgs e)
        //{
        //    // Clear database

        //    // Refresh display
        //    RefreshIsBusy = true;
        //    DbListView.Rows.Clear();
        //    DbListView.Refresh();
        //    // Load CrList database
        //    ScriptFile.Fmain.FileAction.ToDo = FileAction_Do.FileData;
        //    MainThreadIdDoSet = true;
        //    DbPopulate(ref ScriptFile, DbData, sEmpty, ReportOrderMain(), true, false, false, true);
        //    RefreshIsBusy = false;

        //    // throw new NotImplementedException();
        //    string ErrorMsg = "Database Clear is not implemented!";
        //    ErrorMsg += "\n" + "From: " + sender.ToString();
        //    ErrorMsg += " Arguments: " + e.ToString();
        //    st.ExceptNotSupportedImpl(sender, null, ErrorMsg, 0);
        //}
        #endregion
        #region Hide buttons
        public bool ViewHidden = true;
        public void ButtonHide_Click(object sender, EventArgs e)
        {
            if (!ViewHidden)
            {
                ViewHidden = true;
                GridView.Visible = false;
                ViewFooterButtonBar.Visible = false;
                DbListViewLayoutPanel.RowStyles[6] = new RowStyle(SizeType.Absolute, 0F);
                ButtonHide.Text = "v";
            }
            else
            {
                ViewHidden = false;
                GridView.Visible = true;
                ViewFooterButtonBar.Visible = true;
                DbListViewLayoutPanel.RowStyles[6] = new RowStyle(SizeType.Percent, 21F);
                ButtonHide.Text = "^";
            }
            ButtonHide_ResizeControls();
        }
        private void ButtonHide_ResizeControls()
        {
            //if (DbListViewHidden && ViewHidden)
            //{
            //    DbListViewLayoutPanel.RowStyles[1] = new RowStyle(SizeType.Percent, 84F);
            //}
            //else if (DbListViewHidden || ViewHidden)
            //{
            //    DbListViewLayoutPanel.RowStyles[1] = new RowStyle(SizeType.Percent, 57F);
            //}
            //else
            //{
            //    DbListViewLayoutPanel.RowStyles[1] = new RowStyle(SizeType.Percent, 30F);
            //}
        }
        // ToDo - Destoy Control but not List
        public void ButtonClose_Click(object sender, EventArgs e)
        {
            // ToDo - Destoy Control but not List
            GridView.Visible = false;
            ViewHidden = true;
            //if (!ViewHidden)
            //{
            //    DbListView.Visible = false;
            //    DbListViewLayoutPanel.RowStyles[6] = new RowStyle(SizeType.Absolute, 0F);
            //    ViewHidden = true;
            //}
            //else
            //{
            //    DbListView.Visible = true;
            //    DbListViewLayoutPanel.RowStyles[6] = new RowStyle(SizeType.Percent, 21F);
            //    ViewHidden = false;
            //}
            //ButtonHide_ResizeControls();
        }
        #endregion
        #region ButtonPause_Click / Cancel_Click
        public void ButtonPause_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            string ErrorMsg = "Pause not implemented!";
            ErrorMsg += sender.ToString();
            ErrorMsg += " Arguments: " + e.ToString();
            st.ExceptNotSupportedImpl(sender, null, ErrorMsg, 0);
        }
        public void ButtonCancel_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            string ErrorMsg = "Cancel not implemented!";
            ErrorMsg += sender.ToString();
            ErrorMsg += " Arguments: " + e.ToString();
            st.ExceptNotSupportedImpl(sender, null, ErrorMsg, 0);

            // ToDo Cancel
            //RunControlUi.ButtonAction = ButtonActionIs.Execute;
            //if (ShortcutAnalysisForm.ScAnalysisTask != null && (ShortcutAnalysisForm.ScAnalysisTask.CalcState == Thread.CalculationStatus.Calculating || ShortcutAnalysisForm.ScAnalysisTask.CalcState == Thread.CalculationStatus.Pending))
            //{
            //    string LocalMessage = "Cancelling Shortcut Analysis Task.";
            //    LocalMessage += sender.ToString();
            //    st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
            //    ShortcutAnalysisForm.ScAnalysisTask.StopCalculation();
            //}
            //if (DbCmdTask != null && (DbCmdTask.CalcState == Thread.CalculationStatus.Calculating || DbCmdTask.CalcState == Thread.CalculationStatus.Pending))
            //{
            //    string LocalMessage = "Cancelling SQL Command Task.";
            //    LocalMessage += sender.ToString();
            //    st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
            //    DbCmdTask.StopCalculation();
            //}
            //if (DbPopTask != null && (DbPopTask.CalcState == Thread.CalculationStatus.Calculating || DbPopTask.CalcState == Thread.CalculationStatus.Pending))
            //{
            //    string LocalMessage = "Cancelling SQL Populate Form Task.";
            //    LocalMessage += sender.ToString();
            //    st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
            //    DbPopTask.StopCalculation();
            //}
            //if (ShortcutAnalysisForm.ScAnalysisTask != null && (ShortcutAnalysisForm.ScAnalysisTask.CalcState == Thread.CalculationStatus.Calculating || ShortcutAnalysisForm.ScAnalysisTask.CalcState == Thread.CalculationStatus.Pending))
            //{
            //    //if (MainThreadId >= 0)
            //    //{
            //    //    CalculationTaskDef.FileSqlWaitForBusy(MainThreadId, 10000);
            //    //}
            //    //else {
            //    string LocalMessage = "Waiting for Shortcut Analysis Task.";
            //    LocalMessage += sender.ToString();
            //    st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
            //    CalculationTaskUtilsDef.WaitForBusy(st, ShortcutAnalysisForm.ScAnalysisTask.Id, 10000);
            //    LocalMessage = "Shortcut Analysis Task cancelled.";
            //    LocalMessage += sender.ToString();
            //    st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
            //    //}
            //}

            //string LocalMessage = "Waiting for XXX Taks.";
            //LocalMessage += sender.ToString();
            //st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
            //WaitForFileSqlBusy(FileSqlScTask.Id, 10000);
            //WaitForFileSqlBusy(FileSqlScCmdTask.Id, 10000);
            //WaitForFileSqlBusy(FileSqlScPopTask.Id, 10000);
        }
        #endregion
        #endregion
        #region Form Components
        #region DataGrivView Row selection
        public int RowSelectListGet(DataGridView grid)
        {
            if (grid == null) { return 0; }
            if (grid.Rows.Count > 0)
            {
                try
                {
                    if (grid.Rows[grid.Rows.Count - 1].Selected)
                    {
                        if (RowNumberSelected >= 0)
                        {
                            if (RowSequenceNumberSelectedLast >= 0)
                            {
                                grid.Rows[RowSequenceNumberSelectedLast].Selected = false;
                            }
                            grid.Rows[RowNumberSelected].Selected = true;
                            RowSequenceNumberSelectedLast = RowNumberSelected;
                            //grid.CurrentCell = grid.Rows[SequenceNumberSelected].Cells[1];
                            return RowNumberSelected;
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                { }
                catch (ArgumentOutOfRangeException)
                { }
            }

            return 0;
        }
        public int RowSelectListSelect(DataGridView grid, int rowSelected)
        {
            if (grid == null) { return 0; }
            if (grid.Rows.Count > 0)
            {
                try
                {
                    if (grid.Rows[grid.Rows.Count - 1].Selected)
                    {
                        if (RowSequenceNumberSelectedLast >= 0)
                        {
                            grid.Rows[RowSequenceNumberSelectedLast].Selected = false;
                            RowSequenceNumberSelectedLast = rowSelected;
                        }
                        RowNumberSelected = rowSelected;
                        grid.Rows[rowSelected].Selected = true;
                        grid.CurrentCell = grid.Rows[rowSelected].Cells[1];
                        return RowNumberSelected;
                    }
                }
                catch (IndexOutOfRangeException)
                { }
                catch (ArgumentOutOfRangeException)
                { }
            }
            return grid.Rows.Count;
        }
        public int RowSelectListSelectLast(DataGridView grid)
        {
            if (grid == null) { return 0; }
            if (grid.Rows.Count > 0)
            {
                try
                {
                    grid.Rows[grid.Rows.Count - 1].Selected = true;
                    grid.CurrentCell = grid.Rows[grid.Rows.Count - 1].Cells[1];
                }
                catch (IndexOutOfRangeException)
                { }
                catch (ArgumentOutOfRangeException)
                { }
            }
            return grid.Rows.Count;
        }
        #endregion
        #region Grid Initialize and Formatting
        public void DbItemsInitialize(ref DataGridView DbListView)
        {
            //this.Controls.Add(songsDataGridView);

            DbListView.ColumnCount = 5;

            DbListView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            DbListView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DbListView.ColumnHeadersDefaultCellStyle.Font =
                new Font(DbListView.Font, FontStyle.Bold);

            DbListView.Name = "Converted Items";
            DbListView.Location = new Point(ControlMargin, 21);
            DbListView.MinimumSize = new Size(400, 50);
            //DbItems.MaximumSize = new Size(850,200);
            DbListView.Size = new Size(850, 200);
            DbListView.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            DbListView.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            DbListView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            DbListView.GridColor = Color.Black;
            DbListView.RowHeadersVisible = false;

            DbListView.Columns[0].Name = "Id";
            DbListView.Columns[1].Name = "Target";
            DbListView.Columns[2].Name = "Data1";
            DbListView.Columns[3].Name = "Content";
            DbListView.Columns[4].Name = "Sequence";
            DbListView.Columns[4].DefaultCellStyle.Font =
                new Font(
                    DbListView.DefaultCellStyle.Font, FontStyle.Italic);
            // DbItems.Columns["Content"].Width = 850;

            DbListView.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            DbListView.MultiSelect = false;
            DbListView.AllowUserToAddRows = false;

            DbListView.Dock = DockStyle.Fill;
            DbListView.Anchor =
            AnchorStyles.Bottom |
            AnchorStyles.Right |
            AnchorStyles.Top |
            AnchorStyles.Left;
            DbListView.Margin = new Padding(ControlMargin);
            DbListView.CellFormatting += new
                DataGridViewCellFormattingEventHandler(
                DbItems_CellFormatting);
        }
        public void DbItems_CellFormatting(object sender,
DataGridViewCellFormattingEventArgs e)
        {
            if (e != null)
            {
                if (this.GridView.Columns[e.ColumnIndex].Name == "Release Date")
                {
                    if (e.Value != null)
                    {
                        try
                        {
                            e.Value = DateTime.Parse(e.Value.ToString())
                                .ToLongDateString();
                            e.FormattingApplied = true;
                        }
                        catch (FormatException)
                        {
                            System.Console.WriteLine("{0} is not a valid date.", e.Value.ToString());
                        }
                    }
                }
            }
        }
        #endregion
        #region Grid Row and Cell Db Sync
        #region Row Management
        public List<string> FieldUsedList;
        public string FieldUsedListText;
        public List<string> OutputActionList;
        public string OutputActionListText;
        // Row fields
        protected int mRowNumberCurrent; // ToDo
        public int RowNumberCount
        {
            get { return mRowNumberCurrent; }
            set
            {
                //if (mCrListIdCurrent > mRowNumberCurrent) { mRowNumberCurrent = mCrListIdCurrent; }
                //else if (mRowNumberCurrent > mCrListIdCurrent) { mCrListIdCurrent = mRowNumberCurrent; }
            }
        }
        public int RowNumberSelected { get; set; } = -1;
        public int CellNumberSelected { get; set; } = -1;
        public int RowNumberSelectedLast { get; set; } = -1;
        public int RowNumberWorking { get; set; } = -1;
        public int RowSequenceNumberSelectedLast { get; set; } = -1;
        #endregion
        #region DbList Row Functions
        public bool RowAdd(ref DbDataDef PassedDbData, ref DataGridView ListPassed)
        {
            string[] rowString = PassedDbData.SetRowData();
            DbFile.DataInsertSkipRowEnter = true;
            ListPassed.Rows.Add(rowString);
            RowSelect(ref ListPassed, ListPassed.RowCount);
            return true;

        }
        public bool RowInsert(ref DbDataDef PassedDbData, ref DataGridView ListPassed)
        {
            string[] rowString = PassedDbData.SetRowData();
            GridView.Rows.Insert(RowNumberSelected, rowString);
            RowSelect(ref ListPassed, RowNumberSelected - 1);
            return true;

        }
        public bool RowDelete(ref DbDataDef PassedDbData, ref DataGridView ListPassed)
        {
            RowDelete(ref PassedDbData, ref ListPassed, RowNumberSelected);
            return true;
        }
        public bool RowDelete(ref DbDataDef PassedDbData, ref DataGridView ListPassed, int RowNumberPassed)
        {
            bool Ok = true;
            if (ListPassed.Rows[RowNumberPassed].Cells[0].Value != null && ListPassed.Rows[RowNumberPassed].Cells[0].Value.ToString() != "")
            {
                try
                {
                    if (!ListPassed.Rows[RowNumberPassed].IsNewRow)
                    {
                        // ListPassed.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        ListPassed.Rows.RemoveAt(RowNumberPassed);
                    }
                    else
                    {
                        ListPassed.NotifyCurrentCellDirty(true);
                        ListPassed.EndEdit();
                        ListPassed.NotifyCurrentCellDirty(false);
                        ListPassed.Rows.RemoveAt(RowNumberPassed);
                    }
                }
                catch (Exception e)
                {
                    Ok = false;
                    // Output a user message here!
                    // not a throw;
                }
                if (!Ok)
                {
                    // Output a user message here!
                }
            }
            RowSelect(ref ListPassed, RowNumberSelected - 1);
            return true;
        }
        #endregion
        #region DataGrivView Row selection
        public int RowSelectGet(DataGridView grid)
        {
            if (grid == null) { return 0; }
            if (grid.Rows.Count > 0)
            {
                try
                {
                    if (grid.Rows[grid.Rows.Count - 1].Selected)
                    {
                        if (RowNumberSelected >= 0)
                        {
                            if (RowSequenceNumberSelectedLast >= 0)
                            {
                                grid.Rows[RowSequenceNumberSelectedLast].Selected = false;
                            }
                            grid.Rows[RowNumberSelected].Selected = true;
                            RowSequenceNumberSelectedLast = RowNumberSelected;
                            //grid.CurrentCell = grid.Rows[SequenceNumberSelected].Cells[1];
                            return RowNumberSelected;
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                { }
                catch (ArgumentOutOfRangeException)
                { }
            }

            return 0;
        }
        public bool RowSelect(ref DataGridView ListPassed, int RowNumberPassed)
        {
            if (RowNumberPassed < 0) { RowNumberPassed = 0; }
            if (RowNumberPassed > ListPassed.RowCount - 1) { RowNumberPassed = ListPassed.RowCount - 1; }
            ListPassed.FirstDisplayedScrollingRowIndex = RowNumberPassed;
            ListPassed.CurrentCell = ListPassed.Rows[RowNumberPassed].Cells[1];
            ListPassed.Refresh();
            // ListPassed.Rows[RowNumberPassed].Selected = true;
            // ListPassed.CurrentCell = ListPassed[1, RowNumberPassed];
            return true;
        }
        public int RowSelectLast(DataGridView grid)
        {
            if (grid == null) { return 0; }
            if (grid.Rows.Count > 0)
            {
                try
                {
                    grid.Rows[grid.Rows.Count - 1].Selected = true;
                    grid.CurrentCell = grid.Rows[grid.Rows.Count - 1].Cells[1];
                }
                catch (IndexOutOfRangeException)
                { }
                catch (ArgumentOutOfRangeException)
                { }
            }
            return grid.Rows.Count;
        }
        #endregion
        #region Row Cell Management
        public void CellSelectionChanged(object sender, EventArgs e)
        {
            RowNumberSelected = ((DataGridView)sender).CurrentRow.Index;
            CellNumberSelected = ((DataGridView)sender).CurrentCell.ColumnIndex;
            return;
        }

        // CrListCellValueRowLeave
        public void CellSortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (GridView.IsCurrentRowDirty) { GridView.EndEdit(); }
            //Suppose your interested column has index 1
            if (e.Column.Index == 0)
            {
                e.SortResult = int.Parse(e.CellValue1.ToString()).CompareTo(int.Parse(e.CellValue2.ToString()));
                e.Handled = true;//pass by the default sorting
            }
        }
        public void CellRowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (DbData.DataIsDirty)
            {
                DbFile.DataWriteThread(DbData, DbData.Id);
            }
        }
        // CrListCellValueRowEnter
        public void CellRowRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                int cellId = (int)Convert.ToInt32(GridView[0, e.RowIndex].Value);
                DbFile.DataReadThread(DbData, (int)cellId, false, true, false, false);
            }
            catch (Exception) { }
        }
        public void CellRowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null)
            {
                // Get Row
                DataGridViewCell cell = GridView[e.ColumnIndex, e.RowIndex];
                if (GridView[0, e.RowIndex] == null)
                {
                    // enters once while not shown and grid null basically it
                    // seems to be the blank row when the form is shown.
                    DbFile.DataInsertSkipRowEnter = true;
                    DbFile.DataInsertSkipRowEnterArgs = e;
                    return;
                }
                if (DbFile == null || st.StateIsExistenceSuccessful(CalculationTaskUtils.WaitCheckBusy(st, -1)))
                {
                    // row enter fires on each line during populate (busy)
                    // also fired with ScriptFile null during startup.
                    DbFile.DataInsertSkipRowEnter = true;
                    DbFile.DataInsertSkipRowEnterArgs = e;
                    return;
                }

                // Get Row
                RowNumberSelected = e.RowIndex;

                // ToDo Read SQL and load current?
                if (RowNumberSelected != RowNumberSelectedLast || DbFile.DataInsertSkipRowEnter)
                {
                    if (GridView.RowCount - 1 >= e.RowIndex && GridView[0, e.RowIndex].Value != null)
                    {
                        int cellId = (int)Convert.ToInt32(GridView[0, e.RowIndex].Value);
                        DbFile.DataReadThread(DbData, (int)cellId, false, true, false, false);
                    }
                    else
                    {
                        if (DbFile.DataInsertSkipRowEnter)
                        {
                            // Kind of a weird First after populate?
                            DbFile.DataInsertSkipRowEnter = false;
                            DbFile.DataInsertSkipRowEnterArgs = e;
                            return;
                        }
                        DbData = new DbDataDef();
                        DbFile.DataInsertThread(DbData, DbData.Id);
                        GridView[0, e.RowIndex].Value = DbData.Id;
                    }
                    DbFile.DataInsertSkipRowEnter = false;
                    DbFile.DataInsertSkipRowEnterArgs = null;
                    RowNumberSelectedLast = RowNumberSelected;
                }
            }
        }
        DataGridViewCell cellSave;
        public void CellChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null)
            {
                if (DbFile == null || st.StateIsExistenceSuccessful(CalculationTaskUtils.WaitCheckBusy(st, -1)))
                {
                    DbFile.DataInsertSkipRowEnter = true;
                    DbFile.DataInsertSkipRowEnterArgs = e;
                    return;
                }
                // Get Row
                DataGridViewCell cell;
                cell = GridView[e.ColumnIndex, e.RowIndex];
                if (GridView[0, e.RowIndex].Value == null)
                {
                    DbFile.DataInsertThread(DbData, DbData.Id);
                    GridView[0, e.RowIndex].Value = DbData.Id;
                }
                DbData.DateUpdated = DateTime.Now.ToString("O");
                DbData.DataIsDirty = true;

                string ColumnName = GridView.Columns[e.ColumnIndex].Name;
                switch (ColumnName)
                {
                    case "DataType":
                        DbData.DataType = (string)cell.Value;
                        break;
                    case "DataTypeMinor":
                        DbData.DataTypeMinor = (string)cell.Value;
                        break;
                    case "Id":
                        // Id, read only, exception? // 202101 Dgh ToDo
                        // Do Nothing
                        break;
                    //case "Value":
                    //    DbData.Value = (int)cell.Value;
                    //    break;
                    case "DateUpdated":
                        DbData.DateUpdated = (string)cell.Value;
                        break;

                    default:
                        // throw new NotImplementedException();
                        string ErrorMsg = "Unexpected Exception. Column name not found: ("
                            + ColumnName + ")!";
                        ErrorMsg += "\n" + "Item: " + DbData.Id.ToString() + ".";
                        ErrorMsg += " From: " + sender.ToString();
                        ErrorMsg += " Arguments: " + e.ToString();
                        st.ExceptNotSupportedImpl(sender, null, ErrorMsg, 0);
                        break;
                }
            }
        }
        public void CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e != null)
            {
                if (this.GridView.Columns[e.ColumnIndex].Name == "Release Date"
                    // || this.ScList.Columns[e.ColumnIndex].Name == "DateUpdated"
                    )
                {
                    if (e.Value != null)
                    {
                        try
                        {
                            e.Value = DateTime.Parse(e.Value.ToString())
                                .ToLongDateString();
                            e.FormattingApplied = true;
                        }
                        catch (FormatException)
                        {
                            // throw new NotImplementedException();
                            //string ErrorMsg = "Unexpected Exception. Cell formatting error!"
                            //ErrorMsg += "\n" + "Item: " + DbData.Id.ToString() + ".";
                            //ErrorMsg += " From: " + sender.ToString();
                            //ErrorMsg += " Arguments: " + e.Value.ToString() + " is not a valid date.";
                            //st.ExceptNotSupportedImpl(sender, null, ErrorMsg, 0);
                            System.Console.WriteLine("{0} is not a valid date.", e.Value.ToString());
                            // throw message instead.
                            string LocalMessage = e.Value.ToString() + " is not a valid date.";
                            st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);

                        }
                    }
                }
            }
        }
        #endregion
        #endregion
        #endregion
    }
}
