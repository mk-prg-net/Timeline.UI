//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.5.2017
//
//  Projekt.......: mko.Timeline
//  Name..........: ITimeline.cs
//  Aufgabe/Fkt...: Terminkalender, der durch ein Repository über Appointments
//                  dargestellt wird
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
    /// Terminkalender, der durch ein Repository über Appointments
    /// dargestellt wird
    /// </summary>
    public interface ITimeline 
    {
        /// <summary>
        /// Anzahl der gespeicherten Termine
        /// </summary>
        int Count { get; }


        /// <summary>
        /// Prüft, ob ein Termin mit dem übergebenen Terminschlüssel existiert
        /// </summary>
        /// <param name="Owner"></param>
        /// <param name="beginDate"></param>
        /// <param name="beginTime"></param>
        /// <param name="endDate"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        bool Exists(string Owner, IDate beginDate, ITime beginTime, IDate endDate, ITime endTime);


        /// <summary>
        /// Erzeugt einen neuen Terminbuilder. Dieser kann konfiguriert werden. 
        /// Am Ende sollte SaveChanges aufgerufen werden, um den neuen Termin in der DB zu 
        /// übernehmen.
        /// Es wurde bewusst auf eine GetBld(...) Funktion, die ein Update ermöglichen 
        /// würde, verzichtet. Stattdessen wird ein Update durch ein Delete des 
        /// alten Termins, und Create() eines Termins mit den aktualisierten Daten
        /// implementiert. Vorteil:  die zugrundeliegende Collection muss nicht 
        /// über eventuell aufwändig zu implementierende Updates verfügen. Die Einträge
        /// müssen nur lesbar sein ! 
        /// </summary>
        /// <returns></returns>
        IAppointmentBuilder Create();

        /// <summary>
        /// Lesender Zugriff auf einen einzelnen Termin. 
        /// Fachschlüssel setzt sich aus Besitzer, Start- und Endzeitpunkt zusammen
        /// </summary>
        /// <param name="Owner"></param>
        /// <param name="beginDate"></param>
        /// <param name="beginTime"></param>
        /// <param name="endDate"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        IAppointment Get(string Owner, IDate beginDate, ITime beginTime, IDate endDate, ITime endTime);
                


        /// <summary>
        /// Löscht Termin mit dem übergebnen Fachschlüssel
        /// </summary>
        /// <param name="Owner"></param>
        /// <param name="beginDate"></param>
        /// <param name="beginTime"></param>
        /// <param name="endDate"></param>
        /// <param name="endTime"></param>
        void Delete(string Owner, IDate beginDate, ITime beginTime, IDate endDate, ITime endTime);

        /// <summary>
        /// Der übergebene Termin wird im Repository gelöscht
        /// </summary>
        /// <param name="appointment"></param>
        void Delete(IAppointment appointment);


        /// <summary>
        /// Alle Änderungen werden am Repository durchgeführt
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Alle noch nicht mit SaveChanges gesicherten Änderungen wieder zurücknehmen
        /// </summary>
        void Rollback();


        /// <summary>
        /// Erzeugt einen Filterd Sorted Set Builder, mit dem der Klaender nach vielfältigen 
        /// Kriterien gefiltert werden kann
        /// </summary>
        /// <returns></returns>
        ITimelineFSSBld CreateFSSBld();

    }
}
