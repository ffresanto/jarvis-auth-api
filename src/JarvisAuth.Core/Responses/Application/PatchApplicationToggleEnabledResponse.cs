using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.Application
{
    public class PatchApplicationToggleEnabledResponse
    {
        [JsonProperty("message")]
        public string? Info { get; set; }
    }
}
