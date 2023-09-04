// DispText.h
using namespace System;
using namespace System::Reflection;

namespace Mdm {
	namespace SysUtil {

		static public ref class SuStr {
		public:
			static String^ GetString( String^ PassedLine, String^ PassedSep, int PassedOcc ) {
				String^ GetResult = L"";
				String^ ResultCurr = L"";
				bool LastOcc = false;
				if ( PassedOcc < 0 ) {
					LastOcc = true;
					PassedOcc = 100;
				}
				int SepCurr = 0;
				int SepLast = 0;
				int OccCurr = 0;
				if ( PassedOcc > 0 ) {
					for ( OccCurr = 0; OccCurr < PassedOcc; OccCurr++ ) {
						SepCurr = PassedLine->IndexOf( PassedSep , SepLast );
						if ( SepCurr < 0 ) {
							GetResult = ResultCurr;
							break;
						}
						SepLast = SepCurr + 1;
						if ( SepLast <= PassedLine->Length ) {
							ResultCurr = PassedLine->Substring( SepLast );
						} else { ResultCurr = L""; }
					}
				} else return PassedLine;
				return GetResult;
			}
		};

		// -------------
		static public ref class SuText {
		private:
			static Int32 LastIndent = 0;
			static String^ LastFormat = L"";
			static bool DebugOn = false;
			static Int32 DebugLevel = 4;
		public:
			// code that does actual display
			static void DisplayBase( bool PassedNewLine, Int32 indent, String^ format, ... array<Object^>^param );
			//
			// Newline flag passed on to base
			static void Display( Int32 indent, String^ format, ... array<Object^>^param ) { DisplayBase( false, indent, format, param ); }
			static void DisplayLine( Int32 indent, String^ format, ... array<Object^>^param ) { DisplayBase( true, indent, format, param ); }
			static void DisplayAttributes( Int32 indent, MemberInfo^ mi );
			// (String^, Param) uses (LastIndent, String^, Param)
			static void DisplayLine( String^ format, ... array<Object^>^param ) { DisplayLine( LastIndent, format, param ); }
			static void Display( String^ format, ... array<Object^>^param ) { Display( LastIndent, format, param ); }
			// Objects are converted to strings and (String^, Param)
			static void DisplayLine( Object^ PassedObject, ... array<Object^>^param ) { Display( PassedObject->ToString(), param ); }
			static void Display( Object^ PassedObject, ... array<Object^>^param ) { Display( PassedObject->ToString(), param ); }
		};

	}
}