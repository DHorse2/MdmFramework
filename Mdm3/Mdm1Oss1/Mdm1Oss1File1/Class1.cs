using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
//
using Mdm;
//@@@CODE@@@using Mdm.Oss.ClipUtil;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Decl;
using Mdm.Oss.File;
//@@@CODE@@@using Mdm.Oss.Mapp;
using Mdm.Oss.Mobj;
using Mdm.Pick;
using Mdm.Pick.Console;
//@@@CODE@@@using Mdm.Oss.Support;
using Mdm.Oss.Threading;

/// <summary>
/// File Management System
/// Object entities, Support and Utility items.
/// </summary> 

namespace Mdm.Oss.File
{

    /// <MyDocs>
    /// <MyMembers name="Class Object Types">
    /// <summary>
    /// <para> . </para>
    /// <para> The two main types of objects are File and Db (Database).</para>
    /// <para> Each of these may have:</para>
    /// <para> 1) A File Summary Object.</para>
    /// <para> 2) A basic file object (Fmain). </para>
    /// <para> 3) An ID object for identity. </para>
    /// <para> 4) An IO object used readers and writers.</para>
    /// <para> 5) One or more File Status Objects.</para>
    /// <para> 6) One of serveral column and attribute control classes.</para>
    /// </summary>
    /// </MyMembers>
    /// <MyMembers name="test">
    /// <summary>
    ///     The summary for this type.
    /// </summary>
    /// </MyMembers>
    /// 
    /// <MyMembers name="test2">
    /// <summary>
    ///     The summary for this other type.
    /// </summary>
    /// </MyMembers>
    /// </MyDocs>

    /// <include file='MfileObject.cs' path='MyDocs/MyMembers[@name="Class Object Types"]/*' />
    /// 'C:/Srt Project/Mdm/Mdm1/Mdm1Oss1/Mdm1Oss1File1/MfileDocumentation.xml'
    /// 'C:\Srt Project\Mdm\Mdm1\Mdm1Oss1\Mdm1Oss1File1\MfileDocumentation.xml' 
    /// <include file='xml_include_tag.doc' path='MyDocs/MyMembers[@name="Class Object Types"]/*' />


    #region mFile Readers, Writers, Database and File Objects and Buffers
    // Database Io
    /// <summary>
    /// <para> Database IO fields and objects including connection.</para>
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
    public class DbIoDef : IDisposable
    {
        #region $include Mdm.Oss.File mFile FileDbConnObjectConnection
        // <Area Id = "FileDbConnObject">
        // Database Connection
        // ofd  - 	Object - File - Database Connection
        /// <summary>
        /// </summary>
        public SqlConnection SqlDbConn = null;
        // ofde - 	Object - File - Database Connection - Error

        // ofdcd - 	Object - File - Database Connection - Delegate

        // ofdcv - 	Object - File - Database Connection - Event

        #endregion
        // SqlClient - SqlDataReader - xxxxxxxxxxxxxxxxxxxx
        // <Area Id = "ConnString">
        // property String ConnString
        /// <summary>
        /// </summary>
        protected internal String spConnString;
        /// <summary>
        /// </summary>
        public String ConnString
        {
            get { return spConnString; }
            set
            {
                spConnString = value;
            }
        } //   

        // Database Command - xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.File mFile FileDbConnObjectCommand
        // ofdc - 		Object - File - Database Command
        /// <summary>
        /// </summary>
        public SqlCommand SqlDbCommand = null;
        /// <summary>
        /// </summary>
        public int SqlDbRowsAffected = 0;
        /// <summary>
        /// </summary>
        public SqlDataReader SqlDbReader = null;
        /// <summary>
        /// </summary>
        public int SqlDbRowsRead = 0;
        /// <summary>
        /// </summary>
        public int SqlDbRowsWritten = 0;

        // Database Command - Timeout
        /// <summary>
        /// </summary>
        public int SqlDbCommandTimeout = 30;

        // Database Command - Error

        // Database Command - Delegate

        // File - Database Command - Event

        // File - Database Command - Adapter
        /// <summary>
        /// </summary>
        public SqlDataAdapter SqlDbAdapterObject = null;

        /// <summary>
        /// </summary>
        public String CommandCurrent = null;
        /// <summary>
        /// </summary>
        public String CommandPassed = null;
        #endregion
        // FILE SUBCLASS - FILE - READER
        // Database Sql ItemData Reader - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.File mFile FileObject SqlDataReader
        // ofddr - 		Object - File - Database Connection - DataReader
        /// <summary>
        /// </summary>
        public SqlDataReader SqlDbDataReader = null;
        /// <summary>
        /// </summary>
        public SqlDataAdapter SqlDbDataWriter = null;

        // ofddre - 	Object - File - Database Connection - DataReader- Error

        // ofddrcd - 	Object - File - Database Connection - DataReader- Delegate

        // ofddrcv - 	Object - File - Database Connection - DataReader- Event

        #endregion

        #region Destructors
        // Track whether Dispose has been called.
        /// <summary>
        /// </summary>
        private bool disposed = false;
        /// <summary>
        /// </summary>
        private bool instantiated = false;

        /// <summary>
        /// </summary>
        ~DbIoDef()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// </summary>
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (components != null) {
                //    components.Dispose();
                //}
            }
            DataClear();
            // base.Dispose(disposing);
            disposed = true;
        }
        #endregion

        /// <summary>
        /// </summary>
        public void DataClear()
        {
            CommandCurrent = "";
            CommandPassed = "";
            SqlDbCommandTimeout = 15;
            SqlDbRowsRead = 0;
            SqlDbRowsWritten = 0;
            SqlDbRowsAffected = 0;
            spConnString = "";
            //
            // TODO Error on the following statment:
            //if (SqlDbConn != null) { SqlDbConn.Dispose(); }
            if (SqlDbCommand != null) { SqlDbCommand.Dispose(); }
            if (SqlDbAdapterObject != null) { SqlDbAdapterObject.Dispose(); }
            if (SqlDbDataReader != null)
            {
                if (!SqlDbDataReader.IsClosed) { SqlDbDataReader.Close(); }
                // SqlDbDataReader.Dispose();
            }
            if (SqlDbDataWriter != null) { SqlDbDataWriter.Dispose(); }
        }

        /// <summary>
        /// </summary>
        public DbIoDef() { DataClear(); }
    }
    /// <summary>
    /// <para> File IO fields and objects.</para>
    /// <para> . </para>
    /// <para> File Io is used for the types:</para>
    /// <para> Ascii, Text, Xml, Uml, Tld, Delimited and Feild Separated.</para>
    /// <para> . </para>
    /// <para> The IO object includes buffers, streams and IO control flags.</para>
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
    public class FileIoDef : IDisposable
    {
        // File Object and ItemData - dxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.File mFile FileObjects
        // <Area Id = "FileObject">
        public Object DbFileObject;
        // System IO
        // System IO Stream
        public FileStream DbFileStreamObject;
        public StreamReader DbFileStreamReaderObject; // Stream Reader
        public StreamWriter DbFileStreamWriterObject; // Stream Writer
        // System IO TextReader
        public TextReader DbFileTextReadObject; // Text Reader
        #endregion
        // FileSystem Object Common 
        // FileSystem Object Common  - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.File mFile FsoFileControlState (make a struct)
        // <Section Summary>
        // <Section Role="Declarations">
        // <Section Id = "MdmFileFileIo">
        // <Section Vs="MdmFileDbVs0_8_9">
        // <Area Id = "GeneralStatusCondition">
        //
        // <Area Id = "IoType">
        protected internal long ipIoType;
        public long IoType
        {
            get { return ipIoType; }
            set { ipIoType = value; }
        }
        // <Area Id = "FileReadMode">
        protected internal long ipFileReadMode;
        public long FileReadMode
        {
            get { return ipFileReadMode; }
            set { ipFileReadMode = value; }
        }
        // <Area Id = "FileWriteMode">
        protected internal long ipFileWriteMode;
        public long FileWriteMode
        {
            get { return ipFileWriteMode; }
            set { ipFileWriteMode = value; }
        }
        // <Area Id = "FileAccessMode">
        protected internal long ipFileAccessMode;
        public long FileAccessMode
        {
            get { return ipFileAccessMode; }
            set { ipFileAccessMode = value; }
        }
        #endregion
        // 
        #region File Buffers
        protected internal String spIoReadBuffer;
        public String IoReadBuffer
        {
            get { return spIoReadBuffer; }
            set { spIoReadBuffer = value; }
        }

        protected internal String spIoBlock;
        public String IoBlock
        {
            get { return spIoBlock; }
            set { spIoBlock = value; }
        }

        protected internal String spIoLine;
        public String IoLine
        {
            get { return spIoLine; }
            set { spIoLine = value; }
        }

        protected internal String spIoAll;
        public String IoAll
        {
            get { return spIoAll; }
            set { spIoAll = value; }
        }
        #endregion

        #region Destructors
        // Track whether Dispose has been called.
        private bool disposed = false;
        private bool instantiated = false;

        ~FileIoDef()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (components != null) {
                //    components.Dispose();
                //}
                DataClear();
            }
            // base.Dispose(disposing);
            disposed = true;
        }
        #endregion

        public void DataClear()
        {
            // File Buffers
            spIoReadBuffer = "";
            spIoBlock = "";
            spIoLine = "";
            spIoAll = "";
            // FsoFileControlState
            ipIoType = 0;
            ipFileReadMode = 0;
            ipFileWriteMode = 0;
            ipFileAccessMode = 0;
            // FileObjects
            if (DbFileObject != null) { DbFileObject = null; }
            if (DbFileStreamObject != null) { DbFileStreamObject.Dispose(); }
            if (DbFileStreamReaderObject != null) { DbFileStreamReaderObject.Dispose(); }
            if (DbFileStreamWriterObject != null) { DbFileStreamWriterObject.Dispose(); }
        }

        public void CopyTo(ref FileIoDef FileIoPassed)
        {
            // File Buffers
            FileIoPassed.spIoReadBuffer = spIoReadBuffer;
            FileIoPassed.spIoBlock = spIoBlock;
            FileIoPassed.spIoLine = spIoLine;
            FileIoPassed.spIoAll = spIoAll;
            // FsoFileControlState
            FileIoPassed.ipIoType = ipIoType;
            FileIoPassed.ipFileReadMode = ipFileReadMode;
            FileIoPassed.ipFileWriteMode = ipFileWriteMode;
            FileIoPassed.ipFileAccessMode = ipFileAccessMode;
            // FileObjects
            if (DbFileObject != null) { DbFileObject = null; }
            if (DbFileStreamObject != null) { DbFileStreamObject.Dispose(); }
            if (DbFileStreamReaderObject != null) { DbFileStreamReaderObject.Dispose(); }
            if (DbFileStreamWriterObject != null) { DbFileStreamWriterObject.Dispose(); }
            DbFileStreamObject = null;
            DbFileStreamReaderObject = null;
            DbFileStreamWriterObject = null;
        }

        public FileIoDef()
        {
        }
    }
    // Binary and buffer based File IO
    /// <summary>
    /// <para> Buf IO instances are used for
    /// File IO for the buffer.</para>
    /// <para> . </para>
    /// <para> Generally, a File IO class like Buf
    /// will have an matching BufIO class which
    /// contains file buffers, connection pointers,
    /// file streams and other IO objects.</para>
    /// <para> It will additionally have an ID sub-class
    /// which contains ID's and fields that uniquely
    /// identify the item being accessed.</para>
    /// <para> Typically, a File Status object is
    /// also part of the group for status control</para>
    /// </summary>
    public class BufIoDef
    {
        // Buf.Seek
        // public File ofNativeFile;
        /// <summary>
        /// </summary>
        public int iRecordSize = 1024;
        /// <summary>
        /// </summary>
        public int iOffsetSize = (1024 * 32) - 1;
        /// <summary>
        /// </summary>
        public int iCurrentOffset;
        /// <summary>
        /// </summary>
        public int iCurrentOffsetModulo;
        /// <summary>
        /// </summary>
        public int iCurrentOffsetRemainder;
        /// <summary>
        /// </summary>
        public int CurrentAttrCounter;
        //
        /// <summary>
        /// </summary>
        public void DataClear()
        {
            iRecordSize = 1024;
            iOffsetSize = (1024 * 32) - 1;
            iCurrentOffset = 0;
            iCurrentOffsetModulo = 0;
            iCurrentOffsetRemainder = 0;
            CurrentAttrCounter = 0;
        }
        /// <summary>
        /// </summary>
        public BufIoDef()
        {
        }
    }

    // File Buffer
    /// <summary>
    /// <para>Buffer Definition includes the bufferes,
    /// read, write and converted counts, control flags
    /// and fields.  </para>
    /// <para> . </para>
    /// <para>The Char fields are indexers used to
    /// point at characters in the buffer.</para>
    /// <para> . </para>
    /// <para> Buf instances are used by the file system
    /// for the ASCII and Binary file types.</para>
    /// </summary>
    public class BufDef
    {
        #region $include Mdm.Oss.File mFile Buf.
        #region $include Mdm.Oss.File mFile Buf. Name
        /// <summary>
        /// </summary>
        public String FileName;
        #endregion
        #region $include Mdm.Oss.File mFile Buf. Buffer
        // Buf. Fields
        /// <summary>
        /// </summary>
        public String FileWorkBuffer;
        /// <summary>
        /// </summary>
        public String LineBuffer;
        /// <summary>
        /// </summary>
        public String NewItem;
        //
        // public String[] Buf.NewItem;     
        //  sNewItem=""
        // NOTE public String[] Item.ItemData; see Cols instead
        #endregion
        #region $include Mdm.Oss.File mFile Buf. Control Flags
        //  Convert Parameters
        /// <summary>
        /// </summary>
        public String ConvertableItem;
        /// <summary>
        /// </summary>
        public int ItemConvertFlag;
        //  Attr Indexing
        /// <summary>
        /// </summary>
        public int AttrIndex;
        /// <summary>
        /// </summary>
        public int AttrMaxIndex;
        //  Character Controls
        /// <summary>
        /// </summary>
        public int CharactersFound;
        /// <summary>
        /// </summary>
        public int CharCounter; // ItemData Items in AnyString
        #endregion
        #region $include Mdm.Oss.File mFile  Read
        //  Read
        // Number of Reads
        /// <summary>
        /// </summary>
        public int ReadFileCounter;
        // Number of Bytes Read
        /// <summary>
        /// </summary>
        public bool BytesIsRead = false;
        /// <summary>
        /// </summary>
        public int BytesRead;
        /// <summary>
        /// </summary>
        public int BytesReadTotal;
        /// <summary>
        /// </summary>
        public int BytesConverted;
        /// <summary>
        /// </summary>
        public int BytesConvertedTotal;
        #endregion
        #region $include Mdm.Oss.File mFile  Buffer Character Indexing
        //  Character Indexing for Buffer
        /// <summary>
        /// </summary>
        public int CharIndex = 1;
        /// <summary>
        /// </summary>
        public int CharMaxIndex;
        /// <summary>
        /// </summary>
        public int CharItemEofIndex;
        #endregion
        #region $include Mdm.Oss.File mFile  Write
        //  Writes
        //  Number of Writes
        /// <summary>
        /// </summary>
        public int WriteFileCounter;
        //  Number of Bytes Writen
        /// <summary>
        /// </summary>
        public int BytesWriten;
        /// <summary>
        /// </summary>
        public int BytesWritenTotal;
        #endregion
        #endregion
        /// <summary>
        /// </summary>
        public BufDef() { DataClear(); }
        /// <summary>
        /// Clear the read, write and converted fields.
        /// </summary>
        public void ByteCountClear()
        {
            // Read Counts
            BytesRead = 0;
            BytesReadTotal = 0;
            BytesConverted = 0;
            BytesConvertedTotal = 0;
        }
        /// <summary>
        /// Clear all buffer data.
        /// </summary>
        public void DataClear()
        {
            ByteCountClear();
        }
    }
    #endregion

    #region mFile ItemData Structures and Subclasses

    #region Row / Item
    /// <summary>
    /// <para>Item Definition contains the
    /// item data and item Id.</para>
    /// <para> An item is a data record.  Its
    /// specific content can be a:</para>
    /// <para> Document. </para>
    /// <para> File. </para>
    /// <para> Row of data.</para>
    /// <para> Result of one read. </para>
    /// <para> . </para>
    /// <para> A block is not considered an item.  One or
    /// more blocks are read to create an item.</para>
    /// </summary>
    public class ItemDef
    {
        #region ItemDef Declaration
        #region ItemId Declaration
        // ItemId
        /// <summary>
        /// </summary>
        public int ipItemPrimaryKey;
        /// <summary>
        /// </summary>
        public int ItemPrimaryKey
        {
            get { return ipItemPrimaryKey; }
            set { ipItemPrimaryKey = value; }
        }
        /// <summary>
        /// </summary>
        public bool NewRecord = false;
        // ItemId
        /// <summary>
        /// </summary>
        public String spItemId;
        /// <summary>
        /// </summary>
        public String ItemId
        {
            get { return spItemId; }
            set { spItemId = value; }
        }
        /// <summary>
        /// </summary>
        public long DataClear(ref ItemDef PassedItem)
        {
            ItemData = "";
            PassedItem.ItemLen = 0;
            PassedItem.ItemVersion = "";
            PassedItem.ItemVersionDate = "";
            #region ItemId
            PassedItem.ItemId = "Unknown ItemId99";
            #endregion
            return 0;
        }
        // IdControl
        // ItemIdExists
        /// <summary>
        /// </summary>
        public bool ipItemIdExists;
        /// <summary>
        /// </summary>
        public bool ItemIdExists
        {
            get { return ipItemIdExists; }
            set { ipItemIdExists = value; }
        }
        /// <summary>
        /// </summary>
        public String ItemIdChanged;
        /// <summary>
        /// </summary>
        public bool ItemIdIsChanged;
        /// <summary>
        /// </summary>
        public int ItemIdCurrentNotValid;
        #endregion
        #region Item Declaration
        // Item ItemData (a whole row or record)
        protected internal String spItemData;
        public String ItemData
        {
            get { return spItemData; }
            set
            {
                spItemData = value;
                DataIsSet = true;
            }
        }
        //
        public bool DataIsSet = false;
        // Item File Record Version
        public String ItemVersion;
        public String ItemVersionDate;
        // ItemLen
        public int ipItemLen;
        public int ItemLen
        {
            get { return ipItemLen; }
            set { ipItemLen = value; }
        }
        #endregion
        #endregion
        /// <summary>
        /// </summary>
        public ItemDef()
        {
        }
    }

    /// <summary>
    /// <para> Row Information </para>
    /// <para> Control flags, counts, indexers for
    /// row control.  Indicates if a line represents
    /// a column or row.</para>
    /// </summary>
    public class RowInfoDef
    {
        // FileRowInfo
        /// <summary>
        /// </summary>
        public bool LineIsRow = false;
        /// <summary>
        /// </summary>
        public bool LineIsColumn = false;
        // Read mode on database
        /// <summary>
        /// </summary>
        public long UseMethod;
        /// <remarks>
        /// Copied documentation on SQL Connection mode:
        /// 
        //Default The query may return multiple result sets. 
        // Execution of the query may affect the database state. 
        // Default sets no CommandBehavior flags, 
        // so calling ExecuteReader(CommandBehavior.Default) 
        // is functionally equivalent to calling ExecuteReader(). 

        //SingleResult The query returns a single result set. 

        //SchemaOnly The query returns column information only. 
        // When using SchemaOnly, the .NET Framework Data Provider 
        // for SQL Server precedes the statement being executed with SET FMTONLY ON. 

        //KeyInfo The query returns column and primary key information.  

        //SingleRow The query is expected to return a single row. 
        // Execution of the query may affect the database state. 
        // Some .NET Framework data providers may, but are not required to, 
        // use this information to optimize the performance of the command. 
        // When you specify SingleRow with the ExecuteReader method 
        // of the OleDbCommand object, the .NET Framework Data Provider 
        // for OLE DB performs binding using the OLE DB IRow interface 
        // if it is available. Otherwise, it uses the IRowset interface. 
        // If your SQL statement is expected to return only a single row, 
        // specifying SingleRow can also improve application performance. 
        // It is possible to specify SingleRow when executing queries 
        // that return multiple result sets. In that case, 
        // multiple result sets are still returned, but each result set has a single row. 

        //SequentialAccess Provides a way for the DataReader to handle rows 
        // that contain columns with large binary values. 
        // Rather than loading the entire row, SequentialAccess 
        // enables the DataReader to load data as a stream. 
        // You can then use the GetBytes or GetChars method to specify 
        // a byte location to start the read operation, 
        // and a limited buffer size for the data being returned. 
        //CloseConnection When the command is executed, the associated Connection object 
        // is closed when the associated DataReader object is closed. 
        /// </remarks>
        public bool CloseIsNeeded;
        // Row
        protected internal bool bpHasRows = false;
        public bool HasRows
        {
            get { return bpHasRows; }
            set { bpHasRows = value; }
        }
        public bool RowContinue;
        // property int RowIndex (Pointer to this row)
        protected internal int ipRowIndex;
        public int RowIndex
        {
            get { return ipRowIndex; }
            set
            {
                if (value <= ipRowCount)
                {
                    ipRowIndex = value;
                }
            }
        }
        protected internal int ipRowCount = -99999;
        public int RowCount
        {
            get { return ipRowCount; }
            set { ipRowCount = value; }
        }
        public int RowMax;
        // Colomn
        public bool HasColumns;
        public bool ColumnContinue;
        public int ColumnMax;
        // Sql
        public System.Object[] RowArray;
        public RowInfoDef()
        {
        }
        public RowInfoDef(int PdIndexMaxPassed)
        {
            RowMax = PdIndexMaxPassed;
        }
    }
    #endregion

    #region Column / Field
    /// <summary>
    /// <para>Column / Field Index Control Class</para>
    /// <para>An indexed class accessing the Col(umn)Array.</para>
    /// <para> . </para>
    /// <para>The Col(umn)Index access the Array which may
    /// hold up to ColumnMax items. The current count is 
    /// found in Col(umn)IndexTotal.</para>
    /// <para> . </para>
    /// <para>This is a general purpose class used for
    /// storing rows of data.  It can also be used for
    /// building row data or as a general utility class.</para>
    /// <para> . </para>
    /// <para>There is one group of fields, ColXxxxxx intended
    /// to manage this Array.  The second group ColAttrXxxxxx
    /// is independent and used locally as a seperate set of
    /// pointers</para>
    /// </summary>
    public class ColIndexDef
    {
        /// <summary>
        /// </summary>
        public ColIndexDef()
        {

        }
        // 
        /// <summary>
        /// property int ColIndex
        /// </summary>
        protected internal int ipaColIndex;
        /// <summary>
        /// </summary>
        public int ColIndex
        {
            get { return ipaColIndex; }
            set { ipaColIndex = value; }
        } //   
        /// <summary>
        /// </summary>
        public int ColIndexTotal;

        /// <summary>
        /// </summary>
        public int ColMaxIndex = (int)ArrayMax.ColumnMax;
        /// <summary>
        /// </summary>
        public int ColMaxIndexNew = (int)ArrayMax.ColumnMax + 1;
        // property String aColValues
        //
        /// <summary>
        /// </summary>
        public Object[] ColArray = new System.Object[(int)ArrayMax.ColumnMax];
        //
        /// <summary>
        /// </summary>
        public String ColValues
        {
            get { return (String)ColArray[ipaColIndex]; }
            set
            {
                ColArray[ipaColIndex] = (String)value;
                ColSet = true;
            }
        }
        /// <summary>
        /// ColMaxIndexTemp used with counter and buffers
        /// </summary>
        public int ColMaxIndexTemp;
        //
        /// <summary>
        /// </summary>
        public int ColCount;
        /// <summary>
        /// </summary>
        public int ColCountTotal;
        /// <summary>
        /// </summary>
        public int ColCounter;

        /// <summary>
        /// </summary>
        public bool ColSet = false;
        /// <summary>
        /// </summary>
        public int ColInvalid;
        // <Section Summary>
        // <Section Role="Declarations">
        // <Section Id = "MdmFileDictionaryDeclarations">
        // <Section Vs="MdmFileDictVs0_8_9">
        // MdmFileDictionaryDeclarations MdmFileDictVs0_8_9
        //
        /// <summary>
        /// </summary>
        public String ColText;

        /// <summary>
        /// </summary>
        public bool ColAttrSet = false;
        /// <summary>
        /// </summary>
        public int ColAttrInvalid;

        /// <summary>
        /// </summary>
        public int ColAttrIndex;
        /// <summary>
        /// </summary>
        public int ColAttrCount; // ItemData Items in Item / Row / Item
        /// <summary>
        /// </summary>
        public int ColAttrCountTotal;
        /// <summary>
        /// </summary>
        public int ColAttrCounter; // Current Attr
        /// <summary>
        /// </summary>
        public int ColAttrMaxIndex; // Total Attrs in Item
        /// <summary>
        /// </summary>
        public int ColAttrMaxIndexTemp;

        /// <summary>
        /// </summary>
        public int ColLength;

        /// <summary>
        /// </summary>
        public String ColId;
        /// <summary>
        /// </summary>
        public String ColTempId;

        /// <summary>
        /// </summary>
        public char[] CharsPassedIn;
        /// <summary>
        /// </summary>
        public char[] CharsPassedOut;

    }

    /// <summary>
    /// <para> Column Tranformation Definition</para>
    /// <para> Used to transform a column from on type
    /// to another including transformation and setting
    /// of strong type information for the coloumn.</para>
    /// </summary>
    public class ColTransformDef
    {
        // Action
        /// <summary>
        /// </summary>
        public long ColAction;
        //  ToDo File Column Action
        /// <summary>
        /// </summary>
        public const int SFC_RESET = 1;
        /// <summary>
        /// </summary>
        public const int SFC_SET_ROW = 2;
        /// <summary>
        /// </summary>
        public const int SFC_SET_COLUMN = 3;
        /// <summary>
        /// </summary>
        public const int SFC_GET_NATIVE_VALUE = 4;
        /// <summary>
        /// </summary>
        public const int SFC_GET_SQL_VALUE = 5;
        /// <summary>
        /// </summary>
        public const int SFC_GET_IBM_U2 = 11;
        /// <summary>
        /// </summary>
        public const int SFC_GET_IBM_DB2 = 12;
        /// <summary>
        /// </summary>
        public const int SFC_GET_MdmTLD_DICT = 21;
        /// <summary>
        /// </summary>
        public const int SFC_GET_MdmTLD_DATA = 22;

        /// <summary>
        /// </summary>
        public const int SFC_SET_ColumnADD_CMD = 101;
        // File Level
        /// <summary>
        /// </summary>
        public bool FileUseIndexName;
        /// <summary>
        /// </summary>
        public int FileIndex;
        /// <summary>
        /// </summary>
        public String FileIndexName;
        /// <summary>
        /// </summary>
        public int FileCount;
        // Row
        /// <summary>
        /// </summary>
        public int ColRowIndex;
        /// <summary>
        /// </summary>
        public int ColRowCount;
        /// <summary>
        /// </summary>
        public bool ColumnHasRows;
        /// <summary>
        /// </summary>
        public String ColRowIndexName;
        /// <summary>
        /// </summary>
        public DateTime RowLastTouched;
        // Column
        /// <summary>
        /// </summary>
        public bool HasRows;
        /// <summary>
        /// </summary>
        public bool RowContinue;
        /// <summary>
        /// </summary>
        public int RowMax;
        // Column
        /// <summary>
        /// </summary>
        public int ColIndex;
        /// <summary>
        /// </summary>
        public int ColCount;
        /// <summary>
        /// </summary>
        public String ColIndexName;
        /// <summary>
        /// </summary>
        public DateTime ColIndexLastTouched;
        /// <summary>
        /// </summary>
        public int ColCountVisible;
        /// <summary>
        /// </summary>
        public int ColCountHidden;
        /// <summary>
        /// </summary>
        public int ColumnMax;
        // Get Results
        /// <summary>
        /// </summary>
        public int iGetIndex;
        /// <summary>
        /// </summary>
        public String sGetResultToString;
        /// <summary>
        /// </summary>
        public String sGetResultNotSupported;
        //
        // Meta and Control
        /// <summary>
        /// </summary>
        public Type ttGetFieldType;
        /// <summary>
        /// </summary>
        public String iGetName;
        /// <summary>
        /// </summary>
        public int iGetOrdinal;
        // XXX public IEnumerator<T> lnGetEnumerator;
        /// <summary>
        /// </summary>
        public Type ttGetProviderSpecificFieldType;
        /// <summary>
        /// </summary>
        public Object ooGetProviderSpecificValue;
        /// <summary>
        /// </summary>
        public int iGetProviderSpecificValues;
        /// <summary>
        /// </summary>
        public DataTable tfdtGetSchemaTable;
        /// <summary>
        /// </summary>
        public bool bICommandBehavior;
        /// <summary>
        /// </summary>
        public bool bIsDBNull;
        //
        /// <summary>
        /// </summary>
        public bool bNextResult;
        /// <summary>
        /// </summary>
        public bool bRead;
        // Native ItemData Client
        // Native Get and GetArray
        /// <summary>
        /// </summary>
        public Object ooGetValue;
        /// <summary>
        /// </summary>
        public int iGetValues;
        // Native Field Types
        /// <summary>
        /// </summary>
        public bool bGetBoolean;
        /// <summary>
        /// </summary>
        public byte bbGetByte;
        /// <summary>
        /// </summary>
        public long loGetBytes;
        /// <summary>
        /// </summary>
        public char bcGetChar;
        /// <summary>
        /// </summary>
        public long loGetChars;
        /// <summary>
        /// </summary>
        public String sGetDataTypeName;
        /// <summary>
        /// </summary>
        public DateTime tdtGetDateTime;
        /// <summary>
        /// </summary>
        public DateTimeOffset tdtoGetDateTimeOffset;
        /// <summary>
        /// </summary>
        public decimal deGetDecimal;
        /// <summary>
        /// </summary>
        public double doGetDouble;
        /// <summary>
        /// </summary>
        public float fGetFloat;
        /// <summary>
        /// </summary>
        public Guid tgGetGuid;
        /// <summary>
        /// </summary>
        public short isGetInt16;
        /// <summary>
        /// </summary>
        public ushort isuGetInt16;
        /// <summary>
        /// </summary>
        public int iGetInt32;
        /// <summary>
        /// </summary>
        public uint iuGetInt32;
        /// <summary>
        /// </summary>
        public long ilGetInt64;
        /// <summary>
        /// </summary>
        public ulong iluGetInt64;
        /// <summary>
        /// </summary>
        public String sGetString;
        /// <summary>
        /// </summary>
        public TimeSpan tdtsGetTimeSpan;
        // Sql ItemData Client
        // Sql Get and GetArray
        /// <summary>
        /// </summary>
        public int iGetSqlValues;
        /// <summary>
        /// </summary>
        public Object ooGetSqlValue;
        // Sql Field Types
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlBinary sqlbiGetSqlBinary;
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlBoolean sqlbGetSqlBoolean;
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlByte sqlbbGetSqlByte;
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlBytes sqliGetSqlBytes;
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlChars sqliGetSqlChars;
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlDateTime sqltdtGetSqlDateTime;
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlDecimal sqlfdGetSqlDecimal;
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlDouble fdGetSqlDouble;
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlGuid tgGetSqlGuid;
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlInt16 isGetSqlInt16;
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlInt32 iGetSqlInt32;
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlInt64 ilGetSqlInt64;
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlMoney fdGetSqlMoney;
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlSingle fGetSqlSingle;
        /// <summary>
        /// </summary>
        public System.Data.SqlTypes.SqlString sGetSqlString;
        /// <summary>
        /// </summary>
        // XML ItemData Client
        public System.Data.SqlTypes.SqlXml ooGetSqlXml;
        //
    }

    /// <summary>
    /// <para> Pick Column Indexing Management Class</para>
    /// <para> Used by the derived Pick DB Classes to 
    /// augment the basic column management in Column Index. </para>
    /// </summary>
    public class ColPickDef
    {
        #region RowColumnCharacterControl
        // ColTrans
        /// <summary>
        /// </summary>
        public String ColText;
        /// <summary>
        /// </summary>
        public String ColExtracted;
        /// <summary>
        /// </summary>
        public String ColTempId;
        /// <summary>
        /// </summary>
        public String ColTempFileName;
        // FileCharacterControl
        /// <summary>
        /// </summary>
        public String ColCharacter;
        //
        /// <summary>
        /// </summary>
        public bool ColQuoteBool = true;
        /// <summary>
        /// </summary>
        public int ColQuoteDefault = (int)ColQuoteIs.ColQuoteDOUBLE;
        /// <summary>
        /// </summary>
        public int ColQuoteType = (int)ColQuoteIs.ColQuoteDOUBLE;
        //
        /// <summary>
        /// </summary>
        public bool ColEscapedBool = false;
        /// <summary>
        /// </summary>
        public int ColEscapedInt = 1;
        /// <summary>
        /// </summary>
        public int ColEscapedDefault = (int)ColEscapedIs.ColEscapedFORBINARY;
        /// <summary>
        /// </summary>
        public int ColEscapedType = (int)ColEscapedIs.ColEscapedFORBINARY;
        #endregion
        /// <summary>
        /// </summary>
        public ColPickDef()
        {
        }
    }

    /// <summary>
    /// <para> Column Type Management Class</para>
    /// <para> Used to set and manage a Column's Type information</para>
    /// </summary>
    public class ColTypeDef
    {
        #region $include Mdm.Oss.File mFile PickDict DictionaryItem Constants
        //This Dict Attr
        /// <summary>
        /// </summary>
        public const int ITEM_ISNOTSET = 1000;

        /// <summary>
        /// </summary>
        public const int ITEM_ISDICT = 256000;

        /// <summary>
        /// </summary>
        public const int ITEM_ISFILE = 2000;
        /// <summary>
        /// </summary>
        public const int ITEM_ISFILEALIAS = 4000;

        /// <summary>
        /// </summary>
        public const int ITEM_ISTYPESAttr = 8000;

        /// <summary>
        /// </summary>
        public const int ITEM_ISFUNCTION = 16000;

        /// <summary>
        /// </summary>
        public const int ITEM_ISUNKNOWN = 32000;

        /// <summary>
        /// </summary>
        public const int ITEM_ISDATA = 64000;

        /// <summary>
        /// </summary>
        public const int ITEM_TYPEError = 128000;

        // This Assication
        // Primary PK
        /// <summary>
        /// </summary>
        public const int INDEX_PK = 100;
        // Allias AK
        /// <summary>
        /// </summary>
        public const int INDEX_AK = 200;
        // Candiate CK
        /// <summary>
        /// </summary>
        public const int INDEX_CK = 800;
        // Defining, Unique defining CK
        /// <summary>
        /// </summary>
        public const int INDEX_DK = 1600;

        // Foriegn FK 400
        // Foriegn keys loosely define Rererential Integrity RI
        /// <summary>
        /// </summary>
        public const int INDEX_FK = 400; //F,Q Pointer
        /// <summary>
        /// </summary>
        public const int INDEX_FK_ACCOUNT = 401; // ACCOUNT
        /// <summary>
        /// </summary>
        public const int INDEX_FK_FILE = 402; // Q File Pointer = Account,CustDict,CustData
        /// <summary>
        /// </summary>
        public const int INDEX_FK_FileData = 404; // Q File ItemData Pointer = Account,CustDict,CustData1
        /// <summary>
        /// </summary>
        public const int INDEX_FK_FileDICT = 408; // Q Dict Pointer = Account,CustDict,Empty
        /// <summary>
        /// </summary>
        public const int INDEX_FK_FileData_Attr = 416; // F:Cust:Name
        #endregion
        #region $include Mdm.Oss.File mFile DictionaryColumn
        /// <summary>
        /// </summary>
        public const int ColumnISNOTSET = 1;
        /// <summary>
        /// </summary>
        public const int ColumnISNUMERIC = 2;
        /// <summary>
        /// </summary>
        public const int ColumnISINTEGER = 4;
        /// <summary>
        /// </summary>
        public const int ColumnISDATE = 8;
        /// <summary>
        /// </summary>
        public const int ColumnISDATETIME = 16;
        /// <summary>
        /// </summary>
        public const int ColumnISCHAR = 32;
        /// <summary>
        /// </summary>
        public const int ColumnISVARCHAR = 64;
        /// <summary>
        /// </summary>
        public const int ColumnISCHARU = 128;
        /// <summary>
        /// </summary>
        public const int ColumnISVARCHARU = 256;
        /// <summary>
        /// </summary>
        public const int ColumnISFLOAT = 512;
        /// <summary>
        /// </summary>
        public const int ColumnISCURRENCY = 1024;
        /// <summary>
        /// </summary>
        public const int ColumnISFUNCTION = 2048;
        /// <summary>
        /// </summary>
        public const int ColumnISUNKNOWN = 4096;
        /// <summary>
        /// </summary>
        public const int ColumnTYPEError = 8192;
        #endregion
        /// <summary>
        /// </summary>
        public ColTypeDef()
        {
        }
    }
    #endregion

    #region Database
    /// <summary>
    /// <para>Database ID fields and Objects.</para>
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
    public class DbIdDef
    {
        // File Database Information - xxxxxxxxxx
        // #region $include Mdm.Oss.File mFile FileBaseDatabase
        #region $include Mdm.Oss.File mFile FileDatabaseName

        // <Area Id = "FileDatabaseInformation">
        #endregion
        #region $include Mdm.Oss.File mFile FileDatabaseSecurity
        #endregion
        #region $include Mdm.Oss.File mFile FileDatabaseUser
        #endregion
        // #endregion

        // <Area Id = "SourceDatabaseFileGroupInformation">

        // <Area Id = "SourceDatabaseFileNameInformation">

        // <Area Id = "DatabaseMessageConstants">

        /// <summary>
        /// </summary>
        protected internal const String SqlConnectionString =
            "Server=localhost;" +
            "DataBase=;" +
            "Integrated Security=SSPI";

        /// <summary>
        /// </summary>
        protected internal const String ConnectionErrorMsg =
            "To run this sample, you must have SQL " +
            "or MSDE with the Northwind database installed.  For " +
            "instructions on installing MSDE, view the ReadMe file.";

        /*
        /// <summary>
        /// </summary>
        protected internal const String MSDE_CONNECTION_STRING =
            @"Server=(local)\NetSDK;" +
            "DataBase=;" +
            "Integrated Security=SSPI";
        */
        // <Area Id = "FileDatabaseInformation">

        // See DATABASE REGION FOR THESE FIELDS

        /// <summary>
        /// </summary>
        public void DataClear()
        {
        }
    }

    /// <summary>
    /// <para> Database Status flags</para>
    /// <para> Contains database and connection
    /// specific flags to indicate status.</para>
    /// <para> Note: File Status flag instances are
    /// used for other item and file status needs.</para>
    /// </summary>
    public class DbStatusDef
    {
        /// <summary>
        /// </summary>
        public DbStatusDef() { DataClear(); }
        // File Database Connection - xxxxxxxxxxx
        #region $include Mdm.Oss.File mFile FileConnection_ControlFields
        // property bool ConnIsInitialized
        /// <summary>
        /// </summary>
        protected internal bool bpConnIsInitialized;
        /// <summary>
        /// </summary>
        public bool ConnIsInitialized
        {
            get { return bpConnIsInitialized; }
            set
            {
                bpConnIsInitialized = value;
            }
        } //   
        // <Area Id = "ConnDoesExist">
        /// <summary>
        /// </summary>
        protected internal bool bpConnDoesExist;
        /// <summary>
        /// </summary>
        public bool ConnDoesExist
        {
            get { return bpConnDoesExist; }
            set
            {
                bpConnDoesExist = value;
            }
        }
        // <Area Id = "SourceDatabaseInformation">
        /// <summary>
        /// </summary>
        public long ipDoesExistResult;
        // property long ConnResult 
        /// <summary>
        /// </summary>
        protected internal long ipIsConnectingResult;
        /// <summary>
        /// </summary>
        public long ConnResult
        {
            get { return ipIsConnectingResult; }
            set
            {
                ipIsConnectingResult = value;
            }
        } //   
        // property bool ConnIsValid
        /// <summary>
        /// </summary>
        protected internal bool bpConnIsValid;
        /// <summary>
        /// </summary>
        public bool ConnIsValid
        {
            get { return bpConnIsValid; }
            set
            {
                bpConnIsValid = value;
            }
        } //   
        // property bool ConnIsValid
        // property bool ConnHadError
        /// <summary>
        /// </summary>
        protected internal bool bpConnHadError;
        /// <summary>
        /// </summary>
        public bool ConnHadError
        {
            get { return bpConnHadError; }
            set
            {
                bpConnHadError = value;
            }
        } //   
        // property bool ConnIsCreating
        /// <summary>
        /// </summary>
        protected internal bool bpConnIsCreating;
        /// <summary>
        /// </summary>
        public bool ConnIsCreating
        {
            get { return bpConnIsCreating; }
            set
            {
                bpConnIsCreating = value;
            }
        }
        /// <summary>
        /// </summary>
        protected internal bool bpConnIsCreated;
        /// <summary>
        /// </summary>
        public bool ConnIsCreated
        {
            get { return bpConnIsCreated; }
            set
            {
                bpConnIsCreated = value;
            }
        }
        /// <summary>
        /// </summary>
        protected internal bool bpConnIsConnecting;
        /// <summary>
        /// </summary>
        public bool ConnIsConnecting
        {
            get { return bpConnIsConnecting; }
            set
            {
                bpConnIsConnecting = value;
            }
        }
        /// <summary>
        /// </summary>
        protected internal bool bpConnIsConnected;
        /// <summary>
        /// </summary>
        public bool ConnIsConnected
        {
            get { return bpConnIsConnected; }
            set
            {
                bpConnIsConnected = value;
            }
        }
        /// <summary>
        /// </summary>
        protected internal bool bpConnIsOpen;
        /// <summary>
        /// </summary>
        public bool ConnIsOpen
        {
            get { return bpConnIsOpen; }
            set
            {
                bpConnIsOpen = value;
            }
        }
        /// <summary>
        /// </summary>
        protected internal bool bpConnIsClosed;
        /// <summary>
        /// </summary>
        public bool ConnIsClosed
        {
            get { return bpConnIsClosed; }
            set
            {
                bpConnIsClosed = value;
            }
        }
        // property bool ConnDoDispose
        /// <summary>
        /// </summary>
        protected internal bool bpConnDoDispose;
        /// <summary>
        /// </summary>
        public bool ConnDoDispose
        {
            get { return bpConnDoDispose; }
            set
            {
                bpConnDoDispose = value;
            }
        } //
        // property bool ConnDoKeepOpen
        /// <summary>
        /// </summary>
        protected internal bool bpConnDoKeepOpen;
        /// <summary>
        /// </summary>
        public bool ConnDoKeepOpen
        {
            get { return bpConnDoKeepOpen; }
            set
            {
                bpConnDoKeepOpen = value;
            }
        } //
        // Database
        /// <summary>
        /// </summary>
        protected internal bool bpNameIsValid;
        /// <summary>
        /// </summary>
        public bool DatabaseNameIsValid
        {
            get { return bpNameIsValid; }
            set
            {
                bpNameIsValid = value;
            }
        } //   
        // <Area Id = "FileGroupStatus">
        /// <summary>
        /// </summary>
        public bool bpDbFileGroupDoesExist;
        /// <summary>
        /// </summary>
        public bool bpDbFileGroupIsValid;
        /// <summary>
        /// </summary>
        public bool bpDbFileGroupIsCreating;
        /// <summary>
        /// </summary>
        public bool bpDbFileGroupIsCreated;
        #endregion
        /// <summary>
        /// </summary>
        public void DataClear()
        {
            ipIsConnectingResult = (long)StateIs.None;
            bpConnDoKeepOpen = true;
            bpConnDoDispose = true;

            bpConnDoesExist = false;
            ipDoesExistResult = (long)StateIs.None;
            bpConnHadError = false;

            bpConnIsClosed = true;
            bpConnIsConnected = false;
            bpConnIsConnecting = false;
            bpConnIsCreated = false;
            bpConnIsCreating = false;
            bpConnIsInitialized = false;
            bpConnIsOpen = false;
            bpConnIsValid = false;

            bpDbFileGroupDoesExist = false;
            bpDbFileGroupIsCreated = false;
            bpDbFileGroupIsCreating = false;
            bpDbFileGroupIsValid = false;
        }
    }

    /// <summary>
    /// <para>Master File Class for Database</para>
    /// <para> A separate database object for performing
    /// actions on the master files independent of the
    /// current open database file stream object.</para>
    /// </summary>
    public class DbMasterDef
    {
        // OBJECT SUBCLASS - MASTER FILE
        // Database Master File - xxxxxxxxxxxxxxx
        #region $include Mdm.Oss.File mFile FileMasterServer
        // <Area Id = "FileCommand">
        #region $include Mdm.Oss.File mFile FileMaster_Security_Fields

        // <Area Id = "SecurityControl">
        // property bool UseSSIS
        /// <summary>
        /// </summary>
        protected internal bool bpUseSSIS = true;
        /// <summary>
        /// </summary>
        public bool UseSSPI
        {
            get { return bpUseSSIS; }
            set { bpUseSSIS = value; }
        } //   

        /// <summary>
        /// </summary>
        protected internal String MstrSecurityId = "99999";
        /// <summary>
        /// </summary>
        protected internal String MstrSecurity;

        #endregion
        #region $include Mdm.Oss.File mFile FileMaster_User_Control

        // <Area Id = "UserControl">
        /// <summary>
        /// </summary>
        protected internal String MstrUserServerId = "99999";
        /// <summary>
        /// </summary>
        protected internal String MstrUserDbId = "99999";
        /// <summary>
        /// </summary>
        protected internal String MstrUserId = "99999";
        /// <summary>
        /// </summary>
        protected internal String MstrUser = "MdmUser99";
        /// <summary>
        /// </summary>
        protected internal String MstrUserPw = "password99";


        // <Area Id = "UserStatus">
        /// <summary>
        /// </summary>
        protected internal bool UserDoesExist = false;
        /// <summary>
        /// </summary>
        protected internal bool UserIsInvalid = false;
        /// <summary>
        /// </summary>
        protected internal bool UserIsCreating = false;
        /// <summary>
        /// </summary>
        protected internal bool UserIsCreated = false;
        #endregion
        #region $include Mdm.Oss.File mFile FileMaster_Server_Database
        // <Area Id = "MasterServerInformation">

        // <Area Id = "MasterServerDatabase">
        //
        /// <summary>
        /// </summary>
        protected internal String MstrDbSystem = @"";
        /// <summary>
        /// </summary>
        protected internal String MstrDbSystemMdm = @"MDMPC13";
        /// <summary>
        /// </summary>
        // protected internal String MstrDbSystemDefault = "localhost";
        /// <summary>
        /// </summary>
        protected internal String MstrDbSystemDefault = @""; // SYSTEM99
        /// <summary>
        /// </summary>
        protected internal String MstrDbSystemDefaultMdm = @"MDMPC13";
        //
        /// <summary>
        /// </summary>
        // protected internal String MstrDbService = "SQLSERVER";
        /// <summary>
        /// </summary>
        protected internal String MstrDbService = @""; // SQLSERVER
        /// <summary>
        /// </summary>
        protected internal String MstrDbServiceMdm = @"SQLEXPRESS";
        /// <summary>
        /// </summary>
        // protected internal String MstrDbServiceDefault = "SQLSERVER";
        /// <summary>
        /// </summary>
        protected internal String MstrDbServiceDefault = @""; // SERVICE99
        /// <summary>
        /// </summary>
        protected internal String MstrDbServiceDefaultMdm = @"SQLEXPRESS";
        //
        /// <summary>
        /// </summary>
        // protected internal String MstrDbServer = "localhost";
        /// <summary>
        /// </summary>
        protected internal String MstrDbServer = @"";
        /// <summary>
        /// </summary>
        // protected internal String MstrDbServerMdm = "MdmServer99";
        /// <summary>
        /// </summary>
        protected internal String MstrDbServerMdm = @"MDMPC13\SQLEXPRESS";
        /// <summary>
        /// </summary>
        // protected internal String MstrDbServerDefault = "localhost";
        /// <summary>
        /// </summary>
        protected internal String MstrDbServerDefault = @"SERVER99";
        /// <summary>
        /// </summary>
        // protected internal String MstrDbServerDefaultMdm = "MdmServer99";
        /// <summary>
        /// </summary>
        protected internal String MstrDbServerDefaultMdm = @"MDMPC13\SQLEXPRESS";
        //
        /// <summary>
        /// </summary>
        protected internal String MstrDbServerId = "99999";
        /// <summary>
        /// </summary>
        // protected internal String MstrDbServerMasterDefault = "master..sysdatabases";
        /// <summary>
        /// </summary>
        // protected internal String MstrDbServerMasterDefaultMdm = "MdmServer99..sysdatabases";
        /// <summary>
        /// </summary>
        protected internal String MstrDbServerMasterDefault = @"SERVER99";
        /// <summary>
        /// </summary>
        protected internal String MstrDbServerMasterDefaultMdm = @"MDMPC13\SQLEXPRESS..sysdatabases";
        //
        // <Area Id = "MasterServerFilesLocation">
        /*
        protected internal String MstrDbMasterFile = "localhost.dbo.sysobjects";
        protected internal String MstrDbMasterFileMdm = "MdmServer99.dbo.sysobjects";
        protected internal String MstrDbMasterFileDefault = "localhost.dbo.sysobjects";
        protected internal String MstrDbMasterFileDefaultMdm = "MdmServer99.dbo.sysobjects";
        */
        /*
        protected internal String MstrDbMasterFile = "localhost..sysobjects";
        protected internal String MstrDbMasterFileMdm = "MdmServer99..sysobjects";
        protected internal String MstrDbMasterFileDefault = "localhost..sysobjects";
        protected internal String MstrDbMasterFileDefaultMdm = "MdmServer99..sysobjects";
        */
        /// <summary>
        /// </summary>
        protected internal String MstrDbMasterFile = @"MDMPC13\SQLEXPRESS..sysobjects";
        /// <summary>
        /// </summary>
        protected internal String MstrDbMasterFileMdm = @"MDMPC13\SQLEXPRESS....sysobjects";
        /// <summary>
        /// </summary>
        protected internal String MstrDbMasterFileDefault = @"MDMPC13\SQLEXPRESS....sysobjects";
        /// <summary>
        /// </summary>
        protected internal String MstrDbMasterFileDefaultMdm = @"MDMPC13\SQLEXPRESS....sysobjects";

        // <Area Id = "MasterServer - ServerControl">

        // <Area Id = "MasterServer - Connection">
        /// <summary>
        /// </summary>
        protected internal String MstrCmd = "not used";
        // <Area Id = "MasterServer - Creation">
        /// <summary>
        /// </summary>
        protected internal String MstrDbServerCreateCmd;

        // <Area Id = "MasterServer - DatabaseControl">
        /// <summary>
        /// </summary>
        protected internal String MstrDbDatabaseId = "99999";
        /// <summary>
        /// </summary>
        // protected internal String MstrDbDatabase = "dbo";
        /// <summary>
        /// </summary>
        protected internal String MstrDbDatabase = "";
        /// <summary>
        /// </summary>
        protected internal String MstrDbDatabaseDefault = "Database99";
        /// <summary>
        /// </summary>
        protected internal String MstrDbDatabaseDefaultMdm = "MdmDatabase99";

        // <Area Id = "MasterServer - FileGroupControl">
        /// <summary>
        /// </summary>
        protected internal String MstrDbFileGroupId = "";
        /// <summary>
        /// </summary>
        protected internal String MstrDbFileGroup = "";
        /// <summary>
        /// </summary>
        protected internal String MstrDbFileGroupDefault = "";
        /// <summary>
        /// </summary>
        protected internal String MstrDbFileGroupDefaultMdm = "";

        // <Area Id = "MasterServer - OwnerControl">
        /// <summary>
        /// </summary>
        protected internal String MstrDbOwnerId = "99999";
        // protected internal String MstrDbOwner = "dbo";
        // protected internal String MstrDbOwnerDefault = "sa";
        // protected internal String MstrDbOwnerDefaultMdm = "MdmOwner99";
        /// <summary>
        /// </summary>
        protected internal String MstrDbOwner = "dbo";
        // protected internal String MstrDbOwnerDefault = "sa";
        /// <summary>
        /// </summary>
        protected internal String MstrDbOwnerDefault = "dbo";
        /// <summary>
        /// </summary>
        protected internal String MstrDbOwnerDefaultMdm = "MdmOwner99";

        /// <summary>
        /// </summary>
        protected internal String MstrDbTable = @"MDMPC13\SQLEXPRESS..sysobjects";
        /// <summary>
        /// </summary>
        protected internal String MstrDbTableMdm = @"MDMPC13\SQLEXPRESS....sysobjects";
        /// <summary>
        /// </summary>
        protected internal String MstrDbTableDefault = @"MDMPC13\SQLEXPRESS....sysobjects";
        /// <summary>
        /// </summary>
        protected internal String MstrDbTableDefaultMdm = @"MDMPC13\SQLEXPRESS....sysobjects";

        /// <summary>
        /// </summary>
        protected internal String MstrDbFile = @"MDMPC13\SQLEXPRESS..sysobjects";
        /// <summary>
        /// </summary>
        protected internal String MstrDbFileMdm = @"MDMPC13\SQLEXPRESS....sysobjects";
        /// <summary>
        /// </summary>
        protected internal String MstrDbFileDefault = @"MDMPC13\SQLEXPRESS....sysobjects";
        /// <summary>
        /// </summary>
        protected internal String MstrDbFileDefaultMdm = @"MDMPC13\SQLEXPRESS....sysobjects";

        // <Area Id = "MasterConnectionCommand">

        // <Area Id = "MasterDatabase - Connection">
        // protected internal String MstrConnString;
        /// <summary>
        /// </summary>
        protected internal String MstrConnString = Mdm.Oss.File.DbIdDef.SqlConnectionString;

        // <Area Id = "MasterDatabaseStatus">
        /// <summary>
        /// </summary>
        protected internal bool MstrDbDatabaseIsInitialized = false;
        /// <summary>
        /// </summary>
        protected internal bool MstrDbDatabaseDoesExist = false;
        /// <summary>
        /// </summary>
        protected internal bool MstrDbDatabaseIsInvalid = false;
        /// <summary>
        /// </summary>
        protected internal bool MstrDbDatabaseIsCreating = false;
        /// <summary>
        /// </summary>
        protected internal bool MstrDbDatabaseIsCreated = false;

        // <Area Id = "EndOfMasterServerAndDatabase">

        #endregion
        #region $include Mdm.Oss.File mFile FileMaster_FileGroup
        // <Area Id = "FileGroup">

        /// <summary>
        /// </summary>
        protected internal String spMstrDbFileGroupServerId = "99999";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFileGroupDbId = "99999";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFileGroupId = "99999";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFileGroup = "MdmFileGroup99";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFileGroupDefault = "MdmFileGroup99";
        // <Area Id = "FileGroupCommand">
        /// <summary>
        /// </summary>
        protected internal String spMDbFileGroupCreateCmd = "not used";
        #endregion
        #region $include Mdm.Oss.File mFile FileMaster_DbFile

        // <Area Id = "MasterFile">
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFileDbId = "99999";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFileDb = "MdmDatabase99";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFileDbDefault = "MdmDatabase99";

        /// <summary>
        /// </summary>
        protected internal String spMstrDbFileId = "99999";
        /// <summary>
        /// </summary>
        // protected internal String spMstrDbFile = "MdmFile99";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFile = "INFORMATION_SCHEMA.TABLES";
        /// <summary>
        /// </summary>
        // protected internal String spMstrDbFile = "sys.objects";

        // FmainPassed.DbIo.CommandCurrent = "USE[" + Fs.DatabaseName + "]; SELECT * FROM 
        // INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " + "'" + FileId.spFileNameFull + "';";
        // FmainPassed.DbIo.CommandCurrent = "USE[" + Fs.DatabaseName + "]; SELECT * FROM 
        // sys.objects WHERE name = " + "'" + FileId.spFileNameFull + "';";
        // SQL = "SELECT * FROM 
        // INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=MyTable";

        // <Area Id = "MstrDbFileStatus">
        /// <summary>
        /// </summary>
        public bool bpMstrDbFileDoesExist = false;
        /// <summary>
        /// </summary>
        public bool bpMstrDbFileIsInvalid = false;
        /// <summary>
        /// </summary>
        public bool bpMstrDbFileIsCreating = false;
        /// <summary>
        /// </summary>
        public bool bpMstrDbFileIsCreated = false;

        #endregion
        // OBJECT SUBCLASS - MASTER FILE CONNECTION
        // Database Master File Connection - xxxx
        #region $include Mdm.Oss.File mFile FileMaster_DbFileConnection
        // <Area Id = "MstrDbFileConnectionCreationCommand">
        /// <summary>
        /// </summary>
        protected internal String MstrDbConnCreateCmd;

        // <Area Id = "MstrDbFileConnStatus">
        /// <summary>
        /// </summary>
        protected internal int ipMstrDbFileConnStatus = -99999;
        /// <summary>
        /// </summary>
        public int iMstrDbFileConnStatus
        {
            get { return ipMstrDbFileConnStatus; }
            set { ipMstrDbFileConnStatus = value; }
        }
        #endregion
        #endregion

        /// <summary>
        /// </summary>
        public DbMasterDef()
        {
        }
    }
    #endregion

    #region Phrase construction
    /// <summary>
    /// <para> Master File Syntax Object</para>
    /// <para> Used for building console commands
    /// for the master database.</para>
    /// <para>Commands include Create, Delete.</para>
    /// </summary>
    public class DbMasterSynDef
    {
        // OBJECT SUBCLASS - COMMAND PHRASES
        // <Area Id = "MasterDatabase - Creation">
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFileCreateCmd;
        /// <summary>
        /// </summary>
        protected internal String MstrDbDatabaseCreateCmd;

        #region $include Mdm.Oss.File mFile FileDatabasePhrase_Constrution
        // <Area Id = "Phrases">
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseDoLine = "; ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseExecute = "GO"; // wrong
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseDoExecute = "GO"; // wrong

        #region $include Mdm.Oss.File mFile FileMasterServerAndDatabasePhrases
        // <Area Id = "MasterServerAndDatabasePhrases">

        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseServer;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseDatabase;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseSecurity;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseUser;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseUserPw;


        // <Area Id = "CreationPhrases">

        // <Area Id = "MasterDatabase - CreationPhrases">

        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhrase;

        /// <summary>
        /// </summary>
        public bool bpDbPhraseIfIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseIf = "IF EXISTS (";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseIfEnd = ") ";

        /// <summary>
        /// </summary>
        public bool bpDbPhraseSelectIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseSelect = "SELECT * ";

        /// <summary>
        /// </summary>
        public bool bpDbPhraseFromIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseFrom = "FROM ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseFromItems = "MdmServer99..sysdatabases";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseFromEnd = " ";

        /// <summary>
        /// </summary>
        public bool bpDbPhraseWhereIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseWhere = "WHERE ";
        // <Area Id = "sb paired list of dict + value
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseWhereItems = "Name = 'HowToDemo'";

        /// <summary>
        /// </summary>
        public bool bpDbPhraseDropIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseDrop = "DROP ";
        // <Area Id = "sb paired list of dict + value
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseDropItems = "DATABASE HowToDemo";

        /// <summary>
        /// </summary>
        public bool bpDbPhraseCreateIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseCreate = "CREATE ";
        // <Area Id = "sb paired list of dict + value
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseCreateItems = "CREATE DATABASE HowToDemo";

        /// <summary>
        /// </summary>
        protected internal bool spMstrDbPhraseFileGroupIsUsed = false;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbPhraseFileGroup = "HowToDemoFileGroup";
        #endregion
        #region $include Mdm.Oss.File mFile FileCreationPhrases
        // <Area Id = "FileCreationPhrases">

        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhrase;

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseUseIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseUse = "USE ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseUseEnd;

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseIfIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseIf = "IF EXISTS (";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseIfEnd = ")";

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseSelectIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseSelect = "SELECT * ";

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseFromIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseFrom = "FROM ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseFromItems = "master..sysdatabases";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseFromEnd = " ";

        // property bool DbFilePhraseWhereIsUsed
        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseWhereIsUsed = false;
        /// <summary>
        /// </summary>
        public bool DbFilePhraseWhereIsUsed
        {
            get { return bpDbFilePhraseWhereIsUsed; }
            set { bpDbFilePhraseWhereIsUsed = value; }
        } //  this is a sample code snippet

        // Master Phrases

        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseWhere = "WHERE ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseWhereAnd = " AND ";
        // <Area Id = "sb paired list of dict + value">
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseWhereItems1 = "Name = 'HowToDemo'";
        // <Area Id = "sb paired list of dict + value">
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseWhereItems2 = "TYPE = 'u'";
        // spMstrDbFilePhraseWhereItemsId[X];
        // spMstrDbFilePhraseWhereItemsExpression[X];
        // spMstrDbFilePhraseWhereItemsValue[X];

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseBeginIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseBegin = "BEGIN";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseBeginEnd = "END";

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseDropIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseDrop = "DROP ";
        // <Area Id = "sb paired list of dict + value">
        // protected internal String spMstrDbFilePhraseDropItems = "TABLE " + "HowToDemo.dbo.MdmFile99";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseDropItems = "TABLE " + "HowToDemo..MdmFile99";


        // <Section Id = "x">

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseCreateIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseCreate = "CREATE ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseCreateObject = "TABLE ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseCreateTable = "MdmFile99";
        // + "HowToDemo.dbo.This";Table

        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseItemsBegin = "(";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseItemsEnd = ")";

        // <Area Id = "spMstrDbFilePhraseDColumnId[X] 
        // spMstrDbFilePhraseWhereItemsType[X]; 
        // spMstrDbFilePhraseWhereItemsTypeHasLength[X];
        // if (bpDbFilePhraseCreateIsUsed)
        // {
        // + "("
        // spMstrDbFilePhraseWhereItemsTypeLength[X];
        // + ")"
        // }
        // + " "
        // spMstrDbFilePhraseWhereItemsRange[X];
        // "NOT NULL "">


        // <Section Id = "x">

        /// <summary>
        /// </summary>
        public bool bpDbFilePhraseConstraintIsUsed = true;
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraint = "CONSTRAINT [";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintCol = "PK_Numeric99";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintNameDefault = "PK_Numeric99";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintEnd = "]";

        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintType1 = " PRIMARY KEY ";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintType2 = " CLUSTERED ";

        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintColBegin = " (";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintColName = "Column0";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintColNameDefalut = "Column0";
        /// <summary>
        /// </summary>
        protected internal String spMstrDbFilePhraseConstraintColEnd = ")";


        /// <summary>
        /// </summary>
        public String spMstrDbFilePhraseCreateItems = "CREATE DATABASE HowToDemo";
        #endregion
        #endregion
        /// <summary>
        /// </summary>
        public DbMasterSynDef()
        {
        }

        /// <summary>
        /// </summary>
        public void DataClear()
        {
            bpDbFilePhraseSelectIsUsed = false;
        }
    }

    /// <summary>
    /// <para> Database File Syntax Object</para>
    /// <para> Used for building console commands
    /// for the database.</para>
    /// <para>Command include Create, Add, Alter, Insert, Update, Delete.</para>
    /// </summary>
    public class DbSynDef
    {
        // SYNTAX - xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.File mFile SqlSyntax
        // property String DatabaseFileCreateCmd
        /// <summary>
        /// </summary>
        protected internal int ipDatabaseCmdType;
        /// <summary>
        /// </summary>
        public int DatabaseCmdType
        {
            get { return ipDatabaseCmdType; }
            set { ipDatabaseCmdType = value; }
        } //   

        // property String DatabaseFileCreateCmd
        /// <summary>
        /// </summary>
        protected internal String spDatabaseFileCreateCmd;
        /// <summary>
        /// </summary>
        public String DatabaseFileCreateCmd
        {
            get { return spDatabaseFileCreateCmd; }
            set { spDatabaseFileCreateCmd = value; }
        } //   

        // property String ConnCreateCmd
        /// <summary>
        /// </summary>
        protected internal String spConnCreateCmd;
        /// <summary>
        /// </summary>
        public String sConnCreateCmd
        {
            get { return spConnCreateCmd; }
            set
            {
                spConnCreateCmd = value;
            }
        } //   

        // property String OutputCommand
        /// <summary>
        /// </summary>
        public String spOutputCommand;
        /// <summary>
        /// </summary>
        public String OutputCommand
        {
            get { return spOutputCommand; }
            set { spOutputCommand = value; }
        } //   
        #region $include Mdm.Oss.File mFile Sql File Create Commands
        // Sql File Command
        /// <summary>
        /// </summary>
        public String spSqlCreateCmdScript;
        /// <summary>
        /// </summary>
        public String spSqlCreateCmd;
        /// <summary>
        /// </summary>
        public String spSqlFileDeleteCmd;
        /// <summary>
        /// </summary>
        public String spSqlFileAlterCmd;
        /// <summary>
        /// </summary>
        public String spSqlFileViewCmd;
        #endregion
        #region $include Mdm.Oss.File mFile Sql File Column Commands
        // Sql File Column Commands
        /// <summary>
        /// </summary>
        public String spSqlColumnAddCmdScript;
        /// <summary>
        /// </summary>
        public String spSqlColumnAddCmd;

        /// <summary>
        /// </summary>
        public String spSqlColumnDeleteCmd;

        /// <summary>
        /// </summary>
        public String spSqlColumnAlterCmd;
        // spOutputViewCommand
        /// <summary>
        /// </summary>
        protected internal String spSqlColumnViewScript = "SCRIPT to create a view of a TABLE ";
        /// <summary>
        /// </summary>
        protected internal bool bpSqlColumnViewCmdFirst = true;
        /// <summary>
        /// </summary>
        public String spSqlColumnViewCmdPrefix;
        /// <summary>
        /// </summary>
        public String spSqlColumnViewCmd;
        /// <summary>
        /// </summary>
        public String spSqlColumnViewCmdSuffix;

        // property String OutputReadCommand
        /// <summary>
        /// </summary>
        public String spOutputReadCommand;
        /// <summary>
        /// </summary>
        public String spOutputWriteCommand;
        /// <summary>
        /// </summary>
        public String OutputReadCommand
        {
            get { return spOutputReadCommand; }
            set { spOutputReadCommand = value; }
        }
        //   
        // property String OutputInsertCommand
        /// <summary>
        /// </summary>
        public String spOutputInsertCommand;
        /// <summary>
        /// </summary>
        public String OutputInsertCommand
        {
            get { return spOutputInsertCommand; }
            set { spOutputInsertCommand = value; }
        } //   
        #endregion
        #region $include Mdm.Oss.File mFile Sql Command Output
        // Sql Command Output
        /// <summary>
        /// </summary>
        protected internal String spOutputInsertPrefix = "INSERT INTO ";
        /// <summary>
        /// </summary>
        protected internal String spOutputInsertPrefix1 = " (";

        // property String OutputInsert
        /// <summary>
        /// </summary>
        public String spOutputInsert;
        /// <summary>
        /// </summary>
        public String OutputInsert
        {
            get { return spOutputInsert; }
            set { spOutputInsert = value; }
        } //   

        /// <summary>
        /// </summary>
        protected internal String spOutputInsertScript = "SCRIPT TO Insert into TABLE ";
        /// <summary>
        /// </summary>
        protected internal bool bpOutputInsertScriptFirst = true;
        /// <summary>
        /// </summary>
        protected internal String spOutputInsertSuffix = ")";
        //
        /// <summary>
        /// </summary>
        protected internal String spOutputUpdateScript = "UPDATE";
        /// <summary>
        /// </summary>
        protected internal bool bpOutputUpdateScriptFirst = true;
        /// <summary>
        /// </summary>
        protected internal String spOutputUpdatePrefix = "UPDATE";
        //
        /// <summary>
        /// </summary>
        protected internal String spOutputDeleteScript = "DELETE";
        /// <summary>
        /// </summary>
        protected internal bool bpOutputDeleteScriptFirst = true;
        /// <summary>
        /// </summary>
        protected internal String spOutputDeletePrefix = "DELETE";
        //
        /// <summary>
        /// </summary>
        protected internal String spOutputValuesScript = "VALUES (";
        /// <summary>
        /// </summary>
        protected internal bool bpOutputValuesScriptFirst = true;
        /// <summary>
        /// </summary>
        protected internal String spOutputValuesPrefix = "VALUES (";

        // property String OutputValues
        /// <summary>
        /// </summary>
        public String spOutputValues;
        /// <summary>
        /// </summary>
        public String OutputValues
        {
            get { return spOutputValues; }
            set
            {
                spOutputValues = value;
                OutputValuesSet = true;
            }
        } //   

        /// <summary>
        /// </summary>
        public bool OutputValuesSet = false;

        /// <summary>
        /// </summary>
        protected internal String spOutputValuesSuffix = ")";
        //
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterScript = "SCRIPT TO ALTER TABLE ";
        /// <summary>
        /// </summary>
        protected internal bool bpOutputAlterScriptFirst = true;
        //
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterCommand = "ALTER TABLE ";
        //
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterPrefix = "ALTER TABLE ";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterVerb = "ADD";
        /// <summary>
        /// </summary>
        /// <summary>
        /// </summary>
        protected internal bool bpOutputAlterColumnNameQuoted = false;
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterQuoteChar = "'";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterQuoteCharLeft = "[";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterQuoteCharRight = "]";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnNameSource = "1";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnNameAlias = "1";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnName = "1";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnType = "VARCHAR";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnTypePrefix = "(";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnLength = "50";
        /// <summary>
        /// </summary>
        protected internal int ipOutputAlterColumnLength = 50;
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnTypeSuffix = ")";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterColumnNull = " NULL";
        /// <summary>
        /// </summary>
        protected internal String spOutputAlterListSeparatorChar = ",";
        #endregion
        #region $include Mdm.Oss.File mFile Create Database and Database Objects
        // "CREATE DATABASE"
        // "CREATE DEFAULT"
        // "CREATE PROC"
        // "CREATE RULE"
        // "CREATE TRIGGER"
        // "CREATE VIEW"
        // "CREATE SCHEMA"
        // "CREATE PARTITION FUNCTION"
        // "CREATE PARTITION SCHEME"
        // /* Comment text */
        // -- Comment text
        // /* Comment block */

        // @@GLOBALS
        // @@CURSOR
        // @@SYSTEM
        // @@SYSTEM STATISTICAL
        // SYSTEM FUNCTIONS()
        // AGGREGATION()
        // CHECKSUM()
        // CAST() CONVERT()
        // CURSOR_STATUS()
        // DATE()
        // IMAGE TEXTVALID()
        // MATH()
        // METADATA()
        // RANK()
        // SECURITY SUserSID()
        // STRING MANIPULATION()

        // ACTIVE DIRECTORY
        // CATALOG
        // CURSOR MANAGEMENT
        // DATABASE ENGINE
        // DATABASE MAINTENANCE
        // DATABASE QUERIES
        // EXTERNAL SYSTEMS
        // EXTENDED PROCEDURES
        // FULE-TEXT INDEX / SEARCH
        // LOG SHIPPING
        // MAIL
        // NOTIFICATION SERVICES
        // OLE AUTOMATION
        // PROFILER

        // REPLICATION
        // SECURITY
        // SQL AGENT


        // "ENABLE"
        // "DISABLE"

        // "DECLARE"
        // "DECLARE" "@"
        // "SET"
        // "LIKE"

        // "WITH" "AS"
        // "SELECT"
        // "TOP"
        // "SELECT" "INTO"
        // "SELECT" "FROM"
        // "SELECT" "FROM" "WHERE"

        // "GROUP BY"
        // "HAVING"
        // "UNION"

        // "EXCEPT"
        // "INTERCEPT"

        // "ORDER BY"
        // "COMPUTER BY"

        // "FOR"
        // "OPTION"
        // "CASE"
        #endregion
        #endregion
        /// <summary>
        /// </summary>
        public DbSynDef()
        {
        }
    }
    #endregion

    #region Pick Objects
    /// <summary>
    /// <para> Pick Dictionary Item Array Index</para>
    /// <para> An indexed array of ColumnAliasMax elements
    /// that contains the dictionary entry items (records.)
    /// It is used to build or manipulate a Pick dictionary.</para>
    /// </summary>
    /// <remarks>
    /// Usage needs clarification
    /// </remarks>
    public class PickDictItemArrayDef
    {
        public static int PdIndexMax = (int)ArrayMax.ColumnAliasMax;
        public static int PdIndexMaxNew = (int)ArrayMax.ColumnAliasMax + 1; // Used in the new

        public PickDictItemDef[] aPickDictArray = new PickDictItemDef[PdIndexMaxNew];
        public int PdaIndex;
        public PickDictItemDef this[int PdaIndexPassed]
        {
            get
            {
                PdArrayCheck(ref PdaIndexPassed);
                return aPickDictArray[PdaIndex];
            }
            set
            {
                PdArrayCheck(ref PdaIndexPassed);
                aPickDictArray[PdaIndex] = value;
            }
        }

        public PickDictItemArrayDef()
        {
            aPickDictArray = new PickDictItemDef[PdIndexMaxNew];
            PdaIndex = 0;
        }

        public void PdArrayCheck(ref int PdaIndexPassed)
        {
            PdaIndex = PdaIndexPassed;
            if (PdaIndex < 0)
            {
                PdaIndex = 0;
                // TODO Exception Index Error, out of range (below zero)
            }
            if (PdaIndex > (int)ArrayMax.ColumnAliasMax)
            {
                PdaIndex = (int)ArrayMax.ColumnAliasMax;
                // TODO Exception Index Error, out of range (greater than maximum allowed)
            }
            if (aPickDictArray[PdaIndex] == null)
            {
                aPickDictArray[PdaIndex] = new PickDictItemDef();
            }
        }
    }

    /// <summary>
    /// <para> Pick Dictionary Row</para>
    /// <para> An indexed array of ColumnAliasMax elements</para>
    /// </summary>
    /// <remarks>
    /// Usage needs clarification
    /// </remarks>
    public class PickRowDef
    {
        #region $include Mdm.Oss.File mFile PickDictControl
        // Index Key for array and relative row number
        private int ipPdIndex;
        public int PdIndex
        {
            get { return ipPdIndex; }
            set { ipPdIndex = value; }
        }

        public static int PdIndexMax = (int)ArrayMax.ColumnAliasMax;
        public static int PdIndexMaxNew = (int)ArrayMax.ColumnAliasMax + 1; // Used in the new
        public int PdIndexHigh;
        public int PdIndexAliasLow;
        //
        // public PickDictItemDef[] PickDictArray = new PickDictItemDef[PdIndexMaxNew];
        public PickDictItemArrayDef PickDictArray = new PickDictItemArrayDef();
        //
        public int iAttrType;
        // Extracted from ItemId (0) Dictionary PK / Alias key
        public int PdIndexItemId;
        public bool ItemIdIsNumeric = true;
        public String ItemId;
        public int ItemIntId;
        // Extracted from AttrTwo (2) Dictionary Attr
        public String AttrTwoString;
        public int AttrTwoInt;
        public bool AttrTwoIsNumeric = false;
        //
        public int PdIndexAttrTwo;
        //
        public bool ColumnInvalid = false;
        public int DictColumnTouched;
        // Account name can be equivalent to
        // Account indicating another Company
        // Account indicating another System
        public String AttrTwoStringAccounName;
        // (10)
        // Column Width
        public String ColumnWidthString;
        public int ColumnWidth;
        public bool ColumnWidthIsNumeric;
        //
        // Attr three can be a
        // File Name
        // File Name Dict, File ItemData File
        // File Dict without data
        public String AttrThreeFileName;
        //
        public bool ItemIdFoundNumericPk = false;
        public bool DictColumnIdDone = false;

        private int ipPdIndexTemp;
        public int PdIndexTemp
        {
            get
            {
                return ipPdIndexTemp;
            }
            set
            {
                ipPdIndexTemp = value;
            }
        }

        public int PdItemCount;

        public int PdErrorCount;
        public int PdErrorWarningCount;

        public bool PdIndexDoSearch = true;
        public int ColumnDataPoints;
        #endregion

        public PickRowDef() { }

        public void DataClear()
        {
            // Pick Dictionary
            PdIndex = 0;
            PdItemCount = 0;
        }

        public void RowDataClear(int PdIndexPassed)
        {
            // if (PickDictArray[PdIndex] == null) { PickDictArray[PdIndex] = new PickDictItemDef(); } 
            PickDictArray[PdIndexPassed].ItemId = "";
            PickDictArray[PdIndexPassed].ItemIntId = 0;
            PickDictArray[PdIndexPassed].iItemAttrIndex = 0;  // Field being processed in the Dict Column
            //
            PickDictArray[PdIndexPassed].ColIndex = 0;
            PickDictArray[PdIndexPassed].iItemAttrCounter = 0; // TableOpen  How many Fields (size of)
            PickDictArray[PdIndexPassed].iItemAttrCounter = 0;
            PickDictArray[PdIndexPassed].iItemLength = 0;

            PickDictArray[PdIndexPassed].AttrTwoStringValue = "";
            PickDictArray[PdIndexPassed].PdIndexAttrTwo = 0;
            PickDictArray[PdIndexPassed].AttrTwoIsNumeric = false;

            PickDictArray[PdIndexPassed].ColTouched = 0;
            PickDictArray[PdIndexPassed].ColIdDone = false;
            PickDictArray[PdIndexPassed].ColLength = 0;
            PickDictArray[PdIndexPassed].ColLengthChange = false;
            PickDictArray[PdIndexPassed].ColDefinitionFound = false;
        }
    }

    /// <summary>
    /// <para> Pick Dictionary Item</para>
    /// <para> This defines a single dictionary entry
    /// for the pick file system.  Naming conventions
    /// follow the Pick equivalents fairly closely.</para>
    /// </summary>
    /// <remarks>
    /// Usage needs clarification
    /// </remarks>
    public class PickDictItemDef
    {
        public String ItemId;
        public int ItemIntId;

        public bool InstanceCtor = false;

        public bool ItemIdIsNumeric;
        public String ItemIdConverted;
        //
        public bool ItemIdFoundNumericPk;
        //
        public int iItemAttrIndex;  //  Field being examined in this Dictionary Item
        public int iItemAttrCounter;  // Number of fields making up this Dictionary Item
        public int iItemLength;
        //
        public String sAttrNumber;
        public bool bAttrIsData;
        public bool bAttrIsDict;
        // Type
        public int iAttrType;
        public String sAttrType;
        public String sType;
        // SubType
        public int iAttrSubType;
        public String sAttrSubType;
        public String sSubType;
        //
        public int AttrTwoIntId; // Column Number
        public bool AttrTwoIsNumeric;
        // Array Index
        public int ColIndex; // Dictionary Column Number
        public int ColDataPoints;
        public int ColType;
        public int ColInvalid;
        // Heading (3)
        public String sHeading;
        // Dependancy (4) (5)
        public String sFour;
        public String sDependancy;
        public bool bDependancy;
        public int iDendancyKeyColumn;
        public String sDendancyKeyList;

        public String sFive;
        // (6)
        public String InputConversion;
        public String InputConvType;
        public String InputConvSubType;
        // (7)
        public String spOutputConversion;
        public String spOutputConvType;
        public String spOutputConvSubType;
        // (8)
        public String sCorrelative;
        public String sCorrType;
        public String sCorrSubType;
        // (9)
        public String sJustify;
        public String sJustification;
        public String sJustifyType;
        // (10)
        public String ColWidthString;
        public int ColWidth;
        public bool ColWidthIsNumeric;
        // ??
        public String sHeadingLong;
        public String sHelpShort;
        public String sRevColumnName;

        public int ColNumericPoints;
        public int ColDecimals;
        public int ColCurrencyPoints;
        public int ColDateFormat;
        public int ColFunctionPoints;
        public bool ColSuFile;

        public int ColTouched;
        public bool ColIdDone;
        public int ColLength;
        public bool ColLengthChange;
        public bool ColDefinitionFound;

        public String ColTypeWord;
        public bool ColUseParenthesis;

        public String AttrTwoStringValue;

        private int ipPdIndexAttrTwo;
        public int PdIndexAttrTwo
        {
            get { return ipPdIndexAttrTwo; }
            set { ipPdIndexAttrTwo = value; }
        }
        //
        public String AttrTwoStringAccounName;
        public String AttrThreeFileName;
        // Add
        public String ColAdd;
        public String ColDelete;
        public String ColAlter;
        public String ColUpdate;
        public String ColValidate;
        public String ColStatisticsGet;
        public String ColView;

        public bool ColAddFlag;
        public bool ColDeleteFlag;
        public bool ColAlterFlag;
        public bool ColUpdateFlag;
        public bool ColValidateFlag;
        public bool ColStatisticsGetFlag;
        public bool ColViewFlag;

        public String sTrigerAdd;
        public String sTrigerDelete;
        public String sTrigerAlter;
        public String sTrigerUpdate;

        public bool bTrigerAdd;
        public bool bTrigerDelete;
        public bool bTrigerAlter;
        public bool bTrigerUpdate;

        public PickDictItemDef()
        {
            InstanceCtor = true;
            PickDictItemReset(this);
        }

        public void PickDictItemReset(PickDictItemDef PickDictItemPassed)
        {
            PickDictItemPassed.ItemId = "";
            PickDictItemPassed.ItemIntId = 0;
            PickDictItemPassed.iItemAttrIndex = 0;  // Field being processed in the Dict Column
            //
            PickDictItemPassed.ColIndex = 0;
            PickDictItemPassed.iItemAttrCounter = 0; // TableOpen  How many Fields (size of)
            PickDictItemPassed.iItemAttrCounter = 0;
            PickDictItemPassed.iItemLength = 0;

            PickDictItemPassed.AttrTwoStringValue = "";
            PickDictItemPassed.PdIndexAttrTwo = 0;
            PickDictItemPassed.AttrTwoIsNumeric = false;

            PickDictItemPassed.ColTouched = 0;
            PickDictItemPassed.ColIdDone = false;
            PickDictItemPassed.ColLength = 0;
            PickDictItemPassed.ColLengthChange = false;
            PickDictItemPassed.ColDefinitionFound = false;
        }
    }

    // Pick Dict Item Class
    /// <summary>
    /// <para> Pick Dictionary Index</para>
    /// <para> An indexed array of ColumnMax elements
    /// used to build or manipulate a Pick dictionary.</para>
    /// </summary>
    /// <remarks>
    /// Usage needs clarification
    /// </remarks>
    public class PickDictIndexDef
    {
        //
        public static int MaxArr = (int)ArrayMax.ColumnMax;
        public static int MaxArrNew = (int)ArrayMax.ColumnMax + 1;
        //
        public String[] IndArray = new string[(int)ArrayMax.ColumnMax];
        //
        public static int ipIndGet;
        //
        public static int ipInd;
        public static int Ind
        {
            get { return ipInd; }
            set { ipInd = value; }
        }
        // Last Access ItemData
        System.DateTime dtLastAccessDateTime { get; set; }
        //
        // Last Accessed Index
        System.String sLastAccessFieldName { get; set; }
        System.Int32 iLastAccessColumnIndex { get; set; }
        // To enable client code to validate input 
        // when accessing your indexer.
        public int Length
        {
            get { return IndArray.Length; }
        }
        // Indexer declaration.
        // Input parameter is validated by client 
        // code before being passed to the indexer.
        public String this[int IndPassed]
        {
            get
            {
                ipInd = IndPassed;
                return IndArray[IndPassed];
            }

            set
            {
                ipInd = IndPassed;
                IndArray[IndPassed] = value;
            }
        }
        // This method finds the IndInstance or returns -1
        public int IndGet(String IndValuePassed)
        {
            ipIndGet = 0;
            foreach (String IndInstance in IndArray)
            {
                if (IndInstance == IndValuePassed)
                {
                    // ipInd = ipIndGet;
                    return ipIndGet;
                }
                ipIndGet++;
            }
            return -1;
        }
        // This method finds the IndInstance or returns ""
        public String IndGetValue(String IndValuePassed)
        {
            ipIndGet = 0;
            foreach (String IndInstance in IndArray)
            {
                ipIndGet += 1;
                // IndArray[ipIndGet] = "?";
                if (IndInstance == IndValuePassed)
                {
                    ipInd = ipIndGet;
                    return IndInstance;
                }
                ipIndGet++;
            }
            return "";
        }
        // The get accessor returns an integer for a given string
        public String this[String IndValuePassed]
        {
            get { return (IndGetValue(IndValuePassed)); }
            set { IndArray[IndGet(IndValuePassed)] = value; }
        }
    }
    #endregion

    #endregion

    /// <summary>
    /// <para> A group of temporary object, pointers and fields.</para>
    /// </summary>
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

    #endregion

    #region Transformation
    /// <summary>
    /// <para> File Transform Control</para>
    /// <para> This is a control structure for
    /// applications that have an input and output file.
    /// When input data is processed (or tranformed) to
    /// create a new output file.</para>
    /// <para> It consists of an input and output
    /// File Summary for persistence and during the
    /// user interface stage.  The summaries are used
    /// to open the file object for processing.</para>
    /// <para> It is permitted and recommended to have
    /// the InFile and OutFile files open during the
    /// user entry process.  However, proper attention
    /// must be paid to changes in file name versus
    /// what file has be opened.</para>
    /// </summary>
    public class FileTransformControlDef : StdDef
    {
        #region FileTransformRun InputOutput
        public FileTransformControlDef(mFile InFilePassed, mFile OutFilePassed)
            : base()
        {
            InFile = InFilePassed;
            OutFile = OutFilePassed;
            FileTransformControlInitialize();
        }
        public FileTransformControlDef()
            : base()
        {
            FileTransformControlInitialize();
        }
        public void FileTransformControlInitialize()
        {
            // InFile.FileTransformControl = this;
            InputFsCurr = new FileSummaryDef(InFile, (int)FileAction_DirectionIs.Input);
            // OutFile.FileTransformControl = this;
            OutputFsCurr = new FileSummaryDef(OutFile, (int)FileAction_DirectionIs.Output);
        }
        // Transformation
        public String TransformFileTitle = "";
        // Source (Import) and Destination (Output) Object
        #region FileInputItem Declaration
        // InFile
        public mFile InFile;
        public FileSummaryDef InputFsCurr;
        #endregion
        #region FileOutputItem Declaration
        public mFile OutFile;
        public FileSummaryDef OutputFsCurr;
        #endregion
        #endregion
        #region ItemIdNotes
        // An Id would be found in the 
        // input (import) file
        // this id may be compared to the
        // (entered) ItemId that was
        // supplied by the user.
        // (Currently) the user can only
        // enter one id.  The idea is for them
        // to enter a matched lUrlHistList that would
        // be presented as a paired lUrlHistList for
        // comparison (verification) by the user.
        #endregion
        // 
        // Pass Settings Function
        // 
        public void InputItemClassFields(
            // Part of the struct!!
        #region File Fields Passed
        #region InputFile Passed
            // InFile
            String sPassedInputFileName,
            mFile ofPassedInputFileObject,
            String sPassedInputFileOptions,
        #endregion
        #region OutputFile Passed
            // OutputSystem
            String OutputSystemNamePassed,
            Object OutputSystemObjectPassed,
            // OutputDatabase
            String OutputDatabaseNamePassed,
            SqlConnection OutputDbConnObjectPassed,
            // OutFile
            String OutputFileNamePassed,
            // Object ofPassedOutputFileObject,
            mFile ofPassedOutputFileObject,
            String sPassedOutputFileOptions,
            // OutputItemId
            String sPassedOutputItemId,
            // ExistingItemId
            String sPassedInputItemId
        #endregion
        #endregion
    // END OF InputItemClassFields Passed
    )
        {
            #region SetInputItem
            InFile.Fmain.Fs.FileId.FileName = sPassedInputFileName;
            InFile.Fmain.Fs.Direction = (int)FileAction_DirectionIs.Input;
            // huh? InFile.Fmain.Item.ItemLen = sPassedInputFileName.Length;
            #endregion
            #region SetFileOutputItem
            OutFile.Fmain.Fs.FileId.FileName = OutputFileNamePassed;
            OutFile.Fmain.Fs.Direction = (int)FileAction_DirectionIs.Output;
            // MFILE1 OBJECT
            if (ofPassedOutputFileObject != null)
            {
                if (OutFile != null)
                {
                    OutFile = null;
                }
                OutFile = ofPassedOutputFileObject;
            }
            #endregion
            #region ItemId
            InFile.Fmain.Item.ItemId = sPassedOutputItemId;
            #endregion
            #region SetMinputTldItemClassFields
            // Source and Destination Object
            #region SetInputItem Empty
            #endregion
            #region SetFileOutputItem Empty
            // OutputSystem
            // String Fs.spSystemName;
            // Object ooSystemObject;
            // Output Database
            // String spDatabaseName;
            // SqlConnection DbConnObject;
            // OutFile
            // String OutputFileName;
            // MfilePickDb OutFile;
            // String FileOptionsStringPassed;
            // OutputItemId
            // String ItemId;
            // 
            #endregion
            #region SetInputItemId
            // (Existing in Output vs Options)
            InFile.Fmain.Item.ItemId = "";
            InFile.Fmain.Fs.ItemIdCurrent = "";
            // FileTransformControl.ItemId = @"";
            // FileTransformControl.PickRow.PickDictArray.ItemId = @"";
            InFile.Fmain.Item.ItemIdIsChanged = bNO;
            InFile.Fmain.FileStatus.NameIsChanged = bNO;
            #endregion
            #endregion
            #region ClassInternalProperties
            //#region FileInputItemType Set
            //InFile.Fmain.Fs.FileTypeName = "Unknown FileType99";
            //InFile.Fmain.Fs.FileTypeId = (long)FileType_Is.Unknown; // TODO SHOULD LOAD FROM OPTIONS
            //InFile.Fmain.Fs.FileSubTypeName = "Unknown FileSubType99";
            //InFile.Fmain.Fs.FileSubTypeId = (long)FileType_SubTypeIs.Unknown;
            //#endregion
            //#region FileOutputItemType Set
            //OutFile.Fmain.Fs.FileTypeName = "Unknown FileType99";
            //OutFile.Fmain.Fs.FileTypeId = (long)FileType_Is.Unknown; // TODO SHOULD LOAD FROM OPTIONS
            //OutFile.Fmain.Fs.FileSubTypeName = "Unknown FileSubType99";
            //OutFile.Fmain.Fs.FileSubTypeId = (long)FileType_SubTypeIs.Unknown;
            //#endregion
            #endregion
        } // End of Constructor - InputItemClassFields Passed

        public override String ToString()
        {
            if (InFile != null && OutFile != null)
            {
                if (InFile.Fmain.Fs.FileId.FileName != null && OutFile.Fmain.Fs.FileId.FileName != null)
                {
                    String sTemp = "File Transform Control: " + InFile.ToString() + " and " + OutFile.ToString();
                    return sTemp;
                }
                else { return "File Transform Control not initialized."; }
            }
            else { return "File Transform Control not initialized."; }
        }
    }
    #endregion

    /// <summary>
    /// Delimited Separators Common defines the hierarchy
    /// of characters used as delimiters
    /// and support functions for delimited data processing.
    /// </summary> 
    public class DelSepDef
    {
        // Delimited Separators Common
        #region $include Mdm.Oss.File mFile FileField Separator ItemData
        // <Area Id = "Col(Attr)Fields">
        public bool ItemAttrSet = false;
        public int ItemAttrInvalid;
        // Ascii Attr Pointers
        public int ItemAttrIndex; // Current array pointer.  Can change any time and is free of build proces
        public int ItemAttrCount; // ItemData Items in Item / Row / Item
        public int ItemAttrCountTotal; // Accumulator for shrinking work buffer
        public int ItemAttrCounter; // Current Attr being loaded
        public int ItemAttrMaxIndex; // Total Attrs in Item
        public int ItemAttrMaxIndexTemp;

        // Working values Fields
        public int iItemDataAttrEos2Index; // End of Column Separator 2
        public int iItemDataAttrEos1Index; // End of Column Separator 1
        public int iItemDataAttrEosIndex; // End of Column Sub-Value
        public int iItemDataAttrEovIndex; // End of Column Value
        //
        public int iItemDataAttrEoaIndex; // End of Column
        public int iItemDataAttrEoaIndexEnd; // End of Column
        public int iItemDataAttrEorIndex; // End of Row
        public int iItemDataAttrEofIndex; // End of File
        // Character Pointers
        public int iItemDataCharIndex; // DataItem Character Pointer
        public int iItemDataCharEobIndex; // DataItem Character Pointer to end of block
        public int iItemDataCharEofIndex; // DataItem Character Pointer to end of File
        // <Area Id = "AsciiOpenOptions">
        public int iAsciiOpenOptions;
        #endregion
        /// <summary>
        /// </summary>

        public DelSepDef()
        {

        }
    }

    #region Delimited Records
    /// <summary>
    /// <para>Mdm Record class is used
    /// for ASCII delimited IO.  This is
    /// typically disk file data read where
    /// the columns or fields are text.</para>
    /// <para> Typically each line contains
    /// a field, the other most common format
    /// being CSV where a line defines a row
    /// where columns are separated by commas.</para>
    /// <para> MdmRecord accepts Std Delimiter
    /// structures to define field separators.</para>
    /// <para> It is closely tied to the ASCII file
    /// type IO and applies to all sub-types that
    /// are delimited.</para>
    /// </summary>
    public class MdmRecord : StdDef
    {
        public Object[] Items;
        // private String[] Item;
        public String RecordString;
        public String[] RecordArray;
        public StdDelimDef Del;
        public bool IsQuoted = false;
        public String QuoteChar = "\"";
        public bool IsEscaped = false;
        public int EscapedMode = (int)EscapedFormat.SlashedThreeDigit;

        public long RecordSetResult;
        public long RecordSet(ref String RecordStringPassed)
        {
            RecordSetResult = (long)StateIs.Started;
            RecordArray = RecordStringPassed.Split(Del.Rs.ToCharArray());
            Array.Resize(ref Items, RecordArray.Length);
            for (int ItemIndex = 0; ItemIndex < RecordArray.Length; ItemIndex++)
            {
                RecordArray[ItemIndex] = RecordArray[ItemIndex].Trim(Del.Trm.ToCharArray());
                if (RecordArray[ItemIndex].IndexOfAny(Del.Us.ToCharArray()) > 0)
                {
                    Items[ItemIndex] = RecordArray[ItemIndex].Split(Del.Us.ToCharArray());
                }
                else
                {
                    Items[ItemIndex] = RecordArray[ItemIndex];
                }
            }
            RecordArray = null;
            return RecordSetResult;
        }

        #region Indexers
        // Access a record
        public Object this[int RsIndex]
        {
            get
            {
                return Items[RsIndex];
            }
            set
            {
                Items[RsIndex] = value;
            }
        }

        // Access a record column
        public String this[int RsIndex, int UsIndex]
        {
            get
            {
                return ((String[])Items[RsIndex])[UsIndex];
            }
            set
            {
                ((String[])Items[RsIndex])[UsIndex] = value;
            }
        }
        #endregion
        #region Constructors
        public long MdmRecordResult;
        public MdmRecord()
        {
            Sender = this;
            ipFileTypeId = (long)FileType_Is.TEXT;
            ipFileSubTypeId = (long)FileType_SubTypeIs.ASC;
        }

        public MdmRecord(ref FileMainDef FmainPassed)
            : this()
        {
            ipFileTypeId = FmainPassed.Fs.ipFileTypeId;
            // spFileType = FmainPassed.Fs.FileTypeName;
            ipFileSubTypeId = FmainPassed.Fs.ipFileSubTypeId;
            // spFileSubType = FmainPassed.Fs.FileSubTypeName;
            //
            MdmRecordResult = MdmRecordInitialize();
            //
            try
            {
                RecordString = FmainPassed.Fs.FileIo.IoReadBuffer;
                if (RecordString.Length > 0)
                {
                    MdmRecordResult = RecordSet(ref RecordString);
                    RecordString = "";
                }
            }
            catch
            {
                // TODO MdmRecord error handling
            }
        }

        public long MdmRecordInitializeResult;
        public long MdmRecordInitialize()
        {
            MdmRecordInitializeResult = (long)StateIs.Started;
            MdmRecordInitializeResult = DelimLoad();
            return MdmRecordInitializeResult;
        }
        #endregion

        #region File Type
        // FileTypeName
        //public String spFileType;
        //public String FileTypeName {
        //    get { return spFileType; }
        //    set { spFileType = value; }
        //}
        public long ipFileTypeId;
        public long FileTypeId
        {
            get { return ipFileTypeId; }
            set
            {
                ipFileTypeId = value;
                FileTypeMajorId = FileTypeDef.FileTypeMetaLevelGet(ipFileTypeId);
            }
        }
        public long FileTypeMajorId;

        //public String spFileSubType;
        //public String FileSubTypeName {
        //    get { return spFileSubType; }
        //    set { spFileSubType = value; }
        //}
        public long ipFileSubTypeId;
        public long FileSubTypeId
        {
            get { return ipFileSubTypeId; }
            set
            {
                ipFileSubTypeId = value;
                FileSubTypeMajorId = FileTypeDef.FileSubTypeMajorGet(ipFileSubTypeId);
                FileSubTypeMinorId = FileTypeDef.FileSubTypeMinorGet(ipFileSubTypeId);
            }
        }
        public long FileSubTypeMajorId;
        public long FileSubTypeMinorId;
        #endregion

        #region Delimiter Load
        public long DelimLoadResult;
        public long DelimLoad()
        {
            DelimLoadResult = (long)StateIs.Started;
            //None ,
            //FileTypeDictData 1,
            //FileTypeData 2,
            //// Data Types
            //FileTypeTEXT 1,
            //FileTypeSQL 4,
            //FileTypeDB2 1,
            //FileTypeORACLE 2,
            //FileTypeXML 3,
            //FileTypePICK 1,
            //FileTypeOther 2,
            //// Unknonw
            //FileTypeUNKNOWN E,
            //FileTypeUndefined E2,
            //FileTypeUndefined1 E3


            //(long)FileType_SubTypeIs.SQL
            //| (long)FileType_SubTypeIs.MY
            //| (long)FileType_SubTypeIs.DB2
            //| (long)FileType_SubTypeIs.ORACLE
            //| (long)FileType_SubTypeIs.XML
            //| (long)FileType_SubTypeIs.TEXTSTD
            switch (FileSubTypeMajorId)
            {
                case ((long)FileType_SubTypeIs.FIX):
                    // Columns externally defined
                    // One record per line
                    // Fixed column widths (standard FIX)
                    break;
                case ((long)FileType_SubTypeIs.DAT):
                    // Columns externally defined
                    // ID in first position (Index 0) or externally defined
                    // One record per line
                    // ASCII native formating on line using FS, GS, RS, US, USS, USSS
                    Del = CharTable.DelStdGet();
                    break;
                case ((long)FileType_SubTypeIs.CSV):
                case ((long)FileType_SubTypeIs.Tilde_CSV):
                    // ID bracketed in Tildes in first position (Index 0)
                    // One record per line
                    // CSV formating on line
                    Del = new StdDelimDef();
                    Del.Us = ",";
                    break;
                case ((long)FileType_SubTypeIs.Tilde_ROW):
                    // ID bracketed in Tildes in first position (Index 0)
                    // One column per line
                    // Multivalues embedded in column in Native format
                    Del = CharTable.DelPickGet();
                    break;
                case ((long)FileType_SubTypeIs.Tilde_Native):
                    // ID bracketed in Tildes in first position (Index 0)
                    // One record per line
                    // PICK native formating on line using AM, VM, SVM etc.
                    Del = CharTable.DelPickGet();
                    break;
                case ((long)FileType_SubTypeIs.Tilde_Native_ONE):
                    // Not defined...
                    // ID bracketed in Tildes in first position (Index 0)
                    // One record per line
                    // PICK native formating on line using AM, VM, SVM etc.
                    Del = CharTable.DelPickGet();
                    break;
                case ((long)FileType_SubTypeIs.ASC):
                default:
                    // Columns externally defined
                    // ID in first position (Index 0) or externally defined
                    // One record per line
                    // ASCII native formating on line using FS, GS, 
                    // For RS, US, USS, USSS:
                    //      Rs: each line is a record
                    //      Us: each field is separated by a comma,
                    //          fields are not quoted values.
                    //      Uss: where applicable, multivaules are Stick Chars "|"
                    Del = CharTable.DelAsciiGet();
                    break;
            }
            return DelimLoadResult;
        }
        #endregion
    }
    #endregion

}
