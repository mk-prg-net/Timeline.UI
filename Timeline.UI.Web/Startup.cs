using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Timeline.UI.Web.Startup))]
namespace Timeline.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
