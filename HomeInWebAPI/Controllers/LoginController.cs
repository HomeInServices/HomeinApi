using System.Web.Http;
using HomeInWebAPI.Common;

namespace Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : BaseController
    {
        [Route("exlogin")]
        [HttpGet]
        public IHttpActionResult ExternalLogin()
        {
            return new ChallengeResult("Facebook", "/api/home", this.Request);
        }

        //[Route("exlogout")]
        //[HttpGet]
        //public IHttpActionResult ExternalLogout()
        //{
        //    return new ChallengeResult("Facebook", "/api/login/exlogin", this.Request);
        //}
    }
}
