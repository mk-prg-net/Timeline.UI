//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.5.2017
//
//  Projekt.......: mko.Timeline
//  Name..........: Appointment
//  Aufgabe/Fkt...: Strutur eines Termineintrages in einem Kalenmder
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
    public interface IAppointment
    {
        /// <summary>
        /// Startdatum
        /// </summary>
        IDate BeginDate { get; }

        /// <summary>
        /// Startzeit
        /// </summary>
        ITime BeginTime { get; }

        /// <summary>
        /// Datum Terminende
        /// </summary>
        IDate EndDate { get; }

        /// <summary>
        /// Zeit Terminende
        /// </summary>
        ITime EndTime { get; }

        /// <summary>
        /// Ort, an dem der Termin stattfindet
        /// </summary>
        mko.BI.Bo.Addresses.ILocation Location { get; }

        /// <summary>
        /// Terminart
        /// </summary>
        AppointmentCategory Category { get; }

        /// <summary>
        /// Besitzer des Termins
        /// </summary>
        string Owner { get; }

        /// <summary>
        /// Detailinformationen zum Termin
        /// </summary>
        string Details { get; }
    }
}
