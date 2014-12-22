using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestingSystem.Startup))]
namespace TestingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
