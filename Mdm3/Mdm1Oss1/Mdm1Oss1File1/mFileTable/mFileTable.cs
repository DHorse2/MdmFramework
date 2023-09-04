#region Dependencies
#region System
#region README
// 1) Note: If you update this list make sure to
// update the Initialize function.
// This would be used as an app template.
// 2) Description:
// This document represents the possible
// dependencies you will encounter using
// this framework.
// 3) Many "using X" are commented out by default.
// 4) I based this on the ability to compile
// this lower level object. 
// In this sense it should define the
// minimum depencies of Mdm Core.
#endregion
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
#region System Globalization
//using System.Globalization;
#endregion
#region System Other
// using System.Collections.Specialized;
#endregion
#region System Reflection, Runtime and Diagnostics
//using System.Diagnostics;
//using System.Reflection;
//using System.Runtime.InteropServices;
//using System.Runtime.Remoting.Messaging;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Timers;
#endregion
#region System Runtime and Timers
//using System.Runtime;
//using System.Runtime.ExceptionServices;
//using System.Runtime.InteropServices;
//using System.Runtime.Remoting.Messaging;
//using System.Timers;
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
#region  System Threading
using System.Threading;
//using System.Threading.Tasks;
#endregion
#region System Windows Forms
//using System.Drawing;
using System.Windows;
using System.Windows.Forms;
//using System.Windows.Controls;
#endregion
#region System ComponentModel
using System.ComponentModel;
#endregion
#region System Security
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
#endregion
#region System Serialization (Runtime and Xml)
//using System.Runtime.Serialization;
////using System.Runtime.Serialization.DataContractSerializer;
//using System.Runtime.Serialization.Formatters.Binary;
////using System.Runtime.Serialization.XmlObjectSerializer;
//using System.Xml.Serialization;
#endregion
#region System XML
//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Schema;
#endregion
#endregion
#region  Mdm Core
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
using Mdm.Oss.File.Db;
using Mdm.Oss.File.Db.Data;
using Mdm.Oss.File.Db.Table;
using Mdm.Oss.File.Db.Thread;
//using Mdm.Oss.File.Properties;
#endregion
#region  Mdm Apps Clipboard
//using Mdm.Oss.ClipUtil;
//using Mdm.Oss.ClipUtil.Windows;
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
//using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Pick;
using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#region Mdm Srt (Search, replace and transform)
//using Mdm.Srt;
//using Mdm.Srt.Core;
//using Mdm.Srt.Transform;
//using Mdm.Srt.Script;
#endregion
#region  Mdm MVC Mobject
using Mdm.Oss.Mobj;
#endregion
#endregion

namespace Mdm.Oss.File
{
    public partial class mFileDef
    {
        // Table ToDo *** HERE ***
        #region Table
        #region Table Create
        /// <summary> 
        /// 
        /// </summary> 
        public StateIs TableCreateDo(ref mFileMainDef FmainPassed)
        {
            FileState.TableCreateDoResult = StateIs.Started;
            FmainPassed.FileStatus.bpIsCreating = true;
            // this code is for database create:
            //if (DbMasterSyn.MstrDbDatabaseCreateCmd == null) {
            //    mFileState.TableCreateDoResult = DatabaseCreateCmdBuild(); // XXXXXXXXXXXXXXXXXXXXX
            //}
            // Create Table
            FileState.TableCreateDoResult = StateIs.InProgress;
            // Connect to database
            try
            {
                // <Area Id = "General System Errors
                Sys.sMformStatusMessage = sEmpty;
                // Not IsConnected
                if (!FmainPassed.FileStatus.bpIsConnected) { FmainPassed.FileSqlConn.ConnOpen(ref FmainPassed); }
            }
            catch (SqlException ExceptionSql)
            {
                LocalMessage.ErrorMsg = sEmpty;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.TableCreateDoResult);
                //
                FileState.TableCreateDoResult = StateIs.Failed;
                ExceptTableCreationImpl(ref FmainPassed, ref ExceptionSql);
                // <Area Id = "Exit Here
            }
            catch (Exception ExceptionGeneral)
            {
                LocalMessage.ErrorMsg = sEmpty;
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.ConnCloseResult);
                FileState.TableCreateDoResult = StateIs.Failed;
                ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                // <Area Id = "*** App.Job.Exit(); *** TODO
            }
            finally { }
            //
            // Create table
            FileState.TableCreateDoResult = StateIs.InProgress;
            try
            {
                FmainPassed.FileSqlConn.DbMasterSyn.MstrDbDatabaseCreateCmd = "CREATE-FILE " + FmainPassed.Fs.spTableName;
                FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.FileSqlConn.DbMasterSyn.MstrDbDatabaseCreateCmd, FmainPassed.DbIo.SqlDbConn);
                // FmainPassed.DbIo.SqlDbCommand = new SqlCommand(MstrDbDatabaseCreateCmd, FmainPassed.DbIo.SqlDbConn);
                // ??? reader type / mode ???? (ie scalar)
                //=================
                FileState.TableCreateDoResult = FmainPassed.FileSqlConn.SqlCommandDo(ref FmainPassed, FmainPassed.FileSqlConn.DbMasterSyn.MstrDbDatabaseCreateCmd);
                //=================
                if (StateIsSuccessfulAll(FileState.TableCreateDoResult))
                {
                    FmainPassed.FileStatus.bpIsCreated = true;
                }
                // sMformStatusMessage.Close();
                Sys.sMformStatusMessage = sEmpty;
                //=================
                LocalMessage.ErrorMsg = StdProcess.Title + @" Database Table Creation Status" + "\n" + "Database " + FmainPassed.FileSqlConn.DbMasterSyn.spMstrDbPhraseDatabase + @" successfully created!";
                ((iTrace)ConsoleSender).TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickState.PickSystemCallStringResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                //== <Area Id = "Catch Try
                FmainPassed.FileStatus.bpIsCreating = false;
            }
            catch (SqlException ExceptionSql)
            {
                LocalMessage.ErrorMsg = sEmpty;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.TableCreateDoResult);
                FileState.TableCreateDoResult = StateIs.Failed;
                FileState.ConnCloseResult = StateIs.DatabaseError;
                ExceptTableCreationImpl(ref FmainPassed, ref ExceptionSql);
                LocalMessage.ErrorMsg = StdProcess.Title + " SQL Exception Error!";
                LocalMessage.ErrorMsg = ExceptionSql.Message;
                ExceptTableCreationImpl(ref FmainPassed, ref ExceptionSql);
                FmainPassed.FileStatus.bpIsCreated = false;
            }
            catch (Exception ExceptionGeneral)
            {
                LocalMessage.ErrorMsg = sEmpty;
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.ConnCloseResult);
                FileState.TableCreateDoResult = StateIs.Failed;
                //
                ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                FmainPassed.FileStatus.bpIsCreated = false;
            }
            // ToDo z$RelVs? Database File Creation
            if (FmainPassed.FileStatus.bpIsCreated)
            {
                FileState.TableCreateDoResult = StateIs.Successful;
            }
            if (FmainPassed.FileStatus.bpIsCreating)
            {
                FileState.TableCreateDoResult = StateIs.AbnormalEnd;
                FmainPassed.FileStatus.Status = StateIs.AbnormalEnd;
            }
            if (!FmainPassed.FileStatus.bpIsValid)
            {
                FileState.TableCreateDoResult = StateIs.AbnormalEnd;
                FmainPassed.FileStatus.Status = StateIs.AbnormalEnd;
            }
            return FileState.TableCreateDoResult;
        }
        #endregion
        #region Table Open
        // <Section Id = "TableOpen">
        /// <summary> 
        /// Open the current table.
        /// </summary> 
        public StateIs TableOpen()
        {
            FileState.TableOpenResult = StateIs.Started;
            FileState.TableOpenResult = TableOpen(ref Fmain);
            return FileState.TableOpenResult;
        }
        // <Section Id = "x
        /// <summary> 
        /// Open the passed table.
        /// </summary> 
        public StateIs TableOpen(ref mFileMainDef FmainPassed)
        {
            FileState.TableOpenResult = StateIs.Started;
            #region Initialize File Information
            int iOptionsResult = 0;
            //
            FileState.TableOpenResult = StateIs.InProgress;
            // mFileState.TableOpenResult = StateIs.Undefined;
            //
            FmainPassed.Buf.ByteCountClear();
            // ToDo Why is this here??? Move it??? Analysis required...
            PickRow.DataClear();
            PickRow.RowDataClear(PickRow.sdIndex);
            //
            FmainPassed.FileStatus.bpIsInitialized = true;
            #endregion
            #region Check DoesExist
            switch (FmainPassed.Fs.FileIo.IoMode)
            {
                case (FileIo_ModeIs.Sql):
                    // FmainPassed.DbIo.SqlDbConn.Open();
                    // FmainPassed.FileStatus.bpDoesExistResult = (int)TableCheckDoesExist(ref FmainPassed);
                    //  check Connection Name
                    if (FmainPassed.Fs.TableNameLine.Length == 0 || !FmainPassed.FileStatus.bpNameIsValid)
                    {
                        FmainPassed.Fs.TableNameLine = FmainPassed.FileSqlConn.TableNameLineBuild(ref FmainPassed);
                    }
                    //  check Connection
                    FileState.TableOpenResult = FmainPassed.FileSqlConn.ConnCheck(ref FmainPassed);
                    if (StateIsSuccessfulAll(FileState.TableOpenResult))
                    {
                        // mFileState.TableOpenResult = StateIs.InProgress;
                        //
                        // ToDo OPEN TABLE HERE
                        //
                        FmainPassed.FileStatus.ipDoesExistResult = FileState.TableOpenResult;
                        if (StateIsSuccessfulAll(FileState.TableOpenResult))
                        {
                            FileState.TableOpenResult = StateIs.DoesExist;
                            FmainPassed.FileStatus.bpDoesExist = true;
                        }
                        else {
                            FileState.TableOpenResult = StateIs.DoesNotExist;
                            FmainPassed.FileStatus.bpDoesExist = false;
                        }
                    }
                    else { return FileState.TableOpenResult; }
                    break;
                case (FileIo_ModeIs.Block):
                case (FileIo_ModeIs.Line):
                case (FileIo_ModeIs.All):
                    FmainPassed.Fs.TableNameLine = FmainPassed.Fs.spTableName;
                    FmainPassed.FileStatus.bpDoesExist = System.IO.File.Exists(FmainPassed.Fs.TableNameLine);
                    if (FmainPassed.FileStatus.bpDoesExist)
                    {
                        FileState.TableOpenResult = StateIs.DoesExist;
                    }
                    else {
                        FileState.TableOpenResult = StateIs.DoesNotExist;
                    }
                    break;
                default:
                    FileState.TableOpenResult = StateIs.ProgramInvalid;
                    FmainPassed.FileStatus.Status = StateIs.ProgramInvalid;
                    LocalMessage.ErrorMsg = "File Read IoType (" + FmainPassed.Fs.FileIo.IoMode.ToString() + ") is not set";
                    throw new NotSupportedException(LocalMessage.ErrorMsg);
                    // return mFileState.TableOpenResult;
            }
            #endregion
            // File exists or not
            // Process results based on options:
            // Create Missing File
            // Delete Existing File
            // Append to file
            // Empty file
            // File should not exist
            // File should exist
            try
            {
                // File does exist
                if ((FmainPassed.FileStatus.bpDoesExist))
                {
                    FileState.TableOpenResult = StateIs.DoesExist;
                    #region Option: N: File must not alread exist
                    // <Area Id = "Option: File must not alread exist options here.
                    iOptionsResult = FmainPassed.Fs.FileOptionString.IndexOf("N");
                    if (iOptionsResult > 0)
                    {
                        // <Area Id = "Option: error file already exists.
                        FileState.TableOpenResult = StateIs.ShouldNotExist;
                        return FileState.TableOpenResult;
                    }
                    #endregion
                    #region Option: D: Delete the file if it exists
                    // <Area Id = "Option: Delete the file if it exists.
                    iOptionsResult = FmainPassed.Fs.FileOptionString.IndexOf("D");
                    if (iOptionsResult > 0)
                    {
                        #region Option: Delete the file
                        switch (FmainPassed.Fs.FileIo.IoMode)
                        {
                            case (FileIo_ModeIs.Sql):
                                // ToDo Option: Delete the file if it exists
                                // mFileState.TableOpenResult = SqlFileDelete(FmainPassed.spTableName);
                                if (FmainPassed.FileStatus.bpDoesExist)
                                {
                                    FileState.TableOpenResult = StateIs.Successful;
                                }
                                else {
                                    // ToDo $ERROR Error Option: Delete the file if it exists
                                    FileState.TableOpenResult = StateIs.ShouldNotExist;
                                }
                                return FileState.TableOpenResult;
                            case (FileIo_ModeIs.Block):
                            case (FileIo_ModeIs.Line):
                            case (FileIo_ModeIs.All):
                                // ToDo Option: Delete the file if it exists
                                System.IO.File.Delete(FmainPassed.Fs.spTableName);
                                // ToDo Option: Create the file if it exists
                                // <Area Id = "Option: Create the file depending on options here.
                                FmainPassed.Fs.FileIo.DbFileStreamObject = System.IO.File.Create(FmainPassed.Fs.spTableName);
                                if (FmainPassed.Fs.FileIo.DbFileStreamObject != null)
                                {
                                    FileState.TableOpenResult = StateIs.Successful;
                                }
                                else {
                                    FileState.TableOpenResult = StateIs.ShouldNotExist;
                                }
                                break;
                            default:
                                FileState.TableOpenResult = StateIs.ProgramInvalid;
                                FmainPassed.FileStatus.Status = StateIs.ProgramInvalid;
                                LocalMessage.ErrorMsg = "File Read IoType (" + FmainPassed.Fs.FileIo.IoMode.ToString() + ") is not set";
                                throw new NotSupportedException(LocalMessage.ErrorMsg);
                                // return mFileState.TableOpenResult;
                        }
                        #endregion
                        #region VERIFY delete
                        // <Area Id = "Open the stream and read it back.
                        switch (FmainPassed.Fs.FileIo.IoMode)
                        {
                            case (FileIo_ModeIs.Sql):
                                // ToDo OPEN Open the stream and read it back
                                if (FmainPassed.FileStatus.bpDoesExist)
                                {
                                    FileState.TableOpenResult = StateIs.Successful;
                                }
                                else {
                                    FileState.TableOpenResult = StateIs.ShouldNotExist;
                                }
                                return FileState.TableOpenResult;
                            case (FileIo_ModeIs.Block):
                            case (FileIo_ModeIs.Line):
                            case (FileIo_ModeIs.All):
                                // ToDo OPEN Open the stream and read it back
                                FmainPassed.Fs.FileIo.DbFileStreamObject = System.IO.File.OpenRead(FmainPassed.Fs.spTableName);
                                // DbFileStreamObject = System.IO.File.TextFileOpen(Fs.spTableName);
                                if (FmainPassed.Fs.FileIo.DbFileStreamObject != null)
                                {
                                    FileState.TableOpenResult = StateIs.Successful;
                                }
                                else {
                                    FileState.TableOpenResult = StateIs.ShouldNotExist;
                                }
                                break;
                            default:
                                FileState.TableOpenResult = StateIs.ProgramInvalid;
                                FmainPassed.FileStatus.Status = StateIs.ProgramInvalid;
                                // ToDo $ERROR Error Open the stream and read it back
                                LocalMessage.Msg = "File Read IoType (" + FmainPassed.Fs.FileIo.IoMode.ToString() + ") is not set";
                                throw new NotSupportedException(LocalMessage.Msg);
                                // return mFileState.TableOpenResult;
                        }
                        #endregion
                    }
                    #endregion
                    #region Option: F: The file must already exist.
                    // <Area Id = "Option: The file must already exist options here.
                    iOptionsResult = FmainPassed.Fs.FileOptionString.IndexOf("F");
                    if (iOptionsResult > 0)
                    {
                        if (FileState.TableOpenResult == StateIs.DoesExist)
                        {
                            return FileState.TableOpenResult;
                        }
                        // mFileState.TableOpenResult = (int) StateIs.DoesNotExist;
                    }
                    #endregion
                }
                else {
                    FileState.TableOpenResult = StateIs.DoesNotExist;
                    // File does not exist
                    #region Option: ?: File Does Not Exist
                    // <Area Id = "Option: File must not exist options here.
                    iOptionsResult = FmainPassed.Fs.FileOptionString.IndexOf("?"); // File Does Not Exist
                    if (iOptionsResult > 0)
                    {
                        // <Area Id = "Option: error file already exists.
                        FileState.TableOpenResult = StateIs.Successful;
                        return FileState.TableOpenResult;
                    }
                    #endregion
                    #region Option: M: Create the missing file. N: Create New Must Not Exist File
                    // <Area Id = "Option: M: Create the missing file depending on options here.
                    iOptionsResult = FmainPassed.Fs.FileOptionString.IndexOf("M"); // Create Missing
                    if (iOptionsResult < 0) { iOptionsResult = FmainPassed.Fs.FileOptionString.IndexOf("N"); } // Create New Must Not Exist
                    if (iOptionsResult > 0)
                    {
                        // <Area Id = "Option: Create the file depending on options here.
                        switch (FmainPassed.Fs.FileIo.IoMode)
                        {
                            case (FileIo_ModeIs.Sql):
                                // ToDo Option: unknown for Read Mode Sql;
                                if (!FmainPassed.FileStatus.bpDoesExist)
                                {
                                    FileState.TableOpenResult = TableCreateToDo(ref FmainPassed);
                                    if (FmainPassed.FileStatus.bpDoesExist)
                                    {
                                        FileState.TableOpenResult = StateIs.DoesExist;
                                        FmainPassed.FileStatus.bpDoesExist = bYES;
                                    }
                                    else {
                                        // ToDo Option: Error Create the file depending on options
                                        FileState.TableOpenResult = StateIs.ShouldExist;
                                        FmainPassed.FileStatus.bpDoesExist = bNO;
                                    }
                                }
                                return FileState.TableOpenResult;
                            case (FileIo_ModeIs.Block):
                            case (FileIo_ModeIs.Line):
                            case (FileIo_ModeIs.All):
                                FileState.TableOpenResult = TextFileCreate(ref FmainPassed);
                                if (FmainPassed.FileStatus.bpDoesExist)
                                {
                                    FileState.TableOpenResult = StateIs.DoesExist;
                                }
                                else {
                                    // ToDo Option: Error Create the file depending on options 
                                    FileState.TableOpenResult = StateIs.ShouldExist;
                                }
                                break;
                            default:
                                FileState.TableOpenResult = StateIs.ProgramInvalid;
                                FmainPassed.FileStatus.Status = StateIs.NotSupported;
                                // ToDo Option: Error Create the file depending on options 
                                LocalMessage.ErrorMsg = "File Read IoType (" + FmainPassed.Fs.FileIo.IoMode.ToString() + ") is not set";
                                throw new NotSupportedException(LocalMessage.ErrorMsg);
                                // return mFileState.TableOpenResult;
                        }
                    }
                    #endregion

                    // ToDo $ERROR Error Option: file missing and no option to create error
                    // <Area Id = "Option: file missing and no option to create error
                    // mFileState.TableOpenResult = (int) StateIs.DoesNotExist;
                }
                #region Catch Errors
            }
            catch (SqlException ExceptionSql)
            {
                LocalMessage.ErrorMsg = sEmpty;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.TableOpenResult);
                FileState.TableOpenResult = StateIs.DatabaseError;
                //
                ExceptTableOpenError(ref FmainPassed, ref ExceptionSql);
                // Exit Here
            }
            catch (NotSupportedException ExceptionNotSupported)
            {
                LocalMessage.ErrorMsg = "Not Supported Exception(#212) occured in File Action";
                ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileDoResult);
                FileState.TableOpenResult = StateIs.ProgramInvalid;
                FmainPassed.FileStatus.bpIsConnecting = false;
                FmainPassed.FileStatus.bpIsConnected = false;
                FmainPassed.ConnStatus.NameIsValid = false;
            }
            catch (Exception ExceptionGeneral)
            {
                LocalMessage.ErrorMsg = sEmpty;
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.ConnCloseResult);
                FileState.TableOpenResult = StateIs.UnknownFailure;
                //
                ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                FmainPassed.FileStatus.bpIsConnecting = false;
                FmainPassed.FileStatus.bpIsConnected = false;
                FmainPassed.ConnStatus.NameIsValid = false;
                #endregion
            }
            finally
            {
                //
            }
            //
            //  ToDo CREATE READER FOR OUTPUT FILE...
            //
            return FileState.TableOpenResult;
        }
        // <Section Id = "x
        // <Section Id = "x
        /// <summary> 
        /// Obsolete Open...
        /// </summary> 
        public StateIs TableOpenOLD(ref mFileMainDef FmainPassed)
        {
            FileState.TableOpenResult = StateIs.Started;
            // TableNameLine
            if (FmainPassed.Fs.TableNameLine.Length == 0 || !FmainPassed.FileStatus.bpNameIsValid)
            {
                FmainPassed.Fs.TableNameLine = FmainPassed.FileSqlConn.TableNameLineBuild(ref FmainPassed);
            }
            // Counts
            FmainPassed.Buf.BytesRead = 0;
            FmainPassed.Buf.BytesReadTotal = 0;
            FmainPassed.Buf.BytesConverted = 0;
            FmainPassed.Buf.BytesConvertedTotal = 0;
            // Open a connection to the database
            if (!FmainPassed.FileStatus.bpIsConnected)
            {
                FileState.TableOpenResult = FmainPassed.FileSqlConn.ConnOpen(ref FmainPassed);
            }
            //  check if the sql exists
            if (!FmainPassed.FileStatus.bpDoesExist || !FmainPassed.FileStatus.bpIsOpen)
            {
                FmainPassed.FileStatus.bpDoKeepConn = true;
                // ToDo $ERROR TableOpen Correct to leave open ???
                FileState.TableOpenResult = FmainPassed.FileSqlConn.TableListCheck(ref FmainPassed, false, false);
                // mFileState.TableOpenResult = TableCheckDoesExist(ref FmainPassed);
                FmainPassed.FileStatus.ipDoesExistResult = FileState.TableOpenResult;
                if (FmainPassed.FileStatus.ipDoesExistResult == StateIs.DoesExist)
                {
                    FmainPassed.FileStatus.bpDoesExist = true;
                }
            }
            else {
                FileState.TableOpenResult = StateIs.DoesExist;
            }
            // FmainPassed.FileStatus.ipStatus = mFileState.TableOpenResult;
            // FmainPassed.FileStatus.bpIsOpen = true;
            // ToDo $$$CHECK work on Sql File Exits (review for support files)
            if (FmainPassed.FileStatus.bpDoesExist
                && FileState.TableOpenResult == StateIs.DoesExist)
            {
                // the file does not exist
                FileState.TableOpenResult = StateIs.DoesExist;
                // SqlFileDoesExist = true;
            }
            else {
                // the file exists and can be changed
                FmainPassed.DbIo.SqlDbConn.Dispose();
                FileState.TableOpenResult = StateIs.DoesNotExist;
                FmainPassed.FileStatus.bpDoesExist = false;
            }

            return FileState.TableOpenResult;
        }
        #endregion
        #region Table Existance Control (CheckDoesExist)
        // <<Section Id = "x
        /// <summary> 
        /// Returns a bool indicating if the Table exists.
        /// </summary> 
        public bool TableDoesExist(ref mFileMainDef FmainPassed)
        {
            FileState.TableCheckDoesExistResult = TableCheckDoesExist(ref FmainPassed);
            if (StateIsSuccessfulAll(FileState.TableCheckDoesExistResult))
            {
                return true;
            }
            else if (FileState.TableCheckDoesExistResult == StateIs.DoesNotExist)
            {
                return false;
            }
            return false;
        }
        // <<Section Id = "x
        /// <summary> 
        /// Check if the Table exists.
        /// </summary> 
        public StateIs TableCheckDoesExist()
        {
            FileState.TableCheckDoesExistResult = StateIs.Started;
            Fmain.FileStatus.ipDoesExistResult = StateIs.Successful;
            Fmain.FileStatus.bpDoesExist = false;
            //
            FileState.TableCheckDoesExistResult = TableCheckDoesExist(ref Fmain);
            return FileState.TableCheckDoesExistResult;
        }
        // <Section Id = "x">
        // ToDo z$RelVs2 TableCheckDoesExist Add Sql File Check Does Exist Code
        /// <summary> 
        /// Check if the passed table exists.
        /// </summary> 
        public StateIs TableCheckDoesExist(ref mFileMainDef FmainPassed)
        {
            FileState.TableCheckDoesExistResult = StateIs.Started;
            Fmain.FileStatus.ipDoesExistResult = StateIs.Successful;
            Fmain.FileStatus.bpDoesExist = false;
            // Add code for table lookup
            FileState.TableCheckDoesExistResult = TableCheckDoesExist(ref Fmain);
            return FileState.TableCheckDoesExistResult;
        }
        // <Section Id = "x">
        // Sql FileExistance Control (CheckDoesExist) (Obsolete)
        /// <summary> 
        /// Test access to the Table.
        /// </summary> 
        public StateIs TableTestAccess(ref mFileMainDef FmainPassed)
        {
            FileState.TableTestAccessResult = StateIs.Started;
            // ToDo Phase this out of use...
            #region Initialize Flags, Status, etc.
            FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Sql;
            FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.Database;
            FmainPassed.RowInfo.UseMethod = FileIo_SqlCommandModeIs.SingleResult;
            FmainPassed.RowInfo.CloseIsNeeded = false;
            // Row
            FmainPassed.RowInfo.HasRows = false;
            FmainPassed.RowInfo.RowContinue = false;
            FmainPassed.RowInfo.RowMax = PickRowDef.sdIndexMax;
            // Colomn
            FmainPassed.RowInfo.HasColumns = false;
            FmainPassed.RowInfo.ColumnContinue = false;
            FmainPassed.RowInfo.ColumnMax = PickRowDef.sdIndexMax;
            // Sql
            FmainPassed.RowInfo.RowIndex = 0;
            // !FmainPassed.FileStatus.bpDoKeepOpen = PassedConnDoClose;
            // FmainPassed.FileStatus.bpDoDispose = FmainPassed.FileStatus.bpDoDispose;
            // SqlFileDoClose = PassedSqlFileDoClose;
            Faux.FileStatus.ipDoesExistResult = StateIs.InProgress;

            FmainPassed.DbIo.SqlDbCommandTimeout = 15;

            FmainPassed.RowInfo.UseMethod = FileIo_SqlCommandModeIs.SingleResult;
            FmainPassed.RowInfo.CloseIsNeeded = false;
            // Row
            FmainPassed.RowInfo.HasRows = false;
            FmainPassed.RowInfo.RowContinue = false;
            FmainPassed.RowInfo.RowIndex = 0;
            FmainPassed.RowInfoDb.RowCount = 0;
            // Clr Native
            FmainPassed.RowInfoDb.RowMax = PickRowDef.sdIndexMax;
            System.Object[] ThisGetValuesArray = new System.Object[(int)ArrayMax.ColumnMax];
            System.Object ThisGetValueObject;
            // Sql
            // ipRowIndex = 0;
            // System.Object[] RowArray;
            // System.Object osoThisGetSqlValue;
            // Colomn
            FmainPassed.RowInfo.HasColumns = false;
            FmainPassed.RowInfoDb.ColumnContinue = false;
            // TODO
            //  mFileState.TableTestAccessResult = FmainPassed.FileSqlConn.SqlColAction(
                //ref FmainPassed,
                //// ref DbIo.SqlDbDataReader, ref DbIo.SqlDbDataWriter, ref RowInfoDbAux, ref ColTransAux, 
                //false, ColTransformDef.SFC_RESET, sTemp0, 0, 0);
            //
            System.Type tThisTempType;

            System.Data.SqlTypes.SqlString tsdssThisTempSqlString;
            #endregion
            #region Comments
            // This is the test code for performance evaluation on each command type
            // Will require the addition of sql timer functions.
            // Normal usage should be execute schalar.

            // SqlConnection myConnection = new SqlConnection(myConnectionString);
            // SqlCommand myCommand = new SqlCommand(mySelectQuery, myConnection);
            // FmainPassed.DbIo.SqlDbCommand.ExecuteReader();
            // FmainPassed.DbIo.SqlDbCommand.ExecuteReader(new CommandBehavior()); == CommandBehavior.Default
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.KeyInfo);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.SchemaOnly);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.SequentialAccess);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.SingleResult);
            // SqlDataReader myReader = FmainPassed.DbIo.SqlDbCommand.ExecuteReader(CommandBehavior.SingleRow);

            // When CommandBehavior.CloseConnection
            // Implicitly closes the connection because 
            // CommandBehavior.CloseConnection was specified.
            // otherwise:

            // Int32 count = (int32)cmd.ExecuteScalar();
            #endregion
            FileState.TableTestAccessResult = StateIs.InProgress;
            FmainPassed.FileStatus.bpDoesExist = false;
            FmainPassed.RowInfoDb.RowCount = 0;
            //  check Connection Name
            if (FmainPassed.Fs.TableNameLine.Length == 0 || !FmainPassed.FileStatus.bpNameIsValid)
            {
                FmainPassed.Fs.TableNameLine = TableNameLineBuild(ref FmainPassed);
            }
            //  check Connection
            FileState.TableTestAccessResult = FmainPassed.FileSqlConn.ConnCheck(ref FmainPassed);
            if (StateIsSuccessfulAll(FileState.TableTestAccessResult))
            {
                FileState.TableTestAccessResult = StateIs.InProgress;
                //
                if (FileState.TableTestAccessResult == StateIs.Successful || FileState.TableTestAccessResult == StateIs.InProgress)
                {
                    // UseErSingleResult
                    // UseErSchemaOnly
                    // UseErKeyInfo
                    FmainPassed.RowInfo.UseMethod = FileIo_SqlCommandModeIs.SingleRow;
                    // UseErSequentialAccess));
                    FmainPassed.RowInfo.CloseIsNeeded = false;
                    if (!FmainPassed.FileStatus.bpIsOpen)
                    {
                        FileState.TableTestAccessResult = StateIs.InProgress;
                        #region Sql Command Behavior File probing
                        try
                        {
                            #region Sql Command Behavior Cases
                            //
                            //
                            FileState.TableTestAccessResult = FmainPassed.FileSqlConn.DatabaseListCheck(ref FmainPassed, false, false);
                            //
                            // FmainPassed.DbIo.SqlDbCommand.ResetCommandTimeout();
                            //
                            if (((long)FmainPassed.RowInfo.UseMethod
                                & (long)(FileIo_SqlCommandModeIs.SingleResult
                                    | FileIo_SqlCommandModeIs.SchemaOnly
                                    | FileIo_SqlCommandModeIs.KeyInfo
                                    | FileIo_SqlCommandModeIs.SingleRow
                                    | FileIo_SqlCommandModeIs.SequentialAccess)) != 0
                                )
                            {
                                if (FmainPassed.DbIo.SqlDbDataReader != null)
                                {
                                    if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed)
                                    {
                                        FmainPassed.RowInfo.CloseIsNeeded = true;  // Only used for research Benchmark Loop
                                    }
                                }
                            }
                            #endregion

                            #region catch errors on Read Mode
                        }
                        catch (SqlException ExceptionSql)
                        {
                            LocalMessage.ErrorMsg = sEmpty;
                            ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.TableTestAccessResult);
                            FileState.TableTestAccessResult = StateIs.DatabaseError;
                            FmainPassed.FileStatus.bpDoesExist = false;
                            FmainPassed.RowInfoDb.RowCount = 0;
                            FmainPassed.FileStatus.bpIsOpen = false;
                        }
                        catch (Exception ExceptionGeneral)
                        {
                            LocalMessage.ErrorMsg = sEmpty;
                            ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.TableTestAccessResult);
                            FileState.TableTestAccessResult = StateIs.OsError;
                            FmainPassed.FileStatus.bpDoesExist = false;
                            FmainPassed.RowInfoDb.RowCount = 0;
                            FmainPassed.FileStatus.bpIsOpen = false;
                        }
                        finally
                        {
                            // FmainPassed.DbIo.SqlDbCommand = null;
                            // mFileState.TableTestAccessResult = Faux.RowInfoDb.RowCount;
                        }
                        // If File is NOT Open Try to Select the File Name in the Master File
                        #endregion
                        #endregion
                        #region Handle results of probing
                        if (FmainPassed.RowInfo.HasRows)
                        {
                            FmainPassed.FileStatus.ipDoesExistResult = StateIs.DoesExist;
                            FmainPassed.FileStatus.bpDoesExist = true;
                        }
                        else {
                            FmainPassed.FileStatus.ipDoesExistResult = StateIs.DoesNotExist;
                            FmainPassed.FileStatus.bpDoesExist = false;
                            FmainPassed.FileStatus.bpIsOpen = false;
                        }
                        // 
                        #region Set Status and Dispose
                        if (FmainPassed.FileStatus.bpIsConnected && FmainPassed.DbIo.SqlDbConn != null)
                        {
                            // <Area Id = "Close connected">
                            if (!FmainPassed.FileStatus.bpDoKeepConn)
                            {
                                FileState.TableTestAccessResult = FmainPassed.FileSqlConn.ConnClose(ref FmainPassed);
                                if (FmainPassed.RowInfoDb.RowCount > 0)
                                {
                                    FileState.TableTestAccessResult = StateIs.DoesExist;
                                }
                            }
                        }
                        if (FmainPassed.DbIo.SqlDbDataReader != null)
                        {
                            FmainPassed.DbIo.SqlDbDataReader.Dispose();
                            FmainPassed.DbIo.SqlDbDataReader = null;
                        }
                        if (FmainPassed.DbIo.SqlDbDataWriter != null)
                        {
                            FmainPassed.DbIo.SqlDbDataWriter.Dispose();
                            FmainPassed.DbIo.SqlDbDataWriter = null;
                        }
                        if (FmainPassed.FileStatus.bpDoDispose && FmainPassed.DbIo.SqlDbConn != null)
                        {
                            // <Area Id = "Dispose connected">
                            FmainPassed.DbIo.SqlDbConn.Dispose();
                            FmainPassed.DbIo.SqlDbConn = null;
                        }
                        #endregion
                    }
                    else {
                        FmainPassed.FileStatus.ipDoesExistResult = StateIs.Failed;
                        FmainPassed.FileStatus.bpDoesExist = false;
                        FmainPassed.FileStatus.bpIsOpen = false;
                    }
                }
                #endregion
            }
            return FileState.TableTestAccessResult;
        }
        #endregion
        #region TableClose
        // <Section Id = "x
        public StateIs TableClose(ref mFileMainDef FmainPassed)
        {
            FileState.TableCloseResult = StateIs.Started;
            // close reader / writer
            //
            try
            {
                if (FmainPassed.DbIo.SqlDbDataReader != null)
                {
                    if (!FmainPassed.DbIo.SqlDbDataReader.IsClosed) { FmainPassed.DbIo.SqlDbDataReader.Close(); }
                }
            }
            catch {; }
            // Close writer
            try
            {
                if (FmainPassed.DbIo.SqlDbDataWriter != null) { FmainPassed.DbIo.SqlDbDataWriter.Dispose(); }
            }
            catch {; }
            // Close / dispose Command Adapter
            try
            {
                if (FmainPassed.DbIo.SqlDbAdapterObject != null)
                {
                    FmainPassed.DbIo.SqlDbAdapterObject.Dispose();
                    FmainPassed.DbIo.SqlDbAdapterObject = null;
                }
            }
            catch {; }
            // close connection
            try
            {
                if (FmainPassed.DbIo.SqlDbConn != null)
                {
                    if (FmainPassed.DbIo.SqlDbConn.State.ToString() == "Open")
                    {
                        FileState.SqlColAddCmdBuildResult = FmainPassed.FileSqlConn.ConnClose(ref FmainPassed);
                    }
                    else if (FmainPassed.DbIo.SqlDbConn.State.ToString() != "Open")
                    {
                        FileState.SqlColAddCmdBuildResult = FmainPassed.FileSqlConn.ConnReset(ref FmainPassed);
                        // SqlColAddCmdBuildResult = ConnOpen(ref SqlConnection FmainPassed.DbIo.SqlDbConn, spTableName, FileId.spTableNameFull);
                        // FmainPassed.DbIo.SqlDbConn.Close();
                    }
                }
            }
            catch {; }
            //
            // reset all file control fields
            FmainPassed.FileStatus.DataClear();
            FmainPassed.ConnStatus.DataClear();
            FmainPassed.Fs.DbId.DataClear();
            FmainPassed.DbIo.DataClear();

            FmainPassed.FileSqlConn.DbMasterSyn.DataClear();
            //
            PickRow.sdIndexDoSearch = false;

            FmainPassed.Item.ItemIdExists = false;

            FmainPassed.FileSqlConn.DbSyn.bpSqlColumnViewCmdFirst = false;

            FmainPassed.ColTrans.HasRows = false;

            FileState.TableCloseResult = StateIs.Successful;
            return FileState.TableCloseResult;
        }
        #endregion
        #region TableCreateToDo
        // <Section Id = "x
        public StateIs TableCreateToDo(ref mFileMainDef FmainPassed)
        {
            FileState.TableCreateResult = StateIs.Started;
            if (FmainPassed.DbIo.CommandCurrent.Length > 0)
            {
                FileState.TableCreateResult = StateIs.InProgress;
                // FmainPassed.DbIo.CommandCurrent = "CREATE TABLE " + "'" + FmainPassed.Fs.TableNameLine + "'";
                // command
                FmainPassed.DbIo.SqlDbCommand = new SqlCommand(FmainPassed.DbIo.CommandCurrent, FmainPassed.DbIo.SqlDbConn);
            }
            else {
                FmainPassed.Fs.CopyTo(ref Faux.Fs);
                Faux.FileStatus.bpDoKeepConn = true;
                //
                FileState.TableCreateResult = AsciiFileReadRecord(ref Faux);
                //
                FileState.TableCreateResult = StateIs.InProgress;
                Faux.FileSqlConn.DbSyn.spSqlCreateCmd = sEmpty;
                try
                {
                    FileState.TableCreateResult = StateIs.InProgress;
                    Faux.DbIo.SqlDbRowsAffected = Faux.DbIo.SqlDbCommand.ExecuteNonQuery();
                    if (Faux.DbIo.SqlDbRowsAffected > 0)
                    {
                        Faux.FileStatus.bpDoesExist = true;
                        // Add Column 0
                        Faux.DbIo.CommandCurrent = Faux.FileSqlConn.DbSyn.spOutputAlterCommand + " " + "'" + Faux.Fs.spTableName + "'";
                        Faux.DbIo.CommandCurrent += " ADD 0 String ";
                        Faux.DbIo.CommandCurrent += " VARCHAR(512)";
                        Faux.DbIo.CommandCurrent += " { PRIMARY KEY }";
                        Faux.FileSqlConn.DbSyn.spSqlCreateCmd = Faux.DbIo.CommandCurrent;
                        Faux.DbIo.SqlDbCommand = new SqlCommand(Faux.FileSqlConn.DbSyn.spSqlCreateCmd, Faux.DbIo.SqlDbConn);
                        Faux.DbIo.SqlDbRowsAffected = Faux.DbIo.SqlDbCommand.ExecuteNonQuery();
                        // Add Primary Key
                        // Faux.DbIo.CommandCurrent = " { PRIMARY KEY }";
                        // Add Unique
                        // Faux.DbIo.CommandCurrent = " { UNIQUE }";
                    }
                    else {
                        Faux.FileStatus.bpDoesExist = false;
                    }
                }
                catch (SqlException ExceptionSql)
                {
                    LocalMessage.ErrorMsg = sEmpty;
                    ExceptSqlImpl(ref Faux, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.TableCreateResult);
                    Faux.FileStatus.bpDoesExist = false;
                }
                catch (Exception ExceptionGeneral)
                {
                    LocalMessage.ErrorMsg = sEmpty;
                    ExceptGeneralFileImpl(ref Faux, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.TableCreateResult);
                    Faux.FileStatus.bpDoesExist = false;
                }
                finally
                {
                    Faux.DbIo.SqlDbCommand = null;
                }
            }
            return FileState.TableCreateResult;
        }
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx //

    }
    /// <summary>
    /// <para> A group of temporary object, pointers and fields.</para>
    /// </summary>
}
namespace Mdm.Oss.File.Db.Table
{
    public class DbFileTempDef
    {
        // Temp Objects
        /// <summary>
        /// </summary>
        public System.Type tThisTempType;
        /// <summary>
        /// </summary>
        public Object ooThisTempObject;
        /// <summary>
        /// </summary>
        public String sThisTempString;
        /// <summary>
        /// </summary>
        public int iThisTempInt;
        /// <summary>
        /// </summary>
        public bool bThisTempBool;
        /// <summary>
        /// </summary>
        public Object ooTmp;
        /// <summary>
        /// </summary>
        public Object ooThis;
        // Temp Fields
    }
}
