using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHDocVw;
using mshtml;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace Mdm1Oss1Bho1 {
    [
    ComVisible(true),
    Guid("8a194578-81ea-4850-9911-13ba2d71efbd"),
    ClassInterface(ClassInterfaceType.None)
    ]
    public class BHO : IObjectWithSite {
        WebBrowser webBrowser;
        HTMLDocument document;
        String sDescString = "";
        public void OnDocumentComplete(object pDisp, ref object URL) {
            document = (HTMLDocument)webBrowser.Document;
            System.Diagnostics.Debugger.Launch();
            foreach (IHTMLInputElement tempElement in document.getElementsByTagName("INPUT")) {
                sDescString = "";
                if (tempElement.name != null) {
                    sDescString += "Name " + tempElement.name + Environment.NewLine;
                    sDescString += "Alt " + tempElement.alt + Environment.NewLine;
                    sDescString += "Type " + tempElement.type + Environment.NewLine;
                    sDescString += "Value " + tempElement.value + Environment.NewLine;
                    sDescString += "Height " + tempElement.height + Environment.NewLine;
                    sDescString += "Width " + tempElement.width + Environment.NewLine;
                    sDescString += "Size " + tempElement.size + Environment.NewLine;
                    sDescString += "DefaultValue " + tempElement.defaultValue + Environment.NewLine;
                    sDescString += "ToString " + tempElement.ToString() + Environment.NewLine;
                    // sDescString += "n " + tempElement. + Environment.NewLine;
                    // sDescString += "n " + tempElement. + Environment.NewLine;

                } else {
                    // tempElement.name != null ? (sDescString += tempElement.name) : 
                       sDescString += "it sucks, no name, try id" + ((IHTMLElement)tempElement).id;
                    // tempElement.name != null ? tempElement.name : "it sucks, no name, try id" + ((IHTMLElement)tempElement).id;
                }
                System.Windows.Forms.MessageBox.Show(sDescString);
            }
        }

        public void OnBeforeNavigate2(object pDisp, ref object URL, ref object Flags, ref object TargetFrameName, ref object PostData, ref object Headers, ref bool Cancel) {
            document = (HTMLDocument)webBrowser.Document;
            foreach (IHTMLInputElement tempElement in document.getElementsByTagName("INPUT")) {
                if (tempElement.type.ToLower() == "password") {
                    System.Windows.Forms.MessageBox.Show(tempElement.value);
                }
            }
        }

        #region BHO Internal Functions
        public static string BHOKEYNAME = "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Browser Helper Objects";
        [ComRegisterFunction]
        public static void RegisterBHO(Type type) {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(BHOKEYNAME, true);
            if (registryKey == null)
                registryKey = Registry.LocalMachine.CreateSubKey(BHOKEYNAME);
            string guid = type.GUID.ToString("B");
            RegistryKey ourKey = registryKey.OpenSubKey(guid);
            if (ourKey == null)
                ourKey = registryKey.CreateSubKey(guid);
            ourKey.SetValue("Alright", 1);
            registryKey.Close();
            ourKey.Close();
        }

        [ComUnregisterFunction]
        public static void UnregisterBHO(Type type) {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(BHOKEYNAME, true);
            string guid = type.GUID.ToString("B");
            if (registryKey != null)
                registryKey.DeleteSubKey(guid, false);
        }

        public int SetSite(object site) {
            if (site != null) {
                webBrowser = (WebBrowser)site;
                webBrowser.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(this.OnDocumentComplete);
                webBrowser.BeforeNavigate2 += new DWebBrowserEvents2_BeforeNavigate2EventHandler(this.OnBeforeNavigate2);
            } else {
                webBrowser.DocumentComplete -= new DWebBrowserEvents2_DocumentCompleteEventHandler(this.OnDocumentComplete);
                webBrowser.BeforeNavigate2 -= new DWebBrowserEvents2_BeforeNavigate2EventHandler(this.OnBeforeNavigate2);
                webBrowser = null;
            }
            return 0;
        }

        public int GetSite(ref Guid guid, out IntPtr ppvSite) {
            IntPtr punk = Marshal.GetIUnknownForObject(webBrowser);
            int hr = Marshal.QueryInterface(punk, ref guid, out ppvSite);
            Marshal.Release(punk);
            return hr;
        }
        #endregion

    }
    public class Class1 {

    }
}
