using FSNetwork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FSTests.FSNetwork
{
    [TestClass()]
    public class FSNetworkTests
    {
        [TestMethod()]
        public void NetworkConnection()
        {
            NetworkCredential writeCredentials = new NetworkCredential("user", "password", "domain");

            using (new NetworkConnection(@"\\192.168.0.107\FileShare1", writeCredentials))
            {
                Directory.CreateDirectory("\\\\192.168.0.107\\FileShare1\\DatosGT\\SRP\\DocumentosGt\\Servicios\\2030");
            }
        }

        [TestMethod()]
        public void GetFromUrl()
        {
            string result = Http.GetFromUrl("http://www.febrersoftware.com");

            Assert.IsNull(result);
        }
    }
}
