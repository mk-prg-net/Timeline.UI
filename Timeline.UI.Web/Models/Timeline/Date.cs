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

namespace Timeline.UI.Web.Models.Timeline
{
    public class Date : mko.Timeline.IDate
    {
        public Date()
        {
            var n = DateTime.Now;
            Year = n.Year;
            Month = n.Month;
            Day = n.Day;
        }

        public int Day
        {
            get; set;
        }

        public int Month
        {
            get; set;
        }

        public int Year
        {
            get; set;
        }

        public DateTime ToDateTime()
        {
            return new mko.Timeline.Date(Year, Month, Day).ToDateTime();
        }
    }
}