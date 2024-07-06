using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.Application
{
    public class PostCreateApplicationResponse
    {
        [JsonProperty("applicationId")]
        public Guid ApplicationId { get; set; }
    }
}
