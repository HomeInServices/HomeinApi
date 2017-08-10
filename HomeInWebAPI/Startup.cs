using Owin;
//using HomeInWebAPI.App_Start;
using System.Web.Http;

namespace HomeInWebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //SecurityConfig.Configure(app);
            WebApiConfig.Configure(app);
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;


        }
    }
}