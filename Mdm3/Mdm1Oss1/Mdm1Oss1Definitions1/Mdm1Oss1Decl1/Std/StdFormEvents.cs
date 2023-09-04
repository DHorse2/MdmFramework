#region Dependencies
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Controls;

#region  Mdm Core
using Mdm;
using Mdm.Oss;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.File.Control;
//@@@CODE@@@using Mdm.Oss.Mapp;
// using Mdm.Oss.Mobj;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
//using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
//using Mdm.Oss.Threading;
using Mdm.Oss.Components;
//using Mdm.World;
#endregion
#region  Mdm Db and File
//using Mdm.Oss.File;
//using Mdm.Oss.File.Control;
//using Mdm.Oss.File.Db;
//using Mdm.Oss.File.Db.Data;
//using Mdm.Oss.File.Db.Table;
//using Mdm.Oss.File.Db.Thread;
//using Mdm.Oss.File.Properties;
//using Mdm.Oss.File.RunControl;
#endregion
#region Mdm WinUtil, System Shell32, WshRuntime
using Mdm.Oss.WinUtil;
// Project > Add Reference > 
// add shell32.dll reference
// (new) possibly interop.Shell32 & interop.IWshRuntimeLibrary
// > COM > Microsoft Shell Controls and Automation
using Shell32;
// > COM > Windows Script Host Object Model.
using IWshRuntimeLibrary;
#endregion
#endregion

namespace Mdm.Oss.Std
{
    public partial class StdFormDef
    {
    }
}
