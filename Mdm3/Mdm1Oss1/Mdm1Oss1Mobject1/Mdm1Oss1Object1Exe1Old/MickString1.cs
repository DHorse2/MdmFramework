using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
// using    Mdm.Oss.ClipboardUtil;
// using    Mdm.Oss.Code;
// using    Mdm.Oss.Support;
// this     Mdm.Oss.Mapp
// using    Mdm.Oss.Mobj;
// using    Mdm.Oss.FileUtil;
// using    Mdm1Oss1FileCreation1;


namespace Mdm.Oss.Mobj
{
    public static class MickString1
    {
        /* 
        private string sPassedMickString = "";

        MickString1() : base()
        {
            sPassedMickString = "";
        }

        MickString1(string sPassedString) : base()
        {
            sPassedMickString = sPassedString;
        }
        */
        /*
        public string MickString 
        {
            get 
            {
                return sPassedMickString; 
            } 

            set
            { 
                sPassedMickString = value; 
            } 

        }

        public int IndexOf(this String sPassedMickString, string sPassedFieldChar, int iPassedLocCurrent)
        {
            int iTemp = 0;
            iTemp = (sPassedMickString.ToString()).IndexOf(sPassedFieldChar, iPassedLocCurrent);
            return iTemp;
        }

        public int Length(this String sPassedMickString)
        {
            int iTemp = 0;
            iTemp = (sPassedMickString.ToString()).Length;
            return iTemp;
        }

        public string Substring(this String sPassedMickStringint iPassedFieldEndCharacterIndex)
        {
            String sFieldResult = "";
            sFieldResult = (sPassedMickString.ToString()).Substring(iPassedFieldEndCharacterIndex);
            return sFieldResult;
        }
        */
        const int FIELD_GET = 1;
        const int FIELD_DELETE = 2;
        const int FIELD_DROP = 2;
        const int FIELD_INSERT = 3;
        const int FIELD_ADD = 3;

        public static int iFieldActionLast = 99999;
        public static String sFieldResultLast = "";

        public static int iFieldId = 0; // = "0";
        public static int iFieldSm = 0; // = ((char)255).ToString(); // Segment
        public static int iFieldAm = 0; // = ((char)254).ToString(); // Attribute
        public static int iFieldVm = 0; // = ((char)253).ToString(); // Value
        public static int iFieldSvm = 0; // = ((char)252).ToString();  // SubValue
        public static int iFieldLvm = 0; // = "*";
        public static int iFieldLsvm = 0; // = "@";
        public static int iFieldLcsvm = 0; // = ",";



        public static int iFieldEndCharacterIndex = 0;
        public static int iFieldAttributeIndex = 0;
        public static int iFieldLength = 0;
        public static int iFieldLocStart = 0;
        public static int iFieldLocEnd = 0;

        public static int GetFieldId(this String sPassedMickString) {
            return iFieldId;
        }

        public static int GetFieldSm(this String sPassedMickString) {
            return iFieldSm;
        }

        public static int GetFieldAm(this String sPassedMickString) {
            return iFieldAm;
        }

        public static int GetFieldVm(this String sPassedMickString) {
            return iFieldVm;
        }

        public static int GetFieldSvm(this String sPassedMickString) {
            return iFieldSvm;
        }

        public static int GetFieldLsvm(this String sPassedMickString) {
            return iFieldLsvm;
        }

        public static int GetFieldLcsvm(this String sPassedMickString) {
            return iFieldLcsvm;
        }

        public static int GetFieldLvm(this String sPassedMickString) {
            return iFieldLvm;
        }


        public static int GetFieldEndCharacterIndex(this String sPassedMickString) {
            return iFieldEndCharacterIndex;
        }

        public static int GetFieldAttributeIndex(this String sPassedMickString) {
            return iFieldAttributeIndex;
        }

        public static int GetFieldLength(this String sPassedMickString) {
            return iFieldLength;
        }

        public static int GetFieldLocStart(this String sPassedMickString) {
            return iFieldLocStart;
        }

        public static int GetFieldLocEnd(this String sPassedMickString) {
            return iFieldLocEnd;
        }

        public static String GetStatistics(this String sPassedMickString) {
            string sTemp9;
            string sTemp8;
            sTemp9 = "Last Action(" + iFieldActionLast.ToString() + ")";
            sTemp9 += ", Attribute (" + iFieldAm.ToString();
            sTemp9 += "," + iFieldVm.ToString();
            sTemp9 += "," + iFieldSvm.ToString();
            sTemp9 += "," + iFieldLvm.ToString();
            sTemp9 += "," + iFieldLsvm.ToString();
            sTemp9 += "," + iFieldLcsvm.ToString();
            sTemp9 += ")";
            sTemp9 += ", Start Location(" + iFieldLocStart.ToString() + ")";
            sTemp9 += ", End Location(" + iFieldLocEnd.ToString() + ")";
            sTemp9 += ", End Character Index(" + iFieldEndCharacterIndex + ")";
            sTemp9 += ", Length(" + iFieldLength.ToString() + ")";
            sTemp9 += ", Attribute Index(" + iFieldAttributeIndex + ")";
            sTemp8 = sFieldResultLast;
            if (sTemp8.Length > 10) { sTemp8 = sTemp8.Substring(0, 10); }
            sTemp9 += ", Last Result First Ten(" + sTemp8 + ")";
            return sTemp9;
        }

        public static String GetAll(this String sPassedMickString)
        {
            return sPassedMickString;
        }

        public static String ActionTheField(this String sPassedMickString, string sPassedFieldChar, int iPassedFieldOccurence, int iPassedFieldAction)
        {
        // FUNCTION FIELD
        // private string PickField(string sPassedMickString, string sPassedFieldChar, int iPassedFieldOccurence)
        // {
            string sFieldTemp = "";
            string sFieldResult = "";
            // int iFieldEndCharacterIndex = 0;
            // int iFieldAttributeIndex = 0;
            iFieldEndCharacterIndex = 99999;
            iFieldAttributeIndex = 0;
            iFieldLength = 0;
            iFieldLocStart = 0;
            iFieldLocEnd = 0;
            //For Loop
            // Add zero loop for id
            for (iFieldAttributeIndex = 1; (iFieldAttributeIndex <= iPassedFieldOccurence && iFieldEndCharacterIndex >= 0); iFieldAttributeIndex++) 
            {
                iFieldEndCharacterIndex = sPassedMickString.IndexOf(sPassedFieldChar, iFieldLocStart);
                if (iFieldEndCharacterIndex == -1) {
                    // Use sFieldResult as is (to the end)
                    // sFieldResult = "";
                    switch(iPassedFieldAction){
                        case(FIELD_GET):
                            if (iFieldAttributeIndex < iPassedFieldOccurence) {
                                iFieldLocEnd = sPassedMickString.Length;
                                if (iPassedFieldOccurence > 1) {
                                    if (iFieldLocEnd > iFieldLocStart && sPassedMickString.Substring(iFieldLocStart, 1) == sPassedFieldChar) { iFieldLocStart += 1; }
                                    // if (iFieldLocStart < 0) { iFieldLocStart = 0; }
                                    // if (sPassedMickString.Substring(iFieldLocEnd,1) == sPassedFieldChar) { iFieldLocEnd -= 1; }
                                    // if (iFieldLocEnd < 0) { iFieldLocEnd = 0; }
                                    if (iFieldLocEnd < iFieldLocStart) { iFieldLocEnd = iFieldLocStart; }
                                    iFieldLength = iFieldLocEnd - iFieldLocStart;
                                    if (iFieldLength > 0) {
                                        sFieldResult = sPassedMickString.Substring(iFieldLocStart, iFieldLength);
                                    } else { sFieldResult = ""; }
                                } else { sFieldResult = ""; }
                            } else { sFieldResult = ""; }
                            break;
                        case(FIELD_ADD):
                            sFieldResult = sPassedMickString;
                            break;
                        case(FIELD_DELETE):
                            sFieldResult = sPassedMickString;
                            break;
                        default:
                            sFieldResult = sPassedMickString;
                            break;
                    }
                    break;
                } else if (iFieldEndCharacterIndex >= 0 && iFieldAttributeIndex < iPassedFieldOccurence) {
                    switch(iPassedFieldAction){
                        case(FIELD_GET):
                            // sFieldResult = sFieldResult.Substring(iFieldEndCharacterIndex + 1);
                            iFieldLocStart = iFieldEndCharacterIndex + 1;
                            if (iFieldLocStart > sPassedMickString.Length - 1) {
                                iFieldLocStart = sPassedMickString.Length - 1;
                            }
                            break;
                        case(FIELD_ADD):
                            sFieldResult = sPassedMickString;
                            break;
                        case(FIELD_DELETE):
                            sFieldResult = sPassedMickString;
                            break;
                        default:
                            sFieldResult = sPassedMickString;
                            break;
                    }
                } else if (iFieldEndCharacterIndex >= 0 && iFieldAttributeIndex == iPassedFieldOccurence) {
                    switch (iPassedFieldAction) {
                        case (FIELD_GET):
                            iFieldLocEnd = iFieldEndCharacterIndex;
                            // if (sPassedMickString.Substring(iFieldLocEnd, 1) == sPassedFieldChar) { iFieldLocEnd -= 1; }
                            // if (iFieldLocEnd < 0) { iFieldLocEnd = 0; }
                            if (sPassedMickString.Substring(iFieldLocStart, 1) == sPassedFieldChar) { iFieldLocStart += 1; }
                            if (iFieldLocStart < 0) { iFieldLocStart = 0; }
                            if (iFieldLocEnd < iFieldLocStart) { iFieldLocEnd = iFieldLocStart; }
                            iFieldLength = iFieldLocEnd - iFieldLocStart;
                            if (iFieldLength > 0) {
                                sFieldResult = sPassedMickString.Substring(iFieldLocStart, iFieldLength);
                            } else { sFieldResult = ""; }
                            break;
                        case (FIELD_ADD):
                            sFieldResult = sPassedMickString;
                            break;
                        case (FIELD_DELETE):
                            sFieldResult = sPassedMickString;
                            break;
                        default:
                            sFieldResult = sPassedMickString;
                            break;
                    }
                }
            } // end of for
            iFieldLength = sFieldResult.Length;
            sFieldResultLast = sFieldResult;
            return sFieldResult;
        }
        //
        // Overloads The Value Requested
        //
        public static String ActionTheField(this String sPassedMickString, int iPassedAttibute, int iPassedMultivalue, int iPasseedSubvalue, int iPassedListValue, int iPassedListSubvalue, int iPassedListCsv, int iPassedFieldAction)
        {
            //
            string cbId =  "0";
            string cbSm = ((char)255).ToString(); // Segment
            string cbAm = ((char)254).ToString(); // Attribute
            string cbVm = ((char)253).ToString(); // Value
            string cbSvm = ((char)252).ToString();  // SubValue
            string cbLvm = "*";
            string cbLsvm = "@";
            string cbLcsvm = ",";

            iFieldId = 0; // = "0";
            iFieldSm = 0; // = ((char)255).ToString(); // Segment
            iFieldAm = iPassedAttibute; // = ((char)254).ToString(); // Attribute
            iFieldVm = iPassedMultivalue; // = ((char)253).ToString(); // Value
            iFieldSvm = iPasseedSubvalue; // = ((char)252).ToString();  // SubValue
            iFieldLvm = iPassedListValue; // = "*";
            iFieldLsvm = iPassedListSubvalue; // = "@";
            iFieldLcsvm = iPassedListCsv; // = ",";

            iFieldActionLast = iPassedFieldAction;


            // enum cbMultiValues : string { cbId = "0", cbAm = ((char)254).ToString(), cbVm = ((char)253).ToString(), cbSvm = ((char)252).ToString(), cbLvm = "*", cbLsvm = "@", cbLcsvm = ",", cbLcsvmcbSm = ((char)255).ToString( };
            //
            string sResult = sPassedMickString;
            if (iPassedAttibute > 0)
            {
                sResult = ActionTheField(sResult, cbAm, iPassedAttibute, iPassedFieldAction);
            }
            if (iPassedMultivalue > 0)
            {
                sResult = ActionTheField(sResult, cbVm, iPassedMultivalue, iPassedFieldAction);
            }
            if (iPasseedSubvalue > 0)
            {
                sResult = ActionTheField(sResult, cbSvm, iPasseedSubvalue, iPassedFieldAction);
            }
            if (iPassedListValue > 0)
            {
                sResult = ActionTheField(sResult, cbLvm, iPassedListValue, iPassedFieldAction);
            }
            if (iPassedListSubvalue > 0)
            {
                sResult = ActionTheField(sResult, cbLsvm, iPassedListSubvalue, iPassedFieldAction);
            }
            return sResult;

        }

        //
        // Overloads ListCsv
        //
        public static String GetField(this String sPassedMickString, int iPassedAttibute, int iPassedMultivalue, int iPasseedSubvalue, int iPassedListValue, int iPassedListSubvalue, int iPassedListCsv, int FIELD_GET) {
            return ActionTheField(sPassedMickString, iPassedAttibute, iPassedMultivalue, iPasseedSubvalue, iPassedListValue, iPassedListSubvalue, iPassedListCsv, FIELD_GET);
        }
        //
        // Overloads ListSubvalue
        //
        public static String GetField(this String sPassedMickString, int iPassedAttibute, int iPassedMultivalue, int iPasseedSubvalue, int iPassedListValue, int iPassedListSubvalue, int FIELD_GET) {
            return ActionTheField(sPassedMickString, iPassedAttibute, iPassedMultivalue, iPasseedSubvalue, iPassedListValue, iPassedListSubvalue, 0, FIELD_GET);
        }
        //
        // Overloads ListValue
        //
        public static String GetField(this String sPassedMickString, int iPassedAttibute, int iPassedMultivalue, int iPasseedSubvalue, int iPassedListValue, int FIELD_GET) {
            return ActionTheField(sPassedMickString, iPassedAttibute, iPassedMultivalue, iPasseedSubvalue, iPassedListValue, 0, 0, FIELD_GET);
        }

        //
        // Overloads Subvalue
        //
        public static String GetField(this String sPassedMickString, int iPassedAttibute, int iPassedMultivalue, int iPasseedSubvalue, int FIELD_GET) {
            return ActionTheField(sPassedMickString, iPassedAttibute, iPassedMultivalue, iPasseedSubvalue, 0, 0, 0, FIELD_GET);
        }

        //
        // Overloads Multivalue
        //
        public static String GetField(this String sPassedMickString, int iPassedAttibute, int iPassedMultivalue, int FIELD_GET) {
            return ActionTheField(sPassedMickString, iPassedAttibute, iPassedMultivalue, 0, 0, 0, 0, FIELD_GET);
        }

        //
        // Overloads Attribue Value
        //
        public static String GetField(this String sPassedMickString, int iPassedAttibute) {
            return ActionTheField(sPassedMickString, iPassedAttibute, 0, 0, 0, 0, 0, FIELD_GET);
        }

        public static String DeleteField(this String sPassedMickString, int iPassedAttibute) {
            return ActionTheField(sPassedMickString, iPassedAttibute, 0, 0, 0, 0, 0, FIELD_DELETE);
        }

        public static String InsertField(this String sPassedMickString, int iPassedAttibute) {
            return ActionTheField(sPassedMickString, iPassedAttibute, 0, 0, 0, 0, 0, FIELD_INSERT);
        }

    }
}
