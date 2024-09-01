namespace JarvisAuth.Core.Requests.Application
{
    public class PatchApplicationToggleEnabledRequest
    {
        public Guid ApplicationId { get; set; }
        public bool Enable { get; set; }
    }
}
