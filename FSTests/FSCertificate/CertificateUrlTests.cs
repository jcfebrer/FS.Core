using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSCertificate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSTests.FSCertificate
{
    [TestClass]
    public class CertificateUrlTests
    {
        public CertificateUrlTests()
        {
        }
#if NETCOREAPP
        [TestMethod]
        public void TestGetSslCertificate()
        {
            var cert = CertificateUrl.GetCertificateByUrl("https://www.google.com");

            if (cert != null)
            {
                Console.WriteLine($"Sujeto: {cert.Subject}");
                Console.WriteLine($"Emisor: {cert.Issuer}");
                Console.WriteLine($"Válido desde: {cert.NotBefore}");
                Console.WriteLine($"Fecha de caducidad: {cert.NotAfter}");

                // Comprobación de caducidad manual
                if (DateTime.Now > cert.NotAfter)
                {
                    Console.WriteLine("❌ El certificado ha caducado.");
                }
                else
                {
                    Console.WriteLine($"✅ Válido por {(cert.NotAfter - DateTime.Now).Days} días más.");
                }

                // Validar si es válido actualmente
                Console.WriteLine($"¿Confianza del SO?: {cert.Verify()}");
            }
            else
            {
                Console.WriteLine("Error al obtener el certificado SSL.");
                Assert.Fail("CheckSslCertificate returned null");
            }
        }

        [TestMethod]
        public void TestGetSslCertificateDirect()
        {
            var cert = CertificateUrl.GetCertificateByHost("142.251.142.132");

            if (cert != null)
            {
                Console.WriteLine($"Sujeto: {cert.Subject}");
                Console.WriteLine($"Emisor: {cert.Issuer}");
                Console.WriteLine($"Válido desde: {cert.NotBefore}");
                Console.WriteLine($"Fecha de caducidad: {cert.NotAfter}");

                // Comprobación de caducidad manual
                if (DateTime.Now > cert.NotAfter)
                {
                    Console.WriteLine("❌ El certificado ha caducado.");
                }
                else
                {
                    Console.WriteLine($"✅ Válido por {(cert.NotAfter - DateTime.Now).Days} días más.");
                }

                // Validar si es válido actualmente
                Console.WriteLine($"¿Confianza del SO?: {cert.Verify()}");
            }
            else
            {
                Console.WriteLine("Error al obtener el certificado SSL.");
                Assert.Fail("CheckSslCertificate returned null");
            }
        }
#endif
    }
}