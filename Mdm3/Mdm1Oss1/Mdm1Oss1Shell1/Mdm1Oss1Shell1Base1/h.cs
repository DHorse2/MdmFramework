using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Mdm.Oss.WinUtil.Types {
    public static class h {

        public struct LONG_PTR {
            long value;
            public static implicit operator uint(LONG_PTR x) { return (uint)x.value; }
            public static implicit operator int(LONG_PTR x) { return (int)x.value; }
        } //  typedef long LONG_PTR;
        public struct UINT_PTR {
            IntPtr value;
            public static implicit operator uint(UINT_PTR x) { return (uint)x.value; }
            public static implicit operator int(UINT_PTR x) { return (int)x.value; }
        }
        public struct size_t { ulong value; } // typedef ULONG_PTR size_t;

        public struct HINSTANCE {
            IntPtr? value;
            public HINSTANCE(int x) { value = (IntPtr)x; }
            public static implicit operator uint(HINSTANCE x) { return (uint)x.value; }
            public static implicit operator int(HINSTANCE x) { return (int)x.value; }
            public static implicit operator HINSTANCE(int x) {
                HINSTANCE y = new HINSTANCE(x);  // explicit conversion
                // value = (IntPtr)b; 
                System.Console.WriteLine("conversion occurred");
                return y;
            }

        }
        public struct HWND { IntPtr value; }
        public struct HANDLE { IntPtr value; }

        public struct HRESULT {
            LONG value;
            public static implicit operator uint(HRESULT x) { return (uint)x.value; }
            public static implicit operator int(HRESULT x) { return (int)x.value; }
        } // typedef LONG HRESULT;
        public struct LRESULT {
            LONG_PTR value;
            public static implicit operator uint(LRESULT x) { return (uint)x.value; }
            public static implicit operator int(LRESULT x) { return (int)x.value; }
        } // typedef LONG_PTR LRESULT;

        public struct DWORD { ulong value; } // typedef unsigned long DWORD;
        public struct LONG {
            long value;
            public static implicit operator uint(LONG x) { return (uint)x.value; }
            public static implicit operator int(LONG x) { return (int)x.value; }
        } // typedef long LONG;
        public struct UINT {
            uint value;
            public static implicit operator uint(UINT x) { return x.value; }
            public static implicit operator int(UINT x) { return (int)x.value; }
        } // typedef unsigned int UINT;

        public struct CHAR { char value; }
        public struct WCHAR { char value; }
        public struct TCHAR { char value; }

        public struct LPCTSTR { string value; } // typedef LPCWSTR LPCTSTR; // typedef LPCSTR LPCTSTR;
        //[MarshalAs(UnmanagedType.LPTStr)]
        public struct LPTSTR { 
            string _TheValue;
            //get { return _TheValue }
            //set { _TheValue = value }
        } // typedef LPWSTR LPTSTR;

        public static int WINVER = 9999;
        public static int _WIN32_WINNT = 9999;

        public static int WH_MIN = 14;
        public static int WH_MAX = 0;

        /// <summary>
        /// Device Context Handle
        /// </summary>
        public struct HDC { HANDLE value; } // typedef HANDLE HDC; // Device Context Handle

        /// <summary>
        /// Message parameter
        /// </summary>
        public struct Msg { UINT value; }
        /// <summary>
        /// Message parameter. 
        /// </summary>
        public struct WPARAM {
            UINT_PTR value;
            public static implicit operator uint(WPARAM x) { return (uint)x.value; }
            public static implicit operator int(WPARAM x) { return (int)x.value; }
        }
        /// <summary>
        /// Message parameter.
        /// </summary>
        public struct LPARAM { LONG_PTR value; }

        /// <summary>
        /// Handle to a hook. 
        /// </summary>
        public struct HHOOK { IntPtr value; }
        /// <summary>
        /// Common dialog box Hook Procedure.
        /// </summary>
        public struct HOOKPROC { IntPtr value; }

        static h() {
            if (WINVER >= 0x0400) {
                if (_WIN32_WINNT >= 0x0400) {
                    WH_MAX = 14;
                } else {
                    WH_MAX = 12;
                }// #endif // (_WIN32_WINNT >= 0x0400)
            } else {
                WH_MAX = 11;
            }// #endif
        }
    }

}

// [In, MarshalAs( UnmanagedType.X )]
// [Out, MarshalAs( UnmanagedType.X )]

// 	[DispId(2)]

// [Out, MarshalAs( UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT )] out 

// void m3([MarshalAs(UnmanagedType.Interface)] ref Delegate d);  
// void m4([MarshalAs(UnmanagedType.FunctionPtr)] Delegate d); 


/* UnmanagedType Enumeration
Member name Description 
 Bool A 4-byte Boolean value (true != 0, false = 0). This is the Win32 BOOL type. 
 I1 A 1-byte signed integer. You can use this member to transform a Boolean value into a 1-byte, C-style bool (true = 1, false = 0). 
 U1 A 1-byte unsigned integer. 
 I2 A 2-byte signed integer. 
 U2 A 2-byte unsigned integer. 
 I4 A 4-byte signed integer. 
 U4 A 4-byte unsigned integer. 
 I8 An 8-byte signed integer. 
 U8 An 8-byte unsigned integer. 
 R4 A 4-byte floating point number. 
 R8 An 8-byte floating point number. 
 Currency Used on a System..::.Decimal to marshal the decimal value as a COM currency type instead of as a Decimal. 
 BStr A Unicode character string that is a length-prefixed double byte. You can use this member, which is the default string in COM, on the String data type. 
 LPStr A single byte, null-terminated ANSI character string. You can use this member on the System..::.String or System.Text..::.StringBuilder data types 
 LPWStr A 2-byte, null-terminated Unicode character string. 
 LPTStr A platform-dependent character string: ANSI on Windows 98 and Unicode on Windows NT and Windows XP. This value is only supported for platform invoke, and not COM interop, because exporting a string of type LPTStr is not supported. 
 ByValTStr Used for in-line, fixed-length character arrays that appear within a structure. The character type used with ByValTStr is determined by the System.Runtime.InteropServices..::.CharSet argument of the System.Runtime.InteropServices..::.StructLayoutAttribute applied to the containing structure. Always use the MarshalAsAttribute..::.SizeConst field to indicate the size of the array. 
 IUnknown A COM IUnknown pointer. You can use this member on the Object data type. 
 IDispatch A COM IDispatch pointer (Object in Microsoft Visual Basic 6.0). 
 Struct A VARIANT, which is used to marshal managed formatted classes and value types. 
 Interface A COM interface pointer. The Guid of the interface is obtained from the class metadata. Use this member to specify the exact interface type or the default interface type if you apply it to a class. This member produces UnmanagedType..::.IUnknown behavior when you apply it to the Object data type. 
 SafeArray A SafeArray is a self-describing array that carries the type, rank, and bounds of the associated array data. You can use this member with the MarshalAsAttribute..::.SafeArraySubType field to override the default element type. 
 ByValArray When MarshalAsAttribute..::.Value is set to ByValArray, the SizeConst must be set to indicate the number of elements in the array. The ArraySubType field can optionally contain the UnmanagedType of the array elements when it is necessary to differentiate among string types. You can only use this UnmanagedType on an array that appear as fields in a structure. 
 SysInt A platform-dependent, signed integer. 4-bytes on 32 bit Windows, 8-bytes on 64 bit Windows. 
 SysUInt A platform-dependent, unsigned integer. 4-bytes on 32 bit Windows, 8-bytes on 64 bit Windows. 
 VBByRefStr Allows Visual Basic 2005 to change a string in unmanaged code, and have the results reflected in managed code. This value is only supported for platform invoke. 
 AnsiBStr An ANSI character string that is a length prefixed, single byte. You can use this member on the String data type. 
 TBStr A length-prefixed, platform-dependent char string. ANSI on Windows 98, Unicode on Windows NT. You rarely use this BSTR-like member. 
 VariantBool A 2-byte, OLE-defined VARIANT_BOOL type (true = -1, false = 0). 
 FunctionPtr An integer that can be used as a C-style function pointer. You can use this member on a Delegate data type or a type that inherits from a Delegate. 
 AsAny A dynamic type that determines the type of an object at run time and marshals the object as that type. Valid for platform invoke methods only. 
 LPArray A pointer to the first element of a C-style array. When marshaling from managed to unmanaged, the length of the array is determined by the length of the managed array. When marshaling from unmanaged to managed, the length of the array is determined from the MarshalAsAttribute..::.SizeConst and the MarshalAsAttribute..::.SizeParamIndex fields, optionally followed by the unmanaged type of the elements within the array when it is necessary to differentiate among string types. 
 LPStruct A pointer to a C-style structure that you use to marshal managed formatted classes. Valid for platform invoke methods only. 
 CustomMarshaler Specifies the custom marshaler class when used with MarshalAsAttribute..::.MarshalType or MarshalAsAttribute..::.MarshalTypeRef. The MarshalAsAttribute..::.MarshalCookie field can be used to pass additional information to the custom marshaler. You can use this member on any reference type. 
 Error WinUtil.Types This native type associated with an I4 or a U4 causes the parameter to be exported as a HRESULT in the exported type library. 
*/

/* VarEnum Enumeration
Member name Description 
 VT_EMPTY Indicates that a value was not specified. 
 VT_NULL Indicates a null value, similar to a null value in SQL. 
 VT_I2 Indicates a short integer. 
 VT_I4 Indicates a long integer. 
 VT_R4 Indicates a float value. 
 VT_R8 Indicates a double value. 
 VT_CY Indicates a currency value. 
 VT_DATE Indicates a DATE value. 
 VT_BSTR Indicates a BSTR string. 
 VT_DISPATCH Indicates an IDispatch pointer. 
 VT_ERROR Indicates an SCODE. 
 VT_BOOL Indicates a Boolean value. 
 VT_VARIANT Indicates a VARIANT far pointer. 
 VT_UNKNOWN Indicates an IUnknown pointer. 
 VT_DECIMAL Indicates a decimal value. 
 VT_I1 Indicates a char value. 
 VT_UI1 Indicates a byte. 
 VT_UI2 Indicates an unsignedshort. 
 VT_UI4 Indicates an unsignedlong. 
 VT_I8 Indicates a 64-bit integer. 
 VT_UI8 Indicates an 64-bit unsigned integer. 
 VT_INT Indicates an integer value. 
 VT_UINT Indicates an unsigned integer value. 
 VT_VOID Indicates a C style void. 
 VT_HRESULT Indicates an HRESULT. 
 VT_PTR Indicates a pointer type. 
 VT_SAFEARRAY Indicates a SAFEARRAY. Not valid in a VARIANT. 
 VT_CARRAY Indicates a C style array. 
 VT_USERDEFINED Indicates a user defined type. 
 VT_LPSTR Indicates a null-terminated string. 
 VT_LPWSTR Indicates a wide string terminated by nullNothingnullptra null reference (Nothing in Visual Basic). 
 VT_RECORD Indicates a user defined type. 
 VT_FILETIME Indicates a FILETIME value. 
 VT_BLOB Indicates length prefixed bytes. 
 VT_STREAM Indicates that the name of a stream follows. 
 VT_STORAGE Indicates that the name of a storage follows. 
 VT_STREAMED_OBJECT Indicates that a stream contains an object. 
 VT_STORED_OBJECT Indicates that a storage contains an object. 
 VT_BLOB_OBJECT Indicates that a blob contains an object. 
 VT_CF Indicates the clipboard format. 
 VT_CLSID Indicates a class ID. 
 VT_VECTOR Indicates a simple, counted array. 
 VT_ARRAY Indicates a SAFEARRAY pointer. 
 VT_BYREF Indicates that a value is a reference. 
*/