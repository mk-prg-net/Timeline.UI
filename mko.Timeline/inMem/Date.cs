//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.5.2017
//
//  Projekt.......: mko.Timeline
//  Name..........: Date.cs
//  Aufgabe/Fkt...: Implementierung eines Zeitpunktes auf der Zeitachse
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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 18.6.2017
//  Änderungen....: setter in deneinzelnen Datumskomponenten implementiert, um 
//                  Defaultkonstruktor für JSON- Deserialisierung zu implementieren
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Timeline
{
    public class Date : IDate
    {
        DateTime _dat;

        public Date(DateTime dat)
        {
            _dat = new DateTime(dat.Year, dat.Month, dat.Day);
        }

        [Newtonsoft.Json.JsonConstructor]
        public Date(int Year, int Month = 1, int Day = 1)
        {
            _dat = new DateTime(Year, Month, Day);
        }

        public Date(IDate date) : this(date.Year, date.Month, date.Day) { }

        public int Day
        {
            get
            {
                return _dat.Day;
            }
        }

        public int Month
        {
            get
            {
                return _dat.Month;
            }
        }

        public int Year
        {
            get
            {
                return _dat.Year;
            }
        }

        public DateTime ToDateTime()
        {
            return new DateTime(Year, Month, Day);
        }

        public override string ToString()
        {
            return _dat.ToShortDateString();
        }

    }
}
