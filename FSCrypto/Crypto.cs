// // <fileheader>
// // <copyright file="Crypto.cs" company="Febrer Software">
// //     Fecha: 03/07/2010
// //     Project: FSLibrary
// //     Solution: FSLibraryNET2008
// //     Copyright (c) 2010 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using FSException;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace FSCrypto
{
    /// <summary>
    ///     Enumeración con los tipos de encriptación
    /// </summary>
    public enum CryptoProvider
    {
        DES,
        TripleDES,
        RC2,
        Rijndael
    }

    /// <summary>
    ///     Acción a realizar
    /// </summary>
    public enum CryptoAction
    {
        Encrypt,
        Desencrypt
    }

    /// <summary>
    ///     Clase para encriptar.
    /// </summary>
    public class Crypto
    {
        public const string Password = "16055459";

        private readonly CryptoProvider algorithm;


        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="alg"></param>
        public Crypto()
        {
            algorithm = CryptoProvider.DES;
            Iv = "12345678";
            Key = Password;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="alg"></param>
        public Crypto(CryptoProvider alg)
        {
            algorithm = alg;
            Iv = "12345678";
            Key = Password;
        }


        /// <summary>
        ///     Constructor
        /// </summary>
        public Crypto(string key)
        {
            algorithm = CryptoProvider.DES;
            Iv = "12345678";
            Key = key;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public Crypto(string key, string keyIv)
        {
            algorithm = CryptoProvider.DES;
            Iv = keyIv;
            Key = key;
        }


        /// <summary>
        ///     Constructor
        /// </summary>
        public Crypto(CryptoProvider alg, string key)
        {
            algorithm = alg;
            Iv = "12345678";
            Key = key;
        }


        /// <summary>
        ///     Constructor
        /// </summary>
        public Crypto(CryptoProvider alg, string key, string keyIv)
        {
            algorithm = alg;
            Iv = keyIv;
            Key = key;
        }

        /// <summary>
        ///     Clave
        /// </summary>
        public string Key { get; set; }


        /// <summary>
        ///     IV
        /// </summary>
        public string Iv { get; set; }


        /// <summary>
        ///     MD5enc
        /// </summary>
        public bool EncKeyMD5 { get; set; } = false;


        /// <summary>
        ///     Función que encripta la cadena.
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public string Crypt(string cadena)
        {
            MemoryStream memStream = null;
            try
            {
                if (Key != null && Iv != null)
                {
                    var key = MakeKeyByteArray(Key);
                    var iv = MakeIvByteArray(Iv);
                    var textoPlano = Encoding.UTF8.GetBytes(cadena);

                    memStream = new MemoryStream(cadena.Length * 2);
                    var cryptoProvider =
                        new CryptoServiceProvider(algorithm,
                            CryptoAction.Encrypt);

                    //si se requiere MD5 en la encriptación de la clave
                    if (EncKeyMD5)
                    {
                        var md5 = new MD5CryptoServiceProvider();
                        var hashByte = Encoding.ASCII.GetBytes(cadena);
                        key = md5.ComputeHash(hashByte, 0, hashByte.Length);
                    }

                    var transform = cryptoProvider.GetServiceProvider(key, iv);
                    var cs = new CryptoStream(memStream, transform, CryptoStreamMode.Write);
                    cs.Write(textoPlano, 0, textoPlano.Length);
                    cs.Close();
                }
                else
                {
                    throw new Exception("Error al inicializar la clave y el vector");
                }
            }
            catch (ExceptionUtil e)
            {
                throw new Exception(e.Message);
            }

            var memS = "";
            if (memStream != null)
                memS = Convert.ToBase64String(memStream.ToArray());

            return memS;
        }

        /// <summary>
        ///     Función que desencripta la cadena.
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public string Decryp(string cadena)
        {
            MemoryStream memStream = null;
            try
            {
                if (Key != null && Iv != null)
                {
                    var key = MakeKeyByteArray(Key);
                    var iv = MakeIvByteArray(Iv);
                    var textoCifrado = Convert.FromBase64String(cadena);

                    memStream = new MemoryStream(cadena.Length);
                    var cryptoProvider =
                        new CryptoServiceProvider(algorithm,
                            CryptoAction.Desencrypt);
                    var transform = cryptoProvider.GetServiceProvider(key, iv);
                    var cs = new CryptoStream(memStream, transform, CryptoStreamMode.Write);
                    cs.Write(textoCifrado, 0, textoCifrado.Length);
                    cs.Close();
                }
                else
                {
                    throw new Exception("Error al inicializar la clave y el vector.");
                }
            }
            catch (ExceptionUtil e)
            {
                throw new Exception(e.Message);
            }

            var memS = "";
            if (memStream != null)
                memS = Encoding.UTF8.GetString(memStream.ToArray());
            return memS;
        } 

        /// <summary>
        ///     Convierte la clave en un array de bytes
        /// </summary>
        /// <returns></returns>
        private byte[] MakeKeyByteArray(string stringKey)
        {
            switch (algorithm)
            {
                case CryptoProvider.DES:
                case CryptoProvider.RC2:
                    if (stringKey.Length < 8)
                        stringKey = stringKey.PadRight(8);
                    else if (stringKey.Length > 8)
                        stringKey = stringKey.Substring(0, 8);
                    break;
                case CryptoProvider.TripleDES:
                case CryptoProvider.Rijndael:
                    if (stringKey.Length < 16)
                        stringKey = stringKey.PadRight(16);
                    else if (stringKey.Length > 16)
                        stringKey = stringKey.Substring(0, 16);
                    break;
            }

            return Encoding.UTF8.GetBytes(stringKey);
        }

        /// <summary>
        ///     Convierte la clave IV en un array de bytes
        /// </summary>
        /// <returns></returns>
        private byte[] MakeIvByteArray(string stringIv)
        {
            switch (algorithm)
            {
                case CryptoProvider.DES:
                case CryptoProvider.RC2:
                case CryptoProvider.TripleDES:
                    if (stringIv.Length < 8)
                        stringIv = stringIv.PadRight(8);
                    else if (stringIv.Length > 8)
                        stringIv = stringIv.Substring(0, 8);
                    break;
                case CryptoProvider.Rijndael:
                    if (stringIv.Length < 16)
                        stringIv = stringIv.PadRight(16);
                    else if (stringIv.Length > 16)
                        stringIv = stringIv.Substring(0, 16);
                    break;
            }

            return Encoding.UTF8.GetBytes(stringIv);
        }

        /// <summary>
        ///     Encripta/Desencripta un fichero
        /// </summary>
        /// <param name="inFileName"></param>
        /// <param name="outFileName"></param>
        /// <param name="action"></param>
        public void CryptEncryptFile(string inFileName, string outFileName, CryptoAction action)
        {
            if (!File.Exists(inFileName)) throw new Exception("No se ha encontrado el archivo.");

            try
            {
                if (Key != null && Iv != null)
                {
                    var fsIn = new FileStream(inFileName, FileMode.Open, FileAccess.Read);
                    var fsOut = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
                    fsOut.SetLength(0);

                    var key = MakeKeyByteArray(Key);
                    var iv = MakeIvByteArray(Iv);
                    var byteBuffer = new byte[4096];
                    var largoArchivo = fsIn.Length;
                    long bytesProcesados = 0;
                    var cryptoProvider = new CryptoServiceProvider(algorithm,
                        action);
                    var transform = cryptoProvider.GetServiceProvider(key, iv);
                    CryptoStream cryptoStream = null;

                    switch (action)
                    {
                        case CryptoAction.Encrypt:
                            cryptoStream = new CryptoStream(fsOut, transform, CryptoStreamMode.Write);
                            break;
                        case CryptoAction.Desencrypt:
                            cryptoStream = new CryptoStream(fsOut, transform, CryptoStreamMode.Write);
                            break;
                    }

                    while (bytesProcesados < largoArchivo)
                    {
                        var bloqueBytes = fsIn.Read(byteBuffer, 0, 4096);
                        if (cryptoStream != null) cryptoStream.Write(byteBuffer, 0, bloqueBytes);
                        bytesProcesados += bloqueBytes;
                    }

                    if (cryptoStream != null)
                        cryptoStream.Close();
                    fsIn.Close();
                    fsOut.Close();
                }
                else
                {
                    throw new Exception("Error al inicializar la clave y el vector.");
                }
            }
            catch (ExceptionUtil e)
            {
                throw new Exception(e.Message);
            }
        }
    }


    internal class CryptoServiceProvider
    {
        private readonly CryptoProvider algorithm;
        private readonly CryptoAction cAction;


        internal CryptoServiceProvider(CryptoProvider alg, CryptoAction action)
        {
            algorithm = alg;
            cAction = action;
        }

        internal ICryptoTransform GetServiceProvider(byte[] key, byte[] iv)
        {
            ICryptoTransform transform = null;

            switch (algorithm)
            {
                case CryptoProvider.DES:
                    var des = new DESCryptoServiceProvider();
                    switch (cAction)
                    {
                        case CryptoAction.Encrypt:
                            transform = des.CreateEncryptor(key, iv);
                            break;
                        case CryptoAction.Desencrypt:
                            transform = des.CreateDecryptor(key, iv);
                            break;
                    }

                    return transform;
                case CryptoProvider.TripleDES:
                    var des3 = new TripleDESCryptoServiceProvider();
                    switch (cAction)
                    {
                        case CryptoAction.Encrypt:
                            transform = des3.CreateEncryptor(key, iv);
                            break;
                        case CryptoAction.Desencrypt:
                            transform = des3.CreateDecryptor(key, iv);
                            break;
                    }

                    return transform;
                case CryptoProvider.RC2:
                    var rc2 = new RC2CryptoServiceProvider();
                    switch (cAction)
                    {
                        case CryptoAction.Encrypt:
                            transform = rc2.CreateEncryptor(key, iv);
                            break;
                        case CryptoAction.Desencrypt:
                            transform = rc2.CreateDecryptor(key, iv);
                            break;
                    }

                    return transform;
                case CryptoProvider.Rijndael:
                    Rijndael rijndael = new RijndaelManaged();
                    switch (cAction)
                    {
                        case CryptoAction.Encrypt:
                            transform = rijndael.CreateEncryptor(key, iv);
                            break;
                        case CryptoAction.Desencrypt:
                            transform = rijndael.CreateDecryptor(key, iv);
                            break;
                    }

                    return transform;
                default:
                    throw new CryptographicException("Error al inicializar al proveedor de cifrado");
            }
        }
    }
}