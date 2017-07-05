//using Microsoft.Owin.Security.Facebook;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using System.Security.Claims;

//namespace HomeInWebAPI.Provider
//{
//    public class FacebookProvider : FacebookAuthenticationProvider
//    {
//        List<Claim> claims = new List<Claim>();

//        /// <summary>
//        /// Invoked whenever Facebook succesfully authenticates a user
//        /// </summary>
//        /// <param name="context">Contains information about the login session as well as the user <see cref="T:System.Security.Claims.ClaimsIdentity" />.</param>
//        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing the completed operation.</returns>
//        public override Task Authenticated(FacebookAuthenticatedContext context)
//        {
//            TryParseProperty(context, "first_name", claims.name);
//            TryParseProperty(context, "last_name", claims.LastName);
//            TryParseProperty(context, "picture.data.url", claims.PhotoUrl);

//            return base.Authenticated(context);
//        }

//        private void TryParseProperty(FacebookAuthenticatedContext context, string name, string targetName)
//        {
//            var value = context.User.SelectToken(name);
//            if (value != null)
//            {
//                context.Identity.AddClaim(targetName, value.ToString());
//            }
//        }

//    }
//}