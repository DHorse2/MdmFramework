using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Mdm.Oss.Decl;

// namespace Mdm.Pick.Syntax { }

namespace Mdm.Oss.Decl {

    /// <summary>
    /// <para> Multivalued Syntax Class</para>
    /// <para> This implements part of the syntax
    /// for multivalued database languages, sometimes
    /// referred to as post-relational database systems.</para>
    /// <para> . </para>
    /// <para> At this first level, basic language
    /// features are implemented as methods.  File, printer
    /// and display I/O are not implemented at this level.
    /// The second level will implement print and 
    /// display I/O.  The third level implements file I/O.
    /// The fourth level includes many multivalued feutures
    /// amoung other string extensions in the MickString class.</para>
    /// <para> . </para>
    /// <para> The development goal is to implement functions
    /// and syntax that is abscent from C# and particularly
    /// useful or effecient.  By implementing a sufficient 
    /// core set of language features, it should be relatively
    /// easy to convert mutlivalued classes to C# versions.</para>
    /// <para> . </para>
    /// <para> The functions in this class include Ins(ert), 
    /// Del(ete), Field, IConv(ert), OConv(ert), Len(gth), 
    /// SpaceFill, StringFill, Stop, Trim, and a TCL Read</para>
    /// <para> . </para>
    /// <para> In the multivalued database platforms, the
    /// term TCL is an acronym for Terminal Control Language
    /// and is the console language for the environment.  The 
    /// is also a PROC(edure) script language that can contain
    /// TCL commands and would be similar to BAT files on DOS
    /// platforms.  The programming language for these platforms
    /// is DataBasic and is a form of Dartmouth Basic with
    /// built in features for handling multivalues.</para>
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class PickSyntax : DefStdBase {
        // Methods
        public long PickAttrCountGetResult;
        public String sFileDoRequest;
        #region PickData
        public int PickAttrCountResult;
        //
        public long PickBufferMoveToItemDataResult;
        public long PickBufferMoveStringResult;
        //
        public int PickItemDataCounterResult;
        //
        public long PickConsoleDisplayResult;
        //
        public long PickConvertStringResult;
        public long PickConvertTypeTwoResult;
        //
        public long PickIConvResult;
        public long PickIConvStringResult;
        //
        public long PickOconvIntResult;
        public long PickOconvStringStringPassedResult;
        public long PickOconvStringPassedResult;
        //
        public long PickDelResult;
        public long PickDelItemResult;
        public long PickDelAttributeResult;
        public long PickDelValueResult;
        public long PickDelSubValueResult;
        public long PickDelCharValueResult;
        public long PickDelCharSubValueResult;
        // Reset
        public long PickDataResetResult;
        public long PickDictResetResult;
        // ClearCurrent
        public long PickDataClearCurrentResult;
        public long PickDictClearCurrentResult;
        //
        public long PickFieldResult;
        //
        public long PickIndexResult;
        //
        public long PickInsResult;
        public long PickInsStringResult;
        public long PickInItemResult;
        public long PickInsAttributeResult;
        public long PickInsValueResult;
        public long PickInsSubValueResult;
        public long PickInsCharValueResult;
        public long PickInsCharSubValueResult;
        //
        public long PickLenResult;
        //
        public long PickPositionAtRowResult;
        public long PickPositionAtColumnResult;
        //
        public long PickSpaceFillResult;
        public long PickStringFillResult;
        //
        public long PickStopResult;
        //
        public long PickSystemCallResult;
        public long PickSystemCallStringResult;
        //
        public long PickTclReadResult;
        //
        public long PickTrimResult;
        public long PickTrimConversionResult;
        //
        //public long PickWriteResult;
        #endregion
        #region CharacterFields
        public String ItemSeparator = "";
        public String RowSeparator = "";
        public String ColumnSeparator = "";
        public String ColumnSeparatorInput = "";
        public String ColumnSeparatorOutput = "";
        public bool LfRemoveItFlag = bYES;
        public bool TldEscaped = bNO;
        #endregion
        #region PickFieldFuncData
        // PickConsole Command Function of the format XXX (ipPickDictItemGet.TextChangedEvent. L#n R#n MCx, MDx)
        protected String sField = "";
        protected String sField_Conversion = "";
        protected String sField_ConversionRootVerb = "";
        // "L"=Left Justify "R"=RightJustify "M"=Mask
        protected String sField_ConversionRootVerbType = "";
        // "L"=Left "R"=Right "M"=Mask
        protected String sField_ConversionRootVerbFuncOrFill = "";
        // "#"=spaces "0"=zeros "C"="MC"=case "D"="MD"=decimal (MD2ZM) (MD0Z) (MD2Z$M)
        protected String sField_ConversionRootVerbActionOrParamOrLength = ""; // xxA n MCU MCL MD0 R#15 R?15 L?72
        protected int Field_ConversionRootVerbField_Length = 0; // numeric field length
        #endregion
        #region PickSystemCalls
        // System Call Function Constants
        protected int PickSystemCommand = -1;
        /// <summary>
        /// <para> Enumerates the list of supported
        /// multivalued system commands.</para>
        /// </summary>
        public enum PickSystemCommandIs : int {
            SYSTEM_COMMAND_LINE = 0,
            SYSTEM_SLEEP = 11,
            SYSTEM_TYPEAHEAD_CHARACTERS = 14
        }
        #endregion
        //
        #region PickDelegates

        //public delegate void TraceMdmPointDel(
        //    long iPassed_MethodResult,
        //    bool PassedError,
        //    int iPassedErrorLevel,
        //    int iPassedErrorSource,
        //    bool PassedDisplay,
        //    int iPassedUserEntry,
        //    String PassedTraceMessage);
        //protected TraceMdmPointDel TraceMdmPoint;

        //public delegate void PrintOutputMdm_PickPositionDel(
        //    int iMessageLevel,
        //    bool PassedNewLineFlag,
        //    int iPassedPromptColumn,
        //    int iPassedPromptRow);
        //protected PrintOutputMdm_PickPositionDel PrintOutputMdm_PickPosition;

        //public delegate long ConsoleMdmStd_IoWriteDel(String PassedLine);
        //protected ConsoleMdmStd_IoWriteDel ConsoleMdmStd_IoWrite;
        //public delegate void ConsoleMdmPickDisplayDel(String PassedLine);
        //protected ConsoleMdmPickDisplayDel ConsoleMdmPickDisplay;

        #endregion

        public PickSyntax()
            : base() {
            Sender = this;
            SenderIsThis = this;
    }
        public PickSyntax(long ClassHasPassed)
            : base(ClassHasPassed) {
            Sender = this;
            SenderIsThis = this;
    }

        #region PickCodeAll
        // ==================================================================
        //
        // this is the PICK CODE
        // 
        // ==============================================================
        //
        #region ConsolePickConsole TCL SYSTEM
        // FUNCTION TCL (Console) READ
        public String PickTclRead() {
            PickTclReadResult = (long)StateIs.Started;
            String sTemp = "";

            // System Call required here;

            // PickTclRead
            return sTemp;
        }
        // SYSTEM
        public String PickSystemCallString(int iPassedSystemCallId) {
            long PickSystemCallStringResult = (long)StateIs.Started;
            String sTemp = "";
            long RunStatus = 0;
            PickSystemCommand = iPassedSystemCallId;
            //
            switch (PickSystemCommand) {
                case ((int)PickSystemCommandIs.SYSTEM_COMMAND_LINE):
                    sTemp = RunStatus.ToString();
                    PickSystemCallStringResult = RunStatus;
                    break;
                case ((int)PickSystemCommandIs.SYSTEM_SLEEP):
                    // TODO "Sleep is an integer command sLine"; 
                    sTemp = "Sleep is an integer command sLine";
                    PickSystemCallStringResult = 11;
                    break;
                case ((int)PickSystemCommandIs.SYSTEM_TYPEAHEAD_CHARACTERS):
                    // TODO "Typeahead is an integer command sLine";
                    sTemp = "Typeahead is an integer command sLine";
                    PickSystemCallStringResult = 14;
                    break;
                default:
                    PickSystemCallStringResult = (long)StateIs.Undefined;
                    sTemp = "";
                    LocalMessage.Msg6 = "Post-Relational System Call (" + PickSystemCommand.ToString() + ") to get some text does not exist!!";
                    throw new NotSupportedException(LocalMessage.Msg6);
                    break;
            }
            // PickSystemCallString
            return sTemp;
        }
        public long PickSystemCall(int iPassedSystemCallId) {
            PickSystemCallResult = (long)StateIs.Started;
            String sTemp = "";
            long RunStatus = 0;
            PickSystemCommand = iPassedSystemCallId;
            //
            switch (PickSystemCommand) {
                case ((int)PickSystemCommandIs.SYSTEM_COMMAND_LINE):
                    // errror 
                    sTemp = RunStatus.ToString();
                    PickSystemCallResult = RunStatus;
                    break;
                case ((int)PickSystemCommandIs.SYSTEM_SLEEP):
                    PickSystemCallResult = 11;
                    break;
                case ((int)PickSystemCommandIs.SYSTEM_TYPEAHEAD_CHARACTERS):
                    PickSystemCallResult = 14;
                    break;
                default:
                    PickSystemCallResult = (long)StateIs.Undefined;
                    sTemp = "";
                    LocalMessage.Msg6 = "Post-Relational System Call (" + PickSystemCommand.ToString() + ") that gets a number does not exist";
                    throw new NotSupportedException(LocalMessage.Msg6);
            }
            // PickSystemCallResult = iTextInputWidth;
            // PickSystemCallResult
            return PickSystemCallResult;
        }
        #endregion
        #region PickFunctions
        // FUNCTION TRIM
        public String PickTrim(String sField, String sField_Conversion) {
            PickTrimConversionResult = (long)StateIs.Started;
            char[] sCharArray = sField_Conversion.ToCharArray();

            if (sField_Conversion.Length == 0) {
                // PickTrimConversion
                return sField.Trim((" ").ToCharArray());
            } else {
                sField.Trim(sCharArray);
            }
            // PickTrimConversion
            return sField;
        }
        // PickTrim
        public String PickTrim(String sField) {
            PickTrimResult = (long)StateIs.Started;

            // PickTrim
            return sField.Trim((" ").ToCharArray());
        }
        // FUNCTION INDEX
        public int PickIndex(String PassedField, String PassedCharacterToCount, int iPassedField_Occurence) {
            PickIndexResult = (long)StateIs.Started;
            int iLoc = 0;
            int iLocCurrent = 0;
            int iLocStartAt = 0;
            int iForCounter = 0;
            if (iPassedField_Occurence > 0) {
                if (PassedCharacterToCount.Length > 0) {
                    if (PassedField.Length > 0) {
                        //For Loop
                        for (iForCounter = 1; (iForCounter <= iPassedField_Occurence && iLocCurrent <= PassedField.Length); iForCounter++) {
                            iLocCurrent = PassedField.IndexOf(PassedCharacterToCount, iLocStartAt);
                            if (iLocCurrent < 0) {
                                iLoc = -1;
                                break;
                            } else {
                                iLoc = iLocCurrent;
                                iLocStartAt = iLocCurrent + 1;
                            }
                        }
                    } else { iLoc = -1; }
                } else { iLoc = -1; }
            } else { iLoc = -1; }
            // PickIndex
            return iLoc;
        }
        // FUNCTION OCONV
        public String PickOconv(String PassedField, String PassedField_Conversion) {
            PickOconvStringStringPassedResult = (long)StateIs.Started;
            sField = PassedField;
            sField_Conversion = PassedField_Conversion;
            sField_ConversionRootVerb = "";
            sField_ConversionRootVerbType = "";
            sField_ConversionRootVerbFuncOrFill = "";
            sField_ConversionRootVerbActionOrParamOrLength = "";
            int iLoc;
            int iLocCurrent;
            int iForCounter;
            sTemp1 = "";
            //
            sField_Conversion = sField_Conversion.ToUpper();
            if (sField_Conversion.Length > 0) {
                sField_ConversionRootVerb = sField_Conversion.Substring(0, 1);
                sField_ConversionRootVerbType = sField_Conversion.Substring(0, 1);

                if (sField_Conversion.Length > 1) {
                    sField_ConversionRootVerbType = sField_Conversion.Substring(0, 1);
                    sField_ConversionRootVerbFuncOrFill = sField_Conversion.Substring(1, 1);

                    if (sField_Conversion.Length > 2) {
                        sField_ConversionRootVerbActionOrParamOrLength = sField_Conversion.Substring(2);
                    } else {
                        sField_ConversionRootVerbActionOrParamOrLength = "Z";
                    }

                } else if (sField_Conversion.Length == 1) {
                    sField_ConversionRootVerbFuncOrFill = "#";
                    sField_ConversionRootVerbActionOrParamOrLength = "Z";
                }
            } else {
                sField_ConversionRootVerb = "Z";
                sField_ConversionRootVerbFuncOrFill = "#";
                sField_ConversionRootVerbType = "Z#";
                sField_ConversionRootVerbActionOrParamOrLength = "Z";
            }
            switch (sField_ConversionRootVerbType) {
                case "L":
                case "R":
                    switch (sField_ConversionRootVerbFuncOrFill) {
                        case ("0"):
                            // zero padded numbers
                            try {
                                sField_ConversionRootVerbActionOrParamOrLength = "0" + sField_ConversionRootVerbActionOrParamOrLength;
                                Field_ConversionRootVerbField_Length = Convert.ToInt32(sField_ConversionRootVerbActionOrParamOrLength);
                            } catch (Exception oeMexceptConvException) {
                                Field_ConversionRootVerbField_Length = sField.Length;
                            } finally {
                                if (Field_ConversionRootVerbField_Length == 0) { Field_ConversionRootVerbField_Length = sField.Length; }
                            }
                            switch (sField_ConversionRootVerbType) {
                                case "L":
                                    if (Field_ConversionRootVerbField_Length < sField.Length) {
                                        sField = sField.Substring(0, Field_ConversionRootVerbField_Length);
                                    } else {
                                        sField = sField.PadRight(Field_ConversionRootVerbField_Length, '0');
                                    }
                                    break;
                                case "R":
                                    sTemp1 = "D" + Field_ConversionRootVerbField_Length.ToString();
                                    try {
                                        iTemp1 = Convert.ToInt32(sField);
                                        sField = iTemp1.ToString(sTemp1);
                                    } catch (Exception eAnye) {
                                        if (Field_ConversionRootVerbField_Length < sField.Length) {
                                            sField = sField.Substring((sField.Length - Field_ConversionRootVerbField_Length), Field_ConversionRootVerbField_Length);
                                        } else {
                                            sField = sField.PadLeft(Field_ConversionRootVerbField_Length, '0');
                                        }
                                    }
                                    break;
                                case "H":
                                    sTemp1 = "X" + Field_ConversionRootVerbField_Length.ToString();
                                    try {
                                        iTemp1 = Convert.ToInt32(sField);
                                        sField = iTemp1.ToString(sTemp1);
                                    } catch (Exception eAnye) {
                                        if (Field_ConversionRootVerbField_Length < sField.Length) {
                                            sField = sField.Substring((sField.Length - Field_ConversionRootVerbField_Length), Field_ConversionRootVerbField_Length);
                                        } else {
                                            sField = sField.PadLeft(Field_ConversionRootVerbField_Length, '0');
                                        }
                                    }
                                    break;
                                default:
                                    PickOconvStringPassedResult = (long)StateIs.Undefined;
                                    sField = "";
                                    LocalMessage.Msg6 = "Post-Relational Output Converstion Prefix (Justification) (" + sField_ConversionRootVerbType + ") does not exist";
                                    throw new NotSupportedException(LocalMessage.Msg6);
                            }
                            break;

                        case ("#"):
                        case (" "):
                        default:
                            // space padded strings
                            try {
                                // is Actions a length (numeric)?
                                sField_ConversionRootVerbActionOrParamOrLength = "0" + sField_ConversionRootVerbActionOrParamOrLength;
                                Field_ConversionRootVerbField_Length = Convert.ToInt32(sField_ConversionRootVerbActionOrParamOrLength);
                            } catch (Exception oeMexceptConvException) {
                                Field_ConversionRootVerbField_Length = sField.Length;
                            } finally {
                                if (Field_ConversionRootVerbField_Length == 0) { Field_ConversionRootVerbField_Length = sField.Length; }
                            }
                            // Fill Character
                            if (sField_ConversionRootVerbFuncOrFill == "#" || sField_ConversionRootVerbFuncOrFill == "Z") { sField_ConversionRootVerbFuncOrFill = " "; }
                            Char uTempFill = Convert.ToChar((String)(sField_ConversionRootVerbFuncOrFill.Substring(0, 1)));
                            switch (sField_ConversionRootVerbType) {
                                case "L":
                                    if (Field_ConversionRootVerbField_Length < sField.Length) {
                                        sField = sField.Substring(0, Field_ConversionRootVerbField_Length);
                                    } else {
                                        sField = sField.PadRight(Field_ConversionRootVerbField_Length, uTempFill);
                                    }
                                    break;
                                case "R":
                                    // TODO z$RelVs? PickOconv NEED TO DO THE MASK DECIMALS RATHER THAN R#'s
                                    sTemp1 = "D" + Field_ConversionRootVerbField_Length.ToString();
                                    try {
                                        iTemp1 = Convert.ToInt32(sField);
                                        sField = iTemp1.ToString(sTemp1);
                                    } catch (Exception eAnye) {
                                        if (Field_ConversionRootVerbField_Length < sField.Length) {
                                            sField = sField.Substring((sField.Length - Field_ConversionRootVerbField_Length), Field_ConversionRootVerbField_Length);
                                        } else {
                                            sField = sField.PadLeft(Field_ConversionRootVerbField_Length, uTempFill);
                                        }
                                    }
                                    break;
                                case "H":
                                    sTemp1 = "X" + Field_ConversionRootVerbField_Length.ToString();
                                    try {
                                        iTemp1 = Convert.ToInt32(sField);
                                        sField = iTemp1.ToString(sTemp1);
                                    } catch (Exception eAnye) {
                                        if (Field_ConversionRootVerbField_Length < sField.Length) {
                                            sField = sField.Substring((sField.Length - Field_ConversionRootVerbField_Length), Field_ConversionRootVerbField_Length);
                                        } else {
                                            sField = sField.PadLeft(Field_ConversionRootVerbField_Length, 'e');
                                        }
                                    }
                                    break;
                                default:
                                    PickOconvStringPassedResult = (long)StateIs.Undefined;
                                    sField = "";
                                    LocalMessage.Msg6 = "Post-Relational Output Converstion Prefix (Justification) (" + sField_ConversionRootVerbType + ") does not exist";
                                    throw new NotSupportedException(LocalMessage.Msg6);
                            }
                            break;
                    }
                    break;
                case ("M"):
                    // mask
                    switch (sField_ConversionRootVerbFuncOrFill) {
                        case ("C"):
                            // mask case
                            switch (sField_Conversion) {
                                case ("MCU"):
                                    // convert to upper case
                                    // PickOconvStringStringPassedResult
                                    return sField.ToUpper();
                                case ("MCL"):
                                    // convert to lower case
                                    // PickOconvStringStringPassedResult
                                    return sField.ToLower();
                                case ("MCF"):
                                    // convert to first letter capitals case
                                    for (iForCounter = 2; (iForCounter <= sField.Length); iForCounter++) {
                                        if (sField.Substring(iForCounter - 2, 1) == " " || iForCounter == 2) {
                                            if (iForCounter > 2) {
                                                sTemp1 = sField.Substring(0, iForCounter - 1);
                                                sField_ConversionRootVerb = (sField.Substring(iForCounter - 1, 1)).ToUpper();
                                                if (iForCounter < sField.Length) {
                                                    sTemp2 = sField.Substring(iForCounter, sField.Length - iForCounter);
                                                } else
                                                    sTemp2 = "";
                                            } else {
                                                sTemp1 = "";
                                                sField_ConversionRootVerb = (sField.Substring(0, 1)).ToUpper();
                                                if (iForCounter < sField.Length) {
                                                    sTemp2 = sField.Substring(1, sField.Length - 1);
                                                } else
                                                    sTemp2 = "";

                                            }
                                            sField = sTemp1 + sField_ConversionRootVerb + sTemp2;
                                            // PassedField = PassedField.Substring(0, Field_AttributeIndex - 1) + (PassedField.Substring(Field_AttributeIndex - 2,1)).ToUpper() + PassedField.Substring(Field_AttributeIndex, PassedField.Length);
                                        }
                                    }
                                    break;
                                default:
                                    PickOconvStringStringPassedResult = (long)StateIs.Undefined;
                                    sField = "";
                                    LocalMessage.Msg6 = "Post-Relational Output Converstion (" + sField_Conversion + ") does not exist";
                                    throw new NotSupportedException(LocalMessage.Msg6);
                            }
                            break;
                        case ("D"):
                            // TODO z$RelVs? PickOconv - mask decimal - MD2ZM
                            break;
                        default:
                            // no default masking
                            break;
                    }
                    break;
                default:
                    PickOconvStringStringPassedResult = (long)StateIs.Undefined;
                    sField = "";
                    LocalMessage.Msg6 = "Post-Relational Output Converstion (" + sField_Conversion + ") does not exist";
                    throw new NotSupportedException(LocalMessage.Msg6);
            }
            // PickOconvStringStringPassedResult
            return sField;
        }
        public String PickOconv(int Field, String sField_Conversion) {
            PickOconvStringPassedResult = (long)StateIs.Started;
            sTemp = Field.ToString();
            sTemp1 = PickOconv(sTemp, sField_Conversion);

            return sTemp1;
        }
        // FUNCTION ICONV
        public String PickIConv(String sField, String sField_Conversion) {
            PickIConvResult = (long)StateIs.Started;
            String sTemp = "put the command here";

            return sTemp;
        }
        public String PickIConv(int Field, String sField_Conversion) {
            PickIConvResult = (long)StateIs.Started;
            String sTemp = "put the command here";

            return sTemp;
        }
        // FUNCTION FIELD
        public String PickField(String sField, String sField_Char, int Field_Occurence) {
            PickFieldResult = (long)StateIs.Started;
            String sTemp = sField;
            int iLoc = 0;
            int iForCounter = 0;
            //For Loop
            for (iForCounter = 1; (iForCounter <= Field_Occurence && sTemp.Length > 0); iForCounter++) {
                iLoc = sTemp.IndexOf(sField_Char, 0);
                if (iForCounter < Field_Occurence) {
                    if (iLoc == -1) {
                        sTemp = "";
                        break;
                    } else {
                        sTemp = sTemp.Substring(iLoc + 1);
                        if (iForCounter == Field_Occurence) {
                            iLoc = sTemp.IndexOf(sField_Char, 0);
                            if (iLoc > 0) {
                                sTemp = sTemp.Substring(1, iLoc);
                            } else {
                                break;
                            }
                        }
                    }
                }
            } // end of for
            return sTemp;
        }
        // STOP
        public virtual void PickStop() {
            // PickConsole Stop
            PickStopResult = (long)StateIs.Started;

            //sTraceDisplayMessageDetail = "STOP"; // TODO z$RelVs? PickStop PICK STOP Analyze and implement proper handling of pick run abort.

            //RunAction = RunRunDo;
            //RunMetric = RunState;
            //RunTense = RunTense_Done;
            //RunActionState[RunRunDo, RunState] = RunTense_Done;
            //RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
            //    "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + sTraceDisplayMessageDetail);
            //ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs);
            // PickStop
        }
        // COUNT ATTRIBUTES
        public int PickAttrCountGet(String sField, String sCharacterToCount) {
            PickAttrCountGetResult = (long)StateIs.Started;
            int iLoc = 0;
            int iForCounter = 0;
            int iLocCurrent = 0;
            //For Loop
            for (iForCounter = 1; (iLoc >= 0 && iLocCurrent <= sField.Length); iForCounter++) {
                iLoc = sField.IndexOf(sCharacterToCount, iLocCurrent);
                if (iLoc < 0) {
                    return iForCounter - 1;
                } else {
                    iLocCurrent = iLoc + 1;
                }
            }
            return iForCounter;
        }
        #endregion
        #region PickConversionFunctions
        // CHAR CONVERT
        public String PickConvert(String sField_Char, String sCharTo, String sField) {
            PickConvertStringResult = (long)StateIs.Started;
            int iLoc;
            int iForCounter = 0;
            // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            // C#  Copy Code 
            // char[] delimit = new char[] { ' ' };
            // String PassedField = "The cat sat on the mat.";
            String sTemp1 = "";
            foreach (String substr in sField.Split((char[])sField_Char.ToCharArray(), StringSplitOptions.None)) {
                iForCounter++;
                if (iForCounter > 1) {
                    sTemp1 += sCharTo;
                }
                sTemp1 += substr;
                // System.Console.WriteLine(substr);
            }
            return sTemp1;
        }
        // CHAR CONVERT 2
        public String PickConvertTypeTwo(String sField_Char, String sCharTo, String sField) {
            PickConvertTypeTwoResult = (long)StateIs.Started;
            String sTemp1 = "";
            String sTemp2 = "";
            int iLoc = 0;
            int iForCounter = 0;
            // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            // C# For Loop
            do {
                iLoc = sField.IndexOf(sField_Char, 0);
                if (iLoc >= 0) {
                    iForCounter++;
                    sTemp1 = sField.Substring(0, iLoc - 1);
                    if (iLoc < sField.Length - 1) {
                        sTemp2 = sField.Substring(iLoc + 1, sField.Length - iLoc - 1);
                    } else {
                        sTemp2 = "";
                    }
                    sField = sTemp1 + sCharTo + sTemp2;
                }
            } while (iLoc > 0);
            return sField;
        }

        #endregion
        #region PickStringFunctions
        // FUNCTIONS
        public String PickSpaceFill(int iPassedCharacterCount) {
            PickSpaceFillResult = (long)StateIs.Started;
            String sTemp = "";

            // PickSpaceFill
            return sTemp;
        }
        public String PickStringFill(String StringPassed, int iPassedCharacterCount) {
            PickStringFillResult = (long)StateIs.Started;
            String sTemp = "";

            // PickStringFill
            return sTemp;
        }
        // FUNCTION LENGTH
        public int PickLen(String StringPassed) {
            PickLenResult = (long)StateIs.Started;
            // PickLen
            return StringPassed.Length;
        }
        #endregion
        #region PickInsertDelete
        // FUNCTION Delete
        public String PickDel(String StringPassed) {
            PickDelResult = (long)StateIs.Started;
            return StringPassed = PickDel(StringPassed, 1);
        }
        public String PickDel(String StringPassed, int iPassedAttributeIndex) {
            PickDelAttributeResult = (long)StateIs.Started;
            return StringPassed.DeleteField(iPassedAttributeIndex);
            // return Mdm.MickString1.DeleteField(StringPassed, iPassedAttributeIndex);
            // return StringPassed = StringPassed.DeleteField(iPassedAttributeIndex);
        }
        // FUNCTION Insert
        public String PickIns(String StringPassed) {
            PickInsStringResult = (long)StateIs.Started;

            // PickInsString
            return StringPassed = PickIns(StringPassed, 1);
        }
        public String PickIns(String StringPassed, int iPassedAttributeIndex) {
            PickInsAttributeResult = (long)StateIs.Started;
            //
            // PickInsAttribute
            return StringPassed.InsertField(iPassedAttributeIndex);
            // return Mdm.MickString1.InsertField(StringPassed, iPassedAttributeIndex);
            // return StringPassed = StringPassed.InsertField(iPassedAttributeIndex);
        }
        #endregion
        #endregion

    }
}
