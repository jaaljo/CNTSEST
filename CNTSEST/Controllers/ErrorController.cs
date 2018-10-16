using System.Web.Mvc;

namespace CNTS.Controllers
{
    [Authorize]
    public class ErrorController : Controller
    {
        public ActionResult Denied()
        {
            return View();
        }

        public ActionResult CantErase()
        {
            return View();
        }

        public ActionResult OnlyRead()
        {
            return View();
        }
    }
}