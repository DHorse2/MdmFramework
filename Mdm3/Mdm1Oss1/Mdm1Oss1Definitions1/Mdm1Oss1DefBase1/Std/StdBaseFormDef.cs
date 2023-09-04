#region Dependencies
using System;
using System.Windows.Forms;
using System.Windows.Controls;

#region  Mdm Core
using Mdm;
using Mdm.Oss;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.File.Control;
//@@@CODE@@@using Mdm.Oss.Mapp;
// using Mdm.Oss.Mobj;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
//using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
//using Mdm.Oss.Threading;
using Mdm.Oss.Components;
//using Mdm.World;
#endregion
#region  Mdm Db and File
//using Mdm.Oss.File;
using Mdm.Oss.File.Type;
//using Mdm.Oss.File.Control;
//using Mdm.Oss.File.Db;
//using Mdm.Oss.File.Db.Data;
//using Mdm.Oss.File.Db.Table;
//using Mdm.Oss.File.Db.Thread;
//using Mdm.Oss.File.Properties;
//using Mdm.Oss.File.RunControl;
#endregion
#endregion

namespace Mdm.Oss.Std
{
    public partial class StdBaseFormDef : Form, iStdBaseForm, AppStd, iClassFeatures, IDisposable
    {
        #region Standard Features Common to Console and Standard Form
        public string Title;
        public int Id;
        public string ItemId;
        #region Process
        public StdProcessDef StdProcess;
        public ref StdProcessDef StdProcessGet()
        {
            return ref StdProcess;
        }
        public void StdProcessSetFrom(ref StdProcessDef StdProcessPassed)
        {
            StdProcess = StdProcessPassed;
        }
        #endregion
        #region Key
        public StdKeyDef StdKey;
        public StdKeyDef KeyGet()
        { return StdKey; }
        public void KeySet(StdKeyDef StdKeyPassed)
        { StdKey = StdKeyPassed; }
        #endregion
        #region Notify Group
        public StdNotifyDef StdNotifyRoot;
        public StdNotifyDef StdNotify;
        public StdNotifyIconDef StdNotifyIcon;
        public bool StdNotifyEnabled;
        public ref StdNotifyDef StdNotifyGet(string Target)
        {
            if (Target == null || Target == "this" || Target == "")
            {
                return ref StdNotify;
            }
            else if (Target == "Root")
            {
                return ref StdNotifyRoot;
            }
            else if (Target == "Console")
            {
                return ref ((iClassFeatures)ConsoleSender).StdNotifyGet("this");
            }
            else
            {
                // throw;
            }
            return ref StdNotify;
        }
        public void StdNotifySet(ref StdNotifyDef StdNotifyPassed, string Target)
        {
            if (Target == null || Target == "this" || Target == "")
            {
                StdNotify = StdNotifyPassed;
            }
            else if (Target == "Root")
            {
                StdNotifyRoot = StdNotifyPassed;
            }
            else if (Target == "Console")
            {
                ((iClassFeatures)ConsoleSender).StdNotifySet(ref StdNotifyPassed, "this");
            }
            else
            {
                // throw;
            }
        }
        public void StdNotifySetFrom(ref StdNotifyDef StdNotifyPassed, string Target)
        {
            StdNotifyPassed = StdNotify;
            if (Target == null || Target == "this" || Target == "")
            {
                StdNotifyPassed = StdNotify;
            }
            else if (Target == "Root")
            {
                StdNotifyPassed = StdNotifyRoot;
            }
            else if (Target == "Console")
            {
                StdNotifyPassed = ((iClassFeatures)ConsoleSender).StdNotifyGet("this");
            }
            else
            {
                // throw;
            }
            StdNotifyPassed = StdNotify;
        }
        #endregion
        #region State
        public StateIs Status; // of "this" object. ToDo Needs review and rationalization.
        public DataStatusIs RunStatus; // implemented elsewhere
        public FileStatusDef FileStatus; // mFile level
        public StateIs ConsoleStatus; // not implemented yet, implemented elsewhere.
        public DataStatusIs DataStatus; // mFile Data level
        public StateIs FormStatus; // mFile Data level
        // standard object state.
        public bool ClassBusy;
        public bool ClassClosed;
        public bool ClassDisposed;
        public bool ClassEnabled;
        public bool ClassInitialized;
        public bool ClassUsed;
        public bool ClassOpen;
        public bool ClassVisble;
        #endregion
        // #region Notes: Standard Console / Feature Management
        // // ToDo This may not be useable. (re: ref Object)
        // ToDo Results. It's not in the " : base(???) calls.
        // ToDo Weirdly. You can then drop it into
        // ToDo Sender, st, SenderIsThis, FormParent and so on.
        // ToDo From that point the FW works as designed.
        // ToDo Std Objects get resolved to their specific
        // ToDo types in StdConsoleManager. End Results.
        // // In theory this is a redundant set here below
        // // in that the ConsoleGet gets called in the root class.
        // // My knowledge of Upcasting to Object with respect
        // // to ref fields and paramaters is insufficient here.
        // 0331. Stil is.
        // // : base(ref SenderPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        // // does not work regardless of variation in syntax (true).
        // It's contradictory
        // // and may be related to resolving types and the type Object.
        // // Possibly it must be passed as an interface type. Dunno.
        // Note. Resolved. Their is a core (std)
        // and the Console Manager used by all apps and components.
        // // End ToDo.

        #region Standard Console / Feature Management
        #region Class / Features
        // ToDo This may not be useable. (re: ref Object) Fixed.
        // In theory this is a redundant set here below
        // in that the ConsoleGet get called in the root class.
        // My knowledge of Upcasting to Object with respect
        // to ref fields and paramaters is insufficient here.
        // : base(ref SenderPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        // does not work regardless of variation in syntax. It's contradictory
        // and may be related to resolving types and the type Object.
        // Possibly it must be passed as an interface type. Dunno.
        // End ToDo. Fixed.
        //st = SenderPassed as StdConsoleManagerDef;
        //public object st; // StdConsoleManagerDef
        public virtual ClassRoleIs ClassRole { get; set; }
        public virtual ClassFeatureIs ClassFeatures { get; set; }
        public virtual ConsoleSourceIs ConsoleSource { get; set; } // ConsoleObjectIsIn
        public virtual ConsoleFormUses ConsoleFormUse { get; set; } // ConsoleFormUses
        // Class Role
        public virtual ClassRoleIs ClassRoleGet()
        {
            return ClassRole;
        }
        public virtual StateIs ClassRoleSet(ClassRoleIs ClassRolePassed)
        {
            ClassRole = ClassRolePassed;
            ((iClassFeatures)ConsoleSender).ClassFeaturesFlagsSet(ConsoleSource, ClassRole, ClassFeatures);
            return StateIs.Finished;
        }
        #endregion
        #region Console Sender Object and Methods
        //public StdConsoleManagerDef st; // StdConsoleManagerDef
        public object ConsoleSender; // This is st.
        public virtual ref object ConsoleGet()
        {
            return ref ConsoleSender;
        }
        public virtual ref object ConsoleGetTo(ref object stPassed)
        {
            stPassed = ConsoleSender;
            return ref ConsoleSender;
        }
        public virtual StateIs ConsoleSetFrom(ref object stPassed)
        {
            if (stPassed != null)
            {
                ConsoleSender = stPassed;
                if (ConsoleSender is StdConsoleManagerDef)
                {
                    st = ConsoleSender as StdConsoleManagerDef;
                }
                return StateIs.Initialized;
            }
            else
            {
                ConsoleSender = null;
                return StateIs.EmptyValue;
            }
            return StateIs.Finished;
        }
        public virtual StateIs ConsoleSetFrom(ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        {
            // StdConsoleSet(new StdConsoleManagerDef(), new ClassRoleIs(), new ClassFeatureIs());
            ConsoleSource = ConsoleSourcePassed;
            ClassRole = ClassRolePassed;
            ClassFeatures = ClassFeaturesPassed;
            if (stPassed != null)
            {
                ConsoleSender = stPassed;
                //if (ConsoleSender is StdConsoleManagerDef) {
                st = ConsoleSender as StdConsoleManagerDef;
                // }
                return StateIs.Initialized;
            }
            else
            {
                ConsoleSender = null;
                return StateIs.EmptyValue;
            }
        }
        // Console Source
        public virtual ConsoleSourceIs ConsoleSourceGet()
        {
            return ConsoleSource;
        }
        public virtual StateIs ConsoleSourceSet(ConsoleSourceIs ConsoleSourcePassed)
        {
            ConsoleSource = ConsoleSourcePassed;
            return StateIs.Finished;
        }
        // Console Features
        public virtual ClassFeatureIs ClassFeaturesGet()
        {
            return ClassFeatures;
        }
        public virtual StateIs ClassFeaturesSet(ClassFeatureIs ClassFeaturePassed)
        {
            ClassFeatures = ClassFeaturePassed;
            ((iClassFeatures)st).ClassFeaturesFlagsSet(ConsoleSource, ClassRole, ClassFeatures);
            return StateIs.Finished;
        }
        #endregion
        #endregion
        #region File Object
        public object FileObject;
        #endregion
        #region Form and Grid View
        #region Form Object
        private object zFormParentObject;
        private bool zFormParentBusy;
        public object FormParentObject
        {
            get
            {
                return zFormParentObject;
            }
            set
            {
                zFormParentObject = value;
                if (!zFormParentBusy && zFormParentObject is iStdBaseForm)
                {
                    FormParent = zFormParentObject as iStdBaseForm;
                }
                if (FormParent == null)
                {
                    FormStatus = StateIs.DoesNotExist;
                }
                else
                {
                    FormStatus = StateIs.DoesExist; ;
                }
            }
        }
        public object FormChildObject;
        public iStdBaseForm zFormParent;
        public iStdBaseForm FormParent
        {
            get
            {
                return zFormParent;
            }
            set
            {
                zFormParentBusy = true;
                zFormParent = value;
                FormParentObject = zFormParent;
                if (zFormParent == null)
                {
                    FormStatus = StateIs.DoesNotExist;
                }
                else
                {
                    FormStatus = StateIs.DoesExist; ;
                }
                zFormParentBusy = false;
            }
        }
        public iStdBaseForm FormChild;
        #endregion
        #region Visible
        public bool zVisible;
        public new bool Visible
        {
            get
            {
                return zVisible;
            }
            set
            {
                zVisible = value;
                base.Visible = zVisible;
            }
        }
        public bool VisibleGet()
        {
            return zVisible;
        }
        public void VisibleSet(bool VisiblePassed)
        {
            Visible = VisiblePassed;
        }
        public bool VisibleOnRestore;
        #endregion
        #region Form Get / Set
        public ref iStdBaseForm FormParentGet()
        {
            return ref zFormParent;
        }
        public ref object FormParentObjectGet()
        {
            return ref zFormParentObject;
        }
        public void FormParentSetFrom(ref iStdBaseForm ParentFormPassed)
        {
            FormParent = ParentFormPassed;
            FormParentObject = ParentFormPassed;
        }
        public void FormParentObjectSetFrom(ref Form ParentFormPassed)
        {
            FormParentObject = ParentFormPassed;
        }
        public void FormParentObjectSetFrom(ref object ParentFormPassed)
        {
            FormParentObject = ParentFormPassed;
        }
        #endregion
        #region Grid Views
        public DataGridView GridView;
        public bool GridViewIsExternal;
        public StateIs GridViewStatus;
        //
        public object DbListView;
        public bool DbListViewControlIsUsed;
        public bool UseConsoleButtons;
        public ref object DbListViewGet() { return ref DbListView; }
        public void DbListViewSetFrom(ref object DbListViewPassed)
        { DbListView = DbListViewPassed; }
        public ref DataGridView GridViewGet() { return ref GridView; }
        public void GridViewSetFrom(ref DataGridView GridViewPassed) { GridView = GridViewPassed; }
        // Run Control Ui Grid View
        public ref object DbListViewGetDefault() { return ref StdRunControlUi.DbListView; }
        public void DbListViewSetDefaultFrom(ref object DbListViewPassed)
        {
            StdRunControlUi.DbListView = DbListViewPassed;
        }
        public ref DataGridView GridViewGetDefault() { return ref StdRunControlUi.GridView; }
        public void GridViewSetDefaultFrom(ref DataGridView GridViewPassed) { StdRunControlUi.GridView = GridViewPassed; }
        #endregion
        #region Buttons, Get, Set, Enable / Disable
        // These buttons have a default control they
        // are attached to. They can be attached to
        // a differrent control or form on the fly.
        public virtual StateIs StdRunControlUiButtonGet(
        ref ToolStripButton ButtonPausePassed,
        ref ToolStripButton ButtonCancelPassed,
        ref ToolStripButton ButtonStartPassed,
        ref ToolStripButton ButtonFilePassed,
        ref ButtonActionIs ButtonActionPassed,
        ref ToolStripLabel LabelDbBusyMessagePassed)
        {
            ButtonPausePassed = StdRunControlUi.ButtonPause;
            ButtonCancelPassed = StdRunControlUi.ButtonCancel;
            ButtonStartPassed = StdRunControlUi.ButtonStart;
            ButtonFilePassed = StdRunControlUi.ButtonFile;
            ButtonActionPassed = StdRunControlUi.ButtonAction;
            LabelDbBusyMessagePassed = StdRunControlUi.LabelDbBusyMessage;
            return StateIs.Finished;
        }
        public virtual StateIs StdRunControlUiButtonSet(
        ref ToolStripButton ButtonPausePassed,
        ref ToolStripButton ButtonCancelPassed,
        ref ToolStripButton ButtonStartPassed,
        ref ToolStripButton ButtonFilePassed,
        ref ButtonActionIs ButtonActionPassed,
        ref ToolStripLabel LabelDbBusyMessagePassed)
        {
            StdRunControlUi.ButtonPause = ButtonPausePassed;
            StdRunControlUi.ButtonCancel = ButtonCancelPassed;
            StdRunControlUi.ButtonStart = ButtonStartPassed;
            StdRunControlUi.ButtonFile = ButtonFilePassed;
            StdRunControlUi.ButtonAction = ButtonActionPassed;
            StdRunControlUi.LabelDbBusyMessage = LabelDbBusyMessagePassed;
            return StateIs.Finished;
        }
        public virtual void ButtonEnable()
        {
            ButtonDbEnable();
        }
        public virtual void ButtonDbEnable()
        {
        }
        public virtual void ButtonDbDisable()
        {
        }
        public virtual void ButtonDisable()
        {
            ButtonDbDisable();
        }
        #endregion
        public bool LoadCellSelection;
        public EventArgs LoadCellArgs;
        #endregion
        #region App Run Fields
        public string FileNameOpen, DirectoryNameOpen;
        public bool FileOpened;
        public bool DirectoryOpened;
        public bool DoingRefresh;
        public int MainThreadId = -1;
        public bool MainThreadIdDoSet;
        #endregion
        #region Messages
        public LocalMsgDef LocalMessage;
        public mMsgDetailsDef MessageDetails;
        public mMsgSendToPageArgsDef MessageMdmSendToPageArgs;
        #endregion
        #region Class Object Identity - Senders
        // Mdm Class Level Senders
        public object Sender; // Orgin of event (sender). The Control or the app.
        public object SenderIsThis; // this. 
        public object SenderParent; // Parent of Sender
        #endregion
        #region Mdm MVC Objects - App, Page, PageMain and DbDetail
        public System.Windows.Application AppObject;
        public System.Windows.Controls.Page PageObject; // Temporary?
        //
        public System.Windows.Controls.Page PageMainObject;
        public String PageMainReturnValue;
        public bool PageMainInitialized;
        //
        public System.Windows.Controls.Page DbDetailPageObject;
        public String DbDetailPageReturnValue;
        public bool DbDetailPageInitialized;
        #endregion
        #region Screen Object
        public static StdKeyDef StdKeyCurr;
        public static StdKeyDef StdKeyPrev;
        public static string WindowTopmost;
        public string WindowTopmostPrev;
        public static string WindowTopmostNotSet;
        //
        public Screen ScreenObject;
        public string NameIndex;
        public string NameIndexPrev;
        public System.IntPtr HandlePtr;
        public System.IntPtr HandleMainPtr;
        public bool FormShownInitialized;
        #endregion
        #region Class Standard Root Word Constants
        // ToDo this should be a struct. No, a dictionary?
        // ON = YES = OK = true
        // OFF = NO = BAD = false
        #region Primitive Constants
        public const bool xON = true; // not const.

        public const bool bON = true;
        public const bool bOFF = false;
        //
        public const bool bYES = true;
        public const bool bYes = true;
        public const bool bNO = false;
        public const bool bNo = false;
        //
        public const bool bOK = true;
        public const bool bBAD = false;
        //
        public const int iON = 1;
        public const int iOFF = 0;
        //
        public const int iYES = 1;
        public const int iNO = 0;
        //
        public const int iOK = 1;
        public const int iBAD = 0;
        //
        public const string sUnknown = "unknown";
        public const string sEmpty = "";
        public const int iUnknown = 99999;
        #endregion
        #endregion
        #region CharacterConstants
        // System Standard Functions Character Constants.
        // 1) These can be initialized and are altered by
        // certain utility functions. Otherwise they would
        // be constants. Maybe "new" should would work.
        // 2) Anyway, they are left undefined in order to 
        // speed things up.
        public String Temp;
        // Ascii Delimiters
        public static String Comma;
        // public const String Collon = @":";
        public static String Collon;
        public static String Dot;
        public static String CrLf;
        public static String Cr;
        public static String Lf;
        public static String Eof;
        // public static String Eot;
        // Special Ascii Characters
        public static String Esc; // Escape
        public static String Tld;
        public static String Asterisk;
        public static String Stick;
        public static String BackSlash;
        public static String ForwardSlash;
        public static String AtSymbol;
        // White Space Characters
        public static String Ff; // FormFeed
        public static String Bs; // Backspace
        public static String Tab; // Tab
        public static String Sp;
        // Tab ; Horizontal Tab
        // Vtab ; Vertical Tab
        // Null
        public static String Null;
        // Constants: Status Verbose 
        // </Section Summary>
        public virtual void InitializeStdConstants()
        {
            ClassFeatureFlag.InitializeStdConstantsDone = true;
            Temp = sEmpty;
            // Ascii Delimiters
            Comma = ",";
            Collon = ":";
            Dot = ".";
            CrLf = ((char)13).ToString() + ((char)10).ToString();
            Cr = ((char)13).ToString();
            Lf = ((char)10).ToString();
            Eof = ((char)26).ToString();
            // Eot =  ((char)26).ToString();
            // Special Ascii Characters
            Esc = ((char)27).ToString(); // Escape
            Tld = "~";
            Asterisk = "*";
            Stick = "|";
            BackSlash = @"\";
            ForwardSlash = @"/";
            AtSymbol = @"@";
            // White Space Characters
            Ff = ((char)12).ToString(); // FormFeed
            Bs = ((char)08).ToString(); // Backspace
            Tab = ((char)09).ToString(); // Tab
            Sp = ((char)32).ToString();
            // Tab ; Horizontal Tab
            // Vtab ; Vertical Tab
            // Null
            Null = ((char)00).ToString();
            // Constants: Status Verbose
            return;
        }
        #endregion
        #region Initialize, FeaturesExtract, Dispose
        public virtual void FeaturesExtract(object stPassed)
        {
            iClassFeatures tmp = stPassed as iClassFeatures;
            if (tmp != null)
            {
                //ConsoleSender = tmp.ConsoleGet();
                ConsoleSource = tmp.ConsoleSourceGet();
                ClassRole = tmp.ClassRoleGet();
                ClassFeatures = tmp.ClassFeaturesGet();
            }
            else if (ConsoleSender is iClassFeatures)
            {
                ClassRole = ((iClassFeatures)ConsoleSender).ClassRoleGet();
                ClassFeatures = ((iClassFeatures)ConsoleSender).ClassFeaturesGet();
            }
        }
        public new void Dispose()
        {
            Dispose(Status);
            // this is the base.Dispose();
            Status = StateIs.DoesNotExist;
        }
        public void Dispose(StateIs StatusPassed)
        {
            if (StdNotifyIcon != null)
            { StdNotifyIcon.Dispose(); }

            if (StdNotify != null)
            { StdNotify.Dispose(); }

            if (StdNotifyRoot != null)
            { StdNotifyRoot.Dispose(); }

            if (st != null) { st.Dispose(); }

            if (StdRunControlUi != null)
            { StdRunControlUi.Dispose(); }

            base.Dispose();
            Status = StateIs.DoesNotExist; 
        }
        #endregion
        #region Initialize Std
        #region Std Def Initialize
        public virtual void InitializeStd()
        { InitializeStd(ref Sender, ref ConsoleSender, ConsoleSource, ClassRole, ClassFeatures); }
        public virtual void InitializeStd(ref object SenderPassed, ref object stPassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        { InitializeStd(ref SenderPassed, ref stPassed, ConsoleSourceIs.Interface, ClassRolePassed, ClassFeaturesPassed); }
        public virtual void InitializeStd(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        { InitializeStd(ref Sender, ref ConsoleSender, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed); }
        public virtual void InitializeStd(ref object SenderPassed, ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        {
            SenderIsThis = this;
            NameIndex = "";
            NameIndexPrev = "";
            //if (ConsoleSender is ImClassFeatures)
            //{
            //    StdProcess.Title = ((ImClassFeatures)ConsoleSender).ProcessTitleGet(); // ToDo AppStd or lower.
            //}
            if (!ClassFeatureFlag.InitializeStdConstantsDone && ClassFeatureFlag.MdmTransformIsUsed)
            {
                InitializeStdConstants();
            }
            //if (ClassFeatureFlag == null)
            //{ ClassFeatureFlag = new ClassFeatureFlagDef(); }
            ClassFeaturesFlagsSet(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed);
            #region Sender / Object Source validation / defaults
            // st = StdConsoleManagerDef
            // Note: This could equally apply to the Sender
            if (ConsoleSender == null)
            {
                if (stPassed != null)
                {
                    if (stPassed is iClassFeatures)
                    {
                        ConsoleSender = stPassed;
                        ((iClassFeatures)stPassed).ConsoleSetFrom(ref ConsoleSender);// ToDo Wrong? Get? Fixed.
                        st = ConsoleSender as StdConsoleManagerDef;
                    }
                }
                //
                if (SenderPassed != null) { Sender = SenderPassed; }
                if (Sender == null) { Sender = this; }
                if (FormParent == null)
                {
                    if (Sender is iStdBaseForm)
                    {
                        FormParent = (iStdBaseForm)Sender;
                        FormParentObject = Sender;
                    }
                    else if (Sender is Form)
                    {
                        // If someone set it, don't change it.
                        if (FormParentObject == null)
                        {
                            FormParentObject = Sender;
                        }
                    }
                }
                // 1) By implication this second Form
                // (different from the Parent where the
                // Child Form is still unassigned.)
                // is the Child form of the Parent.
                // 2) However I think it would make
                // more sense to switch the Child form
                // to point at this new calling form.
                if (FormChildObject == null || Sender != FormParentObject)
                {
                    if (Sender is iStdBaseForm)
                    {
                        FormChild = (iStdBaseForm)Sender;
                        FormChildObject = Sender;
                    }
                    else if (Sender is Form)
                    {
                        // If someone set it, don't change it.
                        if (FormChildObject == null)
                        {
                            FormChildObject = Sender;
                        }
                    }
                }
            }
            //if (st == null) { st = this; }
            //if (st == null)  { } // Error
            if (ConsoleSender != null)
            {
                if (FormParent == null) { FormParent = ((iClassFeatures)ConsoleSender).FormParentGet(); }
                if (StdProcess == null)
                {
                    // It defaults to the Form Parent.
                    if (FormParent != null) { StdProcess = ((iClassFeatures)FormParent).StdProcessGet(); }
                    // Otherwise it is owned by the Console
                    if (StdProcess == null && st != null && ConsoleSender != this) { StdProcess = ((iClassFeatures)ConsoleSender).StdProcessGet(); }
                    // Lacking both we create it on what is a first pass in default mode.
                    if (StdProcess == null) { StdProcess = new StdProcessDef(); }
                    // If the console lacks a process it defaults to the first one.
                    if (ConsoleSender != null && ConsoleSender != this)
                    {
                        if (((iClassFeatures)ConsoleSender).StdProcessGet() == null)
                        {
                            ((iClassFeatures)ConsoleSender).StdProcessSetFrom(ref StdProcess);
                        }
                    }
                    if (FormParent != null && FormParent != this)
                    {
                        // Each Form is a separate process. Don't set it!
                        // Because the console is shared between forms.
                        //((iClassFeatures)FormParent).StdProcessSetFrom(ref StdProcess);
                    }
                }
                if (StdKey == null) { StdKey = StdProcess.StdKey; }
                if (StdKey == null) { StdKey = new StdKeyDef(this.ToString()); }
                if (StdRunControlUi == null)
                {
                    // The Console hold a pointer to the current run.
                    if (StdRunControlUi == null && ConsoleSender != this) { StdRunControlUi = ((iClassFeatures)ConsoleSender).StdRunControlUiGet(); }
                    //
                    // Otherwise the form's main Run Ui object is the default.
                    // Note the form can have one per each processing function.
                    // The form will dynamically change the Console's (st's) pointer as needed.
                    // In the form, each process should have a Run Ui and also have
                    // a third which is "current running" and shared with the Console.
                    //
                    if (StdRunControlUi == null && FormParent != null) { StdRunControlUi = ((iClassFeatures)FormParent).StdRunControlUiGet(); }
                    //
                    // On a run in default mode it may not exist yet. Create it.
                    if (StdRunControlUi == null) { StdRunControlUi = new StdBaseRunControlUiDef(ref Sender, ref ConsoleSender, StdKey); }
                    // 
                    if (ConsoleSender != this)
                    {
                        // Don't alter it in the base classes. You might hammer
                        // a run in progress.
                        if (((iClassFeatures)ConsoleSender).StdRunControlUiGet() == null)
                        {
                            ((iClassFeatures)ConsoleSender).StdRunControlUiSetFrom(ref StdRunControlUi);
                        }
                    }
                    //
                    if (FormParent != null && FormParent != this)
                    {
                        // Don't alter it in the base classes. You might hammer
                        // a run in progress.
                        if (((iClassFeatures)FormParent).StdRunControlUiGet() == null)
                        {
                            ((iClassFeatures)FormParent).StdRunControlUiSetFrom(ref StdRunControlUi);
                        }
                    }
                }
            }
            //
            // This is default program control of Forms
            SenderIsThis = this;
            #endregion
            #region Object Source
            ConsoleSource = ConsoleSourcePassed;
            // Notes: Console Sender ConsoleSender
            // NOT st (the handle that is passed around)
            // In Self and Parent this is the Console
            // and st should point to this.
            // When an app is calling other classes
            // it will pass the st pointer.
            // (202101) Sender ? still dunno. st?
            // (202103) Left in for future use.
            switch (ConsoleSource)
            {
                case (ConsoleSourceIs.Self):
                    // An App object need a new controller object.
                    // Or its intergrated in a Utility App with a
                    // console control it might show.
                    SenderParent = SenderPassed;
                    //ConsoleSender = this;
                    //st = this;
                    break;
                case (ConsoleSourceIs.Parent):
                    // The control lives in the App.
                    SenderParent = SenderPassed;
                    //ConsoleSender = SenderPassed;
                    //st = this;
                    break;
                case (ConsoleSourceIs.External):
                    // The caller controls these values
                    SenderParent = SenderPassed;
                    //ConsoleSender = SenderPassed;
                    break;
                case (ConsoleSourceIs.Interface):
                    // The caller controls these values.
                    SenderParent = SenderPassed;
                    //ConsoleSender = SenderPassed;
                    break;
                case (ConsoleSourceIs.None):
                    SenderParent = this;
                    //ConsoleSender = this;
                    break;
                default:
                    // exception
                    break;
            }
            #endregion
        }
        public virtual void InitializeStdY(ref object SenderPassed, ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        {
            SenderIsThis = this;
            //if (ConsoleSender is ImClassFeatures)
            //{
            //    StdProcess.Title = ((ImClassFeatures)ConsoleSender).ProcessTitleGet(); // ToDo AppStd or lower.
            //}
            if (!ClassFeatureFlag.InitializeStdConstantsDone && ClassFeatureFlag.MdmTransformIsUsed)
            {
                InitializeStdConstants();
            }
            //if (ClassFeatureFlag == null)
            //{ ClassFeatureFlag = new ClassFeatureFlagDef(); }
            ClassFeaturesFlagsSet(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed);
            #region Sender / Object Source validation / defaults
            //if (SenderPassed != null)
            //{
            //    if (SenderPassed is iClassFeatures)
            //    {
            //        //st = stPassed;
            //        ConsoleSender = ((iClassFeatures)SenderPassed).ConsoleGetTo(ref ConsoleSender);
            //        st = ConsoleSender; // as StdConsoleManagerDef;
            //    }
            //}
            //else
            //{
            //    Sender = this;
            //}
            //Sender = SenderPassed;
            // st = StdConsoleManagerDef
            // Note: This could equally apply to the Sender
            if (st == null)
            {
                if (stPassed != null)
                {
                    if (stPassed is iClassFeatures)
                    {
                        ConsoleSender = stPassed;
                        ((iClassFeatures)stPassed).ConsoleSetFrom(ref ConsoleSender);// ToDo Wrong? Get? Fixed.
                        st = ConsoleSender as StdConsoleManagerDef;
                    }
                }
                //
                if (SenderPassed != null) { Sender = SenderPassed; }
                if (Sender == null) { Sender = this; }
                if (FormParent == null)
                {
                    if (Sender is iStdBaseForm)
                    {
                        FormParent = (iStdBaseForm)Sender;
                        FormParentObject = Sender;
                    }
                    else if (Sender is Form)
                    {
                        // If someone set it, don't change it.
                        if (FormParentObject == null)
                        {
                            FormParentObject = Sender;
                        }
                    }
                }
                // 1) By implication this second Form
                // (different from the Parent where the
                // Child Form is still unassigned.)
                // is the Child form of the Parent.
                // 2) However I think it would make
                // more sense to switch the Child form
                // to point at this new calling form.
                if (FormChildObject == null && Sender != FormParentObject)
                {
                    if (Sender is iStdBaseForm)
                    {
                        FormChild = (iStdBaseForm)Sender;
                        FormChildObject = Sender;
                    }
                    else if (Sender is Form)
                    {
                        // If someone set it, don't change it.
                        if (FormChildObject == null)
                        {
                            FormChildObject = Sender;
                        }
                    }
                }
            }
            //if (st == null) { st = this; }
            //if (st == null)  { } // Error
            if (st != null)
            {
                if (StdProcess == null) { StdProcess = ((iClassFeatures)st).StdProcessGet(); }
                if (StdProcess == null)
                {
                    StdProcess = new StdProcessDef();
                    ((iClassFeatures)st).StdProcessSetFrom(ref StdProcess);
                }
                if (StdKey == null) { StdKey = StdProcess.StdKey; }
                if (StdKey == null) 
                {
                    if (Name.Length > 0)
                    { StdKey = new StdKeyDef(Name); }
                    else
                    { StdKey = new StdKeyDef(this.ToString()); }
                }
                if (StdRunControlUi == null) { StdRunControlUi = ((iClassFeatures)st).StdRunControlUiGet(); }
                if (StdRunControlUi == null)
                {
                    StdRunControlUi = new StdBaseRunControlUiDef(ref Sender, ref ConsoleSender, StdKey);
                    ((iClassFeatures)st).StdRunControlUiSetFrom(ref StdRunControlUi);
                }
            }
            //
            // This is default program control of Forms
            SenderIsThis = this;
            #endregion
            #region Object Source
            ConsoleSource = ConsoleSourcePassed;
            // Notes: Console Sender ConsoleSender
            // NOT st (the handle that is passed around)
            // In Self and Parent this is the Console
            // and st should point to this.
            // When an app is calling other classes
            // it will pass the st pointer.
            // (202101) Sender ? still dunno. st?
            // (202103) Left in for future use.
            switch (ConsoleSource)
            {
                case (ConsoleSourceIs.Self):
                    // An App object need a new controller object.
                    // Or its intergrated in a Utility App with a
                    // console control it might show.
                    SenderParent = SenderPassed;
                    //ConsoleSender = this;
                    //st = this;
                    break;
                case (ConsoleSourceIs.Parent):
                    // The control lives in the App.
                    SenderParent = SenderPassed;
                    //ConsoleSender = SenderPassed;
                    //st = this;
                    break;
                case (ConsoleSourceIs.External):
                    // The caller controls these values
                    SenderParent = SenderPassed;
                    //ConsoleSender = SenderPassed;
                    break;
                case (ConsoleSourceIs.Interface):
                    // The caller controls these values.
                    SenderParent = SenderPassed;
                    //ConsoleSender = SenderPassed;
                    break;
                case (ConsoleSourceIs.None):
                    SenderParent = this;
                    //ConsoleSender = this;
                    break;
                default:
                    // exception
                    break;
            }
            #endregion
        }
        public virtual void InitializeStdX(ref object SenderPassed, ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        {
            SenderIsThis = this;
            StdProcess.Title = sEmpty;
            //if (ConsoleSender is ImClassFeatures)
            //{
            //    StdProcess.Title = ((ImClassFeatures)ConsoleSender).ProcessTitleGet(); // ToDo AppStd or lower.
            //}
            if (!ClassFeatureFlag.InitializeStdConstantsDone && ClassFeatureFlag.MdmTransformIsUsed)
            {
                InitializeStdConstants();
            }
            //if (ClassFeatureFlag == null)
            //{ ClassFeatureFlag = new ClassFeatureFlagDef(); }
            ClassFeaturesFlagsSet(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed);
            #region Sender / Object Source validation / defaults
            if (SenderPassed != null)
            {
                Sender = SenderPassed;
                if (SenderPassed is iClassFeatures)
                {
                    //st = stPassed;
                    ConsoleSender = ((iClassFeatures)SenderPassed).ConsoleGetTo(ref ConsoleSender);
                    st = ConsoleSender as StdConsoleManagerDef;
                }
            }
            else
            {
                Sender = this;
            }
            //Sender = SenderPassed;
            // st = StdConsoleManagerDef
            // Note: This could equally apply to the Sender
            if (stPassed != null)
            {
                if (stPassed is iClassFeatures)
                {
                    ((iClassFeatures)stPassed).ConsoleSetFrom(ref ConsoleSender);// ToDo Wrong? Get? Fixed.
                    st = ConsoleSender as StdConsoleManagerDef;
                }
            }
            //if (st == null) { st = this; }
            //if (st == null)  { } // Error
            if (st != null)
            {
                // everything has a process.
                StdProcess = ((iClassFeatures)st).StdProcessGet();
                if (StdProcess == null) 
                { 
                    StdProcess = new StdProcessDef();
                    ((iClassFeatures)st).StdProcessSetFrom(ref StdProcess);
                }
                StdRunControlUi = ((iClassFeatures)st).StdRunControlUiGet();
                if (StdRunControlUi == null) 
                {
                    StdRunControlUi = new StdBaseRunControlUiDef(ref Sender, ref ConsoleSender, StdKey);
                    ((iClassFeatures)st).StdRunControlUiSetFrom(ref StdRunControlUi);
                }
            }
            //
            // This is default program control of Forms
            if (Sender is iStdBaseForm)
            {
                FormParent = (iStdBaseForm)Sender;
            }
            if (Sender is Form)
            {
                // If someone set it, don't change it.
                if (FormParentObject == null)
                {
                    FormParentObject = (Form)Sender;
                }
                else
                {
                    // 1) By implication this second Form
                    // (different from the Parent where the
                    // Child Form is still unassigned.)
                    // is the Child form of the Parent.
                    // 2) However I think it would make
                    // more sense to switch the Child form
                    // to point at this new calling form.
                    if (FormChildObject == null || FormChildObject != FormParentObject)
                    {
                        FormChildObject = (Form)Sender;
                    }
                }
            }
            SenderIsThis = this;
            #endregion
            #region Object Source
            ConsoleSource = ConsoleSourcePassed;
            // Notes: Console Sender ConsoleSender
            // NOT st (the handle that is passed around)
            // In Self and Parent this is the Console
            // and st should point to this.
            // When an app is calling other classes
            // it will pass the st pointer.
            // (202101) Sender ? still dunno. st?
            // (202103) Left in for future use.
            switch (ConsoleSource)
            {
                case (ConsoleSourceIs.Self):
                    // An App object need a new controller object.
                    // Or its intergrated in a Utility App with a
                    // console control it might show.
                    SenderParent = SenderPassed;
                    //ConsoleSender = this;
                    //st = this;
                    break;
                case (ConsoleSourceIs.Parent):
                    // The control lives in the App.
                    SenderParent = SenderPassed;
                    //ConsoleSender = SenderPassed;
                    //st = this;
                    break;
                case (ConsoleSourceIs.External):
                    // The caller controls these values
                    SenderParent = SenderPassed;
                    //ConsoleSender = SenderPassed;
                    break;
                case (ConsoleSourceIs.Interface):
                    // The caller controls these values.
                    SenderParent = SenderPassed;
                    //ConsoleSender = SenderPassed;
                    break;
                case (ConsoleSourceIs.None):
                    SenderParent = this;
                    //ConsoleSender = this;
                    break;
                default:
                    // exception
                    break;
            }
            #endregion
        }
        #endregion
        #region Class Features
        /// <summary>
        /// </summary> 
        /// <param name="ClassFeaturesPassed">The enumeration ClassUses
        /// controls which MVVC features are active.
        /// </param> 
        public virtual void ClassFeaturesFlagsSet(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        {
            ConsoleSource = ConsoleSourcePassed;
            ClassRole = ClassRolePassed;
            ClassFeatures = ClassFeaturesPassed;
            // Role as Mdm
            if (ClassRole == ClassRoleIs.RoleAsMdm)
            { ClassFeatureFlag.MdmIsUsed = true; }
            // Role uses Ui or Console
            if (ClassFeatures == ClassFeatureIs.MdmRunControl
                || ClassFeatures == ClassFeatureIs.MaskStautsUi
                || ClassRole == ClassRoleIs.RoleAsMdm
                || ClassRole == ClassRoleIs.RoleAsUtility)
            {
                // Flags for optimization
                ClassFeatureFlag.MdmRunIsUsed = true;
                ClassFeatureFlag.MdmConsoleIsUsed = true;
                ClassFeatureFlag.StatusUiIsUsed = true;
            }
            // Class Feature optimizations
            // Object management
            if (ClassFeatures == ClassFeatureIs.LocalMessage) { ClassFeatureFlag.LocalMessageIsUsed = true; }
            if (ClassFeatures == ClassFeatureIs.ExternalId) { ClassFeatureFlag.ExternalIdIsUsed = true; }
            if (ClassFeatures == ClassFeatureIs.LocalId) { ClassFeatureFlag.LocalIdIsUsed = true; }
            // Low level features
            if (ClassFeatures == ClassFeatureIs.MdmUtilTrace) { ClassFeatureFlag.MdmTraceIsUsed = true; }
            if (ClassFeatures == ClassFeatureIs.MdmUtilConsole) { ClassFeatureFlag.MdmConsoleIsUsed = true; }
            if (ClassFeatures == ClassFeatureIs.MdmUtilSend) { ClassFeatureFlag.MdmSendIsUsed = true; }
            if (ClassFeatures == ClassFeatureIs.MdmUtilPrint) { ClassFeatureFlag.MdmPrintIsUsed = true; }
            if (ClassFeatures == ClassFeatureIs.MaskButton) { ClassFeatureFlag.MdmButtonIsUsed = true; }
            //
            // Are Threads, Utilities or the UI being used?
            if (ClassFeatures == ClassFeatureIs.MaskThread) { ClassFeatureFlag.MdmThreadIsUsed = true; }
            if (ClassFeatures == ClassFeatureIs.MaskUtility) { ClassFeatureFlag.MdmIsUsed = true; }
            if (ClassFeatures == ClassFeatureIs.MaskUi) { ClassFeatureFlag.MdmUiIsUsed = true; }
            // If so set 
            // MdmRunIsUsed, MdmFileIsUsed, MdmTransformIsUsed
            if (ClassFeatureFlag.MdmThreadIsUsed || ClassFeatureFlag.MdmIsUsed || ClassFeatureFlag.MdmUiIsUsed)
            {
                ClassFeatureFlag.MdmRunIsUsed = true;
                ClassFeatureFlag.MdmFileIsUsed = true;
                ClassFeatureFlag.MdmTransformIsUsed = true;
            }
        }
        #endregion
        #region Class Feature Flag
        /// <summary>
        /// Boolean flags (an optimization) indicating which 
        /// class features are active.
        /// ToDo optimize (eliminate<-no) this for new usage. InProgress.
        /// </summary> 
        public struct ClassFeatureFlagDef
        {
            #region Feature bools intended to optimize features.
            public bool StatusUiIsUsed;

            public bool LocalMessageIsUsed;
            public bool ExternalIdIsUsed;
            public bool LocalIdIsUsed;

            public bool MdmRunIsUsed; // new true;
            public bool MdmFileIsUsed; // new true;
            public bool MdmTransformIsUsed; // new true;

            public bool MdmIsUsed;
            public bool MdmTraceIsUsed;
            public bool MdmConsoleIsUsed;
            public bool MdmSendIsUsed;
            public bool MdmPrintIsUsed;

            public bool MdmButtonIsUsed;
            public bool MdmControlIsUsed;

            public bool MdmThreadIsUsed;

            public bool MdmUiIsUsed;
            // Flags for what was already initialized.
            // 1) Note. const obviously don't get initialized
            // but do in conversion and transformation
            // where file formats have equivalents for data
            // separators exist and change.
            // 2) The Initialize calls are expensive
            // Your class features determine which
            // init functions gets called.
            // 3) Preferably you can call the inits
            // for the features you use and
            // avoid using the nested inits.
            // 4) ToDo Result. Read me.
            // 4) ToDo Issue.  Where are exceptions and delegates managed?
            // 4) ToDo Result. There are a dozen interfaces used
            // 4) ToDo Result. in the framework.
            // 4) ToDo Result. They are feature specific. However the
            // 4) ToDo Result. class hieararchy is expressed in the
            // 4) ToDo Result. inheritance chain ending in AppStdStateView.
            // 4) ToDo Result. While an MVC app included this is an
            // 4) ToDo Result. ETL / Conversion Utility Framework below that.
            // Base Class (- State, Features and standard objects)
            public bool StdBasePickSyntaxDone;
            public bool InitializeStdConstantsDone;
            public bool InitializeStdCharsDone;
            public bool InitializeStdConversionDone;
            // Run and Console
            public bool InitializeTraceDone;
            public bool InitializeConsoleDone;
            public bool InitializeRunDone;
            // Pick dispaly and printers
            public bool InitializeStdDisplayDone;
            public bool InitializeStdPrinterDone;
            //
            public bool StdBaseRunDefDone;
            // The Console and State management main class
            public bool InitializeStdBaseRunFileConsoleDone;
            // mFile handling
            public bool InitializeFile;
            public bool InitializeFileSql;
            public bool InitializeFileSqlConn;
            public bool InitializeFileMain;
            public bool InitializeFileSummaryDef;
            #endregion
        }
        public ClassFeatureFlagDef ClassFeatureFlag;
        #endregion
        #region File Type
        // FileTypeName
        //public String spFileType;
        //public String FileTypeName {
        //    get { return spFileType; }
        //    set { spFileType = value; }
        //}
        public FileType_LevelIs zFileLevelId;
        public FileType_LevelIs FileLevelId
        {
            get { return zFileLevelId; }
            set
            {
                zFileLevelId = value;
            }
        }
        #region FileType
        public FileType_Is zFileTypeId;
        public FileType_Is FileTypeId
        {
            get { return zFileTypeId; }
            set
            {
                zFileTypeId = value;
                FileTypeMajorId = FileTypeDef.FileTypeMajorGet(zFileTypeId);
                FileTypeMinorId = FileTypeDef.FileTypeMinorGet(zFileTypeId);
            }
        }
        public FileType_Is FileTypeMajorId;
        public FileType_Is FileTypeMinorId;
        #endregion
        #region FileSubType
        public FileType_SubTypeIs zFileSubTypeId;
        public FileType_SubTypeIs FileSubTypeId
        {
            get { return zFileSubTypeId; }
            set
            {
                zFileSubTypeId = value;
                FileSubTypeMajorId = FileTypeDef.FileSubTypeMajorGet(zFileSubTypeId);
                FileSubTypeMinorId = FileTypeDef.FileSubTypeMinorGet(zFileSubTypeId);
            }
        }
        public FileType_SubTypeIs FileSubTypeMajorId;
        public FileType_SubTypeIs FileSubTypeMinorId;
        #endregion
        #endregion
        #region Run Control Ui Get / Set
        public StdBaseRunControlUiDef StdRunControlUi;
        public virtual ref StdBaseRunControlUiDef StdRunControlUiGet()
        {
            return ref StdRunControlUi;
        }
        public virtual StateIs StdRunControlUiSetFrom(ref StdBaseRunControlUiDef RunControlUiPassed)
        {
            StdRunControlUi = RunControlUiPassed;
            return StateIs.Finished;
        }
        public virtual ref StdBaseRunControlUiDef StdRunControlGet(
            ref StdBaseRunControlUiDef StdRunControlUiPassed)
        {
            StdRunControlUiPassed = StdRunControlUi;
            return ref StdRunControlUi;
        }
        public virtual StateIs StdRunControlSet(
            ref StdBaseRunControlUiDef StdRunControlUiPassed)
        {
            if (StdRunControlUi != null)
            {
                StdRunControlUiPassed = StdRunControlUi;
                return StateIs.DoesExist;
            }
            else
            {
                StdRunControlUi = new StdBaseRunControlUiDef();
                StdRunControlUiPassed = StdRunControlUi;
                return StateIs.Created;
            }
        }
        #endregion
        #endregion
        //
        #region Standard Console / Standard Form level.
        public StdConsoleManagerDef st; // not an object
        static StdBaseFormDef()
        {
            WindowTopmost = "~~NotSet";
            WindowTopmostNotSet = WindowTopmost;
            StdKeyCurr = new StdKeyDef("~~NotSet");
            StdKeyPrev = new StdKeyDef("~~NotSet");
        }
        #region Console Sender Object and Methods
        public virtual ref StdConsoleManagerDef ConsoleGetTo(ref StdConsoleManagerDef stPassed)
        {
            stPassed = st;
            return ref st;
        }
        public virtual StateIs ConsoleSetFrom(ref StdConsoleManagerDef stPassed)
        {
            // StdConsoleSet(new StdConsoleManagerDef(), new ClassRoleIs(), new ClassFeatureIs());
            st = stPassed as StdConsoleManagerDef;
            ConsoleSender = stPassed;
            return StateIs.Finished;
        }
        #endregion
        public StdBaseFormDef(ref object SenderPassed, ref object stPassed)
        {
            FeaturesExtract(stPassed);
            InitializeStd(ref SenderPassed, ref stPassed, ConsoleSource, ClassRole, ClassFeatures);
            ScreenGet();
            TopMostInitialize(ref ScreenObject);
        }
        public StdBaseFormDef()
        {
            //FeaturesExtract(ConsoleSender);
            //InitializeStd(ref SenderIsThis, ref ConsoleSender, ConsoleSource, ClassRole, ClassFeatures);
            //ScreenGet();
            //TopMostInitialize(ref ScreenObject);
            //InitializeStd(ref Sender, ref ConsoleSender, ConsoleSource, ClassRole, ClassFeatures); 
        }
        #endregion
        #endregion
    }
}