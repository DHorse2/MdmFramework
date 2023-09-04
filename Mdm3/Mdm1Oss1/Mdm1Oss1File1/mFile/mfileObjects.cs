#region Dependencies
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Navigation;
//
using Mdm;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Mobj;
using Mdm.Oss.Components;
#region  Mdm Db and File
using Mdm.Oss.File;
using Mdm.Oss.File.Control;
using Mdm.Oss.File.Db;
using Mdm.Oss.File.Db.Data;
using Mdm.Oss.File.Db.Table;
using Mdm.Oss.File.Db.Thread;
//using Mdm.Oss.File.Properties;
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
//using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion

namespace Mdm.Oss.File
{
    /// <MyDocs>
    /// <MyMembers name="Class Object Types">
    /// <Documentation>
    /// <para> File Management System</para>
    /// <para> Object entities, Support and Utility items.</para>
    /// <para> . </para>
    /// <para> The two main types of objects are File and Db (Database).</para>
    /// <para> Each of these may have:</para>
    /// <para> 1) A File Summary Object.</para>
    /// <para> 2) A basic file object (Fmain). </para>
    /// <para> 3) An ID object for identity. </para>
    /// <para> 4) An IO object used readers and writers.</para>
    /// <para> 5) One or more File Status Objects.</para>
    /// <para> 6) One of serveral column and attribute control classes.</para>
    /// </Documentation>
    /// </MyMembers>
    /// <MyMembers name="test">
    /// <Documentation>
    ///     The summary for this type.
    /// </Documentation>
    /// </MyMembers>
    /// 
    /// <MyMembers name="test2">
    /// <Documentation>
    ///     The summary for this other type.
    /// </Documentation>
    /// </MyMembers>
    /// </MyDocs>
    // <include file='mFileDefObject.cs' path='MyDocs/MyMembers[@name="Class Object Types"]/*' />
    // 'C:/Srt Project/Mdm/Mdm1/Mdm1Oss1/Mdm1Oss1File1/mFileDefDocumentation.xml'
    // 'C:\Srt Project\Mdm\Mdm1\Mdm1Oss1\Mdm1Oss1File1\mFileDefDocumentation.xml' 
    // <include file='xml_include_tag.doc' path='MyDocs/MyMembers[@name="Class Object Types"]/*' />
    #region Actions and MetaData Objects - SysDef, MetaDef, ActionInfoDef, FileActionDef
    /// <Documentation>
    /// System Level execeptions, arguments and objects
    /// related to a file object.  Not implemented.
    /// </Documentation> 
    public class SysDef
    {
        // ToDo System Objects - (TO BE MOVED) - xxxxxxxxx
        #region $include Mdm.Oss.File mFile System Objects
        #region $include Mdm.Oss.File mFile System - AppDomain
        /// <summary>
        /// ToDo
        /// </summary> 
        public CrossAppDomainDelegate x22 = null;  // Delegate
        /// <summary>
        /// ToDo
        /// </summary> 
        public ApplicationException x21;
        /// <summary>
        /// ToDo
        /// </summary> 
        public AppDomainUnloadedException x23;
        /// <summary>
        /// ToDo
        /// </summary> 
        public AppDomainInitializer x24; // Delegate
        /// <summary>
        /// ToDo
        /// </summary> 
        public AppDomain x24o;
        #endregion
        /* #region $include Mdm.Oss.File mFile System - Action<T1> Signatures
        public Action AT0; // Delegate
        public Action<T1> AT1; //Delegate
        public Action<T1, T2> AT2; // Delegate
        public Action<T1, T2, T3> AT3; // Delegate
        public Action<T1, T2, T3, T4> AT4; // Delegate
        #endregion
        */
        #region $include Mdm.Oss.File mFile System Exceptions
        // System Exceptions - Argument
        public ArgumentException x15 = null;
        public ArgumentOutOfRangeException x15a = null;
        public ArgumentNullException x15b = null;
        // System Exceptions - Arithmetic
        public ArithmeticException x15c = null;
        // System Exceptions - AccessViolation
        public AccessViolationException X41; // Memory Error
        // System Exceptions - DataMisaligned
        public DataMisalignedException x11a = null;
        // System Exceptions - Configuration
        public ConfigurationException x14 = null;
        #endregion
        // .Net - Property change management here.
        // Mdm - Headings and Titles.
        // Mdm - Run Control
        #region $include Mdm.Oss.File mFile System Windows
        // System Windows - StartupEvent
        public StartupEventHandler ooStartTemp = null; // Delegate Application Startup
        public StartupEventArgs ooStartTempA = null; // Delegate Application Startup Arguments
        // System Windows - Controls - TextChanged
        public TextChangedEventHandler ooControlTecxChangedEvent;
        public TextChangedEventHandler ooControlTextChanged;
        #endregion
        // <Area Id = "FileAction_DoMessages">
        public String sMformStatusMessage;
        public String sMessageBoxMessage;
        // XXX - (TO BE MOVED) END - xxxxxxxxxxxxxxxxxxxxxx
        #endregion
        public SysDef()
        {
            // List of used
            sMessageBoxMessage = "";
            sMformStatusMessage = "";
        }
    }
    /// <summary>
    /// A File Managment System Object.
    /// Meta data for general file object defining 
    /// file stream objects (primary/auxillary).
    /// Equivalent to "this" mFileDef.
    /// </summary> 
    /// <remarks>
    /// Data context specific to this file
    /// system object not already present in
    /// the base classes (run, file, etc.)
    /// </remarks> 
    public class MetaDef
    {
        public bool UsePrimary;
        public DateTime tdtPrimaryLastTouched;
        public DateTime tdtAuxiliaryLastTouched;

        public MetaDef()
        {
        }
    }
    #endregion
}
