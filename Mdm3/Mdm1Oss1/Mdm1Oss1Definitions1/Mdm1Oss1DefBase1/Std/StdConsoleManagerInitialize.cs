using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Mdm.Oss.Decl;
using Mdm.Oss.Console;
using Mdm.Oss.File;
using Mdm.Oss.Thread;
using Mdm.Oss.Std;
using Mdm.Oss.Components;

namespace Mdm.Oss.Std
{
    public partial class StdConsoleManagerDef
    {
        #region Console Flags, Delegates and Initialize
        // $include Mdm.Oss.Decl StdDefBaseRunFileConsole 
        /// <summary>
        /// Console Base class initialization after constructor.
        /// Feature and code groups controlled are:
        /// Message areas:
        ///  Console = sEmpty;
        ///  StatusLine = sEmpty;
        ///  Progress = sEmpty;
        ///  Error = sEmpty;
        ///  RunAction = sEmpty;
        ///
        /// Method group names:
        /// 1) TraceMdmDo
        /// 2) MessageMdmSendTo
        /// 3) PrintOutputMdm
        /// 4) PrintOutputMdm_Pick
        /// 5) TextReaderWriter
        /// 6) StreamReaderWriter
        /// 7) ConsoleMdmStd
        /// 8) ConsoleMdmPick
        ///
        /// Notes and history:
        /// These routines combine status line (or box) handling along with
        /// console display.
        ///
        /// Validate Target processing:
        /// The message prefix is examined (1 or more characters) to determine
        /// the type of message and route it to the appropriate user interface
        /// element.
        ///
        /// Validate Output Marshalling:
        /// Character 1 - Target:
        /// To Console:
        /// C - 
        /// example: CHello there
        /// 
        /// Messages to Text Box or Status Line 
        /// (currently not separate) 
        /// Either a Text Box, Combo Box or Status Line Control
        /// will be set.  Line mode uses strings and would be
        /// consumed by a user defined control.
        /// Two message types are currently supported:
        /// A - Add or set text
        /// M - Message not for display
        /// example A1Hello upper box
        /// example M2Hello lower box
        ///
        /// When fully implmented routed messages will include:
        /// A(dd)9 to line N (using defaults).
        /// C(onsole)9 message.
        /// F(ile stream)9.
        /// L(ine area)9 of Status Line.
        /// T(arget box)9 number (Text, Combo, Flow Document).
        /// M(essage) not for display, i.e. commands.
        /// P(rogress bar) control.
        /// V(alue) of property (i.e. Progress Bar).
        /// Lo(w) Value, H(igh) Value (Minimum, Maximum).
        ///
        /// Delegates set to base class method implementations by default.
        ///
        /// The feature tests in code logic tend to follow the following order:
        /// 1) A User Interface is in use.
        /// 2) Start, Cancel, Pause / Resume buttons are used.
        /// 3) Message processing used.
        /// 4) Application is multi-threaded.
        /// 5) Trace, Logging and Pick Console and Console Options.
        /// 6) Ui Interface Box, Status Line and Console are in use.
        /// 6.1) Text or Combo Boxes are used
        /// 6.2) Box Management is used
        /// 6.3) A User Interface is in use
        ///
        /// </summary> 
        /// <param name="RunFileConsolePassed"></param> 
        /// <remarks>
        /// 1) References to the Pick Console apply to legacy application
        /// implementations that are console or green screen based.
        /// PRINTER or DISPLAY handling (controlled by PRINTER ON|OFF)
        /// 2) PrintOutputMdm_PickPrint NewLine *** does actual output
        /// 3) Routing generally depends on flags such as TraceOn.
        /// And make use of methods including TraceMdm.
        /// The flags ConsoleOn || 
        /// ConsoleBasicOn direct 
        /// output to ConsoleMdmPickDisplayImpl
        /// 4) However all messages originating in background or worker
        /// thread (that lack a user interface) must pass through
        /// ThreadUiProgressAsync
        /// </remarks> 
        public void ConsoleMdmInitialize(ref Object RunFileConsolePassed)
        {
            // ConsoleMdmFlagsInitialize(RunFileConsolePassed);
            //((StdConsoleManagerDef)RunFileConsolePassed).XUomUrvvXvCreateNow = true;
            if (RunFileConsolePassed is StdConsoleManagerDef)
            {   // Run Control
                if (ClassFeatureFlag.MdmRunIsUsed)
                {
                    // ((StdConsoleManagerDef)RunFileConsolePassed).Run ???;
                }
                // A User Interface is in use.
                if (ClassFeatureFlag.MdmUiIsUsed)
                {
                    ((StdConsoleManagerDef)RunFileConsolePassed).ProgressBarMdm1 = ProgressBarMdm1;
                }
                // Hook up buttons: Start, Cancel, Pause / Resume buttons are used.
                // Note this is setting the passed console's buttons to "this"'s buttons.
                if (ClassFeatureFlag.MdmButtonIsUsed)
                {
                    ((StdConsoleManagerDef)RunFileConsolePassed).StdRunControlUi.ButtonStart = StdRunControlUi.ButtonStart;
                    ((StdConsoleManagerDef)RunFileConsolePassed).StdRunControlUi.ButtonCancel = StdRunControlUi.ButtonCancel;
                    ((StdConsoleManagerDef)RunFileConsolePassed).StdRunControlUi.ButtonPause = StdRunControlUi.ButtonPause;
                }
                // Message processing used.
                // Delegates set to base class.
                //if (ClassFeature.MdmSendIsUsed)
                //{
                //    ((StdConsoleManagerDef)RunFileConsolePassed).MessageMdmSendToPageNewLineSet = MessageMdmSendToPageNewLineImpl;
                //    ((StdConsoleManagerDef)RunFileConsolePassed).MessageMdmSendToPageNewLine = MessageMdmSendToPageNewLineSetImpl;
                //    ((StdConsoleManagerDef)RunFileConsolePassed).MessageMdmSendToPage = MessageMdmSendToPageImpl;
                //}
                // Application is multi-threaded.
                //if (ClassFeature.MdmThreadIsUsed)
                //{
                //    //((StdConsoleManagerDef)RunFileConsolePassed).ThreadUiProgressAsync = ThreadUiProgressAsync;
                //    ((StdConsoleManagerDef)RunFileConsolePassed).ThreadUiTextMessageAsync = ThreadUiTextMessageAsync;
                //    //((StdConsoleManagerDef)RunFileConsolePassed).ThreadUiProgressAsyncInvoke = ThreadUiProgressAsyncInvoke;
                //    ((StdConsoleManagerDef)RunFileConsolePassed).ThreadUiTextMessageAsyncInvoke = ThreadUiTextMessageAsyncInvoke;
                //}
                ((StdConsoleManagerDef)RunFileConsolePassed).RunControlOn = RunControlOn;
                // Trace, Logging and Pick Console and Console Options.
                if (ClassFeatureFlag.MdmConsoleIsUsed)
                {
                    ((StdConsoleManagerDef)RunFileConsolePassed).TraceData = TraceData;
                    ((StdConsoleManagerDef)RunFileConsolePassed).TraceOn = TraceOn;
                    ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleOn = ConsoleOn;
                    ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleBasicOn = ConsoleBasicOn;
                    ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleToDisc = ConsoleToDisc;
                    //
                    ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleVerbosity = ConsoleVerbosity;
                    //
                    ((StdConsoleManagerDef)RunFileConsolePassed).DoLogActivity = DoLogActivityDefault;
                    ((StdConsoleManagerDef)RunFileConsolePassed).TraceDisplayMessageDetail = TraceDisplayMessageDetail;
                    ((StdConsoleManagerDef)RunFileConsolePassed).TraceHeadings = TraceHeadings;
                }
            }
        }
        /// <summary>
        /// The Flag Initialize sets trace and console control flags only, not data.
        /// </summary> 
        /// <param name="RunFileConsolePassed">The console object to apply flags to.</param> 
        public void ConsoleMdmInitializeFlags(ref Object RunFileConsolePassed)
        {
            // ToDo Exceptions. Gured this.
            ((StdConsoleManagerDef)RunFileConsolePassed).RunControlOn = bON;
            // Std_I0_Console
            if (((StdConsoleManagerDef)RunFileConsolePassed).ConsoleVerbosity == 0)
            {
                ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleVerbosity = 9;
            }
            // Class Role - do nothing.
            // ToDo Implement Class Features per Import Srt
            if (ClassFeatureFlag.MdmConsoleIsUsed)
            {
                // ToDo *** CONSOLE CONTROL
                ((StdConsoleManagerDef)RunFileConsolePassed).TraceDebugOn = bYes;
                ((StdConsoleManagerDef)RunFileConsolePassed).TraceDebugDoErrorPrompt = bNo;
                ((StdConsoleManagerDef)RunFileConsolePassed).TraceBreakOnAll = bNo;
                ((StdConsoleManagerDef)RunFileConsolePassed).TraceData = bNO;
                ((StdConsoleManagerDef)RunFileConsolePassed).TraceOn = bYES;
                //
                ((StdConsoleManagerDef)RunFileConsolePassed).DoLogActivity = bYES;
                ((StdConsoleManagerDef)RunFileConsolePassed).TraceDisplayMessageDetail = bYES;
                ((StdConsoleManagerDef)RunFileConsolePassed).TraceHeadings = bNO;
                //
                if (((StdConsoleManagerDef)RunFileConsolePassed).ConsoleVerbosity == 0) {
                    ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleVerbosity = 5;
                }
                // Display
                ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleOutput = sEmpty;
                ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleOutputLog = sEmpty;
                // <Area Id = "((StdConsoleManagerDef)RunFileConsolePassed).Console">

                ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleOn = bON;
                ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleBasicOn = bON;
                ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleTextOn = bON;
                ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleToControl = bON;
                ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleToDisc = bOFF;
                // Display
                ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleOutput = sEmpty;
                ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleOutputLog = sEmpty;
                ((StdConsoleManagerDef)RunFileConsolePassed).ConsolePickTextConsole = sEmpty;
                // public ((StdConsoleManagerDef)RunFileConsolePassed).ConsolePickTextBlock;
                ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleTextBlock = sEmpty; // text block
                ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleTextPositionX = 0;
                ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleTextPositionY = 0;
                ((StdConsoleManagerDef)RunFileConsolePassed).ConsoleTextPositionZ = 0;
                // Standard Messaging
                ((StdConsoleManagerDef)RunFileConsolePassed).sMessageText = sEmpty;
                ((StdConsoleManagerDef)RunFileConsolePassed).sMessageText0 = sEmpty;
                ((StdConsoleManagerDef)RunFileConsolePassed).MessageTextOutConsole = sEmpty;
                ((StdConsoleManagerDef)RunFileConsolePassed).MessageTextOutStatusLine = sEmpty;
                ((StdConsoleManagerDef)RunFileConsolePassed).MessageTextOutProgress = sEmpty;
                ((StdConsoleManagerDef)RunFileConsolePassed).MessageTextOutError = sEmpty;
                ((StdConsoleManagerDef)RunFileConsolePassed).MessageTextOutRunAction = sEmpty;
                // <Area Id = "TextConsole>
                ((StdConsoleManagerDef)RunFileConsolePassed).CommandLineRequest = sEmpty;
                ((StdConsoleManagerDef)RunFileConsolePassed).CommandLineRequestResult = 0;
                ((StdConsoleManagerDef)RunFileConsolePassed).TextConsole = sEmpty;
            }

            //if (ClassFeature.StatusUiIsUsed)
            //{
            //    if (((StdConsoleManagerDef)RunFileConsolePassed).StatusUi.BoxManageIsUsed)
            //    {
            //        ((StdConsoleManagerDef)RunFileConsolePassed).StatusUi.Box1Manage.ScrollDo = true;
            //        ((StdConsoleManagerDef)RunFileConsolePassed).StatusUi.Box2Manage.ScrollDo = true;
            //        ((StdConsoleManagerDef)RunFileConsolePassed).StatusUi.Box3Manage.ScrollDo = true;
            //        ((StdConsoleManagerDef)RunFileConsolePassed).StatusUi.Box4Manage.ScrollDo = true;
            //        ((StdConsoleManagerDef)RunFileConsolePassed).StatusUi.TextConsoleManage.ScrollDo = true;
            //    }
            //}
        }
        #endregion
        #region Constructors
        /// <summary> 
        /// Use the StdConsoleManagerDef(long ClassFeaturesPassed) constructor
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        /// <summary> 
        /// Instantiates the class passing flags indicating which
        /// features and classes are implemented. 
        /// Or a subset to be used by this object.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public StdConsoleManagerDef(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        {
            st = base.st as StdConsoleManagerDef;
            if (ClassFeatureFlag.MdmRunIsUsed
            || ClassFeatureFlag.MdmConsoleIsUsed
            || ClassFeatureFlag.StatusUiIsUsed)
            {
                InitializeStdBaseRunFileConsole();
            }
        }
        public StdConsoleManagerDef(ref object SenderPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
            : base(ref SenderPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        {
            st = base.st as StdConsoleManagerDef;
            if (ClassFeatureFlag.MdmRunIsUsed
            || ClassFeatureFlag.MdmConsoleIsUsed
            || ClassFeatureFlag.StatusUiIsUsed)
            {
                InitializeStdBaseRunFileConsole();
            }
        }
        public StdConsoleManagerDef()
            : base(ConsoleSourceIs.None, ClassRoleIs.None, ClassFeatureIs.None)
        {
            st = base.st as StdConsoleManagerDef;
            if (ClassFeatureFlag.MdmRunIsUsed
            || ClassFeatureFlag.MdmConsoleIsUsed
            || ClassFeatureFlag.StatusUiIsUsed)
            {
                InitializeStdBaseRunFileConsole();
            }
        }
        public StdConsoleManagerDef(ref object SenderPassed, ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ref SenderPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        {
            st = base.st as StdConsoleManagerDef;
            if (ClassFeatureFlag.MdmRunIsUsed
            || ClassFeatureFlag.MdmConsoleIsUsed
            || ClassFeatureFlag.StatusUiIsUsed)
            {
                InitializeStdBaseRunFileConsole();
            }
        }
        public StdConsoleManagerDef(ref object SenderPassed, ref StdConsoleManagerDef stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ref SenderPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        {

            // ToDo Fixed. This may not be useable. (re: ref Object)
            // In theory this is a redundant set here below
            // in that the ConsoleGet get called in the root class.
            // My knowledge of Upcasting to Object with respect
            // to ref fields and paramaters is insufficient here.
            // : base(ref SenderPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
            // does not work regardless of variation in syntax. It's contradictory
            // and may be related to resolving types and the type Object.
            // Possibly it must be passed as an interface type. Dunno.
            // End ToDo.
            //st = SenderPassed as StdConsoleManagerDef;
            if (stPassed != null)
            {
                st = stPassed; base.st = st; ConsoleSender = st;
            }
            //st = base.st as StdConsoleManagerDef;
            //base.st = (object)stPassed;
            if (ClassFeatureFlag.MdmRunIsUsed
            || ClassFeatureFlag.MdmConsoleIsUsed
            || ClassFeatureFlag.StatusUiIsUsed)
            {
                InitializeStdBaseRunFileConsole();
            }
        }
        #endregion
        #region Initialize Class
        public virtual void InitializeStdBaseRunFileConsole()
        {
            if (!ClassFeatureFlag.InitializeStdBaseRunFileConsoleDone)
            {
                Status = StateIs.InProgress;
                Title = "Messaging Console";
                Name = "Console";
                DgvLock = new object();
                if (st != null)
                {
                    ClassFeatureFlag.InitializeStdBaseRunFileConsoleDone = true;
                }
                if (st == null && this is StdConsoleManagerDef)
                {
                    st = this; base.st = st; ConsoleSender = this;
                }
                base.InitializeStdConsole();
                if (!st.ClassFeatureFlag.InitializeStdBaseRunFileConsoleDone)
                {
                    StdBaseRunFileConsoleInitialize();
                    st.ClassFeatureFlag.InitializeStdBaseRunFileConsoleDone = true;
                }
            }
        }
        /// <summary> 
        /// The initalize routine called by constructors.
		/// This occurs after all constructors and base class
		/// instantiation but prior to events such as "Loaded".
        /// </summary> 
        /// <remarks>
		/// </remarks> 
        public void StdBaseRunFileConsoleInitialize()
        {
            Status = StateIs.Started;
            ClassFeatureFlag.InitializeStdBaseRunFileConsoleDone = true;
            if (st == null)
            {
                st = this; base.st = st; ConsoleSender = this;
            }
            st.ClassFeatureFlag.InitializeStdBaseRunFileConsoleDone = true;
            // The Initialize calls are expensive
            // Your class features determine which
            // init functions gets called.
            //Initialize();
            #region Console
            // Program bool Control Fields are currently in here:
            ConsoleMdmInitializeFlags(ref ConsoleSender);
            // Notes:
            // Delegates;
            //TraceMdmCounterLevel1Get = TraceMdmCounterLevel1GetDefault;
            //TraceMdmCounterLevel2Get = TraceMdmCounterLevel1GetDefault;
            // Item1Add
            //if (ClassFeature.MdmSendIsUsed)
            //{
            //    MessageMdmSendToPageNewLineSet = MessageMdmSendToPageNewLineImpl;
            //    MessageMdmSendToPageNewLine = MessageMdmSendToPageNewLineSetImpl;
            //    MessageMdmSendToPage = MessageMdmSendToPageImpl;
            //}
            //
            //if (ClassFeature.MdmTraceIsUsed) { TraceMdmDo = TraceMdmDoImpl; }
            //
            if (ClassFeatureFlag.LocalMessageIsUsed
                || st.ClassFeatureFlag.LocalMessageIsUsed
                && LocalMessage == null)
            { LocalMessage = new LocalMsgDef(); }
            //
            //if (ClassFeature.StatusUiIsUsed && StatusUi == null) {
            if (ClassFeatureFlag.MdmConsoleIsUsed)
            {
                if (ConsoleSource == ConsoleSourceIs.Self
                    || ConsoleSource == ConsoleSourceIs.Parent)
                {
                    st.InitializeConsoleMdm();
                }
                //    StatusUi = new StatusUiDef(
                //        "StdConsoleManagerDef",
                //        ((ClassFeatures & ClassUses.LineIsUsed) > 0),
                //        ((ClassFeatures & ClassUses.BoxIsUsed) > 0),
                //        ((ClassFeatures & ClassUses.BoxManageIsUsed) > 0),
                //        ((ClassFeatures & ClassUses.BoxDelegateIsUsed) > 0));
                //    //
                //    PageSizeChangedDoAdjust = null;
            }
            // MessageMdmSendToPageA = null;
            #endregion
            // not in use
            //public delegate void TextBoxChangeDel(Object Sender, String s);
            //public delegate void TextBoxAddDel(Object Sender, String s);
            //public delegate void ProgressCompletionDel(Object Sender, String FieldPassed, int AmountPassed, int MaxPassed);
        }
        #endregion
        #region Dispose
        public new void Dispose()
        {
            Dispose(Status);
            // this is the base.Dispose();
            Status = StateIs.DoesNotExist;
        }
        public new void Dispose(StateIs StatusPassed)
        {
            if (StdConsoles != null) { StdConsoles.Dispose(); }
            if (StdRunControlUi != null) { StdRunControlUi.Dispose(); }
            if (StdNotifyIcon != null) { StdNotifyIcon.Dispose(); }
            if (StdNotify != null) { StdNotify.Dispose(); }
            if (StdNotifyRoot != null) { StdNotifyRoot.Dispose(); }
            base.Dispose();
            Status = StateIs.DoesNotExist;
        }
        #endregion
    }
}

