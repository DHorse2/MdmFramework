#region Dependencies
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
using Mdm;
using Mdm.Oss;
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
using Mdm.Oss.File.Type.Delim;
//using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion

namespace Mdm.Oss.File
{
    public partial class mFileDef : Mobject, ImFileType, IDisposable
    {
        #region Class Methods
        #region $include Text based Data Record Parsing
        // <Section Id = "Ascii File Handling
        // <Section Id = "Read and return a data record array
        public DelimDataDef Mrecord;
        /// <summary> 
        /// Get the Ascii File Directory List.
        /// </summary> 
        public Dictionary<String, Object> AsciiFileReadDirList(ref mFileMainDef FmainPassed)
        {
            FileState.AsciiFileReadRecordResult = StateIs.Started;
            FileState.AsciiFileReadRecordResult = AsciiFileReadAll(ref FmainPassed);
            Dictionary<String, Object> MrecDict = new Dictionary<string, object>();

            return MrecDict;
        }
        /// <summary> 
        /// Read an Ascii File Data Record.
        /// </summary> 
        public StateIs AsciiFileReadRecord(ref mFileMainDef FmainPassed)
        {
            FileState.AsciiFileReadRecordResult = StateIs.Started;
            // FmainPassed.Fs.FileId.FileNameLine = FmainPassed.Fs.FileId.PropSystemPath;
            // ToDo needs to use return values.
            FileState.AsciiFileReadRecordResult = AsciiFileReadAll(ref FmainPassed);
            Mrecord = new DelimDataDef(ref FmainPassed);

            return FileState.AsciiFileReadRecordResult;
            // return(mFileState.AsciiFileReadRecordResult = StateIs.Finished);
        }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        #region Ascii Data File
        #region Ascii Read
        // <Section Id = "Check and ReadAll and ReadLine AsciiFile
        /// <summary> 
        /// Ascii Read All Content for Item.
        /// </summary> 
        public StateIs AsciiFileReadAll(ref mFileMainDef FmainPassed)
        {
            // <Area Id = "bRead the IoAll of text
            FileState.AsciiFileReadAllResult = StateIs.Started;
            FmainPassed.Fs.FileId.FileName = FmainPassed.Fs.FileId.FileName;
            if (FmainPassed.Fs.FileId.FileNameLine.Length == 0)
            {
                FmainPassed.Fs.FileId.FileNameLine =
                    FileNameLineBuild(ref FmainPassed);
            }
            // <Area Id = "Check File Stream; 
            FileState.AsciiFileReadAllResult = AsciiFileFileStreamReaderCheck(ref FmainPassed);
            // <Area Id = "bRead All Lines if Stream OK;
            if (FileState.AsciiFileReadAllResult == StateIs.Successful)
            {
                FileState.AsciiFileReadAllResult = StateIs.InProgress;
                try
                {
                    //
                    FmainPassed.FileStatus.bpItemIsAtEnd = true;
                    FmainPassed.Fs.FileIo.spIoReadBuffer =
                    System.IO.File.ReadAllText(FmainPassed.Fs.FileId.FileNameLine);
                    //
                    FmainPassed.FileStatus.bpDoesExist = true;
                    FmainPassed.Buf.BytesRead = FmainPassed.Fs.FileIo.IoReadBuffer.Length;
                    // FmainPassed.Buf.BytesReadTotal = Fmain.Buf.BytesRead;
                    FmainPassed.Fs.FileIo.spIoAll += FmainPassed.Fs.FileIo.spIoReadBuffer;
                    FmainPassed.Buf.BytesReadTotal = FmainPassed.Fs.FileIo.spIoAll.Length;
                    // ToDo X Use of spIoAll is in question vs IoReadBuffer
                    // ToDo X Should IoReadBuffer be cleared or IoAll not loaded?
                    // ToDo X Most likely is that IoAll should not be loaded or flag s/b used...
                    // iItemDataCharEobIndex = Item.ItemData.Length; // End of Character Buffer
                    // if (Fmain.Buf.ItemIsAtEnd == true) {
                    // iItemDataCharEofIndex = iItemDataCharEobIndex;
                    // }
                    FileState.AsciiFileReadAllResult = StateIs.Successful;
                    FmainPassed.FileStatus.Status = StateIs.Successful;
                }
                catch (Exception ExceptionGeneral)
                {
                    FileState.AsciiFileReadAllResult = StateIs.Failed;
                    FmainPassed.FileStatus.bpDoesExist = false;
                    FmainPassed.Fs.FileIo.IoReadBuffer = sEmpty;
                    FmainPassed.Buf.BytesRead = 0;
                    // FmainPassed.Buf.BytesConverted = 0;
                    // FmainPassed.Buf.BytesConvertedTotal = 0;
                    // iItemDataCharEobIndex = Item.ItemData.Length; // End of Character Buffer
                    // if (FmainPassed.Buf.ItemIsAtEnd == true) {
                    // iItemDataCharEofIndex = iItemDataCharEobIndex;
                    // }
                    if (FmainPassed.Fs.FileIo.DbFileStreamReaderObject == null)
                    {
                        FmainPassed.FileStatus.Status = StateIs.Failed;
                    }
                    else
                    {
                        FmainPassed.FileStatus.Status = StateIs.Failed;
                    }
                    //
                    ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.AsciiFileReadAllResult);
                    ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                }
                finally
                {
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickState.PickSystemCallStringResult, RunErrorDidOccur = false, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "Executing finally block.");
                }
            }
            else
            {
                FmainPassed.Fs.FileIo.IoReadBuffer = sEmpty;
                FmainPassed.Buf.BytesRead = 0;
                // FmainPassed.Buf.BytesConverted = 0;
                // FmainPassed.Buf.BytesConvertedTotal = 0;
            }
            return FileState.AsciiFileReadAllResult;
        }
        // <Section Id = "x
        /// <summary> 
        /// Ascii Read Line for Item.
        /// </summary> 
        public StateIs AsciiFileReadLine(ref mFileMainDef FmainPassed)
        {
            // <Area Id = "bRead All Lines if Stream OK;
            FileState.AsciiFileReadLineResult = StateIs.Started;
            FmainPassed.Fs.FileId.FileName = FmainPassed.Fs.FileId.FileName;
            if (FmainPassed.Fs.FileId.spFileNameFull.Length == 0)
            {
                if (FmainPassed.Fs.FileId.FileNameLine.Length > 0)
                {
                    FmainPassed.Fs.FileId.spFileNameFull = FmainPassed.Fs.FileId.FileNameLine;
                }
            }
            // <Area Id = "Check File Stream; 
            FileState.AsciiFileReadLineResult = AsciiFileFileStreamReaderCheck(ref Fmain);
            // <Area Id = "bRead All Lines if Stream OK;
            if (FileState.AsciiFileReadLineResult == StateIs.Successful)
            {
                FileState.AsciiFileReadLineResult = StateIs.InProgress;
                try
                {
                    Fmain.Fs.FileIo.IoReadBuffer = Fmain.Fs.FileIo.DbFileStreamReaderObject.ReadLine();
                    Fmain.Fs.FileIo.IoLine += Fmain.Fs.FileIo.IoReadBuffer;
                    // <Area Id = "Continue to read until you reach end of file
                    Fmain.FileStatus.Status = StateIs.Successful;
                    FileState.AsciiFileReadLineResult = StateIs.Successful;
                }
                catch (Exception ExceptionGeneral)
                {
                    FileState.AsciiFileReadLineResult = StateIs.Failed;
                    LocalMessage.ErrorMsg = sEmpty;
                    ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.AsciiFileReadLineResult);
                    ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                    if (Fmain.Fs.FileIo.DbFileStreamReaderObject == null)
                    {
                        Fmain.FileStatus.Status = StateIs.OsError;
                    }
                    else
                    {
                        Fmain.FileStatus.Status = StateIs.Failed;
                        if (Fmain.Fs.FileIo.IoReadBuffer.Length == 0)
                        {
                            Fmain.FileStatus.Status = StateIs.Finished;
                        }
                    }
                    Fmain.Fs.FileIo.IoReadBuffer = sEmpty;
                    // spIoLine = sEmpty;
                }
                finally
                {
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickState.PickSystemCallStringResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, "Executing finally block.");
                }
            }
            return FileState.AsciiFileReadLineResult;
        }
        // <Section Id = "public String Check and bRead Database
        /// <summary> 
        /// Check the Ascii File Stream Reader.
        /// </summary> 
        public StateIs AsciiFileFileStreamReaderCheck(ref mFileMainDef FmainPassed)
        {
            FileState.AsciiFileFileStreamReaderCheckResult = StateIs.Started;
            FmainPassed.FileStatus.bpIsCreating = true;
            FmainPassed.Fs.FileId.FileName = FmainPassed.Fs.FileId.FileName;
            if (FmainPassed.Fs.FileId.spFileNameLine.Length == 0)
            {
                if (FmainPassed.Fs.FileId.spFileNameFull.Length > 0)
                {
                    FmainPassed.Fs.FileId.spFileNameLine = FmainPassed.Fs.FileId.spFileNameFull;
                }
            }
            // spIoBlock = sEmpty;
            try
            {
                // 
                if (FmainPassed.Fs.FileIo.DbFileStreamReaderObject == null)
                {
                    if (FmainPassed.Fs.FileId.spFileNameLine != null)
                    {
                        //Pass the file path and file name to the StreamReader constructor
                        // ToDo directory not found exception
                        FmainPassed.Fs.FileIo.DbFileStreamReaderObject = new StreamReader(FmainPassed.Fs.FileId.spFileNameLine);
                    }
                }
            }
            catch (Exception e)
            {
                // Error handling code
                LocalMessage.Msg6 = e.ToString();
               st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage,
                    TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(),
                    FileDoResult, RunErrorDidOccur = true,
                    iNoErrorLevel, iNoErrorSource,
                    bDoNotDisplay, MessageNoUserEntry,
                    LocalMessage.Msg6);
            }
            if (FmainPassed.Fs.FileIo.DbFileStreamReaderObject == null)
            {
                FileState.AsciiFileFileStreamReaderCheckResult = StateIs.Failed;
                FmainPassed.FileStatus.bpHadError = true;
            }
            else
            {
                FileState.AsciiFileFileStreamReaderCheckResult = StateIs.Successful;
                FmainPassed.FileStatus.bpIsValid = true;
                FmainPassed.FileStatus.bpIsCreated = true;
                // FmainPassed.FileStatus.bpIsInitialized = true;
                // FmainPassed.FileStatus.bpIsOpen = true;
                // FmainPassed.FileStatus.bpIsOpen= true;
                // FmainPassed.FileStatus.bpDoesExist = true;

            }
            FmainPassed.FileStatus.Status = FileState.AsciiFileFileStreamReaderCheckResult;
            FmainPassed.FileStatus.bpIsCreating = false;
            return FileState.AsciiFileFileStreamReaderCheckResult;
        }
        // <Section Id = "public long Check and bRead Binary (AsciiFileReadBlockSeek)
        /// <summary> 
        /// Read a Block of Data from the Ascii File.
        /// </summary> 
        public String AsciiFileReadBlock(ref mFileMainDef FmainPassed)
        {
            // <Area Id = "bRead the Win32 Seek Block from Handle
            FileState.AsciiFileReadBlockResult = StateIs.Started;

            // <Area Id = "Check File Stream; 
            FileState.AsciiFileReadBlockResult = AsciiFileFileStreamReaderCheck(ref FmainPassed);
            // <Area Id = "bRead All Lines if Stream OK;
            if (FileState.AsciiFileReadBlockResult == StateIs.Successful)
            {
                FileState.AsciiFileReadBlockResult = StateIs.InProgress;
                try
                {
                    // <Area Id = " do a standard read all for now (not Win32 read)
                    // spIoBlock = 
                    FileState.AsciiFileReadBlockResult = AsciiFileReadAll(ref FmainPassed);
                    FileState.AsciiFileReadBlockResult = StateIs.Successful;
                }
                catch (Exception ExceptionGeneral)
                {
                    LocalMessage.ErrorMsg = sEmpty;
                    ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.AsciiFileReadBlockResult);
                    FileState.AsciiFileReadBlockResult = StateIs.Failed;
                    //
                    ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                    if (Fmain.Fs.FileIo.DbFileStreamReaderObject == null)
                    {
                        Fmain.FileStatus.Status = StateIs.Failed;
                    }
                    else
                    {
                        Fmain.FileStatus.Status = StateIs.Failed;
                    }
                }
                finally
                {
                    LocalMessage.ErrorMsg = "Executing finally block.";
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickState.PickSystemCallStringResult, bNoError, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                }
            }
            return Fmain.Fs.FileIo.spIoBlock;
        }
        // <Section Id = "public long Check and bRead Binary (AsciiFileReadBlockSeek)
        /// <summary> 
        /// Read a Block of Data by Seeking in the Ascii File.
        /// </summary> 
        public String AsciiFileReadBlockSeek(ref mFileMainDef FmainPassed)
        {
            // <Area Id = "bRead the Win32 Seek Block from Handle
            FileState.AsciiFileReadBlockSeekResult = StateIs.Started;

            // <Area Id = "Check File Stream; 
            FileState.AsciiFileReadBlockSeekResult = AsciiFileFileStreamReaderCheck(ref FmainPassed);
            // <Area Id = "bRead All Lines if Stream OK;
            if (FileState.AsciiFileReadBlockSeekResult == StateIs.Successful)
            {
                FileState.AsciiFileReadBlockSeekResult = StateIs.InProgress;
                try
                {
                    // <Area Id = " do a standard read all for now (not Win32 read)
                    // spIoBlock = 
                    FileState.AsciiFileReadBlockSeekResult = AsciiFileReadAll(ref FmainPassed);
                    FileState.AsciiFileReadBlockSeekResult = StateIs.Successful;
                }
                catch (Exception ExceptionGeneral)
                {
                    LocalMessage.ErrorMsg = sEmpty;
                    ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.AsciiFileReadBlockSeekResult);
                    //
                    ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                    FileState.AsciiFileReadBlockSeekResult = StateIs.Failed;
                    if (Fmain.Fs.FileIo.DbFileStreamReaderObject == null)
                    {
                        Fmain.FileStatus.Status = StateIs.Failed;
                    }
                    else
                    {
                        Fmain.FileStatus.Status = StateIs.Failed;
                    }
                }
                finally
                {
                    LocalMessage.ErrorMsg = "Executing finally block.";
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickState.PickSystemCallStringResult, bNoError, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                }
            }
            return Fmain.Fs.FileIo.spIoBlock;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Ascii Write
        // <Section Id = "AsciiFileWritePassedName">
        /// <summary> 
        /// Write the Ascii File Data.
        /// </summary> 
        public StateIs AsciiFileWrite()
        {
            FileState.AsciiFileWriteResult = StateIs.Started;
            FileState.AsciiFileWriteResult = AsciiFileWrite(ref Fmain);
            return FileState.AsciiFileWriteResult;
        }
        // <Section Id = "AsciiFileWritePassedName">
        /// <summary> 
        /// Write the Ascii File Data for the Passed File.
        /// </summary> 
        public StateIs AsciiFileWrite(ref mFileMainDef FmainPassed)
        {
            FileState.AsciiFileWritePassedNameResult = StateIs.Started;
            if (FmainPassed.Fs.FileId.spFileNameLine.Length == 0)
            {
                FmainPassed.Fs.FileId.spFileNameLine = FileNameLineBuild(ref FmainPassed);
            }
            // ToDo WRITE ASCII DATA HERE!!!
            FileState.AsciiFileWritePassedNameResult = StateIs.UnknownFailure;
            return FileState.AsciiFileWritePassedNameResult;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Ascii Close
        // <Section Id = "x
        /// <summary> 
        /// Close the Passed Ascii File
        /// </summary> 
        public StateIs AsciiFileClose(ref mFileMainDef FmainPassed)
        {
            FileState.AsciiFileCloseResult = StateIs.Started;
            // Console.ReadLine();
            // <Area Id = "close the file streams
            if (FmainPassed.Fs.FileIo.DbFileStreamReaderObject != null)
            {
                FmainPassed.Fs.FileIo.DbFileStreamReaderObject.Close();
            }
            if (FmainPassed.Fs.FileIo.DbFileStreamObject != null)
            {
                FmainPassed.Fs.FileIo.DbFileStreamObject.Close();
            }
            //close the file
            if (FmainPassed.Fs.FileIo.DbFileObject != null)
            {
                FileState.AsciiFileCloseResult = AsciiFileClear(ref FmainPassed);
                // <Area Id = "do destructor;
            }
            return FileState.AsciiFileCloseResult;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Ascii Delete
        // <Section Id = "AsciiFileDeletePassedName">
        /// <summary> 
        /// Delete the Ascii File Data or Item.
        /// </summary> 
        public StateIs AsciiFileDelete()
        {
            FileState.AsciiFileDeleteResult = StateIs.Started;
            FileState.AsciiFileDeleteResult = AsciiFileDelete(ref Fmain);
            return FileState.AsciiFileDeleteResult;
        }
        // <Section Id = "AsciiFileDeletePassedName">
        /// <summary> 
        /// Delete the Ascii File Data or Item for the Passed File.
        /// </summary> 
        public StateIs AsciiFileDelete(ref mFileMainDef FmainPassed)
        {
            FileState.AsciiFileDeletePassedNameResult = StateIs.Started;
            if (FmainPassed.Fs.FileId.spFileNameLine.Length == 0)
            {
                FmainPassed.Fs.FileId.spFileNameLine = FileNameLineBuild(ref FmainPassed);
            }
            // Delete the data here
            FileState.AsciiFileDeletePassedNameResult = StateIs.UnknownFailure;
            return FileState.AsciiFileDeletePassedNameResult;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Ascii Clear
        // <Section Id = "AsciiFileClear">
        /// <summary> 
        /// Clear the Ascii File Data.
        /// </summary> 
        public StateIs AsciiFileClear(ref mFileMainDef FmainPassed)
        {
            FileState.AsciiFileClearResult = StateIs.Started;
            //
            /*/ 
            TableNameLine = TableNameLineBuild(ref Fs.DatabaseName, ref Fs.spFileOwnerName, ref Fs.spFileName);
            if (spIoAll == null) {
                FmainPassed.Fs.FileIo.IoReadBuffer = sEmpty;
                spIoBlock = sEmpty;
                spIoLine = sEmpty;
                spIoAll = sEmpty;
            }
            /*/
            FmainPassed.Fs.FileIo.DbFileObject = null;
            FmainPassed.Fs.FileIo.DbFileStreamObject = null;
            FmainPassed.Fs.FileIo.DbFileStreamReaderObject = null;
            FmainPassed.DbIo.SqlDbConn = null;
            FmainPassed.DbIo.SqlDbCommand = null;
            FmainPassed.DbIo.CommandCurrent = null;
            /*/
            bDbDatabaseDoesExist = false;
            bDbDatabaseIsInvalid = false;
            bDbDatabaseIsCreating = false;
            bDbDatabaseIsCreated = false;

            // <Area Id = "DatabaseErrorObject">
            DbFileCmdOsException = null;
            ExceptSql = null;
            /*/
            FmainPassed.Fs.DataClear();
            //
            // FileData
            FmainPassed.Fs.FileIo.IoReadBuffer = sEmpty;
            FmainPassed.Fs.FileIo.spIoBlock = sEmpty;
            FmainPassed.Fs.FileIo.spIoLine = sEmpty;
            FmainPassed.Fs.FileIo.spIoAll = sEmpty;

            return FileState.AsciiFileClearResult;
        }
        // <Section Id = "AsciiFileCreatePassedName">
        #endregion
        #region Ascii Create
        /// <summary> 
        /// Create Ascii File.
        /// </summary> 
        public StateIs AsciiFileCreate()
        {
            FileState.AsciiFileCreateResult = StateIs.Started;
            FileState.AsciiFileCreateResult = AsciiFileCreate(ref Fmain);
            return FileState.AsciiFileCreateResult;
        }
        // <Section Id = "AsciiFileCreatePassedName">
        /// <summary> 
        /// Create the Passed Ascii File.
        /// </summary> 
        public StateIs AsciiFileCreate(ref mFileMainDef FmainPassed)
        {
            FileState.AsciiFileCreatePassedNameResult = StateIs.Started;
            if (FmainPassed.Fs.FileId.spFileNameLine.Length == 0)
            {
                FmainPassed.Fs.FileId.spFileNameLine = FileNameLineBuild(ref FmainPassed);
            }
            // Create the data here
            FileState.AsciiFileCreatePassedNameResult = StateIs.Failed;
            return FileState.AsciiFileCreatePassedNameResult;
        }
        // <Section Id = "AsciiFileReset">
        #endregion
        #region Ascii Reset
        /// <summary> 
        /// Reset the Ascii File Data and Fields.
        /// File
        ///  Ascii
        ///  Text
        ///  Binary
        /// Database File
        ///  Sql
        ///      MS Sql
        ///      MY Sql
        ///  Db2
        ///  Pick
        /// xxxxxxxxxxxxxxxxxxxxxxx
        /// File XXXX is virtual and will be overriden in the
        /// subclasses when implemented
        /// Therefore SqlOpen, AsciiFileOpen and TextFileOpen become FileOpen
        /// in the SqlFile, AsciiFile, TextFile classes
        ///
        /// </summary> 
        public StateIs AsciiFileReset(ref mFileMainDef FmainPassed)
        {
            FileState.AsciiFileResetResult = StateIs.Started;
            // if (Faux.FileStatus.bpIsInitialized) {
            // THIS IS A DISPOSE FUNCTION
            Faux.FileStatus.bpIsInitialized = false;
            // }
            return FileState.AsciiFileResetResult;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        #endregion
    }
}
