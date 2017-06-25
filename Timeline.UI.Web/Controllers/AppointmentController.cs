﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Timeline.UI.Web.Controllers
{
    public class AppointmentController : Controller
    {

        const string tlPath = @"~/App_Data/TimelineRepo";
        mko.Timeline.ITimeline tl;

        public AppointmentController()
        {            
        }
        

        // GET: Appointment
        public ActionResult Index()
        {
            tl = new mko.Timeline.FS.Timeline(Server.MapPath(tlPath));
            return View(tl);
        }

        public ActionResult Create()
        {            
            return View(new Models.Timeline.Appointment());
        }

        public ActionResult Save(Models.Timeline.Appointment app)
        {
            tl = new mko.Timeline.FS.Timeline(Server.MapPath(tlPath));

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

        public ActionResult Delete(string Owner,
            string Begin,            
            string End)
        {
            tl = new mko.Timeline.FS.Timeline(Server.MapPath(tlPath));

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