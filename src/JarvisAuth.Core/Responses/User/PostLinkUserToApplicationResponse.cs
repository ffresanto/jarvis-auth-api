using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.User
{
    public class PostLinkUserToApplicationResponse
    {
        [JsonProperty("message")]
        public string? Info { get; set; }
    }
}
