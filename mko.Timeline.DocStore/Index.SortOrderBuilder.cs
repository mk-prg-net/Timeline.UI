//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 2.8.2017
//
//  Projekt.......: mko.Timeline.DocStore
//  Name..........: Index.SortOrderBuilder.cs
//  Aufgabe/Fkt...: Implementiert eine hierarchische Sortierung einer Menge 
//                  von DokumentIds bezüglich Sortierkriterien wie Terminbeginn,
//                  Terminende, Besitzer und Dauer eines Termins
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
using MkPrgNet.Pattern.Repository;

namespace mko.Timeline.DocStore
{
    /// <summary>
    /// Implementiert eine hierarchische Sortierung einer Menge 
    /// von DokumentIds bezüglich Sortierkriterien wie Terminbeginn,
    /// Terminende, Besitzer und Dauer eines Termins
    /// </summary>
    public class SortOrderBuilder : MkPrgNet.Pattern.Repository.ISortOrderBuilder<string>
    {
        Queue<HashSet<string>> IdSetsQueue = new Queue<HashSet<string>>();
        HashSet<string> Stop = new HashSet<string>();

        int DocIdCount = 0;

        internal SortOrderBuilder(System.Collections.Generic.HashSet<string> Ids)
        {
            DocIdCount = Ids.Count;
            IdSetsQueue.Enqueue(Ids);
            IdSetsQueue.Enqueue(Stop);
        }

        /// <summary>
        /// Sortieren nach Besitzer
        /// </summary>
        /// <param name="desc"></param>
        public void OrderByOwner(bool desc)
        {
            var CategoriesSorted = desc ? Index.OwnersSorted.Reverse() : Index.OwnersSorted;
            SplitInSubcategories(CategoriesSorted);
        }

        private void SplitInSubcategories<TKey>(IEnumerable<KeyValuePair<TKey, string[]>> CategoriesSorted)
        {
            // Jede DocId- Menge wird bezüglich der sortierten Owner in 
            // Untermengen eingeteilt. Die neu entstanden Untermengen werden
            // hinter dem Stopp- Symbol in die IdsQueue eingestellt. 
            while (IdSetsQueue.Peek() != Stop)
            {
                var hs = IdSetsQueue.Dequeue();

                foreach (var category in CategoriesSorted)
                {
                    var newHs = new HashSet<string>();
                    foreach (var id in category.Value)
                    {
                        if (hs.Contains(id))
                        {
                            hs.Remove(id);
                            newHs.Add(id);
                        }
                    }

                    if (newHs.Count > 0)
                    {
                        IdSetsQueue.Enqueue(newHs);
                    }
                }
            }
            // Altes Stopp- Symbol entfernen und neues einsetzen
            IdSetsQueue.Dequeue();
            IdSetsQueue.Enqueue(Stop);
        }

        /// <summary>
        /// Sortieren nach Terminbeginn
        /// </summary>
        /// <param name="desc"></param>
        public void OrderByBegin(bool desc)
        {
            var CategoriesSorted = desc ? Index.BeginsSorted.Reverse() : Index.BeginsSorted;
            SplitInSubcategories(CategoriesSorted);
        }

        /// <summary>
        /// Sortieren nach Terminende
        /// </summary>
        /// <param name="desc"></param>
        public void OrderByEnd(bool desc)
        {
            var CategoriesSorted = desc ? Index.EndsSorted.Reverse() : Index.EndsSorted;
            SplitInSubcategories(CategoriesSorted);
        }

        /// <summary>
        /// Sortieren nach Kategorie
        /// </summary>
        /// <param name="desc"></param>
        public void OrderByCategory(bool desc)
        {
            var CategoriesSorted = desc ? Index.CategoriesSorted.Reverse() : Index.CategoriesSorted;
            SplitInSubcategories(CategoriesSorted);
        }

        /// <summary>
        /// Sortieren nach Termindauer
        /// </summary>
        /// <param name="desc"></param>
        public void OrderByDuration(bool desc)
        {

        }


        public IFilteredSortedSet<string> GetFilteredSortedSet()
        {
            // DocIds aus allen Subkategorieen zu einer Liste zusammensetzen
            List<string> Ids = new List<string>(DocIdCount);

            while(IdSetsQueue.Peek() != Stop)
            {
                var cat = IdSetsQueue.Dequeue();
                foreach(var id in cat)
                {
                    Ids.Add(id);
                }
            }

            return new FilteredSortedSet(Ids);
        }
    }
}
