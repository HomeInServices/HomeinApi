using Owin;
using HomeInWebAPI.App_Start;

namespace HomeInWebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            SecurityConfig.Configure(app);
            WebApiConfig.Configure(app);
        }
    }
}