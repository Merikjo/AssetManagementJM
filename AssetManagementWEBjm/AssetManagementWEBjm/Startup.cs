using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AssetManagementWEBjm.Startup))]
namespace AssetManagementWEBjm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
