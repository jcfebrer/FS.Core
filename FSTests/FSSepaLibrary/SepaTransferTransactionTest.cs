using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSSepaLibrary.Test
{
    [TestClass]
    public class SepaTransferTransactionTest
    {
        [TestMethod]
        public void ShouldRejectAmountGreaterOrEqualsThan1000000000()
        {
            try { new SepaCreditTransferTransaction { Amount = 1000000000 }; }
            catch (SepaRuleException e)
            {
                Assert.IsTrue(e.Message.Contains("Invalid amount value"));
            }
        }

        [TestMethod]
        public void ShouldRejectAmountLessThan1Cents()
        {
            try { new SepaCreditTransferTransaction { Amount = 0 }; }
            catch (SepaRuleException e)
            {
                Assert.IsTrue(e.Message.Contains("Invalid amount value"));
            }
        }

        [TestMethod]
        public void ShouldRejectAmountWithMoreThan2Decimals()
        {
            try { new SepaCreditTransferTransaction { Amount = 12.012m }; }
            catch (SepaRuleException e)
            {
                Assert.IsTrue(e.Message.Contains("Amount should have at most 2 decimals"));
            }
        }

        [TestMethod]
        public void ShouldRejectEndToEndIdGreaterThan35()
        {
            try { new SepaCreditTransferTransaction { EndToEndId = "012345678901234567890123456789012345" }; }
            catch (SepaRuleException e)
            {
                Assert.IsTrue(e.Message.Contains("cannot be greater than 35"));
            }
        }
    }
}