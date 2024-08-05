using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.User
{
    public class PostUserPermissionResponse
    {
        [JsonProperty("message")]
        public string? Info { get; set; }
    }
}
