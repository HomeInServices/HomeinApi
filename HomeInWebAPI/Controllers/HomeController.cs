using System.Collections.Generic;
using System.Linq;
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

            //var userProfile = new
            //{
            //    name = claims.FirstOrDefault(x => x.Type == "name").ToString(),
            //    email = claims.FirstOrDefault(x => x.Type == "email").ToString(),
            //    facebookId = claims.FirstOrDefault(x => x.Type == "id").ToString(),
            //    isAuthenticated = user.Identity.IsAuthenticated
            //};

            var userProfile = new
            {
                name = claims.FirstOrDefault(x => x.Type == "name").ToString(),
                email = claims.FirstOrDefault(x => x.Type == "email").ToString(),
                facebookId = claims.FirstOrDefault(x => x.Type == "id").ToString(),
                isAuthenticated = user.Identity.IsAuthenticated,
                accessToken = claims.FirstOrDefault(x => x.Type == "access_token").ToString()
            };
            
            //return Ok("Welcome, " + user.Identity.Name + " authenticated: "+ user.Identity.IsAuthenticated);
            return Ok("Welcome user: " + userProfile);
        }
    }
}
