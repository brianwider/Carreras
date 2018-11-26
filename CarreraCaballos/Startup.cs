using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarreraCaballos.Startup))]
namespace CarreraCaballos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
