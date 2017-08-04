//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 30.7.2017
//
//  Projekt.......: mko.Timeline.WebApi
//  Name..........: Timeline.cs
//  Aufgabe/Fkt...: Implementierung des Timeline- Repositories aus Basis 
//                  einer Restful WebApi für einen Appointment- Store
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

namespace mko.Timeline.WebApi
{
    public class Timeline : mko.Timeline.ITimeline
    {
        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IAppointmentBuilder Create()
        {
            throw new NotImplementedException();
        }

        public ITimelineFSSBld CreateFSSBld()
        {
            throw new NotImplementedException();
        }

        public void Delete(IAppointment appointment)
        {
            throw new NotImplementedException();
        }

        public void Delete(string Owner, IDate beginDate, ITime beginTime, IDate endDate, ITime endTime)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string Owner, IDate beginDate, ITime beginTime, IDate endDate, ITime endTime)
        {
            throw new NotImplementedException();
        }

        public IAppointment Get(string Owner, IDate beginDate, ITime beginTime, IDate endDate, ITime endTime)
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
