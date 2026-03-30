#if NET45_OR_GREATER || NETCOREAPP
using System;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FSCertificate
{
    public class CertificateUrl
    {
        public static X509Certificate2 GetCertificateByUrl(string url)
        {
            return GetCertificateByUrlAsync(url).Result;
        }

        public static async Task<X509Certificate2> GetCertificateByUrlAsync(string url)
        {
            X509Certificate2 certification = null;

            var handler = new HttpClientHandler
            {
                // Este callback se ejecuta cuando el cliente recibe el certificado del servidor
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    certification = new X509Certificate2(cert); // Guardamos el certificado para devolverlo después

                    if (cert == null) return false;

                    //Console.WriteLine($"Sujeto: {cert.Subject}");
                    //Console.WriteLine($"Emisor: {cert.Issuer}");
                    //Console.WriteLine($"Válido desde: {cert.NotBefore}");
                    //Console.WriteLine($"Fecha de caducidad: {cert.NotAfter}");

                    // Comprobación de caducidad manual
                    if (DateTime.Now > cert.NotAfter)
                    {
                        //Console.WriteLine("❌ El certificado ha caducado.");
                    }
                    else
                    {
                        //Console.WriteLine($"✅ Válido por {(cert.NotAfter - DateTime.Now).Days} días más.");
                    }

                    // Comprobación de errores de cadena (SSL Policy Errors)
                    if (errors == System.Net.Security.SslPolicyErrors.None)
                    {
                        //Console.WriteLine("✅ La cadena de confianza es válida.");
                    }
                    else
                    {
                        //Console.WriteLine($"⚠️ Errores de validación: {errors}");
                    }

                    // Validar si es válido actualmente
                    //Console.WriteLine($"¿Confianza del SO?: {cert.Verify()}");

                    return true; // Permitimos que la petición continúe o termine aquí
                }
            };

            using (var client = new HttpClient(handler))
            {
                await client.GetAsync(url);

                return certification; // Devolvemos el certificado obtenido
            }
        }

        public static X509Certificate2 GetCertificateByHost(string host, int port = 443)
        {
            return GetCertificateByHostAsync(host, port).Result;
        }

        public static async Task<X509Certificate2> GetCertificateByHostAsync(string host, int port = 443)
        {
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync(host, port);

                using (SslStream stream = new SslStream(client.GetStream(), false,
                    (sender, certificate, chain, errors) => true)) // Aceptamos todo para inspeccionarlo
                {
                    await stream.AuthenticateAsClientAsync(host);
                    var cert = new X509Certificate2(stream.RemoteCertificate);

                    //Console.WriteLine($"Certificado para: {host}");
                    //Console.WriteLine($"Válido desde: {cert.NotBefore}");
                    //Console.WriteLine($"Expira el: {cert.NotAfter}");

                    // Validar si es válido actualmente
                    //Console.WriteLine($"¿Confianza del SO?: {cert.Verify()}");

                    return cert;
                }
            }
        }
    }
}
#endif