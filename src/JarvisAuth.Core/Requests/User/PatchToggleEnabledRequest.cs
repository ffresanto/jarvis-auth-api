namespace JarvisAuth.Core.Requests.User
{
    public class PatchToggleEnabledRequest
    {
        public Guid UserId { get; set; }
        public bool Enable { get; set; }
    }
}
