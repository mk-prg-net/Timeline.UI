//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 31.7.2017
//
//  Projekt.......: Projektkontext
//  Name..........: Dateiname
//  Aufgabe/Fkt...: Index über einer NoSQL DB. Basis für Abfragen
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
    /// 
    /// </summary>
    /// <typeparam name="TDoc"></typeparam>
    /// <typeparam name="TQueryBuilder">Typ des Querybuilders</typeparam>
    /// <typeparam name="TSortOrderBuilder">Typ des Sortorder- Builders</typeparam>
    public interface IDocIndex<TDoc, out TQueryBuilder, out TSortOrderBuilder>
        where TQueryBuilder : MkPrgNet.Pattern.Repository.IQueryBuilder<string, TSortOrderBuilder>
        where TSortOrderBuilder : MkPrgNet.Pattern.Repository.ISortOrderBuilder<string>
    {
        /// <summary>
        /// True, wenn unter der Id ein gültiges Dokument existiert
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool Exists(string Id);


        /// <summary>
        /// Erfasst ein Dokument zusammen mit einer Id in einem Index
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Document"></param>
        Task AddToIndexAsync(string Id, TDoc Document);


        /// <summary>
        /// Entfernt ein Dokument zusammen mit seiner Id aus einem Index
        /// </summary>
        /// <param name="Id"></param>
        Task RemoveFromIndexAsync(string Id);


        /// <summary>
        /// Alle vorausgegangenen Änderungen werden, falls noch nicht erfolgt
        /// persistiert
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Erzeugt einen Querybuilder, mit dem die Mengen von DocIds, welche
        /// der Index Verwaltet, nach bestimmten Kriterien eingeschränkt werden kann
        /// </summary>
        /// <returns></returns>
        TQueryBuilder CreateQueryBuilder();


    }
}
