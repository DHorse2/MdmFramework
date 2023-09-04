using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mdm.Oss.FileUtil {
    /// <summary>
    /// <para> File Command Class</para>
    /// <para> involving column data retrieved
    /// or created for the the database file.</para>
    /// <para> This differs from a File Action in
    /// that column data is included with the object.
    /// It includes column ordering and unique ID 
    /// information along with requested columns.</para>
    /// </summary>
    /// <remarks>
    /// <para>This class is not currently implemented.  It
    /// will be implement when the multithreaded code and
    /// property system are implemented.</para>
    /// <para> . </para>
    /// <para>Unique Id must be part of the select and returned
    /// Although it may not be part of the data requested
    /// it must be saved in the list in order to susequently
    /// reselect a unique record from the database.</para>
    /// </remarks>
    public class FileCommandDef {
        public String ActionName;
        public Int32 ActionId;
        public FileSummaryDef FileSummary;
        public Int32 FieldCount;
        // basic sorted select
        public String Verb;
        public List<String> FieldName;
        public List<Int32> FieldIndex;
        public List<Int32> OrderUsed;
        // Unique Id must be part of the select and returned
        // Although it may not be part of the data requested
        // it must be saved in the list in order to susequently
        // reselect a unique record from the database.
        public List<Int32> UniqueIdUsedInVerb;
        public List<Int32> UniqueIdSequence;
        //
        //
        public FileCommandDef() {
            ActionName = "";
            ActionId = 0;
            // Fs = new FileSummaryDef();
            FieldCount = 0;
            //
            FieldName = new List<String> ();
            FieldIndex = new List<Int32>();
            OrderUsed = new List<Int32>();
            UniqueIdSequence = new List<Int32>();
            UniqueIdUsedInVerb = new List<Int32>();

        }
    }

}
