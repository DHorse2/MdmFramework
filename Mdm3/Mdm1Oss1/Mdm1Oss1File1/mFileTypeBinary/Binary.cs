#region Dependencies
#region System
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
#endregion
namespace Mdm.Oss.File
{
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
        //  sNewItem=sEmpty
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
}
