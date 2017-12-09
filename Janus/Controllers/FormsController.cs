using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Janus.Controllers
{
    public class FormsController : Controller
    {
        // GET: Forms
        public ActionResult BookOff()
        {
            return View();
        }

        public ActionResult AbsenceClaim()
        {
            return View();
        }

        public ActionResult SwitchShift()
        {
            return View();
        }
    }
}