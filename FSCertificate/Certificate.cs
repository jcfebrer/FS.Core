using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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

        public static string SignMessage(string Mensaje, System.Security.Cryptography.X509Certificates.X509Certificate2 Certificado)
        {
            if (Certificado == null)
                return null;

            byte[] msgBytes = Encoding.Unicode.GetBytes(Mensaje);
            ContentInfo content = new ContentInfo(msgBytes);
            SignedCms sign = new SignedCms(content);
            CmsSigner signer = new CmsSigner(Certificado);
            sign.ComputeSignature(signer);
            byte[] msgSign = sign.Encode();

            return Encoding.Unicode.GetString(msgSign);
        }
    }
}
