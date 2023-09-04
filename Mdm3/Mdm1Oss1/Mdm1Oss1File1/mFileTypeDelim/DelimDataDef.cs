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
//@@@CODE@@@using Mdm.Oss.ClipUtil;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
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
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
//using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion

namespace Mdm.Oss.File.Type.Delim
{
    #region Delimited Records
    /// <summary>
    /// <para> 
    /// 1) Mdm Record class (Now DelimDataDef)
    /// is used for Delimited Records 
    /// (ASCII delimited IO.)
    /// </para><para> 
    /// It is part of the original SRT app but
    /// in terms of the curent design belongs here.
    /// </para><para> 
    /// 1.1) This is typically disk file data read where
    /// the columns or fields are text.
    /// </para><para> 
    /// 1.2) Examples include FIX,ASC,CSV,TLD and so on
    /// and extends markup style data such as
    /// HTML or json.
    /// </para><para> 
    /// 2) Typically each line contains
    /// a field, the other most common format
    /// being CSV where a line defines a row
    /// where columns are separated by commas.
    /// </para><para> 
    /// 3) MdmRecord accepts Std Delimiter
    /// structures to define field separators.
    /// </para><para> 
    /// It is closely tied to the ASCII file
    /// type IO and applies to all sub-types that
    /// are delimited.
    /// </para>
    /// </summary> 
    public class DelimDataDef : StdDef
    {
        #region Fields
        public Object[] Items;
        // private String[] Item;
        public String RecordString;
        public String[] RecordArray;
        public StdDelimDef Del;
        public bool IsQuoted = false;
        public String QuoteChar = "\"";
        public bool IsEscaped = false;
        public int EscapedMode = (int)EscapedFormat.SlashedThreeDigit;
        public StateIs RecordSetResult;
        public FileIo_ModeIs FileIoModeId;
        public StateIs MdmRecordInitializeResult;
        #endregion
        public StateIs RecordSetFrom(ref String RecordStringPassed)
        {
            RecordSetResult = StateIs.Started;
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
        #region Constructors
        public StateIs MdmRecordResult;
        public DelimDataDef()
        {
            Sender = this;
            FileTypeId = FileType_Is.TEXT;
            FileSubTypeId = FileType_SubTypeIs.ASC;
        }
        public DelimDataDef(ref mFileMainDef FmainPassed)
            : this()
        {
            FileTypeId = FmainPassed.Fs.FileType;
            // spFileType = FmainPassed.Fs.FileTypeName;
            FileSubTypeId = FmainPassed.Fs.FileSubType;
            // spFileSubType = FmainPassed.Fs.FileSubTypeName;
            //
            MdmRecordResult = MdmRecordInitialize();
            //
            try
            {
                RecordString = FmainPassed.Fs.FileIo.IoReadBuffer;
                if (RecordString.Length > 0)
                {
                    MdmRecordResult = RecordSetFrom(ref RecordString);
                    RecordString = "";
                }
            }
            catch
            {
                // ToDo MdmRecord error handling
            }
        }
        public StateIs MdmRecordInitialize()
        {
            MdmRecordInitializeResult = StateIs.Started;
            MdmRecordInitializeResult = DelimLoad();
            return MdmRecordInitializeResult;
        }
        #endregion
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
        #region Delimiter Load
        public StateIs DelimLoadResult;
        public StateIs DelimLoad()
        {
            DelimLoadResult = StateIs.Started;
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


            //FileType_SubTypeIs.SQL
            //| FileType_SubTypeIs.MY
            //| FileType_SubTypeIs.DB2
            //| FileType_SubTypeIs.ORACLE
            //| FileType_SubTypeIs.XML
            //| FileType_SubTypeIs.TEXTSTD
            // switch (FileSubTypeMajorId)
            switch (FileSubTypeId)
            {
                case (FileType_SubTypeIs.FIX):
                    // Columns externally defined
                    // One record per line
                    // Fixed column widths (standard FIX)
                    break;
                case (FileType_SubTypeIs.DAT):
                    // Columns externally defined
                    // ID in first position (Index 0) or externally defined
                    // One record per line
                    // ASCII native formating on line using FS, GS, RS, US, USS, USSS
                    Del = CharTable.DelStdGet();
                    break;
                case (FileType_SubTypeIs.CSV):
                case (FileType_SubTypeIs.Tilde_CSV):
                    // ID bracketed in Tildes in first position (Index 0)
                    // One record per line
                    // CSV formating on line
                    Del = new StdDelimDef
                    {
                        Us = ","
                    };
                    break;
                case (FileType_SubTypeIs.Tilde_ROW):
                    // ID bracketed in Tildes in first position (Index 0)
                    // One column per line
                    // Multivalues embedded in column in Native format
                    Del = CharTable.DelPickGet();
                    break;
                case (FileType_SubTypeIs.Tilde_Native):
                    // ID bracketed in Tildes in first position (Index 0)
                    // One record per line
                    // PICK native formating on line using AM, VM, SVM etc.
                    Del = CharTable.DelPickGet();
                    break;
                case (FileType_SubTypeIs.Tilde_Native_ONE):
                    // Not defined...
                    // ID bracketed in Tildes in first position (Index 0)
                    // One record per line
                    // PICK native formating on line using AM, VM, SVM etc.
                    Del = CharTable.DelPickGet();
                    break;
                case (FileType_SubTypeIs.ASC):
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
    /// <summary>
    /// Delimited Separators Common defines the hierarchy
    /// of characters used as delimiters
    /// and support functions for delimited data processing.
    /// </summary> 
    public class DelimSepDef
    {
        // Delimited Separators Common
        #region Fields Item Attribute Delimiters
        // $include Mdm.Oss.File mFile FileField Separator ItemData
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
        /// <para> 
        /// 4) Delimited Separators commonly define the hierarchy
        /// of characters used as delimiters
        /// and support functions for delimited data processing.
        /// </para>
        /// </summary>
        public DelimSepDef() { }
    }
}
