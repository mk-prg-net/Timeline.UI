//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 2.8.2017
//
//  Projekt.......: mko.Timeline
//  Name..........: IAppointmentExt.cs
//  Aufgabe/Fkt...: Erweiterungmethoden für vergleich etc. von
//                  Terminen
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
    public static class IAppointmentExt
    {
        public static bool Equ(this IAppointment me, IAppointment other)
        {
            bool eq = true;

            eq &= me.BeginDate.Equ(other.BeginDate);
            eq &= me.BeginTime.Equ(other.BeginTime);
            eq &= me.Category == other.Category;
            eq &= me.Details == other.Details;
            eq &= me.EndDate.Equ(other.EndDate);
            eq &= me.EndTime.Equ(other.EndTime);
            eq &= me.Location.Equ(other.Location);

            return eq;
        }
    }
}
