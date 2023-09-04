using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mdm.Oss.Std
{
    public partial class StdBaseRunDef 
    {
        #region General Status Validation
        /// <summary> 
        /// Does the result indicate a valid state.
        /// </summary> 
        public bool StateIsSuccessful(StateIs StatePassed)
        {
            return (((long)StatePassed & (long)(StateIs.MaskSuccessful)) > 0);
            //return ((StatePassed & (long)(
            //    StateIs.Finished
            //    | StateIs.Successful
            //    | StateIs.NormalEnd
            //    | StateIs.Valid
            //    )) != 0);
            //|| StatePassed == StateIs.ShouldNotExist
            //|| StatePassed == StateIs.ShouldExist
            //|| StatePassed == StateIs.DoesNotExist
            //
            //|| StatePassed == StateIs.Undefined
            //|| StatePassed == FileAction_Do.NotSet
            //|| StatePassed == FileAction_Do.UseDefault
            //|| StatePassed == StateIs.UndefinedResult
        }
        public bool StateIsSuccessful(long StatePassed)
        {
            return StateIsSuccessful((StateIs)StatePassed);
        }
        // State returned as Enum
        public bool StateIsSuccessfulAll(StateIs StatePassed)
        {
            return (((long)StatePassed & (long)StateIs.MaskSuccessfulAll) > 0);
        }
        public bool StateIsSuccessfulAll(long StatePassed)
        {
            return StateIsSuccessfulAll((StateIs)StatePassed);

            // return (((long)StatePassed & (long)(StateIs.MaskSuccessfulAll)) > 0);
        }
        /// <summary> 
        /// Does the result indicate an invalid state.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public bool StateIsInvalid(long StatePassed)
        {
            return ((StatePassed & (long)(StateIs.MaskInvalid)) > 0);
            //return ((StatePassed & (long)(StateIs.AbnormalEnd
            //        | StateIs.Undefined
            //        | StateIs.UndefinedResult
            //        | StateIs.UnknownFailure
            //        | StateIs.EmptyResult
            //        | StateIs.EmptyValue
            //        | StateIs.MissingName
            //        | StateIs.None
            //        | StateIs.NotSet
            //        )) != 0);
        }

        /// <summary> 
        /// Is the result a value that deals with existence.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public bool StateIsExistence(long StatePassed)
        {
            return ((StatePassed & (long)(StateIs.MaskExistence)) > 0);
            //return ((StatePassed & (long)(
            //        StateIs.DoesExist
            //        | StateIs.ShouldNotExist
            //        | StateIs.ShouldExist
            //        | StateIs.DoesNotExist
            //        )) != 0);
        }

        /// <summary> 
        /// Does the result indicate a failed test for existence.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public bool StateIsExistenceFailed(long StatePassed)
        {
            return ((StatePassed & (long)(StateIs.MaskExistenceFailed)) > 0);
            //return ((StatePassed & (long)(
            //        StateIs.ShouldNotExist
            //        | StateIs.ShouldExist
            //        | StateIs.DoesNotExist
            //        )) != 0);
        }

        /// <summary> 
        /// Does the result indicate a successful test for existence.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public bool StateIsExistenceSuccessful(StateIs StatePassed)
        {
            return (((long)StatePassed & (long)(StateIs.MaskExistenceSuccessful)) > 0);
            //return ((StatePassed & (long)(
            //        StateIs.DoesExist
            //        )) != 0);
        }

        /// <summary> 
        /// Is the result a completion status value.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public bool StateIsTask(long StatePassed)
        {
            return ((StatePassed & (long)(StateIs.MaskTask)) > 0);
            //return ((StatePassed & (long)(
            //        StateIs.NotStarted
            //        | StateIs.Started
            //        | StateIs.InProgress
            //        | StateIs.OnHold
            //        | StateIs.Cancelled
            //        | StateIs.Finished
            //        | StateIs.Failed
            //        | StateIs.Successful
            //        )) != 0);
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
        public bool StateIsError(long StatePassed)
        {
            return ((StatePassed & (long)(StateIs.MaskError)) > 0);
            //return ((StatePassed & (long)(
            //    StateIs.AbnormalEnd
            //    | StateIs.BadData
            //    | StateIs.Cancelled
            //    | StateIs.DatabaseError
            //    | StateIs.EmptyResult
            //    | StateIs.EmptyValue
            //    | StateIs.Failed
            //    | StateIs.InProgress
            //    | StateIs.MissingName
            //    | StateIs.NotSet
            //    | StateIs.NotStarted
            //    | StateIs.OsError
            //    | StateIs.TimedOut
            //    | StateIs.Undefined
            //    | StateIs.UndefinedResult
            //    | StateIs.UnknownFailure
            //        )) != 0);
        }

        /// <summary>
        /// This function generates friendly Descriptions for states
        /// suitable for use in the user interface, logging or error messages.
        /// The Description is in the form of a present tense phrase with
        /// no leading or trailing spaces that can be inserted into other text.
        /// </summary> 
        /// <param name="ResultPassed">The state result to be described.</param> 
        /// <returns>
        /// A string describing the result state.
        /// </returns>
        /// <remarks>
        /// </remarks> 
        public String StateDescriptionGet(StateIs ResultPassed)
        {
            String StateDescription = sEmpty;
            switch (ResultPassed)
            {
                case (StateIs.AbnormalEnd):
                    StateDescription = "abnormal end of operation";
                    break;
                case (StateIs.BadData):
                    StateDescription = "bad data was encountered";
                    break;
                case (StateIs.Cancelled):
                    StateDescription = "operation Cancelled";
                    break;
                case (StateIs.DoesExist):
                    StateDescription = "item already exists";
                    break;
                case (StateIs.DoesNotExist):
                    StateDescription = "item not found";
                    break;
                case (StateIs.DatabaseError):
                    StateDescription = "database error";
                    break;
                case (StateIs.EmptyResult):
                    StateDescription = "empty result occured";
                    break;
                case (StateIs.Failed):
                    StateDescription = "operation Failed";
                    break;
                case (StateIs.Finished):
                    StateDescription = "operation is Finished";
                    break;
                case (StateIs.InProgress):
                    StateDescription = "operation is InProgress";
                    break;
                case (StateIs.Invalid):
                    StateDescription = "invalid result";
                    break;
                case (StateIs.MaskError):
                case (StateIs.MaskFailedAll):
                case (StateIs.MaskExistence):
                case (StateIs.MaskExistenceFailed):
                // case (StateIs.MaskExistenceSuccessful):
                case (StateIs.MaskInvalid):
                case (StateIs.MaskResult):
                case (StateIs.MaskSuccessful):
                case (StateIs.MaskSuccessfulAll):
                case (StateIs.MaskTask):
                case (StateIs.MaskTaskOpen):
                case (StateIs.MaskTaskClosed):
                    StateDescription = "unexpected masking encountered";
                    break;
                case (StateIs.MissingName):
                    StateDescription = "name or value missing in operation";
                    break;
                case (StateIs.None):
                    StateDescription = "no result was set";
                    break;
                case (StateIs.NormalEnd):
                    StateDescription = "normal end of operation";
                    break;
                case (StateIs.NotSet):
                    StateDescription = "expected value not set in operation";
                    break;
                case (StateIs.NotStarted):
                    StateDescription = "operation is NotStarted";
                    break;
                case (StateIs.OnHold):
                    StateDescription = "opation is OnHold";
                    break;
                case (StateIs.OsError):
                    StateDescription = "operating system error";
                    break;
                case (StateIs.ShouldExist):
                    StateDescription = "item should exist but does not";
                    break;
                case (StateIs.ShouldNotExist):
                    StateDescription = "item should not exist but does";
                    break;
                case (StateIs.Started):
                    StateDescription = "operation is Started";
                    break;
                case (StateIs.Successful):
                    StateDescription = "operation was Successful";
                    break;
                case (StateIs.TimedOut):
                    StateDescription = "timed out";
                    break;
                case (StateIs.Undefined):
                    StateDescription = "state not currently defined";
                    break;
                case (StateIs.UndefinedResult):
                    StateDescription = "operation result is undefined";
                    break;
                case (StateIs.UnknownFailure):
                    StateDescription = "operation had an unknown failure";
                    break;
                case (StateIs.Valid):
                    StateDescription = "result of operation was valid";
                    break;
                default:
                    StateDescription = "unknown error value:" + " (" + ResultPassed + ")";
                    break;
            }
            return StateDescription;
        }
        #endregion
        public StateIs ResultLocal;
        public StateIs StateILocal;
        /// <summary>
        /// This function generates friendly Descriptions for states
        /// suitable for use in the user interface, logging or error messages.
        /// The Description is in the form of a present tense phrase with
        /// no leading or trailing spaces that can be inserted into other text.
        /// </summary> 
        /// <param name="ResultPassed">The state result to be described.</param> 
        /// <returns>
        /// A string describing the result state.
        /// </returns>
        /// <remarks>
        /// </remarks> 
        public StateIs ConvertLongToStateIs(long ResultPassed)
        {
            String StateDescription = sEmpty;
            StateILocal = (StateIs)ResultPassed;
            return StateILocal;
        }
        public StateIs ConvertStateIsToLong(StateIs StateIsPassed)
        {
            String StateDescription = sEmpty;
            ResultLocal = StateIsPassed;
            return ResultLocal;
        }
    }

    #region Status Constants
    /// <summary> 
    /// This is the general purpose state enumeration.
    /// It contains result type groups (via Mask items,)
    /// as well as individual result states.
    /// It is used by the file system, MVVC controller, 
    /// UI methods and application processing modules.
    /// </summary> 
    [Flags]
    public enum StateIs : UInt64
    {
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
        MaskExistenceSuccessful = 0x1,
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
        // 2.4 Validity
        Valid = 0x40,
        Invalid = 0x80,
        // 3 Selection
        Skipped = 0x100,
        Selected = 0x200,
        // 3.4 Default file loop
        TryFirst = 0x410,
        TryAgain = 0x420,
        TryAll = 0x440,

        // 3.8 Code and Script Errors
        ProgramInvalid = 0x810,
        NotSupported = 0x820,
        ScriptInvalid = 0x840,

        // 4 Result Sets & Other
        EmptyResult = 0x1000,
        BadData = 0x2000,
        EmptyValue = 0x4000,
        Initialized = 0x8000,

        // This is Task or Status / Progress Tracking
        // 5 Task Open
        NotStarted = 0x10000,
        NotReady = 0x10010,
        Started = 0x20000,
        Ready = 0x20010,
        OnHold = 0x80000,

        //6 Task Closed
        Finished = 0x100000,
        Successful = 0x200000,
        Failed = 0x800000,

        // Errors
        // 7 Indeterminate Errors
        Undefined = 0x1000000, // Action
        NotSet = 0x2000000,
        UndefinedResult = 0x4000000,
        UnknownFailure = 0x8000000,

        // 8 Resource, Service & Cross-cutting Errors
        OsError =  0x10000000,
        Exception = 0x20000000,
        DatabaseError = 0x40000000,
        MissingName =   0x80000000,

        // 9 Actions
        Cancelled = 0x200000000,
        Deleted = 0x400000000,

        // 10 xxx
        Replaced = 0x1000000000,
        Updated =  0x2000000000,
        Created =  0x4000000000,
        // 11 xxx
        NullValue = 0x10000000000,
        // 12 xxx
        Waiting = 0x100000000000,
        InProgress = 0x200000000000,
        TimedOut = 0x400000000000,


        // ToDo Rationalize these extensions.
    }
    public enum DataStatusIs
    {
        Valid,
        Invalid,
        InvalidNewValid,
        InvalidNewInValid,
        New,
        NewValid,
        Functioned,
        Added,
        Inserted,
        Created,
        Replaced,
        Updated,
        Deleted
    }
    /// <summary> 
    /// Not currently used.  Note that StateIs is essentially
    /// filled for the first eight (8) bytes.
    /// </summary> 
    public enum StateOtherIs : long
    {
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
}
