using System;
using System.Runtime.InteropServices;

namespace AnalyzeIE.NET
{
	/// <summary>
	/// Default interface of DOMPeek class
	/// </summary>
	[Guid("0B6EF17E-18E5-4449-86EA-64C82D596EAE"), InterfaceType(ComInterfaceType.InterfaceIsDual)]
	public interface IDOMPeek
	{
		bool DialogDisplay { get; set; }
	}

	// From OCIDL.H
	//   
	//	MIDL_INTERFACE("FC4801A3-2BA9-11CF-A229-00AA003D7352")
	//	IObjectWithSite : public IUnknown

	//	virtual HRESULT STDMETHODCALLTYPE SetSite( 
	//	/* [in] */ IUnknown __RPC_FAR *pUnkSite) = 0;
	        
	//	virtual HRESULT STDMETHODCALLTYPE GetSite( 
	//	/* [in] */ REFIID riid,
	//	/* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvSite) = 0;

	/// <summary>
	/// IObjectWithSite is not our custom interface by it defined in COM so ComImport attribute
	/// is set and it will not appear in type library generated by RegAsm.
	/// </summary>
	[ComImport, Guid("FC4801A3-2BA9-11CF-A229-00AA003D7352"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)] 
	public interface IObjectWithSite
	{
		void SetSite( [In, MarshalAs(UnmanagedType.IUnknown)] object pUnkSite);
		void GetSite( [In] ref Guid riid, [Out] IntPtr ppvSite);
	}    

	// From DOCOBJ.H
	// MIDL_INTERFACE("b722bccb-4e68-101b-a2bc-00aa00404770")
	//	IOleCommandTarget : public IUnknown

	//	virtual /* [input_sync] */ HRESULT STDMETHODCALLTYPE QueryStatus( 
	//	/* [unique][in] */ const GUID __RPC_FAR *pguidCmdGroup,
	//	/* [in] */ ULONG cCmds,
	//	/* [out][in][size_is] */ OLECMD __RPC_FAR prgCmds[  ],
	//	/* [unique][out][in] */ OLECMDTEXT __RPC_FAR *pCmdText) = 0;
	//    
	//	virtual HRESULT STDMETHODCALLTYPE Exec( 
	//	/* [unique][in] */ const GUID __RPC_FAR *pguidCmdGroup,
	//	/* [in] */ DWORD nCmdID,
	//	/* [in] */ DWORD nCmdexecopt,
	//	/* [unique][in] */ VARIANT __RPC_FAR *pvaIn,
	//	/* [unique][out][in] */ VARIANT __RPC_FAR *pvaOut) = 0;
        
	[ComImport, Guid("b722bccb-4e68-101b-a2bc-00aa00404770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)] 
	public interface IOleCommandTarget
	{
		void QueryStatus( [In, MarshalAs(UnmanagedType.LPStruct)] Guid pguidCmdGroup, [In] uint cCmds,
			[ In, Out] ref OLECMD prgCmds,
			[ In, Out] IntPtr pCmdText);
		void Exec( [In, MarshalAs(UnmanagedType.LPStruct)] Guid pguidCmdGroup,	[In] uint nCmdID, [In] OLECMDEXECOPT nCmdexecopt,
			[In] IntPtr pvaIn,
			[In, Out] IntPtr pvaOut);
	}
}