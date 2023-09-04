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
//@@@CODE@@@using Mdm.Oss.ClipUtil;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Decl;
using Mdm.Pick;
using Mdm.Oss.Mobj;
//@@@CODE@@@using Mdm.Oss.Support;
using Mdm.Oss.Sys;
//@@@CODE@@@using Mdm.Oss.UrlUtil.Hist;
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
    public class Mobject : DefStdBaseRunFileConsole {

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
        public Object Sender;
        public Object SenderIsThis;
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
        internal long iMobject;
        internal long iMobjectPassedApp;
        internal long iMobjectStartApp;
        internal long iMobjectReset;

        internal long iAppSetObjectByType;
        #endregion
        #region ClaseInternalCoreObjectResults
        public long iAppAppObjectGet;
        public long iAppAppObjectSet;
        public long iAppMappObjectGet;
        public long iAppMappObjectSet;
        public long iAppMcontrollerObjectGet;
        public long iAppMcontrollerObjectSet;
        public long iAppMapplicationObjectGet;
        public long iAppMapplicationObjectSet;
        public long iAppMobjectObjectGet;
        public long iAppMobjectObjectSet;

        public long iMapplication;
        public long iMapplicationPassedApp;
        public long iMapplicationStartApp;

        public long iPageMainObjectGet;
        public long iPageMainObjectSet;
        public long iPageMainSetCoreObjects;
        public long iPageMainLoaded;
        public long iPageMainSetDefault;
        public long iDbDetailPageObjectGet;
        public long iDbDetailPageObjectSet;
        public long iDbDetailPageSetCoreObjects;
        public long iDbDetailPageLoaded;
        public long iDbDetailPageSetDefault;

        public long iAppVerbObjectGet;
        public long iAppVerbObjectSet;
        public long iAppVerbThreadObjectGet;
        public long iAppVerbThreadObjectSet;
        public long iAppVerbBgWorkerObjectGet;
        public long iAppVerbBgWorkerObjectSet;

        protected long iAppCoreObjectCreate;
        protected long iAppCoreObjectGetFromApp;
        protected long iAppCoreObjectSetInMapplication;
        protected long iAppCoreObjectGetFromMavvXvpplication;
        protected long iAppObjectGet;
        protected long iAppObjectSet;
        #endregion
        #endregion
        #region Class Mdm1 Oss1 properties
        //
        #endregion
        #region MobjectConstructor
        public Mobject()
            : base() {
            iMobject = (long)StateIs.Started;
            MobjectInitialize();
        }

        public Mobject(long ClassFeaturesPassed)
            : base(ClassFeaturesPassed) {
            iMobject = (long)StateIs.Started;
            MobjectInitialize();
        }

        public Mobject(long ClassFeaturesPassed, ref System.Windows.Application PassedA)
            : base(ClassFeaturesPassed) {
            iMobjectPassedApp = (long)StateIs.Started;
            if (PassedA != null) {
                if (XUomApvvXv != null) {
                    XUomApvvXv = System.Windows.Application.Current;
                }
                XUomApvvXv = PassedA;
            }
            MobjectInitialize();
            // iMobjectPassedApp
        }

        public Mobject(long ClassFeaturesPassed, ref System.Windows.Application PassedA, Page PassedP)
            : base(ClassFeaturesPassed) {
            iMobjectPassedApp = (long)StateIs.Started;
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
        public virtual long MobjectInitialize() {
            iMobjectStartApp = (long)StateIs.Started;
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
            return iMobjectPassedApp;
            // iMobjectStartApp
        }

        #region Mobject Sobject replacement introspection object handling...
        // todo MobjectCreate Tested but not implemented
        public virtual long MobjectCreate(String NamePassed, Type TypePassed, Object ObjectPassed) {
            iAppSetObjectByType = (long)StateIs.Started;
            
            // Type type = typeof(int);
            // iAppSetObjectByType = MobjectGet(ObjectPassed.GetType().ToString(), ref ObjectPassed);
            // Create an instance of a type.
            // Type t = SobjectTypes[NamePassed];
            if (TypePassed != null) {
                Object[] args = new Object[] { 8 };
                // args[0] = Xuom;
                args[0] = XUomCovvXv;
                Console.WriteLine("The value of x before the constructor is called is {0}.", args[0]);
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
                iAppSetObjectByType = (long)StateIs.DoesExist;
            } else {
                iAppSetObjectByType = (long)StateIs.DoesNotExist;
            }

            return iAppSetObjectByType;        
        }

        // todo MobjectCreate Tested but not implemented
        public virtual long MobjectSet(Object PassedOb) {
            iAppSetObjectByType = (long)StateIs.Started;
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
            return iAppSetObjectByType;
        }
        // (long)StateIs.DoesExist;
        // (long)StateIs.DoesNotExist;

        // todo MobjectCreate Tested but not implemented
        public virtual long MobjectGet(ref Object ObjectPassed) {
            iAppSetObjectByType = (long)StateIs.Started;
            iAppSetObjectByType = MobjectGet(ObjectPassed.GetType().ToString(), ref ObjectPassed);
            return iAppSetObjectByType;
        }

        public virtual long MobjectGet(Type TypePassed, ref Object ObjectPassed) {
            iAppSetObjectByType = (long)StateIs.Started;
            iAppSetObjectByType = MobjectGet(TypePassed.ToString(), ref ObjectPassed);
            return iAppSetObjectByType;
        }

        public virtual long MobjectGet(String NamePassed, ref Object ObjectPassed) {
            iAppSetObjectByType = (long)StateIs.Started;
            // TODO Sobject evalutation and changes to XU object storage using dict
            if (!XUom.Items.ContainsKey(NamePassed)) {
                iAppSetObjectByType = (long)StateIs.DoesExist;
                ObjectPassed = XUom.Items[NamePassed];
            } else {
                iAppSetObjectByType = (long)StateIs.DoesNotExist;
                ObjectPassed = null;
            }
            return iAppSetObjectByType;
        }

        /////////////////////////////////////////////////////////////
        #endregion

        public virtual long AppSetObjectByType(ref Object PassedOb) {
            iAppSetObjectByType = (long)StateIs.Started;
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
            return iAppSetObjectByType;
        }
        #endregion
        #region MapplicationObjects
        #region MdmAppCoreObjectCreation
        public virtual long AppCoreObjectCreate(ref Page PassedMinputTldPageMain) {
            XUomPmvvXv = (Page)PassedMinputTldPageMain;
            AppCoreObjectCreate();
            return 0;
        }
        public virtual long AppCoreObjectCreate(ref System.Windows.Application PassedApplication) {
            XUomApvvXv = (System.Windows.Application)PassedApplication;
            AppCoreObjectCreate();
            return 0;
        }
        public virtual long AppCoreObjectCreate(ref Mobject PassedMobject) {
            XUomMovvXv = PassedMobject;
            if (PassedMobject.XUomPmvvXv != null) { XUomPmvvXv = PassedMobject.XUomPmvvXv; }
            if (PassedMobject.XUomApvvXv != null) { XUomApvvXv = PassedMobject.XUomApvvXv; }
            AppCoreObjectCreate();
            return 0;
        }
        public virtual long AppCoreObjectSetAll(ref Mobject PassedOb) {
            iAppMobjectObjectSet = (long)StateIs.Valid;
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
            iAppMobjectObjectSet = (long)StateIs.Finished;
            return iAppMobjectObjectSet;
        }
        //
        public virtual long AppCoreObjectCreate() {
            iAppCoreObjectCreate = (long)StateIs.Started;
            // Start Up Ui Capable Controller Unit
            // TODO AppCoreObjectCreate do not use this code at this time
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
            iAppCoreObjectCreate = AppSetObjectByType(ref Sender);
            if (XUomMavvXv == null) {
                XUomMavvXv = new Object();
                iAppCoreObjectCreate = AppMappObjectSet(XUomMavvXv);
                SenderIsThis = XUomPmvvXv;
                iAppCoreObjectCreate = PageMainObjectSet(ref SenderIsThis);
                SenderIsThis = (Object)XUomApvvXv;
                iAppCoreObjectCreate = AppAppObjectSet(ref SenderIsThis);
            }
            // MinputTldApp XUomCovvXv - Main Process Supervision and Control
            if (XUomCovvXv == null) {
                SenderIsThis = XUomCovvXv;
                AppMcontrollerObjectSet(ref SenderIsThis);
            }
            AppCoreObjectCreatePages();
            AppCoreObjectCreateVerbs();
            return iAppCoreObjectCreate;
        }
        public virtual long AppCoreObjectCreatePages() {
            // MinputTldApp XUomPmvvXv - Main or Home Page
            if (XUomPmvvXv == null) {
                // XUomPdvvXv = (MinputTldPageMain)XUomPdvvXv;
                XUomPmvvXv = new Page();
                SenderIsThis = XUomPmvvXv;
                iAppCoreObjectCreate = PageMainObjectSet(ref SenderIsThis);
                return iAppCoreObjectCreate;
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
            // TODO $$$$NEXT AppCoreObjectCreatePages DbDetailPageSetDefault(ref XUomPmvvXv, ref XUomPdvvXv);
            // }
            return iAppCoreObjectCreate;
        }
        public virtual long AppCoreObjectCreateVerbs() {
            // MinputTldApp XUomVevvXv - Main Process performed by XUomApvvXv
            if (XUomVevvXv == null) {
                // XUomVevvXv = (MinputTldThread)XUomVevvXv;
                XUomVevvXv = new Object();
                AppVerbObjectSet(ref XUomVevvXv);
                // AppVerbObjectSet((Object)XUomVevvXv);
            }
            // MinputTldApp XUomVtvvXv - BgWorkerlication being run
            if (XUomVtvvXv == null) {
                XUomVtvvXv = new Object();
                SenderIsThis = XUomVtvvXv;
                iAppCoreObjectCreate = AppVerbBgWorkerObjectSet(ref SenderIsThis);
            }
            return iAppCoreObjectCreate;
        }
        #endregion
        #region MdmCoreObjectGetSetCheck
        public virtual long AppCoreObjectGet(ref Object PassedOb) {
            iAppCoreObjectGet = (long)StateIs.Started;
            // App XUomMovvXv;
            AppCoreObjectGet((Mobject) PassedOb);
            return iAppCoreObjectGet;
        }
        /// <summary>
        /// Retrieve the MVVC framework core objects and store them in the passed Mobject.
        /// </summary>
        public virtual long AppCoreObjectGet(Mobject PassedOb) {
            iAppCoreObjectGet = (long)StateIs.Started;
            // App XUomMovvXv;
            iAppCoreObjectGet = AppCoreObjectCheck(PassedOb, XUomApvvXv, @"Mdm.Oss.Mapp.App");
            if (PassedOb != null && XUomMovvXv == null) {
                XUomMovvXv = (Mobject)PassedOb;
                if (XUomMovvXv == null) {
                    XUomMovvXv = this;
                }
                if (PassedOb != this.XUomMovvXv) {
                    iAppCoreObjectGet = (long)StateIs.ShouldNotExist;
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
            return iAppCoreObjectGet;
        }
        public virtual long AppCoreObjectSet(ref Object PassedOb) {
            iAppCoreObjectSet = (long)StateIs.Started;
            if (iAppCoreObjectSet == (long)StateIs.Valid) {
                iAppCoreObjectSet = AppMappObjectSet(XUomMavvXv = ((Mobject)PassedOb).XUomMavvXv);
                iAppCoreObjectSet = AppMcontrollerObjectSet(ref ((Mobject)PassedOb).XUomCovvXv);
                SenderIsThis = ((Mobject)PassedOb).XUomMovvXv;
                iAppCoreObjectSet = AppMobjectObjectSet(ref ((Mobject)PassedOb).XUomMovvXv);
                SenderIsThis = ((Mobject)PassedOb).XUomApvvXv;
                iAppCoreObjectSet = AppAppObjectSet(ref SenderIsThis);
                SenderIsThis = ((Mobject)PassedOb).XUomPmvvXv;
                iAppCoreObjectSet = PageMainObjectSet(ref SenderIsThis);
                SenderIsThis = ((Mobject)PassedOb).XUomPdvvXv;
                iAppCoreObjectSet = DbDetailPageObjectSet(ref SenderIsThis);
                SenderIsThis = ((Mobject)PassedOb).XUomVevvXv;
                iAppCoreObjectSet = AppVerbObjectSet(ref SenderIsThis);
                SenderIsThis = ((Mobject)PassedOb).XUomVtvvXv;
                iAppCoreObjectSet = AppVerbThreadObjectSet(ref SenderIsThis);
                SenderIsThis = ((Mobject)PassedOb).XUomVbvvXv;
                iAppCoreObjectSet = AppVerbBgWorkerObjectSet(ref SenderIsThis);
            }
            return iAppCoreObjectSet;
        }
        #endregion
        #region MdmMobjectGetSetCheck
        public virtual Mobject AppMobjectObjectGet() {
            iAppMobjectObjectGet = (long)StateIs.Valid;
            if (XUomMovvXv == null) {
                return null;
            }
            if (XUomMovvXv == null) {
                XUomMovvXv = this;
            }
            return XUomMovvXv;
        }
        public virtual long AppMobjectObjectSet(ref Mobject ommPassedObject) {
            iAppMobjectObjectSet = (long)StateIs.Valid;
            iAppMobjectObjectSet = AppCoreObjectCheck((Object)ommPassedObject, (Object)XUomMovvXv, "Mdm1Oss1Mobjectlication1.Mobject");
            if (ommPassedObject == null) {
                iAppMcontrollerObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomMovvXv == null && ommPassedObject != null) {
                XUomMovvXv = ommPassedObject;
            } else if (XUomMovvXv != ommPassedObject) {
                iAppMobjectObjectSet = (long)StateIs.DoesNotExist;
                if (XUomMovvXv != ommPassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iAppMobjectObjectSet;
        }
        #endregion
        #region MdmAppObjectInstanceAccessors
        public virtual System.Windows.Application AppAppObjectGet() {
            iAppAppObjectGet = (long)StateIs.Valid;
            if (XUomApvvXv == null) {
                XUomApvvXv = System.Windows.Application.Current;
            }
            return XUomApvvXv;
            // iAppAppObjectGet
        }
        public virtual long AppAppObjectSet(ref Object omaPassedObject) {
            iAppAppObjectSet = (long)StateIs.Valid;
            // App XUomMovvXv;
            iAppAppObjectSet = AppCoreObjectCheck((Object)omaPassedObject, (Object)XUomApvvXv, "Mdm.Oss.Mapp.Application");
            if (omaPassedObject == null) {
                XUomApvvXv = System.Windows.Application.Current;
                iAppMcontrollerObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomApvvXv == null && omaPassedObject != null) {
                XUomApvvXv = (System.Windows.Application) omaPassedObject;
            } else if (XUomApvvXv != omaPassedObject) {
                iAppAppObjectSet = (long)StateIs.DoesNotExist;
                if (XUomApvvXv != omaPassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iAppAppObjectSet;
        }
        public virtual Object AppMappObjectGet() {
            iAppMappObjectGet = (long)StateIs.Valid;
            return XUomMavvXv;
        }
        public virtual long AppMappObjectSet(Object ommPassedObject) {
            iAppMappObjectSet = (long)StateIs.Valid;
            // Mapp XUomMovvXv;
            iAppMappObjectSet = AppCoreObjectCheck((Object)ommPassedObject, (Object)XUomMavvXv, "Mdm1Oss1Mapplication1.Mapplication");
            if (ommPassedObject == null) {
                iAppMcontrollerObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomMavvXv == null && ommPassedObject != null) {
                XUomMavvXv = ommPassedObject;
            } else if (XUomMavvXv != ommPassedObject) {
                iAppMappObjectSet = (long)StateIs.DoesNotExist;
                if (XUomMavvXv != ommPassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iAppMappObjectSet;
        }
        public virtual Object AppMcontrollerObjectGet() {
            iAppMcontrollerObjectGet = (long)StateIs.Valid;
            if (XUomCovvXv == null) {
                String stemp99 = XUomPmvvXv.Parent.ToString();
                iAppMcontrollerObjectGet = (long)StateIs.DoesNotExist;
                return null;
            }
            return XUomCovvXv;
            // iAppMcontrollerObjectGet
        }
        public virtual long AppMcontrollerObjectSet(ref Object omhPassedObject) {
            iAppMcontrollerObjectSet = (long)StateIs.Valid;
            iAppMcontrollerObjectSet = AppCoreObjectCheck((Object)omhPassedObject, (Object)XUomCovvXv, "Mdm.Oss.Mapp.Mcontroller");
            if (omhPassedObject == null) {
                iAppMcontrollerObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomCovvXv == null && omhPassedObject != null) {
                XUomCovvXv = omhPassedObject;
            } else if (XUomCovvXv != omhPassedObject) {
                iAppMcontrollerObjectSet = (long)StateIs.DoesNotExist;
                if (XUomCovvXv != omhPassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iAppMcontrollerObjectSet;
        }
        #endregion
        #region MdmPageObjectInstanceAccessors
        // Page Main (1)
        // iDbDetailPageObjectGet
        public virtual Page PageMainObjectGet() {
            iPageMainObjectGet = (long)StateIs.Valid;
            if (XUomPmvvXv == null) {
                iPageMainObjectGet = (long)StateIs.DoesNotExist;
                return null;
            }
            return XUomPmvvXv;
            // iPageMainObjectGet
        }
        public virtual long PageMainObjectSet(ref Object PassedObject) {
            iPageMainObjectSet = (long)StateIs.Valid;
            iPageMainObjectSet = AppCoreObjectCheck((Object)PassedObject, (Object)XUomPmvvXv, "Mdm.Oss.Mapp.MinputTld");
            if (PassedObject == null) {
                iPageMainObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomPmvvXv == null && PassedObject != null) {
                XUomPmvvXv = (Page)PassedObject;
            } else if (XUomPmvvXv != PassedObject) {
                iPageMainObjectSet = (long)StateIs.DoesNotExist;
                if (XUomPmvvXv != PassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iPageMainObjectSet;
        }
        // iDbDetailPageObjectGet
        public virtual Page DbDetailPageObjectGet() {
            iDbDetailPageObjectGet = (long)StateIs.Valid;
            if (XUomPdvvXv == null) {
                iDbDetailPageObjectGet = (long)StateIs.DoesNotExist;
                return null;
            }
            return XUomPdvvXv;
        }
        public virtual long DbDetailPageObjectSet(ref Object PassedObject) {
            iDbDetailPageObjectSet = (long)StateIs.Valid;
            iDbDetailPageObjectSet = AppCoreObjectCheck((Object)PassedObject, (Object)XUomPmvvXv, "Mdm.Oss.Mapp.MinputTld");
            if (PassedObject == null) {
                iDbDetailPageObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomPdvvXv == null && PassedObject != null) {
                XUomPdvvXv = (Page)PassedObject;
            } else if (XUomPdvvXv != PassedObject) {
                iDbDetailPageObjectSet = (long)StateIs.DoesNotExist;
                if (XUomPdvvXv != PassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iDbDetailPageObjectSet;
        }
        #endregion
        #region MdmVerbObjectInstanceAccessors
        public virtual Object AppVerbObjectGet() {
            iAppVerbObjectGet = (long)StateIs.Valid;
            if (XUomVtvvXv == null) {
                iAppVerbObjectGet = (long)StateIs.DoesNotExist;
                return null;
            }
            return XUomVtvvXv;
            // iAppVerbObjectGet
        }
        public virtual long AppVerbObjectSet(ref Object omvPassedObject) {
            iAppVerbObjectSet = (long)StateIs.Valid;
            iAppVerbObjectSet = AppCoreObjectCheck((Object)omvPassedObject, (Object)XUomVevvXv, "Mdm.Oss.Mapp.MinputTld");
            if (omvPassedObject == null) {
                iAppVerbObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomVevvXv == null && omvPassedObject != null) {
                XUomVevvXv = omvPassedObject;
            } else if (XUomVevvXv != omvPassedObject) {
                iAppVerbObjectSet = (long)StateIs.DoesNotExist;
                if (XUomVevvXv != omvPassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iAppVerbObjectSet;
        }
        public virtual Object AppVerbThreadObjectGet() {
            iAppVerbThreadObjectGet = (long)StateIs.Valid;
            if (XUomVtvvXv == null) {
                iAppVerbThreadObjectGet = (long)StateIs.DoesNotExist;
                return null;
            }
            return XUomVtvvXv;
            // iAppVerbThreadObjectGet
        }
        public virtual long AppVerbThreadObjectSet(ref Object omvPassedObject) {
            iAppVerbThreadObjectSet = (long)StateIs.Valid;
            iAppVerbThreadObjectSet = AppCoreObjectCheck((Object)omvPassedObject, (Object)XUomVtvvXv, "Mdm.Oss.Mapp.MinputTld");
            if (omvPassedObject == null) {
                iAppVerbThreadObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomVtvvXv == null && omvPassedObject != null) {
                XUomVtvvXv = omvPassedObject;
            } else if (XUomVtvvXv != omvPassedObject) {
                iAppVerbThreadObjectSet = (long)StateIs.DoesNotExist;
                if (XUomVtvvXv != omvPassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iAppVerbThreadObjectSet;
        }
        public virtual Object AppVerbBgWorkerObjectGet() {
            iAppVerbBgWorkerObjectGet = (long)StateIs.Valid;
            if (XUomVbvvXv == null) {
                // XUomVbvvXv = this;
                iPageMainObjectGet = (long)StateIs.DoesNotExist;
                return null;
            }
            return XUomVbvvXv;
        }
        public virtual long AppVerbBgWorkerObjectSet(ref Object PassedObject) {
            iAppVerbBgWorkerObjectSet = (long)StateIs.Valid;
            // MbgWorker XUomMovvXv; 1MMbgWor
            // iAppVerbBgWorkerObjectSet = AppCoreObjectCheck((Object)PassedObject, (Object)XUomVtvvXv, "Mdm1Oss1MbgWorkerlication1.MbgWorkerlication");
            if (PassedObject == null) {
                iAppMcontrollerObjectSet = (long)StateIs.DoesNotExist;
            } else if (XUomVtvvXv == null && PassedObject != null) {
                XUomVtvvXv = (BackgroundWorker)PassedObject;
            } else if (XUomVtvvXv != PassedObject) {
                iAppVerbBgWorkerObjectSet = (long)StateIs.DoesNotExist;
                if (XUomVtvvXv != PassedObject) {
                    iAppMobjectObjectSet = (long)StateIs.ShouldNotExist;
                }
            }
            return iAppVerbBgWorkerObjectSet;
        }
        #endregion
        #region MdmAppIoObjectGet
        public virtual Object AppIoObjectGet(System.Windows.Application PassedA) {
            iAppIoObjectGet = (long)StateIs.Started;

            return iAppIoObjectGet;
        }
        #endregion
        #endregion
        #region MobjectReset
        public virtual long MobjectReset() {
            iMobjectReset = (long)StateIs.Started;
            //

            // iMobjectReset
            return iMobjectReset;
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
            public int iMPivot3Length; // TODO non-unique polish
            // TODO Pivot Table Handling

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