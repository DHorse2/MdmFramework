#region System
using System;
#endregion
#region System Collections
using System.Collections;
using System.Collections.Generic;
//using System.Collections.Specialized;
#endregion
#region System Data & IO
using System.IO;
using System.Data;
//using System.Data.Common;
#endregion
#region System SQL
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
#endregion
#region System Text and Linq
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
#endregion
#region System Serialization (Runtime and Xml)
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
#endregion

#region  Mdm Core
using Mdm.Oss;
//@@@CODE@@@using Mdm.Oss.ClipUtil;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Components;
using Mdm.World;
#endregion
#region  Mdm MVC Mobject
using Mdm.Oss.Mobj;
#endregion
#region  Mdm Db and File
using Mdm.Oss.File;
using Mdm.Oss.File.Control;
using Mdm.Oss.File.Db;
using Mdm.Oss.File.Db.Data;
using Mdm.Oss.File.Db.Table;
using Mdm.Oss.File.Db.Thread;
//using Mdm.Oss.File.Properties;
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
//using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion

namespace Mdm.Oss.File
{
    public partial class mFileDef : Mobject, ImFileType, IDisposable
    {
        #region Class Methods
        #region Base File Open Read Write Close
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region FileNameBuild
        // <Section Id = "FileNameLineBuild">
        /// <summary> 
        /// Build the File Name Line from the File Info for the Passed File.
        /// </summary> 
        public String FileNameLineBuild(ref mFileMainDef FmainPassed)
        {
            FileState.FileNameBuildFullResult = StateIs.Started;
            Fmain.Fs.FileId.FileNameLine = FmainPassed.Fs.FileId.FileNameLineBuild(ref FmainPassed);
            return Fmain.Fs.FileId.FileNameLine;
        }
        // <Section Id = "FileNameLineBuild">
        /// <summary> 
        /// Build the File Name Line from the File Info.
        /// </summary> 
        public StateIs FileNameLineBuild()
        {
            FileState.FileNameBuildFullResult = StateIs.Started;
            Fmain.Fs.FileId.FileNameLine = FileNameLineBuild(ref Fmain);
            return (FileState.FileNameBuildFullResult = StateIs.Successful);
        }
        #endregion
        #region READ
        /// <summary> 
        /// Perform a low level file seek on file.
        /// </summary> 
        public virtual StateIs FileSeek(ref mFileMainDef FmainPassed, int iPassedOffsetModulo, int iPassedOffsetRemainder, long iPassedFileSeekMode)
        {
            // 
            //  ToDo z$RelVs? (when needed) FileSeek SEEK Read a Buffer / Text block from Win32 File Handle
            // 
            StateIs FileSeek = StateIs.Started;
            FmainPassed.Buf.BytesRead = 0;
            // FmainPassed.Buf.BytesConverted = 0;
            // FmainPassed.Buf.BytesConvertedTotal = 0;
            FileSeek = StateIs.Failed;
            //
            return FileSeek;
        }

        /// <summary> 
        /// Perform a read line.
        /// </summary> 
        public virtual StateIs FileReadLine(ref mFileMainDef FmainPassed, String PassedDosRecordBuffer, int iPassedRecordSize)
        {
            // 
            // ToDo $$$CHECK Buf.FileReadLine Read Line from Ascii File
            // 
            FileState.FileReadLineResult = StateIs.Started;
            if (true == false)
            {
                FmainPassed.Buf.BytesRead = 0;
                // FmainPassed.Buf.BytesConverted = 0;
                // FmainPassed.Buf.BytesConvertedTotal = 0;
            }
            FmainPassed.Fs.FileIo.IoReadBuffer = sEmpty;
            // o+fPassedFileObject.FileIo.IoReadBuffer

            FileState.FileReadLineResult = AsciiFileReadAll(ref FmainPassed);
            // FmainPassed.IoAll += PassedDosRecordBuffer;
            // FmainPassed.IoLine += PassedDosRecordBuffer;
            if (true == false)
            {
                FmainPassed.Buf.BytesRead = FmainPassed.Fs.FileIo.IoReadBuffer.Length;
                // FmainPassed.Buf.BytesConverted = 0;
                // FmainPassed.Buf.BytesConvertedTotal = 0;
            }
            if (FmainPassed.Buf.BytesRead < 1)
            {
                FileState.FileReadLineResult = StateIs.Failed;
            }
            else
            {
                FileState.FileReadLineResult = StateIs.Successful;
                FmainPassed.Buf.FileWorkBuffer += FmainPassed.Fs.FileIo.IoReadBuffer;
                if (true == false)
                {
                    FmainPassed.Buf.BytesReadTotal += FmainPassed.Buf.BytesRead;
                    // FmainPassed.Buf.BytesConverted = 0;
                    // FmainPassed.Buf.BytesConvertedTotal = 0;
                }
                FmainPassed.Buf.FileWorkBuffer += FmainPassed.Fs.FileIo.IoReadBuffer;
            }
            FmainPassed.Buf.CharMaxIndex = FmainPassed.Buf.FileWorkBuffer.Length;
            //
            return FileState.FileReadLineResult;
        }

        /// <summary> 
        /// Perform a read all content for item.
        /// </summary> 
        public virtual StateIs FileReadAll(ref mFileMainDef FmainPassed, ref String PassedDosRecordBuffer, int iPassedRecordSize)
        {
            // 
            // Read All Lines from Ascii File
            // 
            FileState.FileReadAllResult = StateIs.Started;
            if (true == false)
            {
                FmainPassed.Buf.BytesRead = 0;
                // FmainPassed.Buf.BytesConverted = 0;
                // FmainPassed.Buf.BytesConvertedTotal = 0;
            }
            FmainPassed.Fs.FileIo.IoReadBuffer = sEmpty;
            //
            FileState.FileReadAllResult = AsciiFileReadAll(ref FmainPassed);
            //
            if (true == false)
            {
                FmainPassed.Buf.BytesRead = FmainPassed.Fs.FileIo.IoReadBuffer.Length;
                // FmainPassed.Buf.BytesConverted = 0;
                // FmainPassed.Buf.BytesConvertedTotal = 0;
            }
            // FmainPassed.FileWorkBuffer = FmainPassed.Fs.FileIo.IoReadBuffer;
            if (FmainPassed.Buf.BytesRead < 1)
            {
                FileState.FileReadAllResult = StateIs.Failed;
            }
            else
            {
                FileState.FileReadAllResult = StateIs.Successful;
                FmainPassed.Buf.FileWorkBuffer += FmainPassed.Fs.FileIo.IoReadBuffer;
                if (true == false)
                {
                    FmainPassed.Buf.BytesReadTotal += FmainPassed.Buf.BytesRead;
                    // FmainPassed.Buf.BytesConverted = 0;
                    // FmainPassed.Buf.BytesConvertedTotal = 0;
                }
            }
            FmainPassed.Buf.CharMaxIndex = FmainPassed.Buf.FileWorkBuffer.Length;
            //
            return FileState.FileReadAllResult;
        }
        #endregion
        #region CLOSE
        /// <summary> 
        /// Close low level file handles, unmanaged objects, etc.
        /// </summary> 
        public virtual StateIs FileCloseHandle(ref mFileMainDef FmainPassed)
        {
            // 
            // Close Win32 Dos File Handle
            // 
            FileState.FileCloseHandleResult = StateIs.Started;
            FmainPassed = null;
            //
            return FileState.FileCloseHandleResult;
        }
        // ToDo $$$CHECK FileReadLineClose Requires correction for handle????
        /// <summary> 
        /// Close the File Line Reader.
        /// </summary> 
        public virtual StateIs FileReadLineClose(ref mFileMainDef FmainPassed)
        {
            // 
            // Close Read Line Ascii File
            //
            FileState.FileCloseResult = StateIs.Started;
            FmainPassed = null;
            //
            return FileState.FileCloseResult;
        }
        /// <summary> 
        /// Close the File.
        /// </summary> 
        public virtual StateIs FileClose(ref mFileMainDef FmainPassed)
        {
            FileState.FileCloseResult = StateIs.Started;
            // ofPassedFileName = sEmpty;
            // ToDo Add current totals to grand totals ??
            return FileState.FileCloseResult;
        }
        #endregion
        #region OPEN
        /// <summary> 
        /// Open a low level file handle.
        /// </summary> 
        public virtual StateIs FileOpenHandle(ref mFileMainDef FmainPassed, String PassedName, FileAction_ToDoIs FileActionToDoPassed, long iPassedFileOpenOptions)
        {
            // 
            // Open Win32 Dos File Handle
            // 
            FileState.FileOpenHandleResult = StateIs.Started;
            //
            return FileState.FileOpenHandleResult;
        }

        /// <summary> 
        /// Open the file.
        /// </summary> 
        public virtual StateIs FileOpen(ref mFileMainDef FmainPassed, String PassedName, FileAction_ToDoIs FileActionToDoPassed, long iPassedFileOpenOptions)
        {
            // 
            // Open Text Ascii File
            // 
            FileState.FileOpenResult = StateIs.Started;
            FmainPassed.Buf.BytesRead = 0;
            FmainPassed.Buf.BytesReadTotal = 0;
            FmainPassed.Buf.BytesConverted = 0;
            FmainPassed.Buf.BytesConvertedTotal = 0;

            FmainPassed.Buf.WriteFileCounter = 0;
            FmainPassed.Buf.ReadFileCounter = 0;

            FileState.FileOpenResult = TextFileOpen(ref FmainPassed);
            if (true == false)
            {
                FmainPassed.Buf.BytesRead = FmainPassed.Fs.FileIo.IoReadBuffer.Length; // ToDo z$NOTE FileOpen this is an open, not a read.
            }
            return FileState.FileOpenResult;
        }

        #endregion
        #region WRITE
        /// <summary> 
        /// Write the file data or item.
        /// </summary> 
        public virtual StateIs FileWrite(ref mFileMainDef FmainPassed)
        {
            FileState.FileWriteResult = StateIs.Started;
            //
            try
            {
                switch (FmainPassed.Fs.MetaLevelId)
                {
                    case (FileType_LevelIs.Data):
                        // Handle File Data
                        // ToDo z$NOTE FileWrite Empty Records (?null?) are allowed in some types!
                        //if (FileSqlConn.DbSyn.OutputInsert.Length > 0)
                        {
                            switch (FmainPassed.Fs.FileSubTypeMajor)
                            {
                                case (FileType_SubTypeIs.SQL):
                                case (FileType_SubTypeIs.MS):
                                case (FileType_SubTypeIs.MY):
                                //mFileState.FileWriteResult = SqlDataInsert(ref FmainPassed);
                                //FmainPassed.Buf.WriteFileCounter += 1;
                                //break;
                                case (FileType_SubTypeIs.TEXT
                                | FileType_SubTypeIs.Tilde):
                                    // Text
                                    FmainPassed.Buf.WriteFileCounter += 1;
                                    break;
                                default:
                                    FileState.FileWriteResult = StateIs.Undefined;
                                    LocalMessage.Msg6 = "Main Application - File Subtype (" + FmainPassed.Fs.FileSubType.ToString() + ") not properly set";
                                    LocalMessage.ErrorMsg = LocalMessage.Msg6;
                                    ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileState.FileWriteResult);
                                    throw new NotSupportedException(LocalMessage.Msg6);
                                    break;
                            }
                        }
                        break;
                    case (FileType_LevelIs.DictData):
                        // FileDictData
                        // FileDictData
                        // Handle File Schema and Data
                        FmainPassed.ColIndex.ColId = FmainPassed.Item.ItemId;
                        // 
                        // not buffer Attr, s/b output dict item max Attr!
                        // ToDo z$NOTE Empty Records (?null?) are allowed in some types!
                        // FileTransformControl.iInputBufferAttrIndex?
                        if (FmainPassed.Item.ItemData.Length > 0)
                        {
                            switch (FmainPassed.Fs.FileSubTypeMajor)
                            {
                                case (FileType_SubTypeIs.SQL):
                                case (FileType_SubTypeIs.MS):
                                case (FileType_SubTypeIs.MY):
                                //mFileState.FileWriteResult = SqlDictArrayInsert(ref FmainPassed, "error?");
                                //FmainPassed.Buf.WriteFileCounter += 1;
                                //break;
                                case (FileType_SubTypeIs.TEXT
                                | FileType_SubTypeIs.Tilde):
                                    // Text
                                    FmainPassed.Buf.WriteFileCounter += 1;
                                    break;
                                default:
                                    FileState.FileWriteResult = StateIs.Undefined;
                                    LocalMessage.Msg6 = "Main Application - File Subtype (" + FmainPassed.Fs.FileSubType.ToString() + ") not properly set";
                                    //throw new NotSupportedException(LocalMessage.Msg6);
                                    LocalMessage.ErrorMsg = LocalMessage.Msg6;
                                    ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileState.FileWriteResult);
                                    break;
                            }
                            FmainPassed.FileSqlConn.DbSyn.OutputValues = sEmpty;  // Values for Insert, Update, Delete
                        }
                        break;
                    default:
                        // FileTypeUNKNOWN
                        FileState.ColPointerIncrementResult = StateIs.Undefined;
                        LocalMessage.Msg5 = "File Type Error (" + FmainPassed.Fs.FileType.ToString() + ") not properly set";
                        LocalMessage.ErrorMsg = LocalMessage.Msg5;
                        ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileState.FileWriteResult);
                        throw new NotSupportedException(LocalMessage.Msg);
                        break;
                } // end or is DATA Attr not DICT
            }
            catch (NotSupportedException nse)
            {
                LocalMessage.ErrorMsg = "Not Supported Exception(#100) occured in File Action";
                ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileState.FileWriteResult);
                FileState.FileWriteResult = StateIs.Failed;
            }
            catch (SqlException ExceptionSql)
            {
                LocalMessage.ErrorMsg = "SQL Exception occured in File Write";
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.FileWriteResult);
                FileState.FileWriteResult = StateIs.DatabaseError;
            }
            catch (Exception ExceptionGeneral)
            {
                LocalMessage.ErrorMsg = "Unhandled Exception(#111) occured in File Action";
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.FileWriteResult);
                FileState.FileWriteResult = StateIs.Failed;
            }
            finally
            {
                // mFileState.FileWriteResult = StateIs.Failed;
            }

            // FileWrite
            return FileState.FileWriteResult;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        #region File Actions, State Description and Results
        //
        #region File IO Error Description
        /// <summary> 
        /// Get a File IO Error Description.
        /// </summary> 
        public String FileIoErrorDescriptionGet(FileIo_ErrorIs ResultPassed)
        {
            String FileIoErrorDescription = sEmpty;
            switch (ResultPassed)
            {
                case (FileIo_ErrorIs.AccessError):
                    FileIoErrorDescription = "information accesss error";
                    break;
                case (FileIo_ErrorIs.DatabaseError):
                    FileIoErrorDescription = "database access error";
                    break;
                case (FileIo_ErrorIs.DiskError):
                    FileIoErrorDescription = "disk access error";
                    break;
                case (FileIo_ErrorIs.DiskFull):
                    FileIoErrorDescription = "disk is full";
                    break;
                case (FileIo_ErrorIs.General):
                    FileIoErrorDescription = "general exception error";
                    break;
                case (FileIo_ErrorIs.InternetError):
                    FileIoErrorDescription = "Internet access error";
                    break;
                case (FileIo_ErrorIs.IoError):
                    FileIoErrorDescription = "general IO error";
                    break;
                case (FileIo_ErrorIs.NetworkError):
                    FileIoErrorDescription = "network access errror";
                    break;
                case (FileIo_ErrorIs.None):
                    FileIoErrorDescription = "no error occured";
                    break;
                case (FileIo_ErrorIs.NotCompleted):
                    FileIoErrorDescription = "action not completed error";
                    break;
                case (FileIo_ErrorIs.NotSupported):
                    FileIoErrorDescription = "software function not supported";
                    break;
                case (FileIo_ErrorIs.OsError):
                    FileIoErrorDescription = "operating system error";
                    break;
                case (FileIo_ErrorIs.SqlError):
                    FileIoErrorDescription = "SQL database error";
                    break;
                default:
                    FileIoErrorDescription = "Unknown error" + " (" + ResultPassed + ")";
                    break;
            }
            if (TraceBugOnNow && TraceDisplayCountTotal > TraceBugThreshold)
            {
                LocalMessage.Msg9 = "\n" + FileIoErrorDescription;
               st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.DelSep.ItemAttrMaxIndex, FileState.ColPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg9);
            }
            return FileIoErrorDescription;
        }
        #endregion
        //

        /// <summary> 
        /// Get a File Object Header string
        /// </summary> 
        public String FileMainHeaderGetTo(ref mFileMainDef FmainPassed)
        {
            LocalMessage.Header = sEmpty;
            try
            {
                LocalMessage.Header += "File (" + FmainPassed.Fs.FileId.FileId + ") " + FmainPassed.Fs.FileNameLineGetFrom(ref FmainPassed.Fs);
                LocalMessage.Header += ", Action (" + FmainPassed.FileAction.ToDo + ") " + FmainPassed.FileAction.Name;
                LocalMessage.Header += ", Mode (" + FmainPassed.FileAction.ModeName + ") " + FmainPassed.FileAction.ModeName;
                LocalMessage.Header += ", Direction (" + FmainPassed.FileAction.Direction + ") " + FmainPassed.FileAction.Direction;
                LocalMessage.Header += ".";
            }
            catch
            {
                LocalMessage.Header += ", there is invalid header data!!!";
            }
            return LocalMessage.Header;
        }

        #region File State Description
        /// <summary> 
        /// Get the State Description for the current File Action.
        /// </summary> 
        public void StateDescriptionGetTo(ref mFileMainDef FmainPassed)
        {
            #region File Object Result
            FileDoResult = FileState.FileDoOpenResult | FileState.FileDoCloseResult | FileState.FileDoCheckResult | FileState.FileDoGetResult;
            FmainPassed.FileAction.Result = FileDoResult;
            FmainPassed.FileAction.ActionInfo.Result = FmainPassed.FileAction.Result;
            // Name.StateDescription = sEmpty;
            LocalMessage.Msg6 = FmainPassed.FileAction.ModeName;
            try
            {
                // FmainPassed.FileAction.ActionInfo.StateDescription = FmainPassed.FileAction.Result.ToString();
                FmainPassed.FileAction.ActionInfo.ResultName = FmainPassed.FileAction.ResultName;
                FmainPassed.FileAction.ActionInfo.ResultDescription = "File Action(#110) result was "
                + FmainPassed.FileAction.ActionInfo.ResultName + ".";
            }
            catch (IOException ExceptionIO)
            {
                FmainPassed.FileAction.ResultName = "Action Result Error";
                FmainPassed.FileAction.ActionInfo.ResultName = FmainPassed.FileAction.ResultName;
                FmainPassed.FileAction.ActionInfo.ResultDescription = "Error converting File Action(#11) result";
                LocalMessage.ErrorMsg = FmainPassed.FileAction.ActionInfo.ResultDescription;
                ExceptIoImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionIO, FmainPassed.FileAction.Result);
                // throw new IOException (LocalMessage.ErrorMsg);
                RunErrorDidOccur = true;
                LocalMessage.Msg6 += FmainPassed.FileAction.Name + " Failed with (" + FileDoResult + "), Verb Result (" + FmainPassed.FileAction.ActionInfo.omvOfResult + ") not properly set";
                //((ImTrace)ConsoleSender).TraceMdmDoImpl(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickSystemCallStringResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                //throw new NotSupportedException(LocalMessage.ErrorMsg);
                LocalMessage.ErrorMsg = LocalMessage.Msg6;
                ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileState.FileWriteResult);
            } // end of switch FmainPassed.omvOfResult
            #endregion
        }
        #endregion

        /// <summary> 
        /// Build the File Action Long Description string.
        /// </summary> 
        public void ResultDescriptionLong(ref mFileMainDef FmainPassed)
        {
            string sTemp2, sTemp3, sTemp4;
            sTemp2 = FmainPassed.Fs.FileNameLineGetFrom(ref FmainPassed.Fs);
            //if (sTemp2.Length > 60) { sTemp2 = sTemp2.Substring(0, 57) + "..."; }
            sTemp3 = "The result "
                + " on file " + sTemp2 + ": ";
            sTemp4 = "The " + FmainPassed.FileAction.Name
                + " for direction " + FmainPassed.FileAction.DirectionName
                + " (using mode " + FmainPassed.FileAction.ModeName + ")"
                + " had the result: "
                + FmainPassed.FileAction.ResultName + " "
                + "(" + FileDoResult + ")."
                + " Return result (" + FileDoResult + ")."
                ;
            LocalMessage.Msg6 += sTemp3 + sTemp4;
           st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 4, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), FileState.FileDoOpenResult, RunErrorDidOccur = false, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg6);

            //PrintOutputMdm_PickPrint(Sender, 3, sTemp3, bYES);
            //PrintOutputMdm_PickPrint(Sender, 3, sTemp4, bYES);

        }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        #region Check Buffer Shift
        public long ShiftCheckPointer; // This is not a state. It is a location.
        #region Increment Column Pointer
        public long FileIndexPointerIncrement; // This is not a state. It is a location.
        /// <summary> 
        /// Increment the File Index Pointer.
        /// </summary> 
        public long FileIndexPointerIncrementDo(ref mFileMainDef FmainPassed)
        {
            FileState.FileIndexPointerIncrementResult = StateIs.Started;
            //
            // Increment Column Pointer
            //
            IterationCount += 1;
            //
            if (FmainPassed.DelSep.ItemAttrCounter > (TraceShiftIndexByCount / 10) && TraceByteCount > (5 * TraceShiftIndexByCount))
            {
                FileIndexPointerIncrement = ShiftCheck(ref FmainPassed);
            }
            if (ConsoleVerbosity >= 10)
            {
                LocalMessage.Msg0 = "Increment: Input (" + FmainPassed.DelSep.ItemAttrCounter.ToString() + ")";
                LocalMessage.Msg0 += "FileIndexPointerIncrement: " + FileIndexPointerIncrement.ToString();
                if (TraceOn) {st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.DelSep.ItemAttrMaxIndex, FileState.FileIndexPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg0); }
            }
            // (Mapplication)XUomMovvXv.XUomMovvXv.XUomPmvvXv.ProgressBarMdm1.Value += 1;
            // ToDo Post Back Progress Bar ItemData
            switch (FmainPassed.Fs.MetaLevelId)
            {
                case (FileType_LevelIs.Data):
                    // Processing ItemData
                    switch (FmainPassed.Fs.FileSubTypeMajor)
                    {
                        //////////////////////// TILDE ROW NOT BEING HANDLED
                        case (FileType_SubTypeIs.SQL):
                        case (FileType_SubTypeIs.MS):
                        case (FileType_SubTypeIs.MY):
                        case (FileType_SubTypeIs.CSV):
                        case (FileType_SubTypeIs.TEXT
                        | FileType_SubTypeIs.Tilde):
                            FmainPassed.DelSep.ItemAttrCount += 1; // total number columns.
                            FmainPassed.DelSep.ItemAttrCountTotal += 1; // total number columns.
                            FmainPassed.DelSep.ItemAttrCounter += 1; // move pointer
                            // Total # of Fields in Dict Item
                            // Increment Output Dictionary Pointer
                            if (FmainPassed.DelSep.ItemAttrIndex + 1 < 100)
                            {
                                FmainPassed.DelSep.ItemAttrIndex += 1;// Next Field within Dict
                            }
                            if (ConsoleVerbosity >= 10)
                            {
                                if (TraceOn || ConsoleOn)
                                {
                                    LocalMessage.Msg5 = "Output ItemData: ";
                                    LocalMessage.Msg5 += " Count (" + FmainPassed.DelSep.ItemAttrCounter.ToString() + ")";
                                    LocalMessage.Msg5 += ", Column (" + FmainPassed.DelSep.ItemAttrCounter.ToString() + ")";
                                    LocalMessage.Msg5 += "FileIndexPointerIncrement: " + FileIndexPointerIncrement.ToString();
                                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.DelSep.ItemAttrMaxIndex, FileState.FileIndexPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg5);
                                }
                            }
                            break;
                        case (FileType_SubTypeIs.ASC):
                        case (FileType_SubTypeIs.DAT):
                        case (FileType_SubTypeIs.FIX):
                        default:
                            // Subtype error
                            FileIndexPointerIncrement = 0;
                            LocalMessage.Msg5 = "File SubType Error (" + FmainPassed.Fs.FileSubType.ToString() + ") not properly set";
                            //throw new NotSupportedException(LocalMessage.Msg5);
                            LocalMessage.Msg5 += "FileIndexPointerIncrement: " + FileIndexPointerIncrement.ToString();
                            LocalMessage.ErrorMsg = LocalMessage.Msg5;
                            ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileState.FileWriteResult);
                            break;
                    }
                    break;
                //
                case (FileType_LevelIs.DictData):
                    // FileDictData
                    switch (FmainPassed.Fs.FileSubTypeMajor)
                    {
                        case (FileType_SubTypeIs.SQL):
                        case (FileType_SubTypeIs.MS):
                        case (FileType_SubTypeIs.MY):
                        case (FileType_SubTypeIs.CSV):
                            Fmain.ColIndex.ColCount += 1;
                            Fmain.ColIndex.ColCountTotal += 1;
                            Fmain.ColIndex.ColCounter += 1;
                            // Total # of Fields in Dict Item
                            // Increment Output Dictionary Pointer
                            if (Fmain.ColIndex.ColIndex + 1 < 100)
                            {
                                Fmain.ColIndex.ColIndex += 1;// Next Field within Dict
                            }
                            //
                            if (ConsoleVerbosity >= 10)
                            {
                                if (TraceOn || ConsoleOn)
                                {
                                    LocalMessage.Msg5 = "Output Dictionary: ";
                                    LocalMessage.Msg5 += " Count (" + Fmain.ColIndex.ColCount.ToString() + ")";
                                    LocalMessage.Msg5 += ", Attr (" + Fmain.ColIndex.ToString() + ")";
                                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.DelSep.ItemAttrMaxIndex, FileState.FileIndexPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg5);
                                }
                            }
                            break;
                        case (FileType_SubTypeIs.TEXT
                        | FileType_SubTypeIs.Tilde):
                        case (FileType_SubTypeIs.ASC):
                        case (FileType_SubTypeIs.DAT):
                        case (FileType_SubTypeIs.FIX):
                        default:
                            // SubType Error 
                            FileIndexPointerIncrement = 0;
                            LocalMessage.Msg5 = "File SubType Error (" + FmainPassed.Fs.FileSubType.ToString() + ") not properly set";
                            //throw new NotSupportedException(LocalMessage.Msg5);
                            LocalMessage.ErrorMsg = LocalMessage.Msg5;
                            ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileState.FileWriteResult);
                            break;
                    }
                    break;
                default:
                    // FileTypeUNKNOWN
                    FileIndexPointerIncrement = 0;
                    LocalMessage.Msg5 = "File Type Error (" + FmainPassed.Fs.FileType.ToString() + ") not properly set";
                    //throw new NotSupportedException(LocalMessage.Msg5);
                    LocalMessage.ErrorMsg = LocalMessage.Msg5;
                    ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileState.FileWriteResult);
                    break;
            } // end or is DATA Attr not DICT
            return FileIndexPointerIncrement;
        }
        #endregion
        #region Increment Column Pointer
        /// <summary> 
        /// Increment the Column Index Pointer.
        /// </summary> 
        public StateIs ColPointerIncrement(ref mFileMainDef FmainPassed)
        {
            FileState.ColPointerIncrementResult = StateIs.Started;
            if (FmainPassed.DelSep.ItemAttrCounter > (TraceShiftIndexByCount / 10) && TraceByteCount > (5 * TraceShiftIndexByCount))
            {
                long ThisWasSomethingElse = ShiftCheck(ref FmainPassed); // may be wrong now.
            }
            if (ConsoleVerbosity >= 10)
            {
                LocalMessage.Msg0 = "Increment: Input (" + FmainPassed.DelSep.ItemAttrCounter.ToString() + ")";
                if (TraceOn) {st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, FmainPassed.Buf.CharMaxIndex, FmainPassed.DelSep.ItemAttrMaxIndex, FileState.ColPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg0); }
            }
            // (Mapplication)XUomMovvXv.XUomMovvXv.XUomPmvvXv.ProgressBarMdm1.Value += 1;
            // ToDo Post Back Progress Bar ItemData
            switch (FmainPassed.Fs.MetaLevelId)
            {
                case (FileType_LevelIs.Data):
                    // Processing ItemData
                    switch (FmainPassed.Fs.FileSubTypeMajor)
                    {
                        //////////////////////// TILDE ROW NOT BEING HANDLED
                        case (FileType_SubTypeIs.SQL):
                        case (FileType_SubTypeIs.MS):
                        case (FileType_SubTypeIs.MY):
                        case (FileType_SubTypeIs.CSV):
                        case (FileType_SubTypeIs.TEXT
                        | FileType_SubTypeIs.Tilde):
                            break;
                        case (FileType_SubTypeIs.ASC):
                        case (FileType_SubTypeIs.DAT):
                        case (FileType_SubTypeIs.FIX):
                            // SubType Error 
                            FileState.ColPointerIncrementResult = StateIs.Undefined;
                            LocalMessage.Msg5 = "File SubType Error (" + FmainPassed.Fs.FileSubType.ToString() + ") not properly set";
                            //throw new NotSupportedException(LocalMessage.Msg5);
                            LocalMessage.ErrorMsg = LocalMessage.Msg5;
                            ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileState.FileWriteResult);
                            break;
                    }
                    if (Fmain.ColIndex.ColSet)
                    {
                        Fmain.ColIndex.ColCounter += 1;
                        // if (ColCounter > ColCount) { ColCounter = ColCount; }
                        // Next Field within Dict
                        if (Fmain.ColIndex.ColIndex + 1 < 100) { Fmain.ColIndex.ColIndex += 1; }
                        if (ConsoleVerbosity >= 10)
                        {
                            if (TraceOn || ConsoleOn)
                            {
                                LocalMessage.Msg5 = "File ItemData Column Increment: ";
                                LocalMessage.Msg5 += " Count (" + Fmain.ColIndex.ColCount.ToString() + ")";
                                LocalMessage.Msg5 += ", Column (" + Fmain.ColIndex.ColCounter.ToString() + ")";
                               st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.ColIndex.ColMaxIndex, FileState.ColPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg5);
                            }
                        }
                    }
                    // Total # of Fields in Dict Item
                    // Increment Output Dictionary Pointer
                    if (FmainPassed.DelSep.ItemAttrSet)
                    {
                        FmainPassed.DelSep.ItemAttrCounter += 1;
                        if (FmainPassed.DelSep.ItemAttrCounter > FmainPassed.DelSep.ItemAttrCount) { FmainPassed.DelSep.ItemAttrCount = FmainPassed.DelSep.ItemAttrCounter; }
                        if (FmainPassed.DelSep.ItemAttrIndex + 1 < 100) { FmainPassed.DelSep.ItemAttrIndex += 1; }
                        if (ConsoleVerbosity >= 10)
                        {
                            if (TraceOn || ConsoleOn)
                            {
                                LocalMessage.Msg5 = "File ItemData Column Attribue Increment: ";
                                LocalMessage.Msg5 += " Count (" + FmainPassed.DelSep.ItemAttrCount.ToString() + ")";
                                LocalMessage.Msg5 += ", Column (" + FmainPassed.DelSep.ItemAttrCounter.ToString() + ")";
                               st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.ColIndex.ColMaxIndex, FileState.ColPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg5);
                            }
                        }
                    }
                    break;
                //
                case (FileType_LevelIs.DictData):
                    // FileDictData
                    switch (FmainPassed.Fs.FileSubTypeMajor)
                    {
                        case (FileType_SubTypeIs.SQL):
                        case (FileType_SubTypeIs.MS):
                        case (FileType_SubTypeIs.MY):
                        case (FileType_SubTypeIs.CSV):
                            break;
                        case (FileType_SubTypeIs.TEXT
                        | FileType_SubTypeIs.Tilde):
                        case (FileType_SubTypeIs.ASC):
                        case (FileType_SubTypeIs.DAT):
                        case (FileType_SubTypeIs.FIX):
                        default:
                            // SubType Error 
                            FileState.ColPointerIncrementResult = StateIs.Undefined;
                            LocalMessage.Msg5 = "File SubType Error (" + FmainPassed.Fs.FileSubType.ToString() + ") not properly set";
                            //throw new NotSupportedException(LocalMessage.Msg5);
                            LocalMessage.ErrorMsg = LocalMessage.Msg5;
                            ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileState.FileWriteResult);
                            break;
                    }
                    if (Fmain.ColIndex.ColSet)
                    {
                        Fmain.ColIndex.ColCounter += 1;
                        // if (ColCounter > ColCount) { ColCounter = ColCount; }
                        // Next Field within Dict
                        if (Fmain.ColIndex.ColIndex + 1 < 100) { Fmain.ColIndex.ColIndex += 1; }
                        if (ConsoleVerbosity >= 10)
                        {
                            if (TraceOn || ConsoleOn)
                            {
                                LocalMessage.Msg5 = "File Dictionary Column Increment: ";
                                LocalMessage.Msg5 += " Count (" + Fmain.ColIndex.ColCount.ToString() + ")";
                                LocalMessage.Msg5 += ", Column (" + Fmain.ColIndex.ColCounter.ToString() + ")";
                               st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.ColIndex.ColMaxIndex, FileState.ColPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg5);
                            }
                        }
                    }
                    if (Fmain.ColIndex.ColAttrSet)
                    {
                        Fmain.ColIndex.ColAttrCounter += 1;
                        // if (ColAttrCounter > ColAttrCount) { ColAttrCount = ColAttrCounter; }
                        if (Fmain.ColIndex.ColAttrIndex + 1 < 100) { Fmain.ColIndex.ColAttrIndex += 1; }
                        //
                        if (ConsoleVerbosity >= 10)
                        {
                            if (TraceOn || ConsoleOn)
                            {
                                LocalMessage.Msg5 = "File Dictionary Column Attribue Increment: ";
                                LocalMessage.Msg5 += " Count (" + Fmain.ColIndex.ColAttrCount.ToString() + ")";
                                LocalMessage.Msg5 += ", Column (" + Fmain.ColIndex.ColAttrCounter.ToString() + ")";
                               st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, Fmain.Buf.CharMaxIndex, Fmain.ColIndex.ColMaxIndex, FileState.ColPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg5);
                            }
                        }
                    }
                    break;
                default:
                    // FileTypeUNKNOWN
                    FileState.ColPointerIncrementResult = StateIs.Undefined;
                    LocalMessage.Msg5 = "File Column Increment File Type Error (" + FmainPassed.Fs.FileType.ToString() + ") not properly set";
                    //throw new NotSupportedException(LocalMessage.Msg5);
                    LocalMessage.ErrorMsg = LocalMessage.Msg5;
                    ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileState.FileWriteResult);
                    break;
            } // end or is DATA Attr not DICT
            return FileState.ColPointerIncrementResult;
        }
        #endregion
        /// <summary> 
        /// Perform a File Buffer Shift Check.
        /// </summary> 
        public long ShiftCheck(ref mFileMainDef FmainPassed)
        {
            ShiftCheckPointer = 0;  // StateIs.Started;
            // Shift Input Buffer after TraceShiftIndexByCount lines or increment Input Buffer Index
            if (FmainPassed.DelSep.ItemAttrCounter > (TraceShiftIndexByCount / 10) && TraceByteCount > (5 * TraceShiftIndexByCount))
            {
                if (FmainPassed.DelSep.ItemAttrCounter > TraceShiftIndexByCount || TraceByteCount > (10 * TraceShiftIndexByCount))
                {
                    if (TraceBugOnNow && TraceDisplayCountTotal > TraceBugThreshold)
                    {
                        LocalMessage.Msg9 = "Debug Point Reached, ready to Shift buffer " + TraceShiftIndexByCount.ToString() + " lines, Index(" + FmainPassed.DelSep.ItemAttrCounter.ToString() + "), ShiftByteCount(" + TraceByteCount.ToString() + "), ByteCount(" + TraceByteCountTotal.ToString() + ")";
                       st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, FmainPassed.Buf.CharMaxIndex, FmainPassed.DelSep.ItemAttrMaxIndex, StateIs.NotSet, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg9);
                        LocalMessage.Msg = LocalMessage.Msg9;
                    }
                    //
                    while (FmainPassed.DelSep.ItemAttrCounter > TraceShiftIndexByCount)
                    {
                        ShiftCheckPointer = ItemDataShift(ref FmainPassed, TraceShiftIndexByCount);
                        // ItemAttrCounter -= TraceShiftIndexByCount;
                        TraceByteCountTotal += (int)ShiftCheckPointer;
                        TraceByteCount -= (int)ShiftCheckPointer;
                    }
                    //
                    if (TraceBugOnNow && TraceDisplayCountTotal > TraceBugThreshold)
                    {
                        LocalMessage.Msg9 = "Debug Point Reached, Shift finished, now Index(" + FmainPassed.DelSep.ItemAttrCounter.ToString() + "), ShiftByteCount(" + TraceByteCount.ToString() + "), ByteCount(" + TraceByteCountTotal.ToString() + ")";
                       st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, FmainPassed.Buf.CharMaxIndex, FmainPassed.DelSep.ItemAttrMaxIndex, FileState.ColPointerIncrementResult, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg9);
                        //System.Diagnostics.Debug.WriteLine("MessageBlock [" + TraceMessageBlock + "]");
                        //TraceMessageBlock = sEmpty;
                        LocalMessage.Msg = LocalMessage.Msg9;
                    }
                }
            }
            //
            return ShiftCheckPointer;
        }
        // Move Shift Item.ItemData by x Attrs
        /// <summary> 
        /// File Buffer - Item Data Shift Data and Pointer.
        /// </summary> 
        public int ItemDataShift(ref mFileMainDef FmainPassed, int iPassedAttrsToShift)
        {
            FileState.ItemDataShiftResult = StateIs.Started;
            int iItemDataShifted = 0;
            // 
            // ToDo $$$CHECK Use Primitive extension values here (McString)
            iItemDataShifted = PickIndex(FmainPassed.Item.ItemData, ColumnSeparator, iPassedAttrsToShift);
            if (iItemDataShifted >= 0)
            {
                // 
                if (FmainPassed.Item.ItemData.Length > iItemDataShifted)
                {
                    FmainPassed.Item.ItemData = FmainPassed.Item.ItemData.Substring(iItemDataShifted + 1);
                    //
                    FmainPassed.ColIndex.ColCounter -= iPassedAttrsToShift; // Current Attr
                    FmainPassed.ColIndex.ColMaxIndexTemp -= iPassedAttrsToShift;
                    if (FmainPassed.ColIndex.ColCounter < 0)
                    {
                        FmainPassed.ColIndex.ColCounter = 0;
                        // error condition - ColIndex.ColCounter 
                    }
                }
                else { iItemDataShifted = 0; }
                // else { ColIndexPassed.Item.ItemData = sEmpty; }
            }
            else { iItemDataShifted = 0; }
            // else { ColIndexPassed.Item.ItemData = sEmpty; }
            //
            // ToDo $$$CHECK Use Primitive extension values here (McString)
            // ItemDataShiftResult = ((FilePickDb)FilePickDbObject).PickItemDataCounterGet(ColIndexPassed);
            // ColIndexPassed.ColMaxIndex = 0; // Total Attrs in Item
            // ColIndexPassed.ColMaxIndexTemp = 0; // Total Attrs in Item
            // ItemDataAtrributeClear(ColIndexPassed);
            // Working value
            // ColIndexPassed.ColCounter = 0; // ItemData Items in Item / Row / Item
            // Character Pointers
            // ItemDataCharClear(ColIndexPassed);
            // 
            // ToDo ItemDataShift NOTE More work needed on delimiters,
            // ToDo ItemDataShift add delimiters as required.
            // ToDo ItemDataShift NOTE Count of columns not rows 
            // ToDo ItemDataShift check for csv handling before changing
            // ToDo ItemDataShift NOTE This does not handle quoteded characters!!!
            //
            return iItemDataShifted;
        }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        #endregion
    }
}
