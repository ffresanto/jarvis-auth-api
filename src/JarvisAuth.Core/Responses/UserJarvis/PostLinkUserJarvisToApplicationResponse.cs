using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.UserJarvis
{
    public class PostLinkUserJarvisToApplicationResponse
    {
        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
