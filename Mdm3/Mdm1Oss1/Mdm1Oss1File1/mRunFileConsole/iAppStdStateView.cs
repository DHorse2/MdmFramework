using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Std;
using Mdm.Oss.File.Db.Data;
using Mdm.Oss.File.RunControl;

namespace Mdm.Oss.Std
{
    public interface AppStdStateView : AppStd
    {
        void OnFileSqlTaskDataEvent(Object sender, DbDataEventArgs e);
        #region Form
        //ref Form FormParentGet();
        //void FormParentSetFrom(ref Form ParentFormPassed);
        #endregion
        ref DbListViewStateGridsDef DbListViewGetTo(ref DbListViewStateGridsDef DbListViewPassed);
        void DbListViewSetFrom(ref DbListViewStateGridsDef DbListViewPassed);
        //ref DataGridView GridViewGet();
        //void GridViewSetFrom(ref DataGridView GridViewPassed);
        #region Console
        //ref StdConsoleManagerDef ConsoleGetTo(ref StdConsoleManagerDef stPassed);
        //StateIs ConsoleSetFrom(ref StdConsoleManagerDef stPassed);
        //ClassRoleIs ClassRoleGet();
        //StateIs ClassRoleSet(ClassRoleIs ClassRolePassed);
        //ClassFeatureIs ClassFeaturesGet();
        //StateIs ClassFeaturesSet(ClassFeatureIs ClassFeaturePassed);
        #endregion
        //void ButtonEnable();
        //void ButtonDisable();
        //void ButtonDbEnable();
        //void ButtonDbDisable();
    }
}
