// DispText.cpp
#include "stdafx.h"
#include "DispText.h"

using namespace System;
using namespace System::Reflection;

namespace Mdm {
	namespace SysUtil {

		void SuText::DisplayAttributes( Int32 indent, MemberInfo^ mi ) {
			// Get the set of custom attributes; if none exist, just return.
			array<Object^>^attrs = mi->GetCustomAttributes( false );
				
			if ( attrs->Length==0 ) {
				return;          
			}
			// Display the custom attributes applied to this member.
			SuText::DisplayLine( indent+1, "Attributes:" );
			int AttrsIndex = 0;
			for each ( Object^ o in attrs ) {
				SuText::DisplayLine( indent*2, "{1}: {0}", o, AttrsIndex );
				AttrsIndex++;
			}				
		}
		void SuText::DisplayBase( bool PassedNewLine, Int32 indent, String^ format, ... array<Object^>^param ) {
			Console::Write( "{0}", gcnew String ( ' ', indent ) );
			if ( PassedNewLine ) {
				Console::WriteLine( format, param );
			} else {
				Console::Write( format, param );
			}
		}

	}
}