using System.Text;
using System.Xml;
using FSSepaLibraryCore.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSSepaLibraryCore.Test.Utils
{
    [TestClass]
    public class XmlUtilsTest
    {
        [TestMethod]
        public void ShouldCreateXmlBicForAProvidedBic()
        {
            var xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", Encoding.UTF8.BodyName, "yes"));
            var el = (XmlElement?)xml.AppendChild(xml.CreateElement("Document"));

            XmlUtils.CreateBic(el, new SepaIbanData { Bic="01234567" });
            Assert.AreEqual("<FinInstnId><BIC>01234567</BIC></FinInstnId>", el.InnerXml);
        }

        [TestMethod]
        public void ShouldCreateXmlUnknownBicForAnUnknwonBic()
        {
            var xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", Encoding.UTF8.BodyName, "yes"));
            var el = (XmlElement?)xml.AppendChild(xml.CreateElement("Document"));

            XmlUtils.CreateBic(el, new SepaIbanData { UnknownBic = true});
            Assert.AreEqual("<FinInstnId><Othr><Id>NOTPROVIDED</Id></Othr></FinInstnId>", el.InnerXml);
        }
    }
}