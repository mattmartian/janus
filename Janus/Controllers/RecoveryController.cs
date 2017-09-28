using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Janus.Controllers
{
    public class RecoveryController : Controller
    {
        // GET: Recovery
        public ActionResult VerifyAccount()
        {
            return View();
        }

        public ActionResult SecurityQuestion()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }
    }
}