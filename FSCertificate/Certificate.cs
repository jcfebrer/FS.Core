using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FSCertificate
{
    /// <summary>
    ///     Funciones para la gestión de certificados.
    /// </summary>
    public class Certificate
    {
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static X509Certificate2 GetCertificate(string sn)
        {
            X509Certificate2 certificate = new X509Certificate2();
            X509Store Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            Store.Open(OpenFlags.ReadOnly);
            foreach (var cert in Store.Certificates)
                if (cert.SerialNumber == sn)
                {
                    certificate = cert;
                    break;
                }

            Store.Close();

            return certificate;
        }

        public static X509Certificate2 GetCertificate(string certPath, string certPass)
        {
            var cert = new X509Certificate2(certPath, certPass, X509KeyStorageFlags.Exportable);

            return cert;
        }

        public static string GetSerialNumber(string certPath, string certPass)
        {
            var cert = new X509Certificate2(certPath, certPass, X509KeyStorageFlags.Exportable);

            return cert.GetSerialNumberString();
        }

        public static string SignMessage(string message, X509Certificate2 cert)
        {
            if (cert == null)
                throw new ArgumentException("cert");

            byte[] msgBytes = Encoding.Unicode.GetBytes(message);
            ContentInfo content = new ContentInfo(msgBytes);
            SignedCms sign = new SignedCms(content);
            CmsSigner signer = new CmsSigner(cert);
            sign.ComputeSignature(signer);
            byte[] msgSign = sign.Encode();

            return Encoding.Unicode.GetString(msgSign);
        }

        // Sign an XML file. 
        // This document cannot be verified unless the verifying 
        // code has the key with which it was signed.
        public static void SignXml(XmlDocument xmlDoc, X509Certificate2 cert)
        {
            // Check arguments.
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (cert == null)
                throw new ArgumentException("cert");

            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(xmlDoc);

            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data keyInfoData = new KeyInfoX509Data(cert);
            keyInfo.AddClause(keyInfoData);
            signedXml.KeyInfo = keyInfo;

            // Add the key to the SignedXml document.
            signedXml.SigningKey = (RSA)cert.PrivateKey;

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

        }
    }
}
