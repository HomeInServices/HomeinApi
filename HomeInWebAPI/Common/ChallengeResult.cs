using Microsoft.Owin.Security;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace HomeInWebAPI.Common
{
    public class ChallengeResult : IHttpActionResult
    {
        private const string XsrfKey = "XsrfId";
        private const string personStatus = "personStatus";



        public ChallengeResult(string provider, string redirectUri, HttpRequestMessage request, string status)
            : this(provider, redirectUri, null, request, status)
        {
        }


        public ChallengeResult(string provider, string redirectUri, string userId, HttpRequestMessage request, string status)
        {
            AuthenticationProvider = provider;
            RedirectUri = redirectUri;
            UserId = userId;
            MessageRequest = request;
            PersonStatus = status;
        }

        public string AuthenticationProvider { get; private set; }

        public string RedirectUri { get; private set; }

        public string UserId { get; private set; }

        public HttpRequestMessage MessageRequest { get; private set; }

        public string PersonStatus { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var properties = new AuthenticationProperties() { RedirectUri = this.RedirectUri };
            if (UserId != null)
            {
                properties.Dictionary[XsrfKey] = UserId;
            }

            if(PersonStatus!= null)
            {
                properties.Dictionary[personStatus] = PersonStatus;
            }

            MessageRequest.GetOwinContext().Authentication.Challenge(properties, AuthenticationProvider);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = MessageRequest;

            return Task.FromResult(response);
        }
    }
}