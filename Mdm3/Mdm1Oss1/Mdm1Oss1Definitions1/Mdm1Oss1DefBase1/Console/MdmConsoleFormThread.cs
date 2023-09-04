using Shell32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
//using System.Windows;
using System.Windows.Forms;
//using System.Windows.Controls; // Page

using Mdm;
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
// using Mdm.Oss.Mobj;
using Mdm.Oss.Thread;
using Mdm.Oss.Std;
using Mdm.Oss.Components;

//using Mdm.Oss.WinUtil;

using HWND = System.IntPtr;
using System.Windows;
using System.Windows.Interop;

namespace Mdm.Oss.Console
{
    public partial class ConsoleFormDef
    {
        private void MessageFilter_ClickThread()
        {
            ConsoleThreadDef MessageFilterThread = new ConsoleThreadDef();
            MessageFilterThread.TaskType = "ScAnalysis";
            MessageFilterThread.SqlCommandType = DbCommandIs.Select;

            //string temp = StdBaseDef.DriveOs + @"\Srt Project1\Links to Folders";
            // ON UI THREAD!

            //string temp = StdBaseDef.DriveOs + @"\Srt Project1\Links to Folders";
            // for (int i = 0; DirectorySelect(); DirectoryCount++)
            // if (OpenDirectorySelect())
            {
                //
                // Create new task object to manage the calculation.
                MessageFilterTask = new ConsoleTaskDef(ref Sender, ref ConsoleSender, ConsoleSource, ClassRole, ClassFeatures);
                MessageFilterTask.TaskType = "MessageFilter";
                //MessageFilterTask.SetButtons = StdBaseRunControlUi.SetButtons;
                //MessageFilterTask.SetButtons = true;
                int Id = MessageFilterTask.Id;
                MessageFilterTaskId = Id;
                MessageFilterThread.TaskId = Id;
                if (MainThreadIdDoSet) { MainThreadId = Id; MainThreadIdDoSet = false; }

                // Subscribe to the calculation status event.
                MessageFilterTask.CalculationStatusChanged += new
                  ConsoleTaskDef.CalculationStatusEventHandler(OnTaskStatusChanged);

                // Subscribe to the calculation progress event.
                MessageFilterTask.CalculationProgressChanged += new
                  ConsoleTaskDef.CalculationProgressEventHandler(OnTaskProgressChanged);

                // Create a delegate to the calculation method.
                MessageFilterTaskDel =
                        new ConsoleTaskDef.CalculationLongDelegate(MessageFilter_ClickThreadDo);

                //// Create a delegate for Data to UI
                //MessageFilterTask.FileSqlScTaskDataEvent += new
                //ConsoleTaskDef.ScTaskDataEventHandler(OnFileSqlScTaskDataEvent);

                // Create a delegate for UI Messages
                MessageFilterTask.ConsoleTaskMessageEvent += new
                    ConsoleTaskDef.ConsoleTaskMessageEventHandler(st.OnTraceMdmDoImpl);

                MessageFilterTask.RowCount = GridView.Rows.Count;

                MessageFilterTask.StartCalculation(MessageFilterThread, (ConsoleTaskDef.CalculationLongDelegate)MessageFilterTaskDel);

                // ButtonStart_ClickThreadDo(openDirectoryName);
            }
        }
        private StateIs MessageFilter_ClickThreadDo(object CalculationTaskThreadPassed)
        {
            MessageFilterThreadResult = StateIs.Started;
            int RowCountLocal;
            try
            {
                MessageFilterTaskThread = CalculationTaskThreadPassed as ConsoleThreadDef;
                if (MessageFilterTaskThread == null) { return StateIs.Failed; } // failed
                MessageFilterTaskId = MessageFilterTaskThread.TaskId;

                RowCountLocal = ((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[MessageFilterTaskId.ToString()]).RowCount;
            }
            catch (Exception) { return StateIs.Failed; }

            string TraceMessage;

            VerbosityHidden = 0;
            VerbosityVisible = 0;

            for (RowIndex = RowCountLocal - 1; RowIndex >= 0; RowIndex--)
            {
                //TraceMessage = "Filter read line: " + RowIndex.ToString();
                //st.TraceMdmDoBasic(TraceMessage, ConsoleFormUses.DebugLog, 13);

                try
                {
                    // Check for cancel
                    if (((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[MessageFilterTaskId.ToString()]).CalcState == CalculationStatus.CancelPending)
                    {
                        break;
                    }

                    FilterRow(RowIndex);

                    // Update Progress
                    ((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[MessageFilterTaskId.ToString()]).FireProgressChangedEvent(DirectoryCount, FileCount, LinkCount);
                }
                catch (Exception)
                {
                    // throw message instead.
                    string LocalMessage = "Message filter or calculation error.";
                    LocalMessage += " Row: " + RowIndex.ToString();
                    LocalMessage += " of " + RowCountLocal.ToString() + " rows.";
                    st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 7);
                    // throw;
                }

                System.Threading.Thread.Sleep(100);
            }

            return StateIs.Successful;
            //throw new NotImplementedException();
        }
        private StateIs ScAnalysisDispose()
        {
            StateIs ScAnalysisDisposeResult = StateIs.Started;
            ((CalculationTaskDef)CalculationTaskDef.CalculationTaskDict[MessageFilterTaskId.ToString()]).Dispose();
            return ScAnalysisDisposeResult = StateIs.Successful;
        }
        public void VisibilitySetRowLast(bool VisibilityPassed)
        {
            int RowIndex = GridView.Rows.Count - 1;
            if (RowIndex >= 0)
            {
                GridView.Rows[RowIndex].Visible = VisibilityPassed;
            }
        }
        public void VisibilitySetRow(int RowIndexPassed, bool VisibilityPassed)
        {
            GridView.Rows[RowIndexPassed].Visible = VisibilityPassed;
        }
        public void ButtonTextSet(ToolStripButton ButtonPassed, string TextPassed)
        {
            ButtonPassed.Text = TextPassed;
        }
        public bool FilterRow(int RowIndex)
        {
            VerbosityHidden = 0;
            VerbosityVisible = 0;

            //for (RowIndex = Cf.Rows.Count - 1; RowIndex >= 0; RowIndex--)
            //{
            try
            {
                if ((int)Convert.ToInt32(GridView.Rows[RowIndex].Cells["Verbosity"].Value) <= Verbosity)
                {

                    this.BeginInvoke(
                        (MethodInvoker)(() =>
                        VisibilitySetRow(RowIndex, true)
                        ))
                        ;
                    // Cf.Rows[RowIndex].Visible = true;
                    VerbosityVisible += 1;
                    //System.Threading.Thread.Yield();
                    //this.BeginInvoke(
                    //    (MethodInvoker)(() =>
                    //    Cf.FirstDisplayedScrollingRowIndex = RowIndex
                    //    ))
                    //    ;
                }
                else {
                    this.BeginInvoke(
                        (MethodInvoker)(() =>
                        VisibilitySetRow(RowIndex, false)
                        ))
                        ;
                    // Cf.Rows[RowIndex].Visible = false;
                    VerbosityHidden += 1;
                }
                System.Threading.Thread.Sleep(0);
                //System.Threading.Thread.Yield();
                this.BeginInvoke(
                    (MethodInvoker)(() =>
                    Invalidate()
                    ))
                    ;
                System.Threading.Thread.Sleep(0);
                //System.Threading.Thread.Yield();
                System.Threading.Thread.Sleep(200);

            }
            catch (Exception)
            {

                // throw;
            }
                System.Threading.Thread.Sleep(100);
            //}

            //foreach (DataGridViewRow row in Cf.Rows)
            //{
            //    try
            //    {
            //        if ((int)Convert.ToInt32(row.Cells["Verbosity"].Value) <= Verbosity)
            //        {
            //            row.Visible = true; VerbosityVisible += 1;
            //        }
            //        else { row.Visible = false; VerbosityHidden += 1; }
            //    }
            //    catch (Exception)
            //    {

            //        // throw;
            //    }
            //}
            this.BeginInvoke(
                (MethodInvoker)(() =>
                ButtonTextSet(ButtonDn, VerbosityHidden.ToString() + " V")
                ))
                ;
            this.BeginInvoke(
                (MethodInvoker)(() =>
                ButtonTextSet(ButtonUp, VerbosityVisible.ToString() + " ^")
                ))
                ;
            this.BeginInvoke(
                (MethodInvoker)(() =>
                ButtonTextSet(ButtonFilter, "V:" + Verbosity.ToString() + " " + GridView.Rows.Count.ToString())
                ))
                ;

            //ButtonDn.Text = VerbosityHidden.ToString() + " V";
            //ButtonUp.Text = VerbosityVisible.ToString() + " ^";
            //ButtonFilter.Text = "V:" + Verbosity.ToString() + " " + Cf.Rows.Count.ToString();

            //string[] rowString = new string[4];
            //rowString[0] = CfData.Id.ToString();
            //rowString[1] = CfData.Sequence;
            //rowString[2] = CfData.InputString;
            //rowString[3] = CfData.OutputString;
            //Cf.Rows.Insert(RowNumberSelected, rowString);
            //if (DeleteNext)
            //{
            //    // ???? Save Value ?????
            //    // DataGridViewRow temp = Cf.Rows[RowNumberSelected + 1];
            //    Cf.Rows.RemoveAt((Int32)(RowNumberSelected + 1));
            //}
            //RowNumberCount += 1;
            //if (CfData.Id > CfIdCurrent) { CfIdCurrent = CfData.Id; }
            return true;

        }
        public void OnTaskStatusChanged(Object sender, CalculationEventArgs e)
        {
            CalculationTaskDef DbTaskLocal;
            CalculationEventArgs LocalCalculationEventArgs = e;
            if (LocalCalculationEventArgs == null) { return; }
            if (sender is CalculationTaskDef)
            {
                if (sender == null) { return; }
                DbTaskLocal = (CalculationTaskDef)sender;
            }
            else { return; }

            string LocalMessage = "UI Status change on ThreadId: ";
            LocalMessage += DbTaskLocal.Id.ToString();
            LocalMessage += " (" + DbTaskLocal.TaskType + ")";
            //if (DbTaskLocal.WaitLockMutex == null) { DbTaskLocal.WaitLockMutex = new Mutex(); }
            switch (LocalCalculationEventArgs.Status)
            {
                case CalculationStatus.PausePending:
                    StdRunControlUi.ButtonPause.Text = "Wait";
                    if (StdRunControlUi.SetButtons)
                    {
                        StdRunControlUi.ButtonCancel.Enabled = false;
                        ButtonUp.Enabled = false;
                        ButtonDn.Enabled = false;
                        ButtonFilter.Enabled = false;
                    }
                    LocalMessage += " Pause is Pending";
                    break;
                case CalculationStatus.Paused:
                    StdRunControlUi.ButtonPause.Text = "Resume";
                    if (StdRunControlUi.SetButtons)
                    {
                        ButtonUp.Enabled = false;
                        ButtonDn.Enabled = false;
                        ButtonFilter.Enabled = false;
                    }
                    LocalMessage += " Paused";
                    break;
                case CalculationStatus.ResumePending:
                    StdRunControlUi.ButtonPause.Text = "Wait";
                    if (StdRunControlUi.SetButtons)
                    {
                        ButtonUp.Enabled = false;
                        ButtonDn.Enabled = false;
                        ButtonFilter.Enabled = false;
                        StdRunControlUi.ButtonCancel.Enabled = false;
                    }
                    LocalMessage += " Resume is Pending";
                    break;
                case CalculationStatus.Pending:
                    StdRunControlUi.ButtonPause.Text = "Pending";
                    //if (!DbTaskLocal.WaitIsBusy)
                    //{
                    //    DbTaskLocal.WaitLockMutex.WaitOne();
                    //    DbTaskLocal.WaitIsBusy = true;
                    //}
                    //if (DbTaskLocal.SetButtons) { }
                    //if (LocalCalculationEventArgs.SetButtons)
                    //if (st.RunControlUi.SetButtons)
                    if (StdRunControlUi.SetButtons)
                    {
                        ButtonUp.Enabled = false;
                        ButtonDn.Enabled = false;
                        ButtonFilter.Enabled = false;
                        StdRunControlUi.ButtonCancel.Enabled = true;
                        StdRunControlUi.ButtonPause.Enabled = true;
                    }
                    LocalMessage += " Calculation is Pending";
                    break;

                case CalculationStatus.Calculating:
                    //if (!DbTaskLocal.WaitIsBusy)
                    //{
                    //    DbTaskLocal.WaitLockMutex.WaitOne();
                    //    DbTaskLocal.WaitIsBusy = true;
                    //}
                    StdRunControlUi.ButtonPause.Text = "Pause";
                    if (StdRunControlUi.SetButtons)
                    {
                        ButtonUp.Enabled = false;
                        ButtonDn.Enabled = false;
                        ButtonFilter.Enabled = false;
                        StdRunControlUi.ButtonCancel.Enabled = true;
                        StdRunControlUi.ButtonPause.Enabled = true;
                        LabelScriptListBusyMessage.Text = " Busy please wait...";
                        LabelScriptListFileCount.Invalidate();
                    }
                    LocalMessage += " Calculating";
                    break;

                case CalculationStatus.NotCalculating:
                    if (StdRunControlUi.SetButtons)
                    {
                        ButtonUp.Enabled = true;
                        ButtonDn.Enabled = true;
                        ButtonFilter.Enabled = true;
                        StdRunControlUi.ButtonCancel.Enabled = false;
                        StdRunControlUi.ButtonPause.Enabled = false;
                        StdRunControlUi.ButtonPause.Text = "Completed";
                        LabelScriptListBusyMessage.Text = sEmpty;
                        LabelScriptListFileCount.Invalidate();
                    }
                    //if (DbTaskLocal.WaitIsBusy)
                    //{
                    //    DbTaskLocal.WaitLockMutex.ReleaseMutex();
                    //    DbTaskLocal.WaitIsBusy = false;
                    //}
                    LocalMessage += " Finished Calculating";
                    break;
                case CalculationStatus.CancelPending:
                    if (StdRunControlUi.SetButtons)
                    {
                        ButtonUp.Enabled = false;
                        ButtonDn.Enabled = false;
                        ButtonFilter.Enabled = false;
                        StdRunControlUi.ButtonCancel.Enabled = false;
                        StdRunControlUi.ButtonPause.Enabled = false;
                        StdRunControlUi.ButtonPause.Text = "Cancelling";
                    }
                    LocalMessage += " Cancel is Pending";
                    break;
            }
            st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 13);
        }
        public void OnTaskProgressChanged(Object sender, CalculationEventArgs e)
        {
            // This should execute on the UI thread...
            if (e.FileProgress == 0 && e.DirectoryProgress == 0) { return; }
            //CrListLabelDirectoryCount.Text = e.DirectoryProgress.ToString();
            //CrListLabelDirectoryCount.Invalidate();
            //CrListLabelFileCount.Text = e.FileProgress.ToString();
            //CrListLabelFileCount.Invalidate();
        }
        public void OnTaskDataEvent(Object sender, CalculationEventArgs e)
        {
            CalculationTaskDef DbTaskLocal;
            if (sender is CalculationTaskDef)
            {
                if (sender == null) { return; }
                DbTaskLocal = (CalculationTaskDef)sender;
            }
            else { return; }

            string LocalMessage = "Data send event from ThreadId: ";
            LocalMessage += DbTaskLocal.Id.ToString();
            LocalMessage += ".";
            st.TraceMdmDoBasic(LocalMessage, ConsoleFormUses.DebugLog, 14);

        }
    }
}
