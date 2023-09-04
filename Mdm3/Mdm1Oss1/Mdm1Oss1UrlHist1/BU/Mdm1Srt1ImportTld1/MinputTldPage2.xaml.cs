using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
// using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//
// using Mdm.Oss.ClipboardUtil;
using Mdm.Oss.CodeUtil;
using Mdm.Oss.Decl;
//using    Mdm.Oss.Support;
using Mdm.Oss.Mapp;
using Mdm.Oss.Mobj;
using Mdm.Oss.FileUtil;
//using    Mdm1Oss1FileCreation1;


namespace Mdm.Srt.InputTld {
    /// <summary>
    /// Interaction logic for MinputTldPageDetail.xaml
    /// </summary>
    public partial class MinputTldPageDetail : PageFunction<String> {
        delegate void UpdatePageUiHandler(RoutedEventHandler erehPassedRoutedEventHandler);
        delegate void UpdatePageUiEvent(RoutedEventArgs ereaPassedRoutedEventArgs);
        /*
using System.Windows; // RoutedEventArgs, RoutedEventHandler, Visibility
using System.Windows.Controls; // Page
using System.Windows.Navigation; // ReturnEventArgs
         * 
        // public partial class PageFunction<"Mdm.Srt.InputTld.MinputTldPageDetail"> : Page
        // public class PageFunction<T> : PageFunctionBase
        // public class PageFunction<MinputTldPageDetail> : PageFunctionBase
         * 
        // public partial class MinputTldPageDetail : PageFunction<String> ?
         * 
        // public class PageFunction<T> : PageFunctionBase
        // partial?
         * 
        // Tld1Page2Arguments?
         * 
        // public MinputTldPageDetail()
        // {
        // InitializeComponent();
        // } 
        //public partial class MinputTldPageDetail : Page
        //{
         */
        #region MdmStandardRunControlDeclarations
        // <Section Summary>
        // <Section Role="Declarations">
        // <Section Id = "MdmStandardRunControl">
        // <Section Vs="MdmStdRunVs0_8_8">
        // MdmStandardRunControlDeclarations MdmStdRunVs0_8_8
        // <Area Id = "ConsoleCommandActions>
        public string sCommandLineRequest;
        public int iCommandLineRequest = 0;
        // <Area Id = "PrimaryActions">
        public string FileActionRequest;
        public string sPickFileActionRequest;
        // <Area Id = "Console">
        public string sConsoleCommand;
        public string sConsoleOutput;
        public string sConsoleOutputLog = "";
        public bool ConsoleOn = true;
        public bool ConsoleToDisc = true;
        // <Area Id = "Errors">
        public int RunErrorNumber = 0;
        public int RunShellErrorNumber = 0;
        public string sLocalErrorMessage = "";
        // <Area Id = "RunStatusControlItFlags">
        public bool bRunAbort = false;
        public bool bRunReloop = false;
        public bool bRunFirst = true;
        // <Area Id = "IterationStatusControlItFlags">
        public int iIterationCount = 99999;
        public int iIterationDebugCount = 99999;
        public bool IterationAbort = false;
        public bool IterationReloop = false;
        public bool IterationFirst = true;
        public int iIterationLoopCounter = 0;
        // <Area Id = "MethodIterationStatusControlItFlags">
        public bool MethodIterationAbort = false;
        public bool MethodIterationReloop = false;
        public bool MethodIterationFirst = true;
        public int MethodIterationLoopCounter = 0;
        // <Area Id = "ExecutionRun">
        public string RunOptions;
        // <Area Id = "RunStatus">
        public int RunStatus = 99999;
        // </Section Summary>
        #endregion

        #region (External) Commands Arguments and Returns
        string sPassedString = "";
        #endregion
        #region (Local) Status and Messages
        public int LocalIntResult;
        public bool LocalBoolResult;
        public bool LocalStarted = false;
        public int iLocalStartOk = 99999;
        public string StatusLineMdmText1 = "99999";
        public string StatusLineMdmText2 = "99999";
        public string StatusLineMdmText3 = "99999";
        public TextBlock PageFunctionResultsTextBlock;
        #endregion
        #region Package Object Declarations
        public Application omAp;
        // <Area Id = "omHControl">
        public Mcontroller omCo;
        // <Area Id = "omoLocalMop Mobject">
        public Mobject omOb;
        // <Area Id = "MdmLocalVerb">
        // <Area Id = "omAplicationThread">
        public MinputTldThread omVe;
        // <Area Id = "omAplication">
        public Mapplication omMa;
        // <Area Id = "omW">
        public MinputTld omWt;
        #endregion
        #region Page Declartions
        // <Area Id = "omP">
        public MinputTldPageMain omPm;
        public string sPage1ReturnValue;
        // <Area Id = "omP2">
        public MinputTldPageDetail omPd;
        public string sPage2ReturnValue;
        #endregion
        #region MinputTldPageDetail Input Field Declarations
        string OutputSystemCurrent = "99999";
        long OutputSystemCurrentNotValid = 99999;

        string OutputDatabaseCurrent = "99999";
        long OutputDatabaseCurrentNotValid = 99999;

        string OutputFileCurrent = "99999";
        long OutputFileCurrentNotValid = 99999;

        string InputFileCurrent = "99999";
        long InputFileCurrentNotValid = 99999;

        string OutputFileItemIdCurrent = "99999";
        long OutputFileItemIdCurrentNotValid = 99999;

        bool OptionToDoOverwriteExistingItemCurrent;
        bool OptionToDoCheckItemIdsCurrent;
        bool OptionToDoCheckFileDoesExistCurrent;
        bool OptionToDoEnterEachItemIdCurrent;
        bool OptionToDoLogActivityCurrent;
        bool OptionToDoProceedAutomaticallyCurrent;
        bool OptionToDoCreateMissingFileCurrent;

        #endregion
        // Class Internal Method Results - xxxxxx
        #region ClasInternalResults
        //
        public long iAppCoreObjectGet;
        public long iDoesExist;

        public long iMainPage2Processing;
        //
        public long OutputSystemCurrentNotValidResult;
        //
        public long iPageLoaded;
        public long iPageLoadedEvent;
        public long iPage2;
        public long iPage2Loaded;
        public long iPage2LoadedEvent;
        public long iPage2PassedMob;
        public long iPage2PassedString;

        //
        public long iSetApplication;
        //
        // Local Messages
        public string LocalMessage = "";
        public string LocalMessage1 = "";
        public string LocalMessage2 = "";
        public string LocalMessage3 = "";
        //
        #endregion
        // MinputTldPageDetail - Construct, Load, SetObjects xxxxxxxxxxxxxxxx
        public delegate void EventHandler(Object sender, EventArgs e);
        public event EventHandler NoDataEventHandler;

        #region Construct Initialize
        public MinputTldPageDetail() {
            iPage2 = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            StatusLineMdmText1 = "";
            StatusLineMdmText2 = "";
            InitializeComponent();
            this.PageScrollViewer.Content = this.PageGridMain;

            this.Content = this.PageScrollViewer;

            this.StatusLine1.Text = "";
            this.StatusLine2.Text = "";
            this.Title = "NOW PAGE2";

            // Page2SizeChangedDoAdjust(this, new RoutedEventArgs());

            iPage2 = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
        }

        public MinputTldPageDetail(Mobject omPmssedO)
            : this() {
            iPage2PassedMob = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            LocalMessage3 = "NOW PAGE 2 Passed Mob";
            if (true == false) {
                if (omOb == null & omPmssedO != null) {
                    omOb = omPmssedO;
                    iPageLoaded = AppCoreObjectGet(omOb);
                }
            }
            // Page2Loaded(omOb);
            iPage2 = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
        }

        public MinputTldPageDetail(Mobject omPmssedO, string PassedString)
            : this() {
            iPage2PassedString = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            LocalMessage3 = "NOW PAGE 2 Passed Mob and String";
            if (omOb == null & omPmssedO != null) {
                omOb = omPmssedO;
                iPageLoaded = AppCoreObjectGet(omOb);
            }
            sPassedString = PassedString;
            Page2Loaded(omOb);
        }

        public string DoesExist(string PassedString) {
            iDoesExist = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // sPassedString = PassedString;
            sPassedString = PassedString;
            // iDoesExist = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            return PassedString + "  Yes, I (MinputTldPageDetail) Exist.";
        }
        #region MdmApplicationOjbectGet
        public long AppCoreObjectGet(Mobject omPmssedO) {
            iAppCoreObjectGet = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            // MinputTldApp omOb;
            // MinputTldApp omOb - Standard Object
            if (omOb == null && omPmssedO == null) {
                omOb = new Mobject(omAp, omPm);
                // omOb = omCo.omOb;
                // omOb = (Mobject)ApplicationMobjectObjectGetFromMaobject();
            }
            if (omOb == null) {
                omOb = omPmssedO;
                //omPmssedO omOb = ApplicationMobjectObjectGetFromMaobject();
                // omOb = (Mobject)ApplicationMobjectObjectGetFromMaobject();
                // omOb = this;
                // ApplicationMobjectObjectSetMobject(this);
            }
            // MinputTldApp omAp
            if (omAp == null) {
                omAp = omMa.ApplicationAppObjectGet();
                // omAp = (Application)ApplicationAppObjectGet();
                // omAp = this;
                // ApplicationAppObjectSet(this);
            }
            // MinputTldApp omH
            if (omCo == null) {
                // omCo = ApplicationHandlerObjectGet();
                omCo = (Mcontroller)omMa.ApplicationHandlerObjectGet();
                // omCo = this;
                // ApplicationHandlerObjectSet(this);
            }
            if (omPm == null || omPd == null) {
                if (omPm == null) {
                    if (omOb.omPm != null) { omPm = (MinputTldPageMain)omOb.omPm; }
                }
                if (omPd == null) {
                    omPd = this;
                    omMa.AppPageDetailObjectSet(omPd);
                }
                iAppCoreObjectGet = omCo.AppPageMainObjectSet(omPm, omPd);
            }
            // MinputTldApp Page;
            if (omPm == null) {
                // omPm = AppPageMainObjectGet();
                omPm = (MinputTldPageMain)omMa.AppPageMainObjectGet();
                // omPm = this
                // AppPageMainObjectSet(this);
            }
            // MinputTldApp MinputTldPageDetail;
            if (omPd == null) {
                // omPd = AppPageDetailObjectGet();
                omPd = (MinputTldPageDetail)omMa.AppPageDetailObjectGet();
                // omPd = this
                // AppPageDetailObjectSet(this);
            }
            // MinputTldApp Verb
            if (omVe == null) {
                // omVe = ApplicationVerbObjectGet();
                omVe = (MinputTldThread)omMa.ApplicationVerbObjectGet();
                // omVe = this;
                // ApplicationVerbObjectSet(this);
            }
            // MinputTldApp omMa
            if (omMa == null) {
                omMa = (Mapplication)omMa.ApplicationMappObjectGet();
                // omMa = (Mapplication)MapplicationMappObjectGet();
                // omMa = this;
                // MapplicationMappObjectSet(this);
            }
            // MinputTldApp omW
            if (omWt == null) {
                omWt = (MinputTld)omMa.ApplicationMbgWorkerObjectGet();
                // omWt = (BgWorkerlication)BgWorkerlicationMbgWorkerObjectGet();
                // omWt = this;
                // BgWorkerlicationMbgWorkerObjectSet(this);
            }
            iAppCoreObjectGet = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
            return iAppCoreObjectGet;
        }
        #endregion
        /*
        public Page PageFunction()
        {
            Page tmp = null;

            return tmp;
        }
        */
        void Page2Loaded(object sender, RoutedEventArgs e) {
            iPage2LoadedEvent = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            StatusLine1.Text += "MinputTldPageDetail Loading complete... " + "\n";
            Page2Loaded(omOb);
            iPage2LoadedEvent = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
        }

        private void Page2Loaded(Mobject omPmssedO) {
            iPageLoaded = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            StatusLine2.Text += "MinputTldPageDetail Preparing please wait... " + "\n";

            // WindowWidth = 700;
            // WindowHeight = 500;
            // MinWidth = 400;
            // MinHeight = 400;
            // MaxWidth = 800;
            // MaxHeight = 800;

            // Width = omPm.ActualWidth;
            // Height = omPm.ActualHeight;

            omPm.PageSizeChangedDoAdjust((Page)this, 0, 0);

            StatusLine1.Text += "Prepare OK... " + "\n";

            if (this._contentLoaded) {
                StatusLine1.Text += "loaded OK... " + "\n";
            }

            // Application Started
            if (!LocalStarted) {
                ControlSetFocus(OutputSystemLine);
                LocalStarted = true;
            }

            LocalIntResult = (int)omCo.AppPageDetailLoaded(omOb);
            iPageLoaded = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
        }

        private void Page2LoadedOld() {
            // TODO Page2SizeChangedDoAdjust(this, new RoutedEventArgs());

            //  Height and Width
            /*
            if (this.ActualHeight > this.Height || this.ActualWidth > this.Width) {
                //  Height
                if (this.ActualHeight > this.Height) {
                    this.InvalidateVisual();
                }
                //  Width
                if (this.ActualWidth > this.Width) {
                    this.InvalidateVisual();
                }
            }
            */
            if ((ActualWidth > 0 && this.ActualHeight < this.MinHeight) || (ActualWidth > 0 && this.ActualWidth < this.MinWidth)) {
                // Java Script Handling
                this.Height = this.MinHeight;
                // this.DatabaseOutput1.Height = this.Height - 20;
                this.Width = this.MinWidth;
                this.DatabaseTabControl1.Width = this.Width - 20;
                // this.NavigationService.Refresh();
                this.InvalidateVisual();

            }
            Page2SizeChangedDoAdjust(this, 0, 0);
            // Application Started
            if (!LocalStarted) {
                ControlSetFocus(OutputSystemLine);
                LocalStarted = true;
            }
            StatusLine1.Text += "Prepare OK... " + "\n";
            iPageLoaded = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.ValidResult;
        }

        private void Page2SizeChanged(object sender, RoutedEventArgs e) {
            Page2SizeChangedDoAdjust((Page)sender, 0, 0);
        }
        public void Page2SizeChangedDoAdjust(Page sender, double dPassedDesiredWidth, double dPassedDesiredHeight) {
            // TODO Window Height Width and Focus
            int iTemp0 = 0;
            double dBase_Width = base.Width;
            double dWindowWidth = 0;
            double dGridMainWidth = 0;
            string sTemp0 = "";
            string sParent_ActualWidth = (Parent.GetValue((Window.ActualWidthProperty)).ToString());
            string sWindow_ActualWidth = (Window.ActualWidthProperty).ToString();
            string sFrame_ActualWidth = (Frame.ActualWidthProperty).ToString();

            double dGridActualWidth = PageGridMain.ActualWidth;
            double dGridActualHeight = PageGridMain.ActualHeight;
            //
            double dDesiredWidth = dPassedDesiredWidth;
            double dDesiredHeight = dPassedDesiredHeight;
            if (dDesiredWidth == 0) {
                sParent_ActualWidth = (this.Parent.GetValue((Window.ActualWidthProperty)).ToString());
                dWindowWidth = Convert.ToDouble(sParent_ActualWidth);
                dDesiredWidth = sender.ActualWidth; 
            }
            if (dDesiredHeight == 0) { dDesiredHeight = sender.ActualHeight; }
            omPm.PageSizeChangedDoAdjust(sender, dDesiredWidth, dDesiredHeight);
            // xxxxxxxxxxxxxxxxxxx
            // this.DatabaseOutput1.Height = this.Height - 20;
            // Panel
            int iStackPanelAdjustment = 4;
            int iStackPanelWidth = 4;
            int iLastColWidth = 25;
            int iTextLineAdjustment = 14;
            int iTextInputWidth = 380;
            //
            double dColWidth_0 = gcCol0.ActualWidth;
            double dColWidth_1 = gcCol1.ActualWidth;
            double dColWidth_2 = gcCol2.ActualWidth;
            double dColWidth_3 = gcCol3.ActualWidth;
            double dColWidth_4 = gcCol4.ActualWidth;
            double dColWidth_5 = gcCol5.ActualWidth;

            if (Width > 2000) {
                try {
                    // sFieldConversionRootVerbType = (Window.WidthProperty).ToString();
                    // sFieldConversionRootVerbType = (Frame.WidthProperty).ToString();
                    // sFieldConversionRootVerbType = (this.Parent.GetValue((Window.WidthProperty)).ToString());
                    // dBase_Width = Convert.ToDouble(sFieldConversionRootVerbType);
                    // sWindow_ActualWidth = (Window.ActualWidthProperty).ToString();
                    // sFrame_ActualWidth = (Frame.ActualWidthProperty).ToString();
                    sWindow_ActualWidth = (this.Parent.GetValue((Window.ActualWidthProperty)).ToString());
                    dWindowWidth = Convert.ToDouble(sWindow_ActualWidth);

                    if (dWindowWidth > 0) {
                        dGridMainWidth = dWindowWidth - 10;


                        // Grid Size
                        PageGridMain.Width = dGridMainWidth;
                        // Tab Panel Size
                        DatabaseTabControl1.Width = dWindowWidth - (double)iLastColWidth;
                        // Stack Panel Size
                        iStackPanelWidth = (int)DatabaseTabControl1.Width - iStackPanelAdjustment;
                        // Wrap Panel Size
                        iStackPanelWidth = (int)DatabaseTabControl1.Width - iStackPanelAdjustment;
                        // Extractable Grid
                        GridLength tGridLength = new GridLength(iTextInputWidth, GridUnitType.Pixel);
                        //
                        iTextInputWidth = (int)(gcCol1.ActualWidth + gcCol2.ActualWidth + gcCol3.ActualWidth + gcCol4.ActualWidth) - iTextLineAdjustment;

                        /*
                        foreach (RowDefinition tmp in Mdm.Srt.InputTldPage2Grid1Main.RowDefinitions) {
                            // RowDefinition tmp = Mdm.Srt.InputTldPage2Grid1Main.RowDefinitions. (iRow) in
                            ;
                        }
                        */
                        // OutputFile1Tab1.Width = DatabaseTabControl1.Width;
                        // DbMasterServer1Tab2.Width = DatabaseTabControl1.Width;
                        // DbUser1Tab3.Width = DatabaseTabControl1.Width;
                        // DbSecurity1Tab4.Width = DatabaseTabControl1.Width;
                        // Stack Panels
                        OutputFile1Tab1StackPanel.Width = iStackPanelWidth;
                        DbServer1Tab2StackPanel.Width = iStackPanelWidth;
                        DbUser1Tab3StackPanel.Width = iStackPanelWidth;
                        DbSecurity1Tab4StackPanel.Width = iStackPanelWidth;
                        // Output Tab
                        //      Lines
                        // OutputSystemLabel.HorizontalAlignment = HorizontalAlignment.Left;
                        // OutputSystemLine.Width = DatabaseOutput1.Width - 150;
                        // OutputDatabaseLabel.HorizontalAlignment = HorizontalAlignment.Left;
                        // OutputDatabaseLine.Width = ?;
                        OutputFileGroupLine.Width = iTextInputWidth;
                        OutputFileLine.Width = iTextInputWidth;
                        //
                        // Master Sever Tab
                        //      Lines
                        DbMasterOutputSystemLine.Width = iTextInputWidth;
                        DbMasterOutputDatabaseLine.Width = iTextInputWidth;
                        DbMasterOutputFileLine.Width = iTextInputWidth;
                        //
                        // User Tab
                        //      Lines

                        //
                        // Security Tab
                        //      Lines

                        // Command Buttons

                        // Console Command
                        ConsoleCommandText.Width = iTextInputWidth;
                        // Status Lines
                        StatusLine1.Width = DatabaseTabControl1.Width - iStackPanelAdjustment;
                        StatusLine2.Width = DatabaseTabControl1.Width - iStackPanelAdjustment;
                        // Redraw
                        // InvalidateVisual();
                    }
                } catch {
                    // dWindowWidth = Width;
                    ;
                }
            }
        }
        #endregion
        #region Page Focus Handling
        public void ControlSetFocus(System.Windows.Controls.Control control) {
            // Set focus to the control, if it can receive focus.
            if (control.Focusable) {
                control.Focus();
            }
        }
        #region Output File
        private void OutputSystemLineGotFocus(object sender, RoutedEventArgs e) {
            StatusLine1.Text += "Enter the name of the System where your Output Database resides." + "\n";
            // OutputSystemLine.Text = "localhost";
        }
        private void OutputSystemLineLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void OutputDatabaseLineGotFocus(object sender, RoutedEventArgs e) {
            StatusLine1.Text += "Enter the name of the Database where the output file exists." + "\n";
            // OutputDatabaseLine.Text = "dbo";
        }
        private void OutputDatabaseLineLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void OutputFileGroupLineGotFocus(object sender, RoutedEventArgs e) {
            StatusLine1.Text += "Enter the name of the Database where the output file exists." + "\n";
            // OutputFileGroupLine.Text = "";
        }
        private void OutputFileGroupLineLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void OutputFileLineGotFocus(object sender, RoutedEventArgs e) {
            StatusLine1.Text += "Enter the file name to place the data into." + "\n";
            // omPd.OutputFileLine.Text = omPm.OutputFileLine.Text;
        }
        private void OutputFileLineLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        #endregion
        #region Master Server
        private void MasterOutputSystemLineGotFocus(object sender, RoutedEventArgs e) {
            StatusLine1.Text += "Enter the name of the System where your Output Database resides." + "\n";
            // omPd.DbMasterOutputSystemLine.Text = omPd.OutputSystemLine.Text;
        }
        private void MasterOutputSystemLineLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void MasterOutputDatabaseLineGotFocus(object sender, RoutedEventArgs e) {
            StatusLine1.Text += "Enter the name of the Database where the output file exists." + "\n";
            // omPd.DbMasterOutputDatabaseLine.Text = omPd.OutputDatabaseLine.Text;
            DbMasterOutputDatabaseLine.Text = OutputDatabaseLine.Text;
        }
        private void MasterOutputDatabaseLineLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        /*
        private void MasterOutputFileGroupLineGotFocus(object sender, RoutedEventArgs eTcea)
        {
            StatusLine1.Text += "Enter the name of the Database where the MasterOutput file exists." + "\n";
            omPd.MasterOutputFileGroupLine.Text = omPd.OutputGroupFileLine.Text;
        }
        private void MasterOutputFileGroupLineLostFocus(object sender, RoutedEventArgs eTcea)
        {
            // StatusLine1.Text = "";
        }
         */
        private void MasterOutputFileLineGotFocus(object sender, RoutedEventArgs e) {
            StatusLine1.Text += "Enter the file name to place the data into." + "\n";
            // omPd.DbMasterOutputFileLine.Text = omPm.DbMasterOutputFileLine.Text;
            DbMasterOutputFileLine.Text = OutputFileLine.Text;
        }
        private void MasterOutputFileLineLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        #endregion
        #region User Focus
        private void UserNameLineGotFocus(object sender, RoutedEventArgs e) {
            // UserNameLine.Text = "Guest";
            // StatusLine1.Text = "";
        }
        private void UserNameLineLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void UserPasswordLineGotFocus(object sender, RoutedEventArgs e) {
            // UserPasswordLine.Text = "";
            // StatusLine1.Text = "";
        }
        private void UserPasswordLineLostFocus(object sender, RoutedEventArgs e) {
            // UserPasswordRequiredOption.Content = false;
            // StatusLine1.Text = "";
        }
        private void UserPasswordRequiredOptionGotFocus(object sender, RoutedEventArgs e) {
            DbUserPasswordRequiredOption.Content = false;
            // StatusLine1.Text = "";
        }
        private void UserPasswordRequiredOptionLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void UserPasswordRequiredOptionChecked(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void UserPasswordRequiredOptionChecked() {
            // StatusLine1.Text = "";
        }
        #endregion
        #region Security Master Server
        private void SecurityMasterOutputSystemLineGotFocus(object sender, RoutedEventArgs e) {
            StatusLine1.Text += "Enter the name of the System where your Output Database resides." + "\n";
            // SecurityMasterOutputSystemLine.Text = OutputSystemLine.Text;
        }
        private void SecurityMasterOutputSystemLineLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        private void SecurityMasterOutputDatabaseLineGotFocus(object sender, RoutedEventArgs e) {
            StatusLine1.Text += "Enter the name of the Database where the output file exists." + "\n";
            // SecurityMasterOutputDatabaseLine.Text = OutputDatabaseLine.Text;
            DbSecurityMasterOutputDatabaseLine.Text = OutputDatabaseLine.Text;
        }
        private void SecurityMasterOutputDatabaseLineLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        /*
        private void SecurityMasterOutputFileGroupLineGotFocus(object sender, RoutedEventArgs eTcea)
        {
            StatusLine1.Text += "Enter the name of the Database where the SecurityMasterOutput file exists." + "\n";
            SecurityMasterOutputFileGroupLine.Text = OutputGroupFileLine.Text;
        }
        private void SecurityMasterOutputFileGroupLineLostFocus(object sender, RoutedEventArgs eTcea)
        {
            // StatusLine1.Text = "";
        }
         */
        private void SecurityMasterOutputFileLineGotFocus(object sender, RoutedEventArgs e) {
            StatusLine1.Text += "Enter the file name to place the data into." + "\n";
            // SecurityMasterOutputFileLine.Text = SecurityMasterOutputFileLine.Text;
            DbSecurityMasterOutputFileLine.Text = OutputFileLine.Text;
        }
        private void SecurityMasterOutputFileLineLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        #endregion
        ///  End of Focus Control
        #endregion
        #region Page Event Handling (OK Button)
        private void OkButtonPressedGotFocus(object sender, RoutedEventArgs e) {
            StatusLine1.Text += "Click to use these settings." + "\n";
        }

        private void OkButtonPressedLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        #endregion
        #region Page Event Handling (Cancel Button)
        private void CancelButtonPressedGotFocus(object sender, RoutedEventArgs e) {
            StatusLine1.Text += "Click to cancel processing and exit." + "\n";
        }
        private void CancelButtonPressedLostFocus(object sender, RoutedEventArgs e) {
            // StatusLine1.Text = "";
        }
        #endregion
        #region Copied Buttons from example
        void OkButtonClick(object sender, RoutedEventArgs e) {
            // Accept when Ok button is clicked

            // Page function is accepted, so return a eArmhResult
            OnReturn(new ReturnEventArgs<string>(this.PageFunctionResultsTextBlock.Text));
        }

        void CancelButtonClick(object sender, RoutedEventArgs e) {
            // Cancel 
            // OnReturn(null);
            // Page function is cancelled, so don't return a eArmhResult
            OnReturn(new ReturnEventArgs<string>(null));
        }
        #endregion
        #region Application Logic
        private void ConfirmFileImportOk(object sender, RoutedEventArgs e) {
            //  Hey Hey Davie Boy... Let's START the File Import*/
            // StatusLineMdmText2 = "";
            StatusLineMdmText1 += "Ok of Output Settings for InputTldProcesFile" + "\n";
            StatusLine1.Text += "Please wait..." + "\n";
            //
            iLocalStartOk = 0;
            // StatusLineMdmText1 = "";
            // StatusLineMdmText2 = "";
            // StatusLineMdmText3 = "";
            // Page function is accepted, so return a eArmhResult
            omCo.OutputFile.FileSummary.SystemName = OutputSystemLine.Text;
            omCo.OutputFile.FileSummary.DatabaseName = OutputDatabaseLine.Text;
            omCo.OutputFile.FileSummary.FileGroupName = OutputFileGroupLine.Text;
            omCo.OutputFile.FileSummary.FileName = OutputFileLine.Text;

            omCo.OutputFile.FileSummary.MasterSystemLine = DbMasterOutputSystemLine.Text;
            omCo.OutputFile.FileSummary.MasterDatabaseLine = DbMasterOutputDatabaseLine.Text;
            omCo.OutputFile.FileSummary.MasterFileLine = DbMasterOutputFileLine.Text;

            omCo.OutputFile.FileSummary.UserNameLine = DbUserNameLine.Text;
            omCo.OutputFile.FileSummary.UserPasswordLine = DbUserPasswordLine.Text;
            omCo.OutputFile.FileSummary.UserPasswordRequiredOption = (bool)DbUserPasswordRequiredOption.IsChecked;

            omCo.OutputFile.FileSummary.SecurityMasterSystemLine = DbSecurityMasterOutputSystemLine.Text;
            omCo.OutputFile.FileSummary.SecurityMasterDatabaseLine = DbSecurityMasterOutputDatabaseLine.Text;
            omCo.OutputFile.FileSummary.SecurityMasterFileLine = DbSecurityMasterOutputFileLine.Text;

            OnReturn(new ReturnEventArgs<string>("OK"));
            // NavigationService.GoBack();
        }

        private void CancelFileImport(object sender, RoutedEventArgs e) {
            // Boo.... Let's Cancel the File Import
            // StatusLineMdmText2 = "";
            StatusLineMdmText1 += "Cancel InputTldProcesFile" + "\n";
            // StatusLine1.Text = "";
            // Are you sure?
            // AnswerYes:
            StatusLineMdmText2 += "Cancel InputTldProcesFile" + "\n";
            // Page function is cancelled, so don't return a eArmhResult
            OnReturn(new ReturnEventArgs<string>("Cancel"));
            // StatusLine1.Text += "Import Process Cancelled" + "\n";
            // LocalLongResult = omCo.AppCancelProcessing();
            // AnswerNo:
            // StatusLineMdmText1 += "Cancel InputTldProcesFile AnswerNo" + "\n";
        }

        #endregion
        #region Status Messages

        private void MessageSet_11(int PassedIntResult) {
            StatusLineMdmText1 = "";
            switch (PassedIntResult) {
                case 99999:
                    StatusLineMdmText1 += "Null start";
                    break;
                case 99:
                    StatusLineMdmText1 += "File must have a value";
                    break;
                case 11:
                    StatusLineMdmText1 += "File not found";
                    break;
                case 12:
                    StatusLineMdmText1 += "File already exists";
                    break;
                case 21:
                    StatusLineMdmText1 += "Item Id not found";
                    break;
                case 22:
                    StatusLineMdmText1 += "Item Id already exists";
                    break;
                default:
                    StatusLineMdmText1 += "Unknown error" + " (" + PassedIntResult + ")";
                    break;
            }

        }

        private void MessageSet_12(int PassedIntAction, int PassedIntResult, string PassedStringText, string PassedStringPrefix, string PassedStringSuffex) {
            StatusLineMdmText3 = "";
            if (PassedStringPrefix.Length > 0) {
                StatusLineMdmText3 = PassedStringPrefix;
            }

            StatusLineMdmText3 += PassedStringText + PassedStringSuffex;
            if (PassedIntResult != 0) {
                StatusLineMdmText3 += " (" + PassedIntResult + ")";
            }

            switch (PassedIntAction) {
                case 99999:
                    StatusLineMdmText2.Insert(1, "Null start");
                    break;
                case 1:
                    if (StatusLineMdmText2.Length > 0) {
                        StatusLineMdmText2 += "\n";
                    }
                    StatusLineMdmText2 += StatusLineMdmText3;
                    break;
                case 2:
                    if (StatusLineMdmText2.Length > 0) {
                        StatusLineMdmText2 = StatusLineMdmText3 + "\n" + StatusLineMdmText2;
                    } else {
                        StatusLineMdmText2 = StatusLineMdmText3;
                    }
                    break;
                case 3:
                    StatusLineMdmText2 = StatusLineMdmText3;
                    break;
                default:
                    if (StatusLineMdmText2.Length > 0) {
                        StatusLineMdmText2 += "\n";
                    }
                    StatusLineMdmText2 += StatusLineMdmText3;
                    break;
            }
            StatusLine2.Text += StatusLineMdmText2 + "\n";
        }

        #endregion
        #region Output Validaton

        private long OutputSystemLineValidation() {
            OutputSystemCurrentNotValidResult = (long)Mdm.Oss.Decl.DefStdBase.MethodStateIs.Start;
            StatusLineMdmText1 += "OutputSystemLineValidation" + "\n";
            if (OutputSystemLine.Text == "") {
                // File Item Id Empty
                // This is usually OK
                // Check Options
                OutputSystemCurrentNotValidResult = 99;
            } else {
                if (OutputSystemCurrent != OutputSystemLine.Text) {
                    /// changed
                    StatusLineMdmText1 += "Input File Changed to: " + OutputSystemLine.Text;
                    StatusLine1.Text += "Checking " + OutputSystemLine.Text + "\n";
                    /// do check
                    OutputSystemCurrentNotValidResult = omCo.OutputSystemCheck(OutputSystemLine.Text);
                    if (OutputSystemCurrentNotValidResult == 0) {
                        /// set current value
                        OutputSystemCurrent = OutputSystemLine.Text;
                    }
                }
            }
            return OutputSystemCurrentNotValidResult;
        }

        private long OutputDatabaseLineValidation() {
            StatusLineMdmText1 += "OutputDatabaseLineValidation" + "\n";
            if (OutputDatabaseLine.Text == "") {
                // File Item Id Empty
                // This is usually OK
                // Check Options
                OutputDatabaseCurrentNotValid = 99;
            } else {
                if (OutputDatabaseCurrent != OutputDatabaseLine.Text) {
                    /// changed
                    StatusLineMdmText1 += "Input File Changed to: " + OutputDatabaseLine.Text;
                    StatusLine1.Text += "Checking " + OutputDatabaseLine.Text + "\n";
                    /// do check
                    OutputDatabaseCurrentNotValid = omCo.OutputDatabaseCheck(OutputDatabaseLine.Text);
                    if (OutputDatabaseCurrentNotValid == 0) {
                        /// set current value
                        OutputDatabaseCurrent = OutputDatabaseLine.Text;
                    }
                }
            }
            return OutputDatabaseCurrentNotValid;
        }

        private long OutputFileLineValidation() {
            // Validation
            StatusLineMdmText1 += "OutputFileLineValidation" + "\n";
            //string tmp;
            //string tmp1;
            //tmp = OutputFileLine.Text;
            //@tmp1 = OutputFileLine.Text;
            if (OutputFileLine.Text == "") {
                // File Item Id Empty
                // This is usually OK
                // Check Options
                OutputFileCurrentNotValid = 99;
            } else {
                if (OutputFileCurrent != OutputFileLine.Text) {
                    /// changed           
                    StatusLineMdmText1 += "Output File Changed to: " + OutputFileLine.Text;
                    StatusLine1.Text += "Checking " + OutputFileLine.Text + "\n";
                    /// do check
                    OutputFileCurrentNotValid = omCo.OutputFileCheck(OutputFileLine.Text);
                    if (OutputFileCurrentNotValid == 0) {
                        /// set current value
                        OutputFileCurrent = OutputFileLine.@Text;
                    }
                }
            }
            return OutputFileCurrentNotValid;
        }


        #endregion
        #region External Events

        private void OutputDatabaseLineExternalChanged(object sender, TextChangedEventArgs eTcea) {
            StatusLine1.Text += "External input file name change coming in, please wait..." + "\n";
        }

        private void OutputFileLineExternalChanged(object sender, TextChangedEventArgs eTcea) {
            StatusLine1.Text += "External output file name change coming in, please wait..." + "\n";
            // * Trace and Log
        }

        private void ConsoleCommandTextTextChanged(object sender, TextChangedEventArgs eTcea) {

        }
        #endregion

    }

}