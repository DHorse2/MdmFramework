using System;
#region  Mdm Core
using Mdm.Oss;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Decl;
using Mdm.Oss.File;
using Mdm.Oss.File.Control;
//@@@CODE@@@using Mdm.Oss.Mapp;
// using Mdm.Oss.Mobj;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
#endregion

namespace Mdm.Oss.Std
{
    /// <summary>
    /// Use StdBaseRunFileDef(long ClassHasPassed) and indicate 
    /// what features are used.
    /// This is the top level class for components
    /// that communication with the console and apps.
    /// Next: StdBaseRunFilePrinterConsole
    /// Top: StdConsoleManagerDef
    /// bool: PickConsoleOn?
    /// </summary> 
    /// <remarks>
    /// </remarks> 
    public class StdBaseRunFileDef : StdBaseRunDef, IDisposable
    {
        // ToDo No initialization? No.
        // ToDo This is an abstraction layer
        // ToDo and essentially providing
        // ToDo an (API) insertion point.
        public StdBaseRunFileDef(ref object SenderPassed)
            : base(ref SenderPassed)
        { }
        public StdBaseRunFileDef(ref object SenderPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
            : base(ref SenderPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        { }
        public StdBaseRunFileDef(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
            : base(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        { }
        public StdBaseRunFileDef()
            : base()
        {
            //Sender = this;
            //SenderThisIs = this;
        }
        /// <summary>
        /// This level of abscration currently includes no methods, fields and properties.
        /// It is defined by it's numerous companion enumerations and classes.
        /// </summary> 
        /// <param name=sEmpty></param> 
        /// <remarks>
        /// </remarks> 
        public StdBaseRunFileDef(ref object SenderPassed, ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed) 
             : base(ref SenderPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        { }
        public void InitializeStdBaseRunFile() {
            base.InitializeStdBaseRun();
        }
    }
}
