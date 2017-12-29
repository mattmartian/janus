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