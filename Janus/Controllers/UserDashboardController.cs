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
            //initialize the database context to be used in all controller actions
            _context = new JanusEntities();
        }

        /// <summary>
        ///The account action will display all of the users details that are signed in as well as upcoming shifts they may have
        /// </summary>
        /// <returns>Account Page</returns>
        public ActionResult Account()
        {
            if (!isLoggedIn())
            {
                return RedirectToAction("Login", "Login");
            }
            DateTime today = DateTime.Today;
            DateTime dt = DateTime.Today;
            string data = "";
            int dataID = 0;
            int identification = Int32.Parse(Session["userID"].ToString());

            //Collect the users shifts
            var q = (from a in _context.Shifts where a.userID == identification select a);
            foreach (var shift in q)
            {
                data = shift.shiftDate;
                dataID = shift.shiftID;
            }
            if (data.Count() > 0)
            {
                dt = Convert.ToDateTime(data);
            }

            //if any of the users shifts are before today remove them
            //Note: Unfortunatley this was the way i had to do this, apparrently my tier of azure subscription did not give me the capability of scheduling a job in my sql server
            if (today > dt)
            {
                var shift = _context.Shifts.Find(dataID);
                _context.Shifts.Remove(shift);
                _context.SaveChanges();
            }

            var userInfo = (from a in _context.Users where a.userID == identification select new Janus.Models.AccountViewModel { userID = a.userID, firstName = a.firstName, lastName = a.lastName, birthDate = a.birthDate, phone = a.phone, email = a.email, streetAddress = a.streetAddress, postalCode = a.postalCode, role = a.role, departmentName = a.departmentName, hireDate = a.hireDate });
            var shiftData = (from a in _context.Shifts where a.userID == identification select new Janus.Models.ScheduleViewModel { shiftID = a.shiftID, userID = a.userID, shiftDate = a.shiftDate, shiftStart = a.shiftStart, shiftEnd = a.shiftEnd, position = a.position, description = a.description, status = a.status }).Take(4);

            if (shiftData.Count() == 0) //?
            {
                ViewBag.noData = "No Shifts For This Week!";
                ViewBag.shiftCount = null;
            }
            else
            {
                ViewBag.shiftCount = shiftData.Count();
            }

            ViewBag.shiftData = shiftData;
            ViewBag.data = userInfo;
            return View();
        }

        //Get the users data that is signed in for editing
        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            // collect the users profile data
            var v = _context.Users.Where(a => a.userID == id).FirstOrDefault();
            return View(v);
        }

        //Update the users detials with the new data inputted into the form
        [HttpPost]
        public ActionResult ProfileChanges(Users emp)
        {
            try
            {
                bool status = false;
                if (ModelState.IsValid)
                {
                    using (_context)
                    {
                        if (emp.userID > 0)
                        {
                            //Update the users profile data
                            var v = _context.Users.Where(a => a.userID == emp.userID).FirstOrDefault();
                            if (v != null)
                            {
                                v.phone = emp.phone;
                                v.streetAddress = emp.streetAddress;
                                v.postalCode = emp.postalCode;
                            }
                        }

                        _context.SaveChanges();
                        status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = "Could Not Change Profile Details";
            }

            return RedirectToAction("Account", "UserDashboard");
        }

        /// <summary>
        ///The Schedule action will display all of the users signed in  shifts that have been assigned to them
        /// </summary>
        /// <returns>Schedule Page</returns>
        public ActionResult Schedule()
        {
            if (!isLoggedIn())
            {
                return RedirectToAction("Login", "Login");
            }
            /**
             * Collect all of the users shifts
             * */
            int identification = Int32.Parse(Session["userID"].ToString());
            var shiftData = (from a in _context.Shifts where a.userID == identification select new Janus.Models.ScheduleViewModel { shiftID = a.shiftID, userID = a.userID, shiftDate = a.shiftDate, shiftStart = a.shiftStart, shiftEnd = a.shiftEnd, position = a.position, description = a.description, status = a.status });
            if (shiftData.Count() == 0) //?
            {
                ViewBag.noData = "No Shifts For This Week!";
            }

            ViewBag.data = shiftData;

            return View();
        }

        /// <summary>
        /// The MakeRequest action will display a list of forms to the user to select which claim they want to file
        /// </summary>
        /// <returns>MakeRequest Page</returns>
        public ActionResult MakeRequest()
        {
            if (!isLoggedIn())
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        /// <summary>
        /// The Mail controller will display all of the users messages for them to read and accept or deny
        /// </summary>
        /// <returns></returns>
        public ActionResult Mail()
        {
            if (!isLoggedIn())
            {
                return RedirectToAction("Login", "Login");
            }
            //Collect all of the messages addressed to the user that is signed in
            int identification = Int32.Parse(Session["userID"].ToString());
            var messages = (from a in _context.Messages where a.mailToUserID == identification && a.isRead == false select new Janus.Models.MailViewModel { messageID = a.messageID, mailFromUserID = a.mailFromUserID, mailFromUsername = a.mailFromUsername, mailToUserID = a.mailToUserID, mailToUsername = a.mailToUsername, subject = a.subject, body = a.body, shiftRequestID = a.shiftRequestID, isRead = a.isRead });

            if (messages.Count() == 0)
            {
                ViewBag.messageCount = null;
            }
            if (messages.Count() > 0)
            {
                ViewBag.messageCount = messages.Count();
            }
            ViewBag.data = messages;
            return View();
        }

        [HttpGet]
        public ActionResult ViewMessage(int id)
        {
            //Get the details of the message selected and mark the message as read
            var y = _context.Messages.Where(a => a.messageID == id).FirstOrDefault();

            return View(y);
        }

        [HttpPost]
        public ActionResult RemoveMessage(Messages msg)
        {
            try
            {
                bool status = false;
                if (ModelState.IsValid)
                {
                    using (_context)
                    {
                        if (msg.messageID > 0)
                        {
                            //Edit
                            var v = _context.Messages.Where(a => a.messageID == msg.messageID).FirstOrDefault();
                            if (v != null)
                            {
                                v.isRead = true;
                            }
                        }

                        _context.SaveChanges();
                        status = true;
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.error = "Could not Deny Message Request!";
            }
            return RedirectToAction("Mail", "UserDashboard");
        }

        //Get all the details inside the message to show the user
        [HttpGet]
        public ActionResult AcceptMail(int id)
        {
            //Get the details of the message selected and mark the message as read
            var v = _context.shiftRequests.Where(a => a.shiftRequestID == id).FirstOrDefault();
            var y = _context.Messages.Where(a => a.messageID == id).FirstOrDefault();
            try
            {
                if (y != null)
                {
                    y.isRead = true;
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                ViewBag.error = "Could not get Message Request!";
            }

            return View(v);
        }

        //Mark the message as accepted and send it off to the manager for review
        [HttpPost]
        public ActionResult AcceptMail(shiftRequests sr)
        {
            try
            {
                bool status = false;
                if (ModelState.IsValid)
                {
                    using (_context)
                    {
                        if (sr.shiftRequestID > 0)
                        {
                            //Edit
                            var v = _context.shiftRequests.Where(a => a.shiftRequestID == sr.shiftRequestID).FirstOrDefault();
                            if (v != null)
                            {
                                v.requestConfirmed = true;
                                v.requestStatus = "Awaiting Manager Approval";
                            }
                        }

                        _context.SaveChanges();
                        status = true;
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.error = "Could not Accept Message Request!";
            }
            //Mark that the recipient accepts the request and send it off to the manager for approval

            return RedirectToAction("Mail", "UserDashboard");
        }

        //Get all the details inside the message to show the user
        [HttpGet]
        public ActionResult DenyMail(int id)
        {
            //Get the message details
            var v = _context.shiftRequests.Where(a => a.shiftRequestID == id).FirstOrDefault();
            var y = _context.Messages.Where(a => a.messageID == id).FirstOrDefault();
            try
            {
                if (y != null)
                {
                    y.isRead = true;
                }
            }
            catch (Exception e)
            {
                ViewBag.error = "Could not get Message Request!";
            }

            return View(v);
        }

        //Mark the message as denied and do not continue
        [HttpPost]
        public ActionResult DenyMail(shiftRequests sr)
        {
            try
            {
                bool status = false;
                if (ModelState.IsValid)
                {
                    using (_context)
                    {
                        if (sr.shiftRequestID > 0)
                        {
                            //Edit
                            var v = _context.shiftRequests.Where(a => a.shiftRequestID == sr.shiftRequestID).FirstOrDefault();
                            if (v != null)
                            {
                                v.requestConfirmed = false;
                            }
                        }

                        _context.SaveChanges();
                        status = true;
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.error = "Could not Deny Message Request!";
            }
            //Decline the request from the sender

            return RedirectToAction("Mail", "UserDashboard");
        }

        //verify that the user is logged in before they can access the pages
        public bool isLoggedIn()
        {
            bool loggedIn = false;
            try
            {
                if (Session["accesslevel"].ToString() != "")
                {
                    loggedIn = true;
                }
            }
            catch (NullReferenceException)
            {
            }

            return loggedIn;
        }
    }
}