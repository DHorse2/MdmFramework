using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Mdm.Oss.Mobj;

namespace Mdm.Oss.Mobj {

    #region ArrayHandling
    /// <summary>
    /// <para> Mdm Array List Extensions</para>
    /// <para> A class that works just like ArrayList, but sends event
    /// notifications whenever the lMapplicationList changes.</para>
    /// </summary>
    public class MdmList1 : ArrayList {
        String MdmListType = "basic";
        // Constructor
        public MdmList1()
            : base() {
            // initialize List properties
            bool bOK;
            bOK = dinitialize();
        }
        internal bool dinitialize() {
            // TODO z$RelVs4 MdmList1 Array Handling
            return true;
        }

        // An event that clients can use to be notified whenever the
        // elements of the MapplicationList change.
        public event ChangedEventHandler Changed;

        // Invoke the Changed event; called whenever lMapplicationList changes
        internal virtual void OnChanged(EventArgs e) {
            if (Changed != null)
                Changed(this, e);
        }

        // Override some of the methods that can change the lMapplicationList;
        // invoke event after each
        public override int Add(Object value) {
            int i = base.Add(value);
            OnChanged(EventArgs.Empty);
            return i;
        }

        public override void Clear() {
            base.Clear();
            OnChanged(EventArgs.Empty);
        }

        public override Object this[int index] {
            set {
                base[index] = value;
                OnChanged(EventArgs.Empty);
            }
            get {
                return base[index];
            }
        }
    }
    #endregion

    #region Mobject Classes and Utilities
    //
    /// <summary>
    /// <para> Mdm Analysis Class</para>
    /// <para> Not implemented in this release.</para>
    /// </summary>
    class MdmAnalysis1 {
        /// <summary>
        /// Analysis class
        /// </summary>
        /// 
    }
    /// AnyThis
    /// 
    /// <summary>
    /// <para> Mdm Buffer Management and Extensions</para>
    /// <para> Not implemented in this release.</para>
    /// </summary>
    class MdmBuffers1 {
        /// <summary>
        /// Buffers class
        /// </summary>
        /// 
    }
    /// <summary>
    /// <para> Mdm Child Object (Associations)</para>
    /// <para> Not implemented in this release.</para>
    /// </summary>
    class MdmChild1 {
        /// <summary>
        /// Children class
        /// </summary>
        /// 
    }
    /// <summary>
    /// <para> Test Class</para>
    /// <para> Not implemented in this release.</para>
    /// </summary>
    class dave1 {
        /// <summary>
        /// Test Class
        /// </summary>
        /// 
    }
    /// <summary>
    /// <para> Mdm Collections Management and Extensions</para>
    /// <para> Not implemented in this release.</para>
    /// </summary>
    class MdmCollection1 {
        /// <summary>
        /// Collection class
        /// </summary>
        /// 
    }

     /* ToDo Move into ReadMe
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

}
