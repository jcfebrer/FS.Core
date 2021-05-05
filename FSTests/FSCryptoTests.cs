using FSCrypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTests
{
    [TestClass()]
    public class FSCryptoTests
    {
        [TestMethod()]
        public void Md5()
        {
            Crypto crypto = new Crypto();

            string c = crypto.Md5("16055459");

            Assert.AreEqual("C758647C2A8CBC92E55A6765525E7D2B", c, "Cálculo de MD5 incorrecto: " + c);

        }

        [TestMethod()]
        public void Crypto()
        {
            Crypto crypto = new Crypto();

            string c = crypto.Crypt("prueba");

            Assert.AreEqual("WipZhqB3yIo=", c, "Cálculo de CRYPT incorrecto: " + c);

            string r = crypto.Decryp("WipZhqB3yIo=");

            Assert.AreEqual("prueba", r, "Decript CRYPT incorrecto: " + r);
        }

        [TestMethod()]
        public void Encode64()
        {
            string base64 = FSCrypto.Utils.EncodeBase64("81354", true);

            Assert.AreEqual("ODEzNTQ=", base64, "Encode error");
        }

        [TestMethod()]
        public void Decode64()
        {
            string base64 = FSCrypto.Utils.DecodeBase64("ODEzNTQ=", true);

            Assert.AreEqual("81354", base64, "Decode error");
        }
    }
}
