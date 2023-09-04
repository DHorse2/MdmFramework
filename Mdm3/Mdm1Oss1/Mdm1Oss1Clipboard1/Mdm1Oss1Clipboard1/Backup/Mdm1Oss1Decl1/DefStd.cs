using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Shapes;
using System.Text;
using System.Windows;
using System.Windows.Controls;


// using Mdm.Pick.Syntax;

namespace Mdm.Oss.Decl {
    #region Class Feature Control
    /// <summary>
    /// This enumeration is used to indicate what features are
    /// active within the base classes, what sub-classes will be 
    /// instantiated and guides the MVVC.
    /// This is defined in terms of Roles, feature groups (Masks),
    /// and individual features / functionality.
    /// </summary> 
    [Flags]
    public enum ClassUses : long {
        RoleAsUtility = MaskId | MaskMdmUtil | MaskThread,
        RoleAsController = MaskUi | MaskButton | MaskStautsUiAsBox | RoleAsUtility,
        RoleAsUi = MaskUi | MaskButton | MaskStautsUiAsBox | RoleAsUtility,
        RoleAsAll = All,

        All = MaskUi | MaskButton | MaskStautsUi | MaskId | MaskMdmUtil | MaskId | MaskThread,
        None = 0x0,

        MaskUi = 0xF,
        Ui = 0x1,
        // ProgressBar = 0x2,

        MaskStautsUi = MaskStautsUiComponents | MaskStautsUiControls,
        MaskStautsUiAsLine = Box | LineIsUsed | BoxManageIsUsed | BoxDelegateIsUsed,
        MaskStautsUiAsBox = Box | BoxIsUsed | BoxManageIsUsed | BoxDelegateIsUsed,
        //
        MaskStautsUiComponents = 0xF0,
        LineIsUsed = 0x10,
        BoxIsUsed = 0x20,
        BoxManageIsUsed = 0x40,
        BoxDelegateIsUsed = 0x80,
        // Display Method for status
        MaskStautsUiControls = 0xF00,
        StatusLine = 0x100,
        Box = 0x200,
        PopUp = 0x400,
        Window = 0x800,
        // 4
        MaskId = 0xF000,
        LocalMessage = 0x1000,
        ExternalId = 0x2000,
        LocalId = 0x4000,
        // 5
        MaskMdmUtil = 0xF0000,
        MdmUtilTrace = 0x10000,
        MdmUtilConsole = 0x20000,
        MdmUtilSend = 0x40000,
        MdmUtilPrint = 0x80000,
        // 6
        MaskButton = 0xF00000,
        ButtonForStart = 0x100000,
        ButtonForPause = 0x200000,
        ButtonForCancel = 0x400000,
        ButtonForOk = 0x800000,
        // 7
        MaskThread = 0xF000000,
        MdmThread = 0x1000000,
    }
    #endregion

    #region ReadMe XML
    #region Programming Standards
    /// <summary> 
    /// <para> The namespace MdmOssDecl - Declarations 
    /// and Definitions namespace(Declarations) is
    /// composed of two tiers.</para>
    /// <para>The first level (Declarations) contains
    /// low level base classes.</para>
    /// <para> The Console is the second tier (Definitions) and implements Trace, Logging, Console
    /// and messaging operations are implemented at this
    /// level</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> Re: Programming standards:</para>
    /// <para> .</para>
    /// <para> Please not the following naming conventions in use:</para>
    /// <para> ..1) All class name end in Def (i.e ClassADef ClassA = new ClassADef();)</para>
    /// <para> ..2) Delegates declarations end in Del</para>
    /// <para> ..3) Event declarations end in Event</para>
    /// <para> ..4) Argument object ends end in Arg</para>
    /// <para> ..5) Methods that implement delegates (or events) of the same name end in Impl.</para>
    /// <para> ..6) This is intended to match similar strategies in events and properties.</para>
    /// <para> ..7) Capitals are not used in enumerations.</para>
    /// <para> ..8) Though not commonly used, underscore characters are acceptable
    /// and recommended for grouping types in complex classes, very long names
    /// or compound types.</para>
    /// <para> ..9) Use of IS HAS DOES occurs frequently and is recommended along with
    /// natural language like naming strategies.</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> Delegates:</para>
    /// <para> Use of Delegates </para>
    /// <para> The naming convention for delegates is:</para>
    /// <para> ..MethodName - to execute the method somewhere</para>
    /// <para> ..MethodNameDel - the delegate that defines the signature</para>
    /// <para> ..MethodNameImpl - the actual method that executes when MethodName is used.</para>
    /// <para> This allows the method to be used in the most natural syntax as a delegate.</para>
    /// <para> The expectation being that you would normally use the delegate.</para>
    /// <para> Additionally, methods witht he Impl suffix must be delegate targets.</para>
    /// <para> Use of the Impl form obviously is a direct call without the delegate.</para>
    /// </summary>
    public enum _a_Programming_Standards_ReadMe : long { ThisIsNotUsed = 0xF }
    #endregion
    #region Namespace Mdm.Oss.Decl ReadMe
    /// <summary> 
    /// <para> Namespace:</para>
    /// <para> Mdm: MacroScope Design Matrix</para>
    /// <para> Oss: Operating Systems Support</para>
    /// <para> Decl: Declarations and Definitons</para>
    /// <para> . </para>
    /// <para> This namespace provides numerous enumerations
    /// and a hierarchically defined base class for use either
    /// individually, within the MVVC applications or as a
    /// basic framework for robust applications outside of the
    /// MVVC.</para>
    /// <para> . </para>
    /// <para> Note: Base classes will be referred to a low level classes
    /// and the term high level to mean higher level of abstraction
    /// contrary to the C view dating to early class diagrams, etc.</para>
    /// <para> . </para>
    /// <para> Each subsequent class inherits from the class below it.
    /// The class defines a layer of functionality in the hierarchy
    /// where a major level is introduced.  Delegates and
    /// object pointers (weak) are used in lower levels.  </para>
    /// <para> It  should also be noted that DLR calls and method 
    /// invokes are not currently used in the low level classes.</para>
    /// <para> . </para>
    /// <para> The class hierarchy is:</para>
    /// <para> . </para>
    /// <para> StdDef: Standard Defintions</para>
    /// <para> . </para>
    /// <para> (StdDef) Base: The primary base class (i.e. like Object)</para>
    /// <para> . </para>
    /// <para> (StdDefBase) Run: The base class with basic 
    /// run control features.</para>
    /// <para> . </para>
    /// <para> (StdDefBaseRun) File: Basic file management 
    /// enumerations and utils.</para>
    /// <para> . </para>
    /// <para> (StdDefBaseRunFile) Console: Includes console and 
    /// user interface functionality.</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> Virtualization:</para>
    /// <para> Beyond defining a framework the MVVC and file
    /// system some additional functionality is added.  
    /// Certain useful features that are absent, seldom 
    /// used, have no robust class definitions are
    /// at issue.  </para>
    /// <para> . </para>
    /// <para> A PICK syntax layer exists in the  inheritance chain.
    /// This adds language syntax and in particular 
    /// extends the String class of the assembly.  </para>
    /// <para> . </para>
    /// <para> As part of this layer, PICK style delimiters
    /// and indexers are introduced to augment the 
    /// ASCII, Json, INI and similar delimited and 
    /// key / value pair strategies for producing import, 
    /// export formats and for storing data in text files.</para>
    /// <para> . </para>
    /// <para> The first virtualization layer (PICK) is an assembly
    /// that inherits from the DefStd assembly.  It in
    /// turn is inherited by the DefBaseRun layer above it.
    /// The Pick Console layer is in turn inherited by the
    /// Run File Console.  </para>
    /// <para> . </para>
    /// <para> Note re Pick virtualization: </para>
    /// <para> At the highest levels, the
    /// Pick database class inherits from the file system
    /// class (Mfile.)</para>
    /// <para> . </para>
    /// <para> The Pick virtualization is therefore defined at three levels:</para>
    /// <para> ..Basic string functions, </para>
    /// <para> ..console and user interface IO, and then </para>
    /// <para> ..file and schema IO.</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> Note re Components: The functionaliy present 
    /// here is not currently implemented as .Net 
    /// components.  This is under advisment but
    /// currently the term component should not be
    /// construed to mean .Net Component.</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> Two part component design pattern:</para>
    /// <para> If each broad category of functionality being
    /// referred to as a component or layer is examined
    /// a two part pattern may appear.  The layer will
    /// appear once at a low level where basic functionality
    /// is implemented along with delegates and key
    /// objects.</para>
    /// <para> . </para>
    /// <para> The same category will often appear at a higher
    /// level where it is able to implement higher level
    /// classes.  Examples of higher level functionality
    /// include user interface, file system access, or 
    /// more complete access to the OSS library.</para>
    /// <para> . </para>
    /// <para> Example of components that follow this pattern
    /// include the PICK virtualization, File system,
    /// Clipboard, URL history, Exceptions handlers,
    /// and Message oriented system (Trace, Logging,
    /// Threaded messages, Errors.)</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> Note re Design Pattners:</para>
    /// <para> The system is being evolved towards following
    /// pattern where calling conventions will adhere as
    /// closely as possible to style used by MS with events.
    /// Specifically, the signature for methods should
    /// as much as possible take one of the following
    /// forms:</para>
    /// <para> ..1) MethodName(Sender, RelatedObjectARGS)</para>
    /// <para> ..2) MethodName(Sender, InteropObject)</para>
    /// <para> ..3) MethodName(Sender, InstanceObject)</para>
    /// <para> ..4) MethodName(Sender, ClassDataObject)</para>
    /// <para> . </para>
    /// <para> The above example indicates that a single object
    /// should be the norm when passing data between
    /// classes.  The object may data that is a related
    /// group to the group of methods in questions.  It
    /// may also be an aggregation of data groups.
    /// One common pattern used in dot net is an
    /// aggregate object of the form:</para>
    /// <para> ..1) Parameters</para>
    /// <para> ..2) One or more object from other sources.</para>
    /// <para> ..3) An anonymous object (UserState for example.)</para>
    /// <para> ..4) Other data or results.</para>
    /// <para> . </para>
    /// <para> There are currently a number of methods where
    /// Parameters are included in the signature but these
    /// will be switched to aggregate objects over time.</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> Mutlithreaded messaging and UI features: </para>
    /// <para> Trace messages and multithreaded messages are
    /// routed through this delegate.  Normally one class
    /// (the controller) will handle all messages for the
    /// applications.</para>
    /// <para> . </para>
    /// <para>The TraceMdm methods themselves use delegates but
    /// typically route message through ConsoleMdmPickDisplayImpl. 
    /// The Console Display method uses ThreadUiTextMessageAsync
    /// to place the message onto the correct (primary) thread.</para>
    /// <para> . </para>
    /// <para>Normally, the console delegate is set to use StatusLineChanged 
    /// which will process the messages as is appropriate.</para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> Note re Events vs Method Calls: </para>
    /// <para> Within the current MVVC implementation events are not used to issue messages.
    /// There is not specific reason why this implementation approach could not be used.
    /// Where implementation strategies are concerned this bares a relationship to the
    /// use of componenents.</para>
    /// <para> . </para>
    /// <para> Many if not all of the classes and functionality present in the
    /// Mdm OSS infrastructure could and may eventually be implemented as components.</para> 
    /// </summary> 
    public enum _a_Namespace_Mdm_Oss_Decl_ReadMe : long { ThisIsNotUsed = 0xF }
#endregion
    #region Run Action Read Me
    /// <summary>
    /// <para>Run Control usage:</para>
    /// <para>[Controller (Role) Class].RunActionState[ActionRequested, RunActionField]</para>
    /// <para> . </para>
    /// <para>RunActionState: A two dimensional array primarily consisting of:</para>
    ///	<para>	[Dimension1]: The current run action whose state is being altered.</para>
    ///	<para>	[Dimension2]: Flags or statuses indicating for each progressive state.</para>
    ///	<para>	The array element RunState contains the State.</para>
    ///	<para>	Other named elements contain fields for flow control, user interace and diagnostics.</para>
    /// <para> . </para>
    /// <para>Remark: In dimension 2 additional elements contain fields.</para>
    /// <para> . </para>
    /// <para>See: Refer to the application level class (Mapplication) for 
    /// the current location of methods that alter and act
    /// on the RunActionState.</para>
    /// <para>Default class name: XUomMavvXv</para>
    /// <para>Controller clase name: XUomCovvXv</para>
    /// <para> . </para>
    /// <para>Example:</para>
    /// <para>if (XUomCovvXv.RunActionState[RunCancel, RunState] == RunTense_Do 
    ///		|| XUomCovvXv.RunActionState[RunPause, RunState] == RunTense_Did 
    ///		|| XUomCovvXv.RunActionState[RunCancel, RunState] == RunTense_Doing) 
    /// 		{ bYES = bYES; }</para>
    /// <para> . </para>
    /// <para>RunControlAction(s) supported:</para>
    /// <para>0 RunMetric</para>
    /// <para>1 RunState</para>
    /// <para>2 RunStart</para>
    /// <para>3 RunCancel</para>
    /// <para>4 RunPause</para>
    /// <para>5 RunResume</para>
    /// <para>6 - 25 Run Fields including state and metrics.</para>
    /// <para> . </para>
    /// <para>Remark: [RunState, RunState] (or [1,1]) can be use if desired and is logically
    /// equivalent to the current run state rather than the state of the current
    /// requested run action.</para>
    /// <para> . </para>
    /// <para>RunAction Command States supported:</para>
    /// <para>0 RunTense_Off</para>
    /// <para>1 RunTense_Do</para>
    /// <para>2 RunTense_Doing</para>
    /// <para>3 RunTense_Did</para>
    /// <para>4 RunTense_Done</para>
    /// <para>5 RunTense_On = 5;</para>
    /// <para>8 RunTense_DidNot = 8;</para>
    /// <para>9 RunTense_DoNot = 9;</para>
    /// <para> . </para>
    /// <para>RunMetrics Supported:</para>
    /// <para>1 RunState</para>
    /// <para>2 RunState_Last_Update</para>
    /// <para>3 RunDoLast_Count</para>
    /// <para>4 RunDoCount</para>
    /// <para>5 RunDoSkip_Count = 5;</para>
    /// <para>6 RunDoError_Count = 6;</para>
    /// <para>7 RunDoWarning_Count = 7;</para>
    /// <para>8 RunDoRetry_Count = 8;</para>
    /// <para> . </para>
    /// <para>RunMetric_Max = 8;</para>
    /// <para>RunMetricOrStateX = RunMetric_Max + 1;</para>
    /// <para>RunMetricOrStateY = RunMetric_Max + 2;</para>
    /// <para>RunMetricOrStateZ = RunMetric_Max + 3;</para>
    /// <para>RunMetricOrState1 = RunMetric_Max + 4;</para>
    /// <para>RunMetricOrState2 = RunMetric_Max + 5;</para>
    /// <para> . </para>
    /// <para>omHPassed.RunActionState[</para>
    /// <para>Order:</para>
    /// <para>RunTense_Off -> RunTense_On</para>
    /// <para>RunTense_On -> RunTense_Off </para>
    /// <para> . </para>
    /// <para>Default progression of changes in state:</para>
    /// <para>0 RunTense_Off</para>
    /// <para>1 RunTense_Do</para>
    /// <para>2 RunTense_Doing</para>
    /// <para>3 RunTense_Did</para>
    /// <para>4 RunTense_Done</para>
    /// <para>5 RunTense_On</para>
    /// <para>6  </para>
    /// <para>7  </para>
    /// <para>8 RunTense_DidNot</para>
    /// <para>9 RunTense_DoNot</para>
    /// <para> . </para>
    /// <para>Role specific progression of changes in state:</para>
    /// <para>View/Controller sets RunTense_Do.</para>
    /// <para>ApplicationVerb sets RunTense_Doing and then RunTense_Did.</para>
    /// <para>Either sets RunTense_Done</para>
    /// <para>View/Controller sets RunTense_Off</para>
    /// <para> Setting the state to RunTense_Off occurs after all
    /// cleanup and disposal after the run.  There is no distinction
    /// made between initialization and cleanup in (Processing) Application
    /// Verb versus that don in the View or Controller.</para>
    /// </summary> 
    [Flags]
    public enum _a_RunControl_ReadMe : long { ThisIsNotUsed = 0xF }
    #endregion
    #region Messages ReadMe
    /// <summary>
    /// <para> Note that where possible, the
    /// method signature (Sender, Arguments)
    /// or (Sender, Object) will be employed.
    /// Arguments is either an aggregation of
    /// objects or a limitied part of the
    /// source object that the called method
    /// acts upon.</para>
    /// <para> . </para>
    /// <para> Please see additional notes in the namespace ReadMe for
    /// Mdm.Oss.Decl.</para>
    /// </summary>
    public enum _a_Messages_ReadMe : long { ThisIsNotUsed = 0xF }
    #endregion
    #region Enumeration Programming Standards ReadMe
    /// <summary>
    /// <para> This enumeration conforms to the loose
    /// internal standard used.  Each entry is a valid
    /// enumerated value with the following exceptions.</para>
    /// <para> 1) Entries beggining with "Mask" are flag
    /// masks intended to isolate the group.  For example,
    /// MaskCore isolates flags for the group "Core".  Masks
    /// of course should not be used as specific flags.</para>
    /// <para> 2) Groups (such as "Core") are also not used
    /// as individual flags but as nouns instead.  They are
    /// used to indentify flags as having membership or
    /// intended for some action.  Typical usage would include:</para>
    /// <para> [Group]Is[Memebership] or </para>
    /// <para> [Group]Is[State] or </para>
    /// <para> [Group]Do[Action].</para>
    /// <para> [Group]Has[Type].</para>
    /// <para> An example might be CoreDoLast.</para>
    /// <para> 3) Mask is used when these states or 
    /// actions do not apply to a specific group.</para>
    /// </summary>
    [Flags]
    public enum _a_Enums_ReadMe : long { ThisIsNotUsed = 0xF }
    #endregion
    #region File Application Object ReadMe
    /// <summary>
    /// <para> . </para>
    /// <para> A File Application Object contains two File Stream Objects:</para>
    /// <para> 1) Fmain, the Main or Primary File Stream Object. </para>
    /// <para> 1) Faux, the Auxillary File Stream Object. </para>
    /// <para> __________________________________________</para>
    /// <para> .</para>
    /// <para> Each File Stream Object is composed of:</para>
    /// <para> 2) A File Summary object. </para>
    /// <para> 3) An ID identification object. </para>
    /// <para> 4) An IO object to move data to and from the file. </para>
    /// <para> 5) One or more File Status Objects. </para>
    /// <para> 6) One or more Row and Column Management objects. </para>
    /// <para> 7) Additional objects created by extended classes. </para>
    /// <para> . </para>
    /// <para> 8) Meta data is also present for the File. 
    /// Internal Data is present in the form of run control,
    /// exceptions handling, threading, messaging, etc. </para>
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
    /// </summary> 
    public enum _a_FileApplicationObject_ReadMe : long { ThisIsNotUsed = 0xF }
    #endregion
    #endregion

    /// <summary>
    /// Base class for most of the class library.  Includes a basic 
    /// set of constants. Roughly equivalent to Object with a few
    /// added constants.
    /// </summary> 
    public class DefStd {
        #region Class Feature Control
        public long ClassFeatures;
        /// <summary>
        /// Boolean flags indicating which class features are active.
        /// </summary> 
        public struct ClassFeatureDef {
            public bool StatusUiIsUsed;

            public bool LocalMessageIsUsed;
            public bool ExternalIdIsUsed;
            public bool LocalIdIsUsed;

            public bool MdmIsUsed;
            public bool MdmTraceIsUsed;
            public bool MdmConsoleIsUsed;
            public bool MdmSendIsUsed;
            public bool MdmPrintIsUsed;

            public bool MdmButtonIsUsed;

            public bool MdmThreadIsUsed;

            public bool MdmUiIsUsed;
        }
        public ClassFeatureDef ClassFeature = new ClassFeatureDef();
        #endregion
        #region Class Object Identity
        // <Area Id = "MdmClassLevelSenders">
        public Object Sender;
        public Object SenderIsThis;
        public System.Windows.Application AppObject;
        public Page PageObject; // Temporary?
        //
        public Page PageMainObject;
        public String PageMainReturnValue;
        public bool PageMainInitialized;
        //
        public Page DbDetailPageObject;
        public String DbDetailPageReturnValue;
        public bool DbDetailPageInitialized;
        #endregion
        #region Class Standard Root Word Constants
        // ON = YES = OK = true
        // OFF = NO = BAD = false
        public bool xON = true;

        protected static bool bON = true;
        protected static bool bOFF = false;
        //
        protected static bool bYES = true;
        protected static bool bYes = true;
        protected static bool bNO = false;
        protected static bool bNo = false;
        //
        protected static bool bOK = true;
        protected static bool bBAD = false;
        //
        protected static int iON = 1;
        protected static int iOFF = 0;
        //
        protected static int iYES = 1;
        protected static int iNO = 0;
        //
        protected static int iOK = 1;
        protected static int iBAD = 0;
        #endregion
        #region CharacterConstants
        // System Standard Functions Character Constants.
        protected String Temp = "";
        // Ascii Delimiters
        protected static String Comma = ",";
        protected static String Collon = ":";
        protected static String Dot = ".";
        protected static String CrLf = ((char)13).ToString() + ((char)10).ToString();
        protected static String Cr = ((char)13).ToString();
        protected static String Lf = ((char)10).ToString();
        protected static String Eof = ((char)26).ToString();
        // protected static String Eot =  ((char)26).ToString();
        // Special Ascii Characters
        protected static String Esc = ((char)27).ToString(); // Escape
        protected static String Tld = "~";
        protected static String Asterisk = "*";
        protected static String Stick = "|";
        protected static String BackSlash = @"\";
        protected static String ForwardSlash = @"/";
        protected static String AtSymbol = @"@";
        // White Space Characters
        protected static String Ff = ((char)12).ToString(); // FormFeed
        protected static String Bs = ((char)08).ToString(); // Backspace
        protected static String Tab = ((char)09).ToString(); // Tab
        protected static String Sp = ((char)32).ToString();
        // Tab ; Horizontal Tab
        // Vtab ; Vertical Tab
        // Null
        protected static String Null = ((char)00).ToString();
        // Constants: Status Verbose 
        // </Section Summary>
        #endregion
        //
        /// <summary>
        /// Use the constructor: DefStd(long ClassFeaturesPassed)
        /// so that correct class features will be instantiated.
        /// </summary> 
        public DefStd() {
            Sender = this;
            SenderIsThis = this;
            ClassFeaturesFlagsSet((long)ClassUses.None);
        }

        /// <summary>
        /// This is the recommended construstor that indicates
        /// what features will be instantiated.
        /// </summary> 
        /// <param name="ClassFeaturesPassed">The enumeration ClassUses
        /// controls which MVVC features are active.
        /// </param> 
        public DefStd(long ClassFeaturesPassed) {
            Sender = this;
            SenderIsThis = this;
            ClassFeaturesFlagsSet(ClassFeaturesPassed);
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <param name="ClassFeaturesPassed">The enumeration ClassUses
        /// controls which MVVC features are active.
        /// </param> 
        public void ClassFeaturesFlagsSet(long ClassFeaturesPassed) {
            ClassFeatures = ClassFeaturesPassed;
            // Flags
            if ((ClassFeaturesPassed & (long)ClassUses.MaskStautsUi) > 0) { ClassFeature.StatusUiIsUsed = true; }
            if ((ClassFeaturesPassed & (long)ClassUses.LocalMessage) > 0) { ClassFeature.LocalMessageIsUsed = true; }
            if ((ClassFeaturesPassed & (long)ClassUses.ExternalId) > 0) { ClassFeature.ExternalIdIsUsed = true; }
            if ((ClassFeaturesPassed & (long)ClassUses.LocalId) > 0) { ClassFeature.LocalIdIsUsed = true; }
            if ((ClassFeaturesPassed & (long)ClassUses.MdmUtilTrace) > 0) { ClassFeature.MdmTraceIsUsed = true; }
            if ((ClassFeaturesPassed & (long)ClassUses.MdmUtilConsole) > 0) { ClassFeature.MdmConsoleIsUsed = true; }
            if ((ClassFeaturesPassed & (long)ClassUses.MdmUtilSend) > 0) { ClassFeature.MdmSendIsUsed = true; }
            if ((ClassFeaturesPassed & (long)ClassUses.MdmUtilPrint) > 0) { ClassFeature.MdmPrintIsUsed = true; }
            if ((ClassFeaturesPassed & (long)ClassUses.MaskButton) > 0) { ClassFeature.MdmButtonIsUsed = true; }
            if ((ClassFeaturesPassed & (long)ClassUses.MaskThread) > 0) { ClassFeature.MdmThreadIsUsed = true; }
            if (ClassFeature.MdmTraceIsUsed | ClassFeature.MdmConsoleIsUsed | ClassFeature.MdmSendIsUsed | ClassFeature.MdmPrintIsUsed) { ClassFeature.MdmIsUsed = true; } else { ClassFeature.MdmIsUsed = false; }
            if ((ClassFeaturesPassed & (long)ClassUses.MaskUi) > 0) { ClassFeature.MdmUiIsUsed = true; }
        }
    }

    /// <summary>
    /// Includes subclasses for introspection and temporary working
    /// fields.  Does not include any additional features.
    /// </summary> 
    public class DefStdBase : DefStd {
        public DefStdBase() {
            Sender = this;
            SenderIsThis = this;
        }

        public DefStdBase(long ClassHasPassed)
            : base(ClassHasPassed) {
            Sender = this;
            SenderIsThis = this;
        }

        #region Class Temporary Declarations
        // <Section Summary>
        // <Section Role="Declarations">
        // <Section Id = "MdmClassTemporaryDeclarations">
        // <Section Vs="MdmTempVarVs0_8_5">
        // MdmClassTemporaryDeclarations MdmTempVarVs0_8_5
        // <Area Id = "Data">
        protected String sTempDataImport;
        protected String sTempData;
        // <Area Id = "Counters">
        protected int iTempCounter;
        protected int iCharCounter;
        protected int iCharLocation;
        // <Area Id = "Arrays">
        // see Mobject Infrastructure Layer.
        // <Area Id = "Bools">
        protected bool bTemp;
        protected bool bTemp0;
        protected bool bTemp1;
        protected bool bTemp2;
        protected bool bTemp3;
        protected bool bTemp4;
        protected bool bTemp5;
        protected bool bTemp6;
        protected bool bTemp7;
        protected bool bTemp8;
        protected bool bTemp9;
        // <Area Id = "Integers">
        protected int iTemp;
        protected int iTemp0;
        protected int iTemp1;
        protected int iTemp2;
        protected int iTemp3;
        protected int iTemp4;
        protected int iTemp5;
        protected int iTemp6;
        protected int iTemp7;
        protected int iTemp8;
        protected int iTemp9;
        protected int iTempA;
        protected int iTempB;
        protected int iTempC;
        protected int iTempD;
        protected int iTempE;
        protected int iTempF;
        // <Area Id = "long">
        protected long lTemp;
        protected long lTemp0;
        protected long lTemp1;
        protected long lTemp2;
        protected long lTemp3;
        protected long lTemp4;
        protected long lTemp5;
        protected long lTemp6;
        protected long lTemp7;
        protected long lTemp8;
        protected long lTemp9;
        protected long lTempA;
        protected long lTempB;
        protected long lTempC;
        protected long lTempD;
        protected long lTempE;
        protected long lTempF;
        // <Area Id = "double">
        protected double dTemp;
        protected double dTemp0;
        protected double dTemp1;
        protected double dTemp2;
        protected double dTemp3;
        protected double dTemp4;
        protected double dTemp5;
        protected double dTemp6;
        protected double dTemp7;
        protected double dTemp8;
        protected double dTraceDisplayMessageDetail;
        protected double dTempA;
        protected double dTempB;
        protected double dTempC;
        protected double dTempD;
        protected double dTempE;
        protected double dTempF;
        // <Area Id = "Strings">
        protected String sTemp;
        protected String sTemp0;
        protected String sTemp1;
        protected String sTemp2;
        protected String sTemp3;
        protected String sTemp4;
        protected String sTemp5;
        protected String sTemp6;
        protected String sTemp7;
        protected String sTemp8;
        protected String sTemp9;
        protected String sTempA;
        protected String sTempB;
        protected String sTempC;
        protected String sTempD;
        protected String sTempE;
        protected String sTempF;

        protected String sCurrentString;

        #endregion
        #region FunctionConstants
        #region Class Scope Status
        protected internal InternalIdDef InternalId;
        protected internal ExternalIdDef ExternalId;
        protected internal LocalIdDef LocalId;
        #endregion
        #region Class Messages
        protected internal LocalMessageDef LocalMessage;
        #endregion
        #region MessageControlConstants
        // protected void TraceMdmDo(ref Sender, bIsMessage, TraceMdmDetailLine, 0, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, 
        protected static int iNoOp = 0;
        protected static bool bNoOp = false;
        protected static long lNoOp = 0;
        // Field 1 - Method / Function int Status
        // Field 2 - Error
        protected static bool ErrorDidOccur = true;
        protected static bool ErrorDidNotOccur = false;
        // Field 3 - Error Level
        protected static int ErrorIsInformation = 1;
        protected static int ErrorIsWarning = 2;
        protected static int ErrorIsRecoverable = 3;
        protected static int ErrorIsFatal = 4;
        protected static int ErrorWithoutLevel = 0;
        // Field 4 - Error Source
        protected static int ExceptionFromOsNotSupported = 1;
        protected static int ExceptionFromDatabase = 2;
        protected static int ExceptionFromOs = 3;
        protected static int ExceptionFromFile = 4;
        protected static int ExceptionFromPath = 5;
        protected static int ExceptionFromData = 6;
        protected static int ExceptionFromView = 7;
        protected static int ExceptionFromValidation = 8;
        protected static int ExceptionFromFormatting = 9;
        protected static int ExceptionFromDevice = 10;
        protected static int ExceptionFromUser = 11;
        protected static int ExceptionFromNetwork = 12;
        protected static int ExceptionFromSecurity = 13;
        protected static int ExceptionFromInput = 14;
        // Field X - Display this Message
        protected static int iNoErrorLevel = 0;
        protected static int iNoErrorSource = 0;
        protected static int iNoMethodResult = 0;
        protected static int iNoValue = 0;
        protected static bool bDoNotDisplay = false;
        protected static bool bDoDisplay = true;
        protected static bool bIsMessage = true;
        protected static bool bIsData = false;
        protected static bool bNoError = false;
        // Field 5 - User Binary Flags
        // User Response
        protected static int MessageEnterOk = 1;
        protected static int MessageEnterOkCancel = 2;
        protected static int MessageEnterAnyKey = 4;
        protected static int MessageEnterF5 = 85;
        protected static int MessageEnterResume = 75;
        protected static int MessageNoUserEntry = 0;
        // User has Console access
        protected static int MessageAllowConsole = 100;
        // User input times out with default
        protected static int MessageAllowTimeOut = 200;
        protected static int MessageUseBox = 400;
        protected static int MessageIsModal = 800;
        // Message routing to other levels
        protected static int MessageTellAdmin = 10000;
        protected static int MessageTellSuper = 20000;
        protected static int MessageTellGroup = 40000;
        protected static int MessageTellSupport = 80000;
        // Field 6 - Message
        //
        //
        #endregion
        #region ErrorControlExceptionDetail
        protected static int SqlException = 110;
        protected static int DBConcurrencyException = 111;
        protected static int ConstraintException = 113;

        protected static int DataException = 220;
        protected static int StateChangeEventHandler = 221;
        protected static int ofdSqlcdDbConnectStateChanged = 222;
        protected static int StateChangeEventArgs = 223;

        protected static int NotSupportedException = 900;

        protected static int AppDomain = 640;
        protected static int CrossAppDomainDelegate = 641;
        protected static int AppException = 642;
        protected static int AppDomainUnloadedException = 643;
        protected static int AppDomainInitializer = 644;

        protected static int SystemExceptions = 550;
        protected static int ArgumentException = 551;
        protected static int ArgumentOutOfRangeException = 552;
        protected static int ArgumentNullException = 553;
        // System Exceptions - Arithmetic
        protected static int ArithmeticException = 354;
        // System Exceptions - AccessViolation
        protected static int AccessViolationException = 455;
        // System Exceptions - DataMisaligned
        protected static int DataMisalignedException = 856;
        // System Exceptions - Configuration
        protected static int ConfigurationException = 757;
        // Path Exception
        protected static int PathException = 3160;
        // File Exception
        protected static int FileException = 1000;
        // File Data Exception
        protected static int FileDataException = 2000;
        // Math
        protected static int MathException = 4000;
        // MarkupMask
        protected static int MarkupException = 5000;
        #endregion
        #endregion
        public void ExceptionCatchNotSupportedMdm(String MsgPassed) {
            throw new NotSupportedException(MsgPassed);
        }
    }

    /// <summary>
    /// This is the base class for Run Control, Trace and 
    /// Logging, Threading and MVVC library delegates.
    /// It includes the Messaging, Console and psuedo virtualization
    /// of syntax (PickSyntax) from the multivalued platforms.
    /// Run control is the most basic level of service to the
    /// applications and must be implemented to use Logging and 
    /// consoles.
    /// </summary> 
    public class DefStdBaseRun : PickSyntax {
        /// <summary>
        /// Use the constructor: DefStdBaseRun(long ClassHasPassed)
        /// </summary> 
        public DefStdBaseRun() {
            Sender = this;
            SenderIsThis = this;
        }

        /// <summary>
        /// Recommended constructor indicating features.
        /// </summary> 
        public DefStdBaseRun(long ClassHasPassed)
            : base(ClassHasPassed) {
            Sender = this;
            SenderIsThis = this;
        }

        #region Class Naming
        public String sProcessHeading = "";
        public String sProcessSubHeading = "";
        #endregion

        #region Class Hierarchy
        #region System properties
        private int ipMdmSystemId = 99999;
        public int MdmSystemId { get { return ipMdmSystemId; } set { ipMdmSystemId = value; } }
        private String spMdmSystemName = "unknown";
        public String MdmSystemName { get { return spMdmSystemName; } set { spMdmSystemName = value; } }
        private String spMdmSystemTitle = "unknown";
        public String MdmSystemTitle { get { return spMdmSystemTitle; } set { spMdmSystemTitle = value; } }
        private int ipMdmSystemNumber = 99999;
        public int MdmSystemNumber { get { return ipMdmSystemNumber; } set { ipMdmSystemNumber = value; } }
        private int ipMdmSystemIntStatus = 99999;
        public int MdmSystemIntStatus { get { return ipMdmSystemIntStatus; } set { ipMdmSystemIntStatus = value; } }
        private String spMdmSystemStatusText = "unknown";
        public String MdmSystemStatusText { get { return spMdmSystemStatusText; } set { spMdmSystemStatusText = value; } }
        private int ipMdmSystemIntResult = 99999;
        public int MdmSystemIntResult { get { return ipMdmSystemIntResult; } set { ipMdmSystemIntResult = value; } }
        private bool bpMdmSystemResult = false;
        public bool MdmSystemResult { get { return bpMdmSystemResult; } set { bpMdmSystemResult = value; } }
        private bool bpMdmServerResult = false;
        public bool MdmServerResult { get { return bpMdmServerResult; } set { bpMdmServerResult = value; } }
        #endregion
        #region Process properties
        private int ipMdmProcessId = 99999;
        public int MdmProcessId { get { return ipMdmProcessId; } set { ipMdmProcessId = value; } }
        private String spMdmProcessName = "unknown";
        public String MdmProcessName { get { return spMdmProcessName; } set { spMdmProcessName = value; } }
        private String spMdmProcessTitle = "unknown";
        public String MdmProcessTitle { get { return spMdmProcessTitle; } set { spMdmProcessTitle = value; } }
        private int ipMdmProcessNumber = 99999;
        public int MdmProcessNumber { get { return ipMdmProcessNumber; } set { ipMdmProcessNumber = value; } }
        private int ipMdmProcessIntResult = 99999;
        public int MdmProcessIntResult { get { return ipMdmProcessIntResult; } set { ipMdmProcessIntResult = value; } }
        private bool bpMdmProcessBoolResult = false;
        public bool MdmProcessBoolResult { get { return bpMdmProcessBoolResult; } set { bpMdmProcessBoolResult = value; } }
        #endregion
        #region Class properties
        private int ipMdmClassId = 99999;
        public int MdmClassId { get { return ipMdmClassId; } set { ipMdmClassId = value; } }
        private String spMdmClassName = "unknown";
        public String MdmClassName { get { return spMdmClassName; } set { spMdmClassName = value; } }
        private String spMdmClassTitle = "unknown";
        public String MdmClassTitle { get { return spMdmClassTitle; } set { spMdmClassTitle = value; } }
        private int ipMdmClassNumber = 99999;
        public int MdmClassNumber { get { return ipMdmClassNumber; } set { ipMdmClassNumber = value; } }
        private int ipMdmClassIntStatus = 99999;
        public int MdmClassIntStatus { get { return ipMdmClassIntStatus; } set { ipMdmClassIntStatus = value; } }
        private String spMdmClassStatusText = "unknown";
        public String MdmClassStatusText { get { return spMdmClassStatusText; } set { spMdmClassStatusText = value; } }
        private long ipMdmClassResult = 99999;
        public long MdmClassResult { get { return ipMdmClassResult; } set { ipMdmClassResult = value; } }
        private bool bpMdmClassBoolResult = false;
        public bool MdmClassBoolResult { get { return bpMdmClassBoolResult; } set { bpMdmClassBoolResult = value; } }
        #endregion
        #region Method properties
        private int ipMdmMethodId = 99999;
        public int MdmMethodId { get { return ipMdmMethodId; } set { ipMdmMethodId = value; } }
        private String spMdmMethodName = "unknown";
        public String MdmMethodName { get { return spMdmMethodName; } set { spMdmMethodName = value; } }
        private String spMdmMethodTitle = "unknown";
        public String MdmMethodTitle { get { return spMdmMethodTitle; } set { spMdmMethodTitle = value; } }
        private int ipMdmMethodNumber = 99999;
        public int MdmMethodNumber { get { return ipMdmMethodNumber; } set { ipMdmMethodNumber = value; } }
        private int ipMdmMethodStatus = 99999;
        public int MdmMethodStatus { get { return ipMdmMethodStatus; } set { ipMdmMethodStatus = value; } }
        private String spMdmMethodStatusText = "unknown";
        public String MdmMethodStatusText { get { return spMdmMethodStatusText; } set { spMdmMethodStatusText = value; } }
        private long ipMdmMethodResult = 99999;
        public long MdmMethodResult { get { return ipMdmMethodResult; } set { ipMdmMethodResult = value; } }
        private bool bpMdmMethodBoolResult = false;
        public bool MdmMethodBoolResult { get { return bpMdmMethodBoolResult; } set { bpMdmMethodBoolResult = value; } }
        #endregion
        #region Attribute properties
        private int ipMdmAttributeId = 99999;
        public int MdmAttributeId { get { return ipMdmAttributeId; } set { ipMdmAttributeId = value; } }
        private String spMdmAttributeName = "unknown";
        public String MdmAttributeName { get { return spMdmAttributeName; } set { spMdmAttributeName = value; } }
        private String spMdmAttributeTitle = "unknown";
        public String MdmAttributeTitle { get { return spMdmAttributeTitle; } set { spMdmAttributeTitle = value; } }
        private int ipMdmAttributeNumber = 99999;
        public int MdmAttributeNumber { get { return ipMdmAttributeNumber; } set { ipMdmAttributeNumber = value; } }
        private int ipMdmAttributeStatus = 99999;
        public int MdmAttributeStatus { get { return ipMdmAttributeStatus; } set { ipMdmAttributeStatus = value; } }
        private String spMdmAttributeStatusText = "unknown";
        public String MdmAttributeStatusText { get { return spMdmAttributeStatusText; } set { spMdmAttributeStatusText = value; } }
        private int ipMdmAttributeIntResult = 99999;
        public int MdmAttributeIntResult { get { return ipMdmAttributeIntResult; } set { ipMdmAttributeIntResult = value; } }
        private bool bpMdmAttributeBoolResult = false;
        public bool MdmAttributeBoolResult { get { return bpMdmAttributeBoolResult; } set { bpMdmAttributeBoolResult = value; } }
        #endregion
        #region Parameter properties
        private int ipMdmParameterId = 99999;
        public int MdmAuthorParameterId { get { return ipMdmParameterId; } set { ipMdmParameterId = value; } }
        private String spMdmParameterName = "unknown";
        public String MdmParameterName { get { return spMdmParameterName; } set { spMdmParameterName = value; } }
        private int ipMdmParameterNumber = 99999;
        public int MdmParameterNumber { get { return ipMdmParameterNumber; } set { ipMdmParameterNumber = value; } }
        private String spMdmParameterTitle = "unknown";
        public String MdmParameterTitle { get { return spMdmParameterTitle; } set { spMdmParameterTitle = value; } }
        private int ipMdmParameterStatus = 99999;
        public int MdmParameterStatus { get { return ipMdmParameterStatus; } set { ipMdmParameterStatus = value; } }
        private String spMdmParameterStatusText = "unknown";
        public String MdmParameterStatusText { get { return spMdmParameterStatusText; } set { spMdmParameterStatusText = value; } }
        private int ipMdmParameterIntResult = 99999;
        public int MdmParameterIntResult { get { return ipMdmParameterIntResult; } set { ipMdmParameterIntResult = value; } }
        private bool bpMdmParameterBoolResult = false;
        public bool MdmParameterBoolResult { get { return bpMdmParameterBoolResult; } set { bpMdmParameterBoolResult = value; } }
        #endregion
        #region Property properties
        private int ipMdmPropertyId = 99999;
        public int MdmAuthorPropertyId { get { return ipMdmPropertyId; } set { ipMdmPropertyId = value; } }
        private String spMdmPropertyName = "unknown";
        public String MdmPropertyName { get { return spMdmPropertyName; } set { spMdmPropertyName = value; } }
        private int ipMdmPropertyNumber = 99999;
        public int MdmPropertyNumber { get { return ipMdmPropertyNumber; } set { ipMdmPropertyNumber = value; } }
        private String spMdmPropertyTitle = "unknown";
        public String MdmPropertyTitle { get { return spMdmPropertyTitle; } set { spMdmPropertyTitle = value; } }
        private int ipMdmPropertyStatus = 99999;
        public int MdmPropertyStatus { get { return ipMdmPropertyStatus; } set { ipMdmPropertyStatus = value; } }
        private String spMdmPropertyStatusText = "unknown";
        public String MdmPropertyStatusText { get { return spMdmPropertyStatusText; } set { spMdmPropertyStatusText = value; } }
        private int ipMdmPropertyIntResult = 99999;
        public int MdmPropertyIntResult { get { return ipMdmPropertyIntResult; } set { ipMdmPropertyIntResult = value; } }
        private bool bpMdmPropertyBoolResult = false;
        public bool MdmPropertyBoolResult { get { return bpMdmPropertyBoolResult; } set { bpMdmPropertyBoolResult = value; } }
        #endregion
        #endregion

        #region RunControlManagement properties
        #region RunControl properties
        protected int ipMdmRunId = 99999;
        public int MdmRunId { get { return ipMdmRunId; } set { ipMdmRunId = value; } }
        protected String spMdmRunName = "unknown";
        public String MdmRunName { get { return spMdmRunName; } set { spMdmRunName = value; } }
        protected String spMdmRunTitle = "unknown";
        public String MdmRunTitle { get { return spMdmRunTitle; } set { spMdmRunTitle = value; } }
        protected int ipMdmRunNumber = 99999;
        public int MdmRunNumber { get { return ipMdmRunNumber; } set { ipMdmRunNumber = value; } }
        protected int ipMdmRunStatus = 99999;
        public int MdmRunStatus { get { return ipMdmRunStatus; } set { ipMdmRunStatus = value; } }
        protected String spMdmRunStatusText = "unknown";
        public String MdmRunStatusText { get { return spMdmRunStatusText; } set { spMdmRunStatusText = value; } }
        protected int ipMdmRunIntResult = 99999;
        public int MdmRunIntResult { get { return ipMdmRunIntResult; } set { ipMdmRunIntResult = value; } }
        protected bool bpMdmRunBoolResult = false;
        public bool MdmRunBoolResult { get { return bpMdmRunBoolResult; } set { bpMdmRunBoolResult = value; } }
        #endregion
        #region AutoRun properties
        protected int ipMdmAutoRunId = 99999;
        public int MdmAutoRunId { get { return ipMdmAutoRunId; } set { ipMdmAutoRunId = value; } }
        protected String spMdmAutoRunName = "unknown";
        public String MdmAutoRunName { get { return spMdmAutoRunName; } set { spMdmAutoRunName = value; } }
        protected String spMdmAutoRunTitle = "unknown";
        public String MdmAutoRunTitle { get { return spMdmAutoRunTitle; } set { spMdmAutoRunTitle = value; } }
        protected int ipMdmAutoRunNumber = 99999;
        public int MdmAutoRunNumber { get { return ipMdmAutoRunNumber; } set { ipMdmAutoRunNumber = value; } }
        protected int ipMdmAutoRunStatus = 99999;
        public int MdmAutoRunStatus { get { return ipMdmAutoRunStatus; } set { ipMdmAutoRunStatus = value; } }
        protected String spMdmAutoRunStatusText = "unknown";
        public String MdmAutoRunStatusText { get { return spMdmAutoRunStatusText; } set { spMdmAutoRunStatusText = value; } }
        protected int ipMdmAutoRunIntResult = 99999;
        public int MdmAutoRunIntResult { get { return ipMdmAutoRunIntResult; } set { ipMdmAutoRunIntResult = value; } }
        protected bool bpMdmAutoRunBoolResult = false;
        public bool MdmAutoRunBoolResult { get { return bpMdmAutoRunBoolResult; } set { bpMdmAutoRunBoolResult = value; } }
        #endregion
        #region Input properties
        protected int ipMdmInputId = 99999;
        public int MdmInputId { get { return ipMdmInputId; } set { ipMdmInputId = value; } }
        protected String spMdmInputName = "unknown";
        public String MdmInputName { get { return spMdmInputName; } set { spMdmInputName = value; } }
        protected String spMdmInputTitle = "unknown";
        public String MdmInputTitle { get { return spMdmInputTitle; } set { spMdmInputTitle = value; } }
        protected int ipMdmInputNumber = 99999;
        public int MdmInputNumber { get { return ipMdmInputNumber; } set { ipMdmInputNumber = value; } }
        protected int ipMdmInputStatus = 99999;
        public int MdmInputStatus { get { return ipMdmInputStatus; } set { ipMdmInputStatus = value; } }
        protected String spMdmInputStatusText = "unknown";
        public String MdmInputStatusText { get { return spMdmInputStatusText; } set { spMdmInputStatusText = value; } }
        protected int ipMdmInputIntResult = 99999;
        public int MdmInputIntResult { get { return ipMdmInputIntResult; } set { ipMdmInputIntResult = value; } }
        protected bool bpMdmInputBoolResult = false;
        public bool MdmInputBoolResult { get { return bpMdmInputBoolResult; } set { bpMdmInputBoolResult = value; } }
        #endregion
        #region Output properties
        protected int ipMdmOutputId = 99999;
        public int MdmOutputId { get { return ipMdmOutputId; } set { ipMdmOutputId = value; } }
        protected String spMdmOutputName = "unknown";
        public String MdmOutputName { get { return spMdmOutputName; } set { spMdmOutputName = value; } }
        protected String spMdmOutputTitle = "unknown";
        public String MdmOutputTitle { get { return spMdmOutputTitle; } set { spMdmOutputTitle = value; } }
        protected int ipMdmOutputNumber = 99999;
        public int MdmOutputNumber { get { return ipMdmOutputNumber; } set { ipMdmOutputNumber = value; } }
        protected int ipMdmOutputStatus = 99999;
        public int MdmOutputStatus { get { return ipMdmOutputStatus; } set { ipMdmOutputStatus = value; } }
        protected String spMdmOutputStatusText = "unknown";
        public String MdmOutputStatusText { get { return spMdmOutputStatusText; } set { spMdmOutputStatusText = value; } }
        protected int ipMdmOutputIntResult = 99999;
        public int MdmOutputIntResult { get { return ipMdmOutputIntResult; } set { ipMdmOutputIntResult = value; } }
        protected bool bpMdmOutputBoolResult = false;
        public bool MdmOutputBoolResult { get { return bpMdmOutputBoolResult; } set { bpMdmOutputBoolResult = value; } }
        #endregion
        #endregion

        #region ConsoleMdmDeclarations
        public bool DoLogActivity = false;
        public bool DoLogActivityDefault = true;
        #region ConsoleTraceMdm_Declarations
        // Tracing Detail
        public int iTraceResult = 99999;
        public bool TraceDebugOn = bOFF;
        public bool TraceOn = bOFF;
        public bool TraceFirst = bYES;
        // Trace Attributes Processed
        public int iTraceIterationCount = 0;
        public int iTraceIterationCheckPointCount = 0;
        public int iTraceIterationCountTotal = 0;
        public int iTraceIterationCurrentDetail = 0;
        //
        public bool TraceHeadings = bYES;
        public bool TraceData = bYES;
        public String sTraceDataPointers = "";
        public String sTraceErrorMessage = "";
        // Trace Iteration Control
        // threshold to stop displaying full detail on iteration count
        // stop detail display after number of iterations
        public bool TraceIteration = bOFF;
        public bool TraceIterationOnNow = bOFF;
        public bool TraceIterationInitialState = bOFF;
        public bool TraceIterationRepeat = bOFF;
        // threshold for pause on iteration count
        // this is a check point to interact with user
        public bool TraceIterationCheckPoint = bOFF;
        public int iTraceIterationCheckPoint = 500;
        // Use 0 to trace initialization, 1 to start at details
        public int iTraceIterationThreshold = 6180;
        public int iTraceIterationOnForCount = 100;
        public bool TraceIterationOnForWarningGiven = bNO;
        public int iTraceIterationOnAgainCount = 200;
        // Trace Lines Displayed Control
        public int iTraceDisplayCount = 0;
        public int iTraceDisplayCheckPointCount = 0;
        public int iTraceDisplayCountTotal = 0;
        //
        public int iTraceCharacterCount = 0;
        //
        public int iTracePercentCompleted = 0;
        //
        public bool TraceDisplay = bOFF;
        public bool TraceDisplayOnNow = bOFF;
        public bool TraceDisplayInitialState = bOFF;
        public bool TraceDisplayRepeat = bOFF;
        // threshold for pause on number of messages
        // that could have been displayed
        public bool TraceDisplayCheckPoint = bOFF;
        public int iTraceDisplayCheckPoint = 5000;
        //
        public int iTraceDisplayThreshold = 200;
        public int iTraceDisplayOnForCount = 5;
        public int iTraceDisplayOnAgainCount = 200;
        //
        // Bug in fields or areas Control
        public int iTraceBugCount = 0;
        public int iTraceBugCheckPointCount = 0;
        public int iTraceBugCountTotal = 0;
        //
        public bool TraceBug = bOFF;
        public bool TraceBugOnNow = bOFF;
        public bool TraceBugInitialState = bOFF;
        public bool TraceBugRepeat = bOFF;
        // After Threshold line display details
        // for OnForCount lines then return to
        // summary mode.  After another OnAgainCount
        // lines repeate display OnForCount.
        public bool TraceBugCheckPoint = bOFF;
        public int iTraceBugCheckPoint = 10;
        //
        public int iTraceBugThreshold = 2000;
        public int iTraceBugOnForCount = 1;
        public int iTraceBugOnAgainCount = 200;
        //
        public bool TraceDisplayMessageDetail = false;
        public String sTraceDisplayMessageDetail = "";
        public String sTraceMessage = "";
        public String sTraceMessageTarget = "";
        public String sTraceMessagePrefix = "";
        public String sTraceMessageSuffix = "";
        public String sTraceMessageFormated = "";
        public String sTraceMessageToPrint = "";
        public String sTraceTemp = "";
        public String sTraceTemp1 = "";

        public String sLocalUserEntry;
        public long iLocalUserEntry;

        public String sTraceMessageBlockString = "";
        public String sTraceMessageBlock = "";

        public int iTraceByteCountTotal = 0;
        public int iTraceByteCount = 0;
        public int iTraceShiftIndexByCount = 100;
        #endregion
        #region ConsoleMessageTarget
        public String MessageStatusTargetText = "";
        public int MessageStatusSubTarget = 0;
        public double MessageStatusSubTargetDouble = 0;
        //
        public int ProcessStatusTarget = 0;
        public int ProcessStatusSubTarget = 0;
        public double ProcessStatusTargetDouble = 0;
        //
        public int ProcessStatusTargetState = 0;
        #endregion
        #region ConsoleProgressBar
        public double ProgressBarMdm1Property = 0;
        public double MessageProperty2 = 0;
        #endregion
        #region ConsoleControlFlags
        // Std_I0_Console
        public bool ConsoleApplication = bYES;
        public bool ConsoleOn = bOFF;
        public bool ConsoleToDisc = bOFF;
        // 
        public bool ConsoleTextOn = bON;
        public bool ConsoleText0On = bOFF;
        public bool ConsoleText1On = bON;
        public bool ConsoleText2On = bON;
        public bool ConsoleText3On = bOFF;
        public bool ConsoleText4On = bOFF;
        public bool ConsoleText5On = bOFF;
        //
        public int ConsoleVerbosity = 9;
        // <Area Id = "ConsolePickConsole">
        public bool ConsolePickConsoleOn = bOFF;
        public bool ConsolePickConsoleBasicOn = bOFF;
        public bool ConsolePickConsoleToDisc = bOFF;
        // Display
        #endregion
        #region ConsoleOutput
        // Display
        public String ConsoleOutput;
        public String ConsoleOutputLog = "";
        public String ConsolePickConsoleOutput;
        public String ConsolePickConsoleOutputLog = "";
        // public ConsolePickConsoleTextBlock;
        public String ConsolePickConsoleTextBlock; // text block
        public int ConsolePickConsoleTextPositionX;
        public int ConsolePickConsoleTextPositionY;
        public int ConsolePickConsoleTextPositionZ;
        public Point ConsolePickConsoleTextPositionOrigin;
        //
        public int iMessageLevelLast = 0;
        //
        #endregion
        #region ConsoleTextMessageOutput
        public String sMessageText = "";
        public String sMessageText0 = "";
        public String MessageTextOutConsole = "";
        public String MessageTextOutStatusLine = "";
        public String MessageTextOutProgress = "";
        public String MessageTextOutError = "";
        public String MessageTextOutRunAction = "";
        //
        public String MessageStatusAction = "";
        public String ProcessStatusAction = "";
        public bool MessageStatusError = false;
        //
        #endregion
        #region TextConsole
        // <Area Id = "TextConsole>
        public String ConsolePickTextConsole;
        public String CommandLineRequest;
        public int CommandLineRequestResult = 0;
        public String TextConsole;
        #endregion
        #region Command properties
        protected int ipMdmCommandId = 99999;
        public int MdmCommandId { get { return ipMdmCommandId; } set { ipMdmCommandId = value; } }
        protected String spMdmCommandName = "unknown";
        public String MdmCommandName { get { return spMdmCommandName; } set { spMdmCommandName = value; } }
        protected String spMdmCommandTitle = "unknown";
        public String MdmCommandTitle { get { return spMdmCommandTitle; } set { spMdmCommandTitle = value; } }
        protected int ipMdmCommandNumber = 99999;
        public int MdmCommandNumber { get { return ipMdmCommandNumber; } set { ipMdmCommandNumber = value; } }
        protected int ipMdmCommandStatus = 99999;
        public int MdmCommandStatus { get { return ipMdmCommandStatus; } set { ipMdmCommandStatus = value; } }
        protected String spMdmCommandStatusText = "unknown";
        public String MdmCommandStatusText { get { return spMdmCommandStatusText; } set { spMdmCommandStatusText = value; } }
        protected int ipMdmCommandIntResult = 99999;
        public int MdmCommandIntResult { get { return ipMdmCommandIntResult; } set { ipMdmCommandIntResult = value; } }
        protected bool bpMdmCommandBoolResult = false;
        public bool MdmCommandBoolResult { get { return bpMdmCommandBoolResult; } set { bpMdmCommandBoolResult = value; } }
        #endregion
        #region Console properties
        protected int ipConsoleMdmId = 99999;
        public int ConsoleMdmId { get { return ipConsoleMdmId; } set { ipConsoleMdmId = value; } }
        protected String spConsoleMdmName = "unknown";
        public String ConsoleMdmName { get { return spConsoleMdmName; } set { spConsoleMdmName = value; } }
        protected String spConsoleMdmTitle = "unknown";
        public String ConsoleMdmTitle { get { return spConsoleMdmTitle; } set { spConsoleMdmTitle = value; } }
        protected int ipConsoleMdmNumber = 99999;
        public int ConsoleMdmNumber {
            get { return ipConsoleMdmNumber; }
            set {
                ipConsoleMdmNumber = value;
            }
        }
        protected int ipConsoleMdmStatus = 99999;
        public int ConsoleMdmStatus { get { return ipConsoleMdmStatus; } set { ipConsoleMdmStatus = value; } }
        protected String spConsoleMdmStatusText = "unknown";
        public String ConsoleMdmStatusText { get { return spConsoleMdmStatusText; } set { spConsoleMdmStatusText = value; } }
        protected int ipConsoleMdmIntResult = 99999;
        public int ConsoleMdmIntResult { get { return ipConsoleMdmIntResult; } set { ipConsoleMdmIntResult = value; } }
        protected bool bpConsoleMdmBoolResult = false;
        public bool ConsoleMdmBoolResult { get { return bpConsoleMdmBoolResult; } set { bpConsoleMdmBoolResult = value; } }
        #endregion
        #endregion
        #region Thread 2 Declarations
        #region  Thread 2 Controllers
        /// <summary>
        /// The delegate to handle messages to the MVVC controller class.
        /// The is the delegate to be uses when messages 
        /// do not cross thread boundaries.
        /// </summary> 
        /// <param name="PassedMessage">The message to the controller.</param> 
        /// <returns>
        /// Standard result state.
        /// </returns>
        public delegate long ThreadControllerDel(String PassedMessage);
        /// <summary>
        /// The delegate to handle messages to the MVVC controller class.
        /// The is the delegate to be uses when messages 
        /// cross thread boundaries and in particular are bound for the user interface.
        /// </summary> 
        /// <param name="PassedMessage">The message to the controller.</param> 
        /// <returns>
        /// Standard result state.
        /// </returns>
        public delegate long ThreadControllerAsyncDel(String PassedMessage);
        public String sAnyExceptionMessage = "";
        #endregion
        #region Thread 2 Message Send
        // Message results (Async)
        public long ThreadUiTextMessageDoResult;
        public long ThreadUiTextMessageDoSetResult;
        public AsyncResult ThreadUiTextMessageAsyncResult;
        public String ThreadUiTextMessageResults;
        // Message Send
        /// <summary>
        /// Delegate used to pass user interface message across thread boundaries
        /// </summary> 
        /// <param name="Sender">Object sending the message.</param> 
        /// <param name="PassedMessage">The message to the user interface.</param> 
        /// <returns>
        /// </returns>
        /// <remarks>
        /// </remarks> 
        public delegate void ThreadUiTextMessageAsyncDel(ref Object Sender, String PassedMessage);
        /// <summary>
        /// Delegate used to handle user interface messages that do not cross threads.
        /// </summary> 
        /// <param name="Sender">Object sending the message.</param> 
        /// <param name="PassedMessage">The message to the user interface.</param> 
        /// <returns>
        /// </returns>
        /// <remarks>
        /// </remarks> 
        public delegate void ThreadUiTextMessageDel(ref Object Sender, String PassedMessage);
        //
        // Delegates performing task
        public ThreadUiTextMessageAsyncDel ThreadUiTextMessageAsync;
        public ThreadUiTextMessageDel ThreadUiTextMessage;
        //
        public ThreadUiTextMessageAsyncDel ThreadUiTextMessageAsyncInvoke;
        public ThreadUiTextMessageDel ThreadUiTextMessageInvoke;
        //
        /// <summary>
        /// <para> Message Events</para>
        /// <para> Where events are fired to issues messages use this delegate.</para>
        /// </summary> 
        /// <param name="Sender">Object sending the message.</param> 
        /// <param name="PassedMessage">The message to the user interface.</param> 
        /// <returns>
        /// </returns>
        /// <remarks>
        /// <para> Within the current MVVC implementation events are not used to issue messages.
        /// There is not specific reason why this implementation approach could not be used.
        /// Where implementation strategies are concerned this bares a relationship to the
        /// use of componenents.</para>
        /// <para> . </para>
        /// <para> Many if not all of the classes and functionality present in the
        /// Mdm OSS infrastructure could and may eventually be implemented as components.</para> 
        /// </remarks> 
        public delegate void ThreadUiTextMessageEventHandler(ref Object Sender, String PassedMessage);
        // (ref Object Sender, MessageMdmSendToPageArgs e)
        public event ThreadUiTextMessageEventHandler ThreadUiTextMessageEvent;
        //
        /// <summary>
        /// Delegate that dispatches progress and UI messages.  This is normally a standard
        /// dispatcher method invoke but a substitution can be made for Tracing, Logging, and
        /// Debugging modes (for example,) at the implementor's discretion.
        /// </summary> 
        /// <param name=""></param> 
        /// <returns>
        /// </returns>
        /// <remarks>
        /// </remarks> 
        public delegate void ThreadUiTextMessageDispatcherDel(System.Windows.Threading.DispatcherPriority oDisp, ref Object oTemp, ref Object results);
        #endregion
        #region Thread 2 Progress Update
        // Results
        public long ThreadUiProgressResult;
        public long ThreadUiProgressDoResult;
        public long ThreadUiProgressDoSetResult;
        public AsyncResult ThreadUiProgressAsyncResult;
        public String ThreadUiProgressResults;
        // Progress Update Send
        /// <summary>
        /// Multithreaded delegate that uses a progress bar update object.
        /// </summary> 
        /// <param name="Sender">Object sending the progress message.</param> 
        /// <param name="ProgressChangedEventArgs ePcea">Argument object containing change data.</param> 
        public delegate void ThreadUiProgressAsyncDel(ref Object Sender, ProgressChangedEventArgs ePcea);
        /// <summary>
        /// Single threaded delegate that uses a progress bar update object.
        /// </summary> 
        /// <param name="Sender">Object sending the progress message.</param> 
        /// <param name="ProgressChangedEventArgs ePcea">Argument object containing change data.</param> 
        public delegate void ThreadUiProgressDel(ref Object Sender, ProgressChangedEventArgs PassedChangedEventArgs);
        //
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        //
        // Delegates and data objects (Args, etc.):
        // Method to handle message.
        public ThreadUiProgressAsyncDel ThreadUiProgressAsync;
        public ThreadUiProgressDel ThreadUiProgress;
        // Method that performs the Invoke for the message.
        public ThreadUiProgressAsyncDel ThreadUiProgressAsyncInvoke;
        public ThreadUiProgressDel ThreadUiProgressInvoke;
        // Progess data passed.
        public ProgressChangedEventArgs ThreadUiProgressChangedArgs;
        public RunWorkerCompletedEventArgs ThreadUiRunCompletedArgs;
        //
        // Property Change argument not curently in use
        public RoutedPropertyChangedEventArgs<double> RunProgressRoutePropChangedArgs;
        //
        // Progress Events
        public event ThreadUiProgressEventHandler ProgressSendToPage;
        /// <summary>
        /// Event based delegate that uses a progress bar update object.
        /// </summary> 
        /// <param name="Sender">Object sending the progress event.</param> 
        /// <param name="ProgressChangedEventArgs ePcea">Argument object containing change data.</param> 
        /// <remarks>
        /// Not currently implemented, using method calls.
        /// </remarks> 
        public delegate void ThreadUiProgressEventHandler(ref Object Sender, ProgressChangedEventArgs ePcea);
        #endregion
        #endregion
        #region Message TextBoxes and Progress
        //
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        //
        // not in use
        /// <summary>
        /// <para> These delegates are not used in the current implementation
        /// but are available to the implementor for custom use.</para> 
        /// <para> See StatusUiDef and DefStdBaseRunFileConsole for active delegates</para>
        /// </summary> 
        public delegate void TextBoxChangeDel(ref Object Sender, String s);
        public TextBoxChangeDel TextBoxChange;
        /// <summary>
        /// <para> These delegates are not used in the current implementation
        /// but are available to the implementor for custom use.</para> 
        /// <para> See StatusUiDef and DefStdBaseRunFileConsole for active delegates</para>
        /// </summary> 
        public virtual void TextBoxChangeImpl(ref Object Sender, String s) { }
        /// <summary>
        /// <para> These delegates are not used in the current implementation
        /// but are available to the implementor for custom use.</para> 
        /// <para> See StatusUiDef and DefStdBaseRunFileConsole for active delegates</para>
        /// </summary> 
        public delegate void TextBoxAddDel(ref Object Sender, String s);
        public TextBoxAddDel TextBoxAdd;
        /// <summary>
        /// <para> These delegates are not used in the current implementation
        /// but are available to the implementor for custom use.</para> 
        /// <para> See StatusUiDef and DefStdBaseRunFileConsole for active delegates</para>
        /// </summary> 
        public virtual void TextBoxAddImpl(ref Object Sender, String s) { }
        /// <summary>
        /// <para> These delegates are not used in the current implementation
        /// but are available to the implementor for custom use.</para> 
        /// <para> See StatusUiDef and DefStdBaseRunFileConsole for active delegates</para>
        /// </summary> 
        public delegate void ProgressCompletionDel(ref Object Sender, String sField, int iAmount, int iMax);
        public ProgressCompletionDel ProgressCompletion;
        /// <summary>
        /// <para> These delegates are not used in the current implementation
        /// but are available to the implementor for custom use.</para> 
        /// </summary> 
        /// <para> See StatusUiDef and DefStdBaseRunFileConsole for active delegates</para>
        public virtual void ProgressCompletionImpl(ref Object Sender, String s) { }
        // Controls delcared in Def console 
        // where basic Form, Page and UI constructs are located.
        //
        #endregion
        #region Trace and Messaging Fields
        // Tracing
        // bracketed information (like line numbers or current byte or row #)
        // displayed in trace console output
        public int TraceMdmCounterLevel1 = 0;
        public int TraceMdmCounterLevel2 = 0;
        /// <summary>
        /// In the print margin additional information can
        /// be displayed.  These values are intended for
        /// utillity applications proving detailed location
        /// information for diagnostics.
        /// </summary> 
        /// <returns>
        /// The current value of counter for tracing.
        /// </returns>
        public delegate int TraceMdmCounterGetDel();
        public TraceMdmCounterGetDel TraceMdmCounterLevel1Get;
        public TraceMdmCounterGetDel TraceMdmCounterLevel2Get;
        //
        /// <summary>
        /// <para> Trace messages and multithreaded messages are
        /// routed through this delegate.  Normally one class
        /// (the controller) will handle all messages for the
        /// applications.</para>
        /// <para> . </para>
        /// <para>The TraceMdm methods themselves use delegates but
        /// typically route message through ConsoleMdmPickDisplayImpl. 
        /// The Console Display method uses ThreadUiTextMessageAsync
        /// to place the message onto the correct (primary) thread.</para>
        /// <para> . </para>
        /// <para>Normally, the console delegate is set to use StatusLineChanged 
        /// which will process the messages as is appropriate.</para>
        /// </summary> 
        /// <param name="MessageDetailsDef MessageDetails">A specific or default message details object.</param> 
        public delegate void TraceMdmDoDel(MessageDetailsDef MessageDetails);
        public TraceMdmDoDel TraceMdmDo;
        #endregion
        #region Call Stack Tracing - Not Implemented
        /// 
        /// using System.Diagnostics;
        /// get call stack
        /// StackTrace stackTrace = new StackTrace();
        /// get calling method name
        /// Console.WriteLine(stackTrace.GetFrame(1).GetMethod().Name);
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxx Run Action Declarations xxxxxxxxxxxxxxxxxxxxxx
        #region Run Action Handling
        #region Run Action Delegates - Started, Cancel, Pause, Resume
        /// <summary>
        /// Delegate that Starts processing.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public delegate void RunStartAsyncDel();
        public RunStartAsyncDel RunStartAsync;
        /// <summary>
        /// Delegate that Cancels processing.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public delegate void RunCancelAsyncDel();
        public RunCancelAsyncDel RunCancelAsync;
        /// <summary>
        /// Delegate that Pauses processing.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public delegate void RunPauseAsyncDel();
        public RunPauseAsyncDel RunPauseAsync;
        #endregion
        #region Run Action Constants
        #region Run Action and Metrics Fields - Array and Friendly names
        // The Verb, Temporal or State component
        public static int RunTense_Max = 9;
        public int RunTense = 0;
        public const int RunTense_Off = 0;
        public const int RunTense_Do = 1;
        public const int RunTense_Doing = 2;
        public const int RunTense_Did = 3;
        public const int RunTense_Done = 4;
        public const int RunTense_On = 5;
        public const int RunTense_DidNot = 8;
        public const int RunTense_DoNot = 9;
        // Metrics and State
        public static int RunMetric_Max = 8;
        public int RunMetric = 0;
        public const int RunState = 1;
        public const int RunState_Last_Update = 2;
        public const int RunDoLast_Count = 3;
        public const int RunDoCount = 4;
        public const int RunDoSkip_Count = 5;
        public const int RunDoError_Count = 6;
        public const int RunDoWarning_Count = 7;
        public const int RunDoRetry_Count = 8;
        public int RunMetricOrStateX = RunMetric_Max + 1;
        public int RunMetricOrStateY = RunMetric_Max + 2;
        public int RunMetricOrStateZ = RunMetric_Max + 3;
        public int RunMetricOrState1 = RunMetric_Max + 4;
        public int RunMetricOrState2 = RunMetric_Max + 5;
        //
        // State, Category, Location or Action
        public static int RunAction_Max = 25;
        public int RunAction = 0;
        public int RunActionRequest = 0;
        public const int RunCancel = 1;
        public const int RunPause = 2;
        public const int RunStart = 3;
        public const int RunResume = 4; // RunNoOp4
        public const int RunNoOp4 = 4;
        public const int RunNoOp5 = 5;
        public const int RunInitialize = 6;
        public const int RunRunDo = 7;
        public const int RunUserInput = 8;
        public const int RunOpen = 9;
        public const int RunMain_Do = 10;
        public const int RunMain_DoSelect = 11;
        public const int RunMain_DoLock_Add = 12;
        public const int RunMain_DoRead = 13;
        public const int RunMain_DoValidate = 14;
        public const int RunMain_DoAccept = 15;
        public const int RunMain_DoReport = 16;
        public const int RunMain_DoProcess = 17;
        public const int RunMain_DoUpdate = 18;
        public const int RunMain_DoWrite = 19;
        public const int RunMain_DoLock_Remove = 20;
        public const int RunClose = 21;
        public const int RunFinish = 22;
        public const int RunAbort = 23;
        public const int RunReloop = 24;
        public const int RunFirst = 25;
        // User actions, options, and misc. array storage
        public int RunActionOrOptionX = RunAction_Max + 1;
        public int RunActionOrOptionY = RunAction_Max + 2;
        public int RunActionOrOptionZ = RunAction_Max + 3;
        public int RunActionOrOption1 = RunAction_Max + 4;
        public int RunActionOrOption2 = RunAction_Max + 5;
        //
        //
        public int[,] RunActionState = new int[RunAction_Max + 5, RunMetric_Max + 5];
        //
        #endregion
        #region Progress Change
        public ProgressChangedEventArgs RunActionProgressChangeEventArgs = new ProgressChangedEventArgs((int)0, "");
        //
        #endregion
        #region Run Action Indexor based Property Change including Progress Change call
        /// <summary>
        /// An indexed accessor for RunActionState[RunAction, RunState] where
        /// set will additionally create an "R"un type message that 
        /// will be passed by the multithreaded delegate (ThreadUiProgressAsync)
        /// or altenatively throw a delegate exception (ExceptDelegate)
        /// </summary> 
        /// <param name="Value">The RunTense value the Action State will be set to.</param> 
        /// <returns>
        /// </returns>
        /// <remarks>
        /// ThreadUiProgressAsync can be set to the following methods when
        /// multithreaded user interface messaging is not in use:
        /// xxx more...
        /// </remarks> 
        public int RunActionStateProp {
            get { return RunActionState[RunAction, RunState]; }
            set {
                RunTense = value;
                RunActionState[RunAction, RunState] = RunTense;
                RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(
                    (int)LocalProgressBar_Value,
                    "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString()
                    // + LocalMessage.Msg
                    );
                if (ThreadUiProgressAsync != null) {
                    ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs);
                } else {
                    ExceptDelegate(
                        "ThreadUiProgressAsync delegate is not set!",
                        (long)StateIs.DoesNotExist);
                }
            }
        }
        #endregion
        #region Description fields for Run Action and Metrics Fields
        public String[] RunActionVerb = { "NoOp", 
                                               "Cancel", "Pause", "Started", "Resume", "NoOp5", 
                                               "Initialize", "Do", "UserInput", "Open", "DoMain",
                                               "Select", "Lock", "Read", "Validate", "Accept",
                                               "Report", "Process", "Update", "Write", "UnLock",
                                               "Finish", "Abort", 
                                               "OptionX", "OptionY", "OptionZ", "VerbY", "VerbZ"
                                           };
        public String[] RunActionDoing = { "NoOping", 
                                                "Cancelling", "Pausing", "Starting", "NoOp4", "NoOp5",
 
                                               "Initialize", "Doing", "UserInputing", "Opening", "DoingMain",
                                               "Selecting", "Locking", "Reading", "Validating", "Accepting",
                                               "Reporting", "Processing", "Updating", "Writing", "UnLocking",
                                               "Finishing", "Abortint", 
                                               "OptionXing", "OptionYing", "OptionZing", "VerbYing", "VerbZing"
                                            };
        public String[] RunActionDid = { "NoOped", 
                                              "Cancelled", "Paused", "Started", "NoOp4", "NoOp5", 
                                               "Initialized", "Did", "UserInputed", "Opened", "DoMained",
                                               "Selected", "Locked", "Read", "Validated", "Accepted",
                                               "Reported", "Processed", "Updated", "Writen", "UnLocked",
                                               "Finished", "Aborted", 
                                               "OptionXed", "OptionYed", "OptionZed", "VerbYed", "VerbZed"
                                          };
        //
        #endregion
        #endregion
        // xxxxxxxx Run Action Declarations - Misc, UserState, Errors, Iteration, 
        #region Run Action State
        #region Run State Fields and Flags.  Misc and working.
        // <Area Id = "PrimaryActions">
        public String spRunOptions;
        public String RunOptions { get { return spRunOptions; } set { spRunOptions = value; } }
        public int ipRunStatus;
        public int RunStatus { get { return ipRunStatus; } set { ipRunStatus = value; } }
        //
        // High level command
        public String FileRunRequest;
        public long FileRunResult;
        // File Level command
        public String FileDoRequest;
        public long FileDoResult;
        // <Area Id = "RunStatusControlItFlags">
        public int RunCount = 0;
        public int RunDebugCount = 0;
        public bool RunReloopIsOn = false; // init
        public bool RunFirstIsOn = true; // init
        //
        public bool RunStartPending = bNO; // init
        public bool RunPausePending = bNO; // init
        public bool RunCancelPending = bNO; // init
        //
        public String sRunActionRequest;
        #endregion
        #region Run User State
        // PROGRESS CHANGED
        public String UserState;
        public String UserCommandPrefix;
        public String UserCommand;
        public String ThreadUiTextMessageContent;
        #endregion
        #region Run Errors
        // <Area Id = "Errors">
        public bool RunAbortIsOn = bNO;
        //
        public bool RunErrorDidOccur = false;
        public bool RunErrorDidOccurOnce = false;
        //
        public int RunErrorNumber = 0;
        public int RunGlobalErrorNumber = 99999;
        public int RunThrowException = 99999;
        public int RunShellErrorNumber = 99999;
        public int RunErrorCount = 0;
        #endregion
        #region Run Processing Loop Iteration
        #region Iteration properties
        // <Area Id = "IterationStatusControlItFlags">
        public int iIterationCount;
        public int iIterationRemaider;
        public int iIterationDebugCount;
        public bool IterationAbort;
        public bool IterationReloop;
        public bool IterationFirst;
        public int iIterationLoopCounter;
        #endregion
        #region Method Iteration properties
        // <Area Id = "MethodIterationStatusControlItFlags">
        public bool MethodIterationAbort;
        public bool MethodIterationReloop;
        public bool MethodIterationFirst;
        public int MethodIterationLoopCounter;
        #endregion
        #endregion
        #endregion
        // xxxxxxxx Run Action Command Evaluation
        #region Run Action Command Evaluate Analysis
        /// <summary> 
        /// The run action system uses strategy of passing messages
        /// between threads with a marshalled prefix indicating the command
        /// message routing or type.  Primary commands that related to Run
        /// Actions are analyzed here and include verb tenses for Start, Cancel, 
        /// and Pause (Started, Cancelled, Paused, Resumed). 
        /// </summary> 
        /// <remarks>
        /// Other possible command may include Suspend, Schedule, 
        /// or Snapshot (or Persist State) as well as any default or template 
        /// creation that is present in the property system.
        /// </remarks> 
        public virtual void RunActionExtractCommands(ref Object Sender, ProgressChangedEventArgs ePcea, DefStdBaseRun omHPassed) {
            // ref Object PassedObject
            // DefStdBaseRun omHPassed 
            // This code handles both the Page UI command request and call backs from BgWorker
            // DefStdBaseRun omHPassed = (DefStdBaseRun)PassedObject;
            try {
                UserState = (String)ePcea.UserState;
            } catch { UserState = ""; }
            UserCommandPrefix = "";
            UserCommand = "";
            if (UserState.Length > 0) { UserCommandPrefix = UserState.Substring(0, 1); }
            if (UserState.Length > 1) { UserCommand = UserState.Substring(1); }
            LocalMessage.Msg = UserCommand;
            if (UserCommandPrefix == "$") {
                if (ThreadUiProgressAsync == null) {
                    ExceptDelegate("FATAL ERROR: ThreadUiProgressAsync delegate is not set!",
                        (long)StateIs.DoesNotExist);
                }
                if (UserCommand == "Started") {
                    if (omHPassed.RunActionState[RunRunDo, RunState] != RunTense_Did
                        && omHPassed.RunActionState[RunRunDo, RunState] != RunTense_Doing
                        ) {
                        RunStartPending = bYES;
                        omHPassed.RunActionState[RunAbort, RunState] = iNO;
                        omHPassed.RunActionState[RunFirst, RunState] = iYES;
                        omHPassed.RunActionState[RunReloop, RunState] = iNO;
                        RunAction = RunRunDo;
                        RunMetric = RunState;
                        RunTense = RunTense_Do;
                        omHPassed.RunActionState[RunRunDo, RunState] = RunTense_Do;
                        omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                    "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                        ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                        // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                    }
                } else if (UserCommand == "Started") {
                    RunStartPending = bNO;
                    RunAction = RunRunDo;
                    RunMetric = RunState;
                    RunTense = RunTense_Doing;
                    omHPassed.RunActionState[RunRunDo, RunState] = RunTense_Doing;
                    omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                         "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                    ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                    // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                } else if (UserCommand == "Cancel") {
                    if (omHPassed.RunActionState[RunRunDo, RunState] == RunTense_Doing) {
                        if (omHPassed.RunActionState[RunCancel, RunState] != RunTense_Did && omHPassed.RunActionState[RunCancel, RunState] != RunTense_Doing) {
                            RunCancelPending = bYES;
                            RunAction = RunCancel;
                            RunMetric = RunState;
                            RunTense = RunTense_Do;
                            omHPassed.RunActionState[RunCancel, RunState] = RunTense_Do;
                            omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                                "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                            ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                            // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                            RunCancelAsync();
                        }
                    }
                } else if (UserCommand == "Cancelled") {
                    RunCancelPending = bNO;
                    RunAction = RunCancel;
                    RunMetric = RunState;
                    RunTense = RunTense_Did;
                    omHPassed.RunActionState[RunCancel, RunState] = RunTense_Did;
                    omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                         "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                    ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                    // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                } else if (UserCommand == "Pause") {
                    // System.Windows.RoutedEventArgs RoutedEventItemTemp = new System.Windows.RoutedEventArgs();
                    // RoutedEventTemp = null;
                    // XUomMavvXv.XUomVtvvXv.CallerAsynchronousEventsPauseClick;
                    // Set State
                    if (!RunCancelPending) {
                        if (omHPassed.RunActionState[RunPause, RunState] == RunTense_Done
                            ) {
                            LocalMessage.Msg = "Pause ended, resuming now...";
                            RunPausePending = bNO;
                            RunAction = RunPause;
                            RunMetric = RunState;
                            RunTense = RunTense_Off;
                            omHPassed.RunActionState[RunPause, RunState] = RunTense_Off;
                            omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                        "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                            ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                            // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                        } else if (omHPassed.RunActionState[RunPause, RunState] == RunTense_Doing
                            ) {
                            LocalMessage.Msg = "Pausing, please wait...";
                            RunAction = RunPause;
                            RunMetric = RunState;
                            RunTense = RunTense_Off;
                            omHPassed.RunActionState[RunPause, RunState] = RunTense_Off;
                            omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                        "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                            ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                            // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                        } else if (omHPassed.RunActionState[RunPause, RunState] == RunTense_Did
                            ) {
                            LocalMessage.Msg = "Paused now, waiting for resume...";
                            RunAction = RunPause;
                            RunMetric = RunState;
                            RunTense = RunTense_Off;
                            omHPassed.RunActionState[RunPause, RunState] = RunTense_Off;
                            omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                        "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                        } else if (omHPassed.RunActionState[RunPause, RunState] == RunTense_Do
                            ) {
                            LocalMessage.Msg = "Pause request submitted, please wait...";
                            RunPausePending = bYES;
                            RunAction = RunPause;
                            RunMetric = RunState;
                            RunTense = RunTense_Do;
                            omHPassed.RunActionState[RunPause, RunState] = RunTense_Do;
                            omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                        "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                            ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                            // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                        } else if (omHPassed.RunActionState[RunPause, RunState] != RunTense_DoNot
                            //  && omHPassed.RunActionState[RunPause, RunState] != RunTense_Doing
                            //  && omHPassed.RunActionState[RunPause, RunState] != RunTense_Do
                            //  && omHPassed.RunActionState[RunPause, RunState] != RunTense_Did
                            //  && omHPassed.RunActionState[RunPause, RunState] != RunTense_Off 
                            //  && omHPassed.RunActionState[RunPause, RunState] != RunTense_Done 
                                && omHPassed.RunActionState[RunPause, RunState] != RunTense_DidNot
                                ) {
                            LocalMessage.Msg = "Other Pause action. I.E. DoNot or DidNot";
                        } else {
                            LocalMessage.Msg = "Invalid Pause action other...";
                        }
                    }
                } else if (UserCommand == "Paused") {
                    RunPausePending = bNO;
                    RunAction = RunPause;
                    RunMetric = RunState;
                    RunTense = RunTense_Did;
                    omHPassed.RunActionState[RunPause, RunState] = RunTense_Did;
                    omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                        "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                    ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                    // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                } else if (UserCommand == "Resume") { // not used
                    LocalMessage.Msg = "Resume command";
                    // omHPassed.RunActionState[RunResume, RunState] = Resume_Something;
                    omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                    ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                    // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                }
            } else if (UserCommand == "xxxxx") {

            }
        }
        #endregion
        // xxxxxxxx 
        #region Local Run Action Pause
        public int iAppActionWaitMilliIncrement = 500;
        public int iAppActionWaitMilliIncrementMax = 30000;
        public bool bAppActionWaitContinue = true;
        public int iAppActionWaitCounter;
        #endregion
        // xxxxxxxx 
        #region Local Progress Bar
        ProgressBar LocalProgressBar;
        public double LocalProgressBar_Minimum = 0;
        public double LocalProgressBar_Maximum = 0;
        public double LocalProgressBar_Value = 0;
        public int LocalProgressBar_Display = 0;
        //
        public ProgressChangedEventArgs LocalThreadProgressChangedItem;
        public RoutedPropertyChangedEventArgs<double> LocalThreadPropertyChangedItem;
        public RunWorkerCompletedEventArgs LocalRunCompletedItem;
        public double LocalOldValue = 0;
        public double LocalNewValue = 0;
        #endregion
        #endregion
        //
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        //
        #region Introspection
        #region Introspection Data
        // Introspection
        public long iAppCoreObjectCheck;
        public long iAppCoreObjectGet;
        public long iAppCoreObjectSet;
        public long iAppCoreObjectReset;

        public long iAppIoObjectGet;
        public long iAppIoObjectSet;
        public long iAppMfileObjectGet;
        public long iAppMfileObjectSet;

        public String MethodObjectType;
        public System.Type odstMethodObjectType;
        public int MethodObjectHashCode;
        public String MethodObjectToString;
        public bool MethodObjectEquality;
        public bool MethodObjectTypeValid;
        public bool MethodObjectExternalExistance;
        public bool MethodObjectInternalExistance;
        #endregion
        /// <summary> 
        /// The Application Core Object Type module is currently
        /// only partly implemented.  A number of basic test involving
        /// weak and strong typed object within the MVVC were conducted.
        /// The final implemented code will work from a list object and use
        /// the standard .Net introspection mechanisms.
        /// This was delayed until the layer handling interop, DLR and the 
        /// property system was more clearly defined and coded.  In particular,
        /// a finilized design for list mangement and loading was desired.
        /// Future applications will include a list of Object that the MVVC will
        /// instantiate, validate, etc.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        /// </Task Id="?" Assembly="?" File="?" Class="?" Method="?" Priority="?"
        /// Urgency="?" DueDate="?" Owner="?" AssignedTo="?">
        /// Implement the list look up, loading etc for the object type at this level,
        /// code is already available for the manipulating and checking the type string.
        /// </Task>
        public virtual long AppCoreObjectCheck(Object ooPassedObject, Object ooPassedInternalObject, String PassedObjectType) {
            iAppCoreObjectCheck = (long)StateIs.Started;
            //
            try {
                odstMethodObjectType = ooPassedObject.GetType();
                MethodObjectHashCode = ooPassedObject.GetHashCode();
                MethodObjectToString = ooPassedObject.ToString();
                MethodObjectEquality = true;
                // 
                // 
                if (ooPassedObject != null) {
                    MethodObjectExternalExistance = true;
                    if (ooPassedInternalObject == null) {
                        ooPassedInternalObject = ooPassedObject;
                    } else {
                        if (ooPassedInternalObject == ooPassedObject) {
                            MethodObjectEquality = true;
                            MethodObjectTypeValid = true;
                        } else {
                            MethodObjectEquality = false;
                            MethodObjectTypeValid = false;
                        }
                    }
                } else {
                    MethodObjectExternalExistance = false;
                    MethodObjectEquality = false;
                    if (ooPassedInternalObject == null) {
                        MethodObjectInternalExistance = false;
                    } else {
                        MethodObjectInternalExistance = true;
                        MethodObjectTypeValid = true;
                    }
                }
            } catch (SystemException omveValidationException) {
                //
                MethodObjectTypeValid = false;
                //
            } finally {
                // TODO z$OPTIONAL AppCoreObjectCheck Message to page here.
                if (ooPassedInternalObject != null) {
                    MethodObjectInternalExistance = true;
                    MethodObjectToString = ooPassedInternalObject.ToString();
                    odstMethodObjectType = ooPassedInternalObject.GetType();
                    MethodObjectType = odstMethodObjectType.ToString();
                    if (PassedObjectType != null) {
                        if (PassedObjectType.Length > 0) {
                            if (MethodObjectType != PassedObjectType) {
                                MethodObjectTypeValid = false;
                            } else { MethodObjectTypeValid = true; };
                        } else { MethodObjectTypeValid = true; };
                    } else {
                        MethodObjectInternalExistance = false;
                    }
                } else {
                    MethodObjectInternalExistance = false;
                }
            }
            return iAppCoreObjectCheck;
        }
        #endregion
        //#region ConsoleMessageBox
        //public double BoxWidthCurrent = 0;
        //public MessageBoxPadding BoxPadding;
        //public double WidthCurrent = 0;
        //#endregion
        #region General Exceptions
        //
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        //
        #region General Delegate Exception
        // A delegate exception occuring should indicate that
        // part of all of the user interface is not working.
        // File logging can be be employed along with throwing
        // an exception that is not caught by the application.
        public long ExceptDelegateResult;
        /// <summary> 
        /// A error has occured involving a delegate's setting or usage.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public void ExceptDelegate(
            String ErrorMsgPassed,
            long PassedMethodResult
            ) {
            ExceptDelegateResult = PassedMethodResult;
            RunErrorDidOccur = bYES;
            LocalMessage.ErrorMsg = "Delegate Exception: (" + PassedMethodResult.ToString() + ") " + ErrorMsgPassed;
            MessageDetailsDef ExceptDelegateMsg = new MessageDetailsDef();
            ExceptDelegateMsg.Sender = Sender;
            ExceptDelegateMsg.IsMessage = bYES;
            ExceptDelegateMsg.Location1 = iNoValue;
            ExceptDelegateMsg.Location2 = iNoValue;
            ExceptDelegateMsg.MethodResult = PassedMethodResult;
            ExceptDelegateMsg.Level = iNoErrorLevel;
            ExceptDelegateMsg.Source = iNoErrorSource;
            ExceptDelegateMsg.DoDisplay = bNO;
            ExceptDelegateMsg.ResponseFlags = MessageNoUserEntry;
            ExceptDelegateMsg.Text = "A2" + LocalMessage.ErrorMsg + "\n";
            TraceMdmDo(ExceptDelegateMsg);
        }
        #endregion
        #region General Exception
        public long ExceptGeneralResult;
        /// <summary> 
        /// The general exeception integrates exceptions with the
        /// Tracing, Logging, Console and Status Line components.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public void ExceptGeneral(
            String ErrorMsgPassed,
            long PassedMethodResult
            ) {
            ExceptGeneralResult = PassedMethodResult;
            RunErrorDidOccur = bYES;
            LocalMessage.ErrorMsg = "General Exception: (" + PassedMethodResult.ToString() + ") " + ErrorMsgPassed;
            MessageDetailsDef ExceptGeneralMsg = new MessageDetailsDef();
            ExceptGeneralMsg.Sender = Sender;
            ExceptGeneralMsg.IsMessage = bYES;
            ExceptGeneralMsg.Location1 = iNoValue;
            ExceptGeneralMsg.Location2 = iNoValue;
            ExceptGeneralMsg.MethodResult = PassedMethodResult;
            ExceptGeneralMsg.Level = iNoErrorLevel;
            ExceptGeneralMsg.Source = iNoErrorSource;
            ExceptGeneralMsg.DoDisplay = bNO;
            ExceptGeneralMsg.ResponseFlags = MessageNoUserEntry;
            ExceptGeneralMsg.Text = "A2" + LocalMessage.ErrorMsg + "\n";
            TraceMdmDo(ExceptGeneralMsg);
        }
        #endregion
        #endregion
        #region General Status Validation
        /// <summary> 
        /// Does the result indicate a valid state.
        /// </summary> 
        public bool StateIsSuccessful(long StatePassed) {
            return ((StatePassed & (long)(StateIs.MaskSuccessful)) > 0);
            return ((StatePassed & (long)(
                StateIs.Finished
                | StateIs.Successful
                | StateIs.NormalEnd
                | StateIs.Valid
                )) != 0);
            //|| StatePassed == (long)StateIs.ShouldNotExist
            //|| StatePassed == (long)StateIs.ShouldExist
            //|| StatePassed == (long)StateIs.DoesNotExist
            //
            //|| StatePassed == (long)StateIs.Undefined
            //|| StatePassed == (long)FileAction_Do.NotSet
            //|| StatePassed == (long)FileAction_Do.UseDefault
            //|| StatePassed == (long)StateIs.UndefinedResult
        }

        public bool StateIsSuccessfulAll(long StatePassed) {
            return ((StatePassed & (long)(StateIs.MaskSuccessfulAll)) > 0);
        }

        /// <summary> 
        /// Does the result indicate an invalid state.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public bool StateIsInvalid(long StatePassed) {
            return ((StatePassed & (long)(StateIs.MaskInvalid)) > 0);
            return ((StatePassed & (long)(StateIs.AbnormalEnd
                    | StateIs.Undefined
                    | StateIs.UndefinedResult
                    | StateIs.UnknownFailure
                    | StateIs.EmptyResult
                    | StateIs.EmptyValue
                    | StateIs.MissingName
                    | StateIs.None
                    | StateIs.NotSet
                    )) != 0);
        }

        /// <summary> 
        /// Is the result a value that deals with existence.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public bool StateIsExistence(long StatePassed) {
            return ((StatePassed & (long)(StateIs.MaskExistence)) > 0);
            return ((StatePassed & (long)(
                    StateIs.DoesExist
                    | StateIs.ShouldNotExist
                    | StateIs.ShouldExist
                    | StateIs.DoesNotExist
                    )) != 0);
        }

        /// <summary> 
        /// Does the result indicate a failed test for existence.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public bool StateIsExistenceFailed(long StatePassed) {
            return ((StatePassed & (long)(StateIs.MaskExistenceFailed)) > 0);
            return ((StatePassed & (long)(
                    StateIs.ShouldNotExist
                    | StateIs.ShouldExist
                    | StateIs.DoesNotExist
                    )) != 0);
        }

        /// <summary> 
        /// Does the result indicate a successful test for existence.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public bool StateIsExistenceSuccessful(long StatePassed) {
            return ((StatePassed & (long)(StateIs.MaskExistenceSuccessful)) > 0);
            return ((StatePassed & (long)(
                    StateIs.DoesExist
                    )) != 0);
        }

        /// <summary> 
        /// Is the result a completion status value.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public bool StateIsTask(long StatePassed) {
            return ((StatePassed & (long)(StateIs.MaskTask)) > 0);
            return ((StatePassed & (long)(
                    StateIs.NotStarted
                    | StateIs.Started
                    | StateIs.InProgress
                    | StateIs.OnHold
                    | StateIs.Cancelled
                    | StateIs.Finished
                    | StateIs.Failed
                    | StateIs.Successful
                    )) != 0);
        }

        /// <summary> 
        /// Does the result indicate any known type error.
        /// </summary> 
        /// <remarks>
        /// Values that express "Should" or "Should Not" are included
        /// in this test.  State results that include should types of value are
        /// generated as a result of options flags in most cases.  
        /// For example, a checked option indicating a file must already
        /// exist could result in a ShouldExist result error if the file were
        /// missing.
        /// </remarks> 
        public bool StateIsError(long StatePassed) {
            return ((StatePassed & (long)(StateIs.MaskError)) > 0);
            return ((StatePassed & (long)(
                StateIs.AbnormalEnd
                | StateIs.BadData
                | StateIs.Cancelled
                | StateIs.DatabaseError
                | StateIs.EmptyResult
                | StateIs.EmptyValue
                | StateIs.Failed
                | StateIs.InProgress
                | StateIs.MissingName
                | StateIs.NotSet
                | StateIs.NotStarted
                | StateIs.OsError
                | StateIs.TimedOut
                | StateIs.Undefined
                | StateIs.UndefinedResult
                | StateIs.UnknownFailure
                    )) != 0);
        }

        /// <summary>
        /// This function generates friendly descriptions for states
        /// suitable for use in the user interface, logging or error messages.
        /// The description is in the form of a present tense phrase with
        /// no leading or trailing spaces that can be inserted into other text.
        /// </summary> 
        /// <param name="ResultPassed">The state result to be described.</param> 
        /// <returns>
        /// A string describing the result state.
        /// </returns>
        /// <remarks>
        /// </remarks> 
        public String StateDescriptionGet(long ResultPassed) {
            String StateDescription = "";
            switch (ResultPassed) {
                case ((long)StateIs.AbnormalEnd):
                    StateDescription = "abnormal end of operation";
                    break;
                case ((long)StateIs.BadData):
                    StateDescription = "bad data was encountered";
                    break;
                case ((long)StateIs.Cancelled):
                    StateDescription = "operation Cancelled";
                    break;
                case ((long)StateIs.DoesExist):
                    StateDescription = "item already exists";
                    break;
                case ((long)StateIs.DoesNotExist):
                    StateDescription = "item not found";
                    break;
                case ((long)StateIs.DatabaseError):
                    StateDescription = "database error";
                    break;
                case ((long)StateIs.EmptyResult):
                    StateDescription = "empty result occured";
                    break;
                case ((long)StateIs.Failed):
                    StateDescription = "operation Failed";
                    break;
                case ((long)StateIs.Finished):
                    StateDescription = "operation is Finished";
                    break;
                case ((long)StateIs.InProgress):
                    StateDescription = "operation is InProgress";
                    break;
                case ((long)StateIs.Invalid):
                    StateDescription = "invalid result";
                    break;
                case ((long)StateIs.MaskError):
                case ((long)StateIs.MaskFailedAll):
                case ((long)StateIs.MaskExistence):
                case ((long)StateIs.MaskExistenceFailed):
                // case ((long)StateIs.MaskExistenceSuccessful):
                case ((long)StateIs.MaskInvalid):
                case ((long)StateIs.MaskResult):
                case ((long)StateIs.MaskSuccessful):
                case ((long)StateIs.MaskSuccessfulAll):
                case ((long)StateIs.MaskTask):
                case ((long)StateIs.MaskTaskOpen):
                case ((long)StateIs.MaskTaskClosed):
                    StateDescription = "unexpected masking encountered";
                    break;
                case ((long)StateIs.MissingName):
                    StateDescription = "name or value missing in operation";
                    break;
                case ((long)StateIs.None):
                    StateDescription = "no result was set";
                    break;
                case ((long)StateIs.NormalEnd):
                    StateDescription = "normal end of operation";
                    break;
                case ((long)StateIs.NotSet):
                    StateDescription = "expected value not set in operation";
                    break;
                case ((long)StateIs.NotStarted):
                    StateDescription = "operation is NotStarted";
                    break;
                case ((long)StateIs.OnHold):
                    StateDescription = "opation is OnHold";
                    break;
                case ((long)StateIs.OsError):
                    StateDescription = "operating system error";
                    break;
                case ((long)StateIs.ShouldExist):
                    StateDescription = "item should exist but does not";
                    break;
                case ((long)StateIs.ShouldNotExist):
                    StateDescription = "item should not exist but does";
                    break;
                case ((long)StateIs.Started):
                    StateDescription = "operation is Started";
                    break;
                case ((long)StateIs.Successful):
                    StateDescription = "operation was Successful";
                    break;
                case ((long)StateIs.TimedOut):
                    StateDescription = "timed out";
                    break;
                case ((long)StateIs.Undefined):
                    StateDescription = "state not currently defined";
                    break;
                case ((long)StateIs.UndefinedResult):
                    StateDescription = "operation result is undefined";
                    break;
                case ((long)StateIs.UnknownFailure):
                    StateDescription = "operation had an unknown failure";
                    break;
                case ((long)StateIs.Valid):
                    StateDescription = "result of operation was valid";
                    break;
                default:
                    StateDescription = "unknown error value:" + " (" + ResultPassed + ")";
                    break;
            }
            return StateDescription;
        }
        #endregion
    }
    //
    /// <summary>
    /// <para> Standard Base class including Run Control and File features.</para> 
    /// <para> . </para>
    /// <para> This enumeration controls the hierarchical loading of
    /// validation lists and combo box drop downs for the
    /// file system and its user interface.</para>
    /// <para> . </para>
    /// <para> Please see the Run File ReadMe for more information</para>
    /// </summary>
    public class DefStdBaseRunFile : DefStdBaseRun {
        /// <summary>
        /// Use DefStdBaseRunFile(long ClassHasPassed) and indicate what features are used.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public DefStdBaseRunFile() {
            Sender = this;
            SenderIsThis = this;
        }

        /// <summary>
        /// This level of abscration currently includes no methods, fields and properties.
        /// It is defined by it's numerous companion enumerations and classes.
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks>
        /// </remarks> 
        public DefStdBaseRunFile(long ClassHasPassed)
            : base(ClassHasPassed) {
            Sender = this;
            SenderIsThis = this;
        }
    }
    //xxxxxxxxxxxxxxxxxxxxxxxxxxx Supporting Classes xxxxxxxxxxxxxxxxxxxx
    // none...
    //xxxxxxxxxxxxxxxxxxxxxxxxxxx Enumerations, Constants, Utility xxxxxxxxxxxxxxxxxxxx

    #region FileAction Constants
    /// <summary> 
    /// Indicates the direction of file IO and a primary type field of files.
    /// </summary> 
    public enum FileAction_DirectionIs : int {
        Output = 1,
        Input = 2,
        Both = 3,
        None = 4
    }
    // FileAction.
    /// <summary> 
    /// The read / write IO mode for the indicated lists and objects.
    ///  This indicates wether the object information is stored in a
    /// database, text file, etc. and how it should be loaded (ie. All).
    /// </summary> 
    [Flags]
    public enum FileAction_ReadModeIs : long {
        System = FileIo_ModeIs.All,
        Server = FileIo_ModeIs.Sql,
        Service = FileIo_ModeIs.All,
        Database = FileIo_ModeIs.Sql,
        FileGroup = FileIo_ModeIs.Sql,
        Table = FileIo_ModeIs.Sql,
        DbUser = FileIo_ModeIs.Sql,
        DbSecurityType = FileIo_ModeIs.Sql,
        DbPassword = FileIo_ModeIs.Sql,
        DiskFile = FileIo_ModeIs.All,
        AsciiDef = FileIo_ModeIs.All,
        //
        None = 0x0
    }

    /// <summary> 
    /// This enumeration provides a list of file action(s) that 
    /// can be performed on the file.  It also provides a list
    /// of the file and database related system objects (such
    /// as systems, servers, databases, etc.)
    /// </summary> 
    [Flags]
    public enum FileAction_Do : long {
        Undefined = 0x90001, // Action
        NotSet = 0x90002,
        UseDefault = 0x90004,
        UndefinedResult = 0x90008,
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // ACTION TODO
        Open = 0x1001,
        Close = 0x1002,
        Create = 0x1004,
        Delete = 0x1008,
        Read = 0x1010,
        Write = 0x1011,
        Connect = 0x1012,
        Check = 0x1014,
        ListGet = 0x1018,
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // File Object Type
        // Database
        FileDictData = 0x10001,
        FileData = 0x10002,
        // File System Object File and streams
        FsoDictData = 0x10004,
        FsoData = 0x10008,
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // OBJECT TODO
        ObjectFILEDATA = 0x100001,
        ObjectFILEDICT = 0x100002,
        ObjectDATABASE = 0x100004,
        ObjectSERVICE = 0x100008,
        ObjectSERVER = 0x100010,
        ObjectSYSTEM = 0x100011,
        ObjectNETWORK = 0x100012,
        ObjectSECURITY = 0x100014,
        ObjectUSER = 0x100018,
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // <Area Id = "Constants">
        System = 0x511,
        Server = 0x513,
        Service = 0x514,
        Database = 0x515,
        FileGroup = 0x517,
        Table = 0x519,
        DbUser = 0x521,
        DbSecurityType = 0x523,
        DbPassword = 0x525,
        DiskFile = 0x551,
        AsciiDef = 0x581,
        //
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // <Area Id = "AsciiOpenOptions">
        // <Area Id = "FileAccess">
        IoAccessReadOnly = 0x8000111,
        IoAccessAppendOnly = 0x8000112,
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // <Area Id = "FileCreation">
        IoCreateIfMissing = 0x7123,
        IoCreateOnly = 0x7124,
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    }


    /// <summary> 
    /// This enumeration provides a list of basic relooping strategies
    /// that can be employed when allowing the file classes to perform
    /// path and default name searching for requested files.
    /// </summary> 
    /// <remarks>
    /// Part of the principals of operation employed in the OSS classes
    /// is the concept that all properties and objects have
    /// default values and a hierachy of determining defaults that may 
    /// include context analysis.
    /// </remarks> 
    public enum FileAction_OpenControl : long {
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // <Area Id = "FileOpenConstants">
        // File Search Loop Control
        TryFirst = -3,
        TryAgain = -2,
        TryEntered = 1,
        TryDefault = 2,
        TryOriginal = 3,
        TryAll = 3
    }
    #endregion

    #region File UI Constants
    /// <summary>
    /// <para> File User Interface Level</para>
    /// <para> Indicates which user interface elements
    /// are to be updated or have changed.</para>
    /// <para> . </para>
    /// <para> The database system has four distinct
    /// areas that would likely be displayed in the
    /// user interface.  They are:</para>
    /// <para> ..Core Services </para>
    /// <para> ..User Information </para>
    /// <para> ..Security Information </para>
    /// <para> ..Master Database Information </para>
    /// <para> . </para>
    /// <para> . </para>
    /// <para> The core services are basically the file
    /// being accessed.  It includes:</para> 
    /// <para> ..System</para>
    /// <para> ..Service</para>
    /// <para> ..Server</para>
    /// <para> ..Database</para>
    /// <para> ..FileGroup</para>
    /// <para> ..Table</para>
    /// <para> ..DiskFile</para>
    /// <para> ..FileOwner</para>
    /// </summary>
    [Flags]
    public enum FileUi_LevelIs : long {
        MaskCore = 0x00001FFF,
        MaskUser = 0x01FF0000,
        MaskSecurity = 0x02FF0000,
        MaskMaster = 0x04FF0000,
        //
        MaskIsCore = 0x00001000,
        MaskIsUser = 0x01000000,
        MaskIsSecurity = 0x02000000,
        MaskIsMaster = 0x04000000,
        MaskIsFile = 0x08000000,
        MaskOther = 0xFF000000,
        MaskIsOther = 0xFF000000,
        //
        CoreDoAll = FileUi_LevelIs.MaskCore,
        System = 0x1200,
        Service = 0x1100,
        Server = 0x1080,
        Database = 0x1040,
        FileOwner = 0x1020,
        // open 0x1010,
        FileGroup = 0x1008,
        Table = 0x1004,
        DiskFile = 0x1002,
        // open 0x1001,
        //
        UserDoAll = FileUi_LevelIs.MaskUser,
        User = 0x1010000,
        UserPassword = 0x1020000,
        //
        SecurityDoAll = FileUi_LevelIs.MaskSecurity,
        Security = 0x2010000,
        //
        MasterDoAll = FileUi_LevelIs.MaskMaster,
        MasterSystem = 0x4010000,
        MasterServer = 0x4020000,
        MasterDatabase = 0x4040000,
        MasterFile = 0x4080000,
        //
        FileDoAll = FileUi_LevelIs.MaskIsFile,
        File = 0x8010000,
    }
    #endregion

    #region File Io Constants
    /// <summary> 
    /// This enumerations contains file system errors.
    /// </summary> 
    [Flags]
    public enum FileIo_ErrorIs : long {
        None = 0x0,
        DiskFull = 0x20,
        DiskError = 0x10,
        NetworkError = 0x100,
        InternetError = 0x1000,
        DatabaseError = 0x10000,
        SqlError = 0x20000,
        NotCompleted = 0x100000,
        IoError = 0x1,
        OsError = 0x2,
        NotSupported = 0x4,
        General = 0x8,
        AccessError = 0x10000000
    }

    /// <summary> 
    /// The read modes for accessing database and disk files.
    /// Sql is the only database mode in use.  For text files the
    /// options include Line Mode, All (content) Mode, or Block Mode.
    /// Block mode is intended for processing very large files.
    /// Where processing is line based and the file is very large
    /// then line mode might be employed.  Binary files would be
    /// read in All Mode or Block Mode.  Binary and Seek Modes
    /// are currently not implemented and should be considered
    /// legacy features.
    /// </summary> 
    [Flags]
    public enum FileIo_ModeIs : long {
        None = 0x0,
        // <Area Id = "FileReadModeConstants">
        Block = 0x1,
        Line = 0x10,
        All = 0x100,
        Sql = 0x1000,
        // additional access modes
        Binary = 0x10000,
        Seek = 0x100000
    }

    /// <summary> 
    /// This enumerates the type of SQL file access available
    /// for use by command execution and controls result sets.
    /// </summary> 
    [Flags]
    public enum FileIo_CommandModeIs : long {
        None = 0x0,

        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // Schema / Entity Relationship / Dictionary
        //UseErSingleResult = 0x601,
        //UseErSchemaOnly = 0x602,
        //UseErKeyInfo = 0x604,
        //UseErSingleRow = 0x608,
        //UseErSequentialAccess = 0x610,
        //UseErCloseConnection = 0x611,
        //UseExecuteNoQuery = 0x612,
        //UseExecuteScalar = 0x614,
        //
        //UseErDefault = 0x90618,
        //
        Default = CommandBehavior.Default,
        SingleResult = CommandBehavior.SingleResult,
        SchemaOnly = CommandBehavior.SchemaOnly,
        KeyInfo = CommandBehavior.KeyInfo,
        SingleRow = CommandBehavior.SingleRow,
        SequentialAccess = CommandBehavior.SequentialAccess,
        CloseConnection = CommandBehavior.CloseConnection,
        //
        UseExecuteNoQuery = 0xFF612,
        UseExecuteScalar = 0xFF614,
    }
    #endregion
    //
    #region Status Constants
    /// <summary> 
    /// This is the general purpose state enumeration.
    /// It contains result type groups (via Mask items,)
    /// as well as individual result states.
    /// It is used by the file system, MVVC controller, 
    /// UI methods and application processing modules.
    /// </summary> 
    [Flags]
    public enum StateIs : long {
        None = 0x0000,
        MaskResult = 0xFFFF,
        MaskTask = 0xFF0000,
        MaskTaskNotStarted = 0X010000,
        MaskTaskOpen = 0x0E0000,
        MaskTaskClosed = 0XF00000,
        MaskError = 0xFF000000,
        MaskInvalid = 0xF000F000,
        MaskSuccessful = 0x00600110,
        MaskExistence = 0xF,
        MaskExistenceFailed = 0xE,
        MaskExistenceSuccessful = 0X1,
        MaskSuccessfulAll = MaskSuccessful | MaskExistenceSuccessful,
        MaskFailedAll = MaskError | MaskInvalid | MaskExistenceFailed,
        // Results
        // 1 Existence
        DoesExist = 0x1,
        DoesNotExist = 0x2,
        ShouldExist = 0x4,
        ShouldNotExist = 0x8,
        // 2 End State
        NormalEnd = 0x10,
        AbnormalEnd = 0x20,
        // 3 Validity
        Valid = 0x100,
        Invalid = 0x200,
        // 4 Result Sets & Other
        EmptyResult = 0x1000,
        EmptyValue = 0x4000,
        BadData = 0x2000,
        // This is Task or Status / Progress Tracking
        // 5 Task Open
        NotStarted = 0x10000,
        Started = 0x20000,
        InProgress = 0x40000,
        OnHold = 0x80000,
        //6 Task Closed
        Cancelled = 0x100000,
        Finished = 0x200000,
        Successful = 0x400000,
        Failed = 0x800000,
        // Errors
        // 7 Indeterminate Errors
        Undefined = 0x1000000, // Action
        NotSet = 0x2000000,
        UndefinedResult = 0x4000000,
        UnknownFailure = 0x8000000,
        // 8 Resource, Service & Cross-cutting Errors
        OsError = 0x10000000,
        TimedOut = 0x20000000,
        DatabaseError = 0x40000000,
        MissingName = 0x80000000
    }

    /// <summary> 
    /// Not currently used.  Note that StateIs is essentially
    /// filled for the first eight (8) bytes.
    /// </summary> 
    public enum StateOtherIs : long {
        // 9
        MdmNameCreationFailed = 0x1,
        // 10
        NameIsEmpty = 0x2,
        // 1 ??? separate byte ???
        ObjectPointerNotMatched = 0x100,
        ObjectPointerAlreadyExists = 0x200, // often not an error, intermediate state
        ObjectPointerDoesNotExist = 0x400
    }
    #endregion

    #region Console and Status Message Box
    /// <summary> 
    /// This is a basic general structure for containing
    /// Margins or Padding for user interface objects.
    /// </summary> 
    public struct MessageBoxPadding {
        public double dLeft;
        public double dTop;
        public double dRight;
        public double dBottom;
        //
        public MessageBoxPadding(
            double dL,
            double dT,
            double dR,
            double dB) {
            dLeft = dL;
            dRight = dR;
            dTop = dT;
            dBottom = dB;
        }
    }

    #region TextBoxManageDef structure
    /// <summary> 
    /// A basic general purpose for managing the
    /// size of text, combo or other UI elements that is
    /// used internally by page size adjustments methods.
    /// </summary> 
    public class TextBoxDim {
        // Max and Min per WPF
        public double Max; // Maximum Size as per WPF
        public double Min; // Minimum Size as per WPF
        // Desired is calculated and frequently overriden in the code.
        public double Desired; // Desired size ignoring maximums & minimums
        public double Actual; // Actual size in presentation container
        public double Current; // Working variable for calculations
        // Current is being used for actual at this time.
        public double High; // Maximum width in order to widen box
        public double Low; // Maximum width in order to narrow box
    }
    #endregion

    #region Mdm Color
    /// <summary> 
    /// Classes used to manage colors for ellipses and other
    /// user interface elements.  This will be expanded to be 
    /// used by any color scheme and style setters in WPF
    /// </summary> 
    public static class MdmEllipseColor {
        public static void ControlGet(ref Object Sender, ref System.Windows.Shapes.Ellipse EllipsePassed, int ColorOfBarPassed) {
            EllipsePassed.Fill = MdmColorDef.ControlGet(ColorOfBarPassed);
            EllipsePassed.InvalidateVisual();
            ((Page)Sender).InvalidateVisual();
        }
    }

    /// <summary> 
    /// Current list of basic colors.
    /// </summary> 
    public static class MdmColorDef {
        /// <summary> 
        /// Current list of basic colors used in the application.
        /// </summary> 
        [Flags]
        public enum Is : int {
            Green = 1,
            Blue = 2,
            Red = 3,
            Yellow = 4,
            LightBlue = 5,
            White = 6,
        }
        //
        /// <summary> 
        /// Uses the passed color to return a media brush color.
        /// </summary> 
        /// <param name="ColorOfBarPassed">Interger enumeration value indicating desired color.</param> 
        /// <returns>
        /// System.Windows.Media.Brush color.
        /// </returns>
        /// <remarks>
        /// </remarks> 
        public static System.Windows.Media.Brush ControlGet(int ColorOfBarPassed) {
            switch (ColorOfBarPassed) {
                case ((int)Is.Green):
                    return System.Windows.Media.Brushes.Green;
                case ((int)Is.Blue):
                    return System.Windows.Media.Brushes.Blue;
                case ((int)Is.Red):
                    return System.Windows.Media.Brushes.Red;
                case ((int)Is.Yellow):
                    return System.Windows.Media.Brushes.Yellow;
                case ((int)Is.White):
                    return System.Windows.Media.Brushes.White;
                default:
                case ((int)Is.LightBlue):
                    return System.Windows.Media.Brushes.LightBlue;
            }
            return System.Windows.Media.Brushes.LightBlue;
        }
    }
    #endregion

    #region TextBoxManageDef structure
    /// <summary>
    /// Text, combo or general purpose UI element management class.
    /// </summary> 
    /// <remarks></remarks> 
    public class TextBoxManageDef {
        public Object Sender;
        public TextBoxManageDef TextBoxManageObject;
        public TextBox BoxObject;
        // Text Displayed so far.
        public double DisplayCount;
        public double DisplayAdjustCount;
        public double DisplayAdjustCountMax;
        //
        // various control properties
        // TODO TextBoxManageDef TextBoxManageDef partially implemented:
        public double DisplayMaxChars;
        public double DisplayMaxCharsToKeep;
        public double DisplayMaxLines;
        public double DisplayMaxLinesToKeep;
        //
        public bool DisplayAddToTop;
        // Flag to scroll to bottom or top
        public bool ScrollDo;
        // Font
        // TODO TextBoxManageDef FONT HERE OR...
        // public Object StyleObject;
        // public Object TextBoxObject;
        //
        // Box Padding
        // NOTE TextBoxManageDef This should be loaded from WPF
        public double BoxWidthCurrent; // TODO TextBoxManageDef replace with Actual below ?
        public double BoxPosX;
        public double BoxPosY;
        public MessageBoxPadding BoxPadding;
        public MessageBoxPadding BoxPaddingAdditional;
        // public TextBoxDim BoxWidth;
        // Width
        public double WidthMax; // Maximum Size as per WPF
        public double WidthMin; // Minimum Size as per WPF
        // Desired is calculated and frequently overriden in the code.
        public double WidthDesired; // Desired size ignoring maximums & minimums
        public double WidthActual; // Actual size in presentation container
        public double WidthCurrent; // Working variable for calculations
        // NOTE TextBoxManageDef Current is being used for actual at this time.
        public double WidthHigh; // Maximum width in order to widen box
        public double WidthLow; // Maximum width in order to narrow box
        // Height
        // TODO TextBoxManageDef This is a presentation object properties structure:
        public double HeightMax;
        public double HeightMin; // Minimum Size as per WPF
        public double HeightDesired;
        public double HeightActual;
        public double HeightCurrent;
        public double HeightHigh;
        public double HeightLow;
        //
        /// <summary>
        /// Standard data clear method.
        /// </summary> 
        public void DataClear() {
            BoxObject = null;
            //
            DisplayCount = 0;
            DisplayAdjustCount = 0;
            DisplayAdjustCountMax = 15;
            DisplayAddToTop = false;
            //
            DisplayMaxChars = 10000;
            DisplayMaxCharsToKeep = 9000;
            DisplayMaxLines = 500;
            DisplayMaxLinesToKeep = 400;
            //
            ScrollDo = true;
            //
            BoxWidthCurrent = 0;
            // TODO TextBoxManageDef Currently not loading from WPF:
            BoxPadding = new MessageBoxPadding(195, 0, 0, 0);
            BoxPaddingAdditional = new MessageBoxPadding(0, 0, 10, 0);
            //
            WidthCurrent = 0;
            WidthHigh = 0;
            WidthLow = 0;
        }
        /// <summary>
        /// Constructor
        /// </summary> 
        public TextBoxManageDef() {
            Sender = this;
            TextBoxManageObject = this;
            DataClear();
        }
        /// <summary>
        /// Constructor creates a box management object for the pass UI element.
        /// </summary> 
        public TextBoxManageDef(TextBox BoxObjectPassed)
            : this() {
            BoxObject = BoxObjectPassed;
        }
    }
    #endregion
    #endregion

    #region Class InternalId properties
    /// <summary>
    /// Class to contain identification (introspection) information for an object.
    /// Identification is viewed in terms of Internal, External and Local scope.
    /// </summary> 
    public class InternalIdDef {
        private int ipId;
        public int Id { get { return ipId; } set { ipId = value; } }
        private String spName;
        public String Name { get { return spName; } set { spName = value; } }
        private String spTitle;
        public String Title { get { return spTitle; } set { spTitle = value; } }
        private int ipNumber;
        public int Number { get { return ipNumber; } set { ipNumber = value; } }
        private int ipStatus;
        public int Status { get { return ipStatus; } set { ipStatus = value; } }
        private String spStatusText;
        public String StatusText { get { return spStatusText; } set { spStatusText = value; } }
        private int ipIntResult;
        public int IntResult { get { return ipIntResult; } set { ipIntResult = value; } }
        private bool bpBoolResult;
        public bool BoolResult { get { return bpBoolResult; } set { bpBoolResult = value; } }

        public InternalIdDef() {
            ipId = 99999;
            spName = "unknown";
            spTitle = "unknown";
            ipNumber = 99999;
            ipStatus = 99999;
            spStatusText = "unknown";
            ipIntResult = 99999;
            bpBoolResult = false;
        }
    }
    #endregion
    #region Class ExternalId properties
    /// <summary>
    /// Class to contain identification (introspection) information for an object.
    /// Identification is viewed in terms of Internal, External and Local scope.
    /// </summary> 
    public class ExternalIdDef {
        private int ipId;
        public int Id { get { return ipId; } set { ipId = value; } }
        private String spName;
        public String Name { get { return spName; } set { spName = value; } }
        private String spTitle;
        public String Title { get { return spTitle; } set { spTitle = value; } }
        private int ipNumber;
        public int Number { get { return ipNumber; } set { ipNumber = value; } }
        private int ipStatus;
        public int Status { get { return ipStatus; } set { ipStatus = value; } }
        private String spStatusText;
        public String StatusText { get { return spStatusText; } set { spStatusText = value; } }
        private int ipIntResult;
        public int IntResult { get { return ipIntResult; } set { ipIntResult = value; } }
        private bool bpBoolResult;
        public bool BoolResult { get { return bpBoolResult; } set { bpBoolResult = value; } }
        public ExternalIdDef() {
            ipId = 99999;
            spName = "unknown";
            spTitle = "unknown";
            ipNumber = 99999;
            ipStatus = 99999;
            spStatusText = "unknown";
            ipIntResult = 99999;
            bpBoolResult = false;
        }
    }
    #endregion

    #region Class LocalId Result properties
    /// <summary>
    /// Class to contain identification (introspection) information for an object.
    /// Identification is viewed in terms of Internal, External and Local scope.
    /// </summary> 
    public class LocalIdDef {
        // Initialization
        public bool Started;
        public bool Running;
        // <Area Id = "CallResults">
        private String spProcessName;
        public String ProcessName { get { return spProcessName; } set { spProcessName = value; } }
        private String spClassName;
        public String ClassName { get { return spClassName; } set { spClassName = value; } }
        private String spPatternName;
        public String PatternName { get { return spPatternName; } set { spPatternName = value; } }
        // Area is refers to area within coding patern
        private String spAreaName; // such as init, main, loop, dispose, open, close, display
        public String AreaName { get { return spAreaName; } set { spAreaName = value; } }
        private String spMethodName;
        public String MethodName { get { return spMethodName; } set { spMethodName = value; } }
        private int ipIntResult;
        public int IntResult { get { return ipIntResult; } set { ipIntResult = value; } }
        private long ipLongResult;
        public long LongResult { get { return ipLongResult; } set { ipLongResult = value; } }
        private String isStringResult;
        public String StringResult { get { return isStringResult; } set { isStringResult = value; } }
        private bool bpBoolResult;
        public bool BoolResult { get { return bpBoolResult; } set { bpBoolResult = value; } }
        private Object opObjectResult;
        public Object ObjectResult { get { return opObjectResult; } set { opObjectResult = (Object)value; } }
        private bool bpObjectDoesExist;
        public bool ObjectDoesExist { get { return bpObjectDoesExist; } set { bpObjectDoesExist = value; } }
        public LocalIdDef() { }
    }
    #endregion
    //
    #region Class Local Messages
    /// <summary> 
    /// A class to contain a hierarchy or set of ten messages.
    /// </summary> 
    /// <remarks>
    /// This might be expanded (include) to be a standard list type
    /// in that the messages can be working values, a stack, each
    /// message belonging to a specific location or function, tied to
    /// a purporse, etc.
    /// </remarks> 
    public class LocalMessageDef {
        // Local Messages
        public String Msg;
        public String Msg0;
        public String Msg1;
        public String Msg2;
        public String Msg3;
        public String Msg4;
        public String Msg5;
        public String Msg6;
        public String Msg7;
        public String Msg8;
        public String Msg9;
        // TODO make these properties, ?? possibly routing ??
        public String Header;
        public String LogEntry;
        public String ErrorMsg;
        public LocalMessageDef() {
            ErrorMsg = "";
            Msg = "";
            Msg0 = "";
            Msg1 = "";
            Msg2 = "";
            Msg3 = "";
            Msg4 = "";
            Msg5 = "";
            Msg6 = "";
            Msg7 = "";
            Msg8 = "";
            Msg9 = "";
        }
    }
    #endregion

    #region Message Mdd To Page Arguments
    /// <summary>
    /// <para> This argument object is used to pass
    /// messages to the user interface.  It is not
    /// yet implemented and might be combined with
    /// the TraceMdm Message object.</para>
    /// <para> .</para>
    /// <para> Note that where possible, the
    /// method signature (Sender, Arguments)
    /// or (Sender, Object) will be employed.
    /// Arguments is either an aggregation of
    /// objects or a limitied part of the
    /// source object that the called method
    /// acts upon.</para>
    /// </summary>
    public class MessageMdmSendToPageArgs : EventArgs {
        public MessageMdmSendToPageArgs(String PassedMessageToPage) {
            MessageToPage = PassedMessageToPage;
        }
        #region Declarations
        private String spMessageToPage;
        public String MessageToPage {
            get { return spMessageToPage; }
            set { spMessageToPage = value; }
        }
        private bool spNewLine;
        public bool NewLine {
            get { return spNewLine; }
            set { spNewLine = value; }
        }
        private String spMessageFrom;
        public String MessageFrom {
            get { return spMessageFrom; }
            set { spMessageFrom = value; }
        }
        private String spMessageTo;
        public String MessageTo {
            get { return spMessageTo; }
            set { spMessageTo = value; }
        }
        private int spIndent;
        public int Indent {
            get { return spIndent; }
            set { spIndent = value; }
        }
        private int spErrorLevel;
        public int ErrorLevel {
            get { return spErrorLevel; }
            set { spErrorLevel = value; }
        }
        private String spRunAction;
        public String RunAction {
            get { return spRunAction; }
            set { spRunAction = value; }
        }
        private int spLine;
        public int Line {
            get { return spLine; }
            set { spLine = value; }
        }
        private int spColumn;
        public int Column {
            get { return spColumn; }
            set { spColumn = value; }
        }
        private String spEtc;
        public String Etc {
            get { return spEtc; }
            set { spEtc = value; }
        }
    }
        #endregion
    #endregion

    #region MessageDetailsDef
    /// <summary>
    /// <para> CLASS MDM MESSAGE</para> 
    /// <para> The message object common to the Trace, Logging, 
    /// Error and Messaging components.  This single object
    /// design is intended for single, multi-threaded, or event
    /// based implementation and might be passed as an Args
    /// object.</para>   
    /// <para> . </para>
    /// <para> This object includes features for user input, there
    /// are plans to implement (console) green screen features.</para> 
    /// <para> . </para>
    /// <para> It currently supports the prefixed message type
    /// marshalling used in multi-threaded user interface / 
    /// background worker scenarios and not all messages are
    /// bound for the user interface.</para> 
    /// </summary> 
    /// <remarks>
    /// TODO MessageDetailsDef InProgress Message Class: implement this soon and eliminate verbosity via defaulting strategy.
    /// not implemented yet
    /// </remarks> 
    public class MessageDetailsDef : DefStdBase {
        #region Declarations
        // Current Trace Parameters:
        public object Sender;
        //
        public long MessageId;
        public long ErrorId;
        public long ResponseId;
        //
        // Errors:
        public int Level;
        public int Source;
        // I assume this is a return result, HRESULT, exception
        // or legacy OS error code.
        public long ErrorCode;
        public double ErrorInnerExceptionId;
        //
        // Externally Set Control Fields: 
        // (Callback routing) (Embedded Fields in Text)
        public String StatusAction;
        public int StatusTarget;
        public int StatusSubTarget;
        public String UserState; // from StatusLineMessage.
        // TODO MessageDetailsDef StatusLine Handling will not be combined with Trace Messaging.
        //
        // Application Details: Numeric Location from app
        public int Location1;
        public int Location2;
        // Application Details: Return result
        public long MethodResult;
        // Basic Message type:
        public bool IsMessage;
        public bool IsError;
        // Type of response to ask for
        // (if any):
        public long ResponseFlags;
        // Logging vs. display:
        public bool DoDisplay;
        public int DisplayFlags;
        // Message Text:
        public String Text;
        #endregion
        /// <summary>
        /// Create a message object for use in the Trace, 
        /// Logging, Error handling and multi-threaded
        /// messaging components.  Includes Status Line, 
        /// Console and Completion Progress messages.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public MessageDetailsDef() {
            MessageId = 0;
            ErrorId = 0;
            ResponseId = 0;
            //
            ErrorCode = 0;
            ErrorInnerExceptionId = 0;
            // Current Trace Parameters:
            Sender = null;
            Location1 = iNoOp;
            Location2 = iNoOp;
            MethodResult = iNoMethodResult;
            IsError = bNO;
            Level = iNoErrorLevel;
            Source = iNoErrorSource;
            DoDisplay = bDoNotDisplay;
            DisplayFlags = MessageNoUserEntry;
            Text = "";
            //
        }
    }
    #endregion

    #region File Type Handling
    #region FileType Constants
    /// <summary> 
    /// To indicate the file data being processed is
    /// either Data or Dictionary (file schema) information.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    [Flags]
    public enum FileType_LevelIs : long {
        None = 0x00000000,
        // Dictionary / Data
        DictData = 0x00000001,
        Data = 0x00000002
    }

    /// <summary> 
    /// Major or Primary file type.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    [Flags]
    public enum FileType_Is : long {
        None = 0x0,
        // 3 Other
        Other = 0x100,
        // 3 System
        MaskSystem = 0xF00,
        SystemList = 0x200,
        SystemData = 0x400,
        // 4 Binary
        Binary = 0xF000,
        // 5 PICK
        PICK = 0xF0000,
        // 6 Text Data Types
        MaskTilde = 0x300000,
        Tilde = 0x100000,
        // x        = 0x200000,
        //
        MaskText = 0x400000,
        // x        = 0x800000,
        TEXT = 0xC00000,
        //
        // 7 MarkupMask Formants
        MaskMarkup = 0xF000000,
        // JSON standard
        JSON = 0x1000000,
        // HTML
        Html = 0x2000000,
        // XML
        XML = 0x3000000,
        // Database
        // 8 SQL-ish
        MaskDatabase = 0xF0000000,
        SQL = 0x10000000,
        DB2 = 0x20000000,
        ORACLE = 0x40000000,
        //
        // 8 Unknonw
        Unknown = 0xFF1,
        Undefined = 0xFF2,
        Undefined1 = 0xFF4
    }

    /// <summary> 
    /// Minor, Secondary or Sub File Type
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    [Flags]
    public enum FileType_SubTypeIs : long {
        None = (long)FileType_Is.None,
        // Ascii, Text
        TEXT = (long)FileType_Is.TEXT,
        //
        TEXTSTD = FileType_Is.TEXT | 0x1,
        // Delimited using standard FS US
        ASC = FileType_Is.TEXT | 0x2,
        // Fixed width columns per supplied definition
        FIX = FileType_Is.TEXT | 0x4,
        // not specified nor predictable
        DAT = FileType_Is.TEXT | 0x8,
        // CSV standard format
        CSV = FileType_Is.TEXT | 0x10,
        //
        // Markup Mask Formats
        MaskMarkup = (long)FileType_Is.MaskMarkup,
        // JSON standard
        JSON = (long)FileType_Is.JSON,
        // HTML
        MaskHtml = (long)FileType_Is.Html,
        Html30 = FileType_SubTypeIs.MaskHtml | 0x1,
        Html40 = FileType_SubTypeIs.MaskHtml | 0x2,
        Html50 = FileType_SubTypeIs.MaskHtml | 0x4,
        // Xml
        XML = (long)FileType_Is.XML,
        //
        // Database Formats Mask
        MaskDatabase = (long)FileType_Is.MaskDatabase,
        // Sql Mask
        // 92 standard syntax
        SQL = (long)FileType_Is.SQL | 0x1,
        // Ms Sql Server
        MS = FileType_SubTypeIs.SQL | 0x2,
        // My Sql Sun / Oracle
        MY = FileType_SubTypeIs.SQL | 0x4,
        //
        // IBM Db2
        DB2 = (long)FileType_Is.DB2,
        //
        // Oracle
        ORACLE = (long)FileType_Is.ORACLE,
        //
        // Tld Tilde Mask 5 F's to subtype tilde
        MaskTilde = (long)FileType_Is.MaskTilde,
        Tilde = FileType_SubTypeIs.MaskTilde | 0x1,
        Tilde_ROW = FileType_SubTypeIs.MaskTilde | 0x2,
        Tilde_Other = FileType_SubTypeIs.MaskTilde | 0x4,
        Tilde_Native = FileType_SubTypeIs.MaskTilde | 0x8,
        Tilde_Native_ONE = FileType_SubTypeIs.MaskTilde | 0x10,
        Tilde_CSV = FileType_SubTypeIs.MaskTilde | 0x20,
        //
        // System Mask
        MaskSystem = (long)FileType_Is.MaskSystem | 0x00000,
        //
        // System List Mask
        SystemList = FileType_SubTypeIs.MaskSystem | 0x200,
        // Cr delimited
        ItemList = FileType_SubTypeIs.SystemList | 0x1,
        // Single value (entire file)
        SingleValue = FileType_SubTypeIs.SystemList | 0x2,
        // Cr delimited
        LookupTable = FileType_SubTypeIs.SystemList | 0x4,
        //
        // System Data Mask
        SystemData = FileType_SubTypeIs.MaskSystem | 0x400,
        // Cr US delimited
        FileDictDef = FileType_SubTypeIs.SystemList | 0x1,
        // Cr US delimited
        PairedList = FileType_SubTypeIs.SystemList | 0x2,
        // Cr Colon delimited
        IniFile = FileType_SubTypeIs.SystemList | 0x4,
        //
        // Binary
        Binary = (long)FileType_Is.Binary,
        //
        // Unknown
        Unknown = (long)FileType_Is.Unknown,
        Undefined = (long)FileType_Is.Undefined,
        Undefined1 = (long)FileType_Is.Undefined1
    }
    #endregion

    #region File Type definitions
    /// <summary> 
    /// File Type Items contains default setting
    /// for processing files of each specific type.
    /// Predefined items constitute the default
    /// types and those most commonly processed
    /// by the system.
    /// </summary> 
    /// <remarks>
    /// </remarks> 

    #endregion

    #region Object Type definitions

    #endregion

    #region Data and Primitive Type Definitons
    public class TypeTableItemDef : DefStd {
        public static string Name;
        public static int Width;
        public static long Lo;
        public static ulong Hi;
        public static String Desc;
        public TypeTableItemDef() {
        }
        public TypeTableItemDef(
            string NamePassed,
            Dictionary<String, TypeTableItemDef> TypeDictPassed,
            int WidthPassed,
            long LoPassed,
            ulong HiPassed,
            String DescPassed
            ) {
            Name = NamePassed;
            Width = WidthPassed;
            // The low range of types that are part of this group
            Lo = LoPassed;
            // The high range of flag values that are included in this group.
            Hi = HiPassed;
            Desc = DescPassed;

            if (!TypeDictPassed.ContainsKey(Name)) {
                TypeDictPassed.Add(Name, this);
            }
        }
    }

    /// <summary>
    /// <para> A list of predefined primitive types used
    /// by the system.  It is mainly important to tranformation
    /// operations on data.</para>
    /// </summary>
    public class TypeTableList {
        public Dictionary<string, TypeTableItemDef> TypeDict = new Dictionary<string, TypeTableItemDef>();
        public TypeTableList() {
            //Type Range Size
            TypeTableItemDef TYPEsbyte = new TypeTableItemDef("sbyte", TypeDict, 3, -128, 127, "Signed 8-bit integer.");
            TypeTableItemDef TYPEbyte = new TypeTableItemDef("byte", TypeDict, 3, 0, 255, "Unsigned 8-bit integer.");
            TypeTableItemDef TYPEchar = new TypeTableItemDef("char", TypeDict, 4, 0x0000, 0xffff, "Unicode 16-bit character.");
            TypeTableItemDef TYPEshort = new TypeTableItemDef("short", TypeDict, 5, -32768, 32767, "Signed 16-bit integer.");
            TypeTableItemDef TYPEushort = new TypeTableItemDef("ushort", TypeDict, 5, 0, 65535, "Unsigned 16-bit integer.");
            TypeTableItemDef TYPEint = new TypeTableItemDef("int", TypeDict, 10, -2147483648, 2147483647, "Signed 32-bit integer.");
            TypeTableItemDef TYPEuint = new TypeTableItemDef("uint", TypeDict, 10, 0, 4294967295, "Unsigned 32-bit integer.");
            TypeTableItemDef TYPElong = new TypeTableItemDef("long", TypeDict, 19, -9223372036854775808, 9223372036854775807, "Signed 64-bit integer.");
            TypeTableItemDef Typeulong = new TypeTableItemDef("ulong", TypeDict, 10, 0, 18446744073709551615, "Unsigned 64-bit integer.");
            // If the value represented by an integer literal 
            // exceeds the range of ulong, a compilation error will occur.
        }
    }
    #endregion
    #endregion

    #region Maximums, Minimums, Char Table, Delimiters, Escaping, Ordering Constants

    // Array Sizes
    /// <summary> 
    /// The maximum number of columns allowed within the file system
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public enum ArrayMax : int {
        ColumnMax = 256,
        ColumnAliasMax = 1024
    }

    /// <summary> 
    /// This set of characters constitutes the special characters
    /// that are in use within all implemented classes.  The mainly
    /// consist of formating, file data separators and punctuation.
    /// There are three instances of delimiter character sets that
    /// are the three most commonly used delimited sets.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public class CharTable : DefStd {
        // Pick Delimeters
        public static String Sm = ((char)255).ToString(); // Segment
        public static String Fm = ((char)28).ToString(); // File Separator
        public static String Gm = ((char)29).ToString(); // Group Separator
        public static String Rm = ((char)30).ToString(); // Record Separator
        public static String Am = ((char)254).ToString(); // Attribute / Field / Column
        public static String Vm = ((char)253).ToString(); // Multivalue
        public static String Svm = ((char)252).ToString(); // Subvalue
        public static String Lvm = "*"; // MultiField Level 1
        public static String Lsvm = "@"; // MultiField Level 2

        public static String Ass = ((char)255).ToString(); // File Separator
        public static String Afs = ((char)28).ToString(); // File Separator
        public static String Ags = ((char)29).ToString(); // Group Separator
        public static String Ars = ((char)30).ToString(); // Record Separator
        public static String Aus = ((char)31).ToString(); // Unit Separtor / Column / Field - Level 1
        public static String Auss = ((char)256).ToString(); // Unit Separtor multivalue = Level 2
        public static String Ausss = "*"; // MultiField Level 3
        public static String Aussss = "@"; // MultiField Level 4

        public static String Trm = ""; // Trim Character

        public static StdDelimDef DelPickGet() {
            return new StdDelimDef(Sm, Fm, Rm, Am, Vm, Svm, Trm);
        }
        public static StdDelimDef DelStdGet() {
            return new StdDelimDef();
        }

        public static StdDelimDef DelAsciiGet() {
            return new StdDelimDef(Sm, Fm, Gm, Lf, ",", "|", Cr);
        }
    }

    /// <summary> 
    /// The Standard Delimiter definition defines a group
    /// of characters that are used to define record formats
    /// where each row, column, datum or key-value pair are
    /// separated by a special character.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public class StdDelimDef {
        public String Ss;
        public String Fs;
        public String Gs;
        public String Rs;
        public String Us;
        public String Uss;
        public String Usss;
        public String Ussss;
        public String Trm;
        //
        /// <summary> 
        /// Default set using the delimiter characters from
        /// the standard ASCII character set.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public StdDelimDef() {
            Ss = Mdm.Oss.Decl.CharTable.Ass;
            Fs = Mdm.Oss.Decl.CharTable.Afs;
            Gs = Mdm.Oss.Decl.CharTable.Ags;
            Rs = Mdm.Oss.Decl.CharTable.Ars;
            Uss = Mdm.Oss.Decl.CharTable.Aus;
            Usss = Mdm.Oss.Decl.CharTable.Ausss;
            Ussss = Mdm.Oss.Decl.CharTable.Aussss;
            Trm = Mdm.Oss.Decl.CharTable.Trm;
        }
        //
        /// <summary> 
        /// Create a delimiter character definition using
        /// the passed characters.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public StdDelimDef(
            String SPassed,
            String FPassed,
            String GPassed,
            String RPassed,
            String UPassed,
            String UsPassed,
            String TrmPassed
            ) {
            Ss = SPassed;
            Fs = FPassed;
            Gs = GPassed;
            Rs = RPassed;
            Us = UPassed;
            Uss = UsPassed;
            Trm = TrmPassed;
        }
    }

    /// <summary> 
    /// When string data is escaped this enumeration
    /// indicates the method used.  Escaped string contain
    /// marker character to indicate special content.  The
    /// most common being NewLines, Quotes and Slashes.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public enum ColEscapedIs : int {
        ColEscapedFORBINARY = 1,
        ColEscapedNEWLINE = 2,
        ColEscapedVstudioFormat = 3
    }

    /// <summary> 
    /// When string data is escaped this enumeration
    /// indicates what technique is used to handle 
    /// quotation marks.
    /// handled.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public enum ColQuoteIs : int {
        ColQuoteDOUBLE = 1,
        ColQuoteSINGLE = 2,
        ColQuoteBACKSLASH = 3,
        ColQuoteFORWARD = 4,
        ColQuoteBRACKETE = 5
    }

    /// <summary> 
    /// When string data is escaped this enumeration
    /// indicates which technique will be used to generate
    /// 7-bit (low order ASCII) text output.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public enum EscapedFormat : int {
        SlashedChar = 1,
        SlashedThreeDigit = 2,
        SlashedShiftInOut = 3
    }

    /// <summary> 
    /// Indicates how and if a column or data will be sorted.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public enum OrderIs : int {
        NotOrdered = 0x0000001,
        Ascending = 0x0000002,
        Descending = 0x0000003
    }
    #endregion
}