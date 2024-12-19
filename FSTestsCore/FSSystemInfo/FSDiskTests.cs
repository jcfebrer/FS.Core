using FSSystemInfoCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FSTestsCore.FSSystemInfo
{
    [TestClass()]
    public class FSDiskTests
    {
        [TestMethod()]
        public void DiskInfo()
        {
            SystemInfo sysInfo = new SystemInfo();
            List<string> phiscalDisk = sysInfo.GetPhysicalDisks();

            FSDiskCore.DiskInfo diskInfo = new FSDiskCore.DiskInfo();

            string sSerial = "";
            string sModel = "";
            string sFirmware = "";

            bool result = diskInfo.GetHDData(phiscalDisk[0], ref sSerial, ref sModel, ref sFirmware);

            Assert.IsFalse(result);

            if (result)
            {
                Assert.AreEqual(sSerial, "");
                Assert.AreEqual(sModel, "");
                Assert.AreEqual(sFirmware, "");
            }
        }

        [TestMethod()]
        public void DiskHDInfo()
        {
            SystemInfo si = new SystemInfo();

            string result = si.GetHdInfo();

            Assert.AreNotEqual(result, "");
        }
    }
}
