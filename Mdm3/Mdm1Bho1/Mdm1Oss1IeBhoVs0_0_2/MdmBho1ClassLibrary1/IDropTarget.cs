using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace NxIEHelperNS
{

	public enum DATADIR
	{
		DATADIR_GET = 1,

		DATADIR_SET = 2,

	}
	
	
	//public enum DVASPECT
	//{
	//    DVASPECT_CONTENT = 1,

	//    DVASPECT_THUMBNAIL = 2,

	//    DVASPECT_ICON = 4,

	//    DVASPECT_DOCPRINT = 8,

	//    DVASPECT_OPAQUE = 16,

	//    DVASPECT_TRANSPARENT = 32,

	//}
	//public enum TYMED
	//{
	//    TYMED_HGLOBAL = 1,

	//    TYMED_FILE = 2,

	//    TYMED_ISTREAM = 4,

	//    TYMED_ISTORAGE = 8,

	//    TYMED_GDI = 16,

	//    TYMED_MFPICT = 32,

	//    TYMED_ENHMF = 64,

	//    TYMED_NULL = 0,

	//}

	//[StructLayoutAttribute(LayoutKind.Sequential, Pack = 4, Size = 0)]
	//public struct STGMEDIUM
	//{
	//    public TYMED TYMED;

	//    public int data;

	//    public int pUnkForRelease;

	//}

	//[StructLayoutAttribute(LayoutKind.Sequential, Pack = 4, Size = 0)]
	//public struct FORMATETC
	//{
	//    public short cfFormat;

	//    public int pDVTARGETDEVICE;

	//    public DVASPECT dwAspect;

	//    public int lindex;

	//    public TYMED TYMED;

	//}


	public enum OLECONTF
	{
		OLECONTF_EMBEDDINGS = 1,
		OLECONTF_LINKS = 2,
		OLECONTF_OTHERS = 4,
		OLECONTF_ONLYUSER = 8,
		OLECONTF_ONLYIFRUNNING = 16,
	}


	public enum DROPEFFECTS
	{
		DROPEFFECT_NONE = 0,

		DROPEFFECT_COPY = 1,

		DROPEFFECT_MOVE = 2,

		DROPEFFECT_LINK = 4,

		DROPEFFECT_SCROLL = int.MinValue,

	}

	/*
	[InterfaceTypeAttribute(1)]
	[GuidAttribute("0000010E-0000-0000-C000-000000000046")]
	[ComImportAttribute()]
	public interface IDataObject
	{

		int GetData(ref FORMATETC pformatetcIn, ref STGMEDIUM pmedium);

		void GetDataHere(ref FORMATETC pformatetc, ref STGMEDIUM pmedium);

		int QueryGetData(ref FORMATETC pformatetc);

		void GetCanonicalFormatEtc(ref FORMATETC pformatectIn, ref FORMATETC pformatetcOut);

		void SetData(ref FORMATETC pformatetc, ref STGMEDIUM pmedium, [ComAliasNameAttribute("olelib.BOOL")] int fRelease);

		[return: MarshalAs(UnmanagedType.Interface)]
		System.Runtime.InteropServices.ComTypes.IEnumFORMATETC EnumFormatEtc(DATADIR dwDirection);

		void DAdvise(ref FORMATETC pformatetc, int ADVF, int pAdvSink, ref int pdwConnection);

		void DUnadvise(int dwConnection);

		void EnumDAdvise(ref int pIEnumAdvise);
	}
	*/

	[GuidAttribute("00000122-0000-0000-C000-000000000046")]
	[InterfaceTypeAttribute(1)]
	[ComImportAttribute()]
	public interface IDropTarget
	{

		void DragEnter(IDataObject pDataObj, int grfKeyState, int ptX, int ptY, ref DROPEFFECTS pdwEffect);

		void DragOver(int grfKeyState, int ptX, int ptY, ref DROPEFFECTS pdwEffect);

		void DragLeave();

		void Drop( IDataObject pDataObj, int grfKeyState, int ptX, int ptY, ref DROPEFFECTS pdwEffect);
	}


}