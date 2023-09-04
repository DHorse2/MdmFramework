using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Remoting.Messaging;
using System.ComponentModel;

// using System.Data;
// using System.Windows.Forms;
// using Mdm.Oss.ClipboardUtil;
using Mdm.Oss.CodeUtil;
using Mdm.Oss.Support;
using Mdm.Oss.Mobj;
using Mdm.Oss.Mapp;
using Mdm.Oss.FileUtil;
// using    Mdm1Oss1FileCreation1;
// using    Mdm1Srt1MinputTld1;


namespace Mdm.Srt.InputTld {
    public class MinputTldThread : MinputTld
	{
		#region Worker Instantiation
		private System.ComponentModel.BackgroundWorker omWt = new System.ComponentModel.BackgroundWorker();
		#endregion
        string sAnyExceptionMessage = "";
		#region Constructor
		/// Create new instance of MinputTldThread and do some setup
        public MinputTldThread(Mobject omPmssedO) : base(omPmssedO) {
            InputTldThread_Initialize();
        }
        public MinputTldThread() : base() {
            InputTldThread_Initialize();
        }
		public void InputTldThread_Initialize()
		{
			// InitializeComponent();
            omVe = this;
            omMa.ApplicationVerbObjectSet(omVe);
            //
            this.omWt.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.omWt.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			this.omWt.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            //
			this.omWt.WorkerSupportsCancellation = true;
			this.omWt.WorkerReportsProgress = true;
            // int iTemp = ApplicationMbgWorkerObjectSet(this.omMa);
            lTemp1 = ApplicationVerbObjectSet(this);
            //
			// NameScope.SetNameScope(this, new NameScope());
			// lastStackPanel.RegisterName("wpfProgressBar", wpfProgressBar);           
		}
		#endregion
        // xxxxxxxxxxxxxxxxxxxxxx UI BUTTONS for START and CANCEL xxxxxxxxxxxxxxxx
        #region Synchronous Execution UI - NoEvents Button based start
        /// Handles click event for synchronousStart button. 
        public void CallerSynchronousStartClick(object sender, System.Windows.RoutedEventArgs e) {
            // this.synchronousCount.Text = ""; 
            InputTldThreadMethodHandler synchronousFunctionHandler = default(InputTldThreadMethodHandler);
            //
            synchronousFunctionHandler = new InputTldThreadMethodHandler(this.InputTldDoMethodSynchronous);
            //
            int returnValue = synchronousFunctionHandler.Invoke("1000000000");
            //
            // this.synchronousCount.Text = "Processing completed. " + returnValue + " rows processed."; 
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxx end of synchronouts UI
        // xxxxxxxxxxxxxxxxx start of Asychronous UI
        #region Asynchronous Event-Based Execution UI -Button based start
        // xxxxxxxxxxxxxxxxx Asynchronous xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx 
        /// Handles click event for wpfAsynchronousStart button. 
        public void CallerAsynchronousEventsStartClick(object sender, System.Windows.RoutedEventArgs eReaPassed) {
            ProgressChangedEventArgs ePcea = new ProgressChangedEventArgs(0,
                "$" + "Start");
            omCo.AppRunActionEvaluate(sender, ePcea, omCo);
            // Calls DoWork on secondary thread 
            this.omWt.RunWorkerAsync();
            // RunWorkerAsync returns immediately, start progress bar 
            // wpfProgressBarAndText.Visibility = Visibility.Visible; 
        }
        //
        /// Handles click event for cancel button. 
        public void CallerAsynchronousEventsCancelClick(object sender, System.Windows.RoutedEventArgs eReaPassed) {
            ProgressChangedEventArgs ePcea = new ProgressChangedEventArgs(0,
                "$" + "Cancel");
            omCo.AppRunActionEvaluate(sender, ePcea, omCo);
            // Cancel the asynchronous operation. 
            this.omWt.CancelAsync();
        }
        #endregion
        // xxxxxxxxxxxxxxxxx start of Asychronous UI
        #region Thread 2 NoEvents-based Asynchronous Execution Button based start
        /// Handles click event for asynchronousStart button. 
        public void CallerAsynchronousNoEventsStartClick(object sender, System.Windows.RoutedEventArgs eReaPassed) {
            ProgressChangedEventArgs ePcea = new ProgressChangedEventArgs(0,
                "$" + "Start");
            omCo.AppRunActionEvaluate(sender, ePcea, omCo);
            //
            AsyncMethodMessageHandler caller = default(AsyncMethodMessageHandler);
            caller = new AsyncMethodMessageHandler(this.InputTldAsynchronouMethod);
            // open new thread with callback method 
            caller.BeginInvoke("1000000000", CallbackMethod, null);
        }
        #endregion
        // end of UI
        #region THREAD 2
        #region Thread 2 Declarations
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxx THREAD TWO Declarations xxxxxxxxxxxxxxxxxxxxxx
        #region  Thread 2 Synchronous Declare Execution Delegate
        delegate int InputTldThreadMethodHandler(string sPassedMessage);
        #endregion
        #region Thread 2 Asynchronous Execution Declarations and delegates
        delegate int AsyncMethodMessageHandler(string sPassedMessage);
        delegate int AsyncMethodProcessChangeHandler(ProgressChangedEventArgs e);
        delegate void UpdateMessageUiHandler(string sPassedMessage);
        delegate void UpdateProgressUiHandler(ProgressChangedEventArgs sPassedMessage);
        // not used
        delegate void TextBoxChangeDelegate(object sender, string s);
        delegate void TextBoxAddDelegate(object sender, string s);
        delegate void ProgressCompletionDelegate(object sender, string sField, int iAmount, int iMax);
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxx THREAD 2 xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
		#region THREAD 2 MAIN
        // xxxxxxxxxxxxxxxxxx THREAD 2 MAIN xxxxxxxxxxxxxxxxxxxxxxxxxxx
        // DO WORK
		/// Runs on secondary thread. 
		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e) 
		{
            // don't start if cancel button clicked 
            if (omCo.bRunCancelPending) {
                return;
            }
            // call long running process and get eArmhResult 
            LocalIntResult = this.InputTldDoMethodAsync();
            e.Result = LocalIntResult;
		}
        /// Method is called everytime omWt.ReportProgress is called which triggers ProgressChanged event. 
		public void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs ePcea) 
		{
            try {
            UserState = (string) ePcea.UserState;
            } catch { UserState = ""; }
            if (UserState.Length > 0) { UserCommandPrefix = UserState.Substring(0, 1); }
            if (UserState.Length > 1) { UserCommand = UserState.Substring(1); }
            //
            if (UserCommandPrefix == "$") {
                omCo.AppRunActionEvaluate(sender, ePcea, omCo);
            }
			// Update UI with % completed. 
			// this.wpfCount.Text = eTcea.ProgressPercentage.ToString() + "% processed."; 
            // Still on secondary THREAD TWO, must update ui on primary thread 
            CallerUpdateSendProgressToUi(ePcea);
		} 
        // WORK COMPLETE
		/// Called when DoWork has completed. 
		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs eRwcea) 
		{ 
			// TODO Back on primary thread, can access ui controls 
			// wpfProgressBarAndText.Visibility = Visibility.Collapsed;
            omCo.bRunAbort = true;
            omCo.bRunCancelPending = false;
			if (eRwcea.Cancelled) {
				// this.wpfCount.Text = "Process Cancelled.";
                return;
			}
			else {
				// this.wpfCount.Text = "Processing completed. " + (string)eTcea.Result + " rows processed.";
			}
            // LocalRunning
            // omPm.LocalRunning = false;
			// this.myStoryboard.Stop(this.lastStackPanel); 
			// this.wpfAsynchronousStart.IsEnabled = true; 
			// this.wpfAsynchronousCancel.IsEnabled = false; 
		}
        // end of thread two MAIN
        #endregion
        #region Thread 2 EXECUTION with sample check cancel code
        // xxxxxxxxxxx THREAD TWO EXECUTION xxxxxxxxxxxxxxxxxxxxxxxxx
        #region Thread 2 Excution Async with no Events
        // xxxxxxxxxxxxxxxxxx THREAD 2  Async NO EVENTS XXXxxxxxxxxxxxxxxxxxx
        /// Used to simulate a long running function such as database call 
        /// or the iteration of many rows. 
        private int InputTldAsynchronouMethod(string sPassedMessage) {
            //
            // don't start if cancel button clicked 
            if (omCo.bRunCancelPending) {
                LocalIntResult = -1;
                return LocalIntResult;
            }
            LocalLongResult = ProcessOpenFile("Import", omCo.InputFile.FileSummary.FileName, omCo.InputFile, omCo.OutputFile.FileSummary.FileName, omCo.OutputFile, omCo.OutputFile.FileSummary.FileItemId, omCo.OutputFile.FileSummary.FileOptions);
            //
            return LocalIntResult;
        }
        #endregion
        // Asynchronous
        #region Thread 2 Asynchronous Execution with Events
        // xxxxxxxxxxxxxxxxx THREAD TWO Async Method xxxxxxxxxxxxxxxxxxxxxx
        // Check Cancell button and stops (belongs is called routine)
		/// Used to simulate a long running function such as database call 
		/// or the iteration of many rows. 
		private int InputTldDoMethodAsync() 
		{ 
            // don't start if cancel button clicked 
            if (omCo.bRunCancelPending) {
                LocalIntResult = -1;
                return LocalIntResult;
            }
            LocalLongResult = ProcessOpenFile("Import", omCo.InputFile.FileSummary.FileName, omCo.InputFile, omCo.OutputFile.FileSummary.FileName, omCo.OutputFile, omCo.OutputFile.FileSummary.FileItemId, omCo.OutputFile.FileSummary.FileOptions);
            // report progress of loop 
				if ((omWt != null) && omWt.WorkerReportsProgress) 
				{ 
					// omWt.ReportProgress(RunStateCurr / iteration); 
				} 
			return LocalIntResult; 
		}
        //
        #endregion
        // Synchrounous
        #region Thread 2 Synchronous Execution with NoEvents
        // xxxxxxxxxxxxxxxxxxxx THREAD TWO Synchrounous Execution
        /// Used to simulate a long running function such as database call 
        /// or the iteration of many rows. 
        private int InputTldDoMethodSynchronous(string sPassedMessage) {
            LocalLongResult = ProcessOpenFile("Import", omCo.InputFile.FileSummary.FileName, omCo.InputFile, omCo.OutputFile.FileSummary.FileName, omCo.OutputFile, omCo.OutputFile.FileSummary.FileItemId, omCo.OutputFile.FileSummary.FileOptions);

            return LocalIntResult;
        }
        #endregion
        // end of execution
        #endregion
        #region THREAD 2 COMMUNICATION
        // xxxxxxxxxxx THREAD 2 COMMUNICATION xxxxxxxxxxxxxxxxxxxxxxxxx
        /// Called when BeginInvoke is finished running. 
        protected void CallbackMethod(IAsyncResult earAscynMethodHandlerResult) {
            try {
                // Retrieve the delegate. 
                AsyncResult eArmhResult = (AsyncResult)earAscynMethodHandlerResult;
                AsyncMethodMessageHandler caller = (AsyncMethodMessageHandler)eArmhResult.AsyncDelegate;
                // Because this method is running from secondary thread it 
                // can never access ui objects because they are created 
                // on the primary thread. Uncomment the next line and 
                // run this demo to see for yourself. 
                // Call EndInvoke to retrieve the results. 
                int iReturnValue = caller.EndInvoke(earAscynMethodHandlerResult);
                sAnyExceptionMessage = "Nomal end of processing: code(" + iReturnValue.ToString() + ").";
                // Still on secondary THREAD TWO, must update ui on primary thread 
                CallerUpdateSendMessageToUi(iReturnValue.ToString());
            } catch (Exception eAnyException) {
                sAnyExceptionMessage = "Error in processing: " + eAnyException.Message;
                CallerUpdateSendMessageToUi(sAnyExceptionMessage);
            }
        }
        // CALL BACK TO UPDATE UI WITH MESSAGE ON MAIN THREAD
        /// THREAD TWO Setup delegate to update ui on THREAD ONE with results 
        public void CallerUpdateSendMessageToUi(string sPassedMessageUpdate) {
            // Get back to primary thread to update ui 
            if (sPassedMessageUpdate == null) { return; }
            if (MinputTldApp.Current == null) { return; }
            UpdateMessageUiHandler uiHandler = new UpdateMessageUiHandler(CallerSendMessageToUi);
            string results = sPassedMessageUpdate;
            // Run new thread off Dispatched (primary thread) 
            // this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, uiHandler, results);
            MinputTldApp.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, uiHandler, results);
        }
        // CALL BACK TO UPDATE UI WITH PROGRESS ON MAIN THREAD
        /// THREAD TWO Setup delegate to update ui on THREAD ONE with results 
        public void CallerUpdateSendProgressToUi(ProgressChangedEventArgs sPassedProgressUpdate) {
            // Get back to primary thread to update ui 
            if (sPassedProgressUpdate == null) { return; }
            if (MinputTldApp.Current == null) { return; }
            UpdateProgressUiHandler uiProgressHandler = new UpdateProgressUiHandler(CallerSendProgressToUi);
            ProgressChangedEventArgs results = sPassedProgressUpdate;
            // Run new thread off Dispatched (primary thread) 
            // this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, uiHandler, results);
            MinputTldApp.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, uiProgressHandler, results);
        }
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxx end of THREAD TWO xxxxxxxxxxxxxxxxxxxx
        // xxxx THREAD ONE xxxx Communications
        #region Thread one receive messages from thread two (thread one portion)
        // xxxxxx THREAD ONE invoked messeages from THREAD TWO xxxxxxxxx
        /// Update UI from Dispatcher Thread 
        public void CallerSendMessageToUi(string sPassedMessage) {
            // update user interface controls from primary UI thread 
            // this.visualIndicator.Text = "Processing Completed.";
            // this.asynchronousCount.Text = rowsupdated + " rows processed.";
            omCo.omPm.StatusLineMdmChanged(this, (string)sPassedMessage);
        }
        // xxxxxx THREAD ONE invoked messeages from THREAD TWO xxxxxxxxx
        /// Update UI from Dispatcher Thread 
        public void CallerSendProgressToUi(ProgressChangedEventArgs sPassedProgress) {
            // update user interface controls from primary UI thread 
            // this.visualIndicator.Text = "Processing Completed.";
            // this.asynchronousCount.Text = rowsupdated + " rows processed.";
            omCo.omPm.StatusLineMdmChanged(this, (ProgressChangedEventArgs)sPassedProgress);
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Action and Locals
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        // PROGRESS CHANGED
        string UserState;
        string UserCommandPrefix;
        string UserCommand;
        //
        #region Class Standard Root Word Constants
        // ON = YES = OK = true
        // OFF = NO = BAD = false
        const bool bON = true;
        const bool bOFF = false;
        //
        const bool bYES = true;
        const bool bNO = false;
        //
        const bool bOK = true;
        const bool bBAD = false;
        //
        const int iON = 1;
        const int iOFF = 0;
        //
        const int iYES = 1;
        const int iNO = 0;
        //
        const int iOK = 1;
        const int iBAD = 0;
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
    }	
}
