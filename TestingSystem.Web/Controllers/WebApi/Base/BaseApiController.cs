namespace TestingSystem.Web.Controllers.WebApi.Base
{
    using System.Web.Http;
    using TestingSystem.Data;

    public abstract class BaseApiController : ApiController
    {
        public BaseApiController(ITestingSystemData data)
        {
            this.Data = data;
        }

        protected ITestingSystemData Data { get; set; }
    }
}
