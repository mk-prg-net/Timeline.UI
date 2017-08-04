//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 31.7.2017
//
//  Projekt.......: mko.Timeline.DocStore
//  Name..........: Docs.cs
//  Aufgabe/Fkt...: Implementierung eines DocStore für Appointments
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

using tmc = mko.Timeline.Timeline;

namespace mko.Timeline.DocStore
{
    /// <summary>
    /// Implementierung eines DocStore für Appointments
    /// </summary>
    public class Docs : mko.DocStore.IDocStore<IAppointment>, mko.DocStore.IDocIdGenerator<IAppointment>
    {
        readonly string _DocStorePath;

        string Filename(string key)
        {
            return _DocStorePath + "\\" + key + ".json";
        }

        // Implementierungsdetails
        System.Security.Cryptography.MD5 md5Gen;


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="DocStorePath">Pfad, unter dem die einzuelnen Termine gespeichert werden</param>
        public Docs(string DocStorePath)
        {
            _DocStorePath = DocStorePath;
            md5Gen = System.Security.Cryptography.MD5.Create();

        }


        public async Task DeleteAsync(string Id)
        {
            try
            {
                string fn = Filename(Id);
                await Task.Run(() => System.IO.File.Delete(fn));
            } catch(Exception ex)
            {
                mko.TraceHlp.ThrowEx(Id + " konnte nicht gelöscht werden", ex);
            }


        }

        public async Task<IAppointment> GetAsync(string Id)
        {
            try
            {
                string fn = Filename(Id);
                using (var reader = new System.IO.StreamReader(fn))
                {
                    string docSer = await reader.ReadToEndAsync();
                    var appointment = Newtonsoft.Json.JsonConvert.DeserializeObject<AppointmentBuilder>(docSer).Create();
                    return appointment;
                }

            }catch(Exception ex)
            {
                mko.TraceHlp.ThrowEx("Zugriff auf " + Id + " fehlgeschlagen", ex);
                return new AppointmentBuilder().Create();
            }
        }

        public async Task PutAsync(string Id, IAppointment Document)
        {
            try
            {
                string fn = Filename(Id);
                using (var writer = new System.IO.StreamWriter(fn))
                {
                    string docSer = Newtonsoft.Json.JsonConvert.SerializeObject(Document);
                    await writer.WriteLineAsync(docSer);
                }
            }
            catch (Exception ex)
            {
                mko.TraceHlp.ThrowEx("Schreiben des Terminsmit der ID " + Id + " fehlgeschlagen. Termin: " + Document, ex);                
            }
        }

        /// <summary>
        /// Erzeugt zu einem Dokument einen eindeutigen Schlüssel
        /// </summary>
        /// <param name="Document"></param>
        /// <returns></returns>
        public string CreateDocHashId(IAppointment Document)
        {
            try
            {
                string txt = Document.Owner
                            + Document.Category.ToString()
                            + tmc.ToDateTime(Document.BeginDate, Document.BeginTime)
                            + tmc.ToDateTime(Document.EndDate, Document.EndTime)
                            + Document.Details;

                byte[] bytes = Encoding.Unicode.GetBytes(txt);
                byte[] md5 = md5Gen.ComputeHash(bytes);

                return String.Join("", md5.Select(b => b.ToString("x2")).ToArray());

            } catch(Exception ex)
            {
                mko.TraceHlp.ThrowEx("DocHashId konnte nicht erzeugt werden für: " + Document, ex);
                return "";
            }
        }
    }
}
