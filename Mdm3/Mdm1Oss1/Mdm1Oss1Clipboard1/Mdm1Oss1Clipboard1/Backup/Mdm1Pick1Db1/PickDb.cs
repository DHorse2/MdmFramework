using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows; // App
using System.Windows.Controls; // Page
//
using Mdm.Oss.Decl;
using Mdm.Oss.FileUtil;
//@@@CODE@@@using Mdm.Oss.Mapp;
using Mdm.Oss.Mobj;
using Mdm.Pick.File;


namespace Mdm.Pick.File {
    public class MfilePickDb : Mfile {
        // Core Objects - Mapplication
        // derived class now:         public Mapplication XUomMavvXv;
        //public new FileMainPickDef Fmain;
        //public new FileMainPickDef Faux;

        protected internal long iMfilePickDb;
        public MfilePickDb FileObjectPickDb;
        public MfilePickDb(ref Mobject PassedOb)
            : base(ref PassedOb) {
            iMfilePickDb = (long)StateIs.Started;
            FileObject = this;
            FileObjectPickDb = this;
            XUomMovvXv = PassedOb;
            MfilePickDbInitialize();
        }

        public MfilePickDb(ref Mobject PassedOb, int DirectionPassed)
            : base(ref PassedOb, DirectionPassed) {
            iMfilePickDb = (long)StateIs.Started;
            FileObject = this;
            FileObjectPickDb = this;
            XUomMovvXv = PassedOb;
            MfilePickDbInitialize();
        }

        public MfilePickDb() 
            : base() {
            FileObject = this;
            // Delegates;
        }
        public MfilePickDb(Mfile FbasePassed) 
            : this() {
            FileObject = FbasePassed;
            XUomMovvXv = new Mobject((long)ClassUses.RoleAsUtility);
            MfilePickDbInitialize();
        }

        public void MfilePickDbInitialize() {
            iMfilePickDb = (long)StateIs.Started;
            FileObject = this;
            FileObjectPickDb = this;
        }

        // #region FileTransformControlPickDef
        // @@@dgh2 public FileTransformControlPickDef FileTransformControl;
        //public enum FileActionDirectionIs : int {
        //    Output = 1,
        //    Input = 2
        //}
        // @@@dgh2 public FileSummaryDef Fs = new FileSummaryDef();
        //public long AppIoObjectSet(FileTransformControlPickDef PassedFileTransformControl) {
        //    iAppIoObjectSet = (long)StateIs.Started;
        //    FileTransformControl = PassedFileTransformControl;
        //    iAppIoObjectSet = (long)StateIs.Successful;
        //    return iAppIoObjectSet;
        //}
        //#endregion

        //
        public long PickItemDataCounterResultGet;
        //
        #region PICK FILE FUNCTIONS
        #region Buffer Handling
        // Buffer Move
        public String PickBufferMoveToString(ref FileMainDef FmainPassed, String PassedInputString, int iPassedCharacterQty) {
            PickBufferMoveStringResult = (long)StateIs.Started;
            // sTemp = "";
            if (iPassedCharacterQty < 1) { iPassedCharacterQty = PassedInputString.Length; }
            sTemp = PassedInputString.Substring(0, iPassedCharacterQty);
            return sTemp;
        }
        // Move Buffer to Item.ItemData
        public long PickBufferMoveToItemData(ref FileMainDef FmainPassed) {
            PickBufferMoveToItemDataResult = (long)StateIs.Started;
            // Move Characters from Buffer to Input Item
            if (FmainPassed.Buf.FileWorkBuffer.Length > 0) {
                // Item Load InFile.Fmain.Item.ItemData with FileTransformControl.InFile.FileWorkBuffer
                FmainPassed.Item.ItemData += PickBufferMoveToString(ref FmainPassed, FmainPassed.Buf.FileWorkBuffer, 0);
                // FmainPassed.ItemAttrCounter = 1; // Current Attr
                // FmainPassed.ItemAttrMaxIndex = PickAttrCountGet(FmainPassed.Item.ItemData, ColumnSeparator); // Total Attrs in Item
                FmainPassed.DelSep.ItemAttrMaxIndexTemp = PickAttrCountGet(FmainPassed.Item.ItemData, ColumnSeparator); // Total Attrs in Item
                // Working value
                // FmainPassed.ItemAttrCounter = 0; // Data Items in Item / Row / Item
                // Character Pointers
                FmainPassed.DelSep.iItemDataCharEobIndex = FmainPassed.Item.ItemData.Length; // End of Character Buffer
                if (FmainPassed.FileStatus.ItemIsAtEnd == bYES) {
                    FmainPassed.DelSep.iItemDataCharEofIndex = FmainPassed.DelSep.iItemDataCharEobIndex;
                }
                // FmainPassed.iItemDataCharIndex = 0; // Character Pointer
                // FmainPassed.iItemDataCharEofIndex = 0; // Character End of File
                // Clear moved buffer
                FmainPassed.Buf.FileWorkBuffer = "";
                FmainPassed.FileStatus.HasCharacters = bNO;
                // TODO PickBufferMoveToItemData This belongs in Move and Convert
                // FmainPassed.Buf.CharItemEofIndex += FmainPassed.FileWorkBuffer.Length;
                FmainPassed.Buf.CharItemEofIndex += 0;
                FmainPassed.Buf.CharIndex = 0;
                // FmainPassed.Buf.CharMaxIndex = FmainPassed.FileWorkBuffer.Length;
                FmainPassed.Buf.CharMaxIndex = 0;
                // FmainPassed.Buf.AttrMaxIndex = PickAttrCountGet(FmainPassed.FileWorkBuffer, ColumnSeparator);
                FmainPassed.Buf.AttrMaxIndex = 0;
            }
            //
            return PickBufferMoveToItemDataResult;
        }
        // Move Shift Item.ItemData by x Attrs
        public long PickItemDataCounterGet(ref FileMainDef FmainPassed) {
            PickItemDataCounterResultGet = (long)StateIs.Started;
            //
            // Count Item.ItemData Attrs
            //
            // Set Temp value for Col for working client program
            // FmainPassed.ColMaxIndex = PickAttrCountGet(FmainPassed.Item.ItemData, ColumnSeparator);
            //
            PickItemDataCounterResult = PickAttrCountGet(FmainPassed.Item.ItemData, ColumnSeparator);
            FmainPassed.ColIndex.ColMaxIndexTemp = PickItemDataCounterResult;
            //
            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(), PickItemDataCounterResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + "Count Current Column Attrs: " + FmainPassed.ColIndex.ColMaxIndexTemp.ToString() + "\n"); }
            // 
            // Set Current Item.ItemData Last Character Location
            // 
            // FmainPassed.iItemDataCharEobIndex = PickIndex(FmainPassed.Item.ItemData, ColumnSeparator, FmainPassed.ColMaxIndexTemp);
            FmainPassed.DelSep.iItemDataCharEobIndex = FmainPassed.Item.ItemData.Length; // End of Character Buffer
            if (FmainPassed.FileStatus.ItemIsAtEnd == bYES) {
                FmainPassed.DelSep.iItemDataCharEofIndex = FmainPassed.DelSep.iItemDataCharEobIndex;
            }
            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(),PickItemDataCounterResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + "Current Column Last Character Location: " + FmainPassed.DelSep.iItemDataCharEobIndex.ToString() + "\n"); }
            //
            // Current Item.ItemData Attr
            //
            // Start of Attr
            if (FmainPassed.ColIndex.ColCounter > 1) {
                FmainPassed.DelSep.iItemDataAttrEoaIndex = PickIndex(FmainPassed.Item.ItemData, ColumnSeparator, FmainPassed.ColIndex.ColCounter - 1);
            } else {
                FmainPassed.DelSep.iItemDataAttrEoaIndex = 0;
            }
            if (FmainPassed.DelSep.iItemDataAttrEoaIndex < 0) { FmainPassed.DelSep.iItemDataAttrEoaIndex = 0; }
            // End of Attr
            if (FmainPassed.ColIndex.ColCounter > 0) {
                FmainPassed.DelSep.iItemDataAttrEoaIndexEnd = PickIndex(FmainPassed.Item.ItemData, ColumnSeparator, (FmainPassed.ColIndex.ColCounter));
            } else {
                FmainPassed.DelSep.iItemDataAttrEoaIndexEnd = -1;
            }
            if (FmainPassed.DelSep.iItemDataAttrEoaIndexEnd < 0) {
                FmainPassed.DelSep.iItemDataAttrEoaIndexEnd = FmainPassed.Item.ItemData.Length;
            }

            return PickItemDataCounterResult;
        }
        #endregion
        #region Pick_Id
        // ID CHECK (Checks Output File Row Existance against options)
        public long PickIdCheckOptions(ref FileMainDef FmainPassed) {
            PickIdCheckOptionsResult = (long)StateIs.Started;
            // TODO PickIdCheckOptionsResult
            // read InFile.Fmain.Item.ItemData from f.file, InFile.Fmain.Item.ItemId) {
            LocalId.LongResult = 0;
            if (LocalId.LongResult != 0) {
                if ((PickIndex(RunOptions, "O", 1)) == 0) {
                    PickIdCheckOptionsResult = (long)FileAction_Do.NotSet;
                    XUomMovvXv.RunErrorDidOccur = bYES;
                    XUomMovvXv.PrintOutputMdm_PickPrint(Sender, 3, bYES, "A2");
                    LocalMessage.Msg6 = "ABORT: Item \"" + FmainPassed.Item.ItemId + "\" already exists in \"" + FmainPassed.Fs.FileId.FileName + "\".";
                    // XUomMovvXv.PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg6, bYES);
                    XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(),PickSystemCallStringResult, XUomMovvXv.RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg6 + "\n");
                    // goto finish;
                    XUomMovvXv.RunAbortIsOn = bYES;
                }
            }
            return PickIdCheckOptionsResult;
        }
        public long PickIdCheck(ref FileMainDef FmainPassed, String PassedPickTildeId) {
            PickIdCheckResult = (long)StateIs.Started;
            // TODO PickIdCheckResult
            return PickIdCheckResult;
        }
        // Check for a Tilde Quoted Id exists
        public bool PickIdCheckDoesExistBool(ref FileMainDef FmainPassed, String PassedItemId) {
            PickIdCheckDoesExistResult = (long)StateIs.Started;
            // Check to see if passed item exists in the file
            // TODO PickIdCheckDoesExist bNO for now xxx
            FmainPassed.Item.ItemIdExists = bNO;
            return FmainPassed.FileStatus.DoesExist;
        }
        // Get a Tilde Quoted Id
        public long PickIdGet(ref FileMainDef FmainPassed) {
            PickIdGetResult = (long)StateIs.Started;
            // Get / check for Id at beginning of item
            sTemp1 = FmainPassed.Item.ItemData.GetField(FmainPassed.Buf.CharIndex);
            sTemp5 = PickIdGetString(sTemp1, RowSeparator);
            //
            if (sTemp5.Length > 0) {
                FmainPassed.Fs.ItemIdCurrent = PickField(sTemp5, " ", 1);
                FmainPassed.Item.ItemId = FmainPassed.Fs.ItemIdCurrent;
                // 
                sTemp3 = PickIdGetFileString(sTemp5);
                FmainPassed.Item.ItemData = PickDel(FmainPassed.Item.ItemData, 1);
            } else {
                // no data in first column
                // InFile.Fmain.Item.ItemId = "";
                // Buf.CharIndex = 0;
                FmainPassed.ColIndex.ColText = FmainPassed.Item.ItemId;
                if (TraceBugOnNow) {
                    if (FmainPassed.ColIndex.ColText.Length > 100) {
                        LocalMessage.Msg9 = "Debug Point Reached, Column length greater than 100!!! " + FmainPassed.ColIndex.ColText.Length + "\n";
                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(),PickIdGetResult, TraceBugOnNow, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg9);
                        LocalMessage.Msg = LocalMessage.Msg9;
                    }
                }
                // Item.ItemIdChanged = bNO;
                FmainPassed.FileStatus.NameIsChanged = bNO;
                PickIdGetResult = (long)StateIs.DoesExist;
            }
            //
            return PickIdGetResult;
        }
        // Get a Tilde Quoted Id
        public String PickIdGetFileString(String StringPassed) {
            PickIdGetFileStringResult = (long)StateIs.Started;
            //
            // Get / check for Id at beginning of item
            sTemp3 = "";
            sField_ConversionRootVerb = StringPassed;
            // Check for embedded File Name in Tilde ID
            // Allow for ~ItemName FileName~
            sTemp3 = PickField(sField_ConversionRootVerb, " ", 2);
            //
            return sTemp3;
        }
        // Get a Tilde Quoted Id
        public String PickIdGetString(String PassedPickTildeId, String PassedQuoteCharacter) {
            PickIdGetStringResult = (long)StateIs.Started;
            sTemp4 = PassedPickTildeId;
            // Tilde Id Handling
            // Ids start and end with a Tilde,
            // with a CrLf following.
            // which as serveds as a RowSeparator
            // This implies that Ids are on a row by themselves.
            // meaning IDs are in a row by themselves basically
            // TODO z$NOTE for future: Needs a different Row Character from Id Quote character.
            if (sTemp4.Length > (2 * PassedQuoteCharacter.Length)) {
                //
                ColPick.ColCharacter = PassedPickTildeId.Substring((sTemp4.Length - PassedQuoteCharacter.Length), PassedQuoteCharacter.Length);
                //
                // sLine starts and ends with a TLD
                if (PassedPickTildeId.Substring(0, PassedQuoteCharacter.Length) == PassedQuoteCharacter && PassedPickTildeId.Substring(PassedPickTildeId.Length - PassedQuoteCharacter.Length, PassedQuoteCharacter.Length) == PassedQuoteCharacter) {
                    //
                    if (sTemp4.Length > (2 * PassedQuoteCharacter.Length)) {
                        //
                        sTemp4 = PassedPickTildeId.Substring(1, PassedPickTildeId.Length - (2 * PassedQuoteCharacter.Length));
                    } else { sTemp4 = ""; }
                } else { sTemp4 = ""; }
            } else { sTemp4 = ""; }
            return sTemp4;
        }
        // Pick Id Changed
        public long PickIdChanged(ref FileMainDef FmainPassed, String ItemIdCurrent) {
            PickIdChangedResult = (long)StateIs.Started;
            // Print
            // XUomMovvXv.PrintOutputMdm_PickPrint(Sender, 3, (bool)bYES);
            // XUomMovvXv.PrintOutputMdm_PickPrint(Sender, 3, bYES, 5);
            LocalMessage.Msg6 = "File Id: Current: \"" + FmainPassed.Item.ItemId + "\"";
            LocalMessage.Msg6 += ", Id length(" + FmainPassed.Item.ItemId.Length.ToString() + ")";
            switch (FmainPassed.Fs.FileTypeMajorId) {
                case ((long)FileType_LevelIs.Data):
                    // Processing Data
                    switch (FmainPassed.Fs.FileSubTypeMajorId) {
                        case ((long)FileType_SubTypeIs.SQL):
                        case ((long)FileType_SubTypeIs.MS):
                        case ((long)FileType_SubTypeIs.MY):
                        case ((long)FileType_SubTypeIs.CSV):
                            LocalMessage.Msg6 += ", Data Count (" + FmainPassed.DelSep.ItemAttrCounter.ToString() + ")";
                            LocalMessage.Msg6 += ", Columns (" + FmainPassed.ColIndex.ToString() + ")";
                            LocalMessage.Msg6 += ", At Positon (" + TraceMdmCounterLevel1GetDefault().ToString() + ")";
                            break;
                        case ((long)FileType_SubTypeIs.ASC):
                        case ((long)FileType_SubTypeIs.DAT):
                        case ((long)FileType_SubTypeIs.FIX):
                            // TODO Implement ID Changed for Text SubType - not supported
                            break;
                        default:
                            break;
                    }
                    break;
                //
                case ((long)FileType_LevelIs.DictData):
                    // FileDictData
                    switch (FmainPassed.Fs.FileSubTypeMajorId) {
                        case ((long)FileType_SubTypeIs.SQL):
                        case ((long)FileType_SubTypeIs.MS):
                        case ((long)FileType_SubTypeIs.MY):
                        case ((long)FileType_SubTypeIs.CSV):
                            LocalMessage.Msg6 += " Dictionary Count (" + FmainPassed.ColIndex.ColCountTotal.ToString() + ")";
                            LocalMessage.Msg6 += " Attrs (" + FmainPassed.ColIndex.ToString() + ")";
                            LocalMessage.Msg6 += ", At Positon (" + TraceMdmCounterLevel1GetDefault().ToString() + ")";
                            break;
                        case ((long)FileType_SubTypeIs.TEXT
                        | (long)FileType_SubTypeIs.Tilde):
                        case ((long)FileType_SubTypeIs.ASC):
                        case ((long)FileType_SubTypeIs.DAT):
                        case ((long)FileType_SubTypeIs.FIX):
                            // TODO Implement ID Changed for Text SubType - not supported
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    // FileTypeUNKNOWN
                    ColPointerIncrementResult = (long)StateIs.Undefined;
                    LocalMessage.Msg5 = "File Type Error (" + FmainPassed.Fs.FileTypeId.ToString() + ") not properly set";
                    throw new NotSupportedException(LocalMessage.Msg5);
            } // end or is DATA Attr not DICT
            //
            //if (ConsoleVerbosity >= 5) {
            //    if (ConsolePickConsoleOn) {
            // XUomMovvXv.PrintOutputMdm_PickPrint(Sender, 2, "A1" + Fs.ItemIdCurrent, bYES);
            XUomMovvXv.PrintOutputMdm_PickPrint(Sender, 2, "A1" + LocalMessage.Msg6, bYES);
            XUomMovvXv.PrintOutputMdm_PickPrint(Sender, 2, "A2" + LocalMessage.Msg6, bYES);
            //    }
            //}
            //
            // Item Above Attr Index
            FmainPassed.Item.ItemId = FmainPassed.Fs.ItemIdCurrent;
            //
            // Move Buffer if present
            if (FmainPassed.Buf.FileWorkBuffer.Length > 0) { PickIdChangedResult = PickBufferMoveToItemData(ref FmainPassed); }
            //
            // Write Output File Item
            if (!IterationFirst) {
                if (FmainPassed.Item.ItemId.Length > 0 && FmainPassed.Item.ItemData.Length > 0) {
                    // WRITE CURRENT ITEM
                    if (ConsolePickConsoleOn) {
                        LocalMessage.Msg6 = "File Id: Write Item: Pick Id \"" + FmainPassed.Item.ItemId + "\"";
                        LocalMessage.Msg6 += ", Id length: (" + FmainPassed.Item.ItemId.Length.ToString() + ")";
                        // XUomMovvXv.PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg6, bYES);
                        XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(),PickIdGetResult, TraceBugOnNow, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg6 + "\n");
                    }
                    //
                    LocalId.LongResult = FileWrite(ref FmainPassed);
                    //
                    PickDictItem.ColInvalid = 0;
                    // m/b wrong ColIndex.ColInvalid = 0;
                }
                // // Shift Data Item if more than 1000 Attrs
                if (FmainPassed.DelSep.ItemAttrCounter > 1000) {
                    PickIdChangedResult = ShiftCheck(ref FmainPassed);
                }
                /*
                if (Item.ItemData.Length > 500000) {
                    iIterationDebugCount = 5000;
                } else if (Item.ItemData.Length > 5000) {
                    iIterationDebugCount = 1000;
                } else if (Item.ItemData.Length > 1000) {
                    iIterationDebugCount = 500;
                }
                if (iIterationCount >= iIterationDebugCount) {
                    iIterationCount = 1;
                }
                */
            }
            // Reset Item Types
            // Reset Output Data Item
            switch (FmainPassed.Fs.FileTypeMajorId) {
                case ((long)FileType_LevelIs.DictData):
                    PickIdChangedResult = FileDictClearCurrent(ref FmainPassed);
                    break;
                case ((long)FileType_LevelIs.Data):
                default:
                    PickIdChangedResult = FileDataClearCurrent(ref FmainPassed);
                    break;
            }
            // Display Line Number Reset
            PickDisplayLineNumber = 0;
            // Load Input and Output Id's with Next item ID
            FmainPassed.Fs.ItemIdCurrent = FmainPassed.Fs.ItemIdNext;
            FmainPassed.Item.ItemId = FmainPassed.Fs.ItemIdCurrent;
            FmainPassed.Fs.ItemIdNext = "";
            //
            LocalMessage.Msg6 = "Pick Id Next: \"" + FmainPassed.Item.ItemId + "\"";
            // XUomMovvXv.PrintOutputMdm_PickPrint(Sender, 3, "A2" + LocalMessage.Msg6, bYES);
            // FileTransformControl.ColText = Item.ItemId;
            if (TraceBugOnNow && iTraceDisplayCountTotal > iTraceBugThreshold) {
                LocalMessage.Msg9 = ", Attr statistics: " + FmainPassed.Item.ItemData.GetStatistics();
                XUomMovvXv.TraceMdmDoImpl(ref Sender, bIsMessage, XUomMovvXv.TraceMdmCounterLevel1GetDefault(), XUomMovvXv.TraceMdmCounterLevel2GetDefault(),PickIdChangedResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "A2" + LocalMessage.Msg6 + "\n");
            }
            //
            FmainPassed.Item.ItemIdIsChanged = bNO;
            return PickIdChangedResult;
        }
        #endregion
        #endregion

        public override String ToString() {
            if (Fmain.Fs.FileId.FileName != null && Fmain.Fs.DirectionName != null) {
                String sTemp = "File Summary: " + Fmain.Fs.FileId.FileName + " for " + Fmain.Fs.DirectionName;
                return sTemp;
            } else { return base.ToString(); }
            return "";
        }
    }
}
// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
//
namespace Mdm.Oss.FileUtil {

    public class FileTransformControlPickDef : FileTransformControlDef {
        public FileTransformControlPickDef()
            : base() {
            //InputFsCurr = new FileSummaryPickDef();
            //OutputFsCurr = new FileSummaryPickDef();
        }
        #region InputItem Declaration
        // InFile
        public new MfilePickDb InFile;
        //public new FileSummaryPickDef InputFsCurr;
        #endregion
        #region FileOutputItem Declaration
        public new MfilePickDb OutFile;
        //public new FileSummaryPickDef OutputFsCurr;
        #endregion
    }

    //public class FileSummaryPickDef : FileSummaryDef {
    //    public FileSummaryPickDef()
    //        : base() {
    //        //
    //    }
    //    #region File Declarations
    //    public new MfilePickDb FileObject;
    //    #endregion
    //}

    //public class FileCommandPickDef : FileCommandDef {
    //    public FileCommandPickDef()
    //        : base() {
    //        //
    //    }
    //    public new FileSummaryPickDef Fs;
    //}

    //public class FileMainPickDef : FileMainDef {
    //    public FileMainPickDef()
    //        : base() {
    //        Fs = new FileSummaryPickDef();
    //    }
    //    public new FileSummaryPickDef Fs;
    //}

}