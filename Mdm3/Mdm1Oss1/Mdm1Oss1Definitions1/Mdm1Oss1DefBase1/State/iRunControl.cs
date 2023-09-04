using System.Windows.Forms;
using Mdm.Oss.Decl;
using Mdm.Oss.Console;
using Mdm.Oss.Std;

namespace Mdm.Oss.Std
{
    interface iRunControl : iClassFeatures
    {
        Form ParentFormGet();
        StateIs ParentFormSet(Form ParentFormPassed);
        StdConsoleManagerDef StdConsoleGet();
        StateIs StdConsoleSet(StdConsoleManagerDef stPassed, ClassFeatureIs ClassFeaturesPassed);
        void ButtonEnable();
        void ButtonDbEnable();
        void ButtonDisable();
        void ButtonDbDisable();

    }
}
