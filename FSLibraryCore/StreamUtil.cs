using System;
using System.IO;

namespace FSLibrary
{
    /// <summary>
    /// Clase para el mánejo de streams
    /// </summary>
    public class StreamUtil
    {
        /// <summary>
        /// Copiel stream src en el destino dest.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        public static void CopyTo(Stream src, Stream dest)
        {
            var size = src.CanSeek ? Math.Min((int) (src.Length - src.Position), 8192) : 8192;
            var buffer = new byte[size];
            int n;
            do
            {
                n = src.Read(buffer, 0, buffer.Length);
                dest.Write(buffer, 0, n);
            } while (n != 0);
        }

        /// <summary>
        /// Copies el array de bytes al stream destino dest.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        public static void CopyTo(byte[] src, Stream dest)
        {
            dest.Write(src, 0, src.Length);
        }

        /// <summary>
        /// Copia el memoryStream src al stream destino dest.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        public static void CopyTo(MemoryStream src, Stream dest)
        {
            dest.Write(src.GetBuffer(), (int) src.Position, (int) (src.Length - src.Position));
        }

        /// <summary>
        /// Copia el memoryStream src al memoryStream destino dest.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        public static void CopyTo(MemoryStream src, MemoryStream dest)
        {
            CopyTo((Stream) src, (Stream) dest);
        }

        /// <summary>
        /// Copia el stream src al memoryStream destino dest.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        public static void CopyTo(Stream src, MemoryStream dest)
        {
            if (src.CanSeek)
            {
                var pos = (int) dest.Position;
                var length = (int) (src.Length - src.Position) + pos;
                dest.SetLength(length);

                while (pos < length)
                    pos += src.Read(dest.GetBuffer(), pos, length - pos);
            }
            else
            {
                CopyTo(src, (Stream) dest);
            }
        }
    }
}