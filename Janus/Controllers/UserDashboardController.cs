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
        public ActionResult Account()
        {
            DateTime today = DateTime.Today;
            DateTime upcoming = DateTime.Today.AddDays(3);

            int identification = Int32.Parse(Session["userID"].ToString());
            var userInfo = (from a in _context.Users where a.userID == identification select new Janus.Models.AccountViewModel { userID = a.userID, firstName = a.firstName, lastName = a.lastName, birthDate = a.birthDate, phone = a.phone, email = a.email, streetAddress = a.streetAddress, postalCode = a.postalCode, role = a.role, departmentName = a.departmentName, hireDate = a.hireDate });
            var shiftData = (from a in _context.Shifts where a.userID == identification select new Janus.Models.ScheduleViewModel { shiftID = a.shiftID, userID = a.userID, shiftDate = a.shiftDate, shiftStart = a.shiftStart, shiftEnd = a.shiftEnd, position = a.position, description = a.description, status = a.status }).Take(2);
            if (shiftData.Count() == 0) //?
            {
                ViewBag.noData = "No Shifts For This Week!";
            }

            ViewBag.shiftData = shiftData;
            ViewBag.data = userInfo;
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
            int identification = Int32.Parse(Session["userID"].ToString());
            var messages = (from a in _context.Messages where a.mailToUserID == identification select new Janus.Models.MailViewModel { messageID = a.messageID, mailFromUserID = a.mailFromUserID, mailToUserID = a.mailToUserID, subject = a.subject, body = a.body, isRead = a.isRead });

            ViewBag.data = messages;
            return View();
        }
    }
}