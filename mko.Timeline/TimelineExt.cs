//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.5.2017
//
//  Projekt.......: mko.Timeline
//  Name..........: Timelinext.cs
//  Aufgabe/Fkt...: Erweiterungsmethoden für Timelineobjekte
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
    public static class TimelineExt
    {
        public static bool Equ(this ITime me, ITime other)
        {
            return me.Hour == other.Hour && me.Minute == other.Minute && me.Second == other.Second && me.Millisecond == other.Millisecond;
        }


        public static bool Equ(this IDate me, IDate other)
        {
            return me.Day == other.Day && me.Month == other.Month && me.Year == other.Year;
        }


        public static string ToStr(this IDate me)
        {
            return string.Format("{0:D4}-{1:D2}-{2:D2}", me.Year, me.Month, me.Day);
        }

        public static string ToStr(this ITime me)
        {
            return string.Format("{0:D2}-{1:D2}-{2:D2}", me.Hour, me.Minute, me.Second);
        }


    }
}
