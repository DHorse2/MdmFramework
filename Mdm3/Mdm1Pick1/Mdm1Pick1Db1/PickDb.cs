#region Dependencies
#region System
using System;
using System.Linq;
#endregion
#region System Collections
using System.Collections;
using System.Collections.Generic;
//using System.Collections.Specialized;
#endregion
#region System Data & SQL
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
#endregion
#region System Text
using System.Text;
using System.Text.RegularExpressions;
#endregion
#region System Windows Forms
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
//using System.Windows.Controls;
#endregion
#region System Other
//using System.Collections.Specialized;
//using System.ComponentModel;
#endregion
#region System Globalization
using System.Globalization;
#endregion
#region System Serialization (Runtime and Xml)
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
#endregion
#region System Reflection, Runtime, Timers
using System.Diagnostics;
using System.Reflection;
using System.Runtime;
//using System.Runtime.InteropServices;
//using System.Runtime.Remoting.Messaging;
using System.Timers;
#endregion
#region System XML
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Serialization.Advanced;
using System.Xml.Serialization.Configuration;
#endregion

#region  Mdm Core
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Mobj;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Components;
using Mdm.World;
#endregion
#region Mdm WinUtil, System Shell32, WshRuntime
//using Mdm.Oss.WinUtil;
//using Mdm.Oss.WinUtil.Types;
////          add shell32.dll reference
////          or COM Microsoft Shell Controls and Automation
//using Shell32;
////          At first, Project > Add Reference > COM > Windows ScriptItemPassed Host Object Model.
//using IWshRuntimeLibrary;
#endregion
#region  Mdm Db and File
using Mdm.Oss.File;
using Mdm.Oss.File.Control;
using Mdm.Oss.File.Db;
using Mdm.Oss.File.Db.Data;
using Mdm.Oss.File.Db.Table;
using Mdm.Oss.File.Db.Thread;
//using Mdm.Oss.File.Properties;
using Mdm.Oss.File.RunControl;
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
// using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
using Mdm.Oss.File.Type.Srt.Script;
using Mdm.Printer.File;
#endregion
#region Mdm Srt (Search, replace and transform)
using Mdm.Srt;
using Mdm.Srt.Core;
using Mdm.Srt.Transform;
//using Mdm.Srt.Script;
#endregion
#region  Mdm Clipboard
// using Mdm.Oss.ClipUtil;
#endregion
#endregion

namespace Mdm.Printer.File
{
    public class mFilePickDbDef : mFileDef
    {
        // Core Objects - Mapplication
        // derived class now:         
        //public Mapplication XUomMavvXv;
        //public new FileMainPickDef Fmain;
        //public new FileMainPickDef Faux;

        protected internal StateIs imFilePickDbDef;
        public mFilePickDbDef FileObjectPickDb;
        public mFilePickDbDef(ref object SenderPassed, ref StdConsoleManagerDef stPassed)
            : base(ref SenderPassed, ref stPassed)
        {
            imFilePickDbDef = StateIs.Started;
            this.Initialize();
        }
        public mFilePickDbDef(ref object SenderPassed, FileAction_DirectionIs DirectionPassed)
            : base(ref SenderPassed, DirectionPassed)
        {
            imFilePickDbDef = StateIs.Started;
            this.Initialize();
        }
        public mFilePickDbDef()
            : base()
        {
            this.Initialize();
        }
        public void Initialize()
        {
            base.InitializeMFile();
            mFilePickDbDefInitialize();
        }
        public void mFilePickDbDefInitialize()
        {
            FileObjectPickDb = this;
            imFilePickDbDef = StateIs.Finished;
        }
        // #region FileTransformControlPickDef
        // @@@dgh2 public FileTransformControlPickDef FileTransformControl;
        //public enum FileActionDirectionIs : int {
        //    Output = 1,
        //    Input = 2
        //}
        // @@@dgh2 public FileSummaryDef Fs = new FileSummaryDef();
        //public long AppIoObjectSet(FileTransformControlPickDef PassedFileTransformControl) {
        //    AppIoObjectSet = StateIs.Started;
        //    FileTransformControl = PassedFileTransformControl;
        //    AppIoObjectSet = StateIs.Successful;
        //    return AppIoObjectSet;
        //}
        //#endregion

        //
        public StateIs PickItemDataCounterResultGet;
        //
        #region PICK FILE FUNCTIONS
        #region Buffer Handling
        // Buffer Move
        public String PickBufferMoveToString(ref mFileMainDef FmainPassed, String PassedInputString, int iPassedCharacterQty)
        {
            PickState.PickBufferMoveStringResult = StateIs.Started;
            // sTemp = sEmpty;
            if (iPassedCharacterQty < 1) { iPassedCharacterQty = PassedInputString.Length; }
            string sTemp = PassedInputString.Substring(0, iPassedCharacterQty);
            return sTemp;
        }
        // Move Buffer to Item.ItemData
        public StateIs PickBufferMoveToItemData(ref mFileMainDef FmainPassed)
        {
            PickState.PickBufferMoveToItemDataResult = StateIs.Started;
            // Move Characters from Buffer to Input Item
            if (FmainPassed.Buf.FileWorkBuffer.Length > 0)
            {
                // Item Load InFile.Fmain.Item.ItemData with FileTransformControl.InFile.FileWorkBuffer
                FmainPassed.Item.ItemData += PickBufferMoveToString(ref FmainPassed, FmainPassed.Buf.FileWorkBuffer, 0);
                // FmainPassed.ItemAttrCounter = 1; // Current Attr
                // FmainPassed.ItemAttrMaxIndex = PickAttrCountGet(FmainPassed.Item.ItemData, ColumnSeparator); // Total Attrs in Item
                FmainPassed.DelSep.ItemAttrMaxIndexTemp = PickAttrCountGet(FmainPassed.Item.ItemData, ColumnSeparator); // Total Attrs in Item
                // Working value
                // FmainPassed.ItemAttrCounter = 0; // Data Items in Item / Row / Item
                // Character Pointers
                FmainPassed.DelSep.iItemDataCharEobIndex = FmainPassed.Item.ItemData.Length; // End of Character Buffer
                if (FmainPassed.FileStatus.ItemIsAtEnd == bYES)
                {
                    FmainPassed.DelSep.iItemDataCharEofIndex = FmainPassed.DelSep.iItemDataCharEobIndex;
                }
                // FmainPassed.iItemDataCharIndex = 0; // Character Pointer
                // FmainPassed.iItemDataCharEofIndex = 0; // Character End of File
                // Clear moved buffer
                FmainPassed.Buf.FileWorkBuffer = sEmpty;
                FmainPassed.FileStatus.HasCharacters = bNO;
                // ToDo PickBufferMoveToItemData This belongs in Move and Convert
                // FmainPassed.Buf.CharItemEofIndex += FmainPassed.FileWorkBuffer.Length;
                FmainPassed.Buf.CharItemEofIndex += 0;
                FmainPassed.Buf.CharIndex = 0;
                // FmainPassed.Buf.CharMaxIndex = FmainPassed.FileWorkBuffer.Length;
                FmainPassed.Buf.CharMaxIndex = 0;
                // FmainPassed.Buf.AttrMaxIndex = PickAttrCountGet(FmainPassed.FileWorkBuffer, ColumnSeparator);
                FmainPassed.Buf.AttrMaxIndex = 0;
            }
            //
            return PickState.PickBufferMoveToItemDataResult;
        }
        // Move Shift Item.ItemData by x Attrs
        public int PickItemDataCounterGetTo(ref mFileMainDef FmainPassed)
        {
            PickItemDataCounterResultGet = StateIs.Started;
            //
            // Count Item.ItemData Attrs
            //
            // Set Temp value for Col for working client program
            // FmainPassed.ColMaxIndex = PickAttrCountGet(FmainPassed.Item.ItemData, ColumnSeparator);
            //
            PickState.PickItemDataCounterResult = StateIs.Started;
            int PickAttrCount = PickAttrCountGet(FmainPassed.Item.ItemData, ColumnSeparator);
            FmainPassed.ColIndex.ColMaxIndexTemp = PickAttrCount;
            //
            if (TraceOn || ConsoleOn || ConsoleBasicOn) { TraceMdmDoImpl(ConsoleFormUse, 3, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickState.PickItemDataCounterResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + "Count Current Column Attrs: " + FmainPassed.ColIndex.ColMaxIndexTemp.ToString() + "\n"); }
            // 
            // Set Current Item.ItemData Last Character Location
            // 
            // FmainPassed.iItemDataCharEobIndex = PickIndex(FmainPassed.Item.ItemData, ColumnSeparator, FmainPassed.ColMaxIndexTemp);
            FmainPassed.DelSep.iItemDataCharEobIndex = FmainPassed.Item.ItemData.Length; // End of Character Buffer
            if (FmainPassed.FileStatus.ItemIsAtEnd == bYES)
            {
                FmainPassed.DelSep.iItemDataCharEofIndex = FmainPassed.DelSep.iItemDataCharEobIndex;
            }
            if (TraceOn || ConsoleOn || ConsoleBasicOn) { TraceMdmDoImpl(ConsoleFormUse, 3, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickState.PickItemDataCounterResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + "Current Column Last Character Location: " + FmainPassed.DelSep.iItemDataCharEobIndex.ToString() + "\n"); }
            //
            // Current Item.ItemData Attr
            //
            // Start of Attr
            if (FmainPassed.ColIndex.ColCounter > 1)
            {
                FmainPassed.DelSep.iItemDataAttrEoaIndex = PickIndex(FmainPassed.Item.ItemData, ColumnSeparator, FmainPassed.ColIndex.ColCounter - 1);
            }
            else
            {
                FmainPassed.DelSep.iItemDataAttrEoaIndex = 0;
            }
            if (FmainPassed.DelSep.iItemDataAttrEoaIndex < 0) { FmainPassed.DelSep.iItemDataAttrEoaIndex = 0; }
            // End of Attr
            if (FmainPassed.ColIndex.ColCounter > 0)
            {
                FmainPassed.DelSep.iItemDataAttrEoaIndexEnd = PickIndex(FmainPassed.Item.ItemData, ColumnSeparator, (FmainPassed.ColIndex.ColCounter));
            }
            else
            {
                FmainPassed.DelSep.iItemDataAttrEoaIndexEnd = -1;
            }
            if (FmainPassed.DelSep.iItemDataAttrEoaIndexEnd < 0)
            {
                FmainPassed.DelSep.iItemDataAttrEoaIndexEnd = FmainPassed.Item.ItemData.Length;
            }

            PickState.PickItemDataCounterResult = StateIs.Finished;
            return PickAttrCount;
        }
        #endregion
        #region Pick_Id
        // ID CHECK (Checks Output File Row Existance against options)
        public StateIs PickIdCheckOptions(ref mFileMainDef FmainPassed)
        {
            PickDataState.PickIdCheckOptionsResult = StateIs.Started;
            // ToDo PickIdCheckOptionsResult
            // read InFile.Fmain.Item.ItemData from f.file, InFile.Fmain.Item.ItemId) {
            LocalId.LongResult = 0;
            if (LocalId.LongResult != 0)
            {
                if ((PickIndex(RunOptions, "O", 1)) == 0)
                {
                    PickDataState.PickIdCheckOptionsResult = StateIs.NotSet;
                    RunErrorDidOccur = bYES;
                    // PrintOutputMdm_PickPrint(Sender, 3, bYES, "A2");
                    LocalMessage.Msg6 = "ABORT: Item \"" + FmainPassed.Item.ItemId + "\" already exists in \"" + FmainPassed.Fs.FileId.FileName + "\".";
                    // PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg6, bYES);
                    TraceMdmDoImpl(ConsoleFormUse, 3, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickState.PickSystemCallStringResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg6 + "\n");
                    // goto finish;
                    XUomMovvXv.RunAbortIsOn = bYES;
                }
            }
            return PickDataState.PickIdCheckOptionsResult;
        }
        public StateIs PickIdCheck(ref mFileMainDef FmainPassed, String PassedPickTildeId)
        {
            PickDataState.PickIdCheckResult = StateIs.Started;
            // ToDo PickIdCheckResult
            return PickDataState.PickIdCheckResult;
        }
        // Check for a Tilde Quoted Id exists
        public bool PickIdCheckDoesExistBool(ref mFileMainDef FmainPassed, String PassedItemId)
        {
            PickDataState.PickIdCheckDoesExistResult = StateIs.Started;
            // Check to see if passed item exists in the file
            // ToDo PickIdCheckDoesExist bNO for now xxx
            FmainPassed.Item.ItemIdExists = bNO;
            return FmainPassed.FileStatus.DoesExist;
        }
        // Get a Tilde Quoted Id
        public StateIs PickIdGetTo(ref mFileMainDef FmainPassed)
        {
            PickDataState.PickIdGetResult = StateIs.Started;
            // Get / check for Id at beginning of item
            string sTemp1, sTemp3, sTemp5;
            sTemp1 = FmainPassed.Item.ItemData.GetField(FmainPassed.Buf.CharIndex);
            sTemp5 = PickIdGetString(sTemp1, RowSeparator);
            //
            if (sTemp5.Length > 0)
            {
                FmainPassed.Fs.ItemIdCurrent = PickField(sTemp5, " ", 1);
                FmainPassed.Item.ItemId = FmainPassed.Fs.ItemIdCurrent;
                // 
                sTemp3 = PickIdGetFileString(sTemp5);
                FmainPassed.Item.ItemData = PickDel(FmainPassed.Item.ItemData, 1);
            }
            else
            {
                // no data in first column
                // InFile.Fmain.Item.ItemId = sEmpty;
                // Buf.CharIndex = 0;
                FmainPassed.ColIndex.ColText = FmainPassed.Item.ItemId;
                if (TraceBugOnNow)
                {
                    if (FmainPassed.ColIndex.ColText.Length > 100)
                    {
                        LocalMessage.Msg9 = "Debug Point Reached, Column length greater than 100!!! " + FmainPassed.ColIndex.ColText.Length + "\n";
                        TraceMdmDoImpl(ConsoleFormUse, 3, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickDataState.PickIdGetResult, TraceBugOnNow, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg9);
                        LocalMessage.Msg = LocalMessage.Msg9;
                    }
                }
                // Item.ItemIdChanged = bNO;
                FmainPassed.FileStatus.NameIsChanged = bNO;
                PickDataState.PickIdGetResult = StateIs.DoesExist;
            }
            //
            return PickDataState.PickIdGetResult;
        }
        // Get a Tilde Quoted Id
        public String PickIdGetFileString(String StringPassed)
        {
            PickDataState.PickIdGetFileStringResult = StateIs.Started;
            //
            // Get / check for Id at beginning of item
            string sTemp3 = sEmpty;
            sField_ConversionRootVerb = StringPassed;
            // Check for embedded File Name in Tilde ID
            // Allow for ~ItemName FileName~
            sTemp3 = PickField(sField_ConversionRootVerb, " ", 2);
            //
            return sTemp3;
        }
        // Get a Tilde Quoted Id
        public String PickIdGetString(String PassedPickTildeId, String PassedQuoteCharacter)
        {
            PickDataState.PickIdGetStringResult = StateIs.Started;
            string sTemp4 = PassedPickTildeId;
            // Tilde Id Handling
            // Ids start and end with a Tilde,
            // with a CrLf following.
            // which as serveds as a RowSeparator
            // This implies that Ids are on a row by themselves.
            // meaning IDs are in a row by themselves basically
            // ToDo z$NOTE for future: Needs a different Row Character from Id Quote character.
            if (sTemp4.Length > (2 * PassedQuoteCharacter.Length))
            {
                //
                PickCol.ColCharacter = PassedPickTildeId.Substring((sTemp4.Length - PassedQuoteCharacter.Length), PassedQuoteCharacter.Length);
                //
                // sLine starts and ends with a TLD
                if (PassedPickTildeId.Substring(0, PassedQuoteCharacter.Length) == PassedQuoteCharacter && PassedPickTildeId.Substring(PassedPickTildeId.Length - PassedQuoteCharacter.Length, PassedQuoteCharacter.Length) == PassedQuoteCharacter)
                {
                    //
                    if (sTemp4.Length > (2 * PassedQuoteCharacter.Length))
                    {
                        //
                        sTemp4 = PassedPickTildeId.Substring(1, PassedPickTildeId.Length - (2 * PassedQuoteCharacter.Length));
                    }
                    else { sTemp4 = sEmpty; }
                }
                else { sTemp4 = sEmpty; }
            }
            else { sTemp4 = sEmpty; }
            return sTemp4;
        }
        // Pick Id Changed
        public StateIs PickIdChanged(ref mFileMainDef FmainPassed, String ItemIdCurrent)
        {
            PickDataState.PickIdChangedResult = StateIs.Started;
            // Print
            // PrintOutputMdm_PickPrint(Sender, 3, (bool)bYES);
            // PrintOutputMdm_PickPrint(Sender, 3, bYES, 5);
            LocalMessage.Msg6 = "File Id: Current: \"" + FmainPassed.Item.ItemId + "\"";
            LocalMessage.Msg6 += ", Id length(" + FmainPassed.Item.ItemId.Length.ToString() + ")";
            switch (FmainPassed.Fs.FileLevelId)
            {
                case (FileType_LevelIs.Data):
                    // Processing Data
                    switch (FmainPassed.Fs.FileSubTypeMajorId)
                    {
                        case (FileType_SubTypeIs.SQL):
                        case (FileType_SubTypeIs.MS):
                        case (FileType_SubTypeIs.MY):
                        case (FileType_SubTypeIs.CSV):
                            LocalMessage.Msg6 += ", Data Count (" + FmainPassed.DelSep.ItemAttrCounter.ToString() + ")";
                            LocalMessage.Msg6 += ", Columns (" + FmainPassed.ColIndex.ToString() + ")";
                            LocalMessage.Msg6 += ", At Positon (" + TraceMdmCounterLevel1GetDefault().ToString() + ")";
                            break;
                        case (FileType_SubTypeIs.ASC):
                        case (FileType_SubTypeIs.DAT):
                        case (FileType_SubTypeIs.FIX):
                            // ToDo Implement ID Changed for Text SubType - not supported
                            break;
                        default:
                            break;
                    }
                    break;
                //
                case (FileType_LevelIs.DictData):
                    // FileDictData
                    switch (FmainPassed.Fs.FileSubTypeMajorId)
                    {
                        case (FileType_SubTypeIs.SQL):
                        case (FileType_SubTypeIs.MS):
                        case (FileType_SubTypeIs.MY):
                        case (FileType_SubTypeIs.CSV):
                            LocalMessage.Msg6 += " Dictionary Count (" + FmainPassed.ColIndex.ColCountTotal.ToString() + ")";
                            LocalMessage.Msg6 += " Attrs (" + FmainPassed.ColIndex.ToString() + ")";
                            LocalMessage.Msg6 += ", At Positon (" + TraceMdmCounterLevel1GetDefault().ToString() + ")";
                            break;
                        case (FileType_SubTypeIs.TEXT
                        | FileType_SubTypeIs.Tilde):
                        case (FileType_SubTypeIs.ASC):
                        case (FileType_SubTypeIs.DAT):
                        case (FileType_SubTypeIs.FIX):
                            // ToDo Implement ID Changed for Text SubType - not supported
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    // FileTypeUNKNOWN
                    FileState.ColPointerIncrementResult = StateIs.Undefined;
                    LocalMessage.Msg5 = "File Type Error (" + FmainPassed.Fs.FileTypeId.ToString() + ") not properly set";
                    throw new NotSupportedException(LocalMessage.Msg5);
            } // end or is DATA Attr not DICT
            //
            //if (ConsoleVerbosity >= 5) {
            //    if (ConsoleOn) {
            // PrintOutputMdm_PickPrint(Sender, 2, "A1" + Fs.ItemIdCurrent, bYES);
            //PrintOutputMdm_PickPrint(Sender, 2, "A1" + LocalMessage.Msg6, bYES);
            //PrintOutputMdm_PickPrint(Sender, 2, "A2" + LocalMessage.Msg6, bYES);
            //    }
            //}
            //
            // Item Above Attr Index
            FmainPassed.Item.ItemId = FmainPassed.Fs.ItemIdCurrent;
            //
            // Move Buffer if present
            if (FmainPassed.Buf.FileWorkBuffer.Length > 0) { PickDataState.PickIdChangedResult = PickBufferMoveToItemData(ref FmainPassed); }
            //
            // Write Output File Item
            if (!IterationFirst)
            {
                if (FmainPassed.Item.ItemId.Length > 0 && FmainPassed.Item.ItemData.Length > 0)
                {
                    // WRITE CURRENT ITEM
                    if (ConsoleOn)
                    {
                        LocalMessage.Msg6 = "File Id: Write Item: Pick Id \"" + FmainPassed.Item.ItemId + "\"";
                        LocalMessage.Msg6 += ", Id length: (" + FmainPassed.Item.ItemId.Length.ToString() + ")";
                        // PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg6, bYES);
                        TraceMdmDoImpl(ConsoleFormUse, 3, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickDataState.PickIdGetResult, TraceBugOnNow, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg6 + "\n");
                    }
                    //
                    LocalId.LongResult = FileWrite(ref FmainPassed);
                    //
                    PickDictItem.ColInvalid = 0;
                    // m/b wrong ColIndex.ColInvalid = 0;
                }
                // // Shift Data Item if more than 1000 Attrs
                if (FmainPassed.DelSep.ItemAttrCounter > 1000)
                {
                    PickDataState.PickIdChangedResult = (StateIs)ShiftCheck(ref FmainPassed);
                }
                /*
                if (Item.ItemData.Length > 500000) {
                    IterationDebugCount = 5000;
                } else if (Item.ItemData.Length > 5000) {
                    IterationDebugCount = 1000;
                } else if (Item.ItemData.Length > 1000) {
                    IterationDebugCount = 500;
                }
                if (IterationCount >= IterationDebugCount) {
                    IterationCount = 1;
                }
                */
            }
            // Reset Item Types
            // Reset Output Data Item
            switch (FmainPassed.Fs.FileLevelId)
            {
                case (FileType_LevelIs.DictData):
                    PickDataState.PickIdChangedResult = FileDictClearCurrent(ref FmainPassed);
                    break;
                case (FileType_LevelIs.Data):
                default:
                    PickDataState.PickIdChangedResult = FileDataClearCurrent(ref FmainPassed);
                    break;
            }
            // Display Line Number Reset
            StdPrinterDisplayLineNumber = 0;
            // Load Input and Output Id's with Next item ID
            FmainPassed.Fs.ItemIdCurrent = FmainPassed.Fs.ItemIdNext;
            FmainPassed.Item.ItemId = FmainPassed.Fs.ItemIdCurrent;
            FmainPassed.Fs.ItemIdNext = sEmpty;
            //
            LocalMessage.Msg6 = "Pick Id Next: \"" + FmainPassed.Item.ItemId + "\"";
            // PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg6, bYES);
            // FileTransformControl.ColText = Item.ItemId;
            if (TraceBugOnNow && TraceDisplayCountTotal > TraceBugThreshold)
            {
                LocalMessage.Msg9 = ", Attr statistics: " + FmainPassed.Item.ItemData.GetStatistics();
                TraceMdmDoImpl(ConsoleFormUse, 3, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickDataState.PickIdChangedResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg6 + "\n");
            }
            //
            FmainPassed.Item.ItemIdIsChanged = bNO;
            return PickDataState.PickIdChangedResult;
        }
        #endregion
        #endregion
        public override String ToString()
        {
            if (Fmain.Fs.FileId.FileName != null && Fmain.Fs.DirectionName != null)
            {
                String sTemp = "File Summary: " + Fmain.Fs.FileId.FileName + " for " + Fmain.Fs.DirectionName;
                return sTemp;
            }
            else { return base.ToString(); }
            //return sEmpty;
        }
    }
}
// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
namespace Mdm.Oss.FileUtil
{
    public class FileTransformControlPickDef : FileTransformControlDef
    {
        public FileTransformControlPickDef()
            : base()
        {
            //InputFsCurr = new FileSummaryPickDef();
            //OutputFsCurr = new FileSummaryPickDef();
        }
        #region InputItem Declaration
        // InFile
        public new mFilePickDbDef InFile;
        //public new FileSummaryPickDef InputFsCurr;
        #endregion
        #region FileOutputItem Declaration
        public new mFilePickDbDef OutFile;
        //public new FileSummaryPickDef OutputFsCurr;
        #endregion
    }

    //public class FileSummaryPickDef : FileSummaryDef {
    //    public FileSummaryPickDef()
    //        : base() {
    //        //
    //    }
    //    #region File Declarations
    //    public new mFilePickDbDef FileObject;
    //    #endregion
    //}

    //public class FileCommandPickDef : FileCommandDef {
    //    public FileCommandPickDef()
    //        : base() {
    //        //
    //    }
    //    public new FileSummaryPickDef Fs;
    //}

    //public class FileMainPickDef : mFileMainDef {
    //    public FileMainPickDef()
    //        : base() {
    //        Fs = new FileSummaryPickDef();
    //    }
    //    public new FileSummaryPickDef Fs;
    //}

}