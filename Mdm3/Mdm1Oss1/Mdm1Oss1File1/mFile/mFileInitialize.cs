#region Dependencies
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
#region System Serialization (Runtime and Xml)
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
#endregion

#region  Mdm Core
using Mdm.Oss;
using Mdm.Oss.Console;
using Mdm.Oss.Decl;
using Mdm.Oss.Run.Control;
using Mdm.Oss.Std;
using Mdm.Oss.Sys;
using Mdm.Oss.Thread;
using Mdm.Oss.Components;
using Mdm.World;
#endregion
#region  Mdm MVC Mobject
using Mdm.Oss.Mobj;
#endregion
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
using Mdm.Oss.File.Type.Pick;
using Mdm.Oss.File.Type.Sql;
using System.Windows.Forms;
//using Mdm.Oss.File.Type.Srt.Script;
#endregion
#endregion

namespace Mdm.Oss.File
{
    public partial class mFileDef : Mobject, ImFileType, IDisposable
    {
        #region Class Methods
        #region Class Initialization
        // $Section oAssembly oClass Main_FileProcessing // xxxxxxxxxx
        #region Constructors
        /// <summary> 
        /// Default constructor would essentially own itself and
        /// operate as a controller.
        /// </summary> 
        /// <param name="SenderPassed">The sending object</param> 
        /// <param name="stPassed">The Run File Console object</param> 
        /// <param name="ConsoleSourcePassed">Location of Console object</param> 
        /// <param name="ClassRolePassed">Role played by the class</param> 
        /// <param name="ClassFeaturesPassed">Features used by the class</param> 
        public mFileDef(ref object SenderPassed, ref StdConsoleManagerDef stPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ref SenderPassed, ref stPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        {
            //st = base.st as StdConsoleManagerDef;
            FileState = new mFileStateDef();
            FileState.mFileResult = StateIs.Started;
            FileObject = this;
            XUomMovvXv = SenderPassed as Mobject;
            this.InitializeMFile(); // Disconnect this? ToDo
        }
        public mFileDef(ref object SenderPassed, ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
             : base(ref SenderPassed, ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        {
            FileState = new mFileStateDef();
            FileState.mFileResult = StateIs.Started;
            FileObject = this;
            XUomMovvXv = SenderPassed as Mobject;
            this.InitializeMFile(); // or this:
        }
        public mFileDef(ConsoleSourceIs ConsoleSourcePassed, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
            : base(ConsoleSourcePassed, ClassRolePassed, ClassFeaturesPassed)
        {
            FileState = new mFileStateDef();
            FileState.mFileResult = StateIs.Started;
            FileObject = this;
            this.InitializeMFile(); // Disconnect this? ToDo
        }
        /// <summary>
        /// Section Id = "Constructor"
        /// Creates a file object associated with the passed Mobject.
        /// This effectively gives ownership of the file to PassedOb.
        /// </summary> 
        /// <param name="SenderPassed">Parent or owner Mobject</param> 
        /// <param name="stPassed">The Run File Console object</param> 
        /// <param name="ClassUses.RoleAsUtility">File (Objects) are utility objects</param> 
        public mFileDef(
            ref object SenderPassed,
            ref StdConsoleManagerDef stPassed
            )
            : base(ref SenderPassed, ref stPassed, ConsoleSourceIs.Interface, stPassed.ClassRole, stPassed.ClassFeatures)
        {
            FileState = new mFileStateDef();
            FileState.mFileResult = StateIs.Started;
            XUomMovvXv = SenderPassed as Mobject;
            #region Initialize Console
            ConsoleSource = ConsoleSourceIs.Interface;
            if (stPassed != null)
            {
                st = stPassed as StdConsoleManagerDef;
                ClassRole = st.ClassRole;
                ClassFeatures = st.ClassFeatures;
            } else
            {
                ConsoleComponentCreate();
            }
            // st.StdConsoleSet(st, st.ConsoleSource, st.ClassRole, st.ClassFeatures);
            #endregion
            this.InitializeMFile();
        }
        /// <summary>
        /// Creates a file for "Direction" (i.e. Input, Output, Both)
        /// whose object is associated with the passed Mobject.
        /// This effectively gives ownership of the file to PassedOb.
        /// </summary> 
        /// <param name="SenderPassed">Parent or owner Mobject</param> 
        /// <param name="DirectionPassed">Direction of File IO for this file.</param> 
        /// <param name="ClassUses.RoleAsUtility">File (Objects) are utility objects</param> 
        public mFileDef(ref object SenderPassed, FileAction_DirectionIs DirectionPassed)
             : base(ref SenderPassed, ConsoleSourceIs.Self, ClassRoleIs.RoleAsUtility, ClassFeatureIs.MdmUtilConsole)
        {
            FileState = new mFileStateDef();
            FileState.mFileResult = StateIs.Started;
            XUomMovvXv = SenderPassed as Mobject;
            this.InitializeMFile();
        }
        /// <summary> 
        /// Default constructor would essentially own itself and
        /// operate as a controller.
        /// </summary> 
        /// <param name="ClassUses.RoleAsUtility">File (Objects) are utility objects</param> 
        public mFileDef()
             : base(ConsoleSourceIs.None, ClassRoleIs.RoleAsUtility, ClassFeatureIs.MdmUtilTrace)
        {
            FileState = new mFileStateDef();
            FileState.mFileResult = StateIs.Started;
            XUomMovvXv = this;
            this.InitializeMFile();
        }
        #endregion
        #region Initialize
        public virtual void InitializeMFile()
        {
            FileState = new mFileStateDef();
            FileState.mFileResult = StateIs.Started;
            FileObject = this;
            if (!ClassFeatureFlag.InitializeFile)
            {
                ClassFeatureFlag.InitializeFile = true;
                base.InitializeStdBaseRunFileConsole();
                InitializemFile(FileAction_DirectionIs.None);
            }
            FileState.mFileResult = StateIs.Initialized;
        }
        /// <summary> 
        /// Standard initialize after constructors.
        /// Create Meta and System data, initializes fields.
        /// Create Primary and Auxillary File Stream objects.
        /// Create Utility, management and state objects.
        /// Create ScriptItemPassed Syntax objects.
        /// </summary> 
        /// <param name="DirectionPassed">Expected direction of IO data flow (Input, Output, Both).</param> 
        public void InitializemFile(FileAction_DirectionIs DirectionPassed)
        {
            ClassFeatureFlag.InitializeFile = true;
            if (XUomMovvXv != null) { ClassFeaturesSet(XUomMovvXv.ClassFeaturesGet()); }
            // or type of (Mobject)
            //
            ClassRole = ClassRoleIs.RoleAsUtility;
            //
            // this isn't perfect but can be tweaked when needed.
            ClassFeatures =
            (ClassFeatureIs)(ClassFeatureIs.MaskUi
            | ClassFeatureIs.MaskButton
            | ClassFeatureIs.MaskStautsUiAsBox
            | ClassFeatureIs.MdmUtilConsole);
            ClassFeaturesSet(ClassFeatures);

            #region Initialize
            Meta = new MetaDef();
            Sys = new SysDef();
            LocalMessage = new LocalMsgDef();
            //
            DbFileTemp = new DbFileTempDef();
            //
            FileObject = this;
            if (XUomMovvXv == null)
            {
                Sender = this;
                XUomMovvXv = new Mobject(ref Sender, ref st, ConsoleSourceIs.None, ClassRoleIs.RoleAsUtility, ClassFeatureIs.MdmUtilConsole); // Mapplication();
            }
            // Location Counter / Values
            //TraceMdmCounterLevel1Get = CharMaxIndexGet;
            //TraceMdmCounterLevel2Get = AttrIndexGet;
            // Delegates;
            //StatusUi.WasCreatedBy = "mFile";
            //StatusUi.BoxDelegatesCopyFrom(ref XUomMovvXv.StatusLine);
            //StatusUi.BoxObjectsCopyFrom(ref XUomMovvXv.StatusLine);
            //StatusUi.WasCreatedBy += " and copy from XUomMovvXv";
            //StatusUi.TextConsoleBox.Text = "Hey Dave";
            // Infinitie Loop control
            FileState.DoingDefaults = false;
            FileState.ObjectListLoading = false;
            // Primary and Auxillary File Objects
            Fmain = new mFileMainDef(FileObject, DirectionPassed, "Primary");
            FileSqlConn = Fmain.FileSqlConn;
            Faux = new mFileMainDef(FileObject, DirectionPassed, "Auxillary");
            //
            //#region Phrase Construction
            //DbSyn = new DbSynDef();
            //DbMasterSyn = new DbMasterSynDef();
            //#endregion
            //mFileSqlConnectionDef mFileSqlConnection;
            //note:
            //if (FileSqlConn == null)
            //{
            //    mFileSqlConnection = new mFileSqlConnectionDef();
            //    FileSqlConn = Fmain.FileSqlConn = mFileSqlConnection;
            //}
            #region Pick Specific Object
            PickRow = new PickRowDef();
            PickDictItem = new PickDictItemDef();
            if (PickDictIndex == null) { PickDictIndex = new PickDictIndexDef(); }
            //PickDictIndex = new StdDictIndexDef<StdDictItemDef>(); // ToDo
            PickDictIndex.Add("ToDo", PickDictItem);
            PickCol = new PickColDef();
            #endregion
            //
            // Initialize Fields
            //
            IterationFirst = bYES;
            MethodIterationFirst = bYES;
            Fmain.ColTrans.sGetResultNotSupported = sEmpty;
            Faux.ColTrans.sGetResultNotSupported = sEmpty;

            Fmain.FileStatus.bpIsInitialized = false;
            Fmain.FileStatus.bpIsInitialized = false;
            PickRow.sdIndexAliasLow = PickRowDef.sdIndexMaxNew;
            PickRow.sdIndex = 0;
            TableNameClear(ref Fmain);
            FileNameClear(ref Fmain);
            ItemIdClear(ref Fmain);
            Fmain.ColIndex.CharsPassedIn = new char[40];
            Fmain.ColIndex.CharsPassedIn = ("/,:*#?\"<>|.,\\';|][{}=+-()*&^%#@!`~ };_").ToCharArray();
            // Fmain.ColIndex.CharsPassedIn = "/,:*#?\"<>|.,\\';|][{}=+-()*&^%#@!`~ };_";
            // ToDo CharsPassedIn
            Fmain.ColIndex.CharsPassedOut = new char[40];
            Fmain.ColIndex.CharsPassedOut = ("________________________________________").ToCharArray();
            // Fmain.ColIndex.CharsPassedIn = "________________________________________";
            #endregion
            #region FileInputBuffers Set
            Fmain.Buf.LineBuffer = sEmpty;
            Fmain.Buf.NewItem = sEmpty;
            Fmain.Item.ItemData = sEmpty;
            Fmain.Buf.CharIndex = 1;
            Fmain.Buf.CharMaxIndex = 0;
            Fmain.Buf.CharCounter = 0;
            // String[] sNewItem = sEmpty;     //  sNewItem=sEmpty
            // String[] InFile.Fmain.Item.ItemData = sEmpty;      
            //  InFile.Fmain.Item.ItemData=sEmpty
            // File Bulk Character Conversion (Function)
            Fmain.Buf.ConvertableItem = sEmpty;
            Fmain.Buf.ItemConvertFlag = 0; // ToDo z$RelVs3 Refine these Import options mFileInitialize
            #endregion
            // ConsoleMdmInitialize();
        }
        #endregion
        #region Initialize Class / Features
        private StateIs ConsoleComponentCreate()
        {
            //this.Initialize();
            StateIs TempStatus = InitializeConsoleComponent();
            st = new StdConsoleManagerDef(ConsoleSource, ClassRole, ClassFeatures)
            { ConsoleVerbosity = 7 };
            // st.ClassFeaturesFlagsSet(ClassFeaturesIsThis);
            return StateIs.Finished;
        }
        private StateIs InitializeConsoleComponent()
        {
            #region Framework Features Used
            ClassRole = ClassRoleIs.RoleAsUtility;
            // this isn't perfect but can be tweaked when needed. (defaults?)
            ClassFeatures =
            ClassFeatureIs.MaskUi
            | ClassFeatureIs.MaskButton
            | ClassFeatureIs.MaskStautsUiAsBox
            | ClassFeatureIs.Window
            | ClassFeatureIs.MdmUtilConsole;
            //
            //st = new StdConsoleManagerDef(ConsoleSource, ClassRole, ClassFeatures)
            //{ ConsoleVerbosity = 7 };
            st.ClassFeaturesFlagsSet(ConsoleSource, ClassRole, ClassFeatures);
            #endregion
            // Form Controls are null.
            // They do a lazy load when the user decides to
            // display any given State (Status) DataGridView.
            //
            // InitializeComponentForm();
            return StateIs.Finished;
        }
        public void ClassFeaturesDefaultsGet(object SenderPassed, ConsoleSourceIs SenderObjectSource, ClassRoleIs ClassRolePassed, ClassFeatureIs ClassFeaturesPassed)
        {
            if (XUomMovvXv != null) { ClassFeaturesSet(XUomMovvXv.ClassFeaturesGet()); }
            else
            {
                // or type of (Mobject)
                // ToDo. This can't be correct. It's passed.
                ClassRole = ClassRoleIs.RoleAsUtility;
                //
                // this isn't perfect but can be tweaked when needed.
                ClassFeatures =
                (ClassFeatureIs)(ClassFeatureIs.MaskUi
                | ClassFeatureIs.MaskButton
                | ClassFeatureIs.MaskStautsUiAsBox
                | ClassFeatureIs.MdmUtilConsole);
                ClassFeaturesSet(ClassFeatures);
            }
        }
        #endregion
        #region AppObjects
        /// <summary> 
        /// Not Used - Get the file object (this)
        /// </summary> 
        public StateIs AppmFileObjectGetTo(ref mFileMainDef FmainPassed)
        {
            FileState.AppmFileObjectGet = StateIs.Started;
            FmainPassed.FileObject = this;
            FmainPassed.Fs.FileObject = this;
            FileState.AppmFileObjectGet = StateIs.Successful;
            return FileState.AppmFileObjectGet;
        }
        /// <summary> 
        /// Not Used - Set the Primary File Stream object Fmain (this)
        /// </summary> 
        public StateIs AppmFileObjectSetFrom(ref mFileMainDef FmainPassed)
        {
            FileState.AppmFileObjectSet = StateIs.Started;
            // This is not a valid action on
            FileObject = this;
            Fmain = FmainPassed;
            FileState.AppmFileObjectSet = StateIs.Successful;
            return FileState.AppmFileObjectSet;
        }
        #endregion
        #region $include Mdm.Oss.FileUtil mFile Main_FileProcessing
        // <Section Id = "MainFileProcessing">
        /// <summary> 
        /// Main Processing Loop method.
        /// Virtual method overriden in derived class
        /// but could be delegate based.
        /// It would use a standard Action delegate.
        /// </summary> 
        public virtual StateIs MainFileProcessing()
        {
            FileState.MainFileProcessingResult = StateIs.Started;
            //
            FileState.MainFileProcessingResult = StateIs.Successful;
            return FileState.MainFileProcessingResult;
        }
        #endregion
        #region TextFile
        // <Section Id = "TextFileCheckDoesExist">
        /// <summary> 
        /// Returns a bool indicating if the Text File Exists.
        /// </summary> 
        public bool TextFileDoesExist(ref mFileMainDef FmainPassed)
        {
            FileState.TextFileDoesExistResult = TextFileCheckDoesExist(ref FmainPassed);
            if (FileState.TextFileDoesExistResult == StateIs.DoesExist)
            {
                return true;
            }
            else if (FileState.TextFileDoesExistResult == StateIs.DoesNotExist)
            {
                return false;
            }
            return false;
        }
        // <Section Id = "TextFileCheckDoesExist">
        /// <summary> 
        /// Check if the Text File Exists.
        /// </summary> 
        public StateIs TextFileCheckDoesExist(ref mFileMainDef FmainPassed)
        {
            FileState.TextFileDoesExistResult = StateIs.Started;
            FmainPassed.FileStatus.bpDoesExist = false;
            switch (FmainPassed.Fs.FileIo.IoMode)
            {
                case (FileIo_ModeIs.Sql):
                    return StateIs.UnknownFailure;
                case (FileIo_ModeIs.Line):
                case (FileIo_ModeIs.All):
                    // xxx full file name parsing
                    if (FmainPassed.Fs.FileId.FileNameLine.Length == 0)
                    {
                        FmainPassed.Fs.FileId.FileNameLine = FileNameLineBuild(ref FmainPassed);
                    }
                    FmainPassed.FileStatus.bpDoesExist = System.IO.File.Exists(FmainPassed.Fs.FileId.FileNameLine);
                    break;
                default:
                    FmainPassed.FileStatus.ipDoesExistResult = StateIs.NotSet;
                    LocalMessage.ErrorMsg = "File Read IoType (" + FmainPassed.Fs.FileIo.IoMode.ToString() + ") is not set";
                    //((ImTrace)ConsoleSender).TraceMdmDoImpl(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PickSystemCallStringResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                    //throw new NotSupportedException(LocalMessage.ErrorMsg);
                    ExceptNotSupportedImpl(FmainPassed, ExceptionNotSupported, LocalMessage.ErrorMsg, FileState.FileWriteResult);
                    FmainPassed.FileStatus.bpDoesExist = false;
                    break;
            }
            if (FmainPassed.FileStatus.bpDoesExist)
            {
                FileState.TextFileDoesExistResult = StateIs.DoesExist;
            }
            else
            {
                FileState.TextFileDoesExistResult = StateIs.
                    DoesNotExist;
            }
            FmainPassed.FileStatus.ipDoesExistResult = FileState.TextFileDoesExistResult;
            return FileState.TextFileDoesExistResult;
        }
        // <Section Id = "x
        /// <summary> 
        /// Open the Text File.
        /// </summary> 
        public StateIs TextFileOpen(ref mFileMainDef FmainPassed)
        {
            FileState.TextFileOpenResult = StateIs.Started;
            Fmain.Buf.BytesRead = 0;
            Fmain.Buf.BytesReadTotal = 0;
            Fmain.Buf.BytesConverted = 0;
            Fmain.Buf.BytesConvertedTotal = 0;
            FileState.TextFileOpenResult = TableOpen(ref FmainPassed);
            return FileState.TextFileOpenResult;
        }
        // <Section Id = "TextFileClose">
        /// <summary> 
        /// Close the Text File.
        /// </summary> 
        public StateIs TextFileClose(ref mFileMainDef FmainPassed)
        {
            FileState.TextFileCloseResult = StateIs.Started;
            // FileClose(Fs.FileId.FileName);
            // <Area Id = "close the file streams
            try
            {
                if (Fmain.Fs.FileIo.DbFileStreamReaderObject != null)
                {
                    Fmain.Fs.FileIo.DbFileStreamReaderObject.Close();
                }
                if (Fmain.Fs.FileIo.DbFileStreamObject != null)
                {
                    Fmain.Fs.FileIo.DbFileStreamObject.Close();
                }
                //close the file
                if (Fmain.Fs.FileIo.DbFileObject != null)
                {
                    FileState.TextFileCloseResult = AsciiFileClear(ref FmainPassed);
                    // <Area Id = "do destructor;
                }
                FileState.TextFileCloseResult = StateIs.Successful;
            }
            catch (SqlException ExceptionSql)
            {
                LocalMessage.ErrorMsg = sEmpty;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.TextFileCloseResult);
                ExceptTableOpenError(ref FmainPassed, ref ExceptionSql);
                FileState.TextFileCloseResult = StateIs.DatabaseError;
                // Exit Here
            }
            catch (Exception ExceptionGeneral)
            {
                LocalMessage.ErrorMsg = sEmpty;
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.TextFileCloseResult);
                ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                FileState.TextFileCloseResult = StateIs.Failed;
            }
            finally {; }
            //
            FileState.TextFileCloseResult = StateIs.Successful;
            return FileState.TextFileCloseResult;
        }
        // <Section Id = "TextFileWrite">
        /// <summary> 
        /// Write data to the Text File.
        /// </summary> 
        public StateIs TextFileWrite(ref mFileMainDef FmainPassed)
        {
            FileState.TextFileWriteResult = StateIs.Started;
            // FileWrite(FileName);
            FileState.TextFileWriteResult = StateIs.UnknownFailure;
            return FileState.TextFileWriteResult;
        }
        // <Section Id = "TextFileReset">
        /// <summary> 
        /// Reset the Text File Object.
        /// </summary> 
        public StateIs TextFileReset(ref mFileMainDef FmainPassed)
        {
            FileState.TextFileResetResult = StateIs.Started;
            // if (Fmain.FileStatus.bpIsInitialized) {
            // THIS IS A DISPOSE FUNCTION
            Fmain.FileStatus.bpIsInitialized = false;
            // }
            return FileState.TextFileResetResult;
        }
        // <Section Id = "TextFileCreate">
        /// <summary> 
        /// Create the Text File.
        /// </summary> 
        public StateIs TextFileCreate(ref mFileMainDef FmainPassed)
        {
            FileState.TextFileCreateResult = StateIs.Started;
            FileState.TextFileCreateResult = StateIs.InProgress;
            try
            {
                FmainPassed.Fs.FileIo.DbFileStreamObject = System.IO.File.Create(FmainPassed.Fs.FileId.FileName);
                FmainPassed.FileStatus.bpDoesExist = true;
                FileState.TextFileCreateResult = StateIs.Successful;
            }
            catch (SqlException ExceptionSql)
            {
                LocalMessage.ErrorMsg = sEmpty;
                ExceptSqlImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionSql, FileState.TextFileCreateResult);
                FileState.TextFileCreateResult = StateIs.ShouldNotExist;
                ExceptTableOpenError(ref FmainPassed, ref ExceptionSql);
                Fmain.FileStatus.bpDoesExist = false;
                // Exit Here
            }
            catch (Exception ExceptionGeneral)
            {
                ExceptGeneralFileImpl(ref FmainPassed, LocalMessage.ErrorMsg, ref ExceptionGeneral, FileState.TextFileCreateResult);
                ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
                FmainPassed.FileStatus.bpDoesExist = false;
                FileState.TextFileCreateResult = StateIs.OsError;
            }
            finally
            {
                FmainPassed.FileStatus.Status = FileState.TextFileCreateResult;
                if (FileState.TextFileCreateResult != StateIs.Successful)
                {
                    Fmain.Fs.FileIo.DbFileStreamObject = null;
                }
            }
            return FileState.TextFileCreateResult;
        }
        // <Section Id = "TextFileDelete">
        /// <summary> 
        /// Delete the Text File.
        /// </summary> 
        public StateIs TextFileDelete(ref mFileMainDef FmainPassed)
        {
            FileState.TextFileDeleteResult = StateIs.Started;
            // FileDelete(FileName);
            FileState.TextFileDeleteResult = StateIs.UnknownFailure;
            return FileState.TextFileDeleteResult;
        }
        // <Section Id = "TextFileProcessMain">
        /// <summary> 
        /// Main Method (virtual) for Processing Text File.
        /// </summary> 
        public virtual StateIs TextFileProcessMain(String PassedFileNameRequest)
        {
            LocalId.LongResult = StateIs.Started;
            // <Area Id = "The files used here were created in the code example
            // in How to: Write to a Text File. You can of course substitute
            // other files of your own.

            // Example #1
            // bRead the file as one string.
            String IoLineAll = System.IO.File.ReadAllText(StdBaseDef.DriveOs + @"\Users\Public\TestFolder\WriteText.txt");

            // <Area Id = "Display the file contents to the Console.
            LocalMessage.Msg = "Contents of writeText.txt : " + IoLineAll;
            //MessageMdmSendToPage(Sender, LocalMessage.Msg);
            // Example #2
            // <Area Id = "bRead the file aslines into a String array.
            String[] LinesArray = System.IO.File.ReadAllLines(StdBaseDef.DriveOs + @"\Users\Public\TestFolder\WriteLines2.txt");

            //MessageMdmSendToPage(Sender, "Contents of writeLines2.txt: ");
            foreach (String sLine in LinesArray)
            {
                //MessageMdmSendToPage(Sender, "\t" + sLine);
            }
            //MessageMdmSendToPage(Sender, "C");

            // <Area Id = "Keep the Console window open in debug mode.
            System.Console.ReadKey();
            //MessageMdmSendToPage(Sender, "C");

            return LocalId.LongResult;
        }
        #endregion
        #region FileDataReset
        // FileDataReset
        /// <summary> 
        /// FileDataReset should reset all of Fmain...
        /// </summary> 
        public StateIs FileDataReset(ref mFileMainDef FmainPassed)
        {
            PickState.PickDataResetResult = StateIs.Started;
            //
            FmainPassed.Item.ItemId = sEmpty; // Reset
            if (IterationFirst)
            {
                FmainPassed.Fs.ItemIdCurrent = sEmpty; // Reset
                FmainPassed.Fs.ItemIdNext = sEmpty; // Reset
            }
            // ToDo FileDataReset Reset Other Item ItemData
            // Item ItemData
            FmainPassed.Item.ItemData = sEmpty;
            // FileDataReset
            return PickState.PickDataResetResult;
        }
        #endregion
        #region DictResetDataReset
        // ToDo ColIndexReset needs work
        /// <summary> 
        /// Reset the Column Index Data.
        /// </summary> 
        /// <param name="ColIndexPassed"></param> 
        public StateIs ColIndexReset(ref ColIndexDef ColIndexPassed)
        {
            PickState.PickDictResetResult = StateIs.Started;
            // Index to field within a dict item
            ColIndexPassed.ColIndex = 0; // Pick Dict Reset
            ColIndexPassed.ColIndexTotal = 0; // Pick Dict Reset
                                              // Info about this dict item;
            ColIndexPassed.ColCount = 0; // Pick Dict Reset
            ColIndexPassed.ColCountTotal = 0;

            ColIndexPassed.ColInvalid = 0;
            // ColIndexPassed.ColInvalid = 0;
            // ToDo Reset Other Item Dict ItemData
            ColIndexPassed.ColAttrIndex = 0;
            ColIndexPassed.ColAttrCount = 0; // ItemData Items in Item / Row / Item
            ColIndexPassed.ColAttrCountTotal = 0;
            ColIndexPassed.ColAttrCounter = 0; // Current Attr
            ColIndexPassed.ColAttrMaxIndex = 0; // Total Attrs in Item
            ColIndexPassed.ColAttrMaxIndexTemp = 0;

            // ColIndexReset
            return PickState.PickDictResetResult;
        }
        #endregion
        #region Output File Clear Dict & Current
        // FileDictClearCurrent
        /// <summary> 
        /// Clear Dictionary Data.
        /// </summary> 
        public StateIs FileDictClearCurrent(ref mFileMainDef FmainPassed)
        {
            PickState.PickDictClearCurrentResult = StateIs.Started;
            // Clear dictionary array that contains this fields properties.
            Array.Clear(FmainPassed.ColIndex.ColArray, 0, 100);
            #region Input Dictionay Index Pointers
            // FILE DICTIONARY FILE CONTROL
            // Reset Input File protected internal Attr pointer for next dict item
            FmainPassed.ColIndex.ColIndex = 0; // dynamic protected internal pointer
            FmainPassed.ColIndex.ColCount = 0; // Total
            FmainPassed.ColIndex.ColCounter = 0; // Current for building
                                                 // FILE DICTIONARY COLUMNS
            FmainPassed.ColIndex.ColAttrIndex = 0;
            FmainPassed.ColIndex.ColAttrCount = 0;
            FmainPassed.ColIndex.ColAttrCounter = 1;
            FmainPassed.ColIndex.ColAttrSet = false;
            FmainPassed.ColIndex.ColAttrInvalid = 0;
            #endregion
            // Item ItemData
            FmainPassed.Item.ItemData = sEmpty;
            // ItemData Item Type
            // OutFile.Fmain.Fs.FileTypeId = Fs.FileTypeId;
            // OutFile.Fmain.Fs.FileSubTypeId = Fs.FileSubTypeId;
            //
            // FileDictClearCurrent
            return PickState.PickDictClearCurrentResult;
        }
        // FileDataClearCurrent
        /// <summary> 
        /// Clear File Data.
        /// </summary> 
        public StateIs FileDataClearCurrent(ref mFileMainDef FmainPassed)
        {
            PickState.PickDataClearCurrentResult = StateIs.Started;
            //
            FmainPassed.Item.ItemId = sEmpty;
            if (IterationFirst)
            { // CHECK & ToDo FileDataClearCurrent
                FmainPassed.Fs.ItemIdCurrent = sEmpty;
                // OutFile.Fmain.Fs.ItemIdNext = sEmpty;
            }
            #region Input ItemData Index Pointers
            // FILE DATA COLUMNS
            FmainPassed.DelSep.ItemAttrIndex = 0; // dynamic protected internal pointer
            FmainPassed.DelSep.ItemAttrCount = 0;
            FmainPassed.DelSep.ItemAttrCountTotal = 0;// Accumulator for shrinking work buffer
            FmainPassed.DelSep.ItemAttrCounter = 1;
            FmainPassed.DelSep.ItemAttrSet = false;
            FmainPassed.DelSep.ItemAttrInvalid = 0;
            // FILE DATA COLUMNS
            FmainPassed.ColIndex.ColAttrIndex = 0; // dynamic protected internal pointer
            FmainPassed.ColIndex.ColAttrCount = 0;
            FmainPassed.ColIndex.ColAttrCountTotal = 0;// Accumulator for shrinking work buffer
            FmainPassed.ColIndex.ColAttrCounter = 1;
            FmainPassed.ColIndex.ColAttrSet = false;
            FmainPassed.ColIndex.ColAttrInvalid = 0;
            // Reset Input File protected internal column processing pointer for next dict item
            FmainPassed.ColIndex.ColIndex = 0;
            FmainPassed.ColIndex.ColCount = 0;
            FmainPassed.ColIndex.ColCountTotal = 0;// Accumulator for shrinking work buffer
            FmainPassed.ColIndex.ColCounter = 0;
            FmainPassed.ColIndex.ColSet = false;
            FmainPassed.ColIndex.ColInvalid = 0;
            #endregion
            #region Item ItemData
            FmainPassed.Item.ItemData = sEmpty;
            // ItemData Item Type
            FmainPassed.Fs.FileType =
                FileType_Is.Unknown;
            FmainPassed.Fs.FileSubType =
                FileType_SubTypeIs.Unknown;
            #endregion
            #region InputItem
            FmainPassed.Item.ItemData = sEmpty; // File Item ItemData
            FmainPassed.Fs.ItemIdCurrent = sEmpty; // Current Id
            FmainPassed.Fs.ItemIdNext = sEmpty; // Next Id
            FmainPassed.Item.ItemId = sEmpty; // This Id
            FmainPassed.Item.ItemIdIsChanged = bNO;
            FmainPassed.FileStatus.bpNameIsChanged = bNO;
            // Import Input File Item Name
            // ** ColIndexPassed.Fs.FileId.FileNameFullDefault = "tld.import";
            #endregion
            #region Import Input File Options, Control and Modes
            // Import Output File Read and Access Modes
            FmainPassed.Fs.FileIo.IoMode = FileIo_ModeIs.Line; // ToDo RunMain FileTransformControl initialize
            FmainPassed.Fs.FileIo.ToDo = FileAction_ToDoIs.Read; // ToDo RunMain FileTransformControl initialize
            FmainPassed.Fs.FileIo.ToDoTarget = FileAction_ToDoTargetIs.DiskFile; // ToDo RunMain FileTransformControl initialize
            FmainPassed.Fs.FileIo.FileReadMode = FileAction_ReadModeIs.Table; // ???? dunno
                                                                              // FmainPassed.Fs.FileIo.FileAccessMode = FileIo_ModeIs.Line; // ToDo RunMain FileTransformControl initialize
                                                                              // Import Input File Options
            FmainPassed.Fs.FileOptionString = "F"; // ToDo $$$CHECK 7 FileDataClearCurrent options hard corded here...
            #endregion
            #region Other Working Fields
            ItemDataAtrributeClear(ref FmainPassed);
            // other
            FmainPassed.DelSep.ItemAttrMaxIndex = 0; // Total Attrs in Item
            FmainPassed.DelSep.ItemAttrMaxIndexTemp = 0; // Total Attrs in Item
                                                         // Character Pointers
            ItemDataCharClear(ref FmainPassed);
            // Buf.
            FmainPassed.Buf.BytesRead = 0;
            FmainPassed.Buf.BytesReadTotal = 0;
            FmainPassed.Buf.CharIndex = 1;
            FmainPassed.Buf.CharItemEofIndex = 0;
            // Working buffer value
            FmainPassed.Buf.FileWorkBuffer = sEmpty;
            // Conversion results
            FmainPassed.Buf.BytesConverted = 0;
            FmainPassed.Buf.BytesConvertedTotal = 0;
            // ToDo FileDataClearCurrent Special Characters
            #endregion
            // FileDataClearCurrent
            return PickState.PickDataClearCurrentResult;
        }
        #endregion

        #region File Property Clear
        // <Section Id = "x
        /// <summary> 
        /// Clear the Database Connection.
        /// </summary> 
        public void ConnectionClear(ref mFileMainDef FmainPassed)
        {
            // ? Readers / Writers etc ?
            // ? Close ? Dispose ?
            FmainPassed.ConnStatus.DataClear();
        }
        // <Section Id = "x
        /// <summary> 
        /// Clear the Database Name.
        /// </summary> 
        public void DatabaseClear(ref mFileMainDef FmainPassed)
        {
            // <Area Id = "SourceDatabaseInformation">
            FmainPassed.Fs.DatabaseName = sEmpty;
            FmainPassed.Fs.spTableNameLine = sEmpty;

            FmainPassed.FileSqlConn.DbSyn.spDatabaseFileCreateCmd = sEmpty;

            FmainPassed.FileStatus.DataClear();
        }
        /// <summary> 
        /// Clear the Tabe Name.
        /// </summary> 
        public void TableNameClear(ref mFileMainDef FmainPassed)
        {
            FmainPassed.Fs.TableName = sEmpty;
            FmainPassed.Fs.FileNameFullNext = sEmpty;
            FmainPassed.Fs.FileNameFullCurrent = sEmpty;
            FmainPassed.Fs.FileNameFullOriginal = sEmpty;
            FmainPassed.Fs.TableNameFull = sEmpty;
            FmainPassed.Fs.TableNameLine = sEmpty;
            //
            // FmainPassed.FileStatus.DataClear();
        }
        /// <summary> 
        /// Clear the Disk File Name.
        /// </summary> 
        public void FileNameClear(ref mFileMainDef FmainPassed)
        {
            FmainPassed.Fs.FileId.FileName = sEmpty;
            FmainPassed.Fs.FileNameFullNext = sEmpty;
            FmainPassed.Fs.FileNameFullCurrent = sEmpty;
            FmainPassed.Fs.FileNameFullOriginal = sEmpty;
            FmainPassed.Fs.FileId.FileNameFull = sEmpty;
            FmainPassed.Fs.FileId.FileNameLine = sEmpty;
            //
            // FmainPassed.FileStatus.DataClear();
        }
        /// <summary> 
        /// Clear the File Item Id.
        /// </summary> 
        public void ItemIdClear(ref mFileMainDef FmainPassed)
        {
            FmainPassed.Item.ItemId = sEmpty;
            FmainPassed.Fs.ItemIdNext = sEmpty;
            FmainPassed.Fs.ItemIdCurrent = sEmpty;
        }
        /// <summary> 
        /// Clear the Item Attribute Pointers.
        /// </summary> 
        public void ItemAtrributeClear(ref mFileMainDef FmainPassed)
        {
            // Attr Handling for Ascii and Text
            // Ascii Attr Pointers
            // Working value
            FmainPassed.DelSep.ItemAttrCounter = 1; // Current Attr
            FmainPassed.DelSep.ItemAttrCount = 0; // ItemData Items in Item / Row / Item
            FmainPassed.DelSep.ItemAttrMaxIndex = 0; // Total Attrs in Item
            FmainPassed.DelSep.ItemAttrMaxIndexTemp = 0;
            //
        }
        /// <summary> 
        /// Clear the Item Attributes and Values Pointers.
        /// </summary> 
        public void ItemDataAtrributeClear(ref mFileMainDef FmainPassed)
        {
            FmainPassed.DelSep.iItemDataAttrEos2Index = 0; // Current Column Separator 2
            FmainPassed.DelSep.iItemDataAttrEos1Index = 0; // Current Column Separator 1
            FmainPassed.DelSep.iItemDataAttrEosIndex = 0; // Current Column Sub-Value
            FmainPassed.DelSep.iItemDataAttrEovIndex = 0; // Current Column Value
            FmainPassed.DelSep.iItemDataAttrEoaIndex = 0; // Current Column
            FmainPassed.DelSep.iItemDataAttrEorIndex = 0; // Current Row
            FmainPassed.DelSep.iItemDataAttrEofIndex = 0; // Current File
        }
        /// <summary> 
        /// Clear the Item Data Characters Pointers.
        /// </summary> 
        public void ItemDataCharClear(ref mFileMainDef FmainPassed)
        {
            // Character Pointers
            FmainPassed.DelSep.iItemDataCharEobIndex = 0; // End of Character Buffer
            FmainPassed.DelSep.iItemDataCharIndex = 0; // Character Pointer
            FmainPassed.DelSep.iItemDataCharEofIndex = 0; // Character End of File
        }
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
        // Exceptions
        #region Exception Code
        #region Common Exception Code
        //The array below will be implemented as an error object in future:
        public String[] ExceptCommonMessage;
        //
        /// <summary> 
        /// Exception Main Method - All Exceptions
        /// </summary> 
        public void ExceptCommonFileImpl(
            ref mFileMainDef FmainPassed,
            Object ExceptionPassed,
            int PassedErrorLevel,
            int PassedErrorSource,
            String ErrorMsgPassed,
            StateIs PassedMethodResult
            )
        {
            String FileNameCurr = FmainPassed.Fs.FileNameLineGetFrom(ref FmainPassed.Fs);
            FileState.ExceptCommonFileResult = PassedMethodResult;
            FmainPassed.FileAction.FileException.Add(ExceptionPassed);
            ConsoleVerbosity = 7;
            if (FileNameCurr != null)
            {
                if (FileNameCurr.Length > 0)
                {
                    //
                    LocalMessage.ErrorMsg = "File Name: " + FileNameCurr + ", ";
                    LocalMessage.ErrorMsg += "Direction: " + FmainPassed.Fs.DirectionName;
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PassedMethodResult, false, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                    //
                    LocalMessage.ErrorMsg = "System: " + FmainPassed.Fs.SystemName + ", ";
                    LocalMessage.ErrorMsg += "Database: " + FmainPassed.Fs.DatabaseName;
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PassedMethodResult, false, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                    //
                    LocalMessage.ErrorMsg = "File Owner: " + FmainPassed.Fs.FileOwnerName + ", ";
                    LocalMessage.ErrorMsg += "File Group: " + FmainPassed.Fs.FileGroupName;
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PassedMethodResult, false, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                }
            }
            if (FmainPassed.DbIo.CommandCurrent != null)
            {
                if (FmainPassed.DbIo.CommandCurrent.Length > 0)
                {
                    LocalMessage.ErrorMsg = "Database command: " + FmainPassed.DbIo.CommandCurrent;
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PassedMethodResult, false, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                }
            }
            if (FmainPassed.DbIo.spConnString != null)
            {
                if (FmainPassed.DbIo.spConnString.Length > 0)
                {
                    LocalMessage.ErrorMsg = "Database connection command: " + FmainPassed.DbIo.spConnString;
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PassedMethodResult, false, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
                }
            }
            if (ErrorMsgPassed.Length > 0)
            {
                if (ExceptCommonMessage != null)
                {
                    String sTemp0 = sEmpty;
                    for (int i = 0; i < ExceptCommonMessage.Length; i++)
                    {
                        if (ExceptCommonMessage[i].Length > 0)
                        {
                            sTemp0 = "Error Details (" + (i + 1) + "): " + ExceptCommonMessage[i];
                           st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PassedMethodResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, sTemp0);
                        }
                    }
                }
                else
                {
                    LocalMessage.ErrorMsg = "Error Summary: " + ErrorMsgPassed;
                   st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PassedMethodResult, RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageEnterResume, LocalMessage.ErrorMsg);
                }
            }
            else
            {
                LocalMessage.ErrorMsg = "No Error Details available.";
               st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PassedMethodResult, RunErrorDidOccur = true, PassedErrorLevel, PassedErrorSource, bDoNotDisplay, MessageEnterResume, LocalMessage.ErrorMsg);
            }
            //
            ExceptCommonMessage = null; // clear after all handling...
        }
        #endregion
        #region SQL Exception
        /// <summary> 
        /// Exceptions Main Method - SQL Exceptions
        /// SECTION MessageDetailsMdm not implemented.
        /// Two classes Trace and Message
        /// Exceptions are part of Trace Class
        /// Database classes can be 
        /// </summary> 
        public void ExceptSqlImpl(
            ref mFileMainDef FmainPassed,
            String ErrorMsgPassed,
            ref SqlException ExceptionSql,
            StateIs PassedMethodResult
            )
        {
            FileState.ExceptSqlResult = PassedMethodResult;
            FmainPassed.FileStatus.FileErrorCurrent = FileIo_ErrorIs.SqlError;
            LocalMessage.ErrorMsg = "SQL Database Exception: (" + PassedMethodResult.ToString() + "): ";
            // + ExceptSql.Message;
            LocalMessage.ErrorMsg += ExceptionSql.Message;
           st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PassedMethodResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
            //
            ExceptCommonMessage = new String[ExceptionSql.Errors.Count];
            for (int i = 0; i < ExceptionSql.Errors.Count; i++)
            {
                ExceptCommonMessage[i] = ExceptionSql.Errors[i].ToString();
            }
            ExceptCommonFileImpl(ref FmainPassed, ExceptionSql, iNoErrorLevel, iNoErrorSource, LocalMessage.ErrorMsg, PassedMethodResult);
        }
        #endregion
        #region General File Exception
        // Exceptions Main Method - File Exceptions
        // Routes from SQL Exception.
        // Routes to General Exception.
        #region Database File General Exceptions
        //                         } catch (NotSupportedException ExceptionNotSupported) {
        /// <summary> 
        /// Exceptions - General - Not Supported.
        /// </summary> 
        public void ExceptTableGeneralImpl(ref NotSupportedException ExceptionNotSupported)
        {
            FileState.ExceptionDatabaseFileGeneralResult = StateIs.Started;
            ExceptTableGeneralImpl(ref Fmain, ref ExceptionGeneral);
        }
        /// <summary> 
        /// Exceptions - General - Not Support for Passed File.
        /// </summary> 
        public void ExceptTableGeneralImpl(ref mFileMainDef FmainPassed, ref NotSupportedException ExceptionNotSupported)
        {
            FileState.ExceptionDatabaseFileGeneralResult = StateIs.Started;
            ExceptTableGeneralImpl(ref FmainPassed, ref ExceptionGeneral);
        }
        /// <summary> 
        /// Exceptions - General
        /// Main Method for all exceptions.
        /// Exceptions being move to base classes
        /// including utility classes such as mFileDef.
        /// </summary> 
        public void ExceptTableGeneralImpl(ref Exception ExceptionGeneral)
        {
            ExceptTableGeneralImpl(ref Fmain, ref ExceptionGeneral);
        }
        /// <summary> 
        /// Exceptions - General for the Passed File.
        /// Main Method for all exceptions.
        /// Exceptions being move to base classes
        /// including utility classes such as mFileDef.
        /// </summary> 
        public void ExceptTableGeneralImpl(ref mFileMainDef FmainPassed, ref Exception ExceptionGeneral)
        {
            FileState.ExceptionDatabaseFileGeneralResult = StateIs.Started;
            FmainPassed.FileStatus.FileErrorCurrent = FileIo_ErrorIs.DatabaseError;
            // sMessageBoxMessage = StdProcess.Title + @"File Creation Status";
            // sMessageBoxMessage += "\n" + @"General Exception Error!";
            // sMessageBoxMessage += "\n";
            // try {
            // sMessageBoxMessage += "\n" + ExceptionNotSupported.ToString();
            //             } catch { ; }
            // ((ImTrace)ConsoleSender).TraceMdmDoImpl(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(),PickSystemCallStringResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, sMessageBoxMessage);
            // <Area Id = "display message
        }
        #endregion
        public void ExceptGeneralFileImpl(
            ref mFileMainDef FmainPassed,
            String ErrorMsgPassed,
            ref Exception ExceptionGeneral,
            StateIs PassedMethodResult
            )
        {
            FileState.ExceptGeneralFileResult = PassedMethodResult;
            FmainPassed.FileStatus.FileErrorCurrent = FileIo_ErrorIs.General;
            LocalMessage.ErrorMsg = "General Exception: (" + PassedMethodResult.ToString() + ") " + ExceptionGeneral.Message;
           st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PassedMethodResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageEnterOk, LocalMessage.ErrorMsg);
            ExceptCommonFileImpl(ref FmainPassed, ExceptionGeneral, iNoErrorLevel, iNoErrorSource, LocalMessage.ErrorMsg, PassedMethodResult);
            RunErrorDidOccur = bNO;
        }
        #endregion
        #region NotSupported Exception
        //public StateIs ExceptNotSupportedResult;
        ///// <summary> 
        ///// Exceptions - Not Supported Exception
        ///// Routes to General Exception.
        ///// </summary> 
        //public void ExceptNotSupportedImpl(
        //    ref mFileMainDef FmainPassed,
        //    ref NotSupportedException ExceptionNotSupported,
        //    String ErrorMsgPassed, 
        //    StateIs PassedMethodResult
        //    )
        //{
        //    ExceptNotSupportedResult = PassedMethodResult;
        //    LocalMessage.ErrorMsg = "NotSupported Exception: (" + PassedMethodResult.ToString() + ") " + ExceptionNotSupported.Message;
        //    LocalMessage.ErrorMsg += ErrorMsgPassed;
        //    ((ImTrace)ConsoleSender).TraceMdmDoImpl(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PassedMethodResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
        //    ExceptCommonFileImpl(ref FmainPassed, ExceptionNotSupported, iNoErrorLevel, iNoErrorSource, LocalMessage.ErrorMsg, PassedMethodResult);
        //    RunErrorDidOccur = bNO;
        //    if (RunAbortIsOn)
        //    {
        //        // (This does a generic throw)
        //        ExceptionCatchNotSupportedMdm(sEmpty);
        //    }
        //}
        #endregion
        #region IO Exception
        /// <summary> 
        /// Exceptions - IO Exception
        /// Routes to File Exception.
        /// </summary> 
        public void ExceptIoImpl(
            ref mFileMainDef FmainPassed,
            String ErrorMsgPassed,
            ref IOException ExceptionIo,
            StateIs PassedMethodResult
            )
        {
            FileState.ExceptIoResult = PassedMethodResult;
            FmainPassed.FileStatus.FileErrorCurrent = FileIo_ErrorIs.IoError;
            LocalMessage.ErrorMsg = "IO Exception: (" + PassedMethodResult.ToString() + ") " + ExceptionIo.Message;
           st.TraceMdmDoDetailed(ConsoleFormUses.DatabaseLog, 7, ref Sender, bIsMessage, TraceMdmCounterLevel1GetDefault(), TraceMdmCounterLevel2GetDefault(), PassedMethodResult, RunErrorDidOccur = true, iNoErrorLevel, iNoErrorSource, bDoNotDisplay, MessageNoUserEntry, LocalMessage.ErrorMsg);
            ExceptCommonFileImpl(ref FmainPassed, ExceptionIo, iNoErrorLevel, iNoErrorSource, LocalMessage.ErrorMsg, PassedMethodResult);
            RunErrorDidOccur = bNO;
        }
        #endregion
        //
        // END OF COMMON EXCEPTIONS
        #endregion
        /// <summary> 
        /// String Description of File System Object
        /// </summary> 
        public override String ToString()
        {
            String FileNameCurr = Fmain.Fs.FileNameLineGetFrom(ref Fmain.Fs);
            if (FileNameCurr != null && Fmain.Fs.DirectionName != null)
            {
                // String sTemp = "File Summary: " + FileNameCurr + " for " + Fmain.Fs.DirectionName;
                String sTemp = Fmain.Fs.ToString();
                return sTemp;
            }
            else { return base.ToString(); }
        }
        #endregion
        #endregion
    }
}
