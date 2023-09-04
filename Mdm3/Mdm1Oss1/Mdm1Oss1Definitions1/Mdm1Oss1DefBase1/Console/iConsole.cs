using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mdm.Oss.Components;
using Mdm.Oss.Decl;
using Mdm.Oss.Std;

namespace Mdm.Oss.Console
{
    interface iConsole : iTrace
    {
        object MdmConsoles { get; set; }
    }
}
