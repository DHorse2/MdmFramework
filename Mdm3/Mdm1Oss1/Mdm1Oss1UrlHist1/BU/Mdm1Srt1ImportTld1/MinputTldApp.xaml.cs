using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

//using Mdm.Oss.ClipboardUtil;
using Mdm.Oss.CodeUtil;
using Mdm.Oss.Support;
using Mdm.Oss.Mapp;
using Mdm.Oss.Mobj;
// using    Mdm.Oss.FileUtil;
// using    Mdm1Oss1FileCreation1;


namespace Mdm.Srt.InputTld
{
    /// <summary>
    /// Interaction logic for MinputTldApp.xaml
    /// </summary>
    /// 

    public partial class MinputTldApp : Application
    {

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e) {
            // Session Ending Code
            base.OnSessionEnding(e);
        }


        protected override void OnLoadCompleted(NavigationEventArgs e) {
            // Load completed code here
            base.OnLoadCompleted(e);
        }

        protected override void OnNavigationProgress(NavigationProgressEventArgs e) {
            // Progress Code here
            base.OnNavigationProgress(e);
        }

    }
}
