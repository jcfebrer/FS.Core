#if NET461_OR_GREATER || NETCOREAPP

using iText.Bouncycastle.X509;
using iText.Commons.Bouncycastle.Cert;
using iText.Forms.Form.Element;
using iText.IO.Image;
using iText.Kernel.Crypto;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Signatures;
using System;
using System.IO;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace FSBarcode
{
    public static class SignFile
    {
        public static void SignHashed(
            string sourcePdf,
            string targetPdf,
            X509Certificate2 certificate,
            string reason = "Firma Digital de Documento",
            string location = "",
            bool addVisibleSign = false,
            string imageUrl = null)
        {
            if (string.IsNullOrEmpty(sourcePdf) || !File.Exists(sourcePdf))
                throw new FileNotFoundException("No se encontró el archivo PDF de origen.", sourcePdf);

            if (certificate == null)
                throw new ArgumentNullException(nameof(certificate));

            // 1. Adaptar el certificado público a la interfaz de iText 7/8
            Org.BouncyCastle.X509.X509CertificateParser parser = new Org.BouncyCastle.X509.X509CertificateParser();
            IX509Certificate bcCert = new X509CertificateBC(parser.ReadCertificate(certificate.RawData));
            IX509Certificate[] chain = new IX509Certificate[] { bcCert };

            using (PdfReader reader = new PdfReader(sourcePdf))
            using (FileStream outputStream = new FileStream(targetPdf, FileMode.Create, FileAccess.Write))
            {
                StampingProperties stampingProperties = new StampingProperties();
                stampingProperties.UseAppendMode();

                PdfSigner signer = new PdfSigner(reader, outputStream, stampingProperties);

                // 2. Definir las propiedades básicas de la firma
                string fieldName = "Signature1";
                SignerProperties signerProperties = new SignerProperties();
                signerProperties.SetFieldName(fieldName);
                signerProperties.SetReason(reason);
                signerProperties.SetLocation(location);

                // 3. Configurar la representación visual si 'addVisibleSign' es true
                if (addVisibleSign)
                {
                    signerProperties.SetPageNumber(1);
                    signerProperties.SetPageRect(new Rectangle(36, 36, 200, 80));

                    SignatureFieldAppearance appearance = new SignatureFieldAppearance(fieldName);

                    if (!string.IsNullOrEmpty(imageUrl) && File.Exists(imageUrl))
                    {
                        ImageData imageData = ImageDataFactory.Create(imageUrl);
                        // Asignar el gráfico y el texto a SignatureFieldAppearance
                        appearance.SetContent(reason, imageData);
                    }
                    else
                    {
                        appearance.SetContent(reason);
                    }

                    // Vincular la apariencia configurada al SignerProperties
                    signerProperties.SetSignatureAppearance(appearance);
                }

                // Aplicar la configuración completa de SignerProperties en PdfSigner
                signer.SetSignerProperties(signerProperties);

                // 4. Instanciar el adaptador de firma CNG (IksCustomSignature)
                IExternalSignature pks = new IksCustomSignature(certificate, DigestAlgorithms.SHA256);

                // 5. Generar la firma PDF desconectada (CMS / PadES)
                signer.SignDetached(
                    new BouncyCastleDigest(),
                    pks,
                    chain,
                    null,
                    null,
                    null,
                    0,
                    PdfSigner.CryptoStandard.CMS
                );
            }
        }

        /// <summary>
        /// Firma un fichero XML con PKCS#7.
        /// </summary>
        /// <param name="rutaXmlOrigen"></param>
        /// <param name="rutaXmlDestino"></param>
        /// <param name="certificado"></param>
        /// <param name="writeDetached"></param>
        public static void SignFileXML(string rutaXmlOrigen, string rutaXmlDestino, X509Certificate2 certificado, bool writeDetached)
        {
            // 1. Leer el contenido binario del XML original
            byte[] datosXml = File.ReadAllBytes(rutaXmlOrigen);

            // 2. Generar la firma PKCS#7 utilizando tu función
            byte[] firmaPkcs7 = SignDataPKCS7(datosXml, certificado);

            if (writeDetached)
            {
                // Guardar la firma en un archivo .p7s que acompaña al XML
                File.WriteAllBytes(rutaXmlDestino + ".p7s", firmaPkcs7);
            }
            else
            {
                // 3. Convertir el bloque PKCS#7 a texto en Base64
                string firmaBase64 = Convert.ToBase64String(firmaPkcs7);

                // 4. Cargar el XML e incrustar el nodo de la firma
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(rutaXmlOrigen);

                XmlElement nodoFirma = xmlDoc.CreateElement("FirmaPKCS7");
                nodoFirma.InnerText = firmaBase64;

                if (xmlDoc.DocumentElement != null)
                {
                    xmlDoc.DocumentElement.AppendChild(nodoFirma);
                }

                // 5. Guardar el XML firmado
                xmlDoc.Save(rutaXmlDestino);
            }
        }

        /// <summary>
        /// Genera una firma PKCS#7 / CMS detached nativa de Windows sin iText.
        /// Funciona con certificados exportables, no exportables (CNG) y tarjetas DNIe.
        /// </summary>
        public static byte[] SignDataPKCS7(byte[] datosAhash, X509Certificate2 certificado)
        {
            if (datosAhash == null) throw new ArgumentNullException(nameof(datosAhash));
            if (certificado == null) throw new ArgumentNullException(nameof(certificado));

            // 1. Contenido a firmar
            ContentInfo contentInfo = new ContentInfo(datosAhash);

            // 2. Definir firma desconectada (detached = true)
            SignedCms signedCms = new SignedCms(contentInfo, detached: true);

            // 3. Configurar el firmante con el certificado
            CmsSigner signer = new CmsSigner(certificado)
            {
                DigestAlgorithm = new System.Security.Cryptography.Oid("2.16.840.1.101.3.4.2.1"), // SHA-256
                IncludeOption = X509IncludeOption.EndCertOnly
            };

            // 4. Calcular la firma delegando en el proveedor de Windows (evita el error de clave no exportable)
            signedCms.ComputeSignature(signer, silent: false);

            // 5. Devolver los bytes codificados en PKCS#7 / DER
            return signedCms.Encode();
        }
    }
}

#endif