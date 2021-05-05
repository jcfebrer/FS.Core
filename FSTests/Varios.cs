using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTests
{
    [TestClass()]
    public class Varios
    {
        [TestMethod()]
        public void Flip()
        {
            string flip = FSLibrary.Flip.FlipString("esto es una prueba");

            Assert.AreEqual("ɐqәnɹd ɐun sә oʇsә", flip, "Función 'Flip' incorrecta: " + flip);
        }
    }
}
