using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCrypto
{
    public class Basic
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
    }
}
