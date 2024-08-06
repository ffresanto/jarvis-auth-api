using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.Authentication
{
    public class PostLoginResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
