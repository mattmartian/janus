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
        private Janus.Models.UserDB userDB = new Janus.Models.UserDB();
        private readonly JanusEntities _context;
        // GET: AdminDashboard

        public AdminDashboardController()
        {
            _context = new JanusEntities();
        }

        public ActionResult AdminDashboard()
        {
            return View();
        }

        public ActionResult ManageEmployees()
        {
            return View();
        }

        public ActionResult ManageRequests()
        {
            return View();
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
            return View();
        }
    }
}