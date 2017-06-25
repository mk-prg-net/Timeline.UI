//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 19.6.2017
//
//  Projekt.......: Projektkontext
//  Name..........: Dateiname
//  Aufgabe/Fkt...: Kurzbeschreibung
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
using System.Web;
using mko.BI.Bo.Addresses;
using mko.Timeline;

using System.ComponentModel.DataAnnotations;

namespace Timeline.UI.Web.Models.Timeline
{
    public class Appointment : mko.Timeline.IAppointment
    {
        public Appointment()
        {
            this.BeginDate = new Date();
            this.BeginTime = new Time();
            this.EndDate = new Date();
            this.EndTime = new Time();
            this.Location = new Location();
        }
        
        IDate IAppointment.BeginDate
        {
            get
            {
                return this.BeginDate;
            }
        }

        [Required]
        [UIHint("Date")]
        public Date BeginDate { get; }

        ITime IAppointment.BeginTime
        {
            get
            {
                return BeginTime;
            }
        }

        [Required]
        [UIHint("Time")]
        public Time BeginTime { get; }

        AppointmentCategory IAppointment.Category
        {
            get
            {
                return this.Category;
            }
        }

        [Required]
        [EnumDataType(typeof(mko.Timeline.AppointmentCategory))]
        public AppointmentCategory Category { get; set; }

        string IAppointment.Details
        {
            get
            {
                return this.Details;
            }
        }
        public string Details { get; set; }
        
        IDate IAppointment.EndDate
        {
            get
            {
                return this.EndDate;
            }
        }

        [Required]
        [UIHint("Date")]
        public Date EndDate { get; }

        ITime IAppointment.EndTime
        {
            get
            {
                return this.EndTime;
            }
        }

        [Required]
        [UIHint("Time")]
        public Time EndTime { get; }

        ILocation IAppointment.Location
        {
            get
            {
                return this.Location;
            }
        }

        [Required]
        public mko.BI.Bo.Addresses.Location Location { get; }

        
        string IAppointment.Owner
        {
            get
            {
                return this.Owner;
            }
        }

        [Required]
        public string Owner { get; set; }
    }
}