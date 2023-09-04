using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Mdm.Oss.Decl;
using Mdm.Oss.Console;
using Mdm.Oss.File;
using Mdm.Oss.Thread;
using Mdm.Oss.Std;
using Mdm.Oss.Components;

namespace Mdm.Oss.Std
{
    public enum DbCommandIs
    {
        None,   // not set
        Insert, // insert row
        Delete, // delete row
        Update, // update row
        Select, // for populate and read
        Table,  // for clearing table and similar
        Clear   // clear data from table
    }
}
namespace Mdm.Oss.Console
{
    [Flags]
    public enum ConsoleControlUsesDef
    {
        None = 0,
        //
        ConsoleApplication = 1 << 1,
        ConsoleOn = 1 << 3,
        ConsoleToDisc = 1 << 4,
        // ToDo this isn't quite right. Any one console might hook to a control
        ConsoleToControl = 1 << 5,
        // 
        ConsoleTextOn = 1 << 6,
        ConsoleBasicOn = 1 << 7,

        DoLogActivity = 1 << 8,
    }
    public class ConsoleTaskDef : CalculationTaskDef
    {
        public delegate void ConsoleTaskMessageEventHandler(
            object sender, ConsoleMessageEventArgs e);

        public event ConsoleTaskMessageEventHandler ConsoleTaskMessageEvent;

        public ConsoleTaskDef(ref object SenderPassed, ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ref SenderPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        {
        }
        public void FireConsoleTaskMessageEvent(mMsgDetailsDef Message)
        {
            if (ConsoleTaskMessageEvent != null)
            {
                //string ThreadName = "Thread" + System.Threading.Thread.CurrentThread.ManagedThreadId;
                //Message.Text = ThreadName + ": " + Message.Text;

                ConsoleMessageEventArgs args = new ConsoleMessageEventArgs(Message);
                if (ConsoleTaskMessageEvent.Target is
                        System.Windows.Forms.Control)
                {
                    System.Windows.Forms.Control targetForm = ConsoleTaskMessageEvent.Target
                            as System.Windows.Forms.Control;
                    targetForm.BeginInvoke(ConsoleTaskMessageEvent,
                            new object[] { this, args });
                }
                else
                {
                    MethodInfo targetMethod = ConsoleTaskMessageEvent.Method;
                    targetMethod.Invoke(ConsoleTaskMessageEvent,
                            new object[] { args });
                    //ConsoleTaskMessageEvent(this, args);
                }
                //System.Threading.Thread.Yield();
                //System.Threading.Thread.Sleep(10);
            }
        }
    }
    public class ConsoleMessageEventArgs : CalculationEventArgs
    {
        public mMsgDetailsDef Message;

        public ConsoleMessageEventArgs(mMsgDetailsDef PassedMessage)
        {
            Message = PassedMessage;
            Status = CalculationStatus.Calculating;
            SetButtons = false;
        }
        public ConsoleMessageEventArgs(int progress)
        {
            Progress = progress;
            Status = CalculationStatus.Calculating;
            SetButtons = false;
        }

        public ConsoleMessageEventArgs(CalculationStatus PassedStatus, bool PassedSetButtons)
        {
            Status = PassedStatus;
            SetButtons = PassedSetButtons;
        }
        public ConsoleMessageEventArgs()
        {
            //
        }
    }
    public class ConsoleThreadDef
    {
        public DbCommandIs SqlCommandType;
        public string TaskType;
        public int TaskId;
        //public CrDataDef CrData;
        //public LinkDataDef LinkData;
        public object mData;
        public int RowCount;
        public bool PostRowData;
    }
}
