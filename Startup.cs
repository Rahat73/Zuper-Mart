using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zuper_Mart.Startup))]
namespace Zuper_Mart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
