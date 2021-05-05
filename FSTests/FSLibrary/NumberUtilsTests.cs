using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FSLibrary.Tests
{
    [TestClass]
    public class NumberUtils
    {
        [TestMethod()]
        public void Packet()
        {
            byte[] dataToSend = new byte[] { 0x8B, 0xB9, 0x00, 0x03, 0x05, 0x01, 0x09 };
            foreach (byte[] bytes in FSLibrary.NumberUtils.BytePackets(dataToSend, dataToSend.Length))
            {
                foreach (byte b in bytes)
                {
                    if(bytes.Length % 2 == 0)
                    {
                        Console.WriteLine(b);
                    }
                    else
                    {
                        Console.Write(b);
                    }
                }
            }
        }
    }
}
