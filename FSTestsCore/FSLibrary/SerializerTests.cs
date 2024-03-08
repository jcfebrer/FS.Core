using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
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

            Assert.AreEqual("<![Capacity=4][Count=4][Item=prueba1][Item=prueba2][Item=prueba3][Item=prueba4]!>", result, "Clase serializada: " + result);
        }

        [TestMethod()]
        public void SerializeRectangle()
        {
            Rectangle rectangle = new Rectangle(10, 10, 100, 100);

            string result = FSLibrary.Serializer.Serialize(rectangle);

            Assert.AreEqual("<![Location={X=10,Y=10}][Size={Width=100, Height=100}][X=10][Y=10][Width=100][Height=100][Left=10][Top=10][Right=110][Bottom=110][IsEmpty=False]!>", result, "Clase serializada: " + result);
        }

        [TestMethod()]
        public void Deserialize()
        {
            string data = "<![Capacity=4][Count=4][Item=prueba1][Item=prueba2][Item=prueba3][Item=prueba4]!>";
            List<string> miclase = FSLibrary.Serializer.DeSerialize<List<string>>(data, new List<string>());

            Assert.AreEqual("prueba1", miclase[0], "Clase deserializada: " + miclase.ToString());
        }

        [TestMethod()]
        public void DeserializeRectangle()
        {
            string data = "<![Location={X=10,Y=10}][Size={Width=100, Height=100}][X=10][Y=10][Width=100][Height=100][Left=10][Top=10][Right=110][Bottom=110][IsEmpty=False]!>";
            Rectangle miclase = FSLibrary.Serializer.DeSerialize<Rectangle>(data, new Rectangle());

            Assert.AreEqual("10", miclase.X, "Clase deserializada: " + miclase.ToString());
        }
    }
}
