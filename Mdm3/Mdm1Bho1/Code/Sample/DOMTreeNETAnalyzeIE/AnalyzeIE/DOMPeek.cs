using System;
using System.Collections;
using System.Runtime.InteropServices;
using Microsoft.Win32;


namespace AnalyzeIE.NET
{
	/// <summary>
	/// CDOMPeek - Browser Helper Object class.
	/// </summary>
	[ Guid("C06B3B91-769A-42d5-8BCD-CF70F8589FBA"), ClassInterface(ClassInterfaceType.None)]
	public class DOMPeek: IDOMPeek, IObjectWithSite
	{
		private const string classguid = "{C06B3B91-769A-42d5-8BCD-CF70F8589FBA}";

		/// <summary>
		/// This bool has nothing to do with DialogDisplay property of each instance
		/// but serves as the class (module) level flag set by the user via IE Extension
		/// object. By default we want dialogs with DOM tree to be displayed. 
		/// </summary>
		private static bool m_bShowDialog = true;
		private static LoadSpy m_LoadSpy = new LoadSpy();
		/// <summary>
		/// list that will keep intances of this class 
		/// </summary>
		public static ArrayList m_Instances = new ArrayList();

		private object m_IUnkSite;
		private SHDocVw.IWebBrowser2 m_IWebBrowser2;
		private SHDocVw.DWebBrowserEvents2_SinkHelper m_SinkHelper;
		private UCOMIConnectionPoint m_CP;
		private MSHTML.IHTMLDocument2 m_IDoc2;

		private CMainDlg m_DocDlg;

		[DllImport("user32")]
		static extern int SetParent( int hWndChild, int hWndNewParent);

		/// <summary>
		/// IDOMPeek dual interface implemented as a property 
		/// </summary>
		bool IDOMPeek.DialogDisplay
		{
			get
			{
				return IsDialogDisplayed();
			}

			set
			{
				SetDialogDisplayed(value);
			}
		}

		void IObjectWithSite.SetSite( object pUnkSite)
		{
			// per SetSite spec, we should release old interface
			// before storing a new one.
			if ( m_IUnkSite != null)
				Marshal.ReleaseComObject( m_IUnkSite);
			m_IUnkSite = pUnkSite; 

			bool bOk = true;
			try
			{
				m_IWebBrowser2 = (SHDocVw.IWebBrowser2)m_IUnkSite;
			}
			catch // cast or no such interface exception
			{
				bOk = false;
			}

			// instead of implementing DWebBrowserEvents2 sink, try create existing
			// sink helper and reuse it.
			if ( bOk)
			{
				Type type = typeof(SHDocVw.DWebBrowserEvents2_SinkHelper);
				try
				{
					// sink helper constructor is not public
					m_SinkHelper = (SHDocVw.DWebBrowserEvents2_SinkHelper)Activator.CreateInstance(type); 
				}
				catch // cast or no such interface exception
				{
					bOk = false;
				}
			}

			if ( bOk)
			{
				try
				{
					UCOMIConnectionPointContainer cpCont = (UCOMIConnectionPointContainer)m_IWebBrowser2;
					if (  cpCont != null)
					{
						Guid guid = typeof(SHDocVw.DWebBrowserEvents2).GUID;
						m_SinkHelper.m_dwCookie = 696;
						cpCont.FindConnectionPoint( ref guid , out m_CP);
					}
				}
				catch
				{
					bOk = false;
				}
			}

			if (bOk)
			{
				// connection point was found. register our sink helper for advise
				// and set up delegates for two events that we are interested in
				m_SinkHelper.m_DocumentCompleteDelegate
					= new SHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(OnDocumentComplete);
				m_SinkHelper.m_OnQuitDelegate
					= new SHDocVw.DWebBrowserEvents2_OnQuitEventHandler(OnQuit);
				m_CP.Advise( m_SinkHelper, out m_SinkHelper.m_dwCookie);
				m_Instances.Add( this);
			} 
		}

		void IObjectWithSite.GetSite( ref Guid riid, IntPtr ppvSite)
		{
			const int e_fail = unchecked((int)0x80004005);

			if ( !ppvSite.Equals((IntPtr)0))
			{
				IntPtr pvSite = (IntPtr)0;
				// be a good COM interface imp - NULL the destination ptr first
				Marshal.WriteIntPtr( ppvSite, pvSite);

				if ( m_IUnkSite != null)
				{
					IntPtr pUnk = Marshal.GetIUnknownForObject( m_IUnkSite);
					Marshal.QueryInterface( pUnk, ref riid, out pvSite);
					Marshal.Release(pUnk); // GetIUnknownForObject AddRefs so Release

					if ( !pvSite.Equals((IntPtr)0))
						Marshal.WriteIntPtr( ppvSite, pvSite);
					else
						Marshal.ThrowExceptionForHR(e_fail);
				}
			}
			else
				Marshal.ThrowExceptionForHR(e_fail);
		}

		void OnQuit()
		{
			m_CP.Unadvise( m_SinkHelper.m_dwCookie);
			m_Instances.Remove( this);
			SetDialogDisplayed( false);
		}

		void OnDocumentComplete( object pDispatch, ref object VariantURL)
		{
			bool bTopFrame = m_IWebBrowser2.Equals( pDispatch);
			if ( bTopFrame)
			{
				try
				{
					m_IDoc2 = (MSHTML.IHTMLDocument2)m_IWebBrowser2.Document;
					if ( m_bShowDialog) // true if IE Extension button not toggled to hide dialog
					{
						if ( !IsDialogDisplayed())
							SetDialogDisplayed( true);
						else
							m_DocDlg.UpdateDOM();
					}
				}
				catch // not a HTML document
				{
					SetDialogDisplayed( false);
				}
			}
		}

		public bool IsDialogDisplayed()
		{
			bool bRet = m_DocDlg != null && !m_DocDlg.IsDisposed;
			return bRet;
		}

		void SetDialogDisplayed( bool bDisplay) 
		{
			bool bIs = IsDialogDisplayed();
			if ( !bDisplay)
			{
				if ( bIs)
				{
					m_DocDlg.Close();
					m_DocDlg = null;
				}
			}
			else if ( !bIs)
				StartNewDialog();
		}

		bool StartNewDialog()
		{
			bool bOk = true;

			if ( m_IDoc2 == null) // document download has not completed yet
			{
				try
				{
					m_IDoc2 = (MSHTML.IHTMLDocument2)m_IWebBrowser2.Document;
				}
				catch // Invalid cast or no-such-interface-supported exception
				{
					bOk = false; // not a HTML document
				}
			}

			// Check if document is complete - this is required since StartNewDialog can be
			// invoked when user clicks IE Extension button and there are no guarantees 
			// that document download will be complete at that time.

			if ( bOk)
			{
				string state = m_IDoc2.readyState;
				bOk = string.Compare( state, "complete", true) == 0;
			}

			if ( bOk)
			{
				// create and plug our dialog into browser window
				m_DocDlg = new CMainDlg( m_IDoc2);
				bOk = m_DocDlg != null && m_DocDlg.Handle != IntPtr.Zero && m_DocDlg.Handle != (IntPtr)0;
				if ( bOk)
				{
					int parenthwnd = m_IWebBrowser2.HWND;
					SetParent( m_DocDlg.Handle.ToInt32(), parenthwnd);
					m_DocDlg.Show();
				}
			}
			return bOk;
		}

		/// <summary>
		/// Called by CmdDispatch IE Extension when user clicks Extension button or menu item
		/// </summary>
		/// <returns>new value of boolean flag</returns>
		public static bool ToggleDialogShow()
		{
			m_bShowDialog = !m_bShowDialog;
			IDOMPeek instance;
			for ( int i = 0; i < m_Instances.Count; i++)
			{
				instance = (IDOMPeek)m_Instances[i];
				if ( instance != null)
					instance.DialogDisplay = m_bShowDialog;
			}
			return m_bShowDialog;
		}

		/// <summary>
		/// Register our class as Browser Helper Object.
		/// Extra Registry editing is done because we do not want mscoree.dll
		/// (set by RegAsm by default) as our inproc server proxy.
		/// </summary>
		[ComRegisterFunctionAttribute()]
		static void RegisterServer(String str1)
		{
			// find out assembly location and turn it into the path to wrapmscoree.dll
			// which is assumed to be installed in the same folder as this assembly
			System.Reflection.Assembly thisAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			string path = thisAssembly.Location;
			int lastSep = path.LastIndexOf( '/');
			path = path.Substring( 0, lastSep + 1);
			path = path.Replace("/", "\\") + "wrapmscoree.dll";
			// str1 contains "HKEY_CLASSES_ROOT\CLSID\<clsid>". Remove "HKEY_CLASSES_ROOT\"
			str1 = str1.Substring( str1.IndexOf('\\') + 1) + "\\InprocServer32";
			
			try
			{
				RegistryKey root = Registry.LocalMachine;
				RegistryKey rk;
				rk = root.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Browser Helper Objects", true);
				if ( rk != null)
				{
					RegistryKey rk1 = rk.CreateSubKey(classguid);
					rk1.Close();
					rk.Close();
				}
				root = Registry.ClassesRoot;
				rk = root.OpenSubKey( str1, true);
				if ( rk != null)
				{
					// default value of InprocServer32 now points to our wrapmscoree.dll
					rk.SetValue("", path);
					rk.Close();
				}
			}
			catch
			{
			}
		}

		[ComUnregisterFunctionAttribute()]
		static void UnregisterServer(String str1)
		{
			try
			{
				RegistryKey root = Registry.LocalMachine;
				RegistryKey rk;
				rk = root.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Browser Helper Objects", true);
				rk.DeleteSubKey(classguid);
				rk.Close();
			}
			catch
			{
			}
		}
	}

	/// <summary>
	///  a little helper class to look at assemblies loaded by this one
	/// </summary>
	class LoadSpy
	{
		static AppDomain thisDomain = AppDomain.CurrentDomain;

		public LoadSpy()
		{
			thisDomain.AssemblyLoad += new AssemblyLoadEventHandler( this.LoadEventHandler);
		}
		/// <summary>
		/// handler called when some assembly gets loaded
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args">look at args in debugger to see which assemblies get loaded</param>
		void LoadEventHandler( object sender, AssemblyLoadEventArgs args)
		{
		}
	}
}