using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FSCertificate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSTests.FSCertificate
{
    [TestClass]
    public class CertificateTest
    {
        [TestMethod()]
        public void TestGetCertificateByName()
        {
            X509Certificate2 cert = Certificate.GetCertificateByName("TestCertificate");

            Assert.IsNotNull(cert);
        }

        [TestMethod()]
        public void TestGetCertificateBySerialNumber()
        {
            X509Certificate2 cert = Certificate.GetCertificateBySerialNumber("50c96af9a925b0386061b5fdb6fd7215");

            Assert.IsNull(cert);
        }
    }
}
