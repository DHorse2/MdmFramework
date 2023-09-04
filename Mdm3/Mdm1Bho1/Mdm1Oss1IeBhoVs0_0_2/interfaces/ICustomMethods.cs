using System;
using System.Runtime.InteropServices;
using stdole;

namespace NxIEHelperNS
{
	/// <summary>
	/// By implementing this class, a WebBrowser controlling host component can expose
	/// any number of methods to HTML pages. From script on a page, you can call one
	/// of the ICustomMethods like this:
	/// 
  //  <SCRIPT language="JScript">
  //  function MyFunc(iSomeData)
  //  {
  //    external.ShowMyDialogBox();
  //  }
  //  </SCRIPT>
  ///
  /// arguments may also be passed to custom methods, using strings, ints, objects or other.
	/// </summary>
	[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface IJSCallback 
	{
		void LogText([MarshalAs(UnmanagedType.BStr)] string textToLog);
		void FileTransferCallback();
		void FolderCreateCallback(object folderId);
	}
}
