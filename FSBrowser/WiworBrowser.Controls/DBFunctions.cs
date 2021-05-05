using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WiworBrowser.Controls
{
    public static class DBFunctions
    {
        public static string Replace(string original, string pattern, string replacement)
        {
            if (original == null) return "";
            if (replacement == null) return original;
            int count, position0, position1;
            count = position0 = position1 = 0;
            string upperString = original.ToUpper();
            string upperPattern = pattern.ToUpper();
            int inc = (original.Length / pattern.Length) *
                      (replacement.Length - pattern.Length);
            char[] chars = new char[original.Length + Math.Max(0, inc)];
            while ((position1 = upperString.IndexOf(upperPattern,
                                                    position0)) != -1)
            {
                for (int i = position0; i < position1; ++i)
                    chars[count++] = original[i];
                for (int i = 0; i < replacement.Length; ++i)
                    chars[count++] = replacement[i];
                position0 = position1 + pattern.Length;
            }
            if (position0 == 0) return original;
            for (int i = position0; i < original.Length; ++i)
                chars[count++] = original[i];
            return new string(chars, 0, count);
        }

        public static int InStr(string cadena, string valor)
        {
            if (cadena == null) return 0;
            return cadena.IndexOf(valor, StringComparison.CurrentCultureIgnoreCase);
        }

        public static int InStr(string cadena, int start, string valor)
        {
            if (cadena == null) return 0;
            return cadena.IndexOf(valor, start, StringComparison.CurrentCultureIgnoreCase);
        }

        public static string RemoveExpressionSignals(string data)
        {
            data = data.Replace(".", " ");
            data = data.Replace("!", " ");
            data = data.Replace("?", " ");
            data = data.Replace(",", " ");

            return data;
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string RemoveHtmlTags(string html)
        {
            char[] array = new char[html.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < html.Length; i++)
            {
                char let = html[i];
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

        public static bool IsUri(string s)
        {
            try
            {
                new Uri(s);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsUrl(string Url)
        {
            string strRegex = "^(https?://)"
                              + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@ 
                              + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184 
                              + "|" // allows either IP or domain 
                              + @"([0-9a-z_!~*'()-]+\.)*" // tertiary domain(s)- www. 
                              + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\." // second level domain 
                              + "[a-z]{2,6})" // first level domain- .com or .museum 
                              + "(:[0-9]{1,4})?" // port number- :80 
                              + "((/?)|" // a slash isn't required if there is no file name 
                              + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
            Regex re = new Regex(strRegex);

            if (re.IsMatch(Url))
                return (true);
            else
                return (false);
        }
    }
}
