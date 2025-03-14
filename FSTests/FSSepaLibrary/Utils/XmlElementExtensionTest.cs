using System.Text;
using System.Xml;
using FSSepaLibrary.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSSepaLibrary.Test.Utils
{
    [TestClass]
    public class XmlElementExtensionTest
    {
        private const string name = "sample";
        private const string name2 = "sample2";
        private const string name3 = "sample3";
        private const decimal value = 12.5m;

        public XmlElement Prepare()
        {
            var document = new XmlDocument();
            document.AppendChild(document.CreateXmlDeclaration("1.0", Encoding.UTF8.BodyName, "yes"));
            return (XmlElement) document.AppendChild(document.CreateElement("Document"));
        }

        [TestMethod]
        public void ShouldAddMultipleOrderedNewElement()
        {
            var element = Prepare();
            var el = element.NewElement(name);
            var el2 = element.NewElement(name2);
            var el3 = element.NewElement(name3);
            Assert.IsTrue(element.HasChildNodes);
            Assert.AreEqual(3, element.ChildNodes.Count);
            Assert.AreEqual(el, element.FirstChild);
            Assert.AreEqual(el2, element.ChildNodes[1]);
            Assert.AreEqual(el3, element.LastChild);
        }

        [TestMethod]
        public void ShouldAddNewElementWithAValue()
        {
            var element = Prepare();
            var el = element.NewElement(name, value);
            Assert.AreEqual(name, el.Name);
            Assert.AreEqual(value.ToString(), el.InnerText);
            Assert.IsTrue(element.HasChildNodes);
            Assert.AreEqual(1, element.ChildNodes.Count);
        }

        [TestMethod]
        public void ShouldAddNewElementWithoutValue()
        {
            var element = Prepare();
            var el = element.NewElement(name);
            Assert.AreEqual(name, el.Name);
            Assert.IsTrue(string.IsNullOrEmpty(el.InnerText));
            Assert.IsTrue(element.HasChildNodes);
            Assert.AreEqual(1, element.ChildNodes.Count);
        }

        [TestMethod]
        public void ShouldAddNewElementExplicitlyWithoutValue()
        {
            var element = Prepare();
            var el = element.NewElement(name, null);
            Assert.AreEqual(name, el.Name);
            Assert.IsTrue(string.IsNullOrEmpty(el.InnerText));
            Assert.IsTrue(element.HasChildNodes);
            Assert.AreEqual(1, element.ChildNodes.Count);
        }
    }
}