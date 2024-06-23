using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.Jarvis
{
    public class GetDocumentTypeResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
