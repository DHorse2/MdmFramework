
using System;
using System.Runtime.InteropServices;
using System.Collections; 
namespace Obsolete
{    
         public enum STATURL_QUERYFLAGS : uint
    {
        STATURL_QUERYFLAG_ISCACHED = 0x00010000,
        STATURL_QUERYFLAG_NOURL = 0x00020000,
        STATURL_QUERYFLAG_NOTITLE = 0x00040000,
        STATURL_QUERYFLAG_TOPLEVEL = 0x00080000,
    }    
         public enum STATURLFLAGS : uint
    {        
                  STATURLFLAG_ISCACHED = 0x00000001,
        STATURLFLAG_ISTOPLEVEL = 0x00000002,
    }     

          //Contains statistics about a URL
    [StructLayout(LayoutKind.Sequential)]
    public struct STATURL
     {
        public int cbSize;
        [MarshalAs(UnmanagedType.LPWStr)] public string pwcsUrl;
        [MarshalAs(UnmanagedType.LPWStr)] public string pwcsTitle;
        public FILETIME ftLastVisited;
        public FILETIME ftLastUpdated;
        public FILETIME ftExpires;
        public STATURLFLAGS dwFlags;
    }     

[StructLayout(LayoutKind.Sequential)]
    public struct UUID
    {
        public int Data1;
        public short Data2;
        public short Data3;
        public byte[] Data4;
    }     

        //Enumerates the cached URLs
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("3C374A42-BAE4-11CF-BF7D-00AA006946EE")]
    public interface IEnumSTATURL
     {
        void Next(int celt, ref STATURL rgelt, out int pceltFetched);
        void Skip(int celt);
        void Reset();
        void Clone(out IEnumSTATURL ppenum);
        void SetFilter([MarshalAs(UnmanagedType.LPWStr)] string poszFilter, STATURLFLAGS dwFlags);
    }
     [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("3C374A41-BAE4-11CF-BF7D-00AA006946EE")]
    public interface IUrlHistoryStg
    {
                  void AddUrl(string pocsUrl, string pocsTitle, STATURLFLAGS dwFlags);
        void DeleteUrl(string pocsUrl, int dwFlags);
        void QueryUrl ([MarshalAs(UnmanagedType.LPWStr)] string pocsUrl , int dwFlags , ref STATURL lpSTATURL );
        void BindToObject ([In] string pocsUrl, [In] UUID riid, IntPtr ppvOut);
        object EnumUrls{[return: MarshalAs(UnmanagedType.IUnknown)] get;}
    }     

          [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("AFA0DC11-C313-11D0-831A-00C04FD5AE38")]
    public interface IUrlHistoryStg2 : IUrlHistoryStg
    {        
                  new void AddUrl(string pocsUrl, string pocsTitle, STATURLFLAGS dwFlags);
        new void DeleteUrl(string pocsUrl, int dwFlags);
        new void QueryUrl ([MarshalAs(UnmanagedType.LPWStr)] string pocsUrl , int dwFlags , ref STATURL lpSTATURL );
        new void BindToObject ([In] string pocsUrl, [In] UUID riid, IntPtr ppvOut);
        new object EnumUrls{[return: MarshalAs(UnmanagedType.IUnknown)] get;}
        void AddUrlAndNotify(string pocsUrl, string pocsTitle, int dwFlags, int fWriteHistory, object poctNotify, object punkISFolder);
        void ClearHistory();
    }     
    
         //UrlHistory class    
         [ComImport]
    [Guid("3C374A40-BAE4-11CF-BF7D-00AA006946EE")]
    public class UrlHistoryClass
    {
    }
}