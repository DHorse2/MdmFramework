using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;

namespace Mdm.Oss.WinUtil.Url {
    /// <summary>
    /// Some Win32Api Url and Ie.
    /// </summary>
    public class Win32UrlDef {
        /// <summary>
        /// Used by CannonializeURL method.
        /// </summary>
        [Flags]
        public enum shlwapi_URL : uint {
            /// <summary>
            /// Treat "/./" and "/../" in a URL string as literal characters, not as shorthand for navigation. 
            /// </summary>
            URL_DONT_SIMPLIFY = 0x08000000,
            /// <summary>
            /// Convert any occurrence of "%" to its escape sequence.
            /// </summary>
            URL_ESCAPE_PERCENT = 0x00001000,
            /// <summary>
            /// Replace only spaces with escape sequences. This flag takes precedence over URL_ESCAPE_UNSAFE, but does not apply to opaque URLs.
            /// </summary>
            URL_ESCAPE_SPACES_ONLY = 0x04000000,
            /// <summary>
            /// Replace unsafe characters with their escape sequences. Unsafe characters are those characters that may be altered during transport across the Internet, and include the (<, >, ", #, {, }, |, \, ^, ~, [, ], and ') characters. This flag applies to all URLs, including opaque URLs.
            /// </summary>
            URL_ESCAPE_UNSAFE = 0x20000000,
            /// <summary>
            /// Combine URLs with client-defined pluggable protocols, according to the World Wide Web Consortium (W3C) specification. This flag does not apply to standard protocols such as ftp, http, gopher, and so on. If this flag is set, UrlCombine does not simplify URLs, so there is no need to also set URL_DONT_SIMPLIFY.
            /// </summary>
            URL_PLUGGABLE_PROTOCOL = 0x40000000,
            /// <summary>
            /// Un-escape any escape sequences that the URLs contain, with two exceptions. The escape sequences for "?" and "#" are not un-escaped. If one of the URL_ESCAPE_XXX flags is also set, the two URLs are first un-escaped, then combined, then escaped.
            /// </summary>
            URL_UNESCAPE = 0x10000000
        }

        [DllImport("shlwapi.dll")]
        public static extern int UrlCanonicalize(
            string pszUrl,
            StringBuilder pszCanonicalized,
            ref int pcchCanonicalized,
            shlwapi_URL dwFlags
            );


        /// <summary>
        /// Takes a URL string and converts it into canonical form
        /// </summary>
        /// <param name="pszUrl">URL string</param>
        /// <param name="dwFlags">shlwapi_URL Enumeration. Flags that specify how the URL is converted to canonical form.</param>
        /// <returns>The converted URL</returns>
        public static string CannonializeURL(string pszUrl, shlwapi_URL dwFlags) {
            StringBuilder buff = new StringBuilder(260);
            int s = buff.Capacity;
            int c = UrlCanonicalize(pszUrl, buff, ref s, dwFlags);
            if (c == 0)
                return buff.ToString();
            else {
                buff.Capacity = s;
                c = UrlCanonicalize(pszUrl, buff, ref s, dwFlags);
                return buff.ToString();
            }
        }

        /// <summary>
        /// The helper class to sort in ascending order by URL.
        /// </summary>
        public class SortUrlAscendingHelper : IComparer {
            // Compare Url Strings
            int IComparer.Compare(object a, object b) {
                STATURL c1 = (STATURL)a;
                STATURL c2 = (STATURL)b;
                System.String sTmpA = c1.pwcsUrl;
                System.String sTmpB = c2.pwcsUrl;
                return (sTmpA.CompareTo(sTmpB));
            }

            public static IComparer SortUrlAscending() {
                return (IComparer)new SortUrlAscendingHelper();
            }
        }
        /// <summary>
        /// The helper class to sort in ascending order by FileTime(LastVisited).
        /// </summary>
        public class SortFileTimeAscendingHelper : IComparer {
            [DllImport("Kernel32.dll")]
            static extern int CompareFileTime([In] ref System.Runtime.InteropServices.FILETIME lpFileTime1, [In] ref System.Runtime.InteropServices.FILETIME lpFileTime2);

            int IComparer.Compare(object a, object b) {
                STATURL c1 = (STATURL)a;
                STATURL c2 = (STATURL)b;
                return (CompareFileTime(ref c1.ftLastVisited, ref c2.ftLastVisited));
            }

            public static IComparer SortFileTimeAscending() {
                return (IComparer)new SortFileTimeAscendingHelper();
            }
        }
        /// <summary>
        /// The helper class to sort in ascending order by TitleAscending.
        /// </summary>
        public class SortTitleAscendingHelper : IComparer {
            // Compare Title Strings
            int IComparer.Compare(object a, object b) {
                STATURL c1 = (STATURL)a;
                STATURL c2 = (STATURL)b;
                System.String sTmpA = c1.pwcsUrl;
                System.String sTmpB = c2.pwcsUrl;
                return (sTmpA.CompareTo(sTmpB));
            }

            public static IComparer SortTitleAscending() {
                return (IComparer)new SortTitleAscendingHelper();
            }
        }
        /// <summary>
        /// The helper class to sort in ascending order by FileTime(LastVisitedAscending).
        /// </summary>
        public class SortLastVisitedAscendingHelper : IComparer {
            [DllImport("Kernel32.dll")]
            static extern int CompareFileTime([In] ref System.Runtime.InteropServices.FILETIME lpFileTime1, [In] ref System.Runtime.InteropServices.FILETIME lpFileTime2);
            // Compare LastVisited Strings
            int IComparer.Compare(object a, object b) {
                STATURL c1 = (STATURL)a;
                STATURL c2 = (STATURL)b;
                return (CompareFileTime(ref c1.ftLastVisited, ref c2.ftLastVisited));
            }

            public static IComparer SortLastVisitedAscending() {
                return (IComparer)new SortLastVisitedAscendingHelper();
            }
        }
        /// <summary>
        /// The helper class to sort in ascending order by FileTime(LastUpdatedAscending).
        /// </summary>
        public class SortLastUpdatedAscendingHelper : IComparer {
            [DllImport("Kernel32.dll")]
            static extern int CompareFileTime([In] ref System.Runtime.InteropServices.FILETIME lpFileTime1, [In] ref System.Runtime.InteropServices.FILETIME lpFileTime2);
            // Compare LastUpdated Strings
            int IComparer.Compare(object a, object b) {
                STATURL c1 = (STATURL)a;
                STATURL c2 = (STATURL)b;
                return (CompareFileTime(ref c1.ftLastUpdated, ref c2.ftLastUpdated));
            }

            public static IComparer SortLastUpdatedAscending() {
                return (IComparer)new SortLastUpdatedAscendingHelper();
            }
        }
        /// <summary>
        /// The helper class to sort in ascending order by FileTime(ExpiresAscending).
        /// </summary>
        public class SortExpiresAscendingHelper : IComparer {
            [DllImport("Kernel32.dll")]
            static extern int CompareFileTime([In] ref System.Runtime.InteropServices.FILETIME lpFileTime1, [In] ref System.Runtime.InteropServices.FILETIME lpFileTime2);
            // Compare Expires Strings
            int IComparer.Compare(object a, object b) {
                STATURL c1 = (STATURL)a;
                STATURL c2 = (STATURL)b;
                return (CompareFileTime(ref c1.ftExpires, ref c2.ftExpires));
            }

            public static IComparer SortExpiresAscending() {
                return (IComparer)new SortExpiresAscendingHelper();
            }
        }

        /// <summary>
        /// The structure that contains statistics about a URL. 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct STATURL {
            /// <summary>
            /// Struct size
            /// </summary>
            public int cbSize;
            /// <summary>
            /// URL
            /// </summary>                                                                   
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pwcsUrl;
            /// <summary>
            /// Page title
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pwcsTitle;
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
            public string URL {
                get { return pwcsUrl; }
            }
            /// <summary>
            /// sets a column header in the DataGrid control. This property is not needed if you do not use it.
            /// </summary>
            public string Title {
                get {
                    if (pwcsUrl.StartsWith("file:"))
                        return CannonializeURL(pwcsUrl, shlwapi_URL.URL_UNESCAPE).Substring(8).Replace('/', '\\');
                    else
                        return pwcsTitle;
                }
            }
            /// <summary>
            /// sets a column header in the DataGrid control. This property is not needed if you do not use it.
            /// </summary>
            public DateTime LastVisited {
                get {
                    return Mdm.Oss.WinUtil.Win32TimeDef.FileTimeToDateTime(ftLastVisited).ToLocalTime();
                }
            }
            /// <summary>
            /// sets a column header in the DataGrid control. This property is not needed if you do not use it.
            /// </summary>
            public DateTime LastUpdated {
                get {
                    return Mdm.Oss.WinUtil.Win32TimeDef.FileTimeToDateTime(ftLastUpdated).ToLocalTime();
                }
            }
            /// <summary>
            /// sets a column header in the DataGrid control. This property is not needed if you do not use it.
            /// </summary>
            public DateTime Expires {
                get {
                    try {
                        return Mdm.Oss.WinUtil.Win32TimeDef.FileTimeToDateTime(ftExpires).ToLocalTime();
                    } catch (Exception) {
                        return DateTime.Now;
                    }
                }
            }
        }
        /// <summary>
        /// Flag on the dwFlags parameter of the STATURL structure, used by the SetFilter method.
        /// </summary>
        public enum STATURLFLAGS : uint {
            /// <summary>
            /// Flag on the dwFlags parameter of the STATURL structure indicating that the item is in the cache.
            /// </summary>
            STATURLFLAG_ISCACHED = 0x00000001,
            /// <summary>
            /// Flag on the dwFlags parameter of the STATURL structure indicating that the item is a top-level item.
            /// </summary>
            STATURLFLAG_ISTOPLEVEL = 0x00000002,
        }
    }

}
