using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
#region  Mdm Core
using Mdm.Oss;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Decl;
using Mdm.Oss.File.Control;
//@@@CODE@@@using Mdm.Oss.Mapp;
// using Mdm.Oss.Mobj;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
//using Mdm.Oss.Sys;
//using Mdm.Oss.Thread;
//using Mdm.Oss.Threading;
//using Mdm.World;
#endregion

namespace Mdm.Oss.Std
{
    // Next: StdConsoleManagerDef
    // bool: StdPrinterConsoleOn?
    public class StdBaseRunFilePrinterConsoleDef : StdBaseRunFileDef
    {
        #region Methods
        //public StateIs StdPrinterAttrCountGetResult;
        //public String sFileDoRequest;
        #region StdPrinterData - StdPrinter State Data (XxxResult)
        public PickDataStateDef PickDataState;
        #endregion        #region Constructors, Init Printer
        public StdBaseRunFilePrinterConsoleDef(ref object SenderPassed, ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
            : base(ref SenderPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed) { }
        public StdBaseRunFilePrinterConsoleDef(ref object SenderPassed)
            : base(ref SenderPassed)
        { PickDataState = new PickDataStateDef(); }
        public StdBaseRunFilePrinterConsoleDef(ref object SenderPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
            : base(ref SenderPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        { PickDataState = new PickDataStateDef(); }
        public StdBaseRunFilePrinterConsoleDef(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
            : base(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        { PickDataState = new PickDataStateDef(); }
        public StdBaseRunFilePrinterConsoleDef()
        : base() { PickDataState = new PickDataStateDef(); }
        public virtual void InitializeStdConsole()
        {
            if (!ClassFeatureFlag.InitializeStdDisplayDone)
            {
                ClassFeatureFlag.InitializeStdDisplayDone = true;
                base.InitializeStdBaseRun();
                if (ClassFeatureFlag.MdmControlIsUsed) { InitializeStdDisplay(); }
                if (ClassFeatureFlag.MdmPrintIsUsed) { InitializeStdPrinter(); }
            }
        }
        #endregion
        #region StdPrinterDevicesAndIO
        #region MdmClass Printer Declarations
        // <Area Id = "Printer">
        // <Area Id = "ProgessDisplayControl">
        // Printer / Progess Display Control
        protected int StdPrinterRouting;
        const int StdPrinter_PRINTERON = 11;
        const int StdPrinter_PRINT_TO_FILE = 12;
        const int StdPrinter_PRINT_TO_DISPLAY = 13;
        const int StdPrinter_PRINT_TO_Console = 14;
        // Head, Footer, Status Line, TextBox
        // Printer Output
        // <Area Id = "PrinterOutput">
        protected bool StdPrinterNewLine;
        protected int StdPrinterColumnCounter;
        protected int StdPrinterRowCounter;
        protected int StdPrinterColumn;
        protected int StdPrinterRow;
        // <Area Id = "PrinterTarget">
        public String StdPrinterTarget;
        public String StdPrinterTargetString;
        public int StdPrinterTargetBox;
        public String StdPrinterTargetText;
        //
        // Console Output
        protected bool Console_NewLine;
        protected int Console_ColumnCounter;
        protected int Console_RowCounter;
        //
        #endregion
        #region MdmClass Display Declarations
        #region StdPrinter Fields
        // <Area Id = "Display">
        // <Area Id = "DisplayOutput">
        // <Area Id = "Head, Footer, Status Line, TextBox">
        // Display Output
        protected bool StdPrinterDisplayNewLine;
        protected int StdPrinterDisplayLineNumber;
        protected int StdPrinterDisplayColumn;
        protected int StdPrinterDisplayRow;
        #endregion
        #region MdmClass Console Declarations
        // <Area Id = "ConsoleOutput">
        protected bool ConsoleNewLine;
        protected int ConsoleColumnCounter;
        protected int ConsoleRowCounter;
        #endregion
        #region MdmClass Input Declarations
        // <Area Id = "Input">
        // Input
        protected bool PromptNewLine;
        protected String PromptChar;
        protected String PromptText;
        protected int PromptRow;
        protected int PromptColumn;
        protected String PromptResponse;
        protected String PromptDefaultResponse;
        #endregion
        #endregion
        #endregion
        #region StdPrinterDelegates
        public delegate void TraceMdmPointDel(
            StateIs iPassed_MethodResult,
            bool PassedError,
            int iPassedErrorLevel,
            int iPassedErrorSource,
            bool PassedDisplay,
            int iPassedUserEntry,
            String PassedTraceMessage);
        protected TraceMdmPointDel TraceMdmPoint;

        public delegate void PrintOutputMdm_StdPrinterPositionDel(
            int iMessageLevel,
            bool PassedNewLineFlag,
            int iPassedPromptColumn,
            int iPassedPromptRow);
        protected PrintOutputMdm_StdPrinterPositionDel PrintOutputMdm_StdPrinterPosition;

        public delegate StateIs ConsoleMdmStd_IoWriteDel(String PassedLine);
        protected ConsoleMdmStd_IoWriteDel ConsoleMdmStd_IoWrite;
        public delegate void ConsoleMdmStdPrinterDisplayDel(String PassedLine);
        protected ConsoleMdmStdPrinterDisplayDel ConsoleMdmStdPrinterDisplay;

        #endregion
        #region Init StdPrinter and Printer
        public virtual void InitializeStdDisplay()
        {
            ClassFeatureFlag.InitializeStdDisplayDone = true;
            #region StdPrinter Fields
            // <Area Id = "Display">
            // <Area Id = "DisplayOutput">
            // <Area Id = "Head, Footer, Status Line, TextBox">
            // Display Output
            StdPrinterDisplayNewLine = bNO;
            StdPrinterDisplayLineNumber = 0;
            StdPrinterDisplayColumn = 0;
            StdPrinterDisplayRow = 0;
            #endregion
            #region MdmClass Console Declarations
            // <Area Id = "ConsoleOutput">
            ConsoleNewLine = bNO;
            ConsoleColumnCounter = 0;
            ConsoleRowCounter = 0;
            #endregion
            #region MdmClass Input Declarations
            // <Area Id = "Input">
            // Input
            PromptNewLine = false;
            PromptChar = @"?";
            PromptText = sEmpty;
            PromptRow = 23;
            PromptColumn = 1;
            PromptResponse = sEmpty;
            PromptDefaultResponse = sEmpty;
            #endregion
        }
        public virtual void InitializeStdPrinter()
        {
            ClassFeatureFlag.InitializeStdPrinterDone = true;
            // <Area Id = "Printer">
            // <Area Id = "ProgessDisplayControl">
            // Printer / Progess Display Control
            StdPrinterRouting = 14;
            // Head, Footer, Status Line, TextBox
            // Printer Output
            StdPrinterNewLine = false;
            StdPrinterColumnCounter = 0;
            // <Area Id = "PrinterOutput">
            StdPrinterNewLine = bNO;
            StdPrinterColumnCounter = 0;
            StdPrinterRowCounter = 0;
            StdPrinterColumn = 0;
            StdPrinterRow = 0;
            // <Area Id = "PrinterTarget">
            StdPrinterTarget = sEmpty;
            StdPrinterTargetString = sEmpty;
            StdPrinterTargetBox = 0;
            StdPrinterTargetText = sEmpty;
            // Console Output
            Console_NewLine = false;
            Console_ColumnCounter = 0;
            Console_RowCounter = 0;
        }
        #endregion
        #region PickCodeAll
        // ==================================================================
        //
        // this is the PICK CODE
        // 
        // ==============================================================
        //
        #region PickInput
        // INPUT
        public String PickInput(int iMessageLevel, bool PassedNewLineFlag, int iPassedPromptColumn, int iPassedPromptRow, String PassedPromptText, String PassedPromptDefaultResponse)
        {

            PickDataState.PickInputStringResult = StateIs.Started;
            String sPromptResponseCurrent = sEmpty;
            //
            PrintOutputMdm_StdPrinterPosition(iMessageLevel, bNO, iPassedPromptColumn, iPassedPromptRow);
            //
            sPromptResponseCurrent = PickInput(iMessageLevel, PassedPromptText, PassedPromptDefaultResponse);
            //

            //
            // PickInput
            return sPromptResponseCurrent;
        }
        public String PickInput(int iMessageLevel, String PassedPromptText, String PassedPromptDefaultResponse)
        {
            PickDataState.PickInputStringResult = StateIs.Started;
            String sPromptResponseCurrent = sEmpty;
            // print it
            //if (ConsoleOn) {
            PickDataState.PickPrintNewLineResult = ConsoleMdmStd_IoWrite(PassedPromptText);
            //} else if (ConsoleOn) {
            ConsoleMdmStdPrinterDisplay(PassedPromptText);
            //}
            // PassedPromptResponse
            // sPromptResponseCurrent = Input me sTempMessage box;
            if (sPromptResponseCurrent.Length == 0)
            {
                sPromptResponseCurrent = PassedPromptDefaultResponse;
            }
            // Validation per InputDataSg
            //
            // PickInputString
            return sPromptResponseCurrent;
        }
        #endregion
        #endregion
    }
    public class PickDataStateDef
    {
        // Id
        public StateIs PickIdChangedResult;
        public StateIs PickIdCheckResult;
        public StateIs PickIdCheckOptionsResult;
        public StateIs PickIdCheckDoesExistResult;
        public StateIs PickIdGetResult;
        public StateIs PickIdGetStringResult;
        public StateIs PickIdGetFileStringResult;
        // Index
        public StateIs PickIndexResult;
        // Input
        public StateIs PickInputBoolResult;
        public StateIs PickInputCharResult;
        public StateIs PickInputCurrencyResult;
        public StateIs PickInputIntResult;
        public StateIs PickInputBigIntResult;
        public StateIs PickInputTinyIntResult;
        public StateIs PickInputFloatResult;
        public StateIs PickInputStringResult;
        public StateIs PickInputStringUResult;
        public StateIs PickInputVarCharResult;
        public StateIs PickInputVarCharUResult;
        // Input Action
        public StateIs PickInputActionResult;
        public StateIs PickInputConfirmResult;
        public StateIs PickInputOkResult;
        public StateIs PickInputYesNoResult;
        // Print
        public StateIs PickPrintResult;
        public StateIs PickPrintNewLineResult;
        public StateIs PickPrintNewLineOnlyResult;
        // Close
        public StateIs PickCloseResult;
        // Write
        public StateIs PickWriteResult;
    }
}
