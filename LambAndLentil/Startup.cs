using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LambAndLentil.Startup))]
namespace LambAndLentil
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
