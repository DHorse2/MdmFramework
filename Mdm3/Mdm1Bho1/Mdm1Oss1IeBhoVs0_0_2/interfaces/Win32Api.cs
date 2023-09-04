using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace NxIEHelperNS
{
	class Win32Api
	{
		[DllImport("shell32")]
		internal static extern uint DragQueryFile( uint hDrop, uint iFile,StringBuilder buffer,	int cch);

	}
}
