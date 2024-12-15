using FSLibraryCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSTestCore.FSLibrary
{
    [TestClass]
    public class DateTimeUtilTests
    {
        [TestMethod]
        public void TestDateTime()
        {
            DateTime a = new DateTime(2024, 12, 1, 0, 0, 0);
            DateTime b = new DateTime(2024, 12, 15, 0, 0, 0);
            double minutes = DateTimeUtil.MinutesBetween2Dates(a , b, true, 7, 19);

            Assert.AreEqual(minutes, 7200);

            double minutesTotal = DateTimeUtil.MinutesBetween2Dates(a, b, true, 0, 24);

            Assert.AreEqual(minutesTotal, 14400);

            double minutes2 = DateTimeUtil.DateDiffBusinessMinutes(a, b);

            Assert.AreEqual(minutes2, 14400);

            double days = DateTimeUtil.DateDiffBusinessDays(a, b);

            Assert.AreEqual(days, 10);
        }
    }
}
