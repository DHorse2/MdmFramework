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
using Mdm.Oss.Components;
using Mdm.World;
#endregion
#region Mdm WinUtil, System Shell32, WshRuntime
using Mdm.Oss.WinUtil;
using Mdm.Oss.WinUtil.Types;
//          add shell32.dll reference
//          or COM Microsoft Shell Controls and Automation
using Shell32;
//          At first, Project > Add Reference > COM > Windows ScriptItemPassed Host Object Model.
using IWshRuntimeLibrary;
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
    public class DoSerial : IXmlSerializable
    {
        // public DoSerial() { }
        public DoSerial(DoSerial t) { this.Value = t; }
        public DoSerial Value { get; set; }
        //

        XmlSchema IXmlSerializable.GetSchema()
        {
            return (null);
            // throw new NotImplementedException();
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (!reader.HasAttributes)
                throw new FormatException("expected a type attribute!");
            string type = reader.GetAttribute("type");
            reader.Read(); // consume the value
            if (type == "null") return;// leave T at default value

            //DataContractSerializer t = new DataContractSerializer();
            XmlSerializer serializer = new XmlSerializer(Type.GetType(type));
            this.Value = (DoSerial)serializer.Deserialize(reader);
            reader.ReadEndElement();
            // throw new NotImplementedException();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            Value = this;
            if (Value == null)
            {
                writer.WriteAttributeString("type", "null");
                return;
            }
            Type type = this.Value.GetType();
            XmlSerializer serializer = new XmlSerializer(type);
            writer.WriteAttributeString("type", type.AssemblyQualifiedName);
            //
            BinaryFormatter b = new BinaryFormatter();
            Stream ClipMetaMemStream = null;
            b.Serialize(ClipMetaMemStream, this);
            //
            // serializer.Serialize(writer, this.Value);
            // serializer.Serialize(writer, this.Value);
            // throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Clipboard Program Control
    /// </summary>
    /// <remarks>
    /// Counters and fields to control the history
    /// </remarks>
    public class ProgControlDef
    {
        public UInt32 IdKeyCurrent;
        public UInt32 SequenceNumber;
        public UInt32 ClipCount;
        DateTimeOffset DataCreationTime;
        //
        /// <summary>
        /// Program control with current id, sequence number.
        /// </summary> 
        public ProgControlDef()
        {
            ClipCount = 0;
            IdKeyCurrent = 0;
            SequenceNumber = 0;
            // DataCreationTime
        }
    }
}
