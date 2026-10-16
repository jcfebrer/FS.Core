using FSException;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.IO;


#if NET40_OR_GREATER || NETCOREAPP
using System.Linq;
    using System.Security.Cryptography.Xml;
    using System.Security.Cryptography.Pkcs;
#endif

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

        public static X509Certificate2 Load(string serialNumber, bool clone)
        {
            if (string.IsNullOrEmpty(serialNumber))
                throw new ArgumentException("El número de serie no puede estar vacío.", nameof(serialNumber));

            // Limpiar espacios o caracteres no visibles del número de serie
            string cleanSerial = System.Text.RegularExpressions.Regex.Replace(serialNumber, @"[^\da-fA-F]", "");

            // Buscar en LocalMachine (Recomendado para IIS) o CurrentUser
            X509Certificate2 cert = GetCertificateBySerialNumber(StoreLocation.LocalMachine, cleanSerial, clone)
                                 ?? GetCertificateBySerialNumber(StoreLocation.CurrentUser, cleanSerial, clone);

            if (cert == null)
                throw new Exception($"No se encontró el certificado con número de serie: {serialNumber}");

            return cert;
        }

        /// <summary>
        /// Obtiene los certificados del almacen
        /// </summary>
        /// <returns></returns>
        public static X509Certificate2Collection GetCertificates()
        {
            X509Certificate2Collection certificates = null;
            X509Store Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            Store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            
            certificates = Store.Certificates;

            Store.Close();

            return certificates;
        }

        /// <summary>
        /// Obtiene el certificate del almacen indicando su nombre.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateByName(string name, bool clone)
        {
            X509Certificate2 certificate = null;
            X509Store Store = new X509Store(StoreName.AddressBook, StoreLocation.LocalMachine);
            Store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            foreach (var cert in Store.Certificates)
                if (cert.FriendlyName.ToLower() == name.ToLower())
                {
                    certificate = cert;
                    break;
                }

            Store.Close();

            if (certificate == null)
                throw new ExceptionUtil("Certificado: " + name + ", no encontrado en el almacen.");
            else
            {
                if (clone)
                {
                    return GetCertificateClone(certificate);
                }
                else
                {
                    return certificate;
                }
            }
        }

        /// <summary>
        /// Obtiene el certificate del almacen indicando su número de serie.
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateBySerialNumber(string serialNumber, bool clone)
        {
            // Limpiamos espacios o caracteres invisibles del número de serie
            serialNumber = serialNumber.Replace(" ", "").Replace("\u200e", "").Replace("\u200f", "");

            X509Certificate2 certificate = null;
            X509Store Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            Store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
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
            {
                if (clone)
                {
                    return GetCertificateClone(certificate);
                }
                else
                {
                    return certificate;
                }
            }
        }

        /// <summary>
        /// Obtiene el certificate del almacen indicando su editor.
        /// </summary>
        /// <param issuerName="editor"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateByIssuer(StoreLocation location, string issuerName, bool clone)
        {
            X509Store Store = new X509Store(location);
            Store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection certs = Store.Certificates.Find(X509FindType.FindByIssuerName, issuerName, true);

            if (certs.Count > 0)
            {
                if (clone)
                {
                    return GetCertificateClone(certs[0]);
                }
                else
                {
                    return certs[0];
                }
            }

            return null;
        }

        /// <summary>
        /// Obtiene el certificate del almacen indicando su número de serie.
        /// </summary>
        /// <param serialNumber="número de serie"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateBySerialNumber(StoreLocation location, string serialNumber, bool clone)
        {
            // Limpiamos espacios o caracteres invisibles del número de serie
            serialNumber = serialNumber.Replace(" ", "").Replace("\u200e", "").Replace("\u200f", "");

            X509Store Store = new X509Store(location);
            Store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection certs = Store.Certificates.Find(X509FindType.FindBySerialNumber, serialNumber, true);

            if (certs.Count > 0)
            {
                if (clone)
                {
                    return GetCertificateClone(certs[0]);
                }
                else
                {
                    return certs[0];
                }
            }

            return null;
        }

        /// <summary>
        /// Obtiene el certificate del almacen indicando su nombre.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateByName(StoreLocation location, string name, bool clone)
        {
            X509Store Store = new X509Store(location);
            Store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection certs = Store.Certificates.Find(X509FindType.FindBySubjectName, name, true);

            if (certs.Count > 0)
            {
                if (clone)
                {
                    return GetCertificateClone(certs[0]);
                }
                else
                {
                    return certs[0];
                }
            }

            return null;
        }

        /// <summary>
        /// Obtiene el certificate indicando el path al fichero .p12 o .pfx y password.
        /// </summary>
        /// <param name="certPath"></param>
        /// <param name="password"></param>
        /// <param name="clone"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificate(string certPath, string password)
        {
            var certificate = new X509Certificate2(certPath, password, X509KeyStorageFlags.Exportable);

            return certificate;
        }

        /// <summary>
        /// Devuelve un duplicado del certificado indicado
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateClone(X509Certificate2 certificate)
        {
            byte[] certBytes = certificate.Export(X509ContentType.Pkcs12, "tempPassword");

            return new X509Certificate2(
                certBytes,
                "tempPassword",
                X509KeyStorageFlags.MachineKeySet |
                X509KeyStorageFlags.PersistKeySet |
                X509KeyStorageFlags.Exportable
            );
        }

        public static string GetSerialNumber(X509Certificate2 cert)
        {
            return cert.GetSerialNumberString();
        }

#if NET40_OR_GREATER || NETCOREAPP
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
#endif

        public static bool IsSelfSigned(X509Certificate2 cert)
        {
            if (cert == null || cert.SubjectName == null || cert.IssuerName == null || cert.SubjectName.RawData == null || cert.IssuerName.RawData == null)
                return false;

#if NET40_OR_GREATER || NETCOREAPP
            return cert.SubjectName.RawData.SequenceEqual(cert.IssuerName.RawData);
#else
            byte[] subjectRawData = cert.SubjectName.RawData;
            byte[] issuerRawData = cert.IssuerName.RawData;

            if (subjectRawData.Length != issuerRawData.Length)
                return false;

            for (int i = 0; i < subjectRawData.Length; i++)
            {
                if (subjectRawData[i] != issuerRawData[i])
                    return false;
            }

            return true;
#endif
        }

        public static bool CheckIfHasPrivateKey(X509Certificate2 cert)
        {
#if NETFRAMEWORK
            return cert.PrivateKey == null;
#else
            return cert.GetRSAPrivateKey() == null;
#endif
        }

#if NET40_OR_GREATER || NETCOREAPP
        public static void GenerateP7s(string fileName, string fileP7s, X509Certificate2 certificate)
        {
            byte[] data = File.ReadAllBytes(fileName);
            ContentInfo contentInfo = new ContentInfo(data);

            // Instanciar el objeto SignedCms con el contenido
            SignedCms signedCms = new SignedCms(contentInfo, detached: true);

            // Configurar el firmante
            CmsSigner signer = new CmsSigner(certificate)
            {
                // Incluir solo el certificado final (evita buscar raíces no instaladas en Windows)
                IncludeOption = X509IncludeOption.EndCertOnly
            };

#if NET40_OR_GREATER
            // Si estás en .NET Framework / .NET 6+ y necesitas especificar SHA-256:
            signer.DigestAlgorithm = new System.Security.Cryptography.Oid("2.16.840.1.101.3.4.2.1");
#endif

            // ComputeSignature realiza la firma en silencio
            signedCms.ComputeSignature(signer, silent: true);

            // Guardar el resultado en el archivo .p7s
            File.WriteAllBytes(fileP7s, signedCms.Encode());
        }

        public static void ExtractP7m(string p7mFileName)
        {
            // Extraer archivo de un sobre .p7m
            byte[] bytesP7M = File.ReadAllBytes(p7mFileName);
            SignedCms signedCms = new SignedCms();
            signedCms.Decode(bytesP7M);

            // Verificar la firma criptográfica
            signedCms.CheckSignature(true);

            // Obtener los bytes originales y guardarlos en disco
            byte[] bytesOriginales = signedCms.ContentInfo.Content;
            File.WriteAllBytes(p7mFileName.Replace(".p7m", ""), bytesOriginales);
        }

        /// <summary>
        /// Genera un sobre digital .p7m incrustando el archivo original y la firma digital CAdES.
        /// </summary>
        /// <param name="inFileName">Ruta al archivo original (PDF, TXT, PNG, JSON, etc.)</param>
        /// <param name="outFileName">Ruta al archivo de salida .p7m</param>
        /// <param name="certificate">Certificado X509Certificate2 con clave privada</param>
        public static void CreateP7m(string inFileName, string outFileName, X509Certificate2 certificate)
        {
            if (!File.Exists(inFileName))
                throw new FileNotFoundException("El archivo de entrada no existe.", inFileName);

            if (certificate == null || !certificate.HasPrivateKey)
                throw new ArgumentException("El certificate es nulo o no contiene una clave privada válida.");

            // 1. Leer la totalidad de los bytes del archivo original
            byte[] bytesOriginales = File.ReadAllBytes(inFileName);

            // 2. Crear la estructura de datos CAdES / PKCS#7
            ContentInfo contentInfo = new ContentInfo(bytesOriginales);

            // CLAVE: detached: false -> Empaqueta los bytes originales DENTRO del archivo resultante (.p7m)
            SignedCms signedCms = new SignedCms(contentInfo, detached: false);

            // 3. Configurar el firmante con el algoritmo de resumen SHA-256 (Oid: 2.16.840.1.101.3.4.2.1)
            CmsSigner signer = new CmsSigner(certificate)
            {
                IncludeOption = X509IncludeOption.EndCertOnly,
            };

#if NET40_OR_GREATER
            // Si estás en .NET Framework / .NET 6+ y necesitas especificar SHA-256:
            signer.DigestAlgorithm = new System.Security.Cryptography.Oid("2.16.840.1.101.3.4.2.1");
#endif

            // 4. Calcular la firma criptográfica con la API nativa de Windows / .NET
            signedCms.ComputeSignature(signer, silent: false);

            // 5. Codificar la estructura ASN.1 en un array de bytes y guardarlo como .p7m
            byte[] datosP7M = signedCms.Encode();
            File.WriteAllBytes(outFileName, datosP7M);
        }
#endif
    }
}
