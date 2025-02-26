using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FSMultimediaCore.Test
{
    [TestClass]
    public class ModemSoundGeneratorTest
    {
        [TestMethod()]
        public void TestSound()
        {
            ModemSoundGenerator modem = new ModemSoundGenerator();

            string text = "ATDT555-1234"; // Mensaje simulado de un módem

            modem.GenerateAndPlay(text);  // 🔊 Reproduce el sonido

            modem.GenerateAndSaveToFile(text, "modem.wav"); // 💾 Guarda el sonido en WAV

            // Abrimos el fichero wav y lo decodificamos.
            ModemSoundDecoder decoder = new ModemSoundDecoder();

            string result = decoder.DecodeFromWav("modem.wav");

            Console.WriteLine("Texto recuperado: " + result);
        }
    }
}
