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
using Mdm.Oss.Std;
using Mdm.Oss.Sys;
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

namespace Mdm.Srt.Transform
{
    #region Transformation
    /// <summary>
    /// <para> File Transform Control</para>
    /// <para> This is a control structure for
    /// applications that have an input and output file.
    /// When input data is processed (or tranformed) to
    /// create a new output file.</para>
    /// <para> It consists of an input and output
    /// File Summary for persistence and during the
    /// user interface stage.  The summaries are used
    /// to open the file object for processing.</para>
    /// <para> It is permitted and recommended to have
    /// the InFile and OutFile files open during the
    /// user entry process.  However, proper attention
    /// must be paid to changes in file name versus
    /// what file has be opened.</para>
    /// </summary>
    public class FileTransformControlDef : StdDef
    {
        #region FileTransformRun InputOutput
        public FileTransformControlDef(mFileDef InFilePassed, mFileDef OutFilePassed)
            : base()
        {
            InFile = InFilePassed;
            OutFile = OutFilePassed;
            FileTransformControlInitialize();
        }
        public FileTransformControlDef()
            : base()
        {
            FileTransformControlInitialize();
        }
        public void FileTransformControlInitialize()
        {
            // InFile.FileTransformControl = this;
            InputFsCurr = new FileSummaryDef(ref Sender, ref InFile, FileAction_DirectionIs.Input);
            // OutFile.FileTransformControl = this;
            OutputFsCurr = new FileSummaryDef(ref Sender, ref OutFile, FileAction_DirectionIs.Output);
        }
        // Transformation
        public String TransformFileTitle = "";
        // Source (Import) and Destination (Output) Object
        #region FileInputItem Declaration
        // InFile
        public mFileDef InFile;
        public FileSummaryDef InputFsCurr;
        #endregion
        #region FileOutputItem Declaration
        public mFileDef OutFile;
        public FileSummaryDef OutputFsCurr;
        #endregion
        #endregion
        #region ItemIdNotes
        // An Id would be found in the 
        // input (import) file
        // this id may be compared to the
        // (entered) ItemId that was
        // supplied by the user.
        // (Currently) the user can only
        // enter one id.  The idea is for them
        // to enter a matched lUrlHistList that would
        // be presented as a paired lUrlHistList for
        // comparison (verification) by the user.
        #endregion
        // Pass Settings Function
        public void InputItemClassFields(
            // Part of the struct!!
        #region File Fields Passed
        #region InputFile Passed
            // InFile
            String sPassedInputFileName,
            mFileDef ofPassedInputFileObject,
            String sPassedInputFileOptions,
        #endregion
        #region OutputFile Passed
            // OutputSystem
            String OutputSystemNamePassed,
            Object OutputSystemObjectPassed,
            // OutputDatabase
            String OutputDatabaseNamePassed,
            SqlConnection OutputDbConnObjectPassed,
            // OutFile
            String OutputFileNamePassed,
            // Object ofPassedOutputFileObject,
            mFileDef ofPassedOutputFileObject,
            String sPassedOutputFileOptions,
            // OutputItemId
            String sPassedOutputItemId,
            // ExistingItemId
            String sPassedInputItemId
        #endregion
        #endregion
        )
        // END OF InputItemClassFields Passed fields
        {
            #region SetInputItem
            InFile.Fmain.Fs.FileId.FileName = sPassedInputFileName;
            InFile.Fmain.Fs.Direction = FileAction_DirectionIs.Input;
            InFile.Fmain.Fs.IoMode = FileIo_ModeIs.Sql;
            #endregion
            #region SetFileOutputItem
            OutFile.Fmain.Fs.FileId.FileName = OutputFileNamePassed;
            OutFile.Fmain.Fs.Direction = FileAction_DirectionIs.Output;
            OutFile.Fmain.Fs.IoMode = FileIo_ModeIs.Sql;
            // MFILE1 OBJECT
            if (ofPassedOutputFileObject != null)
            {
                if (OutFile != null)
                {
                    OutFile = null;
                }
                OutFile = ofPassedOutputFileObject;
            }
            #endregion
            #region ItemId
            InFile.Fmain.Item.ItemId = sPassedOutputItemId;
            #endregion
            #region SetMinputTldItemClassFields
            // Source and Destination Object
            #region SetInputItem Empty
            #endregion
            #region SetFileOutputItem Empty
            // OutputSystem
            // String Fs.spSystemName;
            // Object ooSystemObject;
            // Output Database
            // String spDatabaseName;
            // SqlConnection DbConnObject;
            // OutFile
            // String OutputFileName;
            // mFilePickDbDef OutFile;
            // String FileOptionsStringPassed;
            // OutputItemId
            // String ItemId;
            // 
            #endregion
            #region SetInputItemId
            // (Existing in Output vs Options)
            InFile.Fmain.Item.ItemId = "";
            InFile.Fmain.Fs.ItemIdCurrent = "";
            // FileTransformControl.ItemId = "";
            // FileTransformControl.PickRow.PickDictArray.ItemId = "";
            InFile.Fmain.Item.ItemIdIsChanged = bNO;
            InFile.Fmain.FileStatus.NameIsChanged = bNO;
            #endregion
            #endregion
            #region ClassInternalProperties
            //#region FileInputItemType Set
            //InFile.Fmain.Fs.FileTypeName = "Unknown FileType99";
            //InFile.Fmain.Fs.FileTypeId = FileType_Is.Unknown; // ToDo SHOULD LOAD FROM OPTIONS
            //InFile.Fmain.Fs.FileSubTypeName = "Unknown FileSubType99";
            //InFile.Fmain.Fs.FileSubTypeId = FileType_SubTypeIs.Unknown;
            //#endregion
            //#region FileOutputItemType Set
            //OutFile.Fmain.Fs.FileTypeName = "Unknown FileType99";
            //OutFile.Fmain.Fs.FileTypeId = FileType_Is.Unknown; // ToDo SHOULD LOAD FROM OPTIONS
            //OutFile.Fmain.Fs.FileSubTypeName = "Unknown FileSubType99";
            //OutFile.Fmain.Fs.FileSubTypeId = FileType_SubTypeIs.Unknown;
            //#endregion
            #endregion
        } // End of Constructor - InputItemClassFields Passed
        public override String ToString()
        {
            if (InFile != null && OutFile != null)
            {
                if (InFile.Fmain.Fs.FileId.FileName != null && OutFile.Fmain.Fs.FileId.FileName != null)
                {
                    String sTemp = "File Transform Control: " + InFile.ToString() + " and " + OutFile.ToString();
                    return sTemp;
                }
                else { return "File Transform Control not initialized."; }
            }
            else { return "File Transform Control not initialized."; }
        }
    }
    #endregion
}
