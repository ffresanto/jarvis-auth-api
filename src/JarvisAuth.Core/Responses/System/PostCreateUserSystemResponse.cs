using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.System
{
    public class PostCreateUserSystemResponse
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }
    }
}
