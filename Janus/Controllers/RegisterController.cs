using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Janus.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Register()
        {
            var companyList = new SelectList(new[]
              {
                new { ID = "1", Name = "The Octave: Musical Instruments and Lessons" }
                
            },
              "ID", "Name", 1);

           ViewData["companylist"] = companyList;





            return View();
        }

    }
}