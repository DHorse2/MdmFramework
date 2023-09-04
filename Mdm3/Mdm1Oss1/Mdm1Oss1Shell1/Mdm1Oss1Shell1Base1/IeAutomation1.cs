using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
// using System.Windows.Interop;
using System.Xml.Linq;
using System.Text;
using SHDocVw;
using Mdm.Oss.WinUtil;

namespace Mdm.Oss.IeUtil
{
    class IeAutomation1
    {
        // Mdm1Oss1Ie1
        // I have resolved the issue by using the following class which I have got from the internet
        // public class IEAutomation
        // {
        
        #region Private Members
        private string url;
        private bool launched;
        // private InternetExplorer iexplorer;
        //These are class data members
        static private SHDocVw.InternetExplorer iexplorer = null;
        static private IWebBrowserApp m_WebBrowser = null;
        static private int m_nCounter = 0;
        // static private Win32ShellDef Win32Shell = new Win32ShellDef(); 
        #endregion 

        public static int GoUrl(string lcUrl)
        {
            string lcTPath = Path.GetTempPath();
            IntPtr lnpResult = Win32ShellDef.ShellExecute((IntPtr) null, "OPEN", lcUrl, "", lcTPath, 1);
            int lnResult = (int) lnpResult;
            return lnResult;
        }

        #region Private Methods
        /// <summary>
        /// Starts new instance of IE if not started.
        /// </summary>
        /// 

        private void ieStart()
        {
            try
            {
                if (!launched)
                {
                    iexplorer = new InternetExplorerClass();
                    iexplorer.OnQuit += new DWebBrowserEvents2_OnQuitEventHandler(iexplorer_OnQuit);
                    launched = true;
                }
            }
            catch (Exception ex)
            {
                Trace.Write("Exception in getting ieStart " + ex.Message + " " + ex.StackTrace);
            }
        }

        /// <summary>
        /// This event is called when user closes IE window.
        /// </summary>

        void iexplorer_OnQuit()
        {
            IEQuit();
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// This method opens the web url in IE.
        /// </summary>
        /// <param name="Visible" type="bool">
        /// <para>
        /// true if IE should be visible.
        /// </para>
        /// </param>
        /// <param name="Url" type="string">
        /// <para>
        /// URL of web page.
        /// </para>
        /// </param>

        public void IENavigate(bool Visible, string Url)
        {
            try
            {
                ieStart();
                object o = null;
                iexplorer.Navigate(Url, ref o, ref o, ref o, ref o);
                iexplorer.Visible = Visible;
                url = Url;
            }
            catch (Exception ex)
            {
                Trace.Write("Exception in getting IENavigate " + ex.Message + " " + ex.StackTrace);
            }
        }
        /// <summary>
        /// This method is called when the IE needs to quit to release the objects.
        /// </summary>

        public void IEQuit()
        {
            try
            {
                iexplorer.Stop();
                iexplorer.Quit();
            }
            catch (Exception ex)
            {
                Trace.Write("Exception in getting IEQuit " + ex.Message + " " + ex.StackTrace);
            }
            finally
            {
                launched = false;
            }
        }

        #endregion 

    }
}

    /*

        // Then in the method or menu event from 
        // which you have to launch IE just write 
        // the following code
    public VOID temp()
    {
        if (iExplorer == null)
        {
            iExplorer = new IeAutomation1();

            iExplorer.IENavigate(false, webSiteName + "/login.aspx?flg=1&name=" + defaultpagename + "&userid=" + userId+ "&RoleID=" + roleID);

            iExplorer.IENavigate(true, webSiteName + "/" + webpagename + ".aspx?flg=1&name=" + webpagename + "&userid=" + userId+ "&RoleID=" + roleID.ToString());

            // In the login web page of web application 
            // I have written following code in page_load event
            FormsAuthentication.RedirectFromLoginPage(userId, false)
        }
    }

*/