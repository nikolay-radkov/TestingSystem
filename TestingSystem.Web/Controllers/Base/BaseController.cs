namespace TestingSystem.Web.Controllers.Base
{
    using System.Web.Mvc;
    using TestingSystem.Data;

    public abstract class BaseController : Controller
    {
        public BaseController(ITestingSystemData data)
        {
            this.Data = data;
        }

        protected ITestingSystemData Data { get; set; }
    }
}