using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EstadiasUTTN.Startup))]
namespace EstadiasUTTN
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
