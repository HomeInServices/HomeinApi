using HomeInWebAPI.Entities;
using HomeInWebAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Text.RegularExpressions;
using System.Web.Http.Cors;
using System.Threading.Tasks;

namespace Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/home")]
    public class HomeController : BaseController
    {
        private Dictionary<string, string> userProfile = new Dictionary<string, string>();

        /// <summary>
        /// REgister a user if not present.
        /// </summary>
        /// <returns></returns>
        //public IHttpActionResult Get(string status)
        //{
        //    if(User.Identity.IsAuthenticated)
        //    {

        //        var identity = (ClaimsIdentity)User.Identity;

        //        IEnumerable<Claim> claims = identity.Claims;

        //        var user = Request.GetOwinContext().Authentication.User;
        //        var userProfile = new
        //        {
        //            name = claims.FirstOrDefault(x => x.Type == "name").Value,
        //            email = claims.FirstOrDefault(x => x.Type == "email").Value,
        //            facebookId = claims.FirstOrDefault(x => x.Type == "id").Value,
        //            isAuthenticated = user.Identity.IsAuthenticated,
        //            accessToken = claims.FirstOrDefault(x => x.Type == "access_token").Value,
        //            picture = claims.FirstOrDefault(x => x.Type == "picture").Value,
        //            gender = claims.FirstOrDefault(x => x.Type == "gender").Value,
        //            personStatus = status

        //        };

        //        //var fbClient = new FacebookHttpConnect();
        //        //var fbService = new FacebookService(fbClient);
        //        ////var getFriendList = fbService.GetFriendListAsync(userProfile.accessToken);
        //        //var getData = fbService.GetAccountAsync(userProfile.accessToken);

        //        using (HomeInEntities context = new HomeInEntities())
        //        {
        //            Person person = context.People.FirstOrDefault(x => x.facebook_id == userProfile.facebookId);

        //            if(person != null)
        //            {
        //                var role = context.PersonRoles.FirstOrDefault(x => x.person_id == person.id);
        //                var roleName = context.Roles.FirstOrDefault(x => x.id == role.role_id);
        //                if(role != null)
        //                {
        //                    return Ok("role: " + roleName.name + "person: " + person.name);
        //                }
        //                else
        //                {
        //                    var roleId = context.Roles.FirstOrDefault(x => x.name == userProfile.personStatus);
        //                    context.PersonRoles.Add(new PersonRole
        //                    {
        //                        role_id = roleId.id,
        //                        person_id = person.id
        //                    });

        //                    context.SaveChanges();
        //                    return Ok("role added: "+ roleId.name + "person: "+ person.name);
        //                }

        //            }
        //            else
        //            {
        //                context.People.Add(new Person

        //                {
        //                    facebook_id = userProfile.facebookId,
        //                    name = userProfile.name,
        //                    picture = userProfile.picture,
        //                    email = userProfile.email,
        //                    gender = userProfile.gender
        //                }
        //             );
        //                context.SaveChanges();

        //                var personnew = context.People.FirstOrDefault(x => x.facebook_id == userProfile.facebookId);
        //                var roleId = context.Roles.FirstOrDefault(x => x.name == userProfile.personStatus);
        //                if (personnew != null) { 
        //                    context.PersonRoles.Add(new PersonRole
        //                    {
        //                        role_id = roleId.id,
        //                        person_id = personnew.id
        //                    });
        //                    context.SaveChanges();
        //                    return Ok("role added: " + roleId.name + "person: " + personnew.name);
        //                }
        //                return Ok("User Added: " + userProfile);
        //            }
        //        }    
        //    }
        //    else
        //    {
        //        return BadRequest("Authentication Failed. Please contact customer service");
        //    }
        //}


        [Route("reg")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUser(string loc, string aT, string fId)
        {

            var fbClient = new FacebookHttpConnect();
            var fbService = new FacebookService(fbClient);
            var getFriendList = await fbService.GetFriendListAsync(aT);
            var getData = await fbService.GetAccountAsync(aT);

            if (getData != null)
            { 
            using (HomeInEntities context = new HomeInEntities())
            {
                Person person = context.People.FirstOrDefault(x => x.facebook_id == fId);

                if (person != null)
                {
                    var role = context.PersonRoles.FirstOrDefault(x => x.person_id == person.id);
                    var roleName = context.Roles.FirstOrDefault(x => x.id == role.role_id);
                    if (role != null)
                    {
                        return Ok("role: " + roleName.name + "person: " + person.name);
                    }
                    else
                    {
                        var roleId = context.Roles.FirstOrDefault(x => x.name == loc);
                        context.PersonRoles.Add(new PersonRole
                        {
                            role_id = roleId.id,
                            person_id = person.id
                        });

                        context.SaveChanges();
                        return Ok("role added: " + roleId.name + "person: " + person.name);
                    }

                }
                else
                {
                    context.People.Add(new Person

                    {
                        facebook_id = fId,
                        name = getData.Name,
                        picture = getData.Picture,
                        email = getData.Email,
                        gender = getData.Gender
                    }
                 );
                    context.SaveChanges();

                    var personnew = context.People.FirstOrDefault(x => x.facebook_id == fId);
                    var roleId = context.Roles.FirstOrDefault(x => x.name == loc);
                    if (personnew != null)
                    {
                        context.PersonRoles.Add(new PersonRole
                        {
                            role_id = roleId.id,
                            person_id = personnew.id
                        });
                        context.SaveChanges();
                        return Ok("role added: " + roleId.name + "person: " + personnew.name);
                    }

                    else
                    {
                        return BadRequest("Authentication Failed. Please contact customer service");
                    }
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