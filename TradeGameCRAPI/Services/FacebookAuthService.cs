using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TradeGameCRAPI.Interfaces;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Services
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private readonly string appId;
        private readonly string appSecret;
        private readonly HttpClient httpClient;

        public FacebookAuthService(IConfiguration configuration)
        {
            appId = configuration["FacebookAuthSettings:AppId"];
            appSecret = configuration["FacebookAuthSettings:AppSecret"];
            httpClient = new HttpClient();
        }

        public async Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken)
        {
            var url = GetValidateAccessTokenURL(accessToken);
            var facebookTokenValidationResultStr = await httpClient.GetStringAsync(url);
            var facebookTokenValidationResult = JsonConvert.DeserializeObject
                <FacebookTokenValidationResult>(facebookTokenValidationResultStr);

            return facebookTokenValidationResult;
        }

        public async Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken)
        {
            var url = GetUserInfoURL(accessToken);
            var facebookUserInfoResultStr = await httpClient.GetStringAsync(url);
            var facebookUserInfoResult = JsonConvert.DeserializeObject
                <FacebookUserInfoResult>(facebookUserInfoResultStr);

            return facebookUserInfoResult;
        }

        private string GetValidateAccessTokenURL(string accessToken)
        {
            var baseURL = "https://graph.facebook.com/debug_token";
            var accessTokenParam = $"{appId}|{appSecret}";

            return $"{baseURL}?input_token={accessToken}&access_token={accessTokenParam}";
        }

        private string GetUserInfoURL(string accessToken)
        {
            var baseURL = "https://graph.facebook.com/me";
            var fieldsParam = "first_name,last_name,picture,email";

            return $"{baseURL}?fields={fieldsParam}&access_token={accessToken}";
        }
    }
}
