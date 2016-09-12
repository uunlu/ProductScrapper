using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProductScrapper.Startup))]
namespace ProductScrapper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
