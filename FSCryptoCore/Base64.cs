using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCryptoCore
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
            return Encode(str, false);
        }

        /// <summary>
        /// Encodes the base64.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="urlSafe">if set to <c>true</c> [URL safe].</param>
        /// <returns></returns>
        public static string Encode(string str, bool urlSafe)
        {
            byte[] strBytes = Encoding.UTF8.GetBytes(str);
            string output = Convert.ToBase64String(strBytes);

            if (urlSafe)
                output = output.Replace('+', '-').Replace('/', '_');

            return output;
        }


        /// <summary>
        /// Decodes the base64.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string Decode(string input)
        {
            return Decode(input, false);
        }

        /// <summary>
        /// Decodes the base64.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="urlSafe">if set to <c>true</c> [URL safe].</param>
        /// <returns></returns>
        public static string Decode(string input, bool urlSafe)
        {
            if (urlSafe)
                input = input.Replace('-', '+').Replace('_', '/');

            byte[] strBytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(strBytes);
        }
    }
}
