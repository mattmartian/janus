using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Janus.Controllers
{
    public class UserDashboardController : Controller
    {
        // GET: UserDashboard
        public ActionResult Welcome()
        {
            ViewBag.username = Session["FirstName"] + " " + Session["LastName"];
            ViewBag.userID = Session["userID"];
            ViewBag.userEmail = Session["email"];
            ViewBag.access = Session["accessLevel"];
            return View();
        }

        public ActionResult Schedule()
        {
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