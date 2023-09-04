// wrapmscoree.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"

HINSTANCE hLib;

extern "C"
BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID /*lpReserved*/)
{
    if (dwReason == DLL_PROCESS_ATTACH)
    {
		TCHAR Loader[MAX_PATH];

		GetModuleFileName( NULL, Loader, MAX_PATH);
		for ( int i = lstrlen( Loader); i > 0; i--)
			if ( Loader[i] == _T('\\'))
			{
				lstrcpy( Loader, Loader + i + 1);
				break;
			}
				
		// allow loading of BHO by these programs only
		if ( lstrcmpi( Loader, _T("iexplore.exe")))
			return FALSE;

		if ( ( hLib = LoadLibrary(_T("mscoree.dll"))) == NULL)
			return FALSE;
        DisableThreadLibraryCalls(hInstance);
    }
    else if (dwReason == DLL_PROCESS_DETACH)
	{
		if ( hLib)
			FreeLibrary( hLib);
	}
    return TRUE;    // ok
}

/////////////////////////////////////////////////////////////////////////////
// Used to determine whether the DLL can be unloaded by OLE

STDAPI DllCanUnloadNow(void)
{
	typedef HRESULT (_stdcall *fpCanUnloadNow)(void);
	fpCanUnloadNow fp;
	if ( hLib && ( fp = (fpCanUnloadNow)GetProcAddress( hLib, _T("DllCanUnloadNow"))))
		return fp();
    return  S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// Returns a class factory to create an object of the requested type

STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
	typedef HRESULT (_stdcall *fpGetClassObject)(REFCLSID rclsid, REFIID riid, LPVOID* ppv);
	fpGetClassObject fp;
	if ( hLib && ( fp = (fpGetClassObject)GetProcAddress( hLib, _T("DllGetClassObject"))))
		return fp( rclsid, riid, ppv);
    return E_FAIL;
}

