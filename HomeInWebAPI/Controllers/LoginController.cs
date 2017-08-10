using System.Web.Http;
using HomeInWebAPI.Common;
using System.Web.Http.Cors;

namespace Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [Route("exlogin/{status}")]
        //[Route("exlogin")]
        [HttpGet]
        public IHttpActionResult ExternalLogin(string status)
        {
            var url = "/api/home/" + status;
            return new ChallengeResult("Facebook", url, this.Request, status);
            //return new ChallengeResult("Facebook", url, this.Request);
        }

        //[Route("exlogout")]
        //[HttpGet]
        //public IHttpActionResult ExternalLogout()
        //{
        //    return new ChallengeResult("Facebook", "/api/login/exlogin", this.Request);
        //}
    }
}
