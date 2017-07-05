using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Controllers
{
    
    public class HomeController : BaseController
    {
        public IHttpActionResult Get()
        {
            var identity = (ClaimsIdentity)User.Identity;

            IEnumerable<Claim> claims = identity.Claims;

            var user = Request.GetOwinContext().Authentication.User;

            return Ok("Welcome, " + user.Identity.Name + " authenticated: "+ user.Identity.IsAuthenticated);
        }
    }
}
