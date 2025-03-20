using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

#if NET5_0_OR_GREATER || NETCOREAPP
    using System.Text.Json;
    using System.Text.Json.Serialization;
#endif

using System.Xml.Serialization;

namespace FSLibrary
{
    /// <summary>
    /// Funciones para la manipulación de números
    /// </summary>
    public class NumberUtils
    {

        /// <summary>
        /// Determines whether the specified value is numeric.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is numeric; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumeric(string value)
        {
            bool isNum;
            double retNum;

            if (String.IsNullOrEmpty(value))
                return false;

            isNum = double.TryParse(value, NumberStyles.Number | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowThousands
| NumberStyles.AllowDecimalPoint, NumberFormatInfo.CurrentInfo,
                out retNum);
            return isNum;
        }

        /// <summary>
        /// Convierte en byte el valor especficado.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte NumberByte(object value)
        {
            byte val = 0;

            if (value == null)
                return 0;

            if (string.IsNullOrEmpty(value.ToString())) return 0;
            if (value is System.Enum) return Convert.ToByte(value);
            if (IsNumeric(value.ToString()))
            {
                if (byte.TryParse(value.ToString(), out val))
                    return val;
                return 0;
            }

            return 0;
        }

        /// <summary>
        /// Convierte en numero decimal el valor especificado.
        /// </summary>
        /// <param name="value">The v.</param>
        /// <returns></returns>
        public static decimal NumberDecimal(object value)
        {
            decimal val = 0;

            if (value == null)
                return 0;

            if (string.IsNullOrEmpty(value.ToString())) return 0;
            if (value is System.Enum) return Convert.ToDecimal(value);
            if (IsNumeric(value.ToString()))
            {
                if (decimal.TryParse(value.ToString(), out val))
                    return val;
                return 0;
            }

            return 0;
        }



        /// <summary>
        /// Convierte en numero double el valor especificado.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double NumberDouble(object value)
        {
            double val = 0;
            if (value == null)
                return 0;

            if (string.IsNullOrEmpty(value.ToString())) return 0;
            if (value is System.Enum) return Convert.ToDouble(value);
            if (IsNumeric(value.ToString()))
            {
                if (double.TryParse(value.ToString(), out val))
                    return val;
                return 0;
            }

            return 0;
        }

        /// <summary>
        /// Convierte en numero entero el valor especificado.
        /// </summary>
        /// <param name="value">The v.</param>
        /// <returns></returns>
        public static int NumberInt(object value)
        {
            int val = 0;

            if (value == null)
                return 0;

            if (string.IsNullOrEmpty(value.ToString())) return 0;
            if (value is System.Enum || value is double) return Convert.ToInt32(value);
            if (IsNumeric(value.ToString()))
            {
                if (int.TryParse(value.ToString(), out val))
                    return val;
                return 0;
            }

            return 0;
        }


        /// <summary>
        /// Convierte en numero long el valor especificado.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long NumberLong(object value)
        {
            long val = 0;

            if (value == null)
                return 0;

            if (string.IsNullOrEmpty(value.ToString()))
                return 0;
            if (value is System.Enum)
                return Convert.ToInt64(value);

            if (IsNumeric(value.ToString()))
            {
                if (long.TryParse(value.ToString(), out val))
                    return val;
                return 0;
            }

            return 0;
        }

        /// <summary>
        /// Bytes the array compare.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="a2">The a2.</param>
        /// <returns></returns>
        public static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            for (var i = 0; i < a1.Length; i++)
                if (a1[i] != a2[i])
                    return false;

            return true;
        }

        /// <summary>
        /// Reads all bytes.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="shareMode">The share mode.</param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(string filePath, FileAccess fileAccess = FileAccess.Read, FileShare shareMode = FileShare.ReadWrite)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, fileAccess, shareMode))
            {
                using (var ms = new MemoryStream())
                {
#if NET35
                    FSLibrary.Functions.CopyTo(fs, ms);
#else
                    fs.CopyTo(ms);
#endif
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// Tamaña en bits en la compilación.
        /// </summary>
        /// <returns></returns>
        public static string CompilationSize()
        {
            if (IntPtr.Size == 1)
            {
                return "8-Bit";
            }
            else if (IntPtr.Size == 2)
            {
                return "16-Bit";
            }
            else if (IntPtr.Size == 4)
            {
                return "32-Bit";
            }
            else if (IntPtr.Size == 8)
            {
                return "64-Bit";
            }
            else if (IntPtr.Size == 16)
            {
                return "128-Bit";
            }
            else if (IntPtr.Size == 32)
            {
                return "256-Bit";
            }
            else if (IntPtr.Size == 64)
            {
                return "512-Bit";
            }
            else if (IntPtr.Size == 128)
            {
                return "1024-Bit";
            }
            else if (IntPtr.Size == 256)
            {
                return "2048-Bit";
            }
            else if (IntPtr.Size == 512)
            {
                return "4096-Bit";
            }
            else if (IntPtr.Size == 1024)
            {
                return "8192-Bit";
            }
            else if (IntPtr.Size == 2048)
            {
                return "16384-Bit";
            }

            return "Error!";
        }

        /// <summary>
        /// Byteses to string.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static string BytesToString(byte[] bytes)
        {
            string output = string.Empty;

            MemoryStream stream = new MemoryStream(bytes);
            stream.Position = 0;

            using (var reader = new StreamReader(stream))
            {
                output = reader.ReadToEnd();
            }

            return output;
        }


        /// <summary>
        /// Byteses to string hex number.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="separator">Separator</param>
        /// <returns></returns>
        public static string BytesToStringHex(byte[] bytes, char separator = ' ')
        {
            string output = string.Empty;

            foreach(byte b in bytes)
            {
                output += b.ToString("X2") + separator;
            }

            return output.Substring(0, output.Length - 1);
        }

        /// <summary>
        /// Byteses to string hex number.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static string BytesToStringAscii(byte[] bytes)
        {
            string output = string.Empty;

            foreach (byte b in bytes)
            {
                //if (b >= 20 && b <= 128)
                    output += ((char)b).ToString();
                //else
                //    output += "\\" + b.ToString("X2");
            }
            return output;
        }

        /// <summary>
        /// Devuelve el estado del bit 0 del número entero especificado
        /// </summary>
        /// <param name="number">Número entero.</param>
        /// <returns></returns>
        public static bool BitTest(int number)
        {
            int[] aInt = { number };


            BitArray ba = new BitArray(aInt);

            return ba[0];
        }

        /// <summary>
        /// Establece el bit posición, del número entero.
        /// </summary>
        /// <param name="number">Número entero.</param>
        /// <param name="position">Posición.</param>
        /// <returns></returns>
        public static int BitSet(int number, int position)
        {
            int[] aInt = { number };

            var aBits = new BitArray(aInt);

            aBits.Set(position, true);

            var aResults = Array.CreateInstance(typeof(int), 1);
            aBits.CopyTo(aResults, 0);

            return (int)aResults.GetValue(0);
        }

        /// <summary>
        /// Establece el bit posición, del número entero con el valor indicado.
        /// </summary>
        /// <param name="number">Número entero.</param>
        /// <param name="position">Posición.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static int BitSet(int number, int position, bool value)
        {
            int[] aInt = { number };

            var aBits = new BitArray(aInt);

            aBits.Set(position, value);

            var aResults = Array.CreateInstance(typeof(int), 1);
            aBits.CopyTo(aResults, 0);

            return (int)aResults.GetValue(0);
        }


        /// <summary>
        /// Establece el bit inverso en la posición, del número entero.
        /// </summary>
        /// <param name="number">Número entero.</param>
        /// <param name="position">Posición.</param>
        /// <returns></returns>
        public static int BitInvertSet(int number, int position)
        {
            int[] aInt = { number };

            var aBits = new BitArray(aInt);

            aBits.Set(position, !aBits.Get(position));

            var aResults = Array.CreateInstance(typeof(int), 1);
            aBits.CopyTo(aResults, 0);

            return (int)aResults.GetValue(0);
        }

        /// <summary>
        /// Devuelve el valor hexadecimal de un entero
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string IntToHex(int number)
        {
            return number.ToString("X");
        }

        /// <summary>
        /// Devuelve el valor enterio de un numero hexadecimal
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int HexToInt(string number)
        {
            return int.Parse(number, System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>
        /// Randoms the hexadecimal value.
        /// </summary>
        /// <param name="hexLength">Length of the hexadecimal.</param>
        /// <returns></returns>
        public static string RandomHexValue(int hexLength)
        {
            var intHexLength = 0;
            var intLoopCounter = 0;
            var strHexValue = "";
            var h = "";

            var r = new Random();

            for (intLoopCounter = 1; intLoopCounter <= hexLength; intLoopCounter++)
            {
                intHexLength = NumberUtils.NumberInt(r.Next(1000)) % 16;

                switch (intHexLength)
                {
                    case 1:
                        strHexValue = "1";
                        break;
                    case 2:
                        strHexValue = "2";
                        break;
                    case 3:
                        strHexValue = "3";
                        break;
                    case 4:
                        strHexValue = "4";
                        break;
                    case 5:
                        strHexValue = "5";
                        break;
                    case 6:
                        strHexValue = "6";
                        break;
                    case 7:
                        strHexValue = "7";
                        break;
                    case 8:
                        strHexValue = "8";
                        break;
                    case 9:
                        strHexValue = "9";
                        break;
                    case 10:
                        strHexValue = "A";
                        break;
                    case 11:
                        strHexValue = "B";
                        break;
                    case 12:
                        strHexValue = "C";
                        break;
                    case 13:
                        strHexValue = "D";
                        break;
                    case 14:
                        strHexValue = "E";
                        break;
                    case 15:
                        strHexValue = "F";
                        break;
                    default:
                        strHexValue = "Z";
                        break;
                }


                h = h + strHexValue;
            }

            return h;
        }

        /// <summary>
        /// Copies the packet.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <param name="length">The length.</param>
        /// <param name="padToLength">if set to <c>true</c> [pad to length].</param>
        /// <returns></returns>
        public static byte[] CopyBytePacket(byte[] source, int index, int length, bool padToLength = false)
        {
            int n = length;
            byte[] packet = null;

            if (source.Length < index + length)
            {
                n = source.Length - index;
                if (padToLength)
                    packet = new byte[length];
            }

            if (packet == null)
                packet = new byte[n];
            Array.Copy(source, index, packet, 0, n);

            return packet;
        }

        /// <summary>
        /// Sliceses the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="count">The count.</param>
        /// <param name="padToLength">if set to <c>true</c> [pad to length].</param>
        /// <returns></returns>
        public static IEnumerable<byte[]> BytePackets(byte[] source, int count, bool padToLength = false)
        {
            for (var i = 0; i < source.Length; i += count)
                yield return CopyBytePacket(source, i, count, padToLength);
        }

        /// <summary>
        /// Convierte de bytes a Kb, Mb, Gb, Tb
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }


        /// <summary>
        /// Convierte una cadena con B MB KB GB TB en bytes.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToBytes(string value)
        {
            value = value.Trim();
            value = value.Replace(" ", "");
            value = value.ToLower();

            if (value.IndexOf("tb") > 0)
            {
                value = value.Replace("tb", "");
                return Convert.ToInt64(value) * 1024 * 1024 * 1024 * 1024;
            }
            if (value.IndexOf("gb") > 0)
            {
                value = value.Replace("gb", "");
                return Convert.ToInt64(value) * 1024 * 1024 * 1024;
            }
            if (value.IndexOf("mb") > 0)
            {
                value = value.Replace("mb", "");
                return Convert.ToInt64(value) * 1024 * 1024;
            }
            if (value.IndexOf("kb") > 0)
            {
                value = value.Replace("kb", "");
                return Convert.ToInt64(value) * 1024;
            }
            if (value.IndexOf("b") > 0)
            {
                value = value.Replace("b", "");
                return Convert.ToInt64(value);
            }
            return Convert.ToInt64(value);
        }

        /// <summary>
        /// Convierte una cadena de bytes separadas por 'separator' en un array de bytes.
        /// </summary>
        /// <param name="pairText"></param>
        /// <returns></returns>
        public static byte[] StringBytePairToBytes(string pairText)
        {
            pairText = GetNumbersAndLetters(pairText);
            return StringBytePairToBytes(pairText, '-');
        }

        /// <summary>
        /// Convierte una cadena de bytes separadas por 'separator' en un array de bytes.
        /// </summary>
        /// <param name="pairText"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static byte[] StringBytePairToBytes(string pairText, char separator)
        {
            //Si no se pescifica un separado de bytes, lo añadimos
            if (pairText.IndexOf(separator) < 0)
            {
                var parts = TextUtil.SplitInParts(pairText, 2);
                pairText = TextUtil.JoinParts(separator, parts);
            }

            //Convertimos la cadena separada por parejas de bytes en array de bytes.
            var bytes = pairText.Split(separator)
                 .Select(x => Convert.ToByte(x, 16))
                 .ToArray();

            return bytes;
        }

        /// <summary>
        /// Devuelve una cadena unicamente con los numeros.
        /// </summary>
        /// <param name="input"></param>
        public static string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        /// <summary>
        /// Devuelve una cadena unicamente con los numeros y letras.
        /// </summary>
        /// <param name="input"></param>
        public static string GetNumbersAndLetters(string input)
        {
            return new string(input.Where(c => (char.IsDigit(c) || char.IsLetter(c))).ToArray());
        }

#if NET5_0_OR_GREATER || NETCOREAPP
        /// <summary>
        /// Convert an object to a Byte Array.
        /// </summary>
        public static byte[] ObjectToByteArray(object objData)
        {
            if (objData == null)
                return default;

            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize<object>(objData, GetJsonSerializerOptions()));
        }

        /// <summary>
        /// Convert a byte array to an Object of T.
        /// </summary>
        public static T ByteArrayToObject<T>(byte[] byteArray)
        {
            if (byteArray == null || !byteArray.Any())
                return default;

            return JsonSerializer.Deserialize<T>(byteArray, GetJsonSerializerOptions());
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                WriteIndented = true,
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
        }
#endif

    }
}