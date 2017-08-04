//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: mko.Timeline.DocStore
//  Name..........: Index.QueryBuilder.cs
//  Aufgabe/Fkt...: Implementiert die Einschränkung der Menge von DokumentId's
//                  bezüglich mehrerer unabhängiger Abfragekriterien
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
using mko.BI.Repositories.Interfaces;

using Lisp = mko.Algo.Listprocessing.Fn;

namespace mko.Timeline.DocStore
{
    public class QueryBuilder : MkPrgNet.Pattern.Repository.IQueryBuilder<string, SortOrderBuilder>
    {
        HashSet<string> Ids;

        public QueryBuilder()
        {
            // Kopieren der Ids. Zu Beginn wird die gesammte Menge an Terminen dargestellt
            Ids = new HashSet<string>(Index.Ids);
        }

        public SortOrderBuilder GetSortOrderBuilder()
        {
            return new SortOrderBuilder(Ids);
        }

        /// <summary>
        /// Schränkt auf einen Besitzer ein
        /// </summary>
        public string Owner
        {
            set
            {
                if (Index.OwnersSorted.ContainsKey(value))
                {
                    Ids.IntersectWith(Index.OwnersSorted[value]);
                }
            }
        }

        /// <summary>
        /// Schränkt auf eine Kategorie ein
        /// </summary>
        public AppointmentCategory Category
        {
            set
            {
                if (Index.CategoriesSorted.ContainsKey(value))
                {
                    Ids.IntersectWith(Index.CategoriesSorted[value]);
                }
            }
        }

        /// <summary>
        /// Schränkt Termine auf einen Zeitraum ein
        /// </summary>
        public mko.BI.Bo.Interval<DateTime> Between
        {
            set
            {
                var allbeg = Lisp.L<string>();
                foreach(var arr in Index.BeginsSorted.Where(kv => kv.Key >= value.Begin && kv.Key < value.End).Select(kv => kv.Value))
                {
                    allbeg = allbeg.Concat(arr);
                }

                var allend = Lisp.L<string>();
                foreach (var arr in Index.EndsSorted.Where(kv => kv.Key >= value.Begin && kv.Key < value.End).Select(kv => kv.Value))
                {
                    allend = allend.Concat(arr);
                }

                var flt = allbeg.Intersect(allend);

                Ids.IntersectWith(flt);
            }
        }



    }
}
