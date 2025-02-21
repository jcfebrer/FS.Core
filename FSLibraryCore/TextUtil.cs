#region

using FSExceptionCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace FSLibraryCore
{
    /// <summary>
    /// Clase para el manejo de cadenas de texto.
    /// </summary>
    public class TextUtil
    {
        /// <summary>
        /// Cadena a sustituir por las comillas.
        /// </summary>
        public static string PROTECT_QUOTE = "{*}";

        /// <summary>
        /// Tipo de alineación.
        /// </summary>
        public enum FormatColumnAlignment
        {
            /// <summary>
            /// The left
            /// </summary>
            Left,
            /// <summary>
            /// The center
            /// </summary>
            Center,
            /// <summary>
            /// The right
            /// </summary>
            Right
        }


         /// <summary>
        /// Devuelve el valor ASCII.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte[] Ascii(string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        /// <summary>
        /// Quita los elementos html de un texto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        /// <summary>
        /// Devuelve true/false si la cadena empieza por search.
        /// </summary>
        /// <param name="str">The cadena.</param>
        /// <param name="search">The buscar.</param>
        /// <returns></returns>
        public static bool StartsWith(string str, string search)
        {
            return str.StartsWith(search);
        }

        /// <summary>
        /// Devuelve true/false si la cadena termina por search.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        public static bool EndsWith(string str, string search)
        {
            return str.EndsWith(search);
        }


        /// <summary>
        /// Devuelve la longitud de la cadena.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static int Length(string str)
        {
            return str.Length;
        }


        /// <summary>
        /// Truncs the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="len">The length.</param>
        /// <returns></returns>
        public static string Trunc(string str, int len)
        {
            return str.Substring(0, len);
        }


        /// <summary>
        /// Alinea la cadena especificada a la izquierda.
        /// </summary>
        /// <param name="cadena">The cadena.</param>
        /// <param name="car">The car.</param>
        /// <param name="tam">The tam.</param>
        /// <returns></returns>
        public static string AlinearIzq(string cadena, char car, int tam)
        {
            return cadena.PadLeft(tam, car);
        }

        /// <summary>
        /// Alinea la cadena especificada a la derecha.
        /// </summary>
        /// <param name="cadena">The cadena.</param>
        /// <param name="car">The car.</param>
        /// <param name="tam">The tam.</param>
        /// <returns></returns>
        public static string AlinearDer(string cadena, char car, int tam)
        {
            return cadena.PadRight(tam, car);
        }


        /// <summary>
        /// Centra la cadena especificada con el caracter spaceChar a la izquierda y derecha.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="len">The length.</param>
        /// <param name="spaceChar">The space character.</param>
        /// <returns></returns>
        public static string Center(string str, int len, string spaceChar)
        {
            var nNum = 0;
            if (len < str.Length + 2) return str;
            nNum = Convert.ToInt32(Math.Round((double) ((len - str.Length) / 2), 0));
            return spaceChar.PadRight(nNum) + str + spaceChar.PadRight(nNum);
        }

        /// <summary>
        /// Centra la cadena especificada.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="len">The length.</param>
        /// <returns></returns>
        public static string Center(string str, int len)
        {
            return Center(str, len, " ");
        }

        /// <summary>
        /// Replicates the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="times">The times.</param>
        /// <returns></returns>
        public static string Replicate(string source, int times)
        {
            StringBuilder sb = new StringBuilder(source.Length * times);
            for (int index = 1; index <= times; index++) sb.Append(source);
            return sb.ToString();
        }


        /// <summary>
        /// Devuelve la cadena intercalando el caracter spaceChar entre cada caracter.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="spaceChar">The space character.</param>
        /// <returns></returns>
        public static string Amplia(string str, string spaceChar)
        {
            string result = "";
            for (int nPos = 0; nPos <= str.Length - 2; nPos++)
                result += str.Substring(nPos, 1) + spaceChar;
            return result + str.Substring(str.Length - 1, 1);
        }


        /// <summary>
        /// Devuelve la cadena intercalando un espacio entre cada caracter.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string Amplia(string str)
        {
            return Amplia(str, " ");
        }


        /// <summary>
        /// Devuelve en un array las cadenas que esten entre los caracters especificados en "quotes" utilizando el separador indicado.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="quotes">The quotes.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static ArrayList Split(string str, string quotes, string separator)
        {
            var res = new ArrayList();
            var openChar = Regex.Escape(Convert.ToString(quotes[0]));
            var closeChar = Regex.Escape(Convert.ToString(quotes[quotes.Length - 1]));
            var pattern = @"\s*(" + openChar + "([^" + closeChar + "]*)" + closeChar + "|([^" + separator +
                          @"]+))\s*";

            foreach (Match m in Regex.Matches(str, pattern))
            {
                var g3 = m.Groups[3].Value;
                if (!(g3 == null) && g3.Length > 0)
                    res.Add(g3);
                else
                    res.Add(m.Groups[2].Value);
            }

            return res;
        }

        /// <summary>
        /// Devuelve en un array las cadenas que esten entre los caracters especificados en "quotes" utilizando la coma como separador.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="quotes">The quotes.</param>
        /// <returns></returns>
        public static ArrayList Split(string str, string quotes)
        {
            return Split(str, quotes, ",");
        }


        /// <summary>
        /// Converts to lower.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string ToLower(string str)
        {
            return str.ToLower();
        }

        /// <summary>
        /// Converts to upper.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string ToUpper(string str)
        {
            return str.ToUpper();
        }


        /// <summary>
        /// Trims by the start.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string TrimStart(string str)
        {
            return str.TrimStart();
        }

        /// <summary>
        /// Trims by the end.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string TrimEnd(string str)
        {
            return str.TrimEnd();
        }

        /// <summary>
        /// Trims the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string Trim(string str)
        {
            return str.Trim();
        }

        /// <summary>
        /// Removes the character.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="blankChar">The blank character.</param>
        /// <returns></returns>
        public static string RemoveChar(string str, string blankChar = " ")
        {
            string nuevaCadena = "";
            str = str.Trim();
            foreach (var cad in str)
                if (cad != char.Parse(blankChar))
                    nuevaCadena = nuevaCadena + cad;
            return nuevaCadena;
        }

        /// <summary>
        /// Devuelve true o false si existe la cadena substr en str.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="substr">The substring.</param>
        /// <returns></returns>
        public static bool Contains(string str, string substr)
        {
            return str.IndexOf(substr) == -1 ? false : true;
        }

        /// <summary>
        /// Devuelve true/false si la cadena1 es igual a la cadena2.
        /// </summary>
        /// <param name="cadena1">The cadena1.</param>
        /// <param name="cadena2">The cadena2.</param>
        /// <returns></returns>
        public static bool Compare(string cadena1, string cadena2)
        {
            return cadena1.Equals(cadena2);
        }

        /// <summary>
        ///     Devuelve true si las cadenas son iguales, independientemente de signos de puntuación, espacios o
        ///     mayúsculas/minúsculas.
        /// </summary>
        /// <param name="cadena1"></param>
        /// <param name="cadena2"></param>
        /// <returns></returns>
        public static bool CompareOnlyChars(string cadena1, string cadena2)
        {
            return string.Compare(cadena1, cadena2, CultureInfo.CurrentCulture,
                       CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0;
        }

        /// <summary>
        /// Devuelve el contenido diferente entre s1 y s2
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static string Difference(string s1, string s2)
        {
            if (String.IsNullOrEmpty(s1))
                return "";

            string strDiff = s2.Replace(s1, "");
            return strDiff;
        }


        /// <summary>
        /// Removes the accents.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string RemoveAccents(string str)
        {
            int posCad = 0;
            var result = "";
            string cad = null;
            var subs = "";
            string Con = "á,é,í,ó,ú,Á,É,Í,Ó,Ú";
            string Sin = "a,e,i,o,u,A,E,I,O,U";
            for (int i = 0; i <= str.Length - 1; i++)
            {
                cad = str.Substring(i, 1);
                posCad = Con.IndexOf(cad);
                if (posCad >= 0)
                    subs = Sin.Substring(posCad, 1);
                else
                    subs = cad;
                result = result + subs;
            }

            return result;
        }

        /// <summary>
        /// Reemplazamos las cadenas entre comillas, dentro de una cadena
        /// </summary>
        /// <param name="input"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceStrings(string input, string replace = "")
        {
            // Expresión regular para detectar cadenas entre comillas dobles
            string stringPattern = @"""[^""\\]*(?:\\.[^""\\]*)*""";

            // Reemplaza las cadenas por una cadena vacía
            return Regex.Replace(input, stringPattern, replace);
        }

        /// <summary>
        /// Cuenta la cadena search dentro de str, empezando por start.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="search">The search.</param>
        /// <param name="start">The start.</param>
        /// <returns></returns>
        public static long CountString(string str, string search, int start)
        {
            int i = 0;
            long result = 0;
            i = IndexOf(str, start, search);
            while (i >= 0)
            {
                result = result + 1;
                i = IndexOf(str, i + 1, search);
            }

            return result;
        }


        /// <summary>
        /// Cuenta la cadena search dentro de str.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        public static long CountString(string str, string search)
        {
            return CountString(str, search, 0);
        }


        /// <summary>
        /// Gets the delimited.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="OpenDelimiter">The open delimiter.</param>
        /// <param name="CloseDelimiter">The close delimiter.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static string GetDelimited(string str, string OpenDelimiter, string CloseDelimiter, ref int index)
        {
            var i = 0;
            var j = 0;
            index--;
            if (index < 0) index = 0;
            i = str.IndexOf(OpenDelimiter, index);
            if (i < 0)
            {
                index = -1;
                return string.Empty;
            }

            i = i + OpenDelimiter.Length;
            j = str.IndexOf(CloseDelimiter, i);
            if (j < 0)
            {
                index = -1;
                return string.Empty;
            }

            index = j + CloseDelimiter.Length;
            return str.Substring(i, j - i);
        }


        /// <summary>
        /// Filters the duplicates.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string FilterDup(string str)
        {
            var nPos = 0;
            string cCar = null;
            var cCad2 = "";
            for (nPos = 0; nPos <= str.Length - 1; nPos++)
            {
                cCar = str.Substring(nPos, 1);
                cCad2 += str.IndexOf(cCar, nPos + 1) >= 0 ? cCar : "";
            }

            return cCad2;
        }


        /// <summary>
        /// Devuelve el caracter más alto en la tabla ascii.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string MaxChar(string str)
        {
            string cMayor = null;
            var i = 0;
            string Subcad = null;
            cMayor = str.Substring(0, 1);
            for (i = 1; i <= str.Length - 1; i++)
            {
                Subcad = str.Substring(i, 1);
                if (Ascii(Subcad)[0] > Ascii(cMayor)[0]) 
                    cMayor = Subcad;
            }

            return cMayor;
        }


        /// <summary>
        /// Devuelve el caracter más bajo en la tabla ascii.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string MinChar(string str)
        {
            string cMenor = null;
            var i = 0;
            string Subcad = null;
            cMenor = str.Substring(0, 1);
            for (i = 1; i <= str.Length - 1; i++)
            {
                Subcad = str.Substring(i, 1);
                if ((int)Ascii(Subcad)[0] < (int)Ascii(cMenor)[0]) 
                    cMenor = Subcad;
            }

            return cMenor;
        }


        /// <summary>
        /// Sorts the string in ascendent order.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string SortStringAsc(string str)
        {
            char[] characters = str.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }


        /// <summary>
        /// Sorts the string in descendent order.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string SortStringDesc(string str)
        {
            char[] characters = str.ToArray();
            Array.Sort(characters);
            Array.Reverse(characters);
            return new string(characters);
        }


        /// <summary>
        /// Counts the words.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static int CountWords(string str)
        {
            var lSwitch = true;
            int result = 0;
            var cCad2 = @",;.:-_'{}[]()*+^`???=/&%$#@ |!\" + @"""";
            for (int nPos = 0; nPos <= str.Length - 1; nPos++)
            {
                if (!Contains(cCad2, str.Substring(nPos, 1)) & lSwitch)
                {
                    lSwitch = false;
                    result = result + 1;
                }

                if (Contains(cCad2, str.Substring(nPos, 1))) lSwitch = true;
            }

            return result;
        }

        /// <summary>
        /// Counts the words by blank space.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static int CountWordsByBlankSpace(string str)
        {
            var wordCount = 0;
            var isSpace = true;
            for (int counter = 0; counter <= str.Length - 1; counter++)
                if (str.Substring(counter, 1) == " ")
                {
                    isSpace = true;
                }
                else
                {
                    if (isSpace)
                    {
                        isSpace = false;
                        wordCount = wordCount + 1;
                    }
                }

            return wordCount;
        }


        /// <summary>
        /// Strips the control chars.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="KeepCRLF">if set to <c>true</c> [keep CRLF].</param>
        /// <returns></returns>
        public static string StripControlChars(string str, bool KeepCRLF)
        {
            var sb = new StringBuilder(str.Length);
            for (int index = 0; index <= str.Length - 1; index++)
                if (!char.IsControl(str, index))
                    sb.Append(str[index]);
                else if (KeepCRLF && str.Substring(index, 2) == "\n") sb.Append(str[index]);
            return sb.ToString();
        }


        /// <summary>
        /// Strips the control chars.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string StripControlChars(string str)
        {
            return StripControlChars(str, true);
        }


        /// <summary>
        /// Gets the index of the token.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="PintIndex">Index of the pint.</param>
        /// <param name="PstrDelimiter">The PSTR delimiter.</param>
        /// <returns></returns>
        public static string GetTokenIdx(string str, int PintIndex, string PstrDelimiter)
        {
            string[] strSubString = null;
            var intIndex2 = 0;
            var i = 0;
            var intDelimitLen = 0;
            var PstrVal = str;
            intIndex2 = 1;
            i = 0;
            intDelimitLen = PstrDelimiter.Length;
            while (intIndex2 > 0)
            {
                var tt = new string[i + 1];
                Array.Copy(strSubString, tt, Math.Min(strSubString.Length, tt.Length));
                strSubString = tt;
                intIndex2 = PstrVal.IndexOf(PstrDelimiter, 0) + 1;
                if (intIndex2 > 0)
                {
                    strSubString[i] = PstrVal.Substring(1, intIndex2 - 1);
                    PstrVal = PstrVal.Substring(intIndex2 + intDelimitLen, PstrVal.Length);
                }
                else
                {
                    strSubString[i] = PstrVal;
                }

                i = i + 1;
            }

            if ((PintIndex > i + 1) | (PintIndex < 1))
                return string.Empty;
            return strSubString[PintIndex - 1];
        }


        /// <summary>
        /// Quitas espacios duplicados.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string QuitaEspaciosDup(string str)
        {
            const string TWO_SPACES = "  ";
            int intPos = 0;
            string strtemp = null;
            var PstrText = str.Trim();
            intPos = IndexOf(PstrText, 1, TWO_SPACES);
            while (intPos >= 0)
            {
                strtemp = PstrText.Substring(intPos + 1).TrimStart();
                PstrText = PstrText.Substring(0, intPos) + strtemp;
                intPos = IndexOf(PstrText, 1, TWO_SPACES);
            }

            return PstrText;
        }


        /// <summary>
        /// Checks text is numeric.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="DecValue">if set to <c>true</c> [decimal value].</param>
        /// <returns></returns>
        public static bool CheckNumeric(string text, bool DecValue)
        {
            short i = 0;
            var decSep = ".";
            for (i = 1; i <= text.Length; i++)
            {
                var selectVal = text.Substring(i, 1);
                if ("0" != selectVal && selectVal != "9")
                {
                    //<=   <=
                }
                else if (selectVal == "-" || selectVal == "+")
                {
                    if (i > 1) return false;
                }
                else if (selectVal == decSep)
                {
                    if (!DecValue) return false;
                    if (text.IndexOf(decSep) + 1 < i) return false;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Filters the string wuth valir chars.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <param name="ValidChars">The valid chars.</param>
        /// <returns></returns>
        public static string FilterString(string str, string ValidChars)
        {
            var i = 0;
            var result = "";
            for (i = 0; i <= str.Length - 1; i++)
            {
                var p = str.Substring(i, 1);
                if (ValidChars.IndexOf(p) > -1)
                    result = result + str.Substring(i, 1);
            }

            return result;
        }


        /// <summary>
        /// Devuelve una cadena con solo caracteres alfa numéricos incluyendo acentos.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string OnlyAlfaNumeric(string str)
        {
            string alpha = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
            string accents = "áéíóúÁÉÍÓÚ";
            string symb = "@%.";
            return FilterString(str, alpha + accents + symb);
        }


        /// <summary>
        /// Devuelve una cadena con solo caracteres numéricos.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string OnlyNumeric(string str)
        {
            var digit = "0123456789";
            return FilterString(str, digit);
        }


        /// <summary>
        /// Counts the lines.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static int CountLines(string str)
        {
            if (String.IsNullOrEmpty(str))
                return 0;
            return Convert.ToInt32(CountString(str, "\t", 1) + 1);
        }


        /// <summary>
        /// Gets the line string on position.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public static string GetLineString(string str, int position)
        {
            string[] lines = str.Split("\t".ToCharArray());
            string result = "";
            
            if(position <= lines.Length)
                result = lines[position];

            return result;
        }


        /// <summary>
        /// Joins the string with the separator.
        /// </summary>
        /// <param name="separator">The separator.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static string JoinStrings(string separator, params string[] args)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var o in args)
            {
                if (builder.Length > 0)
                    builder.Append(separator);
                builder.Append(o);
            }

            return builder.ToString();
        }


        /// <summary>
        /// Joins the strings with the comma character.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static string JoinStrings(params string[] args)
        {
            return string.Join(",", args);
        }


        /// <summary>
        /// Determines whether [is string lower] [the specified s text].
        /// </summary>
        /// <param name="str">The s text.</param>
        /// <returns>
        ///   <c>true</c> if [is string lower] [the specified s text]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsStringLower(string str)
        {
            foreach (var c in str)
                if (!char.IsLower(c))
                    return false;
            return true;
        }


        /// <summary>
        /// Determines whether [is string upper] [the specified s text].
        /// </summary>
        /// <param name="str">The s text.</param>
        /// <returns>
        ///   <c>true</c> if [is string upper] [the specified s text]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsStringUpper(string str)
        {
            foreach (var c in str)
                if (!char.IsUpper(c))
                    return false;
            return true;
        }


        /// <summary>
        /// Determines whether the specified string is control.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///   <c>true</c> if the specified string is control; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsControl(string str, int position)
        {
            return char.IsControl(str, position);
        }


        /// <summary>
        /// Determines whether the specified string is digit.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///   <c>true</c> if the specified string is digit; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDigit(string str, int position)
        {
            return char.IsDigit(str, position);
        }


        /// <summary>
        /// Determines whether the specified string is letter.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///   <c>true</c> if the specified string is letter; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLetter(string str, int position)
        {
            return char.IsLetter(str, position);
        }


        /// <summary>
        /// Determines whether [is letter or digit] [the specified string].
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///   <c>true</c> if [is letter or digit] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLetterOrDigit(string str, int position)
        {
            return char.IsLetterOrDigit(str, position);
        }


        /// <summary>
        /// Determines whether [is character lower] [the specified string].
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///   <c>true</c> if [is character lower] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCharLower(string str, int position)
        {
            return char.IsLower(str, position);
        }


        /// <summary>
        /// Determines whether [is character number] [the specified string].
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///   <c>true</c> if [is character number] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCharNumber(string str, int position)
        {
            return char.IsNumber(str, position);
        }


        /// <summary>
        /// Determines whether [is character puntuacion] [the specified string].
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///   <c>true</c> if [is character puntuacion] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCharPuntuacion(string str, int position)
        {
            return char.IsPunctuation(str, position);
        }


        /// <summary>
        /// Determines whether [is character separator] [the specified string].
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///   <c>true</c> if [is character separator] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCharSeparator(string str, int position)
        {
            return char.IsSeparator(str, position);
        }


        /// <summary>
        /// Determines whether [is character symbol] [the specified string].
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///   <c>true</c> if [is character symbol] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCharSymbol(string str, int position)
        {
            return char.IsSymbol(str, position);
        }


        /// <summary>
        /// Determines whether [is character upper] [the specified string].
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///   <c>true</c> if [is character upper] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCharUpper(string str, int position)
        {
            return char.IsUpper(str, position);
        }


        /// <summary>
        /// Determines whether [is character white] [the specified string].
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///   <c>true</c> if [is character white] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCharWhite(string str, int position)
        {
            return char.IsWhiteSpace(str, position);
        }


        /// <summary>
        /// Cleans the ampersand.
        /// </summary>
        /// <param name="stringIn">The string in.</param>
        /// <returns></returns>
        public static string CleanAmpersand(string stringIn)
        {
            return stringIn.Replace("&", "&&");
        }


        /// <summary>
        /// Cleans the quotes.
        /// </summary>
        /// <param name="stringIn">The string in.</param>
        /// <returns></returns>
        public static string CleanQuotes(string stringIn)
        {
            return stringIn.Replace(Convert.ToString(@""""), "'");
        }


        /// <summary>
        /// Determines whether the specified string is alpha.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///   <c>true</c> if the specified string is alpha; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlpha(string str)
        {
            var counter = 0;
            string tempChar = null;
            var hasAlpha = false;
            for (counter = 0; counter <= str.Length - 1; counter++)
            {
                tempChar = str.Substring(counter, 1);
                if (!((double.Parse(tempChar) >= double.Parse("0")) & (double.Parse(tempChar) <= double.Parse("9"))))
                {
                    if ((tempChar != "-") & (tempChar != " "))
                    {
                        hasAlpha = true;
                        break;
                    }
                }
            }

            return hasAlpha;
        }


        /// <summary>
        /// Counts the delimited words.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static int CountDelimitedWords(string str, string delimiter)
        {
            var wordCount = 1;
            var position = 0;
            position = str.IndexOf(delimiter);
            while (position > -1)
            {
                wordCount = wordCount + 1;
                position = str.IndexOf(delimiter, position + 1);
            }

            return wordCount;
        }


        /// <summary>
        /// Counts the occurrences.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="searchFor">The search for.</param>
        /// <returns></returns>
        public static int CountOccurrences(string str, string searchFor)
        {
            var position = 0;
            var wordCount = 0;
            position = str.IndexOf(searchFor);
            if (position > 0) wordCount = 1;
            while (position > 0)
            {
                position = str.IndexOf(searchFor, position + 1);
                if (position > -1) wordCount = wordCount + 1;
            }

            return wordCount;
        }


        /// <summary>
        /// Encloses the string with begin char and end char.
        /// </summary>
        /// <param name="stringIn">The string in.</param>
        /// <param name="charsBegin">The chars begin.</param>
        /// <param name="charsEnd">The chars end.</param>
        /// <param name="skipIfEnclosed">if set to <c>true</c> [skip if enclosed].</param>
        /// <returns></returns>
        public static string EncloseString(string stringIn, string charsBegin, string charsEnd, bool skipIfEnclosed)
        {
            var skip = false;
            var retValue = new StringBuilder(stringIn);
            if (skipIfEnclosed)
                if (stringIn.StartsWith(charsBegin))
                    skip = true;
            if (!skip)
            {
                retValue.Append(charsBegin, 0, charsBegin.Length);
                retValue.Append(charsEnd);
            }

            return retValue.ToString();
        }


        /// <summary>
        /// Generates the random password.
        /// </summary>
        /// <param name="pwdLength">Length of the password.</param>
        /// <param name="alternateVowels">if set to <c>true</c> [alternate vowels].</param>
        /// <returns></returns>
        public static string GenerateRandomPassword(int pwdLength, bool alternateVowels)
        {
            int position = 0;
            int randomChar = 0;
            string letters = "AEIOUBCDFGHJKLMNPQRSTVWXYZ";
            string retValue = "";
            Random rndNum = new Random(System.DateTime.Now.Second);
            for (position = 0; position <= pwdLength - 1; position++)
            {
                if (alternateVowels)
                {
                    if (position % 2 == 0)
                        randomChar = rndNum.Next(6, 26);
                    else
                        randomChar = rndNum.Next(1, 5);
                }
                else
                {
                    randomChar = rndNum.Next(1, 26);
                }

                retValue = retValue + letters.Substring(randomChar, 1);
            }

            return retValue;
        }


        /// <summary>
        /// Gets the delimited word.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="index">The index.</param>
        /// <param name="delimitChar">The delimit character.</param>
        /// <returns></returns>
        public static string GetDelimitedWord(string str, int index, string delimitChar)
        {
            var counter = 1;
            var startPos = 0;
            var endPos = 0;
            var retVal = "";
            var indexExceedsWordCount = false;
            for (counter = 2; counter <= index; counter++)
            {
                startPos = str.IndexOf(delimitChar, startPos) + 1;
                if (startPos == -1) indexExceedsWordCount = true;
            }

            if (!indexExceedsWordCount & !(index == 0))
            {
                endPos = str.IndexOf(delimitChar, startPos + 1);
                if (endPos < 0) endPos = str.Length;
                retVal = str.Substring(startPos, endPos - startPos);
            }

            return retVal;
        }

        /// <summary>
        /// Concatena el sufijo "st,nd,rd,th" al número indicado.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static string NumberSuffix(int number)
        {
            string suffix = null;
            if ((number > 10) & (number < 20))
                suffix = "th";
            else
                switch (number % 10)
                {
                    case 1:
                        suffix = "st";
                        break;
                    case 2:
                        suffix = "nd";
                        break;
                    case 3:
                        suffix = "rd";
                        break;
                    default:
                        suffix = "th";
                        break;
                }

            return number + suffix;
        }


        /// <summary>
        /// Convierte la cadena a formato HTML.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string TextToHTML(string str)
        {
            string result = str.Replace("&", "&amp;");
            result = result.Replace(">", "&gt;");
            result = result.Replace("<", "&lt;");
            result = result.Replace(Convert.ToString(@""""), "&quot;");
            return result;
        }


        /// <summary>
        /// Camel case.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string CamelCase(string str)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string[] words = str.Split(' ');
            string camelCase = "";
            foreach (string word in words)
            {
                camelCase += textInfo.ToTitleCase(word);
            }
            camelCase = char.ToLowerInvariant(camelCase[0]) + camelCase.Substring(1);
            return camelCase;
        }


        /// <summary>
        /// Convierte el nombre completo separado con coma, a nombre completo.
        /// Ejemplo: Febrer, Juan Carlos  ->  Juan Carlos Febrer
        /// </summary>
        /// <param name="fullName">Full name of the reverse.</param>
        /// <returns></returns>
        public static string ConvertReverseFullName(string fullName)
        {
            string result = null;
            int i = 0;
            fullName = fullName.Trim();
            var comma = ",";
            i = fullName.IndexOf(comma) + 1;
            if (i == -1)
            {
                result = fullName;
            }
            else
            {
                result = fullName.Substring(Convert.ToInt32(i + 1)) + " " +
                                               fullName.Substring(0, i-1);
            }

            return result;
        }


        /// <summary>
        /// Instrs the after.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="search">The search.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static long InstrAfter(string str, string search, int index)
        {
            long result = 0;
            result = IndexOf(str, index, search);
            if (result >= 0) 
                result += search.Length;
            return result;
        }


        /// <summary>
        /// Instrs the last.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="Source">The source.</param>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        public static int InstrLast(int str, string Source, string search)
        {
            int result = 0;
            do
            {
                str = IndexOf(Source, str, search);
                if (str == -1) 
                    break;
                result = str;
                str++;
            } while (true);

            return result;
        }


        /// <summary>
        /// Permutes the string.
        /// </summary>
        /// <param name="Ztring">The ztring.</param>
        /// <param name="Base">The base.</param>
        /// <returns></returns>
        public static string PermuteString(string Ztring, string Base)
        {
            string result = "";
            string[] TmpStrArray = null;
            int i = 0;

            if (IndexOf(Ztring, 1, " ") == -1)
            {
                result = Base + " " + Ztring + "\r\n";
                return result;
            }

            TmpStrArray = Ztring.Split(' ');
            if (Base == "")
                for (i = TmpStrArray.GetLowerBound(0); i <= TmpStrArray.GetUpperBound(0); i++)
                    result = result +
                                          PermuteString(ReturnAllBut(TmpStrArray, i),
                                              TmpStrArray[Convert.ToInt32(i)]);
            else
                for (i = TmpStrArray.GetLowerBound(0); i <= TmpStrArray.GetUpperBound(0); i++)
                    result = result + " " +
                                          PermuteString(ReturnAllBut(TmpStrArray, i),
                                              Base + " " + TmpStrArray[Convert.ToInt32(i)]);
            return result;
        }


        /// <summary>
        /// Permutes the string.
        /// </summary>
        /// <param name="Ztring">The ztring.</param>
        /// <returns></returns>
        public static string PermuteString(string Ztring)
        {
            return PermuteString(Ztring, "");
        }


        /// <summary>
        /// Returns all but.
        /// </summary>
        /// <param name="arr">The arr.</param>
        /// <param name="But">The but.</param>
        /// <returns></returns>
        public static string ReturnAllBut(string[] arr, int But)
        {
            string result = "";
            for (int i = arr.GetLowerBound(0); i <= arr.GetUpperBound(0); i++)
                if (i != But)
                    result = result + arr[Convert.ToInt32(i)] + " ";
            result = result.TrimEnd();
            return result;
        }


        /// <summary>
        /// Devuelve una cadena aleatoria.
        /// </summary>
        /// <param name="mask">The mask.</param>
        /// <returns></returns>
        public static string RandomString(string mask)
        {
            string result = mask;
            string options = null;
            string c = "";

            var r = new Random();

            for (int i = 0; i < mask.Length; i++)
            {
                c = mask.Substring(i, 1);
                switch (c)
                {
                    case "?":
                        c = Convert.ToString(Convert.ToChar(Convert.ToInt32(1 + r.Next(127))));
                        options = "";
                        break;
                    case "#":
                        options = "0123456789";
                        break;
                    case "A":
                        options = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                        break;
                    case "N":
                        options = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0" + "123456789";
                        break;
                    case "H":
                        options = "0123456789ABCDEF";
                        break;
                    default:
                        options = "";
                        break;
                }

                if (options.Length > 0)
                    c = Substring(options + options.Substring(options.Length - 1),
                        Convert.ToInt32(1 +
                                        Math.Floor((double) r.Next(options.Length))),
                        1);

                result += c;
            }

            return result;
        }


        /// <summary>
        /// Replaces the last.
        /// </summary>
        /// <param name="Expression">The expression.</param>
        /// <param name="Find">The find.</param>
        /// <param name="ReplaceStr">The replace string.</param>
        /// <returns></returns>
        public static string ReplaceLast(string Expression, string Find, string ReplaceStr)
        {
            var i = 0;
            i = LastIndexOf(Expression, Find);
            if (i > 0)
                return Expression.Substring(0, i - 1) + Expression.Replace(Find, ReplaceStr);
            return Expression;
        }


        /// <summary>
        /// Reemplaza la cadena con los argumentos. En la cadena, los argumentos se especifican con @n.
        /// </summary>
        /// <param name="str">The text.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static string ReplaceArgs(string str, params string[] args)
        {
            for (int i = 0; i <= args.GetUpperBound(0); i++)
            {
                string value = args[i];
                str = Replace(str, "@" + i.ToString(), value);
            }

            return str;
        }


        /// <summary>
        /// Replaces the multi.
        /// </summary>
        /// <param name="str">The text.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="ExceptionUtil">El número de parametros deben ser pares.</exception>
        public static string ReplaceMulti(string str, params string[] args)
        {
            if (args.GetUpperBound(0) % 2 == 0) 
                throw new ExceptionUtil("El número de parametros deben ser pares.");
            for (int i = 0; i <= args.GetUpperBound(0); i += 2) 
                str = str.Replace(args[i], args[i + 1]);
            return str;
        }


        /// <summary>
        /// Uniques the words.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static ArrayList UniqueWords(string str)
        {
            ArrayList arrayResult = new ArrayList();
            string thisWord = null;
            long i = 0;
            long wordStart = 0;

            for (i = 0; i < str.Length; i++)
            {
                var selectVal = Convert.ToInt32(char.Parse(Substring(str, Convert.ToInt32(i), 1)));
                if (65 <= selectVal && selectVal <= 90 || 97 <= selectVal && selectVal <= 122)
                {
                    if (wordStart == 0) wordStart = i;
                }
                else if (48 <= selectVal && selectVal <= 57)
                {
                }
                else
                {
                    if (wordStart > 0)
                    {
                        thisWord =
                            Substring(Convert.ToString(wordStart), Convert.ToInt32(i - wordStart)).ToLower();
                        arrayResult.Add(thisWord);
                        wordStart = 0;
                    }
                }
            }

            if (wordStart > 0)
            {
                thisWord =
                    Substring(Convert.ToString(wordStart), Convert.ToInt32(i - wordStart)).ToLower();
                arrayResult.Add(thisWord);
            }

            return arrayResult;
        }


        /// <summary>
        /// Flips the case.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string FlipCase(string str)
        {
            int i = 0;
            var res = new StringBuilder(str.Length);
            for (i = 0; i <= str.Length - 1; i++)
                if (char.IsLower(str[i]))
                    res.Append(char.ToUpper(str[i]));
                else if (char.IsUpper(str[i]))
                    res.Append(char.ToLower(str[i]));
                else
                    res.Append(str[i]);
            return res.ToString();
        }


        /// <summary>
        /// Formats the value.
        /// </summary>
        /// <param name="str">The value.</param>
        /// <param name="width">The width.</param>
        /// <param name="alignment">The alignment.</param>
        /// <param name="padchar">The padchar.</param>
        /// <returns></returns>
        public static string FormatValue(string str, int width, FormatColumnAlignment alignment, char padchar)
        {
            string result = str;
            int len = result.Length;
            switch (alignment)
            {
                case FormatColumnAlignment.Left:
                    if (len < width)
                        result = result.PadRight(width, padchar);
                    else if (len > width) result = result.Substring(0, width);
                    break;
                case FormatColumnAlignment.Center:
                    if (len < width)
                    {
                        int charnum = len + (width - len) / 2;
                        result = result.PadLeft(charnum, padchar).PadRight(width, padchar);
                    }
                    else if (len > width)
                    {
                        result = result.Substring((len - width) / 2, width);
                    }

                    break;
                case FormatColumnAlignment.Right:
                    if (len < width)
                        result = result.PadLeft(width, padchar);
                    else if (len > width) result = result.Substring(len - width);
                    break;
            }

            return result;
        }


        /// <summary>
        /// Formats the value.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="width">The width.</param>
        /// <param name="alignment">The alignment.</param>
        /// <returns></returns>
        public static string FormatValue(string str, int width, FormatColumnAlignment alignment)
        {
            return FormatValue(str, width, alignment, '0');
        }


        /// <summary>
        /// Increments the string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string IncrementString(string str)
        {
            int index = 0;
            int i = 0;
            for (i = str.Length - 1; i >= 0; i += -1)
            {
                var selectVal = str.Substring(i, 1);
                if ("0" != selectVal && selectVal != "9")
                {
                    // <= <=
                }
                else
                {
                    index = i;
                    break;
                }
            }

            if (index == str.Length - 1) return str + "1";

            var value = int.Parse(str.Substring(index + 1)) + 1;
            return str.Substring(0, index) + value;
        }


        /// <summary>
        /// Instrs the table.
        /// </summary>
        /// <param name="Start">The start.</param>
        /// <param name="Source">The source.</param>
        /// <param name="Table">The table.</param>
        /// <param name="Include">if set to <c>true</c> [include].</param>
        /// <param name="CaseInsensitive">if set to <c>true</c> [case insensitive].</param>
        /// <returns></returns>
        public static int InstrTbl(int Start, string Source, string Table, bool Include, bool CaseInsensitive)
        {
            string pattern;
            if (Include)
                pattern = "[" + Table + "]";
            else
                pattern = "[^" + Table + "]";
            RegexOptions options = 0;
            if (CaseInsensitive) options = RegexOptions.IgnoreCase;
            var re = new Regex(pattern, options);

            var ma = re.Match(Source, Start);
            if (ma.Success)
                return ma.Index;
            return -1;
        }


        /// <summary>
        /// Instrs the table.
        /// </summary>
        /// <param name="Start">The start.</param>
        /// <param name="Source">The source.</param>
        /// <param name="Table">The table.</param>
        /// <returns></returns>
        public static int InstrTbl(int Start, string Source, string Table)
        {
            return InstrTbl(Start, Source, Table, true);
        }


        /// <summary>
        /// Instrs the table.
        /// </summary>
        /// <param name="Start">The start.</param>
        /// <param name="Source">The source.</param>
        /// <param name="Table">The table.</param>
        /// <param name="Include">if set to <c>true</c> [include].</param>
        /// <returns></returns>
        public static int InstrTbl(int Start, string Source, string Table, bool Include)
        {
            return InstrTbl(Start, Source, Table, Include, false);
        }


        /// <summary>
        /// Instrs the table rev.
        /// </summary>
        /// <param name="Start">The start.</param>
        /// <param name="Source">The source.</param>
        /// <param name="Table">The table.</param>
        /// <param name="Include">if set to <c>true</c> [include].</param>
        /// <param name="CaseInsensitive">if set to <c>true</c> [case insensitive].</param>
        /// <returns></returns>
        public static int InstrTblRev(int Start, string Source, string Table, bool Include, bool CaseInsensitive)
        {
            string pattern = null;
            if (Include)
                pattern = "[" + Table + "]";
            else
                pattern = "[^" + Table + "]";
            RegexOptions options = 0;
            if (CaseInsensitive) options = RegexOptions.IgnoreCase;
            var re = new Regex(pattern, options);

            Source = StrReverse(Source);
            if ((Start >= 0) & (Start < Source.Length))
                Start = Source.Length - Start;
            else
                Start = 0;
            var ma = re.Match(Source, Start);
            if (ma.Success)
                return Source.Length - ma.Index - 1;
            return -1;
        }

        /// <summary>
        /// Instrs the table rev.
        /// </summary>
        /// <param name="Start">The start.</param>
        /// <param name="Source">The source.</param>
        /// <param name="Table">The table.</param>
        /// <returns></returns>
        public static int InstrTblRev(int Start, string Source, string Table)
        {
            return InstrTblRev(Start, Source, Table, true);
        }


        /// <summary>
        /// Instrs the table rev.
        /// </summary>
        /// <param name="Start">The start.</param>
        /// <param name="Source">The source.</param>
        /// <param name="Table">The table.</param>
        /// <param name="Include">if set to <c>true</c> [include].</param>
        /// <returns></returns>
        public static int InstrTblRev(int Start, string Source, string Table, bool Include)
        {
            return InstrTblRev(Start, Source, Table, Include, false);
        }


        /// <summary>
        /// Searches the string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="search">The search.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns></returns>
        public static int SearchString(string source, string search, bool ignoreCase)
        {
            RegexOptions options = 0;
            if (ignoreCase)
                options = RegexOptions.IgnoreCase;
            else
                options = RegexOptions.None;

            var m = Regex.Match(source, search, options);
            if (m.Success)
                return m.Index;
            return -1;
        }


        /// <summary>
        /// Searches the string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        public static int SearchString(string source, string search)
        {
            return SearchString(source, search, false);
        }


        /// <summary>
        /// Searches the string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="search">The search.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns></returns>
        public static int SearchString(string source, int startIndex, string search, bool ignoreCase)
        {
            int i = SearchString(source.Substring(startIndex), search, ignoreCase);
            if (i == -1)
                return -1;
            return i + startIndex;
        }


        /// <summary>
        /// Searches the string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        public static int SearchString(string source, int startIndex, string search)
        {
            return SearchString(source, startIndex, search, false);
        }


        /// <summary>
        /// Startses the with.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="caseSensitive">if set to <c>true</c> [case sensitive].</param>
        /// <param name="parts">The parts.</param>
        /// <returns></returns>
        public static int StartsWith(string source, bool caseSensitive, params string[] parts)
        {
            for (int i = 0; i <= parts.Length - 1; i++)
            {
                var part = parts[i];
                if (caseSensitive)
                {
                    if (source.StartsWith(part)) return i;
                }
                else
                {
                    if (source.ToLower().StartsWith(part.ToLower())) return i;
                }
            }

            return -1;
        }


        /// <summary>
        /// Endses the with.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="caseSensitive">if set to <c>true</c> [case sensitive].</param>
        /// <param name="parts">The parts.</param>
        /// <returns></returns>
        public static int EndsWith(string source, bool caseSensitive, params string[] parts)
        {
            for (int i = 0; i <= parts.Length - 1; i++)
            {
                string part = parts[i];
                if (caseSensitive)
                {
                    if (source.EndsWith(part)) return i;
                }
                else
                {
                    if (source.ToLower().EndsWith(part.ToLower())) return i;
                }
            }

            return -1;
        }


        /// <summary>
        /// ps the case.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string PCase(string str)
        {
            var iPosition = 0;
            var iSpace = 0;
            var result = "";

            str = str.Trim();

            while (IndexOf(str, iPosition, " ") != -1)
            {
                iSpace = IndexOf(str, iPosition, " ");
                result = result + str.Substring(iPosition, 1).ToUpper();
                result = result + str.Substring(iPosition + 1, iSpace - iPosition).ToLower();
                iPosition = iSpace + 1;
            }

            result = result + str.Substring(iPosition, 1).ToUpper();
            result = result + str.Substring(iPosition + 1).ToLower();

            return result;
        }

        /// <summary>
        /// Eliminamos los espacios repetidos.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string RemoveRepeatSpaces(string str)
        {
            return Regex.Replace(str, @"\s+", " ");
        }

        /// <summary>
        /// Eliminamos el carácter repetido.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="chr">El caracter a procesar</param>
        /// <returns></returns>
        public static string RemoveRepeat(string str, char chr)
        {
            return Regex.Replace(str, @"[" + chr.ToString() + "]{2,}", chr.ToString());
        }

        /// <summary>
        /// Removes the initial ilegal chars.
        /// </summary>
        /// <param name="str">The data.</param>
        /// <returns></returns>
        public static string RemoveInitialIlegalChars(string str)
        {
            int pos = 0;

            for (int i = 0; i < str.Length - 1; i++)
            {
                string c = str.Substring(i, 1);

                if ("abcdefghijklmnñopqrstuvwxyz".IndexOf(c) >= 0)
                {
                    pos = i;
                    break;
                }
            }

            return str.Substring(pos);
        }

        /// <summary>
        /// Removes the specified initialize.
        /// </summary>
        /// <param name="init">The initialize.</param>
        /// <param name="end">The end.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static string Remove(string init, string end, string data)
        {
            var pattern = init + "(.*?)" + end;
            return Regex.Replace(data, pattern, "");
        }


        /// <summary>
        /// Converts to utf8.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string ToUTF8(string input)
        {
            //byte[] data = Encoding.Default.GetBytes(input);
            //return Encoding.UTF8.GetString(data);

            var iso = Encoding.GetEncoding("ISO-8859-1");
            var utf8 = Encoding.UTF8;
            var utfBytes = utf8.GetBytes(input);
            var isoBytes = Encoding.Convert(utf8, iso, utfBytes);
            return iso.GetString(isoBytes);
        }

        /// <summary>
        /// Converts to title case.
        /// </summary>
        /// <param name="str">The value.</param>
        /// <returns></returns>
        public static string ToTitleCase(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        /// Capitalizes the specified string.
        /// </summary>
        /// <param name="str">The value.</param>
        /// <returns></returns>
        public static string Capitalize(string str)
        {
            return ToTitleCase(str);
        }

        /// <summary>
        /// Pone en mayúscula el primer caracter de la cadena.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string ToCase(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            str = str.ToLower();

            return str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);
        }

        /// <summary>
        /// Removes the illegal character.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string RemoveIllegalChar(string path)
        {
            var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            path = r.Replace(path, "");

            return path;
        }

        /// <summary>
        ///     Comprueba si una cadena es "parecida" a otra
        ///     Ejemplos:
        ///     bool willBeTrue = Like("abcdefg","abcd_fg");
        ///     bool willAlsoBeTrue = Like("abcdefg","ab%f%");
        ///     bool willBeFalse = Like("abcdefghi","abcd_fg");
        /// </summary>
        /// <param name="toSearch"></param>
        /// <param name="toFind"></param>
        /// <returns></returns>
        public static bool Like(string toSearch, string toFind)
        {
            return new Regex(
                @"\A" + new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\").Replace(toFind, ch => @"\" + ch)
                    .Replace('_', '.').Replace("%", ".*") + @"\z", RegexOptions.Singleline).IsMatch(toSearch);
        }

        /// <summary>
        /// Num2s the spanish phrase.
        /// </summary>
        /// <param name="curNumero">The current numero.</param>
        /// <param name="blnO_Final">if set to <c>true</c> [BLN o final].</param>
        /// <returns></returns>
        public static string Num2SpanishPhrase(double curNumero, bool blnO_Final)
        {
            double dblCentimos = 0;
            long lngContDec = 0;
            long lngContCent = 0;
            long lngContMil = 0;
            long lngContMillon = 0;
            var strNumLetras = "";
            string[] strNumero =
            {
                "", "UN", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE",
                "DIEZ",
                "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE", "DIECISEIS", "DIECISIETE",
                "DIECIOCHO",
                "DIECINUEVE", "VEINTE"
            };
            string[] strDecenas =
            {
                "", "", "VEINTI", "TREINTA", "CUARENTA", "CINCUENTA", "SESENTA", "SETENTA",
                "OCHENTA", "NOVENTA", "CIEN"
            };
            string[] strCentenas =
            {
                "", "CIENTO", "DOSCIENTOS", "TRESCIENTOS", "CUATROCIENTOS", "QUINIENTOS",
                "SEISCIENTOS", "SETECIENTOS", "OCHOCIENTOS", "NOVECIENTOS"
            };
            var blnNegativo = false;
            var blnPlural = false;

            if (Math.Floor(curNumero) == 0.0D) strNumLetras = "CERO";

            if (curNumero < 0.0D)
            {
                blnNegativo = true;
                curNumero = Math.Abs(curNumero);
            }

            if (Math.Floor(curNumero) != curNumero)
            {
                var strDec = curNumero.ToString();
                var ingDecPos = strDec.LastIndexOf(",");
                var strDecNum = strDec.Substring(strDec.Length - ingDecPos - 1);

                dblCentimos = double.Parse(strDecNum);
                curNumero = Math.Floor(curNumero);
            }

            while (curNumero >= 1000000.0D)
            {
                lngContMillon = lngContMillon + 1;
                curNumero = curNumero - 1000000.0D;
            }

            while (curNumero >= 1000.0D)
            {
                lngContMil = lngContMil + 1;
                curNumero = curNumero - 1000.0D;
            }

            while (curNumero >= 100.0D)
            {
                lngContCent = lngContCent + 1;
                curNumero = curNumero - 100.0D;
            }

            if (!((curNumero > 10.0D) & (curNumero <= 20.0D)))
                while (curNumero >= 10.0D)
                {
                    lngContDec = lngContDec + 1;
                    curNumero = curNumero - 10.0D;
                }

            if (lngContMillon > 0)
            {
                if (lngContMillon >= 1)
                {
                    strNumLetras = Num2SpanishPhrase(lngContMillon, false);
                    if (!blnPlural) blnPlural = lngContMillon > 1;
                    lngContMillon = 0;
                }

                strNumLetras = strNumLetras.Trim() + strNumero[Convert.ToInt32(lngContMillon)] + " MILLON" +
                               (blnPlural ? "ES " : " ");
            }

            if (lngContMil > 0)
            {
                if (lngContMil >= 1)
                {
                    strNumLetras = strNumLetras + Num2SpanishPhrase(lngContMil, false);
                    lngContMil = 0;
                }

                strNumLetras = strNumLetras.Trim() + strNumero[Convert.ToInt32(lngContMil)] + " MIL ";
            }

            if (lngContCent > 0)
            {
                if ((lngContCent == 1) & (lngContDec == 0) & (curNumero == 0.0D))
                    strNumLetras = strNumLetras + "CIEN";
                else
                    strNumLetras = strNumLetras + strCentenas[Convert.ToInt32(lngContCent)] + " ";
            }

            if (lngContDec >= 1)
            {
                if (lngContDec == 1)
                    strNumLetras = strNumLetras + strNumero[10];
                else
                    strNumLetras = strNumLetras + strDecenas[Convert.ToInt32(lngContDec)];

                if ((lngContDec >= 3) & (curNumero > 0.0D)) strNumLetras = strNumLetras + " Y ";
            }
            else
            {
                if ((curNumero >= 0.0D) & (curNumero <= 20.0D))
                {
                    strNumLetras = strNumLetras + strNumero[Convert.ToInt32(curNumero)];
                    if ((curNumero == 1.0D) & blnO_Final) strNumLetras = strNumLetras + "O";
                    if (dblCentimos > 0.0D)
                        strNumLetras = strNumLetras.Trim() + " CON " + Num2SpanishPhrase(dblCentimos);
                    return strNumLetras;
                }
            }

            if (curNumero > 0.0D)
            {
                strNumLetras = strNumLetras + strNumero[Convert.ToInt32(curNumero)];
                if ((curNumero == 1.0D) & blnO_Final) strNumLetras = strNumLetras + "O";
            }

            if (dblCentimos > 0.0D) strNumLetras = strNumLetras + " CON " + Num2SpanishPhrase(dblCentimos);

            return blnNegativo ? "(" + strNumLetras + ")" : strNumLetras;
        }


        /// <summary>
        /// Num2s the spanish phrase.
        /// </summary>
        /// <param name="curNumero">The current numero.</param>
        /// <returns></returns>
        public static string Num2SpanishPhrase(double curNumero)
        {
            return Num2SpanishPhrase(curNumero, true);
        }


        /// <summary>
        /// Pads the left.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="lon">The lon.</param>
        /// <param name="car">The car.</param>
        /// <returns></returns>
        public static string PadLeft(double value, int lon, char car)
        {
            var s = Convert.ToString(value);
            return s.PadLeft(lon, car);
        }


        /// <summary>
        /// Pads the left.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="lon">The lon.</param>
        /// <returns></returns>
        public string PadLeft(double value, int lon)
        {
            return PadLeft(value, lon, ' ');
        }


        /// <summary>
        /// Pads the right.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="lon">The lon.</param>
        /// <param name="car">The car.</param>
        /// <returns></returns>
        public static string PadRight(double value, int lon, char car)
        {
            var c = Convert.ToString(value);
            return c.PadRight(lon, car);
        }


        /// <summary>
        /// Pads the right.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="lon">The lon.</param>
        /// <returns></returns>
        public static string PadRight(double value, int lon)
        {
            return PadRight(value, lon, ' ');
        }


        /// <summary>
        /// Strings the car.
        /// </summary>
        /// <param name="car">The car.</param>
        /// <param name="lon">The lon.</param>
        /// <returns></returns>
        public static string StringCar(char car, int lon)
        {
            var s = car.ToString();
            return s.PadRight(lon - 1, car);
        }

        /// <summary>
        /// Strings the reverse.
        /// </summary>
        /// <param name="cadena">The cadena.</param>
        /// <returns></returns>
        public static string StrReverse(string cadena)
        {
            var charArray = cadena.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// Strings the dup.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="lon">The lon.</param>
        /// <returns></returns>
        public static string StrDup(char c, int lon)
        {
            return new string(c, lon);
        }

        /// <summary>
        /// Determines whether the specified input email is email.
        /// </summary>
        /// <param name="inputEmail">The input email.</param>
        /// <returns>
        ///   <c>true</c> if the specified input email is email; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmail(string inputEmail)
        {
            var strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var rs = new Regex(strRegex);
            if (rs.IsMatch(inputEmail))
                return true;
            return false;
        }


        /// <summary>
        /// Removes the HTML tags.
        /// </summary>
        /// <param name="html">The html string.</param>
        /// <returns></returns>
        public static string RemoveHtmlTags(string html)
        {
            var array = new char[html.Length];
            var arrayIndex = 0;
            var inside = false;

            for (var i = 0; i < html.Length; i++)
            {
                var let = html[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }

                if (let == '>')
                {
                    inside = false;
                    continue;
                }

                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }

            return new string(array, 0, arrayIndex);
        }

        /// <summary>
        /// Removes the expression signals.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string RemoveExpressionSignals(string str)
        {
            str = str.Replace(".", " ");
            str = str.Replace("!", " ");
            str = str.Replace("?", " ");
            str = str.Replace(",", " ");

            return str;
        }

        /// <summary>
        /// Devuelve la cadena entrecomillada
        /// </summary>
        /// <param name="str">Cadena a entrecomillar</param>
        /// <param name="protect">Protege las comillas internas.</param>
        /// <returns></returns>
        public static string AddQuotes(string str, bool protect = false)
        {
            if (HasQuotes(str))
                return str;

            if (protect)
                str = str.Replace("\"", PROTECT_QUOTE);

            return string.Format("\"{0}\"", str);
        }

        /// <summary>
        /// Devuelve true/false si la cadena contiene comillas al comienzo y fin de la cadena.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasQuotes(string str)
        {
            return str.StartsWith("\"") && str.EndsWith("\"");
        }

        /// <summary>
        /// Devuelve true/false si la cadena contiene comillas protegidas
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasProtectQuotes(string str)
        {
            return str.Contains(TextUtil.PROTECT_QUOTE);
        }

        /// <summary>
        /// Elimina las comillas externas de una cadena.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="removeProtectQuotes">Dejamos las comillas internas correctamente.</param>
        /// <returns></returns>
        public static string RemoveQuotes(string str, bool removeProtectQuotes = false)
        {
            //str = str.Trim('"');
            str = Regex.Replace(str, "^\"(.*)\"$", "$1", RegexOptions.Singleline);

            // Dejamos las comillas internas correctas
            if (removeProtectQuotes)
                str = str.Replace(PROTECT_QUOTE, "\"");

            return str;
        }

        /// <summary>
        /// Lasts the index of.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int LastIndexOf(string str, string value)
        {
            if (String.IsNullOrEmpty(str))
                return 0;
            return str.LastIndexOf(value, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Lasts the index of.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="start">The start.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int LastIndexOf(string str, int start, string value)
        {
            if (String.IsNullOrEmpty(str))
                return 0;
            return str.LastIndexOf(value, start, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Devuelve la posicion del valor en la cadena.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int IndexOf(string str, string value)
        {
            if (String.IsNullOrEmpty(str))
                return 0;
            return str.IndexOf(value, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Devuelve la posicion del valor en la cadena empezando en la posicion star.
        /// </summary>
        /// <param name="str">The cadena.</param>
        /// <param name="start">The start.</param>
        /// <param name="value">The valor.</param>
        /// <returns></returns>
        public static int IndexOf(string str, int start, string value)
        {
            if (String.IsNullOrEmpty(str))
                return 0;
            return str.IndexOf(value, start, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Substrings the specified cadena.
        /// </summary>
        /// <param name="str">The cadena.</param>
        /// <param name="start">The start.</param>
        /// <returns></returns>
        public static string Substring(string str, int start)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;
            return str.Substring(start);
        }


        /// <summary>
        /// Substrings the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="start">The start.</param>
        /// <param name="lon">The lon.</param>
        /// <returns></returns>
        public static string Substring(string str, int start, int lon)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;
            if (lon <= 0)
                return str;
            if (str.Length <= start)
                return str;
            if (str.Length > lon - start)
                return str.Substring(start, lon);
            return str;
        }

        /// <summary>
        /// Devuelve los caracteres indicado en lon por la izquierda.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="lon">The lon.</param>
        /// <returns></returns>
        public static string Left(string str, int lon)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;
            if (lon > str.Length)
                return str;
            return str.Substring(0, lon);
        }

        /// <summary>
        /// Devuelve los caracteres indicado en lon por la derecha.
        /// </summary>
        /// <param name="cadena">The cadena.</param>
        /// <param name="lon">The lon.</param>
        /// <returns></returns>
        public static string Right(string cadena, int lon)
        {
            if (cadena == null)
                return string.Empty;
            if (lon > cadena.Length)
                return cadena;
            return cadena.Substring(cadena.Length - lon, lon);
        }


        /// <summary>
        /// Translates the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public static string Translate(string str, string from, string to)
        {
            from = from.Replace("@", "");
            to = to.Replace("@", "");
            for (int i = 0; i <= from.Length - 1; i++)
            {
                string c = from.Substring(i, 1);
                string t = "";
                if (to.Length > i)
                    t = to.Substring(i, 1);
                str = str.Replace(c, t);
            }

            return str;
        }


        /// <summary>
        /// Replaces the specified string old value with new value.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        public static string Replace(string str, string oldValue, string newValue)
        {
            var sb = new StringBuilder();

            sb.Append(str);

            var index = sb.ToString().IndexOf(oldValue, StringComparison.OrdinalIgnoreCase);
            var indexReplacement = 0;
            if (newValue != null)
                indexReplacement = newValue.IndexOf(oldValue, StringComparison.OrdinalIgnoreCase);

            var lengthToRemove = oldValue.Length;

            if (indexReplacement > 0)
                return sb.ToString().Replace(sb.ToString().Substring(index, lengthToRemove), newValue);

            while (index != -1)
            {
                var foundIt = sb.ToString().Substring(index, lengthToRemove);
                var temp = sb.ToString();
                if (foundIt != "") temp = temp.Replace(foundIt, newValue);

                sb.Remove(0, sb.Length);
                sb.Append(temp);

                var lastIndex = index;
                index = sb.ToString().IndexOf(oldValue, StringComparison.OrdinalIgnoreCase);

                if (lastIndex == index) index = -1;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Replaces the v2.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        public static string Replace_v2(string str, string oldValue, string newValue)
        {
            if (str == null)
                return string.Empty;
            if (newValue == null)
                return str;
            if (oldValue == null)
                return str;
            if (str.IndexOf(oldValue, StringComparison.CurrentCultureIgnoreCase) == -1)
                return str;
            int count, position0, position1;
            count = position0 = position1 = 0;
            string upperString = str.ToUpper();
            string upperPattern = oldValue.ToUpper();
            int inc = str.Length / oldValue.Length *
                      (newValue.Length - oldValue.Length);
            char[] chars = new char[str.Length + Math.Max(0, inc)];
            while ((position1 = upperString.IndexOf(upperPattern,
                       position0)) != -1)
            {
                for (var i = position0; i < position1; ++i)
                    chars[count++] = str[i];
                for (var i = 0; i < newValue.Length; ++i)
                    chars[count++] = newValue[i];
                position0 = position1 + oldValue.Length;
            }

            if (position0 == 0)
                return str;
            for (var i = position0; i < str.Length; ++i)
                chars[count++] = str[i];
            return new string(chars, 0, count);
        }


        /// <summary>
        /// Añade ceros por la parte derecha.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static string AdZero(string str, int size)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;
            return PadRight(str, size, '0');
        }

        /// <summary>
        /// Replaces recursive.
        /// </summary>
        /// <param name="str">The cadena.</param>
        /// <param name="find">The find.</param>
        /// <param name="replaceChar">The replace char.</param>
        /// <returns></returns>
        public static string ReplaceRecursive(string str, string find, string replaceChar)
        {
            while (str.Contains(find))
                str = Regex.Replace(str, find, replaceChar);
            return str;
        }


        /// <summary>
        /// Replaces the reg.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="regex">The regex.</param>
        /// <param name="regreplace">The regreplace.</param>
        /// <returns></returns>
        public static string ReplaceREG(string str, string regex, string regreplace)
        {
            return ReplaceREG(str, regex, regreplace, RegexOptions.Multiline);
        }

        /// <summary>
        /// Replaces the reg.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="regex">The regex.</param>
        /// <param name="regreplace">The regreplace.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static string ReplaceREG(string str, string regex, string regreplace, RegexOptions options)
        {
            var oReg = new Regex(regex, options);
            var oMatch = oReg.Match(str);
            if (oMatch.Success) return oReg.Replace(str, regreplace);
            return str;
        }


        /// <summary>
        /// Replaces the recursive regex.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="regex">The regex.</param>
        /// <param name="regreplace">The regreplace.</param>
        /// <returns></returns>
        public static string ReplaceRecursiveREG(string str, string regex, string regreplace)
        {
            if (str == null)
                return string.Empty;
            if (regreplace == null)
                return str;
            if (regex == null)
                return str;
            if (str.IndexOf(regex, StringComparison.CurrentCultureIgnoreCase) == -1)
                return str;
            string repl = str;
            while (repl.IndexOf(regex) >= 0)
                repl = Regex.Replace(str, Regex.Escape(regex), regreplace, RegexOptions.IgnoreCase);
            return repl;
        }

        /// <summary>
        /// Gets the head HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string GetHeadHtml(string html)
        {
            var reg = new Regex(@"<head[^<>]*>(?<content>.*?)</head>",
                RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            var match = reg.Match(html);
            if (match.Success)
                return match.Groups["content"].Value;

            return string.Empty;
        }

        /// <summary>
        /// Gets the div HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string GetDivHtml(string html, string name)
        {
            var reg = new Regex(@"<div[^<>]*class=""" + name + @"""[^<>]*>(?<content>.*?)</div>",
                RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            var match = reg.Match(html);
            if (match.Success)
                return match.Groups["content"].Value;

            return string.Empty;
        }

        /// <summary>
        /// Removes the links.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string RemoveLinks(string html)
        {
            return ReplaceREG(html, "<a[^>]*>(.*?)</a>", "$1");
        }


        /// <summary>
        /// Cambia los tags "th" por "td"
        /// </summary>
        /// <param name="pdfData"></param>
        /// <returns></returns>
        public static string ChangeTH_TD(string pdfData)
        {
            string result = pdfData;
            result = ReplaceREG(result, "<th(.*?)>", "<th>");
            result = Replace(result, "<th>", "<td><b>");
            result = Replace(result, "</th>", "</b></td>");

            return result;
        }


        /// <summary>
        /// Ponemos el border=0 o border=1 en la tabla.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="border">Border true/false.</param>
        /// <returns></returns>
        public static string SetTableBorder(string html, bool border)
        {
            // eliminamos los "border", existentes
            html = ReplaceREG(html, " border(.*?) ", " ");

            if (border)
                return ReplaceREG(html, "<table (.*?)>", "<table border=1 $1>");
            else
                return ReplaceREG(html, "<table (.*?)>", "<table border=0 $1>");
        }

        /// <summary>
        /// Removes the styles.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string RemoveStyles(string html)
        {
            var reg = new Regex(@"(style=[^""'][^>]*)|(style=""[^""]*"")|(style='[^']*')");
            return reg.Replace(html, "");
        }


        /// <summary>
        /// Elimina los comentarios de un código.
        /// </summary>
        /// <param name="code">Código a analizar.</param>
        /// <returns></returns>
        public static string RemoveComments(string code)
        {
            var re = @"(@(?:""[^""]*"")+|""(?:[^""\n\\]+|\\.)*""|'(?:[^'\n\\]+|\\.)*')|//.*|/\*(?s:.*?)\*/";
            return Regex.Replace(code, re, "$1");
        }

        /// <summary>
        /// Gets the word count.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static long GetWordCount(string str)
        {
            var i = 0;
            var nLength = str.Length;
            long nWordCount = 0;

            if (!char.IsWhiteSpace(str[0])) nWordCount = nWordCount + 1;

            for (i = 0; i <= nLength - 1; i += i + 1)
                if (char.IsWhiteSpace(str[i]))
                    do
                    {
                        i = i + 1;
                        if (i >= nLength) break;
                        if (!char.IsWhiteSpace(str[i]))
                        {
                            nWordCount = nWordCount + 1;
                            break;
                        }
                    } while (true);

            return nWordCount;
        }


        /// <summary>
        /// Gets the word number.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public static string GetWordNumber(string str, int position)
        {
            var nBeginPos = TextUtil.LastIndexOf(str, position - 1, " ") + 1;
            var nEndPos = TextUtil.LastIndexOf(str, position, " ") + 1;
            return str.Substring(nBeginPos + 1, nEndPos - 1 - nBeginPos);
        }


        /// <summary>
        /// Count the lines in string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static int LinesInString(string str)
        {
            if (String.IsNullOrEmpty(str))
                return 0;
            return StringCount("\t", str) + 1;
        }


        /// <summary>
        /// Gets the line from string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="position">The tn line no.</param>
        /// <returns></returns>
        public static string GetLineFromString(string str, int position)
        {
            string[] lines = str.Split(TextUtil.ControlChars.Tab);
            string result = "";
            
            if(position <= lines.Length)
                result = lines[position];

            return result;
        }


        /// <summary>
        /// Cuenta el cuantos caracterés chr hay en la cadena.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="chr">The tc character.</param>
        /// <returns></returns>
        public static int StringCount(string str, char chr)
        {
            int nOccured = 0;

            for (int i = 0; i <= str.Length - 1; i += i + 1)
                if (str[i] == chr)
                    nOccured = nOccured + 1;
            return nOccured;
        }


        /// <summary>
        /// Strings the count.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static int StringCount(string str, string expression)
        {
            int pos = 0;
            var totalChr = 0;
            do
            {
                pos = expression.IndexOf(str, pos);

                if (pos < 0) 
                    break;

                totalChr++;
                pos++;
            } while (true);

            return totalChr;
        }


        /// <summary>
        /// Pads the center.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static string PadCenter(string str, int size)
        {
            int nPaddTotal = size - str.Length;
            int lnHalfLength = Convert.ToInt32(nPaddTotal / 2);

            string lcString = PadLeft(str, str.Length + lnHalfLength);
            return lcString.PadRight(size);
        }


        /// <summary>
        /// Pads the center.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="size">The size.</param>
        /// <param name="chr">The character.</param>
        /// <returns></returns>
        public static string PadCenter(string str, int size, char chr)
        {
            int nPaddTotal = size - str.Length;
            int lnHalfLength = Convert.ToInt32(nPaddTotal / 2);

            string lcString = PadLeft(str, str.Length + lnHalfLength, chr);
            return lcString.PadRight(size, chr);
        }


        /// <summary>
        /// Pads the left.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static string PadLeft(string str, int size)
        {
            return str.PadLeft(size);
        }


        /// <summary>
        /// Pads the left.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="size">The size.</param>
        /// <param name="chr">The character.</param>
        /// <returns></returns>
        public static string PadLeft(string str, int size, char chr)
        {
            return str.PadLeft(size, chr);
        }


        /// <summary>
        /// Pads the right.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static string PadRight(string str, int size)
        {
            return str.PadRight(size);
        }


        /// <summary>
        /// Pads the right.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="size">The size.</param>
        /// <param name="chr">The character.</param>
        /// <returns></returns>
        public static string PadRight(string str, int size, char chr)
        {
            return str.PadRight(size, chr);
        }


        /// <summary>
        /// Propers the case.
        /// </summary>
        /// <param name="cString">The c string.</param>
        /// <returns></returns>
        public static string ProperCase(string cString)
        {
            StringBuilder sb = new StringBuilder(cString.ToLower());

            int j = 0;
            int nLength = cString.Length;

            for (int i = 0; i <= nLength - 1; i += i + 1)
                if ((i == 0) | char.IsWhiteSpace(cString[i]))
                {
                    if (i == 0)
                        j = i;
                    else
                        j = i + 1;

                    sb.Remove(j, 1);
                    sb.Insert(j, Convert.ToString(char.ToUpper(cString[j])));
                }

            return sb.ToString();
        }

        /// <summary>
        /// Strings the extract.
        /// </summary>
        /// <param name="cSearchExpression">The c search expression.</param>
        /// <param name="cBeginDelim">The c begin delimiter.</param>
        /// <param name="cEndDelim">The c end delimiter.</param>
        /// <param name="nBeginOccurence">The n begin occurence.</param>
        /// <param name="nFlags">The n flags.</param>
        /// <returns></returns>
        public static string StringExtract(string cSearchExpression, string cBeginDelim, string cEndDelim,
            int nBeginOccurence, int nFlags)
        {
            var cstring = cSearchExpression;
            var cb = cBeginDelim;
            var ce = cEndDelim;
            var lcRetVal = "";

            if (nFlags == 1)
            {
                cstring = cstring.ToLower();
                cb = cb.ToLower();
                ce = ce.ToLower();
            }

            var nbpos = TextUtil.LastIndexOf(cstring, nBeginOccurence, cb) + 1;
            var nepos = cstring.IndexOf(ce, nbpos + 1);

            if (nepos > nbpos) lcRetVal = cSearchExpression.Substring(nbpos, nepos - nbpos);

            return lcRetVal;
        }


        /// <summary>
        /// Strings the extract.
        /// </summary>
        /// <param name="cSearchExpression">The c search expression.</param>
        /// <param name="cBeginDelim">The c begin delimiter.</param>
        /// <returns></returns>
        public static string StringExtract(string cSearchExpression, string cBeginDelim)
        {
            var nbpos = TextUtil.LastIndexOf(cSearchExpression, cBeginDelim) + 1;
            return cSearchExpression.Substring(nbpos);
        }


        /// <summary>
        /// Strings the extract.
        /// </summary>
        /// <param name="cSearchExpression">The c search expression.</param>
        /// <param name="cBeginDelim">The c begin delimiter.</param>
        /// <param name="cEndDelim">The c end delimiter.</param>
        /// <returns></returns>
        public static string StringExtract(string cSearchExpression, string cBeginDelim, string cEndDelim)
        {
            return StringExtract(cSearchExpression, cBeginDelim, cEndDelim, 1, 0);
        }


        /// <summary>
        /// Strings the extract.
        /// </summary>
        /// <param name="cSearchExpression">The c search expression.</param>
        /// <param name="cBeginDelim">The c begin delimiter.</param>
        /// <param name="cEndDelim">The c end delimiter.</param>
        /// <param name="nBeginOccurence">The n begin occurence.</param>
        /// <returns></returns>
        public static string StringExtract(string cSearchExpression, string cBeginDelim, string cEndDelim,
            int nBeginOccurence)
        {
            return StringExtract(cSearchExpression, cBeginDelim, cEndDelim, nBeginOccurence, 0);
        }

        /// <summary>
        /// Stuffs the string.
        /// </summary>
        /// <param name="cExpression">The c expression.</param>
        /// <param name="nStartReplacement">The n start replacement.</param>
        /// <param name="nCharactersReplaced">The n characters replaced.</param>
        /// <param name="cReplacement">The c replacement.</param>
        /// <returns></returns>
        public static string StuffString(string cExpression, int nStartReplacement, int nCharactersReplaced,
            string cReplacement)
        {
            var sb = new StringBuilder(cExpression);

            if (nCharactersReplaced != 0) sb.Remove(nStartReplacement - 1, nCharactersReplaced);

            sb.Insert(nStartReplacement - 1, cReplacement);
            return sb.ToString();
        }

        /// <summary>
        /// Sustituye los códigos hexadecimales del tipo =E9 a su correspondiente ASCII.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string ConvertHexToAscii(string str)
        {
            Regex occurences = new Regex(@"=[0-9A-H]{2}", RegexOptions.Multiline);
            MatchCollection matches = occurences.Matches(str);
            foreach (Match match in matches)
            {
                char hexChar = (char)Convert.ToInt32(match.Value.Substring(1), 16);
                str = str.Replace(match.Value, hexChar.ToString());
            }
            return str.Replace("=\r\n", "");
        }

        /// <summary>
        /// Códigos de escape de carácteres utilizados en VB
        /// </summary>
        public static class ControlChars
        {
            /// <summary>
            /// The back
            /// </summary>
            public const char Back = '\b';
            /// <summary>
            /// The cr
            /// </summary>
            public const char Cr = '\r';
            /// <summary>
            /// The cr lf
            /// </summary>
            public const string CrLf = "\r\n";
            /// <summary>
            /// The form feed
            /// </summary>
            public const char FormFeed = '\f';
            /// <summary>
            /// The lf
            /// </summary>
            public const char Lf = '\n';
            /// <summary>
            /// Creates new line.
            /// </summary>
            public const string NewLine = "\r\n";
            /// <summary>
            /// The null character
            /// </summary>
            public const char NullChar = '\0';
            /// <summary>
            /// The quote
            /// </summary>
            public const char Quote = '"';
            /// <summary>
            /// The tab
            /// </summary>
            public const char Tab = '\t';
            /// <summary>
            /// The vertical tab
            /// </summary>
            public const char VerticalTab = '\v';
        }

        /// <summary>
        /// Cuenta el numero de ocurrencias de una cadena en otra
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static int CountStringREG(string str, string pattern)
        {
            int tot = Regex.Matches(str, pattern).Count;
            return tot;
        }

        /// <summary>
        /// Busca las direcciones URL en un texto
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IEnumerable<string> SearchUrlValues(string text)
        {
            Regex regex = new Regex("<a [^>]*href=(?:'(?<href>.*?)')|(?:\"(?<href>.*?)\")", RegexOptions.IgnoreCase);
            return regex.Matches(text).OfType<Match>().Select(m => m.Groups["href"].Value);
        }

        /// <summary>
        /// Busca los valores numéricos en un texto
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IEnumerable<string> SearchNumericValues(string text)
        {
            Regex regex = new Regex(@"(?<number>\d+(\,\d+)?)"); //^\d+$)");
            return regex.Matches(text).OfType<Match>().Select(m => m.Groups["number"].Value);
        }

        /// <summary>
        /// Busca las fechas en un texto
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IEnumerable<string> SearchDateValues(string text)
        {
            Regex regex = new Regex(@"(?<date>\d{1,2}\/\d{1,2}\/\d{4})");
            return regex.Matches(text).OfType<Match>().Select(m => m.Groups["date"].Value);
        }

        /// <summary>
        /// Busca las direcciones IP en un texto
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IEnumerable<string> SearchIpValues(string text)
        {
            Regex regex = new Regex(@"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})");
            return regex.Matches(text).OfType<Match>().Select(m => m.Groups["ip"].Value);
        }

        /// <summary>
        /// Divide en partes de cadenas de longitud partLength.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="partLength"></param>
        /// <returns></returns>
        public static IEnumerable<String> SplitInParts(String s, int partLength)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        /// <summary>
        /// Une un array de cadenas utilizando el separador indicado.
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="parts"></param>
        /// <returns></returns>
        public static string JoinParts(char separator, IEnumerable<String> parts)
        {
            return String.Join(separator.ToString(), parts);
        }


        /// <summary>
        /// Aplica las variables a la expresión sustituyendo por los valores de forma recursiva.
        /// </summary>
        /// <param name="expression">Expresión a evaluar y sustituir por las variables.</param>
        /// <param name="variables">Diccionario de variables con su valor.</param>
        // <returns></returns>
        public static string ApplyVariables(string expression, Dictionary<string, object> variables)
        {
            string lastExpression;

                if (variables == null)
                    return expression;

            do
            {
                //if (textMark != null && expression.Contains(textMark))
                //    return expression;

                lastExpression = expression; // Guarda la expresión antes del reemplazo

                // Construir patrón para detectar variables
                string pattern = $@"\b({string.Join("|", variables.Keys.Select(Regex.Escape))})\b";

                // Usar StringBuilder para construir la nueva expresión
                StringBuilder result = new StringBuilder();
                int lastIndex = 0;

                foreach (Match match in Regex.Matches(expression, pattern))
                {
                    // Contar cuántas comillas hay antes de la coincidencia
                    string beforeMatch = expression.Substring(0, match.Index);
                    int doubleQuotesCount = beforeMatch.Count(c => c == '"');
                    int singleQuotesCount = beforeMatch.Count(c => c == '\'');

                    // Si el número de comillas es impar, significa que está dentro de una cadena y no hacemos nada.
                    bool insideDoubleQuotes = doubleQuotesCount % 2 != 0;
                    bool insideSingleQuotes = singleQuotesCount % 2 != 0;

                    // Solo reemplazar si NO está dentro de comillas
                    if (!insideDoubleQuotes && !insideSingleQuotes)
                    {
                        result.Append(expression.Substring(lastIndex, match.Index - lastIndex)); // Agregar parte anterior
                        string variableValue = variables[match.Value].ToString();

                        // Si el valor no es una variable de memoria y no está entrecomillado, aplicamos transformaciones
                        if (!Regex.Match(variableValue, pattern).Success)
                        {
                            if (NumberUtils.IsNumeric(variableValue))
                                variableValue = variableValue.Replace(",", "."); // Asegura que los números sean válidos con punto como separador decimal.
                            else if (!(variableValue.StartsWith("\"") && variableValue.EndsWith("\"")))
                                variableValue = $"\"{variableValue}\""; // Si no es un número, lo convierte a string entre comillas.
                        }

                        result.Append(variableValue);
                    }
                    else
                    {
                        result.Append(expression.Substring(lastIndex, match.Index - lastIndex + match.Length)); // Agregar sin reemplazar
                    }

                    lastIndex = match.Index + match.Length;
                }

                // Agregar el resto de la expresión
                result.Append(expression.Substring(lastIndex));
                expression = result.ToString();

            } while (!expression.Equals(lastExpression)); // Repetir hasta que la expresión ya no cambie

            return expression;
        }
    }
}