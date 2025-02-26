using System;
using System.IO;
using System.Media;
using System.Text;

namespace FSMultimedia
{
    public class ModemSoundGenerator
    {
        private const int SampleRate = 44100; // Frecuencia de muestreo estándar
        private const int BaudRate = 1200; // Velocidad en baudios
        private const int BitDurationSamples = SampleRate / BaudRate; // Duración de cada bit en muestras

        private const int MarkFreq = 1200; // Frecuencia para el bit "1"
        private const int SpaceFreq = 2200; // Frecuencia para el bit "0"

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
        /// Genera el sonido de módem y lo guarda en un archivo WAV.
        /// </summary>
        public void GenerateAndSaveToFile(string text, string filePath)
        {
            byte[] audioData = GenerateAudioData(text);
            SaveToWavFile(audioData, filePath);
            Console.WriteLine($"Archivo WAV guardado en: {filePath}");
        }

        private byte[] GenerateAudioData(string text)
        {
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Convertimos el texto en bits según ASCII
                byte[] dataBytes = Encoding.ASCII.GetBytes(text);

                foreach (byte b in dataBytes)
                {
                    // Bit de inicio (SIEMPRE 0)
                    GenerateTone(writer, SpaceFreq, BitDurationSamples);

                    // 8 bits de datos (LSB a MSB)
                    for (int i = 0; i < 8; i++)
                    {
                        int bit = (b >> i) & 1;
                        int freq = (bit == 1) ? MarkFreq : SpaceFreq;
                        GenerateTone(writer, freq, BitDurationSamples);
                    }

                    // Bit de parada (SIEMPRE 1)
                    GenerateTone(writer, MarkFreq, BitDurationSamples);
                }

                writer.Flush();
                return stream.ToArray();
            }
        }

        private void GenerateTone(BinaryWriter writer, int frequency, int durationSamples)
        {
            for (int i = 0; i < durationSamples; i++)
            {
                double t = (double)i / SampleRate;
                short sample = (short)(Math.Sin(2 * Math.PI * frequency * t) * short.MaxValue);
                writer.Write(sample);
            }
        }

        private void SaveToWavFile(byte[] audioData, string filePath)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Create(filePath)))
            {
                // Escribir encabezado WAV
                writer.Write(Encoding.ASCII.GetBytes("RIFF"));
                writer.Write(36 + audioData.Length); // Tamaño total del archivo
                writer.Write(Encoding.ASCII.GetBytes("WAVE"));

                writer.Write(Encoding.ASCII.GetBytes("fmt "));
                writer.Write(16); // Subchunk1Size
                writer.Write((short)1); // AudioFormat (PCM)
                writer.Write((short)1); // NumChannels (Mono)
                writer.Write(SampleRate); // SampleRate
                writer.Write(SampleRate * 2); // ByteRate
                writer.Write((short)2); // BlockAlign
                writer.Write((short)16); // BitsPerSample

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
