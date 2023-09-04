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
// ToDo $$MAJOR 1) Create TraceMdm Aurgument structure
// ToDo $$MAJOR 2) Create Indexer for RunAction and remove redundant settings
// ToDo $$MAJOR 2) should use indexers passed to set RunAction and RunMetric values
// ToDo $$MAJOR 2) which will reduce lines of code.
// ToDo $$MAJOR 4) Don't pass run action values to TraceMdm but use current value.
// ToDo $$MAJOR 5) Implement TLD for data after testing dict to schema code.

namespace Mdm.Oss.Std
{
    /// <summary> 
    /// <para>
    /// See: StdBaseDef Summary
    /// The documentation can be found in the summary for
    /// the extended standard Object StdBaseDef.
    /// Normally you will instantiate this
    /// although utility apps might inherit it.
    /// It is designed to work either way.
    /// </para>
    /// <para>(StdBaseRunFileDef) Console implements the
    /// Trace, Logging, Console and messaging 
    /// operations.</para>
    /// <para> . </para>
    /// <para> See the Programming Standards ReadMe (HUH?????)
    /// Next: This is the top level Console class.
    /// </para>
    /// </summary> 
    public partial class StdConsoleManagerDef : StdBaseRunFilePrinterConsoleDef, iTrace, AppStd, iClassFeatures, IDisposable
    {
        #region STATIC FUNCTIONS
        public static StateIs ConsoleComponentInitialize(ref object SenderPassed , ref StdConsoleManagerDef stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        {
            StateIs ConsoleComponentInitializeResult = StateIs.Started;
            if (stPassed == null) {
                stPassed = ConsoleComponentCreate(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed);
            } else
            {
                stPassed.InitializeStd(ref SenderPassed, ref stPassed.SenderIsThis, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed);
                stPassed.ClassFeaturesFlagsSet(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed);
            }
            return ConsoleComponentInitializeResult = StateIs.Finished;
        }
        public static StdConsoleManagerDef ConsoleComponentCreate(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        {
            StdConsoleManagerDef stNew = new StdConsoleManagerDef(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
            { ConsoleVerbosity = 7 };
            //stNew.Initialize();
            StateIs LocalStatus = StdConsoleManagerDef.ConsoleComponentInitialize(ref stNew.Sender, ref stNew, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed);
            // st.ClassFeaturesFlagsSet(ClassFeaturesIsThis);
            return stNew;
        }
        public static StateIs ConsoleComponentFeaturesInitialize(ref StdConsoleManagerDef stPassed)
        {
            #region Framework Features Used
            stPassed.ClassRole = ClassRoleIs.RoleAsUtility;
            // this isn't perfect but can be tweaked when needed. (defaults?)
            stPassed.ClassFeatures =
            ClassFeatureIs.MaskUi
            | ClassFeatureIs.MaskButton
            | ClassFeatureIs.MaskStautsUiAsBox
            | ClassFeatureIs.Window
            | ClassFeatureIs.MdmUtilConsole;
            //
            //st = new StdConsoleManagerDef(ConsoleSource, ClassRole, ClassFeatures)
            //{ ConsoleVerbosity = 7 };
            stPassed.ClassFeaturesFlagsSet(stPassed.ConsoleSource, stPassed.ClassRole, stPassed.ClassFeatures);
            #endregion
            // Form Controls are null.
            // They do a lazy load when the user decides to
            // display any given State (Status) DataGridView.
            //
            // InitializeComponentForm();
            return StateIs.Finished;
        }
        #endregion
        #region Standard Objects
        public bool XUomUrvvXvCreateNow;
        public bool XUomClvvXvCreateNow;
        // Text Stream IOException
        #endregion
        #region Fields
        // Current Console (pointer to App object)
        // st = new StdConsoleManagerDef(ClassRoleIs.None, ClassFeatureIs.None);
        // [ThreadStatic]
        public new StdConsoleManagerDef st;
        // Current Message
        [ThreadStatic]
        public static mMsgDetailsDef Message;
        public object DgvLock;
        #endregion
        #region UI objects, Status Line, Page, Progress Bar
        // StdConsoleManagerDef Status Lines, currently text boxes
        // public StatusUiDef StatusUi;
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
        #region Mdm Standard Io Objects - Streams, readers and writers
        // <Area Id = "ConsoleObject">
        public TextWriter ocotConsoleWriter;
        // public TextWriter ocotStandardOutput;
        public TextReader ocitConsolReader;
        public StreamWriter ocoConsoleWriter;
        // public StreamWriter ocoStandardOutput;
        public StreamReader ociConsolReader;
        // public StreamWriter ocetErrorWriter;
        public IOException eIoe;
        public TextWriter ocetErrorWriter;
        //
        #endregion
        #region Console
        public new ref object ConsoleGetTo(ref object stPassed)
        {
            stPassed = ref ConsoleSender;
            return ref ConsoleSender;
        }
        public new StateIs ConsoleSetFrom(ref object stPassed)
        {
            st = stPassed as StdConsoleManagerDef; base.st = st; ConsoleSender = st;
            return StateIs.Finished;
        }
        public virtual ref StdConsoleManagerDef ConsoleGetTo(ref StdConsoleManagerDef stPassed)
        {
            stPassed = ref st;
            return ref st;
        }
        public virtual StateIs ConsoleSetFrom(ref StdConsoleManagerDef stPassed)
        {
            // StdConsoleSet(new StdConsoleManagerDef(), new ClassRoleIs(), new ClassFeatureIs());
            st = stPassed as StdConsoleManagerDef; base.st = st; ConsoleSender = st;
            return StateIs.Finished;
        }
        #endregion
        #region Console Mdm Std_Io
        // Console
        public StateIs ConsoleOpenResult;
        public StateIs ConsoleCloseResult;
        public StateIs ConsolRoutedEventdResult;
        public StateIs ConsoleWriteResult;
        /// <summary>
        /// Opens the STD Console
        /// <param name=sEmpty></param> 
        public StateIs ConsoleMdmStd_IoOpenImpl()
        {
            ConsoleOpenResult = StateIs.Started;
            //
            try
            {
                if (!ConsoleOn & !ConsoleToDisc & !DoLogActivity) { return ConsoleOpenResult; }
                if (DoLogActivity) { ConsoleToDisc = bON; };
                //
                // Text Version
                // ocoConsoleWriter = Console.Out;
                // ociConsolRoutedEventder = Console.In;
                // Stream Version
                // ocoConsoleWriter = new StreamWriter(StdBaseDef.DriveOs + "\\Users\\Public\\Desktop\\$ ToDo Dgh MdmSrt Project\\Mdm\\Mdm1\\MinputTldConsoleOut.txt");
                // ocoConsoleWriter.AutoFlush = bON;
                //
                //
                try
                {
                    if (ConsoleApplication)
                    {
                        try
                        {
                            ocotConsoleWriter = System.Console.Out;
                            // ocotConsoleWriter = new TextWriter(Console.OpenStandardOutput());
                            ocitConsolReader = System.Console.In;
                            // ocitConsolRoutedEventder = new StreamReader(Console.OpenStandardInput());
                            System.Console.Title = sProcessHeading;
                            System.Console.SetWindowSize(400, 200);
                            System.Console.SetWindowPosition(100, 100);
                        }
                        catch
                        {
                            ConsoleToDisc = bYES;
                            ConsoleApplication = bNO;
                        }
                        //
                    }
                    // ToDo ConsoleMdmStd_IoOpenImpl Folder and File Creation
                    if (ConsoleToDisc)
                    {
                        try
                        {
                            ocoConsoleWriter = new StreamWriter(StdBaseDef.DriveOs + "\\Logs\\Console\\MinputTldConsoleOut.txt");
                            System.Console.SetOut(ocoConsoleWriter);
                        }
                        catch
                        {
                            ocotConsoleWriter = System.Console.Out;
                        }
                        try
                        {
                            ociConsolReader = new StreamReader(StdBaseDef.DriveOs + "\\Logs\\Console\\MinputTldConsoleIn.txt");
                            System.Console.SetIn(ociConsolReader);
                        }
                        catch
                        {
                            ocitConsolReader = System.Console.In;
                        }
                    }
                }
                catch
                {
                    //ConsoleToDisc = bYES;
                    //ConsoleApplication = bNO;
                }
                //
                if (!ConsoleToDisc) { System.Console.Beep(); }
                LocalMessage.Msg8 = "Finished opening Console...";

                System.Console.WriteLine(LocalMessage.Msg8);
                if ((ConsoleOn & (ConsoleTextOn | ConsoleBasicOn | ConsoleToDisc | ConsoleToControl))
                    || TraceOn || TraceBug || TraceDebugOn || TraceBreakOnAll)
                {
                   st.TraceMdmDoDetailed(ConsoleFormUses.DebugLog, 7, ref Sender, bIsMessage, iNoOp, iNoOp, ConsoleOpenResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg8);
                }

                if (!ConsoleToDisc)
                {
                    LocalMessage.Msg8 = "Press any key when ready... ";
                    System.Console.WriteLine(LocalMessage.Msg8);
                    if (TraceOn || ConsoleOn || ConsoleBasicOn || TraceBug || TraceDebugOn || TraceBreakOnAll) {st.TraceMdmDoDetailed(ConsoleFormUses.ErrorLog, 1, ref Sender, bIsMessage, iNoOp, iNoOp, ConsoleOpenResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageEnterAnyKey, LocalMessage.Msg8); }
                    // sTemp1 = Console.ReadLine();
                    ConsoleKeyInfo ckiTemp1 = System.Console.ReadKey();
                }
                //
                // ocoStandardOutput = new StreamWriter(Console.OpenStandardOutput());
                // ocoConsoleWriter = new StreamWriter(Console.OpenStandardOutput());
                // ocoConsoleWriter = new StreamWriter(StdBaseDef.DriveOs + "\\Users\\Public\\Desktop\\$ ToDo Dgh MdmSrt Project\\Mdm\\Mdm1\\MinputTldConsoleOut.txt");
                // Console.OpenStandardOutput(5000);
                //
                // ocoStandardOutput.AutoFlush = bON;
                // Console.SetOut(ocoStandardOutput);
            }
            catch (IOException eIoe)
            {
                ConsoleOpenResult = StateIs.ShouldNotExist;
                ConsoleOn = bOFF;
                ocetErrorWriter = System.Console.Error;
                LocalMessage.Msg9 = "Unhandled Error during Console Open.";
                ocetErrorWriter.WriteLine(LocalMessage.Msg9);
               st.TraceMdmDoDetailed(ConsoleFormUses.DebugLog, 3, ref Sender, bIsMessage, iNoOp, iNoOp, ConsoleOpenResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg9);
                ocetErrorWriter.WriteLine(eIoe.Message);
            }

            return ConsoleOpenResult;
        }
        /// <summary>
        /// Get input from the STD Console
        /// </summary> 
        public String ConsoleMdmStd_IoReadImpl(int iPassedLineRequestCount)
        {
            ConsolRoutedEventdResult = StateIs.Started;
            if (!ConsoleOn) { return sEmpty; }
            if (ConsoleToDisc) { return sEmpty; }
            //
            string sTemp3 = sEmpty;
            int iTemp3 = 0;
            string sTemp4 = sEmpty;
            // iTemp3 = iUnknown;
            try
            {
                if (ociConsolReader == null) { ConsolRoutedEventdResult = ConsoleMdmStd_IoOpenImpl(); }
                while (iTemp3 < iPassedLineRequestCount && sTemp3.Length > 0)
                {
                    sTemp3 = System.Console.ReadLine();
                    if (sTemp3.Length > 0)
                    {
                        sTemp4 += sTemp3 + CrLf;
                        iTemp3 += 1;
                    }
                }
            }
            catch (IOException eIoe)
            {
                ConsolRoutedEventdResult = StateIs.Failed;
                ConsoleOn = bOFF;
                ocetErrorWriter = System.Console.Error;
                LocalMessage.Msg9 = "Import Tld Vs0_1 - Console Read Error - Line: " + iTemp3.ToString() + " - ";
                ocetErrorWriter.WriteLine(LocalMessage.Msg9);
               st.TraceMdmDoDetailed(ConsoleFormUses.DebugLog, 3, ref Sender, bIsMessage, iNoOp, iNoOp, ConsolRoutedEventdResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg9);
                ocetErrorWriter.WriteLine(eIoe.Message);
            }
            return sTemp4;
        }
        /// <summary>
        /// Sent Output to the STD Console
        /// </summary> 
        public StateIs ConsoleMdmStd_IoWriteImpl(String PassedLine)
        {
            ConsoleWriteResult = StateIs.Started;
            if (!ConsoleOn)
            {
                LocalMessage.Msg8 = PassedLine;
                System.Windows.MessageBox.Show(PassedLine, (String)"MinputTld", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, System.Windows.MessageBoxOptions.None);
                return ConsoleWriteResult;
            }
            if (ConsoleToDisc) { return ConsoleWriteResult; }
            //
            try
            {
                if (ocoConsoleWriter == null) { ConsoleWriteResult = ConsoleMdmStd_IoOpenImpl(); }
                System.Console.Write(PassedLine);
            }
            catch (IOException eIoe)
            {
                ConsoleWriteResult = StateIs.Failed;
                ocetErrorWriter = System.Console.Error;
                LocalMessage.Msg9 = "Mdm Console Write Error";
                ocetErrorWriter.WriteLine(LocalMessage.Msg9);
               st.TraceMdmDoDetailed(ConsoleFormUses.DebugLog, 3, ref Sender, bIsMessage, iNoOp, iNoOp, ConsoleWriteResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg9);
                ocetErrorWriter.WriteLine(LocalMessage.Msg9);
                ocetErrorWriter.WriteLine(eIoe.Message);
            }
            return ConsoleWriteResult;
        }
        /// <summary>
        /// Close the STD Console
        /// </summary> 
        public StateIs ConsoleMdmStd_IoCloseImpl()
        {
            ConsoleCloseResult = StateIs.Started;
            //
            if (!ConsoleOn) { return ConsoleCloseResult; }
            if (ConsoleToDisc) { return ConsoleCloseResult; }
            //
            try
            {
                ocoConsoleWriter.Close();
                ociConsolReader.Close();
            }
            catch (IOException eIoe)
            {
                ConsoleCloseResult = StateIs.Failed;
                ocetErrorWriter = System.Console.Error;
                LocalMessage.Msg9 = "Mdm Console Close Failed";
                ocetErrorWriter.WriteLine(LocalMessage.Msg9);
               st.TraceMdmDoDetailed(ConsoleFormUses.DebugLog, 3, ref Sender, bIsMessage, iNoOp, iNoOp, ConsoleCloseResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg9);
                ocetErrorWriter.WriteLine(LocalMessage.Msg9);
                ocetErrorWriter.WriteLine(eIoe.Message);
            }
            return ConsoleCloseResult;
        }
        #endregion
        #region Mdm Pick Console - ConsoleMdmPickDisplayImpl
        public void ConsoleMdmPickDisplayImpl(String PassedLine)
        {
            PickState.PickConsoleDisplayResult = StateIs.Started;
            // ConsoleTextBlock = PassedLine + ConsoleTextBlock;
            // MdmConsole
            ConsoleOutput = PassedLine;
            // ToDo Send to standard message
            LocalMessage.ErrorMsg = PassedLine;
           st.TraceMdmDoDetailed(ConsoleFormUses.UserLog, 5, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), 0, false, 0, 0, bDoNotDisplay, MessageNoUserEntry, "A3 " + LocalMessage.ErrorMsg);
        }
        #region Print Output Mdm Pick Print Target Validate (Output Marshalling)
        #endregion
        #region Mdm Pick Console Initialization
        /// <summary>
        /// Standard initialization for the console and Pick console
        /// </summary> 
        public StdConsolesDef StdConsoles { get; set; }
        public virtual void InitializeConsoleMdm()
        {
            Sender = this;
            // if (!ClassFeature.MdmIsUsed) { return; }
            if (!ClassFeatureFlag.MdmConsoleIsUsed) { return; }
            // ConsoleManager
            if (StdNotify == null) { StdKey = new StdKeyDef("1", "0", "Consoles"); } 
            // Standard objects
            #region Notify Group
            if (StdNotify == null)
            {
                if (ConsoleSender == null
                    || StdKey.Key == ((iClassFeatures)ConsoleSender).KeyGet().Key)
                {
                    StdNotify = new StdNotifyDef(ref SenderIsThis, ref ConsoleSender, StdKey, Title, true);
                    StdNotifyRoot = StdNotify; // this is the console root.
                    StdNotify.Root = StdNotify;
                }
                // It defaults to the Form Parent on existing consoles..
                if (FormParent != null)
                {
                    if (StdNotify == null)
                    {
                        StdNotify = ((iClassFeatures)FormParent).StdNotifyGet("Console");
                        if (StdNotify != null)
                        {
                            StdNotifyRoot = StdNotify;
                            StdNotify.Root = ((iClassFeatures)FormParent).StdNotifyGet("Root"); 
                        }
                    }
                    if (StdNotify == null)
                    {
                        StdNotify = ((iClassFeatures)FormParent).StdNotifyGet("Root");
                        if (StdNotify != null)
                        {
                            StdNotifyRoot = StdNotify;
                            StdNotify.Root = ((iClassFeatures)FormParent).StdNotifyGet("Root"); 
                        }
                    }
                }
            }
            #endregion

            // delegates can not be set here...
            // Set these in the application controller
            // override that calls this base class.
            #region Control Flags
            // <Area Id = "ConsoleBasicConsole">
            ConsoleVerbosity = 20;
            // <Area Id = "Console">
            //ConsoleOn = bOFF;
            //ConsoleBasicOn = bON;
            //ConsoleToDisc = bOFF;
            //ConsoleToControl = bOFF;
            #endregion
            #region Console Basic Output
            ConsoleOutput = sEmpty;
            ConsoleOutputLog = sEmpty;
            // Display
            ConsoleOutput = sEmpty;
            ConsoleOutputLog = sEmpty;
            #endregion
            // <Area Id = "TextConsole>
            CommandLineRequest = sEmpty;
            CommandLineRequestResult = 0;
            TextConsole = sEmpty; // Basic
            ConsolePickTextConsole = sEmpty; // Pick
            // <Area Id = "ConsoleResponse">
            // <Area Id = "Pick Console">
            #region Console Text Block - Pick Text areas 1-4
            ConsoleTextBlock = sEmpty; // text block
            ConsoleTextPositionX = 0;
            ConsoleTextPositionY = 0;
            ConsoleTextPositionZ = 0;
            #region ConsoleText4 Documentation
            // ConsoleTextPositionOrigin;
            // ToDo z$RelVs2 ConsoleMdmInitialize  Text Areas 0 - 5
            // ToDo z$RelVs2 ConsoleMdmInitialize  Text Area 0 - ToDo z$RelVs2 ConsoleMdmInitialize 
            // ToDo z$RelVs2 ConsoleMdmInitialize  Text Area 1 - Summary Progress, Messages, Errors and Help, ToolTip
            // ToDo z$RelVs2 ConsoleMdmInitialize  Text Area 2 - Detailed Progress
            // ToDo z$RelVs2 ConsoleMdmInitialize  Text Area 3 - Help - What is it
            // ToDo z$RelVs2 ConsoleMdmInitialize  Text Area 4 - Help - How do I do this
            // ToDo z$RelVs2 ConsoleMdmInitialize  Text Area 5 - Help and Status - Procedure and Event Sequence
            #endregion
            #region ConsoleText4 Flags
            #endregion
            #region ConsoleText4 Message Text
            sMessageText = sEmpty;
            sMessageText0 = sEmpty;
            MessageTextOutConsole = sEmpty;
            MessageTextOutStatusLine = sEmpty;
            MessageTextOutProgress = sEmpty;
            MessageTextOutError = sEmpty;
            MessageTextOutRunAction = sEmpty;
            #endregion
            #region ConsoleText4 Control
            MessageStatusAction = sEmpty;
            ProcessStatusAction = sEmpty;
            //
            MessageStatusTargetText = sEmpty;
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
            if (ConsoleSender == this)
            {
                StdConsoles = new StdConsolesDef(ref Sender, ref st);
            } else
            {
                if (st.StdConsoles != null)
                {
                    StdConsoles = st.StdConsoles;
                } else
                {
                    StdConsoles = new StdConsolesDef(ref Sender, ref st);
                }
            }
        }
        #endregion
        #endregion
        #region Exception handling - General
        public StateIs ExceptGeneralResult;
        public void ExceptGeneralImpl(Exception ExceptionGeneral)
        {
            ExceptGeneralImpl(null, ExceptionGeneral);
        }
        public StateIs ExceptDatabaseGeneralResult;
        /// <summary> 
        /// Exceptions - General for the Passed File.
        /// Main Method for all exceptions.
        /// Exceptions being move to base classes
        /// including utility classes such as mFileDef.
        /// </summary> 
        public void ExceptGeneralImpl(object ObjectPassed, Exception ExceptionGeneral)
        {
            ExceptDatabaseGeneralResult = StateIs.Started;
            //LocalMessage.ErrorMsg = "General Exception: (" + PassedMethodResult.ToString() + ") " + ExceptionGeneral.Message + "\n";
            //LocalMessage.ErrorMsg += ErrorMsgPassed;
            ExceptGeneralImpl(
                ObjectPassed,
                ExceptionGeneral,
                sEmpty,
                StateIs.NotSet
                );
        }
        public void ExceptGeneralImpl(
            object ObjectPassed,
            Exception ExceptionGeneral,
            String ErrorMsgPassed,
            StateIs PassedMethodResult
            )
        {
            ExceptGeneralResult = PassedMethodResult;
            LocalMessage.ErrorMsg = "General Exception: (" + PassedMethodResult.ToString() + ") " + ExceptionGeneral.Message;
            if (ErrorMsgPassed.Length > 0)
            {
                LocalMessage.ErrorMsg += "\n" + ErrorMsgPassed;
            }
           st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PassedMethodResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageEnterOk, LocalMessage.ErrorMsg);
            if (RunAbortIsOn)
            {
                // (This does a generic throw)
                ExceptionCatchNotSupportedMdm(sEmpty);
            }
            RunErrorDidOccur = bYes;
        }
        #endregion
        #region NotSupported Exception
        public StateIs ExceptNotSupportedResult;
        /// <summary> 
        /// Exceptions - Not Supported Exception
        /// Routes to General Exception.
        /// </summary> 
        public void ExceptNotSupportedImpl(
            object ObjectPassed,
            NotSupportedException ExceptionNotSupported,
            String ErrorMsgPassed,
            StateIs PassedMethodResult
            )
        {
            ExceptNotSupportedResult = PassedMethodResult;
            LocalMessage.ErrorMsg = "NotSupported Exception: (" + PassedMethodResult.ToString() + ") " + ExceptionNotSupported.Message + "\n";
            if (ErrorMsgPassed.Length > 0)
            {
                LocalMessage.ErrorMsg += "\n" + ErrorMsgPassed;
            }
           st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 3, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PassedMethodResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
            // Note: ToDo. Some Not supported exception should turn on run abort if appropriate.
            RunErrorDidOccur = bNO;
            if (RunAbortIsOn)
            {
                ExceptGeneralImpl(ObjectPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, PassedMethodResult);
                // (This does a generic throw)
                ExceptionCatchNotSupportedMdm(sEmpty);
            }
        }
        #endregion
    }
}

