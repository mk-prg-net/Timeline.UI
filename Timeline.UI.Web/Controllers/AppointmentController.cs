using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Timeline.UI.Web.Controllers
{
    public class AppointmentController : Controller
    {
        /// <summary>
        /// Referenz auf injeziertes Timeline- Repository
        /// </summary>
        mko.Timeline.ITimeline tl;

        /// <summary>
        /// Das Timeline- Repository wird via Dependenc- Injection
        /// über die Parameterliste injeziert. Siehe App_Start\UnityConfig.cs
        /// </summary>
        /// <param name="timeline"></param>
        public AppointmentController(mko.Timeline.ITimeline timeline)
        {
            tl = timeline;
        }


        // GET: Appointment
        public ActionResult Index(bool OrderByDesc = true)
        {
            var fssbld = tl.CreateFSSBld();
            fssbld.OrderByBegin(OrderByDesc);

            var tlVM = new Models.Timeline.TimelineVM(tl.Count, fssbld.GetSet());
            return View("IndexRelTiles", tlVM);
        }

        public ActionResult Create()
        {            
            return View(new Models.Timeline.Appointment());
        }

        public ActionResult Save(Models.Timeline.Appointment app)
        {
            //tl = new mko.Timeline.FS.Timeline(Server.MapPath(tlPath));

            if (ModelState.IsValid)
            {
                // Alten Termin, falls vorhanden 
                if (tl.Exists(app.Owner,
                              app.BeginDate, app.BeginTime,
                              app.EndDate, app.EndTime))
                    tl.Delete(app.Owner,
                              app.BeginDate, app.BeginTime,
                              app.EndDate, app.EndTime);

                var bld = tl.Create();
                bld.Owner = app.Owner;
                bld.BeginDate = app.BeginDate;
                bld.BeginTime = app.BeginTime;
                bld.Category = app.Category;
                bld.Details = app.Details;
                bld.EndDate = app.EndDate;
                bld.EndTime = app.EndTime;

                tl.SaveChanges();

                return Redirect("~/Appointment/Index");
            } else
            {
                return View("Create", app);
            }
        }

        public RedirectResult Delete(string Owner,
            string Begin,            
            string End)
        {
            //tl = new mko.Timeline.FS.Timeline(Server.MapPath(tlPath));

            var Beg =  DateTime.Parse(Begin);
            var _End = DateTime.Parse(End);

            var datBeg = new mko.Timeline.Date(Beg);
            var tBeg = new mko.Timeline.Time(Beg);
            var datEnd = new mko.Timeline.Date(_End);
            var tEnd = new mko.Timeline.Time(_End);


            tl.Delete(Owner, datBeg, tBeg, datEnd, tEnd);
            tl.SaveChanges();
            return Redirect("~/Appointment/Index");
        }
    }
}