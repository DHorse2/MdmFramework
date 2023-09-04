using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mdm.Oss.Std
{
    #region Class Feature Control
    /// <summary>
    /// This enumeration is used to indicate what high level role is
    /// active within the base classes, what sub-classes will be 
    /// instantiated and guides the MVVC.
    /// This is defined in terms of Roles.
    /// </summary> 
    [Flags]
    public enum ClassRoleIs : long
    {
        None = 0x0,
        // MVC App Roles
        RoleAsApp, // Model
        RoleAsController, // Controller
        RoleAsUi, // View
        // Other roles
        RoleAsUtility, // Attached to Any App. (Ie. Non MVC)
        RoleAsThread, // Worker Thread
        RoleAsMdm =
            RoleAsApp
            | RoleAsUi
            | RoleAsController,
        All = RoleAsMdm | RoleAsUtility | RoleAsThread
    }
    /// <summary>
    /// This enumeration is used to indicate what Features are
    /// active within the base classes, what sub-classes will be 
    /// instantiated and guides the MVVC.
    /// This is defined in terms of Roles, feature groups (Masks),
    /// and individual features / functionality.
    /// </summary> 
    [Flags]
    public enum ClassFeatureIs : long
    {
        All = MaskUi
            | MaskButton
            | MaskStautsUi
            | MaskUtility
            | MaskId
            | MaskRun
            | MaskThread,
        None = 0x0,
        // 1 UI Components
        MaskUi = 0xF,
        Ui = 0x1, // (Full console)
        ProgressBar = 0x2,
        Buttons = 0x4, // Not used or wrong. ToDo
        // not used = 0x8,
        // Status UI and Progress Display
        MaskStautsUi = MaskStautsUiComponents | MaskStautsUiControls,
        // This isn't clear at all.
        // There are controls defined below.
        MaskStautsUiAsLine = Box | LineIsUsed | BoxManageIsUsed | BoxDelegateIsUsed,
        MaskStautsUiAsBox = Box | BoxIsUsed | BoxManageIsUsed | BoxDelegateIsUsed,
        // ToDo Clarify Box vs Line
        // ToDo Actually, don't even use this for now
        // ToDo It did have active code
        // ToDo but must be broken now.
        MaskStautsUiComponents = 0xF0,
        LineIsUsed = 0x10,
        BoxIsUsed = 0x20,
        BoxManageIsUsed = 0x40,
        BoxDelegateIsUsed = 0x80,
        // UI Display Method for status
        // What control is used for display.
        MaskStautsUiControls = 0xF00,
        StatusLine = 0x100,
        Box = 0x200,
        PopUp = 0x400,
        Window = 0x800,
        // 4 External Vs Local
        MaskId = 0xF000,
        LocalMessage = 0x1000,
        ExternalId = 0x2000,
        LocalId = 0x4000,
        // 5 Mdm Mask - Trace, Console, Send, Pring
        // Okay... This is clear... ish...
        // The visible control is MdmUtilConsole
        // This is the routing. Zero or more.
        MaskUtility = 0xF0000,
        MdmUtilTrace = 0x10000,
        MdmUtilConsole = 0x20000,
        MdmUtilSend = 0x40000,
        MdmUtilPrint = 0x80000,
        // Run Control
        // 6 Run Control Buttons
        MaskButton = 0xF00000,
        ButtonForStart = 0x100000,
        ButtonForPause = 0x200000, // a checkbox toggle
        ButtonForCancel = 0x400000,
        ButtonForOk = 0x800000,
        // 7 Run Control Used
        // including threads by default
        MaskRun = 0xF000000, 
        MdmRunControl = 0x1000000,
        MdmRunButtons = 0x2000000, // ToDo Not used or partially implemented.
        // not used = 0x4000000,
        // 7 Worker Thread
        MaskThread = 0x8000000,
        MdmThread = 0x8000000,
    }
    [Flags]
    public enum ConsoleFormUses : int
    {
        None = 0,
        All = 0,
        ErrorLog = 2,
        DatabaseLog = 3,
        UserLog = 1,
        DebugLog = 4
    }
    [Flags]
    public enum ConsoleSourceIs : int
    {
        None = 0,
        // A std (base) compliant app object source 
        // (derived from the console)
        // IE. The app has the console or there is None.
        Parent = 1,
        // I am a main program derived from std classes (this console)
        Self = 2,
        // Supplied by a program with a console object 
        // (a passed StdBaseRunFileConsole object)
        // IE and external class but not an App
        External = 3,
        // I am a program that implements ITraceMdm 
        // (and I pass sender) 
        Interface = 5
    }
    #endregion
}
