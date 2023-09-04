using System;
#region  Mdm Core
using Mdm.Oss;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Decl;
using Mdm.Oss.File.Control;
//@@@CODE@@@using Mdm.Oss.Mapp;
// using Mdm.Oss.Mobj;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
//using Mdm.Oss.Sys;
//using Mdm.Oss.Thread;
//using Mdm.Oss.Threading;
//using Mdm.Pick;
//using Mdm.Pick.Console;
//using Mdm.World;
#endregion

namespace Mdm.Oss.Std
{
    /// <summary>
    /// INSTRUCTIONS: 
    /// 1) This is your base class for components
    /// that have exception handling and can talk to 
    /// (pass messages) to the Console. It is the
    /// Extended Standard Object such that it can
    /// talk the the MVC framework.
    /// 
    /// 2) If you need Run Control use StdBaseRunFileDef
    /// given that database file I/O is assumed.
    /// 
    /// 3) Application create an instance of StdConsoleManagerDef
    /// placing all features within a single object that is passed
    /// to key classes created with it. This is named "st".
    /// 
    /// 4) Utility applications inherit StdConsoleManagerDef
    /// and have full access built in. 
    /// 
    /// 5) They all still use the "st" object in messaging. Including
    /// StdConsoleManagerDef itself.
    /// 
    /// 6) Includes subclasses for Sender, st, App Forms, Role and Features.
    /// Also some utility functions, introspection and temporary working
    /// fields.  Does not include any additional features.
    /// 
    /// 7) The C# Syntax extension. Eek. 
    /// There is emulation of an existing language (part of it) lurking
    /// within the inheritance at the PICK level. 
    /// Unfortunately it shows up when navigating classes
    /// and 
    /// 
    /// 7) (ToDo) I haven't had time to address it.
    /// It's purpose it to allow greatly simplified code conversion
    /// from standard PICK to C#. 
    /// 
    /// 8) Similarly the SRT code implements parts of the transformation
    /// library that was used in the database conversion / abstraction
    /// layer and application.
    /// 
    /// Next: StdBasePickSyntaxDef
    /// Specificity: StdBasePickSyntaxDef : StdBaseRunDef : StdBaseRunFilePrinterConsole : StdConsoleManagerDef
    /// </summary> 
    public class StdBaseDef : StdDef, IDisposable
    {
        public StdBaseDef(ref object SenderPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        : base(ref SenderPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        { }
        public StdBaseDef(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
            : base(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        { }
        public StdBaseDef(ref object SenderPassed, ref object stPassed)
            : base(ref SenderPassed, ref stPassed)
        { }
        public StdBaseDef(ref object SenderPassed)
            : base(ref SenderPassed)
        { }
        public StdBaseDef()
            : base()
        {
            Sender = this;
            SenderIsThis = this;
        }
        public StdBaseDef(ref object SenderPassed, ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ref SenderPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        { }
        public virtual void InitializeStdBase()
        {
            base.InitializeStd();
        }
        #region FunctionConstants
        #region Network Drives
        // O: Operating system
        public const String DriveOs = @"\\localhost\LocalOS";
        // P: ProgramData and Data
        public const String DriveData = @"\\localhost\LocalData";
        // Q: Development
        public const String DriveDev = @"\\localhost\LocalDev";
        #endregion
        #region Class Scope Status
        protected internal InternalIdDef InternalId;
        protected internal ExternalIdDef ExternalId;
        protected internal LocalIdDef LocalId;
        #endregion
        //#region Class Messages
        //protected internal LocalMessageDef LocalMessage;
        //#endregion
        #region MessageControlConstants
        // protected void TraceMdmDo(ref Sender, bIsMessage, TraceMdmDetailLine, 0, ErrorDidNotOccur, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, 
        protected const int iNoOp = 0;
        protected const bool bNoOp = false;
        protected const long lNoOp = 0;
        // Field 1 - Method / Function int Status
        // Field 2 - Error
        protected const bool ErrorDidOccur = true;
        protected const bool ErrorDidNotOccur = false;
        // Field 3 - Error Level
        protected const int ErrorIsInformation = 1;
        protected const int ErrorIsWarning = 2;
        protected const int ErrorIsRecoverable = 3;
        protected const int ErrorIsFatal = 4;
        protected const int ErrorWithoutLevel = 0;
        // Field 4 - Error Source
        protected const int ExceptionFromOsNotSupported = 1;
        protected const int ExceptionFromDatabase = 2;
        protected const int ExceptionFromOs = 3;
        protected const int ExceptionFromFile = 4;
        protected const int ExceptionFromPath = 5;
        protected const int ExceptionFromData = 6;
        protected const int ExceptionFromView = 7;
        protected const int ExceptionFromValidation = 8;
        protected const int ExceptionFromFormatting = 9;
        protected const int ExceptionFromDevice = 10;
        protected const int ExceptionFromUser = 11;
        protected const int ExceptionFromNetwork = 12;
        protected const int ExceptionFromSecurity = 13;
        protected const int ExceptionFromInput = 14;
        protected const int ExceptionFromProgram = 15; // 202101 Dgh
        // Field X - Display this Message
        protected const int iNoErrorLevel = 0;
        protected const int iNoErrorSource = 0;
        protected const int iNoMethodResult = 0;
        protected const int iNoValue = 0;
        protected const bool bDoNotDisplay = false;
        protected const bool bDoDisplay = true;
        protected const bool bIsMessage = true;
        protected const bool bIsData = false;
        protected const bool bNoError = false;
        // Field 5 - User Binary Flags
        // User Response
        protected const int MessageEnterOk = 1;
        protected const int MessageEnterOkCancel = 2;
        protected const int MessageEnterAnyKey = 4;
        protected const int MessageEnterF5 = 85;
        protected const int MessageEnterResume = 75;
        protected const int MessageNoUserEntry = 0;
        // User has Console access
        protected const int MessageAllowConsole = 100;
        // User input times out with default
        protected const int MessageAllowTimeOut = 200;
        protected const int MessageUseBox = 400;
        protected const int MessageIsModal = 800;
        // Message routing to other levels
        protected const int MessageTellAdmin = 10000;
        protected const int MessageTellSuper = 20000;
        protected const int MessageTellGroup = 40000;
        protected const int MessageTellSupport = 80000;
        // Field 6 - Message
        //
        //
        #endregion
        #endregion
        public void ExceptionCatchNotSupportedMdm(String MsgPassed)
        {
            throw new NotSupportedException(MsgPassed);
            //catch (NotSupportedException nse)
            //{
            //    LocalMessage.ErrorMsg = "Not Supported Exception(#0) occured in Pick File Action(#0)";
            //    ExceptNotSupportedImpl(ref FmainPassed, ref ExceptionNotSupported, LocalMessage.ErrorMsg, FileWriteResult);
            //    FileWriteResult = StateIs.Failed;
            //}
        }
    }
}
