using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShipSoftball.Models;

namespace ShipSoftball.Controllers
{
    public class ScheduleController : Controller
    {
        List<Schedule> sched = Schedule.GetFullSchedule();
        //
        // GET: /Schedule/
        public ActionResult Index()
        {
            
            return View(sched);
        }
	}
}