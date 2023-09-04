using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using SHDocVw;
using mshtml;
using COMInterop = System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Configuration;

namespace NxIEHelperNS
{
	[ComVisible(true),
	ClassInterface(ClassInterfaceType.None),
	Guid("8E61D540-4EC6-4915-8BE6-1F86DA78F6E5")]
	public class BhoMain : IObjectWithSite, IDocHostUIHandler, NxIEHelperNS.IDropTarget
	{
		#region constants

		private const int MAX_PATH = 260;
		private const int MAX_FILE_LENGTH = 1 << 30;

		private const int Ok = 0;
		private const int Error = 1;
		private const string pluginActivatorTag = "Nx5PluginEnabled";
		private const string pluginStatusElement = "nx5firefoxhelperStatus";

		#endregion

		#region properties
		
		public bool pluginEnabled = false;
		public bool PluginEnabled
		{
			get
			{
				// static check -> requires enable OnDocumentComplete event
				//return pluginEnabled;

				// dinamic check
				return IsPluginEnabled();
			}
		}

		private bool IsPluginEnabled()
		{
			bool retval = false;
			try
			{
				if (myBrowser != null)
				{
					IHTMLDocument3 document = myBrowser.Document as IHTMLDocument3;
					IHTMLElement pluginSwitchElem = document.getElementById(pluginActivatorTag);
					retval = (pluginSwitchElem != null /*&& (string)pluginSwitchElem.getAttribute("value") == "true" */ );
					if (retval)
						Logger.ShowForm();
				}
			}
			catch (Exception ex)
			{
				Logger.LogText("Exception: {0} -> {1}", ex.Message, ex.StackTrace);
			}
			return retval;
		}

		#endregion
		
		#region private vars
		
		private IWebBrowser2 myBrowser;
		private IDocHostUIHandler defaultDocHandler;
		private IDropTarget defaultDropTarget;
		#endregion

		#region BHO registration

		private static string BhoRegistryKey
		{
			get
			{
				string parentKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Browser Helper Objects\";
				Attribute guidAttr = Attribute.GetCustomAttribute(typeof(BhoMain), typeof(GuidAttribute));
				string guidValue = "{" + (guidAttr as GuidAttribute).Value + "}";
				return parentKey + guidValue;
			}
		}
		[ComRegisterFunction]
		public static void RegisterBHO(Type t)
		{
			Registry.LocalMachine.CreateSubKey(BhoRegistryKey);
		}

		[ComUnregisterFunction]
		public static void UnregisterBHO(Type t)
		{
			Registry.LocalMachine.DeleteSubKey(BhoRegistryKey);
		}

		#endregion

		#region constructor

		public BhoMain()
		{
			myBrowser = null;
			defaultDocHandler = null;
			defaultDropTarget = null;
		} 
		#endregion

		#region IObjectWithSite Members

		public void SetSite(object pUnkSite)
		{
			try
			{
				if (pUnkSite == null)
				{
					myBrowser = null;
				}
				else
				{
					myBrowser = pUnkSite as IWebBrowser2;
					DWebBrowserEvents2_Event ev = myBrowser as DWebBrowserEvents2_Event;
					//ev.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(ev_DocumentComplete);
					ev.StatusTextChange += new DWebBrowserEvents2_StatusTextChangeEventHandler(ev_StatusTextChange);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void GetSite(ref Guid riid, out object ppvSite)
		{
			ppvSite = null;
		}

		#endregion

		#region browser event handlers

		void ev_StatusTextChange(string Text)
		{
			try
			{
				IHTMLDocument2 doc = myBrowser.Document as IHTMLDocument2;
				if (doc != null)
				{
					IOleObject oleObj = doc as IOleObject;
					if (oleObj != null)
					{
						ICustomDoc customDoc = doc as ICustomDoc;
						IOleClientSite clientSite = null;
						oleObj.GetClientSite(ref clientSite);
						if (customDoc != null && clientSite != null)
						{
							defaultDocHandler = clientSite as IDocHostUIHandler;
							customDoc.SetUIHandler(this);
						}
					}
				}
			}
			catch
			{
				defaultDocHandler = null;
				defaultDropTarget = null;
			}
		}

		void ev_DocumentComplete(object pDisp, ref object URL)
		{
			pluginEnabled = IsPluginEnabled();
			//try 
			//{				
			//    IHTMLDocument3 document = myBrowser.Document as IHTMLDocument3;
			//    IHTMLElement pluginSwitchElem = document.getElementById(pluginActivatorTag);
			//    if (pluginSwitchElem != null)
			//        pluginEnabled= true;
			//} 
			//catch (Exception ex)
			//{
			//}
		}

		#endregion

		#region IDropTarget implementation

		public void DragEnter(COMInterop.IDataObject pDataObj, System.Int32 grfKeyState, System.Int32 ptX, System.Int32 ptY, ref DROPEFFECTS pdwEffect)
		{
			Logger.LogText("DragEnter");
			if (defaultDropTarget != null)
				defaultDropTarget.DragEnter(pDataObj, grfKeyState, ptX, ptY, ref pdwEffect);
		}

		public void DragLeave()
		{
			Logger.LogText("DragLeave");
			if (defaultDropTarget != null)
				defaultDropTarget.DragLeave();
		}
		
		public void DragOver(System.Int32 grfKeyState, System.Int32 ptX, System.Int32 ptY, ref DROPEFFECTS pdwEffect)
		{
			Logger.LogText("DragOver");
			if (defaultDropTarget != null)
				defaultDropTarget.DragOver(grfKeyState, ptX, ptY, ref pdwEffect);
		}

		public void Drop(COMInterop.IDataObject pDataObj, System.Int32 grfKeyState, System.Int32 ptX, System.Int32 ptY, ref DROPEFFECTS pdwEffect)
		{
			Logger.LogText("DragEnter");
			if (!PluginEnabled)
			{
				Logger.LogText("Plugin NOT enabled");
				if (defaultDropTarget != null)
					defaultDropTarget.Drop(pDataObj, grfKeyState, ptX, ptY, ref pdwEffect);
				return;
			}

			try
			{
				COMInterop.STGMEDIUM td = new COMInterop.STGMEDIUM();
				td.tymed = COMInterop.TYMED.TYMED_HGLOBAL;
				COMInterop.FORMATETC fr = new COMInterop.FORMATETC();
				fr.cfFormat = 15;
				fr.ptd = IntPtr.Zero;
				fr.dwAspect = COMInterop.DVASPECT.DVASPECT_CONTENT;
				fr.lindex = -1;
				fr.tymed = COMInterop.TYMED.TYMED_HGLOBAL;
				pDataObj.GetData(ref fr, out td);

				ArrayList filesList = new ArrayList();
				ArrayList folderList = new ArrayList();


				uint filesCount = Win32Api.DragQueryFile((uint)td.unionmember.ToInt32(), 0xFFFFFFFF, null, 0);
				if (filesCount > 0)
				{
					int totalFilesCount = 0;
					IHTMLDocument2 htmlDocument = myBrowser.Document as IHTMLDocument2;
					for (uint i = 0; i < filesCount; i++)
					{
						StringBuilder sb = new StringBuilder(MAX_PATH);
						Win32Api.DragQueryFile((uint)td.unionmember.ToInt32(), i, sb, MAX_PATH);
						string fileOrFolderName = sb.ToString();

						if (Directory.Exists(fileOrFolderName))
						{
							folderList.Add(fileOrFolderName);
							totalFilesCount += ComputeFolders_fileCount(fileOrFolderName);
						}
						else if (File.Exists(fileOrFolderName))
						{
							filesList.Add(fileOrFolderName);
							totalFilesCount++;
						}
					}

					Logger.LogText("Initiate transfer");
					string scriptCode = string.Format("initTransfert({0},null)", totalFilesCount);
					htmlDocument.parentWindow.execScript(scriptCode, "JavaScript");
					

					TransferOperation mainOperation = new TransferOperation(null,
																			this,
																			(string[])filesList.ToArray( typeof(string)),
																			(string[])folderList.ToArray(typeof(string)),
																			"",
																			"",
																			htmlDocument);
					TransferOperation.crtOperation = mainOperation;
					mainOperation.ContinueOpertation();

					//CComBSTR redirectUrl;
					//redirectUrl.AppendBSTR(m_url.bstrVal);
					//redirectUrl.AppendBSTR(CComBSTR("?portal_status_message=psm_file(s)_uploaded") );
					//redirectUrl.AppendBSTR(m_location);
					//m_spWebBrowser2->Navigate(redirectUrl,&vtEmpty,&vtEmpty,&vtEmpty,&vtEmpty);
				}
				if (defaultDropTarget != null)
					defaultDropTarget.DragLeave();
			}
			catch (Exception ex)
			{
				Logger.LogText("Exception: {0} -> {1}", ex.Message, ex.StackTrace);
			}
		}

		
		#endregion
	
		#region IDocHostUIHandler Members

		uint  IDocHostUIHandler.ShowContextMenu(uint dwID, ref tagPOINT ppt, object pcmdtReserved, object pdispReserved)
		{
			if (defaultDocHandler!=null)
				return defaultDocHandler.ShowContextMenu( dwID, ref ppt, pcmdtReserved, pdispReserved);
 			throw new Exception("The method or operation is not implemented.");
		}

		void  IDocHostUIHandler.GetHostInfo(ref DOCHOSTUIINFO pInfo)
		{
			
			if (defaultDocHandler!=null)
				defaultDocHandler.GetHostInfo( ref pInfo);
			pInfo.dwFlags = pInfo.dwFlags & (~(uint)DOCHOSTUIFLAG.DOCHOSTUIFLAG_DIALOG);
			//throw new Exception("The method or operation is not implemented.");
		}

		void  IDocHostUIHandler.ShowUI(uint dwID, ref object pActiveObject, ref object pCommandTarget, ref object pFrame, ref object pDoc)
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.ShowUI( dwID, ref pActiveObject, ref pCommandTarget, ref pFrame, ref pDoc);
			//throw new Exception("The method or operation is not implemented.");
		}

		void  IDocHostUIHandler.HideUI()
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.HideUI();
			//throw new Exception("The method or operation is not implemented.");
		}

		void  IDocHostUIHandler.UpdateUI()
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.UpdateUI();
			//throw new Exception("The method or operation is not implemented.");
		}

		void  IDocHostUIHandler.EnableModeless(int fEnable)
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.EnableModeless(fEnable);
			//throw new Exception("The method or operation is not implemented.");
		}

		void  IDocHostUIHandler.OnDocWindowActivate(int fActivate)
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.OnDocWindowActivate(fActivate);
			//throw new Exception("The method or operation is not implemented.");
		}

		void  IDocHostUIHandler.OnFrameWindowActivate(int fActivate)
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.OnFrameWindowActivate(fActivate);
			//throw new Exception("The method or operation is not implemented.");
		}

		void  IDocHostUIHandler.ResizeBorder(ref tagRECT prcBorder, int pUIWindow, int fRameWindow)
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.ResizeBorder(ref prcBorder, pUIWindow, fRameWindow);
			//throw new Exception("The method or operation is not implemented.");
		}

		uint  IDocHostUIHandler.TranslateAccelerator(ref tagMSG lpMsg, ref Guid pguidCmdGroup, uint nCmdID)
		{
			if (defaultDocHandler!=null)
				return defaultDocHandler.TranslateAccelerator(ref lpMsg, ref pguidCmdGroup, nCmdID);
			throw new Exception("The method or operation is not implemented.");
		}

		void  IDocHostUIHandler.GetOptionKeyPath(ref string pchKey, uint dw)
		{
			if (defaultDocHandler!=null)
				defaultDocHandler.GetOptionKeyPath(ref pchKey, dw);
			//throw new Exception("The method or operation is not implemented.");
		}

		int IDocHostUIHandler.GetDropTarget(IDropTarget pDropTarget, out IDropTarget ppDropTarget)
		{
			if (defaultDocHandler != null)
			{
				defaultDocHandler.GetDropTarget(pDropTarget, out ppDropTarget);
				defaultDropTarget = ppDropTarget as IDropTarget;
			}
			ppDropTarget = this;
			return Ok;
		}

		void IDocHostUIHandler.GetExternal([MarshalAs(UnmanagedType.IDispatch)] out object ppDispatch)
		{
			if (PluginEnabled)
			{
				ppDispatch = new CustomMethods();
			}
			else
				if (defaultDocHandler != null)
					defaultDocHandler.GetExternal(out ppDispatch);
				else
					ppDispatch = null;

		}

		uint  IDocHostUIHandler.TranslateUrl(uint dwTranslate, string pchURLIn, ref string ppchURLOut)
		{
			if (defaultDocHandler!=null)
				return defaultDocHandler.TranslateUrl(dwTranslate, pchURLIn, ref ppchURLOut);
			throw new Exception("The method or operation is not implemented.");
		}

		IDataObject  IDocHostUIHandler.FilterDataObject(IDataObject pDO)
		{
			if (defaultDocHandler!=null)
				return defaultDocHandler.FilterDataObject(pDO);
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion

		#region private func
		
		private int ComputeFolders_fileCount(string folderPath)
		{
			int fileCount = 0;
			foreach (string subFolderPath in Directory.GetDirectories(folderPath))
				fileCount += ComputeFolders_fileCount(subFolderPath);
			return fileCount + Directory.GetFiles(folderPath).Length;
		} 
		#endregion

		public void DisplayHtmlStatus(string statusText, params object[] parameters)
		{
			try
			{
				if (!ConfigManager.DisableHtmlStatus)
				{
					IHTMLDocument3 doc = myBrowser.Document as IHTMLDocument3;
					doc.getElementById("nx5firefoxhelperStatus").setAttribute("label", string.Format(statusText, parameters), 0 /*case insensitive*/);
				}
			}
			catch{}
		}
	}

	public class CustomMethods : IJSCallback
	{
		#region ICustomMethods Members

		public void LogText([MarshalAs(UnmanagedType.BStr)] string textToLog)
		{
			Logger.LogText("JScript: {0}", textToLog);
		}

		public void FileTransferCallback()
		{
			TransferOperation.crtOperation.FileCallback();
		}

		public void FolderCreateCallback(object folderId)
		{
			TransferOperation.crtOperation.FolderCallback(folderId as string);
		}

		#endregion
	}
}
