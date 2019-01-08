using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IFZRBot.Startup))]
namespace IFZRBot
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
