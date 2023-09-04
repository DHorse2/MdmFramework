using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

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
//using Mdm.Oss.Thread;
//using Mdm.Oss.Threading;
//using Mdm.Pick;
//using Mdm.Pick.Console;
//using Mdm.World;
#endregion

namespace Mdm.Oss.Std
{
    public partial class StdConsoleManagerDef
    {
        #region Trace Mdm Console Handling
        // CLASS MDM TRACE
        // ToDo StdConsoleManagerDef InProgress Class for control flags.  
        // This will be persisted.
        // not implemented yet
        /// <summary> 
        /// This class contains Trace and Console control fields.  These
        /// are only the flags and options that control which console
        /// features are uses and details such as the verbosity level.
        /// </summary> 
        public struct TraceConsoleFlagsDef
        {
            #region Declarations
            long TraceControlId;
            int ConsoleVerbosity;
            #endregion
            //
            /// <summary>
            /// Default constructor to initalize fields.
            /// </summary> 
            /// <param name="PassedTraceControlId">A unique ID used to identify this object</param> 
            /// <remarks>
            /// </remarks> 
            public void TraceConsoleFlags(long PassedTraceControlId)
            {

                TraceControlId = PassedTraceControlId;
                TraceControlUsesDef TraceControlUses = new TraceControlUsesDef();
                TraceControlUses = TraceControlUsesDef.None;
                ConsoleControlUsesDef ConsoleControlUses = new ConsoleControlUsesDef();
                ConsoleControlUses = ConsoleControlUsesDef.None;
                //
                // Console Control
                // <Area Id = "Console">
                ConsoleControlUses &= ConsoleControlUsesDef.ConsoleOn;
                ConsoleControlUses &= ConsoleControlUsesDef.ConsoleBasicOn;
                ConsoleControlUses &= ConsoleControlUsesDef.ConsoleToDisc;
                ConsoleControlUses &= ConsoleControlUsesDef.ConsoleToControl;
                //
                ConsoleControlUses &= ConsoleControlUsesDef.ConsoleApplication;
                ConsoleControlUses &= ConsoleControlUsesDef.ConsoleTextOn;
                //
                ConsoleControlUses &= ConsoleControlUsesDef.DoLogActivity;
                // Trace Control
                // <Area Id = "Trace">
                //TraceControlUses &= TraceControlUsesDef.TraceData = bNO;
                TraceControlUses &= TraceControlUsesDef.TraceOn;
                TraceControlUses &= TraceControlUsesDef.TraceDisplayMessageDetail;
                //TraceControlUses &= TraceControlUsesDef.TraceHeadings = bNO;
                //
                TraceControlUses &= TraceControlUsesDef.TraceDebugOn;
                TraceControlUses &= TraceControlUsesDef.TraceFirst;
                TraceControlUses &= TraceControlUsesDef.TraceHeadings;
                //
                //TraceControlUses &= TraceControlUsesDef.TraceIteration = bOFF;
                //TraceControlUses &= TraceControlUsesDef.TraceDisplay = bOFF;
                //TraceControlUses &= TraceControlUsesDef.TraceBug = bOFF;
                //TraceControlUses &= TraceControlUsesDef.TraceDisplayMessageDetail = false;
                //
                ConsoleVerbosity = 9;
            }
        }
        //
        #region TraceControl Flags
        // public TraceControlUsesDef TraceControlUses;
        public new bool DoLogActivity;
        //
        public bool TraceDebugOn;
        public bool TraceDebugDoErrorPrompt;
        public bool TraceOn;
        public bool TraceFirst;
        //
        public new bool TraceHeadings;
        public new bool TraceData;
        // stop detail display after number of iterations
        public new bool TraceBug;
        public bool TraceBreakOnAll;
        public new bool TraceDisplayMessageDetail;
        //
        public bool TraceIteration;
        public bool TraceIterationOnNow;
        public bool TraceIterationInitialState;
        public bool TraceIterationRepeat;
        //
        public bool TraceDisplay;
        public bool TraceDisplayOnNow;
        public bool TraceDisplayInitialState;
        public bool TraceDisplayRepeat;
        //
        //public bool ConsoleApplication;
        //public bool ConsoleOn;
        //public bool ConsoleToDisc;
        //public bool ConsoleToControl;
        //// 
        //public bool ConsoleTextOn;
        //public bool ConsoleBasicOn;

        #endregion
        public TraceConsoleFlagsDef TraceConsoleFlags;

        // Trace Fields:
        // Trace Increment Result
        public StateIs TraceMdmIncrementResult;
        public StateIs TraceMdmBasicResult;
        public StateIs TraceMdmPointResult;
        // will return the two location information fields, char
        // These fields are included in displays, logging and tracing
        // execution and errors.
        public virtual int TraceMdmCounterLevel1GetDefault() { return 0; }
        public virtual int TraceMdmCounterLevel2GetDefault() { return 0; }
        // In the import file utility locations within the file data is important
        // a so the two fields are returned by overloaded functions:
        // Buf.CharMaxIndex, and Buf.AttrIndex

        /// <summary>
        /// Provides a low level common point for counting iterations
        /// in the primary loop or within any single instance or class.
        /// </summary> 
        /// <param name="PassedTraceMessage"></param> 
        /// <returns>
        /// Currently returns sucess result, should be increment value.
        /// </returns>
        /// <remarks>
        /// Change to return increment (code check first)
        /// </remarks> 
        public StateIs TraceMdmDoIncrement(String PassedTraceMessage)
        {
            TraceMdmIncrementResult = StateIs.Started;
            //
            // Process Tracing Message Increment
            //
            //if (PassedTraceMessage == "TraceIncrement") {
            TraceIterationCount += 1;
            TraceIterationCountTotal += 1;
            //TraceMessage = PassedTraceMessage;
            //if (TraceData) {
            //    if (TraceOn || TraceIterationOnNow || TraceDisplayOnNow || TraceBugOnNow || ConsoleOn || RunErrorDidOccurOnce || RunErrorDidOccur) {
            //        // if (TraceMessageBlockString.Length > 0) {
            //        TraceMessageBlock += TraceMessageBlockString + "|";
            //        TraceMessageBlockString = sEmpty;
            //        // }
            //    }
            //}

            //if (TraceMessageBlock.Length > 100) {
            //    ((ImTrace)ConsoleSender).TraceMdmDoImpl(5, 1, ref Sender, bIsMessage, iNoOp, iNoOp, TraceMdmIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, PassedTraceMessage);
            //}
            //}
            return TraceMdmIncrementResult;
        }
        #endregion
    }
    [Flags]
    public enum TraceControlUsesDef
    {
        None = 0,
        //
        TraceDebugOn = 1 << 2,
        TraceOn = 1 << 3,
        TraceFirst = 1 << 4,
        //
        TraceHeadings = 1 << 5,
        TraceData = 1 << 6,
        // stop detail display after number of iterations
        TraceBug = 1 << 9,
        TraceBreakOnAll = 1 << 10,
        TraceDisplayMessageDetail = 1 << 11,
        //
        TraceIteration = 1 << 18,
        TraceIterationOnNow = 1 << 19,
        TraceIterationInitialState = 1 << 20,
        TraceIterationRepeat = 1 << 21,
        //
        TraceDisplay = 1 << 12,
        TraceDisplayOnNow = 1 << 13,
        TraceDisplayInitialState = 1 << 14,
        TraceDisplayRepeat = 1 << 15,
    }
    /// <summary>
    /// <para> Status Line User Interface</para>
    /// <para> This class manages status line strings,
    /// text boxes and the console.  There are four zones
    /// or text boxes for Status Line information and
    /// a single Text Console.</para>
    /// <para> . </para>
    /// <para> In terms of usage and implementation one of
    /// two layouts are employed.</para>
    /// <para>First, a UI control that is a standard status
    /// line composed of four zones.</para>
    /// <para>Second, A group of four text boxes that are
    /// independent in terms of size and options.</para>
    /// <para>The Text Console is used identically to the
    /// other four Line Items.  It can reliably be considered
    /// the fifth.  The Console differs in it Two-way nature
    /// and integration with the OS.</para>
    /// <para> . </para>
    /// <para> Regardless of the
    /// user interface element used to display the information,
    /// this class allows a central point that all classes
    /// can direct the output to.  It in turn can redirect output based
    /// on the setting of the various delegates.</para>
    /// </summary>
}