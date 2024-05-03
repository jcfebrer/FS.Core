using FSCryptoCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTestsCore
{
    [TestClass()]
    public class FSCryptoTests
    {
        [TestMethod()]
        public void Md5()
        {
            string c = FSCryptoCore.Md5.Calc("16055459");

            Assert.AreEqual("C758647C2A8CBC92E55A6765525E7D2B", c, "Cálculo de MD5 incorrecto: " + c);
        }

        [TestMethod()]
        public void Sha256()
        {
            string c = FSCryptoCore.Sha256.Calc("16055459");

            Assert.AreEqual("480C48B710C892B02CA8A26486F73C0BC9C3134AD036E46BA1D8701F9639F487", c, "Cálculo de SHA256 incorrecto: " + c);
        }

        [TestMethod()]
        public void Crypto()
        {
            Crypto crypto = new Crypto();

            string c = crypto.Crypt("prueba");

            Assert.AreEqual("tDtOPAUOILk=", c, "Cálculo de CRYPT incorrecto: " + c);

            string r = crypto.Decryp("tDtOPAUOILk=");

            Assert.AreEqual("prueba", r, "Decrypt CRYPT incorrecto: " + r);
        }

        [TestMethod()]
        public void Encode64()
        {
            string base64 = FSCryptoCore.Base64.Encode("81354", true);

            Assert.AreEqual("ODEzNTQ=", base64, "Encode error");
        }

        [TestMethod()]
        public void Decode64()
        {
            string base64 = FSCryptoCore.Base64.Decode("ODEzNTQ=", true);

            Assert.AreEqual("81354", base64, "Decode error");
        }
    }
}
