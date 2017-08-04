//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.5.2017
//
//  Projekt.......: mko.Timeline
//  Name..........: AppointmentBuilder.cs
//  Aufgabe/Fkt...: Implementierung eines Termin- Builders
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

namespace mko.Timeline
{
    public class AppointmentBuilder : IAppointmentBuilder
    {

        public AppointmentBuilder()
        {
            var now = DateTime.Now;
            _loc = new BI.Bo.Addresses.Location();
            _loc.City = "";
            _loc.Country = System.Threading.Thread.CurrentThread.CurrentCulture.Name.Split('-')[1];
            _loc.PostalCode = "";
            _loc.Street = "";

            _Owner = "";

            _beginDate = new Date(now);
            _beginTime = new Time(now);
            _endDate = new Date(now);
            _endTime = new Time(now);
        }

        /// <summary>
        /// Erstell AppointmentBuilder für einen vorhandenen Termin
        /// </summary>
        /// <param name="appointment"></param>
        public AppointmentBuilder(IAppointment appointment)
        {
            _Owner = appointment.Owner;
            _beginDate = new Date(appointment.BeginDate);
            _beginTime = new Time(appointment.BeginTime);
            _endDate = new Date(appointment.EndDate);
            _endTime = new Time(appointment.EndTime);
            _details = appointment.Details;
            _cat = appointment.Category;
            _loc = new BI.Bo.Addresses.Location()
            {
                City = appointment.Location.City,
                PostalCode = appointment.Location.PostalCode,
                Country = appointment.Location.Country,
                Street = appointment.Location.Street
            };        
        }


        [Newtonsoft.Json.JsonConstructor]
        public AppointmentBuilder(
                Date BeginDate,
                Time BeginTime,
                Date EndDate,
                Time EndTime,
                AppointmentCategory category,
                string Owner,
                string Details,
                mko.BI.Bo.Addresses.Location Location
            )
        {
            _Owner = Owner;
            _beginDate = BeginDate;
            _beginTime = BeginTime;
            _endDate = EndDate;
            _endTime = EndTime;
            _cat = category;
            _details = Details;
            _loc = Location;
        }


        public IDate BeginDate
        {
            set
            {
                _beginDate = new Date(value);
            }
        }
        Date _beginDate;

        public ITime BeginTime
        {
            set
            {
                _beginTime = new Time(value);
            }
        }
        Time _beginTime;

        public AppointmentCategory Category
        {
            set
            {
                _cat = value;
            }
        }
        AppointmentCategory _cat;


        public string Details
        {
            set
            {
                _details = value;
            }
        }
        string _details;

        public IDate EndDate
        {
            set
            {
                _endDate = new Date(value);
            }
        }
        Date _endDate;

        public ITime EndTime
        {
            set
            {
                _endTime = new Time(value);
            }
        }
        Time _endTime;

        public bool IsValid
        {
            get
            {
                var begin = new DateTime(_beginDate.Year, _beginDate.Month, _beginDate.Day, _beginTime.Hour, _beginTime.Minute, _beginTime.Second);
                var end = new DateTime(_endDate.Year, _endDate.Month, _endDate.Day, _endTime.Hour, _endTime.Minute, _endTime.Second);
                return begin <= end && !string.IsNullOrWhiteSpace(_Owner);
            }
        }

        public ILocation Location
        {
            set
            {
                _loc = new Location()
                {
                    City = value.City,
                    Country = value.Country,
                    PostalCode = value.PostalCode,
                    Street = value.Street
                };
            }
        }

        public string Owner
        {
            set
            {
                _Owner = value;
            }
        }
        string _Owner;

        mko.BI.Bo.Addresses.Location _loc;

        public IAppointment Create()
        {
            return new Appointment(_beginDate, _beginTime, _endDate, _endTime, _Owner,  _loc, _cat, _details);
        }
    }
}
