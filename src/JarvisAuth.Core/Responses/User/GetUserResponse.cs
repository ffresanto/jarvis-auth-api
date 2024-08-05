using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.User
{
    public class GetUserResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("Enabled")]
        public bool Enabled { get; set; }
    }
}
