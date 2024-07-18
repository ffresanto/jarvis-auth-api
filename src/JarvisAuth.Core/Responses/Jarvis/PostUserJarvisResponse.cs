using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.Jarvis
{
    public class PostUserJarvisResponse
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }
    }
}
