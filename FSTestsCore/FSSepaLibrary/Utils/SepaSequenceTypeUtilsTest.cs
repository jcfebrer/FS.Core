using System;
using FSSepaLibraryCore.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSSepaLibraryCore.Test.Utils
{
    [TestClass]
    public class SepaSequenceTypeUtilsTest
    {
        [TestMethod]
        public void ShouldRetrieveSequenceTypeFromString()
        {
            Assert.AreEqual(SepaSequenceType.OOFF, SepaSequenceTypeUtils.SepaSequenceTypeFromString("OOFF"));
            Assert.AreEqual(SepaSequenceType.FIRST, SepaSequenceTypeUtils.SepaSequenceTypeFromString("FRST"));
            Assert.AreEqual(SepaSequenceType.RCUR, SepaSequenceTypeUtils.SepaSequenceTypeFromString("RCUR"));
            Assert.AreEqual(SepaSequenceType.FINAL, SepaSequenceTypeUtils.SepaSequenceTypeFromString("FNAL"));
        }

        [TestMethod]
        public void ShouldRejectUnknownSequenceType()
        {
            try { SepaSequenceTypeUtils.SepaSequenceTypeFromString("unknown value"); }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("Unknown Sequence Type"));
            }
            
        }

        [TestMethod]
        public void ShouldRetrieveStringFromSequenceType()
        {
            Assert.AreEqual("OOFF", SepaSequenceTypeUtils.SepaSequenceTypeToString(SepaSequenceType.OOFF));
            Assert.AreEqual("FRST", SepaSequenceTypeUtils.SepaSequenceTypeToString(SepaSequenceType.FIRST));
            Assert.AreEqual("RCUR", SepaSequenceTypeUtils.SepaSequenceTypeToString(SepaSequenceType.RCUR));
            Assert.AreEqual("FNAL", SepaSequenceTypeUtils.SepaSequenceTypeToString(SepaSequenceType.FINAL));
        }
    }
}