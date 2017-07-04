using System.Net.Http;
using System.Web.Http;

namespace Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public IHttpActionResult Get()
        {
            var user = Request.GetOwinContext().Authentication.User;
            return Ok("Welcome, " + user.Identity.Name + " authenticated: "+ user.Identity.IsAuthenticated);
        }
    }
}
