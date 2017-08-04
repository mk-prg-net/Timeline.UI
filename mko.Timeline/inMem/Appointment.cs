//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.5.2017
//
//  Projekt.......: mko.Timeline
//  Name..........: Appointment.cs
//  Aufgabe/Fkt...: Implementierung eines Kalendereintrages
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.BI.Bo.Addresses;

using System.ComponentModel.DataAnnotations;

namespace mko.Timeline
{
    public class Appointment : IAppointment, IJson
    {
        public static Tuple<int, int, mko.Timeline.AppointmentCategory, string>[] RepeatingAppointments =
        {
            Tuple.Create(1,1, AppointmentCategory.@private, "Neujahr"),
            Tuple.Create(14, 2, AppointmentCategory.holiday, "Valentinstag"),
            Tuple.Create(1,3, AppointmentCategory.astronomical, "Frühlingsbeginn"),
            Tuple.Create(26,3, AppointmentCategory.astronomical, "Sommerzeitbeginn"),
            Tuple.Create(1,4, AppointmentCategory.@private, "Scherze ohne Ende"),
            Tuple.Create(21,6, AppointmentCategory.astronomical, "Sommeranfang, Sonnenwende"),
            Tuple.Create(1,5, AppointmentCategory.business, "Tag der Arbeit ..."),
            Tuple.Create(14,7, AppointmentCategory.national_day, "Sturm auf die Bastille 1789, franz. Nationalfeiertag"),
            Tuple.Create(1,8, AppointmentCategory.national_day, "1291: Nationalfeiertag Schweiz"),
            Tuple.Create(1,9, AppointmentCategory.memorial_day, "Beginn 2. WK"),
            Tuple.Create(1,9, AppointmentCategory.astronomical, "Herbstanfang Kalender"),
            Tuple.Create(22,9, AppointmentCategory.astronomical, "Herbstanfang"),
            Tuple.Create(29,10, AppointmentCategory.astronomical, "Sommerzeitende"),
            Tuple.Create(3,10, AppointmentCategory.national_day, "Tag der deutschen Einheit"),
            Tuple.Create(6,12, AppointmentCategory.church_festival, "Nikolaus"),
            Tuple.Create(21, 12, AppointmentCategory.astronomical, "Winteranfang"),
            Tuple.Create(24,12, AppointmentCategory.church_festival, "Heiligabend"),
            Tuple.Create(31,12, AppointmentCategory.holiday, "Silvester"),
        };

        public Appointment(
            IDate BeginDate,
            ITime BeginTime,
            IDate EndDate,
            ITime EndTime,
            string Owner,
            ILocation Location,
            AppointmentCategory category,
            string details)
        {
            this.BeginDate = BeginDate;
            this.BeginTime = BeginTime;
            this.EndDate = EndDate;
            this.EndTime = EndTime;
            this.Owner = Owner;
            this.Location = Location;
            this.Category = category;
            this.Details = details;
        }

        /// <summary>
        /// Mittels UIHint kann z.B. in ASP.NET MVC dieser Eigenschaft eine partielle View zum Rendern zugeorndet werden
        /// </summary>
        [UIHint("Date")]
        //[Newtonsoft.Json.JsonConverter(typeof(Date))]
        public IDate BeginDate
        {
            get;
        }

        [UIHint("Time")]
        //[Newtonsoft.Json.JsonConverter(typeof(Time))]
        public ITime BeginTime
        {
            get;
        }

        public AppointmentCategory Category
        {
            get;
        }

        public string Details
        {
            get;
        }

        [UIHint("Date")]
        //[Newtonsoft.Json.JsonConverter(typeof(Date))]
        public IDate EndDate
        {
            get;
        }

        [UIHint("Time")]
        //[Newtonsoft.Json.JsonConverter(typeof(Time))]
        public ITime EndTime
        {
            get;
        }

        [UIHint("Location")]
        //[Newtonsoft.Json.JsonConverter(typeof(mko.BI.Bo.Addresses.Location))]
        public ILocation Location
        {
            get;
        }

        public string Owner
        {
            get;
        }

        public string ToJson()
        {
#if (DEBUG)
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
#else
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
#endif
        }

        public override string ToString()
        {
            return string.Format(string.Format("Begin: {0}, End: {1}, Owner: {2}, {3}",
                Timeline.ToDateTime(BeginDate, BeginTime).ToString("s"),
                Timeline.ToDateTime(EndDate, EndTime).ToString("s"),
                Owner, Details));
        }
    }
}
