using JarvisAuth.Core.Messages;
using Newtonsoft.Json;

namespace JarvisAuth.Core.Responses.Shared
{
    public class Response<T>
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("data")]
        public T? Data { get; set; }

        [JsonProperty("errors")]
        public List<string> Errors { get; set; } = new List<string>();

        public Response()
        {
            StatusCode = 200;
            Success = true;
            Message = GlobalMessages.OPERATION_SUCCESS_200;
        }
    }
}
