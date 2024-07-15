using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.Application
{
    public class PostApplicationResponse
    {
        [JsonProperty("applicationId")]
        public Guid ApplicationId { get; set; }
    }
}
