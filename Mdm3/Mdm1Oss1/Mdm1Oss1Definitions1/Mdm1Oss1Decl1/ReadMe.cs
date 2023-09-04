using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mdm.Oss.Decl
{
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
    /// that inherits from the StdDef assembly.  It in
    /// turn is inherited by the DefBaseRun layer above it.
    /// The Pick Console layer is in turn inherited by the
    /// Run File Console.  </para>
    /// <para> . </para>
    /// <para> Note re Pick virtualization: </para>
    /// <para> At the highest levels, the
    /// Pick database class inherits from the file system
    /// class (mFileDef.)</para>
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
}
