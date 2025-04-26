using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace FSCrypto
{
    public class Base64
    {
        /// <summary>
        /// Encodes the base64.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string Encode(string str)
        {
            return Encode(str, true, true);
        }

        /// <summary>
        /// Encodes the base64.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="urlSafe">if set to <c>true</c> [URL safe].</param>
        /// <returns></returns>
        public static string Encode(string str, bool urlSafe)
        {
            return Encode(str, urlSafe, false);
        }

        /// <summary>
        /// Encodes the base64.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="urlSafe">if set to <c>true</c> [URL safe].</param>
        /// <returns></returns>
        public static string Encode(string str, bool urlSafe, bool withOutEqualSign)
        {
            byte[] strBytes = Encoding.UTF8.GetBytes(str);
            string output = Convert.ToBase64String(strBytes);

            if (urlSafe)
                output = output.Replace('+', '-').Replace('/', '_');

            if(withOutEqualSign)
                output = output.TrimEnd('=');

            return output;
        }


        /// <summary>
        /// Decodes the base64.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string Decode(string input)
        {
            return Decode(input, true, true);
        }

        /// <summary>
        /// Decodes the base64.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="urlSafe">if set to <c>true</c> [URL safe].</param>
        /// <returns></returns>
        public static string Decode(string input, bool urlSafe)
        {
            return Decode(input, urlSafe, false);
        }

        /// <summary>
        /// Decodes the base64.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="urlSafe">if set to <c>true</c> [URL safe].</param>
        /// <returns></returns>
        public static string Decode(string input, bool urlSafe, bool withOutEqualSign)
        {
            if (urlSafe)
                input = input.Replace('-', '+').Replace('_', '/');

            if (withOutEqualSign && !input.EndsWith("="))
            {
                switch (input.Length % 4)
                {
                    case 2:
                        input += "==";
                        break;
                    case 3:
                        input += "=";
                        break;
                }
            }

            byte[] strBytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(strBytes);
        }
    }
}
