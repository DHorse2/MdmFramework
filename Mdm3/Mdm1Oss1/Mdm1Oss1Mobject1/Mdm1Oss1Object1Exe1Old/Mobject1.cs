//Top//

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
using System.Windows; // Page // App
using System.Windows.Controls; // Page
using System.Windows.Data; // Page
using System.Windows.Documents; // Page
using System.Windows.Input; // Page
using System.Windows.Media; // Page
using System.Windows.Media.Imaging; // Page
using System.Windows.Navigation; // Page // App
using System.Windows.Shapes; // Page
//
using Mdm.Oss.ClipboardUtil;
using Mdm.Oss.CodeUtil;
using Mdm.Oss.Support;
using Mdm.Oss.Mapp;

namespace Mdm.Oss.Mobj {
    public class 
        Mobject : Mapplication {

        public string sMessageToPage = "";
        #region StandardIdentifierPrefix
        // Class Standard Properties
        // oo   = Object - Object Type not specified
        // omo  = Object - Mdm  - Mobject
        // omc  - Object - Mdm  - Mdm Application
        // omc  - Object - Mdm  - TODO
        // oe   - Object - Exception
        // of   - Object - File
        // ofb  - Object - File - Buffer
        // ofs  - Object - File - File Stream
        // ofsr - Object - File - File Stream Reader
        // ofdc - Object - File - Databse Command
        // ofd  - Object - File - Database Connection
        // ofe  - Object - File - Exception
        // oh   - Object - HashTable
        // os   - Object - Structure
        #endregion
        #region StandardIdentifierSuffix
        #endregion
        #region MdmStandardRunControlDeclarations   seee Mapplication
        /*  // <Section Id = "MdmStandardRunControl">
        // <Section Vs="MdmStdRunVs0_8_5">
        // <Area Id = "Console_CommandActions>
        public string sCommandLineRequest;
        public int iCommandLineRequest = 0;
        // <Area Id = "PrimaryActions">
        public string sFileActionRequest;
        public string sPickFileActionRequest;
        // <Area Id = "Console_">
        public string sConsole_Command;
        public string sConsole_Output;
        public string sConsole_OutputLog = "";
        // <Area Id = "Errors">
        public int iRunErrorNumber = 0;
        public int iRunShellErrorNumber = 0;
        public string sLocalErrorMessage = "";
        // <Area Id = "RunStatusControlItFlags">
        public bool bRunAbort = false;
        public bool bRunReloop = false;
        public bool bRunFirst = true;
        // <Area Id = "InterationStatusControlItFlags">
        public bool bInterationAbort = false;
        public bool bInterationReloop = false;
        public bool bInterationFirst = true;
        public int iInterationLoopCounter = 0;
        // <Area Id = "MethodInterationStatusControlItFlags">
        public bool bMethodInterationAbort = false;
        public bool bMethodInterationReloop = false;
        public bool bMethodInterationFirst = true;
        public int iMethodInterationLoopCounter = 0;
        // <Area Id = "iRunStatus">
        public int iRunStatus = 99999;
        */
        #endregion
        #region Mdm.Oss.Mobj - StandardObjectClass
        /// Mdm1 Oss1 Mobject Standard Object Class 
        /// Mdm.Oss.Mobj - Mobject
        /// <MdmSrtRels Vs="MdmSrtRels0_8_5">
        #region Class Local Status
        /* /
        internal int iApp_Core_ObjectGet;
        internal int iApplicationIoObjectGet;
        internal int iApplicationAppObjectGet;
        internal int iApplicationAppObjectSet;
        //
        internal int iApplicationHandlerObjectGet;
        internal int iApplicationHandlerObjectSet;
        internal int iApplicationMobjectObjectGet;
        internal int iApplicationMobjectObjectSet;
        internal int iApp_Page_ObjectGet;
        internal int iApp_Page_ObjectSet;
        internal int iApplicationVerbObjectGet;
        internal int iApplicationVerbObjectSet;
        / */
        internal int iMobject;
        internal int iMobjectPassedApp;
        internal int iMobjectStartApp;
        internal int iMobjectReset;
        //
        #endregion 
        #region Package Object Declarations         see Mapplication
        /* // <Section Role="Declarations">
        // <Section Id = "MdmStandardObject">
        // <Section Vs="MdmStdObjVs0_8_8">
        // MdmStandardObject MdmStdObjVs0_8_8
        // <Area Id = "MdmImportTld">
        // <Area Id = "Mapplication">
        // <Area Id = "omAplication">
        public Application omAp;
        // <Area Id = "omHControl">
        public object omHa;
        // <Area Id = "omOb Mapplication">
        public Mapplication omOb;
        // <Area Id = "omoLocalMop Mobject">
        public Mobject omOb;
        // <Area Id = "omP">
        public Page omPa;
        // <Area Id = "MdmImportTld">
        public object omVe;
        // <Area Id = "Console_Object">
        #endregion
        */
        #endregion
        #region MdmStandardIoObject                 see Mapplication
        /* // <Area Id = "Console_Object">
        public TextWriter ocotConsole_Writer;
        // public TextWriter ocotStandardOutput;
        public TextReader ocitConsole_Reader;
        public StreamWriter ocosConsole_Writer;
        // public StreamWriter ocoStandardOutput;
        public StreamReader ocisConsole_Reader;
        // public StreamWriter ocetErrorWriter;
        public IOException eIoe;
        public TextWriter ocetErrorWriter;
        //
        */
        #endregion
        //
        #endregion
        #region Class Mdm1 Oss1 properties
        //
        #endregion
        #region MobjectConstructor
        public Mobject() : base() {
            iMobject = (int) MethodStateIs.Start;
            sMdm_Process_Title = "";
            // if (omOb == null) {
                // omOb = new Mobject();
            // }
            MobjectStartApp();
        }

        public Mobject(Application omPassedA)
            : base() {
            iMobjectPassedApp = (int) MethodStateIs.Start;
            if (omPassedA != null) {
                if (omAp != null) {
                    omAp = Application.Current;
                }
                omAp = omPassedA;
            }
            MobjectStartApp();
            // iMobjectPassedApp
        }

        public Mobject(Application omPassedA, Page omPassedP)
            : base() {
            iMobjectPassedApp = (int) MethodStateIs.Start;
            if (omPassedA != null) {
                if (omAp != null) {
                    omAp = null;
                }
                omAp = omPassedA;
            }
            if (omPassedP != null) {
                if (omPa != null) {
                    omPa = null;
                }
                omPa = omPassedP;
            }
            MobjectStartApp();
            // iMobjectPassedApp
        }

        #endregion
        #region MobjectInstanceManagement
        #region MobjectEngine
        public int MobjectStartApp()
        {
            iMobjectStartApp = (int) MethodStateIs.Start;
            //
            // if (omOb == null) {
                //omOb = new Mobject();
            // }
            return iMobjectPassedApp;
            // iMobjectStartApp
        }
        #endregion
        #region MobjectReset
        public int MobjectReset() {
            iMobjectReset = (int) MethodStateIs.Start;
            //

            // iMobjectReset
            return iMobjectReset;
        }

        #endregion
        #endregion
        #region Mobject Classes and Utilities
        //
        class MdmAnalysis1
        {
            /// <summary>
            /// Analysis class
            /// </summary>
            /// 
        }
        /// AnyThis
        /// 
        class MdmBuffers1
        {
            /// <summary>
            /// Buffers class
            /// </summary>
            /// 
        }
        class MdmChild1
        {
            /// <summary>
            /// Children class
            /// </summary>
            /// 
        }
        class dave1
        {
            /// <summary>
            /// 
            /// </summary>
            /// 
        }
        class MdmCollection1
        {
            /// <summary>
            /// Collection class
            /// </summary>
            /// 
        }

        /*
         * context
         * 
         * Database
         * 
         * Instance
         * 
         * List
         * 
         * Method
         * 
         * Parent
         * 
         * Process
         * 
         * Property
         * 
         * Relationship
         * 
         * Sibling
         * 
         * States
         * 
         * Stream
         * 
         * Symbol
         * 
         * ThisObject
         * 
         * Type
         * 
        *
         * */
        #endregion
        #region ArrayHandling
        // A class that works just like ArrayList, but sends event
        // notifications whenever the lMapplicationList changes.
        public class MdmList1: ArrayList
        {
            // Constructor
            public MdmList1():base()
            {
                // initialize List properties
                bool bOK;
                bOK = dinitialize();
            }
            internal bool dinitialize()
            {
                // TODO Array Handling

                string iMdmListType = "basic";
                return true;
            }

            // An event that clients can use to be notified whenever the
            // elements of the lMapplicationList change.
            public event ChangedEventHandler Changed;

            // Invoke the Changed event; called whenever lMapplicationList changes
            internal virtual void OnChanged(EventArgs e)
            {
                if (Changed != null)
                    Changed(this, e);
            }

            // Override some of the methods that can change the lMapplicationList;
            // invoke event after each
            public override int Add(object value)
            {
                int i = base.Add(value);
                OnChanged(EventArgs.Empty);
                return i;
            }

            public override void Clear()
            {
                base.Clear();
                OnChanged(EventArgs.Empty);
            }

            public override object this[int index]
            {
                set
                {
                    base[index] = value;
                    OnChanged(EventArgs.Empty);
                }
                get {
                    return base[index];
                }
            }
        }
        #endregion
        #region EventHandling
        // A delegate type for hooking up change notifications.
        public delegate void ChangedEventHandler(object sender, EventArgs e);
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
          public void ListChanged(object sender, EventArgs e) 
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
        /// Console_ properties
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
        public  class MdmPivot1
        {
            public string[] saMPivot1;
            public int iMPivot1Length;
            public string[] saMPivot2;
            public int iMPivot2Length;
            public int[] iaMPivot3;
            public int iMPivot3Length; // TODO non-unique polish
            // TODO Pivot Table Handling

            static void DoPivotStrFill(object[] oaPassedArray, int iPassedIndex, int iPassedCount, object ooPassedValue)
            {
                for (int i = iPassedIndex; i < iPassedIndex + iPassedCount; i++)
                {
                    oaPassedArray[i] = ooPassedValue;
                };
            }

            static void BuildPivotStr()
            {
                int iMPivot1Length = 100;
                string[] saMPivot1 = new string[iMPivot1Length];
                int iMPivot2Length = 100;
                string[] saMPivot2 = new string[iMPivot2Length];
                DoPivotStrFill(saMPivot1, 0, iMPivot1Length, "Undefined");
                DoPivotStrFill(saMPivot2, 0, iMPivot2Length, null);
            }

            static void BuildPivotStr(int iPassedLength)
            {
                int iMPivot1Length = iPassedLength;
                string[] saMPivot1 = new string[iMPivot1Length];
                int iMPivot2Length = iPassedLength;
                string[] saMPivot2 = new string[iMPivot2Length];
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

