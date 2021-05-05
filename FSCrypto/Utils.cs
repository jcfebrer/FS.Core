using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCrypto
{
    public class Utils
    {
        /// <summary>
        /// Encrypts the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            long i = 0;
            string crypChar = null;
            var result = "";
            for (i = 0; i <= str.Length - 1; i++)
            {
                crypChar =
                    Convert.ToString(
                        Convert.ToChar(Convert.ToInt32(str.Substring(Convert.ToInt32(i), 1)) + str.Length));
                result = result + crypChar;
            }

            return result;
        }

        /// <summary>
        /// Decrypt the specified string.
        /// </summary>
        /// <param name="str">The cadena encriptada.</param>
        /// <returns></returns>
        public static string Decrypt(string str)
        {
            long i = 0;
            string decryptChar = null;
            var result = "";
            for (i = 0; i <= str.Length - 1; i++)
            {
                decryptChar =
                    Convert.ToString(
                        Convert.ToChar(Convert.ToInt32(str.Substring(Convert.ToInt32(i), 1)) -
                                       str.Length));
                result = result + decryptChar;
            }

            return result;
        }

        /// <summary>
        /// Encrypts the string whith key.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string EncryptKey(string str, string key)
        {
            var intpasslen = 0;
            var inttextlen = 0;
            var k = 0;
            var i = 0;
            var strencrpt = "";
            var cb = 0;
            intpasslen = key.Length;
            for (i = 1; i <= intpasslen; i++)
            {
                k = k + Convert.ToInt32(char.Parse(key.Substring(i, 1))) * i;
                if (k > 255) k = k - 255;
            }

            while (k > 255) k = k - 255;
            inttextlen = str.Length;
            for (i = 1; i <= inttextlen; i++)
            {
                cb = Convert.ToInt32(char.Parse(str.Substring(i, 1))) + k;
                if (cb > 255) cb = cb - 255;
                strencrpt = strencrpt + Convert.ToChar(cb);
                k = k + cb;
                if (k > 255) k = k - 255;
            }

            return strencrpt;
        }

        /// <summary>
        /// Decrypts string with the key.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string DecryptKey(string str, string key)
        {
            var intpasslen = 0;
            var inttextlen = 0;
            var k = 0;
            var i = 0;
            var strdrypt = "";
            var cb = 0;
            intpasslen = key.Length;
            for (i = 1; i <= intpasslen; i++)
            {
                k = k + Convert.ToInt32(char.Parse(key.Substring(i, 1))) * i;
                if (k > 255) k = k - 255;
            }

            while (k > 255) k = k - 255;
            inttextlen = str.Length;
            for (i = 1; i <= inttextlen; i++)
            {
                cb = Convert.ToInt32(char.Parse(str.Substring(i, 1))) - k;
                if (cb < 0) cb = cb + 255;
                strdrypt = strdrypt + Convert.ToChar(cb);
                k = k + Convert.ToInt32(char.Parse(str.Substring(i, 1)));
                if (k > 255) k = k - 255;
            }

            return strdrypt;
        }


        /// <summary>
        /// Encodes the base64.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static string EncodeBase64(string str)
        {
            return EncodeBase64(str, false);
        }

        /// <summary>
        /// Encodes the base64.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="urlSafe">if set to <c>true</c> [URL safe].</param>
        /// <returns></returns>
        public static string EncodeBase64(string str, bool urlSafe)
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
        public static string DecodeBase64(string input)
        {
            return DecodeBase64(input, false);
        }

        /// <summary>
        /// Decodes the base64.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="urlSafe">if set to <c>true</c> [URL safe].</param>
        /// <returns></returns>
        public static string DecodeBase64(string input, bool urlSafe)
        {
            if (urlSafe)
                input = input.Replace('-', '+').Replace('_', '/');

            byte[] strBytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(strBytes);
        }
    }
}
