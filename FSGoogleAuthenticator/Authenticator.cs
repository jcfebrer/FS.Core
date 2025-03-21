using System;
using System.Collections.Generic;
using System.Text;

namespace FSGoogleAuthenticator
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;

    public class Authenticator
    {
        const int IntervalLength = 30;
        const int PinLength = 6;
        static readonly int PinModulo = (int)Math.Pow(10, PinLength);
        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        ///   Number of intervals that have elapsed.
        /// </summary>
        static long CurrentInterval
        {
            get
            {
                var ElapsedSeconds = (long)Math.Floor((DateTime.UtcNow - UnixEpoch).TotalSeconds);

                return ElapsedSeconds / IntervalLength;
            }
        }

        /// <summary>
        ///   Generates a QR code bitmap for provisioning.
        /// </summary>
        public byte[] GenerateProvisioningImage(string email, string company, byte[] key)
        {
            return GenerateProvisioningImage(email, company, key, 300, 300);
        }

        /// <summary>
        ///   Generates a QR code bitmap for provisioning.
        /// </summary>
        public byte[] GenerateProvisioningImage(string email, string company, byte[] key, int width, int height)
        {
            string chartUrl = GenerateQRUrl(email, company, key, width, height);
            using (var Client = new WebClient())
            {
                return Client.DownloadData(chartUrl);
            }
        }

        /// <summary>
        ///   Generates string to QR code url.
        /// </summary>
        public string GenerateQRUrl(string email, string company, byte[] key)
        {
            return GenerateQRUrl(email, company, key, 300, 300);
        }

        /// <summary>
        ///   Generates string to QR code url.
        /// </summary>
        public string GenerateQRUrl(string email, string company, byte[] key, int width, int height)
        {
            var keyString = Encoder.Base32Encode(key);
            var provisionUrl = Encoder.UrlEncode(string.Format("otpauth://totp/{0}?secret={1}&issuer={2}", email, keyString, company));

            //Utilizamos la api de google para generar un QR del tamaño {0}x{1} y con el contenido {2}.
            return string.Format("https://chart.apis.google.com/chart?cht=qr&chs={0}x{1}&chl={2}", width, height, provisionUrl);
        }

        /// <summary>
        /// Genera la clave en formato Base32 para dar de alta manualmente en la aplicación Google Authenticator.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GenerateKey(byte[] key)
        {
            return Encoder.Base32Encode(key);
        }

        /// <summary>
        ///   Generates a pin for the given key.
        /// </summary>
        public string GeneratePin(byte[] key)
        {
            return GeneratePin(key, CurrentInterval);
        }

        /// <summary>
        ///   Generates a pin by hashing a key and counter.
        /// </summary>
        static string GeneratePin(byte[] key, long counter)
        {
            const int SizeOfInt32 = 4;

            var CounterBytes = BitConverter.GetBytes(counter);

            if (BitConverter.IsLittleEndian)
            {
                //spec requires bytes in big-endian order
                Array.Reverse(CounterBytes);
            }

            var Hash = new HMACSHA1(key).ComputeHash(CounterBytes);
            var Offset = Hash[Hash.Length - 1] & 0xF;

            var SelectedBytes = new byte[SizeOfInt32];
            Buffer.BlockCopy(Hash, Offset, SelectedBytes, 0, SizeOfInt32);

            if (BitConverter.IsLittleEndian)
            {
                //spec interprets bytes in big-endian order
                Array.Reverse(SelectedBytes);
            }

            var SelectedInteger = BitConverter.ToInt32(SelectedBytes, 0);

            //remove the most significant bit for interoperability per spec
            var TruncatedHash = SelectedInteger & 0x7FFFFFFF;

            //generate number of digits for given pin length
            var Pin = TruncatedHash % PinModulo;

            return Pin.ToString(CultureInfo.InvariantCulture).PadLeft(PinLength, '0');
        }

#if NET40_OR_GREATER || NETCOREAPP
        /// <summary>
        /// Crea una clave aleatoria (habría que guardar en cada usuario para realizar la validación)
        /// </summary>
        /// <returns></returns>
        public static byte[] CreateKey()
        {
            return CreateKey(12);
        }

        /// <summary>
        /// Crea una clave aleatoria (habría que guardar en cada usuario para realizar la validación)
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[] CreateKey(int size)
        {
            using (var generator = RandomNumberGenerator.Create())
            {
                var salt = new byte[size];
                generator.GetBytes(salt);
                return salt;
            }
        }
#endif

        #region Nested type: Encoder

        static class Encoder
        {
            /// <summary>
            ///   Url Encoding (with upper-case hexadecimal per OATH specification)
            /// </summary>
            public static string UrlEncode(string value)
            {
                const string UrlEncodeAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

                var Builder = new StringBuilder();

                for (var i = 0; i < value.Length; i++)
                {
                    var Symbol = value[i];

                    if (UrlEncodeAlphabet.IndexOf(Symbol) != -1)
                    {
                        Builder.Append(Symbol);
                    }
                    else
                    {
                        Builder.Append('%');
                        Builder.Append(((int)Symbol).ToString("X2"));
                    }
                }

                return Builder.ToString();
            }

            /// <summary>
            ///   Base-32 Encoding
            /// </summary>
            public static string Base32Encode(byte[] data)
            {
                const int InByteSize = 8;
                const int OutByteSize = 5;
                const string Base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

                int i = 0, index = 0;
                var Builder = new StringBuilder((data.Length + 7) * InByteSize / OutByteSize);

                while (i < data.Length)
                {
                    int CurrentByte = data[i];
                    int Digit;

                    //Is the current digit going to span a byte boundary?
                    if (index > (InByteSize - OutByteSize))
                    {
                        int NextByte;

                        if ((i + 1) < data.Length)
                        {
                            NextByte = data[i + 1];
                        }
                        else
                        {
                            NextByte = 0;
                        }

                        Digit = CurrentByte & (0xFF >> index);
                        index = (index + OutByteSize) % InByteSize;
                        Digit <<= index;
                        Digit |= NextByte >> (InByteSize - index);
                        i++;
                    }
                    else
                    {
                        Digit = (CurrentByte >> (InByteSize - (index + OutByteSize))) & 0x1F;
                        index = (index + OutByteSize) % InByteSize;

                        if (index == 0)
                        {
                            i++;
                        }
                    }

                    Builder.Append(Base32Alphabet[Digit]);
                }

                return Builder.ToString();
            }
        }

        #endregion
    }
}
