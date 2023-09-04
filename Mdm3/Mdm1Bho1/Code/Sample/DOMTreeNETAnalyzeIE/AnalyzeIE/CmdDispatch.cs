using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace AnalyzeIE.NET
{
	public enum OLECMDF: uint
	{
		OLECMDF_SUPPORTED = 1,
		OLECMDF_ENABLED	= 2,
		OLECMDF_LATCHED	= 4,
		OLECMDF_NINCHED	= 8
	}

	public enum OLECMDTEXTF: uint
	{
		OLECMDTEXTF_NONE = 0,
		OLECMDTEXTF_NAME = 1,
		OLECMDTEXTF_STATUS = 2
	}

	public enum OLECMDEXECOPT: uint
	{
		OLECMDEXECOPT_DODEFAULT	= 0,
		OLECMDEXECOPT_PROMPTUSER = 1,
		OLECMDEXECOPT_DONTPROMPTUSER = 2,
		OLECMDEXECOPT_SHOWHELP = 3
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct OLECMD
	{
		public uint cmdID;
		public OLECMDF cmdf;
	}

	/// <summary>
	/// Parameter pCmdtext to QueryStatus method but currently unused
	/// since IE passes NULL pointer
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct OLECMDTEXT
	{
		public OLECMDTEXTF cmdtextf;
		public uint cwActual;
		public uint cwBuf;
		public char[] rgwz;
}
	/// <summary>
	/// IE Extension object CmdDispatch.
	/// </summary>
	[Guid("6F431AC3-364A-478b-BBDB-89C7CE1B18F6"), ClassInterface(ClassInterfaceType.None)]	
	public class CmdDispatch: IOleCommandTarget
	{
		private const string classguid = "{6F431AC3-364A-478b-BBDB-89C7CE1B18F6}";

		public void QueryStatus( Guid pguidCmdGroup, uint cCmds, ref OLECMD prgCmds, 
			IntPtr pCmdText)
		{
			// I expected to be able to marshal prgCmds as conformant array via:
			// [ In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] OLECMD[] prgCmds
			// but COM interop didn't like it. Since we have only one command, reference will work
			prgCmds.cmdf = OLECMDF.OLECMDF_SUPPORTED | OLECMDF.OLECMDF_ENABLED; 
		}

		public void Exec( Guid pguidCmdGroup, uint nCmdID, OLECMDEXECOPT nCmdexecopt,
			IntPtr pvaIn, IntPtr pvaOut)
		{
			if ( nCmdID == 0 && nCmdexecopt != OLECMDEXECOPT.OLECMDEXECOPT_SHOWHELP)
			{
				DOMPeek.ToggleDialogShow();
			}
		}

		/// <summary>
		/// register this class as IE Extension object
		/// </summary>
		[ComRegisterFunctionAttribute()]
		static void RegisterServer(String str1)
		{
			// path is needed for button icons
			Assembly thisAssembly = Assembly.GetExecutingAssembly();
			string path = thisAssembly.Location;
			// remove filename
			int lastSep = path.LastIndexOf( '/');
			path = path.Substring( 0, lastSep + 1);

			try
			{
				RegistryKey root = Registry.LocalMachine;
				RegistryKey rk;
				rk = root.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Extensions", true);
			{
				RegistryKey rk1 = rk.CreateSubKey(classguid);
				rk1.SetValue("Default Visible", "Yes");
				rk1.SetValue("MenuText", "Open/Close .NET DOM dialog");
				rk1.SetValue("MenuStatusBar", "Opens or closes HTML DOM tree view dialog");
				rk1.SetValue("CLSID", "{1FBA04EE-3024-11d2-8F1F-0000F87ABD16}");
				rk1.SetValue("ClsidExtension",classguid);
				rk1.SetValue("ButtonText", "Open/Close .NET DOM dialog");
				rk1.SetValue("HotIcon", path + "iespyhot.ico");
				rk1.SetValue("Icon", path + "iespy.ico");
				rk1.Close();
			}
				rk.Close();
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
				rk = root.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Extensions", true);
				rk.DeleteSubKey(classguid);
				rk.Close();
			}
			catch
			{
			}
		}
	}
}
