using System;
using FSSepaLibraryCore.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FSSepaLibraryCore.Test.Utils
{
    [TestClass]
    public class StringUtilsTest
    {
        private const string FirstPart = "012345678";

        [TestMethod]
        public void ShouldTruncateATooLongString()
        {
            const string str = FirstPart + "9" + "another part";
            Assert.AreEqual(FirstPart + "9", StringUtils.GetLimitedString(str, 10));
        }

        [TestMethod]
        public void ShouldNotTruncateSmallString()
        {
            Assert.AreEqual(FirstPart, StringUtils.GetLimitedString(FirstPart, 10));
            Assert.IsNull(StringUtils.GetLimitedString(null, 10));
        }

        [TestMethod]
        public void ShouldNotTruncateNullString()
        {
            Assert.IsNull(StringUtils.GetLimitedString(null, 10));
        }

        [TestMethod]
        public void ShouldFormatADate()
        {
            var date = new DateTime(2013, 11, 27);
            Assert.AreEqual("2013-11-27T00:00:00", StringUtils.FormatDateTime(date));
        }

        [TestMethod]
        public void ShouldCleanUpString()
        {
            Assert.AreEqual(FirstPart, StringUtils.RemoveInvalidChar(FirstPart));

            var allowedChars = "@/-?:(). ,'\"+";
            Assert.AreEqual(allowedChars, StringUtils.RemoveInvalidChar(allowedChars));

            Assert.AreEqual("EAEU", StringUtils.RemoveInvalidChar("éàèù"));
        }
    }
}