using System.Windows.Forms;
using Mdm.Oss.Std;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Components;

namespace Mdm.Oss.Std
{
    public interface iClassFeatures
    {
        StdKeyDef KeyGet();
        void KeySet(StdKeyDef StdKeyPassed);
        ref StdNotifyDef StdNotifyGet(string Target);
        void StdNotifySet(ref StdNotifyDef StdNotifyPassed, string Target);
        void StdNotifySetFrom(ref StdNotifyDef StdNotifyPassed, string Target);
        ref object ConsoleGet();
        ref object ConsoleGetTo(ref object stPassed);
        StateIs ConsoleSetFrom(ref object stPassed);
        ConsoleSourceIs ConsoleSourceGet();
        StateIs ConsoleSourceSet(ConsoleSourceIs ConsoleSourcePassed);
        ClassRoleIs ClassRoleGet();
        StateIs ClassRoleSet(ClassRoleIs ClassRolePassed);
        ClassFeatureIs ClassFeaturesGet();
        StateIs ClassFeaturesSet(ClassFeatureIs ClassFeaturesPassed);
        void ClassFeaturesFlagsSet(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed);
        ref StdBaseRunControlUiDef StdRunControlUiGet();
        StateIs StdRunControlUiSetFrom(ref StdBaseRunControlUiDef RunControlUiPassed);
        ref iStdBaseForm FormParentGet();
        void FormParentSetFrom(ref iStdBaseForm ParentFormPassed);
        ref object FormParentObjectGet();
        void FormParentObjectSetFrom(ref object ParentFormPassed);
        void FormParentObjectSetFrom(ref Form ParentFormPassed);
        ref StdProcessDef StdProcessGet();
        void StdProcessSetFrom(ref StdProcessDef StdProcessPassed);

    }
}
