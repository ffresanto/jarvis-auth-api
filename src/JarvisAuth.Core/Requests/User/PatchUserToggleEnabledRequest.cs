namespace JarvisAuth.Core.Requests.User
{
    public class PatchUserToggleEnabledRequest
    {
        public Guid UserId { get; set; }
        public bool Enable { get; set; }
    }
}
