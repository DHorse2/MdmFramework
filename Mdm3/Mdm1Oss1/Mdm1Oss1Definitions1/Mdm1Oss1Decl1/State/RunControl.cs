using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Forms;
using Mdm.Oss.Components;
using Mdm.Oss.Decl;
using Mdm.Oss.Std;
#region Mdm WinUtil, System Shell32, WshRuntime
using Mdm.Oss.WinUtil;
//// Project > Add Reference > 
//// add shell32.dll reference
//// (new) possibly interop.Shell32 & interop.IWshRuntimeLibrary
//// > COM > Microsoft Shell Controls and Automation
//using Shell32;
//// > COM > Windows Script Host Object Model.
//using IWshRuntimeLibrary;
#endregion

namespace Mdm.Oss.Std
{

    public partial class StdBaseRunDef
    {
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
        public static int RunTense_Max;
        public int RunTense;
        public const int RunTense_Off = 0;
        public const int RunTense_Do = 1;
        public const int RunTense_Doing = 2;
        public const int RunTense_Did = 3;
        public const int RunTense_Done = 4;
        public const int RunTense_On = 5;
        public const int RunTense_DidNot = 8;
        public const int RunTense_DoNot = 9;
        // Metrics and State
        public static int RunMetric_Max;
        public int RunMetric;
        public const int RunState = 1;
        public const int RunState_Last_Update = 2;
        public const int RunDoLast_Count = 3;
        public const int RunDoCount = 4;
        public const int RunDoSkip_Count = 5;
        public const int RunDoError_Count = 6;
        public const int RunDoWarning_Count = 7;
        public const int RunDoRetry_Count = 8;
        public int RunMetricOrStateX;
        public int RunMetricOrStateY;
        public int RunMetricOrStateZ;
        public int RunMetricOrState1;
        public int RunMetricOrState2;
        //
        // State, Category, Location or Action
        public static int RunAction_Max;
        public int RunAction;
        public int RunActionRequest;
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
        public int RunActionOrOptionX;
        public int RunActionOrOptionY;
        public int RunActionOrOptionZ;
        public int RunActionOrOption1;
        public int RunActionOrOption2;
        //
        //
        public int[,] RunActionState;
        //
        #endregion
        #region Progress Change
        //public ProgressChangedEventArgs RunActionProgressChangeEventArgs = new ProgressChangedEventArgs((int)0, sEmpty);
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
        public int RunActionStateProp
        {
            get { return RunActionState[RunAction, RunState]; }
            set
            {
                RunTense = value;
                RunActionState[RunAction, RunState] = RunTense;
                //RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(
                //    (int)LocalProgressBar_Value,
                //    "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString()
                //    // + LocalMessage.Msg
                //    );
                //if (ThreadUiProgressAsync != null)
                //{
                //    ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs);
                //}
                //else {
                //    ExceptDelegate(
                //        "ThreadUiProgressAsync delegate is not set!",
                //        StateIs.DoesNotExist);
                //}
            }
        }
        #endregion
        #region Description fields for Run Action and Metrics Fields
        public String[] RunActionVerb;
        public String[] RunActionDoing;
        public String[] RunActionDid;
        #endregion
        public virtual void InitializeRun()
        {
            ClassFeatureFlag.InitializeRunDone = true;
            #region High level Run Control
            RunControlOn = bON;
            RunCount = 0;
            RunDebugCount = 0;
            RunReloopIsOn = false; // init
            RunFirstIsOn = true; // init
            //
            RunStartPending = bNO; // init
            RunPausePending = bNO; // init
            RunCancelPending = bNO; // init
            #endregion
            #region Run Errors
            RunAbortIsOn = bNO;
            //
            RunErrorDidOccur = false;
            RunErrorDidOccurOnce = false;
            //
            RunErrorNumber = 0;
            RunGlobalErrorNumber = iUnknown;
            RunThrowException = iUnknown;
            RunShellErrorNumber = iUnknown;
            RunErrorCount = 0;
            #endregion
            #region Local Run Action Pause
            AppActionWaitMilliIncrement = 500;
            AppActionWaitMilliIncrementMax = 30000;
            AppActionWaitContinue = true;
            AppActionWaitCounter = 0;
            #endregion
            #region Local Progress Bar
            //ProgressBar LocalProgressBar;
            LocalProgressBar_Minimum = 0;
            LocalProgressBar_Maximum = 0;
            LocalProgressBar_Value = 0;
            LocalProgressBar_Display = 0;
            //
            LocalOldValue = 0;
            LocalNewValue = 0;
            #endregion
            #region Run Action and Metrics Fields - Array and Friendly names
            // The Verb, Temporal or State component
            RunTense_Max = 9;
            RunTense = 0;
            // Metrics and State
            RunMetric_Max = 8;
            RunMetric = 0;
            RunMetricOrStateX = RunMetric_Max + 1;
            RunMetricOrStateY = RunMetric_Max + 2;
            RunMetricOrStateZ = RunMetric_Max + 3;
            RunMetricOrState1 = RunMetric_Max + 4;
            RunMetricOrState2 = RunMetric_Max + 5;
            //
            // State, Category, Location or Action
            RunAction_Max = 25;
            RunAction = 0;
            RunActionRequest = 0;
            // User actions, options, and misc. array storage
            RunActionOrOptionX = RunAction_Max + 1;
            RunActionOrOptionY = RunAction_Max + 2;
            RunActionOrOptionZ = RunAction_Max + 3;
            RunActionOrOption1 = RunAction_Max + 4;
            RunActionOrOption2 = RunAction_Max + 5;
            //
            //
            RunActionState = new int[RunAction_Max + 5, RunMetric_Max + 5];
            //
            #endregion
            #region Description fields for Run Action and Metrics Fields
            RunActionVerb = new string[] { "NoOp",
                                               "Cancel", "Pause", "Started", "Resume", "NoOp5",
                                               "Initialize", "Do", "UserInput", "Open", "DoMain",
                                               "Select", "Lock", "Read", "Validate", "Accept",
                                               "Report", "Process", "Update", "Write", "UnLock",
                                               "Finish", "Abort",
                                               "OptionX", "OptionY", "OptionZ", "VerbY", "VerbZ"
                                               };
            RunActionDoing = new string[] { "NoOping",
                                                "Cancelling", "Pausing", "Starting", "NoOp4", "NoOp5",

                                               "Initialize", "Doing", "UserInputing", "Opening", "DoingMain",
                                               "Selecting", "Locking", "Reading", "Validating", "Accepting",
                                               "Reporting", "Processing", "Updating", "Writing", "UnLocking",
                                               "Finishing", "Abortint",
                                               "OptionXing", "OptionYing", "OptionZing", "VerbYing", "VerbZing"
                                                };
            RunActionDid = new string[] { "NoOped",
                                              "Cancelled", "Paused", "Started", "NoOp4", "NoOp5",
                                               "Initialized", "Did", "UserInputed", "Opened", "DoMained",
                                               "Selected", "Locked", "Read", "Validated", "Accepted",
                                               "Reported", "Processed", "Updated", "Writen", "UnLocked",
                                               "Finished", "Aborted",
                                               "OptionXed", "OptionYed", "OptionZed", "VerbYed", "VerbZed"
                                              };
            //
            #endregion
        }
        #endregion
        // xxxxxxxx Run Action Declarations - Misc, UserState, Errors, Iteration, 
        #region Run Action State
        #region Run State Fields and Flags.  Misc and working.
        // <Area Id = "PrimaryActions">
        public String spRunOptions;
        public String RunOptions { get { return spRunOptions; } set { spRunOptions = value; } }
        //public int ipRunStatus; // See StdDef
        //public int RunStatus { get { return ipRunStatus; } set { ipRunStatus = value; } }
        //
        // High level command
        public String FileRunRequest;
        public StateIs FileRunResult;
        // File Level command
        public String FileDoRequest;
        public StateIs FileDoResult;
        // <Area Id = "RunStatusControlItFlags">
        public int RunCount;
        public int RunDebugCount;
        public bool RunReloopIsOn; // init
        public bool RunFirstIsOn; // init
        //
        public bool RunStartPending; // init
        public bool RunPausePending; // init
        public bool RunCancelPending; // init
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
        public bool RunAbortIsOn;
        //
        public bool RunErrorDidOccur;
        public bool RunErrorDidOccurOnce;
        //
        public int RunErrorNumber;
        public int RunGlobalErrorNumber;
        public int RunThrowException;
        public int RunShellErrorNumber;
        public int RunErrorCount;
        #endregion
        #region Run Processing Loop Iteration
        #region Iteration properties
        // <Area Id = "IterationStatusControlItFlags">
        public int IterationCount;
        public int IterationRemaider;
        public int IterationDebugCount;
        public bool IterationAbort;
        public bool IterationReloop;
        public bool IterationFirst;
        public int IterationLoopCounter;
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
        //public virtual void RunActionExtractCommands(ref Object Sender, ProgressChangedEventArgs ePcea, StdBaseRunDef omHPassed)
        public virtual void RunActionExtractCommands(ref Object Sender, StdBaseRunDef omHPassed)
        {
            // ref Object PassedObject
            // StdBaseRunDef omHPassed 
            // This code handles both the Page UI command request and call backs from BgWorker
            // StdBaseRunDef omHPassed = (StdBaseRunDef)PassedObject;
            try
            {
                //UserState = (String)ePcea.UserState;
            }
            catch { UserState = sEmpty; }
            UserCommandPrefix = sEmpty;
            UserCommand = sEmpty;
            if (UserState.Length > 0) { UserCommandPrefix = UserState.Substring(0, 1); }
            if (UserState.Length > 1) { UserCommand = UserState.Substring(1); }
            LocalMessage.Msg = UserCommand;
            if (UserCommandPrefix == "$")
            {
                //if (ThreadUiProgressAsync == null)
                //{
                //    ExceptDelegate("FATAL ERROR: ThreadUiProgressAsync delegate is not set!",
                //        StateIs.DoesNotExist);
                //}
                if (UserCommand == "Started")
                {
                    if (omHPassed.RunActionState[RunRunDo, RunState] != RunTense_Did
                        && omHPassed.RunActionState[RunRunDo, RunState] != RunTense_Doing
                        )
                    {
                        RunStartPending = bYES;
                        omHPassed.RunActionState[RunAbort, RunState] = iNO;
                        omHPassed.RunActionState[RunFirst, RunState] = iYES;
                        omHPassed.RunActionState[RunReloop, RunState] = iNO;
                        RunAction = RunRunDo;
                        RunMetric = RunState;
                        RunTense = RunTense_Do;
                        omHPassed.RunActionState[RunRunDo, RunState] = RunTense_Do;
                        //    omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                        //"R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                        //    ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                        // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                    }
                }
                else if (UserCommand == "Started")
                {
                    RunStartPending = bNO;
                    RunAction = RunRunDo;
                    RunMetric = RunState;
                    RunTense = RunTense_Doing;
                    omHPassed.RunActionState[RunRunDo, RunState] = RunTense_Doing;
                    //omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                    //     "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                    //ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                    // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                }
                else if (UserCommand == "Cancel")
                {
                    if (omHPassed.RunActionState[RunRunDo, RunState] == RunTense_Doing)
                    {
                        if (omHPassed.RunActionState[RunCancel, RunState] != RunTense_Did && omHPassed.RunActionState[RunCancel, RunState] != RunTense_Doing)
                        {
                            RunCancelPending = bYES;
                            RunAction = RunCancel;
                            RunMetric = RunState;
                            RunTense = RunTense_Do;
                            omHPassed.RunActionState[RunCancel, RunState] = RunTense_Do;
                            //omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                            //    "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                            //ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                            // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                            RunCancelAsync();
                        }
                    }
                }
                else if (UserCommand == "Cancelled")
                {
                    RunCancelPending = bNO;
                    RunAction = RunCancel;
                    RunMetric = RunState;
                    RunTense = RunTense_Did;
                    omHPassed.RunActionState[RunCancel, RunState] = RunTense_Did;
                    //omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                    //     "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                    //ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                    // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                }
                else if (UserCommand == "Pause")
                {
                    // System.Windows.RoutedEventArgs RoutedEventItemTemp = new System.Windows.RoutedEventArgs();
                    // RoutedEventTemp = null;
                    // XUomMavvXv.XUomVtvvXv.CallerAsynchronousEventsPauseClick;
                    // Set State
                    if (!RunCancelPending)
                    {
                        if (omHPassed.RunActionState[RunPause, RunState] == RunTense_Done
                            )
                        {
                            LocalMessage.Msg = "Pause ended, resuming now...";
                            RunPausePending = bNO;
                            RunAction = RunPause;
                            RunMetric = RunState;
                            RunTense = RunTense_Off;
                            omHPassed.RunActionState[RunPause, RunState] = RunTense_Off;
                            //    omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                            //"R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                            //ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                            // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                        }
                        else if (omHPassed.RunActionState[RunPause, RunState] == RunTense_Doing
                          )
                        {
                            LocalMessage.Msg = "Pausing, please wait...";
                            RunAction = RunPause;
                            RunMetric = RunState;
                            RunTense = RunTense_Off;
                            omHPassed.RunActionState[RunPause, RunState] = RunTense_Off;
                            //    omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                            //"R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                            //ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                            // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                        }
                        else if (omHPassed.RunActionState[RunPause, RunState] == RunTense_Did
                          )
                        {
                            LocalMessage.Msg = "Paused now, waiting for resume...";
                            RunAction = RunPause;
                            RunMetric = RunState;
                            RunTense = RunTense_Off;
                            omHPassed.RunActionState[RunPause, RunState] = RunTense_Off;
                            //    omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                            //"R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                        }
                        else if (omHPassed.RunActionState[RunPause, RunState] == RunTense_Do
                          )
                        {
                            LocalMessage.Msg = "Pause request submitted, please wait...";
                            RunPausePending = bYES;
                            RunAction = RunPause;
                            RunMetric = RunState;
                            RunTense = RunTense_Do;
                            omHPassed.RunActionState[RunPause, RunState] = RunTense_Do;
                            //omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                            //"R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                            //ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                            // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                        }
                        else if (omHPassed.RunActionState[RunPause, RunState] != RunTense_DoNot
                              //  && omHPassed.RunActionState[RunPause, RunState] != RunTense_Doing
                              //  && omHPassed.RunActionState[RunPause, RunState] != RunTense_Do
                              //  && omHPassed.RunActionState[RunPause, RunState] != RunTense_Did
                              //  && omHPassed.RunActionState[RunPause, RunState] != RunTense_Off 
                              //  && omHPassed.RunActionState[RunPause, RunState] != RunTense_Done 
                              && omHPassed.RunActionState[RunPause, RunState] != RunTense_DidNot
                              )
                        {
                            LocalMessage.Msg = "Other Pause action. I.E. DoNot or DidNot";
                        }
                        else
                        {
                            LocalMessage.Msg = "Invalid Pause action other...";
                        }
                    }
                }
                else if (UserCommand == "Paused")
                {
                    RunPausePending = bNO;
                    RunAction = RunPause;
                    RunMetric = RunState;
                    RunTense = RunTense_Did;
                    omHPassed.RunActionState[RunPause, RunState] = RunTense_Did;
                    //omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                    //    "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                    //ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                    // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                }
                else if (UserCommand == "Resume")
                { // not used
                    LocalMessage.Msg = "Resume command";
                    // omHPassed.RunActionState[RunResume, RunState] = Resume_Something;
                    //    omHPassed.RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
                    //"R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + LocalMessage.Msg);
                    //    ThreadUiProgressAsync(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                    // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl(ref Sender, omHPassed.RunActionProgressChangeEventArgs);
                }
            }
            else if (UserCommand == "xxxxx")
            {

            }
        }
        #endregion
        // xxxxxxxx 
        #region Local Run Action Pause
        public int AppActionWaitMilliIncrement;
        public int AppActionWaitMilliIncrementMax;
        public bool AppActionWaitContinue;
        public int AppActionWaitCounter;
        #endregion
        // xxxxxxxx 
        #region Local Progress Bar
        //ProgressBar LocalProgressBar;
        public double LocalProgressBar_Minimum;
        public double LocalProgressBar_Maximum;
        public double LocalProgressBar_Value;
        public int LocalProgressBar_Display;
        //
        //public ProgressChangedEventArgs LocalThreadProgressChangedItem;
        //public RoutedPropertyChangedEventArgs<double> LocalThreadPropertyChangedItem;
        //public RunWorkerCompletedEventArgs LocalRunCompletedItem;
        public double LocalOldValue;
        public double LocalNewValue;
        #endregion
        #endregion
    }
    public class Step
    {
        public int Id;
        public string Title;
        public StateIs Status;
        public string InfoTip;
        public DateTime TimeStarted;
        public DateTime TimeEnded;
        public long zElapsed;
        public int LoadTimerStartTick;
        public int LoadTimerEndTick;

        public int Elapsed
        {
            get
            {
                if (TimeEnded == null
                    || TimeEnded.ToBinary() == 0)
                {
                    zElapsed = (DateTime.Now.ToBinary() - TimeStarted.ToBinary());
                }
                else
                {
                    zElapsed = (TimeEnded.ToBinary() - TimeStarted.ToBinary());
                }
                return (int)zElapsed;
            }
        }
        public Step(int StepPassed, string TitlePassed)
        {
            Status = StateIs.Created;
            TimeStarted = DateTime.Now;
            Id = StepPassed;
            Title = TitlePassed;
        }
        public Step(string TitlePassed)
        {
            Status = StateIs.Created;
            TimeStarted = DateTime.Now;
            Id = -1;
            Title = TitlePassed;
        }
        public Step()
        {
            Status = StateIs.Created;
            TimeStarted = DateTime.Now;
            Id = -1;
            Title = "";
        }
    }
    public class StdProcessDef : IComparable<StdProcessDef>
    {
        #region Key
        public StdKeyDef StdKey;
        public int CompareTo(StdProcessDef StdProcessPassed)
        {
            if (String.Compare(IconLevel, StdProcessPassed.IconLevel) > 0)
            {
                return 1;
            } else if (String.Compare(IconLevel, StdProcessPassed.IconLevel) < 0)
            {
                return -1;
            }

            if (String.Compare(IconOrder, StdProcessPassed.IconOrder) > 0)
            {
                return 1;
            }
            else if (String.Compare(IconOrder, StdProcessPassed.IconOrder) < 0)
            {
                return -1;
            }
            if (String.Compare(IconName, StdProcessPassed.IconName) > 0)
            {
                return 1;
            }
            else if (String.Compare(IconName, StdProcessPassed.IconName) < 0)
            {
                return -1;
            }
            return 1;
        }
        public void KeySet()
        {
            if (zIconLevel != null && zIconOrder != null && zIconName != null)
            {
                if (StdKey != null && zIconName != "" && Processes.ContainsKey(StdKey.Key))
                {
                    Processes.Remove(StdKey.Key);
                }
                if (StdKey == null)
                {
                    StdKey = new StdKeyDef(zIconLevel, zIconOrder, zIconName);
                }
                else
                {
                    StdKey.BuildFromFields(zIconLevel, zIconOrder, zIconName);
                }
                if (!Processes.ContainsKey(StdKey.Key))
                {
                    Processes.Add(StdKey.Key, this);
                }
            }
        }

        public string zIconLevel;
        public string IconLevel
        {
            get
            {
                return zIconLevel;
            }
            set
            {
                if (zIconLevel == null) zIconLevel = "#";
                if (zIconLevel != value)
                {
                    zIconLevel = value;
                    KeySet();
                }
            }
        }

        public string zIconOrder;
        public string IconOrder
        {
            get
            {
                return zIconOrder;
            }
            set
            {
                if (zIconOrder == null) zIconOrder = "#";
                if (zIconOrder != value)
                {
                    zIconOrder = value;
                    KeySet();
                }
            }
        }

        public string zIconName;
        public string IconName
        {
            get
            {
                return zIconName;
            }
            set
            {
                if (zIconName == null) zIconName = "#";
                if (zIconName != value)
                {
                    zIconName = value;
                    KeySet();
                }
            }
        }
        #endregion
        #region Fields
        public int Id { get; set; }
        public string Title { get; set; }
        public StateIs Status;
        public string IconFileName { get; set; }
        public int Priority { get; set; } // not used.
        public int Verbosity { get; set; }
        public int ZOrder { get; set; }
        public int Number { get; set; }
        #endregion
        #region Results
        public StateIs IntResult { get; set; }
        public long LongResult { get; set; }
        public string StringResult { get; set; }
        public object ObjectResult { get; set; }
        #endregion
        #region Device objects
        // System.IntPtr MainHandle = GetTopMostDef.GetWindowHandle("Shortcut Repair Browser");
        // Window window = Window.GetWindow((this);
        // var wih = new WindowInteropHelper(window);
        // IntPtr hWnd = wih.Handle;

        // Key: device name, Value: dict of window objects
        // Window object key: zorder, Value: window object

        //Dictionary<string, Dictionary<int, WindowDef>> Screens
        //    = new Dictionary<string, Dictionary<int, WindowDef>>();
        //Dictionary<HWND, string> Windows = new Dictionary<HWND, string>() >;
        //Dictionary<int, WindowDef> WindowsZOrder = new Dictionary<int, WindowDef>();
        public static StdDictIndexDef<string, StdProcessDef> Processes;
        public static string WindowTopMost;
        public static string WindowTopMostPrev;
        public StdScreenDef StdScreen;
        #endregion
        #region Process Steps (ie initialization)
        public List<Step> Steps;
        public int StepCurr;
        public DateTime FirstTime;
        public int StepGetNext() { return StepCurr++; }
        public int StepGet() { return StepCurr; }
        #endregion
        #region Constructors, Init, ToString
        static StdProcessDef()
        {
            Processes = new StdDictIndexDef<string, StdProcessDef>();
            WindowTopMost = "";
            WindowTopMostPrev = "";
        }
        public StdProcessDef(
            string LevelPassed,
            string OrderPassed,
            string NamePassed)
        {
            IconLevel = LevelPassed;
            IconOrder = OrderPassed;
            IconName = NamePassed;
            //Key = new StdKeyDef(IconLevel, IconOrder, IconName);
            InitializeStdProcess();
        }
        public StdProcessDef()
        {
            IconLevel = "#";
            IconOrder = "#";
            IconName = "NotSet";
            InitializeStdProcess();
        }
        public StdProcessDef(String NamePassed)
        {
            IconLevel = "#";
            IconOrder = "#";
            IconName = NamePassed;
            InitializeStdProcess();
        }
        public void InitializeStdProcess()
        {
            Status = StateIs.Initialized;
            FirstTime = DateTime.Now;
            StepCurr = -1;
            Id = -1;
            Title = "";
            StdScreen = new StdScreenDef(StdKey);
            ZOrder = -1;
            Number = -1;
            Priority = -1;
            Steps = new List<Step>();
            //IconName = NamePassed;
            // Dunno.
            //Key = new StdKeyDef(IconLevel, IconOrder, IconName);
            //Screens.Add(Key, Screen = new ScreenDef(Key));
            //if (!Screens.DeviceName.Contains(StdProcess.DeviceName))
            //{
            //    Screens.DeviceName.Add(StdProcess.DeviceName);
            //    Screens.DeviceForm.Add(new object());
            //    Screens.DeviceTopMostPrev.Add(new object());
            //}
        }
        public override string ToString()
        {
            return StdKey.ToString();
        }
        #endregion
    }
    public class StdScreenDef
    {
        public Screen ScreenObject;
        public string DeviceName;
        public object DeviceForm;
        //
        // IntPtr MainHandle;
        // Window Window;
        // IntPtr hWnd;
        //
        // Key: device name, Value: dict of window objects
        // Window object key: zorder, Value: window object
        //
        // static Dictionary<string, Dictionary<int, WindowDef>> Screens
        //     = new Dictionary<string, Dictionary<int, WindowDef>>();
        // static Dictionary<Window, string> Windows = new Dictionary<Window, string>() >;
        // static Dictionary<int, WindowDef> WindowsZOrder = new Dictionary<int, WindowDef>();
        // static Stack<StdKeyDef> FormHistory; // Back stack
        //
        public StdKeyDef StdKey;
        public StdScreenDef(StdKeyDef StdKeyPassed)
        {
            StdKey = StdKeyPassed;
            ScreenObject = null;
            DeviceName = StdKeyPassed.IconName;
            DeviceForm = new object();
        }
    }
    //
    //MainHandle = GetTopMostDef.GetWindowHandle("Shortcut Repair Browser");
    //Window = Window.GetWindow((DeviceForm);
    //var wih = new WindowInteropHelper(window);
    //hWnd = wih.Handle;
    // List<IntPtr> temp = GetTopMostDef.GetAllChildrenWindowHandles(MainHandle, 100);
    //if (!ParentObject.DeviceName.Contains(ScreenObject.DeviceName))
    //{
    //    ParentObject.DeviceName.Add(ScreenObject.DeviceName);
    //    ParentObject.DeviceForm.Add(new object());
    //    ParentObject.DeviceTopMostPrev.Add(new object());
    //}
    // ScreenIndex = ParentObject.DeviceName.IndexOf(ScreenObject.DeviceName);

    // FormResultDef FormResult = GetTopMost.BuildScreenOrder(this, MainHandle);
    // FormResultDef FormResult = GetTopMostDef.GetTopMostWindow(MainHandle);
    // ParentObject.ScreensObject = FormResult.ScreensObject;
}
namespace Mdm.Oss.Run.Control
{
    public class RunControlDef
    {
        #region RunControlManagement properties
        protected const string sUnknown = "unknown";
        protected const string sEmpty = "";
        protected const int iUnknown = 99999;
        #region RunControl properties
        protected int zMdmRunId = iUnknown;
        public int MdmRunId { get { return zMdmRunId; } set { zMdmRunId = value; } }
        protected String zMdmRunName = sUnknown;
        public String MdmRunName { get { return zMdmRunName; } set { zMdmRunName = value; } }
        protected String zMdmRunTitle = sUnknown;
        public String MdmRunTitle { get { return zMdmRunTitle; } set { zMdmRunTitle = value; } }
        protected int zMdmRunNumber = iUnknown;
        public int MdmRunNumber { get { return zMdmRunNumber; } set { zMdmRunNumber = value; } }
        protected StateIs zMdmRunStatus = StateIs.NotSet;
        public StateIs MdmRunStatus { get { return zMdmRunStatus; } set { zMdmRunStatus = value; } }
        protected String zMdmRunStatusText = sUnknown;
        public String MdmRunStatusText { get { return zMdmRunStatusText; } set { zMdmRunStatusText = value; } }
        protected StateIs zMdmRunIntResult = StateIs.NotSet;
        public StateIs MdmRunIntResult { get { return zMdmRunIntResult; } set { zMdmRunIntResult = value; } }
        protected bool zMdmRunBoolResult = false;
        public bool MdmRunBoolResult { get { return zMdmRunBoolResult; } set { zMdmRunBoolResult = value; } }
        #endregion
        #region AutoRun properties
        protected int zMdmAutoRunId = iUnknown;
        public int MdmAutoRunId { get { return zMdmAutoRunId; } set { zMdmAutoRunId = value; } }
        protected String zMdmAutoRunName = sUnknown;
        public String MdmAutoRunName { get { return zMdmAutoRunName; } set { zMdmAutoRunName = value; } }
        protected String zMdmAutoRunTitle = sUnknown;
        public String MdmAutoRunTitle { get { return zMdmAutoRunTitle; } set { zMdmAutoRunTitle = value; } }
        protected int zMdmAutoRunNumber = iUnknown;
        public int MdmAutoRunNumber { get { return zMdmAutoRunNumber; } set { zMdmAutoRunNumber = value; } }
        protected StateIs zMdmAutoRunStatus = StateIs.NotSet;
        public StateIs MdmAutoRunStatus { get { return zMdmAutoRunStatus; } set { zMdmAutoRunStatus = value; } }
        protected String zMdmAutoRunStatusText = sUnknown;
        public String MdmAutoRunStatusText { get { return zMdmAutoRunStatusText; } set { zMdmAutoRunStatusText = value; } }
        protected StateIs zMdmAutoRunIntResult = StateIs.NotSet;
        public StateIs MdmAutoRunIntResult { get { return zMdmAutoRunIntResult; } set { zMdmAutoRunIntResult = value; } }
        protected bool zMdmAutoRunBoolResult = false;
        public bool MdmAutoRunBoolResult { get { return zMdmAutoRunBoolResult; } set { zMdmAutoRunBoolResult = value; } }
        #endregion
        #region Input properties
        protected int zMdmInputId = iUnknown;
        public int MdmInputId { get { return zMdmInputId; } set { zMdmInputId = value; } }
        protected String zMdmInputName = sUnknown;
        public String MdmInputName { get { return zMdmInputName; } set { zMdmInputName = value; } }
        protected String zMdmInputTitle = sUnknown;
        public String MdmInputTitle { get { return zMdmInputTitle; } set { zMdmInputTitle = value; } }
        protected int zMdmInputNumber = iUnknown;
        public int MdmInputNumber { get { return zMdmInputNumber; } set { zMdmInputNumber = value; } }
        protected StateIs zMdmInputStatus = StateIs.NotSet;
        public StateIs MdmInputStatus { get { return zMdmInputStatus; } set { zMdmInputStatus = value; } }
        protected String zMdmInputStatusText = sUnknown;
        public String MdmInputStatusText { get { return zMdmInputStatusText; } set { zMdmInputStatusText = value; } }
        protected StateIs zMdmInputIntResult = StateIs.NotSet;
        public StateIs MdmInputIntResult { get { return zMdmInputIntResult; } set { zMdmInputIntResult = value; } }
        protected bool zMdmInputBoolResult = false;
        public bool MdmInputBoolResult { get { return zMdmInputBoolResult; } set { zMdmInputBoolResult = value; } }
        #endregion
        #region Output properties
        protected int zMdmOutputId = iUnknown;
        public int MdmOutputId { get { return zMdmOutputId; } set { zMdmOutputId = value; } }
        protected String zMdmOutputName = sUnknown;
        public String MdmOutputName { get { return zMdmOutputName; } set { zMdmOutputName = value; } }
        protected String zMdmOutputTitle = sUnknown;
        public String MdmOutputTitle { get { return zMdmOutputTitle; } set { zMdmOutputTitle = value; } }
        protected int zMdmOutputNumber = iUnknown;
        public int MdmOutputNumber { get { return zMdmOutputNumber; } set { zMdmOutputNumber = value; } }
        protected StateIs zMdmOutputStatus = StateIs.NotSet;
        public StateIs MdmOutputStatus { get { return zMdmOutputStatus; } set { zMdmOutputStatus = value; } }
        protected String zMdmOutputStatusText = sUnknown;
        public String MdmOutputStatusText { get { return zMdmOutputStatusText; } set { zMdmOutputStatusText = value; } }
        protected StateIs zMdmOutputIntResult = StateIs.NotSet;
        public StateIs MdmOutputIntResult { get { return zMdmOutputIntResult; } set { zMdmOutputIntResult = value; } }
        protected bool zMdmOutputBoolResult = false;
        public bool MdmOutputBoolResult { get { return zMdmOutputBoolResult; } set { zMdmOutputBoolResult = value; } }
        #endregion
        #endregion
    }
}
namespace Mdm.Oss.File.Control
{
    /// <summary> 
    /// This enumeration provides a list of file action(s) that 
    /// can be performed on the file. 
    /// </summary> 
    [Flags]
    public enum FileAction_ToDoIs : long
    {
        Undefined = 0x90001, // Action
        NotSet = 0x90002,
        UseDefault = 0x90004,
        UndefinedResult = 0x90008,
        // ACTION ToDo Review
        Open = 0x1001,
        Close = 0x1002,
        Connect = 0x1012,
        Disconnect = 0x1013,
        Check = 0x1014,
        // Crud
        Create = 0x1004,
        Read = 0x1010,
        Write = 0x1011,
        Insert = 0x1015,
        Update = 0x1016,
        Delete = 0x1008,
        Drop = 0x1019, // NOT IN USE
        // Lists
        ListGet = 0x1018,
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // <Area Id = "State">
        Start = 0x411,
        Complete = 0x412,
        Failed = 0x413,
        Cancel = 0x414,
        Pause = 0x415,
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        // <Area Id = "FileCreation">
        IoCreateIfMissing = 0x7123,
        IoCreateOnly = 0x7124,
        // - XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    }
    /// <summary> 
    /// This provides a list of the file and 
    /// database related system objects such as 
    /// systems, servers, databases, etc.
    /// </summary> 
    [Flags]
    public enum FileAction_ToDoTargetIs : long
    {
        Undefined = 0x90001, // Action
        NotSet = 0x90002,
        UseDefault = 0x90004,
        UndefinedResult = 0x90008,
        // File Object Type
        // Database
        FileDictData = 0x10001,
        FileData = 0x10002,
        // File System Object File and streams
        FsoDictData = 0x10004,
        FsoData = 0x10008,
        // Objects
        ObjectFILEDATA = 0x100001,
        ObjectFILEDICT = 0x100002,
        ObjectDATABASE = 0x100004,
        ObjectSERVICE = 0x100008,
        ObjectSERVER = 0x100010,
        ObjectSYSTEM = 0x100011,
        ObjectNETWORK = 0x100012,
        ObjectSECURITY = 0x100014,
        ObjectUSER = 0x100018,
        // High Level Objects
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
        // <Area Id = "AsciiOpenOptions">
        // <Area Id = "FileAccess">
        IoAccessReadOnly = 0x8000111,
        IoAccessAppendOnly = 0x8000112,
        // <Area Id = "FileCreation">
        IoCreateIfMissing = 0x7123,
        IoCreateOnly = 0x7124,
    }
}
