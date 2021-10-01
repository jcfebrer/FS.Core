using System;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;

namespace FSLibrary
{
    /// <summary>
    /// Librería multimedia
    /// </summary>
    public static class Multimedia
    {
        ///// <summary>
        ///// Clase Pitch
        ///// </summary>
        //public static class Pitch
        //{
        //    static string C = "0|2093";
        //    static string Csharp = "1|2217";
        //    static string Dflat = "1|2217";
        //    static string D = "2|2349";
        //    static string Dsharp = "3|2489";
        //    static string Eflat = "3|2489";
        //    static string E = "4|2637";
        //    static string F = "5|2794";
        //    static string Fsharp = "6|2960";
        //    static string Gflat = "6|2960";
        //    static string G = "7|3136";
        //    static string Gsharp = "8|3322";
        //    static string Aflat = "8|3322";
        //    static string A = "9|3520";
        //    static string Asharp = "10|3729";
        //    static string Bflat = "10|3729";
        //    static string B = "11|3951";
        //    static string Rest = "0|0";
        //}

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
        /// Play beep sound
        /// </summary>
        public static void PlayBeep()
        {
            SystemSounds.Beep.Play();
        }
        /// <summary>
        /// Play asterisk sound
        /// </summary>
        public static void PlayAsterik()
        {
            SystemSounds.Asterisk.Play();
        }
        /// <summary>
        /// Play exclamation sound
        /// </summary>
        public static void PlayExclamation()
        {
            SystemSounds.Exclamation.Play();
        }
        /// <summary>
        /// Play question sound
        /// </summary>
        public static void PlayQuestion()
        {
            SystemSounds.Question.Play();
        }
        /// <summary>
        /// Play hand sound
        /// </summary>
        public static void PlayHand()
        {
            SystemSounds.Hand.Play();
        }

        /// <summary>
        /// Beep de ZX Spectrum
        /// </summary>
        /// <param name="Duration"></param>
        /// <param name="pitch"></param>
        public static void BeepZx(double Duration, int pitch)
        {
            int p = 0;

            switch (pitch % 12)
            {
                case 0: p = 2093; break; //C
                case 1: p = 2217; break; //C#
                case 2: p = 2343; break; //D
                case 3: p = 2489; break; //D#
                case 4: p = 2637; break; //E
                case 5: p = 2794; break; //F
                case 6: p = 2960; break; //G#
                case 7: p = 3136; break; //G
                case 8: p = 3322; break; //F#
                case 9: p = 3520; break; //A
                case 10: p = 3729; break; //A#
                case 11: p = 3951; break; //B
            }

            Console.Beep(p, (int)(Duration * 1000));
        }


        /// <summary>
        /// Beeps the specified frequency.
        /// </summary>
        /// <param name="Frequency">The frequency.</param>
        /// <param name="Duration">The duration.</param>
        public static void Beep(int Frequency, int Duration)
        {
            Beep(1, Frequency, Duration);
        }

        /// <summary>
        /// Beeps the specified amplitude.
        /// </summary>
        /// <param name="Amplitude">The amplitude.</param>
        /// <param name="Frequency">The frequency.</param>
        /// <param name="Duration">The duration.</param>
        public static void Beep(int Amplitude, int Frequency, int Duration)
        {
            double A = ((Amplitude * 32768.0) / 1000.0) - 1.0;
            double DeltaFT = (2 * Math.PI * Frequency) / 44100.0;
            int Samples = (44100 * Duration) / 1000;

            Console.WriteLine(Samples);
            int Bytes = Samples * 4;
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
                    int sample = Samples - 1;
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
