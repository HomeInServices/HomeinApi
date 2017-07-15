using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Owin;
using System.Configuration;
using System.Threading.Tasks;
using HomeInWebAPI.Models;
using System.Security.Claims;

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
                    Scope = { "email", "public_profile", "user_friends", "user_photos" },
                    Fields = { "name", "email", "gender", "age_range", "birthday", "locale", "picture" },
                    Provider = new FacebookAuthenticationProvider
                    {
                        OnAuthenticated = (context) =>
                        {
                            
                            foreach (var x in context.User) {

                                context.Identity.AddClaim(new Claim(x.Key, x.Value.ToString()));
                            }
                            context.Identity.AddClaim(new Claim("access_token", context.AccessToken.ToString()));
                            //return Task.FromResult(0);
                            return Task.FromResult(context);
                        }
                    }
                };
                //facebookOptions.Scope.Add("email");
                app.UseFacebookAuthentication(facebookOptions);

        }
        const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";
    }
}