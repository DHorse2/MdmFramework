using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mdm.Oss.Decl;
using Mdm.Oss.Std;

namespace Mdm.Oss.File.Type.Sql
{
    // Not used.
    // ImFileType implementation template as parital class.
    // This was created when looking at partial classes
    // inserted pre-build vs inheritance of a base form class.
    public partial class mFileSql 
    {
        #region mFileType
        // Initialize
        public override StateIs InitializeType() { return StateIs.Undefined; }
        public override StateIs InitializeType(object Sender) { return StateIs.Undefined; }
        // Attach
        public override StateIs Attach() { return StateIs.Undefined; }
        // Detach
        public override StateIs Detach() { return StateIs.Undefined; }
        // Dispose
        public override StateIs DisposeType() { return StateIs.Undefined; }
        // Open
        public override StateIs Open() { return StateIs.Undefined; }
        // Close
        public override StateIs Close() { return StateIs.Undefined; }
        // Insert
        public override StateIs Insert() { return StateIs.Undefined; }
        // Delete
        public override StateIs Delete() { return StateIs.Undefined; }
        // Update
        public override StateIs Update() { return StateIs.Undefined; }
        #endregion
    }
}
