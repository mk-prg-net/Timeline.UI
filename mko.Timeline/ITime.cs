//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.5.2017
//
//  Projekt.......: mko.Timeline
//  Name..........: ITime.cs
//  Aufgabe/Fkt...: Struktur einer Aufzeichnung für einen Zeitpunkt auf einem 24h Uhrenkreis (Ziffernblatt)
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

namespace mko.Timeline
{
    public interface ITime
    {
        int Hour { get; }
        int Minute { get; }
        int Second { get; }
        int Millisecond { get; }

        ITime AddHours(int hours);
        ITime AddMinutes(int minutes);
        ITime AddSeconds(int seconds);

        System.DateTime ToDateTime();
    }
}
