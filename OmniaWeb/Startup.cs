using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OmniaWeb.Startup))]
namespace OmniaWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
