using System;
using System.IO;
using System.Media;
using System.Text;

namespace FSMultimediaCore
{
    /// <summary>
    /// Genera un sonido de carga ZX Spectrum basado en texto y lo guarda en un archivo WAV.
    /// </summary>
    public class ZXModemSoundGenerator
    {
        private const int SampleRate = 44100; // Frecuencia de muestreo en Hz
        private const int BaudRate = 1500; // Velocidad en baudios para ZX Spectrum
        private const int BitDurationSamples = SampleRate / BaudRate; // Duración de cada bit en muestras

        private const int Freq0 = 1200; // Frecuencia para el bit "0" (dos ciclos por bit)
        private const int Freq1 = 2400; // Frecuencia para el bit "1" (un ciclo por bit)

        /// <summary>
        /// Genera y reproduce el sonido de módem basado en el texto proporcionado.
        /// </summary>
        public void GenerateAndPlay(string text)
        {
            string filePath = "modem_sound.wav";
            GenerateAndSaveToFile(text, filePath);
            PlayWav(filePath);
        }

        /// <summary>
        /// Genera el sonido a partir del texto y lo guarda en un archivo WAV.
        /// </summary>
        public void GenerateAndSaveToFile(string text, string filePath)
        {
            byte[] audioData = GenerateAudioData(text);
            SaveToWavFile(audioData, filePath);
            Console.WriteLine($"Archivo WAV guardado en: {filePath}");
        }

        /// <summary>
        /// Convierte el texto en una secuencia de bits y lo transforma en audio.
        /// </summary>
        private byte[] GenerateAudioData(string text)
        {
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Convertimos el texto en bytes ASCII
                byte[] dataBytes = Encoding.ASCII.GetBytes(text);

                // 🔹 Generamos el tono piloto para sincronización (~8063 ciclos a 1200 Hz)
                GeneratePilotTone(writer, 8063, Freq0);

                // 🔹 Convertimos los datos a audio
                foreach (byte b in dataBytes)
                {
                    // Bit de inicio (SIEMPRE 0)
                    GenerateTone(writer, Freq0, BitDurationSamples);

                    // 8 bits de datos (LSB a MSB)
                    for (int i = 0; i < 8; i++)
                    {
                        int bit = (b >> i) & 1;
                        int freq = (bit == 1) ? Freq1 : Freq0;
                        GenerateTone(writer, freq, BitDurationSamples);
                    }

                    // Bit de parada (SIEMPRE 1)
                    GenerateTone(writer, Freq1, BitDurationSamples);
                }

                writer.Flush();
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Genera el tono piloto inicial para sincronización en ZX Spectrum.
        /// </summary>
        private void GeneratePilotTone(BinaryWriter writer, int cycles, int frequency)
        {
            for (int i = 0; i < cycles; i++)
            {
                GenerateTone(writer, frequency, BitDurationSamples);
            }
        }

        /// <summary>
        /// Genera una onda sinusoidal para representar un bit en el audio.
        /// </summary>
        private void GenerateTone(BinaryWriter writer, int frequency, int durationSamples)
        {
            for (int i = 0; i < durationSamples; i++)
            {
                double t = (double)i / SampleRate;
                short sample = (short)(Math.Sin(2 * Math.PI * frequency * t) * short.MaxValue);
                writer.Write(sample);
            }
        }

        /// <summary>
        /// Guarda el audio generado en un archivo WAV con formato PCM.
        /// </summary>
        private void SaveToWavFile(byte[] audioData, string filePath)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Create(filePath)))
            {
                // Encabezado del archivo WAV
                writer.Write(Encoding.ASCII.GetBytes("RIFF"));
                writer.Write(36 + audioData.Length);
                writer.Write(Encoding.ASCII.GetBytes("WAVE"));

                // Subchunk "fmt "
                writer.Write(Encoding.ASCII.GetBytes("fmt "));
                writer.Write(16);
                writer.Write((short)1); // AudioFormat (PCM)
                writer.Write((short)1); // NumChannels (Mono)
                writer.Write(SampleRate);
                writer.Write(SampleRate * 2); // ByteRate
                writer.Write((short)2); // BlockAlign
                writer.Write((short)16); // BitsPerSample

                // Subchunk "data"
                writer.Write(Encoding.ASCII.GetBytes("data"));
                writer.Write(audioData.Length);
                writer.Write(audioData);
            }
        }

        private void PlayWav(string filePath)
        {
            using (SoundPlayer player = new SoundPlayer(filePath))
            {
                player.PlaySync();
            }
        }
    }
}
