using System;
using Mdm.Oss.Decl;

namespace Mdm.Oss.Std
{
    /// <summary>
    /// This is the base class for Run Control, Trace and 
    /// Logging, Threading and MVVC library delegates.
    /// It includes the Messaging, Console and psuedo virtualization
    /// of syntax (PickSyntax) from the multivalued platforms.
    /// Run control is the most basic level of service to the
    /// applications and must be implemented to use Logging and 
    /// consoles.
    /// Next: StdBaseRunFileDef
    /// Top: StdConsoleManagerDef
    /// Specificity: StdBaseRunDef : StdBaseRunFilePrinterConsole : StdConsoleManagerDef
    /// </summary> 
    public partial class StdBaseRunDef : StdBasePickSyntaxDef, IDisposable
    {
        public StdBaseRunDef(ref object SenderPassed)
            : base(ref SenderPassed)
        { }
        public StdBaseRunDef(ref object SenderPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
            : base(ref SenderPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        { }
        public StdBaseRunDef(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
            : base(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        { }
        static StdBaseRunDef()
        { TraceLock = new object(); }
        /// <summary>
        /// ToDo
        /// </summary> 
        public StdBaseRunDef()
            :base()
        {
            //Sender = this;
            //SenderThisIs = this;
        }
        /// <summary>
        /// Recommended constructor indicating features.
        /// </summary> 
        public StdBaseRunDef(ref object SenderPassed, ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ref SenderPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        { }
        public virtual void InitializeStdBaseRun()
        {
            if (!ClassFeatureFlag.StdBaseRunDefDone)
            {
                ClassFeatureFlag.StdBaseRunDefDone = true;
                base.InitializeStdBasePickSyntax();
                if (!ClassFeatureFlag.InitializeTraceDone 
                    && (ClassFeatureFlag.MdmTraceIsUsed 
                    || ClassFeatureFlag.MdmConsoleIsUsed)) 
                    { InitializeTrace(); }
                if (!ClassFeatureFlag.InitializeConsoleDone && ClassFeatureFlag.MdmConsoleIsUsed) 
                    { InitializeConsole(); }
                if (!ClassFeatureFlag.InitializeRunDone && ClassFeatureFlag.MdmRunIsUsed) 
                    { InitializeRun(); }
            }
        }
        public virtual void InitializeTrace()
        {
            ClassFeatureFlag.InitializeTraceDone = true;
            
            DoLogActivity = false;
            DoLogActivityDefault = true;
            sProcessHeading = sEmpty;
            sProcessSubHeading = sEmpty;
            ConsoleVerbosity = 5;
            // ToDo Optimize this based on what gets used:
            #region ConsoleTraceMdm_Declarations
            // Tracing Detail
            TraceResult = StateIs.NotSet;
            //TraceDebugOn = bOFF;
            // TraceDebugOn = bON;
            //TraceBreakOnAll = bON;
            //TraceOn = bON;
            //TraceFirst = bYES;
            // Trace Attributes Processed
            TraceIterationCount = 0;
            TraceIterationCheckPointCount = 0;
            TraceIterationCountTotal = 0;
            TraceIterationCurrentDetail = 0;
            //
            TraceHeadings = bYES;
            TraceData = bNO;
            TraceDataPointers = sEmpty;
            TraceErrorMessage = sEmpty;
            // Trace Iteration Control
            // threshold to stop displaying full detail on iteration count
            // stop detail display after number of iterations
            //TraceIteration = bOFF;
            //TraceIterationOnNow = bOFF;
            //TraceIterationInitialState = bOFF;
            //TraceIterationRepeat = bOFF;
            // threshold for pause on iteration count
            // this is a check point to interact with user
            TraceIterationCheckPointOn = bOFF;
            TraceIterationCheckPoint = 500;
            // Use 0 to trace initialization, 1 to start at details
            TraceIterationThreshold = 6180;
            TraceIterationOnForCount = 100;
            TraceIterationOnForWarningGiven = bNO;
            TraceIterationOnAgainCount = 200;
            // Trace Lines Displayed Control
            TraceDisplayCount = 0;
            TraceDisplayCheckPointCount = 0;
            TraceDisplayCountTotal = 0;
            //
            TraceCharacterCount = 0;
            //
            TracePercentCompleted = 0;
            ////
            //TraceDisplay = bOFF;
            //TraceDisplayOnNow = bOFF;
            //TraceDisplayInitialState = bOFF;
            //TraceDisplayRepeat = bOFF;
            // threshold for pause on number of messages
            // that could have been displayed
            TraceDisplayCheckPointOn = bOFF;
            TraceDisplayCheckPoint = 5000;
            //
            TraceDisplayThreshold = 200;
            TraceDisplayOnForCount = 5;
            TraceDisplayOnAgainCount = 200;
            //
            // Bug in fields or areas Control
            TraceBugCount = 0;
            TraceBugCheckPointCount = 0;
            TraceBugCountTotal = 0;
            //
            TraceBug = bOFF;
            TraceBugOnNow = bOFF;
            TraceBugInitialState = bOFF;
            TraceBugRepeat = bOFF;
            // After Threshold line display details
            // for OnForCount lines then return to
            // summary mode.  After another OnAgainCount
            // lines repeate display OnForCount.
            TraceBugCheckPointOn = bOFF;
            TraceBugCheckPoint = 10;
            //
            TraceBugThreshold = 2000;
            TraceBugOnForCount = 1;
            TraceBugOnAgainCount = 200;
            //
            TraceDisplayMessageDetailOn = false;

            TraceDisplayMessageDetail = sEmpty;
            TraceMessage = sEmpty;
            TraceMessageTarget = sEmpty;
            TraceMessagePrefix = sEmpty;
            TraceMessageSuffix = sEmpty;
            TraceMessageFormated = sEmpty;
            TraceMessageToPrint = sEmpty;
            TraceTemp = sEmpty;
            TraceTemp1 = sEmpty;

            LocalUserEntry = sEmpty;
            LocalUserEntryLong = 0;

            TraceMessageBlockString = sEmpty;
            TraceMessageBlock = sEmpty;

            TraceByteCountTotal = 0;
            TraceByteCount = 0;
            TraceShiftIndexByCount = 100;
            #endregion
            #region ConsoleMessageTarget
            MessageStatusTargetText = sEmpty;
            MessageStatusSubTarget = 0;
            MessageStatusSubTargetDouble = 0;
            //
            ProcessStatusTarget = 0;
            ProcessStatusSubTarget = 0;
            ProcessStatusTargetDouble = 0;
            //
            ProcessStatusTargetState = 0;
            #endregion
            #region ConsoleProgressBar
            ProgressBarMdm1Property = 0;
            MessageProperty2 = 0;
            #endregion
            #region ConsoleTextMessageOutput
            sMessageText = sEmpty;
            sMessageText0 = sEmpty;
            MessageTextOutConsole = sEmpty;
            MessageTextOutStatusLine = sEmpty;
            MessageTextOutProgress = sEmpty;
            MessageTextOutError = sEmpty;
            MessageTextOutRunAction = sEmpty;
            //
            MessageStatusAction = sEmpty;
            ProcessStatusAction = sEmpty;
            // ? MessageStatusError = false;
            #endregion
            ConsoleOutputLog = sEmpty;
            MessageLevelLast = 0;
            CommandLineRequestResult = 0;
            zMdmCommandId = iUnknown;
            #region Command properties
            zMdmCommandId = iUnknown;
            zMdmCommandName = sUnknown;
            zMdmCommandTitle = sUnknown;
            zMdmCommandNumber = iUnknown;
            zMdmCommandStatus = StateIs.NotSet;
            zMdmCommandStatusText = sUnknown;
            zMdmCommandIntResult = StateIs.NotSet;
            zMdmCommandBoolResult = false;
            #endregion
            #region Console properties
            zConsoleMdmId = iUnknown;
            zConsoleMdmName = sUnknown;
            zConsoleMdmTitle = sUnknown;
            zConsoleMdmNumber = iUnknown;
            zConsoleMdmStatus = StateIs.NotSet;
            zConsoleMdmStatusText = sUnknown;
            zConsoleMdmIntResult = StateIs.NotSet;
            zConsoleMdmBoolResult = false;
            #endregion
        }
        public void Clear()
        {
            ClassFeatureFlag = new ClassFeatureFlagDef();
            this.InitializeStdBaseRun();
        }
        public new void Dispose()
        {
            base.Dispose();
            // Dispoase ToDo
        }
        #region Class Naming
        public String sProcessHeading;
        public String sProcessSubHeading;
        #endregion
        #region Introspection
        #region Introspection Data
        // Introspection
        protected StateIs AppCoreObjectCheckStatus;
        protected StateIs AppCoreObjectGetStatus;
        protected StateIs AppCoreObjectSetStatus;
        protected StateIs AppCoreObjectResetStatus;

        protected StateIs AppIoObjectGetStatus;
        protected StateIs AppIoObjectSetStatus;
        protected StateIs AppmFileDefObjectGetStatus;
        protected StateIs AppmFileDefObjectSetStatus;

        protected String MethodObjectType;
        protected System.Type odstMethodObjectType;
        protected int MethodObjectHashCode;
        protected String MethodObjectToString;
        protected bool MethodObjectEquality;
        protected bool MethodObjectTypeValid;
        protected bool MethodObjectExternalExistance;
        protected bool MethodObjectInternalExistance;
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
        public virtual StateIs AppCoreObjectCheck(Object ooPassedObject, Object ooPassedInternalObject, String PassedObjectType)
        {
            AppCoreObjectCheckStatus = StateIs.Started;
            //
            try
            {
                odstMethodObjectType = ooPassedObject.GetType();
                MethodObjectHashCode = ooPassedObject.GetHashCode();
                MethodObjectToString = ooPassedObject.ToString();
                MethodObjectEquality = true;
                // 
                // 
                if (ooPassedObject != null)
                {
                    MethodObjectExternalExistance = true;
                    if (ooPassedInternalObject == null)
                    {
                        ooPassedInternalObject = ooPassedObject;
                    }
                    else
                    {
                        if (ooPassedInternalObject == ooPassedObject)
                        {
                            MethodObjectEquality = true;
                            MethodObjectTypeValid = true;
                        }
                        else
                        {
                            MethodObjectEquality = false;
                            MethodObjectTypeValid = false;
                        }
                    }
                }
                else
                {
                    MethodObjectExternalExistance = false;
                    MethodObjectEquality = false;
                    if (ooPassedInternalObject == null)
                    {
                        MethodObjectInternalExistance = false;
                    }
                    else
                    {
                        MethodObjectInternalExistance = true;
                        MethodObjectTypeValid = true;
                    }
                }
            }
            catch (SystemException omveValidationException)
            {
                //
                MethodObjectTypeValid = false;
                //
            }
            finally
            {
                // ToDo z$OPTIONAL AppCoreObjectCheck Message to page here.
                if (ooPassedInternalObject != null)
                {
                    MethodObjectInternalExistance = true;
                    MethodObjectToString = ooPassedInternalObject.ToString();
                    odstMethodObjectType = ooPassedInternalObject.GetType();
                    MethodObjectType = odstMethodObjectType.ToString();
                    if (PassedObjectType != null)
                    {
                        if (PassedObjectType.Length > 0)
                        {
                            if (MethodObjectType != PassedObjectType)
                            {
                                MethodObjectTypeValid = false;
                            }
                            else { MethodObjectTypeValid = true; };
                        }
                        else { MethodObjectTypeValid = true; };
                    }
                    else
                    {
                        MethodObjectInternalExistance = false;
                    }
                }
                else
                {
                    MethodObjectInternalExistance = false;
                }
            }
            return AppCoreObjectCheckStatus;
        }
        #endregion
    }
}
