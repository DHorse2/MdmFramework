using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// namespace Mdm.Oss.Mobj {
namespace Mdm {
    public static class MickString1
    {
        /* 
        private String PassedMickString = "";

        MickString1() : base()
        {
            PassedMickString = "";
        }

        MickString1(String StringPassed) : base()
        {
            PassedMickString = StringPassed;
        }
        */
        /*
        public String MickString 
        {
            get 
            {
                return PassedMickString; 
            } 

            set
            { 
                PassedMickString = value; 
            } 

        }

        public int IndexOf(this String PassedMickString, String PassedFieldChar, int iPassedLocCurrent)
        {
            int iTemp = 0;
            iTemp = (PassedMickString.ToString()).IndexOf(PassedFieldChar, iPassedLocCurrent);
            return iTemp;
        }

        public int Length(this String PassedMickString)
        {
            int iTemp = 0;
            iTemp = (PassedMickString.ToString()).Length;
            return iTemp;
        }

        public String Substring(this String PassedMickStringint, int iPassedFieldEndCharacterIndex)
        {
            String sFieldResult = "";
            sFieldResult = (PassedMickString.ToString()).Substring(iPassedFieldEndCharacterIndex);
            return sFieldResult;
        }
        */
        #region Fields - Set Variables for manuipulating
		/// <summary> 
        /// Generic Field or File Action enumeration
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks></remarks> 
        public enum FieldActionIs : int {
            None = 0,
            Get = 1,
            Delete = 2,
            Drop = 2,
            Insert = 3,
            Add = 3,
            Count = 4
        }

        /// <summary> 
        /// Function to perform during Action on Multivalue field loop
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks></remarks> 
        public enum FieldModeIs : int {
            None = 0,
            Extract = 1,
            Split = 2
        }

        /// <summary> 
        /// Standard and Multivalued field delimiters
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks></remarks> 
        public static String DelType;

        public static String Id;
        public static String Sm; // Segment
        public static String Am; // Attribute
        public static String Vm; // Value
        public static String Svm; // SubValue
        public static String Lvm;
        public static String Lsvm;
        public static String Lcsvm;

        public static int FieldActionLast;
        public static int FieldModeLast;
        public static String sFieldResultLast;

        public static int FieldId; // = "0";
        public static int FieldSm; // = ((char)255).ToString(); // Segment
        public static int FieldAm; // = ((char)254).ToString(); // Attribute
        public static int FieldVm; // = ((char)253).ToString(); // Value
        public static int FieldSvm; // = ((char)252).ToString();  // SubValue
        public static int FieldLvm; // = "*";
        public static int FieldLsvm; // = "@";
        public static int FieldLcsvm; // = ",";

        /// <summary> 
        /// Working fields and pointers
        /// </summary> 
        /// <param name=""></param> 
        /// <remarks></remarks> 
        public static int FieldEndCharacterIndex;
        public static int FieldAttributeIndex;
        public static int FieldLength;
        public static int FieldLocStart;
        public static int FieldLocEnd;
        #endregion
        
        #region Field Functions
        /// <summary>
        /// Returns the last occurance of fields delimited by the passed
		/// delimiter character(s).
        /// </summary> 
        /// <param name="PassedMickString">Extended string to examine</param> 
        /// <param name="PassedFieldChar">Field delimiter characters</param> 
		/// <returns></returns>
        /// <remarks></remarks> 
        public static String FieldLast(this String PassedMickString, String PassedFieldChar) {
            String sResult = PassedMickString;
            int iPassedFieldOccurence = -99999;
            sResult = ActionTheField(sResult, PassedFieldChar, iPassedFieldOccurence, (int)FieldActionIs.Get, (int)FieldModeIs.Extract, null);
            return sResult;
        }
        //public String FieldLast(this String PassedMickString, String PassedFieldChar) {
        //    return FieldLast(this, PassedFieldChar);
        //}

        /// <summary>
        /// Returns the extracted field occurance using the delimiter
        /// </summary> 
        /// <param name="PassedMickString">Extended string to examine</param> 
        /// <param name="PassedFieldChar">Field delimiter characters</param> 
        /// <param name="iPassedFieldOccurence">Which occurence to return</param> 
		/// <returns></returns>
        /// <remarks>
		/// Recommended functions are Field(Character, Occurence),
		/// GetField(Delimiter, Delimeter...), InsertField, and DeleteField.
		/// </remarks> 
        public static String Field(this String PassedMickString, String PassedFieldChar, int iPassedFieldOccurence) {
            String sResult = PassedMickString;
            sResult = ActionTheField(sResult, PassedFieldChar, iPassedFieldOccurence, (int)FieldActionIs.Get, (int)FieldModeIs.Extract, null);
            return sResult;
        }

        /// <summary>
        /// Returns the field number 0 (normally the ID or Unique ID of the field).
        /// </summary> 
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns>
		/// </returns>
        /// <remarks>
		/// </remarks> 
        public static int GetFieldId(this String PassedMickString) {
            return FieldId;
        }
        #endregion

        #region Count Functions
        /// <summary>
        /// Count character or delimiter passed
        /// </summary> 
        /// <param name="PassedMickString">Extended string to examine</param> 
        /// <param name="PassedFieldChar">Field delimiter characters</param> 
		/// <returns>
		/// </returns>
        /// <remarks>Count of a character or delimiter passed</remarks> 
        public static int Count(this String PassedMickString, String PassedFieldChar) {
            String sResult = ActionTheField(PassedMickString, PassedFieldChar, -99999, (int)FieldActionIs.Count, (int)FieldModeIs.None, null);
            return FieldAttributeIndex;
        }

        #endregion

        #region Split Functions
        /// <summary>
        /// Returns the extracted fields split at the last occurance of the delimiter
        /// </summary> 
        /// <param name="PassedMickString">Extended string to examine</param> 
        /// <param name="PassedFieldChar">Field delimiter characters</param> 
		/// <returns>A one dimensional array of 2 split results</returns>
        /// <remarks>Produces the result in a two element array</remarks> 
        public static String[] SplitLast(this String PassedMickString, String PassedFieldChar) {
            String[] sMiddle = new String[2];
            sMiddle = SplitInTwo(PassedMickString, PassedFieldChar, -99999);
            return sMiddle;
        }

        /// <summary>
        /// Returns the extracted fields split at occurance of the delimiter in a two element array
        /// </summary> 
        /// <param name="PassedMickString">Extended string to examine</param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
		/// <returns>A one dimensional array of 2 split results</returns>
        /// <remarks></remarks> 
        public static String[] SplitInTwo(this String PassedMickString, String PassedFieldChar, int iPassedFieldOccurence) {
            String sResult = PassedMickString;
            String[] sMiddle = new String[2];
            sResult = ActionTheField(PassedMickString, PassedFieldChar, iPassedFieldOccurence, (int)FieldActionIs.Get, (int)FieldModeIs.Split, sMiddle);
            return sMiddle;
        }
        #endregion

        #region Get Pointers
        /// <summary>
		/// Multivalued index pointer for a segement mark, block or file delimiter.
        /// Returns the level 0 segment block number (Level 1 is column).
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns>Pointer for a segement mark</returns>
        /// <remarks>First dimension of multidimensional array.</remarks> 
        public static int GetFieldSm(this String PassedMickString) {
            return FieldSm;
        }

        /// <Summary>
		/// Multivalued index pointer for the current field.
        /// Returns the level 1 field number (Level 1 is column).
        /// </Summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns></returns>
        /// <remarks>
		/// Although formally the second dimension of multidimensional 
		///  array within this strategy the first leve (segment) is rarely used.
		///  A file data record (or row) begins to be defined at this 
		///  level of separation.
		///  First dimension of multidimensional array.
		///  Equivalent to a record set array index or single dimension array
		///  by separating file fields or columns
		/// </remarks> 
        public static int GetFieldAm(this String PassedMickString) {
            return FieldAm;
        }

        /// <summary>
		/// Multivalued index pointer for current value field
        /// Returns the level 2 field number (Level 1 is column).
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns></returns>
        /// <remarks>Third dimension of multidimensional array.</remarks> 
        public static int GetFieldVm(this String PassedMickString) {
            return FieldVm;
        }

        /// <summary>
		/// Multivalued index pointer for current subvalue field
        /// Returns the level 3 field number (Level 1 is column).
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns>Pointer for current subvalue field</returns>
        /// <remarks>Fourth dimension of multidimensional array.</remarks> 
        public static int GetFieldSvm(this String PassedMickString) {
            return FieldSvm;
        }

        /// <summary>
		/// Multivalued index pointer for defined character separator
        /// Returns the level 4 field number (default = "*") (Level 1 is column).
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns>Pointer for defined character separator</returns>
        /// <remarks>Fifth dimension of multidimensional array.</remarks> 
        public static int GetFieldLvm(this String PassedMickString) {
            return FieldLvm;
        }

        /// <summary>
		/// Multivalued index pointer for 
        /// Returns the level 5 field number (default = "@") (Level 1 is column).
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns></returns>
        /// <remarks>Six dimension of multidimensional array.</remarks> 
        public static int GetFieldLsvm(this String PassedMickString) {
            return FieldLsvm;
        }

        /// <summary>
		/// Multivalued index pointer for 
        /// Returns the level 6 field number (default = ",") (Level 1 is column).
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns></returns>
        /// <remarks>Seventh dimension of multidimensional array.</remarks> 
        public static int GetFieldLcsvm(this String PassedMickString) {
            return FieldLcsvm;
        }
        #endregion

        #region Get Field Variables
        /// <summary>
		/// Ending character position index pointer for string.
        /// Returns the character pointer to the end of the current field
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns></returns>
        /// <remarks></remarks> 
        public static int GetFieldEndCharacterIndex(this String PassedMickString) {
            return FieldEndCharacterIndex;
        }

        /// <summary>
		/// Multivalued index pointer for the current field.
        /// Returns the level 1 field number (Level 1 is column).
        /// Returns the field number, line number or attribute number of the field
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns></returns>
        /// <remarks>
		///  First dimension of multidimensional array.
		///  Equivalent to a record set array index or single dimension array
		///  by separating file fields or columns
		/// </remarks> 
        public static int GetFieldAttributeIndex(this String PassedMickString) {
            return FieldAttributeIndex;
        }

        /// <summary>
        /// Returns the length of the field.
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns></returns>
        /// <remarks></remarks> 
        public static int GetFieldLength(this String PassedMickString) {
            return FieldLength;
        }

        /// <summary>
		/// Multivalued index pointer for 
        /// Returns the character pointer to the start of field.
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns></returns>
        /// <remarks></remarks> 
        public static int GetFieldLocStart(this String PassedMickString) {
            return FieldLocStart;
        }

        /// <summary>
		/// Multivalued index pointer for 
        /// Returns the character pointer to the end of the field.
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns>
		/// </returns>
        /// <remarks>
		/// </remarks> 
        public static int GetFieldLocEnd(this String PassedMickString) {
            return FieldLocEnd;
        }
        #endregion

        #region Statistics Report
        /// <summary>
        /// Returns detailed debug information resulting from Field functions.
		/// Includes current or resulting index values for the extended string.
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns>
		/// </returns>
        /// <remarks>
		/// </remarks> 
        public static String GetStatistics(this String PassedMickString) {
            String sTemp9;
            String sTemp8;
            sTemp9 = "Last Action(" + FieldActionLast.ToString() + ")";
            sTemp9 += ", Attribute (" + FieldAm.ToString();
            sTemp9 += "," + FieldVm.ToString();
            sTemp9 += "," + FieldSvm.ToString();
            sTemp9 += "," + FieldLvm.ToString();
            sTemp9 += "," + FieldLsvm.ToString();
            sTemp9 += "," + FieldLcsvm.ToString();
            sTemp9 += ")";
            sTemp9 += ", Started Location(" + FieldLocStart.ToString() + ")";
            sTemp9 += ", End Location(" + FieldLocEnd.ToString() + ")";
            sTemp9 += ", End Character Index(" + FieldEndCharacterIndex + ")";
            sTemp9 += ", Length(" + FieldLength.ToString() + ")";
            sTemp9 += ", Attribute Index(" + FieldAttributeIndex + ")";
            sTemp8 = sFieldResultLast;
            if (sTemp8.Length > 10) { sTemp8 = sTemp8.Substring(0, 10); }
            sTemp9 += ", Last Result First Ten(" + sTemp8 + ")";
            return sTemp9;
        }
        #endregion

        #region Delimeters
        /// <summary>
        /// Set the delimiters and replace default Am, Vm, Svm, Lvm, Lsvm
        /// </summary>
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
		/// <returns></returns>
        /// <remarks>This function is used to set the user defined field separators</remarks> 
        public static void SetDel(String DelTypePassed, String SmPassed, String AmPassed, String VmPassed, String SvmPassed, String LvmPassed, String LsvmPassed, String LcsvmPassed) {
            DelType = DelTypePassed;
            Sm = SmPassed; // Segment
            Am = AmPassed; // Attribute
            Vm = VmPassed; // Value
            Svm = SvmPassed;  // SubValue
            Lvm = LvmPassed;
            Lsvm = LsvmPassed;
            Lcsvm = LcsvmPassed;
        }

        /// <summary>
        /// Set the default Am, Vm, Svm, Lvm, Lsvm
        /// </summary>
		/// <returns></returns>
        /// <remarks></remarks> 
        public static void SetDelDefault() {
            DelType = "Pick";
            Sm = ((char)255).ToString(); // Segment
            Am = ((char)254).ToString(); // Attribute
            Vm = ((char)253).ToString(); // Value
            Svm = ((char)252).ToString();  // SubValue
            Lvm = "*";
            Lsvm = "@";
            Lcsvm = ",";
        }
        #endregion

        #region Get / Action the String
        /// <summary>
        /// Returns the passed string unaltered
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns></returns>
        /// <remarks></remarks> 
        public static String GetAll(this String PassedMickString)
        {
            return PassedMickString;
        }

        /// <summary>
        /// Set the working variables to their initialized values
        /// </summary>
		/// <returns></returns>
        /// <remarks></remarks> 
        public static void FieldReset() {
            FieldActionLast = 99999;
            sFieldResultLast = "";

            FieldId = 0; // = "0";
            FieldSm = 0; // = ((char)255).ToString(); // Segment
            FieldAm = 0; // = ((char)254).ToString(); // Attribute
            FieldVm = 0; // = ((char)253).ToString(); // Value
            FieldSvm = 0; // = ((char)252).ToString();  // SubValue
            FieldLvm = 0; // = "*";
            FieldLsvm = 0; // = "@";
            FieldLcsvm = 0; // = ",";

            FieldEndCharacterIndex = 0;
            FieldAttributeIndex = 0;
            FieldLength = 0;
            FieldLocStart = 0;
            FieldLocEnd = 0;
        }

        /// <summary>
        /// Field extraction Action 
		/// using the passed delimiter FieldChar 
		/// (Get, Delete, Drop, Insert, Add.)
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
		/// <returns></returns>
        /// <remarks></remarks> 
        public static String ActionTheField(
            this String PassedMickString, 
            String PassedFieldChar, 
            int iPassedFieldOccurence, 
            int iPassedFieldAction, 
            int iPassedFieldMode, 
            string[] sPassedFieldArr
            ) {
        // FUNCTION FIELD
        // private String PickField(String PassedMickString, String PassedFieldChar, int iPassedFieldOccurence)
        // {
            String sFieldTemp;
            String sFieldResult = "";
            if (iPassedFieldMode == (int)FieldModeIs.Split) {
                sPassedFieldArr[0] = "";
                sPassedFieldArr[1] = "";
            }
            // int FieldEndCharacterIndex = 0;
            // int FieldAttributeIndex = 0;
            FieldEndCharacterIndex = 99999;
            FieldAttributeIndex = 0;
            FieldLength = 0;
            FieldLocStart = 0;
            FieldLocEnd = 0;
            //For Loop
            // Add zero loop for id
            for (FieldAttributeIndex = 1; 
                ((FieldAttributeIndex <= iPassedFieldOccurence 
                || iPassedFieldOccurence == -99999) 
                && FieldEndCharacterIndex >= 0); 
                FieldAttributeIndex++
                ) {
                FieldEndCharacterIndex = PassedMickString.IndexOf(PassedFieldChar, FieldLocStart);
                if (FieldEndCharacterIndex == -1) {
                    // Use sFieldResult as is (to the end)
                    // sFieldResult = "";
                    switch(iPassedFieldAction){
                        case ((int)FieldActionIs.Count):
                            FieldAttributeIndex -= 1;
                            return "";
                        case((int)FieldActionIs.Get):
                            FieldLocEnd = PassedMickString.Length;
                            if (FieldAttributeIndex == iPassedFieldOccurence || iPassedFieldOccurence == -99999) {
                                if (iPassedFieldOccurence > 1 || iPassedFieldOccurence == -99999) {
                                    if (FieldLocEnd > FieldLocStart && PassedMickString.Substring(FieldLocStart, 1) == PassedFieldChar) { FieldLocStart += 1; }
                                    // if (FieldLocStart < 0) { FieldLocStart = 0; }
                                    // if (PassedMickString.Substring(FieldLocEnd,1) == PassedFieldChar) { FieldLocEnd -= 1; }
                                    // if (FieldLocEnd < 0) { FieldLocEnd = 0; }
                                    if (FieldLocEnd < FieldLocStart) { FieldLocEnd = FieldLocStart; }
                                    FieldLength = FieldLocEnd - FieldLocStart;
                                    if (iPassedFieldMode == (int)FieldModeIs.Split) {
                                        if (iPassedFieldOccurence == -99999) {
                                            if (FieldLocStart > 0) {
                                                sPassedFieldArr[0] = PassedMickString.Substring(0, FieldLocStart - 1);
                                                if (FieldLength > 1) {
                                                    sPassedFieldArr[1] = PassedMickString.Substring(FieldLocStart, FieldLength);
                                                } else { sPassedFieldArr[1] = ""; }
                                            } else {
                                                sPassedFieldArr[0] = PassedMickString;
                                                sPassedFieldArr[1] = "";
                                            }
                                        } else {
                                            sPassedFieldArr[0] = PassedMickString;
                                            sPassedFieldArr[1] = "";
                                        }
                                    } else {
                                        if (FieldLength > 0) {
                                            sFieldResult = PassedMickString.Substring(FieldLocStart, FieldLength);
                                        } else { sFieldResult = ""; }
                                    }
                                } else {
                                    if (iPassedFieldMode == (int)FieldModeIs.Split) {
                                        sPassedFieldArr[0] = PassedMickString;
                                    } else {
                                        sFieldResult = PassedMickString;
                                    }
                                }
                            } else { sFieldResult = ""; }
                            break;
                        case((int)FieldActionIs.Add):
                            sFieldResult = PassedMickString;
                            break;
                        case((int)FieldActionIs.Delete):
                            sFieldResult = PassedMickString;
                            break;
                        default:
                            sFieldResult = PassedMickString;
                            break;
                    }
                    break;
                } else if (FieldEndCharacterIndex >= 0 && FieldAttributeIndex < iPassedFieldOccurence || iPassedFieldOccurence == -99999) {
                    switch(iPassedFieldAction){
                        case ((int)FieldActionIs.Count):
                        case ((int)FieldActionIs.Get):
                            // sFieldResult = sFieldResult.Substring(FieldEndCharacterIndex + 1);
                            FieldLocStart = FieldEndCharacterIndex + 1;
                            if (FieldLocStart > PassedMickString.Length - 1) {
                                FieldLocStart = PassedMickString.Length - 1;
                            }
                            break;
                        case((int)FieldActionIs.Add):
                            sFieldResult = PassedMickString;
                            break;
                        case((int)FieldActionIs.Delete):
                            sFieldResult = PassedMickString;
                            break;
                        default:
                            sFieldResult = PassedMickString;
                            break;
                    }
                } else if (FieldEndCharacterIndex >= 0 && FieldAttributeIndex == iPassedFieldOccurence) {
                    switch (iPassedFieldAction) {
                        case ((int)FieldActionIs.Count):
                        case ((int)FieldActionIs.Get):
                            FieldLocEnd = FieldEndCharacterIndex;
                            // if (PassedMickString.Substring(FieldLocEnd, 1) == PassedFieldChar) { FieldLocEnd -= 1; }
                            // if (FieldLocEnd < 0) { FieldLocEnd = 0; }
                            if (PassedMickString.Substring(FieldLocStart, 1) == PassedFieldChar) { FieldLocStart += 1; }
                            if (FieldLocStart < 0) { FieldLocStart = 0; }
                            if (FieldLocEnd < FieldLocStart) { FieldLocEnd = FieldLocStart; }
                            if (iPassedFieldMode == (int)FieldModeIs.Split) {
                                if (FieldEndCharacterIndex >= 0 && FieldLocEnd > 0) {
                                    sPassedFieldArr[0] = PassedMickString.Substring(0, FieldLocEnd);
                                    FieldLocStart = FieldLocEnd + 1;
                                    FieldLocEnd = PassedMickString.Length;
                                    if (FieldLocStart < FieldLocEnd) {
                                        FieldLength = FieldLocEnd - FieldLocStart;
                                        sPassedFieldArr[1] = PassedMickString.Substring(FieldLocStart, FieldLength);
                                    } else {
                                        sPassedFieldArr[1] = "";
                                    }
                                } else {
                                    sPassedFieldArr[0] = PassedMickString;
                                    sPassedFieldArr[1] = "";
                                }
                                return "OK";
                            } else {
                                FieldLength = FieldLocEnd - FieldLocStart;
                                if (FieldLength > 0) {
                                    sFieldResult = PassedMickString.Substring(FieldLocStart, FieldLength);
                                } else { sFieldResult = ""; }
                            }
                            break;
                        case((int)FieldActionIs.Add):
                            sFieldResult = PassedMickString;
                            break;
                        case((int)FieldActionIs.Delete):
                            sFieldResult = PassedMickString;
                            break;
                        default:
                            sFieldResult = PassedMickString;
                            break;
                    }
                }
            } // end of for
            FieldLength = sFieldResult.Length;
            sFieldResultLast = sFieldResult;
            return sFieldResult;
        }
        //
        // Overloads The Value Requested
        //
        /// <summary>
        /// Field extraction Action 
		/// using 1 Attr, 2 Value, 3 Sub-value, 4 List Value, 5 List Sub-Value, 6 List Character Value 
		/// (Get, Delete, Drop, Insert, Add.)
        /// </summary>
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
        /// <param name=""></param> 
		/// <returns></returns>
        /// <remarks>
		/// Recommended functions are Field(Character, Occurence)
		/// and GetField(Delimiter, Delimeter...)
		/// </remarks> 
        public static String ActionTheField(this String PassedMickString, int iPassedFieldLevel, int iPassedAttibute, int iPassedMultivalue, int iPasseedSubvalue, int iPassedListValue, int iPassedListSubvalue, int iPassedListCsv, int iPassedFieldAction, int iPassedFieldMode, string[] sPassedFieldArr)
        {
            if (DelType == null) { 
                SetDelDefault();
                FieldReset();
            }
            //
            FieldId = 0; // = "0";
            FieldSm = 0; // = ((char)255).ToString(); // Segment
            FieldAm = iPassedAttibute; // = ((char)254).ToString(); // Attribute
            FieldVm = iPassedMultivalue; // = ((char)253).ToString(); // Value
            FieldSvm = iPasseedSubvalue; // = ((char)252).ToString();  // SubValue
            FieldLvm = iPassedListValue; // = "*";
            FieldLsvm = iPassedListSubvalue; // = "@";
            FieldLcsvm = iPassedListCsv; // = ",";

            FieldActionLast = iPassedFieldAction;
            FieldModeLast = iPassedFieldMode;


            // enum MultiValues : String { Id = "0", Am = ((char)254).ToString(), Vm = ((char)253).ToString(), Svm = ((char)252).ToString(), Lvm = "*", Lsvm = "@", Lcsvm = ",", LcsvmSm = ((char)255).ToString( };
            //
            String sResult = PassedMickString;
            int iTempFieldMode;
            int iFieldLevel;
            //
            if (iPassedAttibute > 0) {
                iFieldLevel = 1; 
                if (iPassedFieldLevel == iFieldLevel) { iTempFieldMode = iPassedFieldMode; } else { iTempFieldMode = (int) FieldModeIs.Extract; }
                sResult = ActionTheField(sResult, Am, iPassedAttibute, iPassedFieldAction, iPassedFieldMode, sPassedFieldArr);
            }
            if (iPassedMultivalue > 0) {
                iFieldLevel = 2;
                if (iPassedFieldLevel == iFieldLevel) { iTempFieldMode = iPassedFieldMode; } else { iTempFieldMode = (int)FieldModeIs.Extract; }
                sResult = ActionTheField(sResult, Vm, iPassedMultivalue, iPassedFieldAction, iPassedFieldMode, sPassedFieldArr);
            }
            if (iPasseedSubvalue > 0) {
                iFieldLevel = 3;
                if (iPassedFieldLevel == iFieldLevel) { iTempFieldMode = iPassedFieldMode; } else { iTempFieldMode = (int)FieldModeIs.Extract; }
                sResult = ActionTheField(sResult, Svm, iPasseedSubvalue, iPassedFieldAction, iPassedFieldMode, sPassedFieldArr);
            }
            if (iPassedListValue > 0) {
                iFieldLevel = 4;
                if (iPassedFieldLevel == iFieldLevel) { iTempFieldMode = iPassedFieldMode; } else { iTempFieldMode = (int)FieldModeIs.Extract; }
                sResult = ActionTheField(sResult, Lvm, iPassedListValue, iPassedFieldAction, iPassedFieldMode, sPassedFieldArr);
            }
            if (iPassedListSubvalue > 0) {
                iFieldLevel = 5;
                if (iPassedFieldLevel == iFieldLevel) { iTempFieldMode = iPassedFieldMode; } else { iTempFieldMode = (int)FieldModeIs.Extract; }
                sResult = ActionTheField(sResult, Lsvm, iPassedListSubvalue, iPassedFieldAction, iPassedFieldMode, sPassedFieldArr);
            }
            if (iPassedListCsv > 0) {
                iFieldLevel = 6;
                if (iPassedFieldLevel == iFieldLevel) { iTempFieldMode = iPassedFieldMode; } else { iTempFieldMode = (int)FieldModeIs.Extract; }
                sResult = ActionTheField(sResult, Lcsvm, iPassedListSubvalue, iPassedFieldAction, iPassedFieldMode, sPassedFieldArr);
            }
            return sResult;
        }

        #endregion
        #region Get Multivalued Field Overloads
        //
        // Overloads ListCsv
        //
        /// <summary>
        /// Field extraction Get Action using 
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
        /// <param name="">1 Attr, 2 Value, 3 Sub-value, 4 List Value, 5 List Sub-Value, 6 List Character Value </param> 
        /// <param name="">(Get, Delete, Drop, Insert, Add.)</param> 
        /// <param name=""></param> 
		/// <returns>
		/// </returns>
        /// <remarks>
		/// </remarks> 
        public static String GetField(this String PassedMickString, int iPassedAttibute, int iPassedMultivalue, int iPasseedSubvalue, int iPassedListValue, int iPassedListSubvalue, int iPassedListCsv, int GET) {
            return ActionTheField(PassedMickString, 5, iPassedAttibute, iPassedMultivalue, iPasseedSubvalue, iPassedListValue, iPassedListSubvalue, iPassedListCsv, (int)FieldActionIs.Get, (int)FieldModeIs.Extract, null);
        }
        //
        // Overloads ListSubvalue
        //
        /// <summary>
        /// Field extraction Get Action 
		/// using 1 Attr, 2 Value, 3 Sub-value, 4 List Value, 5 List Sub-Value (Get, Delete, Drop, Insert, Add.)
        /// </summary>
        /// <param name="PassedMickString">Extended string to examine</param> 
		/// <returns>
		/// </returns>
        /// <remarks>
		/// </remarks> 
        public static String GetField(this String PassedMickString, int iPassedAttibute, int iPassedMultivalue, int iPasseedSubvalue, int iPassedListValue, int iPassedListSubvalue, int GET) {
            return ActionTheField(PassedMickString, 5, iPassedAttibute, iPassedMultivalue, iPasseedSubvalue, iPassedListValue, iPassedListSubvalue, 0, (int)FieldActionIs.Get, (int)FieldModeIs.Extract, null);
        }
        //
        // Overloads ListValue
        //
        /// <summary>
        /// Field extraction Get Action 
		/// using 1 Attr, 2 Value, 3 Sub-value, 4 List Value (Get, Delete, Drop, Insert, Add.)
        /// </summary>
		/// <returns>
		/// </returns>
        /// <remarks>
		/// </remarks> 
        public static String GetField(this String PassedMickString, int iPassedAttibute, int iPassedMultivalue, int iPasseedSubvalue, int iPassedListValue, int GET) {
            return ActionTheField(PassedMickString, 4, iPassedAttibute, iPassedMultivalue, iPasseedSubvalue, iPassedListValue, 0, 0, (int)FieldActionIs.Get, (int)FieldModeIs.Extract, null);
        }

        //
        // Overloads Subvalue
        //
        /// <summary>
        /// Field extraction Get Action 
		/// using 1 Attr, 2 Value, 3 Sub-value (Get, Delete, Drop, Insert, Add.)
        /// </summary>
		/// <returns>
		/// </returns>
        /// <remarks>
		/// </remarks> 
        public static String GetField(this String PassedMickString, int iPassedAttibute, int iPassedMultivalue, int iPasseedSubvalue, int GET) {
            return ActionTheField(PassedMickString, 3, iPassedAttibute, iPassedMultivalue, iPasseedSubvalue, 0, 0, 0, (int)FieldActionIs.Get, (int)FieldModeIs.Extract, null);
        }

        //
        // Overloads Multivalue
        //
        /// <summary>
        /// Field extraction Get Action 
		/// using 1 Attr, 2 Value (Get, Delete, Drop, Insert, Add.)
        /// </summary>
		/// <returns>
		/// </returns>
        /// <remarks>
		/// </remarks> 
        public static String GetField(this String PassedMickString, int iPassedAttibute, int iPassedMultivalue, int GET) {
            return ActionTheField(PassedMickString, 2, iPassedAttibute, iPassedMultivalue, 0, 0, 0, 0, (int)FieldActionIs.Get, (int)FieldModeIs.Extract, null);
        }

        //
        // Overloads Attribue Value
        //
        /// <summary>
        /// Field extraction Get Action 
		/// using 1 Attr (Get, Delete, Drop, Insert, Add.)
        /// </summary>
		/// <returns>
		/// </returns>
        /// <remarks>
		/// </remarks> 
        public static String GetField(this String PassedMickString, int iPassedAttibute) {
            return ActionTheField(PassedMickString, 1, iPassedAttibute, 0, 0, 0, 0, 0, (int)FieldActionIs.Get, (int)FieldModeIs.Extract, null);
        }

        /// <summary>
        /// Field extraction Delete Action 
		/// using 1 Attr (Get, Delete, Drop, Insert, Add.)
        /// </summary>
		/// <returns>
		/// </returns>
        /// <remarks>
		/// </remarks> 
        public static String DeleteField(this String PassedMickString, int iPassedAttibute) {
            return ActionTheField(PassedMickString, 1, iPassedAttibute, 0, 0, 0, 0, 0, (int)FieldActionIs.Delete, (int)FieldModeIs.Extract, null);
        }

        /// <summary>
        /// Field extraction Insert Action 
		/// using 1 Attr (Get, Delete, Drop, Insert, Add.)
        /// </summary>
		/// <returns>
		/// </returns>
        /// <remarks>
		/// </remarks> 
        public static String InsertField(this String PassedMickString, int iPassedAttibute) {
            return ActionTheField(PassedMickString, 1, iPassedAttibute, 0, 0, 0, 0, 0, (int)FieldActionIs.Insert, (int)FieldModeIs.Extract, null);
        }
        #endregion
    }
}
