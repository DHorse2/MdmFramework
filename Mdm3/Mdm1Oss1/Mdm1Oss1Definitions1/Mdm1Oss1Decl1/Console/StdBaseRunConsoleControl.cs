using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Mdm;
using Mdm.Oss.Decl;
using Mdm.Oss.File;
using Mdm.Oss.Std;

namespace Mdm.Oss.Std
{
    public partial class StdBaseRunDef 
    {
        #region ConsoleMdmDeclarations
        public bool DoLogActivity;
        public bool DoLogActivityDefault;
        // Tracing Detail
        public StateIs TraceResult;
        // public bool TraceDebugOn;
        // public bool TraceDebugOn;
        // public bool TraceBreakOnAll;
        // public bool TraceOn;
        // public bool TraceFirst = bYES;
        // Trace Attributes Processed
        public int TraceIterationCount;
        public int TraceIterationCheckPointCount;
        public int TraceIterationCountTotal;
        public int TraceIterationCurrentDetail;
        //
        public bool TraceHeadings;
        public bool TraceData;
        public String TraceDataPointers;
        public String TraceErrorMessage;
        // Trace Iteration Control
        // threshold to stop displaying full detail on iteration count
        // stop detail display after number of iterations
        //public bool TraceIteration;
        //public bool TraceIterationOnNow;
        //public bool TraceIterationInitialState;
        //public bool TraceIterationRepeat;
        // threshold for pause on iteration count
        // this is a check point to interact with user
        public bool TraceIterationCheckPointOn;
        public int TraceIterationCheckPoint;
        // Use 0 to trace initialization, 1 to start at details
        public int TraceIterationThreshold;
        public int TraceIterationOnForCount;
        public bool TraceIterationOnForWarningGiven;
        public int TraceIterationOnAgainCount;
        // Trace Lines Displayed Control
        public int TraceDisplayCount;
        public int TraceDisplayCheckPointCount;
        public int TraceDisplayCountTotal;
        //
        public int TraceCharacterCount;
        //
        public int TracePercentCompleted;
        ////
        //public bool TraceDisplay;
        //public bool TraceDisplayOnNow;
        //public bool TraceDisplayInitialState;
        //public bool TraceDisplayRepeat;
        // threshold for pause on number of messages
        // that could have been displayed
        public bool TraceDisplayCheckPointOn;
        public int TraceDisplayCheckPoint;
        //
        public int TraceDisplayThreshold;
        public int TraceDisplayOnForCount;
        public int TraceDisplayOnAgainCount;
        //
        // Bug in fields or areas Control
        public int TraceBugCount;
        public int TraceBugCheckPointCount;
        public int TraceBugCountTotal;
        //
        public bool TraceBug;
        public bool TraceBugOnNow;
        public bool TraceBugInitialState;
        public bool TraceBugRepeat;
        // After Threshold line display details
        // for OnForCount lines then return to
        // summary mode.  After another OnAgainCount
        // lines repeate display OnForCount.
        public bool TraceBugCheckPointOn;
        public int TraceBugCheckPoint;
        public bool TraceBugCheckPointDo;
        //
        public int TraceBugThreshold;
        public int TraceBugOnForCount;
        public int TraceBugOnAgainCount;
        //
        public bool TraceDisplayMessageDetailOn;

        public String TraceDisplayMessageDetail;
        public String TraceMessage;
        public String TraceMessageTarget;
        public String TraceMessagePrefix;
        public String TraceMessageSuffix;
        public String TraceMessageFormated;
        public String TraceMessageToPrint;
        public String TraceTemp;
        public String TraceTemp1;
        public static object TraceLock;

        public String LocalUserEntry;
        public long LocalUserEntryLong;

        public String TraceMessageBlockString;
        public String TraceMessageBlock;

        public int TraceByteCountTotal;
        public int TraceByteCount;
        public int TraceShiftIndexByCount;
        #endregion
        #region ConsoleMessageTarget
        public String MessageStatusTargetText;
        public int MessageStatusSubTarget;
        public double MessageStatusSubTargetDouble;
        //
        public int ProcessStatusTarget;
        public int ProcessStatusSubTarget;
        public double ProcessStatusTargetDouble;
        //
        public int ProcessStatusTargetState;
        #endregion
        #region ConsoleProgressBar
        public double ProgressBarMdm1Property;
        public double MessageProperty2;
        #endregion
        #region ConsoleControlFlags
        // Run Control
        public bool RunControlOn;
        // Std_I0_Console
        // Not used in ShortcutUtils weirdly... What's up? ToDo
        // Used by PickDb.
        public bool ConsoleApplication;
        public bool ConsoleOn;
        public bool ConsoleToDisc;
        public bool ConsoleToControl;
        public bool ConsoleInputOn;
        public bool ConsoleTextOn;
        public bool ConsoleBasicOn;
        public bool ConsolePickConsoleOn;
        //
        // ToDo @$@ Verbosity is set to 3 here!
        // Use 3 (completion messages), 
        // 7 (step level completion), 
        // 9 (detailed output), 
        // or 20 (all).
        // MORE
        // In the event of an error you can
        // it temporarily set Verbosity to the
        // highest level. IE for troubleshooting.
        // It can't keep a preceeding fully detailed
        // log due to performance issues at high levels.
        // IE There is no "all" messages being collected.
        //
        // ToDo @$@ Verbosity is set to 3 here!
        // Use 3 (completion messages), 
        // 7 (step level completion), 
        // 9 (detailed output), 
        // or 20 (all).
        public int ConsoleVerbosity; //  = 5;
        // Display
        #endregion
        #region ConsoleOutput
        // Display
        public String ConsoleOutput;
        public String ConsoleOutputLog;
        // public ConsoleTextBlock;
        public String ConsoleTextBlock; // text block
        public int ConsoleTextPositionX;
        public int ConsoleTextPositionY;
        public int ConsoleTextPositionZ;
        public Point ConsoleTextPositionOrigin;
        //
        public int MessageLevelLast;
        #endregion
        #region ConsoleTextMessageOutput
        public String sMessageText;
        public String sMessageText0;
        public String MessageTextOutConsole;
        public String MessageTextOutStatusLine;
        public String MessageTextOutProgress;
        public String MessageTextOutError;
        public String MessageTextOutRunAction;
        //
        public String MessageStatusAction;
        public String ProcessStatusAction;
        #endregion
        #region TextConsole
        // <Area Id = "TextConsole>
        public String ConsolePickTextConsole;
        public String CommandLineRequest;
        public int CommandLineRequestResult;
        public String TextConsole;
        #endregion
        #region Command properties
        protected int zMdmCommandId;
        public int MdmCommandId { get { return zMdmCommandId; } set { zMdmCommandId = value; } }
        protected String zMdmCommandName;
        public String MdmCommandName { get { return zMdmCommandName; } set { zMdmCommandName = value; } }
        protected String zMdmCommandTitle;
        public String MdmCommandTitle { get { return zMdmCommandTitle; } set { zMdmCommandTitle = value; } }
        protected int zMdmCommandNumber;
        public int MdmCommandNumber { get { return zMdmCommandNumber; } set { zMdmCommandNumber = value; } }
        protected StateIs zMdmCommandStatus;
        public StateIs MdmCommandStatus { get { return zMdmCommandStatus; } set { zMdmCommandStatus = value; } }
        protected String zMdmCommandStatusText;
        public String MdmCommandStatusText { get { return zMdmCommandStatusText; } set { zMdmCommandStatusText = value; } }
        protected StateIs zMdmCommandIntResult;
        public StateIs MdmCommandIntResult { get { return zMdmCommandIntResult; } set { zMdmCommandIntResult = value; } }
        protected bool zMdmCommandBoolResult;
        public bool MdmCommandBoolResult { get { return zMdmCommandBoolResult; } set { zMdmCommandBoolResult = value; } }
        #endregion
        #region Console properties
        protected int zConsoleMdmId;
        public int ConsoleMdmId { get { return zConsoleMdmId; } set { zConsoleMdmId = value; } }
        protected String zConsoleMdmName;
        public String ConsoleMdmName { get { return zConsoleMdmName; } set { zConsoleMdmName = value; } }
        protected String zConsoleMdmTitle;
        public String ConsoleMdmTitle { get { return zConsoleMdmTitle; } set { zConsoleMdmTitle = value; } }
        protected int zConsoleMdmNumber;
        public int ConsoleMdmNumber
        {
            get { return zConsoleMdmNumber; }
            set
            {
                zConsoleMdmNumber = value;
            }
        }
        protected StateIs zConsoleMdmStatus;
        public StateIs ConsoleMdmStatus { get { return zConsoleMdmStatus; } set { zConsoleMdmStatus = value; } }
        protected String zConsoleMdmStatusText;
        public String ConsoleMdmStatusText { get { return zConsoleMdmStatusText; } set { zConsoleMdmStatusText = value; } }
        protected StateIs zConsoleMdmIntResult;
        public StateIs ConsoleMdmIntResult { get { return zConsoleMdmIntResult; } set { zConsoleMdmIntResult = value; } }
        protected bool zConsoleMdmBoolResult;
        public bool ConsoleMdmBoolResult { get { return zConsoleMdmBoolResult; } set { zConsoleMdmBoolResult = value; } }
        #endregion
        public virtual void InitializeConsole()
        {
            ClassFeatureFlag.InitializeConsoleDone = true;
            ConsoleApplication = bYES;
            ConsoleOn = bON;
            ConsoleToDisc = bOFF;
            ConsoleToControl = bON;
            ConsoleTextOn = bON;
            ConsoleBasicOn = bOFF;
            ConsolePickConsoleOn = bOFF;
            //
            DoLogActivity = false;
            DoLogActivityDefault = true;
            TraceResult = StateIs.NotSet;
            zMdmCommandName = sUnknown;
            zMdmCommandTitle = sUnknown;
            zMdmCommandNumber = iUnknown;
            zMdmCommandStatus = StateIs.NotSet;
            zMdmCommandStatusText = sUnknown;
            zMdmCommandIntResult = StateIs.NotSet;
            zMdmCommandBoolResult = false;
            zConsoleMdmId = iUnknown;
            zConsoleMdmName = sUnknown;
            zConsoleMdmTitle = sUnknown;
            zConsoleMdmNumber = iUnknown;
            zConsoleMdmStatus = StateIs.NotSet;
            zConsoleMdmStatusText = sUnknown;
            zConsoleMdmIntResult = StateIs.NotSet;
            zConsoleMdmBoolResult = false;

        }
    }
}
