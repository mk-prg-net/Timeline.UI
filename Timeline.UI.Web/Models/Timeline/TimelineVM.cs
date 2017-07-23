//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 24.7.2017
//
//  Projekt.......: Timeline.UI.Web
//  Name..........: TimelineVM.cs
//  Aufgabe/Fkt...: Timeline View Model
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

namespace Timeline.UI.Web.Models.Timeline
{
    public class TimelineVM
    {
        public TimelineVM(int Count, mko.BI.Repositories.Interfaces.IFilteredSortedSet<mko.Timeline.IAppointment> Timeline)
        {
            this.Count = Count;
            this.Timeline = Timeline;
        }

        public int Count { get;}

        public mko.BI.Repositories.Interfaces.IFilteredSortedSet<mko.Timeline.IAppointment> Timeline { get; }

    }
}