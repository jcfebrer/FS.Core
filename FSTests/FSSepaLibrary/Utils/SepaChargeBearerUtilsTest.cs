using System;
using FSSepaLibrary.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSSepaLibrary.Test.Utils
{
    [TestClass]
    public class SepaChargeBearerUtilsTest
    {
        [TestMethod]
        public void ShouldRetrieveChargeBearerFromString()
        {
            Assert.AreEqual(SepaChargeBearer.CRED,  SepaChargeBearerUtils.SepaChargeBearerFromString("CRED"));
            Assert.AreEqual(SepaChargeBearer.DEBT, SepaChargeBearerUtils.SepaChargeBearerFromString("DEBT"));
            Assert.AreEqual(SepaChargeBearer.SHAR,  SepaChargeBearerUtils.SepaChargeBearerFromString("SHAR"));
        }

        [TestMethod]
        public void ShouldRejectUnknownChargeBearer()
        {
            try { SepaChargeBearerUtils.SepaChargeBearerFromString("unknown value"); }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("Unknown Charge Bearer"));
            }
            
        }

        [TestMethod]
        public void ShouldRetrieveStringFromChargeBearer()
        {
            Assert.AreEqual("CRED", SepaChargeBearerUtils.SepaChargeBearerToString(SepaChargeBearer.CRED));
            Assert.AreEqual("DEBT", SepaChargeBearerUtils.SepaChargeBearerToString(SepaChargeBearer.DEBT));
            Assert.AreEqual("SHAR", SepaChargeBearerUtils.SepaChargeBearerToString(SepaChargeBearer.SHAR));
        }
    }
}