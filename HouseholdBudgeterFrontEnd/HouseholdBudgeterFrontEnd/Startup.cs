using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HouseholdBudgeterFrontEnd.Startup))]
namespace HouseholdBudgeterFrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
