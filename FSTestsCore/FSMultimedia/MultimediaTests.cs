using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FSLibraryCore.Tests
{
    [TestClass]
    public class MultimediaTests
    {
        [TestMethod()]
        public void Beeper()
        {
            Multimedia.BeepZx(1, 0);
            //Multimedia.BeepZx(1, 2);
            //Multimedia.BeepZx(0.5, 3);
            //Multimedia.BeepZx(0.5, 2);
            //Multimedia.BeepZx(1, 0);
            //Multimedia.BeepZx(1, 0);
            //Multimedia.BeepZx(1, 2);
            //Multimedia.BeepZx(0.5, 3);
            //Multimedia.BeepZx(0.5, 2);
            //Multimedia.BeepZx(1, 0);
            //Multimedia.BeepZx(1, 3);
            //Multimedia.BeepZx(1, 5);
            //Multimedia.BeepZx(2, 7);
            //Multimedia.BeepZx(1, 3);
            //Multimedia.BeepZx(1, 5);
            //Multimedia.BeepZx(2, 7);
            //Multimedia.BeepZx(.75, 7);
            //Multimedia.BeepZx(.25, 8);
            //Multimedia.BeepZx(.5, 7);
            //Multimedia.BeepZx(.5, 5);
            //Multimedia.BeepZx(.5, 3);
            //Multimedia.BeepZx(.5, 2);
            //Multimedia.BeepZx(1, 0);
            //Multimedia.BeepZx(.75, 7);
            //Multimedia.BeepZx(.25, 8);
            //Multimedia.BeepZx(.5, 7);
            //Multimedia.BeepZx(.5, 5);
            //Multimedia.BeepZx(.5, 3);
            //Multimedia.BeepZx(.5, 2);
            //Multimedia.BeepZx(1, 0);
            //Multimedia.BeepZx(1, 0);
            //Multimedia.BeepZx(1, -5);
            //Multimedia.BeepZx(2, 0);
            //Multimedia.BeepZx(1, 0);
            //Multimedia.BeepZx(1, -5);
            //Multimedia.BeepZx(2, 0);

            //for (int b = 1; b < 50; b++)
            //{
            //    for (int c = 1; c < 40; c++)
            //    {
            //        for (int d = 1; d < 20; d++)
            //        {
            //            //Console.Beep(d * 100, 1 * 100);
            //            //Console.Beep(c * 100, 1 * 100);
            //            Multimedia.BeepZx(d * 1000, 1 * 100);
            //            Multimedia.Beep(c * 1000, 1 * 100);
            //        }
            //    }
            //}
        }

        [TestMethod]
        public void GenerateZXSpectrumWav()
        {
            bool boolResult = true;
            try
            {
                string filePath = "zx_spectrum_key.wav";
                if (!System.IO.File.Exists(filePath))
                {
                    Multimedia.GenerateWav(filePath, 44100, 100, 800);
                    Console.WriteLine($"Archivo generado: {filePath}");
                }
                boolResult = true;
            }
            catch (Exception)
            {
                boolResult = false;
            }

            Assert.AreEqual(boolResult, true);
        }
    }
}
