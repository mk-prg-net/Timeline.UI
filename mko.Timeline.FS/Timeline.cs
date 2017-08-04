//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 16.6.2017
//
//  Projekt.......: mko.Timeline.FS
//  Name..........: Timeline.cs
//  Aufgabe/Fkt...: Implementierung eine Kalenders, der einzelne 
//                  Termine in Dateien in einem Unterverzeichnis speichert
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

using mko.Timeline;
using tmc = mko.Timeline.Timeline;

namespace mko.Timeline.FS
{
    public class Timeline : ITimeline
    {
        string _timelinePath;

        public string Filename(string key)
        {
            return _timelinePath + "\\" + key + ".json";
        }

        // Implementierungsdetails

        System.Security.Cryptography.MD5 md5Gen;


        /// <summary>
        /// Erzeugt aus dem Besitzernamen, dem  Start- und Enddatum einen eindeutigen MD5- Hash
        /// als Schlüssel
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public string GetKey(IAppointment appointment)
        {
            return GetKey(appointment.Owner,
                tmc.ToDateTime(appointment.BeginDate, appointment.BeginTime),
                tmc.ToDateTime(appointment.EndDate, appointment.EndTime));
        }

        /// <summary>
        /// Erzeugt aus dem Besitzernamen, dem  Start- und Enddatum einen eindeutigen MD5- Hash
        /// als Schlüssel
        /// </summary>
        /// <param name="Owner"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string GetKey(string Owner, DateTime begin, DateTime end)
        {
            string txt = Owner
                        + begin.ToShortDateString()
                        + end.ToShortDateString();

            byte[] bytes = Encoding.Unicode.GetBytes(txt);
            byte[] md5 = md5Gen.ComputeHash(bytes);

            return String.Join("", md5.Select(b => b.ToString("x2")).ToArray());
        }

        Queue<AppointmentBuilder> CreateJobQueue = new Queue<AppointmentBuilder>();
        Queue<string> DeleteJobQueue = new Queue<string>();

        public Timeline(string Path)
        {
            _timelinePath = Path;
            md5Gen = System.Security.Cryptography.MD5.Create();

        }

        public int Count
        {
            get
            {
                return System.IO.Directory.GetFiles(_timelinePath).Count();
            }
        }

        public IAppointmentBuilder Create()
        {
            var bld = new AppointmentBuilder();
            CreateJobQueue.Enqueue(bld);
            return bld;
        }

        public ITimelineFSSBld CreateFSSBld()
        {
            return new TimelineFSSBld(_timelinePath);
        }

        public void Delete(IAppointment appointment)
        {
            DeleteJobQueue.Enqueue(GetKey(appointment.Owner,
                tmc.ToDateTime(appointment.BeginDate, appointment.BeginTime),
                tmc.ToDateTime(appointment.EndDate, appointment.EndTime)));
        }


        public bool Exists(string Owner, IDate beginDate, ITime beginTime, IDate endDate, ITime endTime)
        {
            return System.IO.File.Exists(Filename(GetKey(Owner,
                                                        tmc.ToDateTime(beginDate, beginTime),
                                                        tmc.ToDateTime(endDate, endTime))));
        }

        public IAppointment Get(string Owner, IDate beginDate, ITime beginTime, IDate endDate, ITime endTime)
        {
            mko.TraceHlp.ThrowArgExIfNot(Exists(Owner, beginDate, beginTime, endDate, endTime), "Termin existiert nicht");

            using (System.IO.StreamReader reader = new System.IO.StreamReader(Filename(GetKey(Owner,
                                                        tmc.ToDateTime(beginDate, beginTime),
                                                        tmc.ToDateTime(endDate, endTime)))))
            {
                // Json Deserialisierung.
                // Achtung. Appointments sind nur lesbar. Nur ein AppointmentBuilder kann zum erzeugen eines neuen Termins dienen.
                // In diesem ist ein spezieller Konstruktor mittels [JsonConstructor] Attribut gekennzeichnet
                var abld = Newtonsoft.Json.JsonConvert.DeserializeObject<AppointmentBuilder>(reader.ReadToEnd());

                mko.TraceHlp.ThrowArgExIfNot(abld.IsValid, "Termin, auf den zugegriffen werden soll, ist ungültig");

                return abld.Create();
            }
        }

        public void Delete(string Owner, IDate beginDate, ITime beginTime, IDate endDate, ITime endTime)
        {
            DeleteJobQueue.Enqueue(GetKey(Owner,tmc.ToDateTime(beginDate, beginTime), tmc.ToDateTime(endDate, endTime)));
        }


        public void Rollback()
        {
            CreateJobQueue.Clear();
            DeleteJobQueue.Clear();
        }

        public void SaveChanges()
        {
            // Alle Jobs, die Termine erstellen, realisieren
            while (CreateJobQueue.Count > 0)
            {
                var job = CreateJobQueue.Dequeue();

                mko.TraceHlp.ThrowArgExIfNot(job.IsValid, "ungültige Termine");

                var appointment = job.Create();
                string key = GetKey(appointment);

                string filename = _timelinePath + "\\" + key + ".json";
                if (System.IO.File.Exists(filename))
                {
                    // alten Termin löschen
                    System.IO.File.Delete(filename);
                }

                // Termin als JSon- Serialisiertes Objekt schreiben
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filename))
                {
                    writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(appointment, Newtonsoft.Json.Formatting.Indented));
                    writer.Flush();
                }
            }

            // Alle Jobs, die Termine löschen, realisieren
            while (DeleteJobQueue.Count > 0)
            {
                var key = DeleteJobQueue.Dequeue();

                string filename = _timelinePath + "\\" + key + ".json";
                mko.TraceHlp.ThrowArgExIfNot(System.IO.File.Exists(filename), "Nicht existierender Termin " + key + " sollte gelöscht werden");

                System.IO.File.Delete(filename);
            }
        }

    }
}
