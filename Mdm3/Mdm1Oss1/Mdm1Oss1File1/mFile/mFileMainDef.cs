using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

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
using Mdm.Oss.File.Type.Delim;
//using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion

namespace Mdm.Oss.File
{
    #region File Main Object Definition
    /// <summary>
    /// <para> (Main) File Stread Object </para>
    /// <para> See mFile for an expanded discussion
    /// of these objects.  This is the object that
    /// performs file IO for a mFile File System
    /// Object.  There is a primary and auxillary object.</para>
    /// </summary>
    /// <remarks>
    /// <para> List of classes used:</para>
    /// <para> General objects:</para>
    /// <para> ....Io State</para>
    /// <para> ....File Action</para>
    /// <para> ....File Summary</para>
    /// <para> ....File Status</para>
    /// <para> Ascii File Objects:</para>
    /// <para> ....File Id (contained in File Summary)</para>
    /// <para> ....File Io (contained in File Summary)</para>
    /// <para> ....File Opt(ions) (contained in File Summary)</para>
    /// <para> ....Item</para>
    /// <para> ....Buf</para>
    /// <para> ....Buf Io</para>
    /// <para> Database File Objects:</para>
    /// <para> ....Db Io</para>
    /// <para> ....Db Status</para>
    /// <para> ....System Status</para>
    /// <para> ....Server Status</para>
    /// <para> ....Conn(ection) Status</para>
    /// <para> Column and Row Management:</para>
    /// <para> ....Row Info(rmation)</para>
    /// <para> ....Col(umn) Index</para>
    /// <para> Primary and Auxillary Column Indexing:</para>
    /// <para> ....Row Info(rmation)</para>
    /// <para> Column Transformation:</para>
    /// <para> ....Col(umn)Transform</para>
    /// <para> Other:</para>
    /// <para> ....Del(imiter)Sep(arator)</para>
    /// </remarks>
    public class mFileMainDef : StdBaseDef, IDisposable
    {
        /// <summary>
        /// </summary>
        public new mFileDef FileObject;
        /// <summary>
        /// </summary>
        public mFileMainDef FmainObject;
        /// <summary>
        /// </summary>
        public ImFileType FileObjectSql;
        /// <summary>
        /// </summary>
        public mFileSqlConnectionDef FileSqlConn;
        //
        /// <summary>
        /// </summary>
        public String FmainStreamType;
        // System Synchronization
        /// <summary>
        /// </summary>
        public IoStateDef IoState;
        // Action
        /// <summary>
        /// </summary>
        public FileActionDef FileAction;
        // Primary File
        /// <summary>
        /// </summary>
        public FileSummaryDef Fs;
        // public FileIdDef FileId;
        // public FileIoDef FileIo;
        //public FileStatusDef FileStatus;
        // Non database files
        #region $include Mdm.Oss.File mFile FileBase_FileSystem (Ascii, Binary, Text)
        // Ascii, Text, Tilde...
        //public ItemId uses FileId
        public ItemDef Item;
        //public ItemIo uses FileIo
        // LINE???
        // SENTENCE???
        // PARAGRAPH???
        // SECTION???
        //
        // Binary, Block Mode reads...
        //public BufId uses FileId
        public BufDef Buf;
        public BufIoDef BufIo;
        #endregion
        // Rows & Columns
        #region $include Mdm.Oss.File mFile FileBase_RowsColumns
        // ROWS:
        public RowInfoDef RowInfo;
        // COLUMNS:
        public ColIndexDef ColIndex;
        #endregion
        // Delimited Text File Files (Ascii, Text, Tilde)
        /// <summary>
        /// </summary>
        public DelimSepDef DelSep;
        // Database
        #region $include Mdm.Oss.File mFile FileBase_Database
        // SqlClient FileDbConnObject - Primary
        public DbIoDef DbIo;
        public FileStatusDef SystemStatus;
        public FileStatusDef ServerStatus;
        public FileStatusDef ConnStatus;
        //public FileStatusDef FileStatus;
        public DbMasterDef DbMaster;
        #endregion
        // Primary and Auxillary Column Indexing
        /// <summary>
        /// </summary>
        public RowInfoDef RowInfoDb;
        // Column Transformation
        /// <summary>
        /// </summary>
        protected internal ColTransformDef ColTrans;
        //
        private bool ParamsUsed = false;

        #region Constructors
        public mFileMainDef(mFileDef mFilePassed, FileAction_DirectionIs DirectionPassed, String StreamTypePassed)
        {
            Sender = mFilePassed;
            FileObject = mFilePassed;
            FmainStreamType = StreamTypePassed;
            this.InitializeMFileMain();
        }
        public mFileMainDef()
        {
            Sender = this;
            this.InitializeMFileMain();
            FmainStreamType = "Default";
        }
        public virtual void InitializeMFileMain()
        {
            base.InitializeStd();
            if (!ClassFeatureFlag.InitializeFileMain)
            {
                InitializeFileMain(FileAction_DirectionIs.None);
            }
        }
        public void InitializeFileMain(FileAction_DirectionIs DirectionPassed)
        {
            ClassFeatureFlag.InitializeFileMain = true;
            #region Primary File
            FmainObject = this;
            //
            if (instantiated) { this.Dispose(true); }
            //
            IoState = new IoStateDef();
            //
            FileAction = new FileActionDef(ref FileObject);
            // File Identification
            // FileId = new FileIdDef();
            Fs = new FileSummaryDef(ref Sender, ref FileObject, DirectionPassed);
            Fs.FmainStreamType = FmainStreamType;
            // File Status
            FileStatus = new FileStatusDef();
            #endregion
            #region Database
            DbIo = new DbIoDef();
            SystemStatus = new FileStatusDef();
            ServerStatus = new FileStatusDef();
            ConnStatus = new FileStatusDef();
            FileStatus = new FileStatusDef();
            // Rows
            RowInfo = new RowInfoDef();
            RowInfoDb = new RowInfoDef();
            // Column / Field Indexing
            ColTrans = new ColTransformDef();
            ColIndex = new ColIndexDef();
            #endregion
            #region Connection
            // Create Connection here.
            if (Sender is mFileSqlConnectionDef)
            {
                FileSqlConn = (mFileSqlConnectionDef)Sender;
            }
            else
            {
                FileSqlConn = new mFileSqlConnectionDef();
            }
            #endregion
            #region Buffer Based I/O
            BufIo = new BufIoDef();
            // Items
            // Item / Row
            Item = new ItemDef();
            Buf = new BufDef();
            #endregion
            // Delimited Text File Files (Ascii, Text, Tilde) - xxxxxxxxxxxxxxxxxxxxx
            DelSep = new DelimSepDef();
            #region Database Master File
            DbMaster = new DbMasterDef();
            #endregion
            instantiated = true;
        }
        #endregion
        #region Destructors
        // Track whether Dispose has been called.
        /// <summary>
        /// </summary>
        protected bool disposed = false;
        /// <summary>
        /// </summary>
        protected bool instantiated = false;

        /// <summary>
        /// </summary>
        ~mFileMainDef()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        /// <summary>
        /// </summary>
        public new void Dispose()
        {
            base.Dispose();
            Dispose(true);
        }
        /// <summary>
        /// </summary>

        protected void Dispose(bool disposing)
        {
            // If disposing equals true, dispose all managed
            // and unmanaged resources
            if (disposing)
            {
                //if (components != null) {
                //    components.Dispose();
                //}
                DbIo.Dispose(disposing);
                
            }
            // Call the appropriate methods to clean up
            // unmanaged resources here.
            // base.Dispose(disposing);
            disposed = true;
        }
        #endregion

        /// <summary>
        /// </summary>
        public void ResetObjects()
        {
            DbIo.DataClear();
            Fs.DataClear();
        }

        /// <summary>
        /// </summary>
        public void DataClear()
        {
            ResetObjects();
            Fs.DataClear();
            FileStatus = new FileStatusDef();
            SystemStatus = new FileStatusDef();
            ServerStatus = new FileStatusDef();
            ConnStatus = new FileStatusDef();
            FileStatus = new FileStatusDef();
            //
            RowInfo = new RowInfoDef();
            ColTrans = new ColTransformDef();
            //
            DbMaster = new DbMasterDef();
            ////
            //FmainObject;
            //// Object File;
            //FileObject;
            //// System Synchronization
            //IoState;
            //// Action
            //FileAction;
            //// Delimited Text File Files (Ascii, Text, Tilde)
            //DelSep;
        }

    }
    #endregion
}
