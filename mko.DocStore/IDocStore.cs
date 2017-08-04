//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 31.7.2017
//
//  Projekt.......: mko.DocStore
//  Name..........: IDocStore
//  Aufgabe/Fkt...: Allgemeine Schnittstelle einer NoSQL DokumentenDB
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

namespace mko.DocStore
{
    /// <summary>
    /// Allgemeine Schnittstelle einer NoSQL DokumentenDB
    /// </summary>
    public interface IDocStore<TDoc>
    { 
        /// <summary>
        /// Erzeugt eine neues Dokument unter der NodeId.
        /// Dokumente sind hierarchische Strukturen wie z.B. JSon. 
        /// Unter der NodeId wird dieses abgelegt.
        /// </summary>
        /// <param name="NodeId"></param>
        /// <returns></returns>
        Task PutAsync(string Id, TDoc Document);


        /// <summary>
        /// Liefert zu einer Id einen Dokument aus.
        /// Die Id kann ein komplexer Url sein. 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<TDoc> GetAsync(string Id);

        /// <summary>
        /// Löscht eine Dokument mit gegebener NodeId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task DeleteAsync(string Id);       
                
    }
}
