#region Dependencies
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

#region Mdm
// Mdm (Macroscope Design Matrix / Dgh (c))
#region  Mdm Core
using Mdm.Oss;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
//@@@CODE@@@using Mdm.Oss.Mapp;
// using Mdm.Oss.Mobj;
//using Mdm.Oss.Sys;
//using Mdm.Oss.Thread;
//using Mdm.Oss.Threading;
using Mdm.Pick;
using Mdm.Pick.Console;
//using Mdm.World;
#endregion
#region Mdm WinUtil, System Shell32, WshRuntime
//using Mdm.Oss.WinUtil;
//// Project > Add Reference > 
//// add shell32.dll reference
//// (new) possibly interop.Shell32 & interop.IWshRuntimeLibrary
//// > COM > Microsoft Shell Controls and Automation
//using Shell32;
//// > COM > Windows ScriptItemPassed Host Object Model.
//using IWshRuntimeLibrary;
#endregion
#region  Mdm Db and File
using Mdm.Oss.File;
using Mdm.Oss.File.Control;
//using Mdm.Oss.File.Db;
//using Mdm.Oss.File.Db.Data;
//using Mdm.Oss.File.Db.Table;
//using Mdm.Oss.File.Db.Thread;
////using Mdm.Oss.File.Properties;
//using Mdm.Oss.File.RunControl;
#endregion
#region  Mdm File Types
//using Mdm.Oss.File.Type;
////using Mdm.Oss.File.Type.Link;
//using Mdm.Oss.File.Type.Pick;
//using Mdm.Oss.File.Type.Sql;
////using Mdm.Oss.File.Type.Srt.Script;
#endregion
#region Mdm Srt (Search, replace and transform)
//using Mdm.Srt;
//using Mdm.Srt.Core;
//using Mdm.Srt.Transform;
//using Mdm.Srt.Script;
#endregion
#region  Mdm MVC Mobject
//using Mdm.Oss.Mobj;
#endregion
#region  Mdm Apps Clipboard
//using Mdm.Oss.ClipUtil;
//using Mdm.Oss.ClipUtil.Windows;
#endregion
#region  Mdm Apps Shortcut Utils (Link)
//using Mdm.Oss.ShortcutUtil;
#endregion
#endregion
#endregion

namespace Mdm.Pick.Console {
    public class PickConsole : StdBaseRunFileDef {
        // Methods
        //public long PickAttrCountGetResult;
        //public String sFileDoRequest;
        #region PickData
        //
        public long PickIdChangedResult;
        public long PickIdCheckResult;
        public long PickIdCheckOptionsResult;
        public long PickIdCheckDoesExistResult;
        public long PickIdGetResult;
        public long PickIdGetStringResult;
        public long PickIdGetFileStringResult;
        //
        //public long PickIndexResult;
        //
        public long PickInputBoolResult;
        public long PickInputCharResult;
        public long PickInputCurrencyResult;
        public long PickInputIntResult;
        public long PickInputBigIntResult;
        public long PickInputTinyIntResult;
        public long PickInputFloatResult;
        public long PickInputStringResult;
        public long PickInputStringUResult;
        public long PickInputVarCharResult;
        public long PickInputVarCharUResult;
        //
        public long PickInputActionResult;
        public long PickInputConfirmResult;
        public long PickInputOkResult;
        public long PickInputYesNoResult;
        //
        public long PickPrintResult;
        public long PickPrintNewLineResult;
        public long PickPrintNewLineOnlyResult;
        //
        public long PickCloseResult;
        //
        public long PickWriteResult;
        #endregion
        #region PickDevicesAndIO
        #region MdmClass Printer Declarations
        // <Area Id = "Printer">
        // <Area Id = "ProgessDisplayControl">
        // Printer / Progess Display Control
        protected int PickPrinterRouting = 14;
        const int PICK_PRINTERON = 11;
        const int PICK_PRINT_TO_FILE = 12;
        const int PICK_PRINT_TO_DISPLAY = 13;
        const int PICK_PRINT_TO_Console = 14;
        // Head, Footer, Status Line, TextBox
        // Printer Output
        protected bool bPickPrinterNewLine = false;
        protected int iPickPrinterColumnCounter = 0;
        protected int iPickPrinterRowCounter = 0;
        protected int iPickPrinterColumn = 0;
        protected int iPickPrinterRow = 0;
        //
        // <Area Id = "PrinterOutput">
        protected bool PickPrinterNewLine = bNO;
        protected int PickPrinterColumnCounter = 0;
        protected int PickPrinterRowCounter = 0;
        protected int PickPrinterColumn = 0;
        protected int PickPrinterRow = 0;
        // <Area Id = "PrinterTarget">
        public String sPickPrintTarget = sEmpty;
        public String sPickPrintTargetString = sEmpty;
        public int PickPrintTargetBox = 0;
        public String sPickPrintTargetText = sEmpty;
        //
        // Console Output
        protected bool ConsolePickConsole_NewLine = false;
        protected int iConsolePickConsole_ColumnCounter = 0;
        protected int iConsolePickConsole_RowCounter = 0;
        //
        #endregion
        #region MdmClass Display Declarations
        // <Area Id = "Display">
        // <Area Id = "DisplayOutput">
        // <Area Id = "Head, Footer, Status Line, TextBox">
        // Display Output
        protected bool PickDisplayNewLine = bNO;
        protected int PickDisplayLineNumber = 0;
        protected int PickDisplayColumn = 0;
        protected int PickDisplayRow = 0;
        #endregion
        #region MdmClass Console Declarations
        // <Area Id = "ConsoleOutput">
        protected bool ConsolePickConsoleNewLine = bNO;
        protected int ConsolePickConsoleColumnCounter = 0;
        protected int ConsolePickConsoleRowCounter = 0;
        #endregion
        #region MdmClass Input Declarations
        // <Area Id = "Input">
        // Input
        protected bool bPromptNewLine = false;
        protected String sPromptChar = @"?";
        protected String sPromptText = sEmpty;
        protected int iPromptRow = 23;
        protected int iPromptColumn = 1;
        protected String sPromptResponse = sEmpty;
        protected String sPromptDefaultResponse = sEmpty;
        #endregion
        #endregion
        //
        #region PickDelegates

        public delegate void TraceMdmPointDel(
            long iPassed_MethodResult, 
            bool PassedError, 
            int iPassedErrorLevel, 
            int iPassedErrorSource, 
            bool PassedDisplay, 
            int iPassedUserEntry, 
            String PassedTraceMessage);
        protected TraceMdmPointDel TraceMdmPoint;

        public delegate void PrintOutputMdm_PickPositionDel(
            int iMessageLevel,
            bool PassedNewLineFlag,
            int iPassedPromptColumn,
            int iPassedPromptRow);
        protected PrintOutputMdm_PickPositionDel PrintOutputMdm_PickPosition;

        public delegate long ConsoleMdmStd_IoWriteDel(String PassedLine);
        protected ConsoleMdmStd_IoWriteDel ConsoleMdmStd_IoWrite;
        public delegate void ConsoleMdmPickDisplayDel(String PassedLine);
        protected ConsoleMdmPickDisplayDel ConsoleMdmPickDisplay;

        #endregion
        public PickConsole()
            : base() { }
        public PickConsole(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
            : base(null, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed) { }
        #region PickCodeAll
        // ==================================================================
        //
        // this is the PICK CODE
        // 
        // ==============================================================
        //
        #region PickInput
        // INPUT
        public String PickInput(int iMessageLevel, bool PassedNewLineFlag, int iPassedPromptColumn, int iPassedPromptRow, String PassedPromptText, String PassedPromptDefaultResponse) {

            PickInputStringResult = (long)StateIs.Started;
            String sPromptResponseCurrent = sEmpty;
            //
            PrintOutputMdm_PickPosition(iMessageLevel, bNO, iPassedPromptColumn, iPassedPromptRow);
            //
            sPromptResponseCurrent = PickInput(iMessageLevel, PassedPromptText, PassedPromptDefaultResponse);
            //

            //
            // PickInput
            return sPromptResponseCurrent;
        }

        public String PickInput(int iMessageLevel, String PassedPromptText, String PassedPromptDefaultResponse) {
            PickInputStringResult = (long)StateIs.Started;
            String sPromptResponseCurrent = sEmpty;
            // print it
            if (ConsoleOn) {
                PickPrintNewLineResult = ConsoleMdmStd_IoWrite(PassedPromptText);
            } else if (ConsolePickConsoleOn) {
                ConsoleMdmPickDisplay(PassedPromptText);
            }
            // PassedPromptResponse
            // sPromptResponseCurrent = Input me sTempMessage box;
            if (sPromptResponseCurrent.Length == 0) {
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
}
