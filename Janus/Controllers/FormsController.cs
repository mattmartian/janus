/*
 *I, Matthew Martin, student number 000338807, certify that this material is my original work.
    No other person's work has been used without due acknowledgement and I have not made my work available to anyone else.

    */

using System;
using System.Linq;
using System.Web.Mvc;

namespace Janus.Controllers
{
    public class FormsController : Controller
    {
        private readonly JanusEntities _context;

        //Set up database context globally
        public FormsController()
        {
            _context = new JanusEntities();
        }

        // Return the book off form
        public ActionResult BookOff()
        {
            return View();
        }

        /// <summary>
        /// Collect the data entered by the user and file a claim to book time off for the sender of the form
        /// </summary>
        /// <returns>Confirmation that the claim was filed</returns>
        [HttpPost]
        public ActionResult TimeOffRequest()
        {
            //Collect the data entered into the form
            int user = Int32.Parse(Session["userID"].ToString());
            string start = Request["startTime"];
            string end = Request["endTime"];
            string desc = Request["description"];

            //Add the request into the database for the manager to review
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

        //Return the absence claim form
        public ActionResult AbsenceClaim()
        {
            return View();
        }

        /// <summary>
        /// This Action will be responsible for gathering the user enterd data and filing an absence claim on behalf of the user
        /// </summary>
        /// <returns>Confirmation that the claim was filed</returns>
        [HttpPost]
        public ActionResult FileClaim()
        {
            //Collect form data entered
            int user = Int32.Parse(Session["userID"].ToString());
            string start = Request["startTime"];
            string end = Request["endTime"];
            string desc = Request["description"];
            string absType = Request["claimType"];

            //Add the request into the database for the manager to review
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

        /// <summary>
        /// Load the list of all employees for the user to select to switch a shift with
        /// </summary>
        /// <returns>The form for the user to select an employee</returns>
        public ActionResult SelectEmployee()
        {
            //grab all of the names of the employees
            var employees = (from b in _context.Users select new Janus.Models.EmployeeDetailViewModel { userID = b.userID, firstName = b.firstName, lastName = b.lastName });
            ViewBag.data = employees;

            return View();
        }

        /// <summary>
        /// When the employee is selected the user will be redirected to switch a shift with that user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EmployeeSelected()
        {
            //grab the employee selected by the user
            string requestWith = Request["requestWith"];

            TempData["requestWith"] = requestWith;

            return RedirectToAction("SwitchShift", "Forms");
        }

        /// <summary>
        /// This action is responsible for sending a form for the user to pick a shift to switch
        /// </summary>
        /// <returns>The form to switch a shift</returns>
        public ActionResult SwitchShift()
        {
            //Grab the info from the requestore and requestee and start the claim
            int userID = Int32.Parse(@Session["userID"].ToString());
            int requestedWith = Int32.Parse(TempData["requestWith"].ToString());
            var employees = (from b in _context.Users select new Janus.Models.EmployeeDetailViewModel { userID = b.userID, firstName = b.firstName, lastName = b.lastName });
            var requestorShift = (from a in _context.Shifts where a.userID == userID && a.status == "Assigned" select new Janus.Models.ShiftViewModel { shiftID = a.shiftID, userID = a.userID, shiftDate = a.shiftDate, shiftStart = a.shiftStart, shiftEnd = a.shiftEnd, position = a.position, description = a.description, status = a.status });
            var requestWithShift = (from a in _context.Shifts where a.userID == requestedWith && a.status == "Assigned" select new Janus.Models.ShiftViewModel { shiftID = a.shiftID, userID = a.userID, shiftDate = a.shiftDate, shiftStart = a.shiftStart, shiftEnd = a.shiftEnd, position = a.position, description = a.description, status = a.status });
            ViewBag.data = employees;
            TempData["reqWith"] = requestedWith;
            ViewBag.requestorData = requestorShift;
            ViewBag.requestWithData = requestWithShift;
            return View();
        }

        /// <summary>
        ///  This action is responsible for sending the request to the employee that the user would like to swap shifts with for their confirmation
        /// </summary>
        /// <returns>Confrimation that the request was submitted</returns>
        [HttpPost]
        public ActionResult FileShiftChange()
        {
            //collect form data
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
            string requestWithShiftString = retDate.ToString() + " " + retStart.ToString() + ":00" + " " + retEnd.ToString() + ":00";

            var requestWithUserName = from u in _context.Users
                                      where u.userID == requestWith
                                      orderby u.userID
                                      select u;
            foreach (var user in requestWithUserName)
            {
                retrievedName = user.firstName + " " + user.lastName;
            }
            //Add the request into the database for the manager to review
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

            int requestCount = _context.shiftRequests.Count();
            //Send the user a notification of the request
            _context.Messages.Add(new Messages
            {
                mailFromUserID = userid,
                mailFromUsername = Session["name"].ToString(),
                mailToUserID = requestWith,
                mailToUsername = retrievedName,
                subject = "Shift Swap Request",
                body = requestor + " Would Like To Switch Shifts With You!",
                shiftRequestID = requestCount,
                isRead = false
            });

            _context.SaveChanges();

            return RedirectToAction("RequestSubmitted", "Forms");
        }

        //Return the confirmation view
        public ActionResult RequestSubmitted()
        {
            return View();
        }
    }
}