using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.Timeline.Test
{
    [TestClass]
    public class TimelineRepo
    {
        [TestInitialize]
        public void Init()
        {
            mko.Timeline.Timeline.Reset();
        }


        [TestMethod]
        public void Timeline_InsertDelete()
        {

            var tl = new Timeline();

            {
                var t = tl.Create();
                Assert.AreEqual(0, tl.Count);

                t.BeginDate = new Date(2017, 12, 24);
                t.BeginTime = new Time(16, 0, 0);
                t.EndDate = new Date(2017, 12, 25);
                t.EndTime = new Time(0, 0, 0);

                t.Category = AppointmentCategory.@private;
                t.Details = "Geschenke!";
                t.Owner = "ich";
            }

            {
                var t = tl.Create();

                t.BeginDate = new Date(2017, 12, 31);
                t.BeginTime = new Time(20, 0, 0);
                t.EndDate = new Date(2018, 1, 1);
                t.EndTime = new Time(6, 0, 0);

                t.Category = AppointmentCategory.@private;
                t.Details = "Party !!";
                t.Owner = "du";
            }

            {
                var t = tl.Create();

                t.BeginDate = new Date(2017, 5, 15);
                t.BeginTime = new Time(9, 0, 0);
                t.EndDate = new Date(2017, 5, 15);
                t.EndTime = new Time(17, 0, 0);

                t.Category = AppointmentCategory.business;
                t.Details = "mvc kurs";
                t.Owner = "ich";
            }

            tl.SaveChanges();
            Assert.AreEqual(3, tl.Count);

            Assert.IsTrue(tl.Exists("ich", new Date(2017, 12, 24), new Time(16, 0, 0), new Date(2017, 12, 25), new Time()));

            var weihnachten = tl.Get("ich", new Date(2017, 12, 24), new Time(16, 0, 0), new Date(2017, 12, 25), new Time());
            Assert.IsNotNull(weihnachten);
            Assert.AreEqual("Geschenke!", weihnachten.Details);


            tl.Delete(weihnachten);
            tl.SaveChanges();

            Assert.AreEqual(2, tl.Count);
            Assert.IsFalse(tl.Exists("ich", new Date(2017, 12, 24), new Time(16, 0, 0), new Date(2017, 12, 25), new Time())); 

        }

        [TestMethod]
        public void Timeline_Filter_and_sort()
        {
            var tl = new Timeline();

            {
                var t = tl.Create();
                Assert.AreEqual(0, tl.Count);

                t.BeginDate = new Date(2017, 12, 24);
                t.BeginTime = new Time(16, 0, 0);
                t.EndDate = new Date(2017, 12, 25);
                t.EndTime = new Time(0, 0, 0);

                t.Category = AppointmentCategory.@private;
                t.Details = "Geschenke!";
                t.Owner = "ich";
            }

            {
                var t = tl.Create();

                t.BeginDate = new Date(2017, 12, 31);
                t.BeginTime = new Time(20, 0, 0);
                t.EndDate = new Date(2018, 1, 1);
                t.EndTime = new Time(6, 0, 0);

                t.Category = AppointmentCategory.@private;
                t.Details = "Party !!";
                t.Owner = "du";
            }

            {
                var t = tl.Create();

                t.BeginDate = new Date(2017, 5, 15);
                t.BeginTime = new Time(9, 0, 0);
                t.EndDate = new Date(2017, 5, 15);
                t.EndTime = new Time(17, 0, 0);

                t.Category = AppointmentCategory.business;
                t.Details = "mvc kurs";
                t.Owner = "ich";
            }

            {
                var t = tl.Create();

                t.BeginDate = new Date(2017, 4, 15);
                t.BeginTime = new Time(9, 0, 0);
                t.EndDate = new Date(2017, 4, 17);
                t.EndTime = new Time(17, 0, 0);

                t.Category = AppointmentCategory.business;
                t.Details = "c# kurs";
                t.Owner = "ich";
            }

            {
                var t = tl.Create();

                t.BeginDate = new Date(2017, 5, 22);
                t.BeginTime = new Time(9, 0, 0);
                t.EndDate = new Date(2017, 5, 22);
                t.EndTime = new Time(10, 0, 0);

                t.Category = AppointmentCategory.business;
                t.Details = "Präsentation Programm";
                t.Owner = "ich";
            }


            tl.SaveChanges();

            {
                var fssBld = tl.CreateFSSBld();

                fssBld.Owner = "du";

                var set = fssBld.GetSet();
                Assert.IsTrue(set.Any());
                Assert.AreEqual(1, set.Count());
            }

            {
                var fssBld = tl.CreateFSSBld();

                fssBld.Owner = "ich";

                var set = fssBld.GetSet();
                Assert.IsTrue(set.Any());
                Assert.AreEqual(4, set.Count());
            }

            {
                var fssBld = tl.CreateFSSBld();

                fssBld.Between = mko.BI.Bo.Interval.Create(new DateTime(2017, 5, 1), new DateTime(2017, 5, 31));
                 fssBld.OrderByBegin(true);

                var set = fssBld.GetSet();
                Assert.IsTrue(set.Any());
                Assert.AreEqual(2, set.Count());

                var appointments = set.Get();

            }

            //{
            //    // JSon- Test: Serialisierung
            //    var jsonDict = tl.ToJson();

            //    var tl2 = new Timeline(jsonDict);

            //    Assert.AreEqual(5, tl2.Count);

            //    var fssbld = tl2.CreateFSSBld();

            //    var set = fssbld.GetSet();

            //    var appointments = set.Get();

            //}




        }

    }
}
