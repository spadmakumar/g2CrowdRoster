using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(G2CrowdRoster.Startup))]
namespace G2CrowdRoster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
