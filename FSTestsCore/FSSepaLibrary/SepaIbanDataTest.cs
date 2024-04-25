using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSSepaLibraryCore.Test
{
    [TestClass]
    public class SepaIbanDataTest
    {
        private const string Bic = "SOGEFRPPXXX";
        private const string Iban = "FR7030002005500000157845Z02";
        private const string IbanWithSpace = "FR70 30002  005500000157845Z    02";
        private const string Name = "A_NAME";

        [TestMethod]
        public void ShouldBeValidIfAllDataIsNotNull()
        {
            var data = new SepaIbanData
                {
                    Bic = Bic,
                    Iban = Iban,
                    Name = Name
                };

            Assert.IsTrue(data.IsValid);
        }

        [TestMethod]
        public void ShouldBeValidIfAllDataIsNotNullAndBicIsUnknown()
        {
            var data = new SepaIbanData
            {
                UnknownBic = true,
                Iban = Iban,
                Name = Name
            };

            Assert.IsTrue(data.IsValid);
        }
        
        [TestMethod]
        public void ShouldRemoveSpaceInIban()
        {
            var data = new SepaIbanData
            {
                Bic = Bic,
                Iban = IbanWithSpace,
                Name = Name
            };

            Assert.IsTrue(data.IsValid);
            Assert.AreEqual(Iban, data.Iban);
        }

        [TestMethod]
        public void ShouldKeepNameIfLessThan70Chars()
        {
            var data = new SepaIbanData
                {
                    Bic = Bic,
                    Iban = Iban,
                    Name = Name
                };

            Assert.AreEqual(Bic, data.Bic);
            Assert.AreEqual(Name, data.Name);
            Assert.AreEqual(Iban, data.Iban);
        }

        [TestMethod]
        public void ShouldNotBeValidIfBicIsNull()
        {
            var data = new SepaIbanData
                {
                    Iban = Iban,
                    Name = Name
                };

            Assert.IsFalse(data.IsValid);
        }

        [TestMethod]
        public void ShouldNotBeValidIfIbanIsNull()
        {
            var data = new SepaIbanData
                {
                    Bic = Bic,
                    Name = Name
                };

            Assert.IsFalse(data.IsValid);
        }

        [TestMethod]
        public void ShouldNotBeValidIfNameIsNull()
        {
            var data = new SepaIbanData
                {
                    Bic = Bic,
                    Iban = Iban
                };

            Assert.IsFalse(data.IsValid);
        }

        [TestMethod]
        public void ShouldReduceNameIfGreaterThan70Chars()
        {
            const string longName = "12345678901234567890123456789012345678901234567890123456789012345678901234567890";
            const string expectedName = "1234567890123456789012345678901234567890123456789012345678901234567890";
            var data = new SepaIbanData
                {
                    Bic = Bic,
                    Iban = Iban,
                    Name = longName
                };

            Assert.AreEqual(expectedName, data.Name);
        }

        [TestMethod]
        public void ShouldRejectBadBic()
        {
            try { new SepaIbanData { Bic = "BIC" }; }
            catch (SepaRuleException e)
            {
                Assert.IsTrue(e.Message.Contains("Null or Invalid length of BIC"));
            }
        }

        [TestMethod]
        public void ShouldRejectTooLongIban()
        {
            try { new SepaIbanData { Iban = "FR012345678901234567890123456789012" }; }
            catch (SepaRuleException e)
            {
                Assert.IsTrue(e.Message.Contains("Null or Invalid length of IBAN code"));
            }
        }

        [TestMethod]
        public void ShouldRejectTooShortIban()
        {
            try { new SepaIbanData { Iban = "FR01234567890" }; }
            catch (SepaRuleException e)
            {
                Assert.IsTrue(e.Message.Contains("Null or Invalid length of IBAN code"));
            }
        }
    }
}