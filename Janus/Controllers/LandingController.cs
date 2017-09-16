using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Janus.Controllers
{
    public class LandingController : Controller
    {
        // GET: CreateAccount
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateAccount()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}