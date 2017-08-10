using HomeInWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HomeInWebAPI.Services
{
    public interface IFacebookService
    {
        Task<RegistrationModel> GetAccountAsync(string accessToken);
        //Task PostOnWallAsync(string accessToken, string message);
        Task<dynamic> GetFriendListAsync(string accessToken);
    }

    public class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;

        public FacebookService(IFacebookClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

        public async Task<RegistrationModel> GetAccountAsync(string accessToken)
        {
            var result = await _facebookClient.GetAsync<dynamic>(
                accessToken, "me", "fields=id,name,email,gender,picture");

            if (result == null)
            {
                //Error
                return new RegistrationModel();
            }

            var person = new RegistrationModel
            {
                Id = result.id,
                Email = result.email,
                Name = result.name,
                Gender = result.gender,
                //Picture = result.picture
                Picture = result.picture.data.url
            };

            return person;
        }

        public async Task<dynamic> GetFriendListAsync(string accessToken)
        {
            var result = await _facebookClient.GetAsync<dynamic>(
                accessToken, "me/friends", "fields=id,name,email,picture");

            return result;
        }

        //public async Task PostOnWallAsync(string accessToken, string message)
        //    => await _facebookClient.PostAsync(accessToken, "me/feed", new { message });
    }
}