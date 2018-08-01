using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DiplaTool.Startup))]
namespace DiplaTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
