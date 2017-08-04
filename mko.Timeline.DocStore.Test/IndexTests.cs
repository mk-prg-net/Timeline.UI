using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

using mko.Timeline;

namespace mko.Timeline.DocStore.Test
{
    [TestClass]
    public class IndexTests
    {

        const string DocStorePath = "..\\..\\DocStore";
        Docs docs;

        const string IndexStorePath = "..\\..\\IndexStore";
        Index index;


        [TestInitialize]
        public void Init()
        {
            // Alle Dateien im Docstore löschen
            foreach (var filename in System.IO.Directory.GetFiles(DocStorePath))
            {
                System.IO.File.Delete(filename);
            }

            // Alle Dateien im Index löschen
            foreach (var filename in System.IO.Directory.GetFiles(IndexStorePath))
            {
                System.IO.File.Delete(filename);
            }

            // Neuen Docstore anlegen
            docs = new Docs(DocStorePath);

            index = new Index(IndexStorePath);
        }


        [TestMethod]
        public async Task DocStore_Index()
        {

            var t = new Appointment(
                        BeginDate: new Date(2017, 5, 15),
                        BeginTime: new Time(9, 0, 0),
                        EndDate: new Date(2017, 5, 15),
                        EndTime: new Time(17, 0, 0),
                        category: AppointmentCategory.@private,
                        details: "MVC Kurs",
                        Location: new mko.BI.Bo.Addresses.Location() { City = "Düsseldorf", Country = "de" },
                        Owner: "ich");

            string id = docs.CreateDocHashId(t);
            await docs.PutAsync(id, t);
            await index.AddToIndexAsync(id, t);

            // Allgemeine Termine anlegen
            for (int jahr = 2015; jahr < 2030; jahr++)
            {
                foreach(var tupel in Appointment.RepeatingAppointments)
                {
                    var app = new Appointment(
                        BeginDate: new Date(jahr, tupel.Item2, tupel.Item1),
                        BeginTime: new Time(0),
                        EndDate: new Date(jahr, tupel.Item2, tupel.Item1),
                        EndTime: new Time(23, 59, 59),
                        category: tupel.Item3,
                        details: tupel.Item4,
                        Owner: "alle",
                        Location: new mko.BI.Bo.Addresses.Location());

                    id = docs.CreateDocHashId(app);

                    await docs.PutAsync(id, app);
                    await index.AddToIndexAsync(id, app);
                }
            }

            await index.SaveChangesAsync();

            // Abfragen
            var qb =  index.CreateQueryBuilder();
            qb.Between = new BI.Bo.Interval<DateTime>(new DateTime(2017, 1, 1), new DateTime(2018, 1, 1));

            var sob = qb.GetSortOrderBuilder();
            sob.OrderByCategory(false);
            sob.OrderByBegin(true);            

            var fss = sob.GetFilteredSortedSet();

            Assert.IsFalse(0 == fss.Count());

            var set = fss.Get();


            AppointmentCategory old_cat = AppointmentCategory.@private;
            IDate old_date = new Date(2100);

            foreach(var key in set)
            {
                var app = await docs.GetAsync(key);

                Debug.WriteLine(app.Category + ", " + app.BeginDate.ToStr() + "T" + app.BeginTime.ToStr() + ": " + app.Details);

                if(old_cat == app.Category)
                {
                    Assert.IsTrue(old_date.ToDateTime().Ticks >= app.BeginDate.ToDateTime().Ticks);
                    old_date = app.BeginDate;
                } else
                {
                    old_date = new Date(2100);
                    old_cat = app.Category;
                }                
            }
        }
    }
}
