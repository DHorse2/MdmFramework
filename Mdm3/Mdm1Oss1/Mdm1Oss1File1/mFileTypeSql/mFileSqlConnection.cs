#region Dependencies
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

#region  Mdm Core
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.File.Control;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Components;
using Mdm.World;
#endregion
#region  Mdm Db and File
using Mdm.Oss.File;
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
#endregion

namespace Mdm.Oss.File.Type.Sql
{
    public partial class mFileSqlConnectionDef : mFileSql, ImFileType, AppStd, iDbTask, IDisposable
    {
        #region Core Fields
        // Core Objects - Mapplication
        #region $include Mdm.Oss.FileUtil mFileDef FileBasicInformation
        // Command and Phrase Construction
        public DbSynDef DbSyn;
        // Database Master and Master Phrases
        public DbMasterSynDef DbMasterSyn;
        #endregion
        #endregion
        #region Framework Objects
        #endregion
        #region Class Factory
        #endregion
        #region Class
        #region Class Initialization
        // Constructors
        #region Constructor
        public mFileSqlConnectionDef(ref object SenderPassed, ref StdConsoleManagerDef stPassed, ConsoleSourceIs SenderObjectSource, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ref SenderPassed, ref stPassed, SenderObjectSource, ClassRolePassed, ClassFeaturesPassed)
        {
            //st = base.st as StdConsoleManagerDef;
            FileState.mFileResult = StateIs.Started;
            // mFileResult
            Sender = this;
            FileObject = this;
            FileSqlConn = this;
            this.Initialize();
            MainThreadId = -1;
            RefreshIsBusy = false;
            DataInsertSkipRowEnter = false;
            DataInsertSkipRowEnterArgs = null;
        }

        public mFileSqlConnectionDef()
                 : base(ConsoleSourceIs.None, ClassRoleIs.RoleAsUtility, ClassFeatureIs.MdmUtilTrace)
        {
            // : base() {
            Sender = this;
            FileObject = this;
            FileSqlConn = this;
            Fmain.FileStatus.bpIsInitialized = false;
            //
            this.Initialize();
        }
        public new void Initialize()
        {
            if (!ClassFeatureFlag.InitializeFileSqlConn)
            {
                ClassFeatureFlag.InitializeFileSqlConn = true;
                base.Initialize();
                InitializeFileSql();
            }
            Status = StateIs.Initialized;
        }

        public void InitializeFileSql()
        {
            ClassFeatureFlag.InitializeFileSqlConn = true;
            Fmain.DbMaster.MstrDbDatabaseIsInitialized = false;
            Sender = this;
            FileSqlConn = this;
            Fmain.FileSqlConn = this;

            #region Phrase Construction
            DbSyn = new DbSynDef();
            DbMasterSyn = new DbMasterSynDef();
            #endregion
            FileState.mFileResult = DatabaseReset(ref Fmain);
            //
            FileState.mFileResult = ConnReset(ref Fmain);
        }
        #endregion
        // Initializers
        // Delegates
        #endregion
        #region Class Fields
        #endregion
        #region Class Methods
        // Database Connection - xxxxxxxxxxxxxxxxxxxxxxxxx
        #region Conn
        #region Close Connection
        // <Section Id = "x
        /// <summary> 
        /// Close the Connection.
        /// </summary> 
        public StateIs ConnClose(ref mFileMainDef FmainPassed)
        {
            FileState.ConnCloseResult = StateIs.Started;
            // Do not arrive here without checking KeepOpen....
            switch (FmainPassed.Fs.FileIo.IoMode)
            {
                case (FileIo_ModeIs.Sql):
                    if (FmainPassed.ConnStatus.bpIsConnected)
                    {
                        // <Area Id = "WARNING - Already disconnected">
                        FileState.ConnCloseResult = StateIs.Successful;
                    }
                    else
                    {
                        // <Area Id = "Connect">
                        FileState.ConnCloseResult = StateIs.InProgress;
                        try
                        {
                            // <Area Id = "close connection
                            if (FmainPassed.ConnStatus.bpIsOpen)
                            {
                                FmainPassed.DbIo.SqlDbConn.Close();
                            }
                            // <Area Id = "dispose of connection
                            if (FmainPassed.ConnStatus.bpIsCreated || FmainPassed.DbIo.SqlDbConn != null)
                            {
                                FmainPassed.DbIo.SqlDbConn.Dispose();
                            }
                            FileState.ConnCloseResult = StateIs.Successful;
                            FmainPassed.ConnStatus.bpIsConnected = false;
                        }
                        catch (SqlException ExceptionSql)
                        {
                            FmainPassed.ConnStatus.bpIsConnected = false;
                            LocalMessage.ErrorMsg = sEmpty;
                            ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.ConnCloseResult);
                            ExceptTableCloseImpl(ref FmainPassed, ref ExceptionSql);
                            FileState.ConnCloseResult = StateIs.DatabaseError;
                        }
                        catch (Exception ExceptionGeneral)
                        {
                            FmainPassed.ConnStatus.bpIsConnected = false;
                            LocalMessage.ErrorMsg = sEmpty;
                            ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.ConnCloseResult);
                            ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                            FileState.ConnCloseResult = StateIs.OsError;
                        }
                        finally
                        {
                            if (!FmainPassed.ConnStatus.bpIsConnected)
                            {
                                FmainPassed.ConnStatus.bpIsOpen = false;
                                FmainPassed.ConnStatus.bpIsCreated = false;
                                FmainPassed.ConnStatus.bpIsConnected = false;
                                FmainPassed.ConnStatus.bpIsConnecting = false;
                                FmainPassed.ConnStatus.bpNameIsValid = false;
                                //
                                FmainPassed.DbIo.DataClear();
                                ((mFileDef)FmainPassed.Sender).FileState.ConnDoReset = true;
                            }
                            ObjectListClearData();
                        } // of try disconnect
                    } // is already connected
                    FileState.ConnCloseResult = StateIs.Successful;
                    break;
                case (FileIo_ModeIs.Block):
                case (FileIo_ModeIs.Line):
                case (FileIo_ModeIs.All):
                    FmainPassed.ConnStatus.bpIsOpen = false;
                    FmainPassed.ConnStatus.bpIsCreated = false;
                    FmainPassed.ConnStatus.bpIsConnected = false;
                    FmainPassed.ConnStatus.bpIsConnecting = false;
                    FmainPassed.ConnStatus.NameIsValid = false;
                    ((mFileDef)FmainPassed.Sender).FileState.ConnDoReset = true;
                    FileState.ConnCloseResult = StateIs.Successful;
                    // Fmain.FileStatus.bpDoesExist = System.IO.File.Exists(Fs.FileId.FileName);
                    // ((mFileDef)FmainPassed.Sender).State.ConnCloseResult = StateIs.Invalid;
                    break;
                default:
                    FileState.ConnCloseResult = StateIs.NotSet;
                    LocalMessage.ErrorMsg = "File Read IoType (" + FmainPassed.Fs.FileIo.IoMode.ToString() + ") is not set";
                    //throw new NotSupportedException(LocalMessage.Msg);
                    ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileDoResult);
                    break;
            }

            return FileState.ConnCloseResult;
        }
        #endregion
        #region Open Connection
        // <Section Id = "x
        /// <summary> 
        /// Open the Connection.
        /// </summary> 
        public StateIs ConnOpen()
        {
            FileState.ConnOpenResult = StateIs.Started;

            FileState.ConnOpenResult = ConnOpen(ref Fmain);

            return FileState.ConnOpenResult;
        }

        // <Section Id = "x
        /// <summary> 
        /// Open the Passed Connection.
        /// </summary> 
        public StateIs ConnOpen(ref mFileMainDef FmainPassed)
        {
            FileState.ConnOpenResult = StateIs.Started;
            // Fmain.FileStatus.bpIsInitialized
            // if (ConnStatus.bpIsInitialized) {
            // State.ConnOpenResult = ConnReset();
            // }
            if (!FmainPassed.ConnStatus.bpIsCreated
                || !FmainPassed.ConnStatus.bpNameIsValid
                || ((mFileDef)FmainPassed.Sender).FileState.ConnDoReset
                || FmainPassed.DbIo.SqlDbConn == null
                || FmainPassed.DbIo.spConnString.Length == 0
                )
            {
                // close connection
                if (FmainPassed.DbIo.SqlDbConn != null)
                {
                    try
                    {
                        if (FmainPassed.DbIo.SqlDbConn != null)
                        {
                            if (FmainPassed.DbIo.SqlDbConn.State == ConnectionState.Open)
                            {
                                FileState.ConnOpenResult = ConnClose(ref FmainPassed);
                            }
                            if (FmainPassed.DbIo.SqlDbConn.State != ConnectionState.Open)
                            {
                                FileState.ConnOpenResult = ConnReset(ref FmainPassed);
                            }
                        }
                    }
                    catch {; }
                }
                // create connection
                FileState.ConnOpenResult = ConnCreate(ref FmainPassed);
                //
            }
            else if (FmainPassed.DbIo.SqlDbConn != null)
            {
                if (FmainPassed.ConnStatus.bpIsConnected
                    && FmainPassed.DbIo.SqlDbConn.State == ConnectionState.Open
                    // && FmainPassed.ConnStatus.IsOpen
                    )
                {
                    // <Area Id = "WARNING - Already connected">
                    return (FileState.ConnOpenResult = StateIs.Successful);
                }
            }
            FmainPassed.ConnStatus.bpIsInitialized = true;
            // ((mFileDef)FmainPassed.Sender).State.ConnOpenResult = StateIs.InProgress;
            // <Area Id = "CheckDatabaseDoesExist">
            switch (FmainPassed.Fs.FileIo.IoMode)
            {
                case (FileIo_ModeIs.Sql):
                    if (!FmainPassed.ConnStatus.bpIsCreated
                        || !FmainPassed.ConnStatus.bpDoesExist
                        || FmainPassed.DbIo.SqlDbConn == null)
                    {
                        //
                        FileState.ConnOpenResult = ConnCheckDoesExist(ref FmainPassed);
                    }
                    break;
                case (FileIo_ModeIs.Block):
                case (FileIo_ModeIs.Line):
                case (FileIo_ModeIs.All):
                    FileState.ConnOpenResult = StateIs.InProgress;
                    FmainPassed.ConnStatus.ipDoesExistResult = StateIs.DoesExist;
                    FmainPassed.ConnStatus.bpDoesExist = true;
                    Fmain.FileStatus.bpDoesExist = System.IO.File.Exists(FmainPassed.Fs.TableNameLine);
                    if (Fmain.FileStatus.bpDoesExist)
                    {
                        FileState.ConnOpenResult = StateIs.DoesExist;
                        FmainPassed.ConnStatus.bpIsOpen = true;
                        FmainPassed.ConnStatus.bpIsConnected = true;
                        FmainPassed.ConnStatus.bpIsConnecting = false;
                        break;
                    }
                    else
                    {
                        FileState.ConnOpenResult = StateIs.DoesNotExist;
                        FmainPassed.ConnStatus.bpIsOpen = false;
                        FmainPassed.ConnStatus.bpIsConnected = false;
                        FmainPassed.ConnStatus.bpIsConnecting = false;
                        return FileState.ConnOpenResult;
                    }
                default:
                    FileState.ConnOpenResult = StateIs.ProgramInvalid;
                    LocalMessage.ErrorMsg = "File Read IoType (" + FmainPassed.Fs.FileIo.IoMode.ToString() + ") is not set";
                    throw new NotSupportedException(LocalMessage.Msg);
                    // ExceptNotSupportedImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionNotSupported, FileDoResult);
                    break;
            }
            if (!FmainPassed.ConnStatus.bpIsConnected && FmainPassed.DbIo.SqlDbConn != null)
            {
                // <Area Id = "not Connected">
                FmainPassed.ConnStatus.bpIsConnecting = true;
                while (!FmainPassed.ConnStatus.bpIsConnected
                    && FmainPassed.ConnStatus.bpIsConnecting
                    && FmainPassed.ConnStatus.bpDoesExist)
                {
                    // <Area Id = "Connect">
                    FileState.ConnOpenResult = FmainPassed.ConnStatus.ipIsConnectingResult = StateIs.InProgress;
                    try
                    {
                        if (!FmainPassed.ConnStatus.bpDoesExist) { FileState.ConnOpenResult = ConnCreate(); }
                        if (FileState.ConnOpenResult == StateIs.InProgress || FileState.ConnOpenResult == StateIs.Successful)
                        {
                            switch (FmainPassed.Fs.FileIo.IoMode)
                            {
                                case (FileIo_ModeIs.Sql):
                                    // Open Database Connection
                                    // FmainPassed.ConnStatus.ipIsConnectingResult = (int) StateIs.InProgress;
                                    FileState.ConnOpenResult = StateIs.InProgress;
                                    Sys.sMessageBoxMessage = StdProcess.Title + "\n" + "SQL Database Connection:" + FmainPassed.Fs.DatabaseName;
                                    FmainPassed.DbIo.SqlDbConn.Open();
                                    FileState.ConnOpenResult = StateIs.Successful;
                                    FmainPassed.ConnStatus.bpIsOpen = true;
                                    FmainPassed.ConnStatus.bpIsConnecting = false;
                                    FmainPassed.ConnStatus.bpIsConnected = true;
                                    FmainPassed.ConnStatus.NameIsValid = true;
                                    break;
                                case (FileIo_ModeIs.Block):
                                case (FileIo_ModeIs.Line):
                                case (FileIo_ModeIs.All):

                                    // Check Disk access
                                    // Check Folder exists
                                    // Check Other Disk criteria
                                    // Check other File criteria
                                    FileState.ConnOpenResult = StateIs.Successful;
                                    FmainPassed.ConnStatus.bpIsOpen = true;
                                    FmainPassed.ConnStatus.bpIsConnected = true;
                                    FmainPassed.ConnStatus.bpIsConnecting = false;
                                    FmainPassed.ConnStatus.NameIsValid = true;
                                    break;
                                default:
                                    LocalMessage.ErrorMsg = "File Read IoType (" + FmainPassed.Fs.FileIo.IoMode.ToString() + ") is not set";
                                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 9, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickState.PickSystemCallStringResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                                    FileState.ConnOpenResult = StateIs.ProgramInvalid;
                                    throw new NotSupportedException(LocalMessage.Msg);
                                    // ExceptNotSupportedImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionNotSupported, FileDoResult);
                            }
                            // State.ConnOpenResult = (int) StateIs.Successful;
                            // FmainPassed.ConnStatus.bpIsConnected = true;
                            // FmainPassed.ConnStatus.bpIsConnecting = false;
                            // DbIo.SqlDbConn.Close();
                        }
                        else
                        {
                            LocalMessage.ErrorMsg = "SQL Database connection error on database: " + FmainPassed.Fs.DatabaseName;
                           st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 9, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickState.PickSystemCallStringResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                            FileState.ConnOpenResult = StateIs.Failed;
                            FmainPassed.ConnStatus.bpIsConnecting = false;
                            FmainPassed.ConnStatus.bpIsConnected = false;
                            FmainPassed.ConnStatus.NameIsValid = false;
                        }
                        // exceptions:
                    }
                    catch (NotSupportedException ExceptionNotSupported)
                    {
                        FileState.ConnOpenResult = StateIs.Failed;
                        FmainPassed.ConnStatus.bpIsConnecting = false;
                        FmainPassed.ConnStatus.bpIsConnected = false;
                        FmainPassed.ConnStatus.NameIsValid = false;
                        LocalMessage.ErrorMsg += "Not Supported Exception(#415) occured in File Action";
                        ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileDoResult);
                    }
                    catch (SqlException ExceptionSql)
                    {
                        LocalMessage.ErrorMsg = sEmpty;
                        ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.ConnOpenResult);
                        //
                        ExceptTableOpenError(ref FmainPassed, ref ExceptionSql);
                        FileState.ConnOpenResult = StateIs.DatabaseError;


                        FmainPassed.ConnStatus.bpIsConnecting = false;
                        FmainPassed.ConnStatus.bpIsConnected = false;
                        FmainPassed.ConnStatus.NameIsValid = false;
                    }
                    catch (Exception ExceptionGeneral)
                    {
                        LocalMessage.ErrorMsg = sEmpty;
                        ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.ConnOpenResult);
                        FileState.ConnOpenResult = StateIs.Failed;
                        FileState.ConnCloseResult = StateIs.OsError;
                        ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                        FmainPassed.ConnStatus.bpIsConnecting = false;
                        FmainPassed.ConnStatus.bpIsConnected = false;
                        FmainPassed.ConnStatus.NameIsValid = false;
                    }
                    finally
                    {
                        if (FileState.ConnOpenResult == StateIs.InProgress)
                        {
                            FileState.ConnOpenResult = StateIs.Failed;
                        }
                        FmainPassed.ConnStatus.ipIsConnectingResult = FileState.ConnOpenResult;
                        FmainPassed.ConnStatus.bpIsOpen = FmainPassed.ConnStatus.bpIsConnected;
                    } // of try connect
                } // of is connecting
            }
            else
            {
                FileState.ConnOpenResult = StateIs.Successful;
                FmainPassed.ConnStatus.bpIsOpen = false;
                FmainPassed.ConnStatus.bpIsConnecting = false;
                FmainPassed.ConnStatus.bpIsConnected = true;
                FmainPassed.ConnStatus.NameIsValid = false;
                FmainPassed.ConnStatus.bpIsCreating = false;
                FmainPassed.ConnStatus.bpIsCreated = true;
                FmainPassed.ConnStatus.ipIsConnectingResult = FileState.ConnOpenResult;
            }
            ((mFileDef)FmainPassed.Sender).FileState.ConnDoReset = false;
            FmainPassed.ConnStatus.IsOpenResult = FileState.ConnOpenResult;
            return FileState.ConnOpenResult;
        }
        #endregion
        #region Connection Reset, Check
        // <Section Id = "x
        /// <summary> 
        /// Reset the Connection.
        /// </summary> 
        public StateIs ConnReset(ref mFileMainDef FmainPassed)
        {
            FileState.ConnResetResult = StateIs.Started;
            // Fmain.FileStatus.bpIsInitialized
            // if (FmainPassed.ConnStatus.bpIsInitialized) {
            FmainPassed.FileSqlConn.DbSyn.spConnCreateCmd = sEmpty;
            FmainPassed.FileSqlConn.DbMasterSyn.spMstrDbFileCreateCmd = sEmpty;
            // <Area Id = "DbConnObjects">
            FmainPassed.DbIo.DataClear();
            // Status
            FmainPassed.ConnStatus.ipDoesExistResult = 0;
            FmainPassed.ConnStatus.bpDoesExist = false;
            FmainPassed.ConnStatus.bpIsValid = false;
            FmainPassed.ConnStatus.ipIsConnectingResult = 0;
            FmainPassed.ConnStatus.bpIsConnecting = false;
            FmainPassed.ConnStatus.bpIsConnected = false;
            FmainPassed.ConnStatus.bpIsOpen = false;
            FmainPassed.ConnStatus.bpIsCreating = false;
            FmainPassed.ConnStatus.bpIsCreated = false;
            FmainPassed.ConnStatus.bpIsInitialized = false;
            // Execeptions
            DbFileCmdOsException = null;
            //
            ((mFileDef)FmainPassed.Sender).FileState.ConnDoReset = false;
            //
            ObjectListClearData();
            //
            Sys.sMformStatusMessage = sEmpty;
            Sys.sMessageBoxMessage = sEmpty;
            //
            return FileState.ConnResetResult;
        }
        // <<Section Id = "x
        // This function checks the state of the connection (not flags)
        /// <summary> 
        /// Check the Connection.
        /// </summary> 
        public StateIs ConnCheck(ref mFileMainDef FmainPassed)
        {
            FileState.ConnCheckDoesExistResult = StateIs.Started;

            if (FmainPassed.DbIo.SqlDbConn != null
                && !(FmainPassed.DbIo.SqlDbConn.Database == FmainPassed.Fs.DatabaseName
                && FmainPassed.DbIo.SqlDbConn.DataSource == FmainPassed.Fs.ServerName)
                )
            {
                try
                {
                    if (FmainPassed.DbIo.SqlDbConn != null)
                    {
                        if (FmainPassed.DbIo.SqlDbConn.State.ToString() == "Open" && ((mFileDef)FmainPassed.Sender).FileState.ConnDoReset)
                        {
                            ((mFileDef)FmainPassed.Sender).FileState.ConnDoReset = true;
                            FmainPassed.DbIo.ConnString = sEmpty;
                            //} else if (FmainPassed.DbIo.SqlDbConn.State.ToString() != "Open" && FmainPassed.Fs.ConnDoReset) {
                            //    SqlColAddCmdBuildResult = ConnReset(ref FmainPassed);
                        }
                    }
                }
                catch {; }
            }
            //
            if (FmainPassed.Fs.ServerName.Length == 0
                || FmainPassed.Fs.DatabaseName.Length == 0
                || FmainPassed.Fs.TableNameLine.Length == 0
                || !FmainPassed.FileStatus.bpNameIsValid
                )
            {
                FmainPassed.Fs.TableNameLine = TableNameLineBuild(ref FmainPassed);
            }
            //
            if (FmainPassed.DbIo.SqlDbConn == null
                || (!(FmainPassed.DbIo.SqlDbConn.Database == FmainPassed.Fs.DatabaseName
                && FmainPassed.DbIo.SqlDbConn.DataSource == FmainPassed.Fs.ServerName)
                || FmainPassed.DbIo.SqlDbConn.State != ConnectionState.Open)
                )
            {
                if (!(FmainPassed.Fs.DatabaseName.Length == 0
                || FmainPassed.Fs.ServerName.Length == 0))
                {
                    FileState.ConnCheckDoesExistResult = ConnOpen(ref FmainPassed);
                }
                else { FileState.ConnCheckDoesExistResult = StateIs.Failed; }
            }
            else { FileState.ConnCheckDoesExistResult = StateIs.Successful; }
            return FileState.ConnCheckDoesExistResult;
        }

        // This function performs a lookup / search for the database (connection)
        /// <summary> 
        /// Check if the Connection Exists.
        /// </summary> 
        public StateIs ConnCheckDoesExist(ref mFileMainDef FmainPassed)
        {
            FileState.ConnCheckDoesExistResult = StateIs.Started;
            if (FmainPassed.DbIo.SqlDbConn.State.ToString() == "Open")
            {
                FmainPassed.ConnStatus.bpDoesExist = true;
                FileState.ConnCheckDoesExistResult = StateIs.DoesExist;
            }
            else
            {
                FmainPassed.ConnStatus.bpDoesExist = false;
                FileState.ConnCheckDoesExistResult = StateIs.DoesNotExist;
            }
            FmainPassed.ConnStatus.ipDoesExistResult = FileState.ConnCheckDoesExistResult;
            return FileState.ConnCheckDoesExistResult;
        }
        #endregion
        #region ConnCreate
        // <Section Id = "SQL File Handling
        /// <summary> 
        /// Build the Connection Command.
        /// </summary> 
        public StateIs ConnCmdBuild(ref mFileMainDef FmainPassed)
        {
            FileState.ConnCmdBuildResult = StateIs.Started;
            //
            // Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=F:\DEV\DATA\MDMDATA99\CLIPDATA\MDMDATA99CLIPDATAVS1\MDMCLIPBOARDDATA.MDF;
            // Integrated Security=True;Connect Timeout=15;
            // Encrypt =False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
            //
            FmainPassed.ConnStatus.bpNameIsValid = true;
            String sProvider = sEmpty;
            const int PROVIDERDOTNET = 1;
            const int PROVIDEROLEDB = 2;
            const int PROVIDERODBC = 3;
            //
            int iProvider = PROVIDERDOTNET;
            const String PROVIDERSQLNCLI = "SQLNCLI";
            const String PROVIDERNATIVE_CLIENT = "{SQL Native Client}";
            //
            const int PROVIDERLOCAL = 1;
            const int PROVIDERREMOTE = 2;
            //
            // ToDo no abstration for Web
            int iProviderLocation = PROVIDERLOCAL;
            String sProviderIp = sEmpty;
            String sProviderPort = sEmpty;

            String sDatabase = sEmpty;
            String sDbPathDirectory = sEmpty;
            String sDbPathFileName = sEmpty;

            String User = sEmpty;
            bool UseUser = false;
            // bool UseUser = true;
            const int UserEntered = 2;
            const int UserPROMPT = 3;
            //
            int iUserSource = UserEntered;
            //
            String ServerUserName = sEmpty;
            String ServerUserPassword = sEmpty;

            String Security = sEmpty;
            const int CONN_TRUSTED = 1;
            const int CONN_INTEGRATED = 2;
            //
            int iUseTrustedConnection = CONN_INTEGRATED;

            bool UseServer = true;
            bool UseDatabasePath = false;
            bool UseDataSource = false;
            bool UseDatabaseName = false;
            bool UseCatalog = false;

            FmainPassed.DbIo.spConnString = sEmpty;
            IterationLoopCounter += 1;

            if (FileState.DoingDefaults)
            {
                UseDatabaseName = true;
            }
            else
            {
                UseDatabaseName = true;
                //  tried UseDatabasePath = true; 
            }

            if (iUserSource == UserPROMPT)
            {
                // oConn.Properties("Prompt") = adPromptAlways
                // ConnCmdBuildResult = ConnCmdUserPrompt();
                UseUser = true;
            }
            if (UseUser)
            {
                // Not integrates with handler or
                // or page entry of user.
                // Mapplication lacks user info.
                User += "User ID=";
                User += ServerUserName;
                User += "; Password =";
                User += ServerUserPassword;
            }

            // Provider:
            sProvider = sEmpty; // This might be wrong.
            if (iProvider == PROVIDERDOTNET)
            {
                //
            }
            else if (iProvider == PROVIDEROLEDB)
            {
                // Provider=SQLNCLI;
                sProvider += "Provider=";
                sProvider += PROVIDERSQLNCLI;
            }
            else if (iProvider == PROVIDERODBC)
            {
                // Driver={SQL Native Client};
                sProvider += "Driver=";
                sProvider += PROVIDERNATIVE_CLIENT;
            }
            else if (iProviderLocation == PROVIDERREMOTE)
            {
                sProvider = sEmpty;
                // bool UseDataSourceAddress = false;
                UseServer = false;
                sProvider += "ItemData Source=";
                // DbIo.spConnString += "190.190.200.100,1433"
                sProvider += sProviderIp;
                sProvider += ",";
                sProvider += sProviderPort;
                sProvider += ";";
                //
                // bool UseNetworkLibrary = false;
                sProvider += "Network Library=";
                sProvider += "DBMSSOCN";
                // Initial Catalog=myDataBase;
                UseCatalog = true;
                UseDatabaseName = false;
            }

            if (sProvider.Length > 0)
            {
                FmainPassed.DbIo.spConnString += sProvider;
                FmainPassed.DbIo.spConnString += ";";
            }

            // 2015 vs HACK off
            // UseDatabaseName = false; UseCatalog = false;
            // 2020 HACK off being debugged re defaults
            // UseDatabaseName = false; UseDatabasePath = false;  UseCatalog = false;
            //
            // Server:
            if (UseServer)
            {
                if (FmainPassed.Fs.ServerName.Length == 0) { FmainPassed.ConnStatus.bpNameIsValid = false; }
                // HACK 
                // FmainPassed.DbIo.spConnString += "Data Source=";
                FmainPassed.DbIo.spConnString += "Server=";
                // DbIo.spConnString += FmainPassed.SystemName + @"\" + FmainPassed.ServiceName;
                FmainPassed.DbIo.spConnString += FmainPassed.Fs.ServerName;
                // DbIo.spConnString += "localhost";
                FmainPassed.DbIo.spConnString += ";";
            }
            // Database:
            if (UseDatabasePath)
            {
                // AttachDbFilename=|DataDirectory|mydbfile.mdf;
                //sDatabase += sDbPathDirectory;
                //sDatabase += sDbPathFileName;

                // HACK off
                // sDatabase += "Database=";
                sDatabase += "AttachDbFilename=";

                if (FmainPassed.Fs.DatabaseName.Length == 0)
                {
                    FmainPassed.ConnStatus.bpNameIsValid = false;
                    FmainPassed.FileStatus.bpNameIsValid = false;
                }
                sDatabase += FmainPassed.Fs.FileId.FileDriveLetter + ":" + FmainPassed.Fs.FileId.PathName + BackSlash;
                sDatabase += FmainPassed.Fs.DatabaseName;
                sDatabase += ".mdf";
            }
            else if (UseDatabaseName)
            {
                sDatabase += "Database=";
                if (FmainPassed.Fs.DatabaseName.Length == 0)
                {
                    FmainPassed.ConnStatus.bpNameIsValid = false;
                    FmainPassed.FileStatus.bpNameIsValid = false;
                }
                sDatabase += FmainPassed.Fs.DatabaseName;
            }
            else if (UseCatalog)
            {
                sDatabase += "Initial Catalog=";
                if (FmainPassed.Fs.DatabaseName.Length == 0)
                {
                    FmainPassed.ConnStatus.bpNameIsValid = false;
                    FmainPassed.FileStatus.bpNameIsValid = false;
                }
                sDatabase += FmainPassed.Fs.DatabaseName;
            }
            else
            {
                // Default 
                sDatabase += "Initial Catalog=";
                if (FmainPassed.Fs.DatabaseName.Length == 0)
                {
                    FmainPassed.ConnStatus.bpNameIsValid = false;
                    FmainPassed.FileStatus.bpNameIsValid = false;
                }
                sDatabase += FmainPassed.Fs.FileId.FileDriveLetter + ":" + FmainPassed.Fs.FileId.PathName + BackSlash;
                sDatabase += FmainPassed.Fs.DatabaseName;
                sDatabase += ".mdf";
            }
            FmainPassed.DbIo.spConnString += sDatabase;
            FmainPassed.DbIo.spConnString += ";";

            // Security:
            // HACK off
            // See: https://docs.microsoft.com/en-za/sql/relational-databases/errors-events/mssqlserver-18456-database-engine-error?view=sql-server-ver15
            // It wasn't actual implemented... hmmm.
            FmainPassed.DbMaster.UseSSPI = false;

            if (!FmainPassed.DbMaster.UseSSPI)
            {
                Security += "Trusted_Connection=";
                Security += "True";
                // OR Security += "Yes";
            }
            else
            {
                Security += "Integrated Security=";
                Security += "True";
                // OR Security += "SSPI";
            }
            //
            FmainPassed.DbIo.spConnString += Security;
            FmainPassed.DbIo.spConnString += ";";

            if (UseUser)
            {
                FmainPassed.DbIo.spConnString += User;
                FmainPassed.DbIo.spConnString += ";";
                // User ID=myUsername;Password=myPassword;
                // Uid=myUsername; Pwd=myPassword;
            }

            // HACK off
            //User += "User ID=";
            //User += @"RAVEN\david";
            //User += "; Password=";
            //User += "L@$41aj235";
            //User += ";";
            //FmainPassed.DbIo.spConnString += User;
            //
            // Trusted_Connection=True;
            // Found locally with:
            // FmainPassed.DbIo.spConnString += "Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //
            // FmainPassed.DbIo.spConnString += "User Instance=True;";
            // Is not allowed.
            //
            // May have to raise for slow PCs:
            FmainPassed.DbIo.spConnString += "Connect Timeout=60;";
            // Possible attributes:
            // FmainPassed.DbIo.spConnString += "Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            // HACK off
            // FmainPassed.DbIo.spConnString = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=F:\\Dev\\Data\\MdmData99\\ShortcutData\\Vs1\\MdmShortcutData.mdf;Trusted_Connection=True;User ID=RAVEN\\david; Password=L@$41aj235;"
            // FmainPassed.DbIo.spConnString = "Server=(localdb)\\MSSQLLocalDB; Initial Catalog=F:\\Dev\\Data\\MdmData99\\ShortcutData\\Vs1\\MdmShortcutData.mdf;";
            // FmainPassed.DbIo.spConnString += " User ID=RAVEN\\david; Password=L@$41aj235;";
            //
            // .NET Framework ItemData Provider for SQL Server
            // ItemData Source=myServerAddress;
            // Initial Catalog=myDataBase;
            //
            // Integrated Security=SSPI;
            // or
            // Trusted_Connection=True;
            //
            // User ID=myDomain\myUsername;
            // Password=myPassword;
            // 
            // ItemData Source=190.190.200.100,1433;
            // Network Library=DBMSSOCN;
            // Initial Catalog=myDataBase;
            // User ID=myUsername;Password=myPassword;
            //
            // Server=.\SQLExpress;
            // AttachDbFilename=|DataDirectory|mydbfile.mdf; 
            // Database=dbname;
            // Trusted_Connection=Yes;
            // 
            // ItemData Source=.\SQLExpress;
            // Integrated Security=true; 
            // AttachDbFilename=|DataDirectory|\mydb.mdf;
            // User Instance=true;
            //
            // SQL Native Client OLE DB Provider
            // Provider=SQLNCLI;
            // Server=myServerAddress;Database=myDataBase;
            // Uid=myUsername; Pwd=myPassword;
            //
            // oConn.Properties("Prompt") = adPromptAlways
            // oConn.Open "Provider=SQLNCLI;
            // Server=myServerAddress;DataBase=myDataBase;
            // 
            // SQL Native Client ODBC Driver
            // Driver={SQL Native Client};
            // Server=myServerAddress;Database=myDataBase; 
            // Uid=myUsername;Pwd=myPassword;
            //
            // Driver={SQL Native Client};
            // Server=myServerAddress;Database=myDataBase; 
            // Trusted_Connection=yes;
            //
            // oConn.Properties("Prompt") = adPromptAlways
            // Driver={SQL Native Client};
            // Server=myServerAddress;Database=myDataBase;
            //
            // Driver={SQL Native Client};
            // Server=.\SQLExpress;
            // AttachDbFilename=c:\mydbfile.mdf; 
            // Database=dbname;
            // Trusted_Connection=Yes;
            //
            // Driver={SQL Native Client};
            // Server=.\SQLExpress; 
            // AttachDbFilename=|DataDirectory|mydbfile.mdf;
            // Database=dbname;
            // Trusted_Connection=Yes;
            //
            FileState.ConnCmdBuildResult = StateIs.Successful;
            return FileState.ConnCmdBuildResult;
        }
        // <Section Id = "ConnCreate">
        /// <summary> 
        /// Create a Connection.
        /// </summary> 
        public StateIs ConnCreate()
        {
            FileState.ConnCreateResult = StateIs.Started;
            FileState.ConnCreateResult = ConnCreate(ref Fmain);
            return FileState.ConnCreateResult;
        }
        // <Section Id = "ConnCreatePassedConn">
        /// <summary> 
        /// Create the Passed Connection.
        /// </summary> 
        public StateIs ConnCreate(ref mFileMainDef FmainPassed)
        {
            FileState.ConnCreatePassedConnResult = StateIs.Started;
            //
            // Current Database Conn Create 
            // and the Database Conn Open
            // are synonymns
            //
            if (FmainPassed.Fs.ServerName.Length == 0
                || FmainPassed.Fs.DatabaseName.Length == 0
                || FmainPassed.Fs.TableNameLine.Length == 0
                || !FmainPassed.FileStatus.bpNameIsValid)
            {
                FmainPassed.Fs.TableNameLine = TableNameLineBuild(ref FmainPassed);
                FmainPassed.DbIo.ConnString = sEmpty;
            }
            if (!FmainPassed.ConnStatus.bpIsCreated
                || !FmainPassed.ConnStatus.bpNameIsValid
                || ((mFileDef)FmainPassed.Sender).FileState.ConnDoReset
                || FmainPassed.DbIo.SqlDbConn == null
                || FmainPassed.DbIo.spConnString.Length == 0
                )
            {
                if (!FmainPassed.FileStatus.bpNameIsValid)
                {
                    if (FileState.DoingDefaults) { FileState.ConnCreatePassedConnResult = DatabaseFieldsGetDefault(ref FmainPassed); }
                }
                if (FmainPassed.DbIo.spConnString.Length == 0
                    || !FmainPassed.ConnStatus.bpNameIsValid
                    || !((mFileDef)FmainPassed.Sender).FileState.ConnDoReset)
                {
                    FileState.ConnCreatePassedConnResult = ConnCmdBuild(ref FmainPassed);
                }
            }
            if (DbSyn.spDatabaseFileCreateCmd.Length == 0
                && FmainPassed.FileStatus.bpIsCreating)
            {
                FileState.ConnCreatePassedConnResult = DatabaseCreateCmdBuild(ref FmainPassed);
            }
            if (DbSyn.spConnCreateCmd.Length == 0
                && FmainPassed.FileStatus.bpIsCreating)
            {
                FileState.ConnCreatePassedConnResult = ConnCreateCmdBuild();
            }
            if (FmainPassed.DbIo.SqlDbConn != null && FmainPassed.ConnStatus.bpIsCreated == true)
            {
                // <Area Id = "WARNING - Already Created">
                FileState.ConnCreatePassedConnResult = StateIs.Successful;
                FmainPassed.ConnStatus.ipIsConnectingResult = StateIs.Successful;
                return FileState.ConnCreatePassedConnResult;
            }
            else
            {
                // <Area Id = "not Created">
                FmainPassed.ConnStatus.ipIsConnectingResult = StateIs.InProgress;
                FileState.ConnCreatePassedConnResult = FmainPassed.ConnStatus.ipIsConnectingResult;
                FmainPassed.ConnStatus.bpIsCreating = true;
                while (FmainPassed.ConnStatus.bpIsCreating)
                {
                    // <Area Id = "Connect">
                    try
                    {
                        if (FmainPassed.DbIo.SqlDbConn == null)
                        {
                            FmainPassed.DbIo.SqlDbConn = new SqlConnection(FmainPassed.DbIo.spConnString);
                        }
                        FmainPassed.DbIo.SqlDbConn.ConnectionString = FmainPassed.DbIo.ConnString;
                        // FmainPassed.DbIo.SqlDbConn.Database = FmainPassed.Fs.DatabaseName;
                        // FmainPassed.DbIo.SqlDbConn.ConnectionTimeout = FmainPassed.DbIo.SqlDbCommandTimeout;
                        // FmainPassed.DbIo.SqlDbConn = PassedSqlDbConnection;
                        // FmainPassed.DbIo.SqlDbConn.Open();
                        // FmainPassed.DbIo.SqlDbConn.Close();
                        FmainPassed.ConnStatus.ipIsConnectingResult = StateIs.Successful;
                        FileState.ConnCreatePassedConnResult = StateIs.Successful;
                        FmainPassed.ConnStatus.bpDoesExist = true;
                        FmainPassed.ConnStatus.bpIsCreating = false;
                        FmainPassed.ConnStatus.bpIsCreated = true;
                        FmainPassed.ConnStatus.bpIsInitialized = true;
                        ((mFileDef)FmainPassed.Sender).FileState.ConnDoReset = false;
                    }
                    catch (SqlException ExceptionSql)
                    {
                        LocalMessage.ErrorMsg = sEmpty;
                        ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.ConnCloseResult);
                        FmainPassed.ConnStatus.ipIsConnectingResult = StateIs.DatabaseError;
                        FileState.ConnCreatePassedConnResult = StateIs.DatabaseError;
                        //
                        ExeceptConnCreateImpl(ref FmainPassed, ref ExceptionSql);
                        FmainPassed.ConnStatus.bpIsCreating = false;
                        FmainPassed.ConnStatus.bpIsCreated = false;
                    }
                    catch (Exception ExceptionGeneral)
                    {
                        LocalMessage.ErrorMsg = sEmpty;
                        ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.ConnCloseResult);
                        FmainPassed.ConnStatus.ipIsConnectingResult = StateIs.Failed;
                        FileState.ConnCreatePassedConnResult = StateIs.Failed;
                        ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                        FmainPassed.ConnStatus.bpIsCreating = false;
                        FmainPassed.ConnStatus.bpIsCreated = false;
                    }
                    finally
                    {
                        if (FmainPassed.ConnStatus.ipIsConnectingResult == StateIs.InProgress)
                        {
                            FileState.ConnCreatePassedConnResult = StateIs.Failed;
                        }
                        FmainPassed.ConnStatus.ipIsConnectingResult = FileState.ConnCreatePassedConnResult;
                        Fmain.FileStatus.Status = FileState.ConnCreatePassedConnResult;
                    } // of try connect
                } // of is Creating
            } // is already Created
            FmainPassed.ConnStatus.IsCreatingResult = FileState.ConnCreatePassedConnResult;
            return FileState.ConnCreatePassedConnResult;
        }

        #region Database Create Command Build
        // <Section Id = "DatabaseCreateCmdBuild">
        /// <summary> 
        /// Build the Create Command for the Database.
        /// </summary> 
        public StateIs DatabaseCreateCmdBuild(ref mFileMainDef FmainPassed)
        {
            FileState.DatabaseCreateCmdBuildResult = StateIs.Started;
            //
            // DbMasterSyn.bpDbFilePhraseUseIsUsed
            // DbMasterSyn.bpDbFilePhraseIfIsUsed
            // DbMasterSyn.bpDbFilePhraseSelectIsUsed
            // DbMasterSyn.bpDbFilePhraseFromIsUsed
            // DbMasterSyn.bpDbFilePhraseWhereIsUsed
            // DbMasterSyn.bpDbFilePhraseBeginIsUsed
            // DbMasterSyn.bpDbFilePhraseDropIsUsed
            // DbMasterSyn.bpDbFilePhraseCreateIsUsed
            // 


            DbMasterSyn.MstrDbDatabaseCreateCmd = sEmpty;
            if (DbMasterSyn.bpDbFilePhraseUseIsUsed)
            {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseUse;
                DbMasterSyn.MstrDbDatabaseCreateCmd += Fmain.DbMaster.spMstrDbFileDb;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseUseEnd;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
            }
            if (DbMasterSyn.bpDbFilePhraseIfIsUsed)
            {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseIf;
            }
            if (DbMasterSyn.bpDbFilePhraseSelectIsUsed)
            {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseSelect;
            }
            if (DbMasterSyn.bpDbFilePhraseFromIsUsed)
            {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseFrom;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseFromItems;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseFromEnd;
            }
            if (DbMasterSyn.bpDbFilePhraseWhereIsUsed)
            {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseWhere;
                // sb paired list of dict + value
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseWhereItems1;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseWhereAnd;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseWhereItems2;

                // Loop X = 2 to n 
                // on Where Items using When AND
                // DbMasterSyn.spMstrDbFilePhraseWhereItemsAnd + 
                // DbMasterSyn.spMstrDbFilePhraseWhereItemsId[X] 
                // DbMasterSyn.spMstrDbFilePhraseWhereItemsExpression[X] 
                // DbMasterSyn.spMstrDbFilePhraseWhereItemsValue[X] 
            }

            if (DbMasterSyn.bpDbFilePhraseIfIsUsed)
            {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseIfEnd;
            }

            if (DbMasterSyn.bpDbFilePhraseIfIsUsed || DbMasterSyn.bpDbFilePhraseWhereIsUsed)
            {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
            }

            if (DbMasterSyn.bpDbFilePhraseBeginIsUsed)
            {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseBegin;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
            }

            if (DbMasterSyn.bpDbFilePhraseDropIsUsed)
            {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseDrop;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseDropItems + " ";
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
            }

            if (DbMasterSyn.bpDbFilePhraseBeginIsUsed)
            {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseBeginEnd;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
            }

            if (DbMasterSyn.bpDbFilePhraseCreateIsUsed)
            {
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseCreate;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseCreateObject;
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseCreateTable + " ";

                // DbMasterSyn.spMstrDbFilePhraseDColumnId[X] 
                // DbMasterSyn.spMstrDbFilePhraseWhereItemsType[X]; 
                // DbMasterSyn.spMstrDbFilePhraseWhereItemsTypeHasLength[X];
                if (DbMasterSyn.bpDbFilePhraseCreateIsUsed)
                {
                    // + "("
                    // DbMasterSyn.spMstrDbFilePhraseWhereItemsTypeLength[X];
                    // + ")"
                }
                // + " "
                // DbMasterSyn.spMstrDbFilePhraseWhereItemsRange[X];
                // "NOT NULL "
                if (DbMasterSyn.bpDbFilePhraseCreateIsUsed)
                {
                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraint;
                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraintCol;
                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraintEnd;

                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraintType1;
                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraintType2;

                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraintColBegin;
                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraintColName;
                    DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbFilePhraseConstraintColEnd;
                    DbMasterSyn.MstrDbDatabaseCreateCmd += sEmpty;
                }


                // %%% XXX ;    
                // ToDo z$RelVs? Database Creation MstrDbDatabaseCreateCmd
                DbMasterSyn.MstrDbDatabaseCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
            }
            /*
            DbMasterSyn.MstrDbDatabaseCreateCmd = "IF EXISTS (" +
                "SELECT * " +
                "FROM master..sysdatabases " +
                "WHERE Name = 'HowToDemo')" + spMstrDbPhraseDoLine +
                "DROP DATABASE HowToDemo" + spMstrDbPhraseDoLine +
                "CREATE DATABASE HowToDemo";
             */
            return StateIs.NormalEnd;
        }
        // <Section Id = "x
        #endregion

        // <Section Id = "ConnCreateCmdBuild">
        /// <summary> 
        /// Build a Connection Create Command.
        /// </summary> 
        public StateIs ConnCreateCmdBuild()
        {
            FileState.ConnCreateCmdBuildResult = StateIs.Started;
            DbSyn.spConnCreateCmd = "Connection are dynamic";

            FileState.ConnCreateCmdBuildResult = StateIs.Successful;
            return FileState.ConnCreateCmdBuildResult;

            if (DbMasterSyn.bpDbPhraseIfIsUsed)
            {
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseIf;
            }
            if (DbMasterSyn.bpDbFilePhraseSelectIsUsed)
            {
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseSelect;
            }
            if (DbMasterSyn.bpDbPhraseFromIsUsed)
            {
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseFrom;
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseFromItems;
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseFromEnd;
            }
            if (DbMasterSyn.bpDbPhraseWhereIsUsed)
            {
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseWhere;
                // sb paired list of dict + value
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseWhereItems + " ";
            }
            if (DbMasterSyn.bpDbPhraseIfIsUsed)
            {
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseIfEnd;
            }
            if (DbMasterSyn.bpDbPhraseDropIsUsed)
            {
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseDrop;
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseDropItems + " ";
            }
            if (DbMasterSyn.bpDbPhraseCreateIsUsed)
            {
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseDoLine;
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseCreate;
                DbSyn.spConnCreateCmd += DbMasterSyn.spMstrDbPhraseCreateItems + " ";
            }
            /*
            DbSyn.spConnCreateCmd = "IF EXISTS (" +
                "SELECT * " +
                "FROM master..sysdatabases " +
                "WHERE Name = 'HowToDemo')" + spMstrDbPhraseDoLine +
                "DROP DATABASE HowToDemo" + spMstrDbPhraseDoLine +
                "CREATE DATABASE HowToDemo";
             // FROM INFORMATION_SCHEMA.TABLES
             */
            FileState.ConnCreateCmdBuildResult = StateIs.Successful;
            return FileState.ConnCreateCmdBuildResult;
        }
        #endregion
        #region ConnError
        // <Section Id = "x
        /// <summary> 
        /// Exception handling for Connetion Creation.
        /// </summary> 
        public void ExeceptConnCreateImpl(ref SqlException ExceptionSql)
        {
            ExeceptConnCreateImpl(ref Fmain, ref ExceptionSql);
        }
        /// <summary> 
        /// Exception handling for Passed Connection Creation.
        /// </summary> 
        public void ExeceptConnCreateImpl(ref mFileMainDef FmainPassed, ref SqlException ExceptionSql)
        {
            FileState.ConnectionCreateResult = StateIs.Started;
            Sys.sMessageBoxMessage = StdProcess.Title + "\n" + @"File Creation Status";
            Sys.sMessageBoxMessage += "\n" + @"Create Connection error!";
            Sys.sMessageBoxMessage += "\n" + @"SQL Exception Error";
            Sys.sMessageBoxMessage += "\n" + ExceptionSql.ToString();
            // <Area Id = "display message
        }
        #endregion
        #endregion
        #endregion
        #region Class Interface
        // implemented in partial classes
        #endregion
        public override string ToString()
        {
            return ("Sql Conn:" + Name + base.ToString());
        }
        #endregion
        #region Unit Test
        #endregion
    }
}
