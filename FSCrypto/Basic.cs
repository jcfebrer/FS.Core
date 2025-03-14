using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public static string EncryptStr(string str)
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
        public static string DecryptStr(string str)
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

        public static string Encrypt(string cadena, string key)
        {
            //arreglo de bytes donde guardaremos la llave
            byte[] keyArray;
            //arreglo de bytes donde guardaremos el texto
            //que vamos a encriptar
            byte[] Arreglo_a_Cifrar =
            UTF8Encoding.UTF8.GetBytes(cadena);

            //se utilizan las clases de encriptación
            //provistas por el Framework
            //Algoritmo MD5
            var hashmd5 = MD5.Create();
            //se guarda la llave para que se le realice
            //hashing
            keyArray = hashmd5.ComputeHash(
            UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            //Algoritmo 3DAS
            var tdes = TripleDES.Create();

            tdes.Key = keyArray;
            tdes.Mode = (System.Security.Cryptography.CipherMode)CipherMode.ECB;
            tdes.Padding = (System.Security.Cryptography.PaddingMode)PaddingMode.PKCS7;

            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform =
            tdes.CreateEncryptor();

            //arreglo de bytes donde se guarda la
            //cadena cifrada
            byte[] ArrayResultado =
            cTransform.TransformFinalBlock(Arreglo_a_Cifrar,
            0, Arreglo_a_Cifrar.Length);

            tdes.Clear();

            //se regresa el resultado en forma de una cadena
            return Convert.ToBase64String(ArrayResultado,
                   0, ArrayResultado.Length);

        }

        public static string Decrypt(string cadena, string key)
        {
            byte[] keyArray;
            //convierte el texto en una secuencia de bytes
            byte[] Array_a_Descifrar =
            Convert.FromBase64String(cadena);

            //se llama a las clases que tienen los algoritmos
            //de encriptación se le aplica hashing
            //algoritmo MD5
            var hashmd5 = MD5.Create();

            keyArray = hashmd5.ComputeHash(
            UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            var tdes = TripleDES.Create();

            tdes.Key = keyArray;
            tdes.Mode = (System.Security.Cryptography.CipherMode)CipherMode.ECB;
            tdes.Padding = (System.Security.Cryptography.PaddingMode)PaddingMode.PKCS7;

            ICryptoTransform cTransform =
             tdes.CreateDecryptor();

            byte[] resultArray =
            cTransform.TransformFinalBlock(Array_a_Descifrar,
            0, Array_a_Descifrar.Length);

            tdes.Clear();
            //se regresa en forma de cadena
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
