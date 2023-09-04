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
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
//using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
using Mdm.Oss.Std;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion
#region Row / Item
namespace Mdm.Oss.File.Db.Table
{
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
        /// <remarks>
        /// Copied documentation on SQL Connection mode:
        /// 
        /// Default The query may return multiple result sets. 
        /// Execution of the query may affect the database state. 
        /// Default sets no CommandBehavior flags, 
        /// so calling ExecuteReader(CommandBehavior.Default) 
        ///
        /// is functionally equivalent to calling ExecuteReader(). 
        /// SingleResult The query returns a single result set. 
        ///
        /// SchemaOnly The query returns column information only. 
        /// When using SchemaOnly, the .NET Framework Data Provider 
        /// for SQL Server precedes the statement being executed with SET FMTONLY ON. 
        ///
        /// KeyInfo The query returns column and primary key information.  
        ///
        ///SingleRow The query is expected to return a single row. 
        /// Execution of the query may affect the database state. 
        /// Some .NET Framework data providers may, but are not required to, 
        /// use this information to optimize the performance of the command. 
        /// When you specify SingleRow with the ExecuteReader method 
        /// of the OleDbCommand object, the .NET Framework Data Provider 
        /// for OLE DB performs binding using the OLE DB IRow interface 
        /// if it is available. Otherwise, it uses the IRowset interface. 
        /// If your SQL statement is expected to return only a single row, 
        /// specifying SingleRow can also improve application performance. 
        /// It is possible to specify SingleRow when executing queries 
        /// that return multiple result sets. In that case, 
        /// multiple result sets are still returned, but each result set has a single row. 
        ///
        ///SequentialAccess Provides a way for the DataReader to handle rows 
        /// that contain columns with large binary values. 
        /// Rather than loading the entire row, SequentialAccess 
        /// enables the DataReader to load data as a stream. 
        /// You can then use the GetBytes or GetChars method to specify 
        /// a byte location to start the read operation, 
        /// and a limited buffer size for the data being returned. 
        ///CloseConnection When the command is executed, the associated Connection object 
        /// is closed when the associated DataReader object is closed. 
        /// </remarks>
        protected const string sUnknown = "unknown";
        protected const string sEmpty = "";
        protected const int iUnknown = 99999;
        #region FileRowInfo Fields
        /// <summary>
        /// </summary>
        public bool LineIsRow = false;
        /// <summary>
        /// </summary>
        public bool LineIsColumn = false;
        // Read mode on database
        /// <summary>
        /// </summary>
        public FileIo_SqlCommandModeIs UseMethod;
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
        protected internal int ipRowCount = -iUnknown;
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
        #endregion
        public RowInfoDef()
        {
        }
        public RowInfoDef(int PdIndexMaxPassed)
        {
            RowMax = PdIndexMaxPassed;
        }
    }
}
#endregion
#region Column / Field
namespace Mdm.Oss.File.Db.Table
{
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
        public System.Type ttGetFieldType;
        /// <summary>
        /// </summary>
        public String iGetName;
        /// <summary>
        /// </summary>
        public int iGetOrdinal;
        // XXX public IEnumerator<T> lnGetEnumerator;
        /// <summary>
        /// </summary>
        public System.Type ttGetProviderSpecificFieldType;
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
    public class PickColDef
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
        public PickColDef()
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
}
#endregion
