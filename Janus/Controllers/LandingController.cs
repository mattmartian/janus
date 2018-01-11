/*
 *I, Matthew Martin, student number 000338807, certify that this material is my original work.
    No other person's work has been used without due acknowledgement and I have not made my work available to anyone else.

    */

using System.Web.Mvc;

namespace Janus.Controllers
{
    public class LandingController : Controller
    {
        // GET: Landing Page
        public ActionResult Index()
        {
            return View();
        }
    }
}