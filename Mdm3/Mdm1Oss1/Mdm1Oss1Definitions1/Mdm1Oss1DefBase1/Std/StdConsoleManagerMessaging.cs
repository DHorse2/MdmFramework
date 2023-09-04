#region  Dependencies
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

#region  Mdm Core
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
#endregion
#endregion

namespace Mdm.Oss.Std
{
    public partial class StdConsoleManagerDef
    {
        // Message Dispatch Components
        // Note: This class (and those that use it)
        // are designed to use ((StdConsoleManagerDef)ConsoleSender)
        // meaning the case is one of
        // iClassFeatures : ((iClassFeatures)ConsoleSender)
        // or iTrace : ((iTrace)ConsoleSender)
        // in their calls.
        // Were it the case that the interface
        // were being implemented in another class.
        // and specific to this file's functions.
        // That won't happen.
        //
        #region State
        public bool IsPossiblyUIThread()
        {
            return SynchronizationContext.Current != null;
        }
        public void InvokeUI(Action a)
        {
            // this.BeginInvoke(new MethodInvoker(a));
        }
        #endregion
        #region Pause Processing
        /// <summary>
        /// This check can be called from anywhere within update
        /// processing (across thread boundaries) to determine
        /// if the user interface or other system components have
        /// requested a pause in processing.
        /// </summary> 
        public void RunPauseCheck()
        {
            int DotCount;
            int iTemp; int iTemp1;
            //
            // Check for Run Pausing
            //
            if (!RunCancelPending
                && (
                RunPausePending
                || RunActionState[RunPause, RunState] == RunTense_On
                || RunActionState[RunPause, RunState] == RunTense_Do
                || RunActionState[RunPause, RunState] == RunTense_Doing
                || RunActionState[RunPause, RunState] == RunTense_Did
                )
                )
            {
                RunPausePending = bOFF;
                LocalMessage.Msg = "Process paused, waiting form resume...";
                // PrintOutputMdm_PickPrint(Sender, 1, LocalMessage.Msg0, bYES);
                RunAction = RunPause;
                RunMetric = RunState;
                RunTense = RunTense_Did;
                RunActionState[RunPause, RunState] = RunTense_Did;
                //RunActionProgressChangeEventArgs = new ProgressChangedEventArgs((int)LocalProgressBar_Value,
                //"R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString() + LocalMessage.Msg);
                //ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs);
                //
                AppActionWaitMilliIncrement = 500;
                AppActionWaitMilliIncrementMax = 3600000;
                AppActionWaitContinue = true;
                AppActionWaitCounter = 0;
                DotCount = 0;
                //
                while (AppActionWaitContinue
                    && RunActionState[RunPause, RunState] != RunTense_Off
                    && RunActionState[RunPause, RunState] != RunTense_Done
                    )
                {
                    DotCount = 0;
                    AppActionWaitCounter += 1;
                    // every 0.5 seconds
                    System.Threading.Thread.Sleep(AppActionWaitMilliIncrement);
                    // cancel run after max
                    if (AppActionWaitCounter > AppActionWaitMilliIncrementMax)
                    {
                        RunCancelPending = bYES;
                        AppActionWaitContinue = bNO;
                    }
                    // exit if external cancel occured
                    if (RunCancelPending
                        || RunActionState[RunPause, RunState] == RunTense_Off
                        || RunActionState[RunPause, RunState] == RunTense_Done
                        )
                    {
                        AppActionWaitContinue = false;
                    }
                    // every 5 seconds
                    iTemp1 = Math.DivRem(AppActionWaitCounter, 10, out iTemp);
                    if (iTemp == 0)
                    {
                        //RunActionProgressChangeEventArgs = new ProgressChangedEventArgs((int)LocalProgressBar_Value, 
                        //    "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString()
                        //    + ".");
                        if (DotCount >= 50)
                        {
                            // new line after 50 dots
                            //PrintOutputMdm_PickPrint(Sender, 1, ".", bYES);
                            DotCount = 1;
                        }
                        else
                        {
                            DotCount += 1;
                            //PrintOutputMdm_PickPrint(Sender, 1, ".", bNO);
                        }
                        // ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs);
                    }
                }
                if (DotCount > 0)
                {
                    // new line if there are any dots
                    //RunActionProgressChangeEventArgs = new ProgressChangedEventArgs((int)LocalProgressBar_Value,
                    //    "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString()
                    //    + ".");
                    //PrintOutputMdm_PickPrint(Sender, 1, ".", bYES);
                    // ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs);
                }
                LocalMessage.Msg = "Process resuming.";
                //if (RunActionState[RunPause, RunState] == RunTense_Done) {
                RunAction = RunPause;
                RunMetric = RunState;
                RunTense = RunTense_Off;
                RunActionState[RunPause, RunState] = RunTense_Off;
                //RunActionProgressChangeEventArgs = new ProgressChangedEventArgs((int)LocalProgressBar_Value,
                //"R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString() + LocalMessage.Msg);
                //ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs);
                //}
                // StatusUi.Box2Manage.ScrollDo = true;
            }
        }
        #endregion
        #region Message processing - Trace Mdm Do Basic
        /// <summary>
        /// Passes the message on to the trace processor using default values.
        /// </summary> 
        /// <param name="PassedTraceMessage">Message to be processed or displayed</param> 
        /// <returns>
        /// </returns>
        /// <remarks>
        /// </remarks> 
        public void TraceMdmDoBasic(String PassedTraceMessage)
        {
            TraceMdmBasicResult = StateIs.Started;
            //
            // PassedTraceMessage = PassedTraceMessage;
            //(ImTrace)ConsoleSender
            st.TraceMdmDoDetailed(ConsoleFormUses.UserLog, 5, ref Sender, bIsMessage, iNoOp, iNoOp, iNoMethodResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, PassedTraceMessage);
            // return TraceMdmBasic;
        }
        /// <summary>
        /// Passes the message on to the trace processor using default values.
        /// </summary> 
        /// <param name="PassedTraceMessage">Message to be processed or displayed</param> 
        /// <param name="iPassedConsoleType">Console to use</param> 
        /// <param name="iPassedVerbosity">Verbosity level</param> 
        /// <returns>
        /// </returns>
        /// <remarks>
        /// </remarks> 
        public void TraceMdmDoBasic(String PassedTraceMessage, ConsoleFormUses iPassedConsoleType, int iPassedVerbosity)
        {
            TraceMdmBasicResult = StateIs.Started;

            if (ConsoleVerbosity < iPassedVerbosity && iPassedConsoleType != ConsoleFormUses.ErrorLog) { return; }
            //
            // PassedTraceMessage = PassedTraceMessage;
            // (ImTrace)ConsoleSender
            Message = new mMsgDetailsDef();
            Message.Sender = Sender;
            // var temp = System.Threading.Thread.CurrentThread.ManagedThreadId;
            Message.IsMessage = true; // PassedIsMessage;
            Message.Verbosity = iPassedVerbosity;
            Message.ConsoleFormUse = iPassedConsoleType;
            if (Message.ConsoleFormUse == ConsoleFormUses.ErrorLog)
            {
                Message.IsError = true;
            }
            else
            {
                Message.IsError = false;
            }

            // ToDo TraceMdm 11 
            // ToDo TraceMdm 11 Do analysis and implement error levels.
            // ToDo TraceMdm 11 Do analysis and implement inner exceptions.
            // ToDo TraceMdm 11 Do analysis and implement exception call stack analysis
            // ToDo TraceMdm 11 Implement tabbing for levels.
            // ToDo TraceMdm 11 Implement trim back to last word (or puncuation) present in other code.
            // ToDo TraceMdm 11 
            // ToDo TraceMdm 11 
            //Message.Location1 = 0; // CharMaxIndexPassed;
            //Message.Location2 = 0; // MethodAttributeMaxPassed;
            //Message.MethodResult = StateIs.???; // iPassed_MethodResult;
            //Message.Level = 0; // iPassedErrorLevel;
            //Message.Source = 0; // iPassedErrorSource;
            Message.DoDisplay = true; // PassedDisplay;
            //Message.ResponseFlags = 0; // ToDo See examples! iPassedUserEntry;
            Message.Text = PassedTraceMessage;

            // FireConsoleTaskMessageEvent(Message);
            st.TraceMdmDoDetailed(iPassedConsoleType, iPassedVerbosity, ref Sender, bIsMessage, iNoOp, iNoOp, iNoMethodResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, PassedTraceMessage);
            // return TraceMdmBasic;
        }
        #endregion
        #region Message processing - Trace Mdm Do Detailed, Impl
        /// <summary>
        /// Constructs a message object from the passed
        /// arguments and call the standard trace processor.
        /// </summary> 
        /// <param name="Sender">The object sending the message.</param> 
        /// <param name="PassedIsMessage">Flag indicating a message is being passsed.</param> 
        /// <param name="CharMaxIndexPassed">First display and tracing value or counter.</param> 
        /// <param name="MethodAttributeMaxPassed">Second display and tracing value or counter.</param> 
        /// <param name="lPassed_MethodResult">The relevant local result related to the message.</param> 
        /// <param name="PassedError">Flag indicating an error has occured.</param> 
        /// <param name="iPassedErrorLevel">The error value that has occurred.</param> 
        /// <param name="iPassedErrorSource">The source of the error.</param> 
        /// <param name="PassedDisplay">Flag indicating the message is meant to be displayed.</param> 
        /// <param name="iPassedUserEntry">A Flags construct controlling what user entry should occur.</param> 
        /// <param name="PassedTraceMessage">The message itself.</param> 
        /// <remarks>
        /// </remarks> 
        public void TraceMdmDoDetailed(
            ConsoleFormUses iPassedConsoleType,
            int iPassedVerbosity,
            ref Object Sender,
            bool PassedIsMessage,
            int CharMaxIndexPassed,
            int MethodAttributeMaxPassed,
            StateIs lPassed_MethodResult,
            bool PassedError,
            int iPassedErrorLevel,
            int iPassedErrorSource,
            bool PassedDisplay,
            int iPassedUserEntry,
            String PassedTraceMessage
            )
        {
            // This is called before DoImpl so
            // that the Stack Depth is constant
            // accross the call variants above.
            TraceMdmDoImpl(
                iPassedConsoleType,
                iPassedVerbosity,
                ref Sender,
                PassedIsMessage,
                CharMaxIndexPassed,
                MethodAttributeMaxPassed,
                lPassed_MethodResult,
                PassedError,
                iPassedErrorLevel,
                iPassedErrorSource,
                PassedDisplay,
                iPassedUserEntry,
                PassedTraceMessage
                );
        }
        public void TraceMdmDoImpl(
            ConsoleFormUses iPassedConsoleType,
            int iPassedVerbosity,
            ref Object Sender,
            bool PassedIsMessage,
            int CharMaxIndexPassed,
            int MethodAttributeMaxPassed,
            StateIs lPassed_MethodResult,
            bool PassedError,
            int iPassedErrorLevel,
            int iPassedErrorSource,
            bool PassedDisplay,
            int iPassedUserEntry,
            String PassedTraceMessage
            )
        {

            if (ConsoleVerbosity < iPassedVerbosity && !PassedError) { return; }

            TraceMdmPointResult = StateIs.Started;
            // ToDo TraceMdm 11 
            // ToDo TraceMdm 11 Do analysis and implement error levels.
            // ToDo TraceMdm 11 Do analysis and implement inner exceptions.
            // ToDo TraceMdm 11 Do analysis and implement exception call stack analysis
            // ToDo TraceMdm 11 Implement tabbing for levels.
            // ToDo TraceMdm 11 Implement trim back to last word (or puncuation) present in other code.
            // ToDo TraceMdm 11 
            // ToDo TraceMdm 11 
            Message = new mMsgDetailsDef();
            Message.Sender = Sender;
            Message.IsMessage = PassedIsMessage;
            Message.ConsoleFormUse = iPassedConsoleType;
            Message.Verbosity = iPassedVerbosity;
            Message.Location1 = CharMaxIndexPassed.ToString();
            Message.Location2 = MethodAttributeMaxPassed.ToString();
            Message.MethodResult = lPassed_MethodResult;
            Message.IsError = PassedError;
            Message.Level = iPassedErrorLevel;
            Message.Source = iPassedErrorSource;
            Message.DoDisplay = PassedDisplay;
            Message.ResponseFlags = iPassedUserEntry;
            Message.Text = sEmpty;
            if (Message.IsError)
            {
                Message.Text += "[" + ReportErrorLine(4) + "]" + "\n";
            }
            Message.Text += PassedTraceMessage;
            FireConsoleTaskMessageEvent(Message);
            // ((ImTrace)ConsoleSender).TraceMdmDoImpl(Message);

        }
        #endregion
        // StackFrame CallStack;
        static public string ReportErrorLine(int Depth)
        {
            StackFrame CallStack = new StackFrame(Depth, true);
            return ("File: " + CallStack.GetFileName() + ", Line: " + CallStack.GetFileLineNumber());
        }
        #region Events Handler, Fire Task Message
        public delegate void ConsoleTaskMessageEventHandler(
                object sender, ConsoleMessageEventArgs e);
        public event ConsoleTaskMessageEventHandler ConsoleTaskMessageEvent;
        public void FireConsoleTaskMessageEvent(mMsgDetailsDef Message)
        {
            // ToDo This is uber low level and should be
            // ToDo moved to a base class. Shell is the spot.
            // ToDo Ditto with all event clear code.
            // ToDo Normalize it too.

            //string pathToDomain = StdBaseDef.DriveOs + @"\Studies\Reflection\Domain.dll";
            //Assembly domainAssembly = Assembly.LoadFrom(pathToDomain);
            //Type customerType = domainAssembly.GetType("Domain.Customer");
            //Type[] stringArgumentTypes = new Type[] { typeof(string) };
            //ConstructorInfo stringConstructor = customerType.GetConstructor(stringArgumentTypes);
            //object newStringCustomer = stringConstructor.Invoke(new object[] { "Elvis" });

            //MethodInfo retMethodInfo = customerType.GetMethod("DoRetMethod");
            //int returnValue = Convert.ToInt32(retMethodInfo.Invoke(newStringCustomer, new object[] { 4 }));

            if (System.Threading.Thread.CurrentThread.IsThreadPoolThread)
            {
                if (ConsoleTaskMessageEvent == null)
                {
                    ConsoleTaskMessageEvent += st.OnTraceMdmDoImpl;
                }
                if (ConsoleTaskMessageEvent != null)
                {
                    //string ThreadName = "Thread" + System.Threading.Thread.CurrentThread.ManagedThreadId;
                    //Message.Text = ThreadName + ": " + Message.Text;
                    System.Threading.Thread.Sleep(0);
                    //System.Threading.Thread.Yield();

                    ConsoleMessageEventArgs args = new ConsoleMessageEventArgs(Message);
                    if (ConsoleTaskMessageEvent.Target is Control)
                    {
                        // Note a shim can be put in the Form.
                        // via a OnTraceMdmDoImpl function.
                        Control targetForm = ConsoleTaskMessageEvent.Target as Control;
                        targetForm.BeginInvoke(ConsoleTaskMessageEvent,
                                new object[] { this, args });
                    }
                    else
                    {
                        // This executes on a non UI thread.
                        // There are other ways to manage this.
                        // I dunno wtf this is here:
                        //SynchronizationContext SyncContext = SynchronizationContext.Current;
                        //if (IsPossiblyUIThread())
                        //{
                        Type MethodType = ConsoleTaskMessageEvent.GetType();
                        MethodInfo targetMethod = ConsoleTaskMessageEvent.Method;
                        //targetMethod.Invoke(ConsoleTaskMessageEvent,
                        targetMethod.Invoke(st,
                                new object[] { this, args });
                        // ? or this:
                        // new object[] { args });
                        //ConsoleTaskMessageEvent(this, args);
                        //} else
                        //{
                        //    SyncContext.Post(o => FireConsoleTaskMessageEvent(Message), Message);
                        //}
                    }
                    System.Threading.Thread.Sleep(0);
                    //System.Threading.Thread.Yield();
                }
            }
            else
            {
                // ToDo WRONG!
                st.TraceMdmDoImpl(Message);
            }
        }
        #endregion
        #region Row
        public void ConsoleFormRowUpdate(ConsoleFormUses ConsoleId, string[] rowString, bool ShowForm, int PassedVerbosity, bool PassedIsError)
        {
            // lock (((ImTrace)ConsoleSender).MdmConsoles)
            {
                try
                {
                    st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                            .BeginInvoke(
                        (MethodInvoker)(() =>
                        st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                            .GridView.Rows.Add(rowString)
                        ));
                    //System.Threading.Thread.Yield();
                    //DataGridView LocalGridView =
                    //st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm.GridView;
                    //int tmp = LocalGridView.CurrentRow.GetPreferredHeight(
                    //    LocalGridView.RowCount,
                    //    DataGridViewAutoSizeRowMode.AllCellsExceptHeader,
                    //    false);
                    //LocalGridView.CurrentRow.Height = tmp;

                    //st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                    //        .BeginInvoke(
                    //    (MethodInvoker)(() =>
                    //    st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                    //        .GridView.CurrentRow.Height =
                    //    st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                    //        .GridView.CurrentRow.GetPreferredHeight(
                    //        st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                    //        .GridView.RowCount,
                    //        DataGridViewAutoSizeRowMode.AllCellsExceptHeader,
                    //        false)
                    //    ));
                    //
                    //System.Threading.Thread.Yield();
                    if (ConsoleVerbosity >= PassedVerbosity || PassedIsError)
                    {
                        // Set Visible Alert
                        if (PassedIsError)
                        { st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                                .StdNotifyIcon.VisibleAlert = true; }
                        if (ShowForm)
                        {
                            if (!st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                                .Visible)
                            {
                                st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                                        .BeginInvoke(
                                    (MethodInvoker)(() =>
                                    st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                                        .Form_ShowFromTray(false)
                                    )); ;
                            }
                            //System.Threading.Thread.Yield();
                        }

                        st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                                .BeginInvoke(
                            (MethodInvoker)(() =>
                            st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                                .GridView.FirstDisplayedScrollingRowIndex =
                            st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                                .GridView.RowCount - 1
                            ));
                        //System.Threading.Thread.Yield();
                    }
                    else
                    {
                        st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                                .BeginInvoke(
                            (MethodInvoker)(() =>
                            st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                                .VisibilitySetRowLast(false)
                            ));
                        //System.Threading.Thread.Yield();
                    }

                    st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                            .BeginInvoke(
                        (MethodInvoker)(() =>
                        st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                            .Refresh()
                        ));
                    //System.Threading.Thread.Yield();

                    for (int YieldCn = 10; YieldCn <= PassedVerbosity; YieldCn++)
                    {
                        //System.Threading.Thread.Yield();
                        //System.Threading.Thread.Yield();
                        //System.Threading.Thread.Yield();
                        //System.Threading.Thread.Yield();
                        //System.Threading.Thread.Yield();
                        System.Threading.Thread.Sleep(50);
                    }
                }
                catch (Exception) { }
            }
            // System.Threading.Thread.Yield();
            System.Threading.Thread.Sleep(50);
        }
        public void ConsoleFormRowUpdateAll(ConsoleSourceIs ConsoleId, string[] rowString, bool ShowForm, int PassedVerbosity)
        {
            // lock (((ImTrace)ConsoleSender).MdmConsoles)
            {
                try
                {
                    lock (DgvLock)
                    {
                        //((ImTrace)ConsoleSender).StdConsoles.Consoles[(int)console].ConsoleForm
                        //      .BeginInvoke(
                        //    (MethodInvoker)(() =>
                        st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                                .GridView.Rows.Add(rowString);
                        //));
                        if (ConsoleVerbosity >= PassedVerbosity)
                        {
                            if (ShowForm)
                            {
                                //((ImTrace)ConsoleSender).StdConsoles.Consoles[(int)console].ConsoleForm
                                //      .BeginInvoke(
                                //    (MethodInvoker)(() =>
                                // ((ImTrace)ConsoleSender).StdConsoles.Consoles[(int)console].ConsoleForm
                                //      .Form_ShowFromTray()
                                st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                                        .Form_ShowFromTray(false); // .NotifyIcon_ClickDo(this, true, true);
                                //));
                            }
                            //
                            //((ImTrace)ConsoleSender).StdConsoles.Consoles[(int)console].ConsoleForm
                            //      .BeginInvoke(
                            //    (MethodInvoker)(() =>
                            st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                                    .GridView.FirstDisplayedScrollingRowIndex =
                                st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                                    .GridView.RowCount - 1;
                            //));
                        }
                        else
                        {
                            //((ImTrace)ConsoleSender).StdConsoles.Consoles[(int)console].ConsoleForm
                            //      .BeginInvoke(
                            //    (MethodInvoker)(() =>
                            st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                                    .VisibilitySetRowLast(false);
                            //));
                        }

                    //((ImTrace)ConsoleSender).StdConsoles.Consoles[(int)console].ConsoleForm
                    //      .BeginInvoke(
                    //    (MethodInvoker)(() =>
                    st.StdConsoles.Consoles[(int)ConsoleId].ConsoleForm
                            .Refresh();
                        //));
                    }
                }
                catch (Exception) { }
            }
            // System.Threading.Thread.Sleep(200);
        }
        //
        [ThreadStatic]
        public static string[] RowString;
        #endregion
        // Trace Point
        #region Trace Point - (On) Trace Mdm Do Impl, Multiline, Call
        /// <summary>
        /// Main function call for passing messages.
        /// TraceDo handle console, status line, completion progress
        /// and also trace and logging messages.
        /// There are several options to include source and location information
        /// as well a plans to implement user response handling.
        /// Other features accomodate multi-line message formatting.  
        /// This method is also compatable with cross threaded messaging.
        /// Formatted output is passed to ConsoleMdmPickDisplayImpl 
        /// for printing usually but planned targets includ Debug.WriteLn, 
        /// LogMdm and options for modol windows and other rendering.
        /// </summary> 
        /// <param name="Message">Message arguments object</param> 
        /// <remarks>
        /// </remarks> 
        public void OnTraceMdmDoImpl(object sender, ConsoleMessageEventArgs e)
        {
            if (e.Message == null) { return; }
            st.TraceMdmDoImpl(e.Message);
        }
        // These functions handle the complexity of logging, debug and the UI.
        public void TraceMdmDoImpl(mMsgDetailsDef Message)
        {
            // Thread-Local variable that yields a name for a thread
            //string ThreadName = "Thread" + System.Threading.Thread.CurrentThread.ManagedThreadId;
            //Message.Text = ThreadName + ": " + Message.Text;
            lock (TraceLock)
            {
                string sTemp1 = sEmpty;
                // ToDo x
                if (ConsoleOn & (ConsoleTextOn | ConsoleBasicOn | ConsoleToDisc | ConsoleToControl)) { TraceDisplayMessageDetail = bON; } else { TraceDisplayMessageDetail = bOFF; }
                //
                #region Trace Mdm Top
                if (TraceFirst)
                {
                    TraceIterationOnNow = TraceIterationInitialState;
                    TraceDisplayOnNow = TraceDisplayInitialState;
                    TraceBugOnNow = TraceBugInitialState;
                    TraceFirst = false;
                }
                //
                // Tracing Control
                //
                TraceMessage = Message.Text;
                TraceMessageBlockString = TraceMessage;
                TraceMessagePrefix = sEmpty;
                TraceMessageSuffix = sEmpty;
                // Display this message
                TraceDisplayMessageDetail = bOFF;
                // Display Message counting
                // TraceDisplayCount += 1;
                TraceDisplayCountTotal += 1;
                // Bug handling
                TraceBugCountTotal += 1;
                //
                LocalUserEntryLong = Message.ResponseId;
                LocalUserEntry = sEmpty;
                #endregion
                Type objtype = Message.Sender.GetType();
                TraceMessage = objtype.Name + ": " + TraceMessage;
                if (ConsoleToControl)
                {
                    RowString = new string[7];
                    RowString[0] = "1"; // CrData.Id.ToString();
                    RowString[1] = TraceDisplayCountTotal.ToString(); // CrData.Sequence;
                    RowString[2] = TraceMessage;
                    RowString[3] = Message.ConsoleFormUse.ToString();
                    // Verbosity
                    RowString[4] = Message.Verbosity.ToString();
                    // Is Error
                    RowString[5] = Message.IsError.ToString();
                    // MethodResult
                    RowString[6] = Message.MethodResult.ToString();
                }
                #region Error
                if (Message.IsError)
                {
                    //
                    String sTemp4 = sEmpty;
                    RunErrorDidOccurOnce = bYES;
                    if (ConsoleToControl)
                    {
                        try
                        {
                            ConsoleFormRowUpdate(ConsoleFormUses.ErrorLog, RowString, true, Message.Verbosity, Message.IsError);
                            //((ImTrace)ConsoleSender).StdConsoles.Consoles[ConsoleFormUses.ErrorLog].ConsoleForm.Cf.Rows.Add(rowString);
                            //((ImTrace)ConsoleSender).StdConsoles.Consoles[ConsoleFormUses.ErrorLog].ConsoleForm.Form1_ShowFromTray();
                            //((ImTrace)ConsoleSender).StdConsoles.Consoles[ConsoleFormUses.ErrorLog].ConsoleForm.Cf.FirstDisplayedScrollingRowIndex =
                            //    ((ImTrace)ConsoleSender).StdConsoles.Consoles[ConsoleFormUses.ErrorLog].ConsoleForm.Cf.RowCount - 1;
                            //((ImTrace)ConsoleSender).StdConsoles.Consoles[ConsoleFormUses.ErrorLog].ConsoleForm.Refresh();
                        }
                        catch (Exception)
                        {

                            // throw;
                        }
                    }
                    if (Message.ResponseId == MessageEnterResume)
                    {
                        RunPausePending = bYES; // ToDo TraceMdm Implement puase properly
                    }
                    if (Message.Level > 0) { RunErrorCount += 1; }
                    // ToDo Thread
                    LocalMessage.ErrorMsg = TraceMessage;
                    // LocalMessage.ErrorMsg = TraceMessage;
                    //
                    // ConsoleOn = bYES;
                    // ConsoleToDisc = bYES;
                    // ConsoleOn = bYES;
                    ConsoleBasicOn = bYES;
                    // ConsoleToDisc = bYES;
                    //
                    TraceDisplayMessageDetail = bON;
                    LocalUserEntryLong = 0; // MessageEnterF5;
                    LocalUserEntry = "An unexpected error occured!";
                    try
                    {
                        TraceMessageTarget = TraceMessage.Substring(0, 1);
                        sTemp4 = TraceMessage.Substring(1);
                        if (TraceMessageTarget == "M" || TraceMessageTarget == "A")
                        {
                            TraceMessageTarget = TraceMessage.Substring(0, 2);
                            sTemp4 = TraceMessage.Substring(2);
                        }
                    }
                    catch
                    {
                        TraceMessageTarget = "E";
                        sTemp4 = sEmpty;
                    }
                    //
                    //TraceMessage = TraceMessageTarget + "Error";
                    //TraceMdmDoPrint(ref Sender);
                    //
                    if (TraceData)
                    {
                        TraceErrorMessage = TraceMessageTarget + "Error - Count(" + RunErrorCount.ToString() + "), Level(" + Message.Level.ToString() + "), Source(" + Message.Source.ToString() + ") : ";
                    }
                    else { TraceErrorMessage = sEmpty; }
                    TraceErrorMessage += sTemp4;
                    TraceMessage = TraceErrorMessage;
                    //
                }
                #endregion
                //
                if (TraceDisplayMessageDetail 
                    || TraceOn 
                    || TraceBug 
                    || TraceDebugOn 
                    || TraceBreakOnAll)
                {
                    #region Iteration Control
                    if (TraceIteration)
                    {
                        if (TraceIterationOnNow || TraceIterationCountTotal >= TraceIterationThreshold)
                        {
                            // TraceIterationCount += 1;
                            if (TraceIterationOnNow)
                            {
                                // debut display messages is currently on
                                if (TraceIterationCount > TraceIterationOnForCount)
                                {
                                    // turn off display after "off" lines
                                    TraceIterationOnNow = bOFF;
                                    TraceIterationCount = 1;
                                    TraceMessageSuffix += " Iteration OnFor exceeded.";
                                    TraceIterationOnForWarningGiven = bNO;
                                }
                                else if (TraceIterationCount == TraceIterationOnForCount)
                                {
                                    TraceDisplayMessageDetail = bON;
                                    if (TraceIterationOnForWarningGiven == bNO)
                                    {
                                        TraceMessageSuffix += " Iteration OnFor about to be exceeded.";
                                        TraceIterationOnForWarningGiven = bYES;
                                    }
                                }
                                else { TraceDisplayMessageDetail = bON; }
                            }
                            else
                            {
                                // debug display messages is currently off
                                if (TraceIterationRepeat && TraceIterationCount >= TraceIterationOnAgainCount)
                                {
                                    // turn display back on after On Again lines
                                    // reached the point for another cycle of
                                    // detail display TraceIterationCount messages
                                    TraceDisplayMessageDetail = bON;
                                    TraceIterationOnNow = bON;
                                    TraceIterationCount = 1;
                                    TraceMessageSuffix += " Iteration OnAgain reached.";
                                }
                                else if (TraceIterationCountTotal < TraceIterationThreshold + TraceIterationOnForCount)
                                {
                                    // TraceIterationOnNow should be on when the total count
                                    // is between the threshold & thresshold + off count
                                    TraceDisplayMessageDetail = bON;
                                    TraceIterationOnNow = bON;
                                    TraceIterationCount = 1;
                                    TraceMessageSuffix += " Iteration display activated.";
                                } // Bug On Again and Disply On threshold
                            } // Current Bug On or Off
                        } // Bug Control start point reached
                    } // Bug Control turned on
                    #endregion
                    #region Display Message Control
                    if (TraceDisplay)
                    {
                        if (TraceDisplayOnNow || TraceDisplayCountTotal >= TraceDisplayThreshold)
                        {
                            TraceDisplayCount += 1;
                            if (TraceDisplayOnNow)
                            {
                                // debut display messages is currently on
                                if (TraceDisplayCount > TraceDisplayOnForCount)
                                {
                                    // turn off display after "off" lines
                                    TraceDisplayOnNow = bOFF;
                                    TraceDisplayCount = 1;
                                    TraceMessageSuffix += " Display Message OnFor exceeded.";
                                }
                                else if (TraceDisplayCount == TraceDisplayOnForCount)
                                {
                                    TraceDisplayMessageDetail = bON;
                                    TraceMessageSuffix += " Display Message OnFor OnFor about to be exceeded.";
                                }
                                else { TraceDisplayMessageDetail = bON; }
                            }
                            else
                            {
                                // debug display messages is currently off
                                if (TraceDisplayRepeat && TraceDisplayCount >= TraceDisplayOnAgainCount)
                                {
                                    // turn display back on after On Again lines
                                    // reached the point for another cycle of
                                    // detail display TraceDisplayCount messages
                                    TraceDisplayMessageDetail = bON;
                                    TraceDisplayOnNow = bON;
                                    TraceDisplayCount = 1;
                                    TraceMessageSuffix += " Display Message OnAgain reached.";
                                }
                                else if (TraceDisplayCountTotal < TraceDisplayThreshold + TraceDisplayOnForCount)
                                {
                                    // TraceDisplayOnNow should be on when the total count
                                    // is between the threshold & thresshold + off count
                                    TraceDisplayMessageDetail = bON;
                                    TraceDisplayOnNow = bON;
                                    TraceDisplayCount = 1;
                                    TraceMessageSuffix += " Display Message display activated.";
                                } // Bug On Again and Disply On threshold
                            } // Current Bug On or Off
                        } // Display Message Control start point reached
                    } // Display Message Control turned on
                    #endregion
                    #region Bug Control
                    if (TraceBug)
                    {
                        if (TraceBugOnNow || TraceBugCountTotal >= TraceBugThreshold)
                        {
                            TraceBugCount += 1;
                            if (TraceBugOnNow)
                            {
                                // debut display messages is currently on
                                if (TraceBugCount > TraceBugOnForCount)
                                {
                                    // turn off display after "off" lines
                                    TraceBugOnNow = bOFF;
                                    TraceBugCount = 1;
                                    TraceMessageSuffix += " Bug OnFor exceeded.";
                                }
                                else if (TraceBugCount == TraceBugOnForCount)
                                {
                                    TraceDisplayMessageDetail = bON;
                                    TraceMessageSuffix += " Bug OnFor about to be exceeded.";
                                }
                                else { TraceDisplayMessageDetail = bON; }
                            }
                            else
                            {
                                // debug display messages is currently off
                                if (TraceBugRepeat && TraceBugCount >= TraceBugOnAgainCount)
                                {
                                    // turn display back on after On Again lines
                                    // reached the point for another cycle of
                                    // detail display TraceBugCount messages
                                    TraceDisplayMessageDetail = bON;
                                    TraceBugOnNow = bON;
                                    TraceBugCount = 1;
                                    TraceMessageSuffix += " Bug OnAgain reached.";
                                }
                                else if (TraceBugCountTotal < TraceBugThreshold + TraceBugOnForCount)
                                {
                                    // TraceBugOnNow should be on when the total count
                                    // is between the threshold & thresshold + off count
                                    TraceDisplayMessageDetail = bON;
                                    TraceBugOnNow = bON;
                                    TraceBugCount = 1;
                                    TraceMessageSuffix += " Bug display activated.";
                                } // Bug On Again and Disply On threshold
                            } // Current Bug On or Off
                        } // Bug Control start point reached
                    } // Bug Control turned on
                    #endregion
                    #region CHECK POINTS
                    // Trace Iteration counting 
                    // to prompt every checkpoint messages
                    // after reachiing first Threshold messages
                    if (TraceIterationCheckPointOn)
                    {
                        TraceIterationCheckPointCount += 1;
                        // prompt every TraceIterationCountCheckPoint messages
                        if (TraceIterationCheckPointCount >= TraceIterationCheckPoint)
                        {
                            LocalUserEntry += "Iteration Checkpoint.";
                            LocalUserEntryLong = MessageEnterF5;
                            //string tmp = System.Console.ReadLine(); // ToDo: Hangs Program
                            // ToDo
                            TraceIterationCheckPointCount = 0;

                        }
                    }
                    // Trace Display Messages counting 
                    // to prompt every checkpoint messages
                    // after reachiing first Threshold messages
                    if (TraceDisplayCheckPointOn)
                    {
                        TraceDisplayCheckPointCount += 1;
                        // prompt every TraceDisplayCountCheckPoint messages
                        if (TraceDisplayCheckPointCount >= TraceDisplayCheckPoint)
                        {
                            LocalUserEntry += "Display Message Checkpoint.";
                            LocalUserEntryLong = MessageEnterF5;
                            TraceDisplayCheckPointCount = 0;
                        }
                    }
                    // Trace Bug Messages counting 
                    // to prompt every checkpoint messages
                    // after reachiing first Threshold messages
                    if (TraceBugCheckPointOn)
                    {
                        TraceBugCheckPointCount += 1;
                        // prompt every TraceBugCountCheckPoint messages
                        if (TraceBugCount >= TraceBugCheckPoint)
                        {
                            LocalUserEntry += "Bug Checkpoint.";
                            LocalUserEntryLong = MessageEnterF5;
                            TraceBugCount = 0;
                            TraceBugCheckPointDo = true;
                        }
                        else { TraceBugCheckPointDo = false; }
                    }
                    #endregion
                }
                //
                #region Prepare Margin Data
                if (TraceData)
                {
                    if (TraceOn 
                        || TraceIterationOnNow 
                        || TraceDisplayOnNow 
                        || TraceBugOnNow 
                        || TraceDisplayMessageDetail)
                    {
                        // Prepare Message
                        // Margin Data, counts
                        // TraceMesageCount
                        TraceDataPointers = "m[" + PickOconv(TraceDisplayCountTotal, "r06") + "] ";
                        // TraceIterationCount, TraceIterationCountMax
                        TraceDataPointers += "a[" + PickOconv(TraceIterationCountTotal, "r06") + "." + PickOconv(TraceMdmCounterLevel1GetDefault(), "r06") + "] ";
                        // TraceCharacterCount, TraceCharacterCountMax
                        TraceDataPointers += "c[" + PickOconv(TraceCharacterCount, "r06") + "." + PickOconv(TraceMdmCounterLevel2GetDefault(), "r06") + "]";
                    }
                    else { TraceDataPointers = sEmpty; }
                }
                else { TraceDataPointers = sEmpty; }
                #endregion
                //
                #region Display Block Output Data
                //
                if (TraceDisplayMessageDetail 
                    || TraceOn 
                    || TraceIterationOnNow 
                    || TraceDisplayOnNow 
                    || TraceBugOnNow)
                {
                    if (Message.IsMessage)
                    {
                        st.TraceMdmDoPrint(ref Sender);
                    }
                    else
                    {
                        TraceMessageBlockString = TraceMessage;
                        TraceMessageBlock += TraceMessageBlockString;
                        if (Message.Verbosity <= ConsoleVerbosity || Message.IsError)
                        {
                            if (TraceData) { TraceMessageBlock += "|"; }
                            //
                            int Tml = TraceMessageBlock.Length;
                            while (Tml > 80 || (!TraceData && Tml > 0))
                            {
                                if (Tml > 80) { Tml = 80; }
                                if (TraceData)
                                {
                                    TraceMessage = (" [" + TraceMessageBlock.Substring(0, Tml) + "]");
                                }
                                else
                                {
                                    TraceMessage = TraceMessageBlock.Substring(0, Tml);
                                }
                                st.TraceMdmDoPrint(ref Sender);
                                if (TraceMessageBlock.Length > 80)
                                {
                                    TraceMessageBlock = TraceMessageBlock.Substring(80);
                                }
                                else
                                {
                                    TraceMessageBlock = sEmpty;
                                }
                                Tml = TraceMessageBlock.Length;
                            }
                        }
                    }
                }
                #endregion
                //
                #region User Entry, Get any user responses
                if (
                    (Message.ResponseId == MessageEnterF5 
                        || LocalUserEntryLong == MessageEnterF5 
                        || TraceDebugOn)
                    || (Message.IsError 
                        && (TraceDebugOn && TraceDebugDoErrorPrompt))
                    || TraceBreakOnAll
                    || Message.ResponseFlags > 0
                    )
                {
                    if (Message.IsError)
                    {
                        System.Console.WriteLine();
                        System.Console.WriteLine("StackTrace: '{0}'", Environment.StackTrace);
                        System.Console.WriteLine();
                    }
                    // F5 to continue prompt
                    // ToDo z$RelVs2 TraceMdm this will change, it is not a user prompt...
                    if (
                        (Message.Verbosity <= ConsoleVerbosity || Message.IsError) 
                        && ((Message.IsError && (TraceDebugOn && TraceDebugDoErrorPrompt))
                        || TraceBreakOnAll)
                        )
                    {
                        if (ConsoleOn)
                        {
                            // ToDo
                        }
                        if (LocalUserEntryLong == MessageEnterF5)
                        {
                            sTemp1 = "... Press F5 to continue...";
                            // System.Windows.Forms.MessageBox.Show(Message.Text + sTemp1);
                            Debugger.Break();
                        }
                        else
                        {
                            sTemp1 = "... Click OK...";
                            // System.Windows.Forms.MessageBox.Show(TraceMessage + sTemp1);
                            if (System.Windows.Forms.MessageBox.Show(Message.Text + sTemp1, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                // user clicked yes
                            }
                            else
                            {
                                // user clicked no
                                // ToDo add cancel code here...
                                Debugger.Break();
                            }
                        }
                    }
                    LocalUserEntry += sTemp1;
                    // ToDo LocalUserEntry TraceMdmDoPrint(LocalUserEntry)
                    // TOTO LocalUserEntry INPUT RESPONSE HERE
                }
                else { LocalUserEntry = sEmpty; }
                #region Error
                //if (PassedError == ErrorDidOccur) {
                //    TraceMessage = TraceMessageTarget + "Error";
                //    TraceMdmDoPrint(ref Sender);
                //}
                if (Message.IsError)
                {
                    // ToDo Do Logging Here
                    // ToDo NO! This is on the UI thread!
                }
                #endregion
                RunPauseCheck();
                #endregion
                //
                Message.ResponseId = 0;
                LocalUserEntryLong = 0;
                TraceMessage = sEmpty;
                TraceMessageSuffix = sEmpty;
                TraceMessagePrefix = sEmpty;
                //
            }
            // return TraceMdmPoint;
        }
        #region Do Print Multiline, Do Call
        /// <summary>
        /// Called by TraceMdmDo to preserve margin text for multiline messages
        /// </summary> 
        /// <param name="Sender"></param> 
        /// <param name="PassedTraceMessage">Multiline message to process</param> 
        /// <param name="PassedTracePrefix">Text Prefix for message lines</param> 
        /// <remarks>
        /// </remarks> 
        public void TraceMdmDoPrintMultiLine(ref Object Sender, String PassedTraceMessage, String PassedTracePrefix)
        {
            int Tml = PassedTraceMessage.Length;
            while (Tml > 0)
            {
                if (Tml > 100)
                {
                    Tml = 80;
                    TraceMessageToPrint = PassedTracePrefix + PassedTraceMessage.Substring(0, Tml) + " +++";
                    PassedTraceMessage = PassedTraceMessage.Substring(80);
                }
                else
                {
                    TraceMessageToPrint = PassedTracePrefix + PassedTraceMessage;
                    PassedTraceMessage = sEmpty;
                }
                st.TraceMdmDoCall();
                Tml = PassedTraceMessage.Length;
            }
            if ((Message.IsError && (TraceDebugOn && TraceDebugDoErrorPrompt))
                | TraceBreakOnAll)
            {
                //System.Diagnostics.Debug.WriteLine(TraceMessageToPrint);
                //if (TraceBreakOnAll) { Debugger.Break(); }
                Debugger.Break();
            }
        }
        /// <summary>
        /// Called by TraceMdmDo to process messages
        /// </summary> 
        public void TraceMdmDoPrint(ref Object Sender)
        {
            String sTemp = TraceMessage;
            String sTemp1 = sEmpty;
            TraceMessageTarget = "C";
            //String sTemp = sEmpty;
            //String sTemp1 = sEmpty;
            //try
            //{
            //    if (TraceMessage.Length == 2)
            //    {
            //        LocalMessage.Msg0 = "Length 2";
            //    }
            //    else if (TraceMessage.Length == 1)
            //    {
            //        LocalMessage.Msg0 = "Length 1";
            //    }
            //    else if (TraceMessage.Length < 5)
            //    {
            //        LocalMessage.Msg0 = "Length < 5";
            //    }
            //    TraceMessageTarget = TraceMessage.Substring(0, 1);
            //    sTemp = TraceMessage.Substring(1);
            //    if (TraceMessageTarget == "M" || TraceMessageTarget == "A")
            //    {
            //        TraceMessageTarget = TraceMessage.Substring(0, 2);
            //        sTemp = TraceMessage.Substring(2);
            //    }
            //}
            //catch
            //{
            //    TraceMessageTarget = "C";
            //}
            // Data
            TraceMessageFormated = TraceMessageTarget;
            if (TraceData) { TraceMessageFormated += TraceDataPointers; }
            if (TraceHeadings)
            {
                if (sProcessHeading.Length > 0)
                {
                    TraceMessageFormated += sProcessHeading + ", " + sProcessSubHeading + ": ";
                }
                else
                {
                    TraceMessageFormated += Sender.ToString() + ": ";
                }
            }
            //
            if (ConsoleToControl)
            {
                try
                {
                    ConsoleFormRowUpdate(Message.ConsoleFormUse, RowString, false, Message.Verbosity, Message.IsError);
                    ConsoleFormRowUpdate(ConsoleFormUses.All, RowString, false, Message.Verbosity, Message.IsError);
                    //((ImTrace)ConsoleSender).StdConsoles.Consoles[Message.ConsoleFormUse].ConsoleForm.Cf.Rows.Add(rowString);
                    //((ImTrace)ConsoleSender).StdConsoles.Consoles[Message.ConsoleFormUse].ConsoleForm.Refresh();
                    //((ImTrace)ConsoleSender).StdConsoles.Consoles[ConsoleFormUses.All].ConsoleForm.Cf.Rows.Add(rowString);
                    //((ImTrace)ConsoleSender).StdConsoles.Consoles[ConsoleFormUses.All].ConsoleForm.Refresh();
                }
                catch (Exception) { throw; }
            }

            sTemp1 = TraceMessagePrefix + sTemp + TraceMessageSuffix;
            if (sTemp1.Length > 160)
            {
                st.TraceMdmDoPrintMultiLine(ref Sender, sTemp1, sEmpty);
                // ((ImTrace)ConsoleSender).TraceMdmDoPrintMultiLine(ref Sender, sTemp1, TraceMessageFormated);
            }
            else
            {
                TraceMessageToPrint = sTemp1;
                // TraceMessageToPrint = TraceMessageFormated + sTemp1;
                st.TraceMdmDoCall();
            }
        }
        /// <summary>
        /// Call the either the diagnostic system or 
        /// ConsoleMdmPickDisplayImpl or
        /// ConsoleMdmStd_IoWriteImpl
        /// </summary> 
        /// <remarks>
        /// This area needs more work in the future.
        /// Design finalization needs to occur for the
        /// logging and debugging aids as well as
        /// key considerations in the pick virtualization
        /// implementation (ie. WPF, vs HTML vs
        /// rendering)
        /// </remarks> 
        public void TraceMdmDoCall()
        {
            // System.Console.WriteLine(((ImTrace)ConsoleSender).ToString() + ">>>");
            // Display Message
            // Three possible output
            // Debug
            // PickConsole or Trace
            // Regular Console STD IO
            if (ConsoleOn & (ConsoleTextOn | ConsoleBasicOn))
            {
                System.Console.WriteLine(TraceMessageToPrint);
            }
            if (ConsoleOn & ConsoleToDisc)
            {
                TraceMdmPointResult = ConsoleMdmStd_IoWriteImpl(TraceMessageToPrint);
            }
            if (ConsoleToControl)
            {

            }
            if ((Message.IsError && (TraceDebugOn && TraceDebugDoErrorPrompt))
                | TraceBreakOnAll)
            {
                System.Diagnostics.Debug.WriteLine(TraceMessageToPrint);
                //if (TraceBreakOnAll) { Debugger.Break(); }
                //Debugger.Break();
            }
            // User Promptin and Entry
            // Done in TraceMdm
            // TraceMessageFormated += LocalUserEntry;
        }
        #endregion
        #endregion
    }
}
