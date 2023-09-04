using System;
using System.Runtime.InteropServices;
using System.Collections;
using Mdm.Oss.ShellUtil;
using Mdm.Oss.ShellUtil.BaseUtil;
using Mdm.Oss.ShellUtil.FileUtil;

namespace Mdm.Oss.UrlUtil.Hist
{
	/// <summary>
    /// <para>Used by QueryUrl method to control queries.</para>
	/// </summary>
	public enum STATURL_QUERYFLAGS : uint
	{
		/// <summary>
		/// The specified URL is in the content cache.
		/// </summary>
		STATURL_QUERYFLAG_ISCACHED = 0x00010000,
		/// <summary>
		/// Space for the URL is not allocated when querying for STATURL.
		/// </summary>
		STATURL_QUERYFLAG_NOURL    = 0x00020000,
		/// <summary>
		/// Space for the Web page's title is not allocated when querying for STATURL.
		/// </summary>
		STATURL_QUERYFLAG_NOTITLE  = 0x00040000,
		/// <summary>
		/// //The item is a top-level item.
		/// </summary>
		STATURL_QUERYFLAG_TOPLEVEL = 0x00080000,

	}
	/// <summary>
    /// <para>Flag on the dwFlags parameter of the STATURL structure, used by the SetFilter method.</para>
	/// </summary>
    // moved to Mdm.Oss.Shell.File
    /*
	public enum STATURLFLAGS : uint
	{
		/// <summary>
		/// Flag on the dwFlags parameter of the STATURL structure indicating that the item is in the cache.
		/// </summary>
		STATURLFLAG_ISCACHED   = 0x00000001,
		/// <summary>
		/// Flag on the dwFlags parameter of the STATURL structure indicating that the item is a top-level item.
		/// </summary>
		STATURLFLAG_ISTOPLEVEL = 0x00000002,
	}
    */
	/// <summary>
    /// <para>Used by the AddHistoryEntry method.</para>
	/// </summary>
	public enum ADDURL_FLAG : uint
	{
		/// <summary>
		/// Write to both the visited links and the dated containers. 
		/// </summary>
		ADDURL_ADDTOHISTORYANDCACHE = 0,
		/// <summary>
		/// Write to only the visited links container.
		/// </summary>
		ADDURL_ADDTOCACHE = 1
	}


	/// <summary>
    /// <para>The structure that contains statistics about a URL. </para>
	/// </summary>
    /*
	[StructLayout(LayoutKind.Sequential)]
	public struct STATURL 
	{
		/// <summary>
		/// Struct size
		/// </summary>
		public int Size;
        /// <summary>
        /// URL
        /// </summary>                                                                   
		[MarshalAs(UnmanagedType.LPWStr)] public String pwcsUrl;
		/// <summary>
		/// Page title
		/// </summary>
		[MarshalAs(UnmanagedType.LPWStr)] public String pwcsTitle;
		/// <summary>
		/// Last visited date (UTC)
		/// </summary>
		public System.Runtime.InteropServices.FILETIME ftLastVisited;
		/// <summary>
		/// Last updated date (UTC)
		/// </summary>
		public System.Runtime.InteropServices.FILETIME ftLastUpdated;
		/// <summary>
		/// The expiry date of the Web page's content (UTC)
		/// </summary>
		public System.Runtime.InteropServices.FILETIME ftExpires;
		/// <summary>
		/// Flags. STATURLFLAGS Enumaration.
		/// </summary>
		public STATURLFLAGS dwFlags;								
		
		/// <summary>
		/// sets a column header in the DataGrid control. This property is not needed if you do not use it.
		/// </summary>
		public String URL
		{
			get{return pwcsUrl;}
		}
		/// <summary>
		/// sets a column header in the DataGrid control. This property is not needed if you do not use it.
		/// </summary>
		public String Title
		{
			get{
				if(pwcsUrl.StartsWith("file:"))
					return  Win32api.CannonializeURL(pwcsUrl, Win32api.shlwapi_URL.URL_UNESCAPE).Substring(8).Replace('/', '\\');
				else
					return pwcsTitle;
			}
		}
		/// <summary>
		/// sets a column header in the DataGrid control. This property is not needed if you do not use it.
		/// </summary>
		public DateTime LastVisited
		{
			get
			{
				return Win32api.FileTimeToDateTime(ftLastVisited).ToLocalTime();
			}
		}
		/// <summary>
		/// sets a column header in the DataGrid control. This property is not needed if you do not use it.
		/// </summary>
		public DateTime LastUpdated
		{
			get
			{
				return Win32api.FileTimeToDateTime(ftLastUpdated).ToLocalTime();
			}
		}
		/// <summary>
		/// sets a column header in the DataGrid control. This property is not needed if you do not use it.
		/// </summary>
		public DateTime Expires
		{
			get
			{
				try
				{
					return Win32api.FileTimeToDateTime(ftExpires).ToLocalTime();
				}
				catch(Exception)
				{
					return DateTime.Now;
				}
			}
		}

	} 
    */

    /// <summary>
    /// <para> Provides a 4 part standard structure for UUID's.</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
	public struct UUID
	{
		public int Data1;
		public short Data2;
		public short Data3;
		public byte[] Data4;
	}

    /// <summary>
    /// <para>Enumerates the cached URLs</para>
    /// </summary>
	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("3C374A42-BAE4-11CF-BF7D-00AA006946EE")]
	public interface IEnumSTATURL 
	{
		// void Next(int celt, ref STATURL rgelt, out int pceltFetched);	//Returns the next \"celt\" URLS from the cache
        int Next(int celt, ref STATURL rgelt, out int pceltFetched);	//Returns the next \"celt\" URLS from the cache
		// void Skip(int celt);	//Skips the next \"celt\" URLS from the cache. doed not work.
        int Skip(int celt);	//Skips the next \"celt\" URLS from the cache. doed not work.
		void Reset();	//Resets the enumeration
		void Clone(out IEnumSTATURL ppenum);	//Clones this object
		// void SetFilter([MarshalAs(UnmanagedType.LPWStr)] String  poszFilter, STATURLFLAGS dwFlags);	//Sets the enumeration filter
        int SetFilter([MarshalAs(UnmanagedType.LPWStr)] String poszFilter, STATURLFLAGS dwFlags);	//Sets the enumeration filter
			
	}

    /*
    int Next(
    [In, MarshalAs(UnmanagedType.U4)] int celt,
    [Out, MarshalAs(UnmanagedType.LPStruct)] out STATURL rgelt,
    [Out, MarshalAs(UnmanagedType.U4)] out int pceltFetched
    );
23456789101112131415161718192021222324252627282930
 using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace IfacesEnumsStructsClasses
{
    #region IEnumSTATURL Interface
    [ComImport, ComVisible(true)]
    [Guid("3C374A42-BAE4-11CF-BF7D-00AA006946EE")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumSTATURL
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Next(
            [In, MarshalAs(UnmanagedType.U4)] int celt,
            [Out, MarshalAs(UnmanagedType.LPStruct)] out STATURL rgelt,
            [Out, MarshalAs(UnmanagedType.U4)] out int pceltFetched);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Skip([In, MarshalAs(UnmanagedType.U4)] int celt);
        void Reset();
        void Clone(out IEnumSTATURL ppenum);
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        int SetFilter([In, MarshalAs(UnmanagedType.LPWStr)] String poszFilter, uint dwFlags);
    }
    #endregion
}
 
    */

    /// <summary>
    /// <para> Add the IE Url History COM functions</para>
    /// </summary>
    [ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("3C374A41-BAE4-11CF-BF7D-00AA006946EE")]
	public interface IUrlHistoryStg
	{
		void AddUrl(String pocsUrl, String pocsTitle, ADDURL_FLAG dwFlags);	//Adds a new history entry
		void DeleteUrl(String pocsUrl, int dwFlags);	//Deletes an entry by its URL. does not work!
		void QueryUrl ([MarshalAs(UnmanagedType.LPWStr)] String pocsUrl , STATURL_QUERYFLAGS dwFlags , ref STATURL lpSTATURL );	//Returns a STATURL for a given URL
		void BindToObject ([In] String pocsUrl, [In] UUID riid, IntPtr ppvOut); //Binds to an object. does not work!
		object EnumUrls{[return: MarshalAs(UnmanagedType.IUnknown)] get;}	//Returns an enumerator for URLs
		

	}

    /// <summary>
    /// <para> Add the IE Url History Stage 2 COM functions</para>
    /// </summary>
    [ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("AFA0DC11-C313-11D0-831A-00C04FD5AE38")]
	public interface IUrlHistoryStg2 : IUrlHistoryStg
	{
		new void AddUrl(String pocsUrl, String pocsTitle, ADDURL_FLAG dwFlags);	//Adds a new history entry
		new void DeleteUrl(String pocsUrl, int dwFlags);	//Deletes an entry by its URL. does not work!
		new void QueryUrl ([MarshalAs(UnmanagedType.LPWStr)] String pocsUrl , STATURL_QUERYFLAGS dwFlags , ref STATURL lpSTATURL );	//Returns a STATURL for a given URL
		new void BindToObject ([In] String pocsUrl, [In] UUID riid, IntPtr ppvOut);	//Binds to an object. does not work!
		new Object EnumUrls{[return: MarshalAs(UnmanagedType.IUnknown)] get;}	//Returns an enumerator for URLs

		void AddUrlAndNotify(String pocsUrl, String pocsTitle, int dwFlags, int fWriteHistory, Object poctNotify, Object punkISFolder);//does not work!
		void ClearHistory();	//Removes all history items


	}

    /// <summary>
    /// <para>Url History Class</para>
    /// <para>Currently not implemented.</para>
    /// </summary>
    //
	[ComImport]
	[Guid("3C374A40-BAE4-11CF-BF7D-00AA006946EE")]
	public class UrlHistoryClass
	{	
	}


}
