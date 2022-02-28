using FSLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSLibrary.Tests
{
    [TestClass]
    public class DniTests
    {
        [TestMethod()]
        public void Validate()
        {
            bool res = Dni.Check("16055459x");
            Assert.AreEqual(true, res, "DNI Valido: " + res.ToString());
        }

        [TestMethod()]
        public void CalculateLetter()
        {
            string res = Dni.CalculateDNILetter("16055459");
            Assert.AreEqual("X", res, "Letra del DNI 16055459: " + res.ToString());
        }
    }
}
