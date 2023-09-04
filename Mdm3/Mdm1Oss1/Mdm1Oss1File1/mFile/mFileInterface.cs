using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region  Mdm Core
using Mdm.Oss;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.File.Control;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Components;
using Mdm.World;
#endregion

namespace Mdm.Oss.File.Type
{
    public interface ImFileType
    {
        // Initialize
        StateIs InitializeType();
        StateIs InitializeType(object Sender);
        // Dispose
        StateIs DisposeType();
        // Attach
        StateIs Attach();
        // Detach
        StateIs Detach();
        // Open
        StateIs Open();
        // Close
        StateIs Close();
        // CheckExists
        // StateIs CheckExists();
        // ToDo need to implement rows added/returned/updated etc...
        // Insert
        StateIs Insert();
        // Delete
        StateIs Delete();
        // Update
        StateIs Update();
        // ReaderOpen
        // StateIs ReaderOpen();
        // ReadNext
        // StateIs ReadNext();
    }

    // Interface for readers
    // ReaderOpen
    // ReaderClose
    // ReaderRead
    // ReaderReadNext
    // ReaderReadPrev
    // Interface for readers
    // WriterOpen
    // WriterClose
    // WriterWrite

    // Interface extension for delimited file types
    //#region Core Fields
    //#endregion
    //#region Class Factory
    //#endregion
    //#region Class
    //#endregion
    //#region Unit Test
    //#endregion
}
