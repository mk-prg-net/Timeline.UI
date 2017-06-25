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
using mko.Timeline;

namespace Timeline.UI.Web.Models.Timeline
{
    public class Time : mko.Timeline.ITime
    {
        public int Hour
        {
            get; set;
        }

        public int Millisecond
        {
            get; set;
        }

        public int Minute
        {
            get; set;
        }

        public int Second
        {
            get; set;
        }

        public ITime AddHours(int hours)
        {
            return new mko.Timeline.Time(Hour, Minute, Second).AddHours(hours);
        }

        public ITime AddMinutes(int minutes)
        {
            return new mko.Timeline.Time(Hour, Minute, Second).AddHours(minutes);
        }

        public ITime AddSeconds(int seconds)
        {
            return new mko.Timeline.Time(Hour, Minute, Second).AddHours(seconds);
        }

        public DateTime ToDateTime()
        {
            return new mko.Timeline.Time(Hour, Minute, Second).ToDateTime();
        }
    }
}