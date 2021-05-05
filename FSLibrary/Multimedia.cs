using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FSLibrary
{
    /// <summary>
    /// Librería multimedia
    /// </summary>
    public static class Multimedia
    {
        private static SoundPlayer keySound = null;

        /// <summary>
        /// Plays the wav.
        /// </summary>
        /// <param name="wavFile">The wav file.</param>
        public static void PlayWav(string wavFile)
        {
            new System.Threading.Thread(() =>
            {
                if (keySound == null)
                    keySound = new SoundPlayer(wavFile);
                keySound.Play();
            }).Start();
        }

        /// <summary>
        /// Plays the wav synchronize.
        /// </summary>
        /// <param name="wavFile">The wav file.</param>
        public static void PlayWavSync(string wavFile)
        {
            if (keySound == null)
                keySound = new SoundPlayer(wavFile);
            keySound.PlaySync();
        }

        /// <summary>
        /// Beeps the specified frequency.
        /// </summary>
        /// <param name="Frequency">The frequency.</param>
        /// <param name="Duration">The duration.</param>
        public static void Beep(double Frequency, double Duration)
        {
            Beep(1, Frequency, Duration);
        }

        /// <summary>
        /// Beeps the specified amplitude.
        /// </summary>
        /// <param name="Amplitude">The amplitude.</param>
        /// <param name="Frequency">The frequency.</param>
        /// <param name="Duration">The duration.</param>
        public static void Beep(double Amplitude, double Frequency, double Duration)
        {
            double A = ((Amplitude * 32768.0) / 1000.0) - 1.0;
            double DeltaFT = (2 * Math.PI * Frequency) / 44100.0;
            double Samples = (44100 * Duration) / 1000;

            int Bytes = (int)Samples * 4;
            int[] Hdr = new int[] { 0x46464952, 0x24 + Bytes, 0x45564157, 0x20746d66, 0x10, 0x20001, 0xac44, 0x2b110, 0x100004, 0x61746164, Bytes };
            using (MemoryStream MS = new MemoryStream(0x24 + Bytes))
            {
                using (BinaryWriter BW = new BinaryWriter(MS))
                {
                    int length = Hdr.Length - 1;
                    for (int I = 0; I <= length; I++)
                    {
                        BW.Write(Hdr[I]);
                    }
                    int sample = (int)Samples - 1;
                    for (int T = 0; T <= sample; T++)
                    {
                        short Sample = (short)Math.Round((double)(A * Math.Sin(DeltaFT * T)));
                        BW.Write(Sample);
                        BW.Write(Sample);
                    }

                    BW.Flush();
                    MS.Seek(0L, SeekOrigin.Begin);

                    using (SoundPlayer SP = new SoundPlayer(MS))
                    {
                        SP.PlaySync();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Clase Wav
    /// </summary>
    public static class Wav
    {
        [DllImport("winmm.dll", SetLastError = true)]
        static extern bool PlaySound(string pszSound, UIntPtr hmod, uint fdwSound);

        /// <summary>
        /// Flags sound
        /// </summary>
        [Flags]
        public enum SoundFlags
        {
            /// <summary>play synchronously (default)</summary>
            SND_SYNC = 0x0000,
            /// <summary>play asynchronously</summary>
            SND_ASYNC = 0x0001,
            /// <summary>silence (!default) if sound not found</summary>
            SND_NODEFAULT = 0x0002,
            /// <summary>pszSound points to a memory file</summary>
            SND_MEMORY = 0x0004,
            /// <summary>loop the sound until next sndPlaySound</summary>
            SND_LOOP = 0x0008,
            /// <summary>don’t stop any currently playing sound</summary>
            SND_NOSTOP = 0x0010,
            /// <summary>Stop Playing Wave</summary>
            SND_PURGE = 0x40,
            /// <summary>don’t wait if the driver is busy</summary>
            SND_NOWAIT = 0x00002000,
            /// <summary>name is a registry alias</summary>
            SND_ALIAS = 0x00010000,
            /// <summary>alias is a predefined id</summary>
            SND_ALIAS_ID = 0x00110000,
            /// <summary>name is file name</summary>
            SND_FILENAME = 0x00020000,
            /// <summary>name is resource name or atom</summary>
            SND_RESOURCE = 0x00040004
    }

        /// <summary>
        /// Plays the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public static void Play(string fileName)
        {
            PlaySound(fileName, UIntPtr.Zero,
               (uint)(SoundFlags.SND_FILENAME | SoundFlags.SND_ASYNC));
        }
    }
}
