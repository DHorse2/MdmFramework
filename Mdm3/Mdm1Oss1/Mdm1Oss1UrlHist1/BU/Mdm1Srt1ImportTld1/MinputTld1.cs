//Top//
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
// using System.Windows.Forms;
using System.Windows.Input;
// using System.Windows.Media;
// using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//
// using Mdm.Oss.ClipboardUtil;
using Mdm.Oss.CodeUtil;
using Mdm.Oss.Decl;
using Mdm.Oss.Support;
using Mdm.Oss.Mapp;
using Mdm.Oss.Mobj;
using Mdm.Oss.FileUtil;
using Mdm.Pick;
// using    Mdm1Oss1FileCreation1;
using System.ComponentModel;


namespace Mdm.Srt.InputTld
{
    public class MinputTld : DefStdBaseRunFileConsole, IProcesFile1 {
        // ProcessOpenFile or 
        // ProcesFile or 
        // RunEngineXXXX
        //      Application_ProcessRun
        //          RunMain
        // ProcessCommandXXXX
        //      DoProcessCommand
        //          Application_ProcessRun
        //              RunMain

        #region Declarations
        #region Common Declarations and Delegates
        // Mdm1 Srt1 InputTld1 OpSys Bootstrap
        // Mdm.Srt.InputTld - MinputTld Class
        //
        // delegates and event callbacks
        /*
        public delegate void TextBoxChangeDelegate(object sender, string s);
        public delegate void TextBoxAddDelegate(object sender, string s);
        public delegate void ProgressCompletionDelegate(object sender, string sField, int iAmount, int iMax);

        public event TextBoxChangeDelegate InputFileLineChange;
        public event TextBoxChangeDelegate OutputFileLineChange;

        public event ProgressCompletionDelegate oStatusLineMdmChanged;

        public event TextBoxChangeDelegate StatusLineMdmChanged;
        public event TextBoxChangeDelegate StatusLineMdmText2TextChanged;
        public event TextBoxChangeDelegate StatusLineMdmText3TextChanged;
        public event TextBoxChangeDelegate StatusLineMdmText4TextChanged;

        public event TextBoxAddDelegate StatusLineMdmText1TextAdd;
        public event TextBoxAddDelegate StatusLineMdmText2TextAdd;
        public event TextBoxAddDelegate StatusLineMdmText3TextAdd;
        public event TextBoxAddDelegate StatusLineMdmText4TextAdd;
        // public delegate void WorkerProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs eaProcessChangeEventArgs);

            InputTldThreadMethodHandler synchronousFunctionHandler = default(InputTldThreadMethodHandler);
            synchronousFunctionHandler = new InputTldThreadMethodHandler(this.InputTldDoMethodSynchronous);
        */
        // public delegate int MdmTraceCounterGetDel();
        #endregion
        // Class Constants
        #region MdmClassFramework Constants
        // Class Devices, Input and Output
        #region MdmClassFramework Devices
        #region SystemFunction
        protected int iPickSystemCommand = -1;
        #endregion
        #region MdmClassTemporaryDeclarations
        // <Section Summary>
        // <Section Role="Declarations">
        // <Section Id = "MdmClassTemporaryDeclarations">
        // <Section Vs="MdmTempVarVs0_8_5">
        // MdmClassTemporaryDeclarations MdmTempVarVs0_8_5
        // <Area Id = "Data">
        protected string sTempDataImport = "";
        protected string sTempData = "";
        // <Area Id = "Counters">
        protected int iTempCounter = 0;
        private int iCharCounter = 0;
        private int iCharLocation = 0;
        // <Area Id = "Arrays">
        // see Mobject Infrastructure Layer.
        // <Area Id = "Bools">
        protected bool bTemp = false;
        protected bool bTemp0 = false;
        protected bool bTemp1 = false;
        protected bool bTemp2 = false;
        protected bool bTemp3 = false;
        protected bool bTemp4 = false;
        protected bool bTemp5 = false;
        protected bool bTemp6 = false;
        protected bool bTemp7 = false;
        protected bool bTemp8 = false;
        protected bool bTemp9 = false;
        // <Area Id = "Integers">
        protected int iTemp = 0;
        protected int iTemp0 = 0;
        protected int iTemp1 = 0;
        protected int iTemp2 = 0;
        protected int iTemp3 = 0;
        protected int iTemp4 = 0;
        protected int iTemp5 = 0;
        protected int iTemp6 = 0;
        protected int iTemp7 = 0;
        protected int iTemp8 = 0;
        protected int iTemp9 = 0;
        protected int iTempA = 0;
        protected int iTempB = 0;
        protected int iTempC = 0;
        protected int iTempD = 0;
        protected int iTempE = 0;
        protected int iTempF = 0;
        // <Area Id = "long">
        protected long lTemp = 0;
        protected long lTemp0 = 0;
        protected long lTemp1 = 0;
        protected long lTemp2 = 0;
        protected long lTemp3 = 0;
        protected long lTemp4 = 0;
        protected long lTemp5 = 0;
        protected long lTemp6 = 0;
        protected long lTemp7 = 0;
        protected long lTemp8 = 0;
        protected long lTemp9 = 0;
        protected long lTempA = 0;
        protected long lTempB = 0;
        protected long lTempC = 0;
        protected long lTempD = 0;
        protected long lTempE = 0;
        protected long lTempF = 0;
        // <Area Id = "double">
        protected double dTemp = 0;
        protected double dTemp0 = 0;
        protected double dTemp1 = 0;
        protected double dTemp2 = 0;
        protected double dTemp3 = 0;
        protected double dTemp4 = 0;
        protected double dTemp5 = 0;
        protected double dTemp6 = 0;
        protected double dTemp7 = 0;
        protected double dTemp8 = 0;
        protected double dTraceDisplayMessageDetail = 0;
        protected double dTempA = 0;
        protected double dTempB = 0;
        protected double dTempC = 0;
        protected double dTempD = 0;
        protected double dTempE = 0;
        protected double dTempF = 0;
        // <Area Id = "Strings">
        protected string sTemp = "";
        protected string sTemp0 = "";
        protected string sTemp1 = "";
        protected string sTemp2 = "";
        protected string sTemp3 = "";
        protected string sTemp4 = "";
        protected string sTemp5 = "";
        protected string sTemp6 = "";
        protected string sTemp7 = "";
        protected string sTemp8 = "";
        protected string sTemp9 = "";
        protected string sTempA = "";
        protected string sTempB = "";
        protected string sTempC = "";
        protected string sTempD = "";
        protected string sTempE = "";
        protected string sTempF = "";

        private string sCurrentString = "";

        #endregion
        #region MdmClass DatabaseServer Empty
        #endregion
        #endregion
        // Class Standard Object Classes
        #region Class High Level Information
        #region Primary Data Object
        // Win32 Binary File Object
        // include dm,bp,dos, fcntl.h;
        // include dm,bp,dos, errno.h;
        #endregion
        #region Progam Header
        internal string sProcessHeading = "Mdm Migration";
        internal string sProcessSubHeading = "Import Processing";
        // * @@@
        // *PROGRAM: IMPORT.TLD
        // *
        // !
        // * IMPORT MS-DOS TLD file to PICK items
        // *
        // * Copyright 1990, 1991 PICK Systems
        // * AND (C) COSMOS REVELATION
        // * AND (C) ULTIMATE
        // * AND (C) REALITY
        // * AND (C) AXION COMPUTER SOFTWARE
        // * AND (C) MACROSCOPE DESIGN MATRIX
        #endregion
        #region PrimitiveProgramFlowControl
        // Primitive Program looping flags
        // (intended to avoid the need for
        // loops, for's, while's, do's,
        // etc. that are found in various
        // languages)  The idea is to minimize
        // the need for conversion between
        // various languages.  Orginally these
        // where the (DREAD) GOTO'S!!!!!!
        //
        #endregion
        #region Class External properties
        #endregion
        #region Package Object Declarations see Mapplication
        /* // <Area Id = "Mapplication">
        // <Area Id = "omAplication">
        public Application omAp;
        // <Area Id = "omHControl">
        public Mcontroller omCo;
        // <Area Id = "MdmStdObject">
        internal Mobject omOb;
        internal Page omPm;
        // <Area Id = "MdmLocalVerb">
        internal MinputTld omVe;
        */
        #endregion
        #region StandardObjects
        #region PackageObjectDeclarations (strongly)TYPED
        // <Area Id = "Mapplication">
        // <Area Id = "omAplication">
        public Application omAp;
        // <Area Id = "omHControl">
        public Mcontroller omCo;
        // <Area Id = "omO">
        public Mobject omOb;
        // <Area Id = "MdmLocalVerb">
        // <Area Id = "MdmLocalVerb">
        public MinputTldThread omVe;
        // <Area Id = "omAplication">
        public Mapplication omMa;
        // <Area Id = "omW>
        public MinputTld omWt;
        #endregion
        // #endregion
        #region PageEvents
        // public event EventHandler<CloseEventArgs> CancelClose;
        string sPage2ReturnValue;
        #endregion
        #region Page Declartions
        // <Area Id = "omP">
        public MinputTldPageMain omPm;
        // <Area Id = "omP2">
        public MinputTldPageDetail omPd;
        #endregion
        // Class Internals - Properties Fields and Attributes
        #region MinputTldPageMain Internal Field Declarations
        protected string InputFileNameCurrent;
        protected Mfile InputFile;
        internal int InputFileNameCurrentNotValid = 99999;

        //xxxxxxxxxxxxxxxx
        protected Mfile OutputFile;
        internal System.Uri uTld1Page2Uri = new System.Uri("/Mdm.Srt.InputTld;component/page2.xaml", System.UriKind.Relative);

        #endregion
        #endregion
        #endregion
        #endregion
        // Program Control and Fields
        #region Class Detailed Information
        #region ClaseInternalResults;
        // Application
        public long iAppCoreObjectGet;
        public long iApplicationMobjectObjectSet;
        public long iApplicationMbgworkerObjectSet;
        public long iApplicationVerbObjectSet;
        public long iApplicationVerbObjectGet;
        public long iApplication_ProcessRun;
        public long iCommandLineOptionsParse;
        public long iCommandLineParse;
        // Command
        public long RunEngineDoRunResult;
        public long iDoProcessCommand;
        public long iDoProcessCommandLine;
        public long iDoProcesFile;
        // This MinputTldApp
        public long iMinputTld;
        public long iMinputTld_Reset;
        public long iMinputTld_CancelAsync;
        public long iMImportEngine;
        public long RunEngineMainResult;
        //
        public long iMinputTldMain;
        public long iMinputTldMainBufferRead;
        public long iMinputTldMainProcessIdChange;
        public long iMinputTldMainSetOutputData;
        //
        public long iProcessCommandConsole;
        public long iProcessCommandAction;
        public long iProcessCommandLine;
        //
        public long iProcesFile;
        public long iProcessOpenFile;
        #endregion
        #region Import Wrtie Attribute Tld Options
        protected bool bPickOutputItemWriteItBoolFlag;
        protected int iInputItemAttributeIndex1;
        protected int iInputItemAttributeIndex2;
        protected string InputTldFileOptions = "";
        #endregion
        // *  
        // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        // *  Import File Class Field Structure
        // *:
        #region FileTransformControDef
        public FileTransformControDef TransformFileControl;
        #endregion
        #endregion
        #endregion
        int iTemp999 = 999;
        #region Main
        public MinputTld(Mobject omPmssedO) : this() {
            iMinputTld = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectDoesNotExist;
            //
            try {
                if (omOb == null) {
                    if (omPmssedO != null) {
                        omOb = (Mobject)omPmssedO;
                        omAp = (Application)omOb.omAp;
                        omPm = (MinputTldPageMain)omOb.omPm;
                        omPd = (MinputTldPageDetail)omOb.omPd;
                        omCo = (Mcontroller)omOb.omCo;
                        omVe = (MinputTldThread)omOb.omVe;
                        // omMa = (MinputTld)omOb.omMa;
                        omWt = (MinputTld)this;
                        iMinputTld = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectOK;
                    } else {
                        // error
                        omAp = MinputTldApp.Current;
                        omOb = new Mobject(omAp);
                        omWt = (MinputTld)this;
                        omMa.ApplicationAppObjectSet(omAp);
                    }
                    if (omPm == null | omPd == null) {
                        if (omCo.omPm != null | omCo.omPd != null) {
                            omPm = (MinputTldPageMain)omCo.omPm;
                            omPd = (MinputTldPageDetail)omCo.omPd;
                            iMinputTld = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectOK;
                        }
                    }
                }
                if (omWt == null) { omWt = this; }
                iMinputTld = omMa.ApplicationMappObjectSet(omMa);
            } catch (Exception oeMexceptConvException) {
                sTemp = oeMexceptConvException.Message;
            }
            // return iMinputTld;
        }

        protected void MdmCatchNotSupportedException(string LocalMessage) {
            throw new NotSupportedException(LocalMessage);
        }
        //
        public MinputTld() {
            iMinputTld = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectDoesNotExist;
            #region Mimport1TldComments
            /* Struct Notes
             * 
             * A struct in C# is similar to a class, but 
             * structs lack certain features, 
             * such as inheritance. 
             * 
             * Also, since a struct is a value type, 
             * it can typically be created faster than a class. 
             * If you have tight loops in which 
             * new data structures are being created in large numbers, 
             * you should consider using a struct 
             * instead of a class. 
             * 
             * Structs are also used to 
             * encapsulate groups of data fields such as 
             * the coordinates of a point on a grid, or 
             * the dimensions of a rectangle. 
             * 
             * Example
             * This example program defines a struct 
             * to store a geographic location. 
             * 
             * It also overrides the ToString() method 
             * to produce a more useful output 
             * when displayed in the WriteLine statement. 
             * 
             * As there are no methods in the struct, 
             * there is no advantage in defining it as a class.
             * 
             * C#  Copy Code 
             * GeographicLocation Seattle = new GeographicLocation(123, 47);
             * System.Console.WriteLine("Position: \"" + Seattle.ToString() + "\"");
            */
            #endregion
            ConsoleMdmControlSet();
            // TODO Can't start without passed object, the folloing call does not work
            // AppCoreObjectGet(omOb);
            FileTransformControDef TransformFileControl = new FileTransformControDef();
            // return iMinputTld;
            TransformFileControl.OutputFile.FileSummary.FileNameCurrentNotValid = 99999;

            TransformFileControl.OutputFile.FileSummary.FileItemIdCurrent = "99999";
            TransformFileControl.OutputFile.FileSummary.FileItemIdCurrentNotValid = 99999;

            TransformFileControl.OutputFile.FileSummary.FileOptionsCurrent = "";

        }

        public void RunReset() {
            iMinputTld_Reset = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectDoesNotExist;
            RunStatus = 0;
            // return iMinputTld_Reset
        }

        public void RunCancelAsync() {
            iMinputTld_CancelAsync = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectDoesNotExist;
            RunStatus = 0;
            System.Windows.RoutedEventArgs eRea = new System.Windows.RoutedEventArgs();
            omVe.CallerAsynchronousEventsCancelClick(this, eRea);
        }

        public long ApplicationMbgWorkerObjectSet(MinputTldThread omwPassedLocalBgWorker) {
            iApplicationMbgworkerObjectSet = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectOK;
            if (omWt == null) {
                omWt =  this;
            }
            return iApplicationMbgworkerObjectSet;
        }

        public long ApplicationVerbObjectSet(MinputTldThread omPmssedV) {
            iApplicationVerbObjectSet = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectOK;
            if (omVe == null) {
                // omVe = (MinputTldThread)this;
                omVe = omPm.omVe;
            }
            return iApplicationVerbObjectSet;
        }

        #region MdmApplicationOjbectGet
        public long ApplicationMobjectObjectSet(Mobject omPmssedO) {
            iApplicationMobjectObjectSet = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectOK;
            if (omOb == null) {
                omOb = omPmssedO;
            }
            if (omOb == null) {
                omOb = omPm.omOb;
            }
            return iApplicationMobjectObjectSet;
        }

        public long AppCoreObjectGet(Mobject omPmssedO) {
            iAppCoreObjectGet = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // MinputTldApp omOb;
            // MinputTldApp Mobject omO
            if (omOb == null) {
                // omOb = omPmssedO;
                // omOb = ApplicationMobjectObjectGetFromMaobject();
                // omOb = (Mobject)ApplicationMobjectObjectGetFromMaobject();
                // TODO Mob null from MinputTld() does not work when Mob is null
                omOb = (Mobject)omCo.omPm.ApplicationMobjectObjectGet();
                // omOb = this;
                // ApplicationMobjectObjectSetMobject(this);
                if (omOb == null) { omOb = omPm.omOb; }
            }
            // MinputTldApp Application omAp
            if (omAp == null) {
                omAp = omMa.ApplicationAppObjectGet();
                // omAp = (Application)ApplicationAppObjectGet();
                // omAp = this;
                // ApplicationAppObjectSet(this);
            }
            // MinputTldApp Handler omCo;
            if (omCo == null) {
                // omCo = ApplicationHandlerObjectGet();
                omCo = (Mcontroller)omMa.ApplicationHandlerObjectGet();
                // omCo = this;
                // ApplicationHandlerObjectSet(this);
                if (omCo == null) { omCo = omPm.omCo; }

            }
            // MinputTldApp Page;
            if (omPm == null) {
                omPm = (MinputTldPageMain)omMa.AppPageMainObjectGet();
                // omPm = (Page)AppPageMainObjectGet();
                // omPm = this
                // AppPageMainObjectSet(this);
                // TODO if (omPm == null) { omPm = (MinputTldPageMain)MinputTldApp.Current.MainWindow; }
            }
            // MinputTldApp MinputTldPageDetail;
            if (omPd == null) {
                omPd = (MinputTldPageDetail)omMa.AppPageDetailObjectGet();
                // omPd = (MinputTldPageDetail)AppPageDetailObjectGet();
                // omPd = this
                // AppPageDetailObjectSet(this);
                if (omPd == null) { omPd = omPm.omPd; }
            }
            // MinputTldApp Verb
            if (omVe == null) {
                // omVe = ApplicationVerbObjectGet();
                // omVe = (MinputTldThread)ApplicationVerbObjectGet();
                // omVe = (MinputTldThread)omOb.omVe;
                // omMa.ApplicationVerbObjectSet(this);
                omVe = (MinputTldThread)omMa.ApplicationVerbObjectGet();
                if (omVe == null) { omVe = omPm.omVe; }
            }
            // Mapp omMa
            if (omMa == null) {
                omMa = (Mapplication)omMa.ApplicationMappObjectGet();
                // omMa = (Mapplication)MapplicationMappObjectGet();
                // omMa = this;
                // MapplicationMappObjectSet(this);
                if (omMa == null) { omMa = omPm.omMa; }
            }
            // BgWorker omW
            if (omWt == null) {
                // omWt = (MinputTld)omMa.ApplicationMbgWorkerObjectGet();
                // omWt = (BgWorkerlication)BgWorkerlicationMbgWorkerObjectGet();
                omWt = this;
                // BgWorkerlicationMbgWorkerObjectSet(this);
                // if (omWt == null) { omWt = omPm.omWt; }
            }
            return iAppCoreObjectGet;
        }
        #endregion
        // ==================================================================
        // IprocesInputFile1 Interface Methods
        // ==================================================================
        public long RunEngine(string sPassedEngineActionRequest) {
            iMImportEngine = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // x ooSystemObject
            TransformFileControl.OutputFile.FileSummary.SystemObject = sPassedEngineActionRequest;

            if (sPassedEngineActionRequest == "Import") {
                omOb.MdmClassResult = RunEngineDoRun(sPassedEngineActionRequest);
                return iMImportEngine;
            }
            return iMImportEngine;
        }
        #endregion
        #region CommandProcess
        public long ProcessOpenFile(string sPassedFileActionRequest, string sPassedInputFileName, Mfile ofPassedInputFileObject, string sPassedOutputFileName, Mfile ofPassedOutputFile, string sPassedOutputFileItemId, string sPassedFileActionOptions) {
            iProcessOpenFile = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omOb.MdmClassResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            try {
                if (omPm == null | omPd == null) {
                    omPm = (MinputTldPageMain)omCo.omPm;
                    omPd = (MinputTldPageDetail)omCo.omPd;
                    iMinputTld = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectOK;
                }
            } catch (Exception oeMexceptConvException) {
                sTemp = oeMexceptConvException.Message;
            }

            //
            if (ofPassedInputFileObject == null) {
                // Open file
            } else {
                TransformFileControl.InputFile = ofPassedInputFileObject;
            }

            if (ofPassedOutputFile == null) {
                // Open file
            } else {
                TransformFileControl.OutputFile = ofPassedOutputFile;
            }
            // Action
            FileActionRequest = sPassedFileActionRequest;
            // Import
            TransformFileControl.InputFile.FileSummary.FileName = sPassedInputFileName;
            // InputFileType
            TransformFileControl.InputFile.FileSummary.FileTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeTEXT;
            TransformFileControl.InputFile.FileSummary.FileSubTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE;
            // Ouput
            // TransformFileControl.FileTypeId = FileData; // TODO Output Type 
            // WAS FileData (load from form)
            TransformFileControl.OutputFile.FileSummary.FileTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeDictData; // TODO Output Type 
            TransformFileControl.OutputFile.FileSummary.FileSubTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeSQL;
            //
            TransformFileControl.OutputFile.FileSummary.FileName = sPassedOutputFileName;
            TransformFileControl.OutputFile.FileSummary.FileItemId = sPassedOutputFileItemId;
            TransformFileControl.OutputFile.FileSummary.FileOptions = sPassedFileActionOptions;
            TransformFileControl.OutputFile.FileSummary.FileOptions = sPassedFileActionOptions;

            //
            // this is the active run
            // 
            iProcessOpenFile = Application_ProcessRun();

            // iProcessOpenFile
            return iProcessOpenFile;
        }
        public long ProcesFile(string sPassedFileActionRequest, string sPassedInputFileName, string sPassedOutputFileName, string sPassedOutputFileItemId, string sPassedFileActionOptions) {
            iProcesFile = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omOb.MdmClassResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            //
            // this is the active run
            // 
            FileActionRequest = sPassedFileActionRequest;
            TransformFileControl.InputFile.FileSummary.FileName = sPassedInputFileName;
            TransformFileControl.OutputFile.FileSummary.FileItemId = sPassedOutputFileName;
            TransformFileControl.OutputFile.FileSummary.FileItemId = sPassedOutputFileItemId;

            TransformFileControl.OutputFile.FileSummary.FileOptions = sPassedFileActionOptions;
            TransformFileControl.OutputFile.FileSummary.FileOptions = sPassedFileActionOptions;
            RunOptions = sPassedFileActionOptions;

            //
            // this is the active run
            // 
            iProcesFile = Application_ProcessRun();

            //
            return iProcesFile;
        }

        public long ProcessCommandConsole(string sPassedCommandLineRequest) {
            iProcessCommandConsole = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omOb.MdmClassResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omOb.MdmMethodResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            sCommandLineRequest = sPassedCommandLineRequest;
            string sPassedFileActionRequest = "";
            string sPassedInputFileName = "";
            string sPassedOutputFileName = "";
            string sPassedOutputFileItemId = "";
            string sPassedFileActionOptions = "";

            iProcessCommandConsole = CommandLineParse(sCommandLineRequest);

            if (iProcessCommandConsole == 0) {
                if (sPassedFileActionRequest == "Import") {
                    //
                    // Process Command Line Request
                    //
                    iProcessCommandConsole = DoProcessCommand(sPassedFileActionRequest, sPassedInputFileName, sPassedOutputFileName, sPassedOutputFileItemId, sPassedFileActionOptions);
                    return iProcessCommandConsole;
                } else { iProcessCommandConsole = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Failed; }
            }
            // iProcessCommandConsole
            return iProcessCommandConsole;
        }
        public long ProcessCommandAction(string sPassedFileActionRequest, string sPassedInputFileName, string sPassedOutputFileName, string sPassedOutputFileItemId, string sPassedFileActionOptions) {
            iProcessCommandAction = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omOb.MdmClassResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            if (sPassedFileActionRequest == "Import") {
                //
                // Process Command Parameters
                //
                iProcessCommandAction = DoProcessCommand(sPassedFileActionRequest, sPassedInputFileName, sPassedOutputFileName, sPassedOutputFileItemId, sPassedFileActionOptions);
                // iProcessCommandAction
                return omOb.MdmClassResult;
            } else { iProcessCommandAction = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Failed; }
            // iProcessCommandAction
            return iProcessCommandAction;
            // TODO more work needed on Tracing and Introspection
        }

        public long ProcessCommandLine(string sPassedFileCommandLine) {
            omOb.MdmClassResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            //
            // this is the active run
            // 
            omOb.MdmClassResult = Application_ProcessRun();

            return omOb.MdmClassResult;
        }
        #endregion
        #region CheckDbObject
        #region Method Results
        // File Database
        public long FileDatabaseCheckDoesExistResult;
        public long FileNameCheckDoesExistResult;
        public long FileSystemNameCheckDoesExistResult;
        public long FileDatabaseNameDoesExistResult;
        #endregion

        public long FileSystemNameCheckDoesExist(Object ooPassedSystemObject, string sPassedSystemName, bool bDoConnectionClose, bool DatabaseConnDoDispose) {
            FileSystemNameCheckDoesExistResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omOb.MdmSystemResult = bNO;
            //
            // Check if file exists
            // 
            if (ooPassedSystemObject == null) {
                // System Object Empty
                TransformFileControl.OutputFile.FileSummary.SystemObject = new Object();

            }
            if (ooPassedSystemObject == null) {
                // System Object Empty
                FileSystemNameCheckDoesExistResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.MdmObjectCreationFailed;
                return FileSystemNameCheckDoesExistResult;
            }
            // Check SystemName
            if (sPassedSystemName == "") {
                // System Line Empty
                FileSystemNameCheckDoesExistResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.MdmNameCreationFailed;
                return FileSystemNameCheckDoesExistResult;
            }

            omOb.MdmSystemResult = FileSystemNameCheckDoesExist(sPassedSystemName, bDoConnectionClose, DatabaseConnDoDispose);

            if (omOb.MdmSystemResult == bNO) {
                FileSystemNameCheckDoesExistResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectDoesNotExist;
            } else {
                FileSystemNameCheckDoesExistResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectDoesExist;
            }
            return FileSystemNameCheckDoesExistResult;
        }

        public bool FileSystemNameCheckDoesExist(string sPassedSystemName, bool bDoConnectionClose, bool DatabaseConnDoDispose) {
            FileSystemNameCheckDoesExistResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omOb.MdmSystemResult = bON;

            // Check registration of System
            // assumed to exist for now TODO
            FileSystemNameCheckDoesExistResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectDoesExist;
            return omOb.MdmSystemResult;
        }
        //
        public long FileDatabaseCheckDoesExist(SqlConnection ofdPassedDatabaseObject, string sPassedDatabaseName, bool bDoConnectionClose, bool DatabaseConnDoDispose) {
            FileDatabaseCheckDoesExistResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omOb.MdmSystemResult = bNO;
            //
            // Check if file exists
            // 
            if (ofdPassedDatabaseObject == null) {
                // Database Object Empty
                FileDatabaseCheckDoesExistResult = (int)DatabaseControl.ResultMissingName;
            }
            // Check sDatabaseName
            if (sPassedDatabaseName == "") {
                // Database Line Empty
                FileDatabaseCheckDoesExistResult = (int)DatabaseControl.ResultMissingName;
            } else {
                omOb.MdmSystemResult = FileDatabaseNameDoesExist(sPassedDatabaseName, bDoConnectionClose, DatabaseConnDoDispose);

                if (omOb.MdmSystemResult == bNO) {
                    FileDatabaseCheckDoesExistResult = (int)DatabaseControl.ResultDoesNotExist;
                } else {
                    FileDatabaseCheckDoesExistResult = (int)DatabaseControl.ResultDoesExist;
                }
            }
            return FileDatabaseCheckDoesExistResult;
        }

        protected bool FileDatabaseNameDoesExist(string sPassedDatabaseName, bool bDoConnectionClose, bool DatabaseConnDoDispose) {
            FileDatabaseNameDoesExistResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omOb.MdmSystemResult = bON;

            // Check registration of Database
            // TODO asssume File Database (Name) exists for now

            return omOb.MdmSystemResult;
        }

        public long FileNameCheckDoesExist(Mfile ofPassedFileObject, string sPassedFileName, bool bDoConnectionClose, bool DatabaseConnDoDispose) {
            FileNameCheckDoesExistResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            omOb.MdmSystemResult = bNO;
            //
            // Check if file exists
            // 
            if (ofPassedFileObject == null) {
                // File Object Empty
                FileNameCheckDoesExistResult = (int)DatabaseControl.ResultOK;
                return FileNameCheckDoesExistResult;
            }
            // Check FileName
            if (sPassedFileName == "") {
                // File Line Empty
                FileNameCheckDoesExistResult = (int)DatabaseControl.ResultMissingName;
                return FileNameCheckDoesExistResult;
            } else {
                omOb.MdmSystemResult = File.Exists(sPassedFileName);
                if (omOb.MdmSystemResult) {
                    FileNameCheckDoesExistResult = (int)DatabaseControl.ResultDoesExist;
                    return FileNameCheckDoesExistResult;
                } else {
                    FileNameCheckDoesExistResult = (int)DatabaseControl.ResultDoesNotExist;
                    return FileNameCheckDoesExistResult;
                }
            }
            FileNameCheckDoesExistResult = (int)DatabaseControl.ResultShouldNotExist;
            return FileNameCheckDoesExistResult;
        }
        #endregion
        #region CommandParse
        protected long CommandLineParse(string sPassedCommandLineRequest) {
            long iCommandLineParse = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            int iCharCounter = 0;
            string sCurrentString = "";

            while (iCharCounter < sPassedCommandLineRequest.Length) {
                sCurrentString = sPassedCommandLineRequest.Substring(iCharCounter, 1);
            }
            iCommandLineParse = omOb.MdmMethodResult;
            return iCommandLineParse;
        }

        protected long CommandLineOptionsParse(string sPassedOutputFileOptions) {
            iCommandLineOptionsParse = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            int iCharCounter = 0;
            string sCurrentString = "";
            InputTldFileOptions = sPassedOutputFileOptions;

            TransformFileControl.OptionToDoLogActivity = bNO;
            TransformFileControl.OptionToDoProceedAutomatically = bNO;

            TransformFileControl.OptionToDoCheckFileDoesExist = bNO;
            TransformFileControl.OptionToDoCreateNewFile = bNO;
            TransformFileControl.OptionToDoCreateFileMustNotExist = bNO;
            TransformFileControl.OptionToDoDeleteFile = bNO;

            TransformFileControl.OptionToDoOverwriteExistingItem = bNO;
            TransformFileControl.OptionToDoCheckItemIdDoesExist = bNO;
            TransformFileControl.OptionToDoEnterEachItemId = bNO;
            TransformFileControl.OptionToDoConvertItem = bNO;

            TransformFileControl.ItemConvertItFlag = 1;
            TransformFileControl.ItemCreateItFlag = 0;


            while (iCharCounter < sPassedOutputFileOptions.Length) {
                sCurrentString = sPassedOutputFileOptions.Substring(iCharCounter, 1);
                switch (sCurrentString) {
                    case "C":
                        TransformFileControl.OptionToDoConvertItem = bON;
                        TransformFileControl.ItemConvertItFlag = 1;
                        break;
                    case "F":
                        TransformFileControl.OptionToDoCheckFileDoesExist = bON;
                        break;
                    case "N":
                        TransformFileControl.OptionToDoCreateNewFile = bON;
                        break;
                    case "D":
                        TransformFileControl.OptionToDoDeleteFile = bON;
                        break;
                    case "O":
                        TransformFileControl.OptionToDoOverwriteExistingItem = bON;
                        break;
                    case "E":
                        TransformFileControl.OptionToDoCheckItemIdDoesExist = bON;
                        break;
                    case "I":
                        TransformFileControl.OptionToDoEnterEachItemId = bON;
                        break;
                    case "L":
                        TransformFileControl.OptionToDoLogActivity = bON;
                        break;
                    case "A":
                        TransformFileControl.OptionToDoProceedAutomatically = bON;
                        break;
                    case "M":
                        TransformFileControl.OptionToDoCreateFileMustNotExist = bON;
                        TransformFileControl.ItemCreateItFlag = 1;
                        break;
                    default:
                        iCommandLineOptionsParse = (int)DatabaseControl.ResultUndefined;
                        LocalMessage = "Command Line Option (" + sCurrentString + ") does not exist";
                        MdmTraceDoPoint(iNoOp, iNoOp, iCommandLineOptionsParse, omCo.RunErrorDidOccur = true, iNoOp, iNoOp, bNO, MessageNoUserEntry, LocalMessage);
                        throw new NotSupportedException(LocalMessage);
                        break;
                }
                iCharCounter++;
            }

            iCommandLineOptionsParse = (int)DatabaseControl.ResultOK;
            return iCommandLineOptionsParse;
        }
        #endregion
        #region Processing Loops
        // ==================================================================
        // DO PROCESSING
        // ==================================================================
        // MAIN
        public long RunEngineMain() {
            RunEngineMainResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ObjectDoesNotExist;
            // process here
            RunEngineMainResult = Application_ProcessRun();

            return RunEngineMainResult;
        }
        // IMPORT ENGINE
        public long RunEngineDoRun(string RequestEngineAction) {
            RunEngineDoRunResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // process here
            RunEngineDoRunResult = Application_ProcessRun();
            return RunEngineDoRunResult;

        }
        // PROCESS FILE
        public long DoProcesFile(string RequestFileAction, string RequestFileNameIn, string RequestFileNameOut, string RequestFileItemIdOut, string RequestFileActionOptions) {
            iDoProcesFile = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // The files used here were created in the code example
            // in How to: Write to a Text File. You can of course substitute
            // other files of your own.

            // Example #1
            // bRead the file as one string.


            string text = System.IO.File.ReadAllText(@"C:\Users\public\TestFolder\WriteText.txt");

            // Display the file contents to the Console.
            System.Console.WriteLine("Contents of writeText.txt = " + text);

            // Example #2
            // bRead the file aslines into a string array.
            string[] salines = System.IO.File.ReadAllLines(@"C:\Users\public\TestFolder\WriteLines2.txt");

            System.Console.WriteLine("Contents of writeLines2.txt =:");
            foreach (string line in salines) {
                Console.WriteLine("\t" + line);
            }

            // Keep the Console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
            return iDoProcesFile;


        }
        // PROCESS COMMAND
        public long DoProcessCommand(string RequestFileAction, string RequestFileNameIn, string RequestFileNameOut, string RequestFileItemIdOut, string RequestFileActionOptions) {
            iDoProcessCommand = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // process here
            iDoProcessCommand = Application_ProcessRun();
            return iDoProcessCommand;
        }
        // PROCESS COMMAND LINE
        public long DoProcessCommandLine(string RequestFileCommandLine) {
            iDoProcessCommandLine = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // process here
            iDoProcessCommandLine = Application_ProcessRun();
            return iDoProcessCommandLine;
        }
        // APP PROCESS AN IMPORT
        public long Application_ProcessRun() {
            iApplication_ProcessRun = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            iApplication_ProcessRun = RunMain();
            return iApplication_ProcessRun;
        }
        #endregion
        // *:
        // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        // *:
        // Mdm Migration Utility - Top of Main
        // *:
        // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        protected long RunMain() {
            iMinputTldMain = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            #region Run Control Fields
            // Set State
            //
            for (int RunStateCurr = 0; RunStateCurr < iaRunActionState_Max + 5; RunStateCurr++) {
                for (int RunState_Value = 0; RunState_Value < 8; RunState_Value++) {
                    omCo.iaRunActionState[RunStateCurr, RunState_Value] = 0;
                }
            }

            iTraceIterationCount = 0;
            iTraceIterationCheckPointCount = 0;
            iTraceIterationCountTotal = 0;

            iTraceBugCount = 0;
            iTraceBugCheckPointCount = 0;
            iTraceBugCountTotal = 0;

            iTraceByteCountTotal = 0;
            iTraceByteCount = 0;

            iTraceDisplayCount = 0;
            iTraceDisplayCheckPointCount = 0;
            iTraceDisplayCountTotal = 0;
            iTraceCharacterCount = 0;
            iTracePercentCompleted = 0;

            long PickOpenResult = 0;
            // Set State
            omCo.RunAction = RunRunDo;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Doing;
            omCo.iaRunActionState[RunRunDo, RunState] = RunTense_Doing;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs(0,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            omCo.bRunStartPending = false;
            omCo.RunErrorDidOccur = false;
            omCo.iaRunActionState[RunAbort, RunState] = RunTense_Off;
            omCo.iaRunActionState[RunFirst, RunState] = RunTenseOn;
            omCo.iaRunActionState[RunReloop, RunState] = RunTenseOn;
            //
            #endregion
            #region Initialization
            // Set State
            omCo.RunAction = RunInitialize;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Doing;
            omCo.iaRunActionState[RunInitialize, RunState] = RunTense_Doing;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            #region Console and Run Control
            // Import Run Output (PickDb) Console Open
            ConsoleMdmConsolePickConsoleOpen();
            // Import Run Console Command Parameters
            sConsoleCommand = PickTclRead();
            // Import Run
            RunStatus = 0;
            // Import Interface to Page
            // // // omCo.omPm.ProgressBarMdm1.Value = 99999;
            // // // omCo.omPm.ProgressBarMdm1.Focus();
            // Import Run Console Action
            // sTempData = PickTrim(PickOconv(sTempData, "mcu"));
            sPickFileActionRequest = PickField(sConsoleCommand, " ", 1);
            #endregion
            #region Import Input File Initialization
            // Display Opening Output System
            //
            LocalMessage8 = "Mdm Migration Utility";
            MdmOutputPrint_PickPrint(1, "A1" + LocalMessage8, bYES);
            //
            LocalMessage8 = "Initializing Mdm Migration Utility";
            MdmOutputPrint_PickPrint(1, "A2" + LocalMessage8, bYES);
            //
            LocalMessage8 = "Please wait ...";
            MdmOutputPrint_PickPrint(1, "A2" + LocalMessage8, bYES);
            //

            // Import Run Input File Name
            TransformFileControl.InputFile.FileSummary.FileNameOriginal = omCo.InputFile.FileSummary.FileName;
            #region Import Input File Input Reset
            // Reset Input Dict and Data
            iMinputTldMain = ((PickDb)TransformFileControl.InputFile.PickDbObject).FileDataReset(TransformFileControl.InputFile);
            iMinputTldMain = ((PickDb)TransformFileControl.InputFile.PickDbObject).FileDictReset(TransformFileControl.InputFile);
            #region Import Input File Control and Modes
            // Mfile Input File Action Direction
            TransformFileControl.InputFile.FileSummary.FileActionDirection = (int)Mdm.Oss.FileUtil.Mfile.FileActionDirectionIs.Input;
            // Import Output File Read and Access Modes
            TransformFileControl.InputFile.FileReadMode = (int)DatabaseControl.ReadModeLINE; // TODO initialize
            TransformFileControl.InputFile.FileAccessMode = (int)DatabaseControl.ReadModeLINE; // TODO initialize
            #endregion
            // Import Input File Item Id
            TransformFileControl.InputFile.FileSummary.FileItemId = "";
            TransformFileControl.InputFile.FileSummary.FileItemIdCurrent = "";
            TransformFileControl.InputFile.FileSummary.FileItemIdNext = "";
            TransformFileControl.InputFile.FileSummary.FileItemIdIsChanged = bNO;
            TransformFileControl.InputFile.FileSummary.FileNameIsChanged = bNO;
            // Import Input File Item Data
            TransformFileControl.InputFile.FileItemData = "";
            // Import Input File Item Name
            TransformFileControl.InputFile.FileSummary.FileNameDefault = "tld.import";
            // Import Input File Options
            TransformFileControl.InputFile.FileSummary.FileOptions = "F";
            TransformFileControl.InputFile.FileReadMode = (int)DatabaseControl.ReadModeLINE;
            TransformFileControl.InputFile.FileAccessMode = (int)DatabaseControl.ReadModeLINE;
            // Import Run Column Attribute Index
            TransformFileControl.InputFile.FileBufferCharIndex = 1;
            TransformFileControl.InputFile.FileBufferCharItemEofIndex = 0;
            #endregion
            #endregion
            #region OutputFile Initialization
            // Import OutputFile Initialization
            iMinputTldMain = ((PickDb)TransformFileControl.InputFile.PickDbObject).FileDataReset(TransformFileControl.OutputFile);
            iMinputTldMain = ((PickDb)TransformFileControl.InputFile.PickDbObject).FileDictReset(TransformFileControl.OutputFile);
            //
            #region Output Data and Dictionay Index Pointers
            TransformFileControl.OutputFile.iDataItemAttributeCounter = 0;
            TransformFileControl.OutputFile.iDataItemAttributeIndex = 1;
            // Reset internal attribute pointer for next dict item
            TransformFileControl.OutputFile.FileDictItemIndex = 0;
            TransformFileControl.OutputFile.FileDictItemCount = 0;
            #endregion
            #region OutputFile Control and Modes
            // Mfile Output File Action Direction
            TransformFileControl.OutputFile.FileSummary.FileActionDirection = (int)Mdm.Oss.FileUtil.Mfile.FileActionDirectionIs.Output;
            // Import Output File Read and Access Modes ReadModeSQL
            TransformFileControl.OutputFile.FileReadMode = (int)DatabaseControl.ReadModeSQL;
            TransformFileControl.OutputFile.FileAccessMode = (int)DatabaseControl.ReadModeSQL;
            // TransformFileControl.OutputFile.FileReadMode = (int)DatabaseControl.ReadModeLINE;
            // TransformFileControl.OutputFile.FileAccessMode = (int)DatabaseControl.ReadModeLINE;
            // (TransformFileControl.OutputFile.ipFileReadMode == (int)DatabaseControl.ReadModeAll) // initialize
            #endregion
            #region OutputFile System,Datbase,ItemData
            // Import Run Output System
            TransformFileControl.OutputFile.FileSummary.FileActionDirection = (int)Mdm.Oss.FileUtil.Mfile.FileActionDirectionIs.Output;
            TransformFileControl.OutputFile.FileSummary.SystemName = omCo.OutputFile.FileSummary.SystemName;
            TransformFileControl.OutputFile.FileSummary.SystemObject = omCo.OutputFile.FileSummary.SystemObject;
            // Import Run Output Database
            TransformFileControl.OutputFile.FileSummary.DatabaseName = omCo.OutputFile.FileSummary.DatabaseName;
            TransformFileControl.OutputFile.FileSummary.DatabaseName = omCo.OutputFile.FileSummary.DatabaseName;
            TransformFileControl.OutputFile.FileSummary.DatabaseObject = omCo.OutputFile.FileSummary.DatabaseObject;
            // Import Run Output File Name
            TransformFileControl.OutputFile.FileSummary.FileNameOriginal = TransformFileControl.OutputFile.FileSummary.FileName;
            // Import File Item
            TransformFileControl.OutputFile.FileItemData = "";
            // TODO File Dictionary Initialization ?Complete?
            // Display Opening Output System
            LocalMessage8 = "Initializing Output File Information" + TransformFileControl.OutputFile.FileSummary.SystemName;
            MdmOutputPrint_PickPrint(2, "A1" + LocalMessage8, bYES);

            LocalMessage8 = "Database: \"" + OutputFile.FileSummary.DatabaseName + "\", please wait ...";
            MdmOutputPrint_PickPrint(2, "A2" + LocalMessage8, bYES);
            #endregion
            #region OutputOptions
            // Import Output File Store Options
            TransformFileControl.OutputFile.FileSummary.FileOptions = omCo.OutputFile.FileSummary.FileOptions;
            // Import Output Read Mode
            TransformFileControl.OutputFile.FileReadMode = (int)DatabaseControl.ReadModeSQL;
            // Import Output Access Mode
            TransformFileControl.OutputFile.FileAccessMode = (int)DatabaseControl.ReadModeSQL;
            // Import Run Options Get from Console Command
            // Import Run Options: options are the the right of the "(", hence the 2
            RunOptions = PickField(sConsoleCommand, "(", 2);
            // Import Run Options: options are the the left of the ")", hence the 1
            RunOptions = PickField(RunOptions, ")", 1);
            // Import Run Options: convert to upper case
            RunOptions = PickTrim(PickOconv(RunOptions, "mcu"));
            // Import Run Options: check for the "C"onvertable options
            RunOptions += TransformFileControl.OutputFile.FileSummary.FileOptions;
            // Import Run Options Parse RunOptions
            CommandLineOptionsParse(RunOptions);
            if (TransformFileControl.OptionToDoConvertItem == bON) {
                TransformFileControl.ItemConvertItFlag = 1;
            }
            if (TransformFileControl.OptionToDoCreateFileMustNotExist == bON) {
                TransformFileControl.ItemCreateItFlag = 1;
            }
            #endregion
            RunStatusCheckPause();
            #endregion
            // *:
            // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            // *
            // *  File Buffer handling
            // *:
            #region FileBufferFile
            // Import item is the second " " delimeted command attribute
            // the first attribue being "IMPORT" or the command itself.
            TransformFileControl.InputFile.FileSummary.FileName = PickField(sConsoleCommand, " ", 2);
            // not the right place to trim off "(options" really
            TransformFileControl.InputFile.FileSummary.FileName = PickField(TransformFileControl.InputFile.FileSummary.FileName, "(", 1);
            // TransformFileControl.InputFile.FileSummary.FileName = PickTrim(PickOconv(TransformFileControl.InputFile.FileSummary.FileName, "mcu"));
            if (TransformFileControl.InputFile.FileSummary.FileName.Length == 0 && omCo.InputFile.FileSummary.FileName.Length > 0) {
                // use the supplied Import File Name
                TransformFileControl.InputFile.FileSummary.FileName = omCo.InputFile.FileSummary.FileName;
                // TransformFileControl.FileSummary.FileName = "";
                if (TransformFileControl.OutputFile.FileSummary.FileName.Length == 0 && TransformFileControl.OutputFile.FileSummary.FileName.Length > 0) {
                    TransformFileControl.OutputFile.FileSummary.FileOptions = InputTldFileOptions;
                }
                // use the supplied Import File Name
                // TransformFileControl.OutputFile.FileSummary.FileName = "";
                if (TransformFileControl.OutputFile.FileSummary.FileItemId.Length == 0 && TransformFileControl.OutputFile.FileSummary.FileItemId.Length > 0) {
                    TransformFileControl.OutputFile.FileSummary.FileItemId = omCo.OutputFile.FileSummary.FileItemId;
                }
                // TransformFileControl.FileItemId = "";
            }
            #endregion
            RunStatusCheckPause();
            #endregion
            // *:
            // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            // TODO PROCESSING START
            //
            #region Initialize - Start Processing
            // Import
            // Control Fields
            // Set State
            omCo.RunAction = RunStart;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Did;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            // Set State
            omCo.RunAction = RunMain_Do;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Doing;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            IterationFirst = bYES;
            //
            // InputFile
            // InputFileName
            // TransformFileControl.InputFile.FileSummary.FileName = "";
            // TransformFileControl.InputFile.FileSummary.FileName = TransformFileControl.FileSummary.FileNameOriginal;
            // TODO TransformFileControl.InputFile.FileSummary.FileName = TransformFileControl.FileSummary.FileName;
            // OLD TransformFileControl.InputFile.FileSummary.FileName = TransformFileControl.InputFile.FileSummary.FileName;
            // ItemData
            // InputFileItem
            TransformFileControl.InputFile.FileItemData = ""; // File Item Data
            TransformFileControl.InputFile.FileSummary.FileItemIdCurrent = ""; // Current Id
            TransformFileControl.InputFile.FileSummary.FileItemIdNext = ""; // Next Id
            TransformFileControl.InputFile.FileSummary.FileItemId = ""; // This Id
            // Ascii Attribute Pointers
            TransformFileControl.InputFile.iDataItemAttributeIndex = 1; // Current Attribute
            TransformFileControl.InputFile.iDataItemAttributeMaxIndex = 0; // Total Attributes in Item
            //
            TransformFileControl.InputFile.iDataItemAttributeEos2Index = 0; // End of Column Separator 2
            TransformFileControl.InputFile.iDataItemAttributeEos1Index = 0; // End of Column Separator 1
            TransformFileControl.InputFile.iDataItemAttributeEosIndex = 0; // End of Column Sub-Value
            TransformFileControl.InputFile.iDataItemAttributeEovIndex = 0; // End of Column Value-
            TransformFileControl.InputFile.iDataItemAttributeEoaIndex = 0; // End of Column
            TransformFileControl.InputFile.iDataItemAttributeEorIndex = 0; // End of Row
            TransformFileControl.InputFile.iDataItemAttributeEofIndex = 0; // End of File
            // Working value
            TransformFileControl.InputFile.FileWorkBuffer = "";
            TransformFileControl.InputFile.iDataItemAttributeCounter = 0; // Data Items in Item / Row / Item
            TransformFileControl.InputFile.iDataItemAttributeMaxIndexTemp = 0; // Total Attributes in Item
            // Character Pointers
            TransformFileControl.InputFile.iDataItemCharEobIndex = 0; // End of Character Buffer
            TransformFileControl.InputFile.iDataItemCharIndex = 0; // Character Pointer
            TransformFileControl.InputFile.iDataItemCharEofIndex = 0; // Character End of File
            // InputFileBuffer
            TransformFileControl.InputFile.FileBufferBytesRead = 0;
            TransformFileControl.InputFile.FileBufferBytesReadTotal = 0;
            // Conversion results
            TransformFileControl.InputFile.FileBufferBytesConverted = 0;
            TransformFileControl.InputFile.FileBufferBytesConvertedTotal = 0;
            // TODO InputFileType
            TransformFileControl.InputFile.FileSummary.FileTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeTEXT;
            TransformFileControl.InputFile.FileSummary.FileSubTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE;
            // Counters
            TransformFileControl.InputFile.FileBufferCharactersFound = 0;
            TransformFileControl.InputFile.FileBufferCurrentAttributeCounter = 0;
            TransformFileControl.InputFile.FileBufferCharMaxIndex = 0;
            //
            // for comma delimited rows of pick items
            // FileSubType = "Tilde_Csv";  // initialize
            // FileSubTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_CSV;
            // for regular Cr delimited pick items
            // FileSubType = "Tilde";
            // FileSubTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE;

            // Output
            // TODO Output FileType
            TransformFileControl.OutputFile.FileSummary.FileTypeId = (int)DatabaseControl.FileDictData;
            TransformFileControl.OutputFile.FileSummary.FileSubTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeSQL;
            // TODO OutputFileItem
            TransformFileControl.OutputFile.FileItemData = ""; // File Item Data
            TransformFileControl.OutputFile.FileSummary.FileItemIdCurrent = ""; // Current Id
            TransformFileControl.OutputFile.FileSummary.FileItemIdNext = ""; // Next Id
            TransformFileControl.OutputFile.FileSummary.FileItemId = ""; // This Id
            // OutputFileName
            // TransformFileControl.OutputFile.FileSummary.FileName = TransformFileControl.OutputFile.FileSummary.FileName;
            // OutputFileItem
            TransformFileControl.OutputFile.FileItemData = ""; // Output File Item
            TransformFileControl.OutputFile.FileSummary.FileItemIdNext = ""; // Reset Next
            // TODO # Need to client file data on each new output item     
            // TODO # TransformFileControl.OutputFile.FileSummary.FileSubTypeId = TransformFileControl.FileSubTypeId;
            // Counters
            TransformFileControl.OutputFile.FileBufferCharactersFound = 0;
            TransformFileControl.OutputFile.FileBufferCurrentAttributeCounter = 0;
            TransformFileControl.OutputFile.FileBufferCharMaxIndex = 0;
            // Display
            if (omCo.OptionToDoLogActivity) {
                // Console application not written
                // TODO Console disk output not correct
                // TODO route pick Console to disk not os Console
                // ConsoleOn = bON;
                ConsolePickConsoleOn = bON;
            }
            // ProgressBar opLocalProgressBar = new ProgressBar();
            //
            #region Clear Messages
            LocalMessage = "";
            LocalMessage0 = "";
            LocalMessage1 = "";
            LocalMessage2 = "";
            LocalMessage3 = "";
            LocalMessage4 = "";
            LocalMessage5 = "";
            LocalMessage6 = "";
            LocalMessage7 = "";
            LocalMessage8 = "";
            LocalMessage9 = "";
            #endregion
            //
            LocalMessage8 = "Processing File (Load & Open), please wait...";
            MdmOutputPrint_PickPrint(2, "A1" + LocalMessage8, bYES);
            //
            LocalMessage8 = "Opening Output System: \"" + TransformFileControl.OutputFile.FileSummary.SystemName + "\", ";
            LocalMessage8 += "Database: \"" + TransformFileControl.OutputFile.FileSummary.DatabaseName + "\"" + ", please wait ...";
            MdmOutputPrint_PickPrint(2, "A2" + LocalMessage8, bYES);

            LocalMessage8 = "File Name: \"" + TransformFileControl.OutputFile.FileSummary.FileName + "\", ";
            LocalMessage8 += "Item name: \"" + TransformFileControl.OutputFile.FileSummary.FileItemId + "\"" + ", please wait ...";
            MdmOutputPrint_PickPrint(2, "A2" + LocalMessage8, bYES);
            //
            #endregion // of Start Processing
            // Set State
            omCo.RunAction = RunStart;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Did;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            // Set State
            omCo.RunAction = RunInitialize;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Doing;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            #region Process Next Run
            // MAIN PROCESSING
            // Set State
            omCo.RunAction = RunMain_Do;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Doing;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            // RELOOP 
            // Set State
            omCo.RunAction = RunReloop;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_DoNot;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            LocalMessage8 = "Process Next Run";
            MdmOutputPrint_PickPrint(1, "A2" + LocalMessage8, bYES);
            RunStatusCheckPause();
            // *:
            // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            // Process Next Run
            //
            #region User File Input and Validation
            do {
                RunStatusCheckPause();
                // Control
                // RELOOP 
                // Set State
                omCo.RunAction = RunReloop;
                omCo.RunMetric = RunState;
                omCo.RunTense = RunTense_DoNot;
                omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
                omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                    "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
                omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
                //
                #region Input File
                // Ascii NO File
                if (TransformFileControl.InputFile == null) {
                    if (TransformFileControl.InputFile != null && TransformFileControl.InputFile.FileSummary.FileName.Length == 0) {
                        // use the supplied Import File Name
                        // TransformFileControl.InputFile.FileSummary.FileName = TransformFileControl.InputFile..FileSummary.FileName;
                        // OLD TransformFileControl.InputFile.FileSummary.FileOptions = TransformFileControl.InputFile.FileSummary.InputTldFileOptions = "";

                    }
                }
                // Ascii Display
                LocalMessage8 = "Getting Input File Name.";
                MdmOutputPrint_PickPrint(3, "A2" + LocalMessage8, bYES);
                // Ascii NO File
                if (TransformFileControl.InputFile.FileSummary.FileName == "") {
                    // xxxxxxxxxxxxxxxxxxxxxxxxxxxx 2 ;
                    RunStatus = 888;
                    sLocalErrorMessage = "Syntax - IMPORT DospFileName PickFileName ItemName (Options}";
                    MdmOutputPrint_PickPrint(3, "A2" + sLocalErrorMessage, bYES);
                    MdmTraceDoPoint(iNoOp, iNoOp, iMinputTldMain, omCo.RunErrorDidOccur = bYES, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + sLocalErrorMessage + "\n");
                    //
                    // ??? STOP ????
                    //
                    // PickStop();
                }
                // Ascii NO File
                if (TransformFileControl.InputFile != null) {
                    TransformFileControl.InputFile.FileSummary.FileOptions = "F";
                }
                // Import No File
                if (TransformFileControl.InputFile.FileSummary.FileName == "") {
                    // goto 1;
                    omCo.iaRunActionState[RunReloop, RunState] = RunTense_Do;
                }
                RunStatusCheckPause();
                #endregion
                #region Output File
                // Output No File
                if (TransformFileControl.OutputFile.FileSummary.FileName == "") {
                    sTempData = "";
                    // Output item is the third " " delimeted command attribute
                    TransformFileControl.OutputFile.FileSummary.FileName = PickField(sConsoleCommand, " ", 3);

                }
                // Output Prompt user for output filename
                if (TransformFileControl.OutputFile.FileSummary.FileName == "") {
                    sPromptText = "to:(";
                    sPromptChar = " ";
                    sPromptDefaultResponse = "";
                    iPromptColumn = 0;
                    iPromptRow = 23;

                    sPromptResponse = PickInput(1, bNO, iPromptColumn, iPromptRow, sPromptText, sPromptDefaultResponse);
                    TransformFileControl.OutputFile.FileSummary.FileName = sPromptResponse;
                    if (TransformFileControl.OutputFile.FileSummary.FileName.Length == 0) {
                        RunStatus = 888;
                        sLocalErrorMessage = "Syntax - IMPORT DospFileName PickFileName ItemName (Options}";
                        MdmOutputPrint_PickPrint(3, "A2" + sLocalErrorMessage, bYES);
                        MdmTraceDoPoint(iNoOp, iNoOp, iMinputTldMain, omCo.RunErrorDidOccur = bYES, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + sLocalErrorMessage + "\n");
                        // PickStop();
                    } else {
                        // use the supplied Import File Name
                        // TransformFileControl.OutputFile.FileSummary.FileName = TransformFileControl.OutputFile.FileSummary.FileName;
                        // TransformFileControl.OutputFile = TransformFileControl.OutputFile;
                        omCo.iaRunActionState[RunReloop, RunState] = RunTenseOn;
                    }
                }
                RunStatusCheckPause();
                #endregion
                #region Output File Item Id
                //
                // Item Id parsing (from the Console sLine)
                if (TraceOn | ConsoleOn | ConsolePickConsoleOn | ConsolePickConsoleBasicOn) {
                    LocalMessage8 = "Item Id parsing (from the Console sLine)";
                    MdmOutputPrint_PickPrint(3, "A2" + LocalMessage8, bYES);
                }

                sTempData = sConsoleCommand;
                sTempData = PickField(sConsoleCommand, "(", 1);
                // process item id 4th attribute
                TransformFileControl.OutputFile.FileSummary.FileItemId = PickField(sTempData, " ", 4);
                if (TransformFileControl.OutputFile.FileSummary.FileItemId == "") { TransformFileControl.OutputFile.FileSummary.FileItemId = "DUMMY"; };
                if (TransformFileControl.InputFile.FileSummary.FileItemId == "" | TransformFileControl.InputFile.FileSummary.FileItemId == "DUMMY") {
                    // some very bizare snytax options here
                    // used to set output item id
                    // appears to take text after last "\"
                    // and then after a : as the item id
                    // ipPickDictItemGet forget if it is file:item or dictfile:datafile?????
                    // ie c:\dir\filename:itemid
                    TransformFileControl.InputFile.FileBufferCharCounter = PickAttributeCountGet(TransformFileControl.InputFile.FileSummary.FileName, "\\");
                    sTemp = PickField(TransformFileControl.InputFile.FileSummary.FileName, "\\", TransformFileControl.InputFile.FileBufferCharCounter + 1);
                    sTemp1 = PickField(sTemp, ":", 1);
                    sField_ConversionRootVerb = PickField(sTemp1, ":", 2);
                    if (sField_ConversionRootVerb.Length > 0) {
                        if (sTemp1.Length > 0) {
                            // use the supplied Import File Name
                            TransformFileControl.OutputFile.FileSummary.FileName = sTemp1;
                            TransformFileControl.OutputFile = null;
                            TransformFileControl.InputFile.FileSummary.FileItemId = sField_ConversionRootVerb;
                        }
                    }
                }
                // ItemIds
                if (TransformFileControl.InputFile.FileSummary.FileItemId.Length > 0 && TransformFileControl.OutputFile.FileSummary.FileItemId.Length > 0) {
                    // conflicting output item ids
                    // prompt
                }
                if (TransformFileControl.InputFile.FileSummary.FileItemId == "" || TransformFileControl.InputFile.FileSummary.FileItemId == "DUMMY") {
                    if (omCo.OutputFile.FileSummary.FileItemId.Length > 0) {
                        TransformFileControl.InputFile.FileSummary.FileItemId = omCo.OutputFile.FileSummary.FileItemId;
                    }
                }
                if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) {
                    LocalMessage8 = "Reloop to try alternate file names or prompt";
                    MdmOutputPrint_PickPrint(3, "A2" + LocalMessage8, bYES);
                }
                RunStatusCheckPause();
                #endregion
            } while (omCo.iaRunActionState[RunReloop, RunState] == RunTenseOn && (!omCo.bRunAbort && !omCo.bRunCancelPending)); // Reloop to try alternate file names or prompt;
            // Set State
            omCo.RunAction = RunUserInput;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Did;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            RunStatusCheckPause();
            #endregion
            #endregion
            #region Open Files
            // Set State
            omCo.RunAction = RunOpen;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Doing;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            #region Open and Load
            // sPassedOutputFileOptions = "";
            // InputTldFileOptions = "";
            // RunOptions = TransformFileControl.OutputFile.FileSummary.FileOptions;
            // TransformFileControl.InputFile.FileOptions = InputTldFileOptions;
            // TransformFileControl.OutputFile.FileSummary.FileOptions = InputTldFileOptions;
            //
            // Control
            RunStatus = 0;
            // Display
            LocalMessage8 = "Opening DOS file, please wait ...";
            MdmOutputPrint_PickPrint(2, "A2" + LocalMessage8, bYES);
            // *:
            // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            // ! TODO Opening Ascii DOS file
            // *:
            if (TransformFileControl.InputFile != null) {
                TransformFileControl.InputFile.FileReadMode = (int)DatabaseControl.ReadModeAll;
                TransformFileControl.InputFile.FileWriteMode = (int)DatabaseControl.ReadModeAll;
                TransformFileControl.InputFile.FileAccessMode = (int)DatabaseControl.ReadModeAll;
                // ofPassedFileObject.ipFileReadMode = (int)DatabaseControl.ReadModeLINE;
                TransformFileControl.InputFile.FileSummary.FileOptions = "F";
                sProcessSubHeading = "\"" + PickOconv(TransformFileControl.InputFile.FileSummary.FileName, "R#15") + "\"";
                LocalMessage8 = "A2" + "";
                switch (TransformFileControl.InputFile.FileReadMode) {
                    case ((int)DatabaseControl.ReadModeBLOCK):
                        LocalMessage8 += "Buffer SEEK - Ascii File.";
                        PickOpenResult = ((PickDb)TransformFileControl.InputFile.PickDbObject).FileBufferFileOpenHandle(TransformFileControl.InputFile, TransformFileControl.InputFile.FileSummary.FileName, (int)Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.FileIoAccessReadOnly, 0);
                        break;
                    case ((int)DatabaseControl.ReadModeLINE):
                        LocalMessage8 += "Read LINE - Ascii File.";
                        PickOpenResult = ((PickDb)TransformFileControl.InputFile.PickDbObject).FileBufferFileOpenMfile(TransformFileControl.InputFile, TransformFileControl.InputFile.FileSummary.FileName, (int)Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.FileIoAccessReadOnly, 0);
                        break;
                    case ((int)DatabaseControl.ReadModeAll):
                        LocalMessage8 += "Read ALL - Ascii File.";
                        PickOpenResult = ((PickDb)TransformFileControl.InputFile.PickDbObject).FileBufferFileOpenMfile(TransformFileControl.InputFile, TransformFileControl.InputFile.FileSummary.FileName, (int)Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.FileIoAccessReadOnly, 0);
                        break;
                    case ((int)DatabaseControl.ReadModeSQL):
                        LocalMessage8 += "Read SQL - Import File.";
                        PickOpenResult = ((PickDb)TransformFileControl.InputFile.PickDbObject).FileBufferFileOpenMfile(TransformFileControl.InputFile, TransformFileControl.InputFile.FileSummary.FileName, (int)Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.FileIoAccessReadOnly, 0);
                        break;
                    default:
                        iMinputTldMain = (int)DatabaseControl.ReadModeError;
                        LocalMessage = "File Read Method (" + TransformFileControl.InputFile.FileReadMode.ToString() + ") is not set for " + sTemp + "!!!";
                        LocalMessage8 += LocalMessage;
                        break;
                }
                if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(iNoOp, iNoOp, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, LocalMessage8 + "\n"); }
            }
            //
            if (TransformFileControl.InputFile == null) {
                // Opening Ascii DOS file FAILED
                RunStatus = 888;
                LocalMessage = "ABORT: Error [" + PickSystemCallString(0) + "] - Unable to open MS-DOS file \"" + TransformFileControl.InputFile.FileSummary.FileName + "\".";
                MdmOutputPrint_PickPrint(3, "A2" + LocalMessage, bYES);
                MdmTraceDoPoint(iNoOp, iNoOp, iMinputTldMain, omCo.RunErrorDidOccur = bYES, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + sLocalErrorMessage + "\n");
                // ABORT STOP HERE
                // PickStop();
            }
            RunStatusCheckPause();
            #endregion
            #region TODO Prepare Ascii Data for OpenFileOuputType
            //
            // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            // *:
            // !; Ascii DOS file EXISTS
            // *:
            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(iNoOp, iNoOp, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bYES, MessageNoUserEntry, "A2" + "Ascii DOS file EXISTS" + "\n"); }
            // Notes: Both Import and Output Files have both
            // a File Type and Sub Type.  In addition, there
            // is a bRead, Write and Access Mode for 
            // each file.
            //
            // bRead Mode include Block, Line, All and Sql.
            // 
            // Note: Write Mode would include Append, Replace.
            //
            // Note: Access Mode might be ReadOnly or other
            // standard file access modes.
            //
            // Ascii Control
            TransformFileControl.InputFile.iCurrentOffset = 0;
            TransformFileControl.InputFile.FileBufferCharIndex = 1;  // Ascii File Open
            TransformFileControl.InputFile.FileBufferCharItemEofIndex = 0;
            //
            // INPUT FILE TYPE
            TransformFileControl.InputFile.FileSummary.FileType = "PickSyntax";
            // FileType = TransformFileControl.InputFile.FileSummary.FileType;
            TransformFileControl.InputFile.FileSummary.FileTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypePICK;
            // FileTypeId = TransformFileControl.InputFile.FileSummary.FileTypeId;
            // SUB TYPE
            TransformFileControl.InputFile.FileSummary.FileSubType = "Tilde";
            // FileSubType = TransformFileControl.InputFile.FileSummary.FileSubType;
            TransformFileControl.InputFile.FileSummary.FileSubTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE;
            // FileSubTypeId = TransformFileControl.InputFile.FileSummary.FileSubTypeId;
            // Output
            // TODO Output File Object Read Modes should be loaded from user entry
            // TODO Complete work on write and access modes later.
            TransformFileControl.OutputFile.FileReadMode = (int)DatabaseControl.ReadModeSQL;
            TransformFileControl.OutputFile.FileWriteMode = (int)DatabaseControl.ReadModeSQL;
            TransformFileControl.OutputFile.FileAccessMode = (int)DatabaseControl.ReadModeSQL;
            // File Column Control
            TransformFileControl.OutputFile.iColumnInvalid = 0;
            TransformFileControl.FileColumnQuoteBool = bNO;
            TransformFileControl.FileColumnQuoteType = TransformFileControl.FileColumnQuoteDefault;
            // File Characters Control
            TransformFileControl.FileColumnEscapedBool = bNO;
            TransformFileControl.FileColumnEscapedType = (int)Mdm.Oss.FileUtil.FileTransformControDef.FileColumnEscapedIs.FileColumnEscapedNEWLINE;
            //
            // File Output Data Type
            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(iNoOp, iNoOp, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bYES, MessageNoUserEntry, "A2" + "File Output Data Type: (" + TransformFileControl.OutputFile.FileSummary.FileTypeId.ToString() + ")" + "\n"); }
            TransformFileControl.InputFile.FileSummary.FileSubTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE;
            // File Output Data Sub Type
            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(iNoOp, iNoOp, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bYES, MessageNoUserEntry, "A2" + "File Output Data Sub Type: (" + TransformFileControl.OutputFile.FileSummary.FileSubTypeId.ToString() + ")" + "\n"); }
            switch (TransformFileControl.InputFile.FileSummary.FileSubTypeId) {
                case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeCSV):
                    TransformFileControl.FileColumnQuoteBool = bYES;
                    TransformFileControl.FileLineIsColumn = bNO;
                    TransformFileControl.FileLineIsRow = bYES;
                    break;
                case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXT):
                case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeASC):
                case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeFIX):
                    TransformFileControl.FileColumnQuoteBool = bNO;
                    TransformFileControl.FileLineIsColumn = bNO;
                    TransformFileControl.FileLineIsRow = bYES;
                    break;
                case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE):
                    TransformFileControl.FileColumnQuoteBool = bNO;
                    TransformFileControl.FileLineIsColumn = bYES;
                    TransformFileControl.FileLineIsRow = bNO;
                    break;
                case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_NATIVE):
                case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_NATIVE_ONE):
                    TransformFileControl.FileColumnQuoteBool = bNO;
                    TransformFileControl.FileLineIsColumn = bNO;
                    TransformFileControl.FileLineIsRow = bNO;
                    break;
                default:
                    iMinputTldMain = (int)DatabaseControl.ResultUndefined;
                    TransformFileControl.FileColumnQuoteBool = bNO;
                    TransformFileControl.FileLineIsColumn = bNO;
                    TransformFileControl.FileLineIsRow = bNO;
                    omCo.RunErrorDidOccur = bYES;
                    LocalMessage = "Error - File Subtype (" + TransformFileControl.OutputFile.FileSummary.FileSubTypeId.ToString() + ") not properly set";
                    MdmOutputPrint_PickPrint(3, "A2" + LocalMessage, bYES);
                    MdmTraceDoPoint(iNoOp, iNoOp, iMinputTldMain, 
                        omCo.RunErrorDidOccur = bYES, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + sLocalErrorMessage + "\n");
                    break;
            }
            RunStatusCheckPause();
            #endregion
            #region TODO OpenOutputFile
            // Display
            PickDisplayLineNumber = 0;
            LocalMessage8 = "Copying data to PICK, please wait ...";
            MdmOutputPrint_PickPrint(1, "A1" + LocalMessage8, bYES);
            // *:
            // ! TODO Open Output (PICK) file;
            // *:
            TransformFileControl.OutputFile.FileItemData = ""; // Output File Item
            TransformFileControl.OutputFile.FileReadMode = (int)DatabaseControl.ReadModeSQL;
            //
            // Open
            PickOpenResult = ((PickDb)TransformFileControl.OutputFile.PickDbObject).PickFileAction(TransformFileControl.OutputFile, TransformFileControl.OutputFile.FileSummary.FileName, (int)Mdm.Oss.FileUtil.Mfile.FileActionDirectionIs.Output, TransformFileControl.OutputFile.FileReadMode, bYES);
            // PickOpenResult = PickFileAction();
            #endregion
            // Set State
            omCo.RunAction = RunOpen;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Did;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            RunStatusCheckPause();
            #endregion
            #region ItemReadAndProcess
            //
            #region Initialize Input Ascii Data Item and Buffer
            // Attribute Handling for Ascii and Text
            // Ascii Attribute Pointers
            TransformFileControl.InputFile.iDataItemAttributeIndex = 1; // Current Attribute
            TransformFileControl.InputFile.iDataItemAttributeMaxIndex = 0; // Total Attributes in Item
            //
            TransformFileControl.InputFile.iDataItemAttributeEos2Index = 0; // Current Column Separator 2
            TransformFileControl.InputFile.iDataItemAttributeEos1Index = 0; // Current Column Separator 1
            TransformFileControl.InputFile.iDataItemAttributeEosIndex = 0; // Current Column Sub-Value
            TransformFileControl.InputFile.iDataItemAttributeEovIndex = 0; // Current Column Value
            TransformFileControl.InputFile.iDataItemAttributeEoaIndex = 0; // Current Column
            TransformFileControl.InputFile.iDataItemAttributeEorIndex = 0; // Current Row
            TransformFileControl.InputFile.iDataItemAttributeEofIndex = 0; // Current File
            // Working value
            TransformFileControl.InputFile.iDataItemAttributeMaxIndexTemp = 0;
            TransformFileControl.InputFile.iDataItemAttributeCounter = 0; // Data Items in Item / Row / Item
            // Character Pointers
            TransformFileControl.InputFile.iDataItemCharEobIndex = 0; // End of Character Buffer
            TransformFileControl.InputFile.iDataItemCharIndex = 0; // Character Pointer
            TransformFileControl.InputFile.iDataItemCharEofIndex = 0; // Character End of File
            //
            // File Buffer
            // TransformFileControl.InputFile.FileWorkBuffer
            // TransformFileControl.InputFile.FileWorkBuffer = TransformFileControl.InputFile.FileBufferExistingItem;
            // TransformFileControl.InputFile.FileWorkBuffer = TransformFileControl.InputFile.FileIOAll;
            //
            // TransformFileControl.OutputFile.FileBufferCharIndex
            // TransformFileControl.InputFile.FileBufferCharMaxIndex
            //
            // File Seek Mode Processing
            // for binary and image type files where
            // there is no distinct lines, rows or attributes
            //
            // TransformFileControl.InputFile.FileBufferAttributeMaxIndex
            //
            // ! Loop to READ and process DOS InputFile.FileItemData;
            TransformFileControl.InputFile.FileBufferReadFileError = bNO;
            TransformFileControl.InputFile.FileBufferReadFileIsAtEnd = bNO;
            TransformFileControl.InputFile.FileBufferHasCharacters = bNO;
            // TransformFileControl.InputFile.FileBufferBytesRead = TransformFileControl.InputFile.iRecordSize, Line or All;
            //
            // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            //
            // TODO Processing loop for Ascii Read
            //
            //
            LocalMessage8 = "Processing File (Reading), please wait...";
            MdmOutputPrint_PickPrint(2, "A1" + LocalMessage8, bYES);
            //
            TransformFileControl.InputFile.FileBufferReadFileCounter = 0;
            iIterationCount = 0;
            TransformFileControl.InputFile.FileBufferReadFileIsAtEnd = false;
            //
            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) {
                LocalMessage8 = "Processing loop for Ascii Read";
                MdmOutputPrint_PickPrint(3, "A2" + LocalMessage8, bYES);
            }
            // Set State
            omCo.RunAction = RunStart;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Did;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            #endregion
            //
            while (
                (!TransformFileControl.InputFile.FileBufferReadFileIsAtEnd 
                    || TransformFileControl.InputFile.FileBufferHasCharacters)
                || (TransformFileControl.InputFile.iDataItemAttributeIndex <= TransformFileControl.InputFile.iDataItemAttributeMaxIndexTemp
                    && TransformFileControl.InputFile.FileItemData.Length > 0) 
                && (!omCo.bRunAbort && !omCo.bRunCancelPending)) {
                RunStatusCheckPause();
                //
                // Read More Buffer Data Information In
                //
                if (!TransformFileControl.InputFile.FileBufferReadFileIsAtEnd) {
                    // Read next buffer portion
                    //
                    MinputTldMainBufferRead(TransformFileControl.InputFile);
                    //
                    #region BufferNotEmpty
                    // 
                    // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                    // 
                    // Buffer contains data to process
                    //
                    // TODO Process Buffer Rows Area
                    // *:
                    if (TransformFileControl.InputFile.FileBufferHasCharacters) {
                        // if (TransformFileControl.InputFile.FileBufferBytesRead > 0) {
                        // More characters to process from buffer
                        // Dim InputFile.FileItemData(TransformFileControl.InputFile.FileBufferCharIndex);
                        LocalMessage8 = "Processing File (Buffer), please wait...";
                        MdmOutputPrint_PickPrint(2, "A1" + LocalMessage8, bYES);
                        //
                        LocalMessage8 = "More characters to process from buffer ...";
                        MdmOutputPrint_PickPrint(3, "A2" + LocalMessage8, bYES);
                        //
                        //
                        RunStatusCheckPause();
                        #region BufferConvertYes
                        // COVERT AREA
                        // Convert ItFlag
                        if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + "Check Convert ItFlag" + "\n"); }
                        //
                        if (TransformFileControl.ItemConvertItFlag != 0) {
                            //
                            TransformFileControl.InputFile.FileBufferBytesConverted = TransformFileControl.InputFile.FileBufferBytesRead;
                            TransformFileControl.InputFile.FileBufferBytesConvertedTotal += TransformFileControl.InputFile.FileBufferBytesRead;
                            //
                            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { 
                                MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + "Characters to process with Convert It Flag" + "\n"); }
                            // TransformFileControl.InputFile.FileSummary.FileType;
                            // TransformFileControl.InputFile.FileSummary.FileSubType;
                            if (
TransformFileControl.InputFile.FileSummary.FileTypeId 
        == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeTEXT 
    && (
    TransformFileControl.InputFile.FileSummary.FileSubTypeId
            == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXT 
        || TransformFileControl.InputFile.FileSummary.FileSubTypeId 
            == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_CSV
        || TransformFileControl.InputFile.FileSummary.FileSubTypeId
            == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeASC
    )
                                ) {
                                ColumnSeparator = Comma;
                                RowSeparator = Cr;
                                LfRemoveItFlag = bYES;
                                TldEscaped = bNO;

                            } else if (
TransformFileControl.InputFile.FileSummary.FileTypeId 
        == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeTEXT 
    && TransformFileControl.InputFile.FileSummary.FileSubTypeId
        == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE
                                ) {
                                ColumnSeparator = Cr;
                                RowSeparator = Tld;
                                LfRemoveItFlag = bYES;
                                TldEscaped = bYES;

                            } else if (
TransformFileControl.InputFile.FileSummary.FileTypeId
        == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypePICK 
    && (TransformFileControl.InputFile.FileSummary.FileSubTypeId 
        == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_ROW)
                                ) {
                                ColumnSeparator = Am;
                                RowSeparator = Tld;
                                LfRemoveItFlag = bYES;
                                TldEscaped = bYES;

                            } else if (
TransformFileControl.InputFile.FileSummary.FileTypeId
        == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypePICK 
    && (TransformFileControl.InputFile.FileSummary.FileSubTypeId 
        == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_NATIVE 
    || TransformFileControl.InputFile.FileSummary.FileSubTypeId 
        == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_NATIVE_ONE)
                                ) {
                                ColumnSeparator = Am;
                                RowSeparator = Tld;
                                LfRemoveItFlag = bOFF;
                                TldEscaped = bYES;
                            }
                            //
                            // Transfer Characters After end of Column
                            // Convert Lf's and Attribute Separators
                            if (LfRemoveItFlag) { TransformFileControl.InputFile.FileWorkBuffer = PickConvert(Lf, "", TransformFileControl.InputFile.FileWorkBuffer); }
                            //
                            // Convert Cr's to Attribute Sparators???
                            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + "Convert Cr's to Attribute Sparators" + "\n"); }
                            TransformFileControl.InputFile.FileWorkBuffer = PickConvert(Cr, Am, TransformFileControl.InputFile.FileWorkBuffer);
                            ColumnSeparator = Am;
                            // 
                            // Set Attributes to PROCESS and length
                            TransformFileControl.InputFile.FileBufferCharMaxIndex = TransformFileControl.InputFile.FileWorkBuffer.Length;
                            //
                            TransformFileControl.InputFile.FileBufferBytesReadTotal -= TransformFileControl.InputFile.FileBufferBytesRead;
                            TransformFileControl.InputFile.FileBufferBytesRead = TransformFileControl.InputFile.FileWorkBuffer.Length; // Adjust after conversion
                            TransformFileControl.InputFile.FileBufferBytesReadTotal += TransformFileControl.InputFile.FileBufferBytesRead;
                            //
                            if (TransformFileControl.InputFile.FileBufferCharMaxIndex > 0) {
                                if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + "Current Column Last Character Location: (" + TransformFileControl.InputFile.FileBufferCharMaxIndex.ToString() + ")" + "\n"); }
                                // Count Current Column Attributes
                                TransformFileControl.InputFile.FileBufferAttributeMaxIndex = PickAttributeCountGet(TransformFileControl.InputFile.FileWorkBuffer, ColumnSeparator);
                                if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + "Count Current Column Attributes: (" + TransformFileControl.InputFile.FileBufferAttributeMaxIndex.ToString() + ")" + "\n"); }
                                // Check End of Buffer
                                // TransformFileControl.InputFile.FileBufferFileItemIsAtEnd = bNO;
                                //
                            } else {
                                if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + "End of File Buffer" + "\n"); }
                                TransformFileControl.InputFile.FileBufferAttributeMaxIndex = 0;
                                // TransformFileControl.InputFile.FileBufferFileItemIsAtEnd = bYES;
                            }
                            //
                        } else {
                            // TransformFileControl.InputFile.FileBufferBytesConverted = TransformFileControl.InputFile.FileBufferBytesRead;
                            // TransformFileControl.InputFile.FileBufferBytesConvertedTotal += TransformFileControl.InputFile.FileBufferBytesRead;
                            TransformFileControl.InputFile.FileBufferBytesConverted = 0;
                            TransformFileControl.InputFile.FileBufferBytesConvertedTotal = 0;
                        }
                        // end of convert flag ON
                        #endregion
                        #region Buffer Processing Loop Start
                        //
                        #region Processing Documentation
                        // ITEM IS NOW MADE UP OF AMs OR ROWS
                        // Count the number of aslines / attributes / rows
                        //
                        // Method of processing Ascii and Text Files involves
                        // 1) Reading data into the File Buffer
                        // 2) Converting characters and line / row separators as required
                        // 3) Append the current File Buffer to the File Input Data Item
                        // 4) File Input Data is processesed by a line / row basis
                        // 5) File Input Data is then process one column at a time
                        // 6) Depending on the output type, it is moved to the Output Data Item
                        // Depending on output types Dictionary, DataBase file, and
                        // Text types: Text Files, ASCii, FIXed , Comma Separated Values
                        // Each row is formated and inserted into the output file according
                        // to the Output Type
                        //
                        // Mfile object is not being used as a base class at this time
                        // until the code is stabalized at which the Binary, Text, Ms SQL,
                        // My SQL, PICK (RevX, Ult, U2, PickSyntax, etc) classes can be moved out
                        // as appropriated.
                        //
                        // First to Binary, Text, Database
                        // Then to:
                        // Binary: Binary, Image, Blob, Clob
                        // Text: Document: Text (and variants), ASCii
                        // Text: Records: FIX, CSV
                        // Text: Code
                        // Database: SQL-92: Ms Sql, My Sql
                        // Database: PickSyntax: RevX, Ult, U2, PickSyntax
                        //
                        #endregion
                        //
                        if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + "Count the number of lines / attributes / rows" + "\n"); }
                        // TransformFileControl.InputFile.FileItemData
                        // TransformFileControl.InputFile.InputFileName
                        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                        //
                        // TODO ROW PROCESSING LOOP
                        // 
                        // Ready to process attributes
                        // TransformFileControl.InputFile.iDataItemAttributeIndex = 1; 
                        // TransformFileControl.OutputFile.iDataItemAttributeIndex = 1;
                        // TransformFileControl.OutputFile.FileDictItemCount = 1;
                        //
                        // TODO MdmOutputPrint_PickPosition(0, 30);
                        // LocalMessage = PickOconv(TransformFileControl.InputFile.FileBufferAttributeMaxIndex.ToString(), "R#6"); 
                        // MdmOutputPrint_PickPrint(3, LocalMessage, bYES);
                        LocalMessage2 = "Process Buffer: Records: (" + TransformFileControl.InputFile.FileBufferAttributeMaxIndex.ToString() + ")";
                        if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage2 + "\n"); }
                        //
                        //  TransformFileControl.FileBufferCharMaxIndex
                        // PROCESS ATTRIBUTES
                        #endregion
                        #region Progress Display
                        TransformFileControl.InputFile.FileBufferReadFileCounter += 1;
                        if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) {
                            LocalMessage8 = "Processing loop for Ascii Read: Next Buffer: (" + TransformFileControl.InputFile.FileBufferReadFileCounter.ToString() + ")";
                            MdmOutputPrint_PickPrint(3, "A2" + LocalMessage8, bYES);
                        }

                        // TODO REMOVED LINE POSITION progress display
                        if (TransformFileControl.InputFile.FileBufferCharMaxIndex > 0) {
                            iTracePercentCompleted = iTraceCharacterCount / TransformFileControl.InputFile.FileBufferCharMaxIndex * 100;
                        } else {
                            iTracePercentCompleted = 0;
                        }
                        LocalMessage8 = "% Complete";
                        //
                        opLocalProgressBar_Minimum = 0;
                        opLocalProgressBar_Maximum = TransformFileControl.InputFile.FileBufferCharMaxIndex;
                        opLocalProgressBar_Value = iTraceCharacterCount;
                        //
                        LocalMessage7 = opLocalProgressBar_Maximum.ToString();
                        LocalMessage8 += ", Max (" + LocalMessage7 + ")";
                        ProgressChangedEventArgs eaOpcCurrentRunProgressMax = new ProgressChangedEventArgs((int)opLocalProgressBar_Maximum, "H" + LocalMessage7);
                        omVe.backgroundWorker_ProgressChanged(this, eaOpcCurrentRunProgressMax);
                        //
                        LocalMessage7 = opLocalProgressBar_Value.ToString();
                        LocalMessage8 += ", Count (" + LocalMessage7 + ")";
                        ProgressChangedEventArgs eaOpcCurrentRunProgressValue = new ProgressChangedEventArgs((int)opLocalProgressBar_Value, "V" + LocalMessage7);
                        omVe.backgroundWorker_ProgressChanged(this, eaOpcCurrentRunProgressValue);
                        //
                        // eadRpcLocalOldValue = 0;
                        eadRpcLocalNewValue = opLocalProgressBar_Value;
                        RoutedPropertyChangedEventArgs<double> eadRpcCurrentRunProgressChanged = new RoutedPropertyChangedEventArgs<double>(eadRpcLocalOldValue, eadRpcLocalNewValue);
                        eadRpcLocalOldValue = opLocalProgressBar_Value;
                        //
                        MdmOutputPrint_PickPrint(3, "A2" + LocalMessage8, bYES);
                        #endregion
                        //
                    } // end of not at end
                    RunStatusCheckPause();
                    // Check for character in source File Buffer
                    if (TransformFileControl.InputFile.FileWorkBuffer.Length > 0) {
                        #region Buffer WorkBuffer has characters
                        //
                        // ADD BUFFER TO ITEM Move Characters from Buffer to Item
                        if (TransformFileControl.InputFile.FileReadMode == (int)DatabaseControl.ReadModeBLOCK) {
                            // may not be valid for BLOCK MODE (does not end on an attribute)
                            TransformFileControl.InputFile.FileItemData += ((PickDb)TransformFileControl.InputFile.PickDbObject).PickBufferMoveToString(TransformFileControl.InputFile, TransformFileControl.InputFile.FileWorkBuffer, TransformFileControl.InputFile.iRecordSize);
                            if (TransformFileControl.InputFile.FileWorkBuffer.Length > TransformFileControl.InputFile.iRecordSize) {
                                TransformFileControl.InputFile.FileWorkBuffer = TransformFileControl.InputFile.FileWorkBuffer.Substring(TransformFileControl.InputFile.iRecordSize + 1);
                            } else {
                                TransformFileControl.InputFile.FileWorkBuffer = "";
                            }
                        } else {
                            TransformFileControl.InputFile.FileItemData += ((PickDb)TransformFileControl.InputFile.PickDbObject).PickBufferMoveToString(TransformFileControl.InputFile, TransformFileControl.InputFile.FileWorkBuffer, 0);
                            TransformFileControl.InputFile.FileWorkBuffer = "";
                        }
                        if (TransformFileControl.InputFile.FileWorkBuffer.Length == 0) {
                            TransformFileControl.InputFile.FileBufferHasCharacters = bNO;
                        }
                        // 
                        // Set Attributes to PROCESS and length
                        TransformFileControl.InputFile.iDataItemAttributeMaxIndex = (int)((PickDb)TransformFileControl.InputFile.PickDbObject).PickItemDataCounterGet(TransformFileControl.InputFile);
                        //
                        if (TransformFileControl.InputFile.FileReadMode == (int)DatabaseControl.ReadModeBLOCK) {
                            if (!TransformFileControl.InputFile.FileBufferReadFileIsAtEnd && !TransformFileControl.InputFile.FileBufferHasCharacters) {
                                TransformFileControl.InputFile.iCurrentOffset = TransformFileControl.InputFile.iCurrentOffset + TransformFileControl.InputFile.FileBufferBytesRead;
                            }
                        }
                        #endregion
                    }// end of TransformFileControl.InputFile.FileWorkBuffer.Length > 0
                    // *:
                    // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                    // *:
                    // !; Buffer Mode
                    //
                    #region Process Buffer Attributes
                    //
                    // Process Buffer Attributes
                    //
                    while (
                        TransformFileControl.InputFile.iDataItemAttributeIndex <= TransformFileControl.InputFile.iDataItemAttributeMaxIndexTemp 
                        && TransformFileControl.InputFile.FileItemData.Length > 0 
                        && (!omCo.bRunAbort && !omCo.bRunCancelPending)) {
                        //
                        RunStatusCheckPause();
                        if (TraceBugOnNow && iTraceDisplayCountTotal > iTraceBugThreshold) {
                            LocalMessage2 = "Attribute Counter FileBufferAttributeMaxIndex: (" + TransformFileControl.InputFile.FileBufferAttributeMaxIndex.ToString() + ") of (" + TransformFileControl.InputFile.FileBufferCharMaxIndex.ToString() + "). Length: (" + TransformFileControl.InputFile.FileWorkBuffer.Length + ")";
                            LocalMessage8 = LocalMessage2;
                            MdmOutputPrint_PickPrint(3, "A2" + LocalMessage8, bYES);
                        }

                        // Debug / Trace Counter
                        // iIterationCount += 1;
                        // iIterationRemaider = Math.DivRem(iIterationCount, (iTraceIterationCountThreshold * 100), out iTemp);
                        if (TransformFileControl.InputFile.iDataItemAttributeMaxIndexTemp <= 5) {
                            iIterationDebugCount = 1;
                        }
                        //
                        // print progress PROGRESS DISPLAY
                        if (PickDisplayLineNumber <= 250) { PickDisplayLineNumber += 1; } else {
                            MdmOutputPrint_PickPrint(3, "A2" + "Columns of data processed (" + TransformFileControl.InputFile.iDataItemAttributeIndex.ToString() + ").", bYES);
                            // LocalMessage2 = PickOconv(PickDisplayLineNumber, "R#5") + " " + PickOconv(TransformFileControl.InputFile.FileItemData.GetField(TransformFileControl.InputFile.iDataItemAttributeIndex), "L#70");
                            // TODO MdmOutputPrint_PickPosition(0, 5);
                            // MdmOutputPrint_PickPrint(3, LocalMessage);
                            PickDisplayLineNumber = 1;
                        }
                        // Progress Bar
                        if (iTraceCharacterCount > (int)(opLocalProgressBar_Maximum / 100)) {
                            iTracePercentCompleted = iTraceCharacterCount / TransformFileControl.InputFile.FileBufferCharMaxIndex * 100;
                            LocalMessage8 = iTraceCharacterCount.ToString();
                            omVe.backgroundWorker_ProgressChanged(this, new ProgressChangedEventArgs(iTraceCharacterCount, "V" + LocalMessage8));
                            opLocalProgressBar_Display = 1;
                        } else { opLocalProgressBar_Display += 1; }
                        //
                        // Get next Block, Line or Column Text
                        //
                        if (TraceBugOnNow && iTraceDisplayCountTotal > iTraceBugThreshold) {
                            LocalMessage9 = "Debug Point Reached, " + LocalMessage2;
                            MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage9 + "\n");
                            LocalMessage = LocalMessage9;
                            LocalMessage2 = "Check for Next Block, Line or Column";
                            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage2 + "\n"); }
                        }
                        //
                        TransformFileControl.FileColumnText = TransformFileControl.InputFile.FileItemData.GetField(TransformFileControl.InputFile.iDataItemAttributeIndex);
                        //
                        if (TraceBugOnNow) {
                            LocalMessage9 = "Debug Point Reached, After Next Block, Line or Column";
                            if (TraceBugOnNow && iTraceDisplayCountTotal > iTraceBugThreshold) {
                                MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage9 + "\n");
                                LocalMessage = LocalMessage9;
                            }
                            if (TransformFileControl.FileColumnText.Length > 100) {
                                LocalMessage9 = "Debug Point Reached, Length exceeds 100 characters";
                                MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, TraceBugOnNow, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage9 + "\n");
                                LocalMessage9 = TransformFileControl.InputFile.FileItemData.GetStatistics();
                                MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, TraceBugOnNow, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage9 + "\n");
                                LocalMessage = LocalMessage9;
                            }
                        }
                        RunStatusCheckPause();
                        // *:
                        // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                        //
                        // Check for NEXT ITEM ID - Item Id / Row Id VIA Tilde's
                        //
                        if (
TransformFileControl.InputFile.FileSummary.FileTypeId 
        == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeTEXT 
    && (TransformFileControl.InputFile.FileSummary.FileSubTypeId 
            == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE 
    || TransformFileControl.InputFile.FileSummary.FileSubTypeId 
            == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE_CSV)
                            ) {
                            // Check for ID change in Tilde File
                            TransformFileControl.FileColumnTempId = ((PickDb)TransformFileControl.InputFile.PickDbObject).PickIdGetString(TransformFileControl.FileColumnText, Tld);
                            if (TransformFileControl.FileColumnTempId.Length > 0) {
                                // ID CHANGE
                                TransformFileControl.InputFile.FileSummary.FileItemIdIsChanged = bYES;
                                TransformFileControl.FileColumnTempFileName = PickField(TransformFileControl.FileColumnTempId, " ", 2);
                                if (TransformFileControl.FileColumnTempFileName.Length > 0) {
                                    TransformFileControl.InputFile.FileSummary.FileNameNext = TransformFileControl.InputFile.FileSummary.FileNameNext;
                                    TransformFileControl.FileColumnTempId = PickField(TransformFileControl.FileColumnTempId, " ", 1);
                                    TransformFileControl.InputFile.FileSummary.FileNameNext = TransformFileControl.FileColumnTempId;
                                }
                                TransformFileControl.InputFile.FileSummary.FileItemIdNext = TransformFileControl.FileColumnTempId;
                                // FILE ITEM ID Changed
                                if (TransformFileControl.InputFile.FileSummary.FileItemIdNext.Length > 0 && TransformFileControl.InputFile.FileSummary.FileItemIdNext != TransformFileControl.InputFile.FileSummary.FileItemId) {
                                    TransformFileControl.InputFile.FileSummary.FileItemIdIsChanged = bYES;
                                    if (TraceBugOnNow) {
                                        LocalMessage2 = "File Item Id has changed, " + "\"" + TransformFileControl.InputFile.FileSummary.FileItemIdNext + "\"" + "<>" + "\"" + TransformFileControl.InputFile.FileSummary.FileItemId + "\"";
                                        MdmOutputPrint_PickPrint(3, "A2" + LocalMessage2, bYES);
                                    }
                                }
                                // FILE NAME Changed
                                if (TransformFileControl.InputFile.FileSummary.FileNameNext.Length > 0 && TransformFileControl.InputFile.FileSummary.FileNameNext != TransformFileControl.InputFile.FileSummary.FileName) {
                                    TransformFileControl.InputFile.FileSummary.FileItemIdIsChanged = bYES;
                                    TransformFileControl.InputFile.FileSummary.FileNameIsChanged = bYES;
                                    if (TraceBugOnNow) {
                                        LocalMessage2 = "File Name has changed, " + "\"" + TransformFileControl.InputFile.FileSummary.FileNameNext + "\"" + "<>" + "\"" + TransformFileControl.InputFile.FileSummary.FileName + "\"";
                                        MdmOutputPrint_PickPrint(3, "A2" + LocalMessage2, bYES);
                                    }
                                }
                            } // id present
                        } // Tilde Id change check processing
                        RunStatusCheckPause();
                        // *:
                        // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                        //
                        // Check for OTHER Id changes HERE when NOT Tilde Id Usage (ie column 1 = id)
                        //
                        // Process Id change - ID CHANGED
                        //
                        if (TransformFileControl.InputFile.FileSummary.FileItemIdIsChanged | TransformFileControl.InputFile.FileSummary.FileNameIsChanged) {
                            iMinputTldMain = MinputTldMainProcessIdChange();
                        }
                        //
                        // TODO Note: Must handle the presence of File Data (PARameters, CoNTrol, LiSTS, 
                        //
                        // TODO Field Description and other data items present in the dictionary
                        //
                        // TODO Note: No handling for Triggers, Procedures and other SQL object at this time.
                        //
                        // Set Output Data
                        //
                        //
                        if (TraceBugOnNow && iTraceDisplayCountTotal > iTraceBugThreshold) {
                            LocalMessage9 = "Debug Point Reached, ready to set Output Data";
                            MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage9 + "\n");
                            LocalMessage = LocalMessage9;
                        }
                        RunStatusCheckPause();
                        iMinputTldMain = MinputTldMainSetOutputData();
                        RunStatusCheckPause();
                        //
                        // Increment Column Pointer
                        //
                        iMinputTldMain = ((PickDb)TransformFileControl.InputFile.PickDbObject).MinputTldMainIncrementColumnPointer();
                        RunStatusCheckPause();
                        //
                        // Increment Trace Control
                        //
                        iMinputTldMain = MdmTraceDoIncrement("TraceIncrement");
                        RunStatusCheckPause();
                        //
                        // End of Main Processing Loop
                        // Set State
                        omCo.RunAction = RunFirst;
                        omCo.RunMetric = RunState;
                        omCo.RunTense = RunTense_Did;
                        omCo.iaRunActionState[RunFirst, RunState] = RunTense_Did;
                        omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
                        omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
                        //
                        IterationFirst = bNO;
                        //
                        // Check for Run Pausing
                        //
                        RunStatusCheckPause();
                    } // end of Process Buffer Attributes (while data in input file buffer)
                    // *:
                    // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                    #endregion
                    RunStatusCheckPause();
                    //
                } // end of if !TransformFileControl.FileBufferReadFileIsAtEnd || TransformFileControl.InputFile.FileBufferHasCharacters
                //
                LocalMessage9 = "Finished Buffer, get next buffer...";
                MdmOutputPrint_PickPrint(2, "A2" + LocalMessage9, bYES);
                LocalMessage9 = "Debug Point Reached IF the BufferNotEmpty section of code, get next buffer";
                if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage9 + "\n"); }
                RunStatusCheckPause();
                // *:
                // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                    #endregion
            } // reloop for: not at end or buffer has characters
            //
            LocalMessage9 = "Finished Buffers...";
            MdmOutputPrint_PickPrint(2, "A1" + LocalMessage9, bYES);
            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + "Debug Point Reached: " + LocalMessage9 + "\n"); }
            RunStatusCheckPause();
            // do repeat;
            #region Reloop
            // Set State
            omCo.RunAction = RunReloop;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Doing;
            omCo.iaRunActionState[RunReloop, RunState] = RunTense_Doing;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            // *:
            // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            // *  End of Input Item
            // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            // *:
            // !; Control Status and Reloop
            // *:
            LocalMessage9 = "Write output item per options";
            MdmOutputPrint_PickPrint(2, "A2" + LocalMessage9, bYES);
            MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, 0, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + "Write output item per options" + "\n");
            // 
            RunStatusCheckPause();
            #region Run Cancellation Check
            // Check State
            omCo.RunAction = RunAbort;
            omCo.RunTense = RunTense_Do;
            if (omCo.iaRunActionState[RunAbort, RunState] == RunTense_Do || omCo.iaRunActionState[RunAbort, RunState] == RunTense_Doing) {
                // Set State
                omCo.RunAction = RunAbort;
                omCo.RunMetric = RunState;
                omCo.RunTense = RunTense_Did;
                omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
                omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                    "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
                omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
                //
                LocalMessage = "ABORT: RUN WAS CANCELLED !!!!";
                MdmOutputPrint_PickPrint(3, LocalMessage, bNO);
                ProgressChangedEventArgs eaOpcCurrentRunProgressMax = new ProgressChangedEventArgs((int)opLocalProgressBar_Maximum, "Z" + LocalMessage);
                omVe.backgroundWorker_ProgressChanged(this, eaOpcCurrentRunProgressMax);

            } else {
                //
                // Display Error
                if (TransformFileControl.InputFile.FileBufferBytesRead < 0) {
                    MdmOutputPrint_PickPrint(3, bYES, "A2");
                    MdmOutputPrint_PickPrint(3, "SKIP: Empty File or Item!!!! Error [", bNO);
                    MdmOutputPrint_PickPrint(3, "A2" + PickSystemCallString(0), bNO);
                    LocalMessage0 = "] - Unable to read MS-DOS file \"" + TransformFileControl.InputFile.FileSummary.FileName + "\".";
                    MdmOutputPrint_PickPrint(3, "A2" + LocalMessage0, bYES);
                    MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, omCo.RunErrorDidOccur = true, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage0 + "\n");
                    TransformFileControl.InputFile.FileBufferReadFileIsAtEnd = bYES;
                }
            }
            //
            //
            if (!TransformFileControl.InputFile.FileBufferReadFileIsAtEnd) {
                // Display
                TransformFileControl.InputFile.iCurrentOffset = TransformFileControl.InputFile.iCurrentOffset + TransformFileControl.InputFile.FileBufferBytesRead;
                // MdmOutputPrint_PickPosition(bNO, 4);
                LocalMessage0 = PickOconv(TransformFileControl.InputFile.iCurrentOffset, "r#8") + " bytes processed";
                if (!omCo.bRunAbort || !omCo.bRunCancelPending) {
                    LocalMessage0 = "Finished Item, " + LocalMessage0;
                } else {
                    LocalMessage0 = "CANCELLED Item!!! There were " + LocalMessage0;
                }
                MdmOutputPrint_PickPrint(1, "A2" + LocalMessage0, bYES);
            }
            #endregion
            RunStatusCheckPause();
            #region Write Output Item Set
            // *:
            // xxxxxxxxxxxxxxxxxxxxxxxxxxx Write Ouput File
            // *:
            // Set State
            omCo.RunAction = RunMain_DoWrite;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Doing;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            if (!omCo.bRunAbort && !omCo.bRunCancelPending) {
                bPickOutputItemWriteItBoolFlag = bYES;
                LocalMessage7 = "Writing Ouput File";
                MdmOutputPrint_PickPrint(2, "A2" + LocalMessage7, bYES);
                iMinputTldMain = ((PickDb)TransformFileControl.InputFile.PickDbObject).PickWrite(); // XXXXXXXXXXXXXXXXXXXXXX
                LocalMessage7 = "File Statistics for File";
                // MdmOutputPrint_PickPrint(2, "A2" + LocalMessage7, bYES);
                //
            } else {
                LocalMessage7 = "File Statistics for CANCELLED File";
                // MdmOutputPrint_PickPrint(2, "A2" + LocalMessage7, bYES);
            }
            // Set State
            omCo.RunAction = RunMain_DoWrite;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Did;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            #endregion
            RunStatusCheckPause();
            #region Continue and Reloop Checks
            //
            // STATS AND FINAL WRITES
            //
            bTemp7 = bYES;
            //
            // Input File Conditions
            if (!TransformFileControl.InputFile.FileBufferReadFileIsAtEnd) { bTemp7 = bNO; }
            if (TransformFileControl.InputFile.FileBufferReadFileCounter <= 0) { bTemp7 = bNO; }
            if (!TransformFileControl.InputFile.FileBufferDoesExist) { bTemp7 = bNO; }
            // Bytes Read
            // if (TransformFileControl.InputFile.FileBufferBytesRead <= 0) { bTemp7 = bNO; }
            // if (TransformFileControl.InputFile.FileBufferBytesReadTotal <= 0) { bTemp7 = bNO; }
            // Empty Input Item
            // if (TransformFileControl.InputFile.FileItemData.Length <= 0) { bTemp7 = bNO; }
            // Output File Conditions
            if (TransformFileControl.OutputFile.FileBufferWriteFileCounter <= 0) { bTemp7 = bNO; }
            // if (!TransformFileControl.OutputFile.FileBufferHasCharacters) { bTemp7 = bNO; }
            //
            // if (TransformFileControl.OutputFile.FileBufferBytesWriten <= 0) { bTemp7 = bNO; }
            // if (TransformFileControl.OutputFile.FileBufferBytesWritenTotal <= 0) { bTemp7 = bNO; }
            // Empty Output Item.  Empty records are allowed
            // if (TransformFileControl.OutputFile.FileItemData.Length <= 0) { bTemp7 = bNO; }
            // TODO Display Error for Run Main at end of buffers
            // Display Error
            if (bTemp7) {
                switch (TransformFileControl.OutputFile.FileSummary.FileTypeId) {
                    case ((int)DatabaseControl.FileData):
                        // Processing Data
                        switch (TransformFileControl.OutputFile.FileSummary.FileSubTypeId) {
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeSQL):
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMS):
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMY):
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeCSV):
                                LocalMessage7 += ", Data Count (" + TransformFileControl.OutputFile.iDataItemAttributeCounter.ToString() + ")";
                                LocalMessage7 += ", Columns (" + TransformFileControl.OutputFile.iDataItemAttributeIndex.ToString() + ")";
                                MdmOutputPrint_PickPrint(3, "A2" + LocalMessage7, bYES);
                                break;
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeASC):
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDAT):
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeFIX):
                                // error SubType not supported
                                break;
                            default:
                                break;
                        }
                        break;
                    //
                    case ((int)DatabaseControl.FileDictData):
                        // FileDictData
                        switch (TransformFileControl.OutputFile.FileSummary.FileSubTypeId) {
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeSQL):
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMS):
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMY):
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeCSV):
                                LocalMessage7 += " Dictionary Count (" + TransformFileControl.OutputFile.FileDictItemCount.ToString() + ")";
                                LocalMessage7 += " Attributes (" + TransformFileControl.OutputFile.FileDictItemIndex.ToString() + ")";
                                MdmOutputPrint_PickPrint(3, "A2" + LocalMessage7, bYES);
                                if (!omCo.bRunAbort && !omCo.bRunCancelPending) {
                                    iMinputTldMain = TransformFileControl.OutputFile.SqlFileDictColumnAddCmdBuildAllFromAprray("Write InputTld1");
                                } else {
                                    LocalMessage7 = "Cancellation, Dictionary / Schma not written!!!";
                                    MdmOutputPrint_PickPrint(3, "A2" + LocalMessage7, bYES);
                                }
                                // TODO PROCESS AND DO WRITE OF DICTIONARY
                                break;
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXT):
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeASC):
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDAT):
                            case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeFIX):
                                // error SubType not supported
                                break;
                            default:
                                break;
                        }
                        break;
                    default:    
                        break;
                } // end or is DATA Attribute not DICT
                // MdmOutputPrint_PickPrint(3, LocalMessage7);
            } // End of to write to Output File
            LocalMessage9 = "Debug Point Reached: Finished Writing Item(s)";
            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage9 + "\n"); }
            // } // End of Valid Input Item to Write
            //
            // xxxxx FINISH
            //
            #endregion
            RunStatusCheckPause();
            #region Close Files
            // *:
            // xxxxxxxxxxxxxxxxxxxxxxxxxxx Close Files: // *:
            // *:
            omCo.RunAction = RunClose;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Doing;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            LocalMessage8 = "Close Files...";
            MdmOutputPrint_PickPrint(2, "A2" + LocalMessage8, bYES);
            //
            // Set State
            omCo.RunAction = RunFinish;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Doing;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            LocalMessage9 = "Debug Point Reached: Finished processing, Closing Files.";
            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage9 + "\n"); }
            //
            if (true == false) {
                // TransformFileControl.InputFile.FileBufferBytesRead 
                lTemp9 = ((PickDb)TransformFileControl.InputFile.PickDbObject).FileBufferFileCloseHandle(TransformFileControl.InputFile);
            } else {
                //
                // Output File
                lTemp8 = TransformFileControl.OutputFile.FileAccessMode + 100; // close = read + 100
                lTemp9 = ((PickDb)TransformFileControl.InputFile.PickDbObject).PickFileAction(
                    TransformFileControl.OutputFile, 
                    TransformFileControl.OutputFile.FileSummary.FileName, 
                    (int)Mdm.Oss.FileUtil.Mfile.FileActionDirectionIs.Output, (int)lTemp8, bYES);
                iMinputTldMain = (long)(DatabaseControl.ResultDatabaseError & Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.ActionCLOSE & Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.ResultFailed);
                // if (iMinputTldMain > 0) {
                if (lTemp9 > 0) {
                    // TODO PickSyntax Position
                    // MdmOutputPrint_PickPosition(bNO, 4);
                    // LocalMessage0 = "ABORT: File Close Error (" + PickSystemCallString(iMinputTldMain) + "] - Unable to close MS-DOS file \"" + TransformFileControl.InputFile.FileSummary.FileName + "\".";
                    //
                    LocalMessage0 = "ABORT: File Close Error [" + iMinputTldMain + "] - Unable to close MS-DOS file \"" + TransformFileControl.InputFile.FileSummary.FileName + "\".";
                    MdmOutputPrint_PickPrint(3, "A2" + LocalMessage0, bYES);
                    MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, omCo.RunErrorDidOccur = true, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage0 + "\n");
                }
                //
                // Input File
                lTemp8 = TransformFileControl.InputFile.FileAccessMode + 100; // close = read + 100
                
                lTemp9 = ((PickDb)TransformFileControl.InputFile.PickDbObject).PickFileAction(
                    TransformFileControl.OutputFile, 
                    TransformFileControl.OutputFile.FileSummary.FileName, 
                    (int)Mdm.Oss.FileUtil.Mfile.FileActionDirectionIs.Input, 
                    (int)lTemp8, bYES);

                iMinputTldMain = (long)(
                    Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.ResultDatabaseError 
                    & Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.ActionCLOSE 
                    & Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.ResultFailed);
                // if (iMinputTldMain > 0) {
                if (lTemp9 > 0) {
                    // TODO PickSyntax Position
                    // MdmOutputPrint_PickPosition(bNO, 4);
                    // LocalMessage0 = "ABORT: File Close Error (" + PickSystemCallString(iMinputTldMain) + "] - Unable to close MS-DOS file \"" + TransformFileControl.InputFile.FileSummary.FileName + "\".";
                    //
                    LocalMessage0 = "ABORT: File Close Error [" + iMinputTldMain + "] - Unable to close MS-DOS file \"" + TransformFileControl.InputFile.FileSummary.FileName + "\".";
                    MdmOutputPrint_PickPrint(3, "A2" + LocalMessage0, bYES);
                    MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, omCo.RunErrorDidOccur = true, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage0 + "\n");
                }
            }
            //
            // *:
            // xxxxxxxxxxxxxxxxxxxxxxxxxxx Close Application Services: // *:
            // *:
            omCo.RunAction = RunClose;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Doing;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            LocalMessage8 = "Close Application Services...";
            MdmOutputPrint_PickPrint(2, "A1" + LocalMessage8, bYES);
            //
            LocalMessage9 = "Debug Point Reached: Finished processing, Closing Application Services.";
            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage9 + "\n"); }
            //
            // PickSyntax Console Close
            if (ConsoleOn) { ConsoleMdmConsolePickConsoleClose(); }
            if (ConsolePickConsoleOn) { ConsoleMdmConsolePickConsoleClose(); }
            // Set State
            omCo.RunAction = RunClose;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Did;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            //
            //
            #endregion
            RunStatusCheckPause();
            #region Complete Processing
            bTemp9 = false;
            if (omCo.iaRunActionState[RunCancel, RunState] == RunTense_Do || omCo.iaRunActionState[RunPause, RunState] == RunTense_Did || omCo.iaRunActionState[RunCancel, RunState] == RunTense_Doing) { bTemp9 = bYES; }
            if (omCo.iaRunActionState[RunAbort, RunState] == RunTense_Do || omCo.iaRunActionState[RunPause, RunState] == RunTense_Did || omCo.iaRunActionState[RunAbort, RunState] == RunTense_Doing) { bTemp9 = bYES; }
            if (bTemp9) {
                omCo.bRunCancelPending = false;
                // Set State
                omCo.RunAction = RunClose;
                omCo.RunMetric = RunState;
                omCo.RunTense = RunTense_Did;
                omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
                omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                    "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
                omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
                //
                iTemp = (int)omCo.AppCancelCompleted();
                // Set State
                omCo.RunAction = RunClose;
                omCo.RunMetric = RunState;
                omCo.RunTense = RunTense_Did;
                omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
                omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                    "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
                omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
                //
            } else {
                iTemp = (int)omCo.AppComplete_Processing();
            }
            // Set State
            omCo.RunAction = RunRunDo;
            omCo.RunMetric = RunState;
            omCo.RunTense = RunTense_Did;
            omCo.iaRunActionState[omCo.RunAction, RunState] = omCo.RunTense;
            omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString());
            omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
            #endregion
            //
            PickStop();
            // xxxxxxxxxxxxxxxxxxxxxxxxxxx finish: // *:
            // END OF MAIN
            // xxxxxxxxxxxxxxxxxxxxxxxxxxx finish: // *:
            iMinputTldMain = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.NormalEnd;
            return iMinputTldMain;
            #endregion
        }
        // *:
        // ==================================================================
        #endregion
        #region Pause Processing
        public void RunStatusCheckPause() {
            //
            // Check for Run Pausing
            //
            if (!omCo.bRunCancelPending && (omCo.bRunPausePending || omCo.iaRunActionState[RunPause, RunState] != RunTense_Off)) {
                LocalMessage = "Process paused, waiting form resume...";
                MdmOutputPrint_PickPrint(1, "A2" + LocalMessage0, bYES);
                // Set State
                omCo.RunAction = RunPause;
                omCo.RunMetric = RunState;
                omCo.RunTense = RunTense_Did;
                omCo.iaRunActionState[RunPause, RunState] = RunTense_Did;
                omCo.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + omCo.RunMetric.ToString() + omCo.RunTense.ToString() + omCo.RunAction.ToString() + LocalMessage);
                omVe.backgroundWorker_ProgressChanged(this, omCo.ePceaRunActionState);
                //
                iAppActionWaitMilliIncrement = 500;
                iAppActionWaitMilliIncrementMax = 3600000;
                bAppActionWaitContinue = true;
                iAppActionWaitCounter = 0;
                //
                while (bAppActionWaitContinue && iAppActionWaitCounter < iAppActionWaitMilliIncrementMax) {
                    iAppActionWaitCounter += iAppActionWaitMilliIncrement;
                    System.Threading.Thread.Sleep(iAppActionWaitMilliIncrement);
                    if (omCo.bRunCancelPending || omCo.iaRunActionState[RunPause, RunState] != RunTense_Did) { bAppActionWaitContinue = false; }
                }
            }
        }
        #endregion
        #region BufferRead
        protected long MinputTldMainBufferRead(Mfile ofPassedFileObject) {
            iMinputTldMainBufferRead = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            //
            // Read More Data Information In
            LocalMessage9 = "Read More Buffer Data Information In";
            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMain, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage9 + "\n"); }
            //
            LocalMessage3 = "Read Buffer ";
            try {
                switch (ofPassedFileObject.FileReadMode) {
                    case ((int)DatabaseControl.ReadModeBLOCK):
                        // Retrieve more LOW LEVEL characters 
                        LocalMessage3 += "Next Block of " + (PickOconv(ofPassedFileObject.iCurrentOffset, "r#8") + " bytes" + Cr).ToString();
                        ofPassedFileObject.iCurrentOffsetModulo = (int)(ofPassedFileObject.iCurrentOffset / ofPassedFileObject.iOffsetSize);
                        iMinputTldMainBufferRead = Math.DivRem((int)ofPassedFileObject.iCurrentOffset, (int)ofPassedFileObject.iOffsetSize, out ofPassedFileObject.iCurrentOffsetRemainder);
                        //
                        iMinputTldMainBufferRead = ((PickDb)TransformFileControl.InputFile.PickDbObject).FileBufferFileSeek(ofPassedFileObject, ofPassedFileObject.iCurrentOffsetModulo, ofPassedFileObject.iCurrentOffsetRemainder, (int)Mdm.Oss.Decl.DefStdBaseRunFile.DatabaseControl.ReadModeSEEK);
                        //
                        if (ofPassedFileObject.FileBufferBytesRead == ofPassedFileObject.iCurrentOffset) {
                            ofPassedFileObject.FileBufferHasCharacters = bYES;
                            ofPassedFileObject.FileBufferReadFileIsAtEnd = bNO;
                        } else if (ofPassedFileObject.FileBufferBytesRead > 0) {
                            ofPassedFileObject.FileBufferHasCharacters = bYES;
                            ofPassedFileObject.FileBufferReadFileIsAtEnd = bYES;
                        }
                        break;
                    case ((int)DatabaseControl.ReadModeLINE):
                        // bRead Ascii File Line of Characters
                        LocalMessage3 += "Next Line";
                        //
                        iMinputTldMainBufferRead = ((PickDb)TransformFileControl.InputFile.PickDbObject).FileBufferFileReadLine(ofPassedFileObject, ofPassedFileObject.FileWorkBuffer, ofPassedFileObject.iRecordSize);
                        //
                        if (ofPassedFileObject.FileBufferBytesRead > 0) {
                            ofPassedFileObject.FileBufferHasCharacters = bYES;
                            ofPassedFileObject.FileBufferReadFileIsAtEnd = bNO;
                        } else {
                            if (ofPassedFileObject.FileWorkBuffer.Length == 0) {
                                ofPassedFileObject.FileBufferHasCharacters = bNO;
                            }
                            ofPassedFileObject.FileBufferReadFileIsAtEnd = bYES;
                        }
                        break;
                    case ((int)DatabaseControl.ReadModeAll):
                        // bRead All Ascii file Characters
                        LocalMessage3 += "Next Item";
                        ofPassedFileObject.FileBufferReadFileIsAtEnd = bYES;
                        //
                        LocalLongResult = ((PickDb)TransformFileControl.InputFile.PickDbObject).FileBufferFileReadAll(ofPassedFileObject, ofPassedFileObject.FileWorkBuffer, ofPassedFileObject.iRecordSize);
                        //
                        if (ofPassedFileObject.FileBufferBytesRead > 0) {
                            ofPassedFileObject.FileBufferHasCharacters = bYES;
                        }
                        if (ofPassedFileObject.FileWorkBuffer.Length == 0) {
                            ofPassedFileObject.FileBufferHasCharacters = bNO;
                        } else { 
                            ofPassedFileObject.FileBufferHasCharacters = bYES;
                        }
                        break;
                    default:
                        ofPassedFileObject.FileBufferHasCharacters = bNO;
                        ofPassedFileObject.FileBufferReadFileIsAtEnd = bYES;
                        ofPassedFileObject.FileBufferDoesExist = bNO;
                        iMinputTldMainBufferRead = (int)DatabaseControl.ReadModeError;
                        LocalMessage3 += "Buffer Error - File Read Mode (" + ofPassedFileObject.FileReadMode.ToString() + ") is not set";
                        // MdmOutputPrint_PickPrint(3, LocalMessage);
                        MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMainBufferRead, omCo.RunErrorDidOccur = true, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage3 + "\n");
                        // throw new NotSupportedException(LocalMessage);
                        break;
                } // END OF READ MODE SWITCH
                // LocalMessage6 = PickOconv(TransformFileControl.InputFile.FileBufferCharIndex, "R#6") + " " + TransformFileControl.InputFile.FileItemData.GetField(TransformFileControl.InputFile.FileBufferCharIndex);
                LocalMessage3 += ", it is " + ofPassedFileObject.FileBufferHasCharacters.ToString() + " that the buffer has characters";
                LocalMessage3 += ", it has (" + ofPassedFileObject.FileBufferBytesRead.ToString() + ") characters";
                if (ofPassedFileObject.FileBufferReadFileIsAtEnd == bYES) {
                    LocalMessage3 += ", the file is at the end.";
                } else {
                    LocalMessage3 += ", the file end has not been reached.";
                }
            } catch (Exception eAnye) {
                ofPassedFileObject.FileBufferReadFileError = bYES;
                ofPassedFileObject.FileBufferHasCharacters = bNO;
                ofPassedFileObject.FileBufferReadFileIsAtEnd = bYES;
                iIterationCount = 0;
                LocalMessage3 += " ABORT on EXCEPTION: Buffer Read Error [" + omCo.RunShellErrorNumber + "] - ";
                // MdmOutputPrint_PickPrint(3, LocalMessage3);
                MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMainBufferRead, omCo.RunErrorDidOccur = true, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage3);
            } finally {
                // MdmOutputPrint_PickPrint(3, (bool)bYES);
                // MdmOutputPrint_PickPosition(bNO, 4);
                MdmOutputPrint_PickPrint(3, "A2" + LocalMessage3, bYES);
            }
            return iMinputTldMainBufferRead;
        }
        #endregion
        #region Process Id Change
        // *:
        // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        protected long MinputTldMainProcessIdChange() {
            iMinputTldMainProcessIdChange = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            //
            // Process Id change
            //
            // if (TransformFileControl.InputFile.FileSummary.FileItemIdChanged || TransformFileControl.InputFile.FileSummary.FileNameIsChanged) {
            //
            LocalMessage4 = "Process change - ID CHANGED";
            if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMainProcessIdChange, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage4 + "\n"); }
            if (TransformFileControl.InputFile.FileSummary.FileNameIsChanged) {
                LocalMessage4 = "File Name has changed...";
                MdmOutputPrint_PickPrint(2, "A1" + LocalMessage4, bYES);
            }
            if (TransformFileControl.InputFile.FileSummary.FileItemIdIsChanged) {
                if (TraceBugOnNow) {
                    LocalMessage4 = "File Item Id has changed...";
                    MdmOutputPrint_PickPrint(2, "A1" + LocalMessage4, bYES);
                }
            }
            iMinputTldMainProcessIdChange = ((PickDb)TransformFileControl.InputFile.PickDbObject).PickIdChanged();
            //
            // TransformFileControl.InputFile.iDataItemAttributeIndex = 0:
            switch (TransformFileControl.OutputFile.FileSummary.FileTypeId) {
                case ((int)DatabaseControl.FileDictData):
                    TransformFileControl.OutputFile.FileDictItemCount = 0;
                    TransformFileControl.OutputFile.FileDictItemIndex = 0;
                    TransformFileControl.OutputFile.FileItemData = "";
                    break;
                case ((int)DatabaseControl.FileData):
                default:
                    TransformFileControl.OutputFile.iDataItemAttributeCounter = 0;
                    TransformFileControl.OutputFile.iDataItemAttributeIndex = 1;
                    TransformFileControl.OutputFile.FileItemData = "";
                    break;
            }
            //
            if (TransformFileControl.InputFile.FileSummary.FileNameIsChanged) {
                // FILE NAME CHANGED
                LocalMessage4 = "Process change - FILE NAME CHANGED";
                MdmOutputPrint_PickPrint(3, "A1" + LocalMessage4, bYES);
                LocalMessage4 = "Open New File ...";
                MdmOutputPrint_PickPrint(3, "A1" + LocalMessage4, bYES);
                // MdmOutputPrint_PickPrint(3, LocalMessage);
                // TODO NEXT FILE NAME HANDLING
                // TODO MUST BE PROMPTED OR LIST CORRELATED
                TransformFileControl.OutputFile.FileSummary.FileName = TransformFileControl.OutputFile.FileSummary.FileNameNext; // NO
                TransformFileControl.OutputFile.FileSummary.FileName = TransformFileControl.InputFile.FileSummary.FileNameNext; // YES FOR NOW
                LocalLongResult = ((PickDb)TransformFileControl.InputFile.PickDbObject).PickFileAction();
                TransformFileControl.InputFile.FileSummary.FileNameIsChanged = bNO;
            }
            //
            LocalMessage4 = "PickSyntax Id Check Exists: \"" + TransformFileControl.OutputFile.FileSummary.FileItemIdNext + "\"";
            MdmOutputPrint_PickPrint(3, "A2" + LocalMessage4, bYES);
            // TODO File TransformFileControl.InputFile.FileSummary.FileIdExists bNO for now
            bTemp1 = ((PickDb)TransformFileControl.InputFile.PickDbObject).PickIdCheckDoesExistBool(TransformFileControl.OutputFile, TransformFileControl.OutputFile.FileSummary.FileItemIdNext);
            TransformFileControl.OutputFile.FileSummary.FileIdExists = bNO;
            // Check Options for next FILE / ITEM
            LocalMessage4 = "Check Options for next FILE / ITEM";
            MdmOutputPrint_PickPrint(3, "A2" + LocalMessage4, bYES);
            iMinputTldMainProcessIdChange = ((PickDb)TransformFileControl.InputFile.PickDbObject).PickIdCheckOptions();

            // Move Buffer if present
            if (TransformFileControl.InputFile.FileWorkBuffer.Length > 0) {
                LocalMessage4 = "Move Buffer, character are present";
                MdmOutputPrint_PickPrint(3, "A2" + LocalMessage4, bYES);
                PickIdChangedResult = ((PickDb)TransformFileControl.InputFile.PickDbObject).PickBufferMoveToItemData(TransformFileControl.InputFile);
            }
            //
            // Display
            // TODO MdmOutputPrint_PickPosition(0, 25);
            LocalMessage = PickOconv(TransformFileControl.InputFile.iDataItemAttributeIndex, "R#6");
            // TODO MdmOutputPrint_PickPrint (LocalMessage);
            LocalMessage4 = "Attribute iPassedPickDictItemIndex: (" + PickOconv(TransformFileControl.InputFile.iDataItemAttributeIndex, "l#3") + ")";
            MdmOutputPrint_PickPrint(3, "A2" + LocalMessage4, bYES);
            // TODO Column Postioning MdmOutputPrint_PickPrint(3, LocalMessage, bYES);
            //
            // Reset Ouput File Item - File Type and SubType
            LocalMessage4 = "File Type: (" + TransformFileControl.OutputFile.FileSummary.FileTypeId.ToString() + "), File Sub Type: (" + TransformFileControl.OutputFile.FileSummary.FileSubTypeId + ")";
            MdmOutputPrint_PickPrint(3, "A2" + LocalMessage4, bYES);
            //
            TransformFileControl.OutputFile.FileSummary.FileTypeId = TransformFileControl.OutputFile.FileSummary.FileTypeId;
            TransformFileControl.OutputFile.FileSummary.FileSubTypeId = TransformFileControl.OutputFile.FileSummary.FileSubTypeId;
            // Reset Id Change ItFlag
            TransformFileControl.InputFile.FileSummary.FileItemIdIsChanged = bNO;
            //


            return iMinputTldMainProcessIdChange;
        }
        #endregion
        #region MessageToPage
        public override string ToString() {
            if (TransformFileControl.InputFile != null) {
                if (TransformFileControl.InputFile.sMessageToPage != "") { MessageToPage(TransformFileControl.InputFile); }
            }
            if (TransformFileControl.OutputFile != null) {
                if (TransformFileControl.OutputFile.sMessageToPage != "") { MessageToPage(TransformFileControl.OutputFile); }
            }
            if (omOb != null) {
                if (omOb.sMessageToPage != "") { MessageToPage(omOb); }
            }
            return base.ToString();
        }

        public void MessageToPage(Mfile ofPassedFileObject) {
            if (ofPassedFileObject != null && omVe != null) {
                if (ofPassedFileObject.sMessageToPage != null) {
                    if (ofPassedFileObject.sMessageToPage.Length > 0) {
                        if (ofPassedFileObject.sMessageToPage.Substring(0, 1) == "#") {
                            omVe.CallerUpdateSendMessageToUi(ofPassedFileObject.sMessageToPage);
                            ofPassedFileObject.sMessageToPage = "";
                        }
                    }
                }
            }
        }

        public void MessageToPage(Mobject omPmssedO) {
            if (omPmssedO != null && omVe != null) {
                if (omPmssedO.sMessageToPage != null) {
                    if (omPmssedO.sMessageToPage.Length > 0) {
                        if (omPmssedO.sMessageToPage.Substring(0, 1) == "#") {
                            omVe.CallerUpdateSendMessageToUi(omPmssedO.sMessageToPage);
                            omPmssedO.sMessageToPage = "";
                        }
                    }
                }
            }
        }
        #endregion

        #region SetOutputData
        // *:
        // *  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        protected long MinputTldMainSetOutputData() {
            iMinputTldMainSetOutputData = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // Set Output Data
            // Set File and Dict Data
            // Input File Handling of Pointer
            sTraceMessageBlockString = TransformFileControl.FileColumnText;
            // iTraceCharacterCount += TransformFileControl.FileColumnText.Length + 1;
            // if (true == false) { MinputTldMainSetOutputPointers(); }
            // Output Type Handling
            LocalMessage5 = "";
            switch (TransformFileControl.OutputFile.FileSummary.FileTypeId) {
                case ((int)DatabaseControl.FileData):
                    // Processing Data
                    // TransformFileControl.OutputFile.iDataItemAttributeIndex = TransformFileControl.InputFile.iDataItemAttributeIndex;
                    // TransformFileControl.OutputFile.iaFileColumnIndex = TransformFileControl.OutputFile.iDataItemAttributeIndex;
                    // if (TransformFileControl.OutputFile.iaFileColumnIndex > 500) {
                    // TransformFileControl.OutputFile.iaFileColumnIndex = 500;
                    // }
                    LocalMessage5 += "Processing Data";
                    LocalMessage5 += ", Value=\"" + TransformFileControl.FileColumnText + "\"";
                    LocalMessage5 += ", Buffer Field number(" + TransformFileControl.InputFile.iDataItemAttributeIndex.ToString() + ")";
                    LocalMessage5 += ", Output Attribute number(" + TransformFileControl.OutputFile.iaFileColumnIndex.ToString() + ")";
                    if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMainSetOutputData, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage5 + "\n"); }
                    switch (TransformFileControl.OutputFile.FileSummary.FileSubTypeId) {
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeSQL):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMS):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMY):
                            /*
                            // internal attribute pointer for data item?
                            // TransformFileControl.OutputFile.iDataItemAttributeCounter = 0;
                            // TransformFileControl.OutputFile.iDataItemAttributeIndex = 1;
                            if (TransformFileControl.OutputFile.iDataItemAttributeIndex > 1) {
                                TransformFileControl.OutputFile.FileItemData += ColumnSeparator;
                            }
                            TransformFileControl.OutputFile.FileItemData += TransformFileControl.FileColumnText;
                            */
                            TransformFileControl.OutputFile.OutputValues = TransformFileControl.FileColumnText;
                            if (TransformFileControl.OutputFile.iDataItemAttributeIndex <= 500) {
                                TransformFileControl.OutputFile.iaFileColumnIndex = TransformFileControl.OutputFile.iDataItemAttributeIndex;
                                TransformFileControl.OutputFile.saFileColumnValues[TransformFileControl.OutputFile.iaFileColumnIndex] = TransformFileControl.FileColumnText;
                            }
                            iTraceByteCount += TransformFileControl.FileColumnText.Length;
                            break;
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeCSV):
                            //
                            if (TransformFileControl.OutputFile.iDataItemAttributeIndex <= 500) {
                                TransformFileControl.OutputFile.iaFileColumnIndex = TransformFileControl.OutputFile.iDataItemAttributeIndex;
                                TransformFileControl.OutputFile.saFileColumnValues[TransformFileControl.OutputFile.iaFileColumnIndex] = TransformFileControl.FileColumnText;
                            }
                            // OutputFile.FileItemData += TransformFileControl.FileColumnText;
                            if (TransformFileControl.OutputFile.OutputInsert.Length > 0) {
                                TransformFileControl.OutputFile.OutputInsert += ", ";
                            }
                            // TODO TransformFileControl.OutputFile.OutputInsert += 
                            // // // TransformFileControl.OutputFile.OutputInsert += TransformFileControl.InputFile.iDataItemAttributeIndex.ToString();
                            //
                            if (TransformFileControl.OutputFile.OutputValues.Length > 0) {
                                TransformFileControl.OutputFile.OutputValues += ",";
                                iTraceByteCount += ColumnSeparator.Length;
                            }
                            iTraceByteCount += TransformFileControl.FileColumnText.Length;
                            if ("string1" == "string") {
                                TransformFileControl.OutputFile.OutputValues += "\"" + TransformFileControl.FileColumnText + "\"";
                                iTraceByteCount += ColumnSeparator.Length;
                            } else {
                                TransformFileControl.OutputFile.OutputValues += TransformFileControl.FileColumnText;
                            }
                            break;
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXT):
                            // Text
                            // FileTypeTEXT
                            //
                            // TODO output is not inserted via sql update (never)
                            // TODO Concatenate to current data test this area later
                            if (TransformFileControl.OutputFile.OutputValues.Length > 0) {
                                TransformFileControl.OutputFile.OutputValues += RowSeparator;
                                iTraceByteCount += RowSeparator.Length;
                            }
                            TransformFileControl.OutputFile.OutputValues += TransformFileControl.FileColumnText;
                            iTraceByteCount += TransformFileControl.FileColumnText.Length;

                            break;
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeASC):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDAT):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeFIX):
                            // TODO SubType ASC DAT FIX not supported
                            // TODO FIX IS M/B FORMATTED
                            TransformFileControl.OutputFile.OutputValues += TransformFileControl.FileColumnText;
                            iTraceByteCount += TransformFileControl.FileColumnText.Length;
                            break;
                        default:
                            // error
                            // FileSubTypeUNKNOWN
                            //
                            iMinputTldMainSetOutputData = (int)DatabaseControl.ResultUndefined;
                            TransformFileControl.OutputFile.FileSummary.FileSubTypeId = (long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeUNKNOWN;
                            MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMainSetOutputData, omCo.RunErrorDidOccur = true, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage5 + "\n");
                            LocalMessage5 = "File Subtype error (" + TransformFileControl.OutputFile.FileSummary.FileSubTypeId.ToString() + ") not properly set";
                            MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMainSetOutputData, omCo.RunErrorDidOccur = true, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage5 + "\n");
                            // throw new NotSupportedException(LocalMessage);
                            break;
                    }
                    // TODO output is not inserted via sql update (never.)  Concatenate to current data
                    // TODO test this area later 
                    // TransformFileControl.OutputFile.OutputValues += TransformFileControl.FileColumnText;
                    // if (TransformFileControl.OutputFile.FileDictItemIndex > 0) {
                    // add a column separator depending on output type
                    ColumnSeparatorOutput = ColumnSeparator;
                    // TransformFileControl.OutputFile.FileItemData += ColumnSeparatorOutput;
                    // }
                    // TransformFileControl.OutputFile.FileItemData += TransformFileControl.FileColumnText;
                    break;
                //
                case ((int)DatabaseControl.FileDictData):
                    // Processing FileDictData
                    LocalMessage5 += "Processing Dictionary";
                    LocalMessage5 += ", Value=\"" + TransformFileControl.FileColumnText + "\"";
                    LocalMessage5 += ", Buffer Field number(" + TransformFileControl.InputFile.iDataItemAttributeIndex.ToString() + ")";
                    LocalMessage5 += ", Dictionary Attribute number(" + TransformFileControl.OutputFile.FileDictItemIndex.ToString() + ")";
                    if (TraceOn || ConsoleOn || ConsolePickConsoleOn || ConsolePickConsoleBasicOn) { MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMainSetOutputData, ErrorDidNotOccur, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage5 + "\n"); }
                    // TransformFileControl.OutputFile.FileDictItemIndex = TransformFileControl.OutputFile.iDataItemAttributeIndex;
                    switch (TransformFileControl.OutputFile.FileSummary.FileSubTypeId) {
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeSQL):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMS):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeMY):
                            // internal attribute pointer for dict item
                            // TransformFileControl.OutputFile.FileDictItemIndex = 0;
                            // TransformFileControl.OutputFile.FileDictItemCount = 0;
                            /*
                            const int FileTypePICK = 2;
                            const int FileSubTypeTILDE = 3;
                            const int FileSubTypeTILDE_CSV = 4;
                            const int FileSubTypeTILDE_ROW = 7;
                            const int FileSubTypeTILDE_NATIVE = 8;
                            const int FileSubTypeTILDE_NATIVE_ONE = 9;
                            */
                            iTraceCharacterCount += TransformFileControl.FileColumnText.Length;
                            iTraceByteCount += TransformFileControl.FileColumnText.Length;

                            if (TransformFileControl.OutputFile.FileDictItemIndex == 0) {
                                TransformFileControl.OutputFile.FileItemData = TransformFileControl.OutputFile.FileSummary.FileItemId;
                                iTraceByteCount += RowSeparator.Length;
                                iTraceCharacterCount += RowSeparator.Length;
                                if (TransformFileControl.InputFile.FileSummary.FileSubTypeId == (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTILDE) {
                                    iTraceByteCount += RowSeparator.Length;
                                    iTraceCharacterCount += RowSeparator.Length;
                                }
                                TransformFileControl.OutputFile.FileSummary.FileItemId = TransformFileControl.OutputFile.FileSummary.FileItemId;
                                TransformFileControl.OutputFile.saFileDictItem[TransformFileControl.OutputFile.FileDictItemIndex] = TransformFileControl.OutputFile.FileSummary.FileItemId;
                            } else {
                                TransformFileControl.OutputFile.FileItemData += ColumnSeparator;
                                iTraceByteCount += ColumnSeparator.Length;
                                iTraceCharacterCount += RowSeparator.Length;
                                TransformFileControl.OutputFile.FileItemData += TransformFileControl.FileColumnText;
                                if (TransformFileControl.OutputFile.FileDictItemIndex < 100) {
                                    TransformFileControl.OutputFile.saFileDictItem[TransformFileControl.OutputFile.FileDictItemIndex] = TransformFileControl.FileColumnText;
                                }
                            }
                            // Load data to dictionary definition table
                            // }
                            //
                            break;
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeCSV):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeFIX):
                            // depending on output type
                            ColumnSeparatorOutput = ColumnSeparator;
                            TransformFileControl.OutputFile.FileItemData += ColumnSeparatorOutput;
                            // TransformFileControl.OutputFile.OutputValues += TransformFileControl.FileColumnText;

                            TransformFileControl.OutputFile.FileItemData += TransformFileControl.FileColumnText;
                            iTraceByteCount += TransformFileControl.FileColumnText.Length;
                            iTraceCharacterCount += TransformFileControl.FileColumnText.Length;
                            break;
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeTEXT):
                            // Text
                            TransformFileControl.OutputFile.OutputValues += TransformFileControl.FileColumnText;
                            iTraceByteCount += TransformFileControl.FileColumnText.Length;
                            iTraceCharacterCount += TransformFileControl.FileColumnText.Length;
                            break;
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeASC):
                        case ((long)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeDAT):
                            // error SubType not supported
                            TransformFileControl.OutputFile.OutputValues += TransformFileControl.FileColumnText;
                            iTraceByteCount += TransformFileControl.FileColumnText.Length;
                            iTraceCharacterCount += TransformFileControl.FileColumnText.Length;
                            break;
                        default:
                            // error SubType Unknonw
                            // FileSubTypeUNKNOWN
                            iMinputTldMainSetOutputData = (int)DatabaseControl.ResultUndefined;
                            TransformFileControl.OutputFile.FileSummary.FileSubTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileSubTypeIs.FileSubTypeUNKNOWN;

                            TransformFileControl.OutputFile.OutputValues += TransformFileControl.FileColumnText;
                            iTraceByteCount += TransformFileControl.FileColumnText.Length;
                            iTraceCharacterCount += TransformFileControl.FileColumnText.Length;

                            MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMainSetOutputData, omCo.RunErrorDidOccur = true, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage5 + "\n");
                            LocalMessage5 = "File Subtype error (" + TransformFileControl.OutputFile.FileSummary.FileSubTypeId.ToString() + ") not properly set";
                            MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMainSetOutputData, omCo.RunErrorDidOccur = true, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage5 + "\n");
                            // throw new NotSupportedException(LocalMessage);
                            break;
                    }

                    break;
                default:
                    // FileTypeUNKNOWN
                    iMinputTldMainSetOutputData = (int)DatabaseControl.ResultUndefined;
                    TransformFileControl.OutputFile.FileSummary.FileTypeId = (int)Mdm.Oss.Decl.DefStdBaseRunFile.FileTypeIs.FileTypeUNKNOWN;
                    // TransformFileControl.OutputFile.FileSummary.FileSubTypeId = FileSubTypeUNKNOWN;
                    iIterationCount = 0;
                    LocalMessage0 = "File Type error (" + TransformFileControl.OutputFile.FileSummary.FileTypeId.ToString() + ") not properly set";
                    MdmTraceDoPoint(TransformFileControl.InputFile.FileBufferCharMaxIndex,  TransformFileControl.InputFile.iDataItemAttributeMaxIndex, iMinputTldMainSetOutputData, omCo.RunErrorDidOccur = true, iNoOp, iNoOp, bNO, MessageNoUserEntry, "C" + LocalMessage0 + "\n");
                    // throw new NotSupportedException(LocalMessage);
                    break;
            } // end or is DATA Attribute not DICT
            return iMinputTldMainSetOutputData;
        }
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Action and Locals
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Run Action Handling
        #region Run Action Constants
        //
        // if (omCo.iaRunActionState[RunCancel, RunStateCurr] == RunTense_Do || omCo.iaRunActionState[RunPause, RunStateCurr] == RunTense_Did || omCo.iaRunActionState[RunCancel, RunStateCurr] == RunTense_Doing) { bTemp9 = bYES; }
        // if (omCo.iaRunActionState[RunAbort, RunStateCurr] == RunTense_Do || omCo.iaRunActionState[RunPause, RunStateCurr] == RunTense_Did || omCo.iaRunActionState[RunAbort, RunStateCurr] == RunTense_Doing) { bTemp9 = bYES; }
        //
        public int RunTense = 0;
        public const int RunTense_Off = 0;
        public const int RunTense_Do = 1;
        public const int RunTenseOn = 1;
        public const int RunTense_DoNot = 2;
        public const int RunTense_Doing = 3;
        public const int RunTense_Did = 4;
        public const int RunTense_Done = 4;
        public const int RunTense_DidNot = 5;
        //
        public int RunMetric = 0;
        public const int RunState = 1;
        public const int RunState_Last_Update = 2;
        public const int RunDoLast_Count = 3;
        public const int RunDoCount = 4;
        public const int RunDoSkip_Count = 5;
        public const int RunDoError_Count = 6;
        public const int RunDoWarning_Count = 7;
        public const int RunDoRetry_Count = 8;
        //
        public static int iaRunActionState_Max = 25;
        public int[,] iaRunActionState = new int[iaRunActionState_Max + 5, 8];
        public ProgressChangedEventArgs ePceaRunActionState = new ProgressChangedEventArgs((int)0, "");
        //
        public int RunAction = 0;
        public int RunActionRequest = 0;
        //
        public const int RunCancel = 1;
        public const int RunPause = 2;
        public const int RunStart = 3;
        public const int RunNoOp4 = 4;
        public const int RunNoOp5 = 5;
        public const int RunInitialize = 6;
        public const int RunRunDo = 7;
        public const int RunUserInput = 8;
        public const int RunOpen = 9;
        public const int RunMain_Do = 10;
        public const int RunMain_DoSelect = 11;
        public const int RunMain_DoLock_Add = 12;
        public const int RunMain_DoRead = 13;
        public const int RunMain_DoValidate = 14;
        public const int RunMain_DoAccept = 15;
        public const int RunMain_DoReport = 16;
        public const int RunMain_DoProcess = 17;
        public const int RunMain_DoUpdate = 18;
        public const int RunMain_DoWrite = 19;
        public const int RunMain_DoLock_Remove = 20;
        public const int RunClose = 21;
        public const int RunFinish = 22;
        public const int RunAbort = 23;
        public const int RunReloop = 24;
        public const int RunFirst = 25;
        public int RunOptionX = iaRunActionState_Max + 1;
        public int RunOptionY = iaRunActionState_Max + 2;
        public int RunOptionZ = iaRunActionState_Max + 3;
        public int RunOption1 = iaRunActionState_Max + 4;
        public int RunOption2 = iaRunActionState_Max + 5;
        //
        public string[] sRunActionVerb = { "NoOp", 
                                               "Cancel", "Pause", "Start", "NoOp4", "NoOp5", 
                                               "Initialize", "Do", "UserInput", "Open", "DoMain",
                                               "Select", "Lock", "Read", "Validate", "Accept",
                                               "Report", "Process", "Update", "Write", "UnLock",
                                               "Finish", "Abort", 
                                               "OptionX", "OptionY", "OptionZ", "VerbY", "VerbZ"
                                           };
        public string[] sRunActionDoing = { "NoOping", 
                                                "Cancelling", "Pausing", "Starting", "NoOp4", "NoOp5", 
                                               "Initialize", "Doing", "UserInputing", "Opening", "DoingMain",
                                               "Selecting", "Locking", "Reading", "Validating", "Accepting",
                                               "Reporting", "Processing", "Updating", "Writing", "UnLocking",
                                               "Finishing", "Abortint", 
                                               "OptionXing", "OptionYing", "OptionZing", "VerbYing", "VerbZing"
                                            };
        public string[] sRunActionDid = { "NoOped", 
                                              "Cancelled", "Paused", "Started", "NoOp4", "NoOp5", 
                                               "Initialized", "Did", "UserInputed", "Opened", "DoMained",
                                               "Selected", "Locked", "Read", "Validated", "Accepted",
                                               "Reported", "Processed", "Updated", "Writen", "UnLocked",
                                               "Finished", "Aborted", 
                                               "OptionXed", "OptionYed", "OptionZed", "VerbYed", "VerbZed"
                                          };
        //
        #endregion
        #region Run Action State
        #region Run State
        // <Area Id = "PrimaryActions">
        private string spRunOptions;
        public string RunOptions { get { return spRunOptions; } set { spRunOptions = value; } }
        public int ipRunStatus;
        public int RunStatus { get { return ipRunStatus; } set { ipRunStatus = value; } }
        //
        public string FileActionRequest;
        public string sPickFileActionRequest;
        // <Area Id = "RunStatusControlItFlags">
        public int RunCount = 0;
        public int RunDebugCount = 0;
        public bool bRunReloop = false;
        public bool bRunFirst = true;
        //
        public bool bRunStartPending = bNO;
        public bool bRunPausePending = bNO;
        public bool bRunCancelPending = bNO;
        //
        public string sRunActionRequest;
        public string UserState;
        public string UserCommandPrefix;
        public string UserCommand;
        #endregion
        #region Run Errors
        // <Area Id = "Errors">
        public bool bRunAbort = bNO;
        //
        public bool RunErrorDidOccur = false;
        public bool RunErrorDidOccurOnce = false;
        //
        public string sLocalErrorMessage = "";
        //
        public int RunErrorNumber = 0;
        public int RunGlobalErrorNumber = 99999;
        public int RunThrowException = 99999;
        public int RunShellErrorNumber = 99999;
        public int RunErrorCount = 0;
        #endregion
        #region Run Processing Loop
        // <Area Id = "IterationStatusControlItFlags">
        public int iIterationCount = 0;
        public int iIterationRemaider = 0;
        public int iIterationDebugCount = 0;
        public bool IterationAbort = false;
        public bool IterationReloop = false;
        public bool IterationFirst = true;
        public int iIterationLoopCounter = 0;
        // <Area Id = "MethodIterationStatusControlItFlags">
        public bool MethodIterationAbort = false;
        public bool MethodIterationReloop = false;
        public bool MethodIterationFirst = true;
        public int MethodIterationLoopCounter = 0;
        #endregion
        #endregion
        #region Run Action Evaluate Analysis
        public void AppRunActionEvaluate(object sender, ProgressChangedEventArgs ePcea, Mcontroller omHPassed) {
            // This code handles both the Page UI command request and call backs from BgWorker
            try {
                UserState = (string)ePcea.UserState;
            } catch { UserState = ""; }
            UserCommandPrefix = "";
            UserCommand = "";
            if (UserState.Length > 0) { UserCommandPrefix = UserState.Substring(0, 1); }
            if (UserState.Length > 1) { UserCommand = UserState.Substring(1); }

            if (UserCommandPrefix == "$") {
                if (UserCommand == "Start") {
                    if (omHPassed.iaRunActionState[RunRunDo, RunState] != RunTense_Did && omHPassed.iaRunActionState[RunRunDo, RunState] != RunTense_Doing) {
                        bRunStartPending = bYES;
                        omHPassed.iaRunActionState[RunAbort, RunState] = iNO;
                        omHPassed.iaRunActionState[RunFirst, RunState] = iYES;
                        omHPassed.iaRunActionState[RunReloop, RunState] = iNO;
                        RunAction = RunRunDo;
                        RunMetric = RunState;
                        RunTense = RunTense_Do;
                        omHPassed.iaRunActionState[RunRunDo, RunState] = RunTense_Do;
                        omHPassed.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString());
                        omVe.backgroundWorker_ProgressChanged(this, omHPassed.ePceaRunActionState);
                    }
                } else if (UserCommand == "Started") {
                    bRunStartPending = bNO;
                    omHPassed.iaRunActionState[RunRunDo, RunState] = RunTense_Did;
                } else if (UserCommand == "Cancel") {
                    if (omHPassed.iaRunActionState[RunRunDo, RunState] == RunTense_Doing) {
                        if (omHPassed.iaRunActionState[RunCancel, RunState] != RunTense_Did && omHPassed.iaRunActionState[RunCancel, RunState] != RunTense_Doing) {
                            bRunCancelPending = bYES;
                            RunAction = RunCancel;
                            RunMetric = RunState;
                            RunTense = RunTense_Do;
                            omHPassed.iaRunActionState[RunCancel, RunState] = RunTense_Do;
                            omHPassed.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                                "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString());
                            omVe.backgroundWorker_ProgressChanged(this, omHPassed.ePceaRunActionState);
                            omHPassed.RunCancelAsync();
                            // System.Windows.RoutedEventArgs eTcea = new System.Windows.RoutedEventArgs;
                            // eTcea.Source = this;
                            // omVe.CallerAsynchronousEventsCancelClick(this, eTcea);
                        }
                    }
                } else if (UserCommand == "Cancelled") {
                    bRunCancelPending = bNO;
                    RunAction = RunCancel;
                    RunMetric = RunState;
                    RunTense = RunTense_Do;
                    omHPassed.iaRunActionState[RunCancel, RunState] = RunTense_Did;
                    omHPassed.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                        "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString());
                    omVe.backgroundWorker_ProgressChanged(this, omHPassed.ePceaRunActionState);
                } else if (UserCommand == "Pause") {
                    if (!bRunCancelPending) {
                        RunAction = RunPause;
                        RunMetric = RunState;
                        if (omHPassed.iaRunActionState[RunPause, RunState] == RunTense_Did) {
                            bRunPausePending = bNO;
                            RunTense = RunTense_Off;
                        } else {
                            // System.Windows.RoutedEventArgs eReaTemp = new System.Windows.RoutedEventArgs();
                            // eReaTemp = null;
                            // omWt.CallerAsynchronousEventsPauseClick;
                            // Set State
                            if (omHPassed.iaRunActionState[RunRunDo, RunState] == RunTense_Doing) {
                                if (omHPassed.iaRunActionState[RunPause, RunState] != RunTense_Did && omHPassed.iaRunActionState[RunPause, RunState] != RunTense_Doing) {
                                    bRunPausePending = bYES;
                                    RunTense = RunTense_Do;
                                    omHPassed.iaRunActionState[RunPause, RunState] = RunTense_Do;
                                }
                            }
                        }
                        omHPassed.iaRunActionState[RunPause, RunState] = RunTense;
                        omHPassed.ePceaRunActionState = new ProgressChangedEventArgs((int)opLocalProgressBar_Value,
                "R" + RunMetric.ToString() + RunTense.ToString() + RunAction.ToString());
                        omVe.backgroundWorker_ProgressChanged(this, omHPassed.ePceaRunActionState);
                    }
                } else if (UserCommand == "Paused") {
                    bRunPausePending = bNO;
                    omHPassed.iaRunActionState[RunPause, RunState] = RunTense_Did;
                } else if (UserCommand == "xxxxx") {

                }
            }
        }
        #endregion
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #region Local Run Action Pause
        public int iAppActionWaitMilliIncrement = 500;
        public int iAppActionWaitMilliIncrementMax = 30000;
        public bool bAppActionWaitContinue = true;
        public int iAppActionWaitCounter;
        #endregion
        #region Local Progress Bar
        //
        ProgressBar opLocalProgressBar;
        public double opLocalProgressBar_Minimum = 0;
        public double opLocalProgressBar_Maximum = 0;
        public double opLocalProgressBar_Value = 0;
        public int opLocalProgressBar_Display = 0;
        //
        ProgressChangedEventArgs eaOpcLocalRunProgressChanged;
        RoutedPropertyChangedEventArgs<double> eadRpcLocalRunProgressChanged;
        RunWorkerCompletedEventArgs eaRwcLocalRunCompleted;
        double eadRpcLocalOldValue = 0;
        double eadRpcLocalNewValue = 0;
        #endregion
        // xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        #endregion
    }
}
