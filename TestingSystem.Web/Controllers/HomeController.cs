using System.Web.Mvc;
using TestingSystem.Data;
using TestingSystem.Web.Controllers.Base;

namespace TestingSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ITestingSystemData data)
            : base (data)
        {
        }

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Tests", new { area = "" });
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}