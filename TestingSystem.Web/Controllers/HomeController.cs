namespace TestingSystem.Web.Controllers
{
    using System.Web.Mvc;

    using TestingSystem.Data;
    using TestingSystem.Web.Controllers.Base;

    public class HomeController : BaseController
    {
        public HomeController(ITestingSystemData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Tests", new { area = string.Empty });
            }

            return this.View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}