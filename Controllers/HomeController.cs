using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShipSoftball.Models;

namespace ShipSoftball.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Schedule> sched = Schedule.GetScheduleByDate(DateTime.Today);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}