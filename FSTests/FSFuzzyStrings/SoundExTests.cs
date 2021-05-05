using Microsoft.VisualStudio.TestTools.UnitTesting;
using FSFuzzyStrings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSFuzzyStrings.Tests
{
    [TestClass()]
    public class SoundExTests
    {
        [TestMethod()]
        public void DoTest()
        {
            string res = SoundEx.Do("en un país multicolor");
            Assert.AreEqual("E551", res, "SoundEx incorrecto.");
        }

        [TestMethod()]
        public void SoundEx2Test()
        {
            string res = SoundEx.SoundEx2("nacio una abeja bajo el sol");
            Assert.AreEqual("N251", res, "SoundEx2 incorrecto.");
        }
    }
}