#if NET461_OR_GREATER || NETCOREAPP

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using iText.Signatures;

namespace FSBarcode
{
    public class IksCustomSignature : IExternalSignature
    {
        private readonly X509Certificate2 _certificate;
        private readonly string _digestAlgorithm;

        public IksCustomSignature(X509Certificate2 certificate, string digestAlgorithm = "SHA-256")
        {
            _certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
            _digestAlgorithm = digestAlgorithm;
        }

        public string GetHashAlgorithm()
        {
            return _digestAlgorithm;
        }

        public string GetEncryptionAlgorithm()
        {
            return "RSA";
        }

        public byte[] Sign(byte[] message)
        {
            if (message == null || message.Length == 0)
                throw new ArgumentNullException(nameof(message));

            HashAlgorithmName hashName;
            int hashLengthExpected;
            string alg = _digestAlgorithm.Replace("-", "").ToUpperInvariant();

            switch (alg)
            {
                case "SHA1":
                    hashName = HashAlgorithmName.SHA1;
                    hashLengthExpected = 20;
                    break;
                case "SHA512":
                    hashName = HashAlgorithmName.SHA512;
                    hashLengthExpected = 64;
                    break;
                case "SHA384":
                    hashName = HashAlgorithmName.SHA384;
                    hashLengthExpected = 48;
                    break;
                default:
                    hashName = HashAlgorithmName.SHA256;
                    hashLengthExpected = 32;
                    break;
            }

            byte[] hashToSign;
            if (message.Length == hashLengthExpected)
            {
                hashToSign = message;
            }
            else
            {
                using (HashAlgorithm hasher = HashAlgorithm.Create(hashName.Name))
                {
                    if (hasher == null)
                        throw new InvalidOperationException($"No se pudo instanciar el algoritmo {hashName.Name}");
                    hashToSign = hasher.ComputeHash(message);
                }
            }

            using (RSA rsa = _certificate.GetRSAPrivateKey())
            {
                if (rsa != null)
                {
                    return rsa.SignHash(hashToSign, hashName, RSASignaturePadding.Pkcs1);
                }
            }

            if (_certificate.PrivateKey is RSACryptoServiceProvider csp)
            {
                return csp.SignHash(hashToSign, CryptoConfig.MapNameToOID(_digestAlgorithm));
            }

            throw new InvalidOperationException("No se pudo obtener el proveedor criptográfico de la clave privada.");
        }

        /// <summary>
        /// Devuelve el nombre del algoritmo de digest utilizado (ej. "SHA-256").
        /// </summary>
        public string GetDigestAlgorithmName()
        {
            return _digestAlgorithm;
        }

        /// <summary>
        /// Devuelve el algoritmo de firma utilizado según el tipo de clave (ej. "RSA").
        /// </summary>
        public string GetSignatureAlgorithmName()
        {
            return "RSA";
        }

        /// <summary>
        /// Devuelve parámetros adicionales del mecanismo de firma (en firma RSA PKCS#1 v1.5 es null).
        /// </summary>
        public ISignatureMechanismParams GetSignatureMechanismParameters()
        {
            return null;
        }
    }
}

#endif