using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Janus.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly JanusEntities _context;
        // GET: AdminDashboard

        public AdminDashboardController()
        {
            _context = new JanusEntities();
        }

        public ActionResult AdminDashboard()
        {
            return RedirectToAction("ManageEmployees", "AdminDashboard");
        }

        public ActionResult ManageEmployees()
        {
            var employeeData = (from a in _context.Users select new Janus.Models.EmployeeDetailViewModel { firstName = a.firstName, lastName = a.lastName, role = a.role, departmentName = a.departmentName, userID = a.userID });
            ViewBag.data = employeeData;

            return View();
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            var v = _context.Users.Where(a => a.userID == id).FirstOrDefault();
            return View(v);
        }

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
                        //Edit
                        var v = _context.Users.Where(a => a.userID == emp.userID).FirstOrDefault();
                        if (v != null)
                        {
                            v.firstName = emp.firstName;
                            v.lastName = emp.lastName;
                            v.role = emp.role;
                            v.departmentName = emp.departmentName;
                        }
                    }
                    else
                    {
                        //Save
                        _context.Users.Add(emp);
                    }
                    _context.SaveChanges();
                    status = true;
                }
            }
            return RedirectToAction("ManageEmployees", "AdminDashboard");
        }

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
                    _context.Availibility.Remove(c);
                    _context.SaveChanges();
                    _context.Users.Remove(v);
                    _context.SaveChanges();

                    status = true;
                }
            }
            return RedirectToAction("ManageEmployees", "AdminDashboard");
        }

        public ActionResult ManageRequests()
        {
            var requestData = (from b in _context.AbsenceClaims where b.isApproved == null select new Janus.Models.ClaimsVIewModel { claimID = b.claimID, userID = b.userID, startTime = b.startTime, endTime = b.endTime, description = b.description, claimType = b.claimType, isApproved = b.isApproved });
            ViewBag.data = requestData;

            return View();
        }

        [HttpGet]
        public ActionResult ApproveClaim(int id)
        {
            var v = _context.AbsenceClaims.Where(a => a.claimID == id).FirstOrDefault();

            return View(v);
        }

        [HttpPost]
        public ActionResult ApproveClaim(AbsenceClaims claim)
        {
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
                            v.isApproved = true;
                        }
                    }

                    _context.SaveChanges();
                    status = true;
                }
            }
            return RedirectToAction("ManageRequests", "AdminDashboard");
        }

        [HttpGet]
        public ActionResult DenyClaim(int id)
        {
            var v = _context.AbsenceClaims.Where(a => a.claimID == id).FirstOrDefault();
            return View(v);
        }

        [HttpPost]
        public ActionResult DenyClaim(AbsenceClaims claim)
        {
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
            return RedirectToAction("ManageRequests", "AdminDashboard");
        }

        public ActionResult MakeSchedule()
        {
            return View();
        }

        public ActionResult DownloadSchedule()
        {
            return View();
        }

        public ActionResult ShiftManagement()
        {
            var shiftSwitchData = (from b in _context.shiftRequests where b.requestStatus == "Pending Approval" select new Janus.Models.ShiftSwapViewModel { shiftRequestID = b.shiftRequestID, managerSignOff = b.managerSignOff, requestor = b.requestor, requestorShift = b.requestorShift, requestWith = b.requestWith, requestWithShift = b.requestWithShift, requestConfirmed = b.requestConfirmed, requestStatus = b.requestStatus });
            ViewBag.data = shiftSwitchData;
            return View();
        }

        [HttpGet]
        public ActionResult ApproveShift(int id)
        {
            var v = _context.shiftRequests.Where(a => a.shiftRequestID == id).FirstOrDefault();

            return View(v);
        }

        [HttpPost]
        public ActionResult ApproveShift(shiftRequests req)
        {
            bool status = false;
            string requestor = "";
            int userID = 0;

            int secondUserID = 0;

            string requestWith = "";
            if (ModelState.IsValid)
            {
                using (_context)
                {
                    if (req.shiftRequestID > 0)
                    {
                        //Edit
                        var v = _context.shiftRequests.Where(a => a.shiftRequestID == req.shiftRequestID).FirstOrDefault();
                        if (v != null)
                        {
                            requestor = v.requestor;
                            requestWith = v.requestWith;

                            v.managerSignOff = ViewBag.username;
                            v.requestStatus = "Approved";
                        }
                    }

                    _context.SaveChanges();
                    status = true;
                }
            }
            return RedirectToAction("ShiftManagement", "AdminDashboard");
        }

        [HttpGet]
        public ActionResult DenyShift(int id)
        {
            var v = _context.shiftRequests.Where(a => a.shiftRequestID == id).FirstOrDefault();
            return View(v);
        }

        [HttpPost]
        public ActionResult DenyShift(shiftRequests req)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (_context)
                {
                    if (req.shiftRequestID > 0)
                    {
                        //Edit
                        var v = _context.shiftRequests.Where(a => a.shiftRequestID == req.shiftRequestID).FirstOrDefault();
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
    }
}