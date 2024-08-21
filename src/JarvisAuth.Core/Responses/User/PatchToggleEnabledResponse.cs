using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.User
{
    public class PatchToggleEnabledResponse
    {
        [JsonProperty("message")]
        public string? Info { get; set; }
    }
}
