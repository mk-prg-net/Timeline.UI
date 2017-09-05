//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 2.8.2017
//
//  Projekt.......: mko.Timeline.DocStore
//  Name..........: Index.cs
//  Aufgabe/Fkt...: Implementiert einen Index auf einem DocStore für 
//                  Termine. Die Implementierung legt dazu Dateien
//                  mit Listen aus Datensätzen an, 
//                  die jeweils einer Eigenschaft wie Terminbeginn    
//                  eine Menge von TerminIds zugeordnen.
//                  Die Listen sind dann bezüglich der Eigenschaft wie Terminbeginn
//                  sortiert. 
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 10 mit .NET 4.6
//  Werkzeuge.....: Visual Studio 2015
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

using tmc = mko.Timeline.Timeline;
using Lisp = mko.Algo.Listprocessing.Fn;
using System.Diagnostics;

namespace mko.Timeline.DocStore
{
    public class Index : mko.DocStore.IDocIndex<IAppointment, QueryBuilder, SortOrderBuilder>
    {
        readonly string _IndexStorePath;

        const string IdsFileName = "ix.ids";
        const string BeginsSortedFileName = "ix.begins";
        const string EndsSortedFileName = "ix.ends";
        const string OwnersSortedFileName = "ix.owners";
        const string CategoriesSortedFileName = "ix.cats";


        /// <summary>
        /// Listet alle Dokument- Id's auf
        /// </summary>
        internal HashSet<string> Ids;

        /// <summary>
        /// Zu einem Datum werden alle TerminIds aufgelistet, die an diesem beginnen
        /// </summary>
        internal SortedList<DateTime, string[]> BeginsSorted;

        /// <summary>
        /// Zu einem Datum werden alle TerminIds aufgelistet, die an diesem enden
        /// </summary>
        internal SortedList<DateTime, string[]> EndsSorted;

        /// <summary>
        /// Zu einem Besitzer werden alle TerminIds aufgelistet, die an diesem zugeordnet sind
        /// </summary>
        internal SortedList<string, string[]> OwnersSorted;

        internal SortedList<AppointmentCategory, string[]> CategoriesSorted;

        private  object lockHandle = new Object();


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="IndexStorePath">Pfad, in dem die serialisierten Indexdateien liegen</param>
        public Index(string IndexStorePath)
        {
            _IndexStorePath = IndexStorePath;

            if (BeginsSorted == null || EndsSorted == null || OwnersSorted == null)
            {
                // Laden der Indizes von HDD
                {
                    // Ids laden
                    using (var stream = new System.IO.FileStream(Filename(IdsFileName), System.IO.FileMode.Create))
                    {
                        var reader = new System.IO.StreamReader(stream);
                        string IdsStr = reader.ReadToEnd();

                        if (string.IsNullOrWhiteSpace(IdsStr))
                        {
                            Ids = new HashSet<string>();
                        }
                        else
                        {
                            var IdsArr = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(IdsStr);
                            Ids = new HashSet<string>(IdsArr);
                        }
                    }
                }

                {
                    // Startzeitpunkte laden
                    using (var stream = new System.IO.FileStream(Filename(BeginsSortedFileName), System.IO.FileMode.Create))
                    {
                        var reader = new System.IO.StreamReader(stream);
                        string Str = reader.ReadToEnd();
                        SortedListLoadFrom(Str, out BeginsSorted);
                    }
                }

                {
                    // Terminenden laden
                    using (var stream = new System.IO.FileStream(Filename(EndsSortedFileName), System.IO.FileMode.Create))
                    {
                        var reader = new System.IO.StreamReader(stream);
                        string Str = reader.ReadToEnd();
                        SortedListLoadFrom(Str, out EndsSorted);
                    }
                }

                {
                    // Besitzer laden
                    using (var stream = new System.IO.FileStream(Filename(OwnersSortedFileName), System.IO.FileMode.Create))
                    {
                        var reader = new System.IO.StreamReader(stream);
                        string Str = reader.ReadToEnd();
                        SortedListLoadFrom(Str, out OwnersSorted);
                    }
                }

                {
                    // Kategorien laden
                    using (var stream = new System.IO.FileStream(Filename(CategoriesSortedFileName), System.IO.FileMode.Create))
                    {
                        var reader = new System.IO.StreamReader(stream);
                        string Str = reader.ReadToEnd();
                        SortedListLoadFrom(Str, out CategoriesSorted);
                    }
                }
            }
        }

        private static void SortedListLoadFrom<TKey>(string Str, out SortedList<TKey, string[]> SL)
        {
            SL = new SortedList<TKey, string[]>();
            if (!string.IsNullOrWhiteSpace(Str))            
            {
                var Arr = Newtonsoft.Json.JsonConvert.DeserializeObject<KeyValuePair<TKey, string[]>[]>(Str);                

                foreach (var kv in Arr)
                {
                    SL[kv.Key] = kv.Value;
                }
            }
        }

        /// <summary>
        /// Erzeugt den Namen einer Indexdatei
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Filename(string key)
        {
            return _IndexStorePath + "\\" + key + ".json";
        }

        /// <summary>
        /// Dokument zwecks Indizierung hinzufügen
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Document"></param>
        public async Task AddToIndexAsync(string Id, IAppointment Document)
        {
            await Task.Run(() =>
            {
                lock (lockHandle)
                {
                    mko.TraceHlp.ThrowArgExIf(Ids.Contains(Id), "Unter der Id " + Id + " existiert bereits ein Dokument");

                    Ids.Add(Id);

                    var beg = tmc.ToDateTime(Document.BeginDate, Document.BeginTime);
                    AddTo(BeginsSorted, beg, Id);

                    var end = tmc.ToDateTime(Document.EndDate, Document.EndTime);
                    AddTo(EndsSorted, end, Id);

                    AddTo(OwnersSorted, Document.Owner, Id);

                    AddTo(CategoriesSorted, Document.Category, Id);
                }
            });            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="sdict"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        private void AddTo<TKey>(SortedList<TKey, string[]> sdict, TKey key, string val)
        {            
            if (sdict.ContainsKey(key))
            {
                sdict[key] = sdict[key].Concat(Lisp.A(val)).ToArray();
            }
            else
            {
                sdict[key] = Lisp.A(val);
            }
        }

        /// <summary>
        /// Änderungen an den Collections auf HDD sichern
        /// </summary>
        /// <returns></returns>
        private async Task SaveAsync()
        {
            // Sichern auf HDD
            {
                string Str = "";
                lock (lockHandle)
                {
                    Str = Newtonsoft.Json.JsonConvert.SerializeObject(Ids.ToArray(), Newtonsoft.Json.Formatting.Indented);
                }                
                await SaveIndexPart(IdsFileName, Str);
            }

            {
                string Str = "";
                lock (lockHandle)
                {
                    Str = Newtonsoft.Json.JsonConvert.SerializeObject(BeginsSorted.ToArray(), Newtonsoft.Json.Formatting.Indented);
                }
                await SaveIndexPart(BeginsSortedFileName, Str);
            }

            {
                string Str = "";
                lock (lockHandle)
                {
                    Str = Newtonsoft.Json.JsonConvert.SerializeObject(EndsSorted.ToArray(), Newtonsoft.Json.Formatting.Indented);
                }
                await SaveIndexPart(EndsSortedFileName, Str);
            }

            {
                string Str = "";
                lock (lockHandle)
                {
                    Str = Newtonsoft.Json.JsonConvert.SerializeObject(OwnersSorted.ToArray(), Newtonsoft.Json.Formatting.Indented);
                }
                await SaveIndexPart(OwnersSortedFileName, Str);
            }

            {
                string Str = "";
                lock (lockHandle)
                {
                    Str = Newtonsoft.Json.JsonConvert.SerializeObject(CategoriesSorted.ToArray(), Newtonsoft.Json.Formatting.Indented);
                }
                await SaveIndexPart(CategoriesSortedFileName, Str);
            }
        }

        private async Task SaveIndexPart(string FileName, string Str)
        {
            bool dataWritten = false;
            do
            {
                try
                {
                    using (var stream = new System.IO.FileStream(Filename(FileName), System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None, useAsync: true, bufferSize: 1024))
                    {
                        var writer = new System.IO.StreamWriter(stream);
                        await writer.WriteAsync(Str);
                        dataWritten = true;
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Debug.WriteLine("Schreibzugriff auf " + FileName + " gescheitert");
                    System.Threading.Thread.Sleep(300);
                }
            } while (!dataWritten);
        }

        /// <summary>
        /// Prüfen, ob ein Dokument im Index aufgelistet ist
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Exists(string Id)
        {
            return Ids.Contains(Id);
        }

        /// <summary>
        /// Dokument von der Indezierung ausschließen
        /// </summary>
        /// <param name="Id"></param>
        public async Task RemoveFromIndexAsync(string Id)
        {
            await Task.Run(() =>
            {
                lock (lockHandle)
                {
                    Ids.Remove(Id);
                }
            });
        }


        /// <summary>
        /// Erzeugt einen neuen QueryBuilder, über den eine Abfrage aufgebaut werden kann
        /// </summary>
        /// <returns></returns>
        public QueryBuilder CreateQueryBuilder()
        {
            return new QueryBuilder(this);
        }

        public async Task SaveChangesAsync()
        {
            await SaveAsync();
        }
    }
}
