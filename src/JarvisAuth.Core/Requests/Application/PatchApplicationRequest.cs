namespace JarvisAuth.Core.Requests.Application
{
    public class PatchApplicationRequest
    {
        public Guid ApplicationId { get; set; }
        public string? Name { get; set; }
    }
}
