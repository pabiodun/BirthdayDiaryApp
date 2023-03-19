using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BirthdayDiaryApp.Startup))]
namespace BirthdayDiaryApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
