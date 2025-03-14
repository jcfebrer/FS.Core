using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSLibrary;

namespace FSLibrary.Tests
{
    [TestClass]
    public class TicketBAI_CRCTests
    {
        [TestMethod()]
        public void CalcCRC()
        {
            string res = Crc8.Calculate("TBAI-00000006Y-251019-btFpwP8dcLGAF-");
            Assert.AreEqual("237", res, "TicketBai CRC: " + res.ToString());
        }

        [TestMethod()]
        public void CalcCRC2()
        {
            string res = Crc8.Calculate("https://batuz.eus/QRTBAI/?id=TBAI-00000006Y-251019-btFpwP8dcLGAF-237&s=T&nf=27174&i=4.70");
            Assert.AreEqual("007", res, "TicketBai CRC: " + res.ToString());
        }
    }
}
