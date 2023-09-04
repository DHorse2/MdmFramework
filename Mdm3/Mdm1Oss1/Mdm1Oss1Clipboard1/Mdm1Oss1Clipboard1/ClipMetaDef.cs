#region Dependencies
#region System
using System;
using System.Linq;
#endregion
#region System Collections
using System.Collections;
using System.Collections.Generic;
//using System.Collections.Specialized;
#endregion
#region System Data & SQL
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
#endregion
#region System Text
using System.Text;
using System.Text.RegularExpressions;
#endregion
#region System Windows Forms
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
//using System.Windows.Controls;
#endregion
#region System Other
//using System.Collections.Specialized;
//using System.ComponentModel;
#endregion
#region System Globalization
using System.Globalization;
#endregion
#region System Serialization (Runtime and Xml)
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
#endregion
#region System Reflection, Runtime, Timers
using System.Diagnostics;
using System.Reflection;
using System.Runtime;
//using System.Runtime.InteropServices;
//using System.Runtime.Remoting.Messaging;
using System.Timers;
#endregion
#region System XML
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Serialization.Advanced;
using System.Xml.Serialization.Configuration;
#endregion

#region  Mdm Core
using Mdm.Oss;
//@@@CODE@@@using Mdm.Oss.CodeUtil;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
//@@@CODE@@@using Mdm.Oss.Mapp;
// using Mdm.Oss.Mobj;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Printer;
using Mdm.World;
#endregion
#region Mdm WinUtil, System Shell32, WshRuntime
using Mdm.Oss.WinUtil;
using Mdm.Oss.WinUtil.Types;
//          add shell32.dll reference
//          or COM Microsoft Shell Controls and Automation
using Shell32;
//          At first, Project > Add Reference > COM > Windows Script Host Object Model.
using IWshRuntimeLibrary;
using SHDocVw;
#endregion
#region  Mdm Db and File
using Mdm.Oss.File;
using Mdm.Oss.File.Control;
using Mdm.Oss.File.Db;
using Mdm.Oss.File.Db.Data;
using Mdm.Oss.File.Db.Table;
using Mdm.Oss.File.Db.Thread;
//using Mdm.Oss.File.Properties;
using Mdm.Oss.File.RunControl;
#endregion
#region  Mdm File Types
using Mdm.Oss.File.Type;
// using Mdm.Oss.File.Type.Link;
using Mdm.Oss.File.Type.Sql;
using Mdm.Oss.File.Type.Srt.Script;
#endregion
#region Mdm Srt (Search, replace and transform)
using Mdm.Srt;
using Mdm.Srt.Core;
using Mdm.Srt.Transform;
using Mdm.Srt.Script;
#endregion
#region  Mdm Clipboard
using Mdm.Oss.ClipUtil;
#endregion
#endregion

namespace Mdm.Oss.ClipUtil
{
    /// <summary>
    /// Clipboard Meta Data
    /// </summary>
    /// <remarks>
    /// Meta data for each addition to the clipboard
    /// </remarks>
    // [DataContract]
    // ToDo to DataContract and DataContractSerializer add reference to dll "?"
    // ToDo and using ?
    // [Serializable] : IXmlSerializable
    // [Serializable]
    [DataContract(Namespace = "")]
    public class ClipMetaDef
    // : IXmlSerializable
    {
        #region public ClipMetaDef() { }
        public ClipMetaDef(ClipMetaDef t) { this.Value = t; }
        public ClipMetaDef Value { get; set; }
        //
        public XmlSchema GetSchema() { return null; }
        public void ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "MyEvent")
            {
                string TextX = reader["TextX"];
                DateTime DateX = DateTime.FromBinary(Int64.Parse(reader["DateX"]));
                reader.Read();
            }
        }
        public void WriteXml(XmlWriter writer)
        {
            //writer.WriteAttributeString("IdKey", IdKey);
            //writer.WriteAttributeString("DataCreationTime", DataCreationTime.ToBinary().ToString());
            //writer.WriteAttributeString("IdKey", IdKey);
            //writer.WriteAttributeString("IdKey", IdKey);
            //writer.WriteAttributeString("IdKey", IdKey);
            //writer.WriteAttributeString("IdKey", IdKey);
            ////writer.WriteAttributeString("TextX", TextX);
            ////writer.WriteAttributeString("DateX", DateX.ToBinary().ToString());
        }
        #endregion
        #region Fields
        // [DataMember]
        public UInt32 IdKey;
        // [DataMember]
        public DateTimeOffset DataCreationTime;
        /*
        	int year,
	        int month,
	        int day,
	        int hour,
	        int minute,
	        int second,
	        int millisecond,
	        Calendar calendar,
	        DateTimeKind kind
        */
        // [DataMember]
        public bool DataProcessed;
        // [DataMember]
        public Int32 SequenceNumber;
        // [DataMember]
        public string Data1;
        // [DataMember]
        public string cData;
        public IDataObject ciData;
        #endregion
        /// <summary>
        /// Creates meta data object with date, time, 
        /// sequence number, processed flag, format, contents.
        /// </summary> 
        public ClipMetaDef()
        {
            IdKey = 0;
            // Call the native GetSystemTime method
            // with the defined structure.
            Win32TimeDef.SYSTEMTIME DateTimeNow = new Win32TimeDef.SYSTEMTIME();
            Win32TimeDef.GetSystemTime(DateTimeNow);
            // Load Current Time
            DataCreationTime = new DateTimeOffset(
                DateTimeNow.wYear,
                DateTimeNow.wMonth,
                // DateTimeNow.DayOfWeek,
                DateTimeNow.wDay,
                DateTimeNow.wHour,
                DateTimeNow.wMinute,
                DateTimeNow.wSecond,
                DateTimeNow.wMilliseconds,
                CultureInfo.CurrentCulture.Calendar,
                new TimeSpan(0)
                );
            DataProcessed = false;
            SequenceNumber = 0;
            Data1 = "";
            cData = ""; new Object();
            ciData = new DataObject();
        }
    }
}