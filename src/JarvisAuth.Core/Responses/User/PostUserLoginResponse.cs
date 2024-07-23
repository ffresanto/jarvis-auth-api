using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.User
{
    public class PostUserLoginResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
