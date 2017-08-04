using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.Timeline.DocStore.Test
{
    [TestClass]
    public class PutGetDelete
    {
        const string DocStorePath = "..\\..\\DocStore";
        Docs docs;

        [TestInitialize]
        public void Init()
        {
            // Alle Dateien im Repository löschen
            foreach (var filename in System.IO.Directory.GetFiles(DocStorePath))
            {
                System.IO.File.Delete(filename);
            }

            // Neuen Docstore anlegen
            docs = new Docs(DocStorePath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            
        }

        [TestMethod]
        public async System.Threading.Tasks.Task DocStore_Put_Get_Delete()
        {
            var q = new System.Collections.Generic.Queue<IAppointment>();

            {
                var t = new Appointment(
                            BeginDate: new Date(2017, 12, 24),
                            BeginTime: new Time(16, 0, 0),
                            EndDate: new Date(2017, 12, 25),
                            EndTime: new Time(0, 0, 0),
                            category: AppointmentCategory.@private,
                            details: "Geschenke!",
                            Location: new mko.BI.Bo.Addresses.Location() { City= "Stuttgart", Country="de"},
                            Owner: "ich");

                var id = docs.CreateDocHashId(t);
                await docs.PutAsync(id, t);
                q.Enqueue(t);
            }

            {
                var t = new Appointment(
                            BeginDate: new Date(2017, 12, 31),
                            BeginTime: new Time(20, 0, 0),
                            EndDate: new Date(2018, 1, 1),
                            EndTime: new Time(6, 0, 0),
                            category: AppointmentCategory.@private,
                            details: "Party !!",
                            Location: new mko.BI.Bo.Addresses.Location() { City = "Stuttgart", Country = "de" },
                            Owner: "du");

                var id = docs.CreateDocHashId(t);
                await docs.PutAsync(id, t);
                q.Enqueue(t);
            }

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

                var id = docs.CreateDocHashId(t);
                await docs.PutAsync(id, t);
                q.Enqueue(t);
            }

            Assert.AreEqual(3, q.Count);

            // Lesen und löschen
            while(q.Count > 0)
            {
                var ori = q.Dequeue();
                var id = docs.CreateDocHashId(ori);

                var t = await docs.GetAsync(id);

                Assert.IsTrue(ori.Equ(t));

                await docs.DeleteAsync(id);
            }

            Assert.AreEqual(0, System.IO.Directory.GetFiles(DocStorePath).Length);
        }
    }
}
