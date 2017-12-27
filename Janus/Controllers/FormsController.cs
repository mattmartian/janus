using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Janus.Controllers
{
    public class FormsController : Controller
    {
        private readonly JanusEntities _context;

        public FormsController()
        {
            _context = new JanusEntities();
        }

        // GET: Forms
        public ActionResult BookOff()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TimeOffRequest()
        {
            int user = Int32.Parse(Session["userID"].ToString());
            string start = Request["startTime"];
            string end = Request["endTime"];
            string desc = Request["description"];

            _context.AbsenceClaims.Add(new AbsenceClaims
            {
                userID = user,
                startTime = start,
                endTime = end,
                description = desc,
                claimType = "Book Off"
            });
            _context.SaveChanges();

            return RedirectToAction("RequestSubmitted", "Forms");
        }

        public ActionResult AbsenceClaim()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileClaim()
        {
            int user = Int32.Parse(Session["userID"].ToString());
            string start = Request["startTime"];
            string end = Request["endTime"];
            string desc = Request["description"];
            string absType = Request["claimType"];

            _context.AbsenceClaims.Add(new AbsenceClaims
            {
                userID = user,
                startTime = start,
                endTime = end,
                description = desc,
                claimType = absType
            });
            _context.SaveChanges();
            return RedirectToAction("RequestSubmitted", "Forms");
        }

        public ActionResult SelectEmployee()
        {
            var employees = (from b in _context.Users select new Janus.Models.EmployeeDetailViewModel { userID = b.userID, firstName = b.firstName, lastName = b.lastName });
            ViewBag.data = employees;

            return View();
        }

        [HttpPost]
        public ActionResult EmployeeSelected()
        {
            string requestWith = Request["requestWith"];

            TempData["requestWith"] = requestWith;

            return RedirectToAction("SwitchShift", "Forms");
        }

        public ActionResult SwitchShift()
        {
            int userID = Int32.Parse(@Session["userID"].ToString());
            int requestedWith = Int32.Parse(TempData["requestWith"].ToString());
            var employees = (from b in _context.Users select new Janus.Models.EmployeeDetailViewModel { userID = b.userID, firstName = b.firstName, lastName = b.lastName });
            var requestorShift = (from a in _context.Shifts where a.userID == userID select new Janus.Models.ShiftViewModel { shiftID = a.shiftID, userID = a.userID, shiftDate = a.shiftDate, shiftStart = a.shiftStart, shiftEnd = a.shiftEnd, position = a.position, description = a.description, status = a.status });
            var requestWithShift = (from a in _context.Shifts where a.userID == requestedWith select new Janus.Models.ShiftViewModel { shiftID = a.shiftID, userID = a.userID, shiftDate = a.shiftDate, shiftStart = a.shiftStart, shiftEnd = a.shiftEnd, position = a.position, description = a.description, status = a.status });
            ViewBag.data = employees;
            ViewBag.requestWith = requestedWith;
            ViewBag.requestorData = requestorShift;
            ViewBag.requestWithData = requestWithShift;
            return View();
        }

        public ActionResult RequestSubmitted()
        {
            return View();
        }
    }
}