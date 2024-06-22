using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.Types
{
    public class GetGendersTypesResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
