#region Dependencies
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Navigation;
//
using Mdm;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.Mobj;
using Mdm.Oss.Components;
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
//using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion

namespace Mdm.Oss.File
{
    #region File Core - FileSummaryDef, FileIdDef
    /// <summary>
    /// <para> File Summary Class</para>
    /// <para> This class contains all of the 
    /// text fields used to identify a file,
    /// its system, server, database, location
    /// on disk and related fields such as
    /// ownership and security.</para>
    /// <para> This object is intended to be
    /// passed between applications and has a
    /// relatively small footprint compared to
    /// the mFile File Application Object or 
    /// either of the FileMain Stream Objects 
    /// Fmain and Faux.</para>
    /// <para> The class also includes instances
    /// of FileIo, FileId and FileOpt(ion) classes.
    /// These exist here to provide user interface
    /// access to completely identify a file object.</para>
    /// <para> . </para>
    /// <para> . </para>
    /// </summary>
    public class FileSummaryDef : StdBasePickSyntaxDef, IDisposable
    {
        #region Declarations
        public FileSummaryDef FsObject;
        // public Object File;
        public new mFileDef FileObject;
        //
        public String FmainStreamType;
        //
        public FileOptionsDef FileOpt;
        // File
        #region File Type Information
        public FileTypeItemDef FileTypeItem;
        #region Type Fields
        #region MetaLevel
        public String spMetaLevelName;
        public String MetaLevelName
        {
            get { return spMetaLevelName; }
            set { spMetaLevelName = value; }
        }
        public FileType_LevelIs ipMetaLevelId;
        public FileType_LevelIs MetaLevelId
        {
            get { return ipMetaLevelId; }
            set
            {
                if (!Enum.IsDefined(typeof(FileType_LevelIs), value))
                {
                    value = FileTypeDef.FileTypeMetaLevelGet(value);
                }
                if (Enum.IsDefined(typeof(FileType_LevelIs), value))
                {
                    ipMetaLevelId = value;
                    spMetaLevelName = Enum.GetName(typeof(FileType_LevelIs), ipMetaLevelId);
                }
                else { spMetaLevelName = "Meta Level is not valid."; }
            }
        }
        #endregion
        #region FileTypeName
        //
        public String spFileTypeName;
        public String FileTypeName
        {
            get { return spFileTypeName; }
            set
            {
                ((mFileDef)Sender).FileState.DoingFileTypeName = true;
                try
                {
                    if (!((mFileDef)Sender).FileState.DoingFileType)
                    {
                        FileType = (FileType_Is)Enum.Parse(typeof(FileType_Is), value);
                    }
                    spFileTypeName = value;
                }
                catch { }
                ((mFileDef)Sender).FileState.DoingFileTypeName = false;
            }
        }
        public FileType_Is ipFileType;
        public FileType_Is FileType
        {
            get { return ipFileType; }
            set { ipFileType = value; }
            //set
            //{
            //    DoingFileTypeId = true;
            //    try
            //    {
            //        if (!DoingFileType)
            //        {
            //            FileType = Enum.GetName(typeof(FileType_Is), value);
            //        }
            //        ipFileTypeId = value;
            //        // MetaLevelId = FileTypeDef.FileTypeMetaLevelGet(ipFileTypeId);
            //        FileTypeMajorId = FileTypeDef.FileTypeMajorGet(ipFileTypeId);
            //        FileTypeMinorId = FileTypeDef.FileTypeMinorGet(ipFileTypeId);
            //    }
            //    catch { }
            //    DoingFileTypeId = false;
            //}
        }
        public FileType_Is FileTypeMajor; // out of use
        public FileType_Is FileTypeMinor; // out of use
        #endregion
        #region FileSubTypeName
        //
        public String spFileSubTypeName;
        public String FileSubTypeName
        {
            get { return spFileSubTypeName; }
            set
            {
                ((mFileDef)Sender).FileState.DoingFileSubTypeName = true;
                try
                {
                    if (!((mFileDef)Sender).FileState.DoingFileSubType)
                    {
                        FileSubType = (FileType_SubTypeIs)Enum.Parse(typeof(FileType_SubTypeIs), value);
                    }
                    spFileSubTypeName = value;
                }
                catch { }
                ((mFileDef)Sender).FileState.DoingFileSubTypeName = false;
            }
        }
        public FileType_SubTypeIs ipFileSubType;
        public FileType_SubTypeIs FileSubType
        {
            get { return ipFileSubType; }
            set
            {
                ((mFileDef)Sender).FileState.DoingFileSubType = true;
                try
                {
                    if (!((mFileDef)Sender).FileState.DoingFileSubTypeName)
                    {
                        FileSubTypeName = Enum.GetName(typeof(FileType_SubTypeIs), value);
                    }
                    ipFileSubType = value;
                    // MetaLevelId = FileSubTypeDef.FileSubTypeMetaLevelGet(ipFileSubTypeId);
                    FileSubTypeMajor = FileTypeDef.FileSubTypeMajorGet(ipFileSubType);
                    FileSubTypeMinor = FileTypeDef.FileSubTypeMinorGet(ipFileSubType);
                }
                catch { }
                ((mFileDef)Sender).FileState.DoingFileSubType = false;
            }
        }
        public FileType_SubTypeIs FileSubTypeMajor; // out of use
        public FileType_SubTypeIs FileSubTypeMinor; // out of use
        #endregion
        #endregion
        #region Type Set From
        StateIs FileTypeSetResult;
        public StateIs FileTypeSetFromLine(String FileNameLinePassed)
        {
            FileTypeSetResult = StateIs.Started;
            // Extension analysis
            String FileExtPassed;
            if (FileNameLinePassed == null) { FileNameLinePassed = FileId.FileNameLine; }
            if (FileNameLinePassed.Length == 0) { FileNameLinePassed = FileId.FileNameLine; }
            if (FileNameLinePassed.Contains(Dot))
            {
                FileExtPassed = FileNameLinePassed.FieldLast(Dot);
            }
            else { FileExtPassed = sEmpty; }
            FileTypeSetResult = FileTypeSetFromExt(FileExtPassed);
            return FileTypeSetResult;
        }
        public StateIs FileTypeSetFromExt(String FileExtPassed)
        {
            FileTypeSetResult = StateIs.Started;
            if (FileExtPassed == null) { FileExtPassed = FileId.FileExt; }
            if (FileExtPassed.Length == 0) { FileExtPassed = FileId.FileExt; }
            if (FileExtPassed.Contains(Dot)) { FileExtPassed = FileExtPassed.FieldLast(Dot); }
            //
            FileTypeItem = null;
            FileTypeSetResult = StateIs.DoesExist;
            if (FileExtPassed.Length == 0)
            {
                // No extension is SQL (MS) format
                FileTypeItem = FileTypeDef.FileTypeGetTypeId(FileType_Is.SQL);
            }
            else
            {
                FileTypeItem = FileTypeDef.FileTypeGetExt(FileExtPassed);
                if (FileTypeItem == null)
                {
                    FileTypeItem = FileTypeDef.FileTypeGetDefault();
                    FileTypeSetResult = StateIs.DoesNotExist;
                }
            }
            //
            if (FileTypeItem != null)
            {
                MetaLevelId = FileTypeItem.MetaLevelId;
                FileType = FileTypeItem.FileTypeId;
                // FileTypeName = FileTypeItem.FileTypeName;
                FileSubType = FileTypeItem.FileSubTypeId;
                // FileSubTypeName = FileTypeItem.FileSubTypeName;
                FileIo.IoMode = FileTypeItem.IoType;
                FileIo.FileReadMode = FileTypeItem.FileReadMode;
            }
            else { FileTypeSetResult = StateIs.DoesNotExist; }
            return FileTypeSetResult;
        }
        #endregion
        #endregion

        #region File Name Control
        //
        public String spFileNameFullCurrent;
        public String FileNameFullCurrent
        {
            get { return spFileNameFullCurrent; }
            set { spFileNameFullCurrent = value; }
        }
        //
        public String spFileNameFullNext;
        public String FileNameFullNext
        {
            get { return spFileNameFullNext; }
            set { spFileNameFullNext = value; }
        }
        public String FileNameFullDefault;
        public String FileNameFullOriginal;
        #endregion
        #region FileId and Version
        // File Record Version
        public String FileVersion;
        public String FileVersionDate;
        //public int ipFileId;
        //public int FileId {
        //    get { return ipFileId; }
        //    set { ipFileId = value; }
        //}
        public FileIdDef FileId;
        public FileIoDef FileIo;
        #endregion
        #region FileId and Version
        public DbIdDef DbId;
        #endregion
        #region ItemIdCurrent
        public String spItemIdCurrent;
        public String ItemIdCurrent
        {
            get { return spItemIdCurrent; }
            set { spItemIdCurrent = value; }
        }
        // ItemId
        public String spItemIdNext;
        public String ItemIdNext
        {
            get { return spItemIdNext; }
            set { spItemIdNext = value; }
        }

        // ItemId
        public String spItemIdPrev;
        public String ItemIdPrev
        {
            get { return spItemIdPrev; }
            set { spItemIdPrev = value; }
        }
        #endregion
        #region Dir, System, Server... database names
        protected internal String spDirectionName;
        public String DirectionName
        {
            get { return spDirectionName; }
            set { spDirectionName = value; }
        }
        // direction
        protected internal FileAction_DirectionIs ipDirection;
        public FileAction_DirectionIs Direction
        {
            get { return ipDirection; }
            set
            {
                if (Enum.IsDefined(typeof(FileAction_DirectionIs), value))
                {
                    ipDirection = value;
                    spDirectionName = Enum.GetName(typeof(FileAction_DirectionIs), ipDirection);
                }
            }
        }
        // IoType
        protected internal FileIo_ModeIs ipIoType;
        public FileIo_ModeIs IoMode
        {
            get { return ipIoType; }
            set { ipIoType = value; }
        }
        public StateIs ModeResult;
        // System
        public String spSystemName;
        public String SystemName
        {
            get { return spSystemName; }
            set { spSystemName = value; }
        }
        public Object SystemObject;
        //
        // Server
        public String spServerName;
        public String ServerName
        {
            get { return spServerName; }
            set
            {
                if (spServerName != value && spServerName.Length > 0) { ((mFileDef)Sender).FileState.ConnDoReset = true; }
                spServerName = value;
            }
        }
        public Object ServerObject;
        //
        // Service
        public String spServiceName;
        public String ServiceName
        {
            get { return spServiceName; }
            set { spServiceName = value; }
        }
        public Object ServiceObject;
        //
        // Database
        public String spDatabaseName;
        public String DatabaseName
        {
            get { return spDatabaseName; }
            set
            {
                if (spDatabaseName != value && spDatabaseName.Length > 0) { ((mFileDef)Sender).FileState.ConnDoReset = true; }
                spDatabaseName = value;
            }
        }
        // Table
        public String spTableName;
        public String TableName
        {
            get { return spTableName; }
            set { spTableName = value; }
        }
        // Table ** not used **
        public String spTableNameFull;
        public String TableNameFull
        {
            get { return spTableNameFull; }
            set { spTableNameFull = value; }
        }
        // Table Name Line
        protected internal String spTableNameLine;
        public String TableNameLine
        {
            get { return spTableNameLine; }
            set { spTableNameLine = value; }
        }
        //
        // FileGroup
        public String spFileGroupName;
        public String FileGroupName
        {
            get { return spFileGroupName; }
            set { spFileGroupName = value; }
        }
        // FileGroupId
        public int ipFileGroupId;
        public int FileGroupId
        {
            get { return ipFileGroupId; }
            set { ipFileGroupId = value; }
        }
        //
        // Owner
        protected internal String spFileOwnerName;
        public String FileOwnerName
        {
            get { return spFileOwnerName; }
            set { spFileOwnerName = value; }
        }
        #endregion
        //
        #region FileOptionsDef
        public String spFileOptionString;
        public String FileOptionString
        {
            get { return spFileOptionString; }
            set { spFileOptionString = value; }
        }
        // FileWrite
        public bool ItemWriteItBoolFlag;
        #endregion
        #region Database Master
        // File Command Lines
        public String MasterSystemLine;
        public String MasterServerLine;
        public String MasterDatabaseLine;
        public String MasterFileLine;
        #endregion
        #region User Command Lines
        public String UserNameLine;
        public String UserPasswordLine;
        public bool UserPasswordRequiredOption;
        #endregion
        #region Security Authorization
        public bool SecurityWindowsAuth;
        public bool SecuritySqlAuth;
        // Security Lines
        public String SecurityMasterSystemLine;
        public String SecurityMasterServerLine;
        public String SecurityMasterDatabaseLine;
        public String SecurityMasterFileLine;
        #endregion
        #endregion
        #region Constructors
        public FileSummaryDef(ref mFileDef SenderPassed)
            : base()
        {
            if (SenderPassed != null) { FileObject = SenderPassed; }
            this.Initialize();
        }
        public FileSummaryDef(ref object SenderPassed)
            : base(ref SenderPassed)
        {
            if (SenderPassed != null && SenderParent is mFileDef) { FileObject = (mFileDef)SenderPassed; }
            this.Initialize();
        }
        public FileSummaryDef(ref object SenderPassed, ref mFileDef FileObjectPassed, FileAction_DirectionIs DirectionPassed)
            : base(ref SenderPassed)
        {
            if (FileObjectPassed != null && FileObjectPassed is mFileDef) { FileObject = (mFileDef)FileObjectPassed; }
            this.Initialize();
            InitializeFileSummaryDef(DirectionPassed);
        }
        public FileSummaryDef()
            : base()
        {
            this.Initialize();
        }
        public void Initialize()
        {
            base.InitializeStdBasePickSyntax();
            if (!ClassFeatureFlag.InitializeFileSummaryDef)
            {
                InitializeFileSummaryDef(FileAction_DirectionIs.None);
            }
        }
        public void InitializeFileSummaryDef(FileAction_DirectionIs DirectionPassed)
        {
            ClassFeatureFlag.InitializeFileSummaryDef = true;
            FsObject = this;
            FileId = new FileIdDef();
            FileIo = new FileIoDef();
            FileOpt = new FileOptionsDef();
            DbId = new DbIdDef();
            FileTypeItem = new FileTypeItemDef();
            DataClear();
            Direction = DirectionPassed;
        }
        #endregion
        #region Fs Copy
        public void CopyTo(ref FileSummaryDef FsPassed)
        {
            ((mFileDef)FileObject).FileState.DoingCopy = true;
            FileId.CopyTo(ref FsPassed.FileId);
            #region SetInputItem
            FsPassed.Direction = Direction;
            FsPassed.IoMode = IoMode;
            // State Change
            FsPassed.spFileNameFullCurrent = spFileNameFullCurrent;
            FsPassed.spFileNameFullNext = spFileNameFullNext;
            FsPassed.FileNameFullDefault = FileNameFullDefault;
            FsPassed.FileNameFullOriginal = FileNameFullOriginal;
            //
            FsPassed.FileVersion = FileVersion;
            FsPassed.FileVersionDate = FileVersionDate;
            FsPassed.ItemWriteItBoolFlag = ItemWriteItBoolFlag;
            #endregion
            #region ItemType Set
            FsPassed.ipMetaLevelId = ipMetaLevelId;
            FsPassed.ipFileType = ipFileType;
            FsPassed.ipFileSubType = ipFileSubType;
            #endregion
            #region File Other Attributes
            FsPassed.spSystemName = spSystemName;
            FsPassed.spServerName = spServerName;
            FsPassed.spServiceName = spServiceName;
            FsPassed.spDatabaseName = spDatabaseName;
            // FsPassed.ConnDoReset = ConnDoReset; // Applies to DbIo not Fs
            FsPassed.spTableName = spTableName;
            FsPassed.spTableNameFull = spTableNameFull;
            FsPassed.spTableNameLine = spTableNameLine;
            //
            FsPassed.spFileOwnerName = spFileOwnerName;
            FsPassed.spFileGroupName = spFileGroupName;
            FsPassed.ipFileGroupId = ipFileGroupId;
            #endregion
            #region Options, Id, Master, Security and User
            FsPassed.spFileOptionString = spFileOptionString;
            //
            FsPassed.spItemIdCurrent = spItemIdCurrent;
            FsPassed.spItemIdNext = spItemIdNext;
            FsPassed.spItemIdPrev = spItemIdPrev;
            //
            FsPassed.MasterDatabaseLine = MasterDatabaseLine;
            FsPassed.MasterFileLine = MasterFileLine;
            FsPassed.MasterServerLine = MasterServerLine;
            FsPassed.MasterSystemLine = MasterSystemLine;
            //
            FsPassed.SecurityMasterDatabaseLine = SecurityMasterDatabaseLine;
            FsPassed.SecurityMasterFileLine = SecurityMasterFileLine;
            FsPassed.SecurityMasterServerLine = SecurityMasterServerLine;
            FsPassed.SecurityMasterSystemLine = SecurityMasterSystemLine;
            FsPassed.SecuritySqlAuth = SecuritySqlAuth;
            FsPassed.SecurityWindowsAuth = SecurityWindowsAuth;
            //
            FsPassed.UserNameLine = UserNameLine;
            FsPassed.UserPasswordLine = UserPasswordLine;
            FsPassed.UserPasswordRequiredOption = UserPasswordRequiredOption;
            #endregion
            ((mFileDef)FileObject).FileState.DoingCopy = false;
        }
        public void CopyFrom(ref FileSummaryDef FsPassed)
        {
            ((mFileDef)FileObject).FileState.DoingCopy = true;
            FileId.CopyTo(ref FileId);
            #region SetInputItem
            Direction = FsPassed.Direction;
            IoMode = FsPassed.IoMode;
            // State Change
            spFileNameFullCurrent = FsPassed.spFileNameFullCurrent;
            spFileNameFullNext = FsPassed.spFileNameFullNext;
            FileNameFullDefault = FsPassed.FileNameFullDefault;
            FileNameFullOriginal = FsPassed.FileNameFullOriginal;
            //
            FileVersion = FsPassed.FileVersion;
            FileVersionDate = FsPassed.FileVersionDate;
            ItemWriteItBoolFlag = FsPassed.ItemWriteItBoolFlag;
            #endregion
            #region ItemType Set
            ipMetaLevelId = FsPassed.ipMetaLevelId;
            ipFileType = FsPassed.ipFileType;
            ipFileSubType = FsPassed.ipFileSubType;
            #endregion
            #region File Other Attributes
            spSystemName = FsPassed.spSystemName;
            spServerName = FsPassed.spServerName;
            spServiceName = FsPassed.spServiceName;
            spDatabaseName = FsPassed.spDatabaseName;
            // ConnDoReset = FsPassed.ConnDoReset; // Applies to DbIo not Fs
            spTableName = FsPassed.spTableName;
            spTableNameFull = FsPassed.spTableNameFull;
            spTableNameLine = FsPassed.spTableNameLine;
            //
            spFileOwnerName = FsPassed.spFileOwnerName;
            spFileGroupName = FsPassed.spFileGroupName;
            ipFileGroupId = FsPassed.ipFileGroupId;
            #endregion
            #region Options, Id, Master, Security and User
            spFileOptionString = FsPassed.spFileOptionString;
            //
            spItemIdCurrent = FsPassed.spItemIdCurrent;
            spItemIdNext = FsPassed.spItemIdNext;
            spItemIdPrev = FsPassed.spItemIdPrev;
            //
            MasterDatabaseLine = FsPassed.MasterDatabaseLine;
            MasterFileLine = FsPassed.MasterFileLine;
            MasterServerLine = FsPassed.MasterServerLine;
            MasterSystemLine = FsPassed.MasterSystemLine;
            //
            SecurityMasterDatabaseLine = FsPassed.SecurityMasterDatabaseLine;
            SecurityMasterFileLine = FsPassed.SecurityMasterFileLine;
            SecurityMasterServerLine = FsPassed.SecurityMasterServerLine;
            SecurityMasterSystemLine = FsPassed.SecurityMasterSystemLine;
            SecuritySqlAuth = FsPassed.SecuritySqlAuth;
            SecurityWindowsAuth = FsPassed.SecurityWindowsAuth;
            //
            UserNameLine = FsPassed.UserNameLine;
            UserPasswordLine = FsPassed.UserPasswordLine;
            UserPasswordRequiredOption = FsPassed.UserPasswordRequiredOption;
            #endregion
            ((mFileDef)FileObject).FileState.DoingCopy = false;
        }
        #endregion
        #region Fs Clear
        public void DataClear(mFileDef mFileDefPassed, FileAction_DirectionIs DirectionPassed)
        {
            DataClear();
            FileObject = mFileDefPassed;
            Direction = DirectionPassed;
        }

        public void DataClear()
        {
            FileOpt.DataClear();
            FileId.DataClear();
            FileIo.DataClear();
            DbId.DataClear();
            #region FileName current values, Version, Flags
            FileId.FileName = "Unknown99";
            // Direction = (int)FileAction_DirectionIs.None;
            // State Change
            spFileNameFullCurrent = sEmpty;
            spFileNameFullNext = sEmpty;
            FileNameFullDefault = sEmpty;
            FileNameFullOriginal = sEmpty;
            //
            FileVersion = sEmpty;
            FileVersionDate = sEmpty;
            ItemWriteItBoolFlag = false;
            #endregion
            #region File Type Set
            ipMetaLevelId = FileType_LevelIs.None;
            ipFileType = FileType_Is.None;
            ipFileSubType = FileType_SubTypeIs.None;
            #endregion
            #region File System Service, Server, Database, Table, Owner, Group
            spSystemName = sEmpty;
            if (SystemObject != null) { SystemObject = null; } // Dispose
            SystemObject = new object(); // not a proper object yet.
            spServerName = sEmpty;
            if (ServerObject != null) { ServerObject = null; } // Dispose
            ServerObject = new object(); // not a proper object yet.
            spServiceName = sEmpty;
            //
            spDatabaseName = sEmpty;
            if (Sender is mFileDef)
            {
                ((mFileDef)Sender).FileState.ConnDoReset = true;
                ((mFileDef)Sender).FileState.DoingCopy = false;
            } else if (Sender is FileSummaryDef)
            {
                // ((FileSummaryDef)Sender).???
            }
            spTableName = sEmpty;
            spTableNameLine = sEmpty;
            spTableNameFull = sEmpty;
            //
            spFileOwnerName = sEmpty;
            spFileGroupName = sEmpty;
            ipFileGroupId = 0;
            #endregion
            #region Options, Id, Master, Security and User
            spFileOptionString = sEmpty;
            //
            spItemIdCurrent = sEmpty;
            spItemIdNext = sEmpty;
            spItemIdPrev = sEmpty;
            //
            MasterDatabaseLine = sEmpty;
            MasterFileLine = sEmpty;
            MasterServerLine = sEmpty;
            MasterSystemLine = sEmpty;
            //
            SecurityMasterDatabaseLine = sEmpty;
            SecurityMasterFileLine = sEmpty;
            SecurityMasterServerLine = sEmpty;
            SecurityMasterSystemLine = sEmpty;
            SecuritySqlAuth = false;
            SecurityWindowsAuth = false;
            //
            UserNameLine = sEmpty;
            UserPasswordLine = sEmpty;
            UserPasswordRequiredOption = false;
            #endregion
        }
        #endregion
        #region Destructors
        // Track whether Dispose has been called.
        private bool disposed = false;
        private bool instantiated = false;
        // What is ~XXXXXX constructor?
        ~FileSummaryDef()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        public new void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            // If disposing equals true, dispose all managed
            // and unmanaged resources
            if (disposing)
            {
                //if (components != null) {
                //    components.Dispose();
                //}
                ((mFileDef)Sender).FileState.ConnDoReset = true;
                //FileId.Dispose(disposing);
                FileIo.Dispose(disposing);
                //FileOpt.Dispose(disposing);
                //DbId.Dispose(disposing);
            }
            // Call the appropriate methods to clean up
            // unmanaged resources here.
            // base.Dispose(disposing);
            disposed = true;
        }
        #endregion
        #region Name Get
        public String FileNameGetFrom(ref FileSummaryDef FsPassed)
        {
            if ((FsPassed.FileType & FileType_Is.MaskDatabase) > 0)
            {
                return FsPassed.TableNameFull;
            }
            else
            {
                return FsPassed.FileId.FileNameFull;
            }
        }

        public String FileNameLineGetFrom(ref FileSummaryDef FsPassed)
        {
            if ((FsPassed.FileType & FileType_Is.MaskDatabase) > 0)
            {
                return FsPassed.TableNameLine;
            }
            else
            {
                return FsPassed.FileId.FileNameLine;
            }
        }
        #endregion
        #region Parsing
        public String TableNameLineBuild(ref mFileMainDef FmainPassed)
        {
            FmainPassed.Fs.TableNameLine = sEmpty;
            // ?? System ?? Server ??
            // DatabaseName
            FmainPassed.Fs.TableNameLine += TableNameFullBuild(ref FmainPassed);
            //
            return FmainPassed.Fs.TableNameLine;
        }

        public String TableNameFullBuild(ref mFileMainDef FmainPassed)
        {
            FmainPassed.Fs.TableNameFull = sEmpty;
            // DatabaseName
            FmainPassed.Fs.TableNameFull += FmainPassed.Fs.spDatabaseName;
            FmainPassed.Fs.TableNameFull += ".";
            // FileOwnerName
            FmainPassed.Fs.TableNameFull += FmainPassed.Fs.spFileOwnerName;
            FmainPassed.Fs.TableNameFull += ".";
            // FileName
            FmainPassed.Fs.TableNameFull += FmainPassed.Fs.spTableName;
            //
            return FmainPassed.Fs.TableNameFull;
        }

        public void TableNameSetFromLine(String TableNameLinePassed)
        {
            String[] TempStringArr = new String[2];
            String TableNameLineTemp;
            String TableNameLineTemp1;
            if (TableNameLinePassed == null)
            {
                TableNameLineTemp = TableNameLine;
            }
            else
            {
                TableNameLineTemp = TableNameLinePassed;
            }
            if (TableNameLineTemp.Length == 0) { return; }
            //
            TableNameFullClear(false);
            TableNameLineTemp.Trim();
            TableNameLine = TableNameLineTemp;
            int DotCount = TableNameLine.Count(Dot);
            if (DotCount > 0)
            {
                TableNameLineTemp1 = TableNameLineTemp.Field(Dot, 3);
                if (DotCount >= 2)
                {
                    spTableName = TableNameLineTemp1;
                    spFileOwnerName = TableNameLineTemp.Field(Dot, 2);
                    spDatabaseName = TableNameLineTemp.Field(Dot, 1);
                    ((mFileDef)Sender).FileState.ConnDoReset = true;
                }
                else if (DotCount == 1)
                {
                    TableNameLineTemp1 = TableNameLineTemp.Field(Dot, 2);
                    if (TableNameLineTemp1.Length > 0)
                    {
                        spTableName = TableNameLineTemp1;
                        spFileOwnerName = TableNameLineTemp.Field(Dot, 1);
                    }
                    else
                    {
                        spTableName = TableNameLineTemp1;
                    }
                }
                if (spDatabaseName.Contains("["))
                {
                    spDatabaseName = spDatabaseName.Field("[", 2);
                    spDatabaseName = spDatabaseName.Field("]", 2);
                }
                if (spFileOwnerName.Contains("["))
                {
                    spFileOwnerName = spFileOwnerName.Field("[", 2);
                    spFileOwnerName = spFileOwnerName.Field("]", 2);
                }
            }
            else if (TableNameLineTemp.Contains(BackSlash))
            {
                spTableName = TableNameLineTemp;
            }
            else
            {
                spTableName = TableNameLineTemp;
            }
            if (spTableName.Contains("["))
            {
                spTableName = spTableName.Field("[", 2);
                spTableName = spTableName.Field("]", 2);
            }
        }

        public void TableNameFullClear(bool DoClearLinePassed)
        {
            if (DoClearLinePassed) { TableNameLine = sEmpty; }
            //spDatabaseName = sEmpty;
            //spFileOwnerName = sEmpty;
            TableName = sEmpty;
            TableNameFull = sEmpty;
        }

        #endregion
        public override String ToString()
        {
            if (FileId.FileName != null && DirectionName != null)
            {
                String sTemp = "File Summary: ";
                try
                {
                    sTemp += "Level: " + Enum.GetName(typeof(FileType_LevelIs), MetaLevelId);
                    sTemp += ", Type: " + Enum.GetName(typeof(FileType_Is), FileType);
                    sTemp += ", SubType: " + Enum.GetName(typeof(FileType_SubTypeIs), FileSubType);
                    sTemp += ". ";
                }
                catch { sTemp += "Type information not available. "; }
                if (SystemName.Length > 0) { sTemp += "System: " + SystemName + ". "; }
                if (ServiceName.Length > 0) { sTemp += "Service: " + ServiceName + ". "; }
                if (ServerName.Length > 0) { sTemp += "Server: " + ServerName + ". "; }
                if (DatabaseName.Length > 0) { sTemp += "Database: " + DatabaseName + ". "; }
                sTemp += "File: ";
                if ((FileType & FileType_Is.MaskDatabase) > 0)
                {
                    sTemp += spTableName;
                }
                else
                {
                    if (FileId.FileNameLine.Length > 0)
                    {
                        sTemp += FileId.FileNameLine;
                    }
                    else { sTemp += FileId.FileName; }
                }
                sTemp += " for " + DirectionName + ".";
                if (ItemIdCurrent.Length > 0) { sTemp += "Item Id: " + ItemIdCurrent + "."; }
                return sTemp;
            }
            else { return base.ToString(); }
        }
    }
    /// <summary>
    /// <para> File Identification Class</para>
    /// <para> File Name, Path, Drive, etc. </para>
    /// <para> This is used to identify the file that
    /// will be access and to store the disk file name
    /// for database files.</para>
    /// </summary>
    /// <remarks>
    /// <para> The two main types of objects are File and Db (Database).</para>
    /// <para> Each of these may have:</para>
    /// <para> 1) A basic file object (Fmain).</para>
    /// <para> 2) A File Summary Object. </para>
    /// <para> 3) An ID object for identity. </para>
    /// <para> 4) An IO object used readers and writers.</para>
    /// <para> 5) One or more File Status Objects.</para>
    /// <para> 6) One of serveral column and attribute control classes.</para>
    /// </remarks>
    public class FileIdDef : StdBasePickSyntaxDef
    {
        #region File Core Properties
        protected internal String spFileNameLine;
        public String FileNameLine
        {
            get { return spFileNameLine; }
            set { spFileNameLine = value; }
        }

        protected internal String spFileNameLineCurrent;
        public String FileNameLineCurrent
        {
            get { return spFileNameLineCurrent; }
            set { spFileNameLineCurrent = value; }
        }

        protected internal String spFileNameLineOriginal;
        public String FileNameLineOriginal
        {
            get { return spFileNameLineOriginal; }
            set { spFileNameLineOriginal = value; }
        }

        public String spFileNameFull;
        public String FileNameFull
        {
            get { return spFileNameFull; }
            set { spFileNameFull = value; }
        }
        // FileId
        public int ipFileId;
        public int FileId
        {
            get { return ipFileId; }
            set { ipFileId = value; }
        }
        // <Area Id = "FileName">
        protected internal String spFileName;
        public String FileName
        {
            get { return spFileName; }
            set { spFileName = value; }
        }
        protected internal String spFileExt;
        public String FileExt
        {
            get { return spFileExt; }
            set { spFileExt = value; }
        }
        #endregion
        #region Fields ($include) Mdm.Oss.File mFile FileMeta_Type_Details
        #region FileTypeDetails
        // <Area Id = "FileNameAlias">
        protected internal String spFileNameAlias;
        public String FileNameAlias
        {
            get { return spFileNameAlias; }
            set { spFileNameAlias = value; }
        }
        //
        protected internal String spFileShortName;
        public String FileShortName
        {
            get { return spFileShortName; }
            set { spFileShortName = value; }
        }
        //
        protected internal String spFileShort83Name;
        public String FileShort83Name
        {
            get { return spFileShort83Name; }
            set { spFileShort83Name = value; }
        }
        //
        protected internal String spFileShortUnixName;
        public String FileShortUnixName
        {
            get { return spFileShortUnixName; }
            set { spFileShortUnixName = value; }
        }
        // <Area Id = "FileGuid">
        protected internal Guid gpFileNameGuid;
        public Guid FileNameGuid
        {
            get { return gpFileNameGuid; }
            set { gpFileNameGuid = value; }
        }
        // <Area Id = "FileGuid">
        protected internal String spPropSystemPath;
        public String PropSystemPath
        {
            get { return spPropSystemPath; }
            set { spPropSystemPath = value; }
        }
        // File Status ////////////////////
        #endregion
        #region File Path and Location Info
        // <Area Id = "SourceDriveSystem - PhysicalLocation">
        // System Id (System Name) is equivalent to the NTFS Computer
        // System Name is stored in Fs along with owner.
        protected internal int ipFileDriveSystemId = -iUnknown;
        public int FileDriveSystemId
        {
            get { return ipFileDriveSystemId; }
            set { ipFileDriveSystemId = value; }
        }

        // <Area Id = "SourceDriveName - PhysicalLocation">
        // This is the mapped network drive name
        protected internal String spFileDriveName;
        public String FileDriveName
        {
            get { return spFileDriveName; }
            set { spFileDriveName = value; }
        }

        protected internal String spFileDriveLetter;
        public String FileDriveLetter
        {
            get { return spFileDriveLetter; }
            set { spFileDriveLetter = value; }
        }

        // Letter Alias is the letter to be assigned when
        // mapping network drives from other systems.
        protected internal String spFileDriveLetterMapAlias;
        public String FileDriveLetterMapAlias
        {
            get { return spFileDriveLetterMapAlias; }
            set { spFileDriveLetterMapAlias = value; }
        }

        // The short name is used by the property management system.
        protected internal String spFileDriveShortName;
        public String FileDriveShortName
        {
            get { return spFileDriveShortName; }
            set { spFileDriveShortName = value; }
        }

        protected internal String spFileDriveDriveLabel;
        public String FileDriveDriveLabel
        {
            get { return spFileDriveDriveLabel; }
            set { spFileDriveDriveLabel = value; }
        }

        // <Area Id = "SourcePathName">
        protected internal String spPathName;
        public String PathName
        {
            get { return spPathName; }
            set { spPathName = value; }
        }

        // Path Alias is the label to be assigned when
        // mapping network drives from other systems.
        // Example: Share\Music\Mp3 might be Mp3Music
        protected internal String spPathNameMapAlias;
        public String PathNameMapAlias
        {
            get { return spPathNameMapAlias; }
            set { spPathNameMapAlias = value; }
        }

        protected internal int ipPathId = -iUnknown;
        public int PathId
        {
            get { return ipPathId; }
            set { ipPathId = value; }
        }

        // The short name is used by the property management system.
        protected internal String spPathShortName;
        public String PathShortName
        {
            get { return spPathShortName; }
            set { spPathShortName = value; }
        }
        #endregion
        #endregion
        #region Clear Fields
        public void DataClear()
        {
            FileId = 0;
            //
            FileNameFullClear(true);
            //
            FileDriveLetterMapAlias = sEmpty;
            FileDriveShortName = sEmpty;
            FileDriveSystemId = 0;
            //
            PathShortName = sEmpty;
            //
            FileNameAlias = sEmpty;
            FileShortName = sEmpty;
            FileShort83Name = sEmpty;
            FileShortUnixName = sEmpty;
            //
            gpFileNameGuid = new Guid();
        }

        public void FileNameFullClear(bool DoClearLinePassed)
        {
            if (DoClearLinePassed) { FileNameLine = sEmpty; }
            FileDriveDriveLabel = sEmpty;
            FileDriveLetter = sEmpty;
            PathName = sEmpty;
            FileName = sEmpty;
            FileNameFull = sEmpty;
            FileExt = sEmpty;
        }
        #endregion
        #region Copy Fields
        public void CopyTo(ref FileIdDef FileIdPassed)
        {
            FileIdPassed.FileId = FileId;
            //
            FileNameFullCopyTo(ref FileIdPassed);
            //
            FileIdPassed.FileDriveLetterMapAlias = FileDriveLetterMapAlias;
            FileIdPassed.FileDriveShortName = FileDriveShortName;
            FileIdPassed.FileDriveSystemId = FileDriveSystemId;
            //
            FileIdPassed.PathShortName = PathShortName;
            //
            FileIdPassed.FileNameAlias = FileNameAlias;
            FileIdPassed.FileShortName = FileShortName;
            FileIdPassed.FileShort83Name = FileShort83Name;
            FileIdPassed.FileShortUnixName = FileShortUnixName;
            //
            FileIdPassed.gpFileNameGuid = gpFileNameGuid;
        }

        public void FileNameFullCopyTo(ref FileIdDef FileIdPassed)
        {
            FileIdPassed.FileNameLine = FileNameLine;
            FileIdPassed.FileDriveDriveLabel = FileDriveDriveLabel;
            FileIdPassed.FileDriveLetter = FileDriveLetter;
            FileIdPassed.PathName = PathName;
            FileIdPassed.FileName = FileName;
            FileIdPassed.FileNameFull = FileNameFull;
            FileIdPassed.FileExt = FileExt;
        }
        #endregion
        #region File Name Set, Build
        public void FileNameSetFromLine(String FileNameLinePassed)
        {
            String[] TempStringArr = new String[2];
            ////String TempString = sEmpty;
            ////TempString = FileNameLinePassed.Field(BackSlash, 0);
            ////TempString = FileNameLinePassed.Field(BackSlash, 1);
            ////TempString = FileNameLinePassed.Field(BackSlash, 2);
            ////TempString = FileNameLinePassed.Field(BackSlash, 3);
            ////TempString = FileNameLinePassed.Field(BackSlash, 4);
            ////TempString = FileNameLinePassed.FieldLast(BackSlash);
            ////TempStringArr = FileNameLinePassed.SplitInTwo(BackSlash, 0);
            ////TempStringArr = FileNameLinePassed.SplitInTwo(BackSlash, 1);
            ////TempStringArr = FileNameLinePassed.SplitInTwo(BackSlash, 2);
            ////TempStringArr = FileNameLinePassed.SplitInTwo(BackSlash, 3);
            ////TempStringArr = FileNameLinePassed.SplitLast(BackSlash);
            String FileNameLineTemp;
            if (FileNameLinePassed == null)
            {
                FileNameLineTemp = FileNameLine;
            }
            else
            {
                FileNameLineTemp = FileNameLinePassed;
            }
            if (FileNameLineTemp.Length == 0) { return; }
            //
            FileNameFullClear(false);
            FileNameLineTemp.Trim();
            FileNameLine = FileNameLineTemp;
            Collon = ":";
            if (FileNameLineTemp.Contains(Collon) && FileNameLineTemp.Substring(1, 1) == Collon)
            {
                TempStringArr = FileNameLineTemp.SplitInTwo(Collon, 1);
                FileDriveLetter = TempStringArr[0];
                FileNameLineTemp = TempStringArr[1];
            }
            else if (FileNameLineTemp.Contains(BackSlash))
            {
                TempStringArr = FileNameLineTemp.SplitInTwo(BackSlash, 1);
                FileDriveLetterMapAlias = TempStringArr[0];
                FileNameLineTemp = TempStringArr[1];
            }
            BackSlash = @"\";
            if (FileNameLineTemp.Contains(BackSlash))
            {
                TempStringArr = FileNameLineTemp.SplitLast(BackSlash);
                PathName = TempStringArr[0];
                FileNameFull = TempStringArr[1];
            }
            else { FileNameFull = FileNameLineTemp; }
            Dot = ".";
            if (FileNameFull.Contains(Dot))
            {
                TempStringArr = FileNameFull.SplitLast(Dot);
                FileName = TempStringArr[0];
                FileExt = TempStringArr[1];
            }
            else
            {
                FileName = FileNameFull;
                FileExt = sEmpty;
            }
        }
        public string FileNameLineBuild(ref mFileMainDef FmainPassed)
        {
            FmainPassed.Fs.FileId.FileNameLine = sEmpty;
            if (FmainPassed.Fs.FileId.FileDriveLetter.Length > 0)
            {
                FmainPassed.Fs.FileId.FileNameLine +=
                    FmainPassed.Fs.FileId.FileDriveLetter
                    += ":" + BackSlash;
                // or Network Drive
            }
            else if (FmainPassed.Fs.FileId.FileDriveLetterMapAlias.Length > 0)
            {
                FmainPassed.Fs.FileId.FileNameLine += BackSlash
                    + FmainPassed.Fs.FileId.FileDriveLetterMapAlias;
                // System
            }
            else if (FmainPassed.Fs.SystemName.Length > 0)
            {
                FmainPassed.Fs.FileId.FileNameLine += BackSlash
                    + FmainPassed.Fs.SystemName;
            }
            // Path
            if (FmainPassed.Fs.FileId.PathName.Length > 0)
            {
                FmainPassed.Fs.FileId.FileNameLine += FmainPassed.Fs.FileId.PathName;
                if (FmainPassed.Fs.FileId.PathName.LastIndexOf(BackSlash) != FmainPassed.Fs.FileId.PathName.Length)
                {
                    FmainPassed.Fs.FileId.FileNameLine += BackSlash;
                }
            }
            // FileName & Extension
            if (FmainPassed.Fs.FileId.FileNameFull.Length > 0)
            {
                FmainPassed.Fs.FileId.FileNameLine += FmainPassed.Fs.FileId.FileNameFull;
            }
            else
            {
                if (FmainPassed.Fs.FileId.FileName.Length > 0)
                {
                    FmainPassed.Fs.FileId.FileNameLine += FmainPassed.Fs.FileId.FileName
                        + "." + FmainPassed.Fs.FileId.FileExt;
                }
            }
            return FmainPassed.Fs.FileId.FileNameLine;
        }
        #endregion
    }
    #endregion
}
