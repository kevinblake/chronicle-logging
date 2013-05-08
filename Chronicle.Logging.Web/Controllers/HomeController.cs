using System.Web.Mvc;
using Chronicle.Logging.Business;

namespace Chronicle.Logging.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            LoggingContext.General.Error("test");
            return Content("[]");
        }
    }
}