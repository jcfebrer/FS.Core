using FSException;
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

        /// <summary>
        /// Obtiene el certificado del almacen indicando su nombre.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateByName(string name)
        {
            X509Certificate2 certificate = null;
            X509Store Store = new X509Store(StoreName.AddressBook, StoreLocation.LocalMachine);
            Store.Open(OpenFlags.ReadOnly);
            foreach (var cert in Store.Certificates)
                if (cert.FriendlyName == name)
                {
                    certificate = cert;
                    break;
                }

            Store.Close();

            if (certificate == null)
                throw new ExceptionUtil("Certificado: " + name + ", no encontrado en el almacen.");
            else
                return certificate;
        }

        /// <summary>
        /// Obtiene el certificado del almacen indicando su número de serie.
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateBySerialNumber(string serialNumber)
        {
            X509Certificate2 certificate = null;
            X509Store Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            Store.Open(OpenFlags.ReadOnly);
            foreach (var cert in Store.Certificates)
                if (cert.SerialNumber.ToLower() == serialNumber.ToLower())
                {
                    certificate = cert;
                    break;
                }

            Store.Close();

            if (certificate == null)
                throw new ExceptionUtil("Certificado: " + serialNumber + ", no encontrado en el almacen.");
            else
                return certificate;
        }

        /// <summary>
        /// Obtiene el certificado del almacen indicando su editor.
        /// </summary>
        /// <param issuerName="editor"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateByIssuer(StoreLocation location, string issuerName)
        {
            X509Store Store = new X509Store(location);
            Store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certs = Store.Certificates.Find(X509FindType.FindByIssuerName, issuerName, true);
            return certs.OfType<X509Certificate2>().FirstOrDefault();
        }

        /// <summary>
        /// Obtiene el certificado del almacen indicando su número de serie.
        /// </summary>
        /// <param serialNumber="número de serie"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateBySerialNumber(StoreLocation location, string serialNumber)
        {
            X509Store Store = new X509Store(location);
            Store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certs = Store.Certificates.Find(X509FindType.FindBySerialNumber, serialNumber, true);
            return certs.OfType<X509Certificate2>().FirstOrDefault();
        }

        /// <summary>
        /// Obtiene el certificado del almacen indicando su nombre.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateByName(StoreLocation location, string name)
        {
            X509Store Store = new X509Store(location);
            Store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certs = Store.Certificates.Find(X509FindType.FindBySubjectName, name, true);
            return certs.OfType<X509Certificate2>().FirstOrDefault();
        }

        /// <summary>
        /// Obtiene el certificado indicando el path al fichero .p12 o .pfx y password.
        /// </summary>
        /// <param name="certPath"></param>
        /// <param name="certPass"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificate(string certPath, string certPass)
        {
            var cert = new X509Certificate2(certPath, certPass, X509KeyStorageFlags.Exportable);

            return cert;
        }

        public static string GetSerialNumber(X509Certificate2 cert)
        {
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
        public static void SignXml(XmlDocument xmlDocument, X509Certificate2 cert)
        {
            // Check arguments.
            if (xmlDocument == null)
                throw new ArgumentException("xmlDocument");
            if (cert == null)
                throw new ArgumentException("cert");

            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(xmlDocument);

            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data keyInfoData = new KeyInfoX509Data(cert);
            keyInfo.AddClause(keyInfoData);
            signedXml.KeyInfo = keyInfo;

            // Add the key to the SignedXml document.
#if NETFRAMEWORK
            signedXml.SigningKey = (RSA)cert.PrivateKey;
#else
            signedXml.SigningKey = (RSA)cert.GetRSAPrivateKey();
#endif

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
            xmlDocument.DocumentElement.AppendChild(xmlDocument.ImportNode(xmlDigitalSignature, true));

        }

        public static void SignXml(string fileName, X509Certificate2 cert)
        {
            // Check arguments.
            if (fileName == null)
                throw new ArgumentException("fileName");
            if (cert == null)
                throw new ArgumentException("cert");

            // Create a new XML document.
            XmlDocument xmlDocument = new XmlDocument();

            // Load the passed XML file into the document. 
            xmlDocument.Load(fileName);

            SignXml(xmlDocument, cert);
        }

        // Verify the signature of an XML file against an asymmetric 
        // algorithm and return the result.
        public static Boolean VerifyXml(XmlDocument xmlDocument, X509Certificate2 cert)
        {
            // Check arguments.
            if (xmlDocument == null)
                throw new ArgumentException("xmlDocument");
            if (cert == null)
                throw new ArgumentException("cert");

            // Create a new SignedXml object and pass it
            // the XML document class.
            SignedXml signedXml = new SignedXml(xmlDocument);

            // Find the "Signature" node and create a new
            // XmlNodeList object.
            XmlNodeList nodeList = xmlDocument.GetElementsByTagName("Signature");

            // Load the signature node.
            signedXml.LoadXml((XmlElement)nodeList[0]);

            // Check the signature and return the result.
#if NETFRAMEWORK
            return signedXml.CheckSignature((RSA)cert.PrivateKey);
#else
            return signedXml.CheckSignature((RSA)cert.GetRSAPrivateKey());
#endif
        }


        public static Boolean VerifyXml(string fileName, X509Certificate2 cert)
        {
            // Check arguments.
            if (fileName == null)
                throw new ArgumentException("fileName");
            if (cert == null)
                throw new ArgumentException("cert");

            // Create a new XML document.
            XmlDocument xmlDocument = new XmlDocument();

            // Load the passed XML file into the document. 
            xmlDocument.Load(fileName);

            return VerifyXml(xmlDocument, cert);
        }

        public static bool IsSelfSigned(X509Certificate2 cert)
        {
            return cert.SubjectName.RawData.SequenceEqual(cert.IssuerName.RawData);
        }

        public static bool CheckIfHasPrivateKey(X509Certificate2 cert)
        {
#if NETFRAMEWORK
            return cert.PrivateKey == null;
#else
            return cert.GetRSAPrivateKey() == null;
#endif
        }
    }
}
