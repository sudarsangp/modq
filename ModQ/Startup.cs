using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ModQ.Startup))]
namespace ModQ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
