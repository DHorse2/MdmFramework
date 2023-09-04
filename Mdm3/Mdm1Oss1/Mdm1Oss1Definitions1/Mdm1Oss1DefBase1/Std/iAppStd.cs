using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Std;

namespace Mdm.Oss.Std
{
    public interface AppStd : iClassFeatures
    {
        //ref DbListViewStateGridsDef DbListViewGet();
        //void DbListViewSetFrom(ref DbListViewStateGridsDef DbListViewPassed);
        ref DataGridView GridViewGetDefault();
        void GridViewSetDefaultFrom(ref DataGridView GridViewPassed);
        ref StdConsoleManagerDef ConsoleGetTo(ref StdConsoleManagerDef stPassed);
        StateIs ConsoleSetFrom(ref StdConsoleManagerDef stPassed);
        void ButtonEnable();
        void ButtonDisable();
        void ButtonDbEnable();
        void ButtonDbDisable();
    }
}
