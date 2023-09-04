using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls; // Page

using Mdm.Oss.Decl;
// using Mdm.Oss.Mobj;
// using Mdm.Pick.Syntax;
using Mdm.Pick.Console;
// TODO $$MAJOR 1) Create TraceMdm Aurgument structure
// TODO $$MAJOR 2) Create Indexer for RunAction and remove redundant settings
// TODO $$MAJOR 2) should use indexers passed to set RunAction and RunMetric values
// TODO $$MAJOR 2) which will reduce lines of code.
// TODO $$MAJOR 4) Don't pass run action values to TraceMdm but use current value.
// TODO $$MAJOR 5) Implement TLD for data after testing dict to schema code.

namespace Mdm.Oss.Decl {

    /// <summary> 
    /// <para>(DefStdBaseRunFile) Console implements the
		/// Trace, Logging, Console and messaging 
    /// operations.</para>
    /// <para> . </para>
    /// <para> See the Programming Standards ReadMe</para>
    /// </summary> 
	public class DefStdBaseRunFileConsole : PickConsole {
		#region Constructors
        /// <summary> 
        /// Instantiates the class passing flags indicating which
		/// features and classes are implemented.
        /// </summary> 
        /// <remarks>
		/// </remarks> 
        public DefStdBaseRunFileConsole(long ClassFeaturesPassed)
            : base(ClassFeaturesPassed) {
            ClassFeatures = ClassFeaturesPassed;
            DefStdBaseRunFileConsoleInitialize();
        }

        /// <summary> 
        /// Use the DefStdBaseRunFileConsole(long ClassFeaturesPassed) constructor
        /// </summary> 
        /// <remarks>
		/// </remarks> 
        public DefStdBaseRunFileConsole()
            : base() {
            DefStdBaseRunFileConsoleInitialize();
        }

        /// <summary> 
        /// The initalize routine called by constructors.
		/// This occurs after all constructors and base class
		/// instantiation but prior to events such as "Loaded".
        /// </summary> 
        /// <remarks>
		/// </remarks> 
        public void DefStdBaseRunFileConsoleInitialize() {
            Sender = this;
            SenderIsThis = this;
            // Delegates;
            TraceMdmCounterLevel1Get = TraceMdmCounterLevel1GetDefault;
            TraceMdmCounterLevel2Get = TraceMdmCounterLevel1GetDefault;
            // Item1Add
            if (ClassFeature.MdmSendIsUsed) {
                MessageMdmSendToPageNewLineSet = MessageMdmSendToPageNewLineImpl;
                MessageMdmSendToPageNewLine = MessageMdmSendToPageNewLineSetImpl;
                MessageMdmSendToPage = MessageMdmSendToPageImpl;
            }
            //
            if (ClassFeature.MdmTraceIsUsed) { TraceMdmDo = TraceMdmDoImpl; }
            //
            if (ClassFeature.LocalMessageIsUsed && LocalMessage == null) { LocalMessage = new LocalMessageDef(); }
            //
            if (ClassFeature.StatusUiIsUsed && StatusUi == null) {
                StatusUi = new StatusUiDef(
                    "DefStdBaseRunFileConsole",
                    ((ClassFeatures & (long)ClassUses.LineIsUsed) > 0),
                    ((ClassFeatures & (long)ClassUses.BoxIsUsed) > 0),
                    ((ClassFeatures & (long)ClassUses.BoxManageIsUsed) > 0),
                    ((ClassFeatures & (long)ClassUses.BoxDelegateIsUsed) > 0));
                //
                PageSizeChangedDoAdjust = null;
            }
            // MessageMdmSendToPageA = null;
            // not in use
            //public delegate void TextBoxChangeDel(Object Sender, String s);
            //public delegate void TextBoxAddDel(Object Sender, String s);
            //public delegate void ProgressCompletionDel(Object Sender, String FieldPassed, int AmountPassed, int MaxPassed);
        }
        #endregion
		#region UI objects, Status Line, Start/Pause/Cancel buttons, Page, Progress Bar
        // DefStdBaseRunFileConsole Status Lines, currently text boxes
        public StatusUiDef StatusUi;
        // Buttons
        public Button StartButtonPressed;
        public Button CancelButtonPressed;
        public Button PauseButtonPressed;
        // PageMain
        public bool PageMainInvalidateVisual;
        public bool PageMainBringIntoView;
        // ProgressBar 
        public ProgressBar ProgressBarMdm1;
        //  Delegates
        /// <summary>
        /// <para> Routine to adjust page width dynamically based
        /// on the length of current visible text box content.</para>
        /// </summary>
        public delegate void PageSizeChangedDoAdjustDel(Object Sender, TextBoxManageDef PassedTextBoxManage, TextBox PassedTextBox, double PassedDesiredWidth, double PassedDesiredHeight);
        public PageSizeChangedDoAdjustDel PageSizeChangedDoAdjust;
        #endregion
        // Console
        #region Trace Mdm Console Handling
        // CLASS MDM TRACE
        // TODO DefStdBaseRunFileConsole InProgress Class for control flags.  
        // This will be persisted.
        // not implemented yet
        /// <summary> 
        /// This class contains Trace and Console control fields.  These
        /// are only the flags and options that control which console
        /// features are uses and details such as the verbosity level.
        /// </summary> 
        public struct TraceConsoleFlags {
			#region Declarations
            long TraceControlId;
            //
            bool DoLogActivity;
            //
            bool TraceDebugOn;
            bool TraceOn;
            bool TraceFirst;
            //
            bool TraceHeadings;
            bool TraceData;
            // stop detail display after number of iterations
            bool TraceIteration;
            bool TraceDisplay;
            bool TraceBug;
            bool TraceDisplayMessageDetail;
            //
            bool ConsoleApplication;
            bool ConsoleOn;
            bool ConsoleToDisc;
            // 
            bool ConsoleTextOn;
            bool ConsoleText0On;
            bool ConsoleText1On;
            bool ConsoleText2On;
            bool ConsoleText3On;
            bool ConsoleText4On;
            bool ConsoleText5On;
            //
            int ConsoleVerbosity;
            // <Area Id = "ConsolePickConsole">
            bool ConsolePickConsoleOn;
            bool ConsolePickConsoleBasicOn;
            bool ConsolePickConsoleToDisc;
			#endregion
            //
		/// <summary>
        /// Default constructor to initalize fields.
        /// </summary> 
        /// <param name="PassedTraceControlId">A unique ID used to identify this object</param> 
        /// <remarks>
		/// </remarks> 
            public TraceConsoleFlags(long PassedTraceControlId) {
                TraceControlId = PassedTraceControlId;
                TraceData = bNO;
                TraceOn = bON;
                ConsoleOn = bOFF;
                ConsolePickConsoleOn = bON;
                ConsolePickConsoleBasicOn = bON;
                ConsoleToDisc = bOFF;
                //
                DoLogActivity = bYES;
                TraceDisplayMessageDetail = bYES;
                TraceHeadings = bNO;
                //
                TraceDebugOn = bOFF;
                TraceFirst = bYES;
                TraceHeadings = bYES;
                //
                TraceIteration = bOFF;
                TraceDisplay = bOFF;
                TraceBug = bOFF;
                TraceDisplayMessageDetail = false;
                //
                ConsoleApplication = bYES;
                ConsoleOn = bOFF;
                ConsoleToDisc = bOFF;
                // 
                ConsoleTextOn = bON;
                ConsoleText0On = bOFF;
                ConsoleText1On = bON;
                ConsoleText2On = bON;
                ConsoleText3On = bOFF;
                ConsoleText4On = bOFF;
                ConsoleText5On = bOFF;
                //
                ConsoleVerbosity = 9;
                // <Area Id = "ConsolePickConsole">
                ConsolePickConsoleOn = bOFF;
                ConsolePickConsoleBasicOn = bOFF;
                ConsolePickConsoleToDisc = bOFF;
            }
        }
		// Trace Fields:
        // Trace Increment Result
        public long TraceMdmIncrementResult;
        public long TraceMdmBasicResult;
        public long TraceMdmPointResult;
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
        public long TraceMdmDoIncrement(String PassedTraceMessage) {
            TraceMdmIncrementResult = (long)StateIs.Started;
            //
            // Process Tracing Message Increment
            //
            //if (PassedTraceMessage == "TraceIncrement") {
                iTraceIterationCount += 1;
                iTraceIterationCountTotal += 1;
                //sTraceMessage = PassedTraceMessage;
                //if (TraceData) {
                //    if (TraceOn || TraceIterationOnNow || TraceDisplayOnNow || TraceBugOnNow || ConsoleOn || RunErrorDidOccurOnce || RunErrorDidOccur) {
                //        // if (sTraceMessageBlockString.Length > 0) {
                //        sTraceMessageBlock += sTraceMessageBlockString + "|";
                //        sTraceMessageBlockString = "";
                //        // }
                //    }
                //}

                //if (sTraceMessageBlock.Length > 100) {
                //    TraceMdmDoImpl(ref Sender, bIsMessage, iNoOp, iNoOp, TraceMdmIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, PassedTraceMessage);
                //}
            //}
            return TraceMdmIncrementResult;
        }
        #region Message Dispatch Components
		#region Message processing - TraceMdmDo
		/// <summary>
        /// Passes the message on to the trace processor using default values.
        /// </summary> 
        /// <param name="PassedTraceMessage">Message to be processed or displayed</param> 
		/// <returns>
		/// </returns>
        /// <remarks>
		/// </remarks> 
        public void TraceMdmDoBasic(String PassedTraceMessage) {
            TraceMdmBasicResult = (long)StateIs.Started;
            //
            // PassedTraceMessage = PassedTraceMessage;
            TraceMdmDoImpl(ref Sender, bIsMessage, iNoOp, iNoOp, iNoMethodResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "C" + PassedTraceMessage + "\n");
            // return TraceMdmBasic;
        }
        // Current Message
        public MessageDetailsDef Message;
        // Trace Point
		/// <summary>
        /// Constructs a message object from the passed
		/// arguments and call the standard trace processor.
        /// </summary> 
        /// <param name="Sender">The object sending the message.</param> 
        /// <param name="PassedIsMessage">Flag indicating a message is being passsed.</param> 
        /// <param name="CharMaxIndexPassed">First display and tracing value or counter.</param> 
        /// <param name="MethodAttributeMaxPassed">Second display and tracing value or counter.</param> 
        /// <param name="iPassed_MethodResult">The relevant local result related to the message.</param> 
        /// <param name="PassedError">Flag indicating an error has occured.</param> 
        /// <param name="iPassedErrorLevel">The error value that has occurred.</param> 
        /// <param name="iPassedErrorSource">The source of the error.</param> 
        /// <param name="PassedDisplay">Flag indicating the message is meant to be displayed.</param> 
        /// <param name="iPassedUserEntry">A Flags construct controlling what user entry should occur.</param> 
        /// <param name="PassedTraceMessage">The message itself.</param> 
        /// <remarks>
		/// </remarks> 
        public void TraceMdmDoImpl(
            ref Object Sender,
            bool PassedIsMessage,
            int CharMaxIndexPassed,
            int MethodAttributeMaxPassed,
            long iPassed_MethodResult,
            bool PassedError,
            int iPassedErrorLevel,
            int iPassedErrorSource,
            bool PassedDisplay,
            int iPassedUserEntry,
            String PassedTraceMessage
            ) {
            TraceMdmPointResult = (long)StateIs.Started;
            // TODO TraceMdmDoImpl 11 
            // TODO TraceMdmDoImpl 11 Do analysis and implement error levels.
            // TODO TraceMdmDoImpl 11 Do analysis and implement inner exceptions.
            // TODO TraceMdmDoImpl 11 Do analysis and implement exception call stack analysis
            // TODO TraceMdmDoImpl 11 Implement tabbing for levels.
            // TODO TraceMdmDoImpl 11 Implement trim back to last word (or puncuation) present in other code.
            // TODO TraceMdmDoImpl 11 
            // TODO TraceMdmDoImpl 11 
            Message = new MessageDetailsDef();
            Message.Sender = Sender;
            Message.IsMessage = PassedIsMessage;
            Message.Location1 = CharMaxIndexPassed;
            Message.Location2 = MethodAttributeMaxPassed;
            Message.MethodResult = iPassed_MethodResult;
            Message.IsError = PassedError;
            Message.Level = iPassedErrorLevel;
            Message.Source = iPassedErrorSource;
            Message.DoDisplay = PassedDisplay;
            Message.ResponseFlags = iPassedUserEntry;
            Message.Text = PassedTraceMessage;
            TraceMdmDoImpl(Message);
        }
        // Trace Point
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
        public void TraceMdmDoImpl(MessageDetailsDef Message) {
            //
            #region Trace Mdm Top
            if (TraceFirst) {
                TraceIterationOnNow = TraceIterationInitialState;
                TraceDisplayOnNow = TraceDisplayInitialState;
                TraceBugOnNow = TraceBugInitialState;
                TraceFirst = false;
            }
            //
            // Tracing Control
            //
            sTraceMessage = Message.Text;
            sTraceMessageBlockString = sTraceMessage;
            sTraceMessagePrefix = "";
            sTraceMessageSuffix = "";
            // Display this message
            TraceDisplayMessageDetail = bOFF;
            // Display Message counting
            // iTraceDisplayCount += 1;
            iTraceDisplayCountTotal += 1;
            // Bug handling
            iTraceBugCountTotal += 1;
            //
            iLocalUserEntry = Message.ResponseId;
            sLocalUserEntry = "";
            #endregion
            #region Error
            if (Message.IsError == ErrorDidOccur) {
                //
                String sTemp4 = "";
                RunErrorDidOccurOnce = bYES;
                if (Message.ResponseId == MessageEnterResume) {
                    RunPausePending = bYES; // TODO TraceMdmDoImpl Implement puase properly
                }
                if (Message.Level > 0) { RunErrorCount += 1; }
                LocalMessage.ErrorMsg = sTraceMessage;
                // LocalMessage.ErrorMsg = sTraceMessage;
                //
                // ConsoleOn = bYES;
                // ConsoleToDisc = bYES;
                // ConsolePickConsoleOn = bYES;
                ConsolePickConsoleBasicOn = bYES;
                // ConsolePickConsoleToDisc = bYES;
                //
                TraceDisplayMessageDetail = bON;
                iLocalUserEntry = MessageEnterF5;
                sLocalUserEntry = "An unexpected error occured!";
                try {
                    sTraceMessageTarget = sTraceMessage.Substring(0, 1);
                    sTemp4 = sTraceMessage.Substring(1);
                    if (sTraceMessageTarget == "M" || sTraceMessageTarget == "A") {
                        sTraceMessageTarget = sTraceMessage.Substring(0, 2);
                        sTemp4 = sTraceMessage.Substring(2);
                    }
                } catch {
                    sTraceMessageTarget = "E";
                    sTemp4 = "";
                }
                //
                //sTraceMessage = sTraceMessageTarget + "Error" + "\n";
                //TraceMdmDoPrint(ref Sender);
                //
                sTraceErrorMessage = sTraceMessageTarget + "Error - Count(" + RunErrorCount.ToString() + "), Level(" + Message.Level.ToString() + "), Source(" + Message.Source.ToString() + ") : " + sTemp4;
                sTraceMessage = sTraceErrorMessage;
                //
            }
            #endregion
            //
            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) {
                #region Iteration Control
                if (TraceIteration) {
                    if (TraceIterationOnNow || iTraceIterationCountTotal >= iTraceIterationThreshold) {
                        // iTraceIterationCount += 1;
                        if (TraceIterationOnNow) {
                            // debut display messages is currently on
                            if (iTraceIterationCount > iTraceIterationOnForCount) {
                                // turn off display after "off" lines
                                TraceIterationOnNow = bOFF;
                                iTraceIterationCount = 1;
                                sTraceMessageSuffix += " Iteration OnFor exceeded.";
                                TraceIterationOnForWarningGiven = bNO;
                            } else if (iTraceIterationCount == iTraceIterationOnForCount) {
                                TraceDisplayMessageDetail = bON;
                                if (TraceIterationOnForWarningGiven == bNO) {
                                    sTraceMessageSuffix += " Iteration OnFor about to be exceeded.";
                                    TraceIterationOnForWarningGiven = bYES;
                                }
                            } else { TraceDisplayMessageDetail = bON; }
                        } else {
                            // debug display messages is currently off
                            if (TraceIterationRepeat && iTraceIterationCount >= iTraceIterationOnAgainCount) {
                                // turn display back on after On Again lines
                                // reached the point for another cycle of
                                // detail display iTraceIterationCount messages
                                TraceDisplayMessageDetail = bON;
                                TraceIterationOnNow = bON;
                                iTraceIterationCount = 1;
                                sTraceMessageSuffix += " Iteration OnAgain reached.";
                            } else if (iTraceIterationCountTotal < iTraceIterationThreshold + iTraceIterationOnForCount) {
                                // TraceIterationOnNow should be on when the total count
                                // is between the threshold & thresshold + off count
                                TraceDisplayMessageDetail = bON;
                                TraceIterationOnNow = bON;
                                iTraceIterationCount = 1;
                                sTraceMessageSuffix += " Iteration display activated.";
                            } // Bug On Again and Disply On threshold
                        } // Current Bug On or Off
                    } // Bug Control start point reached
                } // Bug Control turned on
                #endregion
                #region Display Message Control
                if (TraceDisplay) {
                    if (TraceDisplayOnNow || iTraceDisplayCountTotal >= iTraceDisplayThreshold) {
                        iTraceDisplayCount += 1;
                        if (TraceDisplayOnNow) {
                            // debut display messages is currently on
                            if (iTraceDisplayCount > iTraceDisplayOnForCount) {
                                // turn off display after "off" lines
                                TraceDisplayOnNow = bOFF;
                                iTraceDisplayCount = 1;
                                sTraceMessageSuffix += " Display Message OnFor exceeded.";
                            } else if (iTraceDisplayCount == iTraceDisplayOnForCount) {
                                TraceDisplayMessageDetail = bON;
                                sTraceMessageSuffix += " Display Message OnFor OnFor about to be exceeded.";
                            } else { TraceDisplayMessageDetail = bON; }
                        } else {
                            // debug display messages is currently off
                            if (TraceDisplayRepeat && iTraceDisplayCount >= iTraceDisplayOnAgainCount) {
                                // turn display back on after On Again lines
                                // reached the point for another cycle of
                                // detail display iTraceDisplayCount messages
                                TraceDisplayMessageDetail = bON;
                                TraceDisplayOnNow = bON;
                                iTraceDisplayCount = 1;
                                sTraceMessageSuffix += " Display Message OnAgain reached.";
                            } else if (iTraceDisplayCountTotal < iTraceDisplayThreshold + iTraceDisplayOnForCount) {
                                // TraceDisplayOnNow should be on when the total count
                                // is between the threshold & thresshold + off count
                                TraceDisplayMessageDetail = bON;
                                TraceDisplayOnNow = bON;
                                iTraceDisplayCount = 1;
                                sTraceMessageSuffix += " Display Message display activated.";
                            } // Bug On Again and Disply On threshold
                        } // Current Bug On or Off
                    } // Display Message Control start point reached
                } // Display Message Control turned on
                #endregion
                #region Bug Control
                if (TraceBug) {
                    if (TraceBugOnNow || iTraceBugCountTotal >= iTraceBugThreshold) {
                        iTraceBugCount += 1;
                        if (TraceBugOnNow) {
                            // debut display messages is currently on
                            if (iTraceBugCount > iTraceBugOnForCount) {
                                // turn off display after "off" lines
                                TraceBugOnNow = bOFF;
                                iTraceBugCount = 1;
                                sTraceMessageSuffix += " Bug OnFor exceeded.";
                            } else if (iTraceBugCount == iTraceBugOnForCount) {
                                TraceDisplayMessageDetail = bON;
                                sTraceMessageSuffix += " Bug OnFor about to be exceeded.";
                            } else { TraceDisplayMessageDetail = bON; }
                        } else {
                            // debug display messages is currently off
                            if (TraceBugRepeat && iTraceBugCount >= iTraceBugOnAgainCount) {
                                // turn display back on after On Again lines
                                // reached the point for another cycle of
                                // detail display iTraceBugCount messages
                                TraceDisplayMessageDetail = bON;
                                TraceBugOnNow = bON;
                                iTraceBugCount = 1;
                                sTraceMessageSuffix += " Bug OnAgain reached.";
                            } else if (iTraceBugCountTotal < iTraceBugThreshold + iTraceBugOnForCount) {
                                // TraceBugOnNow should be on when the total count
                                // is between the threshold & thresshold + off count
                                TraceDisplayMessageDetail = bON;
                                TraceBugOnNow = bON;
                                iTraceBugCount = 1;
                                sTraceMessageSuffix += " Bug display activated.";
                            } // Bug On Again and Disply On threshold
                        } // Current Bug On or Off
                    } // Bug Control start point reached
                } // Bug Control turned on
                #endregion
                #region CHECK POINTS
                // Trace Iteration counting 
                // to prompt every checkpoint messages
                // after reachiing first Threshold messages
                if (TraceIterationCheckPoint) {
                    iTraceIterationCheckPointCount += 1;
                    // prompt every iTraceIterationCountCheckPoint messages
                    if (iTraceIterationCheckPointCount >= iTraceIterationCheckPoint) {
                        sLocalUserEntry += "Iteration Checkpoint.";
                        iLocalUserEntry = MessageEnterF5;
                        iTraceIterationCheckPointCount = 0;
                    }
                }
                // Trace Display Messages counting 
                // to prompt every checkpoint messages
                // after reachiing first Threshold messages
                if (TraceDisplayCheckPoint) {
                    iTraceDisplayCheckPointCount += 1;
                    // prompt every iTraceDisplayCountCheckPoint messages
                    if (iTraceDisplayCheckPointCount >= iTraceDisplayCheckPoint) {
                        sLocalUserEntry += "Display Message Checkpoint.";
                        iLocalUserEntry = MessageEnterF5;
                        iTraceDisplayCheckPointCount = 0;
                    }
                }
                // Trace Bug Messages counting 
                // to prompt every checkpoint messages
                // after reachiing first Threshold messages
                if (TraceBugCheckPoint) {
                    iTraceBugCheckPointCount += 1;
                    // prompt every iTraceBugCountCheckPoint messages
                    if (iTraceBugCount >= iTraceBugCheckPoint) {
                        sLocalUserEntry += "Bug Checkpoint.";
                        iLocalUserEntry = MessageEnterF5;
                        iTraceBugCount = 0;
                    }
                }
                #endregion
            }
            //
            #region Prepare Margin Data
            if (TraceData) {
                if (TraceOn || TraceIterationOnNow || TraceDisplayOnNow || TraceBugOnNow || TraceDisplayMessageDetail) {
                    // Prepare Message
                    // Margin Data, counts
                    // iTraceMesageCount
                    sTraceDataPointers = "m[" + PickOconv(iTraceDisplayCountTotal, "r06") + "] ";
                    // iTraceIterationCount, iTraceIterationCountMax
                    sTraceDataPointers += "a[" + PickOconv(iTraceIterationCountTotal, "r06") + "." + PickOconv(TraceMdmCounterLevel1GetDefault(), "r06") + "] ";
                    // iTraceCharacterCount, iTraceCharacterCountMax
                    sTraceDataPointers += "c[" + PickOconv(iTraceCharacterCount, "r06") + "." + PickOconv(TraceMdmCounterLevel2GetDefault(), "r06") + "]";
                } else { sTraceDataPointers = ""; }
            } else { sTraceDataPointers = ""; }
            #endregion
            //
            #region Display Block Output Data
            //
            if (ConsoleOn || ConsolePickConsoleOn || ConsoleTextOn) { TraceDisplayMessageDetail = bON; }
            //
            if (TraceDisplayMessageDetail || ConsolePickConsoleBasicOn || TraceOn || TraceIterationOnNow || TraceDisplayOnNow || TraceBugOnNow) {
                if (Message.IsMessage) {
                    TraceMdmDoPrint(ref Sender);
                } else {
                    sTraceMessageBlockString = sTraceMessage;
                    sTraceMessageBlock += sTraceMessageBlockString;
                    if (TraceData) { sTraceMessageBlock += "|"; }
                    //
                    int Tml = sTraceMessageBlock.Length;
                    while (Tml > 80 || (!TraceData && Tml > 0)) {
                        if (Tml > 80) { Tml = 80; }
                        if (TraceData) {
                            sTraceMessage = ("C" + " [" + sTraceMessageBlock.Substring(0, Tml) + "]" + "\n");
                        } else {
                            sTraceMessage = "C" + sTraceMessageBlock.Substring(0, Tml) + "\n";
                        }
                        TraceMdmDoPrint(ref Sender);
                        if (sTraceMessageBlock.Length > 80) {
                            sTraceMessageBlock = sTraceMessageBlock.Substring(80);
                        } else {
                            sTraceMessageBlock = "";
                        }
                        Tml = sTraceMessageBlock.Length;
                    }
                }
            }
            #endregion
            //
            #region User Entry, Get any user responses
            if (Message.ResponseId == MessageEnterF5 || iLocalUserEntry == MessageEnterF5) {
                if (true == false) {
                    // F5 to continue prompt
                    // TODO z$RelVs2 TraceMdmDoImpl this will change, it is not a user prompt...
                    if (TraceDebugOn) {
                        sTemp1 = " Press F5 to continue...";
                    } else {
                        sTemp1 = " Press any key to continue...";
                    }
                    sLocalUserEntry += sTemp1;
                }
                // TODO sLocalUserEntry TraceMdmDoPrint(sLocalUserEntry)
                // TOTO sLocalUserEntry INPUT RESPONSE HERE
            } else {sLocalUserEntry = "";}

            #region Error
            //if (PassedError == ErrorDidOccur) {
            //    sTraceMessage = sTraceMessageTarget + "Error" + "\n";
            //    TraceMdmDoPrint(ref Sender);
            //}
            #endregion
            RunPauseCheck();

            #endregion
            //
            Message.ResponseId = 0;
            iLocalUserEntry = 0;
            sTraceMessage = "";
            sTraceMessageSuffix = "";
            sTraceMessagePrefix = "";
            //
            //
            // return TraceMdmPoint;
        }

		/// <summary>
        /// Called by TraceMdmDo to preserve margin text for multiline messages
        /// </summary> 
        /// <param name="Sender"></param> 
        /// <param name="PassedTraceMessage">Multiline message to process</param> 
        /// <param name="PassedTracePrefix">Text Prefix for message lines</param> 
        /// <remarks>
		/// </remarks> 
		public void TraceMdmDoPrintMultiLine(ref Object Sender, String PassedTraceMessage, String PassedTracePrefix) {
            int Tml = PassedTraceMessage.Length;
            while (Tml > 0) {
                if (Tml > 100) { 
                    Tml = 80; 
                    sTraceMessageToPrint = PassedTracePrefix + PassedTraceMessage.Substring(0, Tml) + " +++" + "\n";
                    PassedTraceMessage = PassedTraceMessage.Substring(80);
                } else {
                    sTraceMessageToPrint = PassedTracePrefix + PassedTraceMessage;
                    PassedTraceMessage = "";
                }
                TraceMdmDoCall();
                Tml = PassedTraceMessage.Length;
            }
        }
        
		/// <summary>
        /// Called by TraceMdmDo to process messages
        /// </summary> 
		public void TraceMdmDoPrint(ref Object Sender) {
            String sTemp = "";
            String sTemp1 = "";
            try {
                if (sTraceMessage.Length == 2) {
                    LocalMessage.Msg0 = "Length 2";
                } else if (sTraceMessage.Length == 1) {
                    LocalMessage.Msg0 = "Length 1";
                } else if (sTraceMessage.Length < 5) {
                    LocalMessage.Msg0 = "Length < 5";
                }
                sTraceMessageTarget = sTraceMessage.Substring(0, 1); 
                sTemp = sTraceMessage.Substring(1);
                if (sTraceMessageTarget == "M" || sTraceMessageTarget == "A") {
                    sTraceMessageTarget = sTraceMessage.Substring(0, 2);
                    sTemp = sTraceMessage.Substring(2);
                }
            } catch { 
                sTraceMessageTarget = "C"; 
            }
            //
            sTraceMessageFormated = sTraceMessageTarget;
            if (TraceData) { sTraceMessageFormated += sTraceDataPointers; }
            if (TraceHeadings) {
                if (sProcessHeading.Length > 0) {
                    sTraceMessageFormated += sProcessHeading + ", " + sProcessSubHeading + ": ";
                } else {
                    sTraceMessageFormated += Sender.ToString() + ": ";
                }
            }
            //
            sTemp1 = sTraceMessagePrefix + sTemp + sTraceMessageSuffix;
            if (sTemp1.Length > 160) {
                TraceMdmDoPrintMultiLine(ref Sender, sTemp1, sTraceMessageFormated);
            } else {
                sTraceMessageToPrint = sTraceMessageFormated + sTemp1;
                TraceMdmDoCall();
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
		public void TraceMdmDoCall() {
            // Display Message
            // Three possible output
            // Debug
            // PickConsole or Trace
            // Regular Console STD IO
            if (TraceDebugOn) {
                System.Diagnostics.Debug.WriteLine(sTraceMessageToPrint);
            }
            if (ConsolePickConsoleOn || TraceOn) {
                // ConsoleMdmPickDisplayImpl("C" + sTemp0 + "\n");
                // Console Display or Print or Trace us Thread Here
                ConsoleMdmPickDisplayImpl(sTraceMessageToPrint);
            }
            if (ConsoleOn) {
                TraceMdmPointResult = ConsoleMdmStd_IoWriteImpl(sTraceMessageToPrint);
            }
            // User Promptin and Entry
            // Done in TraceMdmDoImpl
            // sTraceMessageFormated += sLocalUserEntry;
        }
       #endregion
	   #region Pause Processing
		/// <summary>
        /// This check can be called from anywhere within update
		/// processing (across thread boundaries) to determine
		/// if the user interface or other system components have
		/// requested a pause in processing.
        /// </summary> 
        public void RunPauseCheck() {
            int DotCount;
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
                ) {
                RunPausePending = bOFF;
                LocalMessage.Msg = "Process paused, waiting form resume...";
                // PrintOutputMdm_PickPrint(Sender, 1, "A1" + LocalMessage.Msg0, bYES);
                RunAction = RunPause;
                RunMetric = RunState;
                RunTense = RunTense_Did;
                RunActionState[RunPause, RunState] = RunTense_Did;
                RunActionProgressChangeEventArgs = new ProgressChangedEventArgs((int)LocalProgressBar_Value,
                "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString() + LocalMessage.Msg);
                ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs);
                //
                iAppActionWaitMilliIncrement = 500;
                iAppActionWaitMilliIncrementMax = 3600000;
                bAppActionWaitContinue = true;
                iAppActionWaitCounter = 0;
                DotCount = 0;
                //
                while (bAppActionWaitContinue
                    && RunActionState[RunPause, RunState] != RunTense_Off
                    && RunActionState[RunPause, RunState] != RunTense_Done
                    ) {
                    DotCount = 0;
                    iAppActionWaitCounter += 1;
                    // every 0.5 seconds
                    System.Threading.Thread.Sleep(iAppActionWaitMilliIncrement);
                    // cancel run after max
                    if (iAppActionWaitCounter > iAppActionWaitMilliIncrementMax) {
                        RunCancelPending = bYES;
                        bAppActionWaitContinue = bNO;
                    }
                    // exit if external cancel occured
                    if (RunCancelPending
                        || RunActionState[RunPause, RunState] == RunTense_Off
                        || RunActionState[RunPause, RunState] == RunTense_Done
                        ) {
                        bAppActionWaitContinue = false;
                    }
                    // every 5 seconds
                    iTemp1 = Math.DivRem(iAppActionWaitCounter, 10, out iTemp);
                    if (iTemp == 0) {
                        //RunActionProgressChangeEventArgs = new ProgressChangedEventArgs((int)LocalProgressBar_Value, 
                        //    "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString()
                        //    + ".");
                        if (DotCount >= 50) {
                            // new line after 50 dots
                            PrintOutputMdm_PickPrint(Sender, 1, "A1" + ".", bYES);
                            DotCount = 1;
                        } else {
                            DotCount += 1;
                            PrintOutputMdm_PickPrint(Sender, 1, "A1" + ".", bNO);
                        }
                        // ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs);
                    }
                }
                if (DotCount > 0) {
                    // new line if there are any dots
                    //RunActionProgressChangeEventArgs = new ProgressChangedEventArgs((int)LocalProgressBar_Value,
                    //    "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString()
                    //    + ".");
                    PrintOutputMdm_PickPrint(Sender, 1, "A1" + ".", bYES);
                    // ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs);
                }
                LocalMessage.Msg = "Process resuming.";
                //if (RunActionState[RunPause, RunState] == RunTense_Done) {
                RunAction = RunPause;
                RunMetric = RunState;
                RunTense = RunTense_Off;
                RunActionState[RunPause, RunState] = RunTense_Off;
                RunActionProgressChangeEventArgs = new ProgressChangedEventArgs((int)LocalProgressBar_Value,
                "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString() + LocalMessage.Msg);
                ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs);
                //}
                StatusUi.Box2Manage.ScrollDo = true;
            }
        }
        #endregion
        #endregion
        #region Standard Objects
        public bool XUomUrvvXvCreateNow;
        public bool XUomClvvXvCreateNow;
        // Text Stream IOException
        #region Mdm Standard Io Objects - Streams, readers and writers
        // <Area Id = "ConsoleObject">
        public TextWriter ocotConsoleWriter;
        // public TextWriter ocotStandardOutput;
        public TextReader ocitConsolRoutedEventder;
        public StreamWriter ocoConsoleWriter;
        // public StreamWriter ocoStandardOutput;
        public StreamReader ociConsolRoutedEventder;
        // public StreamWriter ocetErrorWriter;
        public IOException eIoe;
        public TextWriter ocetErrorWriter;
        //
        #endregion
        #endregion
        #region Console Mdm Pick and Std_IO
        // PrintOutputMdm_PickPrint NewLine *** does actual output
        // Goes to :
        // TraceOn:
        // TraceMdmDoImpl
        // ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn:
        // ConsoleMdmPickDisplayImpl
        // or to:
        // ThreadUiProgressAsync
        #region Console Mdm Pick Console Initialization
		/// <summary>
        /// Standard initialization for the console and Pick console
        /// </summary> 
        public virtual void ConsoleMdmInitialize() {
            // if (!ClassFeature.MdmIsUsed) { return; }
            if (!ClassFeature.MdmConsoleIsUsed) { return; }
            // delegates can not be set here...
            // Set these in the application controller
            // override that calls this base class.
            #region Control Flags
            // <Area Id = "ConsoleBasicConsole">
            ConsoleOn = bOFF;
            ConsoleToDisc = bOFF;
            ConsoleVerbosity = 1;
            // <Area Id = "ConsolePickConsole">
            ConsolePickConsoleOn = bOFF;
            ConsolePickConsoleBasicOn = bON;
            ConsolePickConsoleToDisc = bOFF;
            #endregion
            #region Console Basic Output
            ConsoleOutput = "";
            ConsoleOutputLog = "";
            // Display
            ConsolePickConsoleOutput = "";
            ConsolePickConsoleOutputLog = "";
            #endregion
            // <Area Id = "TextConsole>
            CommandLineRequest = "";
            CommandLineRequestResult = 0;
            TextConsole = ""; // Basic
            ConsolePickTextConsole = ""; // Pick
            // <Area Id = "ConsoleResponse">
            // <Area Id = "Pick Console">
            #region Console Text Block - Pick Text areas 1-4
            ConsolePickConsoleTextBlock = ""; // text block
            ConsolePickConsoleTextPositionX = 0;
            ConsolePickConsoleTextPositionY = 0;
            ConsolePickConsoleTextPositionZ = 0;
            #region ConsoleText4 Documentation
            // ConsolePickConsoleTextPositionOrigin;
            // TODO z$RelVs2 ConsoleMdmInitialize  Text Areas 0 - 5
            // TODO z$RelVs2 ConsoleMdmInitialize  Text Area 0 - TODO z$RelVs2 ConsoleMdmInitialize 
            // TODO z$RelVs2 ConsoleMdmInitialize  Text Area 1 - Summary Progress, Messages, Errors and Help, ToolTip
            // TODO z$RelVs2 ConsoleMdmInitialize  Text Area 2 - Detailed Progress
            // TODO z$RelVs2 ConsoleMdmInitialize  Text Area 3 - Help - What is it
            // TODO z$RelVs2 ConsoleMdmInitialize  Text Area 4 - Help - How do I do this
            // TODO z$RelVs2 ConsoleMdmInitialize  Text Area 5 - Help and Status - Procedure and Event Sequence
            #endregion
            #region ConsoleText4 Flags
            ConsoleTextOn = bOFF;
            ConsoleText0On = bOFF;
            ConsoleText1On = bON;
            ConsoleText2On = bON;
            ConsoleText3On = bOFF;
            ConsoleText4On = bOFF;
            ConsoleText5On = bOFF;
            #endregion
            #region ConsoleText4 Message Text
            sMessageText = "";
            sMessageText0 = "";
            MessageTextOutConsole = "";
            MessageTextOutStatusLine = "";
            MessageTextOutProgress = "";
            MessageTextOutError = "";
            MessageTextOutRunAction = "";
            #endregion
            #region ConsoleText4 Control
            MessageStatusAction = "";
            ProcessStatusAction = "";
            //
            MessageStatusTargetText = "";
            ProcessStatusTargetDouble = 0;
            ProcessStatusTarget = 0;
            //
            MessageStatusSubTarget = 0;
            MessageStatusSubTargetDouble = 0;
            ProcessStatusSubTarget = 0;
            //
            ProcessStatusTargetState = 0;
            #endregion
            #region ConsoleText4 Presentation
            ProgressBarMdm1Property = 0;
            MessageProperty2 = 0;
            #endregion
            #endregion
        }
        #endregion
        #region Console Mdm Pick Console
		/// <summary>
        /// Set the messsage verbosity level for the console
        /// </summary> 
        /// <param name="PassedConsoleVerbosity">Verbosity level to use</param> 
        public void ConsoleVerbositySet(int PassedConsoleVerbosity) {
            ConsoleVerbosity = PassedConsoleVerbosity;
        }
        // ConsoleMdmPickOpenImpl();
		/// <summary>
        /// Opens the OS Console
        /// </summary> 
        public void ConsoleMdmPickOpenImpl() {
            // ConsolePickTextConsole;
            // ConsolePickConsoleOutput;
            if (!ConsolePickConsoleOn && !DoLogActivity) { return; }
            if (DoLogActivity) { ConsolePickConsoleToDisc = bON; };
            ConsolePickConsoleTextBlock = "";
            ConsolePickConsoleTextPositionX = 0;
            ConsolePickConsoleTextPositionY = 0;
            ConsolePickConsoleTextPositionZ = 0;
            ConsolePickConsoleTextPositionOrigin = new Point(ConsolePickConsoleTextPositionX, ConsolePickConsoleTextPositionY);
            // TODO z$RelVs2 ConsoleMdmPickOpenImpl This Visual is not connected to a PresentationSource
            // TODO z$RelVs2 ConsoleMdmPickOpenImpl ConsolePickConsoleTextBlock.PointToScreen(ConsolePickConsoleTextPositionOrigin);
            // TODO z$RelVs2 ConsoleMdmPickOpenImpl XUomCovvXv.XUomPmvvXv.InvalidateArrange();
            ConsolePickConsoleTextBlock = "Pick Console Open...";
            // ConsolePickConsoleOutputLog = "";
            // ConsolePickConsoleOn = bON;
            // ConsolePickConsoleToDisc = bON;
        }
        // ConsoleMdmPickOpenImpl();
		/// <summary>
        /// Closes the OS Console
        /// </summary> 
        public void ConsoleMdmPickCloseImpl() {
            // ConsolePickTextConsole;
            // ConsolePickConsoleOutput;
            ConsolePickConsoleTextBlock = "Pick Console Close...";
            // ConsolePickConsoleOutputLog = "";
            // ConsolePickConsoleOn = bON;
            // ConsolePickConsoleToDisc = bON;
        }
        // ConsoleMdmPickDisplayImpl
        public void ConsoleMdmPickDisplayImpl(String PassedLine) {
            PickConsoleDisplayResult = (long)StateIs.Started;
            // ConsolePickConsoleTextBlock = PassedLine + ConsolePickConsoleTextBlock;
            // MdmConsole
            ConsoleOutput = PassedLine;
            if (ThreadUiTextMessageAsync != null) {
                ThreadUiTextMessageAsync(ref Sender, ConsoleOutput);
            } else {
                ExceptDelegate(
                    "ThreadUiTextMessageAsync delegate is not set!",
                    (long)StateIs.DoesNotExist);
            }
        }
        #endregion
        #region Console Mdm Std_Io
        // Console
        public long ConsoleOpenResult;
        public long ConsoleCloseResult;
        public long ConsolRoutedEventdResult;
        public long ConsoleWriteResult;
		/// <summary>
        /// Opens the STD Console
        /// <param name=""></param> 
        public long ConsoleMdmStd_IoOpenImpl() {
            ConsoleOpenResult = (long)StateIs.Started;
            //
            try {
                if (!ConsoleOn && !DoLogActivity) { return ConsoleOpenResult; }
                if (DoLogActivity) { ConsoleToDisc = bON; };
                //
                // Text Version
                // ocoConsoleWriter = Console.Out;
                // ociConsolRoutedEventder = Console.In;
                // Stream Version
                // ocoConsoleWriter = new StreamWriter("C:\\Users\\Public\\Desktop\\$ ToDo Dgh MdmSrt Project\\Mdm\\Mdm1\\MinputTldConsoleOut.txt");
                // ocoConsoleWriter.AutoFlush = bON;
                //
                //
                try {
                    if (!ConsoleToDisc && ConsoleApplication) {
                        try {
                            ocotConsoleWriter = Console.Out;
                            // ocotConsoleWriter = new TextWriter(Console.OpenStandardOutput());
                            ocitConsolRoutedEventder = Console.In;
                            // ocitConsolRoutedEventder = new StreamReader(Console.OpenStandardInput());
                            Console.Title = sProcessHeading;
                            Console.SetWindowSize(400, 200);
                            Console.SetWindowPosition(100, 100);
                        } catch {
                            ConsoleToDisc = bYES;
                            ConsoleApplication = bNO;
                        }
                        //
                    }
                    // TODO ConsoleMdmStd_IoOpenImpl Folder and File Creation
                    if (ConsoleToDisc) {
                        try {
                            ocoConsoleWriter = new StreamWriter("C:\\Logs\\Console\\MinputTldConsoleOut.txt");
                            Console.SetOut(ocoConsoleWriter);
                        } catch {
                            ocotConsoleWriter = Console.Out;
                        }
                        try {
                            ociConsolRoutedEventder = new StreamReader("C:\\Logs\\Console\\MinputTldConsoleIn.txt");
                            Console.SetIn(ociConsolRoutedEventder);
                        } catch {
                            ocitConsolRoutedEventder = Console.In;
                        }
                    }
                } catch {
                    //ConsoleToDisc = bYES;
                    //ConsoleApplication = bNO;
                }
                //
                if (!ConsoleToDisc) { Console.Beep(); }
                LocalMessage.Msg8 = "Finished opening Console...";
                Console.WriteLine(LocalMessage.Msg8);
                if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { TraceMdmDoImpl(ref Sender, bIsMessage, iNoOp, iNoOp, ConsoleOpenResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "C" + LocalMessage.Msg8 + "\n"); }

                if (!ConsoleToDisc) {
                    LocalMessage.Msg8 = "Press any key when ready... ";
                    Console.WriteLine(LocalMessage.Msg8);
                    if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { TraceMdmDoImpl(ref Sender, bIsMessage, iNoOp, iNoOp, ConsoleOpenResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageEnterAnyKey, "C" + LocalMessage.Msg8 + "\n"); }
                    // sTemp1 = Console.ReadLine();
                    ConsoleKeyInfo ckiTemp1 = Console.ReadKey();
                }
                //
                // ocoStandardOutput = new StreamWriter(Console.OpenStandardOutput());
                // ocoConsoleWriter = new StreamWriter(Console.OpenStandardOutput());
                // ocoConsoleWriter = new StreamWriter("C:\\Users\\Public\\Desktop\\$ ToDo Dgh MdmSrt Project\\Mdm\\Mdm1\\MinputTldConsoleOut.txt");
                // Console.OpenStandardOutput(5000);
                //
                // ocoStandardOutput.AutoFlush = bON;
                // Console.SetOut(ocoStandardOutput);
            } catch (IOException eIoe) {
                ConsoleOpenResult = (long)StateIs.ShouldNotExist;
                ConsoleOn = bOFF;
                ocetErrorWriter = Console.Error;
                LocalMessage.Msg9 = "Unhandled Error during Console Open.";
                ocetErrorWriter.WriteLine(LocalMessage.Msg9);
                TraceMdmDoImpl(ref Sender, bIsMessage, iNoOp, iNoOp, ConsoleOpenResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "C" + LocalMessage.Msg9 + "\n");
                ocetErrorWriter.WriteLine(eIoe.Message);
            }

            return ConsoleOpenResult;
        }
		/// <summary>
        /// Get input from the STD Console
        /// </summary> 
        public String ConsoleMdmStd_IoReadImpl(int iPassedLineRequestCount) {
            ConsolRoutedEventdResult = (long)StateIs.Started;
            if (!ConsoleOn) { return ""; }
            if (ConsoleToDisc) { return ""; }
            //
            sTemp3 = "";
            iTemp3 = 0;
            sTemp4 = "";
            // iTemp3 = 99999;
            try {
                if (ociConsolRoutedEventder == null) { ConsolRoutedEventdResult = ConsoleMdmStd_IoOpenImpl(); }
                while (iTemp3 < iPassedLineRequestCount && sTemp3.Length > 0) {
                    sTemp3 = Console.ReadLine();
                    if (sTemp3.Length > 0) {
                        sTemp4 += sTemp3 + CrLf;
                        iTemp3 += 1;
                    }
                }
            } catch (IOException eIoe) {
                ConsolRoutedEventdResult = (long)StateIs.Failed;
                ConsoleOn = bOFF;
                ocetErrorWriter = Console.Error;
                LocalMessage.Msg9 = "Import Tld Vs0_1 - Console Read Error - Line: " + iTemp3.ToString() + " - ";
                ocetErrorWriter.WriteLine(LocalMessage.Msg9);
                TraceMdmDoImpl(ref Sender, bIsMessage, iNoOp, iNoOp, ConsolRoutedEventdResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "C" + LocalMessage.Msg9 + "\n");
                ocetErrorWriter.WriteLine(eIoe.Message);
            }
            return sTemp4;
        }
		/// <summary>
        /// Sent Output to the STD Console
        /// </summary> 
        public long ConsoleMdmStd_IoWriteImpl(String PassedLine) {
            ConsoleWriteResult = (long)StateIs.Started;
            if (!ConsoleOn) {
                LocalMessage.Msg8 = PassedLine;
                System.Windows.MessageBox.Show(PassedLine, (String)"MinputTld", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.None);
                return ConsoleWriteResult;
            }
            if (ConsoleToDisc) { return ConsoleWriteResult; }
            //
            try {
                if (ocoConsoleWriter == null) { ConsoleWriteResult = ConsoleMdmStd_IoOpenImpl(); }
                Console.Write(PassedLine);
            } catch (IOException eIoe) {
                ConsoleWriteResult = (long)StateIs.Failed;
                ocetErrorWriter = Console.Error;
                LocalMessage.Msg9 = "Import Tld Vs0_1 - Console Write Error";
                ocetErrorWriter.WriteLine(LocalMessage.Msg9);
                TraceMdmDoImpl(ref Sender, bIsMessage, iNoOp, iNoOp, ConsoleWriteResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "C" + LocalMessage.Msg9 + "\n");
                ocetErrorWriter.WriteLine(LocalMessage.Msg9);
                ocetErrorWriter.WriteLine(eIoe.Message);
            }
            return ConsoleWriteResult;
        }
		/// <summary>
        /// Close the STD Console
        /// </summary> 
        public long ConsoleMdmStd_IoCloseImpl() {
            ConsoleCloseResult = (long)StateIs.Started;
            //
            if (!ConsoleOn) { return ConsoleCloseResult; }
            if (ConsoleToDisc) { return ConsoleCloseResult; }
            //
            try {
                ocoConsoleWriter.Close();
                ociConsolRoutedEventder.Close();
            } catch (IOException eIoe) {
                ConsoleCloseResult = (long)StateIs.Failed;
                ocetErrorWriter = Console.Error;
                LocalMessage.Msg9 = "Import Tld Vs0_1 - Console Close Failed";
                ocetErrorWriter.WriteLine(LocalMessage.Msg9);
                TraceMdmDoImpl(ref Sender, bIsMessage, iNoOp, iNoOp, ConsoleCloseResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "C" + LocalMessage.Msg9 + "\n");
                ocetErrorWriter.WriteLine(LocalMessage.Msg9);
                ocetErrorWriter.WriteLine(eIoe.Message);
            }
            return ConsoleCloseResult;
        }
        #endregion
        #endregion
        #region Print Output Mdm Pick Print
        #region Print Output Mdm Pick Print Target Validate (Output Marshalling)
		/// <summary>
		/// Validate Target processing the message prefix to determine
		/// the type of message and route it to the appropriate user interface
		/// element.
		/// Notes and history:
		/// These routines combine status line (or box) handling along with
		/// console display.
        ///
        /// Validate Output Marshalling:
        /// Target:
        /// To Console:
        /// C - 
        /// example CHello there
        /// To Text Box or status line (currently not separate):
        /// A -
        /// M - 
        /// example A1Hello upper box
        /// example M2Hello lower box
        /// Target Box
        /// TextBox 1 - 4
        /// </summary> 
        /// <param name="PassedText">Text to be displayed</param> 
        /// <remarks>
		/// </remarks> 
        public String PrintOutputMdm_PickPrint_TargetValidate(String PassedText) {
            sPickPrintTargetText = PassedText;
            sPickPrintTarget = "A";
            PickPrintTargetBox = 2;
            if (PassedText.Length > 1) {
                sPickPrintTarget = PassedText.Substring(0, 1);
                if (sPickPrintTarget == "C") {
                    sPickPrintTargetText = PassedText.Substring(1);
                } else if (sPickPrintTarget == "M" || sPickPrintTarget == "A") {
                    try {
                        PickPrintTargetBox = Convert.ToInt32(PassedText.Substring(1, 1));
                        if (PickPrintTargetBox > 4 || PickPrintTargetBox < 0) {
                            PickPrintTargetBox = 2;
                        } else { sPickPrintTargetText = PassedText.Substring(2); }
                    } catch { ; }
                }
            } else if (PassedText.Length > 0) {
                sPickPrintTarget = PassedText.Substring(0, 1);
                if (sPickPrintTarget == "C") {
                    sPickPrintTargetText = PassedText.Substring(1);
                }
            }
            if (sPickPrintTarget != "C") {
                sPickPrintTargetString = sPickPrintTarget + PickPrintTargetBox.ToString();
            } else {
                sPickPrintTargetString = sPickPrintTarget;
            }
            sTemp3 = sPickPrintTargetString + sPickPrintTargetText;
            return sTemp3;
        }
        #endregion
        #region Print Output Mdm Pick Print
		/// <summary>
        /// Does actual output for PrintOutputMdm_PickPrint
        /// Currently PickPrint reroutes output to ConsoleMdmPickDisplayImpl
		/// after analysis by PrintOutputMdm_PickPrint_TargetValidate
        /// </summary> 
        /// <param name="Sender">Object sending this output</param> 
        /// <param name="iMessageLevel">Verbosity level of message</param> 
        /// <param name="PassedText">Text to be displayed</param> 
        /// <param name="PassedNewLineFlag">Position cursor on new line after output</param> 
        public void PrintOutputMdm_PickPrint(Object Sender, int iMessageLevel, bool PassedNewLineFlag, String PassedText) {
            PickPrintNewLineOnlyResult = (long)StateIs.Started;
            // all new line characters route through here
            if (PassedNewLineFlag) {
                PassedText += "\n";
            }
            ConsoleOutput = PrintOutputMdm_PickPrint_TargetValidate(PassedText);
            // if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) {
            //    if (TraceOn) {
            //        TraceMdmDoImpl(ref Sender, bIsMessage, TraceMdmCounterLevel1Get(), TraceMdmCounterLevel1Get(), -3, ErrorDidNotOccur, iNoOp, iNoOp, bDoDisplay, MessageNoUserEntry, ConsoleOutput);
            //    } else {
                    ConsoleMdmPickDisplayImpl(ConsoleOutput);
            //    }
            // } else {
            //    if (iMessageLevel <= ConsoleVerbosity) {
            //        // XUomMavvXv.XUomVtvvXv.ThreadUiProgressVtImpl;
            //       ThreadUiTextMessageAsync(ref Sender, ConsoleOutput);
            //    }
            // }
            iMessageLevelLast = iMessageLevel;
            // return PrintOutputMdm_PickPrint;
        }
		/// <summary>
        /// PrintOutputMdm_PickPrint Text and Newline (alternate order of parameters)
        /// </summary> 
        /// <param name="Sender">Object sending this output</param> 
        /// <param name="iMessageLevel">Verbosity level of message</param> 
        /// <param name="PassedText">Text to be displayed</param> 
        /// <param name="PassedNewLineFlag">Position cursor on new line after output</param> 
        public void PrintOutputMdm_PickPrint(Object Sender, int iMessageLevel, String PassedText, bool PassedNewLineFlag) {
            PickPrintNewLineResult = (long)StateIs.Started;
            PrintOutputMdm_PickPrint(Sender, iMessageLevel, PassedNewLineFlag, PassedText);
        }
		/// <summary>
        /// PrintOutputMdm_PickPrint Text 
        /// </summary> 
        /// <param name="Sender"></param> 
        /// <param name="iMessageLevel"></param> 
        /// <param name="PassedText"></param> 
        public void PrintOutputMdm_PickPrint(Object Sender, int iMessageLevel, String PassedText) {
            PickPrintResult = (long)StateIs.Started;
            PrintOutputMdm_PickPrint(Sender, iMessageLevel, bNO, PassedText);
        }
        #endregion
        #region Print Output Mdm Pick Position
		/// <summary>
        /// PrintOutputMdm_PickPositionImpl
        /// Position Column and Row with newline and text
        /// </summary> 
        /// <param name="Sender">Object sending this output</param> 
        /// <param name="iMessageLevel">Verbosity level of message</param> 
        /// <param name="PassedLine">Text to be displayed</param> 
        /// <param name="PassedNewLineFlag">Position cursor on new line after output</param> 
        /// <param name="iPassedPromptColumn">Relative display column to start output at</param> 
        /// <param name="iPassedPromptRow">Releative display row to start at</param> 
        /// <remarks>
		/// </remarks> 
        public void PrintOutputMdm_PickPositionImpl(int iMessageLevel, String PassedLine, bool PassedNewLineFlag, int iPassedPromptColumn, int iPassedPromptRow) {
            PickConsoleDisplayResult = (long)StateIs.Started;
            // TODO ConsolePickConsoleTextPositionX = iPassedPromptColumn * (int)(XUomCovvXv.XUomPmvvXv.ActualWidth / 80);
            // TODO ConsolePickConsoleTextPositionY = iPassedPromptRow * (int)(XUomCovvXv.XUomPmvvXv.ActualHeight / 23);
            // TODO ConsolePickConsoleTextPositionZ = 0;
            // TODO ConsolePickConsoleTextBlock.
            // TODO ConsolePickConsoleText System.Windows.Controls.TextBox.EndPosition
            ConsoleOutput = "[" + iPassedPromptColumn.ToString() + ", " + iPassedPromptRow.ToString() + "]";
            ConsoleOutput += PassedLine;
            PrintOutputMdm_PickPrint(Sender, iMessageLevel, PassedNewLineFlag, ConsoleOutput);
        }
        /// <summary> 
        /// Position Column and Row with newline and text (alternate order of parameters)
        /// </summary> 
        public void PrintOutputMdm_PickPositionImpl(int iMessageLevel, bool PassedNewLineFlag, int iPassedPromptColumn, int iPassedPromptRow, String PassedLine) {
            PrintOutputMdm_PickPositionImpl(iMessageLevel, PassedLine, PassedNewLineFlag, iPassedPromptColumn, iPassedPromptRow);
        }
        /// <summary> 
        /// Position Column and Row with newline
        /// </summary> 
        public void PrintOutputMdm_PickPositionImpl(int iMessageLevel, bool PassedNewLineFlag, int iPassedPromptColumn, int iPassedPromptRow) {
            PickPositionAtRowResult = (long)StateIs.Started;
            ConsoleOutput = "[" + iPassedPromptColumn.ToString() + ", " + iPassedPromptRow.ToString() + "]";
            PrintOutputMdm_PickPrint(Sender, iMessageLevel, PassedNewLineFlag, ConsoleOutput);
            // PickPositionAtRow
        }
        /// <summary> 
        /// Postion Column with newline
        /// </summary> 
        public void PrintOutputMdm_PickPositionImpl(int iMessageLevel, bool PassedNewLineFlag, int iPassedPromptColumn) {
            PickPositionAtColumnResult = (long)StateIs.Started;
            ConsoleOutput = "[" + iPassedPromptColumn.ToString() + "]";
            PrintOutputMdm_PickPrint(Sender, iMessageLevel, PassedNewLineFlag, ConsoleOutput);
            // PickPositionAtColumn
        }
        #endregion
        #endregion
        // Messaging
        #region Message Mdd To Page - MessageMdmSendToPage
        #region Message Mdd To Page Delegates
        // <Section Id = "private long Database Close
        // <Section Id = "private int Stream Close
        // <Section Id = "private int Binary Close
        // file level:
        /// <remarks>
		/// Event based messaging...
		/// current not implemented
		/// </remarks> 
        public delegate void MessageMdmSendToPageAEventHandler(Object Sender, MessageMdmSendToPageArgs e);
        public event MessageMdmSendToPageAEventHandler MessageMdmSendToPageA;
        public virtual void MessageMdmSendToPageOnA(MessageMdmSendToPageArgs e) {
            MessageMdmSendToPageAEventHandler MessageMdmSendToPageAHandler = MessageMdmSendToPageA;
            if (MessageMdmSendToPageAHandler != null) {
                MessageMdmSendToPageAHandler(Sender, e);
            }
        }
        public void MessageMdmSendToPageTestIt() {
            MessageMdmSendToPageArgs MessageMdmSendToPageArgsTestE = new MessageMdmSendToPageArgs("Hi there.");
            MessageMdmSendToPageOnA(MessageMdmSendToPageArgsTestE);
        }
        /// <remarks>
        /// Agile MessageMdmSendToPageArgs work in progress 
		/// implementing Message Argument Structure (*** IN PROGRESS ***)
        /// Will also adopt separate progress structure, RunAction structures with indexers
        /// and separation of progress updates from status line messages.
        /// Wires the method to the event.
        /// ThatA.MessageMdmSendToPageA += new MessageMdmSendToPageAEventHandler(ThatB.Method);
		/// 
		/// </remarks> 

        /// <remarks>
        /// Message Delegates Section
		/// MessageMdmSendTo
        /// </remarks>
 
        /// <summary>
        /// <para> Sends a message to the page and add a new line after it. </para>
        /// </summary>
        public delegate void MessageMdmSendToPageNewLineDel(Object Sender, String PassedMessage);
        /// <summary>
        /// <para> Sends a message to the page and a flag to indicate if a new line should follow. </para>
        /// </summary>
        public delegate void MessageMdmSendToPageNewLineSetDel(Object Sender, String PassedMessage, bool PassedNewLine);
        /// <summary>
        /// <para> Sends a message to the page. </para>
        /// </summary>
        public delegate void MessageMdmSendToPageDel(Object Sender, String PassedMessage);
        //
        public MessageMdmSendToPageNewLineSetDel MessageMdmSendToPageNewLineSet;
        public MessageMdmSendToPageNewLineDel MessageMdmSendToPageNewLine;
        public MessageMdmSendToPageDel MessageMdmSendToPage;
        #endregion
        #region Message Mdd To Page Overloads - MessageMdmSendToPage
		/// <summary>
        /// see MessageMdmSendToPageImpl(Object Sender, String PassedMessage)
        /// </summary> 
        /// <param name=""></param> 
        public virtual void MessageMdmSendToPageNewLineSetImpl(Object Sender, MessageMdmSendToPageArgs e) {
            MessageMdmSendToPageNewLineSetImpl(Sender, e.MessageToPage);
        }
		/// <summary>
        /// see MessageMdmSendToPageImpl(Object Sender, String PassedMessage)
        /// </summary> 
        /// <param name=""></param> 
        public virtual void MessageMdmSendToPageNewLineSetImpl(Object Sender, String PassedMessage) {
            MessageMdmSendToPageNewLineImpl(Sender, PassedMessage, true);
        }
		/// <summary>
        /// see MessageMdmSendToPageImpl(Object Sender, String PassedMessage)
        /// </summary> 
        public virtual void MessageMdmSendToPageNewLineImpl(Object Sender, String PassedMessage, bool PassedNewLine) {
            if (PassedNewLine) {
                PassedMessage += "\n";
            }
            MessageMdmSendToPageImpl(Sender, PassedMessage);
        }
        #endregion
		#region Message Mdd To Page - Main Method
		/// <summary>
		/// Message Mdd To Page - Main Method
        /// Does actual Output...
        /// Uses PrintOutputMdm_PickPrint
        /// Message routing.  Formatted messages
		/// using a prefix message type.
		/// Routing can be altered by replacing the
		/// action delegate (PrintOutputMdm_PickPrint)
		/// i.e. ThreadUiTextMessageAsync(ConsoleOutput));
		/// This is the default behavior might be overriden or
		/// even turn off.
        /// </summary> 
        /// <param name="Sender"></param> 
        /// <param name="PassedMessage">Message to send to page</param> 
        /// <remarks>
		/// </remarks> 
        public virtual void MessageMdmSendToPageImpl(Object Sender, String PassedMessage) {
            try {
                MessageStatusTargetText = PassedMessage.Substring(0, 1);
            } catch { MessageStatusTargetText = "C"; }

            switch (MessageStatusTargetText) {
                case ("A"):
                    // Add to text area 1-4
                    break;
                case ("M"):
                    // Message replaces text area 1-4
                    break;
                case ("C"):
                    // Console Output
                    if (ConsolePickConsoleOn == bOFF) { return; }
                    break;
                case ("E"):
                    // E stands for Error
                    break;
                case ("T"):
                    // Console Text Output
                    if (ConsoleTextOn == bOFF) { return; }
                    break;
                default:
                    return;
            }
			//
            PrintOutputMdm_PickPrint(Sender, iMessageLevelLast, PassedMessage, true);
            // ???ThreadUiTextMessageAsync(ConsoleOutput));
        }
        #endregion
        #endregion
        //
        #region $include Mdm.Oss.Decl StdDefBaseRunFileConsole Console Flags, Delegates and Initialize
		/// <summary>
        /// Console Base class initialization after constructor.
		/// Feature and code groups controlled are:
        /// Message areas:
        ///  Console = "";
        ///  StatusLine = "";
        ///  Progress = "";
        ///  Error = "";
        ///  RunAction = "";
        ///
		/// Method group names:
		/// 1) TraceMdmDo
		/// 2) MessageMdmSendTo
		/// 3) PrintOutputMdm
		/// 4) PrintOutputMdm_Pick
		/// 5) TextReaderWriter
		/// 6) StreamReaderWriter
		/// 7) ConsoleMdmStd
		/// 8) ConsoleMdmPick
		///
		/// Notes and history:
		/// These routines combine status line (or box) handling along with
		/// console display.
        ///
		/// Validate Target processing:
		/// The message prefix is examined (1 or more characters) to determine
		/// the type of message and route it to the appropriate user interface
		/// element.
		///
        /// Validate Output Marshalling:
        /// Character 1 - Target:
        /// To Console:
        /// C - 
        /// example: CHello there
		/// 
        /// Messages to Text Box or Status Line 
		/// (currently not separate) 
		/// Either a Text Box, Combo Box or Status Line Control
		/// will be set.  Line mode uses strings and would be
		/// consumed by a user defined control.
		/// Two message types are currently supported:
        /// A - Add or set text
        /// M - Message not for display
        /// example A1Hello upper box
        /// example M2Hello lower box
		///
		/// When fully implmented routed messages will include:
		/// A(dd)9 to line N (using defaults).
		/// C(onsole)9 message.
		/// F(ile stream)9.
		/// L(ine area)9 of Status Line.
		/// T(arget box)9 number (Text, Combo, Flow Document).
		/// M(essage) not for display, i.e. commands.
		/// P(rogress bar) control.
        /// V(alue) of property (i.e. Progress Bar).
		/// Lo(w) Value, H(igh) Value (Minimum, Maximum).
		///
		/// Delegates set to base class method implementations by default.
		///
		/// The feature tests in code logic tend to follow the following order:
			/// 1) A User Interface is in use.
			/// 2) Start, Cancel, Pause / Resume buttons are used.
            /// 3) Message processing used.
            /// 4) Application is multi-threaded.
            /// 5) Trace, Logging and Pick Console and Console Options.
			/// 6) Ui Interface Box, Status Line and Console are in use.
				/// 6.1) Text or Combo Boxes are used
                /// 6.2) Box Management is used
                /// 6.3) A User Interface is in use
		///
        /// </summary> 
        /// <param name="RunFileConsolePassed"></param> 
        /// <remarks>
		/// 1) References to the Pick Console apply to legacy application
		/// implementations that are console or green screen based.
        /// PRINTER or DISPLAY handling (controlled by PRINTER ON|OFF)
        /// 2) PrintOutputMdm_PickPrint NewLine *** does actual output
        /// 3) Routing generally depends on flags such as TraceOn.
        /// And make use of methods including TraceMdmDoImpl.
        /// The flags ConsoleOn || ConsolePickConsoleOn || 
		/// ConsolePickConsoleBasicOn direct 
		/// output to ConsoleMdmPickDisplayImpl
        /// 4) However all messages originating in background or worker
		/// thread (that lack a user interface) must pass through
        /// ThreadUiProgressAsync
		/// </remarks> 
        public void ConsoleMdmInitialize(ref Object RunFileConsolePassed) {
            // ConsoleMdmFlagsInitialize(RunFileConsolePassed);
            ((DefStdBaseRunFileConsole)RunFileConsolePassed).XUomUrvvXvCreateNow = true;
			// A User Interface is in use.
            if (ClassFeature.MdmUiIsUsed) {
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ProgressBarMdm1 = ProgressBarMdm1;
            }
			// Start, Cancel, Pause / Resume buttons are used.
            if (ClassFeature.MdmButtonIsUsed) {
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).StartButtonPressed = StartButtonPressed;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).CancelButtonPressed = CancelButtonPressed;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).PauseButtonPressed = PauseButtonPressed;
            }
            // Message processing used.
			// Delegates set to base class.
            if (ClassFeature.MdmSendIsUsed) {
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).MessageMdmSendToPageNewLineSet = MessageMdmSendToPageNewLineImpl;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).MessageMdmSendToPageNewLine = MessageMdmSendToPageNewLineSetImpl;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).MessageMdmSendToPage = MessageMdmSendToPageImpl;
            }
            // Application is multi-threaded.
            if (ClassFeature.MdmThreadIsUsed) {
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ThreadUiProgressAsync = ThreadUiProgressAsync;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ThreadUiTextMessageAsync = ThreadUiTextMessageAsync;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ThreadUiProgressAsyncInvoke = ThreadUiProgressAsyncInvoke;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ThreadUiTextMessageAsyncInvoke = ThreadUiTextMessageAsyncInvoke;
            }
            // Trace, Logging and Pick Console and Console Options.
            if (ClassFeature.MdmConsoleIsUsed) {
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).TraceData = TraceData;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).TraceOn = TraceOn;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleOn = ConsoleOn;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickConsoleOn = ConsolePickConsoleOn;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickConsoleBasicOn = ConsolePickConsoleBasicOn;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleToDisc = ConsoleToDisc;
                //
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleVerbosity = ConsoleVerbosity;
                //
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).DoLogActivity = DoLogActivityDefault;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).TraceDisplayMessageDetail = TraceDisplayMessageDetail;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).TraceHeadings = TraceHeadings;
            }
            //
            #region Line, Box and Console Delegates
			// Ui Interface Box, Status Line and Console are in use.
            if (ClassFeature.StatusUiIsUsed) {
				// Text or Combo Boxes are used
                if (StatusUi.BoxIsUsed) {
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.ItemAdd = StatusUi.BoxAddImpl;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Item1Add = StatusUi.Box1AddImpl;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Item2Add = StatusUi.Box2AddImpl;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Item3Add = StatusUi.Box2AddImpl;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Item4Add = StatusUi.Box2AddImpl;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.TextConsoleAdd = StatusUi.TextConsoleBoxAddImpl;
                    //
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Box1 = StatusUi.Box1;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Box2 = StatusUi.Box2;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Box3 = StatusUi.Box2;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Box4 = StatusUi.Box2;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.TextConsoleBox = StatusUi.TextConsoleBox;
                } else {
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.ItemAdd = StatusUi.LineAddImpl;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Item1Add = StatusUi.Line1AddImpl;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Item2Add = StatusUi.Line2AddImpl;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Item3Add = StatusUi.Line2AddImpl;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Item4Add = StatusUi.Line2AddImpl;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.TextConsoleAdd = StatusUi.TextConsoleAddImpl;
                    //
                    ////<Binding Path="CurrentUser.Rating" 
                    ////Source="{x:Static Application.Current}"/>
                    ////Text="{Binding Source={StaticResource StatusUi}, Path=Line1}" 
                    ////Mdm.Srt.MinputTld.MinputTldPageMain.
                    //// Line1Binding.Path = new PropertyPath(StatusUi.Line1);
                    //// StatusLine1Element.DataContext = "Source={x:Static MinputTldPageMain}, Path=StatutsLine.Line1";
                    ////BindingOperations.SetBinding(StatusLine1Element, TextBlock.TextProperty, (Binding)StatusUi.Line1);
                    //System.Windows.Data.Binding Line1Binding = new System.Windows.Data.Binding("Source ={x:Static MinputTldPageMain}, StatusUi.Line1");
                    //// Line1Binding.Source = "{x:Static MinputTldPageMain}";
                    //StatusLine1Element.SetBinding(System.Windows.Controls.TextBox.TextProperty, Line1Binding);

                    ////BindingSource Line1BindingSource = new BindingSource();
                    ////Line1BindingSource.DataSource = StatusUi.Line1;
                    //BindingOperations.SetBinding(
                    //    StatusLine1Element, 
                    //    System.Windows.Controls.TextBlock.TextProperty,
                    //    Line1Binding);
                    //// StatusLine1Element.DataContext = Line1BindingSource;

                    //StatusLine1Element.InvalidateVisual();

                }
                // Box Management is used
                if (((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.BoxManageIsUsed) {
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Box1Manage.ScrollDo = StatusUi.Box1Manage.ScrollDo;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Box2Manage.ScrollDo = StatusUi.Box2Manage.ScrollDo;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Box3Manage.ScrollDo = StatusUi.Box3Manage.ScrollDo;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Box4Manage.ScrollDo = StatusUi.Box4Manage.ScrollDo;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.TextConsoleManage.ScrollDo = StatusUi.TextConsoleManage.ScrollDo;
                }
                // A User Interface is in use
                if (ClassFeature.MdmUiIsUsed) {
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).PageSizeChangedDoAdjust = PageSizeChangedDoAdjustImpl;
                }
            }
            #endregion
            //
        }
		
		/// <summary>
        /// The Flag Initialize sets trace and console control flags only, not data.
        /// </summary> 
        /// <param name="RunFileConsolePassed">The console object to apply flags to.</param> 
        public void ConsoleMdmFlagsInitialize(ref Object RunFileConsolePassed) {
            // Std_I0_Console
            //
            if (ClassFeature.MdmConsoleIsUsed) {
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).TraceData = bNO;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).TraceOn = bYES;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleOn = bNO;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickConsoleOn = bYES;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickConsoleBasicOn = bYES;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleToDisc = bNO;
                //
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).DoLogActivity = bYES;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).TraceDisplayMessageDetail = bYES;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).TraceHeadings = bNO;
                //
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleVerbosity = 3;
                // Display
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleOutput = "";
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleOutputLog = "";
                // <Area Id = "((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickConsole">
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleOn = bON;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickConsoleBasicOn = bON;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickConsoleToDisc = bOFF;
                // Display
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickConsoleOutput = "";
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickConsoleOutputLog = "";
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickTextConsole = "";
                // public ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickTextBlock;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickConsoleTextBlock = ""; // text block
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickConsoleTextPositionX = 0;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickConsoleTextPositionY = 0;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsolePickConsoleTextPositionZ = 0;
                // 
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleTextOn = bON;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleText0On = bOFF;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleText1On = bON;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleText2On = bON;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleText3On = bOFF;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleText4On = bOFF;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).ConsoleText5On = bOFF;
                //
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).sMessageText = "";
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).sMessageText0 = "";
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).MessageTextOutConsole = "";
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).MessageTextOutStatusLine = "";
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).MessageTextOutProgress = "";
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).MessageTextOutError = "";
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).MessageTextOutRunAction = "";
                // <Area Id = "TextConsole>
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).CommandLineRequest = "";
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).CommandLineRequestResult = 0;
                ((DefStdBaseRunFileConsole)RunFileConsolePassed).TextConsole = "";
            }
            //
            if (ClassFeature.StatusUiIsUsed) {
                if (((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.BoxManageIsUsed) {
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Box1Manage.ScrollDo = true;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Box2Manage.ScrollDo = true;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Box3Manage.ScrollDo = true;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.Box4Manage.ScrollDo = true;
                    ((DefStdBaseRunFileConsole)RunFileConsolePassed).StatusUi.TextConsoleManage.ScrollDo = true;
                }
            }
        }
        #endregion
        //
        #region Page Size, Invalidate, Paint
        public bool PageIsUsed;
        public Grid PageMainGrid;
        public ColumnDefinition[] PageMainGridCol;
        public ScrollViewer PageMainScrollViewer;
		/// <summary>
        /// Calculates and adjust the page to the best width for
		/// displaying the text box content based on the current
		/// and recent history of line widths.
        /// </summary> 
        /// <param name="PageSender">The PAGE object sending the message.</param> 
        /// <param name="TextBoxManagePassed">The Box UI Management object.</param> 
        /// <param name="PassedTextBox">The Box being adjusted (UI Element)</param> 
        /// <param name="PassedDesiredWidth">The preferred box width.</param> 
        /// <param name="DesiredHeightPassed">The preferred box height.</param> 
        /// <remarks>
		/// </remarks> 
        public void PageSizeChangedDoAdjustImpl(Object PageSender, TextBoxManageDef TextBoxManagePassed, System.Windows.Controls.TextBox PassedTextBox, double PassedDesiredWidth, double DesiredHeightPassed) {
            if (!ClassFeature.MdmUiIsUsed) { return; }
            PageMainSizeChangedResult = (long)StateIs.Started;
            #region Top
            // Box1.Text += "Adjusting screen, please wait... " + "\n";
            // TODO $$$$NEXT Window Height Width and Focus
            bool bInvalidateVisual = false;
            double Base_Width = ((Page)PageSender).Width;
            double WindowWidth = 0;
            double GridMainWidth = 0;
            if (TextBoxManagePassed != null) {
                double WidthIdeal = ((Page)PageSender).ActualWidth
                            - (TextBoxManagePassed.BoxPadding.dLeft + TextBoxManagePassed.BoxPadding.dRight) - 10;
            }
            #endregion
            #region No Desired Width and Height
            //if (PassedDesiredWidth == 0 && DesiredHeightPassed == 0) {
            //    if (TextBoxManagePassed.WidthHigh == 0) { TextBoxManagePassed.WidthHigh = this.ActualWidth; }
            //    if (TextBoxManagePassed.WidthLow == 0) { TextBoxManagePassed.WidthLow = this.ActualWidth; }
            //    //
            //    TextBoxManagePassed.WidthCurrent = BoxPassed.Width;
            //    TextBoxManagePassed.BoxWidthCurrent = this.ActualWidth - 10;
            //    TextBoxManagePassed.BoxWidthCurrent -= (TextBoxManagePassed.BoxPadding.dLeft + TextBoxManagePassed.BoxPadding.dRight);
            //    if (TextBoxManagePassed.WidthCurrent > TextBoxManagePassed.BoxWidthCurrent) {
            //        if (TextBoxManagePassed.WidthCurrent > TextBoxManagePassed.WidthHigh) {
            //            TextBoxManagePassed.WidthHigh = TextBoxManagePassed.WidthCurrent;
            //        }
            //    }
            //    if (TextBoxManagePassed.WidthCurrent < TextBoxManagePassed.BoxWidthCurrent) {
            //        if (TextBoxManagePassed.WidthCurrent > TextBoxManagePassed.WidthLow) {
            //            TextBoxManagePassed.WidthLow = TextBoxManagePassed.WidthCurrent;
            //        }
            //    }
            //    if (TextBoxManagePassed.WidthCurrent > 0) {
            //        TextBoxManagePassed.WidthHigh = TextBoxManagePassed.WidthCurrent / 2;
            //        TextBoxManagePassed.WidthLow = BoxPassed.MinWidth;
            //    } else {
            //        TextBoxManagePassed.WidthLow = BoxPassed.MinWidth
            //            + (TextBoxManagePassed.BoxPadding.dLeft + TextBoxManagePassed.BoxPadding.dRight);
            //        TextBoxManagePassed.WidthHigh = BoxPassed.MinWidth
            //            + (TextBoxManagePassed.BoxPadding.dLeft + TextBoxManagePassed.BoxPadding.dRight);
            //    }
            //}
            #endregion
            #region Parent Size
            String sParent_ActualWidth;
            double dParent_ActualWidth;
            String sParent_ActualHeight;
            double dParent_ActualHeight;
            try {
                sParent_ActualWidth = (((Page)PageSender).Parent.GetValue((Window.ActualWidthProperty)).ToString());
                dParent_ActualWidth = Convert.ToDouble(sParent_ActualWidth);
                sParent_ActualHeight = (((Page)PageSender).Parent.GetValue((Window.ActualHeightProperty)).ToString());
                dParent_ActualHeight = Convert.ToDouble(sParent_ActualHeight);
            } catch {
                dParent_ActualWidth = ((Page)PageSender).ActualWidth;
                dParent_ActualHeight = ((Page)PageSender).ActualHeight;
            }
            #endregion
            #region Window Size
            String sWindow_ActualWidth;
            double dWindow_ActualWidth;
            String sWindow_ActualHeight;
            double dWindow_ActualHeight;
            try {
                sWindow_ActualWidth = (((Page)PageSender).GetValue((Window.ActualWidthProperty)).ToString());
                dWindow_ActualWidth = Convert.ToDouble(sWindow_ActualWidth);
                sWindow_ActualHeight = (((Page)PageSender).GetValue((Window.ActualHeightProperty)).ToString());
                dWindow_ActualHeight = Convert.ToDouble(sWindow_ActualHeight);
            } catch {
                dWindow_ActualHeight = ((Page)PageSender).ActualHeight;
                dWindow_ActualWidth = ((Page)PageSender).ActualWidth;
            }
            #endregion
            #region Frame Size
            String sFrame_ActualWidth;
            double dFrame_ActualWidth;
            String sFrame_ActualHeight;
            double dFrame_ActualHeight;
            try {
                sFrame_ActualWidth = (((Page)PageSender).GetValue((Frame.ActualWidthProperty)).ToString());
                dFrame_ActualWidth = Convert.ToDouble(sFrame_ActualWidth);
                sFrame_ActualHeight = (((Page)PageSender).GetValue((Frame.ActualHeightProperty)).ToString());
                dFrame_ActualHeight = Convert.ToDouble(sFrame_ActualHeight);
            } catch {
                dFrame_ActualHeight = dWindow_ActualHeight;
                dFrame_ActualWidth = dWindow_ActualWidth;
            }
            #endregion
            #region Grid Size
            double dGridActualWidth = 0;
            double dGridActualHeight = 0;
            try {
                if (((DefStdBaseRunFileConsole)PageSender) != null) {
                    dGridActualWidth = ((DefStdBaseRunFileConsole)PageSender).PageMainGrid.ActualWidth;
                    dGridActualHeight = ((DefStdBaseRunFileConsole)PageSender).PageMainGrid.ActualHeight;
                } else if (AppObject != null) {
                    dGridActualWidth = AppObject.MainWindow.ActualWidth;
                    dGridActualHeight = AppObject.MainWindow.ActualHeight;
                }
                if (dGridActualWidth == 0) {
                    dGridActualWidth = ((Page)PageSender).ActualWidth;
                    dGridActualHeight = ((Page)PageSender).ActualHeight;
                }
            } catch { }
            #endregion
            #region Load Desired width and height
            double dDesiredWidth = PassedDesiredWidth;
            double dDesiredHeight = DesiredHeightPassed;
            if (PageMainScrollViewer.MinHeight + PageMainScrollViewer.MaxHeight == 0) { return; }
            if (dDesiredWidth == 0) {
                if (TextBoxManagePassed != null) {
                    if (TextBoxManagePassed.WidthLow == 0) {
                        // dDesiredWidth = dParent_ActualWidth;
                        dDesiredWidth = (PageMainScrollViewer.MinHeight + PageMainScrollViewer.MaxHeight) / 2;
                        // + (TextBoxManagePassed.BoxPadding.dLeft + TextBoxManagePassed.BoxPadding.dRight) + 10;
                    } else {
                        dDesiredWidth = TextBoxManagePassed.WidthLow
                        + (TextBoxManagePassed.BoxPadding.dLeft + TextBoxManagePassed.BoxPadding.dRight) + 10;
                    }
                }
                if (dDesiredWidth > PageMainScrollViewer.MaxWidth) { dDesiredWidth = PageMainScrollViewer.MaxWidth; }
            }
            if (dDesiredHeight == 0) {
                dDesiredHeight = (PageMainScrollViewer.MinHeight + PageMainScrollViewer.MaxHeight) / 2;
                // dDesiredHeight = dParent_ActualHeight;
                if (dDesiredHeight > PageMainScrollViewer.MaxHeight) { dDesiredHeight = PageMainScrollViewer.MaxHeight; }
            }
            if (dDesiredHeight > 0 && dDesiredHeight > ((Page)PageSender).MaxHeight) { dDesiredHeight = ((Page)PageSender).MaxHeight; }
            if (dDesiredWidth > 0 && dDesiredWidth > ((Page)PageSender).MaxWidth) { dDesiredWidth = ((Page)PageSender).MaxWidth; }
            if (dDesiredHeight > 0 && dDesiredHeight < ((Page)PageSender).MinHeight) { dDesiredHeight = ((Page)PageSender).MinHeight; }
            if (dDesiredWidth > 0 && dDesiredWidth < ((Page)PageSender).MinWidth) { dDesiredWidth = ((Page)PageSender).MinWidth; }
            #endregion
            #region Adjust Grid
            if (true == true) {
                dGridActualWidth = dDesiredWidth;
                dGridActualHeight = dDesiredHeight;
                bInvalidateVisual = true;
                return;
            }
            //
            if (((Page)PageSender).ActualHeight > 0 && ((Page)PageSender).ActualHeight > ((Page)PageSender).MaxHeight) {
                ((Page)PageSender).Height = ((Page)PageSender).MaxHeight;
                bInvalidateVisual = true;
            }
            if (((Page)PageSender).ActualWidth > 0 && ((Page)PageSender).ActualWidth > ((Page)PageSender).MaxWidth) {
                ((Page)PageSender).Width = ((Page)PageSender).MaxWidth;
                bInvalidateVisual = true;
            }
            if (((Page)PageSender).ActualHeight > 0 && ((Page)PageSender).ActualHeight < ((Page)PageSender).MinHeight) {
                ((Page)PageSender).Height = ((Page)PageSender).MinHeight;
                bInvalidateVisual = true;
            }
            if (((Page)PageSender).ActualWidth > 0 && ((Page)PageSender).ActualWidth < ((Page)PageSender).MinWidth) {
                ((Page)PageSender).Width = ((Page)PageSender).MinWidth;
                bInvalidateVisual = true;
            }
            #endregion
            #region Invalidate Visual
            //
            // TODO FrameworkElement.SizeChangedEvent();
            if (bInvalidateVisual) {
                RoutedEventArgs re = new RoutedEventArgs();
                re.Source = this;
                re.RoutedEvent = Page.SizeChangedEvent;
                ((Page)PageSender).RaiseEvent(new RoutedEventArgs());
                re.Source = this;
                re.Handled = false;
                re.RoutedEvent = Page.RequestBringIntoViewEvent;
                ((Page)PageSender).RaiseEvent(new RoutedEventArgs());
                ((Page)PageSender).InvalidateVisual();
            }
            #endregion
        }
        //
		/// <summary>
        /// Page Main Grid has changed event.
        /// </summary> 
        /// <param name="Sender">The object firing the event.</param> 
        /// <param name="e">RoutedEventArgs for size change event</param> 
        public void PageMainGridChanged(Object Sender, RoutedEventArgs e) {
            if (!ClassFeature.MdmUiIsUsed) { return; }
            PageMainGridSizeChangedDoAdjust((Page)Sender, 0, 0);
        }
        public long PageMainSizeChangedResult;
		/// <summary>
        /// Calculates and adjust the page to the best width for
		/// the main grid and content based on the current
		/// and recent history of line widths.
        /// </summary> 
        /// <param name="PageSender">The PAGE object sending the message.</param> 
        /// <param name="PassedDesiredWidth">The preferred box width.</param> 
        /// <param name="DesiredHeightPassed">The preferred box height.</param> 
        public void PageMainGridSizeChangedDoAdjust(Page Sender, double PassedDesiredWidth, double PassedDesiredHeight) {
            PageMainSizeChangedResult = (long)StateIs.Started;
            if (!ClassFeature.MdmUiIsUsed) { return; }
            // PageMainGrid.
            // XUomCovvXv.XUomPmvvXv.TldOptionRows
            //
            double dColWidth_99;
            foreach (ColumnDefinition ColCurr in PageMainGrid.ColumnDefinitions) {
                dColWidth_99 = ColCurr.ActualWidth;
            }
            //double dColWidth_0 = gcCol0.ActualWidth;
            //double dColWidth_1 = gcCol1.ActualWidth;
            //double dColWidth_2 = gcCol2.ActualWidth;
            //double dColWidth_3 = gcCol3.ActualWidth;
            //double dColWidth_4 = gcCol4.ActualWidth;
            //double dColWidth_5 = gcCol5.ActualWidth;
        }
        #endregion

    }
    // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
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
    public class StatusUiDef : DefStdBase {
        // , IWeakEventListener 
        public Object Sender;
        public StatusUiDef StatusUiObject;
        #region Status Line and Console Strings Declarations
        /// <summary>
        /// <para> Enumeration correlates Element Names to integer Item Numbers</para>
        /// </summary>
        public enum LineNumIs : int {
            Line1 = 1,
            Line2 = 2,
            Line3 = 3,
            Line4 = 4,
            Box1 = 1,
            Box2 = 2,
            Box3 = 3,
            Box4 = 4,
            TextConsole = 5,
            None = 0
        }
        #region Line Buffers
        public String spLine1;
        public String Line1 { 
            get { return spLine1; } 
            set { 
                spLine1 = value;
                if (BoxIsUsed) { OnPropertyChanged("Line1"); }
            } 
        }
        
        private String spLine2;
        public String Line2 { 
            get { return spLine2; } 
            set { 
                spLine2 = value;
                if (BoxIsUsed) { OnPropertyChanged("Line1"); }
            } 
        }
        private String spLine3;
        public String Line3 { 
            get { return spLine3; } 
            set { 
                spLine3 = value;
                if (BoxIsUsed) { OnPropertyChanged("Line1"); }
            } 
        }
        private String spLine4;
        public String Line4 { 
            get { return spLine4; } 
            set { 
                spLine4 = value;
                if (BoxIsUsed) { OnPropertyChanged("Line1"); }
            } 
        }
        //
        private String spTextConsole;
        public String TextConsole {
            get { return spTextConsole; }
            set {
                spTextConsole = value;
                OnPropertyChanged("TextConsoleBox");
            }
        }
        #endregion
        //
        public bool LineIsUsed;
        #endregion
        //
        #region OnPropertyChanged method to raise the event
        public event PropertyChangedEventHandler PropertyChanged;
        PropertyChangedEventHandler PropertyChangedHandler;
		/// <summary>
        /// Property change features not implemented
        /// </summary> 
        protected void OnPropertyChanged(string LineNamePassed, PropertyChangedEventHandler HandlerPassed) {
            PropertyChangedHandler = PropertyChanged;
            if (PropertyChangedHandler != null) {
                PropertyChangedHandler(this, new PropertyChangedEventArgs(LineNamePassed));
            }
        }

		/// <summary>
        /// Property change features not implemented
        /// </summary> 
        protected void OnPropertyChanged(string LineNamePassed) {
            try {
                TextBox TextBoxCurr = BoxObjectGet(LineNamePassed);

                //RoutedEventArgs re = new RoutedEventArgs();
                //re.Source = this;
                //re.RoutedEvent = Page.SizeChangedEvent;
                //((Page)PageSender).RaiseEvent(new RoutedEventArgs());

                // PropertyChanged = TextBoxCurr.SourceUpdated;

                // this.OnPropertyChanged += new System.EventHandler(TextBoxCurr.SourceUpdated());
                // TextBoxCurr.RaiseEvent(TextBoxCurr.SourceUpdated);
                // TextBoxCurr.Parent.Dispatcher.Invoke(TextBoxCurr.SizeChanged, this, null);

                TextBoxCurr.InvalidateVisual();

                //if (PropertyChangedHandler != null) {
                //    PropertyChangedHandler(this, new PropertyChangedEventArgs(LineNamePassed));
                //}
            } catch { }
        }

        #endregion
        #region Status Line TextBox
        public TextBox Box1;
        public TextBoxManageDef Box1Manage;
        public TextBox Box2;
        public TextBoxManageDef Box2Manage;
        public TextBox Box3;
        public TextBoxManageDef Box3Manage;
        public TextBox Box4;
        public TextBoxManageDef Box4Manage;
        // Console
        public TextBox TextConsoleBox;
        public TextBoxManageDef TextConsoleManage;
        public bool BoxIsUsed;
        public bool BoxManageIsUsed;
        public bool BoxDelegateIsUsed;
        #endregion
        #region Delegates for Status Lines and Console

        /// <summary>
        /// <para> Main Method Item Add Text</para>
        /// </summary>
        public delegate void ItemAddDel(Object Sender, String PassedText);
        public ItemAddDel ItemAdd;
        /// <summary>
        /// <para> Main Method Item Change Event</para>
        /// </summary>
        public delegate void ItemChangedDel(Object Sender, String PassedText);
        public ItemChangedDel ItemChanged;

        /// <summary>
        /// <para> Add Text to Status Line Item 1.</para>
        /// </summary>
        public delegate void Item1AddDel(Object Sender, String PassedText);
        public Item1AddDel Item1Add;
        /// <summary>
        /// <para> Text ChangeEvent for Status Line Item 1.</para>
        /// </summary>
        public delegate void Item1ChangedDel(Object Sender, String PassedText);
        public Item1ChangedDel Item1Changed;

        /// <summary>
        /// <para> Add Text to Status Line Item 2.</para>
        /// </summary>
        public delegate void Item2AddDel(Object Sender, String PassedText);
        public Item2AddDel Item2Add;
        /// <summary>
        /// <para> Text ChangeEvent for Status Line Item 2.</para>
        /// </summary>
        public delegate void Item2ChangedDel(Object Sender, String PassedText);
        public Item2ChangedDel Item2Changed;

        /// <summary>
        /// <para> Add Text to Status Line Item 3.</para>
        /// </summary>
        public delegate void Item3AddDel(Object Sender, String PassedText);
        public Item3AddDel Item3Add;
        /// <summary>
        /// <para> Text ChangeEvent for Status Line Item 3.</para>
        /// </summary>
        public delegate void Item3ChangedDel(Object Sender, String PassedText);
        public Item3ChangedDel Item3Changed;

        /// <summary>
        /// <para> Add Text to Status Line Item 4.</para>
        /// </summary>
        public delegate void Item4AddDel(Object Sender, String PassedText);
        public Item4AddDel Item4Add;
        /// <summary>
        /// <para> Text ChangeEvent for Status Line Item 4.</para>
        /// </summary>
        public delegate void Item4ChangedDel(Object Sender, String PassedText);
        public Item4ChangedDel Item4Changed;

        /// <summary>
        /// <para> Add Text to Text Console.</para>
        /// </summary>
        public delegate void TextConsoleAddDel(Object Sender, String PassedText);
        public TextConsoleAddDel TextConsoleAdd;
        /// <summary>
        /// <para> Text ChangeEvent for the Text Console.</para>
        /// </summary>
        public delegate void TextConsoleChangedDel(Object Sender, String PassedText);
        public TextConsoleChangedDel TextConsoleChanged;
        #endregion
        #region Constructors, DataClear & DelegateClear
        public StatusUiDef() {
            LineIsUsed = true;
            BoxIsUsed = false;
            BoxManageIsUsed = true;
            BoxDelegateIsUsed = true;
            Sender = this;
            SenderIsThis = this;
            StatusUiObject = this;
        }

        // public static readonly DependencyProperty Line1Property = DependencyProperty.Register("Line1", typeof(String), typeof(StatusUiDef));
        public String WasCreatedBy;
		/// <summary>
        /// Constructor indicating if Line Strings, Text Boxes and Box Management is used.
        /// </summary> 
        public StatusUiDef(String WasCreatedByPassed, bool LineIsUsedPassed, bool BoxIsUsedPassed, bool BoxManageIsUsedPassed, bool BoxDelegateIsUsedPassed)
            : this() {
            WasCreatedBy = WasCreatedByPassed;
            LineIsUsed = LineIsUsedPassed;
            BoxIsUsed = BoxIsUsedPassed;
            BoxManageIsUsed = BoxManageIsUsedPassed;
            BoxDelegateIsUsed = BoxDelegateIsUsedPassed;
            if (LineIsUsed | BoxIsUsed | BoxManageIsUsed | BoxDelegateIsUsed) { ClassFeature.StatusUiIsUsed = true; }
            if (LineIsUsed) { LineDataClear(); }
            BoxCreate();
            if (BoxDelegateIsUsed) { BoxDelegatesSetDefault(ref StatusUiObject); }
        }

		/// <summary>
        /// Create Boxes and Box Management objects 
        /// </summary> 
        public void BoxCreate() {
            // Display Management
            if (BoxManageIsUsed) {
                Box1Manage = new TextBoxManageDef();
                Box2Manage = new TextBoxManageDef();
                Box3Manage = new TextBoxManageDef();
                Box4Manage = new TextBoxManageDef();
                TextConsoleManage = new TextBoxManageDef();
            }
            // Text Boxes
            if (!BoxIsUsed) { return; }
            Box1 = new TextBox();
            Box2 = new TextBox();
            Box3 = new TextBox();
            Box4 = new TextBox();
            // Console
            TextConsoleBox = new TextBox();
        }
        #endregion
        #region DataClear
		/// <summary>
        /// Standard data clear method
        /// </summary> 
        public void DataClear() { DataClear(false); }

		/// <summary>
        /// Standard clear with flag to indicate if delegates should be cleared.
        /// </summary> 
        public void DataClear(bool DoDelegatePassed) {
            if (LineIsUsed) { LineDataClear(); }
            BoxDataClear();
            if (DoDelegatePassed) { BoxDelegatesClear(); }
        }

		/// <summary>
        /// Clears Line Strings
        /// </summary> 
        public void LineDataClear() { Line1 = Line2 = Line3 = Line4 = TextConsole = ""; }

		/// <summary>
        /// Clears Box Text and Management data
        /// </summary> 
        public void BoxDataClear() {
            if (BoxManageIsUsed) {
                // Display Management
                Box1Manage.DataClear();
                Box2Manage.DataClear();
                Box3Manage.DataClear();
                Box4Manage.DataClear();
                // Console
                TextConsoleManage.DataClear();
            }
            // Text Boxes
            if (!BoxIsUsed) { return; }
            Box1.Text = "";
            Box2.Text = "";
            Box3.Text = "";
            Box4.Text = "";
            // Console
            TextConsoleBox.Text = "";
        }
        #endregion
        #region Line / Box Object Set
		/// <summary>
        /// Copy Lines or Boxes to passed target
        /// </summary> 
        public void BoxObjectsCopyTo(ref StatusUiDef StatusUiPassed) {
            if (LineIsUsed) {
                StatusUiPassed.Line1 = Line1;
                StatusUiPassed.Line2 = Line2;
                StatusUiPassed.Line3 = Line3;
                StatusUiPassed.Line4 = Line4;
                StatusUiPassed.TextConsole = TextConsole;
            }
            if (BoxIsUsed) {
                StatusUiPassed.Box1 = Box1;
                StatusUiPassed.Box2 = Box2;
                StatusUiPassed.Box3 = Box3;
                StatusUiPassed.Box4 = Box4;
                StatusUiPassed.TextConsoleBox = TextConsoleBox;
            }
        }

		/// <summary>
        /// Copy Lines or Boxes from passed source
        /// </summary> 
        public void BoxObjectsCopyFrom(ref StatusUiDef StatusUiPassed) {
            if (LineIsUsed) {
                Line1 = StatusUiPassed.Line1;
                Line2 = StatusUiPassed.Line2;
                Line3 = StatusUiPassed.Line3;
                Line4 = StatusUiPassed.Line4;
                TextConsole = StatusUiPassed.TextConsole;
            }
            if (BoxIsUsed) {
                Box1 = StatusUiPassed.Box1;
                Box2 = StatusUiPassed.Box2;
                Box3 = StatusUiPassed.Box3;
                Box4 = StatusUiPassed.Box4;
                TextConsoleBox = StatusUiPassed.TextConsoleBox;
            }
        }
        #endregion
        #region Delegates
		/// <summary>
        /// Clear delegates only
        /// </summary> 
        public void BoxDelegatesClear() {
            //
            ItemAdd = null;
            ItemChanged = null;
            //
            Item1Add = null;
            Item2Add = null;
            Item3Add = null;
            Item4Add = null;
            ItemChanged = null;
            Item2Changed = null;
            Item3Changed = null;
            Item4Changed = null;
            //
            TextConsoleAdd = null;
            TextConsoleChanged = null;
        }

		/// <summary>
        /// Reset delegates to point at default implementations.
        /// </summary> 
        public void BoxDelegatesSetDefault(ref StatusUiDef StatusUiPassed) {
            //
            StatusUiPassed.ItemAdd = ItemAdd;
            StatusUiPassed.ItemChanged = ItemChanged;
            //
            if (StatusUiPassed.BoxIsUsed) {
                //
                StatusUiPassed.ItemAdd = BoxAddImpl;
                StatusUiPassed.Item1Add = Box1AddImpl;
                StatusUiPassed.Item2Add = Box2AddImpl;
                StatusUiPassed.Item3Add = Box3AddImpl;
                StatusUiPassed.Item4Add = Box4AddImpl;
                //
                StatusUiPassed.TextConsoleAdd = TextConsoleBoxAddImpl;
                //
                //StatusUiPassed.ItemChanged = BoxChangedImpl;
                //StatusUiPassed.Item1Changed = Box1ChangedImpl;
                //StatusUiPassed.Item2Changed = Box2ChangedImpl;
                //StatusUiPassed.Item3Changed = Box3ChangedImpl;
                //StatusUiPassed.Item4Changed = Box4ChangedImpl;
                //StatusUiPassed.TextConsoleChanged = TextConsoleChangedImpl;
                //
            } else if (LineIsUsed) {
                StatusUiPassed.ItemAdd = LineAddImpl;
                StatusUiPassed.Item1Add = Line1AddImpl;
                StatusUiPassed.Item2Add = Line2AddImpl;
                StatusUiPassed.Item3Add = Line3AddImpl;
                StatusUiPassed.Item4Add = Line4AddImpl;
                //
                StatusUiPassed.TextConsoleAdd = TextConsoleAddImpl;
                //
                //StatusUiPassed.ItemChanged = LineChangedImpl;
                //StatusUiPassed.Item1Changed = Line1ChangedImpl;
                //StatusUiPassed.Item2Changed = Line2ChangedImpl;
                //StatusUiPassed.Item3Changed = Line3ChangedImpl;
                //StatusUiPassed.Item4Changed = Line4ChangedImpl;
                //StatusUiPassed.TextConsoleChanged = TextConsoleChangedImpl;
            } else {
                // error, neither Line nor Box is in use
                // do box management by itself qualify as valid?
            }
        }

		/// <summary>
        /// Copy delegate setting from passed source.
        /// </summary> 
        public void BoxDelegatesCopyFrom(ref StatusUiDef StatusUiPassed) {
            ItemAdd = StatusUiPassed.ItemAdd;
            //
            Item1Add = StatusUiPassed.Item1Add;
            Item2Add = StatusUiPassed.Item2Add;
            Item3Add = StatusUiPassed.Item3Add;
            Item4Add = StatusUiPassed.Item4Add;
            //
            ItemChanged = StatusUiPassed.ItemChanged;
            Item1Changed = StatusUiPassed.Item1Changed;
            Item2Changed = StatusUiPassed.Item2Changed;
            Item3Changed = StatusUiPassed.Item3Changed;
            Item4Changed = StatusUiPassed.Item4Changed;
            //
            TextConsoleAdd = StatusUiPassed.TextConsoleAdd;
            TextConsoleChanged = StatusUiPassed.TextConsoleChanged;
        }

		/// <summary>
        /// Copy delegate settings to passed target.
        /// </summary> 
        public void BoxDelegatesCopyTo(ref StatusUiDef StatusUiPassed) {
            //
            StatusUiPassed.ItemAdd = ItemAdd;
            StatusUiPassed.ItemChanged = ItemChanged;
            //
            StatusUiPassed.Item1Add = Item1Add;
            StatusUiPassed.Item2Add = Item2Add;
            StatusUiPassed.Item3Add = Item3Add;
            StatusUiPassed.Item4Add = Item4Add;
            //
            StatusUiPassed.Item1Changed = Item1Changed;
            StatusUiPassed.Item2Changed = Item2Changed;
            StatusUiPassed.Item3Changed = Item3Changed;
            StatusUiPassed.Item4Changed = Item4Changed;
            //
            StatusUiPassed.TextConsoleAdd = TextConsoleAdd;
            StatusUiPassed.TextConsoleChanged = TextConsoleChanged;
        }
        #endregion
        /// Status Line ////////////////////////////
        ////////////////////////////////////////////
        #region Line Bind
        // Bind to Text Box
		/// <summary>
        /// Bind UI Box Element to passed source.
		/// Not implemented
        /// </summary> 
        public void LineTextBind(int LineBoxNumPassed, TextBox BoxPassed, TextBoxManageDef BoxManagePassed) {
            if (BoxManageIsUsed) {
                // BoxPassed is an instance of a TextBox
                BoxManagePassed.BoxObject = BoxPassed;
                //
                BindingExpression BoxBindingExpression = BoxManagePassed.BoxObject.GetBindingExpression(TextBox.TextProperty);
                //
                if (true == false) {
                    BoxBindingExpression.UpdateSource();
                    BoxBindingExpression.UpdateTarget();
                    Object BindingObject = BoxBindingExpression.DataItem;
                    BindingStatus BindingStatusValue = BoxBindingExpression.Status;
                    // BoxManagePassed.BoxObject.SourceUpdated();
                }
                //
                switch (LineBoxNumPassed) {
                    case (1):
                        // BoxManagePassed.Data .SetBinding(BoxManagePassed.Text, "Line1");
                        //      ..DataBindings.Add("Text", myDataSet, "Suppliers.CompanyName");;
                        //      textBox1.DataBindings.Add("Text", myDataSet, "Suppliers.CompanyName");
                        break;
                    case (2):
                        //BoxManagePassed.DataBindings.Add("Text", myDataSet, "Suppliers.CompanyName");;
                        break;
                    case (3):
                        //BoxManagePassed.DataBindings.Add("Text", myDataSet, "Suppliers.CompanyName");;
                        break;
                    case (4):
                        //BoxManagePassed.DataBindings.Add("Text", myDataSet, "Suppliers.CompanyName");;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
        // Add
        #region Status Line Add / Change
        /// <summary> 
        /// Default Status Line Add
        /// </summary> 
        /// <remarks>
		/// </remarks> 
        public void StatusLineAddImpl(Object Sender, String TextStringPassed) { LineAddImpl(Sender, TextStringPassed); }
        /// <summary> 
        /// Status Line Changed Event
        /// </summary> 
        /// <remarks>
		/// </remarks> 
        public void StatusLineChangedImpl(Object Sender, String TextStringPassed) {
            OnPropertyChanged("Line1");
        }
        //
        /// <summary> 
        /// Line Add Overload
        /// </summary> 
        /// <remarks>
		/// </remarks> 
        public void LineAddImpl(Object Sender, String TextStringPassed) { LineAddImpl(Sender, 1, TextStringPassed); }
        /// <summary> 
        /// Line Add Overload
        /// </summary> 
        /// <remarks>
		/// </remarks> 
        public void LineAddImpl(int LineNumPassed, String TextStringPassed) { LineAddImpl(Sender, LineNumPassed, TextStringPassed); }
        /// <summary> 
        /// Line / Box Add Main Route.
        /// </summary> 
        /// <remarks>
		/// Calls BoxDo or LineDo.
		/// </remarks> 
        public void LineAddImpl(
            Object Sender,
            int LineNumPassed,
            String TextStringPassed
            ) {
            int ResultIntPassed = 99999;
            String PrefixStringPassed = "";
            String SuffixStringPassed = "";
            //
            if (BoxIsUsed) {
                BoxDo(Sender, LineNumPassed, (int)LineActionIs.InsertAfter, ResultIntPassed,
                TextStringPassed, PrefixStringPassed, SuffixStringPassed);
            } else {
                LineDo(Sender, LineNumPassed, (int)LineActionIs.InsertAfter, ResultIntPassed,
                TextStringPassed, PrefixStringPassed, SuffixStringPassed);
            }
        }

        #endregion
        #region Line Add #
        /// <summary> 
        /// Line (1) Add Overload
        /// </summary> 
        /// <remarks>
		/// Calls Line Action Do Main Method
		/// </remarks> 
        public void Line1AddImpl(Object Sender, String TextStringPassed) { LineActionDo(Sender, ref spLine1, ref Box1Manage, (int)LineActionIs.InsertAfter, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// Line (2) Add Overload
        /// </summary> 
        /// <remarks>
		/// Calls Line Action Do Main Method
		/// </remarks> 
        public void Line2AddImpl(Object Sender, String TextStringPassed) { LineActionDo(Sender, ref spLine2, ref Box3Manage, (int)LineActionIs.InsertAfter, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// Line (3) Add Overload
        /// </summary> 
        /// <remarks>
		/// Calls Line Action Do Main Method
		/// </remarks> 
        public void Line3AddImpl(Object Sender, String TextStringPassed) { LineActionDo(Sender, ref spLine3, ref Box3Manage, (int)LineActionIs.InsertAfter, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// Line (4) Add Overload
        /// </summary> 
        /// <remarks>
		/// Calls Line Action Do Main Method
		/// </remarks> 
        public void Line4AddImpl(Object Sender, String TextStringPassed) { LineActionDo(Sender, ref spLine4, ref Box4Manage, (int)LineActionIs.InsertAfter, 99999, TextStringPassed, "", ""); }
        #endregion
        #region Line Get / Set Target
        /// <summary> 
        /// Gets the integer line number.
        /// </summary> 
        /// <remarks>
		/// Included only for a complete design pattern.
		/// </remarks> 
        public int LineNumGet(String LineNamePassed) { try { return ((int)Enum.Parse(typeof(LineNumIs), LineNamePassed)); } catch { return 0; } }
        //
        /// <summary> 
        /// Gets the string name for the line number.
        /// </summary> 
        /// <remarks>
		/// Included only for a complete design pattern.
		/// </remarks> 
        public String LineGet(int LineNumPassed) {
            switch (LineNumPassed) {
                case 99999: return null;
                case (1): return Line1;
                case (2): return Line2;
                case (3):  return Line3;
                case (4): return Line4;

                default: return null;
            }
        }

        /// <summary> 
        /// Default Line Set
        /// </summary> 
        /// <remarks>
		/// Calls BoxDo or LineDo.
		/// </remarks> 
        public void LineSet(int LineNumPassed, String TextStringPassed) {
            if (BoxIsUsed) {
                BoxDo(Sender, LineNumPassed, (int)LineActionIs.SetAll, 99999, TextStringPassed, "", "");
            } else {
                LineDo(Sender, LineNumPassed, (int)LineActionIs.SetAll, 99999, TextStringPassed, "", "");
            }
        }

        /// <summary> 
        /// Main Method for Line Set / Add
		/// when passing a Line Number
        /// </summary> 
        /// <remarks>
		/// Calls Line Action Do Main Method
		/// </remarks> 
        public void LineDo(
            Object Sender,
            int LineNumPassed,
            int ActionPassed, 
            int ResultIntPassed,
            String TextStringPassed,
            String PrefixStringPassed,
            String SuffixStringPassed
            ) {
            switch (LineNumPassed) {
                case 99999:
                    Line2.Insert(1, "Null start");
                    break;
                case (1):
                    LineActionDo(Sender, ref spLine1, ref Box1Manage, ActionPassed, ResultIntPassed,
                    TextStringPassed, PrefixStringPassed, SuffixStringPassed);
                    break;
                case (2):
                    LineActionDo(Sender, ref spLine2, ref Box2Manage, ActionPassed, ResultIntPassed,
                    TextStringPassed, PrefixStringPassed, SuffixStringPassed);
                    break;
                case (3):
                    LineActionDo(Sender, ref spLine3, ref Box3Manage, ActionPassed, ResultIntPassed,
                    TextStringPassed, PrefixStringPassed, SuffixStringPassed);
                    break;
                case (4):
                    LineActionDo(Sender, ref spLine4, ref Box4Manage, ActionPassed, ResultIntPassed,
                    TextStringPassed, PrefixStringPassed, SuffixStringPassed);
                    break;
                case (5):
                    LineActionDo(Sender, ref spTextConsole, ref TextConsoleManage, ActionPassed, ResultIntPassed,
                    TextStringPassed, PrefixStringPassed, SuffixStringPassed);
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region Line Action Do
        /// <summary> 
        /// Informs Line / Box Action Do what
		/// actions is being performed (Set, Add, Insert, etc.)
        /// </summary> 
        [Flags]
        public enum LineActionIs : int {
            InsertAfter = 1,
            InsertBefore = 2,
            SetLine = 3,
            SetAll = 4,
            Find = 5,
            NotSet = 99999,
        }

        /// <summary> 
        /// Line Action Do Overload (Default call).
        /// </summary> 
        public void LineActionDo(int LineNumPassed, String TextStringPassed) { LineActionDo(Sender, ref spLine1, ref Box1Manage, (int)LineActionIs.InsertAfter, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// Main Method for all changes to a line (text block).
        /// </summary> 
        /// <param name="Sender">UI object sending the action request</param> 
        /// <param name="LinePassed">Line (text block) being altered.</param> 
        /// <param name="BoxManagePassed">Box UI Management Object</param> 
        /// <param name="ActionPassed">Set, Add, Insert text.</param> 
        /// <param name="ResultIntPassed">Result code to include formatting.</param> 
        /// <param name="TextStringPassed">Text to add or set line to.</param> 
        /// <param name="PrefixStringPassed">Prefix to use with Text.</param> 
        /// <param name="SuffixStringPassed">Suffix to use with Text.</param> 
        /// <remarks>
		/// </remarks> 
        public void LineActionDo(
            Object Sender,
            ref String LinePassed,
            ref TextBoxManageDef BoxManagePassed,
            int ActionPassed,
            int ResultIntPassed,
            String TextStringPassed,
            String PrefixStringPassed,
            String SuffixStringPassed
            ) {
            //
            String TempString = LinePrefixSuffixBuild(ResultIntPassed, TextStringPassed, PrefixStringPassed, SuffixStringPassed);
            //
            switch (ActionPassed) {
                case ((int)LineActionIs.NotSet):
                    LinePassed.Insert(1, "Null start");
                    break;
                case ((int)LineActionIs.InsertAfter):
                    //if (LinePassed.Length > 0) {
                    //    LinePassed += "\n";
                    //}
                    LinePassed += TempString;
                    break;
                case ((int)LineActionIs.InsertBefore):
                    if (LinePassed.Length > 0) {
                        //LinePassed = TempString + "\n" + LinePassed;
                        LinePassed = TempString + LinePassed;
                    } else {
                        LinePassed = TempString;
                    }
                    break;
                case ((int)LineActionIs.SetAll):
                    LinePassed = TempString;
                    break;
                case ((int)LineActionIs.SetLine):
                    // not implemented
                default:
                    // InsertAfter
                    //if (LinePassed.Length > 0) {
                    //    LinePassed += "\n";
                    //}
                    LinePassed += TempString;
                    break;
            }
            //LinePassed += "\n";
        }
        #endregion
        #region Line Prefix / Suffix
        /// <summary> 
        /// Performs predefined formatting using the line
		/// text, suffix, prefix and result code for Line and
		/// Box Action Do.
        /// </summary> 
        /// <remarks>
		/// Used when there is a standard margin of information
		/// independent of the text output being sent.
		/// </remarks> 
        private String LinePrefixSuffixBuild(
            int ResultIntPassed,
            String TextStringPassed,
            String PrefixStringPassed,
            String SuffixStringPassed
            ) {
            String TempString = "";
            if (PrefixStringPassed.Length > 0) { TempString = PrefixStringPassed; }
            TempString += TextStringPassed;
            if (SuffixStringPassed.Length > 0) { TempString += SuffixStringPassed; }
            if (ResultIntPassed != 99999) { TempString += " (" + ResultIntPassed + ")"; }
            return TempString;
        }
        #endregion
        //////////////////////////////////////////////////
        //////////////////////////////////////////////////
        #region Weak Even Listner
        //bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e) {
        //    if (managerType == typeof(LineAddChangeEventManager)) {
        //        OnLineAddChange(sender, (LineChangeEventArgs)e);
        //    } else if (managerType == typeof(LineSetChangeEventManager)) {
        //        OnLineSetChange(sender, (LineChangeEventArgs)e);
        //    } else {
        //        return false;       // unrecognized event
        //    }
        //    return true;
        //}
        //public void OnLineAddChange(object sender, LineChangeEventArgs e) {
        //    //do something here...
        //}
        //public void OnLineSetChange(object sender, LineChangeEventArgs e) {
        //    //do something here...
        //}
        #endregion
        #region Status Messages Set
        // Return Codes
        /// <summary> 
        /// (Obsolete) Use StatusUi methods to
		/// load descriptions for result codes.
        /// </summary> 
        /// <remarks>
		/// Needs final code check to confirms result
		/// code descriptions switched to new method.
		/// </remarks> 
        public void MessageSet_11(int ResultIntPassed, ref TextBox StatusUiPassed) {
            StatusUiPassed.Text = "";
            switch (ResultIntPassed) {
                case 99999:
                    StatusUiPassed.Text += "Null start";
                    break;
                case 99:
                    StatusUiPassed.Text += "File must have a value";
                    break;
                case 11:
                    StatusUiPassed.Text += "File not found";
                    break;
                case 12:
                    StatusUiPassed.Text += "File already exists";
                    break;
                case 21:
                    StatusUiPassed.Text += "Item Id not found";
                    break;
                case 22:
                    StatusUiPassed.Text += "Item Id already exists";
                    break;
                default:
                    StatusUiPassed.Text += "Unknown error" + " (" + ResultIntPassed + ")";
                    break;
            }
        }
        #endregion
        /// Line To / From Box  /////////////////////////
        //////////////////////////////////////////////////
        #region Status Line Copy
        /// <summary> 
        /// Copies text from passed object to
		/// Lines or Box Text.
        /// </summary> 
        public void LineCopyFrom(ref StatusUiDef StatusUiPassed) {
            if (LineIsUsed) {
                Line1 = StatusUiPassed.Line1.ToString();
                //Line1 = StatusUiPassed.Line1;
                //StatusUiPassed.Line1 += "???";
                Line2 = StatusUiPassed.Line2.ToString();
                //StatusUiPassed.Line2 += "xxx";
                Line3 = StatusUiPassed.Line3.ToString();
                Line4 = StatusUiPassed.Line4.ToString();
            }
            if (BoxIsUsed) {
                Box1.Text = StatusUiPassed.Box1.Text;
                Box2.Text = StatusUiPassed.Box2.Text;
                Box3.Text = StatusUiPassed.Box3.Text;
                Box4.Text = StatusUiPassed.Box4.Text;
            }
        }
        /// <summary> 
        /// Copies text from Lines or Box Text.
        /// </summary> 
        public void LineCopyTo(ref StatusUiDef StatusUiPassed) {
            if (LineIsUsed) {
                StatusUiPassed.Line1 = Line1.ToString();
                StatusUiPassed.Line2 = Line2.ToString();
                StatusUiPassed.Line3 = Line3.ToString();
                StatusUiPassed.Line4 = Line4.ToString();
            }
            if (BoxIsUsed) {
                StatusUiPassed.Box1.Text = Box1.Text;
                StatusUiPassed.Box2.Text = Box2.Text;
                StatusUiPassed.Box3.Text = Box3.Text;
                StatusUiPassed.Box4.Text = Box4.Text;
            }
        }
        #endregion
        #region Line To / From Box
        // Line Box Text Handling To / From
        /// <summary> 
        /// Add LineBoxPassed text to Line.
        /// </summary> 
        public void LineAddFromBox(int LineNumPassed, TextBox LineBoxPassed) { LineAddImpl(LineNumPassed, LineBoxPassed.Text); }
        /// <summary> 
        /// Set Line text to LineBoxPassed text.
        /// </summary> 
        public void LineSetFromBox(int LineNumPassed, TextBox LineBoxPassed) { LineSet(LineNumPassed, LineBoxPassed.Text); }
        // Get / Set Text Box by number
        /// <summary> 
        /// Set LineBoxPassed text to Line text.
        /// </summary> 
        public void BoxSetToLine(int LineNumPassed, TextBox LineBoxPassed) { LineBoxPassed.Text = LineGet(LineNumPassed); }
        /// <summary> 
        /// Set Line text to LineBoxPassed text.
        /// </summary> 
        public void LineSetToBox(int LineNumPassed, TextBox LineBoxPassed) { LineSet(LineNumPassed, LineBoxPassed.Text); }
        #endregion
        #region Box Object Get from String / Line #
        /// <summary> 
        /// Returns the requested object for the Box Number.
        /// </summary> 
        protected TextBox BoxObjectGet(string LineNamePassed) {
            int LineNumPassed = LineNumGet(LineNamePassed);
            return BoxObjectGet(LineNumPassed);
        }
        /// <summary> 
        /// Return UI Box element of passed box number.
        /// </summary> 
        protected TextBox BoxObjectGet(int LineNumPassed) {
            if (!BoxManageIsUsed) { return null; }
            switch (LineNumPassed) {
                case 99999:
                    return null;
                case ((int)LineNumIs.Line1):
                    return Box1Manage.BoxObject;
                case ((int)LineNumIs.Line2):
                    return Box2Manage.BoxObject;
                case ((int)LineNumIs.Line3):
                    return Box3Manage.BoxObject;
                case ((int)LineNumIs.Line4):
                    return Box4Manage.BoxObject;
                case ((int)LineNumIs.TextConsole):
                    return TextConsoleManage.BoxObject;
                default:
                    return null;
            }
        }
        #endregion
        /// Box       //////////////////////////////
        ////////////////////////////////////////////
        #region Box
        #region Box Copy (All)
        /// <summary> 
        /// Set Box UI Element to passed source.
        /// </summary> 
        public void BoxCopyFrom(ref StatusUiDef StatusUiPassed) {
            if (BoxIsUsed) {
                Box1 = StatusUiPassed.Box1;
                Box2 = StatusUiPassed.Box2;
                Box3 = StatusUiPassed.Box3;
                Box4 = StatusUiPassed.Box4;
            }
            if (BoxManageIsUsed) {
                Box1Manage = StatusUiPassed.Box1Manage;
                Box2Manage = StatusUiPassed.Box2Manage;
                Box3Manage = StatusUiPassed.Box3Manage;
                Box4Manage = StatusUiPassed.Box4Manage;
            }
        }
        /// <summary> 
        /// Set Box UI Element to passed target.
        /// </summary> 
        public void BoxCopyTo(ref StatusUiDef StatusUiPassed) {
            if (BoxIsUsed) {
                StatusUiPassed.Box1 = Box1;
                StatusUiPassed.Box2 = Box2;
                StatusUiPassed.Box3 = Box3;
                StatusUiPassed.Box4 = Box4;
            }
            if (BoxManageIsUsed) {
                Box1Manage = StatusUiPassed.Box1Manage;
                Box2Manage = StatusUiPassed.Box2Manage;
                Box3Manage = StatusUiPassed.Box3Manage;
                Box4Manage = StatusUiPassed.Box4Manage;
            }
        }
        #endregion
        #region Box Handling Add / Set Impl
        /// <summary> 
        /// Box Add overload
        /// </summary> 
        public void BoxAddImpl(String TextStringPassed) { BoxAddImpl(Sender, 1, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// Box Add overload
        /// </summary> 
        public void BoxAddImpl(Object Sender, String TextStringPassed) { BoxAddImpl(Sender, 1, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// Box Add overload
        /// </summary> 
        public void BoxAddImpl(Object Sender, int BoxNumPassed, String TextStringPassed ) { BoxAddImpl(Sender, BoxNumPassed, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// Box Add overload
        /// </summary> 
        public void BoxAddImpl(int BoxNumPassed, String TextStringPassed) { BoxAddImpl(Sender, BoxNumPassed, 99999, TextStringPassed, "", ""); }

        /// <summary> 
        /// Box Add overload
        /// </summary> 
        public void BoxAddImpl(
            Object Sender, 
            int BoxNumPassed, 
            int ResultIntPassed, 
            String TextStringPassed, 
            String PrefixStringPassed, 
            String SuffixStringPassed
            ) {
            String TempString = "";
            if (BoxIsUsed) {
                BoxDo(Sender, BoxNumPassed, (int)LineActionIs.InsertAfter, ResultIntPassed,
                TextStringPassed, PrefixStringPassed, SuffixStringPassed);
            } else {
                LineDo(Sender, BoxNumPassed, (int)LineActionIs.InsertAfter, ResultIntPassed,
                TextStringPassed, PrefixStringPassed, SuffixStringPassed);
            }
        }

        /// <summary> 
        /// Box Set overload
        /// </summary> 
        public void BoxSetImpl(String TextStringPassed) { BoxSetImpl(Sender, 1, TextStringPassed); }
        /// <summary> 
        /// Box Set overload.
        /// </summary> 
        public void BoxSetImpl(Object Sender, String TextStringPassed) { BoxSetImpl(Sender, 1, TextStringPassed); }
        /// <summary> 
        /// Box Set overload
        /// </summary> 
        public void BoxSetImpl(Object Sender, int BoxNumPassed, String TextStringPassed) {
            // default messages go to Text1
            Box1SetImpl(Sender, TextStringPassed);
            if (BoxIsUsed) {
                BoxDo(Sender, BoxNumPassed, (int)LineActionIs.SetAll, 99999, TextStringPassed, "", "");
            } else {
                LineDo(Sender, BoxNumPassed, (int)LineActionIs.SetAll, 99999, TextStringPassed, "", "");
            }
        }
        //////////////////////////////////////////////////
        //////////////////////////////////////////////////
        /// <summary> 
        /// Check Width and Height of box for adjustment
        /// </summary> 
        /// <remarks>
		/// </remarks> 
        public void CheckDisplayMax(ref TextBox BoxPassed, ref TextBoxManageDef BoxManagePassed) {
            if (BoxManageIsUsed) {
                while (BoxPassed.Text.Length > Box1Manage.DisplayMaxChars) {
                    BoxPassed.Text = BoxPassed.Text.Substring(0, (int)BoxManagePassed.DisplayMaxCharsToKeep);
                }
            }
        }
        #endregion
        //////////////////////////////////////////////////
        //////////////////////////////////////////////////
        #region Action the Box
        /// <summary> 
        /// Main Method for Setting Box Text where Box Number is passed.
        /// </summary> 
        /// <remarks>
		/// </remarks> 
        public void BoxDo(
            Object Sender,
            int BoxNumPassed,
            int ActionPassed,
            int ResultIntPassed,
            String TextStringPassed,
            String PrefixStringPassed,
            String SuffixStringPassed
            ) {
            switch (BoxNumPassed) {
                case 99999:
                    Box2.Text.Insert(1, "Null start");
                    break;
                case (1):
                    BoxActionDo(Sender, ref Box1, ref Box1Manage, ActionPassed, ResultIntPassed,
                    TextStringPassed, PrefixStringPassed, SuffixStringPassed);
                    break;
                case (2):
                    BoxActionDo(Sender, ref Box2, ref Box2Manage, ActionPassed, ResultIntPassed,
                    TextStringPassed, PrefixStringPassed, SuffixStringPassed);
                    break;
                case (3):
                    BoxActionDo(Sender, ref Box3, ref Box3Manage, ActionPassed, ResultIntPassed,
                    TextStringPassed, PrefixStringPassed, SuffixStringPassed);
                    break;
                case (4):
                    BoxActionDo(Sender, ref Box4, ref Box4Manage, ActionPassed, ResultIntPassed,
                    TextStringPassed, PrefixStringPassed, SuffixStringPassed);
                    break;
                case (5):
                    BoxActionDo(Sender, ref TextConsoleBox, ref TextConsoleManage, ActionPassed, ResultIntPassed,
                    TextStringPassed, PrefixStringPassed, SuffixStringPassed);
                    break;
                default:
                    break;
            }
        }

        /// <summary> 
        /// Box Action Do overload.
        /// </summary> 
        public void BoxActionDo(int BoxNumPassed, String TextStringPassed ) { BoxActionDo(Sender, ref Box1, ref Box1Manage, (int)LineActionIs.InsertAfter, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// Main Method for Setting Box Text where Box Target is passed.
        /// </summary> 
        /// <param name="Sender">UI object sending the action request</param> 
        /// <param name="BoxPassed">Box (Text Box) being altered.</param> 
        /// <param name="BoxManagePassed">Box UI Management Object</param> 
        /// <param name="ActionPassed">Set, Add, Insert text.</param> 
        /// <param name="ResultIntPassed">Result code to include formatting.</param> 
        /// <param name="TextStringPassed">Text to add or set line to.</param> 
        /// <param name="PrefixStringPassed">Prefix to use with Text.</param> 
        /// <param name="SuffixStringPassed">Suffix to use with Text.</param> 
        public void BoxActionDo(
            Object Sender,
            ref TextBox BoxPassed,
            ref TextBoxManageDef BoxManagePassed,
            int ActionPassed,
            int ResultIntPassed,
            String TextStringPassed,
            String PrefixStringPassed,
            String SuffixStringPassed
            ) {
            //
            String TempString = LinePrefixSuffixBuild(ResultIntPassed, TextStringPassed, PrefixStringPassed, SuffixStringPassed);
            //
            switch (ActionPassed) {
                case ((int)LineActionIs.NotSet):
                    BoxPassed.Text.Insert(1, "Null start");
                    break;
                case ((int)LineActionIs.InsertAfter):
                    //if (BoxPassed.Text.Length > 0) {
                    //    BoxPassed.Text += "\n";
                    //}
                    BoxPassed.Text += TempString;
                    break;
                case ((int)LineActionIs.InsertBefore):
                    if (BoxPassed.Text.Length > 0) {
                        //BoxPassed.Text = TempString + "\n" + BoxPassed.Text;
                        BoxPassed.Text = TempString + BoxPassed.Text;
                    } else {
                        BoxPassed.Text = TempString;
                    }
                    break;
                case ((int)LineActionIs.SetAll):
                    BoxPassed.Text = TempString;
                    break;
                case ((int)LineActionIs.SetLine):
                // not implemented
                default:
                    // InsertAfter
                    //if (BoxPassed.Text.Length > 0) {
                    //    BoxPassed.Text += "\n";
                    //}
                    BoxPassed.Text += TempString;
                    break;
            }
            //
            if (BoxManageIsUsed) {
                CheckDisplayMax(ref BoxPassed, ref BoxManagePassed);
                if (true == false) {
                    if (BoxManagePassed.ScrollDo) { BoxPassed.ScrollToHome(); }
                } else {
                    if (BoxManagePassed.ScrollDo) { BoxPassed.ScrollToEnd(); }
                }
                // if (XUomCovvXv.RunErrorDidOccur) { this.BringIntoView(); }
            }
        }
        //////////////////////////////////////////////////
        //////////////////////////////////////////////////
        #endregion
        #region Box Set by Box #
        /// <summary> 
        /// Set Console Text overload.
        /// </summary> 
        public void Box1SetImpl(Object SenderPassed, String TextStringPassed) { BoxSetImpl(Sender, 1, TextStringPassed); }
        /// <summary> 
        /// Add Console Text overload.
        /// </summary> 
        public void Box1AddImpl(Object SenderPassed, String TextStringPassed) { BoxAddImpl(Sender, 1, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// Set Console Text overload.
        /// </summary> 
        public void Box2SetImpl(Object SenderPassed, String TextStringPassed) { BoxSetImpl(Sender, 2, TextStringPassed); }
        /// <summary> 
        /// Add Console Text overload.
        /// </summary> 
        public void Box2AddImpl(Object SenderPassed, String TextStringPassed) { BoxAddImpl(Sender, 2, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// Set Console Text overload.
        /// </summary> 
        public void Box3SetImpl(Object SenderPassed, String TextStringPassed) { BoxSetImpl(Sender, 3, TextStringPassed); }
        /// <summary> 
        /// Add Console Text overload.
        /// </summary> 
        public void Box3AddImpl(Object SenderPassed, String TextStringPassed) { BoxAddImpl(Sender, 3, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// Set Console Text overload.
        /// </summary> 
        public void Box4SetImpl(Object SenderPassed, String TextStringPassed) { BoxSetImpl(Sender, 4, TextStringPassed); }
        /// <summary> 
        /// Add Console Text overload.
        /// </summary> 
        public void Box4AddImpl(Object SenderPassed, String TextStringPassed) { BoxAddImpl(Sender, 4, 99999, TextStringPassed, "", ""); }
        #endregion
        #endregion
        /// Console //////////////////////////////
        ////////////////////////////////////////////
        #region TextConsole
        #region TextConsole Add
        // Add
        /// <summary> 
        /// Add Console Text overload.
        /// </summary> 
        public void ConsoleAddImpl(Object SenderPassed, String TextStringPassed) { TextConsoleAddImpl(Sender, (int)LineActionIs.InsertAfter, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// Add Console Text overload.
        /// </summary> 
        public void TextConsoleAddImpl(Object Sender, String TextStringPassed) {TextConsoleAddImpl(Sender, (int)LineActionIs.InsertAfter, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// Add Console Text overload.
        /// </summary> 
        public void TextConsoleAddImpl(int ActionPassed, String TextStringPassed) { TextConsoleAddImpl(Sender, (int)LineActionIs.InsertAfter, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// Main Method for Adding Console Text.
		/// Routes changes through LineActionDo / BoxActionDo
        /// </summary> 
        public void TextConsoleAddImpl(
            Object Sender,
            int ActionPassed,
            int ResultIntPassed,
            String TextStringPassed,
            String PrefixStringPassed,
            String SuffixStringPassed
            ) {
            if (!BoxIsUsed) {
                LineActionDo(Sender, ref spTextConsole, ref TextConsoleManage, ActionPassed, ResultIntPassed,
                TextStringPassed, PrefixStringPassed, SuffixStringPassed);
            } else {
                BoxActionDo(Sender, ref TextConsoleBox, ref TextConsoleManage, ActionPassed, ResultIntPassed,
                TextStringPassed, PrefixStringPassed, SuffixStringPassed);
            }
        }

        #region TextConsoleBox Box Text Handling To / From
        //
        /// <summary> 
        /// Set Console Text to passed text overload.
        /// </summary> 
        public void TextConsoleBoxSetImpl(Object Sender, String TextStringPassed) { TextConsoleAddImpl(Sender, (int)LineActionIs.SetAll, 99999, TextStringPassed, "", ""); }
        /// <summary> 
        /// (Temporary) Main Method for Adding Console Text.
		/// This was not switched over to route through
		/// LineActionDo / BoxActionDo
        /// </summary> 
        public void TextConsoleBoxAddImpl(Object SenderPassed, String PassedText) {
            if (true == false) {
                // TextConsoleBox.Text = TextStringPassed + "\n" + TextConsoleBox.Text;
                TextConsoleBox.Text = PassedText + TextConsoleBox.Text;
                CheckDisplayMax(ref TextConsoleBox, ref TextConsoleManage);
                if (TextConsoleManage.ScrollDo) { TextConsoleBox.ScrollToHome(); }
            } else {
                TextConsoleBox.Text += PassedText;
                CheckDisplayMax(ref TextConsoleBox, ref TextConsoleManage);
                if (TextConsoleManage.ScrollDo) { TextConsoleBox.ScrollToEnd(); }
            }
            // if (XUomCovvXv.RunErrorDidOccur) { this.BringIntoView(); }
        }

        /// <summary> 
        /// Add contents of Text Box to Console Text.
        /// </summary> 
        public void TextConsoleAddFromBox(int TextConsoleBoxNumPassed, TextBox TextConsoleBoxPassed) {
            switch (TextConsoleBoxNumPassed) {
                case (1):
                default:
                    TextConsole += TextConsoleBoxPassed.Text;
                    break;
            }
        }
        // Get / Set Text Box by number
        /// <summary> 
        /// Set Text Box text to Console Text contents.
        /// </summary> 
        public void BoxSetToTextConsole(int TextConsoleBoxNumPassed, TextBox TextConsoleBoxPassed) {
            switch (TextConsoleBoxNumPassed) {
                case (1):
                default:
                    TextConsoleBoxPassed.Text = TextConsole;
                    break;
            }
        }
        /// <summary> 
        /// Set Console Text to Text Box text.
        /// </summary> 
        public void TextConsoleSetToBox(int TextConsoleBoxNumPassed, TextBox TextConsoleBoxPassed) {
            switch (TextConsoleBoxNumPassed) {
                case (1):
                default:
                    TextConsole = TextConsoleBoxPassed.Text;
                    break;
            }
        }
        #endregion
        #endregion
        #region TextConsoleBox Set
        /// <summary> 
        /// Change Console Text overload.
        /// </summary> 
        public void TextConsoleSet(
            int ActionPassed,
            String TextStringPassed
            ) {
            int ResultIntPassed = 0;
            String PrefixStringPassed = "";
            String SuffixStringPassed = "";
            //
            TextConsoleSet(Sender, ActionPassed, ResultIntPassed,
            TextStringPassed, PrefixStringPassed, SuffixStringPassed);
        }

        /// <summary> 
        /// (Temporary) Main Method for Setting Console Text.
		/// This was not switched over to route through
		/// LineActionDo / BoxActionDo
        /// </summary> 
        public void TextConsoleSet(
            Object Sender,
            int ActionPassed,
            int ResultIntPassed,
            String TextStringPassed,
            String PrefixStringPassed,
            String SuffixStringPassed
            ) {
            //
            String TempString = LinePrefixSuffixBuild(ResultIntPassed, TextStringPassed, PrefixStringPassed, SuffixStringPassed);
            //
            switch (ActionPassed) {
                case ((int)LineActionIs.NotSet):
                    TextConsole.Insert(1, "Null start");
                    break;
                case ((int)LineActionIs.InsertAfter):
                    //if (TextConsole.Length > 0) {
                    //    TextConsole += "\n";
                    //}
                    TextConsole += TempString;
                    break;
                case ((int)LineActionIs.InsertBefore):
                    if (TextConsole.Length > 0) {
                        // TextConsole = TempString + "\n" + TextConsole;
                        TextConsole = TempString + TextConsole;
                    } else {
                        TextConsole = TempString;
                    }
                    break;
                case ((int)LineActionIs.SetAll):
                    TextConsole = TempString;
                    break;
                default:
                    // ((int)LineActionIs.InsertAfter)
                    //if (TextConsole.Length > 0) {
                    //    TextConsole += "\n";
                    //}
                    TextConsole += TempString;
                    break;
            }
            // TextConsole += TextConsole + "\n";
        }
        #endregion
        #region Status Line Copy
        /// <summary> 
        /// Copy Console Text from passed source.
        /// </summary> 
        public void TextConsoleCopyFrom(ref StatusUiDef StatusUiPassed) {
            if (LineIsUsed) {
                TextConsole = StatusUiPassed.TextConsole.ToString();
            }
            if (BoxIsUsed) {
                TextConsoleBox.Text = StatusUiPassed.TextConsoleBox.Text;
            }
        }
        /// <summary> 
        /// Copy Console text to passed target.
        /// </summary> 
        public void TextConsoleCopyTo(ref StatusUiDef StatusUiPassed) {
            if (LineIsUsed) {
                StatusUiPassed.TextConsole = TextConsole.ToString();
            }
            if (BoxIsUsed) {
                StatusUiPassed.TextConsoleBox.Text = TextConsoleBox.Text;
            }
        }
        #endregion
        #endregion
        #endregion
    }
}