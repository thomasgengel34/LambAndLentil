using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LambAndLentil.UI.Startup))]
namespace LambAndLentil.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
