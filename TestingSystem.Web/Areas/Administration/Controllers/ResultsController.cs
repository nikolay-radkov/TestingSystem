namespace TestingSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using TestingSystem.Data;
    using TestingSystem.Web.Areas.Administration.ViewModels;
    using TestingSystem.Web.Controllers.Base;

    [Authorize(Roles = "admin")]
    public class ResultsController : BaseController
    {
        public ResultsController(ITestingSystemData data)
            : base(data)
        {
        }

        // GET: Administration/Results
        public ActionResult Index()
        {
            var results = this.Data
                                .Results
                                .All()
                                .Project()
                                .To<ResultViewModel>()
                                .ToList();

            return this.View(results);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Data.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
