using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Janus.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Welcome
        public ActionResult Welcome()
        {
            ViewBag.User = Session["FirstName"];

            return View();
        }
    }
}