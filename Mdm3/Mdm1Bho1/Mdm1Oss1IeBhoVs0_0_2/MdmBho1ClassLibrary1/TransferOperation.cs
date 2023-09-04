using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;
using mshtml;
using System.IO;

namespace NxIEHelperNS
{
	struct OperationStage
	{
		public bool folderCreated;
		public int remainingFiles;
		public int remainingSupOperations;

		public OperationStage(bool folderCreated, int remainingFiles, int remainingFolders)
		{
			this.folderCreated = folderCreated;
			this.remainingFiles = remainingFiles;
			this.remainingSupOperations = remainingFolders;
		}
	}

	public class TransferOperation
	{
		#region constants

		private const char sep = '/';
		private const int MAX_PATH = 260;
		private const int MAX_FILE_LENGTH = 1 << 30; 
		#endregion

		#region static

		public static TransferOperation crtOperation;
		public static int crtOpIdx = 0; 
		#endregion

		#region private vars

		private TransferOperation parent;
		private BhoMain parentBhoInstance;
		private IHTMLDocument2 htmlDoc;

		private string[] filesList;
		private string[] foldersList;
		private string serverFolderName;
		private string serverFolderParentPath;
		private OperationStage opStage;
		private int opIdx;
		private string ServerFolderPath
		{
			get
			{
				string separator = (serverFolderParentPath == "" && serverFolderName == "") ? "" : sep.ToString();
				return serverFolderParentPath + separator + serverFolderName;
			}
		} 
		#endregion

		#region constructor

		public TransferOperation(TransferOperation parent, BhoMain bho, string[] filesList, string[] foldersList,
								string serverFolderName, string serverFolderParentPath, IHTMLDocument2 htmlDoc)
		{
			this.parent = parent;
			this.parentBhoInstance = bho;
			this.htmlDoc = htmlDoc;

			this.filesList = filesList;
			this.foldersList = foldersList;
			this.serverFolderName = serverFolderName.TrimEnd(sep);
			this.serverFolderParentPath = serverFolderParentPath;
			
			this.opIdx = crtOpIdx++;
			bool crtFolderExists = (parent == null);
			this.opStage = new OperationStage(crtFolderExists, filesList.Length, foldersList.Length);


			Logger.LogText(string.Format("Op[{2}]: Created new operation with {0} files & {1} folders",
									filesList.Length, foldersList.Length, opIdx));
		}

		#endregion

		#region main functionality
		
		public void ContinueOpertation()
		{ 
			// create folder (if necessary)
			if (!opStage.folderCreated)
			{
				this.CreateDirectory(serverFolderName, serverFolderParentPath);
				return;
			}

			// create files
			if (opStage.remainingFiles > 0)
			{
				opStage.remainingFiles--; // decrement here -> if any errors occur, we go directly to the next file
				this.UploadFile(this.filesList[opStage.remainingFiles] as string, this.ServerFolderPath); 
				return;
			}
			

			// new op for folder
			if (opStage.remainingSupOperations > 0)
			{
				// iterate through subfolders
				opStage.remainingSupOperations--;
				string crtFolder = this.foldersList[ opStage.remainingSupOperations];
				
				string crtFolderName = new DirectoryInfo(crtFolder).Name;
				string[] subFolderFiles = Directory.GetFiles(crtFolder);
				string[] subFolder_Folders = Directory.GetDirectories(crtFolder);
				TransferOperation subOperation = new TransferOperation(this,
																		this.parentBhoInstance,
																		subFolderFiles,
																		subFolder_Folders,
																		crtFolderName,
																		this.ServerFolderPath,
																		this.htmlDoc);
				TransferOperation.crtOperation = subOperation;
				subOperation.ContinueOpertation();
				
				return;
			}
			ContinueWithParentOperation();
		}

		private void ContinueWithParentOperation()
		{ 
			TransferOperation.crtOperation = this.parent;
			if (TransferOperation.crtOperation != null)
				TransferOperation.crtOperation.ContinueOpertation();
			else
			{
				Logger.LogText("End transfer");
				string scriptCode = string.Format("endTransfert()");
				htmlDoc.parentWindow.execScript(scriptCode, "JavaScript");
			}
		}
		
		private void CreateDirectory(string serverFolderName, string serverRelativeParentPath)
		{
			try
			{
				// JS call to create current folder
				string scriptCode = string.Format("fireCreateFolder('{0}', '{1}', 'external.FolderCreateCallback' )",
				//string scriptCode = string.Format("fireCreateFolder('{0}', '{1}', null )",
													serverFolderName, serverRelativeParentPath);
				Logger.LogText("Op[{0}]: before fireCreateFolder", this.opIdx);
				parentBhoInstance.DisplayHtmlStatus("Creating {0}", serverFolderName);

				htmlDoc.parentWindow.execScript(scriptCode, "JavaScript");
				Logger.LogText("Op[{0}]: after fireCreateFolder", this.opIdx);
			}
			catch (Exception ex)
			{
				Logger.LogText("Exception: {0} -> {1}", ex.Message, ex.StackTrace);
			}

		}

		private void UploadFile(string filePath, string serverRelativePath)
		{
			FileStream f = null;
			try
			{
				string fileName = new FileInfo(filePath).Name;

				Logger.LogText("Op[{0}]: UploadFile - reading file", this.opIdx);
				parentBhoInstance.DisplayHtmlStatus( fileName);

				int bytesRead = 0;
				f = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
				byte[] fileBuffer = new byte[2048];
				if (f.Length > MAX_FILE_LENGTH)
					throw new Exception("Maximum permitted file length is " + MAX_FILE_LENGTH + " bytes");

				StringBuilder sb = new StringBuilder((int)f.Length);
				while ((bytesRead = f.Read(fileBuffer, 0, fileBuffer.Length)) > 0)
				{
					sb.Append(Convert.ToBase64String(fileBuffer, 0, bytesRead, Base64FormattingOptions.None));
				}
				f.Close();
				f = null;


				string content = sb.ToString();
				string mimeType = "application/octet-stream";
				//string fileNameNoPath = fileName.Substring(fileName.LastIndexOf(sep) + 1);
				

				string scriptCode = string.Format("fireCreateFile( '{0}', '{1}', '{2}', '{3}', true, 'external.FileTransferCallback')",
													fileName, content, mimeType, serverRelativePath);

				Logger.LogText("Op[{0}]: UploadFile - before fireCreateFile", this.opIdx);
				htmlDoc.parentWindow.execScript(scriptCode, "JavaScript");
				Logger.LogText("Op[{0}]: UploadFile - after fireCreateFile", this.opIdx);
				
			}
			catch (Exception ex)
			{
				Logger.LogText("Exception: {0} -> {1}", ex.Message, ex.StackTrace);
			}
			finally
			{
				if (f != null)
					f.Close();
			}

		}

		
		#endregion

		#region internal callbacks

		internal void FileCallback()
		{
			Logger.LogText("Op[{0}]: FileCallback", this.opIdx);
			ContinueOpertation();
		}

		internal void FolderCallback(string folderId)
		{
			if (folderId != null && folderId != "") // if success
			{
				Logger.LogText("Op[{0}]: succesful FolderCallback", this.opIdx);
				this.opStage.folderCreated = true;
				this.serverFolderName = folderId;
				ContinueOpertation();
			}
			else // else abandon current folder and go on with its siblings
			{
				Logger.LogText("Op[{0}]: failed FolderCallback", this.opIdx);
				ContinueWithParentOperation();
			}

		} 
		#endregion
	}


}
 