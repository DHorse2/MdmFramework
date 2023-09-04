using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using Mdm.Oss.ShellUtil;
using Mdm.Oss.ShellUtil.BaseUtil;
using Mdm.Oss.ShellUtil.FileUtil;

namespace Mdm.Oss.UrlUtil.Hist
{
	/// <summary>
	/// The class that wraps the C# equivalent of the IURLHistory Interface (in the file "urlhist.cs")
	/// </summary>
	public class UrlHistoryWrapperClass
	{
		
		UrlHistoryClass urlHistory;
        IUrlHistoryStg2 objIUrlHistoryStg2;

        /// <summary>
		/// Default constructor for UrlHistoryWrapperClass
		/// </summary>
		public UrlHistoryWrapperClass()
		{
			urlHistory = new UrlHistoryClass();
			objIUrlHistoryStg2 = (IUrlHistoryStg2) urlHistory;
        }
		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		public void Dispose()
		{
			Marshal.ReleaseComObject(objIUrlHistoryStg2);
            urlHistory = null;
        }

		/// <summary>
		/// Places the specified URL into the history. If the URL does not exist in the history, an entry is created in the history. If the URL does exist in the history, it is overwritten.
		/// </summary>
		/// <param name="pocsUrl">the String of the URL to place in the history</param>
		/// <param name="pocsTitle">the String of the title associated with that URL</param>
		/// <param name="dwFlags">the flag which indicate where a URL is placed in the history.
		/// <example><c>ADDURL_FLAG.ADDURL_ADDTOHISTORYANDCACHE</c></example>
		/// </param>
		public void AddHistoryEntry(String pocsUrl, String pocsTitle, ADDURL_FLAG dwFlags)
		{	
			objIUrlHistoryStg2.AddUrl(pocsUrl, pocsTitle, dwFlags);
		}
		
		/// <summary>
		/// Deletes all instances of the specified URL from the history. does not work!
		/// </summary>
		/// <param name="pocsUrl">the String of the URL to delete.</param>
		/// <param name="dwFlags"><c>dwFlags = 0</c></param>
		public void DeleteHistoryEntry(String pocsUrl, int dwFlags) 
		{
			try
			{
				objIUrlHistoryStg2.DeleteUrl(pocsUrl, dwFlags);
			}
			catch(Exception)
			{
				
			}
			

		}


		/// <summary>
		///Queries the history and reports whether the URL passed as the pocsUrl parameter has been visited by the current user. 
		/// </summary>
		/// <param name="pocsUrl">the String of the URL to querythe String of the URL to query.</param>
		/// <param name="dwFlags">STATURL_QUERYFLAGS Enumeration
		/// <example><c>STATURL_QUERYFLAGS.STATURL_QUERYFLAG_TOPLEVEL</c></example></param>
		/// <returns>Returns STATURL structure that received additional URL history information. If the returned  STATURL's pwcsUrl is not null, Queried URL has been visited by the current user.
		/// </returns>
		public STATURL QueryUrl(String pocsUrl , STATURL_QUERYFLAGS dwFlags)
		{
			STATURL lpSTATURL = new STATURL();
			try
			{
				//In this case, queried URL has been visited by the current user.
				objIUrlHistoryStg2.QueryUrl(pocsUrl, dwFlags, ref lpSTATURL);
				//lpSTATURL.pwcsUrl is NOT null;
				return lpSTATURL;
			}
			catch(FileNotFoundException)
			{
				//Queried URL has not been visited by the current user.
				//lpSTATURL.pwcsUrl is set to null;
				return lpSTATURL;
			}
		}
		
		/// <summary>
		/// Delete all the history except today's history, and Temporary Internet Files.
		/// </summary>
		public void ClearHistory()
		{
			objIUrlHistoryStg2.ClearHistory();
		}
	
		/// <summary>
		/// Create an enumerator that can iterate through the history cache. UrlHistoryWrapperClass does not implement IEnumerable interface 
		/// </summary>
		/// <returns>Returns STATURLEnumerator Object that can iterate through the history cache.</returns>
		public STATURLEnumerator GetEnumerator2()
		{
			return new STATURLEnumerator((IEnumSTATURL)objIUrlHistoryStg2.EnumUrls);
            // no objIUrlHistoryStg1.
		}

        /// <summary>
		/// The inner class that can iterate through the history cache. STATURLEnumerator does not implement IEnumerator interface.
		/// The items in the history cache changes often, and enumerator needs to reflect the data as it existed at a specific point in time.
		/// </summary>
		public class STATURLEnumerator
		{
			IEnumSTATURL enumrator;
			int index;
			STATURL staturl;
			
			/// <summary>
			/// Constructor for <c>STATURLEnumerator</c> that accepts IEnumSTATURL Object that represents the <c>IEnumSTATURL</c> COM Interface.
			/// </summary>
			/// <param name="enumrator">the <c>IEnumSTATURL</c> COM Interface</param>
			public STATURLEnumerator(IEnumSTATURL ePassedEnumrator)
			{
                enumrator = ePassedEnumrator;
			}
			//Advances the enumerator to the next item of the url history cache.
			/// <summary>
			/// Advances the enumerator to the next item of the url history cache.
			/// </summary>
			/// <returns>true if the enumerator was successfully advanced to the next element;
			///  false if the enumerator has passed the end of the url history cache.
			///  </returns>
			public bool MoveNext()
			{
                int iTmpCelt = 2;
                int tmp = enumrator.Next(iTmpCelt , ref staturl, out index);
                if (index == 0)
					return false;
				else
					return true;
			}

			/// <summary>
			/// Gets the current item in the url history cache.
			/// </summary>
			public STATURL Current
			{
				get
				{
					return staturl;
				}
			}
			
			/// <summary>
			/// Skips a specified number of Call objects in the enumeration sequence. does not work!
			/// </summary>
			/// <param name="celt"></param>
			public void Skip(int celt)
			{
				int tmp = enumrator.Skip(celt);
			}
			/// <summary>
			/// Resets the enumerator interface so that it begins enumerating at the beginning of the history. 
			/// </summary>
			public void Reset()
			{
				enumrator.Reset();
			}
			
			/// <summary>
			/// Creates a duplicate enumerator containing the same enumeration state as the current one. does not work!
			/// </summary>
			/// <returns>duplicate STATURLEnumerator object</returns>
			public STATURLEnumerator Clone()
			{
				IEnumSTATURL ppenum;
				enumrator.Clone(out ppenum);
				return new STATURLEnumerator(ppenum);

			}
			/// <summary>
			/// Define filter for enumeration. MoveNext() compares the specified URL with each URL in the history lPassedList to find matches. MoveNext() then copies the lPassedList of matches to a buffer. SetFilter method is used to specify the URL to compare.	 
			/// </summary>
			/// <param name="poszFilter">The String of the filter. 
			/// <example>SetFilter('http://', STATURL_QUERYFLAGS.STATURL_QUERYFLAG_TOPLEVEL)  retrieves only entries starting with 'http.//'. </example>
			/// </param>
			/// <param name="dwFlags">STATURL_QUERYFLAGS Enumeration<exapmle><c>STATURL_QUERYFLAGS.STATURL_QUERYFLAG_TOPLEVEL</c></exapmle></param>
			public void SetFilter(String  poszFilter, STATURLFLAGS dwFlags)
			{
				int tmp = enumrator.SetFilter(poszFilter, dwFlags);
			}
			/// <summary>
			///Enumerate the items in the history cache and store them in the IList object.
			/// </summary>
			/// <param name="lPassedList">IList object
			/// <example><c>ArrayList</c>object</example>
			/// </param>
			public void GetUrlHistory(IList lPassedList)
			{
                lPassedList.Clear();
				while(true)
				{
					staturl = new STATURL();
					enumrator.Next(1, ref staturl, out index);	
					if(index == 0)
						break;
					lPassedList.Add(staturl);
				
				}
				enumrator.Reset();
			
			}

		}

	}

}
