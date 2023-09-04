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

        public static int FieldActionLast = 99999;
        public static String sFieldResultLast = "";

        public static int FieldId = 0; // = "0";
        public static int FieldSm = 0; // = ((char)255).ToString(); // Segment
        public static int FieldAm = 0; // = ((char)254).ToString(); // Attribute
        public static int FieldVm = 0; // = ((char)253).ToString(); // Value
        public static int FieldSvm = 0; // = ((char)252).ToString();  // SubValue
        public static int FieldLvm = 0; // = "*";
        public static int FieldLsvm = 0; // = "@";
        public static int FieldLcsvm = 0; // = ",";



        public static int FieldEndCharacterIndex = 0;
        public static int FieldAttributeIndex = 0;
        public static int FieldLength = 0;
        public static int FieldLocStart = 0;
        public static int FieldLocEnd = 0;

        public static int GetFieldId(this String sPassedMickString) {
            return FieldId;
        }

        public static int GetFieldSm(this String sPassedMickString) {
            return FieldSm;
        }

        public static int GetFieldAm(this String sPassedMickString) {
            return FieldAm;
        }

        public static int GetFieldVm(this String sPassedMickString) {
            return FieldVm;
        }

        public static int GetFieldSvm(this String sPassedMickString) {
            return FieldSvm;
        }

        public static int GetFieldLsvm(this String sPassedMickString) {
            return FieldLsvm;
        }

        public static int GetFieldLcsvm(this String sPassedMickString) {
            return FieldLcsvm;
        }

        public static int GetFieldLvm(this String sPassedMickString) {
            return FieldLvm;
        }


        public static int GetFieldEndCharacterIndex(this String sPassedMickString) {
            return FieldEndCharacterIndex;
        }

        public static int GetFieldAttributeIndex(this String sPassedMickString) {
            return FieldAttributeIndex;
        }

        public static int GetFieldLength(this String sPassedMickString) {
            return FieldLength;
        }

        public static int GetFieldLocStart(this String sPassedMickString) {
            return FieldLocStart;
        }

        public static int GetFieldLocEnd(this String sPassedMickString) {
            return FieldLocEnd;
        }

        public static String GetStatistics(this String sPassedMickString) {
            string sTemp9;
            string sTemp8;
            sTemp9 = "Last Action(" + FieldActionLast.ToString() + ")";
            sTemp9 += ", Attribute (" + FieldAm.ToString();
            sTemp9 += "," + FieldVm.ToString();
            sTemp9 += "," + FieldSvm.ToString();
            sTemp9 += "," + FieldLvm.ToString();
            sTemp9 += "," + FieldLsvm.ToString();
            sTemp9 += "," + FieldLcsvm.ToString();
            sTemp9 += ")";
            sTemp9 += ", Start Location(" + FieldLocStart.ToString() + ")";
            sTemp9 += ", End Location(" + FieldLocEnd.ToString() + ")";
            sTemp9 += ", End Character Index(" + FieldEndCharacterIndex + ")";
            sTemp9 += ", Length(" + FieldLength.ToString() + ")";
            sTemp9 += ", Attribute Index(" + FieldAttributeIndex + ")";
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
            // int FieldEndCharacterIndex = 0;
            // int FieldAttributeIndex = 0;
            FieldEndCharacterIndex = 99999;
            FieldAttributeIndex = 0;
            FieldLength = 0;
            FieldLocStart = 0;
            FieldLocEnd = 0;
            //For Loop
            // Add zero loop for id
            for (FieldAttributeIndex = 1; (FieldAttributeIndex <= iPassedFieldOccurence && FieldEndCharacterIndex >= 0); FieldAttributeIndex++) 
            {
                FieldEndCharacterIndex = sPassedMickString.IndexOf(sPassedFieldChar, FieldLocStart);
                if (FieldEndCharacterIndex == -1) {
                    // Use sFieldResult as is (to the end)
                    // sFieldResult = "";
                    switch(iPassedFieldAction){
                        case(FIELD_GET):
                            if (FieldAttributeIndex < iPassedFieldOccurence) {
                                FieldLocEnd = sPassedMickString.Length;
                                if (iPassedFieldOccurence > 1) {
                                    if (FieldLocEnd > FieldLocStart && sPassedMickString.Substring(FieldLocStart, 1) == sPassedFieldChar) { FieldLocStart += 1; }
                                    // if (FieldLocStart < 0) { FieldLocStart = 0; }
                                    // if (sPassedMickString.Substring(FieldLocEnd,1) == sPassedFieldChar) { FieldLocEnd -= 1; }
                                    // if (FieldLocEnd < 0) { FieldLocEnd = 0; }
                                    if (FieldLocEnd < FieldLocStart) { FieldLocEnd = FieldLocStart; }
                                    FieldLength = FieldLocEnd - FieldLocStart;
                                    if (FieldLength > 0) {
                                        sFieldResult = sPassedMickString.Substring(FieldLocStart, FieldLength);
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
                } else if (FieldEndCharacterIndex >= 0 && FieldAttributeIndex < iPassedFieldOccurence) {
                    switch(iPassedFieldAction){
                        case(FIELD_GET):
                            // sFieldResult = sFieldResult.Substring(FieldEndCharacterIndex + 1);
                            FieldLocStart = FieldEndCharacterIndex + 1;
                            if (FieldLocStart > sPassedMickString.Length - 1) {
                                FieldLocStart = sPassedMickString.Length - 1;
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
                } else if (FieldEndCharacterIndex >= 0 && FieldAttributeIndex == iPassedFieldOccurence) {
                    switch (iPassedFieldAction) {
                        case (FIELD_GET):
                            FieldLocEnd = FieldEndCharacterIndex;
                            // if (sPassedMickString.Substring(FieldLocEnd, 1) == sPassedFieldChar) { FieldLocEnd -= 1; }
                            // if (FieldLocEnd < 0) { FieldLocEnd = 0; }
                            if (sPassedMickString.Substring(FieldLocStart, 1) == sPassedFieldChar) { FieldLocStart += 1; }
                            if (FieldLocStart < 0) { FieldLocStart = 0; }
                            if (FieldLocEnd < FieldLocStart) { FieldLocEnd = FieldLocStart; }
                            FieldLength = FieldLocEnd - FieldLocStart;
                            if (FieldLength > 0) {
                                sFieldResult = sPassedMickString.Substring(FieldLocStart, FieldLength);
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
            FieldLength = sFieldResult.Length;
            sFieldResultLast = sFieldResult;
            return sFieldResult;
        }
        //
        // Overloads The Value Requested
        //
        public static String ActionTheField(this String sPassedMickString, int iPassedAttibute, int iPassedMultivalue, int iPasseedSubvalue, int iPassedListValue, int iPassedListSubvalue, int iPassedListCsv, int iPassedFieldAction)
        {
            //
            string Id =  "0";
            string Sm = ((char)255).ToString(); // Segment
            string Am = ((char)254).ToString(); // Attribute
            string Vm = ((char)253).ToString(); // Value
            string Svm = ((char)252).ToString();  // SubValue
            string Lvm = "*";
            string Lsvm = "@";
            string Lcsvm = ",";

            FieldId = 0; // = "0";
            FieldSm = 0; // = ((char)255).ToString(); // Segment
            FieldAm = iPassedAttibute; // = ((char)254).ToString(); // Attribute
            FieldVm = iPassedMultivalue; // = ((char)253).ToString(); // Value
            FieldSvm = iPasseedSubvalue; // = ((char)252).ToString();  // SubValue
            FieldLvm = iPassedListValue; // = "*";
            FieldLsvm = iPassedListSubvalue; // = "@";
            FieldLcsvm = iPassedListCsv; // = ",";

            FieldActionLast = iPassedFieldAction;


            // enum MultiValues : string { Id = "0", Am = ((char)254).ToString(), Vm = ((char)253).ToString(), Svm = ((char)252).ToString(), Lvm = "*", Lsvm = "@", Lcsvm = ",", LcsvmSm = ((char)255).ToString( };
            //
            string sResult = sPassedMickString;
            if (iPassedAttibute > 0)
            {
                sResult = ActionTheField(sResult, Am, iPassedAttibute, iPassedFieldAction);
            }
            if (iPassedMultivalue > 0)
            {
                sResult = ActionTheField(sResult, Vm, iPassedMultivalue, iPassedFieldAction);
            }
            if (iPasseedSubvalue > 0)
            {
                sResult = ActionTheField(sResult, Svm, iPasseedSubvalue, iPassedFieldAction);
            }
            if (iPassedListValue > 0)
            {
                sResult = ActionTheField(sResult, Lvm, iPassedListValue, iPassedFieldAction);
            }
            if (iPassedListSubvalue > 0)
            {
                sResult = ActionTheField(sResult, Lsvm, iPassedListSubvalue, iPassedFieldAction);
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
