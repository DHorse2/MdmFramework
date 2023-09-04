using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mdm.Oss.File.Type.Sql
{
    public partial class mFileSqlConnectionDef
    {
        /// <summary>
        /// </summary>
        // protected bool disposedValue = false; // To detect redundant calls
        /// <summary>
        /// </summary>
        // public bool disposed = false;
        /// <summary>
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue || !base.disposedValue)
            {
                if (disposing) 
                {
                    // TODO: dispose managed state (managed objects).

                    base.Dispose(disposing);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }
        // This code added to correctly implement the disposable pattern.
        public new void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
    }
}
