namespace TestingSystem.Web.Controllers.WebApi
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;

    using TestingSystem.Data;
    using TestingSystem.Web.Controllers.WebApi.Base;
    using TestingSystem.Web.InputModels;

    public class AccountController : BaseApiController
    {
        private const string URL = @"http://testingsystem.apphb.com/Token";
        private const string GRANT_TYPE = "grant_type";
        private const string PASSWORD = "password";
        private const string USERNAME = "username";
        private const string CONTENT_TYPE = "application/json";

        public AccountController(ITestingSystemData data)
            : base(data)
        {
        }

        public AccountController()
            : this(new TestingSystemData()) 
        {
        }

        // POST api/Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Login(LoginUserBindingModel model)
        {
            using (var client = new HttpClient())
            {
                var requestParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(GRANT_TYPE, PASSWORD),
                    new KeyValuePair<string, string>(USERNAME, model.Username),
                    new KeyValuePair<string, string>(PASSWORD, model.Password)
                };

                var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                var tokenServiceResponse = await client.PostAsync(URL, requestParamsFormUrlEncoded);
                var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
                var responseCode = tokenServiceResponse.StatusCode;
                var responseMsg = new HttpResponseMessage(responseCode)
                {
                    Content = new StringContent(responseString, Encoding.UTF8, CONTENT_TYPE)
                };

                return responseMsg;
            }
        }
    }
}
