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
            TempData["reqWith"] = requestedWith;
            ViewBag.requestorData = requestorShift;
            ViewBag.requestWithData = requestWithShift;
            return View();
        }

        [HttpPost]
        public ActionResult FileShiftChange()
        {
            int userid = Int32.Parse(@Session["userID"].ToString());
            string requestor = @Session["name"].ToString();
            int requestWith = Int32.Parse(TempData["reqWith"].ToString());
            int requestorShiftID = Int32.Parse(Request["requestorShift"]);
            int requestWithShiftID = Int32.Parse(Request["requestWithShift"]);
            int retrievedID = 0;
            string retrievedDate = "";
            int retrievedStart = 0;
            int retrievedEnd = 0;
            int retID = 0;
            string retDate = "";
            int retStart = 0;
            int retEnd = 0;
            string retrievedName = "";

            var query = from u in _context.Shifts
                        where u.shiftID == requestorShiftID
                        orderby u.shiftID
                        select u;
            foreach (var shift in query)
            {
                retrievedID = shift.shiftID;
                retrievedDate = shift.shiftDate;
                retrievedStart = shift.shiftStart;
                retrievedEnd = shift.shiftEnd;
            }
            string requestorShiftString = retrievedDate.ToString() + " " + retrievedStart.ToString() + " " + retrievedEnd.ToString();

            var q = from u in _context.Shifts
                    where u.shiftID == requestWithShiftID
                    orderby u.shiftID
                    select u;
            foreach (var shft in q)
            {
                retID = shft.shiftID;
                retDate = shft.shiftDate;
                retStart = shft.shiftStart;
                retEnd = shft.shiftEnd;
            }
            string requestWithShiftString = retDate.ToString() + " " + retStart.ToString() + " " + retEnd.ToString();

            var requestWithUserName = from u in _context.Users
                                      where u.userID == requestWith
                                      orderby u.userID
                                      select u;
            foreach (var user in requestWithUserName)
            {
                retrievedName = user.firstName + " " + user.lastName;
            }

            _context.shiftRequests.Add(new shiftRequests
            {
                managerSignOff = null,
                requestor = requestor,
                requestorID = userid,
                requestorShiftID = requestorShiftID,
                requestorShift = requestorShiftString,
                requestWith = retrievedName,
                requestWithShiftID = requestWithShiftID,
                requestWithShift = requestWithShiftString,
                requestWithID = requestWith,
                requestConfirmed = null,
                requestStatus = "Awaiting Employee Approval",
            });
            _context.SaveChanges();

            _context.Messages.Add(new Messages
            {
                mailFromUserID = userid,
                mailFromUsername = Session["name"].ToString(),
                mailToUserID = requestWith,
                mailToUsername = retrievedName,
                subject = "Shift Swap Request",
                body = requestor + " Would Like To Switch Shifts With You!",
                isRead = false
            });
            _context.SaveChanges();

            return RedirectToAction("RequestSubmitted", "Forms");
        }

        public ActionResult RequestSubmitted()
        {
            return View();
        }
    }
}