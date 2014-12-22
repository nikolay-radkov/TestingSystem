using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestingSystem.Web.Startup))]
namespace TestingSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
