#region README
// 1) Note: If you update this list make sure to
// update the Initialize function if it is used
// for system reflection.
// This would be used as an app template.
// 2) Description:
// This document represents the possible
// dependencies you will encounter using
// this framework.
// 3) Many "using X" are commented out by default.
// 4) I based this on the ability to compile
// this lower level object. 
// In this sense it should define the
// minimum depencies of Mdm Core.
#endregion
#region Dependencies
#region System
#region System
using System;
#endregion
#region System Collections
using System.Collections;
using System.Collections.Generic;
//using System.Collections.Specialized;
#endregion
#region System Data & IO
using System.IO;
using System.Data;
//using System.Data.Common;
#endregion
#region System Globalization
//using System.Globalization;
#endregion
#region System Other
// using System.Collections.Specialized;
#endregion
#region System SQL
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
#endregion
#region System Text and Linq
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
#endregion
#region  System Threading
using System.Threading;
// using System.Threading.Tasks; // Not 35
#endregion
#region System Windows Forms
//using System.Drawing;
using System.Windows;
using System.Windows.Forms;
//using System.Windows.Controls;
#endregion
#region System ComponentModel
//using System.ComponentModel;
#endregion
#region System Reflection, Runtime, Timers
//using System.Diagnostics;
//using System.Reflection;
//using System.Runtime;
//using System.Runtime.InteropServices;
//using System.Runtime.Remoting.Messaging;
//using System.Timers;
#endregion
#region System Security
//using System.Security.AccessControl;
//using System.Security.Permissions;
//using System.Security.Principal;
#endregion
#region System Serialization (Runtime and Xml)
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Xml.Serialization;
#endregion
#region System XML
//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Schema;
#endregion
#endregion
#region Mdm
// Mdm (Macroscope Design Matrix / Dgh (c))
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
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Components;
using Mdm.World;
#endregion
#region Mdm WinUtil, System Shell32, WshRuntime
//using Mdm.Oss.WinUtil;
//// Project > Add Reference > 
//// add shell32.dll reference
//// (new) possibly interop.Shell32 & interop.IWshRuntimeLibrary
//// > COM > Microsoft Shell Controls and Automation
//using Shell32;
//// > COM > Windows Script Host Object Model.
//using IWshRuntimeLibrary;
#endregion
#region  Mdm Db and File
using Mdm.Oss.File;
//using Mdm.Oss.File.Db;
//using Mdm.Oss.File.Db.Data;
//using Mdm.Oss.File.Db.Table;
//using Mdm.Oss.File.Db.Thread;
////using Mdm.Oss.File.Properties;
//using Mdm.Oss.File.RunControl;
//using Mdm.Oss.File.Type;
#endregion
#region  Mdm File Types
//using Mdm.Oss.File.Type;
////using Mdm.Oss.File.Type.Link;
//using Mdm.Oss.File.Type.Pick;
//using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Delim;
////using Mdm.Oss.File.Type.Srt.Script;
#endregion
#region Mdm Srt (Search, replace and transform)
//using Mdm.Srt;
//using Mdm.Srt.Core;
//using Mdm.Srt.Transform;
//using Mdm.Srt.Transform.Db.Table;
//using Mdm.Srt.Script;
#endregion
#region  Mdm MVC Mobject
//using Mdm.Oss.Mobj;
#endregion
#region  Mdm Apps Clipboard
//using Mdm.Oss.ClipUtil;
//using Mdm.Oss.ClipUtil.Windows;
#endregion
#region  Mdm Apps Shortcut Utils (Link)
//using Mdm.Oss.ShortcutUtil;
#endregion
#endregion
#endregion
namespace Mdm
{
    class DependencesReferenciesList
    {
        #region Properties - Public

        #endregion

        #region Methods - Private

        #endregion
        public List<string> ReferencesList;
        // Use the above using options for templating new code.
        #region Methods - Constructors

        public DependencesReferenciesList()
        {
            Initialize();
        }
        #endregion
        #region Methods - Initialize
        public void Initialize()
        {
            ReferencesList = new List<string>();
            #region System
            ReferencesList.Add("System");
            ReferencesList.Add("System.Collections.Generic");
            ReferencesList.Add("System.Linq");
            ReferencesList.Add("System.Text");
            #endregion
            #region System Data & SQL
            ReferencesList.Add("System.IO");
            ReferencesList.Add("System.Data");
            ReferencesList.Add("System.Data.Sql");
            ReferencesList.Add("System.Data.SqlClient");
            ReferencesList.Add("System.Data.SqlTypes");
            #endregion
            #region  System Threading
            ReferencesList.Add("System.Threading");
            ReferencesList.Add("System.Threading.Tasks");
            #endregion
            #region System Reflection
            ReferencesList.Add("System.Reflection");
            ReferencesList.Add("System.Timers");
            ReferencesList.Add("System.Runtime.InteropServices");
            ReferencesList.Add("System.Runtime.Remoting.Messaging");
            #endregion
            #region System Drawing & Windows Forms & Controls
            ReferencesList.Add("System.Drawing");
            ReferencesList.Add("System.Windows");
            ReferencesList.Add("System.Windows.Forms");
             ReferencesList.Add("System.Windows.Controls");
            #endregion
            #region System ComponentModel
            ReferencesList.Add("System.ComponentModel");
            #endregion
            #region System Security
            ReferencesList.Add("System.Security.AccessControl");
            ReferencesList.Add("System.Security.Permissions");
            ReferencesList.Add("System.Security.Principal");
            #endregion
            #region  Mdm Core
            ReferencesList.Add("Mdm.Oss");
            ReferencesList.Add("Mdm.Oss.Console");
            ReferencesList.Add("Mdm.Oss.Decl");
             ReferencesList.Add("Mdm.Oss.Mobj");
            ReferencesList.Add("Mdm.Oss.Sys");
            ReferencesList.Add("Mdm.Oss.Thread");
            ReferencesList.Add("Mdm.Oss.Threading");
            ReferencesList.Add("Mdm.Pick");
            ReferencesList.Add("Mdm.Pick.Console");
            ReferencesList.Add("Mdm.World");
            #endregion
            #region  Mdm Db and File
            ReferencesList.Add("Mdm.Oss.File");
            ReferencesList.Add("Mdm.Oss.File.Db");
            ReferencesList.Add("Mdm.Oss.File.Db.Data");
            ReferencesList.Add("Mdm.Oss.File.Db.Table");
            ReferencesList.Add("Mdm.Oss.File.Db.Thread");
             ReferencesList.Add("Mdm.Oss.File.Properties");
            #endregion
            #region  Mdm File Types
            ReferencesList.Add("Mdm.Oss.File.Type");
            ReferencesList.Add("Mdm.Oss.File.Type.Link");
            ReferencesList.Add("Mdm.Oss.File.Type.Sql");
            ReferencesList.Add("Mdm.Oss.File.Type.Srt.Script");
            #endregion
            return;
        }
        #endregion
        public List<string> get()
        {
            return ReferencesList;
        }
        public string GetFirst(string RefName)
        {
            return ReferencesList[0];
        }
        public object Get()
        {
            return this;
        }
        public void Set(object value)
        {
            // Set(DependencesReferenciesList)
            // this = value as DependencesReferenciesList;
            // ToDo throw error
        }
    }
    public class ClassModelMdm 
        // : mFileSql, ImFileType, IDisposable
        // Do NOT instantiate. NOT implemented.
    {
        #region Core Fields
        // Core Objects - Mapplication
        #region $include Mdm.Oss.FileUtil mFileDef FileBasicInformation
        // Command and Phrase Construction
        //public DbSynDef DbSyn;
        // Database Master and Master Phrases
        //public DbMasterSynDef DbMasterSyn;
        #endregion
        #endregion
        #region Framework Objects
        #endregion
        #region Class Factory
        #endregion
        #region Class
        #region Class Initialization
        // Constructors
        #region Constructor
        #endregion
        // Initializers
        // Delegates
        #endregion
        #region Class Fields
        #endregion
        #region Class Methods
        #endregion
        #region Class Interface
        #endregion
        #endregion
        #region Unit Test
        #endregion
    }

}
