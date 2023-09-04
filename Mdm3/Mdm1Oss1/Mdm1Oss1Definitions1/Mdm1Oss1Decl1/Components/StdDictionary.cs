using System;
using System.Collections;
//using System.Collections.Concurrent; // Not in Net 35
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
using System.Diagnostics;
//using System.Drawing;
//using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

// add shell32.dll reference
// or COM Microsoft Shell Controls and Automation
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.File;
using Mdm.Oss.File.Control;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.Thread;

namespace Mdm.Oss.Components
{
    /// <summary>
    /// <para> Pick Dictionary Item</para>
    /// <para> This defines a single dictionary entry
    /// for the pick file system.  Naming conventions
    /// follow the Pick equivalents fairly closely.</para>
    /// </summary>
    /// <remarks>
    /// Usage needs clarification (Fixed).
    /// Origin: Mdm code converted as public class PickDictIndexDef
    /// IMPORTANT: Many Mdm function names are being normalized.
    /// References in classes to "Pick",
    /// when not refering to the (multivalued) c# syntax extension
    /// are part of Std.
    /// </remarks>
    public class StdDictItemDef
         : StdBaseDef, IDisposable
    // Pick Dict Item Class
    {
        public string aaabaMarker;
        // OOPS! ToDo This is very Pick Specific
        // We have the pick variant and not the base class.
        #region Object
        public object ItemObjectId;
        public new object Id
        {
            // This is SRT related:
            get
            {
                if (IdIsNumeric) { return Id; }
                if (IdIsString) { return ItemId; }
                return ItemObjectId;
            }
            set
            {
                if (IdIsNumeric) { Id = (int)value; }
                ItemId = value.ToString();
                ItemObjectId = (object)value;
            }
        }
        #endregion
        #region Fields
        public bool InstanceCtor = false;

        public bool IdIsNumeric;
        public bool IdIsString;
        public String IdConverted;
        //
        public bool IdFoundNumericPk;
        //
        public int Index;  //  Field being examined in this Dictionary Item
        public int Counter;  // Number of fields making up this Dictionary Item
        public int Length;
        //
        public String sAttrNumber;
        public bool IsData;
        public bool IsDict;
        // Type
        public Type DataType;
        public Type DataSubType;
        // Dependancy 
        public String sDependancy;
        public bool bDependancy;
        public int iDendancyKeyColumn;
        public String sDendancyKeyList;
        // 
        public String InputConversion;
        public String InputConvType;
        public String InputConvSubType;
        //
        public String spOutputConversion;
        public String spOutputConvType;
        public String spOutputConvSubType;

        public bool Justify;
        public int Width;
        // ??
        public String Heading;
        public String HelpShort;
        public String HelpLong;
        public String HelpHow;

        public int Touched;
        public bool IdDone;
        public bool Change;
        public bool DefinitionFound;
        // Add
        public String ColAdd;
        public String ColDelete;
        public String ColAlter;
        public String ColUsdate;
        public String ColValidate;
        public String ColStatisticsGet;
        public String ColView;

        public bool ColAddFlag;
        public bool ColDeleteFlag;
        public bool ColAlterFlag;
        public bool ColUsdateFlag;
        public bool ColValidateFlag;
        public bool ColStatisticsGetFlag;
        public bool ColViewFlag;

        public String sTrigerAdd;
        public String sTrigerDelete;
        public String sTrigerAlter;
        public String sTrigerUsdate;

        public bool bTrigerAdd;
        public bool bTrigerDelete;
        public bool bTrigerAlter;
        public bool bTrigerUsdate;
        #endregion
        public StdDictItemDef(ref object SenderPassed, ref object stPassed)
            : base(ref SenderPassed, ref stPassed)
        {
            InstanceCtor = true;
            PickDictItemReset(this);
        }
        public StdDictItemDef()
            //: base(ref Sender, ref ConsoleSender)
        {
            InstanceCtor = true;
            PickDictItemReset(this);
        }
        public void PickDictItemReset(StdDictItemDef PickDictItemPassed)
        {
            PickDictItemPassed.ItemId = "";
            PickDictItemPassed.Id = 0;
        }
    }
    /// <summary>
    /// <para> Pick Dictionary Index</para>
    /// <para> An indexed array of ColumnMax elements
    /// used to build or manipulate a Pick dictionary.</para>
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class StdDictIndexDef<TValue>
         : StdDictIndexDef<string, TValue>
    // : StdBaseDef, IDisposable
    // Note: project sample extended generic class. Used.
    {
        public string aaaBMarker;
    }
    public class StdDictIndexDef<TKey, TValue>
         : SortedDictionary<TKey, TValue>
    // : StdBaseDef, IDisposable
    {
        public string aaaAMarker;
        #region Static
        // Net35
        // public static List<CalculationTaskDef> CalculationTaskList;
        // public static Dictionary<string, CalculationTaskDef> StdDict;
        // Not Net35 compatible.
        // public static ConcurrentDictionary<string, object> StdDict;
        //public static SortedDictionary<string, T> Items;
        public static volatile int Counter;
        public volatile DataStatusIs DataStatus;
        public volatile DictionaryStatusIs DictionaryStatus;
        // Threading
        public static Object LockObject;
        public static Mutex BusyMutex;
        public static volatile int DictBusyValue;
        public static int DictBusy
        {
            get { return DictBusyValue; }

            set { DictBusyValue = value; }
        }
        // Wait for thread
        public volatile bool WaitIsBusy;
        public Mutex WaitLockMutex;
        #endregion
        #region Data and properties
        public int Id { get; set; }
        public int ItemId;
        //public T Item;
        public bool NewRecord;
        public string TaskType;
        public bool SetButtons;
        public int RowCount;
        #endregion
        #region Results
        // the use of a long result is to implement Status / State handling per the framework. (Success / Failure)
        public object ObjectResult;
        public StateIs StdResultLong;
        public string StringResult;
        #endregion
        #region Last Access ItemData
        System.DateTime LastAccessDateTime { get; set; }
        // Last Accessed Index
        System.String LastAccessFieldName { get; set; }
        System.Int32 LastAccessColumnIndex { get; set; }
        #endregion
        // To enable client code to validate input 
        // when accessing your indexer.
        public int Length
        {
            get { return Count; }
        }
        private int ipIndGet;
        // Indexer declaration.
        // Input parameter is validated by client 
        // code before being passed to the indexer.
        public new TValue this[TKey IndPassed] // ToDo This didn't work.
        {
            get
            {
                // ToDo something
                return base[IndPassed];
                //return Items[IndPassed];
            }

            set
            {
                // ToDo something
                base[IndPassed] = value;
                //Items[IndPassed] = value;
            }
        }
        //// The get accessor
        //public T this[T IndValuePassed]
        //{
        //    get { return (IndFindValue(IndValuePassed)); }
        //    set { StdDict[IndGet(IndValuePassed)] = value; }
        //}
        // Constructors and Init / Dispose
        #region Position
        // This method finds the IndInstance or returns -1
        public int DictPosGet(TKey IndValuePassed)
        {
            ipIndGet = 0;
            foreach (KeyValuePair<TKey, TValue> IndInstance in this)
            //foreach (KeyValuePair<string, T> IndInstance in Items)
            {
                if (IndInstance.Key.ToString() == IndValuePassed.ToString())
                {
                    // ipInd = ipIndGet;
                    return ipIndGet;
                }
                ipIndGet++;
            }
            return -1;
        }
        // This method finds the IndInstance or returns sEmpty
        public KeyValuePair<TKey, TValue> DictPosGetValue(int IndValuePassed)
        {
            ipIndGet = 0;
            //foreach (KeyValuePair<string, T> IndInstance in Items)
            foreach (KeyValuePair<TKey, TValue> IndInstance in this)
            {
                // StdDict[ipIndGet] = "?";
                if (ipIndGet == IndValuePassed)
                {
                    return IndInstance;
                }
                ipIndGet++;
            }

            return new KeyValuePair<TKey, TValue>();
        }
        KeyValuePair<TKey, TValue> IndInstanceLast;
        public TValue DictFindValue(TValue IndValuePassed)
        {
            ipIndGet = 0;
            foreach (KeyValuePair<TKey, TValue> IndInstance in this)
            {
                IndInstanceLast = IndInstance;
                // StdDict[ipIndGet] = "?";
                if (IndInstance.Value.ToString() == IndValuePassed.ToString())
                {
                    return IndInstance.Value;
                }
                ipIndGet++;
            }
            return IndInstanceLast.Value;
        }
        #endregion
        static StdDictIndexDef()
        {
            LockObject = new object();
            // StdDictBusyLock();
            // a locked regular dictionary is more performant.
            lock (LockObject)
            {
                //StdDict = new SortedDictionary<string, T>();
                //Items = this;
                //this = new StdDictIndexDef<T>;
                Counter = 0;
                LockObject = new object();
                BusyMutex = new Mutex();
            }
        }
    }
    /// <summary>
    /// <para> Pick Dictionary Item Array Index</para>
    /// <para> An indexed array of ColumnAliasMax elements
    /// that contains the dictionary entry items (records.)
    /// It is used to build or manipulate a Pick dictionary.</para>
    /// </summary>
    /// <remarks>
    /// Usage needs clarification
    /// </remarks>
    public class StdDictItemArrayDef
         : StdBaseDef, IDisposable
    // Pick Item Array Dictionary.
    // ToDo check any related code. StdDictItemArrayDef
    {
        public string aaacaMarker;
        #region Static
        public static int sdIndexMax = (int)ArrayMax.ColumnAliasMax;
        public static int sdIndexMaxNew = (int)ArrayMax.ColumnAliasMax + 1; // Used in the new
        #endregion
        //
        public StdDictItemDef[] sdArray = new StdDictItemDef[sdIndexMaxNew];
        public volatile int sdaIndex;
        // Net35
        // public static List<CalculationTaskDef> CalculationTaskList;
        // public static Dictionary<string, CalculationTaskDef> StdDict;
        // Not Net35 compatible.
        // public static ConcurrentDictionary<string, object> StdDict;
        //public static SortedDictionary<string, T> StdDict;
        public static volatile int Counter;
        // Threading
        public static Object LockObject;
        public static Mutex BusyMutex;
        public static volatile int DictBusyValue;
        public StdDictItemDef this[int sdaIndexPassed]
        {
            get
            {
                SdArrayCheck(ref sdaIndexPassed);
                return sdArray[sdaIndex];
            }
            set
            {
                SdArrayCheck(ref sdaIndexPassed);
                sdArray[sdaIndex] = value;
            }
        }
        #region Constructors and Init / Dispose
        static StdDictItemArrayDef()
        {
            // Note: not sure if the dict should get locked or
            // if I should continue locking the task object (this).
            LockObject = new object();
            // StdDictBusyLock();
            // lock (this)
            // a locked regular dictionary is more performant.
            // LockObject = new object();
            lock (LockObject)
            {
                //if (StdDict == null)
                //{
                //CalculationTaskList = new List<CalculationTaskDef>();
                //StdDict = new Dictionary<string, object>();
                //sdArray = new StdDictItemDef[sdIndexMaxNew];
                // StdDict = new ConcurrentDictionary<string, object>();
                //}
                Counter = 0;
                LockObject = new object();
                BusyMutex = new Mutex();
            }
            // st = new StdConsoleManagerDef(ClassRoleIs.None, ClassFeatureIs.None);
        }
        public StdDictItemArrayDef()
        {
            sdArray = new StdDictItemDef[sdIndexMaxNew];
            sdaIndex = 0;
        }
        #endregion
        public void SdArrayCheck(ref int sdaIndexPassed)
        {
            sdaIndex = sdaIndexPassed;
            if (sdaIndex < 0)
            {
                sdaIndex = 0;
                // ToDo Exception Index Error, out of range (below zero)
            }
            if (sdaIndex > (int)ArrayMax.ColumnAliasMax)
            {
                sdaIndex = (int)ArrayMax.ColumnAliasMax;
                // ToDo Exception Index Error, out of range (greater than maximum allowed)
            }
            if (sdArray[sdaIndex] == null)
            {
                sdArray[sdaIndex] = new StdDictItemDef();
            }
        }
    }
    /// <summary>
    /// <para> Pick Dictionary Row</para>
    /// <para> An indexed array of ColumnAliasMax elements</para>
    /// </summary>
    /// <remarks>
    /// Usage needs clarification
    /// </remarks>
    public class StdRowDef
    {
        public string aaaraMarker;
        #region $include Mdm.Oss.File mFile PickDictControl
        // Index Key for array and relative row number
        // ToDo Not revised. This is multivalued version.
        private int ipsdIndex;
        public int sdIndex
        {
            get { return ipsdIndex; }
            set { ipsdIndex = value; }
        }

        public static int sdIndexMax = (int)ArrayMax.ColumnAliasMax;
        public static int sdIndexMaxNew = (int)ArrayMax.ColumnAliasMax + 1; // Used in the new
        public int sdIndexHigh;
        public int sdIndexAliasLow;
        //
        // public PickDictItemDef[] PickDictArray = new PickDictItemDef[sdIndexMaxNew];
        public StdDictItemArrayDef sdArray = new StdDictItemArrayDef();
        //
        public int iAttrType;
        // Extracted from ItemId (0) Dictionary PK / Alias key
        public int sdIndexItemId;
        public bool ItemIdIsNumeric = true;
        public String ItemId;
        public int ItemIntId;
        // Extracted from AttrTwo (2) Dictionary Attr
        public String AttrTwoString;
        public int AttrTwoInt;
        public bool AttrTwoIsNumeric = false;
        //
        public int sdIndexAttrTwo;
        //
        public bool ColumnInvalid = false;
        public int DictColumnTouched;
        // Account name can be equivalent to
        // Account indicating another Company
        // Account indicating another System
        public String AttrTwoStringAccounName;
        // (10)
        // Column Width
        public String ColumnWidthString;
        public int ColumnWidth;
        public bool ColumnWidthIsNumeric;
        //
        // Attr three can be a
        // File Name
        // File Name Dict, File ItemData File
        // File Dict without data
        public String AttrThreeFileName;
        //
        public bool ItemIdFoundNumericPk = false;
        public bool DictColumnIdDone = false;

        private int ipsdIndexTemp;
        public int sdIndexTemp
        {
            get
            {
                return ipsdIndexTemp;
            }
            set
            {
                ipsdIndexTemp = value;
            }
        }

        public int sdItemCount;

        public int sdErrorCount;
        public int sdErrorWarningCount;

        public bool sdIndexDoSearch = true;
        public int ColumnDataPoints;
        #endregion
        public StdRowDef() { }
        public void DataClear()
        {
            // Pick Dictionary
            sdIndex = 0;
            sdItemCount = 0;
        }
        public void RowDataClear(int sdIndexPassed)
        {
            // if (PickDictArray[sdIndex] == null) { PickDictArray[sdIndex] = new PickDictItemDef(); } 
            sdArray[sdIndexPassed].ItemId = "";
            sdArray[sdIndexPassed].Id = 0;
        }
    }
    public enum DictionaryStatusIs : int
    {
        Initialized,
        Open,
        Busy,
        Closed,
        Disposed,
        Expanded,
        Visible,
        NotSet = 0
    }
    public class StdKeyDef : IComparable<StdKeyDef>
    {
        // Type Fix 99Xxxxxx... Level Order StructuredName
        // Name:
        // App_Form 00Xxx is group
        // Console_MsgType
        // Clipboard_Form
        #region Fields
        public String Key;
        public string IconLevel;
        public string IconOrder;
        public String IconName;
        #endregion
        #region Constructors, Clear, Dispose
        public StdKeyDef(
            string LevelPassed,
            string OrderPassed,
            String TextPassed)
        {
            BuildFromFields(LevelPassed, OrderPassed, TextPassed);
        }
        public StdKeyDef(String ItemIdPassed)
        {
            BuildFromString(ItemIdPassed);
        }
        public StdKeyDef()
        {
            Clear();
        }
        public void Clear()
        {
            Key = "##NotSet";
            IconLevel = "#";
            IconOrder = "#";
            IconName = "NotSet";
        }
        #endregion
        #region Get/Set
        public void GetTo(
            ref string LevelPassed,
            ref string OrderPassed,
            ref String TextPassed)
        {
            // This is correct,
            // it aligns the callers Icon fields
            // with the Key.
            LevelPassed = IconLevel;
            OrderPassed = IconOrder;
            TextPassed = IconName;
        }
        public String Get()
        {
            return BuildKey();
        }
        public void Set(String IconLevelPassed, string IconOrderPassed, string IconNamePassed)
        {
            BuildKey();
        }
        public void Set(String ItemIdPassed)
        {
            BuildFromString(ItemIdPassed);
        }
        #endregion
        #region Build Structured Key from FIX string
        public String BuildKey()
        {
            Key = IconLevel + IconOrder + IconName;
            return Key;
        }
        public String BuildFromFields(
            string LevelPassed,
            string OrderPassed,
            String TextPassed)
        {
            IconLevel = LevelPassed;
            IconOrder = OrderPassed;
            IconName = TextPassed;
            return BuildKey();
        }
        public String BuildFromString(String ItemIdPassed)
        {
            try { IconLevel = ItemIdPassed.Substring(0, 1); }
            catch (Exception) { IconLevel = "#"; }
            try { IconOrder = ItemIdPassed.Substring(1, 1); }
            catch (Exception) { IconLevel = "#"; }
            //try { KeyLevel = System.Int32.Parse(ItemIdPassed.Substring(0, 1)); }
            //catch (Exception) { KeyLevel = 0; }
            //try { KeyOrder = System.Int32.Parse(ItemIdPassed.Substring(1, 1)); }
            //catch (Exception) { KeyLevel = 0; }
            try { IconName = ItemIdPassed.Substring(2, ItemIdPassed.Length - 2); }
            catch (Exception) { IconName = "NotSet"; }
            return BuildKey();
        }
        #endregion
        public int CompareTo(StdKeyDef StdKeyPassed)
        {
            if (String.Compare(IconLevel, StdKeyPassed.IconLevel) > 0)
            {
                return 1;
            }
            else if (String.Compare(IconLevel, StdKeyPassed.IconLevel) < 0)
            {
                return -1;
            }

            if (String.Compare(IconOrder, StdKeyPassed.IconOrder) > 0)
            {
                return 1;
            }
            else if (String.Compare(IconOrder, StdKeyPassed.IconOrder) < 0)
            {
                return -1;
            }
            if (String.Compare(IconName, StdKeyPassed.IconName) > 0)
            {
                return 1;
            }
            else if (String.Compare(IconName, StdKeyPassed.IconName) < 0)
            {
                return -1;
            }
            return 1;
        }
        public override string ToString()
        {
            return Key;
        }
    }
    public class StdDictIndexExDef<TKey, TValue>
     : StdBaseDef, IDisposable
    {
        // Experimental. This is weird one.
        // 1) We end up with kind of closure,
        // It is a sorted dict where all thie items
        // share the StdBase (app scope really) class.
        // 2) As such the TValue might be doing the same thing
        // with different values but you would avoid that where possible.
        // 3) With a large array you would use less memory.
        //
        public string aaaAMarker;
        #region Static
        // Net35
        // public static List<CalculationTaskDef> CalculationTaskList;
        // public static Dictionary<string, CalculationTaskDef> StdDict;
        // Not Net35 compatible.
        // public static ConcurrentDictionary<string, object> StdDict;
        //public static SortedDictionary<string, T> Items;
        // Threading
        public static Object LockObject;
        public static Mutex BusyMutex;
        public static volatile int DictBusyValue;
        public static int DictBusy
        {
            get { return DictBusyValue; }

            set { DictBusyValue = value; }
        }
        // Wait for thread
        public volatile bool WaitIsBusy;
        public Mutex WaitLockMutex;
        #endregion
        // public TValue Item; // The current item being indexed.
        public SortedDictionary<TKey, TValue> Items;
        public volatile DictionaryStatusIs DictionaryStatus;
        #region Data and properties
        public volatile int Counter;
        public int Length
        {
            get { return Items.Count; }
        }
        public struct Tex
        {
            StdBaseDef StdBase;
            int Id;
            string ItemId;
            TValue Item;
        }
        public bool NewRecord;
        public string TaskType;
        public bool SetButtons;
        public int RowCount;
        #endregion
        #region Results
        // the use of a long result is to implement Status / State handling per the framework. (Success / Failure)
        public object ObjectResult;
        public StateIs StdResultLong;
        public string StringResult;
        #endregion
        #region Last Access ItemData
        System.DateTime LastAccessDateTime { get; set; }
        // Last Accessed Index
        System.String LastAccessFieldName { get; set; }
        System.Int32 LastAccessColumnIndex { get; set; }
        #endregion
        // To enable client code to validate input 
        // when accessing your indexer.
        #region Extended functions, search, positioning, 
        private int ipIndGet;
        // Indexer declaration.
        // Input parameter is validated by client 
        // code before being passed to the indexer.

        //     Gets a collection containing the values in the System.Collections.Generic.SortedDictionary`2.
        public System.Collections.Generic.SortedDictionary<TKey, TValue>.ValueCollection Values 
        { get { return Items.Values; } }
        //     Gets a collection containing the keys in the System.Collections.Generic.SortedDictionary`2.
        public System.Collections.Generic.SortedDictionary<TKey, TValue>.KeyCollection Keys
        { get { return Items.Keys; } }
        // THIS
        // ToDo This didn't work in the original (2102) tests.
        // It had an odd (???) error here.
        // This here is an old pattern but using a closure-ish idea.
        public TValue this[TKey IndPassed] 
        {
            get
            {
                // ToDo something
                return Items[IndPassed];
                //return Items[IndPassed];
            }

            set
            {
                // ToDo something
                Items[IndPassed] = value;
                //Items[IndPassed] = value;
            }
        }
        #endregion
        #region Position, Find
        // This method finds the IndInstance or returns -1
        public int DictPosGet(TKey IndValuePassed)
        {
            ipIndGet = 0;
            foreach (KeyValuePair<TKey, TValue> IndInstance in Items)
            //foreach (KeyValuePair<string, T> IndInstance in Items)
            {
                if (IndInstance.Key.ToString() == IndValuePassed.ToString())
                {
                    // ipInd = ipIndGet;
                    return ipIndGet;
                }
                ipIndGet++;
            }
            return -1;
        }
        // This method finds the IndInstance or returns sEmpty
        public KeyValuePair<TKey, TValue> DictPosGetValue(int IndValuePassed)
        {
            ipIndGet = 0;
            //foreach (KeyValuePair<string, T> IndInstance in Items)
            foreach (KeyValuePair<TKey, TValue> IndInstance in Items)
            {
                // StdDict[ipIndGet] = "?";
                if (ipIndGet == IndValuePassed)
                {
                    return IndInstance;
                }
                ipIndGet++;
            }

            return new KeyValuePair<TKey, TValue>();
        }
        KeyValuePair<TKey, TValue> IndInstanceLast;
        public TValue DictFindValue(TValue IndValuePassed)
        {
            ipIndGet = 0;
            foreach (KeyValuePair<TKey, TValue> IndInstance in Items)
            {
                IndInstanceLast = IndInstance;
                // StdDict[ipIndGet] = "?";
                if (IndInstance.Value.ToString() == IndValuePassed.ToString())
                {
                    return IndInstance.Value;
                }
                ipIndGet++;
            }
            return IndInstanceLast.Value;
        }
        //// The get accessor
        //public T this[T IndValuePassed]
        //{
        //    get { return (IndFindValue(IndValuePassed)); }
        //    set { StdDict[IndGet(IndValuePassed)] = value; }
        //}
        #endregion
        #region Constructors and Init / Dispose
        static StdDictIndexExDef()
        {
            LockObject = new object();
            // StdDictBusyLock();
            // a locked regular dictionary is more performant.
            lock (LockObject)
            {
                //StdDict = new SortedDictionary<string, T>();
                //Items = this;
                //this = new StdDictIndexDef<T>;
                LockObject = new object();
                BusyMutex = new Mutex();
            }
        }
        public StdDictIndexExDef()
        {
            Items = new SortedDictionary<TKey, TValue>();
            Counter = 0;
        }
        #endregion
        #region Standard Collection Interface
        public IComparer<TKey> Comparer
        {
            //     Gets the System.Collections.Generic.IComparer`1 used to order the elements of
            //     the System.Collections.Generic.SortedDictionary`2.
            get { return Items.Comparer; }
        }
        public int Count
        {
            //     Gets the number of key/value pairs contained in the System.Collections.Generic.SortedDictionary`2.
            get { return Items.Count; }
        }
        public void Add(TKey key, TValue value)
        {
            //     Adds an element with the specified key and value into the System.Collections.Generic.SortedDictionary`2.
            Items.Add(key, value);
        }
        public void Clear()
        {
            //     Removes all elements from the System.Collections.Generic.SortedDictionary`2.
            Items.Clear();
        }
        public bool ContainsKey(TKey key)
        {
            //     Determines whether the System.Collections.Generic.SortedDictionary`2 contains
            //     an element with the specified key.
            return Items.ContainsKey(key);
        }
        public bool ContainsValue(TValue value)
        {
            //     Determines whether the System.Collections.Generic.SortedDictionary`2 contains
            //     an element with the specified value.
            return Items.ContainsValue(value);
        }
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            //     Copies the elements of the System.Collections.Generic.SortedDictionary`2 to the
            //     specified array of System.Collections.Generic.KeyValuePair`2 structures, starting
            //     at the specified index.
            Items.CopyTo(array, index);
        }
        public SortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            //     Returns an enumerator that iterates through the System.Collections.Generic.SortedDictionary`2.
            return Items.GetEnumerator();
        }
        public bool Remove(TKey key)
        {
            //     Removes the element with the specified key from the System.Collections.Generic.SortedDictionary`2.
            return Items.Remove(key);
        }
        public bool TryGetValue(TKey key, out TValue value)
        {
            //     Gets the value associated with the specified key.
            return Items.TryGetValue(key, out value);
        }
        #endregion
    }
}
