using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mdm.Srt.InputTld
{
    /// <summary>
    /// Mdm1 Srt1 InputTld1 OpSys Bootstrap
    /// Mdm.Srt.InputTld - IProcesFile1 Interface
    /// </summary>
    /// 

    interface IProcesFile1
    {
        long RunEngine(string EngineAction);
        long ProcesFile(string FileAction, string FileNameIn, string FileNameOut, string FileItemIdOut, string FileActionOptions);
        long ProcessCommandConsole(string FileAction);
        long ProcessCommandAction(string FileAction, string FileNameIn, string FileNameOut, string FileItemIdOut, string FileActionOptions);
        long ProcessCommandLine(string FileCommandLine);
    }
}
