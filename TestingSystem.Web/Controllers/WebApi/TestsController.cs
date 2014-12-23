using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestingSystem.Data;
using TestingSystem.Web.Controllers.WebApi.Base;

namespace TestingSystem.Web.Controllers.WebApi
{
    public class TestsController : BaseApiController
    {
        public TestsController(ITestingSystemData data)
            : base(data)
        {

        }

    }
}
