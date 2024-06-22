using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.Types
{
    public class GetDocumentsTypesResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
