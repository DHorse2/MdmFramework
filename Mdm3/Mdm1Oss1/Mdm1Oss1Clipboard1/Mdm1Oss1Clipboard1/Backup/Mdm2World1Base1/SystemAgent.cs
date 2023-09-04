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

using Mdm.Oss.Decl;
using Mdm.Oss.Sys;
using Mdm.Oss.Threading;
using Mdm.World;

namespace Mdm {
    namespace Oss {
        namespace Sys {

            public class SystemAgent {
                public SystemAgent() {
                    // 
                }
            }
            // <summary>
            // </summary>
            // 
            // <summary>
            // Contains the core information about a System
            // </summary>
            public class SystemInfoClass {
                public SystemInfoClass() {
                }
                public SystemInfoClass(Object Sender)
                    : this() {
                }
                public SystemInfoClass Get() {
                    return this;
                }
                // <summary>
                // Set the system info fields from a passed SystemInfo Object
                // </summary>
                public void Set(SystemInfoClass SystemInfoPassed) {
                    SystemName_ = SystemInfoPassed.SystemName_;
                }
                private String SystemName_;
                public String SystemName {
                    get { return SystemName_; }
                    set { SystemName_ = value; }
                }
            }

            #region System Core Object and Interop
            public class Sobject {
                // <Area Id = "MdmClassLevelSenders">
                public Object Sender;
                public Object SenderIsThis;
                public String Name;
                public Type ItemType;
                public Type ItemBaseType;
                public Int32 ItemNumber;
                public String Namespace;
                public String InteropName;
                public Object Item;
                public Object OwnerItem;
                public Mdm.World.ItemStatusDef ItemStatus;
                //
                public Dictionary<String, Object> Items = new Dictionary<String, Object>();
                public Dictionary<String, Object> Parents = new Dictionary<String, Object>();
            }
            #endregion

        }

        namespace Threading {
            
            #region System Synchronization
            public delegate void RequestorCallbackDef(IoStateDef IoState);

            public class IoStateDef : DefStdBaseRun {
                #region Declarations
                public String CategoryKey;
                public String ActionKey;
                public String Key;
                public String PostActionKey;
                public String ResultStateKey;
                public Object Requestor;
                // Application Objects
                public IoStateDef IoStateObject;
                public Object CurrentApp;
                public Object CurrentClass;
                public Object IoFileStream; // stream s/b part of Mfile...
                public Object IoMfile;
                //
                public byte[] Item;
                // Thread
                public ItemStatusDef ItemStatus;
                public ThreadStatusDef ThreadStatus;
                public Int32 IoThreadIndex;
                public Int32 IoThread;
                // Execute
                public long DoExecuteResult;
                public bool ExecuteError;
                protected internal bool bpDoExecute;
                public bool DoExecute {
                    get { return bpDoExecute; }
                    set {
                        bpDoExecute = value;
                        //if (bpDoSyncExecute
                        //    | bpDoRequestorExecute
                        //    | bpDoSendOrPostExecute
                        //    ) { bpDoExecute = true; } else { bpDoExecute = false; }
                    }
                }
                // Callback
                public long DoCallbackResult;
                public bool CallbackError;
                protected internal bool bpDoCallback;
                // Note: Clearing DoCallback clears all callback flags.
                // Note: Setting any callback flag to true turns on DoCallback.
                public bool DoCallback {
                    get { return bpDoCallback; }
                    set {
                        bpDoCallback = value;
                        if (!bpDoCallback) {
                            bpDoSyncCallback = false;
                            bpDoRequestorCallback = false;
                            bpDoSendOrPostCallback = false;
                        }
                    }
                }
                // Internal Callback
                protected internal bool bpDoSyncCallback;
                public bool DoSyncCallback {
                    get { return bpDoSyncCallback; }
                    set {
                        bpDoSyncCallback = value;
                        if (bpDoSyncCallback) { DoCallback = true; }
                    }
                }
                public AsyncCallback IoAsyncCallback;
                public Delegate IoSyncCallback; // not relevant?
                //
                // External Callback
                protected internal bool bpDoRequestorCallback;
                public bool DoRequestorCallback {
                    get { return bpDoRequestorCallback; }
                    set {
                        bpDoRequestorCallback = value;
                        if (bpDoRequestorCallback) { DoCallback = true; }
                    }
                }
                public RequestorCallbackDef RequestorCallback;
                //
                // SendOrPostCallback Lookup List
                protected internal bool bpDoSendOrPostCallback;
                public bool DoSendOrPostCallback {
                    get { return bpDoSendOrPostCallback; }
                    set {
                        bpDoSendOrPostCallback = value;
                        if (bpDoSendOrPostCallback) { DoCallback = true; }
                    }
                }
                public long MethodResult;
                public Object ResultObject;
                public Dictionary<String, SendOrPostCallback> SourceCommunications = new Dictionary<String, SendOrPostCallback>();
                //
                // Sycnonization Objects
                public AsyncOperation IoOperation;
                public static Object IoWait = new Object();
                // Sychronization using ResetEvent signalling
                public bool UseIoAutoResetEvent = true;
                public AutoResetEvent IoAutoResetEvent;
                public bool UseIoManualResetEvent = false;
                public ManualResetEvent IoManualResetEvent;
                #endregion

                #region Constructors / Destructors
                public IoStateDef() : base() { Sender = SenderIsThis = IoStateObject = this; }
                public IoStateDef (
                    Object RequestingObjectPassed,
                    RequestorCallbackDef RequestorCallbackPassed
                    ) : this() {
                    Requestor = RequestingObjectPassed;
                    RequestorCallback = RequestorCallbackPassed;
                }
                #endregion
                //
                public void ClearKey() {
                    Key = "";
                    CategoryKey = "";
                    ActionKey = "";
                    PostActionKey = "";
                    ResultStateKey = "";
                }
                public void ReadyToDo() { DoCallback = false; }
                #region Delegate Lists
                public void SourceCommunicationsAdd(String SendOrPostKeyPassed, SendOrPostCallback SendOrPostCallbackPassed) {
                    if (!SourceCommunications.ContainsKey(SendOrPostKeyPassed)) {
                        SourceCommunications.Add(SendOrPostKeyPassed, SendOrPostCallbackPassed);
                    } else {
                        SourceCommunications[SendOrPostKeyPassed] = SendOrPostCallbackPassed;
                    }
                }
                public void SourceCommunicationsDelete(String SendOrPostKeyPassed) {
                    if (SourceCommunications.ContainsKey(SendOrPostKeyPassed)) {
                        SourceCommunications.Remove(SendOrPostKeyPassed);
                    }
                }
                #endregion
                #region Execution
                public void IoThreadExecute() {
                    // Do Work
                    if (RequestorCallback != null)
                        RequestorCallback(this);
                }
                #endregion
                #region Callbacks

                public long IoDoCallback(long MethodResultPassed, object ObjectResultPassed) {
                    DoCallbackResult = (long)StateIs.Started;

                    // if (IoOperation != null) {
                    if (DoRequestorCallback) {
                        ReadyToDo();
                        if (RequestorCallback != null) {
                            RequestorCallback(IoStateObject);
                        }
                    }
                    if (DoSyncCallback) {
                        ReadyToDo();
                        if (IoOperation != null) {
                            if (IoAsyncCallback != null) {
                                IoAsyncCallback((IAsyncResult)IoStateObject);
                            }
                        }
                    }
                    if (DoSendOrPostCallback) {
                        if (CategoryKey.Length == 0) { CategoryKey = "General"; }
                        if (ActionKey.Length == 0) { ActionKey = "DefaultAction"; }
                        if (PostActionKey.Length == 0) { PostActionKey = "Callback"; }
                        if (StateIsSuccessfulAll(MethodResultPassed)) {
                            CallbackError = false;
                            ResultStateKey = "";
                        } else {
                            CallbackError = true;
                            ResultStateKey = "Error";
                        }
                        Key = CategoryKey + ActionKey + PostActionKey + ResultStateKey;
                        ReadyToDo();
                        if (SourceCommunications.ContainsKey(Key)) {
                            SendOrPostCallback DoCallback;
                            SourceCommunications.TryGetValue(Key, out DoCallback);
                            if (DoCallback != null) {
                                if (IoOperation != null) {
                                    if (CallbackError || ObjectResultPassed == null) {
                                        IoOperation.Post(DoCallback, MethodResultPassed);
                                    } else if (ObjectResultPassed != null) {
                                        IoOperation.Post(DoCallback, ObjectResultPassed);
                                    } else if (ResultObject != null) {
                                        IoOperation.Post(DoCallback, ResultObject);
                                    } else {
                                        IoOperation.Post(DoCallback, null);
                                    }
                                    DoCallbackResult = (long)StateIs.UnknownFailure;
                                } else {
                                    if (CallbackError || ObjectResultPassed == null) {
                                        DoCallback(MethodResultPassed);
                                    } else if (ObjectResultPassed != null) {
                                        DoCallback(ObjectResultPassed);
                                    } else if (ResultObject != null) {
                                        DoCallback(ResultObject);
                                    } else {
                                        DoCallback((Object)null);
                                    }
                                    DoCallbackResult = (long)StateIs.Finished;
                                }
                            } else { DoCallbackResult = (long)StateIs.MissingName; }
                        } else { DoCallbackResult = (long)StateIs.MissingName; }
                    }
                    // }
                    return DoCallbackResult;
                }
                #endregion
            }
            #endregion

            public class ThreadStatusDef {
                public bool Running;
                public bool StopRequested;
                public bool SuspendRequested;
                public bool Background;
                public bool Unstarted;
                public bool Stopped;
                public bool WaitSleepJoin;
                public bool Suspended;
                public bool AbortRequested;
                public bool Aborted;

                public Object RunningSource;
                public Object StopRequestedSource;
                public Object SuspendRequestedSource;
                public Object BackgroundSource;
                public Object UnstartedSource;
                public Object StoppedSource;
                public Object WaitSleepJoinSource;
                public Object SuspendedSource;
                public Object AbortRequestedSource;
                public Object AbortedSource;

                public ThreadStatusDef() {
                    Running = false;
                    StopRequested = false;
                    SuspendRequested = false;
                    Background = false;
                    Unstarted = false;
                    Stopped = false;
                    WaitSleepJoin = false;
                    Suspended = false;
                    AbortRequested = false;
                    Aborted = false;

                    RunningSource = null;
                    StopRequestedSource = null;
                    SuspendRequestedSource = null;
                    BackgroundSource = null;
                    UnstartedSource = null;
                    StoppedSource = null;
                    WaitSleepJoinSource = null;
                    SuspendedSource = null;
                    AbortRequestedSource = null;
                    AbortedSource = null;
                }
            }

        }
    }
}