#if NET461_OR_GREATER || NETCOREAPP

using iText.Commons.Bouncycastle.Cert;
using iText.Kernel.Crypto;
using iText.Kernel.Pdf;
using iText.Signatures;
using iText.Bouncycastleconnector;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace FSBarcode
{
    public class SignFileDeCA
    {
        public class DotNetPrivateKeySignature : IExternalSignature
        {
            private readonly X509Certificate2 _certificate;
            private readonly string _hashAlgorithm;

            public DotNetPrivateKeySignature(X509Certificate2 certificate, string hashAlgorithm)
            {
                _certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
                _hashAlgorithm = hashAlgorithm;
            }

            // Método exigido en versiones recientes de iText (7.2+, 8, 9)
            public string GetDigestAlgorithmName()
            {
                return _hashAlgorithm; // Retorna "SHA256"
            }

            // Método exigido en versiones anteriores de iText 7
            public string GetHashAlgorithm()
            {
                return _hashAlgorithm; // Retorna "SHA256"
            }

            // Método para indicar el algoritmo de cifrado/firma
            public string GetEncryptionAlgorithm()
            {
                return "RSA";
            }

            // Método exigido en algunas variantes de iText 8/9
            public string GetSignatureAlgorithmName()
            {
                return "RSA";
            }

            // Retorna null para firma RSA Estándar (PKCS#1 v1.5)
            public ISignatureMechanismParams GetSignatureMechanismParameters()
            {
                return null;
            }

            // Cómputo de la firma RSA sobre los datos SHA-256 usando .NET nativo
            public byte[] Sign(byte[] message)
            {
                using (RSA rsa = _certificate.GetRSAPrivateKey())
                {
                    if (rsa == null)
                    {
                        throw new InvalidOperationException("El certificado digital no contiene una clave privada RSA válida.");
                    }

                    return rsa.SignData(message, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
            }
        }

        public static void SignPdfCades(string pdfOrigen, string pdfDestino, string rutaPfx, string passwordPfx)
        {
            // 1. Cargar el certificado con .NET nativo
            X509Certificate2 cert2 = new X509Certificate2(
                rutaPfx,
                passwordPfx,
                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet
            );

            // 2. Instanciar la firma nativa (IExternalSignature)
            IExternalSignature pks = new DotNetPrivateKeySignature(cert2, DigestAlgorithms.SHA256);

            // 3. Convertir el certificado nativo de .NET a la interfaz iText
            IX509Certificate iTextCert = BouncyCastleFactoryCreator
                .GetFactory()
                .CreateX509Certificate(cert2.RawData);

            IX509Certificate[] chain = new IX509Certificate[] { iTextCert };

            // 4. Firmar el PDF
            using (PdfReader reader = new PdfReader(pdfOrigen))
            using (FileStream os = new FileStream(pdfDestino, FileMode.Create))
            {
                PdfSigner signer = new PdfSigner(reader, os, new StampingProperties());

                // Firma PAdES/CAdES invisible
                signer.SignDetached(
                    new BouncyCastleDigest(),
                    pks,
                    chain,
                    null, // crlList
                    null, // ocspClient
                    null, // tsaClient (Sello de tiempo)
                    0,    // estimatedSize
                    PdfSigner.CryptoStandard.CADES // Configura la estructura CAdES/PAdES
                );
            }
        }
    }
}

#endif