//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.5.2017
//
//  Projekt.......: mko.Timeline
//  Name..........: IAppointmentBuilder.cs
//  Aufgabe/Fkt...: Struktur der Settings für Klassenfabriken von 
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

namespace mko.Timeline
{
    public interface IAppointmentBuilder
    {

        /// <summary>
        /// Eingabevalidierung
        /// True, wenn alle bis Dato erfolgten Eingaben einen gültigen 
        /// Terimn ergeben.
        /// </summary>
        bool IsValid { get; }

        IDate BeginDate { set; }        
        ITime BeginTime { set; }


        IDate EndDate { set; }
        ITime EndTime { set; }

        mko.BI.Bo.Addresses.ILocation Location { set; }

        /// <summary>
        /// Ort, an dem der Termin stattfindet, definieren
        /// </summary>
        AppointmentCategory Category { set; }

        /// <summary>
        /// Eigentümer des Termins setzen
        /// </summary>
        string Owner { set; }

        /// <summary>
        /// Details zum Termin setzen
        /// </summary>
        string Details { set; }

        /// <summary>
        /// Erzeugt einen neuen Termin in einem Terminkalender, und gibt ihn 
        /// zurück
        /// </summary>
        /// <returns></returns>
        IAppointment Create();

    }
}
