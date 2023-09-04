using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mdm.Oss.Decl;
using Mdm.Oss.File.Control;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.File;
using Mdm.Oss.File.RunControl;
using Mdm.Oss.File.Type;

namespace Mdm.Oss.File.RunControl
{
    /// <summary>
    /// <para> File Command Class</para>
    /// <para> involving column data retrieved
    /// or created for the the database file.</para>
    /// <para> This differs from a File Action in
    /// that column data is included with the object.
    /// It includes column ordering and unique ID 
    /// information along with requested columns.</para>
    /// </summary>
    /// <remarks>
    /// <para>This class is not currently implemented.  It
    /// will be implement when the multithreaded code and
    /// property system are implemented.</para>
    /// <para> . </para>
    /// <para>Unique Id must be part of the select and returned
    /// Although it may not be part of the data requested
    /// it must be saved in the list in order to susequently
    /// reselect a unique record from the database.</para>
    /// </remarks>
    public class mFileCommand
    {
        public String ActionName;
        public Int32 ActionId;
        public FileSummaryDef FileSummary;
        public Int32 FieldCount;
        // basic sorted select
        public String Verb;
        public List<String> FieldName;
        public List<Int32> FieldIndex;
        public List<Int32> OrderUsed;
        // Unique Id must be part of the select and returned
        // Although it may not be part of the data requested
        // it must be saved in the list in order to susequently
        // reselect a unique record from the database.
        public List<Int32> UniqueIdUsedInVerb;
        public List<Int32> UniqueIdSequence;
        //
        //
        public mFileCommand()
        {
            ActionName = "";
            ActionId = 0;
            // Fs = new FileSummaryDef();
            FieldCount = 0;
            //
            FieldName = new List<String>();
            FieldIndex = new List<Int32>();
            OrderUsed = new List<Int32>();
            UniqueIdSequence = new List<Int32>();
            UniqueIdUsedInVerb = new List<Int32>();

        }
    }
    #region ActionInfoDef - Mdm.Oss.File mFile SourceFileAction ($include)
    /// <summary>
    ///  <para> System control object for Actions
    /// this would be aggregated with the 
    /// Run Action, File Do, and File Action objects
    /// in the case of logging, tracing and system event history.
    /// Also used in exceptions data collection. </para> 
    ///  <para> . </para> 
    ///  <para> Note 1: A Core Object (strongly typed) Verb 
    /// executes and acts on the Object and Target 
    /// to generate a Result and/or Result Object. </para> 
    ///  <para> Note 2: Applications fill this information in
    /// typically to match the internal implementation
    /// used in the application. </para> 
    /// </summary> 
    public class ActionInfoDef
    {
        //
        // CORE BEHAVIOR SECTION
        // OBJECT, TARGET, Result, VERB
        /// <summary>
        /// </summary>
        public string omvOfObject;
        /// <summary>
        /// </summary>
        public string omvOfTarget;
        /// <summary>
        /// </summary>
        public StateIs omvOfResult;
        /// <summary>
        /// </summary>
        public StateIs omvOfExistStatus;
        /// <summary>
        /// </summary>
        public string omvOfVerb;
        // omvOfVerb  = omvOfObject + omvOfTarget + omvOfResult;
        // Working Variables
        /// <summary>
        /// </summary>
        public String ResultName;
        /// <summary>
        /// </summary>
        public StateIs Result;
        /// <summary>
        /// </summary>
        public String ResultDescription;

        /// <summary>
        /// </summary>
        public ActionInfoDef()
        {
        }
    }
    /// <summary>
    /// <para>
    /// FileActionDef - A File Action to be taken on either the
    /// primary or auxillary streams.</para>
    /// <para> . </para>
    /// <para> File Actions include:</para>
    /// <para> 1) ListGet</para>
    /// <para> 2) Check</para>
    /// <para> 3) Open</para>
    /// <para> 4) Close</para>
    /// </summary> 
    public class FileActionDef
    {
        #region Declarations
        // <Area Id = "MdmStandardFileInformation">
        public mFileDef FileObject;
        public mFileMainDef FmainObject;
        // <Area Id = "SourceDetailsProperties">
        // <Area Id = "IoActionBeingPerformed">
        // <Area Id = "SourceFileAction">
        public ActionInfoDef ActionInfo;

        protected internal FileAction_ToDoIs ipToDo;
        /// <summary>
        /// The File Action to perform.
        /// For example GetList, CheckList, Open, Close...
        /// </summary> 
        public FileAction_ToDoIs ToDo
        {
            get { return ipToDo; }
            set
            {
                if (Enum.IsDefined(typeof(FileAction_ToDoIs), value))
                {
                    ipToDo = value;
                    spName = Enum.GetName(typeof(FileAction_ToDoIs), ipToDo);
                }
            }
        }
        protected internal String spName;
        /// <summary>
        /// The text name of the File Action.
        /// </summary> 
        public String Name
        {
            get { return spName; }
            set { spName = value; }
        }
        protected internal String spKeyName;
        /// <summary>
        /// The id of File Action Transaction
        /// </summary> 
        public String KeyName
        {
            get { return spKeyName; }
            set { spKeyName = value; }
        }
        // public String Result;
        protected internal StateIs ipResult;
        public StateIs Result
        {
            get { return ipResult; }
            set
            {
                ipResult = value;
                if (Enum.IsDefined(typeof(StateIs), value))
                {
                    spResultName = Enum.GetName(typeof(StateIs), ipResult);
                }
                else { spResultName = "Invalid Result"; }
            }
        }
        protected internal String spResultName;
        public String ResultName
        {
            get { return spResultName; }
            set { spResultName = value; }
        }
        //
        public object ResultObject;
        //
        protected internal String spDirectionName;
        public String DirectionName
        {
            get { return spDirectionName; }
            set { spDirectionName = value; }
        }
        // direction
        protected internal FileAction_DirectionIs ipDirection;
        public FileAction_DirectionIs Direction
        {
            get { return ipDirection; }
            set
            {
                if (Enum.IsDefined(typeof(FileAction_DirectionIs), value))
                {
                    ipDirection = value;
                    spDirectionName = Enum.GetName(typeof(FileAction_DirectionIs), ipDirection);
                }
            }
        }
        //
        protected internal String spModeName;
        public String ModeName
        {
            get { return spModeName; }
            set { spModeName = value; }
        }
        //protected internal FileAction_ToDoIs ipFileActionDo;
        //public FileAction_ToDoIs FileActionDo
        //{
        //    get { return ipFileActionDo; }
        //    set
        //    {
        //        if (Enum.IsDefined(typeof(FileAction_ToDoIs), value))
        //        {
        //            ipFileActionDo = value;
        //            spModeName = Enum.GetName(typeof(FileAction_ToDoIs), ipFileActionDo);
        //        }
        //    }
        //}
        //
        // <Area Id = "IoType">
        protected internal FileIo_ModeIs ipIoType;
        public FileIo_ModeIs IoMode
        {
            get
            {
                return ipIoType;
            }
            set
            {
                ipIoType = value;
                spModeName = ipIoType.ToString();
            }
        }
        public StateIs IoTypeResult;

        // <Area Id = "FileActionTarget">
        protected internal FileAction_ToDoTargetIs ipFileActionTarget;
        public FileAction_ToDoTargetIs ToDoTarget
        {
            get { return ipFileActionTarget; }
            set { ipFileActionTarget = value; }
        }
        // <Area Id = "FileAccessMode">
        protected internal FileAction_ToDoTargetIs ipFileAccessMode;
        public FileAction_ToDoTargetIs FileAccessMode
        {
            get { return ipFileAccessMode; }
            set { ipFileAccessMode = value; }
        }

        public FileAction_ReadModeIs FileReadMode; // not a state
        public FileAction_ReadModeIs FileWriteMode; // not a state

        public bool DoRetry;

        public bool DoClearTarget;

        public bool DoGetUiVs;

        // <Area Id = "SourceFileActionItFlags">
        /*
         //   0    - Null
         //   1    - DoesExist
         //   2    - Create
         //   4    - Open
         //   8    - Close
         //   16   - Delete
         //   32   - Empty (Delete All)
         //   64   - Shrink
         //   128  - Expand
         //   256  - Lock
         //   512  - Unlock
         //   1024 - Defragment
         //   2048 - bRead Only
         //   4096 - Rebuild
         //   8192 - Rebuild Statistics
         //   16384 - x
         //   32768 - x
         //   65536 - x
         */
        public List<Object> FileException;
        #endregion

        public void FileExceptionClear() { FileException = new List<Object>(); }

        /// <summary>
        /// Main Method - Create a File Action associated with the passed file.
        /// </summary> 
        public FileActionDef(ref mFileDef mFileDefPassed)
            : this()
        {
            FileObject = mFileDefPassed;
        }

        /// <summary>
        /// Default File Action not (yet) associated with a file handler.
        /// </summary> 
        public FileActionDef()
        {
            FileObject = null;
            IoMode = FileIo_ModeIs.None;
            ToDo = FileAction_ToDoIs.Undefined;
            Name = "";
            //FileActionDo = FileAction_ToDoIs.NotSet;
            IoTypeResult = StateIs.NotSet;
            Result = StateIs.NotSet;
            ResultName = "";
            ResultObject = new Object();
            DoRetry = false;
            DoClearTarget = false;
            DoGetUiVs = false;
            ActionInfo = new ActionInfoDef();
            FileException = new List<Object>();
        }

        /// <summary>
        /// Copy the File Action to the Passed Target.
        /// </summary> 
        public void CopyTo(ref FileActionDef FaPassed)
        {
            FaPassed.FileObject = FileObject;
            FaPassed.ToDo = ToDo;
            FaPassed.ToDoTarget = ToDoTarget;
            FaPassed.Name = Name;
            FaPassed.IoTypeResult = IoTypeResult;
            FaPassed.Result = Result;
            FaPassed.ResultName = ResultName;
            FaPassed.ResultObject = ResultObject;
            FaPassed.DoRetry = DoRetry;
            FaPassed.DoClearTarget = DoClearTarget;
            FaPassed.DoGetUiVs = DoGetUiVs;
            FaPassed.ActionInfo = ActionInfo;
            FaPassed.FileException = FileException;
        }

        /// <summary>
        /// Copy the File Action from the Passed Souce.
        /// </summary> 
        public void CopyFrom(ref FileActionDef FaPassed)
        {
            FileObject = FaPassed.FileObject;
            ToDo = FaPassed.ToDo;
            Name = FaPassed.Name;
            ToDoTarget = FaPassed.ToDoTarget;
            IoTypeResult = FaPassed.IoTypeResult;
            Result = FaPassed.Result;
            ResultName = FaPassed.ResultName;
            ResultObject = FaPassed.ResultObject;
            DoRetry = FaPassed.DoRetry;
            DoClearTarget = FaPassed.DoClearTarget;
            DoGetUiVs = FaPassed.DoGetUiVs;
            ActionInfo = FaPassed.ActionInfo;
            FileException = FaPassed.FileException;
        }

    }
    #endregion
}
