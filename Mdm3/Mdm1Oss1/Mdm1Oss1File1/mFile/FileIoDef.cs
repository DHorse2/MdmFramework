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
using Mdm.Oss.Decl;
using Mdm.Oss.Mobj;
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
    public class FileIoDef : IDisposable
    {
        // File Object and ItemData - dxxxxxxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.File mFile FileObjects
        // <Area Id = "FileObject">
        public Object DbFileObject;
        // System IO
        // System IO Stream
        public FileStream DbFileStreamObject;
        public StreamReader DbFileStreamReaderObject; // Stream Reader
        public StreamWriter DbFileStreamWriterObject; // Stream Writer
                                                      // System IO TextReader
        public TextReader DbFileTextReadObject; // Text Reader
        #endregion
        // FileSystem Object Common 
        // FileSystem Object Common  - xxxxxxxxxxxxxxxxxxxxx
        #region $include Mdm.Oss.File mFile FsoFileControlState (make a struct)
        // <Section Summary>
        // <Section Role="Declarations">
        // <Section Id = "MdmFileFileIo">
        // <Section Vs="MdmFileDbVs0_8_9">
        // <Area Id = "GeneralStatusCondition">
        //
        // <Area Id = "IoType">
        protected internal FileIo_ModeIs ipIoType;
        public FileIo_ModeIs IoMode
        {
            get { return ipIoType; }
            set { ipIoType = value; }
        }
        // <Area Id = "FileReadMode">

        protected internal string spFileActionDoName;
        public string ModeName
        {
            get { return spFileActionDoName; }
            set { spFileActionDoName = value; }
        }
        protected internal FileAction_ToDoIs ipToDo;
        public FileAction_ToDoIs ToDo
        {
            get { return ipToDo; }
            set
            {
                if (Enum.IsDefined(typeof(FileAction_ToDoIs), value))
                {
                    ipToDo = value;
                    spFileActionDoName = Enum.GetName(typeof(FileAction_ToDoIs), ipToDo);
                }
            }
        }
        // <Area Id = "FileActionTarget">
        protected internal FileAction_ToDoTargetIs ipToDoTarget;
        public FileAction_ToDoTargetIs ToDoTarget
        {
            get { return ipToDoTarget; }
            set { ipToDoTarget = value; }
        }
        // <Area Id = "FileAccessMode">
        protected internal FileAction_ToDoTargetIs ipFileAccessMode;
        public FileAction_ToDoTargetIs FileAccessMode
        {
            get { return ipFileAccessMode; }
            set { ipFileAccessMode = value; }
        }
        protected internal FileAction_ReadModeIs ipFileReadMode;
        public FileAction_ReadModeIs FileReadMode
        {
            get { return ipFileReadMode; }
            set { ipFileReadMode = value; }
        }
        // <Area Id = "FileWriteMode">
        protected internal FileAction_ReadModeIs ipFileWriteMode;
        public FileAction_ReadModeIs FileWriteMode
        {
            get { return ipFileWriteMode; }
            set { ipFileWriteMode = value; }
        }
        #endregion
        // 
        #region File Buffers
        protected internal String spIoReadBuffer;
        public String IoReadBuffer
        {
            get { return spIoReadBuffer; }
            set { spIoReadBuffer = value; }
        }

        protected internal String spIoBlock;
        public String IoBlock
        {
            get { return spIoBlock; }
            set { spIoBlock = value; }
        }

        protected internal String spIoLine;
        public String IoLine
        {
            get { return spIoLine; }
            set { spIoLine = value; }
        }

        protected internal String spIoAll;
        public String IoAll
        {
            get { return spIoAll; }
            set { spIoAll = value; }
        }
        #endregion

        #region Destructors
        // Track whether Dispose has been called.
        private bool disposed = false;
        private bool instantiated = false;

        ~FileIoDef()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (components != null) {
                //    components.Dispose();
                //}
                DataClear();
            }
            // base.Dispose(disposing);
            disposed = true;
        }
        #endregion

        public void DataClear()
        {
            // File Buffers
            spIoReadBuffer = "";
            spIoBlock = "";
            spIoLine = "";
            spIoAll = "";
            // FsoFileControlState
            ipIoType = 0;
            ipFileReadMode = 0;
            ipFileWriteMode = 0;
            ipFileAccessMode = 0;
            // FileObjects
            if (DbFileObject != null) { DbFileObject = null; }
            if (DbFileStreamObject != null) { DbFileStreamObject.Dispose(); }
            if (DbFileStreamReaderObject != null) { DbFileStreamReaderObject.Dispose(); }
            if (DbFileStreamWriterObject != null) { DbFileStreamWriterObject.Dispose(); }
        }

        public void CopyTo(ref FileIoDef FileIoPassed)
        {
            // File Buffers
            FileIoPassed.spIoReadBuffer = spIoReadBuffer;
            FileIoPassed.spIoBlock = spIoBlock;
            FileIoPassed.spIoLine = spIoLine;
            FileIoPassed.spIoAll = spIoAll;
            // FsoFileControlState
            FileIoPassed.ipIoType = ipIoType;
            FileIoPassed.ipFileReadMode = ipFileReadMode;
            FileIoPassed.ipFileWriteMode = ipFileWriteMode;
            FileIoPassed.ipFileAccessMode = ipFileAccessMode;
            // FileObjects
            if (DbFileObject != null) { DbFileObject = null; }
            if (DbFileStreamObject != null) { DbFileStreamObject.Dispose(); }
            if (DbFileStreamReaderObject != null) { DbFileStreamReaderObject.Dispose(); }
            if (DbFileStreamWriterObject != null) { DbFileStreamWriterObject.Dispose(); }
            DbFileStreamObject = null;
            DbFileStreamReaderObject = null;
            DbFileStreamWriterObject = null;
        }

        public FileIoDef()
        {
        }
    }
}
