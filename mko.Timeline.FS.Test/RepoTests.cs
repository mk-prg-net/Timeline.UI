//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 18.6.2017
//
//  Projekt.......: mko.Timeline.FS.Test
//  Name..........: RepoTests.cs    
//  Aufgabe/Fkt...: Test des Dateisystembasierten Repositories
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace mko.Timeline.FS.Test
{
    [TestClass]
    public class RepoTests
    {
        const string PathTestRepo = "..\\..\\TestRepo";

        mko.Timeline.FS.Timeline tl;

        [TestInitialize]
        public void  Init()
        {

            // Alle Dateien im Repository löschen
            foreach(var filename in System.IO.Directory.GetFiles(PathTestRepo))
            {
                System.IO.File.Delete(filename);
            }

            // Neue Repository anlegen
            tl = new Timeline(PathTestRepo);
        }


        [TestMethod]
        public void RepoTests_Create_Delete()
        {
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
        public void RepoTests_Filter()
        {
            for(int jahr = 2017; jahr < 2030; jahr++)
            {
                var wn = tl.Create();
                wn.BeginDate = new Date(jahr, 12, 24);
                wn.BeginTime = new Time();
                wn.EndDate = new Date(jahr, 12, 24);
                wn.EndTime = new Time(23, 59, 59);
                wn.Category = AppointmentCategory.@private;
                wn.Details = "Weihnachten, Geschenke";
                wn.Owner = "alle";

                var syl = tl.Create();
                syl.BeginDate = new Date(jahr, 12, 31);
                syl.BeginTime = new Time(23, 59, 59); 
                syl.EndDate = new Date(jahr + 1, 1, 1);
                syl.EndTime = new Time(0, 30, 0);
                syl.Category = AppointmentCategory.@private;
                syl.Details = "Sylvester, Feuerwerk";
                syl.Owner = "alle";


                var st = tl.Create();
                st.BeginDate = new Date(jahr, 5, 1);
                st.BeginTime = new Time();
                st.EndDate = new Date(jahr, 5, 1);
                st.EndTime = new Time(23, 59, 59);
                st.Category = AppointmentCategory.business;
                st.Details = "Steuererklärung abgeben";
                st.Owner = "ich";

                var ws = tl.Create();
                ws.BeginDate = new Date(jahr, 4, 1);
                ws.BeginTime = new Time(15,0,0);
                ws.EndDate = new Date(jahr, 4, 1);
                ws.EndTime = new Time(17, 0, 0);
                ws.Category = AppointmentCategory.@private;
                ws.Details = "Wechsel auf Sommerreifen";
                ws.Owner = "ich";

                var sw = tl.Create();
                sw.BeginDate = new Date(jahr, 11, 1);
                sw.BeginTime = new Time(15, 0, 0);
                sw.EndDate = new Date(jahr, 11, 1);
                sw.EndTime = new Time(17, 0, 0);
                sw.Category = AppointmentCategory.@private;
                sw.Details = "Wechsel auf Winterreifen";
                sw.Owner = "ich";

            }
            tl.SaveChanges();

            var fssbld = tl.CreateFSSBld();

            fssbld.Owner = "ich";
            fssbld.Category = AppointmentCategory.business;
            fssbld.OrderByBegin(true);

            var TermineAbgabeSteuererklaerung = fssbld.GetSet();

            Assert.AreEqual(13, TermineAbgabeSteuererklaerung.Count());
            foreach(var t in TermineAbgabeSteuererklaerung.Get())
            {
                Debug.WriteLine(t.BeginDate.ToDateTime().ToLongDateString() + ": " + t.Details);                
            }
        }
    }
}
