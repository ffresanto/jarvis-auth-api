using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.User
{
    public class PostUserResponse
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }
    }
}
