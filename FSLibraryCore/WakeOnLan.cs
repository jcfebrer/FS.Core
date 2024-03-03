using FSException;
using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace FSLibraryCore
{
    /// <summary>
    /// Clase para la utilización de WOL.
    /// </summary>
    public class WakeOnLan
    {
        /// <summary>
        ///     Se indica la dirección MAC con o sin guiones
        /// </summary>
        /// <param name="macAddress"></param>
        public static void WakeUp(string macAddress)
        {
            var client = new WOLClass();

            macAddress = macAddress.Replace("-", "");

            if (macAddress.Length != 16)
                throw new ExceptionUtil("Dirección MAC incorrecta. Debe ser de 16 carácteres hexadecimales.");

            client.Connect(new IPAddress(0xffffffff), 0x2fff);
            client.SetClientToBroadcastMode();

            var magicNumber = GenerateMagicNumber(macAddress);

            var returnedValue = client.Send(magicNumber, 1024);
        }

        private static byte[] GenerateMagicNumber(string macAddress)
        {
            var counter = 0;

            var bytes = new byte[1024];

            for (var e = 0; e < 6; e++) bytes[counter++] = 0xFF;

            for (var e = 0; e < 16; e++)
            {
                var i = 0;

                for (var w = 0; w < 6; w++)
                {
                    bytes[counter++] = byte.Parse(macAddress.Substring(i, 2), NumberStyles.HexNumber);
                    i += 2;
                }
            }

            return bytes;
        }
    }

    internal class WOLClass : UdpClient
    {
        public void SetClientToBroadcastMode()
        {
            if (Active) Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 0);
        }
    }
}