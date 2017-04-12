using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DrinksSale.Startup))]
namespace DrinksSale
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
