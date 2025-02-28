using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FSMultimediaCore.Test
{
    [TestClass]
    public class ZXModemSoundGeneratorTest
    {
        [TestMethod()]
        public void TestSound()
        {
            ZXModemSoundGenerator modem = new ZXModemSoundGenerator();

            string text = "ATDT555-1234"; // Mensaje simulado de un módem

            modem.GenerateAndPlay(text);  // 🔊 Reproduce el sonido

            modem.GenerateAndSaveToFile(text, "modem.wav"); // 💾 Guarda el sonido en WAV

            // Abrimos el fichero wav y lo decodificamos.
            ModemSoundDecoder decoder = new ModemSoundDecoder();

            string result = decoder.DecodeFromWav("modem.wav");

            Assert.AreEqual(text, result);
        }
    }
}
