using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.User
{
    public class PatchUserToggleEnabledResponse
    {
        [JsonProperty("message")]
        public string? Info { get; set; }
    }
}
