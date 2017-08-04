//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.5.2017
//
//  Projekt.......: mko.Timeline
//  Name..........: Time.cs
//  Aufgabe/Fkt...: Zeitpunkt auf der Zeitachse
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

using System.ComponentModel.DataAnnotations;

namespace mko.Timeline
{
    /// <summary>
    /// Zeitpunkt auf der Zeitachse
    /// </summary>
    public struct Time : ITime
    {

        public Time(DateTime dat)
        {
            Hour = dat.Hour;
            Minute = dat.Minute;
            Second = dat.Second;
            Millisecond = dat.Millisecond;
        }

        [Newtonsoft.Json.JsonConstructor]
        public Time(int Hour, int Minute = 0, int Second= 0, int Millisecond = 0)
        {
            this.Hour = Hour;
            this.Minute = Minute;
            this.Second = Second;
            this.Millisecond = Millisecond;
        }


        public Time(ITime time) : this(time.Hour, time.Minute, time.Second)
        {            
        }

        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }
        public int Millisecond { get; }

        public ITime AddHours(int hours)
        {
            return new Time((Hour + hours) % 24, Minute, Second, Millisecond);
        }

        public ITime AddMinutes(int minutes)
        {
            return new Time((Hour + (Minute + minutes) / 60) % 24, (Minute + minutes) % 60, Second, Millisecond);
        }

        public ITime AddSeconds(int seconds)
        {
            return new Time(
                (Hour + (Second + seconds) / 3600) % 24, 
                (Minute + (Second + seconds) / 60) % 60, 
                (Second + seconds) % 60, Millisecond);
        }

        public DateTime ToDateTime()
        {
            return new DateTime(1, 1, 1, Hour, Minute, Second);
        }

        public override string ToString()
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}", Hour, Minute, Second);
        }
    }
}
