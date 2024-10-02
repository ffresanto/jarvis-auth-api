using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.User
{
    public class PostAssociateUserToApplicationResponse
    {
        [JsonProperty("message")]
        public string? Info { get; set; }
    }
}
