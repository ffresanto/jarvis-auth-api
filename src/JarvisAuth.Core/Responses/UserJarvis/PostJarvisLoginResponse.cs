using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.Jarvis
{
    public class PostJarvisLoginResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
