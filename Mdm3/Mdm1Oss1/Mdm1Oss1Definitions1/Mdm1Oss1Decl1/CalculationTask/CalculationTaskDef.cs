using System;
using System.Collections;
//using System.Collections.Concurrent; // Not in Net 35
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Drawing;
//using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

// add shell32.dll reference
// or COM Microsoft Shell Controls and Automation
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.File;
using Mdm.Oss.File.Control;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.Thread;

namespace Mdm.Oss.Thread
{
    class mThreadCore
    {
        public CalculationTaskDef CalculationTask;
        public CalculationTaskDef.CalculationStringDelegate CalculationTaskDel;
        #region Test Objects
        private string AnyCLassCalculation(int count)
        {
            string result = "";
            for (int i = 0; i < count; i++)
            {
                // Long calculation
                // Check for cancel.
                if (CalculationTask.CalcState == CalculationStatus.CancelPending) break;

                // Update Progress
                CalculationTask.FireProgressChangedEvent(i);
            }
            return result;
        }
        private void AnyClassInitialize()
        {
            // Create new task object to manage the calculation.
            // StdConsoleManagerDef st, long ClassFeaturesIsThis
            // st, ClassFeaturesIsThis
            // ToDo These are all null:
            object Sender = null;
            iTrace st = null;
            object ConsoleSender = null;
            ConsoleSourceIs ConsoleSource = ConsoleSourceIs.None;
            ClassFeatureIs ClassFeatures = ClassFeatureIs.None;
            ClassRoleIs ClassRole = ClassRoleIs.None;
            // st, ClassFeaturesIsThis
            CalculationTask = new CalculationTaskDef(ref Sender, ref ConsoleSender, ConsoleSource, ClassRole, ClassFeatures);

            // Subscribe to the calculation status event.
            CalculationTask.CalculationStatusChanged += new
              CalculationTaskDef.CalculationStatusEventHandler(OnCalculationStatusChanged);

            // Subscribe to the calculation progress event.
            CalculationTask.CalculationProgressChanged += new
              CalculationTaskDef.CalculationProgressEventHandler(OnCalculationProgressChanged);

            // Create a delegate to the calculation method.
            CalculationTaskDel =
                    new CalculationTaskDef.CalculationStringDelegate(AnyCLassCalculation);

        }
        private void AnyClassMain()
        {
            AnyClassInitialize();
            CalculationTask.StartCalculation(1, (CalculationTaskDef.CalculationStringDelegate)CalculationTaskDel);
            CalculationTask.StopCalculation();
        }
        #endregion
        private void CalculationStatusChanged(object sender, CalculationEventArgs e)
        {
            if (e.SetButtons)
            {
                switch (e.Status)
                {
                    case CalculationStatus.Calculating:
                        //button1.Enabled = false;
                        //button2.Enabled = true;
                        break;

                    case CalculationStatus.NotCalculating:
                        //button1.Enabled = true;
                        //button2.Enabled = false;
                        break;

                    case CalculationStatus.CancelPending:
                        //button1.Enabled = false;
                        //button2.Enabled = false;
                        break;
                }
            }
        }
        private void OnCalculationProgressChanged(object sender, CalculationEventArgs e)
        {
            //_progressBar.Value = e.Progress;
        }
        private void OnCalculationStatusChanged(object sender, CalculationEventArgs e)
        {
            //_progressBar.Value = e.Progress;
            CalculationStatusChanged(sender, e);
        }
        private void ButtonStart_Click(object sender, System.EventArgs e)
        {
            CalculationTask.StartCalculation(1000, CalculationTaskDel);
        }
        private void ButtonStop_Click(object sender, System.EventArgs e)
        {
            CalculationTask.StopCalculation();
        }

    }
    public static class CalculationTaskUtils
    {
        #region Wait / Check Busy
        public static StateIs WaitCheckBusy(object st, int ThreadId)
        {
            StateIs FileSqlBusyCheckResult = StateIs.Started;
            FileSqlBusyCheckResult = StateIs.DoesNotExist;
            try
            {
                // if (CalculationTaskDictBusy == null) { CalculationTaskDictBusy = new Mutex(); }
                //lock(CalculationTaskDict)
                {
                    if (ThreadId >= 0)
                    {
                        string ThreadIdCurr = ThreadId.ToString();
                        int ThreadIndexCurr = -1;

                        CalculationTaskDef.CalculationTaskDictBusyLock(true);
                        if (CalculationTaskDef.CalculationTaskDict.ContainsKey(ThreadIdCurr))
                        {
                            lock (CalculationTaskDef.CalculationTaskDict[ThreadIdCurr])
                            {
                                if (CalculationTaskDef.CalculationTaskDict[ThreadIdCurr] != null
                                && (((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadIdCurr]).CalcState == CalculationStatus.Calculating
                                || ((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadIdCurr]).CalcState == CalculationStatus.Pending))
                                {
                                    FileSqlBusyCheckResult = StateIs.DoesExist;
                                    string TraceMessage = "Calculation Task ThreadId " + ThreadIdCurr + " (" + ((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadIdCurr]).TaskType + ")  Checked Thread Busy.";
                                    ((iTrace)st).TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
                                }
                            }
                            CalculationTaskDef.CalculationTaskDictBusyUnlock(true);
                        }
                        else
                        {
                            CalculationTaskDef.CalculationTaskDictBusyUnlock(true);
                            FileSqlBusyCheckResult = StateIs.Failed;
                            // ToDo static message method required (Basic).
                            string TraceMessage = "Calculation Task ThreadId " + ThreadIdCurr + " Checked Dictionary Thread Key does not exit!";
                            ((iTrace)st).TraceMdmDoBasic(TraceMessage, ConsoleFormUses.ErrorLog, 13);
                            return FileSqlBusyCheckResult;
                        }
                    }
                    else if (CalculationTaskDef.CalculationTaskDict != null)
                    {
                        bool cont = true;
                        string ThreadIdCurr;
                        foreach (KeyValuePair<string, object> ThreadCurr in CalculationTaskDef.CalculationTaskDict)
                        {
                            ThreadIdCurr = ThreadCurr.Key.ToString();
                            CalculationTaskDef.CalculationTaskDictBusyLock(true);
                            lock (CalculationTaskDef.CalculationTaskDict[ThreadIdCurr])
                            {
                                if (ThreadCurr.Value != null
                                && (((CalculationTaskDef)ThreadCurr.Value).CalcState == CalculationStatus.Calculating
                                || ((CalculationTaskDef)ThreadCurr.Value).CalcState == CalculationStatus.Pending))
                                {
                                    // Busy Thread hit
                                    FileSqlBusyCheckResult = StateIs.DoesExist;
                                    string TraceMessage = "Calculation Task ThreadId " + ThreadIdCurr 
                                        + " (" + ((CalculationTaskDef)ThreadCurr.Value).TaskType 
                                        + ")  Checked. Thread(s) are Busy.";
                                    ((iTrace)st).TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
                                    break;
                                }
                            }
                            CalculationTaskDef.CalculationTaskDictBusyUnlock(true);
                        }
                        
                        //for (int ThreadIdCurrInt = 1; cont; ThreadIdCurrInt++)
                        //{
                        //    CalculationTaskDef.CalculationTaskDictBusyLock(true);
                        //    ThreadIdCurr = ThreadIdCurrInt.ToString();
                        //    if (CalculationTaskDef.CalculationTaskDict.ContainsKey(ThreadIdCurr))
                        //    {
                        //        lock (CalculationTaskDef.CalculationTaskDict[ThreadIdCurr])
                        //        {
                        //            if (CalculationTaskDef.CalculationTaskDict[ThreadIdCurr] != null
                        //            && (((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadIdCurr]).CalcState == CalculationStatus.Calculating
                        //            || ((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadIdCurr]).CalcState == CalculationStatus.Pending))
                        //            {
                        //                // Busy Thread hit
                        //                FileSqlBusyCheckResult = StateIs.DoesExist;
                        //                string TraceMessage = "Calculation Task ThreadId " + ThreadIdCurr + " (" + ((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadIdCurr]).TaskType + ")  Checked Thread Busy.";
                        //                st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
                        //                break;
                        //            }
                        //        }
                        //    }
                        //    else { cont = false; }
                        //    CalculationTaskDef.CalculationTaskDictBusyUnlock(true);
                        //}
                    } else
                    {
                        CalculationTaskDef.CalculationTaskDict = new Dictionary<string, object>();
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return FileSqlBusyCheckResult;
        }
        public static StateIs WaitForBusy(iTrace st, int ThreadIdPassed, int WaitMax)
        {
            string ThreadId = ThreadIdPassed.ToString();
            bool cont = true;
            StateIs result;
            int WaitCn;

            //for (int i = 0;
            //    cont;
            //    i++)
            //{
            //    lock (CalculationTaskList[ThreadId])
            //    {
            //        if (!CalculationTaskList[ThreadId].FileSqlBusy) { cont = false; }
            //        CalculationTaskList[ThreadId].FileSqlLock.WaitOne();
            //        CalculationTaskList[ThreadId].FileSqlLock.ReleaseMutex();
            //    }
            //}
            //lock (CalculationTaskDictLockObject)
            {
                if (!CalculationTaskDef.CalculationTaskDict.ContainsKey(ThreadId))
                {
                    result = StateIs.Failed;
                    string TraceMessage = "Calculation Task Thread " + ThreadId + " Key does not exit";
                    st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.ErrorLog, 13);
                    return result;
                }
            }
            System.Threading.Thread.Sleep(0);
            // System.Threading.Thread.Yield(); // not in Net35
            // System.Threading.Thread.Sleep(50);
            for (WaitCn = 0;
                cont;
                WaitCn++)
            {
                //lock (CalculationTaskDict[ThreadId])
                {
                    if (CalculationTaskDef.CalculationTaskDict[ThreadId] == null
                        || (((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadId]).CalcState != Thread.CalculationStatus.Calculating
                        && ((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadId]).CalcState != Thread.CalculationStatus.Pending)
                        ) { cont = false; }
                    if (WaitCn >= WaitMax / 400)
                    {
                        result = StateIs.Failed;
                        string TraceMessage = "Calculation Task ThreadId " + ThreadId + " Wait time out error!";
                        st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.ErrorLog, 13);
                        return result;
                    }
                }
                if (cont)
                {
                    string TraceMessage = "Calculation Task Thread " + ThreadId
                        + " (" + ((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadId]).TaskType + ")"
                        + " is busy (" + WaitCn.ToString() + ")...";
                    System.Threading.Thread.Sleep(0);
                    //System.Threading.Thread.Yield();
                    st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
                    System.Threading.Thread.Sleep(0);
                    //System.Threading.Thread.Yield();
                    System.Threading.Thread.Sleep(25);
                }
            }

            //lock (CalculationTaskDict[ThreadId])
            {
                if (CalculationTaskDef.CalculationTaskDict[ThreadId] != null
                && (((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadId]).CalcState == Thread.CalculationStatus.Calculating
                || ((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadId]).CalcState == Thread.CalculationStatus.Pending))
                {
                    // The thread is still busy
                    result = StateIs.Failed;
                }
                else
                {
                    // The thread is now available or finished
                    result = StateIs.Successful;
                }
            }
            //if (DbTaskIdBusy == ThreadIdPassed)
            //{

            //}
            return result;
        }
        // NOT USED:
        public static object WaitGetResult(iTrace st, int ThreadIdPassed, object DataPassed, bool GetData, bool NewData)
        {
            object result = null;
            string TraceMessage;
            string ThreadId = ThreadIdPassed.ToString();
            object DataLoad;

            try
            {
                TraceMessage = "Calculation Task ThreadId " + ThreadId + ", " + DataPassed.GetType().ToString() + ", data " + GetData.ToString() + ", Wait for Result...";
                st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);

                // lock (DbTaskDef.CalculationTaskDict[ThreadId])
                {
                    CalculationTaskUtils.WaitForBusy(st, ThreadIdPassed, 10000);

                    result = ((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadId]).StdResultLong;
                    // Note that either the data exists or it is a new record and Id is being set.
                    // or finally, the long result is being retrieved.
                    // ToDo needs updating to return string as well.  Need thinking through on usage...
                    if (GetData)
                    {
                        DataLoad = ((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadId]).ObjectResult;

                        if (DataLoad != null)
                        {
                            DataPassed = DataLoad;
                        }
                        else
                        {
                            if (NewData)
                            {
                                // PassedData = null;
                                DataPassed = DataPassed.GetType().GetConstructor(new Type[] { }).Invoke(new object[] { });
                            }
                        }
                    }
                    if (((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadId]).NewRecord)
                    {
                        ((IDbTaskDataId)DataPassed).Id = (int)((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[ThreadId]).Id;
                    }
                    else
                    {
                        // ((IDbTaskDataId)DataLoad).Id = -1;
                    }

                    if (GetData) { return DataPassed; }

                    //((CalculationTaskDef)DbTaskDef.CalculationTaskDict[ThreadId]).WaitLockMutex.ReleaseMutex();

                }
            }
            catch (Exception e)
            {

                // throw;
            }

            TraceMessage = "Calculation Task ThreadId " + ThreadId + " Finished Result (" + result.ToString() + ")";
            st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
            return result;
        }
        #endregion
        #region Generic Objects
        public static object GetNewObject(Type t)
        {
            try
            {
                return t.GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
            catch
            {
                return null;
            }
        }
        // Here is the same approach, contained in a generic method:
        public static T GetNewObject<T>()
        {
            try
            {
                return (T)typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
            catch
            {
                return default;
            }
        }
        #endregion
    }
    public class CalculationTaskDef 
        : StdBaseRunFileDef, IDisposable
        //: StdConsoleManagerDef, IDisposable
    {
        #region Data and properties
        // public static List<CalculationTaskDef> CalculationTaskList;
        // public static Dictionary<string, CalculationTaskDef> CalculationTaskDict;
        // Net35
        public static Dictionary<string, object> CalculationTaskDict;
        // Not Net35 compatible.
        // public static ConcurrentDictionary<string, object> CalculationTaskDict;
        public static volatile int CalculationTaskCounter;

        public static Object CalculationTaskDictLockObject;
        public static Mutex CalculationTaskDictBusyMutex;

        public static volatile int DbTaskIdBusyValue;
        public new volatile DataStatusIs DataStatus;

        public volatile bool WaitIsBusy;
        public Mutex WaitLockMutex;
        public static int DbTaskIdBusy
        {
            get
            {
                return DbTaskIdBusyValue;
            }

            set
            {
                //if (st != null)
                //{
                //    string TraceMessage = "Calculation Task DbTaskIdBusy (" + DbTaskIdBusyValue.ToString() + ") now set to " + value.ToString();
                //    st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
                //}
                DbTaskIdBusyValue = value;
            }
        }
        public bool NewRecord;
        public string TaskType;
        private volatile CalculationStatus mCalcState;
        public CalculationStatus CalcState
        {
            get { return mCalcState; }
            set { mCalcState = value; }
        }
        public bool SetButtons;

        public int RowCount;

        // the use of a long result is to implement Status / State handling per the framework. (Success / Failure)
        public object ObjectResult;
        public StateIs StdResultLong;
        public string StringResult;

        //public StdConsoleManagerDef st = null;
        //public ClassRoleIs ClassRole = ClassRoleIs.None;
        //public ClassFeatureIs ClassFeatures = ClassFeatureIs.None;
        #endregion
        #region delgates and events
        public delegate string CalculationStringDelegate(int count);
        public delegate StateIs CalculationLongDelegate(object data);
        public delegate object CalculationObjectDelegate(object sender, object data);

        public delegate void CalculationStatusEventHandler(
                        object sender, CalculationEventArgs e);
        public event CalculationStatusEventHandler CalculationStatusChanged;

        public delegate void CalculationProgressEventHandler(
                        object sender, CalculationEventArgs e);
        public event CalculationProgressEventHandler CalculationProgressChanged;

        public CalculationStringDelegate CalculationStringDel;
        public CalculationLongDelegate CalculationLongDel;
        public CalculationObjectDelegate CalculationObjectDel;
        #endregion
        #region Class Members
        public virtual DataStatusIs GetStatus()
        {
            return DataStatus;
        }
        // Mutex Handling and testing
        public static void CalculationTaskDictBusyLock(bool UniqueSig)
        {
            //Thread safe dictionary... not required
            //CalculationTaskDictBusyMutex.WaitOne();
        }
        public static void CalculationTaskDictBusyLock(CalculationTaskDef sender, iTrace st, bool UniqueSig)
        {
            string TraceMessage = "Calculation Task ThreadId " + sender.Id.ToString() + " Lock Mutex.";
            st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
            CalculationTaskDictBusyLock(true);
        }
        public void CalculationTaskDictBusyLock()
        {
            if (st != null)
            {
                string TraceMessage = "Calculation Task ThreadId " + Id.ToString() + " Lock Mutex.";
                ((iTrace)st).TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
            }
            CalculationTaskDictBusyLock(true);
        }
        public static void CalculationTaskDictBusyUnlock(bool UniqueSig)
        {
            //Thread safe dictionary... not required
            //CalculationTaskDictBusyMutex.ReleaseMutex();
        }
        public void CalculationTaskDictBusyUnlock()
        {
            string TraceMessage = "Calculation Task ThreadId " + Id.ToString() + " Unlock Mutex.";
            ((iTrace)st).TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
            CalculationTaskDictBusyUnlock(true);
        }
        #endregion
        #region Constructors and Init / Dispose
        static CalculationTaskDef()
        {
            // Note: not sure if the dict should get locked or
            // if I should continue locking the task object (this).
            CalculationTaskDictLockObject = new object();
            // CalculationTaskDictBusyLock();
            // lock (this)
            // a locked regular dictionary is more performant.
            // CalculationTaskDictLockObject = new object();
            lock (CalculationTaskDictLockObject)
            {
                //if (CalculationTaskDict == null)
                //{
                    //CalculationTaskList = new List<CalculationTaskDef>();
                    CalculationTaskDict = new Dictionary<string, object>();
                    // CalculationTaskDict = new ConcurrentDictionary<string, object>();
                //}
                CalculationTaskCounter = 0;
                CalculationTaskDictLockObject = new object();
                CalculationTaskDictBusyMutex = new Mutex();
            }
            // st = new StdConsoleManagerDef(ClassRoleIs.None, ClassFeatureIs.None);
        }
        public CalculationTaskDef(ref object SenderPassed, ref object ConsoleSenderPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
               : base(ref SenderPassed, ref ConsoleSenderPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        {
            SetButtons = (((iTrace)st).StdRunControlUiGet()).SetButtons;
            CalculationTaskDictBusyLock();
            lock (CalculationTaskDictLockObject)
            {
                //CalculationTaskList.Add(this);
                // CalculationTaskDict.Add(Id.ToString()
                CalculationTaskCounter += 1;
                Id = CalculationTaskCounter;
                // CalculationTaskDict.TryAdd(Id.ToString(), (object)this); // Not in Net35
                try
                {
                    CalculationTaskDict.Add(Id.ToString(), (object)this); // Not in Net35
                }
                catch (Exception) { }
                //st = stPassed;
                //ConsoleSource = ConsoleSourcePassed;
                //ClassRole = ClassRolePassed;
                //ClassFeatures = ClassFeaturesPassed;
                InitializeCalculationTask();
                // ClassFeatures = ClassFeaturesIsThisPassed;
            }
            CalculationTaskDictBusyUnlock();
        }
        public virtual void InitializeCalculationTask()
        {
            SetButtons = ((iTrace)st).StdRunControlUiGet().SetButtons;
            base.InitializeStdBaseRunFile(); // Console Up call?
            string TraceMessage = "Calculation Task ThreadId " + Id.ToString() + " (" + TaskType + ")  Dictionary Thread Key Initialized.";
            ((iTrace)st).TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
        }
        public new void Dispose()
        {
            CalculationTaskDictBusyLock();
            lock (CalculationTaskDictLockObject)
            {
                //CalculationTaskList.Remove(this);
                object temp;
                // CalculationTaskDict.TryRemove(Id.ToString(), out temp);
                try
                {
                    bool bResult = CalculationTaskDict.Remove(Id.ToString());
                }
                catch (Exception) { }
                // CalculationTaskCounter -= 1; // This would cause overwrites.
            }
            CalculationTaskDictBusyUnlock();
            base.Dispose();
            string TraceMessage = "Calculation Task ThreadId " + Id.ToString() + " (" + TaskType + ")  Dictionary Thread Key Disposed.";
            ((iTrace)st).TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
        }
        #endregion
        #region Start, Stop, End
        public void StartCalculation(object PassedSender, object PassedData, CalculationObjectDelegate PassedDoCalculate)
        {
            lock (CalculationTaskDictLockObject)
            {
                //StartCalculateWaitLock();
                //if (WaitLockMutex == null) { WaitLockMutex = new Mutex(); }
                if (CalcState == CalculationStatus.NotCalculating)
                {
                    // The cacl delegate is now created in the UI thread.
                    // ToDo is this a potential problem? Yes. Set in client.
                    // Create a delegate to the calculation method.
                    //CalculationDelegate calc =
                    //        new CalculationDelegate(AnyCLassCalculation);

                    // Start the calculation.
                    PassedDoCalculate.BeginInvoke(PassedSender, PassedData,
                            new AsyncCallback(EndCalculateObject), PassedDoCalculate);

                    // Update the calculation status.
                    CalcState = CalculationStatus.Calculating;

                    // Fire a status changed event.
                    FireStatusChangedEvent(CalcState, SetButtons);
                }
                else
                {
                    string TraceMessage = "Calculation Task ThreadId " + Id.ToString() + " (" + TaskType + ")  Start Calculation Object Error!";
                    ((iTrace)st).TraceMdmDoBasic(TraceMessage, ConsoleFormUses.ErrorLog, 13);
                }
            }
        }
        public void StartCalculation(object PassedData, CalculationLongDelegate PassedDoCalculate)
        {
            lock (CalculationTaskDictLockObject)
            {
                //StartCalculateWaitLock();
                //if (WaitLockMutex == null) { WaitLockMutex = new Mutex(); }
                if (CalcState == CalculationStatus.NotCalculating)
                {
                    // The cacl delegate is now created in the UI thread.
                    // Create a delegate to the calculation method.
                    //CalculationDelegate calc =
                    //        new CalculationDelegate(AnyCLassCalculation);

                    // Update the calculation status.
                    CalcState = CalculationStatus.Pending;
                    // Fire a status changed event.
                    FireStatusChangedEvent(CalcState, SetButtons);

                    // Start the calculation.
                    PassedDoCalculate.BeginInvoke(PassedData,
                            new AsyncCallback(EndCalculateLong), PassedDoCalculate);
                }
                else
                {
                    string TraceMessage = "Calculation Task ThreadId " + Id.ToString() + " (" + TaskType + ")  Start Calculation Long Error!";
                    ((iTrace)st).TraceMdmDoBasic(TraceMessage, ConsoleFormUses.ErrorLog, 13);
                }
            }
        }
        public void StartCalculation(int PassedCount, CalculationStringDelegate PassedDoCalculate)
        {
            lock (CalculationTaskDictLockObject)
            {
                //StartCalculateWaitLock();
                //if (WaitLockMutex == null) { WaitLockMutex = new Mutex(); }
                if (CalcState == CalculationStatus.NotCalculating)
                {
                    // The cacl delegate is now created in the UI thread.
                    // Create a delegate to the calculation method.
                    //CalculationDelegate calc =
                    //        new CalculationDelegate(AnyCLassCalculation);

                    // Start the calculation.
                    PassedDoCalculate.BeginInvoke(PassedCount,
                            new AsyncCallback(EndCalculateString), PassedDoCalculate);

                    // Update the calculation status.
                    CalcState = CalculationStatus.Calculating;

                    // Fire a status changed event.
                    FireStatusChangedEvent(CalcState, SetButtons);
                }
                else
                {
                    string TraceMessage = "Calculation Task ThreadId " + Id.ToString() + " (" + TaskType + ")  Start Calculation String Error!";
                    ((iTrace)st).TraceMdmDoBasic(TraceMessage, ConsoleFormUses.ErrorLog, 13);
                }
            }
        }
        private void EndCalculateMsgDo(string ResultMsgPassed)
        {
            string TraceMessage = "Calculation Task ThreadId " + Id.ToString() + " (" + TaskType + ") " + ResultMsgPassed;
            ((iTrace)st).TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
        }
        public void StartCalculateWaitLock()
        {
            if (WaitLockMutex == null) { WaitLockMutex = new Mutex(); }
            if (!WaitIsBusy)
            {
                WaitLockMutex.WaitOne();
                WaitIsBusy = true;
            }
        }
        public void EndCalculateWaitRelease()
        {
            if (WaitIsBusy)
            {
                WaitLockMutex.ReleaseMutex();
                WaitIsBusy = false;
            }
        }
        public void StopCalculation()
        {
            lock (CalculationTaskDictLockObject)
            {
                if (CalcState == CalculationStatus.Calculating)
                {
                    // Update the calculation status.
                    CalcState = CalculationStatus.CancelPending;

                    // Fire a status changed event.
                    FireStatusChangedEvent(CalcState, SetButtons);
                }
                else
                {
                    string TraceMessage = "Calculation Task ThreadId " + Id.ToString() + " (" + TaskType + ")  Stop Calculation Error!";
                    ((iTrace)st).TraceMdmDoBasic(TraceMessage, ConsoleFormUses.ErrorLog, 13);
                }
            }
        }
        //private string Calculate(int count);
        private void EndCalculateObject(IAsyncResult ar)
        {
            string TraceMessage = "Calculation Task ThreadId " + Id.ToString() + " (" + TaskType + ")  End Calculation Object...";
            ((iTrace)st).TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);
            lock (CalculationTaskDictLockObject)
            {
                //}
                // ToDo Need a try for bad dispose handling
                CalculationObjectDelegate del = (CalculationObjectDelegate)ar.AsyncState;
                object result = del.EndInvoke(ar);
                ObjectResultDef ObjectResultObject = result as ObjectResultDef;
                if (ObjectResultObject == null)
                {

                    return;
                }

                //lock (CalculationTaskDictLockObject)
                //{
                StdResultLong = ObjectResultObject.StdResultLong;
                ObjectResult = ObjectResultObject.ObjectResult;
                Id = ObjectResultObject.Id;
                ItemId = ObjectResultObject.ItemId;
                NewRecord = ObjectResultObject.NewRecord;

                CalcState = CalculationStatus.NotCalculating;
                FireStatusChangedEvent(CalcState, SetButtons);
                EndCalculateMsgDo("End Calculation Object result (" + StdResultLong.ToString() + ")");
                EndCalculateWaitRelease();
            }
        }
        private void EndCalculateLong(IAsyncResult ar)
        {
            CalculationLongDelegate del = (CalculationLongDelegate)ar.AsyncState;
            lock (CalculationTaskDictLockObject)
            {
                StdResultLong = del.EndInvoke(ar);

                CalcState = CalculationStatus.NotCalculating;
                FireStatusChangedEvent(CalcState, SetButtons);
                EndCalculateMsgDo("End Calculation Long result (" + StdResultLong.ToString() + ")");
                EndCalculateWaitRelease();
            }
        }
        private void EndCalculateString(IAsyncResult ar)
        {
            CalculationStringDelegate del = (CalculationStringDelegate)ar.AsyncState;
            lock (CalculationTaskDictLockObject)
            {
                StringResult = del.EndInvoke(ar);

                CalcState = CalculationStatus.NotCalculating;
                FireStatusChangedEvent(CalcState, SetButtons);
                EndCalculateMsgDo("End Calculation String result(" + StdResultLong.ToString() + ")");
                EndCalculateWaitRelease();
            }
        }
        #endregion
        #region Fire Events
        protected StateIs FireStatusChangedEventResult;
        public void FireStatusChangedEvent(CalculationStatus PassedStatus, bool PassedSetButtons)
        {
            FireStatusChangedEventResult = StateIs.Started;
            #region Mutex
            //switch (PassedStatus)
            //{
            //    case CalculationStatus.Calculating:
            //        if (WaitLockMutex == null) { WaitLockMutex = new Mutex(); }
            //        if (!WaitIsBusy)
            //        {
            //            WaitLockMutex.WaitOne();
            //            WaitIsBusy = true;
            //        }
            //        break;
            //    case CalculationStatus.NotCalculating:
            //        break;
            //    case CalculationStatus.CancelPending:
            //    case CalculationStatus.PausePending:
            //    case CalculationStatus.Paused:
            //    case CalculationStatus.ResumePending:
            //    case CalculationStatus.Pending:
            //    default:
            //        break;
            //}
            #endregion
            #region Fire Status Change
            if (CalculationStatusChanged != null)
            {
                string MethodName = CalculationStatusChanged.Method.Name;
                Type TypeCurr = CalculationStatusChanged.Target.GetType();
                MethodInfo MethodCurr = TypeCurr.GetMethod(MethodName);
                //
                CalculationEventArgs args = new CalculationEventArgs(this, PassedStatus, PassedSetButtons);
                if (CalculationStatusChanged.Target is System.Windows.Forms.Control)
                {
                    Control targetForm = CalculationStatusChanged.Target
                            as System.Windows.Forms.Control;
                    targetForm.BeginInvoke(CalculationStatusChanged,
                            new object[] { this, args });
                }
                else if (CalculationStatusChanged.Target is System.Windows.Forms.Form)
                {
                    Form targetForm = CalculationStatusChanged.Target
                            as System.Windows.Forms.Form;
                    targetForm.BeginInvoke(CalculationStatusChanged,
                            new object[] { this, args });
                }
                else if (CalculationStatusChanged.Target is iClassFeatures)
                {
                    Form targetForm = ((iClassFeatures)CalculationStatusChanged.Target).FormParentGet()
                            as System.Windows.Forms.Form;
                    if (targetForm != null)
                    {
                        targetForm.BeginInvoke(CalculationStatusChanged,
                                new object[] { this, args });
                    }
                }
                else {
                    object ResultCurr = MethodCurr.Invoke(CalculationStatusChanged.Target,
                            new object[] { this, args });
                    //
                    // CalculationStatusChanged(this, args);
                    // Notes:
                    // CalculationStatusChanged.Target.BeginInvoke(CalculationStatusChanged,
                    // typeof(Test).GetMethod("Hello").Invoke(t, new[] { "world" });
                    // alternative if you don't know the type of the object:
                    // t.GetType().GetMethod("Hello").Invoke(t, new[] { "world" });
                    // 
                    // Application targetForm = CalculationStatusChanged.Target
                    //        as System.Windows.Application;
                    // targetForm.BeginInvoke(CalculationStatusChanged,
                    //        new object[] { this, args });
                    // CalculationStatusChanged.Target
                }
            }
            #endregion
            #region Mutex
            //switch (PassedStatus)
            //{
            //    case CalculationStatus.Calculating:
            //        break;
            //    case CalculationStatus.NotCalculating:
            //        if (WaitLockMutex == null) { WaitLockMutex = new Mutex(); }
            //        if (WaitIsBusy)
            //        {
            //            WaitLockMutex.ReleaseMutex();
            //            WaitIsBusy = false;
            //        }
            //        break;
            //    case CalculationStatus.CancelPending:
            //    case CalculationStatus.PausePending:
            //    case CalculationStatus.Paused:
            //    case CalculationStatus.ResumePending:
            //    case CalculationStatus.Pending:
            //    default:
            //        break;
            //}
            #endregion
            FireStatusChangedEventResult = StateIs.Finished;
        }
        public void FireProgressChangedEvent(int PassedDirectoryProgress, int PassedFileProgress, int PassedSelectedProgress)
        {

            if (CalculationProgressChanged != null)
            {
                CalculationEventArgs args =
                    new CalculationEventArgs(PassedDirectoryProgress, PassedFileProgress, PassedSelectedProgress);
                if (CalculationProgressChanged.Target is
                        System.Windows.Forms.Control)
                {
                    Control targetForm = CalculationProgressChanged.Target
                            as System.Windows.Forms.Control;
                    targetForm.BeginInvoke(CalculationProgressChanged,
                            new object[] { this, args });
                }
                else
                {
                    CalculationProgressChanged(this, args);
                }
            }
        }
        public void FireProgressChangedEvent(int PassedProgress)
        {
            if (CalculationProgressChanged != null)
            {
                CalculationEventArgs args =
                    new CalculationEventArgs(PassedProgress);
                if (CalculationProgressChanged.Target is
                        System.Windows.Forms.Control)
                {
                    Control targetForm = CalculationProgressChanged.Target
                            as System.Windows.Forms.Control;
                    targetForm.BeginInvoke(CalculationProgressChanged,
                            new object[] { this, args });
                }
                else
                {
                    CalculationProgressChanged(this, args);
                }
            }
        }
        #endregion
    }
    public enum CalculationStatus
    {
        // Note: Currently toggles between Calculating and NotCalculating
        NotCalculating,
        Calculating,
        Finished,
        PausePending,
        Paused,
        ResumePending,
        CancelPending,
        Pending
    }
    public class CalculationEventArgs : EventArgs
    {
        #region Fields
        public string ResultString;
        public StateIs StdResultLong;

        public int Progress;
        public int SelectedProgress;
        public int FileProgress;
        public int DirectoryProgress;

        public object sender;
        public CalculationStatus Status;
        public bool SetButtons;
        #endregion
        #region Calculation Events - Constructor Options
        public CalculationEventArgs(StateIs PassedResult)
        {
            StdResultLong = PassedResult;
        }
        public CalculationEventArgs(string PassedResult)
        {
            ResultString = PassedResult;
        }
        public CalculationEventArgs(int PassedProgress)
        {
            Progress = PassedProgress;
            Status = CalculationStatus.Calculating;
        }
        public CalculationEventArgs(int PassedDirectoryProgress, int PassedFileProgress)
        {
            DirectoryProgress = PassedDirectoryProgress;
            FileProgress = PassedFileProgress;
            Status = CalculationStatus.Calculating;
        }
        public CalculationEventArgs(int PassedDirectoryProgress, int PassedFileProgress, int PassedSelectedProgress)
        {
            DirectoryProgress = PassedDirectoryProgress;
            FileProgress = PassedFileProgress;
            SelectedProgress = PassedSelectedProgress;
            Status = CalculationStatus.Calculating;
        }
        public CalculationEventArgs(object PassedSender, CalculationStatus PassedStatus, bool PassedSetButtons)
        {
            sender = PassedSender;
            Status = PassedStatus;
            SetButtons = PassedSetButtons;
        }
        public CalculationEventArgs()
        {
            Status = CalculationStatus.Calculating;
            SetButtons = false;
        }
        #endregion
    }
    public interface IDbTaskDataId
    {
        int Id { get; set; }
    }

}
