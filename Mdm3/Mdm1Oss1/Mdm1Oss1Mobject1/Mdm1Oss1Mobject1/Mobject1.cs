using System; // Page // App
using System.Collections;
using System.Collections.Generic; // Page // App
using System.Configuration;  // App
using System.ComponentModel;
using System.Data; // App
using System.Diagnostics;
// using System.Drawing;
using System.IO;
using System.Linq; // Page // App
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text; // Page
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows; // Page // App
using System.Windows.Controls; // Page
// using System.Windows.Data; // Page
// using System.Windows.Documents; // Page
using System.Windows.Forms; // UrlHist
// using System.Windows.Input; // Page
// using System.Windows.Media; // Page
// using System.Windows.Media.Imaging; // Page
// using System.Windows.Navigation; // Page // App
// using System.Windows.Shapes; // Page
//
// Mdm1Oss1Mapp1Ass
using Mdm;
using Mdm.Oss;
//@@@CODE@@@using Mdm.Oss.ClipUtil;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Components;
using Mdm.Oss.Mobj;
//@@@CODE@@@using Mdm.Oss.Support;
using Mdm.Oss.Std;
using Mdm.Oss.Sys;
//@@@CODE@@@using Mdm.Oss.UrlUtil.Hist;
using Mdm.Oss.File;
using Mdm.World;

namespace Mdm.Oss.Mobj {

    /// <summary>
    /// <para> Delegate to route changed events.</para>
    /// </summary>
    public delegate void ChangedEventHandler(Object SenderPassed, EventArgs e);

    /// <summary>
    /// <para> Mdm Object</para>
    /// <para> Final Base Object Class.</para>
    /// <para> Intended as a lightweight smart object.</para>
    /// <para> Put more clearly, this a base class with several
    /// pointers into the Application Framework.  This enables
    /// interoperation, communication, and distribution 
    /// of responsibilities using a role based design pattern.</para>
    /// <para> . </para>
    /// <para> The main functionality allows interoperation between
    /// a set of predefined classes representing roles.  At the
    /// application level, there is a MVVC pattern usage.  The 
    /// classes that are introduced in Mobject are components
    /// within the MVVC and as implemented over an Application
    /// Framework.</para>
    /// <para> . </para>
    /// <para> These "Core" classes present in all applications
    /// represent a simple but effective way to implement
    /// advanced OSS features consistently across the
    /// application.</para>
    /// <para> . </para>
    /// <para> The predefined objects are:</para>
    /// <para> ..mCo - Mcontroller. </para>
    /// <para> ..mMa - Mapplication</para>
    /// <para> ..mVe - Verb</para>
    /// <para> ..mVt - Verb Thread</para>
    /// <para> ..mAp - Current Application. </para>
    /// <para> ..mMo - Mobject</para>
    /// <para> ..mCl - Mdm Clipboard</para>
    /// <para> ..mUr - Mdm UrlHistory</para>
    /// <para> ..mPd - Database Page</para>
    /// <para> ..mPm - Main Page</para>
    /// <para> . </para>
    /// <para> Mobject class implements the object get and set routines</para>
    /// </summary>

    public class Mtext : StdBaseDef
    {
        public List<String> Value;
        public string PathAndStuff;
        
        List<String> Get()
        { return Value; }
        void Set(List<String> ValuePassed)
        { Value = ValuePassed; }
        //public String Value;
        //public string[] Value;
        //public List<String or StringBuilder> Value;
        #region StringBuilder Wrapper Methods
        #endregion
        #region StringBuilder Extensions
        public void TestExtension()
        {

        }
        #endregion
    }
    public class Mstring : StdConsoleManagerDef
    {
        public StringBuilder Value;
        //public String Value;
        //public string[] Value;
        //public List<String or StringBuilder> Value;
        #region StringBuilder Wrapper Methods
        #endregion
        #region StringBuilder Extensions
        public void TestExtension()
        {

        }
        // ToDo any StringBuilder Extension method
        public StateIs EndsWith(string EndsWithPassed)
        {
            return 0;
        }
        public StateIs StartsWith(string StartsWithPassed)
        {
            return 0;
        }
        public StateIs Contains(string ContainsPassed)
        {
            return 0;
        }
        #endregion
    }
    public class MstringConsole : Mstring
    {
        #region StringBuilder Wrapper Methods
        #endregion
        #region StringBuilder Extensions
        public new void TestExtension()
        {

        }
        #endregion
        // ToDo any stringConsole method
        // ToDo EXTRACT (process)
        // ToDo FIND (select)
        // ToDo CONDITIONS (pre)
        // ToDo OUTPUT (post)
        // ToDo EXECUTE
        // ToDo SCRIPT EXECUTE
        // ToDo SCRIPT READ
        // ToDo SCRIPT READ NEXT

    }
    public class Mobject : StdConsoleManagerDef {

        #region StandardIdentifierPrefix
        // Class Standard Properties
        // oo   = Object - Object Type not specified
        // omo  = Object - Mdm  - Mobject
        // omc  - Object - Mdm  - Mdm App
        // omc  - Object - Mdm  - TODO
        // oe   - Object - Exception
        // of   - Object - File
        // ofb  - Object - File - Buffer
        // ofs  - Object - File - File Stream
        // ofsr - Object - File - File Stream Reader
        // ofdc - Object - File - Databse Command
        // ofd  - Object - File - FileOwnerList Connection
        // ofe  - Object - File - Exception
        // oh   - Object - HashTable
        // os   - Object - Structure
        #endregion
        #region StandardIdentifierSuffix
        #endregion
        #region Mdm.Oss.Mobj - StandardObjectClass
        #region StandardObjects
        /// <MdmSrtRels Vs="MdmSrtRels0_8_5">
        #region PackageObjectDeclarations ABSTRACT
        /// Mdm1 Oss1 Mobject Standard Object Class 
        /// Mdm.Oss.Mobj - Mobject
        /// See comments in region StandardIdentifierPrefix above
        public Sobject XUom;
        public Dictionary<String, Type> SobjectTypes;
        // <Area Id = "Mapplication">
        // <Area Id = "XUomApvvXvlication">
        public System.Windows.Application XUomApvvXv; // Generated App
        // <Area Id = "omHControl">
        public Object XUomCovvXv; // Mcontroller, Mdm Controller, OBSOLETE Name: Controller
        // <Area Id = "omO">
        public Mobject XUomMovvXv; // Mobject, Mdm Object
        // <Area Id = "MdmClassLevelSenders">
        // public Object SenderIsThis;
        // <Area Id = "XUomApvvXvlication">
        public Object XUomMavvXv; // Mapplication, Mdm App
        // <Area Id = "MdmLocalVerb">
        public Object XUomVevvXv; // MinpuTld
        // <Area Id = "MdmLocalVerbThread>
        public Object XUomVtvvXv; // MinpuTldThread
        // <Area Id = "MdmLocalWorkerThreadInstance>
        public BackgroundWorker XUomVbvvXv; // MinpuTldThreadBgWorker
        // <Area Id = "omU">
        //@@@CODE@@@public MurlHist1Form1 XUomUrvvXv;
        // public System.Windows.Forms.Form XUomUrvvXv;
        // <Area Id = "omcLocalClipboard">
        //@@@CODE@@@public ClipFormMain XUomClvvXv;
        //
        #region Page Declartions
        // <Area Id = "opPageObjects">
        // <Area Id = "omP">
        public Page XUomPmvvXv;
        public String sPage1ReturnValue;
        // <Area Id = "omP2">
        public Page XUomPdvvXv;
        public String sPage2ReturnValue;
        #endregion
        #endregion
        #region Package Object Declarations         see Mapplication
        /* // <Section Role="Declarations">
        // <Section Id = "MdmStandardObject">
        // <Section Vs="MdmStdObjVs0_8_8">
        // MdmStandardObject MdmStdObjVs0_8_8
        // <Area Id = "MdmMinputTld">
        // <Area Id = "Mapplication">
        // <Area Id = "XUomApvvXvlication">
        // public System.Windows.Application XUomApvvXv;
        // <Area Id = "omHControl">
        // public Object XUomCovvXv;
        // <Area Id = "XUomMovvXv Mapplication">
        // public Mapplication XUomMovvXv;
        // <Area Id = "omoLocalMop Mobject">
        // public Mobject XUomMovvXv;
        // <Area Id = "omP">
        // public Page XUomPmvvXv;
        // <Area Id = "MdmMinputTld">
        // public Object XUomVtvvXv;
        // <Area Id = "ConsoleObject">
        */
        #endregion
        #endregion
        /* #region MdmStandardIoObject                 see Mapplication
        // <Area Id = "ConsoleObject">
        public TextWriter ocotConsoleWriter;
        // public TextWriter ocotStandardOutput;
        public TextReader ocitConsolRoutedEventder;
        public StreamWriter ocoConsoleWriter;
        // public StreamWriter ocoStandardOutput;
        public StreamReader ociConsolRoutedEventder;
        // public StreamWriter ocetErrorWriter;
        public IOException eIoe;
        public TextWriter ocetErrorWriter;
        //
        #endregion*/
        #region ClassInternalResults
        internal StateIs iMobjectStatus;
        internal StateIs iMobjectPassedAppStatus;
        internal StateIs iMobjectStartAppStatus;
        internal StateIs iMobjectResetStatus;

        internal StateIs AppSetObjectByTypeStatus;
        #endregion
        #region ClaseInternalCoreObjectResults
        public StateIs AppAppObjectGetStatus;
        public StateIs AppAppObjectSetStatus;
        public StateIs AppMappObjectGetStatus;
        public StateIs AppMappObjectSetStatus;
        public StateIs AppMcontrollerObjectGetStatus;
        public StateIs AppMcontrollerObjectSetStatus;
        public StateIs AppMapplicationObjectGetStatus;
        public StateIs AppMapplicationObjectSetStatus;
        public StateIs AppMobjectObjectGetStatus;
        public StateIs AppMobjectObjectSetStatus;

        public StateIs iMapplicationStatus;
        public StateIs iMapplicationPassedAppStatus;
        public StateIs iMapplicationStartAppStatus;

        public StateIs iPageMainObjectGetStatus;
        public StateIs iPageMainObjectSetStatus;
        public StateIs iPageMainSetCoreObjectsStatus;
        public StateIs iPageMainLoadedStatus;
        public StateIs iPageMainSetDefaultStatus;
        public StateIs iDbDetailPageObjectGetStatus;
        public StateIs iDbDetailPageObjectSetStatus;
        public StateIs iDbDetailPageSetCoreObjectsStatus;
        public StateIs iDbDetailPageLoadedStatus;
        public StateIs iDbDetailPageSetDefaultStatus;

        public StateIs AppVerbObjectGetStatus;
        public StateIs AppVerbObjectSetStatus;
        public StateIs AppVerbThreadObjectGetStatus;
        public StateIs AppVerbThreadObjectSetStatus;
        public StateIs AppVerbBgWorkerObjectGetStatus;
        public StateIs AppVerbBgWorkerObjectSetStatus;

        protected StateIs AppCoreObjectCreateStatus;
        protected StateIs AppCoreObjectGetFromAppStatus;
        protected StateIs AppCoreObjectSetInMapplicationStatus;
        protected StateIs AppCoreObjectGetFromMavvXvpplicationStatus;
        protected StateIs AppObjectGetStatus;
        protected StateIs AppObjectSetStatus;
        #endregion
        #endregion
        #region Class Mdm1 Oss1 properties
        //
        #endregion
        #region MobjectConstructor
        public Mobject(ref object SenderPassed, ref StdConsoleManagerDef stPassed)
            : base(ref SenderPassed, ref stPassed, ConsoleSourceIs.Interface, stPassed.ClassRole, stPassed.ClassFeatures)
        {
            iMobjectStatus = StateIs.Started;
            MobjectInitialize();
        }
        public Mobject(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        {
            iMobjectStatus = StateIs.Started;
            MobjectInitialize();
        }
        public Mobject()
             : base()
        {
            iMobjectStatus = StateIs.Started;
            MobjectInitialize();
        }
        public Mobject(ref object SenderPassed)
            : base(ref SenderPassed, ConsoleSourceIs.None, ClassRoleIs.None, ClassFeatureIs.None)
        {
            iMobjectStatus = StateIs.Started;
            MobjectInitialize();
        }
        public Mobject(ref object SenderPassed, ConsoleSourceIs SenderObjectSource, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ref SenderPassed, ConsoleSourceIs.Interface, ClassRolePassed, ClassFeaturesPassed)
        {
            iMobjectStatus = StateIs.Started;
            MobjectInitialize();
        }
        public Mobject(ref object SenderPassed, ref StdConsoleManagerDef stPassed, ConsoleSourceIs SenderObjectSource, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ref SenderPassed, ref stPassed, ConsoleSourceIs.Interface, ClassRolePassed, ClassFeaturesPassed) {
            iMobjectStatus = StateIs.Started;
            MobjectInitialize();
        }
        public Mobject(ref object SenderPassed, ref StdConsoleManagerDef stPassed, ConsoleSourceIs SenderObjectSource, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed, ref System.Windows.Application PassedA)
             : base(ref SenderPassed, ref stPassed, ConsoleSourceIs.Interface, ClassRolePassed, ClassFeaturesPassed)
            {
                iMobjectPassedAppStatus = StateIs.Started;
            if (PassedA != null) {
                if (XUomApvvXv != null) {
                    XUomApvvXv = System.Windows.Application.Current;
                }
                XUomApvvXv = PassedA;
            }
            MobjectInitialize();
            // iMobjectPassedApp
        }
        public Mobject(ref object SenderPassed, ref StdConsoleManagerDef stPassed, ConsoleSourceIs SenderObjectSource, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed, ref Mobject PassedM, ref System.Windows.Application PassedA, Page PassedP)
             : base(ref SenderPassed, ref stPassed, ConsoleSourceIs.Interface, ClassRolePassed, ClassFeaturesPassed)
        {
            iMobjectPassedAppStatus = StateIs.Started;
            if (PassedM != null)
            {
                if (XUomMovvXv != null)
                {
                    XUomMovvXv = null;
                }
                XUomMovvXv = PassedM;
            }
            if (PassedA != null) {
                if (XUomApvvXv != null) {
                    XUomApvvXv = null;
                }
                XUomApvvXv = PassedA;
            }
            if (PassedP != null) {
                if (XUomPmvvXv != null) {
                    XUomPmvvXv = null;
                }
                XUomPmvvXv = PassedP;
            }
            MobjectInitialize();
            // iMobjectPassedApp
        }

        #endregion
        #region MobjectInstanceManagement
        #region MobjectEngine
        public virtual StateIs MobjectInitialize() {
            iMobjectStartAppStatus = StateIs.Started;
            //
            XUom = new Sobject();
            XUom.Sender = this;
            XUom.ItemType = this.GetType();
            //
            SobjectTypes = new Dictionary<String, Type>();
            SobjectTypes.Add("Application", typeof(System.Windows.Application));
            SobjectTypes.Add("Mobject", typeof(Mobject));
            SobjectTypes.Add("BackgroundWorker", typeof(BackgroundWorker));
            //@@@CODE@@@SobjectTypes.Add("MurlHist1Form1", typeof(MurlHist1Form1));
            //@@@CODE@@@SobjectTypes.Add("ClipFormMain", typeof(ClipFormMain));
            //
            Sender = this;
            XUomUrvvXvCreateNow = false;
            XUomClvvXvCreateNow = false;
            return iMobjectPassedAppStatus;
            // iMobjectStartApp
        }

        #region Mobject Sobject replacement introspection object handling...
        // todo MobjectCreate Tested but not implemented
        public virtual StateIs MobjectCreate(String NamePassed, Type TypePassed, Object ObjectPassed) {
            AppSetObjectByTypeStatus = StateIs.Started;
            
            // Type type = typeof(int);
            // AppSetObjectByTypeStatus = MobjectGet(ObjectPassed.GetType().ToString(), ref ObjectPassed);
            // Create an instance of a type.
            // Type t = SobjectTypes[NamePassed];
            if (TypePassed != null) {
                Object[] args = new Object[] { 8 };
                // args[0] = Xuom;
                args[0] = XUomCovvXv;
                System.Console.WriteLine("The value of x before the constructor is called is {0}.", args[0]);
                ObjectPassed = TypePassed.InvokeMember(null,
                    BindingFlags.DeclaredOnly |
                    BindingFlags.Public | 
                    BindingFlags.NonPublic |
                    BindingFlags.FlattenHierarchy |
                    BindingFlags.Instance | 
                    BindingFlags.CreateInstance, 
                    null, null, args);
                //Specify BindingFlags.Public to include public members in the search.
                //Specify BindingFlags.NonPublic to include non-public members 
                //      (that is, private and protected members) in the search.
                //Specify BindingFlags.FlattenHierarchy to include static members up the hierarchy.
                //CreateInstance to invoke a constructor. name is ignored. 
                //      Not valid with other invocation flags.

                // typeof(C).InvokeMember("F", BindingFlags.SetField, null, C, new Object{"strings new value"});

                //Permissions
                //ReflectionPermission  
                //for accessing non-public members when the grant set of the non-public members is restricted to the caller's grant set, or a subset thereof. Associated enumeration: ReflectionPermissionFlag..::.RestrictedMemberAccess
                //
                //ReflectionPermission  
                //for accessing non-public members regardless of their grant set. Associated enumeration: ReflectionPermissionFlag..::.MemberAccess
                //
                //SecurityPermission  
                //to call unmanaged code. Associated enumeration: SecurityPermissionFlag..::.UnmanagedCode

                MobjectSet(ObjectPassed);
                AppSetObjectByTypeStatus = StateIs.DoesExist;
            } else {
                AppSetObjectByTypeStatus = StateIs.DoesNotExist;
            }

            return AppSetObjectByTypeStatus;        
        }

        // todo MobjectCreate Tested but not implemented
        public virtual StateIs MobjectSet(Object PassedOb) {
            AppSetObjectByTypeStatus = StateIs.Started;
            // Todo XUom Sobject
            XUom.ItemType = PassedOb.GetType();
            XUom.ItemBaseType = XUom.ItemType.BaseType;
            XUom.Namespace = XUom.ItemType.Namespace;
            XUom.Name = ((XUom.ItemType).ToString()).FieldLast(".");
            if (!XUom.Items.ContainsKey(XUom.Name)) {
                XUom.Items.Add(XUom.Name, PassedOb);
            } else {
                XUom.Items[XUom.Name] = PassedOb;
            }
            return AppSetObjectByTypeStatus;
        }
        // StateIs.DoesExist;
        // StateIs.DoesNotExist;

        // todo MobjectCreate Tested but not implemented
        public virtual StateIs MobjectGetTo(ref Object ObjectPassed) {
            AppSetObjectByTypeStatus = StateIs.Started;
            AppSetObjectByTypeStatus = MobjectGet(ObjectPassed.GetType().ToString(), ref ObjectPassed);
            return AppSetObjectByTypeStatus;
        }

        public virtual StateIs MobjectGet(Type TypePassed, ref Object ObjectPassed) {
            AppSetObjectByTypeStatus = StateIs.Started;
            AppSetObjectByTypeStatus = MobjectGet(TypePassed.ToString(), ref ObjectPassed);
            return AppSetObjectByTypeStatus;
        }

        public virtual StateIs MobjectGet(String NamePassed, ref Object ObjectPassed) {
            AppSetObjectByTypeStatus = StateIs.Started;
            // ToDo Sobject evalutation and changes to XU object storage using dict
            if (!XUom.Items.ContainsKey(NamePassed)) {
                AppSetObjectByTypeStatus = StateIs.DoesExist;
                ObjectPassed = XUom.Items[NamePassed];
            } else {
                AppSetObjectByTypeStatus = StateIs.DoesNotExist;
                ObjectPassed = null;
            }
            return AppSetObjectByTypeStatus;
        }

        /////////////////////////////////////////////////////////////
        #endregion

        public virtual StateIs AppSetObjectByType(ref Object PassedOb) {
            AppSetObjectByTypeStatus = StateIs.Started;
            Type PassedType = PassedOb.GetType();
            Type PassedBaseType = PassedType.BaseType;
            String PassedNamespace = PassedType.Namespace;

            // todo MobjectCreate Tested but not implemented
            XUom.ItemType = PassedOb.GetType();
            XUom.ItemBaseType = XUom.ItemType.BaseType;
            XUom.Namespace = XUom.ItemType.Namespace;
            XUom.Name = ((XUom.ItemType).ToString()).FieldLast(".");
            if (!XUom.Items.ContainsKey(XUom.Name)) {
                XUom.Items.Add(XUom.Name, PassedOb);
            } else {
                XUom.Items[XUom.Name] = PassedOb;
            }

            if (PassedType.BaseType == typeof(System.Windows.Controls.Page)) {
                if (PassedType.ToString() == PassedType.Namespace + ".MinputTldPageMain" || PassedType.BaseType.ToString() == PassedType.Namespace + ".MinputTldPageMain") {
                    // XUomPmvvXv = this;
                    XUomPmvvXv = (Page)PassedOb;
                }
            }
            if (PassedType.BaseType == typeof(System.Windows.Controls.Page)) {
                if (PassedType.ToString() == PassedType.Namespace + ".MinputTldDbDetailPage" || PassedType.BaseType.ToString() == PassedType.Namespace + ".MinputTldDbDetailPage") {
                    // XUomPdvvXv = this;
                    XUomPdvvXv = (Page)PassedOb;
                }
            }
            if (PassedType.BaseType == typeof(System.Windows.Application)) {
                // XUomApvvXv = this;
                XUomApvvXv = (System.Windows.Application)PassedOb;
            }
            if (PassedType.ToString() == PassedType.Namespace + ".Mapplication" || PassedType.BaseType.ToString() == PassedType.Namespace + ".Mapplication") {
                // XUomMavvXv = this;
                XUomMavvXv = PassedOb;
            }
            if (PassedType.ToString() == PassedType.Namespace + ".Mcontroller" || PassedType.BaseType.ToString() == PassedType.Namespace + ".Mcontroller") {
                // XUomCovvXv = this;
                XUomCovvXv = PassedOb;
            }
            if (PassedType.ToString() == PassedType.Namespace + ".MinputTld" || PassedType.BaseType.ToString() == PassedType.Namespace + ".MinputTld") {
                // XUomVevvXv = this;
                XUomVevvXv = PassedOb;
            }
            if (PassedType.ToString() == PassedType.Namespace + ".MinputTldThread" || PassedType.BaseType.ToString() == PassedType.Namespace + ".MinputTldThread") {
                // XUomVtvvXv = this;
                XUomVtvvXv = PassedOb;
            }
            //
            return AppSetObjectByTypeStatus;
        }
        #endregion
        #region MapplicationObjects
        #region MdmAppCoreObjectCreation
        public virtual StateIs AppCoreObjectCreate(ref Page PassedMinputTldPageMain) {
            XUomPmvvXv = (Page)PassedMinputTldPageMain;
            AppCoreObjectCreate();
            return 0;
        }
        public virtual StateIs AppCoreObjectCreate(ref System.Windows.Application PassedApplication) {
            XUomApvvXv = (System.Windows.Application)PassedApplication;
            AppCoreObjectCreate();
            return 0;
        }
        public virtual StateIs AppCoreObjectCreate(ref Mobject PassedMobject) {
            XUomMovvXv = PassedMobject;
            if (PassedMobject.XUomPmvvXv != null) { XUomPmvvXv = PassedMobject.XUomPmvvXv; }
            if (PassedMobject.XUomApvvXv != null) { XUomApvvXv = PassedMobject.XUomApvvXv; }
            AppCoreObjectCreate();
            return 0;
        }
        public virtual StateIs AppCoreObjectSetAll(ref Mobject PassedOb) {
            AppMobjectObjectSetStatus = StateIs.Valid;
            if (XUomMavvXv == null) {
                if (PassedOb != null) {
                    XUomMovvXv = (Mobject)PassedOb;
                    //XUomCovvXv = (Mcontroller)XUomCovvXv;
                    XUomMavvXv = PassedOb.XUomMavvXv;
                    XUomApvvXv = (System.Windows.Application)PassedOb.XUomApvvXv;
                    XUomPmvvXv = PassedOb.XUomPmvvXv;
                    XUomPdvvXv = PassedOb.XUomPdvvXv;
                    XUomVevvXv = PassedOb.XUomVevvXv;
                    XUomVtvvXv = PassedOb.AppVerbBgWorkerObjectGet();
                }
            }
            AppMobjectObjectSetStatus = StateIs.Finished;
            return AppMobjectObjectSetStatus;
        }
        //
        public virtual StateIs AppCoreObjectCreate() {
            AppCoreObjectCreateStatus = StateIs.Started;
            // Start Up Ui Capable Controller Unit
            // ToDo AppCoreObjectCreate do not use this code at this time
            // MinputTldApp XUomApvvXv - App being run
            if (XUomApvvXv == null) {
                // XUomApvvXv = XUomApvvXv;
                XUomApvvXv = System.Windows.Application.Current;
            }
            // MinputTldApp XUomPmvvXv - Main or Home Page
            if (XUomPmvvXv == null) {
                // XUomPmvvXv = ref XUomPmvvXv;
                XUomPmvvXv = new Page();
            }
            // MinputTldApp XUomMavvXv - Standard Object
            if (XUomMavvXv == null) {
                // XUomMavvXv = (Mobject)XUomMavvXv;
                XUomMavvXv = new Object();
            }
            AppCoreObjectCreateStatus = AppSetObjectByType(ref Sender);
            if (XUomMavvXv == null) {
                XUomMavvXv = new Object();
                AppCoreObjectCreateStatus = AppMappObjectSet(XUomMavvXv);
                SenderIsThis = XUomPmvvXv;
                AppCoreObjectCreateStatus = PageMainObjectSetFrom(ref SenderIsThis);
                SenderIsThis = (Object)XUomApvvXv;
                AppCoreObjectCreateStatus = AppAppObjectSetFrom(ref SenderIsThis);
            }
            // MinputTldApp XUomCovvXv - Main Process Supervision and Control
            if (XUomCovvXv == null) {
                SenderIsThis = XUomCovvXv;
                AppMcontrollerObjectSetFrom(ref SenderIsThis);
            }
            AppCoreObjectCreatePages();
            AppCoreObjectCreateVerbs();
            return AppCoreObjectCreateStatus;
        }
        public virtual StateIs AppCoreObjectCreatePages() {
            // MinputTldApp XUomPmvvXv - Main or Home Page
            if (XUomPmvvXv == null) {
                // XUomPdvvXv = (MinputTldPageMain)XUomPdvvXv;
                XUomPmvvXv = new Page();
                SenderIsThis = XUomPmvvXv;
                AppCoreObjectCreateStatus = PageMainObjectSetFrom(ref SenderIsThis);
                return AppCoreObjectCreateStatus;
            }
            // MinputTldApp XUomPdvvXv - Details, level 2, options, etc. supplementary Page
            if (XUomPdvvXv == null) {
                // XUomMavvXv or this
                XUomPmvvXv = new Page();
                //// MinputTldApp omvLocalBoard - Main Process performed by XUomApvvXv
                //if (XUomUrvvXv == null & XUomUrvvXvCreateNow) {
                //    // XUomUrvvXv = new System.Windows.Forms.Form(); // XUomUrvvXv = XUomUrvvXv;
                //    //@@@CODE@@@XUomUrvvXv = new MurlHist1Form1();
                //    // XUomUrvvXv.Show();
                //}
            }
            // Store Pages
            // if (OutputFileNameLast != OutputFileLine.Text && XUomPdvvXv != null) {
            // ToDo $$$$NEXT AppCoreObjectCreatePages DbDetailPageSetDefault(ref XUomPmvvXv, ref XUomPdvvXv);
            // }
            return AppCoreObjectCreateStatus;
        }
        public virtual StateIs AppCoreObjectCreateVerbs() {
            // MinputTldApp XUomVevvXv - Main Process performed by XUomApvvXv
            if (XUomVevvXv == null) {
                // XUomVevvXv = (MinputTldThread)XUomVevvXv;
                XUomVevvXv = new Object();
                AppVerbObjectSetFrom(ref XUomVevvXv);
                // AppVerbObjectSet((Object)XUomVevvXv);
            }
            // MinputTldApp XUomVtvvXv - BgWorkerlication being run
            if (XUomVtvvXv == null) {
                XUomVtvvXv = new Object();
                SenderIsThis = XUomVtvvXv;
                AppCoreObjectCreateStatus = AppVerbBgWorkerObjectSetFrom(ref SenderIsThis);
            }
            return AppCoreObjectCreateStatus;
        }
        #endregion
        #region MdmCoreObjectGetSetCheck
        public virtual StateIs AppCoreObjectGetTo(ref Object PassedOb) {
            AppCoreObjectGetStatus = StateIs.Started;
            // App XUomMovvXv;
            AppCoreObjectGet((Mobject) PassedOb);
            return AppCoreObjectGetStatus;
        }
        /// <summary>
        /// Retrieve the MVVC framework core objects and store them in the passed Mobject.
        /// </summary>
        public virtual StateIs AppCoreObjectGet(Mobject PassedOb) {
            AppCoreObjectGetStatus = StateIs.Started;
            // App XUomMovvXv;
            AppCoreObjectGetStatus = AppCoreObjectCheck(PassedOb, XUomApvvXv, @"Mdm.Oss.Mapp.App");
            if (PassedOb != null && XUomMovvXv == null) {
                XUomMovvXv = (Mobject)PassedOb;
                if (XUomMovvXv == null) {
                    XUomMovvXv = this;
                }
                if (PassedOb != this.XUomMovvXv) {
                    AppCoreObjectGetStatus = StateIs.ShouldNotExist;
                }
            }
            // App System.Windows.Application XUomApvvXv
            if (((Mobject)PassedOb).XUomApvvXv == null && System.Windows.Application.Current != null) {
                ((Mobject)PassedOb).XUomApvvXv = System.Windows.Application.Current;
            }
            // App Mapplication XUomMavvXv
            if (((Mobject)PassedOb).XUomMavvXv == null) {
                ((Mobject)PassedOb).XUomMavvXv = AppMappObjectGet();
            }
            // App Mcontroller XUomCovvXv;
            if (((Mobject)PassedOb).XUomCovvXv == null) {
                ((Mobject)PassedOb).XUomCovvXv = AppMcontrollerObjectGet();
            }
            // App PageMain XUomPmvvXv Page1;
            if (((Mobject)PassedOb).XUomPmvvXv == null) {
                ((Mobject)PassedOb).XUomPmvvXv = PageMainObjectGet();
            }
            // App DbDetailPage XUomPdvvXv Page2;
            if (((Mobject)PassedOb).XUomPdvvXv == null) {
                ((Mobject)PassedOb).XUomPdvvXv = DbDetailPageObjectGet();
            }
            // App Verb XUomVevvXv
            if (((Mobject)PassedOb).XUomVevvXv == null) {
                ((Mobject)PassedOb).XUomVevvXv = AppVerbBgWorkerObjectGet();
            }
            // App BgWorker XUomVtvvXv
            if (((Mobject)PassedOb).XUomVtvvXv == null) {
                ((Mobject)PassedOb).XUomVtvvXv = AppVerbObjectGet();
            }
            return AppCoreObjectGetStatus;
        }
        public virtual StateIs AppCoreObjectSetFrom(ref Object PassedOb) {
            AppCoreObjectSetStatus = StateIs.Started;
            if (AppCoreObjectSetStatus == StateIs.Valid) {
                AppCoreObjectSetStatus = AppMappObjectSet(XUomMavvXv = ((Mobject)PassedOb).XUomMavvXv);
                AppCoreObjectSetStatus = AppMcontrollerObjectSetFrom(ref ((Mobject)PassedOb).XUomCovvXv);
                SenderIsThis = ((Mobject)PassedOb).XUomMovvXv;
                AppCoreObjectSetStatus = AppMobjectObjectSetFrom(ref ((Mobject)PassedOb).XUomMovvXv);
                SenderIsThis = ((Mobject)PassedOb).XUomApvvXv;
                AppCoreObjectSetStatus = AppAppObjectSetFrom(ref SenderIsThis);
                SenderIsThis = ((Mobject)PassedOb).XUomPmvvXv;
                AppCoreObjectSetStatus = PageMainObjectSetFrom(ref SenderIsThis);
                SenderIsThis = ((Mobject)PassedOb).XUomPdvvXv;
                AppCoreObjectSetStatus = DbDetailPageObjectSetFrom(ref SenderIsThis);
                SenderIsThis = ((Mobject)PassedOb).XUomVevvXv;
                AppCoreObjectSetStatus = AppVerbObjectSetFrom(ref SenderIsThis);
                SenderIsThis = ((Mobject)PassedOb).XUomVtvvXv;
                AppCoreObjectSetStatus = AppVerbThreadObjectSetFrom(ref SenderIsThis);
                SenderIsThis = ((Mobject)PassedOb).XUomVbvvXv;
                AppCoreObjectSetStatus = AppVerbBgWorkerObjectSetFrom(ref SenderIsThis);
            }
            return AppCoreObjectSetStatus;
        }
        #endregion
        #region MdmMobjectGetSetCheck
        public virtual Mobject AppMobjectObjectGet() {
            AppMobjectObjectGetStatus = StateIs.Valid;
            if (XUomMovvXv == null) {
                return null;
            }
            if (XUomMovvXv == null) {
                XUomMovvXv = this;
            }
            return XUomMovvXv;
        }
        public virtual StateIs AppMobjectObjectSetFrom(ref Mobject ommPassedObject) {
            AppMobjectObjectSetStatus = StateIs.Valid;
            AppMobjectObjectSetStatus = AppCoreObjectCheck((Object)ommPassedObject, (Object)XUomMovvXv, "Mdm1Oss1Mobjectlication1.Mobject");
            if (ommPassedObject == null) {
                AppMcontrollerObjectSetStatus = StateIs.DoesNotExist;
            } else if (XUomMovvXv == null && ommPassedObject != null) {
                XUomMovvXv = ommPassedObject;
            } else if (XUomMovvXv != ommPassedObject) {
                AppMobjectObjectSetStatus = StateIs.DoesNotExist;
                if (XUomMovvXv != ommPassedObject) {
                    AppMobjectObjectSetStatus = StateIs.ShouldNotExist;
                }
            }
            return AppMobjectObjectSetStatus;
        }
        #endregion
        #region MdmAppObjectInstanceAccessors
        public virtual System.Windows.Application AppAppObjectGet() {
            AppAppObjectGetStatus = StateIs.Valid;
            if (XUomApvvXv == null) {
                XUomApvvXv = System.Windows.Application.Current;
            }
            return XUomApvvXv;
            // AppAppObjectGet
        }
        public virtual StateIs AppAppObjectSetFrom(ref Object omaPassedObject) {
            AppAppObjectSetStatus = StateIs.Valid;
            // App XUomMovvXv;
            AppAppObjectSetStatus = AppCoreObjectCheck((Object)omaPassedObject, (Object)XUomApvvXv, "Mdm.Oss.Mapp.Application");
            if (omaPassedObject == null) {
                XUomApvvXv = System.Windows.Application.Current;
                AppMcontrollerObjectSetStatus = StateIs.DoesNotExist;
            } else if (XUomApvvXv == null && omaPassedObject != null) {
                XUomApvvXv = (System.Windows.Application) omaPassedObject;
            } else if (XUomApvvXv != omaPassedObject) {
                AppAppObjectSetStatus = StateIs.DoesNotExist;
                if (XUomApvvXv != omaPassedObject) {
                    AppMobjectObjectSetStatus = StateIs.ShouldNotExist;
                }
            }
            return AppAppObjectSetStatus;
        }
        public virtual Object AppMappObjectGet() {
            AppMappObjectGetStatus = StateIs.Valid;
            return XUomMavvXv;
        }
        public virtual StateIs AppMappObjectSet(Object ommPassedObject) {
            AppMappObjectSetStatus = StateIs.Valid;
            // Mapp XUomMovvXv;
            AppMappObjectSetStatus = AppCoreObjectCheck((Object)ommPassedObject, (Object)XUomMavvXv, "Mdm1Oss1Mapplication1.Mapplication");
            if (ommPassedObject == null) {
                AppMcontrollerObjectSetStatus = StateIs.DoesNotExist;
            } else if (XUomMavvXv == null && ommPassedObject != null) {
                XUomMavvXv = ommPassedObject;
            } else if (XUomMavvXv != ommPassedObject) {
                AppMappObjectSetStatus = StateIs.DoesNotExist;
                if (XUomMavvXv != ommPassedObject) {
                    AppMobjectObjectSetStatus = StateIs.ShouldNotExist;
                }
            }
            return AppMappObjectSetStatus;
        }
        public virtual Object AppMcontrollerObjectGet() {
            AppMcontrollerObjectGetStatus = StateIs.Valid;
            if (XUomCovvXv == null) {
                String stemp99 = XUomPmvvXv.Parent.ToString();
                AppMcontrollerObjectGetStatus = StateIs.DoesNotExist;
                return null;
            }
            return XUomCovvXv;
            // AppMcontrollerObjectGet
        }
        public virtual StateIs AppMcontrollerObjectSetFrom(ref Object omhPassedObject) {
            AppMcontrollerObjectSetStatus = StateIs.Valid;
            AppMcontrollerObjectSetStatus = AppCoreObjectCheck((Object)omhPassedObject, (Object)XUomCovvXv, "Mdm.Oss.Mapp.Mcontroller");
            if (omhPassedObject == null) {
                AppMcontrollerObjectSetStatus = StateIs.DoesNotExist;
            } else if (XUomCovvXv == null && omhPassedObject != null) {
                XUomCovvXv = omhPassedObject;
            } else if (XUomCovvXv != omhPassedObject) {
                AppMcontrollerObjectSetStatus = StateIs.DoesNotExist;
                if (XUomCovvXv != omhPassedObject) {
                    AppMobjectObjectSetStatus = StateIs.ShouldNotExist;
                }
            }
            return AppMcontrollerObjectSetStatus;
        }
        #endregion
        #region MdmPageObjectInstanceAccessors
        // Page Main (1)
        // iDbDetailPageObjectGet
        public virtual Page PageMainObjectGet() {
            iPageMainObjectGetStatus = StateIs.Valid;
            if (XUomPmvvXv == null) {
                iPageMainObjectGetStatus = StateIs.DoesNotExist;
                return null;
            }
            return XUomPmvvXv;
            // iPageMainObjectGet
        }
        public virtual StateIs PageMainObjectSetFrom(ref Object PassedObject) {
            iPageMainObjectSetStatus = StateIs.Valid;
            iPageMainObjectSetStatus = AppCoreObjectCheck((Object)PassedObject, (Object)XUomPmvvXv, "Mdm.Oss.Mapp.MinputTld");
            if (PassedObject == null) {
                iPageMainObjectSetStatus = StateIs.DoesNotExist;
            } else if (XUomPmvvXv == null && PassedObject != null) {
                XUomPmvvXv = (Page)PassedObject;
            } else if (XUomPmvvXv != PassedObject) {
                iPageMainObjectSetStatus = StateIs.DoesNotExist;
                if (XUomPmvvXv != PassedObject) {
                    AppMobjectObjectSetStatus = StateIs.ShouldNotExist;
                }
            }
            return iPageMainObjectSetStatus;
        }
        // iDbDetailPageObjectGet
        public virtual Page DbDetailPageObjectGet() {
            iDbDetailPageObjectGetStatus = StateIs.Valid;
            if (XUomPdvvXv == null) {
                iDbDetailPageObjectGetStatus = StateIs.DoesNotExist;
                return null;
            }
            return XUomPdvvXv;
        }
        public virtual StateIs DbDetailPageObjectSetFrom(ref Object PassedObject) {
            iDbDetailPageObjectSetStatus = StateIs.Valid;
            iDbDetailPageObjectSetStatus = AppCoreObjectCheck((Object)PassedObject, (Object)XUomPmvvXv, "Mdm.Oss.Mapp.MinputTld");
            if (PassedObject == null) {
                iDbDetailPageObjectSetStatus = StateIs.DoesNotExist;
            } else if (XUomPdvvXv == null && PassedObject != null) {
                XUomPdvvXv = (Page)PassedObject;
            } else if (XUomPdvvXv != PassedObject) {
                iDbDetailPageObjectSetStatus = StateIs.DoesNotExist;
                if (XUomPdvvXv != PassedObject) {
                    AppMobjectObjectSetStatus = StateIs.ShouldNotExist;
                }
            }
            return iDbDetailPageObjectSetStatus;
        }
        #endregion
        #region MdmVerbObjectInstanceAccessors
        public virtual Object AppVerbObjectGet() {
            AppVerbObjectGetStatus = StateIs.Valid;
            if (XUomVtvvXv == null) {
                AppVerbObjectGetStatus = StateIs.DoesNotExist;
                return null;
            }
            return XUomVtvvXv;
            // AppVerbObjectGet
        }
        public virtual StateIs AppVerbObjectSetFrom(ref Object omvPassedObject) {
            AppVerbObjectSetStatus = StateIs.Valid;
            AppVerbObjectSetStatus = AppCoreObjectCheck((Object)omvPassedObject, (Object)XUomVevvXv, "Mdm.Oss.Mapp.MinputTld");
            if (omvPassedObject == null) {
                AppVerbObjectSetStatus = StateIs.DoesNotExist;
            } else if (XUomVevvXv == null && omvPassedObject != null) {
                XUomVevvXv = omvPassedObject;
            } else if (XUomVevvXv != omvPassedObject) {
                AppVerbObjectSetStatus = StateIs.DoesNotExist;
                if (XUomVevvXv != omvPassedObject) {
                    AppMobjectObjectSetStatus = StateIs.ShouldNotExist;
                }
            }
            return AppVerbObjectSetStatus;
        }
        public virtual Object AppVerbThreadObjectGet() {
            AppVerbThreadObjectGetStatus = StateIs.Valid;
            if (XUomVtvvXv == null) {
                AppVerbThreadObjectGetStatus = StateIs.DoesNotExist;
                return null;
            }
            return XUomVtvvXv;
            // AppVerbThreadObjectGet
        }
        public virtual StateIs AppVerbThreadObjectSetFrom(ref Object omvPassedObject) {
            AppVerbThreadObjectSetStatus = StateIs.Valid;
            AppVerbThreadObjectSetStatus = AppCoreObjectCheck((Object)omvPassedObject, (Object)XUomVtvvXv, "Mdm.Oss.Mapp.MinputTld");
            if (omvPassedObject == null) {
                AppVerbThreadObjectSetStatus = StateIs.DoesNotExist;
            } else if (XUomVtvvXv == null && omvPassedObject != null) {
                XUomVtvvXv = omvPassedObject;
            } else if (XUomVtvvXv != omvPassedObject) {
                AppVerbThreadObjectSetStatus = StateIs.DoesNotExist;
                if (XUomVtvvXv != omvPassedObject) {
                    AppMobjectObjectSetStatus = StateIs.ShouldNotExist;
                }
            }
            return AppVerbThreadObjectSetStatus;
        }
        public virtual Object AppVerbBgWorkerObjectGet() {
            AppVerbBgWorkerObjectGetStatus = StateIs.Valid;
            if (XUomVbvvXv == null) {
                // XUomVbvvXv = this;
                iPageMainObjectGetStatus = StateIs.DoesNotExist;
                return null;
            }
            return XUomVbvvXv;
        }
        public virtual StateIs AppVerbBgWorkerObjectSetFrom(ref Object PassedObject) {
            AppVerbBgWorkerObjectSetStatus = StateIs.Valid;
            // MbgWorker XUomMovvXv; 1MMbgWor
            // AppVerbBgWorkerObjectSet = AppCoreObjectCheck((Object)PassedObject, (Object)XUomVtvvXv, "Mdm1Oss1MbgWorkerlication1.MbgWorkerlication");
            if (PassedObject == null) {
                AppMcontrollerObjectSetStatus = StateIs.DoesNotExist;
            } else if (XUomVtvvXv == null && PassedObject != null) {
                XUomVtvvXv = (BackgroundWorker)PassedObject;
            } else if (XUomVtvvXv != PassedObject) {
                AppVerbBgWorkerObjectSetStatus = StateIs.DoesNotExist;
                if (XUomVtvvXv != PassedObject) {
                    AppMobjectObjectSetStatus = StateIs.ShouldNotExist;
                }
            }
            return AppVerbBgWorkerObjectSetStatus;
        }
        #endregion
        #region MdmAppIoObjectGet
        public virtual Object AppIoObjectGet(System.Windows.Application PassedA) {
            AppIoObjectGetStatus = StateIs.Started;

            return AppIoObjectGetStatus;
        }
        #endregion
        #endregion
        #region MobjectReset
        public virtual StateIs MobjectReset() {
            iMobjectResetStatus = StateIs.Started;
            //

            // iMobjectReset
            return iMobjectResetStatus;
        }

        #endregion
        #endregion
        #region EventHandling
        /// <summary>
        /// <para> . </para>
        /// <para> A delegate for hooking up change notifications.</para>
        /// <para> Don't know if this is used anywhere (i.e. no events 
        /// yet and property change not used.) </para>
        /// </summary>
        public delegate void ChangedEventHandler(Object SenderPassed, EventArgs e);
        /*
     }
         * 
    namespace Mdm.Test.Events 
    {
    using Mdm.Oss.Mobj;
        */

        /*
        class EventListener 
        {
          public MdmList1 MdmTestList;

          public EventListener(MdmList1 lMapplicationList) 
          {
             MdmTestList = lMapplicationList;
             // Add "ListChanged" to the Changed event on "MdmTestList".
             MdmTestList.Changed += new ChangedEventHandler(ListChanged);
          }

          // This will be called whenever the lMapplicationList changes.
          public void ListChanged(Object Sender, EventArgs e) 
          {
             System.Diagnostics.Debug.WriteLine("This is called when the event fires.");
          }

          public void Detach() 
          {
             // Detach the event and delete the lMapplicationList
             MdmTestList.Changed -= new ChangedEventHandler(ListChanged);
             MdmTestList = null;
          }
        }
        */
        #endregion
        #region OtherTestCode
        /* 
      class cTest 
      {
          // cTest the MdmList1 class.
          public static void mMain() 
          {
              // Create a new lMapplicationList.
              MdmList1 lMapplicationList = new MdmList1();

              // Create a class that listens to the lMapplicationList's change event.
              EventListener listener = new EventListener(lMapplicationList);

              // Add and remove items from the lMapplicationList.
              lMapplicationList.Add("item 1");
              lMapplicationList.Clear();
              listener.Detach();
          }
      }
    }

        /// <summary>
// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
*/
        #endregion
        #region Notes
        /// class Mdm1 Mobjectl CVS properties
        /// Class Mdm1 Project Control CVS properties
        /// sAuthor Company
        /// sAuthor
        /// Project properties
        /// Task properties
        /// Task Step properties
        /// Solution properties
        /// Namespace properties
        /// Assembly properties
        /// System properties
        /// Process properties
        /// Status Message properties
        /// Class properties
        /// Method properties
        /// Property properties
        /// Attribute properties
        /// Parameter properties
        /// Command properties
        /// Console properties
        /// Run properties
        /// AutoRun properties
        /// Input properties
        /// Output properties
        /// Class external properties
        /// Class internal properties
        /// Class properties

        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        #region PivotTableHandling
        /// <summary>
        /// <para> Mdm Pivot Table definition.</para>
        /// </summary>
        public class MdmPivot1
        {
            public String[] saMPivot1;
            public int iMPivot1Length;
            public String[] saMPivot2;
            public int iMPivot2Length;
            public int[] iaMPivot3;
            public int iMPivot3Length; // ToDo non-unique polish
            // ToDo Pivot Table Handling

            static void DoPivotStrFill(object[] oaPassedArray, int iPassedIndex, int iPassedCount, Object ooPassedValue)
            {
                for (int i = iPassedIndex; i < iPassedIndex + iPassedCount; i++)
                {
                    oaPassedArray[i] = ooPassedValue;
                };
            }

            static void BuildPivotStr()
            {
                int iMPivot1Length = 100;
                String[] saMPivot1 = new string[iMPivot1Length];
                int iMPivot2Length = 100;
                String[] saMPivot2 = new string[iMPivot2Length];
                DoPivotStrFill(saMPivot1, 0, iMPivot1Length, "Undefined");
                DoPivotStrFill(saMPivot2, 0, iMPivot2Length, null);
            }

            static void BuildPivotStr(int iPassedLength)
            {
                int iMPivot1Length = iPassedLength;
                String[] saMPivot1 = new string[iMPivot1Length];
                int iMPivot2Length = iPassedLength;
                String[] saMPivot2 = new string[iMPivot2Length];
            }

            static void DoPivotIntFill(int[] iaPassedArray, int iPassedIndex, int iPassedCount, int iPassedValue)
            {
                for (int i = iPassedIndex; i < iPassedIndex + iPassedCount; i++)
                {
                    iaPassedArray[i] = iPassedValue;
                };
            }

            static void BuildPivotInt()
            {
                // int[] iaMPivot1 = new int[iMPivot1Length];
                // int[] iaMPivot1 = new int[iMPivot1Length];
                int iMPivot1Length = 100;
                int[] MintPivot1 = new int[iMPivot1Length];
                DoPivotIntFill(MintPivot1, 0, iMPivot1Length, 0);
            }
            static void BuildPivotInt(int iPassedLength)
            {
                int iMPivot1Length = iPassedLength;
                int[] iaMintPivot1 = new int[iMPivot1Length];
                DoPivotIntFill(iaMintPivot1, 0, iMPivot1Length, 0);
            }
        }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
    }
}