using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.Jarvis
{
    public class PostCreateUserJarvisResponse
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }
    }
}
