using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;


using Mdm.Oss.FileUtil;
using Mdm.Oss.Threading;
using Mdm.Oss.WinUtil;

namespace Mdm.Oss.FileUtil {

//        public class FileAgent : IDisposable {
        public class FileAgent : Component {
            #region Thread Management Declarations for File Agent
            // States
            protected internal IoStateDef[] IoStates;
            // Thread Index Thread Data
            public const int ThreadMax = 20;
            [ThreadStatic]
            static String ThreadNameTls;
            [ThreadStatic]
            static Int32 ThreadIndexTls;
            // Thread Index Pointers
            protected internal static int ThreadIndex;
            protected internal static int ThreadIndexCurr;
            // Thread Domain
            string FileUser = "System";
            string FileUserName = Environment.UserName;
            string FileDomainName = Environment.UserDomainName;
            // File Stream
            public FileStream IoFileStream; // stream s/b part of Mfile...
            // Async Manager Operations
            protected internal static AsyncOperation[] IoOperations;
            // Threads must decrement IoActiveCount, and protect
            // their access to it through a mutex.
            protected internal static int IoActiveCount = 0;
            // Mutex array of ThreadMax threads.
            protected internal static Object IoMutex = new Object();
            protected internal static Object[] IoMutexArr = new Object[ThreadMax];
            // WaitObject array of ThreadMax threads is signalled when all image processing is done.
            protected internal static Object IoWait = new Object();
            protected internal static Object[] IoWaitArr = new Object[ThreadMax];
            // ResetEvent Sychronization array for Auto and Manual Events using signalling
            protected internal AutoResetEvent[] IoAutoResetEvents;
            protected internal ManualResetEvent IoManualResetEvent;
            #endregion
            // Constructors
            public FileAgent() {
                // Create a string representing the current user.
                FileUser = FileDomainName + "\\" + FileUserName;
                //
                IoAutoResetEvents = new AutoResetEvent[ThreadMax];
                IoOperations = new AsyncOperation[ThreadMax];
                for (int i = 0; i < ThreadMax; i++) {
                    IoAutoResetEvents[i] = new AutoResetEvent(false);
                    IoOperations[i] = null;
                }
                IoManualResetEvent = new ManualResetEvent(false);
                IoStates = new IoStateDef[ThreadMax];
                //
                // Create a string representing the current user.
                FileUser = Environment.UserDomainName + "\\" +
                    Environment.UserName;


            }
            #region Destructors
            ~FileAgent() {
                // Do not re-create Dispose clean-up code here.
                // Calling Dispose(false) is optimal in terms of
                // readability and maintainability.
                Dispose(false);
            }
            public void Dispose() {
                Dispose(true);
            }
            // Track whether Dispose has been called.
            private bool disposed = false;
            public void Dispose(bool DisposingPassed) {
                if (!this.disposed) {
                    if (DisposingPassed) {
                        IoWaitForIdel();
                        for (int i = 0; i < ThreadMax; i++) {
//                            IoAutoResetEvents[i].Dispose(true);
                        }
                        
                    }
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                // Win32GeneralDef.CloseHandle(handle);
                // handle = IntPtr.Zero;

                // base.Dispose(DisposingPassed);

                // indicate disposing has been done.
                disposed = true;
            }
            #endregion
            //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            #region Thread Management
            // Function Type Sections (Read Me):
            //Thread Management
            //CreateThreadpoolIo 
            //CreateThreadpoolTimer 
            //CreateThreadpoolWait 
            //CreateThreadpoolWork 
            //TrySubmitThreadpoolCallback 
            //////////////////////////////////////////////////////////////////
            #region Thread Pool Do Read Stream
            public void ThreadPoolDoReadStream(IAsyncResult asyncResult) {
                AsyncCallback ThreadPoolEventCallback = new
                    AsyncCallback(ThreadPoolDoReadStreamCallbackImpl);
                //
                IoStateDef IoState = new IoStateDef();

                // Very large items are read only once, so you can make the 
                // buffer on the FileStream very small to save memory.
                IoFileStream = new FileStream("dummy.tmp",
                    FileMode.Open, FileAccess.Read, FileShare.Read, 1, true);
                IoFileStream.BeginRead(
                    IoState.Item, 
                    0, 
                    Marshal.SizeOf(IoState.Item), 
                    ThreadPoolEventCallback,
                    IoState);
            }
            //
            public void ThreadPoolDoReadStreamCallbackImpl(IAsyncResult asyncResult) {
                IoStateDef IoState = (IoStateDef)asyncResult.AsyncState;
                Int32 IoIndex = IoState.IoThreadIndex;
                Stream stream = IoFileStream;
                int bytesRead = IoFileStream.EndRead(asyncResult);
                //
                // Record that an ThreadPoolEvent is finished now.
                lock (IoMutex) {
                    IoActiveCount--;
                    if (IoActiveCount == 0) {
                        Monitor.Enter(IoWait);
                        Monitor.Pulse(IoWait);
                        Monitor.Exit(IoWait);
                    }
                }
            }
            #endregion
            //
            #region Thread Pool Do Timer
            System.Timers.Timer IoTimer;
            public void IoTimerSet() {
                // Create a timer with a ten second interval.
                IoTimer = new System.Timers.Timer(10000);

                // Hook up the Elapsed event for the timer.
                IoTimer.Elapsed += new ElapsedEventHandler(IoTimerCallback);

                // Set the Interval to 2 seconds (2000 milliseconds).
                IoTimer.Interval = 2000;
                IoTimer.Enabled = true;
                IoTimer.AutoReset = true;
            }
            //
            public void IoTimerCallback(object source, ElapsedEventArgs e) {

            }
            #endregion
            //
            #region Thread Pool Do Wait
            public void IoWaitForData() {
                // Wait for the first instance of the file to be found.
                int index = WaitHandle.WaitAny(IoAutoResetEvents, 3000, false);
                if (index == WaitHandle.WaitTimeout) {
                    Console.WriteLine("\n{0} Wait Timeout exceeded.", 3000);
                } else {
                    Console.WriteLine("\n{0} thread finished ({1}).", IoStates[index].IoThreadIndex);
                }
            }
            //
            public void IoWaitForIdel() {
                // Determine whether all ThreadPoolEvents are done being processed.  
                // If not, block until all are finished.
                bool mustBlock = false;
                lock (IoMutex) {
                    if (IoActiveCount > 0)
                        mustBlock = true;
                }
                if (mustBlock) {
                    Console.WriteLine("All worker threads are queued. " +
                        " Blocking until they complete. numLeft: {0}",
                        IoActiveCount);
                    // ThreadInstance.Join(waitTime);
                    Monitor.Enter(IoWait);
                    Monitor.Wait(IoWait);
                    Monitor.Exit(IoWait);
                }
                long t0 = Environment.TickCount;
                long t1 = Environment.TickCount;
                Console.WriteLine("Total time processing images: {0}ms",
                    (t1 - t0));

            }
            //////////////////////////////////////////////////////////////////
            #region IoWait Documentation & Test (Read Me)
            // WaitOne(new TimeSpan(0, 0, 1), false)
            // WaitAll(array<WaitHandle>[]()[], TimeSpan, Boolean)
            // WaitAny(array<WaitHandle>[]()[], TimeSpan, Boolean)
            //millisecondsTimeout
            // Type: System..::.Int32
            // The number of milliseconds to wait, or Timeout..::.Infinite (-1) to wait indefinitely. 
            //timeout
            // Type: System..::.TimeSpan
            // A TimeSpan that represents the number of milliseconds to wait, 
            // or a TimeSpan that represents -1 milliseconds to wait indefinitely. 
            //exitContext
            // Type: System..::.Boolean
            // true to exit the synchronization domain for the context before the wait 
            // (if in a synchronized context), and reacquire it afterward; otherwise, false. 

            // TimeSpan(Int32, Int32, Int32, Int32, Int32) 
            // Initializes a new TimeSpan to a specified number of 
            // days, hours, minutes, seconds, and milliseconds. 
            // TimeSpan(Int64) 
            // Initializes a new TimeSpan to the specified number of ticks. 
            #endregion
            #endregion
            //
            #region Thread Pool Do
            private static void ThreadPoolDo() {
                // Queue the task.
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolDoImpl));
            }
            // This thread procedure performs the task.
            static void ThreadPoolDoImpl(Object stateInfo) {
                // No state object was passed to QueueUserWorkItem, so 
                // stateInfo is null.
                Console.WriteLine("Hello from the thread pool.");
            }
            #endregion
            //
            #region Thread Pool Do Mutex
            // This method represents a resource that must be synchronized
            // so that only one thread at a time can enter.
            private static Mutex ThreadPoolMutex = new Mutex();
            private static void ThreadPoolDoExclusive() {
                // Wait until it is safe to enter.
                ThreadPoolMutex.WaitOne();

                Console.WriteLine("{0} has entered the protected area",
                    Thread.CurrentThread.Name);

                // Place code to access non-reentrant resources here.

                // Simulate some work.
                Thread.Sleep(500);

                Console.WriteLine("{0} is leaving the protected area\r\n",
                    Thread.CurrentThread.Name);

                // Release the Mutex.
                ThreadPoolMutex.ReleaseMutex();
            }
            #endregion
            //////////////////////////////////////////////////////////////////
            #region Thread
            #region Thread Do
            #region Thread Documentation (Read Me)
            //ApartmentState Obsolete. Gets or sets the apartment state of this thread. 
            //CurrentContext Gets the current context in which the thread is executing. 
            //CurrentCulture Gets or sets the culture for the current thread. 
            //CurrentPrincipal Gets or sets the thread's current principal (for role-based security). 
            //CurrentThread Gets the currently running thread. 
            //CurrentUICulture Gets or sets the current culture used by the Resource Manager to look up culture-specific resources at run time. 
            //ExecutionContext Gets an ExecutionContext object that contains information about the various contexts of the current thread.  
            //IsAlive Gets a value indicating the execution status of the current thread. 
            //IsBackground Gets or sets a value indicating whether or not a thread is a background thread. 
            //IsThreadPoolThread Gets a value indicating whether or not a thread belongs to the managed thread pool. 
            //ManagedThreadId Gets a unique identifier for the current managed thread. 
            //Name Gets or sets the name of the thread. 
            //Priority Gets or sets a value indicating the scheduling priority of a thread. 
            //////////////////////////////////////////////////////////////////
            //ThreadState Gets a value containing the states of the current thread. 
            //Member name Description 
            // Running The thread has been started, it is not blocked, and there is no pending ThreadAbortException. 
            // StopRequested The thread is being requested to stop. This is for internal use only. 
            // SuspendRequested The thread is being requested to suspend. 
            // Background The thread is being executed as a background thread, as opposed to a foreground thread. This state is controlled by setting the Thread..::.IsBackground property. 
            // Unstarted The Thread..::.Start method has not been invoked on the thread. 
            // Stopped The thread has stopped. 
            // WaitSleepJoin The thread is blocked. This could be the result of calling Thread..::.Sleep or Thread..::.Join, of requesting a lock — for example, by calling Monitor..::.Enter or Monitor..::.Wait — or of waiting on a thread synchronization object such as ManualResetEvent.  
            // Suspended The thread has been suspended. 
            // AbortRequested The Thread..::.Abort method has been invoked on the thread, but the thread has not yet received the pending System.Threading..::.ThreadAbortException that will attempt to terminate it. 
            // Aborted 
            #endregion
            private static void ThreadDo() {
                Thread myThread = new Thread(new ThreadStart(ThreadDoImpl));
                int i = 0;
                myThread.Name = String.Format("Thread{0}", i + 1);
                myThread.IsBackground = true;
                myThread.Start();

            }
            //
            static void ThreadDoImpl() {
                // This thread procedure performs the task.
                // No state object was passed to QueueUserWorkItem, so 
                // stateInfo is null.
                Console.WriteLine("Hello from the thread pool.");
            }
            #endregion
            //
            #region Thread Sleep, Interupt Documentation & Test (Read Me)
            //Calling Thread..::.Sleep with Timeout..::.Infinite 
            //causes a thread to sleep until it is 
            //interrupted by another thread that calls 
            //Thread..::.Interrupt, or until it is 
            //terminated by Thread..::.Abort.

        //Thread.Sleep(Timeout.Infinite);
        //catch(ThreadInterruptedException ProgressChangedEventData)
        //{
        //    Console.WriteLine("newThread cannot go to sleep - " +
        //        "interrupted by main thread.");
        //}
            #endregion
            #endregion
            //////////////////////////////////////////////////////////////////
            #region Semaphore Security
            public void SecurityInitialize(string user) {
                // Create a security object that grants no access.
                SemaphoreSecurity mSec = new SemaphoreSecurity();

                // Add a rule that grants the current user the 
                // right to enter or release the semaphore.
                SemaphoreAccessRule rule = new SemaphoreAccessRule(user,
                    SemaphoreRights.Synchronize | SemaphoreRights.Modify,
                    AccessControlType.Allow);
                mSec.AddAccessRule(rule);

                // Add a rule that denies the current user the 
                // right to change permissions on the semaphore.
                rule = new SemaphoreAccessRule(user,
                    SemaphoreRights.ChangePermissions,
                    AccessControlType.Deny);
                mSec.AddAccessRule(rule);

                // Display the rules in the security object.
                SecurityShow(mSec);

                // Add a rule that allows the current user the 
                // right to read permissions on the semaphore. This rule
                // is merged with the existing Allow rule.
                rule = new SemaphoreAccessRule(user,
                    SemaphoreRights.ReadPermissions,
                    AccessControlType.Allow);
                mSec.AddAccessRule(rule);
            }

            private static void SecurityShow(SemaphoreSecurity security) {
                Console.WriteLine("\r\nCurrent access rules:\r\n");

                foreach (SemaphoreAccessRule ar in
                    security.GetAccessRules(true, true, typeof(NTAccount))) {
                    Console.Write("        User: {0}", ar.IdentityReference);
                    Console.Write("        Type: {0}", ar.AccessControlType);
                    Console.WriteLine("      Rights: {0}", ar.SemaphoreRights);
                    Console.WriteLine();
                }
            }
            #endregion
            #region ThreadPriority Enumeration, Start Constructors, Status Documentation (Read Me)
                //Lowest The Thread can be scheduled after threads with any other priority. 
                //BelowNormal The Thread can be scheduled after threads with Normal priority and before those with Lowest priority. 
                //Normal The Thread can be scheduled after threads with AboveNormal priority and before those with BelowNormal priority. Threads have Normal priority by default. 
                //AboveNormal The Thread can be scheduled after threads with Highest priority and before those with Normal priority. 
                //Highest The Thread can be scheduled before threads with any other priority. 

            //public delegate void ParameterizedThreadStart(
            //    Object obj
            //)
            //[ComVisibleAttribute(true)]
            //public delegate void ThreadStart()

         //Member name Description 
         //Running The thread has been started, it is not blocked, and there is no pending ThreadAbortException. 
         //StopRequested The thread is being requested to stop. This is for internal use only. 
         //SuspendRequested The thread is being requested to suspend. 
         //Background The thread is being executed as a background thread, as opposed to a foreground thread. This state is controlled by setting the Thread..::.IsBackground property. 
         //Unstarted The Thread..::.Start method has not been invoked on the thread. 
         //Stopped The thread has stopped. 
         //WaitSleepJoin The thread is blocked. This could be the result of calling Thread..::.Sleep or Thread..::.Join, of requesting a lock — for example, by calling Monitor..::.Enter or Monitor..::.Wait — or of waiting on a thread synchronization object such as ManualResetEvent.  
         //Suspended The thread has been suspended. 
         //AbortRequested The Thread..::.Abort method has been invoked on the thread, but the thread has not yet received the pending System.Threading..::.ThreadAbortException that will attempt to terminate it. 
         //Aborted The thread state includes AbortRequested and the thread is now dead, but its state has not yet changed to Stopped. 

            #endregion
            #endregion
            // Target Thread code
            #region Target Thread Management
            //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            #region Delegates
            // Global public delegates
            public delegate void ProgressChangedEventHandler(ProgressChangedEventArgs e);
            public delegate void TargetCompletedEventHandler(object sender, TargetCompletedEventArgs e);

            /////////////////////////////////////////////////////////////
            // Internal class delegates
            private delegate void TargetEventHandler(int Data1, AsyncOperation IoOperation);
            //
            private SendOrPostCallback OnTargetProgressChangedDelegate;
            private SendOrPostCallback OnTargetCompletedDelegate;

            /////////////////////////////////////////////////////////////
            #region Public events

            public event ProgressChangedEventHandler TargetProgressChangedEvent;
            public event TargetCompletedEventHandler TargetCompletedEvent;

            #endregion

            protected virtual void InitializeDelegates() {
                OnTargetProgressChangedDelegate = new SendOrPostCallback(TargetProgressChangedAsync);
                OnTargetCompletedDelegate = new SendOrPostCallback(TargetCompletedAsync);
            }
            #endregion
            //
            #region Target Thread Do
            // This method starts an asynchronous calculation. 
            // First, it checks the supplied task ID for uniqueness.
            // If ThreadIPassed is unique, it creates a new TargetEventHandler 
            // and calls its BeginInvoke method to start the calculation.
            public virtual void TargetDoInvoke(
                int Data1,
                object ThreadIPassed) {
                // Create an AsyncOperation for ThreadIPassed.
                ThreadIndexTls = (int)ThreadIPassed;
                AsyncOperation IoOperation =
                    AsyncOperationManager.CreateOperation(ThreadIPassed);
                // Multiple threads will access the task dictionary,
                // so it must be locked to serialize access.
                lock (IoStates.SyncRoot) {
                    // if (IoStates.Contains(ThreadIPassed)) {
                    if (IoStates[ThreadIndexTls].IoOperation != null) {
                        throw new ArgumentException(
                            "Task ID parameter must be unique",
                            "ThreadIPassed");
                    }
                    IoStates[ThreadIndexTls].ItemStatus.Started = true;
                    IoStates[ThreadIndexTls].IoThreadIndex = ThreadIndexTls;
                    IoStates[ThreadIndexTls].IoOperation = IoOperation;
                }

                // Start the asynchronous operation.
                TargetEventHandler Target = new TargetEventHandler(TargetDoInvokeImpl);
                Target.BeginInvoke(
                    Data1,
                    IoOperation,
                    null,
                    null);
            }

            private void TargetDoInvokeImpl(
                int Data4,
                AsyncOperation IoOperation
                ) {
                // Create an AsyncOperation for ThreadIPassed.
                ThreadIndexTls = (int)IoOperation.UserSuppliedState;
                Exception Exception1 = null;
                int i = 0;
                if (!TargetIsCanceled(IoOperation.UserSuppliedState)) {
                    lock (IoStates.SyncRoot) {
                        IoStates[ThreadIndexTls].ItemStatus.Started = true;
                        IoStates[ThreadIndexTls].ItemStatus.Completed = false;
                    }
                    try {
                        i = Data4 + 1;
                        // Do work
                        TargetDoInvokeImplMainLoop(Data4, IoOperation);
                    } catch (Exception ex) {
                        Exception1 = ex;
                    }

                }
                // Package the results of the operation in a 
                // CalculatePrimeCompletedEventArgs.
                int Data1 = 1;
                int Data2 = 1;
                int Data3 = 1;
                TargetCompletedImpl(
                    Data1,
                    Data2,
                    Data3,
                    Exception1,
                    TargetIsCanceled(IoOperation.UserSuppliedState),
                    IoStates[ThreadIndexTls]);

                // Package the results of the operation in a 
                // CalculatePrimeCompletedEventArgs.
                TargetCompletedEventArgs TargetCompletedEvent =
                    new TargetCompletedEventArgs(
                    0,
                    0,
                    0,
                    Exception1,
                    TargetIsCanceled(IoStates[ThreadIndexTls]),
                    IoStates[ThreadIndexTls].IoOperation);

                // End the task. The asyncOp object is responsible 
                // for marshaling the call.
                IoStates[ThreadIndexTls].IoOperation.PostOperationCompleted(OnTargetCompletedDelegate, TargetCompletedEvent);

                // TargetDoInvokeImpl Note that after the call to OperationCompleted, 
                // asyncOp is no longer usable, and any attempt to use it
                // will cause an exception to be thrown.
            }

            // Do Work
            private void TargetDoInvokeImplMainLoop(
                int Data1,
                AsyncOperation IoOperation
                ) {
                // Create an AsyncOperation for ThreadIPassed.
                ThreadIndexTls = (int)IoOperation.UserSuppliedState;
                Exception Exception2 = null;
                ProgressChangedEventArgs TargetProgressEventData = null;
                int DataCount = 1;
                int DataMax = 100;
                while (DataCount < DataMax &&
                   !TargetIsCanceled(IoOperation.UserSuppliedState)) {
                    DataCount += 1;
                    //
                    // Do Work Here
                    //
                    // Report to the client that a prime was found.
                    TargetProgressEventData = new TargetProgressEventArgs(
                        DataCount,
                        (int)((float)DataCount / (float)DataMax * 100),
                        IoOperation.UserSuppliedState);
                    //
                    IoOperation.Post(this.OnTargetProgressChangedDelegate, TargetProgressEventData);
                }
            }
            #endregion
            //
            #region Target Event Data Objects / Args
            public class TargetCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
                public int i = 0;
                public Int32 Data1;
                public Int32 Data2;
                public Int32 Data3;

                public TargetCompletedEventArgs(
                    Int32 Data1,
                    Int32 Data2,
                    Int32 Data3,
                    Exception Exception1,
                    bool IsCancelled,
                    AsyncOperation IoOperation)
                    : base(Exception1, IsCancelled, IoOperation) {
                    i = 2;
                }
            }
            //
            public class TargetProgressEventArgs : ProgressChangedEventArgs {
                private int Data1 = 1;

                public TargetProgressEventArgs(
                    int Data2,
                    int ProgressPercentage,
                    object UserToken
                    )
                    : base(ProgressPercentage, UserToken
                    ) {
                    this.Data1 = Data2;
                }

                public int Data1Get {
                    get {
                        return Data1;
                    }
                }
            }
            #endregion
            //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            #region Target Changed
            private void TargetProgressChangedAsync(object state) {
                ProgressChangedEventArgs ProgressChangedEventData =
                    state as ProgressChangedEventArgs;

                OnTargetProgressChanged(ProgressChangedEventData);
            }
            //
            protected void OnTargetProgressChanged(ProgressChangedEventArgs ProgressChangedEventData) {
                if (TargetProgressChangedEvent != null) {
                    TargetProgressChangedEvent(ProgressChangedEventData);
                }
            }
            #endregion
            //
            #region Target Completed
            // This method is invoked via the AsyncOperation object,
            // so it is guaranteed to be executed on the correct thread.
            private void TargetCompletedAsync(object operationState) {
                TargetCompletedEventArgs TargetCompletedEventData =
                    operationState as TargetCompletedEventArgs;

                OnTargetCompleted(TargetCompletedEventData);
            }

            // Method that the underlying, free-threaded asynchronous behavior will invoke.
            // This will happen on an arbitrary thread.
            private void TargetCompletedImpl(
                int Data1,
                int Data2,
                int Data3,
                Exception Exception1,
                bool IsCancelled,
                IoStateDef IoState) {
                // AsyncOperation IoOperation) {
                // If the task was not previously canceled,
                // remove the task from the lifetime collection.
                if (!IsCancelled) {
                    lock (IoStates.SyncRoot) {
                        // IoStates.Remove(IoOperation.UserSuppliedState);
                        IoStates[IoState.IoThreadIndex].ItemStatus.Completed = true;
                        IoStates[IoState.IoThreadIndex].IoOperation = null;
                    }
                }
                // Package the results of the operation in a 
                // CalculatePrimeCompletedEventArgs.
                TargetCompletedEventArgs TargetCompletedEvent =
                    new TargetCompletedEventArgs(
                    Data1,
                    Data2,
                    Data3,
                    Exception1,
                    IsCancelled,
                    IoState.IoOperation);

                // End the task. The asyncOp object is responsible 
                // for marshaling the call.
                IoState.IoOperation.PostOperationCompleted(OnTargetCompletedDelegate, TargetCompletedEvent);

                // TargetDoInvokeImpl Note that after the call to OperationCompleted, 
                // asyncOp is no longer usable, and any attempt to use it
                // will cause an exception to be thrown.
                if (!IsCancelled) {
                    lock (IoStates.SyncRoot) {
                        // IoStates.Remove(IoOperation.UserSuppliedState);
                        IoStates[IoState.IoThreadIndex].IoOperation = null;
                    }
                }
            }
            //
            protected void OnTargetCompleted(TargetCompletedEventArgs TargetCompletedEventData) {
                if (TargetCompletedEvent != null) {
                    TargetCompletedEvent(this, TargetCompletedEventData);
                }
            }
            #endregion
            //
            #region Target Cancelled
            // Utility method for determining if a task has been canceled.
            private bool TargetIsCanceled(object ThreadIPassed) {
                ThreadIndexTls = (int)ThreadIPassed;
                return (IoStates[ThreadIndexTls].ItemStatus.Cancelled);
            }
            // This method cancels a pending asynchronous operation.
            public void TargetCancelAsync(object ThreadIPassed) {
                ThreadIndexTls = (int)ThreadIPassed;
                // AsyncOperation IoOperation = (Object)IoStates[ThreadIndexTls].IoThreadIndex as AsyncOperation;
                AsyncOperation IoOperation = IoStates[ThreadIndexTls].IoOperation as AsyncOperation;
                if (IoStates[ThreadIndexTls].IoOperation != null) {
                    if (!IoStates[ThreadIndexTls].ItemStatus.Cancelled) {
                        lock (IoStates.SyncRoot) {
                            //userStateToLifetime.Remove(ThreadIPassed);
                            IoStates[ThreadIndexTls].ItemStatus.Cancelled = true;
                            IoStates[ThreadIndexTls].ThreadStatus.Aborted = true;
                            IoStates[ThreadIndexTls].IoOperation = null;
                        }
                    }
                }
            }
            #endregion
            //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            #region Target Existence
            private bool TargetDoesExist(object ThreadIPassed) { return (IoStates[(int)ThreadIPassed].IoOperation != null); }
            //
            private bool TargetDoesNotExist(object ThreadIPassed) { return (IoStates[(int)ThreadIPassed].IoOperation == null); }
            #endregion
            #endregion
        }
}
