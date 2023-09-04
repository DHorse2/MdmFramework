using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.File.Db.Data;
using Mdm.Oss.File.RunControl;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.File.Control;
using Mdm.Oss.Components;

namespace Mdm.Oss.File.RunControl
{
    class AppStdFunctions : AppStd
    {
        #region Process
        public StdProcessDef StdProcess;
        public ref StdProcessDef StdProcessGet()
        {
            return ref StdProcess;
        }
        public void StdProcessSetFrom(ref StdProcessDef StdProcessPassed)
        {
            StdProcess = StdProcessPassed;
        }
        #endregion
        #region Key
        public StdKeyDef StdKey;
        public StdKeyDef KeyGet()
        { return StdKey; }
        public void KeySet(StdKeyDef StdKeyPassed)
        { StdKey = StdKeyPassed; }
        #endregion
        #region Notify Group
        public StdNotifyDef StdNotifyRoot;
        public StdNotifyDef StdNotify;
        public StdNotifyIconDef StdNotifyIcon;
        public ref StdNotifyDef StdNotifyGet(string Target)
        {
            if (Target == null || Target == "this" || Target == "")
            {
                return ref StdNotify;
            }
            else if (Target == "Root")
            {
                return ref StdNotifyRoot;
            }
            else if (Target == "Console")
            {
                return ref ((iClassFeatures)ConsoleSender).StdNotifyGet("this");
            }
            else
            {
                // throw;
            }
            return ref StdNotify;
        }
        public void StdNotifySet(ref StdNotifyDef StdNotifyPassed, string Target)
        {
            if (Target == null || Target == "this" || Target == "")
            {
                StdNotify = StdNotifyPassed;
            }
            else if (Target == "Root")
            {
                StdNotifyRoot = StdNotifyPassed;
            }
            else if (Target == "Console")
            {
                ((iClassFeatures)ConsoleSender).StdNotifySet(ref StdNotifyPassed, "this");
            }
            else
            {
                // throw;
            }
        }
        public void StdNotifySetFrom(ref StdNotifyDef StdNotifyPassed, string Target)
        {
            StdNotifyPassed = StdNotify;
            if (Target == null || Target == "this" || Target == "")
            {
                StdNotifyPassed = StdNotify;
            }
            else if (Target == "Root")
            {
                StdNotifyPassed = StdNotifyRoot;
            }
            else if (Target == "Console")
            {
                StdNotifyPassed = ((iClassFeatures)ConsoleSender).StdNotifyGet("this");
            }
            else
            {
                // throw;
            }
            StdNotifyPassed = StdNotify;
        }
        #endregion
        public virtual ref StdConsoleManagerDef ConsoleGetTo(ref StdConsoleManagerDef stPassed)
        {
            stPassed = st;
            return ref st;
        }
        public virtual StateIs ConsoleSetFrom(ref StdConsoleManagerDef stPassed)
        {
            // StdConsoleSet(new StdConsoleManagerDef(), new ClassRoleIs(), new ClassFeatureIs());
            st = stPassed as StdConsoleManagerDef;
            ConsoleSender = stPassed;
            return StateIs.Finished;
        }

        #region Console/Feature Management
        public ConsoleSourceIs ConsoleSource;
        public ClassRoleIs ClassRole;
        public ClassFeatureIs ClassFeatures;
        public object Sender;
        // This is the App's primary dblistview.
        private DbListViewStateGridsDef DbListView;
        private DataGridView GridView;
        #endregion
        #region State
        public StateIs Status; // of "this" object. ToDo Needs review and rationalization.
        public StateIs RunStatus; // implemented elsewhere
        public FileStatusDef FileStatus; // mFile level
        public StateIs ConsoleStatus; // not implemented yet, implemented elsewhere.
        public DataStatusIs DataStatus; // mFile Data level
        public StateIs FormStatus; // mFile Data level
        // standard object state.
        public bool ClassBusy;
        public bool ClassClosed;
        public bool ClassDisposed;
        public bool ClassEnabled;
        public bool ClassInitialized;
        public bool ClassUsed;
        public bool ClassOpen;
        public bool ClassVisble;
        #endregion
        #region Console Sender Object and Methods
        public StdConsoleManagerDef st;
        public object ConsoleSender; // This is st.
        public virtual ref StdBaseRunControlUiDef StdRunControlUiGet()
        {
            return ref StdRunControlUi;
        }
        public virtual StateIs StdRunControlUiSetFrom(ref StdBaseRunControlUiDef RunControlUiPassed)
        {
            StdRunControlUi = RunControlUiPassed;
            return StateIs.Finished;
        }

        public virtual ref object ConsoleGet()
        {
            return ref ConsoleSender;
        }
        public virtual ref object ConsoleGetTo(ref object stPassed)
        {
            stPassed = ConsoleSender;
            return ref ConsoleSender;
        }
        public virtual StateIs ConsoleSetFrom(ref object stPassed)
        {
            st = stPassed as StdConsoleManagerDef;
            ConsoleSender = st;
            return StateIs.Finished;
        }
        public virtual StateIs ConsoleSetFrom(ref object stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        {
            // StdConsoleSet(new StdConsoleManagerDef(), new ClassRoleIs(), new ClassFeatureIs());
            ConsoleSource = ConsoleSourcePassed;
            ClassRole = ClassRolePassed;
            ClassFeatures = ClassFeaturesPassed;
            if (stPassed != null)
            {
                st = stPassed as StdConsoleManagerDef;
                ConsoleSender = st;
                return StateIs.Initialized;
            }
            else
            {
                st = null;
                ConsoleSender = st;
                return StateIs.EmptyValue;
            }
        }
        // Console Source
        public virtual ConsoleSourceIs ConsoleSourceGet()
        {
            return ConsoleSource;
        }
        public virtual StateIs ConsoleSourceSet(ConsoleSourceIs ConsoleSourcePassed)
        {
            ConsoleSource = ConsoleSourcePassed;
            return StateIs.Finished;
        }
        // Console Features
        public virtual ClassFeatureIs ClassFeaturesGet()
        {
            return ClassFeatures;
        }
        public virtual StateIs ClassFeaturesSet(ClassFeatureIs ClassFeaturePassed)
        {
            ClassFeatures = ClassFeaturePassed;
            ((iClassFeatures)st).ClassFeaturesFlagsSet(ConsoleSource, ClassRole, ClassFeatures);
            return StateIs.Finished;
        }
        #endregion

        #region App Standard Role, Feature, Console Management
        public virtual StateIs ConsoleSetFrom(ref StdConsoleManagerDef stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        {
            st = stPassed;
            st.ConsoleSender = (object)st;
            st.ConsoleSource = ConsoleSourcePassed;
            st.ClassRole = ClassRolePassed;
            st.ClassFeatures = ClassFeaturesPassed;
            return StateIs.Finished;
        }
        // Console Source
        public virtual ClassRoleIs ClassRoleGet()
        {
            return st.ClassRole;
        }
        public virtual StateIs ClassRoleSet(ClassRoleIs ClassRolePassed)
        {
            ClassRole = ClassRolePassed;
            return StateIs.Finished;
        }
        public virtual void ClassFeaturesFlagsSet(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        {
            st.ClassFeaturesFlagsSet(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed);
        }
        #endregion
        #region Form Object
        public object FormParentObject;
        public object FormChildObject;
        public iStdBaseForm FormParent;
        public iStdBaseForm FormChild;
        public StdBaseRunControlUiDef StdRunControlUi;
        #endregion
        #region Form Get / Set
        public ref iStdBaseForm FormParentGet()
        {
            return ref FormParent;
        }
        public ref object FormParentObjectGet()
        {
            return ref FormParentObject;
        }
        public void FormParentSetFrom(ref iStdBaseForm ParentFormPassed)
        {
            FormParent = ParentFormPassed;
            FormParentObject = FormParent;
            if (FormParent == null)
            {
                FormStatus = StateIs.DoesNotExist;
            }
            else
            {
                FormStatus = StateIs.DoesExist; ;
            }
        }
        public void FormParentObjectSetFrom(ref object ParentFormPassed)
        {
            FormParentObject = ParentFormPassed;
            if (FormParentObject is iStdBaseForm)
            {
                FormParent = FormParentObject as iStdBaseForm;
            }
            if (FormParent == null)
            {
                FormStatus = StateIs.DoesNotExist;
            }
            else
            {
                FormStatus = StateIs.DoesExist; ;
            }
        }
        public void FormParentObjectSetFrom(ref Form ParentFormPassed)
        {
            FormParentObject = ParentFormPassed;
            if (FormParentObject is iStdBaseForm)
            {
                FormParent = FormParentObject as iStdBaseForm;
            }
            if (FormParent == null)
            {
                FormStatus = StateIs.DoesNotExist;
            }
            else
            {
                FormStatus = StateIs.DoesExist; ;
            }
        }
        #endregion
        #region DbListView and GridView
        public ref DbListViewStateGridsDef DbListViewGet() { return ref DbListView; }
        public void DbListViewSetFrom(ref DbListViewStateGridsDef DbListViewPassed) { DbListView = DbListViewPassed; }
        public virtual ref DataGridView GridViewGetDefault()
        {
            if (GridView == null)
            {
                Status = StateIs.DoesNotExist;
            }
            else
            {
                Status = StateIs.DoesExist;
            }
            return ref GridView;
        }
        public virtual void GridViewSetDefaultFrom(ref DataGridView GridViewPassed)
        {
            GridView = GridViewPassed;
            if (GridView == null)
            {
                Status = StateIs.DoesNotExist;
            }
            else
            {
                Status = StateIs.DoesExist;
            }
        }
        #endregion
        #region Button Control - Pause Cancel Hide...
        public void ButtonEnable()
        {
            ButtonDbEnable();
        }
        public void ButtonDbEnable()
        {
        }
        public void ButtonDbDisable()
        {
        }
        public void ButtonDisable()
        {
            ButtonDbDisable();
        }
        #endregion
        #region Reflection
        // From: https://stackoverflow.com/questions/6469027/call-methods-using-names-in-c-sharp#6469143
        public void InvokeInternalMethod(string MethodName, List<object> ArgsList)
        {
            // The following is acting on "this" class.
            GetType().GetMethod(MethodName).Invoke(this, ArgsList.ToArray());
        }
        public void InvokeMethod(object Target, string MethodName, List<object> ArgsList)
        {
            // , TargetType) This is all notes here.
            System.Type TargetType = Target.GetType();
            TargetType.GetMethod(MethodName).Invoke(Target, new[] { "world" });
            // or
            // typeof(TargetType).GetMethod(MethodName).Invoke(Target, ArgsList.ToArray());
            // Target.GetType().GetMethod(MethodName).Invoke(this, ArgsList.ToArray());
        }
        public void InvokeMethodScan(object Target, string MethodName, List<object> ArgsList)
        {
            System.Type TargetType = Target.GetType();
            // to strongly-type your method dictionary, 
            // you could make the keys of type MethodInfo 
            // and get them like this...
            MethodInfo[] TargetMethodInfos = TargetType.GetMethods();
            // And then you can do something like this...
            var TargetMethods = new Dictionary<MethodInfo, Object[]>();
            foreach (var TargetMethodItem in TargetMethods)
            {
                // Of course you would only do something similar to
                // in a test framework.
                TargetMethodItem.Key.Invoke(null, TargetMethodItem.Value);
                // 'null' may need to be an instance of the object that
                // you are calling methods on if these are not static methods
            }
        }
        // END OF StackOverFlow with thanks.
        #endregion
    }
}
