using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace HomeInWebAPI.Services
{
    public interface IFacebookClient
    {
        Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null);
        //Task PostAsync(string accessToken, string endpoint, object data, string args = null);
    }

    public class FacebookHttpConnect : IFacebookClient
    {
        private readonly HttpClient _httpClient;
        private readonly string facebookURL = "https://graph.facebook.com/v2.9/";

        public FacebookHttpConnect()
        {
            //_httpClient = new HttpClient
            //{
            //    BaseAddress = new Uri("https://graph.facebook.com/v2.9/")
            //    //BaseAddress = new Uri("https://graph.facebook.com/#/v2.9/")
            //};
            //_httpClient.DefaultRequestHeaders
            //    .Accept
            //    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _httpClient = new HttpClient();
            
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null)
        {
            var url = this.facebookURL+endpoint + "?" + args + "&" + "access_token=" + accessToken;
            //var response = await _httpClient.GetAsync($"{endpoint}?access_token={accessToken}&{args}");
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }

        //public async Task PostAsync(string accessToken, string endpoint, object data, string args = null)
        //{
        //    var payload = GetPayload(data);
        //    await _httpClient.PostAsync($"{endpoint}?access_token={accessToken}&{args}", payload);
        //}

        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        }
    }
}