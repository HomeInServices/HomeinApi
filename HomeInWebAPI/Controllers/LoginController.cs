using System.Web.Http;
using HomeInWebAPI.Common;

namespace Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [Route("exlogin/{status}")]
        [HttpGet]
        public IHttpActionResult ExternalLogin(string status)
        {
            var url = "/api/home/" + status;
            return new ChallengeResult("Facebook", url, this.Request, status);
        }

        //[Route("exlogout")]
        //[HttpGet]
        //public IHttpActionResult ExternalLogout()
        //{
        //    return new ChallengeResult("Facebook", "/api/login/exlogin", this.Request);
        //}
    }
}
