using HomeInWebAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Controllers
{
    
    public class HomeController : BaseController
    {
        //public IFacebookService fbservice;

        public IHttpActionResult Get()
        {
            var identity = (ClaimsIdentity)User.Identity;

            IEnumerable<Claim> claims = identity.Claims;

            var user = Request.GetOwinContext().Authentication.User;
            var userProfile = new
            {
                name = claims.FirstOrDefault(x => x.Type == "name").ToString(),
                email = claims.FirstOrDefault(x => x.Type == "email").ToString(),
                facebookId = claims.FirstOrDefault(x => x.Type == "id").ToString(),
                isAuthenticated = user.Identity.IsAuthenticated,
                accessToken = claims.FirstOrDefault(x => x.Type == "access_token").ToString()
            };

            //var fbClient = new FacebookHttpConnect();
            //var fbService = new FacebookService(fbClient);
            ////var getFriendList = fbService.GetFriendListAsync(userProfile.accessToken);
            //var getList = fbService.GetAccountAsync(userProfile.accessToken);



            //return Ok("Welcome, " + user.Identity.Name + " authenticated: "+ user.Identity.IsAuthenticated);
            return Ok(userProfile);
        }
    }
}
