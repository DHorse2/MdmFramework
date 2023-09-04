using System;
using Mdm.Oss.Components;
using Mdm.Oss.Decl;
using Mdm.Oss.Std;

// namespace Mdm.Pick.Syntax { }

namespace Mdm.Oss.Std
{
    #region PickData C# Language Extension
    public class PickStateDef
    {
        public StateIs PickAttrCountResult;
        //
        public StateIs PickBufferMoveToItemDataResult;
        public StateIs PickBufferMoveStringResult;
        //
        public StateIs PickItemDataCounterResult;
        //
        public StateIs PickConsoleDisplayResult;
        //
        public StateIs PickConvertStringResult;
        public StateIs PickConvertTypeTwoResult;
        //
        public StateIs PickIConvResult;
        public StateIs PickIConvStringResult;
        //
        public StateIs PickOconvIntResult;
        public StateIs PickOconvStringStringPassedResult;
        public StateIs PickOconvStringPassedResult;
        //
        public StateIs PickDelResult;
        public StateIs PickDelItemResult;
        public StateIs PickDelAttributeResult;
        public StateIs PickDelValueResult;
        public StateIs PickDelSubValueResult;
        public StateIs PickDelCharValueResult;
        public StateIs PickDelCharSubValueResult;
        // Reset
        public StateIs PickDataResetResult;
        public StateIs PickDictResetResult;
        // ClearCurrent
        public StateIs PickDataClearCurrentResult;
        public StateIs PickDictClearCurrentResult;
        //
        public StateIs PickFieldResult;
        //
        public StateIs PickIndexResult;
        //
        public StateIs PickInsResult;
        public StateIs PickInsStringResult;
        public StateIs PickInItemResult;
        public StateIs PickInsAttributeResult;
        public StateIs PickInsValueResult;
        public StateIs PickInsSubValueResult;
        public StateIs PickInsCharValueResult;
        public StateIs PickInsCharSubValueResult;
        //
        public StateIs PickLenResult;
        //
        public StateIs PickPositionAtRowResult;
        public StateIs PickPositionAtColumnResult;
        //
        public StateIs PickSpaceFillResult;
        public StateIs PickStringFillResult;
        //
        public StateIs PickStopResult;
        public StateIs PickSystemCallResult;
        public StateIs PickSystemCallStringResult;
        //
        public StateIs PickTclReadResult;
        //
        public StateIs PickTrimResult;
        public StateIs PickTrimConversionResult;
        //
        //public StateIs PickWriteResult;
        public StateIs PickAttrCountGetResult;
    }
    #endregion
    //
    /// <summary>
    /// <para> Multivalued Syntax Class</para>
    /// <para> This implements part of the syntax
    /// for multivalued database languages, sometimes
    /// referred to as post-relational database systems.
    /// This a way of extending language features.
    /// </para>
    /// <para> At this first level, basic language
    /// syntax features are implemented as methods.
    /// (Hypothetically) File, printer and display 
    /// I/O are not implemented at this level.
    /// The second level will implement print and 
    /// display I/O.  The third level implements file I/O.
    /// The fourth level includes many multivalued feutures
    /// amoung other string extensions in the MickString class.</para>
    /// <para> The development goal is to implement functions
    /// and syntax that is abscent from C# and particularly
    /// useful or effecient.  By implementing a sufficient 
    /// core set of language features, it should be relatively
    /// easy to convert mutlivalued classes to C# versions.</para>
    /// <para> The functions in this class include Ins(ert), 
    /// Del(ete), Field, IConv(ert), OConv(ert), Len(gth), 
    /// SpaceFill, StringFill, Stop, Trim, and a TCL Read</para>
    /// <para> In the multivalued database platforms, the
    /// term TCL is an acronym for Terminal Control Language
    /// and is the console language for the environment.  The 
    /// is also a PROC(edure) script language that can contain
    /// TCL commands and would be similar to BAT files on DOS
    /// platforms.  The programming language for these platforms
    /// is DataBasic and is a form of Dartmouth Basic with
    /// built in features for handling multivalues.
    /// Next: StdBaseRunDef
    /// Specificity: StdBasePickSyntaxDef : StdBaseRunDef : StdBaseRunFilePrinterConsole : StdConsoleManagerDef
    /// </para>
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class StdBasePickSyntaxDef : StdBaseDef, IDisposable
    {
        public PickStateDef PickState;
        protected int PickSystemCallDo;
        // Methods
        public String sFileDoRequest;
        #region CharacterFields
        public String ItemSeparator;
        public String RowSeparator;
        public String ColumnSeparator;
        public String ColumnSeparatorInput;
        public String ColumnSeparatorOutput;
        public bool LfRemoveItFlag;
        public bool TldEscaped;
        #endregion

        public StdBasePickSyntaxDef(ref object SenderPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
            : base(ref SenderPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        { PickState = new PickStateDef(); }
        public StdBasePickSyntaxDef(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
            : base(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        { PickState = new PickStateDef(); }
        public StdBasePickSyntaxDef(ref object SenderPassed)
            : base(ref SenderPassed)
        { PickState = new PickStateDef(); }
        public StdBasePickSyntaxDef()
             : base()
        {
            PickState = new PickStateDef();
            //Sender = this;
            //SenderThisIs = this;
        }
        public StdBasePickSyntaxDef(ref object SenderPassed, ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ref SenderPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        { PickState = new PickStateDef(); }
        public virtual void InitializeStdBasePickSyntax()
        {
            if (!ClassFeatureFlag.StdBasePickSyntaxDone)
            {
                ClassFeatureFlag.StdBasePickSyntaxDone = true;
                base.InitializeStdBase();
                if (LocalMessage == null) { LocalMessage = new LocalMsgDef(); }
                if (!ClassFeatureFlag.InitializeStdCharsDone && ClassFeatureFlag.MdmTransformIsUsed)
                {
                    InitializeStdChars();
                }
                if (!ClassFeatureFlag.InitializeStdConversionDone && ClassFeatureFlag.MdmTransformIsUsed)
                {
                    InitializeStdConversion();
                }
            }
        }
        public virtual void InitializeStdChars()
        {
            ClassFeatureFlag.InitializeStdCharsDone = true;
            #region CharacterFields
            ItemSeparator = sEmpty;
            RowSeparator = sEmpty;
            ColumnSeparator = sEmpty;
            ColumnSeparatorInput = sEmpty;
            ColumnSeparatorOutput = sEmpty;
            LfRemoveItFlag = bYES;
            TldEscaped = bNO;
            #endregion
        }
        public virtual void InitializeStdConversion()
        {
            ClassFeatureFlag.InitializeStdConversionDone = true;
            #region PickFieldFuncData
            // PickConsole Command Function of the format XXX (ipPickDictItemGet.TextChangedEvent. L#n R#n MCx, MDx)
            sField = sEmpty;
            sField_Conversion = sEmpty;
            sField_ConversionRootVerb = sEmpty;
            // "L"=Left Justify "R"=RightJustify "M"=Mask
            sField_ConversionRootVerbType = sEmpty;
            // "L"=Left "R"=Right "M"=Mask
            sField_ConversionRootVerbFuncOrFill = sEmpty;
            // "#"=spaces "0"=zeros "C"="MC"=case "D"="MD"=decimal (MD2ZM) (MD0Z) (MD2Z$M)
            sField_ConversionRootVerbActionOrParamOrLength = sEmpty; // xxA n MCU MCL MD0 R#15 R?15 L?72
            Field_ConversionRootVerbField_Length = 0; // numeric field length
            #endregion
            PickSystemCommand = PickSystemCommandIs.SYSTEM_COMMAND_NONE;
        }
        #region PickFieldFuncData
        // PickConsole Command Function of the format XXX (ipPickDictItemGet.TextChangedEvent. L#n R#n MCx, MDx)
        protected String sField;
        protected String sField_Conversion;
        protected String sField_ConversionRootVerb;
        // "L"=Left Justify "R"=RightJustify "M"=Mask
        protected String sField_ConversionRootVerbType;
        // "L"=Left "R"=Right "M"=Mask
        protected String sField_ConversionRootVerbFuncOrFill;
        // "#"=spaces "0"=zeros "C"="MC"=case "D"="MD"=decimal (MD2ZM) (MD0Z) (MD2Z$M)
        protected String sField_ConversionRootVerbActionOrParamOrLength; // xxA n MCU MCL MD0 R#15 R?15 L?72
        protected int Field_ConversionRootVerbField_Length; // numeric field length
        #endregion

        #region PickSystemCalls
        // System Call Function Constants
        protected PickSystemCommandIs PickSystemCommand;
        /// <summary>
        /// <para> Enumerates the list of supported
        /// multivalued system commands.</para>
        /// </summary>
        public enum PickSystemCommandIs : int
        {
            SYSTEM_COMMAND_LINE = 0,
            SYSTEM_SLEEP = 11,
            SYSTEM_TYPEAHEAD_CHARACTERS = 14,
            SYSTEM_COMMAND_NONE = iUnknown
        }
        #endregion
        //
        #region PickDelegates

        //public delegate void TraceMdmPointDel(
        //    StateIs iPassed_MethodResult,
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

        //public delegate StateIs ConsoleMdmStd_IoWriteDel(String PassedLine);
        //protected ConsoleMdmStd_IoWriteDel ConsoleMdmStd_IoWrite;
        //public delegate void ConsoleMdmPickDisplayDel(String PassedLine);
        //protected ConsoleMdmPickDisplayDel ConsoleMdmPickDisplay;

        #endregion

        #region PickCodeAll
        // ==================================================================
        //
        // this is the PICK CODE
        // 
        // ==============================================================
        //
        #region Console TCL SYSTEM
        // FUNCTION TCL (Console) READ
        public String PickTclRead()
        {
            PickState.PickTclReadResult = StateIs.Started;
            String sTemp = sEmpty;

            // System Call required here;

            // PickTclRead
            return sTemp;
        }
        // SYSTEM
        public String PickSystemCallString(PickSystemCommandIs iPassedSystemCallId)
        {
            StateIs PickSystemCallStringResult = StateIs.NotSet;
            String sTemp = sEmpty;
            int RunStatus = 0;
            PickSystemCommand = iPassedSystemCallId;
            //
            switch (PickSystemCommand)
            {
                case (PickSystemCommandIs.SYSTEM_COMMAND_LINE):
                    sTemp = RunStatus.ToString();
                    RunStatus = -1;
                    // PickSystemCallStringResult = RunStatus;
                    PickSystemCallStringResult = StateIs.ProgramInvalid;
                    break;
                case (PickSystemCommandIs.SYSTEM_SLEEP):
                    // ToDo "Sleep is an integer command sLine"; 
                    sTemp = "Sleep is an integer command sLine";
                    RunStatus = 11;
                    PickSystemCallStringResult = StateIs.Successful;
                    break;
                case (PickSystemCommandIs.SYSTEM_TYPEAHEAD_CHARACTERS):
                    // ToDo "Typeahead is an integer command sLine";
                    sTemp = "Typeahead is an integer command sLine";
                    RunStatus = 14;
                    PickSystemCallStringResult = StateIs.Successful;
                    break;
                default:
                    RunStatus = -1;
                    PickSystemCallStringResult = StateIs.Undefined;
                    sTemp = sEmpty;
                    LocalMessage.Msg6 = "Post-Relational System Call (" + PickSystemCommand.ToString() + ") to get some text does not exist!!";
                    throw new NotSupportedException(LocalMessage.Msg6);
            }
            // PickSystemCallString
            return sTemp;
        }
        public StateIs PickSystemCall(PickSystemCommandIs iPassedSystemCallId)
        {
            PickState.PickSystemCallResult = StateIs.Started;
            String sTemp = sEmpty;
            PickSystemCallDo = iUnknown;
            PickSystemCommand = iPassedSystemCallId;
            //
            switch (PickSystemCommand)
            {
                case (PickSystemCommandIs.SYSTEM_COMMAND_LINE):
                    // errror 
                    PickSystemCallDo = -1;
                    PickState.PickSystemCallResult = StateIs.ProgramInvalid;
                    break;
                case (PickSystemCommandIs.SYSTEM_SLEEP):
                    PickSystemCallDo = 11;
                    PickState.PickSystemCallResult = StateIs.Successful;
                    break;
                case (PickSystemCommandIs.SYSTEM_TYPEAHEAD_CHARACTERS):
                    PickSystemCallDo = 14;
                    PickState.PickSystemCallResult = StateIs.Successful;
                    break;
                default:
                    PickState.PickSystemCallResult = StateIs.Undefined;
                    sTemp = sEmpty;
                    LocalMessage.Msg6 = "Post-Relational System Call (" + PickSystemCommand.ToString() + ") that gets a number does not exist";
                    throw new NotSupportedException(LocalMessage.Msg6);
            }
            // PickSystemCallResult = iTextInputWidth;
            return PickState.PickSystemCallResult;
        }
        #endregion
        #region PickFunctions
        // FUNCTION TRIM
        public String PickTrim(String sField, String sField_Conversion)
        {
            PickState.PickTrimConversionResult = StateIs.Started;
            char[] sCharArray = sField_Conversion.ToCharArray();

            if (sField_Conversion.Length == 0)
            {
                // PickTrimConversion
                return sField.Trim((" ").ToCharArray());
            }
            else
            {
                sField.Trim(sCharArray);
            }
            // PickTrimConversion
            return sField;
        }
        // PickTrim
        public String PickTrim(String sField)
        {
            PickState.PickTrimResult = StateIs.Started;

            // PickTrim
            return sField.Trim((" ").ToCharArray());
        }
        // FUNCTION INDEX
        public int PickIndex(String PassedField, String PassedCharacterToCount, int iPassedField_Occurence)
        {
            PickState.PickIndexResult = StateIs.Started;
            int iLoc = 0;
            int iLocCurrent = 0;
            int iLocStartAt = 0;
            int iForCounter = 0;
            if (iPassedField_Occurence > 0)
            {
                if (PassedCharacterToCount.Length > 0)
                {
                    if (PassedField.Length > 0)
                    {
                        //For Loop
                        for (iForCounter = 1; (iForCounter <= iPassedField_Occurence && iLocCurrent <= PassedField.Length); iForCounter++)
                        {
                            iLocCurrent = PassedField.IndexOf(PassedCharacterToCount, iLocStartAt);
                            if (iLocCurrent < 0)
                            {
                                iLoc = -1;
                                break;
                            }
                            else
                            {
                                iLoc = iLocCurrent;
                                iLocStartAt = iLocCurrent + 1;
                            }
                        }
                    }
                    else { iLoc = -1; }
                }
                else { iLoc = -1; }
            }
            else { iLoc = -1; }
            // PickIndex
            return iLoc;
        }
        // FUNCTION OCONV
        public String PickOconv(String PassedField, String PassedField_Conversion)
        {
            PickState.PickOconvStringStringPassedResult = StateIs.Started;
            sField = PassedField;
            sField_Conversion = PassedField_Conversion;
            sField_ConversionRootVerb = sEmpty;
            sField_ConversionRootVerbType = sEmpty;
            sField_ConversionRootVerbFuncOrFill = sEmpty;
            sField_ConversionRootVerbActionOrParamOrLength = sEmpty;
            int iLoc;
            int iLocCurrent;
            int iForCounter;
            string sTemp1 = sEmpty;
            int iTemp1;
            string sTemp2;
            //
            sField_Conversion = sField_Conversion.ToUpper();
            if (sField_Conversion.Length > 0)
            {
                sField_ConversionRootVerb = sField_Conversion.Substring(0, 1);
                sField_ConversionRootVerbType = sField_Conversion.Substring(0, 1);

                if (sField_Conversion.Length > 1)
                {
                    sField_ConversionRootVerbType = sField_Conversion.Substring(0, 1);
                    sField_ConversionRootVerbFuncOrFill = sField_Conversion.Substring(1, 1);

                    if (sField_Conversion.Length > 2)
                    {
                        sField_ConversionRootVerbActionOrParamOrLength = sField_Conversion.Substring(2);
                    }
                    else
                    {
                        sField_ConversionRootVerbActionOrParamOrLength = "Z";
                    }

                }
                else if (sField_Conversion.Length == 1)
                {
                    sField_ConversionRootVerbFuncOrFill = "#";
                    sField_ConversionRootVerbActionOrParamOrLength = "Z";
                }
            }
            else
            {
                sField_ConversionRootVerb = "Z";
                sField_ConversionRootVerbFuncOrFill = "#";
                sField_ConversionRootVerbType = "Z#";
                sField_ConversionRootVerbActionOrParamOrLength = "Z";
            }
            switch (sField_ConversionRootVerbType)
            {
                case "L":
                case "R":
                    switch (sField_ConversionRootVerbFuncOrFill)
                    {
                        case ("0"):
                            // zero padded numbers
                            try
                            {
                                sField_ConversionRootVerbActionOrParamOrLength = "0" + sField_ConversionRootVerbActionOrParamOrLength;
                                Field_ConversionRootVerbField_Length = Convert.ToInt32(sField_ConversionRootVerbActionOrParamOrLength);
                            }
                            catch (Exception oeMexceptConvException)
                            {
                                Field_ConversionRootVerbField_Length = sField.Length;
                            }
                            finally
                            {
                                if (Field_ConversionRootVerbField_Length == 0) { Field_ConversionRootVerbField_Length = sField.Length; }
                            }
                            switch (sField_ConversionRootVerbType)
                            {
                                case "L":
                                    if (Field_ConversionRootVerbField_Length < sField.Length)
                                    {
                                        sField = sField.Substring(0, Field_ConversionRootVerbField_Length);
                                    }
                                    else
                                    {
                                        sField = sField.PadRight(Field_ConversionRootVerbField_Length, '0');
                                    }
                                    break;
                                case "R":
                                    sTemp1 = "D" + Field_ConversionRootVerbField_Length.ToString();
                                    try
                                    {
                                        iTemp1 = Convert.ToInt32(sField);
                                        sField = iTemp1.ToString(sTemp1);
                                    }
                                    catch (Exception eAnye)
                                    {
                                        if (Field_ConversionRootVerbField_Length < sField.Length)
                                        {
                                            sField = sField.Substring((sField.Length - Field_ConversionRootVerbField_Length), Field_ConversionRootVerbField_Length);
                                        }
                                        else
                                        {
                                            sField = sField.PadLeft(Field_ConversionRootVerbField_Length, '0');
                                        }
                                    }
                                    break;
                                case "H":
                                    sTemp1 = "X" + Field_ConversionRootVerbField_Length.ToString();
                                    try
                                    {
                                        iTemp1 = Convert.ToInt32(sField);
                                        sField = iTemp1.ToString(sTemp1);
                                    }
                                    catch (Exception eAnye)
                                    {
                                        if (Field_ConversionRootVerbField_Length < sField.Length)
                                        {
                                            sField = sField.Substring((sField.Length - Field_ConversionRootVerbField_Length), Field_ConversionRootVerbField_Length);
                                        }
                                        else
                                        {
                                            sField = sField.PadLeft(Field_ConversionRootVerbField_Length, '0');
                                        }
                                    }
                                    break;
                                default:
                                    PickState.PickOconvStringPassedResult = StateIs.Undefined;
                                    sField = sEmpty;
                                    LocalMessage.Msg6 = "Post-Relational Output Converstion Prefix (Justification) (" + sField_ConversionRootVerbType + ") does not exist";
                                    throw new NotSupportedException(LocalMessage.Msg6);
                            }
                            break;

                        case ("#"):
                        case (" "):
                        default:
                            // space padded strings
                            try
                            {
                                // is Actions a length (numeric)?
                                sField_ConversionRootVerbActionOrParamOrLength = "0" + sField_ConversionRootVerbActionOrParamOrLength;
                                Field_ConversionRootVerbField_Length = Convert.ToInt32(sField_ConversionRootVerbActionOrParamOrLength);
                            }
                            catch (Exception oeMexceptConvException)
                            {
                                Field_ConversionRootVerbField_Length = sField.Length;
                            }
                            finally
                            {
                                if (Field_ConversionRootVerbField_Length == 0) { Field_ConversionRootVerbField_Length = sField.Length; }
                            }
                            // Fill Character
                            if (sField_ConversionRootVerbFuncOrFill == "#" || sField_ConversionRootVerbFuncOrFill == "Z") { sField_ConversionRootVerbFuncOrFill = " "; }
                            Char uTempFill = Convert.ToChar((String)(sField_ConversionRootVerbFuncOrFill.Substring(0, 1)));
                            switch (sField_ConversionRootVerbType)
                            {
                                case "L":
                                    if (Field_ConversionRootVerbField_Length < sField.Length)
                                    {
                                        sField = sField.Substring(0, Field_ConversionRootVerbField_Length);
                                    }
                                    else
                                    {
                                        sField = sField.PadRight(Field_ConversionRootVerbField_Length, uTempFill);
                                    }
                                    break;
                                case "R":
                                    // ToDo z$RelVs? PickOconv NEED TO DO THE MASK DECIMALS RATHER THAN R#'s
                                    sTemp1 = "D" + Field_ConversionRootVerbField_Length.ToString();
                                    try
                                    {
                                        iTemp1 = Convert.ToInt32(sField);
                                        sField = iTemp1.ToString(sTemp1);
                                    }
                                    catch (Exception eAnye)
                                    {
                                        if (Field_ConversionRootVerbField_Length < sField.Length)
                                        {
                                            sField = sField.Substring((sField.Length - Field_ConversionRootVerbField_Length), Field_ConversionRootVerbField_Length);
                                        }
                                        else
                                        {
                                            sField = sField.PadLeft(Field_ConversionRootVerbField_Length, uTempFill);
                                        }
                                    }
                                    break;
                                case "H":
                                    sTemp1 = "X" + Field_ConversionRootVerbField_Length.ToString();
                                    try
                                    {
                                        iTemp1 = Convert.ToInt32(sField);
                                        sField = iTemp1.ToString(sTemp1);
                                    }
                                    catch (Exception eAnye)
                                    {
                                        if (Field_ConversionRootVerbField_Length < sField.Length)
                                        {
                                            sField = sField.Substring((sField.Length - Field_ConversionRootVerbField_Length), Field_ConversionRootVerbField_Length);
                                        }
                                        else
                                        {
                                            sField = sField.PadLeft(Field_ConversionRootVerbField_Length, 'e');
                                        }
                                    }
                                    break;
                                default:
                                    PickState.PickOconvStringPassedResult = StateIs.Undefined;
                                    sField = sEmpty;
                                    LocalMessage.Msg6 = "Post-Relational Output Converstion Prefix (Justification) (" + sField_ConversionRootVerbType + ") does not exist";
                                    throw new NotSupportedException(LocalMessage.Msg6);
                            }
                            break;
                    }
                    break;
                case ("M"):
                    // mask
                    switch (sField_ConversionRootVerbFuncOrFill)
                    {
                        case ("C"):
                            // mask case
                            switch (sField_Conversion)
                            {
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
                                    for (iForCounter = 2; (iForCounter <= sField.Length); iForCounter++)
                                    {
                                        if (sField.Substring(iForCounter - 2, 1) == " " || iForCounter == 2)
                                        {
                                            if (iForCounter > 2)
                                            {
                                                sTemp1 = sField.Substring(0, iForCounter - 1);
                                                sField_ConversionRootVerb = (sField.Substring(iForCounter - 1, 1)).ToUpper();
                                                if (iForCounter < sField.Length)
                                                {
                                                    sTemp2 = sField.Substring(iForCounter, sField.Length - iForCounter);
                                                }
                                                else
                                                    sTemp2 = sEmpty;
                                            }
                                            else
                                            {
                                                sTemp1 = sEmpty;
                                                sField_ConversionRootVerb = (sField.Substring(0, 1)).ToUpper();
                                                if (iForCounter < sField.Length)
                                                {
                                                    sTemp2 = sField.Substring(1, sField.Length - 1);
                                                }
                                                else
                                                    sTemp2 = sEmpty;

                                            }
                                            sField = sTemp1 + sField_ConversionRootVerb + sTemp2;
                                            // PassedField = PassedField.Substring(0, Field_AttributeIndex - 1) + (PassedField.Substring(Field_AttributeIndex - 2,1)).ToUpper() + PassedField.Substring(Field_AttributeIndex, PassedField.Length);
                                        }
                                    }
                                    break;
                                default:
                                    PickState.PickOconvStringStringPassedResult = StateIs.Undefined;
                                    sField = sEmpty;
                                    LocalMessage.Msg6 = "Post-Relational Output Converstion (" + sField_Conversion + ") does not exist";
                                    throw new NotSupportedException(LocalMessage.Msg6);
                            }
                            break;
                        case ("D"):
                            // ToDo z$RelVs? PickOconv - mask decimal - MD2ZM
                            break;
                        default:
                            // no default masking
                            break;
                    }
                    break;
                default:
                    PickState.PickOconvStringStringPassedResult = StateIs.Undefined;
                    sField = sEmpty;
                    LocalMessage.Msg6 = "Post-Relational Output Converstion (" + sField_Conversion + ") does not exist";
                    throw new NotSupportedException(LocalMessage.Msg6);
            }
            // PickOconvStringStringPassedResult
            return sField;
        }
        public String PickOconv(int Field, String sField_Conversion)
        {
            string sTemp; string sTemp1;
            PickState.PickOconvStringPassedResult = StateIs.Started;
            sTemp = Field.ToString();
            sTemp1 = PickOconv(sTemp, sField_Conversion);

            return sTemp1;
        }
        // FUNCTION ICONV
        public String PickIConv(String sField, String sField_Conversion)
        {
            PickState.PickIConvResult = StateIs.Started;
            String sTemp = "put the command here";

            return sTemp;
        }
        public String PickIConv(int Field, String sField_Conversion)
        {
            PickState.PickIConvResult = StateIs.Started;
            String sTemp = "put the command here";

            return sTemp;
        }
        // FUNCTION FIELD
        public String PickField(String sField, String sField_Char, int Field_Occurence)
        {
            PickState.PickFieldResult = StateIs.Started;
            String sTemp = sField;
            int iLoc = 0;
            int iForCounter = 0;
            //For Loop
            for (iForCounter = 1; (iForCounter <= Field_Occurence && sTemp.Length > 0); iForCounter++)
            {
                iLoc = sTemp.IndexOf(sField_Char, 0);
                if (iForCounter < Field_Occurence)
                {
                    if (iLoc == -1)
                    {
                        sTemp = sEmpty;
                        break;
                    }
                    else
                    {
                        sTemp = sTemp.Substring(iLoc + 1);
                        if (iForCounter == Field_Occurence)
                        {
                            iLoc = sTemp.IndexOf(sField_Char, 0);
                            if (iLoc > 0)
                            {
                                sTemp = sTemp.Substring(1, iLoc);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            } // end of for
            return sTemp;
        }
        // STOP
        public virtual void PickStop()
        {
            // PickConsole Stop
            PickState.PickStopResult = StateIs.Started;

            //TraceDisplayMessageDetail = "STOP"; // ToDo z$RelVs? PickStop PICK STOP Analyze and implement proper handling of pick run abort.

            //RunAction = RunRunDo;
            //RunMetric = RunState;
            //RunTense = RunTense_Done;
            //RunActionState[RunRunDo, RunState] = RunTense_Done;
            //RunActionProgressChangeEventArgs = new ProgressChangedEventArgs(0,
            //    "R" + RunAction.ToString() + RunMetric.ToString() + RunTense.ToString() + TraceDisplayMessageDetail);
            //ThreadUiProgressAsync(ref Sender, RunActionProgressChangeEventArgs);
            // PickStop
        }
        // COUNT ATTRIBUTES
        public int PickAttrCountGet(String sField, String sCharacterToCount)
        {
            PickState.PickAttrCountGetResult = StateIs.Started;
            int iLoc = 0;
            int iForCounter = 0;
            int iLocCurrent = 0;
            //For Loop
            for (iForCounter = 1; (iLoc >= 0 && iLocCurrent <= sField.Length); iForCounter++)
            {
                iLoc = sField.IndexOf(sCharacterToCount, iLocCurrent);
                if (iLoc < 0)
                {
                    return iForCounter - 1;
                }
                else
                {
                    iLocCurrent = iLoc + 1;
                }
            }
            return iForCounter;
        }
        #endregion
        #region PickConversionFunctions
        // CHAR CONVERT
        public String PickConvert(String sField_Char, String sCharTo, String sField)
        {
            PickState.PickConvertStringResult = StateIs.Started;
            int iLoc;
            int iForCounter = 0;
            // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            // C#  Copy Code 
            // char[] delimit = new char[] { ' ' };
            // String PassedField = "The cat sat on the mat.";
            String sTemp1 = sEmpty;
            foreach (String substr in sField.Split((char[])sField_Char.ToCharArray(), StringSplitOptions.None))
            {
                iForCounter++;
                if (iForCounter > 1)
                {
                    sTemp1 += sCharTo;
                }
                sTemp1 += substr;
                // System.Console.WriteLine(substr);
            }
            return sTemp1;
        }
        // CHAR CONVERT 2
        public String PickConvertTypeTwo(String sField_Char, String sCharTo, String sField)
        {
            PickState.PickConvertTypeTwoResult = StateIs.Started;
            String sTemp1 = sEmpty;
            String sTemp2 = sEmpty;
            int iLoc = 0;
            int iForCounter = 0;
            // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            // C# For Loop
            do
            {
                iLoc = sField.IndexOf(sField_Char, 0);
                if (iLoc >= 0)
                {
                    iForCounter++;
                    sTemp1 = sField.Substring(0, iLoc - 1);
                    if (iLoc < sField.Length - 1)
                    {
                        sTemp2 = sField.Substring(iLoc + 1, sField.Length - iLoc - 1);
                    }
                    else
                    {
                        sTemp2 = sEmpty;
                    }
                    sField = sTemp1 + sCharTo + sTemp2;
                }
            } while (iLoc > 0);
            return sField;
        }

        #endregion
        #region PickStringFunctions
        // FUNCTIONS
        public String PickSpaceFill(int iPassedCharacterCount)
        {
            PickState.PickSpaceFillResult = StateIs.Started;
            String sTemp = sEmpty;

            // PickSpaceFill
            return sTemp;
        }
        public String PickStringFill(String StringPassed, int iPassedCharacterCount)
        {
            PickState.PickStringFillResult = StateIs.Started;
            String sTemp = sEmpty;

            // PickStringFill
            return sTemp;
        }
        // FUNCTION LENGTH
        public int PickLen(String StringPassed)
        {
            PickState.PickLenResult = StateIs.Started;
            // PickLen
            return StringPassed.Length;
        }
        #endregion
        #region PickInsertDelete
        // FUNCTION Delete
        public String PickDel(String StringPassed)
        {
            PickState.PickDelResult = StateIs.Started;
            return StringPassed = PickDel(StringPassed, 1);
        }
        public String PickDel(String StringPassed, int iPassedAttributeIndex)
        {
            PickState.PickDelAttributeResult = StateIs.Started;
            return StringPassed.DeleteField(iPassedAttributeIndex);
            // return Mdm.MickString1.DeleteField(StringPassed, iPassedAttributeIndex);
            // return StringPassed = StringPassed.DeleteField(iPassedAttributeIndex);
        }
        // FUNCTION Insert
        public String PickIns(String StringPassed)
        {
            PickState.PickInsStringResult = StateIs.Started;

            // PickInsString
            return StringPassed = PickIns(StringPassed, 1);
        }
        public String PickIns(String StringPassed, int iPassedAttributeIndex)
        {
            PickState.PickInsAttributeResult = StateIs.Started;
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
