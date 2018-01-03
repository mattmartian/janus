using System;
using System.Linq;
using System.Web.Mvc;

namespace Janus.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly JanusEntities _context;
        public int claimCount;
        public int illnessCount;
        public int bookOffCount;
        public int newClaims;
        public int newRequests;

        //Constructor for the controller that also gets the counts of certain absence claims
        public AdminDashboardController()
        {
            _context = new JanusEntities();
            claimCount = _context.AbsenceClaims.Count();
            illnessCount = _context.AbsenceClaims.Count(a => a.claimType == "Illness");
            bookOffCount = _context.AbsenceClaims.Count(a => a.claimType == "Book Off");
            newClaims = _context.AbsenceClaims.Count(a => a.isApproved == null);
            newRequests = _context.shiftRequests.Count(a => a.requestStatus == "Awaiting Manager Approval");
        }

        public ActionResult AdminDashboard()
        {
            if (!isLoggedIn())
            {
                return RedirectToAction("Login", "Login");
            }
            return RedirectToAction("ManageEmployees", "AdminDashboard");
        }

        /// <summary>
        /// The ManageEmployees action will show the manager a list of employees in which they can either edit the details of or fire(disable)
        /// </summary>
        /// <returns>The Manage Employees Dashboard View</returns>
        public ActionResult ManageEmployees()
        {
            int userid = Int32.Parse(@Session["userID"].ToString());
            string departmentName = Session["department"].ToString();
            //Make sure the user is logged in
            if (!isLoggedIn())
            {
                return RedirectToAction("Login", "Login");
            }
            var employeeData = (from a in _context.Users where a.employmentStatus == "Active" && a.userID != userid select new Janus.Models.EmployeeDetailViewModel { firstName = a.firstName, lastName = a.lastName, role = a.role, departmentName = a.departmentName, userID = a.userID });
            ViewBag.data = employeeData;
            if (employeeData.Count() > 0)
            {
                ViewBag.employeeCount = employeeData.Count();
            }
            else
            {
                ViewBag.employeeCount = null;
            }

            GatherStats();
            return View();
        }

        //Get the employees details
        [HttpGet]
        public ActionResult Save(int id)
        {
            var v = _context.Users.Where(a => a.userID == id).FirstOrDefault();
            return View(v);
        }

        //Save the new employees details
        [HttpPost]
        public ActionResult Save(Users emp)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (_context)
                {
                    if (emp.userID > 0)
                    {
                        //Update the record with the data entered into the form
                        var v = _context.Users.Where(a => a.userID == emp.userID).FirstOrDefault();
                        if (v != null)
                        {
                            v.firstName = emp.firstName;
                            v.lastName = emp.lastName;
                            v.role = emp.role;
                            v.departmentName = emp.departmentName;
                        }
                    }

                    _context.SaveChanges();
                    status = true;
                }
            }
            return RedirectToAction("ManageEmployees", "AdminDashboard");
        }

        //Get the employees details
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (_context)
            {
                var v = _context.Users.Where(a => a.userID == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        //Disable the users account but do not delete it from the database
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            bool status = false;
            using (_context)
            {
                var v = _context.Users.Where(a => a.userID == id).FirstOrDefault();
                var c = _context.Availibility.Where(a => a.userID == id).FirstOrDefault();
                if (v != null)
                {
                    v.employmentStatus = "Inactive";
                    v.fireDate = DateTime.Today;
                    _context.SaveChanges();

                    status = true;
                }
            }
            return RedirectToAction("ManageEmployees", "AdminDashboard");
        }

        /// <summary>
        ///  this action will Gather all of the absence claims sent to the manager for review
        /// </summary>
        /// <returns>The manage requests dashboarc view</returns>
        public ActionResult ManageRequests()
        {
            if (!isLoggedIn())
            {
                return RedirectToAction("Login", "Login");
            }
            var requestData = (from b in _context.AbsenceClaims join c in _context.Users on b.userID equals c.userID where b.isApproved == null select new Janus.Models.ClaimsVIewModel { claimID = b.claimID, firstName = c.firstName, lastName = c.lastName, startTime = b.startTime, endTime = b.endTime, description = b.description, claimType = b.claimType, isApproved = b.isApproved });
            if (requestData.Count() > 0)
            {
                ViewBag.requestCount = requestData.Count();
            }
            else
            {
                ViewBag.requestCount = null;
            }
            ViewBag.data = requestData;
            GatherStats();
            return View();
        }

        //Gather the claim data for approval
        [HttpGet]
        public ActionResult ApproveClaim(int id)
        {
            var v = _context.AbsenceClaims.Where(a => a.claimID == id).FirstOrDefault();

            return View(v);
        }

        //Approve the claim and notify all employees involved
        [HttpPost]
        public ActionResult ApproveClaim(AbsenceClaims claim)
        {
            string retrievedName = "";
            int userid = Int32.Parse(@Session["userID"].ToString());
            string claimDetails = "";
            bool status = false;

            if (ModelState.IsValid)
            {
                using (_context)
                {
                    if (claim.claimID > 0)
                    {
                        //Edit
                        var v = _context.AbsenceClaims.Where(a => a.claimID == claim.claimID).FirstOrDefault();

                        claimDetails = "Claim Type: " + v.claimType + "Claim Date : " + v.startTime + " - " + v.endTime;

                        var senderUsername = from u in _context.Users
                                             where u.userID == v.userID
                                             orderby u.userID
                                             select u;
                        foreach (var user in senderUsername)
                        {
                            retrievedName = user.firstName + " " + user.lastName;
                        }

                        _context.Messages.Add(new Messages
                        {
                            mailFromUserID = userid,
                            mailFromUsername = Session["name"].ToString(),
                            mailToUserID = v.userID,
                            mailToUsername = retrievedName,
                            subject = "Update To Your Recent Claim",
                            body = Session["name"].ToString() + " Has accepted Your Recent Absence Claim" + "\n" + " Claim Details:  " + claimDetails,
                            isRead = false
                        });
                        _context.SaveChanges();

                        //Make all of the shifts betweeen the dates specified on the absence open for other employees to take
                        DateTime start = Convert.ToDateTime(v.startTime);
                        DateTime end = Convert.ToDateTime(v.endTime);
                        var query = (from a in _context.Shifts where a.userID == userid select a).ToList();
                        foreach (var item in query)
                        {
                            DateTime shift = Convert.ToDateTime(item.shiftDate);
                            if (shift >= start && shift <= end)
                            {
                                item.status = "Open";
                                _context.SaveChanges();
                            }

                            if (v != null)
                            {
                                v.isApproved = true;
                            }
                        }

                        _context.SaveChanges();
                    }
                    status = true;
                }
            }

            //send a mesage to the claim sender of approval

            return RedirectToAction("ManageRequests", "AdminDashboard");
        }

        //Gather the claim data for denial
        [HttpGet]
        public ActionResult DenyClaim(int id)
        {
            var v = _context.AbsenceClaims.Where(a => a.claimID == id).FirstOrDefault();
            return View(v);
        }

        //Deny the claim and notify all employees involved in the claim
        [HttpPost]
        public ActionResult DenyClaim(AbsenceClaims claim)
        {
            string retrievedName = "";
            string claimDetails = "";
            int userid = Int32.Parse(@Session["userID"].ToString());
            bool status = false;
            if (ModelState.IsValid)
            {
                using (_context)
                {
                    if (claim.claimID > 0)
                    {
                        //Edit
                        var v = _context.AbsenceClaims.Where(a => a.claimID == claim.claimID).FirstOrDefault();
                        if (v != null)
                        {
                            v.isApproved = false;
                        }
                    }

                    _context.SaveChanges();
                    status = true;
                }
            }

            //Notify the sender that the claim has been denied
            var c = _context.AbsenceClaims.Where(a => a.claimID == claim.claimID).FirstOrDefault();
            claimDetails = "Claim Type: " + c.claimType + "Claim Date : " + c.startTime + " - " + c.endTime;
            var senderUsername = from u in _context.Users
                                 where u.userID == c.userID
                                 orderby u.userID
                                 select u;
            foreach (var user in senderUsername)
            {
                retrievedName = user.firstName + " " + user.lastName;
            }
            _context.Messages.Add(new Messages
            {
                mailFromUserID = userid,
                mailFromUsername = Session["name"].ToString(),
                mailToUserID = c.userID,
                mailToUsername = retrievedName,
                subject = "Update to your recent claim",
                body = Session["name"].ToString() + " Has accepted Your Recent Absence Claim" + "\n" + " Claim Details:  " + claimDetails,
                isRead = false
            });
            _context.SaveChanges();
            return RedirectToAction("ManageRequests", "AdminDashboard");
        }

        /// <summary>
        /// The make schedule action will present the admin with a form to make each days schedule for employees under their supervision
        /// </summary>
        /// <returns>MakeSchedule View</returns>
        public ActionResult MakeSchedule()
        {
            if (!isLoggedIn())
            {
                return RedirectToAction("Login", "Login");
            }
            var employees = (from b in _context.Users select new Janus.Models.EmployeeDetailViewModel { userID = b.userID, firstName = b.firstName, lastName = b.lastName });
            ViewBag.data = employees;
            GatherStats();
            return View();
        }

        /// <summary>
        /// The add schedule action will collect all of the form data entered by the manager, if the user is not availible on the time of the shift
        /// specified. The manager will be notified, if they are availbile, the employee will be sent their shift for that day
        /// </summary>
        /// <returns>Redirection to the make schedule form</returns>
        [HttpPost]
        public ActionResult AddSchedule()
        {
            int userID = Int32.Parse(Request["employees"]);
            string shiftDate = Request["shiftDate"];
            int start = Int32.Parse(Request["startTime"]);
            int end = Int32.Parse(Request["endTime"]);
            string position = Request["position"];
            string description = Request["description"];

            int startTime = 0;

            int endTime = 0;

            var query = from u in _context.Availibility
                        where u.userID.Equals(userID)
                        orderby u.userID
                        select u;

            foreach (var availibility in query)
            {
                startTime = (Int32)availibility.startTime;
                endTime = (Int32)availibility.endTime;
            }

            if (start < startTime)
            {
                ViewBag.error = "" +
                    " Employee Not Avalible At This Time";
            }
            if (end > endTime)
            {
                ViewBag.error = "Employee Not Avalible At This Time";
            }

            _context.Shifts.Add(new Shifts
            {
                userID = userID,
                shiftStart = start,
                shiftEnd = end,
                shiftDate = shiftDate,
                position = position,
                description = description,
                status = "Assigned",
            });
            _context.SaveChanges();

            return RedirectToAction("MakeSchedule", "AdminDashboard");
        }

        /// <summary>
        /// The DownloadSchedule action will show the manager the list of shifts for that day that they can then download
        /// </summary>
        /// <returns>Download Schedule view</returns>
        public ActionResult DownloadSchedule()
        {
            if (!isLoggedIn())
            {
                return RedirectToAction("Login", "Login");
            }

            DateTime today = DateTime.Today;
            string todayString = today.ToString("yyyy-MM-dd");

            var shiftData = (from b in _context.Shifts select b.shiftDate).Distinct();
            GatherStats();
            ViewBag.data = shiftData;
            return View();
        }

        /// <summary>
        /// The GenerateCSV action will export todays schedule to a csv for the manager to have on hand
        /// </summary>
        /// <returns>a csv file that will be automatically downloaded</returns>
        ///
        [HttpPost]
        public ActionResult GenerateCSV()
        {
            string day = Request["day"];
            var shiftData = (from b in _context.Shifts join c in _context.Users on b.userID equals c.userID where b.shiftDate == day select new Janus.Models.PrintSchedViewModel { firstName = c.firstName, lastName = c.lastName, shiftStart = b.shiftStart, shiftEnd = b.shiftEnd, shiftDate = b.shiftDate, position = b.position }).ToList();
            ViewBag.data = shiftData;
            var csv = string.Concat("Last Name" + "," + "First Name" + "," + "Shift Date" + "," + "Start Time" + "," + "End Time" + "," + "Position" + "\n");
            csv += string.Concat(shiftData.Select(re => re.lastName + "," + re.firstName + "," + re.shiftDate + "," + re.shiftStart + ":00" + "," + re.shiftEnd + ":00" + "," + re.position + "\n"));
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Schedule.csv");
        }

        /// <summary>
        /// Load all of the Shift Swap requests sent to the manager for review
        /// </summary>
        /// <returns>Shift Management View</returns>
        public ActionResult ShiftManagement()
        {
            if (!isLoggedIn())
            {
                return RedirectToAction("Login", "Login");
            }
            var shiftSwitchData = (from b in _context.shiftRequests where b.requestStatus == "Awaiting Manager Approval" select new Janus.Models.ShiftSwapViewModel { shiftRequestID = b.shiftRequestID, managerSignOff = b.managerSignOff, requestor = b.requestor, requestorShift = b.requestorShift, requestorID = b.requestorID, requestWith = b.requestWith, requestWithShift = b.requestWithShift, requestWithID = b.requestWithID, requestConfirmed = b.requestConfirmed, requestStatus = b.requestStatus });
            if (shiftSwitchData.Count() > 0)
            {
                ViewBag.requestCount = shiftSwitchData.Count();
            }
            else
            {
                ViewBag.requestCount = null;
            }
            ViewBag.data = shiftSwitchData;
            GatherStats();
            return View();
        }

        //Gather all of the data for approval for shfit swap
        [HttpGet]
        public ActionResult ApproveShift(int id)
        {
            var v = _context.shiftRequests.Where(a => a.shiftRequestID == id).FirstOrDefault();

            return View(v);
        }

        //Mark the request as approved and notify all of the employees involved in the request
        [HttpPost]
        public ActionResult ApproveShift(shiftRequests req)
        {
            string retrievedName = "";
            int userid = Int32.Parse(@Session["userID"].ToString());
            string requestDetails = "";
            bool status = false;

            if (ModelState.IsValid)
            {
                using (_context)
                {
                    if (req.shiftRequestID > 0)
                    {
                        //Edit
                        var v = _context.shiftRequests.Where(a => a.shiftRequestID == req.shiftRequestID).FirstOrDefault();
                        requestDetails = "Employee: " + v.requestWith + "Shift: " + v.requestWithShift + " ↔ " + "\n" + " Employee: " + v.requestor + "Shift: " + v.requestorShift;
                        var senderUsername = from u in _context.Users
                                             where u.userID == v.requestorID
                                             orderby u.userID
                                             select u;
                        foreach (var user in senderUsername)
                        {
                            retrievedName = user.firstName + " " + user.lastName;
                        }
                        _context.Messages.Add(new Messages
                        {
                            mailFromUserID = userid,
                            mailFromUsername = Session["name"].ToString(),
                            mailToUserID = v.requestorID,
                            mailToUsername = retrievedName,
                            subject = "Update To Your Recent Request",
                            body = Session["name"].ToString() + " Has accepted Your Recent Shift Swap Request " + "\n" + " Claim Details:  " + requestDetails,
                            isRead = false
                        });
                        _context.SaveChanges();
                        if (v != null)
                        {
                            v.managerSignOff = ViewBag.username;
                            v.requestStatus = "Approved";
                        }

                        _context.SaveChanges();
                        status = true;

                        var shiftOne = _context.Shifts.Where(b => b.userID == req.requestorID).FirstOrDefault();
                        var shiftTwo = _context.Shifts.Where(c => c.userID == req.requestWithID).FirstOrDefault();
                        if (shiftOne != null && shiftTwo != null)
                        {
                            var temp = shiftOne.userID;
                            shiftOne.userID = shiftTwo.userID;
                            shiftTwo.userID = temp;
                        }
                    }

                    _context.SaveChanges();
                    status = true;
                }
            }
            return RedirectToAction("ShiftManagement", "AdminDashboard");
        }

        //Get the data to deny the shift request
        [HttpGet]
        public ActionResult DenyShift(int id)
        {
            var v = _context.shiftRequests.Where(a => a.shiftRequestID == id).FirstOrDefault();
            return View(v);
        }

        //Mark the request as denied and notify all of the employees involved in the request
        [HttpPost]
        public ActionResult DenyShift(shiftRequests req)
        {
            string retrievedName = "";
            int userid = Int32.Parse(@Session["userID"].ToString());
            string requestDetails = "";
            bool status = false;
            if (ModelState.IsValid)
            {
                using (_context)
                {
                    if (req.shiftRequestID > 0)
                    {
                        //Edit
                        var v = _context.shiftRequests.Where(a => a.shiftRequestID == req.shiftRequestID).FirstOrDefault();
                        requestDetails = "Employee: " + v.requestWith + "Shift: " + v.requestWithShift + " ↔ " + "Employee: " + v.requestor + "Shift: " + v.requestorShift;
                        var senderUsername = from u in _context.Users
                                             where u.userID == v.requestorID
                                             orderby u.userID
                                             select u;
                        foreach (var user in senderUsername)
                        {
                            retrievedName = user.firstName + " " + user.lastName;
                        }
                        _context.Messages.Add(new Messages
                        {
                            mailFromUserID = userid,
                            mailFromUsername = Session["name"].ToString(),
                            mailToUserID = v.requestorID,
                            mailToUsername = retrievedName,
                            subject = "Update To Your Recent Request",
                            body = Session["name"].ToString() + " Has Denied Your Recent Shift Swap Request" + "\n" + " Claim Details:  " + requestDetails,
                            isRead = false
                        });
                        _context.SaveChanges();
                        if (v != null)
                        {
                            v.managerSignOff = ViewBag.username;
                            v.requestStatus = "Declined";
                        }
                    }
                    else

                        _context.SaveChanges();
                    status = true;
                }
            }
            return RedirectToAction("ShiftManagement", "AdminDashboard");
        }

        //Gather the counts of specific types of claims to show statistics to the manager
        [NonAction]
        private void GatherStats()
        {
            ViewBag.illnessCount = illnessCount;
            ViewBag.claimCount = claimCount;
            ViewBag.bookOffCount = bookOffCount;
            ViewBag.newClaims = newClaims;
            ViewBag.newRequests = newRequests;
        }

        //Verify the user is logged in before they can access any content on the application
        public bool isLoggedIn()
        {
            bool loggedIn = true;
            try
            {
                if (Session["accesslevel"].ToString() == "")
                {
                    loggedIn = false;
                }
            }
            catch (NullReferenceException)
            {
            }

            return loggedIn;
        }
    }
}