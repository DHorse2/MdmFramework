#region Dependencies
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Mdm;
#region  Mdm Core
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.File.Control;
// using Mdm.Oss.Mobj;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Components;
using Mdm.World;
#endregion
#endregion

namespace Mdm.Oss.File
{ 
    public partial class mFileDef
    {
        #region File Action Section
        //
        /// <summary> 
        /// Do File Action.
        /// </summary> 
        /// ToDo StateIsSuccessfulAll
        public StateIs FileDo(object sender)
        {
            // MFile1 FbasePassed, String PassedFileName
            // bool hasSuccess = false;
            FileDoResult = StateIs.Started;
            // defaults:
            // ToDo = ToDo. Obsolete.
            //Fmain.FileAction.ToDo = Fmain.Fs.FileIo.FileReadMode; // ?OK?
            //
            //Fmain.FileAction.FileObject = FileObject;
            //Fmain.FileAction.Direction = Fmain.Fs.Direction;
            //Fmain.FileAction.Mode = Fmain.Fs.FileIo.FileAccessMode; // IE. Sql
            // These should have defaults and not have to be set.
            //Fmain.FileAction.FileReadMode = Fmain.Fs.FileIo.FileReadMode; // ?OK?
            //Fmain.FileAction.DoRetry = bYES;
            //Fmain.FileAction.DoClearTarget = bNo;

            FileDoResult = FileDo(ref Fmain);
            if (FileDoResult == StateIs.ShouldNotExist)
            {
                // ToDo $$$CHECK ??? FileTransformControl.OutFile = null;
                // hasSuccess = false;
            }
            return FileDoResult;
        }
        //
        /// <summary> 
        /// Do File Action for Passed File.
        /// Main Method for all file actions.
        /// Centralised in order for muilti-threading 
        /// and messaging to operate.
        /// </summary> 
        public StateIs FileDo(ref mFileMainDef FmainPassed)
        {
            FileDoResult = StateIs.Started;
            FileState.FileDoOpenResult = FileState.FileDoCloseResult
                = FileState.FileDoCheckResult = FileState.FileDoGetResult = StateIs.None;
            #region FileDo Top
            String FileNameCurr = sEmpty;
            String FileNameNext = FmainPassed.Fs.FileNameGetFrom(ref FmainPassed.Fs);
            String TryNextMessage = sEmpty;
            bool DoRetry = bYES;
            bool DoSkipNull = bNO;
            bool DoNextTryFirst = bYES;
            long MethodIterationLoopCounter;
            // File Action
            FileDoResult = FmainPassed.FileAction.Result
                = FmainPassed.FileAction.IoTypeResult = StateIs.Started;
            FmainPassed.FileAction.ResultObject = null;
            FmainPassed.FileAction.ActionInfo.omvOfResult = StateIs.InProgress;
            FmainPassed.FileAction.ActionInfo.omvOfObject = FileAction_ToDoTargetIs.ObjectFILEDATA.ToString();
            FmainPassed.FileAction.ActionInfo.omvOfTarget = FmainPassed.FileAction.ToDo.ToString();
            FmainPassed.FileAction.ActionInfo.omvOfResult = StateIs.Successful;
            FmainPassed.FileAction.ActionInfo.omvOfExistStatus = 0;
            FmainPassed.FileAction.ActionInfo.omvOfVerb =
                FmainPassed.FileAction.ActionInfo.omvOfObject
                + FmainPassed.FileAction.ActionInfo.omvOfTarget
                + FmainPassed.FileAction.ActionInfo.omvOfResult.ToString();
            #endregion
            try
            {
                #region Validation - ToDo, Direction, Mode (???), and Read Mode
                #region Target Action ToDo validation
                FmainPassed.FileAction.Name = "UNDEFINED";
                try
                {
                    FmainPassed.FileAction.Name = Enum.GetName(typeof(FileAction_ToDoIs), FmainPassed.FileAction.ToDo);
                }
                catch
                {
                    FileState.FileDoOpenResult = StateIs.ProgramInvalid;
                    LocalMessage.Msg6 = "Unknown File Action(#0) for (" + FmainPassed.FileAction.Name + ")!";
                    LocalMessage.Msg6 += "File Action Verb (" + FmainPassed.FileAction.ToDo + ") not properly set";
                    LocalMessage.ErrorMsg = LocalMessage.Msg6;
                    throw new NotSupportedException(LocalMessage.Msg6);
                }
                #endregion
                #region File Action Direction validation
                if (!Enum.IsDefined(typeof(FileAction_DirectionIs), FmainPassed.FileAction.Direction))
                {
                    FileState.FileDoOpenResult = StateIs.ProgramInvalid;
                    LocalMessage.Msg6 = "Unknown Direction for file.";
                    LocalMessage.Msg6 += " Mode (" + FmainPassed.FileAction.ModeName
                        + ") with File Direction ("
                        + FmainPassed.FileAction.Direction.ToString()
                        + ") " + FmainPassed.FileAction.DirectionName
                        + ", for file \"" + FmainPassed.Fs.FileNameLineGetFrom(ref FmainPassed.Fs)
                        + "\".";
                    LocalMessage.ErrorMsg = LocalMessage.Msg6;
                    throw new NotSupportedException(LocalMessage.ErrorMsg);
                }
                #endregion
                #region File Action ToDo validation (Mode? Refactor this ToDo)
                // Notice the field names. I am checking ToDo
                // but setting FileModeValid and IoTypeResult.
                // This need cleaning up.
                FmainPassed.FileAction.IoTypeResult = StateIs.InProgress;
                bool FileModeValid = true;
                if (FmainPassed.FileAction.ToDo == FileAction_ToDoIs.NotSet
                    || !Enum.IsDefined(typeof(FileAction_ToDoIs), FmainPassed.FileAction.ToDo))
                {
                    if (FmainPassed.FileAction.KeyName.Length > 0)
                    {
                        try
                        {
                            FmainPassed.FileAction.ToDo
                                = (FileAction_ToDoIs)Enum.Parse(typeof(FileAction_ToDoIs), FmainPassed.FileAction.KeyName);
                            FileModeValid = true;
                        }
                        catch { FileModeValid = false; }
                    }
                    else { FileModeValid = false; }
                }
                if (!FileModeValid)
                {
                    FileState.FileDoOpenResult = StateIs.ProgramInvalid;
                    LocalMessage.Msg6 = "File Action(#1) (" + FmainPassed.FileAction.ToDo + ") "
                        + FmainPassed.FileAction.Name + "! " + "\n";
                    LocalMessage.Msg6 += "Unknown File Mode (" + FmainPassed.FileAction.ToDo + ") "
                    + "and the lookup key \"" + FmainPassed.FileAction.KeyName + "\" "
                    + "are not valid!!! " + "\n";
                    LocalMessage.Msg6 += "Occured on file \"" + FmainPassed.Fs.FileNameLineGetFrom(ref FmainPassed.Fs) + "\"";
                    FmainPassed.FileAction.ModeName = "Mode UNKNOWN!";
                    LocalMessage.ErrorMsg = LocalMessage.Msg6;
                    throw new NotSupportedException(LocalMessage.ErrorMsg);
                }
                #endregion
                #region File Read Mode Validation
                bool FileReadModeValid = true;
                if (FmainPassed.FileAction.FileReadMode == (long)FileAction_ReadModeIs.None
                    || !Enum.IsDefined(typeof(FileAction_ReadModeIs), FmainPassed.FileAction.FileReadMode))
                {
                    if (FmainPassed.FileAction.KeyName.Length > 0)
                    {
                        try
                        {
                            FmainPassed.FileAction.FileReadMode
                                = (FileAction_ReadModeIs)Enum.Parse(typeof(FileAction_ReadModeIs), FmainPassed.FileAction.KeyName);
                            FileReadModeValid = true;
                        }
                        catch { FileReadModeValid = false; }
                    }
                    else { FileReadModeValid = false; }
                }
                if (!FileReadModeValid)
                {
                    FileState.FileDoOpenResult = StateIs.ProgramInvalid;
                    FileReadModeValid = false;
                    LocalMessage.Msg6 = "File Action(#2) (" + FmainPassed.FileAction.ToDo + ") " + FmainPassed.FileAction.Name + "! " + "\n";
                    LocalMessage.Msg6 += "Unknown File Read Mode (" + FmainPassed.FileAction.FileReadMode + ") "
                        + "and the lookup key \"" + FmainPassed.FileAction.KeyName + "\" "
                        + "are not valid!!! " + "\n";
                    LocalMessage.Msg6 += "Occured on file \"" + FmainPassed.Fs.FileNameLineGetFrom(ref FmainPassed.Fs) + "\"";
                    LocalMessage.ErrorMsg = LocalMessage.Msg6;
                    //((ImTrace)ConsoleSender).TraceMdmDoImpl(ConsoleFormUses.DatabaseLog, 6, ref Sender, bIsMessage,
                    //    TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(),
                    //    FileDoResult, RunErrorDidOccur = true,
                    //    iNoErrorLevel, iNoErrorSource,
                    //    bDoNotDisplay, MessageNoUserEntry,
                    //    LocalMessage.ErrorMsg);
                    throw new NotSupportedException(LocalMessage.ErrorMsg);
                }
                #endregion
                #endregion
                // --------------------------------- //
                #region Perform Action
                // Loggging Message
                LocalMessage.LogEntry = FileMainHeaderGetTo(ref FmainPassed);
                // Trace
               st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 6, ref Sender, bIsMessage,
                    TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(),
                    FileDoResult, bNoError,
                    iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry,
                    LocalMessage.LogEntry);
                // File Action
                switch (FmainPassed.FileAction.Name)
                {
                    #region Check
                    case ("Check"):
                        // Faux.DataClear();
                        // Faux = FmainPassed;
                        FmainPassed.Fs.CopyTo(ref Faux.Fs);
                        FmainPassed.FileAction.CopyTo(ref Faux.FileAction);
                        Faux.FileAction = FmainPassed.FileAction;
                        Faux.IoState = FmainPassed.IoState;
                        // Faux.DbIo = FmainPassed.DbIo;
                        // Faux.Fs.FileIo = FmainPassed.Fs.FileIo;
                        // FileIo
                        Faux.Fs.Direction = FmainPassed.FileAction.Direction;
                        Faux.Fs.FileIo.IoMode = Fmain.FileAction.IoMode;
                        Faux.Fs.FileIo.FileReadMode = Fmain.FileAction.FileReadMode;
                        Faux.Fs.FileIo.FileWriteMode = Fmain.FileAction.FileWriteMode;
                        Faux.Fs.FileIo.ToDo = Fmain.FileAction.ToDo;
                        Faux.Fs.FileIo.ToDoTarget = Fmain.FileAction.FileAccessMode;
                        //
                        switch (Faux.FileAction.ToDoTarget)
                        {
                            // System
                            case (FileAction_ToDoTargetIs.System):
                                FileState.FileDoCheckResult = Faux.FileSqlConn.SystemListCheck(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            // Database
                            case (FileAction_ToDoTargetIs.Server):
                                FileState.FileDoCheckResult = Faux.FileSqlConn.ServerListCheck(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            case (FileAction_ToDoTargetIs.Service):
                                FileState.FileDoCheckResult = Faux.FileSqlConn.ServiceListCheck(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            case (FileAction_ToDoTargetIs.Database):
                                FileState.FileDoCheckResult = Faux.FileSqlConn.DatabaseListCheck(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            case (FileAction_ToDoTargetIs.Table):
                                // Faux.Fs.TableNameSetFromLine(Faux.Fs.TableNameLine);
                                FileState.FileDoCheckResult = Faux.FileSqlConn.TableListCheck(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            case (FileAction_ToDoTargetIs.FileGroup):
                                break;
                            // FSO Files
                            case (FileAction_ToDoTargetIs.DiskFile):
                                // Faux.Fs.FileId.FileNameSetFromLine(Faux.Fs.FileId.FileNameLine);
                                if (TextFileDoesExist(ref Faux))
                                {
                                    FileState.FileDoCheckResult = StateIs.DoesExist;
                                }
                                else { FileState.FileDoCheckResult = StateIs.DoesNotExist; }
                                break;
                            case (FileAction_ToDoTargetIs.AsciiDef):
                                break;
                            // Database Security
                            case (FileAction_ToDoTargetIs.DbUser):
                                break;
                            case (FileAction_ToDoTargetIs.DbPassword):
                                break;
                            case (FileAction_ToDoTargetIs.DbSecurityType):
                                break;
                            default:
                                FileState.FileDoCheckResult = StateIs.ProgramInvalid;
                                //
                                LocalMessage.Msg6 = "Invalid File Action(#3) FileAction_DoTargetIs (" + Faux.Fs.Direction + ") " + Faux.FileAction.ModeName + "! " + "\n";
                                LocalMessage.Msg6 += "Unknown File Action Target (" + Faux.Fs.FileIo.ToDoTarget + "). ";
                                LocalMessage.Msg6 += " on file \"" + Faux.Fs.FileNameLineGetFrom(ref Faux.Fs) + "\"";
                                LocalMessage.ErrorMsg = LocalMessage.Msg6;
                                throw new NotSupportedException(LocalMessage.ErrorMsg);
                                break;
                        }
                        #region Display and modify results of Action based on file options
                        #region Display results of Action
                        StateDescriptionGetTo(ref FmainPassed);
                        ResultDescriptionLong(ref FmainPassed);
                        #endregion
                        // Handle results of close failure here
                        #endregion
                        break;
                    #endregion
                    #region Get
                    case ("ListGet"):
                        List<String> ObjectList = null;
                        // Faux.DataClear();
                        // Faux = FmainPassed;
                        FmainPassed.Fs.CopyTo(ref Faux.Fs);
                        FmainPassed.FileAction.CopyTo(ref Faux.FileAction);
                        Faux.FileAction = FmainPassed.FileAction;
                        Faux.IoState = FmainPassed.IoState;
                        // Faux.DbIo = FmainPassed.DbIo;
                        // Faux.Fs.FileIo = FmainPassed.Fs.FileIo;
                        // FileIo
                        Faux.Fs.Direction = FmainPassed.FileAction.Direction;
                        Faux.Fs.FileIo.IoMode = Fmain.FileAction.IoMode;
                        Faux.Fs.FileIo.ToDo = Fmain.FileAction.ToDo;
                        Faux.Fs.FileIo.ToDoTarget = Fmain.FileAction.ToDoTarget;
                        Faux.Fs.FileIo.FileReadMode = Fmain.FileAction.FileReadMode;
                        Faux.Fs.FileIo.FileWriteMode = Fmain.FileAction.FileWriteMode;
                        //
                        switch (Faux.FileAction.ToDoTarget)
                        {
                            // System
                            case (FileAction_ToDoTargetIs.System):
                                ObjectList = Faux.FileSqlConn.SystemListGetTo(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            // Server
                            case (FileAction_ToDoTargetIs.Server):
                                ObjectList = Faux.FileSqlConn.ServerListGetTo(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            // Service
                            case (FileAction_ToDoTargetIs.Service):
                                ObjectList = Faux.FileSqlConn.ServiceListGetTo(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            // Database
                            case (FileAction_ToDoTargetIs.Database):
                                ObjectList = Faux.FileSqlConn.DatabaseListGetTo(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            case (FileAction_ToDoTargetIs.Table):
                                // Faux.Fs.TableNameSetFromLine(Faux.Fs.TableNameLine);
                                ObjectList = Faux.FileSqlConn.TableListGetTo(ref Faux, Faux.FileAction.DoClearTarget, Faux.FileAction.DoGetUiVs);
                                break;
                            case (FileAction_ToDoTargetIs.FileGroup):
                                break;
                            // FSO Files
                            case (FileAction_ToDoTargetIs.DiskFile):
                                // Faux.Fs.FileId.FileNameSetFromLine(Faux.Fs.FileId.FileNameLine);
                                if (TextFileDoesExist(ref Faux))
                                {
                                    FileState.FileDoGetResult = StateIs.DoesExist;
                                }
                                else { FileState.FileDoGetResult = StateIs.DoesNotExist; }
                                break;
                            case (FileAction_ToDoTargetIs.AsciiDef):
                                break;
                            // Database Security
                            case (FileAction_ToDoTargetIs.DbUser):
                                break;
                            case (FileAction_ToDoTargetIs.DbPassword):
                                break;
                            case (FileAction_ToDoTargetIs.DbSecurityType):
                                break;
                            default:
                                FileState.FileDoGetResult = StateIs.ProgramInvalid;
                                //
                                LocalMessage.Msg6 = "Invalid File Action(#4) FileAction_DoTarget (" + Faux.FileAction.Direction + ") " + Faux.FileAction.ModeName + "! " + "\n";
                                LocalMessage.Msg6 += "Unknown File Action Target (" + Faux.FileAction.ToDoTarget.ToString() + ").";
                                LocalMessage.Msg6 += " on file \"" + Faux.Fs.FileNameLineGetFrom(ref FmainPassed.Fs) + "\"";
                                LocalMessage.ErrorMsg = LocalMessage.Msg6;
                                throw new NotSupportedException(LocalMessage.ErrorMsg);
                                break;
                        }
                        if (ObjectList != null)
                        {
                            FileState.FileDoGetResult = StateIs.Successful;
                            FmainPassed.FileAction.ResultObject = ObjectList;
                        }
                        else {
                            FileState.FileDoGetResult = StateIs.EmptyResult;
                            FmainPassed.FileAction.ResultObject = null;
                        }
                        #region Display and modify results of Action based on file options
                        #region Display results of Action
                        StateDescriptionGetTo(ref FmainPassed);
                        ResultDescriptionLong(ref FmainPassed);
                        #endregion
                        // Handle results of close failure here
                        #endregion
                        break;
                    #endregion
                    #region Open
                    case ("Open"):
                        FileState.FileDoOpenResult = StateIs.TryAgain; // FileAction_OpenControl.TryFirst;
                        FileDoResult = FileState.FileDoOpenResult;
                        FileNameCurr = sEmpty;
                        FileNameNext = FmainPassed.Fs.FileNameGetFrom(ref FmainPassed.Fs);
                        TryNextMessage = sEmpty;
                        // DoRetry = FmainPassed.FileAction.DoRetry;
                        DoRetry = bYES;
                        DoSkipNull = bNO;
                        DoNextTryFirst = bYES;
                        // FileIo
                        FmainPassed.Fs.Direction = FmainPassed.FileAction.Direction;
                        FmainPassed.Fs.FileIo.IoMode = FmainPassed.FileAction.IoMode;
                        FmainPassed.Fs.FileIo.ToDo = Fmain.FileAction.ToDo;
                        FmainPassed.Fs.FileIo.ToDoTarget = Fmain.FileAction.ToDoTarget;
                        FmainPassed.Fs.FileIo.FileReadMode = Fmain.FileAction.FileReadMode;
                        //
                        #region Loop for Opening File
                        // Note 3. I am aware this is ineffecient
                        // and excessive but this was
                        // a function / line by line port
                        // of a portion of the PICK tools.
                        for (MethodIterationLoopCounter = 1; (MethodIterationLoopCounter <= 3 && DoRetry); MethodIterationLoopCounter++)
                            // for (MethodIterationLoopCounter = 1; (MethodIterationLoopCounter <= (long)FileAction_OpenControl.TryAll && mFileState.FileDoOpenResult < (long)FileAction_OpenControl.TryAll && DoRetry); MethodIterationLoopCounter++)
                        {
                            #region Choose File to try
                            // default output file handling...
                            TryNextMessage = sEmpty;
                            if (!DoNextTryFirst)
                            {
                                TryNextMessage += "Bad file name";
                            }
                            else {
                                TryNextMessage += "Opening file name";
                            }
                            TryNextMessage += " \"" + FmainPassed.Fs.FileNameLineGetFrom(ref FmainPassed.Fs) + "\".";
                            // ToDo $$$CHECK PrintOutputMdm_PickPrint(Sender, 3, (bool)bYES);
                            switch (MethodIterationLoopCounter)
                            {
                                case (2):
                                    // Try to open the Default (Installation) file.
                                    // Note 1. This would be the default location
                                    // of an app or database.
                                    // IE. From the installation defaults.
                                    // Note 2.
                                    // It is not implemented in
                                    // the Mdm ClipUtil or LinkUtil.
                                    // I believe it is used in the 
                                    // Mdm Srt Database Converter.
                                      
                                    // FileAction_OpenControl.TryDefault
                                    TryNextMessage += ". Trying the Default file.";
                                    FileNameNext = FmainPassed.Fs.FileNameFullDefault;
                                    //////switch (ToDo.Direction) {
                                    //////    case (Name.FileAction_DirectionIs.Input):
                                    //////        FileNameFullNext = FileTransformControl.InFile.Fmain.Fs.FileNameFullDefault;
                                    //////        break;
                                    //////    case (Name.FileAction_DirectionIs.Output):
                                    //////        FileNameFullNext = FileTransformControl.OutFile.Fmain.Fs.FileNameFullDefault;
                                    //////        break;
                                    //////    default:
                                    //////        break;
                                    //////}
                                    break;
                                // Reloop and try opening the Original File Name
                                case (3):
                                    // ToDo Document this. From conversions.
                                    // FileAction_OpenControl.TryOriginal
                                    // original output file handling...
                                    TryNextMessage += ". Trying the Original file.";
                                    FileNameNext = FmainPassed.Fs.FileNameFullOriginal;
                                    //////switch (ToDo.Direction) {
                                    //////    case (Name.FileAction_DirectionIs.Input):
                                    //////        FileNameFullNext = FileTransformControl.InFile.Fmain.Fs.FileNameFullOriginal;
                                    //////        break;
                                    //////    case (Name.FileAction_DirectionIs.Output):
                                    //////        FileNameFullNext = FileTransformControl.OutFile.Fmain.Fs.FileNameFullOriginal;
                                    //////        break;
                                    //////    default:
                                    //////        break;
                                    //////}
                                    //
                                    break;
                                // Reloop and try opening the Entered File Name
                                case (1):
                                    // FileAction_OpenControl.TryEntered
                                    TryNextMessage += ". Trying the CHOSEN file.";
                                    FileNameNext = FmainPassed.Fs.FileNameLineGetFrom(ref FmainPassed.Fs);
                                    //////switch (ToDo.Direction) {
                                    //////    case (Name.FileAction_DirectionIs.Input):
                                    //////        FileNameFullNext = PassedFileName;
                                    //////        break;
                                    //////    case (Name.FileAction_DirectionIs.Output):
                                    //////        FileNameFullNext = PassedFileName;
                                    //////        break;
                                    //////    default:
                                    //////        break;
                                    //////}
                                    break;
                                default:
                                    // Loop Error on reloop for other file names
                                    // original output file handling...
                                    TryNextMessage += "!!! Error: Invalid File!!! Trying Original file name";
                                    FileNameNext = FmainPassed.Fs.FileNameFullOriginal;
                                    //////switch (ToDo.Direction) {
                                    //////    case (Name.FileAction_DirectionIs.Input):
                                    //////        //
                                    //////        FileNameFullNext = FileTransformControl.InFile.Fmain.Fs.FileNameFullOriginal;
                                    //////        break;
                                    //////    case (Name.FileAction_DirectionIs.Output):
                                    //////        //
                                    //////        FileNameFullNext = FileTransformControl.OutFile.Fmain.Fs.FileNameFullOriginal;
                                    //////        break;
                                    //////    default:
                                    //////        break;
                                    //////}
                                    //
                                    if (MethodIterationLoopCounter + 1 <= 3 && DoRetry)
                                    //if (MethodIterationLoopCounter + 1 <= (long)FileAction_OpenControl.TryAll && (long)mFileState.FileDoOpenResult < (long)FileAction_OpenControl.TryAll && DoRetry)
                                    {
                                            TryNextMessage += FmainPassed.FileAction.ModeName + ". Import File Direction ("
                                            + FmainPassed.FileAction.Direction.ToString() + ") "
                                            + FmainPassed.FileAction.Direction + ", for file \""
                                            + FmainPassed.Fs.FileNameLineGetFrom(ref FmainPassed.Fs) + "\".";
                                        LocalMessage.Msg6 = TryNextMessage;
                                        LocalMessage.ErrorMsg = LocalMessage.Msg6;
                                       st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 6, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), FileState.FileDoOpenResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                                        //
                                        TryNextMessage = "Will now try \"" + FileNameNext + "\".";
                                        LocalMessage.Msg6 = TryNextMessage;
                                       st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 6, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), FileState.FileDoOpenResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg6);
                                        // blank line...
                                        LocalMessage.Msg6 = sEmpty;
                                        // PrintOutputMdm_PickPrint(Sender, 3, LocalMessage.Msg6, bYES);
                                    }
                                    else {
                                        // ToDo this throw needs work
                                        LocalMessage.ErrorMsg = "The file "
                                            + FmainPassed.Fs.FileNameLineGetFrom(ref FmainPassed.Fs)
                                            + ", and no suitable default file was found.";
                                        throw new NotSupportedException(LocalMessage.ErrorMsg);
                                    }
                                    break;
                            } // end of switch MethodIterationLoopCounter
                            #endregion
                            #region Log the OPEN FILE action
                            //
                            LocalMessage.Msg6 = TryNextMessage + " Open File being used...";
                           st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 6, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), FileState.FileDoOpenResult, false, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg6);
                            // PrintOutputMdm_PickPrint(Sender, 2, LocalMessage.Msg6, bYES);
                            //
                            LocalMessage.Msg6 = FmainPassed.FileAction.Name + " file for direction ("
                                + FmainPassed.FileAction.Direction.ToString() + ") "
                                + FmainPassed.FileAction.DirectionName + " for file \""
                                + FmainPassed.Fs.FileNameLineGetFrom(ref FmainPassed.Fs)
                                + "\"." + "\n";
                            if (FmainPassed.FileAction.KeyName.Length > 0)
                            {
                                LocalMessage.Msg6 += " Activity Key is " + "\""
                                    + FmainPassed.FileAction.KeyName + "\"" + ".\n";
                            }
                            LocalMessage.Msg6 += " The Action Mode ("
                            + FmainPassed.FileAction.ToDo.ToString() + ") is \""
                            + FmainPassed.FileAction.ModeName + "\"." + "\n";
                            // ToDo *** Replace below with proper message call
                           st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 6, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), FileState.FileDoOpenResult, false, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg6);
                            // PrintOutputMdm_PickPrint(Sender, 3, LocalMessage.Msg6, bYES);
                            #endregion
                            #region Open the file
                            FileNameCurr = FileNameNext;
                            LocalMessage.Msg6 = "IoMode: " + FmainPassed.FileAction.ModeName + ", ";
                            if (FileNameCurr.Length > 0)
                            {
                                try
                                {
                                    // switch (FmainPassed.FileAction.Mode) {
                                    switch (FmainPassed.Fs.FileIo.IoMode)
                                    {
                                        case (FileIo_ModeIs.Sql):
                                            FileState.FileDoOpenResult = TableOpen(ref FmainPassed);
                                            break;
                                        case (FileIo_ModeIs.Block):
                                            FileState.FileDoOpenResult = FileOpenHandle(ref FmainPassed, FileNameCurr, FmainPassed.FileAction.ToDo, 0);
                                            break;
                                        case (FileIo_ModeIs.Line):
                                            FileState.FileDoOpenResult = FileOpen(ref FmainPassed, FileNameCurr, FmainPassed.FileAction.ToDo, 0);
                                            break;
                                        case (FileIo_ModeIs.All):
                                            FileState.FileDoOpenResult = TextFileOpen(ref FmainPassed);
                                            // mFileState.FileDoOpenResult = FileOpen(FbasePassed, FileNameCurr, ToDo.Mode, 0);
                                            break;
                                        default:
                                            LocalMessage.ErrorMsg = "Bad FileIo_Mode";
                                            throw new NotSupportedException(LocalMessage.ErrorMsg);
                                            break;
                                    }
                                    #region Catch Open Errors
                                }
                                catch (SqlException ExceptionSql)
                                {
                                    LocalMessage.ErrorMsg = "SQL Exception(#1) occured in File Action";
                                    ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.FileDoOpenResult);
                                    FileDoResult = StateIs.DatabaseError;
                                }
                                catch (NotSupportedException ExceptionNotSupported)
                                {
                                    LocalMessage.ErrorMsg += "Not Supported Exception(#2) occured in File Action";
                                    ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileState.FileDoOpenResult);
                                    FileDoResult = StateIs.ProgramInvalid;
                                }
                                catch (IOException ExceptionIO)
                                {
                                    LocalMessage.ErrorMsg = "SQL Exception(#3) occured in File Action";
                                    ExceptIoImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionIO, FileState.FileDoOpenResult);
                                    FileDoResult = StateIs.Failed;
                                }
                                catch (Exception ExceptionGeneral)
                                {
                                    LocalMessage.ErrorMsg = "Unhandled Exception(#4) occured in File Action";
                                    ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.FileDoOpenResult);
                                    FileDoResult = StateIs.UnknownFailure;
                                }
                                finally
                                {
                                    if (FileDoResult == StateIs.InProgress)
                                    {
                                        FileDoResult = StateIs.Failed;
                                        LocalMessage.Msg6 = "Operation did not complete! Exception Error! ";
                                        // PrintOutputMdm_PickPrint(Sender, 3, LocalMessage.Msg6, bYES);
                                       st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 6, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), FileState.FileDoOpenResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg6);
                                        // ToDo throw error might be prefered instead.  Good for now...
                                    }
                                } // of try connect
                                #endregion
                            }
                            else {
                                FileState.FileDoOpenResult = StateIs.NotSet;
                            }
                            #endregion
                            #region Display and modify results of Action based on file options
                            #region Display results of Action
                            StateDescriptionGetTo(ref FmainPassed);
                            ResultDescriptionLong(ref FmainPassed);
                            #endregion
                            if (FileState.FileDoOpenResult == StateIs.DoesExist)
                            {
                                #region File Does Exist
                                // ToDo FileDo put option check for SHOULD EXIST / SHOULD_NOT EXIST here.
                                if (FmainPassed.FileAction.Direction == FileAction_DirectionIs.Output)
                                {
                                    // Delete File if it Does Exist
                                    if (FmainPassed.Fs.FileOpt.DoCreateFileMustNotExist)
                                    {
                                        ; // ToDo Error - Create File if Does Not Exist
                                        FileState.FileDoOpenResult = StateIs.ShouldNotExist;
                                        DoRetry = bNO;
                                    }
                                    else {
                                        if (FmainPassed.Fs.FileOpt.DoDeleteFile || FmainPassed.Fs.FileOpt.DoCreateFileNew)
                                        {
                                            ; // ToDo FileDo Delete File if it Does Exist
                                        }
                                        // Create New File regardless if it Does Exist
                                        if (FmainPassed.Fs.FileOpt.DoCreateFileNew)
                                        {
                                            ; // NOOP - Create New File regardless if it Does Exist
                                        }
                                        // Create File if Does Not Exist
                                        if (FmainPassed.Fs.FileOpt.DoCreateFileNew)
                                        {
                                            ; // ToDo FileDo Create File if Does Not Exist
                                        }
                                        // ToDo FileDo set up proper default for Good eArmhResult - Check that File Does Exist
                                        FileState.FileDoOpenResult = StateIs.Successful;
                                        DoRetry = bNO;
                                        if (FmainPassed.Fs.FileOpt.DoCheckFileDoesExist)
                                        {
                                            ; // Good eArmhResult - Check that File Does Exist
                                        }
                                    }
                                    //
                                }
                                else {
                                    // Input File handling where File Does Exist
                                    if (!FmainPassed.FileAction.DoRetry)
                                    {
                                        DoRetry = bNO;
                                        DoSkipNull = bYES;
                                    }
                                    else {
                                        DoRetry = bYES;
                                        DoSkipNull = bYES;
                                    }
                                } // end out File handling where Does Exist
                                #endregion
                            }
                            else if (FileState.FileDoOpenResult == StateIs.DoesNotExist
                                || FileState.FileDoOpenResult == StateIs.DatabaseError)
                            {
                                #region File Does Not Exist
                                if (FmainPassed.FileAction.Direction == FileAction_DirectionIs.Output)
                                {
                                    // Output File Does Not Exist
                                    if (FmainPassed.Fs.FileOpt.DoDeleteFile)
                                    {
                                        ; // NOOP - Delete File if it Does Exist
                                    }
                                    if (FmainPassed.Fs.FileOpt.DoCreateFileMustNotExist || FmainPassed.Fs.FileOpt.DoCreateFileNew)
                                    {
                                        ; // ToDo FileDo Create File if Does Not Exist
                                        FileState.FileDoOpenResult = StateIs.Successful;
                                        DoRetry = bNO;
                                    }
                                    // Check that File Does Exist
                                    if (FmainPassed.Fs.FileOpt.DoCheckFileDoesExist)
                                    {
                                        ; // ToDo Error - Check that File Does Exist
                                    }
                                    // Create File if Does Not Exist
                                    if (FmainPassed.Fs.FileOpt.DoCreateFileMustNotExist || FmainPassed.Fs.FileOpt.DoCreateFileNew)
                                    {
                                        FileState.FileDoCreateResult = StateIs.InProgress;
                                        FmainPassed.DbIo.CommandCurrent = sEmpty;
                                        try
                                        {
                                            switch (FmainPassed.Fs.FileIo.IoMode)
                                            {
                                                case (FileIo_ModeIs.Sql):
                                                    FileState.FileDoCreateResult = TableCreateToDo(ref FmainPassed);
                                                    break;
                                                case (FileIo_ModeIs.Block):
                                                    // mFileState.FileDoCreateResult = FileCreateHandle(ref FmainPassed, FileNameCurr, FileAction.Mode, 0);
                                                    break;
                                                case (FileIo_ModeIs.Line):
                                                    // mFileState.FileDoCreateResult = FileCreatemFileDef(ref FmainPassed, FileNameCurr, FileAction.Mode, 0);
                                                    break;
                                                case (FileIo_ModeIs.All):
                                                    // mFileState.FileDoCreateResult = TextFileCreate(ref FmainPassed);
                                                    // mFileState.FileDoCreateResult = FileOpen(FbasePassed, FileNameCurr, ToDo.Mode, 0);
                                                    break;
                                                default:
                                                    break;
                                            }
                                            #region Catch Open Errors
                                        }
                                        catch (SqlException ExceptionSql)
                                        {
                                            LocalMessage.ErrorMsg = "SQL Exception(#5) occured in Pick File Action";
                                            ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.FileDoCreateResult);
                                            FileDoResult = StateIs.DatabaseError;
                                        }
                                        catch (NotSupportedException ExceptionNotSupported)
                                        {
                                            LocalMessage.ErrorMsg += "Not Supported Exception occured in File Creation Action";
                                            ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileState.FileDoCreateResult);
                                            FileDoResult = StateIs.ProgramInvalid;
                                        }
                                        catch (IOException ExceptionIO)
                                        {
                                            LocalMessage.ErrorMsg = "SQL Exception occured in File Creation Action";
                                            ExceptIoImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionIO, FileState.FileDoCreateResult);
                                            FileDoResult = StateIs.Failed;
                                        }
                                        catch (Exception ExceptionGeneral)
                                        {
                                            LocalMessage.ErrorMsg = "Unhandled Exception occured in File Creation Action";
                                            ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.FileDoCreateResult);
                                            FileDoResult = StateIs.UnknownFailure;
                                        }
                                        finally
                                        {
                                            if (FileDoResult == StateIs.InProgress)
                                            {
                                                FileDoResult = StateIs.Failed;
                                                LocalMessage.Msg6 = "File Creation Operation did not complete! Exception Error! ";
                                                // PrintOutputMdm_PickPrint(Sender, 3, LocalMessage.Msg6, bYES);
                                               st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 6, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), FileState.FileDoCreateResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg6);
                                                // ToDo throw error might be prefered instead.  Good for now...
                                            }
                                        } // of try connect
                                        #endregion
                                    }
                                    // end of Ouptput File wher Does Not Exist
                                }
                                else {
                                    // Input File Does Not Exist
                                    // if (mFileState.FileDoOpenResult == StateIs.ShouldNotExist || mFileState.FileDoOpenResult == StateIs.MissingName) {
                                    if (MethodIterationLoopCounter < (long)FileAction_OpenControl.TryAll)
                                    {
                                        // retry different name;
                                        FileState.FileDoOpenResult = StateIs.TryAgain;
                                        DoRetry = bYES;
                                    }
                                    else {
                                        FileState.FileDoOpenResult = StateIs.ShouldNotExist;
                                        RunErrorDidOccur = ErrorDidOccur;
                                        FmainPassed.FileAction.ResultName = "SHOULD_NOT_EXIST";
                                        // failure, can not open file
                                        // PrintOutputMdm_PickPrint(Sender, 3, (bool)bYES);
                                        LocalMessage.Msg6 = "ABORT: Unable to open \"" + FileNameCurr + "\".";
                                        LocalMessage.Msg6 += FmainPassed.FileAction.Name + " Failed with (" + FileDoResult + "), Verb Result (" + FmainPassed.FileAction.ActionInfo.omvOfResult + ")  properly set";
                                        LocalMessage.ErrorMsg = LocalMessage.Msg6;
                                        // PrintOutputMdm_PickPrint(Sender, 3, LocalMessage.Msg6, bYES);
                                       st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 6, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), FileState.FileDoOpenResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                                        //
                                        RunAbortIsOn = bYES;
                                        DoRetry = bNO;
                                        FmainPassed = null;
                                        // 
                                        // ToDo could possibly throw new Exception(LocalMessage.ErrorMsg);
                                       st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 6, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), FileState.FileDoOpenResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                                        //
                                        LocalMessage.Msg6 = "Error, unable to open the " + FmainPassed.FileAction.Direction + " File!!!";
                                       st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 6, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), FileState.FileDoOpenResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg6);
                                    }
                                    //
                                    if (FmainPassed.Fs.FileOpt.DoCreateFileNew)
                                    {
                                        // RESET FILE DATA
                                        FileState.FileDoOpenResult = StateIs.ShouldNotExist;
                                        DoRetry = bNO;
                                    }
                                    //
                                } // end of Input / Ouptput File where Does Not Exist
                                #endregion
                            }
                            // end of Action failed File Does / Does Not Exist handling
                            DoNextTryFirst = bNO;
                            #endregion
                        } // end of for LOOP fault tolerant default file name processing
                        break;
                    #endregion
                    #endregion
                    #region Close
                    case ("Close"):
                        #region CLOSE the file
                        // FileIo
                        FmainPassed.Fs.Direction = FmainPassed.FileAction.Direction;
                        FmainPassed.Fs.FileIo.IoMode = FmainPassed.FileAction.IoMode;
                        FmainPassed.Fs.FileIo.ToDo = Fmain.FileAction.ToDo;
                        FmainPassed.Fs.FileIo.ToDoTarget = Fmain.FileAction.ToDoTarget;
                        FmainPassed.Fs.FileIo.FileReadMode = Fmain.FileAction.FileReadMode;
                        //
                        switch (FmainPassed.Fs.FileIo.IoMode)
                        {
                            case (FileIo_ModeIs.Sql):
                                // (ref mFileMainDef FmainPassed, 
                                // String PassedFileName, 
                                // String PassedFileNameFull) {
                                FileState.FileDoCloseResult = TableClose(ref FmainPassed);
                                break;
                            case (FileIo_ModeIs.Block):
                                FileState.FileDoCloseResult = AsciiFileClose(ref FmainPassed);
                                break;
                            case (FileIo_ModeIs.Line):
                                FileState.FileDoCloseResult = AsciiFileClose(ref FmainPassed);
                                break;
                            case (FileIo_ModeIs.All):
                                FileState.FileDoCloseResult = TextFileClose(ref FmainPassed);
                                // mFileState.FileDoOpenResult = Buf.FileOpen(FbasePassed, FileNameCurr, ToDo.Mode, 0);
                                break;
                            default:
                                FileState.FileDoCloseResult = StateIs.Undefined;
                                //
                                LocalMessage.Msg6 = "Invalid File Action(#6) (" + FmainPassed.FileAction.Direction + ") " + FmainPassed.FileAction.ModeName;
                                LocalMessage.Msg6 += "! Unknown File Action";
                                LocalMessage.Msg6 += " on file \"" + FmainPassed.Fs.FileNameLineGetFrom(ref FmainPassed.Fs) + "\"";
                                LocalMessage.ErrorMsg = LocalMessage.Msg6;
                                throw new NotSupportedException(LocalMessage.ErrorMsg);
                        }
                        #endregion
                        #region Display and modify results of Action based on file options
                        // Note that results are handled once (here) after an
                        // Open or Close and then secondary actions are taken that
                        // depend on file options (ie create if missing.)
                        // (not implemented here for close yet)
                        // The only additional (normal) handling after close might be
                        // to delete a file, move it, or copy it.
                        // Result will be handled again to product a final result.
                        #region Display results of Action
                        StateDescriptionGetTo(ref FmainPassed);
                        ResultDescriptionLong(ref FmainPassed);
                        #endregion
                        // Handle results of close failure here
                        // Implementation of CLOSE HANDLING HERE
                        //#region Display results of Action
                        //StateDescriptionGetTo(ref FmainPassed);
                        //ResultDescriptionLong(ref FmainPassed);
                        //#endregion
                        #endregion
                        break;
                    #endregion
                    #region Error not Check, Open or Close
                    default:
                        FileDoResult = StateIs.Undefined;
                        RunErrorDidOccur = true;
                        FmainPassed.FileAction.ModeName = "ModeUNKNOWN";
                        //
                        LocalMessage.Msg6 = "Invalid File Action(#7) (" + FmainPassed.FileAction.Direction + ") "
                            + FmainPassed.FileAction.ModeName;
                        LocalMessage.Msg6 += "! Unknown File Action";
                        LocalMessage.Msg6 += " on file \"" + FmainPassed.Fs.FileNameLineGetFrom(ref FmainPassed.Fs) + "\"";
                        LocalMessage.ErrorMsg = LocalMessage.Msg6;
                        throw new NotSupportedException(LocalMessage.ErrorMsg);
                        #endregion
                }
                #endregion
                // --------------------------------- //
                #region Process Result of Action
                if (StateIsSuccessful(FileDoResult))
                {
                    // Result has NO Error
                }
                else if (StateIsSuccessfulAll(FileDoResult))
                {
                    // Result has NO Error
                }
                else {
                    #region Result Error
                    LocalMessage.Msg6 = sEmpty;
                    LocalMessage.Msg6 += "File Action(#8) Error (" + FileDoResult.ToString() + ") ";
                    if (Enum.IsDefined(typeof(StateIs), FileDoResult))
                    {
                        LocalMessage.Msg6 += Enum.GetName(typeof(StateIs), FileDoResult);
                    } else
                    {
                        LocalMessage.Msg6 += FileDoResult.GetType().ToString();
                    }
                    LocalMessage.Msg6 += ".";
                    LocalMessage.Msg6 += "\n" + "Failed to " + FmainPassed.FileAction.Name
                        + ", in mode: " + FmainPassed.FileAction.ModeName
                        + ", for direction: " + FmainPassed.FileAction.DirectionName + ".";
                    LocalMessage.Msg7 = "Calling String: " + PickState.PickSystemCallStringResult;
                    LocalMessage.Msg8 = "\n" + "Activity Key: " + FmainPassed.FileAction.KeyName;
                    LocalMessage.Msg6 += "\n" + LocalMessage.Msg7 + "\n" + LocalMessage.Msg8;
                    LocalMessage.ErrorMsg = LocalMessage.Msg6;
                    // PrintOutputMdm_PickPrint(Sender, 3, LocalMessage.Msg6, bYES);
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 6, ref Sender, bIsMessage,
                        TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(),
                        FileDoResult, RunErrorDidOccur = true,
                        iNoErrorLevel, iNoErrorSource,
                        bDoNotDisplay, MessageNoUserEntry,
                        LocalMessage.Msg6);
                    #endregion
                } // end of skip null
                #endregion
                #region Catch General Errors
            }
            catch (SqlException ExceptionSql)
            {
                LocalMessage.ErrorMsg = "SQL Exception(#8) occured in File Action";
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileDoResult);
                FileDoResult = StateIs.DatabaseError;
                FmainPassed.FileStatus.FileErrorCurrent = FileIo_ErrorIs.SqlError;
            }
            catch (NotSupportedException ExceptionNotSupported)
            {
                LocalMessage.ErrorMsg += "Not Supported Exception(#9) occured in File Action";
                ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileDoResult);
                FileDoResult = StateIs.Failed;
                FmainPassed.FileStatus.Status = StateIs.ProgramInvalid;
            }
            catch (IOException ExceptionIO)
            {
                LocalMessage.ErrorMsg = "IO Exception(#10) occured in File Action";
                ExceptIoImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionIO, FileDoResult);
                FileDoResult = StateIs.Failed;
                FmainPassed.FileStatus.FileErrorCurrent = FileIo_ErrorIs.IoError;
            }
            catch (Exception ExceptionGeneral)
            {
                LocalMessage.ErrorMsg = "General Unhandled Exception(#11) occured in File Action";
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileDoResult);
                FileDoResult = StateIs.UnknownFailure;
                FmainPassed.FileStatus.Status = StateIs.UnknownFailure;
            }
            finally
            {
                if (FileDoResult == StateIs.InProgress)
                {
                    FileDoResult = StateIs.Failed;
                    FmainPassed.FileStatus.FileErrorCurrent = FileIo_ErrorIs.NotCompleted;
                    LocalMessage.Msg6 = "Operation did not complete! Exception Error! ";
                    // PrintOutputMdm_PickPrint(Sender, 3, LocalMessage.Msg6, bYES);
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 6, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), FileDoResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.Msg6);
                    // ToDo poosible Throw here
                }
                #endregion
            } // of try connect
            //
            if (FmainPassed.IoState.DoCallback)
            {
                FmainPassed.IoState.ClearKey();
                FmainPassed.IoState.CategoryKey = FmainPassed.FileAction.KeyName;
                FmainPassed.IoState.ActionKey = FmainPassed.FileAction.Name;
                FmainPassed.IoState.IoDoCallback(FileDoResult, FmainPassed.FileAction.ResultObject);
            }
            return FileDoResult;
        }
        #endregion
    }
}
