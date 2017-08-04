//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 16.6.2017
//
//  Projekt.......: mko.Timeline.FS
//  Name..........: TimelineFSSBld.cs
//  Aufgabe/Fkt...: Implementierung eines Filtered Sorted Set Builders 
//                  für den Timeline- Kalender, welcher die Termine in einem
//                  Dateiverzeichnis speichert
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.BI.Bo;
using mko.BI.Repositories.Interfaces;

namespace mko.Timeline.FS
{
    public class TimelineFSSBld : mko.Timeline.ITimelineFSSBld
    {
        IQueryable<IAppointment> query;
        List<mko.BI.Repositories.DefSortOrder<IAppointment>> SortOrders = new List<BI.Repositories.DefSortOrder<IAppointment>>();

        public TimelineFSSBld(string TimelinePath)
        {
            string[] appointmentFileNames = System.IO.Directory.GetFiles(TimelinePath);
            List<IAppointment> appointments = new List<IAppointment>(appointmentFileNames.Count());

            foreach(string fn in appointmentFileNames)
            {
                var reader = new System.IO.StreamReader(fn);
                var appointmentBld = Newtonsoft.Json.JsonConvert.DeserializeObject<AppointmentBuilder>(reader.ReadToEnd());
                mko.TraceHlp.ThrowArgExIfNot(appointmentBld.IsValid, "üngültiger Termin in " + fn);

                appointments.Add(appointmentBld.Create());
            }

            query = appointments.AsQueryable();
        }


        public BI.Bo.Interval<DateTime> Between
        {
            set
            {
                query = query.Where(r => value.Begin.Ticks <= mko.Timeline.Timeline.ToDateTime(r.BeginDate, r.BeginTime).Ticks
                                        && mko.Timeline.Timeline.ToDateTime(r.EndDate, r.EndTime).Ticks <= value.End.Ticks);
            }
        }

        public string Owner
        {
            set
            {
                query = query.Where(r => r.Owner == value);
            }
        }


        public int Skip
        {
            set
            {
                _skip = value;
            }
        }
        int _skip = -1;


        public int Take
        {
            set
            {
                _take = value;
            }
        }

        public AppointmentCategory Category
        {
            set
            {
                query = query.Where(r => r.Category == value);
            }
        }

        int _take = -1;

        public void OrderByBegin(bool desc)
        {
            SortOrders.Add(new BI.Repositories.DefSortOrderCol<IAppointment, long>(r => mko.Timeline.Timeline.ToDateTime(r.BeginDate, r.BeginTime).Ticks, desc));
        }

        public void OrderByDuration(bool desc)
        {
            SortOrders.Add(new BI.Repositories.DefSortOrderCol<IAppointment, long>(r => mko.Timeline.Timeline.ToDateTime(r.EndDate, r.EndTime).Ticks - mko.Timeline.Timeline.ToDateTime(r.BeginDate, r.BeginTime).Ticks, desc));
        }

        public void OrderByEnd(bool desc)
        {
            SortOrders.Add(new BI.Repositories.DefSortOrderCol<IAppointment, long>(r => mko.Timeline.Timeline.ToDateTime(r.EndDate, r.EndTime).Ticks, desc));
        }

        public void OrderByOwner(bool desc)
        {
            SortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<IAppointment, string>(r => r.Owner, desc));
        }

        public IFilteredSortedSet<IAppointment> GetSet()
        {
            if (!SortOrders.Any())
            {
                SortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<IAppointment, long>(r => mko.Timeline.Timeline.ToDateTime(r.BeginDate, r.BeginTime).Ticks, true));
            }
            return new mko.BI.Repositories.FilteredSortedSet<IAppointment>(query, SortOrders, _skip, _take);
        }
    }
}
