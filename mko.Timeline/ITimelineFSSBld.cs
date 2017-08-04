//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.5.2017
//
//  Projekt.......: mko.Timmeline
//  Name..........: ITimelineFSSBld.cs
//  Aufgabe/Fkt...: Struktur eines FilteredSortedSetBuilders für die Timeline
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
    /// <summary>
    /// Struktur eines FilteredSortedSetBuilders für die Timeline
    /// </summary>
    public interface ITimelineFSSBld : mko.BI.Repositories.Interfaces.IFilteredSortedSetBuilder<IAppointment>
    {
        /// <summary>
        /// überspringen der ersten n Datensätze
        /// </summary>
        int Skip { set; }


        /// <summary>
        ///  Mitnehmen der nächsten n Datensätze
        /// </summary>
        int Take { set; }

        /// <summary>
        /// Einschränken aller Termine auf die eines BEsitezers
        /// </summary>
        string Owner { set; }

        /// <summary>
        /// Einschränken aller Termine auf eine Kategorie
        /// </summary>
        AppointmentCategory Category { set; }

        /// <summary>
        /// Einschränken der Termine auf die, welche in einem gegeben Zeitraum liegen
        /// </summary>
        mko.BI.Bo.Interval<DateTime> Between { set; }

        /// <summary>
        /// Sortieren nach Besitzer
        /// </summary>
        /// <param name="desc"></param>
        void OrderByOwner(bool desc);

        /// <summary>
        /// Sortieren nach Terminbeginn
        /// </summary>
        /// <param name="desc"></param>
        void OrderByBegin(bool desc);

        /// <summary>
        /// Sortieren nach Terminende
        /// </summary>
        /// <param name="desc"></param>
        void OrderByEnd(bool desc);

        /// <summary>
        /// Sortieren nach Termindauer
        /// </summary>
        /// <param name="desc"></param>
        void OrderByDuration(bool desc);
    }
}
