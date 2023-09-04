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
using Mdm.Oss.Decl;
//@@@CODE@@@using Mdm.Oss.Mapp;
//using Mdm.Oss.Mobj;
//using Mdm.Pick;
//using Mdm.Pick.Console;
//@@@CODE@@@using Mdm.Oss.Support;
//using Mdm.Oss.Threading;
using Mdm.Oss.Std;

#region  Mdm Db and File
using Mdm.Oss.File;
using Mdm.Oss.File.Type;
//using Mdm.Oss.File.State;
using Mdm.Oss.File.Control;
//using Mdm.Oss.File.Db;
//using Mdm.Oss.File.Db.Data;
//using Mdm.Oss.File.Db.Table;
//using Mdm.Oss.File.Db.Thread;
//using Mdm.Oss.File.Properties;
#endregion
#region  Mdm File Types
//using Mdm.Oss.File.Type.Link;
//using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion

namespace Mdm.Oss.File.Type
{
    #region $include Mdm.Oss.File mFile FileTypeName
    /// <summary>
    /// A Management Class for File Type (Items).
    /// Used extensively by the File Management System
    /// </summary> 
    public class FileTypeDef : StdBaseDef
    {
        #region Declarations and Load
        //
        // note: could use LINQ here instead...
        //public static Dictionary<String, long> FileExtDict;
        //public static Dictionary<String, long> FileTypeDict;
        //public static Dictionary<long, long> FileTypeIdDict;
        //public static Dictionary<long, long> FileSubTypeIdDict;
        // note: LINQ design pattern
        public static IEnumerable<FileTypeItemDef> FileTypeItemsQuery;
        //
        public static Dictionary<long, FileTypeItemDef> FileTypeItems;
        //
        /// <summary> 
        /// Add a completely defined type using the passed fields.
        /// </summary> 
        /// <param name="ItemIdPassed">The id of the file type definition.</param>  
        /// <param name="FileExtPassed">Extesions served by this type.</param>  
        /// <param name="FileLevelPassed">Abstraction level of file (i.e. Data, Dict, Domain)</param> 
        /// <param name="FileTypeNamePassed">The name of the major file type.</param> 
        /// <param name="FileTypeIdPassed">The general or major file type.</param> 
        /// <param name="FileSubTypeNamePassed">Text name of the file sub-type.</param> 
        /// <param name="FileSubTypeIdPassed">The specific fil sub-type.</param> 
        /// <param name="DescriptionPassed">Description for this file type definition.</param> 
        /// <param name="FileIoModePassed">The file IO read mode to use with this type.</param> 
        /// <param name="IsDefaultPassed">Are defaults taken from the passed information.</param> 
        /// <remarks>
        /// </remarks> 
        public static void FileTypeItemsAdd(
            long ItemIdPassed, String FileExtPassed, FileType_LevelIs FileLevelPassed,
            String FileTypeNamePassed, FileType_Is FileTypeIdPassed,
            String FileSubTypeNamePassed, FileType_SubTypeIs FileSubTypeIdPassed,
            String DescriptionPassed, FileIo_ModeIs FileIoModePassed,
            bool IsDefaultPassed
            )
        {
            FileTypeItems.Add(
                ItemIdPassed,
                new FileTypeItemDef(
            ItemIdPassed, FileExtPassed, FileLevelPassed,
            FileTypeNamePassed, FileTypeIdPassed,
            FileSubTypeNamePassed, FileSubTypeIdPassed,
            DescriptionPassed, FileIoModePassed,
            IsDefaultPassed
                    ));
            // don't want exceptions and try's here...
            ////FileExtDict.Add(FileExtPassed, ItemIdPassed);
            ////FileTypeDict.Add(FileTypeNamePassed, ItemIdPassed);
            ////FileTypeIdDict.Add(FileTypeIdPassed, ItemIdPassed);
            ////FileSubTypeIdDict.Add(FileSubTypeIdPassed, ItemIdPassed);
            ////
            //FileExtDict[FileExtPassed] = ItemIdPassed;
            //FileTypeDict[FileTypeNamePassed] = ItemIdPassed;
            //FileTypeIdDict[FileTypeIdPassed] = ItemIdPassed;
            //FileSubTypeIdDict[FileSubTypeIdPassed] = ItemIdPassed;
        }

        /// <summary> 
        /// Build the file types table using the system defined
        /// core group of supported file types.
        /// Clears the list as well so provides a basic list that
        /// specific applicaitons might add to or remove from.
        /// </summary> 
        public static void FileTypeItemsBuild()
        {
            FileTypeItems = new Dictionary<long, FileTypeItemDef>();
            //
            //FileExtDict = new Dictionary<string, long>();
            //FileTypeDict = new Dictionary<string, long>();
            //FileTypeIdDict = new Dictionary<long, long>();
            //FileSubTypeIdDict = new Dictionary<long, long>();
            //
            FileTypeItemsAdd(
                701, "tld", FileType_LevelIs.Data,
                "Tilde Text", FileType_Is.Tilde,
                "Tilde Std", FileType_SubTypeIs.Tilde,
                "Tilde delimited data file", FileIo_ModeIs.All, false);
            FileTypeItemsAdd(
                751, "tlddict", FileType_LevelIs.DictData,
                "Tilde Text Dict", FileType_Is.Tilde,
                "Tilde Dict Std", FileType_SubTypeIs.Tilde,
                "Tilde delimited dictionary file", FileIo_ModeIs.All, false);
            FileTypeItemsAdd(
                2, "txt", FileType_LevelIs.Data,
                "Text", FileType_Is.TEXT,
                "Text", FileType_SubTypeIs.TEXT,
                "Text File", FileIo_ModeIs.All, false);
            FileTypeItemsAdd(
                3, "csv", FileType_LevelIs.Data,
                "Text Csv", FileType_Is.TEXT,
                "Csv", FileType_SubTypeIs.CSV,
                "Comma delimited text file", FileIo_ModeIs.All, false);
            FileTypeItemsAdd(
                4, "mdf", FileType_LevelIs.Data,
                "MsSql", FileType_Is.SQL,
                "Microsoft Sql Server File", FileType_SubTypeIs.MS,
                "MS SQL data file", FileIo_ModeIs.Sql, true);
            FileTypeItemsAdd(
                5, "xxx", FileType_LevelIs.Data,
                "MySql", FileType_Is.SQL,
                "Oracle MY SQL File", FileType_SubTypeIs.MY,
                "My Sql data file", FileIo_ModeIs.Sql, false);
            FileTypeItemsAdd(
                1001, "ItemList", FileType_LevelIs.Data,
                "System", FileType_Is.SystemList,
                "ItemList", FileType_SubTypeIs.MY,
                "System standard Text Cr Lf delimited list", FileIo_ModeIs.All, false);
            FileTypeItemsAdd(
                801, "png", FileType_LevelIs.Data,
                "Binary", FileType_Is.Binary,
                "Image", FileType_SubTypeIs.Binary,
                "PNG Image File", FileIo_ModeIs.Binary, false);
        }

        public FileTypeDef() { }
        #endregion

        #region Lookup based on Ext, Type Name, Type, SubType
        /// <summary> 
        /// Property lookup and default (uses LINQ).
        /// </summary> 
        public static FileTypeItemDef FileTypeGetDefault()
        {
            if (FileTypeItems == null) { FileTypeItemsBuild(); }
            FileTypeItemsQuery = FileTypeItems.Values.Where(
                ItemFound => ItemFound.IsDefault == true);
            return FileTypeItemsQuery.FirstOrDefault();
        }

        /// <summary> 
        /// Property lookup and default (uses LINQ).
        /// </summary> 
        public static FileTypeItemDef FileTypeGetExt(String FileExtPassed)
        {
            if (FileTypeItems == null) { FileTypeItemsBuild(); }
            FileTypeItemsQuery = FileTypeItems.Values.Where(
                ItemFound => ItemFound.FileExt == FileExtPassed.ToLower());
            return FileTypeItemsQuery.FirstOrDefault();
        }

        /// <summary> 
        /// Property lookup and default (uses LINQ).
        /// </summary> 
        public static FileTypeItemDef FileTypeGetType(String FileTypePassed)
        {
            if (FileTypeItems == null) { FileTypeItemsBuild(); }
            FileTypeItemsQuery = FileTypeItems.Values.Where(
                ItemFound => ItemFound.FileTypeName == FileTypePassed);
            return FileTypeItemsQuery.FirstOrDefault();
        }

        /// <summary> 
        /// Property lookup and default (uses LINQ).
        /// </summary> 
        public static FileTypeItemDef FileTypeGetTypeId(FileType_Is FileTypeIdPassed)
        {
            if (FileTypeItems == null) { FileTypeItemsBuild(); }
            FileTypeItemsQuery = FileTypeItems.Values.Where(
                ItemFound => ItemFound.FileTypeId == FileTypeIdPassed);
            return FileTypeItemsQuery.FirstOrDefault();
        }

        /// <summary> 
        /// Property lookup and default (uses LINQ).
        /// </summary> 
        public static FileTypeItemDef FileTypeGetSubTypeId(FileType_SubTypeIs FileSubTypeIdPassed)
        {
            if (FileTypeItems == null) { FileTypeItemsBuild(); }
            FileTypeItemsQuery = FileTypeItems.Values.Where(
                ItemFound => ItemFound.FileSubTypeId == FileSubTypeIdPassed);
            return FileTypeItemsQuery.FirstOrDefault();
        }
        #endregion

        #region Return File Types
        public static FileIo_ModeIs FileTypeMetaLevelGet(FileIo_ModeIs PassedFileIoTypeId) { return (FileIo_ModeIs)(0x0000000F & (long)PassedFileIoTypeId); }
        public static FileType_LevelIs FileTypeMetaLevelGet(FileType_LevelIs PassedFileTypeId) { return (FileType_LevelIs)(0x0000000F & (long)PassedFileTypeId); }
        public static FileType_Is FileTypeMajorGet(FileType_Is PassedFileTypeId) { return (FileType_Is)(0xFFFF0000 & (long)PassedFileTypeId); }
        public static FileType_Is FileTypeMinorGet(FileType_Is PassedFileTypeId) { return (FileType_Is)(0x0000FFFF & (long)PassedFileTypeId); }
        public static FileType_SubTypeIs FileSubTypeMajorGet(FileType_SubTypeIs PassedFileSubTypeId) { return (FileType_SubTypeIs)(0xFFFF0000 & (long)PassedFileSubTypeId); }
        public static FileType_SubTypeIs FileSubTypeMinorGet(FileType_SubTypeIs PassedFileSubTypeId) { return (FileType_SubTypeIs)(0x0000FFFF & (long)PassedFileSubTypeId); }
        //
        public static FileType_Is FileTypeTildeMinorGet(FileType_Is PassedFileTypeId) { return (FileType_Is)(0x00000FFF & (long)PassedFileTypeId); }
        //
        public static bool FileType_IsTilde(FileType_Is PassedFileTypeId)
        {
            return (PassedFileTypeId == FileType_Is.Tilde);
        }
        public static bool FileType_IsMarkup(FileType_Is PassedFileTypeId)
        {
            return (PassedFileTypeId == FileType_Is.MaskMarkup);
        }
        public static bool FileType_IsDiskFile(FileType_Is PassedFileTypeId)
        {
            return (PassedFileTypeId == FileType_Is.TEXT);
        }
        public static bool FileType_IsDatabase(FileType_Is PassedFileTypeId)
        {
            return (PassedFileTypeId == FileType_Is.MaskDatabase);
        }
        public static bool FileType_IsSystem(FileType_Is PassedFileTypeId)
        {
            return (PassedFileTypeId == FileType_Is.MaskSystem);
        }
        #endregion
    }
    /// <summary>
    /// <para> File Type Definition Item</para>
    /// <para> Defines a specific file type
    /// in the file system.  These are collected
    /// by the File Type class and used to validate
    /// file types and provide default information.</para>
    /// </summary>
    public class FileTypeItemDef
    {
        public long ItemId;
        public String FileExt;
        public FileType_LevelIs MetaLevelId; // dict / data
        public String FileTypeName;
        public FileIo_ModeIs IoType;
        public FileType_Is FileTypeId;
        public String FileSubTypeName;
        public FileType_SubTypeIs FileSubTypeId;
        public String Description;
        public FileAction_ReadModeIs FileReadMode; // ToDo Re Read Mode Is.
        // ToDo Write Mode Is What? Where?
        // ToDo This seems incomplete.
        public bool IsDefault;

        public FileTypeItemDef(
            long ItemIdPassed, String FileExtPassed, FileType_LevelIs MetaLevelIdPassed,
            String FileTypeNamePassed, FileType_Is FileTypeIdPassed,
            String FileSubTypeNamePassed, FileType_SubTypeIs FileSubTypeIdPassed,
            String DescriptionPassed, FileIo_ModeIs FileIoTypePassed,
            bool IsDefaultPassed
            )
        {
            MetaLevelId = MetaLevelIdPassed;
            ItemId = ItemIdPassed;
            FileExt = FileExtPassed;
            FileTypeName = FileTypeNamePassed;
            FileTypeId = FileTypeIdPassed;
            FileSubTypeName = FileSubTypeNamePassed;
            FileSubTypeId = FileSubTypeIdPassed;
            Description = DescriptionPassed;
            IoType = FileIoTypePassed;
            IsDefault = IsDefaultPassed;
        }
        public FileTypeItemDef() { }
    }
    #endregion
    #region File Type Handling
    #region FileType Constants
    /// <summary> 
    /// To indicate the file data being processed is
    /// either Data or Dictionary (file schema) information.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    [Flags]
    public enum FileType_LevelIs : long
    {
        None = 0x00000000,
        // Dictionary / Data
        DictData = 0x00000001,
        Data = 0x00000002
    }

    /// <summary> 
    /// Major or Primary file type.
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    [Flags]
    public enum FileType_Is : long
    {
        None = 0x0,
        // 3 Other
        Other = 0x100,
        // 3 System
        MaskSystem = 0xF00,
        SystemList = 0x200,
        SystemData = 0x400,
        // 4 Binary
        Binary = 0xF000,
        // 5 PICK
        PICK = 0xF0000,
        // 6 Text Data Types
        MaskTilde = 0x300000,
        Tilde = 0x100000,
        // x        = 0x200000,
        //
        MaskText = 0x400000,
        // x        = 0x800000,
        TEXT = 0xC00000,
        //
        // 7 MarkupMask Formants
        MaskMarkup = 0xF000000,
        // JSON standard
        JSON = 0x1000000,
        // HTML
        Html = 0x2000000,
        // XML
        XML = 0x3000000,
        // Database
        // 8 SQL-ish
        MaskDatabase = 0xF0000000,
        SQL = 0x10000000,
        DB2 = 0x20000000,
        ORACLE = 0x40000000,
        //
        // 8 Unknonw
        Unknown = 0xFF1,
        Undefined = 0xFF2,
        Undefined1 = 0xFF4
    }

    /// <summary> 
    /// Minor, Secondary or Sub File Type
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    [Flags]
    public enum FileType_SubTypeIs : long
    {
        None = (long)FileType_Is.None,
        // Ascii, Text
        TEXT = (long)FileType_Is.TEXT,
        //
        TEXTSTD = FileType_Is.TEXT | 0x1,
        // Delimited using standard FS US
        ASC = FileType_Is.TEXT | 0x2,
        // Fixed width columns per supplied definition
        FIX = FileType_Is.TEXT | 0x4,
        // not specified nor predictable
        DAT = FileType_Is.TEXT | 0x8,
        // CSV standard format
        CSV = FileType_Is.TEXT | 0x10,
        //
        // Markup Mask Formats
        MaskMarkup = (long)FileType_Is.MaskMarkup,
        // JSON standard
        JSON = (long)FileType_Is.JSON,
        // HTML
        MaskHtml = (long)FileType_Is.Html,
        Html30 = FileType_SubTypeIs.MaskHtml | 0x1,
        Html40 = FileType_SubTypeIs.MaskHtml | 0x2,
        Html50 = FileType_SubTypeIs.MaskHtml | 0x4,
        // Xml
        XML = (long)FileType_Is.XML,
        //
        // Database Formats Mask
        MaskDatabase = (long)FileType_Is.MaskDatabase,
        // Sql Mask
        // 92 standard syntax
        SQL = (long)FileType_Is.SQL | 0x1,
        // Ms Sql Server
        MS = FileType_SubTypeIs.SQL | 0x2,
        // My Sql Sun / Oracle
        MY = FileType_SubTypeIs.SQL | 0x4,
        //
        // IBM Db2
        DB2 = (long)FileType_Is.DB2,
        //
        // Oracle
        ORACLE = (long)FileType_Is.ORACLE,
        //
        // Tld Tilde Mask 5 F's to subtype tilde
        MaskTilde = (long)FileType_Is.MaskTilde,
        Tilde = FileType_SubTypeIs.MaskTilde | 0x1,
        Tilde_ROW = FileType_SubTypeIs.MaskTilde | 0x2,
        Tilde_Other = FileType_SubTypeIs.MaskTilde | 0x4,
        Tilde_Native = FileType_SubTypeIs.MaskTilde | 0x8,
        Tilde_Native_ONE = FileType_SubTypeIs.MaskTilde | 0x10,
        Tilde_CSV = FileType_SubTypeIs.MaskTilde | 0x20,
        //
        // System Mask
        MaskSystem = (long)FileType_Is.MaskSystem | 0x00000,
        //
        // System List Mask
        SystemList = FileType_SubTypeIs.MaskSystem | 0x200,
        // Cr delimited
        ItemList = FileType_SubTypeIs.SystemList | 0x1,
        // Single value (entire file)
        SingleValue = FileType_SubTypeIs.SystemList | 0x2,
        // Cr delimited
        LookupTable = FileType_SubTypeIs.SystemList | 0x4,
        //
        // System Data Mask
        SystemData = FileType_SubTypeIs.MaskSystem | 0x400,
        // Cr US delimited
        FileDictDef = FileType_SubTypeIs.SystemList | 0x1,
        // Cr US delimited
        PairedList = FileType_SubTypeIs.SystemList | 0x2,
        // Cr Colon delimited
        IniFile = FileType_SubTypeIs.SystemList | 0x4,
        //
        // Binary
        Binary = (long)FileType_Is.Binary,
        //
        // Unknown
        Unknown = (long)FileType_Is.Unknown,
        Undefined = (long)FileType_Is.Undefined,
        Undefined1 = (long)FileType_Is.Undefined1
    }
    #endregion
    #region File Type definitions
    /// <summary> 
    /// File Type Items contains default setting
    /// for processing files of each specific type.
    /// Predefined items constitute the default
    /// types and those most commonly processed
    /// by the system.
    /// </summary> 
    /// <remarks>
    /// </remarks> 

    #endregion
    #region Object Type definitions empty
    #endregion
    #region Data and Primitive Type Definitons
    public class TypeTableItemDef
    {
        public static string Name;
        public static int Width;
        public static long Lo;
        public static ulong Hi;
        public static String Desc;
        public TypeTableItemDef()
        {
        }
        public TypeTableItemDef(
            string NamePassed,
            Dictionary<String, TypeTableItemDef> TypeDictPassed,
            int WidthPassed,
            long LoPassed,
            ulong HiPassed,
            String DescPassed
            )
        {
            Name = NamePassed;
            Width = WidthPassed;
            // The low range of types that are part of this group
            Lo = LoPassed;
            // The high range of flag values that are included in this group.
            Hi = HiPassed;
            Desc = DescPassed;

            if (!TypeDictPassed.ContainsKey(Name))
            {
                TypeDictPassed.Add(Name, this);
            }
        }
    }
    /// <summary>
    /// <para> A list of predefined primitive types used
    /// by the system.  It is mainly important to tranformation
    /// operations on data.</para>
    /// </summary>
    public class TypeTableList
    {
        public Dictionary<string, TypeTableItemDef> TypeDict = new Dictionary<string, TypeTableItemDef>();
        public TypeTableList()
        {
            //Type Range Size
            TypeTableItemDef TYPEsbyte = new TypeTableItemDef("sbyte", TypeDict, 3, -128, 127, "Signed 8-bit integer.");
            TypeTableItemDef TYPEbyte = new TypeTableItemDef("byte", TypeDict, 3, 0, 255, "Unsigned 8-bit integer.");
            TypeTableItemDef TYPEchar = new TypeTableItemDef("char", TypeDict, 4, 0x0000, 0xffff, "Unicode 16-bit character.");
            TypeTableItemDef TYPEshort = new TypeTableItemDef("short", TypeDict, 5, -32768, 32767, "Signed 16-bit integer.");
            TypeTableItemDef TYPEushort = new TypeTableItemDef("ushort", TypeDict, 5, 0, 65535, "Unsigned 16-bit integer.");
            TypeTableItemDef TYPEint = new TypeTableItemDef("int", TypeDict, 10, -2147483648, 2147483647, "Signed 32-bit integer.");
            TypeTableItemDef TYPEuint = new TypeTableItemDef("uint", TypeDict, 10, 0, 4294967295, "Unsigned 32-bit integer.");
            TypeTableItemDef TYPElong = new TypeTableItemDef("long", TypeDict, 19, -9223372036854775808, 9223372036854775807, "Signed 64-bit integer.");
            TypeTableItemDef Typeulong = new TypeTableItemDef("ulong", TypeDict, 10, 0, 18446744073709551615, "Unsigned 64-bit integer.");
            // If the value represented by an integer literal 
            // exceeds the range of ulong, a compilation error will occur.
        }
    }
    #endregion
    #endregion
}
