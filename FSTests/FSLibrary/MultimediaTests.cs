using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FSLibrary.Tests
{
    [TestClass]
    public class MultimediaTests
    {
        [TestMethod()]
        public void Beeper()
        {
            for (int b = 0; b < 50; b++)
            {
                for (int c = 0; c < 40; c++)
                {
                    for (int d = 0; d < 20; d++)
                    {
                        Multimedia.Beep(d, 0.01);
                        Multimedia.Beep(c, 0.01);
                    }
                }
            }
        }
    }
}
