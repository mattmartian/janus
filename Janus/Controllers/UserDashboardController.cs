using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Janus.Controllers
{
    public class UserDashboardController : Controller
    {
        private readonly JanusEntities _context;
        // GET: AdminDashboard

        public UserDashboardController()
        {
            _context = new JanusEntities();
        }

        // GET: UserDashboard
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Schedule()
        {
            ViewBag.username = Session["FirstName"] + " " + Session["LastName"];
            ViewBag.userID = Session["userID"];
            ViewBag.userEmail = Session["email"];
            ViewBag.access = Session["accessLevel"];

            int identification = Int32.Parse(Session["userID"].ToString());
            var shiftData = (from a in _context.Shifts where a.userID == identification select new Janus.Models.ScheduleViewModel { shiftID = a.shiftID, userID = a.userID, shiftDate = a.shiftDate, shiftStart = a.shiftStart, shiftEnd = a.shiftEnd, position = a.position, description = a.description, status = a.status });
            if (shiftData.Count() == 0) //?
            {
                ViewBag.noData = "No Shifts For This Week!";
            }

            ViewBag.data = shiftData;

            return View();
        }

        public ActionResult MakeRequest()
        {
            return View();
        }

        public ActionResult Mail()
        {
            return View();
        }
    }
}