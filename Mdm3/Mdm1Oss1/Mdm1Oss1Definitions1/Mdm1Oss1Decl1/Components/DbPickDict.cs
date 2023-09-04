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
#region Mdm Core
using Mdm;
using Mdm.Oss.Decl;
using Mdm.Oss.Components;
using Mdm.Oss.Std;
#endregion
#region  Mdm Db and File
using Mdm.Oss.File;
using Mdm.Oss.File.Control;
//using Mdm.Oss.File.Db;
//using Mdm.Oss.File.Db.Data;
//using Mdm.Oss.File.Db.Table;
//using Mdm.Oss.File.Db.Thread;
//using Mdm.Oss.File.Properties;
#endregion
#region  Mdm File Types
//using Mdm.Oss.File.Type;
//using Mdm.Oss.File.Type.Link;
//using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion

namespace Mdm.Oss.File.Type.Pick
{
    // Pick Dict Item Class
    /// <summary>
    /// <para> Pick Dictionary Item</para>
    /// <para> This defines a single dictionary entry
    /// for the pick file system.  Naming conventions
    /// follow the Pick equivalents fairly closely.</para>
    /// </summary>
    /// <remarks>
    /// Usage needs clarification (Fixed).
    /// Origin: Mdm code converted as public class PickDictIndexDef
    /// IMPORTANT: Many Mdm function names are being normalized.
    /// References in classes to "Pick",
    /// when not refering to the (multivalued) c# syntax extension
    /// are part of Std.
    /// </remarks>
    public class PickDictItemDef
        : StdDictItemDef
    {
        public string aaabbMarker;
        #region Fields
        public bool ItemIdIsNumeric;
        public String ItemIdConverted;
        //
        public bool ItemIdFoundNumericPk;
        //
        public int iItemAttrIndex;  //  Field being examined in this Dictionary Item
        public int iItemAttrCounter;  // Number of fields making up this Dictionary Item
        public int iItemLength;
        //
        //public String sAttrNumber;
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
        public String sFive;
        // (6)
        // (7)
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
        public String sTrigerUpdate;
        #endregion
        public PickDictItemDef()
        {
            InstanceCtor = true;
            PickDictItemReset(this);
        }
        public void PickDictItemReset(PickDictItemDef PickDictItemPassed)
        {
            PickDictItemPassed.Id = 0;
            PickDictItemPassed.ItemId = "";
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
    /// <summary>
    /// <para> Pick Dictionary Index</para>
    /// <para> An indexed array of ColumnMax elements
    /// used to build or manipulate a Pick dictionary.</para>
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class PickDictIndexDef 
         : StdDictIndexDef<PickDictItemDef>
    {
        public string aaadbMarker;
        //
        public static int MaxArr = (int)ArrayMax.ColumnMax;
        public static int MaxArrNew = (int)ArrayMax.ColumnMax + 1;
        //
        public static String[] sdArray = new string[(int)ArrayMax.ColumnMax];
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

        // Indexer declaration.
        // Input parameter is validated by client 
        // code before being passed to the indexer.
        public String this[int IndPassed]
        {
            get
            {
                ipInd = IndPassed;
                return sdArray[IndPassed];
            }

            set
            {
                ipInd = IndPassed;
                sdArray[IndPassed] = value;
            }
        }
        // This method finds the IndInstance or returns -1
        public int IndGet(String IndValuePassed)
        {
            ipIndGet = 0;
            foreach (String IndInstance in sdArray)
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
        // This method finds the IndInstance or returns sEmpty
        public String IndGetValue(String IndValuePassed)
        {
            ipIndGet = 0;
            foreach (String IndInstance in sdArray)
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
        //// The get accessor returns an integer for a given string
        //public String this[String IndValuePassed]
        //{
        //    get { return (IndGetValue(IndValuePassed)); }
        //    set { IndArray[IndGet(IndValuePassed)] = value; }
        //}
    }
    // Pick Item Array Dictionary
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
         : StdDictItemArrayDef
    {
        public string aaacbMarker;
        public new PickDictItemDef[] sdArray = new PickDictItemDef[sdIndexMaxNew];
        public PickDictItemArrayDef()
        {
            sdArray = new PickDictItemDef[sdIndexMaxNew];
            sdaIndex = 0;
        }

        public void PdArrayCheck(ref int PdaIndexPassed)
        {
            sdaIndex = PdaIndexPassed;
            if (sdaIndex < 0)
            {
                sdaIndex = 0;
                // ToDo Exception Index Error, out of range (below zero)
            }
            if (sdaIndex > (int)ArrayMax.ColumnAliasMax)
            {
                sdaIndex = (int)ArrayMax.ColumnAliasMax;
                // ToDo Exception Index Error, out of range (greater than maximum allowed)
            }
            if (sdArray[sdaIndex] == null)
            {
                sdArray[sdaIndex] = new PickDictItemDef();
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
         : StdRowDef
    {
        public string aaarbMarker;
        // $include Mdm.Oss.File mFile PickDictControl
        // Index Key for array and relative row number
        // public PickDictItemDef[] PickDictArray = new PickDictItemDef[PdIndexMaxNew];
        public new PickDictItemArrayDef sdArray = new PickDictItemArrayDef();
        public PickRowDef() { }
        public new void DataClear()
        {
            base.DataClear();
            // Pick Dictionary
            sdIndex = 0;
            sdItemCount = 0;
        }
        public new void RowDataClear(int PdIndexPassed)
        {
            // base.sdArray.
            // if (PickDictArray[PdIndex] == null) { PickDictArray[PdIndex] = new PickDictItemDef(); } 
            sdArray[PdIndexPassed].ItemId = "";
            sdArray[PdIndexPassed].Id = 0;
            // ToDo: (later) The following code should be working and isn't.
            //sdArray[PdIndexPassed].iItemAttrIndex = 0;  // Field being processed in the Dict Column
            ////
            //sdArray[PdIndexPassed].ColIndex = 0;
            //sdArray[PdIndexPassed].iItemAttrCounter = 0; // TableOpen  How many Fields (size of)
            //sdArray[PdIndexPassed].iItemAttrCounter = 0;
            //sdArray[PdIndexPassed].iItemLength = 0;

            //sdArray[PdIndexPassed].AttrTwoStringValue = "";
            //sdArray[PdIndexPassed].PdIndexAttrTwo = 0;
            //sdArray[PdIndexPassed].AttrTwoIsNumeric = false;

            //sdArray[PdIndexPassed].ColTouched = 0;
            //sdArray[PdIndexPassed].ColIdDone = false;
            //sdArray[PdIndexPassed].ColLength = 0;
            //sdArray[PdIndexPassed].ColLengthChange = false;
            //sdArray[PdIndexPassed].ColDefinitionFound = false;
        }
    }
}
