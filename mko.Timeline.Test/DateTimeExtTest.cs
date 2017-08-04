using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using mko.Timeline;

namespace mko.Timeline.Test
{
    [TestClass]
    public class DateTimeExtTest
    {
        [TestMethod]
        public void DateTimeExtTest_DateDiffNumbers()
        {
            var dateOne = new DateTime(2017, 12, 24);
            var dateTwo = new DateTime(2017, 7, 23);

            var diff = dateOne.DateDiff(dateTwo);

            Assert.AreEqual(0, diff.Years);
            Assert.AreEqual(5, diff.Month);
            Assert.AreEqual(1, diff.Days);

        }
    }
}
