using Newtonsoft.Json;

namespace TradeGameCRAPI.Models.GraphQLInputs
{
    public class FacebookAuthResponseInput
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }


        [JsonProperty("data_access_expiration_time")]
        public long DataAccessExpirationTime { get; set; }


        [JsonProperty("expiresIn")]
        public long ExpiresIn { get; set; }


        [JsonProperty("graphDomain")]
        public string GraphDomain { get; set; }


        [JsonProperty("signedRequest")]
        public string SignedRequest { get; set; }


        [JsonProperty("userID")]
        public string UserId
        {
            get; set;
        }
    }
}
