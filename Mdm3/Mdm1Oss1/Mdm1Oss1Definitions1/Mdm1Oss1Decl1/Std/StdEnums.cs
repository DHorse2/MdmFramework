using System;

namespace Mdm.Oss.Std
{
    #region Maximums, Minimums, Char Table, Delimiters, Escaping, Ordering Constants
    // Array Sizes
    /// <summary> 
    /// The maximum number of columns allowed within the file system
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public enum ArrayMax : int
    {
        ColumnMax = 256,
        ColumnAliasMax = 1024
    }
    /// <summary> 
    /// The Standard Delimiter definition defines a group
    /// of characters that are used to define record formats
    /// where each row, column, datum or key-value pair are
    /// separated by a special character.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public class StdDelimDef
    {
        public String Ss;
        public String Fs;
        public String Gs;
        public String Rs;
        public String Us;
        public String Uss;
        public String Usss;
        public String Ussss;
        public String Trm;
        //
        /// <summary> 
        /// Default set using the delimiter characters from
        /// the standard ASCII character set.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public StdDelimDef()
        {
            Ss = Mdm.Oss.Decl.CharTable.Ass;
            Fs = Mdm.Oss.Decl.CharTable.Afs;
            Gs = Mdm.Oss.Decl.CharTable.Ags;
            Rs = Mdm.Oss.Decl.CharTable.Ars;
            Uss = Mdm.Oss.Decl.CharTable.Aus;
            Usss = Mdm.Oss.Decl.CharTable.Ausss;
            Ussss = Mdm.Oss.Decl.CharTable.Aussss;
            Trm = Mdm.Oss.Decl.CharTable.Trm;
        }
        //
        /// <summary> 
        /// Create a delimiter character definition using
        /// the passed characters.
        /// </summary> 
        /// <remarks>
        /// </remarks> 
        public StdDelimDef(
            String SPassed,
            String FPassed,
            String GPassed,
            String RPassed,
            String UPassed,
            String UsPassed,
            String TrmPassed
            )
        {
            Ss = SPassed;
            Fs = FPassed;
            Gs = GPassed;
            Rs = RPassed;
            Us = UPassed;
            Uss = UsPassed;
            Trm = TrmPassed;
        }
    }
    /// <summary> 
    /// When string data is escaped this enumeration
    /// indicates the method used.  Escaped string contain
    /// marker character to indicate special content.  The
    /// most common being NewLines, Quotes and Slashes.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public enum ColEscapedIs : int
    {
        ColEscapedFORBINARY = 1,
        ColEscapedNEWLINE = 2,
        ColEscapedVstudioFormat = 3
    }
    /// <summary> 
    /// When string data is escaped this enumeration
    /// indicates what technique is used to handle 
    /// quotation marks.
    /// handled.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public enum ColQuoteIs : int
    {
        ColQuoteDOUBLE = 1,
        ColQuoteSINGLE = 2,
        ColQuoteBACKSLASH = 3,
        ColQuoteFORWARD = 4,
        ColQuoteBRACKETE = 5
    }
    /// <summary> 
    /// When string data is escaped this enumeration
    /// indicates which technique will be used to generate
    /// 7-bit (low order ASCII) text output.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public enum EscapedFormat : int
    {
        SlashedChar = 1,
        SlashedThreeDigit = 2,
        SlashedShiftInOut = 3
    }
    /// <summary> 
    /// Indicates how and if a column or data will be sorted.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public enum OrderIs : int
    {
        NotOrdered = 0x0000001,
        Ascending = 0x0000002,
        Descending = 0x0000003
    }
    #endregion
    [Flags]
    public enum ButtonActionIs
    {
        None = 0,
        Insert = 1,
        Delete = 2,
        Update = 3,
        Select = 4,
        Execute = 5
    }
}
