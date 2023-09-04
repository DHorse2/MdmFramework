/*
 *    Copyright (C) 2004  Ioannis Aslanidis
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 */

using System;

namespace PacketCustomizer.Functions
{
	/// <summary>
	/// Esta librer�a contiene funciones de conversi�n de cadenas a otros tipos.
	/// </summary>
	public class Conversion
	{
		public static bool isHex(string s)
		{
			foreach(char c in s)
			{
				if(
					!((c.ToString().CompareTo("0") >= 0 &&
					c.ToString().CompareTo("9") <= 0) ||
					(c.ToString().CompareTo("A") >= 0 &&
					c.ToString().CompareTo("F") <= 0)
					))
				{
					return false;
				}
			}
			return true;
		}

		public static bool isDec(string s)
		{
			if(s.Length < 1)
				return false;
			foreach(char c in s)
			{
				if(
					!((c.ToString().CompareTo("0") >= 0 &&
					c.ToString().CompareTo("9") <= 0)
					))
				{
					return false;
				}
			}
			return true;
		}

		public static uint str2uint(string cadena)
			/* Esta funci�n convierte la entrada decimal en uint
			 */
		{
			uint total = 0;
			foreach(char c in cadena)
			{
				total *= 10;
				total += (uint)(c - '0');
			}
			return total;
		}

		public static byte[] String2ByteArray(string cadena)
			/* Esta funci�n convierte una cadena hexadecimal en un array de bytes.
			 */
		{
			byte[] bytearray;
			int posicion = 0;
			bytearray = new byte[(cadena.Length/2) + (cadena.Length%2)];
			string TreatedPair;
			while(cadena.Length > 0)
			{
				if(cadena.Length > 1)
				{
					TreatedPair = cadena.Substring(0,2);
					cadena = cadena.Remove(0,2);
				}
				else
				{
					TreatedPair = cadena.Substring(0,1);
					cadena = cadena.Remove(0,1);
				}
				bytearray[posicion++] = str2byte(TreatedPair);
			}
			return bytearray;
		}

		public static byte str2byte(string cadena)
			/* Esta funci�n convierte la entrada hexadecimal en byte decimal
			 */
		{
			uint total;
			if(cadena.Length == 2)
			{
				total = HexToDec(cadena[0]);
				total *= 16;
				total += HexToDec(cadena[1]);
			}
			else if(cadena.Length == 1)
			{
				total = HexToDec(cadena[0]);
			}
			else
				total = 0;
			return (byte)total;
		}

		public static uint HexToDec(char hex)
			/* Convierte un car�cter en un unsigned integer.
			*/
		{
			if(hex.ToString().StartsWith("0"))
				return(0);
			else if(hex.ToString().StartsWith("1"))
				return(1);
			else if(hex.ToString().StartsWith("2"))
				return(2);
			else if(hex.ToString().StartsWith("3"))
				return(3);
			else if(hex.ToString().StartsWith("4"))
				return(4);
			else if(hex.ToString().StartsWith("5"))
				return(5);
			else if(hex.ToString().StartsWith("6"))
				return(6);
			else if(hex.ToString().StartsWith("7"))
				return(7);
			else if(hex.ToString().StartsWith("8"))
				return(8);
			else if(hex.ToString().StartsWith("9"))
				return(9);
			else if(hex.ToString().StartsWith("A"))
				return(10);
			else if(hex.ToString().StartsWith("B"))
				return(11);
			else if(hex.ToString().StartsWith("C"))
				return(12);
			else if(hex.ToString().StartsWith("D"))
				return(13);
			else if(hex.ToString().StartsWith("E"))
				return(14);
			else if(hex.ToString().StartsWith("F"))
				return(15);
			return(0);
		}
	}
}
