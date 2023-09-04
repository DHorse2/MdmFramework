#region Dependencies
using System;
using System.Windows.Forms;
using Mdm.Oss.Decl;
#region  Mdm File Types
using Mdm.Oss.File.Type;
using Mdm.Oss.File.Control;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.Components;
#endregion
#endregion
// Readme Overview(s) at the bottom.

namespace Mdm.Oss.Std
{
    /// <summary>
    /// 0) More notes at bottom.
    /// 1) Base class for most of the class library.  Includes a basic 
    /// set of constants. 
    /// 2) Roughly equivalent to Object with a few added constants.
    /// 3 Experienced developers will quickly notice there is a lot
    /// of ASCII centric code in here and no handling of
    /// internatialization or Unicode specific code. 
    /// 4) This is a C# conversion of legacy code in most cases. 
    /// It was created as part of evaluating and learning DotNet.
    /// 5) This is part of an MVC framework. MVVC really. 
    /// Lol. (Ironic smile ensues)...
    /// Yes I know nobody needed or needs another partial framework.
    /// 6) However I do now. This is proprietary code and design.
    /// (C) Copyright David G Horsman. All rights reserved.
    /// Please see the copyright readme document distributed with this code.
    /// 
    /// Next: StdBaseDef
    /// </summary> 
    public partial class StdFormDef : Form, iClassFeatures, IDisposable
    {
        #region Standard Features Common to Console and Standard Form
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
        public StateIs RunStatus; // implemented elsewhere
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
        // //st = SenderPassed as StdConsoleManagerDef;
        // public object st; // StdConsoleManagerDef
        // public virtual ClassRoleIs ClassRole { get; set; }
        // public virtual ClassFeatureIs ClassFeatures { get; set; }
        // public virtual ConsoleSourceIs ConsoleSource { get; set; } // ConsoleObjectIsIn
        // public virtual ConsoleFormUses ConsoleFormUse { get; set; } // ConsoleFormUses
        // // Class Role
        // public virtual ClassRoleIs ClassRoleGet()
        // {
        //     return ClassRole;
        // }
        // public virtual StateIs ClassRoleSet(ClassRoleIs ClassRolePassed)
        // {
        //     ClassRole = ClassRolePassed;
        //     ClassFeaturesFlagsSet(ConsoleSource, ClassRole, ClassFeatures);
        //     return StateIs.Finished;
        // }
        // #region Console Object Methods
        // public object ConsoleSender; // dunno. !!! This is st !!!
        // public virtual ref object ConsoleGet()
        // {
        //     return ref ConsoleSender;
        // }
        // public virtual StateIs ConsoleSetFrom(ref object stPassed)
        // {
        //     // StdConsoleSet(new StdConsoleManagerDef(), new ClassRoleIs(), new ClassFeatureIs());
        //     ConsoleSender = st = stPassed;
        //     return StateIs.Finished;
        // }
        // public virtual StateIs ConsoleSetFrom(ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        // {
        //     // StdConsoleSet(new StdConsoleManagerDef(), new ClassRoleIs(), new ClassFeatureIs());
        //     ConsoleSource = ConsoleSourcePassed;
        //     ClassRole = ClassRolePassed;
        //     ClassFeatures = ClassFeaturesPassed;
        //     if (stPassed != null)
        //     {
        //         ConsoleSender = st = stPassed;
        //         return StateIs.Initialized;
        //     }
        //     else
        //     {
        //         ConsoleSender = st = null;
        //         return StateIs.EmptyValue;
        //     }
        // }
        // // Console Source
        // public virtual ConsoleSourceIs ConsoleSourceGet()
        // {
        //     return ConsoleSource;
        // }
        // public virtual StateIs ConsoleSourceSet(ConsoleSourceIs ConsoleSourcePassed)
        // {
        //     ConsoleSource = ConsoleSourcePassed;
        //     return StateIs.Finished;
        // }
        // #endregion
        // #endregion
        // #region Form Management - Parent / Child
        // public Form FormParent; // Form
        // public Form FormChild; // Form
        // public ToolStripButton RunControlUi.ButtonPause;
        // public ToolStripButton RunControlUi.ButtonCancel;
        // public ToolStripButton ButtonStart;
        // public ToolStripButton ButtonFile;
        // public ButtonActionIs ButtonAction;
        //public virtual ref Form FormParentGet()
        // {
        //     return ref FormParent;
        // }
        // public virtual StateIs FormParentSetFrom(ref Form FormParentPassed)
        // {
        //     FormParent = FormParentPassed;
        //     if (FormParent == null)
        //     {
        //         return StateIs.DoesNotExist;
        //     }
        //     else
        //     {
        //         return StateIs.DoesExist; ;
        //     }
        // }
        // public virtual ref Form FormChildGet()
        // {
        //     return ref FormChild;
        // }
        // public virtual StateIs FormChildSetFrom(ref Form FormChildPassed)
        // {
        //     FormChild = FormChildPassed;
        //     if (FormChild == null)
        //     {
        //         return StateIs.DoesNotExist;
        //     }
        //     else
        //     {
        //         return StateIs.DoesExist; ;
        //     }
        // }
        // #endregion
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
            ((iClassFeatures)st).ClassFeaturesFlagsSet(ConsoleSource, ClassRole, ClassFeatures);
            return StateIs.Finished;
        }
        #endregion
        #region Console Sender Object and Methods
        public object st; // StdConsoleManagerDef
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
                return StateIs.Initialized;
            }
            else
            {
                ConsoleSender = null;
                return StateIs.EmptyValue;
            }
            //if (ConsoleSender is StdConsoleManagerDef) { 
            st = ConsoleSender; // as StdConsoleManagerDef; }
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
                return StateIs.Initialized;
            }
            else
            {
                ConsoleSender = null;
                return StateIs.EmptyValue;
            }
            //if (ConsoleSender is StdConsoleManagerDef) {
            st = ConsoleSender; // as StdConsoleManagerDef; }

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
        #region Form and Grid Views
        #region Form Object
        public object FormParentObject;
        public object FormChildObject;
        public iStdBaseForm FormParent;
        public iStdBaseForm FormChild;
        public StdBaseRunControlUiDef StdRunControlUi;
        #endregion
        #region Form Get / Set
        public ref iStdBaseForm FormParentGet()
        {
            return ref FormParent;
        }
        public ref object FormParentObjectGet()
        {
            return ref FormParentObject;
        }
        public void FormParentSetFrom(ref iStdBaseForm ParentFormPassed)
        {
            FormParent = ParentFormPassed;
            FormParentObject = (Form)FormParent;
            if (FormParent == null)
            {
                FormStatus = StateIs.DoesNotExist;
            }
            else
            {
                FormStatus = StateIs.DoesExist; ;
            }
        }
        public void FormParentObjectSetFrom(ref object ParentFormPassed)
        {
            FormParentObject = ParentFormPassed;
            if (FormParentObject is iStdBaseForm)
            {
                FormParent = FormParentObject as iStdBaseForm;
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
        public void FormParentObjectSetFrom(ref Form ParentFormPassed)
        {
            FormParentObject = ParentFormPassed;
            if (FormParentObject is iStdBaseForm)
            {
                FormParent = FormParentObject as iStdBaseForm;
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
        #endregion
        #region Form and Grid Views
        public ref object DbListViewGet() { return ref StdRunControlUi.DbListView; }
        public void DbListViewSetFrom(ref object DbListViewPassed)
        {
            StdRunControlUi.DbListView = DbListViewPassed;
        }
        public ref DataGridView GridViewGet() { return ref StdRunControlUi.GridView; }
        public void GridViewSetFrom(ref DataGridView GridViewPassed) { StdRunControlUi.GridView = GridViewPassed; }
        #endregion
        #region Buttons, Get, Set, Enable / Disable
        // These buttons have a default control they
        // are attached to. They can be attached to
        // a differrent control or form on the fly.
        public virtual StateIs RunControlUiButtonGet(
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
        public virtual StateIs RunControlUiButtonSet(
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
        private bool FileNameOpened;
        private bool DirectoryNameOpened;
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
        public Screen ScreenObject;
        public string NameIndex = "";
        public string NameIndexPrev = "";
        public System.IntPtr HandlePtr;
        public System.IntPtr HandleMainPtr;
        public static string WindowTopmost;
        public bool FormShownFirst;
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
        #region Constructors
        /// <summary>
        /// Use the constructor: StdFormDef(long ClassFeaturesPassed)
        /// so that correct class features will be instantiated.
        /// </summary> 
        public StdFormDef(ref object SenderPassed, ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        {
            FeaturesExtract(stPassed);
            InitializeStd(ref SenderPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed);
        }
        public StdFormDef(ref object SenderPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        { InitializeStd(ref SenderPassed, ref st, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed); }
        public StdFormDef(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        { InitializeStd(ref Sender, ref st, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed); }
        public StdFormDef(ref object SenderPassed)
        { InitializeStd(ref SenderPassed, ref st, ConsoleSource, ClassRole, ClassFeatures); }
        public StdFormDef()
        { InitializeStd(ref Sender, ref st, ConsoleSource, ClassRole, ClassFeatures); }
        /// <summary>
        /// This is the recommended construstor that indicates
        /// what features will be instantiated.
        /// </summary> 
        /// <param name="ClassFeaturesPassed">The enumeration ClassUses
        /// controls which MVVC features are active.
        /// </param> 
        public StdFormDef(ref object SenderPassed, ref object stPassed)
        {
            FeaturesExtract(stPassed);
            InitializeStd(ref SenderPassed, ref stPassed, ConsoleSource, ClassRole, ClassFeatures);
        }
        #endregion
        #region Initialize, Dispose
        public virtual void FeaturesExtract(object stPassed)
        {
            iClassFeatures tmp = stPassed as iClassFeatures;
            if (tmp != null)
            {
                ConsoleSender = tmp.ConsoleGet();
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
        public virtual void InitializeStd()
        {
            StdProcess.Title = sEmpty;
            //if (ConsoleSender is ImClassFeatures)
            //{
            //    StdProcess.Title = ((ImClassFeatures)ConsoleSender).ProcessTitleGet(); // ToDo AppStd or lower.
            //}
            if (!ClassFeatureFlag.InitializeStdConstantsDone && ClassFeatureFlag.MdmTransformIsUsed)
            {
                InitializeStdConstants();
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
            base.Dispose();
            Status = StateIs.DoesNotExist;
        }
        #endregion
        #region Std Def Initialize
        public virtual void InitializeStd(ref object SenderPassed, ref object stPassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        { InitializeStd(ref SenderPassed, ref stPassed, ConsoleSourceIs.Interface, ClassRolePassed, ClassFeaturesPassed); }
        public virtual void InitializeStd(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        { InitializeStd(ref Sender, ref st, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed); }
        public virtual void InitializeStd(ref object SenderPassed, ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        {
            SenderIsThis = this;
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
                    st = ((iClassFeatures)SenderPassed).ConsoleGetTo(ref st); ConsoleSender = st;
                    StdProcess = ((iClassFeatures)SenderPassed).StdProcessGet();
                }
            }
            else
            {
                Sender = this;
            }
            // everything has a process.
            if (StdProcess == null) { StdProcess = new StdProcessDef(); }
            //Sender = SenderPassed;
            // st = StdConsoleManagerDef
            // Note: This could equally apply to the Sender
            if (stPassed != null)
            {
                if (stPassed is iClassFeatures)
                {
                    ((iClassFeatures)stPassed).ConsoleSetFrom(ref st);// ToDo Wrong? Get? Fixed.
                    ConsoleSender = st;
                }
            }
            //
            //StdRunControlUi = new StdBaseRunControlUiDef(ref Sender, ref st, StdNotify.StdKey);
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
        public FileType_LevelIs ipFileLevelId;
        public FileType_LevelIs FileLevelId
        {
            get { return ipFileLevelId; }
            set
            {
                ipFileLevelId = value;
            }
        }
        #region FileType
        public FileType_Is ipFileTypeId;
        public FileType_Is FileTypeId
        {
            get { return ipFileTypeId; }
            set
            {
                ipFileTypeId = value;
                FileTypeMajorId = FileTypeDef.FileTypeMajorGet(ipFileTypeId);
                FileTypeMinorId = FileTypeDef.FileTypeMinorGet(ipFileTypeId);
            }
        }
        public FileType_Is FileTypeMajorId;
        public FileType_Is FileTypeMinorId;
        #endregion
        #region FileSubType
        public FileType_SubTypeIs ipFileSubTypeId;
        public FileType_SubTypeIs FileSubTypeId
        {
            get { return ipFileSubTypeId; }
            set
            {
                ipFileSubTypeId = value;
                FileSubTypeMajorId = FileTypeDef.FileSubTypeMajorGet(ipFileSubTypeId);
                FileSubTypeMinorId = FileTypeDef.FileSubTypeMinorGet(ipFileSubTypeId);
            }
        }
        public FileType_SubTypeIs FileSubTypeMajorId;
        public FileType_SubTypeIs FileSubTypeMinorId;
        #endregion
        #endregion
        #region Run Control Ui Get / Set
        public virtual ref StdBaseRunControlUiDef StdRunControlUiGet()
        {
            return ref StdRunControlUi;
        }
        public virtual StateIs StdRunControlUiSetFrom(ref StdBaseRunControlUiDef RunControlUiPassed)
        {
            StdRunControlUi = RunControlUiPassed;
            return StateIs.Finished;
        }
        public virtual ref StdBaseRunControlUiDef RunControlGet(
            ref StdBaseRunControlUiDef StdRunControlUiPassed)
        {
            StdRunControlUiPassed = StdRunControlUi;
            return ref StdRunControlUi;
        }
        public virtual StateIs RunControlSet(
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
    }
}
#region Framwork Components List
/* ToDo Readme Notes. Components:
 * 
* context
* 
* Database
* 
* Base Class
* 
* Instance
* 
* List
* 
* Method
* 
* Parent
* Parent Form
* 
* Process
* 
* Property
* 
* Relationship
* 
* Sibling
* 
* States
* 
* Stream
* 
* Symbol
* 
* ThisObject
* 
* Entity Type:
* Data Type
* Data Task
* View
* State
* State View
* 
* MVC (MVVC):
* Mvc App
* Mvc Controler
* Mvc Model
* Mvc enabled Object
* 
* Framerwork:
* Console
* * Ui
* * Logging
* * StdOut/console/Output
* * Debug enabled
* Run Control
* Object State and debug data.
* Srt
* File layer
* Extrended C# syntax
* Task Processing
* Multi-threading
* Standard:
* * App API
* * Form
* * endabled Object
* Extensibility
*
* UI
* Form
* Ui Control and Components
* Ui Run Controls
* Ui Tray
* 
* */
/// <remarks>
/// Usage needs clarification (Fixed).
/// Origin: Mdm code converted as public class PickDictIndexDef
/// IMPORTANT: Many Mdm function names are being normalized.
/// References in classes to "Pick",
/// when not refering to the (multivalued) c# syntax extension
/// are part of Std.
/// </remarks>
#endregion
#region ToDo Readme Assembled Notes and documentation of classes:
#region Console/Feature Management
//public string TraceMessage;
//public StateIs Status;
//public StdConsoleManagerDef st;
//public ConsoleSourceIs ConsoleSource;
//public ClassRoleIs ClassRole;
//public ClassFeatureIs ClassFeatures;
//public object Sender;
//public Form FormParent;
//public ShortcutAnalysisFormDef FormChild; // new?
#endregion
#region File Type Area - Sql, Ascii, Text, Binary, Pick
// base, abstract, sealed, interface... ???
// Binary File - xxxxxxxxxxxxxxxxxxxxxxxx
// Ascii File - xxxxxxxxxxxxxxxxxxxxxxxxx
// Ascii Delimited RowPerLine - DEL xxxxx
// Ascii Delimited CellPerLine - DEL xxxx
// Text File - xxxxxxxxxxxxxxxxxxxxxxxxxx        
// Text Delimited File - xxxxxxxxxxxxxxxx
// Text Delimited RowPerLine - xxxxxxxxxx
// Text Delimited RowPerLine - CSV xxxxxx
// Text Delimited RowPerLine - FIX xxxxxx
// Text Delimited CellPerLine - xxxxxxxxx
// (Delimited or multivaluted objects) xx
// Text Delimited CellPerLine - Tilde xxx
// Sql File - xxxxxxxxxxxxxxxxxxxxxxxxxxx
#endregion
#region Architecture and File System Notes
///<summary>
/// Namespace Convention
/// Company.
/// Concern.
/// Role
/// Sub-role
/// Namespace Handling (role)
/// For sealed classes
/// 1) Utility Instance and Static Class
/// 2) Utility Static Class as Extension
/// Interface for all file types
/// </summary>
#region Mdm File Application Object
/// <summary> 
/// <para> Mdm File Application Object.</para>
/// <para> A Utility Object (File Console level)</para>
/// <para> .</para>
/// <para> Notes:</para>
/// <para> Text File is a regular text file.</para>
/// <para> Ascii ItemData File is a Simple Text File.</para>
/// <para> Sql File is the ItemData Table in the RDBMS.</para>
/// <para> Sql Dict is the Schema or Dictionary File for the Sql File.</para>
/// <para> DatabaseFile is the Database File that contains the Sql File.</para>
/// <para> Conn is the Connection opened to access the Sql File.</para>
/// <para> __________________________________________</para>
/// <para> .</para>
#endregion
#region File Base Class Definition
/// <para> File Base Class Definition</para>
/// <para> .</para>
/// <para> File</para>
/// <para> ..Ascii</para>
/// <para> ..Text</para>
/// <para> ..Binary</para>
/// <para> Database File</para>
/// <para> ..Sql</para>
/// <para> ....MS Sql</para>
/// <para> ....MY Sql</para>
/// <para> ..Db2</para>
/// <para> ..Pick</para>
/// <para> __________________________________________</para>
/// <para> .</para>
/// <para> File Type Area:</para>
///	<para> Sql, Ascii, Text, Binary, Pick</para>
/// <para> .</para>
/// <para> File class usage:</para>
/// <para> base, abstract, sealed, interface... ???</para>
/// <para> __________________________________________</para>
/// <para> .</para>
/// <para> File Type Organization:</para>
/// <para> Binary File</para>
/// <para> Ascii File</para>
/// <para> Ascii Delimited RowPerLine - DEL</para>
/// <para> Ascii Delimited CellPerLine - DEL</para>
/// <para> Text File</para>
/// <para> Text Delimited File</para>
/// <para> Text Delimited RowPerLine</para>
/// <para> Text Delimited RowPerLine - CSV</para>
/// <para> Text Delimited RowPerLine - FIX</para>
/// <para> Text Delimited CellPerLine</para>
/// <para> Text Delimited CellPerLine - Tilde</para>
/// <para> Sql File</para>
/// <para> Pick File</para>
/// <para> __________________________________________</para>
/// <para> .</para>
/// <para> File XXXX is virtual and will be overriden in the subclasses when implemented</para>
/// <para> Therefore SqlOpen, AsciiFileOpen and TextFileOpen become FileOpen in the SqlFile, AsciiFile, TextFile classes</para>
/// <para> __________________________________________</para>
/// <para> .</para>
/// <para> File Management Objects:</para>
/// <para> DbMaster - Database Master Files</para>
/// <para> Db - Database Tables being accessed</para>
/// <para> File - Primary File Stream</para>
/// <para> Item - A record item or complete data block</para>
/// <para> Buf - A file stream, ring buffer, pipe, etc.</para>
/// <para> .</para>
#endregion
#region Schema Management Objects
/// <para> Schema Management Objects:</para>
/// <para> DbDict - Schema for file</para>
/// <para> PickDict - Pick style dictionary schema.</para>
/// <para> Dict - Flat file schema (Tld, CSV)</para>
/// <para> XmlDict - Schema for XML</para>
/// <para> __________________________________________</para>
/// <para> .</para>
/// <para> A File System Object contains two File Stream Objects:</para>
/// <para> 1) Fmain, the Main or Primary File Stream Object. </para>
/// <para> ...and...</para>
/// <para> 1) Faux, the Auxillary File Stream Object. </para>
/// <para> .</para>
/// <para> Each File Stream Object is composed of:</para>
/// <para> 2) A File Summary object. </para>
/// <para> For each of the File and DB sub-objects:</para>
/// <para> 3) An ID identification object. </para>
/// <para> 4) An IO object to move data to and from the file. </para>
/// <para> 5) One or more File Status Objects. </para>
/// <para> 6) One or more Row and Column Management objects. </para>
/// <para> At the Stream or System level:</para>
/// <para> 7) Additional objects created by extended classes. </para>
/// <para> 8) Meta data is also present for the File. 
/// Internal Data is present in the form of run control,
/// exceptions handling, threading, messaging, etc. </para>
/// <para> .</para>
/// <para> In general, a File Stream Object should either use
/// the File sub-object for various file actions or it should
/// use the DB sub-object for database IO.</para>
/// <para> However, File and DB sub-objects can be reused or 
/// used concurrently keeping in mind that they share
/// a common set of fields and belong to one File Stream Object.</para>
/// <para> __________________________________________</para>
/// <para> .</para>
/// <para> The Core (or primary) set of objects and lists is
/// ordered by:</para>
/// <para> 1) System, </para>
/// <para> 2) Service, </para>
/// <para> 3) Server, </para>
/// <para> 4) Database, </para>
/// <para> 5) FileOwner, </para>
/// <para> 6) FileGroup, </para>
/// <para> 7) Table and DiskFile.</para>
/// <para> __________________________________________</para>
/// <para> .</para>
#endregion
#region Run Control
/// <para> Types of Run Control Object:</para>
/// <para> File Action - Contains the file action verb and a few details</para>
/// <para> File Transformation - Contains an input, output and direction</para>
/// <para> __________________________________________</para>
/// <para> .</para>
/// <para> Types of File Object classes:</para>
/// <para> General design pattern.  Each type of file object has:</para>
/// <para> .</para>
/// <para> Summary - Contains database, user, security, system and other database and additional info</para>
/// <para> Id - Contains the name of the file, path, Id and basic information</para>
/// <para> Io - Contains I/O Objects such as streams, readers, buffers</para>
/// <para> Status - Status setting related to a file</para>
/// <para> Options - User option flags for a faile</para>
/// <para> </para>
/// <para> Row / Item / Record Level Objects:</para>
/// <para> ItemDef - Contains an Item block, Id, version and a few flags</para>
/// <para> RowInfo - Counters, indexing and status for a row</para>
/// </summary> 
#endregion
#region Note on passing by reference:
/// <remarks>
/// <para> 
/// PROCEED TO SqlDictProcessDb(String PassedFileName </para> 
/// <para> __________________________________________</para>
/// <para> .</para>
/// <para> Note on passing by reference:</para>
/// <para> 
/// All Methods [Object]Check[State] take Passed 
/// Names and Options, States and Actions.</para>
/// <para> </para>
/// <para> 
/// They do NOT set THIS fields, Options, States 
/// or Actions to the passed values.  These methods are 
/// independant of THIS and be able to act on any Passed Name.</para>
/// <para> .</para>
/// <para> 
/// The rationelle for this is that each Method might 
/// need to Check various States of different Files, perhaps 
/// even on different Servers, without 
/// Altering the State of This Object or in particular,</para>
/// <para> 
/// The Options and Actions such as:</para>
/// <para> 
/// Open, Delete, Connect, Count,  CheckExists, 
/// SchemaChange, RowAdd, RowColumnUpdate, etc.</para>
/// <para> </para>
/// <para> 
/// The States or Options and Actions on This Object include 
/// flags such as ConnDoKeepConn which indicates that the 
/// Using Class wants This Object to ultimately be returned 
/// in an Open State.</para>
/// <para> .</para>
/// </remarks> 
#endregion
#region File Main Def
/// <summary>
/// <para> (Main) File Stread Object </para>
/// <para> See mFile for an expanded discussion
/// of these objects.  This is the object that
/// performs file IO for a mFile File System
/// Object.  There is a primary and auxillary object.</para>
/// </summary>
/// <remarks>
/// <para> List of classes used:</para>
/// <para> General objects:</para>
/// <para> ....Io State</para>
/// <para> ....File Action</para>
/// <para> ....File Summary</para>
/// <para> ....File Status</para>
/// <para> Ascii File Objects:</para>
/// <para> ....File Id (contained in File Summary)</para>
/// <para> ....File Io (contained in File Summary)</para>
/// <para> ....File Opt(ions) (contained in File Summary)</para>
/// <para> ....Item</para>
/// <para> ....Buf</para>
/// <para> ....Buf Io</para>
/// <para> Database File Objects:</para>
/// <para> ....Db Io</para>
/// <para> ....Db Status</para>
/// <para> ....System Status</para>
/// <para> ....Server Status</para>
/// <para> ....Conn(ection) Status</para>
/// <para> Column and Row Management:</para>
/// <para> ....Row Info(rmation)</para>
/// <para> ....Col(umn) Index</para>
/// <para> Primary and Auxillary Column Indexing:</para>
/// <para> ....Row Info(rmation)</para>
/// <para> Column Transformation:</para>
/// <para> ....Col(umn)Transform</para>
/// <para> Other:</para>
/// <para> ....Del(imiter)Sep(arator)</para>
/// </remarks>
#endregion
#endregion
#region Console ToDo (2014)
//using Mdm.Oss.Thread;
// ToDo $$MAJOR 1) Create TraceMdm Aurgument structure
// ToDo $$MAJOR 2) Create Indexer for RunAction and remove redundant settings
// ToDo $$MAJOR 2) should use indexers passed to set RunAction and RunMetric values
// ToDo $$MAJOR 2) which will reduce lines of code.
// ToDo $$MAJOR 4) Don't pass run action values to TraceMdm but use current value.
// ToDo $$MAJOR 5) Implement TLD for data after testing dict to schema code.
#endregion
#endregion
