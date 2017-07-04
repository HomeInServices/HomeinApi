using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Owin;
using System.Configuration;
using System.Threading.Tasks;
using HomeInWebAPI.Models;

namespace HomeInWebAPI.App_Start
{
    public class SecurityConfig
    {
        public static void Configure(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ExternalCookie
                //LoginPath = new PathString("Login/ExternalLogin")
            });

            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure google authentication
            //var options = new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "your app client id",
            //    ClientSecret = "your app client secret"
            //};

            //app.UseGoogleAuthentication(options);

            // Facebook : Create New App
            // https://developers.facebook.com/apps
            //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("FacebookAppId")))
            //{
                var facebookOptions = new FacebookAuthenticationOptions
                {
                    //AppId = ConfigurationManager.AppSettings.Get("FacebookAppId"),
                    //AppSecret = ConfigurationManager.AppSettings.Get("FacebookAppSecret"),
                    AppId = "1734115113548184",
                    AppSecret = "b9bfbe8f4d42bc44cc5450f74c25e6f9",
                    Scope = { "email" },
                    Fields = { "name", "email", "gender", "friends" },
                    Provider = new FacebookAuthenticationProvider
                    {
                        OnAuthenticated = (context) =>
                        {
                            context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, XmlSchemaString, "Facebook"));
                            foreach (var x in context.User)
                            {
                                var claimType1 = string.Format("urn:facebook:{0}", x.Key);
                                string claimValue = x.Value.ToString();
                                if (!context.Identity.HasClaim(claimType1, claimValue))
                                    context.Identity.AddClaim(new System.Security.Claims.Claim(claimType1, claimValue, XmlSchemaString, "Facebook"));

                            }

                            return Task.FromResult(0);
                        }
                    }
                };
                //facebookOptions.Scope.Add("email");
                app.UseFacebookAuthentication(facebookOptions);

        }
        const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";
    }
}