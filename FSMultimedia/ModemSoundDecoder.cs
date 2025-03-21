#if NET40_OR_GREATER ||NETCOREAPP

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace FSMultimedia
{
    public class ModemSoundDecoder
    {
        private const int SampleRate = 44100; // Frecuencia de muestreo
        private const int BaudRate = 1200; // Velocidad en baudios
        private const int BitDuration = SampleRate / BaudRate; // Duración de cada bit en samples

        private const int MarkFreq = 1200; // Frecuencia para el bit "1"
        private const int SpaceFreq = 2200; // Frecuencia para el bit "0"

        /// <summary>
        /// Decodifica el texto contenido en un archivo WAV.
        /// </summary>
        public string DecodeFromWav(string filePath)
        {
            byte[] audioData = LoadWavData(filePath);
            return DecodeAudioToText(audioData);
        }

        private byte[] LoadWavData(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                // Saltar el encabezado WAV (44 bytes)
                reader.BaseStream.Seek(44, SeekOrigin.Begin);
                return reader.ReadBytes((int)(reader.BaseStream.Length - 44));
            }
        }

        private string DecodeAudioToText(byte[] audioData)
        {
            StringBuilder decodedText = new StringBuilder();
            int sampleCount = audioData.Length / 2; // Cada muestra es un short (16 bits)
            short[] samples = new short[sampleCount];

            // Convertir bytes en muestras de audio (16 bits)
            for (int i = 0; i < sampleCount; i++)
            {
                samples[i] = BitConverter.ToInt16(audioData, i * 2);
            }

            int index = 0;
            while (index < samples.Length - BitDuration)
            {
                // Obtener el fragmento de audio correspondiente a un bit
                short[] bitSamples = samples.Skip(index).Take(BitDuration).ToArray();

                // Analizar la frecuencia dominante con FFT
                int detectedBit = DetectBitWithFFT(bitSamples);

                if (detectedBit == -1)
                {
                    index += BitDuration;
                    continue;
                }

                // Leer un byte completo (8 bits)
                int character = 0;
                for (int i = 0; i < 8; i++)
                {
                    index += BitDuration;
                    if (index >= samples.Length) break;
                    bitSamples = samples.Skip(index).Take(BitDuration).ToArray();
                    detectedBit = DetectBitWithFFT(bitSamples);

                    if (detectedBit == -1) break;

                    character |= (detectedBit << i);
                }

                // Convertir byte en carácter ASCII
                if (character > 0 && character < 128)
                {
                    decodedText.Append((char)character);
                }

                // Avanzar al siguiente bit de parada (1)
                index += BitDuration;
            }

            return decodedText.ToString();
        }

        /// <summary>
        /// Detecta el bit (0 o 1) según la frecuencia dominante en un fragmento de audio utilizando FFT.
        /// </summary>
        private int DetectBitWithFFT(short[] samples)
        {
            double markPower = CalculateFFT(samples, MarkFreq);
            double spacePower = CalculateFFT(samples, SpaceFreq);

            if (markPower > spacePower) return 1;
            if (spacePower > markPower) return 0;

            return -1; // No se detectó un bit válido
        }

        /// <summary>
        /// Calcula la potencia de una frecuencia específica en un fragmento de audio utilizando FFT.
        /// </summary>
        private double CalculateFFT(short[] samples, int targetFrequency)
        {
            int N = samples.Length;
            Complex[] fftData = new Complex[N];

            for (int i = 0; i < N; i++)
            {
                fftData[i] = new Complex(samples[i], 0);
            }

            // Aplicar FFT (Transformada Rápida de Fourier)
            FFT(fftData);

            int targetIndex = (targetFrequency * N) / SampleRate;
            return fftData[targetIndex].Magnitude;
        }

        /// <summary>
        /// Implementación de FFT en C# (Cooley-Tukey)
        /// </summary>
        private void FFT(Complex[] buffer)
        {
            int n = buffer.Length;
            if (n <= 1) return;

            Complex[] even = new Complex[n / 2];
            Complex[] odd = new Complex[n / 2];

            for (int i = 0; i < n / 2; i++)
            {
                even[i] = buffer[i * 2];
                odd[i] = buffer[i * 2 + 1];
            }

            FFT(even);
            FFT(odd);

            for (int k = 0; k < n / 2; k++)
            {
                Complex t = Complex.Exp(-2 * Math.PI * Complex.ImaginaryOne * k / n) * odd[k];
                buffer[k] = even[k] + t;
                buffer[k + n / 2] = even[k] - t;
            }
        }
    }

}

#endif