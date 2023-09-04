using Shell32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
//using System.Windows;
using System.Windows.Forms;
//using System.Windows.Controls; // Page

using Mdm;
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Components;
// using Mdm.Oss.Mobj;
using Mdm.Oss.Thread;
using Mdm.Oss.Std;

using Mdm.Oss.WinUtil;

using HWND = System.IntPtr;
using System.Windows;
using System.Windows.Interop;
using Mdm.Oss.Run.Control;

namespace Mdm.Oss.Console
{
    public partial class ConsoleFormDef : StdBaseFormDef, IDisposable
    {
        #region Console
        public string IconLevel;
        public string IconOrder; // ConsoleTypeDef
        public string IconName;

        public int ConsoleType; // ConsoleTypeDef
        public int ConsoleId;
        public StdConsolesDef ConsolesLocal;
        public new StdConsoleManagerDef st;
        #endregion
        #region UI objects, Status Line, Page, Progress Bar
        // StdConsoleManagerDef Status Lines, currently text boxes
        // public StatusUiDef StatusUi;
        // PageMain
        //public bool PageMainInvalidateVisual;
        //public bool PageMainBringIntoView;
        // ProgressBar 
        public ProgressBar ProgressBarMdm;
        //  Delegates
        #endregion
        #region Form and Controls
        private FolderBrowserDialog DirectoryBrowserDialog;
        private OpenFileDialog FileDialog;
        private RichTextBox LogOutRichTextBox1;
        public DataGridView GridView = new DataGridView(); // Cf = ConsoleForm
        //
        private ToolBar MenuButtonBar;
        private MenuStrip MenuButtonStrip;
        
        private ToolStripButton ButtonUp;
        private ToolStripButton ButtonDn;
        private ToolStripButton ButtonAdd;
        private ToolStripButton ButtonDel;
        private ToolStripButton ButtonFilter;

        private ToolStripLabel LabelScriptListDirectoryCount;
        private ToolStripLabel LabelScriptListFileCount;
        //
        //ButtonActionIs RunControlUi.ButtonAction;
        //private ToolStripButton RunControlUi.ButtonPause;
        //private ToolStripButton RunControlUi.ButtonCancel;
        //private ToolStripButton RunControlUi.ButtonStart;
        //private ToolStripButton RunControlUi.ButtonFile;

        private MenuStrip StdNotifyMenuStrip;
        private ContextMenu StdNotifyCntxMenu;

        private ToolStripButton ButtonConsoleAll;
        private ToolStripButton ButtonConsoleSystem;
        private ToolStripButton ButtonConsoleUser;
        private ToolStripButton ButtonConsoleDatabase;
        private ToolStripButton ButtonConsoleError;

        private ToolStripLabel LabelScriptListBusyMessage;

        private MainMenu MenuMain;
        private MenuItem MenuFileItem, MenuOpenItem;
        private MenuItem MenuFolderItem, MenuCloseItem;
        private TableLayoutPanel MainTableLayoutPanel;
        #endregion
        #region Control Format
        float ControlScaleWidth;
        float ControlScaleHeight;
        float ControlOrigWidth;
        float ControlOrigHeight;
        float ControlScale;
        int ControlMargin = 4;
        int ControlHeight = 200;
        #endregion
        #region Control Replace List = Cf
        public class CfDataDef
        {
            public int Id;
            public string Sequence;
            public string InputString;
            public string OutputString;
        }
        public CfDataDef CfData;
        public CfDataDef CfDataNext;
        public CfDataDef CfDataPrev;
        protected int mRowNumberCurrent;
        public int RowNumberCount
        {
            get { return mRowNumberCurrent; }
            set { }
        }
        public int RowNumberSelected { get; set; } = -1;
        public int RowNumberSelectedLast { get; set; } = -1;
        public int RowNumberWorking { get; set; } = -1;
        public int RowNumberNext { get; set; } = -1;
        public int RowNumberPrev { get; set; } = -1;
        public int SequenceNumberSelectedLast { get; set; } = -1;

        public int mCfIdCurrent;
        public int CfIdCurrent
        {
            get { return mCfIdCurrent; }
            set
            {
                mCfIdCurrent = value;
                //if (mCfIdCurrent > mRowNumberCurrent) { mRowNumberCurrent = mCfIdCurrent; }
                //else if (mRowNumberCurrent > mCfIdCurrent) { mCfIdCurrent = mRowNumberCurrent; }
            }
        }
        #endregion
        #region Constructors, Initalize Console, Component, Component Form, Console Form
        public ConsoleFormDef(ref object SenderPassed, ref object stPassed,
            string IconLevelPassed, string IconOrderPassed, string IconNamePassed,
            string TitlePassed)
            : base(ref SenderPassed, ref stPassed)
        {
            st = base.st as StdConsoleManagerDef;
            IconLevel = IconLevelPassed;
            IconOrder = IconOrderPassed;
            IconName = IconNamePassed;
            Title = TitlePassed;
            StdKey = new StdKeyDef(IconLevel, IconOrder, IconName);
            //
            ConsolesLocal = SenderPassed as StdConsolesDef;
            StdNotify = ConsolesLocal.StdNotify;
            StdNotifyRoot = ConsolesLocal.StdNotifyRoot;
            StdRunControlUi = ConsolesLocal.StdRunControlUi;
            Sender = SenderPassed;

            Sender = this; SenderIsThis = this;
            StdProcess = new StdProcessDef(IconLevel, IconOrder, IconName)
            {
                // 1 of 2 Processes
                Title = this.Title,
                Verbosity = ConsolesLocal.st.ConsoleVerbosity,
            };
            StdProcess.StdScreen.DeviceForm = this;
            StdProcess.StdScreen.DeviceName = Name;
            StdProcess.StdScreen.ScreenObject = Screen.FromControl(this);
            //
            InitializeMdmConsoleForm();
        }
        public ConsoleFormDef()
        : base()
        {
            ConsolesLocal = null;
            ConsoleId = 0;
            InitializeMdmConsoleForm();
        }
        // ToDo Clean up names.
        private void InitializeMdmConsoleForm()
        {
            //st = base.st as StdConsoleManagerDef;
            //base.InitializeStdBaseForm();
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            // ToDo This is not correct:
            ClassRole = ClassRoleIs.RoleAsUtility;
            ClassFeatures =
                (ClassFeatureIs)(
                ClassFeatureIs.MaskUi
                | ClassFeatureIs.MaskButton
                | ClassFeatureIs.MaskStautsUiAsBox
                );
            Verbosity = st.ConsoleVerbosity;
            // Verbosity = ParentObject.st.ConsoleVerbosity;
            //TopMostInitialize(ref ScreenObject);
            base.InitializeStdBaseForm();
            InitializeComponentForm();
        }
        private void InitializeComponentForm()
        {
            #region Menu and Main Form
            MenuMain = new System.Windows.Forms.MainMenu();
            MenuFileItem = new System.Windows.Forms.MenuItem();
            MenuOpenItem = new System.Windows.Forms.MenuItem();
            MenuFolderItem = new System.Windows.Forms.MenuItem();
            MenuCloseItem = new System.Windows.Forms.MenuItem();
            MainTableLayoutPanel = new TableLayoutPanel();

            FileDialog = new System.Windows.Forms.OpenFileDialog();
            DirectoryBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            LogOutRichTextBox1 = new System.Windows.Forms.RichTextBox();

            //base form events;
            // you coud select them. not used.
            bool UseActivated = true; //  new EventHandler(Form_Activated);
            bool UseDeactivate = true; //  new EventHandler(Form_Deactivated);
            bool UseGotFocus = true; //  new EventHandler(Form_GotFocus);
            bool UseLostFocus = true; //  new EventHandler(Form_LostFocus);
            bool UseFormClosing = true; //  new System.Windows.Forms.FormClosingEventHandler(Form_Close);
            bool UseResize = true; //  new EventHandler(Form_Resize);
            bool UseResizeEnd = true; //  new System.EventHandler(Form_ResizeEnd);

            MenuMain.MenuItems.Add(MenuFileItem);
            MenuFileItem.MenuItems.AddRange(
                                new System.Windows.Forms.MenuItem[] {MenuOpenItem,
                                                                 MenuCloseItem,
                                                                 MenuFolderItem});
            MenuFileItem.Text = "File";

            MenuOpenItem.Text = "Open...";
            MenuOpenItem.Click += new System.EventHandler(MenuOpenItem_Click);

            MenuFolderItem.Text = "Select Directory...";
            MenuFolderItem.Click += new System.EventHandler(DirectoryMenuItem_Click);

            MenuCloseItem.Text = "Close";
            MenuCloseItem.Click += new System.EventHandler(MenuCloseItem_Click);

            MenuCloseItem.Enabled = false;

            FileDialog.DefaultExt = "lnk";
            FileDialog.Filter = "lnk files (*.lnk)|*.lnk";

            // Set the help text Description for the FolderBrowserDialog.
            DirectoryBrowserDialog.Description =
                "Select the directory that you want to use as the default.";

            // Do not allow the user to create new files via the FolderBrowserDialog.
            DirectoryBrowserDialog.ShowNewFolderButton = true;
            
            // Default to the My Documents folder.
            DirectoryBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
            #endregion
            StdNotifyIcon = new StdNotifyIconDef(ref SenderIsThis, ref ConsoleSender, 
                IconLevel, IconOrder, IconName,
                Title, false);
            StdNotifyIcon.StdNotifyAdd();
            //StdNotify.StdNotifyIcon.ItemNotifyIcon = new NotifyIcon();
            // NotifyIcon.Icon = new Icon("MdmControlLeft.ico");
            // NotifyIcon.Icon = Icon;
            //NotifyIcon.Icon = new Icon(Text + ".ico");
            //StdNotify.StdNotifyIcon.Visible = true;
            //StdNotify.StdNotifyIcon.ItemNotifyIcon.MouseClick += new MouseEventHandler(NotifyIcon_Click);
            //StdNotify.StdNotifyIcon.ItemNotifyIcon.BalloonTipClosed += new EventHandler(NotifyIcon_BalloonTipClosed);

            // new EventHandler(NotifyIcon_BalloonTipClosed);
            // (sender1, e) => { var thisIcon = (NotifyIcon)sender1; thisIcon.Visible = false; thisIcon.Dispose(); };
            Visible = false;
            #region Main Table
            MenuButtonBar = new ToolBar();
            MenuButtonStrip = new MenuStrip();
            ButtonUp = new System.Windows.Forms.ToolStripButton();
            ButtonDn = new System.Windows.Forms.ToolStripButton();
            ButtonAdd = new System.Windows.Forms.ToolStripButton();
            ButtonFilter = new System.Windows.Forms.ToolStripButton();
            ButtonDel = new System.Windows.Forms.ToolStripButton();
            StdRunControlUi.ButtonStart = new System.Windows.Forms.ToolStripButton();
            StdRunControlUi.ButtonFile = new System.Windows.Forms.ToolStripButton();

            LabelScriptListDirectoryCount = new ToolStripLabel();
            LabelScriptListFileCount = new ToolStripLabel();
            StdRunControlUi.ButtonPause = new ToolStripButton();
            StdRunControlUi.ButtonCancel = new System.Windows.Forms.ToolStripButton();

            ButtonConsoleAll = new ToolStripButton();
            ButtonConsoleSystem = new ToolStripButton();
            ButtonConsoleUser = new ToolStripButton();
            ButtonConsoleDatabase = new ToolStripButton();
            ButtonConsoleError = new ToolStripButton();

            LabelScriptListBusyMessage = new ToolStripLabel();

            GridView = new DataGridView();
            InitializeConsoleForm();

            MainTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                | System.Windows.Forms.AnchorStyles.Left)
                                | System.Windows.Forms.AnchorStyles.Right)));
            MainTableLayoutPanel.Dock = DockStyle.Fill;
            MainTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;

            MainTableLayoutPanel.ColumnCount = 1;
            MainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));


            MainTableLayoutPanel.RowCount = 2;

            //mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));  Yes... I tried them all...
            //mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            //mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            //mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            //mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            //mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));

            //MenuMain.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            MenuButtonStrip.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
                ButtonUp,
                ButtonDn,
                ButtonFilter
           });
            MenuButtonStrip.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
                LabelScriptListDirectoryCount,
                LabelScriptListFileCount
           });
            MenuButtonStrip.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
                StdRunControlUi.ButtonPause,
                StdRunControlUi.ButtonCancel
           });
            MenuButtonStrip.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
                ButtonConsoleAll,
                ButtonConsoleSystem,
                ButtonConsoleUser,
                ButtonConsoleDatabase,
                ButtonConsoleError
           });
            MenuButtonStrip.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
                LabelScriptListBusyMessage
           });
            #endregion
            MainTableLayoutPanel.Controls.Add(MenuButtonStrip, 0, 0);
            //MainTableLayoutPanel.Controls.Add(st.StdRunControlUi.StdNotify.StdNotifyMenuStrip, 0, 1);
            MainTableLayoutPanel.Controls.Add(StdNotify.Root.NotifyMenuStrip, 1, 0);
            ContextMenu = StdNotify.Root.NotifyCntxMenu;
            MainTableLayoutPanel.Controls.Add(GridView, 0, 1);
            //mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0); // (ControlMargin, ControlMargin);
            MainTableLayoutPanel.Name = "mainTableLayoutPanel";

            //mainTableLayoutPanel.Size = new System.Drawing.Size(660, 400);
            MainTableLayoutPanel.AutoSize = true;

            MainTableLayoutPanel.TabIndex = 1;

            Controls.Add(MainTableLayoutPanel);

            ClientSize = new System.Drawing.Size(680, 500);

            Menu = MenuMain;
            Text = "Console Logger";
        }
        private void InitializeConsoleForm()
        {
            //Controls.Add(songsDataGridView);

            GridView.ColumnCount = 7;

            GridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            GridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            GridView.ColumnHeadersDefaultCellStyle.Font =
                new Font(GridView.Font, System.Drawing.FontStyle.Bold);
            int FontHeight = GridView.ColumnHeadersDefaultCellStyle.Font.Height;
            float FontHeight2 = GridView.ColumnHeadersDefaultCellStyle.Font.GetHeight();
            
            GridView.Name = "ConsoleOutput";
            // GridView.Location = new System.Drawing.Point(ControlMargin, ControlMargin); // 221

            // GridView.MinimumSize = new System.Drawing.Size(200, 100);
            // GridView.MaximumSize = new System.Drawing.Size(2000, 200);
            // GridView.Size = new System.Drawing.Size(850, 160);

            GridView.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            GridView.MultiSelect = false;
            GridView.AllowUserToAddRows = false;

            // Layout
            GridView.Dock = DockStyle.Fill;
            GridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            GridView.Anchor =
            AnchorStyles.Bottom |
            AnchorStyles.Right |
            AnchorStyles.Top |
            AnchorStyles.Left;
            // Formatting
            GridView.RowHeadersVisible = false;
            GridView.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            GridView.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            GridView.GridColor = Color.Black;
            GridView.Margin = new Padding(ControlMargin);
            DataGridViewContentAlignment TopLeft = DataGridViewContentAlignment.TopLeft;
            DataGridViewContentAlignment TopCenter = DataGridViewContentAlignment.TopCenter;

            GridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            GridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;

            // Autosize Columns and Rows
            //GridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            //GridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            //GridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            //GridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //GridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // Events
            //GridView.CellEndEdit += new DataGridViewCellEventHandler(Cf_CellValueChanged);
            //GridView.RowEnter += new DataGridViewCellEventHandler(Cf_CellValueRowEnter);
            GridView.RowLeave += new DataGridViewCellEventHandler(Cf_CellValueRowLeave);
            GridView.RowsAdded += new DataGridViewRowsAddedEventHandler(Cf_RowAdded);
            //GridView.SelectionChanged += new EventHandler(Cf_SelectionChanged);
            GridView.SortCompare += new DataGridViewSortCompareEventHandler(Cf_SortCompare);
            GridView.Resize += new EventHandler(Cf_GridViewResize);
            ////
            GridView.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            //GridView.CellFormatting += new
            //    DataGridViewCellFormattingEventHandler(
            //    Cf_CellFormatting);

            // Define Columns
            GridView.Columns[0].Name = "Id";
            GridView.Columns[1].Name = "Sequence";
            GridView.Columns[2].Name = "Message";
            GridView.Columns[3].Name = "Console";
            GridView.Columns[4].Name = "Verbosity";
            GridView.Columns[5].Name = "IsError";
            GridView.Columns[6].Name = "Result";

            // Set Column Attributes
            GridView.Columns["Id"].ReadOnly = true;
            GridView.Columns["Id"].Visible = false;
            GridView.Columns["Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            GridView.Columns["Sequence"].MinimumWidth = 25;
            GridView.Columns["Sequence"].SortMode = DataGridViewColumnSortMode.Automatic;
            GridView.Columns["Sequence"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;

            GridView.Columns["Message"].MinimumWidth = 150;
            GridView.Columns["Message"].SortMode = DataGridViewColumnSortMode.Automatic;
            GridView.Columns["Message"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            GridView.Columns["Message"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            // (DataGridViewTriState)TextWrapping.WrapWithOverflow
            //GridView.Columns["Message"].

            GridView.Columns["Console"].MinimumWidth = 90;
            GridView.Columns["Console"].SortMode = DataGridViewColumnSortMode.Automatic;
            GridView.Columns["Console"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            GridView.Columns["Verbosity"].MinimumWidth = 25;
            GridView.Columns["Verbosity"].SortMode = DataGridViewColumnSortMode.Automatic;
            GridView.Columns["Verbosity"].DefaultCellStyle.Font =
                new Font(GridView.DefaultCellStyle.Font, System.Drawing.FontStyle.Italic);
            GridView.Columns["Verbosity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;

            GridView.Columns["IsError"].MinimumWidth = 30;
            GridView.Columns["IsError"].SortMode = DataGridViewColumnSortMode.Automatic;
            GridView.Columns["IsError"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;

            GridView.Columns["Result"].MinimumWidth = 30;
            GridView.Columns["Result"].SortMode = DataGridViewColumnSortMode.Automatic;
            GridView.Columns["Result"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;

            // set autosize mode
            GridView.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns["Sequence"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            GridView.Columns["Message"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //

            GridView.Columns["Verbosity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns["IsError"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns["Result"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //// datagrid has calculated it's widths so we can store them
            //foreach (DataGridViewColumn column in Cf.Columns)
            //{
            //    // store autosized widths
            //    int colw = column.Width;
            //    // remove autosizing
            //    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //    // set width to calculated by autosize
            //    column.Width = colw;

            //}
            //For i = 0 To Cf.Columns.Count - 1
            //Next

            GridView.Columns[0].DisplayIndex = 4;
            GridView.Columns[1].DisplayIndex = 0;
            GridView.Columns[2].DisplayIndex = 1;
            GridView.Columns[3].DisplayIndex = 2;
            GridView.Columns[4].DisplayIndex = 3;

            // Set the properties of the DataGridView columns.

            //Cf.Columns["Balance"].HeaderText = "Balance";
            //Cf.Columns["Balance"].ReadOnly = true;
            //Cf.Columns["Description"].SortMode =
            //    DataGridViewColumnSortMode.NotSortable;
            //Cf.Columns["Withdrawals"].SortMode =
            //    DataGridViewColumnSortMode.NotSortable;
            //Cf.Columns["Deposits"].SortMode =
            //    DataGridViewColumnSortMode.NotSortable;
            //Cf.Columns["Balance"].SortMode =
            //    DataGridViewColumnSortMode.NotSortable;

            //
            //MenuMain.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            // MenuButtonStrip.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            //     MenuButtonPrev,
            //     MenuButtonNext,
            //     MenuButtonSet,
            //     MenuButtonStart
            //});
            // MenuButtonPrev
            ButtonUp.Text = "Up";
            ButtonUp.Click += new EventHandler(ButtonUp_Click);
            //MenuButtonPrev.DisplayStyle = ToolBarButtonStyle.PushButton;

            // MenuButtonNext
            ButtonDn.Text = "Dn";
            ButtonDn.Click += new EventHandler(ButtonDn_Click);

            // MenuButtonSet
            ButtonFilter.Text = "Filter";
            ButtonFilter.Click += new EventHandler(ButtonFilter_Click);

            // Directory Count
            LabelScriptListDirectoryCount.Text = "0";

            // Shortcut file count
            LabelScriptListFileCount.Text = "0";

            // MenuButtonPause
            StdRunControlUi.ButtonPause.Text = "Pause";
            StdRunControlUi.ButtonPause.Click += new EventHandler(ButtonPause_Click);
            StdRunControlUi.ButtonPause.Enabled = false;

            // MenuButtonCancel
            StdRunControlUi.ButtonCancel.Text = "Cancel";
            StdRunControlUi.ButtonCancel.Click += new EventHandler(ButtonCancel_Click);
            StdRunControlUi.ButtonCancel.Enabled = false;
            StdRunControlUi.ButtonCancel.BackColor = Color.LightGreen;

            // MenuButtonAdd
            ButtonAdd.Text = "Add";
            ButtonAdd.Click += new EventHandler(ButtonAdd_Click);
            // 
            // MenuButtonStart
            ButtonDel.Text = "Del";
            ButtonDel.Click += new EventHandler(ButtonDel_Click);

            // MenuButtonStart
            StdRunControlUi.ButtonStart.Text = "Directory";
            StdRunControlUi.ButtonStart.Click += new EventHandler(ButtonStart_Click);

            // MenuButtonFile
            StdRunControlUi.ButtonFile.Text = "File";
            StdRunControlUi.ButtonFile.Click += new EventHandler(ButtonFile_Click);

            StdNotifyMenuStrip = StdNotify.Root.NotifyMenuStrip;
            StdNotifyCntxMenu = StdNotify.Root.NotifyCntxMenu;
            //// string temp = StdBaseDef.DriveOs + @"\Srt Project1\Mdm\Mdm3\Mdm1Oss1\Mdm1Oss1Definitions1\Mdm1Oss1DefBase1\";
            //string temp = @"F:\Dev\Mdm3ShortcutUtilsPhase2\Mdm1Oss1\Mdm1Oss1Definitions1\Mdm1Oss1DefBase1\Resource\";
            //ButtonConsoleAll.Image = Image.FromFile(temp + @"Letter-A.ico");
            //ButtonConsoleAll.Click += new EventHandler(ButtonConsoleAll_Click);

            //ButtonConsoleDatabase.Image = Image.FromFile(temp + "Letter-D.ico");
            //ButtonConsoleDatabase.Click += new EventHandler(ButtonConsoleDatabase_Click);

            //ButtonConsoleUser.Image = Image.FromFile(temp + "Letter-U.ico");
            //ButtonConsoleUser.Click += new EventHandler(ButtonConsoleUser_Click);

            //ButtonConsoleSystem.Image = Image.FromFile(temp + "Letter-S.ico");
            //ButtonConsoleSystem.Click += new EventHandler(ButtonConsoleSystem_Click);

            //ButtonConsoleError.Image = Image.FromFile(temp + "Letter-E.ico");
            //ButtonConsoleError.Click += new EventHandler(ButtonConsoleError_Click);
        }
        #endregion
        public new void Dispose()
        {
            Dispose(Status);
            // this is the base.Dispose();
            Status = StateIs.DoesNotExist;
        }
        public new void Dispose(StateIs StatusPassed)
        {
            if (st.StdRunControlUi.StdNotify.ContainsKey(StdKey.Key))
            {
                st.StdRunControlUi.StdNotify.Remove(StdKey.Key);
            }
            base.Dispose();
            Status = StateIs.DoesNotExist;
        }
        #region Menu Events
        private void MenuOpenItem_Click(object sender, System.EventArgs e)
        {
            // If a file is not opened, then set the initial directory to the
            // FolderBrowserDialog.SelectedPath value.
            FileSelect();
        }
        public bool DirectorySelect()
        {
            if (!DirectoryOpened)
            {
            }
            DirectoryNameOpen = sEmpty;
            // Display the openDirectory dialog.
            DialogResult result = DirectoryBrowserDialog.ShowDialog();

            // OK button was pressed.
            if (result == DialogResult.OK)
            {
                DirectoryNameOpen = DirectoryBrowserDialog.SelectedPath;
                return true;
            }

            // Cancel button was pressed.
            else if (result == DialogResult.Cancel)
            {
                return false;
            }
            return false;
        }
        public bool FileSelect()
        {
            if (!FileOpened)
            {
                FileDialog.InitialDirectory = DirectoryBrowserDialog.SelectedPath;
                FileDialog.FileName = null;
            }
            FileNameOpen = sEmpty;
            // Display the openFile dialog.
            DialogResult result = FileDialog.ShowDialog();

            // OK button was pressed.
            if (result == DialogResult.OK)
            {
                FileNameOpen = FileDialog.FileName;
                try
                {
                    // Output the requested file in richTextBox1.
                    Stream s = FileDialog.OpenFile();
                    LogOutRichTextBox1.LoadFile(s, RichTextBoxStreamType.RichText);
                    s.Close();

                    FileOpened = true;

                }
                catch (Exception exp)
                {
                    System.Windows.MessageBox.Show("An error occurred while attempting to load the file. The error is:"
                                    + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
                    FileOpened = false;
                    return false;
                }
                Refresh();

                MenuCloseItem.Enabled = FileOpened;
                return true;
            }

            // Cancel button was pressed.
            else if (result == DialogResult.Cancel)
            {
                return false;
            }
            return false;
        }
        // Close the current file.
        private void MenuCloseItem_Click(object sender, System.EventArgs e)
        {
            LogOutRichTextBox1.Text = sEmpty;
            FileOpened = false;

            MenuCloseItem.Enabled = false;
        }
        // Bring up a dialog to chose a folder path in which to open or save a file.
        private void DirectoryMenuItem_Click(object sender, System.EventArgs e)
        {
            // Show the FolderBrowserDialog.
            DialogResult result = DirectoryBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                DirectoryNameOpen = DirectoryBrowserDialog.SelectedPath;
                if (!FileOpened)
                {
                    // No file is opened, bring up openFileDialog in selected path.
                    FileDialog.InitialDirectory = DirectoryNameOpen;
                    FileDialog.FileName = null;
                    MenuOpenItem.PerformClick();
                }
            }
        }
        #endregion
        #region Cf Selection
        private void Cf_SelectionProcess(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            // throw message instead.
            string LocalMessage = "Not Implemented Exception.";
            LocalMessage += sender.ToString();
            st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
        }
        private void Cf_SelectionChanged(object sender, EventArgs e)
        {
            //if (e != null)
            //// e.RowIndex
            //// e.ColumnIndex
            //{
            //    // Get Row
            //    SequenceNumberSelected = e.RowIndex;
            RowNumberSelected = GridView.CurrentRow.Index;

            //}
        }
        #endregion
        static IEnumerable<string> FilesGet(string path)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                try
                {
                    foreach (string subDir in Directory.GetDirectories(path))
                    {
                        queue.Enqueue(subDir);
                    }
                }
                catch (Exception ex)
                {
                    System.Console.Error.WriteLine(ex);
                }
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(path);
                }
                catch (Exception ex)
                {
                    System.Console.Error.WriteLine(ex);
                }
                if (files != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        yield return files[i];
                    }
                }
            }
        }
        #region Actions
        private void ButtonFile_Click(object sender, EventArgs e)
        {
            // throw message instead.
            string LocalMessage = "Not Implemented Exception.";
            LocalMessage += sender.ToString();
            st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);

            //Mdm.Srt.Core.Run CfExec = new Run(this, Cf, "FixShortcut", Context, 2, 3);
            //// public Run(Object SenderPassed, DataGridView SourceObjectPassed, string ScriptNamePassed, List<string> ContextPassed, int LocateColumnIndex, int ReplaceColumnIndex)

            ////string temp = StdBaseDef.DriveOs + @"\Srt Project1\Links to Folders";
            //for (int i = 0; FileSelect(); FileCount++)
            //{
            //    FileName = openFileName;
            //    ShortcutClass Sc = new ShortcutClass();

            //    // process selected file.
            //    string file = FileName;
            //    System.Console.WriteLine(file);
            //    if (file.FieldLast(".") == "lnk")
            //    {
            //        // read shortcut
            //        Sc.ShortcutRead(file);
            //        // check if target exists else:
            //        if (!Directory.Exists(Sc.shortcutTargetPath))
            //        {
            //            // load item from shortcut target
            //            CfExec.Item = new List<string>();
            //            CfExec.Item.Add(Sc.shortcutTargetPath);
            //            // perform search run
            //            CfExec.ProcessItemFinds();
            //            //
            //            // if changed write new shortcut
            //            if (Directory.Exists(CfExec.Item[0]))
            //            {
            //                System.IO.File.Delete(file);
            //                Sc.shortcutTargetPath = CfExec.Item[0];
            //                Sc.ShortcutWrite();
            //            }
            //        }
            //    }
            //}
        }
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            StdRunControlUi.ButtonAction = ButtonActionIs.Execute;

            // throw message instead.
            string LocalMessage = "Not Implemented Exception.";
            LocalMessage += sender.ToString();
            st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);

            //Mdm.Srt.Core.Run CfExec = new Run(this, Cf, "FixShortcut", Context, 2, 3);
            //// public Run(Object SenderPassed, DataGridView SourceObjectPassed, string ScriptNamePassed, List<string> ContextPassed, int LocateColumnIndex, int ReplaceColumnIndex)

            ////string temp = StdBaseDef.DriveOs + @"\Srt Project1\Links to Folders";
            //for (int i = 0; DirectorySelect(); DirectoryCount++)
            //{
            //    DirectoryName = openDirectoryName;
            //    ShortcutClass Sc = new ShortcutClass();

            //    foreach (string file in GetFiles(DirectoryName))
            //    {
            //        System.Console.WriteLine(file);
            //        if (file.FieldLast(".") == "lnk")
            //        {
            //            // read shortcut
            //            Sc.ShortcutRead(file);
            //            // check if target exists else:
            //            if (!Directory.Exists(Sc.shortcutTargetPath))
            //            {
            //                // load item from shortcut target
            //                CfExec.Item = new List<string>();
            //                CfExec.Item.Add(Sc.shortcutTargetPath);
            //                // perform search run
            //                CfExec.ProcessItemFinds();
            //                //
            //                // if changed write new shortcut
            //                if (Directory.Exists(CfExec.Item[0]))
            //                {
            //                    System.IO.File.Delete(file);
            //                    Sc.shortcutTargetPath = CfExec.Item[0];
            //                    Sc.ShortcutWrite();
            //                }
            //            }
            //        }
            //    }
            //}
        }
        private void ButtonDel_Click(object sender, EventArgs e)
        {
            StdRunControlUi.ButtonAction = ButtonActionIs.Delete;
            // Get Row Sequence Number
            Cf_RowDelete(ref CfData, RowNumberSelected);

            //CfDataDelete(ref CfData);
            // CfDataDelete(ref CfData, SequenceNumberSelected);
            //ControlDataGridViewRowSelectLast(Cf);

            // Delete SQL data
            // RowDeleteToEnd
        }
        private void ButtonFilter_Click(object sender, EventArgs e)
        {
            ButtonFilter_Click(false);
        }
        // ButtonPause_Click
        private void ButtonPause_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            // throw message instead.
            string LocalMessage = "Not Implemented Exception.";
            LocalMessage += sender.ToString();
            st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            StdRunControlUi.ButtonAction = ButtonActionIs.Execute;
            if (MessageFilterTask != null && (MessageFilterTask.CalcState == Thread.CalculationStatus.Calculating || MessageFilterTask.CalcState == Thread.CalculationStatus.Pending))
            {
                MessageFilterTask.StopCalculation();
            }

            if (MessageFilterTask != null && (MessageFilterTask.CalcState == Thread.CalculationStatus.Calculating || MessageFilterTask.CalcState == Thread.CalculationStatus.Pending))
            {
                //if (MainThreadId >= 0)
                //{
                //    CalculationTaskDef.FileSqlWaitForBusy(MainThreadId, 10000);
                //}
                //else {
                CalculationTaskUtils.WaitForBusy(st, MessageFilterTask.Id, 10000);
                //}
            }

            //WaitForFileSqlBusy(FileSqlScTask.Id, 10000);
            //WaitForFileSqlBusy(FileSqlScCmdTask.Id, 10000);
            //WaitForFileSqlBusy(FileSqlScPopTask.Id, 10000);
        }
        #endregion
        #region Up, Dn, Add, Filter
        private void ButtonFilter_Click(bool DeleteNext)
        {
            StdRunControlUi.ButtonAction = ButtonActionIs.Insert;
            CfIdCurrent += 1;
            CfData = new CfDataDef();
            if (CfDataNext == null)
            {
                CfData.Id = CfIdCurrent;
                CfData.Sequence = sEmpty;
                CfData.InputString = sEmpty;
                CfData.OutputString = sEmpty;
            }
            else
            {
                CfData = CfDataNext;
                CfDataNext = null;
            }

            MessageFilter_ClickThread();
            //CfRowFilter(ref CfData, DeleteNext);
            //CfRowAdd(ref CfData);
            //CfDataDelete(ref CfData, SequenceNumberSelected);
            //CfDataInsert(ref CfData, RowNumberSelected);
            if (RowNumberSelected > 0) { Cf_RowSelect(RowNumberSelected - 1); }
            //ControlDataGridViewRowSelectLast(Cf);
            GridView.Refresh();
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            ButtonAdd_Click();
        }
        private void ButtonAdd_Click()
        {
            StdRunControlUi.ButtonAction = ButtonActionIs.Insert;
            CfIdCurrent += 1;
            CfData = new CfDataDef();
            if (CfDataNext == null)
            {
                CfData.Id = CfIdCurrent;
                CfData.Sequence = sEmpty;
                CfData.InputString = sEmpty;
                CfData.OutputString = sEmpty;
            }
            else
            {
                CfData = CfDataNext;
                CfDataNext = null;
            }

            Cf_RowAdd(ref CfData);
            //CfRowAdd(ref CfData);
            //CfDataDelete(ref CfData, SequenceNumberSelected);
            //CfDataInsert(ref CfData, RowNumberSelected);
            if (RowNumberSelected > 0) { Cf_RowSelect(RowNumberSelected - 1); }
            //ControlDataGridViewRowSelectLast(Cf);
            GridView.Refresh();
            // dataGridView1.Sort(dataGridView1.Columns["DateTime"], ListSortDirection.Ascending);
        }
        private void ButtonDn_Click(object sender, EventArgs e)
        {
            Verbosity -= 1;
            ButtonFilter.Text = Verbosity.ToString() + " Do";
            return;
        }
        private void ButtonUp_Click(object sender, EventArgs e)
        {
            Verbosity += 1;
            ButtonFilter.Text = Verbosity.ToString() + " Do";
            return;
        }
        #endregion
        #region Console Buttons
        private void ButtonConsoleAll_Click(object sender, EventArgs e)
        {
            ConsolesLocal.Consoles[(int)ConsoleFormUses.All].ConsoleForm.NotifyIcon_ClickDo();
        }
        private void ButtonConsoleDatabase_Click(object sender, EventArgs e)
        {
            ConsolesLocal.Consoles[(int)ConsoleFormUses.DatabaseLog].ConsoleForm.NotifyIcon_ClickDo();
        }
        private void ButtonConsoleUser_Click(object sender, EventArgs e)
        {
            ((StdConsolesDef)ConsolesLocal).
            Consoles[(int)ConsoleFormUses.UserLog].ConsoleForm.NotifyIcon_ClickDo();
        }
        private void ButtonConsoleSystem_Click(object sender, EventArgs e)
        {
            ((StdConsolesDef)ConsolesLocal).
            Consoles[(int)ConsoleFormUses.DebugLog].ConsoleForm.NotifyIcon_ClickDo();
        }
        private void ButtonConsoleError_Click(object sender, EventArgs e)
        {
            ((StdConsolesDef)ConsolesLocal).
            Consoles[(int)ConsoleFormUses.ErrorLog].ConsoleForm.NotifyIcon_ClickDo();
        }
        #endregion
        #region Cf Row Functions
        private bool Cf_RowAdd(ref CfDataDef CfData)
        {
            string[] rowString = new string[4];
            rowString[0] = CfData.Id.ToString();
            rowString[1] = CfData.Sequence;
            rowString[2] = CfData.InputString;
            rowString[3] = CfData.OutputString;
            GridView.Rows.Add(rowString);
            RowNumberSelected = RowNumberCount = GridView.Rows.Count - 1;
            GridView.AutoResizeRow(GridView.Rows.Count);
            if (CfData.Id > CfIdCurrent) { CfIdCurrent = CfData.Id; }
            //GridView.InvalidateRow(RowNumberSelected);
            //GridView.Invalidate();
            GridView.Invalidate();
            return true;

        }
        public int Verbosity = 9;
        public int VerbosityHidden = 0;
        public int VerbosityVisible = 0;
        private bool Cf_RowSetFrom(ref CfDataDef CfData)
        {
            // ????
            string[] rowString = new string[4];
            rowString[0] = CfData.Id.ToString();
            rowString[1] = CfData.Sequence;
            rowString[2] = CfData.InputString;
            rowString[3] = CfData.OutputString;
            GridView.Rows.RemoveAt(RowNumberSelected);
            GridView.Rows.Insert(RowNumberSelected, rowString);
            return true;

        }
        private bool Cf_RowSelect(int RowNumberPassed)
        {
            GridView.FirstDisplayedScrollingRowIndex = RowNumberPassed;
            GridView.Refresh();
            GridView.CurrentCell = GridView.Rows[RowNumberPassed].Cells[1];
            GridView.Rows[RowNumberPassed].Selected = true;
            GridView.CurrentCell = GridView[1, RowNumberPassed];
            return true;
        }
        private bool Cf_RowDelete(ref CfDataDef CfData)
        {
            Cf_RowDelete(ref CfData, RowNumberSelected);
            return true;

        }
        private bool Cf_RowDelete(ref CfDataDef CfData, int RowNumberPassed)
        {
            if (GridView.Rows[RowNumberPassed].Cells[0].Value != null && GridView.Rows[RowNumberPassed].Cells[0].Value.ToString() != sEmpty)
            {
                GridView.Rows.RemoveAt(RowNumberPassed);
            }
            RowNumberCount -= 1;
            if (RowNumberPassed < RowNumberSelected) { RowNumberSelected -= 1; }
            return true;

        }

        #endregion
        #region Row Sell Management
        // CfCellValueRowLeave
        private void Cf_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            //Suppose your interested column has index 1
            if (e.Column.Index == 0 || e.Column.Index == 1 || e.Column.Index == 4)
            {
                e.SortResult = int.Parse(e.CellValue1.ToString()).CompareTo(int.Parse(e.CellValue2.ToString()));
                e.Handled = true;//pass by the default sorting
            }
        }

        private void TODOME()
        {
            // (dataGridViewFields.DataSource as DataTable).DefaultView.RowFilter = string.Format("Field = '{0}'", textBoxFilter.Text);
        }

        public override void Form_Resize(object sender, EventArgs e)
        {
            Cf_GridViewResize(sender, e);
            GridView.Refresh();
            base.Form_Resize(sender, e);
        }
        public override void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (!StdConsolesDef.Close)
            {
                NotifyIcon_ClickDo();
                e.Cancel = true;
            } else
            {
                base.Form_Closing();
            }
        }
        public override void Form_Closed(object sender, FormClosedEventArgs e)
        {
            // ToDo connection close

            //this.Hide();
            if (!StdConsolesDef.Close)
            {
                NotifyIcon_ClickDo();
                //e.Cancel = true;
            }
            else
            {
                // ToDo I don't think this belongs here.
                // It is duplicated in each of st, RunUi, Notify and Icon.
                
                //NotifyIcon.Visible = false;
                //st.StdRunControlUi.StdNotify.Visible = false;
                //st.StdRunControlUi.StdNotify.StdNotifyIcon.Visible = false;
                //st.StdRunControlUi.StdNotify.StdNotifyIcon.Dispose();
                //st.StdRunControlUi.StdNotify.Dispose();
                base.Dispose();
            }
        }
        #region Cf (Console Form) GridView, Cell Value, Row
        private void Cf_GridViewResize(object sender, EventArgs e)
        {
            GridView.Invalidate();
            if (GridView != null
                && GridView.Rows.Count > 0)
            {
                foreach (DataGridViewRow GridViewRow in GridView.Rows)
                {

                    Cf_RowResize(sender, GridViewRow.Index);
                }
            }
        }
        private void Cf_RowResize(object sender, int RowIndex)
        {
            if (RowIndex < 0) { return; }
            DataGridViewCell LocalCell = GridView.Rows[RowIndex].Cells["Message"];
            string CellText = LocalCell.Value.ToString();
            string CellText1 = (string)LocalCell.FormattedValue;
            int CellTextLength = CellText.Length;
            if (CellTextLength == 0) { return; }
            System.Drawing.Size CellSize = LocalCell.Size;
            System.Drawing.Size CellSizePreferred = LocalCell.PreferredSize;
            //GridView.AutoResizeRow(RowIndex);
            int CellPreferredHeight = GridView.Rows[RowIndex].GetPreferredHeight(
                RowIndex,
                DataGridViewAutoSizeRowMode.AllCellsExceptHeader,
                false);
            int CellLineHeightPx = 20;
            int CellCharWidthPx = 6;
            int CellNewHeight = CellTextLength * CellCharWidthPx 
                / (CellSize.Width + 1) * CellLineHeightPx;
            int tmp5 =
                (((CellSizePreferred.Width + 1) * CellSizePreferred.Height
                / (CellSize.Width + 1) 
                / CellCharWidthPx) + 0) 
                * CellLineHeightPx;
            if (CellSize.Height > CellNewHeight) { CellNewHeight = CellSize.Height; }
            if (CellLineHeightPx > CellNewHeight) { CellNewHeight = CellLineHeightPx; }
            if (CellSizePreferred.Height > CellNewHeight) { CellNewHeight = CellSizePreferred.Height; }
            GridView.Rows[RowIndex].Height = CellNewHeight;
        }
        private void Cf_RowAdded(object sender,
            DataGridViewRowsAddedEventArgs e)
        {
            Cf_RowResize(sender, e.RowIndex);
        }
        private void Cf_CellValueRowLeave(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e != null)
            // e.RowIndex
            // e.ColumnIndex
            {
                // Get Row
                RowNumberSelected = e.RowIndex;

                // ToDo Read SQL and load current?
                // code for handling creating on new last line
                if (RowNumberSelected == RowNumberCount)
                {

                }
                else if (RowNumberSelected != RowNumberSelectedLast)
                {
                    GridView.AutoResizeRow(e.RowIndex);
                    //DataGridViewCell cellIdCell = GridView[0, e.RowIndex];
                    //int cellId = (int)Convert.ToInt32(cellIdCell.Value);
                    //RowNumberSelectedLast = RowNumberSelected;
                    // temp.RowIndex;
                    //CfDataRead(ref CfData, (int)cellId);
                }
            }
        }
        // CfCellValueRowEnter
        private void Cf_CellValueRowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null)
            // e.RowIndex
            // e.ColumnIndex
            {
                // Get Row
                RowNumberSelected = e.RowIndex;

                // ToDo Read SQL and load current?
                if (RowNumberSelected != RowNumberSelectedLast)
                {
                    DataGridViewCell cellIdCell = GridView[0, e.RowIndex];
                    int cellId = (int)Convert.ToInt32(cellIdCell.Value);
                    RowNumberSelectedLast = RowNumberSelected;
                    // temp.RowIndex;
                    //CfDataRead(ref CfData, (int)cellId);
                }

                // Set Column
                DataGridViewCell cell;
                switch (e.ColumnIndex)
                {
                    case 0:
                        // exception
                        break;
                    case 1:
                        cell = GridView[e.ColumnIndex, e.RowIndex];
                        if (e.ColumnIndex == GridView.Columns["Sequence"].Index)
                        {
                            //// Update database row cell
                            //// Get Row
                            //CfDataRead(ref CfData, (int)Cf[0, e.RowIndex].Value);
                            //// SET COLUMN
                            //CfData.InputString = (string)cell.Value;

                            //// Update File
                            //CfDataWrite(ref CfData, (int)Cf[0, e.RowIndex].Value); 
                        }
                        // temp1 = (string)cell.Value;
                        // temp3 = (string)Cf[e.ColumnIndex, e.RowIndex].Value;
                        break;
                    case 2:
                        cell = GridView[e.ColumnIndex, e.RowIndex];
                        if (e.ColumnIndex == GridView.Columns["InputString"].Index)
                        {
                            //// Update database row cell
                            //// Get Row
                            //CfDataRead(ref CfData, (int)Cf[0, e.RowIndex].Value);
                            //// SET COLUMN
                            //CfData.InputString = (string)cell.Value;

                            //// Update File
                            //CfDataWrite(ref CfData, (int)Cf[0, e.RowIndex].Value); 
                        }
                        // temp1 = (string)cell.Value;
                        // temp3 = (string)Cf[e.ColumnIndex, e.RowIndex].Value;
                        break;
                    case 3:
                        //cell = Cf[e.ColumnIndex, e.RowIndex];
                        //if (e.ColumnIndex == Cf.Columns["OutputString"].Index)
                        //{
                        //    // Update database row cell
                        //    // Get Row
                        //    CfDataRead(ref CfData, (int)Cf[0, e.RowIndex].Value);
                        //    // SET COLUMN
                        //    CfData.OutputString = (string)cell.Value;

                        //    // Update File
                        //    CfDataWrite(ref CfData, (int)Cf[0, e.RowIndex].Value);
                        //}
                        //temp1 = (string)cell.Value;
                        //temp3 = (string)Cf[e.ColumnIndex, e.RowIndex].Value;
                        break;
                    default:
                        break;
                        // exception
                        // throw new NotImplementedException();
                        // throw message instead.
                        string LocalMessage = "Not Implemented Exception." 
                        + " Row Enter error. Source: " 
                        + sender.ToString()
                        + " at column:" 
                        + e.ColumnIndex.ToString();
                        st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
                        return;
                }
                // Update Row
            }
        }
        DataGridViewCell cellSave;
        private void Cf_CellValueChanged(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e != null)
            // e.RowIndex
            // e.ColumnIndex
            {
                // Get Row
                DataGridViewCell cell;
                DataGridViewCell cellIdCell = GridView[0, e.RowIndex];
                cell = GridView[e.ColumnIndex, e.RowIndex];
                string temp1, temp3;

                if (GridView[0, e.RowIndex].Value == null)
                {
                    cellSave = cell;
                    //if (Cf.Rows[e.RowIndex].IsNewRow)
                    //{
                    //CfButtonIns_Click(true);
                    CfIdCurrent += 1;
                    GridView[0, e.RowIndex].Value = CfIdCurrent;
                    //CfDataInsert(ref CfData, RowNumberSelected);
                    CfData.Id = CfIdCurrent;

                    //}
                }
                // temp.RowIndex;
                // Set Column
                switch (e.ColumnIndex)
                {
                    case 0:
                        // exception
                        break;
                    case 1:
                        cell = GridView[e.ColumnIndex, e.RowIndex];
                        if (e.ColumnIndex == GridView.Columns["Sequence"].Index)
                        {
                            //if (cellId == null)
                            //{
                            //    cell = cellSave;
                            //}
                            // Update database row cell
                            // Get Row
                            //CfDataRead(ref CfData, (int)Convert.ToInt32(Cf[0, e.RowIndex].Value));
                            // SET COLUMN
                            CfData.Sequence = (string)cell.Value;

                            // Update File
                            //CfDataWrite(ref CfData, (int)Convert.ToInt32(Cf[0, e.RowIndex].Value));
                        }
                        // temp1 = (string)cell.Value;
                        // temp3 = (string)Cf[e.ColumnIndex, e.RowIndex].Value;
                        break;
                    case 2:
                        cell = GridView[e.ColumnIndex, e.RowIndex];
                        if (e.ColumnIndex == GridView.Columns["InputString"].Index)
                        {
                            // Update database row cell
                            // Get Row
                            //CfDataRead(ref CfData, (int)Convert.ToInt32(Cf[0, e.RowIndex].Value));
                            // SET COLUMN
                            CfData.InputString = (string)cell.Value;

                            // Update File
                            //CfDataWrite(ref CfData, (int)Convert.ToInt32(Cf[0, e.RowIndex].Value));
                        }
                        // temp1 = (string)cell.Value;
                        // temp3 = (string)Cf[e.ColumnIndex, e.RowIndex].Value;
                        break;
                    case 3:
                        cell = GridView[e.ColumnIndex, e.RowIndex];
                        if (e.ColumnIndex == GridView.Columns["OutputString"].Index)
                        {
                            // Update database row cell
                            // Get Row
                            //CfDataRead(ref CfData, (int)Convert.ToInt32(Cf[0, e.RowIndex].Value));
                            // SET COLUMN
                            CfData.OutputString = (string)cell.Value;

                            // Update File
                            //CfDataWrite(ref CfData, (int)Convert.ToInt32(Cf[0, e.RowIndex].Value));
                        }
                        temp1 = (string)cell.Value;
                        temp3 = (string)GridView[e.ColumnIndex, e.RowIndex].Value;
                        break;
                    default:
                        // exception
                        // throw new NotImplementedException();
                        // throw message instead.
                        string LocalMessage = "Not Implemented Exception.";
                        LocalMessage += " Row Enter error. Source: ";
                        LocalMessage += sender.ToString();
                        LocalMessage += " at Column: ";
                        LocalMessage += e.ColumnIndex.ToString();
                        st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
                        break;
                }
                // Update Row
            }
        }
        private void Cf_CellFormatting(object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e != null)
            {
                if (GridView.Columns[e.ColumnIndex].Name == "Release Date")
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
        #endregion
    }
    public class ConsoleTypeDef : StdBaseDef
    {
        public string IconLevel;
        public string IconOrder; // ConsoleTypeDef
        public string IconName;
        public StdConsolesDef ConsolesLocal;
        public new StdConsoleManagerDef st;
        //
        public Button VerboseMore;
        public Button VerbossLess;
        //
        public TextBox ConsoleFilterText;
        public TextBox MessageCurrent;
        //
        public ConsoleFormDef ConsoleForm;
        //
        public ConsoleTypeDef()
        {
            ConsoleForm = new ConsoleFormDef();
        }
        public ConsoleTypeDef(
            string IconLevelPassed, string IconOrderPassed, string IconNamePassed,
            string TitlePassed)
        {
            IconLevel = IconLevelPassed;
            IconOrder = IconOrderPassed;
            IconName = IconNamePassed;
            Title = TitlePassed;
            ConsoleForm = new ConsoleFormDef(ref SenderIsThis, ref ConsoleSender, IconLevel, IconOrder, IconName, Title);
        }
        public ConsoleTypeDef(ref object SenderPassed, ref StdConsoleManagerDef stPassed,
            string IconLevelPassed, string IconOrderPassed, string IconNamePassed,
            string TitlePassed)
        {
            st = stPassed; base.st = st; ConsoleSender = st;
            ConsolesLocal = SenderPassed as StdConsolesDef;
            StdNotify = ConsolesLocal.StdNotify;
            StdNotifyRoot = ConsolesLocal.StdNotifyRoot;
            Sender = SenderPassed;
            IconLevel = IconLevelPassed;
            IconOrder = IconOrderPassed;
            IconName = IconNamePassed;
            Title = TitlePassed;
            //Id = ConsoleTypePassed;
            ConsoleForm = new ConsoleFormDef(ref SenderPassed, ref ConsoleSender, IconLevel, IconOrder, IconName, Title);
        }


    }
}

