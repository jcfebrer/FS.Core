using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSLibrary.Tests
{
    [TestClass]
    public class Serializer
    {
        [TestMethod()]
        public void Serialize()
        {
            List<string> miclase = new List<string>();
            miclase.Add("prueba1");
            miclase.Add("prueba2");
            miclase.Add("prueba3");
            miclase.Add("prueba4");

            string result = FSLibrary.Serializer.Serialize(miclase);

            Assert.AreEqual("237", result, "Clase serializada: " + result);
        }

        [TestMethod()]
        public void Deserialize()
        {
            string data = "";
            List<string> miclase = FSLibrary.Serializer.DeSerialize<List<string>>(data, new List<string>());

            Assert.AreEqual("prueba1", miclase[0], "Clase deserializada: " + miclase.ToString());
        }
    }
}
