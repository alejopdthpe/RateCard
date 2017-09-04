using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RateCard.UI.Web.Startup))]
namespace RateCard.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
