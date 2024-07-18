using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.Application
{
    public class PostApplicationPermissionResponse
    {
        [JsonProperty("ApplicationPermissionId")]
        public Guid ApplicationPermissionId { get; set; }
    }
}
