namespace JarvisAuth.Core.Requests.Application
{
    public class PostApplicationPermissionRequest
    {
        public Guid ApplicationId { get; set; }
        public string? Name { get; set; }
    }
}
