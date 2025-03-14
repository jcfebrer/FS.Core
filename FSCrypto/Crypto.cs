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
using FSLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        AES,
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
    ///     CipherMode
    /// </summary>
    public enum CipherMode
    {
        CBC = 1,
        ECB = 2,
        OFB = 3,
        CFB = 4,
        CTS = 5
    }

    public enum PaddingMode
    {
        None = 1,
        PKCS7 = 2,
        Zeros = 3,
        ANSIX923 = 4,
        ISO10126 = 5
    }

    /// <summary>
    ///     Enumeración con los tipos de transporte
    /// </summary>
    public enum TransportMode
    {
        Base64,
        BytePair,
        UTF8,
        ASCII
    }

    /// <summary>
    ///     Clase para encriptar.
    /// </summary>
    public class Crypto
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public Crypto()
        {
            cryptoProvider = CryptoProvider.TripleDES;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="cryptoProvider"></param>
        public Crypto(CryptoProvider cryptoProvider)
        {
            this.cryptoProvider = cryptoProvider;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="cryptoProvider"></param>
        public Crypto(CryptoProvider cryptoProvider, CipherMode cipherMode)
        {
            this.cryptoProvider = cryptoProvider;
            this.cipherMode = cipherMode;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="cryptoProvider"></param>
        public Crypto(CryptoProvider cryptoProvider, CipherMode cipherMode, PaddingMode paddingMode)
        {
            this.cryptoProvider = cryptoProvider;
            this.cipherMode = cipherMode;
            this.paddingMode = paddingMode;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="cryptoProvider"></param>
        public Crypto(CryptoProvider cryptoProvider, CipherMode cipherMode, PaddingMode paddingMode, TransportMode transportMode)
        {
            this.cryptoProvider = cryptoProvider;
            this.cipherMode = cipherMode;
            this.paddingMode = paddingMode;
            this.transportMode = transportMode;
        }


        /// <summary>
        ///     Constructor
        /// </summary>
        public Crypto(string key)
        {
            Key = key;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public Crypto(string key, string keyIv)
        {
            Iv = keyIv;
            Key = key;
        }


        /// <summary>
        ///     Constructor
        /// </summary>
        public Crypto(CryptoProvider cryptoProvider, string key)
        {
            this.cryptoProvider = cryptoProvider;
            Key = key;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public Crypto(CryptoProvider cryptoProvider, string key, CipherMode cipherMode)
        {
            this.cryptoProvider = cryptoProvider;
            Key = key;
            this.cipherMode = cipherMode;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public Crypto(CryptoProvider cryptoProvider, string key, CipherMode cipherMode, PaddingMode paddingMode)
        {
            this.cryptoProvider = cryptoProvider;
            Key = key;
            this.cipherMode = cipherMode;
            this.paddingMode = paddingMode;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public Crypto(CryptoProvider cryptoProvider, string key, CipherMode cipherMode, PaddingMode paddingMode, TransportMode transportMode)
        {
            this.cryptoProvider = cryptoProvider;
            Key = key;
            this.cipherMode = cipherMode;
            this.paddingMode = paddingMode;
            this.transportMode = transportMode;
        }


        /// <summary>
        ///     Constructor
        /// </summary>
        public Crypto(CryptoProvider cryptoProvider, string key, string keyIv)
        {
            this.cryptoProvider = cryptoProvider;
            Iv = keyIv;
            Key = key;
        }

        /// <summary>
        ///     Clave
        /// </summary>
        public string Key { get; set; } = "16055459x";


        /// <summary>
        ///     IV
        /// </summary>
        public string Iv { get; set; } = null;

        /// <summary>
        ///     CypherMode
        /// </summary>
        public CipherMode cipherMode { get; set; } = CipherMode.ECB;

        /// <summary>
        ///     PaddingMode
        /// </summary>
        public PaddingMode paddingMode { get; set; } = PaddingMode.PKCS7;

        /// <summary>
        ///     TransportMode
        /// </summary>
        public TransportMode transportMode { get; set; } = TransportMode.Base64;

        /// <summary>
        ///     MD5enc
        /// </summary>
        public bool EncKeyMD5 { get; set; } = true;

        /// <summary>
        /// Algoritmo de encriptación.
        /// </summary>
        public CryptoProvider cryptoProvider { get; set; } = CryptoProvider.TripleDES;


        /// <summary>
        ///     Función que encripta la cadena.
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public string Crypt(string cadena)
        {
            try
            {
                if (Key != null)
                {
                    var key = MakeKeyByteArray(Key);
                    var iv = MakeIvByteArray(Iv);
                    var textoPlano = Encoding.UTF8.GetBytes(cadena);

                    var cryptoProvider =
                        new CryptoServiceProvider(this.cryptoProvider,
                            CryptoAction.Encrypt, cipherMode, paddingMode);

                    //si se requiere MD5 en la encriptación de la clave
                    if (EncKeyMD5)
                    {
                        var md5 = MD5.Create();
                        var hashByte = UTF8Encoding.UTF8.GetBytes(Key);
                        key = md5.ComputeHash(hashByte, 0, hashByte.Length);
                    }

                    var transform = cryptoProvider.GetServiceProvider(key, iv);

                    var resultArray = transform.TransformFinalBlock(textoPlano, 0, textoPlano.Length);

                    string result = null;
                    switch(transportMode)
                    {
                        case TransportMode.Base64:
                            result = Convert.ToBase64String(resultArray, 0, resultArray.Length);
                            break;
                        case TransportMode.BytePair:
                            result = NumberUtils.BytesToStringHex(resultArray);
                            break;
                        case TransportMode.UTF8:
                            result = Encoding.UTF8.GetString(resultArray);
                            break;
                        case TransportMode.ASCII:
                            result = Encoding.ASCII.GetString(resultArray);
                            break;
                    }

                    return result;
                }
                else
                {
                    throw new ExceptionUtil("Error: Clave no definida.");
                }
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e.Message);
            }
        }

        /// <summary>
        ///     Función que desencripta la cadena.
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public string Decryp(string cadena)
        {
            try
            {
                if (Key != null)
                {
                    var key = MakeKeyByteArray(Key);
                    var iv = MakeIvByteArray(Iv);

                    byte[] textoCifrado = null;

                    switch (transportMode)
                    {
                        case TransportMode.Base64:
                            textoCifrado = Convert.FromBase64String(cadena);
                            break;
                        case TransportMode.BytePair:
                            textoCifrado = NumberUtils.StringBytePairToBytes(cadena);
                            break;
                        case TransportMode.UTF8:
                            textoCifrado = Encoding.UTF8.GetBytes(cadena);
                            break;
                        case TransportMode.ASCII:
                            textoCifrado = Encoding.ASCII.GetBytes(cadena);
                            break;
                    }

                    var cryptoProvider =
                        new CryptoServiceProvider(this.cryptoProvider,
                            CryptoAction.Desencrypt, cipherMode, paddingMode);

                    //si se requiere MD5 en la encriptación de la clave
                    if (EncKeyMD5)
                    {
                        var md5 = MD5.Create();
                        var hashByte = UTF8Encoding.UTF8.GetBytes(Key);
                        key = md5.ComputeHash(hashByte, 0, hashByte.Length);
                    }

                    var transform = cryptoProvider.GetServiceProvider(key, iv);

                    var resultArray = transform.TransformFinalBlock(textoCifrado, 0, textoCifrado.Length);
                    return UTF8Encoding.UTF8.GetString(resultArray);
                }
                else
                {
                    throw new ExceptionUtil("Error: Clave no definida.");
                }
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e.Message);
            }
        }
        

        /// <summary>
        ///     Convierte la clave en un array de bytes
        /// </summary>
        /// <returns></returns>
        private byte[] MakeKeyByteArray(string stringKey)
        {
            switch (cryptoProvider)
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
            if (stringIv == null)
                return null;

            switch (cryptoProvider)
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
        public void CryptFile(string inFileName, string outFileName, CryptoAction action)
        {
            if (!File.Exists(inFileName))
                throw new ExceptionUtil("No existe el fichero: " + inFileName);

            try
            {
                if (Key != null)
                {
                    var fsIn = new FileStream(inFileName, FileMode.Open, FileAccess.Read);
                    var fsOut = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
                    fsOut.SetLength(0);

                    var key = MakeKeyByteArray(Key);
                    var iv = MakeIvByteArray(Iv);
                    var byteBuffer = new byte[4096];
                    var largoArchivo = fsIn.Length;
                    long bytesProcesados = 0;
                    var cryptoProvider = new CryptoServiceProvider(this.cryptoProvider,
                        action, cipherMode, paddingMode);

                    //si se requiere MD5 en la encriptación de la clave
                    if (EncKeyMD5)
                    {
                        var md5 = MD5.Create();
                        var hashByte = UTF8Encoding.UTF8.GetBytes(Key);
                        key = md5.ComputeHash(hashByte, 0, hashByte.Length);
                    }

                    var transform = cryptoProvider.GetServiceProvider(key, iv);

                    CryptoStream cryptoStream = new CryptoStream(fsOut, transform, CryptoStreamMode.Write);

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
                    throw new ExceptionUtil("Error: Clave no definida.");
                }
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e.Message);
            }
        }
    }


    internal class CryptoServiceProvider
    {
        private readonly CryptoProvider algorithm;
        private readonly CryptoAction cAction;
        private readonly CipherMode cipherMode;
        private readonly PaddingMode paddingMode;


        internal CryptoServiceProvider(CryptoProvider alg, CryptoAction action, CipherMode cipherMode, PaddingMode paddingMode)
        {
            algorithm = alg;
            cAction = action;
            this.cipherMode = cipherMode;
            this.paddingMode = paddingMode;
        }

        internal ICryptoTransform GetServiceProvider(byte[] key, byte[] iv)
        {
            ICryptoTransform transform = null;

            switch (algorithm)
            {
                case CryptoProvider.AES:
                    var aes = Aes.Create();

                    aes.Mode = (System.Security.Cryptography.CipherMode)cipherMode;
                    aes.Padding = (System.Security.Cryptography.PaddingMode)paddingMode;
                    aes.KeySize = 256;

                    switch (cAction)
                    {
                        case CryptoAction.Encrypt:
                            transform = aes.CreateEncryptor(key, iv);
                            break;
                        case CryptoAction.Desencrypt:
                            transform = aes.CreateDecryptor(key, iv);
                            break;
                    }

                    aes.Clear();
                    return transform;
                case CryptoProvider.DES:
                    var des = DES.Create();

                    des.Mode = (System.Security.Cryptography.CipherMode)cipherMode;
                    des.Padding = (System.Security.Cryptography.PaddingMode)paddingMode;

                    switch (cAction)
                    {
                        case CryptoAction.Encrypt:
                            transform = des.CreateEncryptor(key, iv);
                            break;
                        case CryptoAction.Desencrypt:
                            transform = des.CreateDecryptor(key, iv);
                            break;
                    }

                    des.Clear();
                    return transform;
                case CryptoProvider.TripleDES:
                    var des3 = TripleDES.Create();

                    des3.Mode = (System.Security.Cryptography.CipherMode)cipherMode;
                    des3.Padding = (System.Security.Cryptography.PaddingMode)paddingMode;

                    switch (cAction)
                    {
                        case CryptoAction.Encrypt:
                            transform = des3.CreateEncryptor(key, iv);
                            break;
                        case CryptoAction.Desencrypt:
                            transform = des3.CreateDecryptor(key, iv);
                            break;
                    }

                    des3.Clear();
                    return transform;
                case CryptoProvider.RC2:
                    var rc2 = RC2.Create();

                    rc2.Mode = (System.Security.Cryptography.CipherMode)cipherMode;
                    rc2.Padding = (System.Security.Cryptography.PaddingMode)paddingMode;

                    switch (cAction)
                    {
                        case CryptoAction.Encrypt:
                            transform = rc2.CreateEncryptor(key, iv);
                            break;
                        case CryptoAction.Desencrypt:
                            transform = rc2.CreateDecryptor(key, iv);
                            break;
                    }

                    rc2.Clear();
                    return transform;
                case CryptoProvider.Rijndael:
                    var rijndael = Aes.Create();

                    rijndael.Mode = (System.Security.Cryptography.CipherMode)cipherMode;
                    rijndael.Padding = (System.Security.Cryptography.PaddingMode)paddingMode;

                    switch (cAction)
                    {
                        case CryptoAction.Encrypt:
                            transform = rijndael.CreateEncryptor(key, iv);
                            break;
                        case CryptoAction.Desencrypt:
                            transform = rijndael.CreateDecryptor(key, iv);
                            break;
                    }

                    rijndael.Clear();
                    return transform;
                default:
                    throw new CryptographicException("Error al inicializar al proveedor de cifrado");
            }
        }
    }
}
