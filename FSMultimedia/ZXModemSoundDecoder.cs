using System;
using System.IO;
using System.Text;

#if NET35_OR_GREATER || NETCOREAPP
    using System.Linq;
#endif

namespace FSMultimedia
{
    /// <summary>
    /// Decodifica un archivo WAV generado en formato ZX Spectrum y extrae el texto contenido.
    /// </summary>
    public class ZXModemSoundDecoder
    {
        private const int SampleRate = 44100; // Frecuencia de muestreo
        private const int BaudRate = 1500; // Velocidad en baudios
        private const int BitDurationSamples = SampleRate / BaudRate; // Duración de cada bit en muestras

        private const int Freq0 = 1200; // Frecuencia para el bit "0"
        private const int Freq1 = 2400; // Frecuencia para el bit "1"

        /// <summary>
        /// Carga y decodifica un archivo WAV para obtener el texto almacenado.
        /// </summary>
        public string DecodeFromWav(string filePath)
        {
            byte[] audioData = LoadWavData(filePath);
            return DecodeAudioToText(audioData);
        }

        /// <summary>
        /// Carga los datos de audio desde un archivo WAV.
        /// </summary>
        private byte[] LoadWavData(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                reader.BaseStream.Seek(44, SeekOrigin.Begin); // Saltar el encabezado WAV
                return reader.ReadBytes((int)(reader.BaseStream.Length - 44));
            }
        }

        /// <summary>
        /// Convierte los datos de audio en texto, detectando los tonos de 1200 Hz y 2400 Hz.
        /// </summary>
        private string DecodeAudioToText(byte[] audioData)
        {
            StringBuilder decodedText = new StringBuilder();
            int sampleCount = audioData.Length / 2;
            short[] samples = new short[sampleCount];

            // Convertir los bytes en muestras de audio
            for (int i = 0; i < sampleCount; i++)
            {
                samples[i] = BitConverter.ToInt16(audioData, i * 2);
            }

            int index = 0;
            while (index < samples.Length - BitDurationSamples)
            {
                int detectedBit = DetectBit(samples, index);

                if (detectedBit == -1)
                {
                    index += BitDurationSamples;
                    continue;
                }

                int character = 0;
                for (int i = 0; i < 8; i++)
                {
                    index += BitDurationSamples;
                    if (index >= samples.Length) break;
                    detectedBit = DetectBit(samples, index);
                    if (detectedBit == -1) break;

                    character |= (detectedBit << i);
                }

                if (character > 0 && character < 128)
                {
                    decodedText.Append((char)character);
                }

                index += BitDurationSamples;
            }

            return decodedText.ToString();
        }

        /// <summary>
        /// Detecta si un fragmento de audio representa un bit "0" o "1".
        /// </summary>
        private int DetectBit(short[] samples, int startIndex)
        {
#if NET35_OR_GREATER || NETCOREAPP
            double avgPower = samples.Skip(startIndex).Take(BitDurationSamples).Average(s => Math.Abs(s));
#else
            double avgPower = CalculateAveragePower(samples, startIndex, BitDurationSamples);
#endif
            return avgPower > 1000 ? 1 : 0; // Umbral de detección
        }

        /// <summary>
        /// Calculamos la potencia media
        /// </summary>
        /// <param name="samples"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private double CalculateAveragePower(short[] samples, int startIndex, int count)
        {
            if (samples == null || startIndex < 0 || count <= 0 || startIndex + count > samples.Length)
                throw new ArgumentException("Invalid parameters.");

            double sum = 0;
            for (int i = startIndex; i < startIndex + count; i++)
                sum += Math.Abs(samples[i]);
            return sum / count;
        }
    }
}