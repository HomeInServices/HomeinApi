using HomeInWebAPI.Entities;
using HomeInWebAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Text.RegularExpressions; 

namespace Controllers
{
    
    public class HomeController : BaseController
    {
        private Dictionary<string, string> userProfile = new Dictionary<string, string>();

        /// <summary>
        /// REgister a user if not present.
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            if(User.Identity.IsAuthenticated)
            {
                 
                var identity = (ClaimsIdentity)User.Identity;

                IEnumerable<Claim> claims = identity.Claims;

                var user = Request.GetOwinContext().Authentication.User;
                var userProfile = new
                {
                    name = claims.FirstOrDefault(x => x.Type == "name").Value,
                    email = claims.FirstOrDefault(x => x.Type == "email").Value,
                    facebookId = claims.FirstOrDefault(x => x.Type == "id").Value,
                    isAuthenticated = user.Identity.IsAuthenticated,
                    accessToken = claims.FirstOrDefault(x => x.Type == "access_token").Value,
                    picture = claims.FirstOrDefault(x => x.Type == "picture").Value,
                    gender = claims.FirstOrDefault(x => x.Type == "gender").Value
                };

                //var fbClient = new FacebookHttpConnect();
                //var fbService = new FacebookService(fbClient);
                ////var getFriendList = fbService.GetFriendListAsync(userProfile.accessToken);
                //var getData = fbService.GetAccountAsync(userProfile.accessToken);

                using (HomeInEntities context = new HomeInEntities())
                {
                    Person person = context.Person.FirstOrDefault(x => x.facebook_id == userProfile.facebookId);
                    
                    if(person != null)
                    {
                        return Ok(person);
                    }
                    else
                    {
                        context.Person.Add(new Person

                        {
                            facebook_id = userProfile.facebookId,
                            name = userProfile.name,
                            picture = userProfile.picture,
                            email = userProfile.email,
                            gender = userProfile.gender
                        }
                     );
                        context.SaveChanges();

                        return Ok("User Added: " + userProfile);
                    }
                }    
            }
            else
            {
                return BadRequest("Authentication Failed. Please contact customer service");
            }
        }
    }
}
